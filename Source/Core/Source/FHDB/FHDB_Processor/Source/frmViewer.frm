VERSION 5.00
Object = "{DB797681-40E0-11D2-9BD5-0060082AE372}#4.5#0"; "xceedzip.dll"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "comdlg32.ocx"
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Begin VB.Form frmViewer 
   Caption         =   "DataBase Viewer"
   ClientHeight    =   7125
   ClientLeft      =   60
   ClientTop       =   630
   ClientWidth     =   10890
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MDIChild        =   -1  'True
   ScaleHeight     =   7125
   ScaleWidth      =   10890
   WindowState     =   2  'Maximized
   Begin MSComDlg.CommonDialog SaveFile 
      Left            =   360
      Top             =   6600
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin MSComctlLib.Slider SldRecord 
      Height          =   255
      Left            =   6600
      TabIndex        =   52
      Top             =   6120
      Width           =   3855
      _ExtentX        =   6800
      _ExtentY        =   450
      _Version        =   393216
      LargeChange     =   10
   End
   Begin Threed.SSFrame ssfraSystem 
      Height          =   1935
      Left            =   240
      TabIndex        =   15
      Top             =   0
      Width           =   10455
      _Version        =   65536
      _ExtentX        =   18441
      _ExtentY        =   3413
      _StockProps     =   14
      Caption         =   "System Data"
      ForeColor       =   -2147483635
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Font3D          =   3
      Begin VB.TextBox txtData 
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   2
         Left            =   8325
         TabIndex        =   42
         Top             =   1515
         Width           =   1970
      End
      Begin VB.TextBox txtData 
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   1
         Left            =   8325
         TabIndex        =   40
         Top             =   1035
         Width           =   1970
      End
      Begin VB.TextBox txtData 
         Alignment       =   2  'Center
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   4
         Left            =   2565
         TabIndex        =   38
         Top             =   660
         Width           =   1740
      End
      Begin VB.TextBox txtData 
         Alignment       =   2  'Center
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   16
         Left            =   8325
         TabIndex        =   36
         Top             =   555
         Width           =   1290
      End
      Begin VB.TextBox txtData 
         Alignment       =   2  'Center
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   5
         Left            =   5205
         TabIndex        =   34
         Top             =   660
         Width           =   1290
      End
      Begin VB.TextBox txtData 
         Alignment       =   2  'Center
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   6
         Left            =   5205
         TabIndex        =   32
         Top             =   1515
         Width           =   1290
      End
      Begin VB.TextBox txtData 
         Alignment       =   2  'Center
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   7
         Left            =   2685
         TabIndex        =   30
         Top             =   1515
         Width           =   1290
      End
      Begin VB.TextBox txtData 
         Alignment       =   2  'Center
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   3
         Left            =   300
         TabIndex        =   28
         Top             =   1515
         Width           =   1290
      End
      Begin VB.TextBox txtData 
         Alignment       =   2  'Center
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   0
         Left            =   300
         TabIndex        =   26
         Top             =   660
         Width           =   1050
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   0
         Left            =   240
         TabIndex        =   27
         Top             =   600
         Width           =   1140
         _Version        =   65536
         _ExtentX        =   2002
         _ExtentY        =   520
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.11
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   3
         Left            =   240
         TabIndex        =   29
         Top             =   1440
         Width           =   1380
         _Version        =   65536
         _ExtentX        =   2434
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   7
         Left            =   2640
         TabIndex        =   31
         Top             =   1440
         Width           =   1380
         _Version        =   65536
         _ExtentX        =   2434
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   6
         Left            =   5160
         TabIndex        =   33
         Top             =   1440
         Width           =   1380
         _Version        =   65536
         _ExtentX        =   2434
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   5
         Left            =   5160
         TabIndex        =   35
         Top             =   600
         Width           =   1380
         _Version        =   65536
         _ExtentX        =   2434
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   16
         Left            =   8280
         TabIndex        =   37
         Top             =   480
         Width           =   1380
         _Version        =   65536
         _ExtentX        =   2434
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   4
         Left            =   2520
         TabIndex        =   39
         Top             =   600
         Width           =   1815
         _Version        =   65536
         _ExtentX        =   3201
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   1
         Left            =   8280
         TabIndex        =   41
         Top             =   960
         Width           =   2050
         _Version        =   65536
         _ExtentX        =   3616
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   2
         Left            =   8280
         TabIndex        =   43
         Top             =   1440
         Width           =   2050
         _Version        =   65536
         _ExtentX        =   3616
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "Record Number"
         Height          =   195
         Index           =   0
         Left            =   250
         TabIndex        =   24
         Top             =   360
         Width           =   1125
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "ERO"
         Height          =   195
         Index           =   3
         Left            =   700
         TabIndex        =   23
         Top             =   1200
         Width           =   345
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "TPCCN"
         Height          =   195
         Index           =   4
         Left            =   3100
         TabIndex        =   22
         Top             =   360
         Width           =   540
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "ID Serial Number"
         Height          =   195
         Index           =   7
         Left            =   2730
         TabIndex        =   21
         Top             =   1200
         Width           =   1200
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "UUT Serial Number"
         Height          =   195
         Index           =   5
         Left            =   5160
         TabIndex        =   20
         Top             =   360
         Width           =   1380
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "UUT Revision"
         Height          =   195
         Index           =   6
         Left            =   5350
         TabIndex        =   19
         Top             =   1200
         Width           =   1005
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "Temperature"
         Height          =   195
         Index           =   16
         Left            =   7260
         TabIndex        =   18
         Top             =   600
         Width           =   900
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "Start Time"
         Height          =   195
         Index           =   1
         Left            =   7440
         TabIndex        =   17
         Top             =   1080
         Width           =   720
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "Stop Time"
         Height          =   195
         Index           =   2
         Left            =   7440
         TabIndex        =   16
         Top             =   1560
         Width           =   720
      End
   End
   Begin Threed.SSFrame ssfraMeasurement 
      Height          =   1815
      Left            =   240
      TabIndex        =   5
      Top             =   2040
      Width           =   10455
      _Version        =   65536
      _ExtentX        =   18441
      _ExtentY        =   3201
      _StockProps     =   14
      Caption         =   "Measurement Data"
      ForeColor       =   -2147483635
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Font3D          =   3
      MousePointer    =   1
      Begin VB.TextBox txtData 
         Alignment       =   1  'Right Justify
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   11
         Left            =   3645
         TabIndex        =   46
         Top             =   840
         Width           =   2410
      End
      Begin VB.TextBox txtData 
         Alignment       =   1  'Right Justify
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   14
         Left            =   3645
         TabIndex        =   45
         Top             =   1380
         Width           =   2410
      End
      Begin VB.TextBox txtData 
         Alignment       =   1  'Right Justify
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   13
         Left            =   3645
         TabIndex        =   44
         Top             =   315
         Width           =   2410
      End
      Begin VB.TextBox txtData 
         BorderStyle     =   0  'None
         Height          =   220
         Index           =   9
         Left            =   285
         TabIndex        =   13
         Top             =   1380
         Width           =   1290
      End
      Begin VB.TextBox txtData 
         Alignment       =   2  'Center
         BorderStyle     =   0  'None
         ForeColor       =   &H00000000&
         Height          =   220
         Index           =   8
         Left            =   300
         TabIndex        =   10
         Top             =   547
         Width           =   1000
      End
      Begin VB.TextBox txtData 
         Height          =   1120
         Index           =   10
         Left            =   6600
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   6
         Top             =   480
         Width           =   3720
      End
      Begin Threed.SSPanel SSPnlPassFail 
         Height          =   350
         Left            =   240
         TabIndex        =   11
         Top             =   480
         Width           =   1140
         _Version        =   65536
         _ExtentX        =   2011
         _ExtentY        =   617
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.36
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   13
         Left            =   3600
         TabIndex        =   12
         Top             =   240
         Width           =   2505
         _Version        =   65536
         _ExtentX        =   4410
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   9
         Left            =   240
         TabIndex        =   14
         Top             =   1305
         Width           =   1380
         _Version        =   65536
         _ExtentX        =   2434
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   11
         Left            =   3600
         TabIndex        =   47
         Top             =   765
         Width           =   2505
         _Version        =   65536
         _ExtentX        =   4419
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin Threed.SSPanel SSPnlData 
         Height          =   300
         Index           =   14
         Left            =   3600
         TabIndex        =   48
         Top             =   1305
         Width           =   2505
         _Version        =   65536
         _ExtentX        =   4410
         _ExtentY        =   529
         _StockProps     =   15
         BackColor       =   16777215
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BevelWidth      =   2
         BorderWidth     =   0
         BevelOuter      =   0
         BevelInner      =   1
         FloodColor      =   16776960
      End
      Begin VB.Label lblData 
         Alignment       =   2  'Center
         AutoSize        =   -1  'True
         Caption         =   "Lower Limit"
         Height          =   195
         Index           =   14
         Left            =   2640
         TabIndex        =   51
         Top             =   1425
         Width           =   825
      End
      Begin VB.Label lblData 
         Alignment       =   2  'Center
         AutoSize        =   -1  'True
         Caption         =   "Upper Limit"
         Height          =   195
         Index           =   13
         Left            =   2670
         TabIndex        =   50
         Top             =   345
         Width           =   795
      End
      Begin VB.Label lblData 
         Alignment       =   2  'Center
         AutoSize        =   -1  'True
         Caption         =   "Failing Measurement"
         Height          =   195
         Index           =   11
         Left            =   2010
         TabIndex        =   49
         Top             =   855
         Width           =   1455
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "Fault Callout"
         Height          =   195
         Index           =   10
         Left            =   6600
         TabIndex        =   9
         Top             =   240
         Width           =   870
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "Failure Step"
         Height          =   195
         Index           =   9
         Left            =   500
         TabIndex        =   8
         Top             =   1080
         Width           =   840
      End
      Begin VB.Label lblData 
         AutoSize        =   -1  'True
         Caption         =   "Pass/Fail"
         Height          =   195
         Index           =   8
         Left            =   470
         TabIndex        =   7
         Top             =   240
         Width           =   660
      End
   End
   Begin Threed.SSCommand cmdFirst 
      Height          =   345
      Left            =   600
      TabIndex        =   0
      Top             =   6050
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "|<<"
   End
   Begin VB.TextBox txtComments 
      Height          =   1455
      Left            =   340
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   4
      Top             =   4320
      Width           =   10255
   End
   Begin Threed.SSCommand cmdPrevious 
      Height          =   345
      Left            =   1845
      TabIndex        =   1
      Top             =   6045
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   "<"
   End
   Begin Threed.SSCommand cmdNext 
      Height          =   345
      Left            =   3075
      TabIndex        =   2
      Top             =   6045
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   ">"
   End
   Begin Threed.SSCommand cmdLast 
      Height          =   345
      Left            =   4320
      TabIndex        =   3
      Top             =   6045
      Width           =   1125
      _Version        =   65536
      _ExtentX        =   1984
      _ExtentY        =   609
      _StockProps     =   78
      Caption         =   ">>|"
   End
   Begin Threed.SSFrame ssfraComment 
      Height          =   1850
      Left            =   240
      TabIndex        =   25
      Top             =   4005
      Width           =   10455
      _Version        =   65536
      _ExtentX        =   18441
      _ExtentY        =   3263
      _StockProps     =   14
      Caption         =   "Operator Comments"
      ForeColor       =   -2147483635
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.27
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Font3D          =   3
   End
   Begin XceedZipLibCtl.XceedZip xUnzip 
      Left            =   960
      Top             =   6600
      BasePath        =   ""
      CompressionLevel=   6
      EncryptionPassword=   ""
      RequiredFileAttributes=   0
      ExcludedFileAttributes=   24
      FilesToProcess  =   ""
      FilesToExclude  =   ""
      MinDateToProcess=   2
      MaxDateToProcess=   2958465
      MinSizeToProcess=   0
      MaxSizeToProcess=   0
      SplitSize       =   0
      PreservePaths   =   -1  'True
      ProcessSubfolders=   0   'False
      SkipIfExisting  =   0   'False
      SkipIfNotExisting=   0   'False
      SkipIfOlderDate =   0   'False
      SkipIfOlderVersion=   0   'False
      TempFolder      =   ""
      UseTempFile     =   -1  'True
      UnzipToFolder   =   ""
      ZipFilename     =   ""
      SpanMultipleDisks=   2
      ExtraHeaders    =   10
      ZipOpenedFiles  =   0   'False
      BackgroundProcessing=   0   'False
      SfxBinrayModule =   ""
      SfxDefaultPassword=   ""
      SfxDefaultUnzipToFolder=   ""
      SfxExistingFileBehavior=   0
      SfxReadmeFile   =   ""
      SfxExecuteAfter =   ""
      SfxInstallMode  =   0   'False
      SfxProgramGroup =   ""
      SfxProgramGroupItems=   ""
      SfxExtensionsToAssociate=   ""
      SfxIconFilename =   ""
   End
   Begin VB.Label lblScroller 
      Alignment       =   2  'Center
      AutoSize        =   -1  'True
      Caption         =   "Record Scroller"
      Height          =   195
      Left            =   8160
      TabIndex        =   53
      Top             =   5920
      Width           =   1125
   End
   Begin VB.Menu mnuFile 
      Caption         =   "&File"
      Begin VB.Menu mnuExportAsText 
         Caption         =   "Export as Text File"
         Begin VB.Menu mnuExportRecord 
            Caption         =   "Displayed Record"
         End
         Begin VB.Menu mnuExportRecordSet 
            Caption         =   "Multiple Records from Search Results"
         End
      End
      Begin VB.Menu mnuSep2 
         Caption         =   "-"
      End
      Begin VB.Menu mnuExit 
         Caption         =   "E&xit"
      End
   End
   Begin VB.Menu mnuOperation 
      Caption         =   "&Operation"
      Begin VB.Menu mnuImport 
         Caption         =   "I&mport"
      End
      Begin VB.Menu mnuExport 
         Caption         =   "&Export"
      End
      Begin VB.Menu mnuSep1 
         Caption         =   "-"
      End
      Begin VB.Menu mnuComment 
         Caption         =   "Add a &Comment"
      End
   End
   Begin VB.Menu mnuNavigation 
      Caption         =   "Na&vigation"
      Begin VB.Menu mnuFirst 
         Caption         =   "&First"
      End
      Begin VB.Menu mnuLast 
         Caption         =   "&Last"
      End
      Begin VB.Menu mnuPrevious 
         Caption         =   "&Previous"
      End
      Begin VB.Menu mnuNext 
         Caption         =   "&Next"
      End
   End
   Begin VB.Menu mnuSearch 
      Caption         =   "&Search"
      Begin VB.Menu mnuLoadAll 
         Caption         =   "Load all &Records"
      End
      Begin VB.Menu mnuSep3 
         Caption         =   "-"
      End
      Begin VB.Menu mnuSpecific 
         Caption         =   "S&pecific Record/s"
      End
   End
   Begin VB.Menu mnuHelp 
      Caption         =   "&Help"
      Begin VB.Menu mnuAbout 
         Caption         =   "&About the FHDB Processor"
      End
   End
