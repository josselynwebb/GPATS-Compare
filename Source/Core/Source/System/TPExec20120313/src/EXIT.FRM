VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form ExitForm 
   Appearance      =   0  'Flat
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Exit "
   ClientHeight    =   2190
   ClientLeft      =   2505
   ClientTop       =   2595
   ClientWidth     =   4620
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "MS Sans Serif"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H80000008&
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2190
   ScaleWidth      =   4620
   Begin Threed.SSPanel Panel3D1 
      Height          =   2130
      Left            =   30
      TabIndex        =   1
      Top             =   30
      Width           =   4575
      _Version        =   65536
      _ExtentX        =   8070
      _ExtentY        =   3757
      _StockProps     =   15
      ForeColor       =   0
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BevelWidth      =   2
      BevelInner      =   1
      Font3D          =   2
      Autosize        =   2
      Begin VB.PictureBox Picture1 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BorderStyle     =   0  'None
         ForeColor       =   &H80000008&
         Height          =   810
         Left            =   240
         Picture         =   "EXIT.frx":0000
         ScaleHeight     =   810
         ScaleWidth      =   885
         TabIndex        =   4
         Top             =   240
         Width           =   885
      End
      Begin Threed.SSCommand Cancel 
         Height          =   615
         Left            =   2400
         TabIndex        =   2
         Top             =   1200
         Width           =   1575
         _Version        =   65536
         _ExtentX        =   2778
         _ExtentY        =   1085
         _StockProps     =   78
         Caption         =   "Cancel"
         ForeColor       =   8388608
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin Threed.SSCommand VerifyExit 
         Height          =   615
         Left            =   480
         TabIndex        =   0
         Top             =   1200
         Width           =   1575
         _Version        =   65536
         _ExtentX        =   2778
         _ExtentY        =   1085
         _StockProps     =   78
         Caption         =   "Ok"
         ForeColor       =   8388608
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin VB.Label Label1 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BackColor       =   &H00C0C0C0&
         Caption         =   "This will end your session"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   300
         Left            =   1275
         TabIndex        =   3
         Top             =   480
         Width           =   3045
      End
   End
End
Attribute VB_Name = "ExitForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Private Sub Cancel_Click()

    ExitForm.Hide
    UserEvent% = 0
    
End Sub

Private Sub Cancel_KeyDown(KeyCode As Integer, Shift As Integer)

    Select Case KeyCode
        Case vbKeyC Or vbKeyReturn
            If Shift = 2 Then Cancel_Click
        Case vbKeyO
            If Shift = 2 Then VerifyExit_Click
    End Select
    
End Sub

Private Sub Form_Load()

    CenterForm Me
    
End Sub

Private Sub VerifyExit_Click()

    ExitForm.VerifyExit.Enabled = False
    ExitForm.Cancel.Enabled = False
    frmMain.Timer1.Enabled = False
    frmMain.Timer1b.Enabled = False
    frmMain.Timer2.Enabled = False
    frmMain.Timer2b.Enabled = False
    frmMain.Timer3.Enabled = False
    Unload frmMain
    
End Sub

