VERSION 5.00
Begin VB.Form frmSFPRFMA 
   ClientHeight    =   5070
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6405
   LinkTopic       =   "Form1"
   ScaleHeight     =   5070
   ScaleWidth      =   6405
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   375
      Left            =   4800
      TabIndex        =   30
      Top             =   2640
      Width           =   1095
   End
   Begin VB.Frame Frame3 
      Caption         =   "Mode"
      Height          =   735
      Left            =   4560
      TabIndex        =   27
      Top             =   240
      Width           =   1695
      Begin VB.OptionButton OptMode 
         Caption         =   "Simulation"
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   29
         Top             =   440
         Width           =   1455
      End
      Begin VB.OptionButton OptMode 
         Caption         =   "Execution"
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   28
         Top             =   210
         Width           =   1455
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Error Information"
      Height          =   1575
      Left            =   240
      TabIndex        =   19
      Top             =   3240
      Width           =   5895
      Begin VB.CommandButton btnClrErrors 
         Caption         =   "CLR Errors"
         Height          =   375
         Left            =   4440
         TabIndex        =   26
         Top             =   240
         Width           =   1215
      End
      Begin VB.TextBox txtErrorSeverity 
         Height          =   285
         Left            =   3360
         TabIndex        =   23
         Top             =   340
         Width           =   735
      End
      Begin VB.TextBox txtErrorCode 
         Height          =   285
         Left            =   960
         TabIndex        =   22
         Top             =   340
         Width           =   1215
      End
      Begin VB.TextBox txtErrorDescr 
         Height          =   285
         Left            =   120
         TabIndex        =   21
         Text            =   "No Error"
         Top             =   720
         Width           =   5655
      End
      Begin VB.TextBox txtMoreErrorInfo 
         Height          =   285
         Left            =   120
         TabIndex        =   20
         Text            =   "No Error"
         Top             =   1080
         Width           =   5655
      End
      Begin VB.Label Label11 
         Caption         =   "Error Severity"
         Height          =   255
         Left            =   2280
         TabIndex        =   25
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label10 
         Caption         =   "Error Code"
         Height          =   255
         Left            =   120
         TabIndex        =   24
         Top             =   360
         Width           =   855
      End
   End
   Begin VB.TextBox txtAttenuation 
      Height          =   285
      Left            =   2640
      TabIndex        =   12
      Top             =   2640
      Width           =   1095
   End
   Begin VB.TextBox txtSpan 
      Height          =   285
      Left            =   2640
      TabIndex        =   11
      Top             =   2040
      Width           =   1095
   End
   Begin VB.TextBox txtCenterFreq 
      Height          =   285
      Left            =   2640
      TabIndex        =   10
      Top             =   1440
      Width           =   1455
   End
   Begin VB.ComboBox cmbMeasUnits 
      Height          =   315
      Left            =   240
      TabIndex        =   6
      Top             =   2640
      Width           =   1695
   End
   Begin VB.ComboBox cmbMeasSigType 
      Height          =   315
      Left            =   240
      TabIndex        =   5
      Top             =   2040
      Width           =   1695
   End
   Begin VB.ComboBox cmbMeasMode 
      Height          =   315
      Left            =   240
      TabIndex        =   4
      Top             =   1440
      Width           =   2175
   End
   Begin VB.Frame Frame1 
      Caption         =   "Measure"
      Height          =   735
      Left            =   240
      TabIndex        =   0
      Top             =   240
      Width           =   4095
      Begin VB.CommandButton btnMeasure 
         Caption         =   "Measure"
         Height          =   375
         Left            =   2880
         TabIndex        =   3
         Top             =   240
         Width           =   1095
      End
      Begin VB.TextBox txtMeasResult 
         Height          =   285
         Left            =   120
         TabIndex        =   1
         Text            =   "0.0"
         Top             =   270
         Width           =   2055
      End
      Begin VB.Label lblMeasRUnits 
         Caption         =   "dBm"
         Height          =   255
         Left            =   2280
         TabIndex        =   2
         Top             =   300
         Width           =   495
      End
   End
   Begin VB.Label Label9 
      Caption         =   "dB"
      Height          =   255
      Left            =   3840
      TabIndex        =   18
      Top             =   2670
      Width           =   255
   End
   Begin VB.Label lblSpanUnits 
      Caption         =   "MHz"
      Height          =   255
      Left            =   3840
      TabIndex        =   17
      Top             =   2070
      Width           =   375
   End
   Begin VB.Label Label7 
      Caption         =   "ATTN"
      Height          =   255
      Left            =   2640
      TabIndex        =   16
      Top             =   2400
      Width           =   855
   End
   Begin VB.Label lblCenterFreqUnits 
      Caption         =   "MHz"
      Height          =   255
      Left            =   4200
      TabIndex        =   15
      Top             =   1470
      Width           =   495
   End
   Begin VB.Label Label5 
      Caption         =   "Span"
      Height          =   255
      Left            =   2640
      TabIndex        =   14
      Top             =   1800
      Width           =   735
   End
   Begin VB.Label Label4 
      Caption         =   "Center Frequency"
      Height          =   255
      Left            =   2640
      TabIndex        =   13
      Top             =   1200
      Width           =   1335
   End
   Begin VB.Label Label3 
      Caption         =   "Measurement Units"
      Height          =   255
      Left            =   240
      TabIndex        =   9
      Top             =   2400
      Width           =   1455
   End
   Begin VB.Label Label2 
      Caption         =   "Measure Signal Type"
      Height          =   255
      Left            =   240
      TabIndex        =   8
      Top             =   1800
      Width           =   1575
   End
   Begin VB.Label Label1 
      Caption         =   "Measurement Mode"
      Height          =   255
      Left            =   240
      TabIndex        =   7
      Top             =   1200
      Width           =   1455
   End
