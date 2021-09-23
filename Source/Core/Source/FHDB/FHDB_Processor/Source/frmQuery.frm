VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmQuery 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Search FHDB"
   ClientHeight    =   3090
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4770
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   3090
   ScaleWidth      =   4770
   WindowState     =   2  'Maximized
   Begin Threed.SSFrame ssfraPassFail 
      Height          =   975
      Left            =   1930
      TabIndex        =   3
      Top             =   850
      Visible         =   0   'False
      Width           =   975
      _Version        =   65536
      _ExtentX        =   1720
      _ExtentY        =   1720
      _StockProps     =   14
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Begin Threed.SSOption ssoptPassFail 
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   1
         Top             =   600
         Width           =   615
         _Version        =   65536
         _ExtentX        =   1085
         _ExtentY        =   450
         _StockProps     =   78
         Caption         =   "&Fail"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin Threed.SSOption ssoptPassFail 
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   2
         Top             =   240
         Width           =   735
         _Version        =   65536
         _ExtentX        =   1296
         _ExtentY        =   450
         _StockProps     =   78
         Caption         =   "&Pass"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Value           =   -1  'True
      End
   End
   Begin Threed.SSCommand cmdSearch 
      Height          =   345
      Left            =   2925
      TabIndex        =   7
      Top             =   2280
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "&Search"
   End
   Begin VB.TextBox txtValue 
      Alignment       =   2  'Center
      BeginProperty DataFormat 
         Type            =   0
         Format          =   "m/d/yy h:nn"
         HaveTrueFalseNull=   0
         FirstDayOfWeek  =   0
         FirstWeekOfYear =   0
         LCID            =   1033
         SubFormatType   =   0
      EndProperty
      Height          =   375
      Index           =   1
      Left            =   1680
      TabIndex        =   4
      Top             =   960
      Visible         =   0   'False
      Width           =   2175
   End
   Begin VB.TextBox txtValue 
      Alignment       =   2  'Center
      Height          =   375
      Index           =   2
      Left            =   1680
      TabIndex        =   5
      Top             =   960
      Visible         =   0   'False
      Width           =   2175
   End
   Begin VB.TextBox txtValue 
      Alignment       =   2  'Center
      Height          =   375
      Index           =   3
      Left            =   1680
      TabIndex        =   6
      Top             =   1440
      Visible         =   0   'False
      Width           =   2175
   End
   Begin VB.ComboBox cboValue 
      Height          =   315
      Left            =   983
      Style           =   2  'Dropdown List
      TabIndex        =   0
      Top             =   360
      Visible         =   0   'False
      Width           =   2895
   End
   Begin Threed.SSCommand cmdByValue 
      Height          =   345
      Left            =   2925
      TabIndex        =   12
      Top             =   720
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "By &Value"
   End
   Begin Threed.SSCommand cmdByRange 
      Height          =   345
      Left            =   800
      TabIndex        =   13
      Top             =   720
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "By &Range"
   End
   Begin Threed.SSCommand cmdCancel 
      Height          =   345
      Left            =   800
      TabIndex        =   8
      Top             =   2280
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "&Cancel"
   End
   Begin VB.Label lblDeg 
      AutoSize        =   -1  'True
      Caption         =   "C"
      Height          =   195
      Index           =   1
      Left            =   3960
      TabIndex        =   15
      Top             =   1560
      Visible         =   0   'False
      Width           =   105
   End
   Begin VB.Label lblDeg 
      AutoSize        =   -1  'True
      Caption         =   "C"
      Height          =   195
      Index           =   0
      Left            =   3960
      TabIndex        =   14
      Top             =   1080
      Visible         =   0   'False
      Width           =   105
   End
   Begin VB.Label lblCriteria 
      AutoSize        =   -1  'True
      Caption         =   "Criteria:"
      Height          =   195
      Left            =   1080
      TabIndex        =   11
      Top             =   1100
      Visible         =   0   'False
      Width           =   525
   End
   Begin VB.Label lblFrom 
      AutoSize        =   -1  'True
      Caption         =   "From"
      Height          =   195
      Left            =   1200
      TabIndex        =   10
      Top             =   1080
      Visible         =   0   'False
      Width           =   345
   End
   Begin VB.Label lblTo 
      AutoSize        =   -1  'True
      Caption         =   "To"
      Height          =   195
      Left            =   1320
      TabIndex        =   9
      Top             =   1560
      Visible         =   0   'False
      Width           =   195
   End
End
Attribute VB_Name = "frmQuery"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
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


Option Explicit


Private Sub cboValue_Click()

   Call FormatSelectedValue                 'Format Criteria TxtBoxes to accept
                                            'Criteria based on Search Field selected
End Sub


Private Sub cmdByRange_Click()

    Call Show_Range                         'Format Search Form to accept a Range
    Call Initalize_cboValue                 'of values for search criteria
    Call Form_Grow_Range

End Sub


Private Sub cmdByValue_Click()

    Call Show_Value                         'Format Search Form to accept a single
    Call Initalize_cboValue                 'value for Search criteria
    Call Form_Grow_Value
     
