Attribute VB_Name = "m_Main"
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
'**********************************************************************

Option Explicit


Public bNoRecord As Boolean         'Flag to indicate No Records in Database
Public bPassFail As Boolean         'Pass/Fail Status of record

Public sUOM As String               'Unit of Measure value
Public sNewComment As String        'New Comment entered
Public sOldComment As String        'Old, existing Comment

Public Q As String                  'Holds Date indicator value
Public bExport As Boolean           'Export Operation Flag
Public bDate As Boolean             'Flag to indicate Search is on a Date value
Public bValidData As Boolean        'Flag to indicate a valid date
Public bSearch(3) As Boolean        'Array to Flag valid Search Criteria

'Variables needed to convert data types from the Tester's INI
Public sXDate As String                 'Export Date from the Tester's INI
Public sDDI As String                   'DDI from the Tester's INI
Public dtXdate As Date                  'Export Date converted to Date/Time
Public iDDI As Integer                  'DDI converted to an Integer
Public sIDate As String                 'String value of Import Date
Public dtIDate As Date                  'Import Date converted to Date/Time
Public bImportExport As Boolean         'Flag to indicate an Import/Export Operation is due
Public dtNext_ImportExport As Date      'Date next Import/Export Operation Due

Public sMessageString As String         'Status Message to the Operator
Public bTerminateImporter As Boolean    'Flag to force the Importer to Teminate
Public bOnStart As Boolean              'Flag to indicate Program first started
Public iMax As Integer                  'Indicates Maximum Windows State
Public bLast As Boolean                 'Flag to indicate the Last Record in the Recordset
Public nBookMark As Long                'Record Number of the last record shown in the Viewer
Public sFileLocation As String          'File location where the database is stored or is to be stored.
Public gResponse As Integer             'Used to indicate the uers response to a msgbox

'Declarations for displaying a browse window
Private Type BrowseInfo
    hWndOwner As Long
    pIDLRoot As Long
    pszDisplayName As Long
    lpszTitle As Long
    ulFlags As Long
    lpfnCallback As Long
    lParam As Long
    iImage As Long
End Type

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

Private Const MAX_PATH = 260
Private Declare Sub CoTaskMemFree Lib "ole32.dll" (ByVal hMem As Long)
Private Declare Function lstrcat Lib "kernel32.dll" Alias "lstrcatA" (ByVal lpString1 As String, ByVal lpString2 As String) As Long
Private Declare Function SHBrowseForFolder Lib "shell32.dll" (lpbi As BrowseInfo) As Long
Private Declare Function SHGetPathFromIDList Lib "shell32.dll" (ByVal pidList As Long, ByVal lpBuffer As String) As Long
Private Declare Function SHGetSpecialFolderLocation Lib "shell32.dll" (ByVal hWndOwner As Long, ByVal nFolder As Long, ListId As Long) As Long

Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" _
                 (ByVal lpBuffer As String, nSize As Long) As Long

Const SECS_IN_DAY = 86400       'Seconds in a Day
Global Const EXPORT_FILE = 1    'Flag used for Message
Global Const IMPORT_FILE = 2    'Flag used for Message



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

    frmComment.txtComment.Text = ""     'Clear New Comment
    sNewComment = ""
    frmViewer.Show                      'Show Viewer
    frmViewer.WindowState = 2           'Maximized (inside MDI Container)
    frmViewer.SetFocus
    Unload frmComment                   'Unload Comment Form

End Sub


Sub Disable_Navigation()
'**********************************************************************
'***    Routine to Disable all Record Navigation Controls when      ***
'***    they are no longer useful.                                  ***
'***    EI: Only one record in ResultSet                            ***
'**********************************************************************

    frmViewer.cmdFirst.Enabled = False          'Disable all Navigational Controls
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

    frmViewer.mnuFirst.Enabled = False      'Disable MoveFirst and MovePrevious
    frmViewer.mnuPrevious.Enabled = False
    frmViewer.cmdFirst.Enabled = False
    frmViewer.cmdPrevious.Enabled = False

End Sub


