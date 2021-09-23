Option Strict Off
Option Explicit On

Imports System
Imports System.Windows.Forms
'Imports System.Text
'Imports System.Math

Public Module modDcpsViperTSaif


    '**************************************************************
    '* Nomenclature   : TETS SYSTEM SELF TEST                     *
    '*                  DC Power Supply Test                      *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  DC Power Supply (PPU) Self Test           *
    '**************************************************************

    Dim PolarityStatus(10) As Boolean

    Dim Wcentury As Integer
    Dim Wyear As Integer
    Dim Wmonth As Integer
    Dim Wday As Integer

    Dim Rcentury As Integer
    Dim Ryear As Integer
    Dim Rmonth As Integer
    Dim Rday As Integer

    Public DumpSupplyVT As Single
    Public DumpAddrVT As Integer

    '************************************************************
    '* Nomenclature   : UUT Power Supplies                      *
    '*    DESCRIPTION:                                          *
    '*     Send a command string to the power supplies          *
    '*    EXAMPLE:                                              *
    '*      SendDCPSCommand (3, Command$)                       *
    '*    PARAMTERS:                                            *
    '*     slot%     = The slot number of the supply where to   *
    '*                 send the command                         *
    '*     Command$  = The command string to send to the supply *
    '*                                                          *
    '*     NOTE: All commands must be three bytes in length     *
    '*           even if one of the bytes is a null byte.       *
    '*           All programmed voltages for the 40V supplies   *
    '*           need to be multiplied by 100 (65V by 50) and   *
    '*           converted to 12 bits.                          *
    '*           All programmed currents need to be multiplied  *
    '*           by 500 and converted to 12 bits.               *
    '*                                                          *
    '*                                                          *
    '*    Commands     Description                              *
    '*    1S, 00, 00   Reset Supply S(1-A)                      *
    '*    2S, 4Z, ZZ   Program Supply S to ZZZ current limit    *
    '*    2S, 5Z, ZZ   Program Supply S to ZZZ voltage limit    *
    '*    4S, 00, 00   Execute BIT on supply S                  *
    '*    2S, B0, 00   Close Output Relay on supply S           *
    '*    2S, A0, 00   Open Output Relay on supply S (default)  *
    '*    2S, 80, 03   Reverse Polarity on supply S             *
    '*    2S, 80, 02   Normal Polarity on supply S (default)    *
    '*    2S, 8C, 00   Slave Mode on supply S                   *
    '*    2S, 88, 00   Master Mode on supply S (default)        *
    '*    2S, 83, 00   Remote Sense on supply S                 *
    '*    2S, 82, 00   Local Sense on supply S (default)        *
    '*    2S, 80, 30   Constant Current Mode on supply S        *
    '*    2S, 80, 20   Constant Voltage Mode on supply S (def)  *
    '*                                                          *
    '*    AS, 4Z, ZZ   Cal Start Location ZZZ for supply S      *
    '*    CS, 4Z, ZZ   Cal Next Location ZZZ for supply S       *
    '*    ES, 4x, ZZ   Cal Offset ZZ to write between Start and *
    '*                 Next Locations                           *
    '*    2S, 6Z, ZZ   Voltage Set command which bypasses Cal   *
    '*                 Offsets - use for every voltage setting  *
    '*                 during calibration to ignore past cal.   *
    '*                                                          *
    '*    9S, xx, xx   Cal finished for supply S                *
    '*    2S, 80, 40   Place supply S into NoFault Mode         *
    '*    8S, 4F, AA   Calibration Store Century Location       *
    '*    8S, 6x, ZZ   Calibration Store Century ZZ             *
    '*    8S, 4F, AB   Calibration Store Year Location          *
    '*    8S, 6x, ZZ   Calibration Store Year ZZ                *
    '*    8S, 4F, AC   Calibration Store Month Location         *
    '*    8S, 4F, ZZ   Calibration Store Month ZZ               *
    '*    8S, 4F, AD   Calibration Store Day Location           *
    '*    8S, 4F, ZZ   Calibration Store Day ZZ                 *
    '*    0S, 6F, AA   Read Cal Century - returns in byte 5     *
    '*    0S, 6F, AA   Read Cal Year    - returns in byte 5     *
    '*    0S, 6F, AA   Read Cal Month   - returns in byte 5     *
    '*    0S, 6F, AA   Read Cal Day     - returns in byte 5     *
    '*                                                          *
    '*    0S, 44, 00   Status Query to supply S                 *
    '*    0S, 42, 00   Measurement Query to supply S            *
    '*                                                          *
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
    '*   Measurement Query                                      *
    '*   Byte 1 5C     12 bit current reading CCC               *
    '*        2 CC       (divide by 500 to get Amps)            *
    '*        3 2V     12 bit voltage reading VVV               *
    '*          AV       (when polarity is reversed)            *
    '*        4 VV       (divide by 100 to get Volts)           *
    '*        5 SB     Status Byte (same as above byte 2)       *
    '*                                                          *
    '*                                                          *
    '*                                                          *
    '************************************************************

    Public Function TestPowerSuppliesVT() As Integer
        '    'DESCRIPTION:
        'This routine tests the power supplies
        'Returns:
        'PASSED if the supplies are passing and FAILED if they fail
        'Module Defaults on power up/Reset:
        '  0V, 75mA, CL mode, Independent, Local sense, Disabled

        '*****************************************************************************************************
        '                   PPS Self Test Procedure
        '*****************************************************************************************************
        'DC1 to DC10
        ' 1. Self Test Supply
        ' 2. Close all interconnect relays and connect DC9 to DMM
        '    Set Supply to 5v but don't close the output relay
        '    Use DMM to measure less than 1v
        ' 3. Set Supply to 40(65)v with no load.
        '    Use DMM to measure and verify voltage
        ' 4. Connect 13 ohm load to DC1
        '    Set Supply to 13 vdc to draw 1 amp on 13 ohm load using local sense
        '    Use DMM to measure and Verify voltage
        ' 5. Change power supply to remote sense
        '    Use DMM to measure and Verify voltage rises
        ' 6. Use PS to measure and Verify voltage
        ' 7. Use PS to measure and Verify current
        ' 8. Use DMM to measure ripple (rms) voltage
        ' 9. Change power supply to negative 13v (with load about 1 amp)
        '    Use DMM to measure and Verify voltage
        '10. Change power supply to 0.5 amps, 13volts (should trip supply)
        '    Use DMM to verify less than 2 volts
        '11. Reset supply
        '    Verify Status byte = &h80 (no errors)
        '
        '12. Change power supply to constant current, 2 amp, 40 volts max (should only be about 18v)
        '    Use PS to measure and verify current
        '
        'DC2 to DC9 only
        '13. Change first supply to master 20v, 5 amps (8 ohm load draws 2.5 amps)
        '    Change second supply to slave mode
        '    Verify second supply current is between 1.0 and 1.5 amps (1.25 amps)
        '******************************************************************************************************

        Dim DC(9) As String
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
        Dim d1Measurement As Double
        Dim d2Measurement As Double
        Dim x As DialogResult
        Dim S As String = ""
        Dim temp As String = ""
        Dim i As Integer
        Dim VoltsHi As Single
        Dim LoopStepNo As Integer
        Dim StrMeasurement As String = ""
        Dim TestPPU As Integer


        HelpContextID = 1230
        'Programmable Power Unit Test Title Block
        EchoTitle(InstrumentDescription(PPU) & " ", True)
        EchoTitle("Programmable Power Unit Test", False)
        frmSTest.proProgress.Maximum = 130
        frmSTest.proProgress.Value = 0

        TestPowerSuppliesVT = UNTESTED
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        If FirstPass = True Then
            If RunningEndToEnd = False Then
                x = MsgBox("Is the W201 cable installed?", MsgBoxStyle.YesNo)
                If x <> DialogResult.Yes Then
                    S = "1. Remove all cables/adapters from the SAIF." & vbCrLf
                    S &= "2. Connect W201-P1 cable (15011A6045) to SAIF J1 connector." & vbCrLf
                    S &= "3. Note: W201-P2 to P23 cables maybe left on or disconnected." & vbCrLf
                    DisplaySetup(S, "ST-W201-1.jpg", 3)
                    If AbortTest = True Then GoTo TestComplete
                End If
            ElseIf (ControlStartTest = PPU) Then

