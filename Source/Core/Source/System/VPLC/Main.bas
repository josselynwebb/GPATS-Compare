Attribute VB_Name = "MainProcedure"
'**************************************************************
'* ManTech Test Systems Software Module                       *
'*                                                            *
'* Nomenclature   : TETS PATH LOSS COMPENSATION PROGRAM       *
'* Written By     : Jeff Hill / Tom Biggs                     *
'* Last Update    : 08/28/03                                  *
'* Purpose        : This program measures path losses in dB   *
'*                  for the HF switches                       *
'*------ Revision History for V2.1 (TETS Release 4): ---------*
'* 02/03/99 JHill     Per TDR 98309                           *
'*  Changed EndProgram sub to write dates in DateSerial format*
'*    instead of 'Format$()' format ("2/3/1999").             *
'*  Changed 'Quit' button label to 'Close'.                   *
'*  Added 'Abort' function. 'Close' button label changes to   *
'*  'Abort' while running. Added tests within loops to abort. *
'*  Made it so the existing PLC.DAT file is not destroyed     *
'*    until a successful run has completed.                   *
'*------ Revision History for V2.2 (TETS Release ?): ---------*
'* 04/07/99 JHill                                             *
'*  Almost a complete rewrite for the following reasons:      *
'*  1. Switching PLC now uses RF Stim and Power Meter         *
'*    correction tables. This was done to eliminate the       *
'*    initial reference-measurements run.                     *
'*  2. The initial reference-measurements run became obsolete *
'*    when the UTILITY MF IN/OUT connectors changed from N to *
'*    BNC.                                                    *
'*  3. Due to the very long run time for the HF switching     *
'*    (over 1 hr.), it was desireable to allow MF and HF      *
'*    switching path loss to be run individually.             *
'*  4. Since the PLC.DAT file is now shared with the RF Stim, *
'*    it was necessary to change the way the switching PLC    *
'*    generation modified that file. Before, it completely    *
'*    overwrote it.                                           *
'*------ Revision History for V2.3 (TETS Release 9.0): -------*
'* 08/28/03 T. Biggs                                          *
'*  1. Modify MainProcedure.BAS to revised version of SYSCAL  *
'*    switching path loss compensation as follows:            *
'*      A. Remove MF switch PLC tests. MF PLC data is hard    *
'*         coded at the factory.                              *
'*      B. Incorporate compensation routine for W23 test      *
'*         cable in response to DR #181.                      *
'*      C. Added Sub ArraySmooth to remove phasing effects.   *
'*  2. Modify to frmPLC.frm to remove controls associated with*
'*    MF switch PLC, Add a "START" button to run PLC, and     *
'*    resize accordingly.                                     *
'*  3. Modify to frmAbout.frm and Sub MAIN to set program     *
'*    version on splash screen using APP.Major and APP.Minor  *
'*    properties.                                             *
'*  4. Project updated from VB V4.0 to V6.0. Renamed from     *
'*    "Project1" to more descriptive "PathLossComp". Renamed  *
'*    various constants and variables to conform with project *
'*    standards.                                              *
'*  5. Corrected Sub UpdateStatus to display Failed picture   *
'*    on frmPLC when a test fails.                            *
'*------ Revision History for V2.4 (VIPER Release 1.0): ------*
'* 05/08/06 A. Pipia                                          *
'*
'
'**************************************************************

Option Explicit

Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long

'---------------- RF Stim Declarations --------------------------
'Declare Function gt50000_init& Lib "gt50000.dll" (ByVal resourceName$, ByVal idQuery%, ByVal resetDevice%, instrSession&)
'Declare Function gt50000_reset& Lib "gt50000.dll" (ByVal instrSession&)
'Declare Function gt50000_close& Lib "gt50000.dll" (ByVal instrSession&)
'Declare Function gt50000_selectCorrectionsTable& Lib "gt50000.dll" (ByVal filePath$, ByVal TableName$)
'Declare Function gt50000_setCorrectedPower& Lib "gt50000.dll" (ByVal instrSession&, ByVal frequency#, ByVal Power!)
'Declare Function gt50000_errorMessage& Lib "gt50000.dll" (ByVal instrSession&, ByVal ErrorCode&, ByVal errorMessage$)
'Public Const GT50000_ERROR_FILE_OPEN = &HBFFC0800
'Public Const GT50000_ERROR_FILE_WRITE = &HBFFC0801
'Public Const GT50000_ERROR_FILE_READ = &HBFFC0802
'Public Const GT50000_ERROR_INVALID_TABLE = &HBFFC0803
'Public Const GT50000_ERROR_INV_CORRECTIONS = &HBFFC0804
'Public Const GT50000_ERROR_INV_FREQUENCY = &HBFFC0805

Public Const cPLC_DATA_POINTS = 180
Public Const cPLC_START_FREQ = 100000000#
Public Const cPLC_STOP_FREQ = 18000000000#
Public Const cPLC_STEP_FREQ = 100000000#

Public bLiveMode As Boolean
Public lSessionHandle As Long
Public lpBuffer As String * 256
Public sReadBuffer As String * 256
Public i As Integer
Public sTetsIni As String
Public sPlcPath As String
Public lSystErr As Long
Public lRetCount As Long
Public iSelMode As Integer  'Indicates user selection from the Instruct box
Public iMsgBoxBtn As Integer
Public iWriteMsgError As Integer
Public Const ALLTESTS = 0         'Selected all tests or adjustments
Public Const USER_TERMINATE = -2  'Stop tests or adjustments
Public Const SKIP_TEST = -3       'Skip current test or adjustment
Public Const NO_OP = -99          'Default no-operation state
Public Const HF_SWITCH = 2        'Replaces option to select MF or HF

Dim DataLine$, ProgramPath$
Dim sQuote As String
Dim ReadBuffer As String * 255
Dim LabelToBlink%
Dim bTimeout As Boolean
Dim frequency#
Dim GraphicsPath As String
Dim sSwitchPath1 As String
Dim sSwitchPath As String
Dim sMux(0 To 5) As String
Dim sSwitchPin(0 To 8) As String

Const CAL_INTERVAL = 365          '1 year

Public Const SWITCHING = 1
Public Const RF_PM = 2
Public Const RF_STIM = 3
Public Const LAST_INSTRUMENT = RF_STIM

Const MSG_RESP_REG = 10
Const DOR_MASK = &H2000
Const MANUF = 1
Const MODEL = 2
Const TETS_INI = "Tets.ini"
Public Const MAX_LOW = -1E+300  '\__/ Used to replace 'dMeasured' values when
Public Const MAX_HIGH = 1E+300  '/  \ error conditions exist

Public lInstrumentHandle(1 To LAST_INSTRUMENT) As Long
Public sIDNResponse(1 To LAST_INSTRUMENT) As String
Public sInstrumentSpec(1 To LAST_INSTRUMENT) As String
Public sOemName(1 To LAST_INSTRUMENT) As String
Public sTetsName(1 To LAST_INSTRUMENT) As String
Public sSaifName(1 To LAST_INSTRUMENT) As String
Public sLabelName(1 To LAST_INSTRUMENT) As String
Dim sIniKey(1 To LAST_INSTRUMENT) As String

Public sMeasured As String    'Holds instrument return strings
Public dMeasured As Double    'Holds converted string values
Public bPassed As Boolean     '\   These flags are set by the 'Test' procedure to
Public bFailed As Boolean     ' \  define the status of the current test.  The
Public bOutHi As Boolean      ' /  test status represented by each is self-evident.
Public bOutLo As Boolean      '/
Public bFailedAny As Boolean  'This flag designates that some test failed.
                              ' It is set by Test ONLY when a test fails, and should be
                              ' cleared by Performance Test procedures usually at the
                              ' beginning of a test group.
Public bTimedOut As Boolean   'This flag is set when an instrument does not respond to
                              'a measurement query within the TMO time.
Public bFailedInit As Boolean 'Designates that some instrument did not initialize.
Public sMsg As String         'General Purpose string
Public s8481A_SN As String    'Serial Number of system HP8481A power sensor
Public s8481D_SN As String    'Serial Number of system HP8481D power sensor
Public s11708A_SN As String   'Serial Number of system 30dB attenuator

Public Sub Adjustment(iCurTest As Integer)
  'DESCRIPTION:
  '   Performs Adjustments
  'PARAMETERS:
  '   iCurTest: Number of the test group currently being run
  
  '6 900-series switches with 6 channels.  cPLC_DATA_POINTS frequencies per channel
  Dim dS900(1 To 6, 0 To 5, 1 To cPLC_DATA_POINTS) As Single
  Dim dCablePLoss(1 To cPLC_DATA_POINTS) As Double
  
  Dim freqMHz As Single
  Dim Index%, iWiredGroup%, iMux1%, iMux2%, iChannel%, iChannelMax%, iStep%
  Dim sSaifLossDatLines() As String
  Static sPlcDatLines() As String
  Static sPlcSimData() As String
  Dim sItems() As String
  Dim i%, n%
  Dim dPathLoss As Double
  Dim dFrequency As Double
  Dim sSwitchPath1 As String, sSwitchPath As String
  Dim sMux(0 To 5) As String, sSwitchPin(0 To 8) As String
  Dim sLine As String
  Dim retVal As Long
  Dim table As String
  Dim dSimLoss As Double
  Dim daPowerMeterLoss(0 To cPLC_DATA_POINTS) As Double
      
  If Not InitInstrument(SWITCHING) Then GoTo EndSelect
  If Not InitInstrument(RF_STIM) Then GoTo EndSelect
   
  'retVal = readPLCFile_if()
  'retVal = exportToExcel_if("C:\Program Files\DME Corporation\RFMS\Source\ConfigFiles\PLC.xls")
  
  If Not InitInstrument(RF_PM) Then GoTo EndSelect
  SetTimeOut RF_PM, 20
  'Read entire PLC.DAT
  If Not bReadFile(sPlcPath, sPlcDatLines()) Then
    bFailedInit = True
    GoTo EndSelect
  End If
  
  'If Simulating read PLCSim.dat file
  If (Not bLiveMode) Then
    ' Read entire PLCSim.dAT
    If Not bReadFile("C:\Program Files\DME Corporation\RFMS\Source\ConfigFiles\PLCSim.dat", sPlcSimData()) Then
        bFailedInit = True
        GoTo EndSelect
    End If
  End If
  
  'If CheckForCorrectionsTables(True) <> VI_SUCCESS Then GoTo EndSelect     'CODE_CHECK NEED TO SUBSTITUE SOME CHECK HERE
  'Read SaifLoss.Dat lines into array
  'If Not bReadFile(App.path & "\SaifLoss.Dat", sSaifLossDatLines()) Then 'I don't see sSaifLossDaLines being used anywhere???? CODE_CHECK
  '  bFailedInit = True
  '  GoTo EndSelect
  'End If
  
  If Not bSensorCal("8481A") Then GoTo EndSelect
  checkPMError gRFPm.setMeasureUnits("dBm")
  checkPMError gRFPm.setRangeUpper(5, "dBm")
  checkPMError gRFPm.setExpFreq(0.1, "GHz")
   
  'Begin HF Switching Path Loss Compensation
  frmPLC.lblTestName(iCurTest).Tag = "Failed"
  
  DisplaySetup "Connect the 8481A Power Sensor to SAIF RF STIM OUTPUT through a W23 cable and an N-Type Female to Female adapter.", "PLC_1.BMP"
  WriteMsg RF_STIM, "OUTP ON"
  MainMenuMsg "Profiling RF-STIM via External Cable"
  frmPLC.proProgressBar.Visible = True
  frmPLC.proProgressBar.Value = 0
  frmPLC.proProgressBar.Max = cPLC_DATA_POINTS
  Index% = 0

  freqMHz = cPLC_START_FREQ / 1000000#      'CODE_CHECK FOR DEBUG/DEVELOPMENT ONLY -- REMOVE
  dSimLoss = 0.001
 
  'Loop for Profiling RF-STIM via External Cable
  For dFrequency = cPLC_START_FREQ To cPLC_STOP_FREQ Step cPLC_STEP_FREQ
    Index% = Index% + 1
    SetRfStim dFrequency, 0

