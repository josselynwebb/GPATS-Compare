Option Strict Off
Option Explicit On

Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Configuration
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports Microsoft.Win32

''' <summary>
'''---------------------------------------------------------------------
'''        -- VB CRASH WARNING WHEN RUNNING IN DEBUG MODE!! --        --
'''---------------------------------------------------------------------
'''  When running this project in Debug mode,                         --
'''                        -- DO NOT --                               --
'''  END the program through the interface, use the [End] Button      --
'''  on the Debug toolbar Only!!                                    --
'''  Failure to follow these guidelines may cause the Visual Basic    --
'''  Development Environment to Crash, resulting in the loss of any   --
'''  unsaved changes.                                                 --
''' </summary>
''' <remarks>
'''*                                                                  **
'''*    Nomenclature:   Module VDisplay (Main.bas) : VidTool        **
'''*    Purpose:                                                      **
'''*     To provide the user with a container to view images from     **
'''*     the Video Capture Card (VCC) both in continuous acquisition  **
'''*     or single snapshot. There are two modes of operation, SAIS   **
'''*     mode and NAM mode.                                           **
'''*     Functionality includes the ability to control EO Assets      **
'''*     (IR, VIS, and ALTA) from a self contained Asset Control      **
'''*     Panel, a target alignment overlay, a Field-of-View (FoV)     **
'''*     calculator, and the ability to position a movable and        **
'''*     resizable Region-of-Interest (RoI) box which reports         **
'''*     selected coordinates as well as Height and Width             **
'''*     attributes. An integral Video configuration utility          **
'''*     allows the user to select configuration files from           **
'''*     IRWindows as well as the ability to point to a file in an    **
'''*     alternate location (Default = RS170_640X480.txt). To       **
'''*     prevent a control conflict, if an open instance of           **
'''*     IRWindows is detected, this application will not execute.    **
'''*                                                                  **
'''*     NAM Mode:                                                    **
'''*        This application is an integral part of the Video         **
'''*            Display NAM (VidDisplayNAM.exe).                      **
'''*     Depending on the intended NAM mode, some or all of the       **
'''*     following arguments will be passed in the listed order.      **
'''*     The Video.dat file is used to pass such data as the Help   **
'''*     Text, Operators Instructions, and return Error Information   **
'''*     and Coordinates if indicated by mode.                        **
'''*     Arg 1: (MODE)        FOV | TGTCOORD | ALIGN | DISPLAY_ONLY   **
'''*     Arg 2: (TYPE)        Video Configuration File                **
'''*     Arg 3: (HelpFile)   Help Graphic File to display             **
'''*     Arg 4: (TargDimX)   Horizontal Target Dimension (nRads)      **
'''*     Arg 5: (TargDimY)   Vertical Target Dimension   (mRads)      **
'''*------------------------------------------------------------------**
''' </remarks>
Module VDisplay

#Region "Common Interface Control Layer API"

    Public Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal ProcUuid As String) As Short
    Public Declare Function atxml_Close Lib "AtXmlApi.dll" () As Integer
    Public Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer

    Public Declare Function atxmlDF_viWrite Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal InstrumentHandle As Short, ByVal InstrumentCmds As String, ByVal BufferSize As Integer, ByRef ActWriteLen As Integer) As Integer
    Public Declare Function atxmlDF_viRead Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal InstrumentHandle As Short, ByVal ReadBuffer As String, ByVal BufferSize As Integer, ByRef ActReadLen As Integer) As Integer

#End Region '"Common Interface Control Layer API"

#Region "Windows APIs"

    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As Integer) As Integer
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer

