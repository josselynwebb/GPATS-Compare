Attribute VB_Name = "INITInst"
Option Explicit
DefInt A-Z

Const MSG_RESP_REG = 10
Const DOR_MASK = &H2000
Const MANUF = 1
Const MODEL = 2
Const MANUF_REG = 0
Const MODEL_REG = 2
Const STATUS_REG = 4

Public SwInfo As String
Global SwitchCardOK(5)
Global IDList$(10)
Global IDData$()


Public AllPins(0 To 191) As Integer 'Driver Sensor Pins

Function iInitSwitching(iInst As Long) As Integer
    'DESCRIPTION:
    '   This Routine checks to see if the proper switching cards are available and functioning.
    'PARAMETERS:
    '   iInst = the index of the instrument
    'RETURNS:
    '   True if all switching cards are available and configured correctly
    'MODIFIES:
    '   instrumentHandle&(SWITCH1)
    '   SwitchCardOK()
    'EXAMPLE:
    '   If iInitSwitching(iInstToInitialize) Then
    Dim ErrorStatus&
    Static InitStatus&
    Dim RetDevParam%
    Dim MsgRespReg%
    Dim SwitchIDText%
    Dim Items%

    If InitStatus& = 99 Then    'If no communication with switch controller
        iInitSwitching = False
        Exit Function
    End If
    iInitSwitching = True
    
    Echo ""
    Select Case iInst
        Case SWITCH1
'LF1
            For Items% = SWITCH_CONTROLLER To RFSWITCH
                SwitchCardOK(Items%) = False
            Next
            ErrorStatus& = -1
            ErrorStatus& = viOpen(nSessionHandle&, sInstrumentSpec$(SWITCH1), VI_NULL, VI_NULL, nInstrumentHandle&(SWITCH1))
            
            If ErrorStatus& < VI_SUCCESS Then
                Echo "Could not get instrument Handle for " & sInstrumentSpec$(SWITCH1)
                Echo "Initialization Failure."
                InitStatus& = 99
                iInstrumentInitialized(SWITCH1) = False
                iInitSwitching = False
                Exit Function
            Else
                WriteMsg SWITCH1, "PDATAOUT 0-12"
                RetDevParam% = 0
                Do
                    ReDim Preserve IDData$(RetDevParam%)
                    nSystErr = ReadMsg(SWITCH1, IDData$(RetDevParam%))
                    RetDevParam% = RetDevParam% + 1
                    ' Delay enough time for switching module to output another line of data to the output
                    '   buffer and then check the Data Output Ready(DOR) bit to see if there is another
                    '   line of text to be downloaded.  Repeat process until done
                    Delay 0.2
                    MsgRespReg = &H3FF7
                    nSystErr = atxmlDF_viIn16(ResourceName(SWITCH1), 0&, VI_A16_SPACE, MSG_RESP_REG, MsgRespReg%)
                   
                Loop While ((MsgRespReg% And DOR_MASK) <> 0) And (RetDevParam% < 31)
                For SwitchIDText% = 0 To RetDevParam% - 1
                    Items% = StringToList(IDData$(SwitchIDText%), IDList$(), ".")
                    Select Case Val(IDList$(1))
                        Case SWITCH_CONTROLLER
                            If Trim$(IDList$(2)) = "MODEL 1260 UNIVERSAL SWITCH CONTROLLER FOR VXI REV 1" Then
                                SwitchCardOK(SWITCH_CONTROLLER) = True
                            End If
                        Case LFSWITCH1
                            If Trim$(IDList$(2)) = "1260-39 HIGH DENSITY MULTIPLE CONFIGURATION SWITCH MODULE" Then
                                SwitchCardOK(LFSWITCH1) = True
                            End If
                        Case LFSWITCH2
                            If Trim$(IDList$(2)) = "1260-39 HIGH DENSITY MULTIPLE CONFIGURATION SWITCH MODULE" Then
                                SwitchCardOK(LFSWITCH2) = True
                            End If
                        Case PWRSWITCH
                            If Trim$(IDList$(2)) = "1260-38A 1x128 2-WIRE  SCANNER/MULTIPLEXER" Then
                                SwitchCardOK(PWRSWITCH) = True
                            End If
                        Case MFSWITCH
                            If Trim$(IDList$(2)) = "1260-58 4 1x8 750 MHZ SWITCHING MODULE" Then
                                SwitchCardOK(MFSWITCH) = True
                            End If
                        Case RFSWITCH
                            If Trim$(IDList$(2)) = "1260-66A Six 1x6 MICROWAVE SWITCHING MODULE" Then
                                SwitchCardOK(RFSWITCH) = True
                            End If
                    End Select
                Next
            End If