Sub DisableLastNav()
'**********************************************************************
'***     Routine to Disable "Next" and "Last" Navigation commands   ***
'***     when a Record no longer exists.                            ***
'**********************************************************************
    
    frmViewer.mnuLast.Enabled = False       'Disable MoveLast and MoveNext
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
        frmViewer.SSPnlPassFail.BackColor = &HFF00&   'Outline Green
    Else
        frmViewer.txtData(8).Text = "FAIL"
        frmViewer.SSPnlPassFail.BackColor = &HFF&     'Outline Red
    End If

End Sub


Sub Enable_Navigation()
'**********************************************************************
'***     Routine to Enable all Record Navigation Controls when      ***
'***     they are useful.                                           ***
'***     EI: Many records in ResultSet                              ***
'**********************************************************************

    frmViewer.cmdFirst.Enabled = True           'Enable all Navigation controls
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

    frmViewer.mnuFirst.Enabled = True           'Disable MoveFirst and MovePrevious
    frmViewer.mnuPrevious.Enabled = True
    frmViewer.cmdFirst.Enabled = True
    frmViewer.cmdPrevious.Enabled = True

End Sub


Sub EnableLastNav()
'**********************************************************************
'***      Routine to Enable "Next" and "Last" Navigation commands   ***
'***      when a Record exists.                                     ***
'**********************************************************************

    frmViewer.mnuLast.Enabled = True            'Enable MoveLast and MoveNext
    frmViewer.mnuNext.Enabled = True
    frmViewer.cmdLast.Enabled = True
    frmViewer.cmdNext.Enabled = True

End Sub


Sub EnableSearch()
'**********************************************************************
'***    Enable [Search] button when valid search criteria has       ***
'***    been entered.                                               ***
'**********************************************************************

    If frmQuery.txtValue(1).Visible Then            'Single Criteria
        If bSearch(1) Then
            frmQuery.cmdSearch.Enabled = True
        End If
    Else
        If bSearch(2) And bSearch(3) Then           'Range Criteria
                frmQuery.cmdSearch.Enabled = True
        End If
    End If
    
End Sub


Sub FindUserName()
'*************************************************************************
'*** Routine to Get the Users name to ID Individual entering a Comment.***
'*** Logon name: Administrator, Maintenance or Operator                ***
'*************************************************************************

Dim lpBuffer As String * 255 'Input Buffer for the API Call
Dim nSize As Long 'Size of the Input Buffer for the API Call
Dim ReturnValue As Long 'Return Status of the API Call
Dim Frame As Integer 'Index of frames to "grant"/"refuse" access
Dim ChasSlot As Integer
    nSize& = 255 'Set Bufffer Size
    ReturnValue& = GetUserName(ByVal lpBuffer$, nSize&)
    'JDH V1.12 corrected
    sUserName = Left$(lpBuffer$, nSize& - 1) 'Remove Leading / Trailing Spaces
    sUserName = UCase(sUserName)
    
End Sub


Sub FormatSelectedValue()
'**********************************************************************
'***   When the User selects a field to Query, Form is Formatted    ***
'***   to User's Choice.                                            ***
'**********************************************************************
    
    bSearch(1) = False          'Set all Search Flags to False
    bSearch(2) = False
    bSearch(3) = False
    frmQuery.cmdSearch.Enabled = False          'Initiate the Search Command to Disabled
        
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
            frmQuery.ssfraPassFail.Visible = True           'Display Pas/Fail selector
            frmQuery.lblCriteria.Visible = False
            frmQuery.txtValue(1).Visible = False
            frmQuery.cmdSearch.Enabled = True
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
                    frmQuery.txtValue(1) = "MM/DD/YY HH:mm"     'Display Date/Time mask
                    bDate = True
                    frmQuery.txtValue(1).SetFocus
                Else
                    frmQuery.txtValue(2).Text = "MM/DD/YY HH:mm"
                    frmQuery.txtValue(3).Text = "MM/DD/YY HH:mm"
                    bDate = True
                    frmQuery.txtValue(2).SetFocus
                End If
                
            Else
            
                frmQuery.txtValue(1) = ""
                bDate = False
                frmQuery.txtValue(1).SetFocus
            End If
                             
        Case Else
            bDate = False
            If frmQuery.txtValue(3).Visible = False Then
                frmQuery.txtValue(1) = ""
                frmQuery.txtValue(1).SetFocus
            Else
                frmQuery.txtValue(3).Text = ""
                frmQuery.txtValue(2).Text = ""
                frmQuery.txtValue(2).SetFocus
            End If
            
        End Select
 
