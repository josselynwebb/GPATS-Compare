Option Strict Off
Option Explicit On

''' <summary>
''' Provide functionality to display graphic images
''' for help in the FoV Calculator (Design Mode).
''' </summary>
''' <remarks></remarks>
Friend Class frmGraphic
    Inherits System.Windows.Forms.Form

#Region "Private Events"

    Private Sub frmGraphic_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
        gbGraphicOpen = True 'Set Open Flag
        If gbRunFromNAM Then
            imgHelp.Visible = False
            pboxViewDir.Visible = True
            pboxViewDir.Load(gsGraphicFile)

            If gbGraphicText = True Then
                txtGraphic.Visible = True
                pboxViewDir.Top = 40
                txtGraphic.Text = gsGraphicText
                Height = 382
            Else
                txtGraphic.Visible = False
                pboxViewDir.Top = 0
                Height = 342
            End If
        Else
            imgHelp.Visible = True
            pboxViewDir.Visible = False
            txtGraphic.Visible = False
        End If

        Width = 586
        'Position in upper right corner of screen
        Top = 0
        Left = Screen.PrimaryScreen.Bounds.Width - Width
    End Sub

    Private Sub frmGraphic_FormClosed(ByVal eventSender As Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gbGraphicOpen = False 'Reset Open Flag

        If gbRunFromNAM = True Then
            Select Case gshMode

                Case ALIGN
                    gofrmMain.cmdCapture.Enabled = True 'Reenable [Help] Button
                    gbCapture = True
                    OverlayTarget() 'Resume capturing Video
                Case FOV, TGTCOORD
                    gofrmROI.cmdHelp.Enabled = True 'Reenable [Help] Button
            End Select
        Else
            frmConfig.cmdHelp.Enabled = True 'Reenable [Help] Button
        End If
    End Sub

    Public Sub frmGraphic_Resize(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Resize

        If Me.WindowState = System.Windows.Forms.FormWindowState.Minimized Then Exit Sub

        On Error Resume Next 'To handle extremely small sizes

        With pboxViewDir
            If gbGraphicText = True Then
                .Height = Height - (txtGraphic.Height + 28)
                .Width = Width - 8
                txtGraphic.Width = .Width
            Else
                .Height = Height - 28
                .Width = Width - 8
            End If
        End With
    End Sub
#End Region '"Private Events"
End Class