RetryCable:
    checkPMError gRFPm.setExpFreq(dFrequency, "Hz")
    checkPMError gRFPm.setRangeLower(5, "dBm")
    Fetch RF_PM, dMeasured
    If bTimedOut Then
      bFailedAny = True
      GoTo EndSelect
    End If
    
    'Simulation mode supports development and training
'    If (Not bLiveMode) Then
'        dSimLoss = dSimLoss + 0.001
'        'get measured value from file
'        retVal = getSimulatedLoss(sPlcSimData(), "SAIF", Index% - 1, dMeasured)
'        If (retVal <> 0) Then GoTo EndSelect
'        CheckPLCFileMgrError setPLCPathData_if(getPLCPath("SAIF"), freqMHz, dMeasured) 'CODE_CHECK FOR DEBUG/DEVELOPMENT ONLY -- REMOVE
'        freqMHz = (freqMHz + cPLC_STEP_FREQ / 1000000#) 'CODE_CHECK FOR DEBUG/DEVELOPMENT ONLY -- REMOVE
'        dCablePLoss(Index%) = dSimLoss
'    Else:
'        dCablePLoss(Index%) = dMeasured
'    End If

    'Simulation mode supports development and training
    If (Not bLiveMode) Then
      'get measured value from file
      daPowerMeterLoss(i) = dSimLoss
      CheckPLCFileMgrError setPLCPathData_if(getPLCPath("POWERMETER"), freqMHz, dSimLoss) 'CODE_CHECK FOR DEBUG/DEVELOPMENT ONLY -- REMOVE
      freqMHz = (((freqMHz * 1000000#) + cPLC_STEP_FREQ) / 1000000#) 'CODE_CHECK FOR DEBUG/DEVELOPMENT ONLY -- REMOVE
      dSimLoss = dSimLoss + 0.001
    End If
   'Do a sanity check
    If dCablePLoss(Index%) < -10 Then
      sMsg = "The path loss value is questionable." & vbCrLf & _
        "    Frequency: " & EngNotate(dFrequency, "Hz") & vbCrLf & _
        "    Path Loss: " & EngNotate(dCablePLoss(Index%), "dBm") & vbCrLf & _
        "    Visa Error Code: " & Hex(lSystErr) & vbCrLf & vbCrLf & _
        "What do you want to do?"
      i = MsgBox(sMsg, vbAbortRetryIgnore + vbQuestion)
      If i = vbAbort Then
        bFailedAny = True
        GoTo EndSelect
      ElseIf i = vbRetry Then
        GoTo RetryCable
      End If
    End If

    MainMenuMsg "Path Loss for external test cable at " & _
      EngNotate(dFrequency, "Hz") & ": " & Format$(dCablePLoss(Index%), "#0.000") & "dB"
    BumpProgBar
    If iSelMode = USER_TERMINATE Then GoTo EndSelect

  Next dFrequency
    
  'Now, perform path loss on HF switch paths
  WriteMsg RF_STIM, "OUTP OFF"
  DisplaySetup "Connect RF STIM OUTPUT to UTILITY HF IN and the Power Sensor to UTILITY HF OUT.", "PLC_3.BMP"
  WriteMsg RF_STIM, "OUTP ON"
  MainMenuMsg "Calculating HF Switch Path Loss Compensation"
  
  'Fill error message arrays
  For i% = 0 To 5
    sMux(i) = "S90" & CStr(i% + 1)
  Next i%
  For i% = 0 To 6
    sSwitchPin(i) = "-" & CStr(i% + 2)
  Next i%
  frmPLC.proProgressBar.Value = 0
  frmPLC.proProgressBar.Max = 1360
  
  'Take measurements
  For iWiredGroup = 1 To 3
    Select Case iWiredGroup
      Case 1:
        iMux1 = 3       'S904
        iMux2 = 2       'S903
        iChannelMax = 4
        WriteMsg SWITCHING, "CLOSE 5.00"
        WriteMsg SWITCHING, "CLOSE 5.10"
        sSwitchPath1 = "S901-2, S902-2, "
      Case 2:
        iMux1 = 0       'S901
        iMux2 = 1       'S902
        iChannelMax = 4
        sSwitchPath1 = ""
      Case 3:
        iMux1 = 4       'S905
        iMux2 = 5       'S906
        iChannelMax = 5
        WriteMsg SWITCHING, "CLOSE 5.05"
        WriteMsg SWITCHING, "CLOSE 5.15"
        sSwitchPath1 = "S901-7, S902-7, "
    End Select
    
    For iChannel = 0 To iChannelMax
      WriteMsg SWITCHING, "CLOSE 5." & CStr(iMux1) & CStr(iChannel)
      WriteMsg SWITCHING, "CLOSE 5." & CStr(iMux2) & CStr(iChannel)
      Index% = 0
      sSwitchPath = sSwitchPath1 & sMux(iMux1) & sSwitchPin(iChannel) & ", " _
                                 & sMux(iMux2) & sSwitchPin(iChannel)
    ' if simulation Reset it for next Channel
    If (Not bLiveMode) Then dSimLoss = 0.006
    
    For dFrequency = cPLC_START_FREQ To cPLC_STOP_FREQ Step cPLC_STEP_FREQ
        Index% = Index% + 1
        SetRfStim dFrequency, 0
        ' if simulating set dMeasured value

RetryS900:
        checkPMError gRFPm.setExpFreq(dFrequency, "Hz")
        checkPMError gRFPm.setRangeLower(5, "dBm")
        If (bLiveMode) Then
            Fetch RF_PM, dMeasured
            If bTimedOut Then
                bFailedAny = True
                GoTo EndSelect
            End If
        End If
                    
        'Simulation mode supports development and training
        If (Not bLiveMode) Then
            dSimLoss = dSimLoss + 0.001
            If (iWiredGroup = 1) Then
                ' multiple simPathLoss by 4
                dPathLoss = dSimLoss / 4
                'dPathLoss = 0.001
                'dPathLoss = (dCablePLoss(Index%) - dSimLoss) ' / 4
                If iChannel = 0 Then
                  dS900(1, 5, Index%) = dPathLoss
                  dS900(2, 5, Index%) = dPathLoss
                End If
                dS900(3, iChannel, Index%) = dPathLoss
                dS900(4, iChannel, Index%) = dPathLoss
            End If
                If (iWiredGroup = 3) Then
                ' multiple simPathLoss by 6
                dPathLoss = dSimLoss / 6
                'dPathLoss = 0.001
                'dPathLoss = (dCablePLoss(Index%) - (dSimLoss)) ' / 6
                If iChannel = 5 Then
                  dS900(3, 5, Index%) = dPathLoss
                  dS900(4, 5, Index%) = dPathLoss
                End If
                dS900(5, iChannel, Index%) = dPathLoss
                dS900(6, iChannel, Index%) = dPathLoss
            End If
            If (iWiredGroup = 2) Then   'Switch 2
                ' multiple simPathLoss by 2
                dPathLoss = dSimLoss / 2
                'dPathLoss = 0.001
                'dPathLoss = (dCablePLoss(Index%) - (dSimLoss)) ' / 2
                'Switch2, Channel 5 is calculated with Switch 1.
                dS900(1, iChannel, Index%) = dPathLoss
                dS900(2, iChannel, Index%) = dPathLoss
            End If
          End If
'              'get measured value from file
'              If iWiredGroup = 1 Or iWiredGroup = 3 Then
'                If iWiredGroup = 1 Then
'                 If iChannel = 0 Then
'                   retVal = getSimulatedLoss(sPlcSimData(), "S901-2", Index% - 1, dMeasured)
'                   dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                   dS900(1, 0, Index%) = dPathLoss
'                   retVal = getSimulatedLoss(sPlcSimData(), "S904-2", Index% - 1, dMeasured)
'                   dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                   dS900(4, 0, Index%) = dPathLoss
'                 End If
'                 table = "S902-" + CStr(iChannel + 2)
'                 retVal = getSimulatedLoss(sPlcSimData(), table, Index% - 1, dMeasured)
'                 dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                 dS900(2, iChannel, Index%) = dPathLoss
'                 table = "S903-" + CStr(iChannel + 2)
'                 retVal = getSimulatedLoss(sPlcSimData(), table, Index% - 1, dMeasured)
'                 dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                 dS900(3, iChannel, Index%) = dPathLoss
'               Else     'Switch3
'                 If iChannel = 5 Then
'                   retVal = getSimulatedLoss(sPlcSimData(), "S901-5", Index% - 1, dMeasured)
'                   dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                   dS900(1, 5, Index%) = dPathLoss
'                   retVal = getSimulatedLoss(sPlcSimData(), "S904-5", Index% - 1, dMeasured)
'                   dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                   dS900(4, 5, Index%) = dPathLoss
'                 End If
'                 table = "S905-" + CStr(iChannel + 2)
'                 retVal = getSimulatedLoss(sPlcSimData(), table, Index% - 1, dMeasured)
'                 dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                 dS900(5, iChannel, Index%) = dPathLoss
'                 table = "S906-" + CStr(iChannel + 2)
'                 retVal = getSimulatedLoss(sPlcSimData(), table, Index% - 1, dMeasured)
'                 dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                 dS900(6, iChannel, Index%) = dPathLoss
'                End If
'             Else    'Switch 2
'               'Switch2, Channels 2&7 are already calculated with Switch 1&3 repectively.
'               If iChannel > 0 And iChannel < 5 Then
'                 table = "S901-" + CStr(iChannel + 2)
'                 retVal = getSimulatedLoss(sPlcSimData(), table, Index% - 1, dMeasured)
'                 dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                 dS900(1, iChannel, Index%) = dPathLoss
'                 table = "S904-" + CStr(iChannel + 2)
'                 retVal = getSimulatedLoss(sPlcSimData(), table, Index% - 1, dMeasured)
'                 dPathLoss = (dCablePLoss(Index%) - dMeasured)
'                 dS900(4, iChannel, Index%) = dPathLoss
'               End If
'             End If
'          End If
             
         ' Live mode (not Simulating)
         If (bLiveMode) Then
            If (iWiredGroup = 1) Then
                dPathLoss = (dCablePLoss(Index%) - dMeasured) / 4
                If iChannel = 0 Then
                  dS900(1, 5, Index%) = dPathLoss
                  dS900(2, 5, Index%) = dPathLoss
                End If
                dS900(3, iChannel, Index%) = dPathLoss
                dS900(4, iChannel, Index%) = dPathLoss
            End If
                If (iWiredGroup = 3) Then
                dPathLoss = (dCablePLoss(Index%) - dMeasured) / 6
                If iChannel = 5 Then
                  dS900(3, 5, Index%) = dPathLoss
                  dS900(4, 5, Index%) = dPathLoss
                End If
                dS900(5, iChannel, Index%) = dPathLoss
                dS900(6, iChannel, Index%) = dPathLoss
            End If
            If (iWiredGroup = 2) Then   'Switch 2
                dPathLoss = (dCablePLoss(Index%) - dMeasured) / 2
                'Switch2, Channel 5 is calculated with Switch 1.
                dS900(1, iChannel, Index%) = dPathLoss
                dS900(2, iChannel, Index%) = dPathLoss
            End If
          End If
          
          'Do a sanity check
          If (dPathLoss > 10) Or (dPathLoss < 0) Then
            If (dPathLoss > -1) And (dPathLoss < 0) Then
              dPathLoss = 0   'Convert a low negative value to 0 dB loss
            Else
              sMsg = "The path loss value is questionable." & vbCrLf & _
                "    Path: " & sSwitchPath & vbCrLf & _
                "    Frequency: " & EngNotate(dFrequency, "Hz") & vbCrLf & _
                "    Path Loss: " & EngNotate(dPathLoss, "dB") & vbCrLf & _
                "    Visa Error Code: " & Hex(lSystErr) & vbCrLf & vbCrLf & _
                "What do you want to do?"
              i = MsgBox(sMsg, vbAbortRetryIgnore + vbQuestion)
              If i = vbAbort Then
                bFailedAny = True
                GoTo EndSelect
              ElseIf i = vbRetry Then
                GoTo RetryS900
              End If
            End If
          End If
          MainMenuMsg "Unsmoothed Path Loss for switches " & sSwitchPath & " at " & _
            EngNotate(dFrequency, "Hz") & ": " & Format$(dPathLoss, "#0.000") & "dB"
          BumpProgBar
          If iSelMode = USER_TERMINATE Then GoTo EndSelect
    Next dFrequency
    Next iChannel
   Next iWiredGroup
  
  'Smooth the losses to remove phasing effects
  Dim dLoss(1 To cPLC_DATA_POINTS) As Double
  Dim dSmooth() As Double
  MainMenuMsg "Smoothing Loss Data"
  For iWiredGroup = 1 To 6
    For iChannel = 0 To 5
      For iStep = 1 To cPLC_DATA_POINTS
        dLoss(iStep) = dS900(iWiredGroup, iChannel, iStep)
      Next iStep
      ArraySmooth dLoss(), dSmooth(), 20
      For iStep = 1 To cPLC_DATA_POINTS
        dS900(iWiredGroup, iChannel, iStep) = dSmooth(iStep)
      Next iStep
    Next iChannel
  Next iWiredGroup

  'Build and replace PLC.DAT lines
  MainMenuMsg "Updating PLC Data file"
  For iWiredGroup = 1 To 6
    For iChannel = 0 To 5
      DoEvents
      sLine = "S90" & CStr(iWiredGroup) & "-" & CStr(iChannel + 2)
      table = sLine
      freqMHz = cPLC_START_FREQ / 1000000#
      'Append cPLC_DATA_POINTS PL values per iChannel to DataLine string
      For iStep = 1 To cPLC_DATA_POINTS
       CheckPLCFileMgrError setPLCPathData_if(getPLCPath(table), freqMHz, dS900(iWiredGroup, iChannel, iStep))
      freqMHz = (((freqMHz * 1000000#) + cPLC_STEP_FREQ) / 1000000#)
      sLine = sLine & ", " & Format$(dS900(iWiredGroup, iChannel, iStep), "#0.000")
      Next iStep
      'Find and replace line
      For i = LBound(sPlcDatLines) To UBound(sPlcDatLines)
        If Left$(sPlcDatLines(i), 6) = Left$(sLine, 6) Then
          sPlcDatLines(i) = sLine
          Exit For
        End If
      Next i
      'If not found, add one
      If i > UBound(sPlcDatLines) Then
        ReDim Preserve sPlcDatLines(LBound(sPlcDatLines) To i)
        sPlcDatLines(i) = sLine
      End If
    Next iChannel
  Next iWiredGroup
 
  'OK, write the file
  If bWriteFile(sPlcPath, sPlcDatLines()) Then
    frmPLC.lblTestName(iCurTest).Tag = "Passed"
  End If
  CheckPLCFileMgrError savePLCData_if()
  CheckPLCFileMgrError exportToExcel_if("C:\Program Files\DME Corporation\RFMS\Source\ConfigFiles\PLC.dat")
  'CheckPLCFileMgrError exportSplinesToExcel_if("C:\Program Files\DME Corporation\RFMS\Source\ConfigFiles\SplnPLC.xls")

EndSelect:
  MainMenuMsg "Click Start button to rerun."
  frmPLC.cmdStart.Visible = True
  frmPLC.proProgressBar.Visible = True
  frmPLC.proProgressBar.Value = 0
  lSystErr = InitInstrument(RF_PM)
  lSystErr = InitInstrument(RF_STIM)
  lSystErr = InitInstrument(SWITCHING)
End Sub

Public Function bWriteFile(sFilePath As String, sLines() As String) As Boolean
  Dim iFileID As Integer
  Dim i As Integer
  
  On Error GoTo FileError
  
  iFileID = FreeFile
  Open sFilePath For Output Access Write As #iFileID
  For i = LBound(sLines) To UBound(sLines)
    Print #iFileID, sLines(i)
  Next i
  Close iFileID
  bWriteFile = True
  Exit Function

FileError:
  MsgBox "Error writing file: " & sFilePath & vbCrLf & "Error Message: " & Error(Err)
  bWriteFile = False

End Function

Public Function bInstruct(sInstructions) As Boolean
  
  DoEvents
  
  Select Case iMsgBox(sInstructions, "Ok", "Skip")
    Case 1 'Ok
      iSelMode = NO_OP
      bInstruct = True
    Case 2  'Skip
      iSelMode = SKIP_TEST
      bInstruct = False
  End Select
  
End Function

Function iMsgBox(ParamArray ArgArray())
' This function allows up to six buttons to be dispayed
' on a form as well as a text message.  The arguments are:
' text message, and up to six button labels.
' the buttons are placed as shown below
' button 1   button 2   button 3
' button 4   button 5   button 6
' To use only one button but you want it to be centered
' use a blank " " as the second argument and the text for
' the second button as the third argument.  The function
' will not display a button if the text is a blank.

  Dim i As Integer
  Dim iNumArg As Integer
  Dim sTmpMsg As String
  
  With frmMsgBox
  
  Beep  'Get the operator's attention
  
  .Caption = "Operator Message"
  
  sTmpMsg = frmPLC.sbrInfoBar.SimpleText 'Save
  MainMenuMsg "Please respond to the Operator Message"
  
  .Height = 4020
  .Width = 4680
  
  CenterForm frmMsgBox
  
  iNumArg = UBound(ArgArray)
  
  ' Check for no arguments
  If iNumArg = -1 Then
    iMsgBox " ", " ", "OK"
    iMsgBox = 1
    Exit Function
  End If
  
  ' The first argument is the text
  .lblMessage.Caption = ArgArray(0)

  ' The remaining arguments are the button labels
  For i = 1 To iNumArg
    .Command1(i).Caption = ArgArray(i)
    If (ArgArray(i) <> "") Then
      .Command1(i).Visible = True
    Else
      .Command1(i).Visible = False
    End If
  Next i

  Dim iButTop
  If iNumArg > 3 Then
    iButTop = 2640
    .lblMessage.Height = 2505
  Else
    iButTop = 3120
    .lblMessage.Height = 2985
  End If
  For i = 1 To 3
    .Command1(i).Top = iButTop
  Next i

  .Show vbModal
  iMsgBox = iMsgBoxBtn
  
  For i = 1 To 6
    .Command1(i).Visible = False
  Next i
  
  End With
  MainMenuMsg sTmpMsg   'Restore
  
End Function

Public Function StringToList(sStr As String, iLower As Integer, List$(), sDelimiter As String) As Integer
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
  
  Dim numels%, i As Integer
  Dim iDelimiterLength As Integer
  
  iDelimiterLength = Len(sDelimiter)
  If sStr = "" Then
    StringToList% = 0
    Exit Function
  End If
  
  numels% = 1
  ReDim List$(iLower To iLower)
  'Go through parsed string a character at a time.
  For i = 1 To Len(sStr)
    'Test for delimiter
    If Mid$(sStr, i, iDelimiterLength) <> sDelimiter Then
      'Add the character to the current argument.
      List$(iLower + numels% - 1) = List$(iLower + numels% - 1) & Mid$(sStr, i, 1)
    Else
      'Found a delimiter.
      ReDim Preserve List$(iLower To iLower + numels%)
      numels% = numels% + 1
      i = i + iDelimiterLength - 1
    End If
  Next i
  StringToList% = numels%
  
End Function
Public Sub SetRfStim(ByVal frequency As Double, ByVal Power As Single)
  Dim errorMessage$
  Dim lStoreTmo&
  Dim dStartTime As Double
  
  iWriteMsgError = 0
  If lInstrumentHandle(RF_STIM) = 0 Then
    lSystErr = InitInstrument(RF_STIM)
  End If
  dStartTime = dGetTime("Now")
  If (bLiveMode) Then
    ' New Coded Added by Anthony for Viper/T
    WriteMsg RF_STIM, ":FREQ " & CStr(frequency) & " HZ"
    WriteMsg RF_STIM, ":POW " & CStr(Power) & " DBM"
    WriteMsg RF_STIM, ":OUTP ON"
  
'    ' Old code removed by Anthony for Viper/T
'    'CODE_CHECK POWER NEEDS TO BE ADJUSTED BY SAIF LOSS
'    lSystErr = eip114x_freq_power(lInstrumentHandle(RF_STIM), frequency#, cHZ, Power!, cATTEN_AUTO)
'    'lSystErr = gt50000_setCorrectedPower(lInstrumentHandle(RF_STIM), frequency#, Power!)
'    If lSystErr = 8 Then  'If "WARNING: Questionable status summary"
'      lSystErr = viGetAttribute(lInstrumentHandle(RF_STIM), VI_ATTR_TMO_VALUE, lStoreTmo)
'      lStoreTmo = lStoreTmo / 1000 'Convert from mSec to Sec
'      Do While dGetTime("Now") - dStartTime < lStoreTmo
'        Debug.Print "Questionable Status from RF Stim"
'        ' CODE_CHECK POWER NEEDS TO BE ADJUSTED BY SAIF LOSS
'        lSystErr = eip114x_freq_power(lInstrumentHandle(RF_STIM), frequency#, cHZ, Power!, cATTEN_AUTO)
'        'lSystErr = gt50000_setCorrectedPower(lInstrumentHandle(RF_STIM), frequency#, Power!)
'        If lSystErr = VI_SUCCESS Then Exit Do
'        DoEvents
'      Loop
'    End If
  
  End If
  WriteMsg RF_STIM, "FREQ?": Fetch RF_STIM, dMeasured
  If dMeasured <> frequency Then
    If (bLiveMode = False) Then
        dMeasured = frequency
        Exit Sub
        Else: MsgBox "RF Stim frequency should be " & CStr(frequency) & " but is " & CStr(dMeasured)
    End If
  End If
  ' also removed by Anthony
'  If lSystErr <> VI_SUCCESS Then
'    eip114x_error_query lInstrumentHandle(RF_STIM), lSystErr, lpBuffer
'    'gt50000_errorMessage lInstrumentHandle(RF_STIM), lSystErr, lpBuffer
'    MsgBox "Error sending:" & vbCrLf & "Freq: " & frequency & ", Power: " & Power & " to " & _
'      sTetsName(RF_STIM) & vbCrLf & "Error Message:" & vbCrLf & StripNullCharacters(lpBuffer$), vbExclamation, "VISA Error Message"
'    iWriteMsgError = True
'  Else
'    Do
'      DoEvents
'      lSystErr = viWrite(lInstrumentHandle(RF_STIM), "*OPC?", 5, lRetCount)
'      lSystErr = ReadMsg(RF_STIM, errorMessage$)
'      lSystErr = viWrite(lInstrumentHandle(RF_STIM), "SYST:ERR?", 9, lRetCount)
'      lSystErr = ReadMsg(RF_STIM, errorMessage$)
'      If Val(errorMessage$) = 0 Then Exit Do
'      iWriteMsgError = Val(errorMessage$)
'      MsgBox "Error sending:" & vbCrLf & "Freq: " & frequency & ", Power: " & Power & " to " & _
'      sTetsName(RF_STIM) & vbCrLf & "Error Code:" & CStr(iWriteMsgError) & vbCrLf & _
'      "Error:" & errorMessage$, vbExclamation, sTetsName(RF_STIM) & " Error Message"
'    Loop
'  End If
    ' Added by Anthony
    Do
      DoEvents
      lSystErr = viWrite(lInstrumentHandle(RF_STIM), "*OPC?", 5, lRetCount)
      lSystErr = ReadMsg(RF_STIM, errorMessage$)
      lSystErr = viWrite(lInstrumentHandle(RF_STIM), "SYST:ERR?", 9, lRetCount)
      lSystErr = ReadMsg(RF_STIM, errorMessage$)
      If Val(errorMessage$) = 0 Then Exit Do
      iWriteMsgError = Val(errorMessage$)
      MsgBox "Error sending:" & vbCrLf & "Freq: " & frequency & ", Power: " & Power & " to " & _
      sTetsName(RF_STIM) & vbCrLf & "Error Code:" & CStr(iWriteMsgError) & vbCrLf & _
      "Error:" & errorMessage$, vbExclamation, sTetsName(RF_STIM) & " Error Message"
    Loop
End Sub

'Public Function CheckForCorrectionsTables(DisplayErrors As Boolean)
'  'Must be called after RF_STIM Init (for sPlcPath)
'
'  lpBuffer = ""
'  lSystErr = gt50000_selectCorrectionsTable(sPlcPath, "Receiver")
'  Select Case lSystErr
'    Case GT50000_ERROR_FILE_OPEN
'      If DisplayErrors Then MsgBox "Error opening " & sPlcPath
'      sMsg = "Error opening Path Loss file needed for the " & sOemName(RF_STIM)
'    Case GT50000_ERROR_INVALID_TABLE
'      sMsg = "Error finding 'Receiver' Path Loss table needed for the " & sOemName(RF_STIM)
'    Case GT50000_ERROR_INV_CORRECTIONS
'      sMsg = "Wrong number of corrections in 'Receiver' Path Loss table needed for the " & sOemName(RF_STIM)
'    Case Is <> VI_SUCCESS
'      gt50000_errorMessage lInstrumentHandle(RF_STIM), lSystErr, lpBuffer
'      sMsg = "Error setting corrections for the " & sOemName(RF_STIM) & vbCrLf & StripNullCharacters(lpBuffer)
'    Case Else
'      lSystErr = gt50000_selectCorrectionsTable(sPlcPath, "SAIF")
'      Select Case lSystErr
'        Case GT50000_ERROR_INVALID_TABLE
'          sMsg = "Error finding 'SAIF' Path Loss table needed for the " & sOemName(RF_STIM)
'        Case GT50000_ERROR_INV_CORRECTIONS
'          sMsg = "Wrong number of corrections in 'SAIF' Path Loss table needed for the " & sOemName(RF_STIM)
'        Case Is <> VI_SUCCESS
'          gt50000_errorMessage lInstrumentHandle(RF_STIM), lSystErr, lpBuffer
'          sMsg = "Error setting corrections for the " & sOemName(RF_STIM) & vbCrLf & StripNullCharacters(lpBuffer)
'        Case Else
'          sMsg = ""
'      End Select
'  End Select
'  If DisplayErrors And sMsg <> "" Then
'    bFailedInit = True
'    MsgBox sMsg
'    sMsg = "Perform the " & sTetsName(RF_STIM) & " 'Path Loss Compensation' Adjustment to correct the problem."
'    MsgBox sMsg
'  End If
'  CheckForCorrectionsTables = lSystErr
'
'End Function

Public Function bSensorCal(ByVal sSensor As String) As Boolean
  'DESCRIPTION:
  '   Performs Zero and Self-Cal for RFPM
  '   Provides hookup instructions
  'PARAMETER:
  '   sSensor:  Sensor to use
  'RETURNS:
  '   True if successful, else False
  
  Dim sAtten As String
  Dim iSelection As Integer
  Dim iRetry As Integer
  Dim sMsgStore As String
  Dim dZeroTol As Double
  Dim lErrorCode As Long
  
  If (Not bLiveMode) Then
    bSensorCal = True
    Exit Function
  End If
  sMsgStore = frmPLC.sbrInfoBar.SimpleText
  sSensor = UCase(sSensor)
  Select Case sSensor
    Case "8481A"
      lErrorCode = gRFPm.setPowerHead(sSensor)
      checkPMError lErrorCode
      dZeroTol = Val("30e-9")
    Case "8418D"
      lErrorCode = gRFPm.setPowerHead(sSensor)
      checkPMError lErrorCode
      sAtten = "through 11708A 30dB attenuator (S/N " & s11708A_SN & ")" & vbCrLf
      If InStr(sSensor, "_") = 0 Then sSensor = sSensor & "_" & Right(s8481D_SN, 4)
      dZeroTol = Val("3e-12")
    Case Else
      MsgBox "Unrecognized sensor: " & sSensor
      GoTo FailExit
  End Select
    
  If (lErrorCode <> 0) Then GoTo FailExit
  
  lErrorCode = gRFPm.doZeroAndCal
  checkPMError lErrorCode
  If (lErrorCode <> 0) Then GoTo FailExit
  
  DisplaySetup "Connect the " & sSensor & " Power Meter Sensor to RF POWER METER POWER REF.", "PSH_PRO.BMP"

  WaitForResponse RF_PM, 45, "Sensor Zero/Cal"
  If iSelMode = USER_TERMINATE Then GoTo FailExit
  checkPMError gRFPm.setRefOsc(cPM_STATE_ON)
  Fetch RF_PM, dMeasured
  If bTimedOut Or dMeasured = MAX_LOW Then
    MsgBox "Sensor Zero/Cal procedure did not complete."
    checkPMError gRFPm.setRefOsc(cPM_STATE_OFF)
    GoTo FailExit
  End If
  checkPMError gRFPm.setRefOsc(cPM_STATE_OFF)
  checkPMError gRFPm.setMeasureUnits("W")

  bSensorCal = True
  MainMenuMsg sMsgStore
  Exit Function
  
FailExit:
  MainMenuMsg sMsgStore
  bFailedInit = True
  bSensorCal = False

End Function

Sub SetTimeOut(instrumentIndex%, Seconds!)
  'DESCRIPTION:
  '   Sets the VISA timeout time for an instrument
  'PARAMETERS:
  '   instrumentIndex%:   Instrument for which to set the time out
  '   Seconds!:           The amount of time to wait
  
  If (Not bLiveMode) Then Exit Sub
  If (instrumentIndex = RF_PM) Then
    checkPMError gRFPm.setMaxTime(Seconds)
  Else
    lSystErr = viSetAttribute(lInstrumentHandle(instrumentIndex%), VI_ATTR_TMO_VALUE, CLng(Seconds! * 1000))
  End If

End Sub
Public Function bReadFile(sFilePath As String, sLines() As String) As Boolean
  Dim iFileID As Integer
  Dim i As Integer
  
  On Error GoTo FileError
  
  iFileID = FreeFile
  Open sFilePath For Input Access Read As #iFileID
  ReDim sLines(1 To 1)
  i = 1
  While Not EOF(iFileID)
    ReDim Preserve sLines(1 To i)
    Line Input #iFileID, sLines(i)
    i = i + 1
  Wend
  Close iFileID
  bReadFile = True
  Exit Function

FileError:
  MsgBox "Error reading file: " & sFilePath & vbCrLf & "Error Message: " & Error(Err)
  bReadFile = False

End Function

Sub DisplaySetup(Caption$, Graphic$)
    'DESCRIPTION
    '   This routine displays frmDirections, displays a caption on it to explain
    '   to the operator what manual intervention to perform, and loads the
    '   graphic file associated with this action
    'PARAMETERS
    '   Caption$  = This is the caption to be displayed on the form
    '   Graphic$  = This is the file name of the graphic to be displayed (426 x 222 pixels)
    
    frmDirections.lblCaption.Caption = Caption$
    On Error Resume Next
    
    With frmDirections
    With .picGraphic
    .AutoSize = True
    On Error Resume Next
    .Picture = LoadPicture(App.path & "\GRAPHICS\" & Graphic$)
    If Err Then
      .Picture = LoadPicture(App.path & "\GRAPHICS\NoPicture.bmp")
    End If
    End With
    
    'Re-position objects based on bitmap height
    .imgLogo.Top = .picGraphic.Height + 1065
    .cmdContinue.Top = .picGraphic.Height + 1125
    .Height = .picGraphic.Height + 2025
    End With
    
    CenterForm frmDirections
    Err.Clear
    frmDirections.Show vbModal
End Sub

Sub Delay(Seconds As Single)
    'DESCRIPTION:
    '   Delays the program for a specified time
    'PARAMETERS:
    '   Seconds! = number of seconds to delay
    'EXAMPLE:
    '           Delay 2.3
    Dim t#
    t# = dGetTime("Now")
    Do While dGetTime("Now") - t# < Seconds
        DoEvents
    Loop

End Sub

Function GatherIniFileInformation(lpApplicationName$, lpKeyName$, lpDefault$) As String
'************************************************************
'* ManTech Test Systems Software Module                     *
'************************************************************
'* Nomenclature   : System Monitor  [SystemStartUp]         *
'* Written By     : David W. Hartley                        *
'*    DESCRIPTION:                                          *
'*     Finds a value on in the TETS.INI File                *
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

Dim lpReturnedString As String * 255 'Return Buffer
Dim nSize As Long 'Return Buffer Size
Dim lpFileName As String 'INI File Key Name "Key=?"
Dim ReturnValue As Long 'Return Value Buffer
Dim FileNameInfo As String 'Formatted Return String
Dim WindowsDirectory As String 'Windows Directory API Query
Dim lpString As String 'String to write to INI File
    
    'Clear String Buffer
    lpReturnedString$ = Space$(255)
    'Find Windows Directory
    ReturnValue& = GetWindowsDirectory(ByVal lpReturnedString$, ByVal 255)
    FileNameInfo$ = Trim(lpReturnedString$)
    FileNameInfo$ = Mid$(FileNameInfo$, 1, Len(FileNameInfo$) - 1)
    WindowsDirectory$ = FileNameInfo$
    lpFileName$ = WindowsDirectory$ & "\TETS.INI"
    nSize& = 255
    lpReturnedString$ = ""
    FileNameInfo$ = ""
    'Find File Locations
    ReturnValue& = GetPrivateProfileString(ByVal lpApplicationName$, ByVal lpKeyName$, ByVal lpDefault$, ByVal lpReturnedString$, ByVal nSize&, ByVal lpFileName$)
    FileNameInfo$ = Trim(lpReturnedString$)
    FileNameInfo$ = Mid$(FileNameInfo$, 1, Len(FileNameInfo$) - 1)
    'If File Locations Missing, then create empty keys
    If FileNameInfo$ = lpDefault$ + Chr$(0) Or FileNameInfo$ = lpDefault$ Then
        lpString$ = " "
        ReturnValue& = WritePrivateProfileString(ByVal lpApplicationName$, ByVal lpKeyName$, ByVal lpString$, ByVal lpFileName$)
    End If
    
    'Return Information In INI File
    GatherIniFileInformation = FileNameInfo$
    
End Function

Function InitInstrument(iIndex As Integer) As Boolean
  'DESCRIPTION:
  '   This routine acquires a handle and initializes a VXI instrument
  '   Assumes that if the handle is non-zero, it is already open
  '   Resets the instrument
  'PARAMETERS:
  '   iIndex: Global index for instrument data arrays
  'RETURNS:
  '   True of False for Success or Failure
  
  Dim MsgRespReg%, Items%, i%
  Dim AllMsg$
  Dim InitStatus&, ErrorStatusRead&, lBytesRead As Long
  Dim ManufID%, DevType%, StatusReg%
  Dim ModelNum As Long
  Dim sIDList() As String
  Dim lDateCode As Long
  
  If (Not bLiveMode) Then
    InitInstrument = True
    If (iIndex = RF_STIM) Then
      'Get path to Corrections file (PLC.DAT)
      lpBuffer$ = ""
      lBytesRead = GetPrivateProfileString("File Locations", "PLC_DATA", "", lpBuffer, Len(lpBuffer), sTetsIni)
      If lBytesRead <= 0 Then
        sMsg = "Error locating PLC_DATA key in TETS.INI needed for the " & sOemName(iIndex)
        GoTo FailedInit
      End If
      sPlcPath = Left$(lpBuffer, lBytesRead)
    End If
    If (iIndex = RF_PM) Then
        gRFPm.open cSIMULATION
        'Turn on Power meter
        checkPMError gRFPm.setPwrMeterOn_Off(cPM_STATE_ON)
    End If
    Exit Function
  End If
  InitInstrument = True  'Until proven otherwise
  GetRM_Handle
  
  Select Case iIndex
    Case RF_PM
        If lInstrumentHandle(iIndex) Then     ' CODE_CHECK need to assign lInstrumentHandle(iIndex) to 1
            'Turn on Power meter
            checkPMError gRFPm.setPwrMeterOn_Off(cPM_STATE_ON)
            Exit Function
        End If
        ' Open a session to the Power Meter
        lSystErr = gRFPm.open(cEXECUTION)
        checkPMError lSystErr
        If (lSystErr = 0) Then
           lInstrumentHandle(iIndex) = True
           checkPMError gRFPm.setPwrMeterOn_Off(cPM_STATE_ON)
           checkPMError gRFPm.setMeasureMode(cABSOLUTE)
        Else
           lInstrumentHandle(iIndex) = False
           'GoTo FailedInit         CODE_CHECK: put this back. it is for development/debug only.
        End If
        
        'Get Serial Number information for the power meter
        checkPMError gRFPm.setPowerHead("8481A")
        checkPMError gRFPm.getPowerHeadSerNum(s8481A_SN)
        checkPMError gRFPm.setPowerHead("8481D")
        checkPMError gRFPm.getPowerHeadSerNum(s8481D_SN)
        checkPMError gRFPm.getAttenuatorSerNum(s11708A_SN)
 
    Case RF_STIM
      If lInstrumentHandle(iIndex) Then
        lSystErr = eip114x_reset(lInstrumentHandle(iIndex))
        Exit Function
      End If
      lSystErr = eip114x_init(sInstrumentSpec(iIndex), 1, VI_NULL, lInstrumentHandle(iIndex), ModelNum)
      If lSystErr Then
        Delay 1 'Try one more time, because VISA is sometimes unpredictable
        lSystErr = eip114x_init(sInstrumentSpec(iIndex), 1, VI_NULL, lInstrumentHandle(iIndex), ModelNum)
        If lSystErr Then
          sMsg = "Could not get instrument handle for the " & sOemName(iIndex)
          GoTo FailedInit
        End If
      End If
      lSystErr = eip114x_reset(lInstrumentHandle(iIndex))
      Delay 0.5
      WriteMsg iIndex, "*IDN?"
      lSystErr = ReadMsg(iIndex, sReadBuffer)
      Items% = StringToList(sReadBuffer, 1, sIDList(), ",")
      If InStr(Trim$(sIDList(MANUF)) & Trim$(sIDList(MODEL)), sIDNResponse(iIndex)) = 0 Then
        sMsg = sIDList(MANUF) & "," & sIDList(MODEL) & " found at " & sInstrumentSpec(iIndex) & vbCrLf
        sMsg = sMsg & "Expected " & sIDNResponse(iIndex)
        GoTo FailedInit
      End If
      
      'Power up language is HP, but you can't know what language it is in
      'when Cal program starts. So, send SCPI but ignore error since if it
      'is already in SCPI, it will generate an error. If not, it won't.
      'Either way, it gets set to SCPI.
      lSystErr = viWrite(lInstrumentHandle(iIndex), "SCPI", 4, lRetCount)
      Do  'Flush errors
        WriteMsg iIndex, "SYST:ERR?"
        lSystErr = ReadMsg(iIndex, sReadBuffer)
      Loop While Val(sReadBuffer) <> 0
      
      'Get path to Corrections file (PLC.DAT)
      lpBuffer$ = ""
      lBytesRead = GetPrivateProfileString("File Locations", "PLC_DATA", "", lpBuffer, Len(lpBuffer), sTetsIni)
      If lBytesRead <= 0 Then
        sMsg = "Error locating PLC_DATA key in TETS.INI needed for the " & sOemName(iIndex)
        GoTo FailedInit
      End If
      sPlcPath = Left$(lpBuffer, lBytesRead)

    Case SWITCHING
      If lInstrumentHandle(iIndex) Then
        WriteMsg iIndex, "RESET"
        Exit Function
      End If
      lSystErr = viOpen(lSessionHandle, sInstrumentSpec(iIndex), VI_NULL, VI_NULL, lInstrumentHandle(iIndex))
      If lSystErr Then
        Delay 1 'Try one more time, because VISA is sometimes unpredictable
        lSystErr = viOpen(lSessionHandle, sInstrumentSpec(iIndex), VI_NULL, VI_NULL, lInstrumentHandle(iIndex))
        If lSystErr Then
          sMsg = "Could not get instrument handle for " & sOemName(iIndex)
          GoTo FailedInit
        End If
      End If
      WriteMsg iIndex, "RESET"
      Delay 0.2
  
    Case Else
      MsgBox "Call program maintenance! Unsupported instrument index in 'InitInstrument': " & iIndex
      InitInstrument = False
  End Select
  
  DoEvents
  Exit Function
  
FailedInit:
  MsgBox sMsg
  bLiveMode = False
  InitInstrument = False
  If iIndex = RF_STIM Then
    lSystErr = eip114x_close(lInstrumentHandle(iIndex))
  Else
    lSystErr = viClose(lInstrumentHandle(iIndex))
  End If
  lInstrumentHandle(iIndex) = 0
  bFailedInit = True

End Function
Public Sub GetRM_Handle()
  
  If lSessionHandle <> 0 Then Exit Sub
  lSystErr = viOpenDefaultRM&(lSessionHandle)
  If lSystErr < 0 Then
    bLiveMode = False
    lSessionHandle = 0
  End If
  Delay 1

End Sub

Sub ReleaseHandles()
  Dim iIndex%
  
  If Not bLiveMode Then Exit Sub
  
  'These use a different close routine
  If lInstrumentHandle(RF_STIM) <> 0 Then
    lSystErr = eip114x_close(lInstrumentHandle(RF_STIM))
  End If
  
  'This operation closes the session handle and all instrument handles opened for this session.
  lSystErr = viClose(lSessionHandle)
  
  'Now, clear all handle variables, for later auto-handle-opening
  lSessionHandle = 0
  For iIndex% = 1 To LAST_INSTRUMENT
    lInstrumentHandle(iIndex%) = 0
  Next iIndex%
  
End Sub

Private Sub InitVar()
  ' DESCRIPTION:
  '   Initializes program variables and data structures
  
  Dim i As Integer
  
  sQuote = Chr$(34)
  
  sIDNResponse(RF_STIM) = "50000"
  sInstrumentSpec(RF_STIM) = "VXI::20"
  sInstrumentSpec(SWITCHING) = "VXI::38"
  
  'These are the OEM (Original Equipment Manufacturer) names
  sOemName(RF_STIM) = "EIP 114X VXIbus Microwave Synthesizer"
  sOemName(SWITCHING) = "Racal 1260 Series Switch Controller"
'  sOemName(SWITCH2) = "Racal 1260-39 Multi-Purpose Switch #1 (Slot 6)"
'  sOemName(SWITCH3) = "Racal 1260-39 Multi-Purpose Switch #2 (Slot 7)"
'  sOemName(SWITCH4) = "Racal 1260-58 Medium Frequency Switch (Slot 8)"
'  sOemName(SWITCH5) = "Racal 1260-66 Radio Frequency Switch (Slot 9)"
'  sOemName(MFSWITCH) = "Racal 1260-58 RF Multiplexer"
'  sOemName(HFSWITCH) = "Racal 1260-66A Microwave Switch"
  
  'These are the TETS instrument names
  sTetsName(RF_PM) = "RF Power Meter"
  sTetsName(RF_STIM) = "RF Stimulus"
  sTetsName(SWITCHING) = "Switching"
  
  'These match lexan labels, or funnel tags
  sLabelName(RF_PM) = "POWER METER"
  sLabelName(RF_STIM) = "RF STIM"
  sLabelName(SWITCHING) = "LF-1 SWITCH"
  
  'These match the SAIF instrument group labels
  sSaifName(RF_PM) = "[SAIF] RF POWER METER"
  sSaifName(RF_STIM) = "[SAIF] RF STIM"
  sSaifName(SWITCHING) = "[SAIF] UTILITY"
  
  sIniKey(RF_PM) = "RFP"
  sIniKey(RF_STIM) = "RFS"
  sIniKey(SWITCHING) = ""

End Sub

Function Extract(FromString$, Item$) As String
  Dim i%
  Dim C$
  
  Select Case UCase(Item$)
    Case "FILENAME"
      For i% = Len(FromString$) To 1 Step -1
        C$ = Mid$(FromString$, i%, 1)
        If (C$ = ".") Or (C$ = "\") Then Exit For
      Next i%
      If C$ = "." Then    'Found an extension before a backslash
        FromString$ = Left$(FromString$, i% - 1)
      End If
      For i% = Len(FromString$) To 1 Step -1
        If Mid$(FromString$, i%, 1) = "\" Then Exit For
      Next i%
      Extract = Mid$(FromString$, i% + 1)
  
    Case "FILENAME_EXTENSION"
      For i% = Len(FromString$) To 1 Step -1
        If Mid$(FromString$, i%, 1) = "\" Then Exit For
      Next i%
      Extract = Mid$(FromString$, i% + 1)
  
    Case "EXTENSION"
    Case "PATH"
      For i% = Len(FromString$) To 1 Step -1
        If Mid$(FromString$, i%, 1) = "\" Then Exit For
      Next i%
      Extract = Mid$(FromString$, 1, i%)
  
    Case "DRIVE"
    
    Case Else
        MsgBox "Error: Unsupported 'item' in 'Extract' function.", vbCritical
            
  End Select
End Function

Sub WaitForResponse(instrumentIndex%, ByVal iSeconds As Integer, Msg$)
  'DESCRIPTION
  '   Waits for a message to be placed in the instruments output buffer
  '   Shows a message on the status bar
  '   When done, restores original status bar message
  'PARAMETERS:
  '   InstrumentIndex%: The Index of the instrument for which to wait
  '   iSeconds: The maximum number of seconds to wait
  '   Msg$: The message to display on the status bar
  'GLOBAL VARIABLES MODIFIED:
  '   bTimedOut
  
  Dim MsgRespReg%, iStoreState%, iStoreMax%, iStoreVal%
  Dim sTmpMsg As String
  Dim sWaitTime As String
  Dim lStartTime As Long
  Dim lPmCalState As Long
  
  If iSeconds > 89 Then
    sWaitTime = CStr(iSeconds / 60) & " minutes"
  ElseIf iSeconds > 59 Then
    sWaitTime = "1 minute"
  Else
    sWaitTime = CStr(iSeconds) & " seconds"
  End If
  If Msg$ <> "" Then
    iStoreState = frmPLC.proProgressBar.Visible
    iStoreVal = frmPLC.proProgressBar.Value
    iStoreMax = frmPLC.proProgressBar.Max
    frmPLC.proProgressBar.Visible = True
    frmPLC.proProgressBar.Value = 0
    frmPLC.proProgressBar.Max = iSeconds
    sTmpMsg = frmPLC.sbrInfoBar.SimpleText
    MainMenuMsg Msg$ & ", Please Wait up to " & sWaitTime & "..."
  End If

  bTimedOut = True
  lStartTime = dGetTime("Now")
  Do While (lStartTime + iSeconds) > dGetTime("Now")
    'start Power meter section
    If (instrumentIndex = RF_PM) Then
       checkPMError gRFPm.getCalState(lPmCalState)
        If (lPmCalState = cZERO_CAL_NONE) Then
            bTimedOut = False
            Exit Do
        End If
        If (lPmCalState = -1) Then
            bTimedOut = True
            frmPLC.proProgressBar.Visible = iStoreState
            frmPLC.proProgressBar.Max = iStoreMax
            frmPLC.proProgressBar.Value = iStoreVal
            MainMenuMsg sTmpMsg
            Exit Do
        End If
    End If
    'end Power Meter section
    If (instrumentIndex <> RF_PM) Then
          'Check to see if Data Output Ready (DOR) bit goes high
          lSystErr = viIn16(lInstrumentHandle(instrumentIndex%), VI_A16_SPACE, MSG_RESP_REG, MsgRespReg%)
          If (MsgRespReg% And DOR_MASK) <> 0 Then
            bTimedOut = False
            Exit Do
          End If
          If iSelMode = USER_TERMINATE Then Exit Do
          If Msg$ <> "" Then frmPLC.proProgressBar.Value = CInt(dGetTime("Now") - lStartTime)
          Delay 1
    End If
    Loop
    If (instrumentIndex <> RF_PM) Then
        If Msg$ <> "" Then
          frmPLC.proProgressBar.Visible = iStoreState
          frmPLC.proProgressBar.Max = iStoreMax
          frmPLC.proProgressBar.Value = iStoreVal
          MainMenuMsg sTmpMsg
        End If
    End If
End Sub
Function Fetch(instrumentIndex%, measValue As Double) As Long
  'DESCRIPTION:
  '   This routine retrieves a measurement from a specified instrument,
  '     and stores it in Global dMeasured.
  '   It also sets bTimedOut if a timeout occurs.
  'PARAMETERS:
  '   InstrumentIndex%  = The handle to the instrument from which to
  '    obtain the measurement
  'RETURNS:
  '   VISA Error code
  'GLOBAL VARIABLES MODIFIED:
  '   dMeasured   sMeasured    bTimedOut
  Dim Measured As Single
  Dim sUnits As String
  sUnits = Space(4)
  bTimedOut = False
  
  If (instrumentIndex = RF_PM) Then
    lSystErr = gRFPm.getMeasurement(Measured, sUnits)
    If (lSystErr = 0) Then measValue = Measured
    If (lSystErr = cTIMEOUTERROR) Then Measured = MAX_LOW
        checkPMError lSystErr
  Else
    lSystErr = ReadMsg(instrumentIndex%, sMeasured)

    Select Case lSystErr
      Case VI_SUCCESS
        measValue = Val(sMeasured)
      Case VI_ERROR_TMO
        measValue = MAX_LOW
        bTimedOut = True    'Set the 'TIMED OUT' flag for the 'TEST' sub
      Case Else
        measValue = MAX_LOW
    End Select
  End If
  Fetch = lSystErr

End Function

Public Function EngNotate(ByVal Number#, UOM$) As String
    'DESCRIPTION:
    '   Returns passed number as numeric string in Engineering notation (every
    '   3rd exponent) with selectable precision along with Unit Of Measure.
    '   Prepadded to 15 characters.
    'PARAMETERS:
    '   Number#:    The number to be formatted
    
    Dim Multiplier%, Negative%, Digits%
    Dim Prefix$, ReturnString$
    
    If Number# > 1E+99 Then
        EngNotate = "Over Range"
        Exit Function
    ElseIf (Abs(Number#) < 1E-99) And (Number# <> 0) Then
        EngNotate = "Under Range"
        Exit Function
    ElseIf InStr(UCase$(UOM$), "DB") Or (UOM$ = "%") Then
        EngNotate = Format$(Number, "0.0######") & UOM$
        Exit Function
    ElseIf UCase$(UOM$) = "RATIO" Then
        EngNotate = Format$(Number, "0.0#######")
        Exit Function
    ElseIf UCase$(UOM$) = "PPM" Then
        EngNotate = Format$(Number, "0.00000") & UOM$
        Exit Function
    End If
    
    Multiplier% = 0: Negative% = False  'Initialize local variables
    
    If Number < 0 Then      ' If negative
        Number = Abs(Number)    'Make it positive for now
        Negative% = True        'Set flag
    End If
    
    If Number >= 1000 Then                    'For positive exponent
        Do While Number >= 1000 And Multiplier <= 4
            Number = Number / 1000
            Multiplier% = Multiplier% + 1
        Loop
    ElseIf Number < 1 And Number <> 0 Then    'For negative exponent (but not 0)
        Do While Number < 1 And Multiplier >= -6
            Number = Number * 1000
            Multiplier% = Multiplier% - 1
        Loop
    End If
    
    Select Case Multiplier%
        Case 4:  Prefix$ = "T"     'tera   E+12
        Case 3:  Prefix$ = "G"     'giga   E+09
        Case 2:  Prefix$ = "M"     'mega   E+06
        Case 1:  Prefix$ = "k"     'kilo   E+03
        Case 0:  Prefix$ = ""      '<none> E+00
        Case -1: Prefix$ = "m"     'milli  E-03
        Case -2: Prefix$ = Chr$(181)     'micro  E-06
        Case -3: Prefix$ = "n"     'nano   E-09
        Case -4: Prefix$ = "p"     'pico   E-12
        Case -5: Prefix$ = "f"     'femto  E-15
        Case -6: Prefix$ = "a"     'atto   E-18
        Case Else:
            Prefix$ = ""
            Number = 0
            Multiplier = 0
    End Select
    
    If Negative% Then Number = -Number
    
    If Multiplier > 4 Then
        ReturnString$ = "Over Range"
    Else
        Number = Val(Str$(Number))      ' Clear out very low LSBs from binary math
        ReturnString$ = Format$(Number, "0.0######")
    End If
    
    EngNotate = ReturnString$ & Prefix$ & UOM$

End Function
Function ReadMsg(instrumentIndex%, ReturnMessage$) As Long
  'DESCRIPTION:
  '   This Routine is a pass through to the VISA layer using VB conventions to
  '   facilitate clean Word Serial read communications to message based instruments.
  'PARAMETERS:
  '   instrumentIndex% = the index of the instrument from which to read
  '   ReturnMessage$ = the string returned to VB
  'RETURNS:
  '   A long integer representing any errors that may occur
  
  Dim errorMessage$, Dummy&, iHandle&, Seconds&

  If (Not bLiveMode) Then
    ReadMsg = 0
    Exit Function
  End If
  
  iHandle& = lInstrumentHandle(instrumentIndex%)
  sReadBuffer = ""
  DoEvents
  lSystErr = viRead(iHandle&, sReadBuffer, 256, lRetCount)
  DoEvents
  Select Case lSystErr
    Case VI_SUCCESS
      ReturnMessage$ = StripNullCharacters(sReadBuffer)
    Case VI_ERROR_TMO
      If iSelMode <> USER_TERMINATE Then
        Dummy& = viGetAttribute(iHandle&, VI_ATTR_TMO_VALUE, Seconds&)
        Seconds& = Seconds& / 1000   'Convert ms to s
        MsgBox "Exceeded time limit of" & Str$(Seconds&) & " seconds while reading from " & sTetsName(instrumentIndex%) & "." & vbCrLf & "Measurement invalid.", vbCritical, "VISA Error Message"
      End If
    Case Else
      If iSelMode <> USER_TERMINATE Then
        Dummy& = viStatusDesc(iHandle&, lSystErr, lpBuffer$)
        MsgBox "Error Message:" & vbCrLf & StripNullCharacters(lpBuffer$), vbCritical, "VISA Error Message"
      End If
  End Select
  
  ReadMsg = lSystErr
End Function

Function StripNullCharacters(Parsed$) As String
    'DESCRIPTION:
    '   This routine strips characters with ASCII values less than 32 from the
    '   end of a string
    'PARAMTERS:
    '   Parsed$ = String from which to remove null characters
    'RETURNS:
    '   The resultant parsed string
    
    Dim x&
    
    For x = Len(Parsed$) To 1 Step -1
        If Asc(Mid$(Parsed$, x, 1)) > 32 Then
            Exit For
        End If
    Next x
    StripNullCharacters = Left$(Parsed$, x)
End Function

Sub WriteMsg(ByVal instrumentIndex%, MessageToSend$)
  'DESCRIPTION:
  '   This Routine is a pass through to the VISA layer using VB conventions to
  '   facilitate clean Word Serial write communications to message based instruments.
  'PARAMETERS:
  '   IHandle& = the handle to the instrument to write
  '   MessageToSend$ = the string of data to be written
  'EXAMPLE:
  '   WriteMsg DMMHandle&, "*TST?"
  
  Dim errorMessage$, iHandle&
      
  iWriteMsgError = 0
  If (Not bLiveMode) Then Exit Sub
  
  DoEvents
  If lInstrumentHandle(instrumentIndex%) = 0 Then
    lSystErr = InitInstrument(instrumentIndex%)
  End If
  iHandle& = lInstrumentHandle(instrumentIndex%)
  
  lSystErr = viWrite(iHandle&, MessageToSend$, Len(MessageToSend$), lRetCount)
  If lSystErr Then
    lSystErr = viStatusDesc(iHandle&, lSystErr, lpBuffer$)
    MsgBox "Error sending:" & vbCrLf & MessageToSend$ & " to " & sTetsName(instrumentIndex%) & vbCrLf & "Error Message:" & vbCrLf & StripNullCharacters(lpBuffer$), vbExclamation, "VISA Error Message"
    iWriteMsgError = True
  Else
    Select Case instrumentIndex%
      Case RF_PM, RF_STIM
        Do
          DoEvents
          If InStr(MessageToSend$, "?") <> 0 Then Exit Do    ' If expecting a response
          If InStr(MessageToSend$, "*CLS") <> 0 Then Exit Do
          If InStr(MessageToSend$, "*RST") <> 0 Then Exit Do
          lSystErr = viWrite(iHandle&, "SYST:ERR?", 9, lRetCount)
          lSystErr = ReadMsg(instrumentIndex%, errorMessage$)
          If Val(errorMessage$) = 0 Then Exit Do
          iWriteMsgError = Val(errorMessage$)
          MsgBox "Error sending:" & vbCrLf & MessageToSend$ & vbCrLf & "Error:" & errorMessage$, vbExclamation, sTetsName(instrumentIndex%) & " Error Message"
        Loop
    End Select
  End If
End Sub

Sub CenterForm(Form As Object)
'************************************************************
'* ManTech Test Systems Software Module                     *
'************************************************************
'* Nomenclature   : TETS SYSTEM SELF TEST                   *
'* Written By     : Michael McCabe                          *
'*    DESCRIPTION:                                          *
'*     This Module Centers One Form With Respect To The     *
'*     User's Screen.                                       *
'*    EXAMPLE:                                              *
'*     CenterForm frmMain                                   *
'************************************************************
    Form.Top = Screen.Height / 2 - Form.Height / 2
    Form.Left = Screen.Width / 2 - Form.Width / 2
End Sub

Public Sub Main()
    Dim SystErr&
    Dim DetectRF$, Msg$
    Dim Response%
        
    bLiveMode = True
    'bLiveMode = False      ' CODE_CHECK: simulation mode for development
    readPLCFile_if
    InitVar
    frmPLC.cmdClose.Caption = "&Close"
        
    DetectRF$ = GatherIniFileInformation("System Startup", "RF_OPTION_INSTALLED", "")
    If DetectRF$ <> "YES" Then
        Msg = "RF Option is not installed. Terminating program..."
        MsgBox Msg, vbExclamation + vbOKOnly
        End
    End If
    
    lpBuffer$ = ""
    lSystErr = GetWindowsDirectory(lpBuffer$, ByVal 80)
    sTetsIni = StripNullCharacters(lpBuffer$) & "\" & "tets.ini"
    
    'Build Graphics path
    GraphicsPath$ = GatherIniFileInformation("File Locations", "PLC_GEN", "")
    If GraphicsPath$ = "" Then
      MsgBox "Cannot find PLCGen.Exe file path. Hookup graphics not available."
    Else
      GraphicsPath$ = Extract(GraphicsPath$, "Path")
      GraphicsPath$ = GraphicsPath$ & "GRAPHICS\"
    End If
    
    frmPLC.proProgressBar.Visible = True
    
    CenterForm frmAbout
    frmAbout.lblInstrument(3).Caption = App.Major & "." & App.Minor
    frmAbout.Show
    Delay 2
    Unload frmAbout
    CenterForm frmPLC
    frmPLC.Show
    
    MainMenuMsg "Click Start button to run."
    DoEvents
    
End Sub
Public Sub Dispatch(iInst As Integer)
  'DESCRIPTION:
  '   This routine is called from the instrument click buttons on the main form.
  'PARAMETERS:
  '   iInst:  The index of the selected instrument

  frmPLC.SSPanel1.Enabled = False
  'bLiveMode = True
  bSetupTest iInst
  Adjustment iInst
  UpdateStatus iInst
  
  ReleaseHandles
  MainMenuMsg "Click Start button to run."
  frmPLC.SSPanel1.Enabled = True
  
End Sub

Public Sub SetCalDate(iInst As Integer)
  'DESCRIPTION:
  '   Sets "cal due" date in Tets.ini.
  'PARAMETERS:
  '   iInst:  Instrument index
  
  Dim lCalDate As Long, i As Integer
  Dim sDueDate As String
  Dim sJustification As String
  
  lCalDate = DateSerial(1 + Year(Now), Month(Now), Day(Now))
  
  'Now, write TETS.INI date keys
  sDueDate = CStr(lCalDate)  'Due date for TETS.INI date keys
  lSystErr = WritePrivateProfileString("Calibration", "HFS", sDueDate, sTetsIni)
  
End Sub

Public Sub UpdateStatus(iIdx As Integer)
  'DESCRIPTION
  '   Called after a test group has been executed
  '   Stops GUI activity blinking
  '   Updates frmplc GUI status
  'PARAMETERS:
  '   iIdx:   The Index of the instrument being tested
  '   iTest:  Test or Adjustment number just executed
  
  Dim i As Integer, j As Integer
  Dim sAdjustList() As String
  
  With frmPLC
  .timTimer.Enabled = False
  .lblTestName(iIdx).ForeColor = vbBlack
  
  .cmdClose.Caption = "&Close"
  If (Not bLiveMode) Or (iSelMode = SKIP_TEST) Or (iSelMode = USER_TERMINATE) Then
    .picSwitchPass(iIdx) = .picNoTest
    .picSwitchFail(iIdx) = .picNoTest
    Exit Sub
  End If
  
  If bFailedInit Then
    If bFailedInit Then MsgBox "Instrument failed to initialize, cannot continue."
    iSelMode = USER_TERMINATE
    Exit Sub
  End If
  
  If .lblTestName(iIdx).Tag = "Passed" Then
    .picSwitchPass(iIdx) = .picTestPassed
    .picSwitchFail(iIdx) = .picNoTest
    SetCalDate iIdx
  Else
    .picSwitchPass(iIdx) = .picNoTest
    .picSwitchFail(iIdx) = .picTestFailed
  End If
  End With
  
End Sub

Function dGetTime(sKey As String) As Double
    'DESCRIPTION:
    '   Determines the number of seconds between 1997 and a time.
    '   Avoids errors caused by 'Timer' when crossing midnight.
    'PARAMETERS:
    '   sKey: "Now" or
    '         TETS.INI System Startup key name:
    '         "STARTUP_TIME":   The last time the FPU was turned on.
    '         "SHUTDOWN_TIME":  The last time the FPU was turned off.
    'RETURNS:
    '   The Number of seconds between 00:00 Jan 1, 1997 and the specified time.
    
    Dim lBytesRead As Long
    Dim sTime As String
    Dim Items%, LeapYears%, YearsInSeconds#, DaysInSeconds#, HoursInSeconds#, MinutesInSeconds%, Seconds%, SecondsSince1997#
    
    If UCase(sKey) = "NOW" Then
      sTime = Now
    Else  'Else its a TETS.INI key
      lpBuffer$ = ""
      lBytesRead = GetPrivateProfileString("System Startup", sKey, "", lpBuffer$, 256, sTetsIni)
      If lBytesRead < 1 Then
        dGetTime = 0
        Exit Function
      End If
      sTime = Left$(lpBuffer$, lBytesRead)
    End If
    
    LeapYears% = (Val(Format$(sTime, "yyyy")) - 1995) \ 4
    YearsInSeconds# = (((Val(Format$(sTime, "yyyy")) - 1997) * 365) + LeapYears%) * 24 * 60 * 60
    DaysInSeconds# = (Val(Format$(sTime, "y")) - 1) * 24 * 60 * 60
    HoursInSeconds# = (Val(Format$(sTime, "h"))) * 60 * 60
    MinutesInSeconds% = (Val(Format$(sTime, "n"))) * 60
    Seconds% = Val(Format$(sTime, "s"))
    
    SecondsSince1997# = YearsInSeconds# + DaysInSeconds# + HoursInSeconds# + MinutesInSeconds% + Seconds%
    dGetTime = SecondsSince1997#
End Function

Public Function bSetupTest(iInstr As Integer) As Boolean
  'Performed before every Adjustment
  
  DoEvents
  bSetupTest = True
  bFailedAny = False
  bFailedInit = False
  bTimedOut = False
  iSelMode = NO_OP
  
  MainMenuMsg "Performing " & frmPLC.lblTestName(iInstr).Caption
  If GetVxiChassisPowerState(False) = False Then
    bInstruct "Turn on chassis power by pressing the green INSTRUMENT CHASSIS POWER button and wait for Resource Manager to finish."
  End If
  
  With frmPLC
  
  .timTimer.Tag = iInstr
  .timTimer.Enabled = True
  .picSwitchPass(iInstr) = .picNoTest
  .picSwitchFail(iInstr) = .picNoTest
  .cmdClose.Caption = "&Abort"
  End With

End Function
Function GetVxiChassisPowerState(bWaitFor As Boolean) As Boolean
  'DESCRIPTION:
  '   This routine checks the state of VXI Chassis Power and Resource Manager completion
  'PARAMETERS:
  '   bWaitFor:  If true, the function will not return until State is True
  'RETURNS:
  '   True:   VXI Chassis Power Button is ON and Resource Manager has completed
  '   False:  VXI Chassis Power Button is OFF or Resource Manager has NOT completed
  
  Dim sTmpMsg As String, lBytesRead As Long
  
  If bWaitFor Then sTmpMsg = frmPLC.sbrInfoBar.SimpleText
  
  Do
    lpBuffer$ = ""
    lBytesRead = GetPrivateProfileString("System Monitor", "CHASSIS_STATE", "", lpBuffer, 256, sTetsIni)
    
    If StripNullCharacters(lpBuffer$) = "ON" Or iSelMode = USER_TERMINATE Then
      GetVxiChassisPowerState = True
    ElseIf StripNullCharacters(lpBuffer$) = "" Then
      GetVxiChassisPowerState = False
    Else
      GetVxiChassisPowerState = False
      If bWaitFor Then MainMenuMsg "Waiting for INSTRUMENT CHASSIS POWER and Resource Manager to finish"
    End If
    DoEvents
  Loop Until GetVxiChassisPowerState = True Or bWaitFor = False
  
  If bWaitFor Then MainMenuMsg sTmpMsg
  
End Function

Public Sub MainMenuMsg(Msg$)
  frmPLC.sbrInfoBar.SimpleText = Msg$
End Sub

Public Sub BumpProgBar()
  On Error Resume Next
  frmPLC.proProgressBar.Value = frmPLC.proProgressBar.Value + 1
End Sub

Public Sub ArraySmooth(dSource() As Double, dDest() As Double, gPercent As Single)
  'DESCRIPTION:
  '   Creates a smoothed copy of the dSource in the dDest array.
  '   Used to remove phasing effects from path-loss sweep measurements.
  'PARAMETERS:
  '   dSource: Source array of type Double.
  '            Can be filled with the Array statement
  '            or a fixed array or a dynamic array
  '   dDest: Destination array of type Double.
  '          Must be a dynamic array.
  '   gPercent: The percentage of the array to smooth. Each smoothed point
  '             is the average of the number of points specified by gPercent.
  'EXAMPLE:
  '   ArraySmooth dProfile(), dSmoothedArray(), 10
  '   Each point in dSmoothedArray() is the average of the corresponding
  '   points in dProfile() from -5% to +5% of the array size.
  
  Dim iSmoIdx As Integer
  Dim iSpan As Integer
  Dim iNumAvgs As Integer
  Dim i As Integer


  ReDim dDest(LBound(dSource) To UBound(dSource))
  iSpan = CInt((gPercent / 200) * (1 + (UBound(dSource) - LBound(dSource))))
  For iSmoIdx = LBound(dSource) To UBound(dSource)
    iNumAvgs = 0
    For i = iSmoIdx - iSpan To iSmoIdx + iSpan
      If i < LBound(dSource) Then
        dDest(iSmoIdx) = dDest(iSmoIdx) + dSource(LBound(dSource))
      ElseIf i > UBound(dSource) Then
        dDest(iSmoIdx) = dDest(iSmoIdx) + dSource(UBound(dSource))
      Else
        dDest(iSmoIdx) = dDest(iSmoIdx) + dSource(i)
      End If
      iNumAvgs = iNumAvgs + 1
    Next i
    dDest(iSmoIdx) = dDest(iSmoIdx) / iNumAvgs
  Next iSmoIdx

End Sub

Private Function getSimulatedLoss(ByRef PLCSimData() As String, ByVal table As String, ByVal dataPos As Long, ByRef measValue As Double) As Long
'DESCRIPTION:
'   Obtains loss data to be used during development
'    or simulation modes
'PARAMETERS:
'   measType: indicates which table to get the loss
'    data from
    
    Dim retVal As Long
    Dim Message As String
    Dim CommaCount As Long
    Dim dataStart As Long
    Dim measValueString As String
    Dim strLen As Long
    Dim pos As Long
    Dim tmps As String
    
    If (bLiveMode) Then Exit Function
    
    'Need to find string len of table
    strLen = Len(table)
    
    'Find the table
      For i = LBound(PLCSimData) To UBound(PLCSimData)
        If Left$(PLCSimData(i), strLen) = Left$(table, strLen) Then
          'table(i) = sLine
          Exit For
        End If
      Next i
    'If table not found, return an error
      If i > UBound(PLCSimData) Then
        Message = table + "not found in PLCSim.data"
        MsgBox Message, vbCritical, "Simulation PLC Data File Error"
      End If
    
    'i points to the table, count the comma's until
    ' the correct element is found
    tmps = PLCSimData(i)
    pos = 1
    Do While (True)
        pos = InStr(pos, tmps, ",", vbTextCompare)
        If (pos = 0) Then
            MsgBox "dataPos not index not found in PLCSim.data", vbCritical, "Simulation PLC Data File Error"
            getSimulatedLoss = -1
            Exit Function
        End If
        If (CommaCount = dataPos) Then
            dataStart = pos + 1
            Exit Do
        End If
        CommaCount = CommaCount + 1
        pos = pos + 1
    Loop
    
    tmps = PLCSimData(i)
    pos = InStr(dataStart, tmps, ",", vbTextCompare)
    If (pos = 0) Then
        pos = Len(PLCSimData(i)) + 1
    End If
        
    tmps = Mid(PLCSimData(i), dataStart, pos - dataStart)
    measValue = tmps
              
End Function

Private Function getPLCPath(ByVal sLine) As Long
    
'DESCRIPTION:
'   Converts the line stings into constants used
'    by the PLCFileMgr dll
'PARAMETERS:
'   sLine: string representation of the path
    
    sLine = UCase(sLine)
    Select Case sLine
    
    Case "S901-2"
        getPLCPath = 0
    Case "S901-3"
        getPLCPath = 1
    Case "S901-4"
        getPLCPath = 2
    Case "S901-5"
        getPLCPath = 3
    Case "S901-6"
        getPLCPath = 4
    Case "S901-7"
        getPLCPath = 5
    Case "S902-2"
        getPLCPath = 6
    Case "S902-3"
        getPLCPath = 7
    Case "S902-4"
        getPLCPath = 8
    Case "S902-5"
        getPLCPath = 9
    Case "S902-6"
        getPLCPath = 10
    Case "S902-7"
        getPLCPath = 11
    Case "S903-2"
        getPLCPath = 12
    Case "S903-3"
        getPLCPath = 13
    Case "S903-4"
        getPLCPath = 14
    Case "S903-5"
        getPLCPath = 15
    Case "S903-6"
        getPLCPath = 16
    Case "S903-7"
        getPLCPath = 17
    Case "S904-2"
        getPLCPath = 18
    Case "S904-3"
        getPLCPath = 19
    Case "S904-4"
        getPLCPath = 20
    Case "S904-5"
        getPLCPath = 21
    Case "S904-6"
        getPLCPath = 22
    Case "S904-7"
        getPLCPath = 23
    Case "S905-2"
        getPLCPath = 24
    Case "S905-3"
        getPLCPath = 25
    Case "S905-4"
        getPLCPath = 26
    Case "S905-5"
        getPLCPath = 27
    Case "S905-6"
        getPLCPath = 28
    Case "S905-7"
        getPLCPath = 29
    Case "S906-2"
        getPLCPath = 30
    Case "S906-3"
        getPLCPath = 31
    Case "S906-4"
        getPLCPath = 32
    Case "S906-5"
        getPLCPath = 33
    Case "S906-6"
        getPLCPath = 34
    Case "S906-7"
        getPLCPath = 35
    Case "POWERMETER"
        getPLCPath = 36
    End Select

End Function

Private Sub checkPMError(ErrorCode As Long)
'DESCRIPTION:
  '   Check for RFMS power meter failures
  '   Displays a pop message when an error occurs
  'PARAMETERS:
  '   dSource: ErrorCode
  '            return code from any RFMS power meter method call
  
    Dim Message As String
    Dim ErrorSeverity As Long
    Dim ErrorDescr As String
    Dim MoreErrorInfo As String
    
    If ErrorCode <> 0 Then
        gRFPm.getError ErrorCode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256
        Message = "Error:  " & ErrorCode & " --" & ErrorDescr & ". " & MoreErrorInfo
        MsgBox Message, vbCritical, CRFMS_ERROR
    End If
End Sub

Private Sub CheckPLCFileMgrError(ErrorCode As Long)
'DESCRIPTION:
  '   Check for PLCFileMgr for errors
  '   Displays a pop message when an error occurs
  'PARAMETERS:
  '   dSource: ErrorCode
  '            return code from any PLCFileMgr method call
  
    Dim Message As String
    Dim ErrorSeverity As Long
    Dim ErrorDescr As String
    Dim MoreErrorInfo As String
    
    If ErrorCode <> 0 Then
        gRFPm.getError ErrorCode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256
        Message = "Error:  " & ErrorCode & " --" & ErrorDescr & ". " & MoreErrorInfo
        MsgBox Message, vbCritical, cPLCFILEMGR_ERROR
    End If

End Sub
