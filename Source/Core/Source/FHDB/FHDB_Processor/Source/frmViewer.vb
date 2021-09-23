Option Strict Off
Option Explicit On
Friend Class frmViewer
	Inherits System.Windows.Forms.Form
	'**********************************************************************
	'***    United States Marine Corps                                  ***
	'***                                                                ***
	'***    Nomenclature:   Form "frmViewer" : FHDB FHDB_Processor      ***
	'***    Written By:     Dave Joiner                                 ***
	'***    Purpose:                                                    ***
	'***  ECP-TETS-023                                                  ***
	'***    To allow the User to View selected Data from the Database   ***
	'***    in a structured manner.                                     ***
	'***    Also allows the User access other functions via a Menu.     ***
	'***  2.1.0                                                         ***
	'***    Sub Form_Load                                               ***
	'***    Removed the commented out line in the 657 version, 717 still***
	'***    had this line active, ran the program all possible ways did ***
	'***    not find any difference with or without this line of code.  ***
	'***    Sub Form_Unload                                             ***
	'***    added the call to close the data base                       ***
	'***    Sub mnuExport_Click                                         ***
	'***    Modified to unload export form on selecting cancel          ***
	'***    Sub mnuExit_Click                                           ***
	'***    Added the call to close the data base                       ***
	'**********************************************************************
	
	
	
	
	Private Sub frmViewer_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		'Center Form Inside MDI Form
		
		If bOnStart = True Then
			'Pop About Screen
            frmAbout.Show() : frmAbout.Refresh()
			Delay(1)
			frmAbout.Close() 'Remove About Screen
			bOnStart = False
		End If
		
		Width = VB6.TwipsToPixelsX(11010) ' Set width of form in hard code.
		Height = VB6.TwipsToPixelsY(7530) ' Set height of form in hard code.
		Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(MDIMain.Width) - VB6.PixelsToTwipsX(Width)) / 2) ' Center form horizontally.
        Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(MDIMain.Height) - VB6.PixelsToTwipsY(Height)) / 2) ' Center form vertically.
		
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Call ReturnAll() 'Query all records and load into RecordSet

        'set record slider max to number of records
        Call LastRecord() 'Move curser to Last record in RecordSet
        Me.SldRecord.Value = Me.SldRecord.Maximum
        System.Windows.Forms.Application.DoEvents()

	End Sub
	
	
	Private Sub frmViewer_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		
		Call CloseDB()
		
	End Sub
	
	Private Sub SldRecord_Scroll(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SldRecord.Scroll
		'Scrolls through selected recordset
		
		Dim nRecPosition As Integer 'Record Number Displayed
		Dim bForward As Boolean 'Switch to indicate direction
		Dim bBack As Boolean 'Recordset Curser is moving in
		Dim sngResult As Single 'Total Records returned/100
		Dim nResult As Integer 'Used to calculate every 100th record
		Dim bMoving As Boolean 'Flag to indicate Slider is moving
		Dim nSliderValue As Integer 'The Slider value when moving
		
		bMoving = False
		bLargeMove = False
		'Change Mouse Pointer to an Hourglass W/Arrow while moving through recordset
		Me.Cursor = System.Windows.Forms.Cursors.AppStarting
		nSliderValue = SldRecord.Value
		'Iterates through recordset until selected record is found
		Do Until nRecPosition = SldRecord.Value
			Call StatusMsg("Scrolling through records..........         Slider will move until selected record is found!!!")
			
			nRecPosition = rstFaults.Fields("Record_Identifier").Value 'Assign Value to Current Record Number
			
			If nRecPosition < SldRecord.Value Then 'If Current is < Selected
				bMoving = True
				bForward = True 'Set direction switch to Forward
				'Do not update records on Viewer if moving through over 100 Records
				If SldRecord.Value - nRecPosition > 100 Then
					bLargeMove = True
				Else
					bLargeMove = False
				End If
				If bBack = True Then Exit Do 'If direction changes, selected record number invalid
				
				sngResult = nRecPosition / 100
				nResult = CShort(sngResult)
				If nResult * 100 = nRecPosition Then 'Show every 100th record
					rstFaults.Move(SldRecord.Value)
					bLargeMove = False
				End If
				
				Call NextRecord() 'Move curser to Next record in RecordSet
				'If Slider value invalid, move to next record
				If rstFaults.Fields("Record_Identifier").Value > SldRecord.Value Then
					'Set Slider Value to match RecNum
					SldRecord.Value = rstFaults.Fields("Record_Identifier").Value
					Exit Do
				End If
			ElseIf nRecPosition > SldRecord.Value Then  'If Current is > Selected
				bMoving = True
				bBack = True 'Set direction switch to Backward
				'Do not update records on Viewer if moving through over 100 Records
				If nRecPosition - SldRecord.Value > 100 Then
					bLargeMove = True
				Else
					bLargeMove = True
					bLargeMove = False
				End If
				If bForward = True Then Exit Do 'If direction changes, selected record number invalid
				
				sngResult = nRecPosition / 100
				nResult = CShort(sngResult)
				If nResult * 100 = nRecPosition Then 'Show every 100th record
					bLargeMove = True
					bLargeMove = False
				End If
				
				Call PreviousRecord() 'Move curser to Previous record in RecordSet
				'If Slider value invalid, move to Previous record
				If rstFaults.Fields("Record_Identifier").Value < SldRecord.Value Then
					'Set Slider Value to match RecNum
					SldRecord.Value = rstFaults.Fields("Record_Identifier").Value
					Exit Do
				End If
			Else
				Exit Do
			End If
			
			'Safeties to get out of Loop
			If rstFaults.BOF Or rstFaults.EOF Then Exit Do
			If rstFaults.AbsolutePosition = SldRecord.Value Then Exit Do
			If rstFaults.AbsolutePosition = 0 Then Exit Do
			'If moving less than 100 records
			If bLargeMove = False Then
				System.Windows.Forms.Application.DoEvents() 'Show records as they are iterated through
			End If
		Loop 
		
		System.Windows.Forms.Application.DoEvents() 'Show records
		bForward = False 'Reset Switches
		bBack = False
		'Change Mouse Pointer back to an Arrow (Defalt)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Call StatusMsg("Selected Record Displayed.")
		
	End Sub
	
	
	Private Sub txtData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtData.Click
		Dim Index As Short = txtData.GetIndex(eventSender)
		'Prevents User from altering Text in the Comments field
		'Moves focus to an availible command button
		'Uses Next, Previous Ordering
		
		
		If cmdNext.Enabled Then
			
            cmdNext.Focus()
			
		ElseIf cmdPrevious.Enabled Then 
			
            cmdPrevious.Focus()
		End If
		
	End Sub
	
	
	Private Sub txtData_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtData.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim Index As Short = txtData.GetIndex(eventSender)
		'Prevents Operator from entering any data in Text Box
		
		KeyAscii = 1 'Nothing is entered
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	Private Sub txtComments_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComments.Click
		'Prevents User from altering Text in the Comments field
		'Moves focus to an availible command button
		'Uses Next, Previous Ordering
			
		If cmdNext.Enabled Then
			
            cmdNext.Focus()
			
		ElseIf cmdPrevious.Enabled Then 
			
            cmdPrevious.Focus()
		End If
		
	End Sub
	
	
	Private Sub txtComments_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtComments.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		'Prevents Operator from entering any data in Text Box
		
		KeyAscii = 1 'Nothing is entered
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	'***********************************************************************
	'****               Navigation Comand Buttons                       ****
	'***********************************************************************
    Private Sub cmdFirst_Click(sender As Object, e As EventArgs) Handles cmdFirst.Click
        Call FirstRecord() 'Move curser to First record in RecordSet
        SldRecord.Value = rstFaults.Fields("Record_Identifier").Value 'Set Slider Value to match RecNum

    End Sub
	
    Private Sub cmdLast_Click(sender As Object, e As EventArgs) Handles cmdLast.Click
        Call LastRecord() 'Move curser to Last record in RecordSet
        SldRecord.Value = rstFaults.Fields("Record_Identifier").Value 'Set Slider Value to match RecNum

    End Sub
	
    Private Sub cmdNext_Click(sender As Object, e As EventArgs) Handles cmdNext.Click
        Call NextRecord() 'Move curser to Next record in RecordSet
        SldRecord.Value = rstFaults.Fields("Record_Identifier").Value 'Set Slider Value to match RecNum
    End Sub
	
    Private Sub cmdPrevious_Click(sender As Object, e As EventArgs) Handles cmdPrevious.Click
        Call PreviousRecord() 'Move curser to Previous record in RecordSet
        SldRecord.Value = rstFaults.Fields("Record_Identifier").Value 'Set Slider Value to match RecNum

    End Sub
	
	
	'***********************************************************************
	'****                      Menu Items                               ****
	'***********************************************************************
	
	Public Sub mnuAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAbout.Click
		
        Call ShowAbout() 'Show About Form
		
	End Sub
	
	Public Sub mnuComment_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuComment.Click
		
		frmComment.Show() 'Launch Comment Form to allow User to enter a Comment
		Me.Hide()
		
	End Sub
	
	Public Sub mnuExportRecord_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuExportRecord.Click
		
		Call ExportRecord()
		
	End Sub
	
	Public Sub mnuExportRecordSet_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuExportRecordSet.Click
		Dim iResponse As Short 'Operator's responce to message box
		
		'If Record Set is greater than 25 records, Give the Operator a chance to cancel Operation.
		If rstFaults.RecordCount > 25 Then
			iResponse = MsgBox("There are currently " & rstFaults.RecordCount & " records in the record set." & vbCrLf & "Are you sure sure you want to Export these records to a text file?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "Record Set contains " & rstFaults.RecordCount & " Records")
			If iResponse = MsgBoxResult.Yes Then
				Call ExportRecordSet()
			Else
				Exit Sub
			End If
		Else
			Call ExportRecordSet()
		End If
		
	End Sub
	
	Public Sub mnuFirst_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFirst.Click
		
		Call FirstRecord() 'Move curser to First record in RecordSet
		SldRecord.Value = rstFaults.Fields("Record_Identifier").Value 'Set Slider Value to match RecNum
		
	End Sub
	
	Public Sub mnuLast_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuLast.Click
		
		Call LastRecord() 'Move curser to Last record in RecordSet
		SldRecord.Value = rstFaults.Fields("Record_Identifier").Value 'Set Slider Value to match RecNum
		
	End Sub
	
	Public Sub mnuLoadAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuLoadAll.Click
		
		Call ReturnAll() 'Query all records and load into RecordSet
		
	End Sub
	
	Public Sub mnuNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuNext.Click
		
		Call NextRecord() 'Move curser to Next record in RecordSet
		SldRecord.Value = rstFaults.Fields("Record_Identifier").Value 'Set Slider Value to match RecNum
		
	End Sub
	
	Public Sub mnuPrevious_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuPrevious.Click
		
		Call PreviousRecord() 'Move curser to Previous record in RecordSet
		SldRecord.Value = rstFaults.Fields("Record_Identifier").Value 'Set Slider Value to match RecNum
		
	End Sub
	
	Public Sub mnuSpecific_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuSpecific.Click
		
		frmQuery.Show() 'Launch Search Form to allow the User to Search
		'Database for specified criteria
	End Sub
	
	
	Public Sub mnuExport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuExport.Click
		
		gResponse = MsgBoxResult.OK
		bExport = True 'Set the Export Operation Flag
		frmExport.Show() 'Launches the Export Data Form
		If gResponse = MsgBoxResult.Cancel Then
			frmExport.Close()
		End If
		
	End Sub
	
	
	Public Sub mnuImport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuImport.Click
		
		bTerminateImporter = False
		Call Import()
		
	End Sub
	
	
	Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuExit.Click
		
		Call CloseDB()
		End 'Quit Application
		
	End Sub
	
	
	'***********************************************************************
	'****                   Status Line Messages                        ****
	'***********************************************************************
	
	Public Sub mnuFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFile.Click
		Call StatusMsg("Export a record or records as a text file.")
	End Sub
	
	Public Sub mnuOperation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuOperation.Click
		Call StatusMsg("Chose another Operation or Quit.")
	End Sub
	
	Public Sub mnuSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuSearch.Click
		nBookMark = Val(txtData(0).Text) 'Bookmark Current Record Number
		Call StatusMsg("Select the type of Search to Query the Database.")
	End Sub
	
	Public Sub mnuNavigation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuNavigation.Click
		Call StatusMsg("Navigate through the Records.")
	End Sub
	
	Public Sub mnuHelp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuHelp.Click
		Call StatusMsg("FHDB Help.")
	End Sub
	
	
	Private Sub txtData_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtData.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		Dim Index As Short = txtData.GetIndex(eventSender)
		'Keep Mouse Pointer an Arrow, Do Not change over Text Box
		txtData(Index).Cursor = System.Windows.Forms.Cursors.Arrow
		Call StatusMsg("Record Display Area.")
	End Sub
	
	Private Sub txtComments_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtComments.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		'Keep Mouse Pointer an Arrow, Do Not change over Text Box
		txtComments.Cursor = System.Windows.Forms.Cursors.Arrow
		Call StatusMsg("Record Display Area.")
	End Sub
	
	Private Sub cmdFirst_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		Call StatusMsg("Displays the First Record from the search results.")
	End Sub
	
	Private Sub cmdLast_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		Call StatusMsg("Displays the Last Record from the search results.")
	End Sub
	
	Private Sub cmdNext_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		Call StatusMsg("Displays the Next Record from the search results.")
	End Sub
	
	Private Sub cmdPrevious_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		Call StatusMsg("Displays the Previous Record from the search results.")
	End Sub
	
	Private Sub frmViewer_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		'If Status Message is Empty, Display generic message
		
		If sMessageString = "" Then 'If the record return string is empty
			Call StatusMsg("Database Viewer.") 'Display generic message.
		Else
			Call StatusMsg(sMessageString) 'Else Display record return String
		End If
		
	End Sub
   
    Private Sub frmViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = False

    End Sub

    Private Sub SldRecord_Scroll(sender As Object, e As ScrollEventArgs) Handles SldRecord.Scroll

    End Sub
End Class