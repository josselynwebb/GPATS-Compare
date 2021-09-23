Attribute VB_Name = "Module1"
' Copyright (C) 1998 Racal Instruments
' VXIplug&play Instrument Driver for the ri4152A

'------------------------------------------------------------------------------
' Global Constants

'------------------------------------------------------------------------------

'  Returned error status values
'  RI Common Error numbers start at BFFC0D00

Global Const ri4152a_INSTR_ERROR_NOT_VXI = &HBFFC0D00
' D01 unused
Global Const ri4152a_INSTR_ERROR_NULL_PTR = &HBFFC0D02
Global Const ri4152a_INSTR_ERROR_RESET_FAILED = &HBFFC0D03
Global Const ri4152a_INSTR_ERROR_UNEXPECTED = &HBFFC0D04
Global Const ri4152a_INSTR_ERROR_INV_SESSION = &HBFFC0D05
Global Const ri4152a_INSTR_ERROR_LOOKUP = &HBFFC0D06
Global Const ri4152a_INSTR_ERROR_DETECTED = &HBFFC0D07

' Used by Status System Functions
Global Const ri4152a_USER_ERROR_HANDLER = -1

Global Const ri4152a_QUES_VOLT = 401
Global Const ri4152a_QUES_CURR = 402
Global Const ri4152a_QUES_RES = 410
Global Const ri4152a_QUES_LIM_LO = 412
Global Const ri4152a_QUES_LIM_HI = 413

Global Const ri4152a_ESR_OPC = 601
Global Const ri4152a_ESR_QUERY_ERROR = 603
Global Const ri4152a_ESR_DEVICE_DEPENDENT_ERROR = 604
Global Const ri4152a_ESR_EXECUTION_ERROR = 605
Global Const ri4152a_ESR_COMMAND_ERROR = 606

' Used by Function ri4152a_timeOut
Global Const ri4152a_TIMEOUT_MAX = 2147483647
Global Const ri4152a_TIMEOUT_MIN = 0&


'------------------------------------------------------------------------------
' Function Declarations
'------------------------------------------------------------------------------

' required plug and play functions from VPP-3.1
Declare Function ri4152a_init Lib "ri4152a_32.dll" (ByVal InstrDesc As String, ByVal id_query As Integer, ByVal do_reset As Integer, vi As Long) As Long

Declare Function ri4152a_close Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_reset Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_self_test Lib "ri4152a_32.dll" (ByVal vi As Long, test_result As Integer,test_message As String) As Long

Declare Function ri4152a_error_query Lib "ri4152a_32.dll" (ByVal vi As Long, error_num As Long, error_message As String) As Long

Declare Function ri4152a_error_message Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal error_num As Long, message As String) As Long

Declare Function ri4152a_revision_query Lib "ri4152a_32.dll" (ByVal vi As Long, driver_rev As String,instr_rev As String) As Long

' RI other standard functions

Declare Function ri4152a_dcl Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_errorQueryDetect Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal errorQueryDetect As Integer) As Long

Declare Function ri4152a_errorQueryDetect_Q Lib "ri4152a_32.dll" (ByVal vi As Long, pErrDetect As Integer) As Long

Declare Function ri4152a_opc Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_opc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, opc As Integer) As Long

Declare Function ri4152a_readStatusByte_Q Lib "ri4152a_32.dll" (ByVal vi As Long, statusByte As Integer) As Long

Declare Function ri4152a_statCond_Q Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal happening As Long, pCondition As Integer) As Long

Declare Function ri4152a_statEvenClr Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_statEven_Q Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal happening As Long, pEvent As Integer) As Long

Declare Function ri4152a_timeOut Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal timeOut As Long) As Long

Declare Function ri4152a_timeOut_Q Lib "ri4152a_32.dll" (ByVal vi As Long, pTimeOut As Long) As Long

Declare Function ri4152a_trg Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_wai Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

'------------------------------------------------------------------------------
' Global Constants

'------------------------------------------------------------------------------

' Used by Function ri4152a_calLfr
Global Const ri4152a_CAL_LFR_MAX = 400
Global Const ri4152a_CAL_LFR_MIN = 50
Global Const ri4152a_CAL_LFR_50 = 50
Global Const ri4152a_CAL_LFR_60 = 60
Global Const ri4152a_CAL_LFR_400 = 400

' Used by Function ri4152a_calVal
Global Const ri4152a_CAL_VAL_MAX = 100000000#
Global Const ri4152a_CAL_VAL_MIN = -300#

