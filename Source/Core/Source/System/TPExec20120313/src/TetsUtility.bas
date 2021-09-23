Attribute VB_Name = "TETS"
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
Declare Function aps6062_init Lib "APS6062.DLL" (ByVal ResourceNameX$, ByVal idQuery%, ByVal resetDevice%, instrumentHandle&) As Long
Declare Function terM9_init Lib "terM9_32.dll" (ByVal rsrcName As String, ByVal idQuery As Integer, ByVal resetInstr As Integer, vi As Long) As Long
Declare Function terM9_reset Lib "terM9_32.dll" (ByVal vi As Long) As Long
Declare Function terM9_close Lib "terM9_32.dll" (ByVal vi As Long) As Long

Public Const SEEDED_GUIDED_PROBE = 1
Public Const FAULT_DICTIONARY_ONLY = 2
Public Const GUIDED_PROBE_ONLY = 3
Public Const MAX_DIAG_TEXT_SIZE = 4096

Public Const LOG_FILE = "C:\APS\DATA\FAULT-FILE"
Public Const SYSTEM_LOG = "C:\TETS\SYSLOG\SYSLOG.EXE"
Public Const TETS_INI = "VIPERT.INI"
    
Public Const MANUAL = -1

Public sInstrName()
Public Const MIL_STD_1553 = 1
Public Const DIGITAL = 2
Public Const DMM = 3
Public Const OSCOPE = 4
Public Const COUNTER = 5
Public Const ARB = 6
Public Const FGEN = 7
Public Const PPU = 8
Public Const APROBE = 9
Public Const SWITCH1 = 10:  Public Const LFSWITCH1 = 1: Public Const SWITCH_CONTROLLER = 0
Public Const SWITCH2 = 11:  Public Const LFSWITCH2 = 2
Public Const SWITCH3 = 12:  Public Const PWRSWITCH = 3
Public Const SWITCH4 = 13:  Public Const MFSWITCH = 4
Public Const SWITCH5 = 14:  Public Const RFSWITCH = 5

Public Const SYN_RES = 15
Public Const RS422 = 16
Public Const RS232 = 17
Public Const GIGA = 18

Public Const LAST_CORE_INST = GIGA

Public Const RFPM = 19
Public Const RFCOUNTER = 20
Public Const RFREC = 21
Public Const MOD_ANAL = 22
Public Const RFSYN = 23
Public Const RFMEAS = 24
Public Const LAST_RF_INST = RFSYN

Public Const EO_VC = 25
Public Const EO_MF = 26
Public Const EO_IR = 27
Public Const EO_VIS = 28
Public Const EO_MS = 29
Public Const EO_VCC = 30
Public Const VEO2 = 31
Public Const LAST_EO_INST = VEO2
Public Const LAST_INSTRUMENT = LAST_EO_INST

'Added to Identify specific RFMA instruments
Public Const DOWNCONV = LAST_INSTRUMENT + 1   ' 32
Public Const LOCALOSC = LAST_INSTRUMENT + 2   ' 33
Public Const DIGITIZER = LAST_INSTRUMENT + 3  ' 34
Public Const CALIBRATOR = LAST_INSTRUMENT + 4 ' 35
Public Const VEO_LARRS = LAST_INSTRUMENT + 5  ' 36
  

Public bSimulation As Boolean
Public bLiveMode As Boolean
Public nSessionHandle As Long
Public lSystErr As Long
Public TPS_Development As Boolean
Public bTimedOut As Boolean   'This flag is set when an instrument does not respond to
                              ' a measurement query within that instruments TMO time.
                              ' It is initialized to False by 'iViperTinit'.
Public Const MAX_LOW = -1E+300  '\__/ Used to replace 'dMeasured' values when
Public Const MAX_HIGH = 1E+300  '/  \ error conditions exist

Public iWriteMsgError As Integer    'Instrument Error code set by various WriteMsg functions.

Public bInstrRequired() As Boolean
Public sInstFileName(LAST_INSTRUMENT + 8) As String
Public nInstrumentHandle(LAST_INSTRUMENT) As Long
Public sIDNResponse(LAST_INSTRUMENT) As String
Public sInstrumentSpec(LAST_INSTRUMENT) As String
Public sInstrumentDescription(LAST_INSTRUMENT + 8) As String
Public iInstrumentInitialized(LAST_INSTRUMENT) As String
Public sInstrumentIDCode(LAST_INSTRUMENT) As String
Public nSupplyHandle(1 To 10) As Long
Public bRFOptionInstalled As Boolean
Public bEoOptionInstalled As Boolean
Public iLastInstrument As Integer
Public ReadBuffer As String * 256
Public lBytesRead As Long
Public GIGA_DOWN As Boolean
Public bSwitchCardOK(RFSWITCH) As Boolean
Public sIddata() As String
Public sIDList(5) As String


