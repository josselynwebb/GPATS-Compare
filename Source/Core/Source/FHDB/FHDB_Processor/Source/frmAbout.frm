VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmAbout 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "About"
   ClientHeight    =   2535
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   3795
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   Icon            =   "frmAbout.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2535
   ScaleWidth      =   3795
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.PictureBox AppPicture 
      Appearance      =   0  'Flat
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   480
      Left            =   45
      Picture         =   "frmAbout.frx":0442
      ScaleHeight     =   480
      ScaleWidth      =   480
      TabIndex        =   0
      Top             =   0
      Width           =   480
   End
   Begin Threed.SSPanel panTitle 
      Height          =   315
      Left            =   600
      TabIndex        =   1
      Top             =   105
      Width           =   3030
      _Version        =   65536
      _ExtentX        =   5345
      _ExtentY        =   556
      _StockProps     =   15
      Caption         =   "FHDB Processor"
      ForeColor       =   8388608
      BackColor       =   12632256
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BevelOuter      =   0
      FloodColor      =   16777215
      FloodShowPct    =   0   'False
      Font3D          =   3
      Alignment       =   0
      Autosize        =   2
   End
   Begin Threed.SSPanel SSPanel1 
      Height          =   570
      Left            =   0
      TabIndex        =   2
      Top             =   585
      Width           =   3615
      _Version        =   65536
      _ExtentX        =   6371
      _ExtentY        =   995
      _StockProps     =   15
      BackColor       =   12632256
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BevelOuter      =   1
      Begin VB.Label lblInstrument 
         Caption         =   "App.Version"
         Height          =   255
         Index           =   1
         Left            =   1020
         TabIndex        =   6
         Top             =   255
         Width           =   1995
      End
      Begin VB.Label lblInstrument 
         Caption         =   "Portable Automated Tester"
         Height          =   255
         Index           =   0
         Left            =   1020
         TabIndex        =   5
         Top             =   15
         Width           =   2535
      End
      Begin VB.Label lblAttribute 
         AutoSize        =   -1  'True
         Caption         =   "Version:"
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   4
         Top             =   255
         Width           =   570
      End
      Begin VB.Label lblAttribute 
         AutoSize        =   -1  'True
         Caption         =   "System:"
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   3
         Top             =   15
         Width           =   555
      End
   End
   Begin Threed.SSCommand cmdOk 
      Height          =   375
      Left            =   2400
      TabIndex        =   7
      Top             =   1680
      Visible         =   0   'False
      Width           =   1335
      _Version        =   65536
      _ExtentX        =   2355
      _ExtentY        =   661
      _StockProps     =   78
      Caption         =   "&Ok"
   End
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   1215
      Left            =   0
      Picture         =   "frmAbout.frx":0884
      Stretch         =   -1  'True
      Top             =   1185
      Width           =   2295
   End
End
Attribute VB_Name = "frmAbout"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'**********************************************************************
'***    United States Marine Corps                                  ***
'***                                                                ***
'***    Nomenclature:   Form "frmAbout" : FHDB_Processor            ***
'***    Written By:     Dave Joiner                                 ***
'***    Purpose:                                                    ***
'***  ECP-TETS-023                                                  ***
'***    To allow the User to View the Infomation about the          ***
'***    FHDB_Processor.                                             ***
'**********************************************************************

Option Explicit


Private Sub cmdOk_Click()
    Unload frmAbout
End Sub


Private Sub Form_Load()
    'Updates Version number to reflect Project Properties.  DJoiner  07/19/2001
    lblInstrument(1).Caption = App.Major & "." & App.Minor & "." & App.Revision
End Sub
