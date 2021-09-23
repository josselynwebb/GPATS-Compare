Option Strict Off
Option Explicit On
Module TETS
	'************************************************************
	'* Written By     : Grady Johnson                           *
	'*    DESCRIPTION:                                          *
	'*     This Module provides the ability to press the Abort  *
	'*     button have the VIPERT instrumentation Reset and     *
	'*     return to the Main Menu.                             *
	'*    EXAMPLE:                                              *
	'*     TETS.ResetSystem                                     *
	'*    PARAMETERS:                                           *
	'*     CMain.CInstrHandles =  Provides session hndl, instr  *
	'*     hndls, and required instruments from executing TPS   *
	'************************************************************
	
	'-----------------Power Supply Declarations---------------------------
	Declare Function aps6062_init Lib "APS6062.DLL" (ByVal ResourceNameX As String, ByVal idQuery As Short, ByVal resetDevice As Short, ByRef instrumentHandle As Integer) As Integer
	''bbbb deleted
    ''Declare Function aps6062_readInstrData Lib "APS6062.DLL" (ByVal instrumentHandle As Long, ByVal numberBytesToRead As Long, ByVal ReadBuffer As String, numBytesRead As Long) As Long
    ''Declare Function aps6062_writeInstrData Lib "APS6062.DLL" (ByVal instrumentHandle As Long, ByVal writeBuffer As String) As Long
    ''Declare Function aps6062_errorMessage Lib "APS6062.DLL" (ByVal instrumentHandle As Long, ByVal errorcode As Long, ByVal errorMessage As String) As Long
    ''Declare Function aps6062_close Lib "APS6062.DLL" (ByVal instrumentHandle As Long) As Long
    ''Declare Function aps6062_errorQuery Lib "APS6062.DLL" (ByVal instrumentHandle As Long, ByVal errorcode As long, ByVal errorMessage As String) As Long
    ''Declare Function eip1315a_init Lib "rfcnt.dll" (ByVal ResourceName As String, instrumentHandle As Long)
    ''Declare Function eip1315a_setLO Lib "rfcnt.dll" (ByVal theSession As Long, ByVal Frequency As Double, ByVal mode, ByVal attn As Long, ByVal IFDET As Integer)
    ''Declare Function eip1315a_close Lib "rfcnt.dll" (ByVal instrumentHandle As Long)
    ''Declare Function gt50000_init Lib "gt50000.dll" (ByVal ResourceName As String, ByVal idQuery As Integer, ByVal resetDevice As Integer, instrumentHandle As Long) As Long
	''
    ''5/5/03 by Soon Nam
    ''added for RF COUNTER dll calls
    ''Declare Function rfcnt_init Lib "rfcnt.dll" (ByVal ResourceName As String, instrumentHandle As Long)
    ''Declare Function gt55210a_init Lib "rfcnt.dll" (ByVal ResourceName As String, instrumentHandle As Long)
    ''Declare Function rfcnt_reset Lib "rfcnt.dll" (ByVal instrSession As Long)
    ''Declare Function rfcnt_writeInstrData Lib "rfcnt.dll" (ByVal instrSession As Long, ByVal writeBuffer As String)
    ''Declare Function rf_measfreq Lib "rfcnt.dll" (ByVal ihDC As Long, ByVal ihCNT As Long, ByVal expectedfreq As Long, ByVal attn As Long, measfreq As Integer)
    ''Declare Function rf_measpw Lib "rfcnt.dll" (ByVal ihDC As Long, ByVal ihCNT As Long, ByVal carrierfreq As Double, ByVal expectedpw As Double, ByVal attn As Long, measpw As Double)
    ''Declare Function rf_measpd Lib "rfcnt.dll" (ByVal ihDC&, ByVal ihCNT As Long, ByVal carrierfreq As double, ByVal expectedpw As Double, ByVal attn As Long, measpd As Double)
    ''end add
	''
    ''5/8/03 by Soon Nam
    ''added for RF Stim dll calls
    ''Declare Function gt50000_selectCorrectionsTable Lib "gt50000.dll" (ByVal FilePath As String, ByVal TableName As String)
    ''Declare Function gt50000_setCorrectedPower Lib "gt50000.dll" (ByVal instrSession As Long, ByVal Frequency As Double, ByVal Power As Single)
    ''Declare Function selectCorrectionsTable Lib "gt50000.dll" Alias "gt50000_selectCorrectionsTable" (ByVal FilePath As String, ByVal TableName As String)
    ''Declare Function setCorrectedPower Lib "gt50000.dll" Alias "gt50000_setCorrectedPower" (ByVal instrSession As Long, ByVal Frequency As Double, ByVal Power As Single)
    ''end add
	''
    ''6/24/03 Soon Nam
	''added for RF POWER METER dll calls
    ''Declare Function hpe1416a_init Lib "hpe1416a.DLL" (ByVal ResourceName As String, ByVal idQuery As Integer, ByVal resetDevice As Integer, instrumentHandle As Long) As Long
    ''Declare Function hpe1416a_readInstrData Lib "hpe1416a.DLL" (ByVal instrumentHandle As Long, ByVal numberBytesToRead As Long, ByVal ReadBuffer As String, numBytesRead As Long) As Long
    ''end add
	
	Declare Function terM9_init Lib "terM9_32.dll" (ByVal rsrcName As String, ByVal idQuery As Short, ByVal resetInstr As Short, ByRef vi As Integer) As Integer
	Declare Function terM9_reset Lib "terM9_32.dll" (ByVal vi As Integer) As Integer
	Declare Function terM9_close Lib "terM9_32.dll" (ByVal vi As Integer) As Integer
	
	Public Const SEEDED_GUIDED_PROBE As Short = 1
	Public Const FAULT_DICTIONARY_ONLY As Short = 2
	Public Const GUIDED_PROBE_ONLY As Short = 3
	Public Const MAX_DIAG_TEXT_SIZE As Short = 4096
	
	Public Const LOG_FILE As String = "C:\APS\DATA\FAULT-FILE"
	'Public Const SYSTEM_LOG = "C:\TETS\SYSLOG\SYSLOG.EXE"
	'Public Const TETS_INI = "TETS.INI"
    Public TETS_ini As String

    Public Const MANUAL As Short = -1

    Public sInstrName() As Object
    Public Const MIL_STD_1553 As Short = 1
    Public Const DIGITAL As Short = 2
    Public Const DMM As Short = 3
    Public Const OSCOPE As Short = 4
    Public Const COUNTER As Short = 5
    Public Const ARB As Short = 6
    Public Const FGEN As Short = 7
    Public Const PPU As Short = 8
    Public Const APROBE As Short = 9
    Public Const SWITCH1 As Short = 10 : Public Const LFSWITCH1 As Short = 1 : Public Const SWITCH_CONTROLLER As Short = 0
    Public Const SWITCH2 As Short = 11 : Public Const LFSWITCH2 As Short = 2
    Public Const SWITCH3 As Short = 12 : Public Const PWRSWITCH As Short = 3
    Public Const SWITCH4 As Short = 13 : Public Const MFSWITCH As Short = 4
    Public Const SWITCH5 As Short = 14 : Public Const RFSWITCH As Short = 5

    'bbbb added for ViperT
    Public Const SYN_RES As Short = 15
    Public Const RS422 As Short = 16
    Public Const RS232 As Short = 17
    Public Const GIGA As Short = 18

    Public Const LAST_CORE_INST As Short = GIGA ' bbbb was SWITCH4

    Public Const RFPM As Short = 19 ' bbbb was 15
    Public Const RFCOUNTER As Short = 20
    Public Const RFREC As Short = 21
    Public Const MOD_ANAL As Short = 22
    Public Const RFSYN As Short = 23
    Public Const RFMEAS As Short = 24 'bbbb
    Public Const LAST_RF_INST As Short = RFSYN

    Public Const EO_VC As Short = 25 ' bbbb was 20
    Public Const EO_MF As Short = 26
    Public Const EO_IR As Short = 27
    Public Const EO_VIS As Short = 28
    Public Const EO_MS As Short = 29
    Public Const EO_VCC As Short = 30 'bbbb
    Public Const VEO2 As Short = 31 'bbbb
    Public Const LAST_EO_INST As Short = VEO2
    Public Const LAST_INSTRUMENT As Short = LAST_EO_INST

    'Added to Identify specific RFMA instruments
    Public Const DOWNCONV As Integer = LAST_INSTRUMENT + 1 ' 32
    Public Const LOCALOSC As Integer = LAST_INSTRUMENT + 2 ' 33
    Public Const DIGITIZER As Integer = LAST_INSTRUMENT + 3 ' 34
    Public Const CALIBRATOR As Integer = LAST_INSTRUMENT + 4 ' 35
    Public Const VEO_LARRS As Integer = LAST_INSTRUMENT + 5 ' 36


    Public bSimulation As Boolean
    Public bLiveMode As Boolean
    'Public nSessionHandle As Long
    Public lSystErr As Integer
    Public TPS_Development As Boolean
    Public bTimedOut As Boolean 'This flag is set when an instrument does not respond to
    ' a measurement query within that instruments TMO time.
    ' It is initialized to False by 'iViperTinit'.
    Public Const MAX_LOW As Double = -1.0E+300 '\__/ Used to replace 'dMeasured' values when
    Public Const MAX_HIGH As Double = 1.0E+300 '/  \ error conditions exist

    Public iWriteMsgError As Short 'Instrument Error code set by various WriteMsg functions.

    Public bInstrRequired() As Boolean
    Public sInstFileName(LAST_INSTRUMENT + 8) As String
    Public nInstrumentHandle(LAST_INSTRUMENT) As Integer
    Public sIDNResponse(LAST_INSTRUMENT) As String
    Public sInstrumentSpec(LAST_INSTRUMENT) As String
    Public sInstrumentDescription(LAST_INSTRUMENT + 8) As String
    Public iInstrumentInitialized(LAST_INSTRUMENT) As String
    Public sInstrumentIDCode(LAST_INSTRUMENT) As String
    Public nSupplyHandle(11) As Integer
    Public bRFOptionInstalled As Boolean
    Public bEoOptionInstalled As Boolean
    Public iLastInstrument As Short
    Public ReadBuffer As String = Space(256)
    Public lBytesRead As Integer
    Public GIGA_DOWN As Boolean
    Public bSwitchCardOK(RFSWITCH) As Boolean
    Public sIddata() As String
    Public sIDList(5) As String

    Sub ResetSystem()
        'DESCRIPTION:
        '   This routine resets the system.
        'PARAMETERS:
        '   None
        Dim nSystErr As Integer
        Dim nInitStatus As Integer
        Dim SystErr As Integer

        bReseting = True
        If bSimulation Then
            ShowMainMenu()
            bReseting = False
            Exit Sub
        End If

        If bInstrRequired(PPU) Then
            iInitDCPS(bReset:=True)
        End If

        nSystErr = atxmlDF_viClear(ResourceName(DMM), 0)
        nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
        nSystErr = atxmlDF_viClear(ResourceName(ARB), 0)
        nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0)
        nSystErr = atxmlDF_viClear(ResourceName(FGEN), 0)
        nSystErr = atxmlDF_viClear(ResourceName(SWITCH1), 0)

        If bRFOptionInstalled Then
            nSystErr = atxmlDF_viClear(ResourceName(RFPM), 0)
            nSystErr = atxmlDF_viClear(ResourceName(RFSYN), 0)
            nSystErr = atxmlDF_viClear(ResourceName(RFMEAS), 0)
        End If

        WriteMsg(DMM, "*RST; *CLS")
        WriteMsg(COUNTER, "*RST; *CLS")
        WriteMsg(ARB, "*RST; *CLS")
        WriteMsg(OSCOPE, "*RST; *CLS")
        WriteMsg(OSCOPE, "SYST:LANG COMP") 'bbbb
        WriteMsg(FGEN, "*RST; *CLS")
        WriteMsg(SWITCH1, "RESET")

        ' close interconnect relays
        WriteMsg(SWITCH1, "OPEN 3.1000,1002,2000,2001,3002,4002,5000,5001,5002,6002,7002,8002")
        WriteMsg(SWITCH1, "CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")

        'bbbb deleted
        ''    If bInstrRequired(DMM) Then SystErr& = viClear(nInstrumentHandle(DMM))
        ''    If bInstrRequired(COUNTER) Then SystErr& = viClear(nInstrumentHandle(COUNTER))
        ''    If bInstrRequired(RFPM) Then SystErr& = viClear(nInstrumentHandle(RFPM))
        ''    If bInstrRequired(ARB) Then SystErr& = viClear(nInstrumentHandle(ARB))
        ''    If bInstrRequired(RFSYN) Then SystErr& = viClear(nInstrumentHandle(RFSYN))
        ''    If bInstrRequired(OSCOPE) Then SystErr& = viClear(nInstrumentHandle(OSCOPE))
        ''    If bInstrRequired(FGEN) Then SystErr& = viClear(nInstrumentHandle(FGEN))
        ''    If bInstrRequired(RFCOUNTER) Then SystErr& = viClear(nInstrumentHandle(RFCOUNTER))
        ''    If bInstrRequired(MOD_ANAL) Then SystErr& = viClear(nInstrumentHandle(MOD_ANAL))
        ''    If bInstrRequired(SWITCH1) Then SystErr& = viClear(nInstrumentHandle(SWITCH1))
        ''    If bInstrRequired(RFPM) Then WriteMsg RFPM, "*RST; *CLS"
        ''    If bInstrRequired(rfcounter) Then WriteMsg rfcounter, "*RST; *CLS"
        ''    If bInstrRequired(DMM) Then InitStatus& = eip1315a_setLO&(nInstrumentHandle(RFREC), 0, MANUAL, 70, DETOUT)

        'bbbb added Apr 2011
        If bInstrRequired(DMM) Then SystErr = atxmlDF_viClear(ResourceName(DMM), 0)
        If bInstrRequired(COUNTER) Then SystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
        If bInstrRequired(ARB) Then SystErr = atxmlDF_viClear(ResourceName(ARB), 0)
        If bInstrRequired(RFSYN) Then SystErr = atxmlDF_viClear(ResourceName(RFSYN), 0)
        If bInstrRequired(OSCOPE) Then SystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0)
        If bInstrRequired(FGEN) Then SystErr = atxmlDF_viClear(ResourceName(FGEN), 0)
        If bInstrRequired(SWITCH1) Then SystErr = atxmlDF_viClear(ResourceName(SWITCH1), 0)

        If bInstrRequired(DMM) Then WriteMsg(DMM, "*RST; *CLS")
        If bInstrRequired(COUNTER) Then WriteMsg(COUNTER, "*RST; *CLS")
        If bInstrRequired(ARB) Then WriteMsg(ARB, "*RST; *CLS")
        If bInstrRequired(RFSYN) Then WriteMsg(RFSYN, "*RST")
        If bInstrRequired(RFSYN) Then WriteMsg(RFSYN, "*CLS")
        If bInstrRequired(OSCOPE) Then WriteMsg(OSCOPE, "*RST; *CLS")
        If bInstrRequired(FGEN) Then WriteMsg(FGEN, "*RST; *CLS")
        If bInstrRequired(SWITCH1) Then WriteMsg(SWITCH1, "RESET")
        If bRFOptionInstalled Then
            WriteMsg(RFPM, "*RST; *CLS")
            WriteMsg(RFSYN, "*RST")
            WriteMsg(RFSYN, "*CLS")
        End If
        If CBool(iInstrumentInitialized(DIGITAL)) Then
            nInitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, nInstrumentHandle(DIGITAL))
            nSystErr = terM9_reset(nInstrumentHandle(DIGITAL))
            nInitStatus = terM9_close(nInstrumentHandle(DIGITAL))
        End If
        bReseting = False
    End Sub

    Sub ReleaseSystem()

        'bbbb added this routine to release the Cicl upon exiting
        Dim InstrumentToClose As Short
        Dim SupplyCount As Short
        Dim ErrorStatus As Integer
        Dim FileError As Integer
        Dim SystemLogSpec As String
        Dim FileHandle As Integer
        Dim TaskID As Integer
        Dim count As Short

        On Error Resume Next

        If nInstrumentHandle(DIGITAL) <> 0 Then
            nSystErr = terM9_close(nInstrumentHandle(DIGITAL))
        End If
        nInstrumentHandle(DIGITAL) = 0

        'Return Instrument Handles
        For InstrumentToClose = 0 To LAST_INSTRUMENT
            If nInstrumentHandle(InstrumentToClose) <> 0 Then
                nSystErr = viClose(nInstrumentHandle(InstrumentToClose))
            End If
        Next InstrumentToClose

        'Return PPU Handles
        For InstrumentToClose = 1 To 10
            If nSupplyHandle(InstrumentToClose) <> 0 Then
                nSystErr = viClose(nSupplyHandle(InstrumentToClose))
            End If
        Next InstrumentToClose
        On Error Resume Next

        lSystErr = viClose(nSessionHandle)

        ErrorStatus = atxml_Close()
        Delay(0.5)
        Echo("Program End")
        FileClose() ' close all open files
        ' END OF PROGRAM (DLL)
    End Sub

    ''' <summary>
    ''' This routine strips contiguous characters with ASCII values less than 33 from the end of a string.  
    ''' The original string doesn't actually get stripped.
    ''' </summary>
    ''' <param name="Parsed">String from which to remove null characters</param>
    ''' <returns>The resultant parsed string</returns>
    ''' <remarks></remarks>
    Function StripNullCharacters(ByRef Parsed As String) As String
        Dim x As Short

        For x = Len(Parsed) To 1 Step -1
            If Asc(Mid(Parsed, x, 1)) > 32 Then
                Exit For
            End If
        Next x
        StripNullCharacters = Left(Parsed, x)
    End Function

    ''' <summary>
    ''' This function checks to see if the TETS System Software is installed and and SHOULD be run at start-up of TPS.
    ''' </summary>
    ''' <returns>True if the TETS System Software is installed;  False if the TETS System Software is not installed</returns>
    ''' <remarks></remarks>
    Function iTETSInit() As Short
        Dim iInstrument As Integer
        TETSVar()
        bTimedOut = False
        ClearTestMeasurementInformation()
        gFrmMain.txtModule.Text = "System Init"
        gFrmMain.txtTestName.Text = "VISA Check"
        gFrmMain.txtStep.Text = "N/A"
        gFrmMain.txtInstrument.Text = "N/A"
        gFrmMain.txtCommand.Text = "N/A"
        gFrmMain.txtUpperLimit.Text = "TRUE"
        gFrmMain.txtLowerLimit.Text = "TRUE"
        gFrmMain.txtUnit.Text = "BOOLEAN"

        '*********** TEST NAME AND PRINT OUT HEADLINE **********
        Echo("**** System Initialization ****")

        'Verify presence of system
        iTETSInit = True
        bLiveMode = True
        SystemDir = Environment.SystemDirectory

        If Right(SystemDir, 1) <> "\" Then
            SystemDir = SystemDir & "\"
        End If
        VisaLibrary = SystemDir & "\VISA32.DLL"

