VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'create the RFMa_if object
Dim gRFMA As New RFMa_if
Dim units As String
Dim gRetVal As Long
Dim MeasResult As Variant
Dim MeasResultStr As String

Private Sub Form_Load()

' open a session in execution mode--or pass 1 for simulation mode
gRetVal = gRFMA.open(0)
    checkError

' setup the instrument for the measurement
gRetVal = gRFMA.setCenterFreq(3, "GHz")
    checkError
gRetVal = gRFMA.setSpan(1, "mhz")
    checkError
gRetVal = gRFMA.setAttenuator(10)
    checkError

' Tell the server the signal type is AC
gRetVal = gRFMA.setMeasSignalType(0)
     checkError
    
' Tell the server which units of measure we want to use
gRetVal = gRFMA.setMeasureUnits("MHz")
    checkError
    
' Tell the server the Measurement mode (Frequency)
gRetVal = gRFMA.setMeasureMode(1)
     checkError

' Tell VB the variant is a type double
MeasResult = CDbl(0)

' get the measured result in this case it is a Frequency
gRetVal = gRFMA.getMeasurement(MeasResult, units)
    checkError
 
MeasResultStr = " Frequency = " & MeasResult & units
MsgBox (MeasResultStr)
End Sub

Sub checkError()
Dim ErrorCode As Long
Dim ErrorSeverity As Long
Dim ErrorDescr As String
Dim MoreErrorInfo As String
Dim Message As String

If gRetVal <> 0 Then
gRFMA.getError ErrorCode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256
Message = "Error:  " & ErrorCode & " --" & ErrorDescr & ". " & MoreErrorInfo
    MsgBox (Message)
End If
End Sub

