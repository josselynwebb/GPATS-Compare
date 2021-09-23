VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form InputForm 
   Appearance      =   0  'Flat
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "***Operator Input***"
   ClientHeight    =   2295
   ClientLeft      =   2580
   ClientTop       =   2565
   ClientWidth     =   4440
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "MS Sans Serif"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H80000008&
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2295
   ScaleWidth      =   4440
   StartUpPosition =   2  'CenterScreen
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
      Left            =   480
      TabIndex        =   0
      Text            =   "Test"
      Top             =   960
      Width           =   3375
   End
   Begin Threed.SSPanel Panel3D1 
      Height          =   1410
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   4215
      _Version        =   65536
      _ExtentX        =   7435
      _ExtentY        =   2487
      _StockProps     =   15
      ForeColor       =   0
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Font3D          =   2
      Autosize        =   2
      Begin VB.Label InputLabel 
         Alignment       =   2  'Center
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         BorderStyle     =   1  'Fixed Single
         Caption         =   "Enter the complete UUT Part Number, then press OK"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   615
         Left            =   120
         TabIndex        =   2
         Top             =   120
         Width           =   3915
         WordWrap        =   -1  'True
      End
   End
   Begin Threed.SSCommand Continue 
      Height          =   435
      Left            =   2880
      TabIndex        =   3
      Top             =   1680
      Width           =   1500
      _Version        =   65536
      _ExtentX        =   2646
      _ExtentY        =   767
      _StockProps     =   78
      Caption         =   "Continue"
      ForeColor       =   8388608
      BevelWidth      =   3
      Font3D          =   3
   End
   Begin VB.Image imgLogo 
      Appearance      =   0  'Flat
      Height          =   495
      Left            =   120
      Picture         =   "Inputfrm.frx":0000
      Stretch         =   -1  'True
      Top             =   1680
      Width           =   1815
   End
End
Attribute VB_Name = "InputForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit


'Private Declare Function SetSysModalWindow Lib "User" (ByVal hWnd As Integer) As Integer
    'The SetSysModalWindow function is
    'obsolete. This function is provided
    'only for compatibility with 16-bit
    'versions of Windows. The new input
    'model does not allow for System
    'Modal windows.

'Used For Bringing An Application To the top of the Windows "Z-Order"
Private Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long

Const KEY_BCKSP = 8
Const KEY_ENTER = 13

Private Sub Continue_Click()
    If InputData.Text <> "" Then
        UserInputData = InputData.Text
        Unload InputForm
        SetStatus ("TESTING")
    End If
End Sub

Private Sub Form_Load()
    SetStatus ("WAITING")
    If frmMain.SeqTextWindow.Visible = True Then
        Me.Top = (frmMain.SeqTextWindow.Height / 2) - (Me.Height / 2)
        Me.Left = (frmMain.SeqTextWindow.Width / 2) - (Me.Width / 2)
    End If
End Sub

Private Sub InputData_KeyPress(KeyAscii As Integer)

    If KeyAscii = KEY_ENTER And InputData.Text <> "" Then
        KeyAscii = 0
        UserInputData = InputData.Text
        Unload InputForm
    Else
        'Only allow user to enter numbers if input is a number
        If InputNumber% Then
            If (Not IsNumber(KeyAscii) And KeyAscii <> KEY_BCKSP) Then
                KeyAscii = 0
            End If
        End If
    End If

End Sub

Private Function IsNumber(KeyAscii As Integer) As Integer

    'DESCRIPTION:
    '   Determines if a particular ASCII character is a Number
    'PARAMETER:
    '   KeyAscii% = ASCII character to test
    'RETURNS:
    '   True if KeyAscii is a number
    '   False if KeyAscii is not a number

    If (KeyAscii < 48 Or KeyAscii > 57) Then
        IsNumber% = False
    Else
        IsNumber% = True
    End If

End Function

Private Sub OK_Click()

    If InputData.Text <> "" Then
        UserInputData = InputData.Text
        Unload InputForm
        SetStatus ("TESTING")
    End If

End Sub

