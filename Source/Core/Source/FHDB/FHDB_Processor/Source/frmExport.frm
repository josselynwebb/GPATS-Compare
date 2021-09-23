VERSION 5.00
Object = "{DB797681-40E0-11D2-9BD5-0060082AE372}#4.5#0"; "xceedzip.dll"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmExport 
   Caption         =   "File Exporter"
   ClientHeight    =   5760
   ClientLeft      =   60
   ClientTop       =   630
   ClientWidth     =   10335
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   Moveable        =   0   'False
   NegotiateMenus  =   0   'False
   ScaleHeight     =   5760
   ScaleWidth      =   10335
   WindowState     =   2  'Maximized
   Begin Threed.SSCommand cmdZip 
      Height          =   375
      Left            =   7700
      TabIndex        =   0
      Top             =   1200
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   661
      _StockProps     =   78
      Caption         =   "&Zip"
   End
   Begin MSComctlLib.ProgressBar BarGlobal 
      Height          =   300
      Left            =   1440
      TabIndex        =   9
      Top             =   4800
      Visible         =   0   'False
      Width           =   8040
      _ExtentX        =   14182
      _ExtentY        =   529
      _Version        =   393216
      Appearance      =   1
   End
   Begin MSComctlLib.ListView lstResults 
      Height          =   1215
      Left            =   1560
      TabIndex        =   2
      Top             =   2880
      Width           =   7575
      _ExtentX        =   13361
      _ExtentY        =   2143
      View            =   2
      MultiSelect     =   -1  'True
      LabelWrap       =   -1  'True
      HideSelection   =   -1  'True
      AllowReorder    =   -1  'True
      FlatScrollBar   =   -1  'True
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   1
      NumItems        =   0
   End
   Begin Threed.SSFrame ssfraResults 
      Height          =   1575
      Left            =   1440
      TabIndex        =   3
      Top             =   2640
      Width           =   7815
      _Version        =   65536
      _ExtentX        =   13785
      _ExtentY        =   2778
      _StockProps     =   14
      Caption         =   "Results"
      ForeColor       =   -2147483635
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Font3D          =   3
   End
   Begin Threed.SSFrame ssfraCommand 
      Height          =   1575
      Left            =   1440
      TabIndex        =   4
      Top             =   840
      Width           =   7815
      _Version        =   65536
      _ExtentX        =   13785
      _ExtentY        =   2778
      _StockProps     =   14
      ForeColor       =   -2147483635
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Font3D          =   3
      Begin VB.TextBox txtZipFilename 
         Alignment       =   2  'Center
         Height          =   285
         Left            =   1680
         TabIndex        =   6
         Top             =   360
         Width           =   4095
      End
      Begin VB.TextBox txtFilesToProcess 
         Alignment       =   2  'Center
         Height          =   285
         Left            =   1680
         TabIndex        =   5
         Top             =   720
         Width           =   4095
      End
      Begin Threed.SSCommand cmdClose 
         Height          =   375
         Left            =   6250
         TabIndex        =   1
         Top             =   960
         Width           =   1125
         _Version        =   65536
         _ExtentX        =   1984
         _ExtentY        =   661
         _StockProps     =   78
         Caption         =   "C&lose"
      End
      Begin VB.Label lblZipFileName 
         Caption         =   "Zip file name : "
         Height          =   255
         Left            =   240
         TabIndex        =   8
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label lblFilesToProcess 
         Caption         =   "File to process : "
         Height          =   255
         Index           =   0
         Left            =   240
         TabIndex        =   7
         Top             =   720
         Width           =   1215
      End
   End
   Begin Threed.SSPanel sspnlMain 
      Height          =   3885
      Left            =   1080
      TabIndex        =   10
      Top             =   600
      Width           =   8625
      _Version        =   65536
      _ExtentX        =   15214
      _ExtentY        =   6853
      _StockProps     =   15
      BackColor       =   12632256
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BevelOuter      =   1
      BevelInner      =   1
   End
   Begin XceedZipLibCtl.XceedZip XZip 
      Left            =   9240
      Top             =   480
      BasePath        =   ""
      CompressionLevel=   6
      EncryptionPassword=   ""
      RequiredFileAttributes=   0
      ExcludedFileAttributes=   24
      FilesToProcess  =   ""
      FilesToExclude  =   ""
      MinDateToProcess=   2
      MaxDateToProcess=   2958465
      MinSizeToProcess=   0
      MaxSizeToProcess=   0
      SplitSize       =   0
      PreservePaths   =   -1  'True
      ProcessSubfolders=   0   'False
      SkipIfExisting  =   0   'False
      SkipIfNotExisting=   0   'False
      SkipIfOlderDate =   0   'False
      SkipIfOlderVersion=   0   'False
      TempFolder      =   ""
      UseTempFile     =   -1  'True
      UnzipToFolder   =   ""
      ZipFilename     =   ""
      SpanMultipleDisks=   2
      ExtraHeaders    =   10
      ZipOpenedFiles  =   0   'False
      BackgroundProcessing=   0   'False
      SfxBinrayModule =   ""
      SfxDefaultPassword=   ""
      SfxDefaultUnzipToFolder=   ""
      SfxExistingFileBehavior=   0
      SfxReadmeFile   =   ""
      SfxExecuteAfter =   ""
      SfxInstallMode  =   0   'False
      SfxProgramGroup =   ""
      SfxProgramGroupItems=   ""
      SfxExtensionsToAssociate=   ""
      SfxIconFilename =   ""
   End
   Begin VB.Menu mnuOperation 
      Caption         =   "&Operation"
      Begin VB.Menu mnuViewer 
         Caption         =   "Vie&wer"
      End
      Begin VB.Menu mnuImport 
         Caption         =   "I&mport"
      End
      Begin VB.Menu mnuSep1 
         Caption         =   "-"
      End
      Begin VB.Menu mnuExit 
         Caption         =   "E&xit"
      End
   End
   Begin VB.Menu mnuHelp 
      Caption         =   "&Help"
      Begin VB.Menu mnuAbout 
         Caption         =   "&About the FHDB Processor"
      End
   End
