
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class frmDataType
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Form_Initialize()

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
    Friend WithEvents txtData As System.Windows.Forms.TextBox
    Friend WithEvents Frame1 As System.Windows.Forms.GroupBox
    Friend WithEvents optText As System.Windows.Forms.RadioButton
    Friend WithEvents optHex As System.Windows.Forms.RadioButton
    Friend WithEvents opt01 As System.Windows.Forms.RadioButton
    Friend WithEvents optLH As System.Windows.Forms.RadioButton
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtData = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.optText = New System.Windows.Forms.RadioButton()
        Me.optHex = New System.Windows.Forms.RadioButton()
        Me.opt01 = New System.Windows.Forms.RadioButton()
        Me.optLH = New System.Windows.Forms.RadioButton()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtData
        '
        Me.txtData.BackColor = System.Drawing.SystemColors.Window
        Me.txtData.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.Location = New System.Drawing.Point(8, 65)
        Me.txtData.Multiline = True
        Me.txtData.Name = "txtData"
        Me.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtData.Size = New System.Drawing.Size(333, 90)
        Me.txtData.TabIndex = 3
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.optText)
        Me.Frame1.Controls.Add(Me.optHex)
        Me.Frame1.Controls.Add(Me.opt01)
        Me.Frame1.Controls.Add(Me.optLH)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(8, 162)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Size = New System.Drawing.Size(333, 41)
        Me.Frame1.TabIndex = 2
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Data Types"
        '
        'optText
        '
        Me.optText.BackColor = System.Drawing.SystemColors.Control
        Me.optText.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optText.Location = New System.Drawing.Point(275, 12)
        Me.optText.Name = "optText"
        Me.optText.Size = New System.Drawing.Size(50, 25)
        Me.optText.TabIndex = 7
        Me.optText.Text = "Text"
        Me.optText.UseVisualStyleBackColor = False
        '
        'optHex
        '
        Me.optHex.BackColor = System.Drawing.SystemColors.Control
        Me.optHex.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optHex.Location = New System.Drawing.Point(178, 16)
        Me.optHex.Name = "optHex"
        Me.optHex.Size = New System.Drawing.Size(82, 17)
        Me.optHex.TabIndex = 6
        Me.optHex.Text = "Hexidecimal"
        Me.optHex.UseVisualStyleBackColor = False
        '
        'opt01
        '
        Me.opt01.BackColor = System.Drawing.SystemColors.Control
        Me.opt01.ForeColor = System.Drawing.SystemColors.ControlText
        Me.opt01.Location = New System.Drawing.Point(105, 12)
        Me.opt01.Name = "opt01"
        Me.opt01.Size = New System.Drawing.Size(66, 25)
        Me.opt01.TabIndex = 5
        Me.opt01.Text = "0101 Bits"
        Me.opt01.UseVisualStyleBackColor = False
        '
        'optLH
        '
        Me.optLH.BackColor = System.Drawing.SystemColors.Control
        Me.optLH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLH.Location = New System.Drawing.Point(24, 16)
        Me.optLH.Name = "optLH"
        Me.optLH.Size = New System.Drawing.Size(74, 17)
        Me.optLH.TabIndex = 4
        Me.optLH.Text = "LHLH Bits"
        Me.optLH.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(146, 210)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(66, 33)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 17)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Data"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(333, 33)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Please select the data type you are sending.  If you change the data below it wil" & _
    "l be sent instead of what was being sent earlier."
        '
        'frmDataType
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(349, 253)
        Me.Controls.Add(Me.txtData)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "frmDataType"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Select Data Type"
        Me.Frame1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub

#End Region

	'=========================================================
    '**************************************************************************
    '   Description:  This form lets the user pick the data type they wish to *
    '                 send when the program can't determine the proper type   *
    '                                                                         *
    '**************************************************************************
    Public hex As Boolean
    Public bits01 As Boolean
    Public bitsLH As Boolean
    Public txt As Boolean
    Public senddata As String
    Public formShown As Boolean

    Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If opt01.Checked = True Or optLH.Checked = True Or optText.Checked = True Or optHex.Checked = True Then
            senddata = txtData.Text
            hex = optHex.Checked
            bits01 = opt01.Checked
            bitsLH = optLH.Checked
            txt = optText.Checked
            Me.Hide()
        End If
    End Sub

    Private Sub frmDataType_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        txtData.Text = senddata
        optHex.Enabled = hex
        opt01.Enabled = bits01
        optLH.Enabled = bitsLH
        formShown = True
    End Sub

    Private Sub Form_Initialize()
        txtData.Text = senddata
    End Sub

    Private Sub frmDataType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtData.Text = senddata
        optHex.Enabled = hex
        opt01.Enabled = bits01
        optLH.Enabled = bitsLH
    End Sub

    Private Sub txtData_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtData.TextChanged
        If Not sender.Created() Then Exit Sub
        checktype(txtData.Text)

    End Sub

    Sub checktype(ByVal data As String)
        'determine type of data
        Dim i As Integer
        Dim c As String

        'check to see if data is in the form "LHLHHHLL, LLHHLLHH"
        Dim isLHbits As Boolean
        isLHbits = True
        For i = 1 To Len(data)
            c = Mid(data, i, 1)
            Select Case c
                Case "L", "H", ",", " ", "l", "h"

                Case Else
                    isLHbits = False
            End Select
        Next i

        'check to see if data is in the form "01001001, 01010010"
        Dim is01bits As Boolean
        is01bits = True
        For i = 1 To Len(data)
            c = Mid(data, i, 1)
            Select Case c
                Case "0", "1", ",", " ":

                Case Else
                    is01bits = False
            End Select
        Next i
        Select Case InStr(data, ",")
            Case 5, 6, 7, 8, 0:

            Case Else
                is01bits = False
                isLHbits = False
        End Select
        If Len(data) < 5 Then
            is01bits = False
            isLHbits = False
        End If

        'check to see if data is Hex "05, 3F, 2B"
        Dim ishex As Boolean
        ishex = True

        For i = 1 To Len(data)
            c = Mid(data, i, 1)
            Select Case c
                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "a", "b", "c", "d", "e", "f", ",", " ":

                Case Else
                    ishex = False
            End Select
        Next i

        'determine if there are always two characters seperated by ", " for isHex=true
        If ishex=True Then
            For i = 0 To Len(data)/2-1
                c = Mid(data, i*2+1, 2)
                If c=", " Then
                    'do nothing
                Else
                    Dim j As Short
                    Dim d As String
                    For j = 1 To 2
                        d = Mid(c, j, 1)
                        Select Case d
                            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "a", "b", "c", "d", "e", "f":

                            Case Else
                                ishex = False
                        End Select
                    Next j
                End If
            Next i
        End If

        'check to see if it is 2 characters followed by a comma and a space
        If Len(data)>2 Then
            If InStr(data, ",")<>3 Then
                ishex = False
            End If
        End If
        optHex.Enabled = ishex
        opt01.Enabled = is01bits
        optLH.Enabled = isLHbits
    End Sub
End Class