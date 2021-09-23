VERSION 5.00
Begin VB.Form frmSplash 
   BackColor       =   &H000097C0&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   4335
   ClientLeft      =   255
   ClientTop       =   1410
   ClientWidth     =   7500
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4335
   ScaleWidth      =   7500
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame1 
      BackColor       =   &H000097C0&
      Height          =   4170
      Left            =   120
      TabIndex        =   0
      Top             =   75
      Width           =   7290
      Begin VB.CommandButton cmdOK 
         Caption         =   "OK"
         Height          =   255
         Left            =   120
         TabIndex        =   14
         Top             =   3840
         Width           =   1455
      End
      Begin VB.Timer SplashTimer 
         Interval        =   5000
         Left            =   4140
         Top             =   3195
      End
      Begin VB.Label lblTPCCN 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "TPCCN:"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   240
         Left            =   4560
         TabIndex        =   10
         Top             =   1320
         Width           =   2460
         WordWrap        =   -1  'True
      End
      Begin VB.Label lblTPExecVersion 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "TP Executive Ver.:"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   240
         Left            =   4560
         TabIndex        =   13
         Top             =   1560
         Width           =   2460
         WordWrap        =   -1  'True
      End
      Begin VB.Label lblWeaponSystemNomenclature 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "Nomenclature"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   240
         TabIndex        =   12
         Top             =   3360
         Width           =   4065
         WordWrap        =   -1  'True
      End
      Begin VB.Label Label3 
         BackStyle       =   0  'Transparent
         Caption         =   "Developed By:"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   225
         Left            =   4935
         TabIndex        =   11
         Top             =   3210
         Width           =   1710
      End
      Begin VB.Label lblUUTPartNo 
         BackStyle       =   0  'Transparent
         Caption         =   "Part No.: #########"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   225
         Left            =   4560
         TabIndex        =   9
         Top             =   2760
         Width           =   2370
      End
      Begin VB.Label lblUUTName 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "<uut name>"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   240
         Left            =   4560
         TabIndex        =   8
         Top             =   2280
         Width           =   2220
         WordWrap        =   -1  'True
      End
      Begin VB.Label lblUUT 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "Unit Under Test:"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   4575
         TabIndex        =   7
         Top             =   1905
         Width           =   2250
      End
      Begin VB.Label lblWeaponSystem 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "Weapon System"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   240
         TabIndex        =   6
         Top             =   3000
         Width           =   4140
         WordWrap        =   -1  'True
      End
      Begin VB.Label lblTPName 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "TPS#####"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   4560
         TabIndex        =   5
         Top             =   735
         Width           =   2475
      End
      Begin VB.Image imgLogo 
         BorderStyle     =   1  'Fixed Single
         Height          =   2745
         Left            =   135
         Picture         =   "frmSplash.frx":0000
         Top             =   225
         Width           =   4185
      End
      Begin VB.Label lblAddress 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "City, State"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   210
         Left            =   4935
         TabIndex        =   2
         Top             =   3660
         Width           =   735
      End
      Begin VB.Label lblCompany 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "Company Name"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   210
         Left            =   4935
         TabIndex        =   1
         Top             =   3450
         Width           =   1125
      End
      Begin VB.Label lblTPVersion 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "Version: #.#"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   4560
         TabIndex        =   3
         Top             =   1020
         Width           =   2445
      End
      Begin VB.Label lblPlatform 
         AutoSize        =   -1  'True
         BackColor       =   &H000097C0&
         BackStyle       =   0  'Transparent
         Caption         =   "Test Program:"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   4575
         TabIndex        =   4
         Top             =   280
         Width           =   2010
      End
   End
End
Attribute VB_Name = "frmSplash"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Private Sub cmdOk_Click()

    Unload Me    'Unload splash after 5 seconds
    
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)

    Unload Me
    
End Sub

Private Sub Form_Load()

    'Set test program specific information on Splash Screen
    lblTPName.Caption = TPName$
    lblTPVersion.Caption = "Version: " & TPVersion$
    lblUUTName.Caption = UUTName$
    lblUUTPartNo.Caption = "Part No.: " & UUTPartNo$
    lblTPCCN.Caption = "TPCCN: " & sTPCCN
    lblTPExecVersion.Caption = "TP Executive Ver.: " & App.Major & "." & App.Minor & "." & App.Revision

End Sub

Private Sub Frame1_Click()

    Unload Me
    
End Sub

Private Sub SplashTimer_Timer()

    Unload Me    'Unload splash after 5 seconds
    
End Sub