' Used by Function ri4152a_calcDbRef
Global Const ri4152a_CALC_DB_REF_MAX = 200#
Global Const ri4152a_CALC_DB_REF_MIN = -200#

' Used by Function ri4152a_calcDbRef_Q
'    ri4152a_CALC_DB_REF_MAX
'    ri4152a_CALC_DB_REF_MIN

' Used by Function ri4152a_calcDbmRef
Global Const ri4152a_CALC_DBM_REF_MAX = 8000
Global Const ri4152a_CALC_DBM_REF_MIN = 50
Global Const ri4152a_CALC_DBM_REF_50 = 50#
Global Const ri4152a_CALC_DBM_REF_75 = 75#
Global Const ri4152a_CALC_DBM_REF_93 = 93#
Global Const ri4152a_CALC_DBM_REF_110 = 110#
Global Const ri4152a_CALC_DBM_REF_124 = 124#
Global Const ri4152a_CALC_DBM_REF_125 = 125#
Global Const ri4152a_CALC_DBM_REF_135 = 135#
Global Const ri4152a_CALC_DBM_REF_150 = 150#
Global Const ri4152a_CALC_DBM_REF_250 = 250#
Global Const ri4152a_CALC_DBM_REF_300 = 300#
Global Const ri4152a_CALC_DBM_REF_500 = 500#
Global Const ri4152a_CALC_DBM_REF_600 = 600#
Global Const ri4152a_CALC_DBM_REF_800 = 800#
Global Const ri4152a_CALC_DBM_REF_900 = 900#
Global Const ri4152a_CALC_DBM_REF_1000 = 1000#
Global Const ri4152a_CALC_DBM_REF_1200 = 1200#
Global Const ri4152a_CALC_DBM_REF_8000 = 8000#

' Used by Function ri4152a_calcFunc
Global Const ri4152a_CALC_FUNC_NULL = 0
Global Const ri4152a_CALC_FUNC_AVER = 1
Global Const ri4152a_CALC_FUNC_LIM = 2
Global Const ri4152a_CALC_FUNC_DBM = 3
Global Const ri4152a_CALC_FUNC_DB = 4

' Used by Function ri4152a_calcFunc_Q
'    ri4152a_CALC_FUNC_NULL
'    ri4152a_CALC_FUNC_AVER
'    ri4152a_CALC_FUNC_LIM
'    ri4152a_CALC_FUNC_DBM
'    ri4152a_CALC_FUNC_DB

' Used by Function ri4152a_calcLimLow
Global Const ri4152a_CALC_LIM_MAX = 120000000#
Global Const ri4152a_CALC_LIM_MIN = -120000000#

' Used by Function ri4152a_calcLimUpp
'    ri4152a_CALC_LIM_MAX
'    ri4152a_CALC_LIM_MIN

' Used by Function ri4152a_calcNullOffs
Global Const ri4152a_CALC_NULL_OFFS_MAX = 120000000#
Global Const ri4152a_CALC_NULL_OFFS_MIN = -120000000#

' Used by Function ri4152a_calcNullOffs_Q
'    ri4152a_CALC_NULL_OFFS_MAX
'    ri4152a_CALC_NULL_OFFS_MIN

' Used by Function ri4152a_conf_Q
Global Const ri4152a_CONFQ_FREQ = 0
Global Const ri4152a_CONFQ_PER = 1
Global Const ri4152a_CONFQ_FRES = 2
Global Const ri4152a_CONFQ_RES = 3
Global Const ri4152a_CONFQ_VOLT_AC = 4
Global Const ri4152a_CONFQ_VOLT_DC = 5
Global Const ri4152a_CONFQ_CURR_AC = 6
Global Const ri4152a_CONFQ_CURR_DC = 7
Global Const ri4152a_CONFQ_VOLT_RAT = 8

' Used by Function ri4152a_configure
Global Const ri4152a_CONF_FREQ = 0
Global Const ri4152a_CONF_PER = 1
Global Const ri4152a_CONF_FRES = 2
Global Const ri4152a_CONF_RES = 3
Global Const ri4152a_CONF_VOLT_AC = 4
Global Const ri4152a_CONF_VOLT_DC = 5
Global Const ri4152a_CONF_CURR_AC = 6
Global Const ri4152a_CONF_CURR_DC = 7
Global Const ri4152a_CONF_VOLT_RAT = 8

