VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Begin VB.Form frmMain 
   BackColor       =   &H00C0C0C0&
   BorderStyle     =   1  'Fixed Single
   Caption         =   "v"
   ClientHeight    =   10020
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   12165
   ClipControls    =   0   'False
   Icon            =   "frmMain.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   10020
   ScaleWidth      =   12165
   StartUpPosition =   3  'Windows Default
   Begin VB.Timer TimerProbe 
      Enabled         =   0   'False
      Interval        =   500
      Left            =   7080
      Top             =   9600
   End
   Begin VB.Timer Timer3 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   7560
      Top             =   9600
   End
   Begin VB.Timer TimerStatusByteRec 
      Enabled         =   0   'False
      Interval        =   500
      Left            =   8160
      Top             =   9600
   End
   Begin VB.Timer Timer2b 
      Enabled         =   0   'False
      Interval        =   450
      Left            =   8750
      Top             =   9600
   End
   Begin VB.Timer Timer1b 
      Enabled         =   0   'False
      Interval        =   450
      Left            =   8750
      Top             =   9120
   End
   Begin MSComctlLib.ProgressBar ProgressBar 
      Height          =   255
      Left            =   9240
      TabIndex        =   111
      Top             =   9120
      Visible         =   0   'False
      Width           =   135
      _ExtentX        =   238
      _ExtentY        =   450
      _Version        =   393216
      Appearance      =   1
      Enabled         =   0   'False
   End
   Begin VB.Timer Timer2 
      Enabled         =   0   'False
      Interval        =   900
      Left            =   8520
      Top             =   9600
   End
   Begin VB.Timer Timer1 
      Enabled         =   0   'False
      Interval        =   900
      Left            =   8520
      Top             =   9120
   End
   Begin MSComctlLib.ImageList ImageList2 
      Left            =   11520
      Top             =   9360
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   15
      ImageHeight     =   15
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   2
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmMain.frx":030A
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmMain.frx":0425
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin VB.PictureBox picStatus 
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   255
      Index           =   4
      Left            =   10920
      Picture         =   "frmMain.frx":0800
      ScaleHeight     =   255
      ScaleWidth      =   255
      TabIndex        =   75
      Top             =   9120
      Width           =   255
   End
   Begin VB.PictureBox picStatus 
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   255
      Index           =   3
      Left            =   10560
      Picture         =   "frmMain.frx":090B
      ScaleHeight     =   255
      ScaleWidth      =   255
      TabIndex        =   74
      Top             =   9120
      Width           =   255
   End
   Begin VB.PictureBox picStatus 
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   255
      Index           =   2
      Left            =   10200
      Picture         =   "frmMain.frx":0A16
      ScaleHeight     =   255
      ScaleWidth      =   255
      TabIndex        =   73
      Top             =   9120
      Width           =   255
   End
   Begin VB.PictureBox picStatus 
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   255
      Index           =   1
      Left            =   9840
      Picture         =   "frmMain.frx":0B21
      ScaleHeight     =   255
      ScaleWidth      =   255
      TabIndex        =   72
      Top             =   9120
      Width           =   255
   End
   Begin VB.PictureBox picStatus 
      Appearance      =   0  'Flat
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   255
      Index           =   0
      Left            =   9480
      Picture         =   "frmMain.frx":0C2C
      ScaleHeight     =   255
      ScaleWidth      =   255
      TabIndex        =   71
      Top             =   9120
      Width           =   255
   End
   Begin VB.Frame fraProgramNavigation 
      BackColor       =   &H00C0C0C0&
      Caption         =   "TPS Navigation"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   120
      TabIndex        =   18
      Top             =   9000
      Width           =   3375
      Begin Threed.SSCommand Quit 
         Height          =   435
         Left            =   120
         TabIndex        =   19
         Top             =   240
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&Quit"
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
         Enabled         =   0   'False
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin Threed.SSCommand MainMenuButton 
         Height          =   435
         Left            =   1680
         TabIndex        =   20
         Top             =   240
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&Main Menu"
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
         Enabled         =   0   'False
         BevelWidth      =   3
      End
   End
   Begin VB.Frame fraProgramControl 
      BackColor       =   &H00C0C0C0&
      Caption         =   "TPS Control"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   3600
      TabIndex        =   58
      Top             =   9000
      Width           =   4815
      Begin Threed.SSCommand NewUUT 
         Height          =   435
         Left            =   3240
         TabIndex        =   112
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&New UUT"
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
      Begin Threed.SSCommand YesButton 
         Height          =   435
         Left            =   1680
         TabIndex        =   108
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&Yes"
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
         Enabled         =   0   'False
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin Threed.SSCommand AbortButton 
         Height          =   435
         Left            =   1680
         TabIndex        =   59
         Top             =   240
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&Abort"
         ForeColor       =   255
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Enabled         =   0   'False
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin Threed.SSCommand Continue 
         Height          =   435
         Left            =   3240
         TabIndex        =   60
         Top             =   240
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&Continue"
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
         Enabled         =   0   'False
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin Threed.SSCommand RerunButton 
         Height          =   435
         Left            =   1680
         TabIndex        =   61
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "R&erun"
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
         Enabled         =   0   'False
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin Threed.SSCommand PrintButton 
         Height          =   435
         Left            =   120
         TabIndex        =   62
         Top             =   240
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&Print"
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
         Enabled         =   0   'False
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin Threed.SSCommand NoButton 
         Height          =   435
         Left            =   3240
         TabIndex        =   109
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&No"
         ForeColor       =   255
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Enabled         =   0   'False
         BevelWidth      =   3
         Font3D          =   3
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   11520
      Top             =   9120
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   118
      ImageHeight     =   84
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   3
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmMain.frx":0F6E
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmMain.frx":8490
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmMain.frx":F9B2
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin Threed.SSPanel Panel 
      Height          =   8895
      Left            =   120
      TabIndex        =   4
      Top             =   120
      Width           =   11970
      _Version        =   65536
      _ExtentX        =   21114
      _ExtentY        =   15690
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
      Begin VB.Frame frMeasContinuous 
         Caption         =   "Measurement"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   3135
         Left            =   1800
         TabIndex        =   131
         Top             =   2760
         Visible         =   0   'False
         Width           =   4095
         Begin VB.TextBox txtMeasuredBig 
            Alignment       =   2  'Center
            BackColor       =   &H0000FF00&
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   24
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   675
            Left            =   360
            TabIndex        =   132
            Text            =   "1.0"
            Top             =   1320
            Width           =   3375
         End
         Begin VB.Label lblHigh 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            Caption         =   "1.0"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   24
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   615
            Left            =   360
            TabIndex        =   134
            Top             =   360
            Width           =   3375
         End
         Begin VB.Label lblLow 
            Alignment       =   2  'Center
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   1  'Fixed Single
            Caption         =   "1.0"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   24
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   615
            Left            =   360
            TabIndex        =   133
            Top             =   2160
            Width           =   3375
         End
      End
      Begin VB.PictureBox MainMenu 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         ForeColor       =   &H80000008&
         Height          =   8655
         Left            =   240
         ScaleHeight     =   8625
         ScaleWidth      =   7110
         TabIndex        =   8
         Top             =   120
         Width           =   7140
         Begin VB.ComboBox cboPartsList 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   360
            ItemData        =   "frmMain.frx":16ED4
            Left            =   2520
            List            =   "frmMain.frx":16ED6
            Style           =   2  'Dropdown List
            TabIndex        =   128
            Top             =   6600
            Width           =   3375
         End
         Begin VB.ComboBox cboAssembly 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   360
            ItemData        =   "frmMain.frx":16ED8
            Left            =   2520
            List            =   "frmMain.frx":16EDA
            Style           =   2  'Dropdown List
            TabIndex        =   127
            Top             =   5040
            Width           =   3375
         End
         Begin VB.ComboBox cboSchematic 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   360
            ItemData        =   "frmMain.frx":16EDC
            Left            =   2520
            List            =   "frmMain.frx":16EDE
            Style           =   2  'Dropdown List
            TabIndex        =   126
            Top             =   3480
            Width           =   3375
         End
         Begin VB.PictureBox picTestDocumentation 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            DrawWidth       =   2
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            FontTransparent =   0   'False
            ForeColor       =   &H80000008&
            Height          =   8655
            Left            =   0
            ScaleHeight     =   8625
            ScaleWidth      =   345
            TabIndex        =   96
            Top             =   0
            Width           =   375
            Begin VB.ComboBox cboITAPartsList 
               Appearance      =   0  'Flat
               BackColor       =   &H00C0C0C0&
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   9.75
                  Charset         =   0
                  Weight          =   400
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               Height          =   360
               Left            =   2640
               Style           =   2  'Dropdown List
               TabIndex        =   125
               Top             =   4920
               Width           =   3375
            End
            Begin VB.ComboBox cboITAAssy 
               Appearance      =   0  'Flat
               BackColor       =   &H00C0C0C0&
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   9.75
                  Charset         =   0
                  Weight          =   400
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               Height          =   360
               Left            =   2640
               Style           =   2  'Dropdown List
               TabIndex        =   124
               Top             =   3360
               Width           =   3375
            End
            Begin VB.ComboBox cboITAWiring 
               Appearance      =   0  'Flat
               BackColor       =   &H00C0C0C0&
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   9.75
                  Charset         =   0
                  Weight          =   400
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               Height          =   360
               ItemData        =   "frmMain.frx":16EE0
               Left            =   2640
               List            =   "frmMain.frx":16EE2
               Style           =   2  'Dropdown List
               TabIndex        =   123
               Top             =   1800
               Width           =   3375
            End
            Begin Threed.SSCommand MenuOption 
               Height          =   1110
               Index           =   7
               Left            =   1320
               TabIndex        =   98
               ToolTipText     =   "Launches ID Schematic View Software"
               Top             =   1080
               Width           =   1110
               _Version        =   65536
               _ExtentX        =   1958
               _ExtentY        =   1958
               _StockProps     =   78
               ForeColor       =   8388608
               BevelWidth      =   3
               Font3D          =   3
               AutoSize        =   2
               Picture         =   "frmMain.frx":16EE4
            End
            Begin Threed.SSCommand MenuOption 
               Height          =   1110
               Index           =   8
               Left            =   1320
               TabIndex        =   99
               ToolTipText     =   "Launches ID Assembly View Software"
               Top             =   2640
               Width           =   1110
               _Version        =   65536
               _ExtentX        =   1958
               _ExtentY        =   1958
               _StockProps     =   78
               ForeColor       =   8388608
               BevelWidth      =   3
               Font3D          =   3
               AutoSize        =   1
               Picture         =   "frmMain.frx":17776
            End
            Begin Threed.SSCommand MenuOption 
               Height          =   1095
               Index           =   10
               Left            =   6600
               TabIndex        =   100
               ToolTipText     =   "Displays TSR file"
               Top             =   1080
               Width           =   1095
               _Version        =   65536
               _ExtentX        =   1931
               _ExtentY        =   1931
               _StockProps     =   78
               ForeColor       =   8388608
               BevelWidth      =   3
               Font3D          =   3
               AutoSize        =   1
               Picture         =   "frmMain.frx":1EC98
            End
            Begin Threed.SSCommand MenuOption 
               Height          =   1095
               Index           =   11
               Left            =   6600
               TabIndex        =   101
               ToolTipText     =   "Displays ELTD file"
               Top             =   2640
               Width           =   1095
               _Version        =   65536
               _ExtentX        =   1931
               _ExtentY        =   1931
               _StockProps     =   78
               ForeColor       =   8388608
               BevelWidth      =   3
               Font3D          =   3
               AutoSize        =   1
               MouseIcon       =   "frmMain.frx":1EFB2
               Picture         =   "frmMain.frx":1F2CC
            End
            Begin Threed.SSCommand MenuOption 
               Height          =   1110
               Index           =   9
               Left            =   1320
               TabIndex        =   106
               ToolTipText     =   "Launches ID Parts List View Software"
               Top             =   4200
               Width           =   1110
               _Version        =   65536
               _ExtentX        =   1958
               _ExtentY        =   1958
               _StockProps     =   78
               ForeColor       =   8388608
               BevelWidth      =   3
               Font3D          =   3
               AutoSize        =   2
               Picture         =   "frmMain.frx":1F5E6
            End
            Begin Threed.SSCommand MenuOption 
               Height          =   1095
               Index           =   12
               Left            =   6600
               TabIndex        =   113
               ToolTipText     =   "Displays General Safty Information"
               Top             =   4200
               Width           =   1095
               _Version        =   65536
               _ExtentX        =   1931
               _ExtentY        =   1931
               _StockProps     =   78
               ForeColor       =   8388608
               BevelWidth      =   3
               Font3D          =   3
               AutoSize        =   1
               MouseIcon       =   "frmMain.frx":1FE78
               Picture         =   "frmMain.frx":20192
            End
            Begin VB.Label MenuOptionText 
               Appearance      =   0  'Flat
               AutoSize        =   -1  'True
               BackColor       =   &H80000005&
               Caption         =   "General Information and Safety Precautions"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   12
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00000000&
               Height          =   600
               Index           =   12
               Left            =   7920
               TabIndex        =   114
               Top             =   4440
               Width           =   3030
               WordWrap        =   -1  'True
            End
            Begin VB.Label MenuOptionText 
               Appearance      =   0  'Flat
               AutoSize        =   -1  'True
               BackColor       =   &H80000005&
               Caption         =   "View ITA Assembly Drawing"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   12
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00000000&
               Height          =   600
               Index           =   8
               Left            =   2640
               TabIndex        =   107
               Top             =   2640
               Width           =   2715
               WordWrap        =   -1  'True
            End
            Begin VB.Label MenuOptionText 
               Appearance      =   0  'Flat
               AutoSize        =   -1  'True
               BackColor       =   &H80000005&
               Caption         =   "View English Language Test Description (ELTD)"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   12
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00000000&
               Height          =   900
               Index           =   11
               Left            =   7920
               TabIndex        =   105
               Top             =   2760
               Width           =   3015
               WordWrap        =   -1  'True
            End
            Begin VB.Label MenuOptionText 
               Appearance      =   0  'Flat
               AutoSize        =   -1  'True
               BackColor       =   &H80000005&
               Caption         =   "View Test Strategy Report (TSR)"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   12
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00000000&
               Height          =   600
               Index           =   10
               Left            =   7920
               TabIndex        =   104
               Top             =   1320
               Width           =   2865
               WordWrap        =   -1  'True
            End
            Begin VB.Label MenuOptionText 
               Appearance      =   0  'Flat
               BackColor       =   &H80000005&
               Caption         =   "View ITA Parts List"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   12
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00000000&
               Height          =   405
               Index           =   9
               Left            =   2640
               TabIndex        =   103
               Top             =   4560
               Width           =   3075
            End
            Begin VB.Label MenuOptionText 
               Appearance      =   0  'Flat
               AutoSize        =   -1  'True
               BackColor       =   &H80000005&
               Caption         =   "View ITA And Accessory Wiring Diagram"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   12
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00000000&
               Height          =   600
               Index           =   7
               Left            =   2640
               TabIndex        =   102
               Top             =   1080
               Width           =   3300
               WordWrap        =   -1  'True
            End
            Begin VB.Label Label1 
               Appearance      =   0  'Flat
               AutoSize        =   -1  'True
               BackColor       =   &H80000005&
               Caption         =   "TEST DOCUMENTATION MENU"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   13.5
                  Charset         =   0
                  Weight          =   700
                  Underline       =   -1  'True
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00FF0000&
               Height          =   360
               Left            =   3600
               TabIndex        =   97
               Top             =   120
               Width           =   4590
            End
         End
         Begin Threed.SSCommand MenuOption 
            Height          =   1110
            Index           =   4
            Left            =   1200
            TabIndex        =   3
            ToolTipText     =   "Launches Parts List View Software"
            Top             =   5880
            Width           =   1110
            _Version        =   65536
            _ExtentX        =   1958
            _ExtentY        =   1958
            _StockProps     =   78
            ForeColor       =   8388608
            BevelWidth      =   3
            Font3D          =   3
            AutoSize        =   2
            Picture         =   "frmMain.frx":204AC
         End
         Begin Threed.SSCommand MenuOption 
            Height          =   1110
            Index           =   2
            Left            =   1200
            TabIndex        =   1
            ToolTipText     =   "Launches Schematic View Software"
            Top             =   2760
            Width           =   1110
            _Version        =   65536
            _ExtentX        =   1958
            _ExtentY        =   1958
            _StockProps     =   78
            ForeColor       =   8388608
            BevelWidth      =   3
            Font3D          =   3
            AutoSize        =   2
            Picture         =   "frmMain.frx":20D3E
         End
         Begin Threed.SSCommand MenuOption 
            Height          =   1110
            Index           =   3
            Left            =   1200
            TabIndex        =   2
            ToolTipText     =   "Launches Assembly View Software"
            Top             =   4320
            Width           =   1110
            _Version        =   65536
            _ExtentX        =   1958
            _ExtentY        =   1958
            _StockProps     =   78
            ForeColor       =   8388608
            BevelWidth      =   3
            Font3D          =   3
            AutoSize        =   2
            Picture         =   "frmMain.frx":215D0
         End
         Begin Threed.SSCommand MenuOption 
            Height          =   1095
            Index           =   1
            Left            =   1200
            TabIndex        =   0
            ToolTipText     =   "Launches Test Program Software"
            Top             =   1200
            Width           =   1095
            _Version        =   65536
            _ExtentX        =   1931
            _ExtentY        =   1931
            _StockProps     =   78
            ForeColor       =   8388608
            BevelWidth      =   3
            Font3D          =   3
            AutoSize        =   1
            Picture         =   "frmMain.frx":21E62
         End
         Begin Threed.SSCommand MenuOption 
            Height          =   1095
            Index           =   5
            Left            =   6480
            TabIndex        =   38
            ToolTipText     =   "Launches ID Survey"
            Top             =   1200
            Width           =   1095
            _Version        =   65536
            _ExtentX        =   1931
            _ExtentY        =   1931
            _StockProps     =   78
            ForeColor       =   8388608
            BevelWidth      =   3
            Font3D          =   3
            AutoSize        =   1
            Picture         =   "frmMain.frx":43514
         End
         Begin Threed.SSCommand MenuOption 
            Height          =   1095
            Index           =   6
            Left            =   6480
            TabIndex        =   39
            ToolTipText     =   "Show's Test Documentation Menu"
            Top             =   2760
            Width           =   1095
            _Version        =   65536
            _ExtentX        =   1931
            _ExtentY        =   1931
            _StockProps     =   78
            ForeColor       =   8388608
            BevelWidth      =   3
            Font3D          =   3
            AutoSize        =   1
            Picture         =   "frmMain.frx":43530
         End
         Begin Threed.SSCommand MenuOption 
            Height          =   1095
            Index           =   13
            Left            =   6480
            TabIndex        =   120
            ToolTipText     =   "Display FAULT-FILE"
            Top             =   4320
            Width           =   1095
            _Version        =   65536
            _ExtentX        =   1931
            _ExtentY        =   1931
            _StockProps     =   78
            ForeColor       =   8388608
            BevelWidth      =   3
            Font3D          =   3
            AutoSize        =   1
            Picture         =   "frmMain.frx":45EA2
         End
         Begin Threed.SSCommand MenuOption 
            Height          =   1095
            Index           =   14
            Left            =   6480
            TabIndex        =   129
            ToolTipText     =   "Display FAULT-FILE"
            Top             =   5880
            Width           =   1095
            _Version        =   65536
            _ExtentX        =   1931
            _ExtentY        =   1931
            _StockProps     =   78
            ForeColor       =   8388608
            BevelWidth      =   3
            Font3D          =   3
            AutoSize        =   1
            Picture         =   "frmMain.frx":461BC
         End
         Begin VB.Label MenuOptionText 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "View Interactive Electronic Technical Manual (IETM)"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   600
            Index           =   14
            Left            =   7800
            TabIndex        =   130
            Top             =   6120
            Width           =   3330
            WordWrap        =   -1  'True
         End
         Begin VB.Label lblAbout 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "Program Information"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   -1  'True
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FF0000&
            Height          =   240
            Left            =   4440
            TabIndex        =   122
            Top             =   8280
            Width           =   2100
         End
         Begin VB.Label MenuOptionText 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "View Fault-File"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   300
            Index           =   13
            Left            =   7800
            TabIndex        =   121
            Top             =   4680
            Width           =   1800
         End
         Begin VB.Label MenuOptionText 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "View Test &Documentation"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   300
            Index           =   6
            Left            =   7800
            TabIndex        =   40
            Top             =   3120
            Width           =   3090
         End
         Begin VB.Label MenuOptionText 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "Run &ITA Survey"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   300
            Index           =   5
            Left            =   7800
            TabIndex        =   37
            Top             =   1560
            Width           =   1890
         End
         Begin VB.Label MenuOptionText 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "View UUT Parts &List"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   300
            Index           =   4
            Left            =   2520
            TabIndex        =   13
            Top             =   6240
            Width           =   2415
         End
         Begin VB.Label MenuOptionText 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            Caption         =   "View UUT Assembly &Diagram"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   645
            Index           =   3
            Left            =   2520
            TabIndex        =   12
            Top             =   4320
            Width           =   3075
         End
         Begin VB.Label MenuOptionText 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "View UUT &Schematic"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   300
            Index           =   2
            Left            =   2550
            TabIndex        =   11
            Top             =   3120
            Width           =   2535
         End
         Begin VB.Label MenuOptionText 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "&Run Test Program"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   300
            Index           =   1
            Left            =   2520
            TabIndex        =   10
            Top             =   1680
            Width           =   2190
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            Caption         =   "M A I N    M E N U"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   13.5
               Charset         =   0
               Weight          =   700
               Underline       =   -1  'True
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FF0000&
            Height          =   360
            Left            =   4320
            TabIndex        =   9
            Top             =   0
            Width           =   2580
         End
      End
      Begin VB.PictureBox PictureWindow 
         Appearance      =   0  'Flat
         AutoRedraw      =   -1  'True
         BackColor       =   &H80000005&
         ClipControls    =   0   'False
         DrawWidth       =   3
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   8715
         Left            =   8760
         ScaleHeight     =   8685
         ScaleWidth      =   165
         TabIndex        =   5
         Top             =   120
         Width           =   195
         Begin VB.PictureBox pinp 
            Appearance      =   0  'Flat
            AutoRedraw      =   -1  'True
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            BorderStyle     =   0  'None
            DrawWidth       =   2
            ForeColor       =   &H80000008&
            Height          =   5055
            Left            =   600
            ScaleHeight     =   5055
            ScaleWidth      =   8895
            TabIndex        =   14
            Top             =   960
            Width           =   8895
         End
         Begin VB.Label Message 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            BackColor       =   &H000000FF&
            BeginProperty Font 
               Name            =   "System"
               Size            =   9.75
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00FFFFFF&
            Height          =   480
            Left            =   1080
            TabIndex        =   93
            Top             =   240
            Visible         =   0   'False
            Width           =   795
         End
      End
      Begin VB.PictureBox ModuleMenu 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         ForeColor       =   &H80000008&
         Height          =   8655
         Index           =   0
         Left            =   7800
         ScaleHeight     =   8625
         ScaleWidth      =   705
         TabIndex        =   16
         Top             =   120
         Visible         =   0   'False
         Width           =   735
         Begin VB.PictureBox ModuleOuter 
            Appearance      =   0  'Flat
            BackColor       =   &H00FFFFFF&
            ForeColor       =   &H80000008&
            Height          =   8055
            Left            =   -120
            ScaleHeight     =   8025
            ScaleWidth      =   8625
            TabIndex        =   21
            Top             =   360
            Width           =   8655
            Begin VB.PictureBox ModuleInner 
               Appearance      =   0  'Flat
               BackColor       =   &H00FFFFFF&
               BorderStyle     =   0  'None
               ForeColor       =   &H80000008&
               Height          =   4095
               Left            =   120
               ScaleHeight     =   4095
               ScaleWidth      =   8535
               TabIndex        =   24
               Top             =   0
               Width           =   8535
               Begin Threed.SSCommand cmdPwrOnModule 
                  Height          =   615
                  Index           =   0
                  Left            =   2160
                  TabIndex        =   25
                  ToolTipText     =   "Click Here to Run PWR On Test "
                  Top             =   1680
                  Width           =   1095
                  _Version        =   65536
                  _ExtentX        =   1931
                  _ExtentY        =   1085
                  _StockProps     =   78
                  AutoSize        =   1
                  Picture         =   "frmMain.frx":464D6
               End
               Begin Threed.SSCommand cmdModule 
                  Height          =   615
                  Index           =   1
                  Left            =   2160
                  TabIndex        =   26
                  ToolTipText     =   "Click Here to Run Test Module"
                  Top             =   2400
                  Width           =   1095
                  _Version        =   65536
                  _ExtentX        =   1931
                  _ExtentY        =   1085
                  _StockProps     =   78
                  AutoSize        =   1
                  Picture         =   "frmMain.frx":467F0
               End
               Begin Threed.SSCommand cmdSTTOModule 
                  Height          =   615
                  Index           =   1
                  Left            =   2160
                  TabIndex        =   27
                  ToolTipText     =   "Click Here to Safe-To-Turn-On Test"
                  Top             =   960
                  Width           =   1095
                  _Version        =   65536
                  _ExtentX        =   1931
                  _ExtentY        =   1085
                  _StockProps     =   78
                  AutoSize        =   1
                  Picture         =   "frmMain.frx":46B0A
               End
               Begin Threed.SSCommand cmdEndToEnd 
                  Height          =   615
                  Index           =   0
                  Left            =   2160
                  TabIndex        =   28
                  ToolTipText     =   "Click Here to Run End-To-End Test"
                  Top             =   240
                  Width           =   1095
                  _Version        =   65536
                  _ExtentX        =   1931
                  _ExtentY        =   1085
                  _StockProps     =   78
                  AutoSize        =   1
                  Picture         =   "frmMain.frx":46E24
               End
               Begin VB.Label lblModuleRunTime 
                  BackColor       =   &H00FFFFFF&
                  Caption         =   "RunTime"
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
                  Index           =   1
                  Left            =   7080
                  TabIndex        =   119
                  Top             =   2520
                  Width           =   1455
               End
               Begin VB.Label lblPowerOnRunTime 
                  BackColor       =   &H00FFFFFF&
                  Caption         =   "RunTime"
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
                  Left            =   7080
                  TabIndex        =   118
                  Top             =   1800
                  Width           =   1455
               End
               Begin VB.Label lblSTTORunTime 
                  BackColor       =   &H00FFFFFF&
                  Caption         =   "RunTime"
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
                  Left            =   7080
                  TabIndex        =   117
                  Top             =   1080
                  Width           =   1455
               End
               Begin VB.Label lblEndToEndRunTime 
                  BackColor       =   &H00FFFFFF&
                  Caption         =   "RunTime"
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
                  Left            =   7080
                  TabIndex        =   116
                  Top             =   480
                  Width           =   1455
               End
               Begin VB.Label lblModuleName 
                  BackColor       =   &H00FFFFFF&
                  Caption         =   "Module 1 Test"
                  BeginProperty Font 
                     Name            =   "MS Sans Serif"
                     Size            =   12
                     Charset         =   0
                     Weight          =   700
                     Underline       =   0   'False
                     Italic          =   0   'False
                     Strikethrough   =   0   'False
                  EndProperty
                  ForeColor       =   &H00000000&
                  Height          =   375
                  Index           =   1
                  Left            =   3600
                  TabIndex        =   36
                  Top             =   2520
                  Width           =   3135
               End
               Begin VB.Label lblModuleStatus 
                  Alignment       =   2  'Center
                  Appearance      =   0  'Flat
                  BackColor       =   &H00FFFFC0&
                  BorderStyle     =   1  'Fixed Single
                  Caption         =   "Unknown"
                  BeginProperty Font 
                     Name            =   "MS Sans Serif"
                     Size            =   12
                     Charset         =   0
                     Weight          =   400
                     Underline       =   0   'False
                     Italic          =   0   'False
                     Strikethrough   =   0   'False
                  EndProperty
                  ForeColor       =   &H00C00000&
                  Height          =   375
                  Index           =   1
                  Left            =   360
                  TabIndex        =   35
                  Top             =   2520
                  Width           =   1215
               End
               Begin VB.Label lblPwrOnStatus 
                  Alignment       =   2  'Center
                  Appearance      =   0  'Flat
                  BackColor       =   &H00FFFFC0&
                  BorderStyle     =   1  'Fixed Single
                  Caption         =   "Unknown"
                  BeginProperty Font 
                     Name            =   "MS Sans Serif"
                     Size            =   12
                     Charset         =   0
                     Weight          =   400
                     Underline       =   0   'False
                     Italic          =   0   'False
                     Strikethrough   =   0   'False
                  EndProperty
                  ForeColor       =   &H00C00000&
                  Height          =   375
                  Index           =   0
                  Left            =   360
                  TabIndex        =   34
                  Top             =   1800
                  Width           =   1215
               End
               Begin VB.Label lblPwrOn 
                  BackColor       =   &H00FFFFFF&
                  Caption         =   "Power On Test"
                  BeginProperty Font 
                     Name            =   "MS Sans Serif"
                     Size            =   12
                     Charset         =   0
                     Weight          =   700
                     Underline       =   0   'False
                     Italic          =   0   'False
                     Strikethrough   =   0   'False
                  EndProperty
                  ForeColor       =   &H00000000&
                  Height          =   375
                  Index           =   0
                  Left            =   3600
                  TabIndex        =   33
                  Top             =   1800
                  Width           =   3135
               End
               Begin VB.Label lblSTTOStatus 
                  Alignment       =   2  'Center
                  Appearance      =   0  'Flat
                  BackColor       =   &H00FFFFC0&
                  BorderStyle     =   1  'Fixed Single
                  Caption         =   "Unknown"
                  BeginProperty Font 
                     Name            =   "MS Sans Serif"
                     Size            =   12
                     Charset         =   0
                     Weight          =   400
                     Underline       =   0   'False
                     Italic          =   0   'False
                     Strikethrough   =   0   'False
                  EndProperty
                  ForeColor       =   &H00C00000&
                  Height          =   375
                  Index           =   1
                  Left            =   360
                  TabIndex        =   32
                  Top             =   1080
                  Width           =   1215
               End
               Begin VB.Label lblSTTO 
                  BackColor       =   &H00FFFFFF&
                  Caption         =   "Safe-To-Turn-On Test"
                  BeginProperty Font 
                     Name            =   "MS Sans Serif"
                     Size            =   12
                     Charset         =   0
                     Weight          =   700
                     Underline       =   0   'False
                     Italic          =   0   'False
                     Strikethrough   =   0   'False
                  EndProperty
                  ForeColor       =   &H00000000&
                  Height          =   375
                  Index           =   1
                  Left            =   3600
                  TabIndex        =   31
                  Top             =   1080
                  Width           =   3255
               End
               Begin VB.Label lblEndToEndStatus 
                  Alignment       =   2  'Center
                  Appearance      =   0  'Flat
                  BackColor       =   &H00FFFFC0&
                  BorderStyle     =   1  'Fixed Single
                  Caption         =   "Unknown"
                  BeginProperty Font 
                     Name            =   "MS Sans Serif"
                     Size            =   12
                     Charset         =   0
                     Weight          =   400
                     Underline       =   0   'False
                     Italic          =   0   'False
                     Strikethrough   =   0   'False
                  EndProperty
                  ForeColor       =   &H00C00000&
                  Height          =   375
                  Index           =   0
                  Left            =   360
                  TabIndex        =   30
                  Top             =   360
                  Width           =   1215
               End
               Begin VB.Label lblEndToEnd 
                  BackColor       =   &H00FFFFFF&
                  Caption         =   "End-To-End Test"
                  BeginProperty Font 
                     Name            =   "MS Sans Serif"
                     Size            =   12
                     Charset         =   0
                     Weight          =   700
                     Underline       =   0   'False
                     Italic          =   0   'False
                     Strikethrough   =   0   'False
                  EndProperty
                  ForeColor       =   &H00000000&
                  Height          =   375
                  Index           =   0
                  Left            =   3600
                  TabIndex        =   29
                  Top             =   480
                  Width           =   3255
               End
            End
         End
         Begin VB.VScrollBar VScroll1 
            Height          =   8655
            Left            =   8400
            TabIndex        =   17
            Top             =   0
            Width           =   375
         End
         Begin VB.Label lblModuleStatusTitle 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H0000FFFF&
            BorderStyle     =   1  'Fixed Single
            Caption         =   "Run Time"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   375
            Index           =   3
            Left            =   6840
            TabIndex        =   115
            Top             =   0
            Width           =   1935
         End
         Begin VB.Label lblStatusTitle 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H0000FFFF&
            BorderStyle     =   1  'Fixed Single
            Caption         =   "Status"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   375
            Index           =   3
            Left            =   0
            TabIndex        =   95
            Top             =   0
            Width           =   1935
         End
         Begin VB.Label lblModuleStatusTitle 
            Appearance      =   0  'Flat
            BackColor       =   &H0000FFFF&
            BorderStyle     =   1  'Fixed Single
            Caption         =   " Name"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   375
            Index           =   2
            Left            =   3480
            TabIndex        =   57
            Top             =   0
            Width           =   5295
         End
         Begin VB.Label lblModuleStatusTitle 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H0000FFFF&
            BorderStyle     =   1  'Fixed Single
            Caption         =   "Status"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   375
            Index           =   0
            Left            =   2880
            TabIndex        =   23
            Top             =   5280
            Width           =   1815
         End
         Begin VB.Label lblModuleStatusTitle 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H0000FFFF&
            BorderStyle     =   1  'Fixed Single
            Caption         =   "Execute"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   12
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   375
            Index           =   1
            Left            =   1920
            TabIndex        =   22
            Top             =   0
            Width           =   1575
         End
      End
      Begin Threed.SSCommand cmdDiagnostics 
         Height          =   435
         Left            =   9000
         TabIndex        =   110
         Top             =   5400
         Visible         =   0   'False
         Width           =   2820
         _Version        =   65536
         _ExtentX        =   4974
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "Perform Diagnostics"
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
      Begin Threed.SSCommand cmdFHDB 
         Height          =   435
         Left            =   9000
         TabIndex        =   94
         Top             =   4920
         Visible         =   0   'False
         Width           =   2820
         _Version        =   65536
         _ExtentX        =   4974
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&Save Failure Data"
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
         Enabled         =   0   'False
         BevelWidth      =   3
         Font3D          =   3
      End
      Begin VB.PictureBox picDanger 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   0  'None
         ForeColor       =   &H80000008&
         Height          =   855
         Left            =   9480
         Picture         =   "frmMain.frx":47276
         ScaleHeight     =   57
         ScaleMode       =   3  'Pixel
         ScaleWidth      =   127
         TabIndex        =   85
         TabStop         =   0   'False
         Top             =   7560
         Width           =   1900
      End
      Begin VB.Frame fraInstructions 
         BackColor       =   &H00C0C0C0&
         Caption         =   "Instructions"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   2655
         Left            =   9000
         TabIndex        =   81
         Top             =   4800
         Visible         =   0   'False
         Width           =   2775
         Begin VB.PictureBox picInstructions 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            ForeColor       =   &H80000008&
            Height          =   2295
            Left            =   120
            ScaleHeight     =   2265
            ScaleWidth      =   2505
            TabIndex        =   82
            Top             =   240
            Width           =   2535
            Begin VB.Label lblPressContinue 
               Alignment       =   2  'Center
               BackColor       =   &H00FFFFFF&
               Caption         =   "PRESS CONTINUE AND/OR PROBE BUTTON"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   8.25
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00C00000&
               Height          =   375
               Left            =   0
               TabIndex        =   92
               Top             =   1800
               Width           =   2535
            End
            Begin VB.Label lblInstruction 
               BackColor       =   &H00FFFFFF&
               Caption         =   "Test Data"
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
               Height          =   255
               Index           =   6
               Left            =   0
               TabIndex        =   91
               Top             =   1440
               Width           =   2535
            End
            Begin VB.Label lblInstruction 
               BackColor       =   &H00FFFFFF&
               Caption         =   "Test Data"
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
               Height          =   255
               Index           =   5
               Left            =   0
               TabIndex        =   90
               Top             =   1200
               Width           =   2535
            End
            Begin VB.Label lblInstruction 
               BackColor       =   &H00FFFFFF&
               Caption         =   "Test Data"
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
               Height          =   255
               Index           =   4
               Left            =   0
               TabIndex        =   89
               Top             =   960
               Width           =   2535
            End
            Begin VB.Label lblInstruction 
               BackColor       =   &H00FFFFFF&
               Caption         =   "Test Data"
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
               Height          =   255
               Index           =   3
               Left            =   0
               TabIndex        =   88
               Top             =   720
               Width           =   2535
            End
            Begin VB.Label lblInstruction 
               BackColor       =   &H00FFFFFF&
               Caption         =   "Test Data"
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
               Height          =   255
               Index           =   2
               Left            =   0
               TabIndex        =   87
               Top             =   480
               Width           =   2535
            End
            Begin VB.Label lblInstruction 
               BackColor       =   &H00FFFFFF&
               Caption         =   "Test Data"
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
               Height          =   255
               Index           =   1
               Left            =   0
               TabIndex        =   84
               Top             =   240
               Width           =   2535
            End
            Begin VB.Label lblInstructionType 
               Alignment       =   2  'Center
               BackColor       =   &H00FFFFFF&
               Caption         =   "*** OPERATOR ACTION***"
               BeginProperty Font 
                  Name            =   "MS Sans Serif"
                  Size            =   8.25
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00C00000&
               Height          =   255
               Left            =   0
               TabIndex        =   83
               Top             =   0
               Width           =   2535
            End
         End
      End
      Begin VB.Frame fraInstrument 
         BackColor       =   &H00C0C0C0&
         Caption         =   "Instrument"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1095
         Left            =   9000
         TabIndex        =   76
         Top             =   1680
         Visible         =   0   'False
         Width           =   2775
         Begin VB.TextBox txtCommand 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   78
            TabStop         =   0   'False
            Top             =   600
            Width           =   1815
         End
         Begin VB.TextBox txtInstrument 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   77
            TabStop         =   0   'False
            Top             =   240
            Width           =   1815
         End
         Begin VB.Label lblCommand 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "Cmd:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   80
            Top             =   600
            Width           =   615
         End
         Begin VB.Label lblInstrument 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "Instr:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   79
            Top             =   240
            Width           =   615
         End
      End
      Begin VB.Frame fraMeasurement 
         BackColor       =   &H00C0C0C0&
         Caption         =   "Measurement"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1815
         Left            =   9000
         TabIndex        =   46
         Top             =   2880
         Visible         =   0   'False
         Width           =   2775
         Begin VB.TextBox txtUnit 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   55
            TabStop         =   0   'False
            Top             =   240
            Width           =   1815
         End
         Begin VB.TextBox txtLowerLimit 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   51
            TabStop         =   0   'False
            Top             =   1320
            Width           =   1815
         End
         Begin VB.TextBox txtUpperLimit 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   48
            TabStop         =   0   'False
            Top             =   600
            Width           =   1815
         End
         Begin VB.TextBox txtMeasured 
            BackColor       =   &H00FFFFFF&
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   47
            TabStop         =   0   'False
            Top             =   960
            Width           =   1815
         End
         Begin VB.Label lblUnit 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "Unit:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   56
            Top             =   240
            Width           =   615
         End
         Begin VB.Label lbLowerLimit 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "LLmt:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   52
            Top             =   1320
            Width           =   615
         End
         Begin VB.Label lbMeasured 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "Mea:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   49
            Top             =   960
            Width           =   615
         End
         Begin VB.Label lbUpperLimit 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "ULmt:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   50
            Top             =   600
            Width           =   615
         End
      End
      Begin VB.Frame fraTestInformation 
         BackColor       =   &H00C0C0C0&
         Caption         =   "Test Information"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1455
         Left            =   9000
         TabIndex        =   41
         Top             =   120
         Visible         =   0   'False
         Width           =   2775
         Begin VB.TextBox txtModule 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   53
            TabStop         =   0   'False
            Text            =   "test"
            Top             =   240
            Width           =   1815
         End
         Begin VB.TextBox txtTestName 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   45
            TabStop         =   0   'False
            Top             =   960
            Width           =   1815
         End
         Begin VB.TextBox txtStep 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00800000&
            Height          =   285
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   42
            TabStop         =   0   'False
            Text            =   "test"
            Top             =   600
            Width           =   1815
         End
         Begin VB.Label lblModule 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "Mod:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   54
            Top             =   240
            Width           =   615
         End
         Begin VB.Label lblName 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "Name:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   44
            Top             =   960
            Width           =   615
         End
         Begin VB.Label lblStep 
            Alignment       =   1  'Right Justify
            BackColor       =   &H00C0C0C0&
            Caption         =   "Step:"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   43
            Top             =   600
            Width           =   615
         End
      End
      Begin VB.TextBox TextWindow 
         Appearance      =   0  'Flat
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1980
         Left            =   5520
         MultiLine       =   -1  'True
         TabIndex        =   6
         Top             =   2880
         Visible         =   0   'False
         Width           =   1665
      End
      Begin VB.PictureBox SeqTextWindow 
         Appearance      =   0  'Flat
         AutoRedraw      =   -1  'True
         BackColor       =   &H80000005&
         ClipControls    =   0   'False
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   6615
         Left            =   360
         ScaleHeight     =   6585
         ScaleWidth      =   6390
         TabIndex        =   7
         Top             =   120
         Visible         =   0   'False
         Width           =   6420
         Begin VB.Label lblTestResults 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H0000FFFF&
            BorderStyle     =   1  'Fixed Single
            Caption         =   "TEST RESULTS"
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   18
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   495
            Left            =   240
            TabIndex        =   15
            Top             =   240
            Width           =   3735
         End
      End
      Begin VB.Label lblPowerApplied 
         Alignment       =   2  'Center
         BackColor       =   &H00C0C0C0&
         Caption         =   "POWER APPLIED"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   9120
         TabIndex        =   86
         Top             =   8400
         Width           =   2655
      End
   End
   Begin Threed.SSPanel SSPanel1 
      Height          =   2415
      Left            =   120
      TabIndex        =   65
      Top             =   120
      Width           =   11895
      _Version        =   65536
      _ExtentX        =   20981
      _ExtentY        =   4260
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
         Height          =   2175
         Left            =   120
         ScaleHeight     =   2145
         ScaleWidth      =   11265
         TabIndex        =   66
         Top             =   120
         Width           =   11295
         Begin VB.PictureBox msgInBox 
            BackColor       =   &H00FFFFFF&
            BorderStyle     =   0  'None
            Height          =   1455
            Left            =   0
            ScaleHeight     =   1455
            ScaleWidth      =   11175
            TabIndex        =   67
            Top             =   0
            Width           =   11175
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
               TabIndex        =   68
               Top             =   0
               Width           =   11055
               WordWrap        =   -1  'True
            End
         End
      End
      Begin VB.VScrollBar VScroll2 
         Height          =   2175
         Left            =   11400
         TabIndex        =   69
         Top             =   120
         Width           =   375
      End
   End
   Begin Threed.SSPanel SSPanel2 
      Height          =   6495
      Left            =   120
      TabIndex        =   63
      Top             =   2520
      Width           =   11895
      _Version        =   65536
      _ExtentX        =   20981
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
         ScaleWidth      =   775
         TabIndex        =   64
         Top             =   120
         Width           =   11655
      End
   End
   Begin VB.Label lblStatus 
      Alignment       =   2  'Center
      BackColor       =   &H00C0C0C0&
      Caption         =   "Waiting For User ..."
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   375
      Left            =   8880
      TabIndex        =   70
      Top             =   9480
      Width           =   3015
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Public Sub AbortButton_Click()

    'Prevent user from selecting Abort Button more than once
    TimerStatusByteRec.Enabled = False
    frmMain.cmdFHDB.Enabled = False
    frmMain.cmdFHDB.Visible = False
    AbortButton.Enabled = False
    
    If ProgressBar.Visible Then
        ProgressBar.Visible = False
    End If
    
    frmMain.lblStatus.Caption = "TESTING ABORTED!"
    frmMain.lblStatus.ForeColor = vbRed
    Echo ""
    Echo ""
    Echo "*******************************************************************************"
    Echo "USER ABORTED TESTING - DATE/TIME: " & Format$(Now, "dddd, d-mmm-yyyy h:mm")
    Echo "*******************************************************************************"

    UserEvent = ABORT_BUTTON
    TETS.ResetSystem
    
