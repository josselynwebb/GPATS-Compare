'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.IO
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Microsoft.VisualBasic.FileIO

Public Module Install


    '=========================================================
    '*******************************************************************
    '**  Install.bas (Install)                                        **
    '**  This module contains the Functions and Subs which make up    **
    '**  the Software Installation Routines.                          **
    '*******************************************************************
    'API Types

    Structure LUID
        Dim LowPart As Integer
        Dim HighPart As Integer
    End Structure

    Structure LUID_AND_ATTRIBUTES
        Dim pLuid As LUID
        Dim Attributes As Integer
    End Structure

    Structure TOKEN_PRIVILEGES
        Dim PrivilegeCount As Integer
        Dim Privileges() As LUID_AND_ATTRIBUTES
        Dim LuidUDT As LUID
        Dim Attributes As Integer

        Public Sub New(ByVal unusedParam As Integer)
            ReDim Privileges(0)
        End Sub
    End Structure


    Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure


    Structure POINTAPI
        Dim X As Integer
        Dim Y As Integer
    End Structure


    Structure WINDOWPLACEMENT
        Dim length As Integer
        Dim flags As Integer
        Dim showCmd As Integer
        Dim ptMinPosition As POINTAPI
        Dim ptMaxPosition As POINTAPI
        Dim rcNormalPosition As RECT
    End Structure


    <StructLayout(LayoutKind.Sequential)> _
    Structure OSVERSIONINFO
        Dim dwOSVersionInfoSize As Integer
        Dim dwMajorVersion As Integer
        Dim dwMinorVersion As Integer
        Dim dwBuildNumber As Integer
        Dim dwPlatformId As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128), VBFixedString(128)> Dim szCSDVersion As String 'Maintenance string for PSS usage,
    End Structure    'Returns Service Pack Info

    'API Constants
    Private Const TOKEN_ADJUST_PRIVILEGES As Short = &H20
    Private Const TOKEN_QUERY As Short = &H8
    Private Const PROCESS_ALL_ACCESS As Integer = &H1F0FFF
    Private Const SW_SHOWNORMAL As Short = 1
    Private Const SW_SHOWMINIMIZED As Short = 2
    Private Const PROCESS_QUERY_INFORMATION As Short = &H400
    Private Const STILL_ACTIVE As Short = &H103

    'System Configuration Type Constants
    Public Const UNKNOWN As Short = 0
    Public Const TETS As Short = 1
    Public Const VIPERT_RF As Short = 2
    Public Const VIPERT_EO As Short = 3
    Public Const MNG As Short = 4
    Public Const VIPERT As Short = 5


    'RF Subsystem Device Constants
    Public Const TETS_RF_MANF_ID As Integer = 3922
    Public Const TETS_RF_MODEL_CODE As Integer = 50000
    Public Const VIPERT_RF_MANF_ID As Integer = 4008
    Public Const VIPERT_RF_MODEL_CODE As Integer = 1140
    Public Const TETS_DSO_MANF_ID As Integer = 4095
    Public Const TETS_DSO_MODEL_CODE As Integer = 451
    Public Const VIPERT_DSO_MANF_ID As Integer = 3712
    Public Const VIPERT_DSO_MODEL_CODE As Integer = 1428

    'API Declarations
    Private Declare Function GetVersion Lib "kernel32" () As Integer
    Private Declare Function GetCurrentProcess Lib "kernel32" () As Integer
    Private Declare Function OpenProcessToken Lib "advapi32" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
    Private Declare Function LookupPrivilegeValue Lib "advapi32" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer
    Private Declare Function AdjustTokenPrivileges Lib "advapi32" (ByVal TokenHandle As Integer, ByVal DisableAllPrivileges As Integer, ByRef NewState As TOKEN_PRIVILEGES, ByVal BufferLength As Integer, ByRef PreviousState As Integer, ByRef ReturnLength As Integer) As Integer
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwprocessID As Integer) As Integer
    Private Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As Integer, ByVal uExitCode As Integer) As Integer
    Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Integer, ByRef lpExitCode As Integer) As Integer
    Private Declare Function BringWindowToTop Lib "user32" (ByVal hWnd As Integer) As Integer
    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Private Declare Function GetWindowPlacement Lib "user32" (ByVal hWnd As Integer, ByRef lpwndpl As WINDOWPLACEMENT) As Integer
    Private Declare Function SetWindowPlacement Lib "user32" (ByVal hWnd As Integer, ByRef lpwndpl As WINDOWPLACEMENT) As Integer
    Private Declare Function WaitForInputIdle Lib "user32" (ByVal hProcess As Integer, ByVal dwMilliseconds As Integer) As Integer
    Private Declare Function GetVersionEx Lib "kernel32" Alias "GetVersionExA" (ByRef lpVersionInformation As OSVERSIONINFO) As Integer


    'Global Variables
    'Flag to indicate a software installation,
    'do not white a Shutdown record to the FHDB
    Public bInstall As Boolean
    Public iOpSystem As Short 'Operating System Identifier
    Public bIsNT As Boolean 'Flag to indicate this is a NT based OS

    'RF Configuration Globals
    Dim CheckRF_PH_SN As Boolean = False 'Flag to set RF Power Head SN

    'Path to the Internet Explorer
    'Win98 (for Debugging)
    Private Const EXPLORER_98 As String = "C:\Program Files (x86)\Internet Explorer\IEXPLORE.EXE"
    'WinNT 4
    Private Const EXPLORER_NT As String = "C:\Program Files (x86)\Plus!\Microsoft Internet\IExplore.exe"
    'Win2000
    Private Const EXPLORER_2K As String = "C:\Program Files (x86)\Internet Explorer\IExplore.exe"
    'Executables used for Installing software and Configuration of system
    Private Const TM_EXPLORER As String = "C:\Program Files (x86)\National Instruments\MAX\NIMAX.exe"
    'WinNT4
    Private Const THINKPAD_NT As String = "TPN.exe"
    'Win2000
    Private Const THINKPAD_2K As String = "C:\Program Files (x86)\ThinkPad\Utilities\TP98.exe"
    Private Const PSTORES As String = "C:\WinNT\System32\PStores -Install"
    Private Const SYSMENU As String = "C:\Program Files (x86)\ATS\SysMenu.exe"
    Private Const SNGL_SPACE As String = " "

    '.Htm Instruction sets to guide the INSTALLER through the Installation process
    Private Const INSTRUCTIONS_RESTORE As String = "C:\Program Files (x86)\ATS\Install_Instructions\Restore UDD.htm"
    Private Const INSTRUCTIONS_VXI_CONFIG As String = "C:\Program Files (x86)\ATS\Install_Instructions\VXI Memory Allocation Setting.htm"

    'Win NT4
    Private Const INSTRUCTIONS_TP_CONFIG_NT As String = "C:\Program Files (x86)\ATS\Install_Instructions\Configure ThinkPad Settings.htm"
    'Win 2000
    Private Const INSTRUCTIONS_TP_CONFIG_2K As String = "C:\Program Files (x86)\ATS\Install_Instructions\Configure ThinkPad Settings Win2k.htm"
    Private Const INSTRUCTIONS_EOTP_CONFIG As String = "C:\Program Files (x86)\ATS\Install_Instructions\EO TP Config.htm"
    Private Const INSTRUCTIONS_PORTS As String = "C:\Program Files (x86)\ATS\Install_Instructions\Configure PORTS.htm"
    Private Const INSTRUCTIONS_DEVICES As String = "C:\Program Files (x86)\ATS\Install_Instructions\Configure Devices.htm"

    'OS Identifier Constants
    Private Const WIN98 As Short = 1
    Public Const WINNT As Short = 2
    Private Const WIN2K As Short = 3
    Private Const WINXP As Short = 4
    Private Const WIN10 As Short = 5

    Dim sIE_Explorer As String 'Location of the Internet Explorer Application
    Dim sIC_SN As String 'Instrument Controller Serial Number


    Public Sub Configuration()
        '*****************************************************************************
        '**   Create the ATLAS Test Data file structure.                            **
        '**   To prevent the possibility of an obsolete version of the              **
        '**   M9SelfTest.exe in the "...\System Test" directory (This file was      **
        '**   copied manually), this routine will copy the current version to the   **
        '**   "...\System Test" Directory.                                          **
        '**   Automatically launches the Instructions Set and the user interface    **
        '**   required to configure settings based on the Operating System.         **
        '**   Launches the System Menu to allow the Installer to Restore the UDD.   **
        '**   Launches the T&M Explorer to allow the Installer to set the MXI-2     **
        '**   Hardware configuration.                                               **
        '**   Launches the IBM Configuration Utility. (The OS is identified and     **
        '**   the correct Instruction set is opened)                                **
        '**   Each is triggered by Closing the Current Configuration Application.   **
        '---------------------------------------------------------------------------**
        '** WINDOWS 2000 OS:                                                        **
        '**   The an attempt is made to initialize the Video Capture Card, if       **
        '**   successful, the system is automaticly identified as an EO Vareant.    **
        '**   If unsuccessful, the Installed is asked.                              **
        '**   System is shutdown.                                                   **
        '---------------------------------------------------------------------------**
        '** WINDOWS NT OS:                                                          **
        '**   User is asked if system variant is EO, if not, system is shutdown.    **
        '**   ---- EO Variant Only ---                                              **
        '**   Launches the IBM Configuration Utility again for EO Settings.         **
        '**   Launches the "Ports" Control Panel Applet.                            **
        '**   Launches the "Devices" Control Panel Applet.                          **
        '**   System is shutdown.                                                   **
        '*****************************************************************************
        Dim nRet As Integer 'Function Return Value
        Dim nHProc As Integer 'Process Handle
        Dim nExitCode As Integer 'Exit Code
        Dim nConfigHandle As Integer 'Configuration Utility Process Handle
        Dim nTMExplorer As Integer 'T&M Explorer Process Handle
        Dim nMenuHandle As Integer 'System Menu Process Handle
        Dim nPID_ToClose As Integer 'Internet Explorer Process ID

        Dim nMsgResponse As DialogResult 'Users Response to MsgBox
        Dim nCplHandle As Integer 'Control Panel Applet Handle
        Dim sDeviceError As String 'Error String from attempting to enable device
        Dim sSourceFile As String 'Source file to copy from
        Dim sDestinationFile As String 'Desination to copy file to
        Dim sInptboxResponse As String 'Input box response for Power Head SN
        Dim ConfigFlag(8) As Boolean 'flag for configureation
        Dim sMsg As String 'message for confirmation
        Dim nFile As Short
        Dim FileName As String
        Dim sLineResult As String
        Dim systemConfigurationType As Short = UNKNOWN
        Dim numTries As Short = 0


        'Keep for debugging
        'MessageBox.Show("Debug", "Sysmon", MessageBoxButtons.OK)

        '>>>>>>>>>>> Set up Test Data File Structure <<<<<<<<<<<
        If Not System.IO.Directory.Exists("C:\APS") Then
            System.IO.Directory.CreateDirectory("C:\APS")
        End If

        If Not System.IO.Directory.Exists("C:\APS\DATA") Then
            System.IO.Directory.CreateDirectory("C:\APS\DATA")
        End If

