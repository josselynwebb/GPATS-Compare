'Option Strict Off
'Option Explicit On

Imports System
Imports System.Text
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports FHDBD
Imports Microsoft.Win32

Public Module FHDB_Services


    '=========================================================
    '**********************************************************************
    '***                                                                ***
    '***    Nomenclature:   Module "FHDB_Services" : SysMon             ***
    '***    Purpose:                                                    ***
    '***    Provides the SysMon  with the functionality needed to       ***
    '***    create records and pass data to the FHDB.                   ***
    '***    Also automatically checks for Proper Shutdown and Launches  ***
    '***    FHDB Processor when an Import/Export operation is Due.      ***
    '***    If the Operator chooses to defer the Operation, a timer     ***
    '***    (tmrUpdateStop) in the frmSysMon checks at 1 Minute         ***
    '***    Intervals to determine if 24 hours has passed, at this      ***
    '***    time the Processor is Triggered again.                      ***
    '***    This Timer also Updates the VIPERT.ini with the Primary       ***
    '***    Chassis Intake Temperature making it available to the FHDB. ***
    '**********************************************************************

    'Define Global Level Variables
    Public bShutdownFail As Boolean 'Switch to indicate a Normal Shutdown failure
    Public dShutdownCode As Double 'Code to Identify an Emergency Shutdown
    Public bReStart As Boolean 'Switch to indicate a Restart event
    Public bOnStart As Boolean 'Switch to indicate the System is Starting
    Public sFaultCallout As String 'The Fault Callout
    Public sLastStartupTime As String 'The last value written to the System_Startup key in the VIPERT.ini file

    'Define Data variables to pass to the FHDB
    Public sERO As String 'ERO field value  (Char Len 5)
    Public sUUT_Serial_No As String 'UUT Serial Number field value  (Char Len 15)
    Public sUUT_Rev As String 'UUT Revision field value  (Char Len 10)
    Public sID_Serial_No As String 'ID Serial Number field value  (Char Len 10)
    Public sSerial_No As String 'VIPERT System Serial Number

    'Define Local Variables
    Dim bATE_Serial_Invalid As Boolean 'Flag to indicate an Invalid ATE Serial Number
    Dim sPresent_Startup_Time As String 'Present Session's Startup Time
    Dim bRestoredSystem As Boolean 'Switch to indicate a Restored System

    'Used to Reference the VB DLL (FHDBCDriver.DLL) as an Object
    'NOTE: It must be Referenced in the Project References section
    Public objData As New FHDB.DLLClass

    '-----------------------------------------------------------------
    '---                    Constants                              ---
    '-----------------------------------------------------------------
    Public Const START As String = "START"
    Public Const END_IT As String = "STOP"
    Public Const Pass As Short = 1
    Public Const FAIL As Short = 0
    Public Const F_START_AB As String = "START EVENT FAILED: " & vbCrLf & "Previous Shutdown Abnormal. " & vbCrLf & "Synthesized Shutdown Performed"
    Public Const F_START_CAL As String = "START EVENT FAILED: " & vbCrLf & "Calibration Due"
    Public Const F_STOP_EMER As String = "STOP EVENT FAILED: " & vbCrLf & "Fully Mature SysMon-Initiated " & vbCrLf & "EMERGENCY  SHUTDOWN"
    Private Const DEFAULT_DATE As Date = #12:00:00 AM#


    Public Sub CheckImportExport()
        '********************************************************************************
        '***    Procedure to check for Import/Export function due.                    ***
        '***    The procedure will check for function due on Startup then             ***
        '***    set bOnStart switch to False.                                         ***
        '***    When the Timer triggers to check for Import/Export function due       ***
        '***    the procedure will also check the LastChecked value to determine      ***
        '***    if the Operator Deferred the function due.                            ***
        '***    If there are no values in the INI keys, IE: First install or Upgrade  ***
        '***    Default values will be written to keys.                               ***
        '***                                                                          ***
        '********************************************************************************

        Dim dtNext_ImportExport As Date 'Date/Time the next Import/Export Operation is Due
        Dim sDDI As String 'Data Dump Interval
        Dim sXDate As String 'Export time Operation Due Date/time
        Dim sIDate As String 'Import Operation Due Date/time
        Dim sLastCheck As String 'Last time the Operation Due was checked
        Dim lCount As Short 'counter for loop


        Try ' On Error GoTo IXErrorHandler

            lCount = 0

            Do
                sXDate = ValidatedDate("FHDB", "EXPORT_TIME") 'Get Export Due Date/Time
                If (sXDate = Convert.ToString(DEFAULT_DATE)) Then
                    UpdateIniFile("FHDB", "EXPORT_TIME", "")
                    Delay(2)
                End If
                sXDate = ValidatedDate("FHDB", "EXPORT_TIME")

                sIDate = ValidatedDate("FHDB", "IMPORT_TIME", , True) 'Get Import Due Date/Time, If invalid,Delay 2 sec
                If (sIDate = Convert.ToString(DEFAULT_DATE)) Then
                    UpdateIniFile("FHDB", "IMPORT_TIME", "")
                    Delay(2)
                End If
                sIDate = ValidatedDate("FHDB", "IMPORT_TIME", , True)

                sLastCheck = ValidatedDate("FHDB", "LASTCHECKED") 'Get the Time an Operation was last checked
                If (sLastCheck = Convert.ToString(DEFAULT_DATE)) Then
                    UpdateIniFile("FHDB", "LASTCHECKED", "")
                End If
                sLastCheck = ValidatedDate("FHDB", "LASTCHECKED")

                lCount += 1
            Loop While sXDate = "" And lCount < 3

            'Get the Data Dump Interval
            sDDI = GatherIniFileInformation("FHDB", "DDI", "")
            If Not IsNumeric(sDDI) Then
                sDDI = "30" 'Government approved Initial value
                UpdateIniFile("FHDB", "DDI", "30") 'Write to INI file
            End If



            Debug.Print(sLastCheck)
            'Is Import or Export the next operation due
            If CDate(sIDate).ToOADate > CDate(sXDate).ToOADate Then 'If Export.
                Debug.Print("Export")
                dtNext_ImportExport = DateAdd(DateInterval.Day, CInt(sDDI), CDate(sXDate)) 'Calculate Next Export due date.
                If Now.ToOADate > dtNext_ImportExport.ToOADate Then 'If Export operation has been deferred,
                    If bOnStart = True Then
                        LaunchFHDB() 'Launch FHDB Processor
                    Else

                        '                dtNext_ImportExport = DateAdd("n", 5, dtLastCheck)   'For Debugging
                        Debug.Print("Time to Trip:  " & vbTab & dtNext_ImportExport.ToString())
                        dtNext_ImportExport = DateAdd(DateInterval.Day, 1, CDate(sLastCheck)) 'DJoiner 03/23/01
                        If Now.ToOADate > dtNext_ImportExport.ToOADate Then
                            LaunchFHDB() 'Launch FHDB Processor
                        End If
                    End If
                End If
            Else                'if Import
                Debug.Print("Import")
                If bOnStart = True Then
                    LaunchFHDB() 'Launch FHDB Processor
                Else                    'Add 24 Hrs to LastChecked value

                    '           dtNext_ImportExport = DateAdd("n", 2, dtLastCheck)      'For Debuging (2 Minutes)
                    dtNext_ImportExport = DateAdd(DateInterval.Day, 1, CDate(sLastCheck)) '1 Day (24 Hours) Deferment
                    If Now.ToOADate > dtNext_ImportExport.ToOADate Then
                        Debug.Print("The Time is: " & Now)
                        Debug.Print("Launch FHDB Importer at : " & dtNext_ImportExport.ToString())
                        LaunchFHDB() 'Launch FHDB Processor
                    End If
                End If
            End If

            bOnStart = False 'Set bOnStart to False so when system checks
            'it will also check the LastChecked value

        Catch   ' IXErrorHandler:
            'do nothing and resume

        End Try
    End Sub


    Sub FHDB_Scheduler()
        '******************************************************************
        '***    Procedure to Create StartUp Record and Initiate         ***
        '***    Scheduler.                                              ***
        '***                                                            ***
        '******************************************************************

        bOnStart = True 'Set OnStart Switch to True
        FHDB_Startup() 'Check for Proper Shutdown
        CheckImportExport() 'Check for Import or Export Operation Due

        frmSysMon.tmrUpdateStop.Enabled = True 'Start Update Timer

    End Sub




    Sub FHDB_Shutdown()
        '******************************************************************
        '***    Procedure to check for an Emergency Shutdown.           ***
        '***    If Emergency Shutdown occured, bShutdownFail            ***
        '***    is set to TRUE and and ERROR code is Assigned           ***
        '***    to dShutdownCode in the Emergency Shutdown              ***
        '***    sequence.                                               ***
        '***    The RUN_TIME key in the INI is assigned the same        ***
        '***    value as the SYSTEM_SHUTDOWN key. This will be an       ***
        '***    indicator that a fully mature shutdown sequence         ***
        '***    was completed on Shutdown.                              ***
        '***                                                            ***
        '******************************************************************

        Dim sShutDown_Time As String 'System Shut Down Time

        'On System Shutdown
        frmSysMon.tmrUpdateStop.Enabled = False 'Disable Update Timer
        sShutDown_Time = ValidatedDate("System Startup", "SYSTEM_SHUTDOWN", "Update_Now")

        'Test for Abnormal Shutdown
        If bShutdownFail = True Then 'If True, Shutdown FAILED
            WriteData(CDate(sPresent_Startup_Time), DateTime.Now.ToString, END_IT, , CStr(dShutdownCode), F_STOP_EMER & vbCrLf & sFaultCallout)
        Else            'Normal Shutdown
            WriteData(CDate(sPresent_Startup_Time), DateTime.Now.ToString, END_IT, Pass)
        End If

        'Set RUN_TIME to same value as SYSTEM_SHUTDOWN to mark as a fully mature shutdown
        UpdateIniFile("FHDB", "RUN_TIME", sShutDown_Time)

        'Reset Variables to Default
        dShutdownCode = 0
        bShutdownFail = False

    End Sub



    Sub FHDB_Startup()
        '******************************************************************
        '***    Procedure to check for an Abnormal Shutdown.            ***
        '***    If Shutdown Time is not Equal to Run Time               ***
        '***    Then an Abnormal Shutdown occured.                      ***
        '***    This procedure will alert the Operator and write an     ***
        '***    Abnormal Shutdown Record to the FHDB.                   ***
        '***    A Start Record will then be written to FHDB.            ***
        '***    The System Effective Date will be checked to            ***
        '***    determine if Calibration is Due.                        ***
        '***    If Cal Due it will be indicated on Start record.        ***
        '***                                                            ***
        '******************************************************************

        Dim sShutDown_Time As String 'System Shut Down Date/Time
        Dim sRunTime As String 'System Running Date/Time
        Dim sCalDue As String 'System Efective Date/Time
        Dim sStartupTemp As String 'Primary Chassis Temp from the VIPERT.ini File on Startup


        'Check for a Valid value in the VIPERT.ini file for the Primary Chassis Temp
        sStartupTemp = GatherIniFileInformation("System Monitor", "PRI_TEMP", "")
        'If value is not valid, Set to a Default of 0
        If Not IsNumeric(sStartupTemp) Then
            UpdateIniFile("System Monitor", "PRI_TEMP", 0)
        End If

        sShutDown_Time = ValidatedDate("System Startup", "SYSTEM_SHUTDOWN")
        If sShutDown_Time = Convert.ToString(DEFAULT_DATE) Then bRestoredSystem = True 'Set flag to indicate a Restored System

        sRunTime = ValidatedDate("FHDB", "RUN_TIME", "")
        If sRunTime = Convert.ToString(DEFAULT_DATE) Then
            UpdateIniFile("FHDB", "RUN_TIME", "") 'Write to INI file
            bRestoredSystem = True 'Set flag to indicate a Restored System
        End If


        sCalDue = GatherIniFileInformation("Calibration", "SYSTEM_EFFECTIVE", "")
        sCalDue = CheckForDate(sCalDue) 'Vaildate Date


        'If system is not being restored then test for an Abnormal Shutdown
        If bRestoredSystem = False Then
            'Test for Abnormal Shutdown
            'During a Normal Shutdown The RunTime value is set to the Shutdown Time value, This
            'indicates a fully mature shut took place.
            If CDate(sShutDown_Time).ToOADate <> CDate(sRunTime).ToOADate Then 'If Shutdown time does not equal Runtime, Shutdown was abnormal
                MsgBox("System was not shut down properly." & vbCrLf & "A synthesized, abnormal shut down record will be written to the FHDB." & vbCrLf & "Time of Startup: " & sLastStartupTime & vbCrLf & "Time of Shutdown: " & sRunTime, , "ABNORMAL SHUTDOWN")

                WriteData(sLastStartupTime, sRunTime, END_IT, , , F_START_AB)
            End If
        End If

        'Write Start Record
        If Now.ToOADate > CDate(sCalDue).ToOADate Then 'Check for Cal Due, if True, Start event FAILED
            WriteData(sPresent_Startup_Time, Now, START, , , F_START_CAL)
        Else            'Start event PASSED
            WriteData(sPresent_Startup_Time, Now, START, Pass)
        End If

    End Sub


    Sub InitFHDBData()

        '******************************************************************
        '***    Procedure to Inialize Data Variables to be              ***
        '***    Passed to the FHDB.                                     ***
        '***                                                            ***
        '******************************************************************

        'Initialize Data variable Defaults not used
        sERO = " "
        sUUT_Rev = " "
        sID_Serial_No = " "

        'Get the last System Startup Time from the VIPERT.INI file
        sLastStartupTime = GatherIniFileInformation("System Startup", "SYSTEM_STARTUP", "")
        sLastStartupTime = CheckForDate(sLastStartupTime) 'Vaildate Date
        'Set the current System Startup Time
        UpdateIniFile("System Startup", "SYSTEM_STARTUP", "")
        'Get the Start Time just entered and assign value to Global Variable
        sPresent_Startup_Time = GatherIniFileInformation("System Startup", "SYSTEM_STARTUP", "")
        sPresent_Startup_Time = CheckForDate(sPresent_Startup_Time) 'Vaildate Date

    End Sub



    Sub LaunchFHDB()
        '**************************************************************************************
        '***    Gets the FHDB Processor file path, checks for file existance,               ***
        '***    the launches FHDB.exe.                                                      ***
        '**************************************************************************************

        Dim sFHDBFilePath As String 'Drive\Path\Name for the FHDB Database
        Dim nRetVal As Integer 'Return value for the Shell Function

        On Error Resume Next 'If an Error Occurs, Resume code Execution
        'Find File location from the VIPERT.ini File
        sFHDBFilePath = GatherIniFileInformation("File Locations", "FHDB_PROCESSOR", "")

        If FileExists(sFHDBFilePath) Then 'If File is found, Launch it
            nRetVal = Shell(sFHDBFilePath, AppWinStyle.NormalNoFocus)
        Else            'If the file can't be found, let the Operator know
            MsgBox("File Not Found: " & sFHDBFilePath, MsgBoxStyle.Critical)
        End If

    End Sub




    
    
    Sub WriteData(Optional ByVal sStart_Time As String = "#12:00:00 AM#", Optional ByVal sStop_Time As String = "#12:00:00 AM#", Optional ByVal sTPCCN As String = " ", Optional ByVal nTest_Status As Integer = 0, Optional ByVal sFailure_Step As String = " ", Optional ByVal sFault_Callout As String = " ", Optional ByVal dMeas_Value As Double = 0, Optional ByVal sDimension As String = " ", Optional ByVal dUpper_Limit As Double = 0, Optional ByVal dLower_Limit As Double = 0, Optional ByVal sOperator_Comments As String = " ")

        '******************************************************************
        '***    Procedure to Pass Data to the FHDB and return           ***
        '***    an ERROR Code.                                          ***
        '***    **This Function uses the FHDBDriver.DLL**               ***
        '***    Data variables are then reinitialized.                  ***
        '***                                                            ***
        '******************************************************************

        Dim nErrCode As Integer 'Returned Error Code from FHDB

        'Write Data to the FHDB
        nErrCode = objData.SaveData(sStart_Time, sStop_Time, sERO, sTPCCN, sUUT_Serial_No, sUUT_Rev, sID_Serial_No, nTest_Status, sFailure_Step, sFault_Callout, dMeas_Value, sDimension, dUpper_Limit, dLower_Limit, sOperator_Comments)

    End Sub




    
    Public Sub UpdateIniFile(ByVal AppName As String, ByVal Key As String, ByVal IniInfo As String)
        '**************************************************************************************
        '***    Revised function to allow AppName, Key, and IniInfo to be passed.           ***
        '***    If IniInfo is an empty string "", then the Time Now is Updated              ***
        '***    in the Location specified.                                                  ***
        '**************************************************************************************

        
        Dim ReturnValue As Integer 'API Returned Eror Code
        Dim lpFileName As String 'File to write to (VIPERT.INI)
        Dim lpKeyName As String 'KEY= in INI file
        Dim lpDefault As String 'Default value if key not found
        Dim lpApplicationName As String '[Application] in INI file
        
        Dim DateSerVal As Single 'Date Serial Code
        
        Dim TimeSerVal As Single 'Time Serial Code
        
        Dim DisplaySerVal As Double

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        lpApplicationName = AppName
        lpKeyName = Key

        If IniInfo = "" Then
            'Init File
            DateSerVal = DateSerial(Year(Now), Month(Now), DateAndTime.Day(Now)).ToOADate
            TimeSerVal = TimeSerial(Hour(Now), Minute(Now), Second(Now)).ToOADate
            DisplaySerVal = DateSerVal + TimeSerVal
            lpDefault = VB6.Format(DisplaySerVal, "0.00000")
        Else
            lpDefault = IniInfo
        End If

        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpFileName)

    End Sub




    
    Function CheckForDate(ByVal sDate As String) As String
        CheckForDate = ""
        '*****************************************************************************
        '** This function checks for a date value, If Length is over 4 Characters   **
        '** It may be a Date. Convert it to a Windows format and send it back,      **
        '** Else send back a blank.                                                 **
        '*****************************************************************************
        Dim dGetTime As String
 
        If Len(sDate) > 4 Then
            CheckForDate = DateTime.FromOADate(sDate)
        Else
            CheckForDate = Convert.ToString(DEFAULT_DATE)
        End If

    End Function




    Function ValidatedDate(ByVal sAppName As String, ByVal sKey As String, Optional ByVal sValue As String = "", Optional ByVal bDelayIt As Boolean = False) As String
        ValidatedDate = ""
        '*****************************************************************************
        '** This function checks for a valid date in the VIPERT.ini. If the date is   **
        '** invalid it is assigned a new value, (either a pass value or NOW)        **
        '** This value is written to the VIPERT.ini file and then read back.          **
        '*****************************************************************************

        Dim sDate As String 'Date string being validated

        sDate = GatherIniFileInformation(sAppName, sKey, "")
        sDate = CheckForDate(sDate) 'Vaildate Date

        If sDate = DEFAULT_DATE.ToOADate.ToString Then
            If bDelayIt Then Delay(2)
            UpdateIniFile(sAppName, sKey, sValue) 'Write to INI file
            sDate = GatherIniFileInformation(sAppName, sKey, "")
        End If

        If sValue = "Update_Now" Then
            UpdateIniFile(sAppName, sKey, "") 'Write to INI file
            sDate = GatherIniFileInformation(sAppName, sKey, "")
        End If

        ValidatedDate = sDate

    End Function


End Module