Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic.Compatibility
Imports Microsoft.Win32
Imports System.Text

Module DBServices
    '**********************************************************************
    '***    United States Marine Corps                                  ***
    '***                                                                ***
    '***    Nomenclature:   Module "DBServices" : FHDB_Processor        ***
    '***    Written By:     Dave Joiner                                 ***
    '***    Purpose:                                                    ***
    '***  ECP-TETS-023                                                  ***
    '***    General Module to support the FHDB file manipulation        ***
    '***    functionality. It supports various routines to Add,         ***
    '***    Delete, Update, Search and navigate through a recordset.    ***
    '***    Project Reference: DAO 2.5/3.51 Compatibility Library       ***
    '***  2.1.0                                                         ***
    '***    Removed the Declare Function GetUserName from this file. For***
    '***    the 657 version it was commented out, for the 717 version it***
    '***    is not used in this bas file.                               ***
    '***    Added the Declare Function GetComputerName, this is used to ***
    '***    which tester (657 or 717) to select the correct ini file.   ***
    '***    Removed the sub FindUserName() from this bas file.  This sub***
    '***    is not used in this file, however there is another sub in   ***
    '***    another bas file with the same name that is used, that one  ***
    '***    is keeped.                                                  ***
    '***    Changed all references to either TETS.INI or VIPERT.ini in  ***
    '***    any comments to Tester.ini                                  ***
    '***    Function GatherIniFileInformation                           ***
    '***    Added the variable computName to the function, this is used ***
    '***    to determine which ini file to look into.  Added the test to***
    '***    detect the computer name to select the proper ini file.     ***
    '***    Sub Load_Record_Data                                       ***
    '***    This function was previously modified to allow for the new  ***
    '***    fhdb.mdb to be used, the 657 version has this modification  ***
    '***    the 717 still has the old version and also has an improper  ***
    '***    fhdb.mdb.  This modification appends an empty string to all ***
    '***    fields of the fhdb to prevent the fhdb processor from       ***
    '***    displaying an error message of a NULL field.                ***
    '***    Removed the commented out lines from this function          ***
    '***    Function UpdateIniFile                                      ***
    '***    Added the variable computName to the function, this is used ***
    '***    to determine which ini file to look into.  Added the test to***
    '***    detect the computer name to select the proper ini file.     ***
    '***    Removed Function StripNullCharacters from this file, this   ***
    '***    was used only for debugging long ago and is nolonger needed ***
    '**********************************************************************


    '---  Dimension DAO Database Objects  ---
    Public wrkJet As DAO.Workspace 'Workspace for the Database

    Public rstFaults As DAO.Recordset 'Returned Recordset from the Database
    Public dbsFHDB As DAO.Database 'Database to open

    '---  Dimension Global Level Variables  ---
    Public sUserName As String = System.Environment.UserName 'Computer Logon name: DoD_Admin, Maintainer, Operator
    Public sDBLocation As String 'Database Drive/Path location
    Public sDBFile As String 'Database File name

    Public sWas As String 'Was or Were depending the number of records returned
    Public sRecord As String
    Public bDBOpened As Boolean 'Flag to indicate the Database is Opened

    Public sCriteria As String 'The Criteria to base a search Query on
    Public sRangeFrom As String 'From criteria when searching a range of values
    Public sRangeTo As String 'To criteria when searching a range of values
    Public sField As String 'Field to search on
    Public sSql As String 'The SQL statement used to Query the Database
    Public sDateIndicator As String 'Passes a "#" as a date indicator if required
    Public bDaySearch As Boolean 'Flag to verify a Date has been entered
    Public bNoRecords As Boolean 'Flag to indicate No Records were returned
    Public bNoUnload As Boolean 'Flag to suppress "About Form" except when Program is first loaded
    Public bLargeMove As Boolean 'Flag to indicate the Slider is being moved more than 100 records
    Public Const SECS_IN_DAY As Integer = 86400

    Dim sDebug As String 'For Debugging only
    '---  Declare API Functions  ---
    'Requires (3) Windows Api Functions to retrieve info from Tester's INI file

    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    '**********************************************************************
    '***            Set Constants to identify Table Names               ***
    '**********************************************************************
    Public Const DB_FAULT_TABLE As String = "FAULTS" 'Fault table name
    Public Const DB_SYSTEM_TABLE As String = "SYSTEM" 'System table name
    Public Const STIME As Date = #12:00:00 AM# 'Start Time for Day Query
    Public Const ETIME As Date = #11:59:00 PM# 'End Time for Day Query



    Sub CloseDB()
        '**********************************************************************
        '***      Routine to Close the Database connection and unload       ***
        '**********************************************************************

        On Error GoTo ErrLabel

        If bDBOpened = True Then 'If Database is Open, Close it
            rstFaults.Close() 'Close DB connection and Release Object from Memory
            dbsFHDB.Close()
            wrkJet.Close()
            bDBOpened = False
        End If
        bDBOpened = False
        Exit Sub 'Prevent from dropping through the Error handler

