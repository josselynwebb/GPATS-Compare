VERSION 5.00
Begin VB.Form frmAbout 
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Path Loss Compensation"
   ClientHeight    =   1845
   ClientLeft      =   3720
   ClientTop       =   4755
   ClientWidth     =   4095
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   Icon            =   "About.frx":0000
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   1845
   ScaleWidth      =   4095
   ShowInTaskbar   =   0   'False
   Begin VB.PictureBox panTitle 
      AutoSize        =   -1  'True
      BackColor       =   &H00C0C0C0&
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   315
      Left            =   840
      ScaleHeight     =   255
      ScaleWidth      =   2970
      TabIndex        =   0
      Top             =   120
      Width           =   3030
   End
   Begin VB.PictureBox SSPanel1 
      BackColor       =   &H00C0C0C0&
      Height          =   510
      Left            =   120
      ScaleHeight     =   450
      ScaleWidth      =   3795
      TabIndex        =   1
      Top             =   600
      Width           =   3855
      Begin VB.Label lblInstrument 
         BackColor       =   &H00C0C0C0&
         Height          =   225
         Index           =   3
         Left            =   1560
         TabIndex        =   5
         Top             =   270
         Width           =   1995
      End
      Begin VB.Label lblInstrument 
         BackColor       =   &H00C0C0C0&
         Caption         =   "AN/USM-657 TETS"
         Height          =   255
         Index           =   0
         Left            =   1560
         TabIndex        =   4
         Top             =   30
         Width           =   1995
      End
      Begin VB.Label lblAttribute 
         AutoSize        =   -1  'True
         BackColor       =   &H00C0C0C0&
         Caption         =   "Used on:"
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   3
         Top             =   30
         Width           =   645
      End
      Begin VB.Label lblAttribute 
         AutoSize        =   -1  'True
         BackColor       =   &H00C0C0C0&
         Caption         =   "Version:"
         Height          =   195
         Index           =   3
         Left            =   135
         TabIndex        =   2
         Top             =   270
         Width           =   570
      End
   End
   Begin VB.Image Image1 
      Height          =   480
      Left            =   120
      Picture         =   "About.frx":0442
      Top             =   60
      Width           =   480
   End
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   495
      Left            =   120
      Picture         =   "About.frx":0884
      Stretch         =   -1  'True
      Top             =   1305
      Width           =   1815
   End
End
Attribute VB_Name = "frmAbout"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