End Sub

Private Sub cmdDiagnostics_Click()

    frmMain.cmdDiagnostics.Visible = False
    UserEvent% = DIAGNOSTIC_BUTTON

End Sub

Private Sub cmdEndToEnd_Click(index As Integer)

    frmMain.ModuleMenu(0).Visible = False
    frmMain.SeqTextWindow.Visible = True
    bEndToEnd = True
    bFirstRun = False
    frmMain.NewUUT.Visible = False
    TimerStatusByteRec.Enabled = True
    UserEvent% = END_TO_END
    
End Sub

Private Sub cmdFHDB_Click()

    Dim i As Integer
    Dim sTmpString As String
    Dim sStartTmp As String
    Dim sStopTmp As String
    Dim sStatus As String
    Dim sCmdString As String
                             
    frmMain.cmdFHDB.Enabled = False
    frmMain.cmdFHDB.Visible = False
    CenterForm frmFHDBComment
    frmFHDBComment.Show 1
    
    sTmpString = """"
    sTmpString = sTmpString + sCallout
    sTmpString = sTmpString + """"
    sCallout = sTmpString
    
    sTmpString = """"
    sTmpString = sTmpString + sUOM
    sTmpString = sTmpString + """"
    sUOM = sTmpString
    
    sTmpString = """"
    sTmpString = sTmpString + sComment
    sTmpString = sTmpString + """"
    sComment = sTmpString
    
    sStartTmp = Format(sStart, "YYMMDDhhmmSs")
    sStart = sStartTmp + "000"
    
    sStopTmp = Format(SStop, "YYMMDDhhmmSs")
    SStop = sStopTmp + "000"
                
    If Not bSimulation Then
        sCmdString = "fhdb.exe " & sStart & " " & SStop & " " & EROs & " "
        sCmdString = sCmdString + sTPCCN & " " & sUUTSerial & " "
        sCmdString = sCmdString + sUUTRev & " " & sIDSerial & " "
        sCmdString = sCmdString + sStatus & " " & sFailStep & " "
        sCmdString = sCmdString + sCallout & " " & dMeasurement & " "
        sCmdString = sCmdString + sUOM & " " & duLimit & " "
        sCmdString = sCmdString + " " & dlLimit & " " & sComment
        i = Shell(sCmdString, vbHide)
    End If
               