End
Attribute VB_Name = "frmViewer"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'**********************************************************************
'***    United States Marine Corps                                  ***
'***                                                                ***
'***    Nomenclature:   Form "frmViewer" : FHDB FHDB_Processor      ***
'***    Written By:     Dave Joiner                                 ***
'***    Purpose:                                                    ***
'***  ECP-TETS-023                                                  ***
'***    To allow the User to View selected Data from the Database   ***
'***    in a structured manner.                                     ***
'***    Also allows the User access other functions via a Menu.     ***
'***  2.1.0                                                         ***
'***    Sub Form_Load                                               ***
'***    Removed the commented out line in the 657 version, 717 still***
'***    had this line active, ran the program all possible ways did ***
'***    not find any difference with or without this line of code.  ***
'***    Sub Form_Unload                                             ***
'***    added the call to close the data base                       ***
'***    Sub mnuExport_Click                                         ***
'***    Modified to unload export form on selecting cancel          ***
'***    Sub mnuExit_Click                                           ***
'***    Added the call to close the data base                       ***
'**********************************************************************


Option Explicit


Private Sub Form_Load()
'Center Form Inside MDI Form

    If bOnStart = True Then
        'Pop About Screen
        CenterForm frmAbout: frmAbout.Show: frmAbout.Refresh
        Delay 1
        Unload frmAbout         'Remove About Screen
        bOnStart = False
    End If

   Width = 11010                        ' Set width of form in hard code.
   Height = 7530                        ' Set height of form in hard code.
   Left = (MDIMain.Width - Width) / 2   ' Center form horizontally.
   Top = (MDIMain.Height - Height) / 2  ' Center form vertically.
        
   frmViewer.WindowState = 2
   Call ReturnAll              'Query all records and load into RecordSet
   
