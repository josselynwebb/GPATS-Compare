Option Strict Off
Option Explicit On
Friend Class frmQuery
	Inherits System.Windows.Forms.Form
	'**********************************************************************
	'***    United States Marine Corps                                  ***
	'***                                                                ***
	'***    Nomenclature:   Form "frmQuery" : FHDB FHDB_Processor       ***
	'***    Written By:     Dave Joiner                                 ***
	'***    Purpose:                                                    ***
	'***  ECP-TETS-023                                                  ***
	'***    Form allows the User to search for information              ***
	'***    in the Database based on User entered criteria.             ***
	'***    Search results are returned to the Viewer.                  ***
	'**********************************************************************
	
	
	
	
	Private Sub cboValue_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboValue.SelectedIndexChanged
		
		Call FormatSelectedValue() 'Format Criteria TxtBoxes to accept
		'Criteria based on Search Field selected
	End Sub

    Private Sub cmdByRange_Click(sender As Object, e As EventArgs) Handles cmdByRange.Click
        Call Show_Range() 'Format Search Form to accept a Range
        Call Initalize_cboValue() 'of values for search criteria
    End Sub

    Private Sub cmdByValue_Click(sender As Object, e As EventArgs) Handles cmdByValue.Click
        Call Show_Value() 'Format Search Form to accept a single
        Call Initalize_cboValue() 'value for Search criteria
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

        StatusMsg(("Database Viewer - Search Aborted."))
        Call ClearSearch() 'Clear all Search data to default
        Call MoveToBookmark() 'Display Last Record
        frmViewer.Show() 'Unload Search Form and Show Viewer
        Me.Close()
    End Sub

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Call ValidateDate() 'Check that the Date/Dates Entered are Valid
        If bValidData Then
            StatusMsg(("Initiating Search.............."))
            Search_DB()
            'If Record dosen't exist, don't clear
            If bNoRecord = False Then 'A record was found, Load it into Viewer
                frmViewer.Show()
                Me.Close()
            End If
            If bNoRecord = True Then
                StatusMsg(("Enter Criteria to Search on."))
                bNoRecord = False
                Exit Sub
            End If
            bNoRecord = False 'Set the No Record Found Switch to False
        End If

        bValidData = True 'Set the Valid Date Switch to True

        Call SetMinMax()

    End Sub

	Private Sub frmQuery_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		
		Call ClearSearch()
		Me.cmdSearch.Enabled = False 'Initiate the Search Comand to Disabled
		bValidData = True 'Initiate Valid Date Variable
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal 'Force Windows State to Normal
		Me.Refresh() 'Refresh the RecordSet
		StatusMsg(("Select the type of search you would like to perform."))
		
	End Sub
	
	
	Private Sub frmQuery_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		
		frmViewer.WindowState = System.Windows.Forms.FormWindowState.Maximized 'Force the Windows State to Maximized
		frmViewer.Refresh() 'Refresh the Viewer Form Image
		
	End Sub
	
	
	Private Sub txtValue_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtValue.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim iIndex As Short = txtValue.GetIndex(eventSender)
		'**********************************************************************
		'***        Verify a valid Date is entered and force formating,     ***
		'***        limit number of characters entered.                     ***
		'**********************************************************************
		
		Dim iLen As Short 'Variable to hold Length
		Dim sCharacter As String 'Variable to hold String value
		
		
		If txtValue(iIndex).Text = "MM/DD/YY HH:mm" Then 'Clear Date mask
			txtValue(iIndex).Text = ""
		End If
		
		iLen = Len(txtValue(iIndex).Text) 'Get the length of text in TextBox
		
		If KeyAscii = 13 Then 'If User press "Enter" execute Search
			
            cmdSearch.PerformClick()

			GoTo EventExitSub
		End If
		
		If KeyAscii = 8 Then 'Allow User to Backspace
			GoTo EventExitSub
		End If
		
		'**************************************************************
		
		Select Case cboValue.SelectedIndex
			
			Case 0 'Record Indentifier, allow only numbers, no spaces
				If KeyAscii < 48 Or KeyAscii > 57 Then
					KeyAscii = 1 'Nothing is entered
					MsgBox("Please Enter a number",  , "Invalid Entry")
					GoTo EventExitSub
				End If
				bSearch(iIndex) = True
			Case 1 'Start Date/Time
				'If Entry is a date, force validation/format
				If bDate Then
					Select Case iLen + 1
						'First filter out nonnumeric values
						Case 1, 2, 4, 5, 7, 8, 10, 11, 13, 14
							'asc(48-57) Only 0-9 (Numbers) Allowed here
							If KeyAscii < 48 Or KeyAscii > 57 Then
								KeyAscii = 1 'Nothing is entered
								MsgBox("Please Enter a number",  , "Invalid Entry")
								GoTo EventExitSub
							End If
							'If there are at least 8 characters
							If iLen = 7 Then 'Enable Search Command
								bSearch(iIndex) = True
								bDaySearch = True
							End If
							
							If iLen > 8 Then
								bDaySearch = False
							End If
							
							
							'********************************************************************
							'******             Validate Date Characters                    *****
							'******     Does not validate number of days in different       *****
							'******     Months nor Leap year.                               *****
							'******     Just a generic validation routine.                  *****
							'********************************************************************
							Select Case iLen + 1
								'Only allow 0-1 to be entered   '(0)1 - (1)2
								Case 1
									If KeyAscii < 48 Or KeyAscii > 49 Then
										KeyAscii = 1 'Nothing is entered
										GoTo EventExitSub
									End If
									'Only allow 0-2 if first Month char is a "1"
								Case 2 '1(0) - 1(2)
									sCharacter = Mid(txtValue(iIndex).Text, 1, 1)
									If sCharacter = "1" Then
										If KeyAscii < 48 Or KeyAscii > 50 Then
											KeyAscii = 1 'Nothing is entered
											GoTo EventExitSub
										End If
									End If
									'Only allow 0-3 to be entered   '(0)1 - (3)1
								Case 4
									If KeyAscii < 48 Or KeyAscii > 51 Then
										KeyAscii = 1 'Nothing is entered
										GoTo EventExitSub
									End If
									'Only allow 0-1 if first Day char is a "3"
								Case 5 '3(0) - 3(1)
									sCharacter = Mid(txtValue(iIndex).Text, 4, 1)
									If sCharacter = "3" Then
										If KeyAscii < 48 Or KeyAscii > 49 Then
											KeyAscii = 1 'Nothing is entered
											GoTo EventExitSub
										End If
									End If
									'Only allow 0-2 to be entered   '(0)1 - (2)4 hours
								Case 10
									If KeyAscii < 48 Or KeyAscii > 50 Then
										KeyAscii = 1 'Nothing is entered
										GoTo EventExitSub
									End If
									'Only allow 0-5 to be entered  '(0)0 - (5)9 minutes
								Case 13
									If KeyAscii < 48 Or KeyAscii > 53 Then
										KeyAscii = 1 'Nothing is entered
										GoTo EventExitSub
									End If
									
							End Select
							'*******************************************************************
							
						Case 3, 6
							'asc(45 or 47) = "-" or "/" This is the only Entry allowed here
							If KeyAscii <> 45 Or KeyAscii <> 47 Then
								KeyAscii = 47
							End If
							
						Case 9
							'asc(32) = " " This is the only Entry allowed here
							If KeyAscii <> 32 Then
								KeyAscii = 32
							End If
							
						Case 12
							'asc(58) = ":" This is the only Entry allowed here
							If KeyAscii <> 58 Then
								KeyAscii = 58
							End If
							
						Case 15
							'If only one Text Box is used for Search Criteria
							'Using the By Value Selection, If the Length is over 14,
							'do not enter character and trigger Search command
							If txtValue(1).Visible = True Then
								KeyAscii = 1
								
                                cmdSearch.PerformClick()
								GoTo EventExitSub
							End If
							'If both Text Boxes are used for Search Criteria
							'Using the By Range Selection, If the Length of either textbox is over 14,
							'Check to varify a valid date has been entered. If so,
							'do not enter character and trigger Search command
							If Len(txtValue(2).Text) = 14 And Len(txtValue(3).Text) = 14 Then
								If IsDate(txtValue(2).Text) And IsDate(txtValue(3).Text) Then
									KeyAscii = 1
									
                                    cmdSearch.PerformClick()
									GoTo EventExitSub
								End If
								KeyAscii = 1
								If IsDate(txtValue(2).Text) Then
									txtValue(3).Focus()
								Else
									txtValue(2).Focus()
								End If
							End If
							
						Case Else
							'MsgBox to inform User that Entry is not valid
							MsgBox("Non valid entry",  , "Invalid Entry")
					End Select
					
				End If
				
			Case 2 'Intake Temperature, allow only numbers and Asc(45) "-" or Asc(46) "."
				Debug.Print(KeyAscii)
				If KeyAscii < 45 Or KeyAscii > 46 Then
					If KeyAscii < 48 Or KeyAscii > 57 Then
						KeyAscii = 1
						GoTo EventExitSub
					End If
				End If
				bSearch(iIndex) = True
				
				'**************************************************************
				'***   Varify the following fields are Length validated     ***
				'**************************************************************
			Case 4 'UUT_Serial_No      'Limit to 15 characters
				If iLen > 14 Then
					KeyAscii = 1 'Nothing is entered
					
                    cmdSearch.Focus()
					GoTo EventExitSub
				End If
				bSearch(iIndex) = True
			Case 5, 6 'UUT_Revision and ID_Serial_No       'Limit to 10 characters
				If iLen > 9 Then
					KeyAscii = 1 'Nothing is entered
					
                    cmdSearch.Focus()
					GoTo EventExitSub
				End If
				bSearch(iIndex) = True
			Case 7 'TPCCN      'Limit to 16 characters
				If iLen > 15 Then
					KeyAscii = 1 'Nothing is entered
					
                    cmdSearch.Focus()
					GoTo EventExitSub
				End If
				bSearch(iIndex) = True
			Case 8 'ERO        'Limit to 5 characters
				If iLen > 4 Then
					KeyAscii = 1 'Nothing is entered
					
                    cmdSearch.Focus()
					GoTo EventExitSub
				End If
				bSearch(iIndex) = True
		End Select
		
		Call EnableSearch()
		