End Sub

Private Sub cmdModule_Click(index As Integer)

    frmMain.ModuleMenu(0).Visible = False
    frmMain.SeqTextWindow.Visible = True
    bEndToEnd = False
    bFirstRun = False
    frmMain.NewUUT.Visible = False
    TimerStatusByteRec.Enabled = True
    UserEvent% = index
    
End Sub

Private Sub cmdPwrOnModule_Click(index As Integer)

    frmMain.ModuleMenu(0).Visible = False
    frmMain.SeqTextWindow.Visible = True
    bEndToEnd = False
    bFirstRun = False
    frmMain.NewUUT.Visible = False
    TimerStatusByteRec.Enabled = True
    UserEvent% = PWR_ON
    
End Sub

Private Sub cmdSTTOModule_Click(index As Integer)

    frmMain.ModuleMenu(0).Visible = False
    frmMain.SeqTextWindow.Visible = True
    bEndToEnd = False
    bFirstRun = False
    frmMain.NewUUT.Visible = False
    TimerStatusByteRec.Enabled = True
    UserEvent% = STTO
    
End Sub

Public Sub Continue_Click()

    frmMain.frMeasContinuous.Visible = False
    frmMain.fraInstructions.Visible = False
    frmMain.cmdFHDB.Enabled = False
    frmMain.cmdFHDB.Visible = False
    UserEvent% = CONTINUE_BUTTON
    
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)

    Select Case KeyCode
    
        Case vbKeyEscape
            AbortButton_Click
            
        Case vbKeyA
            If Shift = 2 Then AbortButton_Click
            
        Case vbKeyR
            If Shift = 2 And frmMain.MainMenu.Visible = True Then MenuOption_Click (1)
            
        Case vbKeyS
            If Shift = 2 And frmMain.MainMenu.Visible = True Then MenuOption_Click (2)
            
        Case vbKeyD
            If Shift = 2 And frmMain.MainMenu.Visible = True Then MenuOption_Click (3)
            
        Case vbKeyL
            If Shift = 2 And frmMain.MainMenu.Visible = True Then MenuOption_Click (4)
            
        Case vbKeyI
            If Shift = 2 And frmMain.MainMenu.Visible = True Then MenuOption_Click (5)
            
        Case vbKeyT
            If Shift = 2 And frmMain.MainMenu.Visible = True Then MenuOption_Click (6)
            
        Case vbKeyM
            If Shift = 2 Then MainMenuButton_Click
            
        Case vbKeyQ
            If Shift = 2 Then Quit_Click
            
        Case vbKeyP
            If Shift = 2 Then PrintButton_Click
            
        Case vbKeyC
            If Shift = 2 Then Continue_Click
            
        Case vbKeyE
            If Shift = 2 Then RerunButton_Click
            
        Case vbKeyF12
        
            If SOF = True Then
              SOF = False
              Echo "Enable COF mode"
            Else
              SOF = True
              Echo "Enable SOF mode"
            End If
            
    End Select
    
    Continue.TabIndex = 5
    
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)

    If KeyAscii = vbKeyReturn And Continue.Enabled = True Then Continue_Click
    