End
Attribute VB_Name = "frmExport"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'**********************************************************************
'***    United States Marine Corps                                  ***
'***                                                                ***
'***    Nomenclature:   Form "frmExport" : FHDB FHDB_Processor      ***
'***    Written By:     Dave Joiner                                 ***
'***    Purpose:                                                    ***
'***  ECP-TETS-023                                                  ***
'***    This form provides the User Interface to Export             ***
'***    Files from the FHDB. Both the Faults Table and the          ***
'***    System Table are Exported as a Zipped version of the        ***
'***    FHBD.MDB File.(EXPORT.zip) The Zipped file will span        ***
'***    multiple disks if size dictates.                            ***
'***    The System Table contains only one record that is either    ***
'***    created or updated immediately pior to Export.              ***
'***  2.1.0                                                         ***
'***    Changed all references to either TETS.INI or VIPERT.ini in  ***
'***    any comments to Tester.ini                                  ***
'***    Sub Form_Load                                               ***
'***    Added a variable dDisplayMsg, used to hold a message string ***
'***    to be displayed in a mesgbox, used to shorten and be able to***
'***    view without a lot of scrolling.                            ***
'***    Left the unload call to frmExport intead of Me.             ***
'***    Modified the lower portion of the function on where the FHDB***
'***    will be saved and how that it is selected.  Also removed an ***
'***    operator message on what to do, no longer revelant.         ***
'***    Sub xZip_FileStatus                                         ***
'***    Modified the call to StatusMsg to display the location that ***
'***    was being written to.                                       ***
'***    xZip_ProcessCompleted                                       ***
'***    Added the test for xerCannotUpdateAndSpan to display a      ***
'***    possible reason for the error, what was being displayed was ***
'***    kind of lacking explaning what happen. Also tested for the  ***
'***    return xerOpenZipFile to display a proper error message     ***
'**********************************************************************


Option Explicit



Public Sub AddMessage(sToAdd As String)
'--------------------------------------------------
'---    Adds a line in the lstResults Listbox   ---
'--------------------------------------------------