End Sub


Private Sub Form_Unload(Cancel As Integer)

    Call CloseDB

End Sub

Private Sub SldRecord_Scroll()
'Scrolls through selected recordset

Dim nRecPosition As Long        'Record Number Displayed
Dim bForward As Boolean         'Switch to indicate direction
Dim bBack As Boolean            'Recordset Curser is moving in
Dim sngResult As Single         'Total Records returned/100
Dim nResult As Long             'Used to calculate every 100th record
Dim bMoving As Boolean          'Flag to indicate Slider is moving
Dim nSliderValue As Long        'The Slider value when moving

    bMoving = False
    bLargeMove = False
    'Change Mouse Pointer to an Hourglass W/Arrow while moving through recordset
    frmViewer.MousePointer = vbArrowHourglass
    nSliderValue = SldRecord.Value
    'Iterates through recordset until selected record is found
    Do Until nRecPosition = SldRecord.Value
        Call StatusMsg("Scrolling through records..........         Slider will move until selected record is found!!!")

        nRecPosition = rstFaults!Record_Identifier  'Assign Value to Current Record Number
                
        If nRecPosition < SldRecord.Value Then  'If Current is < Selected
            bMoving = True
            bForward = True         'Set direction switch to Forward
            'Do not update records on Viewer if moving through over 100 Records
            If SldRecord.Value - nRecPosition > 100 Then
                bLargeMove = True
             Else
                bLargeMove = False
            End If
            If bBack = True Then Exit Do    'If direction changes, selected record number invalid
            
            sngResult = nRecPosition / 100
            nResult = CInt(sngResult)
            If nResult * 100 = nRecPosition Then     'Show every 100th record
                rstFaults.Move SldRecord.Value
                bLargeMove = False
            End If
            
            Call NextRecord             'Move curser to Next record in RecordSet
            'If Slider value invalid, move to next record
            If rstFaults!Record_Identifier > SldRecord.Value Then
                'Set Slider Value to match RecNum
                SldRecord.Value = rstFaults!Record_Identifier
                Exit Do
            End If
        ElseIf nRecPosition > SldRecord.Value Then  'If Current is > Selected
            bMoving = True
            bBack = True            'Set direction switch to Backward
            'Do not update records on Viewer if moving through over 100 Records
            If nRecPosition - SldRecord.Value > 100 Then
                bLargeMove = True
            Else
                bLargeMove = True
                bLargeMove = False
            End If
            If bForward = True Then Exit Do     'If direction changes, selected record number invalid
                        
            sngResult = nRecPosition / 100
            nResult = CInt(sngResult)
            If nResult * 100 = nRecPosition Then     'Show every 100th record
               bLargeMove = True
               bLargeMove = False
            End If
                        
            Call PreviousRecord         'Move curser to Previous record in RecordSet
            'If Slider value invalid, move to Previous record
            If rstFaults!Record_Identifier < SldRecord.Value Then
                'Set Slider Value to match RecNum
                SldRecord.Value = rstFaults!Record_Identifier
                Exit Do
            End If
        Else
            Exit Do
        End If
        
        'Safeties to get out of Loop
        If rstFaults.BOF Or rstFaults.EOF Then Exit Do
        If rstFaults.AbsolutePosition = SldRecord.Value Then Exit Do
        If rstFaults.AbsolutePosition = 0 Then Exit Do
        'If moving less than 100 records
        If bLargeMove = False Then
        DoEvents        'Show records as they are iterated through
        End If
    Loop
    
    DoEvents            'Show records
    bForward = False    'Reset Switches
    bBack = False
    'Change Mouse Pointer back to an Arrow (Defalt)
    frmViewer.MousePointer = vbDefault
    Call StatusMsg("Selected Record Displayed.")
    