End Sub


Sub Initalize_cboValue()
'**********************************************************************
'***        Fill the cboBox with the correct value depending        ***
'***        on Users selection.                                     ***
'**********************************************************************

    'Fields that can be Queried when the User selects "Range"
    frmQuery.cboValue.AddItem ("Record Number"), 0
    frmQuery.cboValue.AddItem ("Start Date/Time"), 1
    frmQuery.cboValue.AddItem ("Intake Temperature"), 2
    'Additional fields that can be Queried when the User selects 'RANGE"
    If frmQuery.txtValue(2).Visible = False And frmQuery.txtValue(3).Visible = False Then
        frmQuery.cboValue.AddItem ("Pass/Fail Status"), 3
        frmQuery.cboValue.AddItem ("UUT Serial Number"), 4
        frmQuery.cboValue.AddItem ("UUT Revision"), 5
        frmQuery.cboValue.AddItem ("ID Serial Number"), 6
        frmQuery.cboValue.AddItem ("TPCCN"), 7
        frmQuery.cboValue.AddItem ("ERO"), 8
    End If
    
    frmQuery.cboValue.ListIndex = 0
    
End Sub


Sub Main()
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

Dim iResponse As Integer        'Response to Message Box
Dim Cmdline                     'Command Line Argument
Dim bImport As Boolean          'Import Operation Flag

    If App.PrevInstance Then End 'Allow only one instance of the FHDB Processor
    
    bOnStart = True
    Q = Chr(39)
    bImport = False
    bExport = False
    
    Cmdline = Command()
    
    If UCase(Cmdline) = "EXPORT" Then
        frmExport.Show
        Exit Sub
        
        bExport = True
    ElseIf UCase(Cmdline) = "IMPORT" Then
        bTerminateImporter = True
        Call Import
        Exit Sub
        bImport = True
    End If
    
    'Get Info from Tester's INI file
    sXDate = GatherIniFileInformation("FHDB", "EXPORT_TIME", "")
    sDDI = GatherIniFileInformation("FHDB", "DDI", "")
    sIDate = GatherIniFileInformation("FHDB", "IMPORT_TIME", "")
        
    'Convert ini information to proper data type
    If Len(sXDate) = 11 Then
        dtXdate = CDate(sXDate)
    Else                                            'If there is no Date in Tester's INI
        frmViewer.Show                              'Launch Viewer
        Exit Sub                                    'Exit Procedure
    End If
    If Len(sIDate) = 11 Then
        dtIDate = CDate(sIDate)
    Else
        dtIDate = #12:00:00 AM#                     'If there is no Date in Tester's INI
    End If                                          'Assign Date Default
    If sDDI <> "" Then                              'If there is no DDI, assign 0
        iDDI = CInt(sDDI)
    Else
        iDDI = 0
    End If
    
    If dtIDate > dtXdate Then
        bImportExport = True                            'EXPORT
    Else
        bImportExport = False                           'IMPORT
    End If
          
    If bImport = True Then bImportExport = False        'IMPORT
          
    'Is Import or Export the next operation due
    If bImportExport = True Or bExport = True Then                               'If Export.
        dtNext_ImportExport = DateAdd("d", iDDI, dtXdate)     'Calculate Next Export due date.
        If Now >= dtNext_ImportExport Or bExport = True Then  'If Export operation has been deferred,
                                                              'Calculate Next Export Due Date - 24 hours from time of deferment.
                            
        iResponse = MsgBox("FILE EXPORT OPERATION DUE." & vbCrLf _
                    & "Would you like to go to the Exporter now?" & vbCrLf & vbCrLf _
                    & "Press YES to launch the Exporter." & vbCrLf _
                    & "Press NO to launch the Viewer and defer the Export operation." & vbCrLf _
                    & "Press CANCEL to QUIT the Application and defer Export operation.", _
                      vbYesNoCancel + vbDefaultButton1 + vbInformation, _
                      "FILE EXPORT DUE.")
                                            
            If iResponse = 6 Then                               'If YES
                frmExport.Show                                  'Launch Exporter
                    If bNoRecords = True Then
                        Unload frmExport
                        bNoRecords = False                      'Reset No Record switch to false
                    End If
                Exit Sub
                
            ElseIf iResponse = 7 Then                           'IF NO
                Call UpdateIniFile("FHDB", "LASTCHECKED", "")   'Update Last time checked key
                frmViewer.Show                                  'Launch Viewer
            Else                                                'If CANCEL  (2)
                Call UpdateIniFile("FHDB", "LASTCHECKED", "")   'Update Last time checked key
                Unload MDIMain
                Exit Sub
            End If
                 
        Else                                                    'If Export not Due
            frmViewer.Show                                      'Launch Viewer
        End If
    
    Else                                                        'Import Operation is Due

        iResponse = MsgBox("FILE IMPORT OPERATION DUE." & vbCrLf _
                    & "Would you like to go to the Importer now?" & vbCrLf & vbCrLf _
                    & "YES:         Launch the IMPORTER." & vbCrLf _
                    & "NO:           Launch the VIEWER and defer the Import operation." & vbCrLf _
                    & "CANCEL:  QUIT and defer the Import operation.", _
                      vbYesNoCancel + vbDefaultButton1 + vbInformation, _
                      "FILE IMPORT DUE.")
                               
            If iResponse = vbYes Then       'If YES
                If bNoRecords = True Then
                   bNoRecords = False  'Reset No Record switch to false
                   Exit Sub            'If no records, Abort
                 Else
                    Call Import         'Launch Importer   'DJoiner 03/11/02
                End If
            ElseIf iResponse = 7 Then                           'IF NO
                Call UpdateIniFile("FHDB", "LASTCHECKED", "")   'Update Last time checked key
                frmViewer.Show                                  'Launch Viewer
            Else                                                'If CANCEL  (2)
                Call UpdateIniFile("FHDB", "LASTCHECKED", "")   'Update Last time checked key
                Unload MDIMain
                Exit Sub
            End If
    End If