'LF1-00-N02
            If Not SwitchCardOK(SWITCH_CONTROLLER) Then
              Echo sInstrumentDescription$(SWITCH1) & " Controller identification FAILED."
              iInstrumentInitialized(SWITCH1) = False
              iInitSwitching = False
            End If

'LF1-00-N03
            If Not SwitchCardOK(LFSWITCH1) Then
              Echo sInstrumentDescription$(SWITCH1) & " identification FAILED"
              iInstrumentInitialized(SWITCH1) = False
              iInitSwitching = False
            End If

'LF2-00-N01, LF3-00-N01, MFS-00-N01, HFS-00-N01
        Case SWITCH2, SWITCH3, SWITCH4, SWITCH5 ' this was  iInst (11-14), now 4,5,6,7
            Dim cardId As Integer
            Select Case iInst
              Case 4: cardId = 2  ' switch 2
              Case 5: cardId = 3  ' switch 3
              Case 6: cardId = 4  ' switch 4
              Case 7: cardId = 5  ' switch 5
            End Select
            If Not SwitchCardOK(cardId) Then
              Echo sInstrumentDescription$(iInst) & " identification FAILED"
              iInitSwitching = False
              iInstrumentInitialized(SWITCH1) = False
            End If
    End Select
    WriteSW "CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001"

End Function
'

Function iInitDigital(bReset As Boolean) As Integer
    'DESCRIPTION:
    '   This Routine checks to see if the Digital Test System (DTS) is available and
    '   configured properly.
    'RETURNS:
    '   True if the DTS initializes and is properly cofigured
    'MODIFIES:
    '   nInstrumentHandle&(DIGITAL)
    
    Dim InitStatus&, viSession&, cardCount&, channelCount&, cardIdx&, cardId&
    Dim boardCount&, chanCount&, chassis&, slot&
    Dim Items%
    Dim desc$
    Dim cardName As String * 33
    Dim addlInfo As String * 2049
    Dim iStepCount As Integer           'Increments the Sub Test ID in the Faut ID
    Dim nReset As Long
    If bReset = True Then nReset = VI_TRUE Else nReset = VI_FALSE
    iInitDigital = True
    iStepCount = 5                      'Initialize Step Counter
       
    InitStatus& = terM9_init("TERM9::#0", VI_FALSE, nReset, viSession)     'Chassis 0
    If (InitStatus& <> VI_SUCCESS) Or (viSession& = VI_NULL) Then
        Echo "INSTRUMENT ERROR - Error locating DIGITAL CRB in VXI primary chassis, slot 4."
        iInitDigital = False
        Exit Function
    Else
        nInstrumentHandle(DIGITAL) = viSession
    End If
    
    InitStatus& = terM9_getSystemInformation(viSession&, lpBuffer$, cardCount&, channelCount&)
'Retrieve M9 System Information
    If InitStatus& <> VI_SUCCESS Then
        Echo "INSTRUMENT ERROR - Error initializing DIGITAL CRB in VXI primary chassis, slot 4."
        iInitDigital = False
        Exit Function
    Else
        desc$ = StripNullCharacters(lpBuffer$)
    End If
'Instrument Identification
    If InStr(desc$, "M9 Digital Test Instrument") = 0 Then
        Echo "INSTRUMENT ERROR - M9 Digital Test Instrument could not be Initialized."
        iInitDigital = False
        Exit Function
    End If
'Card Count
    If cardCount& <> 5 Then
        Echo "INSTRUMENT ERROR - Incorrect number of M910 cards: " & Format$(cardCount&) & " found."
        iInitDigital = False
        Exit Function
    End If
    ' Set the systemwide ground reference source to INTERNAL.
    terM9_setGroundReference viSession&, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL
    terM9_setSystemEnable viSession&, VI_TRUE

