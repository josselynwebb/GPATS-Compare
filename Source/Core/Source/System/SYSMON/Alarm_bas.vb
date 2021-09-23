'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Text
Imports Microsoft.VisualBasic

Public Module AlarmCondition


	'=========================================================
    '**************************************************************
    '**************************************************************
    '**                                                          **
    '** Nomenclature  : System Monitor  [AlarmCondition]         **
    '** Purpose       : This module handles the system alarm     **
    '**                 procedures and events for the VIPERT.      **
    '** Module Begins Execution In  --Sub:Warning--              **
    '**************************************************************
    '**************************************************************
    'added for reciever switch and opening instrument outputs
    Const ARB As Short = 1
    Const DTI As Short = 2
    Const FGEN As Short = 3
    Const RFSTIM As Short = 4
    Const SR As Short = 5
    Const PPU As Short = 6

    Const NUM_OF_INSTR As Short = 6
    Const FIRST_INSTRUMENT = ARB
    Const LAST_INSTRUMENT = SR

    'array used to store the instrument name
    Dim Instr_Hand(NUM_OF_INSTR) As String
    Public Instr_Hand_DTI As Integer
    Public Pin_array1(64) As Integer
    Public Pin_array2(64) As Integer
    Public Pin_array3(64) As Integer

    Public Const TERM9_RELAY_OPEN As Short = 161
    Public Const TERM9_SCOPE_LAST As Integer = 65535
    Public Const TERM9_SCOPE_FIRST_USER As Short = 0
    Public Const TERM9_SCOPE_LAST_USER As Integer = 32767
    Public Const TERM9_SCOPE_FIRST_PREDEFINED As Integer = 32768
    Public Const TERM9_SCOPE_LAST_PREDEFINED As Integer = 65535
    Public Const TERM9_SCOPE_FIRST_UC_INDEX As Integer = 33068
    Public Const TERM9_SCOPE_LAST_UC_INDEX As Integer = 33167
    Public Const TERM9_SCOPE_FIRST_SR_INDEX As Integer = 33168
    Public Const TERM9_SCOPE_LAST_SR_INDEX As Integer = 33267
    Public Const TERM9_SCOPE_FIRST_CARD_INDEX As Integer = 32868
    Public Const TERM9_SCOPE_LAST_CARD_INDEX As Integer = 33067
    Public Const TERM9_SCOPE_FIRST_CHNGRP_INDEX As Integer = 33768
    Public Const TERM9_SCOPE_LAST_CHNGRP_INDEX As Integer = 33967
    Public Const TERM9_SCOPE_FIRST_PIN_INDEX As Integer = 33968
    Public Const TERM9_SCOPE_LAST_PIN_INDEX As Integer = 65535
    Public Const TERM9_SCOPE_UNDEFINED As Short = -1
    Public Const TERM9_SCOPE_ISGROUP As Short = -2
    Public Const TERM9_SCOPE_SYSTEM As Integer = 32768
    Public Const TERM9_SCOPE_CHANSYSTEM As Integer = 32769




    'ps handles to go thru cicle
    Dim Ps_Hand(10) As String

    'dimed variable for this part of code
    Dim Instrument_Index As Short
    Dim retCount As Integer
    Dim SystErr As Integer
    Public Status As Integer
    Public viSession As Integer
    Dim PPU_Index As Short
    Dim TestCommandData As String
    Dim Supply As Short
    Dim PPU_msg As String
    Public Pin_Index As Integer

    Dim pinListCount As Integer

    'pass/fail flags for instruments durring alarm
    Public ARB_pass As Boolean
    Public FG_pass As Boolean
    Public DTI_pass As Boolean
    Public RFS_pass As Boolean
    Public SR_pass As Boolean
    Public PPU_pass As Boolean


    '-----------------API / DLL Declarations------------------------------
    'Used For Bringing An Application To the top of the Windows "Z-Order"
    'Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    'Used for Hunting and Killing Applications
    Declare Function GetWindow Lib "user32" (ByVal hWnd As Integer, ByVal wCmd As Integer) As Integer
    Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    '---- API Declarations and Constants for Sub Procedure CloseAbnormalShutdownMsgBx() ----
    Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal msg As Integer, ByRef wParam As Integer, ByRef lParam As Integer) As Integer
    Public Const BM_CLICK As Short = &HF5
    Public Declare Function SetActiveWindow Lib "user32.dll" (ByVal hWnd As Integer) As Integer
    Public Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32.dll" Alias "FindWindowExA" (ByVal hwndParent As Integer, ByVal hwndChildAfter As Integer, ByVal lpszClass As Integer, ByVal lpszWindow As String) As Integer
    '-----------------Global Constants------------------------------------
    Public Const GW_Child As Short = 5 'API Constant for Changing a Window "Z-Order"
    Public Const GW_HWNDFIRST As Short = 0 'API Constant for Changing a Window "Z-Order"
    Public Const GW_HWNDLAST As Short = 1 'API Constant for Changing a Window "Z-Order"
    Public Const GW_HWNDNEXT As Short = 2 'API Constant for Changing a Window "Z-Order"
    Public Const GW_HWNDPREV As Short = 3 'API Constant for Changing a Window "Z-Order"
    Public Const GW_OWNER As Short = 4 'API Constant for Changing a Window "Z-Order"
    '-----------------User Defined Types----------------------------------

    Structure typWarning 'Type Defined to hold User Warning Information in a QUEUE
        Dim Topic As String 'TOPIC Heading (Type of Warning)
        Dim Component As String 'COMPONENT that is causing a problem
        Dim Condition As String 'Description of the Problem/Error
    End Structure
    '-----------------Global Variables------------------------------------
    Public WarningQueue(26) As typWarning 'Holds warning information in memory
    Public QueuePointer As Short 'Index of current warning
    Public QueueCounter As Short 'Number of Warnings in Queue
    Public OverflowPointer As Short 'This Keeps track of Queue Overflows
    '-----------------Local Variables-------------------------------------
    Dim AcquisitionTimer1 As Short 'Timer For Temperture Warning Timer
    Dim AcquisitionTimer2 As Short 'Timer For Temperture Warning Timer
    Dim FpuVRefLevel(6,2) As Single '2-D Index Of (FPU Volt Levels,LowHighDefValues)
    Dim FpuCRefLevel(6) As Single 'Index Of (FPU Current alarm Levels)

    
    Dim CommunicationFailFlag As Short 'TRUE if a communication failure is detected ,FALSE if not or failure cleared


    Function TERM9_SCOPE_CARD(ByVal n As Short) As Integer
        TERM9_SCOPE_CARD = TERM9_SCOPE_FIRST_CARD_INDEX+n
    End Function



    Function TERM9_SCOPE_CHAN(ByVal n As Short) As Integer
        TERM9_SCOPE_CHAN = TERM9_SCOPE_FIRST_PIN_INDEX + n
    End Function



    Function TERM9_SCOPE_CHANCARD(ByVal n As Short) As Integer
        TERM9_SCOPE_CHANCARD = TERM9_SCOPE_FIRST_CHNGRP_INDEX + n - 1
    End Function



    Function ChassisCommunication(ByVal Chassis As Short) As Short
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     Verifies that the chassis controllers are returning  *
        '*     data.  If the All chassis temp sensors return NULL   *
        '*     bytes (-45 after formatting) then we can assume that *
        '*     that particular chassis is not communication.        *
        '*    RETURNS:                                              *
        '*     TRUE if Communication is Valid, FALSE if error       *
        '*    EXAMPLE:                                              *
        '*      Status% = ChassisCommunication(PRI_CHASS)           *
        '************************************************************
        
        Dim CommunicationFlag As Short 'Status of chassis communication test
        Dim ChassisSlot As Short 'Chassis Slot Index

        'Set Flag to TRUE Until proven False
        CommunicationFlag = True

        If Chassis = 1 Then 'Check Primary Chassis
            For ChassisSlot = 0 To 12
                If PrimaryChassis.Temperature!(ChassisSlot) <> (-45) Then
                    CommunicationFlag = False
                    GoTo ReturnCommunicationValue 'Report Good Communication
                End If
            Next ChassisSlot
            If PrimaryChassis.IntakeTemperature <> (-45) Then
                CommunicationFlag = False
                GoTo ReturnCommunicationValue 'Report Good Communication
            End If
        Else            'Check Secondary Chassis
            For ChassisSlot = 0 To 12
                If SecondaryChassis.Temperature!(ChassisSlot) <> (-45) Then
                    CommunicationFlag = False
                    GoTo ReturnCommunicationValue
                End If
            Next ChassisSlot
            If SecondaryChassis.IntakeTemperature <> (-45) Then
                CommunicationFlag = False
                GoTo ReturnCommunicationValue
            End If
        End If

