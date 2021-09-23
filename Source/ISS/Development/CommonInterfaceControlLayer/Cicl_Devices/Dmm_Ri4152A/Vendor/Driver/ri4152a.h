/* Copyright (C) 1998 Racal Instruments, Inc. */
/* VXIplug&play Instrument Driver for the RI4152A*/

#ifndef ri4152a_INCLUDE 
#define ri4152a_INCLUDE
#include "vpptype.h"

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif

/******************************************************
 * INSTR_DEVELOPER: Take out what you don't need 
 ******************************************************
 */
/* HP Common Error numbers start at BFFC0D00 */
#define ri4152a_INSTR_ERROR_NOT_VXI     (_VI_ERROR+0x3FFC0D00L) /* 0xBFFC0D00 */
#define ri4152a_INSTR_ERROR_MEM_ALLOC   (_VI_ERROR+0x3FFC0D01L) /* 0xBFFC0D01 */
#define ri4152a_INSTR_ERROR_NULL_PTR    (_VI_ERROR+0x3FFC0D02L) /* 0xBFFC0D02 */
#define ri4152a_INSTR_ERROR_RESET_FAILED (_VI_ERROR+0x3FFC0D03L)/* 0xBFFC0D03 */
#define ri4152a_INSTR_ERROR_UNEXPECTED  (_VI_ERROR+0x3FFC0D04L) /* 0xBFFC0D04 */
#define ri4152a_INSTR_ERROR_INV_SESSION (_VI_ERROR+0x3FFC0D05L) /* 0xBFFC0D05 */
#define ri4152a_INSTR_ERROR_LOOKUP      (_VI_ERROR+0x3FFC0D06L) /* 0xBFFC0D06 */
#define ri4152a_INSTR_ERROR_DETECTED    (_VI_ERROR+0x3FFC0D07L) /* 0xBFFC0D07 */

#define ri4152a_USER_ERROR_HANDLER			-1

/******   Used by Status System Functions   *****/

#define ri4152a_QUES_VOLT				401
#define ri4152a_QUES_CURR				402
#define ri4152a_QUES_RES				410
#define ri4152a_QUES_LIM_LO			412
#define ri4152a_QUES_LIM_HI			413

#define ri4152a_ESR_OPC				601
#define ri4152a_ESR_QUERY_ERROR			603
#define ri4152a_ESR_DEVICE_DEPENDENT_ERROR		604
#define ri4152a_ESR_EXECUTION_ERROR		605
#define ri4152a_ESR_COMMAND_ERROR			606


/******   Used by Function ri4152a_timeOut   *****/
#define  ri4152a_TIMEOUT_MAX  2147483647
#define  ri4152a_TIMEOUT_MIN  0


/* required plug and play functions from VPP-3.1*/


ViStatus _VI_FUNC ri4152a_init (ViRsrc InstrDesc, ViBoolean id_query,
                            ViBoolean reset, ViPSession vi );//DRB

ViStatus _VI_FUNC ri4152a_close (ViSession vi);

ViStatus _VI_FUNC ri4152a_reset (ViSession vi);

ViStatus _VI_FUNC ri4152a_self_test (ViSession vi, ViPInt16 test_result,
                                 ViChar _VI_FAR test_message[]);

ViStatus _VI_FUNC ri4152a_error_query (ViSession vi, ViPInt32 error,
                                   ViChar _VI_FAR error_message[]);

ViStatus _VI_FUNC ri4152a_error_message (ViSession vi, ViStatus error,
                                     ViChar _VI_FAR message[]);

ViStatus _VI_FUNC ri4152a_revision_query (ViSession vi, ViChar _VI_FAR driver_rev[],
                                      ViChar _VI_FAR instr_rev[]);

/* HP other standard functions */

ViStatus _VI_FUNC ri4152a_dcl(ViSession vi);

ViStatus _VI_FUNC ri4152a_errorQueryDetect(ViSession vi, ViBoolean errDetect);

ViStatus _VI_FUNC ri4152a_errorQueryDetect_Q(ViSession vi, ViPBoolean pErrDetect);

ViStatus _VI_FUNC ri4152a_opc(ViSession vi);

ViStatus _VI_FUNC ri4152a_opc_Q(ViSession vi, ViPInt16 opc);

ViStatus _VI_FUNC ri4152a_readStatusByte_Q(ViSession vi, ViPInt16 statusByte);

ViStatus _VI_FUNC ri4152a_statCond_Q(ViSession vi, ViInt32 happening,
                                 ViPBoolean pCondition);

ViStatus _VI_FUNC ri4152a_statEvenClr(ViSession vi);

ViStatus _VI_FUNC ri4152a_statEven_Q(ViSession vi, ViInt32 happening,
                                 ViPBoolean pEvent);

#ifdef INSTR_CALLBACKS

typedef void (_VI_FUNCH _VI_PTR  ri4152a_InstrEventHandler)(
        ViSession vi, ViInt32 happening, ViAddr userData);     
	
