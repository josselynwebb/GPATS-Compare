Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Collections.Generic

''' <summary>
''' User's interface to the Region-of-Interest (ROI) functionality.
''' </summary>
''' <remarks></remarks>
Friend Class frmROI
    Inherits System.Windows.Forms.Form

#Region "Private Members"

    'ROI Box Support
    Private mRect As Rectangle

    Private mshRectKey As Short 'Current Rectangle Key
    Private mshSelRectID As Short 'Selected Rectangle's ID
    Private mshHandle As Short 'Selected sizing handle
    Private miAnchorXPos As Integer 'Current Anchor Horizontal position
    Private miAnchorYPos As Integer 'Current Anchor Vertical position
    Private miOldAnchorXPos As Integer 'Previous Anchor Horizontal position
    Private miOldAnchorYPos As Integer 'Previous Anchor Vertical position
    Private miOldXPos As Integer 'Previous Horizontal position
    Private miOldYPos As Integer 'Previous Vertical position
    Private msRectMode As String 'Current Rectangle modification Mode

    'Local Flags
    Private mbCreateNewRect As Boolean 'Flag: Create New Rectangle
    Private mbUnloadOnly As Boolean 'Flag: Unload form only

    'Light Gray Background Color Constant
    Private mclrLightGray As Color = Color.LightGray

#End Region '"Private Members"