Step1:          'Install the W201 cable
                S = "Remove all cables/adapters from the SAIF." & vbCrLf
                S &= "Connect W201 cable (15011A6045) to SAIF as follows:" & vbCrLf
                S &= "1.  P1  to SAIF J1 connector." & vbCrLf
                S &= "2.  P4  to SAIF FG OUTPUT." & vbCrLf
                S &= "3.  P5  to SAIF FG TRIG/PLL IN." & vbCrLf
                S &= "4.  P6  to SAIF FG SYNC OUT." & vbCrLf
                S &= "5.  P7  to SAIF FG CLOCK IN." & vbCrLf
                S &= "6.  P8  to SAIF FG PM IN." & vbCrLf
                S &= "7.  P9  to SAIF ARB OUTPUT/7." & vbCrLf
                S &= "8.  P10 to SAIF ARB OUTPUT." & vbCrLf
                S &= "9.  P11 to SAIF ARB START ARM IN." & vbCrLf
                S &= "10. P12 to SAIF ARB MARKER OUT." & vbCrLf
                S &= "11. P13 to SAIF ARB REF/SMPL IN." & vbCrLf
                S &= "12. P14 to SAIF ARB STOP TRIG/FSK/GATE IN." & vbCrLf
                S &= "13. P15 to SAIF SCOPE INPUT 1." & vbCrLf
                S &= "14. P16 to SAIF SCOPE INPUT 2." & vbCrLf
                S &= "15. P17 to SAIF SCOPE TRIG IN." & vbCrLf
                S &= "16. P18 to SAIF SCOPE COMP CAL." & vbCrLf
                S &= "17. P19 to SAIF C/T INPUT 1." & vbCrLf
                S &= "18. P20 to SAIF C/T INPUT 2." & vbCrLf
                S &= "19. P21 to SAIF C/T ARM IN." & vbCrLf
                S &= "20. P22 to SAIF DMM TRIG IN." & vbCrLf
                S &= "21. P23 to SAIF DMM VCOMP OUT."
                DisplaySetup(S, "ST-W201-1.jpg", 21, True, 1, 3)
                If AbortTest = True Then GoTo TestComplete

Step2:          ' install W204 S/R cable
                S = "Install the W204 cable (93006H6048) as follows:" & vbCrLf
                S &= "1. Connect W204-P1 cable onto the secondary chassis S/R I/O connector. " & vbCrLf
                S &= "2. Connect W204-P2 cable onto the SAIF J5 connector. " & vbCrLf
                DisplaySetup(S, "ST-W204-1.jpg", 2, True, 2, 3)
                If GoBack = True Then GoTo Step1
                If AbortTest = True Then GoTo TestComplete

