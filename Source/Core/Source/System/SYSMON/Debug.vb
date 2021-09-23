'Option Strict Off
'Option Explicit On

Imports System
Imports System.Diagnostics
Imports Microsoft.VisualBasic

Public Module DebugCode


	'=========================================================
    'Public DebugDwh As Short
    'EASY Debugging (Off VIPERT Station)
    '1. Press [F8]
    '2. Press [Alt]+V
    '3. Press [Alt]+D
    '4. Type Debug%Dwh% = TRUE in debug window to get Debugging privledges
    '5. Data May Be Altered In the SUBS and Functions in this module


    Sub PrintDataDump(ByVal DataDump As String)

        Dim NewStr As String
        Dim DataByte As Short
        Dim FormattedData As String
        NewStr = Convert.ToString(DataDump)
        For DataByte = 1 To 111
            FormattedData = "Byte#" & CStr(DataByte) & "    " & Asc(Mid(NewStr, DataByte, 1)) & " DEC"
            FormattedData &= " , " & Hex(Asc(Mid(NewStr, DataByte, 1))) & " HEX"
            Debug.Print(FormattedData)
        Next DataByte

    End Sub



    Sub PrintWarningQueue()

        Dim Pointer As Short
        Dim BuildStr As String

        For Pointer = 1 To 25
            BuildStr = "Position:" & CStr(Pointer) & " "
            If WarningQueue(Pointer).Topic <> "" Then
                BuildStr &= WarningQueue(Pointer).Topic & " "
                BuildStr &= WarningQueue(Pointer).Component
            End If
            Debug.Print(BuildStr)
        Next Pointer

    End Sub

    Function SendStringOfDebugData() As String
        SendStringOfDebugData = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     This Module takes the SYSMON large data dump string  *
        '*     and formats the bytes into useable data and places   *
        '*     the data into a user defined type --> ChassisData    *
        '*    EXAMPLE:                                              *
        '*     PrimaryChassis = FormatSystemMonitorData(DataBuffer$)*
        '*    RETURNS:                                              *
        '*     Formatted Data as ChassisData User Defined Type      *
        '************************************************************
        Dim DebugStringBuild As String = ""

        Dim SlotIndex As Short
        Dim Chassis As ChassisData
        Dim ChassByte As String
        Dim CountX As Short
        Static ChassNum As Short


        'Alternate Data To Both Chassis
        If ChassNum = 1 Then
            ChassByte = Convert.ToString(Chr(&H8))
            ChassNum = 2
        Else
            ChassByte = Convert.ToString(Chr(&H0))
            ChassNum = 1
        End If

        DebugStringBuild &= Convert.ToString(Chr(22)) 'ActionByte1 1

        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 1 Current 2
        DebugStringBuild &= Convert.ToString(Chr(&H0 + &H80)) 'Power Supply 1 Voltage HH 3
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 1 Voltage LL 4
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 2 Current 5
        DebugStringBuild &= Convert.ToString(Chr(&H0 + &H80)) 'Power Supply 2 Voltage HH 6
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 2 Voltage LL 7
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 3 Current 8
        DebugStringBuild &= Convert.ToString(Chr(&H0 + &H80)) 'Power Supply 3 Voltage HH 9
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 3 Voltage LL 10
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 4 Current 11
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 4 Voltage HH 12
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 4 Voltage LL 13
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 5 Current 14
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 5 Voltage HH 15
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 5 Voltage LL 16
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 6 Current 17
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 6 Voltage HH 18
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 6 Voltage LL 19
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 7 Current 20
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 7 Voltage HH 21
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 7 Voltage LL 22
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 8 Current 23
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 8 Voltage HH 24
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 8 Voltage LL 25
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 9 Current 26
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 9 Voltage HH 27
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 9 Voltage LL 28
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 10 Current 29
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 10 Voltage HH 30
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Power Supply 10 Voltage LL 31

        DebugStringBuild &= Convert.ToString(Chr(64)) 'ICPU Status 32

        DebugStringBuild &= Convert.ToString(Chr(80)) ''Backplane Supply Current +5A 33
        DebugStringBuild &= Convert.ToString(Chr(68)) ''Backplane Supply Current +5B 34
        DebugStringBuild &= Convert.ToString(Chr(21)) ''Backplane Supply Current +12 35
        DebugStringBuild &= Convert.ToString(Chr(1)) ''Backplane Supply Current +24A 36

        DebugStringBuild &= vbTab ''Backplane Supply Current -2 37
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(20) ''Backplane Supply Current +24B 37

        DebugStringBuild &= Convert.ToString(Chr(59)) ''Backplane Supply Current -5.2A 38
        DebugStringBuild &= vbTab ''Backplane Supply Current -24 39
        DebugStringBuild &= Convert.ToString(Chr(7)) ''Backplane Supply Current -12 40
        DebugStringBuild &= vbTab ''Backplane Supply Current -2 41
        DebugStringBuild &= Convert.ToString(Chr(1)) ''Backplane Supply Voltage HH +5A 42
        DebugStringBuild &= Convert.ToString(Chr(244)) ''Backplane Supply Voltage LL +5A 43
        DebugStringBuild &= Convert.ToString(Chr(1)) ''Backplane Supply Voltage HH +5B 44
        DebugStringBuild &= Convert.ToString(Chr(242)) ''Backplane Supply Voltage LL +5B 45
        DebugStringBuild &= Convert.ToString(Chr(4)) ''Backplane Supply Voltage HH +12A 46
        DebugStringBuild &= Convert.ToString(Chr(183)) ''Backplane Supply Voltage LL +12A 47
        DebugStringBuild &= vbTab ''Backplane Supply Voltage HH +24A 48
        DebugStringBuild &= Convert.ToString(Chr(100)) ''Backplane Supply Voltage LL +24A 49


        DebugStringBuild &= Convert.ToString(Chr(&H0)) ''Backplane Supply Voltage HH -2A 50
        DebugStringBuild &= Convert.ToString(Chr(200)) ''Backplane Supply Voltage LL -2A 51
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(9) ''Backplane Supply Voltage HH +24B 50
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(95) ''Backplane Supply Voltage LL +24B 51

        DebugStringBuild &= Convert.ToString(Chr(2)) ''Backplane Supply Voltage HH -5.2A 52
        DebugStringBuild &= vbLf ''Backplane Supply Voltage LL -5.2A 53
        DebugStringBuild &= vbTab ''Backplane Supply Voltage HH -24A 54
        DebugStringBuild &= Convert.ToString(Chr(112)) ''Backplane Supply Voltage LL -24A 55
        DebugStringBuild &= Convert.ToString(Chr(4)) ''Backplane Supply Voltage HH -12A 56
        DebugStringBuild &= Convert.ToString(Chr(178)) ''Backplane Supply Voltage LL -12A 57
        DebugStringBuild &= Convert.ToString(Chr(&H0)) ''Backplane Supply Voltage HH -2B 58
        DebugStringBuild &= Convert.ToString(Chr(200)) ''Backplane Supply Voltage LL -2B 59

        DebugStringBuild &= ChassByte '& Chr$(&H4) 'ActionByte2 60

        'STEST|HTR2|HTR1|AFS3|AFS2|AFS1
        '32    16   8    4    2    1
        DebugStringBuild &= Convert.ToString(Chr(32 + 16 + 4 + 2 + 1)) 'Chassis Status Byte 61

        DebugStringBuild &= Convert.ToString(Chr(100)) 'Fan Speed 62

        For CountX = 0 To 12
            DebugStringBuild &= Convert.ToString(Chr(200)) '144 = 3 deg rise 150=6
        Next CountX
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(142) 'Slot 0 Chassis Temperature  63
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(154) 'Slot 1 Chassis Temperature  64
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(147) 'Slot 2 Chassis Temperature  65
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(146) 'Slot 3 Chassis Temperature  66
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(153) 'Slot 4 Chassis Temperature  67
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(156) 'Slot 5 Chassis Temperature  68
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(152) 'Slot 6 Chassis Temperature  69
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(143) 'Slot 7 Chassis Temperature  70
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(140) 'Slot 8 Chassis Temperature  71
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(142) 'Slot 9 Chassis Temperature  72
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(148) 'Slot 10 Chassis Temperature  73
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(148) 'Slot 11 Chassis Temperature  74
        'DebugStringBuild$ = DebugStringBuild$ & Chr$(144) 'Slot 12 Chassis Temperature  75

        DebugStringBuild &= Convert.ToString(Chr(190)) '138 Ambient (Intake) Temperature 76

        DebugStringBuild &= Convert.ToString(Chr(11)) '+24V Backplane Voltage HH 77
        DebugStringBuild &= Convert.ToString(Chr(173)) '+24V Backplane Voltage LL 78
        DebugStringBuild &= Convert.ToString(Chr(11)) '+12V Backplane Voltage HH 79
        DebugStringBuild &= Convert.ToString(Chr(175)) '+12V Backplane Voltage LL 80
        DebugStringBuild &= vbTab '+5V Backplane Voltage HH 81
        DebugStringBuild &= Convert.ToString(Chr(117)) '+5V Backplane Voltage LL 82
        DebugStringBuild &= Convert.ToString(Chr(7)) '+2V Backplane Voltage HH 83
        DebugStringBuild &= Convert.ToString(Chr(191)) '+2V Backplane Voltage LL 84
        DebugStringBuild &= vbLf '-5.2V Backplane Voltage HH 85
        DebugStringBuild &= Convert.ToString(Chr(5)) '-5.2V Backplane Voltage LL 86
        DebugStringBuild &= Convert.ToString(Chr(11)) '-12V Backplane Voltage HH 87
        DebugStringBuild &= Convert.ToString(Chr(146)) '-12V Backplane Voltage LL 88
        DebugStringBuild &= Convert.ToString(Chr(11)) '-24V Backplane Voltage HH 89
        DebugStringBuild &= Convert.ToString(Chr(206)) '-24V Backplane Voltage LL 90

        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'VinSinglePhase 91
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'VinThreePhase 92
        DebugStringBuild &= Convert.ToString(Chr(2)) 'Current In 93
        DebugStringBuild &= Convert.ToString(Chr(47)) 'Power Ok 94
        DebugStringBuild &= Convert.ToString(Chr(141)) 'DCV Level 95
        DebugStringBuild &= Convert.ToString(Chr(&H0)) 'Input Power Status 96

        'Dme added for new freedom power supply up to 111
        DebugStringBuild &= Convert.ToString(Chr(21)) ''Backplane Supply Current +12B 97
        DebugStringBuild &= Convert.ToString(Chr(1)) ''Backplane Supply Current +24B 98
        DebugStringBuild &= Convert.ToString(Chr(59)) ''Backplane Supply Current -5.2B 99
        DebugStringBuild &= vbTab ''Backplane Supply Current -24b 100
        DebugStringBuild &= Convert.ToString(Chr(7)) ''Backplane Supply Current -12B 101

        DebugStringBuild &= Convert.ToString(Chr(4)) ''Backplane Supply Voltage HH +12B 102
        DebugStringBuild &= Convert.ToString(Chr(183)) ''Backplane Supply Voltage LL +12B 103
        DebugStringBuild &= vbTab ''Backplane Supply Voltage HH +24B 104
        DebugStringBuild &= Convert.ToString(Chr(100)) ''Backplane Supply Voltage LL +24B 105
        DebugStringBuild &= Convert.ToString(Chr(2)) ''Backplane Supply Voltage HH -5.2B 106
        DebugStringBuild &= vbLf ''Backplane Supply Voltage LL -5.2B 107
        DebugStringBuild &= vbTab ''Backplane Supply Voltage HH -24B 108
        DebugStringBuild &= Convert.ToString(Chr(112)) ''Backplane Supply Voltage LL -24B 109
        DebugStringBuild &= Convert.ToString(Chr(4)) ''Backplane Supply Voltage HH -12B 110
        DebugStringBuild &= Convert.ToString(Chr(178)) ''Backplane Supply Voltage LL -12B 111

        'Return Function Value
        SendStringOfDebugData = DebugStringBuild

    End Function


    Function GetStatusDebug() As Short

        Dim TheByte As Short
        '-------------------------------------------------------------------------------
        '- Action Status Byte ----------------------------------------------------------
        '-        B7 |  B6 |        B5 |           B4 | B3 |     B2 |     B1 |      B0 -
        '- Data Dump | RQS | Ret Error | Module Fault | A3 | A2/28V | A1/PRB | A0/RCVR -
        '-------------------------------------------------------------------------------
        'Data Dump 1000 xxxx
        'Query Failed 0010 xxxx
        'Mod Failed 0001 ADDR
        'Action Byte 1011 xABC where A=28V, B=PRB, C=RCVR
        'ICPU Response 0011 xxxx
        'Module Response 0100 ADDR
        'Generally 80H = Data Dump
        '          BXH = Action Byte
        '-------------------------------------------------------------------------------
        'ActChassisAddress% = ActionByteValue% And 8
        'ActVolt28Ok% = ActionByteValue% And 4
        'ActProbeEvent% = ActionByteValue% And 2
        'ActReceiverEvent% = ActionByteValue% And 1

        TheByte = (&H10) Or (&HB0) Or (&H8) Or (&H4)

        GetStatusDebug = TheByte

    End Function



    Public Function TempSensorSpy(ByVal sBuffer As String, ByVal iChassis As Short) As Object
        '*********************************************************************
        '**  Debugging Function to record the Temp values from the          **
        '**  Data Dump. Results are printed to a text file for evaluation.  **
        '**  Text File Name: "C:\_SysDiag.txt". The file will be Appended   **
        '**  so, it must be deleted manually.                               **
        '** Arguments:                                                      **
        '**  sBuffer = Data Dump String                                     **
        '**  iChassis = Chassis number. 1=Pri  2=Sec                        **
        '*********************************************************************

        Dim sDataOut As String = ""
        Dim iIndex As Short
        Dim iElement As Short
        Dim sData(14) As String

        iElement = 63
        For iIndex = 0 To 13
            sData(iIndex) = "Buffer Element: " & Asc(Mid(sBuffer, iElement, 1)) & Space(10)
            sData(iIndex) &= "Temp Value: " & (Asc(Mid(sBuffer, iElement, 1)) / 2) - 45

            If iIndex < 13 Then
                sDataOut &= "Temp Sensor " & iIndex & " = " & sData(iIndex) & vbCrLf
            Else
                sDataOut &= "Intake Temp =  " & sData(iIndex) & vbCrLf
            End If
            iElement += 1
        Next

        'Write to a File Here.
        FileOpen(1, "C:\_SysDiag.txt", OpenMode.Append)
        PrintLine(1, "********************************************************")
        If iChassis = 1 Then
            PrintLine(1, "Primary Chassis Data Dump at: " & TimeOfDay)
        Else
            PrintLine(1, "Secondary Chassis Data Dump at: " & TimeOfDay)
        End If
        PrintLine(1, sDataOut)
        FileClose(1)

    End Function




End Module