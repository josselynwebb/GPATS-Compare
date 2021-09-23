Attribute VB_Name = "ExportRecordasText"
'**********************************************************************
'***    United States Marine Corps                                  ***
'***                                                                ***
'***    Nomenclature:   Module "ExportRecordasText":FHDB_Processor  ***
'***    Written By:     Dave Joiner                                 ***
'***    Purpose:                                                    ***
'***    ECP-TETS-023   05/03/2001  DJoiner                          ***
'***    General Module to support the FHDB's Export Record's as     ***
'***    a text file functionality.                                  ***
'***    Each Record is Formatted with Field Names.                  ***
'***    The Save As Dialog is seeded with the record number or      ***
'***    the First - Last Record Number. Default is Save to A:\.     ***
'***    When saving multiple files, the Operator is informed via    ***
'***    message box when record count is over 15. Modified to       ***
'***    save to a temp file then evaluate size, if file will fit    ***
'***    on destination drive the process is completed, otherwise    ***
'***    the Operator is given a chance to choose another            ***
'***    destination.                                                ***
'***  2.1.0                                                         ***
'***    Added the Constant APSDIR to the file, this is used as for  ***
'***    location of the temperary work location.                    ***
'***    Function SaveAsText                                         ***
'***    Changed how the savefile is set, it uses the variable file  ***
'***    location set by the operator instead of hard coding it to   ***
'***    a drive location.  This allows the flexabilty of change.    ***
'***    Sub ExportRecordSet                                         ***
'***    Change the location of where the temp file is written to, it***
'***    was being written into the Windows directory which should   ***
'***    NEVER be used for this type of useage, this was being done  ***
'***    on the 717, the 657 was already changed to the new location.***
'***    Sub SaveToTemp                                              ***
'***    The procedure was modified as above for the same reason.    ***
'***    Remove the procedures ReturnUserDir or the ReturnWinDir from***
'***    this file, the temp dir is set to aps/data for this system  ***
'**********************************************************************

Option Explicit


Dim sRowHeader(6) As String         'Row Header Array
Dim sField(17) As String            'Field Title Array
Dim iFieldSpace(17) As Integer      'Field Spaces for formating Array
Dim sFileText As String             'Holds the Record/s formatted text to Export
Dim sFieldValue(17) As String       'Field Value Array
Dim bRecordError As Boolean         'Flag to indicate an Error has occured
Dim bExit As Boolean                'Flag to indicate Exit file creation
Dim bSingleFile As Boolean         'Flag to indicate a single record
Dim sFilename As String

Const TEMPFILE As String = "\Temp\TempFile.txt"    'Temporary Text file to evaluate size
Const APSDIR As String = "c:\aps\data"


Sub SetFieldData()
'****************************************************************************************
'***    Load the individual Fields of data into appropriate variable in the array     ***
'***                                                           DJoiner  11/10/2001    ***
'****************************************************************************************
Dim sDegree As String

    On Error GoTo DataError
    
    sDegree = Chr(176)
    sFieldValue(0) = CStr(rstFaults!Record_Identifier)          'Record Number
    sFieldValue(1) = CStr(rstFaults!Start_Time)                 'Start Time
    sFieldValue(2) = CStr(rstFaults!Stop_Time)                  'Stop Time
    sFieldValue(4) = Trim(rstFaults!UUT_Serial_No)              'UUT Serial No
    sFieldValue(5) = Trim(rstFaults!UUT_Rev)                    'UUT Revision
    sFieldValue(6) = Trim(rstFaults!TPCCN)                      'TPCCN
    sFieldValue(7) = Trim(rstFaults!ERO)                        'ERO
    sFieldValue(8) = Trim(rstFaults!ID_Serial_No)               'ID Serial No
    sFieldValue(9) = Trim(rstFaults!Failure_Step)               'Failure Step
    
    If rstFaults!Test_Status Then                               'Test Status"
        sFieldValue(10) = "PASSED"
    Else
        sFieldValue(10) = "FAILED"
    End If
    
    sFieldValue(11) = Trim(rstFaults!Dimension)                 'Dimension
    sFieldValue(12) = CStr(rstFaults!Lower_Limit)               'Lower Limit
    sFieldValue(13) = CStr(rstFaults!Meas_Value)                'Measurement
    sFieldValue(14) = CStr(rstFaults!Upper_Limit)               'Upper Limit
    sFieldValue(3) = CStr(rstFaults!Temperature) & sDegree & " C"         'Temperature
    sFieldValue(15) = rstFaults!Fault_Callout                             'Fault Callout
    sFieldValue(16) = StripLeadingReturns(rstFaults!Operator_Comments)    'Operator Comments
    Exit Sub
    
