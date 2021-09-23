VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cDTS"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

'2011-10-11 bbamford
' Modified DTS.state routine to handle 32 pin groups, and report error for >32 pins.
'2011-10-11 Str10606 #6
'Added 9 HV pin routines:
'  HVDISCONNECT, HVIH, HVIL, HVIOX, HVOH, HVOL, HVRESET, HVSETSTATE, and HVSTATE.
' Also added StaticDigitalHV.bas file to be included in TP source code.

'2012-03-08 STR10606 #10
'Fixed iCount variable in HVState command (not HVIOX)

Public Sub AbortTest()
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "AbortTest"
    
    If bSimulation = True Then
        Exit Sub
    End If
    terM9_reset (nInstrumentHandle(DIGITAL))
    terM9_close (nInstrumentHandle(DIGITAL))
    
End Sub

Public Sub SetLevelSet(nSet As Long, _
                             VIH As Double, _
                             VIL As Double, _
                             VOH As Double, _
                             VOL As Double, _
                             Optional ChannelCard As Integer = 0, _
                             Optional IOH As Double = -0.01, _
                             Optional IOL As Double = 0.01, _
                             Optional VCOM As Double = 3.5, _
                             Optional slewRate As String = "MEDIUM")

    Dim iStatus As Long
    Dim dtiHandle As Long
    Dim nSlewRate As Long
    Dim nChannelCard As Long
    Dim bSetLevelSet As Boolean
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    bSetLevelSet = True
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "bSetLevelSet"
    
    If nSet > 1 Or nSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet nSet argument out of range."
        bSetLevelSet = False
        Err.Raise -1000
        Exit Sub
    End If
    If VIH > 5 Or VIH < -2 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VIH argument out of range."
        bSetLevelSet = False
        Err.Raise -1000
        Exit Sub
    End If
    If VIL > 5 Or VIL < -2 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VIL argument out of range."
        bSetLevelSet = False
        Err.Raise -1000
        Exit Sub
    End If
    If VOH > 5 Or VOH < -2 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VOH argument out of range."
        bSetLevelSet = False
        Err.Raise -1000
        Exit Sub
    End If
    If VOL > 5 Or VOL < -2 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VOL argument out of range."
        bSetLevelSet = False
        Err.Raise -1000
        Exit Sub
    End If
    If IOH > 0# Or IOH < -0.01 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet IOH argument out of range."
        bSetLevelSet = False
        Err.Raise -1000
        Exit Sub
    End If
    If IOL > 0.01 Or IOL < 0# Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet IOL argument out of range."
        bSetLevelSet = False
        Err.Raise -1000
        Exit Sub
    End If
    If VCOM > 5 Or VCOM < -2 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VCOM argument out of range."
        bSetLevelSet = False
        Err.Raise -1000
        Exit Sub
    End If
    
    slewRate = UCase(Left(slewRate, 1))
    Select Case UCase(slewRate)
        Case "L"
            nSlewRate = TERM9_SLEWRATE_LOW
        Case "M"
            nSlewRate = TERM9_SLEWRATE_MEDIUM
        Case "H"
            nSlewRate = TERM9_SLEWRATE_HIGH
        Case Else
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet SLEWRATE argument incorrect."
            bSetLevelSet = False
            Err.Raise -1000
            Exit Sub
    End Select
    
    Select Case ChannelCard
        Case 0
            nChannelCard = TERM9_SCOPE_SYSTEM
        Case 1, 2, 3
            nChannelCard = TERM9_SCOPE_CARD(ChannelCard)
        Case Else
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet ChannelCard argument incorrect."
            bSetLevelSet = False
            Err.Raise -1000
            Exit Sub
    End Select
    
    If bSimulation = True Then
        Exit Sub
    End If
    
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    iStatus = terM9_setLevelSet(dtiHandle, nChannelCard, nSet, VIH, VIL, VOH, VOL, IOH, IOL, VCOM, TERM9_SLEWRATE_MEDIUM)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet failed."
        Err.Raise -1000
        Exit Sub
    End If
    
End Sub

Public Sub resetInstr()

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "ResetInstr"
    
    If bSimulation = True Then
        Exit Sub
    End If

    terM9_reset nInstrumentHandle(DIGITAL)
    
End Sub

Public Sub CloseInstr()

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "CloseInstr"
    
    If bSimulation = True Then
        Exit Sub
    End If
    terM9_close nInstrumentHandle(DIGITAL)
    
End Sub

Public Function bInit(bDTSReset As Boolean) As Boolean

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "bInit"
    
    If bSimulation = True Then
        nInstrumentHandle(DIGITAL) = 0
        Exit Function
    End If
    bInit = INITInst.iInitDigital(bDTSReset)
    
End Function

Public Function bRunDTB(sDTBFile As String, _
                        Optional bReset As Boolean = True, _
                        Optional bPinState As Boolean = False, _
                        Optional bConcurrent As Boolean = False) As Boolean

    Dim dtiHandle As Long
    Dim nTestResults As Long
    Dim nStatus As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "bRunDTB"
    bRunDTB = True
    
    If nInstrumentHandle(DIGITAL) = 0 And Not bSimulation Then
        
        If Not INITInst.iInitDigital(bReset) Then
            Echo "INSTRUMENT ERROR - DTS failed to initialize."
            frmMain.txtMeasured.Text = "FAILED"
            bRunDTB = False
            Exit Function
        End If
    End If
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    If bReset Then
        If Not bSimulation Then nStatus = terM9_reset(nInstrumentHandle(DIGITAL)) Else nStatus = 0
        If Not bChkStatTerM9(nStatus) Then
            Echo "INSTRUMENT ERROR - DTS failed _setSystemEnable."
            frmMain.txtMeasured.Text = "FAILED"
            bRunDTB = False
            Exit Function
        End If
    End If
    
    If Not bSimulation Then nStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE) Else nStatus = 0
    If Not bChkStatTerM9(nStatus) Then
        Echo "INSTRUMENT ERROR - DTS failed _setSystemEnable."
        frmMain.txtMeasured.Text = "FAILED"
        bRunDTB = False
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If Not bSimulation Then
        nStatus = terM9_executeDigitalTest(dtiHandle, ProgramPath$ + sDTBFile, VI_FALSE, nTestResults)
    Else
        nStatus = 0
        nTestResults = 0
        Do
            nTestResults = CLng(InputBox("Command cmdDTS.bRunDTB peformed." & vbCrLf & "Enter 166 for Pass or 162 for FAIL Value:", "SIMULATION MODE"))
        Loop Until nTestResults = 166 Or nTestResults = 162
    End If
    If Not bChkStatTerM9(nStatus) Then
        Echo "INSTRUMENT ERROR - DTS failed _setSystemEnable."
        frmMain.txtMeasured.Text = "FAILED"
        bRunDTB = False
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case nTestResults
    
        Case TERM9_RESULT_PASS
            frmMain.txtMeasured.Text = "PASSED"
            bRunDTB = True
            
        Case TERM9_RESULT_FAIL
            frmMain.txtMeasured.Text = "FAILED"
            bRunDTB = False
            
        Case TERM9_RESULT_NOT_RUN
            frmMain.txtMeasured.Text = "FAILED"
            bRunDTB = False
            
    End Select

