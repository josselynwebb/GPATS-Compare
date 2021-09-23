Attribute VB_Name = "RFMa_Defs"
Public gRFMa As New RFMa_if
Public gRetVal As Long

'Operation Modes
Public Const cEXECUTION = 0
Public Const cSIMULATION = 1

'Trigger Sources
Public Const cIMMEDIATE = 0
Public Const cINPUT = 1
Public Const cEXTERNAL = 2
Public Const cTTL = 3

Public Const cAUTO_MOD_FREQ_OFF = 0
Public Const cAUTO_MOD_FREQ_ON = 1

'RBW OPTIONS
Public Const cMANUAL_MODE = 0
Public Const cAUTO_MODE = 1

'FREQUENCY WINDOWS
Public Const cWIN_NONE = 0
Public Const cWIN_BLACKMAN_HARRIS = 1
Public Const cWIN_HANN = 2
Public Const cWIN_FLAT_TOP = 3

'YIG PATH
Public Const cYIG_PATH_DISABLED = 1
Public Const cYIG_PATH_ENABLED = 0

'POST DETECTION FILTERS
Public Const PST_DETECT_FILTER_3K = 0
Public Const PST_DETECT_FILTER_15K = 1
Public Const PST_DETECT_FILTER_20K = 2
Public Const PST_DETECT_FILTER_50K = 3
Public Const PST_DETECT_FILTER_200K = 4
Public Const PST_DETECT_FILTER_OFF = 5

'CARRIER CORRECTION
Public Const cCARRIER_CORR_OFF = 0
Public Const cCARRIER_CORR_PHASE = 1
Public Const cCARRIER_CORR_FREQ = 2
Public Const cCARRIER_CORR_PHASE_FREQ = 3

'MEASURED SIGNAL TYPES
Public Const cSIG_TYPE_AC = 0
Public Const CSIG_TYPE_AM = 1
Public Const CSIG_TYPE_FM = 2
Public Const CSIG_TYPE_PM = 3
Public Const CSIG_TYPE_PAM = 4

'MEASUREMENT MODES
Public Const cMEASMODE_CATPURE = 0
Public Const cMEASMODE_POWER = 1
Public Const cMEASMODE_NON_HARMONICS_ABS = 2
Public Const cMEASMODE_HARM_POWER_ABS = 3
Public Const cMEASMODE_HARM_VOLTAGE_ABS = 4
Public Const cMEASMODE_HARM_DIST = 5
Public Const cMEASMODE_CAR_FREQ = 6
Public Const cMEASMODE_CAR_APML = 7
Public Const cMEASMODE_MOD_FREQ = 8
Public Const cMEASMODE_MOD_AMPL = 9
Public Const cMEASMODE_MOD_DIST = 10
Public Const cMEASMODE_NOISE = 11
Public Const cMEASMODE_MOD_AMPL_PP = 12
Public Const cMEASMODE_MOD_AMPL_PN = 13
Public Const cMEASMODE_FREQ_DEV_AVG = 14
Public Const cMEASMODE_FREQ_DEV_PP = 15
Public Const cMEASMODE_FREQ_DEV_PN = 16
Public Const cMEASMODE_PHASE_DEV_AVG = 17
Public Const cMEASMODE_PHASE_DEV_PP = 18
Public Const cMEASMODE_PHASE_DEV_PN = 19
Public Const cMEASMODE_PERIOD = 20
Public Const cMEASMODE_PULSE_WIDTH = 21
Public Const cMEASMODE_PRF = 22
Public Const cMEASMODE_BANDWIDTH = 23
Public Const cMEASMODE_AM_INDEX = 24
Public Const cMEASMODE_HARM_VOLTAGE_REL = 25
Public Const cMEASMODE_NON_HARMONICS_REL = 26
Public Const cMEASMODE_HARM_POWER_REL = 27
Public Const cHARMONICS = 28

'PLOT
Public Const cMEMORY_1 = "MEMORY_1"
      
