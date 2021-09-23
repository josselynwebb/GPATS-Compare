VERSION 5.00
Object = "{0BA686C6-F7D3-101A-993E-0000C0EF6F5E}#1.0#0"; "THREED32.OCX"
Object = "{CD4409E1-BF00-11D3-BE16-0090270D7696}#4.52#0"; "ViewDirX.ocx"
Begin VB.Form frmImage 
   BackColor       =   &H00C0C0C0&
   Caption         =   "Form1"
   ClientHeight    =   8385
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   11730
   Icon            =   "frmImage.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   ScaleHeight     =   8385
   ScaleWidth      =   11730
   StartUpPosition =   3  'Windows Default
   WindowState     =   2  'Maximized
   Begin VB.Frame ImageNavigationlPanel 
      BackColor       =   &H00C0C0C0&
      Caption         =   "Navigation"
      ClipControls    =   0   'False
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1215
      Left            =   120
      TabIndex        =   1
      Top             =   7080
      Width           =   3255
      Begin Threed.SSCommand PrintButton 
         Height          =   435
         Left            =   1680
         TabIndex        =   5
         ToolTipText     =   "Print Current Image"
         Top             =   720
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
      Begin Threed.SSCommand Quit 
         Height          =   435
         Left            =   120
         TabIndex        =   6
         ToolTipText     =   "Shut down Image Viewer"
         Top             =   720
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
      Begin Threed.SSCommand NPage 
         Height          =   435
         Left            =   1680
         TabIndex        =   2
         ToolTipText     =   "Next Image Page"
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "&Next Page"
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
      Begin Threed.SSCommand PPage 
         Height          =   435
         Left            =   120
         TabIndex        =   3
         ToolTipText     =   "Previous Image Page"
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "P&rev Page"
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
         MouseIcon       =   "frmImage.frx":030A
         Picture         =   "frmImage.frx":0326
      End
   End
   Begin VB.Frame ImageControlPanel 
      BackColor       =   &H00C0C0C0&
      Caption         =   "Image Control Options"
      ClipControls    =   0   'False
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1215
      Left            =   3480
      TabIndex        =   8
      Top             =   7080
      Width           =   8175
      Begin Threed.SSCommand MouseControl 
         Height          =   900
         Index           =   0
         Left            =   120
         TabIndex        =   10
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   1587
         _StockProps     =   78
         Caption         =   "P&an"
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
         Picture         =   "frmImage.frx":0342
      End
      Begin Threed.SSCommand SSCommand3 
         Height          =   435
         Left            =   8760
         TabIndex        =   9
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   767
         _StockProps     =   78
         Caption         =   "Zoom Out 50%"
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
      Begin Threed.SSCommand ResetView 
         Height          =   900
         Left            =   4800
         TabIndex        =   0
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   1587
         _StockProps     =   78
         Caption         =   "Reset &View"
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
         Picture         =   "frmImage.frx":065C
      End
      Begin Threed.SSCommand MouseControl 
         Height          =   900
         Index           =   2
         Left            =   3240
         TabIndex        =   11
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   1587
         _StockProps     =   78
         Caption         =   "&Magnify"
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
         Picture         =   "frmImage.frx":0976
      End
      Begin Threed.SSCommand MouseControl 
         Height          =   900
         Index           =   1
         Left            =   1680
         TabIndex        =   12
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   1587
         _StockProps     =   78
         Caption         =   "&Zoom"
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
         Picture         =   "frmImage.frx":0C90
      End
      Begin Threed.SSCommand ZoomOut50 
         Height          =   900
         Left            =   6360
         TabIndex        =   13
         Top             =   240
         Visible         =   0   'False
         Width           =   1500
         _Version        =   65536
         _ExtentX        =   2646
         _ExtentY        =   1587
         _StockProps     =   78
         Caption         =   "Zoom &Out 50%"
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
         AutoSize        =   1
         MouseIcon       =   "frmImage.frx":0FAA
         Picture         =   "frmImage.frx":12C4
      End
   End
   Begin VIEWDIRXLib.ViewDirX ViewDirX1 
      Height          =   6615
      Left            =   240
      TabIndex        =   7
      Top             =   240
      Width           =   11175
      _Version        =   262149
      _ExtentX        =   19711
      _ExtentY        =   11668
      _StockProps     =   33
      BackColor       =   16777152
      BorderStyle     =   4
      AnnotationMode  =   10075
      BorderStyle     =   4
      BitonalScaleToGray=   10003
      ImageQuality    =   10000
      ColorReduction  =   10008
      MagnifierHeight =   200
      MagnifierWidth  =   400
      Mirror          =   10003
      ResetImage      =   10041
      Rotation        =   10044
      WindowPaletteType=   10048
      LoadImage       =   ""
   End
   Begin Threed.SSPanel SSPanel1 
      Height          =   6855
      Left            =   120
      TabIndex        =   4
      Top             =   120
      Width           =   11490
      _Version        =   65536
      _ExtentX        =   20267
      _ExtentY        =   12091
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
   End
