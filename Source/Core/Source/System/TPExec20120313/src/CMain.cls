VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

Public Sub SetProbeData(sProbeAssyFileName As String, sProbeDataFile As String)
    sProbeAssy = sProbeAssyFileName
    sProbeData = sProbeDataFile
End Sub

Public Function nBeginTest(TestProgramName As String, sTCCN As String, TestProgramVersion As String, _
                            UUTPartNumber As String, boardName As String, IDName As String, _
                            IDPartNumber As String, TPPath As String, iMaxPartsListPages As Integer, _
                            iMaxIDPartsListPages As Integer, iMaxSchemPages As Integer, iMaxIDSchemPages As Integer, _
                            iMaxAssyPages As Integer, iMaxIDAssyPages As Integer, sProbeAssyFileName As String, _
                            sProbeDataFile As String, nEndToEndTime As Long, nSafeToTurnOnTime As Long, _
                            nPowerOnTime As Long, bDevelopment As Boolean) As Long
    
    'Establish link with Test Program
    'Define Test Program Name, Version Number, UUT Part Number, Board Name and Path
    Dim sEndToEndTime As String
    Dim sSafeToTurnOnTime As String
    Dim sPowerOnTime As String
    Dim sCDROM As String

    TPName$ = TestProgramName
    sTPCCN = sTCCN
    TPVersion$ = TestProgramVersion
    UUTPartNo$ = UUTPartNumber
    IDPartNo$ = IDPartNumber
    UUTName$ = boardName
    ProgramPath$ = TPPath
    
    TPSGraphics$ = TPPath + "graphics\Form Images\"
    MaxSchematicPages% = iMaxSchemPages
    MaxIDSchematicPages% = iMaxIDSchemPages
    
    MaxPartsListPages% = iMaxPartsListPages
    MaxIDPartsListPages% = iMaxIDPartsListPages
    
    MaxAssemblyPages% = iMaxAssyPages
    MaxIDAssemblyPages% = iMaxIDAssyPages
    
    DocumentsPath = TPPath + "graphics\doc\"
    sProbeAssy = sProbeAssyFileName
    sProbeData = sProbeDataFile
    bSimulation = False
    bTPSDevelopment = bDevelopment
    bFirstRun = True
    bSystemResetComplete = True
    
    If nEndToEndTime > 60 Then
        frmMain.lblEndToEndRunTime.Caption = Format(nEndToEndTime / 60, "##0.00") & " Min."
    Else
        frmMain.lblEndToEndRunTime.Caption = nEndToEndTime & " Sec."
    End If
    
    If nSafeToTurnOnTime > 60 Then
        frmMain.lblSTTORunTime.Caption = Format(nSafeToTurnOnTime / 60, "##0.00") & " Min."
    Else
        frmMain.lblSTTORunTime.Caption = nSafeToTurnOnTime & " Sec."
    End If
    
    If nPowerOnTime > 60 Then
        frmMain.lblPowerOnRunTime.Caption = Format(nPowerOnTime / 60, "##0.00") & " Min."
    Else
        frmMain.lblPowerOnRunTime.Caption = nPowerOnTime & " Sec."
    End If
    
    sCDROM = GetCompactDiscDrives()
    nFileError = GetPrivateProfileString("IETM", "FILE_NAME", "NONE", lpBuffer$, 256, sCDROM & "VIPERT_TPS.INI")
    If UCase(Left(lpBuffer, 4)) = "NONE" Then
        frmMain.MenuOption(-VIEW_IETM).Enabled = False
        frmMain.MenuOptionText(-VIEW_IETM).Enabled = False
    Else
        frmMain.MenuOption(-VIEW_IETM).Enabled = True
        frmMain.MenuOptionText(-VIEW_IETM).Enabled = True
    End If
    IETMPath = sCDROM + Left(lpBuffer, nFileError)
    
    nBeginTest = 0

End Function

Public Sub SetUUTData(Optional sTypeOfTest As String = "UUT")
    Dim sType As String
    Dim sInstructions(1 To 2) As String
    Dim sTempSN As String
    Dim iCount As Integer
    
    If UCase(Left(sTypeOfTest, 1)) = "U" Then sType = "UUT" Else sType = "INTERFACE"
        
    sTempSN = ""
    sInstructions(1) = "INPUT " & sType & " SERIAL NUMBER"
    sInstructions(2) = "(24 CHARACTERS MAXIMUM)"
    Do While sTempSN = ""
        sTempSN = UCase(sUserInput("** Operator Input **", vbCenter, sInstructions))
    Loop
    If sTempSN <> sUUTSerial Then
        sUUTSerial = sTempSN
        frmMain.lblEndToEndStatus(0).Caption = "Unknown"
        frmMain.lblEndToEndStatus(0).BackColor = &HFFFFC0
        frmMain.lblEndToEndStatus(0).ForeColor = &HC00000
            
        frmMain.lblSTTOStatus(1).Caption = "Unknown"
        frmMain.lblSTTOStatus(1).BackColor = &HFFFFC0
        frmMain.lblSTTOStatus(1).ForeColor = &HC00000
            
        frmMain.lblPwrOnStatus(0).Caption = "Unknown"
        frmMain.lblPwrOnStatus(0).BackColor = &HFFFFC0
        frmMain.lblPwrOnStatus(0).ForeColor = &HC00000
        frmMain.lblPwrOnStatus(0).Enabled = False
        frmMain.lblPwrOnStatus(0).Enabled = False
        frmMain.lblPwrOnStatus(0).Enabled = False
            
        If bModuleMenuBuilt = True Then
            For iCount = 1 To MaxModules
                frmMain.lblModuleStatus(iCount).Caption = "Unknown"
                frmMain.lblModuleStatus(iCount).BackColor = &HFFFFC0
                frmMain.lblModuleStatus(iCount).ForeColor = &HC00000
                frmMain.lblModuleStatus(iCount).Enabled = False
                frmMain.lblModuleStatus(iCount).Enabled = False
                frmMain.lblModuleStatus(iCount).Enabled = False
            Next iCount
        End If
    Else
        bFirstRun = False
        Exit Sub
    End If
        
    'Set UUT Revision
    If sType = "UUT" Then
        sUUTRev = ""
        sInstructions(1) = "INPUT UUT REVISION"
        sInstructions(2) = "(24 CHARACTERS MAXIMUM)"
        Do While sUUTRev = ""
            sUUTRev = UCase(sUserInput("** Operator Input **", vbCenter, sInstructions))
        Loop
    Else
        sUUTRev = "N/A"
    End If
        
    'Set ERO number
    EROs = ""
    sInstructions(1) = "INPUT EQUIPMENT REPAIR ORDER NUMBER"
    sInstructions(2) = "(5 CHARACTERS MAXIMUM)"
    Do While EROs = ""
        EROs = UCase(sUserInput("** Operator Input **", vbCenter, sInstructions))
    Loop
            
End Sub

Public Function bSetIDData() As Boolean
    Dim sInstructions(1 To 2) As String
    Dim sTempSN As String
    
    bSetIDData = True
    sTempSN = ""
    sInstructions(1) = "INPUT INTERFACE SERIAL NUMBER"
    sInstructions(2) = "(24 CHARACTERS MAXIMUM)"
    Do While sTempSN = ""
        sTempSN = UCase(sUserInput("** Operator Input **", vbCenter, sInstructions))
    Loop
    
    'Check SN to see if new ID
    If sTempSN <> sIDSerial Then
        bSetIDData = False
        sIDSerial = sTempSN
        frmMain.MenuOption(5).Picture = frmMain.ImageList1.ListImages(1).Picture
    End If

End Function

Public Function nSetPartsList(sPartsList() As String) As Long

    Dim i
    MaxPartsListPages% = UBound(sPartsList)
    ReDim sPartsListFileNames(1 To MaxPartsListPages)
    sPartsListFileNames = sPartsList
    If sPartsList(1) = "NONE" Then
        frmMain.MenuOption(VIEW_PARTSLIST * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_PARTSLIST * -1).ForeColor = &H808080
        frmMain.cboPartsList.Visible = False
        Exit Function
    Else
        frmMain.MenuOption(VIEW_PARTSLIST * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_PARTSLIST * -1).ForeColor = vbBlack
        frmMain.cboPartsList.Visible = True
    End If
    
   'Empty ComboBox
    frmMain.cboPartsList.Clear
        
    'Fill With Specified Number of array elements necessary to Instrument Function
    For i = UBound(sPartsListFileNames) To 1 Step -1
        frmMain.cboPartsList.AddItem sPartsListFileNames(i), 0
    Next i
    'Select First Chioce
    frmMain.cboPartsList.ListIndex = 0

    PartListPageNum% = 1
    If Right$(ProgramPath$, 1) <> "\" Then
      PartListPath$ = ProgramPath$ + "\graphics\Pl\"
    Else
      PartListPath$ = ProgramPath$ + "graphics\Pl\"
    End If
    nSetPartsList = 0

End Function

