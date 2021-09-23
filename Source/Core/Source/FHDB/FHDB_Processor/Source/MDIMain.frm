VERSION 5.00
Begin VB.MDIForm MDIMain 
   BackColor       =   &H00E0E0E0&
   Caption         =   "FHDB Processor"
   ClientHeight    =   7305
   ClientLeft      =   165
   ClientTop       =   450
   ClientWidth     =   10890
   Icon            =   "MDIMain.frx":0000
   LinkTopic       =   "MDIForm1"
   StartUpPosition =   2  'CenterScreen
   Begin VB.Timer Timer1 
      Interval        =   10
      Left            =   9480
      Top             =   0
   End
   Begin VB.Menu mnuFile 
      Caption         =   "&Operation"
      Begin VB.Menu mnuExit 
         Caption         =   "E&xit"
      End
   End
   Begin VB.Menu mnuHelp 
      Caption         =   "&Help"
      Begin VB.Menu mnuAbout 
         Caption         =   "&About the FHDB Processor"
      End
   End
End
Attribute VB_Name = "MDIMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'**********************************************************************
'***    United States Marine Corps                                  ***
'***                                                                ***
'***    Nomenclature:   Form "MDIMain" : FHDB FHDB_Processor        ***
'***    Written By:     Dave Joiner                                 ***
'***    Purpose:                                                    ***
'***  ECP-TETS-023                                                  ***
'***    The Multiple Document Interface Form that serves as a       ***
'***    holder for the different Function Forms.                    ***
'***    Timer routine to check form size and alignment, to keep     ***
'***    Background Synchronized with the MDIMain form.              ***
'***  2.1.0                                                         ***
'***    Sub Timer1_timer                                            ***
'***    Removed the commented out lines                             ***
'**********************************************************************


Option Explicit


Private Sub MDIForm_Load()

    Width = 11010        ' Set width of form.
    Height = 7400       ' Set height of form.
    Left = (Screen.Width - Width) / 2   ' Center form horizontally.
    Top = (Screen.Height - Height) / 2   ' Center form vertically.
    frmBackground.Show  'Show Background

End Sub


Private Sub MDIForm_Resize()

    Call ResizeMDI
    Call ResizeCenterBackground
    
End Sub


Private Sub MDIForm_Unload(Cancel As Integer)
    End
End Sub


Private Sub mnuAbout_Click()

    Call ShowAbout              'Show About Form

End Sub


Private Sub mnuExit_Click()

    Unload MDIMain

End Sub


Sub ResizeCenterBackground()

    frmBackground.Width = MDIMain.Width        '11010        ' Set width of form.
    frmBackground.Height = (MDIMain.Height + 840)    '8650  ' Set height of form.
    frmBackground.Left = MDIMain.Left   '(Screen.Width - Width) / 2   ' Center form horizontally.
    frmBackground.Top = MDIMain.Top     '(Screen.Height - Height) / 2   ' Center form vertically.
    frmBackground.imgLogo.Top = (frmBackground.Height - 850)

End Sub


Private Sub Timer1_Timer()

    If WindowState = 2 Then
        iMax = iMax + 1
        If iMax = 2 Then
            WindowState = 0
            Width = 11010        ' Set width of form.
            Height = 7400       ' Set height of form.
            Left = (Screen.Width - Width) / 2   ' Center form horizontally.
            Top = (Screen.Height - Height) / 2   ' Center form vertically.
        Else
            WindowState = 0
            MDIMain.Width = Screen.Width
            MDIMain.Height = (Screen.Height - 1250)
            MDIMain.Top = 0
            MDIMain.Left = 0
        End If
            If iMax = 2 Then
                iMax = 0
            End If
    End If
    Call ResizeCenterBackground
    Call ResizeTextBox
    
End Sub


Sub ResizeMDI()

    On Error Resume Next

    If MDIMain.Width < 11010 Then   'Restrict Form's minimum Width
        MDIMain.Width = 11010
    End If
    If MDIMain.Height < 7400 Then   'Restrict Form's minimum Height
        MDIMain.Height = 7400
    End If                          'Restrict Form's maximum Height
    If MDIMain.Height > (Screen.Height - 940) Then
        MDIMain.Height = (Screen.Height - 940)
    End If
    
End Sub


Sub ResizeTextBox()

    If MDIMain.Width > 11010 Then
        frmViewer.ssfraSystem.Width = 10455 + (MDIMain.Width - 11010)
        frmViewer.ssfraMeasurement.Width = 10455 + (MDIMain.Width - 11010)
        frmViewer.ssfraComment.Width = 10455 + (MDIMain.Width - 11010)
        frmViewer.txtData(10).Width = 3720 + (MDIMain.Width - 11010)
        frmViewer.txtComments.Width = 10255 + (MDIMain.Width - 11010)
    Else
        frmViewer.ssfraSystem.Width = 10455
        frmViewer.ssfraMeasurement.Width = 10455
        frmViewer.ssfraComment.Width = 10455
        frmViewer.txtData(10).Width = 3720
        frmViewer.txtComments.Width = 10255
    End If
        
End Sub


Private Sub mnuHelp_Click()
    Call StatusMsg("FHDB Help.")
End Sub