End
Attribute VB_Name = "frmImage"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
DefInt A-Z

Private Sub MainMenuButton_Click()

End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)

    Select Case KeyCode
    
        Case vbKeyQ
            If Shift = 2 Then Quit_Click
            
        Case vbKeyR
            If Shift = 2 Then PPage_Click
            
        Case vbKeyN
            If Shift = 2 Then NPage_Click
            
        Case vbKeyP
            If Shift = 2 Then PrintButton_Click
            
        Case vbKeyA
            If Shift = 2 Then MouseControl_Click (0)
            
        Case vbKeyZ
            If Shift = 2 Then MouseControl_Click (1)
            
        Case vbKeyM
            If Shift = 2 Then MouseControl_Click (2)
            
        Case vbKeyV
            If Shift = 2 Then ResetView_Click
            
        Case vbKeyO
            If Shift = 2 Then ZoomOut50_Click
            
    End Select
    
End Sub

Private Sub Form_Load()

    ResetView_Click
    
End Sub

Private Sub Form_Resize()

    If WindowState = MINIMIZED Then Exit Sub

    SSPanel1.Move 120, 120, Limit(Width - 360, 0), Limit(Height - 2040, 0)
    ViewDirX1.Move 240, 240, Limit(Width - 675, 0), Limit(Height - 2280, 0)
    ImageNavigationlPanel.Move 120, SSPanel1.Height + 225, 3255, 1215
    ImageControlPanel.Move 3480, SSPanel1.Height + 225, 8175, 1215
    
End Sub

Private Sub MouseControl_Click(index As Integer)
    
    Select Case index
    
        Case PANNING
            frmImage.ViewDirX1.LeftMouseTool = VX_HANDPAN
        
        Case ZOOMING
            frmImage.ViewDirX1.LeftMouseTool = VX_MARKZOOM
        
        Case MAGNIFYING
            frmImage.ViewDirX1.LeftMouseTool = VX_MAGNIFIER
            
    End Select
    
End Sub