End Function

Public Function sDiagnostic(sDTBFile As String, _
                                sCircuitFile As String, _
                                nType As Long) As String

    Dim dtiHandle As Long
    Dim Proc As MAINMod.PROCESS_INFORMATION
    Dim Start As MAINMod.STARTUPINFO
    Dim lRet As Long
    Dim bCurrentQuit As Boolean
    Dim bCurrentMainMenu As Boolean
    
    bCurrentQuit = frmMain.Quit.Enabled
    bCurrentMainMenu = frmMain.MainMenu.Enabled
    frmMain.Quit.Enabled = False
    frmMain.MainMenu.Enabled = False
    
    dtiHandle = nInstrumentHandle(DIGITAL)

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "sDiagnostic"
    
    If nType > 3 Or nType < 1 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.sDiagnostics nType argument out of range."
        Err.Raise -1000
        Exit Function
    End If
    
    bChkStatDM DM_setDiagnosticEnable(DIAG_TYPE_BOUNDARY_SCAN, DIAG_DISABLE)
    
    Select Case nType
        
        Case SEEDED_GUIDED_PROBE
            bChkStatDM DM_setDiagnosticEnable(DIAG_TYPE_FAULT_DICTIONARY, DIAG_ENABLE)
            bChkStatDM DM_setDiagnosticEnable(DIAG_TYPE_GUIDED_PROBE, DIAG_ENABLE)
            
        Case FAULT_DICTIONARY_ONLY
            bChkStatDM DM_setDiagnosticEnable(DIAG_TYPE_FAULT_DICTIONARY, DIAG_ENABLE)
            bChkStatDM DM_setDiagnosticEnable(DIAG_TYPE_GUIDED_PROBE, DIAG_DISABLE)
        
        Case GUIDED_PROBE_ONLY
            bChkStatDM DM_setDiagnosticEnable(DIAG_TYPE_FAULT_DICTIONARY, DIAG_DISABLE)
            bChkStatDM DM_setDiagnosticEnable(DIAG_TYPE_GUIDED_PROBE, DIAG_ENABLE)
    
    End Select
    
    bChkStatDM DM_setDiagnosticOutputDir(DIAGNOSTICS_DIRECTORY)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function

    bChkStatDM DM_setMaxSeedingFaultSetsValue(5)
    bChkStatDM DM_diagnoseTest(ProgramPath$ + sDTBFile, ProgramPath$ + sCircuitFile, "", dtiHandle)

    frmMain.Quit.Enabled = bCurrentQuit
    frmMain.MainMenu.Enabled = bCurrentMainMenu
    
End Function

Public Function bSystemAlignment() As Boolean

    Dim alignmentResult As Long
    Dim nStatus As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "bRunDTB"
    bSystemAlignment = True
    
    nStatus = terM9_executeSystemAlignment(nInstrumentHandle(DIGITAL), alignmentResult)
    If nStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.bSystemAligment failed.  Invalid Session Handle."
        Err.Raise -1000
        bSystemAlignment = False
        Exit Function
    End If
    
    Select Case alignmentResult
        
        Case TERM9_RESULT_PASS
            frmMain.txtMeasured.Text = "PASSED"
            bSystemAlignment = True
        
        Case TERM9_RESULT_FAIL
            frmMain.txtMeasured.Text = "FAILED"
            bSystemAlignment = False
        
        Case TERM9_RESULT_NOT_RUN
            frmMain.txtMeasured.Text = "FAILED"
            bSystemAlignment = False
      
      End Select
    
End Function

Private Function bChkStatTerM9(nFunctionStatus As Long) As Boolean

    Dim sStatusString As String * 256
    Dim nStatus As Long
    
    bChkStatTerM9 = True
    
    If nFunctionStatus <> VI_SUCCESS Then
        nStatus = terM9_errorMessage(nInstrumentHandle(DIGITAL), nFunctionStatus, sStatusString)
        
        If nStatus <> VI_SUCCESS Then
            Echo "DTS ERROR - Failed with unknown error."
        Else
            Echo "DTS ERROR - " & StripNullCharacters(sStatusString)
        End If
        
        AbortTest
        bChkStatTerM9 = False
    End If
    
End Function

Private Function bChkStatDM(nFunctionStatus As Long) As Boolean

    Dim sStatusString As String * 256
    Dim sSeverity As String * 1
    Dim nStatus As Long
    
    bChkStatDM = True
    
    If nFunctionStatus <> DIAG_SUCCESS Then
        nStatus = DM_errorMessage(nFunctionStatus, MAX_DIAG_TEXT_SIZE, sSeverity, sStatusString)
        
        Select Case nStatus
            
            Case DIAG_SUCCESS
            
            Case DIAG_WARN_BUFFER_TOO_SMALL
                Echo "DTS DIAGNOSTIC ERROR - Buffer overrun."
            
            Case Else
                Echo "DTS DIAGNOSTIC ERROR -Failed with unknown error."
        
        End Select
        
        bChkStatDM = False
    End If

End Function

