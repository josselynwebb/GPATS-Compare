'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Text
Imports System.Diagnostics
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Imports DAO
Imports Microsoft.Win32

Public Module FHDB_Services


    '=========================================================
    '**********************************************************************
    '***    ManTech Test Systems                                        ***
    '***                                                                ***
    '***    Nomenclature:   Module "FHDB_Services" : VIPERTMenu           ***
    '***    Written By:     Dave Joiner                                 ***
    '***    Purpose:                                                    ***
    '***    General Module to support the FHDB Database functionality   ***
    '***    as it relates to the UDD.                                   ***
    '***    Supports Validation, Copy, Zip Routines and Launches        ***
    '***    the FHDB_Processor to the appropriate Operation.            ***
    '***    Reference: (Microsoft DAO 2.5/3.51 Compatibility Library)   ***
    '**********************************************************************

    '*****************************************************************************
    '************                  GLOBAL VARIABLES                  *************
    '*****************************************************************************
    Friend DAODBEngine_definst As New DAO.DBEngine
    Public bZipIt As Boolean 'Flag to signal that this file will be Zipped
    Public nLastSystemRecord As Integer 'Last Record number on the System
    Public nLastRestoredRecord As Integer 'Last Record number restored
    Public sDBTempFileName As String 'File name for the Temporary database file (Used for comparison)
    Public sDBDir As String 'Database Drive and Path
    Public bSuccess As Boolean 'Flag to signal a successful Operation
    Public bDone As Boolean 'Switch to indicate Operation Complete
    Public sSXBinaryFilePath As String 'Location of the Binary File used by the Zip utility
    Public bDBOperationDue As Boolean 'Flag to signal an Import/Export Operation is due
    Public sDBFile As String 'Database File Name
    Public bFileError As Boolean 'Flag to signal a File Error
    Public bUnload As Boolean 'Flag to signal a form unload event
    Public sTitle As String 'Meassage Box Title String
    Public sFHDBFilePath As String 'FHDB Drive/Path/Name
    Public sBackSlash As String 'Back Slash Character

    Public dtXDate As Date 'Export Date from the VIPERT.ini File
    Public nDDI As Integer 'Data Dump Interval from the VIPERT.ini File
    Public dtIDate As Date 'Import Date from the VIPERT.ini File
    Public sTestItFile As String 'Holds the Zipped Test File Path and Name
    Public bZipToADrive As Boolean 'Flag to indicate file will fit on USB

    '*****************************************************************************
    '************                   LOCAL VARIABLES                  *************
    '*****************************************************************************
    '---  Dimension DAO Database Objects  ---
    Dim wrkJet As DAO.Workspace 'DAO Workspace Object
    
    Dim rstFaults As DAO.Recordset'FHDB Faults Table Recordset Object
    Dim dbsFHDB As DAO.Database 'DAO FHDB Database Object
    '-----------------------------------------
    
    Dim nDBSize As Integer 'File size of the Database
    
    Dim nADriveFreeSpace As Integer 'Free space left on the USB Disk
    Dim nDaysUntilDue As Integer 'Days until an Export Operation is due
    Dim sSpace As String 'Holds an empty string
    Dim bCancel As Boolean 'Flag to signal Operator has Canceled a file operation
    Dim bDBOpened As Boolean 'Flag to signal that the Database is Open
    Dim sSql As String 'Holds the SQL Query
    Dim sArg As String 'Holds the Argument for launching the Importer/Exporter
    Dim bZipped As Boolean 'Switch to indicate file has been Zipped
    Dim bNoFile As Boolean 'Flag to signal no file exists
    Dim sMsgCaption As String 'Caption in the Message Box
    Dim sLabelCaption As String 'Caption for Comments
    Dim bDBEmpty As Boolean 'Flag to signal the Database has no records
    'Comparative info on the Database files
    
    Dim nLastRecOnSystem As Integer 'Record number of the last record on the system Database
    
    Dim nLastRecOnUSB As Integer 'Record number of the last record on the USB Database
    Dim dtUSBLastModified As Date 'Date the Database on USB was last Modified
    Dim dtSystemLastModified As Date 'Date the Database on System was last Modified
    Dim nDaysSinceLastExport As Integer 'Days since the last Export Operation
    Dim nDaysSinceLastImport As Integer 'Days since the last Import Operation

    '*****************************************************************************
    '************                     CONSTANTS                      *************
    '*****************************************************************************
    '--- Global Constants  ---
    Public Const TEST_ZIP_FILE As String = "TestIt.zip" 'Self Extracting Test Zip File Name
    Public Const BINARY_FILE As String = "xcdsfx32.bin" 'Binary File required for Self-Extractor

    '--- Local Constants  ---
    Const DB_FAULT_TABLE As String = "FAULTS" 'FHDB Database Table Name
    Const MINUDDSIZE As Integer = 200000 'Minumum buffer to allow for UDD Data Files 200MB
    Const REASONABLE_TIME As Short = 5 'Time in days before Export/Import is required
    'Argument Action Indicators
    Const IMPORT As String = "IMPORT"
    Const EXPORT As String = "EXPORT"
    Const NONE As String = "NONE"
    'Self Extracting Zip File Constants
    Const TEMP_FILE As String = "FHDB_TEMP.mdb" 'Temp Database File Name
    Const SELFX_TITLE As String = "The Xceed Zip Self-Extractor" 'For Self Extracting File
    Const SELFX_INTRO As String = "Welcome to the Xceed Zip Self-Extractor. " & "This program will unzip some files onto your system."
    'Constants for results display
    Const DATABASE_EXCEEDS As String = "The FHDB Database exceeds the 1.4MB size limit." & vbCrLf
    Const UDD_TERMINATE As String = vbCrLf & vbCrLf & vbCrLf & "The UDD Utility will terminate in ......."
    Const LAUNCH_IMPORTER As String = "The FHDB Importer will automatically Launch." & vbCrLf
    Const LAUNCH_EXPORTER As String = "The FHDB Exporter will automatically Launch." & vbCrLf
    Const PRIOR_TO_UDD As String = "and must be completed prior to creating a UDD." & vbCrLf
    Const EXPORT_DUE As String = "The FHDB Database file size exceeds the " & vbCrLf & "Free Space Available on the USB Disk." & vbCrLf & "The FHDB Export Operation must be completed prior to creating a UDD." & vbCrLf
    Const IMPORT_PAST_DUE As String = "The FHDB Import Operation is past due " & vbCrLf
    Const UDD_RECENT As String = "The Database from the UDD is the most recent."
    Const SYS_RECENT As String = "The System Database is most recent"
    Const DB_EQUAL As String = "The Databases have been compared and found to be equal."



    Public Sub AddMessage(ByVal sToAdd As String)
        '*****************************************************************************
        '***              Adds a line to the lblcomment caption                    ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        frmUdd.lblComment.Text = frmUdd.lblComment.Text & vbCrLf & sToAdd
        Debug.Print(sToAdd) 'For Debugging only
        Delay(1) 'Delay to allow Operator time to read

    End Sub


    Public Sub CheckDBFileSize()
        '******************************************************************************
        '***                        Check FHDB File Size                            ***
        '******************************************************************************
        '***    If File will fit on USB Disk, Copy it. If it Will Not Fit,       ***
        '***    Test it as a Self Extracting Zip File. If it fits, Copy it as a     ***
        '***    Zipped File. If it still won't fit, Force an Import/Export          ***
        '***    Operation and Abort this Operation.                                 ***
        '***    Delays are added to allow the Operator time to read Comments.       ***
        '***                                        Dave Joiner  06/04/2001         ***
        '******************************************************************************

        Try ' On Error GoTo ErrorHandler

            'Initiate Variables
            nADriveFreeSpace = 0 'File Space
            nDBSize = 0 'Database Size
            bFileError = False 'Reset File Error Flag
            bZipToADrive = True 'Zipped fit to USB Flag

            ShowFreeSpace(sFDD) 'Find Free space on USB Disk

            'If operation Canceled, Exit this Sub
            If bCancel = True Then Exit Sub

            ShowFileSize(sDBFile) 'Find the size of the FHDB Database

            '--The average UDD file size is 29KB, at 40KB it leaves an 11KB Buffer
            '--Although, if the UDD is larger than 40KB, the actual Free Space will be used
            '--If FreeSpace is greater Than 1.4MB then FreeSpace is equal to 1.4MB (Use Highest value)

            'Set bFileError flag in the ShowFileSpace() Function if there is a file Error
            If bFileError = True Then Exit Sub

            If MINUDDSIZE < nADriveFreeSpace Then 'If FreeSpace is greater than the Min UDD Size
                nADriveFreeSpace = MINUDDSIZE 'Then use the Greatest value (Min UDD Size)
            End If

            '--If Database file will fit on USB uncompressed, no other validation is required.
            '--Copy to USB and complete the operation.

            '********** Database will fit on USB Disk unzipped **********
            If nDBSize < nADriveFreeSpace Then
                bDBOperationDue = True 'Set Import/Export Operation Due Flag
                frmUdd.lblComment.Text = ""
                frmUdd.lblTitle.Text = "Copying Database"
                If usb_flag = True Then
                    AddMessage("Copying Database file to the USB Disk.........")
                ElseIf uddHDflag = True Then
                    AddMessage("Copying Database file to the UDD Folder.......")
                End If
                frmUdd.proProgress.Visible = True
                frmUdd.proProgress.Value = 10
                FileCopy(sDBFile, sFDD_DB) 'Copy Database to the USB Disk
                frmUdd.proProgress.Value = 100
                If usb_flag = True Then
                    AddMessage(vbCrLf & vbCrLf & "The Database file has been successfully" & " copied to the USB Disk.")
                ElseIf uddHDflag = True Then
                    AddMessage(vbCrLf & vbCrLf & "The Database file has been successfully" & " copied to the UDD Folder.")
                End If
                bSuccess = True 'File Copied Successfully Flag
                frmUdd.proProgress.Visible = False 'Hide Progress Bar
                Delay(3) 'Allow Operator time to read
                Exit Sub 'Exit, skip rest of Sub
            End If

            '********** Database File will not fit on USB, Will it fit if Zipped? **********
            FindZippedFileSize() 'Find out if Zipped File will fit on USB

            If bZipToADrive = True Then 'If Zipped File fits on USB then no further
                bZipIt = True 'validation is necessary
                Exit Sub 'Exit, skip rest of Sub

                '** Database File will not fit on USB, even if Zipped. --> Launch Appropriate Operation **
            Else

                sArg = CheckImportExportDates() 'Find out which Operation is due

                'Assign Argument and display Caption to Operator
                Select Case sArg

                    'If no Operation Due, Make Export Due.
                    Case NONE
                        sArg = EXPORT
                        'Export Due
                    Case EXPORT
                        'Pass the EXPORT argument to the FHDB_Processor
                        sLabelCaption = EXPORT_DUE & LAUNCH_EXPORTER & UDD_TERMINATE
                        'Import Due
                    Case IMPORT
                        'Pass the IMPORT argument to the FHDB_Processor
                        sLabelCaption = IMPORT_PAST_DUE & PRIOR_TO_UDD & LAUNCH_IMPORTER & UDD_TERMINATE

                End Select

                LaunchFHDB(sArg) 'Launch FHDB Processor
                frmSysPanl.WindowState = FormWindowState.Minimized 'Minimize Menu
                bUnload = True 'Indicates the form is about to Unload
                frmUdd.Hide() 'Abort UDD Operation

            End If

            Exit Sub 'Prevent from falling through Error Handler

        Catch   ' ErrorHandler:
            If Err.Number = 71 Then Exit Sub
            If Err.Number <> 0 Then
                MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
                bSuccess = False
            End If
            Err.Clear()
            ResumeNext()

        End Try
    End Sub


    Public Function CheckForZipFile() As Boolean
        '*****************************************************************************
        '***          Function to find File Extension, True if Zipped              ***
        '***       Checks for which database file type exists on USB, if any    ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        On Error Resume Next 'InLine Eror Handler
        bNoFile = False 'Reset No File Error Flag

        If System.IO.File.Exists(sFileSelfX) Then 'Self Extracting Zip File w/.exe
            CheckForZipFile = True 'Set Flag to indicate Zip file Ext.
        ElseIf System.IO.File.Exists(sFDD_DB) Then            '.MDB File
            CheckForZipFile = False 'Set Flag to indicate not a Zip file Ext.
        Else
            MsgBox("The Database file can not be found on the USB Disk", , "File Error")
            bNoFile = True
        End If

    End Function


    Public Function CheckImportExportDates() As String
        CheckImportExportDates = ""
        '*****************************************************************************
        '***    Calculates Import/Export info. Evaluates data and returns either   ***
        '***    ("IMPORT", "EXPORT", or "NONE") depending on what Operation is     ***
        '***    due. Also Calculates the days left until the next Export is due.   ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        'Declare Function Level Variables
        Dim dtNext_ImportExport As Date 'Calulated Next Operation Due Date
        Dim sReturn As String 'Return Action Indicator
        Dim bImportExport As Boolean 'Flag to ID Import or Export

        Try ' On Error GoTo ErrorHandler

            'Find which operation is Due next -- Is Import Date greater than Export Date?
            If dtIDate.ToOADate > dtXDate.ToOADate Then
                bImportExport = True 'EXPORT
            Else
                bImportExport = False 'IMPORT
            End If

            If bImportExport = True Then 'If Export.
                dtNext_ImportExport = DateAdd("d", nDDI, dtXDate) 'Next Export due date.
                nDaysUntilDue = DateDiff("d", Now, dtNext_ImportExport) 'Days Until Export Due
                nDaysSinceLastExport = DateDiff("d", dtXDate, Now) 'Days since Last Export
                nDaysSinceLastImport = DateDiff("d", dtIDate, Now) 'Days since Last Import

                If Now.ToOADate >= dtNext_ImportExport.ToOADate Then 'Is Export Due Now?
                    'Export Operation is due
                    sReturn = EXPORT
                Else
                    'No operation is Due, Launch Exporter
                    sReturn = NONE
                End If
            Else
                'Import Operation is Due
                sReturn = IMPORT
            End If
            'Return Results
            CheckImportExportDates = sReturn

            Exit Function

        Catch   ' ErrorHandler:

            If CompareErrNumber("<>", 0) Then
                MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
            End If
            Err.Clear()
            ResumeNext()

        End Try
    End Function


    
    Public Function CheckLastRecord(ByVal sDatabase As String) As Integer
        '*****************************************************************************
        '***   Function to Open database, find last record number and return it.   ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        Try ' On Error GoTo ErrLabel
            'Define SQL Query
            sSql = "SELECT * FROM FAULTS ORDER BY Record_Identifier"

            bDBOpened = True 'Set Database Opened Flag
             wrkJet = DAODBEngine_definst.Workspaces(0)
            dbsFHDB = wrkJet.OpenDatabase(sDatabase) 'Open Database
            rstFaults = dbsFHDB.OpenRecordset(sSql, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open Recordset
            'If there are 0-1 records in the Database then don't move last
            If rstFaults.EOF = False Then
                rstFaults.MoveLast() 'Move to last Record
            End If
            CheckLastRecord = rstFaults.Fields("Record_Identifier").Value 'Set Last Record Number value

            CloseDB()

        Catch   ' ErrLabel:
            If CompareErrNumber("<>", 0) Then
                MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
            End If
            CloseDB()
            Err.Clear()

        End Try
    End Function


    Public Sub CloseDB()
        '*****************************************************************************
        '***          Routine to Close the Database connection and unload          ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        Try ' On Error GoTo ErrLabel

            If bDBOpened = True Then
                rstFaults.Close() 'Close DB connection and Release Object from Memory
                dbsFHDB.Close()
                wrkJet.Close()
                bDBOpened = False
            End If

            Exit Sub 'Prevent from dropping through the Error handler

        Catch   ' ErrLabel:
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
            Err.Clear()
            ResumeNext()

        End Try
    End Sub


    Public Sub CopyRestore()
        '*****************************************************************************
        '***         Routine to Initialize the Copy or Restore Operation.          ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        bSuccess = False 'Initialize Success Flag

        '********* Copy Operation *********
        If bZipIt = True Then 'If Zip flag is true then Zip it
            frmUdd.proProgress.Visible = True
            frmUdd.proProgress.Value = 0
            frmUdd.lblTitle.Text = "Zipping Database File"
            frmUdd.Zip_It()
            If bSuccess = True Then 'If Successfully Zipped then
                frmUdd.TestFileIntegrity() 'Verify File Integrity
            End If
            bZipIt = False 'Set Zip flag to False
        Else

            '********* Restore Operation *********
            frmUdd.lblComment.Text = "" 'Setup form
            frmUdd.proProgress.Visible = True
            frmUdd.proProgress.Value = 0
            AddMessage("Retrieving File Information........")
            frmUdd.proProgress.Value = 10 'Update Progress Bar
            bZipped = CheckForZipFile() 'Check for Zip File Extension, Set Zipped Flag
            frmUdd.proProgress.Value = 30 'Update Progress Bar

            If bNoFile = False Then
                If bZipped = True Then
                    frmUdd.RestoreZippedDB() 'Expand zipped file
                    RestoreMostRecentDB() 'Compare files
                Else
                    GetDBFileInfo() 'Retrieve Last record numbers and Date Last Modified
                    frmUdd.proProgress.Value = 100 'Update Progress Bar
                End If
            End If
        End If

    End Sub


    
    Public Function FindPath(ByVal sParsed As String) As String
        FindPath = ""
        '*****************************************************************************
        '***           Parse off file name to find the Directory Path.             ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        
        Dim x As Integer 'Index

        For x = Convert.ToString(sParsed).Length To 1 Step -1
            If Asc(Mid(sParsed, x, 1)) = 92 Then 'chr(92) = \
                Exit For
            End If
        Next x
        FindPath = Strings.Left(sParsed, (x - 1))

    End Function



    Public Sub FindZippedFileSize()
        '*****************************************************************************
        '***    Zips Database to a Temp File then Checks the file size.            ***
        '***    The Temp file is deleted after the file size is found.             ***
        '***                                        Dave Joiner  06/07/2001        ***
        '*****************************************************************************

        frmUdd.lblTitle.Text = "Testing Zipped File Size"
        frmUdd.proProgress.Visible = True 'Display Progress bar
        frmUdd.TestZip_It() 'Create Temp Zip File
        ShowFileSize(sTestItFile) 'Find the size of the Zipped FHDB Database file
        Kill(sTestItFile) 'Delete the Temp file
        frmUdd.proProgress.Visible = False 'Hide Progress bar

    End Sub



    Function GatherIniFileInformation(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherIniFileInformation = ""
        '************************************************************
        '***        Find a value in the VIPERT.INI File             ***
        '************************************************************

        
        Dim lpReturnedString As New StringBuilder(255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        'Clear String Buffer
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo

    End Function


    Public Sub GetDBFileInfo()
        '*****************************************************************************
        '***                     Get Database File Information                     ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        Try ' On Error GoTo ErrLabel

            OpenDB() 'Open Database

            If bDBEmpty = True Then 'If the Database is empty
                nLastRecOnSystem = 0
            Else
                'Get last Record Number from C:\
                nLastRecOnSystem = rstFaults.Fields("Record_Identifier").Value
            End If

            CloseDB() 'Close Database
            frmUdd.proProgress.Value = 50 'Update Progress Bar

            'Get last record number from USB
            sSql = "SELECT * FROM FAULTS ORDER BY Record_Identifier"

            bDBOpened = True
             wrkJet = DAODBEngine_definst.Workspaces(0)
            dbsFHDB = wrkJet.OpenDatabase(sFDD_DB) 'Open Database
            rstFaults = dbsFHDB.OpenRecordset(sSql, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open Recordset

            If rstFaults.BOF Then 'If the Database is empty
                nLastRecOnUSB = 0
            Else
                rstFaults.MoveLast() 'Move to last Record
                nLastRecOnUSB = rstFaults.Fields("Record_Identifier").Value
            End If

            CloseDB()

            frmUdd.proProgress.Value = 70
            'Get Date Last Modified from USB
            dtUSBLastModified = ShowFileAccessInfo(sFDD_DB)
            'Get Date Last Modified from C:\
            dtSystemLastModified = ShowFileAccessInfo(sDBFile)
            frmUdd.proProgress.Value = 80 'Update Progress Bar

            If nLastRecOnUSB < nLastRecOnSystem Then
                'The last Record Number on the USB is less than the System Database
                frmUdd.lblTitle.Text = "No Action Necessary"
                sMsgCaption = (SYS_RECENT & vbCrLf & "The FHDB Database is obsolete and was not installed on System.")

            ElseIf nLastRecOnUSB = nLastRecOnSystem Then
                'The file on the USB is more recent then the System Database
                If dtUSBLastModified.ToOADate > dtSystemLastModified.ToOADate Then
                    frmUdd.lblTitle.Text = ""
                    frmUdd.lblTitle.Text = "Copying Database"
                    sMsgCaption = (UDD_RECENT & vbCrLf & "The FHDB Database has been Restored on the System.")
                    'Copy Database file from USB to C:\ Drive.
                    FileCopy(sFDD_DB, sDBFile)
                    bSuccess = True 'Set Operation Success flag to true

                Else                    'The files are equal, No action is necessary.
                    sTitle = "No Action Necessary"
                    sMsgCaption = (DB_EQUAL & vbCrLf & "The FHDB Database was NOT installed on System.")
                    bSuccess = True 'Set Operation Success flag to true
                    Delay(3)
                End If

            Else
                frmUdd.lblTitle.Text = "Copying Database"
                sMsgCaption = ("Copying Database.............")
                'Copy Database from the USB Drive to the C:\
                FileCopy(sFDD_DB, sDBFile)
                'This case is true when the SYSTEM Database is Obsolete
                sTitle = "Database Copied"
                sMsgCaption = (UDD_RECENT & vbCrLf & "The System Database is obsolete and has been replaced.")
                bSuccess = True 'Set Operation Success flag to true
            End If

            frmUdd.lblComment.Text = "" 'Clear lblComment.Caption
            frmUdd.proProgress.Value = 100 'Update Progress Bar
            frmUdd.proProgress.Visible = False 'Hide Progress Bar
            frmUdd.lblTitle.Text = sTitle
            AddMessage("The last Record Number on the UDD FHDB is: " & nLastRecOnUSB & vbCrLf & "The last Record Number on the System FHDB is: " & nLastRecOnSystem & vbCrLf & sMsgCaption)
            bSuccess = True 'Set Operation Success flag to true

            Delay(5)
            Exit Sub

        Catch   ' ErrLabel:
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
            Err.Clear()
            ResumeNext()

        End Try
    End Sub


    Public Function InitRefFiles() As Boolean
        '*****************************************************************************
        '***                Inialize Reference Files and Variables                 ***
        '***     If FHDB_DATABASE key exists and has a value, return TRUE, else    ***
        '***     return FALSE.                                                     ***
        '***                                        Dave Joiner  02/18/2002        ***
        '*****************************************************************************
        On Error Resume Next
        sBackSlash = Convert.ToString(Chr(92))
        sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")

        If Trim(Len(sDBFile)) > 0 Then
            sDBDir = FindPath(sDBFile)
            sDBTempFileName = sDBDir & sBackSlash & TEMP_FILE
            sSXBinaryFilePath = sDBDir & sBackSlash & BINARY_FILE
            sTestItFile = sDBDir & sBackSlash & TEST_ZIP_FILE
            sFHDBFilePath = GatherIniFileInformation("File Locations", "FHDB_PROCESSOR", "")

            'Get Info from VIPERT.ini file and convert data type
            If Len(GatherIniFileInformation("FHDB", "EXPORT_TIME", "")) > 1 Then
                dtXDate = CDate(GatherIniFileInformation("FHDB", "EXPORT_TIME", ""))
                InitRefFiles = True
            End If
            If Len(GatherIniFileInformation("FHDB", "DDI", "")) > 0 Then
                nDDI = CLng(GatherIniFileInformation("FHDB", "DDI", ""))
                InitRefFiles = True
            End If
            If Len(GatherIniFileInformation("FHDB", "IMPORT_TIME", "")) > 1 Then
                dtIDate = CDate(GatherIniFileInformation("FHDB", "IMPORT_TIME", ""))
                InitRefFiles = True
            End If
        Else
            InitRefFiles = False
        End If

    End Function


    Public Sub LaunchFHDB(ByVal sArg As String)
        '*****************************************************************************
        '***    Gets the FHDB Processor file path, checks for file existance,      ***
        '***    the launches FHDB.exe with the appropreate Argument.               ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        Dim nRetVal As Integer

        On Error Resume Next

        bDBOperationDue = True 'Set Database Operation Due Flag to True
        frmUdd.proProgress.Visible = False 'Hide Progress Bar
        frmUdd.lblTitle.Text = "UDD will Terminate"
        frmUdd.lblComment.Text = sLabelCaption
        frmUdd.lblCountDown.Visible = True 'Show countdown
        'Countdown 5 Seconds then Launch FHDB_Processor with the appropriate Argument.
        ShowCountdown()

        'Set Space Character value
        If Len(sArg) > 0 Then
            sSpace = Convert.ToString(Chr(32)) 'Used as space character for Argument
        Else
            sSpace = "" 'If no Argument then No Space
        End If
        'If file exists, Launch it. If not -- Inform Operator.
        If System.IO.File.Exists(sFHDBFilePath) Then 'If File is found, Launch it
            nRetVal = Shell(sFHDBFilePath & sSpace & sArg, AppWinStyle.NormalNoFocus)
        Else            'If the file can't be found, let the Operator know
            MsgBox("File Not Found: " & sFHDBFilePath, MsgBoxStyle.Critical)
        End If

    End Sub



    Public Sub OpenDB()
        '*****************************************************************************
        '***         Procedure to Open the FHDB using DOA Access Objects.          ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        Dim sSqlAll As String 'SQL Query String

        Try ' On Error GoTo ErrLabel

            bDBEmpty = False
            sSqlAll = "SELECT * FROM " & DB_FAULT_TABLE & " ORDER BY Record_Identifier"
            bDBOpened = True 'Flag Database as Open

            'Open database and fill recordset (Reference: Microsoft DAO 2.5/3.51 Compatibility Library)
             wrkJet = DAODBEngine_definst.Workspaces(0)
            dbsFHDB = wrkJet.OpenDatabase(sDBFile) 'Open Database
            rstFaults = dbsFHDB.OpenRecordset(sSqlAll, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open Recordset
            'If Database is empty -- Exit
            If rstFaults.BOF Then
                bDBEmpty = True
                Exit Sub
            Else
                rstFaults.MoveLast() 'Move to last Record in table
            End If

            Exit Sub 'Prevent program from flowing through Error Handler

            'Error handling routine
        Catch   ' ErrLabel:
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
            Err.Clear()
            ResumeNext()

        End Try
    End Sub



    
    Public Function RemoveFirstLine(ByVal sParsed As String) As String
        RemoveFirstLine = ""
        '*****************************************************************************
        '***    Parse off First line of the contents in lblComents.Caption.        ***
        '***    Return the contents without the first line.                        ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        
        Dim x As Integer 'Index
        Dim iCounter As Short 'Counter

        For x = 1 To Convert.ToString(sParsed).Length Step 1
            If Asc(Mid(sParsed, x, 1)) = 13 Then
                If Asc(Mid(sParsed, (x + 1), 1)) = 10 Then
                    If iCounter > 0 Then
                        Exit For
                    End If
                    iCounter += 1
                End If
            End If
        Next x
        RemoveFirstLine = Mid(sParsed, x, Convert.ToString(sParsed).Length)

    End Function



    Public Sub RestoreMostRecentDB()
        '*****************************************************************************
        '***          Performs a comparison on Last Record Numbers                 ***
        '*****************************************************************************
        '***   Deletes obsolete Database and renames database if necessary.        ***
        '***   When extracting the Zipped file it extracts as FHDB.mdb             ***
        '***   The System File has to be renamed to FHDB_TEMP.mdb prior            ***
        '***   to extracting the Zipped file.                                      ***
        '***   This is necessary in order to keep the System Database intact.      ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        'Get Date Last Date Modified from USB
        dtUSBLastModified = ShowFileAccessInfo(sDBDir & sBackSlash & TEMP_FILE)

        'Get Date Last Date Modified from C:\
        dtSystemLastModified = ShowFileAccessInfo(sDBFile)

        If nLastSystemRecord > nLastRestoredRecord Then

            'If the System Database has a higher last record number than the Restored Database
            Kill(sDBFile)
            'Rename the temp name file back to the original file name
            Rename(sDBTempFileName, sDBFile)
            AddMessage(vbCrLf & vbCrLf & SYS_RECENT & vbCrLf & "The FHDB Database on the UDD is obsolete and was not installed on System.")
            bSuccess = True 'Set Operation Success flag to true

        ElseIf nLastSystemRecord = nLastRestoredRecord Then

            If dtUSBLastModified.ToOADate > dtSystemLastModified.ToOADate Then
                AddMessage(vbCrLf & vbCrLf & UDD_RECENT & vbCrLf & "The FHDB Database has been Restored on the System.")
                bSuccess = True 'Set Operation Success flag to true
            Else                'Both Databases are equal
                AddMessage(vbCrLf & DB_EQUAL & vbCrLf & "The FHDB Database will NOT be Restored to the System.")
                bSuccess = True 'Set Operation Success flag to true
                'Copy FHDB_TEMP.mdb to FHDB.mdb
                FileCopy(sDBTempFileName, sDBFile)
                Kill(sDBTempFileName) 'Delete FHDB_TEMP.mdb
            End If

        Else
            'Keep the Restored file and delete the Temp
            Kill(sDBTempFileName)
            AddMessage(vbCrLf & vbCrLf & UDD_RECENT & vbCrLf & "The FHDB Database has been Restored on the System.")
            bSuccess = True
        End If
        AddMessage(vbCrLf & "Click [Next] to continue.")
    End Sub



    Public Sub ShowCountdown()
        '*****************************************************************************
        '***        Runs a 5 Second Countdown with read out at bottom of form.     ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        Dim iCountSecs As Short 'Number of Seconds left on countdown
        Dim sSeconds As String 'Displays the correct unit: Second/Seconds

        For iCountSecs = 5 To 1 Step -1
            If iCountSecs = 1 Then
                sSeconds = "  Second"
            Else
                sSeconds = "  Seconds"
            End If
            frmUdd.lblCountDown.Text = iCountSecs & sSeconds
            Delay(1)
        Next

    End Sub



    Public Function ShowFileAccessInfo(ByVal filespec As String) As Date
        '*****************************************************************************
        '***        Get the files Last date modified and return date.              ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        
        Dim fi As System.IO.FileInfo = New IO.FileInfo(filespec)

        ShowFileAccessInfo = fi.LastWriteTime

    End Function



    
    Public Sub ShowFileSize(ByVal filespec As String)
        '*****************************************************************************
        '***    Finds the File Size of the FHDB Database and returns Results       ***
        '***    File Size formated in Killo Bytes.                                 ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        
        Dim fi As System.IO.FileInfo = New IO.FileInfo(filespec)

        Try ' On Error GoTo ErrorHandler

            bFileError = Not (fi.Exists)

            'Format Results in Killo Bytes
            nDBSize = FormatNumber(fi.Length / 1024, 0)
            If nADriveFreeSpace > nDBSize Then
                bZipToADrive = True
            Else
                bZipToADrive = False
            End If
            Debug.Print("Database size: " & nDBSize)
            Exit Sub
        Catch   ' ErrorHandler:

            If CompareErrNumber("<>", 0) Then
                MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
                bFileError = True
            End If

        End Try
    End Sub



    
    Public Sub ShowFreeSpace(ByVal drvpath As String)
        '*****************************************************************************
        '***    Finds the Free Space left on the USB Disk.  Results are         ***
        '***    returned formated in Killo Bytes.                                  ***
        '***                                        Dave Joiner  05/24/2001        ***
        '*****************************************************************************

        
        Dim di As System.IO.DriveInfo = New IO.DriveInfo(drvpath)

        Dim sReadyResponse As String 'User's Response to Message Box

        Try ' On Error GoTo ErrorHandler
CheckAgain:

            'Format Results in Killo Bytes
            nADriveFreeSpace = FormatNumber(di.AvailableFreeSpace / 1024, 0)
            Debug.Print("Free space on " & sFDD & ": " & nADriveFreeSpace)
            bCancel = False
            Exit Sub
        Catch   ' ErrorHandler:

            If CompareErrNumber("=", 71) Then 'If the Disk is not ready, allow the Operator to try again
                sReadyResponse = MsgBox("Disk Not Ready" & vbCrLf & "Would you like to try again?", MsgBoxStyle.Information + MsgBoxStyle.RetryCancel, "Disk not Ready")

                If sReadyResponse = 4 Then 'Retry
                    GoTo CheckAgain
                Else                    'Cancel
                    bCancel = True
                    Exit Sub
                End If

            Else
                If CompareErrNumber("<>", 0) Then
                    MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
                End If
            End If
            Debug.Print("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description)
            Err.Clear()
            ResumeNext()

        End Try
    End Sub



    Public Sub WillItFit()
        '*****************************************************************************
        '***    Compare USB's Free Space with Zipped File size.                 ***
        '***    Will the Zipped File fit on the USB Disk?                       ***
        '***                                        Dave Joiner  06/07/2001        ***
        '*****************************************************************************

        'If the Zipped File will fit on the USB, Set Flag to True
        If nDBSize < nADriveFreeSpace Then
            bZipToADrive = True 'Set Zip to A Drive Flag to True
        Else
            bZipToADrive = False
        End If

    End Sub

End Module