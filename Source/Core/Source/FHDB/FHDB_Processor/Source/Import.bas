Attribute VB_Name = "m_Import"
'**********************************************************************
'***    Unite States Marine Corps                                   ***
'***                                                                ***
'***    Nomenclature:   Module "m_Import" : FHDB_Processor          ***
'***    Written By:     Dave Joiner                                 ***
'***    Purpose:                                                    ***
'***  ECP-TETS-023                                                  ***
'***    General Module to support the Import Form.                  ***
'***    The Importer will unzip (Import.zip) into (Import.dat).     ***
'***    The ATE Serial Number is validated and if successful the    ***
'***    [Delete] button is enabled.                                 ***
'***    When the Operator presses Delete, all records in the        ***
'***    stated range are deleted, a blank record is added as a      ***
'***    place holder and the database is compacted. a detailed      ***
'***    summery is generated and INI keys are Updated.              ***
'***  2.1.0                                                         ***
'***    Changed all references to either TETS.INI or VIPERT.ini in  ***
'***    any comments to Tester.ini                                  ***
'***    Sub Import                                                  ***
'***    Removed the unused Dims                                     ***
'***    Changed the selection of where the import is coming from by ***
'***    having the operator select its location. Also pretty much   ***
'***    chnaged the entire code in this sub.                        ***
'***    Function ValidImportDisk                                    ***
'***    Modified the message stating the failure of finding the file***
'***    Sub InitFileName                                            ***
'***    Modified how sImportZipFile is set.                         ***
'**********************************************************************

Option Explicit

Dim sDBDir As String                 'Drive/Path/ info
Dim sImportZipFile As String         'Import Zip file name
Dim sImportDatFile As String         'Import Dat file name

Dim nRecs_to_be_Deleted As Long      'How many records to delete in an Import Operation
Dim nRecsLeft As Long                'How many records left in the Database after an Import operation
Dim iImportSerialNum As Integer           'System Serial Number from the Import file

Dim nImportStartRec As Long          'Starting record from the Import file
Dim nImportEndRec As Long            'Ending record from the Import file
Dim nImportDDI As Integer            'DDI from Import file

Dim nRec_Num As Long                 'Current Record Number

Dim sBackSlash As String                '"/" character

Const DAT_FILE As String = "Import.Dat"       'Import Data File Name
Const DBCOMPACTED As String = "FHDB_Compacted.mdb"    'Compacted Database File name


Function ValidSerialNum() As Boolean
'**********************************************************************
'***  Procedure to verify ATE Serial Number is valid.               ***
'**********************************************************************
Dim sSerialNum As String             'System Serial Number from the Tester's INI
Dim iSerialNum As Integer            'System Serial Number from the Tester's INI

On Error GoTo ErrorLabel
    
    'Get Info from Tester's INI file
    sSerialNum = CInt(GatherIniFileInformation("System Startup", "SN", ""))
    If IsNumeric(sSerialNum) Then
        iSerialNum = CInt(sSerialNum)
    Else
        ValidSerialNum = False
        MsgBox "ATE Serial Number in the Tester's INI file is invalid.", vbCritical
        StatusMsg ("Import Disk is Invalid")
        Exit Function
    End If
    
    'If ATE Serial Number from the Import file Matches the ATE Serial Number from the Database,
    If iImportSerialNum = iSerialNum Then
        StatusMsg ("ATE Serial Number (" & Format(iImportSerialNum, "000") & ") has been confirmed.")
        MsgBox "The ATE Serial Number (" & Format(iImportSerialNum, "000") & ") has been confirmed.", , "FHDB Importer"
        ValidSerialNum = True
    Else
        'If it dose not match, inform Operator and do not allow records to be Deleted.
         MsgBox "ATE Serial Number " & iImportSerialNum & " does not match!!" & vbCrLf _
         & "Please varifiy you have the correct Import Disk.", vbCritical, _
           "Serial Number Mismatch"
        StatusMsg ("Import Disk is Invalid")
        ValidSerialNum = False
    End If
    Exit Function
    
ErrorLabel:
    If Err.Number = 53 Then                 'Handle Error if File Does Not Exist
        ValidSerialNum = False
        Resume Next
    End If

End Function