End Sub


Private Sub cmdCancel_Click()

    StatusMsg ("Database Viewer - Search Aborted.")
    Call ClearSearch                        'Clear all Search data to default
    Call MoveToBookmark                     'Display Last Record
    frmViewer.Show                          'Unload Search Form and Show Viewer
    Unload Me
    
End Sub


Private Sub cmdSearch_Click()
    
    Call ValidateDate                       'Check that the Date/Dates Entered are Valid
    If bValidData Then
        StatusMsg ("Initiating Search..............")
        Call Search_DB
        'If Record dosen't exist, don't clear
        If bNoRecord = False Then           'A record was found, Load it into Viewer
            frmViewer.Show
            Unload Me
        End If
        If bNoRecord = True Then
            StatusMsg ("Enter Criteria to Search on.")
        End If
        bNoRecord = False                   'Set the No Record Found Switch to False
    End If
    
    bValidData = True                       'Set the Valid Date Switch to True
    Call SetMinMax
    
End Sub


Private Sub Form_Load()
    
    Width = 4890                            ' Set width of form.
    Height = 2000                           ' Set height of form.
    Left = (frmViewer.Width - Width) / 2    ' Center form horizontally.
    Top = (frmViewer.Height - Height) / 2   ' Center form vertically.
    
    Call ClearSearch
    frmQuery.cmdSearch.Enabled = False      'Initiate the Search Comand to Disabled
    bValidData = True                       'Initiate Valid Date Variable
    frmQuery.WindowState = 0                'Force Windows State to Normal
    frmQuery.Refresh                        'Refresh the RecordSet
    StatusMsg ("Select the type of search you would like to perform.")
    
End Sub


Private Sub Form_Unload(Cancel As Integer)

    frmViewer.WindowState = 2               'Force the Windows State to Maximized
    frmViewer.Refresh                       'Refresh the Viewer Form Image
    
End Sub


Sub Form_Grow_Range()
'Resize Form when Range is Selected

    Width = 4890     'Screen.Width * 0.1   ' Set width of form.
    Height = 3465       '3945    'Screen.Height * 0.1   ' Set height of form.
    Left = (frmViewer.Width - Width) / 2   ' Center form horizontally.
    Top = (frmViewer.Height - Height) / 2   ' Center form vertically.
    
End Sub



Sub Form_Grow_Value()
'Resize Form when By Value is Selected

    Width = 4890     'Screen.Width * 0.1   ' Set width of form.
    Height = 3000       '3945    'Screen.Height * 0.1   ' Set height of form.
    Left = (frmViewer.Width - Width) / 2   ' Center form horizontally.
    Top = (frmViewer.Height - Height) / 2   ' Center form vertically.
    cmdCancel.Top = (cmdCancel.Top - 200)
    cmdSearch.Top = (cmdSearch.Top - 200)
    
End Sub



Private Sub txtValue_KeyPress(iIndex As Integer, KeyAscii As Integer)
'**********************************************************************
'***        Verify a valid Date is entered and force formating,     ***
'***        limit number of characters entered.                     ***
'**********************************************************************

Dim iLen As Integer         'Variable to hold Length
Dim sCharacter As String    'Variable to hold String value


    If txtValue(iIndex) = "MM/DD/YY HH:mm" Then     'Clear Date mask
        txtValue(iIndex) = ""
    End If
    
    iLen = Len(txtValue(iIndex).Text)       'Get the length of text in TextBox
    
    If KeyAscii = 13 Then               'If User press "Enter" execute Search
        cmdSearch.DoClick
        Exit Sub
     End If
     
    If KeyAscii = 8 Then                'Allow User to Backspace
        Exit Sub
    End If
    
