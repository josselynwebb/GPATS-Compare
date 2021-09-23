VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmHV 
   BackColor       =   &H000000FF&
   BorderStyle     =   0  'None
   Caption         =   "Form1"
   ClientHeight    =   6120
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   7665
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6120
   ScaleWidth      =   7665
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin Threed.SSCommand MouseControl 
      Height          =   345
      Index           =   1
      Left            =   5640
      TabIndex        =   9
      Top             =   5520
      Width           =   1860
      _Version        =   65536
      _ExtentX        =   3281
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "No"
      ForeColor       =   255
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BevelWidth      =   3
      MouseIcon       =   "frmHV.frx":0000
      Picture         =   "frmHV.frx":001C
   End
   Begin Threed.SSPanel SSPanel1 
      Height          =   5895
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   7455
      _Version        =   65536
      _ExtentX        =   13150
      _ExtentY        =   10398
      _StockProps     =   15
      Caption         =   "SSPanel1"
      BackColor       =   -2147483638
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Begin VB.Timer SplashTimer 
         Interval        =   100
         Left            =   4560
         Top             =   5400
      End
      Begin VB.PictureBox Picture1 
         Appearance      =   0  'Flat
         BackColor       =   &H00C0C0C0&
         ForeColor       =   &H80000008&
         Height          =   4455
         Left            =   120
         ScaleHeight     =   4425
         ScaleWidth      =   7185
         TabIndex        =   4
         Top             =   120
         Width           =   7215
         Begin VB.PictureBox Picture3 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   0  'None
            ForeColor       =   &H80000008&
            Height          =   855
            Left            =   480
            Picture         =   "frmHV.frx":0038
            ScaleHeight     =   855
            ScaleWidth      =   6375
            TabIndex        =   6
            Top             =   2760
            Width           =   6375
         End
         Begin VB.PictureBox Picture2 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   0  'None
            ForeColor       =   &H80000008&
            Height          =   2535
            Left            =   840
            Picture         =   "frmHV.frx":1105A
            ScaleHeight     =   2535
            ScaleWidth      =   5415
            TabIndex        =   5
            Top             =   240
            Width           =   5415
         End
         Begin VB.Label Label1 
            BackColor       =   &H00C0C0C0&
            Caption         =   "*** UUT POWER IS ABOUT TO BE APPLIED ***"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   13.5
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   375
            Left            =   240
            TabIndex        =   7
            Top             =   3720
            Width           =   6735
         End
      End
      Begin Threed.SSCommand MouseControl 
         Height          =   345
         Index           =   0
         Left            =   5520
         TabIndex        =   0
         Top             =   5040
         Width           =   1860
         _Version        =   65536
         _ExtentX        =   3281
         _ExtentY        =   609
         _StockProps     =   78
         Caption         =   "Yes"
         ForeColor       =   16384
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   3
         MouseIcon       =   "frmHV.frx":3C044
         Picture         =   "frmHV.frx":3C060
      End
      Begin VB.Label Label4 
         Alignment       =   2  'Center
         Caption         =   "Acknowledge"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   375
         Left            =   5640
         TabIndex        =   8
         Top             =   4680
         Width           =   1575
      End
      Begin VB.Label Label3 
         Caption         =   "Press ""Yes"" to continue.  ""No"" to exit."
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   375
         Left            =   240
         TabIndex        =   2
         Top             =   5400
         Width           =   4695
      End
      Begin VB.Label Label2 
         Caption         =   $"frmHV.frx":3C07C
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   855
         Left            =   240
         TabIndex        =   3
         Top             =   4680
         Width           =   5055
      End
   End
End
Attribute VB_Name = "frmHV"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Private Sub Form_Load()

     iAcknowledge = 2
     Me.Top = (Screen.Height / 2 - Me.Height / 2) ' - 600
     Me.Left = Screen.Width / 2 - Me.Width / 2
     If frmMain.SeqTextWindow.Visible Then Me.Left = Me.Left '- 700

End Sub

Private Sub MouseControl_Click(index As Integer)

    If index = 0 Then iAcknowledge = 0 Else iAcknowledge = 1
    
End Sub

Private Sub SplashTimer_Timer()

     Do
        frmHV.Label1.ForeColor = vbBlack
        DoEvents
        
        If iAcknowledge = 0 Then Exit Do
        
        Delay 0.5
        frmHV.Label1.ForeColor = vbRed
        DoEvents
        
        If iAcknowledge = 0 Then Exit Do
        
        Delay 0.5
        
     Loop While iAcknowledge = 2
     
     Unload Me
     
End Sub