End Sub

Private Sub Form_Load()
    
    FormMainLoaded = True
    frmMain.SSPanel1.Visible = False
    frmMain.SSPanel2.Visible = False
    
    Me.Width = 12255
    Me.Height = 10500
    
    Panel.Left = 120
    Panel.Top = 120
    Panel.Width = 11895
    Panel.Height = 8805
    
    MainMenu.Left = 120
    MainMenu.Top = 120
    MainMenu.Width = 11700
    MainMenu.Height = 8565
    picTestDocumentation.Left = 0
    picTestDocumentation.Top = 0
    picTestDocumentation.Width = 11700
    picTestDocumentation.Height = 8565
    picTestDocumentation.Visible = False
    
    ModuleInner.Width = 8535
    ModuleOuter.Width = 8655
    VScroll1.Height = 8565 - lblModuleStatusTitle(2).Height
    VScroll1.Top = lblModuleStatusTitle(2).Height
    lblModuleStatusTitle(2).Width = 5295
    
    ModuleMenu(0).Left = 120
    ModuleMenu(0).Top = 120
    ModuleMenu(0).Width = 8775
    ModuleMenu(0).Height = 8565
    ModuleMenu(0).Visible = False
    
    PictureWindow.Top = 120
    PictureWindow.Left = 120
    PictureWindow.Height = 8565
    PictureWindow.Width = 8835
    PictureWindow.Visible = False
     
    pinp.Top = 120
    pinp.Left = 120
    pinp.Height = 8565
    pinp.Width = 8835
    pinp.Visible = False
    
    fraInstructions.Visible = False
    lblPowerApplied.Visible = False
    picDanger.Visible = False
    
    frmMain.ModuleOuter.Height = 8565
    
    SeqTextWindow.Top = 120
    SeqTextWindow.Left = 120
    SeqTextWindow.Height = 8565
    SeqTextWindow.Width = 8700
    
    lblTestResults.Top = 0
    lblTestResults.Left = 0
    lblTestResults.Width = 8725
    lblTestResults.Height = 495
    
    TextWindow.Top = 120
    TextWindow.Left = 120
    TextWindow.Height = 8565
    TextWindow.Width = 11700
    TextWindow.Text = ""
    
    CenterForm Me
    
