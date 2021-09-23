Module IMAGE
    ' *** IMAGE VIEWER DECLARATIONS ***

    Public TheImage As Integer
    Public MagnifyImage As Integer

    Structure RECT
        Dim Left As Short
        Dim Top As Short
        Dim Right As Short
        Dim bottom As Short
    End Structure

    Public Const CUSTOMCLIP As Short = 1
    Public Const STANDARDCLIP As Short = 2
    Structure RATIO
        Dim rto As Integer
        Dim rfrom As Integer
    End Structure

    Public zoomer As RATIO

    Structure PAINTSTRUCT
        Dim hDC As Short
        Dim fErase As Short
        Dim rcpaint As RECT
        Dim fRestore As Short
        Dim fIncUpdate As Short
        <VBFixedString(16)> Public rgbReserved As String
    End Structure

    Declare Function NewCopyToClipboard Lib "misc.dll" (ByVal hwnd As Short, ByVal x As Integer, ByVal Y As Integer,
                                                        ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Declare Function Copytoclipboardratio Lib "misc.dll" (ByVal hwnd As Short, ByVal image As Integer, ByVal toratio As Integer,
                                                          ByVal fromratio As Integer, ByVal realx As Integer, ByVal realy As Integer,
                                                          ByVal realwidth As Integer, ByVal realheight As Integer) As Integer
    Declare Function AliasingOn Lib "misc.dll" (ByVal hwnd As Short, ByVal image As Integer) As Integer

    Structure PointAPI
        Dim x As Short
        Dim Y As Short
    End Structure

    Structure BITMAP '14 bytes
        Dim bmType As Short
        Dim bmWidth As Short
        Dim bmHeight As Short
        Dim bmWidthBytes As Short
        <VBFixedString(1)> Public bmPlanes As String
        <VBFixedString(1)> Public bmBitsPixel As String
        Dim bmBits As Integer
    End Structure

    ' --------------------------------------------------------------------------
    '   Kernel Section
    ' --------------------------------------------------------------------------

    '  GDI Escapes
    Public Const NEWFRAME As Short = 1
    Public Const ABORTDOC As Short = 2
    Public Const NEXTBAND As Short = 3
    Public Const SETCOLORTABLE As Short = 4
    Public Const GETCOLORTABLE As Short = 5
    Public Const FLUSHOUTPUT As Short = 6
    Public Const DRAFTMODE As Short = 7
    Public Const QUERYESCSUPPORT As Short = 8
    Public Const SETABORTPROC As Short = 9
    Public Const STARTDOC As Short = 10
    Public Const ENDDOCAPI As Short = 11
    Public Const GETPHYSPAGESIZE As Short = 12
    Public Const GETPRINTINGOFFSET As Short = 13
    Public Const GETSCALINGFACTOR As Short = 14
    Public Const MFCOMMENT As Short = 15
    Public Const GETPENWIDTH As Short = 16
    Public Const SETCOPYCOUNT As Short = 17
    Public Const SELECTPAPERSOURCE As Short = 18
    Public Const DEVICEDATA As Short = 19
    Public Const PASSTHROUGH As Short = 19
    Public Const GETTECHNOLGY As Short = 20
    Public Const GETTECHNOLOGY As Short = 20
    Public Const SETENDCAP As Short = 21
    Public Const SETLINEJOIN As Short = 22
    Public Const SETMITERLIMIT As Short = 23
    Public Const BANDINFO As Short = 24
    Public Const DRAWPATTERNRECT As Short = 25
    Public Const GETVECTORPENSIZE As Short = 26
    Public Const GETVECTORBRUSHSIZE As Short = 27
    Public Const ENABLEDUPLEX As Short = 28
    Public Const GETSETPAPERBINS As Short = 29
    Public Const GETSETPRINTORIENT As Short = 30
    Public Const ENUMPAPERBINS As Short = 31
    Public Const SETDIBSCALING As Short = 32
    Public Const EPSPRINTING As Short = 33
    Public Const ENUMPAPERMETRICS As Short = 34
    Public Const GETSETPAPERMETRICS As Short = 35
    Public Const POSTSCRIPT_DATA As Short = 37
    Public Const POSTSCRIPT_IGNORE As Short = 38
    Public Const GETEXTENDEDTEXTMETRICS As Short = 256
    Public Const GETEXTENTTABLE As Short = 257
    Public Const GETPAIRKERNTABLE As Short = 258
    Public Const GETTRACKKERNTABLE As Short = 259
    Public Const EXTTEXTOUT As Short = 512
    Public Const ENABLERELATIVEWIDTHS As Short = 768
    Public Const ENABLEPAIRKERNING As Short = 769
    Public Const SETKERNTRACK As Short = 770
    Public Const SETALLJUSTVALUES As Short = 771
    Public Const SETCHARSET As Short = 772

    '  Device Parameters for GetDeviceCaps()
    Public Const DRIVERVERSION As Short = 0 '  Device driver version
    Public Const TECHNOLOGY As Short = 2 '  Device classification
    Public Const HORZSIZE As Short = 4 '  Horizontal size in millimeters
    Public Const VERTSIZE As Short = 6 '  Vertical size in millimeters
    Public Const HORZRES As Short = 8 '  Horizontal width in pixels
    Public Const VERTRES As Short = 10 '  Vertical width in pixels
    Public Const BITSPIXEL As Short = 12 '  Number of bits per pixel
    Public Const PLANES As Short = 14 '  Number of planes
    Public Const NUMBRUSHES As Short = 16 '  Number of brushes the device has
    Public Const NUMPENS As Short = 18 '  Number of pens the device has
    Public Const NUMMARKERS As Short = 20 '  Number of markers the device has
    Public Const NUMFONTS As Short = 22 '  Number of fonts the device has
    Public Const NUMCOLORS As Short = 24 '  Number of colors the device supports
    Public Const PDEVICESIZE As Short = 26 '  Size required for device descriptor
    Public Const RASTERCAPS As Short = 38 '  Bitblt capabilities

    Public Const LOGPIXELSX As Short = 88 '  Logical pixels/inch in X
    Public Const LOGPIXELSY As Short = 90 '  Logical pixels/inch in Y
    Public Const SIZEPALETTE As Short = 104 '  Number of entries in physical palette

    '  Device Capability Masks:

    '  Raster Capabilities
    Public Const RC_BITBLT As Short = 1 '  Can do standard BLT.
    Public Const RC_BANDING As Short = 2 '  Device requires banding support
    Public Const RC_SCALING As Short = 4 '  Device requires scaling support
    Public Const RC_BITMAP64 As Short = 8 '  Device can support >64K bitmap

    '  palette entry flags
    Public Const PC_RESERVED As Integer = &H1 '  palette index used for animation
    Public Const PC_EXPLICIT As Integer = &H2 '  palette index is explicit to device
    Public Const PC_NOCOLLAPSE As Integer = &H4 '  do not match color to system palette

    '  DIB color table identifiers
    Public Const DIB_RGB_COLORS As Short = 0 '  color table in RGBTriples
    Public Const DIB_PAL_COLORS As Short = 1 '  color table in palette indices


    Declare Function GetWindowDC Lib "GDI" (ByVal hwnd As Short) As Short
    Declare Function GetDC Lib "User" (ByVal hwnd As Short) As Short
    Declare Function ReleaseDC Lib "GDI" (ByVal hwnd As Short, ByVal hDC As Short) As Short
    Declare Function CreateDC Lib "GDI" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByVal lpInitData As String) As Short
    Declare Function CreateCompatibleDC Lib "GDI" (ByVal hDC As Short) As Short
    Declare Function DeleteDC Lib "GDI" (ByVal hDC As Short) As Short

    Declare Function StretchBlt Lib "GDI" (ByVal hDC As Short, ByVal x As Short, ByVal Y As Short, ByVal nWidth As Short, ByVal nHeight As Short, ByVal hSrcDC As Short, ByVal XSrc As Short, ByVal YSrc As Short, ByVal nSrcWidth As Short, ByVal nSrcHeight As Short, ByVal dwRop As Integer) As Short

    Declare Function CreateCompatibleBitmap Lib "GDI" (ByVal hDC As Short, ByVal nWidth As Short, ByVal nHeight As Short) As Short

    Declare Function GetDeviceCaps Lib "GDI" (ByVal hDC As Short, ByVal nIndex As Short) As Short

    Declare Sub ClipCursor Lib "User" (ByRef lpRect As RECT)
    Declare Function SetCapture Lib "User" (ByVal hwnd As Short) As Short
    Declare Sub ClientToScreen Lib "User" (ByVal hwnd As Short, ByRef lpPoint As PointAPI)
    Declare Function Escape Lib "GDI" (ByVal hDC As Short, ByVal nEscape As Short, ByVal nCount As Short, ByRef lplnData As Object, ByRef lpOutData As Object) As Short
    Declare Function GetSystemMetrics Lib "User" (ByVal nIndex As Short) As Short

    Public Const ALT_MASK As Short = 4
    Public Const LEFT_BUTTON As Short = 1
    Public Const RIGHT_BUTTON As Short = 2

    Declare Function IsRectEmpty Lib "User" (ByRef lpRect As RECT) As Short
    Declare Function OpenClipboard Lib "User" (ByVal hwnd As Short) As Short
    Declare Function EmptyClipboard Lib "User" () As Short
    Declare Function SetClipboardData Lib "User" (ByVal wFormat As Short, ByVal hMem As Short) As Short
    Declare Function SelectObject Lib "GDI" (ByVal hDC As Short, ByVal hObject As Short) As Short
    Declare Function CloseClipboard Lib "User" () As Short
    Declare Function DeleteObject Lib "GDI" (ByVal hObject As Short) As Short
    Declare Sub GetClientRect Lib "User" (ByVal hwnd As Short, ByRef lpRect As RECT)
    Declare Function GetFreeSpace Lib "Kernel" (ByVal wFlags As Short) As Integer
    'Declare Function GetFreeSpace Lib "Kernel" (ByVal wFlags As Integer) As Long
    Declare Function BeginPaint Lib "User" (ByVal hwnd As Short, ByRef lpPaint As PAINTSTRUCT) As Short
    Declare Sub EndPaint Lib "User" (ByVal hwnd As Short, ByRef lpPaint As PAINTSTRUCT)

    Declare Function LoadCursor Lib "User" (ByVal hInstance As Short, ByRef lpCursorName As String) As Short
    Declare Sub SetCursor Lib "User" (ByVal hCursor As Short)

    Function LoadImage() As Integer
        Dim VX_NONE As Short
        Dim VX_MARKZOOM As Short
        Dim VX_BEST As Short
        Dim VX_4SHADES As Short
        Dim VX_270DEGREE As Short
        Dim VX_LASTPAGE As Short
        Dim iNumPagesInFile As Short

        On Error Resume Next
        If Len(sImageFile) > 0 Then
            gFrmImage.ViewDirX1.UnloadImage()
            gFrmImage.ViewDirX1.AutoRefresh = False
            'frmimage.ViewDirX1.Visible = True
            'load image, don't draw it
            gFrmImage.ViewDirX1.LoadImage(sImageFile)
            If gFrmImage.ViewDirX1.LastErrorString = "Unable to open file" Then
                MessageBox.Show("Could not load specified image", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                gFrmImage.ViewDirX1.Visible = False
                TheImage = 0
                LoadImage = TheImage
                Exit Function
            Else
                TheImage = 1
                LoadImage = TheImage
            End If

            'Load Last page of image if multi page image
            iNumPagesInFile = gFrmImage.ViewDirX1.TotalPages
            If iNumPagesInFile > 1 Then
                gFrmImage.ViewDirX1.GotoPage(VX_LASTPAGE)
            End If

            'Rotate Image if required
            If gFrmImage.ViewDirX1.ImagePixelHeight > gFrmImage.ViewDirX1.ImagePixelWidth Then
                gFrmImage.ViewDirX1.Rotation = VX_270DEGREE
            End If

            gFrmImage.ViewDirX1.ZoomPercentage = 50
            gFrmImage.ViewDirX1.BitonalScaleToGray = VX_4SHADES
            gFrmImage.ViewDirX1.ImageQuality = VX_BEST
            gFrmImage.ViewDirX1.LeftMouseTool = VX_MARKZOOM
            gFrmImage.ViewDirX1.RightMouseTool = VX_NONE
        End If
    End Function
End Module