Public Function nSetIDPartsList(sIDPartsList() As String) As Long

  Dim i As Integer
  Dim sCDROM As String

    MaxIDPartsListPages% = UBound(sIDPartsList)
    ReDim sIDPartsListFileNames(1 To MaxIDPartsListPages)
    sIDPartsListFileNames = sIDPartsList
    If sIDPartsList(1) = "NONE" Then
        frmMain.MenuOption(VIEW_ID_PARTSLIST * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_ID_PARTSLIST * -1).ForeColor = &H808080
        frmMain.cboITAPartsList.Visible = False
        Exit Function
    Else
        frmMain.MenuOption(VIEW_ID_PARTSLIST * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_ID_PARTSLIST * -1).ForeColor = vbBlack
        frmMain.cboITAPartsList.Visible = False
    End If
    
    frmMain.cboITAPartsList.Clear
        
    For i = UBound(sIDPartsListFileNames) To 1 Step -1
        frmMain.cboITAPartsList.AddItem sIDPartsListFileNames(i), 0
    Next i
    'Select First Chioce
    frmMain.cboITAPartsList.ListIndex = 0
    
    IDPartListPageNum% = 1
    sCDROM = GetCompactDiscDrives()
    nFileError = GetPrivateProfileString(IDPartNo$, "ID_PL", "E:\", lpBuffer$, 256, sCDROM & "VIPERT_TPS.INI")
    IDPartListPath$ = sCDROM + Left(lpBuffer, nFileError)

    nSetIDPartsList = 0

End Function

Public Function nSetModuleList(sModuleNames() As String, nModuleRunTime() As Long) As Long
    
    MaxModules = UBound(sModuleNames)
    ReDim ModuleNames(1 To MaxModules)
    ModuleNames = sModuleNames
    ReDim ModuleRunTime(1 To MaxModules)
    ModuleRunTime = nModuleRunTime
    
    nSetModuleList = 0

End Function

Public Function nSetAssyList(sAssyList() As String) As Long

    Dim i As Integer

    MaxAssemblyPages% = UBound(sAssyList)
    ReDim sAssyFileNames(1 To MaxAssemblyPages%)
    sAssyFileNames = sAssyList
    If sAssyList(1) = "NONE" Then
        frmMain.MenuOption(VIEW_ASSEMBLY * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_ASSEMBLY * -1).ForeColor = &H808080
        frmMain.cboAssembly.Visible = False
        Exit Function
    Else
        frmMain.MenuOption(VIEW_ASSEMBLY * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_ASSEMBLY * -1).ForeColor = vbBlack
        frmMain.cboAssembly.Visible = True
    End If
   'Empty ComboBox
    frmMain.cboAssembly.Clear
        
    'Fill With Specified Number of array elements necessary to Instrument Function
    For i = UBound(sAssyFileNames) To 1 Step -1
        frmMain.cboAssembly.AddItem sAssyFileNames(i), 0
    Next i
    'Select First Chioce
    frmMain.cboAssembly.ListIndex = 0

    AssyPageNum% = 1
    If Right$(ProgramPath$, 1) <> "\" Then
      AssyPath$ = ProgramPath$ + "\graphics\Assy\"
    Else
      AssyPath$ = ProgramPath$ + "graphics\Assy\"
    End If
    nSetAssyList = 0
    
End Function

Public Function nSetIDAssyList(sIDAssyList() As String) As Long

  Dim i As Integer
  Dim sCDROM As String

    MaxIDAssemblyPages% = UBound(sIDAssyList)
    ReDim sIDAssyFileNames(1 To MaxIDAssemblyPages%)
    sIDAssyFileNames = sIDAssyList
    If sIDAssyList(1) = "NONE" Then
        frmMain.MenuOption(VIEW_ID_ASSEMBLY * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_ID_ASSEMBLY * -1).ForeColor = &H808080
        frmMain.cboITAAssy.Visible = False
        Exit Function
    Else
        frmMain.MenuOption(VIEW_ID_ASSEMBLY * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_ID_ASSEMBLY * -1).ForeColor = vbBlack
        frmMain.cboITAAssy.Visible = True
    End If
    
    frmMain.cboITAAssy.Clear
        
    For i = UBound(sIDAssyFileNames) To 1 Step -1
        frmMain.cboITAAssy.AddItem sIDAssyFileNames(i), 0
    Next i
    'Select First Chioce
    frmMain.cboITAAssy.ListIndex = 0
    
    IDAssyPageNum% = 1
    sCDROM = GetCompactDiscDrives()
    nFileError = GetPrivateProfileString(IDPartNo$, "ID_ASSY", "E:\", lpBuffer$, 256, sCDROM & "VIPERT_TPS.INI")
    IDAssyPath$ = sCDROM + Left(lpBuffer, nFileError)

    nSetIDAssyList = 0
    
End Function

Public Function nSetDocuments(sTSR As String, sELTD As String, sInfo As String) As Long
    
    sTSRFileName = sTSR
    If UCase(sTSR) = "NONE" Then
        frmMain.MenuOption(VIEW_TSR * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_TSR * -1).ForeColor = &H808080
    Else
        frmMain.MenuOption(VIEW_TSR * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_TSR * -1).ForeColor = vbBlack
    End If
    
    sELTDFileName = sELTD
    If UCase(sELTD) = "NONE" Then
        frmMain.MenuOption(VIEW_ELTD * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_ELTD * -1).ForeColor = &H808080
    Else
        frmMain.MenuOption(VIEW_ELTD * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_ELTD * -1).ForeColor = vbBlack
    End If
    
    sInfoFileName = sInfo
    If UCase(sInfo) = "NONE" Then
        frmMain.MenuOption(VIEW_GENERAL_INFO * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_GENERAL_INFO * -1).ForeColor = &H808080
    Else
        frmMain.MenuOption(VIEW_GENERAL_INFO * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_GENERAL_INFO * -1).ForeColor = vbBlack
    End If
    
    If Right$(ProgramPath, 1) <> "\" Then
      DocumentsPath = ProgramPath$ + "\graphics\doc\"
    Else
      DocumentsPath = ProgramPath$ + "graphics\doc\"
    End If
    
    nSetDocuments = 0
    
End Function

Public Function nSetSchematic(sSchemList() As String) As Long

    Dim i
    
    MaxSchematicPages% = UBound(sSchemList)
    ReDim sSchemFileNames(1 To MaxSchematicPages%)
    sSchemFileNames = sSchemList
    If sSchemList(1) = "NONE" Then
        frmMain.MenuOption(VIEW_SCHEMATIC * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_SCHEMATIC * -1).ForeColor = &H808080
        frmMain.cboSchematic.Visible = False
        Exit Function
    Else
        frmMain.MenuOption(VIEW_SCHEMATIC * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_SCHEMATIC * -1).ForeColor = vbBlack
        frmMain.cboSchematic.Visible = True
    End If
    
    'Empty ComboBox
    frmMain.cboSchematic.Clear
        
    'Fill With Specified Number of array elements necessary to Instrument Function
    For i = UBound(sSchemFileNames) To 1 Step -1
        frmMain.cboSchematic.AddItem sSchemFileNames(i), 0
    Next i
    'Select First Chioce
    frmMain.cboSchematic.ListIndex = 0
    
    SchemPageNum% = 1
    If Right(ProgramPath, 1) <> "\" Then
      SchemPath$ = ProgramPath$ + "\graphics\schem\"
    Else
      SchemPath$ = ProgramPath$ + "graphics\schem\"
    End If
    
    nSetSchematic = 0
    
End Function

Public Function nSetIDSchematic(sIDSchemList() As String) As Long

    Dim i As Integer
    Dim slpBuffer As String * 256
    Dim nFileError As Long
    Dim sReadBuffer As String
    Dim sCDROM As String

    MaxIDSchematicPages% = UBound(sIDSchemList)
    ReDim sIDSchemFileNames(1 To MaxIDSchematicPages%)
    sIDSchemFileNames = sIDSchemList
    If sIDSchemList(1) = "NONE" Then
        frmMain.MenuOption(VIEW_ID_SCHEMATIC * -1).Enabled = False
        frmMain.MenuOptionText(VIEW_ID_SCHEMATIC * -1).ForeColor = &H808080
        frmMain.cboITAWiring.Visible = False
        Exit Function
    Else
        frmMain.MenuOption(VIEW_ID_SCHEMATIC * -1).Enabled = True
        frmMain.MenuOptionText(VIEW_ID_SCHEMATIC * -1).ForeColor = vbBlack
        frmMain.cboITAWiring.Visible = True
    End If
    
    'Empty ComboBox
    frmMain.cboITAWiring.Clear
        
    'Fill With Specified Number of array elements necessary to Instrument Function
    For i = UBound(sIDSchemFileNames) To 1 Step -1
        frmMain.cboITAWiring.AddItem sIDSchemFileNames(i), 0
    Next i
    'Select First Chioce
    frmMain.cboITAWiring.ListIndex = 0
    
    IDSchemPageNum% = 1
    sCDROM = GetCompactDiscDrives()
    nFileError = GetPrivateProfileString(IDPartNo$, "ID_SCHEM", "E:\", lpBuffer$, 256, sCDROM & "VIPERT_TPS.INI")
    IDSchemPath$ = sCDROM + Left(lpBuffer, nFileError)
    
    nSetIDSchematic = 0
    
End Function

Public Function nShowMainMenu() As Long
    
    MAINMod.ShowMainMenu
    nShowMainMenu = 0
    
End Function

Public Function nWaitForUserResponse() As Long

    Dim iCount As Integer
    Dim sTemp As String
    
    sTemp = frmMain.lblStatus.Caption
    frmMain.Timer1.Enabled = True
    frmMain.Timer2.Enabled = False
    frmMain.lblStatus.Caption = "Waiting for User"

    UserEvent% = 0
    Do While UserEvent = 0
        'added 5/23/03 - fix 100% CPU usage during idle time
        Sleep 1
        DoEvents
        If UserEvent% <> 0 Then
            Exit Do
        End If
        If FormMainLoaded = False Then 'bb added 2012-02-16 to prevent hangup upon exit
          Exit Do
        End If
    Loop
    nWaitForUserResponse = UserEvent%
    frmMain.Timer1.Enabled = False
    frmMain.lblStatus.Caption = sTemp

End Function

Public Function nTerminateShell() As Long

    Unload frmMain
    nTerminateShell = 0
    
End Function

Public Function nEcho(sText As String) As Long

    Echo sText
    nEcho = 0
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    
End Function

Public Function nChangeTextColor(nColor As Long) As Long

    frmMain.SeqTextWindow.ForeColor = nColor
    nChangeTextColor = 0
    
End Function

Public Function nEnableAbort() As Long

    EnableAbort
    nEnableAbort = 0
    
End Function

Public Function nLongDelay(fTime As Single) As Long

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    LongDelay fTime
    nLongDelay = 0
    
End Function

Public Function nDisablePrint() As Long

    DisablePrint
    nDisablePrint = 0
    
End Function

Public Function nEnableContinue() As Long

    EnableContinue
    nEnableContinue = 0
    
End Function

Public Function nEnableQuit() As Long

    EnableQuit
    nEnableQuit = 0
    
End Function

Public Function nDisableQuit() As Long

    DisableQuit
    nDisableQuit = 0
    
End Function

Public Function nUpdateProgress(fPercentComplete As Single) As Long

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    UpdateProgress (fPercentComplete)
    nUpdateProgress = 0
    
End Function

Public Function nShowTestWindow() As Long

    frmMain.fraInstructions.Visible = False
    frmMain.fraTestInformation.Visible = True
    frmMain.fraMeasurement.Visible = True
    frmMain.fraInstrument.Visible = True
    
    ShowWindow frmMain.SeqTextWindow
    frmMain.AbortButton.Enabled = True
    frmMain.MainMenuButton.Enabled = False
    
    nShowTestWindow = 0
    
End Function

Public Function nDisableContinue() As Long

    DisableContinue
    nDisableContinue = 0
    
End Function

Public Function nDisableRerun() As Long

    DisableRerun
    nDisableRerun = 0
    
End Function

Public Function nEnablePrint() As Long

    EnablePrint
    nEnablePrint = 0
    
End Function

Public Function nEnableReRun() As Long

    EnableRerun
    nEnableReRun = 0
    
End Function

Public Function nMsgBox(sPrompt As String, iTypeButton As Integer, sTitle As String) As Long

        nMsgBox = MsgBox(sPrompt, iTypeButton, sTitle)
        
End Function

Public Function nShowSplashScreen(sImgLogo As String, sWeaponSystem As String, sWeaponSystemNomenclature As String, sDevelopedBy As String, sAddress As String) As Long

    CenterForm frmSplash
    Set frmSplash.imgLogo.Picture = LoadPicture(TPSGraphics$ + sImgLogo)
    WeaponSystem = sWeaponSystem
    WeaponSystemNomenclature = sWeaponSystemNomenclature
    DevelopedBy = sDevelopedBy
    address = sAddress
    MAINMod.ShowSplashScreen
    nShowSplashScreen = 0
    
End Function

Public Function nInputBox(sPrompt As String, sTitle As String) As Long

        nInputBox = InputBox(sPrompt, sTitle)
        
End Function

Public Function bAcknowledgeHVWarning() As Boolean

    iAcknowledge = 2
    frmHV.Show MODAL
    bHighVoltage = True
    If iAcknowledge = 0 Then bAcknowledgeHVWarning = True Else bAcknowledgeHVWarning = False
    
End Function

Public Function bAcknowledgeLasarWarning() As Boolean

End Function

Public Function nDelay(nTime As Single) As Long

    'DESCRIPTION:
    '   Delays the program for a specified time. If delay is
    '   more than 7 seconds, then the shell is called to display
    '   a progress bar to display time remaining.
    'PARAMETERS:
    '   Seconds! = number of seconds to delay
    'EXAMPLE:
    '           Delay 2.3
    Dim T As Single

    T! = Timer
    If Abs(nTime) > 7 Then
        LongDelay nTime
    Else
        Do While Timer - T! < nTime
            Sleep 1
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            DoEvents
        Loop
    End If
    nDelay = 0
    
End Function

Public Sub PrintHeadLine(sTestName As String)
    
    Dim StrTestNumber As String * 9
    Dim SubName As String * 22          'Sub name 20 characters max. (leave 2 spaces)
    Dim StrLow_Limit As String * 12
    Dim StrMeasured As String * 12
    Dim StrHigh_Limit As String * 12
    Dim StrUnit As String * 6
    Dim PassFail As String * 8
 
   frmMain.txtModule.Text = sTestName
   StrTestNumber = "Test"
   SubName = "SubTest Name"
   StrLow_Limit = "Low Limit"
   StrMeasured = "Measured"
   StrHigh_Limit = "High Limit"
   StrUnit = "Units"
   PassFail = "P/F"
    
   ChangeTextColor vbBlack
   
   'PRINT
    Echo ""
    Echo "**** MODULE NAME: " & sTestName & " ****"
    Echo ""
    Echo StrTestNumber & SubName & StrLow_Limit & StrMeasured & StrHigh_Limit & StrUnit & PassFail
    Echo "----     ------------          ---------   --------    ----------  ----- --- "
    
End Sub

Public Sub PrintTestResult(sTestPath As String, sPrintFormat As String, _
                           Optional sMeasured As Variant)
 
    Dim SubName As String * 22
    Dim StrTestNumber As String * 9
    Dim StrHigh_Limit As String * 12
    Dim StrLow_Limit As String * 12
    Dim StrMeasured As String * 12
    Dim StrUnit As String * 6
    Dim PassFail As String * 8
    Dim dLowLimit As Double
    Dim dHighLimit As Double
    Dim dMeasured As Double
    
    SubName = frmMain.txtTestName.Text
    StrTestNumber = frmMain.txtStep.Text
    StrHigh_Limit = frmMain.txtUpperLimit.Text
    StrLow_Limit = frmMain.txtLowerLimit.Text
    StrMeasured = frmMain.txtMeasured.Text
    StrUnit = frmMain.txtUnit.Text
    sPrintFormat = UCase(sPrintFormat)
    
    dLowLimit = Val(StrLow_Limit)
    dHighLimit = Val(StrHigh_Limit)
    If IsMissing(sMeasured) Then sMeasured = 0
    dMeasured = Val(sMeasured)
    
    frmMain.fraInstructions.Visible = False

  Select Case sPrintFormat
  
    Case "BCD", "DIG", "DIGITAL", "BIT" 'DIGITAL DISPLAY IN BCD
        If IsNumeric(StrLow_Limit) Then
          LSet StrLow_Limit = "h" & Hex(StrLow_Limit)
        End If
        If IsNumeric(StrMeasured) Then
          LSet StrMeasured = "h" & Hex(StrMeasured)
        End If
        If IsNumeric(StrHigh_Limit) Then
          LSet StrHigh_Limit = "h" & Hex(StrHigh_Limit)
        End If
        
    Case "SPECIAL"
        StrMeasured = sMeasured
    
    Case "STR", "STRING" 'for UUT Verification or visual verification
        sPrintFormat = "STR"
    
    Case "NO TEST"
    
        StrLow_Limit = "Set Up"
        StrMeasured = "Only"
        StrHigh_Limit = "No Test"
        
    Case "EXP"  'Exponential
    
        LSet StrLow_Limit = Format(StrLow_Limit, "0.000E+00")
        LSet StrMeasured = Format(StrMeasured, "0.000E+00")
        LSet StrHigh_Limit = Format(StrHigh_Limit, "0.000E+00")
        
    Case "DEC", "DECIMAL" 'Decimal
    
        If dLowLimit < 999999 Then
            LSet StrLow_Limit = Format(StrLow_Limit, "000000.00") 'Decimal
        Else
            LSet StrLow_Limit = Format(StrLow_Limit, "0.000E+00") 'Exponential
        End If
        
        If dMeasured < 999999 Then
            LSet StrMeasured = Format(StrMeasured, "000000.00") 'Decimal
        Else
            LSet StrMeasured = Format(StrMeasured, "0.000E+00") 'Exponential
        End If
   
        If dHighLimit < 999999 Then
            LSet StrHigh_Limit = Format(StrHigh_Limit, "000000.00") 'Decimal
        Else
            LSet StrHigh_Limit = Format(StrHigh_Limit, "0.000E+00") 'Exponential
        End If
  
    Case Else   'DEFAULT in Exponantial Format
           
        LSet StrLow_Limit = Format(StrLow_Limit, "0.000E+00")
        LSet StrMeasured = Format(StrMeasured, "0.000E+00")
        LSet StrHigh_Limit = Format(StrHigh_Limit, "0.000E+00")
  
  End Select
      
      If Not Pass Then
        PassFail = "FAIL"
        ChangeTextColor vbRed
      Else
        PassFail = "PASS"
      End If
  
      If dHighLimit = 1E+99 Then
        StrHigh_Limit = "N/A"
      End If
      
      If dLowLimit = -1E-99 Then
        StrLow_Limit = "N/A"
      End If
      
      If dHighLimit = 1E+99 And dLowLimit = -1E-99 And sPrintFormat = "STR" Then
        StrLow_Limit = "User Input D"
        StrMeasured = "ata"
        StrHigh_Limit = ""
        StrUnit = "DATA"
      End If
      
      If Not Pass Then
        'Store Most Recent Failure Record
        'Set current Failed record
            CurrentFailure.sSubName = frmMain.txtTestName.Text
            CurrentFailure.sTestNumber = frmMain.txtStep.Text
            CurrentFailure.sPath = sTestPath
            CurrentFailure.sHighLimit = frmMain.txtUpperLimit.Text
            CurrentFailure.sLowLimit = frmMain.txtLowerLimit.Text
            CurrentFailure.sMeasured = frmMain.txtMeasured.Text
            CurrentFailure.sUnit = frmMain.txtUnit.Text
      End If
      
      Echo StrTestNumber & SubName & StrLow_Limit & StrMeasured & StrHigh_Limit & StrUnit & PassFail
      DoEvents

End Sub

Public Function nShowModuleMenu() As Long

    frmMain.fraInstructions.Visible = False
    MAINMod.ShowModuleMenu
    nShowModuleMenu = 0

End Function

Public Function nSimulation(bStatus As Boolean) As Long
    
    bSimulation = bStatus
    nSimulation = 0
    
End Function

Public Sub SetModuleStatus(iModule As Integer, bStatus As Boolean)

    Dim iCount As Integer

    Select Case iModule
        Case ID_SURVEY
            If Not bStatus Then
                frmMain.lblEndToEndStatus(0).Caption = "Unknown"
                frmMain.lblEndToEndStatus(0).BackColor = &HFFFFC0
                frmMain.lblEndToEndStatus(0).ForeColor = &HC00000
                
                frmMain.lblSTTOStatus(1).Caption = "Unknown"
                frmMain.lblSTTOStatus(1).BackColor = &HFFFFC0
                frmMain.lblSTTOStatus(1).ForeColor = &HC00000
                
                frmMain.lblPwrOnStatus(0).Caption = "Unknown"
                frmMain.lblPwrOnStatus(0).BackColor = &HFFFFC0
                frmMain.lblPwrOnStatus(0).ForeColor = &HC00000
                
                If bModuleMenuBuilt = True Then
                    For iCount = 1 To MaxModules
                        frmMain.lblModuleStatus(iCount).Caption = "Unknown"
                        frmMain.lblModuleStatus(iCount).BackColor = &HFFFFC0
                        frmMain.lblModuleStatus(iCount).ForeColor = &HC00000
                    Next iCount
                End If
                frmMain.MenuOption(5).Picture = frmMain.ImageList1.ListImages(2).Picture
            Else
                frmMain.MenuOption(5).Picture = frmMain.ImageList1.ListImages(3).Picture
            End If
        
        Case END_TO_END
            If Not bStatus Then
                frmMain.lblEndToEndStatus(0).Caption = "FAILED"
                frmMain.lblEndToEndStatus(0).BackColor = vbRed
            Else
                frmMain.lblEndToEndStatus(0).Caption = "PASSED"
                frmMain.lblEndToEndStatus(0).BackColor = vbGreen
            End If
            frmMain.lblEndToEndStatus(0).ForeColor = vbBlack
        
        Case STTO
            If Not bStatus Then
                frmMain.lblSTTOStatus(1).Caption = "FAILED"
                frmMain.lblSTTOStatus(1).BackColor = vbRed
            Else
                frmMain.lblSTTOStatus(1).Caption = "PASSED"
                frmMain.lblSTTOStatus(1).BackColor = vbGreen
            End If
            frmMain.lblSTTOStatus(1).ForeColor = vbBlack
        
        Case PWR_ON
            If Not bStatus Then
                frmMain.lblPwrOnStatus(0).Caption = "FAILED"
                frmMain.lblPwrOnStatus(0).BackColor = vbRed
            Else
                frmMain.lblPwrOnStatus(0).Caption = "PASSED"
                frmMain.lblPwrOnStatus(0).BackColor = vbGreen
            End If
            frmMain.lblPwrOnStatus(0).ForeColor = vbBlack
        
        Case 1 To MaxModules
            If Not bStatus Then
                frmMain.lblModuleStatus(iModule).Caption = "FAILED"
                frmMain.lblModuleStatus(iModule).BackColor = vbRed
            Else
                frmMain.lblModuleStatus(iModule).Caption = "PASSED"
                frmMain.lblModuleStatus(iModule).BackColor = vbGreen
            End If
            frmMain.lblModuleStatus(iModule).ForeColor = vbBlack
        
    End Select
    
End Sub

Public Sub ReSetModuleStatus()

    Dim iCount As Integer
    
        frmMain.lblEndToEndStatus(0).Caption = "Unknown"
        frmMain.lblEndToEndStatus(0).BackColor = &HFFFFC0 ' light green
        frmMain.lblEndToEndStatus(0).ForeColor = &HC00000 ' bright blue
        
        frmMain.lblSTTOStatus(1).Caption = "Unknown"
        frmMain.lblSTTOStatus(1).BackColor = &HFFFFC0
        frmMain.lblSTTOStatus(1).ForeColor = &HC00000
        
        frmMain.lblPwrOnStatus(0).Caption = "Unknown"
        frmMain.lblPwrOnStatus(0).BackColor = &HFFFFC0
        frmMain.lblPwrOnStatus(0).ForeColor = &HC00000
        
        For iCount = 1 To MaxModules
            frmMain.lblModuleStatus(iCount).Caption = "Unknown"
            frmMain.lblModuleStatus(iCount).BackColor = &HFFFFC0
            frmMain.lblModuleStatus(iCount).ForeColor = &HC00000
        Next iCount

End Sub

Public Sub DisplayUserMsgImage(sTitle As String, iTextJustification As Integer, sLinesOfText() As String, sImage As String)

    Dim i As Integer
    Dim nNumberoflines As Long
    Dim sTemp As String
    Dim bCurrentState As Boolean
    Dim bTempProgressBar As Boolean
    
    sTemp = frmMain.lblStatus.Caption
    bCurrentState = frmMain.Timer2.Enabled
    bTempProgressBar = frmMain.ProgressBar.Visible
    frmMain.ProgressBar.Visible = False
    
    frmMain.Timer1.Enabled = True
    frmMain.Timer2.Enabled = False
    frmMain.lblStatus.Caption = "Waiting for User ..."
    
    UserEvent = 0
    
    nNumberoflines = UBound(sLinesOfText)
    frmMain.MainMenuButton.Enabled = True
    frmMain.AbortButton.Enabled = False

    
    frmMain.SSPanel1.Top = 120
    frmMain.SSPanel1.Left = 120
    frmMain.SSPanel1.Height = 2415
    frmMain.SSPanel1.Width = 11895
    frmMain.msgOutBox.Top = 120
    frmMain.msgOutBox.Left = 120
    frmMain.msgOutBox.Height = 2175
    frmMain.msgInBox.Top = 120
    frmMain.msgInBox.Left = 120
    frmMain.lblMsg(1).Left = 120
    
    frmMain.SSPanel2.Top = 2520
    frmMain.SSPanel2.Left = 120
    frmMain.SSPanel2.Height = 6495
    frmMain.SSPanel2.Width = 11895
    frmMain.picGraphic.Top = 120
    frmMain.picGraphic.Left = 120
    frmMain.picGraphic.Height = 6200
    frmMain.picGraphic.Width = 11665
    
    'Build Form
    If nNumberoflines > 7 Then

        'Resize Message Box add Scroll bar
        frmMain.msgOutBox.Width = 11295
        frmMain.msgInBox.Width = 11295
        frmMain.lblMsg(1).Width = 11055
        
        frmMain.VScroll2.Visible = True
        frmMain.VScroll2.LargeChange = 7
        frmMain.VScroll2.Min = 0
        frmMain.VScroll2.Max = nNumberoflines - 7
    Else
        'Resize Message Box
        frmMain.msgOutBox.Width = 11295 + 360
        frmMain.msgInBox.Width = 11295 + 360
        frmMain.lblMsg(1).Width = 11055 + 360
        frmMain.VScroll2.Visible = False
    End If

    frmMain.msgInBox.Height = 120 + ((nNumberoflines) * (frmMain.lblMsg(1).Height))
    frmMain.lblMsg(1).Alignment = iTextJustification
    frmMain.lblMsg(1).Caption = sLinesOfText(1)
    frmMain.lblMsg(1).Visible = True
    
    For i = 2 To nNumberoflines
        
        Load frmMain.lblMsg(i)
        frmMain.lblMsg(i).Top = frmMain.lblMsg(i - 1).Top + 240
        frmMain.lblMsg(i).Alignment = iTextJustification
        frmMain.lblMsg(i).Caption = sLinesOfText(i)
        frmMain.lblMsg(i).Visible = True
    Next i

    frmMain.picGraphic.Picture = LoadPicture(TPSGraphics$ & sImage)
    frmMain.SSPanel1.Visible = True
    frmMain.SSPanel2.Visible = True
    
    Do While UserEvent = 0
        DoEvents
        Delay 0.3
    Loop
    frmMain.ProgressBar.Visible = bTempProgressBar
    
    'performing QUIT causes invalid Err.Raise number due to
    'the fact that all forms have been unloaded from QUIT button operation in ExitForm
    'must bypass frm.Main commands when QUIT is pressed.
    
    If UserEvent = QUIT_BUTTON Then
       If FormMainLoaded = True Then
         Err.Raise USER_EVENT + QUIT_BUTTON
       End If
    End If
    
    For i = 2 To nNumberoflines
        Unload frmMain.lblMsg(i)
    Next i
    
    frmMain.SSPanel1.Visible = False
    frmMain.SSPanel2.Visible = False
    frmMain.lblStatus.Caption = sTemp
    frmMain.Timer2.Enabled = bCurrentState

    frmMain.Timer1.Enabled = False

    If UserEvent = MAINMENU_BUTTON Then Err.Raise USER_EVENT + MAINMENU_BUTTON

End Sub

Public Sub DisplayUserMsg(sTitle As String, nTextJustification As Integer, sLinesOfText() As String)

    Dim i As Integer
    Dim nNumberoflines As Long
    Dim sTemp As String
    Dim bCurrentState As Boolean
    Dim bContinueState As Boolean
    Dim bQuitState As Boolean
    Dim bMainMenuState As Boolean
    
    sTemp = frmMain.lblStatus.Caption
    bCurrentState = frmMain.Timer2.Enabled
    bContinueState = frmMain.Continue.Enabled
    bQuitState = frmMain.Quit.Enabled
    bMainMenuState = frmMain.MainMenuButton.Enabled
    
    frmMain.Continue.Enabled = False
    
    frmMain.Timer1.Enabled = True
    frmMain.Timer2.Enabled = False
    frmMain.lblStatus.Caption = "Waiting for User ..."
    
    nNumberoflines = UBound(sLinesOfText)
    
    frmOperatorMsg.lblInput.Visible = False
    frmOperatorMsg.InputData.Visible = False
    frmOperatorMsg.Width = 8535
    frmOperatorMsg.SSPanel1.Width = 8175
    frmOperatorMsg.msgOutBox.Width = 7575
    frmOperatorMsg.msgInBox.Width = 7575
    frmOperatorMsg.lblMsg(1).Width = 7215
    frmOperatorMsg.lblMsg(1).Left = 120
    frmOperatorMsg.imgLogo.Left = 120

    If nNumberoflines > 20 Then
        'Set limit on Form Size
        frmOperatorMsg.Height = 6645
        
        'Locate Continue Button
        frmOperatorMsg.Continue.Left = 6840
        frmOperatorMsg.SSPanel1.Height = 5295
        frmOperatorMsg.msgOutBox.Height = 5055
        frmOperatorMsg.VScroll1.Height = 5055
        frmOperatorMsg.VScroll1.Visible = True
        frmOperatorMsg.VScroll1.LargeChange = 20
        frmOperatorMsg.VScroll1.Min = 0
        frmOperatorMsg.VScroll1.Max = nNumberoflines - 20
    Else
        'Resize Form
        frmOperatorMsg.Width = 8100
        frmOperatorMsg.SSPanel1.Width = 7815
        frmOperatorMsg.Height = 2085 + (frmOperatorMsg.lblMsg(1).Height * nNumberoflines)
        frmOperatorMsg.SSPanel1.Height = 735 + (frmOperatorMsg.lblMsg(1).Height * nNumberoflines)
        frmOperatorMsg.msgOutBox.Height = 495 + (frmOperatorMsg.lblMsg(1).Height * nNumberoflines)

        'Locate Buttons and Logo
        frmOperatorMsg.Continue.Left = 6360
        frmOperatorMsg.imgLogo.Left = 120
        frmOperatorMsg.VScroll1.Visible = False
    End If
    
    'Build Form
    frmOperatorMsg.Continue.Top = frmOperatorMsg.SSPanel1.Top + frmOperatorMsg.SSPanel1.Height + 120
    frmOperatorMsg.imgLogo.Top = frmOperatorMsg.SSPanel1.Top + frmOperatorMsg.SSPanel1.Height + 120

    frmOperatorMsg.msgInBox.Height = 495 + ((nNumberoflines) * (frmOperatorMsg.lblMsg(1).Height))
    frmOperatorMsg.lblMsg(1).Alignment = nTextJustification
    frmOperatorMsg.lblMsg(1).Caption = sLinesOfText(1)

    For i = 2 To nNumberoflines
        Load frmOperatorMsg.lblMsg(i)
        frmOperatorMsg.lblMsg(i).Top = frmOperatorMsg.lblMsg(i - 1).Top + 240
        frmOperatorMsg.lblMsg(i).Caption = sLinesOfText(i)
        frmOperatorMsg.lblMsg(i).Visible = True
    Next i

    frmOperatorMsg.Caption = sTitle
    CenterForm frmOperatorMsg
    frmOperatorMsg.Show vbModal

    frmMain.lblStatus.Caption = sTemp
    frmMain.Timer2.Enabled = bCurrentState
    
    frmMain.Timer1.Enabled = False
    
    sTemp = frmMain.lblStatus.Caption = sTemp
    frmMain.Timer2.Enabled = bCurrentState
    frmMain.Continue.Enabled = bContinueState
    frmMain.Quit.Enabled = bQuitState
    frmMain.MainMenuButton.Enabled = bMainMenuState
    
End Sub

Public Function sDisplayUserInput(sTitle As String, nTextJustification As Long, sLinesOfText() As String) As String
    
    sDisplayUserInput = sUserInput(sTitle, nTextJustification, sLinesOfText)

End Function

Public Sub FaultDisplay(sSubName As String, sTestNumber As String, _
                             sPath As String, dHighLimit As Double, _
                             dLowLimit As Double, dMeasured As Double, _
                             sStrUnit As String, sPrintFormat As String, _
                             sPCOF() As String)
                             
    Dim n As Long
    Dim i As Integer
    Dim sFailedAtData As String
                             
    frmMain.Timer1.Enabled = True
    frmMain.lblStatus.Caption = "Waiting for User"
    
    ShowWindow frmMain.SeqTextWindow
    frmMain.AbortButton.Visible = False
    frmMain.AbortButton.Enabled = False
    frmMain.RerunButton.Visible = True
    frmMain.RerunButton.Enabled = False
    frmMain.MainMenuButton.Enabled = True
    frmMain.Quit.Enabled = True
    frmMain.PrintButton.Visible = True
    frmMain.PrintButton.Enabled = True
    frmMain.Continue.Enabled = True
    frmMain.SeqTextWindow.ForeColor = &HFF&         'DARK Red
    frmMain.cmdFHDB.Enabled = True
    frmMain.cmdFHDB.Visible = True

    Echo ""
    Echo "                              *** TEST STATUS ***"
    Echo ""
    Echo "                     UUT FAILED: STEP " & CurrentFailure.sTestNumber & " " & CurrentFailure.sSubName
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
        Echo Space(CInt((79 - Len(sFailedAtData)) / 2)) & sFailedAtData
    Else
        If Len("FAILED AT: " & CurrentFailure.sPath) < 80 Then
          Echo Space(CInt((79 - Len("FAILED AT: " & CurrentFailure.sPath)) / 2)) & "FAILED AT: " & CurrentFailure.sPath
        Else
          Echo "FAILED AT: " & CurrentFailure.sPath
        
        End If
    End If
    Echo ""
    
    If CurrentFailure.sHighLimit = CurrentFailure.sLowLimit Then
        If UCase(CurrentFailure.sUnit) = "QUESTION" Or UCase(CurrentFailure.sUnit) = "Y/N" Then
            Echo "                   EXPECTED VALUE: " & CurrentFailure.sHighLimit
        Else
            Select Case UCase(CurrentFailure.sUnit)
                Case "BIT", "DIG", "BCD"
                    Echo "                   EXPECTED VALUE: " & ConvertHexToBin(Hex(CDbl(CurrentFailure.sLowLimit))) & " " & CurrentFailure.sUnit
                Case "DTB"
                    Echo "                   EXPECTED VALUE: DTB PASSED"
                Case Else
                    Echo "                   EXPECTED VALUE: " & CurrentFailure.sLowLimit & " " & CurrentFailure.sUnit
            End Select
        End If
    Else
        If CurrentFailure.sLowLimit = "N/A" Then
            Echo "                   EXPECTED VALUE: LESS THAN " & CurrentFailure.sHighLimit & " " & CurrentFailure.sUnit
        ElseIf CurrentFailure.sHighLimit = "N/A" Then
            Echo "                   EXPECTED VALUE: GREATER THAN " & CurrentFailure.sLowLimit & " " & CurrentFailure.sUnit
        Else
            Echo "                   EXPECTED VALUE: " & CurrentFailure.sLowLimit & " TO " & CurrentFailure.sHighLimit & " " & CurrentFailure.sUnit
        End If
    End If
    
    If UCase(CurrentFailure.sUnit) = "QUESTION" Or UCase(CurrentFailure.sUnit) = "Y/N" Then
        Echo "                   MEASURED VALUE: " & CurrentFailure.sMeasured
    Else
        Select Case UCase(CurrentFailure.sUnit)
            Case "BIT", "DIG", "BCD"
                Echo "                   MEASURED VALUE: " & ConvertHexToBin(Hex(CurrentFailure.sMeasured)) & " " & CurrentFailure.sUnit
            Case "DTB"
                Echo "                   MEASURED VALUE: DTB FAILED"
            Case Else
                Echo "                   MEASURED VALUE: " & CurrentFailure.sMeasured & " " & CurrentFailure.sUnit
        End Select
    End If
    Echo ""
    Echo "                          PROBABLE CAUSE OF FAILURE:"
    Echo ""
    
    If UCase(CurrentFailure.sUnit) = "DTB" Then
        If CurrentFailure.sPath <> "DTB Returned no value" Then
            For n = i To UBound(sPCOF)
                Echo Space(CInt((79 - Len(sPCOF(n))) / 2)) & sPCOF(n)
                If n = 1 Then
                    sCallout = sPCOF(n) & vbCrLf
                Else
                    sCallout = sCallout & sPCOF(n) & vbCrLf
                End If
            Next n
        Else
            For n = 1 To UBound(sPCOF)
                Echo Space(CInt((79 - Len(sPCOF(n))) / 2)) & sPCOF(n)
                If n = 1 Then
                    sCallout = sPCOF(n) & vbCrLf
                Else
                    sCallout = sCallout & sPCOF(n) & vbCrLf
                End If
            Next n
        End If
    Else
        For n = 1 To UBound(sPCOF)
            Echo Space(CInt((79 - Len(sPCOF(n))) / 2)) & sPCOF(n)
            If n = 1 Then
                sCallout = sPCOF(n) & vbCrLf
            Else
                sCallout = sCallout & sPCOF(n) & vbCrLf
            End If
        Next n
    End If
    
    If bEndToEnd Then
        sFailStep = "E" & sTestNumber
    Else
        sFailStep = "M" & sTestNumber
    End If
    If UCase(Left(sTestNumber, 2)) = "ID" Then
        sFailStep = sTestNumber
    End If
    nStatus = False
    SStop = Now
    If Not IsNumeric(CurrentFailure.sMeasured) Then CurrentFailure.sMeasured = "9.7E+37"
    dMeasurement = CDbl(CurrentFailure.sMeasured)
        
    If Not IsNumeric(CurrentFailure.sLowLimit) Then CurrentFailure.sLowLimit = -1E-99
    dlLimit = CDbl(CurrentFailure.sLowLimit)
    If Not IsNumeric(CurrentFailure.sHighLimit) Then CurrentFailure.sHighLimit = 1E+99
    duLimit = CDbl(CurrentFailure.sHighLimit)
    sUOM = CurrentFailure.sUnit
    
End Sub

Public Sub UUTPassed(bStatus As Boolean)

    Dim sUUTStatus As String
    Dim i As Integer
    Dim sStartTmp As String
    Dim sStopTmp As String
    Dim sStatus As String
    Dim sCmdString As String
                             
    ShowWindow frmMain.SeqTextWindow
    frmMain.AbortButton.Enabled = False
    frmMain.NewUUT.Enabled = False
    frmMain.NewUUT.Visible = False
    frmMain.Continue.Enabled = True
    frmMain.MainMenuButton.Enabled = True
        
    If bStatus Then
        frmMain.SeqTextWindow.ForeColor = &H5500 'Green
        sUUTStatus = "UUT IS READY FOR ISSUE"
        nStatus = bStatus
        SStop = Now
        
        If bStatus Then
            sCallout = """"
            sCallout = sCallout + "N/A"
            sCallout = sCallout + """"
            dMeasurement = 0
            dlLimit = 0
            duLimit = 0
            sUOM = """"
            sUOM = sUOM + "N/A"
            sUOM = sUOM + """"
            sStatus = "Passed"
            sFailStep = "E0"
            sComment = """"
            sComment = sComment + "No operator comments"
            sComment = sComment + """"
            
            sStartTmp = Format(sStart, "YYMMDDhhmmSs")
            sStart = sStartTmp + "000"
            
            sStopTmp = Format(SStop, "YYMMDDhhmmSs")
            SStop = sStopTmp + "000"
            
            If Not bSimulation Then
            
                sCmdString = "fhdb.exe " & sStart & " " & SStop & " " & EROs & " "
                sCmdString = sCmdString + sTPCCN & " " & sUUTSerial & " "
                sCmdString = sCmdString + sUUTRev & " " & sIDSerial & " "
                sCmdString = sCmdString + sStatus & " " & sFailStep & " "
                sCmdString = sCmdString + sCallout & " " & dMeasurement & " "
                sCmdString = sCmdString + sUOM & " " & duLimit
                sCmdString = sCmdString + " " & dlLimit & " " & sComment
                i = Shell(sCmdString, vbNormalFocus)
            End If
        Else
            frmMain.SeqTextWindow.ForeColor = &HFF& 'Red
            sUUTStatus = "UUT IS NOT ACCEPTABLE"
        End If
    End If
    
    Echo ""
    Echo ""
    Echo "*******************************************************************************"
    Echo "                     TESTING COMPLETE: " & sUUTStatus
    Echo "                      DATE/TIME: " & Format$(Now, "dddd, d-mmm-yyyy h:mm")
    Echo "*******************************************************************************"
    
End Sub

Public Sub IDPassed(bStatus As Boolean)

    Dim sIDStatus As String
    Dim i As Integer
    Dim sStartTmp As String
    Dim sStopTmp As String
    Dim sStatus As String
    Dim sCmdString As String
    
    ShowWindow frmMain.SeqTextWindow
    frmMain.AbortButton.Enabled = False
    frmMain.MainMenuButton.Enabled = True
    If bStatus Then
        frmMain.SeqTextWindow.ForeColor = &H5500 'Green
        sIDStatus = "INTERFACE DEVICE PASSED"
        nStatus = bStatus
        SStop = Now
        
        If bStatus Then
            sCallout = """"
            sCallout = sCallout + "N/A"
            sCallout = sCallout + """"
            dMeasurement = 0
            dlLimit = 0
            duLimit = 0
            sUOM = """"
            sUOM = sUOM + "N/A"
            sUOM = sUOM + """"
            sFailStep = "E0"
            sStatus = "Passed"
            sComment = """"
            sComment = sComment + "No operator comments"
            sComment = sComment + """"
            
            sStartTmp = Format(sStart, "YYMMDDhhmmSs")
            sStart = sStartTmp + "000"
            
            sStopTmp = Format(SStop, "YYMMDDhhmmSs")
            SStop = sStopTmp + "000"
                
            If Not bSimulation Then
            
                sCmdString = "fhdb.exe " & sStart & " " & SStop & " " & EROs & " "
                sCmdString = sCmdString + sTPCCN & " " & sUUTSerial & " "
                sCmdString = sCmdString + sUUTRev & " " & sIDSerial & " "
                sCmdString = sCmdString + sStatus & " " & sFailStep & " "
                sCmdString = sCmdString + sCallout & " " & dMeasurement & " "
                sCmdString = sCmdString + sUOM & " " & duLimit
                sCmdString = sCmdString + " " & dlLimit & " " & sComment
                i = Shell(sCmdString, vbNormalFocus)
            End If
        Else
        
        frmMain.SeqTextWindow.ForeColor = &HFF& 'Red
        sIDStatus = "INTERFACE DEVICE IS NOT ACCEPTABLE"
        End If
    End If
    Echo ""
    Echo ""
    Echo "*******************************************************************************"
    Echo "                     TESTING COMPLETE: " & sIDStatus
    Echo "                      DATE/TIME: " & Format$(Now, "dddd, d-mmm-yyyy h:mm")
    Echo "*******************************************************************************"
    
End Sub

Public Sub PrintSerialNumberHeader()
     
     sStart = Now
     Echo ""
     Echo "*******************************************************************************"
     Echo " TEST PROGRAM RESULTS FOR: " & UUTPartNo$ & "      S/N: " & sUUTSerial
     Echo " INTERFACE DEVICE PN: " & IDPartNo$ & "   S/N: " & sIDSerial
     Echo " DATE/TIME: " & Format$(sStart, "dddd, d-mmm-yyyy h:mm")
     Echo "*******************************************************************************"
     Echo ""

End Sub

Public Function bTestIDInfo() As Boolean

    Dim iCount As Integer
    
    bTestIDInfo = True ' was bSetIDInfo
    'Check SN to see if new ID
    If IDSN <> sIDSerial Then
        bTestIDInfo = False ' was bSetIDInfo
        frmMain.lblEndToEndStatus(0).Caption = "Unknown"
        frmMain.lblEndToEndStatus(0).BackColor = &HFFFFC0
        frmMain.lblEndToEndStatus(0).ForeColor = &HC00000
        
        frmMain.lblSTTOStatus(1).Caption = "Unknown"
        frmMain.lblSTTOStatus(1).BackColor = &HFFFFC0
        frmMain.lblSTTOStatus(1).ForeColor = &HC00000
        
        frmMain.lblPwrOnStatus(0).Caption = "Unknown"
        frmMain.lblPwrOnStatus(0).BackColor = &HFFFFC0
        frmMain.lblPwrOnStatus(0).ForeColor = &HC00000
        
        If bModuleMenuBuilt = True Then
            For iCount = 1 To MaxModules
                frmMain.lblModuleStatus(iCount).Caption = "Unknown"
                frmMain.lblModuleStatus(iCount).BackColor = &HFFFFC0
                frmMain.lblModuleStatus(iCount).ForeColor = &HC00000
            Next iCount
        End If
        frmMain.MenuOption(5).Picture = frmMain.ImageList1.ListImages(1).Picture
    End If
    
End Function

Public Sub ModulePassed(sModule As String)

    Dim i As Integer
    Dim sStartTmp As String
    Dim sStopTmp As String
    Dim sStatus As String
    Dim sCmdString As String
                             
    ShowWindow frmMain.SeqTextWindow
    frmMain.AbortButton.Enabled = False
    frmMain.MainMenuButton.Enabled = True
    frmMain.PrintButton.Enabled = True
    frmMain.PrintButton.Visible = True
    frmMain.Quit.Enabled = True
    Echo ""
    Echo ""
    Echo "**** MODULE COMPLETE: " & sModule & " PASSED ****"
    Echo "     DATE/TIME: " & Format$(Now, "dddd, d-mmm-yyyy h:mm")
    
    If Not bSimulation And Not bEndToEnd Then
        SStop = Now
        sFailStep = "M0"
        sCallout = """"
        sCallout = sCallout + "N/A"
        sCallout = sCallout + """"
        dMeasurement = 0
        sUOM = """"
        sUOM = sUOM + "N/A"
        sUOM = sUOM + """"
        duLimit = 0
        dlLimit = 0
        sStatus = "Passed"
        sComment = """"
        sComment = sComment + "No operator comments"
        sComment = sComment + """"
            
        sStartTmp = Format(sStart, "YYMMDDhhmmSs")
        sStart = sStartTmp + "000"
            
        sStopTmp = Format(SStop, "YYMMDDhhmmSs")
        SStop = sStopTmp + "000"
                
        If Not bSimulation Then
            
            sCmdString = "fhdb.exe " & sStart & " " & SStop & " " & EROs & " "
            sCmdString = sCmdString + sTPCCN & " " & sUUTSerial & " "
            sCmdString = sCmdString + sUUTRev & " " & sIDSerial & " "
            sCmdString = sCmdString + sStatus & " " & sFailStep & " "
            sCmdString = sCmdString + sCallout & " " & dMeasurement & " "
            sCmdString = sCmdString + sUOM & " " & duLimit & " "
            sCmdString = sCmdString + " " & dlLimit & " " & sComment
            i = Shell(sCmdString, vbHide)
        End If
    End If

End Sub

Public Sub SetTestInformation(sTestName As String, sTestNumber As String, _
                                nUpperLimit As Double, nLowerLimit As Double, sUnit As String, Optional sPrintFormat As String = "DEFAULT")
    
    frmMain.txtStep.Text = ""
    frmMain.txtUpperLimit.Text = ""
    frmMain.txtLowerLimit.Text = ""
    frmMain.txtUnit.Text = ""
    frmMain.txtInstrument.Text = ""
    frmMain.txtCommand.Text = ""
    frmMain.txtMeasured.Text = ""
    frmMain.txtMeasured.BackColor = vbWhite
    
    'Reset System Level Flag States
    Pass = False
    Failed = False
    OutHigh = False
    OutLow = False
    
    'Error check limits
    If nLowerLimit <> -1E-99 Then
        If nLowerLimit > nUpperLimit Then
            Echo "PROGRAMMING ERROR:  Lower Limit Argument is Greater than Upper Limit Argument"
            Err.Raise -1010
            Exit Sub
        End If
    End If
    
    frmMain.txtTestName.Text = sTestName
    frmMain.txtStep.Text = sTestNumber
    Select Case UCase(sUnit)
        Case "DTB"
            frmMain.txtUpperLimit.Text = "PASSED"
            frmMain.txtLowerLimit.Text = "PASSED"
        
        Case "PN"
            frmMain.txtUpperLimit.Text = UUTPartNo
            frmMain.txtLowerLimit.Text = UUTPartNo
        
        Case "QUESTION"
            If nUpperLimit = True Then
                frmMain.txtUpperLimit.Text = "YES"
                frmMain.txtLowerLimit.Text = "YES"
            Else
                frmMain.txtUpperLimit.Text = "NO"
                frmMain.txtLowerLimit.Text = "NO"
            End If
            
        Case "Y/N"
            If nUpperLimit = True Then
                frmMain.txtUpperLimit.Text = "YES"
                frmMain.txtLowerLimit.Text = "YES"
            Else
                frmMain.txtUpperLimit.Text = "NO"
                frmMain.txtLowerLimit.Text = "NO"
            End If

        
        Case "NA"
            frmMain.txtUpperLimit.Text = "NA"
            frmMain.txtLowerLimit.Text = "NA"
            
        Case Else
            Select Case UCase(sPrintFormat)
                Case "DEFAULT"
                    frmMain.txtUpperLimit.Text = CStr(nUpperLimit)
                    frmMain.txtLowerLimit.Text = CStr(nLowerLimit)
                Case "EXP"
                    frmMain.txtUpperLimit.Text = Format(nUpperLimit, "0.0###E+00")
                    frmMain.txtLowerLimit.Text = Format(nLowerLimit, "0.0###E+00")
                Case "DEC"
                    frmMain.txtUpperLimit.Text = Format(nUpperLimit, "######.0##")
                    frmMain.txtLowerLimit.Text = Format(nLowerLimit, "######.0##")
                Case "AUTODEC"
                    'Set Upper Limit Value for Display
                    If nUpperLimit < 0.0001 Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "0.0###E+00")
                    ElseIf nUpperLimit < 0.001 Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "0.000#####")
                    ElseIf nUpperLimit < 0.01 Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "0.00######")
                    ElseIf nUpperLimit < 0.1 Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "0.0#######")
                    ElseIf nUpperLimit < 1# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "0.0#######")
                    ElseIf nUpperLimit < 10# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "#.0#######")
                    ElseIf nUpperLimit < 100# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "##.0######")
                    ElseIf nUpperLimit < 1000# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "###.0#####")
                    ElseIf nUpperLimit < 10000# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "####.0####")
                    ElseIf nUpperLimit < 100000# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "#####.0###")
                    ElseIf nUpperLimit < 1000000# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "######.0##")
                    ElseIf nUpperLimit < 10000000# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "#######.0#")
                    ElseIf nUpperLimit < 100000000# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "########.0")
                    ElseIf nUpperLimit < 1000000000# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "#########")
                    ElseIf nUpperLimit < 10000000000# Then
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "##########")
                    Else
                      frmMain.txtUpperLimit.Text = Format(nUpperLimit, "0.0###E+00")
                    End If
                    'Set Lower Limit Value For Display
                    If nLowerLimit < 0.0001 Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "0.0###E+00")
                    ElseIf nLowerLimit < 0.001 Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "0.000#####")
                    ElseIf nLowerLimit < 0.01 Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "0.00######")
                    ElseIf nLowerLimit < 0.1 Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "0.0#######")
                    ElseIf nLowerLimit < 1# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "0.0#######")
                    ElseIf nLowerLimit < 10# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "#.0#######")
                    ElseIf nLowerLimit < 100# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "##.0######")
                    ElseIf nLowerLimit < 1000# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "###.0#####")
                    ElseIf nLowerLimit < 10000# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "####.0####")
                    ElseIf nLowerLimit < 100000# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "#####.0###")
                    ElseIf nLowerLimit < 1000000# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "######.0##")
                    ElseIf nLowerLimit < 10000000# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "#######.0#")
                    ElseIf nLowerLimit < 100000000# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "########.0")
                    ElseIf nLowerLimit < 1000000000# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "#########")
                    ElseIf nLowerLimit < 10000000000# Then
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "##########")
                    Else
                      frmMain.txtLowerLimit.Text = Format(nLowerLimit, "0.0###E+00")
                    End If
            End Select
    End Select
    frmMain.txtUnit.Text = sUnit

