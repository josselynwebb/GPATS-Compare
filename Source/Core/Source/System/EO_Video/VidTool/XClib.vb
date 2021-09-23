Module XClib
    Public Declare Function PXD_PIXCICLOSE Lib "XCLIBWNT.DLL" Alias "pxd_PIXCIclose" () As Integer

    Public Declare Function PXD_PIXCIOPEN Lib "XCLIBWNT.DLL" Alias "pxd_PIXCIopen" _
        (ByVal c_driverparms As String, ByVal c_formatname As String, ByVal c_formatfile As String) As Integer

    Public Declare Function PXD_IMAGEBDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageBdim" () As Integer

    Public Declare Function PXD_IMAGEXDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageXdim" () As Integer

    Public Declare Function PXD_IMAGEYDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageYdim" () As Integer

    Public Declare Function PXD_CAPTUREDFIELDCOUNT Lib "XCLIBWNT.DLL" Alias "pxd_capturedFieldCount" (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_IMAGECDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageCdim" () As Integer

    Public Declare Function PXD_GOLIVE Lib "XCLIBWNT.DLL" Alias "pxd_goLive" (ByVal c_unitmap As Integer, ByVal c_buffer As Integer) As Integer

    Public Declare Function PXD_GOUNLIVE Lib "XCLIBWNT.DLL" Alias "pxd_goUnLive" (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_GOSNAP Lib "XCLIBWNT.DLL" Alias "pxd_goSnap" (ByVal c_unitmap As Integer, ByVal c_buffer As Integer) As Integer

    Public Declare Function SetStretchBltMode Lib "gdi32.dll" (ByVal hDC As IntPtr, ByVal mode As Integer) As Integer

    Public Declare Function PXD_RENDERSTRETCHDIBITS Lib "XCLIBWNT.DLL" Alias "pxd_renderStretchDIBits" (ByVal c_unitmap As Integer, ByVal c_buf As Integer, ByVal c_ulx As Integer, _
                                                                                                        ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, _
                                                                                                        ByVal c_options As Integer, ByVal c_hDC As IntPtr, ByVal c_nX As Integer, _
                                                                                                        ByVal c_nY As Integer, ByVal c_nWidth As Integer, ByVal c_nHeight As Integer, _
                                                                                                        ByVal c_winoptions As Integer) As Integer

    Public Declare Function PXD_SAVEBMP Lib "XCLIBWNT.DLL" Alias "pxd_saveBmp" (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_buf As Integer, ByVal c_ulx As Integer, _
                                                                                ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_savemode As Integer, _
                                                                                ByVal c_options As Integer) As Integer

    Public Declare Function PXD_LOADBMP Lib "XCLIBWNT.DLL" Alias "pxd_loadBmp" (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_buf As Integer, ByVal c_ulx As Integer, _
                                                                                ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_loadmode As Integer, _
                                                                                ByVal c_options As Integer) As Integer

    Public Declare Function PXD_MESGFAULT Lib "XCLIBWNT.DLL" Alias "pxd_mesgFault" (ByVal c_unitmap As Integer) As Integer
End Module