DataError:
    'Notify Operator an Error has occured and describe
    MsgBox Err.Number & ":  " & Err.Description, vbExclamation, "ERROR"
    bRecordError = True     'Set Flag to indicate an Error has Occured
    Exit Sub
   
End Sub


Public Sub FindFieldSpaces()
'****************************************************************************************
'***    Sets the Field Space values used in Formatting the Record                     ***
'***                                                           DJoiner  11/10/2001    ***
'****************************************************************************************

Dim iFieldLen(17) As Integer        'Length of field Array
Dim x As Integer                    'Index

    For x = 0 To 16
            iFieldLen(x) = Len(sFieldValue(x))
    
        Select Case x
        
            Case 0, 3, 4, 5, 6, 7, 8, 9, 10, 11, 14
                iFieldSpace(x) = 20 - iFieldLen(x)          '20 Spaces
                        
            Case 1
                iFieldSpace(x) = 30 - iFieldLen(x)          '30 Spaces
                
            Case 2
                iFieldSpace(x) = 31 - iFieldLen(x)          '31 Spaces
                
            Case 12, 13
                iFieldSpace(x) = 25 - iFieldLen(x)          '25 Spaces
                            
            Case Else
                iFieldSpace(x) = 0                          '0 Spaces
                
            End Select
    Next

End Sub


Public Function RecordHeader() As String
'****************************************************************************************
'***    Builds and Returns the Record Header with the Record Number                   ***
'***    to Indentify Record.                                                          ***
'***                                                           DJoiner  11/10/2001    ***
'****************************************************************************************

Dim sReturnString As String     'String value to Return
Dim iRecLen As Integer          'Length of the Record Number
Dim iHead As Integer            'Length to make leading "*"
Dim iTail As Integer            'Length to make Trailing "*"
Dim x As Integer                'Index
   
    sReturnString = Space(31) & sField(0) & vbCrLf      'First line
        
    iRecLen = Len(sFieldValue(0))

    iHead = 37.5 - (iRecLen / 2) - 2
    iTail = 75 - (iHead + iRecLen + 2)

    For x = 1 To iHead
        sReturnString = sReturnString & "*"
    Next
        sReturnString = sReturnString & "  " & CStr(sFieldValue(0)) & "  "
        
    For x = 1 To iTail
        sReturnString = sReturnString & "*"
    Next
    RecordHeader = sReturnString & vbCrLf
    Debug.Print RecordHeader
End Function


Private Sub InitializeFields()
'****************************************************************************************
'***    Initialize Tiltle Field Values and Builds Headers                             ***
'***                                                           DJoiner  11/10/2001    ***
'****************************************************************************************

    sField(0) = "Record Number"
    sField(1) = "Start Time"
    sField(2) = "Stop Time"
    sField(4) = "UUT Serial No"
    sField(5) = "UUT Revision"
    sField(6) = "TPCCN"
    sField(7) = "ERO"
    sField(8) = "ID Serial No"
    sField(9) = "Failure Step"
    sField(10) = "Test Status"
    sField(11) = "Dimension"
    sField(12) = "Lower Limit"
    sField(13) = "Measurement"
    sField(14) = "Upper Limit"
    sField(3) = "Temperature"
    sField(15) = "Fault Callout:"
    sField(16) = "Operator Comments:"
    
        
    sRowHeader(0) = sField(1) & Space(20) & sField(2) & Space(22) & sField(3)
    sRowHeader(1) = vbCrLf & sField(4) & Space(7) & sField(5) & Space(8) _
                           & sField(6) & Space(15) & sField(7)
    sRowHeader(2) = vbCrLf & sField(8) & Space(8) & sField(9) & Space(8) _
                           & sField(10) & Space(9) & sField(11)
    sRowHeader(3) = vbCrLf & Space(5) & sField(12) & Space(14) & sField(13) & Space(14) & sField(14)
    sRowHeader(4) = vbCrLf & sField(15)
    sRowHeader(5) = vbCrLf & sField(16)
        
 End Sub
 

Public Sub FormatRecord()
'****************************************************************************************
'***    Procedure to Format each Record for easier readability in a Text File.        ***
'***                                                           DJoiner  11/10/2001    ***
'****************************************************************************************

