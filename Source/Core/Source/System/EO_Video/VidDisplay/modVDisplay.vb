Option Strict Off
Option Explicit On

Imports Microsoft.VisualBasic

''' <summary>
'''    DME Corporation                                             
'''                                                                
'''    Nomenclature:   Module "VDisplay" : VidDisplay              
'''    Written By:     Robert Giumarra                             
'''    Date:           12/13/2007                                  
'''    Version:        1.0                                         
'''    Purpose:                                                    
'''    To provide the user a with a container to view images from  
'''    the Video Capture Card. This application is an integral     
'''    part of the Video Capture NAM (VidCapNAM.exe).                 
'''    This program can also be used from the DOS prompt by        
'''    passing "Type, Mode, and Operator Instructions" as          
'''    arguments. In order to facilitate testing if no Arguments   
'''    are passed, (RS-170, Single, Description) are used.         
'''    Arguments are passed from an ATLAS project to the NAM,      
'''    the NAM in turn formats the arguments and passes them       
'''    to this application via command line arguments.             
''' </summary>
''' <remarks>
''' Argument 1: Camera Configuration File name w/o extension.      
'''                        [ RS-170 | RS-343A ]                    
'''            (This will allow the user to create additional      
'''             configurations via the IFC Camera                  
'''             Configuration Utility.) These configuration files  
'''             are stored in "C:\USR\TYX\BIN\VidConfig\".         
'''    If the specified configuration file fails to load, IFC      
'''    will load a generic Rs-170 config file.                     
'''    Note:                                                       
'''      Configuration files RS-170.txt (640x480) and RS-343A.txt  
'''      (808x808) are the only configurations tested and are      
'''      shipped with the project.                                 
''' </remarks>
''' Argument 2: Video Display Mode.                                
'''                        [ CONTINUOUS | SINGLE ]                 
'''             Continuous Mode will provide a display whereby     
'''             the display is continuously acquiring the image.   
'''             SINGLE Mode will provide a display whereby a       
'''             single snapshot or acquisition is displayed.       
'''             While in Single Acquisition Mode a [Reacquire]     
'''             button will be available to snap image again.      
''' Argument 3: Operator Instructions.                             
'''                  [ A String of up to 256 characters ]          
'''             Provides the ATLAS Programmer a container to       
'''             transmit any Instructions to the Operator          
'''             while viewing the displayed image.                 
'''    If no Instructions are passed, the Operator Instructions    
'''    text box will not be displayed. If Instructions are passed, 
'''    the text will appear below the Display area. All controls   
'''    are positioned dynamically when the form is launched. The   
'''    Form size is determined by the Video Image resolution       
'''    information contained in the Configuration file. If the     
'''    vertical value is less 600, the form size will be           
'''    determined by an x15 multiplier, otherwise, an x10          
'''    multiplier will be used.                                    
'''    Error result code is routed to the NAM via an INI file      
'''    "VidErr.err" which is created and updated with the current  
'''    IFC Error Code.                                            
'''    The Forms and Controls Appearance has been checked in a     
'''    Win2K environment and the BackColor has been changed to     
'''    "H00C0C0C0" where applicable. All Labels BackStyle has    
'''    changed to "Transparent".                                   
Module VDisplay

#Region "API Declarations"
    '********************************************************************
    '***                    API Declarations                          ***
    '********************************************************************
    Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String,
                                                                                                         ByVal lpKeyName As String,
                                                                                                         ByVal lpString As String,
                                                                                                         ByVal lpFileName As String) As Integer
    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String,
                                                                                                     ByVal lpKeyName As String,
                                                                                                     ByVal lpDefault As String,
                                                                                                     ByVal lpReturnedString As String,
                                                                                                     ByVal nSize As Integer,
                                                                                                     ByVal lpFileName As String) As Integer
#End Region ' "API Declarations"