Private Sub NPage_Click()

    Dim sfunction As String
    
    frmImage.PPage.Enabled = True
    sfunction = UCase(Left(frmImage.Caption, 8))
    
    Select Case sfunction
    
        Case "SCHEMATI" 'Schematic View
            SchemPageNum% = SchemPageNum% + 1
            frmMain.cboSchematic.ListIndex = SchemPageNum - 1
            DoMenuChoice (VIEW_SCHEMATIC)
        
        Case "ASSEMBLY" 'Assembly View
            AssyPageNum% = AssyPageNum% + 1
            frmMain.cboAssembly.ListIndex = AssyPageNum% - 1
            DoMenuChoice (VIEW_ASSEMBLY)

        Case "PARTS LI" 'Parts List View
            PartListPageNum% = PartListPageNum% + 1
            frmMain.cboPartsList.ListIndex = PartListPageNum - 1
            DoMenuChoice (VIEW_PARTSLIST)
            
        Case "ITA SCHE"
            IDSchemPageNum% = IDSchemPageNum% + 1
            frmMain.cboITAWiring.ListIndex = IDSchemPageNum - 1
            DoMenuChoice (VIEW_ID_SCHEMATIC)

        Case "ITA ASSE"
            IDAssyPageNum% = IDAssyPageNum% + 1
            If IDAssyPageNum% = 1 Then PPage.Enabled = False
            frmMain.cboITAAssy.ListIndex = IDAssyPageNum - 1
            DoMenuChoice (VIEW_ID_ASSEMBLY)
     
        Case "ITA PART" 'ID Parts List View
            IDPartListPageNum% = IDPartListPageNum% + 1
            frmMain.cboITAPartsList.ListIndex = IDPartListPageNum - 1
            DoMenuChoice (VIEW_ID_PARTSLIST)
            
    End Select

End Sub

Private Sub PPage_Click()

    Dim sfunction As String
    
    frmImage.NPage.Enabled = True
    
    Select Case MouseAction
    
        Case SCROLLING
            'PageUp Display.PictureWindow
            
        Case Else
            sfunction = UCase(Left(frmImage.Caption, 8))
            
            Select Case sfunction
            
                Case "SCHEMATI" 'Schematic View
                    SchemPageNum% = SchemPageNum% - 1
                    If SchemPageNum% = 1 Then PPage.Enabled = False
                    frmMain.cboSchematic.ListIndex = SchemPageNum - 1
                    DoMenuChoice (VIEW_SCHEMATIC)
                
                Case "ASSEMBLY" 'Assembly View
                    AssyPageNum% = AssyPageNum% - 1
                    If AssyPageNum% = 1 Then PPage.Enabled = False
                    frmMain.cboAssembly.ListIndex = AssyPageNum% - 1
                    DoMenuChoice (VIEW_ASSEMBLY)
                    
                Case "PARTS LI" 'Parts List View
                    PartListPageNum% = PartListPageNum% - 1
                    frmMain.cboPartsList.ListIndex = PartListPageNum% - 1
                    If PartListPageNum% = 1 Then PPage.Enabled = False
                    DoMenuChoice (VIEW_PARTSLIST)
                    
                Case "ITA SCHE"
                    IDSchemPageNum% = IDSchemPageNum% - 1
                    frmMain.cboITAWiring.ListIndex = IDSchemPageNum% - 1
                    If IDSchemPageNum% = 1 Then PPage.Enabled = False
                    DoMenuChoice (VIEW_ID_SCHEMATIC)

                Case "ITA ASSE"
                    IDAssyPageNum% = IDAssyPageNum% - 1
                    frmMain.cboITAAssy.ListIndex = IDAssyPageNum - 1
                    If IDAssyPageNum% = 1 Then PPage.Enabled = False
                    DoMenuChoice (VIEW_ID_ASSEMBLY)
            
                Case "ITA PART" 'ID Parts List View
                    IDPartListPageNum% = IDPartListPageNum% - 1
                    frmMain.cboITAPartsList.ListIndex = IDPartListPageNum% - 1
                    If IDPartListPageNum% = 1 Then PPage.Enabled = False
                    DoMenuChoice (VIEW_ID_PARTSLIST)
                    
        End Select
    End Select
    
End Sub

Private Sub PrintButton_Click()

    frmImage.ViewDirX1.PrintCurrentSelection
    
End Sub

Private Sub Quit_Click()

    Unload frmImage
    
End Sub

Private Sub ResetView_Click()

    LoadImage
    
End Sub

Private Sub SSCommand4_Click()

End Sub

Private Sub ZoomOut50_Click()

    frmImage.ViewDirX1.ZoomPercentage = 50
    frmImage.ViewDirX1.ZoomOut

End Sub