ViStatus _VI_FUNC ri4152a_statEvenHdlr(ViSession vi, ViInt32 happening,       
                                   ri4152a_InstrEventHandler eventHandler,
                                   ViAddr userData);

typedef void (_VI_FUNCH _VI_PTR _VI_PTR ri4152a_InstrPEventHandler)(
	ViSession vi, ViInt32 happening, ViAddr userData);

ViStatus _VI_FUNC ri4152a_statEvenHdlr_Q(ViSession vi, ViInt32 happening,
                                     ri4152a_InstrPEventHandler pEventHandler,
                                     ViPAddr pUserData);

ViStatus _VI_FUNC ri4152a_statEvenHdlrDelAll(ViSession vi);
#endif /* INSTR_CALLBACKS */

ViStatus _VI_FUNC ri4152a_timeOut (ViSession vi, ViInt32 timeOut);

ViStatus _VI_FUNC ri4152a_timeOut_Q (ViSession vi, ViPInt32 timeOut);

ViStatus _VI_FUNC ri4152a_trg(ViSession vi);

ViStatus _VI_FUNC ri4152a_wai(ViSession vi);

/* Instr Specific Functions follow */


/******   Used by Function ri4152a_calLfr   *****/
#define  ri4152a_CAL_LFR_MAX  400
#define  ri4152a_CAL_LFR_MIN  50
#define  ri4152a_CAL_LFR_50  50
#define  ri4152a_CAL_LFR_60  60
#define  ri4152a_CAL_LFR_400  400


/******   Used by Function ri4152a_calVal   *****/
#define  ri4152a_CAL_VAL_MAX  100.0e6
#define  ri4152a_CAL_VAL_MIN  -300.0


/******   Used by Function ri4152a_calcDbRef   *****/
#define  ri4152a_CALC_DB_REF_MAX  200.0
#define  ri4152a_CALC_DB_REF_MIN  -200.0


/******   Used by Function ri4152a_calcDbRef_Q   *****/
/*  ri4152a_CALC_DB_REF_MAX  */
/*  ri4152a_CALC_DB_REF_MIN  */


/******   Used by Function ri4152a_calcDbmRef   *****/
#define  ri4152a_CALC_DBM_REF_MIN  50.0
#define  ri4152a_CALC_DBM_REF_MAX  8000.0
#define  ri4152a_CALC_DBM_REF_50  50.0
#define  ri4152a_CALC_DBM_REF_75  75.0
#define  ri4152a_CALC_DBM_REF_93  93.0
#define  ri4152a_CALC_DBM_REF_110  110.0
#define  ri4152a_CALC_DBM_REF_124  124.0
#define  ri4152a_CALC_DBM_REF_125  125.0
#define  ri4152a_CALC_DBM_REF_135  135.0
#define  ri4152a_CALC_DBM_REF_150  150.0
#define  ri4152a_CALC_DBM_REF_250  250.0
#define  ri4152a_CALC_DBM_REF_300  300.0
#define  ri4152a_CALC_DBM_REF_500  500.0
#define  ri4152a_CALC_DBM_REF_600  600.0
#define  ri4152a_CALC_DBM_REF_800  800.0
#define  ri4152a_CALC_DBM_REF_900  900.0
#define  ri4152a_CALC_DBM_REF_1000  1000.0
#define  ri4152a_CALC_DBM_REF_1200  1200.0
#define  ri4152a_CALC_DBM_REF_8000  8000.0


/******   Used by Function ri4152a_calcFunc   *****/
#define  ri4152a_CALC_FUNC_NULL  0
#define  ri4152a_CALC_FUNC_AVER  1
#define  ri4152a_CALC_FUNC_LIM  2
#define  ri4152a_CALC_FUNC_DBM  3
#define  ri4152a_CALC_FUNC_DB  4


/******   Used by Function ri4152a_calcFunc_Q   *****/
/*  ri4152a_CALC_FUNC_NULL  */
/*  ri4152a_CALC_FUNC_AVER  */
/*  ri4152a_CALC_FUNC_LIM  */
/*  ri4152a_CALC_FUNC_DBM  */
/*  ri4152a_CALC_FUNC_DB  */


/******   Used by Function ri4152a_calcLimLow   *****/
#define  ri4152a_CALC_LIM_MAX  1.2e8
#define  ri4152a_CALC_LIM_MIN  -1.2e8


/******   Used by Function ri4152a_calcLimUpp   *****/
/*  ri4152a_CALC_LIM_MAX  */
/*  ri4152a_CALC_LIM_MIN  */


/******   Used by Function ri4152a_calcNullOffs   *****/
#define  ri4152a_CALC_NULL_OFFS_MAX  1.2e8
#define  ri4152a_CALC_NULL_OFFS_MIN  -1.2e8


/******   Used by Function ri4152a_calcNullOffs_Q   *****/
/*  ri4152a_CALC_NULL_OFFS_MAX  */
/*  ri4152a_CALC_NULL_OFFS_MIN  */