' Used by Function ri4152a_currAcRang
Global Const ri4152a_CURR_AC_RANG_MAX = 3.03
Global Const ri4152a_CURR_AC_RANG_MIN = 0#
Global Const ri4152a_CURR_AC_RANG_1A = 1#
Global Const ri4152a_CURR_AC_RANG_3A = 3#

' Used by Function ri4152a_currAcRes
Global Const ri4152a_CURR_AC_RES_MAX = 0.0003
Global Const ri4152a_CURR_AC_RES_MIN = 0.000001
Global Const ri4152a_CURR_AC_RES_1_MICRO = 0.000001
Global Const ri4152a_CURR_AC_RES_3_MICRO = 0.000003
Global Const ri4152a_CURR_AC_RES_10_MICRO = 0.00001
Global Const ri4152a_CURR_AC_RES_30_MICRO = 0.00003
Global Const ri4152a_CURR_AC_RES_100_MICRO = 0.0001
Global Const ri4152a_CURR_AC_RES_300_MICRO = 0.0003

' Used by Function ri4152a_currDcAper
Global Const ri4152a_APER_MAX = 2#
Global Const ri4152a_APER_MIN = 0.0004

' Used by Function ri4152a_currDcNplc
Global Const ri4152a_NPLC_MAX = 100#
Global Const ri4152a_NPLC_MIN = 0.02

' Used by Function ri4152a_currDcRang
Global Const ri4152a_CURR_DC_RANG_MAX = 3.03
Global Const ri4152a_CURR_DC_RANG_MIN = 0#
Global Const ri4152a_CURR_DC_RANG_10MA = 0.01
Global Const ri4152a_CURR_DC_RANG_100MA = 0.1
Global Const ri4152a_CURR_DC_RANG_1A = 1#
Global Const ri4152a_CURR_DC_RANG_3A = 3#

' Used by Function ri4152a_currDcRes
Global Const ri4152a_CURR_DC_RES_MAX = 0.0003
Global Const ri4152a_CURR_DC_RES_MIN = 0.000000003
Global Const ri4152a_CURR_DC_RES_3_NANO = 0.000000003
Global Const ri4152a_CURR_DC_RES_10_NANO = 0.00000001
Global Const ri4152a_CURR_DC_RES_30_NANO = 0.00000003
Global Const ri4152a_CURR_DC_RES_100_NANO = 0.0000001
Global Const ri4152a_CURR_DC_RES_300_NANO = 0.0000003
Global Const ri4152a_CURR_DC_RES_900_NANO = 0.0000009
Global Const ri4152a_CURR_DC_RES_1_MICRO = 0.000001
Global Const ri4152a_CURR_DC_RES_3_MICRO = 0.000003
Global Const ri4152a_CURR_DC_RES_9_MICRO = 0.000009
Global Const ri4152a_CURR_DC_RES_10_MICRO = 0.00001
Global Const ri4152a_CURR_DC_RES_30_MICRO = 0.00003
Global Const ri4152a_CURR_DC_RES_100_MICRO = 0.0001
Global Const ri4152a_CURR_DC_RES_300_MICRO = 0.0003

' Used by Function ri4152a_detBand
Global Const ri4152a_DET_BAND_MAX = 300000#
Global Const ri4152a_DET_BAND_MIN = 3#

' Used by Function ri4152a_freqAper
Global Const ri4152a_FREQ_APER_MAX = 1#
Global Const ri4152a_FREQ_APER_MIN = 0.01

' Used by Function ri4152a_freqVoltRang
Global Const ri4152a_VOLT_RANG_MAX = 300#
Global Const ri4152a_VOLT_RANG_MIN = 0#
Global Const ri4152a_VOLT_RANG_100MV = 0.1
Global Const ri4152a_VOLT_RANG_1V = 1#
Global Const ri4152a_VOLT_RANG_10V = 10#
Global Const ri4152a_VOLT_RANG_100V = 100#
Global Const ri4152a_VOLT_RANG_300V = 300#

' Used by Function ri4152a_func
Global Const ri4152a_FUNC_FREQ = 0
Global Const ri4152a_FUNC_PER = 1
Global Const ri4152a_FUNC_FRES = 2
Global Const ri4152a_FUNC_RES = 3
Global Const ri4152a_FUNC_VOLT_AC = 4
Global Const ri4152a_FUNC_VOLT_DC = 5
Global Const ri4152a_FUNC_CURR_AC = 6
Global Const ri4152a_FUNC_CURR_DC = 7
Global Const ri4152a_FUNC_VOLT_DC_RAT = 8