End Sub

Public Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    
    UserEvent% = QUIT_BUTTON
    frmMain.lblStatus.Caption = "Resetting System ..."
    
    Delay 7

    Unload ExitForm
    Unload frmImage
    FormMainLoaded = False

End Sub

Private Sub fraMeasurement_Click()

  If frMeasContinuous.Visible = False Then
    frMeasContinuous.Left = ((frmMain.Width - frMeasContinuous.Width) / 2) - 1000
    frMeasContinuous.Top = ((frmMain.Height - frMeasContinuous.Height)) / 2 + 1500
    
    If frmMain.ModuleMenu(0).Visible = False Then
      frMeasContinuous.Visible = True
    Else
      frMeasContinuous.Visible = False
    End If
  Else
    frMeasContinuous.Visible = False
  End If

End Sub

Private Sub frMeasContinuous_Click()

  frMeasContinuous.Visible = False

End Sub

Private Sub lblAbout_Click()

    MAINMod.ShowSplashScreen True
    
End Sub

Private Sub lblStatus_Change()

    If Left(UCase(lblStatus.Caption), 4) = "WAIT" Then
        frmMain.ProgressBar.Visible = False
    End If
    
End Sub

Private Sub MainMenuButton_Click()

    frmMain.cmdFHDB.Enabled = False
    frmMain.cmdFHDB.Visible = False
    frmMain.NewUUT.Visible = False
    frmMain.cmdDiagnostics.Visible = False
    UserEvent = MAINMENU_BUTTON
    frmMain.MainMenuButton.Enabled = False
    SchemPageNum% = 1
    AssyPageNum% = 1
    PartListPageNum% = 1
    UserEvent% = MAINMENU_BUTTON
    ShowMainMenu
    