Sub CompactFHDB()
'******************************************************************************
'***   Procedure to Compact the FHDB Database after records are deleted.    ***
'***   Creates a new compacted database named FHDB_Compacted.mdb            ***
'***   Deletes the original, then renames the Compacted database            ***
'***   to the original name.                                                ***
'***   At the present time there is not a Microsoft Method to               ***
'***   Compact a Database without creating another one.                     ***
'***                                                DJoiner  04/20/2001     ***
'******************************************************************************
Dim sCompactDB As String             'Compacted Database Drive/Path/Name

On Error GoTo CompactError

    'Make sure the Database is closed
    If bDBOpened = True Then
        Call CloseDB
    End If
    
    'Get the File names and loctions from the Tester's INI file
    sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
    'Assign Path and file name to the Compacted Database
    sCompactDB = sDBDir & sBackSlash & DBCOMPACTED
 
    'Make sure there isn't already a file with the
    'name of the compacted database.
    If Dir(sCompactDB) <> "" Then
        Kill (sCompactDB)
    End If
    
   'Basic compact - creating new database named FHDB_Compacted.mdb
   DBEngine.CompactDatabase sDBFile, sCompactDB
   
   Kill sDBFile     ' Delete the original database

   ' Rename the file back to the original name
   Name sCompactDB As sDBFile
   
   Exit Sub
   
CompactError:
    Err.Clear
    Resume Next

End Sub


Sub DeleteRecords()
'*****************************************************************************
'***         Procedure to Delete Records from the FHDB from                ***
'***         Start Record ID# to the End Record ID# identified             ***
'***         in the Import.dat file.                                       ***
'***                                            Dave Joiner  05/11/2001    ***
'*****************************************************************************
Dim dPBarInterval As Double         'Progress Bar Interval
Dim sImportFile As String           'Drive/Path/Name for the Import file
Dim nTotalRec As Long               'Total Number of Records in the RecordSet

   On Error GoTo ErrLabel
    'Get Info from Tester's INI file
    sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
    nTotalRec = 0        'Initialize Total Record Count
    'Initialize SQL Query to retrieve all Records from the FAULTS Table
    'From the Starting record number to the Ending record number, Order By Recorord ID
    sSql = "SELECT * FROM FAULTS WHERE Record_Identifier between " & nRecsLeft & " and " & nImportEndRec
        
    bDBOpened = True            'Set DB Opened Flag to indicate Opened
    Set wrkJet = Workspaces(0)                                      'Open Workspace
    Set dbsFHDB = wrkJet.OpenDatabase(sDBFile)                      'Open Database
    Set rstFaults = dbsFHDB.OpenRecordset(sSql, dbOpenDynaset)      'Open Recordset
  
    rstFaults.MoveLast                              'Move to last Record
    nRecs_to_be_Deleted = rstFaults.RecordCount     'Count the Records
    rstFaults.MoveFirst                             'Move to the first Record
    
    Screen.MousePointer = vbHourglass
    frmBackground.PBarFile.Visible = True
    frmBackground.PBarFile.Value = 30
    dPBarInterval = (50 / nRecs_to_be_Deleted)
   'Loop through Recordset and DELETE each record, Display Operation Status
    Do Until rstFaults.EOF
        nRec_Num = rstFaults!Record_Identifier           'Assign Current Record Number
        rstFaults.Delete              'Delete current record.       'Comment out for Debugging Only
        rstFaults.MoveNext            'Next Record
        nTotalRec = nTotalRec + 1     'Count Records as they are DELETED
        frmBackground.PBarFile.Value = (dPBarInterval * nTotalRec) + 30
    Loop
    frmBackground.PBarFile.Value = 80
    '-----------------------------------------------------------------------------
    'When Deleting all records and performing a Compact DB operation it is
    'necessary to have at least one record remaining in order to keep the
    'AutoNumber field incrementing to the next consecutive number.
    'This will require the addition of a blank record to be used as a
    'Record_Indentifier number place holder.
    '-----------------------------------------------------------------------------
    
    Set rstFaults = Nothing     'Empty Recordset
    
    sSql = "SELECT * FROM FAULTS ORDER BY Record_Identifier"
    Set rstFaults = dbsFHDB.OpenRecordset(sSql, dbOpenDynaset)      'Open Recordset
    If rstFaults.RecordCount > 1 Then rstFaults.MoveLast            'If more than one record, move to last
    'Add a Blank Record only when the Database will be empty prior to Compacting
    'This will ensure the record number will increment correctly.
    If rstFaults.RecordCount < 1 Then
    'Add a Blank Record and Identify it as such.
        rstFaults.AddNew
        rstFaults!Start_Time = Now          'Mark present Date/Time
        rstFaults!TPCCN = "BLANK RECORD"    'ID as a Blank Record
        rstFaults!Test_Status = True
        rstFaults!Fault_Callout = "This Record is left intentionally Blank" & vbCrLf _
                                & "It is used by the system as a Place Holder to maintain the Record_Identifier Count."
        rstFaults.Update
    End If

    Call UpdateImportIni        'Update the ini file
    Call CloseDB                'Close the Database
    
    Exit Sub
    
