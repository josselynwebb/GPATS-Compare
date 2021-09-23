'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports Microsoft.VisualBasic
Imports Microsoft.Win32

Public Module FuncGenMain


	'=========================================================
    '//2345678901234567890123456789012345678901234567890123456789012345678901234567890
    '///////////////////////////////////////////////////////////////////////////////////////////////////////
    '//
    '// Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module
    '//
    '// File:       Main.bas
    '//
    '// Date:       14FEB06
    '//
    '// Purpose:    SAIS: Function Generator Front Panel
    '//
    '// Instrument: Ri3152A Function Generator (Fg)
    '//
    '//
    '// Revision History
    '// Rev      Date                  Reason                                       Author
    '// =======  =======    =======================================             ===================
    '// 1.1.0.0  27Oct06    Initial Release                                     EADS, North America Defense
    '// 1.1.0.1  14Nov06    The following changes were made:                    M.Hendricksen, EADS
    '//                     - Corrected SFP to only send queries on initialization
    '//                         because of how CICL interrupts if it is in use.
    '//                         before it would query then turn around and send a cmd
    '//                         that it just query to set instr.
    '//                     - Corrected SFP to only send queries on a Reset command
    '//                         it also used to query then send a command.  Also changed
    '//                         because of CICL.
    '//                     - Corrected the Trigger Delay and Trigger Burst delay to scroll
    '//                         and to accept discrete values.
    '//                     - Corrected the Trigger Level to scroll correctly.
    '//                     - Changed Logo to be just ViperT with no truck.
    '//                     - Added Code to close the program when the Windows 'X' is used
    '// 1.2.0.1  21Dec06    The following changes were made:                    M.Hendricksen, EADS
    '//                     - Corrected code in AdjustVoltOffs, to coorespond
    '//                         to the correct Offset limits.
    '// 1.2.0.2  07Mar07    The following changes were made:                    M.Hendricksen, EADS
    '//                     - frmRac3152.txtExpoChange was deleted.  This is due
    '//                         to the fact that it would send the instrument a cmd
    '//                         on every key stroke.  Now txtExpo_KeyPress does only
    '//                         when it is a return.
    '//                     - Corrected frmHelp to only use one help file due to the second help
    '//                         file not being found or neeeded.
    '////////////////////////////////////////////////////////////////////////////////////////////////////////
    '


    '----------------- VXIplug&play Driver Declarations ---------
    'Declare Function ri3151_init Lib "ri3151.dll" (ByVal resourceName$, ByVal IDQuery%, ByVal resetDevice%, instrumentHandle&) As Long
    'Declare Function ri3151_close Lib "ri3151.dll" (ByVal instrumentHandle&) As Long
    'Declare Function ri3151_error_message Lib "ri3151.dll" (ByVal instrumentHandle&, ByVal ErrorCode&, ByVal errorMessage$) As Long
    'Declare Function ri3151_error_query Lib "ri3151.dll" (ByVal instrumentHandle&, ErrorCode&, ByVal errorMessage$) As Long
    'Declare Function viRead Lib "VISA32.DLL" Alias "#256" (ByVal vi As Long, ByVal Buffer As String, ByVal Count As Long, retCount As Long) As Long
    'Declare Function viWrite Lib "VISA32.DLL" Alias "#257" (ByVal vi As Long, ByVal Buffer As String, ByVal Count As Long, retCount As Long) As Long

    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer


    Declare Function atxml_Close Lib "AtXmlApi.dll"  As Integer

    'Prototype:
    'int atxml_ValidateRequirements(ATXML_ATML_Snippet* TestRequirements,
    '                               ATXML_XML_Filename* Allocation,
    '                               ATXML_XML_String*   Availability,
    '                               int                 BufferSize)
    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Integer) As Integer

    Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer

    ' WMT 1/20/2006 Changed the BufferSize declaration
    ' from:  BufferSize As Long,
    ' to:    ByVal BufferSize as Integer,
    ' BUT ByRef ActReadLen As Long BECAUSE IT IS A PONITER
    Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Integer, ByRef ActReadLen As Integer) As Integer


    Declare Function atxmlDF_viClear Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer) As Integer


    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{0833A730-2EBD-4585-A31B-2E0CD287FDE4}"
    Public Const proctype As String = "SFP"

    Declare Function WritePrivateProfileString Lib "Kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Function GetPrivateProfileString Lib "Kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    '----------------- API / DLL Declarations -------------------

    Structure STARTUPINFO
        Dim cb As Integer
        Dim lpReserved As String
        Dim lpDeskTop As String
        Dim lpTitle As String
        Dim dwX As Integer
        Dim dwY As Integer
        Dim dwXSize As Integer
        Dim dwYSize As Integer
        Dim dwXCountChars As Integer
        Dim dwYCountChars As Integer
        Dim dwFillAttribute As Integer
        Dim dwFlags As Integer
        Dim wShowWindow As Short
        Dim cbReserved2 As Short
        Dim lpReserved2 As Integer
        Dim hStdInput As Integer
        Dim hStdOutput As Integer
        Dim hStdError As Integer
    End Structure

    Structure PROCESS_INFORMATION
        Dim hProcess As Integer
        Dim hThread As Integer
        Dim dwProcessID As Integer
        Dim dwThreadID As Integer
    End Structure

    Private Declare Function CreateProcessA Lib "Kernel32" (ByVal lpApplicationName As Integer, ByVal lpCommandLine As String, ByVal lpProcessAttributes As Integer, ByVal lpThreadAttributes As Integer, ByVal bInheritHandles As Integer, ByVal dwCreationFlags As Integer, ByVal lpEnvironment As Integer, ByVal lpCurrentDirectory As Integer, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Object
    Private Declare Function WaitForSingleObject Lib "Kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
    Private Declare Function CloseHandle Lib "Kernel32" (ByVal hObject As Integer) As Integer

    Private Const INFINITE As Short = -1
    Private Const NORMAL_PRIORITY_CLASS As Short = &H20

    ' WindowStates
    Public Const NORMAL As Short = 0 ' 0 - Normal
    Public Const MINIMIZED As Short = 1 ' 1 - Minimized
    Public Const MAXIMIZED As Short = 2 ' 2 - Maximized
    Public sWindowsDir As String 'Path of Windows Directory 'NS

    '----------------- Global Constants ----------------------------------
    ' Constant declarations for the indices to the SETTINGS arrays.
    ' Each constant name is formed from the non-optional SCPI
    '   keywords for the parameter it represents.

    Public Const FORM_BORD As Short = 1 ' SCPI: ":FORMat:BORDer"
    Public Const OUTP As Short = 2 ' SCPI: ":OUTPut[:STATe]
    Public Const OUTP_FILT_FREQ As Short = 3
    Public Const OUTP_FILT As Short = 4
    Public Const OUTP_ECLT As Short = 5
    Public Const OUTP_TRIG_SOUR As Short = 6
    Public Const OUTP_TTLT As Short = 7
    Public Const OUTP_SYNC_SOUR As Short = 8
    Public Const OUTP_SYNC As Short = 9
    Public Const OUTP_SYNC_POS As Short = 10
    Public Const FREQ As Short = 11
    Public Const FREQ_RAST_SOUR As Short = 12
    Public Const VOLT As Short = 13
    Public Const VOLT_OFFS As Short = 14
    Public Const FUNC_MODE As Short = 15
    Public Const FUNC_SHAPE As Short = 16
    Public Const SIN_PHAS As Short = 17
    Public Const SIN_POW As Short = 18
    Public Const TRI_PHAS As Short = 19
    Public Const TRI_POW As Short = 20
    Public Const SQU_DCYC As Short = 21
    Public Const PULS_DEL As Short = 22
    Public Const PULS_WIDT As Short = 23
    Public Const PULS_TRAN As Short = 24
    Public Const PULS_TRAN_TRA As Short = 25
    Public Const RAMP_DEL As Short = 26
    Public Const RAMP_TRAN As Short = 27
    Public Const RAMP_TRAN_TRA As Short = 28
    Public Const SINC_NCYC As Short = 29
    Public Const GAUS_EXP As Short = 30
    Public Const EXP_EXP As Short = 31
    Public Const DC As Short = 32
    Public Const AM As Short = 33
    Public Const AM_INT_FREQ As Short = 34
    Public Const PHAS_LOCK As Short = 35
    Public Const PHAS As Short = 36
    Public Const PHAS_SOUR As Short = 37
    Public Const SEQ_ADV As Short = 38
    Public Const SEQ_DEF As Short = 39
    Public Const SEQ_DEL As Short = 40
    Public Const TRAC_DEF As Short = 41
    Public Const TRAC_DEL As Short = 42
    Public Const TRAC_SEL As Short = 43
    Public Const INIT_CONT As Short = 44
    Public Const TRIG_BURS As Short = 45
    Public Const TRIG_COUN As Short = 46
    Public Const TRIG_DEL As Short = 47
    Public Const TRIG_LEV As Short = 48
    Public Const TRIG_SOUR_ADV As Short = 49
    Public Const TRIG_GATE As Short = 50
    Public Const TRIG_SLOP As Short = 51
    Public Const TRIG_TIM As Short = 52
    Public Const SMEM_MODE As Short = 53
    Public Const SMEM As Short = 54

    Public Const MAX_SETTINGS As Short = 54 ' The number of parameter settings

    '-----------------VISA Global Constants------------------------------------
    Public Const VI_ERROR_SYSTEM_ERROR As Integer = &HBFFF0000
    Public Const VI_ERROR_TMO As Integer = &HBFFF0015

    '-----------------Global Variables------------------------------------
    Public SetCur(MAX_SETTINGS) As String ' "Current Settings" Array
    Public SetDef(MAX_SETTINGS) As String ' "Default Settings" Array
    Public SetMin(MAX_SETTINGS) As String ' "Maximum Settings" Array
    Public SetMax(MAX_SETTINGS) As String ' "Minimum Settings" Array
    Public SetUOM(MAX_SETTINGS) As String ' "Unit Of Measure" Array
    Public SetRes(MAX_SETTINGS) As String ' "Setting Resolution" Array
    Public SetMinInc(MAX_SETTINGS) As String ' "Setting Minimum Increment" Array
    Public SetCod(MAX_SETTINGS) As String ' "Setting SCPI Code" Array

    Public WaveIdx As Short ' Index of the selected wave button in the tool bar
    Public ModeTrigSour(9) As String
    Public ModeTrigSourIdx As Short

    Public bWriteMsgCheck As Boolean 'set equal to the value of WriteInstrument function
    Public SampleClockTime As Single ' Stores current Sample Clock period
    Public TrigDelayInClocks As Integer ' Stores TRIGger:DELay value in clocks, not seconds

    'Options Tab Index Constants
    Public Const TAB_FUNCTIONS As Short = 0
    Public Const TAB_TRIGGERS As Short = 1
    Public Const TAB_MARKERS As Short = 2
    Public Const TAB_OPTIONS As Short = 3

    '1/16/03 NS
    Public bDesignMode As Boolean ' True if in Design mode
    Public bPersistMode As Boolean ' True if in Persist mode
    Public bRunMode As Boolean ' True if in Run mode
    Public bHi As Boolean ' True if Hi Impedance
    Public bInstrumentMode As Boolean ' True if in GetCurrentInstrument Mode


    Public Const INSTRUMENT_NAME As String = "Function Generator"
    Public Const MANUF As String = "Racal Instruments, Inc."
    Public Const MODEL_CODE_TETS As String = "3152"
    Public Const MODEL_CODE_VIPER As String = "3152A"

    Public Const RESOURCE_NAME As String = "VXI::19"

    Public ResourceName As String

    Public IsOn As Boolean

    '***Read Instrument Mode***
    Public iStatus As Short
    Public gctlParse As New VIPERT_Common_Controls.Common
    '###############################################################


    Function ReadFG() As String
        ReadFG = ""
        'DESCRIPTION:
        '   This Module issues a command via the HPE1412A.DLL to poll
        '   the instrument for the measured value.
        'EXAMPLE:
        '   Measurement$ = ReadFG

        Dim CharsRead As Integer
        Dim ReadString As String = ""
        Dim Status As Integer

        If  Not LiveMode Then Exit Function
        If DoNotTalk Then Exit Function
        'If LiveMode% Then
        do
            ' ErrorStatus& = viRead(instrumentHandle&, ReadBuffer$, 255&, CharsRead&)
            ReadBuffer = Space(255)
            Status = atxml_ReadCmds(ResourceName, ReadBuffer, 255, CharsRead)
            ReadString &= Strings.Left(ReadBuffer, CharsRead)
        Loop While CharsRead=255

        DisplayErrorMessage(ErrorStatus)
        ' End If

        '   ##########################################
        '   Action Required: Remove debug code
        '   This will enable simulation of commands
        '    If gcolCmds.Count <> 0 Then
        '        ReadBuffer$ = gctlDebug.Simulate(gcolCmds)''
        '
        '       Clear collection
        '        Set gcolCmds = New Collection
        '    End If
        '   ##########################################


        'Return Function Value
        ReadFG = Strings.Left(ReadBuffer, CharsRead)
        ReadFG = Trim(ReadFG)
    End Function

    Sub WriteInstrument(ByVal Command As String)
'        Dim Count As Integer
        Dim ErrorCode As Integer
        bWriteMsgCheck = True
        Dim Status As Integer
        Dim sInstrumentStatus As String
        Dim viclearStatus As Double

        If DoNotTalk Or Command = "" Then Exit Sub

        If  Not LiveMode Then
            HelpPanel("Instrument Command: " & Quote & Command & Quote)
            Application.DoEvents()
            Exit Sub
        End If
        If bInstrumentMode And InStr(Command, "?") = 0 Then Exit Sub

        'Check Mode
        If frmRac3152.Panel_Conifg.DebugMode = True And Strings.Right(Command, 1) <> "?" Then Exit Sub

        '   ErrorStatus& = viWrite(instrumentHandle&, Command$, CLng(Len(Command$)), Count)
        Status = atxml_WriteCmds(ResourceName, Command, Command.Length)

        If ErrorStatus Then
            DisplayErrorMessage(ErrorStatus)
            viclearStatus = atxmlDF_viClear(ResourceName, 255)
        Else
            If InStr(Command, "?") = 0 Then ' If NOT expecting a response
                Status = atxml_WriteCmds(ResourceName, "SYST:ERR?", CLng(Len("SYST:ERR?")))
                sInstrumentStatus = ReadFG()

                If Val(sInstrumentStatus) <> 0 Then
                    If bDesignMode Then
                        MsgBox("Unexpected Command <" & Command & "> sent to the instrument." & vbCrLf & vbCrLf & "Instrument will be set to RESET state.", MsgBoxStyle.Exclamation, "Function Generator")
                    End If

                    If bRunMode Or bPersistMode Then
                        MsgBox("Unexpected Command <" & Command & "> sent to the instrument." & vbCrLf & vbCrLf & "SAIS session will be terminated.", MsgBoxStyle.Exclamation, "Function Generator")

                        SetKey("TIPS", "STATUS", "Error from FG: " & ErrorCode & " " & ReadBuffer)
                        frmRac3152.cmdQuit_Click()
                    End If

                    If bDesignMode Then
                        frmRac3152.cmdReset_Click() 'reset instrument and Front Panel
                        bWriteMsgCheck = False
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub AdjustVoltOffs()
        ' Conditions:
        '   1. All VOLT and VOLT_OFFS parameters will be doubled or
        '       halved when going to or from hi-Z output impedance
        '       for this front panel.
        '   2. The FG always assumes 50 Ohm output impedance.
        '   3. When sending commands, the 50 Ohm output option is
        '       tested.  If true, VOLT and VOLT_OFFS are sent as is.
        '       If false, half values will be sent.

        Dim Window As Single, CurrentAmpl As Single

        If frmRac3152.optLoadImp50.Checked = True Then
            CurrentAmpl = Val(SetCur(VOLT))
        Else
            CurrentAmpl = Val(SetCur(VOLT)) / 2
        End If
        Select Case CurrentAmpl
            Case Is>1.6:
                Window = 8
            Case Is>0.16:
                Window = 0.8

            Case Else
                Window = 0.0799
        End Select
        SetMax(VOLT_OFFS) = (Window - (CurrentAmpl / 2)).ToString("0.###")
        If Val(SetMax(VOLT_OFFS))<0 Then
            SetMax(VOLT_OFFS) = "0"
        End If
        SetMin(VOLT_OFFS) = -Val(SetMax(VOLT_OFFS))

        If frmRac3152.optLoadImpHigh.Checked = True Then
            SetMax(VOLT_OFFS) = 2 * Val(SetMax(VOLT_OFFS))
            SetMin(VOLT_OFFS) = 2 * Val(SetMin(VOLT_OFFS))
        End If

        If Val(SetCur(VOLT_OFFS))>Val(SetMax(VOLT_OFFS)) Then
            SetCur(VOLT_OFFS) = SetMax(VOLT_OFFS)
            frmRac3152.txtDcOffs.Text = EngNotate(Val(SetCur(VOLT_OFFS)), VOLT_OFFS)
            SendCommand(VOLT_OFFS)
        ElseIf Val(SetCur(VOLT_OFFS))<Val(SetMin(VOLT_OFFS)) Then
            SetCur(VOLT_OFFS) = SetMin(VOLT_OFFS)
            frmRac3152.txtDcOffs.Text = EngNotate(Val(SetCur(VOLT_OFFS)), VOLT_OFFS)
            SendCommand(VOLT_OFFS)
        End If
    End Sub

    Sub SendCommand(ByVal SetIdx As Short)
        '   Sends the current setting for a parameter to "WriteInstrument".
        Dim Value As Single

        ' This CASE statement is used for exception processing.
        Select Case SetIdx
            Case VOLT, VOLT_OFFS:
                ' These parameters are maintained at levels based on
                '   the output impedance setting in this program (50 Ohm or Hi-Z).
                ' The instrument ALWAYS assumes the output is connected to
                '   a 50 Ohm load.
                ' Therefore, it is halved on output if the user sets "Hi-Z".
                If frmRac3152.optLoadImpHigh.Checked = True Then
                    If Not bInstrumentMode Then WriteInstrument(SetCod(SetIdx) & Val(SetCur(SetIdx)) / 2)
                    Exit Sub
                End If
            Case EXP_EXP:
                ' This parameter won't tolerate a setting of zero.
                ' If zero, bump it one more time.
                If Val(SetCur(EXP_EXP))=0 Then
                    If LastDirection="UP" Then
                        SetCur(EXP_EXP) = "0.0002"
                    Else
                        SetCur(EXP_EXP) = "-0.0002"
                    End If
                    frmRac3152.txtExpo.Text = EngNotate(SetCur(EXP_EXP), EXP_EXP)
                End If
            Case DC:
                ' This parameter is maintained in this program as VOLTS.
                ' In the instrument, it is a percentage of the VOLT setting.
                ' Since the VOLT setting cannot be negative or zero, the only
                '   way to set a negative DC level is to set DC to -100%.
                ' Output impedance is also accounted for here.
                If frmRac3152.optLoadImp50.Checked = True Then
                    Value = Val(SetCur(DC)) * 2
                Else
                    Value = Val(SetCur(DC))
                End If
                If Value = 0 Then
                    WriteInstrument(":DC 0")
                    Value = 0.01
                ElseIf Value < 0 Then
                    WriteInstrument(":DC -50")
                    Value = -Value
                Else
                    WriteInstrument(":DC 50")
                End If
                If Not bInstrumentMode Then WriteInstrument(SetCod(VOLT) & Value)
                Exit Sub
            Case TRIG_DEL:
                ' This parameter is maintained in this program as SECONDS, not a
                '   number of sample clocks as required by the instrument.
                If  Not bInstrumentMode Then WriteInstrument(SetCod(TRIG_DEL) & TrigDelayInClocks)
                Exit Sub

        End Select

        If  Not bInstrumentMode Then WriteInstrument(SetCod(SetIdx) & SetCur(SetIdx))
    End Sub

    Public Sub CheckTriggerDelay()
        ' Trigger Delay value cannot be between 0 and 10 and must be even.
        TrigDelayInClocks = Val(SetCur(TRIG_DEL))/SampleClockTime
        If TrigDelayInClocks<11 Then
            TrigDelayInClocks = 10
        ElseIf TrigDelayInClocks Mod 2 Then            'If odd
            TrigDelayInClocks -= 1
        End If
        SetCur(TRIG_DEL) = Round(TrigDelayInClocks*SampleClockTime, 9)
        frmRac3152.txtModeTrigDela.Text = EngNotate(SetCur(TRIG_DEL), TRIG_DEL)
    End Sub

    Sub DisplayErrorMessage(ByVal ErrorStatus As Integer)
        Dim viclearStatus As Double

        If ErrorStatus Then
            If ErrorStatus = VI_ERROR_SYSTEM_ERROR Or ErrorStatus > VI_ERROR_SYSTEM_ERROR Then
                viclearStatus = atxmlDF_viClear(ResourceName, 255)

                If ErrorStatus = VI_ERROR_TMO Then
                    MsgBox("VISA TimeOut Error <" & Microsoft.VisualBasic.Command() & "> sent to the instrument." & vbCrLf & vbCrLf)
                Else
                    MsgBox("VISA Error <" & Microsoft.VisualBasic.Command() & "> sent to the instrument." & vbCrLf & vbCrLf)
                End If
            Else
                MsgBox("Error <" & Microsoft.VisualBasic.Command() & "> sent to the instrument." & vbCrLf & vbCrLf)
            End If
            '        Error = ri3151_error_message(instrumentHandle&, ErrorStatus&, ReadBuffer$)
            '        MsgBox ReadBuffer$, 48, "VISA Error Message"
            '
            '        If bRunMode Or bPersistMode Then
            '            SetKey "TIPS", "STATUS", "Error from FG: " & " " & ReadBuffer$ & "VISA Error Message"
            '            frmRac3152.cmdQuit_Click
            '        End If
        End If
    End Sub

    Public Sub ExecCmd(ByVal cmdline As String)
'        Dim Proc As PROCESS_INFORMATION
'        Dim Start As STARTUPINFO = New STARTUPINFO()
'        Dim Ret As Integer

'        'Initialize the STARTUPINFO structure
'        Start.cb = Len(Start)

'        'Start the shelled application
'        Ret = CreateProcessA(0, cmdline, 0, 0, 1, NORMAL_PRIORITY_CLASS, 0, 0, Start, Proc)

'        'Wait for the shelled application to finish
'        If Ret=1 Then 'If successful
'            Ret = WaitForSingleObject(Proc.hProcess, INFINITE)
'            Ret = CloseHandle(Proc.hProcess)
'        End If
        Dim p As Process = New Process()

        p.StartInfo.FileName = cmdline
        p.Start()
        p.WaitForExit()
    End Sub

    Public Function GetMaxTime(ByVal SetIdx As Short) As Single
        'Returns Max timing parameter in percent
        Dim Total As Single

        Select Case frmRac3152.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse":
                Total = Val(SetCur(PULS_DEL))+Val(SetCur(PULS_WIDT))+Val(SetCur(PULS_TRAN))+Val(SetCur(PULS_TRAN_TRA))
            Case "Ramp":
                Total = Val(SetCur(RAMP_DEL))+Val(SetCur(RAMP_TRAN))+Val(SetCur(RAMP_TRAN_TRA))
        End Select
        Total -= Val(SetCur(SetIdx)) 'Subtract the one we're checking for.
        GetMaxTime = 99.9-Total
    End Function

    Public Sub SetMaxTrigDelay()
        ' This procedure is called when the FREQUENCY is changed.  It performs three functions:
        '   1. Converts the maximum trigger delay to time.  The instrument keeps
        '       it as a number of sample clocks, with a maximum of 2,000,000.
        '   2. Assigns the current sample clock period to global SampleClockTime based on
        '       the new frequency setting.  This variable is used by other routines.
        '   Refer to Racal Instruments 3152 User Manual #980793, September 1996 page 3-14
        '       for an explanation of what follows.  Sample clock time is based on points per
        '       waveform which is based on waveform frequency.
        '   3. Handles a special condition: Square wave duty cycle must be 50% above 10MHz.

        Dim CurFreq As Single

        CurFreq = Val(SetCur(FREQ))
        SampleClockTime = 1/100000000.0

        Select Case frmRac3152.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine", "Square", "Triangle", "Sinc":
                If CurFreq<200000.0 Then
                    SampleClockTime = 1/(500*CurFreq)
                End If
                If frmRac3152.tolFunc.Buttons(WaveIdx - 1).Name="Square" Then
                    If CurFreq>10000000.0 Then
                        frmRac3152.fraDuty.Visible = False
                        frmRac3152.txtDuty.Text = "50 %"
                        SetCur(SQU_DCYC) = "50"
                    Else
                        frmRac3152.fraDuty.Visible = True
                    End If
                End If
            Case "Ramp", "Pulse", "Gaussian", "Exponential":
                If CurFreq<100000.0 Then
                    SampleClockTime = 1/(1000*CurFreq)
                End If
        End Select

        SetMax(TRIG_DEL) = 2000000!*SampleClockTime

        If Val(SetMax(TRIG_DEL))<Val(SetCur(TRIG_DEL)) Then
            SetCur(TRIG_DEL) = SetMax(TRIG_DEL)
        End If
    End Sub

    Public Sub Main()
        Dim SystemDirectory As String
        Dim Status As Integer
        Dim XmlBuf As String
        Dim Allocation As String
        Dim Response As String

        ' initialize resource name
        ResourceName = "FUNC_GEN_1"

        XmlBuf = Space(4096)
        Response = Space(4096)

        bHi = False 'sets High impedance to false
        SetMode()

        If (bDesignMode = False) And (bPersistMode = False) And (bRunMode = False) Then
            SplashStart()
        Else
            frmRac3152.Left = (PrimaryScreen.Bounds.Width - frmRac3152.Width)
            frmRac3152.Top = 0
        End If

        If bDesignMode Then
            frmRac3152.cmdUpdateTIP.Visible = True
        Else
            frmRac3152.cmdUpdateTIP.Visible = False
        End If

        Quote = Convert.ToString(Chr(34))

        sWindowsDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

        'Check System and Instrument For Errors
        SystemDirectory = Environment.SystemDirectory
        '        If FileExists(SystemDirectory$ & "VISA32.DLL") Then
        'Determine if the instrument is functioning
        'ErrorStatus& = ri3151_init(resourceName:=RESOURCE_NAME$, IDQuery:=1, resetDevice:=1, instrumentHandle:=instrumentHandle&)
        Status = atxml_Initialize(proctype, guid)


        XmlBuf = "<AtXmlTestRequirements>" & _
                 "    <ResourceRequirement>" & _
                 "        <ResourceType>Source</ResourceType>" & _
                 "        <SignalResourceName>FUNC_GEN_1</SignalResourceName> " & _
                 "    </ResourceRequirement> " & _
                 "</AtXmlTestRequirements>"

        Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

        Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

        'Parse Availability XML string to get the status(Mode) of the instrument
        iStatus = gctlParse.ParseAvailiability(Response)

        If Status Then
            DisplayErrorMessage(ErrorStatus)
            MsgBox("The Function Generator is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
            LiveMode = False
        Else
            LiveMode = True
        End If

        bInstrumentMode = True
        Initialize()
        SplashEnd()
        frmRac3152.tabOptions.SelectedIndex = TAB_FUNCTIONS
        frmRac3152.Refresh()
        frmRac3152.tolFunc.Buttons("Sine").Pushed = True 'Depress Sine button

        If (bDesignMode = True) Or (bPersistMode = True) Or (bRunMode = True) Then
            InitializeForMode()
        End If

        If bRunMode Then 'minimize
            frmRac3152.WindowState = FormWindowState.Minimized
        End If

        'Set the Refresh rate when in "Debug" mode from 1000 = 1 sec to over 24000 = 24 sec
        frmRac3152.Panel_Conifg.Refresh = 5000

        '   If instrument is "Live" then get current configuration
        frmRac3152.ConfigGetCurrent()
        bInstrumentMode = False
        Application.DoEvents()

        'DO NOT Put a break point After this point. Causes error if timer is running
        'Set the Mode the Instrument is in: Local = True, Debug = False
        If iStatus <> 1 Then
            frmRac3152.Panel_Conifg.SetDebugStatus(True)
        Else
            frmRac3152.Panel_Conifg.SetDebugStatus(False)
        End If

        'Set the "On" state
        IsOn = False
    End Sub

    Sub Initialize()
        'DoNotTalk% = True
        With frmRac3152
            .cboModeTrigSour.Items.Clear()
            .cboModeTrigSour.Items.Add("External")
            .cboModeTrigSour.Items.Add("Internal")
            .cboModeTrigSour.Items.Add("TTLTrg0")
            .cboModeTrigSour.Items.Add("TTLTrg1")
            .cboModeTrigSour.Items.Add("TTLTrg2")
            .cboModeTrigSour.Items.Add("TTLTrg3")
            .cboModeTrigSour.Items.Add("TTLTrg4")
            .cboModeTrigSour.Items.Add("TTLTrg5")
            .cboModeTrigSour.Items.Add("TTLTrg6")
            .cboModeTrigSour.Items.Add("TTLTrg7")

            .cboModeTrigSlop.Items.Clear()
            .cboModeTrigSlop.Items.Add("Positive")
            .cboModeTrigSlop.Items.Add("Negative")

            .cboTrigOutECL.Items.Clear()
            .cboTrigOutECL.Items.Add("ECLTrg0")
            .cboTrigOutECL.Items.Add("ECLTrg1")

            .cboTrigOutTTL.Items.Clear()
            .cboTrigOutTTL.Items.Add("TTLTrg0")
            .cboTrigOutTTL.Items.Add("TTLTrg1")
            .cboTrigOutTTL.Items.Add("TTLTrg2")
            .cboTrigOutTTL.Items.Add("TTLTrg3")
            .cboTrigOutTTL.Items.Add("TTLTrg4")
            .cboTrigOutTTL.Items.Add("TTLTrg5")
            .cboTrigOutTTL.Items.Add("TTLTrg6")
            .cboTrigOutTTL.Items.Add("TTLTrg7")
        End With

        SetCod(FORM_BORD) = ":FORM:BORD "
        SetDef(FORM_BORD) = "NORM"

        SetCod(OUTP) = ":OUTP "
        SetDef(OUTP) = "OFF"

        SetCod(OUTP_FILT_FREQ) = ":OUTP:FILT:FREQ "
        SetDef(OUTP_FILT_FREQ) = "20MHz"

        SetCod(OUTP_FILT) = ":OUTP:FILT "
        SetDef(OUTP_FILT) = "OFF"

        SetCod(OUTP_ECLT) = ":OUTP:ECLTrg0 " 'Changed at run time
        SetDef(OUTP_ECLT) = "OFF"

        SetCod(OUTP_TRIG_SOUR) = ":OUTP:TRIG:SOUR "
        SetDef(OUTP_TRIG_SOUR) = "BIT"

        SetCod(OUTP_TTLT) = ":OUTP:TTLTrg0 " 'Changed at run time
        SetDef(OUTP_TTLT) = "OFF"

        SetCod(OUTP_SYNC_SOUR) = ":OUTP:SYNC:SOUR "
        SetDef(OUTP_SYNC_SOUR) = "BIT"

        SetCod(OUTP_SYNC) = ":OUTP:SYNC "
        SetDef(OUTP_SYNC) = "OFF"

        SetCod(OUTP_SYNC_POS) = ":OUTP:SYNC:POS "
        SetDef(OUTP_SYNC_POS) = "261138"
        SetMin(OUTP_SYNC_POS) = "2"
        SetMax(OUTP_SYNC_POS) = "261144"
        SetUOM(OUTP_SYNC_POS) = ""
        SetRes(OUTP_SYNC_POS) = "N0"

        SetCod(FREQ) = ":FREQ "
        SetDef(FREQ) = "1000000"
        SetMin(FREQ) = "0.0001"
        SetMax(FREQ) = "50000000"
        SetRes(FREQ) = "D7"
        SetUOM(FREQ) = "Hz"

        SetCod(FREQ_RAST_SOUR) = ":FREQ:RAST:SOUR "
        SetDef(FREQ_RAST_SOUR) = "INT"

        SetCod(VOLT) = ":VOLT "
        SetDef(VOLT) = "5"
        SetMin(VOLT) = "0.01"
        SetMax(VOLT) = "16"
        SetUOM(VOLT) = "Vp-p"
        SetRes(VOLT) = "D4"

        SetCod(VOLT_OFFS) = ":VOLT:OFFS "
        SetDef(VOLT_OFFS) = "0"
        SetMin(VOLT_OFFS) = "-7.19"
        SetMax(VOLT_OFFS) = "7.19"
        SetUOM(VOLT_OFFS) = "V"
        SetRes(VOLT_OFFS) = "D4"
        SetMinInc(VOLT_OFFS) = "0.01"

        SetCod(FUNC_MODE) = ":FUNC:MODE "
        SetDef(FUNC_MODE) = "FIX"

        SetCod(FUNC_SHAPE) = ":FUNC:SHAPE "
        SetDef(FUNC_SHAPE) = "SIN"

        SetCod(SIN_PHAS) = ":SIN:PHAS "
        SetDef(SIN_PHAS) = "0"
        SetMin(SIN_PHAS) = "0"
        SetMax(SIN_PHAS) = "360"
        SetUOM(SIN_PHAS) = "Degrees"
        SetRes(SIN_PHAS) = "D0"

        SetCod(SIN_POW) = ":SIN:POW "
        SetDef(SIN_POW) = "1"
        SetMin(SIN_POW) = "1"
        SetMax(SIN_POW) = "9"
        SetUOM(SIN_POW) = ""
        SetRes(SIN_POW) = "D0"

        SetCod(TRI_PHAS) = ":TRI:PHAS "
        SetDef(TRI_PHAS) = "0"
        SetMin(TRI_PHAS) = "0"
        SetMax(TRI_PHAS) = "360"
        SetUOM(TRI_PHAS) = "Degrees"
        SetRes(TRI_PHAS) = "D0"

        SetCod(TRI_POW) = ":TRI:POW "
        SetDef(TRI_POW) = "1"
        SetMin(TRI_POW) = "1"
        SetMax(TRI_POW) = "9"
        SetUOM(TRI_POW) = ""
        SetRes(TRI_POW) = "D0"

        SetCod(SQU_DCYC) = ":SQU:DCYC "
        SetDef(SQU_DCYC) = "50"
        SetMin(SQU_DCYC) = "1"
        SetMax(SQU_DCYC) = "99"
        SetUOM(SQU_DCYC) = "%"
        SetRes(SQU_DCYC) = "A0"

        SetCod(PULS_DEL) = ":PULS:DEL "
        SetDef(PULS_DEL) = "10"
        SetMin(PULS_DEL) = "0"
        SetMax(PULS_DEL) = "99.9"
        SetUOM(PULS_DEL) = "%"
        SetRes(PULS_DEL) = "N1"
        SetMinInc(PULS_DEL) = "0.1"

        SetCod(PULS_WIDT) = ":PULS:WIDT "
        SetDef(PULS_WIDT) = "10"
        SetMin(PULS_WIDT) = "0"
        SetMax(PULS_WIDT) = "99.9"
        SetUOM(PULS_WIDT) = "%"
        SetRes(PULS_WIDT) = "N1"
        SetMinInc(PULS_WIDT) = "0.1"

        SetCod(PULS_TRAN) = ":PULS:TRAN "
        SetDef(PULS_TRAN) = "10"
        SetMin(PULS_TRAN) = "0"
        SetMax(PULS_TRAN) = "99.9"
        SetUOM(PULS_TRAN) = "%"
        SetRes(PULS_TRAN) = "N1"
        SetMinInc(PULS_TRAN) = "0.1"

        SetCod(PULS_TRAN_TRA) = ":PULS:TRAN:TRA "
        SetDef(PULS_TRAN_TRA) = "10"
        SetMin(PULS_TRAN_TRA) = "0"
        SetMax(PULS_TRAN_TRA) = "99.9"
        SetUOM(PULS_TRAN_TRA) = "%"
        SetRes(PULS_TRAN_TRA) = "N1"
        SetMinInc(PULS_TRAN_TRA) = "0.1"

        SetCod(RAMP_DEL) = ":RAMP:DEL "
        SetDef(RAMP_DEL) = "10"
        SetMin(RAMP_DEL) = "0"
        SetMax(RAMP_DEL) = "99.9"
        SetUOM(RAMP_DEL) = "%"
        SetRes(RAMP_DEL) = "N1"
        SetMinInc(RAMP_DEL) = "0.1"

        SetCod(RAMP_TRAN) = ":RAMP:TRAN "
        SetDef(RAMP_TRAN) = "10"
        SetMin(RAMP_TRAN) = "0"
        SetMax(RAMP_TRAN) = "99.9"
        SetUOM(RAMP_TRAN) = "%"
        SetRes(RAMP_TRAN) = "N1"
        SetMinInc(RAMP_TRAN) = "0.1"

        SetCod(RAMP_TRAN_TRA) = ":RAMP:TRAN:TRA "
        SetDef(RAMP_TRAN_TRA) = "10"
        SetMin(RAMP_TRAN_TRA) = "0"
        SetMax(RAMP_TRAN_TRA) = "99.9"
        SetUOM(RAMP_TRAN_TRA) = "%"
        SetRes(RAMP_TRAN_TRA) = "N1"
        SetMinInc(RAMP_TRAN_TRA) = "0.1"

        SetCod(SINC_NCYC) = ":SINC:NCYC "
        SetDef(SINC_NCYC) = "10"
        SetMin(SINC_NCYC) = "4"
        SetMax(SINC_NCYC) = "100"
        SetUOM(SINC_NCYC) = "x"
        SetRes(SINC_NCYC) = "D0"

        SetCod(GAUS_EXP) = ":GAUS:EXP "
        SetDef(GAUS_EXP) = "10"
        SetMin(GAUS_EXP) = "1"
        SetMax(GAUS_EXP) = "200"
        SetUOM(GAUS_EXP) = ""
        SetRes(GAUS_EXP) = "D0"

        SetCod(EXP_EXP) = ":EXP:EXP "
        SetDef(EXP_EXP) = "-10"
        SetMin(EXP_EXP) = "-200"
        SetMax(EXP_EXP) = "200"
        SetUOM(EXP_EXP) = ""
        SetRes(EXP_EXP) = "D0"
        SetMinInc(EXP_EXP) = "0.0002"

        SetCod(DC) = ":DC "
        SetDef(DC) = "2.5"
        SetMin(DC) = "-8"
        SetMax(DC) = "8"
        SetUOM(DC) = "Vdc"
        SetRes(DC) = "A2"
        SetMinInc(DC) = "0.01"

        SetCod(AM) = ":AM "
        SetDef(AM) = "50"
        SetMin(AM) = "1"
        SetMax(AM) = "200"
        SetUOM(AM) = "% Mod"
        SetRes(AM) = "N0"

        SetCod(AM_INT_FREQ) = ":AM:INT:FREQ "
        SetDef(AM_INT_FREQ) = "100"
        SetMin(AM_INT_FREQ) = "10"
        SetMax(AM_INT_FREQ) = "500"
        SetUOM(AM_INT_FREQ) = "Hz"
        SetRes(AM_INT_FREQ) = "D0"

        SetCod(PHAS_LOCK) = ":PHAS:LOCK "
        SetDef(PHAS_LOCK) = "OFF"

        SetCod(PHAS) = ":PHAS "
        SetDef(PHAS) = "0"
        SetMin(PHAS) = "0"
        SetMax(PHAS) = "360"
        SetUOM(PHAS) = "Deg"
        SetRes(PHAS) = "D0"

        SetCod(PHAS_SOUR) = ":PHAS:SOUR "
        SetDef(PHAS_SOUR) = "MAST"

        SetCod(SEQ_ADV) = ":SEQ:ADV "
        SetDef(SEQ_ADV) = "AUTO"

        SetCod(SEQ_DEF) = ":SEQ:DEF "
        SetDef(SEQ_DEF) = "1,1,1"
        SetMin(SEQ_DEF) = "1,1,1"
        SetMax(SEQ_DEF) = "4096,4096,1000000"
        SetUOM(SEQ_DEF) = ""
        SetRes(SEQ_DEF) = "D0"

        SetCod(SEQ_DEL) = ":SEQ:DEL "
        SetDef(SEQ_DEL) = "1"
        SetMin(SEQ_DEL) = "1"
        SetMax(SEQ_DEL) = "4096"
        SetUOM(SEQ_DEL) = ""
        SetRes(SEQ_DEL) = "D0"

        SetCod(TRAC_DEF) = ":TRAC:DEF "
        SetDef(TRAC_DEF) = "1,10"
        SetMin(TRAC_DEF) = "1,10"
        SetMax(TRAC_DEF) = "4096,261144"
        SetUOM(TRAC_DEF) = ""
        SetRes(TRAC_DEF) = "D0"

        SetCod(TRAC_DEL) = ":TRAC:DEL "
        SetDef(TRAC_DEL) = "1"
        SetMin(TRAC_DEL) = "1"
        SetMax(TRAC_DEL) = "4096"
        SetUOM(TRAC_DEL) = ""
        SetRes(TRAC_DEL) = "D0"

        SetCod(TRAC_SEL) = ":TRAC:SEL "
        SetDef(TRAC_SEL) = "1"
        SetMin(TRAC_SEL) = "1"
        SetMax(TRAC_SEL) = "4096"
        SetUOM(TRAC_SEL) = ""
        SetRes(TRAC_SEL) = "D0"

        SetCod(INIT_CONT) = ":INIT:CONT "
        SetDef(INIT_CONT) = "ON"

        SetCod(TRIG_BURS) = ":TRIG:BURS "
        SetDef(TRIG_BURS) = "OFF"

        SetCod(TRIG_COUN) = ":TRIG:COUN "
        SetDef(TRIG_COUN) = "1"
        SetMin(TRIG_COUN) = "1"
        SetMax(TRIG_COUN) = "1000000"
        SetUOM(TRIG_COUN) = "Cycles"
        SetRes(TRIG_COUN) = "N0"

        SetCod(TRIG_DEL) = ":TRIG:DEL "
        SetDef(TRIG_DEL) = "0.0000001"
        SetMin(TRIG_DEL) = "0.0000001"
        SetMax(TRIG_DEL) = "0.02"
        SetUOM(TRIG_DEL) = "Sec"
        SetRes(TRIG_DEL) = "D6"

        SetCod(TRIG_LEV) = ":TRIG:LEV "
        SetDef(TRIG_LEV) = "1.6"
        SetMin(TRIG_LEV) = "-10"
        SetMax(TRIG_LEV) = "10"
        SetUOM(TRIG_LEV) = "V"
        SetRes(TRIG_LEV) = "A2"
        SetMinInc(TRIG_LEV) = "0.01"

        SetCod(TRIG_SOUR_ADV) = ":TRIG:SOUR:ADV "
        SetDef(TRIG_SOUR_ADV) = "External"

        SetCod(TRIG_GATE) = ":TRIG:GATE "
        SetDef(TRIG_GATE) = "OFF"

        SetCod(TRIG_SLOP) = ":TRIG:SLOP "
        SetDef(TRIG_SLOP) = "Positive"

        SetCod(TRIG_TIM) = ":TRIG:TIM "
        SetDef(TRIG_TIM) = "0.0001"
        SetMin(TRIG_TIM) = "0.000015"
        SetMax(TRIG_TIM) = "1000"
        SetUOM(TRIG_TIM) = "Sec"
        SetRes(TRIG_TIM) = "D4"

        SetCod(SMEM_MODE) = ":SMEM:MODE "
        SetDef(SMEM_MODE) = "READ"

        SetCod(SMEM) = ":SMEM "
        SetDef(SMEM) = "OFF"

        SetControlsToReset()
        'DoNotTalk% = False
    End Sub

    Sub SetControlsToReset()
        Dim Setting As Short

        WaveIdx = 1

        'Change all Current Settings to their Defaults
        frmRac3152.optLoadImp50.Checked = True
        For Setting = 1 To MAX_SETTINGS
            SetCur(Setting) = SetDef(Setting)
        Next Setting

        ' Handle conditional default settings
        AdjustVoltOffs() ' Assigns Max and Min amplitude
        SetMax(PULS_DEL) = GetMaxTime(PULS_DEL)
        SetMax(PULS_WIDT) = GetMaxTime(PULS_WIDT)
        SetMax(PULS_TRAN) = GetMaxTime(PULS_TRAN)
        SetMax(PULS_TRAN_TRA) = GetMaxTime(PULS_TRAN_TRA)
        SetMax(RAMP_DEL) = GetMaxTime(RAMP_DEL)
        SetMax(RAMP_TRAN) = GetMaxTime(RAMP_TRAN)
        SetMax(RAMP_TRAN_TRA) = GetMaxTime(RAMP_TRAN_TRA)

        SetCod(OUTP_ECLT) = ":OUTP:ECLTrg0 "
        SetCod(OUTP_TTLT) = ":OUTP:TTLTrg0 "

        ' Display default settings
        With frmRac3152
            '*** Main Form Settings
            .imgFGDisplay.Image = .pcpFGDisplay.Images(0)
            For Each Button In .tolFunc.Buttons
                Button.Pushed = False
            Next
            .tolFunc_ButtonClick(.tolFunc.Buttons("Sine")) 'Run Sine parameters
            .tolFunc.Buttons("Sine").Pushed = True 'Depress Sine button

            '*** "Functions" Tab
            .txtFreq.Text = EngNotate(Val(SetDef(FREQ)), FREQ)
            .txtAmpl.Text = EngNotate(Val(SetDef(VOLT)), VOLT)
            .txtDcOffs.Text = EngNotate(Val(SetDef(VOLT_OFFS)), VOLT_OFFS)
            .txtExpo.Text = EngNotate(Val(SetDef(SIN_POW)), SIN_POW)
            .txtPhas.Text = EngNotate(Val(SetDef(SIN_PHAS)), SIN_PHAS)
            .txtSinc.Text = EngNotate(Val(SetDef(SINC_NCYC)), SINC_NCYC)
            .txtDela.Text = EngNotate(Val(SetDef(PULS_DEL)), PULS_DEL)
            .txtWidt.Text = EngNotate(Val(SetDef(PULS_WIDT)), PULS_WIDT)
            .txtLead.Text = EngNotate(Val(SetDef(PULS_TRAN)), PULS_TRAN)
            .txtTrai.Text = EngNotate(Val(SetDef(PULS_TRAN_TRA)), PULS_TRAN_TRA)
            .txtDuty.Text = EngNotate(Val(SetDef(SQU_DCYC)), SQU_DCYC)

            '*** "Trigger Mode" Tab
            .cboModeTrigSour.SelectedIndex = 0 'Select External
            .cboModeTrigSlop.SelectedIndex = 0 'Select Positive
            .optModeCont.Checked = True 'Select Continuous Mode
            .optModeCont_Click(True) 'Execute it's code
            .fraModeTrigSlop.Text = "Slope"
            .txtModeTrigLevel.Text = EngNotate(Val(SetDef(TRIG_LEV)), TRIG_LEV)
            .txtModeTrigTime.Text = EngNotate(Val(SetDef(TRIG_TIM)), TRIG_TIM)
            .txtIntTimer.Text = .txtModeTrigTime.Text
            .txtModeTrigBurst.Text = EngNotate(Val(SetDef(TRIG_COUN)), TRIG_COUN)
            .txtModeTrigDela.Text = EngNotate(Val(SetDef(TRIG_DEL)), TRIG_DEL)

            '*** "Markers" Tab
            .chkTrigOutECLOn.Checked = False
            .cboTrigOutECL.Visible = True
            .cboTrigOutECL.SelectedIndex = 0 'Select ECLTrg0
            .chkTrigOutTTLOn.Checked = False
            .cboTrigOutTTL.Visible = True
            .cboTrigOutTTL.SelectedIndex = 0 'Select TTLTrg0
            .optTrigSourceWave_Click(True) 'Enable appropriate controls
            .optTrigSourceWave.Checked = True
            .optSyncSourceWave.Visible = True
            .optSyncSourceWave.Checked = True
            .chkSyncOutStateOn.Checked = False

            '*** "Options" Tab
            .optLoadImp50.Checked = False
            .optLoadImpHigh.Checked = True
            .chkFilterStateOn.Checked = False
            .optCutOff20.Checked = True
            .cmdRunWaveCAD.Visible = True

        End With

        If DoNotTalk Then Exit Sub

        frmRac3152.ConfigGetCurrent()
    End Sub

    Sub SetKey(ByRef sSection As String, ByVal sKey As String, ByVal sKeyVal As String)
        Dim lpFileName As String 'INI File Key Name "Key=?"

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        WritePrivateProfileString(sSection, sKey, sKeyVal, lpFileName)
    End Sub

    Function sGetKey(ByVal sSection As String, ByVal sKey As String) As String
        sGetKey = ""
        Dim lpReturnedString As String
        Dim nNumChars As Integer

        'Clear String Buffer
        lpReturnedString = Space(5000)

        nNumChars = GetPrivateProfileString(sSection, sKey, "", lpReturnedString.ToString(), Len(lpReturnedString), sWindowsDir & "\VIPERT.INI")
        sGetKey = Strings.Left(lpReturnedString.ToString(), nNumChars)
    End Function

    Function sGetCommandString() As String
        sGetCommandString = ""
        Dim i As Short
        Dim sTemp As String = ""
        Dim Value As Single

        sGetCommandString = ""
        SetMaxTrigDelay()

        If (SetCur(FUNC_SHAPE)<>"DC") Then
            TrigDelayInClocks = Val(SetCur(TRIG_DEL))/SampleClockTime
            If TrigDelayInClocks<11 Then
                TrigDelayInClocks = 10
            ElseIf TrigDelayInClocks Mod 2 Then                'If odd
                TrigDelayInClocks -= 1
            End If

            sTemp = TrigDelayInClocks
        End If

        If bHi Then
            sGetCommandString = sGetCommandString & ":HI YES;"
            SetCur(VOLT) = Val(SetCur(VOLT))/2
            SetCur(VOLT_OFFS) = Val(SetCur(VOLT_OFFS))/2
            SetCur(DC) = Val(SetCur(DC))/2
        End If
        If (SetCur(FUNC_SHAPE)<>SetDef(FUNC_SHAPE)) Then
            sGetCommandString = sGetCommandString & SetCod(FUNC_SHAPE) & SetCur(FUNC_SHAPE) & ";"
        End If

        If (SetCur(FREQ)<>SetDef(FREQ)) Then
            sGetCommandString = sGetCommandString & SetCod(FREQ) & SetCur(FREQ) & ";"
        End If

        ' if VOLT was changed
        If (SetCur(VOLT)<>SetDef(VOLT) Or bHi) And (SetCur(FUNC_SHAPE)<>"DC") Then
            sGetCommandString = sGetCommandString & SetCod(VOLT) & SetCur(VOLT) & ";"
        End If

        For i = 1 To MAX_SETTINGS
            If (SetCur(i)<>SetDef(i)) And (i<>OUTP_TTLT) And (i<>OUTP_ECLT) Then
                Select Case i
                    Case FUNC_SHAPE:
                    Case FREQ:
                    Case VOLT:
                    Case SIN_PHAS, SIN_POW:
                        If (SetCur(FUNC_SHAPE)="SIN") Then
                            sGetCommandString = sGetCommandString & SetCod(i) & SetCur(i) & ";"
                        End If

                    Case TRI_PHAS, TRI_POW:
                        If (SetCur(FUNC_SHAPE)="TRI") Then
                            sGetCommandString = sGetCommandString & SetCod(i) & SetCur(i) & ";"
                        End If

                    Case GAUS_EXP:
                        If (SetCur(FUNC_SHAPE)="GAUS") Then
                            sGetCommandString = sGetCommandString & SetCod(i) & SetCur(i) & ";"
                        End If
                    Case EXP_EXP:
                        If (SetCur(FUNC_SHAPE)="EXP") Then
                            sGetCommandString = sGetCommandString & SetCod(i) & SetCur(i) & ";"
                        End If
                    Case RAMP_DEL, RAMP_TRAN, RAMP_TRAN_TRA:
                        If (SetCur(FUNC_SHAPE)="RAMP") Then
                            sGetCommandString = sGetCommandString & SetCod(i) & SetCur(i) & ";"
                        End If
                    Case PULS_DEL, PULS_TRAN, PULS_TRAN_TRA:
                        If (SetCur(FUNC_SHAPE)="PULS") Then
                            sGetCommandString = sGetCommandString & SetCod(i) & SetCur(i) & ";"
                        End If
                    Case DC:
                        If (SetCur(FUNC_SHAPE)="DC") Then
                            Value = Val(SetCur(DC))
                            If Value=0 Then
                                sGetCommandString = sGetCommandString & ":DC 0" & ";"
                                Value = 0.01
                            ElseIf Value<0 Then
                                sGetCommandString = sGetCommandString & ":DC -50" & ";"
                                Value = -Value
                            Else
                                sGetCommandString = sGetCommandString & ":DC 50" & ";"
                            End If
                            Value *= 2 'to match the code in SendCommand
                            sGetCommandString = sGetCommandString & SetCod(VOLT) & Value & ";"
                        End If
                    Case TRIG_DEL:
                        If SetCur(FUNC_SHAPE)<>"DC" Then
                            sGetCommandString = sGetCommandString & SetCod(i) & sTemp & ";"
                        End If

                    Case Else
                        sGetCommandString = sGetCommandString & SetCod(i) & SetCur(i) & ";"
                End Select
            End If
        Next i

        If (SetCod(OUTP_TTLT)<>":OUTP:TTLTrg0 ") Or (SetCur(OUTP_TTLT)<>SetDef(OUTP_TTLT)) Then
            sGetCommandString = sGetCommandString & SetCod(OUTP_TTLT) & SetCur(OUTP_TTLT) & ";"
        End If

        If (SetCod(OUTP_ECLT)<>":OUTP:ECLTrg0 ") Or (SetCur(OUTP_ECLT)<>SetDef(OUTP_ECLT)) Then
            sGetCommandString = sGetCommandString & SetCod(OUTP_ECLT) & SetCur(OUTP_ECLT) & ";"
        End If

        If sGetCommandString<>"" Then
            sGetCommandString = Strings.Left(sGetCommandString, Len(sGetCommandString)-1)
        End If
    End Function

    Sub SetMode()
        '1/16/03 NS
        ' Reads command line to set the operating mode of SAIS
        Dim sCmdline As String
        bDesignMode = False
        bRunMode = False
        bPersistMode = False

        sCmdline = Microsoft.VisualBasic.Command()

        If UCase(sCmdline)="TIP_DESIGN" Then
            bDesignMode = True
            Exit Sub
        End If

        If UCase(sCmdline)="TIP_RUNPERSIST" Then
            bPersistMode = True
            Exit Sub
        End If

        If UCase(sCmdline)="TIP_RUNSETUP" Then
            bRunMode = True
            Exit Sub
        End If
    End Sub

    Sub InitializeForMode()
        Dim sTIP_CMDstring As String
        Dim i As Short
        Dim sCmds() As String = {""}

        'if Design Mode, display Update Tip button.

        WriteInstrument("*RST;*CLS")

        sTIP_CMDstring = sGetKey("TIPS", "CMD")
        sTIP_CMDstring = Trim(sTIP_CMDstring)

        DoNotTalk = True
        For i = 1 To StringToList(sTIP_CMDstring, 1, sCmds, ";")
            If  Not ActivateControl(sCmds(i)) Then 'if error loading saved step
                DoNotTalk = False
                Exit Sub
            End If
        Next i
        DoNotTalk = False

        If InStr(sTIP_CMDstring, "HI") Then 'take the high impedence out

            sTIP_CMDstring = Mid(sTIP_CMDstring, InStr(sTIP_CMDstring, ";")+1)
        End If

        For i = 1 To StringToList(sTIP_CMDstring, 1, sCmds, ";")
            WriteInstrument(sCmds(i))

            If  Not bWriteMsgCheck Then
                Exit Sub
            End If
        Next i

        If (bRunMode=True) Or (bPersistMode=True) Then
            SetKey("TIPS", "STATUS", "Ready")
        End If
    End Sub

    Function ActivateControl(ByVal SCPI As String) As Boolean
        'DESCRIPTION:
        '   Sets a panel control to the state indicated by the argument of the
        '   SCPI command in the SCPI$ string argument. It simulates the action
        '   of a user selecting the control except that the actual commands are not
        '   sent to the instrument.
        'PARAMETERS:
        '   SCPI$:  A single SCPI or IEEE-488.2 command with argument(s) if any.
        Dim i As Short
        Dim sCmd As String
        Dim sArg As String = ""
        Dim sErrorMsg As String

        ActivateControl = True

        sErrorMsg = ""
        'Filter out blank sends caused by redundant semi-colon character at end of command.
        If Convert.ToString(SCPI).Length<2 Then Exit Function

        'Filter out IEEE-488.2 (non-SCPI) commands. None of these affect the GUI
        'except *RST which is irrelevant because the SAIS and instrument are reset
        'before calling this sub.
        If InStr(Convert.ToString(SCPI), "*") Then Exit Function

        'Parse the command from the argument if it has one.
        If InStr(Convert.ToString(SCPI), " ")>0 Then
            sCmd = Strings.Left(Convert.ToString(SCPI), InStr(Convert.ToString(SCPI), " ")) 'Include space to match SetCod$ array
            sArg = Mid(Convert.ToString(SCPI), InStr(Convert.ToString(SCPI), " ")+1)
        Else
            sCmd = Convert.ToString(SCPI)
        End If

        'Check for High Impedance
        If (sCmd=":HI ") Then
            frmRac3152.optLoadImpHigh.Checked = True

            SetMin(DC) = "-16"
            SetMax(DC) = "16"
            SetMin(VOLT) = "0.02"
            SetMax(VOLT) = "32"

            bHi = True
            Exit Function
        End If

        'Find match to a command index constant
        For i = 1 To MAX_SETTINGS

            If (i=OUTP_ECLT) Then
                If (sCmd=":OUTP:ECLTrg0 ") Or (sCmd=":OUTP:ECLTrg1 ") Then
                    Exit For
                End If
            End If

            If (i=OUTP_TTLT) Then
                If (sCmd=":OUTP:TTLTrg0 ") Or (sCmd=":OUTP:TTLTrg1 ") Or (sCmd=":OUTP:TTLTrg2 ") Or (sCmd=":OUTP:TTLTrg3 ") Or (sCmd=":OUTP:TTLTrg4 ") Or (sCmd=":OUTP:TTLTrg5 ") Or (sCmd=":OUTP:TTLTrg6 ") Or (sCmd=":OUTP:TTLTrg7 ") Then
                    Exit For
                End If
            End If

            If sCmd=SetCod(i) Then
                SetCur(i) = sArg 'Update Current Array
                Exit For
            End If
        Next i

        With frmRac3152
            For Each Button In .tolFunc.Buttons
                Button.Pushed = False
            Next
            Select Case i
                Case FUNC_SHAPE
                    If (sArg = "SIN") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("Sine"))
                        .tolFunc.Buttons("Sine").Pushed = True
                    ElseIf (sArg = "SQU") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("Square"))
                        .tolFunc.Buttons("Square").Pushed = True
                    ElseIf (sArg = "PULS") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("Pulse"))
                        .tolFunc.Buttons("Pulse").Pushed = True
                    ElseIf (sArg = "RAMP") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("Ramp"))
                        .tolFunc.Buttons("Ramp").Pushed = True
                    ElseIf (sArg = "TRI") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("Triangle"))
                        .tolFunc.Buttons("Triangle").Pushed = True
                    ElseIf (sArg = "EXP") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("Exponential"))
                        .tolFunc.Buttons("Exponential").Pushed = True
                    ElseIf (sArg = "GAUS") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("Gaussian"))
                        .tolFunc.Buttons("Gaussian").Pushed = True
                    ElseIf (sArg = "SINC") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("Sinc"))
                        .tolFunc.Buttons("Sinc").Pushed = True
                    ElseIf (sArg = "DC") Then
                        .tolFunc_ButtonClick(.tolFunc.Buttons("btnDC"))
                        .tolFunc.Buttons("btnDC").Pushed = True
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case OUTP
                    If (sArg = "OFF") Then
                        IsOn = False
                        .cmdOff_Click()
                    ElseIf (sArg = "ON") Then
                        IsOn = True
                        .cmdOn_Click()
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case FREQ
                    .txtFreq.Text = EngNotate(Val(sArg), FREQ)
                    .txtFreq_KeyPress(Keys.Return)
                Case VOLT
                    If bHi Then
                        sArg = Val(sArg) * 2
                    End If

                    If (SetCur(FUNC_SHAPE) = "DC") Then
                        If Val(SetCur(DC)) < 0 Then
                            sArg = "-" & sArg
                        End If
                        sArg = Val(sArg) / 2
                    End If

                    If Val(SetCur(DC)) = 0 Then
                        sArg = 0
                    End If
                    .txtAmpl.Text = EngNotate(Val(sArg), VOLT)
                    .txtAmpl_KeyPress(Keys.Return)

                Case VOLT_OFFS
                    If bHi Then
                        .txtDcOffs.Text = EngNotate(Val(sArg) * 2, VOLT_OFFS)
                    Else
                        .txtDcOffs.Text = EngNotate(Val(sArg), VOLT_OFFS)
                    End If
                    .txtDcOffs_KeyPress(Keys.Return)

                Case SIN_POW, TRI_POW, EXP_EXP, GAUS_EXP
                    .txtExpo.Text = EngNotate(Val(sArg), i)
                    .txtExpo_KeyPress(Keys.Return)
                Case SIN_PHAS, TRI_PHAS
                    .txtPhas.Text = EngNotate(Val(sArg), i)
                    .txtPhas_KeyPress(Keys.Return)

                Case SINC_NCYC
                    .txtSinc.Text = EngNotate(Val(sArg), SINC_NCYC)
                    .txtSinc_KeyPress(Keys.Return)

                Case PULS_DEL, RAMP_DEL
                    .txtDela.Text = EngNotate(Val(sArg), i)
                    .txtDela_KeyPress(Keys.Return)

                Case PULS_WIDT
                    .txtWidt.Text = EngNotate(Val(sArg), PULS_WIDT)
                    .txtWidt_KeyPress(Keys.Return)

                Case PULS_TRAN, RAMP_TRAN
                    .txtLead.Text = EngNotate(Val(sArg), i)
                    .txtLead_KeyPress(Keys.Return)

                Case PULS_TRAN_TRA, RAMP_TRAN_TRA
                    .txtTrai.Text = EngNotate(Val(sArg), i)
                    .txtTrai_KeyPress(Keys.Return)

                Case SQU_DCYC
                    .txtDuty.Text = EngNotate(Val(sArg), SQU_DCYC)
                    .txtDuty_KeyPress(Keys.Return)

                Case SINC_NCYC
                    .txtSinc.Text = EngNotate(Val(sArg), SINC_NCYC)
                    .txtSinc_KeyPress(Keys.Return)

                Case DC
                    ' Sets Amplitude for DC shape
                    .txtAmpl.Text = EngNotate(Val(sArg), DC)
                    .txtAmpl_KeyPress(Keys.Return)

                Case TRIG_LEV
                    .txtModeTrigLevel.Text = EngNotate(Val(sArg), TRIG_LEV)
                    .txtModeTrigLevel_KeyPress(Keys.Return)

                Case TRIG_TIM
                    .txtModeTrigTime.Text = EngNotate(Val(sArg), TRIG_TIM)
                    .txtIntTimer.Text = .txtModeTrigTime.Text
                    .txtModeTrigTime_KeyPress(Keys.Return)

                Case TRIG_COUN
                    .txtModeTrigBurst.Text = EngNotate(Val(sArg), TRIG_COUN)
                    .txtModeTrigBurst_KeyPress(Keys.Return)

                Case TRIG_DEL
                    SetMaxTrigDelay()
                    SetCur(TRIG_DEL) = Round(Val(sArg) * SampleClockTime, 9)
                    .txtModeTrigDela.Text = EngNotate(Val(SetCur(TRIG_DEL)), TRIG_DEL)
                    .txtModeTrigDela_KeyPress(Keys.Return)

                Case TRIG_SLOP
                    If (sArg = "Positive") Or (sArg = "POS") Then
                        .cboModeTrigSlop.Text = "Positive"
                        .cboModeTrigSlop_Click()
                    ElseIf (sArg = "POS") Or (sArg = "NEG") Then
                        .cboModeTrigSlop.Text = "Negative"
                        .cboModeTrigSlop_Click()
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case OUTP_TRIG_SOUR
                    If (sArg = "BIT") Then
                        .optTrigSourceWave.Checked = True
                        .optTrigSourceWave_Click(True)
                    ElseIf (sArg = "External") Or (sArg = "EXT") Then
                        .optTrigSourceExte.Checked = True
                        .optTrigSourceExte_Click(True)
                    ElseIf (sArg = "Internal") Or (sArg = "INT") Then
                        .optTrigSourceTimer.Checked = True
                        .optTrigSourceTimer_Click(True)
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case INIT_CONT
                    If (sArg = "ON") Then
                        .optModeCont.Checked = True 'Select Continuous Mode
                        .optModeCont_Click(True) 'Execute it's code
                    ElseIf (sArg = "OFF") Then
                        .optModeTrig.Checked = True
                        .optModeTrig_Click(True)
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case TRIG_BURS
                    If (sArg = "ON") Then
                        .optModeBurst.Checked = True
                        .optModeBurst_Click(True)
                    ElseIf (sArg <> "OFF") Then
                        sErrorMsg = "Argument"
                    End If

                Case TRIG_GATE
                    If (sArg = "ON") Then
                        .optModeGated.Checked = True
                        .optModeGated_Click(True)
                    ElseIf (sArg <> "OFF") Then
                        sErrorMsg = "Argument"
                    End If

                Case OUTP_SYNC_SOUR
                    If (sArg = "BIT") Then
                        .optSyncSourceWave_Click(True)
                        .optSyncSourceWave.Checked = True
                    ElseIf (sArg = "HCLock") Then
                        .optSyncSourceHalfSamp_Click(True)
                        .optSyncSourceHalfSamp.Checked = True
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case TRIG_SOUR_ADV
                    Select Case sArg
                        Case "BIT"
                            .optTrigSourceWave.Checked = True
                            .optTrigSourceWave_Click(True)

                        Case "EXT"
                            .optTrigSourceExte.Checked = True
                            .optTrigSourceExte_Click(True)
                            .cboModeTrigSour.Text = "External"
                            .cboModeTrigSour_Click()

                        Case "INT"
                            .optTrigSourceTimer.Checked = True
                            .optTrigSourceTimer_Click(True)
                            .cboModeTrigSour.Text = "Internal"
                            .cboModeTrigSour_Click()

                        Case "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"
                            .cboModeTrigSour.Text = sArg
                            .cboModeTrigSour_Click()

                        Case Else
                            sErrorMsg = "Argument"
                    End Select

                Case OUTP_SYNC
                    If (sArg = "ON") Then
                        .chkSyncOutStateOn.Checked = True
                        .chkSyncOutStateOn_Click(True)
                    ElseIf (sArg = "OFF") Then
                        .chkSyncOutStateOn.Checked = False
                        .chkSyncOutStateOn_Click(False)
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case OUTP_TTLT
                    If InStr(sCmd, "TTLTrg0") Then
                        .cboTrigOutTTL.Text = "TTLTrg0"
                    ElseIf InStr(sCmd, "TTLTrg1") Then
                        .cboTrigOutTTL.Text = "TTLTrg1"
                    ElseIf InStr(sCmd, "TTLTrg2") Then
                        .cboTrigOutTTL.Text = "TTLTrg2"
                    ElseIf InStr(sCmd, "TTLTrg3") Then
                        .cboTrigOutTTL.Text = "TTLTrg3"
                    ElseIf InStr(sCmd, "TTLTrg4") Then
                        .cboTrigOutTTL.Text = "TTLTrg4"
                    ElseIf InStr(sCmd, "TTLTrg5") Then
                        .cboTrigOutTTL.Text = "TTLTrg6"
                    ElseIf InStr(sCmd, "TTLTrg6") Then
                        .cboTrigOutTTL.Text = "TTLTrg6"
                    ElseIf InStr(sCmd, "TTLTrg7") Then
                        .cboTrigOutTTL.Text = "TTLTrg7"
                    Else
                        sErrorMsg = "Command"
                    End If
                    SetCod(OUTP_TTLT) = ":OUTP:" & .cboTrigOutTTL.Text & " "

                    If (sArg = "ON") Then
                        .chkTrigOutTTLOn.Checked = True
                        .chkTrigOutTTLOn_Click(True)
                    ElseIf (sArg = "OFF") Then
                        .chkTrigOutTTLOn.Checked = False
                        .chkTrigOutTTLOn_Click(False)
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case OUTP_ECLT
                    If InStr(sCmd, "ECLTrg0") Then
                        .cboTrigOutECL.Text = "ECLTrg0"
                    ElseIf InStr(sCmd, "ECLTrg1") Then
                        .cboTrigOutECL.Text = "ECLTrg1"
                    Else
                        sErrorMsg = "Command"
                    End If
                    SetCod(OUTP_ECLT) = ":OUTP:" & .cboTrigOutECL.Text & " "

                    If (sArg = "ON") Then
                        .chkTrigOutECLOn.Checked = True
                        .chkTrigOutECLOn_Click(True)
                    ElseIf (sArg = "OFF") Then
                        .chkTrigOutECLOn.Checked = False
                        .chkTrigOutECLOn_Click(False)
                    Else
                        sErrorMsg = "Argument"
                    End If

                Case OUTP_FILT_FREQ
                    If (sArg = "20MHZ") Then
                        .optCutOff20.Checked = True
                        .optCutOff20_Click(True)
                    ElseIf (sArg = "25MHZ") Then
                        .optCutOff25.Checked = True
                        .optCutOff25_Click(True)
                    ElseIf (sArg = "50MHZ") Then
                        .optCutOff50.Checked = True
                        .optCutOff50_Click(True)
                    Else
                        'mh sErrorMsg = "Argument"
                        .optCutOff20.Checked = True
                        .optCutOff20_Click(True)
                    End If

                Case OUTP_FILT
                    If (sArg = "ON") Then
                        .chkFilterStateOn.Checked = True
                        .chkFilterStateOn_Click(True)
                    ElseIf (sArg = "OFF") Then
                        .chkFilterStateOn.Checked = False
                        .chkFilterStateOn_Click(False)
                    Else
                        sErrorMsg = "Argument"
                    End If


                Case Else
                    sErrorMsg = "Command"
            End Select

            If sErrorMsg <> "" Then

                If bDesignMode Then
                    MsgBox("Unexpected Command <" & SCPI & "> sent from TIP Studio." & vbCrLf & vbCrLf & "Instrument will be set to RESET state.", MsgBoxStyle.Exclamation, "Function Generator")

                ElseIf bRunMode Or bPersistMode Then
                    MsgBox("Unexpected Command <" & SCPI & "> sent from TIP Studio." & vbCrLf & vbCrLf & "SAIS session will be terminated.", MsgBoxStyle.Exclamation, "Function Generator")

                ElseIf bInstrumentMode Then
                    MsgBox("Invalid <" & SCPI & "> argument received from instrument query.", MsgBoxStyle.OkOnly, "Get Current - Unhandled Parameter")
                    Exit Function
                End If

                If (bRunMode) Or (bPersistMode) Then
                    SetKey("TIPS", "STATUS", "Error from FG SAIS: " & "Unexpected " & sErrorMsg & ": " & SCPI)
                    .cmdQuit_Click()
                End If

                .cmdReset_Click() 'Reset Instrument and Front Panel
                ActivateControl = False
                Exit Function
            End If
        End With
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
        Dim lpReturnedString As String = "" 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File
        Const MAX_PATH As Long = 260

        'Clear String Buffer
        lpReturnedString = Space$(MAX_PATH)
        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString)
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo)-1)
        'If File Locations Missing, then create empty keys
        If FileNameInfo=lpDefault+Convert.ToString(Chr(0)) Or FileNameInfo=lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo
    End Function

'    Public Sub Exit_App()
'        Dim frm As Form

'        For Each frm In Application.OpenForms
'            frm.Hide() ' hide the form
'            frm.Close() ' deactivate the form
'            frm = Nothing ' remove from memory
'        Next
'    End Sub
End Module