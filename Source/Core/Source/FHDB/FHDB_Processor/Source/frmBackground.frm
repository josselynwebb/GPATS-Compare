VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Object = "{D940E4E4-6079-11CE-88CB-0020AF6845F6}#1.6#0"; "cwui.ocx"
Begin VB.Form frmBackground 
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   8520
   ClientLeft      =   45
   ClientTop       =   45
   ClientWidth     =   10890
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   NegotiateMenus  =   0   'False
   ScaleHeight     =   8520
   ScaleWidth      =   10890
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin CWUIControlsLib.CWSlide PBarFile 
      Height          =   255
      Left            =   4200
      TabIndex        =   1
      Top             =   7560
      Visible         =   0   'False
      Width           =   6375
      _Version        =   393218
      _ExtentX        =   11245
      _ExtentY        =   450
      _StockProps     =   68
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Reset_0         =   0   'False
      CompatibleVers_0=   393218
      Slider_0        =   1
      ClassName_1     =   "CCWSlider"
      opts_1          =   2110
      C[0]_1          =   16777215
      BGImg_1         =   2
      ClassName_2     =   "CCWDrawObj"
      opts_2          =   60
      C[0]_2          =   65280
      Image_2         =   3
      ClassName_3     =   "CCWPictImage"
      opts_3          =   1280
      Rows_3          =   1
      Cols_3          =   1
      Pict_3          =   286
      F_3             =   65280
      B_3             =   -2147483633
      ColorReplaceWith_3=   8421504
      ColorReplace_3  =   8421504
      Tolerance_3     =   2
      Animator_2      =   0
      Blinker_2       =   0
      BFImg_1         =   4
      ClassName_4     =   "CCWDrawObj"
      opts_4          =   62
      Image_4         =   5
      ClassName_5     =   "CCWPictImage"
      opts_5          =   1280
      Rows_5          =   1
      Cols_5          =   1
      Pict_5          =   286
      F_5             =   -2147483633
      B_5             =   -2147483633
      ColorReplaceWith_5=   8421504
      ColorReplace_5  =   8421504
      Tolerance_5     =   2
      Animator_4      =   0
      Blinker_4       =   0
      style_1         =   7
      Label_1         =   6
      ClassName_6     =   "CCWDrawObj"
      opts_6          =   60
      C[0]_6          =   -2147483640
      Image_6         =   7
      ClassName_7     =   "CCWTextImage"
      style_7         =   16777217
      font_7          =   0
      Animator_6      =   0
      Blinker_6       =   0
      Border_1        =   8
      ClassName_8     =   "CCWDrawObj"
      opts_8          =   62
      C[0]_8          =   -2147483632
      Image_8         =   9
      ClassName_9     =   "CCWPictImage"
      opts_9          =   1280
      Rows_9          =   1
      Cols_9          =   1
      Pict_9          =   25
      F_9             =   -2147483632
      B_9             =   -2147483633
      ColorReplaceWith_9=   8421504
      ColorReplace_9  =   8421504
      Tolerance_9     =   2
      Animator_8      =   0
      Blinker_8       =   0
      FillBound_1     =   10
      ClassName_10    =   "CCWGuiObject"
      opts_10         =   60
      FillTok_1       =   11
      ClassName_11    =   "CCWGuiObject"
      opts_11         =   62
      Axis_1          =   12
      ClassName_12    =   "CCWAxis"
      opts_12         =   575
      Name_12         =   "Axis"
      Orientation_12  =   512
      format_12       =   13
      ClassName_13    =   "CCWFormat"
      Scale_12        =   14
      ClassName_14    =   "CCWScale"
      opts_14         =   90112
      rMax_14         =   424
      dMax_14         =   100
      discInterval_14 =   1
      Radial_12       =   0
      Enum_12         =   15
      ClassName_15    =   "CCWEnum"
      Editor_15       =   16
      ClassName_16    =   "CCWEnumArrayEditor"
      Owner_16        =   12
      Font_12         =   0
      tickopts_12     =   2706
      major_12        =   10
      minor_12        =   5
      Caption_12      =   17
      ClassName_17    =   "CCWDrawObj"
      opts_17         =   62
      C[0]_17         =   -2147483640
      Image_17        =   18
      ClassName_18    =   "CCWTextImage"
      style_18        =   65629440
      font_18         =   0
      Animator_17     =   0
      Blinker_17      =   0
      DrawLst_1       =   19
      ClassName_19    =   "CDrawList"
      count_19        =   10
      list[10]_19     =   8
      list[9]_19      =   20
      ClassName_20    =   "CCWThumb"
      opts_20         =   61
      Name_20         =   "Pointer-1"
      C[0]_20         =   0
      C[1]_20         =   0
      C[2]_20         =   65535
      Image_20        =   21
      ClassName_21    =   "CCWPictImage"
      opts_21         =   1280
      Rows_21         =   1
      Cols_21         =   1
      Pict_21         =   218
      F_21            =   0
      B_21            =   0
      ColorReplaceWith_21=   8421504
      ColorReplace_21 =   8421504
      Tolerance_21    =   2
      Animator_20     =   0
      Blinker_20      =   0
      style_20        =   1
      mode_20         =   1
      FillStyle_20    =   1
      Fill_20         =   22
      ClassName_22    =   "CCWDrawObj"
      opts_22         =   62
      Image_22        =   23
      ClassName_23    =   "CCWPictImage"
      opts_23         =   1280
      Rows_23         =   1
      Cols_23         =   1
      Pict_23         =   286
      F_23            =   -2147483633
      B_23            =   -2147483633
      ColorReplaceWith_23=   8421504
      ColorReplace_23 =   8421504
      Tolerance_23    =   2
      Animator_22     =   0
      Blinker_22      =   0
      list[8]_19      =   12
      list[7]_19      =   6
      list[6]_19      =   11
      list[5]_19      =   4
      list[4]_19      =   24
      ClassName_24    =   "CCWDrawObj"
      opts_24         =   62
      C[0]_24         =   11320183
      C[1]_24         =   11320183
      Image_24        =   25
      ClassName_25    =   "CCWPictImage"
      opts_25         =   1280
      Rows_25         =   1
      Cols_25         =   1
      Pict_25         =   2
      F_25            =   11320183
      B_25            =   11320183
      ColorReplaceWith_25=   8421504
      ColorReplace_25 =   8421504
      Tolerance_25    =   2
      Animator_24     =   0
      Blinker_24      =   0
      list[3]_19      =   26
      ClassName_26    =   "CCWDrawObj"
      opts_26         =   60
      C[0]_26         =   11320183
      C[1]_26         =   11320183
      Image_26        =   27
      ClassName_27    =   "CCWPictImage"
      opts_27         =   1280
      Rows_27         =   1
      Cols_27         =   1
      Pict_27         =   93
      F_27            =   11320183
      B_27            =   11320183
      ColorReplaceWith_27=   8421504
      ColorReplace_27 =   8421504
      Tolerance_27    =   2
      Animator_26     =   0
      Blinker_26      =   0
      list[2]_19      =   28
      ClassName_28    =   "CCWDrawObj"
      opts_28         =   60
      C[0]_28         =   11320183
      C[1]_28         =   11320183
      Image_28        =   29
      ClassName_29    =   "CCWPictImage"
      opts_29         =   1280
      Rows_29         =   1
      Cols_29         =   1
      Pict_29         =   94
      F_29            =   11320183
      B_29            =   11320183
      ColorReplaceWith_29=   8421504
      ColorReplace_29 =   8421504
      Tolerance_29    =   2
      Animator_28     =   0
      Blinker_28      =   0
      list[1]_19      =   2
      Ptrs_1          =   30
      ClassName_30    =   "CCWPointerArray"
      Array_30        =   1
      Editor_30       =   31
      ClassName_31    =   "CCWPointerArrayEditor"
      Owner_31        =   1
      Array[0]_30     =   20
      Bindings_1      =   32
      ClassName_32    =   "CCWBindingHolderArray"
      Editor_32       =   33
      ClassName_33    =   "CCWBindingHolderArrayEditor"
      Owner_33        =   1
      Stats_1         =   34
      ClassName_34    =   "CCWStats"
      doInc_1         =   28
      doDec_1         =   26
      doFrame_1       =   24
   End
   Begin MSComctlLib.StatusBar stbMain 
      Align           =   2  'Align Bottom
      Height          =   255
      Left            =   0
      TabIndex        =   0
      Top             =   8265
      Width           =   10890
      _ExtentX        =   19209
      _ExtentY        =   450
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   1
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            AutoSize        =   2
         EndProperty
      EndProperty
   End
   Begin VB.Image imgLogo 
      Height          =   495
      Left            =   120
      Picture         =   "frmBackground.frx":0000
      Stretch         =   -1  'True
      Top             =   7600
      Width           =   1815
   End
End
Attribute VB_Name = "frmBackground"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'**********************************************************************
'***    United States Marine Corps                                  ***
'***                                                                ***
'***    Nomenclature:   Form "frmBackground" : FHDB_Processor       ***
'***    Written By:     Dave Joiner                                 ***
'***    Purpose:                                                    ***
'***  ECP-TETS-023                                                  ***
'***    Provides a Background form to hold the TETS Logo and        ***
'***    Status Bar for the FHDB_Processor.                          ***
'**********************************************************************


Option Explicit

Private Sub Form_Click()

    MDIMain.SetFocus    'Don't let the Background get focus
    
End Sub


Private Sub Form_GotFocus()

    MDIMain.ZOrder      'Keep Background behind Main Form

End Sub

Private Sub Form_Load()

    Width = MDIMain.Width        '11010        ' Set width of form.
    Height = (MDIMain.Height + 940)    '8650  ' Set height of form.
    Left = MDIMain.Left '   (Screen.Width - Width) / 2   ' Center form horizontally.
    Top = MDIMain.Top     '(Screen.Height - Height) / 2   ' Center form vertically.

End Sub