End Sub

Private Sub MainMenuButton_KeyDown(KeyCode As Integer, Shift As Integer)

    Select Case KeyCode
    
        Case vbKeyR
            If Shift = 2 Then MenuOption_Click (1)
            
        Case vbKeyS
            If Shift = 2 Then MenuOption_Click (2)
            
        Case vbKeyA
            If Shift = 2 Then MenuOption_Click (3)
            
        Case vbKeyP
            If Shift = 2 Then MenuOption_Click (4)
            
    End Select
    
End Sub

Private Sub MenuOption_Click(index As Integer)

    Dim flash As Integer
    Dim x%
    
    index = -index
    
    'Change "Test Status" frame to "Image Options" if menu choice is to view drawings
    Select Case index
    
        Case VIEW_SCHEMATIC, VIEW_ASSEMBLY, VIEW_ID_SCHEMATIC, VIEW_ID_ASSEMBLY
            EnableImageControl (index)
            
        Case VIEW_PARTSLIST
            If LCase(Right(sPartsListFileNames(PartListPageNum%), 4)) <> ".txt" Then EnableImageControl (index)
            
        Case VIEW_ID_PARTSLIST
            If LCase(Right(sIDPartsListFileNames(PartListPageNum%), 4)) <> ".txt" Then EnableImageControl (index)
            
    End Select
    
    For flash% = 1 To 3
        MenuOptionText(index * -1).Visible = False
        x% = DoEvents()
        Delay (0.1)
        MenuOptionText(index * -1).Visible = True
        x% = DoEvents()
        Delay (0.1)
    Next flash%

    DoMenuChoice (index)
    