Dim lList As ListItem
    Set lList = frmExport.lstResults.ListItems.Add(, , sToAdd)
    DoEvents
End Sub


Private Sub cmdClose_Click()

On Error GoTo ErrorLabel
   
    Unload Me                   'Unload Export Form from memory
        
ErrorLabel:
    If Err.Number = 53 Then     'Handle Error if File Does Not Exist
        Resume Next
    End If
        
End Sub


Private Sub txtFilesToProcess_Click()
'Prevent Operator from changing Default File Location and Name

    cmdClose.SetFocus
    
End Sub


Private Sub txtZipFilename_Click()
'Prevent Operator from changing Default File Location and Name

    cmdClose.SetFocus
    
End Sub


Private Sub Form_Load()

Dim dDisplayMsg As String        'message to be displayed in a message box
'--------------------------------------------------------------
'---        Load Export Form and Initialize Fields          ---
'--------------------------------------------------------------

    If bOnStart = True Then
        'Pop Up About Screen
        bNoUnload = True
        CenterForm frmAbout: frmAbout.Show: frmAbout.Refresh
        Delay 1                 'Show About Screen for 1 Second
        Unload frmAbout         'Remove About Screen
        bOnStart = False
    End If
                
    bExport = True      'Set the Export Operation Flag
    Call CloseDB        'Make sure the Database Connection is not open
    Call ClearViewer
    Call Disable_Navigation
    
    'Check Database for Record Count, If no Records, Abort
    Call OpenDB
    If rstFaults.RecordCount = 0 Then
        dDisplayMsg = "There are no records in the DataBase - Operation Aborted"
        MsgBox dDisplayMsg, vbExclamation, "Export Operation Aborted"
        bNoRecords = True
            If bNoUnload = False Then
                Unload frmExport
            End If
            bNoUnload = False
            Exit Sub
        End If
    Call CloseDB
        
    'Get File Names from the Tester's INI File
    sDatabase = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
    sExportFile = GatherIniFileInformation("File Locations", "FHDB_EXPORT", "")
    
    frmExport.Show              'Launches the Export Data Form
    
    sFileLocation = UDD_HD_Path(EXPORT_FILE)
    If sFileLocation = "" Then
        bExport = False
        gResponse = vbCancel
        Exit Sub
    End If
    
    'Set Status Messsage
    Call StatusMsg("File Export Utility -- Ready to Export Files")
    
    Call InitializeExportFields
    txtZipFilename.Text = sFileLocation & "\" & sExportFile
    txtFilesToProcess.Text = sDatabase
    
End Sub


Private Sub Form_Unload(Cancel As Integer)
'--------------------------------------------------------------
'---        When Export Form Unloads                        ---
'---        If Viewer is loaded, set Status Message         ---
'---        Else Load Viewer Form                           ---
'--------------------------------------------------------------

    If frmViewer.Visible Then
        Call StatusMsg("Record Viewer")     'Set Status Messsage
        Call ReturnAll              'Query all records and load into RecordSet
    Else
        frmViewer.Show
        frmViewer.SetFocus
    End If
    bExport = False     'Reset Export Operation Flag
    
End Sub


Private Sub cmdZip_Click()
'----------------------------------------------------------------------
'--         Call procedure to initiate the Zipping Process          ---
'----------------------------------------------------------------------
    
    cmdClose.Enabled = False
    Call UpdateSystemRecord
    If bSystemRecordError = True Then
        Unload Me
        Exit Sub
    End If
    Call Zip_It
    If bDiskError Then
        cmdClose.Enabled = True
        cmdZip.SetFocus
        bDiskError = False  'Reset Disk Error Flag
    Else
        cmdClose.Enabled = True
        cmdClose.SetFocus
    End If
        
End Sub


'***********************************************************************
'****             Procedures to handle ZIP Events                   ****
'***********************************************************************