ErrLabel:
        MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
        Err.Clear()
        Resume Next

    End Sub


    Sub FirstRecord()
        '**********************************************************************
        '***      Display first record in recordset in the viewer           ***
        '**********************************************************************

        On Error GoTo ErrLabel
        Call ClearViewer() 'Clear Viewer Text Boxes
        rstFaults.MoveFirst() 'Move cursor to the first record in the Recordset
        'Check cursor position and enable/disable navigation

        If rstFaults.RecordCount > 1 And frmViewer.cmdNext.Enabled = False Then
            Call EnableLastNav() 'Enable MoveLast and MoveNext
        End If

        Call Load_Record_Data() 'Load the Record into the form
        Call DisableFirstNav() 'Disable MoveFirst and MovePrevious
        Exit Sub

ErrLabel:
        If Err.Number = 0 Then
            Exit Sub
        Else
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
        End If

    End Sub



    Function GatherIniFileInformation(ByRef lpApplicationName As String, ByRef lpKeyName As String, ByRef lpDefault As String) As String
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     Finds a value on in the Tester's INI File            *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName$ -[Application]in Tester's INI File*
        '*     lpKeyName$ - KEYNAME= in Tester's INI File           *
        '*     lpDefault$ - Default value to return if not found    *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath$ = GatherIniFileInformation("Heading", ...  *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************

        Dim lpReturnedString As New StringBuilder(1024) 'Return Buffer
        Dim nSize As Integer = 1024 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer

        'Clear String Buffer
        'lpReturnedString.Value = ""
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Find File Locations
        Try
            ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
            Return lpReturnedString.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try


    End Function


    Sub Initialize_SQL()
        '**********************************************************************
        '***    Routine to Build SQL statement based on Users Entries       ***
        '**********************************************************************

        If frmQuery.txtValue(3).Visible = True And frmQuery.txtValue(2).Visible = True Or bDaySearch = True Then
            'Build SQL Query Statement to search on a Range using Date/Time criteria
            sSql = "SELECT * FROM " & "[" & DB_FAULT_TABLE & "]" & " WHERE " & sField & " between " & sDateIndicator & sRangeFrom & sDateIndicator & " and " & sDateIndicator & sRangeTo & sDateIndicator & " ORDER BY Record_Identifier"
        ElseIf frmQuery.ssfraPassFail.Visible Then
            sSql = "SELECT * FROM " & "[" & DB_FAULT_TABLE & "]" & " WHERE " & sField & " = " & sDateIndicator & sCriteria & sDateIndicator & " ORDER BY Record_Identifier"
        Else
            'Build SQL Query Statement to search on a Range NOT using Date/Time criteria
            sSql = "SELECT * FROM " & "[" & DB_FAULT_TABLE & "]" & " WHERE " & sField & " = " & sDateIndicator & sCriteria & sDateIndicator & " ORDER BY Record_Identifier"

        End If

    End Sub



    Sub LastRecord()
        '**********************************************************************
        '***      Display Last record in recordset in the viewer            ***
        '**********************************************************************

        On Error GoTo ErrLabel
        Call ClearViewer() 'Clear Viewer Text Boxes
        rstFaults.MoveLast() 'Move cursor to the last record in the Recordset

        Call DisableLastNav() 'Disable MoveLast and MoveNext

        Call Load_Record_Data() 'Load the Record into the form

        'Check cursor position and enable/disable navigation

        If rstFaults.RecordCount > 1 And frmViewer.cmdFirst.Enabled = False Then
            Call EnableFirstNav() 'Disable MoveFirst and MovePrevious
        End If

        Exit Sub

