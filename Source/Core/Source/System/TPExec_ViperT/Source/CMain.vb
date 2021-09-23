Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

<System.Runtime.InteropServices.ProgId("cMain_NET.cMain")> Public Class cMain

	Public Sub SetProbeData(ByRef sProbeAssyFileName As String, ByRef sProbeDataFile As String)
		sProbeAssy = sProbeAssyFileName
		sProbeData = sProbeDataFile
	End Sub
	
    Public Function nBeginTest(ByRef TestProgramName As String, ByRef sTCCN As String, ByRef TestProgramVersion As String,
                               ByRef UUTPartNumber As String, ByRef boardName As String, ByRef IDName As String,
                               ByRef IDPartNumber As String, ByRef TPPath As String, ByRef iMaxPartsListPages As Short,
                               ByRef iMaxIDPartsListPages As Short, ByRef iMaxSchemPages As Short, ByRef iMaxIDSchemPages As Short,
                               ByRef iMaxAssyPages As Short, ByRef iMaxIDAssyPages As Short, ByRef sProbeAssyFileName As String,
                               ByRef sProbeDataFile As String, ByRef nEndToEndTime As Integer, ByRef nSafeToTurnOnTime As Integer,
                               ByRef nPowerOnTime As Integer, ByRef bDevelopment As Boolean) As Integer
        'Establish link with Test Program
        'Define Test Program Name, Version Number, UUT Part Number, Board Name and Path
        Dim sCDROM As String

        With TestData
            TPName = TestProgramName
            .sTPCCN = sTCCN
            TPVersion = TestProgramVersion
            UUTPartNo = UUTPartNumber
            IDPartNo = IDPartNumber
            UUTName = boardName
            ProgramPath = TPPath

            TPSGraphics = TPPath & "graphics\Form Images\"
            MaxSchematicPages = iMaxSchemPages
            MaxIDSchematicPages = iMaxIDSchemPages

            MaxPartsListPages = iMaxPartsListPages
            MaxIDPartsListPages = iMaxIDPartsListPages

            MaxAssemblyPages = iMaxAssyPages
            MaxIDAssemblyPages = iMaxIDAssyPages

            DocumentsPath = TPPath & "graphics\doc\"
            sProbeAssy = sProbeAssyFileName
            sProbeData = sProbeDataFile
            bSimulation = False
            bTPSDevelopment = bDevelopment
            bFirstRun = True
            bSystemResetComplete = True

            If nEndToEndTime > 60 Then
                gFrmMain.lblEndToEndRunTime.Text = (nEndToEndTime / 60).ToString("##0.00") & " Min."
            Else
                gFrmMain.lblEndToEndRunTime.Text = nEndToEndTime & " Sec."
            End If

            If nSafeToTurnOnTime > 60 Then
                gFrmMain.lblSTTORunTime.Text = (nSafeToTurnOnTime / 60).ToString("##0.00") & " Min."
            Else
                gFrmMain.lblSTTORunTime.Text = nSafeToTurnOnTime & " Sec."
            End If

            If nPowerOnTime > 60 Then
                gFrmMain.lblPowerOnRunTime.Text = (nPowerOnTime / 60).ToString("##0.00") & " Min."
            Else
                gFrmMain.lblPowerOnRunTime.Text = nPowerOnTime & " Sec."
            End If

            sCDROM = GetCompactDiscDrives()
            nFileError = GetPrivateProfileString("IETM", "FILE_NAME", "NONE", lpBuffer, 256, sCDROM & "VIPERT_TPS.INI")
            If UCase(Left(lpBuffer, 4)) = "NONE" Then
                gFrmMain.MenuOption(-VIEW_IETM).Enabled = False
                gFrmMain.MenuOptionText(-VIEW_IETM).Enabled = False
            Else
                gFrmMain.MenuOption(-VIEW_IETM).Enabled = True
                gFrmMain.MenuOptionText(-VIEW_IETM).Enabled = True
            End If
            IETMPath = sCDROM & Left(lpBuffer, nFileError)

            nBeginTest = 0
        End With
    End Function
	
	Public Sub SetUUTData(Optional ByRef sTypeOfTest As String = "UUT")
		Dim sType As String
        Dim sInstructions(3) As String
		Dim sTempSN As String

		With TestData
			If UCase(Left(sTypeOfTest, 1)) = "U" Then sType = "UUT" Else sType = "INTERFACE"
			
			'Set UUT Serial Number
			sTempSN = ""
			sInstructions(1) = "INPUT " & sType & " SERIAL NUMBER"
			sInstructions(2) = "(24 CHARACTERS MAXIMUM)"
			Do While sTempSN = ""
                sTempSN = UCase(sUserInput("** Operator Input **", System.Drawing.ContentAlignment.MiddleCenter, sInstructions))
			Loop 
			If sTempSN <> .sUUTSerial Then
				.sUUTSerial = sTempSN
                gFrmMain.lblEndToEndStatus.Text = "Unknown"
                gFrmMain.lblEndToEndStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                gFrmMain.lblEndToEndStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
				
                gFrmMain.lblSTTOStatus.Text = "Unknown"
                gFrmMain.lblSTTOStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                gFrmMain.lblSTTOStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
				
                gFrmMain.lblPwrOnStatus.Text = "Unknown"
                gFrmMain.lblPwrOnStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                gFrmMain.lblPwrOnStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
                gFrmMain.lblPwrOnStatus.Enabled = False
                gFrmMain.lblPwrOnStatus.Enabled = False
                gFrmMain.lblPwrOnStatus.Enabled = False
				
				If bModuleMenuBuilt = True Then
                    gFrmMain.lblModuleStatus_1.Text = "Unknown"
                    gFrmMain.lblModuleStatus_1.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                    gFrmMain.lblModuleStatus_1.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
                    'Disable all modules items except EndToEnd, STTO if New UUT
                    gFrmMain.lblModuleStatus_1.Enabled = False
                    gFrmMain.lblModuleStatus_1.Enabled = False
                    gFrmMain.lblModuleStatus_1.Enabled = False
                End If
            Else
                bFirstRun = False
                Exit Sub
            End If

            'Set UUT Revision
            If sType = "UUT" Then
                .sUUTRev = ""
                sInstructions(1) = "INPUT UUT REVISION"
                sInstructions(2) = "(24 CHARACTERS MAXIMUM)"
                Do While .sUUTRev = ""
                    .sUUTRev = UCase(sUserInput("** Operator Input **", System.Drawing.ContentAlignment.MiddleCenter, sInstructions))
                Loop
            Else
                .sUUTRev = "N/A"
            End If

            'Set ERO number
            .EROs = ""
            sInstructions(1) = "INPUT EQUIPMENT REPAIR ORDER NUMBER"
            sInstructions(2) = "(5 CHARACTERS MAXIMUM)"
            Do While .EROs = ""
                .EROs = UCase(sUserInput("** Operator Input **", System.Drawing.ContentAlignment.MiddleCenter, sInstructions))
            Loop
        End With
    End Sub

    Public Function bSetIDData() As Boolean
        Dim sInstructions(3) As String
        Dim sTempSN As String

        bSetIDData = True
        sTempSN = ""
        sInstructions(1) = "INPUT INTERFACE SERIAL NUMBER"
        sInstructions(2) = "(24 CHARACTERS MAXIMUM)"
        Do While sTempSN = ""
            sTempSN = UCase(sUserInput("** Operator Input **", System.Drawing.ContentAlignment.MiddleCenter, sInstructions))
        Loop
        gFrmMain.Refresh()

        'Check SN to see if new ID
        If sTempSN <> TestData.sIDSerial Then
            bSetIDData = False
            TestData.sIDSerial = sTempSN
            gFrmMain.MenuOption(5).Image = gFrmMain.ImageList1.Images(0)
        End If
        gFrmMain.Refresh()
    End Function

    Public Function nSetPartsList(ByRef sPartsList() As String) As Integer
        Dim i As Short

        MaxPartsListPages = UBound(sPartsList)
        ReDim sPartsListFileNames(MaxPartsListPages)
        Array.Copy(sPartsList, sPartsListFileNames, MaxPartsListPages + 1)
        If sPartsList(1) = "NONE" Then
            gFrmMain.MenuOption(VIEW_PARTSLIST * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_PARTSLIST * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
            gFrmMain.cboPartsList.Visible = False
            nSetPartsList = 0
            Exit Function
        Else
            gFrmMain.MenuOption(VIEW_PARTSLIST * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_PARTSLIST * -1).ForeColor = System.Drawing.Color.Black
            gFrmMain.cboPartsList.Visible = True
        End If

        'Empty ComboBox
        gFrmMain.cboPartsList.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For i = UBound(sPartsListFileNames) To 1 Step -1
            gFrmMain.cboPartsList.Items.Insert(0, sPartsListFileNames(i))
        Next i
        'Select First Chioce
        gFrmMain.cboPartsList.SelectedIndex = 0

        PartListPageNum = 1
        If Right(ProgramPath, 1) <> "\" Then
            PartListPath = ProgramPath & "\graphics\Pl\"
        Else
            PartListPath = ProgramPath & "graphics\Pl\"
        End If
        nSetPartsList = 0
    End Function

    Public Function nSetIDPartsList(ByRef sIDPartsList() As String) As Integer

        Dim i As Short 'bbbb added
        Dim sCDROM As String 'bbbb added

        MaxIDPartsListPages = UBound(sIDPartsList)
        ReDim sIDPartsListFileNames(MaxIDPartsListPages)
        Array.Copy(sIDPartsList, sIDPartsListFileNames, MaxIDPartsListPages + 1)
        If sIDPartsList(1) = "NONE" Then
            gFrmMain.MenuOption(VIEW_ID_PARTSLIST * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_ID_PARTSLIST * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
            gFrmMain.cboITAPartsList.Visible = False
            nSetIDPartsList = -1
            Exit Function
        Else
            gFrmMain.MenuOption(VIEW_ID_PARTSLIST * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_ID_PARTSLIST * -1).ForeColor = System.Drawing.Color.Black
            gFrmMain.cboITAPartsList.Visible = False
        End If
        'Empty ComboBox
        gFrmMain.cboITAPartsList.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For i = UBound(sIDPartsListFileNames) To 1 Step -1
            gFrmMain.cboITAPartsList.Items.Insert(0, sIDPartsListFileNames(i))
        Next i
        'Select First Chioce
        gFrmMain.cboITAPartsList.SelectedIndex = 0

        IDPartListPageNum = 1
        sCDROM = GetCompactDiscDrives()
        nFileError = GetPrivateProfileString(IDPartNo, "ID_PL", "E:\", lpBuffer, 256, sCDROM & "VIPERT_TPS.INI")
        IDPartListPath = sCDROM & Left(lpBuffer, nFileError)

        'IDPartListPath = ProgramPath + "\graphics\IDPl\"

        nSetIDPartsList = 0
    End Function

    Public Function nSetModuleList(ByRef sModuleNames() As String, ByRef nModuleRunTime() As Integer) As Integer
        MaxModules = sModuleNames.Count
        Array.Resize(ModuleNames, MaxModules)
        Array.Copy(sModuleNames, ModuleNames, MaxModules)
        Array.Resize(ModuleRunTime, MaxModules)
        Array.Copy(nModuleRunTime, ModuleRunTime, MaxModules)

        nSetModuleList = 0
    End Function

    Public Function nSetAssyList(ByRef sAssyList() As String) As Integer
        Dim i As Short

        MaxAssemblyPages = UBound(sAssyList)
        ReDim sAssyFileNames(MaxAssemblyPages)
        Array.Copy(sAssyList, sAssyFileNames, MaxAssemblyPages + 1)
        If sAssyList(1) = "NONE" Then
            gFrmMain.MenuOption(VIEW_ASSEMBLY * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_ASSEMBLY * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
            gFrmMain.cboAssembly.Visible = False
            nSetAssyList = 0
            Exit Function
        Else
            gFrmMain.MenuOption(VIEW_ASSEMBLY * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_ASSEMBLY * -1).ForeColor = System.Drawing.Color.Black
            gFrmMain.cboAssembly.Visible = True
        End If
        'Empty ComboBox
        gFrmMain.cboAssembly.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For i = UBound(sAssyFileNames) To 1 Step -1
            gFrmMain.cboAssembly.Items.Insert(0, sAssyFileNames(i))
        Next i
        'Select First Chioce
        gFrmMain.cboAssembly.SelectedIndex = 0

        AssyPageNum = 1
        If Right(ProgramPath, 1) <> "\" Then
            AssyPath = ProgramPath & "\graphics\Assy\"
        Else
            AssyPath = ProgramPath & "graphics\Assy\"
        End If
        nSetAssyList = 0
    End Function

    Public Function nSetIDAssyList(ByRef sIDAssyList() As String) As Integer
        Dim i As Short 'bbbb added
        Dim sCDROM As String 'bbbb added

        MaxIDAssemblyPages = UBound(sIDAssyList)
        ReDim sIDAssyFileNames(MaxIDAssemblyPages)
        Array.Copy(sIDAssyList, sIDAssyFileNames, MaxIDAssemblyPages + 1)
'		sIDAssyFileNames = VB6.CopyArray(sIDAssyList)
        If sIDAssyList(1) = "NONE" Then
            gFrmMain.MenuOption(VIEW_ID_ASSEMBLY * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_ID_ASSEMBLY * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
            gFrmMain.cboITAAssy.Visible = False
            nSetIDAssyList = 0
            Exit Function
        Else
            gFrmMain.MenuOption(VIEW_ID_ASSEMBLY * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_ID_ASSEMBLY * -1).ForeColor = System.Drawing.Color.Black
            gFrmMain.cboITAAssy.Visible = True
        End If

        'Empty ComboBox
        gFrmMain.cboITAAssy.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For i = UBound(sIDAssyFileNames) To 1 Step -1
            gFrmMain.cboITAAssy.Items.Insert(0, sIDAssyFileNames(i))
        Next i
        'Select First Chioce
        gFrmMain.cboITAAssy.SelectedIndex = 0

        IDAssyPageNum = 1
        sCDROM = GetCompactDiscDrives()
        nFileError = GetPrivateProfileString(IDPartNo, "ID_ASSY", "E:\", lpBuffer, 256, sCDROM & "VIPERT_TPS.INI")
        IDAssyPath = sCDROM & Left(lpBuffer, nFileError)

        'IDAssyPath = ProgramPath + "\graphics\idAssy\"

        nSetIDAssyList = 0
    End Function

    Public Function nSetDocuments(ByRef sTSR As String, ByRef sELTD As String, ByRef sInfo As String) As Integer
        sTSRFileName = sTSR
        If UCase(sTSR) = "NONE" Then
            gFrmMain.MenuOption(VIEW_TSR * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_TSR * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
        Else
            gFrmMain.MenuOption(VIEW_TSR * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_TSR * -1).ForeColor = System.Drawing.Color.Black
        End If

        sELTDFileName = sELTD
        If UCase(sELTD) = "NONE" Then
            gFrmMain.MenuOption(VIEW_ELTD * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_ELTD * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
        Else
            gFrmMain.MenuOption(VIEW_ELTD * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_ELTD * -1).ForeColor = System.Drawing.Color.Black
        End If

        sInfoFileName = sInfo
        If UCase(sInfo) = "NONE" Then
            gFrmMain.MenuOption(VIEW_GENERAL_INFO * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_GENERAL_INFO * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
        Else
            gFrmMain.MenuOption(VIEW_GENERAL_INFO * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_GENERAL_INFO * -1).ForeColor = System.Drawing.Color.Black
        End If

        If Right(ProgramPath, 1) <> "\" Then
            DocumentsPath = ProgramPath & "\graphics\doc\"
        Else
            DocumentsPath = ProgramPath & "graphics\doc\"
        End If

        nSetDocuments = 0
    End Function

    Public Function nSetSchematic(ByRef sSchemList() As String) As Integer
        Dim i As Short
        MaxSchematicPages = UBound(sSchemList)
        ReDim sSchemFileNames(MaxSchematicPages)
        Array.Copy(sSchemList, sSchemFileNames, MaxSchematicPages + 1)
        If sSchemList(1) = "NONE" Then
            gFrmMain.MenuOption(VIEW_SCHEMATIC * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_SCHEMATIC * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
            gFrmMain.cboSchematic.Visible = False
            nSetSchematic = 0
            Exit Function
        Else
            gFrmMain.MenuOption(VIEW_SCHEMATIC * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_SCHEMATIC * -1).ForeColor = System.Drawing.Color.Black
            gFrmMain.cboSchematic.Visible = True
        End If

        'Empty ComboBox
        gFrmMain.cboSchematic.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For i = UBound(sSchemFileNames) To 1 Step -1
            gFrmMain.cboSchematic.Items.Insert(0, sSchemFileNames(i))
        Next i
        'Select First Chioce
        gFrmMain.cboSchematic.SelectedIndex = 0

        SchemPageNum = 1
        If Right(ProgramPath, 1) <> "\" Then
            SchemPath = ProgramPath & "\graphics\schem\"
        Else
            SchemPath = ProgramPath & "graphics\schem\"
        End If

        nSetSchematic = 0
    End Function

    Public Function nSetIDSchematic(ByRef sIDSchemList() As String) As Integer
        Dim i As Short
        Dim slpBuffer As String = Space(256)
        Dim nFileError As Integer
        Dim sCDROM As String

        MaxIDSchematicPages = UBound(sIDSchemList)
        ReDim sIDSchemFileNames(MaxIDSchematicPages)
        Array.Copy(sIDSchemList, sIDSchemFileNames, MaxIDSchematicPages + 1)
        If sIDSchemList(1) = "NONE" Then
            gFrmMain.MenuOption(VIEW_ID_SCHEMATIC * -1).Enabled = False
            gFrmMain.MenuOptionText(VIEW_ID_SCHEMATIC * -1).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
            gFrmMain.cboITAWiring.Visible = False
            nSetIDSchematic = -1
            Exit Function
        Else
            gFrmMain.MenuOption(VIEW_ID_SCHEMATIC * -1).Enabled = True
            gFrmMain.MenuOptionText(VIEW_ID_SCHEMATIC * -1).ForeColor = System.Drawing.Color.Black
            gFrmMain.cboITAWiring.Visible = True
        End If

        'Empty ComboBox
        gFrmMain.cboITAWiring.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For i = UBound(sIDSchemFileNames) To 1 Step -1
            gFrmMain.cboITAWiring.Items.Insert(0, sIDSchemFileNames(i))
        Next i
        'Select First Chioce
        gFrmMain.cboITAWiring.SelectedIndex = 0

        IDSchemPageNum = 1
        sCDROM = GetCompactDiscDrives()
        nFileError = GetPrivateProfileString(IDPartNo, "ID_SCHEM", "E:\", lpBuffer, 256, sCDROM & "VIPERT_TPS.INI")
        IDSchemPath = sCDROM & Left(lpBuffer, nFileError)

        nSetIDSchematic = 0
    End Function

    Public Function nShowMainMenu() As Integer
        MAINMod.ShowMainMenu()
        nShowMainMenu = 0
    End Function

    Public Function nWaitForUserResponse() As Integer
        Dim sTemp As String

        sTemp = gFrmMain.lblStatus.Text
        gFrmMain.Timer1.Enabled = True
        gFrmMain.Timer2.Enabled = False
        gFrmMain.lblStatus.Text = "Waiting for User"

        UserEvent = 0
        Do While UserEvent = 0
            'added 5/23/03 - fix 100% CPU usage during idle time
            System.Threading.Thread.Sleep(1)
            System.Windows.Forms.Application.DoEvents()
            If UserEvent <> 0 Then
                Exit Do
            End If
        Loop
        nWaitForUserResponse = UserEvent
        gFrmMain.Timer1.Enabled = False
        gFrmMain.lblStatus.Text = sTemp
    End Function

    Public Function nTerminateShell() As Integer
        gFrmMain.Close()
        nTerminateShell = 0
    End Function

    Public Function nEcho(ByRef sText As String) As Integer
        Echo(sText)
        nEcho = 0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Function

    Public Function nChangeTextColor(ByRef nColor As Integer) As Integer
        gFrmMain.SeqTextWindow.ForeColor = System.Drawing.ColorTranslator.FromOle(nColor)
        nChangeTextColor = 0
    End Function

    Public Function nEnableAbort() As Integer
        EnableAbort()
        nEnableAbort = 0
    End Function

    Public Function nLongDelay(ByRef fTime As Single) As Integer
        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            nLongDelay = 0
            Exit Function
        End If
        LongDelay(fTime)
        nLongDelay = 0
    End Function

    Public Function nDisablePrint() As Integer
        DisablePrint()
        nDisablePrint = 0
    End Function

    Public Function nEnableContinue() As Integer
        EnableContinue()
        nEnableContinue = 0
    End Function

    Public Function nEnableQuit() As Integer
        EnableQuit()
        nEnableQuit = 0
    End Function

    Public Function nDisableQuit() As Integer
        DisableQuit()
        nDisableQuit = 0
    End Function

    Public Function nUpdateProgress(ByRef fPercentComplete As Single) As Integer
        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            nUpdateProgress = 0
            Exit Function
        End If
        UpdateProgress(fPercentComplete)
        nUpdateProgress = 0
    End Function

    Public Function nShowTestWindow() As Integer
        gFrmMain.fraInstructions.Visible = False
        gFrmMain.fraTestInformation.Visible = True
        gFrmMain.fraMeasurement.Visible = True
        gFrmMain.fraInstrument.Visible = True

        ShowWindow(gFrmMain.SeqTextWindow)
        gFrmMain.AbortButton.Enabled = True
        gFrmMain.MainMenuButton.Enabled = False

        nShowTestWindow = 0
    End Function

    Public Function nDisableContinue() As Integer
        DisableContinue()
        nDisableContinue = 0
    End Function

    Public Function nDisableRerun() As Integer
        DisableRerun()
        nDisableRerun = 0
    End Function

    Public Function nEnablePrint() As Integer
        EnablePrint()
        nEnablePrint = 0
    End Function

    Public Function nEnableReRun() As Integer
        EnableRerun()
        nEnableReRun = 0
    End Function

    Public Function nMsgBox(ByRef sPrompt As String, ByRef iTypeButton As Short, ByRef sTitle As String) As Integer
        nMsgBox = MsgBox(sPrompt, iTypeButton, sTitle)
    End Function

    Public Function nShowSplashScreen(ByRef sImgLogo As String, ByRef sWeaponSystem As String,
                                      ByRef sWeaponSystemNomenclature As String, ByRef sDevelopedBy As String,
                                      ByRef sAddress As String) As Integer
        CenterForm(gFrmSplash)
        gFrmSplash.imgLogo.Image = System.Drawing.Image.FromFile(TPSGraphics & sImgLogo)
        WeaponSystem = sWeaponSystem
        WeaponSystemNomenclature = sWeaponSystemNomenclature
        DevelopedBy = sDevelopedBy
        address = sAddress
        MAINMod.ShowSplashScreen()
        nShowSplashScreen = 0
    End Function

    Public Function nInputBox(ByRef sPrompt As String, ByRef sTitle As String) As Integer
        nInputBox = CInt(InputBox(sPrompt, sTitle))
    End Function

    Public Function bAcknowledgeHVWarning() As Boolean
        iAcknowledge = 2
        gFrmHV.ShowDialog()
        bHighVoltage = True
        If iAcknowledge = 0 Then bAcknowledgeHVWarning = True Else bAcknowledgeHVWarning = False
    End Function

    Public Function nDelay(ByRef nTime As Single) As Integer
        'DESCRIPTION:
        '   Delays the program for a specified time. If delay is
        '   more than 7 seconds, then the shell is called to display
        '   a progress bar to display time remaining.
        'PARAMETERS:
        '   Seconds = number of seconds to delay
        'EXAMPLE:
        '           Delay 2.3
        Dim T As Single

        T = VB.Timer()
        If System.Math.Abs(nTime) > 7 Then 'bbbb was Abs(seconds)
            LongDelay(nTime)
        Else
            Do While VB.Timer() - T < nTime
                'added 5/23/03 - fix 100% CPU usage during idle time
                System.Threading.Thread.Sleep(1)
                If UserEvent = ABORT_BUTTON Then
                    Err.Raise(USER_EVENT + ABORT_BUTTON)
                    nDelay = 0
                    Exit Function
                End If
'				System.Windows.Forms.Application.DoEvents()
            Loop
        End If
        nDelay = 0
    End Function

    Public Sub PrintHeadLine(ByRef sTestName As String)
        Dim StrTestNumber As String = Space(9)
        Dim SubName As String = Space(22) 'Sub name 20 characters max. (leave 2 spaces)
        Dim StrLow_Limit As String = Space(12)
        Dim StrMeasured As String = Space(12)
        Dim StrHigh_Limit As String = Space(12)
        Dim StrUnit As String = Space(6)
        Dim PassFail As String = Space(8)

        gFrmMain.txtModule.Text = sTestName
        StrTestNumber = "Test"
        SubName = "SubTest Name"
        StrLow_Limit = "Low Limit"
        StrMeasured = "Measured"
        StrHigh_Limit = "High Limit"
        StrUnit = "Units"
        PassFail = "P/F"

        ChangeTextColor(System.Drawing.Color.Black)

        'PRINT
        Echo("")
        Echo("**** MODULE NAME: " & sTestName & " ****")
        Echo("")
        Echo(StrTestNumber & SubName & StrLow_Limit & StrMeasured & StrHigh_Limit & StrUnit & PassFail)
        Echo("----     ------------          ---------   --------    ----------  ----- --- ")
    End Sub

    Public Sub PrintTestResult(ByRef sTestPath As String, ByRef sPrintFormat As String, Optional ByRef sMeasured As String = Nothing)
        Dim SubName As String
        Dim StrTestNumber As String = ""
        Dim StrHigh_Limit As String = ""
        Dim StrLow_Limit As String = ""
        Dim StrMeasured As String = ""
        Dim StrUnit As String = ""
        Dim PassFail As String = ""
        Dim dLowLimit As Double 'bbbb
        Dim dHighLimit As Double 'bbbb
        Dim dMeasured As Double 'bbbb

        SubName = gFrmMain.txtTestName.Text
        StrTestNumber = gFrmMain.txtStep.Text
        StrHigh_Limit = gFrmMain.txtUpperLimit.Text
        StrLow_Limit = gFrmMain.txtLowerLimit.Text
        StrMeasured = gFrmMain.txtMeasured.Text
        StrUnit = gFrmMain.txtUnit.Text
        sPrintFormat = UCase(sPrintFormat)

        dLowLimit = Val(StrLow_Limit) ' bbbb
        dHighLimit = Val(StrHigh_Limit) 'bbbb
        If sMeasured = String.Empty Then sMeasured = "0"
        dMeasured = Val(sMeasured)

        gFrmMain.fraInstructions.Visible = False

        Select Case sPrintFormat

            Case "BCD", "DIG", "DIGITAL", "BIT" 'DIGITAL DISPLAY IN BCD
                If IsNumeric(StrLow_Limit) Then
                    StrLow_Limit = LSet("h" & Hex(CInt(StrLow_Limit)), Len(StrLow_Limit))
                End If
                If IsNumeric(StrMeasured) Then
                    StrMeasured = LSet("h" & Hex(CInt(StrMeasured)), Len(StrMeasured))
                End If
                If IsNumeric(StrHigh_Limit) Then
                    StrHigh_Limit = LSet("h" & Hex(CInt(StrHigh_Limit)), Len(StrHigh_Limit))
                End If

            Case "SPECIAL"
                'Changed 4/15/03 - Craig R. Weirich
                StrMeasured = sMeasured

            Case "STR", "STRING" 'for UUT Verification or visual verification
                sPrintFormat = "STR"

            Case "NO TEST"

                StrLow_Limit = "Set Up"
                StrMeasured = "Only"
                StrHigh_Limit = "No Test"

            Case "EXP" 'Exponential

                StrLow_Limit = LSet(Convert.ToDouble(StrLow_Limit).ToString("0.000E+00"), StrLow_Limit.Length)
                StrMeasured = LSet(Convert.ToDouble(StrMeasured).ToString("0.000E+00"), StrMeasured.Length)
                StrHigh_Limit = LSet(Convert.ToDouble(StrHigh_Limit).ToString("0.000E+00"), StrHigh_Limit.Length)

            Case "DEC", "DECIMAL" 'Decimal

                If dLowLimit < 999999 Then ' was Low_Limit bbbb?
                    StrLow_Limit = LSet(Convert.ToDouble(StrLow_Limit).ToString("000000.00"), StrLow_Limit.Length) 'Decimal
                Else
                    StrLow_Limit = LSet(Convert.ToDouble(StrLow_Limit).ToString("0.000E+00"), StrLow_Limit.Length) 'Exponential
                End If

                If dMeasured < 999999 Then
                    StrMeasured = LSet(Convert.ToDouble(StrMeasured).ToString("000000.00"), StrMeasured.Length) 'Decimal
                Else
                    StrMeasured = LSet(Convert.ToDouble(StrMeasured).ToString("0.000E+00"), StrMeasured.Length) 'Exponential
                End If

                If dHighLimit < 999999 Then ' was High_Limit bbbb?
                    StrHigh_Limit = LSet(Convert.ToDouble(StrHigh_Limit).ToString("000000.00"), StrHigh_Limit.Length) 'Decimal
                Else
                    StrHigh_Limit = LSet(Convert.ToDouble(StrHigh_Limit).ToString("0.000E+00"), StrHigh_Limit.Length) 'Exponential
                End If

            Case Else 'DEFAULT in Exponantial Format

                StrLow_Limit = LSet(Convert.ToDouble(StrLow_Limit).ToString("0.000E+00"), StrLow_Limit.Length)
                StrMeasured = LSet(Convert.ToDouble(StrMeasured).ToString("0.000E+00"), StrMeasured.Length)
                StrHigh_Limit = LSet(Convert.ToDouble(StrHigh_Limit).ToString("0.000E+00"), StrHigh_Limit.Length)

        End Select

        If Not Pass Then
            PassFail = "FAIL"
            ChangeTextColor(System.Drawing.Color.Red)
        Else
            PassFail = "PASS"
        End If

        If dHighLimit = 1.0E+99 Then
            StrHigh_Limit = "N/A"
        End If

        If dLowLimit = -1.0E-99 Then
            StrLow_Limit = "N/A"
        End If

        If dHighLimit = 1.0E+99 And dLowLimit = -1.0E-99 And sPrintFormat = "STR" Then
            StrLow_Limit = "User Input D"
            StrMeasured = "ata"
            StrHigh_Limit = ""
            StrUnit = "DATA"
        End If

        If Not Pass Then
            'Store Most Recent Failure Record
            'Set current Failed record
            CurrentFailure.sSubName = gFrmMain.txtTestName.Text
            CurrentFailure.sTestNumber = gFrmMain.txtStep.Text
            CurrentFailure.sPath = sTestPath
            CurrentFailure.sHighLimit = gFrmMain.txtUpperLimit.Text
            CurrentFailure.sLowLimit = gFrmMain.txtLowerLimit.Text
            CurrentFailure.sMeasured = gFrmMain.txtMeasured.Text
            CurrentFailure.sUnit = gFrmMain.txtUnit.Text
        End If

        Echo(StrTestNumber & SubName & StrLow_Limit & StrMeasured & StrHigh_Limit & StrUnit & PassFail)
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Public Function nShowModuleMenu() As Integer
        gFrmMain.fraInstructions.Visible = False
        MAINMod.ShowModuleMenu()
        nShowModuleMenu = 0
    End Function

    Public Function nSimulation(ByRef bStatus As Boolean) As Integer
        bSimulation = bStatus
        nSimulation = 0 'bbbb was nSetSimulation = 0
    End Function

    Public Sub SetModuleStatus(ByRef iModule As Short, ByRef bStatus As Boolean)
        Select Case iModule
            Case ID_SURVEY
                If Not bStatus Then
                    gFrmMain.lblEndToEndStatus.Text = "Unknown"
                    gFrmMain.lblEndToEndStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                    gFrmMain.lblEndToEndStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

                    gFrmMain.lblSTTOStatus.Text = "Unknown"
                    gFrmMain.lblSTTOStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                    gFrmMain.lblSTTOStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

                    gFrmMain.lblPwrOnStatus.Text = "Unknown"
                    gFrmMain.lblPwrOnStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                    gFrmMain.lblPwrOnStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

                    If bModuleMenuBuilt = True Then
                        gFrmMain.lblModuleStatus_1.Text = "Unknown"
                        gFrmMain.lblModuleStatus_1.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                        gFrmMain.lblModuleStatus_1.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
                    End If
                    gFrmMain.MenuOption(5).Image = gFrmMain.ImageList1.Images(1)
                Else
                    gFrmMain.MenuOption(5).Image = gFrmMain.ImageList1.Images(2)
                End If

            Case END_TO_END
                If Not bStatus Then
                    gFrmMain.lblEndToEndStatus.Text = "FAILED"
                    gFrmMain.lblEndToEndStatus.BackColor = System.Drawing.Color.Red
                Else
                    gFrmMain.lblEndToEndStatus.Text = "PASSED"
                    gFrmMain.lblEndToEndStatus.BackColor = System.Drawing.Color.Lime
                End If
                gFrmMain.lblEndToEndStatus.ForeColor = System.Drawing.Color.Black

            Case STTO
                If Not bStatus Then
                    gFrmMain.lblSTTOStatus.Text = "FAILED"
                    gFrmMain.lblSTTOStatus.BackColor = System.Drawing.Color.Red
                Else
                    gFrmMain.lblSTTOStatus.Text = "PASSED"
                    gFrmMain.lblSTTOStatus.BackColor = System.Drawing.Color.Lime
                End If
                gFrmMain.lblSTTOStatus.ForeColor = System.Drawing.Color.Black

            Case PWR_ON
                If Not bStatus Then
                    gFrmMain.lblPwrOnStatus.Text = "FAILED"
                    gFrmMain.lblPwrOnStatus.BackColor = System.Drawing.Color.Red
                Else
                    gFrmMain.lblPwrOnStatus.Text = "PASSED"
                    gFrmMain.lblPwrOnStatus.BackColor = System.Drawing.Color.Lime
                End If
                gFrmMain.lblPwrOnStatus.ForeColor = System.Drawing.Color.Black

            Case 1 To MaxModules
                If Not bStatus Then
                    gFrmMain.lblModuleStatus_1.Text = "FAILED"
                    gFrmMain.lblModuleStatus_1.BackColor = System.Drawing.Color.Red
                Else
                    gFrmMain.lblModuleStatus_1.Text = "PASSED"
                    gFrmMain.lblModuleStatus_1.BackColor = System.Drawing.Color.Lime
                End If
                gFrmMain.lblModuleStatus_1.ForeColor = System.Drawing.Color.Black

        End Select
    End Sub

    Public Sub ReSetModuleStatus()
        gFrmMain.lblEndToEndStatus.Text = "Unknown"
        gFrmMain.lblEndToEndStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0) ' light green
        gFrmMain.lblEndToEndStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) ' bright blue

        gFrmMain.lblSTTOStatus.Text = "Unknown"
        gFrmMain.lblSTTOStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
        gFrmMain.lblSTTOStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        gFrmMain.lblPwrOnStatus.Text = "Unknown"
        gFrmMain.lblPwrOnStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
        gFrmMain.lblPwrOnStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        gFrmMain.lblModuleStatus_1.Text = "Unknown"
        gFrmMain.lblModuleStatus_1.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
        gFrmMain.lblModuleStatus_1.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
    End Sub

    Public Sub DisplayUserMsgImage(ByRef sTitle As String, ByRef iTextJustification As Drawing.ContentAlignment, ByRef sLinesOfText() As String, ByRef sImage As String)
        Dim i As Short
        Dim nNumberoflines As Integer
        Dim sTemp As String
        Dim bCurrentState As Boolean
        Dim bTempProgressBar As Boolean

        sTemp = gFrmMain.lblStatus.Text
        bCurrentState = gFrmMain.Timer2.Enabled
        bTempProgressBar = gFrmMain.ProgressBar.Visible
        gFrmMain.ProgressBar.Visible = False

        gFrmMain.Timer1.Enabled = True
        gFrmMain.Timer2.Enabled = False
        gFrmMain.lblStatus.Text = "Waiting for User ..."

        UserEvent = 0

        nNumberoflines = UBound(sLinesOfText)
        gFrmMain.MainMenuButton.Enabled = True
        gFrmMain.AbortButton.Enabled = False

        gFrmMain.SSPanel1.Top = 8
        gFrmMain.SSPanel1.Left = 8
        gFrmMain.SSPanel1.Height = 160
        gFrmMain.SSPanel1.Width = gFrmMain.Width - 12
        gFrmMain.msgInBox.Top = 0
        gFrmMain.msgInBox.Left = 0
        gFrmMain.lblMsg_1.Left = 0
        gFrmMain.SSPanel1.BringToFront()

        gFrmMain.SSPanel2.Top = gFrmMain.SSPanel1.Top + gFrmMain.SSPanel1.Height
        gFrmMain.SSPanel2.Left = 8
        gFrmMain.SSPanel2.Height = gFrmMain.fraProgramNavigation.Top - 8 - gFrmMain.SSPanel1.Height - 8
        gFrmMain.SSPanel2.Width = gFrmMain.SSPanel1.Width
        gFrmMain.picGraphic.Top = 0
        gFrmMain.picGraphic.Left = 0
        gFrmMain.picGraphic.Height = gFrmMain.SSPanel2.Height
        gFrmMain.picGraphic.Width = gFrmMain.SSPanel2.Width - 16
        gFrmMain.SSPanel2.BringToFront()

        gFrmMain.msgInBox.Width = gFrmMain.SSPanel1.Width - 16
        gFrmMain.lblMsg_1.Width = gFrmMain.msgInBox.Width - 20

        gFrmMain.msgInBox.Height = gFrmMain.SSPanel1.Height - 15
        gFrmMain.msgInBox.AutoScrollMinSize = New Size(gFrmMain.msgInBox.Width - 40, gFrmMain.msgInBox.Height - 7)
        gFrmMain.lblMsg_1.TextAlign = iTextJustification
        gFrmMain.lblMsg_1.Text = sLinesOfText(1)
        gFrmMain.lblMsg_1.Visible = True

        Dim lbl As Label
        For i = 2 To nNumberoflines
            lbl = gFrmMain.lblMsg(i)
            lbl.Top = gFrmMain.lblMsg(i - 1).Top + 16
            lbl.Text = sLinesOfText(i)
            lbl.Visible = True
        Next i

        gFrmMain.picGraphic.Image = System.Drawing.Image.FromFile(TPSGraphics & sImage)
        gFrmMain.SSPanel1.Visible = True
        gFrmMain.SSPanel2.Visible = True

        Do While UserEvent = 0
            System.Windows.Forms.Application.DoEvents()
            Delay(0.3)
        Loop
        gFrmMain.ProgressBar.Visible = bTempProgressBar
        gFrmMain.Refresh()

        'added 5/16/03 by Soon Nam
        'performing QUIT causes invalid Err.Raise number due to
        'the fact that all forms have been unloaded from QUIT button operation in ExitForm
        'must bypass frm.Main commands when QUIT is pressed.

        If UserEvent = QUIT_BUTTON Then ' bbbb Apr, 2011
            If FormMainLoaded = True Then
                Err.Raise(USER_EVENT + QUIT_BUTTON)
            End If
        End If

        For i = 2 To nNumberoflines
            gFrmMain.UnloadLabel(gFrmMain.lblMsg(i))
        Next i

        gFrmMain.SSPanel1.Visible = False
        gFrmMain.SSPanel2.Visible = False
        gFrmMain.lblStatus.Text = sTemp
        gFrmMain.Timer2.Enabled = bCurrentState

        gFrmMain.Timer1.Enabled = False

        'If UserEvent = QUIT_BUTTON Then Err.Raise USER_EVENT + QUIT_BUTTON
        If UserEvent = MAINMENU_BUTTON Then Err.Raise(USER_EVENT + MAINMENU_BUTTON)
        gFrmMain.Refresh()
    End Sub

    Public Sub DisplayUserMsg(ByRef sTitle As String, ByRef nTextJustification As Short, ByRef sLinesOfText() As String)
        Dim i As Short
        Dim nNumberoflines As Integer
        Dim sTemp As String
        Dim bCurrentState As Boolean
        Dim bContinueState As Boolean
        Dim bQuitState As Boolean
        Dim bMainMenuState As Boolean

        sTemp = gFrmMain.lblStatus.Text
        bCurrentState = gFrmMain.Timer2.Enabled
        bContinueState = gFrmMain.cmdContinue.Enabled
        bQuitState = gFrmMain.Quit.Enabled
        bMainMenuState = gFrmMain.MainMenuButton.Enabled

        gFrmMain.cmdContinue.Enabled = False

        gFrmMain.Timer1.Enabled = True
        gFrmMain.Timer2.Enabled = False
        gFrmMain.lblStatus.Text = "Waiting for User ..."

        nNumberoflines = UBound(sLinesOfText)

        gFrmOperatorMsg.lblInput.Visible = False
        gFrmOperatorMsg.InputData.Visible = False
        gFrmOperatorMsg.Width = 757

        If nNumberoflines > 20 Then
            'Set limit on Form Size
            gFrmOperatorMsg.Height = 443
        Else
            gFrmOperatorMsg.Height = 80 + (nNumberoflines * 16)
        End If
        gFrmOperatorMsg.msgInBox.AutoScrollMinSize = New Size(gFrmOperatorMsg.msgInBox.AutoScrollMinSize.Width, gFrmOperatorMsg.msgInBox.Height - 20)
        'Build Form

        gFrmOperatorMsg.lblMsg_1.TextAlign = nTextJustification
        gFrmOperatorMsg.lblMsg_1.Text = sLinesOfText(1)

        For i = 2 To nNumberoflines
            gFrmOperatorMsg.lblMsg(i).Top = gFrmOperatorMsg.lblMsg(i - 1).Top + 16
            gFrmOperatorMsg.lblMsg(i).Left = gFrmOperatorMsg.lblMsg_1.Left
            gFrmOperatorMsg.lblMsg(i).Text = sLinesOfText(i)
            gFrmOperatorMsg.lblMsg(i).Visible = True
            gFrmOperatorMsg.lblMsg(i).Width = gFrmOperatorMsg.lblMsg_1.Width
            gFrmOperatorMsg.lblMsg(i).Height = gFrmOperatorMsg.lblMsg_1.Height
            gFrmOperatorMsg.lblMsg(i).TextAlign = nTextJustification
        Next i

        gFrmOperatorMsg.Text = sTitle
        CenterForm(gFrmOperatorMsg)
        gFrmOperatorMsg.ShowDialog()

        gFrmMain.lblStatus.Text = sTemp
        gFrmMain.Timer2.Enabled = bCurrentState

        gFrmMain.Timer1.Enabled = False

        sTemp = CStr(gFrmMain.lblStatus.Text = sTemp)
        gFrmMain.Timer2.Enabled = bCurrentState
        gFrmMain.cmdContinue.Enabled = bContinueState
        gFrmMain.Quit.Enabled = bQuitState
        gFrmMain.MainMenuButton.Enabled = bMainMenuState
        gFrmMain.Refresh()
    End Sub

    Public Function sDisplayUserInput(ByRef sTitle As String, ByRef nTextJustification As Integer, ByRef sLinesOfText() As String) As String
        sDisplayUserInput = sUserInput(sTitle, nTextJustification, sLinesOfText)
    End Function

    Public Sub FaultDisplay(ByRef sSubName As String, ByRef sTestNumber As String, ByRef sPath As String, ByRef dHighLimit As Double,
                            ByRef dLowLimit As Double, ByRef dMeasured As Double, ByRef sStrUnit As String, ByRef sPrintFormat As String, ByRef sPCOF() As String)
        Dim n As Integer
        Dim i As Short
        Dim sFailedAtData As String

        gFrmMain.Timer1.Enabled = True
        gFrmMain.lblStatus.Text = "Waiting for User"

        ShowWindow(gFrmMain.SeqTextWindow)
        gFrmMain.AbortButton.Visible = False
        gFrmMain.AbortButton.Enabled = False
        gFrmMain.RerunButton.Visible = True
        gFrmMain.RerunButton.Enabled = False
        gFrmMain.MainMenuButton.Enabled = True
        gFrmMain.Quit.Enabled = True
        gFrmMain.PrintButton.Visible = True
        gFrmMain.PrintButton.Enabled = True
        gFrmMain.cmdContinue.Enabled = True
        gFrmMain.SeqTextWindowLabel.ForeColor = System.Drawing.Color.Red
        gFrmMain.cmdFHDB.Enabled = True
        gFrmMain.cmdFHDB.Visible = True

        Echo("")
        Echo("                              *** TEST STATUS ***")
        Echo("")
        Echo("                     UUT FAILED: STEP " & CurrentFailure.sTestNumber & " " & CurrentFailure.sSubName)
        If UCase(CurrentFailure.sUnit) = "DTB" Then

            sFailedAtData = "FAILED AT: DTB Returned no value"
            CurrentFailure.sPath = "DTB Returned no value"
            'FIND FROM PCOF FAILED AT DATA
            For n = 1 To UBound(sPCOF)
                If InStr(sPCOF(1), "DIAG") <> 0 Then
                    sFailedAtData = "FAILED AT: DTB"
                    CurrentFailure.sPath = "DTB"
                    Exit For
                ElseIf InStr(sPCOF(n), "Primary Output") = 0 Then
                Else
                    sFailedAtData = "FAILED AT: " & sPCOF(n)
                    CurrentFailure.sPath = sPCOF(n)
                    i = n
                End If
            Next n
            Echo(Space(CShort((79 - Len(sFailedAtData)) / 2)) & sFailedAtData)
        Else
            If Len("FAILED AT: " & CurrentFailure.sPath) < 80 Then
                Echo(Space(CShort((79 - Len("FAILED AT: " & CurrentFailure.sPath)) / 2)) & "FAILED AT: " & CurrentFailure.sPath)
            Else
                Echo("FAILED AT: " & CurrentFailure.sPath)

            End If
        End If
        Echo("")

        If CurrentFailure.sHighLimit = CurrentFailure.sLowLimit Then
            If UCase(CurrentFailure.sUnit) = "QUESTION" Or UCase(CurrentFailure.sUnit) = "Y/N" Then
                Echo("                   EXPECTED VALUE: " & CurrentFailure.sHighLimit)
            Else
                Select Case UCase(CurrentFailure.sUnit)
                    Case "BIT", "DIG", "BCD"
                        Echo("                   EXPECTED VALUE: " & ConvertHexToBin(Hex(CDbl(CurrentFailure.sLowLimit))) & " " & CurrentFailure.sUnit)
                    Case "DTB"
                        Echo("                   EXPECTED VALUE: DTB PASSED")
                    Case Else
                        Echo("                   EXPECTED VALUE: " & CurrentFailure.sLowLimit & " " & CurrentFailure.sUnit)
                End Select
            End If
        Else
            If CurrentFailure.sLowLimit = "N/A" Then
                Echo("                   EXPECTED VALUE: LESS THAN " & CurrentFailure.sHighLimit & " " & CurrentFailure.sUnit)
            ElseIf CurrentFailure.sHighLimit = "N/A" Then
                Echo("                   EXPECTED VALUE: GREATER THAN " & CurrentFailure.sLowLimit & " " & CurrentFailure.sUnit)
            Else
                Echo("                   EXPECTED VALUE: " & CurrentFailure.sLowLimit & " TO " & CurrentFailure.sHighLimit & " " & CurrentFailure.sUnit)
            End If
        End If

        If UCase(CurrentFailure.sUnit) = "QUESTION" Or UCase(CurrentFailure.sUnit) = "Y/N" Then
            Echo("                   MEASURED VALUE: " & CurrentFailure.sMeasured)
        Else
            Select Case UCase(CurrentFailure.sUnit)
                Case "BIT", "DIG", "BCD"
                    Echo("                   MEASURED VALUE: " & ConvertHexToBin(Hex(CInt(CurrentFailure.sMeasured))) & " " & CurrentFailure.sUnit)
                Case "DTB"
                    Echo("                   MEASURED VALUE: DTB FAILED")
                Case Else
                    Echo("                   MEASURED VALUE: " & CurrentFailure.sMeasured & " " & CurrentFailure.sUnit)
            End Select
        End If
        Echo("")
        Echo("                          PROBABLE CAUSE OF FAILURE:")
        Echo("")
        With TestData
            If UCase(CurrentFailure.sUnit) = "DTB" Then
                If CurrentFailure.sPath <> "DTB Returned no value" Then
                    For n = i To UBound(sPCOF)
                        Echo(Space(CShort((79 - Len(sPCOF(n))) / 2)) & sPCOF(n))
                        If n = 1 Then
                            .sCallout = sPCOF(n) & vbCrLf
                        Else
                            .sCallout = .sCallout & sPCOF(n) & vbCrLf
                        End If
                    Next n
                Else
                    For n = 1 To UBound(sPCOF)
                        Echo(Space(CShort((79 - Len(sPCOF(n))) / 2)) & sPCOF(n))
                        If n = 1 Then
                            .sCallout = sPCOF(n) & vbCrLf
                        Else
                            .sCallout = .sCallout & sPCOF(n) & vbCrLf
                        End If
                    Next n
                End If
            Else
                For n = 1 To UBound(sPCOF)
                    Echo(Space(CShort((79 - Len(sPCOF(n))) / 2)) & sPCOF(n))
                    If n = 1 Then
                        .sCallout = sPCOF(n) & vbCrLf
                    Else
                        .sCallout = .sCallout & sPCOF(n) & vbCrLf
                    End If
                Next n
            End If

            If bEndToEnd Then
                .sFailStep = "E" & sTestNumber
            Else
                .sFailStep = "M" & sTestNumber
            End If
            If UCase(Left(sTestNumber, 2)) = "ID" Then
                .sFailStep = sTestNumber
            End If
            .nStatus = False
            .SStop = CStr(Now)
            If Not IsNumeric(CurrentFailure.sMeasured) Then CurrentFailure.sMeasured = "9.7E+37"
            .dMeasurement = CDbl(CurrentFailure.sMeasured)

            If Not IsNumeric(CurrentFailure.sLowLimit) Then CurrentFailure.sLowLimit = CStr(-1.0E-99)
            .dlLimit = CDbl(CurrentFailure.sLowLimit)
            If Not IsNumeric(CurrentFailure.sHighLimit) Then CurrentFailure.sHighLimit = CStr(1.0E+99)
            .duLimit = CDbl(CurrentFailure.sHighLimit)
            .sUOM = CurrentFailure.sUnit
        End With
    End Sub

    Public Sub UUTPassed(ByRef bStatus As Boolean)
        Dim sUUTStatus As String
        Dim i As Short 'bbbb

        ShowWindow(gFrmMain.SeqTextWindow)
        gFrmMain.AbortButton.Enabled = False
        gFrmMain.NewUUT.Enabled = False
        gFrmMain.NewUUT.Visible = False
        gFrmMain.cmdContinue.Enabled = True
        gFrmMain.MainMenuButton.Enabled = True
        With TestData
            If bStatus Then
                gFrmMain.SeqTextWindowLabel.ForeColor = System.Drawing.Color.Green
                sUUTStatus = "UUT IS READY FOR ISSUE"
                .nStatus = bStatus
                .SStop = CStr(Now)
                If bStatus Then
                    .sCallout = "N/A"
                    .dMeasurement = 0
                    .dlLimit = 0
                    .duLimit = 0
                    .sUOM = "N/A"
                    .sFailStep = "E0"
                    .duLimit = 0
                    .dlLimit = 0
                    .sComment = "No operator comments"

                    If Not bSimulation Then i = FHDB.SaveData(.sStart, .SStop, .EROs, .sTPCCN, .sUUTSerial, .sUUTRev, .sIDSerial, .nStatus, .sFailStep, .sCallout, .dMeasurement, .sUOM, .duLimit, .dlLimit, .sComment)
                End If
            Else
                gFrmMain.SeqTextWindowLabel.ForeColor = System.Drawing.Color.Red
                sUUTStatus = "UUT IS NOT ACCEPTABLE"
            End If
        End With

        Echo("")
        Echo("")
        Echo("*******************************************************************************")
        Echo("                     TESTING COMPLETE: " & sUUTStatus)
        Echo("                      DATE/TIME: " & DateTime.Now.ToString("dddd, dd-MMM-yyyy HH:mm"))
        Echo("*******************************************************************************")
    End Sub

    Public Sub IDPassed(ByRef bStatus As Boolean)
        Dim sIDStatus As String
        Dim i As Short ' bbbb

        ShowWindow(gFrmMain.SeqTextWindow)
        gFrmMain.AbortButton.Enabled = False
        gFrmMain.MainMenuButton.Enabled = True
        With TestData
            If bStatus Then
                gFrmMain.SeqTextWindowLabel.ForeColor = System.Drawing.Color.Green
                sIDStatus = "INTERFACE DEVICE PASSED"
                .nStatus = bStatus
                .SStop = CStr(Now)
                If bStatus Then
                    .sCallout = "N/A"
                    .dMeasurement = 0
                    .dlLimit = 0
                    .duLimit = 0
                    .sUOM = "N/A"
                    .sFailStep = "E0"
                    .duLimit = 0
                    .dlLimit = 0
                    .sComment = "No operator comments"

                    If Not bSimulation Then i = FHDB.SaveData(.sStart, .SStop, .EROs, .sTPCCN, .sUUTSerial, .sUUTRev, .sIDSerial, .nStatus, .sFailStep, .sCallout, .dMeasurement, .sUOM, .duLimit, .dlLimit, .sComment)
                End If
            Else
                gFrmMain.SeqTextWindowLabel.ForeColor = System.Drawing.Color.Red
                sIDStatus = "INTERFACE DEVICE IS NOT ACCEPTABLE"
            End If
        End With
        Echo("")
        Echo("")
        Echo("*******************************************************************************")
        Echo("                     TESTING COMPLETE: " & sIDStatus)
        Echo("                      DATE/TIME: " & DateTime.Now.ToString("dddd, dd-MMM-yyyy HH:mm"))
        Echo("*******************************************************************************")
    End Sub

    Public Sub PrintSerialNumberHeader()
        Dim currentTime As DateTime = DateTime.Now
        With TestData
            .sStart = currentTime.ToString()
            Echo("")
            Echo("*******************************************************************************")
            Echo(" TEST PROGRAM RESULTS FOR: " & UUTPartNo & "      S/N: " & .sUUTSerial)
            Echo(" INTERFACE DEVICE PN: " & IDPartNo & "   S/N: " & .sIDSerial)
            Echo(" DATE/TIME: " & currentTime.ToString("dddd, dd-MMM-yyyy HH:mm"))
            Echo("*******************************************************************************")
            Echo("")
        End With
    End Sub

    Public Function bTestIDInfo() As Boolean
        bTestIDInfo = True ' was bSetIDInfo
        'Check SN to see if new ID
        If IDSN <> TestData.sIDSerial Then
            bTestIDInfo = False ' was bSetIDInfo
            gFrmMain.lblEndToEndStatus.Text = "Unknown"
            gFrmMain.lblEndToEndStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
            gFrmMain.lblEndToEndStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            gFrmMain.lblSTTOStatus.Text = "Unknown"
            gFrmMain.lblSTTOStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
            gFrmMain.lblSTTOStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            gFrmMain.lblPwrOnStatus.Text = "Unknown"
            gFrmMain.lblPwrOnStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
            gFrmMain.lblPwrOnStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            If bModuleMenuBuilt = True Then
                gFrmMain.lblModuleStatus_1.Text = "Unknown"
                gFrmMain.lblModuleStatus_1.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
                gFrmMain.lblModuleStatus_1.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            End If
            gFrmMain.MenuOption(5).Image = gFrmMain.ImageList1.Images(0)
        End If
    End Function

    Public Sub ModulePassed(ByRef sModule As String)
        Dim i As Short ' bbbb

        ShowWindow(gFrmMain.SeqTextWindow)
        gFrmMain.AbortButton.Enabled = False
        gFrmMain.MainMenuButton.Enabled = True
        gFrmMain.PrintButton.Enabled = True
        gFrmMain.PrintButton.Visible = True
        gFrmMain.Quit.Enabled = True
        Echo("")
        Echo("")
        Echo("**** MODULE COMPLETE: " & sModule & " PASSED ****")
        Echo("     DATE/TIME: " & DateTime.Now.ToString("dddd, dd-MMM-yyyy HH:mm"))

        If Not bSimulation And Not bEndToEnd Then
            With TestData
                .SStop = CStr(Now)
                .sFailStep = "M0"
                .sCallout = ""
                .dMeasurement = 0
                .sUOM = ""
                .duLimit = 0
                .dlLimit = 0
                .sComment = "No operator comments"
                .nStatus = True
                i = FHDB.SaveData(.sStart, .SStop, .EROs, .sTPCCN, .sUUTSerial, .sUUTRev, .sIDSerial, .nStatus, .sFailStep, .sCallout, .dMeasurement, .sUOM, .duLimit, .dlLimit, .sComment)

            End With
        End If
    End Sub

    'Modified 4/15/03 Craig Weirich
    Public Sub SetTestInformation(ByRef sTestName As String, ByRef sTestNumber As String,
                                  ByRef nUpperLimit As Double, ByRef nLowerLimit As Double,
                                  ByRef sUnit As String, Optional ByRef sPrintFormat As String = "DEFAULT")

        gFrmMain.txtStep.Text = ""
        gFrmMain.txtUpperLimit.Text = ""
        gFrmMain.txtLowerLimit.Text = ""
        gFrmMain.txtUnit.Text = ""
        gFrmMain.txtInstrument.Text = ""
        gFrmMain.txtCommand.Text = ""
        gFrmMain.txtMeasured.Text = ""
        gFrmMain.txtMeasured.BackColor = System.Drawing.Color.White

        'Reset System Level Flag States
        Pass = False
        Failed = False
        OutHigh = False
        OutLow = False

        'Error check limits
        If nLowerLimit <> -1.0E-99 Then
            If nLowerLimit > nUpperLimit Then
                Echo("PROGRAMMING ERROR:  Lower Limit Argument is Greater than Upper Limit Argument")
                Err.Raise(-1010)
                Exit Sub
            End If
        End If

        gFrmMain.txtTestName.Text = sTestName
        gFrmMain.txtStep.Text = sTestNumber
        Select Case UCase(sUnit)
            Case "DTB"
                gFrmMain.txtUpperLimit.Text = "PASSED"
                gFrmMain.txtLowerLimit.Text = "PASSED"

            Case "PN"
                gFrmMain.txtUpperLimit.Text = UUTPartNo
                gFrmMain.txtLowerLimit.Text = UUTPartNo

            Case "QUESTION"
                If nUpperLimit = True Then
                    gFrmMain.txtUpperLimit.Text = "YES"
                    gFrmMain.txtLowerLimit.Text = "YES"
                Else
                    gFrmMain.txtUpperLimit.Text = "NO"
                    gFrmMain.txtLowerLimit.Text = "NO"
                End If

            Case "Y/N"
                If nUpperLimit = True Then
                    gFrmMain.txtUpperLimit.Text = "YES"
                    gFrmMain.txtLowerLimit.Text = "YES"
                Else
                    gFrmMain.txtUpperLimit.Text = "NO"
                    gFrmMain.txtLowerLimit.Text = "NO"
                End If


            Case "NA"
                gFrmMain.txtUpperLimit.Text = "NA"
                gFrmMain.txtLowerLimit.Text = "NA"

            Case Else
                Select Case UCase(sPrintFormat)
                    Case "DEFAULT"
                        gFrmMain.txtUpperLimit.Text = CStr(nUpperLimit)
                        gFrmMain.txtLowerLimit.Text = CStr(nLowerLimit)
                    Case "EXP"
                        gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("0.0###E+00")
                        gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("0.0###E+00")
                    Case "DEC"
                        gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("######.0##")
                        gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("######.0##")
                    Case "AUTODEC"
                        'Set Upper Limit Value for Display
                        If nUpperLimit < 0.0001 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("0.0###E+00")
                        ElseIf nUpperLimit < 0.001 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("0.000#####")
                        ElseIf nUpperLimit < 0.01 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("0.00######")
                        ElseIf nUpperLimit < 0.1 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("0.0#######")
                        ElseIf nUpperLimit < 1.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("0.0#######")
                        ElseIf nUpperLimit < 10.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("#.0#######")
                        ElseIf nUpperLimit < 100.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("##.0######")
                        ElseIf nUpperLimit < 1000.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("###.0#####")
                        ElseIf nUpperLimit < 10000.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("####.0####")
                        ElseIf nUpperLimit < 100000.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("#####.0###")
                        ElseIf nUpperLimit < 1000000.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("######.0##")
                        ElseIf nUpperLimit < 10000000.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("#######.0#")
                        ElseIf nUpperLimit < 100000000.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("########.0")
                        ElseIf nUpperLimit < 1000000000.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("#########")
                        ElseIf nUpperLimit < 10000000000.0 Then
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("##########")
                        Else
                            gFrmMain.txtUpperLimit.Text = nUpperLimit.ToString("0.0###E+00")
                        End If
                        'Set Lower Limit Value For Display
                        If nLowerLimit < 0.0001 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("0.0###E+00")
                        ElseIf nLowerLimit < 0.001 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("0.000#####")
                        ElseIf nLowerLimit < 0.01 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("0.00######")
                        ElseIf nLowerLimit < 0.1 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("0.0#######")
                        ElseIf nLowerLimit < 1.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("0.0#######")
                        ElseIf nLowerLimit < 10.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("#.0#######")
                        ElseIf nLowerLimit < 100.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("##.0######")
                        ElseIf nLowerLimit < 1000.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("###.0#####")
                        ElseIf nLowerLimit < 10000.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("####.0####")
                        ElseIf nLowerLimit < 100000.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("#####.0###")
                        ElseIf nLowerLimit < 1000000.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("######.0##")
                        ElseIf nLowerLimit < 10000000.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("#######.0#")
                        ElseIf nLowerLimit < 100000000.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("########.0")
                        ElseIf nLowerLimit < 1000000000.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("#########")
                        ElseIf nLowerLimit < 10000000000.0 Then
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("##########")
                        Else
                            gFrmMain.txtLowerLimit.Text = nUpperLimit.ToString("0.0###E+00")
                        End If
                End Select
        End Select
        gFrmMain.txtUnit.Text = sUnit
    End Sub

    Public Sub ClearTestInformation()
        ClearTestMeasurementInformation()
    End Sub

    Public Sub ClearMeasInfo()
        gFrmMain.txtStep.Text = ""
        gFrmMain.txtUpperLimit.Text = ""
        gFrmMain.txtLowerLimit.Text = ""
        gFrmMain.txtUnit.Text = ""
        gFrmMain.txtInstrument.Text = ""
        gFrmMain.txtCommand.Text = ""
        gFrmMain.txtMeasured.Text = ""
        gFrmMain.txtMeasured.BackColor = System.Drawing.Color.White
    End Sub

    Public Sub SetMeasuredValue(ByRef sValue As String)
        gFrmMain.txtMeasured.Text = sValue
    End Sub

    Public Sub DisplayProbeMessage(ByRef sLocation As String, ByRef sProbeInstructions() As String, Optional ByRef bContinueOnly As Boolean = False)
        Dim i As Short
        Dim iCount As Short
        Dim nDatafile As Integer
        Dim sProbePoint As String
        Dim sDelimiter As String
        Dim sProbe(5) As String
        Dim nNumOfElements As Integer
        Dim dFX1 As Double
        Dim dFY1 As Double
        Dim dFX2 As Double
        Dim dFY2 As Double
        Dim dPX1 As Double
        Dim dPY1 As Double
        Dim dPX2 As Double
        Dim dPY2 As Double


        gFrmMain.lblPressContinue.Text = "PRESS CONTINUE AND/OR PROBE BUTTON"
        If bContinueOnly Then
            gFrmMain.lblPressContinue.Text = "PRESS CONTINUE ONLY"
            gFrmMain.lblPressContinue.ForeColor = System.Drawing.Color.Black
        End If
        gFrmMain.cmdContinue.Enabled = True
        gFrmMain.AbortButton.Enabled = True
        gFrmMain.cmdFHDB.Visible = False
        gFrmMain.cmdDiagnostics.Visible = False

        If sProbeData = " " Then
            MsgBox("Probe data file not specified.  Set the ProbeDataFile variable", 16, "Shell Error")
            Exit Sub
        End If

        nDatafile = FreeFile
        FileOpen(nDatafile, ProgramPath & sProbeData, OpenMode.Input)
        Do
            sProbePoint = LineInput(nDatafile)
            If InStr(1, sProbePoint, sLocation & ",") > 0 Then
                Exit Do
            Else
                sProbePoint = ""
            End If
        Loop Until EOF(nDatafile)
        FileClose(nDatafile)
        If sProbePoint = "" Then
            MsgBox("Error locating probe information in " & sProbeData & " file.", 16, "File Error")
            Exit Sub
        End If

        If UBound(sProbeInstructions) > 6 Then
            MsgBox("To many lines of instructions passed to DisplayProbeMessage Subroutine.  " & vbCrLf & "Only 6 line of instructions are allowed.", 16, "Shell Error")
            Exit Sub
        End If

        For iCount = 1 To UBound(sProbeInstructions)
            gFrmMain.lblInstruction(iCount).Text = sProbeInstructions(iCount)
        Next iCount
        If iCount < 6 Then
            For i = iCount To 6
                gFrmMain.lblInstruction(i).Text = ""
            Next i
        End If

        'Turn off MainMenu and Test Window
        gFrmMain.MainMenu.Visible = False
        gFrmMain.SeqTextWindow.Visible = False
        'Turn on Picture window
        gFrmMain.PictureWindow.Visible = True
        gFrmMain.pinp.Visible = True
        gFrmMain.fraInstructions.Visible = True

        sDelimiter = ","

        nNumOfElements = StringToList(sProbePoint, sProbe, sDelimiter)
        sProbe(4) = CStr(Val(CStr(CDbl(sProbe(4)) + 180)))
        gFrmMain.txtMeasured.Text = ""

        gFrmMain.PictureWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        gFrmMain.PictureWindow.BackgroundImage = Nothing 'LoadPicture is a Visual Basic Function
        gFrmMain.pinp.Image = System.Drawing.Image.FromFile(TPSGraphics & sProbeAssy)
        gFrmMain.pinp.Top = (gFrmMain.PictureWindow.Height / 2) - (gFrmMain.pinp.Height / 2)
        gFrmMain.pinp.Left = (gFrmMain.PictureWindow.Width / 2) - (gFrmMain.pinp.Width / 2)
        gFrmMain.pinp.Visible = True

        gFrmMain.Message.Text = sProbe(1)
        gFrmMain.Message.SetBounds(gFrmMain.pinp.Left + Val(sProbe(4)), gFrmMain.pinp.Top + Val(sProbe(5)), 0, 0, Windows.Forms.BoundsSpecified.X Or Windows.Forms.BoundsSpecified.Y)
        gFrmMain.Message.Visible = True
        gFrmMain.PictureWindow.Visible = True

        dFX1 = gFrmMain.Message.Left + (gFrmMain.Message.Width / 2)
        dFY1 = gFrmMain.Message.Top + (gFrmMain.Message.Height / 2)
        dFX2 = Val(sProbe(2)) + gFrmMain.pinp.Left
        dFY2 = Val(sProbe(3)) + gFrmMain.pinp.Top

        dPX1 = -gFrmMain.pinp.Left + gFrmMain.Message.Left + (gFrmMain.Message.Width / 2)
        dPY1 = -gFrmMain.pinp.Top + gFrmMain.Message.Top + (gFrmMain.Message.Height / 2)
        dPX2 = Val(sProbe(2))
        dPY2 = Val(sProbe(3))

        gFrmMain.PictureWindow_Line.X1 = dFX1
        gFrmMain.PictureWindow_Line.Y1 = dFY1
        gFrmMain.PictureWindow_Line.X2 = dFX2
        gFrmMain.PictureWindow_Line.Y2 = dFY2
        gFrmMain.PictureWindow_Line.BorderColor = Color.Red
        gFrmMain.PictureWindow_Line.Show()

        gFrmMain.pinp_Line.X1 = dPX1
        gFrmMain.pinp_Line.Y1 = dPY1
        gFrmMain.pinp_Line.X2 = dPX2
        gFrmMain.pinp_Line.Y2 = dPY2
        gFrmMain.pinp_Line.BorderColor = Color.Red
        gFrmMain.pinp_Line.Show()
        Delay(0.2)
    End Sub

    Public Sub PowerApplied(ByRef bState As Boolean)
        If bState Then
            gFrmMain.lblPowerApplied.Visible = True
            If bHighVoltage Then
                gFrmMain.picDanger.Visible = True
            End If
        Else
            gFrmMain.lblPowerApplied.Visible = False
            gFrmMain.picDanger.Visible = False
        End If
    End Sub

    Public Function bPass() As Boolean
        bPass = Pass
    End Function

    Public Function bFailed() As Boolean
        bFailed = Failed
    End Function

    Public Function bOutHigh() As Boolean
        bOutHigh = OutHigh
    End Function

    Public Function bOutLow() As Boolean
        bOutLow = OutLow
    End Function


    ''' <summary>
    ''' This function is used to test a LED device
    ''' </summary>
    ''' <param name="sLEDfile">name of bitmap picture showing the LED</param>
    ''' <param name="sLEDname">name if LED device as shown on schematic</param>
    ''' <param name="sLEDstatus">(OPTIONAL)  "ON" checks LED ON (default), "OFF" checks LED OFF</param>
    ''' <returns>True if the LED is in the sLEDstatus, otherwise False</returns>
    ''' <remarks></remarks>
    Public Function DisplayLEDMessage(ByRef sLEDfile As String, ByRef sLEDname As String, Optional ByRef sLEDstatus As String = "ON") As Boolean
        'declare string variables
        Dim sMsgON As String
        Dim sMsgOFF As String

        DisplayLEDMessage = False

        'convert to upper case
        sLEDstatus = UCase(sLEDstatus)

        'assign display message
        sMsgON = "Visually verify that " & sLEDname & " is ON:" & vbCrLf & "   Click YES if " & sLEDname & " is ON" & vbCrLf & "   Click NO if " & sLEDname & " is OFF"
        sMsgOFF = "Visually verify that " & sLEDname & " is OFF:" & vbCrLf & "   Click YES if " & sLEDname & " is OFF" & vbCrLf & "   Click NO if " & sLEDname & " is ON"

        'load LED picture into LED frame
        gFrmDisplayLED.PictureWindow.Image = System.Drawing.Image.FromFile(ProgramPath & "\graphics\Form Images\" & sLEDfile)

        Select Case sLEDstatus
            Case "ON" 'checking LED status "ON"
                'display LED message in frame testbox
                gFrmDisplayLED.cmdTextBox.Text = sMsgON
                gFrmDisplayLED.ShowDialog() 'wait for user event

                'Result
                If LEDUserResponse = True Then
                    gFrmDisplayLED.Hide()
                    If LEDButtonPressed <> "YES" Then
                        'Echo "LED FAIL" 'debug statement
                        DisplayLEDMessage = False
                    Else
                        'Echo "LED PASS" 'debug statement
                        DisplayLEDMessage = True
                    End If
                End If

            Case "OFF" 'checking LED status "OFF"
                'display LED message in frame testbox
                gFrmDisplayLED.cmdTextBox.Text = sMsgOFF
                gFrmDisplayLED.ShowDialog() 'wait for user event

                'Result
                If LEDUserResponse = True Then
                    gFrmDisplayLED.Hide()
                    If LEDButtonPressed <> "YES" Then
                        'Echo "LED FAIL" 'debug statement
                        DisplayLEDMessage = False
                    Else
                        'Echo "LED PASS" 'debug statement
                        DisplayLEDMessage = True
                    End If
                End If

            Case Else 'invalid argument
                Echo("INVALID ARGUMENT")
                DisplayLEDMessage = False
                Exit Function
        End Select
    End Function

    Public Function bYesNoUserMsgImage(ByRef sTitle As String, ByRef iTextJustification As Short, ByRef sLinesOfText() As String, ByRef sImage As String) As Boolean
        Dim i As Short
        Dim nNumberoflines As Integer
        Dim sTemp As String
        Dim bCurrentState As Boolean

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            bYesNoUserMsgImage = False
            Exit Function
        End If
        gFrmMain.txtInstrument.Text = "OPERATOR"
        gFrmMain.txtCommand.Text = "bYesNoUserMsgImage"

        sTemp = gFrmMain.lblStatus.Text
        bCurrentState = gFrmMain.Timer2.Enabled

        gFrmMain.Timer1.Enabled = True
        gFrmMain.Timer2.Enabled = False
        gFrmMain.lblStatus.Text = "Waiting for User ..."

        UserEvent = 0

        nNumberoflines = UBound(sLinesOfText)
        gFrmMain.MainMenuButton.Enabled = True
        gFrmMain.AbortButton.Enabled = False

        'Disable controls
        gFrmMain.Quit.Enabled = False
        gFrmMain.MainMenuButton.Enabled = False
        gFrmMain.PrintButton.Enabled = False
        gFrmMain.AbortButton.Visible = False
        gFrmMain.cmdContinue.Visible = False
        gFrmMain.RerunButton.Visible = False

        'Enable Yes/No Buttons
        gFrmMain.YesButton.Enabled = True
        gFrmMain.YesButton.Visible = True
        gFrmMain.NoButton.Enabled = True
        gFrmMain.NoButton.Visible = True

        gFrmMain.SSPanel1.Top = 8
        gFrmMain.SSPanel1.Left = 8
        gFrmMain.SSPanel1.Height = 160
        gFrmMain.SSPanel1.Width = gFrmMain.Width - 12
        gFrmMain.msgInBox.Top = 0
        gFrmMain.msgInBox.Left = 0
        gFrmMain.lblMsg_1.Left = 0
        gFrmMain.SSPanel1.BringToFront()

        gFrmMain.SSPanel2.Top = gFrmMain.SSPanel1.Top + gFrmMain.SSPanel1.Height
        gFrmMain.SSPanel2.Left = 8
        gFrmMain.SSPanel2.Height = gFrmMain.fraProgramNavigation.Top - 8 - gFrmMain.SSPanel1.Height - 8
        gFrmMain.SSPanel2.Width = gFrmMain.SSPanel1.Width
        gFrmMain.picGraphic.Top = 0
        gFrmMain.picGraphic.Left = 0
        gFrmMain.picGraphic.Height = gFrmMain.SSPanel2.Height
        gFrmMain.picGraphic.Width = gFrmMain.SSPanel2.Width - 16
        gFrmMain.SSPanel2.BringToFront()

        gFrmMain.msgInBox.Width = gFrmMain.SSPanel1.Width - 16
        gFrmMain.lblMsg_1.Width = gFrmMain.msgInBox.Width - 20

        gFrmMain.msgInBox.Height = gFrmMain.SSPanel1.Height - 15
        gFrmMain.msgInBox.AutoScrollMinSize = New Size(gFrmMain.msgInBox.Width - 40, gFrmMain.msgInBox.Height - 7)
        gFrmMain.lblMsg_1.TextAlign = iTextJustification
        gFrmMain.lblMsg_1.Text = sLinesOfText(1)
        gFrmMain.lblMsg_1.Visible = True

        Dim lbl As Label
        For i = 2 To nNumberoflines
            lbl = gFrmMain.lblMsg(i)
            lbl.Top = gFrmMain.lblMsg(i - 1).Top + 16
            lbl.Text = sLinesOfText(i)
            lbl.Visible = True
        Next i

        gFrmMain.picGraphic.Image = System.Drawing.Image.FromFile(TPSGraphics & sImage)
        gFrmMain.SSPanel1.Visible = True
        gFrmMain.SSPanel2.Visible = True

        Do While UserEvent = 0
            System.Windows.Forms.Application.DoEvents()
            Delay(0.3)
        Loop
        gFrmMain.Refresh()

        If UserEvent = YES_BUTTON Then
            bYesNoUserMsgImage = True
            gFrmMain.txtMeasured.Text = "YES"
        Else
            bYesNoUserMsgImage = False
            gFrmMain.txtMeasured.Text = "NO"
        End If

        'disable Yes/No Buttons
        gFrmMain.YesButton.Enabled = False
        gFrmMain.YesButton.Visible = False
        gFrmMain.NoButton.Enabled = False
        gFrmMain.NoButton.Visible = False
        'Show Abort and Continue button
        gFrmMain.cmdContinue.Visible = True
        gFrmMain.AbortButton.Visible = True

        For i = 2 To nNumberoflines
            gFrmMain.UnloadLabel(gFrmMain.lblMsg(i))
        Next i

        gFrmMain.SSPanel1.Visible = False
        gFrmMain.SSPanel2.Visible = False
        gFrmMain.lblStatus.Text = sTemp
        gFrmMain.Timer2.Enabled = bCurrentState

        gFrmMain.Timer1.Enabled = False
    End Function
	
    Public Sub nEnableDiagnostics()
        gFrmMain.cmdDiagnostics.Visible = True
    End Sub
	
	Public Function sGetCurrentUUTserno() As String
		sGetCurrentUUTserno = TestData.sUUTSerial
	End Function
	
	Public Sub DisableNewUUT()
        gFrmMain.NewUUT.Visible = False
        gFrmMain.NewUUT.Enabled = False
	End Sub
	
	Public Sub EnableNewUUT()
        gFrmMain.NewUUT.Visible = True
        gFrmMain.NewUUT.Enabled = True
	End Sub
	
	Public Sub nDisableMainMenu()
        gFrmMain.MainMenu.Enabled = False
	End Sub
End Class