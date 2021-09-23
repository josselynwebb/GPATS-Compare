Option Strict Off
Option Explicit On

Imports System
Imports System.IO

Friend Class frmConfig
    Inherits System.Windows.Forms.Form

    ''' <summary>
    ''' Provide the user an interface to select a Video Configuration file.
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    Private Sub cmdBrowse_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdBrowse.Click
        Dim ofd As OpenFileDialog = New OpenFileDialog()

        Try
            With ofd
                .Title = "Open Video Configuration File"
                .InitialDirectory = Directory.GetCurrentDirectory()
                .Filter = "Camera Configuration Files (*.fmt)|*.fmt"
                .DefaultExt = "fmt"
                .CheckFileExists = True
                If .ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
                cboCamConfig.SelectedIndex = 0
                cboCamConfig.SelectedItem = .FileName
            End With
        Catch ex As SystemException
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmdHelp_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdHelp.Click
        Enabled = False
        'Only open one instance
        If gbGraphicOpen = False Then frmGraphic.ShowDialog()
        Enabled = True
    End Sub

    ''' <summary>
    ''' Currently only Supporting EPIX's Video Format "Video 720x480i 60HZ (RS-170)" until we get SBR's Files and info from them
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    Private Sub cmdOK_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdOK.Click
        'Only show NIR Camera Controls if NIR video is selected
        If cboCamConfig.SelectedIndex > 0 Then
            gsVidFormatFile = IrwinDir & "Irwd" & cboCamConfig.Text & VID_FMT_EXT
        Else
            If cboCamConfig.Text = "ALTA NIR Camera" Then
                'ALTA NIR Camera is RS-170 format
                'gsCNF_File = gsIrwinDir & "\IrwdRS170_640x480.txt"
                'gsVidFormatFile = gsIrwinDir & "Video 720x480i 60HZ (RS-170).fmt"
                gsVidFormatFile = IrwinDir & "IrwdRS170_640x480" & VID_FMT_EXT
            Else
                gsVidFormatFile = IrwinDir & "Irwd" & cboCamConfig.Text
                'Check for a Correct extension, if its not there, add it
                If Strings.Right(gsVidFormatFile, 4).ToLower <> VID_FMT_EXT Then
                    gsVidFormatFile = gsVidFormatFile & VID_FMT_EXT
                End If
            End If
        End If
        gpixciCamera.formatfile = gsVidFormatFile

        'Make sure the file exists
        If Not File.Exists(gsVidFormatFile) Then
            MsgBox("File not Found: " & gsVidFormatFile)
            Exit Sub
        End If

        Me.Close()
    End Sub
End Class