ErrLabel:
    StatusMsg (Err.Description)     'Display any Error in the Status Bar
    Debug.Print "Error Number: " & Err.Number & vbCrLf _
                & "Description: " & Err.Description         'For Debugging
    Err.Clear
    
    
End Sub


Sub UpdateImportIni()
'*********************************************************************************
'**Procedure to Update the LASTCHECKED, DDI, and IMPORT_TIME in the Tester's INI**
'*********************************************************************************

    Call UpdateIniFile("FHDB", "LASTCHECKED", "")   'Update Last time checked key
    Call UpdateIniFile("FHDB", "DDI", Val(nImportDDI))   'Update the DDI key
    Call UpdateIniFile("FHDB", "IMPORT_TIME", "")   'Update the IMPORT_TIME key

End Sub


Function VaildRecordNumbers() As Boolean
'**************************************************************************
'***    Procedure to Validate Starting Record Number and Ending         ***
'***    Record Number Identified in the Confirmation file exist in      ***
'***    the FHDB Database.                                              ***
'***    If Record numbers do not exist, the error is handled and        ***
'*** the Import_Time and DDI keys in the Tester's INI file are Updated. ***
'***    Records are not Deleted.                                        ***
'***                                Dave Joiner   04/26/2001            ***
'**************************************************************************
Dim bStartExists As Boolean     'Flag to indicate Starting file exists
Dim bEndExists As Boolean       'Flag to indicate Ending file exists
Dim sSql2 As String             'Variable to hold second SQL statement

   On Error GoTo ErrLabel
   
   bStartExists = False     'Initialize Record Number exits flags
   bEndExists = False
   
    'Get Info from Tester's INI file
    sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
     
    'Set SQL Statements to query the Database for Starting and Ending record number
    sSql = "SELECT * FROM FAULTS WHERE Record_Identifier = " & nImportStartRec
    sSql2 = "SELECT * FROM FAULTS WHERE Record_Identifier = " & nImportEndRec
    
    'Set Database Opened flag
    bDBOpened = True
    Set wrkJet = Workspaces(0)                                      'Open Workspace
    Set dbsFHDB = wrkJet.OpenDatabase(sDBFile)                      'Open Database
    
    'Run first SQL Statement and Query the Database
    Set rstFaults = dbsFHDB.OpenRecordset(sSql, dbOpenDynaset)      'Open Recordset
    'Move through the returned RecordSet to allow a count
    rstFaults.MoveLast                          'Move to last Record
    rstFaults.MoveFirst                         'Move to the First Record
    
    'If there is a record in the RecordSet, Set Starting Record Exists flag to True
    If rstFaults.RecordCount = 1 Then bStartExists = True
    'If a "No Record" Error is encountered, Set Starting Record Exists flag to False
    If Err.Number = 3021 Then bStartExists = False
    Err.Clear                           'Clear the Errors
    
    'Run second SQL Statement and Query the Database
    Set rstFaults = dbsFHDB.OpenRecordset(sSql2, dbOpenDynaset)      'Open Recordset
    'Move through the returned RecordSet to allow a count
    rstFaults.MoveLast                          'Move to last Record
    rstFaults.MoveFirst                         'Move to the First Record
    
    If rstFaults.RecordCount = 1 Then bEndExists = True     'If only one Record is returned, Set flag.
    If Err.Number = 3021 Then bEndExists = False            'No current record.
    Err.Clear
    
    Call CloseDB
    
    If bStartExists = True And bEndExists = True Then
        VaildRecordNumbers = True
    Else
        VaildRecordNumbers = False
    End If
               
    Exit Function
    
ErrLabel:
    StatusMsg (Err.Description)     'Display Error Description in Status Bar
    Resume Next                     'If an Error occurs, resume code execution
    
End Function