/******   Used by Function ri4152a_conf_Q   *****/
#define  ri4152a_CONFQ_FREQ  0
#define  ri4152a_CONFQ_PER  1
#define  ri4152a_CONFQ_FRES  2
#define  ri4152a_CONFQ_RES  3
#define  ri4152a_CONFQ_VOLT_AC  4
#define  ri4152a_CONFQ_VOLT_DC  5
#define  ri4152a_CONFQ_CURR_AC  6
#define  ri4152a_CONFQ_CURR_DC  7
#define  ri4152a_CONFQ_VOLT_RAT  8


/******   Used by Function ri4152a_configure   *****/
#define  ri4152a_CONF_FREQ  0
#define  ri4152a_CONF_PER  1
#define  ri4152a_CONF_FRES  2
#define  ri4152a_CONF_RES  3
#define  ri4152a_CONF_VOLT_AC  4
#define  ri4152a_CONF_VOLT_DC  5
#define  ri4152a_CONF_CURR_AC  6
#define  ri4152a_CONF_CURR_DC  7
#define  ri4152a_CONF_VOLT_RAT  8


/******   Used by Function ri4152a_currAcRang   *****/
#define  ri4152a_CURR_AC_RANG_MAX  3.0
#define  ri4152a_CURR_AC_RANG_MIN  0.0
#define  ri4152a_CURR_AC_RANG_1A  1.0
#define  ri4152a_CURR_AC_RANG_3A  3.0


/******   Used by Function ri4152a_currAcRes   *****/
#define  ri4152a_CURR_AC_RES_MAX  300.0e-6
#define  ri4152a_CURR_AC_RES_MIN  1.0e-6
#define  ri4152a_CURR_AC_RES_1_MICRO  1.0e-6
#define  ri4152a_CURR_AC_RES_3_MICRO  3.0e-6
#define  ri4152a_CURR_AC_RES_10_MICRO  10.0e-6
#define  ri4152a_CURR_AC_RES_30_MICRO  30.0e-6
#define  ri4152a_CURR_AC_RES_100_MICRO  100.0e-6
#define  ri4152a_CURR_AC_RES_300_MICRO  300.0e-6


/******   Used by Function ri4152a_currDcAper   *****/
#define  ri4152a_APER_MAX  2.0
#define  ri4152a_APER_MIN  4.0e-4


/******   Used by Function ri4152a_currDcNplc   *****/
#define  ri4152a_NPLC_MAX  100.0
#define  ri4152a_NPLC_MIN  0.02


/******   Used by Function ri4152a_currDcRang   *****/
#define  ri4152a_CURR_DC_RANG_MAX  3.0
#define  ri4152a_CURR_DC_RANG_MIN  0.0
#define  ri4152a_CURR_DC_RANG_10MA  10.0e-3
#define  ri4152a_CURR_DC_RANG_100MA  100.0e-3
#define  ri4152a_CURR_DC_RANG_1A  1.0
#define  ri4152a_CURR_DC_RANG_3A  3.0


/******   Used by Function ri4152a_currDcRes   *****/
#define  ri4152a_CURR_DC_RES_MAX  300.0e-6
#define  ri4152a_CURR_DC_RES_MIN  3.0e-9
#define  ri4152a_CURR_DC_RES_3_NANO  3.0e-9
#define  ri4152a_CURR_DC_RES_10_NANO  10.0e-9
#define  ri4152a_CURR_DC_RES_30_NANO  30.0e-9
#define  ri4152a_CURR_DC_RES_100_NANO  100.0e-9
#define  ri4152a_CURR_DC_RES_300_NANO  300.0e-9
#define  ri4152a_CURR_DC_RES_900_NANO  900.0e-9
#define  ri4152a_CURR_DC_RES_1_MICRO  1.0e-6
#define  ri4152a_CURR_DC_RES_3_MICRO  3.0e-6
#define  ri4152a_CURR_DC_RES_9_MICRO  9.0e-6
#define  ri4152a_CURR_DC_RES_10_MICRO  10.0e-6
#define  ri4152a_CURR_DC_RES_30_MICRO  30.0e-6
#define  ri4152a_CURR_DC_RES_100_MICRO  100.0e-6
#define  ri4152a_CURR_DC_RES_300_MICRO  300.0e-6


/******   Used by Function ri4152a_detBand   *****/
#define  ri4152a_DET_BAND_MAX  3.0e5
#define  ri4152a_DET_BAND_MIN  3.0


/******   Used by Function ri4152a_freqAper   *****/
#define  ri4152a_FREQ_APER_MAX  1.0
#define  ri4152a_FREQ_APER_MIN  0.01


/******   Used by Function ri4152a_freqVoltRang   *****/
#define  ri4152a_VOLT_RANG_MAX  300.0
#define  ri4152a_VOLT_RANG_MIN  0.0
#define  ri4152a_VOLT_RANG_100MV  100.0e-3
#define  ri4152a_VOLT_RANG_1V  1.0
#define  ri4152a_VOLT_RANG_10V  10.0
#define  ri4152a_VOLT_RANG_100V  100.0
#define  ri4152a_VOLT_RANG_300V  300.0