' Used by Function ri4152a_func_Q
'    ri4152a_FUNC_FREQ
'    ri4152a_FUNC_PER
'    ri4152a_FUNC_FRES
'    ri4152a_FUNC_RES
'    ri4152a_FUNC_VOLT_AC
'    ri4152a_FUNC_VOLT_DC
'    ri4152a_FUNC_CURR_AC
'    ri4152a_FUNC_CURR_DC
'    ri4152a_FUNC_VOLT_DC_RAT

' Used by Function ri4152a_measure_Q
'    ri4152a_CONF_FREQ
'    ri4152a_CONF_PER
'    ri4152a_CONF_FRES
'    ri4152a_CONF_RES
'    ri4152a_CONF_VOLT_AC
'    ri4152a_CONF_VOLT_DC
'    ri4152a_CONF_CURR_AC
'    ri4152a_CONF_CURR_DC
'    ri4152a_CONF_VOLT_RAT

' Used by Function ri4152a_outpTtlt_M
Global Const ri4152a_TTLT_LINE_MAX = 7
Global Const ri4152a_TTLT_LINE_MIN = 0

' Used by Function ri4152a_outpTtlt_M_Q
'    ri4152a_TTLT_LINE_MAX
'    ri4152a_TTLT_LINE_MIN

' Used by Function ri4152a_perAper
Global Const ri4152a_PER_APER_MAX = 1#
Global Const ri4152a_PER_APER_MIN = 0.01

' Used by Function ri4152a_perVoltRang
'    ri4152a_VOLT_RANG_MAX
'    ri4152a_VOLT_RANG_MIN
'    ri4152a_VOLT_RANG_100MV
'    ri4152a_VOLT_RANG_1V
'    ri4152a_VOLT_RANG_10V
'    ri4152a_VOLT_RANG_100V
'    ri4152a_VOLT_RANG_300V

' Used by Function ri4152a_resAper
'    ri4152a_APER_MAX
'    ri4152a_APER_MIN

' Used by Function ri4152a_resNplc
'    ri4152a_NPLC_MAX
'    ri4152a_NPLC_MIN

' Used by Function ri4152a_resRang
Global Const ri4152a_RES_RANG_MAX = 120000000#
Global Const ri4152a_RES_RANG_MIN = 0#
Global Const ri4152a_RES_RANG_100 = 100
Global Const ri4152a_RES_RANG_1K = 1000#
Global Const ri4152a_RES_RANG_10K = 10000#
Global Const ri4152a_RES_RANG_100K = 100000#
Global Const ri4152a_RES_RANG_1M = 1000000#
Global Const ri4152a_RES_RANG_10M = 10000000#
Global Const ri4152a_RES_RANG_100M = 100000000#

' Used by Function ri4152a_resRes
Global Const ri4152a_RES_RES_MAX = 10000#
Global Const ri4152a_RES_RES_MIN = 0.00003
Global Const ri4152a_RES_RES_30_MICRO = 0.00003
Global Const ri4152a_RES_RES_100_MICRO = 0.0001
Global Const ri4152a_RES_RES_300_MICRO = 0.0003
Global Const ri4152a_RES_RES_1_MILLI = 0.001
Global Const ri4152a_RES_RES_3_MILLI = 0.003
Global Const ri4152a_RES_RES_10_MILLI = 0.01
Global Const ri4152a_RES_RES_30_MILLI = 0.03
Global Const ri4152a_RES_RES_100_MILLI = 0.1
Global Const ri4152a_RES_RES_300_MILLI = 0.3
Global Const ri4152a_RES_RES_1 = 1#
Global Const ri4152a_RES_RES_3 = 3#
Global Const ri4152a_RES_RES_10 = 10#
Global Const ri4152a_RES_RES_30 = 30#
Global Const ri4152a_RES_RES_100 = 100#
Global Const ri4152a_RES_RES_300 = 300#
Global Const ri4152a_RES_RES_1K = 1000#
Global Const ri4152a_RES_RES_10K = 10000#

' Used by Function ri4152a_sampCoun
Global Const ri4152a_SAMP_COUN_MAX = 50000
Global Const ri4152a_SAMP_COUN_MIN = 1

' Used by Function ri4152a_systLfr
Global Const ri4152a_SYST_LFR_MAX = 400
Global Const ri4152a_SYST_LFR_MIN = 50
Global Const ri4152a_SYST_LFR_50 = 50
Global Const ri4152a_SYST_LFR_60 = 60
Global Const ri4152a_SYST_LFR_400 = 400