Public Function nIDIdentification(ByRef IDpins() As Long) As Long

    'DESCRIPTION:
    '   This Routine measures the digital signature of IDpins.  This is accomplished by setting
    '   the active load voltage (VCOM) level to 3.5 VDC and grounding the pins to provide the
    '   digital signature
    'RETURNS:
    '   Integer value of measurement for the 8 digital pins
    
    Dim iCount As Integer
    Dim iStatus As Long
    Dim dtiHandle As Long
    Dim PinMapContents(1 To 8) As Long
    Dim ResultsList(1 To 8) As Long
    Dim nResultsCount As Long
    Dim SimulationDefault As Long

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "nIDIdentification"
    
    If bSimulation = True Then
        SimulationDefault = CLng(frmMain.txtUpperLimit.Text)
        nIDIdentification = CLng(InputBox("Enter ID Value:", "SIMULATION MODE", SimulationDefault))
        frmMain.txtMeasured.Text = nIDIdentification
        Exit Function
    End If
    
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    iStatus = terM9_reset(dtiHandle)

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    For iCount = 1 To 8
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(IDpins(iCount))
    Next iCount
    
    iStatus = terM9_setPinmapValues(dtiHandle, 1, nSizeOf(PinMapContents), PinMapContents(1))
    
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(1), TERM9_RELAY_CLOSED)
        
    ' Level set zero                                              VIH VIL VOH  VOL     IOH    IOL  VCOM
    iStatus = terM9_setLevelSet(dtiHandle, TERM9_SCOPE_SYSTEM, 0, 5#, 0#, 2#, 0.8, -0.0004, 0.008, 3.5, TERM9_SLEWRATE_MEDIUM)
    
    ' Level set one
    iStatus = terM9_setLevelSet(dtiHandle, TERM9_SCOPE_SYSTEM, 1, 5#, 0#, 2#, 0.8, -0.0004, 0.008, 3.5, TERM9_SLEWRATE_MEDIUM)
    iStatus = terM9_setChannelLevel(dtiHandle, 8, PinMapContents(1), 1)
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)

    ' Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(1), TERM9_CHANMODE_STATIC)

    ' Set the enable state of the active load of the channels
    iStatus = terM9_setChannelLoad(dtiHandle, nSizeOf(PinMapContents), PinMapContents(1), TERM9_LOAD_ON)
    iStatus = terM9_setPinmapGroup(dtiHandle, 203, nSizeOf(PinMapContents), PinMapContents(1))
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 203, TERM9_OP_IHOL)
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_TRUE)
    iStatus = terM9_fetchStaticPinResults(dtiHandle, nSizeOf(PinMapContents), PinMapContents(1), nResultsCount, ResultsList(1))

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Calculate and set Return Value
    nIDIdentification = 0

    For iCount = 1 To 8
    
        If ResultsList(iCount) = TERM9_RESULT_FAIL Then

          If iCount < 31 Then
            nIDIdentification = nIDIdentification Or (2 ^ (iCount - 1))
          Else
            nIDIdentification = nIDIdentification Or &H80000000
          End If
        End If
    Next iCount
    
    frmMain.txtMeasured.Text = nIDIdentification

End Function

Public Sub IOX(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "IOX"

    For iCount = 0 To UBound(nCh)

        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount

    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If

    If bSimulation = True Then
        Exit Sub
    End If

    dtiHandle = nInstrumentHandle(DIGITAL)

    ReDim PinMapContents(0 To UBound(nCh)) As Long

    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount

    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)

    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)

    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)

    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)

    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)

    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))

    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_IOX)

    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)

    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub DISCONNECT(ByRef nCh() As Long, Optional PerformVerify As Boolean = True)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim pinList() As Long
    Dim MinIndex As Long
    Dim MaxIndex As Long
    Dim PinCount As Long
    Dim VerifyStatus As Long
    
    '**************************************************************************************
    ' DISCONNECT                                                                          '
    '--------------------------------------------------------------------------------------
    ' This procedure will open the digital relay(s) of the channel(s) specified in nCH()  '
    '                                                                                     '
    ' If PerformVerify is allowed to be true then the procedure will also use a DTI-API   '
    ' function to verify that the specified channels actually opened to provided an       '
    ' extra level of certainty.  Failure of this procedure could result in damage to the  '
    ' DTI.                                                                                '
    '                                                                                     '
    ' Craig R. Weirich 05/19/03                                                           '
    '                                                                                     '
    '**************************************************************************************
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "DISCONNECT"

    'Set PinMap Values using nCh
    MinIndex = LBound(nCh, 1)
    MaxIndex = UBound(nCh, 1)
    PinCount = MaxIndex - MinIndex + 1
    
    For iCount = MinIndex To MaxIndex
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo _
            "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
        
    If bSimulation = True Then
        Exit Sub
    End If
    
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    ReDim pinList(0 To (PinCount - 1)) As Long
    
    'Convert to zero based array (if not all ready zero based) and to system relative channel numbers
    For iCount = MinIndex To MaxIndex
        pinList(iCount - MinIndex) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount

    'Open All relays of channels in nCh()
    iStatus = terM9_setChannelConnect(dtiHandle, PinCount, pinList(0), TERM9_RELAY_OPEN)
    If iStatus <> VI_SUCCESS Then
        Echo _
        "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_setChannelConnect function." + vbCrLf + _
        "The digital relays of the selected channels have not opened successfully!"
        Err.Raise -1000, , _
        "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_setChannelConnect function." + vbCrLf + _
        "The digital relays of the selected channels have not opened successfully!"
        Exit Sub
    End If
     
    'Perform Verify Unless Opted Out
    If PerformVerify Then
      For iCount = MinIndex To MaxIndex
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        
        'Check state of each channel to make sure it's open otherwise raise error
        iStatus = terM9_getChannelConnect(dtiHandle, TERM9_SCOPE_CHAN(nCh(iCount)), VerifyStatus)
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        
        If iStatus <> VI_SUCCESS Then
          Echo _
          "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_getChannelConnect function." + vbCrLf + _
          "Unable to verify if digital relay for channel " + nCh(iCount) + " has opened successfully!"
          Err.Raise -1000, , _
          "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_getChannelConnect function." + vbCrLf + _
          "Unable to verify if digital relay for channel " + nCh(iCount) + " has opened successfully!"
          Exit Sub
        End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
        If VerifyStatus <> TERM9_RELAY_OPEN Then
          Echo _
          "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_getChannelConnect function." + vbCrLf + _
          "The digital relay for channel " + nCh(iCount) + "could not be opened successfully!"
          Err.Raise -1000, , _
          "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_getChannelConnect function." + vbCrLf + _
          "The digital relay for channel " + nCh(iCount) + "could not be opened successfully!"
          Exit Sub
        End If
      
      Next iCount
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
End Sub

