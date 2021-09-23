VERSION 5.00
Begin VB.Form frmDirections 
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Test Setup Instructions"
   ClientHeight    =   4995
   ClientLeft      =   8505
   ClientTop       =   1320
   ClientWidth     =   6645
   Icon            =   "Frmdirec.frx":0000
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   4995
   ScaleWidth      =   6645
   ShowInTaskbar   =   0   'False
   Begin VB.CommandButton cmdContinue 
      Caption         =   "&Continue"
      Height          =   345
      Left            =   5385
      TabIndex        =   0
      Top             =   4500
      Width           =   1125
   End
   Begin VB.PictureBox picGraphic 
      Height          =   3375
      Left            =   120
      Picture         =   "Frmdirec.frx":0442
      ScaleHeight     =   3315
      ScaleWidth      =   6330
      TabIndex        =   1
      Top             =   960
      Width           =   6390
   End
   Begin VB.PictureBox SSPanel1 
      Height          =   855
      Left            =   120
      ScaleHeight     =   795
      ScaleWidth      =   6330
      TabIndex        =   2
      Top             =   30
      Width           =   6390
      Begin VB.Label lblCaption 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H00C0C0C0&
         Caption         =   $"Frmdirec.frx":45A84
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   765
         Left            =   30
         TabIndex        =   3
         Top             =   45
         Width           =   6330
         WordWrap        =   -1  'True
      End
   End
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   495
      Left            =   120
      Picture         =   "Frmdirec.frx":45B21
      Stretch         =   -1  'True
      Top             =   4440
      Width           =   1815
   End
End
Attribute VB_Name = "frmDirections"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdContinue_Click()
    frmDirections.Hide
End Sub


