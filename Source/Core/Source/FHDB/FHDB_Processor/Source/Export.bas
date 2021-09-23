Attribute VB_Name = "m_Export"
'**********************************************************************
'***    United States Marine Corps                                  ***
'***                                                                ***
'***    Nomenclature:   Module "m_Export" : FHDB_Processor          ***
'***    Written By:     Dave Joiner                                 ***
'***    Purpose:                                                    ***
'***  ECP-TETS-023                                                  ***
'***    General Module to support the Export Form.                  ***
'***    The Exporter will allow the Operator to Export              ***
'***    Files from the FHDB. Both the Faults Table and the          ***
'***    System Table are Exported as a Zipped version of the        ***
'***    FHBD.MDB File.(EXPORT.zip) The Zipped file will span        ***
'***    multiple disks if size dictates.                            ***
'***    The System Table contains only one record that is either    ***
'***    created or updated immediately pior to Export.              ***
'***  2.1.0                                                         ***
'***    Deleted Public dtCalDue As Date line in the header area of  ***
'***    this file, on the 657 it was commented out anyway, it is not***
'***    being used by the program.                                  ***
'***    Changed all references to either TETS.INI or VIPERT.ini in  ***
'***    any comments to Tester.ini                                  ***
'***    Sub UpdateSystemRecord()                                    ***
'***    Deleted the variable sControllerSN, added the variable      ***
'***    sAteType.  The serial number of the controller isn't used   ***
'***    for any data mining what so ever, however the Ate family is ***
'***    of importance for data mining and this replaces it.         ***
'***    Added the line to get the AteType from the ini file and     ***
'***    deleted the reference to the Controller serial number.      ***
'***    Added the verification code of the AteType value to the     ***
'***    procedure.                                                  ***
'***    Changed the message to display Tester.ini now instead of    ***
'***    either TETS.ini or VIPERT.ini.                              ***
'**********************************************************************

Option Explicit

'Dimension Public Variables
Public sDatabase As String              'Drive/Path/File Name of the Database
Public sExportFile As String            'Export File Drive/Path/Name
Public bSystemRecordError As Boolean    'Flag to indicate the ATE Serial Number is invalid
Public bDiskError As Boolean            'Flag to indicate the device is not in the USB Drive


Public Sub InitializeExportFields()
'**********************************************************************
'***             Initializes the File Export Fields.                ***
'**********************************************************************
  
    frmExport.txtZipFilename.Text = sExportFile
    frmExport.txtFilesToProcess.Text = sDatabase
    frmExport.lstResults.ListItems.Clear           'Clear the Results List Field
    
End Sub
                      

Public Sub SetDefaultProperties(XZip As XceedZip)
'**********************************************************************
'***      Sets XceedZip instance with default property values.      ***
'**********************************************************************

    XZip.BasePath = ""
    XZip.CompressionLevel = xclHigh
    XZip.EncryptionPassword = ""
    XZip.RequiredFileAttributes = xfaNone
    XZip.ExcludedFileAttributes = xfaFolder + xfaVolume
    XZip.FilesToProcess = ""
    XZip.FilesToExclude = ""
    XZip.MinDateToProcess = DateSerial(1900, 1, 1)
    XZip.MaxDateToProcess = DateSerial(9999, 12, 31)
    XZip.MinSizeToProcess = 0
    XZip.MaxSizeToProcess = 0      ' Zero means no upper limit
    XZip.SplitSize = 0             ' Zero means no split
    XZip.PreservePaths = False
    XZip.ProcessSubfolders = False
    XZip.SkipIfExisting = False
    XZip.SkipIfNotExisting = False
    XZip.SkipIfOlderDate = False
    XZip.SkipIfOlderVersion = False
    XZip.TempFolder = ""            'Empty means default Windows temp folder
    XZip.UseTempFile = True
    XZip.UnzipToFolder = ""
    XZip.ZipFilename = ""
    XZip.SpanMultipleDisks = xdsRemovableDrivesOnly
    XZip.ExtraHeaders = xehNone
    frmExport.lstResults.ListItems.Clear
      