Public Sub Zip_It()
'--------------------------------------------------------------------------
'---   Main properties are first reset to the desired default values,   ---
'---   then the values of the form's controls are copied to xZip's      ---
'---   properties and the Zip method is called.                         ---
'---   If a value is encountered in the SfxBinaryModule property,       ---
'---   XceedZip will create a self-extracting zip file with an          ---
'---   integrated self-extracting zip file module specified by          ---
'---   the SfxBinaryModule property.                                    ---
'---------------------------------------------------------------------------

Dim xErr As xcdError
     
    Call StatusMsg("Zipping Export File")                     'Set Status Messsage
        
    frmExport.BarGlobal.Visible = True                        'Make ProgressBar Visible
    
    Call SetDefaultProperties(frmExport.XZip)                 'Reset to Default properties

    frmExport.XZip.ZipFilename = frmExport.txtZipFilename.Text          'Get Output File name
    frmExport.XZip.FilesToProcess = frmExport.txtFilesToProcess.Text    'Get Input file name
    frmExport.lstResults.ListItems.Clear                      'Clear the Results List Field
        
    xErr = frmExport.XZip.Zip                                 'Used to Report Zip Errors

End Sub


Public Sub xZip_FileStatus(ByVal sFilename As String, ByVal lSize As Long, _
                    ByVal lCompressedSize As Long, ByVal lBytesProcessed As Long, _
                    ByVal nBytesPercent As Integer, ByVal nCompressionRatio As Integer, _
                    ByVal bFileCompleted As Boolean)
'----------------------------------------------------------------------
'---    The FileStatus Event is triggered during the processing     ---
'---    of each file. It provides progress information about        ---
'---    the status of each file.                                    ---
'----------------------------------------------------------------------
                    
    Debug.Print nBytesPercent
    If nBytesPercent < 98 Then
        Call StatusMsg("Creating Zip file")
    Else
        Call StatusMsg("Writing file to " & sFileLocation & ".............................")
    End If

End Sub


Public Sub xZip_GlobalStatus(ByVal lFilesTotal As Long, ByVal lFilesProcessed As Long, _
                    ByVal lFilesSkipped As Long, ByVal nFilesPercent As Integer, _
                    ByVal lBytesTotal As Long, ByVal lBytesProcessed As Long, _
                    ByVal lBytesSkipped As Long, ByVal nBytesPercent As Integer, _
                    ByVal lBytesOutput As Long, ByVal nCompressionRatio As Integer)
'--------------------------------------------------------------------------
'---    The GlobalStatus Event provides progress information on the     ---
'---    execution of any Xceed Zip method.                              ---
'--------------------------------------------------------------------------
                    
    frmExport.BarGlobal.Value = nBytesPercent     'Update Progress Bar
        
End Sub


Public Sub xZip_InsertDisk(ByVal nDiskNumber As Long, bDiskInserted As Boolean)
'------------------------------------------------------------------------------
'---    The InsertDisk Event is triggered when reading or writing           ---
'---    a spanned zip file and a new or different disk must be inserted     ---
'---    into the drive to continue the current operation.                   ---
'------------------------------------------------------------------------------

Dim nAnswer As VbMsgBoxResult
    
    If nDiskNumber = 0 Then
        If bExport Then
            MsgBox "Please insert a USB disk into the USB slot" & vbCrLf _
            & "This USB Disk will be overwritten" & vbCrLf _
            & vbCrLf & "Then depress the 'Zip' Button to begin Export process" & vbCrLf _
            & "Press the Close button to Cancel Export operation." & vbCrLf & vbCrLf _
            & "        Label Disk: Export Disk Number 1", vbInformation, _
              "Insert a USB into the USB slot "
            bDiskError = True
            Exit Sub
        Else
            nAnswer = MsgBox("This zip file is part of a multidisk zip file. " _
                           & "Please insert the last disk of the set.", vbExclamation + vbOKCancel)
        End If
    Else
        nAnswer = MsgBox("Please insert USB Disk #" & Trim(Str(nDiskNumber)) & vbCrLf & vbCrLf _
                        & "Label Disk: Export Disk Number " & Trim(Str(nDiskNumber)), _
                            vbExclamation + vbOKCancel)
    End If
        
    bDiskInserted = (nAnswer = vbOK)
    