#Region "Public Members"

    Public ATS_INI As String
    Public IrwinDir As String

    Public goFrmMain As frmMain
    Public goFrmAbout As frmAbout

    Public Const OFFSET As Short = 88  'Was 1320 'Command Button Offset for Main Form Resize Event
    Public Const NUMRINGFRAMES As Short = 4

    Public gbPrStop As Boolean 'Tells processing loop to exit
    Public gbGrabActive As Boolean 'If True Grab is active
    Public gshImageDX As Short 'Image X Size Var
    Public gshImageDY As Short 'Image y Size Var
    Public gsInstructions As String 'Operator Instructions passed from NAM
    Public gbInstructions As Boolean 'Flag to indicate Operator Instructions have been passed
    Public gshScreenHeight As Short 'Screen Height
    Public gbLoadInstructions As Boolean 'Flag to supress change and click event proccessing
    Public gsBoardName As String ' Holds board names
    Public gshCaptureBoards As Short '
    Public giCapMod As Integer 'CapMod Pointers

    'ARRAYS
    Public garriITICam(16) As Integer 'Main Camera Pointers
    Public garrbHostFrame(0, 0) As Byte ' this is the default size

    Public gshSrcBytesPP As Short
    Public gshCam As Short
    Public gsMode As String 'Video Capture Mode passed from NAM (SINGLE | CONTINUOUS)

    'EPIX XCLIB Members
    Public PIXCI_FORMAT As String = "RS-170" '"default"        '// NSTC S-Video on input 1

    'Public FORMATFILE As String = "Video 1280x720p 60Hz RBG.fmt"
    'Public FORMATFILE As String = "xcvidset.fmt"

    ' For PIXCI(R) A310
    ' FORMAT = "default"        // as preset in board's eeprom
    ' FORMAT = "RS-170"         // RS-170
    ' FORMAT = "CCIR"           // CCIR
    ' FORMAT = "Video 720x480i 60Hz"        // RS-170
    ' FORMAT = "Video 720x480i 60Hz RGB"
    ' FORMAT = "Video 720x576i 50Hz"        // CCIR
    ' FORMAT = "Video 720x576i 50Hz RGB"
    ' FORMAT = "Video 640x480i 60Hz"        // RS-170
    ' FORMAT = "Video 640x480i 60Hz RGB"
    ' FORMAT = "Video 768x576i 50Hz"        // CCIR
    ' FORMAT = "Video 768x576i 50Hz RGB"
    ' FORMAT = "Video 1920x1080i 60Hz"
    ' FORMAT = "Video 1920x1080i 60Hz RGB"
    ' FORMAT = "Video 1920x1080i 50Hz"
    ' FORMAT = "Video 1920x1080i 50Hz RGB"
    ' FORMAT = "Video 1280x720p 50Hz"
    ' FORMAT = "Video 1280x720p 50Hz RGB"
    ' FORMAT = "Video 1280x720p 60Hz"
    ' FORMAT = "Video 1280x720p 60Hz RGB"
    ' FORMAT = "SVGA 800x600 60Hz RGB"
    ' FORMAT = "SXGA 1280x1024 60Hz RGB"
    ' FORMAT = "VGA 640x480 60Hz RGB"
    ' FORMAT = "XGA 1024x768 60Hz RGB"
    ' FORMAT = "RS343 875i 60Hz"
    ' FORMAT = "RS343 875i 60Hz RGB"

    Public gpixciCamera As Pixci

#End Region '"Public Members"

#Region "Private Members"

    'Configuration file extension
    'Working Video Format File: "Video 720x480i 60HZ (RS-170).fmt"
    Private Const CONFIG_EXT As String = ".fmt"

    Private Const ERRORFILE As String = "C:\USR\TYX\BIN\VidErr.err" 'Error INI file to pass error code to NAM

    '-----------------Delay Constants-------------------------------------
    Private Const SECS_IN_DAY As Integer = 86400

    Private mbZoomAutoSize As Boolean 'If True size image to window
    Private mshICPCIBoards As Short 'Number of IC-PCI Boards
    Private mshCurrentBoard As Short '0 to iICPCI_Boards, Current Board = 0

    Private miCamSource As Integer 'Camera Source ID
    Private miHostSource As Integer 'Host source ID
    Private miDisplaySink As Integer 'Display Sink ID
    Private miCamConn As Integer 'Camera Connection ID
    Private miHostConn As Integer 'Host Connection ID                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           

    'Command line Argument Values
    Private msArgs() As String
    'Example Command Line Values
    'msArgs(0) = "RS-170"
    'msArgs(1) = "CONTINUOUS" '"Single"
    'msArgs(2) = "Testing an RS-170 Signal, Single Mode."

    Private msQuote As String 'Quote Character
    Private msCNFFile As String 'Video Configuration File to load
    Private miCurrErrorCode As Integer 'Current Error Code

