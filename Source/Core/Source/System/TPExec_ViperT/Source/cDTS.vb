Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

<System.Runtime.InteropServices.ProgId("cDTS_NET.cDTS")> Public Class cDTS
    Public Sub AbortTest()
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "DTS"
        gFrmMain.txtCommand.Text = "AbortTest"

        If bSimulation = True Then
            Exit Sub
        End If
        terM9bas.terM9_reset(nInstrumentHandle(DIGITAL))
        terM9bas.terM9_close(nInstrumentHandle(DIGITAL))
    End Sub
	
    Public Sub SetLevelSet(ByRef nSet As Integer, ByRef VIH As Double, ByRef VIL As Double,
                           ByRef VOH As Double, ByRef VOL As Double, Optional ByRef ChannelCard As Short = 0,
                           Optional ByRef IOH As Double = -0.01, Optional ByRef IOL As Double = 0.01,
                           Optional ByRef VCOM As Double = 3.5, Optional ByRef slewRate As String = "MEDIUM")
        Dim iStatus As Integer
        Dim dtiHandle As Integer
        Dim nSlewRate As Integer
        Dim nChannelCard As Integer
        Dim bSetLevelSet As Boolean ' bbbb

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        bSetLevelSet = True
        gFrmMain.txtInstrument.Text = "DTS"
        gFrmMain.txtCommand.Text = "bSetLevelSet"

        'Error Check
        If nSet > 1 Or nSet < 0 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet nSet argument out of range.")
            bSetLevelSet = False
            Err.Raise(-1000)
            Exit Sub
        End If

        If VIH > 5 Or VIH < -2 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VIH argument out of range.")
            bSetLevelSet = False
            Err.Raise(-1000)
            Exit Sub
        End If

        If VIL > 5 Or VIL < -2 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VIL argument out of range.")
            bSetLevelSet = False
            Err.Raise(-1000)
            Exit Sub
        End If

        If VOH > 5 Or VOH < -2 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VOH argument out of range.")
            bSetLevelSet = False
            Err.Raise(-1000)
            Exit Sub
        End If

        If VOL > 5 Or VOL < -2 Then 'bbbb was V zero L
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VOL argument out of range.")
            bSetLevelSet = False
            Err.Raise(-1000)
            Exit Sub
        End If

        If IOH > 0.0 Or IOH < -0.01 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet IOH argument out of range.")
            bSetLevelSet = False
            Err.Raise(-1000)
            Exit Sub
        End If

        If IOL > 0.01 Or IOL < 0.0 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet IOL argument out of range.")
            bSetLevelSet = False
            Err.Raise(-1000)
            Exit Sub
        End If

        If VCOM > 5 Or VCOM < -2 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet VCOM argument out of range.")
            bSetLevelSet = False
            Err.Raise(-1000)
            Exit Sub
        End If

        'SlewRate Check
        slewRate = UCase(Left(slewRate, 1))
        Select Case UCase(slewRate)
            Case "L"
                nSlewRate = TERM9_SLEWRATE_LOW
            Case "M"
                nSlewRate = TERM9_SLEWRATE_MEDIUM
            Case "H"
                nSlewRate = TERM9_SLEWRATE_HIGH
            Case Else
                Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet SLEWRATE argument incorrect.")
                bSetLevelSet = False
                Err.Raise(-1000)
                Exit Sub
        End Select

        'SlewRate Check
        Select Case ChannelCard
            Case 0
                nChannelCard = TERM9_SCOPE_SYSTEM
            Case 1, 2, 3
                nChannelCard = TERM9_SCOPE_CARD(ChannelCard)
            Case Else
                Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet ChannelCard argument incorrect.")
                bSetLevelSet = False
                Err.Raise(-1000)
                Exit Sub
        End Select

        If bSimulation = True Then
            Exit Sub
        End If

        'Set DTI Handle for function use
        dtiHandle = nInstrumentHandle(DIGITAL)
        ' Set Level Set                                              VIH VIL VOH  VOL     IOH    IOL  VCOM
        iStatus = terM9_setLevelSet(dtiHandle, nChannelCard, nSet, VIH, VIL, VOH, VOL, IOH, IOL, VCOM, TERM9_SLEWRATE_MEDIUM)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSetLevelSet failed.")
            Err.Raise(-1000)
            Exit Sub
        End If
    End Sub

	Public Sub resetInstr()
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "ResetInstr"
		
		If bSimulation = True Then
			Exit Sub
		End If
		
		terM9bas.terM9_reset(nInstrumentHandle(DIGITAL))
    End Sub

	Public Sub CloseInstr()
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "CloseInstr"
		
		If bSimulation = True Then
			Exit Sub
		End If
		terM9bas.terM9_close(nInstrumentHandle(DIGITAL))
    End Sub

    Public Function bInit(ByRef bDTSReset As Boolean) As Boolean
        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            bInit = False
            Exit Function
        End If
        gFrmMain.txtInstrument.Text = "DTS"
        gFrmMain.txtCommand.Text = "bInit"

        If bSimulation = True Then
            nInstrumentHandle(DIGITAL) = 0
            bInit = True
            Exit Function
        End If
        bInit = INITInst.iInitDigital(bDTSReset)
    End Function
	
    Public Function bRunDTB(ByRef sDTBFile As String,
                            Optional ByRef bReset As Boolean = True,
                            Optional ByRef bPinState As Boolean = False,
                            Optional ByRef bConcurrent As Boolean = False) As Boolean
        Dim dtiHandle As Integer
        Dim nTestResults As Integer
        Dim nStatus As Integer

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            bRunDTB = False
            Exit Function
        End If
        gFrmMain.txtInstrument.Text = "DTS"
        gFrmMain.txtCommand.Text = "bRunDTB"
        bRunDTB = True

        'Set DTI Handle for function use
        If nInstrumentHandle(DIGITAL) = 0 And Not bSimulation Then
            'Initialize Instrument
            If Not INITInst.iInitDigital(bReset) Then
                Echo("INSTRUMENT ERROR - DTS failed to initialize.")
                gFrmMain.txtMeasured.Text = "FAILED"
                bRunDTB = False
                Exit Function
            End If
        End If
        dtiHandle = nInstrumentHandle(DIGITAL)
        'Reset Instrument if required
        If bReset Then
            If Not bSimulation Then nStatus = terM9bas.terM9_reset(nInstrumentHandle(DIGITAL)) Else nStatus = 0
            If Not bChkStatTerM9(nStatus) Then
                Echo("INSTRUMENT ERROR - DTS failed _setSystemEnable.")
                gFrmMain.txtMeasured.Text = "FAILED"
                bRunDTB = False
                Exit Function
            End If
        End If
        'Enable Instrument
        If Not bSimulation Then nStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE) Else nStatus = 0
        If Not bChkStatTerM9(nStatus) Then
            Echo("INSTRUMENT ERROR - DTS failed _setSystemEnable.")
            gFrmMain.txtMeasured.Text = "FAILED"
            bRunDTB = False
            Exit Function
        End If

        'Run DTB File
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            nStatus = terM9_executeDigitalTest(dtiHandle, ProgramPath & sDTBFile, VI_FALSE, nTestResults)
        Else
            nStatus = 0
            nTestResults = 0
            Do
                nTestResults = CInt(InputBox("Command cmdDTS.bRunDTB peformed." & vbCrLf & "Enter 166 for Pass or 162 for FAIL Value:", "SIMULATION MODE"))
            Loop Until nTestResults = 166 Or nTestResults = 162
        End If
        If Not bChkStatTerM9(nStatus) Then
            Echo("INSTRUMENT ERROR - DTS failed _setSystemEnable.")
            gFrmMain.txtMeasured.Text = "FAILED"
            bRunDTB = False
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        Select Case nTestResults
            Case TERM9_RESULT_PASS
                gFrmMain.txtMeasured.Text = "PASSED"
                bRunDTB = True

            Case TERM9_RESULT_FAIL
                gFrmMain.txtMeasured.Text = "FAILED"
                bRunDTB = False

            Case TERM9_RESULT_NOT_RUN
                gFrmMain.txtMeasured.Text = "FAILED"
                bRunDTB = False ' was RRUNDTB bbbb
        End Select
    End Function
	
	Public Function sDiagnostic(ByRef sDTBFile As String, ByRef sCircuitFile As String, ByRef nType As Integer) As String
		Dim dtiHandle As Integer
        Dim bCurrentQuit As Boolean
		Dim bCurrentMainMenu As Boolean
		
        bCurrentQuit = gFrmMain.Quit.Enabled
		bCurrentMainMenu = gFrmMain.MainMenu.Enabled
        gFrmMain.Quit.Enabled = False
		gFrmMain.MainMenu.Enabled = False

        sDiagnostic = ""

        'Set DTI Handle for function use
		dtiHandle = nInstrumentHandle(DIGITAL)
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "sDiagnostic"
		'Error Check
		If nType > 3 Or nType < 1 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.sDiagnostics nType argument out of range.")
			Err.Raise(-1000)
			Exit Function
		End If
		'Turn off Boundary Scan Diagnostics
		bChkStatDM(DM_setDiagnosticEnable(DIAG_TYPE_BOUNDARY_SCAN, DIAG_DISABLE))
		'bChkStatDM DM_setMaxSeedingFaultSetsValue(5)
		'Set the Type of Diagnostics
		Select Case nType
			Case SEEDED_GUIDED_PROBE
				bChkStatDM(DM_setDiagnosticEnable(DIAG_TYPE_FAULT_DICTIONARY, DIAG_ENABLE))
				bChkStatDM(DM_setDiagnosticEnable(DIAG_TYPE_GUIDED_PROBE, DIAG_ENABLE))
				
			Case FAULT_DICTIONARY_ONLY
				bChkStatDM(DM_setDiagnosticEnable(DIAG_TYPE_FAULT_DICTIONARY, DIAG_ENABLE))
				bChkStatDM(DM_setDiagnosticEnable(DIAG_TYPE_GUIDED_PROBE, DIAG_DISABLE))
				
			Case GUIDED_PROBE_ONLY
				bChkStatDM(DM_setDiagnosticEnable(DIAG_TYPE_FAULT_DICTIONARY, DIAG_DISABLE))
				bChkStatDM(DM_setDiagnosticEnable(DIAG_TYPE_GUIDED_PROBE, DIAG_ENABLE))
		End Select
		
		bChkStatDM(DM_setDiagnosticOutputDir(DIAGNOSTICS_DIRECTORY))
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
		'Run diagnostics
		
		bChkStatDM(DM_setMaxSeedingFaultSetsValue(5))
		bChkStatDM(DM_diagnoseTest(ProgramPath & sDTBFile, ProgramPath & sCircuitFile, "", dtiHandle))
		
        gFrmMain.Quit.Enabled = bCurrentQuit
		gFrmMain.MainMenu.Enabled = bCurrentMainMenu
    End Function

	Public Function bSystemAlignment() As Boolean
		Dim alignmentResult As Integer
		Dim nStatus As Integer
		
        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            bSystemAlignment = False
            Exit Function
        End If
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "bRunDTB"
		bSystemAlignment = True
		
		'bbbb was status =
		nStatus = terM9_executeSystemAlignment(nInstrumentHandle(DIGITAL), alignmentResult)
		If nStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.bSystemAligment failed.  Invalid Session Handle.")
			Err.Raise(-1000)
			bSystemAlignment = False
			Exit Function
		End If
		Select Case alignmentResult
			Case TERM9_RESULT_PASS
				gFrmMain.txtMeasured.Text = "PASSED"
				bSystemAlignment = True
			Case TERM9_RESULT_FAIL
				gFrmMain.txtMeasured.Text = "FAILED"
				bSystemAlignment = False
			Case TERM9_RESULT_NOT_RUN
				gFrmMain.txtMeasured.Text = "FAILED"
				bSystemAlignment = False
		End Select
    End Function
	
	Private Function bChkStatTerM9(ByRef nFunctionStatus As Integer) As Boolean
        Dim sStatusString As String = Space(256)
		Dim nStatus As Integer
		
		bChkStatTerM9 = True
		If nFunctionStatus <> VI_SUCCESS Then
            nStatus = terM9_errorMessage(nInstrumentHandle(DIGITAL), nFunctionStatus, sStatusString)
			If nStatus <> VI_SUCCESS Then 'bbbb was visuccess
				Echo("DTS ERROR - Failed with unknown error.")
			Else
                Echo("DTS ERROR - " & StripNullCharacters(sStatusString))
			End If
			AbortTest()
			bChkStatTerM9 = False
		End If
	End Function
	Private Function bChkStatDM(ByRef nFunctionStatus As Integer) As Boolean
        Dim sStatusString As String = Space(256)
        Dim sSeverity As String = Space(1)
		Dim nStatus As Integer
		
		bChkStatDM = True
		If nFunctionStatus <> DIAG_SUCCESS Then
            nStatus = DM_errorMessage(nFunctionStatus, MAX_DIAG_TEXT_SIZE, sSeverity, sStatusString)
			Select Case nStatus
				Case DIAG_SUCCESS
					'Echo "DTS DIAGNOSTIC ERROR - " & StripNullCharacters(sStatusString)
				Case DIAG_WARN_BUFFER_TOO_SMALL
					Echo("DTS DIAGNOSTIC ERROR - Buffer overrun.")
				Case Else
					Echo("DTS DIAGNOSTIC ERROR -Failed with unknown error.")
			End Select
			bChkStatDM = False
		End If
    End Function

    ''' <summary>
    ''' This Routine measures the digital signature of IDpins.  This is accomplished by setting 
    ''' the active load voltage (VCOM) level to 3.5 VDC and grounding the pins to provide the
    ''' digital signature
    ''' </summary>
    ''' <param name="IDpins">Pins to be measured</param>
    ''' <returns>Integer value of measurement for the 8 digital pins</returns>
    ''' <remarks></remarks>
    Public Function nIDIdentification(ByRef IDpins() As Integer) As Integer
        Dim iCount As Short
        Dim iStatus As Integer
        Dim dtiHandle As Integer
        Dim PinMapContents(9) As Integer
        Dim ResultsList(9) As Integer
        Dim nResultsCount As Integer

        'Added 5/13/03 To allow passing value to be entered in box automatically
        Dim SimulationDefault As Integer

        nIDIdentification = 0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "DTS"
        gFrmMain.txtCommand.Text = "nIDIdentification"
        If bSimulation = True Then
            'Added 5/13/03 To allow passing value to be entered in box automatically in simulation mode
            SimulationDefault = CInt(gFrmMain.txtUpperLimit.Text)

            nIDIdentification = CInt(InputBox("Enter ID Value:", "SIMULATION MODE", CStr(SimulationDefault)))
            gFrmMain.txtMeasured.Text = CStr(nIDIdentification)
            Exit Function
        End If

        'Set DTI Handle for function use
        dtiHandle = nInstrumentHandle(DIGITAL)

        'Reset the M910 DTI. */
        iStatus = terM9bas.terM9_reset(dtiHandle)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        'Setup PinMap Values using IDpins
        For iCount = 1 To 8
            PinMapContents(iCount) = TERM9_SCOPE_CHAN(IDpins(iCount))
        Next iCount
        iStatus = terM9_setPinmapValues(dtiHandle, 1, nSizeOf(PinMapContents), PinMapContents(1))

        'Close output relays
        iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(1), TERM9_RELAY_CLOSED)

        ' Set the programmable attributes of level set 1 systemwide.
        ' Set level set for TTL levels:
        ' Set VIH to 5.0 V.
        ' Set VIL to 0.0 V.
        ' Set VOH to 2.0 V.
        ' Set VOL to 0.8 V.
        ' Set IOH to -400 uA.
        ' Set IOL to 8 mA.
        ' Set VCOM to 3.5 V.
        ' Set slew rate to MEDIUM.
        ' Level set zero                                              VIH VIL VOH  VOL     IOH    IOL  VCOM
        iStatus = terM9_setLevelSet(dtiHandle, TERM9_SCOPE_SYSTEM, 0, 5.0, 0.0, 2.0, 0.8, -0.0004, 0.008, 3.5, TERM9_SLEWRATE_MEDIUM)
        ' Level set one
        iStatus = terM9_setLevelSet(dtiHandle, TERM9_SCOPE_SYSTEM, 1, 5.0, 0.0, 2.0, 0.8, -0.0004, 0.008, 3.5, TERM9_SLEWRATE_MEDIUM)
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

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        'Calculate and set Return Value
        nIDIdentification = 0
        For iCount = 1 To 8
            If ResultsList(iCount) = TERM9_RESULT_FAIL Then
                nIDIdentification = nIDIdentification Or (2 ^ (iCount - 1))
            End If
        Next iCount
        gFrmMain.txtMeasured.Text = CStr(nIDIdentification)
    End Function

	'*******************************************Pin Opcodes**********************************************
	Public Sub IOX(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "IOX"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
        End If

		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_IOX)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
        End If

		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub
	
	Public Sub DISCONNECT(ByRef nCh() As Integer, Optional ByRef PerformVerify As Boolean = True)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim pinList() As Integer
		Dim MinIndex As Integer
		Dim MaxIndex As Integer
		Dim PinCount As Integer
		Dim VerifyStatus As Integer
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
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "DISCONNECT"
		
		'Set PinMap Values using nCh
		
		MinIndex = LBound(nCh, 1)
		MaxIndex = UBound(nCh, 1)
		PinCount = MaxIndex - MinIndex + 1
		
		'Error Check
		For iCount = MinIndex To MaxIndex
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		
		ReDim pinList((PinCount - 1))
		
		'Convert to zero based array (if not all ready zero based) and to system relative channel numbers
		For iCount = MinIndex To MaxIndex
			pinList(iCount - MinIndex) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		
		'Open All relays of channels in nCh()
		iStatus = terM9_setChannelConnect(dtiHandle, PinCount, pinList(0), TERM9_RELAY_OPEN)
		If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_setChannelConnect function." & vbCrLf & _
                 "The digital relays of the selected channels have not opened successfully!")
            Err.Raise(-1000, ,
                      "DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_setChannelConnect function." & vbCrLf & _
                      "The digital relays of the selected channels have not opened successfully!")
			Exit Sub
		End If
		
		'Perform Verify Unless Opted Out
		If PerformVerify Then
			For iCount = MinIndex To MaxIndex
                If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
				
				'Check state of each channel to make sure it's open otherwise raise error
				iStatus = terM9_getChannelConnect(dtiHandle, TERM9_SCOPE_CHAN(nCh(iCount)), VerifyStatus)
				
				If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
				
				'Error if verify function fails
				If iStatus <> VI_SUCCESS Then
                    Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_getChannelConnect function." & vbCrLf & _
                         "Unable to verify if digital relay for channel ") + nCh(iCount) + CDbl(" has opened successfully!")))
                    Err.Raise(-1000, ,
                              CDbl("DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_getChannelConnect function." & vbCrLf & _
                              "Unable to verify if digital relay for channel ") + nCh(iCount) + CDbl(" has opened successfully!"))
					Exit Sub
				End If
				
				If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
				
				'Error if verify function finds relay has not opened
				If VerifyStatus <> TERM9_RELAY_OPEN Then
                    Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_getChannelConnect function." & vbCrLf & _
                         "The digital relay for channel ") + nCh(iCount) + CDbl("could not be opened successfully!")))
                    Err.Raise(-1000, ,
                              CDbl("DTS PROGRAMMING ERROR:  Attention! Command cmdDTS.DISCONNECT failed on terM9_getChannelConnect function." & vbCrLf & _
                              "The digital relay for channel ") + nCh(iCount) + CDbl("could not be opened successfully!"))
					Exit Sub
				End If
            Next iCount
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

	Public Sub IL(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "IL"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.IL - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_IL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IL failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub IH(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "IH"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.IH - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IOX failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_IH)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IH failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub ML(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "ML"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.ML - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
        End If

		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_ML)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub MH(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "MH"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.MH - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_MH)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.MH failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub OL(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "OL"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.OL - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_OL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OL failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub OH(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "OH"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.OH - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_OH)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OH failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub OB(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "OB"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.OB - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_OB)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OB failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub OK(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "OK"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.OK - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_OK)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.OK failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub ILOH(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "ILOH"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_ILOH)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ILOH failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub IHOL(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "IHOL"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_IHOL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.IHOL failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub TOG(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "TOG"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_TOG)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.TOG failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

	Public Sub KEEP(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "KEEP"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_KEEP)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.KEEP failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub
	
	Public Sub SIG(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "SIG"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_setChannelPinOpcode(dtiHandle, 300, TERM9_OP_SIG)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SIG failed on terM9_setChannelPinOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub
	
	Public Sub SetState(ByRef nCh() As Integer, ByRef nState As Integer, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim PinMapContents() As Integer

		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "SetState"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setChannelChanMode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		' Set the systemwide ground reference source to INTERNAL.
		iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setGroundReference function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set the pin-state-data mode
		iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setPinmapGroup function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		
		iStatus = terM9_setGroupGroupOpcode(dtiHandle, 300, TERM9_OP_IX, nState)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetState failed on terM9_setGroupGroupOpcode function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		iStatus = terM9_runStaticPattern(dtiHandle, VI_FALSE, VI_FALSE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.ML failed on terM9_runStaticPattern function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub

    Public Function state(ByRef nCh() As Integer, Optional ByRef nLevelSet As Integer = 0,
                          Optional ByRef APROBE As String = "OFF",
                          Optional ByRef bMisProbeMsg As Boolean = True) As Integer
        Dim dtiHandle As Integer
        Dim iCount As Short
        Dim iStatus As Integer
        Dim PinMapContents() As Integer
        Dim nBitState() As Integer
        Dim nDeskewRelay As Integer
        Dim iOverTemperature As Short
        Dim iReferenceOverload As Short

        state = 0

        'Added 5/13/03 To allow passing value to be entered in box automatically in simulation
        Dim SimulationDefault As Integer

        'Added 5/13/03 to make sure state allways intitiates the gFrmMain.txtMeasured.change
        'Method - Craig W. - Used invisible characters to prevent garbage from being displayed
        gFrmMain.txtMeasured.Text = Space(99)
        gFrmMain.txtMeasured.BackColor = System.Drawing.Color.White

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            state = -1
            Exit Function
        End If
        'Set Display
        gFrmMain.txtInstrument.Text = "DTS"
        gFrmMain.txtCommand.Text = "State"

        'Error Check
        For iCount = 0 To UBound(nCh)
            If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
                Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.State - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
                state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
                Err.Raise(-1000)
                Exit Function
            End If
        Next iCount
        If nLevelSet > 1 Or nLevelSet < 0 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State nLevelSet argument out of range.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If
        'Set Case Aprobe
        APROBE = UCase(APROBE)
        If UBound(nCh) > 0 And Left(APROBE, 1) <> "O" Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State APROBE argument connot be used with an Array of Channel Pins.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If
        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            state = -1
            Exit Function
        End If
        Select Case Left(APROBE, 1)
            Case "O", "S", "C" ' Off, Single, Continuous
            Case Else
                Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State APROBE argument out of range.")
                state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Check For simulation
        If bSimulation = True Then

            'Changed 5/13/03 To allow passing value to be entered in box automatically simulation
            SimulationDefault = CInt(gFrmMain.txtUpperLimit.Text)

            'Added 5/07/03 to allow simulation mode support for the state() command
            'Craig W.
            'state = CDbl(InputBox("Command cmdDTS.state peformed." & vbCrLf & "Enter Word or Bit as an Integer value:", "SIMULATION MODE"))
            state = CDbl(InputBox("Command cmdDTS.state peformed." & vbCrLf & "Enter Word or Bit as an Integer value:", "SIMULATION MODE", CStr(SimulationDefault)))
            gFrmMain.txtMeasured.Text = CStr(state)
            Exit Function
        End If

        'Set DTI Handle
        dtiHandle = nInstrumentHandle(DIGITAL)
        'Set PinMap Values using nCh
        nNumberOfChannelPins = UBound(nCh)
        ReDim PinMapContents(UBound(nCh))
        For iCount = 0 To UBound(nCh)
            PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
        Next iCount
        iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setPinmapValues function.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            state = -1
            Exit Function
        End If

        'Set System Enabled
        iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setSystemEnable function.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            state = -1
            Exit Function
        End If

        'Close output Relay
        iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelConnect function.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            state = -1
            Exit Function
        End If

        'Close output Relay
        iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelLevel function.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            state = -1
            Exit Function
        End If

        'Set the pin-state-data mode
        iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelChanMode function.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            state = -1
            Exit Function
        End If

        ' Set the systemwide ground reference source to INTERNAL.
        iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setGroundReference function.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            state = -1
            Exit Function
        End If

        'Set the pin-state-data mode
        iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setPinmapGroup function.")
            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
            Err.Raise(-1000)
            Exit Function
        End If

        Select Case Left(APROBE, 1)
            Case "O" ' Off
                If UserEvent = ABORT_BUTTON Then
                    Err.Raise(USER_EVENT + ABORT_BUTTON)
                    state = -1
                    Exit Function
                End If
                state = 0
                ReDim nBitState(UBound(nCh))
                For iCount = 0 To UBound(nCh)
                    iStatus = terM9_fetchChannelState(dtiHandle, TERM9_SCOPE_CHAN(nCh(iCount)), nBitState(iCount), nDeskewRelay, iOverTemperature, iReferenceOverload)
                    If iStatus <> VI_SUCCESS Then
                        Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_fetchChannelState function.")
                        state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
                        Err.Raise(-1000)
                        Exit Function
                    End If
                    If nBitState(iCount) = TERM9_DETECTOR_HIGH Then
                        state = state Or (2 ^ iCount)
                    End If
                Next iCount
                gFrmMain.txtMeasured.Text = CStr(state)

            Case "S" ' Single
                MisProbe = MsgBoxResult.Yes
                Do While MisProbe = MsgBoxResult.Yes
                    Failed = False
                    UserEvent = 0
                    bProbeClosed = False
                    gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        System.Windows.Forms.Application.DoEvents()
                    Loop
                    gFrmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    state = 0
                    ReDim nBitState(UBound(nCh))
                    For iCount = 0 To UBound(nCh)
                        iStatus = terM9_fetchChannelState(dtiHandle, TERM9_SCOPE_CHAN(nCh(iCount)), nBitState(iCount), nDeskewRelay, iOverTemperature, iReferenceOverload)
                        If iStatus <> VI_SUCCESS Then
                            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_fetchChannelState function.")
                            state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
                            Err.Raise(-1000)
                            Exit Function
                        End If
                        If nBitState(iCount) = TERM9_DETECTOR_HIGH Then
                            state = state Or (2 ^ iCount)
                        End If
                    Next iCount
                    gFrmMain.txtMeasured.Text = CStr(state)
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                    If Failed = True And bMisProbeMsg = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                          MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                          "Analog Probe Failure")
                    Else
                        MisProbe = MsgBoxResult.No
                    End If
                Loop

            Case "C"
                MisProbe = MsgBoxResult.Yes
                Do While MisProbe = MsgBoxResult.Yes
                    UserEvent = 0
                    Failed = False
                    gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        System.Threading.Thread.Sleep(1) 'Causes the application to be idle for 1 mS - the rest of the loop probably executes in a few micro seconds.
                        System.Windows.Forms.Application.DoEvents()

                        state = 0
                        ReDim nBitState(UBound(nCh))
                        For iCount = 0 To UBound(nCh)
                            iStatus = terM9_fetchChannelState(dtiHandle, TERM9_SCOPE_CHAN(nCh(iCount)), nBitState(iCount), nDeskewRelay, iOverTemperature, iReferenceOverload)
                            If iStatus <> VI_SUCCESS Then
                                Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_fetchChannelState function.")
                                state = -1 : gFrmMain.txtMeasured.Text = CStr(-1)
                                Err.Raise(-1000)
                                Exit Function
                            End If
                            If nBitState(iCount) = TERM9_DETECTOR_HIGH Then
                                state = state Or (2 ^ iCount)
                            End If
                        Next iCount
                        gFrmMain.txtMeasured.Text = CStr(state)
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                    Loop
                    gFrmMain.TimerProbe.Enabled = False
                    bProbeClosed = False

                    If Failed = True And bMisProbeMsg = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = MsgBoxResult.No
                    End If
                Loop
		End Select
    End Function
	
	Private Function Toggle(ByRef T As Boolean) As Short
        Toggle = Not T
    End Function
	
    Private Function ConvertTeradyneState(ByRef TeradyneState As Integer) As Boolean
        If TeradyneState = TERM9_DETECTOR_HIGH Then
            ConvertTeradyneState = True
        Else
            ConvertTeradyneState = False
        End If
    End Function
	
	Public Sub SetActiveLoad(ByRef nCh() As Integer, Optional ByRef bState As Boolean = True, Optional ByRef nLevelSet As Integer = 0)
		Dim dtiHandle As Integer
		Dim iCount As Short
		Dim iStatus As Integer
		Dim nState As Integer
		Dim PinMapContents() As Integer
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "DTS"
		gFrmMain.txtCommand.Text = "SetActiveLoad"
		
		'Error Check
		For iCount = 0 To UBound(nCh)
			If nCh(iCount) > 191 Or nCh(iCount) < 0 Then
				Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad - Channel Pin ") + nCh(iCount) + CDbl(" argument out of range.")))
				Err.Raise(-1000)
				Exit Sub
			End If
		Next iCount
		If nLevelSet > 1 Or nLevelSet < 0 Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad nLevelSet argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		'Check For simulation
		If bSimulation = True Then
			Exit Sub
		End If
		
		'Set DTI Handle
		dtiHandle = nInstrumentHandle(DIGITAL)
		'Set PinMap Values using nCh
		ReDim PinMapContents(UBound(nCh))
		For iCount = 0 To UBound(nCh)
			PinMapContents(iCount) = TERM9_SCOPE_CHAN(nCh(iCount))
		Next iCount
		iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setPinmapValues function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Set System Enabled
		iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setSystemEnable function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setChannelConnect function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		
		'Close output Relay
		iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setChannelLevel function.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Active Load State
		If bState = True Then nState = TERM9_LOAD_ON Else nState = TERM9_LOAD_OFF
		iStatus = terM9_setChannelLoad(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nState)
		If iStatus <> VI_SUCCESS Then
			Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.SetActiveLoad failed on terM9_setChannelLoad function.")
			Err.Raise(-1000)
			Exit Sub
		End If
    End Sub
	
    Public Function frequency(ByRef nCh As Integer, Optional ByRef nLevelSet As Integer = 0,
                              Optional ByRef MinimumAperture As Double = 0, Optional ByRef HysterisisSeconds As Double = 0,
                              Optional ByRef ActualAperture As Double = 0, Optional ByRef ActualSampleRate As Double = 0,
                              Optional ByRef APROBE As String = "OFF", Optional ByRef bMisProbeMsg As Boolean = True) As Double
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
        Dim dtiHandle As Integer
        Dim iStatus As Integer
        Dim nDeskewRelay As Integer
        Dim iOverTemperature As Short
        Dim iReferenceOverload As Short

        'FrequencyCalculation Code
        '*********************************
        Dim PinMapContents(0) As Integer
        Dim Pointer As Integer
        Dim SampleCount As Integer
        Dim Aperture As Double
        Dim TimeStamp As Double
        Dim SampleRate As Integer
        Dim CurrentState As Boolean
        Dim CurrentSample As Integer
        Dim HysterisisSamples As Integer
        Dim HysterisisCounter As Integer
        Dim TransitionCounter As Integer
        Dim DigitizedData(100001) As Integer
        Dim ScopeIndex As Integer
        '*********************************

        'Added 5/13/03 To allow passing value to be entered in box automatically in simulation
        Dim SimulationDefault As Integer

        Frequency = 0

        'Added 5/13/03 to make sure state allways intitiates the gFrmMain.txtMeasured.change
        'Method - Craig W. - Used invisible characters to prevent garbage from being displayed
        gFrmMain.txtMeasured.Text = Space(99)
        gFrmMain.txtMeasured.BackColor = System.Drawing.Color.White

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        'Set Display
        gFrmMain.txtInstrument.Text = "DTS"
        gFrmMain.txtCommand.Text = "State"

        'Error Check
        If nCh > 191 Or nCh < 0 Then
            Echo(CStr(CDbl("DTS PROGRAMMING ERROR:  Command cmdDTS.State - Channel Pin ") + nCh + CDbl(" argument out of range.")))
            Err.Raise(-1000)
            Exit Function
        End If

        If nLevelSet > 1 Or nLevelSet < 0 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State nLevelSet argument out of range.")
            Err.Raise(-1000)
            Exit Function
        End If

        'Check Aperture
        If MinimumAperture >= 0.15 Or MinimumAperture <= 0 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State MinimumAperture argument out of range.")
            Echo("                        Allowable Range:  0 < MinimumAperture < .150")
            'Err.Raise -1000
            Exit Function
        End If

        'Check Hysterisis
        If HysterisisSeconds > 0.02 Or HysterisisSeconds < 0 Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State HysterisisSeconds argument out of range.")
            Echo("                        Allowable Range:  0 <= HysterisisSeconds <= .02")
            Err.Raise(-1000)
            Exit Function
        End If

        'Set Case Aprobe
        APROBE = UCase(APROBE)
        If nCh > 0 And Left(APROBE, 1) <> "O" Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State APROBE argument cannot be used with an Array of Channel Pins.")
            Err.Raise(-1001)
            Frequency = 0 'bbbb was dMeasFreq = 0
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case Left(APROBE, 1)
            Case "O", "S", "C" ' Off, Single, Continuous
            Case Else
                Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State APROBE argument out of range.")
                Err.Raise(-1001)
                Frequency = 0 'bbbb was dMeasFreq = 0
                Exit Function
        End Select

        'Check For simulation
        If bSimulation = True Then

            'Changed 5/13/03 To allow passing value to be entered in box automatically simulation
            SimulationDefault = CInt(gFrmMain.txtUpperLimit.Text)

            'Added 5/07/03 to allow simulation mode support for the state() command
            'Craig W.
            'state = CDbl(InputBox("Command cmdDTS.state peformed." & vbCrLf & "Enter Word or Bit as an Integer value:", "SIMULATION MODE"))
            Frequency = CDbl(InputBox("Command cmdDTS.state peformed." & vbCrLf & "Enter Word or Bit as an Integer value:", "SIMULATION MODE", CStr(SimulationDefault)))
            gFrmMain.txtMeasured.Text = CStr(Frequency)
            Exit Function
        End If

        'Set DTI Handle
        dtiHandle = nInstrumentHandle(DIGITAL)

        'Set PinMap Values using nCh
        PinMapContents(0) = TERM9_SCOPE_CHAN(nCh)
        iStatus = terM9_setPinmapValues(dtiHandle, 0, nSizeOf(PinMapContents), PinMapContents(0))
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setPinmapValues function.")
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        'Set System Enabled
        iStatus = terM9_setSystemEnable(dtiHandle, VI_TRUE)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setSystemEnable function.")
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        'Close output Relay
        iStatus = terM9_setChannelConnect(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_RELAY_CLOSED)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelConnect function.")
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        'Close output Relay
        iStatus = terM9_setChannelLevel(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), nLevelSet)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelLevel function.")
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        'Set the pin-state-data mode
        iStatus = terM9_setChannelChanMode(dtiHandle, nSizeOf(PinMapContents), PinMapContents(0), TERM9_CHANMODE_STATIC)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setChannelChanMode function.")
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        ' Set the systemwide ground reference source to INTERNAL.
        iStatus = terM9_setGroundReference(dtiHandle, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setGroundReference function.")
            Err.Raise(-1000)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        'Set the pin-state-data mode
        iStatus = terM9_setPinmapGroup(dtiHandle, 300, nSizeOf(PinMapContents), PinMapContents(0))
        If iStatus <> VI_SUCCESS Then
            Echo("DTS PROGRAMMING ERROR:  Command cmdDTS.State failed on terM9_setPinmapGroup function.")
            Err.Raise(-1000)
            Exit Function
        End If

        Select Case Left(APROBE, 1)
            Case "O" ' Off

                If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Frequency = 0

                'High = 52 'TERM9_DETECTOR_HIGH
                'Low = 53 'TERM9_DETECTOR_LOW

                'Frequency Code
                '****************************************************************************************
                Pointer = 0

                ScopeIndex = PinMapContents(0)

                TimeStamp = VB.Timer()
                Do While VB.Timer() <= TimeStamp
                Loop
                TimeStamp = VB.Timer()
                Do While (Aperture - TimeStamp) < MinimumAperture
                    Pointer = Pointer + 1
                    terM9_fetchChannelState(dtiHandle, ScopeIndex, DigitizedData(Pointer), nDeskewRelay, iOverTemperature, iReferenceOverload)
                    Aperture = VB.Timer()
                Loop
                Aperture = Aperture - TimeStamp
                SampleRate = Pointer / Aperture
                SampleCount = Pointer
                HysterisisSamples = CInt(HysterisisSeconds * SampleRate)

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

                'Result
                ActualSampleRate = SampleRate
                ActualAperture = Aperture
                Frequency = TransitionCounter / Aperture / 2
                gFrmMain.txtMeasured.Text = CStr(Frequency)

                'Debugging Purposes
                'Echo "--------------------"
                'For CurrentSample = 1 To SampleCount
                '  If ConvertTeradyneState(DigitizedData(CurrentSample)) = True Then
                '    Echo "  |"
                '  Else
                '    Echo "|"
                '  End If
                'Next
                'Echo "--------------------"

                '****************************************************************************************

            Case "S" ' Single
                MisProbe = MsgBoxResult.Yes
                Do While MisProbe = MsgBoxResult.Yes
                    Failed = False
                    UserEvent = 0
                    bProbeClosed = False
                    gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        System.Windows.Forms.Application.DoEvents()
                    Loop
                    gFrmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    Frequency = 0

                    'High = 52 'TERM9_DETECTOR_HIGH
                    'Low = 53 'TERM9_DETECTOR_LOW

                    'Frequency Code
                    '****************************************************************************************
                    Pointer = 0

                    ScopeIndex = PinMapContents(0)

                    TimeStamp = VB.Timer()
                    Do While VB.Timer() <= TimeStamp
                    Loop
                    TimeStamp = VB.Timer()
                    Do While (Aperture - TimeStamp) < MinimumAperture
                        Pointer = Pointer + 1
                        terM9_fetchChannelState(dtiHandle, ScopeIndex, DigitizedData(Pointer), nDeskewRelay, iOverTemperature, iReferenceOverload)
                        Aperture = VB.Timer()
                    Loop
                    Aperture = Aperture - TimeStamp
                    SampleRate = Pointer / Aperture
                    SampleCount = Pointer
                    HysterisisSamples = CInt(HysterisisSeconds * SampleRate)

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

                    'Result
                    ActualSampleRate = SampleRate
                    ActualAperture = Aperture
                    Frequency = TransitionCounter / Aperture / 2
                    gFrmMain.txtMeasured.Text = CStr(Frequency)
                    '****************************************************************************************

                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                    If Failed = True And bMisProbeMsg = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                          MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                          "Analog Probe Failure")
                    Else
                        MisProbe = MsgBoxResult.No
                    End If
                Loop

            Case "C"
                MisProbe = MsgBoxResult.Yes
                Do While MisProbe = MsgBoxResult.Yes
                    UserEvent = 0
                    Failed = False
                    gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        Frequency = 0
                        'High = 52 'TERM9_DETECTOR_HIGH
                        'Low = 53 'TERM9_DETECTOR_LOW

                        'Frequency Code
                        '****************************************************************************************
                        Pointer = 0

                        ScopeIndex = PinMapContents(0)

                        TimeStamp = VB.Timer()
                        Do While VB.Timer() <= TimeStamp
                        Loop
                        TimeStamp = VB.Timer()
                        Do While (Aperture - TimeStamp) < MinimumAperture
                            Pointer = Pointer + 1
                            terM9_fetchChannelState(dtiHandle, ScopeIndex, DigitizedData(Pointer), nDeskewRelay, iOverTemperature, iReferenceOverload)
                            Aperture = VB.Timer()
                        Loop
                        Aperture = Aperture - TimeStamp
                        SampleRate = Pointer / Aperture
                        SampleCount = Pointer
                        HysterisisSamples = CInt(HysterisisSeconds * SampleRate)

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

                        'Result
                        ActualSampleRate = SampleRate
                        ActualAperture = Aperture
                        Frequency = TransitionCounter / Aperture / 2
                        gFrmMain.txtMeasured.Text = CStr(Frequency)

                        '****************************************************************************************
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                    Loop
                    gFrmMain.TimerProbe.Enabled = False
                    bProbeClosed = False

                    If Failed = True And bMisProbeMsg = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                          MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                          "Analog Probe Failure")
                    Else
                        MisProbe = MsgBoxResult.No
                    End If
                Loop
        End Select
    End Function
End Class