/******   Used by Function ri4152a_func   *****/
#define  ri4152a_FUNC_FREQ  0
#define  ri4152a_FUNC_PER  1
#define  ri4152a_FUNC_FRES  2
#define  ri4152a_FUNC_RES  3
#define  ri4152a_FUNC_VOLT_AC  4
#define  ri4152a_FUNC_VOLT_DC  5
#define  ri4152a_FUNC_CURR_AC  6
#define  ri4152a_FUNC_CURR_DC  7
#define  ri4152a_FUNC_VOLT_DC_RAT  8


/******   Used by Function ri4152a_measure_Q   *****/
/*  ri4152a_CONF_FREQ  */
/*  ri4152a_CONF_PER  */
/*  ri4152a_CONF_FRES  */
/*  ri4152a_CONF_RES  */
/*  ri4152a_CONF_VOLT_AC  */
/*  ri4152a_CONF_VOLT_DC  */
/*  ri4152a_CONF_CURR_AC  */
/*  ri4152a_CONF_CURR_DC  */
/*  ri4152a_CONF_VOLT_RAT  */


/******   Used by Function ri4152a_outpTtlt_M   *****/
#define  ri4152a_TTLT_LINE_MAX  7
#define  ri4152a_TTLT_LINE_MIN  0


/******   Used by Function ri4152a_outpTtlt_M_Q   *****/
/*  ri4152a_TTLT_LINE_MAX  */
/*  ri4152a_TTLT_LINE_MIN  */


/******   Used by Function ri4152a_perAper   *****/
#define  ri4152a_PER_APER_MAX  1.0
#define  ri4152a_PER_APER_MIN  0.01


/******   Used by Function ri4152a_perVoltRang   *****/
/*  ri4152a_VOLT_RANG_MAX  */
/*  ri4152a_VOLT_RANG_MIN  */
/*  ri4152a_VOLT_RANG_100MV  */
/*  ri4152a_VOLT_RANG_1V  */
/*  ri4152a_VOLT_RANG_10V  */
/*  ri4152a_VOLT_RANG_100V  */
/*  ri4152a_VOLT_RANG_300V  */


/******   Used by Function ri4152a_resAper   *****/
/*  ri4152a_APER_MAX  */
/*  ri4152a_APER_MIN  */


/******   Used by Function ri4152a_resNplc   *****/
/*  ri4152a_NPLC_MAX  */
/*  ri4152a_NPLC_MIN  */


/******   Used by Function ri4152a_resRang   *****/
#define  ri4152a_RES_RANG_MAX  100.0e6
#define  ri4152a_RES_RANG_MIN  100.0
#define  ri4152a_RES_RANG_100  100
#define  ri4152a_RES_RANG_1K  1.0e3
#define  ri4152a_RES_RANG_10K  10.0e3
#define  ri4152a_RES_RANG_100K  100.0e3
#define  ri4152a_RES_RANG_1M  1.0e6
#define  ri4152a_RES_RANG_10M  10.0e6
#define  ri4152a_RES_RANG_100M  100.0e6


/******   Used by Function ri4152a_resRes   *****/
#define  ri4152a_RES_RES_MAX  10.0e3
#define  ri4152a_RES_RES_MIN  30.0e-6
#define  ri4152a_RES_RES_30_MICRO  30.0e-6
#define  ri4152a_RES_RES_100_MICRO  100.0e-6
#define  ri4152a_RES_RES_300_MICRO  300.0e-6
#define  ri4152a_RES_RES_1_MILLI  1.0e-3
#define  ri4152a_RES_RES_3_MILLI  3.0e-3
#define  ri4152a_RES_RES_10_MILLI  10.0e-3
#define  ri4152a_RES_RES_30_MILLI  30.0e-3
#define  ri4152a_RES_RES_100_MILLI  100.0e-3
#define  ri4152a_RES_RES_300_MILLI  300.0e-3
#define  ri4152a_RES_RES_1  1.0
#define  ri4152a_RES_RES_3  3.0
#define  ri4152a_RES_RES_10  10.0
#define  ri4152a_RES_RES_30  30.0
#define  ri4152a_RES_RES_100  100.0
#define  ri4152a_RES_RES_300  300.0
#define  ri4152a_RES_RES_1K  1.0e3
#define  ri4152a_RES_RES_10K  10.0e3


/******   Used by Function ri4152a_sampCoun   *****/
#define  ri4152a_SAMP_COUN_MAX  50000
#define  ri4152a_SAMP_COUN_MIN  1


/******   Used by Function ri4152a_systLfr   *****/
#define  ri4152a_SYST_LFR_MAX  400
#define  ri4152a_SYST_LFR_MIN  50
#define  ri4152a_SYST_LFR_50  50
#define  ri4152a_SYST_LFR_60  60
#define  ri4152a_SYST_LFR_400  400


