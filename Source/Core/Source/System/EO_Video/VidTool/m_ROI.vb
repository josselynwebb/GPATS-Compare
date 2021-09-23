Option Strict Off
Option Explicit On

Imports System.IO

''' <summary>
''' Provide support for Region-of-Interest interface
''' </summary>
''' <remarks></remarks>
Module m_ROI

#Region "Public Members"
    'Public gimgStdPic As System.Drawing.Image 'Picture Object
    Public giBitHeight As Integer 'Bitmap Height in pixels
    Public giBitWidth As Integer 'Bitmap Width in pixels
    Public giHorzRes As Integer 'Horizontal Screen Resolution

    'ID's for Rectange's sides
    Public Const NORTH As Short = 4 'Top
    Public Const SOUTH As Short = 5 'Bottom
    Public Const EAST As Short = 6 'Right
    Public Const WEST As Short = 7 'Left

    Public recTypeLRTBInstance As RectTypeLRTB 'Create an Instance of RECT as RecType

    Public Structure RectTypeLRTB 'Structure used in lpvParam of SystemParametersInfo function
        Dim iLeft As Integer
        Dim Top As Integer
        Dim iRight As Integer
        Dim Bottom As Integer
    End Structure

    Public Structure RECTX1Y1X2Y2
        Dim x1 As Integer
        Dim y1 As Integer
        Dim x2 As Integer
        Dim y2 As Integer
    End Structure

    Public Structure tagInitCommonControlsEx
        Dim lngSize As Integer
        Dim lngICC As Integer
    End Structure
#End Region '"Public Members"

#Region "Public Methods"
    ''' <summary>
    ''' Returns the calculated Field-of-View in Radians.
    ''' </summary>
    ''' <param name="dROIDim_Pixels">
    '''     dROIDim_Pixels -   ROI Box Dimension in Pixels
    ''' </param>
    ''' <param name="dTargetDim_mRad">
    '''     dTargetDim_mRad -  Target Dimension in mRads
    ''' </param>
    ''' <param name="dImageDimPixel">
    '''     dImageDim -        Image Dimension in Pixels
    ''' </param>
    ''' <returns>
    '''     Image Dimension in Radians(From NAM), otherwise, mRads
    ''' </returns>
    ''' <remarks></remarks>
    Public Function dCalcFieldOfView(ByRef dROIDim_Pixels As Double, ByRef dTargetDim_mRad As Double, ByRef dImageDimPixel As Double) As Double
        Dim d_mRadPerPixel As Double 'mRad per Pixel
        Dim dImageDim_mRad As Double 'Image Dimension in mRads

        'Calculate mRads per Pixel
        d_mRadPerPixel = (dTargetDim_mRad / dROIDim_Pixels)
        'Calculate Image Dimension in mRads
        dImageDim_mRad = (dImageDimPixel * d_mRadPerPixel)

        If gbRunFromNAM Then
            'Calculate and return Image Dimension in Radians
            dCalcFieldOfView = dImageDim_mRad / 1000
        Else
            'Calculate and return Image Dimension in mRads
            dCalcFieldOfView = dImageDim_mRad
        End If
    End Function

    ''' <summary>
    ''' Retrieve Height and Width Information from .BMP File
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <remarks></remarks>
    Public Sub GetBMPInfo(ByRef sPath As String)
        Dim bmpFile As Bitmap

        bmpFile = New Bitmap(sPath)

        Debug.Print(String.Format("bmpFile.Height = '{0}'", bmpFile.Height.ToString))
        Debug.Print(String.Format("bmpFile.Width = '{0}'", bmpFile.Width.ToString))
        giBitHeight = bmpFile.Height
        giBitWidth = bmpFile.Width

        With gofrmROI 'Update Image Height and Width on GUI
            'LARRYP: Changing this to be Camera Settings like we use on frmMain!!
            '.txtImageHeight.Text = giBitHeight.ToString
            '.txtImageWidth.Text = giBitWidth.ToString
            .txtImageHeight.Text = gpixciCamera.maxHeight.ToString
            .txtImageWidth.Text = gpixciCamera.maxWidth.ToString
        End With

        'Release Loaded Image
        bmpFile.Dispose()
    End Sub

    ''' <summary>
    ''' Load previously saved "Capture.bmp" into Picture Box (gofrmROI.picROI).
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadIt()
        Dim sFile As String

        sFile = gsXCAPCaptureDir & "Capture.bmp"

        PXD_LOADBMP(1, sFile, 1, 0, 0, -1, -1, 0, 0)
        GetBMPInfo(sFile)

        gofrmROI.pboxROI.BringToFront() 'Bring picROI forward

        'Cause Refresh of pboxROI
        gofrmROI.pboxROI.Invalidate()
    End Sub

    ''' <summary>
    ''' Setup GUI, Load Image, and Size form for the FoV Calculator.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Load_ROI_FOV()

        If gofrmROI Is Nothing Then
            gofrmROI = New frmROI
        End If

        gofrmROI.LoadImage() 'Load Captured Image

        gofrmROI.Show() 'Display ROI Form
    End Sub
#End Region '"Public Methods"

#Region "Private Members"
    '----- Type Declaration for APIs -----
    Private Structure BITMAPINFOHEADER '40 bytes
        Dim biSize As Integer
        Dim biWidth As Integer
        Dim biHeight As Integer
        Dim biPlanes As Short
        Dim biBitCount As Short
        Dim biCompression As Integer
        Dim biSizeImage As Integer
        Dim biXPelsPerMeter As Integer
        Dim biYPelsPerMeter As Integer
        Dim biRUsed As Integer
        Dim biRImportant As Integer
    End Structure

    Private Structure BITMAPFILEHEADER
        Dim bfType As Short
        Dim bfSize As Integer
        Dim bfReserved1 As Short
        Dim bfReserved2 As Short
        Dim bfOffBits As Integer
    End Structure
#End Region '"Private Members"
End Module