ReturnCommunicationValue:
        'Return Function Value
        ChassisCommunication = CommunicationFlag

    End Function



    Sub SetFpuTolerances()

        Dim FpuVal(6) As Single
        Dim FpuWVal(6) As Single 'Max watts of each supply defined in Freedom PDU spec
        Dim SupplyFpuIndex As Short 'Index of FPU supply

        FpuVal(0) = 24
        FpuVal(1) = 12
        FpuVal(2) = 5
        FpuVal(3) = -2
        FpuVal(4) = -5.2
        FpuVal(5) = -12
        FpuVal(6) = -24

        FpuWVal(0) = 162 'max power 24V
        FpuWVal(1) = 150 'max power 12V
        FpuWVal(2) = 300 'max power 5V
        FpuWVal(3) = 56 'max power -2V
        FpuWVal(4) = 156 'max power -5.2V
        FpuWVal(5) = 75 'max power -12V
        FpuWVal(6) = 150 'max power -24V

        'Fix for new Teradyne DTS
        'FpuWVal(0) = 200 'max power 24V
        'FpuWVal(1) = 150 'max power 12V
        'FpuWVal(2) = 300 'max power 5V
        'FpuWVal(3) = 56 'max power -2V
        'FpuWVal(4) = 156 'max power -5.2V
        'FpuWVal(5) = 93 'max power -12V
        'FpuWVal(6) = 150 'max power -24V

        'Find Low
        For SupplyFpuIndex = 0 To 6
            FpuVRefLevel(SupplyFpuIndex, 0) = (Math.Abs(FpuVal(SupplyFpuIndex))) - (Math.Abs(FpuVal(SupplyFpuIndex)) * 0.3)
        Next SupplyFpuIndex
        'Find High
        For SupplyFpuIndex = 0 To 6
            FpuVRefLevel(SupplyFpuIndex, 1) = (Math.Abs(FpuVal(SupplyFpuIndex))) + (Math.Abs(FpuVal(SupplyFpuIndex)) * 0.3)
            FpuCRefLevel(SupplyFpuIndex) = (Math.Abs(FpuWVal(SupplyFpuIndex))) / (Math.Abs(FpuVal(SupplyFpuIndex)))
        Next SupplyFpuIndex
        'Find Def
        For SupplyFpuIndex = 0 To 6
            FpuVRefLevel(SupplyFpuIndex, 2) = FpuVal(SupplyFpuIndex)
        Next SupplyFpuIndex

    End Sub



    
    Function TerminateAtlasRts() As Short
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     Checks for the PAWS ATLAS RTS On the desktop, and if *
        '*     running terminates the application ("WM_CLOSE" level)*
        '*     This Function ADDED in Version 1.11 DWH              *
        '*    RETURNS:                                              *
        '*     TRUE (If found and terminated)                       *
        '*     FALSE (If application is not running)                *
        '*    EXAMPLE:                                              *
        '*     AtlasRunning%=TerminateAtlasRts()                    *
        '************************************************************
        Dim CurrWnd As Integer 'Current Window Handle
        Dim Title As String 'Title of window found
        Dim WindowHandle As Integer 'Registersd Windows Operating System Handle
        Dim rc As Integer 'Return Value of Windows API Call
        
        Dim lpString As String = Space(255) 'Buffer for WIndow text in API call
        Dim Ret As Integer 'Window Text Error Code
        Dim ParentHandle As Integer 'RTS Window Handle

        Dim PAWSRtsProcesses() As Process = Process.GetProcessesByName("Wrts")
        If (PAWSRtsProcesses.Length > 0) Then
            Dim i As Integer = 0
            While (i < PAWSRtsProcesses.Length())
                PAWSRtsProcesses(i).Kill()
                i += 1
            End While
            Return True
        End If

        'If no WRTS processes, return false
        Return False

    End Function



    Sub VerifyChassisStatus()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     Verifies that the chassis controllers are operating  *
        '*     without error. The rountine also verifies that the   *
        '*     Fans are not blocked or malfunctioning detected by   *
        '*     the Air Flow Sensors                                 *
        '*    EXAMPLE:                                              *
        '*      VerifyChassisStatus                                 *
        '************************************************************
        Static FailCount(9) As Short 'Number of consecutive failures
        Dim FailIndex As Short 'Element that is failing
        Dim ChassisNumber As Short 'Chassis being tested
        Dim ChassisName As String 'User to report the failure to the user
        Dim ChasFanspeed As Short 'The Reported Chassis Fanspeed
        Const FAN_THRESH As Short = 75 'Any Airflow sensor events below this threshold will not cause a fault
        Const FAIL_TMO As Short = 60 'The Number of consecutive fails necessary to cause a fault

        FailIndex = 0 'Init Fail Array Index
        For ChassisNumber = 1 To 2 'Check Both Chassis
            If ChassisNumber = 1 Then
                CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
                ChasFanspeed = PrimaryChassis.FanSpeed
                ChassisName = "Primary VXI Chassis "
            Else
                CheckChassisStatusByte(SecondaryChassis.ChassisStatus)
                ChasFanspeed = SecondaryChassis.FanSpeed
                ChassisName = "Secondary VXI Chassis "
            End If

            'Check For Communication Error / Disconnected Cable
            If ChassisCommunication(ChassisNumber) = True Then
                FailCount(FailIndex) += 1
                If FailCount(FailIndex) >= FAIL_TMO / 4 Then
                    FailCount(FailIndex) = 0 'Reset Fail Count
                    Warning("Communication Error", ChassisName & "Power Cable J1", 3002)
                    CommunicationFailFlag = True
                End If
            Else
                FailCount(FailIndex) = 0
            End If
            FailIndex += 1

            'Check Self Test
            If StatSelfTest = 0 Then 'FAIL
                FailCount(FailIndex) += 1
                If FailCount(FailIndex) >= FAIL_TMO Then
                    FailCount(FailIndex) = 0 'Reset Fail Count
                    Warning("Chassis Control", ChassisName, 2004)
                End If
            Else                'PASS
                FailCount(FailIndex) = 0 'Reset Flag
            End If
            FailIndex += 1

            'Air Flow Sensors
            If StatAirFlowSensor1 = 0 Then 'AFS3
                FailCount(FailIndex) += 1
                If FailCount(FailIndex) >= FAIL_TMO Then
                    FailCount(FailIndex) = 0 'Reset Fail Count
                    Warning("Airflow", ChassisName & "Air Flow Sensor 1", 2003)
                End If
            Else                'PASS
                FailCount(FailIndex) = 0 'Reset Flag
            End If
            FailIndex += 1
            If StatAirFlowSensor2 = 0 Then 'AFS2
                FailCount(FailIndex) += 1
                If FailCount(FailIndex) >= FAIL_TMO Then
                    FailCount(FailIndex) = 0 'Reset Fail Count
                    Warning("Airflow", ChassisName & "Air Flow Sensor 2", 2003)
                End If
            Else                'PASS
                FailCount(FailIndex) = 0 'Reset Flag
            End If
            FailIndex += 1
            If StatAirFlowSensor3 = 0 Then 'AFS3
                FailCount(FailIndex) += 1
                If FailCount(FailIndex) >= FAIL_TMO Then
                    FailCount(FailIndex) = 0 'Reset Fail Count
                    Warning("Airflow", ChassisName & "Air Flow Sensor 3", 2003)
                End If
            Else                'PASS
                FailCount(FailIndex) = 0 'Reset Flag
            End If
            FailIndex += 1

        Next ChassisNumber

        'Reset Fail Flag if applicable
        If ChassisCommunication(1) = True And ChassisCommunication(2) = True Then
            CommunicationFailFlag = False
        End If

    End Sub



    Sub VerifyChassisVoltLevels()

        Dim FpuVMeasured(6, 2) As Single 'Chassis Current Levels
        Dim SupplyFpuIndex As Short 'Chassis Level Index
        Dim ChassisIndex As Short 'Chassis Number Index
        Dim ChasDesc As String 'Description of failing chassis
        Static FailCount(6, 2) As Short 'Number of consecutive failures
        Const FAIL_TMO As Short = 30 'The Number of consecutive fails necessary to cause a fault

        'If No Voltage then Exit Sub
        If AllPowerOff Then Exit Sub

        'Get Voltages
        FpuVMeasured(0, 1) = PrimaryChassis.ChP24V
        FpuVMeasured(1, 1) = PrimaryChassis.ChP12V
        FpuVMeasured(2, 1) = PrimaryChassis.ChP5V
        FpuVMeasured(3, 1) = PrimaryChassis.ChN2V
        FpuVMeasured(4, 1) = PrimaryChassis.ChN52V
        FpuVMeasured(5, 1) = PrimaryChassis.ChN12V
        FpuVMeasured(6, 1) = PrimaryChassis.ChN24V
        FpuVMeasured(0, 2) = SecondaryChassis.ChP24V
        FpuVMeasured(1, 2) = SecondaryChassis.ChP12V
        FpuVMeasured(2, 2) = SecondaryChassis.ChP5V
        FpuVMeasured(3, 2) = SecondaryChassis.ChN2V
        FpuVMeasured(4, 2) = SecondaryChassis.ChN52V
        FpuVMeasured(5, 2) = SecondaryChassis.ChN12V
        FpuVMeasured(6, 2) = SecondaryChassis.ChN24V

        If FPU_IgnoreFaults = False Then
            'Check For Shutdown Condition
            For ChassisIndex = 1 To 2
                For SupplyFpuIndex = 0 To 6
                    If FpuVMeasured(SupplyFpuIndex, ChassisIndex) <> 0 Then
                        GoTo VoltageCheck
                    End If
                Next SupplyFpuIndex
            Next ChassisIndex

            Exit Sub

VoltageCheck:
            'Check for Voltages to deviate 30%
            For ChassisIndex = 1 To 2
                For SupplyFpuIndex = 0 To 6
                    If FpuVMeasured(SupplyFpuIndex, ChassisIndex) < FpuVRefLevel(SupplyFpuIndex, 0) Or FpuVMeasured(SupplyFpuIndex, ChassisIndex) > FpuVRefLevel(SupplyFpuIndex, 1) Then
                        'Fail  Out of tolerance
                        FailCount(SupplyFpuIndex, ChassisIndex) += 1
                        If FailCount(SupplyFpuIndex, ChassisIndex) > FAIL_TMO Then
                            FailCount(SupplyFpuIndex, ChassisIndex) = 0 'Reset Count
                            If ChassisIndex = 1 Then
                                ChasDesc = "Primary VXI Chassis "
                            Else
                                ChasDesc = "Secondary VXI Chassis "
                            End If
                            TerminateInstrumentApplications()
                            Application.DoEvents()
                            SetFpu(False) 'Shutdown FPU
                            ResetPPU(Check:=True) 'Reset PPU Power Supplies
                            Warning("Instrument Power", ChasDesc & "Backplane: " & Str(FpuVRefLevel(SupplyFpuIndex, 2)) & " Volt Supply", 1006)
                        End If
                    Else
                        FailCount(SupplyFpuIndex, ChassisIndex) = 0
                    End If
                Next SupplyFpuIndex
            Next ChassisIndex
        End If
    End Sub

    Sub VerifyChassisCurrentLevels()

        Dim FpuCMeasured(6, 2) As Single 'Chassis Current Levels
        Dim SupplyFpuIndex As Short 'Chassis Level Index
        Dim ChassisIndex As Short 'Chassis Number Index
        Dim ChasDesc As String 'Description of failing chassis
        Static FailCount(6, 2) As Short 'Number of consecutive failures
        Const FAIL_TMO As Short = 30 'The Number of consecutive fails necessary to cause a fault

        'If No Voltage then Exit Sub
        If AllPowerOff Then Exit Sub

        'Get FPU Currents
        FpuCMeasured(0, 1) = PrimaryChassis.P24ACurrent
        FpuCMeasured(1, 1) = PrimaryChassis.P12ACurrent
        FpuCMeasured(2, 1) = PrimaryChassis.P5ACurrent
        FpuCMeasured(3, 1) = PrimaryChassis.N2ACurrent
        FpuCMeasured(4, 1) = PrimaryChassis.N52ACurrent
        FpuCMeasured(5, 1) = PrimaryChassis.N12ACurrent
        FpuCMeasured(6, 1) = PrimaryChassis.N24ACurrent
        FpuCMeasured(0, 2) = SecondaryChassis.P24BCurrent
        FpuCMeasured(1, 2) = SecondaryChassis.P12BCurrent
        FpuCMeasured(2, 2) = SecondaryChassis.P5BCurrent
        FpuCMeasured(3, 2) = SecondaryChassis.N2BCurrent
        FpuCMeasured(4, 2) = SecondaryChassis.N52BCurrent
        FpuCMeasured(5, 2) = SecondaryChassis.N12BCurrent
        FpuCMeasured(6, 2) = SecondaryChassis.N24BCurrent

        If FPU_IgnoreFaults = False Then
            'Check For Shutdown Condition
            For ChassisIndex = 1 To 2
                For SupplyFpuIndex = 0 To 6
                    If FpuCMeasured(SupplyFpuIndex, ChassisIndex) <> 0 Then
                        GoTo CurrentCheck
                    End If
                Next SupplyFpuIndex
            Next ChassisIndex

            Exit Sub

