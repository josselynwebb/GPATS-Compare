
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports Microsoft.VisualBasic

Public Class frmLaRRS
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cmdAbort As System.Windows.Forms.Button
    Friend WithEvents txtEL As System.Windows.Forms.TextBox
    Friend WithEvents txtAZ As System.Windows.Forms.TextBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents Line3 As System.Windows.Forms.Label
    Friend WithEvents Line2 As System.Windows.Forms.Label
    Friend WithEvents Line1 As System.Windows.Forms.Label
    Friend WithEvents Shape1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdAbort = New System.Windows.Forms.Button()
        Me.txtEL = New System.Windows.Forms.TextBox()
        Me.txtAZ = New System.Windows.Forms.TextBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.Line3 = New System.Windows.Forms.Label()
        Me.Line2 = New System.Windows.Forms.Label()
        Me.Line1 = New System.Windows.Forms.Label()
        Me.Shape1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdAbort
        '
        Me.cmdAbort.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbort.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbort.Location = New System.Drawing.Point(238, 100)
        Me.cmdAbort.Name = "cmdAbort"
        Me.cmdAbort.Size = New System.Drawing.Size(122, 33)
        Me.cmdAbort.TabIndex = 9
        Me.cmdAbort.Text = "Abort"
        Me.cmdAbort.UseVisualStyleBackColor = False
        '
        'txtEL
        '
        Me.txtEL.BackColor = System.Drawing.SystemColors.Window
        Me.txtEL.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtEL.Location = New System.Drawing.Point(60, 158)
        Me.txtEL.Name = "txtEL"
        Me.txtEL.Size = New System.Drawing.Size(92, 13)
        Me.txtEL.TabIndex = 3
        Me.txtEL.Text = "0"
        '
        'txtAZ
        '
        Me.txtAZ.BackColor = System.Drawing.SystemColors.Window
        Me.txtAZ.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAZ.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAZ.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtAZ.Location = New System.Drawing.Point(60, 141)
        Me.txtAZ.Name = "txtAZ"
        Me.txtAZ.Size = New System.Drawing.Size(93, 13)
        Me.txtAZ.TabIndex = 2
        Me.txtAZ.Text = "0"
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(241, 142)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(122, 33)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'Line3
        '
        Me.Line3.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line3.Location = New System.Drawing.Point(71, 97)
        Me.Line3.Name = "Line3"
        Me.Line3.Size = New System.Drawing.Size(86, 1)
        Me.Line3.TabIndex = 10
        '
        'Line2
        '
        Me.Line2.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line2.Location = New System.Drawing.Point(71, 93)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(86, 1)
        Me.Line2.TabIndex = 11
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line1.Location = New System.Drawing.Point(71, 89)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(86, 1)
        Me.Line1.TabIndex = 12
        '
        'Shape1
        '
        Me.Shape1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Shape1.Location = New System.Drawing.Point(19, 86)
        Me.Shape1.Name = "Shape1"
        Me.Shape1.Size = New System.Drawing.Size(154, 92)
        Me.Shape1.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(27, 98)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(137, 17)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Santa Barbara Infrared, Inc"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(27, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 17)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "SBIR"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(26, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "EL:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(26, 140)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "AZ:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(26, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "LaRRS SN: xxxxxx"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(6, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(365, 74)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Verify the LaRRS  AZ (Azimuth) and EL (Elevation) data. "
        '
        'frmLaRRS
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(374, 185)
        Me.Controls.Add(Me.txtEL)
        Me.Controls.Add(Me.txtAZ)
        Me.Controls.Add(Me.cmdAbort)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Line3)
        Me.Controls.Add(Me.Line2)
        Me.Controls.Add(Me.Line1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Shape1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLaRRS"
        Me.ShowInTaskbar = False
        Me.Text = "Verify the LaRRS Azimuth and Elevation Data"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================
    'This form is used to verify/change the LaRRS.dat file

    Dim AZ As Integer
    Dim EL As Integer
    Dim Datachanged As Boolean
    Dim LarrsData(32) As String
    Dim LineCount As Short

    Private Sub cmdAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbort.Click

        AbortTest = True
        Me.Close()

    End Sub

    Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        cmdOK_Click()
    End Sub
    Public Sub cmdOK_Click()

        Dim x As DialogResult
        If Val(txtAZ.Text)<0 Or Val(txtAZ.Text)>20000 Then
            x = MsgBox("AZ value must be in range of 0-20000", MsgBoxStyle.OkCancel)
            Exit Sub
        End If
        If Val(txtEL.Text)<0 Or Val(txtEL.Text)>20000 Then
            x = MsgBox("EL value must be in range of 0-20000", MsgBoxStyle.OkCancel)
            Exit Sub
        End If
        Me.Close()

    End Sub


    Private Sub frmLaRRS_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        If Not IsElevated(System.Environment.UserName) Then ' don't allow the operator to change data
            txtAZ.SelectionStart = 0
            txtAZ.SelectionLength = Len(txtAZ.Text)
            txtAZ.Focus()
        End If

    End Sub

    Private Sub frmLaRRS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim x As Integer, i As Integer, S As String
        Dim lpBuffer As String = Space(255) 'Input Buffer for the API Call
        Dim nSize As Integer 'Size of the Input Buffer for the API Call
        Dim ReturnValue As Integer 'Return Status of the API Call
        Me.Left = (PrimaryScreen.Bounds.Width-Me.Width)/2
        Me.Top = (PrimaryScreen.Bounds.Height-Me.Height)/2
        Datachanged = False

        nSize = 255 'Set Bufffer Size

        If Not IsElevated(System.Environment.UserName) Then ' don't allow the operator to change data
            S = "Verify the LaRRS AZ (Azimuth) and EL (Elevation) data." + vbCrLf
            S &= "These numbers are found on the LaRRS serial number label." & vbCrLf
            S &= "If different, consult your system adminstrator to correct the problem." & vbCrLf
            S &= "Then press OK."
        Else
            S = "Verify the LaRRS AZ (Azimuth) and EL (Elevation) data." + vbCrLf
            S &= "These numbers are found on the LaRRS serial number label." & vbCrLf
            S &= "If different, correct the problem here." & vbCrLf
            S &= "Then press OK."
        End If
        Label1.Text = S

        'Load Larrs.dat file into the LarrsData array
        Array.Clear(LarrsData, 0, LarrsData.Length)
        LineCount = 0
        If Dir(sLarrs_path & "LaRRS.dat", 17) <> "" Then
            x = FreeFile()
            FileOpen(x, sLarrs_path & "LaRRS.dat", OpenMode.Input)
            For i = 1 To 20
                If EOF(x) Then Exit For
                LarrsData(i) = LineInput(x)
            Next i
            FileClose(x)
            LineCount = i - 1

            'Get the current AZ and EL values
            S = LarrsData(1)
            x = InStr(S, ",")
            If x > 0 Then
                AZ = Val(Strings.Left(S, x - 1))
                S = Mid(S, x + 1)
                x = InStr(S, ",")
                If x > 0 Then
                    EL = Val(Strings.Left(S, x - 1))
                    S = Mid(S, x + 1)
                Else ' may not be a 3rd character
                    If IsNumeric(S) Then
                        EL = Val(S)
                    Else
                        EL = 0
                    End If
                End If
            End If
            txtAZ.Text = CStr(AZ)
            txtEL.Text = CStr(EL)
            Application.DoEvents()
        End If

        If Not IsElevated(System.Environment.UserName) Then
            Application.DoEvents()
            txtAZ.ReadOnly = True
            txtEL.ReadOnly = True
        End If

    End Sub

    Private Sub Form_QueryUnload(ByRef Cancel As Integer, ByVal UnloadMode As Short)

        If UnloadMode = CloseReason.UserClosing Then
            If Datachanged = True Then
                cmdOK_Click()
            End If
            If Val(txtAZ.Text) < 0 Or Val(txtAZ.Text) > 20000 Then
                Cancel = True
            End If
            If Val(txtEL.Text) < 0 Or Val(txtEL.Text) > 20000 Then
                Cancel = True
            End If
        End If

    End Sub

    Private Sub frmLaRRS_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Integer = 0
        Form_QueryUnload(Cancel, 0)
        If Cancel <> 0 Then
            e.Cancel = True
            Exit Sub
        End If
        Form_Unload(Cancel)
        If Cancel <> 0 Then e.Cancel = True
    End Sub

    Private Sub Form_Unload(ByRef Cancel As Short)

        Dim x As Integer, i As Integer
        Dim S As String

        Azimuth = txtAZ.Text
        Elevation = txtEL.Text

        If Datachanged=False Then Exit Sub ' exit without saving
        If AbortTest=True Then Exit Sub

        ' don't allow the operator to change the LaRRS.dat file,  PA 07/08/2008

        If Lcase(UserName)="operator" Then ' don't allow the operator to change data
            Exit Sub
        End If

        'Sample LaRRS.dat file
        '1       310, 566, 30, 4500, 20, 20, 310, 246
        '2       310, 566, 30, 4500, 20, 20, 310, 246
        '3       310, 566, 30, 12000, 20, 20, 310, 246
        '4
        '5
        '6       Data sequence - LARRS AZ position, LARRS EL POSITION, Lower Area Limit, Upper Area limit,
        '7       Signal Block Low x, Low y, Upper x, Upper y
        '8
        '9       LARRS position is only written to the device once prior to 1540 testing but is fetched three times from the data above
        '10      in order to simplify array handling
        '11
        '12      Lasers - line #1 is 1540, #2 is 1570, #3 is 1064
        '13
        '14      This file to be placed in the c:\ directory, NOT in the self-test software directory.

        'overwrite LaRRs.dat file
        AZ = Val(txtAZ.Text)
        EL = Val(txtEL.Text)
        S = CStr(AZ) & ", " & CStr(EL) & ", 30, 4500, 20, 20, 310, 246" + vbCrLf
        S &= CStr(AZ) & ", " & CStr(EL) & ", 30, 4500, 20, 20, 310, 246" + vbCrLf
        S &= CStr(AZ) & ", " & CStr(EL) & ", 30, 12000, 20, 20, 310, 246" + vbCrLf
        ''S &= vbCrLf
        ''S &= vbCrLf
        ''S &= "Data sequence - LARRS AZ position, LARRS EL POSITION, Lower Area Limit, Upper Area limit," & vbCrLf
        ''S &= "Signal Block Low x, Low y, Upper x, Upper y" & vbCrLf
        ''S &= vbCrLf
        ''S &= "LARRS position is only written to the device once prior to 1540 testing but is fetched three times from the data above" & vbCrLf
        ''S &= "in order to simplify array handling" & vbCrLf
        ''S &= vbCrLf
        ''S &= "Lasers - line #1 is for 1540, #2 is for 1570, #3 is for 1064" & vbCrLf
        ''S &= vbCrLf
        ''S &= "This file to be placed in the " & sLarrs_path & " directory, NOT in the self-test software directory." & vbCrLf

        Try ' On Error GoTo WriteError
            x = FreeFile()
            FileOpen(x, sLarrs_path & "LaRRS.dat", OpenMode.Output)
            PrintLine(x, S)

ResumeError:
            FileClose(x)
            '? On Error Resume Next 
            Exit Sub

        Catch   ' WriteError:
            If (Err.Number <> 0) Then
                MsgBox("Error Trying to write to " & sLarrs_path & "LaRRS.dat file!" & vbCrLf & "Error Number: " & Err.Number & vbCrLf & "Error Description: " & Err.Description, MsgBoxStyle.Exclamation)
                Err.Clear()
                ErrorDetected = True
            End If
            GoTo ResumeError

        End Try
    End Sub

    Private Sub txtAZ_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAZ.TextChanged
        If Not sender.Created() Then Exit Sub

        Datachanged = True

    End Sub

    Private Sub txtAZ_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAZ.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)


        '  If KeyAscii = key_Enter Then
        '    txtEL.SelStart = 0
        '    txtEL.SelLength = Len(txtEL.Text)
        '    txtEL.SetFocus
        '  End If


        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtEL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEL.TextChanged
        If Not sender.Created() Then Exit Sub

        Datachanged = True

    End Sub

    Private Sub txtEL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEL.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)


        If KeyAscii=key_Enter Then
            cmdOK.Focus()
        End If



        e.KeyChar = Chr(KeyAscii)
    End Sub

End Class