/******   Used by Function ri4152a_timedFetch_Q   *****/
/* #define  ri4152a_TIMEOUT_MAX  2147483647 */
/* #define  ri4152a_TIMEOUT_MIN  0 */


/******   Used by Function ri4152a_trigCoun   *****/
#define  ri4152a_TRIG_COUN_MAX  50000
#define  ri4152a_TRIG_COUN_MIN  1


/******   Used by Function ri4152a_trigDel   *****/
#define  ri4152a_TRIG_DEL_MAX  3600.0
#define  ri4152a_TRIG_DEL_MIN  0.0


/******   Used by Function ri4152a_trigSour   *****/
#define  ri4152a_TRIG_SOUR_BUS  0
#define  ri4152a_TRIG_SOUR_EXT  1
#define  ri4152a_TRIG_SOUR_IMM  2
#define  ri4152a_TRIG_SOUR_TTLT0  3
#define  ri4152a_TRIG_SOUR_TTLT1  4
#define  ri4152a_TRIG_SOUR_TTLT2  5
#define  ri4152a_TRIG_SOUR_TTLT3  6
#define  ri4152a_TRIG_SOUR_TTLT4  7
#define  ri4152a_TRIG_SOUR_TTLT5  8
#define  ri4152a_TRIG_SOUR_TTLT6  9
#define  ri4152a_TRIG_SOUR_TTLT7  10


/******   Used by Function ri4152a_trigSour_Q   *****/
/*  ri4152a_TRIG_SOUR_BUS  */
/*  ri4152a_TRIG_SOUR_EXT  */
/*  ri4152a_TRIG_SOUR_IMM  */
/*  ri4152a_TRIG_SOUR_TTLT0  */
/*  ri4152a_TRIG_SOUR_TTLT1  */
/*  ri4152a_TRIG_SOUR_TTLT2  */
/*  ri4152a_TRIG_SOUR_TTLT3  */
/*  ri4152a_TRIG_SOUR_TTLT4  */
/*  ri4152a_TRIG_SOUR_TTLT5  */
/*  ri4152a_TRIG_SOUR_TTLT6  */
/*  ri4152a_TRIG_SOUR_TTLT7  */


/******   Used by Function ri4152a_trigger   *****/
/*  ri4152a_TRIG_COUN_MAX  */
/*  ri4152a_TRIG_COUN_MIN  */
/*  ri4152a_TRIG_DEL_MAX  */
/*  ri4152a_TRIG_DEL_MIN  */
/*  ri4152a_TRIG_SOUR_BUS  */
/*  ri4152a_TRIG_SOUR_EXT  */
/*  ri4152a_TRIG_SOUR_IMM  */
/*  ri4152a_TRIG_SOUR_TTLT0  */
/*  ri4152a_TRIG_SOUR_TTLT1  */
/*  ri4152a_TRIG_SOUR_TTLT2  */
/*  ri4152a_TRIG_SOUR_TTLT3  */
/*  ri4152a_TRIG_SOUR_TTLT4  */
/*  ri4152a_TRIG_SOUR_TTLT5  */
/*  ri4152a_TRIG_SOUR_TTLT6  */
/*  ri4152a_TRIG_SOUR_TTLT7  */


/******   Used by Function ri4152a_voltAcRang   *****/
/*  ri4152a_VOLT_RANG_MAX  */
/*  ri4152a_VOLT_RANG_MIN  */
/*  ri4152a_VOLT_RANG_100MV  */
/*  ri4152a_VOLT_RANG_1V  */
/*  ri4152a_VOLT_RANG_10V  */
/*  ri4152a_VOLT_RANG_100V  */
/*  ri4152a_VOLT_RANG_300V  */


/******   Used by Function ri4152a_voltAcRes   *****/
#define  ri4152a_VOLT_AC_RES_MAX  100.0e-3
#define  ri4152a_VOLT_AC_RES_MIN  100.0e-9
#define  ri4152a_VOLT_AC_RES_100_NANO  100.0e-9
#define  ri4152a_VOLT_AC_RES_1_MICRO  1.0e-6
#define  ri4152a_VOLT_AC_RES_10_MICRO  10.0e-6
#define  ri4152a_VOLT_AC_RES_100_MICRO  100.0e-6
#define  ri4152a_VOLT_AC_RES_1_MILLI  1.0e-3
#define  ri4152a_VOLT_AC_RES_10_MILLI  10.0e-3
#define  ri4152a_VOLT_AC_RES_100_MILLI  100.0e-3


/******   Used by Function ri4152a_voltDcAper   *****/
/*  ri4152a_APER_MAX  */
/*  ri4152a_APER_MIN  */


/******   Used by Function ri4152a_voltDcNplc   *****/
/*  ri4152a_NPLC_MAX  */
/*  ri4152a_NPLC_MIN  */


