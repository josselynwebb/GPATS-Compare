'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Microsoft.Win32

Public Module basLogMain


    '=========================================================
    '**************************************************************
    '**************************************************************
    '** ManTech Test Systems Software Module                     **
    '**                                                          **
    '** Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          **
    '** Version        : 1.8                                     **
    '** Written By     : David W. Hartley                        **
    '** Last Update    : 09/22/97                                **
    '** Purpose        : This module controls the functions of   **
    '**                  the VIPERT System Log Viewer              **
    '** Program Begins Executing Instructions In Sub:MAIN        **
    '**************************************************************
    '** Revision History:                                        **
    '** V1.8 ECO-565 - 05/03/2001  DJoiner                       **
    '**     1. VIPERT-DR-144                                       **
    '**        Modified the mnuFileDeletelogfile_Click() event   **
    '**        in frmSyslogViewer. Changed Buttons for MsgBox    **
    '**        from TesNoCancel to YesNo, Made Critical,         **
    '**        and [No]is now the Default Button.                **
    '**     2. Modified Sub AddFileToLog() in LogMain.bas.       **
    '**        Reformat message in msgBox, make [No] button      **
    '**        Default.                                          **
    '**     3. Modified syntex in Sub UpdateLogViewer() at       **
    '**        RestoreToolBar Event.                             **
    '**     4. Converted from VB 4.0 to VB 6.0                   **
    '** Self initiated --                                        **
    '**       Modified the About Form to get Version Information **
    '**       from the Project Properties.                       **
    '** Self initiated --                                        **
    '**       Rename Project from Project1 to SysLog.            **
    '**************************************************************

    'User Defined Types

    Structure SYSTEMTIME
        Dim wYear As Short
        Dim wMonth As Short
        Dim wDayOfWeek As Short
        Dim wDay As Short
        Dim wHour As Short
        Dim wMinute As Short
        Dim wSecond As Short
        Dim wMilliseconds As Short
    End Structure

    Structure TIME_ZONE_INFORMATION
        Dim Bias As Integer
        Dim StandardName() As Short
        Dim StandardDate As SYSTEMTIME
        Dim StandardBias As Integer
        Dim DaylightName() As Short
        Dim DaylightDate As SYSTEMTIME
        Dim DaylightBias As Integer

        Public Sub New(ByVal unusedParam As Integer)
            ReDim StandardName(256)
            ReDim DaylightName(256)
        End Sub
    End Structure
    'API Declarations
    Declare Sub GetLocalTime Lib "kernel32" (ByRef lpSystemTime As SYSTEMTIME)
    Declare Sub GetSystemTime Lib "kernel32" (ByRef lpSystemTime As SYSTEMTIME)
    Declare Function GetTimeZoneInformation Lib "kernel32" (ByRef lpTimeZoneInformation As TIME_ZONE_INFORMATION) As Integer
    'Used For Bringing An Application To the top of the Windows "Z-Order"
    Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    'Search Parameters
    Public CurrentSearch As String
    
    Public MatchCase As Short
    
    Public FindWholeWordOnly As Short
    'Core Parameters
    Public TextChar As Double
    Public FileLength As Double
    Public CmdLine As String
    
    Public FileLine As Integer
    'Command Line Update Parameters
    Public Const MAXFILESIZE As Integer = 255000000 'Increased For VIPER/T usb stick DME 04-10-07
    Public Const SCREENSIZE As Short = 25
    Public SyslogName As String
    Public TempLogFileName As String
    Public YesNoResponse As Short 'Added in version 1.7 to hold the Yes/No choice from the frmPrompt
    Const TB_H_DELTA As Short = 2055
    Const TB_W_DELTA As Short = 390
    Const LOGO_TOP_DELTA As Short = 1470
    Const MIN_WIN_H As Short = 3060
    Const MIN_WIN_W As Short = 6660 '4845
    Const MINIMIZED As Short = 1

    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer


    Sub FindString(ByVal Find As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function search the RTF Control for a string    *
        '*    PARAMETERS:                                           *
        '*     Find% = TRUE if Find First, FALSE if Find Next       *
        '*    EXAMPLE:                                              *
        '*      FindString (TRUE)                                   *
        '************************************************************
        Dim Options As Short
        Dim SearchStart As Short
        Dim SearchEnd As Short

        'If no search selected EXIT
        If CurrentSearch = "" Then Exit Sub
        frmSyslogViewer.rtbLogViewer.Focus()
        'Set Search Options
        If MatchCase Then Options += 4
        If FindWholeWordOnly Then Options += 2

        EchoToStatusBar("Searching...", 1)

        If Find Then 'First Find Condition
            TextChar = frmSyslogViewer.rtbLogViewer.Find(CurrentSearch, 0, frmSyslogViewer.rtbLogViewer.TextLength, Options)
        Else            'Find Next Condition
            TextChar += 1
            TextChar = frmSyslogViewer.rtbLogViewer.Find(CurrentSearch, TextChar, frmSyslogViewer.rtbLogViewer.TextLength, Options)
        End If

        EchoToStatusBar("Text Found", 1)

        If TextChar = -1 Then 'Search has reached End Of File
            MsgBox("The entire system log has been searched.", MsgBoxStyle.Exclamation, "System Log Viewer")
            EchoToStatusBar("Search Text Not Found", 1)
        End If
        'Reset Control
        frmSyslogViewer.rtbLogViewer.Focus()

    End Sub


    Sub HandleResize()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : M. MCCABE MODIFICATION (2/18/97)        *
        '*    DESCRIPTION:                                          *
        '*     This subroutine handles a resize of the main form    *
        '*    EXAMPLE:                                              *
        '*      HandleResize                                        *
        '************************************************************

        If frmSyslogViewer.WindowState = MINIMIZED Then
            Exit Sub
        End If
        'Check Minimum size specs
        If frmSyslogViewer.Height < MIN_WIN_H Then
            frmSyslogViewer.Height = MIN_WIN_H
        End If
        If frmSyslogViewer.Width < MIN_WIN_W Then
            frmSyslogViewer.Width = MIN_WIN_W
        End If

        'Adjust Size
        frmSyslogViewer.rtbLogViewer.Height = frmSyslogViewer.Height - TB_H_DELTA
        frmSyslogViewer.rtbLogViewer.Width = frmSyslogViewer.Width - TB_W_DELTA

    End Sub
    Public Sub Main()
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This module is the program entry (starting) point.   *
        '************************************************************

        Dim DummyVar As Double

        If AppPrevInstance And Microsoft.VisualBasic.Command() = "" Then
            'Only One Instance of the Viewer Can Be Running
            Application.Exit()
        End If
        'Read Command Line
        CmdLine = UCase(Microsoft.VisualBasic.Command())

        'Uncomment the line of code below this one for For Debugging Purposes
        'CmdLine$ = App.Path & "\Add.txt"

        If CmdLine <> "" Then
            If FileExists(CmdLine) Then
                FileCopy(CmdLine, Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "TEMPLOG.TXT")
            Else
                MsgBox("Error Opening: " & CmdLine, MsgBoxStyle.Exclamation, "VIPERT System Log")
                Application.Exit()
            End If
        End If

        'Syslog Must be in same folder (directory) as EXE File
        SyslogName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "SYSLOG.TXT"
        If CmdLine = "" Then 'If No Command Line Launch The Viewer
            UpdateLogViewer()
            'End Splash
            frmSyslogViewer.Show()
            'Application.Run()
        Else            'If Command Line Then Append/Replace Log File (Add log file in CmdLine$ to SYSLOG.LOG)
            AddFileToLog(Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "\TEMPLOG.TXT")
            Kill(Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "\TEMPLOG.TXT")
            Application.Exit()
        End If

    End Sub

    Sub AddFileToLog(ByVal newlog As String)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Adds a new file to the system log                    *
        '*    PARAMETERS:                                           *
        '*     newlog$ = new file to add to the system log          *
        '*    EXAMPLE:                                              *
        '*      AddFileToLog (CmdLine$)                             *
        '************************************************************
        
        Dim UserChoice As DialogResult
        Dim SaveFileName As String
        Dim UserText As String

        '! Load frmMess
        CenterForm(frmMess)
        frmMess.Visible = True
        frmMess.txtMessage.Text = "Updating System Log..."
        frmMess.Refresh()
        Application.DoEvents()
        '   If the files don't already exist, create them
        If (Exist(newlog) = 0) Then
            MsgBox(newlog + " does not exist", MsgBoxStyle.Exclamation, "SysLog:File Not Found")
            Application.Exit()
        End If

        If (Exist(SyslogName) = 0) Then FCreate(SyslogName)
        '   Check to see if the addition of the new file > MAXFILESIZE

        If ((FileLen(SyslogName) + (FileLen(Convert.ToString(newlog))) > MAXFILESIZE)) Then
            'This is to prevent the user from being asked more than once
            'if they want to overwrite the log
            If AppPrevInstance Then
                Application.Exit()
            End If
            '   File + newlog is larger than MAXFILESIZE so ask user what to do.
            '**********************************************************************
            '*******This Section Added for Version 1.5 VIPERT CD Release 3 DWH*******
            '**********************************************************************
AskAgain:
            UserText = "The VIPERT System Log File has exceeded the maximum file size. "
            UserText &= "Would you like to archive the current log before "
            UserText &= "it is overwitten with new data?"

            '*******This Section Added for Version 1.7 DWH*******
            'MCLB#05059901
            UserPrompt(UserText)

            If YesNoResponse = 6 Then 'User Clicked Yes
                '*******End of Modification for Version 1.7 DWH*******
                'Move Data To User Specified Disk Memory Location
                frmSyslogViewer.Hide()
                UpdateLogViewer()
                frmSyslogViewer.cdlSyslog.Filter = "Archive(*.arc;*.bak)|*.arc;*.bak|Text(*.txt;*.log)|*.txt;*.log|All Files(*.*)|*.*"
                frmSyslogViewer.cdlSyslog.DefaultExt = "*.arc"
                frmSyslogViewer.cdlSyslog.FileName = "Syslog.arc"
                '- frmSyslogViewer.cdlSyslog.CancelError = True
                On Error Resume Next
                frmSyslogViewer.cdlSyslog.ShowDialog()
                If CompareErrNumber("=", 0) Then
                    SaveFileName = frmSyslogViewer.cdlSyslog.FileName
                    CenterForm(frmMess)
                    frmMess.txtMessage.Text = "Saving System Log..."
                    frmMess.Visible = True
                    frmMess.Refresh()
                    frmSyslogViewer.rtbLogViewer.SaveFile(SaveFileName, RichTextBoxStreamType.RichText)
                    frmMess.Hide()
                    '---- Modified for VIPERT-DR-144       05/03/2001  DJoiner
                    UserText = "                      Would you like to delete the current log file?" & vbCrLf
                    UserText &= "Clicking [Yes] will delete the log.  "
                    UserText &= "Clicking [No] will overwrite the current log."
                    UserChoice = MsgBox(UserText, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "VIPERT System Log")
                    '--- End DR-144 Modifications ---
                    If UserChoice = DialogResult.Yes Then 'User Clicked Yes
                        Kill(SyslogName)
                        FCreate(SyslogName)
                        UpdateSyslog()
                    Else
                        Overwrite()
                    End If
                Else
                    Err.Number = 0
                    GoTo AskAgain
                End If
            Else                'User Clicked No
                frmMess.Hide()
                '--- Modified for VIPERT-DR-144       05/03/2001  DJoiner
                UserText = "                      Would you like to delete the current log file?" & vbCrLf
                UserText &= "Clicking [Yes] will delete the log.  "
                UserText &= "Clicking [No] will overwrite the current log."
                UserChoice = MsgBox(UserText, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "VIPERT System Log")
                '--- End DR-144 Modifications ---
                If UserChoice = DialogResult.Yes Then 'User Clicked Yes
                    Kill(SyslogName)
                    FCreate(SyslogName)
                    UpdateSyslog()
                Else
                    Overwrite()
                End If
            End If
            '**********************************************************************
            '******End of Modifications For Version 1.5 VIPERT CD Release 3 DWH******
            '**********************************************************************
        Else
            UpdateSyslog()
        End If

    End Sub

    Sub BringToTop(ByVal hWnd As Integer)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature  : System Log Viewer                        *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Routine will bring a window to the top of the   *
        '*     desktop Z-Order                                      *
        '*    PARAMETERS:                                           *
        '*     hwnd& = handle of window to move to top              *
        '*    RETURNS:                                              *
        '*     Boolean - True if Terminated and False if not        *
        '*    EXAMPLE:                                              *
        '*     Terminated% = TerminateApp32 Notepad                 *
        '************************************************************
        'Used For Bringing An Application To the top of the Windows "Z-Order"
        'Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
        Dim nFlags As Short 'API Call Register Settings
        Dim wFlags As Integer 'API Call Register Settings
        Dim RetValue As Integer 'API Call Register Settings
        Const HWND_TOPMOST As Short = -1 'API Call Register CONSTANT
        Const SWP_NOMOVE As Short = &H2 'API Call Register CONSTANT
        Const SWP_NOSIZE As Short = &H1 'API Call Register CONSTANT

        wFlags = SWP_NOMOVE Or SWP_NOSIZE
        RetValue = SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, wFlags)

    End Sub

    Sub FCreate(ByVal Filename As String)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION        *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function will create a time-stamped file        *
        '*    PARAMETERS:                                           *
        '*     Filename$ = Path$ + New File Create                  *
        '*    EXAMPLE:                                              *
        '*      FCreate "C:\MYFILE.TXT"                             *
        '************************************************************
        
        Dim Fileptr As Integer

        Try ' On Error GoTo FCreateError
            Fileptr = FreeFile()

            FileOpen(Fileptr, Convert.ToString(Filename), OpenMode.Output)
            PrintLine(Fileptr, ("New System Log File Generated: "))
            PrintLine(Fileptr, DateTime.Today.ToString)
            FileClose(Fileptr)
            Exit Sub

        Catch   ' FCreateError:
            If CompareErrNumber("=", 76) Then MsgBox("Invalid path", MsgBoxStyle.Exclamation, "SysLog:File I/O Error")

        End Try
    End Sub
    
    Function Exist(ByVal Filename As String) As Boolean
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function will check for the existence of a file *
        '*    PARAMETERS:                                           *
        '*     Filename$ = File + Path To Check for Existance       *
        '*    EXAMPLE:                                              *
        '*      Exist "C:\MYFILE.TXT"                               *
        '*    RETURNS:                                              *
        '*      If the file exists, it will return True (-1)        *
        '*      If the file does not exist, it will return False (0)*
        '************************************************************
        
        Dim Filenum As Integer
        Exist = True
        Try ' On Error GoTo Errorhandler

            Filenum = FreeFile()
            FileOpen(Filenum, Convert.ToString(Filename), OpenMode.Input)
            FileClose(Filenum)
            Exit Function

        Catch   ' Errorhandler:
            If CompareErrNumber("=", 53) Or CompareErrNumber("=", 76) Then ' Check for File Not Found error or Path Not Found error
                Exist = False
            End If
            Exit Function

        End Try
    End Function

    Sub Overwrite()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function Overwrites The Log File                *
        '*    PARAMETERS: ()                                        *
        '*    EXAMPLE:                                              *
        '*      Overwrite                                           *
        '************************************************************
        Dim newfile_size As Integer
        Dim syslog_size As Integer
        Dim delete_from_beginning_amt As Integer
        Dim SessionLogFileName As String
        
        Dim sessionlogfile As Integer
        Dim file_buffer As String = ""
        
        Dim syslogfile As Integer
        
        Dim templogfile As Integer
        
        Dim newfile As Integer

        ' Get file sizes
        newfile_size = FileLen(Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "\TEMPLOG.TXT")
        syslog_size = FileLen(SyslogName)

        ' Check to see if the session logfile is > MAXFILESIZE.  If yes, then FIFO beginning.
        If newfile_size > MAXFILESIZE Then

            delete_from_beginning_amt = newfile_size - MAXFILESIZE

            SessionLogFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "\TEMPLOG.TXT"
            sessionlogfile = FreeFile()
            FileOpen(sessionlogfile, SessionLogFileName, OpenMode.Input)

            While delete_from_beginning_amt > Seek(sessionlogfile)
                file_buffer = LineInput(sessionlogfile)
            End While

            syslogfile = FreeFile()
            FileOpen(syslogfile, SyslogName, OpenMode.Output)

            While Not EOF(sessionlogfile)
                file_buffer = LineInput(sessionlogfile)
                PrintLine(syslogfile, file_buffer)
            End While

            FileClose(sessionlogfile)
            FileClose(syslogfile)

        Else

            delete_from_beginning_amt = syslog_size + newfile_size - MAXFILESIZE

            ' Open existing SYSLOG file
            syslogfile = FreeFile()
            FileOpen(syslogfile, SyslogName, OpenMode.Input)

            ' Discard the beginning of the SYSLOG
            While delete_from_beginning_amt > Seek(syslogfile)
                file_buffer = LineInput(syslogfile)
            End While

            ' Copy remaining SYSLOG.LOG to TEMPFILE.LOG
            ' Open the temporary log file TEMPFILE.LOG
            templogfile = FreeFile()
            'Change file name from templog.txt to tempfile.log
            TempLogFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) + "\tempfile.log"
            FileOpen(templogfile, TempLogFileName, OpenMode.Output)

            While Not EOF(syslogfile)
                file_buffer = LineInput(syslogfile)
                PrintLine(templogfile, file_buffer)
            End While

            ' Now copy session log file text to TEMPFILE.LOG
            newfile = FreeFile()
            FileOpen(newfile, Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "\TEMPLOG.TXT", OpenMode.Input)

            While Not EOF(newfile)
                file_buffer = LineInput(newfile)
                PrintLine(templogfile, file_buffer)
            End While

            FileClose() ' all files
            ' Delete old SYSLOG.TXT
            Kill(SyslogName)
            ' Rename TEMPFILE.LOG as SYSLOG.TXT
            Rename(TempLogFileName, SyslogName)

        End If

    End Sub
    Sub UpdateLogViewer()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function Updates the Log VIewer GUI             *
        '*    PARAMETERS: ()                                        *
        '*    EXAMPLE:                                              *
        '*      UpdateLogViewer                                     *
        '************************************************************
        Dim DummyVar As Double

        If FileExists(SyslogName) Then
            'Show Splash
            CenterForm(frmMess)
            frmMess.Visible = True
            frmMess.txtMessage.Text = "Opening System Log..."
            frmMess.Refresh()

            'Force Normal State
            frmMess.WindowState = FormWindowState.Normal
            frmMess.Show()
            frmMess.Refresh()
            SetDefaults()
            frmSyslogViewer.rtbLogViewer.LoadFile(SyslogName, RichTextBoxStreamType.PlainText)
            '---- Modified for VIPERT-DR-144       05/03/2001  DJoiner
            '            DummyVar# = frmSyslogViewer.tbrSyslog.RestoreToolbar(1, "VIPERT", "System Log Toolbar")
            'frmSyslogViewer.tbrSyslog.RestoreToolbar(1, "VIPERT", "System Log Toolbar")

            frmMess.Hide()
        Else
            MsgBox("File Not Found: " + SyslogName, MsgBoxStyle.Exclamation, "System Log Error")
            'Show Splash
            CenterForm(frmMess)
            frmMess.Visible = True
            frmMess.txtMessage.Text = "Creating New System Log..."
            'Force Normal State
            frmMess.WindowState = FormWindowState.Normal
            frmMess.Show()
            frmMess.Refresh()
            FCreate(SyslogName)
            SetDefaults()
            frmSyslogViewer.rtbLogViewer.LoadFile(SyslogName, RichTextBoxStreamType.PlainText)

            frmMess.Hide()
        End If
        FileLength = FileLen(SyslogName)

    End Sub

    Sub UpdateSyslog()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function will update the System Log File        *
        '*    EXAMPLE:                                              *
        '*      UpdateSyslog                                        *
        '************************************************************
        
        Dim syslogfile As Integer
        
        Dim newfile As Integer
        Dim file_buffer As String = ""

        'Open Current Log
        syslogfile = FreeFile()
        FileOpen(syslogfile, SyslogName, OpenMode.Append)
        'Open New Data
        newfile = FreeFile()
        FileOpen(newfile, Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "\TEMPLOG.TXT", OpenMode.Input)

        While Not EOF(newfile)
            file_buffer = LineInput(newfile)
            PrintLine(syslogfile, file_buffer)
        End While

        FileClose() ' All files

    End Sub

    Function GetEchoTime() As String
        GetEchoTime = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function will determine local and GMT (Z) Time  *
        '*    EXAMPLE:                                              *
        '*      Time$ = GetEchoTime                                 *
        '************************************************************

        Dim TimeZoneInformation As New TIME_ZONE_INFORMATION(1) 'API Structure
        Dim StandardDate As SYSTEMTIME 'API Structure
        Dim DaylightDate As SYSTEMTIME 'API Structure
        Dim LocalTimeStruct As SYSTEMTIME 'API Structure
        Dim SystemTimeStruct As SYSTEMTIME 'API Structure
        Dim EchoTimeData As String = "" 'Formatted Time String
        Dim retval As Integer 'API Return Value
        Dim StdName As String 'String Std Format
        Dim ChrLoop As Short 'Character Index
        Dim Letter As String 'Position in array
        Dim Pos As Short 'Position in string
        On Error Resume Next

        'Check Local Time Values
        GetLocalTime(LocalTimeStruct)
        'Check System Time Values
        GetSystemTime(SystemTimeStruct)
        'Check System For Time, Date , Timezone
        retval = GetTimeZoneInformation(TimeZoneInformation)

        StdName = ""
        For ChrLoop = 0 To 32
            Letter = Convert.ToString(Chr(TimeZoneInformation.StandardName(ChrLoop)))
            If Letter = Convert.ToString(Chr(0)) Then Exit For
            StdName &= Letter
        Next ChrLoop

        '---Added as a result of TDR98337 for Version 1.6---
        'Error when System is Set in GMT+0 Timezone

        Pos = InStr(StdName, "Standard")
        If Pos <> 0 Then
            StdName = Strings.Left(StdName, Pos - 1)
        Else
            StdName &= " " 'Add A Space
        End If

        If retval = 2 Then 'Daylight Time
            StdName += "Daylight Time"
        End If
        If retval = 1 Then 'Standard Time
            StdName += "Standard Time"
        End If

        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wMonth), "00") & "/"
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wDay), "00") & "/"
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wYear), "00") & " "
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wHour), "00") & ":"
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wMinute), "00") & ":"
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wSecond), "00") & " "
        EchoTimeData &= "Zulu Time (GMT)"
        EchoTimeData &= vbCrLf

        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wMonth), "00") & "/"
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wDay), "00") & "/"
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wYear), "00") & " "
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wHour), "00") & ":"
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wMinute), "00") & ":"
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wSecond), "00") & " "

        If TimeZoneInformation.Bias & -60 >= 0 Then
            EchoTimeData &= StdName & " (GMT+" & Trim(CStr(TimeZoneInformation.Bias & -60)) & ") "
        Else
            EchoTimeData &= StdName & " (GMT" & Trim(CStr(TimeZoneInformation.Bias & -60)) & ") "
        End If

        EchoTimeData &= vbCrLf

        GetEchoTime = EchoTimeData

        '---End of modifications TDR98337 for Version 1.6---

    End Function
    
    
    Sub CenterChildForm(ByVal ParentForm As frmSyslogViewer, ByVal Childform As Form)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Centers the passed form with respect to a parent form*
        '*    PARAMETERS:                                           *
        '*     Childform=form to center, ParentForm=reference form  *
        '*    EXAMPLE:                                              *
        '*      CenterChildForm frmMain1, frmMessage                *
        '************************************************************

        Childform.Left = ((ParentForm.Width / 2) + ParentForm.Left) - Childform.Width / 2

    End Sub

    
    Sub CenterForm(ByVal X As Form)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Centers the passed form                              *
        '*    PARAMETERS:                                           *
        '*     x = form to center                                   *
        '*    EXAMPLE:                                              *
        '*      LiveMode% = VerifyInstrumentCommunication%          *
        '************************************************************

        ScreenCursorActive(Cursors.WaitCursor)
        X.Top = (PrimaryScreen.Bounds.Height) / 2 - X.Height / 2
        X.Left = PrimaryScreen.Bounds.Width / 2 - X.Width / 2
        ScreenCursorActive(Cursors.Default)

    End Sub


    
    Function FileExists(ByVal Path As String) As Short
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function will check for the existence of a file *
        '*    PARAMETERS:                                           *
        '*     Filename$ = Path$ + File To Check for Existance      *
        '*    EXAMPLE:                                              *
        '*      FileExists "C:\MYFILE.TXT"                          *
        '*    RETURNS:                                              *
        '*      If the file exists, it will return True (-1)        *
        '*      If the file does not exist, it will return False (0)*
        '************************************************************

        
        Dim X As Integer

        X = FreeFile() 'Get File Handle

        On Error Resume Next 'Disable Run-Time Error Trapping
        FileOpen(X, Convert.ToString(Path), OpenMode.Input)
        If CompareErrNumber("=", 0) Then 'No Error
            FileExists = True
        Else            'Error opening file
            FileExists = False
        End If
        FileClose(X) 'Close File

    End Function




    
    Sub SearchLog(ByVal Find As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function calls Findstring and Externally unloads*
        '*     the search form                                      *
        '*    PARAMETERS:                                           *
        '*     Find% = TRUE if Find First, FALSE if Find Next       *
        '*    EXAMPLE:                                              *
        '*      SearchLog (TRUE)                                    *
        '************************************************************

        frmSearch.Hide()
        frmSyslogViewer.Refresh()
        FindString(Find)

    End Sub

    Sub SetDefaults()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This function sets variable defaults at EXE Start-Up *
        '*    EXAMPLE:                                              *
        '*      SetDefaults                                         *
        '************************************************************

        MatchCase = False
        FindWholeWordOnly = False
        EchoToStatusBar("Row:0 ", 2)
        EchoToStatusBar("VIPERT System Log Viewer", 1)

    End Sub


    Sub EchoToStatusBar(ByVal Message As String, ByVal Panel As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Places Text Messages in the Status Bar               *
        '*    PARAMETERS:                                           *
        '*     Message$ = Text to be displayed ,Panel%=Panel #      *
        '*    EXAMPLE:                                              *
        '*      EchoToStatusBar("This Is Displayed In Panel 1"), 1) *
        '************************************************************

        'If message is not present, then print message. This avoids status bar "Flicker"
        If frmSyslogViewer.sbrUserInformation.Panels(Panel - 1).Text <> Message Then
            frmSyslogViewer.sbrUserInformation.Panels(Panel - 1).Text = Convert.ToString(Message)
        End If

    End Sub

    Sub UserPrompt(ByVal UserInstructionText As String)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG 32-BIT VERSION          *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Prompts User YES/NO With "ON-TOP" Z-ORDER            *
        '*     CREATED IN VERSION 1.7 MCLBID#05059901               *
        '*    PARAMETERS:                                           *
        '*     UserInstructionText$ = Text to be displayed          *
        '*    EXAMPLE:                                              *
        '*      Call UserPrompt("This Is Displayed In Panel")       *
        '************************************************************

        frmPrompt.txtMessage2.Text = UserInstructionText
        CenterForm(frmPrompt)
        BringToTop(frmPrompt.Handle.ToInt32)
        frmPrompt.ShowDialog()

    End Sub
    Function GatherIniFileInformation(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherIniFileInformation = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Finds a value on in the TETS.INI File                *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName$ -[Application] in TETS.INI File   *
        '*     lpKeyName$ - KEYNAME= in TETS.INI File               *
        '*     lpDefault$ - Default value to return if not found    *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath$ = GatherIniFileInformation("Heading", ...  *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************

        Dim lpReturnedString As New StringBuilder(Space(255), 255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        'Clear String Buffer
        lpReturnedString = New StringBuilder("")
        'Find Windows Directory
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        lpReturnedString = New StringBuilder("")
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo

    End Function


    Public mScreenCursor As Cursor = Cursors.Default
    Public Sub ScreenCursorSetValue(ByVal cur As Cursor)
        mScreenCursor = cur
    End Sub
    Public Sub ScreenCursorSetValue(ByVal bmp As Bitmap)
        Dim cur As Cursor = New Cursor(bmp.GetHicon())
        mScreenCursor = cur
    End Sub

    Public Sub ScreenCursorActive(ByVal cur As Cursor)
        For i As Integer = 0 To Application.OpenForms.Count - 1
            SetCursorRecursive(Application.OpenForms.Item(i), cur)
        Next
    End Sub

    Public Sub SetCursorRecursive(ByVal ctrl As Control, ByVal cur As Cursor)
        ctrl.Cursor = cur
        For i As Integer = 0 To ctrl.Controls.Count - 1
            SetCursorRecursive(ctrl.Controls.Item(i), cur)
        Next
    End Sub
    Public Sub SetCursorRecursive(ByVal ctrl As Control, ByVal bmp As Bitmap)
        SetCursorRecursive(ctrl, New Cursor(bmp.GetHicon()))
    End Sub

    
    Public ReadOnly Property AppPrevInstance() As Boolean
        Get
            AppPrevInstance = False
        End Get
    End Property

    Public Function CompareErrNumber(ByVal sConditon As String, ByVal Value As Integer) As Boolean
        Dim te As Integer = Err.Number
        Select Case sConditon
            Case "<=" : Return te <= Value
            Case ">=" : Return te >= Value
            Case "<>" : Return te <> Value
            Case "=" : Return te = Value
            Case ">" : Return te > Value
            Case "<" : Return te < Value
            Case Else
                Return False    ' - ???
        End Select

    End Function


    ' === External Constants: ===
    'Public Const Cursors.Default As Integer = 99
    Public Const cdlOFNHideReadOnly As Integer = 4
    Public Const cdlOFNOverwritePrompt As Integer = 2
    Public Const cdlPDReturnDC As Integer = 256
    Public Const cdlPDNoPageNums As Integer = 8
    Public Const cdlPDHidePrintToFile As Integer = 1048576
    Public Const cdlPDAllPages As Integer = 0
    Public Const cdlPDNoSelection As Integer = 4
    Public Const cdlPDSelection As Integer = 1



End Module