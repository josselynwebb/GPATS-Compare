Attribute VB_Name = "RFPm_Defs"

Public gRFPm As New RFPm_if

Public gRetVal As Long

'Operation Modes
Public Const cEXECUTION = 0
Public Const cSIMULATION = 1

'These are the Measurment types
Public Const cRELATIVE = 1
Public Const cABSOLUTE = 0

'These are the consts for the range modes
Public Const cRANGE_MODE_AUTO = 0
Public Const cRANGE_MODE_UPPER = 1
Public Const cRANGE_MODE_LOWER = 2

'Consts for set the ON/OFF state (setPwrMeterOn_Off())
Public Const cPM_STATE_ON = 1
Public Const cPM_STATE_OFF = 0

'Error string
Public Const CRFMS_ERROR = "RMFS Error"

'Error constants
Public Const cTIMEOUTERROR = 10

'Calibration and zero states
Public Const cZERO_CAL_CAL = 0
Public Const cZERO_CAL_ZERO = 1
Public Const cZERO_CAL_ALL = 2
Public Const cZERO_CAL_NONE = 3