End Sub


Sub Select_Last()
'**********************************************************************
'***                Set focus to last TextBox                       ***
'**********************************************************************

    If frmQuery.txtValue(1).Visible Then
        frmQuery.txtValue(1).SetFocus
        frmQuery.txtValue(1).SelStart = 0
        frmQuery.txtValue(1).SelLength = Len(frmQuery.txtValue(1).Text)
    ElseIf frmQuery.txtValue(2).Visible Then
        frmQuery.txtValue(2).SetFocus
        frmQuery.txtValue(2).SelStart = 0
        frmQuery.txtValue(2).SelLength = Len(frmQuery.txtValue(3).Text)
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
Dim sTimeStamp As String    'Date/Time/User Comment Header

    dtDay = Now                                 'Get Date and Time and Format.
    dtDay = Format(dtDay, "MM/DD/YY h:mm:Ss")
                
    Call FindUserName                               'Get Users Logon Name.

        sOldComment = frmViewer.txtComments.Text      'Save Existing Comments.

    frmViewer.txtComments.Text = ""                 'Clear Comments Text Box.
    'If comment field = default then Clear default and don't skip a line.
    If Trim(sOldComment) = "" Then      'Find out if any Comment have been previously entered.
        sOldComment = ""              'If no previous comments - Clear Existing comments.
                                    'Do not Skip a line before adding First Comment.
                                    'Enter TimeDate Stamp and Users name in Comment TextBox.
        sTimeStamp = "Comment Entered at:  " & dtDay _
                   & "     By:  " & sUserName & vbCrLf
    Else
                         'If a Previous Comment exists, seperate next Comment by a blank line.
        sTimeStamp = vbCrLf & vbCrLf & "Comment Entered at:  " _
                   & dtDay & "     By:  " & sUserName & vbCrLf
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
                MsgBox "Please Enter a valid Date", , "Invalid Entry"
                frmQuery.txtValue(1).Text = ""
                frmQuery.txtValue(1).SetFocus
                bValidData = False
            End If
        Else
            '(Range Criteria) If both Entries are Dates, then it is valid
            If IsDate(frmQuery.txtValue(2).Text) And IsDate(frmQuery.txtValue(3).Text) Then
                bValidData = True
            Else
                bValidData = False
                If IsDate(frmQuery.txtValue(2).Text) Then
                    MsgBox "Please Enter a valid Date", , "Invalid Entry"
                    frmQuery.txtValue(3).Text = ""
                    frmQuery.txtValue(3).SetFocus
                Else
                    MsgBox "Please Enter a valid Date", , "Invalid Entry"
                    frmQuery.txtValue(2).Text = ""
                    frmQuery.txtValue(2).SetFocus
                End If
            End If
        End If
    End If
    
