Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6

Public Class frmMain
    Inherits System.Windows.Forms.Form

    Private Sub AbortButton_Click(sender As Object, e As EventArgs) Handles AbortButton.Click
        AbortButton_Click()
    End Sub

    Public Sub AbortButton_Click()
        'Prevent user from selecting Abort Button more than once
        TimerStatusByteRec.Enabled = False
        Me.cmdFHDB.Enabled = False
        Me.cmdFHDB.Visible = False
        AbortButton.Enabled = False
        If ProgressBar.Visible Then
            ProgressBar.Visible = False
            'DelayRemainingLabel.Visible = False
        End If
        Me.lblStatus.Text = "TESTING ABORTED!"
        Me.lblStatus.ForeColor = System.Drawing.Color.Red
        Echo("")
        Echo("")
        Echo("*******************************************************************************")
        Echo("USER ABORTED TESTING - DATE/TIME: " & DateTime.Now.ToString("dddd, dd-MMM-yyyy HH:mm"))
        Echo("*******************************************************************************")

        UserEvent = ABORT_BUTTON
        TETS.ResetSystem()
    End Sub

    Private Sub cmdDiagnostics_Click(sender As Object, e As EventArgs) Handles cmdDiagnostics.Click
        Me.cmdDiagnostics.Visible = False
        UserEvent = DIAGNOSTIC_BUTTON
    End Sub

    Private Sub cmdEndToEnd_Click(sender As Object, e As EventArgs) Handles cmdEndToEnd.Click
        Me.ModuleMenu.Visible = False
        Me.SeqTextWindow.Visible = True
        bEndToEnd = True
        bFirstRun = False
        Me.NewUUT.Visible = False
        TimerStatusByteRec.Enabled = True
        UserEvent = END_TO_END
    End Sub

    Private Sub cmdFHDB_Click(sender As Object, e As EventArgs) Handles cmdFHDB.Click
        Dim i As Short
        Me.cmdFHDB.Enabled = False
        Me.cmdFHDB.Visible = False
        CenterForm(gFrmFHDBComment)
        gFrmFHDBComment.ShowDialog()

        With TestData
            i = FHDB.SaveData(.sStart, .SStop, .EROs, .sTPCCN, .sUUTSerial, .sUUTRev, .sIDSerial, .nStatus, .sFailStep, .sCallout, .dMeasurement, .sUOM, .duLimit, .dlLimit, .sComment)
        End With
    End Sub

    Private Sub cmdModuleArray_Click(sender As Object, e As EventArgs) Handles cmdModule_1.Click
        Dim index As Integer
        Dim buttonName As String = sender.Name

        index = Convert.ToInt32(buttonName.Substring(buttonName.Length - 1, 1))
        cmdModule_Click(index)
    End Sub

    Private Sub cmdModule_Click(ByRef index As Short)
        Me.ModuleMenu.Visible = False
        Me.SeqTextWindow.Visible = True
        bEndToEnd = False
        bFirstRun = False
        Me.NewUUT.Visible = False
        TimerStatusByteRec.Enabled = True
        UserEvent = index
    End Sub

    Private Sub cmdPwrOnModule_Click(sender As Object, e As EventArgs) Handles cmdPwrOnModule.Click
        Me.ModuleMenu.Visible = False
        Me.SeqTextWindow.Visible = True
        bEndToEnd = False
        bFirstRun = False
        Me.NewUUT.Visible = False
        TimerStatusByteRec.Enabled = True
        UserEvent = PWR_ON
    End Sub

    Private Sub cmdSTTOModule_Click(sender As Object, e As EventArgs) Handles cmdSTTOModule.Click
        Me.ModuleMenu.Visible = False
        Me.SeqTextWindow.Visible = True
        bEndToEnd = False
        bFirstRun = False
        Me.NewUUT.Visible = False
        TimerStatusByteRec.Enabled = True
        UserEvent = STTO
    End Sub

    Private Sub cmdContinue_Click(sender As Object, e As EventArgs) Handles cmdContinue.Click
        Continue_Click()
    End Sub

    Public Sub Continue_Click()
        Me.frMeasContinuous.Visible = False
        Me.fraInstructions.Visible = False
        Me.cmdFHDB.Enabled = False
        Me.cmdFHDB.Visible = False
        UserEvent = CONTINUE_BUTTON
    End Sub

    Private Sub frmMain_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        Select Case KeyCode
            Case System.Windows.Forms.Keys.Escape
                AbortButton_Click()
            Case System.Windows.Forms.Keys.A
                If Shift = 2 Then AbortButton_Click()
            Case System.Windows.Forms.Keys.R
                If Shift = 2 And Me.MainMenu.Visible = True Then MenuOption_Click((1))
            Case System.Windows.Forms.Keys.S
                If Shift = 2 And Me.MainMenu.Visible = True Then MenuOption_Click((2))
            Case System.Windows.Forms.Keys.D
                If Shift = 2 And Me.MainMenu.Visible = True Then MenuOption_Click((3))
            Case System.Windows.Forms.Keys.L
                If Shift = 2 And Me.MainMenu.Visible = True Then MenuOption_Click((4))
            Case System.Windows.Forms.Keys.I
                If Shift = 2 And Me.MainMenu.Visible = True Then MenuOption_Click((5))
            Case System.Windows.Forms.Keys.T
                If Shift = 2 And Me.MainMenu.Visible = True Then MenuOption_Click((6))
            Case System.Windows.Forms.Keys.M
                If Shift = 2 Then MainMenuButton_Click()
            Case System.Windows.Forms.Keys.Q
                If Shift = 2 Then Quit_Click()
            Case System.Windows.Forms.Keys.P
                If Shift = 2 Then PrintButton_Click()
            Case System.Windows.Forms.Keys.C
                If Shift = 2 Then Continue_Click()
            Case System.Windows.Forms.Keys.E
                If Shift = 2 Then RerunButton_Click()
            Case System.Windows.Forms.Keys.F12
                If SOF = True Then 'bbbb
                    SOF = False
                    Echo("Enable COF mode")
                Else
                    SOF = True
                    Echo("Enable SOF mode")
                End If
        End Select
        cmdContinue.TabIndex = 5
    End Sub

    Private Sub frmMain_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return And cmdContinue.Enabled = True Then Continue_Click()
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        FormMainLoaded = True
        Me.SSPanel1.Visible = False
        Me.SSPanel2.Visible = False

        MainPanel.Left = 8
        MainPanel.Top = 8
        MainPanel.Width = 842
        MainPanel.Height = 593

        MainMenu.Left = 8
        MainMenu.Top = 8
        MainMenu.Width = Me.Width - 8
        MainMenu.Height = 571
        picTestDocumentation.Left = 0
        picTestDocumentation.Top = 0
        picTestDocumentation.Width = Me.MainMenu.Width
        picTestDocumentation.Height = 571
        picTestDocumentation.Visible = False

        lblModuleStatusTitle(2).Width = 224

        ModuleMenu.Left = 8
        ModuleMenu.Top = 8
        ModuleMenu.Width = fraTestInformation.Left - 20
        ModuleMenu.Height = 571
        ModuleMenu.Visible = False
        lblModuleStatusTitle_3.Width = ModuleMenu.Width - lblModuleStatusTitle_2.Left - lblModuleStatusTitle_2.Width
        ModuleInner.Left = 8
        ModuleInner.Top = 25
        ModuleInner.Width = ModuleMenu.Width - 10
        ModuleInner.Height = ModuleMenu.Height - lblStatusTitle.Height - 4

        PictureWindow.Top = 8
        PictureWindow.Left = 8
        PictureWindow.Height = 571
        PictureWindow.Width = 589
        PictureWindow.Visible = False

        pinp.Top = 8
        pinp.Left = 8
        pinp.Height = 571
        pinp.Width = 589
        pinp.Visible = False

        fraInstructions.Visible = False
        lblPowerApplied.Visible = False
        picDanger.Visible = False

        SeqTextWindow.Top = 8
        SeqTextWindow.Left = 8
        SeqTextWindow.Height = 571
        SeqTextWindow.Width = fraTestInformation.Left - SeqTextWindow.Left - 8
        SeqTextWindowLabel.Top = 30
        SeqTextWindowLabel.Left = 0
        SeqTextWindowLabel.Height = 541
        SeqTextWindowLabel.Width = 635

        lblTestResults.Top = 0
        lblTestResults.Left = 0
        lblTestResults.Width = 635
        lblTestResults.Height = 33

        TextWindow.Top = 8
        TextWindow.Left = 8
        TextWindow.Height = 571
        TextWindow.Width = 780
        TextWindow.Text = ""

        MAINMod.Main()
        CenterForm(Me)
    End Sub

    Public Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        UserEvent = QUIT_BUTTON
        Me.lblStatus.Text = "Resetting System ..."

        Delay(7)

        gFrmExit.Close()
        gFrmDisplayLED.Close()
        gFrmFHDBComment.Close()
        gFrmHV.Close()
        gFrmOperatorComment.Close()
        gFrmOperatorMsg.Close()
        gFrmImage.Close()
        gFrmSplash.Close()
        FormMainLoaded = False
    End Sub

    Private Sub fraMeasurement_Enter(sender As Object, e As EventArgs) Handles fraMeasurement.Enter
        'bbbb added to open a frame that I could see
        If frMeasContinuous.Visible = False Then
            frMeasContinuous.Left = ((Me.Width - frMeasContinuous.Width) / 2) - 67
            frMeasContinuous.Top = ((Me.Height - frMeasContinuous.Height) / 2) + 100
            If Me.ModuleMenu.Visible = False Then
                frMeasContinuous.Visible = True
            Else
                frMeasContinuous.Visible = False
            End If
        Else
            frMeasContinuous.Visible = False
        End If
    End Sub

    Private Sub frMeasContinuous_Enter(sender As Object, e As EventArgs) Handles frMeasContinuous.Enter
        frMeasContinuous.Visible = False
    End Sub

    Private Sub lblAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblAbout.Click
        MAINMod.ShowSplashScreen(True)
    End Sub

    Private Sub lblStatus_TextChanged(sender As Object, e As EventArgs) Handles lblStatus.TextChanged
        If lblStatus.Text.Substring(0, 4).ToUpper() = "WAIT" Then
            Me.ProgressBar.Visible = False
        End If
    End Sub

    Private Sub MainMenuButton_Click(sender As Object, e As EventArgs) Handles MainMenuButton.Click
        MainMenuButton_Click()
    End Sub

    Private Sub MainMenuButton_Click()
        Me.cmdFHDB.Enabled = False
        Me.cmdFHDB.Visible = False
        Me.NewUUT.Visible = False
        Me.cmdDiagnostics.Visible = False
        UserEvent = MAINMENU_BUTTON
        Me.MainMenuButton.Enabled = False
        SchemPageNum = 1
        AssyPageNum = 1
        PartListPageNum = 1
        UserEvent = MAINMENU_BUTTON
        ShowMainMenu()
    End Sub

    Private Sub MainMenuButton_KeyDown(ByRef KeyCode As Short, ByRef Shift As Short)
        Select Case KeyCode
            Case System.Windows.Forms.Keys.R
                If Shift = 2 Then MenuOption_Click((1))
            Case System.Windows.Forms.Keys.S
                If Shift = 2 Then MenuOption_Click((2))
            Case System.Windows.Forms.Keys.A
                If Shift = 2 Then MenuOption_Click((3))
            Case System.Windows.Forms.Keys.P
                If Shift = 2 Then MenuOption_Click((4))
        End Select
    End Sub

    Private Sub MenuOption_Click(sender As Object, e As EventArgs) Handles MenuOption_1.Click, MenuOption_2.Click, MenuOption_3.Click,
                                                                           MenuOption_4.Click, MenuOption_5.Click, MenuOption_6.Click,
                                                                           MenuOption_7.Click, MenuOption_8.Click, MenuOption_9.Click,
                                                                           MenuOption_10.Click, MenuOption_11.Click, MenuOption_12.Click,
                                                                           MenuOption_13.Click, MenuOption_14.Click
        Dim index As Integer
        Dim btnName As String = sender.Name

        index = Convert.ToInt32(btnName.Substring(btnName.IndexOf("_") + 1, btnName.Length - btnName.IndexOf("_") - 1))
        MenuOption_Click(index)
    End Sub

    Private Sub MenuOption_Click(ByRef index As Short)
        Dim flash As Short ' bbbb
        index = -index

        'Change "Test Status" frame to "Image Options" if menu choice is to view drawings
        Select Case index
            Case VIEW_SCHEMATIC, VIEW_ASSEMBLY, VIEW_ID_SCHEMATIC, VIEW_ID_ASSEMBLY
                EnableImageControl((index))

            Case VIEW_PARTSLIST
                If LCase(VB.Right(sPartsListFileNames(PartListPageNum), 4)) <> ".txt" Then EnableImageControl((index))
            Case VIEW_ID_PARTSLIST
                If LCase(VB.Right(sIDPartsListFileNames(PartListPageNum), 4)) <> ".txt" Then EnableImageControl((index))
        End Select


        For flash = 1 To 3
            MenuOptionText(index * -1).Visible = False
            System.Windows.Forms.Application.DoEvents()
            Delay((0.1))
            MenuOptionText(index * -1).Visible = True
            System.Windows.Forms.Application.DoEvents()
            Delay((0.1))
        Next flash

        'gFrmMain.MainMenuButton.Enabled = True
        DoMenuChoice(index)
    End Sub

    Private Sub NewUUT_Click(sender As Object, e As EventArgs) Handles NewUUT.Click
        bFirstRun = True
        TestData.sUUTSerial = ""
        Me.NewUUT.Visible = False
        ShowModuleMenu()
    End Sub

    Private Sub NoButton_Click(sender As Object, e As EventArgs) Handles NoButton.Click
        Me.fraInstructions.Visible = False
        UserEvent = NO_BUTTON
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        PrintButton_Click()
    End Sub

    Private Sub PrintButton_Click()
        Dim Printer As New Printer
        On Error GoTo Errorhandler

        If SeqTextWindow.Visible = True Then
            'Print Echo'ed test results of the current test run
            Printer.FontName = "Courier New"
            Printer.FontSize = 10
            Printer.FontBold = False
            Printer.Print(TextWindow.Text)
            Printer.EndDoc()
        End If
        Exit Sub
