VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmOperatorMsg 
   BackColor       =   &H00C0C0C0&
   Caption         =   "** Operator Action **"
   ClientHeight    =   6210
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   8415
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   Moveable        =   0   'False
   ScaleHeight     =   6210
   ScaleWidth      =   8415
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox InputData 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   405
      Left            =   3000
      TabIndex        =   0
      Top             =   5520
      Width           =   3135
   End
   Begin Threed.SSPanel SSPanel1 
      Height          =   5295
      Left            =   120
      TabIndex        =   2
      Top             =   120
      Width           =   7815
      _Version        =   65536
      _ExtentX        =   13785
      _ExtentY        =   9340
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
      Begin VB.PictureBox msgOutBox 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         ForeColor       =   &H80000008&
         Height          =   5055
         Left            =   120
         ScaleHeight     =   5025
         ScaleWidth      =   7545
         TabIndex        =   3
         Top             =   120
         Width           =   7575
         Begin VB.PictureBox msgInBox 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BorderStyle     =   0  'None
            ForeColor       =   &H80000008&
            Height          =   495
            Left            =   0
            ScaleHeight     =   495
            ScaleWidth      =   7575
            TabIndex        =   4
            Top             =   0
            Width           =   7575
            Begin VB.Label lblMsg 
               Alignment       =   2  'Center
               BackColor       =   &H00FFFFFF&
               BackStyle       =   0  'Transparent
               Caption         =   "12345678901234567890123456789012345678901234567890"
               BeginProperty Font 
                  Name            =   "Courier New"
                  Size            =   12
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00800000&
               Height          =   255
               Index           =   1
               Left            =   0
               TabIndex        =   5
               Top             =   120
               Width           =   7575
               WordWrap        =   -1  'True
            End
         End
      End
      Begin VB.VScrollBar VScroll1 
         Height          =   5055
         Left            =   7680
         TabIndex        =   6
         Top             =   120
         Width           =   375
      End
   End
   Begin Threed.SSCommand Continue 
      Height          =   435
      Left            =   6360
      TabIndex        =   1
      Top             =   5520
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
   Begin VB.Label lblInput 
      BackColor       =   &H00C0C0C0&
      Caption         =   "&Input:"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2160
      TabIndex        =   7
      Top             =   5520
      Width           =   855
   End
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   675
      Left            =   120
      Picture         =   "frmOperatorMsg.frx":0000
      Stretch         =   -1  'True
      Top             =   5475
      Width           =   1815
   End
End
Attribute VB_Name = "frmOperatorMsg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Private Sub Continue_Click()
    
    If InputData.Text <> "" And frmOperatorMsg.InputData.Visible = True Then
        UserInputData = InputData.Text
        frmOperatorMsg.Hide
        Unload frmOperatorMsg
    Else
        'added by Soon Nam 7/17/03
        'following line fixes a bug that sets UserInputData equal to
        'previous data if nothing was entered at user prompt
        UserInputData = ""
        'end add
        frmOperatorMsg.Hide
        Unload frmOperatorMsg
    End If
    
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)

    If KeyAscii = vbKeyReturn Then Continue_Click
    
End Sub

Private Sub InputData_KeyPress(KeyAscii As Integer)

    If KeyAscii = vbKeyReturn Then Continue_Click
    
End Sub

Private Sub VScroll1_Change()

    ' 60 is the whitespace between buttons
    frmOperatorMsg.msgInBox.Top = frmOperatorMsg.msgOutBox - (frmOperatorMsg.VScroll1.value * (frmOperatorMsg.lblMsg(1).Height + 120))

End Sub
