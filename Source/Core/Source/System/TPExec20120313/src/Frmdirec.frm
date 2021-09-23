VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmOperatorMsgImage 
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "** Operator Instruction **"
   ClientHeight    =   9450
   ClientLeft      =   270
   ClientTop       =   1170
   ClientWidth     =   11745
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   NegotiateMenus  =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   9450
   ScaleWidth      =   11745
   ShowInTaskbar   =   0   'False
   Begin Threed.SSPanel SSPanel2 
      Height          =   6495
      Left            =   120
      TabIndex        =   2
      Top             =   2280
      Width           =   11535
      _Version        =   65536
      _ExtentX        =   20346
      _ExtentY        =   11456
      _StockProps     =   15
      Caption         =   "SSPanel2"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Begin VB.PictureBox picGraphic 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         ClipControls    =   0   'False
         ForeColor       =   &H80000008&
         Height          =   6200
         Left            =   120
         ScaleHeight     =   411
         ScaleMode       =   3  'Pixel
         ScaleWidth      =   751
         TabIndex        =   3
         Top             =   120
         Width           =   11295
      End
   End
   Begin Threed.SSPanel SSPanel1 
      Height          =   2055
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   11535
      _Version        =   65536
      _ExtentX        =   20346
      _ExtentY        =   3625
      _StockProps     =   15
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
         BackColor       =   &H00FFFFFF&
         ForeColor       =   &H80000008&
         Height          =   1815
         Left            =   120
         ScaleHeight     =   1785
         ScaleWidth      =   10905
         TabIndex        =   4
         Top             =   120
         Width           =   10935
         Begin VB.PictureBox msgInBox 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   0  'None
            Height          =   1455
            Left            =   0
            ScaleHeight     =   1455
            ScaleWidth      =   10935
            TabIndex        =   5
            Top             =   0
            Width           =   10935
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
               Left            =   120
               TabIndex        =   7
               Top             =   0
               Width           =   10575
               WordWrap        =   -1  'True
            End
         End
      End
      Begin VB.VScrollBar VScroll1 
         Height          =   1815
         Left            =   11040
         TabIndex        =   6
         Top             =   120
         Width           =   375
      End
   End
   Begin Threed.SSCommand Continue 
      Height          =   435
      Left            =   10080
      TabIndex        =   0
      Top             =   8880
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
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   495
      Left            =   240
      Picture         =   "Frmdirec.frx":0000
      Stretch         =   -1  'True
      Top             =   8880
      Width           =   1815
   End
End
Attribute VB_Name = "frmOperatorMsgImage"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'************************************************************
'* Third Echelon Test Set (TETS) Software Module            *
'*                                                          *
'* Nomenclature   : System Self Test: Direction Box         *
'* Version        : 1.2                                     *
'* Written By     : Michael McCabe                          *
'* Last Update    : 1/5/97                                  *
'* Purpose        : This form is used to show set-up        *
'*                  instructions to the user                *
'************************************************************
Option Explicit

Private Sub Continue_Click()
    frmOperatorMsgImage.Hide
    Unload frmOperatorMsgImage
End Sub


Private Sub Form_KeyPress(KeyAscii As Integer)
    If KeyAscii = vbKeyReturn Then Continue_Click
End Sub

Private Sub VScroll1_Change()
    frmOperatorMsgImage.msgInBox.Top = frmOperatorMsgImage.msgOutBox - (frmOperatorMsgImage.VScroll1.Value * (frmOperatorMsgImage.lblMsg(1).Height + 120))
End Sub