Sub ResetSystem()
    'DESCRIPTION:
    '   This routine resets the system.
    'PARAMETERS:
    '   None
    Dim nSupply As Long
    Dim nInitStatus As Long
    Dim SystErr&
    
    bReseting = True
    If bSimulation Then
        ShowMainMenu
        bReseting = False
        Exit Sub
    End If
    
    If bInstrRequired(PPU) Then
        iInitDCPS bReset:=True
    End If

    If bRFOptionInstalled Then
      nSystErr = atxmlDF_viClear(ResourceName(RFPM), 0&)
      nSystErr = atxmlDF_viClear(ResourceName(RFSYN), 0&)
      nSystErr = atxmlDF_viClear(ResourceName(RFMEAS), 0&)
    End If
    
    WriteMsg DMM, "*RST; *CLS"
    WriteMsg COUNTER, "*RST; *CLS"
    WriteMsg ARB, "*RST; *CLS"
    WriteMsg OSCOPE, "*RST; *CLS"
    WriteMsg OSCOPE, "SYST:LANG COMP"
    WriteMsg FGEN, "*RST; *CLS"
    WriteMsg SWITCH1, "RESET"
    
    If bInstrRequired(DMM) Then SystErr& = atxmlDF_viClear(ResourceName(DMM), 0&)
    If bInstrRequired(COUNTER) Then SystErr& = atxmlDF_viClear(ResourceName(COUNTER), 0&)
    If bInstrRequired(ARB) Then SystErr& = atxmlDF_viClear(ResourceName(ARB), 0&)
    If bInstrRequired(RFSYN) Then SystErr& = atxmlDF_viClear(ResourceName(RFSYN), 0&)
    If bInstrRequired(OSCOPE) Then SystErr& = atxmlDF_viClear(ResourceName(OSCOPE), 0&)
    If bInstrRequired(FGEN) Then SystErr& = atxmlDF_viClear(ResourceName(FGEN), 0&)
    If bInstrRequired(SWITCH1) Then SystErr& = atxmlDF_viClear(ResourceName(SWITCH1), 0&)

    If bInstrRequired(DMM) Then WriteMsg DMM, "*RST; *CLS"
    If bInstrRequired(COUNTER) Then WriteMsg COUNTER, "*RST; *CLS"
    If bInstrRequired(ARB) Then WriteMsg ARB, "*RST; *CLS"
    If bInstrRequired(RFSYN) Then WriteMsg RFSYN, "*RST"
    If bInstrRequired(RFSYN) Then WriteMsg RFSYN, "*CLS"
    If bInstrRequired(OSCOPE) Then WriteMsg OSCOPE, "*RST; *CLS"
    If bInstrRequired(FGEN) Then WriteMsg FGEN, "*RST; *CLS"
    If bInstrRequired(SWITCH1) Then
        WriteMsg SWITCH1, "RESET"
        Delay 1
        
        If Not bSimulation Then
            WriteMsg SWITCH1, "CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001"
        End If
    End If
    
    If bRFOptionInstalled Then
      WriteMsg RFPM, "*RST; *CLS"
      WriteMsg RFSYN, "*RST"
      WriteMsg RFSYN, "*CLS"
    End If
    
    If bInstrRequired(DIGITAL) Then
      nInitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, nInstrumentHandle(DIGITAL))
      nSystErr = terM9_reset(nInstrumentHandle(DIGITAL))
      nInitStatus = terM9_close(nInstrumentHandle(DIGITAL))
    End If
    
    bReseting = False
    
    
End Sub

Sub ReleaseSystem()

    Dim InstrumentToClose%
    Dim SupplyCount%
    Dim ErrorStatus&
    Dim FileError&
    Dim SystemLogSpec$
    Dim FileHandle&
    Dim TaskID&
    Dim count%
    
    On Error Resume Next
    
    If nInstrumentHandle&(DIGITAL) <> 0 Then
        nSystErr = terM9_close(nInstrumentHandle&(DIGITAL))
    End If
    nInstrumentHandle&(DIGITAL) = 0
    
    'Return Instrument Handles
    For InstrumentToClose% = 0 To LAST_INSTRUMENT
        If nInstrumentHandle&(InstrumentToClose%) <> 0 Then
            nSystErr = viClose(nInstrumentHandle&(InstrumentToClose%))
        End If
    Next InstrumentToClose%
    
    'Return PPU Handles
    For InstrumentToClose% = 1 To 10
        If nSupplyHandle(InstrumentToClose%) <> 0 Then
           nSystErr = viClose(nSupplyHandle(InstrumentToClose%))
        End If
    Next InstrumentToClose%
    On Error Resume Next
    
    lSystErr = viClose(nSessionHandle)
    
    ErrorStatus& = atxml_Close()
    Delay 0.5
    Echo "Program End"
    Close ' close all open files
    ' END OF PROGRAM (DLL)

End Sub
Function StripNullCharacters(ByRef Parsed$) As String
    'DESCRIPTION:
    '   This routine strips contiguous characters with ASCII values less
    '   than 33 from the end of a string.
    '   The original string doesn't actually get stripped.
    'PARAMETERS:
    '   Parsed$:    String from which to remove null characters
    'RETURNS:
    '   The resultant parsed string
    
    Dim x As Integer
    
    For x = Len(Parsed$) To 1 Step -1
        If Asc(Mid$(Parsed$, x, 1)) > 32 Then
            Exit For
        End If
    Next x
    StripNullCharacters = Left$(Parsed$, x)