End
Attribute VB_Name = "frmSFPRFMA"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim gRFMa As New RFMa_if
Dim gRetVal As Long
Dim gErrorCode As Long
Dim gErrorSeverity As Long
Dim gErrorDescr As String
Dim gMoreErrorInfo As String

Const cEXEMODE = 0
Const cSIMMODE = 1

Private Sub Command1_Click()
    
    Dim MA_MeasResult As Double
    Dim FrequencyList(8) As String
    FrequencyList$(1) = "100"
    FrequencyList$(2) = "1000"
    FrequencyList$(3) = "5000"
    FrequencyList$(4) = "10000"
    FrequencyList$(5) = "14000"
    FrequencyList$(6) = "16000"
    FrequencyList$(7) = "18000"
    
'    WriteMsg RFSYN, "OUTP On"
'    WriteMsg RFSYN, "pow:0 dbm"
    gRetVal = gRFMa.open(0): checkError
    gRetVal = gRFMa.setCenterFreq(100, "MHZ"): checkError
    gRetVal = gRFMa.setSpan(10, "MHZ"): checkError
    gRetVal = gRFMa.setAttenuator(10): checkError
    gRetVal = gRFMa.setMeasSignalType(0): checkError
    gRetVal = gRFMa.setMeasureUnits("MHZ"): checkError
  
    For FreqCount% = 1 To 7
'      WriteMsg2 hndlRFSYN&, "FREQ " & FrequencyList$(FreqCount%) & "MHZ"
      gRetVal = gRFMa.setCenterFreq(FrequencyList$(FreqCount%), "MHZ"): checkError
      gRetVal = gRFMa.setMeasureMode(0): checkError
      ' Measure Mode = CAPTURE
      gRetVal = gRFMa.getMeasurement(0, "MHZ"): checkError
      ' Measure Mode = CAR_FREQ
      gRetVal = gRFMa.setMeasureMode(6): checkError
      gRetVal = gRFMa.getMeasurement(MA_MeasResult, "MHZ"): checkError
      'dMeasurement = MA_MeasResult
    Next FreqCount%

End Sub

Private Sub Form_Load()

