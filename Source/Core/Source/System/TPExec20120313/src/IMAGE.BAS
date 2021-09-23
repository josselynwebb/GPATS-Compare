Attribute VB_Name = "IMAGE"
Option Explicit
DefInt A-Z

' *** IMAGE VIEWER DECLARATIONS ***

Global TheImage&
Global MagnifyImage&

Type RECT
        Left As Integer
        Top As Integer
        Right As Integer
        bottom As Integer
End Type

Global Const CUSTOMCLIP = 1
Global Const STANDARDCLIP = 2
Type RATIO
    rto As Long
    rfrom As Long
End Type

Global zoomer As RATIO

Type PAINTSTRUCT
        hDC As Integer
        fErase As Integer
        rcpaint As RECT
        fRestore As Integer
        fIncUpdate As Integer
        rgbReserved As String * 16
End Type

Declare Function NewCopyToClipboard& Lib "misc.dll" (ByVal hwnd As Integer, ByVal x As Long, ByVal Y As Long, ByVal nWidth As Long, ByVal nHeight As Long)
Declare Function Copytoclipboardratio& Lib "misc.dll" (ByVal hwnd As Integer, ByVal image As Long, ByVal toratio As Long, ByVal fromratio As Long, ByVal realx As Long, ByVal realy As Long, ByVal realwidth As Long, ByVal realheight As Long)
Declare Function AliasingOn& Lib "misc.dll" (ByVal hwnd As Integer, ByVal image As Long)




Type PointAPI
        x As Integer
        Y As Integer
End Type

Type BITMAP '14 bytes
        bmType As Integer
        bmWidth As Integer
        bmHeight As Integer
        bmWidthBytes As Integer
        bmPlanes As String * 1
        bmBitsPixel As String * 1
        bmBits As Long
End Type

' --------------------------------------------------------------------------
'   Kernel Section
' --------------------------------------------------------------------------


'  GDI Escapes
Global Const NEWFRAME = 1
Global Const ABORTDOC = 2
Global Const NEXTBAND = 3
Global Const SETCOLORTABLE = 4
Global Const GETCOLORTABLE = 5
Global Const FLUSHOUTPUT = 6
Global Const DRAFTMODE = 7
Global Const QUERYESCSUPPORT = 8
Global Const SETABORTPROC = 9
Global Const STARTDOC = 10
Global Const ENDDOCAPI = 11
Global Const GETPHYSPAGESIZE = 12
Global Const GETPRINTINGOFFSET = 13
Global Const GETSCALINGFACTOR = 14
Global Const MFCOMMENT = 15
Global Const GETPENWIDTH = 16
Global Const SETCOPYCOUNT = 17
Global Const SELECTPAPERSOURCE = 18
Global Const DEVICEDATA = 19
Global Const PASSTHROUGH = 19
Global Const GETTECHNOLGY = 20
Global Const GETTECHNOLOGY = 20
Global Const SETENDCAP = 21
Global Const SETLINEJOIN = 22
Global Const SETMITERLIMIT = 23
Global Const BANDINFO = 24
Global Const DRAWPATTERNRECT = 25
Global Const GETVECTORPENSIZE = 26
Global Const GETVECTORBRUSHSIZE = 27
Global Const ENABLEDUPLEX = 28
Global Const GETSETPAPERBINS = 29
Global Const GETSETPRINTORIENT = 30
Global Const ENUMPAPERBINS = 31
Global Const SETDIBSCALING = 32
Global Const EPSPRINTING = 33
Global Const ENUMPAPERMETRICS = 34
Global Const GETSETPAPERMETRICS = 35
Global Const POSTSCRIPT_DATA = 37
Global Const POSTSCRIPT_IGNORE = 38
Global Const GETEXTENDEDTEXTMETRICS = 256
Global Const GETEXTENTTABLE = 257
Global Const GETPAIRKERNTABLE = 258
Global Const GETTRACKKERNTABLE = 259
Global Const EXTTEXTOUT = 512
Global Const ENABLERELATIVEWIDTHS = 768
Global Const ENABLEPAIRKERNING = 769
Global Const SETKERNTRACK = 770
Global Const SETALLJUSTVALUES = 771
Global Const SETCHARSET = 772

'  Device Parameters for GetDeviceCaps()
Global Const DRIVERVERSION = 0  '  Device driver version
Global Const TECHNOLOGY = 2     '  Device classification
Global Const HORZSIZE = 4       '  Horizontal size in millimeters
Global Const VERTSIZE = 6       '  Vertical size in millimeters
Global Const HORZRES = 8        '  Horizontal width in pixels
Global Const VERTRES = 10       '  Vertical width in pixels
Global Const BITSPIXEL = 12     '  Number of bits per pixel
Global Const PLANES = 14        '  Number of planes
Global Const NUMBRUSHES = 16    '  Number of brushes the device has
Global Const NUMPENS = 18       '  Number of pens the device has
Global Const NUMMARKERS = 20    '  Number of markers the device has
Global Const NUMFONTS = 22      '  Number of fonts the device has
Global Const NUMCOLORS = 24     '  Number of colors the device supports
Global Const PDEVICESIZE = 26   '  Size required for device descriptor
Global Const RASTERCAPS = 38    '  Bitblt capabilities

Global Const LOGPIXELSX = 88    '  Logical pixels/inch in X
Global Const LOGPIXELSY = 90    '  Logical pixels/inch in Y
Global Const SIZEPALETTE = 104  '  Number of entries in physical palette


'  Device Capability Masks:

