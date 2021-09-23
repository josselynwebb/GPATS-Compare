Attribute VB_Name = "EIP114X"
' Consts for EIP114X
' units:
Public Const cHZ = 0
Public Const cKHZ = 1
Public Const cMHZ = 2
Public Const cGHZ = 3

' Attenuator
Public Const cATTEN_AUTO = 0

Declare Function eip114x_init& Lib "EIP114X.dll" (laddr$, platform%, id_query%, _
                                                    Instrument_ID&, ByRef Model_Number&)
                                                    
Declare Function eip114x_freq_power& Lib "EIP114X.dll" (instrumentID&, ByVal frequency As Double, _
                                                    ByVal unit As Long, ByVal powerdBm As Double, _
                                                    ByVal attenuationdB As Long)

Declare Function eip114x_reset& Lib "EIP114X.dll" (instrumentID&)

Declare Function eip114x_close& Lib "EIP114X.dll" (instrumentID&)

Declare Function eip114x_error_query& Lib "EIP114X.dll" (instrumentID&, errorNumber&, errorMessage$)

