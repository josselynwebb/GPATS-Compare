VERSION 5.00
Object = "{6B7E6392-850A-101B-AFC0-4210102A8DA7}#1.3#0"; "COMCTL32.OCX"
Begin VB.Form frmPLC 
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Path Loss Compensation Program"
   ClientHeight    =   2700
   ClientLeft      =   2970
   ClientTop       =   1890
   ClientWidth     =   6765
   Icon            =   "Plc.frx":0000
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2700
   ScaleWidth      =   6765
   Begin VB.CommandButton cmdStart 
      Caption         =   "&Start"
      Height          =   375
      Left            =   3840
      TabIndex        =   0
      Top             =   1560
      Width           =   1215
   End
   Begin VB.PictureBox picNoTest 
      AutoSize        =   -1  'True
      BorderStyle     =   0  'None
      Height          =   240
      Left            =   2820
      Picture         =   "Plc.frx":0442
      ScaleHeight     =   240
      ScaleWidth      =   480
      TabIndex        =   4
      Top             =   1560
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.PictureBox picTestPassed 
      BorderStyle     =   0  'None
      Height          =   300
      Left            =   1920
      Picture         =   "Plc.frx":05C4
      ScaleHeight     =   300
      ScaleWidth      =   495
      TabIndex        =   3
      Top             =   1560
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.PictureBox picTestFailed 
      BorderStyle     =   0  'None
      Height          =   285
      Left            =   2340
      Picture         =   "Plc.frx":0746
      ScaleHeight     =   285
      ScaleWidth      =   495
      TabIndex        =   2
      Top             =   1560
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.Timer timTimer 
      Enabled         =   0   'False
      Interval        =   300
      Left            =   5280
      Top             =   240
   End
   Begin VB.CommandButton cmdClose 
      Caption         =   "&Close"
      Height          =   375
      Left            =   5400
      TabIndex        =   1
      Top             =   1560
      Width           =   1215
   End
   Begin VB.PictureBox SSPanel1 
      Height          =   1335
      Left            =   60
      ScaleHeight     =   1275
      ScaleWidth      =   6480
      TabIndex        =   7
      Top             =   120
      Width           =   6540
      Begin VB.PictureBox picSwitchFail 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Height          =   240
         Index           =   1
         Left            =   3600
         ScaleHeight     =   240
         ScaleWidth      =   435
         TabIndex        =   15
         Top             =   120
         Visible         =   0   'False
         Width           =   435
      End
      Begin VB.PictureBox picSwitchPass 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Height          =   240
         Index           =   1
         Left            =   3600
         ScaleHeight     =   240
         ScaleWidth      =   435
         TabIndex        =   14
         Top             =   120
         Visible         =   0   'False
         Width           =   435
      End
      Begin VB.PictureBox picSwitchPass 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Height          =   240
         Index           =   2
         Left            =   120
         ScaleHeight     =   240
         ScaleWidth      =   435
         TabIndex        =   9
         Top             =   600
         Width           =   435
      End
      Begin VB.PictureBox picSwitchFail 
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   0  'None
         Height          =   240
         Index           =   2
         Left            =   720
         ScaleHeight     =   240
         ScaleWidth      =   435
         TabIndex        =   8
         Top             =   600
         Width           =   435
      End
      Begin VB.PictureBox sscSwitch 
         AutoSize        =   -1  'True
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   300
         Index           =   2
         Left            =   1365
         ScaleHeight     =   240
         ScaleWidth      =   240
         TabIndex        =   10
         Top             =   600
         Width           =   300
      End
      Begin VB.PictureBox sscSwitch 
         AutoSize        =   -1  'True
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   300
         Index           =   1
         Left            =   3240
         ScaleHeight     =   240
         ScaleWidth      =   240
         TabIndex        =   16
         Top             =   120
         Visible         =   0   'False
         Width           =   300
      End
      Begin VB.Label lblTestName 
         Caption         =   "Not Used"
         Height          =   255
         Index           =   1
         Left            =   4080
         TabIndex        =   17
         Top             =   120
         Visible         =   0   'False
         Width           =   855
      End
      Begin VB.Label lblTestName 
         Caption         =   "HF Switch Path Loss Measurements"
         Height          =   255
         Index           =   2
         Left            =   1920
         TabIndex        =   13
         Top             =   600
         Width           =   3135
      End
      Begin VB.Label Label2 
         Caption         =   "Pass"
         Height          =   255
         Left            =   165
         TabIndex        =   12
         Top             =   180
         Width           =   375
      End
      Begin VB.Label Label3 
         Caption         =   "Fail"
         Height          =   255
         Left            =   795
         TabIndex        =   11
         Top             =   180
         Width           =   315
      End
   End
   Begin ComctlLib.StatusBar sbrInfoBar 
      Align           =   2  'Align Bottom
      Height          =   255
      Left            =   0
      TabIndex        =   6
      Top             =   2445
      Width           =   6765
      _ExtentX        =   11933
      _ExtentY        =   450
      Style           =   1
      SimpleText      =   ""
      _Version        =   327682
      BeginProperty Panels {0713E89E-850A-101B-AFC0-4210102A8DA7} 
         NumPanels       =   1
         BeginProperty Panel1 {0713E89F-850A-101B-AFC0-4210102A8DA7} 
            Object.Tag             =   ""
         EndProperty
      EndProperty
   End
   Begin ComctlLib.ProgressBar proProgressBar 
      Height          =   375
      Left            =   30
      TabIndex        =   5
      Top             =   2040
      Width           =   6585
      _ExtentX        =   11615
      _ExtentY        =   661
      _Version        =   327682
      Appearance      =   1
   End
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   495
      Left            =   30
      Picture         =   "Plc.frx":08C8
      Stretch         =   -1  'True
      Top             =   1500
      Width           =   1695
   End
End
Attribute VB_Name = "frmPLC"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public Sub cmdClose_Click()
    
  If cmdClose.Caption = "&Close" Then
    End
  Else  'It's Abort
    iSelMode = USER_TERMINATE
  End If
  
End Sub

Private Sub cmdStart_Click()
  cmdStart.Visible = False
  Dispatch HF_SWITCH
End Sub

Private Sub Form_Load()
  For i = 1 To 2
    frmPLC.picSwitchPass(i).Picture = frmPLC.picNoTest
    frmPLC.picSwitchFail(i).Picture = frmPLC.picNoTest
  Next i
End Sub

Private Sub sscSwitch_Click(Index As Integer)
  'Code replace by cmdStart button
End Sub

Private Sub timTimer_Timer()
    'DESCRIPTION:
    '   This routine causes the lable of the executing test to blink every .3 seconds
    
    Dim LabelToBlink%
    
    LabelToBlink% = Val(frmPLC.timTimer.Tag)
    If frmPLC.lblTestName(LabelToBlink%).ForeColor = vbYellow Then
        frmPLC.lblTestName(LabelToBlink%).ForeColor = vbRed
    Else
        frmPLC.lblTestName(LabelToBlink%).ForeColor = vbYellow
    End If
End Sub