End Sub


Private Sub txtData_Click(Index As Integer)
'Prevents User from altering Text in the Comments field
'Moves focus to an availible command button
'Uses Next, Previous Ordering

    If cmdNext.Enabled Then
        cmdNext.SetFocus
    ElseIf cmdPrevious.Enabled Then
        cmdPrevious.SetFocus
    End If
    
End Sub


Private Sub txtData_KeyPress(Index As Integer, KeyAscii As Integer)
'Prevents Operator from entering any data in Text Box

    KeyAscii = 1                'Nothing is entered

End Sub


Private Sub txtComments_Click()
'Prevents User from altering Text in the Comments field
'Moves focus to an availible command button
'Uses Next, Previous Ordering
    
    If cmdNext.Enabled Then
        cmdNext.SetFocus
    ElseIf cmdPrevious.Enabled Then
        cmdPrevious.SetFocus
    End If
    
End Sub


Private Sub txtComments_KeyPress(KeyAscii As Integer)
'Prevents Operator from entering any data in Text Box

    KeyAscii = 1                'Nothing is entered

End Sub


'***********************************************************************
'****               Navigation Comand Buttons                       ****
'***********************************************************************

Private Sub cmdFirst_Click()

    Call FirstRecord            'Move curser to First record in RecordSet
    SldRecord.Value = rstFaults!Record_Identifier   'Set Slider Value to match RecNum
    