Step3:          'Install the SP2,SP3,SP4 adapters and the W206, W209 cables
                S = "Install SP2, SP3, SP4, W206 and W209 as follows:" & vbCrLf
                S &= "1. Connect SP2 adapter (15011A6053) to the CIC J10 (Serial) connector." & vbCrLf
                S &= "2. Connect SP3 adapter (93006H6054) to the CIC J5 (RS232) connector." & vbCrLf
                S &= "3. Connect SP4 adapter (93006H6055) to the CIC J6 (RS422) connector." & vbCrLf
                S &= "4. Connect W206 cable (93006H6861) from CIC J15 (GIGABIT) to CIC J16 (LAN)." & vbCrLf
                S &= "5. Connect W209 cable (15011A6055) from CIC J21 (CAN1) to CIC J22 (CAN2)." & vbCrLf
                DisplaySetup(S, "ST-SPALL-1.jpg", 5, True, 3, 3)
                If GoBack = True Then GoTo Step2
                If AbortTest = True Then GoTo TestComplete
            End If
        End If

        TestPowerSuppliesVT = PASSED

        'Define command options.
        CloseRelay = 0 'false
        SetSlave = 0
        SenseRemote = 0
        CurrentConstant = 0

        OpenRelay = -1 'true
        SetMaster = -1
        SenseLocal = -1
        CurrentLimit = -1

        DC(1) = "1.0002" ' switch S101-9,10(11,12)           connects DC1 to DC2
        DC(2) = "1.0003" ' switch S101-13,14(15,16)          connects DC2 to DC3
        DC(3) = "1.0004" ' switch S101-17,18(19,20)          connects DC3 to DC4
        DC(4) = "1.1038-1047" ' switch S301-(77,78)-(96,96)       connects DC4 to DC5
        DC(5) = "1.1028-1037" ' switch S301-(57,58)-(75,76)       connects DC5 to DC6
        DC(6) = "2.1038-1047" ' switch S301-(173,174)-(191,192)   connects DC6 to DC7
        DC(7) = "2.1028-1037" ' switch S301-(153,154)-(171,172)   connects DC7 to DC8
        DC(8) = "2.0002" ' switch S101-29,30(31,32)          connects DC8 to DC9
        DC(9) = "2.0003" ' switch S101-33,34(35,36)          connects DC9 to DC10


        '************************************Self-Test procedure******************************************
        'Close all SAIF DC Supply interconnect Switches except DC9 to DC10
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.0003") 'disconnect DC9 from DC10
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0002-0004,1028-1047") 'interconnect DC1 through DC6
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0002,1028-1047") 'interconnect DC6 through DC9
        LoopStepNo = 1
        frmSTest.proProgress.Value = 1
        For Supply = 1 To 10

            ' shift and control and left click on ppu allows operator to select one PPU to test
            If StartPPUTest = True Then
                frmSTest.FrPPUStart.Visible = True
                Do
                    Application.DoEvents()
                Loop Until frmSTest.FrPPUStart.Visible = False
                Supply = CInt(Val(frmSTest.txtPPUstart.Text))
                If Supply < 1 Or Supply > 10 Then
                    Supply = 1
                End If
            End If

            Echo(vbCrLf & "*** TEST DC" & CStr(Supply) & " ***")
            TestPPU = PASSED

            nSystErr = WriteMsg(SWITCH1, "OPEN 1.0000") ' disconnect Loads from DMM
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.0001") ' disconnect Loads from DC1
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.0000") ' disconnect DC9 from DMM
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.0001") ' disconnect DC10 to DMM
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.0004") ' set load to 13 ohms

            If Supply = 10 Then
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0001") ' connect DC10 to DMM
            Else
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0000") ' connect DC9 to DMM
            End If

            SupplyStatus = PASSED
            '      'skip all supplies that are not initialized
            '      Do Until SupplyHandle&(Supply) <> 0
            '          ReportFailure PPU
            '          If Supply% = 10 Then Exit For
            '          Supply% = Supply% + 1
            '      Loop
            CommandSetOptionsVT(Supply, OpenRelay, -1, -1, -1)
            CommandSetOptionsVT(Supply, 1, 1, 1, CurrentLimit)

PPU_1:      'P01-01-001 to P10-01-001 BIT test
            If SelfTestPPS(Supply) = FAILED Then 'Function call to reset and self test each supply 1-10
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_1
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU


PPU_2:      'P01-02-001 to P10-02-001 Open Relay Test
            sTNum = "P" & Format(Supply, "00") & "-02-001"
            nSystErr = WriteMsg(DMM, "*RST;*CLS")
            If Supply < 10 Then
                VoltsHi = 40
            Else
                VoltsHi = 65
            End If

            'Set DC Supply (1 to 10) to 5 volts but don't close the power supply relay
            CommandSetOptions(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
            CommandSetPolarityVT(Supply, "+")

            CommandSetCurrentVT(Supply, 0.5) 'set current limit to .5 Amp
            CommandSetVoltageVT(Supply, VoltsHi)
            Delay(1)

            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
            Delay(0.1)

            dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Open Relay Test", "", "1", "V", PPU, sTNum)
            If dMeasurement > 1 Then
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_2
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_3:      'P01-03-001 to P10-03-001 No Load Voltage Test
            sTNum = "P" & Format(Supply, "00") & "-03-001"
            nSystErr = WriteMsg(DMM, "*RST;*CLS")

            CommandSetOptionsVT(Supply, CloseRelay, 1, 1, 1) 'Close Relay with no load
            Delay(0.5)

            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
            Delay(0.1)

            d1Measurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " No Load Voltage Test", CStr(VoltsHi - 1), CStr(VoltsHi + 1), "V", PPU, sTNum)
            If (d1Measurement < VoltsHi - 1) Or (d1Measurement > VoltsHi + 1) Then
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_3
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU


PPU_4:      'P01-04-001 to P10-04-001 'Local Sense Voltage Test under Load
            sTNum = "P" & Format(Supply, "00") & "-04-001"

            CommandSetCurrentVT(Supply, 3) 'set current limit to 3.0 Amp

            nSystErr = WriteMsg(DMM, "*RST;*CLS")
            CommandSetVoltage(Supply, 13) ' drop voltage from Vmax to 13V
            Delay(1)

            'Connect UTILITY LOADS (HI/LO) to DC POWER SUPPLIES DC9 or DC10.
            nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0000") 'Connect 13ohm LOADS to DMM
            If Supply < 10 Then
                nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0001") 'Connect 13ohm LOADS to DC1
            End If
            Delay(1)

            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
            Delay(0.1)

            d1Measurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Local Sense Voltage Test", "11", "13", "V", PPU, sTNum)
            If d1Measurement < 11 Or d1Measurement > 13 Then
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_4
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_5:      'P01-05-001 to P10-05-001 Remote sense Voltage Test
            sTNum = "P" & Format(Supply, "00") & "-05-001"
            nSystErr = WriteMsg(DMM, "*RST;*CLS")

            'DC Supply (1 to 10) set to 13 volts to draw about 1 amp on 13 ohm load
            CommandSetOptionsVT(Supply, 1, 1, SenseRemote, 1)
            Delay(1)

            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
            Delay(0.1)

            d2Measurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Remote Sense Voltage Test", "11", "13", "V", PPU, sTNum)
            If d2Measurement < 11 Or d2Measurement > 13 Then
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If

            If d2Measurement <= d1Measurement Then
                Echo("    *********************************")
                Echo("    Voltage did not rise as expected.")
                Echo("    Check sense lines for DC" & CStr(Supply) & ".")
                Echo("    *********************************" & vbCrLf)
            End If

            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_5
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_6:      'P01-06-001 to P10-06-001' Measure internal voltage
            sTNum = "P" & Format(Supply, "00") & "-06-001"
            dMeasurement = CommandMeasureVolts(Supply) ' measure volts
            temp = "Min: 12.0 V         Max: 14.0 V         Measured: " & Format(dMeasurement, "0.00") & " V"
            Echo(sTNum & " PPU Supply DC" & CStr(Supply) & " Int Voltage Test")
            If dMeasurement < 12 Or dMeasurement > 14 Then
                Echo(FormatResults(False, temp))
                RegisterFailure(CStr(PPU), sTNum, dMeasurement, "V", 12, 14, "PPU Supply DC" & CStr(Supply) & " Int Voltage Test")
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, temp))
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_6
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_7:      'P01-07-001 to P10-07-001  Measure internal current
            sTNum = "P" & Format(Supply, "00") & "-07-001"
            dMeasurement = CommandMeasureAmps(Supply) ' measure amps
            Echo(sTNum & " PPU Supply DC" & CStr(Supply) & " Int Current Test")
            temp = "Min: 0.5 A          Max: 1.5 A          Measured: " & Format(dMeasurement, "0.00") & " A"
            If dMeasurement < 0.5 Or dMeasurement > 1.5 Then
                Echo(FormatResults(False, temp))
                RegisterFailure(CStr(PPU), sTNum, dMeasurement, "A", 0.5, 1.5, "PPU Supply DC" & CStr(Supply) & " Int Current Test")
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, temp))
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_7
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_8:      'P01-08-001 to P10-08-001  Voltage Ripple Test max 80mVac
            sTNum = "P" & Format(Supply, "00") & "-08-001"
            nSystErr = WriteMsg(DMM, "MEAS:VOLT:AC?")
            nSystErr = ReadMsg(DMM, S)
            Delay(0.1)
            nSystErr = WriteMsg(DMM, "MEAS:VOLT:AC?")

            dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Voltage Ripple Test", "", "0.08", "VAC", PPU, sTNum)
            If dMeasurement > 0.08 Then
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_8
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_9:      'P01-09-001 to P10-09-001  Negative Voltage Test under load
            sTNum = "P" & Format(Supply, "00") & "-09-001"
            nSystErr = WriteMsg(DMM, "*RST;*CLS")

            ' turn off supply
            CommandSetOptionsVT(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit) ' open output relay
            CommandSupplyResetVT(Supply) ' Reset will clear supply which may have tripped
            Delay(0.5)

            CommandSetPolarityVT(Supply, "-")

            CommandSetCurrentVT(Supply, 3) 'set current limit to 3 Amp
            CommandSetVoltageVT(Supply, 13) 'set voltage to -13V
            CommandSetOptionsVT(Supply, CloseRelay, SetMaster, SenseLocal, CurrentLimit)
            Delay(2)

            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
            Delay(0.1)

            dMeasurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Negative Voltage Test", "-13", "-11", "V", PPU, sTNum)
            If dMeasurement < -13 Or dMeasurement > -11 Then
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_9
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_10:     'P01-10-001 to P10-10-001  Overload Trip Test
            sTNum = "P" & Format(Supply, "00") & "-010-001"
            nSystErr = WriteMsg(DMM, "*RST;*CLS")

            ' turn off -13V supply
            CommandSetOptionsVT(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit) ' open output relay
            CommandSupplyResetVT(Supply) ' Reset will clear supply which may have tripped
            CommandSetPolarityVT(Supply, "+")
            CommandSetVoltageVT(Supply, 0) 'discharge power supply first
            Delay(1)

            'set voltage to 13 volts and current to .5 amps
            'verify supply trips
            'reset supply, verify that it clears error
            'Set DC Supply (1 to 10) to 13 volts at 0.5 amps with a 1 amp load

            CommandSetOptionsVT(Supply, CloseRelay, SetMaster, SenseLocal, CurrentLimit)
            CommandSetCurrentVT(Supply, 0.5) 'set current limit to .5 Amp
            CommandSetVoltageVT(Supply, 13) 'set voltage to 13
            '      Delay 3
            '      nSystErr = WriteMsg DMM, "MEAS:VOLT?"
            '      Delay 0.1



            For i = 1 To 30
                nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
                nSystErr = ReadMsg(DMM, StrMeasurement)
                If Val(StrMeasurement) < 1 Then Exit For
            Next i

            nSystErr = WriteMsg(DMM, "MEAS:VOLT?")
            Delay(0.1)
            d1Measurement = FetchMeasurement(DMM, "PPU Supply DC" & Supply & " Excess Current Shut Down Test", "0", "2", "V", PPU, sTNum)
            If d1Measurement > 2 Then
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            d2Measurement = CommandMeasureAmps(Supply) ' measure amps
            S = "    Note: DC" & CStr(Supply) & " Measured " & Format(d2Measurement, "0.00") & " Amps"
            If i < 31 Then
                S &= ", and tripped at " & Format(i / 2, "0.0") & " Seconds."
            Else
                S &= " and failed to trip!"
            End If
            Echo(S)
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_10
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_11:     'P01-11-001 to P10-11-001  Reset Test
            sTNum = "P" & Format(Supply, "00") & "-011-001"

            ' turn off supply
            CommandSetOptionsVT(Supply, OpenRelay, SetMaster, SenseLocal, CurrentLimit) ' open output relay
            CommandSupplyResetVT(Supply) ' Reset will clear supply which should have tripped
            Delay(1)

            '*    0S, 44, 00   Status Query to supply S
            CommandSupplyResetVT(Supply) ' ?? Chr(Supply) & Chr(&H44) & Chr(&H0))
            Delay(0.1)

            'get status query returned data
            ReadBuffer = ""
            ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer) ' get 5 bytes
            Echo(sTNum & " PPU DC" & CStr(Supply) & " Reset Test")

            temp = "PPU Reset Supply DC" & CStr(Supply) & " Test"
            If Asc(Strings.Mid(ReadBuffer, 2, 1)) <> &H80 Then 'Status Byte 0x80=no errors
                Echo(FormatResults(False, temp))
                RegisterFailure(CStr(PPU), sTNum, , , , , "PPU Supply DC" & CStr(Supply) & " Reset Test")
                Echo("Expected &H80, Measured &H" & Hex(Asc(Strings.Mid(ReadBuffer, 2, 1))))
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, temp))
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_11
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU

PPU_12:     'P01-12-001 to P10-12-001  Constant Current Test
            sTNum = "P" & Format(Supply, "00") & "-012-001"

            'Change load to 8 ohms
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0004") 'Close S101-37,38(39,40) to apply 8ohm load

            'Turn on supply to 2 amp constant current mode (Note voltage drops to about 18vdc)
            CommandSetVoltage(Supply, 40) 'set voltage to 40vdc
            CommandSetCurrent(Supply, 2) 'set current limit to 2 Amp
            CommandSetOptions(Supply, 1, 1, 1, CurrentConstant) 'select constant current
            CommandSetOptions(Supply, CloseRelay, 1, 1, 1) 'close output relay
            Delay(2)

            d1Measurement = CommandMeasureAmps(Supply) ' measure amps via supply
            d2Measurement = CommandMeasureVolts(Supply) ' measure volts via supply
            Echo(sTNum & " PPU Supply DC" & CStr(Supply) & " Constant Current Test")
            temp = "Min: 1.5 A          Max: 2.5 A          Measured: " & Format(d1Measurement, "0.00") & " A"
            If d1Measurement < 1.5 Or d1Measurement > 2.5 Then
                Echo(FormatResults(False, temp))
                RegisterFailure(CStr(PPU), sTNum, d1Measurement, "A", 1.5, 2.5, "PPU Supply DC" & CStr(Supply) & " Constant Current Test")
                TestPowerSuppliesVT = FAILED
                TestPPU = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then
                    Echo("    Note: Measured " & Format(d2Measurement, "0.00") & " V")
                    GoTo TestComplete
                End If
            Else
                Echo(FormatResults(True, temp))
                IncStepPassed()
            End If
            Echo("    Note: Measured " & Format(d2Measurement, "0.00") & " V")
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                GoTo PPU_12
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            If OptionFaultMode = COFmode And TestPPU = FAILED Then GoTo NextPPU


