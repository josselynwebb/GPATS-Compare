Option Strict Off
Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Text
Imports System.Math
Imports Microsoft.VisualBasic

Public Module modDcpsOldTetsCode

    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : System Self Test: DC Power Supply Test    *
    '* Written By     : Michael McCabe                            *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  DC Power Supply Self Test                 *
    '**************************************************************

    Dim PolarityStatus(10) As Integer

    Dim TesterVer As String = "VersionB"
    Public DumpSupply As Single
    Public DumpAddr As Integer

    Dim Rcentury As Integer
    Dim Ryear As Integer
    Dim Rmonth As Integer
    Dim Rday As Integer

    Public Function TestPowerSuppliesTETS() As Integer
        'DESCRIPTION:
        'This routine tests the power supplies
        'Returns:
        'PASSED if the supplies are passing and FAILED if they fail
        'Module Defaults on power up/Reset:
        '           0V, 75mA, CL mode, Independent, Local sense, Disabled

        '*****************************************************************************************************
        '                   PPS Self Test Procedure
        '*****************************************************************************************************
        '1.  Measure volts w/o Load w/ power supply
        '2.  Open Power Supply relay and verify no voltage with the DMM
        '3.  Measure volts with Load w/ DMM
        '4.  Measure current with the power supply with Load.
        '5.  Verify with the DMM that the output voltage drops when using local sense under loaded conditions
        '6.  Verify that the supply faults when overloaded in constant voltage mode
        '7.  Verify that the supply regulates current correctly in constant current mode
        '8.  Set supplies 2-8 to slave mode and verify voltage slaves to Power supply on left
        '******************************************************************************************************

        Dim DC(0 To 9) As String
        Dim CloseRelay As Integer
        Dim SetSlave As Integer
        Dim SenseRemote As Integer
        Dim CurrentConstant As Integer
        Dim OpenRelay As Integer
        Dim SetMaster As Integer
        Dim SenseLocal As Integer
        Dim CurrentLimit As Integer
        Dim Supply As Integer
        Dim SupplyStatus As Integer
        Dim dMeasurement As Double
        Dim VoltWithLoad As Double
        Dim current As Single
        Dim test As String
        Dim UpperLimit As Single
        Dim LowerLimit As Single
        Dim CurrentMaster As Single
        Dim CurrentSlave As Single
        Dim VoltageMeas As Single
        Dim iTerm As Integer                  'DC Terminal loop counter
        Dim iTermNumber As Integer            'DC Terminal Number to report
        Dim iResponse As Integer              'Response indicator from Message Box
        Dim sSecondMeasString As String = ""  'Second returned Measurement from Instrumnet
        Dim sMeasString As String = ""        'Returned Measurement from Instrumnet
        Dim DCRes(0 To 9) As Double
        Dim LoopCount As Integer


        'Programmable Power Unit Test Title Block
        EchoTitle(InstrumentDescription(PPU), True)
        EchoTitle("Programmable Power Unit Test", False)

        ''If InstrumentStatus(DMM) = FAILED And RunningEndToEnd Then
        ''    SError = ReturnDependentInstrumentString(PPU, DMM)
        ''    MsgBox(SError, vbExclamation) : Echo(SError)
        ''    TestPowerSuppliesTets = DMM
        ''    GoTo TestComplete
        ''End If

        ''hndlDMM& = DMM

        ''If hndlDMM& = 0 Then
        ''    SError = ReturnNoCommString(DMM, PPU)
        ''    MsgBox(SError, vbExclamation) : Echo(SError)
        ''    TestPowerSuppliesTets = DMM
        ''    GoTo TestComplete
        ''End If

        TestPowerSuppliesTets = PASSED

        'Define command options.
        CloseRelay = False
        SetSlave = False
        SenseRemote = False
        CurrentConstant = False

        OpenRelay = True
        SetMaster = True
        SenseLocal = True
        CurrentLimit = True

        DC(1) = "1.0000"
        DC(2) = "1.0001"
        DC(3) = "1.0002"
        DC(4) = "1.0003"
        DC(5) = "1.0004"
        DC(6) = "2.0000"
        DC(7) = "2.0001"
        DC(8) = "2.0002"
        DC(9) = "2.0003"

        '************************************Self-Test procedure******************************************
        'P10-01-001 ~ P01-01-001 reported in SelfTestPPS()

        For Supply = 1 To 10
            If SelfTestPPS(Supply) = FAILED Then 'Function call to reset and BIT test each supply 1-10
                TestPowerSuppliesTets = FAILED
            End If
        Next Supply

        DisplaySetup("Connect DMM INPUT (HI/LO) to DC POWER SUPPLIES DC9 (HI/LO) using cable W24.", "DC9_DIN.BMP", 1)

        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        '+++                        DC1-DC10 Terminal Short Test                            +++
        '+++                                        Added for DR207  DJoiner  11/27/01      +++
        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        'Close all SAIF DC Supply interconnect Switches
        For iTerm = 1 To 9
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & DC(iTerm))
        Next

