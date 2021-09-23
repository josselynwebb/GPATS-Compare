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

    Private mbIsInitializing As Boolean = True 'Set to False after Form Loads to prevent Events being called during Initialization

#Region "Events"

    ''' <summary>
    ''' Main Program Entry Point
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadErrorHandler 'Trap Errors

        goFrmMain = Me
        VidDisplay.Main()

        'Make [OK] button visible on About Form (Allows Form closure if launched by Menu)
        goFrmAbout.cmdOK.Visible = True
        goFrmAbout.Hide()

        '** Align Form to top left of screen
        Top = 0
        Left = 0

        'Set the Form Title to reflect the config file, config dimensions and mode
        Text = "Video Capture Display  Resolution: " & gshImageDX & " X " & gshImageDY & "   " & gsMode.ToUpper() & " ACQUISITION"

        'After the display is configured and loaded, write Error Code to the ini file
        SetErrorKey()

        'Size Form and Align Controls
        Call SizeForm()

        mbIsInitializing = False

        If gbGrabActive = False Then ' If in Single Acquisition Mode
            Snapshot_Pixci()
        Else
            Timer1.Enabled = True
            GrabLive_Pixci()
        End If

        Exit Sub

LoadErrorHandler:
        MsgBox("Error Number: " & Err.Number & "   Description: " & Err.Description)     'For Debugging only
        Resume Next
    End Sub

    Private Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Timer1.Enabled = False
        gbPrStop = True 'Stop any processing, if there is
        PXD_PIXCICLOSE()
    End Sub

    Private Sub cmdAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAbout.Click
        goFrmAbout.cmdOK.Visible = True
        goFrmAbout.Show()
    End Sub

    Private Sub cmdAcquire_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAcquire.Click
        Snapshot_Pixci()
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        gbPrStop = True ' Stop any processing, if there is
        Me.Close()
    End Sub

    Private Sub txtInstructions_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInstructions.TextChanged
        If mbIsInitializing Then Exit Sub

        'Prevent User from changing text in text box
        If gbInstructions And cmdClose.Visible = True Then cmdClose.Focus()
    End Sub


    Private Sub txtInstructions_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInstructions.Click

        'Prevent User from changing text in text box
        If gbInstructions And cmdClose.Visible = True Then cmdClose.Focus()

    End Sub

    Private Sub pboxDisplay_Paint(sender As Object, e As PaintEventArgs) Handles pboxDisplay.Paint
        Dim Draw As Graphics = e.Graphics ' Create a local version of the graphics object for the PictureBox.
        Dim hDC As IntPtr = Draw.GetHdc() ' Get a handle to PictureBox1

        ' set resize mode to STRETCH_DELETESCANS
        SetStretchBltMode(hDC, STRETCH_DELETESCANS)

        ' Draw image buffer scaled to the size of PictureBox1
        PXD_RENDERSTRETCHDIBITS(1, 1, 0, 0, -1, -1, 0, hDC, 0, 0, pboxDisplay.Width, pboxDisplay.Height, 0)
        Draw.ReleaseHdc(hDC) ' Release PictureBox1 handle

        Static Dim screenUpdate As Double = 0
        Static Dim frameRate As Double = 0
        Static Dim screenRate As Double = 0
        Static Dim lasttickcount As Long = Now.Ticks
        Static Dim lastcapturedfieldcount As Long = 0
        Dim capturedfieldcount As Long
        Dim tickcount As Long = Now.Ticks

        capturedfieldcount = PXD_CAPTUREDFIELDCOUNT(1)

        If screenUpdate.Equals(50) Then
            frameRate = 1000.0 * 10000 * (capturedfieldcount - lastcapturedfieldcount) / (tickcount - lasttickcount)
            screenRate = 1000.0 * 10000 * (screenUpdate) / (tickcount - lasttickcount)
            screenUpdate = 0
            lasttickcount = tickcount
            lastcapturedfieldcount = capturedfieldcount
        End If

        screenUpdate = screenUpdate + 1
    End Sub

#End Region '"Events"

End Class