PPU_13:     'P01-13-001 to P10-13-001  Slave Mode Test (DC2 to DC9 only)
            sTNum = "P" & Format(Supply, "00") & "-013-001"

            If Supply > 1 And Supply < 10 Then ' just do dc2 to dc9
                'Enable 1 master, 2 slave
                'Turn on master supply to 35v, 5 amp max
                ' Note: Load at 8 ohms (+ path impedance which averages about 1 ohm?)
                'verify slave supply is pulling current

                ' reset supply
                CommandSupplyResetVT(Supply - 1) ' Reset master (DC1 to DC8)
                CommandSupplyResetVT(Supply) ' Reset slave  (DC2 to DC9)
                Delay(2)

                'setup slave
                CommandSetOptions(Supply, 1, SetSlave, 1, 1) ' set slave mode
                Delay(0.5)
                CommandSetOptions(Supply, CloseRelay, 1, 1, 1) ' close output relay
                Delay(0.5)

                'Turn on master
                CommandSetOptions(Supply - 1, 1, SetMaster, 1, 1) ' set master mode
                Delay(0.5)
                CommandSetCurrent(Supply - 1, 5) ' set current limit to 5 Amp
                Delay(0.5)
                CommandSetVoltage(Supply - 1, 35) ' set voltage to 35 (4 amp load)
                Delay(0.5)
                CommandSetOptions(Supply - 1, CloseRelay, 1, 1, 1) ' close output relay
                Delay(5) ' allow plenty of time for PPU to switch to master/slave mode

                d1Measurement = CommandMeasureAmps(Supply - 1) ' measure amps master (2.00)
                d2Measurement = CommandMeasureAmps(Supply) ' measure amps slave  (2.00)

                temp = "Min: 1.5 A       Max: 2.5 A         Measured: " & Format(d2Measurement, "0.00") & " A"
                Echo(sTNum & " PPU Supply DC" & CStr(Supply) & " Slave Mode Test")
                If d2Measurement < 1.5 Or d2Measurement > 2.5 Then
                    Echo(FormatResults(False, temp))
                    Echo("    Also check Master Supply DC" & CStr(Supply - 1))
                    RegisterFailure(CStr(PPU), sTNum, d2Measurement, "A", 1.5, 2.5, "PPU Supply DC" & CStr(Supply) & " Slave Mode Test")
                    TestPowerSuppliesVT = FAILED
                    TestPPU = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then
                        Echo("    Note: Master Measured " & Format(d1Measurement, "0.00") & " A")
                        GoTo TestComplete
                    End If
                Else
                    Echo(FormatResults(True, temp))
                    IncStepPassed()
                End If
                Echo("    Note: Master Measured " & Format(d1Measurement, "0.00") & " A")

                'disconnect two supplies
                CommandSetVoltage((Supply - 1), 0) 'set voltage to 0
                CommandSetCurrent((Supply - 1), 0) 'set current limit to 0 amp
                CommandSetOptions(Supply - 1, OpenRelay, 1, 1, 1)
                CommandSetOptions(Supply, OpenRelay, SetMaster, 1, 1)
                Delay(1)
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = "PPU" And OptionStep = LoopStepNo Then
                    GoTo PPU_13
                End If
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

            End If

NextPPU:
            ' Reset will clear supply which may have tripped
            CommandSetOptions(Supply - 1, 1, 1, 1, CurrentLimit) ' force constant voltage mode
            CommandSupplyResetVT(Supply)
            If Supply > 1 And Supply < 10 Then ' also reset slave supply
                CommandSupplyResetVT(Supply - 1)
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If StartPPUTest = True Then
                StartPPUTest = False
                GoTo TestComplete
            End If
        Next Supply

TestComplete:
        frmSTest.proProgress.Value = 100
        frmSTest.proProgress.Maximum = 100

        'reset active supply which may have tripped when complete
        CommandSetOptions(Supply - 1, 1, 1, 1, CurrentLimit) ' force constant voltage mode
        CommandSupplyResetVT(Supply)
        If Supply > 1 And Supply < 10 Then ' also reset slave supply
            CommandSupplyResetVT(Supply - 1)
        End If
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches

        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        Application.DoEvents()

        If AbortTest = True Then
            If TestPowerSuppliesVT = FAILED Then
                ReportFailure(PPU)
            Else
                ReportUnknown(PPU)
            End If
        ElseIf TestPowerSuppliesVT = PASSED Then
            ReportPass(PPU)
        ElseIf TestPowerSuppliesVT = FAILED Then
            ReportFailure(PPU)
        Else
            ReportUnknown(PPU)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If


    End Function


    Function CommandCalDateReadVT(ByVal Supply As Integer) As Integer

        '************************************************************
        '*    DESCRIPTION:                                          *
        '************************************************************
        Dim ReadBuffer As String = ""
        Dim ErrorStatus As Integer

        CommandCalDateReadVT = -1

        'century
        SendDCPSCommandVT(Supply, Chr(Supply) & Chr(&H6F) & Chr(&HAA))
        SendDCPSCommandVT(Supply, Chr(Supply) & Chr(&H44) & Chr(0))
        ErrorStatus = ReadDCPSCommandVT(Supply, ReadBuffer)
        Rcentury = Asc(Strings.Mid(ReadBuffer, 5, 1))

        'year
        SendDCPSCommandVT(Supply, Chr(Supply) & Chr(&H6F) & Chr(&HAB))
        SendDCPSCommandVT(Supply, Chr(Supply) & Chr(&H44) & Chr(0))
        ErrorStatus = ReadDCPSCommandVT(Supply, ReadBuffer)
        Ryear = Asc(Strings.Mid(ReadBuffer, 5, 1))

        'month
        SendDCPSCommandVT(Supply, Chr(Supply) & Chr(&H6F) & Chr(&HAC))
        SendDCPSCommandVT(Supply, Chr(Supply) & Chr(&H44) & Chr(0))
        ErrorStatus = ReadDCPSCommandVT(Supply, ReadBuffer)
        Rmonth = Asc(Strings.Mid(ReadBuffer, 5, 1))

        'day
        SendDCPSCommandVT(Supply, Chr(Supply) & Chr(&H6F) & Chr(&HAD))
        SendDCPSCommandVT(Supply, Chr(Supply) & Chr(&H44) & Chr(0))
        ErrorStatus = ReadDCPSCommandVT(Supply, ReadBuffer)
        Rday = Asc(Strings.Mid(ReadBuffer, 5, 1))

    End Function

    '#Const defUse_CommandCalDateWrite = True
