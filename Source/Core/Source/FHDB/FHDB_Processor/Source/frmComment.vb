Option Strict Off
Option Explicit On
Friend Class frmComment
	Inherits System.Windows.Forms.Form
	'**********************************************************************
	'***    United States Marine Corps                                  ***
	'***                                                                ***
	'***    Nomenclature:   Form "frmCommnet" : FHDB_Processor          ***
	'***    Written By:     Dave Joiner                                 ***
	'***    Purpose:                                                    ***
	'***  ECP-TETS-023                                                  ***
	'***    Allows the User to add a Comment to an existing record.     ***
	'***    A TimeDate Stamp as well as the Users Log On Name           ***
	'***    are automaticly captured and inserted into a new Comment.   ***
	'***    The only way to delete this Header is to Terminate          ***
	'***    new Comment.                                                ***
	'***    This method will allow anyone to enter a Comment but        ***
	'***    NO ONE will be able to alter or Delete a Comment.           ***
	'***    Allowing a for a greater degree of Data Integrity.          ***
	'**********************************************************************



    Private Sub _cmdAction_0_Click(sender As Object, e As EventArgs) Handles _cmdAction_0.Click
        If Trim(Me.txtComment.Text) = "" Then
            Call CloseComment()
            Exit Sub
        Else
            CharacterInString((Trim(Me.txtComment.Text)))
        End If
    End Sub

    Private Sub _cmdAction_1_Click(sender As Object, e As EventArgs) Handles _cmdAction_1.Click
        txtComment.Text = "" 'Clear TextBox
        txtComment.Focus() 'Set Focus on TextBox
        _cmdAction_0.Enabled = False 'Disable [Save] button
    End Sub

    Private Sub _cmdAction_2_Click(sender As Object, e As EventArgs) Handles _cmdAction_2.Click
        Dim iResponse As Short

        'If no Comment was Entered, do not Warn User
        If Me.txtComment.Text = "" Or _cmdAction_0.Enabled = False Then
            Call CloseComment()
            Exit Sub
        Else 'Comment was Entered, Warn User Comment will Not be Saved
            iResponse = MsgBox("You will loose all data entered!!" & vbCrLf & "Are you sure you want to Exit?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Exit Comment Form?")
            If iResponse = 6 Then 'If Yes, Close
                Call CloseComment()
            Else 'If No, Resume
                txtComment.Focus()
            End If
        End If
    End Sub

	Private Sub frmComment_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		'Set up Form for User entry when Loaded
		
		Width = VB6.TwipsToPixelsX(11010) 'Set width of form in hard code.
		Height = VB6.TwipsToPixelsY(7530) 'Set height of form in hard code.
		Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(MDIMain.Width) - VB6.PixelsToTwipsX(Width)) / 2) 'Center form horizontally.
		Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(MDIMain.Height) - VB6.PixelsToTwipsY(Height)) / 2) 'Center form vertically.
		txtRecNum.Text = frmViewer.txtData(0).Text 'Insert Current Record number in TextBox
		sOldComment = frmViewer.txtComments.Text 'Save Existing Comments.
		
        _cmdAction_0.Enabled = False 'Disable [Save] button
		
	End Sub

	Private Sub txtComment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComment.TextChanged
		
		If Trim(Me.txtComment.Text) <> "" Then
            _cmdAction_0.Enabled = True
		Else
            _cmdAction_0.Enabled = False
		End If
		
	End Sub
	
	
	Private Sub txtRecNum_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecNum.Enter
		'Prevent User from Changing the Record Number
		
		txtComment.Focus()
		
	End Sub
	
	
	'***********************************************************************
	'****                   Status Line Messages                        ****
	'***********************************************************************
	
	Private Sub frmComment_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		Call StatusMsg("Add Comment to  Record Number: " & Me.txtRecNum.Text)
	End Sub
	
	Private Sub txtRecNum_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtRecNum.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		txtRecNum.Cursor = System.Windows.Forms.Cursors.Arrow
		Call StatusMsg("Current  Record Number")
	End Sub
	
	Private Sub txtComment_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtComment.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		Call StatusMsg("Enter Comment for Record Number: " & Me.txtRecNum.Text)
	End Sub
	
	Private Sub cmdAction_MouseMove(ByRef Index As Short, ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
		
		Select Case Index
			
			Case 0 'Save Comment, Update Record
				Call StatusMsg("Saves Comment to Record Number: " & Me.txtRecNum.Text & "   Closes form and returns to Viewer.")
				
			Case 1 'Cancel, Clear Textbox
				Call StatusMsg("Clears text entered and resets Date/Time line.")
				
			Case 2 'Exit, Unload form, Do not Save, Show Viewer
				Call StatusMsg("Aborts Operation and returns to the Viewer, Comment will not be added.")
				
		End Select
		
	End Sub

   
End Class