Public Sub IL(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "IL"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_IL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub IH(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "IH"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_IH)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub ML(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "ML"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If

    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_ML)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub MH(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "MH"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_MH)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub OL(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "OL"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_OL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub OH(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "OH"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_OH)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub OB(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "OB"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_OB)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub OK(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "OK"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_OK)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub ILOH(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "ILOH"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_ILOH)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub IHOL(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "IHOL"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_IHOL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub TOG(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "TOG"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_TOG)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub KEEP(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "KEEP"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_KEEP)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub SIG(ByRef nCh() As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "SIG"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_SIG)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setChannelPinOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Sub SetState(nCh() As Long, nState As Long, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    Dim nBitState() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "SetState"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Sub
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Sub
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    

    iStatus = terM9_setGroupGroupOpcode(dtiHandle, 300, TERM9_OP_IX, nState)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setGroupGroupOpcode function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Function state(nCh() As Long, Optional nLevelSet As Long = 0, _
                        Optional APROBE As String = "OFF", _
                        Optional bMisProbeMsg As Boolean = True) As Long
                        
    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim PinMapContents() As Long
    Dim nBitState() As Long
    Dim nDeskewRelay As Long
    Dim iOverTemperature As Integer
    Dim iReferenceOverload As Integer
    Dim SimulationDefault As Long

    frmMain.txtMeasured.Text = Space(99)
    frmMain.txtMeasured.BackColor = vbWhite
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "State"
    
    If UBound(nCh) > 31 Then
       Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State - Group pin count argument out of range (max=32)."
       state = -1: frmMain.txtMeasured.Text = -1
       Err.Raise -1000
       Exit Function
    End If
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State - Channel Pin " + nCh(iCount) + " argument out of range."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
            Exit Function
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State nLevelSet argument out of range."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If
    
    APROBE = UCase(APROBE)
    If UBound(nCh) > 0 And Left(APROBE, 1) <> "O" Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State APROBE argument connot be used with an Array of Channel Pins."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case Left(APROBE, 1)
        Case "O", "S", "C" ' Off, Single, Continuous
        Case Else
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State APROBE argument out of range."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
            Exit Function
    End Select

    If bSimulation = True Then
        SimulationDefault = CLng(frmMain.txtUpperLimit.Text)
        state = CDbl(InputBox("Command cmdDTS.state peformed." & vbCrLf & "Enter Word or Bit as an Integer value:", "SIMULATION MODE", SimulationDefault))
        frmMain.txtMeasured.Text = state
        Exit Function
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    nNumberOfChannelPins = UBound(nCh)
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setPinmapValues function."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setSystemEnable function."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelConnect function."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelLevel function."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelChanMode function."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setGroundReference function."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setPinmapGroup function."
            state = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
        Exit Function
    End If
    
    Select Case Left(APROBE, 1)
        Case "O" ' Off
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            state = 0
            ReDim nBitState(0 To UBound(nCh)) As Long
            For iCount = 0 To UBound(nCh)
                iStatus = terM9_fetchChannelState(dtiHandle, TERM9_SCOPE_CHAN(nCh(iCount)), _
                                                    nBitState(iCount), nDeskewRelay, _
                                                    iOverTemperature, iReferenceOverload)
                If iStatus <> VI_SUCCESS Then
                    Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_fetchChannelState function."
                    state = -1: frmMain.txtMeasured.Text = -1
                    Err.Raise -1000
                    Exit Function
                End If
                If nBitState(iCount) = TERM9_DETECTOR_HIGH Then
                    If iCount < 31 Then
                        state = state Or (2 ^ iCount)
                    Else
                        state = state Or &H80000000
                    End If
                End If
            Next iCount
            frmMain.txtMeasured.Text = state

        Case "S" ' Single
            MisProbe = vbYes
            Do While MisProbe = vbYes
                Failed = False
                UserEvent = 0
                bProbeClosed = False
                frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    DoEvents
                Loop
                frmMain.TimerProbe.Enabled = False
                bProbeClosed = False
                state = 0
                ReDim nBitState(0 To UBound(nCh)) As Long
                For iCount = 0 To UBound(nCh)
                    iStatus = terM9_fetchChannelState(dtiHandle, TERM9_SCOPE_CHAN(nCh(iCount)), _
                                                        nBitState(iCount), nDeskewRelay, _
                                                        iOverTemperature, iReferenceOverload)
                    If iStatus <> VI_SUCCESS Then
                        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_fetchChannelState function."
                        state = -1: frmMain.txtMeasured.Text = -1
                        Err.Raise -1000
                        Exit Function
                    End If
                    If nBitState(iCount) = TERM9_DETECTOR_HIGH Then
                        If iCount < 31 Then
                            state = state Or (2 ^ iCount)
                        Else
                            state = state Or &H80000000
                        End If
                    End If
                Next iCount
                frmMain.txtMeasured.Text = state
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                If Failed = True And bMisProbeMsg = True Then
                    MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                        "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                Else
                    MisProbe = vbNo
                End If
            Loop
            
        Case "C"
            MisProbe = vbYes
            Do While MisProbe = vbYes
                UserEvent = 0
                Failed = False
                frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    Sleep 1   'Causes the application to be idle for 1 mS - the rest of the loop probably executes in a few micro seconds.
                    DoEvents

                    state = 0
                    ReDim nBitState(0 To UBound(nCh)) As Long
                    For iCount = 0 To UBound(nCh)
                        iStatus = terM9_fetchChannelState(dtiHandle, TERM9_SCOPE_CHAN(nCh(iCount)), _
                                                            nBitState(iCount), nDeskewRelay, _
                                                            iOverTemperature, iReferenceOverload)
                        If iStatus <> VI_SUCCESS Then
                            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_fetchChannelState function."
                            state = -1: frmMain.txtMeasured.Text = -1
                            Err.Raise -1000
                            Exit Function
                        End If
                        If nBitState(iCount) = TERM9_DETECTOR_HIGH Then
                            If iCount < 31 Then
                                state = state Or (2 ^ iCount)
                            Else
                                state = state Or &H80000000
                            End If
                        End If
                    Next iCount
                    frmMain.txtMeasured.Text = state
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                Loop
                frmMain.TimerProbe.Enabled = False
                bProbeClosed = False

                If Failed = True And bMisProbeMsg = True Then
                    MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                        "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                Else
                    MisProbe = vbNo
                End If
            Loop
        
    End Select

End Function

Private Function Toggle(T As Boolean)

    If T = True Then Toggle = False
    If T = False Then Toggle = True

End Function


Private Function ConvertTeradyneState(TeradyneState As Long) As Boolean

    If TeradyneState = TERM9_DETECTOR_HIGH Then
        ConvertTeradyneState = True
    Else
        ConvertTeradyneState = False
    End If

End Function

Public Sub SetActiveLoad(ByRef nCh() As Long, Optional bState As Boolean = True, Optional nLevelSet As Long = 0)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim nState As Long
    Dim PinMapContents() As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "SetActiveLoad"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    ReDim PinMapContents(0 To UBound(nCh)) As Long
    For iCount = 0 To UBound(nCh)
        PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
    Next iCount
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    'Set Active Load State
    If bState = True Then nState = TERM9_LOAD_ON Else nState = TERM9_LOAD_OFF
    iStatus = terM9_setChannelLoad(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nState)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setChannelLoad function."
        Err.Raise -1000
        Exit Sub
    End If

End Sub

Public Function Frequency(nCh As Long, Optional nLevelSet As Long = 0, _
                          Optional MinimumAperture As Double, _
                          Optional HysterisisSeconds As Double = 0, _
                          Optional ByRef ActualAperture As Double, _
                          Optional ByRef ActualSampleRate As Double, _
                          Optional APROBE As String = "OFF", _
                          Optional bMisProbeMsg As Boolean = True) As Double
    
    ' DTS Frequency Counter
    ' Expected sample rate: 60 Ksps
    ' Assuming the sample rate performs to expectations:
    ' Capable of measuring the frequency of "clean, 50% duty cycle" TTL signals up to 30 KHz
    ' If not 50% duty cycle then the shorter pulse must be longer than 16.6 uS
    '--------------------------------
    '
    ' Inputs:
    '--------------------
    '
    ' nCh: DTS channel to measure the frequency at
    '
    ' nLevelSet: Which levelset to use.  This will determine the trigger level
    '            of the counter
    '
    ' MinimumAperture: The Aperture cannot be directly specified but it can be
    '                  controlled to some degree using this parameter.
    '
    ' HysterisisSeconds: Glitch rejection - Minimum duration of a pulse for it to
    '                    be consisdered in the frequency measurement. (default zero)
    '
    'Outputs:
    '--------------------
    '
    ' Frequency: Main return value - Returns the frequency measured with
    '            Hysterisis filter applied.
    '
    ' ActualAperture: Return Value of the actual Aperture of this digitization.
    '                 Will be greater than MinimumAperture by 0 to 50mS
    '
    ' ActualSampleRate: Return Value of the actual average sample rate of this digitization
    '
    '--------------------------------
    'WARNING
    'To maximize sample rate some live error checking was spared.
    'If the MinAperture causes the SampleCount to exceed the size of the Digitizeddata
    'An estimated high limit for MinimumAperture will be applied beforehand
    'A sample rate of 61 Ksps has been measured
    'Therefore with 10,000 posistions in the array the max MinimumAperture allowed will
    'be  S = ( 10,000 smpl ) / (61000 smpl / 1 S ) Or 163 mS
    '150 mS will be used
    'the MinimumAperture must also be greater than zero
    'as the aperture approaches zero it will be no smaller than the resolution of Timer()
    '
    'Hysterisis high limit will be set to 20 mS or the perioud of 50 Hz
    'it would seem absurd to set it much higher.
     
    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim nBitState() As Long
    Dim nDeskewRelay As Long
    Dim iOverTemperature As Integer
    Dim iReferenceOverload As Integer
    Dim PinMapContents(0 To 0) As Long
    Dim Pointer As Long
    Dim SampleCount As Long
    Dim Aperture As Double
    Dim TimeStamp As Double
    Dim SampleRate As Long
    Dim CurrentState As Boolean
    Dim CurrentSample As Long
    Dim HysterisisSamples As Long
    Dim HysterisisCounter As Long
    Dim TransitionCounter As Long
    Dim DigitizedData(1 To 100000) As Long
    Dim ScopeIndex As Long
    Dim SimulationDefault As Long

    frmMain.txtMeasured.Text = Space(99)
    frmMain.txtMeasured.BackColor = vbWhite
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "State"
    
    If nCh > 191 Or nCh < 0 Then
      Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State - Channel Pin " + nCh + " argument out of range."
      Err.Raise -1000
      Exit Function
    End If
    
    If nLevelSet > 1 Or nLevelSet < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State nLevelSet argument out of range."
        Err.Raise -1000
        Exit Function
    End If
    
    If MinimumAperture >= 0.15 Or MinimumAperture <= 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State MinimumAperture argument out of range."
        Echo "                        Allowable Range:  0 < MinimumAperture < .150"
        Exit Function
    End If
    
    If HysterisisSeconds > 0.02 Or HysterisisSeconds < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State HysterisisSeconds argument out of range."
        Echo "                        Allowable Range:  0 <= HysterisisSeconds <= .02"
        Err.Raise -1000
        Exit Function
    End If
    
    APROBE = UCase(APROBE)
    If nCh > 0 And Left(APROBE, 1) <> "O" Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State APROBE argument cannot be used with an Array of Channel Pins."
        Err.Raise -1001
        Frequency = 0
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    Select Case Left(APROBE, 1)
        Case "O", "S", "C" ' Off, Single, Continuous
        Case Else
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State APROBE argument out of range."
            Err.Raise -1001
            Frequency = 0
            Exit Function
    End Select

    If bSimulation = True Then
        SimulationDefault = CLng(frmMain.txtUpperLimit.Text)
        Frequency = CDbl(InputBox("Command cmdDTS.state peformed." & vbCrLf & "Enter Word or Bit as an Integer value:", "SIMULATION MODE", SimulationDefault))
        frmMain.txtMeasured.Text = Frequency
        Exit Function
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set PinMap Values using nCh
    PinMapContents(0) = TERM9_SCOPE_CHAN(nCh)
    iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setPinmapValues function."
        Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set System Enabled
    iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setSystemEnable function."
        Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Close output Relay
    iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelConnect function."
        Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Close output Relay
    iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelLevel function."
        Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set the pin-state-data mode
    iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelChanMode function."
        Err.Raise -1000
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    ' Set the systemwide ground reference source to INTERNAL.
    iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setGroundReference function."
        Err.Raise -1000
        Exit Function
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set the pin-state-data mode
    iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
    If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setPinmapGroup function."
        Err.Raise -1000
        Exit Function
    End If
    
    Select Case Left(APROBE, 1)
        Case "O" ' Off
            
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            Frequency = 0
            Pointer = 0
            
            ScopeIndex = PinMapContents(0)
            
            TimeStamp = Timer
            Do While Timer <= TimeStamp
            Loop
            TimeStamp = Timer
            Do While (Aperture - TimeStamp) < MinimumAperture
              Pointer = Pointer + 1
              terM9_fetchChannelState dtiHandle, ScopeIndex, _
                                      DigitizedData(Pointer), nDeskewRelay, _
                                      iOverTemperature, iReferenceOverload
              Aperture = Timer
            Loop
            Aperture = Aperture - TimeStamp
            SampleRate = Pointer / Aperture
            SampleCount = Pointer
            HysterisisSamples = CLng(HysterisisSeconds * SampleRate)
            
            CurrentSample = 1
            HysterisisCounter = 0
            TransitionCounter = 0
            CurrentState = ConvertTeradyneState(DigitizedData(1))
            For CurrentSample = 2 To SampleCount
              If ConvertTeradyneState(DigitizedData(CurrentSample)) = Toggle(CurrentState) Then
                HysterisisCounter = HysterisisCounter + 1
              Else
                HysterisisCounter = 0
              End If
              
              If HysterisisCounter = HysterisisSamples Then
                CurrentState = Toggle(CurrentState)
                TransitionCounter = TransitionCounter + 1
                HysterisisCounter = 0
              End If
            Next
                
            ActualSampleRate = SampleRate
            ActualAperture = Aperture
            Frequency = TransitionCounter / Aperture / 2
            frmMain.txtMeasured.Text = Frequency

        Case "S" ' Single
            MisProbe = vbYes
            Do While MisProbe = vbYes
                Failed = False
                UserEvent = 0
                bProbeClosed = False
                frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    DoEvents
                Loop
                frmMain.TimerProbe.Enabled = False
                bProbeClosed = False
                Frequency = 0
                Pointer = 0
                
                ScopeIndex = PinMapContents(0)
                
                TimeStamp = Timer
                Do While Timer <= TimeStamp
                Loop
                TimeStamp = Timer
                Do While (Aperture - TimeStamp) < MinimumAperture
                  Pointer = Pointer + 1
                  terM9_fetchChannelState dtiHandle, ScopeIndex, _
                                          DigitizedData(Pointer), nDeskewRelay, _
                                          iOverTemperature, iReferenceOverload
                  Aperture = Timer
                Loop
                Aperture = Aperture - TimeStamp
                SampleRate = Pointer / Aperture
                SampleCount = Pointer
                HysterisisSamples = CLng(HysterisisSeconds * SampleRate)
                
                CurrentSample = 1
                HysterisisCounter = 0
                TransitionCounter = 0
                CurrentState = ConvertTeradyneState(DigitizedData(1))
                For CurrentSample = 2 To SampleCount
                  If ConvertTeradyneState(DigitizedData(CurrentSample)) = Toggle(CurrentState) Then
                    HysterisisCounter = HysterisisCounter + 1
                  Else
                    HysterisisCounter = 0
                  End If
                  
                  If HysterisisCounter = HysterisisSamples Then
                    CurrentState = Toggle(CurrentState)
                    TransitionCounter = TransitionCounter + 1
                    HysterisisCounter = 0
                  End If
                Next
                    
                ActualSampleRate = SampleRate
                ActualAperture = Aperture
                Frequency = TransitionCounter / Aperture / 2
                frmMain.txtMeasured.Text = Frequency
                
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                If Failed = True And bMisProbeMsg = True Then
                    MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                        "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                Else
                    MisProbe = vbNo
                End If
            Loop
            
        Case "C"
            MisProbe = vbYes
            Do While MisProbe = vbYes
                UserEvent = 0
                Failed = False
                frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    Frequency = 0
                    Pointer = 0
                    
                    ScopeIndex = PinMapContents(0)
                    
                    TimeStamp = Timer
                    Do While Timer <= TimeStamp
                    Loop
                    TimeStamp = Timer
                    Do While (Aperture - TimeStamp) < MinimumAperture
                      Pointer = Pointer + 1
                      terM9_fetchChannelState dtiHandle, ScopeIndex, _
                                              DigitizedData(Pointer), nDeskewRelay, _
                                              iOverTemperature, iReferenceOverload
                      Aperture = Timer
                    Loop
                    Aperture = Aperture - TimeStamp
                    SampleRate = Pointer / Aperture
                    SampleCount = Pointer
                    HysterisisSamples = CLng(HysterisisSeconds * SampleRate)
                    
                    CurrentSample = 1
                    HysterisisCounter = 0
                    TransitionCounter = 0
                    CurrentState = ConvertTeradyneState(DigitizedData(1))
                    For CurrentSample = 2 To SampleCount
                      If ConvertTeradyneState(DigitizedData(CurrentSample)) = Toggle(CurrentState) Then
                        HysterisisCounter = HysterisisCounter + 1
                      Else
                        HysterisisCounter = 0
                      End If
                      
                      If HysterisisCounter = HysterisisSamples Then
                        CurrentState = Toggle(CurrentState)
                        TransitionCounter = TransitionCounter + 1
                        HysterisisCounter = 0
                      End If
                    Next
                        
                    ActualSampleRate = SampleRate
                    ActualAperture = Aperture
                    Frequency = TransitionCounter / Aperture / 2
                    frmMain.txtMeasured.Text = Frequency
                    
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                Loop
                frmMain.TimerProbe.Enabled = False
                bProbeClosed = False

                If Failed = True And bMisProbeMsg = True Then
                    MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                        "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                Else
                    MisProbe = vbNo
                End If
            Loop
        
    End Select

End Function

Public Sub HVDISCONNECT(ByRef nCh() As Long, Optional ByRef PerformVerify As Boolean = True)
    
    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim HVPinDriveState As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVDISCONNECT"

    For iCount = 0 To UBound(nCh)
      If nCh(iCount) > 31 Or nCh(iCount) < 0 Then
        Echo _
        "DTS PROGRAMMING ERROR:  Command cmdDTS.HVDISCONNECT - Channel Pin " + nCh(iCount) + " argument out of range."
        Err.Raise -1000
        Exit Sub
      End If
    Next iCount
        
    If bSimulation = True Then
        Exit Sub
    End If
    
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Open relay of each channel in nCh() array
    For iCount = 0 To UBound(nCh)
      iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_OFF)
      If iStatus <> VI_SUCCESS Then
          Echo _
          "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.HVDISCONNECT failed on terM9_setHighVoltagePinMode function." + vbCrLf + _
          "The digital relays of the selected channels have not opened successfully!"
          
          Err.Raise -1000, , _
          "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.HVDISCONNECT failed on terM9_setHighVoltagePinMode function." + vbCrLf + _
          "The digital relays of the selected channels have not opened successfully!"
          Exit Sub
      End If
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Next iCount
    
    If PerformVerify Then
      For iCount = 0 To UBound(nCh)
        
        'Check state of each channel to make sure it's off
        iStatus = terM9_getHighVoltagePinMode(dtiHandle, nCh(iCount), HVPinDriveState)
        If HVPinDriveState = TERM9_HVPINMODE_ON Then ' channel is supposed to be OFF
          Echo _
          "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.HVDISCONNECT failed on terM9_setHighVoltagePinMode function." + vbCrLf + _
          "The digital relays of the selected channels have not opened successfully!"
          
          Err.Raise -1000, , _
          "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.HVDISCONNECT failed on terM9_setHighVoltagePinMode function." + vbCrLf + _
          "The digital relays of the selected channels have not opened successfully!"
          Exit Sub
        End If
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
      Next iCount
    End If
   
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
End Sub

Public Sub HVIOX(ByRef nCh() As Long)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim HVPinIndex As Long
    Dim HVPinDriveState As Long
    
    '**************************************************************************************
    ' HVIOX (Pinstate = OFF)                                                                                '
    '   (Use channel specific commands to avoid resetting all 32 pins)                    '
    '**************************************************************************************
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVIOX"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 31 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVIOX - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    
    If bSimulation = True Then
        Exit Sub
    End If
    
    dtiHandle = nInstrumentHandle(DIGITAL)
    
   'set one channel at a time to IOX
   For iCount = 0 To UBound(nCh)
     HVPinIndex = nCh(iCount)
     iStatus = terM9_setHighVoltagePinMode(dtiHandle, HVPinIndex, TERM9_HVPINMODE_OFF)
     If iStatus <> VI_SUCCESS Then
         Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVIOX failed on terM9_setHighVoltagePinMode function."
         Err.Raise -1000
         Exit Sub
     End If
   Next iCount
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
End Sub

Public Sub HVIH(ByRef nCh() As Long)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVIH"
    
    For iCount = 0 To UBound(nCh)
      If nCh(iCount) > 31 Or nCh(iCount) < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVIH - Channel Pin " + nCh(iCount) + " argument out of range."
        Err.Raise -1000
        Exit Sub
      End If
    Next iCount
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set selected pins state = OFF (IH)
    For iCount = 0 To UBound(nCh)
      iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_OFF)
      If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVIH failed on terM9_setHighVoltagePinMode function."
        Err.Raise -1000
        Exit Sub
      End If
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Next iCount

End Sub

Public Sub HVIL(ByRef nCh() As Long)

    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    
    '**************************************************************************************
    ' HVIL (Pinstate = ON)                                                                                '
    '   (Use channel specific commands to avoid resetting all 32 pins)                    '
    '**************************************************************************************
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVIL"
    
    For iCount = 0 To UBound(nCh)
      If nCh(iCount) > 31 Or nCh(iCount) < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVIL - Channel Pin " + nCh(iCount) + " argument out of range."
        Err.Raise -1000
        Exit Sub
      End If
    Next iCount
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set selected pins state = ON (IL)
    For iCount = 0 To UBound(nCh)
      iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_ON)
      If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVIL failed on terM9_setHighVoltagePinMode function."
        Err.Raise -1000
        Exit Sub
      End If
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Next iCount

End Sub

Public Sub HVOL(ByRef nCh() As Long, Optional ByRef nThreshold As Double = 2.5, _
                 Optional ByRef nPinState As Long)
                 
    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim HVPinDetectState As Long
     
    '**************************************************************************************
    ' HVOL (PinMode = OFF, PinState = LO)                                                                                '
    '   (Use channel specific commands to avoid resetting all 32 pins)                    '
    '**************************************************************************************
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVOL"
    
    For iCount = 0 To UBound(nCh)
      If nCh(iCount) > 31 Or nCh(iCount) < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOL - Channel Pin " + nCh(iCount) + " argument out of range."
        Err.Raise -1000
        Exit Sub
      End If
    Next iCount
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set Threshold
    iStatus = terM9_setHighVoltagePinGroupThreshold(dtiHandle, nThreshold)
    If iStatus <> VI_SUCCESS Then
      Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOL failed on terM9_setHighVoltagePinGroupThreshold function."
      frmMain.txtMeasured.Text = -1
      Err.Raise -1000
      Exit Sub
    End If
    
    'Set selected pins Mode = OFF (Output pin)
    For iCount = 0 To UBound(nCh)
      iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_OFF)
      If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOL failed on terM9_setHighVoltagePinMode function."
        Err.Raise -1000
        Exit Sub
      End If
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Next iCount
    
    'Get selected pins state = LO/HI (0/1)
    nPinState = 0
    For iCount = 0 To UBound(nCh)
      iStatus = terM9_fetchHighVoltagePinState(dtiHandle, nCh(iCount), HVPinDetectState)
      If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOL failed on terM9_setHighVoltagePinMode function."
        Err.Raise -1000
        Exit Sub
      End If
      If HVPinDetectState = TERM9_STATE_HIGH Then
         If iCount < 31 Then
            nPinState = nPinState Or (2 ^ iCount) '1,2,4,8,16,32,,,,
         Else
            nPinState = nPinState Or &H80000000
         End If
         Failed = True
      End If
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Next iCount
    
