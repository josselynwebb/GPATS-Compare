VERSION 5.00
Begin VB.Form frmMsgBox 
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Operator Message"
   ClientHeight    =   3615
   ClientLeft      =   225
   ClientTop       =   1245
   ClientWidth     =   4560
   ControlBox      =   0   'False
   Icon            =   "Msgbox.frx":0000
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   3615
   ScaleWidth      =   4560
   Begin VB.CommandButton Command1 
      BackColor       =   &H00C0C0C0&
      Height          =   375
      Index           =   6
      Left            =   3120
      TabIndex        =   5
      Top             =   3120
      Visible         =   0   'False
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H00C0C0C0&
      Height          =   375
      Index           =   5
      Left            =   1620
      TabIndex        =   4
      Top             =   3120
      Visible         =   0   'False
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H00C0C0C0&
      Height          =   375
      Index           =   4
      Left            =   120
      TabIndex        =   3
      Top             =   3120
      Visible         =   0   'False
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H00C0C0C0&
      Height          =   375
      Index           =   3
      Left            =   3120
      TabIndex        =   2
      Top             =   2640
      Visible         =   0   'False
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H00C0C0C0&
      Height          =   375
      Index           =   2
      Left            =   1620
      TabIndex        =   1
      Top             =   2640
      Visible         =   0   'False
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H00C0C0C0&
      Height          =   375
      Index           =   1
      Left            =   120
      TabIndex        =   0
      Top             =   2640
      Visible         =   0   'False
      Width           =   1215
   End
   Begin VB.Label lblMessage 
      BackColor       =   &H00C0C0C0&
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2505
      Left            =   120
      TabIndex        =   6
      Top             =   60
      Width           =   4215
   End
End
Attribute VB_Name = "frmMsgBox"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click(Index As Integer)
  iMsgBoxBtn = Index
  Hide
End Sub

Private Sub Form_Activate()
  Dim i As Integer
  
  For i = 1 To 6
    If Command1(i).Visible Then
      Command1(i).SetFocus
      Exit For
    End If
  Next i
End Sub