/******   Used by Function ri4152a_voltDcRang   *****/
/*  ri4152a_VOLT_RANG_MAX  */
/*  ri4152a_VOLT_RANG_MIN  */
/*  ri4152a_VOLT_RANG_100MV  */
/*  ri4152a_VOLT_RANG_1V  */
/*  ri4152a_VOLT_RANG_10V  */
/*  ri4152a_VOLT_RANG_100V  */
/*  ri4152a_VOLT_RANG_300V  */


/******   Used by Function ri4152a_voltDcRes   *****/
#define  ri4152a_VOLT_DC_RES_MAX  100.0e-3
#define  ri4152a_VOLT_DC_RES_MIN  30.0e-9
#define  ri4152a_VOLT_DC_RES_30_NANO  30.0e-9
#define  ri4152a_VOLT_DC_RES_100_NANO  100.0e-9
#define  ri4152a_VOLT_DC_RES_300_NANO  300.0e-9
#define  ri4152a_VOLT_DC_RES_1_MICRO  1.0e-6
#define  ri4152a_VOLT_DC_RES_3_MICRO  3.0e-6
#define  ri4152a_VOLT_DC_RES_10_MICRO  10.0e-6
#define  ri4152a_VOLT_DC_RES_30_MICRO  30.0e-6
#define  ri4152a_VOLT_DC_RES_100_MICRO  100.0e-6
#define  ri4152a_VOLT_DC_RES_300_MICRO  300.0e-6
#define  ri4152a_VOLT_DC_RES_1_MILLI  1.0e-3
#define  ri4152a_VOLT_DC_RES_3_MILLI  3.0e-3
#define  ri4152a_VOLT_DC_RES_10_MILLI  10.0e-3
#define  ri4152a_VOLT_DC_RES_100_MILLI  100.0e-3

ViStatus _VI_FUNC ri4152a_abor  (ViSession vi);

ViStatus _VI_FUNC ri4152a_average_Q  (ViSession vi, ViPReal64 average, ViPReal64 minValue, ViPReal64 maxValue, ViPInt32 points);

ViStatus _VI_FUNC ri4152a_calCoun_Q  (ViSession vi, ViPInt16 calCoun);

ViStatus _VI_FUNC ri4152a_calLfr  (ViSession vi, ViInt16 calLfr);

ViStatus _VI_FUNC ri4152a_calLfr_Q  (ViSession vi, ViPInt16 calLfr);

ViStatus _VI_FUNC ri4152a_calSecCode  (ViSession vi, ViString code);

ViStatus _VI_FUNC ri4152a_calSecStat  (ViSession vi, ViBoolean state, ViString code);

ViStatus _VI_FUNC ri4152a_calSecStat_Q  (ViSession vi, ViPBoolean state);

ViStatus _VI_FUNC ri4152a_calVal  (ViSession vi, ViReal64 value);

ViStatus _VI_FUNC ri4152a_calVal_Q  (ViSession vi, ViPReal64 value);

ViStatus _VI_FUNC ri4152a_calZeroAuto  (ViSession vi, ViInt16 calZeroAuto);

ViStatus _VI_FUNC ri4152a_calZeroAuto_Q  (ViSession vi, ViPInt16 calZeroAuto);

ViStatus _VI_FUNC ri4152a_calcAverAver_Q  (ViSession vi, ViPReal64 average);

ViStatus _VI_FUNC ri4152a_calcAverCoun_Q  (ViSession vi, ViPInt32 points);

ViStatus _VI_FUNC ri4152a_calcAverMax_Q  (ViSession vi, ViPReal64 maxValue);

ViStatus _VI_FUNC ri4152a_calcAverMin_Q  (ViSession vi, ViPReal64 minValue);

ViStatus _VI_FUNC ri4152a_calcDbRef  (ViSession vi, ViReal64 calcDbRef);

ViStatus _VI_FUNC ri4152a_calcDbRef_Q  (ViSession vi, ViPReal64 calcDbRef);

ViStatus _VI_FUNC ri4152a_calcDbmRef  (ViSession vi, ViReal64 calcDbmRef);

ViStatus _VI_FUNC ri4152a_calcDbmRef_Q  (ViSession vi, ViPReal64 calcDbmRef);

ViStatus _VI_FUNC ri4152a_calcFunc  (ViSession vi, ViInt16 calcFunc);

ViStatus _VI_FUNC ri4152a_calcFunc_Q  (ViSession vi, ViPInt16 calcFunc);

ViStatus _VI_FUNC ri4152a_calcLimLow  (ViSession vi, ViReal64 calcLimLow);

ViStatus _VI_FUNC ri4152a_calcLimLow_Q  (ViSession vi, ViPReal64 calcLimLow);

ViStatus _VI_FUNC ri4152a_calcLimUpp  (ViSession vi, ViReal64 calcLimUpp);

ViStatus _VI_FUNC ri4152a_calcLimUpp_Q  (ViSession vi, ViPReal64 calcLimUpp);

ViStatus _VI_FUNC ri4152a_calcNullOffs  (ViSession vi, ViReal64 calcNullOffs);

ViStatus _VI_FUNC ri4152a_calcNullOffs_Q  (ViSession vi, ViPReal64 calcNullOffs);