End Sub


Sub CenterChildForm(ParentForm As Form, Childform As Form)
'**************************************************************************
'***  This Module Centers One Form With Respect To The Parent Form.     ***
'**************************************************************************
    
    'Align Top
    Childform.Top = ((ParentForm.Height / 2) + ParentForm.Top) - Childform.Height / 2
    'Align Left
    Childform.Left = ((ParentForm.Width / 2) + ParentForm.Left) - Childform.Width / 2
    
End Sub


Sub CenterForm(Form As Object)
'**************************************************************************
'***  This Module Centers One Form With Respect To The User's Screen.   ***
'**************************************************************************

    Form.Top = Screen.Height / 2 - Form.Height / 2
    Form.Left = Screen.Width / 2 - Form.Width / 2

End Sub


Public Sub Delay(ByVal dSeconds As Double)
  'DESCRIPTION:
  '   Delays for a specified number of seconds.
  '   50 milli-second resolution.
  '   Minimum delay is about 50 milli-seconds.
  'PARAMETERS:
  '   dSeconds: The number of seconds to Delay.
Dim t As Double     'Time in the format d.s where:
                    'd = the number of days since 1900 and
                    's = the fraction of a day with 10 milli-second resolution
    t = dGetTime()
    dSeconds = dSeconds / SECS_IN_DAY
    Do
        DoEvents
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
  
  dGetTime = CLng(DateSerial(Year(Now), Month(Now), Day(Now))) + (Timer / SECS_IN_DAY)
  
End Function


Sub ShowAbout()
'**********************************************************************
'***       Show About Form, Center in form, Bring to top            ***
'**********************************************************************

    'Display About Box
        Load frmAbout
        frmAbout.cmdOk.Visible = True
        CenterChildForm MDIMain, frmAbout
        frmAbout.Show 1
        
End Sub


Public Sub StatusMsg(sStatusMessage As String)
'**********************************************************************
'****     Sets Status Message in the MDIMain Status Pannel         ****
'**********************************************************************

    If sStatusMessage <> frmBackground.stbMain.Panels(1).Text Then
        frmBackground.stbMain.Panels(1).Text = sStatusMessage
    End If

End Sub


Public Sub MoveToBookmark()
'**********************************************************************
'****    When the Search is Canceled, Quary all records to fill    ****
'****    the recordset. Move to the last record displayed, the     ****
'****    Bookmarked Record.                                        ****
'****                                    Dave Joiner  06/15/2001   ****
'**********************************************************************

    Call ReturnAll              'Query all records and load into RecordSet
    
    'Loop through the Recordset set until the Book Marked Record Number is found
    Do Until rstFaults!Record_Identifier = nBookMark Or rstFaults.EOF
        rstFaults.MoveNext
    Loop
    'Display Book Marked Record in the Viewer
    Call Load_Record_Data
    
End Sub

Public Function UDD_HD_Path(messageToUse As Integer) As String
    Dim UDDPath As String
    Dim messageResponse As Integer
    Dim hWnd As Long

    If messageToUse = EXPORT_FILE Then
        exportMessage
    ElseIf messageToUse = IMPORT_FILE Then
        importMessage
    End If

    UDDPath = BrowseFolders(hWnd, "Select a Folder", BrowseForFolders, CSIDL_DESKTOP)

    If UDDPath = "" Then Exit Function


    UDD_HD_Path = UDDPath