End Sub

Private Sub NewUUT_Click()

    bFirstRun = True
    sUUTSerial = ""
    frmMain.NewUUT.Visible = False
    ShowModuleMenu
    
End Sub

Private Sub NoButton_Click()

    frmMain.fraInstructions.Visible = False
    UserEvent = NO_BUTTON
    
End Sub

Private Sub PrintButton_Click()

    On Error GoTo Errorhandler
    
    If SeqTextWindow.Visible = True Then
        'Print Echo'ed test results of the current test run
        Printer.FontName = "Courier New"
        Printer.FontSize = 10
        Printer.FontBold = False
        Printer.Print TextWindow.Text
        Printer.EndDoc
    End If
    
    Exit Sub
    
Errorhandler:
     MsgBox "Error No.: " & Err.Number & vbCrLf _
       & "Description: " & Err.Description, 16, "Shell Error"
    Err.Clear
    Resume Next
    
End Sub

Public Sub Quit_Click()

    ExitForm.Show 1
    
End Sub

Private Sub RerunButton_Click()

    'Prevent user from selecting RERUN Button more than once
    frmMain.cmdFHDB.Enabled = False
    frmMain.cmdFHDB.Visible = False
    frmMain.cmdDiagnostics.Visible = False
    EnableAbort
    'Reset TextWindow to contain echoed test results of new run
    TextWindow.Text = "" + CRLF + CRLF + CRLF + CRLF + CRLF
    UserEvent% = RERUN_BUTTON
    
End Sub

Private Sub Timer1_Timer()

    Dim iCount As Integer
    Dim Interval As Integer
    
    Interval = 900
    frmMain.Timer2.Enabled = False
    frmMain.lblStatus.ForeColor = vbBlack
    
    For iCount = 0 To 4
        frmMain.picStatus(iCount).Visible = True
        frmMain.picStatus(iCount).Picture = frmMain.ImageList2.ListImages(2).Picture
    Next iCount
    
    frmMain.Timer1b.Interval = 450
    frmMain.Timer1b.Enabled = True
    
End Sub

Private Sub Timer1b_Timer()

    Dim iCount As Integer
        
    For iCount = 0 To 4
        frmMain.picStatus(iCount).Picture = frmMain.ImageList2.ListImages(1).Picture
    Next iCount
    
    frmMain.Timer1b.Enabled = False 'Oneshot Timer

End Sub

