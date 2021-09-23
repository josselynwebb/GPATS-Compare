VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmFHDBComment 
   BackColor       =   &H00C0C0C0&
   Caption         =   "** FHDB Comment Selection List **"
   ClientHeight    =   6060
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   5850
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   Moveable        =   0   'False
   ScaleHeight     =   6060
   ScaleWidth      =   5850
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin Threed.SSPanel SSPanel1 
      Height          =   5055
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   5535
      _Version        =   65536
      _ExtentX        =   9763
      _ExtentY        =   8916
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   0
         Left            =   120
         TabIndex        =   1
         Top             =   120
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "1  - No Operator Comments                              "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   1
         Left            =   120
         TabIndex        =   2
         Top             =   600
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "2  - Misprobe/probing problem                          "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   2
         Left            =   120
         TabIndex        =   3
         Top             =   1080
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "3  - Test Instructions not properly followed          "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   3
         Left            =   120
         TabIndex        =   4
         Top             =   1560
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "4  - UUT/Interface Device not setup properly     "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   4
         Left            =   120
         TabIndex        =   5
         Top             =   2040
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "5  - Interface Device is broken/has problem       "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   5
         Left            =   120
         TabIndex        =   6
         Top             =   2520
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "6  - Connection/connector/cable problem          "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   6
         Left            =   120
         TabIndex        =   7
         Top             =   3000
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "7  - Operator induced failure to exit test            "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   7
         Left            =   120
         TabIndex        =   8
         Top             =   3480
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "8  - The TETS has software/hardware problem  "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   8
         Left            =   120
         TabIndex        =   9
         Top             =   3960
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "9  - Environmental issues impacting testing       "
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
      Begin Threed.SSCommand cmdOperatorComment 
         Height          =   435
         Index           =   9
         Left            =   120
         TabIndex        =   10
         Top             =   4440
         Width           =   5220
         _Version        =   65536
         _ExtentX        =   9208
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "10 - Manually generate comments (Next Window)"
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
   End
   Begin VB.Label Label2 
      BackColor       =   &H00C0C0C0&
      Caption         =   "Select a common comment from the list or generate a manual comment via item 10"
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
      Left            =   2400
      TabIndex        =   11
      Top             =   5280
      Width           =   3255
   End
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   705
      Left            =   180
      Picture         =   "frmFHDBComment.frx":0000
      Stretch         =   -1  'True
      Top             =   5280
      Width           =   1725
   End
End
Attribute VB_Name = "frmFHDBComment"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Private Sub cmdOperatorComment_Click(index As Integer)

    Select Case index
    
        Case 0 To 8
            sComment = cmdOperatorComment(index).Caption
            frmFHDBComment.Hide
            Unload frmFHDBComment
            
        Case 9
            CenterForm frmOperatorComment
            frmFHDBComment.Hide
            Unload frmFHDBComment
            frmOperatorComment.Show 1

    End Select
    
End Sub