#End Region '"Windows APIs"

    Public ATS_INI As String

    Public Const GUID As String = "{EO-VID-CAPTURE}"
    Public Const HELP_TEXT As String = "HelpText"
    Public Const PROCTYPE As String = "SFP"

    Public Const VID_FMT_EXT As String = ".FMT"

    'gshMode
    Public Const FOV As Short = 0
    Public Const TGTCOORD As Short = 1
    Public Const ALIGN As Short = 2
    Public Const DISPLAY_ONLY As Short = 3
    Public Const STRETCH_DELETESCANS As Integer = 3 'Constant used in calling SetStrechBltMode

    'frmMain Laser Tab Settings
    Public Const AMPL As Short = 0                  'Amplitude Control(s)
    Public Const PULPER As Short = 1                'Pulse Period Control(s)
    Public Const LEL As Short = 2                   'Elevation Control(s)
    Public Const LAZ As Short = 3                   'LARRS Azimuth Control(s)
    Public Const LPOL As Short = 4                  'LARRS Polarization Control(s)

    Public PIXCI_FORMAT As String = "RS-170" 'VB6 Value        '// NSTC S-Video on input 1

    Public gofrmMain As frmMain                     'MAIN FORM!!
    Public gofrmAbout As frmAbout                'ABOUT FORM

    Public gshMode As Short 'Mode index

    Public gsStartX As String                       'ROI Box Starting X Point
    Public gsStartY As String                       'ROI Box Starting Y Point
    Public gsEndX As String                         'ROI Box Ending X Point
    Public gsEndY As String                         'ROI Box Ending Y Point
    Public gsGraphicFile As String                  'Graphic File Name
    Public gsGraphicText As String                  'Text String to display in the Graphics Text Box
    Public gsInstructions As String                 'Operator Instructions passed from NAM

    Public gsVidFormatFile As String                'Camera Configuration File String
    Public gsXCAPDocsDir As String
    Public gsXCAPCaptureDir As String = "C:\usr\tyx\bin\"

    'Optional Command Line Arguments
    Public gsTargDimX As String                     'Target Width Dimension
    Public gsTargDimY As String                     'Target Height Dimension

    Public IrwinDir As String

    Public gshVEO2PowerOn As Short

    'ARRAYS

    Public nUpperLimit(5) As Integer            'Upper Limit for Setting ALTA Values
    Public nLowerLimit(5) As Integer            'Lower Limit for Setting ALTA Values
    Public nSetDefault(5) As Integer            'Default Limit for Setting ALTA Values
    Public garrshAzPos(2) As Short                  'LARRS Azimuth Position
    Public garrshElPos(2) As Short                  'LARRS Elevation Position

    'EO Asset Setting Array Idenifiers
    Public garrsPsResourceName(9) As String         'Power Supply Resource Names ["DCPS_1" - "DCPS_10"]
    Public garrsLaserDiode(2) As String             'Laser Diode Identifiers
    Public garrsLaserTrigger(2) As String           'Laser Trigger Identifiers

    Public gbCapture As Boolean                     'Flag: Image Captured
    Public gbCaptureImage As Boolean                'Flag to signal Save Image
    Public gbEoPowerOnVIPERT As Boolean             'Flag: EO Power applied using PPU 7,8,9
    Public gbEOPoweredAtStartup As Boolean          'Flag: EO Power aapplied when opening Panel
    Public gbGraphicOpen As Boolean                 'Flag: Graphic Open
    Public gbGraphicText As Boolean                 'Flag: Help Text was passed from ATLAS
    Public gbHelpGraphic As Boolean                 'Flag: Help Graphic is available
    Public gbInitVideo As Boolean                   'Flag: Video has been Initialized
    Public gbInstructions As Boolean                'Flag: Instruction String passed from ATLAS
    Public gbLaunchNoPower As Boolean               'Flag: Launch the display form without power
    Public gbMainShown As Boolean                   'Flag: Main Form is displayed
    Public gbMonitorsrc As Boolean                  'Flag: Monitor source positions
    Public gbStartedWithPower As Boolean            'Flag: Leave Power on when exiting application
    Public gbOverlay As Boolean                     'Flag: Overlay Image with crosshair
    Public gbROIFormOpen As Boolean                 'Flag: ROI Form is Open
    Public gbRunFromNAM As Boolean                  'Flag: Run from NAM
    Public gbUpdateLARRS As Boolean                 'Flag: Update Laser Settings
    Public gbUpdateIR As Boolean                    'Flag: Update Delta T Temperature
    Public gbUpdateVIS As Boolean                   'Flag: Update Radiance Value

    Public gctlViperTParse As New VIPERT_Common_Controls.Common

    Public gpixciCamera As Pixci

    'Global because frmMain uses this as well as modVDisplay
    Public gbIsInitializing As Boolean = True 'Set to False after Form Loads to prevent Events being called during Initialization

    Public gofrmROI As frmROI

    Public WithEvents goTimer As Timer

    Public gbEoPowerOnRemote As Boolean 'Flag: EO Power applied via other sources than PPU 7,8,9

    Public gbMainClosing As Boolean = False     'Set to True in frmROI when exiting application so when frmMain.chkLiveDisplay_CheckedChanged fires it knows we are closing the application

    Private Const NAMEERRROR As String = "NAMError"
    Private Const LARRSFILE As String = "C:\Users\Public\Documents\ATS\LARRS\LARRS.dat"

    Private Const DATFILE As String = "C:\usr\Tyx\bin\Video.dat"

    Private Const INSTRUCT As String = "Instructions"
    Private Const OP_INSTRUT As String = "OpInstructions"

    Private Const ERRCODE As String = "ErrCode"
    Private Const ERRSTR As String = "ErrString"
    Private Const NUMRINGFRAMES As Short = 4

    'These are used with WritePrivateProfileString (INI File)
    Private Const RETDATA As String = "Return_Data"
    Private Const STARTX As String = "X1"
    Private Const STARTY As String = "Y1"
    Private Const ENDX As String = "X2"
    Private Const ENDY As String = "Y2"
    Private Const HORZFOV As String = "Hfov"
    Private Const VERTFOV As String = "Vfov"

    Private Const CMD_BUT_OFFSET As Short = 90 'Was 1320  'Not sure if we need this yet

    Private msHorzFoV As String                     'Horizontal Field-Of-View String Value
    Private msVertFoV As String                     'Vertical Field-Of-View String Value
    Private msQuote As String                       'Quote Character

    Private marrsVisWheelStr(14) As String          'Visible Module Target Array
    Private marrsIRWheelStr(14) As String           'IR Module Target Array
    Private marrssVisTgtString(14) As String        'Visible Target Wheel String match to Index
    Private marrshVisTgtImageIndex(14) As Short     'Visible Target Image Index Setting
    Private marrshIRTgtImageIndex(14) As Short      'IR Target Image Index Setting

    Sub Main()
        Dim shNumTrys As Short
        Dim sTemp As String
        Dim shCountI As Short = 0
        Dim cmdArgs() As String = Environment.GetCommandLineArgs()

        'DEBUGGING MESSAGE BOX SO WE CAN TRY TO ATTACH TO VIDTOOL ALREADY RUNNING VIA NAM.
        'MsgBox("YO!", MsgBoxStyle.OkOnly)

        Try
            gpixciCamera = New Pixci()

            gofrmMain.Visible = False

            InitControls()

            'TEC Constants
            gofrmMain.txtSetPP.Text = "50"

            '** Align Form to top left of screen
            gofrmMain.Top = 0
            gofrmMain.Left = 0

            ATS_INI = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
            IrwinDir = GatherIniFileInformation(ATS_INI, "SAIS", "IRWIN")
            If IrwinDir.ToUpper().EndsWith(".EXE") Then
                IrwinDir = IrwinDir.Substring(0, IrwinDir.LastIndexOf("\") + 1)
            End If
            If Not IrwinDir.EndsWith("\") Then
                IrwinDir = IrwinDir & "\"
            End If

            VerifyInstrumentCommunication()

            'Initialize Variables
            gbUpdateLARRS = True
            gbUpdateIR = False
            gbUpdateVIS = False
            shNumTrys = 0

            'Leave this in the App.Config for now as it is new
            sTemp = GatherAppSettings("XCAP")
            gsXCAPDocsDir = sTemp

            'Check for an Open instance of IRWindows, Terminate if Open
            IRWinOpen()

            If cmdArgs.Length = 1 Then
                '**************************************************************************************
                '                       No Command Line Arguments - SAIS MODE
                '**************************************************************************************
                If IsNothing(gofrmAbout) Then
                    gofrmAbout = New frmAbout()
                End If

                With gofrmAbout
                    .cmdOK.Visible = False
                    .Show()
                    .BringToFront()
                    .tsStatusLabel1.Text = "Loading Video Display Tools Application...Please Wait"
                    .Refresh()
                    .Cursor = Cursors.WaitCursor
                End With

                'Configure Forms while splash screen is visible
                InitSaisModeVar() 'Initialize SAIS Mode variables

                CheckEoPower()

                gbRunFromNAM = False 'Set Flag - Running from SAIS

                If gbEoPowerOnVIPERT = False Then
                    gbStartedWithPower = False
                    PowerOnAndStartup()
                Else
                    'Launch Main Video display with a 640x480 resolution, no live video
                    gbStartedWithPower = True
                    gbLaunchNoPower = False
                    LoadVideoFormat()
                    InitAsset() 'Check to see if an EO Module is on via another power source
                End If
            Else
                '***************************************************************************************************************************
                '                                         Command Line Arguments - NAM MODE
                '***************************************************************************************************************************
                '*     Arg 1: (MODE)        FOV | TGTCOORD | ALIGN | DISPLAY_ONLY                                                         **
                '*     Arg 2: (TYPE)        Video Configuration File                                                                      **
                '*     Arg 3: (HelpFile)   Help Graphic File to display                                                                   **
                '*     Arg 4: (TargDimX)   Horizontal Target Dimension (nRads)                                                            **
                '*     Arg 5: (TargDimY)   Vertical Target Dimension   (mRads)                                                            **
                '*                                                                                                                        **
                '* Example Command Line (FOV)                                                                                             **
                '*   "FOV" "IrwdRS170_640x480.fmt" "C:\Projects\ENG_SW_CIC\Source\Core\Source\System\EO_Video\VidTool\imgHelp.bmp" 20 15  **
                '*   "FOV" "IrwdRS343_1256x944.fmt" "C:\Projects\ENG_SW_CIC\Source\Core\Source\System\EO_Video\VidTool\imgHelp.bmp" 20 15 **
                '*                                                                                                                        **
                '****************************************************************************************************************************
                gbRunFromNAM = True 'Set NAM Flag
                If Not VerifyModeSetting(cmdArgs(1).Trim) Then
                    SetErrorKey(-101, String.Format("ERROR - Invalid Mode: '{0}'", cmdArgs(1)))
                    End 'Mode not supported, close application
                End If

                'If there is a "\" character in Argument, don't build, Use passed Drive\Path\Filename
                If cmdArgs(2).Contains("\") Then
                    gsVidFormatFile = cmdArgs(2)
                Else
                    'Build configuration file path and extension from passed argument
                    If Path.GetExtension(cmdArgs(2)).ToUpper() = VID_FMT_EXT Then
                        gsVidFormatFile = IrwinDir & "Irwd" & cmdArgs(2)
                    Else
                        gsVidFormatFile = IrwinDir & "Irwd" & cmdArgs(2) & VID_FMT_EXT
                    End If
                End If

                'Make sure configuration file exists, if not, set error and close app
                If Not File.Exists(gsVidFormatFile) Then
                    MsgBox("The configuration file: " & gsVidFormatFile & " could not be found")
                    SetErrorKey(-107, String.Format("The configuration file: '{0}' could not be found", gsVidFormatFile))
                    'EXIT PROGRAM!
                    Exit Sub
                End If

                'GET APPROPRIATE COMMAND LINE ARGS
                Select Case gshMode
                    Case FOV
                        gsGraphicFile = cmdArgs(3).Trim
                        If gsGraphicFile.Length > 5 Then 'Minimum file name size "a.ext"
                            If File.Exists(gsGraphicFile) Then
                                gbHelpGraphic = True
                            Else
                                gbHelpGraphic = False 'File not found ERROR
                            End If
                        Else
                            gbHelpGraphic = False
                        End If
                        If gbHelpGraphic = True Then
                            gsGraphicText = GatherIniFileInformation(DATFILE, INSTRUCT, HELP_TEXT)
                            If gsGraphicText.Length > 0 Then
                                gbGraphicText = True
                            Else
                                gbGraphicText = False
                            End If
                        Else
                            gbGraphicText = False
                        End If

                        If cmdArgs(4).Trim.Length > 0 Then 'It contains Data
                            gsTargDimX = cmdArgs(4).Trim
                        Else
                            'ERROR          'Not Optional, Must contain Data
'                            MsgBox("First Data Argument Required If Other Command Line Args Are Utilized", MsgBoxStyle.Critical, "Invalid Command Line Arguments")
'                            Exit Sub
                        End If

                        If cmdArgs(5).Trim.Length > 0 Then 'It contains Data
                            gsTargDimY = cmdArgs(5).Trim
                        Else
                            'ERROR          'Not Optional, Must contain Data
'                            MsgBox("Second Data Argument Required If Other Command Line Args Are Utilized", MsgBoxStyle.Critical, "Invalid Command Line Arguments")
'                            Exit Sub
                        End If

                        gsInstructions = GatherIniFileInformation(DATFILE, INSTRUCT, OP_INSTRUT)
                        If gsInstructions.Length > 0 Then
                            gbInstructions = True
                        Else
                            gbInstructions = False
                        End If

                    Case TGTCOORD
                        gsGraphicFile = cmdArgs(3).Trim
                        If gsGraphicFile.Length > 5 Then 'Minimum file name size "a.ext"
                            If File.Exists(gsGraphicFile) Then
                                gbHelpGraphic = True
                            Else
                                gbHelpGraphic = False 'File not found ERROR
                            End If
                        Else
                            gbHelpGraphic = False
                        End If
                        If gbHelpGraphic = True Then
                            gsGraphicText = GatherIniFileInformation(DATFILE, INSTRUCT, HELP_TEXT)
                            If gsGraphicText.Length > 0 Then
                                gbGraphicText = True
                            Else
                                gbGraphicText = False
                            End If
                        Else
                            gbGraphicText = False
                        End If

                        gsInstructions = GatherIniFileInformation(DATFILE, INSTRUCT, OP_INSTRUT)

                        If gsInstructions.Length > 0 Then
                            gbInstructions = True
                        End If

                    Case ALIGN
                        gsGraphicFile = cmdArgs(3).Trim
                        If gsGraphicFile.Length > 5 Then 'Minimum file name size "a.ext"
                            If File.Exists(gsGraphicFile) Then
                                gbHelpGraphic = True
                            Else
                                gbHelpGraphic = False 'File not found ERROR
                            End If
                        Else
                            gbHelpGraphic = False
                        End If
                        If gbHelpGraphic = True Then
                            gsGraphicText = GatherIniFileInformation(DATFILE, INSTRUCT, HELP_TEXT)
                            If gsGraphicText.Length > 0 Then
                                gbGraphicText = True
                            Else
                                gbGraphicText = False
                            End If
                        Else
                            gbGraphicText = False
                        End If

                        gsInstructions = GatherIniFileInformation(DATFILE, INSTRUCT, OP_INSTRUT)
                        If gsInstructions.Length > 0 Then
                            gbInstructions = True
                        End If

                    Case DISPLAY_ONLY
                        gsInstructions = GatherIniFileInformation(DATFILE, INSTRUCT, OP_INSTRUT)
                        If gsInstructions.Length > 0 Then
                            gbInstructions = True
                        End If
                End Select

                'Setup Video Capture Mode
                Select Case gshMode
                    Case ALIGN, FOV, TGTCOORD
                        InitVideo()
                        gofrmMain.SizeForm()
                        gofrmMain.Visible = True    'Causes Form Load if not previously loaded
                        gofrmMain.BringToFront()
                        gofrmMain.Refresh()

                    Case DISPLAY_ONLY
                        InitVideo()
                        'May Not Be Necessary??
                        gofrmMain.SizeForm()
                        gofrmMain.Visible = True    'Causes Form Load if not previously loaded
                        gofrmMain.BringToFront()
                End Select
            End If 'SAIS Mode Else NAM Mode

        Catch ex As Exception
            MsgBox(String.Format("Unexpected Error in Sub Main: Error Number = '{0}'; Description = '{1}'", Err.Number, Err.Description), MsgBoxStyle.Critical, "Unexpected Error")
            Exit Sub
        End Try
    End Sub

    ''' <summary>
    ''' This method performs a grab operation on the video capture interface.  It is intended to be used 
    ''' in a timer event to perform periodic updates to the pboxDisplay.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DoVideoCapture()
        Static Dim LastCapturedField As Integer

        'If we are live and the captured image has changed then update the counter and redraw the image window
        If gpixciCamera.is_live And Not LastCapturedField.Equals(PXD_CAPTUREDFIELDCOUNT(1)) Then
            LastCapturedField = PXD_CAPTUREDFIELDCOUNT(1)
            gofrmMain.pboxDisplay.Invalidate() 'Causes PictureBox to redraw
        End If
    End Sub

    ''' <summary>
    ''' This method updates the video display every 10 ms for a specified 
    ''' number of seconds
    ''' </summary>
    ''' <param name="seconds">Lenth of the delay in seconds</param>
    ''' <remarks></remarks>
    Private Sub DelayWithVideoUpdate(ByVal seconds As Double)
        Dim iLoopCount As Integer = 0
        Dim iTotalLoop As Integer = Math.Round(seconds * 10)

        While iLoopCount < iTotalLoop
            Delay(0.1)
            DoVideoCapture()
            iLoopCount += 1
        End While
    End Sub

    ''' <summary>
    ''' onTimerEvent was used prior to using Sub Main as Startup
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnTimerEvent(ByVal [source] As Object, ByVal e As EventArgs)
        Dim BlackBodyTemp As Single
        Dim AmbientTemp As Single
        Dim VISOutput As Single
        Dim iSrcStgRcvd As Integer
        Dim iSensStgRcvd As Integer
        Static bPInit As Boolean = True ' Used to force an update for all tabs only the first time through the timer
        Static shVEO2Count As Short = 1 ' Used to determine when to update the VEO2 data
        Dim shCountI As Short = 0
        Dim iCurrTab As Integer = -1

        Try
            goTimer.Enabled = False

            ''Update the pboxDisplay if requested
            'If gofrmMain.chkLiveDisplay.Checked Then
            '    DoVideoCapture()
            'End If

            If (gbRunFromNAM = False) Then
                If shVEO2Count = 1 And gbEoPowerOnVIPERT Then
                    With gofrmMain
                        If .tabAsset.SelectedTab.Name = "tabpVisible" Then
                            iCurrTab = 1    'Visible
                        ElseIf .tabAsset.SelectedTab.Name = "tabpInfrared" Then
                            iCurrTab = 2    'Infrared
                        Else
                            iCurrTab = 3    'Laser
                        End If

                        'Only do this if Current Tab is "Laser"
                        If iCurrTab = 3 Then 'Laser
                            If .cboLaserTrigger.Text <> "Free Run" Then
                                .panSetPP.Visible = False
                                .lblPulsePeriod.Visible = False
                            Else
                                .panSetPP.Visible = True
                                .lblPulsePeriod.Visible = True
                            End If
                        End If
                        SET_SOURCE_STAGE_LASER_FETCH(iSrcStgRcvd)

                        Select Case iSrcStgRcvd
                            Case 1
                                .optSrc0.Checked = True
                            Case 2
                                .optSrc2.Checked = True
                            Case 3
                                .optSrc1.Checked = True
                        End Select

                        SET_SENSOR_STAGE_LASER_FETCH(iSensStgRcvd)
                        Select Case iSensStgRcvd
                            Case 1
                                .optSens0.Checked = True
                            Case 2
                                .optSens1.Checked = True
                            Case 3
                                .optSens2.Checked = True
                        End Select

                        'Only do this if Current Tab is "Infrared"
                        If iCurrTab = 2 Then 'Infrared
                            If .cboIR_TargetWheel.Text <> .txtCurrIRTargPos.Text Then
                                .txtCurrIRTargPos.BackColor = Color.Red
                            Else
                                .txtCurrIRTargPos.BackColor = Color.Lime
                            End If

                            If gbUpdateIR = True Or bPInit = True Then

                                DelayWithVideoUpdate(1.0)

                                For shCountI = 1 To 5
                                    DelayWithVideoUpdate(1.0)
                                    GET_TEMP_TARGET_IR_INITIATE()
                                    GET_TEMP_TARGET_IR_FETCH(AmbientTemp)
                                    SET_TEMP_ABSOLUTE_IR_FETCH(BlackBodyTemp)
                                    If BlackBodyTemp <> 0 Then Exit For
                                Next shCountI
                                .txtAMT_Out.Text = CStr(AmbientTemp)
                                .txtBLB_Out.Text = CStr(BlackBodyTemp)
                            End If
                        End If

                        'Update the pboxDisplay if requested
                        'If gofrmMain.chkLiveDisplay.Checked Then
                        '    DoVideoCapture()
                        'End If

                        'Only do this if Current Tab is "Visual"
                        If iCurrTab = 1 Then 'Visual
                            If .cboVisWheel.Text <> .txtVisWheel.Text Then
                                .txtVisWheel.BackColor = Color.Red
                            Else
                                .txtVisWheel.BackColor = Color.Lime
                            End If
                            If gbUpdateVIS = True Or bPInit = True Then
                                If IsNumeric(gofrmMain.txtSetRadiance.Text) = False OrElse CDbl(gofrmMain.txtSetRadiance.Text) < 0.0005 Then
                                    .txtCurrRadiance.Text = "0"
                                Else
                                    SET_RADIANCE_VIS_FETCH(VISOutput)
                                    .txtCurrRadiance.Text = VISOutput.ToString("0.0000")
                                    .txtCurrRadiance.Refresh()
                                End If
                            End If
                        End If
                    End With
                End If

                shVEO2Count += 1
                If shVEO2Count > 5 Then
                    shVEO2Count = 1
                End If

                If bPInit Then
                    bPInit = False
                End If
            End If
            gofrmMain.Refresh()
            goTimer.Enabled = True

        Catch ex As Exception
            MsgBox(String.Format("Unexpected Error in OnTimerEvent: Error Number = '{0}'; Description = '{1}'", Err.Number, Err.Description), MsgBoxStyle.Critical, "Unexpected Error")
            Application.Exit()
            goTimer.Enabled = False
            goTimer.Stop()
        End Try
    End Sub

    ''' <summary>
    ''' Initializes Form Controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitControls()
        Try
            With gofrmMain
                .txtSetAmplitude.Text = "0"
                .txtSetPP.Text = "0"
                .txtSetElevation.Text = "0"
                .txtSetAzimuth.Text = "0"
                .txtSetPA.Text = "0"
                .txtSetRadiance.Text = "0.0"
                .txtSetDT.Text = "0.0"
                .txtCurrRadiance.Text = "0"
            End With

        Catch ex As Exception
            MsgBox(String.Format("Unexpected Error in InitControls: Error Number = '{0}'; Description = '{1}'", Err.Number, Err.Description), MsgBoxStyle.Critical, "Unexpected Error")
        End Try
    End Sub

    ''' <summary>
    ''' Check for LARRS.DAT file ' AZPOS, ELPOS, LAREA_LO, LAREA_HI, XCENTROID_LO, YCENTROID_LO, XCENTROID_HI, YCENTROID_HI
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetLaRRS()
        Dim x As Short
        Dim S As String = vbNullString
        Dim shIdx As Short

        'If File.Exists("c:\LARRS.DAT") = False Then
        If File.Exists(LARRSFILE) = False Then
            MsgBox(String.Format("Cannot find file '{0}'.", LARRSFILE))
            Exit Sub
        End If

        'Parse LARRS.DAT
        x = FreeFile()
        FileOpen(x, (LARRSFILE), OpenMode.Input)

        'Array Index Decremented by 1 for 0-Based .NET Code
        For shIdx = 0 To 2
            If Not EOF(x) Then
                Input(x, S)
                If IsNumeric(S) = False Then
                    MsgBox("AZPOS invalid! " & vbCrLf & S)
                    Exit For
                End If
                garrshAzPos(shIdx) = CSng(S)

                Input(x, S)
                If IsNumeric(S) = False Then
                    MsgBox("ELPOS invalid! " & vbCrLf & S)
                    Exit For
                End If
                garrshElPos(shIdx) = CShort(S)
            End If
        Next shIdx

        FileClose(x)
    End Sub

    ''' <summary>
    ''' Initialize and Define Variables used for the Asset Controls while running in SAIS Mode.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitSaisModeVar()
        Dim iIndex As Short 'Loop Counter

        'ALTA asset control variables
        nUpperLimit(AMPL) = 5000000
        nUpperLimit(PULPER) = 125
        nUpperLimit(LPOL) = 359
        nUpperLimit(LAZ) = 20000
        nUpperLimit(LEL) = 20000

        nLowerLimit(AMPL) = 0
        nLowerLimit(PULPER) = 50
        nLowerLimit(LAZ) = 0
        nLowerLimit(LEL) = 0
        nLowerLimit(LPOL) = 0

        nSetDefault(AMPL) = 0
        nSetDefault(PULPER) = 50
        nSetDefault(LPOL) = 0

        'Fill Target Wheel Combo Boxes (put here to do only once)
        '*** IR Module Target Selection Combo Box
        marrsIRWheelStr(0) = "Open Aperture"
        marrsIRWheelStr(1) = "Pinhole: IR, Vis, Laser"
        marrsIRWheelStr(2) = "Pie Sector"
        marrsIRWheelStr(3) = "5.00 Cyc/mRad 4 Bar"
        marrsIRWheelStr(4) = "3.8325 Cyc/mRad 4 Bar"
        marrsIRWheelStr(5) = "2.665 Cyc/mRad 4 Bar"
        marrsIRWheelStr(6) = "1.4975 Cyc/mRad 4 Bar"
        marrsIRWheelStr(7) = "0.33 Cyc/mRad 4 Bar"
        marrsIRWheelStr(8) = "Diagonal Slit"
        marrsIRWheelStr(9) = "Multi Pinhole"
        marrsIRWheelStr(10) = "Alignment Cross 1 mRad W x 7 mRad L"
        marrsIRWheelStr(11) = "USAF 1951, Groups 0-4"
        marrsIRWheelStr(12) = "1.0 Cyc/mRad 4 Bar"
        marrsIRWheelStr(13) = ".66 Cyc/mRad 4 Bar"
        marrsIRWheelStr(14) = "Square Aperture Target 21 mRad"

        'Index for target image display on form (Reduced by one for 0-(N-1) Arrays in .NET Vs VB6 (1-N)
        marrshIRTgtImageIndex(0) = 0
        marrshIRTgtImageIndex(1) = 3
        marrshIRTgtImageIndex(2) = 4
        marrshIRTgtImageIndex(3) = 1
        marrshIRTgtImageIndex(4) = 1
        marrshIRTgtImageIndex(5) = 1
        marrshIRTgtImageIndex(6) = 1
        marrshIRTgtImageIndex(7) = 1
        marrshIRTgtImageIndex(8) = 2
        marrshIRTgtImageIndex(9) = 7
        marrshIRTgtImageIndex(10) = 5
        marrshIRTgtImageIndex(11) = 6
        marrshIRTgtImageIndex(12) = 1
        marrshIRTgtImageIndex(13) = 1
        marrshIRTgtImageIndex(14) = 8
        'LARRYP: Why Declare marrshIRTgtImageIndex as 0 to 16 but only prefill 0 to 14?

        For iIndex = 0 To 14
            gofrmMain.cboIR_TargetWheel.Items.Insert(iIndex, marrsIRWheelStr(iIndex))
        Next

        '*** Visible Module Target Selection Combo Box
        marrsVisWheelStr(0) = "Open Aperture" 'Index = 0
        marrsVisWheelStr(1) = "Pinhole: IR, Vis, Laser" 'Index = 1
        marrsVisWheelStr(2) = "Pie Sector" 'Index = 2
        marrsVisWheelStr(3) = "5.00 Cyc/mRad 4 Bar" 'Index = 3
        marrsVisWheelStr(4) = "3.8325 Cyc/mRad 4 Bar" 'Index = 4
        marrsVisWheelStr(5) = "2.665 Cyc/mRad 4 Bar" 'Index = 5
        marrsVisWheelStr(6) = "1.4975 Cyc/mRad 4 Bar" 'Index = 6
        marrsVisWheelStr(7) = "0.33 Cyc/mRad 4 Bar" 'Index = 7
        marrsVisWheelStr(8) = "Diagonal Slit" 'Index = 8
        marrsVisWheelStr(9) = "Multi Pinhole" 'Index = 9
        marrsVisWheelStr(10) = "Alignment Cross 1 mRad W x 7 mRad L" 'Index = 10
        marrsVisWheelStr(11) = "USAF 1951, Groups 0-4" 'Index = 11
        marrsVisWheelStr(12) = "1.0 Cyc/mRad 4 Bar" 'Index = 12
        marrsVisWheelStr(13) = ".66 Cyc/mRad 4 Bar" 'Index = 13
        marrsVisWheelStr(14) = "Square Aperture Target 21 mRad" 'Index = 14

        'Index for target image display on form (Reduced by one for 0-(N-1) Arrays in .NET Vs VB6 (1-N)
        marrshVisTgtImageIndex(0) = 0
        marrshVisTgtImageIndex(1) = 3
        marrshVisTgtImageIndex(2) = 4
        marrshVisTgtImageIndex(3) = 1
        marrshVisTgtImageIndex(4) = 1
        marrshVisTgtImageIndex(5) = 1
        marrshVisTgtImageIndex(6) = 1
        marrshVisTgtImageIndex(7) = 1
        marrshVisTgtImageIndex(8) = 2
        marrshVisTgtImageIndex(9) = 7
        marrshVisTgtImageIndex(10) = 5
        marrshVisTgtImageIndex(11) = 6
        marrshVisTgtImageIndex(12) = 1
        marrshVisTgtImageIndex(13) = 1
        marrshVisTgtImageIndex(14) = 8

        marrssVisTgtString(0) = "Open Aperture" 'Index = 0
        marrssVisTgtString(1) = "Pinhole: IR, Vis, Laser" 'Index = 1
        marrssVisTgtString(2) = "Pie Sector" 'Index = 2
        marrssVisTgtString(3) = "5.00 Cyc/mRad 4 Bar" 'Index = 3
        marrssVisTgtString(4) = "3.8325 Cyc/mRad 4 Bar" 'Index = 4
        marrssVisTgtString(5) = "2.665 Cyc/mRad 4 Bar" 'Index = 5
        marrssVisTgtString(6) = "1.4975 Cyc/mRad 4 Bar" 'Index = 6
        marrssVisTgtString(7) = "0.33 Cyc/mRad 4 Bar" 'Index = 7
        marrssVisTgtString(8) = "Diagonal Slit" 'Index = 8
        marrssVisTgtString(9) = "Multi Pinhole" 'Index = 9
        marrssVisTgtString(10) = "Alignment Cross 1 mRad W x 7 mRad L" 'Index = 10
        marrssVisTgtString(11) = "USAF 1951, Groups 0-4" 'Index = 11
        marrssVisTgtString(12) = "1.0 Cyc/mRad 4 Bar" 'Index = 12
        marrssVisTgtString(13) = ".66 Cyc/mRad 4 Bar" 'Index = 13
        marrssVisTgtString(14) = "Square Aperture Target 21 mRad" 'Index = 14

        For iIndex = 0 To 14
            gofrmMain.cboVisWheel.Items.Insert(iIndex, marrsVisWheelStr(iIndex))
        Next

        '*** ALTA Module Combo Boxes
        garrsLaserDiode(0) = "1570"
        garrsLaserDiode(1) = "1540"
        garrsLaserDiode(2) = "1064"

        garrsLaserTrigger(0) = "Free Run"
        garrsLaserTrigger(1) = "Laser Pulse"
        garrsLaserTrigger(2) = "External"

        For iIndex = 0 To 2
            gofrmMain.cboDiode.Items.Insert(iIndex, garrsLaserDiode(iIndex))
        Next

        For iIndex = 0 To 2
            gofrmMain.cboLaserTrigger.Items.Insert(iIndex, garrsLaserTrigger(iIndex))
        Next
    End Sub

    ''' <summary>
    ''' Sets the IR Target Wheel to the desired position.
    ''' Continually updates the Current Target Wheel Position on the Asset Panel.
    ''' </summary>
    ''' <param name="iTargetPos">
    '''     iTargetPos: The desired wheel index to move to.
    ''' </param>
    ''' <returns></returns>
    ''' <remarks>
    '''    True if no errors, False if the wheel failed to move to the proper position.
    ''' </remarks>
    Public Function SetTargetPos(ByRef iTargetPos As Integer) As Boolean
        Dim iDelay As Short 'Delay Counter
        Dim iTgtPosRcvd As Integer 'Present Target Wheel Position
        Dim bRetVal As Boolean = False

        SET_TARGET_POSITION_INITIATE(iTargetPos)
        Delay(1)

        With gofrmMain
            Do
                SET_TARGET_POSITION_FETCH(iTgtPosRcvd)
                Delay(1)
                iDelay = iDelay + 1
                If (iTgtPosRcvd < 0) Or (iTgtPosRcvd > 14) Then iTgtPosRcvd = 0

                'Update Current Target Position Text Box and Image

                .pboxIRTarget.Image = .TargetImageList.Images.Item(marrshIRTgtImageIndex(iTgtPosRcvd))
                .pboxIRTarget.Invalidate()
                .txtCurrIRTargPos.Text = marrsIRWheelStr(iTgtPosRcvd)

                .pboxVisTarget.Image = .TargetImageList.Images.Item(marrshVisTgtImageIndex(iTgtPosRcvd))
                .pboxVisTarget.Invalidate()
                .txtVisWheel.Text = marrssVisTgtString(iTgtPosRcvd)

                Application.DoEvents()
            Loop Until (iTgtPosRcvd = iTargetPos) Or iDelay > 30

            If iTargetPos <> iTgtPosRcvd Then
                'Failed to Set Correct Target Position
                bRetVal = False
            Else
                'Target Position Set Correctly
                bRetVal = True

                'Since successful set both combo boxes (Vis and IR) to the set position
                .cboIR_TargetWheel.SelectedIndex = iTargetPos
                .cboVisWheel.SelectedIndex = iTargetPos
            End If
        End With

        Return bRetVal
    End Function

    ''' <summary>
    ''' Sends the Set Delta T command to the IR Module.
    ''' </summary>
    ''' <remarks>
    ''' The (Set) parameter is read from the txtSetDT
    ''' Text Box on the Asset Control Panel. This allows
    ''' the set value to be changed without using the button.
    ''' </remarks>
    Public Sub SetDeltaT()
        Dim sngTemp As Single
        Dim shConfig As Short
        Dim shX As Short
        Dim shK As Short

        goTimer.Enabled = False
        gofrmMain.tsStatusLabel.Text = "Setting System Configuration....Please Wait"
        gofrmMain.Cursor = Cursors.WaitCursor
        gofrmMain.Refresh()

        gbUpdateIR = True 'Set Flag to update Delta T on Asset Control Panel
        gbUpdateVIS = False

        SET_SYSTEM_CONFIGURATION_FETCH(shConfig)
        If shConfig <> 2 Then
            SET_SYSTEM_CONFIGURATION_INITIATE(2)
        End If

        While shX < 30
            SET_SYSTEM_CONFIGURATION_FETCH(shConfig)
            Delay(2)
            If shConfig <> 2 Then
                shX = shX + 1
            Else
                shX = 30
            End If
            If shX = 29 Then
                shK = MsgBox("EO system failed to set configuration!", MsgBoxResult.Ok)
                shX = 30
            End If
        End While
        shX = 0

        'Set to Differential Mode
        sngTemp = CSng(gofrmMain.txtSetDT.Text)
        SET_TEMP_DIFFERENTIAL_IR_INITIATE(sngTemp)
        Delay(1)

        GET_TEMP_TARGET_IR_INITIATE()
        Delay(1)

        gofrmMain.tsStatusLabel.Text = ""
        gofrmMain.Cursor = Cursors.Default
        gofrmMain.Refresh()
        gofrmMain.BringToFront()
        goTimer.Enabled = True
    End Sub

    ''' <summary>
    ''' Sends the Set Radiance command to the Visible Module.
    ''' </summary>
    ''' <remarks>
    ''' The (Set) parameter is read from the txSetRadiance
    ''' Text Box on the Asset Control Panel. This allows
    ''' the set value to be changed without using the button.
    ''' </remarks>
    Public Sub SetRadiance()
        Dim setRad As Single
        Dim config As Short
        Dim x As Short
        Dim k As Short

        goTimer.Enabled = False
        gofrmMain.Cursor = Cursors.WaitCursor
        gofrmMain.tsStatusLabel.Text = "Setting System Configuration....Please Wait"
        gofrmMain.Refresh()

        gbUpdateIR = False 'Set Flag to stop updates of Delta T on Asset Control Panel

        SET_SYSTEM_CONFIGURATION_FETCH(config)
        If config <> 3 Then
            SET_SYSTEM_CONFIGURATION_INITIATE(3)
        End If

        While x < 30
            SET_SYSTEM_CONFIGURATION_FETCH(config)
            Delay(2)
            If config <> 3 Then
                x = x + 1
            Else
                x = 30
            End If
            If x = 29 Then
                k = MsgBox("EO system failed to set configuration!", MsgBoxResult.Ok)
                x = 30
            End If
        End While
        x = 0

        setRad = CSng(gofrmMain.txtSetRadiance.Text)
        'Set Iris to desired radiance in uw/cm2-sr
        If (setRad) < 0.0005 Then
            SET_RADIANCE_VIS_INITIATE(0)
            gofrmMain.txtCurrRadiance.Text = "0"
            Delay(5)
            gbUpdateVIS = False 'Set Flag the update current Radiance Value
        Else
            SET_RADIANCE_VIS_INITIATE(setRad)
            Delay(5)
            gbUpdateVIS = True 'Set Flag the update current Radiance Value
        End If
        gofrmMain.tsStatusLabel.Text = ""
        gofrmMain.Refresh()
        gofrmMain.BringToFront()
        gofrmMain.Cursor = Cursors.Default
        goTimer.Enabled = True
    End Sub

    ''' <summary>
    ''' Procedure makes sure that the module replies correctly to the
    ''' initialization command, acknowledging that the module is ready.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitAsset()
        gbEoPowerOnRemote = True 'EO Power from non Standard Supplies 7,8,9
        gofrmMain.gboxEOPower.Visible = False 'Don't show EO Power Controls if not using PPU7,8,9
        gofrmMain.cmdEOPower.Visible = False

        If (gofrmMain.chkLiveDisplay.Checked) And (Not gbInitVideo) Then
            Init_Pixci()
            GrabLive_Pixci() 'Begin capturing Video
        ElseIf Not gbInitVideo Then
            Init_Pixci()
            Snapshot_Pixci() 'Get Snapshot
        End If
        gbInitVideo = True
    End Sub

    ''' <summary>
    ''' Inititiaize the EO Asset Controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitAssetVar()
        Dim sDegree As String 'Symbol for Degree
        Dim iIndex As Short 'Loop Index
        Dim sMicro As String 'Symbol for Micro
        Dim sSquared As String 'Symbol for Squared
        Dim iTgtPosRcvd As Integer 'Present Target Wheel
        Dim shSetDiode As Short 'Present Diode Position

        On Error GoTo InitErrorHandler 'Trap Errors

        sDegree = Chr(176) 'Assign symbol for degrees
        sMicro = Chr(181) 'Assign symbol for Micro
        sSquared = Chr(178) 'Assign symbol for Squared

        'do this to ensure veo2.dll is initialized properly
        If gbEOPoweredAtStartup = True Then
            RESET_MODULE_INITIATE()
            Delay(1)
        End If

        With gofrmMain
            .cboIR_TargetWheel.SelectedIndex = 0 'Set Default
            .tabAsset.SelectedIndex = 0
            .tabAsset.TabPages.Item(0).Enabled = True
            .tabAsset.TabPages.Item(1).Enabled = True
            .tabAsset.TabPages.Item(2).Enabled = True
            .lblDeltaT1.Text = "Ambient Temperature (" & sDegree & "C)"
            .lblDeltaT2.Text = "BlackBody Temperature (" & sDegree & "C)"
            .lblReqDelta.Text = "Requested Delta (" & sDegree & "C)"

            If SetTargetPos(0) Then 'Set wheel to Open Aperture
                'Poll and set wheel string
                SET_TARGET_POSITION_FETCH(iTgtPosRcvd)
                Delay(1)
                If (iTgtPosRcvd < 0) Or (iTgtPosRcvd > 14) Then iTgtPosRcvd = 0
                .txtCurrIRTargPos.Text = marrsIRWheelStr(iTgtPosRcvd)
                .txtCurrIRTargPos.BackColor = Color.Lime
            Else
                .txtCurrIRTargPos.BackColor = Color.Red
            End If

            .StatusStrip1.Refresh()
            .cboVisWheel.SelectedIndex = 0 'Set Default

            .lblRadiance.Text = "Current Radiance (" & sMicro & "W/cm" & sSquared & "/sr)"
            .lblRequestRadiance.Text = "Requested Radiance (" & sMicro & "W/cm" & sSquared & "/sr)"
            .lblAmplitudeLaser.Text = "Amplitude (nW/cm " & sSquared & "/sr)"

            If SetTargetPos(0) Then 'Set wheel to Open Aperture
                'Poll and set wheel string
                .txtVisWheel.Text = marrssVisTgtString(iTgtPosRcvd)
                .txtVisWheel.BackColor = Color.Lime
            Else
                .txtVisWheel.BackColor = Color.Red
            End If

            .cboDiode.SelectedIndex = 0 'Set Default
            .cboLaserTrigger.SelectedIndex = 0 'Set Default

            SELECT_DIODE_LASER_FETCH(shSetDiode)
            Delay(1)
            .cboDiode.SelectedIndex = shSetDiode

            If gbMainShown Then .tabAsset.Focus()
        End With

        Exit Sub

InitErrorHandler:
        MsgBox("Sub InitAssetVar() Error Number: " & Err.Number & "   Description: " & Err.Description) 'For Debugging only
        Resume Next
    End Sub

    ''' <summary>
    ''' Nomenclature   : 7081-PPU
    ''' Verifies Communication With Message Delivery System
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Public Function VerifyInstrumentCommunication() As Short
        Dim Pass As Boolean 'Instrument Pass Test (True/False)
        Dim status As Integer
        Dim XmlBuf As String
        Dim sAllocFilename As String
        Dim response As String

        garrsPsResourceName(0) = "DCPS_1"
        garrsPsResourceName(1) = "DCPS_2"
        garrsPsResourceName(2) = "DCPS_3"
        garrsPsResourceName(3) = "DCPS_4"
        garrsPsResourceName(4) = "DCPS_5"
        garrsPsResourceName(5) = "DCPS_6"
        garrsPsResourceName(6) = "DCPS_7"
        garrsPsResourceName(7) = "DCPS_8"
        garrsPsResourceName(8) = "DCPS_9"
        garrsPsResourceName(9) = "DCPS_10"

        Pass = False 'Until Proven True

        'Check For The Resource Manager
        status = atxml_Initialize(PROCTYPE, GUID)

        sAllocFilename = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)
        If sAllocFilename.Trim() = vbNullString Then
            Pass = False
            Return Pass
        End If

        If File.Exists(sAllocFilename) = False Then
            MsgBox("Invalid PawsAllocation.xml Path", MsgBoxStyle.Critical, "Full XML File Path from Registry")
            Pass = False
            Return Pass
        End If

        XmlBuf = Space(4096)
        response = Space(4096)

        XmlBuf = "<AtXmlTestRequirements>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_1</SignalResourceName> " & "</ResourceRequirement> "
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_2</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_3</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_4</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_5</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_6</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_7</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_8</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_9</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_10</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "</AtXmlTestRequirements>"

        status = atxml_ValidateRequirements(XmlBuf, sAllocFilename, response, Len(XmlBuf))

        If status Then
            MsgBox("The PPU is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
            Pass = False
            Return Pass
        Else
            Pass = True
        End If

        'Parse Availability XML string to get the status(Mode) of the instrument
        gctlViperTParse.ParseAvailiability(response)

        'In case EPIX Integration has run before and aborted before completion, the XCLIB may not have been
        'closed, and the open would fail
        PXD_PIXCICLOSE()

        Return Pass
    End Function

    ''' <summary>
    ''' Check EO Power Supplies to determine State, Set Power indicator accordingly.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckEoPower()
        Dim D1 As Single
        Dim D2 As Single
        Dim data As String
        Dim ErrorStatus As Integer
        Dim shSupply As Short 'Power Supply Number
        Dim dVolts As Double 'Voltage measurement returned
        Dim sReadBuffer As String
        Dim shParm1 As Short
        Dim shParm2 As Short
        Dim shParm3 As Short
        Dim Relayclosed As Boolean

        For shSupply = 3 To 1 Step -2
            shParm1 = shSupply
            shParm2 = &H42
            shParm3 = &H0
            'Send Command
            SendDCPSCommand(shSupply, Convert.ToString(Chr(shParm1)) & Convert.ToString(Chr(shParm2)) & Convert.ToString(Chr(shParm3)))

            Delay(0.1)

            'Read Sense Measurement
            sReadBuffer = Space(4000)
            ErrorStatus = ReadDCPSCommand(shSupply, sReadBuffer)
            If ErrorStatus <> 0 Then
                Exit Sub
            End If

            'Voltage Measurement
            data = sReadBuffer

            If Asc(data.Substring(0, 1)) > 64 Then '80
                'voltage measurement divide by 100
                D1 = (Asc(data.Substring(2, 1))) And 15
                D1 = D1 * 256
                D2 = Asc(data.Substring(3, 1))
                dVolts = CDbl((D1 + D2) / 100.0)

                If (shSupply = 1 And (dVolts < 26 Or dVolts > 30)) Or ((shSupply = 3) And (dVolts < 13 Or dVolts > 17)) Then
                    SetPowerIndicator(False) 'Indicate Power OFF
                    gbEoPowerOnVIPERT = False
                    gbEOPoweredAtStartup = False
                    Exit Sub
                End If
            End If

            'Check to see if the relay is closed to apply power
            SendDCPSCommand(shSupply, Convert.ToString(Chr(shSupply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(&H0)))
            Delay(0.1)
            ErrorStatus = ReadDCPSCommand(shSupply, sReadBuffer)
            If ErrorStatus <> 0 Then
                Exit Sub
            End If
            If Len(sReadBuffer) > 2 Then
                Relayclosed = RelayState(Asc(Mid(sReadBuffer, 1, 1))) 'Relay State?
                If (Relayclosed = False) Then
                    SetPowerIndicator(False) 'Indicate Power OFF
                    gbEoPowerOnVIPERT = False
                    gbEOPoweredAtStartup = False
                    Exit Sub
                End If
            End If
        Next shSupply

        gbEOPoweredAtStartup = True
        SetPowerIndicator(True) 'Indicate Power ON
        gbEoPowerOnVIPERT = True
    End Sub

    Function RelayState(ByVal iBinary As Integer) As Boolean
        Dim msByte As String
        msByte = IntToBinary(iBinary)
        'EADS - DRB added 2/25/2008 to properly select supply for setting
        'Isolation Relay
        If Mid(msByte, 4, 1) = "1" Then
            RelayState = True
        Else
            RelayState = False
        End If

    End Function
    ' Convert this Integer into a binary string.
    Function IntToBinary(ByVal iValue As Integer) As String
        IntToBinary = ""

        Dim result_string As String = ""
        Dim digit_num As Short
        Dim factor As Short

        If iValue > 255 Then Exit Function
        ' Read the hexadecimal digits
        ' one at a time from right to left.
        factor = 128
        For digit_num = 0 To 7
            ' Convert the value into bits.

            If iValue And factor Then
                result_string &= "1"
            Else
                result_string &= "0"
            End If
            factor = factor \ 2

        Next digit_num

        ' Return the result.
        IntToBinary = result_string
    End Function
    ''' <summary>
    ''' ex:   ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer)
    ''' </summary>
    ''' <param name="SupplyX"></param>
    ''' <param name="ReadBuffer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ReadDCPSCommand(ByRef SupplyX As Short, ByRef ReadBuffer As String) As Integer
        Dim ErrStatus As Integer
        Dim iReadLenReturned As Integer
        Dim S As String
        Dim i As Short

        ReadBuffer = Space(255)
        ErrStatus = atxmlDF_viRead(garrsPsResourceName(SupplyX - 1), 0, ReadBuffer, 255, iReadLenReturned)
        If ErrStatus <> 0 Then
            MsgBox("AtXml Power Source Communication Error", MsgBoxStyle.OkOnly, "Non-Zero Status Returned")
        Else
            'The power supply should always return 5 bytes for a status read command
            If iReadLenReturned = 5 Then
                ReadBuffer = ReadBuffer.Substring(0, iReadLenReturned)
            Else
                S = ""
                For i = 1 To iReadLenReturned
                    S = S & "Byte" & CStr(i) & " =" & Str(Asc(ReadBuffer.Substring(i - 1, 1))) & vbCrLf
                Next i

                ErrStatus = atxmlDF_viRead(garrsPsResourceName(SupplyX), 0, ReadBuffer, 255, iReadLenReturned)
                If iReadLenReturned = 5 Then
                    ReadBuffer = ReadBuffer.Substring(0, iReadLenReturned)
                End If
            End If
            Delay(0.5)
        End If

        Return ErrStatus
    End Function

    Public Function Init_Pixci() As Boolean
        Dim bRetVal As Boolean = False

        'gpixciCamera.format = "default"    ' as per board's intended camera
        gpixciCamera.format = PIXCI_FORMAT
        gpixciCamera.formatfile = gsVidFormatFile

        Dim err As Integer = PXD_PIXCIOPEN("", gpixciCamera.format, gpixciCamera.formatfile)
        Delay(1)
        If err < 0 Then
            MsgBox("Pixci Open Failed", MsgBoxStyle.Exclamation, "Initializing Pixci Elements")
            PXD_MESGFAULT(1)
            Application.Exit()
            'Just in case!
            Exit Function
        End If

        'Not sure how to set this with the New API yet
        'EPIX XCAP, Camera Info shows for 10 Bits per Pixie (???) and 1 Pixie per Pixel
        'So assuming, for now, it is 10 Bits Per Pixel
        gpixciCamera.BytesPerPixel = PXD_IMAGEBDIM
        gpixciCamera.maxWidth = PXD_IMAGEXDIM
        gpixciCamera.maxHeight = PXD_IMAGEYDIM

        gofrmMain.tsStatusLabel.Text = "Camera Resolution is " & gpixciCamera.maxWidth.ToString & " x " & gpixciCamera.maxHeight.ToString

        If PXD_IMAGECDIM.Equals(3) Then
            gpixciCamera.is_color = True
            gofrmMain.tsStatusLabel.Text = "Color Camera"
        End If
        gofrmMain.StatusStrip1.Refresh()

        bRetVal = True

        Return bRetVal
    End Function

    ''' <summary>
    ''' Grab is Live (Continuous) Vs Snapshot
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GrabLive_Pixci() As Short

        gpixciCamera.BytesPerPixel = PXD_IMAGEBDIM
        gpixciCamera.maxWidth = PXD_IMAGEXDIM
        gpixciCamera.maxHeight = PXD_IMAGEYDIM

        'Do we need to see if we are already Live or can this call handle it either way?
        If Not gpixciCamera.is_live Then
            PXD_GOLIVE(1, 1)
            gpixciCamera.is_live = True
        End If

        gofrmMain.chkLiveDisplay.Checked = True
        gofrmMain.pboxDisplay.Invalidate()
        gofrmMain.pboxDisplay.BringToFront()

    End Function

    ''' <summary>
    ''' Snap is a Snapshot vs Grab (Live)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Snapshot_Pixci() As Short

        PXD_GOUNLIVE(1)
        gpixciCamera.is_live = False
        PXD_GOSNAP(1, 1)

        gofrmMain.pboxDisplay.Invalidate() ' Redraw PictureBox1
    End Function

    ''' <summary>
    ''' Find an open instance of an application with matching window title passed through "sWindowTitle" (not case sensitive).
    ''' If the "bCloseApp" Flag is set to TRUE, the Application is closed.Same as clicking the "X" on the form.
    ''' </summary>
    ''' <param name="psWindowTitle">
    '''     sWindowTitle: The Window's Title in the Title Bar.
    ''' </param>
    ''' <param name="pbCloseApp">
    '''     bCloseApp:    True, Close Application
    '''                   False, don't Close Application
    ''' </param>
    ''' <returns>
    '''     True, Application is open
    '''     False, Application is Closed or not open
    ''' </returns>
    ''' <remarks></remarks>
    Public Function FindOpenApp(ByRef psWindowTitle As String, ByRef pbCloseApp As Boolean) As Boolean
        Dim nWndCtlApp As Integer 'Control Application Handle
        Const WM_CLOSE As Short = &H10S 'API Constant to Close Application

        'Attempt to find an Open Window matching the passed Title
        nWndCtlApp = FindWindow(vbNullString, psWindowTitle)

        If nWndCtlApp <> 0 Then
            FindOpenApp = True 'If Application Open, Set FLAG

            If pbCloseApp = True Then
                'If the CLOSE FLAG is set, Close Application
                SendMessage(nWndCtlApp, WM_CLOSE, 0, 0)
                FindOpenApp = False 'Set FLAG to indicate App Closed
            End If
        Else
            FindOpenApp = False 'Application not Open, Set FLAG
        End If
    End Function

    ''' <summary>
    ''' Delay Parameter "SleepTime" Seconds
    ''' Calls Thread.Sleep (SleepTime * 1000) as the System parm is in Milliseconds
    ''' </summary>
    ''' <param name="SleepTime">
    ''' Seconds to Sleep
    ''' </param>
    ''' <remarks></remarks>
    Public Sub Delay(ByVal SleepTime As Double)
        Dim mSecs As Integer = Math.Round(SleepTime * 1000)
        Dim startTime As DateTime = DateTime.Now
        Dim timeDiff As TimeSpan = DateTime.Now.Subtract(startTime)

        While timeDiff.TotalMilliseconds < mSecs
            Threading.Thread.Sleep(10)
            Application.DoEvents()
            timeDiff = DateTime.Now.Subtract(startTime)
        End While
    End Sub

    Private Sub EnableUI(ByVal pbEnable As Boolean)
        gofrmMain.Enabled = pbEnable
        gofrmMain.Refresh()
    End Sub

    ''' <summary>
    ''' Draw a Red Cross Hair Target through the center of the Displayed Image.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OverlayTarget()
        '------------------------------------------------------------------
        '***  If running from NAM and mode is either Field-of-View      ***
        '***  or Target Coordinates, Save Image and exit                ***
        '***  This will also launch the ROI Form and Hide the Display   ***
        '------------------------------------------------------------------
        Try
            gofrmMain.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Ensure an image input is displayed
            GrabLive_Pixci()

            'Display Snapshot of current Image input
            'Snapshot_Pixci()

            If gbRunFromNAM = True Then
                If (gshMode = ALIGN) Then
                    If (gofrmMain.cmdCapture.Text = "&Help") And (gofrmMain.cmdCapture.Enabled = False) Then
                        Exit Sub
                    End If
                    Delay(1)
                    gofrmMain.pboxDisplay.Invalidate()
                End If
                Select Case gshMode
                    Case FOV, TGTCOORD
                        gbCapture = False
                        SaveImage()
                        Exit Sub
                End Select
            End If

        Catch ex As Exception

        Finally
            gofrmMain.Cursor = Cursors.Default
        End Try

        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Captures current Image and saves as "Capture.bmp".  Files are Saved in the Windows Temporary Directory.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Sub SaveImage()
        Dim nRetVal As Integer 'Function Return Value
        Dim sFile As String

        On Error GoTo ErrorHandler

        'Hide the Main Form
        gofrmMain.Visible = False

        sFile = gsXCAPCaptureDir & "Capture.bmp"

        'Make sure the Capture BitMap doesn't already exist.
        If File.Exists(sFile) Then
            File.Delete(sFile)
        End If

        nRetVal = PXD_SAVEBMP(1, sFile, 1, 0, 0, -1, -1, 0, 0)

        gofrmMain.tsStatusLabel.Text = String.Format("{0} saved.", sFile)
        gofrmMain.StatusStrip1.Refresh()

        Load_ROI_FOV()

        Exit Sub

ErrorHandler:
        'Catch a "Subscript out of Range" Error
        gofrmMain.Cursor = Cursors.Default

        If Err.Number <> 0 Then
            MsgBox("SaveImage Error Number: " & Err.Number & "   Description: " & Err.Description) 'For Debugging only
        End If
    End Sub

    ''' <summary>
    ''' Function returns True if the Mode Argument is supported and sets the Mode Index.
    ''' 
    ''' </summary>
    ''' <param name="sArgMode"></param>
    ''' <returns>
    '''     Returns False if the Mode is not supported.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function VerifyModeSetting(ByRef sArgMode As String) As Boolean
        Dim bRetVal As Boolean = False

        Select Case sArgMode.ToUpper
            Case "FOV"
                gshMode = FOV
                bRetVal = True
            Case "TGTCOORD"
                gshMode = TGTCOORD
                bRetVal = True
            Case "ALIGN"
                gshMode = ALIGN
                bRetVal = True
            Case "DISPLAY_ONLY"
                gshMode = DISPLAY_ONLY
                bRetVal = True
            Case Else
                gshMode = -1
                bRetVal = False
        End Select

        Return bRetVal
    End Function

    ''' <summary>
    ''' Writes the Error Code and Description to the NAMError section of the VidData.dat file for use by the ATLAS NAM.
    ''' </summary>
    ''' <param name="nErrNum">
    '''     NErrNum: Error Number
    ''' </param>
    ''' <param name="sErrDescription">
    '''     sErrDescription: Error Description
    '''         Optional Parm which defaults to Empty String
    ''' </param>
    ''' <remarks>
    ''' Example INI Lines Written by SetErrorKey()
    '''     [NAMError]
    '''     ErrCode=-101
    '''     ErrString=ERROR - Invalid Mode: 
    ''' </remarks>
    Public Sub SetErrorKey(ByRef nErrNum As Integer, Optional ByRef sErrDescription As String = "")
        Dim nRetVal As Integer

        'This API will create file, Heading, Key and Value if it does not exist,
        'otherwise it will update Key value.
        nRetVal = WritePrivateProfileString(NAMEERRROR, ERRCODE, CStr(nErrNum), DATFILE)
        nRetVal = WritePrivateProfileString(NAMEERRROR, ERRSTR, sErrDescription, DATFILE)
    End Sub

    ''' <summary>
    ''' If an Instance of IRWindows is Open, Warn the user of the conflict then terminate the Application.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub IRWinOpen()
        Dim sWindowTitle As String 'IRWindow's Window Title

        'Get Window Title from the VIPERT.INI File
        'Title should be "IRWindows 2001" in .NET and Windows 7
        sWindowTitle = GatherIniFileInformation(ATS_INI, "SAIS", "IRWIN_C")
        If sWindowTitle = "" Then
            'Just move on!!
        Else
            FindOpenApp(sWindowTitle, False) 'First find out if the Application is Open

            'If Window is open, warn user and terminate
            If FindOpenApp(sWindowTitle, False) = True Then
                'Inform user that a conflict has beeen detected, terminate IRWindows
                MsgBox("Conflict Detected!" & vbCrLf & "IRWindows will be automatically terminated.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                'Terminate Process
                FindOpenApp(sWindowTitle, True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Initialize Video Capture Card, Create Sources, Sinks and
    ''' Connections to hold and display Image.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitVideo()
        On Error GoTo MainErrorHandler 'Set Error Handler

        'Initialize Video Capture Card
        If Not gbInitVideo Then
            If Not Init_Pixci() Then
                UpdateData()
                Exit Sub
            End If
        End If

        gbInitVideo = True
        gofrmMain.chkLiveDisplay.Enabled = True

        Exit Sub

MainErrorHandler:
        MsgBox("Error Number: " & Err.Number & "   Description: " & Err.Description) 'For Debugging only
        Resume Next
    End Sub

    ''' <summary>
    ''' Set Power Indicators on GUI.
    ''' Power On: True    Power Off: False
    ''' </summary>
    ''' <param name="bOn"></param>
    ''' <remarks></remarks>
    Public Sub SetPowerIndicator(ByRef bOn As Boolean)

        'If Not gbMainShown Then Exit Sub

        With gofrmMain
            If bOn = True Then
                .pboxLight0.Image = .ImageList1.Images.Item(0)
                .pboxLight0.Invalidate()
                .pboxLight1.Image = .ImageList1.Images.Item(0)
                .pboxLight1.Invalidate()
                .pboxLight2.Image = .ImageList1.Images.Item(0)
                .pboxLight2.Invalidate()
                .cmdEOPower.Text = "Power Off"
                .gboxEOPower.Text = "EO Power On"
            Else
                .pboxLight0.Image = .ImageList1.Images.Item(1)
                .pboxLight0.Invalidate()
                .pboxLight1.Image = .ImageList1.Images.Item(1)
                .pboxLight1.Invalidate()
                .pboxLight2.Image = .ImageList1.Images.Item(1)
                .pboxLight2.Invalidate()
                .cmdEOPower.Text = "Power On"
                .gboxEOPower.Text = "EO Power Off"
            End If
        End With
    End Sub

    Public Sub LoadVideoFormat()
        With frmConfig.cboCamConfig.Items
            .Clear()
            '.Insert(0, "ALTA NIR Camera")
            .Insert(0, "RS170_640x480")
            .Insert(1, "RS343_624x624")
            .Insert(2, "RS343_672x672")
            .Insert(3, "RS343_808x808")
            .Insert(4, "RS343_832x624")
            .Insert(5, "RS343_872x872")
            .Insert(6, "RS343_896x672")
            .Insert(7, "RS343_944x944")
            .Insert(8, "RS343_1080x808")
            .Insert(9, "RS343_1160x872")
            .Insert(10, "RS343_1256x944")
        End With

        'Launch Video Configuration Form (Modal)
        frmConfig.ShowDialog()
    End Sub

    ''' <summary>
    ''' Power ON EO Module and Identify, Configure Camera and start Image Capture.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PowerOnAndStartup()

        CheckEoPower()

        If gbEoPowerOnVIPERT = False Then
            EO_Power(True) 'Turn on EO Power
        End If

        If gbInitVideo = False Then
            'Load Camera Configuration files combo box
            LoadVideoFormat()
            InitVideo()
        End If

        gofrmMain.SizeForm()

        If Not gbInitVideo Then Init_Pixci()

        If gofrmMain.chkLiveDisplay.Checked Then
            GrabLive_Pixci() 'Begin capturing Video
        Else
            Snapshot_Pixci()
        End If
    End Sub

    ''' <summary>
    ''' Set the connected EO Module in the Default State and shut off power if not started with power.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PowerOffAndShutDown()

        If Not gbEoPowerOnVIPERT Then
            Exit Sub
        End If

        SET_CAMERA_POWER_INITIATE(0)
        Delay(1)

        SetTargetPos(0)
        Delay(1)

        SET_OPERATION_LASER_INITIATE(0)
        Delay(1)

        SET_SOURCE_STAGE_LASER_INITIATE(1)
        Delay(1)

        SET_SENSOR_STAGE_LASER_INITIATE(3)
        Delay(10)

        If Not gbStartedWithPower Then
            EO_Power(False) 'Turn off EO Power
        End If
    End Sub

    ''' <summary>
    ''' Update key values in the VidData.dat file to pass data to the ATLAS NAM.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateData()
        Dim nReturnErrNum As Integer 'Error Return Number to ATLAS
        Dim sErrReturnStr As String = vbNullString 'Error Return String

        If gbRunFromNAM = False Then Exit Sub

        Select Case gshMode
            Case FOV
                msHorzFoV = gofrmROI.txtTargetWidth.Text
                WritePrivateProfileString(RETDATA, HORZFOV, msHorzFoV, DATFILE)
                msVertFoV = gofrmROI.txtTargetHeight.Text
                WritePrivateProfileString(RETDATA, VERTFOV, msVertFoV, DATFILE)
            Case TGTCOORD
                WritePrivateProfileString(RETDATA, STARTX, gsStartX, DATFILE)
                WritePrivateProfileString(RETDATA, STARTY, gsStartY, DATFILE)
                WritePrivateProfileString(RETDATA, ENDX, gsEndX, DATFILE)
                WritePrivateProfileString(RETDATA, ENDY, gsEndY, DATFILE)
        End Select

        'Set Error Code and Error String if any
        If Len(sErrReturnStr) < 1 Then
            nReturnErrNum = 0
        Else
            sErrReturnStr = ""
            nReturnErrNum = -107 'IFC Error
        End If

        'Update Error Keys
        SetErrorKey(nReturnErrNum, sErrReturnStr)
    End Sub

    Public Function PrevInstance() As Boolean
        If UBound(Diagnostics.Process.GetProcessesByName _
           (Diagnostics.Process.GetCurrentProcess.ProcessName)) _
           > 0 Then
            Return True
        Else
            Return False
        End If
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

    Public Function GatherAppSettings(ByVal lpKeyName As String, Optional ByVal lpDefault As String = "") As String
        Dim sRetVal As String = vbNullString

        sRetVal = ConfigurationManager.AppSettings(lpKeyName)

        If sRetVal = vbNullString Then
            sRetVal = lpDefault
        End If

        Return sRetVal
    End Function

    ''' <summary>
    ''' This Module turns on and off the PDUs used for for EO power
    ''' </summary>
    ''' <param name="bOn"></param>
    ''' <remarks></remarks>
    Private Sub EO_Power(ByVal bOn As Boolean)
        Dim shResult As Short
        Dim loop1 As Short
        Dim oFrmDelay As frmDelay = New frmDelay()

        gofrmMain.Cursor = Cursors.WaitCursor

        'Center the delay form based on the visibility of the main form
        If gofrmMain.Visible Then
            oFrmDelay.StartPosition = FormStartPosition.Manual
            oFrmDelay.Location = New Point(gofrmMain.Left + gofrmMain.Width / 2 - oFrmDelay.Width / 2, gofrmMain.Top + gofrmMain.Height / 2 - oFrmDelay.Height / 2)
        Else
            oFrmDelay.StartPosition = FormStartPosition.CenterScreen
        End If

        ' Stop the timer from running so we don't get exceptions when the VEO2 is powered down
        ' Wait a second for any current timer processing to complete.  Then set the EO power On
        ' flag to false, preventing VEO2 interaction.  Finally, reenable the timer so video 
        ' capture is updated.
        If Not IsNothing(goTimer) Then
            goTimer.Enabled = False
            Delay(1)
            gbEoPowerOnVIPERT = False
            goTimer.Enabled = True
        End If

       If bOn Then
            shResult = MsgBox("Are you ready to turn on VEO2 power?", MsgBoxStyle.YesNo)
            If shResult <> MsgBoxResult.Yes Then
                gbEoPowerOnVIPERT = False 'needed to shutdown
                PowerOffAndShutDown()
                frmMain.cmdEOPower.Text = "Power On"
                Exit Sub
            Else
                frmMain.cmdEOPower.Text = "Power Off"
            End If
            gbEoPowerOnVIPERT = False

            '      "**** Turning on VEO2 Power ****"
            oFrmDelay.Show()
            oFrmDelay.Text1.Text = "Initializing Power Supplies"
            oFrmDelay.Refresh()
            oFrmDelay.BringToFront()

            ' open relays and reset supplies
            'Power Supplies 1, 2 and 3
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&HA0)) & Convert.ToString(Chr(&H0)))
            SendDCPSCommand(2, Convert.ToString(Chr(&H22)) & Convert.ToString(Chr(&HA0)) & Convert.ToString(Chr(&H0)))
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&HA0)) & Convert.ToString(Chr(&H0)))

            'Power Supplies 1, 2 and 3
            SendDCPSCommand(1, Convert.ToString(Chr(&H11)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))
            SendDCPSCommand(2, Convert.ToString(Chr(&H12)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))
            SendDCPSCommand(3, Convert.ToString(Chr(&H13)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))

            oFrmDelay.Text1.Text = "Turn on 15v supply 3"
            oFrmDelay.Refresh()
            oFrmDelay.BringToFront()
            'Turn on 15v supply 3
            'Close Output Relay
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&HB0)) & Convert.ToString(Chr(&H0)))
            'Set Current Limit to 5 Amps
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&H49)) & Convert.ToString(Chr(&HC4)))
            'Set Voltage to 15
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&H55)) & Convert.ToString(Chr(&HDC)))

            oFrmDelay.Text1.Text = "Setup Slave Supply 2"
            oFrmDelay.Refresh()
            oFrmDelay.BringToFront()
            'Setup Slave Supply 2
            SendDCPSCommand(2, Convert.ToString(Chr(&H22)) & Convert.ToString(Chr(&H8C)) & Convert.ToString(Chr(&H0)))
            'Close Output Relay
            SendDCPSCommand(2, Convert.ToString(Chr(&H22)) & Convert.ToString(Chr(&HB0)) & Convert.ToString(Chr(&H0)))

            oFrmDelay.Text1.Text = "Turn On Master and Close Output Relay"
            oFrmDelay.Refresh()
            oFrmDelay.BringToFront()
            'Turn On Master and Close Output Relay
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&HB0)) & Convert.ToString(Chr(&H0)))
            'Set Current Limit to 5 Amps
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&H49)) & Convert.ToString(Chr(&HC4)))
            'Set Voltage to 28
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&H5A)) & Convert.ToString(Chr(&HF0)))

            SetPowerIndicator(True)

            For loop1 = 180 To 0 Step -1
                oFrmDelay.Text1.Text = "Initializing the VEO2" & vbCrLf & loop1 & " sec"
                Delay(1)
                oFrmDelay.Refresh()
            Next loop1
            oFrmDelay.Visible = False
            oFrmDelay.Close()

            RESET_MODULE_INITIATE()
            Delay(1)

            gbEoPowerOnVIPERT = True

        Else
            '**** Turning off VEO2 Power ****
            oFrmDelay.Show()
            oFrmDelay.Text1.Text = "Turning off VEO2 Power"
            oFrmDelay.Refresh()
            oFrmDelay.BringToFront()

            goTimer.Enabled = False

            IRWIN_SHUTDOWN()
            Delay(1)

            oFrmDelay.Text1.Text = "Setting Power Supply 1 to 0 volts and amps"
            oFrmDelay.Refresh()
            oFrmDelay.BringToFront()

            'Disconnect two supplies
            'Set Supply 1 Voltage to 0
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&H50)) & Convert.ToString(Chr(&H0)))
            'Set Supply 1 Current Limit to 0 Amps
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&H40)) & Convert.ToString(Chr(&H0)))

            oFrmDelay.Text1.Text = "Setting Power Supply 3 to 0 volts and amps"
            oFrmDelay.Refresh()
            oFrmDelay.BringToFront()

            'Set Supply 3 Voltage to 0
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&H50)) & Convert.ToString(Chr(&H0)))
            'Set Supply 3 Current Limit to 0 Amps
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&H40)) & Convert.ToString(Chr(&H0)))

            oFrmDelay.Text1.Text = "Resetting power supplies 1-3"
            oFrmDelay.Refresh()
            oFrmDelay.BringToFront()

            'Power Supplies 1, 2 and 3
            SendDCPSCommand(1, Convert.ToString(Chr(&H11)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))
            SendDCPSCommand(2, Convert.ToString(Chr(&H12)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))
            SendDCPSCommand(3, Convert.ToString(Chr(&H13)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))

            gbEoPowerOnVIPERT = False
            SetPowerIndicator(False)
        End If
        oFrmDelay.Close()
        oFrmDelay = Nothing
        gofrmMain.Cursor = Cursors.WaitCursor
    End Sub

    ''' <summary>
    ''' Nomenclature   : APS 6062 UUT Power Supplies
    ''' Send a command string to the power supplies
    ''' </summary>
    ''' <param name="shSupply">
    '''     shSupply:    DCPS Supply Number
    ''' </param>
    ''' <param name="sCommand">
    '''     sCommand:   3-byte command string
    ''' </param>
    ''' <remarks>
    ''' EXAMPLE:
    '''     SendDCPSCommand (3, Command)
    ''' NOTE: All commands must be three bytes in length even if one of the bytes is a null byte.
    ''' All programmed voltages for the 40V supplies need to be multiplied by 100 (65V by 50) and converted to 12 bits.
    ''' All programmed currents need to be multiplied by 500 and converted to 12 bits.
    ''' </remarks>
    Private Sub SendDCPSCommand(ByVal shSupply As Short, ByVal sCommand As String)
        Dim iErrStat As Integer
        Dim iWriteLenReturned As Integer

        If sCommand.Length <> 3 Then
'            MsgBox("Invalid Command length in SendDCPSCommand Sub: " & sCommand, MsgBoxStyle.Critical, "Power Source Communication Error")
            Exit Sub
        End If

        iErrStat = atxmlDF_viWrite(garrsPsResourceName(shSupply - 1), 0, sCommand, CLng(sCommand.Length), iWriteLenReturned)

        'Copied from Freedom01502
        If iErrStat Or iWriteLenReturned = 0 Then
            MsgBox("Error in SendDCPSCommand for Slot: '" & garrsPsResourceName(shSupply - 1) & "'", MsgBoxStyle.Exclamation, "SendDCPSCommand Failed")
        End If

        Delay(1)
    End Sub

    Private Sub AddUpdateAppSettings(key As String, value As String)
        Try
            Dim configFile As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim settings As KeyValueConfigurationCollection = configFile.AppSettings.Settings

            If IsNothing(settings(key)) Then
                settings.Add(key, value)
            Else
                settings(key).Value = value
            End If

            configFile.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name)

        Catch e As ConfigurationErrorsException
            Throw
        End Try
    End Sub
End Module






