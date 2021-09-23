VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cRFGEN"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

Private instrumentHandle&
Private sTETSIni As String
Private sPlcDatPath As String
Private lpBuffer As String * 256

Public Sub SendMsg(sMsg As String)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "RFGEN"
    frmMain.txtCommand.Text = "SendMsg"
    WriteMsg RFSYN, sMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
End Sub

Public Function sReadMsg(sMsg As String) As String
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "RFGEN"
    frmMain.txtCommand.Text = "ReadMsg"
    ReadMsg RFSYN, sReadMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
End Function

Public Sub InitInstrument()
    'DESCRIPTION:
    '   This function initializes the RF Signal generator
    
    Dim ErrorStatus As Long
    Dim lSystErr As Long
    Dim lBytesRead As Long
    
    If bSimulation Then
        Exit Sub
    End If
        
    If ErrorStatus& Then
        Echo "CANNOT INITIALIZE RF STIM"
    Else
        WriteMsg RFSYN, "*CLS"
        WriteMsg RFSYN, ":STATUS:PRESET"
        WriteMsg RFSYN, ":OUTP OFF"
    End If

    'Get ViperT.INI path
    lpBuffer$ = ""
    lSystErr = GetWindowsDirectory(lpBuffer$, ByVal 80)
    sTETSIni = StripNullCharacters(lpBuffer$) & "\" & "vipert.ini"
    
    'Get PLC data file path
    lpBuffer$ = ""
    lBytesRead = GetPrivateProfileString("File Locations", "PLC_DATA", "C:\", lpBuffer, Len(lpBuffer), sTETSIni)
    sPlcDatPath = Left$(lpBuffer, lBytesRead)

End Sub

Public Function RFStimGenFixed(dFreq As Double, dPower!, _
                          Optional sOutput As String = "ON", _
                          Optional sPowerCorrection As String = "NONE", _
                          Optional sMODE_FM As String = "OFF", _
                          Optional sMODE_AM As String = "OFF", _
                          Optional sMODE_PM As String = "OFF") As Boolean
    'DESCRIPTION
    '   This function generates RF stimulus for Gigatronics RF synthesizer.
    'PARAMETERS
    '   dFreq:      10 MHz < dFreq < 8.5 GHz
    '   dPower:     -90 dBm < dPower < 10 dBm
    '   sOutput(opt):    "ON" or "OFF"
    '       allows user to set dFreq and dPower without turning on the output
    '   sPowerCorrection(opt):      "NONE", "RECEIVER", or "SAIF"
    '       retrieves power correction table from ViperT.ini and apply corected power and freq
    '   sMODE_FM(opt):   "ON" activates FM mode, else "OFF"
    '   sMODE_AM(opt):   "ON" activates AM mode, else "OFF"
    '   sMODE_PM(opt):   "ON" activates PM mode, else "OFF"
    'RETURNS
    '   TRUE if successful
    '   FALSE if not
    
    'declare variable
    Dim nErr As Long
    
    'convert string data to upper case
    sOutput = UCase(sOutput)
    sPowerCorrection = UCase(sPowerCorrection)
    sMODE_FM = UCase(sMODE_FM)
    sMODE_AM = UCase(sMODE_AM)
    sMODE_PM = UCase(sMODE_PM)
    
    'check freq range
    If (dFreq < 10000000# Or dFreq > 8500000000#) Then
        Echo "FREQ OUT OF RANGE"
        RFStimGenFixed = False
        Exit Function
    End If
    
    'check power range
    If (dPower < -90 Or dPower > 10) Then
        Echo "POWER OUT OF RANGE"
        RFStimGenFixed = False
        Exit Function
    End If
    
    'check output parameter
    If (sOutput <> "ON" And sOutput <> "OFF") Then
        Echo "INVALID OUTPUT ON/OFF PARAMETER"
        RFStimGenFixed = False
        Exit Function
    End If
    
    'check RF freq mode
    If (sMODE_FM <> "OFF" And sMODE_FM <> "ON") Then
        Echo "INVALID FM MODE"
        RFStimGenFixed = False
        Exit Function
    End If
    If (sMODE_AM <> "OFF" And sMODE_AM <> "ON") Then
        Echo "INVALID AM MODE"
        RFStimGenFixed = False
        Exit Function
    End If
    If (sMODE_PM <> "OFF" And sMODE_PM <> "ON") Then
        Echo "INVALID PM MODE"
        RFStimGenFixed = False
        Exit Function
    End If
    
    'check power correction input parameter
    If Not (sPowerCorrection = "RECEIVER" Or sPowerCorrection = "SAIF") Then
        sPowerCorrection = "NONE"
    End If
    
    If bSimulation Then
        RFStimGenFixed = True
        Exit Function
    End If
    
    'init RF synthesizer
    InitInstrument
    
    'set power correction table
    If (nErr <> 0) Then
        Echo "UNABLE TO SET POWER CORRECTION"
        RFStimGenFixed = False
        Exit Function
    End If
    
    'select RF signal type
    If (sMODE_FM = "OFF" And sMODE_AM = "OFF" And sMODE_PM = "OFF") Then
        WriteMsg RFSYN, ":FM:STAT OFF;:AM:STAT OFF;:PULM:STAT OFF"
    Else
        WriteMsg RFSYN, ":FM:STAT " & sMODE_FM
        WriteMsg RFSYN, ":AM:STAT " & sMODE_AM
        WriteMsg RFSYN, ":PULM:STAT " & sMODE_PM
    End If
    
    'set freq and power
    If (nErr <> 0) Then
        Echo "UNABLE TO SET FREQ and POWER"
        RFStimGenFixed = False
        Exit Function
    End If
    
    'turn on/off RF output
    WriteMsg RFSYN, ":OUTP " & sOutput
        
    RFStimGenFixed = True
    
End Function

Public Sub SetRFPower(dPower As Double)
    'DESCRIPTION
    '   This sub will allow the user to specify Power level of RF STIM independently
    'PARAMETER
    '   dPower: power level in dBm
    
    Dim sPower As String
    
    'check power range
    If (dPower < -90 Or dPower > 10) Then
        Echo "POWER OUT OF RANGE"
        Exit Sub
    End If
    
    'convert double to string data type
    sPower = CStr(dPower)
    
    If Not bSimulation Then
        'send SCPI command to RF STIM
        WriteMsg RFSYN, ":POW:LEV:IMM:AMPL " & sPower
    End If
    
End Sub

Public Sub SetRFFreq(dFreq As Double)
    'DESCRIPTION
    '   This sub will allow the user to specify frequency of RF STIM independently
    'PARAMETER
    '   dFreq: frequency in Hz
    
    Dim sFreq As String
    
    'check frequency range
    If (dFreq < 10000000# Or dFreq > 8500000000#) Then
        Echo "FREQ OUT OF RANGE"
        Exit Sub
    End If
    
    'convert double to string data type
    sFreq = CStr(dFreq)
    
    If Not bSimulation Then
        'send SCPI command to RF STIM
        WriteMsg RFSYN, ":FREQ:CW " & sFreq
    End If
    
End Sub

Public Sub RFOutputON(Optional bON_OFF As Boolean = True)
    'DESCRIPTION
    '   This sub will turn on/off the RF output of Giga-tronics RF STIM
    'PARAMETER
    '   bON_OFF:  True (default), turns on the output
    '             False, turns off output
    
    If Not bSimulation Then
        If bON_OFF Then
            'turn on/off RF output
            WriteMsg RFSYN, ":OUTP ON"
        Else
            WriteMsg RFSYN, ":OUTP OFF"
        End If
    End If
    
End Sub
