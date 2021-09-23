Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Friend Class frmImage
    Inherits System.Windows.Forms.Form

    Private Sub frmImage_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Select Case KeyCode
            Case System.Windows.Forms.Keys.Q
                If Shift = 2 Then Quit_Click()
            Case System.Windows.Forms.Keys.R
                If Shift = 2 Then PPage_Click()
            Case System.Windows.Forms.Keys.N
                If Shift = 2 Then NPage_Click()
            Case System.Windows.Forms.Keys.P
                If Shift = 2 Then PrintButton_Click()
            Case System.Windows.Forms.Keys.A
                If Shift = 2 Then MouseControl_Click((0))
            Case System.Windows.Forms.Keys.Z
                If Shift = 2 Then MouseControl_Click((1))
            Case System.Windows.Forms.Keys.M
                If Shift = 2 Then MouseControl_Click((2))
            Case System.Windows.Forms.Keys.V
                If Shift = 2 Then ResetView_Click()
            Case System.Windows.Forms.Keys.O
                If Shift = 2 Then ZoomOut50_Click()
        End Select
    End Sub

    Private Sub frmImage_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        ResetView_Click()
    End Sub

    Private Sub frmImage_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        If WindowState = MINIMIZED Then Exit Sub

        SSPanel1.SetBounds(8, 8, Limit(Width - 24, 0), Limit(Height - 136, 0))
        ViewDirX1.SetBounds(16, 16, Limit(Width - 45, 0), Limit(Height - 152, 0))
        ImageNavigationlPanel.SetBounds(8, SSPanel1.Height + 15, 217, 81)
        ImageControlPanel.SetBounds(232, SSPanel1.Height + 15, 545, 81)
    End Sub

    Private Sub MouseControl_0_Click(sender As Object, e As EventArgs) Handles MouseControl_0.Click
        MouseControl_Click(PANNING)
    End Sub

    Private Sub MouseControl_1_Click(sender As Object, e As EventArgs) Handles MouseControl_1.Click
        MouseControl_Click(ZOOMING)
    End Sub

    Private Sub MouseControl_2_Click(sender As Object, e As EventArgs) Handles MouseControl_2.Click
        MouseControl_Click(MAGNIFYING)
    End Sub

    Private Sub MouseControl_Click(ByRef index As Short)
        Select Case index
            Case PANNING
                Me.ViewDirX1.LeftMouseTool = VIEWDIRXLib.Constants.VX_HANDPAN

            Case ZOOMING
                Me.ViewDirX1.LeftMouseTool = VIEWDIRXLib.Constants.VX_MARKZOOM

            Case MAGNIFYING
                Me.ViewDirX1.LeftMouseTool = VIEWDIRXLib.Constants.VX_MAGNIFIER
        End Select
    End Sub

    Private Sub NPage_Click(sender As Object, e As EventArgs) Handles NPage.Click
        NPage_Click()
    End Sub

    Private Sub NPage_Click()
        Dim sfunction As String

        Me.PPage.Enabled = True
        sfunction = UCase(VB.Left(Me.Text, 8))
        Select Case sfunction
            Case "SCHEMATI" 'Schematic View
                SchemPageNum = SchemPageNum + 1
                gFrmMain.cboSchematic.SelectedIndex = SchemPageNum - 1
                DoMenuChoice((VIEW_SCHEMATIC))

            Case "ASSEMBLY" 'Assembly View
                AssyPageNum = AssyPageNum + 1
                gFrmMain.cboAssembly.SelectedIndex = AssyPageNum - 1
                DoMenuChoice((VIEW_ASSEMBLY))

            Case "PARTS LI" 'Parts List View
                PartListPageNum = PartListPageNum + 1
                gFrmMain.cboPartsList.SelectedIndex = PartListPageNum - 1
                DoMenuChoice((VIEW_PARTSLIST))

                '5/9/03 by Soon Nam
                'added case for ID schematic view
            Case "ITA SCHE"
                IDSchemPageNum = IDSchemPageNum + 1
                gFrmMain.cboITAWiring.SelectedIndex = IDSchemPageNum - 1
                DoMenuChoice((VIEW_ID_SCHEMATIC))

                '5/9/03 by Soon Nam
                'added case for ID assembly view
            Case "ITA ASSE"
                IDAssyPageNum = IDAssyPageNum + 1
                If IDAssyPageNum = 1 Then PPage.Enabled = False
                gFrmMain.cboITAAssy.SelectedIndex = IDAssyPageNum - 1
                DoMenuChoice((VIEW_ID_ASSEMBLY))

                '5/9/03 by Soon Nam
                'added case for ID parts list view
            Case "ITA PART" 'ID Parts List View
                IDPartListPageNum = IDPartListPageNum + 1
                gFrmMain.cboITAPartsList.SelectedIndex = IDPartListPageNum - 1
                DoMenuChoice((VIEW_ID_PARTSLIST))
        End Select
    End Sub

    Private Sub PPage_Click(sender As Object, e As EventArgs) Handles PPage.Click
        PPage_Click()
    End Sub

    Private Sub PPage_Click()
        Dim sfunction As String

        Me.NPage.Enabled = True
        Select Case MouseAction
            Case SCROLLING
                'PageUp Display.PictureWindow
            Case Else
                sfunction = UCase(VB.Left(Me.Text, 8))
                'sfunction = UCase(gFrmImage.Caption)
                Select Case sfunction
                    Case "SCHEMATI" 'Schematic View
                        SchemPageNum = SchemPageNum - 1
                        If SchemPageNum = 1 Then PPage.Enabled = False
                        gFrmMain.cboSchematic.SelectedIndex = SchemPageNum - 1
                        DoMenuChoice((VIEW_SCHEMATIC))

                    Case "ASSEMBLY" 'Assembly View
                        AssyPageNum = AssyPageNum - 1
                        If AssyPageNum = 1 Then PPage.Enabled = False
                        gFrmMain.cboAssembly.SelectedIndex = AssyPageNum - 1
                        DoMenuChoice((VIEW_ASSEMBLY))

                    Case "PARTS LI" 'Parts List View
                        PartListPageNum = PartListPageNum - 1
                        gFrmMain.cboPartsList.SelectedIndex = PartListPageNum - 1
                        If PartListPageNum = 1 Then PPage.Enabled = False
                        DoMenuChoice((VIEW_PARTSLIST))

                        '5/9/03 by Soon Nam
                        'added case for ITA schematic view
                    Case "ITA SCHE"
                        IDSchemPageNum = IDSchemPageNum - 1
                        gFrmMain.cboITAWiring.SelectedIndex = IDSchemPageNum - 1
                        If IDSchemPageNum = 1 Then PPage.Enabled = False
                        DoMenuChoice((VIEW_ID_SCHEMATIC))

                        '5/9/03 by Soon Nam
                        'added case for ID assembly view
                    Case "ITA ASSE"
                        IDAssyPageNum = IDAssyPageNum - 1
                        gFrmMain.cboITAAssy.SelectedIndex = IDAssyPageNum - 1
                        If IDAssyPageNum = 1 Then PPage.Enabled = False
                        DoMenuChoice((VIEW_ID_ASSEMBLY))

                        '5/9/03 by Soon Nam
                        'added case for ID parts list view
                    Case "ITA PART" 'ID Parts List View
                        IDPartListPageNum = IDPartListPageNum - 1
                        gFrmMain.cboITAPartsList.SelectedIndex = IDPartListPageNum - 1
                        If IDPartListPageNum = 1 Then PPage.Enabled = False
                        DoMenuChoice((VIEW_ID_PARTSLIST))
                End Select
        End Select
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        PrintButton_Click()
    End Sub

    Private Sub PrintButton_Click()
        Me.ViewDirX1.PrintCurrentSelection()
    End Sub

    Private Sub Quit_Click(sender As Object, e As EventArgs) Handles Quit.Click
        Quit_Click()
    End Sub

    Private Sub Quit_Click()
        Me.Hide()
    End Sub

    Private Sub ResetView_Click(sender As Object, e As EventArgs) Handles ResetView.Click
        ResetView_Click()
    End Sub

    Private Sub ResetView_Click()
        LoadImage()
    End Sub

    Private Sub ZoomOut50_Click(sender As Object, e As EventArgs) Handles ZoomOut50.Click
        ZoomOut50_Click()
    End Sub

    Private Sub ZoomOut50_Click()
        Me.ViewDirX1.ZoomPercentage = 50
        Me.ViewDirX1.ZoomOut()
    End Sub
End Class