ErrLabel:
        If Err.Number = 0 Then
            Exit Sub
        Else
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
        End If

    End Sub


    Sub Load_Record_Data()
        '****************************************************************************************
        '*** Load the individual Fields of data into the appropriate text boxes in the Viewer ***
        '****************************************************************************************

        Dim sDegree As String

        sDegree = Chr(176)

        If IsDBNull(rstFaults.Fields("Dimension").Value) Then
            sUOM = ""
        Else
            sUOM = RTrim(rstFaults.Fields("Dimension").Value) & "" 'Define the Unit of Measure, Trim blank spaces
        End If


        frmViewer.txtData(0).Text = rstFaults.Fields("Record_Identifier").Value

        If IsDBNull(rstFaults.Fields("Start_Time").Value) Then
            frmViewer.txtData(1).Text = ""
        Else
            'If Start Time is equal to Default then Clear Display Text, otherwise display it
            If rstFaults.Fields("Start_Time").Value <> #12:00:00 AM# Then
                frmViewer.txtData(1).Text = rstFaults.Fields("Start_Time").Value
            Else
                frmViewer.txtData(1).Text = ""
            End If
        End If

        If IsDBNull(rstFaults.Fields("Stop_Time").Value) Then
            frmViewer.txtData(2).Text = ""
        Else
            'If Stop Time is other than Default, Display, otherwise clear Display Text
            If rstFaults.Fields("Stop_Time").Value <> #12:00:00 AM# Then
                frmViewer.txtData(2).Text = rstFaults.Fields("Stop_Time").Value
            Else
                frmViewer.txtData(2).Text = ""
            End If
        End If


        'Display String values, Trim blank spaces
        If IsDBNull(rstFaults.Fields("ERO").Value) Then
            frmViewer.txtData(3).Text = ""
        Else
            frmViewer.txtData(3).Text = RTrim(rstFaults.Fields("ERO").Value) & ""
        End If

        If IsDBNull(rstFaults.Fields("TPCCN").Value) Then
            frmViewer.txtData(4).Text = ""
        Else
            frmViewer.txtData(4).Text = RTrim(rstFaults.Fields("TPCCN").Value) & ""
        End If

        If IsDBNull(rstFaults.Fields("UUT_Serial_No").Value) Then
            frmViewer.txtData(5).Text = ""
        Else
            frmViewer.txtData(5).Text = RTrim(rstFaults.Fields("UUT_Serial_No").Value) & ""
        End If

        If IsDBNull(rstFaults.Fields("UUT_Rev").Value) Then
            frmViewer.txtData(6).Text = ""
        Else
            frmViewer.txtData(6).Text = RTrim(rstFaults.Fields("UUT_Rev").Value) & ""
        End If

        If IsDBNull(rstFaults.Fields("ID_Serial_No").Value) Then
            frmViewer.txtData(7).Text = ""
        Else
            frmViewer.txtData(7).Text = RTrim(rstFaults.Fields("ID_Serial_No").Value) & ""
        End If


        bPassFail = rstFaults.Fields("Test_Status").Value
        Call DisplayPass_Fail() 'Display True/False as Pass/Fail

        'Display Failure Step, Trim blank spaces
        If IsDBNull(rstFaults.Fields("Failure_Step").Value) Then
            frmViewer.txtData(9).Text = ""
        Else
            frmViewer.txtData(9).Text = RTrim(rstFaults.Fields("Failure_Step").Value) & ""
        End If

        If IsDBNull(rstFaults.Fields("Fault_Callout").Value) Then
            frmViewer.txtData(10).Text = ""
        Else
            frmViewer.txtData(10).Text = rstFaults.Fields("Fault_Callout").Value & ""
        End If

        'If Unit of Measure is Blank, then Clear Display Text
        If sUOM <> "" Then
            frmViewer.txtData(11).Text = rstFaults.Fields("Meas_Value").Value & " " & sUOM 'Add UOM to Value
            frmViewer.txtData(13).Text = rstFaults.Fields("Upper_Limit").Value & " " & sUOM 'Add UOM to Value
            frmViewer.txtData(14).Text = rstFaults.Fields("Lower_Limit").Value & " " & sUOM 'Add UOM to value
        Else
            frmViewer.txtData(11).Text = ""
            frmViewer.txtData(13).Text = ""
            frmViewer.txtData(14).Text = ""
        End If


        'Display Operator Comments
        If IsDBNull(rstFaults.Fields("Operator_Comments").Value) Then
            frmViewer.txtComments.Text = ""
        Else
            frmViewer.txtComments.Text = rstFaults.Fields("Operator_Comments").Value & ""
        End If


        'If Temperature value is other than zero, Display it
        If rstFaults.Fields("Temperature").Value <> 0 Then
            frmViewer.txtData(16).Text = rstFaults.Fields("Temperature").Value & sDegree & " C" 'Add Deg "C" Celsius to value
        Else
            frmViewer.txtData(16).Text = ""
        End If

    End Sub


    Sub NextRecord()

        '**********************************************************************
        '***      Display Next record in recordset in the viewer            ***
        '**********************************************************************

        On Error GoTo ErrLabel

        If Not rstFaults.EOF Then
            rstFaults.MoveNext()

            If rstFaults.RecordCount > 1 And frmViewer.cmdFirst.Enabled = False Then
                EnableFirstNav() 'Enable MoveFirst and MovePrevious
            End If
            Call ClearViewer()
        End If
        'If Recordset has any records, move cursor to last Record
        If rstFaults.EOF And rstFaults.RecordCount > 0 Then
            rstFaults.MoveLast()
        End If
        'If not moving to another record with a span greater than 100 records, Display Record
        If bLargeMove = False Then 'Support for Slider Control
            Call Load_Record_Data()
        End If
        rstFaults.MoveNext()

        If rstFaults.EOF Then
            DisableLastNav() 'Disable MoveLast and MoveNext
            rstFaults.MoveLast()
        Else
            rstFaults.MovePrevious()
        End If

        Exit Sub