Dim iFieldLen(17) As Integer        'Length of Field value Array
Dim iRow As Integer                 'Row Index
Dim iRowEnd(6) As Integer           'Index to signal end of row Array
Dim iField As Integer               'Index

    'Define the rows ending Field array number
    iRowEnd(0) = 4
    iRowEnd(1) = 8
    iRowEnd(2) = 12
    iRowEnd(3) = 15
    iRowEnd(4) = 16
    iRowEnd(5) = 17

    iField = 1      'Initalize Field Array number
    
    For iRow = 0 To 5
        sFileText = sFileText & sRowHeader(iRow) & vbCrLf
        Debug.Print sFileText
        Do Until iField = iRowEnd(iRow)
            If iField = 12 Then sFileText = sFileText & Space(5)
            sFileText = sFileText & sFieldValue(iField) & Space(iFieldSpace(iField))
            iField = iField + 1
            Debug.Print sFileText
        Loop
        sFileText = sFileText & vbCrLf
    Next

End Sub


Function SaveAsText(sFileNameSeed As String) As Long
'****************************************************************************************
'***    Procedure recieves a File Name to Seed the "Save As" File Name                ***
'***    in the Common Dialog Control.                                                 ***
'***    It checks for an Empty RecordSet as well as Warning the Operator              ***
'***    before OverWriting a File.                                                    ***
'***                                                           DJoiner  11/10/2001    ***
'****************************************************************************************

Dim iResponse As Integer        'Operator's response to Message Box

    'If there are no records to be Saved, Inform Operator and Exit Sub
    If rstFaults.RecordCount < 1 Then
        MsgBox "There is no current record in the RecordSet", , "RecordSet Error"
        Exit Function
    End If
    
  ' Set CancelError is True
  frmViewer.SaveFile.CancelError = True
  On Error GoTo ErrHandler
    Call StatusMsg("Select file name and desination to export text file.")
    frmViewer.SaveFile.FileName = sFileLocation & sFileNameSeed & ".txt"
    frmViewer.SaveFile.InitDir = sFileLocation
    frmViewer.SaveFile.DefaultExt = ".txt"
    frmViewer.SaveFile.Filter = ".txt"
    frmViewer.SaveFile.ShowSave
    
    If FileExists(frmViewer.SaveFile.FileName) Then
        iResponse = MsgBox(frmViewer.SaveFile.FileName & " already exists." & vbCrLf _
                    & "Overwrite?", vbExclamation + vbYesNo, "File Exists")
            If iResponse = vbNo Then
                bExit = True
                Exit Function
            End If
    End If
    If bSingleFile Then
        Call StatusMsg("Saving text file...............")
        Screen.MousePointer = vbHourglass
        DoEvents
        Open frmViewer.SaveFile.FileName For Output As #1
        bSingleFile = False
    Else
        sFilename = frmViewer.SaveFile.FileName
        SaveAsText = ShowFreeSpace(Left(frmViewer.SaveFile.FileName, 3))
    End If
    Exit Function
    
ErrHandler:

    Debug.Print Err.Description
    'User pressed the Cancel button
    bExit = True
    Exit Function
End Function


Public Sub ExportRecord()
'****************************************************************************************
'***    Exports a single Record to a Text File.                                       ***
'***                                                           DJoiner  12/24/2001    ***
'****************************************************************************************

    bSingleFile = True          'Set single record flag to true
    bExit = False
    'If there are no records in the RecordSet, Inform Operator, DO NOT Save
    If rstFaults.RecordCount < 1 Then
        MsgBox "There is no current record in the RecordSet", , "RecordSet Error"
        Exit Sub
    End If
    
    sFileText = ""                  'Initialize File Text
    Call InitializeFields
    Call SetFieldData               'Get the Data from the record Fields
    If bRecordError Then Exit Sub   'If a DOA Record Error Occurs, Exit Sub
    Call FindFieldSpaces            'Get the Field Spaces for Formating
    sFileText = RecordHeader        'Create Record Header
    Call FormatRecord                 'Format the Record
    Call SaveAsText(sFieldValue(0)) 'Launch SaveFile Dialog - Seed with Current Record Number
    If Not bExit Then
        Print #1, sFileText
        Close #1                                'Close File
    End If
    bSingleFile = False         'Reset single record flag
    Screen.MousePointer = vbDefault
    Call StatusMsg("Record Display Area.")
End Sub


Public Sub ExportRecordSet()
'****************************************************************************************
'***   Procedure to Export Multiple Records to a Text File                            ***
'***   BookMarks Current Record, Moves curser to First Record in RecordSet            ***
'***   Builds String from Record, Moves to Next Record and add contents to String.    ***
'***   File is built one record at a time to a temp file for sizr evaluation.         ***
'***   Resets Display with current RecordSet, displaying Bookmarked Record.           ***
'***                                                           DJoiner  12/24/2001    ***
'****************************************************************************************