End Function

Function iInitDCPS(Optional bReset As Boolean = False) As Integer
    'DESCRIPTION:
    '   This Routine checks to see if the DCPS
    '   are available and configured properly.
    'MODIFIES:
    '   nInstrumentHandle&(PPU)
    'EXAMPLE:
    '   If Not iInitDCPS() Then
    
    Dim Supply%, Try%
    Dim SystErr&
    Dim iCnt As Integer
    Dim Nbr As Long
    
    iInitDCPS = True
    If Not bReset Then
        For Supply% = 10 To 1 Step -1 ' get system handle for all 10 PPUs
            For Try% = 1 To 3   'Try multiple times to get a handle
                SystErr& = aps6062_init(ResourceNameX:="GPIB0::5::" & CStr(Supply%), idQuery:=0, _
                    resetDevice:=0, instrumentHandle:=nSupplyHandle(Supply%))
                If SystErr& = VI_SUCCESS Then Exit For
                Delay 0.2
            Next Try%
            If SystErr& <> VI_SUCCESS Then
                Echo "Could not get instrument handle for GPIB0::5::" & CStr(Supply%)
                iInitDCPS = False
                nSupplyHandle(Supply%) = 0
                iInitDCPS = False
                Exit Function
            End If
            SystErr& = viSetAttribute(nSupplyHandle(Supply%), VI_ATTR_TMO_VALUE, 3000)
        Next Supply%
    End If
    
' getPPU status
    For Supply% = 1 To 10
      
      SendDCPSCommand CLng(Supply), Chr$(&H10 + Supply) & Chr$(&H0) & Chr$(&H0) ' reset
        
      'Read Status
      For Try = 1 To 5
        'Send Status Query
        nSystErr = atxmlDF_viWrite(PsResourceName$(Supply%), 0, Chr$(Supply%) & Chr$(&H44) & Chr$(0), 3, Nbr)
        lpBuffer = ""
        nSystErr& = atxmlDF_viRead(PsResourceName$(Supply%), 0, lpBuffer, 255&, Nbr&)
        If Asc(Left(lpBuffer, 1)) <> 0 Then Exit For
      Next Try

      'If MODE byte is zero, supply module is absent or dead
      If Asc(Left(lpBuffer, 1)) <> 0 Then
          'Identification PASSED
      Else            'Identification FAILED
          Echo sInstrumentDescription$(PPU) & " DC" & Supply% & " identification "
          Echo "    Identification Failure for Supply " & CStr(Supply%) & "."
          nSupplyHandle&(Supply%) = 0
      End If
    Next Supply%

            
End Function

Public Sub SendDCPSCommand(nSupply As Long, Command$)
'**********************************************************
'* ManTech Test Systems Software Sub                        *
'************************************************************
'* Nomenclature   : APS 6062 UUT Power Supplies             *
'* Written By     : David W. Hartley                        *
'*    DESCRIPTION:                                          *
'*     Send a command string to the power supplies          *
'*    EXAMPLE:                                              *
'*      SendDCPSCommand (3, Command$                        *
'*    PARAMTERS:                                            *
'*     slot%     = The slot number of the supply where to   *
'*                 send the command                         *
'*     Command$  = The command string to send to the supply *
'************************************************************

  Dim ErrorStatus&
  Dim Nbr As Long
    
   If Not bSimulation Then
        If Len(Command$) <> 3 Then Exit Sub
        ErrorStatus& = atxmlDF_viWrite(PsResourceName$(nSupply), 0, Command$, CLng(Len(Command$)), Nbr)
        Delay 0.5
        If ErrorStatus <> 0 Then
            
            Echo "DCPS PROGRAMMING ERROR:" & nSupply & " Command: " & Command
        End If
    End If
End Sub