' load the combo boxes
cmbMeasMode.AddItem ("FREQ")
cmbMeasMode.AddItem ("PEAK_POWER")
cmbMeasMode.AddItem ("WAVE_FORM_CAPTURE")
cmbMeasMode.AddItem ("DISTORTION")
cmbMeasMode.AddItem ("NON_HARM_ABS")
cmbMeasMode.AddItem ("NON_HARM_REL")
cmbMeasMode.AddItem ("HARMONICS")
cmbMeasMode.AddItem ("BANDWIDTH")

cmbMeasSigType.AddItem ("AC Signal")
cmbMeasUnits.AddItem ("dBm")
cmbMeasUnits.AddItem ("dB")
cmbMeasUnits.AddItem ("W")
cmbMeasUnits.AddItem ("mW")
cmbMeasUnits.AddItem ("Hz")
cmbMeasUnits.AddItem ("KHz")
cmbMeasUnits.AddItem ("MHz")
cmbMeasUnits.AddItem ("GHz")

' load some defaults
cmbMeasMode = "PEAK_POWER"
cmbMeasSigType = "AC Signal"
cmbMeasUnits = "dBm"
cmbMeasUnits = "dB"
cmbMeasUnits = "pct"
' request execution mode, if no hardware is
'  present Simulation mode is entered
enterExecutionMode
'enterSimulationMode

' get instrument settings from the server
loadInstrSettings

End Sub

'**************************************
'Open RFCt session in Execution mode
'**************************************
Sub enterExecutionMode()
    'Close the session--RFMS ignores this if a session is not open
    gRFMa.Close
    gRetVal = gRFMa.open(0)
    If gRetVal <> 0 Then
        checkError
        enterSimulationMode
        frmSFPRFMA.Caption = "RFMS RF Measurement Analyzer SFP:  SIMULATION MODE"
        OptMode(cSIMMODE).Value = True
    Else
        frmSFPRFMA.Caption = "RFMS RF Measurement Analyzer SFP: Execution Mode"
        OptMode(cEXEMODE).Value = True
    End If
    
End Sub

'**************************************
'Open RFCt session in Simulation mode
'**************************************
Private Sub enterSimulationMode()
    'Close the session--RFMS ignores this if a session is not open
    gRFMa.Close
    gRetVal = gRFMa.open(1)
    frmSFPRFMA.Caption = "RFMS RF Measurement Analyzer SFP:  SIMULATION MODE"
    checkError
    OptMode(cSIMMODE).Value = True
End Sub

'**************************************
' Perform a measurement
'**************************************
Private Sub btnMeasure_Click()
    Dim MeasResult As Variant
    Dim units As String
    Dim NumOfFFTSamples As Long
    Dim FFTArray() As Double
     
    ' Let VB know Variant type is double
    MeasResult = CDbl(0)
    
    setInstrSettings
    
    ' Tell the server the signal type
    If (measSigType = "AC Signal") Then gRetVal = gRFMa.setMeasSignalType(0)
    checkError
    
    ' Tell the server which units of measure we want to use
    gRetVal = gRFMa.setMeasureUnits(cmbMeasUnits.Text)
    checkError
    
    ' Tell the server the Measurement mode
    If (cmbMeasMode.Text = "FREQ") Then gRetVal = gRFMa.setMeasureMode(cMEASMODE_CAR_FREQ)
            
    If (cmbMeasMode.Text = "PEAK_POWER") Then gRetVal = gRFMa.setMeasureMode(cMEASMODE_CAR_APML)
    
    If (cmbMeasMode.Text = "DISTORTION") Then gRetVal = gRFMa.setMeasureMode(cMEASMODE_HARM_DIST)
    
    If (cmbMeasMode.Text = "NON_HARM_ABS") Then gRetVal = gRFMa.setMeasureMode(cMEASMODE_NON_HARMONICS_ABS)
    
    If (cmbMeasMode.Text = "NON_HARM_REL") Then gRetVal = gRFMa.setMeasureMode(cMEASMODE_NON_HARMONICS_REL)
    
    If (cmbMeasMode.Text = "HARMONICS") Then gRetVal = gRFMa.setMeasureMode(cHARMONICS)
    
    If (cmbMeasMode.Text = "BANDWIDTH") Then
        gRetVal = gRFMa.setFreqWindow(0)
        gRetVal = gRFMa.setMeasureMode(cMEASMODE_BANDWIDTH)
    End If
    
    checkError
    ' Tell the server which units of measure we want to use
    gRetVal = gRFMa.setMeasureUnits(cmbMeasUnits.Text)
    checkError
    
    If (cmbMeasMode.Text = "WAVE_FORM_CAPTURE") Then
        gRetVal = gRFMa.setMeasureMode(0)
        checkError
        gRetVal = gRFMa.getMeasurement(MeasResult, units)
        checkError
        
        ' if you want to copy the data from the variant here is how it is done
        gRetVal = gRFMa.getFFTSmplLen(NumOfFFTSamples - 1)
        checkError
        ReDim FFTArray(NumOfFFTSamples)
        For i = 0 To NumOfFFTSamples
            FFTArray(i) = MeasResult(i)
        Next i
        
        lblMeasRUnits.Caption = "dBm"
        txtMeasResult.Text = 0#
        Exit Sub
    End If
        
    gRetVal = gRFMa.getMeasurement(MeasResult, units)
    checkError
        
    txtMeasResult.Text = MeasResult
    lblMeasRUnits.Caption = units
            