Sub Import()
'*****************************************************************************
'***                           Main Import Routine                         ***
'*****************************************************************************

    frmViewer.Hide
    MDIMain.Caption = "FHDB Importer"
    DoEvents

    If bOnStart = True Then
        'Pop About Screen
        bNoUnload = True
        CenterForm frmAbout: frmAbout.Show: frmAbout.Refresh
        Delay 1
        Unload frmAbout         'Remove About Screen
        bOnStart = False
    End If

    If frmViewer.Visible = True Then        ''Hide the Viewer if visible
        frmViewer.Hide
    End If
    
    Call CloseDB        'Make sure the Database Connection is not open
    Call ClearViewer
    Call Disable_Navigation
    
    'Check Database for Record Count, If no Records, Abort
    Call OpenDB
        
        If rstFaults.RecordCount = 0 Then
            MsgBox "There are no records in the DataBase - Operation Aborted" _
                                , vbExclamation, "Import Operation Aborted"
            bNoRecords = True
            If bNoUnload = False Then
                UnloadImporter         'DJoiner 3/11/02
            End If
            bNoUnload = False
            Exit Sub
        End If
        
    Call CloseDB
    
    sFileLocation = UDD_HD_Path(IMPORT_FILE)
    
    If sFileLocation = "" Then
        UnloadImporter
        Exit Sub
    End If
    
    Call InitFileNames      'Initialize File Drive/Path/File Name

    bOnStart = False
    frmBackground.PBarFile.Visible = True
    frmBackground.PBarFile.Value = 10
    
    If ValidImportDisk Then
       'Validate Starting and Ending Record Numbers
       If VaildRecordNumbers Then
          frmBackground.PBarFile.Value = 20
          ProcessImport (True)
       Else        'The Starting and Ending Records are not in the Database
          MsgBox "The Starting and/or Ending records from the Import Disk are not present in the Database." & vbCrLf & "The FHDB Importer will terminate.", , "FHDB Importer"
          UnloadImporter     'Terminate application
       End If
    Else        'Import Disk is not valid or not present
      UnloadImporter     'Terminate application
    End If

End Sub


Sub ProcessImport(bDelete As Boolean)
'*****************************************************************************
'***      Routine to handle Delete, Compact, and Tester's INI updates      ***
'*****************************************************************************
Dim dDate_to_Next_Export As Date     'Date and Time the next Export will be due
Dim dDate_of_Last_Export As Date     'Date and Time of the Last Export
    
    'Query Database, check for Starting and Ending record.
    'If Records do not exist, do not Delete Records and terminate Importer.
    
   If bDelete Then
        StatusMsg ("Importing Data..............")
        Call DeleteRecords         'Delete Records
        StatusMsg ("Compacting Database............")
        frmBackground.PBarFile.Value = 90
        Call CompactFHDB
        Screen.MousePointer = vbDefault
                
        'Get Info from Tester's INI file
        dDate_of_Last_Export = GatherIniFileInformation("FHDB", "EXPORT_TIME", "")  'Last Export Date/Time
        dDate_to_Next_Export = DateAdd("d", nImportDDI, dDate_of_Last_Export)   'Calculate Next Export due date.
        StatusMsg ("Import successful")
        frmBackground.PBarFile.Value = 100
        MsgBox "Import Operation completed successfully." & vbCrLf & "The next Export Operation will be due: " & dDate_to_Next_Export, , "FHDB Importer"
        frmBackground.PBarFile.Visible = False
        Call UnloadImporter
    Else
        MsgBox "The Starting and Ending records do not exist in the Database." & vbCrLf _
               & "The FHDB Importer will terminate.", , "FHDB Importer"
        frmBackground.PBarFile.Visible = False
        Call UnloadImporter
    End If
    Screen.MousePointer = vbDefault
    frmBackground.PBarFile.Visible = False
    
End Sub


Function ValidImportDisk() As Boolean
'Read File
'Open "...\Import.dat" For Output As #1
'First get File name from the Tester's INI file
Dim sImport_ATE_SerialNum As String       'ATE Serial Number
Dim sImportFile As String   'File to open and read
Dim iResponse As Integer    'Users Response to message box