TestForLoads:
        'Measure Resistance, If less than 35 Ohms, Warn Operator: Loads connected prematurely
        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        nSystErr = WriteMsg(DMM, "SENS:RES:APER MIN")
        Delay(0.01)
        nSystErr = WriteMsg(DMM, "MEAS:RES?")
        WaitForResponse(DMM, 0)
        nSystErr = ReadMsg(DMM, sMeasString)

        'If resistance is less than 35 Ohms, check for Loads connected or a short.
        If Val(sMeasString) < 35 Then
            If Val(sMeasString) > 2 Then    'Check for Loads connected

                'If Measurement indicates that the Loads are connected to DC1 (next step), Inform Operator
                If Val(sMeasString) > 8.5 And Val(sMeasString) < 35 Then
                    'Open DC(1) and check Resistance again
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & DC(1))
                    nSystErr = WriteMsg(DMM, "MEAS:RES?")
                    WaitForResponse(DMM, 0)
                    nSystErr = ReadMsg(DMM, sSecondMeasString)
                    'P01-02-D01
                    If Val(sSecondMeasString) > 35 Then
                        iResponse = MsgBox("Self Test has detected a resistance reading across DC1 HI/LO indicating a load has been connected prematurely." & vbCrLf _
                            & "If there is a load connected across DC1 HI/LO, please remove it and select [Retry]." & vbCrLf _
                            & "Otherwise select [Cancel] to terminate test.", vbExclamation + vbRetryCancel, "Check for Loads Connected")

                        If iResponse = vbRetry Then           'Allows Operator to Repeat test if Loads where connected
                            nSystErr = WriteMsg(SWITCH1, "CLOSE " & DC(1))
                            GoTo TestForLoads
                        Else    'Allows the operator to confirm Loads are not connected and terminate test
                            ' Tested twice and the operator confirmed, Report as a failure
                            MsgBox("Self Test has detected a resistance reading on DC1 matching that of the Loads.")
                            Echo(FormatResults(False, "DC1 HI/LO Short Test, Resistance is less than 35 Ohms", "P01-02-D01"))
                            Echo("Self Test has detected a resistance reading on DC1 matching that of the Loads.")
                            RegisterFailure(PPU, "P01-02-D01", Val(sMeasString), "Ohms", 35, , _
                              "Self Test has detected a resistance reading on DC1 matching that of the Loads.")
                            SupplyStatus = FAILED
                            TestPowerSuppliesTets = FAILED
                            GoTo TestComplete
                        End If
                    End If
                End If
            End If
            'If Loads are not connected prematurely and Resistance is less than 35 Ohms, check for a Short
            'Open all DC Switches
            For iTerm = 1 To 9
                nSystErr = WriteMsg(SWITCH1, "OPEN " & DC(iTerm))
            Next
            nSystErr = WriteMsg(DMM, "MEAS:RES?")
            '       WaitForResponse DMM, 0
            'P09-02-001
            'Check DC9 for a short
            dMeasurement = FetchMeasurement(DMM, "DC9 HI/LO Short Test.", "100", "", "Ohms", PPU, "P09-02-D02")

            If dMeasurement < 100 Then
                MsgBox("A short has been detected across DC9 HI/LO", vbCritical)            'DC9 is Shorted
                SupplyStatus = FAILED
                TestPowerSuppliesTets = FAILED
                GoTo TestComplete
            End If

            For iTerm = 9 To 1 Step -1
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & DC(iTerm))
                nSystErr = WriteMsg(DMM, "MEAS:RES?")
                WaitForResponse(DMM, 0)

                'Assign correct terminal number to callout
                If iTerm > 8 Then
                    iTermNumber = 10
                Else
                    iTermNumber = iTerm
                End If

                'P10-02-001; P08-02-001 ~ P01-02-001
                'Check DC10 for a short
                dMeasurement = FetchMeasurement(DMM, "DC" & CStr(iTermNumber) & " HI/LO Short Test.", "100", "", _
                  "Ohms", PPU, "P" & Format(iTermNumber, "00") & "02-001")
                If dMeasurement < 100 Then
                    'Inform Operator where short was found
                    MsgBox("A short has been detected across DC" & CStr(iTermNumber) & " HI/LO", vbCritical)
                    SupplyStatus = FAILED
                    TestPowerSuppliesTets = FAILED
                    GoTo TestComplete

                End If
            Next
            'Report No Shorts found between DC1 and DC10 Terminals.
        Else
            Echo(FormatResults(True, "DC1 to DC10 Terminal Short Test", "P01-02-001 to P10-02-001"))
            'Open all DC Switches
            For iTerm = 1 To 9
                nSystErr = WriteMsg(SWITCH1, "OPEN " & DC(iTerm))
            Next
        End If
        '
        '   Check for resistors on the power supply output card
        '

        For iTerm = 1 To 9
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & DC(iTerm))
        Next

        setupResistanceMeasurement()
        '    WaitForResponse DMM, 0
        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", "1040", "1300", "Ohms", PPU, "P09-02-002")

        '    If dMeasurement > 10000 Then
        '        MsgBox "Missing Bleed Off Resistors On VXI J1 Connector.", vbCritical
        '        SupplyStatus = FAILED
        '        TestPowerSuppliesTets = FAILED
        '        GoTo TestComplete
        '    End If

        For iTerm = 1 To 9
            nSystErr = WriteMsg(SWITCH1, "OPEN " & DC(iTerm))
        Next


        '+++++++++++++++++++++++++  End DC1-DC10 Terminal Short Test  ++++++++++++++++++++++++++

        '2.0004:  S101-37,   if CLOSED, it provides 8.1 ohms to channels, if OPENED, 13 ohms to DC10.

        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0001-4")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0000-2")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0004")

        DisplaySetup("Connect UTILITY LOADS (HI/LO) to DC POWER SUPPLIES DC1 using cable W28.", "DC1_ULD.BMP", 1)

        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0000")      'Close a relay between DC1 and DC2 to apply LOADS.

        'Under Current Limit and Constant Current modes, verify each voltage limit and current by repeating the loop
        For Supply = 1 To 10
            If SupplyHandle(Supply) = 0 Then  'If this supply not initialized, don't test it, skip to next supply
                ReportFailure(PPU)
                If Supply = 10 Then
                    Exit For
                Else
                    Supply = Supply + 1
                End If
            End If

            SupplyStatus = PASSED
            CommandSetVoltage(Supply, 0)        'to ensure that output cap is discharged
            Delay(0.2)
            CommandSetCurrent(Supply, 5)        'set current limit to 5Amp to each channel; Possible max. voltage is 40.5V

            If TesterVer = "VersionB" Then
                Delay(0.3)
                CommandSetOptions(Supply, 1, SetMaster, SenseLocal, CurrentLimit)
                Delay(0.3)
                CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
                Delay(0.3)
            Else
                Delay(1)
                CommandSetOptions(Supply, CloseRelay, SetMaster, SenseLocal, CurrentLimit)
                Delay(0.8)
            End If

            CommandSetOptions(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
            Delay(0.8)

            If Supply < 10 Then
                CommandSetVoltage(Supply, 35)
            Else
                DisplaySetup("Move the W24 cable connection from DC POWER SUPPLIES DC9 to DC10.", "DC10_DIN.BMP", 1)
                DisplaySetup("Move the W28 cable connection from DC POWER SUPPLIES DC1 to DC10.", "DC10_ULD.BMP", 1)
                'Apply 13 Ohms to DC10 by opening Switch 101-37.
                nSystErr = WriteMsg(SWITCH1, "OPEN  2.0004")       'S101-37
                CommandSetVoltage(Supply, 56)
            End If
            Delay(0.8)

            nSystErr = WriteMsg(DMM, "*RST;*CLS")
            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
            'P01-03-001  ~  P10-03-001
            dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Output Relay Stuck Test", "", "1", "V", _
              PPU, "P" & Format(Supply, "00") & "-03-001")

            If dMeasurement > 10 Then
                SupplyStatus = FAILED
            End If

            CommandSetVoltage(Supply, 0)
            Delay(2)

            If TesterVer = "VersionB" Then
                CommandSetOptions(Supply, 1, SetMaster, SenseRemote, CurrentLimit)
                Delay(0.3)
                CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
            Else
                CommandSetOptions(Supply, CloseRelay, SetMaster, SenseRemote, CurrentLimit)
            End If
            Delay(1)

            If Supply < 10 Then
                CommandSetVoltage(Supply, 35)
            Else
                CommandSetVoltage(Supply, 56)
            End If
            Delay(3)

            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")

            'Each DCPS has +/- 2V tolerance in voltage measurement due to variable resistances in SAIF and switches


            If Supply < 10 Then
                'P01-04-001  ~  P09-04-001
                dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Output Loaded, Voltage Measurement Test", _
                  "33", "37", "V", PPU, "P" & Format(Supply, "00") & "-04-001")
                VoltWithLoad# = dMeasurement

                If dMeasurement < 33 Or dMeasurement > 37 Then
                    SupplyStatus = FAILED
                End If
                If PolarityStatus(Supply) Then
                    CommandSetVoltage(Supply, 0)
                    Delay(1.5)
                    CommandSetOptions(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
                    Delay(0.3)
                    CommandSetPolarity(Supply, "-")

                    If TesterVer = "VersionB" Then
                        CommandSetOptions(Supply, 1, SetMaster, SenseRemote, CurrentLimit)
                        Delay(0.3)
                        CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
                        Delay(0.3)
                    Else
                        Delay(0.3)
                        CommandSetOptions(Supply, CloseRelay, SetMaster, SenseRemote, CurrentLimit)
                        Delay(0.3)
                    End If

                    CommandSetVoltage(Supply, 35)
                    Delay(2)
                    nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
                    'P01-05-001  ~  P09-05-001
                    dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & _
                      " Output Loaded, Negative Voltage Measurement Test", "-37", "-33", "V", PPU, _
                      "P" & Format(Supply, "00") & "-05-001")

                    If dMeasurement > -33 Or dMeasurement < -37 Then
                        Echo("  Power Supply DC" & Supply & " . . . FAILED polarity test.")
                        SupplyStatus = FAILED
                    End If
                    CommandSetVoltage(Supply, 0)
                    Delay(1.5)
                    CommandSetOptions(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
                    Delay(0.3)
                    CommandSetPolarity(Supply, "+")

                    If TesterVer = "VersionB" Then
                        CommandSetOptions(Supply, 1, SetMaster, SenseRemote, CurrentLimit)
                        Delay(0.3)
                        CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
                        Delay(0.3)
                    Else
                        Delay(0.3)
                        CommandSetOptions(Supply, CloseRelay, SetMaster, SenseRemote, CurrentLimit)
                        Delay(0.3)
                    End If

                    CommandSetVoltage(Supply, 35)
                    Delay(2)
                Else
                    Echo("PPU Supply DC" & Supply & " not equipped for polarity switching.")
                End If

            Else
                'P10-04-001
                dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Output Loaded, Voltage Measurement Test", _
                  "54", "58", "V", PPU, "P" & Format(Supply, "00") & "-04-001")
                VoltWithLoad# = dMeasurement

                If dMeasurement < 54 Or dMeasurement > 58 Then
                    Echo("  Power Supply DC" & Supply & " . . . FAILED.")
                    SupplyStatus = FAILED
                End If

                If PolarityStatus(Supply) Then
                    CommandSetVoltage(Supply, 0)
                    Delay(1.5)
                    CommandSetOptions(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
                    Delay(0.3)
                    CommandSetPolarity(Supply, "-")

                    If TesterVer = "VersionB" Then
                        CommandSetOptions(Supply, 1, SetMaster, SenseRemote, CurrentLimit)
                        Delay(0.3)
                        CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
                        Delay(0.3)
                    Else
                        Delay(0.3)
                        CommandSetOptions(Supply, CloseRelay, SetMaster, SenseRemote, CurrentLimit)
                        Delay(0.3)
                    End If

                    CommandSetVoltage(Supply, 56)
                    Delay(2)
                    nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
                    'P10-05-001
                    dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & _
                      " Output Loaded, Negative Voltage Measurement Test", "-58", "-54", "V", PPU, _
                      "P" & Format(Supply, "00") & "-05-001")

                    If dMeasurement > -54 Or dMeasurement < -58 Then
                        SupplyStatus = FAILED
                    End If
                    CommandSetVoltage(Supply, 0)
                    Delay(1.5)
                    CommandSetOptions(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
                    Delay(0.3)
                    CommandSetPolarity(Supply, "+")

                    If TesterVer = "VersionB" Then
                        CommandSetOptions(Supply, 1, SetMaster, SenseRemote, CurrentLimit)
                        Delay(0.3)
                        CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
                        Delay(0.3)
                    Else
                        Delay(0.3)
                        CommandSetOptions(Supply, CloseRelay, SetMaster, SenseRemote, CurrentLimit)
                        Delay(0.3)
                    End If

                    CommandSetVoltage(Supply, 56)
                    Delay(2)
                Else
                    Echo("PPU Supply DC" & Supply & " not equipped for polarity switching.")
                End If
            End If

            current! = CommandMeasureAmps!(Supply)

            test = "PPU Supply DC" & Supply & " Output Loaded, Current Measurement Test"
            'P01-06-001  ~  P10-06-001
            RecordTest("P" & Format(Supply, "00") & "-06-001", test, 3, 4.6, current!, "A")
            If current! < 3 Or current! > 4.6 Then
                RegisterFailure(PPU, "P" & Format(Supply, "00") & "-06-001", Val(current!), "A", 3, 4.6, test)
                SupplyStatus = FAILED
            End If


            'This time by switching to Local Sense mode, verify that the output voltage is drops.

            If TesterVer = "VersionB" Then
                CommandSetOptions(Supply, 1, SetMaster, SenseLocal, CurrentLimit)
                Delay(0.3)
                CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
            Else
                CommandSetOptions(Supply, CloseRelay, SetMaster, SenseLocal, CurrentLimit)
            End If

            Delay(0.3)

            nSystErr = WriteMsg(DMM, "*RST;*CLS")
            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
            'P01-07-001  ~  P10-07-001
            dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Remote Sense Relay Test", _
              "", CStr(VoltWithLoad# - 0.2), "V", PPU, "P" & Format(CStr(Supply), "00") & "-07-001")

            If dMeasurement > (VoltWithLoad# - 0.2) Then
                SupplyStatus = FAILED
            End If

            If Supply = 10 Then
                'Now apply 8.1 Ohm to DC10 by closing the relay
                nSystErr = WriteMsg(SWITCH1, "CLOSE  2.0004")       'S101-37
            End If

            CommandSetVoltage(Supply, 0)
            Delay(1)

            '**Constant Current Mode
            CommandSetCurrent(Supply, 2)
            Delay(0.3)

            If TesterVer = "VersionB" Then
                CommandSetOptions(Supply, 1, SetMaster, SenseLocal, CurrentConstant)
                Delay(1)
                CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
                Delay(0.3)
            Else
                CommandSetOptions(Supply, CloseRelay, SetMaster, SenseLocal, CurrentConstant)
                Delay(1)
            End If

            CommandSetVoltage(Supply, 40)
            Delay(2)
            current! = CommandMeasureAmps!(Supply)

            UpperLimit! = 2.2       '8.1 Ohm x 2 Amp = 16.2 V
            LowerLimit! = 1.8

            test = "PPU Supply DC" & Supply & " Constant Current Mode Test"
            'P01-08-001  ~  P10-08-001
            RecordTest("P" & Format(Supply, "00") & "-08-001", test, 1.8, 2.2, current!, "A")

            If current! > UpperLimit! Or current! < LowerLimit! Then
                RegisterFailure(PPU, "P" & Format(Supply, "00") & "-08-001", Val(current!), "A", 1.8, 2.2, test)
                SupplyStatus = FAILED
            End If
            CommandSetVoltage(Supply, 0)
            Delay(1)

            '** Master & Slave Mode Test
            If (Supply > 1) And (Supply < 10) Then
                If SupplyHandle(Supply - 1) <> 0 Then
                    If TesterVer = "VersionB" Then
                        For LoopCount = 1 To 3
                            CommandSupplyReset(Supply)
                            Delay(0.7)
                            CommandSupplyReset(Supply - 1)
                            Delay(0.7)

                            'Slave settings
                            CommandSetOptions(Supply, 1, SetSlave, SenseLocal, CurrentLimit)
                            Delay(0.7)
                            CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
                            Delay(0.7)

                            'Master Settings
                            CommandSetOptions(Supply - 1, 1, SetMaster, SenseLocal, CurrentLimit)
                            Delay(0.5)
                            CommandSetCurrent(Supply - 1, 5)   'assume total 5Amp need to be derived
                            Delay(0.5)
                            CommandSetVoltage(Supply - 1, 35)
                            Delay(0.5)
                            CommandSetOptions(Supply - 1, CloseRelay, 1, 1, 1)
                            Delay(3)

                            CurrentMaster! = CommandMeasureAmps!(Supply - 1)
                            Delay(0.5)
                            CurrentSlave! = CommandMeasureAmps!(Supply)

                            If CurrentSlave! < 3.5 And CurrentSlave! > 1.5 Then
                                Exit For
                            End If
                        Next LoopCount
                    Else
                        CommandSetOptions(Supply, CloseRelay, SetSlave, SenseLocal, CurrentLimit)
                        Delay(0.2)
                        CommandSetOptions(Supply - 1, CloseRelay, SetMaster, SenseLocal, CurrentLimit)
                        Delay(1)
                        CommandSetCurrent(Supply - 1, 5)   'assume total 5Amp need to be derived
                        Delay(0.2)
                        CommandSetVoltage(Supply - 1, 40)
                        Delay(1.5)
                        CurrentMaster! = CommandMeasureAmps!(Supply - 1)
                        Delay(0.5)
                        CurrentSlave! = CommandMeasureAmps!(Supply)
                    End If

                    test = "PPU Supply DC" & Supply & " Slave Mode Test"
                    'P01-09-001  ~  P10-09-001
                    RecordTest("P" & Format(Supply, "00") & "-09-001", test, 1.5, 3.5, CurrentSlave!, "A")

                    If CurrentSlave! > 3.5 Or CurrentSlave! < 1.5 Then
                        RegisterFailure(PPU, "P" & Format(Supply, "00") & "-09-001", Val(CurrentSlave!), "A", 1.5, 3.5, test)
                        SupplyStatus = FAILED
                        Echo("Also check Master Supply DC" & (Supply - 1))
                    End If
                    Delay(0.2)
                    CommandSetVoltage(Supply - 1, 0)
                    Delay(1)
                    CommandSetOptions(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
                    Delay(0.4)
                    CommandSupplyReset(Supply - 1)
                End If
            End If

            CommandSupplyReset(Supply)

            'Current Excess Shut-down Test
            CommandSetCurrent(Supply, 3)        'set current limit to 3Amp to each channel; Possible max. voltage is 24.3V

            If TesterVer = "VersionB" Then
                Delay(0.3)
                CommandSetOptions(Supply, 1, SetMaster, SenseRemote, CurrentLimit)
                Delay(0.3)
                CommandSetOptions(Supply, CloseRelay, 1, 1, 1)
                Delay(0.3)
            Else
                Delay(0.6)
                CommandSetOptions(Supply, CloseRelay, SetMaster, SenseRemote, CurrentLimit)
                Delay(0.6)
            End If

            CommandSetVoltage(Supply, 35)
            Delay(3)
            VoltageMeas! = CommandMeasureVolts!(Supply)

            If VoltageMeas! > 2 Then
                Delay(1.5)
                VoltageMeas! = CommandMeasureVolts!(Supply)
            End If

            test = "PPU Supply DC" & Supply & " Excess Current Shut-down Test"
            'P01-10-001  ~  P10-10-001
            RecordTest("P" & Format(CStr(Supply), "00") & "-10-001", test, 0, 2, VoltageMeas!, "V")

            If VoltageMeas! > 2 Then
                RegisterFailure(PPU, "P" & Format(Supply, "00") & "-10-001", Val(VoltageMeas!), "V", 0, 2, test)
                SupplyStatus = FAILED
            End If

            Delay(0.2)
            CommandSupplyReset(Supply)
            Echo("")

            If SupplyStatus = PASSED Then
                Echo("PPU Supply DC" & Supply & " has PASSED.")
            Else
                Echo("PPU Supply DC" & Supply & " has FAILED.")
                TestPowerSuppliesTets = FAILED
            End If
            Echo("")
            Echo("")
            frmSTest.proProgress.Value = (Supply * 10)
        Next Supply

        'Open all the closed relays
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.0000-4")
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.0000-4")

        If TestPowerSuppliesTets = PASSED Then
            ReportPass(PPU)
            TestPowerSuppliesTets = PASSED
        Else
            TestPowerSuppliesTets = FAILED
        End If
TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        Application.DoEvents()
        If AbortTest = True Then
            Echo("********************")
            Echo(" PPU Test Aborted")
            Echo(" Resetting DC1-10")
            Echo("   Please Wait")
            Echo("********************")
        End If
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        ResetPowerSupplies()

        If AbortTest = True Then
            If TestPowerSuppliesTets = FAILED Then
                ReportFailure(PPU)
            Else
                ReportUnknown(PPU)
            End If
        ElseIf TestPowerSuppliesTets = PASSED Then
            ReportPass(PPU)
        ElseIf TestPowerSuppliesTets = FAILED Then
            ReportFailure(PPU)
        Else
            ReportUnknown(PPU)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If



    End Function

    Function CommandMeasureAmps(ByVal Supply As Integer) As Single
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module Takes A Current Measurement From the     *
        '*     internal DMM of a power supply                       *
        '*    EXAMPLE:                                              *
        '*     Amps! = CommandMeasureAmps!(3)                       *
        '*    PARAMETERS:                                           *
        '*     Supply%   =  The supply number from which to measure *
        '*    RETURNS:                                              *
        '*     The measured current in amps                         *
        '************************************************************
        Dim B1 As Integer
        Dim B2 As Integer
        Dim B3 As Integer
        Dim D1 As Single
        Dim D2 As Single
        Dim data As String
        Dim ReadBuffer As String = Space(255)
        Dim ErrorStatus As Integer
        Dim ReadBufferA As String

        CommandMeasureAmps = 0

        '*    0S, 42, 00   Measurement Query to supply S            *
        B1 = Supply
        B2 = &H42
        B3 = 0 ' changed to 0 for TETS, was 128 for the TETS bb

        'Send Command
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

        'Read Sense Measurement
        ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer)
        data = ReadBuffer
        If Asc(Strings.Mid(data, 1, 1)) > 64 Then 'byte1
            ' 12 bit current reading ccc - divide by 500 to get amps
            D1 = (Asc(Strings.Mid(data, 1, 1))) And 15 ' byte1 (msb bits 0-3)
            D1 *= 256 ' hi byte
            D2 = (Asc(Strings.Mid(data, 2, 1))) ' byte2 lsb
            ReadBufferA = Format(((D1 + D2) / 500.0), "0.000") ' amps
            CommandMeasureAmps = CSng(Val(ReadBufferA))
        End If
        Application.DoEvents()

    End Function


    Function CommandMeasureVolts(ByVal Supply As Integer) As Single
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module Takes A Voltage Measurement From the     *
        '*     internal DMM of a power supply                       *
        '*    EXAMPLE:                                              *
        '*     Volts! = CommandMeasureVolts!(4)                     *
        '*    PARAMETERS:                                           *
        '*     Supply%   =  The supply number from which to measure *
        '*    RETURNS:                                              *
        '*     The measured voltage in volts                        *
        '************************************************************
        Dim B1 As Integer
        Dim B2 As Integer
        Dim B3 As Integer
        Dim D1 As Single
        Dim D2 As Single
        Dim data As String
        Dim ReadBuffer As String = Space(255)
        Dim ReadBufferV As String
        Dim ErrorStatus As Integer

        'Program Query Supply For Sense Measurement
        '*    0S, 42, 00   Measurement Query to supply S            *
        B1 = Supply
        B2 = &H42
        B3 = 0 ' changed to 0 for TETS, was 128 for the TETS bb

        'Send Command
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))
        'Read Sense Measurement
        ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer)
        'Voltage Measurement
        'only update display if valid data is returned (not zeros)

        data = ReadBuffer
        If Asc(Strings.Mid(data, 1, 1)) > 64 Then
            'voltage measurement ' divide by 100 unless DC10 (divide by 50)
            D1 = (Asc(Strings.Mid(data, 3, 1))) And 15
            D1 *= 256
            D2 = (Asc(Strings.Mid(data, 4, 1)))
            ReadBufferV = (D1 + D2) / 100

            '65 Volt Supply Re-Scale Voltage Here
            If Supply = 10 Then
                D1 = (Asc(Strings.Mid(data, 3, 1))) And 15
                D1 *= 256
                D2 = (Asc(Strings.Mid(data, 4, 1)))
                ReadBufferV = (D1 + D2) / 50
            End If
            CommandMeasureVolts = ReadBufferV
        Else
            CommandMeasureVolts = -99
        End If
        Application.DoEvents()

    End Function


    Sub CommandSetOptions(ByVal Supply As Integer, ByVal OpenRelay As Integer, ByVal SetMaster As Integer, ByVal SenseLocal As Integer, ByVal ConstantVoltage As Integer)
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module sets the optional settings of a power    *
        '*     supply                                               *
        '*    EXAMPLE:                                              *
        '*     CommandSetOptions 4, False, True, False, False       *
        '*    Note: If a +1 is passed do nothing (0)                *
        '*     CommandSetOptions 4, 1, 1, True, False               *
        '*                                                          *
        '*    PARAMETERS:                                           *
        '*     Supply%    =  The supply number of which to set      *
        '*     OpenRelay% =  True to open the output relay          *
        '*                   False to close the output relay        *
        '*     SetMaster% =  True to set the supply to master mode  *
        '*                   False to set the supply to slave mode  *
        '*     SenseLocal%=  True to set to local sense             *
        '*                   False to set to remote sense           *
        '*     ConstantVoltage% = True for constant Voltage mode    *
        '*                   False for constant current mode        *
        '************************************************************

        '2s,B0,00 Close Relay s
        '2s,A0,00 Open Relay s
        '2s,80,03 Reverse Polarity s
        '2s,80,02 Normal Polarity s
        '2s,8C,00 Slave s
        '2s,88,00 Master s
        '2s,83,00 Remote Sense s
        '2s,82,00 Local Sense s
        '2s,80,30 Constant Current s
        '2s,80,20 Constant Voltage s
        '2s,80,40 Nofault s
        '2s,80,08 Disable Fail Safe s

        'B1  = 2s
        '      ******8*********7*********6*********5*********4**********3*********2*********1*****
        'B2 =1 * change   *         * En C/O   * Closed * En M/S   * Slave   * En L/R   * Remote *
        '    0 * Nochange *    0    * Disable  * Open   * Disable  * Master  * Disable  * Local  *
        '      ******8*********7*********6*********5*********4**********3*********2*********1*****
        'B3= 1 *          *         * En CC/CV * CCurr  * Dis FS   * Nofault * En R/N   * Revers *
        '    0 *          *         * Disable  * CVolt  * FailSafe * Dis NF  * Disable  * Normal *
        '      ******8*********7*********6*********5*********4**********3*********2*********1*****

        Dim B1 As Integer
        Dim B2 As Integer
        Dim B3 As Integer

        B1 = &H20 + Supply
        Select Case OpenRelay
            Case True   '(-1)
                B2 += &H20 'Enable / Relay Open
            Case False  '( 0)
                B2 += &H30 'Enable / Relay Closed
        End Select
        Select Case SetMaster
            Case True  '(-1)
                B2 += &H8 'Enable / Set as Master
            Case False  '( 0)
                B2 += &HC 'Enable / Set as Slave
        End Select
        Select Case SenseLocal
            Case True  '(-1)
                B2 += 2 'Enable / Sense Local
            Case False '( 0)
                B2 += 3 'Enable / Sense Remote
        End Select
        Select Case ConstantVoltage
            Case True '(-1)
                B3 += &H20 'Enable / Current Limiting(Protection)
            Case False  '( 0)
                B3 += &H30 'Enable / Constant Current
        End Select

        If (B2 > 0) Or (B3 > 0) Then
            B2 += &H80
        End If

        'Send Command
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub


    Public Sub CommandSetVoltage(ByVal Supply As Integer, ByVal Volts2 As Single)
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module set the output voltage of a power supply *
        '*    EXAMPLE:                                              *
        '*     CommandSetVoltage 4, .5                              *
        '*    PARAMETERS:                                           *
        '*     Supply%   =  The supply number of which to set       *
        '*     Current!  =  The current in amps for which to set    *
        '*                  the supply                              *
        '*                                                          *
        '*     'Set voltage command = 2s, 5z, zz                    *
        '************************************************************

        Dim B1 As Integer
        Dim B2 As Integer
        Dim B3 As Integer
        Dim VoltsX As Integer
        Dim Volts As Single

        Volts = Volts2

        'Convert To Send Format
        If Supply = 10 Then
            Volts *= 50 '20mV Resolution (1/.02=50)
        Else
            Volts *= 100 '10mV Resolution (1/.01=100)
        End If
        VoltsX = CInt(Volts)

        B1 = &H20 + Supply
        B2 = &H50 + CInt(VoltsX \ 256) ' Note: integer division
        B3 = CInt(VoltsX Mod 256)

        'Send Command
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub


    Public Sub CommandSetPolarity(ByVal Supply As Integer, ByVal polarity As String)
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module set the output polarity of a power supply*
        '*    EXAMPLE:                                              *
        '*     CommandSetPolarity                                   *
        '*    PARAMETERS:                                           *
        '*     Supply%   =  The supply number of which to set       *
        '*     Polarity%  =  The polarity "-" or "+"                *
        '* Procedure added per ECO-3047-078 for polarity testing.   *
        '************************************************************

        Dim Byte3 As Integer

        If CStr(polarity) = "-" Then
            Byte3 = &H3 'Negative
        Else
            Byte3 = &H2 'Default to Positive
        End If

        'Send Command
        SendDCPSCommand(Supply, Chr(32 + Supply) & Chr(&H80) & Chr(Byte3)) 'Set polarity

    End Sub


    Sub CommandSupplyReset(Supply As Integer)
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module resets a power supply                    *
        '*    EXAMPLE:                                              *
        '*     CommandSupplyReset 4                                 *
        '*    PARAMETERS:                                           *
        '*     Supply%    =  The supply number of which to reset    *
        '************************************************************

        Dim B1 As Integer
        Dim B2 As Integer
        Dim B3 As Integer

        'Send "Open All Relays" Command  2S, A0, 00   Open Output Relay on supply S (default)  *
        B1 = &H20 + Supply
        B2 = &HA0 'was &HAA  bb
        B3 = 0 ' was &H20
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

        'Send "Reset" Command 1S, 00, 00   Reset Supply S(1-A)                      *
        B1 = &H10 + Supply
        B2 = 0 ' was 128  bb
        B3 = 0 ' was 128  bb
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub


    Function ConvertHexChrToBin(HexChars As String) As String
        '************************************************************
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module converts a hex digit to a binary string  *
        '*    EXAMPLE:                                              *
        '*     BinString = ConvertHexChrToBin("A")                  *
        '*    PARAMETERS:                                           *
        '*     HexChars  =  The Hex charater                        *
        '*    RETURNS:                                              *
        '*     The Binary string                                    *
        '************************************************************


        Dim Conv As String

        Conv = "0000"
        Select Case HexChars
            Case "0"
                Conv = "0000"
            Case "1"
                Conv = "0001"
            Case "2"
                Conv = "0010"
            Case "3"
                Conv = "0011"
            Case "4"
                Conv = "0100"
            Case "5"
                Conv = "0101"
            Case "6"
                Conv = "0110"
            Case "7"
                Conv = "0111"
            Case "8"
                Conv = "1000"
            Case "9"
                Conv = "1001"
            Case "A"
                Conv = "1010"
            Case "B"
                Conv = "1011"
            Case "C"
                Conv = "1100"
            Case "D"
                Conv = "1101"
            Case "E"
                Conv = "1110"
            Case "F"
                Conv = "1111"
        End Select
        ConvertHexChrToBin = Conv

    End Function


    Sub ResetPowerSupplies()
        '************************************************************
        '* ManTech Test Systems Software Sub                        *
        '************************************************************
        '* Nomenclature   : APS 6062 UUT Power Supplies             *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Resets The Hardware and Software                     *
        '*     To It's Default Power-On State                       *
        '*    EXAMPLE:                                              *
        '*      ResetPowerSupplies                                  *
        '************************************************************
        Dim SupplyIndex As Integer

        'Insure no supplies are slaved
        For SupplyIndex = 9 To 1 Step -1
            CommandSetOptions(SupplyIndex, True, True, True, True)
            Delay(0.2)
        Next SupplyIndex

        For SupplyIndex = 10 To 1 Step -1
            CommandSupplyReset(SupplyIndex)
            Delay(0.2)
        Next SupplyIndex
    End Sub


    Sub SendDCPSCommand(slot As Integer, Command As String)
        '************************************************************
        '* Nomenclature   : APS 6062 UUT Power Supplies             *
        '*    DESCRIPTION:                                          *
        '*     Send a command string to the power supplies          *
        '*    EXAMPLE:                                              *
        '*      SendDCPSCommand -(3, Command)                       *
        '*    PARAMTERS:                                            *
        '*     slot%     = The slot number of the supply where to   *
        '*                 send the command                         *
        '*     Command  = The command string to send to the supply *
        '*                                                          *
        '*     NOTE: All commands must be three bytes in length     *
        '*           even if one of the bytes is a null byte.       *
        '*           All programmed voltages for the 40V supplies   *
        '*           need to be multiplied by 100 (65V by 50) and   *
        '*           converted to 12 bits.                          *
        '*           All programmed currents need to be multiplied  *
        '*           by 500 and converted to 12 bits.               *
        '************************************************************

        Dim ErrorStatus As Integer
        Dim Nbr As Integer = 0

        If Len(Command) <> 3 Then Exit Sub
        '  ErrorStatus& = aps6062_writeInstrData(SupplyHandle&(slot%), writeBuffer:=Command)
        ErrorStatus = atxmlDF_viWrite(PsResourceName(slot), 0, Command, CInt(Command.Length), Nbr)

        Delay(0.5)
        If ErrorStatus <> 0 Then
            '  Echo "Power Supply #"+str(slot%)+" Error#" &Cstr(ErrorStatus&)) 'bb temp
        End If

    End Sub

    Function ReadDCPSCommand(ByVal SupplyX As Integer, ByRef ReadBuffer As String) As Integer

        'ex:   ErrorStatus& = ReadDCPSCommand(Supply%, ReadBuffer)

        Dim Nbr As Integer
        Dim S As String, i As Integer

        ReadBuffer = Space(255)
        ErrorStatus = atxmlDF_viRead(PsResourceName(SupplyX), 0, ReadBuffer, 255, Nbr)

        'the power supply should always return 5 bytes for a status read command
        If Nbr = 5 Then
            ReadBuffer = Strings.Left(ReadBuffer, Nbr)
        Else
            S = ""
            For i = 1 To Nbr
                S &= "Byte" & CStr(i) & " = " & CStr(Asc(Strings.Mid(ReadBuffer, i, 1))) & vbCrLf
            Next i

            Echo("HW error, Freedom Status Byte count should always be 5,  count = " & CStr(Nbr) & vbCrLf & "Read Status=" & S & vbCrLf & "Reading status again")
            ReadBuffer = Space(255)
            ErrorStatus = atxmlDF_viRead(PsResourceName(SupplyX), 0, ReadBuffer, 255, Nbr)
            If Nbr = 5 Then
                ReadBuffer = Strings.Left(ReadBuffer, Nbr)
            End If
        End If
        ReadDCPSCommand = ErrorStatus
        Delay(0.5)

    End Function

    Function SelfTestPPS(ByVal Supply As Integer) As Integer

        'This routine will BIT test selected power supply
        'DESCRIPTION:
        '   This routine tests the power supplies
        'Returns:
        '   PASSED if the supplies are passing and FAILED if they fail

        Dim ReadBuffer As String = Space(255)
        Dim TestStatus As Short
        Dim CommandData As String
        Dim ErrorStatus As Integer
        Dim Nbr As Integer
        Dim ErrCode As Integer
        Dim iErrStatusCode As Integer 'Error Status Code
        Dim sErrorText(13) As String 'Error Text Array
        Dim sErrText As String 'Error Text to display in Message Box
        Dim i As Short 'Index for loop
        Dim sPPU_Test_Number As String

        '    If SupplyHandle&(Supply%) = 0 Then
        '      TestStatus% = FAILED
        '      SelfTestPPS% = TestStatus%
        '      Exit Function
        '    End If

        TestStatus = PASSED

        'P01-01-001 ~ P10-01-001
        'Query supply and check self test data which should be 80H if passing
        ReadBuffer = ""
        '*    1S, 00, 00   Reset Supply S(1-10)
        CommandData = Chr(&H10 + Supply) & Chr(&H0) & Chr(&H0) 'reset supply
        '  ErrorStatus& = aps6062_writeInstrData(SupplyHandle&(Supply%), writeBuffer:=CommandData)
        ErrorStatus = atxmlDF_viWrite(PsResourceName(Supply), 0, CommandData, CInt(Len(CommandData)), Nbr)
        Delay(5)

        Array.Clear(sErrorText, 0, sErrorText.Length)
        sErrText = ""

        sPPU_Test_Number = "P" & Format(Supply, "00") & "-01-001" 'Build Test Number

        '*    0S, 44, 00   Status Query to supply S
        CommandData = Chr(Supply) & Chr(&H44) & Chr(&H0) 'Status query
        '    ErrorStatus& = aps6062_writeInstrData(SupplyHandle&(Supply%), writeBuffer:=CommandData)
        ErrorStatus = atxmlDF_viWrite(PsResourceName(Supply), 0, CommandData, CInt(Len(CommandData)), Nbr)
        Delay(1)

        'get status query returned data
        ReadBuffer = ""
        '   Echo vbCrLf & "*** TEST DC" & CStr(Supply) & " ***"
        ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer) ' get 5 bytes

        '*   Status Query Byte 1  (Returned Data)                   *
        '*   Bit 0 L/R     Sense Mode 0=local, 1=Remote             *
        '*       1 x                                                *
        '*       2 M/S     Mode 0=Master, 1=Slave                   *
        '*       3 x                                                *
        '*       4 O/C     Isolation Relay 0=open, 1=closed         *
        '*       5 1                                                *
        '*       6 NFM     1=NoFault Mode                           *
        '*       7 0                                                *
        '*                                                          *
        '*   Status Query Byte 2 (Status Byte)                      *
        '*   Bit 0 UV      Under Voltage faults (over current)      *
        '*       1 STF     Self Test Failure                        *
        '*       2 CC      Constant Current Mode                    *
        '*       3 OC      Overcurrent Fault                        *
        '*       4 OV      Overvoltage Fault                        *
        '*       5 IC      Invalid Command or sequence              *
        '*       6 CAL     Cal in progress                          *
        '*       7 STP     Self Test Passed (result of last test)   *
        '*                                                          *
        '*   Status Query Byte 3 (Bit Fail Test)                    *
        '*   Bit 0 2V      2V test failed                           *
        '*       1 10V     10V test failed                          *
        '*       2 20V     20V test failed                          *
        '*       3 40V     40V test failed                          *
        '*       4 65V     65V test failed                          *
        '*       5 EE      EEprom test failed                       *
        '*       6 x                                                *
        '*       7 x                                                *
        '*                                                          *
        '*   Status Query Byte 4 (Rev of firmware  High.Low)        *
        '*   Bit 0-2       Low                                      *
        '*       4-7       High                                     *
        '*                                                          *
        '*   Status Query Byte 5 (EEprom Read Data)                 *
        '*                                                          *
        '*                                                          *

        If Asc(Strings.Mid(ReadBuffer, 2, 1)) = &H80 Then 'Passed
            Echo(FormatResults(True, "PPU Supply DC" & CStr(Supply) & " Built-In Test", sPPU_Test_Number))
        Else            'Failed
            TestStatus = FAILED
            ErrCode = Asc(Strings.Mid(ReadBuffer, 3, 1))
            'Identify the cause of the failure and report
            If ErrorStatus <> 0 Then
                Echo("GPIB Communication Error.  Timeout occurred.")
                Echo(FormatResults(False, "PPU Supply DC" & CStr(Supply) & " Built-In Test", sPPU_Test_Number))
                RegisterFailure(CStr(PPU), sPPU_Test_Number, , , , , "PPU Supply DC" & CStr(Supply) & " Built-In Test  =>   FAILED" & vbCrLf & "GPIB Communication Error.  Timeout occurred.")
            Else
                'Check Status Byte (BYTE2)
                iErrStatusCode = Asc(Strings.Mid(ReadBuffer, 2, 1))

                If (iErrStatusCode And &H40) <> 0 Then
                    sErrorText(8) = "Calibration in Progress"
                End If
                If (iErrStatusCode And &H20) <> 0 Then
                    sErrorText(9) = "Invalid Command or Sequence"
                End If
                If (iErrStatusCode And &H10) <> 0 Then
                    sErrorText(10) = "Over Voltage Fault"
                End If
                If (iErrStatusCode And &H8) <> 0 Then
                    sErrorText(11) = "Over Current Fault"
                End If
                If (iErrStatusCode And &H4) <> 0 Then
                    sErrorText(12) = "Constant Current Mode"
                End If
                If (iErrStatusCode And &H1) <> 0 Then
                    sErrorText(13) = "Under Voltage Fault (may also indicate certain over current faults)"
                End If

                'If BIT Failed, Find out why BIT Failed  (BYTE3)
                If Asc(Strings.Mid(ReadBuffer, 2, 1)) <> &H80 Then
                    ErrCode = Asc(Strings.Mid(ReadBuffer, 3, 1))
                    If (ErrCode And &H20) <> 0 Then
                        sErrorText(1) = "EEPROM test failed"
                    End If
                    If (ErrCode And &H10) <> 0 Then
                        sErrorText(2) = "65V test failed"
                    End If
                    If (ErrCode And &H8) <> 0 Then
                        sErrorText(3) = "40V test failed"
                    End If
                    If (ErrCode And &H4) <> 0 Then
                        sErrorText(4) = "20V test failed"
                    End If
                    If (ErrCode And &H2) <> 0 Then
                        sErrorText(5) = "10V test failed"
                    End If
                    If (ErrCode And &H1) <> 0 Then
                        sErrorText(6) = "2V test failed"
                    End If
                    If ErrCode = 0 Then
                        sErrorText(7) = "Failed to respond"
                    End If
                End If
                'Build Error Text Message
                For i = 1 To 13
                    If Len(sErrorText(i)) > 0 Then
                        If Len(sErrText) > 0 Then
                            sErrText &= ", " & sErrorText(i)
                        Else
                            sErrText = sErrorText(i)
                        End If
                    End If
                Next
                Echo(FormatResults(False, "PPU Supply DC" & CStr(Supply) & " Built-In Test  =>", sPPU_Test_Number))
                Echo("    " & sErrText)
                RegisterFailure(CStr(PPU), sPPU_Test_Number, sComment:="PPU Supply DC" & CStr(Supply) & " Built-In Test  =>   FAILED" & vbCrLf & "Power Supply " & CStr(Supply) & "  :  " & sErrText)
            End If
        End If

        'Check to see if Firmware Version number has Polarity switching capabilities (BYTE4)
        If Asc(Strings.Mid(ReadBuffer, 4, 1)) > &H13 Then
            PolarityStatus(Supply) = True
        Else
            PolarityStatus(Supply) = False
        End If

        SelfTestPPS = TestStatus

    End Function



    Public Sub CommandSetCurrent(ByVal Supply As Integer, ByVal Current2 As Single)
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module set the current limit of a power supply  *
        '*    EXAMPLE:                                              *
        '*     CommandSetCurrent! 4, .5                             *
        '*    PARAMETERS:                                           *
        '*     Supply%   =  The supply number of which to set       *
        '*     Current!  =  The current in amps for which to set    *
        '*                  the supply                              *
        '************************************************************
        Dim B1 As Integer
        Dim B2 As Integer
        Dim B3 As Integer
        Dim CurX As Integer
        Dim Current As Single

        Current = Current2

        'Convert To Send Format
        CurX = CInt(Current * 500) '2ma Resolution (1/.002=500)
        'ie, 1 amp = 1F4 (500), 5 amps = 9C4 (2500)

        ' Set Current Limit = 2s, 4z, zz
        B1 = &H20 + Supply
        B2 = &H40 + CInt(CurX \ 256) ' note: integer division
        B3 = CInt(CurX Mod 256)

        'Send Command
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub
    Public Sub setupResistanceMeasurement()

        Dim StrMeasurement As String = ""

        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        nSystErr = WriteMsg(DMM, "SENS:FUNC " & Chr(34) & "RES" & Chr(34))
        nSystErr = WriteMsg(DMM, "SENS:RES:APER 167E-3")
        nSystErr = WriteMsg(DMM, "SENS:RES:NPLC 10")
        nSystErr = WriteMsg(DMM, "SENS:RES:RANG:AUTO OFF")
        nSystErr = WriteMsg(DMM, "SENS:RES:RANG 1000000")
        nSystErr = WriteMsg(DMM, "SENS:ZERO:AUTO OFF")
        nSystErr = WriteMsg(DMM, "TRIG:SOUR IMM")
        nSystErr = WriteMsg(DMM, "TRIG:DEL:AUTO ON")
        nSystErr = WriteMsg(DMM, "INIT")
        nSystErr = WriteMsg(DMM, "FETC?")
        nSystErr = ReadMsg(DMM, StrMeasurement)
        nSystErr = WriteMsg(DMM, "SYST:ERR?")
        nSystErr = ReadMsg(DMM, StrMeasurement)
        Delay(17.1)
        nSystErr = WriteMsg(DMM, "SENS:FUNC " & Chr(34) & "RES" & Chr(34))
        nSystErr = WriteMsg(DMM, "SENS:RES:APER 167E-3")
        nSystErr = WriteMsg(DMM, "SENS:RES:NPLC 10")
        nSystErr = WriteMsg(DMM, "SENS:RES:RANG:AUTO OFF")
        nSystErr = WriteMsg(DMM, "SENS:RES:RANG 1000000")
        nSystErr = WriteMsg(DMM, "SENS:ZERO:AUTO OFF")
        nSystErr = WriteMsg(DMM, "TRIG:SOUR IMM")
        nSystErr = WriteMsg(DMM, "TRIG:DEL:AUTO ON")
        nSystErr = WriteMsg(DMM, "INIT")
        nSystErr = WriteMsg(DMM, "FETC?")

    End Sub

    Sub DumpPPURomData()
        ' shift/control function F12 dump PPU Rom data

        Dim Supply As Integer
        Dim EndAddr As Integer
        Dim Addr As Integer
        Dim StepX As Integer
        Dim S As String
        Dim S1 As String
        Dim RomData As Byte
        ' debug stuff to test read/write calibration rom data bbbb
        ' 0s,adr1,adr2  Read a byte from non-volatile memory

        AbortDump = False
        Supply = DumpSupply
        If Supply <> 10 Then
            EndAddr = &HFA0
        Else
            EndAddr = &HCB2
        End If
        S = vbCrLf + "**** Dump DC" + CStr(Supply) + " Rom Data *****" + vbCrLf
        frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + S

        For Addr = DumpAddr To EndAddr
            If Supply <> 10 Then ' 10mV per division
                S = "DC" + CStr(Supply) + vbTab + Format(Addr / 100, "0.00") + "V" + vbTab
            Else                ' 20mV per division
                S = "DC10" + vbTab + Format(Addr / 50, "0.00") + "V" + vbTab
            End If
            S1 = ""
            For StepX = 1 To 10 ' show ten values on one line
                SendDCPSCommand(Supply, CStr(Chr(Supply)) & CStr(Chr(Addr \ 256)) & CStr(Chr(Addr Mod 256)))
                SendDCPSCommand(Supply, CStr(Chr(Supply)) & CStr(Chr(&H44)) & CStr(Chr(0))) ' status query
                ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer) ' get 5 byte status
                RomData = Asc(Mid(ReadBuffer, 5, 1))
                If RomData < 0 Then
                    S1 += Format(RomData, "00") + " " ' negative values
                Else
                    S1 += " " + Format(RomData, "00") + " " 'positive values
                End If
                Application.DoEvents()
                If AbortDump = True Or frmOptions.FrDumpRomData.Visible = False Then
                    frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + S + S1 + vbCrLf + "Dump Aborted" + vbCrLf + vbCrLf
                    Exit Sub
                ElseIf StepX <> 10 Then
                    Addr += 1
                End If
                Application.DoEvents()
            Next StepX
            frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + S + S1 + vbCrLf
        Next Addr

    End Sub

    Sub DumpPPUStatus()
        '  shift/control function F10 dump PS status

        Dim Supply As Integer
        Dim ReadBuffer As String
        Dim sErrorText(13) As String 'Error Text Array
        Dim sErrText As String 'Error Text to display in Message Box
        Dim CommandData As String
        Dim i As Integer
        Dim temp As String
        Dim Nbr As Integer
        Dim S As String

        S = vbCrLf + "**** Dump DC1-DC10 status register *****" + vbCrLf
        frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + S
        'Note: this software will display the status byte for all 10 supplies
        For Supply = 1 To 10

            'Query supply and check self test data which should be 80H if passing
            ReadBuffer = ""
            Array.Clear(sErrorText, 0, sErrorText.Length)
            sErrText = ""

            '*    0S, 44, 00   Status Query to supply S
            CommandData = CStr(Chr(Supply)) & CStr(Chr(&H44)) & CStr(Chr(&H0)) 'Status query
            '      ErrorStatus& = aps6062_writeInstrData(SupplyHandle&(Supply%), writeBuffer:=CommandData)
            nSystErr = atxmlDF_viWrite(PsResourceName(Supply), 0, CommandData, 3, Nbr)
            Delay(1)

            'get status query returned data
            ReadBuffer = ""
            ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer) ' get 5 bytes
            temp = ""
            For i = 1 To 5
                temp += Strings.Right("00" & Hex(Asc(Mid(ReadBuffer, i, 1))), 2) + " "
            Next i
            S = "DC" + CStr(Supply) + " status =" + temp + vbCrLf
            frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + S
            Application.DoEvents()
            If AbortDump = True Then
                frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + "Dump Aborted" + vbCrLf + vbCrLf
                Exit For
            End If
        Next Supply

    End Sub

    Sub DumpPPUCalDates()
        ' shift/control function F11 dump PPU Cal dates

        Dim Supply As Integer
        Dim S As String
        ' debug stuff to test read/write calibration dates bbbbb
        ' Test read write the date

        S = vbCrLf + "**** Dump DC1-DC10 Cal Dates *****" + vbCrLf
        frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + S
        For Supply = 1 To 10

            '      Wcentury = 20
            '      Wyear = 6
            '      Wmonth = 6
            '      Wday = 30
            '      If Supply = 1 Then' 2006 06 30
            '        Call CommandCalDateWrite(Supply) ' read  Rcentury,Ryear,Rmonth,Rday
            '      End If
            CommandCalDateRead(Supply) ' read  Rcentury,Ryear,Rmonth,Rday
            S = "Read Supply " + CStr(Supply) + vbTab + " " + Format(Rcentury, "00") + Format(Ryear, "00") + "-" + Format(Rmonth, "00") + "-" + Format(Rday, "00") & vbCrLf
            frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + S
            Application.DoEvents()
            Application.DoEvents()
            If AbortDump = True Then
                frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + "Dump Aborted" + vbCrLf + vbCrLf
                Exit For
            End If
        Next Supply

    End Sub


    Function CommandCalDateRead(ByVal Supply As Integer) As Integer

        '************************************************************
        '*    DESCRIPTION:                                          *
        '************************************************************
        Dim ReadBuffer As String = ""
        Dim ErrorStatus As Integer

        CommandCalDateRead = -1

        'century
        SendDCPSCommand(Supply, Chr(Supply) & Chr(&H6F) & Chr(&HAA))
        SendDCPSCommand(Supply, Chr(Supply) & Chr(&H44) & Chr(0))
        ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer)
        Rcentury = Asc(Strings.Mid(ReadBuffer, 5, 1))

        'year
        SendDCPSCommand(Supply, Chr(Supply) & Chr(&H6F) & Chr(&HAB))
        SendDCPSCommand(Supply, Chr(Supply) & Chr(&H44) & Chr(0))
        ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer)
        Ryear = Asc(Strings.Mid(ReadBuffer, 5, 1))

        'month
        SendDCPSCommand(Supply, Chr(Supply) & Chr(&H6F) & Chr(&HAC))
        SendDCPSCommand(Supply, Chr(Supply) & Chr(&H44) & Chr(0))
        ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer)
        Rmonth = Asc(Strings.Mid(ReadBuffer, 5, 1))

        'day
        SendDCPSCommand(Supply, Chr(Supply) & Chr(&H6F) & Chr(&HAD))
        SendDCPSCommand(Supply, Chr(Supply) & Chr(&H44) & Chr(0))
        ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer)
        Rday = Asc(Strings.Mid(ReadBuffer, 5, 1))

    End Function


    Private Sub savethis()

        '    If dMeasurement > 10000 Then
        '        MsgBox "Missing Bleed Off Resistors On VXI J1 Connector.", vbCritical
        '        SupplyStatus = FAILED
        '        TestPowerSupplies = FAILED
        '        GoTo TestComplete
        '    End If

        '    For iTerm = 1 To 9
        '        nSystErr = WriteMsg SWITCH1, "OPEN " & DC(iTerm)
        '    Next

        '    If dMeasurement < 1040 Or dMeasurement > 1166 Then
        '
        '   Checking Power supply 9's bleed resistor
        '
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", "9000", "11000", "Ohms", PPU, "P09-02-D02")
        '        DCRes(9) = dMeasurement
        '        If dMeasurement < 9850 Or dMeasurement > 10150 Then
        '            Echo "  Power Supply DC 9 . . . FAILED bleed off resistor test."
        '        End If
        '
        '   Checking Power supply 8's bleed resistor
        '
        '        TempResH = (1 / (1 / DCRes(9) + 1 / 10000)) * 1.015
        '        TempResL = (1 / (1 / DCRes(9) + 1 / 10000)) * 0.985
        '        nSystErr = WriteMsg SWITCH1, "CLOSE " & DC(8)
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", Format(TempResL), Format(TempResH), "Ohms", PPU, "P09-02-D02")
        '        DCRes(8) = 1 / ((1 / dMeasurement) - (1 / DCRes(9)))
        '        If DCRes(8) < 9850 Or DCRes(8) > 10150 Then
        '            Echo "  Power Supply DC 8 . . . FAILED bleed off resistor test."
        '        End If
        '
        '   Checking Power supply 7's bleed resistor
        '
        '        TempResH = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / 10000)) * 1.015
        '        TempResL = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / 10000)) * 0.985
        '        nSystErr = WriteMsg SWITCH1, "CLOSE " & DC(7)
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", Format(TempResL), Format(TempResH), "Ohms", PPU, "P09-02-D02")
        '        DCRes(7) = 1 / ((1 / dMeasurement) - (1 / DCRes(9) + 1 / DCRes(8)))
        '        If DCRes(7) < 9850 Or DCRes(7) > 10150 Then
        '            Echo "  Power Supply DC 7 . . . FAILED bleed off resistor test."
        '        End If
        '
        '   Checking Power supply 6's bleed resistor
        '
        '        TempResH = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / 10000)) * 1.015
        '        TempResL = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / 10000)) * 0.985
        '        nSystErr = WriteMsg SWITCH1, "CLOSE " & DC(6)
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", Format(TempResL), Format(TempResH), "Ohms", PPU, "P09-02-D02")
        '        DCRes(6) = 1 / ((1 / dMeasurement) - (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7)))
        '        If DCRes(6) < 9850 Or DCRes(6) > 10150 Then
        '            Echo "  Power Supply DC 6 . . . FAILED bleed off resistor test."
        '        End If
        '
        '   Checking Power supply 5's bleed resistor
        '
        '        TempResH = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / 10000)) * 1.015
        '        TempResL = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / 10000)) * 0.985
        '        nSystErr = WriteMsg SWITCH1, "CLOSE " & DC(5)
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", Format(TempResL), Format(TempResH), "Ohms", PPU, "P09-02-D02")
        '        DCRes(5) = 1 / ((1 / dMeasurement) - (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6)))
        '        If DCRes(5) < 9850 Or DCRes(5) > 10150 Then
        '            Echo "  Power Supply DC 5 . . . FAILED bleed off resistor test."
        '        End If
        '
        '   Checking Power supply 4's bleed resistor
        '
        '        TempResH = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / 10000)) * 1.015
        '        TempResL = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / 10000)) * 0.985
        '        nSystErr = WriteMsg SWITCH1, "CLOSE " & DC(4)
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", Format(TempResL), Format(TempResH), "Ohms", PPU, "P09-02-D02")
        '        DCRes(4) = 1 / ((1 / dMeasurement) - (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5)))
        '        If DCRes(4) < 9850 Or DCRes(4) > 10150 Then
        '            Echo "  Power Supply DC 4 . . . FAILED bleed off resistor test."
        '        End If
        '
        '   Checking Power supply 3's bleed resistor
        '
        '        TempResH = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4) + 1 / 10000)) * 1.015
        '        TempResL = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4) + 1 / 10000)) * 0.985
        '        nSystErr = WriteMsg SWITCH1, "CLOSE " & DC(3)
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", Format(TempResL), Format(TempResH), "Ohms", PPU, "P09-02-D02")
        '        DCRes(3) = 1 / ((1 / dMeasurement) - (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4)))
        '        If DCRes(3) < 9850 Or DCRes(3) > 10150 Then
        '            Echo "  Power Supply DC 3 . . . FAILED bleed off resistor test."
        '        End If
        '
        '   Checking Power supply 2's bleed resistor
        '
        '        TempResH = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4) + 1 / DCRes(3) + 1 / 10000)) * 1.015
        '        TempResL = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4) + 1 / DCRes(3) + 1 / 10000)) * 0.985
        '        nSystErr = WriteMsg SWITCH1, "CLOSE " & DC(2)
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", Format(TempResL), Format(TempResH), "Ohms", PPU, "P09-02-D02")
        '        DCRes(2) = 1 / ((1 / dMeasurement) - (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4) + 1 / DCRes(3)))
        '        If DCRes(2) < 9850 Or DCRes(2) > 10150 Then
        '            Echo "  Power Supply DC 2 . . . FAILED bleed off resistor test."
        '        End If
        '
        '   Checking Power supply 1's bleed resistor
        '
        '        TempResH = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4) + 1 / DCRes(3) + 1 / DCRes(2) + 1 / 10000)) * 1.015
        '        TempResL = (1 / (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4) + 1 / DCRes(3) + 1 / DCRes(2) + 1 / 10000)) * 0.985
        '        nSystErr = WriteMsg SWITCH1, "CLOSE " & DC(1)
        '        setupResistanceMeasurement
        '        WaitForResponse DMM, 0
        '        dMeasurement = FetchMeasurement(DMM, "Bleed Resistors present.", Format(TempResL), Format(TempResH), "Ohms", PPU, "P09-02-D02")
        '        DCRes(1) = 1 / ((1 / dMeasurement) - (1 / DCRes(9) + 1 / DCRes(8) + 1 / DCRes(7) + 1 / DCRes(6) + 1 / DCRes(5) + 1 / DCRes(4) + 1 / DCRes(3) + 1 / DCRes(2)))
        '        If DCRes(1) < 9850 Or DCRes(1) > 10150 Then
        '            Echo "  Power Supply DC 1 . . . FAILED bleed off resistor test."
        '        End If
        '        For iTerm = 1 To 8
        '            nSystErr = WriteMsg SWITCH1, "OPEN " & DC(iTerm)
        '        Next
        '        SupplyStatus = FAILED
        '        TestPowerSupplies = FAILED
        '    End If

    End Sub
End Module