' Used by Function ri4152a_trigCoun
Global Const ri4152a_TRIG_COUN_MAX = 50000
Global Const ri4152a_TRIG_COUN_MIN = 1

' Used by Function ri4152a_trigDel
Global Const ri4152a_TRIG_DEL_MAX = 3600#
Global Const ri4152a_TRIG_DEL_MIN = 0#

' Used by Function ri4152a_trigSour
Global Const ri4152a_TRIG_SOUR_BUS = 0
Global Const ri4152a_TRIG_SOUR_EXT = 1
Global Const ri4152a_TRIG_SOUR_IMM = 2
Global Const ri4152a_TRIG_SOUR_TTLT0 = 3
Global Const ri4152a_TRIG_SOUR_TTLT1 = 4
Global Const ri4152a_TRIG_SOUR_TTLT2 = 5
Global Const ri4152a_TRIG_SOUR_TTLT3 = 6
Global Const ri4152a_TRIG_SOUR_TTLT4 = 7
Global Const ri4152a_TRIG_SOUR_TTLT5 = 8
Global Const ri4152a_TRIG_SOUR_TTLT6 = 9
Global Const ri4152a_TRIG_SOUR_TTLT7 = 10

' Used by Function ri4152a_trigSour_Q
'    ri4152a_TRIG_SOUR_BUS
'    ri4152a_TRIG_SOUR_EXT
'    ri4152a_TRIG_SOUR_IMM
'    ri4152a_TRIG_SOUR_TTLT0
'    ri4152a_TRIG_SOUR_TTLT1
'    ri4152a_TRIG_SOUR_TTLT2
'    ri4152a_TRIG_SOUR_TTLT3
'    ri4152a_TRIG_SOUR_TTLT4
'    ri4152a_TRIG_SOUR_TTLT5
'    ri4152a_TRIG_SOUR_TTLT6
'    ri4152a_TRIG_SOUR_TTLT7

' Used by Function ri4152a_trigger
'    ri4152a_TRIG_COUN_MAX
'    ri4152a_TRIG_COUN_MIN
'    ri4152a_TRIG_DEL_MAX
'    ri4152a_TRIG_DEL_MIN
'    ri4152a_TRIG_SOUR_BUS
'    ri4152a_TRIG_SOUR_EXT
'    ri4152a_TRIG_SOUR_IMM
'    ri4152a_TRIG_SOUR_TTLT0
'    ri4152a_TRIG_SOUR_TTLT1
'    ri4152a_TRIG_SOUR_TTLT2
'    ri4152a_TRIG_SOUR_TTLT3
'    ri4152a_TRIG_SOUR_TTLT4
'    ri4152a_TRIG_SOUR_TTLT5
'    ri4152a_TRIG_SOUR_TTLT6
'    ri4152a_TRIG_SOUR_TTLT7

' Used by Function ri4152a_trigger_Q
'    ri4152a_TRIG_SOUR_BUS
'    ri4152a_TRIG_SOUR_EXT
'    ri4152a_TRIG_SOUR_IMM
'    ri4152a_TRIG_SOUR_TTLT0
'    ri4152a_TRIG_SOUR_TTLT1
'    ri4152a_TRIG_SOUR_TTLT2
'    ri4152a_TRIG_SOUR_TTLT3
'    ri4152a_TRIG_SOUR_TTLT4
'    ri4152a_TRIG_SOUR_TTLT5
'    ri4152a_TRIG_SOUR_TTLT6
'    ri4152a_TRIG_SOUR_TTLT7

' Used by Function ri4152a_voltAcRang
'    ri4152a_VOLT_RANG_MAX
'    ri4152a_VOLT_RANG_MIN
'    ri4152a_VOLT_RANG_100MV
'    ri4152a_VOLT_RANG_1V
'    ri4152a_VOLT_RANG_10V
'    ri4152a_VOLT_RANG_100V
'    ri4152a_VOLT_RANG_300V

' Used by Function ri4152a_voltAcRes
Global Const ri4152a_VOLT_AC_RES_MAX = 0.1
Global Const ri4152a_VOLT_AC_RES_MIN = 0.0000001
Global Const ri4152a_VOLT_AC_RES_100_NANO = 0.0000001
Global Const ri4152a_VOLT_AC_RES_1_MICRO = 0.000001
Global Const ri4152a_VOLT_AC_RES_10_MICRO = 0.00001
Global Const ri4152a_VOLT_AC_RES_100_MICRO = 0.0001
Global Const ri4152a_VOLT_AC_RES_1_MILLI = 0.001
Global Const ri4152a_VOLT_AC_RES_10_MILLI = 0.01
Global Const ri4152a_VOLT_AC_RES_100_MILLI = 0.1