CurrentCheck:
            'Check for Max Current Condition
            For ChassisIndex = 1 To 2
                For SupplyFpuIndex = 0 To 6
                    If FpuCMeasured(SupplyFpuIndex, ChassisIndex) > FpuCRefLevel(SupplyFpuIndex) Then
                        'Fail  Out of tolerance
                        FailCount(SupplyFpuIndex, ChassisIndex) += 1
                        If FailCount(SupplyFpuIndex, ChassisIndex) > FAIL_TMO Then
                            FailCount(SupplyFpuIndex, ChassisIndex) = 0 'Reset Count
                            If ChassisIndex = 1 Then
                                ChasDesc = "Primary VXI Chassis "
                            Else
                                ChasDesc = "Secondary VXI Chassis "
                            End If
                            TerminateInstrumentApplications()
                            Application.DoEvents()
                            SetFpu(False) 'Shutdown FPU
                            ResetPPU(Check:=True) 'Reset PPU Power Supplies
                            Warning("Instrument Power", ChasDesc & "Backplane: " & Str(FpuCRefLevel(SupplyFpuIndex)) & " Volt Supply", 1008)
                        End If
                    Else
                        FailCount(SupplyFpuIndex, ChassisIndex) = 0
                    End If
                Next SupplyFpuIndex
            Next ChassisIndex
        End If
    End Sub

    Sub VerifyFpuSupplyFaults()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     Verifies that the FPU supplies have not faulted due  *
        '*     to over/under voltage conditions .  This routine     *
        '*     also checks if the modules are plugged into the      *
        '*     correct slot in the FPU chassis.                     *
        '*    EXAMPLE:                                              *
        '*      VerifyFpuSupplyFaults                               *
        '************************************************************
        Dim Failure As Short 'Failure error code
        Static FailCount As Short 'Number of consecutive failures
        Const FAIL_TMO As Short = 10 'The Number of consecutive fails necessary to cause a fault

        If FPU_IgnoreFaults = False Then
            'Check For FPU Module Faults
            If FpuStatModFault <> 0 Then
                FailCount += 1
                If FpuStatOffMismnatch = 0 Then 'Voltage Problem
                    If FpuStatOverVolt = 0 Then
                        Failure = 1004 'Under Voltage
                    Else
                        Failure = 1003 'Over Voltage
                    End If
                Else
                    Failure = 3004 'Mismatched Module
                End If
            Else
                FailCount = 0
            End If

            'Check if Valid Failure
            If FailCount >= FAIL_TMO Then
                FailCount = 0
                TerminateInstrumentApplications()
                Delay(1)
                SetFpu(False) 'Shutdown FPU
                ResetPPU(Check:=True) 'Reset PPU Power Supplies
                Warning("Module Fault", "Fixed Power Unit(FPU) Supply " & Str(FpuStatAddress), Failure)
            End If
        End If
    End Sub


    Sub VerifyInputPower()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     Verifies that the Input power alarm has not faulted. *
        '*     If there is a problem, then the firmware will take   *
        '*     care of shutting down.  This module just checks to   *
        '*     see if that event has happened.                      *
        '*    EXAMPLE:                                              *
        '*      VerifyInputPower                                    *
        '************************************************************

        Dim ChassisData As Short 'Chassis Index
        Dim Failure As Short 'Error code
        Static FailCount As Short 'Number of consecutive failures for DC
        Static FailCount1 As Short 'Number of consecutive failures for single phase
        Static FailCount2 As Short 'Number of consecutive failures for 3 phase
        Const FAIL_TMO As Short = 10 'The Number of consecutive fails necessary to cause a fault


        If FPU_IgnoreFaults = False Then
            'Check for Input Power Fault
            'DME Change to check the value of the voltage rather than the status byte to start
            '        alarm condition
            For ChassisData = 1 To 2
                If ChassisData = 1 Then
                    'InputPowerByte% = PrimaryChassis.StatusInputPower%
                    InputPowerByte = PrimaryChassis.DcvLevel
                    Input1PhaseByte = PrimaryChassis.VinSinglePhase
                    Input3PhaseByte = PrimaryChassis.VinThreePhase
                Else
                    'InputPowerByte% = SecondaryChassis.StatusInputPower%
                    InputPowerByte = SecondaryChassis.DcvLevel
                    Input1PhaseByte = SecondaryChassis.VinSinglePhase
                    Input3PhaseByte = SecondaryChassis.VinThreePhase
                End If

                'changed the pass/fail conditions
                'If ((InputPowerByte% And &H6) = 0) Then
                If (PowerStatusDc <> 0) Then 'DC mode
                    If (InputPowerByte > 20 And InputPowerByte < 30) Then
                        FailCount = 0
                        FPU_faulted = False
                        FPU_FORCE_SD = False
                    Else                        'Input Power Fail
                        FailCount += 1
                        If FailCount >= FAIL_TMO Then
                            'If (InputPowerByte% And &H4) = 4 Then 'Fault Low
                            If (InputPowerByte < 20) Then
                                Failure = 1001
                            End If
                            'If (InputPowerByte% And &H2) = 2 Then 'Fault High
                            If (InputPowerByte > 30) Then
                                Failure = 1000
                            End If
                        End If
                    End If
                End If

                'scan for failures in single phase
                If (PowerStatusAc <> 0 And PowerStatusSingle = 0) Then 'AC and Single Phase
                    If (Input1PhaseByte > 108 And Input1PhaseByte < 132) Then
                        FailCount1 = 0
                        FPU_faulted = False
                        FPU_FORCE_SD = False
                    Else                        'Input Single phase Fail
                        FailCount1 += 1
                        If FailCount1 >= FAIL_TMO Then
                            If (Input1PhaseByte < 108) Then 'Fault Low
                                Failure = 1010
                            End If
                            If (Input1PhaseByte > 132) Then 'Fault High
                                Failure = 1009
                            End If
                        End If
                    End If
                End If

                'scan for failures in three phase
                If (PowerStatusAc <> 0 And PowerStatusSingle <> 0) Then 'AC and 3phase
                    If (Input3PhaseByte > 187 And Input3PhaseByte < 228) Then
                        FailCount2 = 0
                        FPU_faulted = False
                        FPU_FORCE_SD = False
                    Else                        'Input three phase Fail
                        FailCount2 += 1
                        If FailCount2 >= FAIL_TMO Then
                            If (Input3PhaseByte < 187) Then 'Fault Low
                                Failure = 1012
                            End If
                            If (Input3PhaseByte > 228) Then 'Fault High
                                Failure = 1011
                            End If
                        End If
                    End If
                End If
            Next ChassisData

            If (FailCount >= FAIL_TMO Or FailCount1 >= FAIL_TMO Or FailCount2 >= FAIL_TMO) Then
                'TerminateInstrumentApplications
                'Delay 5
                ' SetFpu False 'Shutdown FPU
                'ResetPPU Check:=True 'Reset PPU Power Supplies

                If FPU_faulted = False Then 'forces only one error message
                    Warning("Input Power", "Power Distribution Unit (PDU)", Failure)
                    FPU_faulted = True
                End If

                'reset the fail counts
                FailCount = 0
                FailCount1 = 0
                FailCount2 = 0
            End If

            If (PowerStatus28FailHigh = 128 And FPU_FORCE_SD = False) Then
                PDUerrCount += 1
                'firmware faulted FPU condition occurs 3 times DME PCR 308
                'raise error saying "FPU has been shutdown due to input power out of tolerance"
                If PDUerrCount = 3 Then
                    PDUerrCount = 0
                    Failure = 1013
                    Warning("Input Power", "Power Distribution Unit (PDU)", Failure)
                    FPU_FORCE_SD = True
                End If
            End If
        End If
    End Sub

    Sub VerifyPowerBudget()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     Verifies that the system has not exceeded the        *
        '*     maximum available power                              *
        '*    PARAMETERS:                                           *
        '*     NONE                                                 *
        '*    EXAMPLE:                                              *
        '*      VerifyPowerBudget                                   *
        '************************************************************
        Static FailCount As Short 'Number of consecutive failures at 100&
        Static ThresholdFailCount As Short 'Number of consecutive failures for 90% warning
        Dim Info As String 'INI File Information (Power Supply SAIS Panel Caption)
        Dim Terminated As Short 'Successful Program Termination Flag
        Const FAIL_TMO As Short = 30 'The Number of consecutive fails necessary to cause a fault

        If InputPower = 0 Then InputPower = 1 'Divide By Zero Trap
        '90% Failure -- Inform User of Impending Power Overrun.  If user does not
        'heed this warning then corrective action, and increases power use, then
        'component shutdown procedures will occur.
        If UserPowerConsumption / InputPower >= 0.9 Then 'Power Budget Exceeded By User
            ThresholdFailCount += 1
            If ThresholdFailCount >= FAIL_TMO Then 'Warn User
                Warning("Power Requests Near Limit", "Power Distribution Unit (PDU)", 1007)
                ThresholdFailCount = 0 'Reset Fail Count
            End If
        End If

        '100% Failure -- Take Corective Action To Ensere Power Safety Guidelines
        If TotalPowerUsage < POWER_MARGIN Then 'Power Budget Exceeded By User
            FailCount += 1
            If FailCount >= FAIL_TMO Then 'Take Corrective Action
                'If Heaters Are On Then Shut Off
                CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
                If StatHeater2 Then
                    SetHeater(1, False)
                    Exit Sub
                End If
                CheckChassisStatusByte(SecondaryChassis.ChassisStatus)
                If StatHeater2 Then
                    SetHeater(3, False)
                    Exit Sub
                End If
                CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
                If StatHeater1 Then
                    SetHeater(0, False)
                    Exit Sub
                End If
                CheckChassisStatusByte(SecondaryChassis.ChassisStatus)
                If StatHeater1 Then
                    SetHeater(2, False)
                    Exit Sub
                End If

                'If Heaters are off then Reset PPU
                TerminateInstrumentApplications()
                Delay(5)
                SetFpu(False) 'Shutdown FPU
                ResetPPU(Check:=False) 'Reset PPU Power Supplies

                Warning("Excessive Power Request", "Power Distribution Unit (PDU)", 1002)
                'Info$ = GatherIniFileInformation("SAIS", "UUTPS_C", "") 'Get Name
                'If Info$ <> "" Then 'Kill Power Supply Application
                '    Terminated% = TerminateApp32(Trim$(Info$))
                'End If
                FailCount = 0 'Reset Fail Count
            End If
        Else
            FailCount = 0
        End If

    End Sub

    Sub EventReceiverActivation()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     Verifies that the system has not exceeded the        *
        '*     maximum available power                              *
        '*    PARAMETERS:                                           *
        '*     NONE                                                 *
        '*    EXAMPLE:                                              *
        '*      EventReceiverActivation                             *
        '************************************************************
        
        Dim ActiveFlag As Short 'Set if an active power supply is found
        Dim Terminated As Short 'Flag indicating Program Termination Successful
        Dim Info As String 'VIPERT.INI Information
        Dim SupplyLoop As Short 'PPU supply Index

        Try
            'stop data poll timer
            frmSysMon.tmrDataPoll.Enabled = False
            frmSysMon.ETITimer.Enabled = False

            'Check For Active Power Supplies
            ActiveFlag = False
            For SupplyLoop = 1 To 10
                If (PrimaryChassis.SlotVoltage(SupplyLoop) > 0) Or (SecondaryChassis.SlotVoltage(SupplyLoop) > 0) Then
                    ActiveFlag = True 'Found an Active PPU Supply
                    Exit For
                End If
            Next SupplyLoop

            'DME CHANGE TO: open all insturments currently running with saif
            'if stest is running, there is no need for this
            If FindWindow(vbNullString, "Stest.exe") = 0 Then 'stest is not running if findwindow returns 0

                Ps_Hand(1) = "DCPS_1"
                Ps_Hand(2) = "DCPS_2"
                Ps_Hand(3) = "DCPS_3"
                Ps_Hand(4) = "DCPS_4"
                Ps_Hand(5) = "DCPS_5"
                Ps_Hand(6) = "DCPS_6"
                Ps_Hand(7) = "DCPS_7"
                Ps_Hand(8) = "DCPS_8"
                Ps_Hand(9) = "DCPS_9"
                Ps_Hand(10) = "DCPS_10"

                Instr_Hand(ARB) = "ARB_GEN_1"
                Instr_Hand(DTI) = "DTS" 'name needs changed
                Instr_Hand(FGEN) = "FUNC_GEN_1"
                Instr_Hand(RFSTIM) = "RFGEN_1"
                Instr_Hand(SR) = "SRS_1_DS1"
                Instr_Hand(PPU) = "DCP_1"

                pinListCount = 64

                PPU_msg = "PPU_"


                For Instrument_Index = FIRST_INSTRUMENT To NUM_OF_INSTR
                    Select Case Instrument_Index
                        Case ARB
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "ABOR", retCount)
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "*RST", retCount)
                            ARB_pass = True
                            If SystErr <> VI_SUCCESS Then ARB_pass = False

                        Case DTI
                            'command to open dts outputs, reset the DTI
                            SystErr = terM9_reset(Instr_Hand_DTI)

                        Case FGEN
                            'command to open fg outputs
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), ":OUTP OFF", retCount)
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "*RST", retCount)
                            FG_pass = True
                            If SystErr <> VI_SUCCESS Then FG_pass = False

                        Case RFSTIM
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), ":OUTP OFF", retCount)
                            RFS_pass = True

                        Case SR
                            'commands to open synchro outputs
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "REF_GEN1 STATE OPEN ", retCount)
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "REF_GEN2 STATE OPEN ", retCount)
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "SDH1 STATE OPEN ", retCount)
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "SDH2 STATE OPEN ", retCount)
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "DSH1 STATE OPEN ", retCount)
                            SystErr = atxml_WriteCmds(Instr_Hand(Instrument_Index), "DSH2 STATE OPEN ", retCount)

                        Case PPU
                            For PPU_Index = 1 To 10
                                TestCommandData = Convert.ToString(Chr(&H20 + PPU_Index)) + Convert.ToString(Chr(&HA0)) + Convert.ToString(Chr(&H0))
                                SystErr = atxml_WriteCmds(Ps_Hand(PPU_Index), TestCommandData, CLng(Len(TestCommandData)))
                                PPU_pass = True
                                If SystErr <> VI_SUCCESS Then
                                    PPU_msg &= PPU_Index & ", "
                                    PPU_pass = False
                                End If
                            Next PPU_Index
                    End Select
                Next Instrument_Index
            End If

            'If there are any active Power Supplys Prompt Warning
            If ActiveFlag = True Then
                'Give Warning to User
                Warning("Operator Warning", "Primary VXI Chassis Receiver Unit", 3000)
            End If


            If TerminateAtlasRts() Then
                'Give Warning to User if ATLAS RTS is Running
                Warning("Operator Warning", "Primary VXI Chassis Receiver Unit", 3005)
            End If

            'restart data poll timer
            frmSysMon.tmrDataPoll.Enabled = True
            frmSysMon.ETITimer.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Function StripNullCharacters(ByVal Parsed As String) As String
        StripNullCharacters = ""
        'DESCRIPTION:
        '   This routine strips characters with ASCII values less than 32 from the
        '   end of a string
        'PARAMTERS:
        '   Parsed$ = String from which to remove null characters
        'RETURNS:
        '   A the resultant parsed string
        
        Dim X As Integer

        For X = Convert.ToString(Parsed).Length To 1 Step -1
            If Asc(Mid(Convert.ToString(Parsed), X, 1)) > 32 Then
                Exit For
            End If
        Next X
        StripNullCharacters = Strings.Left(Convert.ToString(Parsed), X)
    End Function

    Sub OrderlyShutdown(ByVal WarningTopic As String, ByVal Component As String, ByVal Condition As Short, ByVal TimeUntilShutDown As Short)
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine starts an orderly shutdown procedure.   *
        '*     The user is given the option to abort shutdown.      *
        '*     The user and system log are given reason for the     *
        '*     shutdown condition.                                  *
        '*    PARAMETERS:                                           *
        '*     WarningTopic$ -The Error Type Heading                *
        '*     Component$ -The component/event causing the problem  *
        '*     Condition% -Index of the Error Text message          *
        '*     TimeUntilShutDown% -Time in Seconds until shutdown   *
        '*    EXAMPLE:                                              *
        '*      OrderlyShutdown "System ERROR", "Instrument", 1, 60 *
        '************************************************************

        'Check For Redundant Shutdown Events
        If frmAlarm.Visible Then Exit Sub
        'If a Modal MsgBx is displayed, use this error handler to set frmAlarm to Modal
        Try ' On Error GoTo FormErrorHandler

            'Disbale Polling timer
            frmSysMon.tmrDataPoll.Enabled = False

            'Reset Software, PPU, FPU
            TerminateInstrumentApplications()
            Delay(5)
            SetFpu(False) 'Shutdown FPU
            ResetPPU(Check:=True) 'Reset PPU Power Supplies
            'If the "ABNORMAL SHUTDOWN" MsgBx is displayed,
            'Close it to prevent Run Time Error 141.
            CloseAbnormalShutdownMsgBx()
            'Write Info to Alarm Form
            frmAlarm.lblAlarmType.Text = WarningTopic
            frmAlarm.lblComponentDescription.Text = Component
            frmAlarm.lblCondDesc.Text = GetWarningErrorMessage(Condition)
            UserTimeout = TimeUntilShutDown
            'Pop-up Alarm
            CenterForm(frmAlarm)
            BringToTop(frmAlarm.Handle.ToInt32)
            frmAlarm.Timer1.Enabled = True