#End Region '"Private Members"

#Region "Public Methods"
    Public Sub Main()

        On Error GoTo MainErrorHandler 'Trap Errors

        goFrmAbout = New frmAbout()

        ATS_INI = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        IrwinDir = GatherIniFileInformation(ATS_INI, "SAIS", "IRWIN")
        If IrwinDir.ToUpper().EndsWith(".EXE") Then
            IrwinDir = IrwinDir.Substring(0, IrwinDir.LastIndexOf("\") + 1)
        End If
        If Not IrwinDir.EndsWith("\") Then
            IrwinDir = IrwinDir & "\"
        End If

        'In case this example was run before and aborted before completion, the XCLIB may not have been
        'closed, and the open would fail
        PXD_PIXCICLOSE()

        'Links the timer to the screen update function
        AddHandler goFrmMain.Timer1.Tick, AddressOf OnTimerEvent

        msQuote = Chr(34) 'Define ASCII character "

        'For testing and debugging w/o command line arguments
        'If Len(Trim(VB.Command())) = 0 Then
        If Environment.GetCommandLineArgs().Length = 1 Then
            ReDim msArgs(4)
            msArgs(0) = "Me"
            msArgs(1) = "RS-170"
            msArgs(2) = "CONTINUOUS" '"Single"
            msArgs(3) = "Testing an RS-170 Signal, Single Mode."
        Else
            GetCommandLine() 'Assign Arguments to variables from Command Line Args passed.
        End If

        If Not VerifyMode(msArgs(2).Trim) Then
            miCurrErrorCode = -102 'Invalid Type Passed
            SetErrorKey()
            End
        End If

        'If there is a "\" character in Argument, don't build, Use passed Drive\Path\Filename
        If InStr(msArgs(1), "\") Then
            msCNFFile = msArgs(1)
        Else
            'Build configuration file path and extention from passed argument
            '(PATH = "C:\USR\TYX\BIN\VidConfig\") + (sArg(1)= Passed File Name) + (EXT = ".fmt")
            msCNFFile = IrwinDir & "Irwd" & msArgs(1) & CONFIG_EXT
        End If

        'Ensure File Exists
        If Not IO.File.Exists(msCNFFile) Then
            MsgBox(String.Format("Indicated Video Format File Does Not Exist: '{0}'", msCNFFile), MsgBoxStyle.Critical, "Invalid Video Format Filepath")
            End
        End If

        gsMode = msArgs(2).Trim
        gsMode = UCase(gsMode)

        'Set Mode to either SNAP or GRAB
        If gsMode.ToUpper() = "CONTINUOUS" Then
            gbGrabActive = True
        Else
            gbGrabActive = False
        End If

        If msArgs.Length = 4 Then
            gsInstructions = msArgs(3)
            gbInstructions = True
        Else
            gbInstructions = False
        End If

        'All Arguments passed tests, Launch About Form
        goFrmAbout.Show()
        Delay(0.4)
        goFrmAbout.BringToFront()
        goFrmAbout.Refresh()

        'Set Default Values
        gbPrStop = True
        mbZoomAutoSize = True
        mshCurrentBoard = 0
        miCamSource = 0
        miHostSource = 0
        'gsinkkindCurrentMode = IFC_DIB_SINK
        miDisplaySink = 0
        miCamConn = 0
        miHostConn = 0

        'Initialize Video Capture Card
        If Not Init_Pixci() Then
            goFrmAbout.Close()
            goFrmMain.Close()
            SetErrorKey()
            MsgBox("Can't Initialize the Capture Board using EPIX!")
            End 'Stop Program here
        End If

        Exit Sub

MainErrorHandler:
        MsgBox("Error Number: " & Err.Number & "   Description: " & Err.Description)     'For Debugging only
        Resume Next
    End Sub

    Public Function Init_Pixci() As Boolean
        Dim bRetVal As Boolean = False

        gpixciCamera = New Pixci()
        gpixciCamera.format = PIXCI_FORMAT
        gpixciCamera.formatfile = msCNFFile ' using format file saved by XCAP

        goFrmMain.tsStatusLabel.Text = "OPENING PIXCI(R) FRAME GRABBER"
        Dim iErr As Integer = PXD_PIXCIOPEN("", gpixciCamera.format, gpixciCamera.formatfile)
        Delay(1)
        If iErr < 0 Then
            MsgBox("THE OPEN FAILED")
            Call PXD_MESGFAULT(1)
            Debug.Print("PXD_PIXCIOPEN:" & PXD_MESGERRORCODE(iErr))
            End
        End If

        gpixciCamera.maxWidth = PXD_IMAGEXDIM
        gpixciCamera.maxHeight = PXD_IMAGEYDIM
        gshImageDX = gpixciCamera.maxWidth
        gshImageDY = gpixciCamera.maxHeight

        goFrmMain.tsStatusLabel.Text = "Camera Resolution is " & gshImageDX.ToString & " x " & gshImageDY.ToString

        If PXD_IMAGECDIM.Equals(3) Then
            gpixciCamera.is_color = True
            goFrmMain.tsStatusLabel.Text = "Color Camera"
        End If

        bRetVal = True

        Return bRetVal
    End Function

    Public Function GatherIniFileInformation(ByVal sINIPath As String, ByVal sSection As String, ByVal sKey As String) As String
        Dim iBytesRead As Integer
        Dim sKeyValue As String = vbNullString
        Dim lpString As String

        GatherIniFileInformation = vbNullString

        Try
            lpString = Space(4000)

            iBytesRead = GetPrivateProfileString(sSection, sKey, "", lpString, lpString.Length, sINIPath)
            sKeyValue = Strings.Left(lpString, iBytesRead)
            Return sKeyValue.Trim()
        Catch ex As Exception
            MsgBox(String.Format("Unexpected Error in GatherIniFileInformation: Error Number = '{0}'; Description = '{1}'", Err.Number, Err.Description), MsgBoxStyle.Critical, "Unexpected Error")
        End Try
    End Function

    Public Sub OnTimerEvent(ByVal [source] As Object, ByVal e As EventArgs)
        Static Dim LastCapturedField As Integer

        'If we are live and the captured image has changed then update the counter and redraw the image window
        If gpixciCamera.is_live And Not LastCapturedField.Equals(PXD_CAPTUREDFIELDCOUNT(1)) Then
            LastCapturedField = PXD_CAPTUREDFIELDCOUNT(1)
            goFrmMain.pboxDisplay.Invalidate() 'Causes PictureBox to redraw
            goFrmMain.pboxDisplay.Update()
        End If
    End Sub

    Public Sub SetErrorKey()
        'Write the IFC Error code to the VidErr.err file.
        Dim lResp As Integer 'Return Code
        'This API will create file, Heading, Key and Value if it does not exist,
        'otherwise it will update Key value.
        lResp = WritePrivateProfileString("NAMError", "ErrCode", CStr(miCurrErrorCode), ERRORFILE)
        'If there is an Error in creating Error key, notify user.
        If lResp = 0 Then
            'MsgBox "Error writting to the ini file." & vbCrLf & "Error return will be invalid."    'For Debugging only
        End If
    End Sub

    ''' <summary>
    ''' Will have to allow for Resizing based on Command Line Parameter for Resolution and Config File for a given resolution
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SizeForm()
        Dim dHeight As Double = 0.0
        Dim dWidth As Double = 0.0
        Dim dmaxDispHolderHeight As Double = 0.0

        If goFrmMain.WindowState <> System.Windows.Forms.FormWindowState.Minimized Then
            'If no image Height and Width, set to 640 X 480
            If IsNothing(gpixciCamera) OrElse gpixciCamera.maxWidth = 0 Then
                MsgBox("No Video Format specified.  Display will be set to 640 X 480", MsgBoxStyle.Exclamation)
                gpixciCamera.maxWidth = 640
                gpixciCamera.maxHeight = 480
            End If

            With goFrmMain
                ' Adjust the window size according to the image but limited by the actual available screen area
                If (gpixciCamera.maxWidth + 16) > Screen.PrimaryScreen.WorkingArea.Width Then
                    dWidth = Screen.PrimaryScreen.WorkingArea.Width
                Else
                    dWidth = gpixciCamera.maxWidth + 16
                End If

                If gbInstructions Then
                    If (gpixciCamera.maxHeight + .txtInstructions.Height + .cmdClose.Height + .StatusStrip1.Height + 80) > Screen.PrimaryScreen.WorkingArea.Height Then
                        dHeight = Screen.PrimaryScreen.WorkingArea.Height
                    Else
                        dHeight = gpixciCamera.maxHeight + .txtInstructions.Height + .cmdClose.Height + .StatusStrip1.Height + 80
                    End If
                Else
                    If (gpixciCamera.maxHeight + .StatusStrip1.Height + .txtInstructions.Height + 60) > Screen.PrimaryScreen.WorkingArea.Height Then
                        dHeight = Screen.PrimaryScreen.WorkingArea.Height
                    Else
                        dHeight = gpixciCamera.maxHeight + .StatusStrip1.Height + .cmdClose.Height + .txtInstructions.Height + 40
                    End If
                End If

                .Size = New Size(dWidth, dHeight)

                'Set the size of the pnlScrollBox based on the window size and the visible controls
                dWidth = .Width - 16

                If gbInstructions Then
                    If (gpixciCamera.maxHeight + .txtInstructions.Height + .cmdClose.Height + .StatusStrip1.Height + 40 + 39) > .Height Then
                        dHeight = .Height - .txtInstructions.Height - .cmdClose.Height - .StatusStrip1.Height - 40 - 39  ' 39 = height of Window title bar (31) and bottom border
                    Else
                        dHeight = gpixciCamera.maxHeight
                    End If
                Else
                    If (gpixciCamera.maxHeight + .cmdClose.Height + .StatusStrip1.Height + 79) > .Height Then
                        dHeight = .Height - .cmdClose.Height - .StatusStrip1.Height - 40 - 39  ' 39 = height of Window title bar (31) and bottom border
                    Else
                        dHeight = gpixciCamera.maxHeight
                    End If
                End If
                .pnlDisplay.Location = New Point(0, 0)
                .pnlDisplay.Size = New Size(dWidth, dHeight)

                'ALWAYS Set Picturebox Size = Actual Size, Panel will have Scroll Bars as applicable
                .pboxDisplay.Location = New Point(0, 0)
                .pboxDisplay.Size = New Size(gpixciCamera.maxWidth, gpixciCamera.maxHeight)

                'Reposition the buttons and textbox where applicable
                .cmdClose.Location = New Point(.Width - .cmdClose.Width - 28, .Height - .cmdClose.Height - .StatusStrip1.Height - 49)
                .cmdAbout.Location = New Point(.cmdClose.Left - .cmdAbout.Width - 20, .cmdClose.Top)
                If gbGrabActive = False Then ' If in Single Acquisition Mode
                    .cmdAcquire.Visible = True
                    .cmdAcquire.Location = New Point(.cmdAbout.Left - .cmdAcquire.Width - 20, .cmdClose.Top)
                Else
                    .cmdAcquire.Visible = False
                End If
                If gbInstructions Then
                    .txtInstructions.Visible = True 'Make Text Box Visible
                    .txtInstructions.Text = gsInstructions 'Fill with Passed Instructions
                    .txtInstructions.Size = New Size(.pnlDisplay.Width - 10, 55)
                    .txtInstructions.Location = New Point(.pnlDisplay.Left + 5, .cmdClose.Top - .txtInstructions.Height - 20)
                Else
                    .txtInstructions.Visible = False
                End If
            End With
        End If
    End Sub

    ''' <summary>
    ''' Grab is Live (Continuous) Vs Snapshot
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GrabLive_Pixci() As Short

        gpixciCamera.maxWidth = PXD_IMAGEXDIM
        gpixciCamera.maxHeight = PXD_IMAGEYDIM

        'Do we need to see if we are already Live or can this call handle it either way?
        If Not gpixciCamera.is_live Then
            PXD_GOLIVE(1, 1)
            gpixciCamera.is_live = True
        End If

        goFrmMain.pboxDisplay.Invalidate() ' Redraw PictureBox1
        goFrmMain.pboxDisplay.Update()
        goFrmMain.pboxDisplay.BringToFront()

        goFrmMain.tsStatusLabel.Text = "CONTINUOUS"
        goFrmMain.StatusStrip1.Refresh()
    End Function

    ''' <summary>
    ''' Snap is a Snapshot vs Grab (Live)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Snapshot_Pixci() As Short

        PXD_GOUNLIVE(1)
        gpixciCamera.is_live = False
        goFrmMain.pboxDisplay.Invalidate() ' Redraw PictureBox1
        goFrmMain.pboxDisplay.Update()

        PXD_DOSNAP(1, 1, 0)

        goFrmMain.pboxDisplay.Invalidate() ' Redraw PictureBox1
        goFrmMain.pboxDisplay.Update()

        goFrmMain.tsStatusLabel.Text = "SNAPSHOT"
        goFrmMain.StatusStrip1.Refresh()
    End Function

    Public Sub Delay(ByRef dSeconds As Double)
        'DESCRIPTION:
        '   Delays for a specified number of seconds.
        '   50 milli-second resolution.
        '   Minimum delay is about 50 milli-seconds.
        'PARAMETERS:
        '   dSeconds: The number of seconds to Delay.
        Dim t As Double

        t = dGetTime()
        dSeconds = dSeconds / SECS_IN_DAY
        Do
            Application.DoEvents()
        Loop While dGetTime() - t < dSeconds
    End Sub

#End Region '"Public Methods"

#Region "Private Methods"

    ''' <summary>
    ''' Get Command Line Arguments (If Any)
    ''' </summary>
    ''' <remarks>
    ''' Example:
    ''' msArgs(0) = "C:\Projects\ENG_SW_CIC\trunk\Source\Core\Source\System\EO_Video\VidDisplay\bin\x86\Debug\VidDisplay.vshost.exe"
    ''' msArgs(1) = "RS-170"
    ''' msArgs(2) = "CONTINUOUS"
    ''' msArgs(3) = "RELAX AND ENJOY YOUR FINE VIDEO"
    ''' </remarks>
    Private Sub GetCommandLine()
        'Get command line arguments.
        msArgs = Environment.GetCommandLineArgs()
    End Sub

    ''' <summary>
    ''' Determines the time since 1900.
    ''' Avoids errors caused by 'Timer' when crossing midnight.
    ''' </summary>
    ''' <returns>
    '''    A double-precision floating-point number in the format d.s where:
    '''    d = the number of days since 1900 and
    '''    s = the fraction of today with 1 second resolution.
    '''    Lng(Now) returns only the number of days (the "d." part).
    '''    Timer returns the number of seconds since midnight with 10 mSec resoulution.
    '''    So, the following statement returns a composite double-float number representing
    '''    the time since 1900 with 10 mSec resoultion. (no cross-over midnight bug)
    ''' </returns>
    ''' <remarks>
    ''' The CDbl(Now) function returns a number formatted d.s where:
    ''' s = the fraction of a day with 10 milli-second resolution
    ''' </remarks>
    Private Function dGetTime() As Double
        dGetTime = CLng(DateSerial(Year(Now), Month(Now), Day(Now)).ToOADate) + (Timer() / SECS_IN_DAY)
    End Function

    Private Function VerifyMode(ByRef sArgMode As String) As Boolean
        If UCase(sArgMode) = "CONTINUOUS" Or UCase(sArgMode) = "SINGLE" Then
            VerifyMode = True
        Else
            VerifyMode = False
        End If
    End Function

#End Region '"Private Methods"

End Module