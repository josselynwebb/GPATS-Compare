VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmOperatorComment 
   BackColor       =   &H00C0C0C0&
   Caption         =   "** Manually Generate Operator Comment **"
   ClientHeight    =   2745
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   8640
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   Moveable        =   0   'False
   ScaleHeight     =   2745
   ScaleWidth      =   8640
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin Threed.SSPanel SSPanel1 
      Height          =   1815
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   8415
      _Version        =   65536
      _ExtentX        =   14843
      _ExtentY        =   3201
      _StockProps     =   15
      Caption         =   "SSPanel1"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Begin VB.TextBox txtOperatorComment 
         Appearance      =   0  'Flat
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1575
         Left            =   120
         MaxLength       =   255
         MultiLine       =   -1  'True
         TabIndex        =   1
         Top             =   120
         Width           =   8175
      End
   End
   Begin Threed.SSCommand Continue 
      Height          =   435
      Left            =   7080
      TabIndex        =   2
      Top             =   2040
      Width           =   1500
      _Version        =   65536
      _ExtentX        =   2646
      _ExtentY        =   767
      _StockProps     =   78
      Caption         =   "Continue"
      ForeColor       =   8388608
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BevelWidth      =   3
      Font3D          =   3
   End
   Begin VB.Label Label2 
      Alignment       =   2  'Center
      BackColor       =   &H00C0C0C0&
      Caption         =   "Enter Comment above and select Continue"
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
      Left            =   2160
      TabIndex        =   3
      Top             =   2160
      Width           =   4695
   End
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   645
      Left            =   135
      Picture         =   "frmOperatorComment.frx":0000
      Stretch         =   -1  'True
      Top             =   2040
      Width           =   1815
   End
End
Attribute VB_Name = "frmOperatorComment"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Private Sub Continue_Click()

    sComment = txtOperatorComment.Text
    frmOperatorComment.Hide
    Unload frmOperatorComment
    
End Sub

Private Sub Form_Load()

    txtOperatorComment.Text = ""
    
End Sub
