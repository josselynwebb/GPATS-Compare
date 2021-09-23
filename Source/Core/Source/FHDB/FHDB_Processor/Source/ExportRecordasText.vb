Option Strict Off
Option Explicit On
Module ExportRecordasText
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
	
	
	
	Dim sRowHeader(6) As String 'Row Header Array
	Dim sField(17) As String 'Field Title Array
	Dim iFieldSpace(17) As Short 'Field Spaces for formating Array
	Dim sFileText As String 'Holds the Record/s formatted text to Export
    Dim sFieldValue(18) As String 'Field Value Array
	Dim bRecordError As Boolean 'Flag to indicate an Error has occured
    Dim bExit As Boolean 'Flag to indicate Exit file creation
    Dim bCancel As Boolean 'Flag to indicate operation canceled
	Dim bSingleFile As Boolean 'Flag to indicate a single record
	Dim sFilename As String
	
    Const TEMPFILE As String = "\TempFile.txt" 'Temporary Text file to evaluate size
	Const APSDIR As String = "c:\aps\data"
	
	
	Sub SetFieldData()
		'****************************************************************************************
		'***    Load the individual Fields of data into appropriate variable in the array     ***
		'***                                                           DJoiner  11/10/2001    ***
		'****************************************************************************************
		Dim sDegree As String
		
        'On Error GoTo DataError
		
        sDegree = Chr(176)

        sFieldValue(0) = CStr(rstFaults.Fields("Record_Identifier").Value) 'Record Number

        If (IsDate(rstFaults.Fields("Start_Time").Value)) Then
            sFieldValue(1) = CStr(rstFaults.Fields("Start_Time").Value) 'Start Time
        Else
            sFieldValue(1) = ""
        End If

        If (IsDate(rstFaults.Fields("Stop_Time").Value)) Then
            sFieldValue(2) = CStr(rstFaults.Fields("Stop_Time").Value) 'Stop Time
        Else
            sFieldValue(2) = ""
        End If

        sFieldValue(3) = CStr(rstFaults.Fields("Temperature").Value) & sDegree & " C" 'Temperature
        Try
            sFieldValue(4) = Trim(rstFaults.Fields("UUT_Serial_No").Value) 'UUT Serial No
        Catch ex As Exception
            sFieldValue(4) = ""
        End Try

        Try
            sFieldValue(5) = Trim(rstFaults.Fields("UUT_Rev").Value) 'UUT Revision
        Catch ex As Exception
            sFieldValue(5) = ""
        End Try

        Try
            sFieldValue(6) = Trim(rstFaults.Fields("TPCCN").Value) 'TPCCN
        Catch ex As Exception
            sFieldValue(6) = ""
        End Try

        Try
            sFieldValue(7) = Trim(rstFaults.Fields("ERO").Value) 'ERO
        Catch ex As Exception
            sFieldValue(7) = ""
        End Try

        Try
            sFieldValue(8) = Trim(rstFaults.Fields("ID_Serial_No").Value) 'ID Serial No
        Catch ex As Exception
            sFieldValue(8) = ""
        End Try

        Try
            sFieldValue(9) = Trim(rstFaults.Fields("Failure_Step").Value) 'Failure Step
        Catch ex As Exception
            sFieldValue(9) = ""
        End Try

        If rstFaults.Fields("Test_Status").Value Then 'Test Status"
            sFieldValue(10) = "PASSED"
        Else
            sFieldValue(10) = "FAILED"
        End If


        Try
            sFieldValue(11) = Trim(rstFaults.Fields("Dimension").Value) 'Dimension
        Catch ex As Exception
            sFieldValue(11) = "FAILED"
        End Try

        Try
            sFieldValue(12) = CStr(rstFaults.Fields("Lower_Limit").Value) 'Lower Limit
        Catch ex As Exception
            sFieldValue(12) = "FAILED"
        End Try

        Try
            sFieldValue(13) = CStr(rstFaults.Fields("Meas_Value").Value) 'Measurement
        Catch ex As Exception
            sFieldValue(13) = "FAILED"
        End Try

        Try
            sFieldValue(14) = CStr(rstFaults.Fields("Upper_Limit").Value) 'Upper Limit
        Catch ex As Exception
            sFieldValue(14) = "FAILED"
        End Try

        Try
            sFieldValue(15) = rstFaults.Fields("Fault_Callout").Value 'Fault Callout
        Catch ex As Exception
            sFieldValue(15) = "FAILED"
        End Try

        Try
            sFieldValue(16) = rstFaults.Fields("Operator_Comments").Value 'Operator Comments
        Catch ex As Exception
            sFieldValue(16) = "FAILED"
        End Try

        Exit Sub