' Used by Function ri4152a_voltDcAper
'    ri4152a_APER_MAX
'    ri4152a_APER_MIN

' Used by Function ri4152a_voltDcNplc
'    ri4152a_NPLC_MAX
'    ri4152a_NPLC_MIN

' Used by Function ri4152a_voltDcRang
'    ri4152a_VOLT_RANG_MAX
'    ri4152a_VOLT_RANG_MIN
'    ri4152a_VOLT_RANG_100MV
'    ri4152a_VOLT_RANG_1V
'    ri4152a_VOLT_RANG_10V
'    ri4152a_VOLT_RANG_100V
'    ri4152a_VOLT_RANG_300V

' Used by Function ri4152a_voltDcRes
Global Const ri4152a_VOLT_DC_RES_MAX = 0.1
Global Const ri4152a_VOLT_DC_RES_MIN = 0.00000003
Global Const ri4152a_VOLT_DC_RES_30_NANO = 0.00000003
Global Const ri4152a_VOLT_DC_RES_100_NANO = 0.0000001
Global Const ri4152a_VOLT_DC_RES_300_NANO = 0.0000003
Global Const ri4152a_VOLT_DC_RES_1_MICRO = 0.000001
Global Const ri4152a_VOLT_DC_RES_3_MICRO = 0.000003
Global Const ri4152a_VOLT_DC_RES_10_MICRO = 0.00001
Global Const ri4152a_VOLT_DC_RES_30_MICRO = 0.00003
Global Const ri4152a_VOLT_DC_RES_100_MICRO = 0.0001
Global Const ri4152a_VOLT_DC_RES_300_MICRO = 0.0003
Global Const ri4152a_VOLT_DC_RES_1_MILLI = 0.001
Global Const ri4152a_VOLT_DC_RES_3_MILLI = 0.003
Global Const ri4152a_VOLT_DC_RES_10_MILLI = 0.01
Global Const ri4152a_VOLT_DC_RES_100_MILLI = 0.1


'------------------------------------------------------------------------------
' Function Declarations
'------------------------------------------------------------------------------

Declare Function ri4152a_abor Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_average_Q Lib "ri4152a_32.dll" (ByVal vi As Long, average As Double, minValue As Double, maxValue As Double, points As Long) As Long

Declare Function ri4152a_calCoun_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calCoun As Integer) As Long

Declare Function ri4152a_calLfr Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calLfr As Integer) As Long

Declare Function ri4152a_calLfr_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calLfr As Integer) As Long

Declare Function ri4152a_calSecCode Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal code As String) As Long

Declare Function ri4152a_calSecStat Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal state As Integer, ByVal code As String) As Long

Declare Function ri4152a_calSecStat_Q Lib "ri4152a_32.dll" (ByVal vi As Long, state As Integer) As Long

Declare Function ri4152a_calVal Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal value As Double) As Long

Declare Function ri4152a_calVal_Q Lib "ri4152a_32.dll" (ByVal vi As Long, value As Double) As Long

Declare Function ri4152a_calZeroAuto Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calZeroAuto As Integer) As Long

Declare Function ri4152a_calZeroAuto_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calZeroAuto As Integer) As Long

Declare Function ri4152a_calcAverAver_Q Lib "ri4152a_32.dll" (ByVal vi As Long, average As Double) As Long

Declare Function ri4152a_calcAverCoun_Q Lib "ri4152a_32.dll" (ByVal vi As Long, points As Long) As Long

Declare Function ri4152a_calcAverMax_Q Lib "ri4152a_32.dll" (ByVal vi As Long, maxValue As Double) As Long

Declare Function ri4152a_calcAverMin_Q Lib "ri4152a_32.dll" (ByVal vi As Long, minValue As Double) As Long

Declare Function ri4152a_calcDbRef Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calcDbRef As Double) As Long

Declare Function ri4152a_calcDbRef_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calcDbRef As Double) As Long

Declare Function ri4152a_calcDbmRef Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calcDbmRef As Double) As Long

Declare Function ri4152a_calcDbmRef_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calcDbmRef As Double) As Long

Declare Function ri4152a_calcFunc Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calcFunc As Integer) As Long

Declare Function ri4152a_calcFunc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calcFunc As Integer) As Long

Declare Function ri4152a_calcLimLow Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calcLimLow As Double) As Long