#If defUse_CommandCalDateWrite Then
    Function CommandCalDateWrite(ByVal Supply As integer) As Single
        '************************************************************
        '*    DESCRIPTION:                                          *
        '************************************************************
        Dim B1 As integer
        Dim B2 As integer
        Dim B3 As integer

        'century
        SendDCPSCommandVT(Supply, Chr(&H80+Supply) & Chr(&H4F) & Chr(&HAA))
        SendDCPSCommandVT(Supply, Chr(&H80+Supply) & Chr(&H60+i) & Chr(Wcentury))

        'year
        SendDCPSCommandVT(Supply, Chr(&H80+Supply) & Chr(&H4F) & Chr(&HAB))
        SendDCPSCommandVT(Supply, Chr(&H80+Supply) & Chr(&H60+i) & Chr(Wyear))

        'month
        SendDCPSCommandVT(Supply, Chr(&H80+Supply) & Chr(&H4F) & Chr(&HAC))
        SendDCPSCommandVT(Supply, Chr(&H80+Supply) & Chr(&H60+i) & Chr(Wmonth))

        'day
        SendDCPSCommandVT(Supply, Chr(&H80+Supply) & Chr(&H4F) & Chr(&HAD))
        SendDCPSCommandVT(Supply, Chr(&H80+Supply) & Chr(&H60+i) & Chr(Wday))
        Echo("Write Supply"+Str(Supply)+vbTab+">"+Format(Wcentury, "00")+Format(Wyear, "00")+"-"+Format(Wmonth, "00")+"-"+Format(Wday, "00"))

    End Function