End Sub


Public Sub xZip_ListingFile(ByVal sFilename As String, _
                ByVal sComment As String, ByVal lSize As Long, _
                ByVal lCompressedSize As Long, ByVal nCompressionRatio As Integer, _
                ByVal xAttributes As XceedZipLibCtl.xcdFileAttributes, ByVal lCRC As Long, _
                ByVal dtLastModified As Date, ByVal dtLastAccessed As Date, _
                ByVal dtCreated As Date, ByVal xMethod As XceedZipLibCtl.xcdCompressionMethod, _
                ByVal bEncrypted As Boolean, ByVal lDiskNumber As Long, _
                ByVal bExcluded As Boolean, ByVal xReason As XceedZipLibCtl.xcdSkippingReason)
'----------------------------------------------------------------------------------
'---    The Listing File event is triggered once for each file listed as        ---
'---    a result of calling the ListZipContents method. It provides complete    ---
'---    file information for each file.                                         ---
'-----------------------------------------------------------------------------------

    If bExcluded Then
        Call AddMessage("Excluding " & sFilename & " (reason:" & Str(xReason) & ")")
    Else
        Call AddMessage("Listing " & sFilename)
    End If
    
End Sub


Public Sub xZip_ProcessCompleted(ByVal lFilesTotal As Long, ByVal lFilesProcessed As Long, _
                    ByVal lFilesSkipped As Long, ByVal lBytesTotal As Long, _
                    ByVal lBytesProcessed As Long, ByVal lBytesSkipped As Long, _
                    ByVal lBytesOutput As Long, ByVal nCompressionRatio As Integer, _
                    ByVal xResult As XceedZipLibCtl.xcdError)
'----------------------------------------------------------------------------------
'---    The ProcessCompleted event is triggered every time an Xceed Zip         ---
'---    method has completed its operation. This event provides you with        ---
'---    some statistics on the operation. It also provides the method           ---
'---    call's return value (xResult parameter), which is of particular         ---
'---    use when you have set the BackgroundProcessing flag to True.            ---
'-----------------------------------------------------------------------------------

    If bDiskError Then Exit Sub
    ' Display error code and description
    Call AddMessage("Process Completed. Error code: " & xResult)
    Call AddMessage(XZip.GetErrorDescription(xvtError, xResult))
    If xResult = xerCannotUpdateAndSpan Then
        Call AddMessage("Probable cause " & sFileLocation & "Export.Zip already exists")
    End If
    
    ' Display statistics
    If XZip.CurrentOperation = xcoPreviewing Then
        AddMessage ("Total number of files previewed: " + Str(lFilesTotal))
    ElseIf XZip.CurrentOperation = xcoListing Then
        AddMessage ("Total number of files listed: " + Str(lFilesTotal))
    Else
        AddMessage ("Total number of files processed: " + Str(lFilesProcessed))
        AddMessage ("Total number of files skipped: " + Str(lFilesSkipped))
        AddMessage ("The Compression Ratio is: " + Str(nCompressionRatio) & " Percent")
    End If
    
    frmExport.BarGlobal.Visible = False
   
    
    If xResult = 0 Then
        sMessageString = "File Export Operation Successful"
        DoEvents
        Call UpdateIniFile("FHDB", "EXPORT_TIME", "")
        DoEvents
        MsgBox "File Export Operation was Successful", , "FILE EXPORT SUCCESSFUL"
    ElseIf xResult = xerOpenZipFile Then
        sMessageString = "File Export Operation Unsuccessful, See Results for cause."
        MsgBox "There was an Error Exporting Files" & vbCrLf _
               & "See Results readout for cause." & vbCrLf & vbCrLf _
               & "The most probable cause is" & vbCrLf _
               & sFileLocation & "Export.Zip already exists" & vbCrLf _
               & "and the user doesn't have permission to modify it.", vbCritical, "EXPORT ERROR"
    Else
        sMessageString = "File Export Operation Unsuccessful, See Results for cause."
        MsgBox "There was an Error Exporting Files" & vbCrLf _
               & "See Results readout for cause.", vbCritical, "EXPORT ERROR"
    End If
    
    If sMessageString <> frmBackground.stbMain.Panels(1).Text Then
        frmBackground.stbMain.Panels(1).Text = sMessageString
    End If
    