RecordError:
            'Write Fault to System Log
            Echo(" ")
            Echo("///////////////////////////////////////////////////////")
            Echo("System Monitor Warning")
            Echo("System Time:")
            Echo(DateTime.Now.ToString())
            Echo("Operator: " & UserName)
            Echo(WarningTopic)
            Echo("Component: " & Component)
            Echo("Fault Condition: " & GetWarningErrorMessage(Condition))
            Echo("The System has been shut down due to a hazardous condition")
            Echo("///////////////////////////////////////////////////////")
            Echo(" ")
            UpdateLogFile()

            bShutdownFail = True
            sFaultCallout = "The System has been shut down due to a hazardous condition" & vbCrLf & WarningTopic & vbCrLf & Component & vbCrLf & GetWarningErrorMessage(Condition)
            dShutdownCode = Condition

            frmSysMon.Close()
            frmAlarm.ShowDialog()
            Exit Sub

        Catch   ' FormErrorHandler:
            'Err.Number 141: Can not display a Non-Modal Form when a Modal Form is displayed.
            If Err.Number = 141 Then 'If a Modal MsgBx is already displayed
                GoTo RecordError 'Alarm Form cannot be displayed,
            End If 'Record the Error, but the System will not shut down

        End Try


    End Sub



    Sub TerminateInstrumentApplications()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine terminates VIPERT applications that use   *
        '*     VISA instrument handles.  For a successful system    *
        '*     restart, all VISA handles must be closed. This may   *
        '*     also be used to reset instruments.                   *
        '*    EXAMPLE:                                              *
        '*      TerminateInstrumentApplications                     *
        '************************************************************
        Dim Info As String 'VIPERT.INI File Information
        Dim Terminated As Short 'Flag showing successful program termination
        Dim AtlasRunning As Short 'TRUE if ATLAS RTS is RUNNING

        Info = GatherIniFileInformation("SAIS", "SAIS_C", "") 'Get Name
        If Info <> "" Then 'Kill SAIS
            For Each p As Process In Process.GetProcessesByName("SAISMGR")
                p.Kill()
            Next
        End If

        AtlasRunning = TerminateAtlasRts() 'Kill ATLAS RTS

        Info = GatherIniFileInformation("File Locations", "SYSTEM_SURVEY_C", "") 'Get Name
        If Info <> "" Then 'Kill CTEST
            For Each p As Process In Process.GetProcessesByName("SYSTEMSURVEY")
                p.Kill()
            Next
        End If

        Info = GatherIniFileInformation("File Locations", "SYSTEM_SELF_TEST_C", "") 'Get Name
        If Info <> "" Then 'Kill STEST
            For Each p As Process In Process.GetProcessesByName("STEST")
                p.Kill()
            Next
        End If

    End Sub



    Sub AlarmWarningRoutineHere()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine checks the incoming data for hazardous  *
        '*     and potentially hazardous conditions.  The user is   *
        '*     then given an alarm or warning depending on the      *
        '*     severity of the fault.                               *
        '*    EXAMPLE:                                              *
        '*      AlarmWarningRoutineHere                             *
        '************************************************************
        Dim iReturn As Short 'Function Return value

        'Power Check
        VerifyInputPower() 'Check the input power for low/high voltages
        VerifyPowerConversion() 'Check HAM Phases and 2800 Watt converter
        VerifyFpuSupplyFaults() 'Check FPU Supplies for Mismatches or Faults
        VerifyFpuVoltageLevels() 'Check to see if a supply has been removed
        VerifyChassisVoltLevels() 'Check to see if a VXI backplane level is operating out of tolerance
        VerifyChassisCurrentLevels() 'Check to see if a VXI backplane current level is operating out of tolerance
        'Operator Settings Check
        VerifyPowerBudget() 'Check to see if the user has exceeded the available system power

        'Environmental Check
        'Note: If there is a communication failure it is not nesessary to
        'monitor OP Temps or Temp Rises because the data is invalid and will
        'trigger flase alarms
        VerifyChassisStatus() 'Check for Communication Error,Chassis Self Test, and the (6) Air Flow Sensors/Fans
        If CommunicationFailFlag = False Then
            iReturn = VerifyTemperatureSensors() 'Check Temperator Sensors for a failure.    'DR#203
            VerifyOperatingTemperature() 'Check to see if the system is above or below operating temperature
            VerifyTemperatureRise() 'Check to see if the rise are exceeding the defined thresholds
        End If

    End Sub





    
    Function TerminateApp32(ByVal AppTitle As String) As Short
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine will terminatee an external application *
        '*    PARAMETERS:                                           *
        '*     AppTitle$ = Window Caption application to terminate  *
        '*    RETURNS:                                              *
        '*     Boolean - True if Terminated and False if not        *
        '*    EXAMPLE:                                              *
        '*     Terminated% = TerminateApp32 Notepad                 *
        '************************************************************
        Dim CurrWnd As Integer 'Current Window Handle
        Dim Title As String 'Title of window found
        Dim WindowHandle As Integer 'Registersd Windows Operating System Handle
        Dim rc As Integer 'Return Value of Windows API Call
        
        Dim lpString As String = Space(255) 'Buffer for WIndow text in API call
        Dim Ret As Integer 'Window Text Error Code

        Application.DoEvents() 'Process Events For Timing Considerations
        CurrWnd = GetWindow(My.Forms.frmSysMon.Handle, GW_HWNDFIRST) 'Get First Window
        While CurrWnd <> 0 'Get All Window Information
            lpString = Space(255)
            Ret = GetWindowText(CurrWnd, lpString, 255)
            Title = Trim(lpString.ToString())
            'Debug.Print Title$ 'Uncomment this line is for debugging
            If UCase(Title) = UCase(AppTitle) + Convert.ToString(Chr(0)) Then
                WindowHandle = CurrWnd
                GoTo FoundIt
            End If
            CurrWnd = GetWindow(CurrWnd, GW_HWNDNEXT)
        End While