End Sub


Sub UpdateSystemRecord()
'**********************************************************************
'*** Procedure to either create or update System Information Record ***
'**********************************************************************

Dim sSysSN As String                'ATE Serial Number from the Tester's INI file
Dim sAteType As String              'String value for the ATE family of tester
Dim sSSV As String                  'Software Revison Value from the Tester's INI file
Dim sCalDue As String               'String value for the Date Effective
Dim iRecCount As Integer            'Total number of Records Returned from Query
Dim slpBuffer As String * 255       '255 character Buffer for Computer Name
Dim nRetVal As Long                 'Return value for Computer Name

On Error GoTo ErrLabel

    bSystemRecordError = False  'Reset System Record Error flag
    
    'Get Info from Tester's INI file
    sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
    sSysSN = GatherIniFileInformation("System Startup", "SN", "")
    sSSV = GatherIniFileInformation("System Startup", "SWR", "")
    sAteType = GatherIniFileInformation("System Startup", "ATE_TYPE", "")
    sCalDue = GatherIniFileInformation("Calibration", "SYSTEM_EFFECTIVE", "")
    
    bDBOpened = True
    Set wrkJet = Workspaces(0)                                                 'Open Workspace
    Set dbsFHDB = wrkJet.OpenDatabase(sDBFile)                                 'Open Database
    Set rstFaults = dbsFHDB.OpenRecordset(DB_SYSTEM_TABLE, dbOpenDynaset)      'Open Recordset
  
    rstFaults.MoveLast                          'Move to last Record
    iRecCount = rstFaults.RecordCount
    rstFaults.MoveFirst
    'Limit SYSTEM Table to only one record
    If iRecCount = 0 Then                       'If no records, Create New record
        rstFaults.AddNew
    Else
        rstFaults.Edit                          'If Record exists, Edit it
    End If
    
    If Len(sCalDue) > 4 Then                'If > 4 Digits, it is proberly a date
        If IsNumeric(sCalDue) Then          'Check to see if value is Numeric
            If Len(sCalDue) > 18 Then       'If Field value Length is > 18, Truncate
                sCalDue = Mid(sCalDue, 1, 18)
            End If
        Else
            sCalDue = "UNKNOWN"             'If there is no Date in Tester's INI
        End If                              'Assign Default of "UNKNOWN"
    Else
        sCalDue = "UNKNOWN"                 'If there is no Date in Tester's INI
    End If                                  'Assign Default of "UNKNOWN"
            
'   Validate ATE family type
    If IsNumeric(sAteType) Then
        rstFaults!ATE_Identifer = sAteType
    End If
    
    'Validate System Serial Number
    If IsNumeric(sSysSN) Then
        rstFaults!ATE_Serial = sSysSN
    Else
        'Inform the Operator that the ATE Serial Number Invalid and terminate Operation.
        MsgBox "The ATE Serial number referenced in the Tester's INI file is invalid." & vbCrLf _
            & "Please update the Proper Key in the Tester's INI file with a valid value." & vbCrLf _
            & "The ATE Serial Number: " & sSysSN & " is invalid." & vbCrLf _
            & "This Operation will be terminated.", vbCritical, "ATE Serial Number Invalid"
        bSystemRecordError = True
    End If
        
    'Assign Date to FHDB fields
    rstFaults!Sys_Software_Ver = sSSV
    rstFaults!Cal_Due_Date = sCalDue
    
    rstFaults.Update                                'Update Data in FHDB

    Call UpdateIniFile("FHDB", "LASTCHECKED", "")   'Update Last time checked key
        
    Call CloseDB
    Exit Sub                    'To prevent from falling through Error Handler
    
ErrLabel:
    If Err.Number = 3021 Then       'If No Current Record, ignore
        Resume Next
    Else
        MsgBox Err.Description, vbExclamation, "ERROR"
        Err.Clear
        Resume Next
   End If
   
End Sub