End Sub

Public Sub HVOH(ByRef nCh() As Long, Optional ByRef nThreshold As Double = 2.5, _
                Optional ByRef nPinState As Long)
                
    Dim dtiHandle As Long
    Dim iCount As Integer
    Dim iStatus As Long
    Dim HVPinDetectState As Long
    Dim ExpPinState As Long
    
    '**************************************************************************************
    ' HVOH (Pinstate = HI)                                                                                '
    '   (Use channel specific commands to avoid resetting all 32 pins)                    '
    '**************************************************************************************
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVOH"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 31 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOH - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    If nThreshold > 10 Or nThreshold < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOH nLevelSet argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set Threshold
    iStatus = terM9_setHighVoltagePinGroupThreshold(dtiHandle, nThreshold)
    If iStatus <> VI_SUCCESS Then
      Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOH failed on terM9_setHighVoltagePinGroupThreshold function."
      frmMain.txtMeasured.Text = -1
      Err.Raise -1000
      Exit Sub
    End If
    
    'Set selected pins Mode = OFF (Output pin)
    For iCount = 0 To UBound(nCh)
      iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_OFF)
      If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOH failed on terM9_setHighVoltagePinMode function."
        Err.Raise -1000
        Exit Sub
      End If
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Next iCount
    
    'Get selected pins state = LO/HI (0/1)
    nPinState = 0
    Failed = False
    For iCount = 0 To UBound(nCh)
      iStatus = terM9_fetchHighVoltagePinState(dtiHandle, nCh(iCount), HVPinDetectState)
      If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVOH failed on terM9_setHighVoltagePinMode function."
        Err.Raise -1000
        Exit Sub
      End If
      If HVPinDetectState = TERM9_STATE_HIGH Then
        If iCount < 31 Then
          nPinState = nPinState Or (2 ^ iCount) '1,2,4,8,16,32,,,,
        Else
          nPinState = nPinState Or &H8000000
        End If
      Else
        Failed = True
      End If
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Next iCount
    