ViStatus _VI_FUNC ri4152a_calcStat  (ViSession vi, ViBoolean calcStat);

ViStatus _VI_FUNC ri4152a_calcStat_Q  (ViSession vi, ViPBoolean calcStat);

ViStatus _VI_FUNC ri4152a_confCurrAc  (ViSession vi);

ViStatus _VI_FUNC ri4152a_confCurrDc  (ViSession vi);

ViStatus _VI_FUNC ri4152a_confFreq  (ViSession vi);

ViStatus _VI_FUNC ri4152a_confFres  (ViSession vi);

ViStatus _VI_FUNC ri4152a_confPer  (ViSession vi);

ViStatus _VI_FUNC ri4152a_confRes  (ViSession vi);

ViStatus _VI_FUNC ri4152a_confVoltAc  (ViSession vi);

ViStatus _VI_FUNC ri4152a_confVoltDc  (ViSession vi);

ViStatus _VI_FUNC ri4152a_confVoltDcRat  (ViSession vi);

ViStatus _VI_FUNC ri4152a_conf_Q  (ViSession vi, ViPInt16 function, ViPBoolean autoRange, ViPReal64 range, ViPReal64 resolution);

ViStatus _VI_FUNC ri4152a_configure  (ViSession vi, ViInt16 function);

ViStatus _VI_FUNC ri4152a_currAcRang  (ViSession vi, ViBoolean autoRange, ViReal64 range);

ViStatus _VI_FUNC ri4152a_currAcRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);

ViStatus _VI_FUNC ri4152a_currAcRes  (ViSession vi, ViReal64 resolution);

ViStatus _VI_FUNC ri4152a_currAcRes_Q  (ViSession vi, ViPReal64 resolution);

ViStatus _VI_FUNC ri4152a_currDcAper  (ViSession vi, ViReal64 aperture);

ViStatus _VI_FUNC ri4152a_currDcAper_Q  (ViSession vi, ViPReal64 aperture);

ViStatus _VI_FUNC ri4152a_currDcNplc  (ViSession vi, ViReal64 nplc);

ViStatus _VI_FUNC ri4152a_currDcNplc_Q  (ViSession vi, ViPReal64 nplc);

ViStatus _VI_FUNC ri4152a_currDcRang  (ViSession vi, ViBoolean autoRange, ViReal64 range);

ViStatus _VI_FUNC ri4152a_currDcRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);

ViStatus _VI_FUNC ri4152a_currDcRes  (ViSession vi, ViReal64 resolution);

ViStatus _VI_FUNC ri4152a_currDcRes_Q  (ViSession vi, ViPReal64 resolution);

ViStatus _VI_FUNC ri4152a_dataPoin_Q  (ViSession vi, ViPInt32 numReadings);

ViStatus _VI_FUNC ri4152a_detBand  (ViSession vi, ViReal64 detBand);

ViStatus _VI_FUNC ri4152a_detBand_Q  (ViSession vi, ViPReal64 detBand);

ViStatus _VI_FUNC ri4152a_fetc_Q  (ViSession vi, ViReal64 _VI_FAR readings[], ViPInt32 numReadings);

ViStatus _VI_FUNC ri4152a_freqAper  (ViSession vi, ViReal64 aperture);

ViStatus _VI_FUNC ri4152a_freqAper_Q  (ViSession vi, ViPReal64 aperture);

ViStatus _VI_FUNC ri4152a_freqVoltRang  (ViSession vi, ViBoolean autoRange, ViReal64 range);

ViStatus _VI_FUNC ri4152a_freqVoltRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);

ViStatus _VI_FUNC ri4152a_func  (ViSession vi, ViInt16 func);

ViStatus _VI_FUNC ri4152a_func_Q  (ViSession vi, ViPInt16 func);

ViStatus _VI_FUNC ri4152a_initImm  (ViSession vi);

ViStatus _VI_FUNC ri4152a_inpImpAuto  (ViSession vi, ViBoolean inpImpAuto);

ViStatus _VI_FUNC ri4152a_inpImpAuto_Q  (ViSession vi, ViPBoolean inpImpAuto);

ViStatus _VI_FUNC ri4152a_measCurrAc_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measCurrDc_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measFreq_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measFres_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measPer_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measRes_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measVoltAc_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measVoltDcRat_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measVoltDc_Q  (ViSession vi, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_measure_Q  (ViSession vi, ViInt16 function, ViPReal64 reading);

ViStatus _VI_FUNC ri4152a_outpTtlt_M  (ViSession vi, ViInt16 ttltLine, ViBoolean ttltState);

ViStatus _VI_FUNC ri4152a_outpTtlt_M_Q  (ViSession vi, ViInt16 ttltLine, ViPBoolean ttltState);

ViStatus _VI_FUNC ri4152a_perAper  (ViSession vi, ViReal64 aperture);

ViStatus _VI_FUNC ri4152a_perAper_Q  (ViSession vi, ViPReal64 aperture);

ViStatus _VI_FUNC ri4152a_perVoltRang  (ViSession vi, ViBoolean autoRange, ViReal64 range);

ViStatus _VI_FUNC ri4152a_perVoltRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);