End Function

Function iTETSInit() As Integer
   
    'DESCRIPTION:
    ' This function checks to see if the ViperT System Software is installed and
    ' and SHOULD be run at start-up of TPS.
    'RETURNS:
    ' True if the ViperT System Software is installed
    ' False if the ViperT System Software is not installed
    'EXAMPLE:
    '  SystemInit& = MDSInstalled()
    
    Dim iResponse As Integer
    Dim iInstrument As Long
    TETSVar
    bTimedOut = False
    ClearTestMeasurementInformation
    frmMain.txtModule.Text = "System Init"
    frmMain.txtTestName.Text = "VISA Check"
    frmMain.txtStep.Text = "N/A"
    frmMain.txtInstrument.Text = "N/A"
    frmMain.txtCommand.Text = "N/A"
    frmMain.txtUpperLimit.Text = "TRUE"
    frmMain.txtLowerLimit.Text = "TRUE"
    frmMain.txtUnit.Text = "BOOLEAN"

    '*********** TEST NAME AND PRINT OUT HEADLINE **********
    Echo "**** System Initialization ****"
    

   'Verify presence of system
    iTETSInit = True
    bLiveMode = True
    lpBuffer$ = ""
    lSystErr = GetSystemDirectory(lpBuffer$, ByVal 80)
    SystemDir$ = StripNullCharacters(lpBuffer$)
    lpBuffer$ = ""
    lSystErr = GetWindowsDirectory(lpBuffer$, ByVal 80)
    WindowsDirectory$ = StripNullCharacters(lpBuffer$) & "\"
    VisaLibrary$ = SystemDir$ & "\VISA32.DLL"
    
    If Not bFileExists(VisaLibrary$) Then
      frmMain.txtMeasured.Text = "FALSE"
      iTETSInit = False
      bLiveMode = False
      nSessionHandle = 0
      Echo "SYSTEM ERROR - Unable to initialize VIPERT System. Can't find VISA32.DLL."
      Exit Function
    Else
      frmMain.txtMeasured.Text = "TRUE"
    End If

    frmMain.txtTestName.Text = "Open Default RM"
    frmMain.txtMeasured.Text = ""
    lSystErr = viOpenDefaultRM&(nSessionHandle)
    If lSystErr < 0 Then
      frmMain.txtMeasured.Text = "FALSE"
      iTETSInit = False
      bLiveMode = False
      nSessionHandle = 0
      Echo "SYSTEM ERROR - Unable to initialize ViperT System. Can't obtain VISA session handle."
      Exit Function
    Else
      frmMain.txtMeasured.Text = "TRUE"
    End If
    
    'Check for RF Instruments
    frmMain.txtTestName.Text = "Check RF Variant"
    frmMain.txtUpperLimit.Text = "N/A"
    frmMain.txtLowerLimit.Text = "N/A"
    frmMain.txtMeasured.Text = ""
    iLastInstrument = LAST_CORE_INST
    bRFOptionInstalled = bCheckOptionInstalled("RF")
    If bRFOptionInstalled Then
        frmMain.txtMeasured.Text = "TRUE"
        iLastInstrument = LAST_RF_INST
        'Check RF STIM Type
        If Not bCheckOptionInstalled("RFSTIM") Then
            sIDNResponse(RFSYN) = "1140B"
            sInstrumentDescription(RFSYN) = "PM 1140B RF Stimulus"
        End If
    Else
          frmMain.txtMeasured.Text = "FALSE"
    End If
    
    frmMain.txtTestName.Text = "Check VXI Power"
    frmMain.txtUpperLimit.Text = "TRUE"
    frmMain.txtLowerLimit.Text = "TRUE"
    frmMain.txtMeasured.Text = ""
    If Not VXIPowerCheck% Then
      frmMain.txtMeasured.Text = "FALSE"
      iTETSInit = False
      bLiveMode = False
      nSessionHandle = 0
      Echo "SYSTEM ERROR - Unable to initialize VIPERT System. VXI Power off."
      Exit Function
    Else
        frmMain.txtMeasured.Text = "TRUE"
    End If

    'moved from above to this place 5/20/03 Soon Nam
    UpdateProgress (0)  'Initialize progress bar on shell screen

    '**** Initialize Instruments ****
    '********************************
    For iInstrument = 1 To iLastInstrument
        If bInstrRequired(iInstrument) Then
            frmMain.txtTestName.Text = "Init. " & sInstFileName(iInstrument)
            Select Case iInstrument&
                Case RFPM, RFCOUNTER
                  iInstrumentInitialized(iInstrument) = True
                Case DMM, COUNTER, ARB, FGEN, OSCOPE, RFSYN, RFMEAS
                    If Not InitMessageBasedInstrument(iInstrument) Then
                            frmMain.txtMeasured.Text = "FALSE"
                            iTETSInit = False
                            bLiveMode = False
                            Echo "INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & "."
                            Exit Function
                    End If
                Case SWITCH1, SWITCH2, SWITCH3, SWITCH4, SWITCH5
                    If Not INITInst.iInitSwitching(iInstrument) Then
                            frmMain.txtMeasured.Text = "FALSE"
                            iTETSInit = False
                            bLiveMode = False
                            Echo "INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & "."
                            Exit Function
                    Else
                        iInstrumentInitialized(iInstrument) = True
                    End If
                Case DIGITAL
                    If Not INITInst.iInitDigital(True) Then
                            frmMain.txtMeasured.Text = "FALSE"
                            iTETSInit = False
                            bLiveMode = False
                            Echo "INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & "."
                            Exit Function
                    Else
                        iInstrumentInitialized(iInstrument) = True
                    End If
                Case MIL_STD_1553
                    iInstrumentInitialized(iInstrument) = True
                    frmMain.txtMeasured.Text = "TRUE"
                Case PPU
                    If Not INITInst.iInitDCPS Then
                            frmMain.txtMeasured.Text = "FALSE"
                            iTETSInit = False
                            bLiveMode = False
                            Echo "INSTRUMENT ERROR - Unable to initialize " & sInstrumentDescription(iInstrument) & "."
                            Exit Function
                    Else
                        iInstrumentInitialized(iInstrument) = True
                    End If
            End Select
            frmMain.txtMeasured.Text = "TRUE"
            DoEvents
        End If
        UpdateProgress ((Round((iInstrument / iLastInstrument), 2)) * 100)
    Next iInstrument
    Echo ""