End Sub

Public Sub HVSetState(ByRef nCh() As Long, nPinState As Long)

    Dim dtiHandle As Long
    Dim iCount As Long
    Dim iStatus As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVSetState"
    
    For iCount = 0 To UBound(nCh)
        If nCh(iCount) > 31 Or nCh(iCount) < 0 Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVSetState - Channel Pin " + nCh(iCount) + " argument out of range."
            Err.Raise -1000
            Exit Sub
        End If
    Next iCount
    
    If bSimulation = True Then
        Exit Sub
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
    'Set selected pins state = OFF (IH)
    For iCount = 0 To UBound(nCh)
      If iCount < 31 Then
        If (nPinState And (2 ^ iCount)) = 0 Then '1,2,4,8,16,,,,
          iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_ON) ' IL
        Else
          iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_OFF) ' IH
        End If
      Else
        If (nPinState And &H8000000) = 0 Then
          iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_ON) ' IL
        Else
          iStatus = terM9_setHighVoltagePinMode(dtiHandle, nCh(iCount), TERM9_HVPINMODE_OFF) ' IH
        End If
      End If
      If iStatus <> VI_SUCCESS Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVSetState failed on terM9_setHighVoltagePinMode function."
        Err.Raise -1000
        Exit Sub
      End If
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Next iCount
    
