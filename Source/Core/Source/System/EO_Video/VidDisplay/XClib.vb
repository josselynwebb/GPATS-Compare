Module XClib

    'NOT USED CURRENTLY
    'Const ascii_b = 98  ' an asc("b")
    'Const ascii_e = 101 ' an asc("e")
    'Const ascii_n = 110 ' an asc("n")
    'Const ascii_o = 111 ' an asc("o")
    'Const ascii_p = 112 ' an asc("p")
    'Const ascii_s = 115 ' an asc("s")
    'Const ascii_r = 114 ' an asc("r")
    'Const ascii_v = 118 ' an asc("v")
    'Const ascii_w = 119 ' an asc("w")
    'Const ascii_z = 122 ' an asc("z")
    'Const ascii_capB = 66  ' an asc("B")
    'Const ascii_capG = 71  ' an asc("G")
    'Const ascii_capR = 82  ' an asc("R")
    'Const PXFNXT = &H1 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXFODD = &H2 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXFEVN = &H4 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXFNOW = &H8 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXSDIGI = &H10 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXSDISP = &H20 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXSIMOP = &H40 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXSBLCK = &H80 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXSNVOT = &H81 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXAWAIT = &H0 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXASYNC = &H1000 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXABORT = &H2000 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXAALL = &H3000 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXIBXTC = &H80 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXPIXBLT = &H0 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXPIXBATN = &H1 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXPIXBRT = &H2 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXPIXBWT = &H3 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXPIXBXT = &H4 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXODONE = &H1 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXOPRUN = &H0 ' (for SVOBJ/4MOBJ/XCOBJ, not XCLIB)
    'Const PXREAD = &H0
    'Const PXRITE = &H1
    'Const PXDATUCHAR = &H1
    'Const PXDATUSHORT = &H2
    'Const PXDATUINT = &H4
    'Const PXDATULONG = &H8
    'Const PXDATFLOAT = &H10
    'Const PXDATDOUBLE = &H20
    'Const PXDATMSB = &H100
    'Const PXDATLSB = &H200
    'Const PXDATDIRTY = &H400
    'Const PXDATENDIAN = &H800
    'Const PXRXSCAN = &H0
    'Const PXRYSCAN = &H2
    'Const PXRZSCAN = &H4
    'Const PXRXYZSCAN = &H6
    'Const PXIWRAP = &H40
    'Const PXIASYNC = &H800
    'Const PXIMAYMOD = &H1000
    'Const PXIXYVALID = &H2000
    'Const PXHINTNONE = &H0
    'Const PXHINTFRAME = &H10
    'Const PXHINTGREY = &H1
    'Const PXHINTGRAY = &H1
    'Const PXHINTINDEX = &H11
    'Const PXHINTBAYER = &H21
    'Const PXHINTCOMPLEX = &H2
    'Const PXHINTCBYCRY = &H12
    'Const PXHINTYCBYCR = &H22
    'Const PXHINTBGR = &H3
    'Const PXHINTYCRCB = &H13
    'Const PXHINTBSH = &H23
    'Const PXHINTRGB = &H43
    'Const PXHINTBGRX = &H4
    'Const PXHINTYCRCBX = &H14
    'Const PXHINTRGBX = &H24
    'Const PXHINTCMY = &H63
    'Const PXHINTCMYK = &H44
    'Const PXHINTPIXIES = &HF
    'Const PXHINTUSER = &H7000
    'Const PXHINTUSERN = &HFFF
    'Const PXUNITUNKNOWN = &H0
    'Const PXUNITRATIO = &H72
    'Const PXUNITINCH = &HBC
    'Const PXUNITFOOT = &HB8
    'Const PXUNITMETER = &H6D
    'Const PXUNITMILLIMETER = &HB7
    'Const PXUNITCENTIMETER = &HAB
    'Const PXUNITSECOND = &H83
    'Const PXIP8BLOB_CONNECT4 = &H0
    'Const PXIP8BLOB_NOCLEAR = &H2
    'Const PXIP8BLOB_PERIMITER = &H4
    'Const PXIP8BLOB_IGNOREEDGE = &H8
    'Const PXIP8BLOB_CONVEX = &H100
    'Const PXIP8BLOB_NOHOLE = &H200
    'Const CLR_INVALID = &HFFFFFFFF

    '''''''SILICON VIDEO FUNCTIONS

    ''''''' PXIPL ''''''
    '
    ' This declaration should be commented out if the PXIPL library
    ' is not being used.
    '
    ' _cDcl(_dllpxipl,_cfunfcc,int)pxip8_pixneg(pxabortfunc_t**,pximage_s*sip,pximage_s*dip);

    ' The `noname1' parameter has been declared expecting a 0 to be passed
    ' The `sip' parameter has been declared expecting the return value of PXD_DEFIMAGE/PXD_DEFINEIMAGE to be passed
    ' The `dip' parameter has been declared expecting the return value of PXD_DEFIMAGE/PXD_DEFINEIMAGE to be passed
    Private Declare Function PXIP8_PIXNEG Lib "c:\xclib\pxiplwnt.dll" Alias "_pxip8_pixneg@12" _
    (ByVal c_noname1 As Integer, ByVal c_sip As Integer, ByVal c_dip As Integer) As Integer
    '
    ''''''' PXIPL ''''''


    '''''''''
    '
    ' Declarations that must be must be included in the declarations section.
    ' The SetStretchBltMode is a Windows function, for
    ' which VB .NET doesn't have a direct equivalent.
    '
    '''''''''
    Public Declare Function SetStretchBltMode Lib "gdi32.dll" (ByVal hDC As IntPtr, ByVal mode As Integer) As Integer
    Public Const STRETCH_DELETESCANS As Integer = 3 'Constant used in calling SetStrechBltMode
    Public Const R As Integer = 82
    Public Const G As Integer = 71
    Public Const B As Integer = 66
    Public Const RxorG As Integer = 21
    Public Const BxorG As Integer = 5
    Public Const Max As Double = 9.0E+99
    Public Const Min As Double = -9.0E+99

    '                                                          
    '   XCLIBWNT_VBNET.TXT  External     19-Oct-2010    
    '   Copyright (C)  2006-2010   EPIX, Inc.  All rights reserved.
    '                                                          
    '   XCLIB Declarations for VB.NET x86 (32 bit)             
    '                                                          

    Public Declare Function PXD_PIXCIOPEN Lib "XCLIBWNT.DLL" Alias "pxd_PIXCIopen" _
    (ByVal c_driverparms As String, ByVal c_formatname As String, ByVal c_formatfile As String) As Integer

    Public Declare Function PXD_PIXCICLOSE Lib "XCLIBWNT.DLL" Alias "pxd_PIXCIclose" _
    () As Integer

    Public Declare Function PXD_MESGFAULT Lib "XCLIBWNT.DLL" Alias "pxd_mesgFault" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_MESGERRORCODE Lib "XCLIBWNT.DLL" Alias "pxd_mesgErrorCode" _
    (ByVal c_err As Integer) As String

    Public Declare Function PXD_MESGFAULTTEXT Lib "XCLIBWNT.DLL" Alias "pxd_mesgFaultText" _
    (ByVal c_unitmap As Integer, ByRef c_buf As Byte, ByVal c_bufsize As Integer) As Integer

    Public Declare Function PXD_INFOMODEL Lib "XCLIBWNT.DLL" Alias "pxd_infoModel" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_INFOSUBMODEL Lib "XCLIBWNT.DLL" Alias "pxd_infoSubmodel" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_INFOUNITS Lib "XCLIBWNT.DLL" Alias "pxd_infoUnits" _
    () As Integer

    Public Declare Function PXD_INFODRIVERID Lib "XCLIBWNT.DLL" Alias "pxd_infoDriverId" _
    () As String

    Public Declare Function PXD_INFOLIBRARYID Lib "XCLIBWNT.DLL" Alias "pxd_infoLibraryId" _
    () As String

    Public Declare Function PXD_INFOMEMSIZE Lib "XCLIBWNT.DLL" Alias "pxd_infoMemsize" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_IMAGEXDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageXdim" _
    () As Integer

    Public Declare Function PXD_IMAGEYDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageYdim" _
    () As Integer

    Public Declare Function PXD_IMAGECDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageCdim" _
    () As Integer

    Public Declare Function PXD_IMAGEBDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageBdim" _
    () As Integer

    Public Declare Function PXD_IMAGEZDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageZdim" _
    () As Integer

    Public Declare Function PXD_IMAGEIDIM Lib "XCLIBWNT.DLL" Alias "pxd_imageIdim" _
    () As Integer

    Public Declare Function PXD_IMAGEASPECTRATIO Lib "XCLIBWNT.DLL" Alias "pxd_imageAspectRatio" _
    () As Double

    Public Declare Function PXD_IMAGEXDIMS Lib "XCLIBWNT.DLL" Alias "pxd_imageXdims" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_IMAGEYDIMS Lib "XCLIBWNT.DLL" Alias "pxd_imageYdims" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_IMAGECDIMS Lib "XCLIBWNT.DLL" Alias "pxd_imageCdims" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_IMAGEBDIMS Lib "XCLIBWNT.DLL" Alias "pxd_imageBdims" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_IMAGEZDIMS Lib "XCLIBWNT.DLL" Alias "pxd_imageZdims" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_IMAGEIDIMS Lib "XCLIBWNT.DLL" Alias "pxd_imageIdims" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_IMAGEASPECTRATIOS Lib "XCLIBWNT.DLL" Alias "pxd_imageAspectRatios" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_READUCHAR Lib "XCLIBWNT.DLL" Alias "pxd_readuchar" _
    (ByVal c_unitmap As Integer, ByVal c_framebuf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByRef c_membuf As Byte, ByVal c_cnt As Integer, ByVal c_colorspace As String) As Integer

    Public Declare Function PXD_WRITEUCHAR Lib "XCLIBWNT.DLL" Alias "pxd_writeuchar" _
    (ByVal c_unitmap As Integer, ByVal c_framebuf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByRef c_membuf As Byte, ByVal c_cnt As Integer, ByVal c_colorspace As String) As Integer

    Public Declare Function PXD_READUSHORT Lib "XCLIBWNT.DLL" Alias "pxd_readushort" _
    (ByVal c_unitmap As Integer, ByVal c_framebuf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByRef c_membuf As Short, ByVal c_cnt As Integer, ByVal c_colorspace As String) As Integer

    Public Declare Function PXD_WRITEUSHORT Lib "XCLIBWNT.DLL" Alias "pxd_writeushort" _
    (ByVal c_unitmap As Integer, ByVal c_framebuf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByRef c_membuf As Short, ByVal c_cnt As Integer, ByVal c_colorspace As String) As Integer

    Public Declare Function PXD_DEFINEIMAGE Lib "XCLIBWNT.DLL" Alias "pxd_defineImage" _
    (ByVal c_unitmap As Integer, ByVal c_framebuf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_colorspace As String) As IntPtr

    Public Declare Function PXD_DEFINEIMAGE3 Lib "XCLIBWNT.DLL" Alias "pxd_defineImage3" _
    (ByVal c_unitmap As Integer, ByVal c_startbuf As Integer, ByVal c_endbuf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_colorspace As String) As IntPtr

    Public Declare Function PXD_DEFINEPXIMAGE Lib "XCLIBWNT.DLL" Alias "pxd_definePximage" _
    (ByVal c_unitmap As Integer, ByVal c_framebuf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_colorspace As String) As IntPtr

    Public Declare Function PXD_DEFINEPXIMAGE3 Lib "XCLIBWNT.DLL" Alias "pxd_definePximage3" _
    (ByVal c_unitmap As Integer, ByVal c_startbuf As Integer, ByVal c_endbuf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_colorspace As String) As IntPtr

    Public Declare Sub PXD_DEFINEPXIMAGEFREE Lib "XCLIBWNT.DLL" Alias "pxd_definePximageFree" _
    (ByVal c_noname1 As IntPtr)

    Public Declare Sub PXD_DEFINEPXIMAGE3FREE Lib "XCLIBWNT.DLL" Alias "pxd_definePximage3Free" _
    (ByVal c_noname1 As IntPtr)

    Public Declare Function PXD_DOSNAP Lib "XCLIBWNT.DLL" Alias "pxd_doSnap" _
    (ByVal c_unitmap As Integer, ByVal c_buffer As Integer, ByVal c_timeout As Integer) As Integer

    Public Declare Function PXD_GOSNAP Lib "XCLIBWNT.DLL" Alias "pxd_goSnap" _
    (ByVal c_unitmap As Integer, ByVal c_buffer As Integer) As Integer

    Public Declare Function PXD_GOSNAPPAIR Lib "XCLIBWNT.DLL" Alias "pxd_goSnapPair" _
    (ByVal c_unitmap As Integer, ByVal c_buffer1 As Integer, ByVal c_buffer2 As Integer) As Integer

    Public Declare Function PXD_GOLIVE Lib "XCLIBWNT.DLL" Alias "pxd_goLive" _
    (ByVal c_unitmap As Integer, ByVal c_buffer As Integer) As Integer

    Public Declare Function PXD_GOUNLIVE Lib "XCLIBWNT.DLL" Alias "pxd_goUnLive" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_GOABORTLIVE Lib "XCLIBWNT.DLL" Alias "pxd_goAbortLive" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_GOLIVEPAIR Lib "XCLIBWNT.DLL" Alias "pxd_goLivePair" _
    (ByVal c_unitmap As Integer, ByVal c_buffer1 As Integer, ByVal c_buffer2 As Integer) As Integer

    Public Declare Function PXD_GOLIVESEQ Lib "XCLIBWNT.DLL" Alias "pxd_goLiveSeq" _
    (ByVal c_unitmap As Integer, ByVal c_startbuf As Integer, ByVal c_endbuf As Integer, ByVal c_incbuf As Integer, ByVal c_numbuf As Integer, ByVal c_period As Integer) As Integer

    Public Declare Function PXD_GOLIVETRIG Lib "XCLIBWNT.DLL" Alias "pxd_goLiveTrig" _
    (ByVal c_unitmap As Integer, ByVal c_buffer As Integer, ByVal c_gpin10mask As Integer, ByVal c_gpout20value As Integer, ByVal c_gpout20mask As Integer, ByVal c_gpout20when As Integer, _
     ByVal c_gpin30wait As Integer, ByVal c_gpin30mask As Integer, ByVal c_gpout40value As Integer, ByVal c_gpout40mask As Integer, _
     ByVal c_option50 As Integer, ByVal c_field50 As Integer, ByVal c_gpout50value As Integer, ByVal c_gpout50mask As Integer, ByVal c_delay60 As Integer, ByVal c_gpout60value As Integer, ByVal c_gpout60mask As Integer, _
     ByVal c_delay70 As Integer, ByVal c_field70 As Integer, ByVal c_capture70 As Integer, ByVal c_gpin80mask As Integer, ByVal c_gpout80value As Integer, ByVal c_gpout80mask As Integer) As Integer

    Public Declare Function PXD_GOLIVESEQTRIG Lib "XCLIBWNT.DLL" Alias "pxd_goLiveSeqTrig" _
    (ByVal c_unitmap As Integer, ByVal c_startbuf As Integer, ByVal c_endbuf As Integer, ByVal c_incbuf As Integer, ByVal c_numbuf As Integer, ByVal c_period As Integer, ByVal c_rsvd1 As Integer, ByVal c_rsvd2 As Integer, ByVal c_trig20wait As Integer, _
     ByVal c_trig20slct As Integer, ByVal c_trig20delay As Integer, ByVal c_rsvd3 As Integer, ByVal c_rsvd4 As Integer, ByVal c_rsvd5 As Integer, ByVal c_rsvd6 As Integer, ByVal c_rsvd7 As Integer, ByVal c_rsvd8 As Integer, ByVal c_rsvd9 As Integer,
     ByVal c_trig40wait As Integer, ByVal c_trig40slct As Integer, ByVal c_trig40delay As Integer, ByVal c_rsvd10 As Integer, ByVal c_rsvd11 As Integer, ByVal c_rsvd12 As Integer, ByVal c_rsvd13 As Integer, ByVal c_rsvd14 As Integer, _
     ByVal c_rsvd15 As Integer) As Integer

    Public Declare Function PXD_GONELIVE Lib "XCLIBWNT.DLL" Alias "pxd_goneLive" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd As Integer) As Integer

    Public Declare Function PXD_VIDEOFIELDSPERFRAME Lib "XCLIBWNT.DLL" Alias "pxd_videoFieldsPerFrame" _
    () As Integer

    Public Declare Function PXD_VIDEOFIELDSPERFRAMES Lib "XCLIBWNT.DLL" Alias "pxd_videoFieldsPerFrames" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_VIDEOFIELDCOUNT Lib "XCLIBWNT.DLL" Alias "pxd_videoFieldCount" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_CAPTUREDBUFFER Lib "XCLIBWNT.DLL" Alias "pxd_capturedBuffer" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_CAPTUREDSYSTICKS Lib "XCLIBWNT.DLL" Alias "pxd_capturedSysTicks" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_CAPTUREDFIELDCOUNT Lib "XCLIBWNT.DLL" Alias "pxd_capturedFieldCount" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_BUFFERSFIELDCOUNT Lib "XCLIBWNT.DLL" Alias "pxd_buffersFieldCount" _
    (ByVal c_unitmap As Integer, ByVal c_buffer As Integer) As Integer

    Public Declare Function PXD_BUFFERSSYSTICKS Lib "XCLIBWNT.DLL" Alias "pxd_buffersSysTicks" _
    (ByVal c_unitmap As Integer, ByVal c_buffer As Integer) As Integer

    Public Declare Function PXD_BUFFERSSYSTICKS2 Lib "XCLIBWNT.DLL" Alias "pxd_buffersSysTicks2" _
    (ByVal c_unitmap As Integer, ByVal c_buffer As Integer, ByRef c_ticks As Integer) As Integer

    Public Declare Function PXD_BUFFERSGPIN Lib "XCLIBWNT.DLL" Alias "pxd_buffersGPIn" _
    (ByVal c_unitmap As Integer, ByVal c_buffer As Integer) As Integer

    Public Declare Function PXD_SETVIDMUX Lib "XCLIBWNT.DLL" Alias "pxd_setVidMux" _
    (ByVal c_unitmap As Integer, ByVal c_inmux As Integer) As Integer

    Public Declare Function PXD_GETVIDMUX Lib "XCLIBWNT.DLL" Alias "pxd_getVidMux" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SETCONTRASTBRIGHTNESS Lib "XCLIBWNT.DLL" Alias "pxd_setContrastBrightness" _
    (ByVal c_unitmap As Integer, ByVal c_contrast As Double, ByVal c_brightness As Double) As Integer

    Public Declare Function PXD_SETHUESATURATION Lib "XCLIBWNT.DLL" Alias "pxd_setHueSaturation" _
    (ByVal c_unitmap As Integer, ByVal c_hue As Double, ByVal c_Ugain As Double, ByVal c_Vgain As Double) As Integer

    Public Declare Function PXD_GETCONTRAST Lib "XCLIBWNT.DLL" Alias "pxd_getContrast" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_GETBRIGHTNESS Lib "XCLIBWNT.DLL" Alias "pxd_getBrightness" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_GETHUE Lib "XCLIBWNT.DLL" Alias "pxd_getHue" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_GETUGAIN Lib "XCLIBWNT.DLL" Alias "pxd_getUGain" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_GETVGAIN Lib "XCLIBWNT.DLL" Alias "pxd_getVGain" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SETEXSYNCPRIN Lib "XCLIBWNT.DLL" Alias "pxd_setExsyncPrin" _
    (ByVal c_unitmap As Integer, ByVal c_exsync As Integer, ByVal c_prin As Integer) As Integer

    Public Declare Function PXD_GETEXSYNC Lib "XCLIBWNT.DLL" Alias "pxd_getExsync" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_GETPRIN Lib "XCLIBWNT.DLL" Alias "pxd_getPrin" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SETEXSYNCPRINCMODE Lib "XCLIBWNT.DLL" Alias "pxd_setExsyncPrincMode" _
    (ByVal c_unitmap As Integer, ByVal c_exsyncbits As Integer, ByVal c_princbits As Integer) As Integer

    Public Declare Function PXD_GETEXSYNCMODE Lib "XCLIBWNT.DLL" Alias "pxd_getExsyncMode" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_GETPRINCMODE Lib "XCLIBWNT.DLL" Alias "pxd_getPrincMode" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_GETGPIN Lib "XCLIBWNT.DLL" Alias "pxd_getGPIn" _
    (ByVal c_unitmap As Integer, ByVal c_data As Integer) As Integer

    Public Declare Function PXD_SETGPIN Lib "XCLIBWNT.DLL" Alias "pxd_setGPIn" _
    (ByVal c_unitmap As Integer, ByVal c_data As Integer) As Integer

    Public Declare Function PXD_SETGPOUT Lib "XCLIBWNT.DLL" Alias "pxd_setGPOut" _
    (ByVal c_unitmap As Integer, ByVal c_data As Integer) As Integer

    Public Declare Function PXD_GETGPOUT Lib "XCLIBWNT.DLL" Alias "pxd_getGPOut" _
    (ByVal c_unitmap As Integer, ByVal c_data As Integer) As Integer

    Public Declare Function PXD_GETGPTRIGGER Lib "XCLIBWNT.DLL" Alias "pxd_getGPTrigger" _
    (ByVal c_unitmap As Integer, ByVal c_which As Integer) As Integer

    Public Declare Function PXD_SETCAMERALINKCCOUT Lib "XCLIBWNT.DLL" Alias "pxd_setCameraLinkCCOut" _
    (ByVal c_unitmap As Integer, ByVal c_data As Integer) As Integer

    Public Declare Function PXD_GETCAMERALINKCCOUT Lib "XCLIBWNT.DLL" Alias "pxd_getCameraLinkCCOut" _
    (ByVal c_unitmap As Integer, ByVal c_data As Integer) As Integer

    Public Declare Function PXD_RENDERDIBCREATE Lib "XCLIBWNT.DLL" Alias "pxd_renderDIBCreate" _
    (ByVal c_unitmap As Integer, ByVal c_buf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_mode As Integer, ByVal c_options As Integer) As IntPtr

    Public Declare Function PXD_RENDERDIBFREE Lib "XCLIBWNT.DLL" Alias "pxd_renderDIBFree" _
    (ByVal c_hDIB As IntPtr) As Integer

    Public Declare Function PXD_RENDERSTRETCHDIBITS Lib "XCLIBWNT.DLL" Alias "pxd_renderStretchDIBits" _
    (ByVal c_unitmap As Integer, ByVal c_buf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_options As Integer, ByVal c_hDC As IntPtr, _
     ByVal c_nX As Integer, ByVal c_nY As Integer, ByVal c_nWidth As Integer, ByVal c_nHeight As Integer, ByVal c_winoptions As Integer) As Integer

    Public Declare Function PXD_SAVEBMP Lib "XCLIBWNT.DLL" Alias "pxd_saveBmp" _
    (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_buf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_savemode As Integer, ByVal c_options As Integer) As Integer

    Public Declare Function PXD_LOADBMP Lib "XCLIBWNT.DLL" Alias "pxd_loadBmp" _
    (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_buf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_loadmode As Integer, ByVal c_options As Integer) As Integer

    Public Declare Function PXD_SAVETGA Lib "XCLIBWNT.DLL" Alias "pxd_saveTga" _
    (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_buf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_savemode As Integer, ByVal c_options As Integer) As Integer

    Public Declare Function PXD_SAVEPCX Lib "XCLIBWNT.DLL" Alias "pxd_savePcx" _
    (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_buf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_savemode As Integer, ByVal c_options As Integer) As Integer

    Public Declare Function PXD_SAVETIFF Lib "XCLIBWNT.DLL" Alias "pxd_saveTiff" _
    (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_buf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_savemode As Integer, ByVal c_options As Integer) As Integer

    Public Declare Function PXD_LOADTIFF Lib "XCLIBWNT.DLL" Alias "pxd_loadTiff" _
    (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_buf As Integer, ByVal c_ulx As Integer, ByVal c_uly As Integer, ByVal c_lrx As Integer, ByVal c_lry As Integer, ByVal c_loadmode As Integer, ByVal c_options As Integer) As Integer

    Public Declare Function PXD_SAVERAWBUFFERS Lib "XCLIBWNT.DLL" Alias "pxd_saveRawBuffers" _
    (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_startbuf As Integer, ByVal c_endbuf As Integer, ByRef c_filehandle As IntPtr, ByVal c_fileoffset As Integer, ByVal c_alignment As Integer, ByVal c_options As Integer) As Integer

    Public Declare Function PXD_LOADRAWBUFFERS Lib "XCLIBWNT.DLL" Alias "pxd_loadRawBuffers" _
    (ByVal c_unitmap As Integer, ByVal c_pathname As String, ByVal c_startbuf As Integer, ByVal c_endbuf As Integer, ByRef c_filehandle As IntPtr, ByVal c_fileoffset As Integer, ByVal c_alignment As Integer, ByVal c_options As Integer) As Integer

    Public Declare Function PXD_RENDERDIRECTVIDEOUNLIVE Lib "XCLIBWNT.DLL" Alias "pxd_renderDirectVideoUnLive" _
    (ByVal c_unitmap As Integer, ByVal c_hWnd As IntPtr) As Integer

    Public Declare Function PXD_RENDERDIRECTVIDEOLIVE Lib "XCLIBWNT.DLL" Alias "pxd_renderDirectVideoLive" _
    (ByVal c_unitmap As Integer, ByVal c_hWnd As IntPtr, ByVal c_nX As Integer, ByVal c_nY As Integer, ByVal c_nWidth As Integer, ByVal c_nHeight As Integer, ByVal c_ClrKey1 As Integer, ByVal c_ClrKey2 As Integer) As Integer

    Public Declare Function PXD_RENDERDIRECTVIDEODONE Lib "XCLIBWNT.DLL" Alias "pxd_renderDirectVideoDone" _
    (ByVal c_unitmap As Integer, ByVal c_hWnd As IntPtr) As Integer

    Public Declare Function PXD_RENDERDIRECTVIDEOINIT Lib "XCLIBWNT.DLL" Alias "pxd_renderDirectVideoInit" _
    (ByVal c_unitmap As Integer, ByVal c_hWnd As IntPtr) As Integer

    Public Declare Function PXD_SETIMAGEDARKBALANCE Lib "XCLIBWNT.DLL" Alias "pxd_setImageDarkBalance" _
    (ByVal c_unitmap As Integer, ByRef c_referenceRGB As Integer, ByRef c_targetRGB As Integer, ByVal c_gamma As Double) As Integer

    Public Declare Function PXD_SETIMAGEBRIGHTBALANCE Lib "XCLIBWNT.DLL" Alias "pxd_setImageBrightBalance" _
    (ByVal c_unitmap As Integer, ByRef c_referenceRGB As Integer, ByRef c_targetRGB As Integer, ByVal c_gamma As Double) As Integer

    Public Declare Function PXD_SERIALCONFIGURE Lib "XCLIBWNT.DLL" Alias "pxd_serialConfigure" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd0 As Integer, ByVal c_baud As Double, ByVal c_bits As Integer, ByVal c_parity As Integer, ByVal c_stopbits As Integer, _
     ByVal c_rsvd1 As Integer, ByVal c_rsvd2 As Integer, ByVal c_rsvd3 As Integer) As Integer

    Public Declare Function PXD_SERIALREAD Lib "XCLIBWNT.DLL" Alias "pxd_serialRead" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd0 As Integer, ByRef c_buf As Byte, ByVal c_cnt As Integer) As Integer

    Public Declare Function PXD_SERIALWRITE Lib "XCLIBWNT.DLL" Alias "pxd_serialWrite" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd0 As Integer, ByRef c_buf As Byte, ByVal c_cnt As Integer) As Integer

    Public Declare Function CLSERIALINIT Lib "XCLIBWNT.DLL" Alias "clSerialInit" _
    () As Integer

    Public Declare Sub CLSERIALCLOSE Lib "XCLIBWNT.DLL" Alias "clSerialClose" _
    ()

    Public Declare Function CLSERIALREAD Lib "XCLIBWNT.DLL" Alias "clSerialRead" _
    () As Integer

    Public Declare Function CLSERIALWRITE Lib "XCLIBWNT.DLL" Alias "clSerialWrite" _
    () As Integer

    Public Declare Function CLGETNUMBYTESAVAIL Lib "XCLIBWNT.DLL" Alias "clGetNumBytesAvail" _
    () As Integer

    Public Declare Function CLFLUSHPORT Lib "XCLIBWNT.DLL" Alias "clFlushPort" _
    () As Integer

    Public Declare Function CLGETERRORTEXT Lib "XCLIBWNT.DLL" Alias "clGetErrorText" _
    () As Integer

    Public Declare Function CLGETNUMSERIALPORTS Lib "XCLIBWNT.DLL" Alias "clGetNumSerialPorts" _
    () As Integer

    Public Declare Function CLGETSERIALPORTIDENTIFIER Lib "XCLIBWNT.DLL" Alias "clGetSerialPortIdentifier" _
    () As Integer

    Public Declare Function CLGETMANUFACTURERINFO Lib "XCLIBWNT.DLL" Alias "clGetManufacturerInfo" _
    () As Integer

    Public Declare Function CLGETSUPPORTEDBAUDRATES Lib "XCLIBWNT.DLL" Alias "clGetSupportedBaudRates" _
    () As Integer

    Public Declare Function CLSETBAUDRATE Lib "XCLIBWNT.DLL" Alias "clSetBaudRate" _
    () As Integer

    Public Declare Function PXD_SILICONVIDEO_SETAXC Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_setAxC" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd As Integer, ByVal c_agcA As Integer, ByVal c_agcB As Integer, ByVal c_rsvd2 As Integer, ByVal c_rsvd3 As Integer, ByVal c_aec As Integer, ByVal c_rsvd4 As Integer, ByVal c_rsvd5 As Integer, ByVal c_rsvd6 As Integer, ByVal c_rsvd7 As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_SETEXPOSUREGAINOFFSET Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_setExposureGainOffset" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd As Integer, ByVal c_exposure As Double, ByVal c_gainA As Double, ByVal c_offsetA As Double, ByVal c_gainB As Double, ByVal c_offsetB As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_SETEXPOSURECOLORGAINOFFSETS Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_setExposureColorGainOffsets" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd As Integer, ByVal c_exposure As Double, ByRef c_gainsA As Double, ByRef c_gainsB As Double, ByRef c_offsetsA As Double, ByRef c_offsetsB As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_SETEXPOSURE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_setExposure" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd As Integer, ByVal c_exposure As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_SETVIDEOANDTRIGGERMODE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_setVideoAndTriggerMode" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd As Integer, ByVal c_videomode As Integer, ByVal c_controlledmode As Integer, ByVal c_controlledtrigger As Integer, ByVal c_rsvd1 As Integer, ByVal c_rsvd2 As Integer, _
     ByVal c_rsvd3 As Integer, ByVal c_rsvd4 As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_SETRESOLUTIONANDTIMING Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_setResolutionAndTiming" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd1 As Integer, ByVal c_subsample As Integer, ByVal c_aoileft As Integer, ByVal c_aoitop As Integer, ByVal c_aoiwidth As Integer, ByVal c_aoiheight As Integer, _
     ByVal c_scandirection As Integer, ByVal c_bitdepth As Integer, ByVal c_rsvd3 As Integer, ByVal c_rsvd4 As Integer, ByVal c_pixelClkFreq As Double, ByVal c_framePeriod As Double, _
     ByVal c_rsvd5 As Double, ByVal c_rsvd6 As Double, ByVal c_rsvd7 As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_SETCTRLRATES Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_setCtrlRates" _
    (ByVal c_unitmap As Integer, ByVal c_rsvd As Integer, ByVal c_rsvd1 As Double, ByVal c_framerate As Double, ByVal c_rsvd2 As Double, ByVal c_rsvd3 As Double, ByVal c_rsvd4 As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETEXPOSURE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getExposure" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SILICONVIDEO_GETAOITOP Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getAoiTop" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETAOILEFT Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getAoiLeft" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETAOIWIDTH Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getAoiWidth" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETAOIHEIGHT Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getAoiHeight" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETPIXELCLOCK Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getPixelClock" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SILICONVIDEO_GETFRAMEPERIOD Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getFramePeriod" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SILICONVIDEO_GETSUBSAMPLE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getSubsample" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETSCANDIRECTION Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getScanDirection" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETVIDEOMODE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getVideoMode" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETCTRLFRAMERATE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getCtrlFrameRate" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SILICONVIDEO_GETCTRLVIDEOMODE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getCtrlVideoMode" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETCTRLTRIGGERMODE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getCtrlTriggerMode" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETGAINA Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getGainA" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SILICONVIDEO_GETGAINB Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getGainB" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SILICONVIDEO_GETGAINSA Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getGainsA" _
    (ByVal c_unitmap As Integer, ByRef c_gainsA As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETGAINSB Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getGainsB" _
    (ByVal c_unitmap As Integer, ByRef c_gainsB As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETOFFSETSA Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getOffsetsA" _
    (ByVal c_unitmap As Integer, ByRef c_offsetsA As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETOFFSETSB Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getOffsetsB" _
    (ByVal c_unitmap As Integer, ByRef c_offsetsB As Double) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETOFFSETA Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getOffsetA" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SILICONVIDEO_GETOFFSETB Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getOffsetB" _
    (ByVal c_unitmap As Integer) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXEXPOSURE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxExposure" _
    (ByVal c_unitmap As Integer, ByVal c_exposure As Double) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXFRAMEPERIOD Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxFramePeriod" _
    (ByVal c_unitmap As Integer, ByVal c_framePeriod As Double) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXCTRLFRAMERATE Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxCtrlFrameRate" _
    (ByVal c_unitmap As Integer, ByVal c_frameRate As Double) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXPIXELCLOCK Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxPixelClock" _
    (ByVal c_unitmap As Integer, ByVal c_pixelClkFreq As Double) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXGAINA Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxGainA" _
    (ByVal c_unitmap As Integer, ByVal c_gain As Double) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXGAINB Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxGainB" _
    (ByVal c_unitmap As Integer, ByVal c_gain As Double) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXOFFSETA Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxOffsetA" _
    (ByVal c_unitmap As Integer, ByVal c_offset As Double) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXOFFSETB Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxOffsetB" _
    (ByVal c_unitmap As Integer, ByVal c_offset As Double) As Double

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXAOIWIDTH Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxAoiWidth" _
    (ByVal c_unitmap As Integer, ByVal c_width As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXAOIHEIGHT Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxAoiHeight" _
    (ByVal c_unitmap As Integer, ByVal c_height As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETAGCA Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getAgcA" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETAGCB Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getAgcB" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETAEC Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getAec" _
    (ByVal c_unitmap As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXAGCA Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxAgcA" _
    (ByVal c_unitmap As Integer, ByVal c_agc As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXAGCB Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxAgcB" _
    (ByVal c_unitmap As Integer, ByVal c_agc As Integer) As Integer

    Public Declare Function PXD_SILICONVIDEO_GETMINMAXAEC Lib "XCLIBWNT.DLL" Alias "pxd_SILICONVIDEO_getMinMaxAec" _
    (ByVal c_unitmap As Integer, ByVal c_aec As Integer) As Integer

    Public Declare Function PXD_XCLIBESCAPED Lib "XCLIBWNT.DLL" Alias "pxd_xclibEscaped" _
    (ByVal c_rsvd1 As Integer, ByVal c_rsvd2 As Integer, ByVal c_rsvd3 As Integer) As Integer
End Module