End Function

Function bFileExists(Path$) As Boolean
'************************************************************
'* ManTech Test Systems Software Module                     *
'************************************************************
'* Written By     : David W. Hartley                        *
'*    DESCRIPTION:                                          *
'*     This Module Checks To See If A Disk File Exists      *
'*    EXAMPLE:                                              *
'*     If Not bFileExists("C:\ISFILE.EX") Then Exit Sub     *
'*    RETURNS:                                              *
'*     TRUE if File is present.                             *
'*     False if File is not present.                        *
'************************************************************
    Dim x&
    x& = FreeFile

    On Error Resume Next
    Open Path$ For Input As x&
    If Err = 0 Then
        bFileExists = True
    Else
        lSystErr = Err
        bFileExists = False
    End If
    Close x&

End Function

Function bCheckOptionInstalled(sOpt As String) As Boolean
    'DESCRIPTION:
    '   This routine determines if the ViperT RF option is installed
    'RETURNS:
    '   TRUE if the RF option is installed
    
    Dim lpBuffer As String * 256
    Dim FileError As Long
    Dim ReadBuffer As String
    Dim sKey As String
    
    Select Case UCase(sOpt)
      Case "RF":    sKey = "RF_OPTION_INSTALLED"
      Case "EO":    sKey = "EO_OPTION_INSTALLED"
      Case "BUSS":  sKey = "BUSS_OPTION_INSTALLED"
      Case "RFSTIM": sKey = "RF_OPTION_INSTALLED"
      Case Else: MsgBox "Invalid argument (" + UCase(sOpt) + ") in CheckOptionInstalled function, notify maintenance."
    End Select

    lpBuffer$ = ""
    FileError& = GetPrivateProfileString("System Startup", sKey, "YES", lpBuffer$, 256, WindowsDirectory$ + TETS_INI)
    ReadBuffer$ = StripNullCharacters(lpBuffer$)
    If UCase(Trim$(ReadBuffer$)) = "YES" Then
        bCheckOptionInstalled = True
    Else
        bCheckOptionInstalled = False
    End If
   
End Function

Public Function VXIPowerCheck() As Integer
    '************************************************************
    '* Nomenclature   : VIPER/T SYSTEM SELF TEST                *
    '*    DESCRIPTION:                                          *
    '*     This Module checks the VIPERT.INI file and ensures   *
    '*     the VXI power switch is on.                          *
    '*    EXAMPLE:                                              *
    '*     VXI28voltOn% = VXIPowerCheck%                        *
    '************************************************************
    Dim keyinfo As String * 256
    Dim errorMessage As Long
    Dim Answer As Long
    
    VXIPowerCheck = True
    Do
        errorMessage = GetPrivateProfileString("System Monitor", "CHASSIS_STATE", "OFF", keyinfo$, 256, WindowsDirectory$ + TETS_INI)
        
        If UCase$(Trim$(StripNullCharacters(keyinfo$))) = "OFF" Then
            Answer& = MsgBox("Instrument Chassis Power Switch Not Activated.", vbRetryCancel)
            If Answer& = vbCancel Then
                Exit Do
            End If
        Else
            Exit Do
        End If
    Loop
    
End Function