End Function

Public Function BrowseFolders(hWndOwner As Long, sMessage As String, Browse As BrowseType, ByVal RootFolder As FolderType) As String
    Dim Nullpos As Integer
    Dim lpIDList As Long
    Dim res As Long
    Dim sPath As String
    Dim BInfo As BrowseInfo
    Dim RootID As Long
    Dim MesBoxRes As VbMsgBoxResult
    Dim Temp As String
    Dim GoodPath As Boolean
    
    GoodPath = False
    
    Do

        sPath = ""
        GoodPath = True
        SHGetSpecialFolderLocation hWndOwner, RootFolder, RootID
        BInfo.hWndOwner = hWndOwner
        BInfo.lpszTitle = lstrcat(sMessage, "")
        BInfo.ulFlags = Browse
        
        If RootID <> 0 Then
            BInfo.pIDLRoot = RootID
        End If
        
        lpIDList = SHBrowseForFolder(BInfo)
        
        If lpIDList <> 0 Then
            sPath = String(MAX_PATH, 0)
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
       If InStr(1, UCase(Temp), "C:\APS") = 0 And InStr(1, UCase(Temp), "C:\DOCUMENT") = 0 And _
          InStr(1, UCase(Temp), "C:\TEMP") = 0 Then
           
           If InStr(1, UCase(Temp), "C:") <> 0 Then
                        
                MsgBox """" & Temp & """" & " is not a valid directory for the Export.zip.", vbOKOnly
                'usb_flag = False
                GoodPath = False
            End If
        End If


    Loop Until GoodPath = True
    
    BrowseFolders = sPath
    
End Function


Function exportMessage()
    Dim Message1 As String
    
    Message1$ = "The next screen will allow you to choose where you want to extract "
    Message1$ = Message1$ & "the FHDB to, for export." & vbCrLf & vbCrLf
    Message1$ = Message1$ & "If you choose the c:\ drive, there are only 3 locations that are allowed:" & vbCrLf & vbCrLf
    Message1$ = Message1$ & """" & "C:\aps\..." & """" & "   or   " & """" & "C:\Temp" & """" & "   or   " & """" & "C:\Documents and Settings\..." & """" & vbCrLf & vbCrLf
    Message1$ = Message1$ & "This program will then create a directory   " & """" & "FHDB_Files" & """"
    Message1$ = Message1$ & "   at the location you selected." & vbCrLf & vbCrLf
    Message1$ = Message1$ & "For example you select   " & """" & "C:\Temp" & """" & "   you get:  "
    Message1$ = Message1$ & """" & "C:\Temp\FHDB_Files" & """" & vbCrLf & vbCrLf
    
    MsgBox Message1, vbOKOnly, "FHDB Location Selection"
    
End Function

Function importMessage() As Integer
    Dim Message1 As String
    
    Message1$ = "The next screen will allow you to choose where you want to extract "
    Message1$ = Message1$ & "the FHDB from, for import." & vbCrLf & vbCrLf
    Message1$ = Message1$ & "If you choose the c:\ drive, there are only 3 locations that are allowed:" & vbCrLf & vbCrLf
    Message1$ = Message1$ & """" & "C:\aps\..." & """" & "   or   " & """" & "C:\Temp" & """" & "   or   " & """" & "C:\Documents and Settings\..." & """" & vbCrLf & vbCrLf
    Message1$ = Message1$ & "This program will then create a directory   " & """" & "FHDB_Files" & """"
    Message1$ = Message1$ & "   at the location you selected." & vbCrLf & vbCrLf
    Message1$ = Message1$ & "For example you select   " & """" & "C:\Temp" & """" & "   you get:  "
    Message1$ = Message1$ & """" & "C:\Temp\FHDB_Files" & """" & vbCrLf & vbCrLf

    MsgBox Message1, vbOKOnly, "FHDB Location Selection"
 
End Function