ViStatus _VI_FUNC ri4152a_read_Q  (ViSession vi, ViReal64 _VI_FAR readings[], ViPInt32 numReadings);

ViStatus _VI_FUNC ri4152a_resAper  (ViSession vi, ViReal64 aperture);

ViStatus _VI_FUNC ri4152a_resAper_Q  (ViSession vi, ViPReal64 aperture);

ViStatus _VI_FUNC ri4152a_resNplc  (ViSession vi, ViReal64 nplc);

ViStatus _VI_FUNC ri4152a_resNplc_Q  (ViSession vi, ViPReal64 nplc);

ViStatus _VI_FUNC ri4152a_resRang  (ViSession vi, ViBoolean autoRange, ViReal64 range);

ViStatus _VI_FUNC ri4152a_resRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);

ViStatus _VI_FUNC ri4152a_resRes  (ViSession vi, ViReal64 resolution);

ViStatus _VI_FUNC ri4152a_resRes_Q  (ViSession vi, ViPReal64 resolution);

ViStatus _VI_FUNC ri4152a_sampCoun  (ViSession vi, ViInt32 sampCoun);

ViStatus _VI_FUNC ri4152a_sampCoun_Q  (ViSession vi, ViPInt32 sampCoun);

ViStatus _VI_FUNC ri4152a_systLfr  (ViSession vi, ViInt16 systLfr);

ViStatus _VI_FUNC ri4152a_systLfr_Q  (ViSession vi, ViPInt16 systLfr);

ViStatus _VI_FUNC ri4152a_timedFetch_Q  (ViSession vi, ViInt32 timeOut, ViReal64 _VI_FAR readings[], ViPInt32 numReadings);

ViStatus _VI_FUNC ri4152a_trigCoun  (ViSession vi, ViInt32 trigCoun);

ViStatus _VI_FUNC ri4152a_trigCoun_Q  (ViSession vi, ViPInt32 trigCoun);

ViStatus _VI_FUNC ri4152a_trigDel  (ViSession vi, ViBoolean trigDelAuto, ViReal64 trigDel);

ViStatus _VI_FUNC ri4152a_trigDel_Q  (ViSession vi, ViPBoolean trigDelAuto, ViPReal64 trigDel);

ViStatus _VI_FUNC ri4152a_trigImm(ViSession vi);

ViStatus _VI_FUNC ri4152a_trigSour  (ViSession vi, ViInt16 trigSour);

ViStatus _VI_FUNC ri4152a_trigSour_Q  (ViSession vi, ViPInt16 trigSour);

ViStatus _VI_FUNC ri4152a_trigger  (ViSession vi, ViInt32 count, ViBoolean autoDelay, ViReal64 delay, ViInt16 source);

ViStatus _VI_FUNC ri4152a_trigger_Q  (ViSession vi, ViPInt32 count, ViPBoolean autoDelay, ViPReal64 delay, ViPInt16 trigSour);

ViStatus _VI_FUNC ri4152a_voltAcRang  (ViSession vi, ViBoolean autoRange, ViReal64 range);

ViStatus _VI_FUNC ri4152a_voltAcRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);

ViStatus _VI_FUNC ri4152a_voltAcRes  (ViSession vi, ViReal64 resolution);

ViStatus _VI_FUNC ri4152a_voltAcRes_Q  (ViSession vi, ViPReal64 resolution);

ViStatus _VI_FUNC ri4152a_voltDcAper  (ViSession vi, ViReal64 aperture);

ViStatus _VI_FUNC ri4152a_voltDcAper_Q  (ViSession vi, ViPReal64 aperture);

ViStatus _VI_FUNC ri4152a_voltDcNplc  (ViSession vi, ViReal64 nplc);

ViStatus _VI_FUNC ri4152a_voltDcNplc_Q  (ViSession vi, ViPReal64 nplc);

ViStatus _VI_FUNC ri4152a_voltDcRang  (ViSession vi, ViBoolean autoRange, ViReal64 range);

ViStatus _VI_FUNC ri4152a_voltDcRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);

ViStatus _VI_FUNC ri4152a_voltDcRes  (ViSession vi, ViReal64 resolution);

ViStatus _VI_FUNC ri4152a_voltDcRes_Q  (ViSession vi, ViPReal64 resolution);

ViStatus _VI_FUNC ri4152a_get_syst_vers (ViSession vi, ViPString version);

ViStatus _VI_FUNC ri4152a_cal_string (ViSession vi, ViChar string[]);

ViStatus _VI_FUNC ri4152a_get_cal_string (ViSession vi, ViPString result);

/* Used for "C" externs in C++ */
#if defined(__cplusplus) || defined(__cplusplus__)
}    /* end of "C" externs for C++ */
#endif 

#endif /* ri4152a_INCLUDE */