Function InitMessageBasedInstrument(InstrumentToInit&) As Integer
    'DESCRIPTION:
    '   This Routine initializes a message based instrument and verifies that it
    '   is the correct instrument
    'PARAMETERS:
    '   InstrumentToInit% = The index of the instrument to initialize
    'RETURNS:
    '   An integer representing status PASSED, FAILED or UNTESTED
    'EXAMPLE:
    '   SystErr& = ReceiveMessage (DMMHandle&, Ret$) '* This will retrieve a message from the DMM
    Dim DevSpec As String
    Dim IDSpec As String
    Dim InitStatus&
    Dim iHandle&
    Dim Dummy&
    Dim SystErr&
    Dim Items%

    InitMessageBasedInstrument = True
    iInstrumentInitialized(InstrumentToInit) = True
    iHandle = nInstrumentHandle&(InstrumentToInit) 'The VISA session handle
    DevSpec$ = sInstrumentSpec$(InstrumentToInit)   'The unique symbolic name of the resource
    IDSpec$ = sIDNResponse$(InstrumentToInit)       'The concatination of the first two elements returned from the *IDN?

    Select Case InstrumentToInit
      Case RFMEAS, COUNTER, ARB, OSCOPE
         InitStatus& = viOpen(nSessionHandle&, DevSpec$, VI_NULL, VI_NULL, iHandle&)
         If InitStatus& <> 0 Then
           Echo "Could not get instrument handle for " & DevSpec$
         End If
    End Select
    
    If InitStatus& <> VI_SUCCESS Then
        Echo "Could not get instrument handle for " & DevSpec$
        iInstrumentInitialized(InstrumentToInit) = False
        InitMessageBasedInstrument = False
        Exit Function
    End If
    
    Dummy& = atxmlDF_viClear(ResourceName(InstrumentToInit), 0&)
    Delay 0.5
    If InstrumentToInit = RFMEAS Then
       WriteMsg2 iHandle&, "*RST" ' RFMEAS does not use the cicl
       WriteMsg2 iHandle&, "*CLS"
       Delay 0.2
       Do
           WriteMsg2 iHandle&, "SYST:ERR?"
           Delay 0.5
           nSystErr = ReadMsg2(iHandle&, ReadBuffer$)
       Loop While Val(ReadBuffer$) <> 0
   ElseIf (InstrumentToInit <= LAST_RF_INST) Or (InstrumentToInit > LAST_EO_INST) Then
       WriteMsg InstrumentToInit, "*RST" ' uses the cicl
       WriteMsg InstrumentToInit, "*CLS"
       Delay 0.2
       Do
           WriteMsg InstrumentToInit, "SYST:ERR?"
           Delay 0.5
           nSystErr = ReadMsg(InstrumentToInit&, ReadBuffer$)
       Loop While Val(ReadBuffer$) <> 0
   End If
    
   nInstrumentHandle&(InstrumentToInit) = iHandle&
       
End Function