CHECK_CONFIG:
        If (numTries > 2) Then
            MessageBox.Show("No Valid System Configuration Has Been Detected. System Will Shutdown.")
            ShutDownSysmon()
            Exit Sub
        End If


        'initialize to false
        ConfigFlag(7) = False 'VIPERT
        ConfigFlag(6) = False 'TETS
        ConfigFlag(5) = False 'MNG
        ConfigFlag(4) = False 'v4 EO/RF
        ConfigFlag(3) = False 'V3 EO
        ConfigFlag(2) = False 'V2 RF
        ConfigFlag(1) = False

        systemConfigurationType = DetectSystemConfiguration()

        'Task 68238 Removing RF.  Changed this step to only copy ini file until I
        'could determine which systemtype(i.e. V2, V3) then copy the rest of the files over
        CopyATSIni(systemConfigurationType)
        RenameConfigurationFiles(systemConfigurationType)

        If (systemConfigurationType = TETS) Then
            UpdateIniFile("System Startup", "SYSTEM_TYPE", "AN/USM-657(V)2")
            UpdateIniFile("System Startup", "RF_OPTION_INSTALLED", "NO")
            UpdateIniFile("System Startup", "EO_OPTION_INSTALLED", "NO")
            ConfigFlag(6) = True
        ElseIf (systemConfigurationType = MNG) Then
            UpdateIniFile("System Startup", "SYSTEM_TYPE", "AN/USM-717(V)4")
            UpdateIniFile("System Startup", "RF_OPTION_INSTALLED", "NO")
            UpdateIniFile("System Startup", "EO_OPTION_INSTALLED", "NO")
            ConfigFlag(5) = True
        ElseIf (systemConfigurationType = VIPERT_RF) Then
            UpdateIniFile("System Startup", "SYSTEM_TYPE", "AN/USM-717(V)2")
            UpdateIniFile("System Startup", "RF_OPTION_INSTALLED", "NO")
            UpdateIniFile("System Startup", "EO_OPTION_INSTALLED", "NO")
            ConfigFlag(2) = True
        ElseIf (systemConfigurationType = VIPERT_EO) Then
            UpdateIniFile("System Startup", "SYSTEM_TYPE", "AN/USM-717(V)3")
            UpdateIniFile("System Startup", "RF_OPTION_INSTALLED", "NO")
            UpdateIniFile("System Startup", "EO_OPTION_INSTALLED", "YES")
            ConfigFlag(3) = True
        ElseIf (systemConfigurationType = VIPERT) Then
            UpdateIniFile("System Startup", "SYSTEM_TYPE", "AN/USM-717")
            UpdateIniFile("System Startup", "RF_OPTION_INSTALLED", "NO")
            UpdateIniFile("System Startup", "EO_OPTION_INSTALLED", "YES")
            ConfigFlag(7) = True
        ElseIf (systemConfigurationType = UNKNOWN) Then
            MessageBox.Show("Could Not Detect System Type. Exiting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(0)
        End If

        'Confirm System Configuration
        If ConfigFlag(7) = True Then sMsg = "AN/USM-717" 'VIPERT
        If ConfigFlag(6) = True Then sMsg = "AN/USM-657(V)2" 'TETS RF
        If ConfigFlag(4) = True Then sMsg = "AN/USM-717(V)1 EO/RF" 'EO/RF
        If ConfigFlag(3) = True Then sMsg = "AN/USM-717(V)3 EO" 'EO
        If ConfigFlag(2) = True Then sMsg = "AN/USM-717(V)2 RF" 'RF
        If ConfigFlag(5) = True Then sMsg = "AN/USM-717(V)4" 'MNG

        If ConfigFlag(2) = False And ConfigFlag(3) = False And ConfigFlag(4) = False And ConfigFlag(5) = False And ConfigFlag(6) = False And ConfigFlag(7) = False Then
            MsgBox("You need to select a ATS configuration.", MsgBoxStyle.OkOnly)
            GoTo CHECK_CONFIG
        End If

        nMsgResponse = MsgBox("This is an " & sMsg & " ATS." & vbCrLf & "Is this correct?", MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Verify Configuration")
        If nMsgResponse = DialogResult.No Then
            numTries += 1
            GoTo CHECK_CONFIG
        End If

        'Call interface to allow the user to enter the System Serial Number
        Check_ATE_Serial_Number()


        'DME change to ask user for the RF Power Head Serial Numbers and set to vipert.ini file
        'DME PCR VSYS - 270

        If CheckRF_PH_SN = True Then
PH_Serial_Number_A:
            sInptboxResponse = InputBox("Locate the 'HP8481A' Power Head Serial Number." & vbCrLf & "Type the Serial Number in to the space provided." & vbCrLf & vbCrLf & "Example: SERIAL NUMBER:" & vbTab & "US41030527", "Enter HP8481A Serial Number")

            nMsgResponse = MsgBox("The Serial Number entered is:  " & sInptboxResponse & vbCrLf & "Is this correct?", MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Verify Serial Number")
            If nMsgResponse = DialogResult.No Then
                sInptboxResponse = "" 'If the entry is incorrect, Clear the variable
                GoTo PH_Serial_Number_A 'Go through the process again
            Else
                UpdateIniFile("Serial Number", "HP8481A", sInptboxResponse)
            End If

PH_Serial_Number_D:
            sInptboxResponse = InputBox("Locate the 'HP8481D' Power Head Serial Number." & vbCrLf & "Type the Serial Number in to the space provided." & vbCrLf & vbCrLf & "Example: SERIAL NUMBER:" & vbTab & "3643A22262", "Enter HP8481D Serial Number")

            nMsgResponse = MsgBox("The Serial Number entered is:  " & sInptboxResponse & vbCrLf & "Is this correct?", MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Verify Serial Number")
            If nMsgResponse = DialogResult.No Then
                sInptboxResponse = "" 'If the entry is incorrect, Clear the variable
                GoTo PH_Serial_Number_D 'Go through the process again
            Else
                UpdateIniFile("Serial Number", "HP8481D", sInptboxResponse)
            End If

ATTN_Serial_Number:
            sInptboxResponse = InputBox("Locate the 'HP11708A' Attenuator Serial Number." & vbCrLf & "Type the Serial Number in to the space provided." & vbCrLf & vbCrLf & "Example: SERIAL NUMBER:" & vbTab & "12345", "Enter HP11708A Serial Number")

            nMsgResponse = MsgBox("The Serial Number entered is:  " & sInptboxResponse & vbCrLf & "Is this correct?", MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Verify Serial Number")
            If nMsgResponse = DialogResult.No Then
                sInptboxResponse = "" 'If the entry is incorrect, Clear the variable
                GoTo ATTN_Serial_Number 'Go through the process again
            Else
                UpdateIniFile("Serial Number", "HP11708A", sInptboxResponse)
            End If
        End If
        SetUpEthernetPorts()
        'DME Added to set the Azimuth and Elivation on EO units
        'PCR VSYS- 351
        '10/26/18 JW Remove prompting of AZ and EL 1320 change
        'If ConfigFlag(4) = True Or ConfigFlag(3) = True Then 'prompt for Azimuth and Elivation
        '    ResetLarrs()
        'End If
        'End If

    End Sub
    Public Sub SetUpEthernetPorts()
        'Provides instructions to the user on how to name the Ethernet ports for 
        'installation

        Dim caption As String
        Dim message As String
        Dim result As Integer
        Dim result2 As Integer

        caption = "Configure Ethernet Ports"
        message = "1. Open the Control Panel" & vbCrLf
        message &= "2. Click Network and Internet." & vbCrLf
        message &= "3. In the right pane click Network and Sharing Center" & vbCrLf
        message &= "4. In the left pane click Change adapter settings" & vbCrLf
        message &= "    NOTE: There will 6 possible connections shown." & vbCrLf
        message &= "6. Plug the Green Ethernet cable into J15 and J16 " & vbCrLf
        message &= "   on the backside of the controller." & vbCrLf & vbCrLf
        message &= "    NOTE: The red X will disappear for the two named " & vbCrLf
        message &= "    connections that have been connected, make a note of these." & vbCrLf & vbCrLf
        message &= "7. Move the cable from J16 to J18 paying close attention to which X " & vbCrLf
        message &= "   returns and disappears on the screen." & vbCrLf & vbCrLf
        message &= "    NOTE: The enumerated connection that remains must be J15." & vbCrLf
        message &= "    The connection that was connected and now shows as disconnected " & vbCrLf
        message &= "    is J16 and the newly enumerated conection must be J18." & vbCrLf
        message &= "    Use this technique to determine which connection is J19." & vbCrLf & vbCrLf
        message &= "8. Rename the J15 port to Gigabit1" & vbCrLf
        message &= "   Say [Yes] at the User Account Control" & vbCrLf
        message &= "9. Rename the J16 port to Gigabit2" & vbCrLf
        message &= "   Say [Yes] at the User Account Control" & vbCrLf
        message &= "10. Rename the J18 port to Local Area Connection" & vbCrLf
        message &= "   Say [Yes] at the User Account Control" & vbCrLf
        message &= "11. Rename the J19 port to Gigabit4" & vbCrLf
        message &= "   Say [Yes] at the User Account Control" & vbCrLf
        message &= "12. Rename the first unused connection to Local Area Connection X" & vbCrLf
        message &= "   Say [Yes] at the User Account Control" & vbCrLf
        message &= "13. Rename the second unused connection to Local Area Connection Y" & vbCrLf
        message &= "   Say [Yes] at the User Account Control" & vbCrLf & vbCrLf
        message &= "Verify that the port names are set up correctly" & vbCrLf & vbCrLf
        message &= "When finished Click OK to set the IP Adresses for these connections." & vbCrLf
        message &= "Say [Yes] when asked if you want to allow changes." & vbCrLf
        message &= "Click Enter to close command window"

        result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel)

        If result = DialogResult.OK Then
            RunSetIpAddress()
        End If

        Dim msg = "Before closing command window verify the program ran without errors." & vbCrLf
        msg &= "If there are issues fix them and click Retry to run the program again." & vbCrLf
        msg &= "Otherwise click Cancel to proceed."

        result2 = MessageBox.Show(msg, caption, MessageBoxButtons.RetryCancel)

        If result2 = DialogResult.Retry Then
            RunSetIpAddress()
        End If

    End Sub
    Public Sub RunSetIpAddress()
        Try

            Dim proc As New ProcessStartInfo()
            proc.UseShellExecute = True
            'You must set the UseShellExecute to "true" for this to work.
            proc.WorkingDirectory = "C:\Program Files (x86)\ATS\Development"
            proc.FileName = "SetIpAddress.bat"
            proc.Verb = "runas"
            'This is the action.
            Process.Start(proc)

        Catch ex As Exception

            MessageBox.Show("IP Address setup failed.")

        End Try
    End Sub
    '    Public Sub ResetLarrs()




    '        Dim sAZ As String, sEL As String, sReplace As String, sTemp As String, sFinal As String

    '        Dim msgResponse As DialogResult
    '        Dim FileArray(14) As String

    '        Dim i As Short, j As Short

    '        Dim nFile As Integer
    '        Dim FileName As String
    '        Dim sLineResult As String = ""

    '        nFile = FreeFile()
    '        FileName = "C:\Users\Public\Documents\ATS\LARRS\LARRS.dat"
    '        i = 1



    '        FileOpen(nFile, FileName, OpenMode.Input)
    '        ' Read the contents of the file
    '        Do While Not EOF(nFile)
    '            sLineResult = LineInput(nFile)
    '            FileArray(i) = sLineResult
    '            i += 1
    '        Loop
    '        FileClose(nFile)
    'InputAZ:
    '        'On Error GoTo ErrorHandler
    '        sAZ = InputBox("This is for EO configuration.  Look at the LARRS unit." & vbCrLf & "Please Input Azimuth (AZ):")
    '        Try
    '            If sAZ > 9999 Or sAZ = "" Then
    '                GoTo InputAZ
    '            End If
    '        Catch ex As Exception
    '            MsgBox("Input error, be sure to enter a number 0-9999, Please try again", MsgBoxStyle.OkOnly)
    '            GoTo InputAZ
    '        End Try

    'InputEL:
    '        sEL = InputBox("This is for EO configuration.  Look at the LARRS unit." & vbCrLf & "Please Input Elevation (EL):")
    '        Try
    '            If sEL > 9999 Or sEL = "" Then
    '                GoTo InputEL
    '            End If
    '        Catch ex As Exception
    '            MsgBox("Input error, be sure to enter a number 0-9999, Please try again", MsgBoxStyle.OkOnly)
    '            GoTo InputEL
    '        End Try

    '        msgResponse = MsgBox("Please double check your Azimuth and Elevation:" & vbCrLf & "AZ:    " & sAZ & vbCrLf & "EL:    " & sEL, MsgBoxStyle.YesNo)
    '        If msgResponse = DialogResult.No Then GoTo InputAZ


    '        sReplace = sAZ & ", " & sEL

    '        For i = 1 To 3
    '            sTemp = Mid(FileArray(i), 9)
    '            sFinal = sReplace & sTemp
    '            FileArray(i) = sFinal
    '        Next i

    '        Kill("C:\Users\Public\Documents\ATS\LARRS\LARRS.dat")

    '        FileOpen(nFile, FileName, OpenMode.Append)
    '        For i = 1 To 14
    '            PrintLine(nFile, FileArray(i))
    '        Next i
    '        FileClose(nFile)

    '        MsgBox("The LARRS.dat file has been successfully updated.", MsgBoxStyle.OkOnly)


    '    End Sub




    Public Function bWaitForAppToClose(ByVal sWindowTitle As String, Optional ByVal nAppHndl As Integer = 0, Optional ByVal bSysMenu As Boolean = False) As Boolean
        '****************************************************************
        '**    Waits for a window or application to close.             **
        '**                                                            **
        '**    sWindowTitle: The Window Tiltle to wait until closed    **
        '**    nAppHndl: The Application Handle to Wait until closed   **
        '**    bSysMenu: Flag to check for the System Menu to close    **
        '**                                                            **
        '**    Function returns True only when the System Menu has     **
        '**    been closed by the user, otherwise the return is False. **
        '****************************************************************
        Dim nWndCtlApp As Integer 'Control Application Handle
        Dim bUDDOpen As Boolean 'UDD Wizard Open Flag
        Dim bUDDClosed As Boolean 'UDD Closed Flag
        Dim nExitCode As Integer 'Application Exit Code

        bUDDOpen = False 'Initialize UDD Flags
        bUDDClosed = False

        Do Until bUDDClosed

            'Obtain the handle to the control app.
            nWndCtlApp = FindWindow(vbNullString, sWindowTitle)
            'If a handle was returned, set the UDD Open Flag
            If nWndCtlApp Then
                bUDDOpen = True 'Set UDD Open Flag
            End If
            'If the Control App Handle is 0, the App is Closed.
            If nWndCtlApp = 0 And bUDDOpen Then 'App has been opened then Closed
                bUDDClosed = True 'Set Flag to jump out of loop
                Exit Do 'Jump out of Loop
            End If
            System.Threading.Thread.Sleep(500) 'Wait for .5 Seconds

            'If the Installer closes the System Menu Window
            If bSysMenu Then
                nExitCode = 0 'Reset the Exit Code
                Application.DoEvents()
                'Verify if a process with handle: nHProc is still running
                GetExitCodeProcess(nAppHndl, nExitCode)
                Application.DoEvents()
                System.Threading.Thread.Sleep(500) 'Wait 0.5 Seconds
                'If not...end loop
                If nExitCode <> STILL_ACTIVE Then
                    bWaitForAppToClose = True 'Return True if System Menu has
                    Exit Do 'been closed by user.
                End If
            End If

        Loop

    End Function



    Public Sub RestoreWindow(ByVal sWindowTitle As String)
        '*********************************************************
        '**  Find the window identified by the Title Caption    **
        '**  passed in argument (sWindowTitle). Determine the   **
        '**  window state and restore if minimized. Set window  **
        '**  to foreground and bring to top.                    **
        '*********************************************************
        Dim nWndCtlApp As Integer 'Control Application Handle
        Dim currWinP As WINDOWPLACEMENT 'Current Window Placement

        'Obtain the handle to the control app.
        nWndCtlApp = FindWindow(vbNullString, sWindowTitle)

        If nWndCtlApp Then
            'Prepare the WINDOWPLACEMENT type.
            currWinP.length = Len(currWinP)

            If GetWindowPlacement(nWndCtlApp, currWinP) > 0 Then
                'Determine the window state.
                If currWinP.showCmd = SW_SHOWMINIMIZED Then
                    'Minimized, so restore.
                    currWinP.length = Len(currWinP)
                    currWinP.flags = 0
                    currWinP.showCmd = SW_SHOWNORMAL
                    SetWindowPlacement(nWndCtlApp, currWinP)
                Else
                    'On screen, so assure visible.
                    SetForegroundWindow(nWndCtlApp)
                    BringWindowToTop(nWndCtlApp)
                End If
            End If
        End If

    End Sub




    Public Function bInstallation() As Boolean
        '*********************************************************
        '**   Returns True if the INSTALLER is logged in and    **
        '**   starts the Software Installation Procedure.       **
        '**   Returns False if INSTALLER is NOT Logged in,      **
        '**   No further action is taken.                       **
        '*********************************************************
        Dim sInitMsg As String 'Initialization Message
        Dim iTimeElaspsed As Short 'Minutes Elapsed Counter
        Dim fProgress As Single 'Value to set the Progress Bar

        sInitMsg = "System Initializing...                            Please Wait" & vbCrLf
        sInitMsg &= vbCrLf
        sInitMsg &= "If any 'Service Control Manager' messages appear, Select [OK]." & vbCrLf
        sInitMsg &= "If a 'DHCP Client' message appears, Select [No]."


        bInstall = True 'Initialize Install Flag
        If (IsElevated(System.Environment.UserName) And GatherIniFileInformation("System Startup", "SN", "Unknown") = "") Then

            'If the Installer is Logged in
            If bSNValid() = False Then 'System Serial Number is Invalid
                '
                SetupInstructions()

                'Call the Software Installation Routine
                Configuration()

                'Inform the user that they must log back on to system with their normal logon
                MsgBox("The System will automatically shut down." & vbCrLf & "Please Restart and log in as either DoD_Admin or Maintainer" & vbCrLf & "to run and verify proper ATS startup.")

                'At this point the system has been configured
                ShutDownSysmon() 'Shutdown System

                bInstallation = True 'Return True
                Exit Function 'Exit
            Else
                ExitWindowsNT() 'ShutDown System
            End If

        Else

            If bSNValid() = False Then 'System Serial Number is not Valid
                'If logged on as non-Admin account and the System Serial
                'Number is invalid, Shut down system.
                ExitWindowsNT() 'ShutDown System
            Else
                'Normal Startup......
                bInstallation = False 'Return False
                bInstall = False 'Set Install Flag to False to allow Shutdown
            End If 'entry to be made to the FHDB.
        End If

    End Function

    Public Sub SetupInstructions()


        Dim curTime As String

        Dim UserResponse As DialogResult
        Dim EthernetResult As String
        Dim bFixEthResponse As Boolean
        Dim MesBoxResp As DialogResult


        frmSetup.Visible = True

        'Hardware Dialog box's

        frmSetup.pHDB.Image = My.Resources.frmSetup_picPass

        'Set Current time
        frmSetup.Text1.Text = "Please follow the Date and Time Message instructions and click [Ok] when ready to continue."
        frmSetup.Refresh()
        curTime = DateAndTime.Now.ToString("M/dd/yyyy h:mm tt")
        UserResponse = MsgBox("Is the current Date and Time = " & curTime & "?", MsgBoxStyle.YesNo)

        Do While UserResponse = DialogResult.No

            MsgBox("Date and Time Setup Instructions:" & vbCrLf &
                   "- Right click the digital clock in the lower right corner" & vbCrLf &
                   "- Select ""Adjust date/time"" in the menu" & vbCrLf &
                   "- Ensure ""Set Time automatically"" is turned off" & vbCrLf &
                   "- Ensure ""Set Time Zone automatically"" is turned off" & vbCrLf &
                   "- In the Time Zone dropdown, choose your respective time zone." & vbCrLf &
                   "- Click the ""Change"" button under Change date and time." & vbCrLf &
                   "- Select the correct date and time" & vbCrLf &
                   "- Click ""Change""" & vbCrLf &
                   "- Close the window.", MsgBoxStyle.OkOnly)
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData()
            curTime = DateAndTime.Now.ToString("M/dd/yyyy h:mm tt")
            UserResponse = MsgBox("Is the current time = " & curTime & "?", MsgBoxStyle.YesNo)
        Loop
        frmSetup.pSDT.Image = My.Resources.frmSetup_picPass
        frmSetup.Refresh()
        frmSetup.Text1.Text = "Initial Setup Complete."
        frmSetup.Refresh()
        MsgBox("ATS Initial Setup is now complete, Click [Ok] to continue.", MsgBoxStyle.OkOnly)
        Delay(0.5)
        frmSetup.Close()

    End Sub



    Public Sub UpdateVIPERT_SN(ByVal sSysSN As String)
        '*********************************************************************
        '**     Update the VIPERT INI file with the System Serial Number.     **
        '*********************************************************************

        'Write the Serial Number value to the VIPERT.ini file
        sUUT_Serial_No = sSysSN 'Assign value to Global variable
        UpdateIniFile("System Startup", "SN", sSysSN)

    End Sub




    Sub Check_ATE_Serial_Number()
        '**************************************************************************************
        '***    Read the VIPERT Serial Number from the ini file on startup.  If the SN value  ***
        '***    is invalid, the operator will be prompted to enter a valid SN. After the    ***
        '***    new SN has been validated, the Operator will be given a chance to verify    ***
        '***    and change it if necessary.                                                 ***
        '**************************************************************************************

        Dim bUpdated As Boolean 'Serial Number Updated Flag

        Dim iResponse As DialogResult 'Message Box Response indicator

        'On System Startup or Restart
        bUpdated = False 'Initialize Updated flag
        'Even through the Input Box has a [Cancel] button, it will not let the Operator proceed
        'Without first Entering a valid VIPERT System Serial Number.

Check_Serial_Number:
        'If the System Serial Number is not valid, Prompt the operator to enter a valid Serial Number
        If Not IsNumeric(sSerial_No) Then 'If the Serial Number is not numeric
            bUpdated = True 'Set Update flag to true
            Delay(0.5)
            'Prompt the Operator to Enter a valid Serial Number
            sSerial_No = InputBox("Please Enter the ATS Serial Number." & vbCrLf & "This information is stamped on the Identification Plate located on the sides of all transit cases." & vbCrLf & vbCrLf & "Example: SERIAL NUMBER:" & vbTab & "2011", "Enter System Serial Number")
            'If the Serial Number entered is greater than 3 digits, INVALID
            'Set the Variable's value to an empty string and Check again.
            If Not IsNumeric(sSerial_No) Then
                sSerial_No = ""
                GoTo Check_Serial_Number
            End If
            If CInt(sSerial_No) > 9999 Or CInt(sSerial_No) < 0 Then
                MsgBox("The serial number you entered is invalid." & vbCrLf & "You must enter a serial number between 1000 - 9999", MsgBoxStyle.OkOnly)
                sSerial_No = ""
                GoTo Check_Serial_Number
            End If
            If Len(sSerial_No) < 1 Then
                sSerial_No = ""
                GoTo Check_Serial_Number
            End If 'Check the Serial Number for validity again
        End If
        'If the Serial Number has been Updated, Ask the Operator to Varify his entry
        If bUpdated Then
            sSerial_No = VB6.Format(sSerial_No, "0000") 'Format Serial Number into a four digit number
            iResponse = MsgBox("The Serial Number entered is:  " & sSerial_No & vbCrLf & "Is this correct?", MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Verify Serial Number")
            If iResponse = DialogResult.No Then
                sSerial_No = "" 'If the entry is incorrect, Clear the variable
                GoTo Check_Serial_Number 'Go through the validation process again
            End If
        End If
        'If the Serial Number has been Updated, Write the new value to the VIPERT.ini file
        If bUpdated Then
            sUUT_Serial_No = sSerial_No 'Assign value to Global variable
            UpdateIniFile("System Startup", "SN", sSerial_No)
        End If

        'Task 68238 RF Removal.  Determine which VIPER it is then copoy the configuration files as well as assign the correct system type to ini
        If CInt(sSerial_No) > 1000 And CInt(sSerial_No) < 2000 Then
            UpdateIniFile("System Startup", "EO_OPTION_INSTALLED", "NO")
            UpdateIniFile("System Startup", "SYSTEM_TYPE", "AN/USM-717(V)2")
            CopyConfigurationFiles(VIPERT_RF)


        ElseIf CInt(sSerial_No) > 2000 And CInt(sSerial_No) < 3000 Then
            UpdateIniFile("System Startup", "SYSTEM_TYPE", "AN/USM-717(V)3")
            CopyConfigurationFiles(VIPERT_EO)


        ElseIf CInt(sSerial_No) < 1000 Then
            UpdateIniFile("System Startup", "SYSTEM_TYPE", "AN/USM-657(V)2")
            CopyConfigurationFiles(TETS)


        End If

    End Sub




    Private Function bSNValid() As Boolean
        '**********************************************************
        '*  Checks the System Serial Number value for numeric,    *
        '*  if True, Test numeric value to equal 1-999.           *
        '*  If any test fails, Return is False, otherwise         *
        '*  Return is True.                                       *
        '**********************************************************
        Dim nValidateSN As Integer 'Integer Serial Number Value
        Dim sSerialNumToValiidate As String 'String value of the System Serial Number

        'Get System Serial Number from the VIPERT.INI File
        sSerialNumToValiidate = GatherIniFileInformation("System Startup", "SN", "")
        'Validate System Serial Number
        If IsNumeric(sSerialNumToValiidate) Then
            sSerial_No = sSerialNumToValiidate 'Assign to Global
            nValidateSN = CLng(sSerialNumToValiidate)
            If nValidateSN < 0 Or nValidateSN > 9999 Then
                'Serial Number is Invalid
                bSNValid = False
            Else
                'Serial Number is Valid
                bSNValid = True
            End If
        Else
            'Serial Number is Invalid
            bSNValid = False
        End If

    End Function




    Private Function KillProcess(ByVal nProcessID As Integer, Optional ByVal nExitCode As Integer = 0) As Boolean
        '********************************************************************
        '*  Terminates any application and returns an exit code to Windows **
        '*  This works under NT/2000, even when the calling process        **
        '*  doesn't have the privilege to terminate the application        **
        '*  (for example, this may happen when the process was launched    **
        '*  by yet another program)                                        **
        '*                                                                 **
        '*  Usage: Dim pID As Long                                         **
        '*         pID = Shell("Notepad.Exe", vbNormalFocus)               **
        '*         '...                                                    **
        '*         If KillProcess(pID, 0) Then                             **
        '*             MsgBox "Notepad was terminated"                     **
        '*         End If                                                  **
        '********************************************************************
        Dim nToken As Integer 'Current Token Privileges
        Dim nProcess As Integer 'Process ID (Handle)
        Dim tp As TOKEN_PRIVILEGES 'NT Security Privileges
        'Windows NT/2000 require a special treatment to ensure that the
        'calling process has the privileges to shut down the system
        'under NT the high-order bit (that is, the sign bit)
        'of the value returned by GetVersion is cleared

        If GetVersion() >= 0 Then
            'Open the tokens for the current process
            'Exit if any error
            If OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, nToken) = 0 Then
                GoTo CleanUp
            End If

            'Retrieves the Locally Unique IDentifier (LUID) used
            'to locally represent the specified privilege name
            '(first argument = "" means the local system)
            ' Exit if any error
            If LookupPrivilegeValue("", "SeDebugPrivilege", tp.LuidUDT) = 0 Then
                GoTo CleanUp
            End If

            'Complete the TOKEN_PRIVILEGES structure with the # of
            'privileges and the desired attribute
            tp.PrivilegeCount = 1
            tp.Attributes = SE_PRIVILEGE_ENABLED

            'Try to acquire debug privilege for this process
            'Exit if error
            If AdjustTokenPrivileges(nToken, False, tp, 0, 0, 0) = 0 Then
                GoTo CleanUp
            End If
        End If

        'Now we can finally open the other process
        'while having complete access on its attributes
        'Exit if any error
        nProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, nProcessID)
        If nProcess Then
            'Call was successful, so we can kill the application
            'Set return value for this function
            KillProcess = (TerminateProcess(nProcess, nExitCode) <> 0)
            'Close the process handle
            CloseHandle(nProcess)
        End If

        If GetVersion() >= 0 Then
            'Under NT restore original privileges
            tp.Attributes = 0
            AdjustTokenPrivileges(nToken, False, tp, 0, 0, 0)

CleanUp:
            If nToken Then CloseHandle(nToken)
        End If
    End Function




    Public Sub GetOSVersion()
        '*****************************************************************************
        '**   Gets the Operating System Version Information from the System         **
        '**   and sets the "iOpSystem" value to identify the current Operating      **
        '**   System.                                                               **
        '*****************************************************************************
        Dim OsVerMin As Integer = System.Environment.OSVersion.Version.Minor
        Dim OsVerMaj As Integer = System.Environment.OSVersion.Version.Major

        'Identify the Operating System
        Select Case OsVerMaj
            Case 4
                'Win 95, 98, ME, NT4

                Select Case OsVerMin
                    Case 0
                        'Win NT4
                        iOpSystem = WINNT 'ID OS
                        sIE_Explorer = EXPLORER_NT 'Set IExplorer location
                        bIsNT = True 'Set NT based OS Flag
                    Case 10
                        'Win 98
                        iOpSystem = WIN98 'ID OS
                        sIE_Explorer = EXPLORER_98 'Set IExplorer location
                End Select

            Case 5
                'Win 2K, XP, Server 2003

                Select Case OsVerMin
                    Case 0
                        'Win NT4, Win2K
                        iOpSystem = WIN2K 'ID OS
                        sIE_Explorer = EXPLORER_2K 'Set IExplorer location
                        bIsNT = True 'Set NT based OS Flag
                    Case 1
                        'Win XP
                        iOpSystem = WINXP 'ID OS
                        bIsNT = True 'Set NT based OS Flag
                End Select

            Case 6
                'Win10

                iOpSystem = WIN10 'ID OS
                bIsNT = True 'Set NT Based OS Flag

        End Select

    End Sub




    Function sFindDirRoot(ByVal sPath As String) As String
        sFindDirRoot = ""
        '**********************************************************
        '***    Returns the relative Drive/Path string          ***
        '**********************************************************

        Dim iIndex As Integer 'Loop Index
        Dim iCounter As Short 'Second Loop Counter

        'Parse off end until a "\" is found
        For iIndex = Len(sPath) To 1 Step -1
            If Asc(Mid(sPath, iIndex, 1)) = 92 Then 'chr(92) = \
                Exit For
            End If
        Next iIndex
        sFindDirRoot = Strings.Left(sPath, (iIndex))

    End Function

    Function DetectSystemConfiguration() As Short

        Dim fpuSession As Integer = 0
        Dim dsoSession As Integer = 0
        Dim retCount As Integer = 0
        Dim manfId As Integer = 0
        Dim modelCode As Integer = 0
        Dim configType As Short = UNKNOWN

        'show dialog box to inform user of whats going on
        frmDialog.lblDialog.Text = "Detecting System Configuration..."
        frmDialog.Show()
        frmDialog.Refresh()

        'first power on chassis
        RetValue = viOpenDefaultRM(RmSession)
        If RetValue Then
            'unable to open visa
        End If

        RetValue = viOpen(RmSession, "GPIB0::5::15", 0, 10, fpuSession)
        If RetValue Then
            'unable to open session with fpu
        End If

        RetValue = viWrite(fpuSession, "KAA" + vbCrLf, 4, retCount)
        If RetValue Then
            'unable to power on fpu
        End If

        'then run resman
        RunResMan()

        'open visa session and get manf id and model codes for the DSO intrument
        If (viOpen(RmSession, "VXI0::17::INSTR", 0, 10, dsoSession) = 0) Then

            viGetAttribute(dsoSession, VI_ATTR_MANF_ID, manfId)
            viGetAttribute(dsoSession, VI_ATTR_MODEL_CODE, modelCode)

            If (manfId = TETS_DSO_MANF_ID And modelCode = TETS_DSO_MODEL_CODE) Then
                configType = TETS
            ElseIf (manfId = VIPERT_DSO_MANF_ID And modelCode = VIPERT_DSO_MODEL_CODE) Then
                configType = VIPERT
            Else
                configType = UNKNOWN
            End If

        End If

        'power off chassis
        RetValue = viWrite(fpuSession, "+AA" + vbCrLf, 4, retCount)
        If RetValue Then
            'unable to power on fpu
        End If

        viClose(dsoSession)
        viClose(fpuSession)

        'hide dialog
        frmDialog.Hide()

        Return configType

    End Function


    Sub CopyConfigurationFiles(ByVal systemType As Integer)

        Dim directory As String

        Select Case systemType
            Case VIPERT_RF
                directory = "C:\Program Files (x86)\ATS\ISS\config\Station_Configuration_Files\VIPERT_RF_CONFIG_FILES\"
            Case VIPERT_EO
                directory = "C:\Program Files (x86)\ATS\ISS\config\Station_Configuration_Files\VIPERT_EO_CONFIG_FILES\"
            Case TETS
                directory = "C:\Program Files (x86)\ATS\ISS\config\Station_Configuration_Files\TETS_CONFIG_FILES\"
            Case VIPERT
                directory = "C:\Program Files (x86)\ATS\ISS\config\Station_Configuration_Files\VIPERT_CONFIG_FILES\"
        End Select

        Try
            File.Copy(directory & "TestStationInstance.xml", "C:\Program Files (x86)\ATS\ISS\config\TestStationInstance.xml", True)
            File.Copy(directory & "PawsAllocation.xml", "C:\Program Files (x86)\ATS\ISS\config\PawsAllocation.xml", True)

        Catch ex As Exception

            MessageBox.Show("Could Not Copy Configuration Files")

        End Try

    End Sub
    Sub CopyATSIni(ByVal systemType As Integer)
        '********************************************************************
        '*  Takes in the systemType and uses that value to determine       **
        '*  the directory to get the ini file from.  Once determined       **
        '*  copies over existing ini file                                  **       
        '********************************************************************
        Dim directory As String

        Select Case systemType
            Case VIPERT_RF
                directory = "C:\Program Files (x86)\ATS\ISS\config\Station_Configuration_Files\VIPERT_RF_CONFIG_FILES\"
            Case VIPERT_EO
                directory = "C:\Program Files (x86)\ATS\ISS\config\Station_Configuration_Files\VIPERT_EO_CONFIG_FILES\"
            Case TETS
                directory = "C:\Program Files (x86)\ATS\ISS\config\Station_Configuration_Files\TETS_CONFIG_FILES\"
            Case VIPERT
                directory = "C:\Program Files (x86)\ATS\ISS\config\Station_Configuration_Files\VIPERT_CONFIG_FILES\"
        End Select

        Try
            File.Copy(directory & "ATS.ini", "C:\Users\Public\Documents\ATS\ATS.ini", True)
        Catch ex As Exception
            MessageBox.Show("Could Not Copy Configuration Files")
        End Try

    End Sub

    Sub RenameConfigurationFiles(ByVal systemType As Integer)

        Try
            'First delete any that are still out there
            FileIO.FileSystem.DeleteFile("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\Busconfi", UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            FileIO.FileSystem.DeleteFile("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\DeviceDB.DEV", UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            FileIO.FileSystem.DeleteFile("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB.SWX", UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            FileIO.FileSystem.DeleteFile("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB", UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            FileIO.FileSystem.DeleteFile("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB.LEX", UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        Catch ex As Exception
            'we don't care if the files are already there
        End Try

        Try
            Select Case systemType
                Case VIPERT_RF, VIPERT_EO, VIPERT
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\Busconfi_VIPERT", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\Busconfi")
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\DeviceDB_VIPERT.DEV", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\DeviceDB.DEV")
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB_VIPERT.SWX", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB.SWX")
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB_VIPERT", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB")
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB_VIPERT.LEX", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB.LEX")
                Case TETS
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\Busconfi_TETS", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\Busconfi")
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\DeviceDB_TETS.DEV", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Station\DeviceDB.DEV")
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB_TETS.SWX", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB.SWX")
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\lexDB_TETS", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB")
                    File.Copy("C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB_TETS.LEX", "C:\usr\Tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB.LEX")
            End Select

        Catch ex As Exception

            'we don't care if the files are already there

        End Try

    End Sub

    Public Function IsAdmin(userName As String) As Boolean

        Const GROUP_ADMIN = "administrators"

        Using context As System.DirectoryServices.AccountManagement.PrincipalContext = New System.DirectoryServices.AccountManagement.PrincipalContext(DirectoryServices.AccountManagement.ContextType.Machine)
            Dim groupPrincipal As System.DirectoryServices.AccountManagement.GroupPrincipal = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, GROUP_ADMIN)
            If groupPrincipal IsNot Nothing Then
                Dim members = groupPrincipal.GetMembers
                For Each member As System.DirectoryServices.AccountManagement.Principal In members
                    If String.Compare(member.Name, userName, True) = 0 Then
                        IsAdmin = True
                        Exit Function
                    End If
                Next
            End If
        End Using

        IsAdmin = False

    End Function

    Public Function IsElevated(userName As String) As Boolean

        Const GROUP_ADMIN = "administrators"
        Const GROUP_POWER = "power users"

        Using context As System.DirectoryServices.AccountManagement.PrincipalContext = New System.DirectoryServices.AccountManagement.PrincipalContext(DirectoryServices.AccountManagement.ContextType.Machine)
            Dim groupPrincipal As System.DirectoryServices.AccountManagement.GroupPrincipal = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, GROUP_ADMIN)
            If groupPrincipal IsNot Nothing Then
                Dim members = groupPrincipal.GetMembers
                For Each member As System.DirectoryServices.AccountManagement.Principal In members
                    If String.Compare(member.Name, userName, True) = 0 Then
                        IsElevated = True
                        Exit Function
                    End If
                Next
            End If

            groupPrincipal = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, GROUP_POWER)

            If groupPrincipal IsNot Nothing Then
                Dim members = groupPrincipal.GetMembers
                For Each member As System.DirectoryServices.AccountManagement.Principal In members
                    If String.Compare(member.Name, userName, True) = 0 Then
                        IsElevated = True
                        Exit Function
                    End If
                Next
            End If

        End Using

        IsElevated = False

    End Function

End Module