Private Sub Timer2_Timer()

    Dim Interval As Integer
    
    Interval = 900
    frmMain.Timer1.Enabled = False
    
    frmMain.lblStatus.ForeColor = &HFF0000
              
    frmMain.Timer2b.Interval = 450
    frmMain.Timer2b.Enabled = True
    DoEvents

End Sub

Private Sub Timer2b_Timer()

  frmMain.lblStatus.ForeColor = vbBlack
  frmMain.Timer2b.Enabled = False 'Oneshot Timer
  DoEvents
  
End Sub

Private Sub Timer3_Timer()

    If ProgressBar.value < ProgressBar.Max Then
        frmMain.ProgressBar.value = frmMain.ProgressBar.value + (100 / frmMain.ProgressBar.Max)
        DoEvents
    End If
    
End Sub

Private Sub TimerProbe_Timer()

    Dim bytStatus As Byte
    Dim Status As Integer
    
    If Not bSimulation Then ' get the PS actionbyte
    
      nSystErr = viReadSTB(ByVal nSupplyHandle&(1), Status)
      DoEvents
      
      If ((Status And &HF0) = &HB0) Then
         Status = Val(Str$(Status))
         bProbeClosed = Not ((Status And &H2) = &H2)
      End If
    End If
    
End Sub

Private Sub TimerStatusByteRec_Timer()

    Dim Status As Integer
    
    If Not bSimulation Then
      nSystErr = viReadSTB(ByVal nSupplyHandle&(1), Status)
      DoEvents
      
      If ((Status And &HF0) = &HB0) Then
         Status = Val(Str$(Status))
         bReceiverClosed = Not ((Status And &H1) = &H1)
         
         If Not bReceiverClosed Then
             AbortButton_Click
             frmMain.TimerStatusByteRec.Enabled = False
             Exit Sub
         End If
         
         bProbeClosed = Not ((Status And &H2) = &H2)
      End If
    End If

End Sub

Private Sub txtLowerLimit_Change()

    If Val(txtLowerLimit.Text) = -1E-99 Then
        txtLowerLimit.Text = "N/A"
        lblLow.Caption = "N/A"
    Else
        lblLow.Caption = txtLowerLimit.Text
    End If
    
End Sub

Private Sub txtMeasured_Change()

    frmMain.txtMeasuredBig = frmMain.txtMeasured
    DoEvents
    
    If frmMain.txtMeasured.Text = "" Then
        frmMain.txtMeasured.BackColor = vbWhite
        frmMain.txtMeasuredBig.BackColor = vbWhite
        Exit Sub
    End If
    
    If UCase(frmMain.txtUnit.Text) = "DTB" Then
    
        If UCase(frmMain.txtMeasured.Text) = "PASSED" Then
            frmMain.txtMeasured.BackColor = vbGreen
            frmMain.txtMeasuredBig.BackColor = vbGreen
            Pass = True
            Failed = False
        Else
            frmMain.txtMeasured.BackColor = vbRed
            frmMain.txtMeasuredBig.BackColor = vbRed
            Pass = False
            Failed = True
        End If
        
        Exit Sub
    End If
    
    If UCase(frmMain.txtUnit.Text) = "QUESTION" Or UCase(frmMain.txtUnit.Text) = "Y/N" Then
    
        If UCase(frmMain.txtMeasured.Text) = UCase(frmMain.txtUpperLimit.Text) Then
            frmMain.txtMeasured.BackColor = vbGreen
            frmMain.txtMeasuredBig.BackColor = vbGreen
            Pass = True
            Failed = False
        Else
            frmMain.txtMeasured.BackColor = vbRed
            frmMain.txtMeasuredBig.BackColor = vbRed
            Pass = False
            Failed = True
        End If
        
        Exit Sub
    End If
    
    If frmMain.txtUpperLimit.Text = "N/A" And frmMain.txtLowerLimit.Text = "N/A" Then
        frmMain.txtMeasured.BackColor = vbWhite
        frmMain.txtMeasuredBig.BackColor = vbWhite
        Exit Sub
    End If
    
    If IsNumeric(txtMeasured.Text) Then
    
        Select Case frmMain.txtCommand
        
            Case "dMeasImp"
            
            Case Else
            
                If Val(frmMain.txtMeasured.Text) = 9.9E+37 Then
                    frmMain.txtMeasured.BackColor = vbRed
                    frmMain.txtMeasured.Text = "No Meas"
                    frmMain.txtMeasuredBig.BackColor = vbRed
                    frmMain.txtMeasuredBig.Text = "No Meas"
                    Pass = False
                    Failed = True
                    OutHigh = True
                    OutLow = True
                    Exit Sub
                End If
                
        End Select
    End If
    
    If IsNumeric(txtMeasured.Text) Then
    
        If txtUpperLimit.Text = "N/A" Then
        
            If Val(txtMeasured.Text) >= Val(txtLowerLimit.Text) Then
                frmMain.txtMeasured.BackColor = vbGreen
                frmMain.txtMeasuredBig.BackColor = vbGreen
                Pass = True
                Failed = False
                OutHigh = True
                OutLow = False
            Else
                frmMain.txtMeasured.BackColor = vbRed
                
                If Val(frmMain.txtMeasured.Text) = 9.9E+37 Then
                    frmMain.txtMeasured.Text = "No Meas"
                    frmMain.txtMeasuredBig.Text = "No Meas"
                End If
                
                Pass = False
                Failed = True
                OutHigh = False
                OutLow = True
            End If
            
            Exit Sub
        End If
        
        If txtLowerLimit.Text = "N/A" Then
        
            If Val(txtMeasured.Text) <= Val(txtUpperLimit.Text) Then
                frmMain.txtMeasured.BackColor = vbGreen
                frmMain.txtMeasuredBig.BackColor = vbGreen
                Pass = True
                Failed = False
                OutHigh = False
                OutLow = True
            Else
                frmMain.txtMeasured.BackColor = vbRed
                frmMain.txtMeasuredBig.BackColor = vbRed
                Pass = False
                Failed = True
                OutHigh = True
                OutLow = False
            End If
            
            Exit Sub
        End If
        
        If Val(txtMeasured.Text) > Val(txtUpperLimit.Text) Or _
            Val(txtMeasured.Text) < Val(txtLowerLimit.Text) Then
            frmMain.txtMeasured.BackColor = vbRed
            frmMain.txtMeasuredBig.BackColor = vbRed
            Pass = False
            Failed = True
            
            If Val(txtMeasured.Text) > Val(txtUpperLimit.Text) Then OutHigh = True Else OutHigh = False
            If Val(txtMeasured.Text) < Val(txtLowerLimit.Text) Then OutLow = True Else OutLow = False
        Else
            frmMain.txtMeasured.BackColor = vbGreen
            frmMain.txtMeasuredBig.BackColor = vbGreen
            Pass = True
            Failed = False
            OutHigh = False
            OutLow = False
        End If
    Else
    
        If txtMeasured.Text <> txtUpperLimit.Text Then
            frmMain.txtMeasured.BackColor = vbRed
            frmMain.txtMeasuredBig.BackColor = vbRed
            Pass = False
            Failed = True
            OutHigh = False
            OutLow = False
        Else
            frmMain.txtMeasured.BackColor = vbGreen
            frmMain.txtMeasuredBig.BackColor = vbGreen
            Pass = True
            Failed = False
            OutHigh = False
            OutLow = False
        End If
    End If
    
End Sub

Private Sub txtUnit_Change()

    If UCase(txtUnit.Text) = "DTB" Then
        txtUpperLimit.Text = "N/A"
        txtLowerLimit.Text = "N/A"
        lblHigh.Caption = "N/A"
        lblLow.Caption = "N/A"
    End If
    
End Sub

Private Sub txtUpperLimit_Change()

    If Val(txtUpperLimit.Text) = 1E+99 Then
        txtUpperLimit.Text = "N/A"
        lblHigh.Caption = "N/A"
    Else
        lblHigh.Caption = txtUpperLimit.Text
    End If
    
End Sub

Private Sub VScroll1_Change()

    ' 60 is the whitespace between buttons
    frmMain.ModuleInner.Top = frmMain.ModuleOuter - (frmMain.VScroll1.value * (frmMain.cmdEndToEnd(0).Height + 154))
    
End Sub

Private Sub VScroll2_Change()

    frmMain.msgInBox.Top = frmMain.msgOutBox - (frmMain.VScroll2.value * (frmMain.lblMsg(1).Height + 120))
    
End Sub

Private Sub YesButton_Click()

    UserEvent = YES_BUTTON
    
End Sub
