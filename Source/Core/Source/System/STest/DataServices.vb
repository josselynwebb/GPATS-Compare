'Option Strict Off
'Option Explicit On

Imports System.Text
Imports FHDB

Public Module DataServices


    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  E1445A ARB Test                           *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This "DataServices" : STest               *
    '*                : To provide FHDB functionality for STest.  *
    '**************************************************************


    'Define Data variables to pass to the FHDB
    Public sERO As String = " " 'ERO field value  (Char Len 5)
    Public sTPCCN As String = "" 'TPCCN field value  (Char Len 16)
    Public sUUT_Serial_No As String = "" 'UUT Serial Number field value  (Char Len 15)
    Public sUUT_Rev As String = "" 'UUT Revision field value  (Char Len 10)
    Public sID_Serial_No As String = "" 'ID Serial Number field value  (Char Len 10)
    Public sSerial_No As String = "" 'ATS ATE Serial Number
    Public nErrCode As Integer = 0 'Error return code from DLL
    Public dtTestStartTime As Date = Now 'Date/Time of the Start of Self Test
    Public bTestFailed As Boolean = False 'Flag to indicate Self Test Failed
    Public bTestHasBeenPerformed As Boolean = False 'Flag to indicate a test was performed
    Public sGeneralInfo As String = "" 'For General Messages to add to FaultCallout  '03/15/02


    Public objData As New FHDB.DLLClass

    'Used to Reference the VB DLL (FHDBDriver.DLL) as an Object
    'NOTE: It must be Referenced in the Project References section

    '-----------------------------------------------------------------
    '---                    Constants                              ---
    '-----------------------------------------------------------------

    Public Const TEST As String = "SELF TEST" 'Identifies Test for the TPCCN field
    Public Const FAIL As String = "System Self Test Failed"

    Public Sub EndData()
        '********************************************************************************
        '***          End Record for the System Self Test.                            ***
        '***          If all Self Tests Passed, This will be the only record.         ***
        '***                                                             06/20/2001   ***
        '********************************************************************************
        'Declare Procedure Level Variables

        If (bTestHasBeenPerformed) And (Not bTestFailed) Then 'If test ran and no failures
            WriteData(CStr(dtTestStartTime), CStr(Now), 1)
        End If

    End Sub

    Public Sub LogFHDBTestPassed(ByVal TestName As String)
        '********************************************************************************
        '***          End Record for individual Instrument tests.                     ***
        '********************************************************************************


        WriteData(CStr(dtTestStartTime), CStr(Now), 1, "", TestName)

    End Sub




    Public Sub InitData()
        '********************************************************************************
        '***                    Initialize FHDB Data variables.                       ***
        '***                                                            06/20/2001    ***
        '********************************************************************************
        Dim File_Error As Integer
        dtTestStartTime = Now 'Set Self Test Start Time

        sERO = " " 'Initialize unused String variables w/Defaults
        sUUT_Rev = " "
        sID_Serial_No = " "

        'Get ATS Serial Number from the ATS.INI File
        File_Error = GetPrivateProfileString("System Startup", "SN", "NULL", lpBuffer, 256, sATS_INI)
        sUUT_Serial_No = StripNullCharacters(lpBuffer)

        sTPCCN = TEST 'Set TPCCN value to "SELF TEST"

    End Sub

    Sub WriteData(sStart_Time As String, Optional sStop_Time As String = "#12:00:00 AM#", Optional lTest_Status As Integer = 0, Optional sFailure_Step As String = "", Optional sFault_Callout As String = "",
                  Optional dMeas_Value As Double = 0, Optional sDimension As String = "", Optional dUpper_Limit As Double = 0, Optional dLower_Limit As Double = 0,
                  Optional sOperator_Comments As String = "")
        '********************************************************************************
        '***        Procedure to Pass Data to the FHDB and return an ERROR Code.      ***
        '***        All Variables are Optional, if no value, assign Default.          ***
        '***        Total Test Flag is Set if any test fails.                         ***
        '***                                                     07/09/2001           ***
        '********************************************************************************
        ' example:
        ' Call WriteData(CStr(dtTestStartTime), CStr(Now), 0, sFailStep, MeasComment,
        ''        val(Measurment#), Units, val(HighLimitIn#), val(LowLimitIn#),
        ''        sOperatorComments)
        '********************************************************************************
        'Write Data to the FHDB

        nErrCode = objData.SaveData(sStart_Time, sStop_Time, sERO, sTPCCN, sUUT_Serial_No, sUUT_Rev, sID_Serial_No, lTest_Status, sFailure_Step, sFault_Callout, dMeas_Value, sDimension, dUpper_Limit,
                                    dLower_Limit, sOperator_Comments)

        'This sub is returning an error for an unknown reason
        If Err.Number <> 0 Then
            '   MsgBox("Error: " & CStr(Err.Number) & Err.Description)
            Err.Clear()
        End If

    End Sub


End Module