#End If

    Function CommandMeasureAmpsVT(ByVal Supply As Integer) As Single
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

        CommandMeasureAmpsVT = 0

        '*    0S, 42, 00   Measurement Query to supply S            *
        B1 = Supply
        B2 = &H42
        B3 = 0 ' changed to 0 for TETS, was 128 for the TETS bb

        'Send Command
        SendDCPSCommandVT(Supply, Chr(B1) & Chr(B2) & Chr(B3))

        'Read Sense Measurement
        ErrorStatus = ReadDCPSCommandVT(Supply, ReadBuffer)
        data = ReadBuffer
        If Asc(Strings.Mid(data, 1, 1)) > 64 Then 'byte1
            ' 12 bit current reading ccc - divide by 500 to get amps
            D1 = (Asc(Strings.Mid(data, 1, 1))) And 15 ' byte1 (msb bits 0-3)
            D1 *= 256 ' hi byte
            D2 = (Asc(Strings.Mid(data, 2, 1))) ' byte2 lsb
            ReadBufferA = Format(((D1 + D2) / 500.0), "0.000") ' amps
            CommandMeasureAmpsVT = CSng(Val(ReadBufferA))
        End If
        Application.DoEvents()

    End Function


    Function CommandMeasureVoltsVT(ByVal Supply As Integer) As Single
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
        SendDCPSCommandVT(Supply, Chr(B1) & Chr(B2) & Chr(B3))
        'Read Sense Measurement
        ErrorStatus = ReadDCPSCommandVT(Supply, ReadBuffer)
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
            CommandMeasureVoltsVT = ReadBufferV
        Else
            CommandMeasureVoltsVT = -99
        End If
        Application.DoEvents()

    End Function

    Public Sub CommandSetCurrentVT(ByVal Supply As Integer, ByVal Current2 As Single)
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
        SendDCPSCommandVT(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub

    Sub CommandSetOptionsVT(ByVal Supply As Integer, ByVal OpenRelay As Integer, ByVal SetMaster As Integer, ByVal SenseLocal As Integer, ByVal ConstantVoltage As Integer)
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
        SendDCPSCommandVT(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub


    Public Sub CommandSetPolarityVT(ByVal Supply As Integer, ByVal polarity As String)
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
        SendDCPSCommandVT(Supply, Chr(32 + Supply) & Chr(&H80) & Chr(Byte3)) 'Set polarity

    End Sub


    Public Sub CommandSetVoltageVT(ByVal Supply As Integer, ByVal Volts2 As Single)
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
        SendDCPSCommandVT(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub

    Sub CommandSupplyResetVT(ByVal Supply As Integer)
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
        SendDCPSCommandVT(Supply, Chr(B1) & Chr(B2) & Chr(B3))

        'Send "Reset" Command 1S, 00, 00   Reset Supply S(1-A)                      *
        B1 = &H10 + Supply
        B2 = 0 ' was 128  bb
        B3 = 0 ' was 128  bb
        SendDCPSCommandVT(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub

    Function ReadDCPSCommandVT(ByVal SupplyX As Integer, ByRef ReadBuffer As String) As Integer

        'ex:   ErrorStatus& = ReadDCPSCommandVT(Supply%, ReadBuffer$)

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
                S &= "Byte" & CStr(i) & " =" & Str(Asc(Strings.Mid(ReadBuffer, i, 1))) & vbCrLf
            Next i

            Echo("HW error, Freedom Status Byte count should always be 5,  count =" & Str(Nbr) & vbCrLf & "Read Status=" & S & vbCrLf & "Reading status again")
            ReadBuffer = Space(255)
            ErrorStatus = atxmlDF_viRead(PsResourceName(SupplyX), 0, ReadBuffer, 255, Nbr)
            If Nbr = 5 Then
                ReadBuffer = Strings.Left(ReadBuffer, Nbr)
            End If
        End If
        ReadDCPSCommandVT = ErrorStatus
        Delay(0.5)

    End Function


    Sub ResetPowerSupplies()
        '************************************************************
        '* Nomenclature   : APS 6062 UUT Power Supplies             *
        '*    DESCRIPTION:                                          *
        '*     Resets The Hardware and Software                     *
        '*     To It's Default Power-On State                       *
        '*    EXAMPLE:                                              *
        '*      ResetPowerSupplies                                  *
        '************************************************************
        Dim SupplyIndex As Short

        'Insure no supplies are slaved
        For SupplyIndex = 10 To 1 Step -1
            CommandSetOptions(SupplyIndex, True, True, True, True)
            CommandSupplyResetVT(SupplyIndex)
        Next SupplyIndex


    End Sub

    Sub SendDCPSCommandVT(ByVal slot As Integer, ByVal Command As String)
        '************************************************************
        '* Nomenclature   : APS 6062 UUT Power Supplies             *
        '*    DESCRIPTION:                                          *
        '*     Send a command string to the power supplies          *
        '*    EXAMPLE:                                              *
        '*      SendDCPSCommand -(3, Command$)                       *
        '*    PARAMTERS:                                            *
        '*     slot%     = The slot number of the supply where to   *
        '*                 send the command                         *
        '*     Command$  = The command string to send to the supply *
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
        '  ErrorStatus& = aps6062_writeInstrData(SupplyHandle&(slot%), writeBuffer:=Command$)
        ErrorStatus = atxmlDF_viWrite(PsResourceName(slot), 0, Command, CInt(Command.Length), Nbr)

        Delay(0.5)
        If ErrorStatus <> 0 Then
            '  Echo "Power Supply #"+str$(slot%)+" Error#" & Str$(ErrorStatus&)) 'bb temp
        End If

    End Sub


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
        '  ErrorStatus& = aps6062_writeInstrData(SupplyHandle&(Supply%), writeBuffer:=CommandData$)
        ErrorStatus = atxmlDF_viWrite(PsResourceName(Supply), 0, CommandData, CInt(Len(CommandData)), Nbr)
        Delay(5)

        Array.Clear(sErrorText, 0, sErrorText.Length)
        sErrText = ""

        sPPU_Test_Number = "P" & Format(Supply, "00") & "-01-001" 'Build Test Number

        '*    0S, 44, 00   Status Query to supply S
        CommandData = Chr(Supply) & Chr(&H44) & Chr(&H0) 'Status query
        '    ErrorStatus& = aps6062_writeInstrData(SupplyHandle&(Supply%), writeBuffer:=CommandData$)
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
                Echo(FormatResults(False, "PPU Supply DC" & Str(Supply) & " Built-In Test  =>", sPPU_Test_Number))
                Echo("    " & sErrText)
                RegisterFailure(CStr(PPU), sPPU_Test_Number, sComment:="PPU Supply DC" & Str(Supply) & " Built-In Test  =>   FAILED" & vbCrLf & "Power Supply" & Str(Supply) & "  :  " & sErrText)
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



    Sub DumpPPUStatusVT()
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
            '      ErrorStatus& = aps6062_writeInstrData(SupplyHandle&(Supply%), writeBuffer:=CommandData$)
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

    Sub DumpPPUCalDatesVT()
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
            CommandCalDateReadVT(Supply) ' read  Rcentury,Ryear,Rmonth,Rday
            S = "Read Supply" + Str(Supply) + vbTab + " " + Format(Rcentury, "00") + Format(Ryear, "00") + "-" + Format(Rmonth, "00") + "-" + Format(Rday, "00") & vbCrLf
            frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + S
            Application.DoEvents()
            Application.DoEvents()
            If AbortDump = True Then
                frmOptions.TxtDump.Text = frmOptions.TxtDump.Text + "Dump Aborted" + vbCrLf + vbCrLf
                Exit For
            End If
        Next Supply

    End Sub

    Sub DumpPPURomDataVT()
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
        Supply = DumpSupplyVT
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
                SendDCPSCommandVT(Supply, CStr(Chr(Supply)) & CStr(Chr(Addr \ 256)) & CStr(Chr(Addr Mod 256)))
                SendDCPSCommandVT(Supply, CStr(Chr(Supply)) & CStr(Chr(&H44)) & CStr(Chr(0))) ' status query
                ErrorStatus = ReadDCPSCommandVT(Supply, ReadBuffer) ' get 5 byte status
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
    Public Sub ReleaseDCPS()

        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String = ""


        'Release DCPS1
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_1</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS2
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_2</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS3
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_3</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS4
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_4</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS5
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_5</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS6
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_6</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS7
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_7</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS8
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_8</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS9
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_9</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS10
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_10</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)

    End Sub


End Module