Declare Function ri4152a_calcLimLow_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calcLimLow As Double) As Long

Declare Function ri4152a_calcLimUpp Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calcLimUpp As Double) As Long

Declare Function ri4152a_calcLimUpp_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calcLimUpp As Double) As Long

Declare Function ri4152a_calcNullOffs Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calcNullOffs As Double) As Long

Declare Function ri4152a_calcNullOffs_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calcNullOffs As Double) As Long

Declare Function ri4152a_calcStat Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal calcStat As Integer) As Long

Declare Function ri4152a_calcStat_Q Lib "ri4152a_32.dll" (ByVal vi As Long, calcStat As Integer) As Long

Declare Function ri4152a_confCurrAc Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_confCurrDc Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_confFreq Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_confFres Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_confPer Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_confRes Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_confVoltAc Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_confVoltDc Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_confVoltDcRat Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_conf_Q Lib "ri4152a_32.dll" (ByVal vi As Long, func As Integer, autoRange As Integer, range As Double, resolution As Double) As Long

Declare Function ri4152a_configure Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal func As Integer) As Long

Declare Function ri4152a_currAcRang Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal autoRange As Integer, ByVal range As Double) As Long

Declare Function ri4152a_currAcRang_Q Lib "ri4152a_32.dll" (ByVal vi As Long, autoRange As Integer, range As Double) As Long

Declare Function ri4152a_currAcRes Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal resolution As Double) As Long

Declare Function ri4152a_currAcRes_Q Lib "ri4152a_32.dll" (ByVal vi As Long, resolution As Double) As Long

Declare Function ri4152a_currDcAper Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal aperture As Double) As Long

Declare Function ri4152a_currDcAper_Q Lib "ri4152a_32.dll" (ByVal vi As Long, aperture As Double) As Long

Declare Function ri4152a_currDcNplc Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal nplc As Double) As Long

Declare Function ri4152a_currDcNplc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, nplc As Double) As Long

Declare Function ri4152a_currDcRang Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal autoRange As Integer, ByVal range As Double) As Long

Declare Function ri4152a_currDcRang_Q Lib "ri4152a_32.dll" (ByVal vi As Long, autoRange As Integer, range As Double) As Long

Declare Function ri4152a_currDcRes Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal resolution As Double) As Long

Declare Function ri4152a_currDcRes_Q Lib "ri4152a_32.dll" (ByVal vi As Long, resolution As Double) As Long

Declare Function ri4152a_dataPoin_Q Lib "ri4152a_32.dll" (ByVal vi As Long, numReadings As Long) As Long

Declare Function ri4152a_detBand Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal detBand As Double) As Long

Declare Function ri4152a_detBand_Q Lib "ri4152a_32.dll" (ByVal vi As Long, detBand As Double) As Long

Declare Function ri4152a_fetc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, readings As Double, numReadings As Long) As Long

Declare Function ri4152a_freqAper Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal aperture As Double) As Long

Declare Function ri4152a_freqAper_Q Lib "ri4152a_32.dll" (ByVal vi As Long, aperture As Double) As Long

Declare Function ri4152a_freqVoltRang Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal autoRange As Integer, ByVal range As Double) As Long

Declare Function ri4152a_freqVoltRang_Q Lib "ri4152a_32.dll" (ByVal vi As Long, autoRange As Integer, range As Double) As Long

Declare Function ri4152a_func Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal func As Integer) As Long

Declare Function ri4152a_func_Q Lib "ri4152a_32.dll" (ByVal vi As Long, func As Integer) As Long

Declare Function ri4152a_initImm Lib "ri4152a_32.dll" (ByVal vi As Long) As Long

Declare Function ri4152a_inpImpAuto Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal inpImpAuto As Integer) As Long

Declare Function ri4152a_inpImpAuto_Q Lib "ri4152a_32.dll" (ByVal vi As Long, inpImpAuto As Integer) As Long

Declare Function ri4152a_measCurrAc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measCurrDc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measFreq_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measFres_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measPer_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measRes_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measVoltAc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measVoltDcRat_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measVoltDc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, reading As Double) As Long

Declare Function ri4152a_measure_Q Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal func As Integer, reading As Double) As Long

Declare Function ri4152a_outpTtlt_M Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal ttltLine As Integer, ByVal ttltState As Integer) As Long

Declare Function ri4152a_outpTtlt_M_Q Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal ttltLine As Integer, ttltState As Integer) As Long

Declare Function ri4152a_perAper Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal aperture As Double) As Long

Declare Function ri4152a_perAper_Q Lib "ri4152a_32.dll" (ByVal vi As Long, aperture As Double) As Long

