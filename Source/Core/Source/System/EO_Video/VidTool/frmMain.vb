Option Strict Off
Option Explicit On
Imports System.IO

''' <summary>
''' To display the Captured Image to the user and allow access to the Operators Instructions, as well as the About Form.
''' If in SINGLE Mode, allow User to Reacquire Image. The Type, Resolution and Mode are displayed in the Title Bar.
''' The Application can be terminated via the [Exit] button, "ALT+X" or X on the form.
''' The Form can be moved, but not resized.
''' </summary>
''' <remarks></remarks>
Friend Class frmMain
    Inherits System.Windows.Forms.Form

    Private sSetCurRad As String = "0" 'Current Radiance Setting
    Private sSetCurDt As String = "0.000" 'Current Delta T Setting
    Private sCurrVal(5) As String   'Current Set ValuesSet

    ''' <summary>
    ''' Main Program Entry Point
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True

        gofrmMain = Me
        VDisplay.Main()

        ' Set some maximum sizes
        Me.MaximumSize = New Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height)
        Me.pnlDispHolder.MaximumSize = New Size(Me.MaximumSize.Width - Me.tabAsset.Width - 10, Me.MaximumSize.Height - gboxSrcStage.Height - StatusStrip1.Height - 15)

        If Not IsNothing(gofrmAbout) Then
            gofrmAbout.tsStatusLabel1.Text = "Formatting Main form"
            gofrmAbout.Refresh()
        End If
        tsStatusLabel.Text = ""
        SizeForm()

        'Enabled Video Refresh Timer
        Me.Timer1.Enabled = True
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim iMsgLARRS As Short
        Dim shSetAZ As Short
        Dim shSetEL As Short
        Dim shConfig As Short
        Dim shNumTrys As Short
        Dim shCountI As Short = 0

        Me.Refresh()
        Me.Cursor = Cursors.WaitCursor
        gbMainShown = True
        If Not IsNothing(gofrmAbout) Then
            gofrmAbout.sbrInformation.Text = "Initializing Video Display...Please Wait"
            gofrmAbout.Refresh()
        End If
        tsStatusLabel.Text = "Initializing Video Display...Please Wait"
        goTimer = New Timer
        Try
            If gbRunFromNAM Then
                '**************************************************************************************
                '                       Command Line Arguments - NAM MODE
                '**************************************************************************************
                'Setup Video Capture Mode
                Select Case gshMode
                    Case ALIGN, FOV, TGTCOORD
                        If gshMode = ALIGN Then
                            gbOverlay = True
                        End If
                        OverlayTarget()

                    Case DISPLAY_ONLY
                        GrabLive_Pixci()
                        Delay(1)
                        gofrmMain.pboxDisplay.Invalidate()
                End Select

                gofrmMain.Cursor = Cursors.Default
                Exit Sub
            End If

            '**************************************************************************************
            '                       No Command Line Arguments - SAIS MODE
            '**************************************************************************************
            SetPowerIndicator(gbEoPowerOnVIPERT) 'Set Power Indicator on GUI

            If gbEoPowerOnVIPERT Then
                gofrmMain.Cursor = Cursors.Default

                iMsgLARRS = MsgBox("Do you want the LaRRS position set automatically?" & vbCrLf & vbCrLf, MsgBoxStyle.YesNo)

                gofrmMain.Cursor = Cursors.WaitCursor

                'Need to refresh form as may have above Dialog Box still showing without Repaint
                gofrmMain.Refresh()

                If iMsgLARRS = MsgBoxResult.Yes Then
                    With gofrmMain
                        .tabAsset.SelectedIndex = 2
                        .tabAsset.Focus()
                        .tsStatusLabel.Text = "Setting LaRRS position"
                        .StatusStrip1.Refresh()
                    End With

                    GetLaRRS()
                    SET_LARRS_AZ_LASER_INITIATE(CShort(garrshAzPos(0)))
                    Delay(1)
                    SET_LARRS_EL_LASER_INITIATE(garrshElPos(0))
                    Delay(1)

                    gofrmMain.txtSetElevation.Text = garrshElPos(0).ToString
                    gofrmMain.txtSetAzimuth.Text = garrshAzPos(0).ToString
                    sCurrVal(LEL) = gofrmMain.txtSetElevation.Text
                    sCurrVal(LAZ) = gofrmMain.txtSetAzimuth.Text

                    While shNumTrys < 50 And gbUpdateLARRS
                        gbUpdateLARRS = False

                        SET_LARRS_AZ_LASER_FETCH(shSetAZ)
                        Delay(1)
                        gofrmMain.txtVal3.Text = shSetAZ.ToString()
                        If CShort(gofrmMain.txtVal3.Text) <> garrshAzPos(0) Then
                            gbUpdateLARRS = True
                        End If

                        SET_LARRS_EL_LASER_FETCH(shSetEL)
                        Delay(1)

                        gofrmMain.txtVal2.Text = shSetEL.ToString()
                        If CShort(gofrmMain.txtVal2.Text) <> garrshElPos(0) Then
                            gbUpdateLARRS = True
                        End If

                        If shNumTrys = 50 Then
                            gofrmMain.Cursor = Cursors.Default
                            MsgBox("LARRS position setting has failed!", MsgBoxResult.Ok)
                            gofrmMain.Cursor = Cursors.WaitCursor
                        End If
                        shNumTrys = shNumTrys + 1
                    End While

                    gofrmMain.tabAsset.SelectedIndex = 0
                    gofrmMain.tabAsset.Focus()

                End If 'If iMsgLARRS = MsgBoxResult.Yes Then
            End If

            gofrmMain.tsStatusLabel.Text = "Setting System Configuration"
            gofrmMain.StatusStrip1.Refresh()
            gofrmMain.Cursor = Cursors.WaitCursor
            gofrmMain.Refresh()

            If gbEoPowerOnVIPERT Then
                SET_SYSTEM_CONFIGURATION_INITIATE(5)
                While shCountI < 30
                    SET_SYSTEM_CONFIGURATION_FETCH(shConfig)
                    Delay(2)
                    If shConfig <> 5 Then
                        shCountI = shCountI + 1
                    Else
                        shCountI = 30
                    End If
                    If shCountI = 29 Then
                        MessageBox.Show("EO system failed to set configuration!")
                        shCountI = 30
                    End If
                End While

                SELECT_DIODE_LASER_INITIATE(0)

                SET_LARRS_POLARIZE_LASER_INITIATE(0)
                Delay(0.5)
            End If
            gofrmMain.txtVal4.Text = "0"

            gofrmMain.BringToFront()

            gofrmMain.tsStatusLabel.Text = ""
            gofrmMain.StatusStrip1.Refresh()
            gofrmMain.Cursor = Cursors.Default
            gofrmMain.Enabled = True
            gofrmMain.Refresh()

        Catch ex As Exception
            MsgBox(String.Format("Unexpected Error in Event frmMain.Shown: Error Number = '{0}'; Description = '{1}'", Err.Number, Err.Description), MsgBoxStyle.Critical, "Unexpected Error")
            Application.Exit()
        Finally
            goTimer.Interval = 500 '1/2 Second
            AddHandler goTimer.Tick, AddressOf OnTimerEvent
            goTimer.Start()
            goTimer.Enabled = True

            If Not IsNothing(gofrmAbout) Then
                gofrmAbout.Hide()
            End If
            gofrmMain.Cursor = Cursors.Default
            gbIsInitializing = False
            Me.Visible = True
        End Try
    End Sub

    Private Sub frmMain_FormClosing(ByVal eventSender As Object, ByVal eventArgs As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim iMsgResponse As Short 'User answer from message box
        Dim iErrorStatus As Integer

        If gbRunFromNAM = True Then
            Select Case gshMode
                Case FOV, TGTCOORD
                    If gbROIFormOpen = False Then
                        'Warn User that this cause an error
                        iMsgResponse = MsgBox("WARNING!!!!" & vbCrLf & "Exiting this application will generate an ATLAS Error!" & vbCrLf & vbCrLf & "Do you still wish to Exit?",
                                              MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2)
                        If iMsgResponse = MsgBoxResult.Yes Then
                            Me.Enabled = False
                            gofrmMain.Cursor = Cursors.WaitCursor
                            UpdateData()
                            gofrmMain.Cursor = Cursors.Default
                        Else
                            eventArgs.Cancel = True 'Cancels Closing of Form
                            Exit Sub
                        End If
                    End If
            End Select
        Else
            gofrmMain.Cursor = Cursors.WaitCursor
            PowerOffAndShutDown() 'Put EO Instrument in Default Position
        End If

        Me.Enabled = False

        iErrorStatus = atxml_Close()

        Delay(1)

        gofrmMain.Cursor = Cursors.Default

        PXD_PIXCICLOSE()
    End Sub

    Private Sub pboxDisplay_Paint(sender As Object, e As PaintEventArgs) Handles pboxDisplay.Paint
        Dim Draw As Graphics = e.Graphics ' Create a local version of the graphics object for the PictureBox.
        Dim hDC As IntPtr = Draw.GetHdc() ' Get a handle to PictureBox1

        'Set resize mode to STRETCH_DELETESCANS
        SetStretchBltMode(hDC, STRETCH_DELETESCANS)

        'Draw image buffer scaled to the size of PictureBox1
        PXD_RENDERSTRETCHDIBITS(1, 1, 0, 0, -1, -1, 0, hDC, 0, 0, pboxDisplay.Width, pboxDisplay.Height, 0)

        Draw.ReleaseHdc(hDC) ' Release PictureBox1 handle

        If gbOverlay Then
            'Draw Lines over Image we just Drew
            Dim myPen As Pen = New Pen(Color.Red, 3)

            'Draw Horizontal Line
            Draw.DrawLine(pen:=myPen, x1:=0, y1:=CInt(pboxDisplay.Height / 2), x2:=pboxDisplay.Width, y2:=CInt(pboxDisplay.Height / 2))

            'Draw Vertical Line
            Draw.DrawLine(pen:=myPen, x1:=CInt(pboxDisplay.Width / 2), y1:=0, x2:=CInt(pboxDisplay.Width / 2), y2:=pboxDisplay.Height)
        End If
    End Sub

    Private Sub chkCameraPower_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles chkCameraPower.CheckedChanged
        Dim config As Short
        Dim cpwr As Short
        Dim x As Short
        Dim k As Short

        If gbIsInitializing Then Exit Sub

        If chkCameraPower.Checked Then
            Me.tsStatusLabel.Text = "Setting System Configuration...Please Wait"
            Me.StatusStrip1.Refresh()
            SET_SYSTEM_CONFIGURATION_INITIATE(6)
            While x < 30
                SET_SYSTEM_CONFIGURATION_FETCH(config)
                Delay(2)
                If config <> 6 Then
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

            Me.tsStatusLabel.Text = "Setting camera power ON...Please Wait"
            Me.StatusStrip1.Refresh()
            SET_CAMERA_POWER_INITIATE(1)
            While x < 30
                SET_CAMERA_POWER_FETCH(cpwr)
                Delay(2)
                If cpwr <> 1 Then
                    x = x + 1
                Else
                    x = 30
                End If
                If x = 29 Then
                    k = MsgBox("Camera failed to turn on", MsgBoxResult.Ok)
                    x = 30
                End If
            End While
            x = 0
        Else
            Me.tsStatusLabel.Text = "Setting camera power OFF...Please Wait"
            Me.StatusStrip1.Refresh()
            SET_CAMERA_POWER_INITIATE(0)
            While x < 30
                SET_CAMERA_POWER_FETCH(cpwr)
                Delay(2)
                If cpwr <> 0 Then
                    x = x + 1
                Else
                    x = 30
                End If
                If x = 29 Then
                    k = MsgBox("Camera failed to turn off", MsgBoxResult.Ok)
                    x = 30
                End If
            End While
            SET_SYSTEM_CONFIGURATION_INITIATE(5)
        End If

        Me.tsStatusLabel.Text = ""
        Me.StatusStrip1.Refresh()
        Me.BringToFront()
    End Sub

    Private Sub chkLiveDisplay_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles chkLiveDisplay.CheckedChanged
        If gbIsInitializing Or gbMainClosing Then Exit Sub

        If Not gbInitVideo Then Exit Sub

        If gofrmMain.chkLiveDisplay.Checked Then
            'GO LIVE
            If cmdAbout.Text = "&Refresh" Then cmdAbout.Text = "&About"
            pboxDisplay.Invalidate() ' causes PictureBox to redraw
            GrabLive_Pixci()
        Else
            'SNAPSHOT
            If cmdAbout.Text = "&About" Then cmdAbout.Text = "&Refresh"
            Snapshot_Pixci()    'Snapshot Vs Grab (Live)
            cmdAbout.Enabled = True
            pboxDisplay.Invalidate() ' causes PictureBox to redraw
        End If
    End Sub

    Private Sub cmdAbout_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdAbout.Click
        If cmdAbout.Text = "&About" Then
            gofrmAbout.tsStatusLabel1.Visible = False
            gofrmAbout.tsStatusLabel1.Text = ""
            gofrmAbout.sbrInformation.Refresh()
            gofrmAbout.cmdOK.Visible = True
            gofrmAbout.Show()
            gofrmAbout.Refresh()
        Else 'It's used to Refresh Display
            If chkLiveDisplay.Checked = False Then
                Snapshot_Pixci() 'Refresh Display with Snapshot
            End If
        End If
    End Sub

    Private Sub cmdAlign_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdAlign.Click
        Cursor = Cursors.WaitCursor
        Try
            Select Case cmdAlign.Text
                Case "Align On"
                    cmdCapture.Enabled = False
                    cmdAlign.Text = "Align Off"
                    gofrmMain.chkLiveDisplay.Checked = True
                    Me.chkLiveDisplay.Enabled = False
                    gbCapture = True
                    gbOverlay = True 'Set Overlay Flag
                    OverlayTarget()
                Case "Align Off"
                    cmdCapture.Enabled = True
                    cmdAlign.Text = "Align On"
                    Me.chkLiveDisplay.Enabled = True
                    gbCapture = False
                    gbOverlay = False 'Set Overlay Flag
                    Delay(1)
                    Me.pboxDisplay.Refresh()
                    If Me.chkLiveDisplay.Checked Then
                        Me.pboxDisplay.Refresh()
                        GrabLive_Pixci() 'Go Live
                    End If
                    Me.pboxDisplay.Refresh()
            End Select

        Catch ex As Exception
            MsgBox("Sub cmdAlign_Click() Error Number: " & Err.Number & "   Description: " & ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmdCapture_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdCapture.Click
        Cursor = Cursors.WaitCursor

        Try
            Select Case Trim(cmdCapture.Text)
                Case "&Capture"
                    Me.chkLiveDisplay.Checked = False
                    If cmdAlign.Text = "Align Off" Then cmdAlign_Click(cmdAlign, New EventArgs)
                    If gbOverlay = False Then
                        SaveImage()
                    Else
                        gbCaptureImage = True
                    End If

                Case "&Help"
                    'Only open one instance
                    If gbGraphicOpen = False Then
                        'Suspend video capture while showing HELP to prevent memory error
                        gbCapture = False
                        cmdCapture.Enabled = False
                        frmGraphic.Show()
                    End If

            End Select
        Catch ex As Exception
            MsgBox(String.Format("Unexpected Error in Sub cmdCapture_Click: Error Number = '{0}'; Description = '{1}'", Err.Number, Err.Description),
                   MsgBoxStyle.Critical, "Unexpected Error")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdClose.Click
        Cursor = Cursors.WaitCursor

        If IsNothing(goTimer) = False Then
            goTimer.Enabled = False
            goTimer.Stop()
            goTimer.Dispose()
        End If
        If IsNothing(pboxDisplay.Image) = False Then
            pboxDisplay.Image.Dispose()
        End If

        'Set to True so when gofrmMain.chkLiveDisplay_CheckedChanged fires it knows we are closing the application
        gbMainClosing = True
        PXD_PIXCICLOSE()
        Me.Close()
    End Sub

    Private Sub cmdEoPower_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdEOPower.Click
        Cursor = Cursors.WaitCursor
        Try
            cmdEOPower.Enabled = False
            If Me.cmdEOPower.Text = "Power On" Then
                PowerOnAndStartup()
            Else
                PowerOffAndShutDown()
            End If

            cmdEOPower.Enabled = True
            SizeForm()
        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CmdSetLARRS_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdSetLARRS.Click
        Dim shNumTrys As Short
        Dim shSetPOL As Short
        Dim shSetAZ As Short
        Dim shSetEL As Short
        Dim shReqPOL As Short
        Dim shReqAZ As Short
        Dim shReqEL As Short
        Dim config As Short
        Dim x As Short
        Dim k As Short
        Dim shreqconfig As Short = 6

        Me.tsStatusLabel.Text = "Setting LaRRS Position....Please Wait"
        Me.StatusStrip1.Refresh()

        SET_SYSTEM_CONFIGURATION_INITIATE(shreqconfig)
        While x < 30
            SET_SYSTEM_CONFIGURATION_FETCH(config)
            Delay(2)
            If config <> shreqconfig Then
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

        gbUpdateLARRS = True
        shReqAZ = CShort(Me.txtSetAzimuth.Text)
        shReqEL = CShort(Me.txtSetElevation.Text)
        shReqPOL = CShort(Me.txtSetPA.Text)

        SET_LARRS_POLARIZE_LASER_INITIATE(shReqPOL)
        Delay(1)

        SET_LARRS_AZ_LASER_INITIATE(shReqAZ)
        Delay(1)

        SET_LARRS_EL_LASER_INITIATE(shReqEL)
        Delay(1)

        shNumTrys = 0

        While shNumTrys < 40 And gbUpdateLARRS
            Application.DoEvents()

            gbUpdateLARRS = False

            SET_LARRS_AZ_LASER_FETCH(shSetAZ)
            Delay(1)
            Me.txtVal3.Text = CStr(shSetAZ)

            If shSetAZ <> shReqAZ Then
                'Didn't Work, Continue
                gbUpdateLARRS = True
            End If

            SET_LARRS_EL_LASER_FETCH(shSetEL)
            Delay(1)
            Me.txtVal2.Text = CStr(shSetEL)

            If shSetEL <> shReqEL Then
                gbUpdateLARRS = True
            End If

            SET_LARRS_POLARIZE_LASER_FETCH(shSetPOL)
            Delay(1)
            Me.txtVal4.Text = CStr(shSetPOL)

            If shSetPOL <> shReqPOL Then
                gbUpdateLARRS = True
            End If

            shNumTrys = shNumTrys + 1
        End While

        If shNumTrys = 40 Then
            MsgBox("LARRS position setting has failed!", MsgBoxResult.Ok)
        End If

        SET_SYSTEM_CONFIGURATION_INITIATE(5)

        Me.tsStatusLabel.Text = ""
        Me.StatusStrip1.Refresh()
    End Sub

    Private Sub cmdDiodePower_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdDiodePower.Click
        Dim config As Short
        Dim diodepwr As Short
        Dim shX As Short

        Cursor = Cursors.WaitCursor
        Try
            Me.tsStatusLabel.Text = "Setting Diode Power....Please Wait"
            Me.StatusStrip1.Refresh()

            SET_SYSTEM_CONFIGURATION_INITIATE(5)
            While shX < 30
                SET_SYSTEM_CONFIGURATION_FETCH(config)
                Delay(2)
                If config <> 5 Then
                    shX = shX + 1
                Else
                    shX = 30
                End If
                If shX = 29 Then
                    MsgBox("EO system failed to set configuration!", MsgBoxResult.Ok)
                    shX = 30
                    Exit Sub
                End If
            End While
            shX = 0

            If cmdDiodePower.Text = "Turn Diode Power On" Then
                SET_OPERATION_LASER_INITIATE(1) 'Turn on Laser
                Delay(1)

                'Disable controls that should not be set with the Laser power on
                cboLaserTrigger.Enabled = False
                cboDiode.Enabled = False
                pboxBeam.Visible = True
                panSetAmplitude.Enabled = False
                panSetPP.Enabled = False

                While shX < 10
                    SET_OPERATION_LASER_FETCH(diodepwr)
                    Delay(2)
                    If config <> 1 Then
                        shX = shX + 1
                    Else
                        shX = 10
                    End If
                    If shX = 9 Then
                        MsgBox("Diode Power failed to Turn ON!", MsgBoxResult.Ok)
                        shX = 10
                        cboLaserTrigger.Enabled = True
                        cboDiode.Enabled = True
                        pboxBeam.Visible = False
                        panSetPP.Enabled = True
                        panSetAmplitude.Enabled = True
                        Cursor = Cursors.Default
                        Me.tsStatusLabel.Text = ""
                        Exit Sub
                    End If
                End While
                shX = 0
                cmdDiodePower.Text = "Turn Diode Power Off"

            Else
                SET_OPERATION_LASER_INITIATE(0) 'Turn off Laser
                Delay(1)

                'Enable controls
                cboLaserTrigger.Enabled = True
                cboDiode.Enabled = True
                pboxBeam.Visible = False
                panSetPP.Enabled = True
                panSetAmplitude.Enabled = True

                While shX < 10
                    SET_OPERATION_LASER_FETCH(diodepwr)
                    Delay(2)
                    If config <> 0 Then
                        shX = shX + 1
                    Else
                        shX = 10
                    End If
                    If shX = 9 Then
                        MsgBox("Diode Power failed to Turn OFF!", MsgBoxResult.Ok)
                        shX = 10
                        'Disable controls that should not be set with the Laser power on
                        cboLaserTrigger.Enabled = False
                        cboDiode.Enabled = False
                        pboxBeam.Visible = True
                        panSetAmplitude.Enabled = False
                        panSetPP.Enabled = False
                        Cursor = Cursors.Default
                        Me.tsStatusLabel.Text = ""
                        Exit Sub
                    End If
                End While
                shX = 0
                cmdDiodePower.Text = "Turn Diode Power On"

            End If

            Me.tsStatusLabel.Text = ""
            Me.StatusStrip1.Refresh()
        Catch ex As Exception
            MsgBox(String.Format("Unexpected Error in Sub cmdDiodePower_Click: Error Number = '{0}'; Description = '{1}'", Err.Number, Err.Description),
                   MsgBoxStyle.Critical, "Unexpected Error")
        Finally
            Cursor = Cursors.Default
            Me.tsStatusLabel.Text = ""
        End Try
    End Sub

    Private Sub cmdLaserSet_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdLaserSet.Click
        Dim shReqDiode As Short
        Dim shReqTrigger As Short
        Dim shConfig As Short
        Dim shX As Short
        Dim shDiode As Short
        Dim shTrigger As Short

        Me.tsStatusLabel.Text = "Setting Laser Configuration....Please Wait"
        Me.StatusStrip1.Refresh()

        shX = 0
        SET_SYSTEM_CONFIGURATION_INITIATE(5)

        While shX < 30
            SET_SYSTEM_CONFIGURATION_FETCH(shConfig)
            Delay(2)
            If shConfig <> 5 Then
                shX = shX + 1
            Else
                shX = 30
            End If
            If shX = 29 Then
                MsgBox("EO system failed to set configuration!", MsgBoxResult.Ok)
                shX = 30
            End If
        End While
        shX = 0

        Select Case Me.cboDiode.Text
            Case "1570"
                shReqDiode = 0
            Case "1540"
                shReqDiode = 1
            Case "1064"
                shReqDiode = 2
        End Select

        SELECT_DIODE_LASER_INITIATE(CInt(shReqDiode))
        Delay(2)

        While shX < 30
            SELECT_DIODE_LASER_FETCH(shDiode)
            Delay(2)
            If shDiode <> shReqDiode Then
                shX = shX + 1
            Else
                shX = 30
            End If
            If shX = 29 Then
                MsgBox("EO system failed to select proper diode!", MsgBoxResult.Ok)
                shX = 30
            End If
        End While
        shX = 0

        Select Case Me.cboLaserTrigger.Text
            Case "Free Run"
                shReqTrigger = 1
            Case "Laser Pulse"
                shReqTrigger = 2
            Case "External"
                shReqTrigger = 3
        End Select

        SET_TRIGGER_SOURCE_LASER_INITIATE(CInt(shReqTrigger))
        Delay(2)

        While shX < 30
            SET_TRIGGER_SOURCE_LASER_FETCH(shTrigger)
            If shTrigger <> shReqTrigger Then
                Delay(2)
                SET_TRIGGER_SOURCE_LASER_INITIATE(CInt(shReqTrigger))
                shX = shX + 1
            Else
                shX = 30
            End If
            If shX = 29 Then
                MsgBox("EO system failed to select proper trigger!", MsgBoxResult.Ok)
                shX = 30
            End If
        End While
        shX = 0

        SET_PULSE_AMPLITUDE_LASER_INITIATE(CSng(Me.txtSetAmplitude.Text))
        Delay(1)

        SET_PULSE_PERIOD_LASER_INITIATE(CSng(Me.txtSetPP.Text))
        Delay(1)
        Me.tsStatusLabel.Text = ""
        Me.StatusStrip1.Refresh()
    End Sub

    Private Sub cmdSetIRTarget_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdSetIRTarget.Click
        On Error Resume Next

        'Disable the Target selector
        cboIR_TargetWheel.Enabled = False
        cmdSetIRTarget.Enabled = False

        If SetTargetPos(cboIR_TargetWheel.SelectedIndex) = False Then
            MsgBox("The EO system did not set to the selected Target.", MsgBoxStyle.Exclamation)
        Else
            cmdSetDT.Enabled = True
        End If

        cmdSetIRTarget.Enabled = True
        cboIR_TargetWheel.Enabled = True
    End Sub

    Private Sub cmdSetDT_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdSetDT.Click
        'Range: -15 to 35
        gofrmMain.txtSetRadiance.Text = 0.0
        gofrmMain.txtCurrRadiance.Text = 0.0

        If IsNumeric(gofrmMain.txtSetDT.Text) Then
            SetDeltaT()
        Else
            MsgBox("Invalid Numeric Entry!", MsgBoxResult.Ok)
            gofrmMain.txtSetDT.Text = 0.0
        End If
    End Sub

    Private Sub cmdSetRadiance_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdSetRadiance.Click
        'Range: OFF = (0)
        'Range: ON = .0005 to 5,000
        gofrmMain.txtAMT_Out.Text = 0.0
        gofrmMain.txtBLB_Out.Text = 0.0
        gofrmMain.txtSetDT.Text = 0.0

        If IsNumeric(gofrmMain.txtSetRadiance.Text) Then
            SetRadiance()
        Else
            MsgBox("Invalid Numeric Entry!", MsgBoxResult.Ok)
            gofrmMain.txtSetRadiance.Text = 0.0
        End If
    End Sub

    Private Sub cmdSetVisPos_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdSetVisPos.Click
        On Error Resume Next

        'Disable the Target selector
        cboVisWheel.Enabled = False

        cmdSetVisPos.Enabled = False
        If SetTargetPos(cboVisWheel.SelectedIndex) = False Then
            MsgBox("The EO system did not set to the selected Target.", MsgBoxStyle.Exclamation)
        Else
            cmdSetRadiance.Enabled = True
        End If

        cmdSetVisPos.Enabled = True
        cboVisWheel.Enabled = True
    End Sub

    Private Sub txtSetRadiance_Leave(ByVal eventSender As Object, ByVal eventArgs As EventArgs)

    End Sub

    Private Sub cboIR_TargetWheel_SelectedValueChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cboIR_TargetWheel.SelectedValueChanged
        If gbIsInitializing Then Exit Sub

        'Enable button to set to new position
        cmdSetIRTarget.Enabled = True
    End Sub

    Private Sub cboVisWheel_SelectedValueChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cboVisWheel.SelectedValueChanged
        If gbIsInitializing Then Exit Sub

        'Enable button to set new position
        cmdSetVisPos.Enabled = True
    End Sub

    Private Sub cboVisWheel_Enter(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cboVisWheel.Enter
        'Enable button to set new position
        cmdSetVisPos.Enabled = True
    End Sub

    Private Sub txtSetRadiance_Enter(sender As Object, e As EventArgs) Handles txtSetRadiance.Enter
        txtSetRadiance.SelectAll()
    End Sub

    Private Sub txtSetRadiance_TextChanged(sender As Object, e As EventArgs) Handles txtSetRadiance.TextChanged
        If Not IsNumeric(txtSetRadiance.Text) Then
            'Entery is non numeric and over three characters in length, it's Invalid; set to DEF
            If Len(txtSetRadiance.Text) > 4 Then txtSetRadiance.Text = 0

            Select Case Trim(UCase(txtSetRadiance.Text))
                Case "DEF"
                    txtSetRadiance.Text = 0
                Case "MIN"
                    txtSetRadiance.Text = 0.0005
                Case "MAX"
                    txtSetRadiance.Text = 5000
            End Select
        Else
            If Val(txtSetRadiance.Text) > 5000 Then txtSetRadiance.Text = 5000
        End If
    End Sub

    Private Sub txtSetRadiance_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSetRadiance.KeyPress
        Dim KeyAscii As Integer = Asc(e.KeyChar)

        'If user presses Enter, Format Setting to the Instruments resolution
        If KeyAscii = 13 Then
            txtCurrRadiance.Focus()
            Exit Sub
        End If

        '***  Filter out any illegal Key Strokes  ***
        Select Case KeyAscii
            Case 8             'BackSpace,
            Case 46                             ' "."
                'Only allow one instance of "."
                If InStr(1, txtSetRadiance.Text, ".") Then
                    e.Handled = True
                End If
                'Filter out any other nonNumeric Characters
            Case Else
                If KeyAscii < 48 Or KeyAscii > 57 Then
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub SpinRadiance_DownButtonClicked(sender As Object, e As EventArgs) Handles SpinRadiance.DownButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        OldVal = Val(sSetCurRad)
        If OldVal > 1 Then
            NewVal = Val(OldVal) - 1
        ElseIf OldVal <= 1 And OldVal > 0.1 Then
            NewVal = Val(OldVal) - 0.1
        ElseIf OldVal <= 0.1 And OldVal > 0.01 Then
            NewVal = Val(OldVal) - 0.01
        ElseIf OldVal <= 0.01 And OldVal > 0.001 Then
            NewVal = Val(OldVal) - 0.001
        ElseIf OldVal <= 0.001 And OldVal >= 0.0005 Then
            NewVal = Val(OldVal) - 0.0001
        End If

        If NewVal >= 0 Then
            If NewVal < 0.00045 Then
                sSetCurRad = 0
            Else
                sSetCurRad = Str(NewVal)
            End If
        Else
            sSetCurRad = 0
        End If

        txtSetRadiance.Text = Convert.ToDouble(sSetCurRad).ToString("0.0000")
    End Sub

    Private Sub SpinRadiance_UpButtonClicked(sender As Object, e As EventArgs) Handles SpinRadiance.UpButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        OldVal = Val(sSetCurRad)
        If OldVal >= 1 Then
            NewVal = Val(OldVal) + 1
        ElseIf OldVal < 1 And OldVal >= 0.1 Then
            NewVal = Val(OldVal) + 0.1
        ElseIf OldVal < 0.1 And OldVal >= 0.01 Then
            NewVal = Val(OldVal) + 0.01
        ElseIf OldVal < 0.01 And OldVal >= 0.001 Then
            NewVal = Val(OldVal) + 0.001
        ElseIf OldVal < 0.001 And OldVal >= 0.0005 Then
            NewVal = Val(OldVal) + 0.0001
        ElseIf OldVal = 0 Then
            NewVal = Val(0.0005)
        End If

        If NewVal <= 5000 Then
            If NewVal < 0.0005 And NewVal <> 0 Then
                sSetCurRad = 0.0005
            Else
                sSetCurRad = NewVal.ToString("0.0000")
            End If
        Else
            sSetCurRad = 5000
        End If

        txtSetRadiance.Text = Convert.ToDouble(sSetCurRad).ToString("0.0000")
    End Sub

    Private Sub txtSetRadiance_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSetRadiance.Validating
        If (IsNumeric(txtSetRadiance.Text)) Then
            If Val(txtSetRadiance.Text) < 0.0005 Then
                txtSetRadiance.Text = 0
            ElseIf Val(txtSetRadiance.Text) > 5000 Then
                txtSetRadiance.Text = 5000
            End If
            sSetCurRad = txtSetRadiance.Text
        End If
    End Sub

    Private Sub spinSetDT_DownButtonClicked(sender As Object, e As EventArgs) Handles spinSetDT.DownButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        OldVal = Val(sSetCurDt)
        NewVal = Val(sSetCurDt) - 1
        If NewVal >= -15 Then
            sSetCurDt = Str(NewVal)
        Else
            sSetCurDt = -15
        End If
        txtSetDT.Text = Convert.ToDouble(sSetCurDt).ToString("0.000")
    End Sub

    Private Sub spinSetDT_UpButtonClicked(sender As Object, e As EventArgs) Handles spinSetDT.UpButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        OldVal = Val(sSetCurDt)
        NewVal = Val(sSetCurDt) + 1
        If NewVal <= 35 Then
            sSetCurDt = Str(NewVal)
        Else
            sSetCurDt = 35
        End If
        txtSetDT.Text = Convert.ToDouble(sSetCurDt).ToString("0.000")
    End Sub

    Private Sub txtSetDT_TextChanged(sender As Object, e As EventArgs) Handles txtSetDT.TextChanged
        If IsNumeric(txtSetDT.Text) Then
            'Verify setings are within limits
            If Val(txtSetDT.Text) > 35 Then txtSetDT.Text = 35
            If Val(txtSetDT.Text) < -15 Then txtSetDT.Text = -15
        End If
    End Sub

    Private Sub txtSetDT_Enter(sender As Object, e As EventArgs) Handles txtSetDT.Enter
        txtSetDT.SelectAll()
    End Sub

    Private Sub txtSetDT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSetDT.KeyPress
        Dim KeyAscii As Integer = Asc(e.KeyChar)

        'If user presses Enter, Format Setting to the Instrument's resolution
        If KeyAscii = 13 Then
            txtAMT_Out.Focus()
            Exit Sub
        End If

        '***  Filter out any illegal Key Strokes  ***
        Select Case KeyAscii

            Case 8, 43, 45             'BackSpace, "+", "-"
            Case 46                             ' "."
                'Only allow one instance of "."
                If InStr(1, txtSetDT.Text, ".") Then
                    e.Handled = True
                End If
                'Filter out any other nonNumeric Characters
            Case Else
                If KeyAscii < 48 Or KeyAscii > 57 Then
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub txtSetDT_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSetDT.Validating
        If Val(txtSetDT.Text) < -15 Then
            txtSetDT.Text = -15
        ElseIf Val(txtSetDT.Text) > 35 Then
            txtSetDT.Text = 35
        End If

        sSetCurDt = txtSetDT.Text
    End Sub

    Private Sub SpinSetSetAmplitude_DownButtonClicked(sender As Object, e As EventArgs) Handles SpinSetSetAmplitude.DownButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = AMPL

        dOldVal = Val(sCurrVal(Index))
        dNewVal = Val(sCurrVal(Index)) - 1
        If dNewVal >= nLowerLimit(Index) Then
            If dNewVal < nLowerLimit(Index) Then
                sCurrVal(Index) = nLowerLimit(Index)
            Else
                sCurrVal(Index) = Str(dNewVal)
            End If
        Else
            sCurrVal(Index) = nLowerLimit(Index)
        End If

        txtSetAmplitude.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub SpinSetSetAmplitude_UpButtonClicked(sender As Object, e As EventArgs) Handles SpinSetSetAmplitude.UpButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = AMPL

        dOldVal = Val(sCurrVal(Index))
        dNewVal = Val(sCurrVal(Index)) + 1
        If dNewVal <= nUpperLimit(Index) Then
            sCurrVal(Index) = Str(dNewVal)
        Else
            sCurrVal(Index) = nUpperLimit(Index)
        End If

        txtSetAmplitude.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub txtSetAmplitude_TextChanged(sender As Object, e As EventArgs) Handles txtSetAmplitude.TextChanged
        Dim Index As Integer = AMPL

        If Not IsNumeric(txtSetAmplitude.Text) Then
            'Entry is non numeric and over three characters in length, it's Invalid; set to DEF
            If txtSetAmplitude.Text.Length > 3 Then txtSetAmplitude.Text = 0

            Select Case Trim(UCase(txtSetAmplitude.Text))
                Case "DEF"
                    txtSetAmplitude.Text = CStr(nSetDefault(Index))
                Case "MIN"
                    txtSetAmplitude.Text = CStr(nLowerLimit(Index))
                Case "MAX"
                    txtSetAmplitude.Text = CStr(nUpperLimit(Index))
            End Select
        End If
    End Sub

    Private Sub txtSetAmplitude_Enter(sender As Object, e As EventArgs) Handles txtSetAmplitude.Enter
        txtSetAmplitude.SelectAll()
    End Sub

    Private Sub txtSetAmplitude_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSetAmplitude.KeyPress
        Dim KeyAscii As Integer = Asc(e.KeyChar)

        'If user presses Enter, Format Setting to the Instruments resolution
        If (KeyAscii = 13) Or (KeyAscii = Asc(vbTab)) Then
            cmdDiodePower.Focus()
            Exit Sub
        End If

        '***  Filter out any illegal Key Strokes  ***
        Select Case KeyAscii
            Case 8
            Case 46                             ' "."
                'Only allow one instance of "."
                If InStr(1, txtSetAmplitude.Text, ".") Then
                    e.Handled = True
                End If
                'Filter out any other nonNumeric Characters
            Case Else
                If KeyAscii < 48 Or KeyAscii > 57 Then
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub txtSetAmplitude_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSetAmplitude.Validating
        Dim Index As Integer = AMPL

        If Val(txtSetAmplitude.Text) < nLowerLimit(Index) Then
            txtSetAmplitude.Text = nLowerLimit(Index).ToString()
        ElseIf Val(txtSetAmplitude.Text) > nUpperLimit(Index) Then
            txtSetAmplitude.Text = nUpperLimit(Index).ToString()
        End If

        sCurrVal(Index) = txtSetAmplitude.Text
    End Sub

    Private Sub SpinSetPP_DownButtonClicked(sender As Object, e As EventArgs) Handles SpinSetPP.DownButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = PULPER

        If txtSetPP.Text.Length > 0 Then
            dOldVal = Convert.ToDouble(txtSetPP.Text)
            dNewVal = Convert.ToDouble(txtSetPP.Text) - 1
        ElseIf Not IsNothing(sCurrVal(Index)) Then
            dOldVal = Val(sCurrVal(Index))
            dNewVal = Val(sCurrVal(Index)) - 1
        Else
            dOldVal = nSetDefault(Index)
            dNewVal = nSetDefault(Index) - 1
        End If

        If dNewVal >= nLowerLimit(Index) Then
            If dNewVal < nLowerLimit(Index) Then
                sCurrVal(Index) = nLowerLimit(Index)
            Else
                sCurrVal(Index) = Str(dNewVal)
            End If
        Else
            sCurrVal(Index) = nLowerLimit(Index)
        End If

        txtSetPP.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub SpinSetPP_UpButtonClicked(sender As Object, e As EventArgs) Handles SpinSetPP.UpButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = PULPER

        If txtSetPP.Text.Length > 0 Then
            dOldVal = Convert.ToDouble(txtSetPP.Text)
            dNewVal = Convert.ToDouble(txtSetPP.Text) + 1
        ElseIf Not IsNothing(sCurrVal(Index)) Then
            dOldVal = Val(sCurrVal(Index))
            dNewVal = Val(sCurrVal(Index)) + 1
        Else
            dOldVal = nSetDefault(Index)
            dNewVal = nSetDefault(Index) + 1
        End If

        If dNewVal <= nUpperLimit(Index) Then
            sCurrVal(Index) = Str(dNewVal)
        Else
            sCurrVal(Index) = nUpperLimit(Index)
        End If

        txtSetPP.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub txtSetPP_TextChanged(sender As Object, e As EventArgs) Handles txtSetPP.TextChanged
        Dim Index As Integer = PULPER

        If Not IsNumeric(txtSetPP.Text) Then
            'Entry is non numeric and over three characters in length, it's Invalid; set to DEF
            If txtSetPP.Text.Length > 3 Then txtSetPP.Text = 0

            Select Case Trim(UCase(txtSetPP.Text))
                Case "DEF"
                    txtSetPP.Text = CStr(nSetDefault(Index))
                Case "MIN"
                    txtSetPP.Text = CStr(nLowerLimit(Index))
                Case "MAX"
                    txtSetPP.Text = CStr(nUpperLimit(Index))
            End Select
        End If
    End Sub

    Private Sub txtSetPP_Enter(sender As Object, e As EventArgs) Handles txtSetPP.Enter
        txtSetPP.SelectAll()
    End Sub

    Private Sub txtSetPP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSetPP.KeyPress
        Dim KeyAscii As Integer = Asc(e.KeyChar)

        'If user presses Enter, Format Setting to the Instruments resolution
        If (KeyAscii = 13) Or (KeyAscii = Asc(vbTab)) Then
            cmdDiodePower.Focus()
            Exit Sub
        End If

        '***  Filter out any illegal Key Strokes  ***
        Select Case KeyAscii

            'Allow DEF, MIN, MAX, def, min, max
            Case 65, 68, 70, 73, 77 To 78, 88, 97, 100, 102, 105, 109 To 110, 120
            Case 8
            Case 69, 101             'BackSpace, "E", "e"
                'Only allow one E or e
                If InStr(1, txtSetPP.Text, "E") Then
                    e.Handled = True
                ElseIf InStr(1, txtSetPP.Text, "e") Then
                    e.Handled = True
                End If

            Case 46                             ' "."
                'Only allow one instance of "."
                If InStr(1, txtSetPP.Text, ".") Then
                    e.Handled = True
                End If
                'Filter out any other nonNumeric Characters
            Case Else
                If KeyAscii < 48 Or KeyAscii > 57 Then
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub txtSetPP_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSetPP.Validating
        Dim Index As Integer = PULPER

        If Val(txtSetPP.Text) < nLowerLimit(Index) Then
            txtSetPP.Text = nLowerLimit(Index).ToString()
        ElseIf Val(txtSetPP.Text) > nUpperLimit(Index) Then
            txtSetPP.Text = nUpperLimit(Index).ToString()
        End If

        sCurrVal(Index) = txtSetPP.Text
    End Sub

    Private Sub SpinSetAzimuth_DownButtonClicked(sender As Object, e As EventArgs) Handles SpinSetAzimuth.DownButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = LAZ

        dOldVal = Val(sCurrVal(Index))
        dNewVal = Val(sCurrVal(Index)) - 1
        If dNewVal >= nLowerLimit(Index) Then
            If dNewVal < nLowerLimit(Index) Then
                sCurrVal(Index) = nLowerLimit(Index)
            Else
                sCurrVal(Index) = Str(dNewVal)
            End If
        Else
            sCurrVal(Index) = nLowerLimit(Index)
        End If

        txtSetAzimuth.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub SpinSetAzimuth_UpButtonClicked(sender As Object, e As EventArgs) Handles SpinSetAzimuth.UpButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = LAZ

        dOldVal = Val(sCurrVal(Index))
        dNewVal = Val(sCurrVal(Index)) + 1
        If dNewVal <= nUpperLimit(Index) Then
            sCurrVal(Index) = Str(dNewVal)
        Else
            sCurrVal(Index) = nUpperLimit(Index)
        End If

        txtSetAzimuth.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub txtSetAzimuth_TextChanged(sender As Object, e As EventArgs) Handles txtSetAzimuth.TextChanged
        Dim Index As Integer = LAZ

        If Not IsNumeric(txtSetAzimuth.Text) Then
            'Entry is non numeric and over three characters in length, it's Invalid; set to DEF
            If txtSetAzimuth.Text.Length > 3 Then txtSetAzimuth.Text = 0

            Select Case Trim(UCase(txtSetAzimuth.Text))
                Case "DEF"
                    txtSetAzimuth.Text = CStr(nSetDefault(Index))
                Case "MIN"
                    txtSetAzimuth.Text = CStr(nLowerLimit(Index))
                Case "MAX"
                    txtSetAzimuth.Text = CStr(nUpperLimit(Index))
            End Select
        End If
    End Sub

    Private Sub txtSetAzimuth_Enter(sender As Object, e As EventArgs) Handles txtSetAzimuth.Enter
        txtSetAzimuth.SelectAll()
    End Sub

    Private Sub txtSetAzimuth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSetAzimuth.KeyPress
        Dim KeyAscii As Integer = Asc(e.KeyChar)

        'If user presses Enter, Format Setting to the Instruments resolution
        If (KeyAscii = 13) Or (KeyAscii = Asc(vbTab)) Then
            cmdDiodePower.Focus()
            Exit Sub
        End If

        '***  Filter out any illegal Key Strokes  ***
        Select Case KeyAscii

            Case 8
                'Filter out any other nonNumeric Characters
            Case Else
                If KeyAscii < 48 Or KeyAscii > 57 Then
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub txtSetAzimuth_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSetAzimuth.Validating
        Dim Index As Integer = LAZ

        If Val(txtSetAzimuth.Text) < nLowerLimit(Index) Then
            txtSetAzimuth.Text = nLowerLimit(Index).ToString()
        ElseIf Val(txtSetAzimuth.Text) > nUpperLimit(Index) Then
            txtSetAzimuth.Text = nUpperLimit(Index).ToString()
        End If

        sCurrVal(Index) = txtSetAzimuth.Text
    End Sub

    Private Sub SpinSetElevation_DownButtonClicked(sender As Object, e As EventArgs) Handles SpinSetElevation.DownButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = LEL

        dOldVal = Val(sCurrVal(Index))
        dNewVal = Val(sCurrVal(Index)) - 1
        If dNewVal >= nLowerLimit(Index) Then
            If dNewVal < nLowerLimit(Index) Then
                sCurrVal(Index) = nLowerLimit(Index)
            Else
                sCurrVal(Index) = Str(dNewVal)
            End If
        Else
            sCurrVal(Index) = nLowerLimit(Index)
        End If

        txtSetElevation.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub SpinSetElevation_UpButtonClicked(sender As Object, e As EventArgs) Handles SpinSetElevation.UpButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = LEL

        dOldVal = Val(sCurrVal(Index))
        dNewVal = Val(sCurrVal(Index)) + 1
        If dNewVal <= nUpperLimit(Index) Then
            sCurrVal(Index) = Str(dNewVal)
        Else
            sCurrVal(Index) = nUpperLimit(Index)
        End If

        txtSetElevation.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub txtSetElevation_TextChanged(sender As Object, e As EventArgs) Handles txtSetElevation.TextChanged
        Dim Index As Integer = LEL

        If Not IsNumeric(txtSetElevation.Text) Then
            'Entry is non numeric and over three characters in length, it's Invalid; set to DEF
            If txtSetElevation.Text.Length > 3 Then txtSetElevation.Text = 0

            Select Case Trim(UCase(txtSetElevation.Text))
                Case "DEF"
                    txtSetElevation.Text = CStr(nSetDefault(Index))
                Case "MIN"
                    txtSetElevation.Text = CStr(nLowerLimit(Index))
                Case "MAX"
                    txtSetElevation.Text = CStr(nUpperLimit(Index))
            End Select
        End If
    End Sub

    Private Sub txtSetElevation_Enter(sender As Object, e As EventArgs) Handles txtSetElevation.Enter
        txtSetElevation.SelectAll()
    End Sub

    Private Sub txtSetElevation_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSetElevation.KeyPress
        Dim KeyAscii As Integer = Asc(e.KeyChar)

        'If user presses Enter, Format Setting to the Instruments resolution
        If (KeyAscii = 13) Or (KeyAscii = Asc(vbTab)) Then
            cmdDiodePower.Focus()
            Exit Sub
        End If

        '***  Filter out any illegal Key Strokes  ***
        Select Case KeyAscii
            Case 8
                'Filter out any other nonNumeric Characters
            Case Else
                If KeyAscii < 48 Or KeyAscii > 57 Then
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub txtSetElevation_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSetElevation.Validating
        Dim Index As Integer = LEL

        If Val(txtSetElevation.Text) < nLowerLimit(Index) Then
            txtSetElevation.Text = nLowerLimit(Index).ToString()
        ElseIf Val(txtSetElevation.Text) > nUpperLimit(Index) Then
            txtSetElevation.Text = nUpperLimit(Index).ToString()
        End If

        sCurrVal(Index) = txtSetElevation.Text
    End Sub

    Private Sub SpinSetPA_DownButtonClicked(sender As Object, e As EventArgs) Handles SpinSetPA.DownButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = LPOL

        dOldVal = Val(sCurrVal(Index))
        dNewVal = Val(sCurrVal(Index)) - 1
        If dNewVal >= nLowerLimit(Index) Then
            If dNewVal < nLowerLimit(Index) Then
                sCurrVal(Index) = nLowerLimit(Index)
            Else
                sCurrVal(Index) = Str(dNewVal)
            End If
        Else
            sCurrVal(Index) = nLowerLimit(Index)
        End If

        txtSetPA.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub SpinSetPA_UpButtonClicked(sender As Object, e As EventArgs) Handles SpinSetPA.UpButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double
        Dim Index As Integer = LPOL

        dOldVal = Val(sCurrVal(Index))
        dNewVal = Val(sCurrVal(Index)) + 1
        If dNewVal <= nUpperLimit(Index) Then
            sCurrVal(Index) = Str(dNewVal)
        Else
            sCurrVal(Index) = nUpperLimit(Index)
        End If

        txtSetPA.Text = Convert.ToInt32(sCurrVal(Index)).ToString("0")
    End Sub

    Private Sub txtSetPA_TextChanged(sender As Object, e As EventArgs) Handles txtSetPA.TextChanged
        Dim Index As Integer = LPOL

        If Not IsNumeric(txtSetPA.Text) Then
            'Entry is non numeric and over three characters in length, it's Invalid; set to DEF
            If txtSetPA.Text.Length > 3 Then txtSetPA.Text = 0

            Select Case Trim(UCase(txtSetPA.Text))
                Case "DEF"
                    txtSetPA.Text = CStr(nSetDefault(Index))
                Case "MIN"
                    txtSetPA.Text = CStr(nLowerLimit(Index))
                Case "MAX"
                    txtSetPA.Text = CStr(nUpperLimit(Index))
            End Select
        End If
    End Sub

    Private Sub txtSetPA_Enter(sender As Object, e As EventArgs) Handles txtSetPA.Enter
        txtSetPA.SelectAll()
    End Sub

    Private Sub txtSetPA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSetPA.KeyPress
        Dim KeyAscii As Integer = Asc(e.KeyChar)

        'If user presses Enter, Format Setting to the Instruments resolution
        If (KeyAscii = 13) Or (KeyAscii = Asc(vbTab)) Then
            cmdDiodePower.Focus()
            Exit Sub
        End If

        '***  Filter out any illegal Key Strokes  ***
        Select Case KeyAscii
            Case 8             'BackSpace,
                'Filter out any other nonNumeric Characters
            Case Else
                If KeyAscii < 48 Or KeyAscii > 57 Then
                    e.Handled = True
                End If
        End Select


    End Sub

    Private Sub txtSetPA_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSetPA.Validating
        Dim Index As Integer = LPOL

        If Val(txtSetPA.Text) < nLowerLimit(Index) Then
            txtSetPA.Text = nLowerLimit(Index).ToString()
        ElseIf Val(txtSetPA.Text) > nUpperLimit(Index) Then
            txtSetPA.Text = nUpperLimit(Index).ToString()
        End If

        sCurrVal(Index) = txtSetPA.Text
    End Sub

    Public Sub SizeForm()
        Dim bCamTallerThanTabAsset As Boolean = False
        Dim bOverSizeWidth As Boolean 'Flag: Show Scroll Bars
        Dim bOverSizeHeight As Boolean 'Flag: Show Scroll Bars
        Dim iNewClientWidth As Integer
        Dim iNewClientHeight As Integer
        Dim iNewWidth As Integer
        Dim iNewHeight As Integer
        Dim dHeight As Double = 0.0
        Dim dWidth As Double = 0.0
        Dim dmaxDispHolderHeight As Double = 0.0
        On Error GoTo ErrorHandler 'Trap Errors

        If WindowState = FormWindowState.Minimized Then Exit Sub

        'If no image Height and Width, set to 640 X 480
        If IsNothing(gpixciCamera) OrElse gpixciCamera.maxWidth = 0 Then
            MsgBox("No Video Format specified.  Display will be set to 640 X 480", MsgBoxStyle.Exclamation)
            gpixciCamera.maxWidth = 640
            gpixciCamera.maxHeight = 480
        End If

        gofrmMain.SuspendLayout()
        If gbRunFromNAM = False Then
            '----------------------------------------------------------------------------------------------------------------------------------------------------------
            '>>>>>>>>>>>>>>>>>  SAIS MODE  <<<<<<<<<<<<<<<<<<<<
            '----------------------------------------------------------------------------------------------------------------------------------------------------------

            ' Some frame formats are too small to use as sizing due to the size of the tab control
            ' so adjust accordingly
            If gpixciCamera.maxHeight > tabAsset.Height Then
                dHeight = gpixciCamera.maxHeight + gboxSrcStage.Height + StatusStrip1.Height + 80
            Else
                dHeight = tabAsset.Height + gboxSrcStage.Height + StatusStrip1.Height + 80
            End If

            Me.Size = New Size(gpixciCamera.maxWidth + Me.tabAsset.Width + 40, dHeight)

            ' This will be restricted by the Max size specified in the Form Load
            dmaxDispHolderHeight = Me.Height - gboxSrcStage.Height - StatusStrip1.Height - 80
            If gpixciCamera.maxHeight > dmaxDispHolderHeight Then
                Me.pnlDispHolder.Size = New Size(gpixciCamera.maxWidth, dmaxDispHolderHeight)
            Else
                Me.pnlDispHolder.Size = New Size(gpixciCamera.maxWidth, gpixciCamera.maxHeight)
            End If

            'ALWAYS Set Picturebox Size = Actual Size, Panel will have Scroll Bars as applicable
            pboxDisplay.Size = New Size(gpixciCamera.maxWidth, gpixciCamera.maxHeight)

            If (gbEoPowerOnVIPERT = False) And (gbEoPowerOnRemote = False) Then
                lblAsset.Visible = True
                lblAsset.BringToFront()
                tabAsset.Visible = False
                lblAsset.Left = Me.Width - lblAsset.Width - 25
            Else
                tabAsset.Visible = True
                tabAsset.BringToFront()
                lblAsset.Visible = False
                InitAssetVar()
                tabAsset.Left = Me.Width - tabAsset.Width - 25
            End If

            chkLiveDisplay.Visible = True
            cmdCapture.Visible = True
            cmdAlign.Visible = True
            cmdAbout.Visible = True
            cmdEOPower.Visible = True
            gboxEOPower.Visible = True
            gboxSensStage.Visible = True
            gboxSrcStage.Visible = True

            '-------------------------------------
            ' Position Controls Now
            '-------------------------------------

            'Close Button
            cmdClose.Top = Me.Height - cmdClose.Height - StatusStrip1.Height - 50
            cmdClose.Left = Me.Width - cmdClose.Width - 25

            'About Button
            cmdAbout.Top = Me.Height - gboxSrcStage.Height - StatusStrip1.Height - 50
            cmdAbout.Left = cmdClose.Left

            'Capture Button
            cmdCapture.Top = cmdClose.Top
            cmdCapture.Left = cmdAbout.Left - cmdCapture.Width - 10

            'Align Button
            cmdAlign.Top = cmdAbout.Top
            cmdAlign.Left = cmdCapture.Left

            'Power On Button
            cmdEOPower.Top = cmdClose.Top
            cmdEOPower.Left = cmdCapture.Left - chkLiveDisplay.Width - 10

            'Live Checkbox
            chkLiveDisplay.Top = cmdAbout.Top
            chkLiveDisplay.Left = cmdCapture.Left - chkLiveDisplay.Width - 10

            'EoPower Groupbox
            gboxEOPower.Top = cmdAbout.Top
            gboxEOPower.Left = chkLiveDisplay.Left - gboxEOPower.Width - 10

            'Sensor Stage Group Box
            gboxSensStage.Top = cmdAbout.Top
            gboxSensStage.Left = gboxEOPower.Left - gboxSensStage.Width - 10

            'Source Stage Group Box
            gboxSrcStage.Top = cmdAbout.Top
            gboxSrcStage.Left = gboxSensStage.Left - gboxSrcStage.Width - 10

            'IF INSTRUCTIONS THEN
            lblInstructions.Top = gboxSrcStage.Top - (2 * txtInstructions.Height)

            'Instructions Textbox
            txtInstructions.Top = lblInstructions.Bottom + CInt(0.5 * lblInstructions.Height)

            'If the EO was powered up when the application started, don't allow the user to 
            'turn EO power off
            If gbStartedWithPower Then
                cmdEOPower.Visible = False
            End If

            If Not gbLaunchNoPower Then
                cmdAlign.Enabled = True
                cmdCapture.Enabled = True
            End If
        Else
            '----------------------------------------------------------------------------------------------------------------------------------------------------------
            '>>>>>>>>>>>>>>>  Running from NAM  <<<<<<<<<<<<<<<<<
            '----------------------------------------------------------------------------------------------------------------------------------------------------------

            'Width: DO NOT SHOW tabAsset therefore it is NOT Part of Sizing like code above
            'Height: Depends on what is visible unlike code above
            tabAsset.Visible = False
            cmdAlign.Visible = False
            cmdCapture.Visible = False
            cmdEOPower.Visible = False
            gboxEOPower.Visible = False
            gboxSensStage.Visible = False
            gboxSrcStage.Visible = False

            Select Case gshMode
                Case FOV, TGTCOORD
                    Height = pboxDisplay.Height + 34
                Case Else
                    If gbInstructions = True Then 'Show the Instructions Text Box
                        txtInstructions.Visible = True 'Make Text Box Visible
                        lblInstructions.Visible = True '***Operator Instructions*** Banner
                        txtInstructions.Text = gsInstructions 'Fill with Passed Instructions
                    End If

                    If gbInstructions = True Then
                        'Increase Form Height If Instructions are displayed
                        'Position Instruction Text Box
                        'If mbOverSize Then
                        'Position ***Operator Instructions*** Banner
                    End If
            End Select

            If gshMode = ALIGN And gbHelpGraphic = True Then
                'Setup [Help] Button
                cmdCapture.Visible = True
                cmdCapture.Text = "&Help"
            End If

            If gpixciCamera.maxWidth > Screen.PrimaryScreen.WorkingArea.Width Then
                dWidth = Screen.PrimaryScreen.WorkingArea.Width
            Else
                dWidth = gpixciCamera.maxWidth + 16 ' 16 is width of the window borders (8 pixels each side)
            End If

            If gbInstructions Then
                If (gpixciCamera.maxHeight + txtInstructions.Height + lblInstructions.Height + StatusStrip1.Height + 100) > Screen.PrimaryScreen.WorkingArea.Height Then
                    dHeight = Screen.PrimaryScreen.WorkingArea.Height
                Else
                    dHeight = gpixciCamera.maxHeight + txtInstructions.Height + lblInstructions.Height + StatusStrip1.Height + 100
                End If
            Else
                If (gpixciCamera.maxHeight + StatusStrip1.Height + 100) > Screen.PrimaryScreen.WorkingArea.Height Then
                    dHeight = Screen.PrimaryScreen.WorkingArea.Height
                Else
                    dHeight = gpixciCamera.maxHeight + StatusStrip1.Height + 100
                End If
            End If

            Me.Size = New Size(dWidth, dHeight)


            ' This will be restricted by the Max size specified in the Form Load
            dmaxDispHolderHeight = Me.Height - txtInstructions.Height - lblInstructions.Height - StatusStrip1.Height - 80
            If gpixciCamera.maxHeight > dmaxDispHolderHeight Then
                Me.pnlDispHolder.Size = New Size(dWidth, dmaxDispHolderHeight)
            Else
                Me.pnlDispHolder.Size = New Size(dWidth, gpixciCamera.maxHeight)
            End If

            'ALWAYS Set Picturebox Size = Actual Size, Panel will have Scroll Bars as applicable
            pboxDisplay.Size = New Size(gpixciCamera.maxWidth, gpixciCamera.maxHeight)

            'Position the controls
            cmdClose.Location = New Point(Me.Width - cmdClose.Width - 30, Me.Height - StatusStrip1.Height - cmdClose.Height - 20 - 39) ' 39 = height of Window title bar (31) and bottom border

            If gbInstructions Then
                Me.txtInstructions.Width = cmdClose.Left - 80 '80 = 50 from left of form and 30 for space before cmdClose
                Me.txtInstructions.Location = New Point(50, Me.Height - txtInstructions.Height - StatusStrip1.Height - 20 - 39) ' 39 = height of Window title bar (31) and bottom border
                Me.lblInstructions.Location = New Point(50 + txtInstructions.Width / 2 - lblInstructions.Width / 2,
                                                        txtInstructions.Top - lblInstructions.Height - 20)
            End If
        End If

        gofrmMain.ResumeLayout()

        Exit Sub

ErrorHandler:
        MsgBox("Sub SizeForm() Error Number: " & Err.Number & "   Description: " & Err.Description) 'For Debugging only
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        'Update the pboxDisplay if requested
        If gofrmMain.chkLiveDisplay.Checked Then
            DoVideoCapture()
        End If

    End Sub

End Class
