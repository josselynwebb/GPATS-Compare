VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmComment 
   Caption         =   "Operator Comments"
   ClientHeight    =   6735
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   10890
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MDIChild        =   -1  'True
   ScaleHeight     =   6735
   ScaleWidth      =   10890
   Begin VB.TextBox txtRecNum 
      Alignment       =   2  'Center
      BorderStyle     =   0  'None
      Height          =   220
      Left            =   9405
      TabIndex        =   1
      Top             =   660
      Width           =   1050
   End
   Begin Threed.SSPanel sspnlRecNum 
      Height          =   295
      Left            =   9360
      TabIndex        =   6
      Top             =   600
      Width           =   1135
      _Version        =   65536
      _ExtentX        =   2002
      _ExtentY        =   520
      _StockProps     =   15
      BackColor       =   16777215
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.11
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BevelWidth      =   2
      BorderWidth     =   0
      BevelOuter      =   0
      BevelInner      =   1
      FloodColor      =   16776960
   End
   Begin Threed.SSCommand cmdAction 
      Height          =   345
      Index           =   2
      Left            =   9480
      TabIndex        =   3
      Top             =   6120
      Width           =   1140
      _Version        =   65536
      _ExtentX        =   2020
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "C&lose"
   End
   Begin VB.TextBox txtComment 
      BorderStyle     =   0  'None
      Height          =   6255
      Left            =   200
      MultiLine       =   -1  'True
      ScrollBars      =   3  'Both
      TabIndex        =   0
      Top             =   240
      Width           =   8880
   End
   Begin Threed.SSCommand cmdAction 
      Height          =   345
      Index           =   0
      Left            =   9480
      TabIndex        =   4
      Top             =   5040
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "Sa&ve"
   End
   Begin Threed.SSCommand cmdAction 
      Height          =   345
      Index           =   1
      Left            =   9480
      TabIndex        =   5
      Top             =   5580
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "&Clear"
   End
   Begin Threed.SSPanel sspnlComment 
      Height          =   6455
      Left            =   100
      TabIndex        =   7
      Top             =   100
      Width           =   9050
      _Version        =   65536
      _ExtentX        =   15963
      _ExtentY        =   11386
      _StockProps     =   15
      BackColor       =   16777215
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.26
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BevelWidth      =   2
      BorderWidth     =   0
      BevelOuter      =   0
      BevelInner      =   1
      FloodColor      =   16776960
   End
   Begin VB.Label lblRecNum 
      AutoSize        =   -1  'True
      Caption         =   "Record Number"
      Height          =   195
      Left            =   9360
      TabIndex        =   2
      Top             =   240
      Width           =   1125
   End
End
Attribute VB_Name = "frmComment"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
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


Option Explicit


Private Sub cmdAction_Click(Index As Integer)
'Routine to take the appropriate Action based on the Users Selection

Dim iResponse As Integer

    Select Case Index
    
        Case 0          '[Save] Comment, Update Record
            
            If Trim(frmComment.txtComment.Text) = "" Then
                Call CloseComment
                Exit Sub
            Else
                CharacterInString (Trim(frmComment.txtComment.Text))
            End If

        Case 1          '[Clear], Clear Textbox
            
            txtComment.Text = ""        'Clear TextBox
            txtComment.SetFocus         'Set Focus on TextBox
            cmdAction(0).Enabled = False                 'Disable [Save] button
        Case 2          '[Close], Unload form, Do not Save, Show Viewer
                        'If no Comment was Entered, do not Warn User
            If frmComment.txtComment.Text = "" Or cmdAction(0).Enabled = False Then
                Call CloseComment
                Exit Sub
            Else        'Comment was Entered, Warn User Comment will Not be Saved
                iResponse = MsgBox("You will loose all data entered!!" & vbCrLf _
                                & "Are you sure you want to Exit?", vbCritical + vbYesNo _
                                + vbDefaultButton2, "Exit Comment Form?")
                If iResponse = 6 Then       'If Yes, Close
                    Call CloseComment
                Else                        'If No, Resume
                    txtComment.SetFocus
                End If
            End If
            
        End Select
    
End Sub


Private Sub Form_Load()
'Set up Form for User entry when Loaded
        
    Width = 11010                                 'Set width of form in hard code.
    Height = 7530                                 'Set height of form in hard code.
    Left = (MDIMain.Width - Width) / 2            'Center form horizontally.
    Top = (MDIMain.Height - Height) / 2           'Center form vertically.
    txtRecNum.Text = frmViewer.txtData(0).Text    'Insert Current Record number in TextBox
    sOldComment = frmViewer.txtComments.Text      'Save Existing Comments.
    cmdAction(0).Enabled = False                  'Disable [Save] button
   
End Sub


Private Sub txtComment_Change()
    
    If Trim(frmComment.txtComment.Text) <> "" Then
        cmdAction(0).Enabled = True
    Else
        cmdAction(0).Enabled = False
    End If
    
End Sub


Private Sub txtRecNum_GotFocus()
'Prevent User from Changing the Record Number

    txtComment.SetFocus

End Sub


'***********************************************************************
'****                   Status Line Messages                        ****
'***********************************************************************

Private Sub Form_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Add Comment to  Record Number: " & frmComment.txtRecNum.Text)
End Sub

Private Sub txtRecNum_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    txtRecNum.MousePointer = 1
    Call StatusMsg("Current  Record Number")
End Sub

Private Sub txtComment_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Enter Comment for Record Number: " & frmComment.txtRecNum.Text)
End Sub

Private Sub cmdAction_MouseMove(Index As Integer, Button As Integer, Shift As Integer, x As Single, Y As Single)

    Select Case Index
    
     Case 0          'Save Comment, Update Record
        Call StatusMsg("Saves Comment to Record Number: " & frmComment.txtRecNum.Text & "   Closes form and returns to Viewer.")
     
     Case 1          'Cancel, Clear Textbox
        Call StatusMsg("Clears text entered and resets Date/Time line.")
        
     Case 2          'Exit, Unload form, Do not Save, Show Viewer
        Call StatusMsg("Aborts Operation and returns to the Viewer, Comment will not be added.")
     
   End Select
    
End Sub