FoundIt:
        If WindowHandle <> 0 Then
            rc = PostMessage(WindowHandle, &H10, 0, 0) 'WM_CLOSE = &H10
        End If

        'Return Function Value
        If WindowHandle Then
            TerminateApp32 = True
        Else
            TerminateApp32 = False
        End If

    End Function

    Sub VerifyTemperatureRise()
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                   *
        '*    DESCRIPTION:                                          *
        '*     This Module Evaluates Reported Tempertures and warns *
        '*     the user if the limits are exceeded                  *
        '*    DEPENDENCES: Global Chassis.ChassisData  Variables    *
        '*    EXAMPLE:                                              *
        '*     VerifyTemperatureRise                                *
        '************************************************************
        Dim SlotIndex As Short 'VXI Chassis Slot Index
        Dim ChassName As String 'Description of Failing Component
        Static FailCount(25) As Short 'Number of consecutive failures
        Const FAIL_TMO As Short = 120 'The Number of consecutive fails necessary to cause a fault

        'Check Primary Chassis
        For SlotIndex = 0 To 12
            ChassName = "Primary VXI Chassis"
            'Find Diff Between Threshold and Rise
            If (((PrimaryChassis.TempRisePerSlot(SlotIndex)) / (TempertureThreshold(SlotIndex))) * 100) > 100 Then
                FailCount(SlotIndex) += 1
                If FailCount(SlotIndex) >= FAIL_TMO Then
                    FailCount(SlotIndex) = 0
                    OrderlyShutdown("Temperature Rise", ChassName & " Slot " & Str(SlotIndex), 2002, 240)
                End If
            Else
                FailCount(SlotIndex) = 0
            End If
        Next SlotIndex

        'Check Secondary Chassis
        For SlotIndex = 13 To 25
            ChassName = "Secondary VXI Chassis"
            'Find Diff Between Threshold and Rise
            If (((SecondaryChassis.TempRisePerSlot(SlotIndex - 13)) / (TempertureThreshold(SlotIndex))) * 100) > 100 Then
                FailCount(SlotIndex) += 1
                If FailCount(SlotIndex) >= FAIL_TMO Then
                    FailCount(SlotIndex) = 0
                    OrderlyShutdown("Temperature Rise", ChassName & " Slot " & Str(SlotIndex - 13), 2002, 240)
                End If
            Else
                FailCount(SlotIndex) = 0
            End If
        Next SlotIndex

    End Sub

    Function GetWarningErrorMessage(ByVal Condition As Short) As String
        GetWarningErrorMessage = ""
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine will return system fault/error text     *
        '*    PARAMETERS:                                           *
        '*     Condition% = Error Code                              *
        '*    RETURNS:                                              *
        '*     String Data Description of the Error                 *
        '*    EXAMPLE:                                              *
        '*     UserMessage$ = GetWarningErrorMessage(ERROR_2)       *
        '************************************************************
        Dim WarningMessage As String 'Text String Built to display alarm information to the user

        WarningMessage = "Fault:" & Str(Condition) & vbCrLf
        Select Case Condition 'Get Failure Condition Text
            '***Power Alarm Messages***
            Case 1000
                'Input Power Fail High
                WarningMessage &= "Input power has exceeded 30 VDC " & vbCrLf
                WarningMessage &= "in excess of 100 mS." & vbCrLf
                WarningMessage &= "PDU will shutdown over 32VDC."
            Case 1001
                'Input Power Fail Low
                WarningMessage &= "Input power has fallen below 20 VDC" & vbCrLf
                WarningMessage &= "in excess of 100 mS." & vbCrLf
                WarningMessage &= "PDU will shutdown under 18VDC."
            Case 1002
                'Power Budget Exceeded
                WarningMessage &= "The system has detected excessive power requests.  The sum of " & vbCrLf
                WarningMessage &= "power sources has exceeded total available system power." & vbCrLf & vbCrLf
                WarningMessage &= "PPU Supplies have been reset." & vbCrLf
                WarningMessage &= "All Power Supply Applications have been terminated." & vbCrLf
            Case 1003
                'FPU Fault Over Voltage
                WarningMessage &= "A Fixed Power Unit (FPU) Module has faulted due to an " & vbCrLf
                WarningMessage &= "over Voltage condition." & vbCrLf
                WarningMessage &= "Power sources will not be operable until " & vbCrLf
                WarningMessage &= "fault is resolved." & vbCrLf
                WarningMessage &= "FPU Supplies have been shut down." & vbCrLf
                WarningMessage &= "PPU Supplies have been reset." & vbCrLf
                WarningMessage &= "All Instrument Applications have been terminated." & vbCrLf
            Case 1004
                'FPU Fault Under Voltage
                WarningMessage &= "A Fixed Power Unit (FPU) Module has faulted due to an " & vbCrLf
                WarningMessage &= "under Voltage condition." & vbCrLf
                WarningMessage &= "Power sources will not be operable until " & vbCrLf
                WarningMessage &= "fault is resolved." & vbCrLf
                WarningMessage &= "FPU Supplies have been shut down." & vbCrLf
                WarningMessage &= "PPU Supplies have been reset." & vbCrLf
                WarningMessage &= "All Instrument Applications have been terminated." & vbCrLf
            Case 1005
                '2800 Watt Converter Fault / HAM Phase Fault
                WarningMessage &= "A failure condition has been detected in the Power" & vbCrLf
                WarningMessage &= " Distribution Unit (PDU)" & vbCrLf
            Case 1006
                'Backplane Voltages out of Tolerance or missing while FPU is on
                WarningMessage &= "VXI Chassis backplane Voltage levels are out of tolerance" & vbCrLf
                WarningMessage &= " due to a severed or disconnected cable." & vbCrLf
                WarningMessage &= "FPU Supplies have been shut down." & vbCrLf
                WarningMessage &= "PPU Supplies have been reset." & vbCrLf
                WarningMessage &= "All Instrument Applications have been terminated." & vbCrLf
            Case 1007
                'Power Requests Near Limit
                WarningMessage &= "The system has detected excessive power requests.  The sum of " & vbCrLf
                WarningMessage &= "power sources is 90% of the total available system power." & vbCrLf & vbCrLf
                WarningMessage &= "If power demands increase, system resources will reset." & vbCrLf
            Case 1008
                'Backplane Currents out of Tolerance or missing while FPU is on
                WarningMessage &= "VXI Chassis backplane Current levels are out of tolerance" & vbCrLf
                WarningMessage &= "FPU Supplies have been shut down." & vbCrLf
                WarningMessage &= "PPU Supplies have been reset." & vbCrLf
                WarningMessage &= "All Instrument Applications have been terminated." & vbCrLf
            Case 1009
                'Input Single Phase Fail High
                WarningMessage &= "Single Phase input power has exceeded 132 VAC " & vbCrLf
                WarningMessage &= "in excess of 100 mS." & vbCrLf
                WarningMessage &= "PDU will shutdown over 138 VAC Single Phase."
            Case 1010
                'Input Single Phase Fail Low
                WarningMessage &= "Single Phase input power has fallen below 108 VAC" & vbCrLf
                WarningMessage &= "in excess of 100 mS." & vbCrLf
                WarningMessage &= "PDU will shutdown under 102 VAC Single Phase."
            Case 1011
                'Input 3 phase Fail High
                WarningMessage &= "Three Phase input power has exceeded 228 VAC " & vbCrLf
                WarningMessage &= "in excess of 100 mS." & vbCrLf
                WarningMessage &= "PDU will shutdown over 250 VAC Three Phase."
            Case 1012
                'Input 3 phase Fail Low
                WarningMessage &= "Three Phase input power has fallen below 187 VAC" & vbCrLf
                WarningMessage &= " in excess of 100 mS." & vbCrLf
                WarningMessage &= "PDU will shutdown under 180 VAC Three Phase."
            Case 1013
                'FPU turn off
                WarningMessage &= "FPU's and PPU's have been shutdown due to input power out of tolerance" & vbCrLf
            Case 1014
                'FPU turn off
                WarningMessage &= "FPU or PPU Modual at Address: " & ActModAddr & " is faulted.  ATS must shutdown until the problem is repaired." & vbCrLf

                '***Environmental Alarm Messages***
            Case 2000
                'Below OP Temp
                WarningMessage &= "The system operating temperature has fallen below 10 degrees Celsius" & vbCrLf
                WarningMessage &= "" & vbCrLf
                WarningMessage &= "The Instrument Controller will perform an " & vbCrLf
                WarningMessage &= "orderly shut down procedure." & vbCrLf
            Case 2001
                'Above OP Temp
                WarningMessage &= "The system operating temperature has exceeded 65 degrees Celsius" & vbCrLf
                WarningMessage &= "" & vbCrLf
                WarningMessage &= "The Instrument Controller will perform an " & vbCrLf
                WarningMessage &= "orderly shut down procedure." & vbCrLf
            Case 2002
                'Temp Threshold Exceeded
                WarningMessage &= "An Instrument slot has exceed the maximum allowable temperature rise." & vbCrLf
                WarningMessage &= "" & vbCrLf
            Case 2003
                'Air Flow Sensor Fault
                WarningMessage &= "A chassis air flow sensor has detected a blocked or malfunctioning fan." & vbCrLf
                WarningMessage &= "" & vbCrLf
            Case 2004
                'PIC Controller in Chassis Reports an error
                WarningMessage &= "A failure condition has been detected in a VXI chassis." & vbCrLf
                WarningMessage &= "" & vbCrLf
            Case 2005
                WarningMessage &= "The system operating temperature has exceeded 55 degrees Celsius. " & vbCrLf
                WarningMessage &= "If temperatures continue to increase, system resources will reset." & vbCrLf

                '************** Must be documented in all documentation  *********
            Case 2006
                WarningMessage &= "The system operating temperature indicates that one or more " & vbCrLf
                WarningMessage &= "Temperature Sensors are malfunctioning." & vbCrLf
                WarningMessage &= "The Instrument Controller will perform an " & vbCrLf
                WarningMessage &= "orderly shut down procedure." & vbCrLf


                '***Operator Alarm Messages***
            Case 3000
                'Receiver Device Removed while Power Supply(s) on causing dangerous condition
                WarningMessage &= "The User has removed the Interface Test Adapter (ITA)" & vbCrLf
                WarningMessage &= " or Stand Alone Instrument Fixture (SAIF) while the" & vbCrLf
                WarningMessage &= " Programmable Power Unit (PPU) supplies are active." & vbCrLf
                WarningMessage &= vbCrLf
                WarningMessage &= "PPU Supplies have been reset." & vbCrLf
                WarningMessage &= "All Power Supply Applications have been terminated." & vbCrLf
            Case 3001
                WarningMessage &= "The Instrument Controller has lost GPIB communication with the " & vbCrLf
                WarningMessage &= "remainder of the system." & vbCrLf
            Case 3002
                WarningMessage &= "A cable is disconnected or the chassis hardware has lost the " & vbCrLf
                WarningMessage &= "ability to communicate with the instrument controller." & vbCrLf
            Case 3003
                WarningMessage &= "An FPU module has failed or has been removed during normal operation." & vbCrLf
            Case 3004
                'FPU Module Mismatch
                WarningMessage &= "A Fixed Power Unit (FPU) Module has been inserted into the wrong slot." & vbCrLf
                WarningMessage &= "FPU Supplies have been shut down." & vbCrLf
                WarningMessage &= "PPU Supplies have been reset." & vbCrLf
                WarningMessage &= "All Instrument Applications have been terminated." & vbCrLf
            Case 3005
                'Receiver Device Removed while Run-Time System operating, causing dangerous condition
                WarningMessage &= "The User has removed the Interface Test Adapter (ITA)" & vbCrLf
                WarningMessage &= " or Stand Alone Instrument Fixture (SAIF) while the" & vbCrLf
                WarningMessage &= " ATLAS Run-Time System was active." & vbCrLf
                WarningMessage &= vbCrLf
                WarningMessage &= "Instrument resources have been reset." & vbCrLf
                WarningMessage &= "The ATLAS Run-Time Application has been terminated." & vbCrLf

        End Select

        GetWarningErrorMessage = WarningMessage

    End Function

    Sub VerifyFpuVoltageLevels()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine will Check the Fixed Power Unit (FPU)   *
        '*     for Fault Conditions.
        '*    PARAMETERS:                                           *
        '*     NONE                                                 *
        '*    EXAMPLE:                                              *
        '*     VerifyFpuVoltageLevels()                             *
        '************************************************************
        Dim FpuVMeasured(6, 2) As Single 'Fpu Levels
        Dim SupplyFpuIndex As Short 'Fpu Supply Index
        Dim ChassisIndex As Short 'Chassis Number Index
        Dim FpuCallOutIndex As String 'Determines Supply Number Reported To User
        Static FailCount(6, 2) As Short 'Number of consecutive failures
        Const FAIL_TMO As Short = 20 'The Number of consecutive fails necessary to cause a fault

        'If No Voltage then Exit Sub
        If AllPowerOff Then Exit Sub

        'Get Voltages
        'Fpu slot A right
        FpuVMeasured(0, 1) = PrimaryChassis.P5AVoltage
        FpuVMeasured(1, 1) = PrimaryChassis.N2AVoltage
        FpuVMeasured(2, 1) = PrimaryChassis.N52AVoltage

        'Fpu slot B right
        FpuVMeasured(3, 1) = PrimaryChassis.P12AVoltage
        FpuVMeasured(4, 1) = PrimaryChassis.P24AVoltage
        FpuVMeasured(5, 1) = PrimaryChassis.N24AVoltage
        FpuVMeasured(6, 1) = PrimaryChassis.N12AVoltage

        'Fpu slot A left
        FpuVMeasured(0, 2) = SecondaryChassis.P5BVoltage
        FpuVMeasured(1, 2) = SecondaryChassis.N2BVoltage
        FpuVMeasured(2, 2) = SecondaryChassis.N52BVoltage

        'Fpu slot B left
        FpuVMeasured(3, 2) = SecondaryChassis.P12BVoltage
        FpuVMeasured(4, 2) = SecondaryChassis.P24BVoltage
        FpuVMeasured(5, 2) = SecondaryChassis.N24BVoltage
        FpuVMeasured(6, 2) = SecondaryChassis.N12BVoltage

        If FPU_IgnoreFaults = False Then
            'Check For Shutdown Condition
            For ChassisIndex = 1 To 2
                For SupplyFpuIndex = 0 To 6
                    If FpuVMeasured(SupplyFpuIndex, ChassisIndex) <> 0 Then
                        GoTo aVoltageCheck
                    End If
                Next SupplyFpuIndex
            Next ChassisIndex
            Exit Sub