End Sub

Public Sub ClearTestInformation()
                                    
    ClearTestMeasurementInformation
    
End Sub

Public Sub ClearMeasInfo()

    frmMain.txtStep.Text = ""
    frmMain.txtUpperLimit.Text = ""
    frmMain.txtLowerLimit.Text = ""
    frmMain.txtUnit.Text = ""
    frmMain.txtInstrument.Text = ""
    frmMain.txtCommand.Text = ""
    frmMain.txtMeasured.Text = ""
    frmMain.txtMeasured.BackColor = vbWhite
    
End Sub

Public Sub SetMeasuredValue(sValue As String)

    frmMain.txtMeasured.Text = sValue
    
End Sub

Public Sub DisplayProbeMessage(sLocation As String, sProbeInstructions() As String, _
                                Optional bContinueOnly As Boolean = False)
                                
    Dim i As Integer
    Dim iCount As Integer
    Dim nDatafile As Long
    Dim sProbePoint As String
    Dim sDelimiter As String
    Dim sProbe(5) As String
    Dim nNumOfElements As Long
    Dim dFX1 As Double
    Dim dFY1 As Double
    Dim dFX2 As Double
    Dim dFY2 As Double
    Dim dPX1 As Double
    Dim dPY1 As Double
    Dim dPX2 As Double
    Dim dPY2 As Double
    
    frmMain.lblPressContinue.Caption = "PRESS CONTINUE AND/OR PROBE BUTTON"
    If bContinueOnly Then
        frmMain.lblPressContinue.Caption = "PRESS CONTINUE ONLY"
        frmMain.lblPressContinue.ForeColor = vbBlack
    End If
    frmMain.Continue.Enabled = True
    frmMain.AbortButton.Enabled = True
    frmMain.cmdFHDB.Visible = False
    frmMain.cmdDiagnostics.Visible = False
    
    If sProbeData = " " Then
        MsgBox "Probe data file not specified.  Set the ProbeDataFile$ variable", 16, "Shell Error"
        Exit Sub
    End If
    
    nDatafile = FreeFile
    Open ProgramPath$ + sProbeData For Input As #nDatafile
    Do
        Line Input #nDatafile, sProbePoint
        If InStr(1, sProbePoint, sLocation + ",") > 0 Then
            Exit Do
        Else
            sProbePoint = ""
        End If
    Loop Until EOF(nDatafile)
    Close #nDatafile
    If sProbePoint = "" Then
        MsgBox "Error locating probe information in " & sProbeData & " file.", 16, "File Error"
        Exit Sub
    End If
    
    If UBound(sProbeInstructions) > 6 Then
        MsgBox "To many lines of instructions passed to DisplayProbeMessage Subroutine.  " & vbCrLf & _
                "Only 6 line of instructions are allowed.", 16, "Shell Error"
        Exit Sub
    End If
    
    For iCount = 1 To UBound(sProbeInstructions)
        frmMain.lblInstruction(iCount).Caption = sProbeInstructions(iCount)
    Next iCount
    If iCount < 6 Then
        For i = iCount To 6
            frmMain.lblInstruction(i).Caption = ""
        Next i
    End If

    'Turn off MainMenu and Test Window
    frmMain.MainMenu.Visible = False
    frmMain.SeqTextWindow.Visible = False
    frmMain.ModuleOuter.Visible = False
    'Turn on Picture window
    frmMain.PictureWindow.Visible = True
    frmMain.pinp.Visible = True
    frmMain.fraInstructions.Visible = True
    
    sDelimiter = ","

    nNumOfElements = StringToList%(sProbePoint, sProbe(), sDelimiter)
    sProbe(4) = CStr(Val(sProbe(4) + 180))
    frmMain.txtMeasured.Text = ""
    
    frmMain.PictureWindow.Picture = LoadPicture()  'LoadPicture is a Visual Basic Function
    frmMain.pinp.Picture = LoadPicture(TPSGraphics$ + sProbeAssy)
    frmMain.pinp.Top = frmMain.PictureWindow.Height / 2 - frmMain.pinp.Height / 2
    frmMain.pinp.Left = frmMain.PictureWindow.Width / 2 - frmMain.pinp.Width / 2
    frmMain.pinp.Visible = True

    frmMain.Message.Caption = sProbe(1)
    frmMain.Message.Move frmMain.pinp.Left + Val(sProbe(4)), frmMain.pinp.Top + Val(sProbe(5))
    frmMain.Message.Visible = True
    frmMain.PictureWindow.Cls
    frmMain.PictureWindow.Visible = True

    dFX1 = frmMain.Message.Left + frmMain.Message.Width / 2
    dFY1 = frmMain.Message.Top + frmMain.Message.Height / 2
    dFX2 = Val(sProbe(2)) + frmMain.pinp.Left
    dFY2 = Val(sProbe(3)) + frmMain.pinp.Top

    dPX1 = -frmMain.pinp.Left + frmMain.Message.Left + frmMain.Message.Width / 2
    dPY1 = -frmMain.pinp.Top + frmMain.Message.Top + frmMain.Message.Height / 2
    dPX2 = Val(sProbe(2))
    dPY2 = Val(sProbe(3))
    
    frmMain.PictureWindow.Line (dFX1, dFY1)-(dFX2, dFY2), RGB(255, 0, 0)
    frmMain.pinp.Line (dPX1, dPY1)-(dPX2, dPY2), RGB(255, 0, 0)
    Delay 0.2