On Error GoTo ErrLabel
    
   
        If FileExists(sImportZipFile) Then      'Does the Zipped File exist
            'Extract the Import.zip file into the Import.dat file
            Call UnZipIt
                    
            sImportFile = sImportDatFile
            StatusMsg ("Reading Import Disk")
            
            'Open Import.dat and read data
            Open sImportFile For Input As #1
            Input #1, sImport_ATE_SerialNum, nImportStartRec, nImportEndRec, nImportDDI
            Close #1
            
            'Catch a non-numeric Serial Number Error
            If IsNumeric(sImport_ATE_SerialNum) Then
                iImportSerialNum = CInt(sImport_ATE_SerialNum)
            Else
                MsgBox "The ATE Serial Number is " & sImport_ATE_SerialNum & " ." & vbCrLf _
                     & "This is an invalid number.", vbCritical, "Invalid ATE Serial Number"
                    Kill sImportDatFile
                    Exit Function
            End If
    
            If ValidSerialNum Then
                ValidImportDisk = True
            End If
            
            Kill sImportDatFile     'Delete temporary Import.Dat file
        Else
            MsgBox "The " & sImportZipFile & " can't be found." & vbCrLf & "The FHDB Importer will terminate.", vbCritical, "Importer File Error"
            ValidImportDisk = False
        End If
    
    Exit Function
ErrLabel:

    MsgBox Err.Description, vbExclamation
    StatusMsg (Err.Description)
    Err.Clear

End Function


Sub UnloadImporter()

    frmBackground.PBarFile.Visible = False
        'If launched from the FHDB Processor, return to Viewer
    If bTerminateImporter = False Then
        frmViewer.Show
        ReturnAll
    Else
        'Terminate Importer application
        End
    End If

End Sub


Sub UnZipIt()

    Dim ResultCode As xcdError
    ' All properties keep their default values except the four below
    frmViewer.xUnzip.FilesToProcess = DAT_FILE ' The file to unzip
    frmViewer.xUnzip.PreservePaths = False ' In case file is stored in the
        ' zip file with a path, we need to make sure the path is
        ' removed so that the file will match with "readme.txt"
    
    frmViewer.xUnzip.ZipFilename = sImportZipFile
    frmViewer.xUnzip.UnzipToFolder = sDBDir

    ' Start unzipping
    ResultCode = frmViewer.xUnzip.Unzip
    ' Check the return value.
    If ResultCode <> xerSuccess Then
        MsgBox "Unsuccessful. Error # " + Str(ResultCode) + " occurred. " _
        + "Description: " + frmViewer.xUnzip.GetErrorDescription(xvtError, ResultCode)
    End If

End Sub


Public Sub InitFileNames()
'*****************************************************************************
'***    Initiate File names and paths for extraction of Zipped file.       ***
'***                                        Dave Joiner  06/07/2001        ***
'*****************************************************************************

    sBackSlash = Chr(92)
    'Get Database Name and Path from ini File
    sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
    sDBDir = FindPath(sDBFile)              'Get Drive\Path
    'Get Import File Name from ini File
    sImportZipFile = GatherIniFileInformation("File Locations", "FHDB_IMPORT", "")  'Import.Zip
    sImportZipFile = sFileLocation & "\" & sImportZipFile
    sImportDatFile = sDBDir & sBackSlash & DAT_FILE     'Import.DAT file
    
End Sub


Public Function FindPath(sParsed) As String
'*****************************************************************************
'***           Parse off file name to find the Directory Path.             ***
'***                                        Dave Joiner  06/07/2001        ***
'*****************************************************************************
Dim x As Integer      'Index

    'Parse off end until a "\" is found
    For x = Len(sParsed) To 1 Step -1
        If Asc(Mid$(sParsed, x, 1)) = 92 Then       'chr(92) = \
            Exit For
        End If
    Next x
    FindPath = Left$(sParsed, (x - 1))

End Function


Function FileExists(Path$) As Integer
'*****************************************************************************
'***           Find out if File exists and if Disk not ready               ***
'*****************************************************************************

Dim x As Long 'File Handle
Dim iResponse As Integer

    x& = FreeFile

On Error GoTo FileErrorHandler

    'Test File
        Open Path$ For Input As x&
    If Err = 0 Then
        'File Opens Correctly
        FileExists = True
    Else
        'File Error
        FileExists = False
    End If
    Close x&
    Exit Function
    
FileErrorHandler:
    
    If Err = 71 Then
    Debug.Print Err.Number
    Debug.Print Err.Description
        iResponse = MsgBox(Error$, vbRetryCancel + vbCritical, "UDD Archive Error")
        If iResponse = vbCancel Then
            FileExists = False
            Exit Function
        Else
            Resume
        End If
    Else
        FileExists = False
        Exit Function
    End If

End Function