Sub TETSVar()
      'DESRIPTION:
    '   This routine initializes the program's variables
    
    Dim lpBuffer As String * 256
    Dim Ret&
    Dim CardIndex%
    Dim FileError&

    sIDNResponse(DMM) = "E1412A"
    sIDNResponse(COUNTER) = "E1420B"
    sIDNResponse(ARB) = "E1445A"
    sIDNResponse(RFPM) = "E1416A"
    sIDNResponse(RFSYN) = "1140B"
    sIDNResponse(OSCOPE) = "ZT1428" ' vipert
    sIDNResponse(FGEN) = "3152"
    sIDNResponse$(RFMEAS) = "VM2601"
    
    sInstrumentSpec$(OSCOPE) = "VXI::17::INSTR"
    sInstrumentSpec$(COUNTER) = "VXI::18::INSTR"
    sInstrumentSpec$(FGEN) = "VXI::19::INSTR"
    sInstrumentSpec$(RFSYN) = "VXI0::20::INSTR"
    sInstrumentSpec$(RFMEAS) = "VXI::25::INSTR"
    sInstrumentSpec$(RFPM) = "VXI::28::INSTR"
    sInstrumentSpec$(SYN_RES) = "VXI::30::INSTR"
    sInstrumentSpec$(SWITCH1) = "VXI::38::INSTR"
    sInstrumentSpec$(DMM) = "VXI::44::INSTR"
    sInstrumentSpec$(ARB) = "VXI::48::INSTR"

    sInstrumentDescription$(OSCOPE) = "ZTEC 1428 Digitizing Oscilloscope (SC Slot 1)"
    sInstrumentDescription$(DMM) = "Racal Instruments 4152A DMM (PC Slot 11)"
    sInstrumentDescription$(COUNTER) = "HP E1420B Counter/Timer (SC Slot 2)"
    sInstrumentDescription$(ARB) = "HP E1445A Arbitrary Waveform Gen (PC Slot 12)"
    sInstrumentDescription$(DIGITAL) = "Teradyne M910 Digital Subsystem "
    sInstrumentDescription$(FGEN) = "Racal Instruments 3152A Function Gen (SC Slot 3)"
    
    sInstrumentDescription$(RFPM) = "Phase Matrix 1313B RF Power Meter (SC Slot 7)"
    sInstrumentDescription$(RFSYN) = "Phase Matrix 1140B RF Synthesizer (SC Slots 4-6)"
    sInstrumentDescription$(RFMEAS) = "PM 1313B,20309, VXITech VM2601IF,VM7510 (SC Slots7-9)"
    sInstrumentDescription$(DOWNCONV) = "Phase Matrix 1313B-D7 (SC Slot 7)"
    sInstrumentDescription$(LOCALOSC) = "Phase Matrix 20309 (SC Slot 8)"
    sInstrumentDescription$(DIGITIZER) = "VXITech VM2601IF (SC Slot 9)"
    sInstrumentDescription$(CALIBRATOR) = "VXITech VM7510 (SC Slot 9)"
    
    sInstrumentDescription$(OSCOPE) = "ZTEC 1428 Digitizing Oscilloscope (SC Slot 1)"
    sInstrumentDescription$(PPU) = "Freedom Power Systems FPU/PPU"
    sInstrumentDescription$(SWITCH1) = "Racal 1260-39,OPT1 LF Sw #1 (PC Slot 5)"
    sInstrumentDescription$(SWITCH2) = "Racal 1260-39 LF Sw #2 (PC Slot 6)"
    sInstrumentDescription$(SWITCH3) = "Racal 1260-38T LF Sw #3 (PC Slot 7)"
    sInstrumentDescription$(SWITCH4) = "Racal 1260-58 MF Sw (PC Slot 8)"
    sInstrumentDescription$(SWITCH5) = "Racal 1260-67A HF Sw (PC Slot 9)"
    sInstrumentDescription$(MIL_STD_1553) = "DDC BU-65569i1, MIL-STD-1553 I/F"
    sInstrumentDescription$(SYN_RES) = "North Atlantic Instruments 65CS4 (SC Slot 12)"
    sInstrumentDescription$(APROBE) = "Analog Probe"
    sInstrumentDescription$(EO_VCC) = "Coreco Imaging PC2-Vision Module (IC Slot 6)"
    sInstrumentDescription$(VEO2) = "SBIR 93006G7100 ViperT EO"
    sInstrumentDescription$(VEO_LARRS) = "SBIR LARRS"
    
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
    
    sInstFileName$(RFMEAS) = "RFM"
    sInstFileName$(DOWNCONV) = "RFD"
    sInstFileName$(LOCALOSC) = "RFLO"
    sInstFileName$(DIGITIZER) = "RFDIG"
    
    sInstFileName$(RFSYN) = "RFS"
    sInstFileName$(RFPM) = "RFP"
    sInstFileName$(OSCOPE) = "DSCOPE"
    sInstFileName$(PPU) = "UUTPS"
    sInstFileName$(MIL_STD_1553) = "MIL1553"
    sInstFileName$(EO_VCC) = "EOVC"
    sInstFileName$(VEO2) = "EO_LASER"
    
    sInstFileName$(RS422) = "RS422"
    sInstFileName$(RS232) = "RS232"
    sInstFileName$(GIGA) = "GIGABIT ETHERNET"
    sInstFileName$(SYN_RES) = "SYN_RES"

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
    
    PsResourceName$(1) = "DCPS_1"
    PsResourceName$(2) = "DCPS_2"
    PsResourceName$(3) = "DCPS_3"
    PsResourceName$(4) = "DCPS_4"
    PsResourceName$(5) = "DCPS_5"
    PsResourceName$(6) = "DCPS_6"
    PsResourceName$(7) = "DCPS_7"
    PsResourceName$(8) = "DCPS_8"
    PsResourceName$(9) = "DCPS_9"
    PsResourceName$(10) = "DCPS_10"
 
  
End Sub

Function ReadMsg(instrument&, ReturnMessage$) As Long
  
    Dim retCount&
    ReadBuffer$ = ""
    DoEvents
    ReturnMessage$ = space$(256)
    nSystErr = atxml_ReadCmds(ResourceName(instrument), ReturnMessage$, 256&, retCount&)
    
    DoEvents
    ReturnMessage$ = Left$(ReturnMessage$, retCount&)
    ReturnMessage$ = StripNullCharacters(ReturnMessage$)
    ReadMsg = nSystErr
  
End Function

Function ReadMsg2(iHandle&, ReturnMessage$) As Long
    
    'DESCRIPTION:
    '   This Routine is a pass through to the VISA layer using VB conventions to
    '   facilitate clean Word Serial read communications to message based instruments.
    'PARAMETERS:
    '   Instrument = the name of the instrument to read
    '   ReturnMessage$ = the string returned to VB
    'RETURNS:
    '   A long integer representing any errors that may occur
    'EXAMPLE:
    '   nSystErr = ReceiveMessage (DMMHandle&, Ret$) '* This will retrieve a message from the DMM
    Dim retCount&

    ReadBuffer$ = ""
    DoEvents
    ReturnMessage$ = space$(256)
    nSystErr = viRead(iHandle&, ReturnMessage$, 256, retCount&)
    
    DoEvents
    ReturnMessage$ = Left$(ReturnMessage$, retCount&)
    ReturnMessage$ = StripNullCharacters(ReturnMessage$)
    ReadMsg2 = nSystErr
    