ErrLabel:

        If Err.Number = 0 Then
            Exit Sub
        Else
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
        End If

    End Sub


    Sub OpenDB()
        '**********************************************************************
        '***    Procedure to Open the FHDB using DOA Access Objects.        ***
        '**********************************************************************

        Dim sSqlAll As String 'SQL Statement to Query all Records in the FAULTS Table

        On Error GoTo ErrLabel
        sSqlAll = "SELECT * FROM " & "[" & DB_FAULT_TABLE & "]" & " ORDER BY Record_Identifier"
        bDBOpened = True 'Set Flag to indicate the Database is Opened
        'Find Database file location from the Tester's INI file
        sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")

        'Open database and fill recordset (Reference: DAO 2.5/3.51 Compatibility Library)
        DAODBEngine_definst.SetOption(DAO.SetOptionEnum.dbMaxLocksPerFile, 128)
        wrkJet = DAODBEngine_definst.Workspaces(0) 'Open Workspace

        dbsFHDB = wrkJet.OpenDatabase(sDBFile) 'Open Database

        rstFaults = dbsFHDB.OpenRecordset(sSqlAll, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open Recordset
        If rstFaults.BOF Then
            Exit Sub
        Else
            rstFaults.MoveLast() 'Move to last Record in table
        End If
        Exit Sub 'Prevent program from flowing through Error Handler

        'Error hane
ErrLabel:
        'If Database can't be found (3024) or the Object variable can't be Set (91), Terminate application.
        If Err.Number = 3024 Or Err.Number = 91 Then
            MsgBox(Err.Description & vbCrLf & "Aplication will terminate.", MsgBoxStyle.Critical, "FHDB Importer ERROR")
            End
        Else
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
            Err.Clear()
            Resume Next
        End If

    End Sub


    Sub PreviousRecord()
        '**********************************************************************
        '***      Display Previous record in recordset in the viewer        ***
        '**********************************************************************

        On Error GoTo ErrLabel

        Call ClearViewer() 'Clear the Display in the Veiwer
        'If the Cursor it not at the First Record, then Move back one
        If Not rstFaults.BOF Then rstFaults.MovePrevious()
        'If the Cursor is at the First Record and there are records in the recordset, Move to First Record
        If rstFaults.BOF And rstFaults.RecordCount > 0 Then
            rstFaults.MoveFirst()
        End If


        If rstFaults.RecordCount > 1 And frmViewer.cmdNext.Enabled = False Then
            Call EnableLastNav() 'Enable MoveLast and MoveNext
        End If
        'If not moving to another record with a span greater than 100 records, Display Record
        If bLargeMove = False Then 'Support for Slider Control
            Load_Record_Data()
        End If

        rstFaults.MovePrevious()
        'If this is the First Record, Disable Appropriate Navigation Controls, Else Move to the Next Record
        If rstFaults.BOF Then
            Call DisableFirstNav() 'Disable MoveFirst and MovePrevious
            rstFaults.MoveFirst()
        Else
            rstFaults.MoveNext()
        End If

        Exit Sub 'Prevent from falling through the Error Handler

ErrLabel:
        If Err.Number = 0 Then
            Exit Sub
        Else
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
        End If

    End Sub


    Sub ReturnAll()
        '**********************************************************************
        '***   Query FAULTS table and load into ResultSet allowing User     ***
        '***   to iterate through records via the Viewer.                   ***
        '**********************************************************************

        Call OpenDB() 'Open DB and populate recordset

        If rstFaults.RecordCount = 0 Then
            sMessageString = "There are no records in database."
            frmViewer.mnuNavigation.Enabled = False 'Disable functionality for multiple records
            frmViewer.mnuSearch.Enabled = False
            frmViewer.mnuExport.Enabled = False
            frmViewer.mnuImport.Enabled = False
            frmViewer.mnuComment.Enabled = False
            Call Disable_Navigation()

            If sMessageString <> frmBackground.stbMain.Items.Item(0).Text Then

                frmBackground.stbMain.Items.Item(0).Text = sMessageString
            End If
            Exit Sub
        Else
            frmViewer.mnuNavigation.Enabled = True 'Enable functionality for multiple records
            frmViewer.mnuSearch.Enabled = True
            frmViewer.mnuExport.Enabled = True
            frmViewer.mnuImport.Enabled = True
            frmViewer.mnuComment.Enabled = True
        End If

        'If over 10 total Records then Show Slider
        If rstFaults.RecordCount > 10 Then
            frmViewer.SldRecord.Visible = True
            frmViewer.lblScroller.Visible = True
        End If
        'Set Status variables and Navigation Controls depending on number of records returned
        If rstFaults.RecordCount > 1 Then
            Call Enable_Navigation()
            sWas = "were"
            sRecord = "records"
        Else
            Call Disable_Navigation()
            sWas = "was"
            sRecord = "record"
        End If
        'Build Status Message
        sMessageString = "There " & sWas & " a total of " & rstFaults.RecordCount & " " & sRecord & " returned in your search."
        'Display Status Message

        If sMessageString <> frmBackground.stbMain.Items.Item(0).Text Then

            frmBackground.stbMain.Items.Item(0).Text = sMessageString
        End If

        Call SetMinMax() 'Set Scroller Properties
        Call FirstRecord() 'Move curser to First record in RecordSet
        'If moving to the Last Record
        If bLast = True Then
            Call LastRecord() 'Move to Last Record
            bLast = False 'Reset Flag
        End If

    End Sub


    Sub SaveComment()
        '**********************************************************************
        '***            Routine to Save the Comment Entered                 ***
        '**********************************************************************

        On Error GoTo ErrLabel

        Call TimeStamp()
        'Concatenate Existing Comments and New Comments and Save
        rstFaults.Edit()
        rstFaults.Fields("Operator_Comments").Value = sOldComment & sNewComment
        rstFaults.Update()
        'Update Veiwer Display
        RefreshRecord()
        frmViewer.Refresh()
        Exit Sub

ErrLabel:

        MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
        Err.Clear()
        Resume Next

    End Sub


    Sub Search_DB()
        '**********************************************************************
        '***   Routine to Query the FAULTS table based on specific          ***
        '***   criteria and return results.                                 ***
        '**********************************************************************

        On Error GoTo ErrLabel
        If bDBOpened = True Then 'If Database is open, Close
            Call CloseDB()
        End If

        sCriteria = frmQuery.txtValue(1).Text 'Assign a value to SCriteria
        'If Search is a Date/Time for a by value entry, search by range from 12:00 AM to 12:00 PM that day
        If bDaySearch = True And frmQuery.txtValue(1).Visible = True Then
            sRangeFrom = sCriteria & " " & STIME
            sRangeTo = sCriteria & " " & ETIME
        Else
            sRangeFrom = frmQuery.txtValue(2).Text
            sRangeTo = frmQuery.txtValue(3).Text
        End If
        Call SetSQLValues() 'Set the values based on the criteria for the SQL statement
        Call Initialize_SQL() 'Build the Query

        'Find Database file location from the Tester's INI file
        sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
        'Open database and fill recordset (Reference: Microsoft DAO 2.5/3.51 Compatibility Library)
        wrkJet = DAODBEngine_definst.Workspaces(0) 'Open Workspace
        dbsFHDB = wrkJet.OpenDatabase(sDBFile) 'Open Database
        rstFaults = dbsFHDB.OpenRecordset(sSql, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open Recordset

        rstFaults.MoveLast() 'Move to last Record
        rstFaults.MoveFirst() 'Move to First Record
        Call Load_Record_Data() 'Load record into Viewer

        If rstFaults.RecordCount > 1 Then 'Format Message variables
            Call Enable_Navigation()
            sWas = "were"
            sRecord = "records"
        Else
            Call Disable_Navigation()
            sWas = "was"
            sRecord = "record"
        End If
        'Inform Operator how of many Records were returned in the Query
        sMessageString = "There " & sWas & " a total of " & rstFaults.RecordCount & " " & sRecord & " returned in your search."
        Call FirstRecord() 'Move curser to First record in RecordSet

        If sMessageString <> frmBackground.stbMain.Text Then

            frmBackground.stbMain.Text = sMessageString
        End If
        Call CloseDB()
        bDBOpened = False 'Reset value to False
        bDaySearch = False

        Exit Sub

        'Error handling routine
ErrLabel:
        If Err.Number = 0 Then
            Exit Sub
        ElseIf Err.Number = 3021 Then  'Record not in Database
            MsgBox("The Record you have specified does not exist in Database." & vbCrLf & "Please revise your search criteria.", , "Record does not exist")
            bNoRecord = True
            bDaySearch = False
            CloseDB()
            OpenDB()
            Call Select_Last()
        ElseIf Err.Number = -2147217900 Then  'Invalid Criteria Entered
            MsgBox("Please enter valid criteria to perform search on!", MsgBoxStyle.Exclamation, "Enter Valid Criteria")
            bNoRecord = True
            bDaySearch = False
            CloseDB()
            OpenDB()
            Call Select_Last()
        Else
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
        End If
        '    Debug.Print Err.Number                 'For Debugging only

    End Sub


    Sub SetSQLValues()
        '***********************************************************************
        '*** Routine to insert User selected cboBox Field into SQL statement ***
        '***********************************************************************

        sDateIndicator = Q
        Select Case frmQuery.cboValue.Text

            Case "Record Number"
                sDateIndicator = ""
                sField = "Record_Identifier"
            Case "Start Date/Time"
                sField = "Start_Time"
                sDateIndicator = "#"
            Case "Intake Temperature"
                sDateIndicator = ""
                sField = "Temperature"
            Case "UUT Serial Number" : sField = "UUT_Serial_No"
            Case "UUT Revision" : sField = "UUT_Rev"
            Case "ID Serial Number" : sField = "ID_Serial_No"
            Case "TPCCN"
                sCriteria = UCase(sCriteria)
                sField = "TPCCN"
            Case "ERO" : sField = "ERO"
            Case "Pass/Fail Status"
                sDateIndicator = ""

                If CBool(frmQuery._ssoptPassFail_0.Checked) Then
                    sCriteria = CStr(True)
                ElseIf frmQuery._ssoptPassFail_1.Checked Then
                    sCriteria = CStr(False)
                End If
                sField = "Test_Status"

        End Select

    End Sub



    Sub UpdateIniFile(ByRef AppName As String, ByRef Key As String, ByRef IniInfo As String)
        '******************************************************************************
        '***    Procedure to Update Information in the Tester's INI File            ***
        '***    If IniInfo is equal to "" then it Updates with the Current Time     ***
        '***    If IniInfo contains a String value, it will update with that value  ***
        '***                                              Dave Joiner  03/12/2001   ***
        '******************************************************************************

        Dim lpReturnedString As New VB6.FixedLengthString(255) 'Returned API Buffer
        Dim ReturnValue As Integer 'API Returned Eror Code
        Dim lpFileName As String 'File to write to (Tester's INI)
        Dim lpKeyName As String 'KEY= in INI file
        Dim lpDefault As String 'Default value if key not found
        Dim lpApplicationName As String '[Application] in INI file

        'Clear String Buffer
        lpReturnedString.Value = Space(255)

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        lpReturnedString.Value = ""

        lpApplicationName = AppName
        lpKeyName = Key

        If IniInfo = "" Then
            lpDefault = CLng(DateSerial(Year(Now), Month(Now), Microsoft.VisualBasic.Day(Now)).ToOADate) + (Microsoft.VisualBasic.Timer() / SECS_IN_DAY)
            lpDefault = VB6.Format(lpDefault, "0.00000")
        Else
            lpDefault = IniInfo
        End If

        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpFileName)

    End Sub


    Public Sub SetMinMax()
        '******************************************************************************
        '***    Procedure to set the Min and Max Settings on the Slider Control     ***
        '***                                    Dave Joiner   03/29/2001            ***
        '******************************************************************************

        'Initialize Min and Value
        frmViewer.SldRecord.Value = frmViewer.SldRecord.Minimum

        'If Records returned is less than LargeChange Value then Disable Scroller
        If rstFaults.RecordCount < 10 Then
            frmViewer.SldRecord.Visible = False 'Hide Scroller
            frmViewer.lblScroller.Visible = False

            'We still need to set the min and max values of the scroller
            rstFaults.MoveLast() 'Set Max value
            frmViewer.SldRecord.Maximum = rstFaults.Fields("Record_Identifier").Value

            rstFaults.MoveFirst() 'Set Min Value
            frmViewer.SldRecord.Minimum = rstFaults.Fields("Record_Identifier").Value

            'Set Value and Slider's Starting point
            frmViewer.SldRecord.Value = rstFaults.Fields("Record_Identifier").Value
            frmViewer.SldRecord.Value = frmViewer.SldRecord.Minimum
            Exit Sub
        Else 'If more than one Record
            frmViewer.lblScroller.Visible = True 'Display Scroller
            frmViewer.SldRecord.Visible = True

            rstFaults.MoveLast() 'Set Max value
            frmViewer.SldRecord.Maximum = CInt(rstFaults.Fields("Record_Identifier").Value)

            rstFaults.MoveFirst() 'Set Min Value
            'Set Value and Slider's Starting point
            frmViewer.SldRecord.Minimum = CInt(rstFaults.Fields("Record_Identifier").Value)

        End If

    End Sub


    Public Sub RefreshRecord()

        If rstFaults.RecordCount < 2 Then Exit Sub


        If frmViewer.cmdPrevious.Enabled = False And rstFaults.RecordCount > 1 Then
            NextRecord()
            PreviousRecord()

        ElseIf frmViewer.cmdNext.Enabled = False And rstFaults.RecordCount > 1 Then
            PreviousRecord()
            NextRecord()
        Else
            NextRecord()
            PreviousRecord()
        End If
        System.Windows.Forms.Application.DoEvents()
    End Sub


    Public Sub CharacterInString(ByRef sParsed As String)
        'DESCRIPTION:
        '   This routine checks for any characters with ASCII values greter than or equal to 32 from the
        '   or equal to 32 and less than or equal to 126. (any character will be in this range)
        '   If a character is found, the Comment is Saved.
        '   If no characters are found, the Comment is not Saved.
        '   This prevents the Operator from Saving a Comment with empty lines in it.
        'PARAMTERS:
        '   sParsed = String to check for Characters
        Dim x As Object

        For x = 1 To Len(sParsed)

            If Asc(Mid(sParsed, x, 1)) >= 33 And Asc(Mid(sParsed, x, 1)) <= 126 Then
                'Save it
                SaveComment()
                Call CloseComment()
                'Comment will be Updated as sOldComment + sNewComment
                Call Load_Record_Data()
                Exit Sub
            Else
                'Don't Save it
                Call CloseComment()
                Exit Sub
            End If
        Next

    End Sub
End Module