aVoltageCheck:
            'Check For Removed Supplies
            For ChassisIndex = 1 To 2
                For SupplyFpuIndex = 0 To 6
                    If FpuVMeasured(SupplyFpuIndex, ChassisIndex) = 0 Then 'Supply Removed
                        FailCount(SupplyFpuIndex, ChassisIndex) += 1
                        If FailCount(SupplyFpuIndex, ChassisIndex) >= FAIL_TMO Then
                            FailCount(SupplyFpuIndex, ChassisIndex) = 0 'Reset Fail Count
                            Select Case SupplyFpuIndex
                                Case 0 To 2
                                    If ChassisIndex = 1 Then
                                        Warning("Module Fault", "Fixed Power Unit (FPU) Supply Slot A Right", 3003) 'Dislpay Warning
                                        SetFpu(False)
                                    Else
                                        Warning("Module Fault", "Fixed Power Unit (FPU) Supply Slot A Left", 3003) 'Dislpay Warning
                                        SetFpu(False)
                                    End If
                                Case 3 To 6
                                    If ChassisIndex = 1 Then
                                        Warning("Module Fault", "Fixed Power Unit (FPU) Supply Slot B Right", 3003) 'Dislpay Warning
                                        SetFpu(False)
                                    Else
                                        Warning("Module Fault", "Fixed Power Unit (FPU) Supply Slot B Left", 3003) 'Dislpay Warning
                                        SetFpu(False)
                                    End If
                            End Select
                        End If
                    Else
                        FailCount(SupplyFpuIndex, ChassisIndex) = 0 'Pass / Reset Count
                    End If
                Next SupplyFpuIndex
            Next ChassisIndex
        End If
    End Sub

    Sub UpdateWarningGui(ByVal Pointer As Short)
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine will update the Warning/Caution Dialog  *
        '*    PARAMETERS:                                           *
        '*     Pointer% = Location in Queue                         *
        '*    EXAMPLE:                                              *
        '*     UpdateWarningGui NextPosition%                       *
        '************************************************************

        frmWarning.lblAlarmType.Text = WarningQueue(Pointer).Topic
        frmWarning.lblComponentDescription.Text = WarningQueue(Pointer).Component
        frmWarning.lblCondDesc.Text = WarningQueue(Pointer).Condition

        'Limit Queue Size
        If QueueCounter = 26 Then QueueCounter = 25

        If Pointer = QueueCounter Then
            frmWarning.cmdNext.Enabled = False
        Else
            frmWarning.cmdNext.Enabled = True
        End If

        If Pointer = 1 Then
            frmWarning.cmdPrevious.Enabled = False
        Else
            frmWarning.cmdPrevious.Enabled = True
        End If
    End Sub

    Sub VerifyOperatingTemperature()
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                   *
        '*    DESCRIPTION: Verifies operating environment has not   *
        '*     exceeded the design limits of the system             *
        '*    DEPENDENCES: Global Chassis.ChassisData  Variables    *
        '*    EXAMPLE: VerifyOperatingTemperature                   *
        '************************************************************
        Dim slot As Short 'Chassis slot index
        Dim Component As String 'Description of failing component
        Static FailCount(5) As Short 'Number of consecutive failures
        Const FAIL_TMO As Short = 30 'The Number of consecutive fails necessary to cause a fault

        Dim sTemp As String 'String value of the Primary Intake Temperature
        Dim dTemp As Double 'Numeric value for the Primary Intake Temperature
        dTemp = PrimaryChassis.IntakeTemperature 'Get the Primary Temp
        sTemp = CStr(dTemp) 'Convert to a String value
        UpdateIniFile("System Monitor", "PRI_TEMP", sTemp) 'Update the VIPERT.ini File


        If (PrimaryChassis.IntakeTemperature > 55) And (PrimaryChassis.IntakeTemperature <> -45) Then
            If (PrimaryChassis.IntakeTemperature > 65) Then
                FailCount(4) += 1
                If FailCount(4) >= FAIL_TMO Then
                    FailCount(4) = 0
                    Component = "Primary VXI Chassis"
                    OrderlyShutdown("Operating Temperature", Component, 2001, 240)
                End If
            Else
                FailCount(4) = 0
            End If
            FailCount(0) += 1
            If FailCount(0) >= FAIL_TMO Then
                FailCount(0) = 0
                Component = "Primary VXI Chassis"
                Warning("Operating Temperature", Component, 2005)
            End If
        Else
            FailCount(0) = 0
            FailCount(4) = 0
        End If

        'Check Secondary Chassis Intake For "Above OP Temp"
        If (SecondaryChassis.IntakeTemperature > 55) And (SecondaryChassis.IntakeTemperature <> -45) Then
            If (SecondaryChassis.IntakeTemperature > 65) Then
                FailCount(5) += 1
                If FailCount(5) >= FAIL_TMO Then
                    FailCount(5) = 0
                    Component = "Secondary VXI Chassis"
                    OrderlyShutdown("Operating Temperature", Component, 2001, 240)
                End If
            Else
                FailCount(5) = 0
            End If
            FailCount(1) += 1
            If FailCount(1) >= FAIL_TMO Then
                FailCount(1) = 0
                Component = "Secondary VXI Chassis"
                Warning("Operating Temperature", Component, 2005)
            End If
        Else
            FailCount(1) = 0
            FailCount(5) = 0
        End If

        'Check Primary Chassis Intake For "Below OP Temp"
        If (PrimaryChassis.IntakeTemperature < (-12)) And (PrimaryChassis.IntakeTemperature <> (-45)) Then
            FailCount(2) += 1
            If FailCount(2) >= FAIL_TMO Then
                FailCount(2) = 0
                Component = "Primary VXI Chassis"
                OrderlyShutdown("Operating Temperature", Component, 2000, 240)
            End If
        Else
            FailCount(2) = 0
        End If
        'Check Secondary Chassis Intake For "Below OP Temp"
        If (SecondaryChassis.IntakeTemperature < (-12)) And (SecondaryChassis.IntakeTemperature <> (-45)) Then
            FailCount(3) += 1
            If FailCount(3) >= FAIL_TMO Then
                FailCount(3) = 0
                Component = "Secondary VXI Chassis"
                OrderlyShutdown("Operating Temperature", Component, 2000, 240)
            End If
        Else
            FailCount(3) = 0
        End If


    End Sub

    Sub VerifyPowerConversion()
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine verify that the Input power converters  *
        '*     are operating correctly                              *
        '*    EXAMPLE:                                              *
        '*     VerifyPowerConversion                                *
        '************************************************************
        
        Dim FailFlag As Short 'Failure State
        Dim FalingComponent As String 'Description of failing component
        Static FailCount(3) As Short 'Number of consecutive failures
        Const FAIL_TMO As Short = 30 'The Number of consecutive fails necessary to cause a fault

        'Check 2800 Watt Converter Fault
        If PowerStatus2800Watt <> 0 And PowerStatusDc = 0 Then
            FailCount(0) += 1
            If FailCount(0) >= FAIL_TMO Then
                FailCount(0) = 0
                FailFlag = True
                FalingComponent = "2800 Watt Converter Unit"
            End If
        Else
            FailCount(0) = 0
        End If

        'Check HAM Phase Converter Fault
        If (PowerStatusPhase1 <> 0) And (PowerStatusDc = 0) Then 'HAM1
            FailCount(1) += 1
            If FailCount(1) >= FAIL_TMO Then
                FailCount(1) = 0
                FailFlag = True
                FalingComponent = "Phase 1 of the Harmonic Amplitude Modulator (HAM)"
            End If
        Else
            FailCount(1) = 0
        End If
        If (PowerStatusPhase2 <> 0) And (PowerStatusDc = 0) Then 'HAM2
            FailCount(2) += 1
            If FailCount(2) >= FAIL_TMO Then
                FailCount(2) = 0
                FailFlag = True
                FalingComponent = "Phase 2 of the Harmonic Amplitude Modulator (HAM)"
            End If
        Else
            FailCount(2) = 0
        End If
        If (PowerStatusPhase3 <> 0) And (PowerStatusDc = 0) Then 'HAM3
            FailCount(3) += 1
            If FailCount(3) >= FAIL_TMO Then
                FailCount(3) = 0
                FailFlag = True
                FalingComponent = "Phase 3 of the Harmonic Amplitude Modulator (HAM)"
            End If
        Else
            FailCount(3) = 0
        End If


        If FailFlag = True Then
            TerminateInstrumentApplications()
            Delay(5)
            SetFpu(False) 'Shutdown FPU
            ResetPPU(Check:=True) 'Reset PPU Power Supplies
            Warning("Input Power", FalingComponent, 1005)
        End If

    End Sub

    Sub Warning(ByVal WarningTopic As String, ByVal Component As String, ByVal Condition As Short)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     This Module Displays a system fault condition to the *
        '*     user.                                                *
        '*    EXAMPLE: Warning "Error", "Item That Failed", ErrNum% *
        '************************************************************
        Dim TempCounter As Short

        'Check For Redundant Warnings
        For TempCounter = 1 To 25
            If WarningQueue(TempCounter).Topic = WarningTopic Then
                If WarningQueue(TempCounter).Component = Component Then
                    Exit Sub 'Ignore Redundant Alarms
                End If
            End If
        Next TempCounter

        'Check For Next Position in Queue
        For TempCounter = 1 To 26 'Get Next Empty Message Position
            If WarningQueue(TempCounter).Condition = "" Then Exit For
        Next TempCounter
        'If No empty positions, write over next position
        QueueCounter = TempCounter
        If QueueCounter = 26 Then
            If OverflowPointer = 26 Then OverflowPointer = 0
            OverflowPointer += 1
            QueuePointer = OverflowPointer
        Else
            QueuePointer = QueueCounter 'Go to next position
        End If

        'Write Info to Queue
        WarningQueue(QueuePointer).Topic = WarningTopic
        WarningQueue(QueuePointer).Component = Component
        WarningQueue(QueuePointer).Condition = GetWarningErrorMessage(Condition)

        'Write Fault to System Log
        Echo(" ")
        Echo("///////////////////////////////////////////////////////")
        Echo("System Monitor Warning")
        Echo("System Time:")
        Echo(DateTime.Now.ToString)
        Echo("Operator: " & UserName)
        Echo(WarningTopic)
        Echo("Component: " & Component)
        Echo("Fault Condition: " & GetWarningErrorMessage(Condition))
        Echo("///////////////////////////////////////////////////////")
        Echo(" ")
        UpdateLogFile()

        'Pop up warning
        frmWarning.WindowState = 0 'Normal/Restore
        CenterForm(frmWarning)
        'QueuePointer% = 1
        UpdateWarningGui(QueuePointer)


        If SING_CHASSIS_OPTION Then
            frmWarning.WindowState = FormWindowState.Minimized
        End If
        If frmWarning.Visible = False Then
            frmWarning.ShowDialog()
        End If



    End Sub

    Sub BringToTop(ByVal hWnd As Integer)
        '************************************************************
        '************************************************************
        '* Nomenclature  : System Monitor  [AlarmCondition]         *
        '*    DESCRIPTION:                                          *
        '*     This Routine will terminatee an external application *
        '*    PARAMETERS:                                           *
        '*     AppTitle$ = Window Caption application to terminate  *
        '*    RETURNS:                                              *
        '*     Boolean - True if Terminated and False if not        *
        '*    EXAMPLE:                                              *
        '*     Terminated% = TerminateApp32 Notepad                 *
        '************************************************************
        Dim nFlags As Short 'API Call Register Settings
        Dim wFlags As Integer 'API Call Register Settings
        Dim RetValue As Integer 'API Call Register Settings
        Const HWND_TOPMOST As Short = -1 'API Call Register CONSTANT
        Const SWP_NOMOVE As Short = &H2 'API Call Register CONSTANT
        Const SWP_NOSIZE As Short = &H1 'API Call Register CONSTANT

        wFlags = SWP_NOMOVE Or SWP_NOSIZE
        RetValue = SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, wFlags)

    End Sub

    Public Function VerifyTemperatureSensors(Optional ByVal bVerifyOkToHeat As Boolean = False) As Short
        '*******************************************************************************
        '***    When the Temperature Sensor is unplugged from the SysMon Board       ***
        '***    the buffer will read ASCII 82 or a Temperature of -4 deg for the     ***
        '***    affected Exhaust Temperature Slot.                                   ***
        '***    The Rule is that the Exhaust Temperature must be -4 for 2+ Minutes   ***
        '***    (240 consecutive cycles) and the Intake Temperature must not be      ***
        '***    within +- 10 deg to constitute a Temperature Sensor Board Failure.   ***
        '***    If at any time in the 240 cycles the temperature does not equal -4,  ***
        '***    the counter is reset. In the case of a multiple failures other than  ***
        '***    Total, only the last failure will be identified.                     ***
        '*******************************************************************************

        Dim iChassisSlot As Short 'Chassis Slot identifier
        Dim iPriSensor As Short 'Primary Sensor Failure counter
        Dim iSecSensor As Short 'Secondary Sensor Failure counter
        Dim bPriTotalFailure As Boolean 'Total Primary Exhaust Temperature Sensor Board failure.
        Dim bSecTotalFailure As Boolean 'Total Secondary Exhaust Temperature Sensor Board failure.
        Dim bTempSensorFailure As Boolean 'Flag to indicate a Failure has occurred
        Dim sComponentMsg As String 'Chassis/Slot message for a failure other than Total
        Dim bAdjacentSlotTemp(26) As Boolean 'Flag to indicate the Adjacent slot's temp within 10 deg. C
        Static nTempFailCount(26) As Integer 'Holds failure count for evaluation


        '0 to 12 for Primary Chassis and 13 to 25 for Secondary Chassis
        For iChassisSlot = 0 To 25
            'If User Cancels the Alarm, Reset Temp Fail Counters
            If nTempFailCount(iChassisSlot) > 240 Then nTempFailCount(iChassisSlot) = 0
            'If the Exhaust Slot Temperature is equal to -4 deg C
            'And the Intake Temperature has a differential > 10
            '(no need test lower limit, outside of operating range).
            'If this holds true for 240 consecutive cycles (2+ Minutes) then
            'The Temperature Sensor has most probably failed. Increment Count.

            'Primary Chassis analysis
            If iChassisSlot <= 12 Then
                If (PrimaryChassis.Temperature!(iChassisSlot) = -4) And PrimaryChassis.IntakeTemperature > 6 Then
                    nTempFailCount(iChassisSlot) += 1 'iterate counter

                    'Evaluate adjacent slot temperature, if the adjacent slot temperature
                    'falls outside a 10 deg. Differential then set Adjacent slot flag to True.
                    If iChassisSlot = 0 Then
                        'If its the first slot, add 1 to the slot number to find adjacent slot
                        If (PrimaryChassis.Temperature!(iChassisSlot + 1) > 6) Then
                            'Adjacent Slot outside 10 deg. C
                            bAdjacentSlotTemp(iChassisSlot) = True
                        End If
                    Else
                        'If its not the first slot, subtract 1 to the slot number to find adjacent slot
                        If (PrimaryChassis.Temperature!(iChassisSlot - 1) > 6) Then
                            'Adjacent Slot outside 10 deg. C
                            bAdjacentSlotTemp(iChassisSlot) = True
                        End If
                    End If
                    'Check for total Primary Exhaust Temperature Sensor Board Failure.
                    iPriSensor += 1
                    'Set Component Message to identify the Chassis and Slot number
                    sComponentMsg = "Primary Chassis (Slot " & iChassisSlot & ")"
                    If iPriSensor = 13 Then bPriTotalFailure = True

                    If nTempFailCount(iChassisSlot) = 240 Then '240 iterations = approx. 2+ min.
                        bTempSensorFailure = True 'Set Temp Sensor Failure Flag to True
                    End If

                Else                    'Primary Exhaust Temperature doesn't fit the failing criteria
                    'Reset Counter
                    nTempFailCount(iChassisSlot) = 0
                    bAdjacentSlotTemp(iChassisSlot) = False 'Reset Adjacent slot differential Flag
                End If

                'Secondary Chassis analysis
            Else                'Subtract 13 from iChassisSlot to match Slot Numbers in Secondary Chassis
                If (SecondaryChassis.Temperature!(iChassisSlot - 13) = -4) And SecondaryChassis.IntakeTemperature > 6 Then

                    'Evaluate adjacent slot temperature, if the adjacent slot temperature
                    'falls outside a 10 deg. Differential then set Adjacent slot flag to True.
                    If iChassisSlot = (iChassisSlot - 13) Then
                        If (SecondaryChassis.Temperature!((iChassisSlot - 13) + 1) > 6) Then
                            'Adjacent Slot outside 10 deg. C
                            bAdjacentSlotTemp(iChassisSlot) = True
                        End If
                    Else
                        If (SecondaryChassis.Temperature!((iChassisSlot - 13) - 1) > 6) Then
                            'Adjacent Slot outside 10 deg. C
                            bAdjacentSlotTemp(iChassisSlot) = True
                        End If
                    End If

                    'Check for total Secondary Exhaust Temperature Sensor Board Failure.
                    iSecSensor += 1
                    'Set Component Message to identify the Chassis and Slot number
                    sComponentMsg = "Secondary Chassis (Slot " & (iChassisSlot - 13) & ")"
                    If iSecSensor = 13 Then bSecTotalFailure = True

                    nTempFailCount(iChassisSlot) += 1 'iterate counter
                    If nTempFailCount(iChassisSlot) = 240 Then '240 iterations = approx. 2+ min.
                        bTempSensorFailure = True 'Set Temp Sensor Failure Flag to True
                    End If

                Else
                    'Reset Counter
                    nTempFailCount(iChassisSlot) = 0
                    bAdjacentSlotTemp(iChassisSlot) = False 'Reset Adjacent slot differential Flag
                End If
            End If
        Next

        '====================================================================================
        '**                        Evaluate results...                                     **
        '**  If any Slot's nTempFailCount is equal to 240, then report it as a failure.    **
        '**  If all slots in a single chassis fail, report it as a total sensor            **
        '**  board failure.                                                                **
        '**  If called from Sub AdjustHeaters(), perform an evaluation to determine        **
        '**  if it is safe to turn on Heaters. If a sensor is reading -4 deg. C and the    **
        '**  adjacent exhaust temp slot is outside a 10 deg. C differential then inhibit   **
        '**  automatic heater adjustment. This will prevent the heaters from coming on     **
        '**  in the event of a possible temperature sensor failure.                        **
        '**  The Heaters can be still be manually operated at any time.                    **
        '====================================================================================

        If bTempSensorFailure Then 'An Exhaust Temperature Sensor failure was detected.
            'Report failure and perform an orderly shutdown
            If bPriTotalFailure = True Then 'Total Primary Temperature Sensor Board failure.
                OrderlyShutdown("Exhaust Temperature Sensors", "Primary VXI Chassis", 2006, 30)
                Exit Function
            ElseIf bSecTotalFailure = True Then                'Total Secondary Temperature Sensor Board failure.
                OrderlyShutdown("Exhaust Temperature Sensors", "Secondary VXI Chassis", 2006, 30)
                Exit Function
            Else                'If it wasn't a Total failure, then report the Chassis and individual slot.

                OrderlyShutdown("Exhaust Temperature Sensor", sComponentMsg, 2006, 30)
                Exit Function
            End If

        Else
            'Evaluate if its safe to turn on heaters
            'If the bVerifyOkToHeat flag is True, Call is from Sub Procedure AdjustHeaters()
            If bVerifyOkToHeat Then
                'Check for a Total failure first...
                If bPriTotalFailure = True Then
                    'A total failure has been detected on the
                    'Primary Chassis Exhaust Temp Sensor Board.
                    VerifyTemperatureSensors = PRI 'Inhibit Primary Chassis Heaters
                    Exit Function
                ElseIf bSecTotalFailure = True Then
                    'A total failure has been detected on the
                    'Secondary Chassis Exhaust Temp Sensor Board.
                    VerifyTemperatureSensors = Sec 'Inhibit Secondary Chassis Heaters
                    Exit Function
                End If

                'Next check for a single slot failure
                For iChassisSlot = 0 To 26 'Loop through Chassis Exhaust Temp Slots
                    If iChassisSlot > -1 And iChassisSlot < 13 Then 'Primary Chassis
                        If bAdjacentSlotTemp(iChassisSlot) = True Then
                            'A least one failure has been detected on the
                            'Primary Chassis Exhaust Temp Sensor Board.
                            VerifyTemperatureSensors = PRI 'Inhibit Primary Chassis Heaters
                            Exit Function
                        End If
                    ElseIf iChassisSlot > 12 And iChassisSlot < 26 Then                        'Secondary Chassis
                        If bAdjacentSlotTemp(iChassisSlot) = True Then
                            'A least one failure has been detected on the
                            'Secondary Chassis Exhaust Temp Sensor Board.
                            VerifyTemperatureSensors = Sec 'Inhibit Secondary Chassis Heaters
                            Exit Function
                        End If
                    End If
                Next 'Next Chassis Slot
            End If
        End If

        VerifyTemperatureSensors = SAFE 'Safe to turn on heaters, no failures detected.

    End Function

    Public Sub CloseAbnormalShutdownMsgBx()
        '*********************************************************************
        '**     Finds the "ABNORMAL SHUTDOWN" Message Box Handle            **
        '**     Finds the [OK] button's Handle                              **
        '**     Sets the Message Box to the active Window                   **
        '**     Executes a Button Click on the Message Box's [OK] button.   **
        '**     Will try (5) times to close Message Box, If the Message Box **
        '**     is not open or has been closed, Exit Sub.                   **
        '*********************************************************************
        Dim nMsgBxHandle As Integer 'Handle to the Message Box
        Dim nButtonHandle As Integer 'Handle to the OK button
        Dim nRetVal As Integer 'Return value
        Dim iTrys As Short 'Number of times to try and close Message Box

        For iTrys = 1 To 5
            'First, see if the Message Box titled "ABNORMAL SHUTDOWN" is currently open.
            nMsgBxHandle = FindWindow(CLng(0), "ABNORMAL SHUTDOWN")
            If nMsgBxHandle = 0 Then Exit Sub 'If MsgBx box is not open, or has been
            'closed, Exit Sub
            'Now get a handle to the "OK" button in the Message Box.
            nButtonHandle = FindWindowEx(nMsgBxHandle, 0, 0, "OK")

            'Make sure that the Message Box is the active window.
            nRetVal = SetActiveWindow(nMsgBxHandle)
            'Click the "OK" button.
            nRetVal = SendMessage(nButtonHandle, BM_CLICK, 0, 0)
        Next

    End Sub


    Sub ReceiverEventWarningThread()
        Warning("Operator Warning", "Primary VXI Chassis Receiver Unit", 3005)
    End Sub

End Module