'**************************************************************

    Select Case cboValue.ListIndex
    
        Case 0              'Record Indentifier, allow only numbers, no spaces
            If KeyAscii < 48 Or KeyAscii > 57 Then
                        KeyAscii = 1                'Nothing is entered
                        MsgBox "Please Enter a number", , "Invalid Entry"
                        Exit Sub
            End If
            bSearch(iIndex) = True
        Case 1          'Start Date/Time
                                            'If Entry is a date, force validation/format
            If bDate Then
                Select Case iLen + 1
                'First filter out nonnumeric values
                Case 1, 2, 4, 5, 7, 8, 10, 11, 13, 14
                    'asc(48-57) Only 0-9 (Numbers) Allowed here
                    If KeyAscii < 48 Or KeyAscii > 57 Then
                        KeyAscii = 1                'Nothing is entered
                        MsgBox "Please Enter a number", , "Invalid Entry"
                        Exit Sub
                    End If
                                            'If there are at least 8 characters
                    If iLen = 7 Then        'Enable Search Command
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
                                KeyAscii = 1                'Nothing is entered
                                Exit Sub
                            End If
                        'Only allow 0-2 if first Month char is a "1"
                        Case 2                          '1(0) - 1(2)
                            sCharacter = Mid(txtValue(iIndex).Text, 1, 1)
                            If sCharacter = "1" Then
                                If KeyAscii < 48 Or KeyAscii > 50 Then
                                    KeyAscii = 1            'Nothing is entered
                                    Exit Sub
                                End If
                            End If
                        'Only allow 0-3 to be entered   '(0)1 - (3)1
                        Case 4
                            If KeyAscii < 48 Or KeyAscii > 51 Then
                                KeyAscii = 1            'Nothing is entered
                                Exit Sub
                            End If
                        'Only allow 0-1 if first Day char is a "3"
                        Case 5                          '3(0) - 3(1)
                            sCharacter = Mid(txtValue(iIndex).Text, 4, 1)
                            If sCharacter = "3" Then
                                If KeyAscii < 48 Or KeyAscii > 49 Then
                                    KeyAscii = 1            'Nothing is entered
                                    Exit Sub
                                End If
                            End If
                        'Only allow 0-2 to be entered   '(0)1 - (2)4 hours
                        Case 10
                            If KeyAscii < 48 Or KeyAscii > 50 Then
                                KeyAscii = 1            'Nothing is entered
                                Exit Sub
                            End If
                         'Only allow 0-5 to be entered  '(0)0 - (5)9 minutes
                         Case 13
                            If KeyAscii < 48 Or KeyAscii > 53 Then
                                KeyAscii = 1            'Nothing is entered
                                Exit Sub
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
                        cmdSearch.DoClick
                    Exit Sub
                End If
                    'If both Text Boxes are used for Search Criteria
                    'Using the By Range Selection, If the Length of either textbox is over 14,
                    'Check to varify a valid date has been entered. If so,
                    'do not enter character and trigger Search command
                If Len(txtValue(2).Text) = 14 And Len(txtValue(3).Text) = 14 Then
                    If IsDate(txtValue(2).Text) And IsDate(txtValue(3).Text) Then
                        KeyAscii = 1
                        cmdSearch.DoClick
                        Exit Sub
                    End If
                        KeyAscii = 1
                        If IsDate(txtValue(2).Text) Then
                            txtValue(3).SetFocus
                        Else
                            txtValue(2).SetFocus
                        End If
                 End If
                 
               Case Else
               'MsgBox to inform User that Entry is not valid
                    MsgBox "Non valid entry", , "Invalid Entry"
               End Select
        
        End If
        
        Case 2          'Intake Temperature, allow only numbers and Asc(45) "-" or Asc(46) "."
            Debug.Print KeyAscii
            If KeyAscii < 45 Or KeyAscii > 46 Then
                If KeyAscii < 48 Or KeyAscii > 57 Then
                    KeyAscii = 1
                    Exit Sub
                End If
            End If
            bSearch(iIndex) = True
            
        '**************************************************************
        '***   Varify the following fields are Length validated     ***
        '**************************************************************
        Case 4          'UUT_Serial_No      'Limit to 15 characters
            If iLen > 14 Then
                KeyAscii = 1                'Nothing is entered
                cmdSearch.SetFocus
                Exit Sub
            End If
            bSearch(iIndex) = True
        Case 5, 6       'UUT_Revision and ID_Serial_No       'Limit to 10 characters
            If iLen > 9 Then
                KeyAscii = 1                'Nothing is entered
                cmdSearch.SetFocus
                Exit Sub
            End If
            bSearch(iIndex) = True
        Case 7          'TPCCN      'Limit to 16 characters
            If iLen > 15 Then
                KeyAscii = 1                'Nothing is entered
                cmdSearch.SetFocus
                Exit Sub
            End If
            bSearch(iIndex) = True
        Case 8          'ERO        'Limit to 5 characters
            If iLen > 4 Then
                KeyAscii = 1                'Nothing is entered
                cmdSearch.SetFocus
                Exit Sub
            End If
            bSearch(iIndex) = True
    End Select
        
        Call EnableSearch
        
End Sub


'***********************************************************************
'****                   Status Line Messages                        ****
'***********************************************************************

Private Sub Form_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Search Database.")
End Sub

Private Sub cmdByRange_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Search database to return records between two values.")
End Sub

Private Sub cmdByValue_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    StatusMsg ("Search database by a single value.")
End Sub

Private Sub cmdCancel_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Aborts Search Operation and returns to the Viewer.")
End Sub

Private Sub cmdSearch_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Searches Database based selected criteria.")
End Sub

Private Sub ssoptPassFail_MouseMove(Index As Integer, Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Select Pass or Fail.")
End Sub

Private Sub txtValue_MouseMove(Index As Integer, Button As Integer, Shift As Integer, x As Single, Y As Single)

    Select Case Index
    
     Case 1          'Single entry Criteria
        Call StatusMsg("Enter Criteria to Search on.")
     
     Case 2          'From on Range
        Call StatusMsg("Enter Start value to base search on.")
        
     Case 3          'To on Range
        Call StatusMsg("Enter Ending value to base search on.")
     
   End Select

End Sub

Private Sub cboValue_GotFocus()
    Call StatusMsg("Select Field to base Search on.")
End Sub