End Sub

Public Function HVstate(ByRef nCh() As Long, Optional ByRef nThreshold As Double = 2.5, _
                        Optional APROBE As String = "OFF", _
                        Optional bMisProbeMsg As Boolean = True) As Long
    
    Dim dtiHandle As Long
    Dim iCount As Long
    Dim iStatus As Long
    Dim SimulationDefault As Long
    Dim HVPinDetectState As Long
    Dim ExpPinState As Long
    
    ExpPinState = CLng(frmMain.txtUpperLimit.Text)
    
    frmMain.txtMeasured.Text = Space(99)
    frmMain.txtMeasured.BackColor = vbWhite
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    'Set Display
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVstate"
    
    If UBound(nCh) > 31 Then
       Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVState - Too many pins in one group (max=32)."
       HVstate = -1: frmMain.txtMeasured.Text = -1
       Err.Raise -1000
       Exit Function
    End If
    
    For iCount = 0 To UBound(nCh)
      If nCh(iCount) > 31 Or nCh(iCount) < 0 Then
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVstate - Channel Pin " + nCh(iCount) + " argument out of range."
        HVstate = -1: frmMain.txtMeasured.Text = -1
        Err.Raise -1000
        Exit Function
      End If
    Next iCount
    
    If nThreshold > 10 Or nThreshold < 0 Then
      Echo "HV PROGRAMMING ERROR:  Command cmdDTS.HVstate nThreshold argument out of range."
        HVstate = -1: frmMain.txtMeasured.Text = -1
        Err.Raise -1000
      Exit Function
    End If
    
    APROBE = UCase(APROBE)
    If UBound(nCh) > 0 And Left(APROBE, 1) <> "O" Then ' default to "OFF"
      Echo "HVDTS PROGRAMMING ERROR:  Command cmdDTS.HVstate APROBE argument connot be used with an Array of Channel Pins."
        HVstate = -1: frmMain.txtMeasured.Text = -1
        Err.Raise -1000
      Exit Function
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case Left(APROBE, 1)
      Case "O", "S", "C" ' Off, Single, Continuous
      Case Else
        Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVstate APROBE argument out of range."
        HVstate = -1: frmMain.txtMeasured.Text = -1
        Err.Raise -1000
        Exit Function
    End Select

    If bSimulation = True Then
      SimulationDefault = CLng(frmMain.txtUpperLimit.Text)
      HVstate = CDbl(InputBox("Command cmdDTS.HVstate peformed." & vbCrLf & "Enter Word or Bit as an Integer value:", "SIMULATION MODE", SimulationDefault))
      frmMain.txtMeasured.Text = HVstate
      Exit Function
    End If
   
    dtiHandle = nInstrumentHandle(DIGITAL)
    
     'Set Threshold
    iStatus = terM9_setHighVoltagePinGroupThreshold(dtiHandle, nThreshold)
    If iStatus <> VI_SUCCESS Then
      Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVstate failed on terM9_setHighVoltagePinGroupThreshold function."
      HVstate = -1: frmMain.txtMeasured.Text = -1
      Err.Raise -1000
      Exit Function
    End If
    
    Select Case Left(APROBE, 1)
      Case "O" ' Off
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        HVstate = 0
        ReDim nBitState(0 To UBound(nCh)) As Long
        For iCount = 0 To UBound(nCh)
          iStatus = terM9_fetchHighVoltagePinState(dtiHandle, nCh(iCount), HVPinDetectState)
          If iStatus <> VI_SUCCESS Then
            Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVstate failed on terM9_fetchHighVoltagePinState function."
            HVstate = -1: frmMain.txtMeasured.Text = -1
            Err.Raise -1000
            Exit Function
          End If
          If HVPinDetectState = TERM9_STATE_HIGH Then
            If iCount < 31 Then
              HVstate = HVstate Or (2 ^ iCount)
            Else
              HVstate = HVstate Or &H80000000
            End If
          End If
        Next iCount
        frmMain.txtMeasured.Text = HVstate

      Case "S" ' Single
        MisProbe = vbYes
        Do While MisProbe = vbYes
          Failed = False
          UserEvent = 0
          bProbeClosed = False
          frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
          Do While (UserEvent = 0) And (Not bProbeClosed)
            DoEvents
          Loop
          frmMain.TimerProbe.Enabled = False
          bProbeClosed = False
          HVstate = 0
          For iCount = 0 To UBound(nCh)
            iStatus = terM9_fetchHighVoltagePinState(dtiHandle, nCh(iCount), HVPinDetectState)
            If iStatus <> VI_SUCCESS Then
              Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVstate failed on terM9_fetchHighVoltagePinState function."
              HVstate = -1: frmMain.txtMeasured.Text = -1
              Err.Raise -1000
              Exit Function
            End If
            If HVPinDetectState = TERM9_STATE_HIGH Then
              If iCount < 31 Then
                HVstate = HVstate Or (2 ^ iCount)
              Else
                HVstate = HVstate Or &H80000000
              End If
            End If
          Next iCount
          frmMain.txtMeasured.Text = HVstate
          If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
          If Failed = True And bMisProbeMsg = True Then
            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
              "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
          Else
            MisProbe = vbNo
          End If
        Loop
          
      Case "C"
        MisProbe = vbYes
        Do While MisProbe = vbYes
          UserEvent = 0
          Failed = False
          frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
          Do While (UserEvent = 0) And (Not bProbeClosed)
            Sleep 1   'Causes the application to be idle for 1 mS - the rest of the loop probably executes in a few micro seconds.
            DoEvents

            HVstate = 0
            ReDim nBitState(0 To UBound(nCh)) As Long
            For iCount = 0 To UBound(nCh)
              iStatus = terM9_fetchHighVoltagePinState(dtiHandle, nCh(iCount), HVPinDetectState)
              If iStatus <> VI_SUCCESS Then
                Echo "DTS PROGRAMMING ERROR:  Command cmdDTS.HVstate failed on terM9_fetchHighVoltagePinState function."
                HVstate = -1: frmMain.txtMeasured.Text = -1
                Err.Raise -1000
                Exit Function
              End If
              If HVPinDetectState = TERM9_STATE_HIGH Then
                If iCount < 31 Then
                  HVstate = HVstate Or (2 ^ iCount)
                Else
                  HVstate = HVstate Or &H80000000
                End If
              End If
            Next iCount
            frmMain.txtMeasured.Text = HVstate
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
          Loop
          frmMain.TimerProbe.Enabled = False
          bProbeClosed = False

          If Failed = True And bMisProbeMsg = True Then
            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
              "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
          Else
            MisProbe = vbNo
          End If
        Loop
    End Select

End Function

Public Sub HVreset()

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DTS"
    frmMain.txtCommand.Text = "HVreset"
    
    If bSimulation = True Then
        Exit Sub
    End If

    terM9_resetHighVoltagePins nInstrumentHandle(DIGITAL)
    
End Sub


