
Imports System
Imports System.Windows.Forms
Imports System.Text
Imports System.Diagnostics
Imports System.IO
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Microsoft.Win32
Imports System.IO.Compression

Public Class frmUdd
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Panel(1) = Panel_1
        'SpinButton(1) = SpinButton_1

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
    Friend WithEvents TextBox As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Friend SpinButton(2) As System.Windows.Forms.NumericUpDown
    Friend WithEvents Picture1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdBack As System.Windows.Forms.Button
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCountDown As System.Windows.Forms.Label
    Friend WithEvents lblSerNum As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents TextBox_1 As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents lblComment As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUdd))
        Me.TextBox = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.Picture1 = New System.Windows.Forms.PictureBox()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.proProgress = New System.Windows.Forms.ProgressBar()
        Me.lblCountDown = New System.Windows.Forms.Label()
        Me.lblSerNum = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.TextBox_1 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        CType(Me.TextBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox
        '
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.SystemColors.Control
        Me.Picture1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture1.Image = CType(resources.GetObject("Picture1.Image"), System.Drawing.Image)
        Me.Picture1.Location = New System.Drawing.Point(0, 261)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.Size = New System.Drawing.Size(1023, 2)
        Me.Picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture1.TabIndex = 8
        Me.Picture1.TabStop = False
        '
        'cmdBack
        '
        Me.cmdBack.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBack.Enabled = False
        Me.cmdBack.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBack.Location = New System.Drawing.Point(12, 274)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(78, 23)
        Me.cmdBack.TabIndex = 2
        Me.cmdBack.Text = "< &Back"
        Me.cmdBack.UseVisualStyleBackColor = False
        '
        'cmdNext
        '
        Me.cmdNext.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNext.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNext.Location = New System.Drawing.Point(89, 274)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(78, 23)
        Me.cmdNext.TabIndex = 3
        Me.cmdNext.Text = "&Next >"
        Me.cmdNext.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(186, 274)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(78, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'proProgress
        '
        Me.proProgress.Location = New System.Drawing.Point(12, 233)
        Me.proProgress.Name = "proProgress"
        Me.proProgress.Size = New System.Drawing.Size(272, 21)
        Me.proProgress.TabIndex = 7
        Me.proProgress.Visible = False
        '
        'lblCountDown
        '
        Me.lblCountDown.BackColor = System.Drawing.SystemColors.Control
        Me.lblCountDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountDown.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCountDown.Location = New System.Drawing.Point(48, 202)
        Me.lblCountDown.Name = "lblCountDown"
        Me.lblCountDown.Size = New System.Drawing.Size(187, 25)
        Me.lblCountDown.TabIndex = 11
        Me.lblCountDown.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblCountDown.Visible = False
        '
        'lblSerNum
        '
        Me.lblSerNum.BackColor = System.Drawing.SystemColors.Control
        Me.lblSerNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSerNum.Location = New System.Drawing.Point(66, 144)
        Me.lblSerNum.Name = "lblSerNum"
        Me.lblSerNum.Size = New System.Drawing.Size(66, 17)
        Me.lblSerNum.TabIndex = 10
        Me.lblSerNum.Text = "ATS S/N: "
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTitle.Location = New System.Drawing.Point(9, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(275, 25)
        Me.lblTitle.TabIndex = 9
        Me.lblTitle.Text = "Title of Procedure"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblComment
        '
        Me.lblComment.BackColor = System.Drawing.SystemColors.Control
        Me.lblComment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComment.Location = New System.Drawing.Point(12, 38)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(272, 189)
        Me.lblComment.TabIndex = 0
        Me.lblComment.Text = "Proper warm-up time of one half hour has not yet expired.  Please wait until the " & _
    "count down indicates that the tester is ready to conduct self test procedures."
        '
        'TextBox_1
        '
        Me.TextBox_1.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.TextBox_1.Location = New System.Drawing.Point(138, 142)
        Me.TextBox_1.Name = "TextBox_1"
        Me.TextBox_1.Range = New NationalInstruments.UI.Range(0.0R, 9000.0R)
        Me.TextBox_1.Size = New System.Drawing.Size(60, 20)
        Me.TextBox_1.TabIndex = 14
        Me.TextBox_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox_1.Value = 1000.0R
        Me.TextBox_1.Visible = False
        '
        'frmUdd
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(296, 311)
        Me.Controls.Add(Me.TextBox_1)
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.cmdBack)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.proProgress)
        Me.Controls.Add(Me.lblCountDown)
        Me.Controls.Add(Me.lblSerNum)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblComment)
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ATS Unique Data Disk (UDD) Wizard"
        CType(Me.TextBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    '=========================================================

    Dim StepNumber As Short
    
    Dim iRangeDisplay As Short
    
    Dim iCopySuccess As Short
    Const BACK_CLICK As Short = 1
    Const NEXT_CLICK As Short = 2
    Const CANCEL_CLICK As Short = 3
    Const NO_CLICK As Short = 0



    Sub AdjustWizzardStep(ByVal ButtonClicked As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                  *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*    This code updates text and objects to Updates a VIPERT*
        '*     Unique Data Disk (UDD)                               *
        '*    PARAMETERS:                                           *
        '*     ButtonClicked = Button Constant Set in a Button      *
        '*     Click Event                                          *
        '*    EXAMPLE:                                              *
        '*      AdjustWizzardStep NEXT_BUTTON                       *
        '************************************************************
        Dim Message1 As String
        Dim Success As Short
        Static CopySuccess As Short

        'Adjust GUI for Back/Next Selection
        Select Case ButtonClicked
            Case BACK_CLICK
                StepNumber -= 1
            Case NEXT_CLICK
                StepNumber += 1
            Case CANCEL_CLICK
                Close()
                Exit Sub
            Case NO_CLICK
                'No Action Required

            Case Else
                Close()
                Exit Sub
        End Select
        'Force Index Limits
        If StepNumber = 6 Then StepNumber = 5 'Modified DJoiner 05/18/01
        If StepNumber = 0 Then StepNumber = 1

        'Process Control Logic
        If (StepNumber = 2) And (ButtonClicked = NEXT_CLICK) Then
            SetCur(VIPERT_SN) = GetSerialNumber(True)
            TextBox_1.Text = VB6.Format(SetCur(VIPERT_SN), SetRes(VIPERT_SN))
        End If
        If (StepNumber = 3) And (ButtonClicked = NEXT_CLICK) Then
            WriteSerialNumber(TextBox_1.Text)
        End If
        If StepNumber = 4 Then
            proProgress.Visible = True
            cmdBack.Enabled = False
            cmdNext.Enabled = False
            cmdCancel.Enabled = False
            iCopySuccess = CopyUddFiles()
            proProgress.Visible = False
            cmdCancel.Enabled = True
            cmdBack.Enabled = True 'Modified DJoiner 05/18/01
            cmdNext.Enabled = True 'Modified DJoiner 05/18/01
        End If

        '**************************** Added to support FHDB ********************************
        'If there was an error creating the UDD -- GoTo Finish
        If StepNumber = 4 Then
            If iCopySuccess = False Then
                StepNumber = 7
            End If
        End If

        If StepNumber = 5 Then 'Modified DJoiner 05/18/01
            proProgress.Visible = True
            proProgress.Value = 0 'Reset Progress Bar
            cmdBack.Enabled = False
            cmdNext.Enabled = False
            cmdCancel.Enabled = False
            If InitRefFiles() = True Then 'If File Initialization Fails, GoTo Finish
                CheckDBFileSize()
                'File Error Handler
                If bFileError = False Then
                    If bDBOperationDue = False Then
                        CopyRestore()
                    End If
                    If bSuccess = False Then
                        iCopySuccess = False
                    Else
                        iCopySuccess = True
                    End If
                    proProgress.Visible = False
                    cmdCancel.Enabled = True
                End If
            Else
                MsgBox("FHDB keys could not be found in the ATS.INI file.")
            End If
        End If
        '************************************ End ******************************************
        If bUnload = True Then
            bUnload = False
            Exit Sub
        End If

        Select Case StepNumber
            Case 1
                'Startup message
                Me.lblTitle.Text = "Information"
                Message1 = "This wizard updates the ATS Unique Data Disk (UDD).  " & vbCrLf & vbCrLf
                Message1 &= "In the event of an Instrument Controller or Hard Disk Drive failure, "
                Message1 &= "any user logged in may restore unique data from the "
                Message1 &= "Unique Data Disk to the Instrument Controller. "
                Message1 &= "If a Unique Data Disk is lost or damaged, then this wizard will create a replacement.  "
                Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf
                Message1 &= "Click [Next>] to continue."
                Me.lblComment.Text = Message1
                cmdBack.Enabled = False
                cmdNext.Enabled = True
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Cancel"
                lblSerNum.Visible = False
                TextBox_1.Visible = False
                bSuccess = False
            Case 2
                'Enter VIPERT Serial Number
                Me.lblTitle.Text = "ATS Serial Number"
                Message1 = "Enter the serial number of the ATS: " & vbCrLf & vbCrLf
                Message1 &= "The Serial Number of the ATS is located on a black system identification plate.  "
                Message1 &= "The identification plate is located between the handles of each transit case.  "
                Message1 &= "The ATS System Serial Number is stamped in the field marked ""SERIAL NUMBER:"" "
                Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                Message1 &= "Click [Next>] to continue."
                Me.lblComment.Text = Message1
                cmdBack.Enabled = True
                cmdNext.Enabled = True
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Cancel"
                lblSerNum.Visible = True
                TextBox_1.Visible = True
            Case 3
                'Archive Files
                Me.lblTitle.Text = "Archive Files"
                If usb_flag = True Then
                    Message1 = "You have chosen to create a fresh Unique Data Disk using a USB stick. "
                    Message1 &= "Once created be sure to apply the following label: " & vbCrLf & vbCrLf
                    Message1 &= "     ATS Unique Data Disk" & vbCrLf
                    Message1 &= "     S/N: " & VB6.Format(SetCur(VIPERT_SN), SetRes(VIPERT_SN)) & vbCrLf & vbCrLf
                    Message1 &= "The system has detected your USB stick as drive '" & sFDD & "'.  When prompted, select [Start] to format the disk, unless logged in as Operator."
                    Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf
                    Message1 &= "Click [Next>] to continue."
                ElseIf usb_flag = False And uddHDflag = True Then
                    Message1 = "You have chosen to create a fresh Unique Data Disk using the system hard drive."
                    Message1 &= "The selected directory for the UDD is: " & vbCrLf & vbCrLf
                    Message1 &= sFDD & vbCrLf & vbCrLf
                    Message1 &= "If this is not the desired destination folder click [Cancel] and start again."
                    Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf
                    Message1 &= "Click [Next>] to continue."
                End If

                Me.lblComment.Text = Message1
                cmdBack.Enabled = True
                cmdNext.Enabled = True
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Cancel"
                lblSerNum.Visible = False
                TextBox_1.Visible = False
                '**************************** Added to support FHDB ********************************
            Case 4
                'Copy Database                       'Modified DJoiner 05/18/01

                If InitRefFiles() = True Then 'If File Initialization Fails, GOTO Finish
                    cmdBack.Enabled = True
                    cmdNext.Enabled = True
                    cmdNext.Focus()
                    cmdCancel.Enabled = True
                    cmdCancel.Text = "&Cancel"
                    cmdCancel.Enabled = True
                    lblSerNum.Visible = False
                    TextBox_1.Visible = False
                    Me.lblTitle.Text = "Copy FHDB Database"
                    If usb_flag = True And uddHDflag = False Then
                        Message1 = "The FHDB Database will be checked for size." & vbCrLf & vbCrLf
                        Message1 &= "If FHDB Database is less than the USB's Free Space,  it will be copied to the USB Disk." & vbCrLf
                        Message1 &= "If the FHDB Database exceeds the USB's Free Space, the Zipped size will be tested." & vbCrLf
                        Message1 &= "If the Zipped Database file doesn't exceed the USB's Free Space,"
                        Message1 &= " it will be Copied as a Zip file," & vbCrLf
                        Message1 &= "otherwise the Export or Import Operation is due. The appropriate Operation will be launched automatically "
                        Message1 &= "and the UDD Wizard will be Aborted." & vbCrLf & vbCrLf
                    ElseIf usb_flag = False And uddHDflag = True Then
                        Message1 = "The FHDB Database will be checked for size." & vbCrLf & vbCrLf
                        Message1 &= "If FHDB Database is less than the destination folder free Space,  it will be copied to the destination folder." & vbCrLf
                        Message1 &= "If the FHDB Database exceeds the destination folders free space, the zipped size will be tested." & vbCrLf
                        Message1 &= "If the zipped database file doesn't exceed the destination folder free space,"
                        Message1 &= " it will be copied as a zip file," & vbCrLf
                        Message1 &= "otherwise the 'Export'/'Import' operation is due. The appropriate operation will be launched automatically "
                        Message1 &= "and the UDD Wizard will be Aborted." & vbCrLf & vbCrLf
                    End If

                    Message1 &= "Click [Next] to continue."
                    Me.lblComment.Text = Message1
                Else
                    MsgBox("FHDB keys could not be found in the ATS.INI file.")
                    StepNumber = 5
                End If
                '************************************ End *****************************************

            Case 5
                'Finish
                If bFileError = True Then iCopySuccess = False 'Modified DJoiner 05/24/01
                If bSuccess = True Then
                    iCopySuccess = True
                End If
                If iCopySuccess = True Then 'Modified DJoiner 05/18/01
                    Me.lblTitle.Text = "Unique Data Archived"
                    If usb_flag = True Then
                        Message1 = "The Unique Data Disk Archive is complete.  Remove USB stick from USB port." & vbCrLf & vbCrLf
                        Message1 &= "In the event of an Instrument Controller or Hard Disk Drive failure, "
                        Message1 &= "any user logged in may restore unique data from the "
                        Message1 &= "Unique Data Disk to the Instrument Controller. "
                        Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                        Message1 &= "Click [Finish] to continue."
                    ElseIf uddHDflag = True Then
                        Message1 = "The Unique Data Disk Archive is complete." & vbCrLf & vbCrLf
                        Message1 &= "In the event of an Instrument Controller or Hard Disk Drive failure, "
                        Message1 &= "any user logged in may restore unique data from the "
                        Message1 &= "Unique Data Disk to the Instrument Controller. "
                        Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                        Message1 &= "Click [Finish] to continue."
                    End If
                    Me.lblComment.Text = Message1
                Else
                    Me.lblTitle.Text = "Unique Data Archive Error"
                    Message1 = "The Unique Data Disk Archive is incomplete due to missing or corrupted files.  Remove USB stick from USB port." & vbCrLf & vbCrLf
                    Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                    Message1 &= "Click [Finish] to continue."
                    Me.lblComment.Text = Message1

                End If
                cmdBack.Enabled = False
                cmdNext.Enabled = False
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Finish"
                lblSerNum.Visible = False
                TextBox_1.Visible = False
                cmdCancel.Focus()

            Case Else
                Close() 'Error Trap
        End Select

    End Sub


    Sub RecoverWizzardStep(ByVal ButtonClicked As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This code updates text and objects to Recover a VIPERT *
        '*     with Unique Data Disk (UDD) data                     *
        '*    PARAMETERS:                                           *
        '*     ButtonClicked = Button Constant Set in a Button      *
        '*     Click Event                                          *
        '*    EXAMPLE:                                              *
        '*      RecoverWizzardStep NEXT_BUTTON                      *
        '************************************************************
        Dim Message1 As String
        
        Dim iResponse As DialogResult

        bSuccess = False 'DJoiner 05/25/01
        'Adjust GUI for Back/Next Selection
        Select Case ButtonClicked
            Case BACK_CLICK
                StepNumber -= 1
            Case NEXT_CLICK
                StepNumber += 1
            Case CANCEL_CLICK
                Close()
                Exit Sub
            Case NO_CLICK
                'No Action Required

            Case Else
                Close()
                Exit Sub
        End Select
        'Force Index Limits
        If StepNumber = 7 Then StepNumber = 6 'Modified DJoiner 05/21/2001
        If StepNumber = 0 Then StepNumber = 1

        'Process Control Logic
        If (StepNumber = 2) And (ButtonClicked = NEXT_CLICK) Then
            Do 'Check For Disk
                If Not (System.IO.File.Exists(sFDD & "\Users\Public\Documents\ATS\ATS.INI")) Then
                    If usb_flag = True Then
                        iResponse = MsgBox("Insert USB stick into a USB port.", MsgBoxStyle.RetryCancel + MsgBoxStyle.Critical, "UDD Archive Error")
                    ElseIf uddHDflag = True Then
                        iResponse = MsgBox("The selected source folder is not a valid UDD directory.  Please try again.", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "UDD Archive Error")

                    End If
                    If iResponse = DialogResult.Cancel Or iResponse = DialogResult.OK Then
                        iCopySuccess = False
                        StepNumber = 5 'Modified DJoiner 05/21/2001
                        GoTo jumphere
                    End If
                Else
                    Exit Do
                End If
            Loop
            If SetCur(VIPERT_SN) = "" Then
                MsgBox("Configuration File Error: The ATS Configuration File is missing or corrupted.", MsgBoxStyle.Critical, "UDD Archive Error")
                iCopySuccess = False
                StepNumber = 5 'Modified DJoiner 05/21/2001
                GoTo jumphere
            End If
            SetCur(VIPERT_SN) = GetSerialNumber(False)
            TextBox_1.Text = VB6.Format(SetCur(VIPERT_SN), SetRes(VIPERT_SN))
        End If

        '**************************** Added to support FHDB ********************************
        If StepNumber = 5 Then
            cmdBack.Enabled = False
            cmdNext.Enabled = False
            cmdCancel.Enabled = False

            If InitRefFiles() = True Then 'If File Initialization Fails, GOTO Finish
                bZipIt = False 'Reset Zip Indicator Flag
                CopyRestore() 'Call the Copy/Restore Procedure

                proProgress.Visible = False
                cmdCancel.Enabled = True
            Else
                MsgBox("FHDB keys could not be found in the ATS.INI file.")
            End If

        End If
        '************************************ End *****************************************

        If StepNumber = 4 Then 'Modified DJoiner 05/21/2001
            proProgress.Visible = True
            cmdBack.Enabled = False
            cmdNext.Enabled = False
            cmdCancel.Enabled = False
            iCopySuccess = RestoreUddFiles()
            proProgress.Visible = False
            cmdCancel.Enabled = True
            If iCopySuccess = False Then 'Added DJoiner 05/25/2001
                StepNumber = 6
            End If

            'DR#260,  DJoiner 03/11/02
            'Check the UDD Disk for a Database file, either FHDB.mbd or FHDB_Database.exe
            If (System.IO.File.Exists(sFDD_DB)) Or (System.IO.File.Exists(sFileSelfX)) Then
                StepNumber = 4
            Else                'If not, Finish
                StepNumber = 6
            End If
        End If

        If StepNumber = 5 Then
            cmdNext.Enabled = True
            cmdNext.Focus()
            Exit Sub
        End If

jumphere:

        Select Case StepNumber
            Case 1
                'Startup message
                Me.lblTitle.Text = "Recovery Information"
                If usb_flag = True Then
                    Message1 = "This wizard updates the ATS Hard Disk Drive from the Unique Data Disk (UDD) "
                    Message1 &= "in the event of an Instrument Controller or Hard Disk Drive failure. " & vbCrLf & vbCrLf
                    Message1 &= "Caution: This procedure will REPLACE any system specific data on the Hard Disk Drive.  " & vbCrLf & vbCrLf
                    Message1 &= "The system has detected your USB stick. Disk Media should be labeled:" & vbCrLf
                    Message1 &= "          ""ATS Unique Data Disk""" & vbCrLf
                    Message1 &= vbCrLf
                    Message1 &= "Click [Next>] to continue."
                ElseIf uddHDflag = True Then
                    Message1 = "This wizard updates the ATS Hard Disk Drive from the Unique Data Disk (UDD) "
                    Message1 &= "in the event of an Instrument Controller or Hard Disk Drive failure. " & vbCrLf & vbCrLf
                    Message1 &= "Caution: This procedure will REPLACE any system specific data on the Hard Disk Drive.  " & vbCrLf & vbCrLf
                    Message1 &= "The system will restore the UDD from the following source folder:" & vbCrLf
                    Message1 &= "          """ & sFDD & """" & vbCrLf
                    Message1 &= vbCrLf
                    Message1 &= "Click [Next>] to continue."
                End If
                Me.lblComment.Text = Message1
                cmdBack.Enabled = False
                cmdNext.Enabled = True
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Cancel"
                lblSerNum.Visible = False
                TextBox_1.Visible = False
            Case 2
                'Verify VIPERT Serial Number
                Me.lblTitle.Text = "Verify ATS Serial Number"
                Message1 = "Verify that the serial number of the ATS is: " & SetCur(VIPERT_SN) & vbCrLf & vbCrLf
                Message1 &= "The Serial Number of the ATS is located on a black system identification plate.  "
                Message1 &= "The identification plate is located between the handles of each transit case.  "
                Message1 &= "The ATS System Serial Number is stamped in the field marked ""SERIAL NUMBER:"" " & vbCrLf & vbCrLf
                Message1 &= "If the Serial Numbers do not match, click [Cancel] and DO NOT PROCEED WITH RECOVERY!"
                Message1 &= vbCrLf & vbCrLf
                Message1 &= "Click [Next>] to continue."
                Me.lblComment.Text = Message1
                cmdBack.Enabled = True
                cmdNext.Enabled = True
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Cancel"
                lblSerNum.Visible = False
                TextBox_1.Visible = False
            Case 3
                'Recover Files
                Me.lblTitle.Text = "Recover Files"
                Message1 = "Caution: This procedure will REPLACE any system specific data on the Hard Disk Drive.  " & vbCrLf & vbCrLf
                Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                Message1 &= "Click [Next>] to continue."
                Me.lblComment.Text = Message1
                cmdBack.Enabled = True
                cmdNext.Enabled = True
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Cancel"
                lblSerNum.Visible = False
                TextBox_1.Visible = False

                '**************************** Added to support FHDB ********************************
            Case 4
                'Recover Database
                Me.lblTitle.Text = "Recover FHDB Database"
                Message1 = "The FHDB Recovery process will:" & vbCrLf & vbCrLf
                Message1 &= "1)   Check the Database file type on the UDD. If the file is in a Zipped format it will be extracted." & vbCrLf
                Message1 &= "2)   The last record number in both Database files will be evaluated and compared. If the System returns a greater number, then the UDD Database will NOT be restored. If the same number is returned from both files, the Date Last Modified will be evaluated to determine the latest file."
                Message1 &= vbCrLf & vbCrLf
                Message1 &= "Click [Next>] to continue."
                Me.lblComment.Text = Message1
                cmdBack.Enabled = True
                cmdNext.Enabled = True
                cmdNext.Focus()
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Cancel"
                lblSerNum.Visible = False
                TextBox_1.Visible = False
                '************************************ End *****************************************

            Case 6
                'Finish
                If bSuccess = True Then 'Modified DJoiner 05/21/2001
                    iCopySuccess = True
                End If
                If iCopySuccess = True Then
                    Me.lblTitle.Text = "Recovery Complete"
                    Message1 = "The Unique Data Recovery is complete." & vbCrLf & vbCrLf
                    Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                    Message1 &= "Click [Finish] to continue."
                    Me.lblComment.Text = Message1
                Else
                    Me.lblTitle.Text = "Recovery Error"
                    Message1 = "The Unique Data Recovery is incomplete due to missing or corrupted files." & vbCrLf & vbCrLf
                    Message1 &= vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                    Message1 &= "Click [Finish] to continue."
                    Me.lblComment.Text = Message1
                End If
                cmdBack.Enabled = False
                cmdNext.Enabled = False
                cmdCancel.Enabled = True
                cmdCancel.Text = "&Finish"
                lblSerNum.Visible = False
                TextBox_1.Visible = False
                cmdCancel.Focus()

            Case Else
                Close() 'Error Trap
        End Select

    End Sub



    
    Function CopyUddFiles() As Short
        '---Added/Modified for Version 1.7 DWH---
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Copies Files Designated in VIPERT.INI to Unique Data   *
        '*     disk                                                 *
        '*    PARAMETERS:                                           *
        '*     NONE                                                 *
        '*    EXAMPLE:                                              *
        '*      Success%=CopyUddFiles                               *
        '*    RETURNS:                                              *
        '*      TRUE if copied without errors, FLASE if not         *
        '************************************************************
        Dim lpApplicationName As String '[HEADING] in INI File
        
        Dim lpReturnedString As New StringBuilder(Space(255), 255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpKeyName As String
        Dim lpDefault As String
        Dim FileKeyNumber As Single
        Dim NumberOfFiles As Single
        
        Dim iResponse As DialogResult
        Dim DriveType As Integer
        Dim RetVal As Integer, RetFromMsg As Short
        Dim bCopyAll As Boolean
        Dim sFile As String
        Dim sSourceFileNameInfo As String
        Dim sDestFileNameInfo As String
        Dim iParseindex As Short

        CopyUddFiles = True 'Until Proven False
        'Clear String Buffer
        lpReturnedString = New StringBuilder(Space(255))
        'Find Windows Directory
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        lpApplicationName = "UDD Files"
        lpDefault = SetCur(VIPERT_SN)

        ReturnValue = WritePrivateProfileString("System Startup", "SN", lpDefault, lpFileName)
        lpDefault = ""

        'Count the number of files
        NumberOfFiles = 0
        Do
            NumberOfFiles += 1
            lpKeyName = "FILE_SOUR" & NumberOfFiles
            ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, "", lpReturnedString, nSize, lpFileName)
            FileNameInfo = Trim(lpReturnedString.ToString()) 'Transfer to VB String
            If ReturnValue = 0 Then Exit Do 'No more Files To Copy (Exit)
            lpReturnedString = New StringBuilder(Space(255)) 'Clear Buffer
        Loop
        NumberOfFiles -= 1 'Adjust
        If NumberOfFiles = 0 Then 'Error Trap (NO INI File Entry)
            MsgBox("Configuration File Error: The ATS Configuration File is missing or corrupted.", MsgBoxStyle.Critical, "UDD Archive Error")
            CopyUddFiles = False
            Exit Function
        End If

        'Format USB Disk
        If IsElevated(System.Environment.UserName) And uddHDflag = False Then
            On Error Resume Next
            RetVal = SetVolumeLabel(sFDD, "ATS_UDD")
            DriveType = GetDriveType(sFDD)
            If DriveType = DRIVE_REMOVABLE Then
                RetVal = SHFormatDrive(Me.Handle.ToInt32, nDrive, &H0, 1)
            Else                'Error Tap
                MsgBox("File Format Error: Drive """ & sFDD & """ is not a removable drive.", MsgBoxStyle.Critical, "UDD Archive Error")
                CopyUddFiles = False
                Exit Function
            End If
        End If

        'Find File Locations
        FileKeyNumber = 0
        Do
            'Get Source File
            FileKeyNumber += 1
            If FileKeyNumber > NumberOfFiles Then
                Exit Do
            End If
            lpKeyName = "FILE_SOUR" & FileKeyNumber
            ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
            sSourceFileNameInfo = Trim(lpReturnedString.ToString()) 'Transfer to VB String
            sSourceFileNameInfo = Mid(sSourceFileNameInfo, 1, Len(sSourceFileNameInfo)) 'Strip Null

            If sSourceFileNameInfo = "" Then Exit Do 'No more Files To Copy (Exit)
            lpReturnedString = New StringBuilder(Space(255)) 'Clear Buffer
            If Strings.Right(sSourceFileNameInfo, 1) <> "\" Then 'JHill V1.10 per DR #167
                If Not (System.IO.File.Exists(sSourceFileNameInfo)) Then
                    MsgBox("File Not Found Error: " & sSourceFileNameInfo, MsgBoxStyle.Critical, "UDD Archive Error")
                    CopyUddFiles = False
                    Exit Function
                End If
            End If

            'Get Destination File
            lpKeyName = "FILE_DEST" & FileKeyNumber
            ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
            sDestFileNameInfo = Trim(lpReturnedString.ToString()) 'Transfer to VB String
            sDestFileNameInfo = Mid(sDestFileNameInfo, 1, Len(sDestFileNameInfo)) 'Strip Null
            lpReturnedString = New StringBuilder(Space(255)) 'Clear Buffer

            'Check for different USB drive letters
            If sFDD <> Strings.Left(sDestFileNameInfo, 3) Then
                If Mid(sDestFileNameInfo, 2, 2) = ":\" Then 'Make sure the 1st chars are a drive spec
                    sDestFileNameInfo = sFDD & Mid(sDestFileNameInfo, 4)
                Else                    'else, add drive spec
                    sDestFileNameInfo = sFDD & sDestFileNameInfo
                End If
            End If

            'Copy Unique Data File
            Do
                On Error Resume Next
                Err.Number = 0 'Reset Error Level
                iParseindex = 0 'Reset Index
                bCopyAll = False
                Do 'Create Directorys?
                    iParseindex = InStr(iParseindex + 1, sDestFileNameInfo, "\", CompareMethod.Binary)
                    If iParseindex > 3 Then 'If not root level
                        Dir(Mid(sDestFileNameInfo, 1, iParseindex))
                    End If
                    If iParseindex <> 0 Then
                        Directory.CreateDirectory(Path.GetDirectoryName(sDestFileNameInfo))
                    Else
                        Exit Do
                    End If
                     If sDestFileNameInfo(sDestFileNameInfo.Length - 1) = "\" Then
                        bCopyAll = True
                        Exit Do
                    End If
                Loop

                Err.Number = 0 'Reset Error Level
                If bCopyAll Then
                    sFile = Dir(sSourceFileNameInfo)
                    Debug.Print("The sFile Value is: " & sFile) 'For Debugging DJoiner 05/22/2001
                    While sFile <> ""
                        File.Copy(sSourceFileNameInfo & sFile, sDestFileNameInfo & sFile, True)
                        sFile = Dir()
                    End While
                Else
                    FileCopy(sSourceFileNameInfo, sDestFileNameInfo)
                End If

                Debug.Print(Str(Err))
                Debug.Print(sSourceFileNameInfo)

                Me.proProgress.Value = (FileKeyNumber / NumberOfFiles) * 100
                Me.Refresh()
            Loop Until Err.Number <> 0 ''Until FileKeyNumber > NumberOfFiles 'CompareErrNumber("=", 0)
        Loop


    End Function

    Private Sub RestoreHardwareUniqueKeys(ByVal sSrcFile As String, ByRef sDestFile As String)
        'Added by JHill V1.9 per DR #108

        RestoreSection("System Monitor", sSrcFile, sDestFile, sExclude:="CHASSIS_STATE")
        RestoreSection("Calibration", sSrcFile, sDestFile, "")
        RestoreSection("Serial Number", sSrcFile, sDestFile, "")
        RestoreSection("Self Test", sSrcFile, sDestFile, "")
        RestoreSection("FHDB", sSrcFile, sDestFile, sExclude:="RUN_TIME")

    End Sub


    Private Sub RestoreSection(ByRef sSection As String, ByVal sSrcFile As String, ByRef sDestFile As String, ByVal sExclude As String)
        'Added by JHill V1.9 per DR #108

        Dim lpReturnedString As New StringBuilder(Space(1024), 1024)
        Dim nNumChars As Integer
        Dim i As Short
        Dim sList() As String
        Dim sKeyVal As String

        nNumChars = GetPrivateProfileString(sSection, vbNullString, "", lpReturnedString, Len(lpReturnedString), sSrcFile)
        If nNumChars = 0 Then Exit Sub

        For i = 1 To StrToDynList(Strings.Left(lpReturnedString.ToString(), nNumChars - 1), 1, sList, vbNullChar)
            nNumChars = GetPrivateProfileString(sSection, sList(i), "", lpReturnedString, Len(lpReturnedString), sSrcFile)
            If nNumChars <> 0 Then
                sKeyVal = Strings.Left(lpReturnedString.ToString(), nNumChars)
                If sList(i) <> Convert.ToString(sExclude) Then
                    WritePrivateProfileString(sSection, sList(i), sKeyVal, sDestFile)
                End If
            End If

        Next i
    End Sub


    Public Function StrToDynList(ByVal sStr As String, ByVal iLower As Short, ByRef List() As String, ByVal sDelimiter As String) As Short
        'Added by JHill V1.9 per DR #108
        'DESCRIPTION:
        '   Procedure to convert a delimited string into a dynamic string array
        '   ReDims the array from iLower to the number of elements in string
        'Parameters:
        '   sStr       : String to be parsed.
        '   iLower     : Lower bound of target array
        '   List$()    : Dynamic array in which to return list of strings
        '   sDelimiter : Delimiter string.
        'Returns:
        '   Number of items in string
        '   or 0 if string is empty

        
        Dim numels As Short, i As Integer
        
        Dim iDelimiterLength As Integer

        iDelimiterLength = Len(sDelimiter)
        If sStr = "" Then
            StrToDynList = 0
            Exit Function
        End If

        numels = 1
        ReDim List(iLower)
        'Go through parsed string a character at a time.
        For i = 1 To Len(sStr)
            'Test for delimiter
            If Mid(sStr, i, iDelimiterLength) <> sDelimiter Then
                'Add the character to the current argument.
                List(iLower + numels - 1) &= Mid(sStr, i, 1)
            Else
                'Found a delimiter.
                ReDim Preserve List(iLower + numels)
                numels += 1
                i += iDelimiterLength - 1
            End If
        Next i
        StrToDynList = numels

    End Function


    
    Function RestoreUddFiles() As Short
        '---Added/Modified for Version 1.7 DWH---
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Restores Files Designated in VIPERT.INI to Unique Data *
        '*     disk                                                 *
        '*    PARAMETERS:                                           *
        '*     NONE                                                 *
        '*    EXAMPLE:                                              *
        '*      Success%=RestoreUddFiles                            *
        '*    RETURNS:                                              *
        '*      TRUE if restored without errors, FLASE if not       *
        '************************************************************
        Dim lpApplicationName As String '[HEADING] in INI File
        
        Dim lpReturnedString As New StringBuilder(Space(255), 255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File
        Dim lpKeyName As String
        Dim lpDefault As String
        Dim FileKeyNumber As Single
        Dim NumberOfFiles As Single
        
        Dim iResponse As DialogResult
        Dim bCopyAll As Boolean
        Dim sFile As String
        Dim iParseindex As Short
        Dim sDestFileNameInfo As String
        Dim sSourceFileNameInfo As String
        Dim swVersionNumber As StringBuilder = New StringBuilder(256)
 
        RestoreUddFiles = True 'Until Proven False
        'Clear String Buffer
        lpReturnedString = New StringBuilder(Space(255))

        lpFileName = sFDD & "Users\Public\Documents\ATS\ATS.INI"
        nSize = 255
        lpApplicationName = "UDD Files"
        lpDefault = ""

        'Grab the SW version number from the current system ats.ini file
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        GetPrivateProfileString("System Startup", "SWR", vbNullString, swVersionNumber, nSize, lpFileName)

        'Count the number of files
        NumberOfFiles = 0
        Do
            NumberOfFiles += 1
            lpKeyName = "FILE_SOUR" & NumberOfFiles
            ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
            FileNameInfo = Trim(lpReturnedString.ToString()) 'Transfer to VB String
            If FileNameInfo = "" Or FileNameInfo = Convert.ToString(Chr(0)) Then Exit Do 'No more Files To Copy (Exit)
            lpReturnedString = New StringBuilder(Space(255)) 'Clear Buffer
        Loop
        NumberOfFiles -= 1 'Adjust
        If NumberOfFiles = 0 Then 'Error Trap (NO INI File Entry)
            MsgBox("Configuration File Error: The ATS Configuration File is missing or corrupted.", MsgBoxStyle.Critical, "UDD Archive Error")
            RestoreUddFiles = False
            Exit Function
        End If

        'Find File Locations
        FileKeyNumber = 0
        Do
            'Get Source File
            FileKeyNumber += 1
            lpKeyName = "FILE_DEST" & FileKeyNumber
            ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
            sSourceFileNameInfo = Trim(lpReturnedString.ToString()) 'Transfer to VB String
            sSourceFileNameInfo = Mid(sSourceFileNameInfo, 1, Len(sSourceFileNameInfo)) 'Strip Null
            If sSourceFileNameInfo = "" Then Exit Do 'No more Files To Copy (Exit)
            lpReturnedString = New StringBuilder(Space(255)) 'Clear Buffer

            'Check for different USB drive letters
            If sFDD <> Strings.Left(sSourceFileNameInfo, 3) Then
                If Mid(sSourceFileNameInfo, 2, 2) = ":\" Then 'Make sure the 1st chars are a drive spec
                    sSourceFileNameInfo = sFDD & Mid(sSourceFileNameInfo, 4)
                Else                    'else, add drive spec
                    sSourceFileNameInfo = sFDD & sSourceFileNameInfo
                End If
            End If

            If Strings.Right(sSourceFileNameInfo, 1) <> "\" Then 'JHill V1.10 per DR #167
                If Not (System.IO.File.Exists(sSourceFileNameInfo)) Then
                    MsgBox("File Not Found Error: " & sSourceFileNameInfo, MsgBoxStyle.Critical, "UDD Archive Error")
                    RestoreUddFiles = False
                    Exit Function
                End If
            End If

            'Get Destination File
            lpKeyName = "FILE_SOUR" & FileKeyNumber
            ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
            sDestFileNameInfo = Trim(lpReturnedString.ToString()) 'Transfer to VB String
            sDestFileNameInfo = Mid(sDestFileNameInfo, 1, Len(sDestFileNameInfo)) 'Strip Null
            lpReturnedString = New StringBuilder(Space(255)) 'Clear Buffer

            'Copy Unique Data Files
            Do
                On Error Resume Next
                Err.Number = 0 'Reset Error Level
                iParseindex = 0 'Reset Index
                bCopyAll = False
                Application.DoEvents()
                Do 'Create Directorys?
                    iParseindex = InStr(iParseindex + 1, sDestFileNameInfo, "\", CompareMethod.Binary)
                    If iParseindex > 3 Then 'If not root level
                        Dir(Mid(sDestFileNameInfo, 1, iParseindex))
                    End If
                    If iParseindex <> 0 Then
                        ChDir(Mid(sDestFileNameInfo, 1, iParseindex))
                    Else
                        Exit Do
                    End If
                    If iParseindex = Len(sDestFileNameInfo) Then 'If last char is "\"
                        bCopyAll = True
                        Exit Do
                    End If
                Loop

                Err.Number = 0 'Reset Error Level
                'JHill V1.9 per DR #108 begin ------------
                If InStr(UCase(sSourceFileNameInfo), "ATS.ini") Then
                    RestoreHardwareUniqueKeys(sSourceFileNameInfo, sDestFileNameInfo)
                Else
                    'JHill V1.10 per DR #167 begin ------------
                    If bCopyAll Then
                        sFile = Dir(sSourceFileNameInfo)
                        While sFile <> ""
                            FileCopy(sSourceFileNameInfo & sFile, sDestFileNameInfo & sFile)
                            sFile = Dir()
                        End While
                    Else
                        FileCopy(sSourceFileNameInfo, sDestFileNameInfo)
                    End If
                    'JHill V1.10 per DR #167 end ------------
                End If
                'JHill V1.9 per DR #108 end ------------
                If CompareErrNumber("<>", 0) Then
                    iResponse = MsgBox(ErrorToString(), MsgBoxStyle.RetryCancel + MsgBoxStyle.Critical, "UDD Archive Error")
                    If iResponse = DialogResult.Cancel Then
                        RestoreUddFiles = False
                        Exit Function
                    End If
                End If
                Me.proProgress.Value = (FileKeyNumber / NumberOfFiles) * 100
            Loop Until CompareErrNumber("=", 0)
        Loop

        'When done, set station swr number back to what it was originally
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        WritePrivateProfileString("System Startup", "SWR", swVersionNumber.ToString, lpFileName)


        '---End of Modification Version 1.7 DWH---
    End Function

    Sub DispTxt(ByVal SetIndex As Short)

        TextBox(SetIndex).Text = VB6.Format(Val(SetCur(SetIndex)) / Val(SetUOM(SetIndex)), SetRes(SetIndex))

    End Sub


    Private Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click

        If UpdateFlag = True Then
            AdjustWizzardStep(BACK_CLICK)
        Else
            RecoverWizzardStep(BACK_CLICK)
        End If

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        If UpdateFlag = True Then
            AdjustWizzardStep(CANCEL_CLICK)
        Else
            RecoverWizzardStep(CANCEL_CLICK)
        End If

        frmSysPanl.WindowState = FormWindowState.Normal

    End Sub


    Private Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click

        If UpdateFlag = True Then
            AdjustWizzardStep(NEXT_CLICK)
        Else
            RecoverWizzardStep(NEXT_CLICK)
        End If

    End Sub


    Private Sub frmUdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        StepNumber = 1 'Initialize Step Number
        TextBox_1.Text = VB6.Format(SetCur(VIPERT_SN), SetRes(VIPERT_SN))

        If UpdateFlag = True Then
            AdjustWizzardStep(NO_CLICK)
        Else
            RecoverWizzardStep(NO_CLICK)
        End If

    End Sub


    Private Sub SpinButton_SpinDown(ByVal Index As Short)

        Dim OldVal As Double
        Dim NewVal As Double

        OldVal = Val(SetCur(Index))
        NewVal = Val(SetCur(Index)) - Val(SetInc(Index))
        If NewVal >= Val(SetMin(Index)) Then
            SetCur(Index) = Str(NewVal)
        Else
            SetCur(Index) = SetMin(Index)
        End If
        DispTxt(Index)
        Application.DoEvents()

    End Sub

    Private Sub SpinButton_SpinUp(ByVal Index As Short)

        Dim OldVal As Double
        Dim NewVal As Double

        OldVal = Val(SetCur(Index))
        NewVal = Val(SetCur(Index)) + Val(SetInc(Index))
        If NewVal <= Val(SetMax(Index)) Then
            SetCur(Index) = Str(NewVal)
        Else
            SetCur(Index) = SetMax(Index)
        End If
        DispTxt(Index)
        Application.DoEvents()

    End Sub


    Private Sub TextBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox.Click
        Dim Index As Short = TextBox.GetIndex(sender)
        TextBox_Click(Index)
    End Sub
    Public Sub TextBox_Click(ByVal Index As Integer)

        TextBox(Index).SelectionStart = 0
        TextBox(Index).SelectionLength = Len(TextBox(Index).Text)

    End Sub


    Private Sub TextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox.Enter
        Dim Index As Short = TextBox.GetIndex(sender)

        iRangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))

    End Sub


    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox.KeyPress
        Dim Index As Short = TextBox.GetIndex(sender)
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub TextBox_KeyPress(ByVal Index As Integer, ByRef KeyAscii As Short)

        If KeyAscii = 13 Or KeyAscii = 9 Then
            KeyAscii = 0
            cmdNext.Focus()
        ElseIf KeyAscii = 27 Then
            KeyAscii = 0
            TextBox(Index).Text = Str(Val(SetCur(Index)) / Val(SetUOM(Index)))
            cmdNext.Focus()
        End If

    End Sub


    Private Sub TextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox.Leave
        Dim Index As Short = TextBox.GetIndex(sender)

        Dim First4 As String
        Dim NewVal As Double

        First4 = UCase(Strings.Left(TextBox(Index).Text, 4))
        Select Case First4
            Case "MIN"
                NewVal = Val(SetMin(Index))
            Case "MAX"
                NewVal = Val(SetMax(Index))
            Case "DEF"
                NewVal = Val(SetDef(Index))

            Case Else
                If Not IsNumeric(TextBox(Index).Text) Then
                    MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
                    TextBox(Index).Focus()
                    TextBox_Click(Index)
                    Exit Sub
                End If
                NewVal = Val(TextBox(Index).Text) * Val(SetUOM(Index))
        End Select

        If NewVal < Val(SetMin(Index)) Then
            MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
            TextBox(Index).Focus()
            TextBox_Click(Index)
        ElseIf NewVal > Val(SetMax(Index)) Then
            MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
            TextBox(Index).Focus()
            TextBox_Click(Index)
        Else
            SetCur(Index) = Str(NewVal)
            DispTxt(Index)
            iRangeDisplay = False
        End If

    End Sub


    '**************************************************************************
    '***                        Added for FHDB Support                      ***
    '***              ----- Procedures to handle ZIP Events ----            ***
    '**************************************************************************

    Public Sub Zip_It()
        '--------------------------------------------------------------------------
        '---   Main properties are first reset to the desired default values,   ---
        '---   then the values of the form's controls are copied to xZip's      ---
        '---   properties and the Zip method is called.                         ---
        '---   If a value is encountered in the SfxBinaryModule property,       ---
        '---   XceedZip will create a self-extracting zip file with an          ---
        '---   integrated self-extracting zip file module specified by          ---
        '---   the SfxBinaryModule property.                                    ---
        '--------------------------------------------------------------------------

        Dim xErr As Integer

        Try
            Using zipToOpen As FileStream = New FileStream(sFileSelfX, FileMode.CreateNew)
                Using archive As ZipArchive = New ZipArchive(zipToOpen, ZipArchiveMode.Create)
                    archive.CreateEntryFromFile(sDBFile, Path.GetFileName(sDBFile))
                End Using
            End Using
            bSuccess = True
        Catch ex As Exception
            bSuccess = False
        End Try

    End Sub


    Sub RestoreZippedDB()
        '--------------------------------------------------------------------------------
        '   Restore the zipped copy of the FHDB Database to the VIPERT System
        '   Destination: "C:\Program Files\VIPERT\FHDB\FHDB.mdb"
        '   Before Restoring, the Zipped file will be checked for file integrity
        '   Unzip Database and Install in correct Directory, Overwrite if file exists
        '--------------------------------------------------------------------------------

        Dim xErr As Integer

        bSuccess = True

        nLastSystemRecord = CheckLastRecord(sDBFile)

        'Rename the original System Database file to a temp name
        If System.IO.File.Exists(sDBTempFileName) Then Kill(sDBTempFileName)
        Rename(sDBFile, sDBTempFileName)

        AddMessage(vbCrLf & "Before Installing the FHDB Database on the System" & vbCrLf & "The Zipped file will be checked for file integrity.")
        Delay(2)
        Me.proProgress.Visible = True

        Me.proProgress.Visible = True
        Me.proProgress.Value = 0
        Dim fileToDecompress As New FileInfo(sFileSelfX)

        Me.lblComment.Text = ""
        Me.lblTitle.Text = "Extracting Database"
        AddMessage("Extracting Zipped File...........")

        Using originalFileStream As FileStream = fileToDecompress.OpenRead()
            Dim currentFileName As String = fileToDecompress.FullName
            Dim newFileName = Path.GetFileName(currentFileName) 'currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length)
            Using decompressedFileStream As FileStream = File.Create(sDBDir & "\" & newFileName)
                Using decompressionStream As GZipStream = New GZipStream(originalFileStream, CompressionMode.Decompress)
                    originalFileStream.CopyTo(decompressedFileStream)
                End Using
            End Using
            Using archive As ZipArchive = ZipFile.OpenRead(sDBDir & "\" & newFileName)
                archive.ExtractToDirectory(sDBDir)
            End Using
            Kill(sDBDir & "\" & newFileName)
        End Using

        bSuccess = False
        nLastRestoredRecord = CheckLastRecord(sDBFile)
        Me.lblComment.Text = ""
        Me.lblTitle.Text = "Comparing Database Files"
        AddMessage("The Last Record Number on the System is : " & nLastSystemRecord)
        AddMessage("The Last Record Number Restored is : " & nLastRestoredRecord)
        Delay(2)
    End Sub


    Public Sub TestFileIntegrity()
        '------------------------------------------------------------------------------
        '---            Test the File intergrity of the Zipped File                 ---
        '------------------------------------------------------------------------------

        Dim ResultCode As Integer

        Me.lblComment.Text = ""
        Me.lblTitle.Text = "Testing Zipped File"
        AddMessage("Testing File Integrity...............")

        ' Check the return value
        If ResultCode <> 0 Then
            MsgBox("The file is corrupt and can not be restored." & vbCrLf & "This Operation will now be Aborted.", MsgBoxStyle.Critical, "File Corrupt, Operation Aborted")
        Else
            AddMessage(vbCrLf & vbCrLf & "The tested zip file is completely error-free.")
            Delay(2)
        End If

    End Sub


    Public Sub TestZip_It()
        '------------------------------------------------------------------------------
        '---        Zip FHDB Database File to a Temp File for comparison            ---
        '------------------------------------------------------------------------------

        Try
            Using zipToOpen As FileStream = New FileStream(sTestItFile, FileMode.CreateNew)
                Using archive As ZipArchive = New ZipArchive(zipToOpen, ZipArchiveMode.Create)
                    archive.CreateEntryFromFile(sDBFile, Path.GetFileName(sDBFile))
                End Using
            End Using
            bSuccess = True
        Catch ex As Exception
            bSuccess = False
        End Try

    End Sub

    Private Sub frmUdd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub
End Class