Errorhandler:
        MsgBox("Error No.: " & Err.Number & vbCrLf & "Description: " & Err.Description, 16, "Shell Error")
        Err.Clear()
        Resume Next
    End Sub

    Private Sub Quit_Click(sender As Object, e As EventArgs) Handles Quit.Click
        Quit_Click()
    End Sub

    Public Sub Quit_Click()
        '  bQuitExitForm = False' bbbb not defined anywhere
        gFrmExit.ShowDialog()
    End Sub

    Private Sub RerunButton_Click(sender As Object, e As EventArgs) Handles RerunButton.Click
        RerunButton_Click()
    End Sub

    Private Sub RerunButton_Click()
        'Prevent user from selecting RERUN Button more than once
        Me.cmdFHDB.Enabled = False
        Me.cmdFHDB.Visible = False
        Me.cmdDiagnostics.Visible = False
        EnableAbort()
        'Reset TextWindow to contain echoed test results of new run
        TextWindow.Text = "" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
        UserEvent = RERUN_BUTTON
    End Sub

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
        Dim iCount As Short

        Me.Timer2.Enabled = False
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        For iCount = 0 To 4
            Me.picStatus(iCount).Visible = True
            Me.picStatus(iCount).Image = Me.ImageList2.Images(1)
        Next iCount
        Me.Timer1b.Interval = 450
        Me.Timer1b.Enabled = True
        '    Delay 0.5
        '    For iCount = 0 To 4
        '        gFrmMain.picStatus(iCount).Picture = gFrmMain.ImageList2.ListImages(0).Picture
        '    Next iCount
    End Sub

    Private Sub Timer1b_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1b.Tick
        For iCount = 0 To 4
            Me.picStatus(iCount).Image = Me.ImageList2.Images(0)
        Next iCount
        Me.Timer1b.Enabled = False 'Oneshot Timer
    End Sub

    Private Sub Timer2_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer2.Tick
        Dim Interval As Short ' bbbb

        Interval = 900
        Me.Timer1.Enabled = False

        Me.lblStatus.ForeColor = System.Drawing.Color.Red
        Me.Timer2b.Interval = 450
        Me.Timer2b.Enabled = True
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub Timer2b_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer2b.Tick
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.Timer2b.Enabled = False 'Oneshot Timer
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub Timer3_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer3.Tick
        If ProgressBar.Value < ProgressBar.Maximum Then
            Me.ProgressBar.Value = Me.ProgressBar.Value + (100 / Me.ProgressBar.Maximum)
            System.Windows.Forms.Application.DoEvents()
        End If
    End Sub

    Private Sub TimerProbe_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TimerProbe.Tick
        'bbbb changed to ViperT command
        'Dim bytStatus As Byte
        Dim status As Short

        If Not bSimulation Then ' get the PS actionbyte
            nSystErr = MAINMod.viReadSTB(nSupplyHandle(1), status)
            System.Windows.Forms.Application.DoEvents()
            If ((status And &HF0) = &HB0) Then
                status = Val(Str(status))
                bProbeClosed = Not ((status And &H2) = &H2)
                'if bProbeClosed = True then Probe button is Pressed
            End If
        End If

        ''bbbb
        ''    Dim nCurrWnd As Long 'Current Window Handle
        ''    Dim sCaption As String 'Caption of window found
        ''    Dim lpString As String * 255 'Buffer for Window text in API call
        ''    Dim nRet As Long 'Window Text Error Code
        ''    Static bProbeStatus As Boolean
        ''    'Get First Window
        ''    nCurrWnd = GetWindow(Me.hwnd, GW_HWNDFIRST)
        ''
        ''    'Get Window Information for all Windows on the desktop
        ''    Do While nCurrWnd <> 0
        ''
        ''        'Get Caption of Window
        ''        lpString = ""
        ''        nRet = GetWindowText(ByVal nCurrWnd, ByVal lpString, ByVal 255)
        ''        sCaption = Trim(lpString)
        ''
        ''        'Check Window Caption
        ''        If InStr(sCaption, "TETS SYSTEM MONITOR STATUS BYTE DWH:") Then
        ''            bytStatus = CByte(Mid(sCaption, 38))
        ''            If (bytStatus And &HF0) = &HB0 Then 'Qualify the status byte
        ''                bProbeClosed = Not ((bytStatus And &H2) = &H2)
        ''            End If
        ''            Exit Do
        ''        End If
        ''
        ''        'Acquire the next Window on the Desktop
        ''        nCurrWnd = GetWindow(nCurrWnd, GW_HWNDNEXT)
        ''    Loop
        ''    End If
    End Sub

    Private Sub TimerStatusByteRec_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TimerStatusByteRec.Tick
        'bbbb added for ViperT
        Dim status As Short

        'bbbb
        If Not bSimulation Then
            nSystErr = MAINMod.viReadSTB(nSupplyHandle(1), status)
            System.Windows.Forms.Application.DoEvents()
            If ((status And &HF0) = &HB0) Then
                status = Val(Str(status))
                bReceiverClosed = Not ((status And &H1) = &H1)
                If Not bReceiverClosed Then
                    AbortButton_Click()
                    Me.TimerStatusByteRec.Enabled = False
                    Exit Sub
                End If
                bProbeClosed = Not ((status And &H2) = &H2)
            End If
        End If

        ''bbbb
        ''    Dim bytStatus As Byte
        ''    Dim nCurrWnd As Long 'Current Window Handle
        ''    Dim sCaption As String 'Caption of window found
        ''    Dim lpString As String * 255 'Buffer for Window text in API call
        ''    Dim nRet As Long 'Window Text Error Code
        ''    Static bProbeStatus As Boolean
        ''    If Not bSimulation Then
        ''    'Get First Window
        ''    nCurrWnd = GetWindow(Me.hwnd, GW_HWNDFIRST)
        ''
        ''    'Get Window Information for all Windows on the desktop
        ''    Do While nCurrWnd <> 0
        ''
        ''        'Get Caption of Window
        ''        lpString = ""
        ''        nRet = GetWindowText(ByVal nCurrWnd, ByVal lpString, ByVal 255)
        ''        sCaption = Trim(lpString)
        ''
        ''        'Check Window Caption
        ''        If InStr(sCaption, "TETS SYSTEM MONITOR STATUS BYTE DWH:") Then
        ''            bytStatus = CByte(Mid(sCaption, 38))
        ''            If (bytStatus And &HF0) = &HB0 Then 'Qualify the status byte
        ''                bReceiverClosed = Not ((bytStatus And &H1) = &H1)
        ''                If Not bReceiverClosed Then
        ''                    AbortButton_Click
        ''                    gFrmMain.TimerStatusByteRec.Enabled = False
        ''                    Exit Sub
        ''                End If
        ''                bProbeClosed = Not ((bytStatus And &H2) = &H2)
        ''            End If
        ''            Exit Do
        ''        End If
        ''
        ''        'Acquire the next Window on the Desktop
        ''        nCurrWnd = GetWindow(nCurrWnd, GW_HWNDNEXT)
        ''    Loop
        ''    End If
    End Sub

    Private Sub txtLowerLimit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLowerLimit.TextChanged
        If Val(txtLowerLimit.Text) = -1.0E-99 Then
            txtLowerLimit.Text = "N/A"
            lblLow.Text = "N/A"
        Else
            lblLow.Text = txtLowerLimit.Text
        End If
    End Sub

    Private Sub txtMeasured_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMeasured.TextChanged
        Me.txtMeasuredBig.Text = Me.txtMeasured.Text
        System.Windows.Forms.Application.DoEvents()
        If Me.txtMeasured.Text = "" Then
            Me.txtMeasured.BackColor = System.Drawing.Color.White
            Me.txtMeasuredBig.BackColor = System.Drawing.Color.White
            Exit Sub
        End If

        If UCase(Me.txtUnit.Text) = "DTB" Then
            If UCase(Me.txtMeasured.Text) = "PASSED" Then
                Me.txtMeasured.BackColor = System.Drawing.Color.Lime
                Me.txtMeasuredBig.BackColor = System.Drawing.Color.Lime
                Pass = True
                Failed = False
            Else
                Me.txtMeasured.BackColor = System.Drawing.Color.Red
                Me.txtMeasuredBig.BackColor = System.Drawing.Color.Red
                Pass = False
                Failed = True
            End If
            Exit Sub
        End If

        If UCase(Me.txtUnit.Text) = "QUESTION" Or UCase(Me.txtUnit.Text) = "Y/N" Then
            If UCase(Me.txtMeasured.Text) = UCase(Me.txtUpperLimit.Text) Then
                Me.txtMeasured.BackColor = System.Drawing.Color.Lime
                Me.txtMeasuredBig.BackColor = System.Drawing.Color.Lime
                Pass = True
                Failed = False
            Else
                Me.txtMeasured.BackColor = System.Drawing.Color.Red
                Me.txtMeasuredBig.BackColor = System.Drawing.Color.Red
                Pass = False
                Failed = True
            End If
            Exit Sub
        End If

        If Me.txtUpperLimit.Text = "N/A" And Me.txtLowerLimit.Text = "N/A" Then
            Me.txtMeasured.BackColor = System.Drawing.Color.White
            Me.txtMeasuredBig.BackColor = System.Drawing.Color.White
            Exit Sub
        End If

        If IsNumeric(txtMeasured.Text) Then
            Select Case Me.txtCommand.Text
                Case "dMeasImp"
                Case Else
                    If Val(Me.txtMeasured.Text) = 9.9E+37 Then
                        Me.txtMeasured.BackColor = System.Drawing.Color.Red
                        Me.txtMeasured.Text = "No Meas"
                        Me.txtMeasuredBig.BackColor = System.Drawing.Color.Red
                        Me.txtMeasuredBig.Text = "No Meas"
                        Pass = False
                        Failed = True
                        OutHigh = True
                        OutLow = True
                        Exit Sub
                    End If
            End Select
        End If

        If IsNumeric(txtMeasured.Text) Then
            If txtUpperLimit.Text = "N/A" Then
                If Val(txtMeasured.Text) >= Val(txtLowerLimit.Text) Then
                    Me.txtMeasured.BackColor = System.Drawing.Color.Lime
                    Me.txtMeasuredBig.BackColor = System.Drawing.Color.Lime
                    Pass = True
                    Failed = False
                    OutHigh = True
                    OutLow = False
                Else
                    Me.txtMeasured.BackColor = System.Drawing.Color.Red
                    If Val(Me.txtMeasured.Text) = 9.9E+37 Then
                        Me.txtMeasured.Text = "No Meas"
                        Me.txtMeasuredBig.Text = "No Meas"
                    End If
                    Pass = False
                    Failed = True
                    OutHigh = False
                    OutLow = True
                End If
                Exit Sub
            End If

            If txtLowerLimit.Text = "N/A" Then
                If Val(txtMeasured.Text) <= Val(txtUpperLimit.Text) Then
                    Me.txtMeasured.BackColor = System.Drawing.Color.Lime
                    Me.txtMeasuredBig.BackColor = System.Drawing.Color.Lime
                    Pass = True
                    Failed = False
                    OutHigh = False
                    OutLow = True
                Else
                    Me.txtMeasured.BackColor = System.Drawing.Color.Red
                    Me.txtMeasuredBig.BackColor = System.Drawing.Color.Red
                    Pass = False
                    Failed = True
                    OutHigh = True
                    OutLow = False
                End If
                Exit Sub
            End If

            If Val(txtMeasured.Text) > Val(txtUpperLimit.Text) Or Val(txtMeasured.Text) < Val(txtLowerLimit.Text) Then
                Me.txtMeasured.BackColor = System.Drawing.Color.Red
                Me.txtMeasuredBig.BackColor = System.Drawing.Color.Red
                Pass = False
                Failed = True
                If Val(txtMeasured.Text) > Val(txtUpperLimit.Text) Then OutHigh = True Else OutHigh = False
                If Val(txtMeasured.Text) < Val(txtLowerLimit.Text) Then OutLow = True Else OutLow = False
            Else
                Me.txtMeasured.BackColor = System.Drawing.Color.Lime
                Me.txtMeasuredBig.BackColor = System.Drawing.Color.Lime
                Pass = True
                Failed = False
                OutHigh = False
                OutLow = False
            End If
        Else
            If txtMeasured.Text <> txtUpperLimit.Text Then
                Me.txtMeasured.BackColor = System.Drawing.Color.Red
                Me.txtMeasuredBig.BackColor = System.Drawing.Color.Red
                Pass = False
                Failed = True
                OutHigh = False
                OutLow = False
            Else
                Me.txtMeasured.BackColor = System.Drawing.Color.Lime
                Pass = True
                Failed = False
                OutHigh = False
                OutLow = False
            End If
        End If
    End Sub

    Private Sub txtUnit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnit.TextChanged
        If UCase(txtUnit.Text) = "DTB" Then
            txtUpperLimit.Text = "N/A"
            txtLowerLimit.Text = "N/A"
            lblHigh.Text = "N/A"
            lblLow.Text = "N/A"
        End If
    End Sub

    Private Sub txtUpperLimit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUpperLimit.TextChanged
        If Val(txtUpperLimit.Text) = 1.0E+99 Then
            txtUpperLimit.Text = "N/A"
            lblHigh.Text = "N/A"
        Else
            lblHigh.Text = txtUpperLimit.Text
        End If
    End Sub

    Private Sub YesButton_Click(sender As Object, e As EventArgs) Handles YesButton.Click
        UserEvent = YES_BUTTON
    End Sub

    Public Function MenuOption(ByVal Index As Integer) As System.Windows.Forms.Button
        Select Case Index
            Case 1
                MenuOption = MenuOption_1
            Case 2
                MenuOption = MenuOption_2
            Case 3
                MenuOption = MenuOption_3
            Case 4
                MenuOption = MenuOption_4
            Case 5
                MenuOption = MenuOption_5
            Case 6
                MenuOption = MenuOption_6
            Case 7
                MenuOption = MenuOption_7
            Case 8
                MenuOption = MenuOption_8
            Case 9
                MenuOption = MenuOption_9
            Case 10
                MenuOption = MenuOption_10
            Case 11
                MenuOption = MenuOption_11
            Case 12
                MenuOption = MenuOption_12
            Case 13
                MenuOption = MenuOption_13
            Case 14
                MenuOption = MenuOption_14
            Case Else
                MenuOption = MenuOption_1
        End Select
    End Function

    Public Function MenuOptionText(ByVal Index As Integer) As System.Windows.Forms.Label
        Select Case Index
            Case 1
                MenuOptionText = MenuOptionText_1
            Case 2
                MenuOptionText = MenuOptionText_2
            Case 3
                MenuOptionText = MenuOptionText_3
            Case 4
                MenuOptionText = MenuOptionText_4
            Case 5
                MenuOptionText = MenuOptionText_5
            Case 6
                MenuOptionText = MenuOptionText_6
            Case 7
                MenuOptionText = MenuOptionText_7
            Case 8
                MenuOptionText = MenuOptionText_8
            Case 9
                MenuOptionText = MenuOptionText_9
            Case 10
                MenuOptionText = MenuOptionText_10
            Case 11
                MenuOptionText = MenuOptionText_11
            Case 12
                MenuOptionText = MenuOptionText_12
            Case 13
                MenuOptionText = MenuOptionText_13
            Case 14
                MenuOptionText = MenuOptionText_14
            Case Else
                MenuOptionText = MenuOptionText_1
        End Select
    End Function

    Public Function lblInstruction(ByVal Index As Integer) As System.Windows.Forms.Label
        Select Case Index
            Case 1
                lblInstruction = lblInstruction_1
            Case 2
                lblInstruction = lblInstruction_2
            Case 3
                lblInstruction = lblInstruction_3
            Case 4
                lblInstruction = lblInstruction_4
            Case 5
                lblInstruction = lblInstruction_5
            Case 6
                lblInstruction = lblInstruction_6
            Case Else
                lblInstruction = lblInstruction_1
        End Select
    End Function

    Public Function lblModuleStatusTitle(ByVal Index As Integer) As System.Windows.Forms.Label
        Select Case Index
            Case 1
                lblModuleStatusTitle = lblModuleStatusTitle_1
            Case 2
                lblModuleStatusTitle = lblModuleStatusTitle_2
            Case 3
                lblModuleStatusTitle = lblModuleStatusTitle_3
            Case Else
                lblModuleStatusTitle = lblModuleStatusTitle_1
        End Select
    End Function

    Public Function picStatus(ByVal index As Integer) As System.Windows.Forms.PictureBox
        Select Case index
            Case 0
                picStatus = picStatus_0
            Case 1
                picStatus = picStatus_1
            Case 2
                picStatus = picStatus_2
            Case 3
                picStatus = picStatus_3
            Case 4
                picStatus = picStatus_4
            Case Else
                picStatus = picStatus_1
        End Select
    End Function

    Public Function lblMsg(ByVal Index As Integer) As Label
        Dim labelName As String = "lblMsg_" & Index.ToString()
        Dim parent As Control = lblMsg_1.Parent

        If LabelExists(labelName, parent) Then
            lblMsg = GetLabel(labelName, parent)
        Else
            Dim lbl As New Label
            lbl.Name = labelName
            lbl.BackColor = lblMsg_1.BackColor
            lbl.Cursor = lblMsg_1.Cursor
            lbl.Font = lblMsg_1.Font
            lbl.ForeColor = lblMsg_1.ForeColor
            lbl.RightToLeft = lblMsg_1.RightToLeft
            lbl.BorderStyle = lblMsg_1.BorderStyle
            lbl.Left = lblMsg_1.Left
            lbl.Size = lblMsg_1.Size
            lbl.TextAlign = lblMsg_1.TextAlign
            lbl.Visible = lblMsg_1.Visible
            parent.Controls.Add(lbl)
            lblMsg = lbl
        End If
    End Function

    Public Function lblModuleStatus(ByVal Index As Integer) As Label
        Dim labelName As String = "lblModuleStatus_" & Index.ToString()
        Dim parent As Control = lblModuleStatus_1.Parent

        If LabelExists(labelName, parent) Then
            lblModuleStatus = GetLabel(labelName, parent)
        Else
            Dim lbl As New Label
            lbl.Name = labelName
            lbl.BackColor = lblModuleStatus_1.BackColor
            lbl.Cursor = lblModuleStatus_1.Cursor
            lbl.Font = lblModuleStatus_1.Font
            lbl.ForeColor = lblModuleStatus_1.ForeColor
            lbl.RightToLeft = lblModuleStatus_1.RightToLeft
            lbl.BorderStyle = lblModuleStatus_1.BorderStyle
            lbl.Left = lblModuleStatus_1.Left
            lbl.Size = lblModuleStatus_1.Size
            lbl.TextAlign = lblModuleStatus_1.TextAlign
            lbl.Visible = lblModuleStatus_1.Visible
            parent.Controls.Add(lbl)
            lblModuleStatus = lbl
        End If
    End Function

    Public Function lblModuleName(ByVal Index As Integer) As Label
        Dim labelName As String = "lblModuleName_" & Index.ToString()
        Dim parent As Control = lblModuleName_1.Parent

        If LabelExists(labelName, parent) Then
            lblModuleName = GetLabel(labelName, parent)
        Else
            Dim lbl As New Label
            lbl.Name = labelName
            lbl.BackColor = lblModuleName_1.BackColor
            lbl.Cursor = lblModuleName_1.Cursor
            lbl.Font = lblModuleName_1.Font
            lbl.ForeColor = lblModuleName_1.ForeColor
            lbl.RightToLeft = lblModuleName_1.RightToLeft
            lbl.BorderStyle = lblModuleName_1.BorderStyle
            lbl.Left = lblModuleName_1.Left
            lbl.Size = lblModuleName_1.Size
            lbl.TextAlign = lblModuleName_1.TextAlign
            lbl.Visible = lblModuleName_1.Visible
            parent.Controls.Add(lbl)
            lblModuleName = lbl
        End If
    End Function

    Public Function lblModuleRunTime(ByVal Index As Integer) As Label
        Dim labelName As String = "lblModuleRunTime_" & Index.ToString()
        Dim parent As Control = lblModuleRunTime_1.Parent

        If LabelExists(labelName, parent) Then
            lblModuleRunTime = GetLabel(labelName, parent)
        Else
            Dim lbl As New Label
            lbl.Name = labelName
            lbl.BackColor = lblModuleRunTime_1.BackColor
            lbl.Cursor = lblModuleRunTime_1.Cursor
            lbl.Font = lblModuleRunTime_1.Font
            lbl.ForeColor = lblModuleRunTime_1.ForeColor
            lbl.RightToLeft = lblModuleRunTime_1.RightToLeft
            lbl.BorderStyle = lblModuleRunTime_1.BorderStyle
            lbl.Left = lblModuleRunTime_1.Left
            lbl.Size = lblModuleRunTime_1.Size
            lbl.TextAlign = lblModuleRunTime_1.TextAlign
            lbl.Visible = lblModuleRunTime_1.Visible
            parent.Controls.Add(lbl)
            lblModuleRunTime = lbl
        End If
    End Function

    ''' <summary>
    ''' This function check for the existance of a label on this form
    ''' </summary>
    ''' <param name="LabelName">Name property of the label in question</param>
    ''' <param name="parent">Reference to the parent of the label in question</param>
    ''' <returns>true if the label exists otherwise false</returns>
    ''' <remarks></remarks>
    Private Function LabelExists(ByVal LabelName As String, ByRef parent As Control) As Boolean
        Dim retVal As Boolean = False

        For Each ctrl As Control In parent.Controls
            If (TypeOf ctrl Is Label) And (ctrl.Name = LabelName) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Gets the instance of a label by name.  This function should only be called after a call 
    ''' to LabelExists confirms the label exists or the return must be checked for 'Nothing'
    ''' </summary>
    ''' <param name="LabelName">Name property of the label of interest</param>
    ''' <param name="parent">Reference to the parent of the label of interest</param>
    ''' <returns>Reference to an instance of label with the desired name property or 'Nothing' if thelable does not exist</returns>
    ''' <remarks></remarks>
    Private Function GetLabel(ByVal LabelName As String, ByRef parent As Control) As Label
        For Each ctrl As Control In parent.Controls
            If (TypeOf ctrl Is Label) And (ctrl.Name = LabelName) Then
                Return ctrl
            End If
        Next
        Return Nothing
    End Function

    Public Sub UnloadLabel(ByRef lbl As Label)
        lbl.Parent.Controls.Remove(lbl)
        lbl = Nothing
    End Sub

    Public Function cmdModule(ByVal Index As Integer) As Button
        Dim buttonName As String = "cmdModule_" & Index.ToString()
        Dim parent As Control = cmdModule_1.Parent

        If ButtonExists(buttonName, parent) Then
            cmdModule = GetButton(buttonName, parent)
        Else
            Dim btn As New Button
            btn.Name = buttonName
            btn.AutoSize = cmdModule_1.AutoSize
            btn.BackColor = cmdModule_1.BackColor
            btn.ImageList = cmdModule_1.ImageList
            btn.ImageIndex = 3
            btn.Size = cmdModule_1.Size
            ToolTip1.SetToolTip(btn, "Click Here to Run Test Module")
            btn.UseVisualStyleBackColor = False
            parent.Controls.Add(btn)
            AddHandler btn.Click, AddressOf cmdModuleArray_Click
            cmdModule = btn
        End If
    End Function

    ''' <summary>
    ''' This function check for the existance of a label on this form
    ''' </summary>
    ''' <param name="buttonName">Name property of the button in question</param>
    ''' <param name="parent">Reference to the parent of the button in question</param>
    ''' <returns>true if the button exists otherwise false</returns>
    ''' <remarks></remarks>
    Private Function ButtonExists(ByVal buttonName As String, ByRef parent As Control) As Boolean
        For Each ctrl As Control In parent.Controls
            If (TypeOf ctrl Is Button) And (ctrl.Name = buttonName) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Gets the instance of a button by name.  This function should only be called after a call 
    ''' to ButtonExists confirms the label exists or the return must be checked for 'Nothing'
    ''' </summary>
    ''' <param name="buttonName">Name property of the button of interest</param>
    ''' <param name="parent">Reference to the parent of the button of interest</param>
    ''' <returns>Reference to an instance of button with the desired name property or 'Nothing' if the 
    ''' button does not exist</returns>
    ''' <remarks></remarks>
    Private Function GetButton(ByVal buttonName As String, ByRef parent As Control) As Button
        For Each ctrl As Control In parent.Controls
            If (TypeOf ctrl Is Button) And (ctrl.Name = buttonName) Then
                Return ctrl
            End If
        Next
        Return Nothing
    End Function
End Class