End Sub

'**************************************
' Get Instr Settings
'**************************************
Private Sub loadInstrSettings()
    Dim tmpd As Double
    Dim tmpl As Long
    Dim units As String
    
    ' Center Frequency
    gRetVal = gRFMa.getCenterFreq(tmpd, units)
    checkError
    txtCenterFreq.Text = tmpd
    lblCenterFreqUnits.Caption = units
        
    ' Span
    gRetVal = gRFMa.getSpan(tmpd, units)
    checkError
    txtSpan.Text = tmpd
    lblSpanUnits = units
    
    ' Attenuator
    gRetVal = gRFMa.getAttenuator(tmpl)
    txtAttenuation.Text = tmpl
    checkError
        
End Sub

'**************************************
' Get Instr Settings
'**************************************
Private Sub setInstrSettings()
    gRetVal = gRFMa.setCenterFreq(CDbl(txtCenterFreq.Text), lblCenterFreqUnits.Caption)
    checkError
    gRetVal = gRFMa.setSpan(CDbl(txtSpan), lblSpanUnits.Caption)
    checkError
    gRetVal = gRFMa.setAttenuator(CLng(txtAttenuation))
    checkError
End Sub

'**************************************
'Test for errors
'**************************************
Sub checkError()
    Dim Message As String
    If gRetVal <> 0 Then
        gRFMa.getError gErrorCode, gErrorSeverity, gErrorDescr, 256, gMoreErrorInfo, 256
        Message = "Error:  " & gErrorCode & " --" & gErrorDescr & ". " & gMoreErrorInfo
        txtErrorCode.Text = gErrorCode
        txtErrorSeverity.Text = gErrorSeverity
        txtErrorDescr.Text = gErrorDescr
        txtMoreErrorInfo.Text = gMoreErrorInfo
    End If
    
End Sub

'**************************************
'Clear errors
'**************************************
Private Sub btnClrErrors_Click()
    txtErrorCode.Text = ""
    txtErrorSeverity.Text = ""
    txtErrorDescr.Text = ""
    txtMoreErrorInfo.Text = ""
End Sub

Private Sub Harmonics()
    Dim MeasResult As Variant
    Dim units As String
    MeasResult = CDbl(0)
    gRetVal = gRFMa.setCenterFreq(3, "GHz")
    checkError
    gRetVal = gRFMa.setAttenuator(10)
    checkError
    gRetVal = gRFMa.setSpan(50, "KHz")
    checkError
    gRetVal = gRFMa.setMeasureMode(4)
    checkError
    gRetVal = gRFMa.setHarmonic(1)
    checkError
    gRetVal = gRFMa.setMeasureUnits("dBm")
    checkError
    gRetVal = gRFMa.getMeasurement(MeasResult, "dBm")
    checkError
    txtMeasResult = MeasResult
End Sub