Declare Function ri4152a_perVoltRang Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal autoRange As Integer, ByVal range As Double) As Long

Declare Function ri4152a_perVoltRang_Q Lib "ri4152a_32.dll" (ByVal vi As Long, autoRange As Integer, range As Double) As Long

'Declare Function ri4152a_readStatusByte_Q Lib "ri4152a_32.dll" (ByVal vi As Long, statusByte As Integer) As Long

Declare Function ri4152a_read_Q Lib "ri4152a_32.dll" (ByVal vi As Long, readings As Double, numReadings As Long) As Long

Declare Function ri4152a_resAper Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal aperture As Double) As Long

Declare Function ri4152a_resAper_Q Lib "ri4152a_32.dll" (ByVal vi As Long, aperture As Double) As Long

Declare Function ri4152a_resNplc Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal nplc As Double) As Long

Declare Function ri4152a_resNplc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, nplc As Double) As Long

Declare Function ri4152a_resRang Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal autoRange As Integer, ByVal range As Double) As Long

Declare Function ri4152a_resRang_Q Lib "ri4152a_32.dll" (ByVal vi As Long, autoRange As Integer, range As Double) As Long

Declare Function ri4152a_resRes Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal resolution As Double) As Long

Declare Function ri4152a_resRes_Q Lib "ri4152a_32.dll" (ByVal vi As Long, resolution As Double) As Long

Declare Function ri4152a_sampCoun Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal sampCoun As Long) As Long

Declare Function ri4152a_sampCoun_Q Lib "ri4152a_32.dll" (ByVal vi As Long, sampCoun As Long) As Long

Declare Function ri4152a_systLfr Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal systLfr As Integer) As Long

Declare Function ri4152a_systLfr_Q Lib "ri4152a_32.dll" (ByVal vi As Long, systLfr As Integer) As Long

Declare Function ri4152a_timedFetch_Q Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal timeOut As Long, readings As Double, numReadings As Long) As Long

Declare Function ri4152a_trigCoun Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal trigCoun As Long) As Long

Declare Function ri4152a_trigCoun_Q Lib "ri4152a_32.dll" (ByVal vi As Long, trigCoun As Long) As Long

Declare Function ri4152a_trigDel Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal trigDelAuto As Integer, ByVal trigDel As Double) As Long

Declare Function ri4152a_trigDel_Q Lib "ri4152a_32.dll" (ByVal vi As Long, autoDelay As Integer, delay As Double) As Long

Declare Function ri4152a_trigSour Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal trigSour As Integer) As Long

Declare Function ri4152a_trigSour_Q Lib "ri4152a_32.dll" (ByVal vi As Long, trigSour As Integer) As Long

Declare Function ri4152a_trigger Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal count As Long, ByVal autoDelay As Integer, ByVal delay As Double, ByVal source As Integer) As Long

Declare Function ri4152a_trigger_Q Lib "ri4152a_32.dll" (ByVal vi As Long, count As Long, autoDelay As Integer, delay As Double, source As Integer) As Long

Declare Function ri4152a_voltAcRang Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal autoRange As Integer, ByVal range As Double) As Long

Declare Function ri4152a_voltAcRang_Q Lib "ri4152a_32.dll" (ByVal vi As Long, autoRange As Integer, range As Double) As Long

Declare Function ri4152a_voltAcRes Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal resolution As Double) As Long

Declare Function ri4152a_voltAcRes_Q Lib "ri4152a_32.dll" (ByVal vi As Long, resolution As Double) As Long

Declare Function ri4152a_voltDcAper Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal aperture As Double) As Long

Declare Function ri4152a_voltDcAper_Q Lib "ri4152a_32.dll" (ByVal vi As Long, aperture As Double) As Long

Declare Function ri4152a_voltDcNplc Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal nplc As Double) As Long

Declare Function ri4152a_voltDcNplc_Q Lib "ri4152a_32.dll" (ByVal vi As Long, nplc As Double) As Long

Declare Function ri4152a_voltDcRang Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal autoRange As Integer, ByVal range As Double) As Long

Declare Function ri4152a_voltDcRang_Q Lib "ri4152a_32.dll" (ByVal vi As Long, autoRange As Integer, range As Double) As Long

Declare Function ri4152a_voltDcRes Lib "ri4152a_32.dll" (ByVal vi As Long, ByVal resolution As Double) As Long

Declare Function ri4152a_voltDcRes_Q Lib "ri4152a_32.dll" (ByVal vi As Long, resolution As Double) As Long