End Sub


Private Sub cmdLast_Click()

    Call LastRecord             'Move curser to Last record in RecordSet
    SldRecord.Value = rstFaults!Record_Identifier   'Set Slider Value to match RecNum
    
End Sub

Private Sub cmdNext_Click()

        Call NextRecord             'Move curser to Next record in RecordSet
        SldRecord.Value = rstFaults!Record_Identifier   'Set Slider Value to match RecNum
        
End Sub


Private Sub cmdPrevious_Click()

    Call PreviousRecord         'Move curser to Previous record in RecordSet
    SldRecord.Value = rstFaults!Record_Identifier   'Set Slider Value to match RecNum
    
End Sub


'***********************************************************************
'****                      Menu Items                               ****
'***********************************************************************

Private Sub mnuAbout_Click()

    Call ShowAbout              'Show About Form

End Sub

Private Sub mnuComment_Click()

    frmComment.Show             'Launch Comment Form to allow User to enter a Comment
    frmViewer.Hide

End Sub

Private Sub mnuExportRecord_Click()

    Call ExportRecord
    
End Sub

Private Sub mnuExportRecordSet_Click()
Dim iResponse As Integer        'Operator's responce to message box

    'If Record Set is greater than 25 records, Give the Operator a chance to cancel Operation.
    If rstFaults.RecordCount > 25 Then
        iResponse = MsgBox("There are currently " & rstFaults.RecordCount & " records in the record set." & vbCrLf _
                    & "Are you sure sure you want to Export these records to a text file?", vbYesNo + vbDefaultButton2 + vbInformation, "Record Set contains " & rstFaults.RecordCount & " Records")
        If iResponse = vbYes Then
            Call ExportRecordSet
        Else
            Exit Sub
        End If
    Else
        Call ExportRecordSet
    End If
    