End Function

Sub WriteMsg(ByVal instrument, MessageToSend$)

    'DESCRIPTION:
    '   This Routine is a pass through to the VISA layer using VB conventions to
    '   fascilitate clean Word Serial write communications to message based instruments.
    'PARAMETERS:
    '   Instrument = the name of the instrument to write
    '   MessageToSend$ = the string of data to be written
    'EXAMPLE:
    '   SendMessage DMMHandle&, "*TST?" '* This will run a self test on the DMM
    Dim retCount&
    Dim errorMessage$
    
    DoEvents
    lSystErr = atxml_WriteCmds(ResourceName(instrument), MessageToSend$, retCount&)
    If lSystErr = VI_ERROR_TMO Then 'if there is an error writing to the instrument, this probably means it is hanging and waiting for an input. This is probably the oscope waiting for a trigger event
        atxmlDF_viClear ResourceName(OSCOPE), 0&
    ElseIf lSystErr <> 0 Then
      Echo "WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(instrument) & "."
      Echo "COMMAND: " & MessageToSend$
      MsgBox "Error sending:" & vbCrLf & MessageToSend$ & vbCrLf _
        & "See Log File for additional information.", vbExclamation, "WRITEMSG Function Error"
      iWriteMsgError = True
    End If

End Sub

Function nSizeOf(AnyArray As Variant) As Long
    'Returns number of elements in an array
    nSizeOf = UBound(AnyArray) - LBound(AnyArray) + 1
End Function

Public Function ConvertHexChrToBin(HexChars$) As String
'************************************************************
'* Written By     : David W. Hartley                        *
'*    DESCRIPTION:                                          *
'*     This Module converts a hex digit to a binary string  *
'*    EXAMPLE:                                              *
'*     BinString$ = ConvertHexChrToBin("A")                 *
'*    PARAMETERS:                                           *
'*     HexChars$  =  The Hex charater                       *
'*    RETURNS:                                              *
'*     The Binary string                                    *
'************************************************************


Dim Conv$

Select Case HexChars$
    Case "0"
        Conv$ = "0000"
    Case "1"
        Conv$ = "0001"
    Case "2"
        Conv$ = "0010"
    Case "3"
        Conv$ = "0011"
    Case "4"
        Conv$ = "0100"
    Case "5"
        Conv$ = "0101"
    Case "6"
        Conv$ = "0110"
    Case "7"
        Conv$ = "0111"
    Case "8"
        Conv$ = "1000"
    Case "9"
        Conv$ = "1001"
    Case "A"
        Conv$ = "1010"
    Case "B"
        Conv$ = "1011"
    Case "C"
        Conv$ = "1100"
    Case "D"
        Conv$ = "1101"
    Case "E"
        Conv$ = "1110"
    Case "F"
        Conv$ = "1111"
End Select
    ConvertHexChrToBin = Conv$
    
End Function

Sub WriteSW(Command$)
    Dim count As Long
    Dim ErrorFlag As Long
    Dim InstrumentError As Integer
    Dim ErrorStatus As Long
    Dim sReadBuffer As String
    Dim i%
    
    If bSimulation Then Exit Sub
    
    WriteMsg SWITCH1, Command$

    Select Case UCase(Left$(Command$, 2))
        Case "TE", "YE", "PD", "PS"
        Case "RE"
            If UCase(Left$(Command$, 3)) = "RES" Then
                Delay 0.1
            End If
        Case Else
    End Select

End Sub

Sub WriteMsg2(iHandle&, MessageToSend$)
    
    'DESCRIPTION:
    '   This Routine is a pass through to the VISA layer using VB conventions to
    '   fascilitate clean Word Serial write communications to message based instruments.
    'PARAMETERS:
    '   Instrument = the name of the instrument to write
    '   MessageToSend$ = the string of data to be written
    'EXAMPLE:
    '   SendMessage DMMHandle&, "*TST?" '* This will run a self test on the DMM
    Dim retCount&
    
    DoEvents
    nSystErr = viWrite(iHandle&, MessageToSend$, Len(MessageToSend$), retCount&)
    
End Sub

Sub DisplayErrorMessage(ErrorStatus&)
    Dim Error&
    
    If ErrorStatus < 0 Then
    End If
End Sub



Function ReadSW() As String
    Dim CharsRead&
    Dim ReadString As String
    Dim ErrorStatus As Long
    
    If bSimulation Then Exit Function
    
    Do
        ErrorStatus& = atxml_ReadCmds(ResourceName(SWITCH1), ReadBuffer$, 255&, CharsRead&)
        ReadString = ReadString & Left$(ReadBuffer, CharsRead&)
    Loop While CharsRead = 255
    ReadString = StripNonPrintChar(ReadString)
    ReadSW = ""
    ReadSW = ReadString
    