Dim iCounter As Integer         'Progress Bar Counter
Dim iIncrement As Integer       'Progress Bar increment per record
Dim nFileSize As Long           'File of Temp File
Dim nFreeSpace As Long          'Free Space on destination Dive Disk
Dim iResponse As Integer        'Message Box Response

    bExit = False
    sFileText = ""
On Error GoTo FileErr
    
    nBookMark = frmViewer.txtData(0).Text       'BookMark Current Record
    rstFaults.MoveFirst                         'Move to first record in RecordSet
    Call InitializeFields                       'Initialize Field Values
    Screen.MousePointer = vbHourglass
    
    If Not bExit Then
        nFileSize = SaveToTemp
        nFreeSpace = SaveAsText(GetFileNameSeed)
        If WillItFit(nFileSize, nFreeSpace) Then
            Screen.MousePointer = vbHourglass
            Call StatusMsg("Saving text file...............")
            DoEvents
            FileCopy APSDIR & TEMPFILE, sFilename
            Kill APSDIR & TEMPFILE
            Call StatusMsg("File Saved.")
        Else
            Call StatusMsg("The Text File is too large for the destination, choose another location.")
            iResponse = MsgBox("It won't fit, Select another location?", vbYesNo, "Will Not Fit")
                If iResponse = vbNo Then
                    Kill APSDIR & TEMPFILE
                Else
                'Allow operator to select another location.
                nFreeSpace = SaveAsText(GetFileNameSeed)
                End If
        End If
        Call StatusMsg("Record Display Area.")

    End If
 
    rstFaults.Requery                               'Requery RecordSet
    Do Until rstFaults!Record_Identifier = nBookMark    'Move Curser to BookMarked Record
        rstFaults.MoveNext
    Loop
    Load_Record_Data                            'Display Bookmarked Record
    Screen.MousePointer = vbDefault
    If FileExists(APSDIR & TEMPFILE) Then Kill APSDIR & TEMPFILE
    Exit Sub
    
FileErr:
    MsgBox "Error Number: " & Err.Number & vbCrLf _
        & "Error Description: " & Err.Description, , "Error"
    frmBackground.PBarFile.Visible = False
    Screen.MousePointer = vbDefault
End Sub


Public Function GetFileNameSeed() As String
'****************************************************************************************
'***    Function to build file name seed when Exporting Multiple Documents            ***
'***    Return= (First Record Number) - (Last Record Number) in ResultSet             ***
'***                                                           DJoiner  11/10/2001    ***
'****************************************************************************************

Dim sFirst As String        'First Record Number as a String
Dim sSecond As String       'Second Record Number as String

    rstFaults.MoveFirst
    sFirst = CStr(rstFaults!Record_Identifier)      'Get First Record Number in RecordSet
    rstFaults.MoveLast
    sSecond = CStr(rstFaults!Record_Identifier)     'Get Last Record Number in RecordSet
    
    GetFileNameSeed = sFirst & "-" & sSecond     'Build File Name to Seed SaveFile Dialog with

End Function


Public Function StripLeadingReturns(sText As String) As String
'****************************************************************************************
'***    Function to Strip any leading Return Characters                               ***
'***                                                                                  ***
'***    sText = String to Strip leading characters from                               ***
'***    Return = String with out leading Return characters                            ***
'***                                                           DJoiner  11/10/2001    ***
'****************************************************************************************

Dim sTempText As String     'Temporary Text String
Dim x As Integer            'Starting point for vbMid() Function

    'First Strip off any leading Return Chracters
        For x = 1 To Len(sText)
            If Asc(Mid$(sText, x, 1)) = 13 Or Asc(Mid$(sText, x, 1)) = 10 Then
                'Rebuild Text String
                sTempText = Trim((Mid$(sText, x + 1, Len(sText))))
            Else
                Exit For
            End If
        Next
        StripLeadingReturns = sTempText
        
End Function


Function ShowFileSize(filespec) As Long
'*****************************************************************************
'***    Finds the File Size of the text file and returns Results           ***
'***    File Size formatted in Killo Bytes.                                ***
'***                                        Dave Joiner  12/24/2001        ***
'*****************************************************************************

Dim fs, f
Dim bFileError As Boolean