DataError:
        'Notify Operator an Error has occured and describe
        MsgBox(Err.Number & ":  " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
        bRecordError = True 'Set Flag to indicate an Error has Occured
        Exit Sub

    End Sub
	
	
	Public Sub FindFieldSpaces()
		'****************************************************************************************
		'***    Sets the Field Space values used in Formatting the Record                     ***
		'***                                                           DJoiner  11/10/2001    ***
		'****************************************************************************************
		
		Dim iFieldLen(17) As Short 'Length of field Array
		Dim x As Short 'Index
		
		For x = 0 To 16
			iFieldLen(x) = Len(sFieldValue(x))
			
			Select Case x
				
				Case 0, 3, 4, 5, 6, 7, 8, 9, 10, 11, 14
					iFieldSpace(x) = 20 - iFieldLen(x) '20 Spaces
					
				Case 1
					iFieldSpace(x) = 30 - iFieldLen(x) '30 Spaces
					
				Case 2
					iFieldSpace(x) = 31 - iFieldLen(x) '31 Spaces
					
				Case 12, 13
					iFieldSpace(x) = 25 - iFieldLen(x) '25 Spaces
					
				Case Else
					iFieldSpace(x) = 0 '0 Spaces
					
			End Select
		Next 
		
	End Sub
	
	
	Public Function RecordHeader() As String
		'****************************************************************************************
		'***    Builds and Returns the Record Header with the Record Number                   ***
		'***    to Indentify Record.                                                          ***
		'***                                                           DJoiner  11/10/2001    ***
		'****************************************************************************************
		
		Dim sReturnString As String 'String value to Return
		Dim iRecLen As Short 'Length of the Record Number
		Dim iHead As Short 'Length to make leading "*"
		Dim iTail As Short 'Length to make Trailing "*"
		Dim x As Short 'Index
		
		sReturnString = Space(31) & sField(0) & vbCrLf 'First line
		
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
		Debug.Print(RecordHeader)
	End Function
	
	
	Private Sub InitializeFields()
		'****************************************************************************************
		'***    Initialize Tiltle Field Values and Builds Headers                             ***
		'***                                                           DJoiner  11/10/2001    ***
		'****************************************************************************************
		
		sField(0) = "Record Number"
		sField(1) = "Start Time"
		sField(2) = "Stop Time"
        sField(3) = "Temperature"
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
		sField(15) = "Fault Callout:"
		sField(16) = "Operator Comments:"
		
		
		sRowHeader(0) = sField(1) & Space(20) & sField(2) & Space(22) & sField(3)
		sRowHeader(1) = vbCrLf & sField(4) & Space(7) & sField(5) & Space(8) & sField(6) & Space(15) & sField(7)
		sRowHeader(2) = vbCrLf & sField(8) & Space(8) & sField(9) & Space(8) & sField(10) & Space(9) & sField(11)
		sRowHeader(3) = vbCrLf & Space(5) & sField(12) & Space(14) & sField(13) & Space(14) & sField(14)
		sRowHeader(4) = vbCrLf & sField(15)
		sRowHeader(5) = vbCrLf & sField(16)
		
	End Sub
	
	
	Public Sub FormatRecord()
		'****************************************************************************************
		'***    Procedure to Format each Record for easier readability in a Text File.        ***
		'***                                                           DJoiner  11/10/2001    ***
		'****************************************************************************************
		
		Dim iFieldLen(17) As Short 'Length of Field value Array
		Dim iRow As Short 'Row Index
		Dim iRowEnd(6) As Short 'Index to signal end of row Array
		Dim iField As Short 'Index
		
		'Define the rows ending Field array number
		iRowEnd(0) = 4
		iRowEnd(1) = 8
		iRowEnd(2) = 12
		iRowEnd(3) = 15
		iRowEnd(4) = 16
		iRowEnd(5) = 17
		
		iField = 1 'Initalize Field Array number
		
		For iRow = 0 To 5
			sFileText = sFileText & sRowHeader(iRow) & vbCrLf
			Debug.Print(sFileText)
			Do Until iField = iRowEnd(iRow)
				If iField = 12 Then sFileText = sFileText & Space(5)
				sFileText = sFileText & sFieldValue(iField) & Space(iFieldSpace(iField))
				iField = iField + 1
				Debug.Print(sFileText)
			Loop 
			sFileText = sFileText & vbCrLf
		Next 
		
	End Sub
	
	
	Function SaveAsText(ByRef sFileNameSeed As String) As Integer
		'****************************************************************************************
		'***    Procedure recieves a File Name to Seed the "Save As" File Name                ***
		'***    in the Common Dialog Control.                                                 ***
		'***    It checks for an Empty RecordSet as well as Warning the Operator              ***
		'***    before OverWriting a File.                                                    ***
		'***                                                           DJoiner  11/10/2001    ***
		'****************************************************************************************
		
		Dim iResponse As Short 'Operator's response to Message Box
        bCancel = False
		'If there are no records to be Saved, Inform Operator and Exit Sub
		If rstFaults.RecordCount < 1 Then
			MsgBox("There is no current record in the RecordSet",  , "RecordSet Error")
			Exit Function
		End If
		
		' Set CancelError is True
		
        'frmViewer.SaveFile.CancelError = True
		On Error GoTo ErrHandler
        Call StatusMsg("Select file name and desination to export text file.")
        sFileLocation = ""
		frmViewer.SaveFileSave.FileName = sFileLocation & sFileNameSeed & ".txt"
		frmViewer.SaveFileSave.InitialDirectory = sFileLocation
		frmViewer.SaveFileSave.DefaultExt = ".txt"
		
        frmViewer.SaveFileSave.Filter = "Text Files | *.txt"
        If (frmViewer.SaveFileSave.ShowDialog() = DialogResult.Cancel) Then
            bCancel = True
            Exit Function
        End If
        If FileExists((frmViewer.SaveFileSave.FileName)) Then
            iResponse = MsgBox(frmViewer.SaveFileSave.FileName & " already exists." & vbCrLf & "Overwrite?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "File Exists")
            If iResponse = MsgBoxResult.No Then
                bExit = True
                Exit Function
            End If
        End If
        If bSingleFile Then
            Call StatusMsg("Saving text file...............")
            
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            System.Windows.Forms.Application.DoEvents()
            FileOpen(1, frmViewer.SaveFileSave.FileName, OpenMode.Output)
            bSingleFile = False
        Else
            sFilename = frmViewer.SaveFileSave.FileName
            SaveAsText = ShowFreeSpace(Left(frmViewer.SaveFileSave.FileName, 3))
        End If
        Exit Function

ErrHandler:

        Debug.Print(Err.Description)
        'User pressed the Cancel button
        bExit = True
        Exit Function
    End Function
	
	
	Public Sub ExportRecord()
		'****************************************************************************************
		'***    Exports a single Record to a Text File.                                       ***
		'***                                                           DJoiner  12/24/2001    ***
		'****************************************************************************************
		
		bSingleFile = True 'Set single record flag to true
		bExit = False
		'If there are no records in the RecordSet, Inform Operator, DO NOT Save
		If rstFaults.RecordCount < 1 Then
			MsgBox("There is no current record in the RecordSet",  , "RecordSet Error")
			Exit Sub
		End If
		
		sFileText = "" 'Initialize File Text
		Call InitializeFields()
		Call SetFieldData() 'Get the Data from the record Fields
		If bRecordError Then Exit Sub 'If a DOA Record Error Occurs, Exit Sub
		Call FindFieldSpaces() 'Get the Field Spaces for Formating
		sFileText = RecordHeader 'Create Record Header
		Call FormatRecord() 'Format the Record
        Call SaveAsText(sFieldValue(0)) 'Launch SaveFile Dialog - Seed with Current Record Number
        If (bCancel = True) Then
            Exit Sub
        End If
		If Not bExit Then
			PrintLine(1, sFileText)
			FileClose(1) 'Close File
		End If
		bSingleFile = False 'Reset single record flag
		
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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
		
		Dim iCounter As Short 'Progress Bar Counter
		Dim iIncrement As Short 'Progress Bar increment per record
		Dim nFileSize As Integer 'File of Temp File
		Dim nFreeSpace As Integer 'Free Space on destination Dive Disk
		Dim iResponse As Short 'Message Box Response
		
		bExit = False
		sFileText = ""
		On Error GoTo FileErr
		
		nBookMark = CInt(frmViewer.txtData(0).Text) 'BookMark Current Record
		rstFaults.MoveFirst() 'Move to first record in RecordSet
		Call InitializeFields() 'Initialize Field Values
		
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		
		If Not bExit Then
			nFileSize = SaveToTemp
            nFreeSpace = SaveAsText(GetFileNameSeed)
            If bCancel Then
                Exit Sub
            End If
			If WillItFit(nFileSize, nFreeSpace) Then
				
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
				Call StatusMsg("Saving text file...............")
				System.Windows.Forms.Application.DoEvents()
				FileCopy(APSDIR & TEMPFILE, sFilename)
				Kill(APSDIR & TEMPFILE)
				Call StatusMsg("File Saved.")
			Else
				Call StatusMsg("The Text File is too large for the destination, choose another location.")
				iResponse = MsgBox("It won't fit, Select another location?", MsgBoxStyle.YesNo, "Will Not Fit")
				If iResponse = MsgBoxResult.No Then
					Kill(APSDIR & TEMPFILE)
				Else
					'Allow operator to select another location.
					nFreeSpace = SaveAsText(GetFileNameSeed)
				End If
			End If
			Call StatusMsg("Record Display Area.")
			
		End If
		
		rstFaults.Requery() 'Requery RecordSet
		Do Until rstFaults.Fields("Record_Identifier").Value = nBookMark 'Move Curser to BookMarked Record
			rstFaults.MoveNext()
		Loop 
		Load_Record_Data() 'Display Bookmarked Record
		
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		If FileExists(APSDIR & TEMPFILE) Then Kill(APSDIR & TEMPFILE)
		Exit Sub
		
FileErr: 
		MsgBox("Error Number: " & Err.Number & vbCrLf & "Error Description: " & Err.Description,  , "Error")
        frmBackground.ProgressBar1.Visible = False
		
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	
	Public Function GetFileNameSeed() As String
		'****************************************************************************************
		'***    Function to build file name seed when Exporting Multiple Documents            ***
		'***    Return= (First Record Number) - (Last Record Number) in ResultSet             ***
		'***                                                           DJoiner  11/10/2001    ***
		'****************************************************************************************
		
		Dim sFirst As String 'First Record Number as a String
		Dim sSecond As String 'Second Record Number as String
		
		rstFaults.MoveFirst()
		sFirst = CStr(rstFaults.Fields("Record_Identifier").Value) 'Get First Record Number in RecordSet
		rstFaults.MoveLast()
		sSecond = CStr(rstFaults.Fields("Record_Identifier").Value) 'Get Last Record Number in RecordSet
		
		GetFileNameSeed = sFirst & "-" & sSecond 'Build File Name to Seed SaveFile Dialog with
		
	End Function
	
	
	Public Function StripLeadingReturns(ByRef sText As String) As String
		'****************************************************************************************
		'***    Function to Strip any leading Return Characters                               ***
		'***                                                                                  ***
		'***    sText = String to Strip leading characters from                               ***
		'***    Return = String with out leading Return characters                            ***
		'***                                                           DJoiner  11/10/2001    ***
		'****************************************************************************************
		
		Dim sTempText As String 'Temporary Text String
        Dim x As Short 'Starting point for vbMid() Function

        StripLeadingReturns = sText
		
		'First Strip off any leading Return Chracters
        For x = 1 To sText.Length()
            If Asc(Mid(sText, x, 1)) = 13 Or Asc(Mid(sText, x, 1)) = 10 Then
                'Rebuild Text String
                sTempText = Trim(Mid(sText, x + 1, Len(sText)))
            Else
                Exit For
            End If
        Next
		StripLeadingReturns = sTempText
		
	End Function
	
	
	Function ShowFileSize(ByRef filespec As Object) As Integer
		'*****************************************************************************
		'***    Finds the File Size of the text file and returns Results           ***
		'***    File Size formatted in Killo Bytes.                                ***
		'***                                        Dave Joiner  12/24/2001        ***
		'*****************************************************************************
		
		Dim fs, f As Object
		Dim bFileError As Boolean
		
		On Error GoTo ErrorHandler
		
		fs = CreateObject("Scripting.FileSystemObject")
		
		f = fs.GetFile(filespec)
		
		'Format Results in Killo Bytes
		
		ShowFileSize = CInt(FormatNumber(f.Size / 1024, 0))
		Exit Function
ErrorHandler: 
		
		If Err.Number <> 0 Then
			MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
			bFileError = True
		End If
		
	End Function
	
	
	Function ShowFreeSpace(ByRef drvpath As Object) As Integer
		'*****************************************************************************
		'***    Finds the Free Space left on the selected Drive Disk.  Size        ***
		'***    is  returned formatted in Killo Bytes.                             ***
		'***                                        Dave Joiner  12/24/2001        ***
		'*****************************************************************************
		
		Dim fs, d As Object
		Dim sReadyResponse As String 'Operator's response from message box
		
		On Error GoTo ErrorHandler
CheckAgain: 
		fs = CreateObject("Scripting.FileSystemObject")
		
		
		d = fs.GetDrive(fs.GetDriveName(drvpath))
		
		'Format Results in Killo Bytes
		
		ShowFreeSpace = CInt(FormatNumber(d.FreeSpace / 1024, 0))
		Exit Function
		
ErrorHandler: 
		
		If Err.Number = 71 Then 'If the Disk is not ready, allow the Operator to try again
			sReadyResponse = CStr(MsgBox("Disk Not Ready" & vbCrLf & "Would you like to try again?", MsgBoxStyle.Information + MsgBoxStyle.RetryCancel, "Disk not Ready"))
			
			If CDbl(sReadyResponse) = 4 Then 'Retry
				GoTo CheckAgain
			Else 'Cancel
				Exit Function
			End If
			
		Else
			If Err.Number <> 0 Then
				MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
			End If
		End If
		Debug.Print("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description)
		Err.Clear()
		Resume Next
		
	End Function
	
	
	Function WillItFit(ByRef nTempFile As Integer, ByRef nFreeSpace As Integer) As Boolean
		'*****************************************************************************
		'***    Compare destination's Free Space with the Text File size.          ***
		'***    Return "True" if it will fit and "False" if not.                   ***
		'***                                        Dave Joiner  12/24/2001        ***
		'*****************************************************************************
		
		'If the Temp File will fit on the Selected Disk, Set Flag to True
		If nTempFile < nFreeSpace Then
			WillItFit = True 'Set Flag to True
		Else
			WillItFit = False 'Set Flag to False
		End If
		
	End Function
	
	
	Public Function SaveToTemp() As Integer
		'*****************************************************************************
		'***    Saves the selected records to a temporary text file. Returns the   ***
		'***    the file size in Killo Bytes.                                      ***
		'***                                        Dave Joiner  12/24/2001        ***
		'*****************************************************************************
		
        Dim sngCounter As Single = 0 'Progress Bar Counter
        Dim sngIncrement As Single = 0 'Progress Bar increment per record
		
		FileOpen(1, APSDIR & TEMPFILE, OpenMode.Output)
		
		rstFaults.MoveFirst() 'Move to first record in RecordSet
        Do Until rstFaults.EOF 'Iterate through the RecordSet
            sFileText = ""
            Call SetFieldData() 'Get the Data from the record Fields
            If bRecordError Then Exit Function 'If a DOA Record Error Occurs, Exit Sub
            sFileText = sFileText & RecordHeader() 'Create Record Header
            Call FindFieldSpaces() 'Get the Field Spaces for Formating
            Call FormatRecord() 'Format the Record
            sFileText = sFileText & vbCrLf & vbCrLf 'Add Blank rows between Records
            rstFaults.MoveNext() 'Move to the Next Record in the RecordSet
            PrintLine(1, sFileText)
        Loop
        FileClose(1) 'Close File

		
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		rstFaults.Requery() 'Requery RecordSet
		Do Until rstFaults.Fields("Record_Identifier").Value = nBookMark 'Move Curser to BookMarked Record
			rstFaults.MoveNext()
		Loop 
		FileClose(1)
		SaveToTemp = ShowFileSize(APSDIR & TEMPFILE)
		
	End Function
End Module