End Sub

Public Sub PowerApplied(bState As Boolean)

    If bState Then
        frmMain.lblPowerApplied.Visible = True
        If bHighVoltage Then
            frmMain.picDanger.Visible = True
        End If
    Else
        frmMain.lblPowerApplied.Visible = False
        frmMain.picDanger.Visible = False
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

'This function is used to test a LED device
'   sLEDfile:   name of bitmap picture showing the LED
'   sLEDname:   name if LED device as shown on schematic
'   sLEDstatus: (OPTIONAL)  "ON" checks LED ON
'                           "OFF" checks LED OFF
 
Public Function DisplayLEDMessage(sLEDfile As String, sLEDname As String, Optional sLEDstatus As String = "ON") As Boolean
    
    'declare string variables
    Dim sMsgON As String
    Dim sMsgOFF As String
    
    'convert to upper case
    sLEDstatus = UCase(sLEDstatus)
        
    'assign display message
    sMsgON = "Visually verify that " & sLEDname & " is ON:" + vbCrLf + "   Click YES if " & sLEDname & " is ON" + vbCrLf + "   Click NO if " & sLEDname & " is OFF"
    sMsgOFF = "Visually verify that " & sLEDname & " is OFF:" + vbCrLf + "   Click YES if " & sLEDname & " is OFF" + vbCrLf + "   Click NO if " & sLEDname & " is ON"
    
    'load LED picture into LED frame
    frmDisplayLED.PictureWindow.Picture = LoadPicture(ProgramPath + "\graphics\Form Images\" + sLEDfile)
    
    Select Case sLEDstatus
        Case "ON"   'checking LED status "ON"
            'display LED message in frame testbox
            frmDisplayLED.cmdTextBox.Text = sMsgON
            frmDisplayLED.Show vbModal  'wait for user event
 
            'Result
            If LEDUserResponse = True Then
                frmDisplayLED.Hide
                If LEDButtonPressed <> "YES" Then
                    'Echo "LED FAIL" 'debug statement
                    DisplayLEDMessage = False
                Else
                    'Echo "LED PASS" 'debug statement
                    DisplayLEDMessage = True
                End If
            End If
            
        Case "OFF"  'checking LED status "OFF"
            'display LED message in frame testbox
            frmDisplayLED.cmdTextBox.Text = sMsgOFF
            frmDisplayLED.Show vbModal  'wait for user event
 
            'Result
            If LEDUserResponse = True Then
                frmDisplayLED.Hide
                If LEDButtonPressed <> "YES" Then
                    'Echo "LED FAIL" 'debug statement
                    DisplayLEDMessage = False
                Else
                    'Echo "LED PASS" 'debug statement
                    DisplayLEDMessage = True
                End If
            End If
 
        Case Else   'invalid argument
            Echo "INVALID ARGUMENT"
            DisplayLEDMessage = False
            Exit Function
    End Select
    
End Function

Public Function bYesNoUserMsgImage(sTitle As String, iTextJustification As Integer, sLinesOfText() As String, sImage As String) As Boolean

    Dim i As Integer
    Dim nNumberoflines As Long
    Dim sTemp As String
    Dim bCurrentState As Boolean
    Dim sData As String
    Dim nErr As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "OPERATOR"
    frmMain.txtCommand.Text = "bYesNoUserMsgImage"
    
    sTemp = frmMain.lblStatus.Caption
    bCurrentState = frmMain.Timer2.Enabled
    
    frmMain.Timer1.Enabled = True
    frmMain.Timer2.Enabled = False
    frmMain.lblStatus.Caption = "Waiting for User ..."
    
    UserEvent = 0
    
    nNumberoflines = UBound(sLinesOfText)
    frmMain.MainMenuButton.Enabled = True
    frmMain.AbortButton.Enabled = False
    
    'Disable controls
    frmMain.Quit.Enabled = False
    frmMain.MainMenuButton.Enabled = False
    frmMain.PrintButton.Enabled = False
    frmMain.AbortButton.Visible = False
    frmMain.Continue.Visible = False
    frmMain.RerunButton.Visible = False
    
    'Enable Yes/No Buttons
    frmMain.YesButton.Enabled = True
    frmMain.YesButton.Visible = True
    frmMain.NoButton.Enabled = True
    frmMain.NoButton.Visible = True
    
    frmMain.SSPanel1.Top = 120
    frmMain.SSPanel1.Left = 120
    frmMain.SSPanel1.Height = 2415
    frmMain.SSPanel1.Width = 11895
    frmMain.msgOutBox.Top = 120
    frmMain.msgOutBox.Left = 120
    frmMain.msgOutBox.Height = 2175
    frmMain.msgInBox.Top = 120
    frmMain.msgInBox.Left = 120
    frmMain.lblMsg(1).Left = 120
    
    frmMain.SSPanel2.Top = 2520
    frmMain.SSPanel2.Left = 120
    frmMain.SSPanel2.Height = 6495
    frmMain.SSPanel2.Width = 11895
    frmMain.picGraphic.Top = 120
    frmMain.picGraphic.Left = 120
    frmMain.picGraphic.Height = 6200
    frmMain.picGraphic.Width = 11665
    
    'Build Form
    If nNumberoflines > 7 Then

        'Resize Message Box add Scroll bar
        frmMain.msgOutBox.Width = 11295
        frmMain.msgInBox.Width = 11295
        frmMain.lblMsg(1).Width = 11055
        
        frmMain.VScroll2.Visible = True
        frmMain.VScroll2.LargeChange = 7
        frmMain.VScroll2.Min = 0
        frmMain.VScroll2.Max = nNumberoflines - 7
    Else
        'Resize Message Box
        frmMain.msgOutBox.Width = 11295 + 360
        frmMain.msgInBox.Width = 11295 + 360
        frmMain.lblMsg(1).Width = 11055 + 360
        frmMain.VScroll2.Visible = False
    End If
    
    frmMain.msgInBox.Height = 120 + ((nNumberoflines) * (frmMain.lblMsg(1).Height))
    frmMain.lblMsg(1).Alignment = iTextJustification
    frmMain.lblMsg(1).Caption = sLinesOfText(1)
    frmMain.lblMsg(1).Visible = True
    
    For i = 2 To nNumberoflines
        Load frmMain.lblMsg(i)
        frmMain.lblMsg(i).Top = frmMain.lblMsg(i - 1).Top + 240
        frmMain.lblMsg(i).Alignment = iTextJustification
        frmMain.lblMsg(i).Caption = sLinesOfText(i)
        frmMain.lblMsg(i).Visible = True
    Next i
    
    frmMain.picGraphic.Picture = LoadPicture(TPSGraphics$ & sImage)
    frmMain.SSPanel1.Visible = True
    frmMain.SSPanel2.Visible = True
    
    Do While UserEvent = 0
        DoEvents
        Delay 0.3
    Loop
    
    If UserEvent = YES_BUTTON Then
        bYesNoUserMsgImage = True
        frmMain.txtMeasured.Text = "YES"
    Else
        bYesNoUserMsgImage = False
        frmMain.txtMeasured.Text = "NO"
    End If
    
    'disable Yes/No Buttons
    frmMain.YesButton.Enabled = False
    frmMain.YesButton.Visible = False
    frmMain.NoButton.Enabled = False
    frmMain.NoButton.Visible = False
    'Show Abort and Continue button
    frmMain.Continue.Visible = True
    frmMain.AbortButton.Visible = True

    For i = 2 To nNumberoflines
        Unload frmMain.lblMsg(i)
    Next i
    
    frmMain.SSPanel1.Visible = False
    frmMain.SSPanel2.Visible = False
    frmMain.lblStatus.Caption = sTemp
    frmMain.Timer2.Enabled = bCurrentState
    
    frmMain.Timer1.Enabled = False

End Function

Public Function nEnableDiagnostics() As Long

    frmMain.cmdDiagnostics.Visible = True

End Function

Public Function sGetCurrentUUTserno() As String
  
  sGetCurrentUUTserno = sUUTSerial

End Function

Public Sub DisableNewUUT()
    
    frmMain.NewUUT.Visible = False
    frmMain.NewUUT.Enabled = False

End Sub

Public Sub EnableNewUUT()
    
    frmMain.NewUUT.Visible = True
    frmMain.NewUUT.Enabled = True

End Sub

Public Sub nDisableMainMenu()
    
    frmMain.MainMenu.Enabled = False

End Sub