End Sub
                                                                           

Public Sub xZip_SkippingFile(ByVal sFilename As String, ByVal sComment As String, _
                    ByVal sFilenameOnDisk As String, ByVal lSize As Long, _
                    ByVal lCompressedSize As Long, _
                    ByVal xAttributes As XceedZipLibCtl.xcdFileAttributes, _
                    ByVal lCRC As Long, ByVal dtLastModified As Date, _
                    ByVal dtLastAccessed As Date, ByVal dtCreated As Date, _
                    ByVal xMethod As XceedZipLibCtl.xcdCompressionMethod, _
                    ByVal bEncrypted As Boolean, _
                    ByVal xReason As XceedZipLibCtl.xcdSkippingReason)
'--------------------------------------------------------------------------
'---    The SkippingFile event is triggered whenever a file cannot be   ---
'---    processed because of a problem, because it didn't correspond    ---
'---    to the criteria specified in the xZip controls properties       ---
'---    (like MinDateToProcess) or because the bExcluded flag was set   ---
'---    in the ZipPreprocessingFile or UnzipPreprocessingFile events.   ---
'---------------------------------------------------------------------------
                    
    Call AddMessage("Skipping " & sFilename & " (reason:" & Str(xReason) & ")")
    
End Sub


Public Sub xZip_ZipPreprocessingFile(sFilename As String, sComment As String, _
                    ByVal sSourceFilename As String, ByVal lSize As Long, _
                    xAttributes As XceedZipLibCtl.xcdFileAttributes, _
                    dtLastModified As Date, dtLastAccessed As Date, dtCreated As Date, _
                    xMethod As XceedZipLibCtl.xcdCompressionMethod, bEncrypted As Boolean, _
                    sPassword As String, bExcluded As Boolean, _
                    ByVal xReason As XceedZipLibCtl.xcdSkippingReason, _
                    ByVal bExisting As Boolean)
'------------------------------------------------------------------------------
'---    The ZipPreprocessingFile represents the last validation before      ---
'---    the real zipping of each file begins. It gives the user chance      ---
'---    to change information about the file, or cause the file to be       ---
'---    skipped (by setting the bExcluded parameter)                        ---
'-------------------------------------------------------------------------------
                    
    If bExcluded Then
        Call AddMessage("Excluding " & sFilename & " (reason:" & Str(xReason) & ")")
    Else
        Call AddMessage("Zipping " & sFilename)
    End If
    
End Sub


'***********************************************************************
'****                      Menu Functions                           ****
'***********************************************************************

Private Sub mnuAbout_Click()

    Call ShowAbout              'Show About Form

End Sub


Private Sub mnuExit_Click()
    
    Unload MDIMain
    
End Sub


Private Sub mnuImport_Click()

    Unload frmExport
    bTerminateImporter = False
    Call Import
    
End Sub


Private Sub mnuViewer_Click()
    
    Unload frmExport

End Sub


'***********************************************************************
'****                   Status Line Messages                        ****
'***********************************************************************

Private Sub txtZipFilename_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    txtZipFilename.MousePointer = 1
    Call StatusMsg("The Export File name and location.")
End Sub

Private Sub Form_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("File Exporter.")
End Sub

Private Sub cmdClose_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Closes File Exporter and Launches the Viewer.")
End Sub

Private Sub cmdZip_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Zips Database to Export File.")
End Sub

Private Sub txtFilesToProcess_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    txtFilesToProcess.MousePointer = 1
    Call StatusMsg("The Database File name and location.")
End Sub

Private Sub lstResults_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Results of the File Export Operation.")
End Sub

Private Sub mnuOperation_Click()
     Call StatusMsg("Chose another Operation or Quit.")
End Sub

Private Sub mnuHelp_Click()
    Call StatusMsg("FHDB Help.")
End Sub