#Region "Private Events"

    Private Sub frmROI_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
        Dim sFile As String

        Me.DoubleBuffered = True

        msRectMode = "" 'Initialize Mode

        gbROIFormOpen = True 'Set Flag to indicate ROI Form is open

        sFile = gsXCAPCaptureDir & "Capture.bmp"

        'Make sure the BMP File was created.
        If Not File.Exists(sFile) Then
            MsgBox(String.Format("ERROR: {0}{1} The Captured image located at '{2} Can not be found.", vbCrLf, vbTab, sFile), MsgBoxStyle.Exclamation, "Unable to Load Captured Image")
            Exit Sub
        End If

        'Move form to the top left of the screen
        SetBounds(0, 0, 0, 0, BoundsSpecified.X Or BoundsSpecified.Y)
        If IsNumeric(gsTargDimX) And IsNumeric(gsTargDimY) Then
            txtTargetWidth.Text = String.Format(gsTargDimX, "0.###")
            txtTargetHeight.Text = String.Format(gsTargDimY, "0.###")
        End If
        Me.Visible = True
    End Sub

    Private Sub frmROI_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        SizeROIForm()    'Size Form and Controls based on Image Size
    End Sub

    Private Sub cmdAccept_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdAccept.Click
        Dim dblWidth As Double
        Dim dblTargetWidth As Double
        Dim dblImageWidth As Double
        Dim dblHeight As Double
        Dim dblTargetHeight As Double
        Dim dblImageHeight As Double

        cmdAccept.Enabled = False

        'First Check for value in the txtTargetWidth and txtTargetHeight Text Boxes
        If txtTargetWidth.Text.Length() < 1 Then
            MsgBox("Please enter a valid Target Width before continuing.", MsgBoxStyle.Information)
            cmdAccept.Enabled = True
            txtTargetWidth.Focus()
            Exit Sub
        End If

        If txtTargetHeight.Text.Length < 1 Then
            MsgBox("Please enter a valid Target Height before continuing.", MsgBoxStyle.Information)
            cmdAccept.Enabled = True
            txtTargetHeight.Focus()
            Exit Sub
        End If

        'Both Target Height and Target Width values are valid; Setup GUI
        gboxTarget.Text = "Field of View"
        lblTargWUom.Text = "mRad"
        lblTargHUom.Text = "mRad"

        'Populate the Field of View dimensions in the Text Boxes
        If IsNumeric(txtWidth.Text.Trim) Then
            dblWidth = CDbl(txtWidth.Text.Trim)
        End If
        If IsNumeric(txtTargetWidth.Text.Trim) Then
            dblTargetWidth = CDbl(txtTargetWidth.Text.Trim)
        End If
        If IsNumeric(txtImageWidth.Text.Trim) Then
            dblImageWidth = CDbl(txtImageWidth.Text.Trim)
        End If
        txtTargetWidth.Text = String.Format(Math.Round(dCalcFieldOfView(dblWidth, dblTargetWidth, dblImageWidth), 3), "0.###")
        If IsNumeric(txtHeight.Text.Trim) Then
            dblHeight = CDbl(txtHeight.Text.Trim)
        End If
        If IsNumeric(txtTargetHeight.Text.Trim) Then
            dblTargetHeight = CDbl(txtTargetHeight.Text.Trim)
        End If
        If IsNumeric(txtImageHeight.Text.Trim) Then
            dblImageHeight = CDbl(txtImageHeight.Text.Trim)
        End If
        txtTargetHeight.Text = String.Format(Math.Round(dCalcFieldOfView(dblHeight, dblTargetHeight, dblImageHeight), 3), "0.###")

        If gbRunFromNAM Then
            lblTargWUom.Text = "Rad"
            lblTargHUom.Text = "Rad"
        End If

        'Prevent the user from entering data after the F-O-V has been calculated
        txtTargetWidth.ReadOnly = True
        txtTargetHeight.ReadOnly = True
        txtTargetWidth.BackColor = mclrLightGray
        txtTargetHeight.BackColor = mclrLightGray
        cmdClear.Enabled = True
    End Sub

    Private Sub cmdBack_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdBack.Click

        mbUnloadOnly = True
        If Not IsNothing(pboxROI.Image) Then
            pboxROI.Image.Dispose()
        End If

        'Me.Close()
        gofrmROI.Close()
        gofrmROI.Dispose()
        gofrmROI = Nothing

        gofrmMain.Visible = True
    End Sub

    Private Sub cmdClear_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdClear.Click
        With Me
            .txtTargetHeight.Clear()
            .txtTargetHeight.Clear()
            .txtTargetWidth.Clear()
            .cmdAccept.Enabled = True
            .txtTargetHeight.ReadOnly = False
            .txtTargetWidth.ReadOnly = False
            .txtTargetWidth.BackColor = Color.White
            .txtTargetHeight.BackColor = Color.White
            .cmdClear.Enabled = False
            .txtTargetWidth.Focus()
        End With
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdClose.Click
        Cursor = Cursors.WaitCursor

        If IsNothing(goTimer) = False Then
            goTimer.Enabled = False
            goTimer.Stop()
            goTimer.Dispose()
        End If

        'Me.Hide()
        'Me.Close()
        If IsNothing(gofrmROI.pboxROI.Image) = False Then
            gofrmROI.pboxROI.Image.Dispose()
        End If

        gofrmROI.Close()

        'Set to True so when gofrmMain.chkLiveDisplay_CheckedChanged fires it knows we are closing the application
        gbMainClosing = True

        gofrmMain.Close()
    End Sub

    Private Sub cmdHelp_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdHelp.Click
        If gbHelpGraphic = True Then
            If gbGraphicOpen = False Then
                cmdHelp.Enabled = False
                frmGraphic.Show()
                frmGraphic.Left = Screen.PrimaryScreen.Bounds.Width - frmGraphic.Width
                frmGraphic.Top = 0
            End If
        Else
            MsgBox("This will launch online Help --> Work in Progress.")
        End If
    End Sub

    Private Sub frmROI_FormClosing(ByVal eventSender As Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim bCancel As Boolean = eventArgs.Cancel    'Should be false but will use this incoming value if we do not change it to true
        Dim sMsgBoxText As String 'Message Box String

        If gbRunFromNAM And Not cmdClose.Enabled Then
            'Don't allow close if ROI has not been defined in NAM Mode
            sMsgBoxText = "This window cannot be closed until the Rectangular Block is defined" & vbCrLf & "in accordance with the ***OPERATOR INSTRUCTIONS***."
            If cmdHelp.Visible Then
                sMsgBoxText = sMsgBoxText & vbCrLf & vbCrLf & "Click the 'HELP' button for more detailed instruction."
            End If
            'Inform user he can't exit yet
            MsgBox(sMsgBoxText, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Video Display Non-ATLAS Module")
            bCancel = True
        End If

        eventArgs.Cancel = bCancel
    End Sub

    Private Sub frmROI_FormClosed(ByVal eventSender As Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If mbUnloadOnly = False Then
            If gbRunFromNAM = True Then
                UpdateData()
            Else
                PowerOffAndShutDown() 'Put EO Instrument in Default Position
            End If
            gofrmMain.Close()
        End If

        gbROIFormOpen = False 'Reset Form Open Flag
    End Sub

    ' ''' <summary>
    ' ''' LARRYP: Added this Form KeyDown Event Handler to replace PictureBox (pboxROI) KeyDown which can be created but will never get called as it can never get focus
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks>
    ' '''     This will test to see if Cursor is Over PictureBox AND KeyDown occurred
    ' ''' </remarks>
    'Private Sub frmROI_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown

    '    If Not Me.pboxROI.Bounds.Contains(Me.PointToClient(Cursor.Position)) Then
    '        'The Cursor is NOT over the PictureBox.
    '        Exit Sub
    '    End If

    '    Select Case e.KeyCode
    '        Case Keys.Up    'Up Arrow
    '            mshHandle = NORTH
    '        Case Keys.Down
    '            mshHandle = SOUTH
    '        Case Keys.Left
    '            mshHandle = EAST
    '        Case Keys.Right
    '            mshHandle = WEST
    '        Case Else
    '            Exit Sub
    '    End Select

    '    If e.Shift Or e.Control Then 'Resize
    '        msRectMode = "Resize"
    '        pboxROI.Image = Nothing
    '        'LARRYP: Not sure if we need the 2 lines below
    '        pboxROI.BackColor = Color.Empty
    '        pboxROI.Invalidate()
    '        'mlistRectangles(mshSelRectID).DeleteRect()
    '        'mlistRectangles(mshSelRectID).ResizeRect(mshHandle, 0, 0, True, e.Control)
    '        'mdictRectangles(mshSelRectID).DeleteRect()
    '        'mdictRectangles(mshSelRectID).ResizeRect(mshHandle, 0, 0, True, e.Control)
    '    Else
    '        msRectMode = "Drag"
    '        pboxROI.Image = Nothing
    '        'LARRYP: Not sure if we need the 2 lines below
    '        pboxROI.BackColor = Color.Empty
    '        pboxROI.Invalidate()
    '        MsgBox("Check the Types and Values going to DeleteRect and ResizeRect")
    '        'mdictRectangles(mshSelRectID).DeleteRect()
    '        'mdictRectangles(mshSelRectID).ResizeRect(CShort(miAnchorXPos), 0, 0, True, mshSelRectID)
    '    End If

    'End Sub

    Private Sub picROI_MouseDown(ByVal eventSender As Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles pboxROI.MouseDown
        Dim iXCoord As Integer = eventArgs.X
        Dim iYCoord As Single = eventArgs.Y

        'Initialize Coordinates
        miAnchorXPos = iXCoord
        miAnchorYPos = iYCoord
        miOldXPos = iXCoord
        miOldYPos = iYCoord
        miOldAnchorXPos = iXCoord
        miOldAnchorYPos = iYCoord
        msRectMode = ""

        'If Right Mouse Button Pressed
        If eventArgs.Button = MouseButtons.Right Then
            MyBase.OnMouseDown(eventArgs)
            Exit Sub
        End If 'Button=2 (Right Mouse Button)

        'Shouldn't need a New Rectangle here as long as it is initially instantiated
        If IsNothing(mRect) Then
            mRect = New Rectangle(eventArgs.X, eventArgs.Y, 0, 0)
        Else
            mRect.X = eventArgs.X
            mRect.Y = eventArgs.Y
            mRect.Width = 0
            mRect.Height = 0
        End If
        txtWidth.Text = mRect.Width.ToString()
        txtHeight.Text = mRect.Height.ToString()

        pboxROI.Invalidate()
    End Sub

    Private Sub picROI_MouseUp(ByVal eventSender As Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles pboxROI.MouseUp

        'If any button other than Left was clicked then call base event and exit sub
        If eventArgs.Button <> Windows.Forms.MouseButtons.Left Then
            MyBase.OnMouseUp(eventArgs)
            Exit Sub
        End If

        If CInt(txtHeight.Text) > 0 And CInt(txtWidth.Text) > 0 Then
            'Enable the Accept Button if all parameters are valid for F-O-V calculation
            If gbRunFromNAM Then
                cmdClose.Enabled = True
                txtTargetWidth.Text = gsTargDimX
                txtTargetHeight.Text = gsTargDimY
                cmdAccept.Enabled = True
            Else
                cmdAccept.Enabled = True
            End If
        Else
            'Allow User another attempt to Draw ROI Box
            pboxROI.Cursor = Cursors.Cross
            cmdAccept.Enabled = False
        End If
    End Sub

    Private Sub picROI_MouseMove(ByVal eventSender As Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles pboxROI.MouseMove
        Dim iXCoord As Integer = eventArgs.X
        Dim iYCoord As Integer = eventArgs.Y

        'Update Current Mouse Pointer Coordinate for the User
        tsStatusLabel.Text = "Cur X: " & iXCoord & "     Cur Y: " & iYCoord

        If eventArgs.Button <> MouseButtons.Left Then
            MyBase.OnMouseMove(eventArgs)
            Exit Sub
        End If

        'mRect = New Rectangle(mRect.Left, mRect.Top, iXCoord - mRect.Left, iYCoord - mRect.Top)
        With mRect
            'mRect = New Rectangle(x, y, Width, Height)
            .Width = iXCoord - .Left
            .Height = iYCoord - .Top
            txtX.Text = .Left.ToString()
            txtY.Text = .Top
            txtEndX.Text = .Right
            txtEndY.Text = .Bottom
            txtWidth.Text = .Width
            txtHeight.Text = .Height
            gsStartX = .Left
            gsStartY = .Top
            gsEndX = .Right
            gsEndY = .Bottom
        End With

        pboxROI.Invalidate()
    End Sub

    ''' <summary>
    ''' Uses EPIX Frame Buffer to Paint pboxROI (if image available)
    ''' Draw Crosshairs if Overlay is True
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    Private Sub pboxROI_Paint(sender As Object, eventArgs As PaintEventArgs) Handles pboxROI.Paint
        Dim Draw As Graphics = eventArgs.Graphics ' Create a local version of the graphics object for the PictureBox.
        Dim hDC As IntPtr = Draw.GetHdc() ' Get a handle to pboxROI

        'Set resize mode to STRETCH_DELETESCANS
        SetStretchBltMode(hDC, STRETCH_DELETESCANS)

        'Draw image buffer scaled to the size of PictureBox
        PXD_RENDERSTRETCHDIBITS(1, 1, 0, 0, -1, -1, 0, hDC, 0, 0, pboxROI.Width, pboxROI.Height, 0)

        Draw.ReleaseHdc(hDC) ' Release pboxROI handle

        'Draw Previously Saved mRect Rectangle
        Using redPen As New Pen(Color.Firebrick, 3)
            eventArgs.Graphics.DrawRectangle(redPen, mRect)
        End Using
    End Sub

    Private Sub txtTargetHeight_KeyPress(ByVal eventSender As Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTargetHeight.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        'Trap Enter Key
        If KeyAscii = System.Windows.Forms.Keys.Return Then
            If cmdAccept.Enabled = True Then
                cmdAccept_Click(cmdAccept, New System.EventArgs())
            End If
            If txtTargetWidth.Text = "" Then
                txtTargetWidth.Focus()
            End If
        Else

            'Only allow the user to enter positive numbers, BackSpace, Delete and Period
            If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 127 And KeyAscii <> 8 And KeyAscii <> 46 Then
                KeyAscii = 1
                MsgBox("Please enter a valid Target Height", MsgBoxStyle.Question, "Invalid Entry")
            End If
        End If

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTargetWidth_KeyPress(ByVal eventSender As Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTargetWidth.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        'Trap Enter Key
        If KeyAscii = System.Windows.Forms.Keys.Return Then
            If cmdAccept.Enabled = True Then
                cmdAccept_Click(cmdAccept, New System.EventArgs())
            End If
            If txtTargetHeight.Text = "" Then
                txtTargetHeight.Focus()
            End If
        Else

            'Only allow the user to enter positive numbers, BackSpace, Delete and Period
            If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 127 And KeyAscii <> 8 And KeyAscii <> 46 Then
                KeyAscii = 1
                MsgBox("Please enter a valid Target Width", MsgBoxStyle.Question, "Invalid Entry")
            End If
        End If

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

#End Region '"Private Events"

#Region "Public Methods"

    ''' <summary>
    ''' Calls LoadIt() and allows User to Draw
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadImage()
        'Load the Captured BitMap Image
        LoadIt()

        'Allow User to Draw ROI Box
        mbCreateNewRect = True
        mshRectKey = mshRectKey + 1
        pboxROI.Cursor = System.Windows.Forms.Cursors.Cross
        mshSelRectID = 0
    End Sub

    ''' <summary>
    ''' Adjust Form and Controls to the current Image Size.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SizeROIForm()
        Dim bCamTallerThanGBoxes As Boolean = False
        Dim bOverSizeWidth As Boolean = False
        Dim bOverSizeHeight As Boolean = False
        Dim dWidth As Double = 0.0
        Dim dHeight As Double = 0.0

        If Me.Visible = False Then Exit Sub

        '***** RUN FROM NAM  *****
        If gbRunFromNAM = True Then
            gofrmMain.Visible = False
            Me.cmdBack.Visible = False

            'CODE Does Nothing unless FOV or TGTCOORD!!
            Select Case gshMode
                Case FOV, TGTCOORD
                    cmdClose.Visible = True
                    'Disable Close until Rectangular Block is drawn
                    cmdClose.Enabled = False
                    If gbHelpGraphic = True Then
                        cmdHelp.Visible = True
                    Else
                        cmdHelp.Visible = False
                    End If
                    If gbInstructions = True Then
                        'Resize form to accomodate Instruction Text and Banner
                        rtbFoVText.Visible = True
                        lblOpInstr.Visible = True
                        rtbFoVText.Text = gsInstructions
                    End If
            End Select
        Else
            '***** SAIS Mode *****
            Me.cmdBack.Visible = True
        End If

        '-------------------------------------
        '        DETERMINE SIZING (SAIS Or NAM)
        '-------------------------------------

        ' Adjust the window size according to the image but limited by the actual available screen area
        If (gpixciCamera.maxWidth + gboxImage.Width + 47) > Screen.PrimaryScreen.WorkingArea.Width Then
            dWidth = Screen.PrimaryScreen.WorkingArea.Width
        Else
            dWidth = gpixciCamera.maxWidth + gboxImage.Width + 47
        End If

        If gbInstructions Then
            If (gpixciCamera.maxHeight + rtbFoVText.Height + lblOpInstr.Height + StatusStripUserInfo.Height + 70) > Screen.PrimaryScreen.WorkingArea.Height Then
                dHeight = Screen.PrimaryScreen.WorkingArea.Height
            Else
                dHeight = gpixciCamera.maxHeight + rtbFoVText.Height + lblOpInstr.Height + StatusStripUserInfo.Height + 70
            End If
        Else
            If (gpixciCamera.maxHeight + StatusStripUserInfo.Height + 40) > Screen.PrimaryScreen.WorkingArea.Height Then
                dHeight = Screen.PrimaryScreen.WorkingArea.Height
            Else
                dHeight = gpixciCamera.maxHeight + StatusStripUserInfo.Height + 40
            End If
        End If

        Me.Size = New Size(dWidth, dHeight)

        'Set the size of the pnlScrollBox based on the window size and the visible controls
        dWidth = Me.Width - gboxImage.Width - 50

        If gbInstructions Then
            dHeight = Me.Height - lblOpInstr.Height - rtbFoVText.Height - StatusStripUserInfo.Height - 30 - 39  ' 39 = height of Window title bar (31) and bottom border
        Else
            dHeight = Me.Height - StatusStripUserInfo.Height - 15 - 39  ' 39 = height of Window title bar (31) and bottom border
        End If
        pnlScrollBox.Location = New Point(0, 0)
        pnlScrollBox.Size = New Size(dWidth, dHeight)

        'ALWAYS Set Picturebox Size = Actual Size, Panel will have Scroll Bars as applicable
        pboxROI.Size = New Size(gpixciCamera.maxWidth, gpixciCamera.maxHeight)

        If gbInstructions Then
            rtbFoVText.Top = StatusStripUserInfo.Top - rtbFoVText.Height - 18
            rtbFoVText.Width = pnlScrollBox.Width - 20
            rtbFoVText.Left = pnlScrollBox.Left + 10
            lblOpInstr.Top = rtbFoVText.Top - 20
            lblOpInstr.Left = rtbFoVText.Left + rtbFoVText.Width / 2 - lblOpInstr.Width / 2
            cmdClose.Top = lblOpInstr.Top + 10
        Else
            If cmdHelp.Visible Then
                cmdClose.Top = StatusStripUserInfo.Top - cmdHelp.Height - cmdClose.Height - 17
            Else
                cmdClose.Top = StatusStripUserInfo.Top - cmdClose.Height - 12
            End If
        End If

        cmdClose.Left = Me.Width - (cmdClose.Width + 20) - 16

        'May Not Be Visible
        cmdBack.Top = cmdClose.Top
        cmdBack.Left = cmdClose.Left - (cmdBack.Width + 20)

        cmdHelp.Top = cmdClose.Bottom + 5
        cmdHelp.Left = cmdClose.Left

        'Group Boxes Top and Width does not change
        'Need Group Boxes X (Left) Positions based on screen width
        gboxImage.Left = Me.Width - gboxImage.Width - 28
        gboxTarget.Left = gboxImage.Left
        gboxROI.Left = gboxTarget.Left
    End Sub
#End Region '"Public Methods"
End Class