'  Raster Capabilities
Global Const RC_BITBLT = 1      '  Can do standard BLT.
Global Const RC_BANDING = 2     '  Device requires banding support
Global Const RC_SCALING = 4     '  Device requires scaling support
Global Const RC_BITMAP64 = 8    '  Device can support >64K bitmap

'  palette entry flags
Global Const PC_RESERVED = &H1  '  palette index used for animation
Global Const PC_EXPLICIT = &H2  '  palette index is explicit to device
Global Const PC_NOCOLLAPSE = &H4        '  do not match color to system palette

'  DIB color table identifiers
Global Const DIB_RGB_COLORS = 0 '  color table in RGBTriples
Global Const DIB_PAL_COLORS = 1 '  color table in palette indices


Declare Function GetWindowDC Lib "GDI" (ByVal hwnd As Integer) As Integer
Declare Function GetDC Lib "User" (ByVal hwnd As Integer) As Integer
Declare Function ReleaseDC Lib "GDI" (ByVal hwnd As Integer, ByVal hDC As Integer) As Integer
Declare Function CreateDC Lib "GDI" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByVal lpInitData As String) As Integer
Declare Function CreateCompatibleDC Lib "GDI" (ByVal hDC As Integer) As Integer
Declare Function DeleteDC Lib "GDI" (ByVal hDC As Integer) As Integer

Declare Function StretchBlt% Lib "GDI" (ByVal hDC%, ByVal x%, ByVal Y%, ByVal nWidth%, ByVal nHeight%, ByVal hSrcDC%, ByVal XSrc%, ByVal YSrc%, ByVal nSrcWidth%, ByVal nSrcHeight%, ByVal dwRop&)

Declare Function CreateCompatibleBitmap Lib "GDI" (ByVal hDC As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer

Declare Function GetDeviceCaps Lib "GDI" (ByVal hDC As Integer, ByVal nIndex As Integer) As Integer

Declare Sub ClipCursor Lib "User" (lpRect As Any)
Declare Function SetCapture Lib "User" (ByVal hwnd As Integer) As Integer
Declare Sub ClientToScreen Lib "User" (ByVal hwnd As Integer, lpPoint As PointAPI)
Declare Function Escape Lib "GDI" (ByVal hDC As Integer, ByVal nEscape As Integer, ByVal nCount As Integer, lplnData As Any, lpOutData As Any) As Integer
Declare Function GetSystemMetrics Lib "User" (ByVal nIndex As Integer) As Integer

Global Const ALT_MASK = 4
Global Const LEFT_BUTTON = 1
Global Const RIGHT_BUTTON = 2

Declare Function IsRectEmpty Lib "User" (lpRect As RECT) As Integer
Declare Function OpenClipboard Lib "User" (ByVal hwnd As Integer) As Integer
Declare Function EmptyClipboard Lib "User" () As Integer
Declare Function SetClipboardData Lib "User" (ByVal wFormat As Integer, ByVal hMem As Integer) As Integer
Declare Function SelectObject Lib "GDI" (ByVal hDC As Integer, ByVal hObject As Integer) As Integer
Declare Function CloseClipboard Lib "User" () As Integer
Declare Function DeleteObject Lib "GDI" (ByVal hObject As Integer) As Integer
Declare Sub GetClientRect Lib "User" (ByVal hwnd As Integer, lpRect As RECT)
Declare Function BeginPaint Lib "User" (ByVal hwnd As Integer, lpPaint As PAINTSTRUCT) As Integer
Declare Sub EndPaint Lib "User" (ByVal hwnd As Integer, lpPaint As PAINTSTRUCT)

Declare Function LoadCursor Lib "User" (ByVal hInstance As Integer, lpCursorName As Any) As Integer
Declare Sub SetCursor Lib "User" (ByVal hCursor As Integer)


Function LoadImage&()
    Dim ViewDirectorErrorString$
    Dim iNumPagesInFile%
    
    On Error Resume Next   ' Defer error trapping
    If Len(sImageFile) > 0 Then
        frmImage.ViewDirX1.UnloadImage
        frmImage.ViewDirX1.AutoRefresh = False
        frmImage.ViewDirX1.LoadImage (sImageFile)
        If frmImage.ViewDirX1.LastErrorString = "Unable to open file" Then
            MsgBox "Could not load specified image", 48, "Image Error"
            frmImage.ViewDirX1.Visible = False
            TheImage& = 0
            LoadImage& = TheImage&
            Exit Function
        Else
            TheImage& = 1
            LoadImage& = TheImage&
        End If
        'Load Last page of image if multi page image
        iNumPagesInFile% = frmImage.ViewDirX1.TotalPages
        If iNumPagesInFile% > 1 Then
            frmImage.ViewDirX1.GotoPage VX_LASTPAGE
        End If
        'Rotate Image if required
        If frmImage.ViewDirX1.ImagePixelHeight > frmImage.ViewDirX1.ImagePixelWidth Then
            frmImage.ViewDirX1.Rotation = VX_270DEGREE
        End If
        frmImage.ViewDirX1.ZoomPercentage = 50
        frmImage.ViewDirX1.BitonalScaleToGray = VX_4SHADES
        frmImage.ViewDirX1.ImageQuality = VX_BEST
        frmImage.ViewDirX1.LeftMouseTool = VX_MARKZOOM
        frmImage.ViewDirX1.RightMouseTool = VX_NONE
        ' turn AutoRefresh back on so image is drawn
        frmImage.ViewDirX1.AutoRefresh = True
    End If
End Function