'        If Not bFileExists(VisaLibrary) Then
            gFrmMain.txtMeasured.Text = "FALSE"
            iTETSInit = False
            bLiveMode = False
            nSessionHandle = 0
'            Echo("SYSTEM ERROR - Unable to initialize VIPERT System. Can't find VISA32.DLL.")
            Exit Function
'        Else
'            gFrmMain.txtMeasured.Text = "TRUE"
'        End If

        gFrmMain.txtTestName.Text = "Open Default RM"
        gFrmMain.txtMeasured.Text = ""
        lSystErr = viOpenDefaultRM(nSessionHandle)
        If lSystErr < 0 Then
            gFrmMain.txtMeasured.Text = "FALSE"
            iTETSInit = False
            bLiveMode = False
            nSessionHandle = 0
            Echo("SYSTEM ERROR - Unable to initialize ViperT System. Can't obtain VISA session handle.")
            Exit Function
        Else
            gFrmMain.txtMeasured.Text = "TRUE"
        End If

        'Check for RF Instruments
        gFrmMain.txtTestName.Text = "Check RF Variant"
        gFrmMain.txtUpperLimit.Text = "N/A"
        gFrmMain.txtLowerLimit.Text = "N/A"
        gFrmMain.txtMeasured.Text = ""
        iLastInstrument = LAST_CORE_INST
        bRFOptionInstalled = bCheckOptionInstalled("RF")
        If bRFOptionInstalled Then
            gFrmMain.txtMeasured.Text = "TRUE"
            iLastInstrument = LAST_RF_INST
            'Check RF STIM Type
            If Not bCheckOptionInstalled("RFSTIM") Then
                sIDNResponse(RFSYN) = "1140B"
                sInstrumentDescription(RFSYN) = "PM 1140B RF Stimulus"
            End If
        Else
            gFrmMain.txtMeasured.Text = "FALSE"
        End If

        'Check for EO Instruments
        gFrmMain.txtTestName.Text = "Check EO Variant"
        gFrmMain.txtMeasured.Text = ""
        bEoOptionInstalled = bCheckOptionInstalled("EO")
        If bEoOptionInstalled Then
            gFrmMain.txtMeasured.Text = "TRUE"
            iLastInstrument = LAST_EO_INST
        Else
            gFrmMain.txtMeasured.Text = "FALSE"
        End If

        gFrmMain.txtTestName.Text = "Check VXI Power"
        gFrmMain.txtUpperLimit.Text = "TRUE"
        gFrmMain.txtLowerLimit.Text = "TRUE"
        gFrmMain.txtMeasured.Text = ""
        If Not VXIPowerCheck() Then
            gFrmMain.txtMeasured.Text = "FALSE"
            iTETSInit = False
            bLiveMode = False
            nSessionHandle = 0
            Echo("SYSTEM ERROR - Unable to initialize VIPERT System. VXI Power off.")
            Exit Function
        Else
            gFrmMain.txtMeasured.Text = "TRUE"
        End If

        'moved from above to this place 5/20/03 Soon Nam
        UpdateProgress((0)) 'Initialize progress bar on shell screen

        '**** Initialize Instruments ****
        '********************************
        For iInstrument = 1 To iLastInstrument
            If bInstrRequired(iInstrument) Then
                gFrmMain.txtTestName.Text = "Init. " & sInstFileName(iInstrument)
                Select Case iInstrument
                    Case RFPM, RFCOUNTER
                        iInstrumentInitialized(iInstrument) = "TRUE"
                    Case DMM, COUNTER, ARB, FGEN, OSCOPE, RFSYN, RFMEAS
                        If Not InitMessageBasedInstrument(iInstrument) Then
                            gFrmMain.txtMeasured.Text = "FALSE"
                            iTETSInit = False
                            bLiveMode = False
                            Echo("INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & ".")
                            Exit Function
                        End If
                        'bbbb deleted
                        ''                Case MOD_ANAL
                        ''                    If Not INITInst.iInitModAnal Then
                        ''                            gFrmMain.txtMeasured.Text = "FALSE"
                        ''                            iTetsInit = False
                        ''                            bLiveMode = False
                        ''                            Echo "INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & "."
                        ''                            Exit Function
                        ''                    End If
                        ''                Case RFREC
                        ''                    If Not INITInst.iInitRFReceiver Then
                        ''                            gFrmMain.txtMeasured.Text = "FALSE"
                        ''                            iTetsInit = False
                        ''                            bLiveMode = False
                        ''                            Echo "INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & "."
                        ''                            Exit Function
                        ''                    End If
                    Case SWITCH1, SWITCH2, SWITCH3, SWITCH4, SWITCH5
                        If Not INITInst.iInitSwitching(iInstrument) Then
                            gFrmMain.txtMeasured.Text = "FALSE"
                            iTETSInit = False
                            bLiveMode = False
                            Echo("INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & ".")
                            Exit Function
                        Else
                            iInstrumentInitialized(iInstrument) = "TRUE"
                        End If
                    Case DIGITAL
                        If Not INITInst.iInitDigital(True) Then
                            gFrmMain.txtMeasured.Text = "FALSE"
                            iTETSInit = False
                            bLiveMode = False
                            Echo("INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & ".")
                            Exit Function
                        Else
                            iInstrumentInitialized(iInstrument) = "TRUE"
                        End If
                    Case MIL_STD_1553
                        iInstrumentInitialized(iInstrument) = "TRUE"
                        gFrmMain.txtMeasured.Text = "TRUE"
                    Case PPU
                        If Not INITInst.iInitDCPS Then
                            gFrmMain.txtMeasured.Text = "FALSE"
                            iTETSInit = False
                            bLiveMode = False
                            Echo("INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & ".")
                            Exit Function
                        Else
                            iInstrumentInitialized(iInstrument) = "TRUE"
                        End If
                End Select
                gFrmMain.txtMeasured.Text = "TRUE"
                System.Windows.Forms.Application.DoEvents()
            End If
            UpdateProgress(((System.Math.Round(iInstrument / iLastInstrument, 2)) * 100))
        Next iInstrument
        Echo("")
    End Function

    ''' <summary>
    ''' This routine determines if the TETS RF option is installed
    ''' </summary>
    ''' <param name="sOpt">Option to check for; RF, EO, or RFSTIM</param>
    ''' <returns>TRUE if the RF option is installed</returns>
    ''' <remarks></remarks>
    Function bCheckOptionInstalled(ByRef sOpt As String) As Boolean
        Dim lpBuffer As String = Space(256)
        Dim FileError As Integer
        Dim ReadBuffer As String
        Dim sKey As String = ""

        'bbbb added
        Select Case UCase(sOpt)
            Case "RF" : sKey = "RF_OPTION_INSTALLED"
            Case "EO" : sKey = "EO_OPTION_INSTALLED"
            Case "BUSS" : sKey = "BUSS_OPTION_INSTALLED"
            Case "RFSTIM" : sKey = "RF_OPTION_INSTALLED" 'bbbb
            Case Else : MsgBox("Invalid argument (" & UCase(sOpt) & ") in CheckOptionInstalled function, notify maintenance.")
        End Select

        lpBuffer = ""
        FileError = GetPrivateProfileString("System Startup", sKey, "YES", lpBuffer, 256, TETS_ini)
        ReadBuffer = StripNullCharacters(lpBuffer)
        If UCase(Trim(ReadBuffer)) = "YES" Then
            bCheckOptionInstalled = True
        Else
            bCheckOptionInstalled = False
        End If

        'bbbb deleted
        ''    Select Case UCase(sOpt)
        ''      Case "RF":  sKey = "RF_OPTION_INSTALLED"
        ''      Case "EO":  sKey = "EO_OPTION_INSTALLED"
        ''      Case "RFSTIM": sKey = "RFS"
        ''      Case Else
        ''         Echo "Invalid argument in CheckOptionInstalled function."
        ''         bCheckOptionInstalled = False
        ''    End Select
        ''
        ''    slpBuffer = ""
        ''    If sKey <> "RFS" Then
        ''      nFileError = GetPrivateProfileString("System Startup", sKey, "YES", lpBuffer, 256, WindowsDirectory + TETS_INI)
        ''    Else
        ''      nFileError = GetPrivateProfileString("SAIS", sKey, "GT50000.EXE", lpBuffer, 256, WindowsDirectory + TETS_INI)
        ''    End If
        ''
        ''    sReadBuffer = StripNullCharacters(lpBuffer)
        ''    If InStr(UCase(sReadBuffer), "YES") <> 0 Or UCase(Trim(sReadBuffer)) = "GT50000.EXE" Then
        ''      bCheckOptionInstalled = True
        ''    Else
        ''      bCheckOptionInstalled = False
        ''    End If
    End Function

    ''' <summary>
    ''' This Module checks the TETS.INI file and ensures the VXI power switch is on.
    ''' </summary>
    ''' <returns>VXI power state</returns>
    ''' <remarks></remarks>
    Public Function VXIPowerCheck() As Short
        Dim keyinfo As String = Space(256)
        Dim errorMessage As Integer
        Dim Answer As Integer

        VXIPowerCheck = True
        Do
            errorMessage = GetPrivateProfileString("System Monitor", "CHASSIS_STATE", "OFF", keyinfo, 256, TETS_ini)

            If UCase(Trim(StripNullCharacters(keyinfo))) = "OFF" Then
                Answer = MsgBox("Instrument Chassis Power Switch Not Activated.", MsgBoxStyle.RetryCancel)
                If Answer = MsgBoxResult.Cancel Then
                    Exit Do
                End If
            Else
                Exit Do
            End If
        Loop

    End Function

    ''' <summary>
    ''' This Routine initializes a message based instrument and verifies that it is the correct instrument
    ''' </summary>
    ''' <param name="InstrumentToInit">The index of the instrument to initialize</param>
    ''' <returns>An integer representing status PASSED, FAILED or UNTESTED</returns>
    ''' <remarks></remarks>
    Function InitMessageBasedInstrument(ByRef InstrumentToInit As Integer) As Short
        Dim DevSpec As String
        Dim IDSpec As String
        Dim InitStatus As Integer
        Dim iHandle As Integer
        Dim Dummy As Integer

        InitMessageBasedInstrument = True
        iInstrumentInitialized(InstrumentToInit) = CStr(True)
        iHandle = nInstrumentHandle(InstrumentToInit) 'The VISA session handle
        DevSpec = sInstrumentSpec(InstrumentToInit) 'The unique symbolic name of the resource
        IDSpec = sIDNResponse(InstrumentToInit) 'The concatination of the first two elements returned from the *IDN?

        'bbbb deleted
        ''    If IDSpec = "50000" Then
        ''      InitStatus = gt50000_init(DevSpec, idQuery:=0, resetDevice:=1, instrumentHandle:=IHandle)
        ''      Delay 0.1   'Allow RF Stim to change language
        ''    Else
        ''      InitStatus = viOpen(nSessionHandle, DevSpec, VI_NULL, VI_NULL, IHandle)
        ''    End If

        'bbbb added
        Select Case InstrumentToInit
            Case RFMEAS, COUNTER, ARB
                InitStatus = viOpen(nSessionHandle, DevSpec, VI_NULL, VI_NULL, iHandle)
                If InitStatus <> 0 Then
                    Echo("Could not get instrument handle for " & DevSpec)
                End If
        End Select

        If InitStatus <> VI_SUCCESS Then
            Echo("Could not get instrument handle for " & DevSpec)
            iInstrumentInitialized(InstrumentToInit) = CStr(False)
            InitMessageBasedInstrument = False
            Exit Function
        End If

        'bbbb deleted
        ''    If IDSpec <> "50000" Then
        ''      Dummy = viClear(ihandle)
        ''    End If
        ''    WriteMsg InstrumentToInit, "*RST"
        ''    WriteMsg InstrumentToInit, "*CLS"
        ''    Delay 0.2
        ''    Do
        ''        WriteMsg InstrumentToInit, "SYST:ERR?"
        ''        Delay 0.5
        ''        SystErr = ReadMsg(InstrumentToInit, ReadBuffer)
        ''    Loop While Val(ReadBuffer) <> 0

        'bbbb added
        Dummy = atxmlDF_viClear(ResourceName(InstrumentToInit), 0)
        Delay(0.5)
        If InstrumentToInit = RFMEAS Then
            WriteMsg2(iHandle, "*RST") ' RFMEAS does not use the cicl
            WriteMsg2(iHandle, "*CLS")
            Delay(0.2)
            Do
                WriteMsg2(iHandle, "SYST:ERR?")
                Delay(0.5)
                nSystErr = ReadMsg2(iHandle, ReadBuffer)
            Loop While Val(ReadBuffer) <> 0
        ElseIf (InstrumentToInit <= LAST_RF_INST) Or (InstrumentToInit > LAST_EO_INST) Then
            WriteMsg(InstrumentToInit, "*RST") ' uses the cicl
            WriteMsg(InstrumentToInit, "*CLS")
            Delay(0.2)
            Do
                WriteMsg(InstrumentToInit, "SYST:ERR?")
                Delay(0.5)
                nSystErr = ReadMsg(InstrumentToInit, ReadBuffer)
            Loop While Val(ReadBuffer) <> 0
        End If

        nInstrumentHandle(InstrumentToInit) = iHandle
    End Function

    ''' <summary>
    ''' This routine initializes the program's variables
    ''' </summary>
    ''' <remarks></remarks>
    Sub TETSVar()
        Dim lpBuffer As String = Space(256)
        'Call InitData           'Initialize FHDB Data as soon as possible

        sIDNResponse(DMM) = "E1412A"
        sIDNResponse(COUNTER) = "E1420B"
        sIDNResponse(ARB) = "E1445A"
        sIDNResponse(RFPM) = "E1416A"
        sIDNResponse(RFSYN) = "1140B"
        sIDNResponse(OSCOPE) = "ZT1428" ' vipert
        sIDNResponse(FGEN) = "3152"
        sIDNResponse(RFMEAS) = "VM2601"

        ' bbbb deleted
        ''    sIDNResponse(MOD_ANAL) = "Boonton Model 8701"
        ''
        ''    sInstrumentSpec(OSCOPE) = "VXI::17::INSTR"
        ''    sInstrumentSpec(COUNTER) = "VXI::18::INSTR"
        ''    sInstrumentSpec(FGEN) = "VXI::19::INSTR"
        ''    sInstrumentSpec(RFSYN) = "VXI0::20::INSTR"
        ''    sInstrumentSpec(RFREC) = "VXI::23::INSTR"
        ''    sInstrumentSpec(RFCOUNTER) = "VXI::25::INSTR"
        ''    sInstrumentSpec(DMM) = "VXI::44::INSTR"
        ''    sInstrumentSpec(ARB) = "VXI::48::INSTR"
        ''    sInstrumentSpec(RFPM) = "VXI::28::INSTR"
        ''    sInstrumentSpec(SWITCH1) = "VXI::38::INSTR"
        ''    sInstrumentSpec(MOD_ANAL) = "VXI::27::INSTR"
        ''
        ''    sInstrumentDescription(DMM) = "HP E1412A Digital Multimeter"
        ''    sInstrumentDescription(COUNTER) = "HP E1420B Counter/Timer (Slot 2)"
        ''    sInstrumentDescription(RFCOUNTER) = "HP E1420B Counter/Timer (Slot 9)"
        ''    sInstrumentDescription(ARB) = "HP E1445A Arbitrary Waveform Generator"
        ''    sInstrumentDescription(DIGITAL) = "Teradyne M910 Digital Subsystem"
        ''    sInstrumentDescription(FGEN) = "Racal Instruments 3152 Function Generator"
        ''    sInstrumentDescription(RFPM) = "HP E1416A RF Power Meter"
        ''    sInstrumentDescription(RFSYN) = "Giga-tronics 50000A RF Synthesizer"
        ''    sInstrumentDescription(RFREC) = "EIP 1315A RF Downconverter"
        ''    sInstrumentDescription(OSCOPE) = "HP E1428A Digitizing Oscilloscope"
        ''    sInstrumentDescription(PPU) = "APS 7081-PPU Programmable Power Unit"
        ''    sInstrumentDescription(SWITCH1) = "Racal 1260-39 LF Switches #1 (Slot 5)"
        ''    sInstrumentDescription(SWITCH2) = "Racal 1260-39 LF Switches #2 (Slot 6)"
        ''    sInstrumentDescription(SWITCH3) = "Racal 1260-38T LF Switches #3 (Slot 7)"
        ''    sInstrumentDescription(SWITCH4) = "Racal 1260-58 MF Switches (Slot 8)"
        ''    sInstrumentDescription(SWITCH5) = "Racal 1260-66 HF Switches (Slot 9)"
        ''    sInstrumentDescription(MOD_ANAL) = "Boonton 8701 Modulation Meter"
        ''    sInstrumentDescription(MIL_STD_1553) = "DDC BUS-69080, MIL-STD-1553 Interface"
        ''    sInstrumentDescription(APROBE) = "Analog Probe"
        ''    sInstrumentDescription(EO_VC) = "ITI IC-PCI Video Capture Card"
        ''    sInstrumentDescription(EO_MF) = "SBIR Multi-Function Module"
        ''    sInstrumentDescription(EO_IR) = "SBIR Infrared Module"
        ''    sInstrumentDescription(EO_VIS) = "SBIR Visible Module"
        ''    sInstrumentDescription(EO_MS) = "SBIR Modulated Source Module"

        'bbbb added
        sInstrumentSpec(OSCOPE) = "VXI::17::INSTR"
        sInstrumentSpec(COUNTER) = "VXI::18::INSTR"
        sInstrumentSpec(FGEN) = "VXI::19::INSTR"
        sInstrumentSpec(RFSYN) = "VXI0::20::INSTR"
        sInstrumentSpec(RFMEAS) = "VXI::25::INSTR"
        sInstrumentSpec(RFPM) = "VXI::28::INSTR"
        sInstrumentSpec(SYN_RES) = "VXI::30::INSTR"
        sInstrumentSpec(SWITCH1) = "VXI::38::INSTR"
        sInstrumentSpec(DMM) = "VXI::44::INSTR"
        sInstrumentSpec(ARB) = "VXI::48::INSTR"

        sInstrumentDescription(OSCOPE) = "ZTEC 1428 Digitizing Oscilloscope (SC Slot 1)"
        sInstrumentDescription(DMM) = "Racal Instruments 4152A DMM (PC Slot 11)"
        sInstrumentDescription(COUNTER) = "HP E1420B Counter/Timer (SC Slot 2)"
        sInstrumentDescription(ARB) = "HP E1445A Arbitrary Waveform Gen (PC Slot 12)"
        sInstrumentDescription(DIGITAL) = "Teradyne M910 Digital Subsystem "
        sInstrumentDescription(FGEN) = "Racal Instruments 3152A Function Gen (SC Slot 3)"

        sInstrumentDescription(RFPM) = "Phase Matrix 1313B RF Power Meter (SC Slot 7)"
        sInstrumentDescription(RFSYN) = "Phase Matrix 1140B RF Synthesizer (SC Slots 4-6)"
        sInstrumentDescription(RFMEAS) = "PM 1313B,20309, VXITech VM2601IF,VM7510 (SC Slots7-9)"
        sInstrumentDescription(DOWNCONV) = "Phase Matrix 1313B-D7 (SC Slot 7)"
        sInstrumentDescription(LOCALOSC) = "Phase Matrix 20309 (SC Slot 8)"
        sInstrumentDescription(DIGITIZER) = "VXITech VM2601IF (SC Slot 9)"
        sInstrumentDescription(CALIBRATOR) = "VXITech VM7510 (SC Slot 9)"

        sInstrumentDescription(OSCOPE) = "ZTEC 1428 Digitizing Oscilloscope (SC Slot 1)"
        sInstrumentDescription(PPU) = "Freedom Power Systems FPU/PPU"
        sInstrumentDescription(SWITCH1) = "Racal 1260-39,OPT1 LF Sw #1 (PC Slot 5)"
        sInstrumentDescription(SWITCH2) = "Racal 1260-39 LF Sw #2 (PC Slot 6)"
        sInstrumentDescription(SWITCH3) = "Racal 1260-38T LF Sw #3 (PC Slot 7)"
        sInstrumentDescription(SWITCH4) = "Racal 1260-58 MF Sw (PC Slot 8)"
        sInstrumentDescription(SWITCH5) = "Racal 1260-67A HF Sw (PC Slot 9)"
        sInstrumentDescription(MIL_STD_1553) = "DDC BU-65569i1, MIL-STD-1553 I/F"
        sInstrumentDescription(SYN_RES) = "North Atlantic Instruments 65CS4 (SC Slot 12)"
        sInstrumentDescription(APROBE) = "Analog Probe"
        sInstrumentDescription(EO_VCC) = "Coreco Imaging PC2-Vision Module (IC Slot 6)"
        sInstrumentDescription(VEO2) = "SBIR 93006G7100 ViperT EO"
        sInstrumentDescription(VEO_LARRS) = "SBIR LARRS"

        'VIPERT.INI keys for sections  [Serial Number], [Calibration]
        sInstFileName(DIGITAL) = "DTS"
        sInstFileName(SWITCH1) = "LFS1"
        sInstFileName(SWITCH2) = "LFS2"
        sInstFileName(SWITCH3) = "LFS3"
        sInstFileName(SWITCH4) = "MFS"
        sInstFileName(SWITCH5) = "HFS"
        sInstFileName(DMM) = "DMM"
        sInstFileName(ARB) = "ARB"
        sInstFileName(COUNTER) = "UCT"
        sInstFileName(FGEN) = "FG"

        sInstFileName(RFMEAS) = "RFM"
        sInstFileName(DOWNCONV) = "RFD"
        sInstFileName(LOCALOSC) = "RFLO"
        sInstFileName(DIGITIZER) = "RFDIG"

        sInstFileName(RFSYN) = "RFS"
        sInstFileName(RFPM) = "RFP"
        sInstFileName(OSCOPE) = "DSCOPE"
        sInstFileName(PPU) = "UUTPS"
        sInstFileName(MIL_STD_1553) = "MIL1553"
        sInstFileName(EO_VCC) = "EOVC"
        sInstFileName(VEO2) = "EO_LASER"

        sInstFileName(RS422) = "RS422"
        sInstFileName(RS232) = "RS232"
        sInstFileName(GIGA) = "GIGABIT ETHERNET"
        sInstFileName(SYN_RES) = "SYN_RES"

        'Support Instrument Identification for Failure Step in the FHDB
        sInstrumentIDCode(PPU) = "PPU"
        sInstrumentIDCode(MIL_STD_1553) = "BUS"
        sInstrumentIDCode(DMM) = "DMM"
        sInstrumentIDCode(COUNTER) = "C/T"
        sInstrumentIDCode(OSCOPE) = "DSO"
        sInstrumentIDCode(FGEN) = "FGN"
        sInstrumentIDCode(ARB) = "ARB"
        sInstrumentIDCode(APROBE) = "APR"
        sInstrumentIDCode(DIGITAL) = "DTS"
        sInstrumentIDCode(SWITCH1) = "LF1"
        sInstrumentIDCode(SWITCH2) = "LF2"
        sInstrumentIDCode(SWITCH3) = "LF3"
        sInstrumentIDCode(SWITCH4) = "MFS"
        sInstrumentIDCode(SWITCH5) = "HFS"
        sInstrumentIDCode(SYN_RES) = "S/R"
        sInstrumentIDCode(RFSYN) = "RST"
        sInstrumentIDCode(RFMEAS) = "RMA"
        sInstrumentIDCode(RFPM) = "RPM"
        sInstrumentIDCode(EO_VCC) = "EVC"
        sInstrumentIDCode(VEO2) = "EMF"

        sInstrumentIDCode(RS422) = "422"
        sInstrumentIDCode(RS232) = "232"
        sInstrumentIDCode(GIGA) = "GBE"


        'bbbb Added to Support CICL
        ResourceName(PPU) = "DCP_1"
        ResourceName(MIL_STD_1553) = "MIL1553_1"
        ResourceName(DMM) = "DMM_1"
        ResourceName(COUNTER) = "CNTR_1"
        ResourceName(OSCOPE) = "DSO_1"
        ResourceName(FGEN) = "FUNC_GEN_1"
        ResourceName(ARB) = "ARB_GEN_1"
        ResourceName(APROBE) = "PROBE"
        ResourceName(DIGITAL) = "DWG_1"
        ResourceName(SWITCH1) = "PAWS_SWITCH"
        ResourceName(SWITCH2) = "PAWS_SWITCH"
        ResourceName(SWITCH3) = "PAWS_SWITCH"
        ResourceName(SWITCH4) = "PAWS_SWITCH"
        ResourceName(SWITCH5) = "PAWS_SWITCH"
        ResourceName(SYN_RES) = "SRS_1_DS1"
        ResourceName(RFSYN) = "RFGEN_1"
        ResourceName(RFMEAS) = "RF_MEASAN_1"
        ResourceName(RFPM) = "RF_PWR_1"

        ResourceName(RS422) = "COM_1"
        ResourceName(RS232) = "COM_2"
        ResourceName(GIGA) = "ETHERNET_1"

        PsResourceName(1) = "DCPS_1"
        PsResourceName(2) = "DCPS_2"
        PsResourceName(3) = "DCPS_3"
        PsResourceName(4) = "DCPS_4"
        PsResourceName(5) = "DCPS_5"
        PsResourceName(6) = "DCPS_6"
        PsResourceName(7) = "DCPS_7"
        PsResourceName(8) = "DCPS_8"
        PsResourceName(9) = "DCPS_9"
        PsResourceName(10) = "DCPS_10"
    End Sub

    ''' <summary>
    ''' This Routine is a pass through to the VISA layer using VB conventions to facilitate 
    ''' clean Word Serial read communications to message based instruments.
    ''' </summary>
    ''' <param name="instrument">the index of the instrument from which to read</param>
    ''' <param name="ReturnMessage">the string returned to VB</param>
    ''' <returns>A long integer representing any errors that may occur</returns>
    ''' <remarks></remarks>
    Function ReadMsg(ByRef instrument As Integer, ByRef ReturnMessage As String) As Integer
        Dim ReadBuffer As String

        Dim retCount As Integer
        ReadBuffer = ""
        System.Windows.Forms.Application.DoEvents()
        ReturnMessage = Space(256)
        nSystErr = atxml_ReadCmds(ResourceName(instrument), ReturnMessage, 256, retCount)
        '    nSystErr = atxmlDF_viRead(ResourceName(Instrument), 0, ReturnMessage, 256, retCount)

        System.Windows.Forms.Application.DoEvents()
        ReturnMessage = Left(ReturnMessage, retCount)
        ReturnMessage = StripNullCharacters(ReturnMessage)
        ReadMsg = nSystErr

        'bbbb deleted
        ''
        ''  'DESCRIPTION:
        ''  '   This Routine is a pass through to the VISA layer using VB conventions to
        ''  '   facilitate clean Word Serial read communications to message based instruments.
        ''  'PARAMETERS:
        ''  '   instrumentIndex = the index of the instrument from which to read
        ''  '   ReturnMessage = the string returned to VB
        ''  'RETURNS:
        ''  '   A long integer representing any errors that may occur
        ''
        ''  Dim errorMessage, ihandle, Dummy, seconds, lRetCount
        ''  Dim sReadBuffer As String * 256
        ''
        ''
        ''  ihandle = nInstrumentHandle(instrumentIndex)
        ''  sReadBuffer = ""
        ''  DoEvents
        ''  lSystErr = viRead(ihandle, sReadBuffer, 256, lRetCount)
        ''  DoEvents
        ''  Select Case lSystErr
        ''    Case VI_SUCCESS
        ''      ReturnMessage = StripNullCharacters(sReadBuffer)
        ''    Case VI_ERROR_TMO
        ''
        ''    Case Else
        ''    Dummy = viStatusDesc(ihandle, lSystErr, lpBuffer)
        ''    Echo "VISA Error Message:" & vbCrLf & StripNullCharacters(lpBuffer)
        ''    MsgBox "Error Message:" & vbCrLf & StripNullCharacters(lpBuffer), vbCritical, "VISA Error Message"
        ''  End Select
        ''
        ''  ReadMsg = lSystErr
    End Function

    ''' <summary>
    ''' This Routine is a pass through to the VISA layer using VB conventions to facilitate clean Word Serial 
    ''' read communications to message based instruments.
    ''' </summary>
    ''' <param name="iHandle">handle of the instrument to be read</param>
    ''' <param name="ReturnMessage">the string returned to VB</param>
    ''' <returns>A long integer representing any errors that may occur</returns>
    ''' <remarks></remarks>
	Function ReadMsg2(ByRef iHandle As Integer, ByRef ReturnMessage As String) As Integer
		Dim ReadBuffer As String
        Dim retCount As Integer
		
		ReadBuffer = ""
		System.Windows.Forms.Application.DoEvents()
		ReturnMessage = Space(256)
        '    nSystErr = atxml_ReadCmds(ResourceName(Instrument), ReturnMessage, 256, retCount)
        '    nSystErr = atxmlDF_viRead(ResourceName(Instrument), 0, ReturnMessage, 256, retCount)
		nSystErr = viRead(iHandle, ReturnMessage, 256, retCount)
		
		System.Windows.Forms.Application.DoEvents()
		ReturnMessage = Left(ReturnMessage, retCount)
		ReturnMessage = StripNullCharacters(ReturnMessage)
		ReadMsg2 = nSystErr
    End Function
	

    ''' <summary>
    ''' This Routine is a pass through to the VISA layer using VB conventions to facilitate 
    ''' clean Word Serial write communications to message based instruments.
    ''' </summary>
    ''' <param name="instrument">the handle to the instrument to write</param>
    ''' <param name="MessageToSend">the string of data to be written</param>
    ''' <remarks></remarks>
    Sub WriteMsg(ByVal instrument As Integer, ByRef MessageToSend As String)
        Dim retCount As Integer
'        Dim errorMessage As String

        'bbbb added
        System.Windows.Forms.Application.DoEvents()
        lSystErr = atxml_WriteCmds(ResourceName(instrument), MessageToSend, retCount)
        If lSystErr <> 0 Then
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(instrument) & ".")
            Echo("COMMAND: " & MessageToSend)
            MsgBox("Error sending:" & vbCrLf & MessageToSend & vbCrLf & "See Log File for additional information.", MsgBoxStyle.Exclamation, "WRITEMSG Function Error")
            iWriteMsgError = True
        End If

        'bbbb deleted
        ''  'DESCRIPTION:
        ''  '   This Routine is a pass through to the VISA layer using VB conventions to
        ''  '   facilitate clean Word Serial write communications to message based instruments.
        ''  'PARAMETERS:
        ''  '   IHandle = the handle to the instrument to write
        ''  '   MessageToSend = the string of data to be written
        ''  'EXAMPLE:
        ''  '   WriteMsg DMMHandle, "*TST?"
        ''
        ''  Dim errorMessage, ihandle, lRetCount
        ''
        ''  iWriteMsgError = 0
        ''
        ''  DoEvents
        ''  If nInstrumentHandle(instrumentIndex) = 0 Then
        ''    Exit Sub
        ''  End If
        ''  ihandle = nInstrumentHandle(instrumentIndex)
        ''
        ''  lSystErr = viWrite(ihandle, MessageToSend, Len(MessageToSend), lRetCount)
        ''  If lSystErr Then
        ''    lSystErr = viStatusDesc(ihandle, lSystErr, lpBuffer)
        ''    Echo "WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(instrumentIndex) & "."
        ''    Echo "COMMAND: " & MessageToSend
        ''    MsgBox "Error sending:" & vbCrLf & MessageToSend & vbCrLf & "Error:" & _
        ''    errorMessage & vbCrLf & "See Log File for additional information.", vbExclamation, "WRITEMSG Function Error"
        ''    iWriteMsgError = True
        ''  End If
    End Sub
	
	Function nSizeOf(ByRef AnyArray As Object) As Integer
		'Returns number of elements in an array
		nSizeOf = UBound(AnyArray) - LBound(AnyArray) + 1
	End Function

    ''' <summary>
    ''' This Module converts a hex digit to a binary string
    ''' </summary>
    ''' <param name="HexChars">The Hex character</param>
    ''' <returns>The Binary string</returns>
    ''' <remarks></remarks>
    Public Function ConvertHexChrToBin(ByRef HexChars As String) As String
        Dim Conv As String

        Select Case HexChars
            Case "0"
                Conv = "0000"
            Case "1"
                Conv = "0001"
            Case "2"
                Conv = "0010"
            Case "3"
                Conv = "0011"
            Case "4"
                Conv = "0100"
            Case "5"
                Conv = "0101"
            Case "6"
                Conv = "0110"
            Case "7"
                Conv = "0111"
            Case "8"
                Conv = "1000"
            Case "9"
                Conv = "1001"
            Case "A"
                Conv = "1010"
            Case "B"
                Conv = "1011"
            Case "C"
                Conv = "1100"
            Case "D"
                Conv = "1101"
            Case "E"
                Conv = "1110"
            Case "F"
                Conv = "1111"
            Case Else
                Conv = "0000"
        End Select
        ConvertHexChrToBin = Conv

    End Function
	
    Sub WriteSW(ByRef cmd As String)
        'Dim count As Integer
        'Dim ErrorFlag As Integer
        'Dim InstrumentError As Short
        'Dim ErrorStatus As Integer
        'Dim sReadBuffer As String

        If bSimulation Then Exit Sub

        WriteMsg(SWITCH1, cmd)

        '    viWrite nInstrumentHandle(TETS.SWITCH1), Command, CLng(Len(Command)), count

        Select Case UCase(Left(cmd, 2))
            Case "TE", "YE", "PD", "PS"
            Case "RE"
                If UCase(Left(cmd, 3)) = "RES" Then
                    Delay(0.1)
                End If
            Case Else
                '            InstrumentError = False
                '            Do
                '                sReadBuffer = ""
                '                ErrorStatus = Sw_Error_Query(nInstrumentHandle(TETS.SWITCH1), ErrorFlag, ReadBuffer)
                '                If ErrorFlag = 0 Then Exit Do
                '                InstrumentError = True
                '                Echo "INSTRUMENT ERROR: " & DecodeErrorMessage(ReadBuffer)
                '            Loop
        End Select
    End Sub

    ''' <summary>
    ''' This Routine is a pass through to the VISA layer using VB conventions to fascilitate clean Word 
    ''' Serial write communications to message based instruments.
    ''' </summary>
    ''' <param name="iHandle">handle of the instrument to write</param>
    ''' <param name="MessageToSend">the string of data to be written</param>
    ''' <remarks></remarks>
	Sub WriteMsg2(ByRef iHandle As Integer, ByRef MessageToSend As String)
        Dim retCount As Integer
		
		System.Windows.Forms.Application.DoEvents()
		nSystErr = viWrite(iHandle, MessageToSend, Len(MessageToSend), retCount)
		
		'    bbbb need to get this error trapping working
		'    If nSystErr Then
		'      DisplayErrorMessage nSystErr
		'    Else
        '      If InStr(MessageToSend, "?") = 0 Then     ' If NOT expecting a response
		'        Do
        '          ErrorStatus = hpe1445a_errorQuery(instrumentHandle, ErrorCode, ReadBuffer)
		'          If ErrorCode = 0 Then Exit Do
        '          UserError = True
        '          MsgBox "Error Code: " & ErrorCode & vbCrLf & ReadBuffer, 48, ResourceName & "Error Message"
		'        Loop
		'      End If
		'    End If
    End Sub
	
	Sub DisplayErrorMessage(ByRef ErrorStatus As Integer)
        If ErrorStatus < 0 Then
            '        Error = Sw_Error_Message(InstrumentHandle, ErrorStatus, ReadBuffer)
            '        MsgBox ReadBuffer, 48, "VISA Error Message"
        End If
	End Sub

	Function ReadSW() As String
        Dim ReadBuffer As String = Space(255)
		Dim CharsRead As Integer
        Dim ReadString As String = ""
		Dim ErrorStatus As Integer
		
        If bSimulation Then
            ReadSW = ""
            Exit Function
        End If
		
		Do 
            'bbbb   ErrorStatus = viRead(nInstrumentHandle(TETS.SWITCH1), ReadBuffer, 255, CharsRead)
			ErrorStatus = atxml_ReadCmds(ResourceName(SWITCH1), ReadBuffer, 255, CharsRead)
			ReadString = ReadString & Left(ReadBuffer, CharsRead)
		Loop While CharsRead = 255
		ReadString = StripNonPrintChar(ReadString)
		ReadSW = ""
		ReadSW = ReadString
    End Function
	
	Public Function DecodeErrorMessage(ByRef Msg As String) As String 'Note only called from WriteSW()
		Dim SourceOfError, ErrorText As String
		Dim ErrorNum As Short
		
		If (Len(Msg) < 12) Or (InStr(Msg, ".") = 0) Then
			DecodeErrorMessage = "Error decoding error message"
			Exit Function
		End If
		
		SourceOfError = Mid(Msg, InStr(Msg, ".") - 3, 3)
		Select Case SourceOfError
			Case "XXX" : SourceOfError = ""
			Case "000" : SourceOfError = "1260 Switch Controller"
			Case "001" : SourceOfError = "1260-39 LF-1 1-Wire Switch Module"
			Case "002" : SourceOfError = "1260-39 LF-2 1-Wire Switch Module"
			Case "003" : SourceOfError = "1260-38T LF-3 2-Wire Switch Module"
			Case "004" : SourceOfError = "1260-58 MF Coaxial Switch Module"
			Case "005" : SourceOfError = "1260-66A HF Coaxial Switch Module"
			Case Else : SourceOfError = "Unknown Switch Module (Module Address: " & SourceOfError & ")"
        End Select

		If SourceOfError <> "" Then
			SourceOfError = "Error reported from: " & SourceOfError & vbCrLf
		End If
		
		ErrorNum = Val(Mid(Msg, InStr(Msg, ".") + 1, 2))
		Select Case ErrorNum
			Case 0 : ErrorText = "No error"
			Case 1 : ErrorText = "Invalid module number specified"
			Case 2 : ErrorText = "Specified module not installed"
			Case 3 : ErrorText = "Invalid channel number specified"
			Case 4 : ErrorText = "Invalid port number specified"
			Case 5 : ErrorText = "Command syntax error"
			Case 6 : ErrorText = "Read value larger than expected"
			Case 7 : ErrorText = "Function not supported by module"
			Case 8 : ErrorText = "Expected line terminator not found"
			Case 9 : ErrorText = "Valid command not found"
			Case 20 : ErrorText = "Exclude list too long"
			Case 21 : ErrorText = "Channel entered on exclude list twice"
			Case 22 : ErrorText = "Module doesn't allow exclude function"
			Case 23 : ErrorText = "Scan list too long"
			Case 24 : ErrorText = "Module doesn't allow scan"
			Case 25 : ErrorText = "Equate list too long"
			Case 26 : ErrorText = "Module entered on scan list twice"
			Case 27 : ErrorText = "Incompatible modules equated or digital modules invalid"
			Case 31 : ErrorText = "SRQMASK invalid"
			Case 40 : ErrorText = "Number invalid as a test number"
			Case 41 : ErrorText = "RAM test failure"
			Case 42 : ErrorText = "ROM test failure"
			Case 43 : ErrorText = "Non-vol memory test failure"
			Case 44 : ErrorText = "Incompatible operating system EPROMS, software revisions"
			Case 45 : ErrorText = "Self-test CPU or related circuitry failure"
			Case 46 : ErrorText = "Self-test 24 V supply failure"
			Case 47 : ErrorText = "Self-test timer chip failure"
			Case 48 : ErrorText = "Insufficient RAM for option module"
			Case 49 : ErrorText = "Checksum error reading from option module EPROM"
			Case 50 : ErrorText = "Option module EPROM incompatible with CPU EPROM"
			Case 51 : ErrorText = "Failed confidence test"
			Case 55 : ErrorText = "Error STOREing to non-vol memory"
			Case 56 : ErrorText = "Error REDALLing from non-vol memory"
			Case 57 : ErrorText = "Non-vol storage location number out of range"
			Case 58 : ErrorText = "RECALLing from an empty non-vol location not allowed"
			Case 59 : ErrorText = "Non-vol memory initialization required"
			Case Else : ErrorText = "Unknown Error Code"
		End Select
        ErrorText = "Error Number: " & ErrorNum.ToString("00") & vbCrLf & "Error Message: " & ErrorText
		
		DecodeErrorMessage = SourceOfError & ErrorText
    End Function

    ''' <summary>
    ''' This routine strips trailing characters with ASCII values less than 32 from the end of a string
    ''' </summary>
    ''' <param name="Parsed">String from which to remove null characters</param>
    ''' <returns>A the reultant parsed string</returns>
    ''' <remarks></remarks>
    Function StripNonPrintChar(ByRef Parsed As String) As String
        Dim i As Short

        For i = Len(Parsed) To 1 Step -1
            If Asc(Mid(Parsed, i, 1)) > 32 Then
                Exit For
            End If
        Next i
        StripNonPrintChar = Left(Parsed, i)
    End Function
	
	Public Sub DownLoadSegment(ByRef sSegmentFileName As String, ByRef sLineOfText As Object)
		'Used with Arb
    End Sub
End Module