Function ReadDCPSCommand(SupplyX&, ReadBuffer$) As Long

  Dim ErrStatus&
  Dim Nbr&
  Dim S$, i%
  
  ReadBuffer$ = space$(255)
  ErrStatus& = atxmlDF_viRead(PsResourceName$(SupplyX), 0, ReadBuffer, 255&, Nbr&)
 
  'the power supply should always return 5 bytes for a status read command
  If Nbr& = 5 Then
    ReadBuffer$ = Left$(ReadBuffer$, Nbr&)
  Else
    S$ = ""
    For i = 1 To Nbr&
      S$ = S$ + "Byte" + CStr(i) + " =" + Str(Asc(Mid$(ReadBuffer$, i, 1))) + vbCrLf
    Next i
    
    Echo "HW error, Freedom Status Byte count should always be 5,  count =" + Str$(Nbr&) + vbCrLf + "Read Status=" & S$ + vbCrLf + "Reading status again"
    ReadBuffer$ = space$(255)
    ErrStatus& = atxmlDF_viRead(PsResourceName$(SupplyX), 0, ReadBuffer, 255&, Nbr&)
    If Nbr& = 5 Then
       ReadBuffer$ = Left$(ReadBuffer$, Nbr&)
    End If
  End If
  ReadDCPSCommand = ErrStatus
  Delay 0.5
  
End Function

Public Sub SupplyReset(nSupply As Long)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This Module resets a power supply                    *
'*    EXAMPLE:                                              *
'*     Reset 4                                              *
'*    PARAMETERS:                                           *
'*     nSupply    =  The supply number of which to reset    *
'************************************************************

Dim B1%
Dim B2%
Dim B3%

    'Send "Open All Relays" Command
    B1% = 32 + nSupply
    B2% = &HA0
    B3% = &H0
    SendDCPSCommand nSupply, Chr$(B1%) & Chr$(B2%) & Chr$(B3%)
    Delay 0.3
    'Send "Reset" Command
    B1% = 16 + nSupply
    B2% = 0
    B3% = 0
    SendDCPSCommand nSupply, Chr$(B1%) & Chr$(B2%) & Chr$(B3%)

End Sub

Sub CommandSetOptions(nSupply As Long, OpenRelay%, SetMaster%, SenseLocal%, ConstantVoltage%)
'************************************************************
'* Written By     : David W. Hartley                        *
'*    DESCRIPTION:                                          *
'*     This Module sets the optional settings of a power    *
'*     supply                                               *
'*    EXAMPLE:                                              *
'*     CommandSetOptions 4, False, True, False, False       *
'*    PARAMETERS:                                           *
'*     Supply%    =  The supply number of which to set      *
'*     OpenRelay% =  Set to False to close the output relay *
'*     SetMaster% =  Set to True to set the supply to master*
'*                   mode                                   *
'*     SenseLocal%=  Set to False to remote sense           *
'*     ConstantVoltage% = Set to True for constant Voltage  *
'*                   mode, and False for constant current   *
'*                   mode.                                  *
'************************************************************
 
Dim B1%
Dim B2%
Dim B3%
    
    B1% = 32 + nSupply
    
    'Note: If a +1 is passed do nothing
    Select Case OpenRelay%
        Case True  '(-1)
            B2% = B2% + 32 'Enable / Relay Open
        Case False '( 0)
            B2% = B2% + 32 'Enable
            B2% = B2% + 16 'Relay Closed
    End Select
    
    Select Case SetMaster%
        Case True  '(-1)
            B2% = B2% + 8 'Enable /Set as Master
        Case False '( 0)
            B2% = B2% + 4 'Set as Slave
            B2% = B2% + 8 'Enable
    End Select
    
    Select Case SenseLocal%
        Case True  '(-1)
            B2% = B2% + 2 'Enable / Sense Local
        Case False '( 0)
            B2% = B2% + 2 'Enable
            B2% = B2% + 1 'Sense Remote
    End Select
    
    Select Case ConstantVoltage%
      Case True  '(-1)
            B2% = B2% + 128
            B3% = B3% + 32 'Enable / Current Limiting(Protection)
       Case False '( 0)
            B3% = B3% + 16 'Constant Current
            B2% = B2% + 128
            B3% = B3% + 32 'Enable
    End Select
    
    'Send Command
    SendDCPSCommand nSupply, Chr$(B1%) & Chr$(B2%) & Chr$(B3%)
    
End Sub