End Sub

Private Sub mnuFirst_Click()

    Call FirstRecord            'Move curser to First record in RecordSet
    SldRecord.Value = rstFaults!Record_Identifier   'Set Slider Value to match RecNum
    
End Sub

Private Sub mnuLast_Click()

    Call LastRecord             'Move curser to Last record in RecordSet
    SldRecord.Value = rstFaults!Record_Identifier   'Set Slider Value to match RecNum
    
End Sub

Private Sub mnuLoadAll_Click()

    Call ReturnAll              'Query all records and load into RecordSet

End Sub

Private Sub mnuNext_Click()

    Call NextRecord             'Move curser to Next record in RecordSet
    SldRecord.Value = rstFaults!Record_Identifier   'Set Slider Value to match RecNum
    
End Sub

Private Sub mnuPrevious_Click()

    Call PreviousRecord         'Move curser to Previous record in RecordSet
    SldRecord.Value = rstFaults!Record_Identifier   'Set Slider Value to match RecNum
    
End Sub

Private Sub mnuSpecific_Click()

    frmQuery.Show               'Launch Search Form to allow the User to Search
                                'Database for specified criteria
End Sub

    
Private Sub mnuExport_Click()

    gResponse = vbOK
    bExport = True              'Set the Export Operation Flag
    frmExport.Show              'Launches the Export Data Form
    If gResponse = vbCancel Then
        Unload frmExport
    End If
    