On Error GoTo ErrorHandler

    Set fs = CreateObject("Scripting.FileSystemObject")
    Set f = fs.GetFile(filespec)
    
    'Format Results in Killo Bytes
    ShowFileSize = FormatNumber(f.Size / 1024, 0)
    Exit Function
ErrorHandler:

    If Err.Number <> 0 Then
        MsgBox "Error Number: " & Err.Number & vbCrLf _
            & "Description: " & Err.Description, vbExclamation, "ERROR"
        bFileError = True
    End If
    
End Function


Function ShowFreeSpace(drvpath) As Long
'*****************************************************************************
'***    Finds the Free Space left on the selected Drive Disk.  Size        ***
'***    is  returned formatted in Killo Bytes.                             ***
'***                                        Dave Joiner  12/24/2001        ***
'*****************************************************************************

Dim fs, d
Dim sReadyResponse As String            'Operator's response from message box

On Error GoTo ErrorHandler
CheckAgain:
    Set fs = CreateObject("Scripting.FileSystemObject")
    Set d = fs.GetDrive(fs.GetDriveName(drvpath))

    'Format Results in Killo Bytes
    ShowFreeSpace = FormatNumber(d.FreeSpace / 1024, 0)
    Exit Function
    
ErrorHandler:

    If Err.Number = 71 Then     'If the Disk is not ready, allow the Operator to try again
        sReadyResponse = MsgBox("Disk Not Ready" & vbCrLf _
            & "Would you like to try again?", vbInformation + vbRetryCancel, "Disk not Ready")
    
        If sReadyResponse = 4 Then          'Retry
            GoTo CheckAgain
        Else                                'Cancel
            Exit Function
        End If
            
    Else
        If Err.Number <> 0 Then
            MsgBox "Error Number: " & Err.Number & vbCrLf _
                & "Description: " & Err.Description, vbExclamation, "ERROR"
        End If
    End If
    Debug.Print "Error Number: " & Err.Number & vbCrLf _
                & "Description: " & Err.Description
    Err.Clear
    Resume Next

End Function


Function WillItFit(nTempFile As Long, nFreeSpace As Long) As Boolean
'*****************************************************************************
'***    Compare destination's Free Space with the Text File size.          ***
'***    Return "True" if it will fit and "False" if not.                   ***
'***                                        Dave Joiner  12/24/2001        ***
'*****************************************************************************

    'If the Temp File will fit on the Selected Disk, Set Flag to True
    If nTempFile < nFreeSpace Then
        WillItFit = True        'Set Flag to True
    Else
        WillItFit = False        'Set Flag to False
    End If
    
End Function


Public Function SaveToTemp() As Long
'*****************************************************************************
'***    Saves the selected records to a temporary text file. Returns the   ***
'***    the file size in Killo Bytes.                                      ***
'***                                        Dave Joiner  12/24/2001        ***
'*****************************************************************************

Dim sngCounter As Single         'Progress Bar Counter
Dim sngIncrement As Single       'Progress Bar increment per record

    Open APSDIR & TEMPFILE For Output As #1
    
    rstFaults.MoveFirst                         'Move to first record in RecordSet
        frmBackground.PBarFile.Visible = True
        frmBackground.PBarFile.Value = sngCounter
        sngIncrement = (100 / rstFaults.RecordCount)
        DoEvents
        Do Until rstFaults.EOF                      'Iterate through the RecordSet
            sngCounter = sngCounter + sngIncrement
            frmBackground.PBarFile.Value = sngCounter
            DoEvents
            sFileText = ""
            Call SetFieldData                       'Get the Data from the record Fields
            If bRecordError Then Exit Function      'If a DOA Record Error Occurs, Exit Sub
            sFileText = sFileText & RecordHeader    'Create Record Header
            Call FindFieldSpaces                    'Get the Field Spaces for Formating
            Call FormatRecord                         'Format the Record
            sFileText = sFileText & vbCrLf & vbCrLf       'Add Blank rows between Records
            rstFaults.MoveNext                      'Move to the Next Record in the RecordSet
            Print #1, sFileText
        Loop
        frmBackground.PBarFile.Value = 100
        Close #1                                'Close File
        frmBackground.PBarFile.Visible = False
    
    Screen.MousePointer = vbDefault
    rstFaults.Requery                           'Requery RecordSet
    Do Until rstFaults!Record_Identifier = nBookMark    'Move Curser to BookMarked Record
        rstFaults.MoveNext
    Loop
    Close #1
    SaveToTemp = ShowFileSize(APSDIR & TEMPFILE)
    
End Function