EventExitSub: 
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	'***********************************************************************
	'****                   Status Line Messages                        ****
	'***********************************************************************
	
	Private Sub frmQuery_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		Call StatusMsg("Search Database.")
	End Sub
	
	Private Sub cmdByRange_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		Call StatusMsg("Search database to return records between two values.")
	End Sub
	
	Private Sub cmdByValue_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		StatusMsg(("Search database by a single value."))
	End Sub
	
	Private Sub cmdCancel_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		Call StatusMsg("Aborts Search Operation and returns to the Viewer.")
	End Sub
	
	Private Sub cmdSearch_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		Call StatusMsg("Searches Database based selected criteria.")
	End Sub
	
	Private Sub ssoptPassFail_MouseMove(ByRef Index As Short, ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		Call StatusMsg("Select Pass or Fail.")
	End Sub
	
	Private Sub txtValue_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtValue.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		Dim Index As Short = txtValue.GetIndex(eventSender)
		
		Select Case Index
			
			Case 1 'Single entry Criteria
				Call StatusMsg("Enter Criteria to Search on.")
				
			Case 2 'From on Range
				Call StatusMsg("Enter Start value to base search on.")
				
			Case 3 'To on Range
				Call StatusMsg("Enter Ending value to base search on.")
				
		End Select
		
	End Sub
	
	Private Sub cboValue_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboValue.Enter
		Call StatusMsg("Select Field to base Search on.")
	End Sub

    Private Sub _ssoptPassFail_0_CheckedChanged(sender As Object, e As EventArgs) Handles _ssoptPassFail_0.CheckedChanged
        If Me._ssoptPassFail_0.Checked Then
            Me._ssoptPassFail_1.Checked = False
        End If
    End Sub

    Private Sub _ssoptPassFail_1_CheckedChanged(sender As Object, e As EventArgs) Handles _ssoptPassFail_1.CheckedChanged
        If Me._ssoptPassFail_1.Checked Then
            Me._ssoptPassFail_0.Checked = False
        End If
    End Sub
End Class