End Sub


Private Sub mnuImport_Click()

    bTerminateImporter = False
    Call Import
    
End Sub
  

Private Sub mnuExit_Click()

    Call CloseDB
    End                         'Quit Application

End Sub


'***********************************************************************
'****                   Status Line Messages                        ****
'***********************************************************************

Private Sub mnuFile_Click()
    Call StatusMsg("Export a record or records as a text file.")
End Sub

Private Sub mnuOperation_Click()
     Call StatusMsg("Chose another Operation or Quit.")
End Sub

Private Sub mnuSearch_Click()
    nBookMark = Val(txtData(0).Text)            'Bookmark Current Record Number
    Call StatusMsg("Select the type of Search to Query the Database.")
End Sub

Private Sub mnuNavigation_Click()
    Call StatusMsg("Navigate through the Records.")
End Sub

Private Sub mnuHelp_Click()
    Call StatusMsg("FHDB Help.")
End Sub


Private Sub txtData_MouseMove(Index As Integer, Button As Integer, Shift As Integer, x As Single, Y As Single)
'Keep Mouse Pointer an Arrow, Do Not change over Text Box
    txtData(Index).MousePointer = 1
    Call StatusMsg("Record Display Area.")
End Sub

Private Sub txtComments_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
'Keep Mouse Pointer an Arrow, Do Not change over Text Box
    txtComments.MousePointer = 1
    Call StatusMsg("Record Display Area.")
End Sub

Private Sub cmdFirst_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Displays the First Record from the search results.")
End Sub

Private Sub cmdLast_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Displays the Last Record from the search results.")
End Sub

Private Sub cmdNext_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Displays the Next Record from the search results.")
End Sub

Private Sub cmdPrevious_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Displays the Previous Record from the search results.")
End Sub

Private Sub SldRecord_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Call StatusMsg("Scroll through records.")
End Sub


Private Sub Form_MouseMove(Button As Integer, Shift As Integer, x As Single, Y As Single)
'If Status Message is Empty, Display generic message
    
    If sMessageString = "" Then             'If the record return string is empty
        Call StatusMsg("Database Viewer.")  'Display generic message.
    Else
        Call StatusMsg(sMessageString)      'Else Display record return String
    End If
    
End Sub

