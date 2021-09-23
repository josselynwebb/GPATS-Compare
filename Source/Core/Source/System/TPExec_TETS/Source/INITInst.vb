Option Strict Off
Option Explicit On
Module INITInst
    '----------------- VXIplug&play Driver Declarations ---------
    Declare Function Sw_Init Lib "RI1260.DLL" Alias "ri1260_init" (ByVal rsrcName As String, ByVal idQuery As Short, ByVal reset_Renamed As Short, ByRef vi As Integer) As Integer
    Declare Function Sw_Close Lib "RI1260.DLL" Alias "ri1260_close" (ByVal vi As Integer) As Integer
    Declare Function Sw_Self_Test Lib "RI1260.DLL" Alias "ri1260_self_test" (ByVal vi As Integer, ByRef test_result As Short, ByRef test_message As String) As Integer
    Declare Function Sw_Error_Query Lib "RI1260.DLL" Alias "ri1260_error_query" (ByVal vi As Integer, ByRef error_code As Integer, ByVal error_message As String) As Integer
    Declare Function Sw_Error_Message Lib "RI1260.DLL" Alias "ri1260_error_message" (ByVal vi As Integer, ByRef error_code As Integer, ByVal Message As String) As Integer
    Declare Function Sw_Reset Lib "RI1260.DLL" Alias "ri1260_reset" (ByVal vi As Integer) As Integer
    Declare Function Sw_closeAllRMSessions Lib "RI1260.DLL" Alias "ri1260_closeAllRMSessions" () As Integer

    '-----------------API / DLL Declarations------------------------------
    Declare Function hpe1428a_init Lib "HPE1428A.DLL" (ByVal resourceName As String, ByVal idQuery As Short, ByVal resetDevice As Short, ByRef instrumentHandle As Integer) As Integer
    Declare Function hpe1428a_readInstrData Lib "HPE1428A.DLL" (ByVal instrumentHandle As Integer, ByVal numberBytesToRead As Integer, ByVal ReadBuffer As String, ByRef numBytesRead As Integer) As Integer
    Declare Function hpe1428a_writeInstrData Lib "HPE1428A.DLL" (ByVal instrumentHandle As Integer, ByVal writeBuffer As String) As Integer
    Declare Function hpe1428a_errorMessage Lib "HPE1428A.DLL" (ByVal instrumentHandle As Integer, ByVal errorcode As Integer, ByVal errorMessage As String) As Integer

    Const MSG_RESP_REG As Short = 10
    Const DOR_MASK As Integer = &H2000
    Const MANUF As Short = 1
    Const MODEL As Short = 2
    Const MANUF_REG As Short = 0
    Const MODEL_REG As Short = 2
    Const STATUS_REG As Short = 4

    Public SwInfo As String

    Public AllPins(191) As Short 'Driver Sensor Pins

    ''' <summary>
    ''' This Routine initializes the Modulation Analyzer and verifies that it is the correct instrument
    ''' </summary>
    ''' <returns>An integer representing any errors that may occur</returns>
    ''' <remarks></remarks>
    Function iInitModAnal() As Boolean
        Dim InitStatus As Integer
        Dim ErrorStatusRead As Integer
        Dim SystErr As Integer

        iInitModAnal = True
        'ninstrumentInitialized(MOD_ANAL) = True
        InitStatus = viOpen(nSessionHandle, sInstrumentSpec(MOD_ANAL), VI_NULL, VI_NULL, nInstrumentHandle(MOD_ANAL))

        If InitStatus <> VI_SUCCESS Then
            Echo("Could not get instrument handle for " & sInstrumentSpec(MOD_ANAL))
            'InstrumentInitialized(MOD_ANAL) = False
            iInitModAnal = False
            Exit Function
        End If

        ErrorStatusRead = viSetAttribute(nInstrumentHandle(MOD_ANAL), VI_ATTR_TMO_VALUE, 10000)
        SystErr = ReadMsg(MOD_ANAL, ReadBuffer)
        WriteMsg(MOD_ANAL, "PG99RE")
        Delay(4)
        WriteMsg(MOD_ANAL, "CL" & vbCrLf)
    End Function

    ''' <summary>
    ''' This Routine checks to see if the EIP 1315A Downconverter is available and configured properly.  
    ''' If not available, then it checks for the GT 55240A Freq. Translator.
    ''' </summary>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Function iInitRFReceiver() As Short
        Dim InitStatus As Integer

        iInitRFReceiver = True
        iInstrumentInitialized(RFREC) = CStr(True)
        InitStatus = viOpen(nSessionHandle, sInstrumentSpec(RFREC), VI_NULL, VI_NULL, nInstrumentHandle(RFREC))
        If InitStatus = VI_SUCCESS Then
            sInstrumentDescription(RFREC) = "EIP 1315A RF Downconverter"
        ElseIf InitStatus = VI_ERROR_RSRC_NFOUND Then
            sInstrumentSpec(RFREC) = sInstrumentSpec(RFSYN)
            sInstrumentDescription(RFREC) = "Giga-tronics 55210A Frequency Translator"
            sIDNResponse(RFREC) = "1=FT"
        Else
            Echo("Could not get instrument handle for " & sInstrumentSpec(RFREC))
            iInstrumentInitialized(RFREC) = CStr(False)
            iInitRFReceiver = True
        End If
    End Function

    ''' <summary>
    ''' This Routine checks to see if the proper switching cards are available and functing.
    ''' </summary>
    ''' <param name="iInst">the index of the instrument</param>
    ''' <returns>True if all switching cards are available and configured correctly</returns>
    ''' <remarks></remarks>
    Function iInitSwitching(ByRef iInst As Integer) As Short
        Dim NumChars As Short
        Dim SystemDirectory As String
        Dim S As String
        Dim SwInfo As String = ""
        Const conNoDLL As Short = 48
        Dim ErrorStatus As Integer

        'Quote = Chr(34)
        Select Case iInst
            Case TETS.SWITCH1
                On Error Resume Next
                ErrorStatus = Sw_Init(rsrcName:=sInstrumentSpec(TETS.SWITCH1), idQuery:=1, reset_Renamed:=1, vi:=nInstrumentHandle(TETS.SWITCH1))
                If Err.Number = conNoDLL Or ErrorStatus Then
                    Echo("Could not get instrument Handle for " & sInstrumentDescription(TETS.SWITCH1))
                    Echo("Error in loading Ri1260.DLL.")
                    iInstrumentInitialized(SWITCH1) = CStr(False)
                    iInitSwitching = False
                    Exit Function
                Else
                    iInitSwitching = True
                End If
                Delay(0.5)
                On Error GoTo 0

                ErrorStatus = Sw_Reset(nInstrumentHandle(TETS.SWITCH1))
                If ErrorStatus Then
                    Echo("INSTRUMENT ERROR: Could not reset " & sInstrumentDescription(TETS.SWITCH1))
                    iInitSwitching = False
                    Exit Function
                End If

                WriteSW("PDATAOUT 0-5")
                Do
                    S = ReadSW()
                    SwInfo = SwInfo & S
                Loop Until InStr(S, "END") Or S = ""

                If InStr(SwInfo, "001. 1260-39") = 0 Then
                    Echo("SWITCH ERROR: Did not find a 1260-39 switch module at switch module address 1.")
                    iInstrumentInitialized(SWITCH1) = CStr(False)
                    iInitSwitching = False
                    Exit Function
                End If
            Case SWITCH2
    '            If InStr(SwInfo, "002. 1260-39") = 0 Then
    '                Echo "SWITCH ERROR: Did not find a 1260-39 switch module at switch module address 2."
    '                iInstrumentInitialized(SWITCH1) = False
    '                iInitSwitching = False
    '                Exit Function
    '            End If
                iInitSwitching = True
                Exit Function
            Case SWITCH3
    '            If InStr(SwInfo, "003. 1260-38A") = 0 Then
    '                Echo "SWITCH ERROR: Did not find a 1260-38T switch module at switch module address 3."
    '                iInstrumentInitialized(SWITCH1) = False
    '                iInitSwitching = False
    '                Exit Function
    '            End If
                iInitSwitching = True
                Exit Function
            Case SWITCH4
    '            If InStr(SwInfo, "004. 1260-58") = 0 Then
    '                Echo "SWITCH ERROR: Did not find a 1260-58 switch module at switch module address 4."
    '                iInstrumentInitialized(SWITCH1) = False
    '                iInitSwitching = False
    '                Exit Function
    '            End If
                iInitSwitching = True
                Exit Function
            Case SWITCH5
    '            If bRFOptionInstalled And (InStr(SwInfo, "005. 1260-66A") = 0) Then
    '                Echo "SWITCH ERROR: Did not find a 1260-66A switch module at switch module address 5."
    '                iInstrumentInitialized(SWITCH1) = False
    '                iInitSwitching = False
    '                Exit Function
    '            End If
    ' Close Internal Relays to make matricis
                WriteSW("CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
                iInitSwitching = True
                Exit Function
        End Select
    End Function

    ''' <summary>
    ''' This Routine checks to see if the Digital Test System (DTS) is available and configured properly.
    ''' </summary>
    ''' <param name="bReset">Indicates whether or not to reset the instrument</param>
    ''' <returns>True if the DTS initializes and is properly cofigured</returns>
    ''' <remarks>MODIFIES:  instrumentHandle(DIGITAL)</remarks>
    Function iInitDigital(ByRef bReset As Boolean) As Short
        Dim cardCount, InitStatus, viSession, channelCount As Integer
        Dim desc As String
        Dim cardName As String = Space(33)
        Dim addlInfo As String = Space(2049)
        Dim iStepCount As Short 'Increments the Sub Test ID in the Faut ID
        Dim nReset As Integer

        If bReset = True Then
            nReset = VI_TRUE
        Else
            nReset = VI_FALSE
        End If

        iInitDigital = True
        iStepCount = 5 'Initialize Step Counter

        InitStatus = terM9_init("TERM9::#0", VI_FALSE, nReset, viSession) 'Chassis 0
        If (InitStatus <> VI_SUCCESS) Or (viSession = VI_NULL) Then
            Echo("INSTRUMENT ERROR - Error locating DIGITAL CRB in VXI primary chassis, slot 4.")
            iInitDigital = False
            Exit Function
        Else
            nInstrumentHandle(DIGITAL) = viSession
        End If

        InitStatus = terM9_getSystemInformation(viSession, lpBuffer, cardCount, channelCount)
        'Retrieve M9 System Information
        If InitStatus <> VI_SUCCESS Then
            Echo("INSTRUMENT ERROR - Error initializing DIGITAL CRB in VXI primary chassis, slot 4.")
            iInitDigital = False
            Exit Function
        Else
            desc = StripNullCharacters(lpBuffer)
        End If
        'Instrument Identification
        If InStr(desc, "M9 Digital Test Instrument") = 0 Then
            Echo("INSTRUMENT ERROR - M9 Digital Test Instrument could not be Initialized.")
            iInitDigital = False
            Exit Function
        End If

        'Card Count
        If cardCount <> 5 Then
            Echo("INSTRUMENT ERROR - Incorrect number of M910 cards: " & cardCount.ToString() & " found.")
            iInitDigital = False
            Exit Function
        End If

        ' Set the systemwide ground reference source to INTERNAL.
        terM9_setGroundReference(viSession, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        terM9_setSystemEnable(viSession, VI_TRUE)
    End Function

    ''' <summary>
    ''' This Routine checks to see if the DCPS are available and configured properly.
    ''' </summary>
    ''' <param name="bReset">Indicates whether power supplies should be reset</param>
    ''' <returns>True if sucessful; otherwise False</returns>
    ''' <remarks></remarks>
    Function iInitDCPS(Optional ByRef bReset As Boolean = False) As Short
        Dim Supply, attempt As Short
        Dim SystErr As Integer
        Dim iCnt As Short
        Dim Nbr As Integer

        iInitDCPS = True
        If Not bReset Then
            For Supply = 10 To 1 Step -1
                For attempt = 1 To 3 'Try multiple times to get a handle
                    SystErr = aps6062_init(ResourceNameX:="GPIB0::5::" & Supply.ToString(), idQuery:=0, resetDevice:=0, instrumentHandle:=nSupplyHandle(Supply))
                    If SystErr = VI_SUCCESS Then Exit For
                    Delay(0.2)
                Next attempt

                If SystErr <> VI_SUCCESS Then
                    Echo("Could not get instrument handle for GPIB0::5::" & CStr(Supply))
                    iInitDCPS = False
                    nSupplyHandle(Supply) = 0
                    iInitDCPS = False
                    Exit Function
                End If
                SystErr = viSetAttribute(nSupplyHandle(Supply), VI_ATTR_TMO_VALUE, 3000)
            Next Supply
        End If

        For Supply = 10 To 1 Step -1
            'Init/Reset Supplies
            SystErr = atxmlDF_viWrite(PsResourceName(Supply), 0, Chr(&H10 + Supply) & Chr(0) & Chr(0), 3, Nbr)
            If SystErr <> VI_SUCCESS And Not bReset Then
            End If
        Next Supply

        For Supply = 10 To 1 Step -1
            Delay(0.2)
            iCnt = 0
            Do
                'Send Status Query
                SystErr = atxmlDF_viWrite(PsResourceName(Supply), 0, Chr(Supply) & Chr(&H44) & Chr(0), 3, Nbr)
                'Read Status
                SystErr = atxmlDF_viRead(PsResourceName(Supply), 0, lpBuffer, lpBuffer.Length, Nbr)
                iCnt = iCnt + 1
                If iCnt > 20 Then Exit Do
            Loop While CByte(Asc(Left(lpBuffer, 1))) = 0

            If (CByte(Asc(Left(lpBuffer, 1))) <> &H20) Or (CByte(Asc(Mid(lpBuffer, 2, 1))) <> &H80) Then
                SystErr = atxmlDF_viWrite(PsResourceName(Supply), 0, Chr(&H10 + Supply) & Chr(0) & Chr(0), 3, Nbr)
                If SystErr <> VI_SUCCESS And Not bReset Then
                    Echo("Error sending Status Query to DCPS " & Supply & ", Error code: " & Hex(SystErr))
                    nSupplyHandle(Supply) = 0
                    iInitDCPS = False
                    Exit Function
                End If
            End If
        Next Supply
    End Function

    ''' <summary>
    ''' Send a command string to a power supply 
    ''' </summary>
    ''' <param name="nSupply">The number of the supply where to send the command</param>
    ''' <param name="Cmd">The command string to send to the supply</param>
    ''' <remarks></remarks>
    Public Sub SendDCPSCommand(ByRef nSupply As Integer, ByRef Cmd As String)
        Dim ErrorStatus As Integer
        Dim Nbr As Integer

        If Not bSimulation Then
            If Len(Cmd) <> 3 Then Exit Sub
            ErrorStatus = atxmlDF_viWrite(PsResourceName(nSupply), 0, Cmd, CInt(Len(Cmd)), Nbr)
            Delay(0.5)
            If ErrorStatus <> 0 Then
                Echo("DCPS PROGRAMMING ERROR:" & nSupply & " Command: " & Cmd)
                Err.Raise(ErrorStatus)
            End If
        End If
    End Sub

    Function ReadDCPSCommand(ByRef SupplyX As Integer, ByRef ReadBuffer As String) As Integer
        'ex:   ErrorStatus = ReadDCPSCommand(Supply, ReadBuffer)
        Dim ErrStatus As Integer
        Dim Nbr As Integer
        Dim S As String
        Dim i As Short

        ReadBuffer = Space(255)
        'bbbb  ErrorStatus = aps6062_readInstrData(SupplyHandle(SupplyX), numberBytesToRead:=255, ReadBuffer:=ReadBuffer, numBytesRead:=Nbr)
        ErrStatus = atxmlDF_viRead(PsResourceName(SupplyX), 0, ReadBuffer, 255, Nbr)

        'the power supply should always return 5 bytes for a status read command
        If Nbr = 5 Then
            ReadBuffer = Left(ReadBuffer, Nbr)
        Else
            S = ""
            For i = 1 To Nbr
                S = S & "Byte" & CStr(i) & " =" & Str(Asc(Mid(ReadBuffer, i, 1))) & vbCrLf
            Next i

            Echo("HW error, Freedom Status Byte count should always be 5,  count =" & Str(Nbr) & vbCrLf & "Read Status=" & S & vbCrLf & "Reading status again")
            ReadBuffer = Space(255)
            'bbbb   ErrorStatus = aps6062_readInstrData(SupplyHandle(SupplyX), numberBytesToRead:=255, ReadBuffer:=ReadBuffer, numBytesRead:=Nbr)
            ErrStatus = atxmlDF_viRead(PsResourceName(SupplyX), 0, ReadBuffer, 255, Nbr)
            If Nbr = 5 Then
                ReadBuffer = Left(ReadBuffer, Nbr)
            End If
        End If
        ReadDCPSCommand = ErrStatus
        Delay(0.5)
    End Function

    ''' <summary>
    ''' This Module resets a power supply
    ''' </summary>
    ''' <param name="nSupply">The supply number of which to reset</param>
    ''' <remarks></remarks>
    Public Sub SupplyReset(ByRef nSupply As Integer)
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        'Send "Open All Relays" Command
        B1 = 32 + nSupply
        B2 = &HAA
        B3 = &H20
        SendDCPSCommand(nSupply, Chr(B1) & Chr(B2) & Chr(B3))
        Delay(0.3)
        'Send "Reset" Command
        B1 = 16 + nSupply
        B2 = 128
        B3 = 128
        SendDCPSCommand(nSupply, Chr(B1) & Chr(B2) & Chr(B3))
    End Sub

    ''' <summary>
    ''' This Module sets the optional settings of a power supply
    ''' </summary>
    ''' <param name="nSupply">The supply number of which to set</param>
    ''' <param name="OpenRelay">Set to False to close the output relay</param>
    ''' <param name="SetMaster">Set to True to set the supply to master mode</param>
    ''' <param name="SenseLocal">Set to False to remote sense</param>
    ''' <param name="ConstantVoltage">Set to True for constant Voltage mode, and False for constant current mode.</param>
    ''' <remarks></remarks>
    Sub CommandSetOptions(ByRef nSupply As Integer, ByRef OpenRelay As Short, ByRef SetMaster As Short, ByRef SenseLocal As Short, ByRef ConstantVoltage As Short)
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        B1 = 32 + nSupply

        'Note: If a +1 is passed do nothing
        Select Case OpenRelay
            Case True '(-1)
                B2 = B2 + 32 'Enable / Relay Open
            Case False '( 0)
                B2 = B2 + 32 'Enable
                B2 = B2 + 16 'Relay Closed
        End Select

        Select Case SetMaster
            Case True '(-1)
                B2 = B2 + 8 'Enable /Set as Master
            Case False '( 0)
                B2 = B2 + 4 'Set as Slave
                B2 = B2 + 8 'Enable
        End Select

        Select Case SenseLocal
            Case True '(-1)
                B2 = B2 + 2 'Enable / Sense Local
            Case False '( 0)
                B2 = B2 + 2 'Enable
                B2 = B2 + 1 'Sense Remote
        End Select

        Select Case ConstantVoltage
            Case True '(-1)
                B2 = B2 + 128
                B3 = B3 + 32 'Enable / Current Limiting(Protection)
            Case False '( 0)
                B3 = B3 + 16 'Constant Current
                B2 = B2 + 128
                B3 = B3 + 32 'Enable
        End Select

        'Send Command
        SendDCPSCommand(nSupply, Chr(B1) & Chr(B2) & Chr(B3))
    End Sub
End Module