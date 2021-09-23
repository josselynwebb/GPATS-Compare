VERSION 5.00
Begin VB.Form frmDisplayLED 
   Caption         =   "LED Test"
   ClientHeight    =   6135
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   8415
   LinkTopic       =   "Form1"
   ScaleHeight     =   6135
   ScaleWidth      =   8415
   StartUpPosition =   3  'Windows Default
   Begin VB.PictureBox PictureWindow 
      Appearance      =   0  'Flat
      AutoRedraw      =   -1  'True
      BackColor       =   &H80000005&
      ClipControls    =   0   'False
      ForeColor       =   &H80000008&
      Height          =   4575
      Left            =   0
      ScaleHeight     =   4545
      ScaleWidth      =   8385
      TabIndex        =   3
      Top             =   0
      Width           =   8415
   End
   Begin VB.TextBox cmdTextBox 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1335
      Left            =   1560
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      TabIndex        =   2
      Top             =   4680
      Width           =   6735
   End
   Begin VB.CommandButton cmdNO 
      Height          =   495
      Left            =   120
      Picture         =   "frmDisplayLED.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   5400
      Width           =   1215
   End
   Begin VB.CommandButton cmdYES 
      Default         =   -1  'True
      Height          =   495
      Left            =   120
      Picture         =   "frmDisplayLED.frx":31FA
      Style           =   1  'Graphical
      TabIndex        =   0
      Top             =   4800
      Width           =   1215
   End
End
Attribute VB_Name = "frmDisplayLED"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Private Sub cmdLEDPicture_Click()

End Sub

Private Sub cmdNO_Click()

    LEDUserResponse = True
    LEDButtonPressed = "NO"
    frmDisplayLED.Hide
    
End Sub

Private Sub cmdYES_Click()

    LEDUserResponse = True
    LEDButtonPressed = "YES"
    frmDisplayLED.Hide
    
End Sub

Private Sub Text1_Change()

End Sub