End Function

Public Function DecodeErrorMessage$(Msg$)

'Note only called from WriteSW()
    
    Dim SourceOfError$, ErrorNum%, ErrorText$
    
    If (Len(Msg$) < 12) Or (InStr(Msg$, ".") = 0) Then
        DecodeErrorMessage = "Error decoding error message"
        Exit Function
    End If

    SourceOfError$ = Mid$(Msg$, InStr(Msg$, ".") - 3, 3)
    Select Case SourceOfError$
        Case "XXX": SourceOfError$ = ""
        Case "000": SourceOfError$ = "1260 Switch Controller"
        Case "001": SourceOfError$ = "1260-39 LF-1 1-Wire Switch Module"
        Case "002": SourceOfError$ = "1260-39 LF-2 1-Wire Switch Module"
        Case "003": SourceOfError$ = "1260-38T LF-3 2-Wire Switch Module"
        Case "004": SourceOfError$ = "1260-58 MF Coaxial Switch Module"
        Case "005": SourceOfError$ = "1260-66A HF Coaxial Switch Module"
        Case Else:  SourceOfError$ = "Unknown Switch Module (Module Address: " & SourceOfError$ & ")"
    End Select
    If SourceOfError$ <> "" Then
        SourceOfError$ = "Error reported from: " & SourceOfError$ & vbCrLf
    End If
    
    ErrorNum% = Val(Mid$(Msg$, InStr(Msg$, ".") + 1, 2))
    Select Case ErrorNum%
        Case 0: ErrorText$ = "No error"
        Case 1: ErrorText$ = "Invalid module number specified"
        Case 2: ErrorText$ = "Specified module not installed"
        Case 3: ErrorText$ = "Invalid channel number specified"
        Case 4: ErrorText$ = "Invalid port number specified"
        Case 5: ErrorText$ = "Command syntax error"
        Case 6: ErrorText$ = "Read value larger than expected"
        Case 7: ErrorText$ = "Function not supported by module"
        Case 8: ErrorText$ = "Expected line terminator not found"
        Case 9: ErrorText$ = "Valid command not found"
        Case 20: ErrorText$ = "Exclude list too long"
        Case 21: ErrorText$ = "Channel entered on exclude list twice"
        Case 22: ErrorText$ = "Module doesn't allow exclude function"
        Case 23: ErrorText$ = "Scan list too long"
        Case 24: ErrorText$ = "Module doesn't allow scan"
        Case 25: ErrorText$ = "Equate list too long"
        Case 26: ErrorText$ = "Module entered on scan list twice"
        Case 27: ErrorText$ = "Incompatible modules equated or digital modules invalid"
        Case 31: ErrorText$ = "SRQMASK invalid"
        Case 40: ErrorText$ = "Number invalid as a test number"
        Case 41: ErrorText$ = "RAM test failure"
        Case 42: ErrorText$ = "ROM test failure"
        Case 43: ErrorText$ = "Non-vol memory test failure"
        Case 44: ErrorText$ = "Incompatible operating system EPROMS, software revisions"
        Case 45: ErrorText$ = "Self-test CPU or related circuitry failure"
        Case 46: ErrorText$ = "Self-test 24 V supply failure"
        Case 47: ErrorText$ = "Self-test timer chip failure"
        Case 48: ErrorText$ = "Insufficient RAM for option module"
        Case 49: ErrorText$ = "Checksum error reading from option module EPROM"
        Case 50: ErrorText$ = "Option module EPROM incompatible with CPU EPROM"
        Case 51: ErrorText$ = "Failed confidence test"
        Case 55: ErrorText$ = "Error STOREing to non-vol memory"
        Case 56: ErrorText$ = "Error REDALLing from non-vol memory"
        Case 57: ErrorText$ = "Non-vol storage location number out of range"
        Case 58: ErrorText$ = "RECALLing from an empty non-vol location not allowed"
        Case 59: ErrorText$ = "Non-vol memory initialization required"
        Case Else: ErrorText$ = "Unknown Error Code"
    End Select
    ErrorText$ = "Error Number: " & Format$(ErrorNum%, "00") & vbCrLf & "Error Message: " & ErrorText$
    
    DecodeErrorMessage$ = SourceOfError$ & ErrorText$
    
End Function

Function StripNonPrintChar(Parsed$) As String
    'DESCRIPTION:
    '   This routine strips trailing characters with ASCII values
    '   less than 32 from the end of a string
    'PARAMTERS:
    '   Parsed$ = String from which to remove null characters
    'RETURNS:
    '   A the reultant parsed string
    Dim i%
    
    For i% = Len(Parsed$) To 1 Step -1
        If Asc(Mid$(Parsed$, i%, 1)) > 32 Then
            Exit For
        End If
    Next i%
    StripNonPrintChar = Left$(Parsed$, i%)
End Function

Public Sub DownLoadSegment(sSegmentFileName As String, sLineOfText)
'Used with Arb
        
End Sub

