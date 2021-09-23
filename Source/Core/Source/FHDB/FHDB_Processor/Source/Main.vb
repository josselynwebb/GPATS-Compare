Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Module m_Main
	'**********************************************************************
	'***    United States Marine Corps                                  ***
	'***                                                                ***
	'***    Nomenclature:   Module "m_Main" : FHDB_Processor            ***
	'***    Version:        1.2                                         ***
	'***    Written By:     Dave Joiner                                 ***
	'***    Last Update:    04/15/04                                    ***
	'***    Purpose:                                                    ***
	'***  ECP-TETS-023   05/03/2001  DJoiner                            ***
	'***    General Module to support the FHDB                          ***
	'***              TETS FAULT HISTORY DATABASE SCHEDULER             ***
	'***    This Procedure connects to the "FHDB1" Database and         ***
	'***    checks for Last Export Date, Import Date, and               ***
	'***    Data Dump Interval.                                         ***
	'***    These values are evaluated and the appropriate action       ***
	'***    is Triggered.                                               ***
	'***    Either Launch Import Function, Launch Export Function,      ***
	'***    Viewer Function, or Exit Procedure.                         ***
	'***                                                                ***
	'***    Change History:                                             ***
	'**********************************************************************
	'*** V1.1, ECO-3047-579, Software Release 8.1                       ***
	'***                                                                ***
	'*** DR#261  03/14/02  Dave Joiner                                  ***
	'***    Modified the Importer Operation. Removed frmImport, the     ***
	'***    Import Operation uses Message Boxes, Status Bar and         ***
	'***    Progress Bar to interact with the User.                     ***
	'***    If the Import Disk is valid, the system will automatically  ***
	'***Delete Records, Compact Database, and Update Tester's ini Keys. ***
	'***    A message Box will pop informing the User that the          ***
	'***    Operation was successful. If any errors are encountered,    ***
	'***    the User is informed and the Operation is terminated.       ***
	'***    If the Operation is launched by the "Create UDD Operation"  ***
	'***    the application will terminate at completion, otherwise     ***
	'***    the system will return control to the FHDB Viewer.          ***
	'*** DR#262  03/15/02  Dave Joiner                                  ***
	'***    Added a call to function StripNullCharacters() to remove    ***
	'***    all Null characters from the variable "sControllerSN".      ***
	'***    Modified routine to only Trim "sControllerSN" if the length ***
	'***    exceeded 15 characters. (this is the Database field length) ***
	'**********************************************************************
	'** V1.2, ECO-3047-663   - Release 10.0B -                           **
	'** Universal Dock Modifications - Dave Joiner  4/13/04              **
	'**    General modifications to allow operation on systems where     **
	'**    the floppy drive letter is "A:" or "B:".                      **
	'**********************************************************************
	'** V2.00                                                            **
	'** DME PCR ICR-10 floppy removal - Jess Gillespie 3/16/07           **
	'**    All floppy references will be changed to USB reference        **
	'**     where all A: or B: will change to G: and all "Floppy"        **
	'**     will change to "USB" with exception of variable names        **
	'**********************************************************************
	'** V2.1.0                                                           **
	'**     Changed all references to either TETS.INI or VIPERT.ini in   **
	'**     any comments to Tester.ini                                   **
	'**     Deleted the nLength, strOldText, sFDD declaration and change **
	'**     the declaration of gResponse from a file object to an integer**
	'**     because the way the selection of where the file is has been  **
	'**     completely redone, and used the gResponse as ok or cancel    **
	'**     now.  The operator selects a location from a file manger now **
	'**     Added the declaration for the new way of selecting the       **
	'**     location.                                                    **
	'**     Added the constants EXPORT_FILE and IMPORT_FILE to choose the**
	'**     correct message to display.                                  **
	'**     Deleted the Sub CloseViewer, this sub was never being called **
	'**     Sub Main                                                     **
	'**     Deleted the drive detection code at the begining of the sub, **
	'**     the selection of where to put or get the file has been       **
	'**     completely re-written.                                       **
	'**     Removed the bImportFromMain variable from the program, the   **
	'**     setting and testing of this variable wasn't being used for   **
	'**     anything.                                                    **
	'**     Sub SavePass_Fail was removed, not being used.               **
	'**     Function UDD_HD_Path and Function BrowseFolders              **
	'**     These function were added.  This is the new way of selecting **
	'**     of where to get or put the FHDB file information.            **
	'**     Function exportMessage and Function importMessage            **
	'**     These function were added to instruct the operator on how to **
	'**     select, and which areas were not allow for selection, where  **
    '**     to either get or put the FHDB file info.                     **
    '***
    '*** 
    '***    Updated 8/31/2015 by Jess Gillespie
    '***    - Update code from vs2006 to VS2012
    '***
	'**********************************************************************
	
	
	
	Public bNoRecord As Boolean 'Flag to indicate No Records in Database
	Public bPassFail As Boolean 'Pass/Fail Status of record
	
	Public sUOM As String 'Unit of Measure value
	Public sNewComment As String 'New Comment entered
	Public sOldComment As String 'Old, existing Comment
	
	Public Q As String 'Holds Date indicator value
	Public bExport As Boolean 'Export Operation Flag
	Public bDate As Boolean 'Flag to indicate Search is on a Date value
	Public bValidData As Boolean 'Flag to indicate a valid date
	Public bSearch(3) As Boolean 'Array to Flag valid Search Criteria
	
	'Variables needed to convert data types from the Tester's INI
	Public sXDate As String 'Export Date from the Tester's INI
	Public sDDI As String 'DDI from the Tester's INI
    Public dtXdate As DateTime 'Export Date converted to Date/Time
	Public iDDI As Short 'DDI converted to an Integer
	Public sIDate As String 'String value of Import Date
    Public dtIDate As DateTime 'Import Date converted to Date/Time
	Public bImportExport As Boolean 'Flag to indicate an Import/Export Operation is due
	Public dtNext_ImportExport As Date 'Date next Import/Export Operation Due
	
	Public sMessageString As String 'Status Message to the Operator
	Public bTerminateImporter As Boolean 'Flag to force the Importer to Teminate
	Public bOnStart As Boolean 'Flag to indicate Program first started
	Public iMax As Short 'Indicates Maximum Windows State
	Public bLast As Boolean 'Flag to indicate the Last Record in the Recordset
	Public nBookMark As Integer 'Record Number of the last record shown in the Viewer
	Public sFileLocation As String 'File location where the database is stored or is to be stored.
	Public gResponse As Short 'Used to indicate the uers response to a msgbox
	
	'Declarations for displaying a browse window
	Private Structure BrowseInfo
		Dim hWndOwner As Integer
		Dim pIDLRoot As Integer
		Dim pszDisplayName As Integer
		Dim lpszTitle As Integer
		Dim ulFlags As Integer
		Dim lpfnCallback As Integer
		Dim lParam As Integer
		Dim iImage As Integer
	End Structure
	
	'Browsing type.
	Public Enum BrowseType
		BrowseForFolders = &H1
		BrowseForComputers = &H1000
		BrowseForPrinters = &H2000
		BrowseForEverything = &H4000
	End Enum
	
	'Folder Type
	Public Enum FolderType
		CSIDL_BITBUCKET = 10
		CSIDL_CONTROLS = 3
		CSIDL_DESKTOP = 0
		CSIDL_DRIVES = 17
		CSIDL_FONTS = 20
		CSIDL_NETHOOD = 18
		CSIDL_NETWORK = 19
		CSIDL_PERSONAL = 5
		CSIDL_PRINTERS = 4
		CSIDL_PROGRAMS = 2
		CSIDL_RECENT = 8
		CSIDL_SENDTO = 9
		CSIDL_STARTMENU = 11
	End Enum
	
	Private Const MAX_PATH As Short = 260
	Private Declare Sub CoTaskMemFree Lib "ole32.dll" (ByVal hMem As Integer)
	Private Declare Function lstrcat Lib "kernel32.dll"  Alias "lstrcatA"(ByVal lpString1 As String, ByVal lpString2 As String) As Integer
	
	Private Declare Function SHBrowseForFolder Lib "shell32.dll" (ByRef lpbi As BrowseInfo) As Integer
	Private Declare Function SHGetPathFromIDList Lib "shell32.dll" (ByVal pidList As Integer, ByVal lpBuffer As String) As Integer
	Private Declare Function SHGetSpecialFolderLocation Lib "shell32.dll" (ByVal hWndOwner As Integer, ByVal nFolder As Integer, ByRef ListId As Integer) As Integer
	
    Const SECS_IN_DAY As Integer = 86400 'Seconds in a Day
	Public Const EXPORT_FILE As Short = 1 'Flag used for Message
	Public Const IMPORT_FILE As Short = 2 'Flag used for Message
	
	
	
	Sub ClearSearch()
		'**********************************************************************
		'***            Clear the Query form for shrinking                  ***
		'**********************************************************************
		
		frmQuery.cboValue.Visible = False
		frmQuery.lblCriteria.Visible = False
		
		frmQuery.cmdByValue.Visible = True
		
		frmQuery.cmdByRange.Visible = True
		
		frmQuery.cmdSearch.Visible = False
		frmQuery.lblFrom.Visible = False
		frmQuery.lblTo.Visible = False
		frmQuery.txtValue(3).Visible = False
		frmQuery.txtValue(3).Text = ""
		frmQuery.txtValue(2).Visible = False
		frmQuery.txtValue(2).Text = ""
		frmQuery.txtValue(1).Visible = False
		frmQuery.txtValue(1).Text = ""
		
	End Sub
	
	
	Sub ClearViewer()
		'**********************************************************************
		'***               Clear all text values in Viewer                  ***
		'**********************************************************************
		
		sUOM = ""
		frmViewer.txtData(0).Text = ""
		frmViewer.txtData(1).Text = ""
		frmViewer.txtData(2).Text = ""
		frmViewer.txtData(3).Text = ""
		frmViewer.txtData(4).Text = ""
		frmViewer.txtData(5).Text = ""
		frmViewer.txtData(6).Text = ""
		frmViewer.txtData(7).Text = ""
		frmViewer.txtData(8).Text = ""
		frmViewer.txtData(9).Text = ""
		frmViewer.txtData(10).Text = ""
		frmViewer.txtData(11).Text = ""
		frmViewer.txtData(13).Text = ""
		frmViewer.txtData(14).Text = ""
		frmViewer.txtComments.Text = ""
		frmViewer.txtData(16).Text = ""
		
	End Sub
	
	
	Sub CloseComment()
		'****************************************************************************
		'*** When Closing frmComment, Cleanup variables and show Viewer Maximized ***
		'****************************************************************************
		
		frmComment.txtComment.Text = "" 'Clear New Comment
		sNewComment = ""
		frmViewer.Show() 'Show Viewer
		frmViewer.WindowState = System.Windows.Forms.FormWindowState.Maximized 'Maximized (inside MDI Container)
		frmViewer.Activate()
		frmComment.Close() 'Unload Comment Form
		
	End Sub
	
	
	Sub Disable_Navigation()
		'**********************************************************************
		'***    Routine to Disable all Record Navigation Controls when      ***
		'***    they are no longer useful.                                  ***
		'***    EI: Only one record in ResultSet                            ***
		'**********************************************************************
		
		
		frmViewer.cmdFirst.Enabled = False 'Disable all Navigational Controls
		
		frmViewer.cmdPrevious.Enabled = False
		
		frmViewer.cmdNext.Enabled = False
		
		frmViewer.cmdLast.Enabled = False
		frmViewer.mnuNavigation.Enabled = False
		frmViewer.SldRecord.Visible = False
		frmViewer.lblScroller.Visible = False
		
	End Sub
	
	
	Sub DisableFirstNav()
		'**********************************************************************
		'***    Routine to Enable "First" and "Previous" Navigation         ***
		'***    commands when a Record no longer exists.                    ***
		'**********************************************************************
		
		frmViewer.mnuFirst.Enabled = False 'Disable MoveFirst and MovePrevious
		frmViewer.mnuPrevious.Enabled = False
		
		frmViewer.cmdFirst.Enabled = False
		
		frmViewer.cmdPrevious.Enabled = False
		
	End Sub
	
	
	Sub DisableLastNav()
		'**********************************************************************
		'***     Routine to Disable "Next" and "Last" Navigation commands   ***
		'***     when a Record no longer exists.                            ***
		'**********************************************************************
		
		frmViewer.mnuLast.Enabled = False 'Disable MoveLast and MoveNext
		frmViewer.mnuNext.Enabled = False
		
		frmViewer.cmdLast.Enabled = False
		
		frmViewer.cmdNext.Enabled = False
		
	End Sub
	
	
	Sub DisplayPass_Fail()
		'**********************************************************************
		'***     Procedure to change the Pass/Fail Color indicator          ***
		'**********************************************************************
		
		If bPassFail Then
            frmViewer.txtData(8).Text = "PASS"
            frmViewer.txtData(8).ForeColor = Color.Green           
        Else
            frmViewer.txtData(8).Text = "FAIL"
            frmViewer.txtData(8).ForeColor = Color.Red
        End If
		
	End Sub
	
	
	Sub Enable_Navigation()
		'**********************************************************************
		'***     Routine to Enable all Record Navigation Controls when      ***
		'***     they are useful.                                           ***
		'***     EI: Many records in ResultSet                              ***
		'**********************************************************************
		
		
		frmViewer.cmdFirst.Enabled = True 'Enable all Navigation controls
		
		frmViewer.cmdPrevious.Enabled = True
		
		frmViewer.cmdNext.Enabled = True
		
		frmViewer.cmdLast.Enabled = True
		frmViewer.mnuNavigation.Enabled = True
		
	End Sub
	
	
	Sub EnableFirstNav()
		'**********************************************************************
		'***    Routine to Enable "First" and "Previous" Navigation         ***
		'***    commands when a Record exists.                              ***
		'**********************************************************************
		
		frmViewer.mnuFirst.Enabled = True 'Disable MoveFirst and MovePrevious
		frmViewer.mnuPrevious.Enabled = True
		
		frmViewer.cmdFirst.Enabled = True
		
		frmViewer.cmdPrevious.Enabled = True
		
	End Sub
	
	
	Sub EnableLastNav()
		'**********************************************************************
		'***      Routine to Enable "Next" and "Last" Navigation commands   ***
		'***      when a Record exists.                                     ***
		'**********************************************************************
		
		frmViewer.mnuLast.Enabled = True 'Enable MoveLast and MoveNext
		frmViewer.mnuNext.Enabled = True
		
		frmViewer.cmdLast.Enabled = True
		
		frmViewer.cmdNext.Enabled = True
		
	End Sub
	
	
	Sub EnableSearch()
		'**********************************************************************
		'***    Enable [Search] button when valid search criteria has       ***
		'***    been entered.                                               ***
		'**********************************************************************
		
		If frmQuery.txtValue(1).Visible Then 'Single Criteria
			If bSearch(1) Then
				
				frmQuery.cmdSearch.Enabled = True
			End If
		Else
			If bSearch(2) And bSearch(3) Then 'Range Criteria
				
				frmQuery.cmdSearch.Enabled = True
			End If
		End If
		
	End Sub

	
	
	Sub FormatSelectedValue()
		'**********************************************************************
		'***   When the User selects a field to Query, Form is Formatted    ***
		'***   to User's Choice.                                            ***
		'**********************************************************************
		
		bSearch(1) = False 'Set all Search Flags to False
		bSearch(2) = False
		bSearch(3) = False
		
        frmQuery.cmdSearch.Enabled = False 'Initiate the Search Command to Disabled
		
		If frmQuery.cboValue.Text = "Intake Temperature" Then
			If frmQuery.txtValue(3).Visible = False Then
				frmQuery.lblDeg(0).Visible = True
			Else
				frmQuery.lblDeg(0).Visible = True
				frmQuery.lblDeg(1).Visible = True
			End If
		Else
			frmQuery.lblDeg(0).Visible = False
			frmQuery.lblDeg(1).Visible = False
		End If
		
		If frmQuery.txtValue(3).Visible = False Then
			If frmQuery.cboValue.Text = "Pass/Fail Status" Then
				
				frmQuery.ssfraPassFail.Visible = True 'Display Pas/Fail selector
				frmQuery.lblCriteria.Visible = False
				frmQuery.txtValue(1).Visible = False
				
                FHDB_Processor.frmQuery.cmdSearch.Visible = True
                frmQuery.cmdSearch.Enabled = True
                frmQuery.cmdSearch.Show()
                bDate = False
				Exit Sub
			Else
				
				frmQuery.ssfraPassFail.Visible = False
				frmQuery.lblCriteria.Visible = True
				frmQuery.txtValue(1).Visible = True
			End If
			
		End If
		
		Select Case frmQuery.cboValue.Text
			
			Case "Start Date/Time"
				
				frmQuery.cmdSearch.Enabled = False
				If frmQuery.cboValue.Text = "Start Date/Time" Then
					
					If frmQuery.txtValue(1).Visible Then
						frmQuery.txtValue(1).Text = "MM/DD/YY HH:mm" 'Display Date/Time mask
						bDate = True
						frmQuery.txtValue(1).Focus()
					Else
						frmQuery.txtValue(2).Text = "MM/DD/YY HH:mm"
						frmQuery.txtValue(3).Text = "MM/DD/YY HH:mm"
						bDate = True
						frmQuery.txtValue(2).Focus()
					End If


				Else
					
					frmQuery.txtValue(1).Text = ""
					bDate = False
					frmQuery.txtValue(1).Focus()
				End If
				
			Case Else
				bDate = False
				If frmQuery.txtValue(3).Visible = False Then
					frmQuery.txtValue(1).Text = ""
					frmQuery.txtValue(1).Focus()
				Else
					frmQuery.txtValue(3).Text = ""
					frmQuery.txtValue(2).Text = ""
					frmQuery.txtValue(2).Focus()
				End If
				
		End Select
		
	End Sub
	
	
	Sub Initalize_cboValue()
		'**********************************************************************
		'***        Fill the cboBox with the correct value depending        ***
		'***        on Users selection.                                     ***
		'**********************************************************************
		
		'Fields that can be Queried when the User selects "Range"
		frmQuery.cboValue.Items.Insert(0, ("Record Number"))
		frmQuery.cboValue.Items.Insert(1, ("Start Date/Time"))
		frmQuery.cboValue.Items.Insert(2, ("Intake Temperature"))
		'Additional fields that can be Queried when the User selects 'RANGE"
		If frmQuery.txtValue(2).Visible = False And frmQuery.txtValue(3).Visible = False Then
			frmQuery.cboValue.Items.Insert(3, ("Pass/Fail Status"))
			frmQuery.cboValue.Items.Insert(4, ("UUT Serial Number"))
			frmQuery.cboValue.Items.Insert(5, ("UUT Revision"))
			frmQuery.cboValue.Items.Insert(6, ("ID Serial Number"))
			frmQuery.cboValue.Items.Insert(7, ("TPCCN"))
			frmQuery.cboValue.Items.Insert(8, ("ERO"))
		End If
		
		frmQuery.cboValue.SelectedIndex = 0
		
	End Sub
	
	
	
	Public Sub Main()
		'Start Up Routine
		'**************************************************************************
		'***      Check Data in Tester's INI to determine which Operation       ***
		'***      to Launch.                                                    ***
		'***                                        Dave Joiner  03/02/2001     ***
		'**************************************************************************
		'**                                                                      **
		'**   When this Application is Launched we want to ask Operator if       **
		'**   they would Like to Launch the Operation that is Due,               **
		'**   regaurdless of when the last time they were asked.                 **
		'**   If the Operator dose not select the Operation that is Due,         **
		'**   it will be difered and the Tester's INI will be Updated.           **
		'**   The system will not automaticly ask again until 24 hours.          **
		'**************************************************************************
		
		Dim iResponse As Short 'Response to Message Box
		Dim Cmdline As Object 'Command Line Argument
		Dim bImport As Boolean 'Import Operation Flag
		
		
        'If Application. Then End 'Allow only one instance of the FHDB Processor

        bOnStart = True
        Q = Chr(39)
        bImport = False
        bExport = False

        
        Cmdline = VB.Command()

        
        If UCase(Cmdline) = "EXPORT" Then
            frmExport.Show()
            Exit Sub

            bExport = True
            
        ElseIf UCase(Cmdline) = "IMPORT" Then
            bTerminateImporter = True
            Call Import()
            Exit Sub
            bImport = True
        End If

        'Get Info from Tester's INI file
        sXDate = GatherIniFileInformation("FHDB", "EXPORT_TIME", "")
        sDDI = GatherIniFileInformation("FHDB", "DDI", "")
        sIDate = GatherIniFileInformation("FHDB", "IMPORT_TIME", "")

        If sXDate <> "" Then
            dtXdate = DateTime.FromOADate(sXDate)
        Else 'If there is no Date in Tester's INI        
            frmViewer.Show() 'Launch Viewer
            frmViewer.MdiParent = MDIMain
            frmViewer.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmViewer.ControlBox = False
            frmViewer.MaximizeBox = False
            frmViewer.MinimizeBox = False
            frmViewer.ShowIcon = False
            frmViewer.Text = ""
            frmViewer.Dock = DockStyle.Fill
            Exit Sub 'Exit Procedure
        End If
        If sIDate <> "" Then
            dtIDate = DateTime.FromOADate(sIDate)
        Else
            dtIDate = #12:00:00 AM# 'If there is no Date in Tester's INI
        End If 'Assign Date Default
        If sDDI <> "" Then 'If there is no DDI, assign 0
            iDDI = CShort(sDDI)
        Else
            iDDI = 0
        End If

        Dim exportDate As New DateTime
        DateTime.TryParse(dtXdate, exportDate)
        Dim numberOfDaysSinceLastExport As Integer = DateDiff(DateInterval.Day, exportDate, DateTime.Today)
        If (numberOfDaysSinceLastExport > iDDI) Then
            bExport = True
        End If

        If DateTime.Compare(dtXdate, dtIDate) < 0 Then
            bImportExport = True 'EXPORT
        Else
            bImportExport = False 'IMPORT
        End If

        If bImport = True Then bImportExport = False 'IMPORT

        'Is Import or Export the next operation due
        If bImportExport = True Or bExport = True Then 'If Export.
            dtNext_ImportExport = DateAdd(Microsoft.VisualBasic.DateInterval.Day, iDDI, dtXdate) 'Calculate Next Export due date.
            If Now >= dtNext_ImportExport Or bExport = True Then 'If Export operation has been deferred,
                'Calculate Next Export Due Date - 24 hours from time of deferment.

                iResponse = MsgBox("FILE EXPORT OPERATION DUE." & vbCrLf & "Would you like to go to the Exporter now?" & vbCrLf & vbCrLf & "Press YES to launch the Exporter." & vbCrLf & "Press NO to launch the Viewer and defer the Export operation." & vbCrLf & "Press CANCEL to QUIT the Application and defer Export operation.", MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Information, "FILE EXPORT DUE.")

                If iResponse = 6 Then 'If YES
                    frmExport.Show() 'Launch Exporter
                    If bNoRecords = True Then
                        frmExport.Close()
                        bNoRecords = False 'Reset No Record switch to false
                    End If
                    Exit Sub

                ElseIf iResponse = 7 Then  'IF NO
                    Call UpdateIniFile("FHDB", "LASTCHECKED", "") 'Update Last time checked key
                    frmViewer.Show() 'Launch Viewer
                Else 'If CANCEL  (2)
                    Call UpdateIniFile("FHDB", "LASTCHECKED", "") 'Update Last time checked key
                    MDIMain.Close()
                    Exit Sub
                End If

            Else 'If Export not Due
                frmViewer.Show() 'Launch Viewer
            End If

        Else 'Import Operation is Due

            iResponse = MsgBox("FILE IMPORT OPERATION DUE." & vbCrLf & "Would you like to go to the Importer now?" & vbCrLf & vbCrLf & "YES:         Launch the IMPORTER." & vbCrLf & "NO:           Launch the VIEWER and defer the Import operation." & vbCrLf & "CANCEL:  QUIT and defer the Import operation.", MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Information, "FILE IMPORT DUE.")

            If iResponse = MsgBoxResult.Yes Then 'If YES
                If bNoRecords = True Then
                    bNoRecords = False 'Reset No Record switch to false
                    Exit Sub 'If no records, Abort
                Else
                    Call Import() 'Launch Importer   'DJoiner 03/11/02
                End If
            ElseIf iResponse = 7 Then  'IF NO
                Call UpdateIniFile("FHDB", "LASTCHECKED", "") 'Update Last time checked key
                frmViewer.Show() 'Launch Viewer
            Else 'If CANCEL  (2)
                Call UpdateIniFile("FHDB", "LASTCHECKED", "") 'Update Last time checked key
                MDIMain.Close()
                Exit Sub
            End If
        End If

    End Sub
	
	
	Sub Select_Last()
		'**********************************************************************
		'***                Set focus to last TextBox                       ***
		'**********************************************************************
		
		If frmQuery.txtValue(1).Visible Then
			frmQuery.txtValue(1).Focus()
			frmQuery.txtValue(1).SelectionStart = 0
			frmQuery.txtValue(1).SelectionLength = Len(frmQuery.txtValue(1).Text)
		ElseIf frmQuery.txtValue(2).Visible Then 
			frmQuery.txtValue(2).Focus()
			frmQuery.txtValue(2).SelectionStart = 0
			frmQuery.txtValue(2).SelectionLength = Len(frmQuery.txtValue(3).Text)
		End If
		
	End Sub
	
	
	Sub Show_Range()
		'**********************************************************************
		'*** Format frmQuery Form to accept Range search criteria from User ***
		'**********************************************************************
		
		frmQuery.cboValue.Visible = True
		frmQuery.lblCriteria.Visible = False
		frmQuery.cmdByValue.Visible = False
		frmQuery.cmdByRange.Visible = False
		frmQuery.cmdSearch.Visible = True
		frmQuery.lblFrom.Visible = True
		frmQuery.lblTo.Visible = True
		frmQuery.txtValue(3).Visible = True
		frmQuery.txtValue(2).Visible = True
		frmQuery.txtValue(1).Visible = False
		
	End Sub
	
	
	Sub Show_Value()
		'***********************************************************************
		'*** Format frmQuery Form to accept Single search criteria from User ***
		'***********************************************************************
		
		frmQuery.cboValue.Visible = True
		frmQuery.lblCriteria.Visible = True
		frmQuery.cmdByValue.Visible = False
		frmQuery.cmdByRange.Visible = False
		frmQuery.cmdSearch.Visible = True
		frmQuery.lblFrom.Visible = False
		frmQuery.lblTo.Visible = False
		frmQuery.txtValue(3).Visible = False
		frmQuery.txtValue(2).Visible = False
		frmQuery.txtValue(1).Visible = True
		
	End Sub
	
	
	Sub TimeStamp()
		'****************************************************************************
		'*** Routine to add a TimeStamp and and User to the begining of a Comment ***
		'****************************************************************************
		
		Dim dtDay As Date
		Dim sTimeStamp As String 'Date/Time/User Comment Header
		
		dtDay = Now 'Get Date and Time and Format.

        sUserName = UCase(System.Environment.UserName)

		sOldComment = frmViewer.txtComments.Text 'Save Existing Comments.
		
		frmViewer.txtComments.Text = "" 'Clear Comments Text Box.
		'If comment field = default then Clear default and don't skip a line.
		If Trim(sOldComment) = "" Then 'Find out if any Comment have been previously entered.
			sOldComment = "" 'If no previous comments - Clear Existing comments.
			'Do not Skip a line before adding First Comment.
			'Enter TimeDate Stamp and Users name in Comment TextBox.
			sTimeStamp = "Comment Entered at:  " & dtDay & "     By:  " & sUserName & vbCrLf
		Else
			'If a Previous Comment exists, seperate next Comment by a blank line.
			sTimeStamp = vbCrLf & vbCrLf & "Comment Entered at:  " & dtDay & "     By:  " & sUserName & vbCrLf
		End If
		
		sNewComment = sTimeStamp & frmComment.txtComment.Text
		
	End Sub
	
	
	Sub ValidateDate()
		'**********************************************************************
		'***    Verify the Query search date entered is a valid date.       ***
		'**********************************************************************
		
		If bDate Then
			If frmQuery.txtValue(1).Visible Then
				'(Single Criteria) If Entry is a Date, then it is valid
				If IsDate(frmQuery.txtValue(1).Text) Then
					bValidData = True
				Else
					MsgBox("Please Enter a valid Date",  , "Invalid Entry")
					frmQuery.txtValue(1).Text = ""
					frmQuery.txtValue(1).Focus()
					bValidData = False
				End If
			Else
				'(Range Criteria) If both Entries are Dates, then it is valid
				If IsDate(frmQuery.txtValue(2).Text) And IsDate(frmQuery.txtValue(3).Text) Then
					bValidData = True
				Else
					bValidData = False
					If IsDate(frmQuery.txtValue(2).Text) Then
						MsgBox("Please Enter a valid Date",  , "Invalid Entry")
						frmQuery.txtValue(3).Text = ""
						frmQuery.txtValue(3).Focus()
					Else
						MsgBox("Please Enter a valid Date",  , "Invalid Entry")
						frmQuery.txtValue(2).Text = ""
						frmQuery.txtValue(2).Focus()
					End If
				End If
			End If
		End If
		
	End Sub
	
	
	Sub CenterChildForm(ByRef ParentForm As System.Windows.Forms.Form, ByRef Childform As System.Windows.Forms.Form)
		'**************************************************************************
		'***  This Module Centers One Form With Respect To The Parent Form.     ***
		'**************************************************************************
		
		'Align Top
		Childform.Top = VB6.TwipsToPixelsY(((VB6.PixelsToTwipsY(ParentForm.Height) / 2) + VB6.PixelsToTwipsY(ParentForm.Top)) - VB6.PixelsToTwipsY(Childform.Height) / 2)
		'Align Left
		Childform.Left = VB6.TwipsToPixelsX(((VB6.PixelsToTwipsX(ParentForm.Width) / 2) + VB6.PixelsToTwipsX(ParentForm.Left)) - VB6.PixelsToTwipsX(Childform.Width) / 2)
		
	End Sub
	
	
	Sub CenterForm(ByRef Form As Object)
		'**************************************************************************
		'***  This Module Centers One Form With Respect To The User's Screen.   ***
		'**************************************************************************
		
		Form.Top = VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2 - Form.Height / 2
		Form.Left = VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Form.Width / 2
		
	End Sub
	
	
	Public Sub Delay(ByVal dSeconds As Double)
		'DESCRIPTION:
		'   Delays for a specified number of seconds.
		'   50 milli-second resolution.
		'   Minimum delay is about 50 milli-seconds.
		'PARAMETERS:
		'   dSeconds: The number of seconds to Delay.
		Dim t As Double 'Time in the format d.s where:
		'd = the number of days since 1900 and
		's = the fraction of a day with 10 milli-second resolution
		t = dGetTime()
		dSeconds = dSeconds / SECS_IN_DAY
		Do 
			System.Windows.Forms.Application.DoEvents()
		Loop While dGetTime() - t < dSeconds
		
	End Sub
	
	
	Public Function dGetTime() As Double
		'DESCRIPTION:
		'   Determines the time since 1900.
		'   Avoids errors caused by 'Timer' when crossing midnight.
		'PARAMETERS:
		'   None
		'RETURNS:
		'   A double-precision floating-point number in the format d.s where:
		'     d = the number of days since 1900 and
		'     s = the fraction of a day with 10 milli-second resolution
		
		'The CDbl(Now) function returns a number formatted d.s where:
		' d = the number of days since 1900 and
		' s = the fraction of a day with 10 milli-second resolution.
		'CLng(Now) returns only the number of days (the "d." part).
		'Timer returns the number of seconds since midnight with 10 mSec resoulution.
		'So, the following statement returns a composite double-float number representing
		' the time since 1900 with 10 mSec resoultion. (no cross-over midnight bug)
		
		dGetTime = CLng(DateSerial(Year(Now), Month(Now), VB.Day(Now)).ToOADate) + (VB.Timer() / SECS_IN_DAY)
		
	End Function
	
	
	Sub ShowAbout()
		'**********************************************************************
		'***       Show About Form, Center in form, Bring to top            ***
		'**********************************************************************
		
        'Display About Box
        frmAbout.Show()
		frmAbout.cmdOk.Visible = True
        'CenterChildForm(MDIMain, frmAbout)

		
	End Sub
	
	
	Public Sub StatusMsg(ByRef sStatusMessage As String)
		'**********************************************************************
		'****     Sets Status Message in the MDIMain Status Pannel         ****
		'**********************************************************************
		
        If sStatusMessage <> frmBackground.stbMain.Items.Item(0).Text Then
            frmBackground.stbMain.Items.Item(0).Text = sStatusMessage
        End If

    End Sub
	
	
	Public Sub MoveToBookmark()
		'**********************************************************************
		'****    When the Search is Canceled, Quary all records to fill    ****
		'****    the recordset. Move to the last record displayed, the     ****
		'****    Bookmarked Record.                                        ****
		'****                                    Dave Joiner  06/15/2001   ****
		'**********************************************************************
		
		Call ReturnAll() 'Query all records and load into RecordSet
		
		'Loop through the Recordset set until the Book Marked Record Number is found
		Do Until rstFaults.Fields("Record_Identifier").Value = nBookMark Or rstFaults.EOF
			rstFaults.MoveNext()
		Loop 
		'Display Book Marked Record in the Viewer
		Call Load_Record_Data()
		
	End Sub
	
	Public Function UDD_HD_Path(ByRef messageToUse As Short) As String
		Dim UDDPath As String
		Dim messageResponse As Short
		Dim hWnd As Integer
		
		If messageToUse = EXPORT_FILE Then
			exportMessage()
		ElseIf messageToUse = IMPORT_FILE Then 
			importMessage()
		End If
		
		UDDPath = BrowseFolders(hWnd, "Select a Folder", BrowseType.BrowseForFolders, FolderType.CSIDL_DESKTOP)
		
		If UDDPath = "" Then Exit Function
		
		
		UDD_HD_Path = UDDPath
		
	End Function
	
	Public Function BrowseFolders(ByRef hWndOwner As Integer, ByRef sMessage As String, ByRef Browse As BrowseType, ByVal RootFolder As FolderType) As String
		Dim Nullpos As Short
		Dim lpIDList As Integer
		Dim res As Integer
		Dim sPath As String
		Dim BInfo As BrowseInfo
		Dim RootID As Integer
		Dim MesBoxRes As MsgBoxResult
		Dim Temp As String
		Dim GoodPath As Boolean
		
		GoodPath = False
		
		Do 
			
			sPath = ""
			GoodPath = True
			SHGetSpecialFolderLocation(hWndOwner, RootFolder, RootID)
			BInfo.hWndOwner = hWndOwner
            BInfo.lpszTitle = 0
			BInfo.ulFlags = Browse
			
			If RootID <> 0 Then
				BInfo.pIDLRoot = RootID
			End If
			
			lpIDList = SHBrowseForFolder(BInfo)
			
			If lpIDList <> 0 Then
				sPath = New String(Chr(0), MAX_PATH)
				res = SHGetPathFromIDList(lpIDList, sPath)
				Call CoTaskMemFree(lpIDList)
				Nullpos = InStr(sPath, vbNullChar)
				
				If Nullpos <> 0 Then
					sPath = Left(sPath, Nullpos - 1)
				End If
			End If
			
			Temp = sPath
			
			If Temp = "" Then Exit Function
			
            'make sure the path for the export.zip is not in any system path
            If InStr(1, UCase(Temp), "C:\APS") = 0 And InStr(1, UCase(Temp), "C:\TEMP") = 0 And InStr(1, UCase(Temp), "C:\USERS") = 0 Then

                If InStr(1, UCase(Temp), "C:") <> 0 Then

                    MsgBox("""" & Temp & """" & " is not a valid directory for the Export.zip.", MsgBoxStyle.OkOnly)
                    'usb_flag = False
                    GoodPath = False
                End If
            End If


        Loop Until GoodPath = True
        If sPath.EndsWith("\") Then
            sPath = sPath.Remove(sPath.Length - 1)
        End If
		BrowseFolders = sPath
		
	End Function
	
	
	Function exportMessage() As Object
		Dim Message1 As String
		
        Message1 = "The next screen will allow you to choose where you want to send "
		Message1 = Message1 & "the FHDB to, for export." & vbCrLf & vbCrLf
        Message1 = Message1 & "If you choose the c:\ drive, there are only 3 locations that are allowed:" & vbCrLf & vbCrLf
        Message1 = Message1 & """" & "C:\aps\..." & """" & "   or   " & """" & "C:\Temp" & """" & "   or   "
        Message1 = Message1 & """" & "C:\Users\..." & """" & vbCrLf & vbCrLf
        Message1 = Message1 & "This program will then create a zip file   " & """" & "Export.zip" & """"
		Message1 = Message1 & "   at the location you selected." & vbCrLf & vbCrLf
		Message1 = Message1 & "For example you select   " & """" & "C:\Temp" & """" & "   you get:  "
        Message1 = Message1 & """" & "C:\Temp\Export.zip" & """" & vbCrLf & vbCrLf
		
        MsgBox(Message1, MsgBoxStyle.OkOnly, "Export Location Selection")
		
	End Function
	
	Function importMessage() As Short
		Dim Message1 As String
		
        Message1 = "The next screen will allow you to choose where you want to get "
        Message1 = Message1 & "the FHDB from (Import.zip), for import." & vbCrLf & vbCrLf
        Message1 = Message1 & "If you choose the c:\ drive, there are only 2 locations that are allowed:" & vbCrLf & vbCrLf
        Message1 = Message1 & """" & "C:\aps\..." & """" & "   or   " & """" & "C:\Temp" & """" & vbCrLf & vbCrLf
		
        MsgBox(Message1, MsgBoxStyle.OkOnly, "Import Location Selection")
		
	End Function
End Module