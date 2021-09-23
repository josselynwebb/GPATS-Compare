#include <utility.h>
#include "ri4152a.h"

int module;
int status;


ViStatus _VI_FUNC (* ri_findRsrc) (
  ViInt16 arraySize,
  char rsrcList[][80],
  ViRsrc rsrcDesc,
  ViPInt16 matches );

ViStatus _VI_FUNC rix_findRsrc (
  ViInt16 arraySize,
  char rsrcList[][80],
  ViRsrc rsrcDesc,
  ViPInt16 matches ){   return 0; }

ViStatus _VI_FUNC (* ri_init) (
  ViRsrc InstrDesc,
  ViBoolean id_query,
  ViBoolean reset,
  ViPSession vi );

ViStatus _VI_FUNC rix_init (
  ViRsrc InstrDesc,
  ViBoolean id_query,
  ViBoolean reset,
  ViPSession vi ){   return 0; }

ViStatus _VI_FUNC (* ri_close) (
  ViSession vi );

ViStatus _VI_FUNC rix_close (
  ViSession vi ){  return 0; }

ViStatus _VI_FUNC (* ri_reset) (
  ViSession vi );

ViStatus _VI_FUNC rix_reset (
  ViSession vi ){  return 0; }

ViStatus _VI_FUNC (* ri_self_test) (
  ViSession vi,
  ViPInt16 test_result,
  ViString test_message);

ViStatus _VI_FUNC rix_self_test (
  ViSession vi,
  ViPInt16 test_result,
  ViString test_message){  return 0; }

ViStatus _VI_FUNC (* ri_error_query) (
  ViSession vi,
  ViPInt32 error,
  ViChar _VI_FAR error_message[]);

ViStatus _VI_FUNC rix_error_query (
  ViSession vi,
  ViPInt32 error,
  ViChar _VI_FAR error_message[]){  return 0; }

ViStatus _VI_FUNC (* ri_error_message) (
  ViSession vi,
  ViStatus error,
  ViChar _VI_FAR message[]);

ViStatus _VI_FUNC rix_error_message (
  ViSession vi,
  ViStatus error,
  ViChar _VI_FAR message[]){ return 0; }

ViStatus _VI_FUNC (* ri_revision_query) (
  ViSession vi,
  ViChar _VI_FAR driver_rev[],
  ViChar _VI_FAR instr_rev[]);

ViStatus _VI_FUNC rix_revision_query (
  ViSession vi,
  ViChar _VI_FAR driver_rev[],
  ViChar _VI_FAR instr_rev[]){  return 0; }

ViStatus _VI_FUNC (* ri_timeOut) (
  ViSession vi,
  ViInt32 timeOut);

ViStatus _VI_FUNC rix_timeOut (
  ViSession vi,
  ViInt32 timeOut){  return 0; }

ViStatus _VI_FUNC (* ri_timeOut_Q) (
  ViSession vi,
  ViPInt32 timeOut);

ViStatus _VI_FUNC rix_timeOut_Q (
  ViSession vi,
  ViPInt32 timeOut){  return 0; }

ViStatus _VI_FUNC (* ri_errorQueryDetect)(
  ViSession vi,
  ViBoolean errDetect);

ViStatus _VI_FUNC rix_errorQueryDetect(
  ViSession vi,
  ViBoolean errDetect){  return 0; }

ViStatus _VI_FUNC (* ri_errorQueryDetect_Q) (
  ViSession vi,
  ViPBoolean pErrDetect);

ViStatus _VI_FUNC rix_errorQueryDetect_Q(
  ViSession vi,
  ViPBoolean pErrDetect){  return 0; }




/* Prototypes
 */
ViStatus _VI_FUNC (* ri_statCond_Q)(
  ViSession     vi,
  ViInt32       happening,
  ViPBoolean    pCondition
);

ViStatus _VI_FUNC rix_statCond_Q(
  ViSession     vi,
  ViInt32       happening,
  ViPBoolean    pCondition
){  return 0; }

ViStatus _VI_FUNC (* ri_statEven_Q)(
  ViSession     vi,
  ViInt32       happening,
  ViPBoolean    pEvent
);

ViStatus _VI_FUNC rix_statEven_Q(
  ViSession     vi,
  ViInt32       happening,
  ViPBoolean    pEvent
){  return 0; }

ViStatus _VI_FUNC (* ri_statEvenClr) (
  ViSession vi
);

ViStatus _VI_FUNC rix_statEvenClr(
  ViSession vi
){  return 0; }

/* high level functions */

ViStatus _VI_FUNC (* ri_configure)(ViSession vi, ViInt16 func);
ViStatus _VI_FUNC rix_configure(ViSession vi, ViInt16 func){  return 0; }
ViStatus _VI_FUNC (* ri_measure_Q) (ViSession vi, ViInt16 func, ViPReal64 reading);
ViStatus _VI_FUNC rix_measure_Q(ViSession vi, ViInt16 func, ViPReal64 reading){  return 0; }


ViStatus _VI_FUNC (* ri_confTemp) (ViSession vi, ViInt16 trans);
ViStatus _VI_FUNC rix_confTemp(ViSession vi, ViInt16 trans){  return 0; }
ViStatus _VI_FUNC (* ri_measTemp_Q)(ViSession vi, ViInt16 trans, ViPReal64 reading);
ViStatus _VI_FUNC rix_measTemp_Q(ViSession vi, ViInt16 trans, ViPReal64 reading){  return 0; }

ViStatus _VI_FUNC (* ri_input) (ViSession vi, ViInt16 inpCoup,
                                 ViBoolean inpImpAuto, ViBoolean inpStat);
ViStatus _VI_FUNC rix_input(ViSession vi, ViInt16 inpCoup,
                                 ViBoolean inpImpAuto, ViBoolean inpStat){  return 0; }

ViStatus _VI_FUNC (* ri_input_Q) (ViSession vi, ViPInt16 inpCoup,
                                 ViPBoolean inpImpAuto, ViPBoolean inpStat);
ViStatus _VI_FUNC rix_input_Q (ViSession vi, ViPInt16 inpCoup,
                                 ViPBoolean inpImpAuto, ViPBoolean inpStat){  return 0; }

ViStatus _VI_FUNC (* ri_sample) (ViSession vi, ViInt32 sampCoun,
                                 ViInt16 sampSour, ViReal64 sampTim );
ViStatus _VI_FUNC rix_sample(ViSession vi, ViInt32 sampCoun,
                                 ViInt16 sampSour, ViReal64 sampTim ){  return 0; }

ViStatus _VI_FUNC (* ri_sample_Q)(ViSession vi, ViPInt32 sampCoun,
                                 ViPInt16 sampSour, ViPReal64 sampTim );
ViStatus _VI_FUNC rix_sample_Q(ViSession vi, ViPInt32 sampCoun,
                                 ViPInt16 sampSour, ViPReal64 sampTim ){  return 0; }

ViStatus _VI_FUNC (* ri_trigger) (ViSession vi, ViBoolean trigBuf,
                                  ViInt32 trigCoun, ViBoolean trigDelAuto,
                                  ViReal64 trigDel, ViInt16 trigSlop,
                                  ViInt16 trigSour);
ViStatus _VI_FUNC rix_trigger(ViSession vi, ViBoolean trigBuf,
                                  ViInt32 trigCoun, ViBoolean trigDelAuto,
                                  ViReal64 trigDel, ViInt16 trigSlop,
                                  ViInt16 trigSour){  return 0; }

ViStatus _VI_FUNC (* ri_trigger_Q) (ViSession vi, ViPBoolean trigBuf,
                                    ViPInt32 trigCoun, ViPBoolean trigDelAuto,
                                    ViPReal64 trigDel, ViPInt16 trigSlop,
                                    ViPInt16 trigSour);
ViStatus _VI_FUNC rix_trigger_Q(ViSession vi, ViPBoolean trigBuf,
                                    ViPInt32 trigCoun, ViPBoolean trigDelAuto,
                                    ViPReal64 trigDel, ViPInt16 trigSlop,
                                    ViPInt16 trigSour){  return 0; }


/* Instr Specific Functions follow */

ViStatus _VI_FUNC (* ri_abor) (ViSession vi);
ViStatus _VI_FUNC rix_abor (ViSession vi){  return 0; }

/* info for function rix_detBand */
ViStatus _VI_FUNC (* ri_detBand) (ViSession vi, ViReal64 detBand);
ViStatus _VI_FUNC rix_detBand (ViSession vi, ViReal64 detBand){  return 0; }

ViStatus _VI_FUNC (* ri_detBand_Q) (ViSession vi, ViPReal64 detBand);
ViStatus _VI_FUNC rix_detBand_Q (ViSession vi, ViPReal64 detBand){  return 0; }

/* info for function hp1410_calInt_Q */
ViStatus _VI_FUNC (* ri_calInt_Q) (ViSession vi, ViInt16 calType, ViPInt16 calInt);
ViStatus _VI_FUNC rix_calInt_Q (ViSession vi, ViInt16 calType, ViPInt16 calInt){  return 0; }

/* info for function rix_systLfr */
ViStatus _VI_FUNC (* ri_systLfr) (ViSession vi, ViInt16 systLfr);
ViStatus _VI_FUNC rix_systLfr (ViSession vi, ViInt16 systLfr){  return 0; }
ViStatus _VI_FUNC (* ri_systLfr_Q) (ViSession vi, ViPInt16 systLfr);
ViStatus _VI_FUNC rix_systLfr_Q (ViSession vi, ViPInt16 systLfr){  return 0; }

/* info for function rix_calLfr */
ViStatus _VI_FUNC (* ri_calLfr) (ViSession vi, ViInt16 calLfr);
ViStatus _VI_FUNC rix_calLfr (ViSession vi, ViInt16 calLfr){  return 0; }
ViStatus _VI_FUNC (* ri_calLfr_Q) (ViSession vi, ViPInt16 calLfr);
ViStatus _VI_FUNC rix_calLfr_Q (ViSession vi, ViPInt16 calLfr){  return 0; }

ViStatus _VI_FUNC (* ri_calNumb_Q) (ViSession vi, ViPInt16 calNumb);
ViStatus _VI_FUNC rix_calNumb_Q (ViSession vi, ViPInt16 calNumb){  return 0; }

/* Generic ON/OFF used by several routines */

/* info for function rix_calZeroAuto */
ViStatus _VI_FUNC (* ri_calZeroAuto) (ViSession vi, ViInt16 calZeroAuto);
ViStatus _VI_FUNC rix_calZeroAuto (ViSession vi, ViInt16 calZeroAuto){  return 0; }
ViStatus _VI_FUNC (* ri_calZeroAuto_Q) (ViSession vi, ViPInt16 calZeroAuto);
ViStatus _VI_FUNC rix_calZeroAuto_Q (ViSession vi, ViPInt16 calZeroAuto){  return 0; }

ViStatus _VI_FUNC (* ri_conf_Q) (ViSession vi, ViPInt16 confFunc,
                                  ViPBoolean confRangAuto, ViPReal64 confRange,
                                  ViPReal64 confRes, ViPInt16 trans);
ViStatus _VI_FUNC rix_conf_Q (ViSession vi, ViPInt16 confFunc,
                                  ViPBoolean confRangAuto, ViPReal64 confRange,
                                  ViPReal64 confRes, ViPInt16 trans){  return 0; }

/* info for function rix_confFreq */

ViStatus _VI_FUNC (* ri_confFreq) (ViSession vi);
ViStatus _VI_FUNC rix_confFreq (ViSession vi){  return 0; }

ViStatus _VI_FUNC (* ri_measFreq_Q) (ViSession vi, ViPReal64 reading);
ViStatus _VI_FUNC rix_measFreq_Q (ViSession vi, ViPReal64 reading){  return 0; }

ViStatus _VI_FUNC (* ri_confFres) (ViSession vi);
ViStatus _VI_FUNC rix_confFres (ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measFres_Q) ( ViSession vi, ViPReal64 reading);
ViStatus _VI_FUNC rix_measFres_Q( ViSession vi, ViPReal64 reading){  return 0; }

ViStatus _VI_FUNC (* ri_confPer) (ViSession vi);
ViStatus _VI_FUNC rix_confPer (ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measPer_Q) (ViSession vi, ViPReal64 reading);
ViStatus _VI_FUNC rix_measPer_Q (ViSession vi, ViPReal64 reading){  return 0; }


ViStatus _VI_FUNC (* ri_confRes)  (ViSession vi);
ViStatus _VI_FUNC rix_confRes (ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measRes_Q) ( ViSession vi, ViPReal64 reading);
ViStatus _VI_FUNC rix_measRes_Q( ViSession vi, ViPReal64 reading){  return 0; }


ViStatus _VI_FUNC (* ri_confVoltAc) ( ViSession vi);
ViStatus _VI_FUNC rix_confVoltAc( ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measVoltAc_Q) ( ViSession vi, ViPReal64 reading );
ViStatus _VI_FUNC rix_measVoltAc_Q( ViSession vi, ViPReal64 reading ){  return 0; }

ViStatus _VI_FUNC (* ri_confVoltAcDc)( ViSession vi);
ViStatus _VI_FUNC rix_confVoltAcDc( ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measVoltAcDc_Q) ( ViSession vi, ViPReal64 reading );
ViStatus _VI_FUNC rix_measVoltAcDc_Q( ViSession vi, ViPReal64 reading ){  return 0; }

ViStatus _VI_FUNC (* ri_confVoltDc)( ViSession vi);
ViStatus _VI_FUNC rix_confVoltDc( ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measVoltDc_Q) ( ViSession vi, ViPReal64 reading );
ViStatus _VI_FUNC rix_measVoltDc_Q( ViSession vi, ViPReal64 reading ){  return 0; }

ViStatus _VI_FUNC (* ri_confCurrAc) ( ViSession vi);
ViStatus _VI_FUNC rix_confCurrAc( ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measCurrAc_Q) ( ViSession vi, ViPReal64 reading );
ViStatus _VI_FUNC rix_measCurrAc_Q( ViSession vi, ViPReal64 reading ){  return 0; }

ViStatus _VI_FUNC (* ri_confCurrDc)( ViSession vi);
ViStatus _VI_FUNC rix_confCurrDc( ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measCurrDc_Q) ( ViSession vi, ViPReal64 reading );
ViStatus _VI_FUNC rix_measCurrDc_Q( ViSession vi, ViPReal64 reading ){  return 0; }

ViStatus _VI_FUNC (* ri_confVoltDcRat)( ViSession vi);
ViStatus _VI_FUNC rix_confVoltDcRat( ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_measVoltDcRat_Q) ( ViSession vi, ViPReal64 reading );
ViStatus _VI_FUNC rix_measVoltDcRat_Q( ViSession vi, ViPReal64 reading ){  return 0; }


ViStatus _VI_FUNC (* ri_fetc_Q) (ViSession vi, ViReal64 _VI_FAR readings[],
                                  ViPInt32 numReadings );
ViStatus _VI_FUNC rix_fetc_Q (ViSession vi, ViReal64 _VI_FAR readings[],
                                  ViPInt32 numReadings ){  return 0; }

ViStatus _VI_FUNC (* ri_timedFetch_Q)  (ViSession vi, ViInt32 timeOut,
                                        ViReal64 _VI_FAR readings[],
                                        ViPInt32 numReadings );
ViStatus _VI_FUNC rix_timedFetch_Q (ViSession vi, ViInt32 timeOut,
                                        ViReal64 _VI_FAR readings[],
                                        ViPInt32 numReadings ){  return 0; }

ViStatus _VI_FUNC (* ri_readStatusByte) (ViSession vi, ViPInt16 statusByte);
ViStatus _VI_FUNC rix_readStatusByte (ViSession vi, ViPInt16 statusByte){  return 0; }

/* info for function rix_func */
ViStatus _VI_FUNC (* ri_func) (ViSession vi, ViInt16 func);
ViStatus _VI_FUNC rix_func (ViSession vi, ViInt16 func){  return 0; }

ViStatus _VI_FUNC (* ri_func_Q)  (ViSession vi, ViPInt16 func);
ViStatus _VI_FUNC rix_func_Q (ViSession vi, ViPInt16 func){  return 0; }

ViStatus _VI_FUNC (* ri_initImm) (ViSession vi);
ViStatus _VI_FUNC rix_initImm (ViSession vi){  return 0; }

/* info for function rix_inpCoup */
ViStatus _VI_FUNC (* ri_inpCoup) (ViSession vi, ViInt16 inpCoup);
ViStatus _VI_FUNC rix_inpCoup (ViSession vi, ViInt16 inpCoup){  return 0; }

ViStatus _VI_FUNC (* ri_inpCoup_Q) (ViSession vi, ViPInt16 inpCoup);
ViStatus _VI_FUNC rix_inpCoup_Q (ViSession vi, ViPInt16 inpCoup){  return 0; }

/* info for function rix_inpImpAuto */
ViStatus _VI_FUNC (* ri_inpImpAuto) (ViSession vi, ViBoolean inpImpAuto);
ViStatus _VI_FUNC rix_inpImpAuto (ViSession vi, ViBoolean inpImpAuto){  return 0; }

ViStatus _VI_FUNC (* ri_inpImpAuto_Q) (ViSession vi, ViPBoolean inpImpAuto);
ViStatus _VI_FUNC rix_inpImpAuto_Q (ViSession vi, ViPBoolean inpImpAuto){  return 0; }

/* info for function rix_inpStat */
ViStatus _VI_FUNC (* ri_inpStat) (ViSession vi, ViBoolean inpStat);
ViStatus _VI_FUNC rix_inpStat (ViSession vi, ViBoolean inpStat){  return 0; }
ViStatus _VI_FUNC (* ri_inpStat_Q) (ViSession vi, ViPBoolean inpStat);
ViStatus _VI_FUNC rix_inpStat_Q (ViSession vi, ViPBoolean inpStat){  return 0; }

/* info for function rix_memVmeAddr */
ViStatus _VI_FUNC (* ri_memVmeAddr) (ViSession vi, ViInt32 memVmeAddr);
ViStatus _VI_FUNC rix_memVmeAddr (ViSession vi, ViInt32 memVmeAddr){  return 0; }
ViStatus _VI_FUNC (* ri_memVmeAddr_Q) (ViSession vi, ViPInt32 memVmeAddr);
ViStatus _VI_FUNC rix_memVmeAddr_Q (ViSession vi, ViPInt32 memVmeAddr){  return 0; }

/* info for function rix_memVmeSize */
ViStatus _VI_FUNC (* ri_memVmeSize) (ViSession vi, ViInt32 memVmeSize);
ViStatus _VI_FUNC rix_memVmeSize (ViSession vi, ViInt32 memVmeSize){  return 0; }
ViStatus _VI_FUNC (* ri_memVmeSize_Q) (ViSession vi, ViPInt32 memVmeSize);
ViStatus _VI_FUNC rix_memVmeSize_Q (ViSession vi, ViPInt32 memVmeSize){  return 0; }

/* info for function rix_memVmeStat */
ViStatus _VI_FUNC (* ri_memVmeStat) (ViSession vi, ViBoolean memVmeStat);
ViStatus _VI_FUNC rix_memVmeStat (ViSession vi, ViBoolean memVmeStat){  return 0; }
ViStatus _VI_FUNC (* ri_memVmeStat_Q) (ViSession vi, ViPBoolean memVmeStat);
ViStatus _VI_FUNC rix_memVmeStat_Q (ViSession vi, ViPBoolean memVmeStat){  return 0; }

/* info for function rix_outpTtlt */

ViStatus _VI_FUNC (* ri_outpTtlt) (ViSession vi, ViInt16 ttltLine, ViBoolean ttltStat);
ViStatus _VI_FUNC rix_outpTtlt (ViSession vi, ViInt16 ttltLine, ViBoolean ttltStat){  return 0; }
ViStatus _VI_FUNC (* ri_outpTtlt_Q) (ViSession vi, ViInt16 ttltLine, ViPBoolean ttltStat);
ViStatus _VI_FUNC rix_outpTtlt_Q (ViSession vi, ViInt16 ttltLine, ViPBoolean ttltStat){  return 0; }

ViStatus _VI_FUNC (* ri_read_Q) (ViSession vi, ViReal64 _VI_FAR readings[]);
ViStatus _VI_FUNC rix_read_Q (ViSession vi, ViReal64 _VI_FAR readings[]){  return 0; }

/* info for function rix_resAper */
ViStatus _VI_FUNC (* ri_resAper) (ViSession vi, ViReal64 resAper);
ViStatus _VI_FUNC rix_resAper (ViSession vi, ViReal64 resAper){  return 0; }
ViStatus _VI_FUNC (* ri_resAper_Q) (ViSession vi, ViPReal64 resAper);
ViStatus _VI_FUNC rix_resAper_Q (ViSession vi, ViPReal64 resAper){  return 0; }

/* info for function rix_resNplc */
ViStatus _VI_FUNC (* ri_resNplc) (ViSession vi, ViReal64 resNplc);
ViStatus _VI_FUNC rix_resNplc (ViSession vi, ViReal64 resNplc){  return 0; }
ViStatus _VI_FUNC (* ri_resNplc_Q) (ViSession vi, ViPReal64 resNplc);
ViStatus _VI_FUNC rix_resNplc_Q (ViSession vi, ViPReal64 resNplc){  return 0; }

/* info for function rix_resOcom */
ViStatus _VI_FUNC (* ri_resOcom) (ViSession vi, ViBoolean resOcom);
ViStatus _VI_FUNC rix_resOcom (ViSession vi, ViBoolean resOcom){  return 0; }
ViStatus _VI_FUNC (* ri_resOcom_Q) (ViSession vi, ViPBoolean resOcom);
ViStatus _VI_FUNC rix_resOcom_Q (ViSession vi, ViPBoolean resOcom){  return 0; }

/* info for function rix_resRang */
ViStatus _VI_FUNC (* ri_resRang) (ViSession vi, ViBoolean resRangAuto, ViReal64 resRang);
ViStatus _VI_FUNC rix_resRang (ViSession vi, ViBoolean resRangAuto, ViReal64 resRang){  return 0; }
ViStatus _VI_FUNC (* ri_resRang_Q) (ViSession vi, ViPBoolean resRangAuto, ViPReal64 resRang);
ViStatus _VI_FUNC rix_resRang_Q (ViSession vi, ViPBoolean resRangAuto, ViPReal64 resRang){  return 0; }

/* info for function rix_resRes */
ViStatus _VI_FUNC (* ri_resRes) (ViSession vi, ViReal64 resRes);
ViStatus _VI_FUNC rix_resRes (ViSession vi, ViReal64 resRes){  return 0; }
ViStatus _VI_FUNC (* ri_resRes_Q) (ViSession vi, ViPReal64 resRes);
ViStatus _VI_FUNC rix_resRes_Q (ViSession vi, ViPReal64 resRes){  return 0; }

/* info for function rix_freqRang */
ViStatus _VI_FUNC (* ri_freqRang_Q) (ViSession vi, ViPBoolean freqRangAuto, ViPReal64 freqRang);
ViStatus _VI_FUNC rix_freqRang_Q (ViSession vi, ViPBoolean freqRangAuto, ViPReal64 freqRang){  return 0; }

/* info for function rix_resAper */
ViStatus _VI_FUNC (* ri_freqAper) (ViSession vi, ViReal64 resAper);
ViStatus _VI_FUNC rix_freqAper (ViSession vi, ViReal64 resAper){  return 0; }
ViStatus _VI_FUNC (* ri_freqAper_Q) (ViSession vi, ViPReal64 resAper);
ViStatus _VI_FUNC rix_freqAper_Q (ViSession vi, ViPReal64 resAper){  return 0; }

/* info for function rix_perRang */
ViStatus _VI_FUNC (* ri_perRang_Q) (ViSession vi, ViPBoolean freqRangAuto, ViPReal64 freqRang);
ViStatus _VI_FUNC rix_perRang_Q (ViSession vi, ViPBoolean freqRangAuto, ViPReal64 freqRang){  return 0; }

/* info for function rix_perAper */
ViStatus _VI_FUNC (* ri_perAper) (ViSession vi, ViReal64 resAper);
ViStatus _VI_FUNC rix_perAper (ViSession vi, ViReal64 resAper){  return 0; }
ViStatus _VI_FUNC (* ri_perAper_Q) (ViSession vi, ViPReal64 resAper);
ViStatus _VI_FUNC rix_perAper_Q (ViSession vi, ViPReal64 resAper){  return 0; }


/* info for function rix_sampCoun */
ViStatus _VI_FUNC (* ri_sampCoun) (ViSession vi, ViInt32 sampCoun);
ViStatus _VI_FUNC rix_sampCoun (ViSession vi, ViInt32 sampCoun){ return 0; }
ViStatus _VI_FUNC (* ri_sampCoun_Q) (ViSession vi, ViPInt32 sampCoun);
ViStatus _VI_FUNC rix_sampCoun_Q (ViSession vi, ViPInt32 sampCoun){  return 0; }

/* info for function rix_sampSour */
ViStatus _VI_FUNC (* ri_sampSour) (ViSession vi, ViInt16 sampSour);
ViStatus _VI_FUNC rix_sampSour (ViSession vi, ViInt16 sampSour){ return 0; }
ViStatus _VI_FUNC (* ri_sampSour_Q) (ViSession vi, ViPInt16 sampSour);
ViStatus _VI_FUNC rix_sampSour_Q (ViSession vi, ViPInt16 sampSour){  return 0; }

/* info for function rix_sampTim */
ViStatus _VI_FUNC (* ri_sampTim) (ViSession vi, ViReal64 sampTim);
ViStatus _VI_FUNC rix_sampTim (ViSession vi, ViReal64 sampTim){ return 0; }
ViStatus _VI_FUNC (* ri_sampTim_Q) (ViSession vi, ViPReal64 sampTim);
ViStatus _VI_FUNC rix_sampTim_Q (ViSession vi, ViPReal64 sampTim){  return 0; }

/* info for function rix_trigBuff */
ViStatus _VI_FUNC (* ri_trigBuff) (ViSession vi, ViInt16 trigBuff);
ViStatus _VI_FUNC rix_trigBuff (ViSession vi, ViInt16 trigBuff){ return 0; }
ViStatus _VI_FUNC (* ri_trigBuff_Q) (ViSession vi, ViPInt16 trigBuff);
ViStatus _VI_FUNC rix_trigBuff_Q (ViSession vi, ViPInt16 trigBuff){  return 0; }

/* info for function rix_trigCoun */
ViStatus _VI_FUNC (* ri_trigCoun) (ViSession vi, ViInt32 trigCoun);
ViStatus _VI_FUNC rix_trigCoun (ViSession vi, ViInt32 trigCoun){ return 0; }
ViStatus _VI_FUNC (* ri_trigCoun_Q) (ViSession vi, ViPInt32 trigCoun);
ViStatus _VI_FUNC rix_trigCoun_Q (ViSession vi, ViPInt32 trigCoun){  return 0; }

/* info for function rix_trigDel */
ViStatus _VI_FUNC (* ri_trigDel) (ViSession vi, ViBoolean trigDelAuto,
                                   ViReal64 trigDel);
ViStatus _VI_FUNC rix_trigDel (ViSession vi, ViBoolean trigDelAuto,
                                   ViReal64 trigDel){ return 0; }
ViStatus _VI_FUNC (* ri_trigDel_Q) (ViSession vi, ViPBoolean trigDelAuto,
                                     ViPReal64 trigDel);
ViStatus _VI_FUNC rix_trigDel_Q (ViSession vi, ViPBoolean trigDelAuto,
                                     ViPReal64 trigDel){  return 0; }

ViStatus _VI_FUNC (* ri_trigImm) (ViSession vi);
ViStatus _VI_FUNC rix_trigImm (ViSession vi){ return 0;}

/* info for function rix_trigSlop */
ViStatus _VI_FUNC (* ri_trigSlop) (ViSession vi, ViInt16 trigSlop);
ViStatus _VI_FUNC rix_trigSlop (ViSession vi, ViInt16 trigSlop){  return 0; }
ViStatus _VI_FUNC (* ri_trigSlop_Q) (ViSession vi, ViPInt16 trigSlop);
ViStatus _VI_FUNC rix_trigSlop_Q (ViSession vi, ViPInt16 trigSlop){ return 0; }

/* info for function rix_trigSour */
ViStatus _VI_FUNC (* ri_trigSour) (ViSession vi, ViInt16 trigSour);
ViStatus _VI_FUNC rix_trigSour (ViSession vi, ViInt16 trigSour){  return 0; }
ViStatus _VI_FUNC (* ri_trigSour_Q) (ViSession vi, ViPInt16 trigSour);
ViStatus _VI_FUNC rix_trigSour_Q (ViSession vi, ViPInt16 trigSour){ return 0; }

/* info for function rix_voltAcAper */
ViStatus _VI_FUNC (* ri_voltAcAper) (ViSession vi, ViReal64 voltAcAper);
ViStatus _VI_FUNC rix_voltAcAper (ViSession vi, ViReal64 voltAcAper){  return 0; }
ViStatus _VI_FUNC (* ri_voltAcAper_Q) (ViSession vi, ViPReal64 voltAcAper);
ViStatus _VI_FUNC rix_voltAcAper_Q (ViSession vi, ViPReal64 voltAcAper){ return 0; }

/* info for function rix_voltAcNplc */
ViStatus _VI_FUNC (* ri_voltAcNplc) (ViSession vi, ViReal64 voltAcNplc);
ViStatus _VI_FUNC rix_voltAcNplc (ViSession vi, ViReal64 voltAcNplc){  return 0; }

ViStatus _VI_FUNC (* ri_voltAcNplc_Q) (ViSession vi, ViPReal64 voltAcNplc);
ViStatus _VI_FUNC rix_voltAcNplc_Q (ViSession vi, ViPReal64 voltAcNplc){ return 0; }

/* info for function rix_voltAcRang */
/* This is used by both Ac and Dc range functions*/
ViStatus _VI_FUNC (* ri_voltAcRang) (ViSession vi, ViBoolean voltRangAuto,
                                      ViReal64 voltRang);
ViStatus _VI_FUNC rix_voltAcRang (ViSession vi, ViBoolean voltRangAuto,
                                      ViReal64 voltRang){  return 0; }
ViStatus _VI_FUNC (* ri_voltAcRang_Q) (ViSession vi, ViPBoolean voltRangAuto,
                                        ViPReal64 voltRang);
ViStatus _VI_FUNC rix_voltAcRang_Q (ViSession vi, ViPBoolean voltRangAuto,
                                        ViPReal64 voltRang){ return 0; }

ViStatus _VI_FUNC (* ri_voltAcRes) (ViSession vi, ViReal64 voltAcRes);
ViStatus _VI_FUNC rix_voltAcRes (ViSession vi, ViReal64 voltAcRes){  return 0; }

ViStatus _VI_FUNC (* ri_voltAcRes_Q) (ViSession vi, ViPReal64 voltAcRes);
ViStatus _VI_FUNC rix_voltAcRes_Q (ViSession vi, ViPReal64 voltAcRes){ return 0; }

/* info for function rix_voltDcAper */
ViStatus _VI_FUNC (* ri_voltDcAper) (ViSession vi, ViReal64 voltDcAper);
ViStatus _VI_FUNC rix_voltDcAper (ViSession vi, ViReal64 voltDcAper){  return 0; }
ViStatus _VI_FUNC (* ri_voltDcAper_Q) (ViSession vi, ViPReal64 voltDcAper);
ViStatus _VI_FUNC rix_voltDcAper_Q (ViSession vi, ViPReal64 voltDcAper){ return 0; }

/* info for function rix_voltDcNplc */
ViStatus _VI_FUNC (* ri_voltDcNplc) (ViSession vi, ViReal64 voltDcNplc);
ViStatus _VI_FUNC rix_voltDcNplc (ViSession vi, ViReal64 voltDcNplc){  return 0; }
ViStatus _VI_FUNC (* ri_voltDcNplc_Q) (ViSession vi, ViPReal64 voltDcNplc);
ViStatus _VI_FUNC rix_voltDcNplc_Q (ViSession vi, ViPReal64 voltDcNplc){ return 0; }

/* info for function rix_voltDcRang */
ViStatus _VI_FUNC (* ri_voltDcRang) (ViSession vi, ViBoolean voltRangAuto, ViReal64 voltDcRang);
ViStatus _VI_FUNC rix_voltDcRang (ViSession vi, ViBoolean voltRangAuto, ViReal64 voltDcRang){  return 0; }
ViStatus _VI_FUNC (* ri_voltDcRang_Q) (ViSession vi, ViPBoolean voltRangAuto, ViPReal64 voltDcRang);
ViStatus _VI_FUNC rix_voltDcRang_Q (ViSession vi, ViPBoolean voltRangAuto, ViPReal64 voltDcRang){ return 0; }

ViStatus _VI_FUNC (* ri_voltDcRes) (ViSession vi, ViReal64 voltDcRes);
ViStatus _VI_FUNC rix_voltDcRes (ViSession vi, ViReal64 voltDcRes){  return 0; }

ViStatus _VI_FUNC (* ri_voltDcRes_Q) (ViSession vi, ViPReal64 voltDcRes);
ViStatus _VI_FUNC rix_voltDcRes_Q (ViSession vi, ViPReal64 voltDcRes){ return 0; }




/* info for function rix_currDcAper */
ViStatus _VI_FUNC (* ri_currDcAper) (ViSession vi, ViReal64 currDcAper);
ViStatus _VI_FUNC rix_currDcAper (ViSession vi, ViReal64 currDcAper){  return 0; }
ViStatus _VI_FUNC (* ri_currDcAper_Q) (ViSession vi, ViPReal64 currDcAper);
ViStatus _VI_FUNC rix_currDcAper_Q (ViSession vi, ViPReal64 currDcAper){ return 0; }

/* info for function rix_currDcNplc */
ViStatus _VI_FUNC (* ri_currDcNplc) (ViSession vi, ViReal64 currDcNplc);
ViStatus _VI_FUNC rix_currDcNplc (ViSession vi, ViReal64 currDcNplc){  return 0; }
ViStatus _VI_FUNC (* ri_currDcNplc_Q) (ViSession vi, ViPReal64 currDcNplc);
ViStatus _VI_FUNC rix_currDcNplc_Q (ViSession vi, ViPReal64 currDcNplc){ return 0; }

/* info for function rix_currDcRang */
ViStatus _VI_FUNC (* ri_currDcRang) (ViSession vi, ViBoolean voltRangAuto, ViReal64 currDcRang);
ViStatus _VI_FUNC rix_currDcRang (ViSession vi, ViBoolean voltRangAuto, ViReal64 voltDcRang){  return 0; }
ViStatus _VI_FUNC (* ri_currDcRang_Q) (ViSession vi, ViPBoolean currRangAuto, ViPReal64 currDcRang);
ViStatus _VI_FUNC rix_currDcRang_Q (ViSession vi, ViPBoolean currRangAuto, ViPReal64 currDcRang){ return 0; }

ViStatus _VI_FUNC (* ri_currDcRes) (ViSession vi, ViReal64 currDcRes);
ViStatus _VI_FUNC rix_currDcRes (ViSession vi, ViReal64 currDcRes){  return 0; }

ViStatus _VI_FUNC (* ri_currDcRes_Q) (ViSession vi, ViPReal64 currDcRes);
ViStatus _VI_FUNC rix_currDcRes_Q (ViSession vi, ViPReal64 currDcRes){ return 0; }



/* info for function rix_currAcRang */
ViStatus _VI_FUNC (* ri_currAcRang) (ViSession vi, ViBoolean currRangAuto, ViReal64 currDcRang);
ViStatus _VI_FUNC rix_currAcRang (ViSession vi, ViBoolean currRangAuto, ViReal64 voltDcRang){  return 0; }
ViStatus _VI_FUNC (* ri_currAcRang_Q) (ViSession vi, ViPBoolean currRangAuto, ViPReal64 currDcRang);
ViStatus _VI_FUNC rix_currAcRang_Q (ViSession vi, ViPBoolean currRangAuto, ViPReal64 currDcRang){ return 0; }

ViStatus _VI_FUNC (* ri_currAcRes) (ViSession vi, ViReal64 currAcRes);
ViStatus _VI_FUNC rix_currAcRes (ViSession vi, ViReal64 currAcRes){  return 0; }

ViStatus _VI_FUNC (* ri_currAcRes_Q) (ViSession vi, ViPReal64 currAcRes);
ViStatus _VI_FUNC rix_currAcRes_Q (ViSession vi, ViPReal64 currAcRes){ return 0; }

/* ------------------  IEEE common commands --------------------------- */

ViStatus _VI_FUNC (* ri_opc) (ViSession vi);
ViStatus _VI_FUNC rix_opc (ViSession vi){  return 0; }
ViStatus _VI_FUNC (* ri_opc_Q) (ViSession vi, ViPInt16 opc);
ViStatus _VI_FUNC rix_opc_Q (ViSession vi, ViPInt16 opc){  return 0; }

ViStatus _VI_FUNC (* ri_dcl) (ViSession vi);
ViStatus _VI_FUNC rix_dcl (ViSession vi){  return 0; }

ViStatus _VI_FUNC (* ri_trg) (ViSession vi);
ViStatus _VI_FUNC rix_trg (ViSession vi){  return 0; }

ViStatus _VI_FUNC (* ri_wai) (ViSession vi);
ViStatus _VI_FUNC rix_wai (ViSession vi){  return 0; }

ViStatus _VI_FUNC GetStub(ViObject a, ViAttr b, ViPAttrState c) {return 0; }


ViStatus _VI_FUNC (* ri_freqVoltRang)  (ViSession vi, ViBoolean autoRange, ViReal64 range);
ViStatus _VI_FUNC rix_freqVoltRang  (ViSession vi, ViBoolean autoRange, ViReal64 range) { return 0; }


ViStatus _VI_FUNC (* ri_freqVoltRang_Q)  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);
ViStatus _VI_FUNC rix_freqVoltRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range) { return 0; }


ViStatus _VI_FUNC (* ri_perVoltRang)  (ViSession vi, ViBoolean autoRange, ViReal64 range);
ViStatus _VI_FUNC rix_perVoltRang  (ViSession vi, ViBoolean autoRange, ViReal64 range) { return 0; }

ViStatus _VI_FUNC (* ri_perVoltRang_Q)  (ViSession vi, ViPBoolean autoRange, ViPReal64 range);
ViStatus _VI_FUNC rix_perVoltRang_Q  (ViSession vi, ViPBoolean autoRange, ViPReal64 range) { return 0; }

ViStatus _VI_FUNC (* ri_calcAverAver_Q)  (ViSession vi, ViPReal64 average);
ViStatus _VI_FUNC rix_calcAverAver_Q  (ViSession vi, ViPReal64 average){ return 0;}

ViStatus _VI_FUNC (* ri_calcAverCoun_Q)  (ViSession vi, ViPInt32 points);
ViStatus _VI_FUNC rix_calcAverCoun_Q  (ViSession vi, ViPInt32 points){ return 0;}

ViStatus _VI_FUNC (* ri_calcAverMax_Q ) (ViSession vi, ViPReal64 maxValue);
ViStatus _VI_FUNC rix_calcAverMax_Q  (ViSession vi, ViPReal64 maxValue){ return 0;}

ViStatus _VI_FUNC (* ri_calcAverMin_Q ) (ViSession vi, ViPReal64 minValue);
ViStatus _VI_FUNC rix_calcAverMin_Q  (ViSession vi, ViPReal64 minValue){ return 0;}

ViStatus _VI_FUNC (* ri_calcDbRef ) (ViSession vi, ViReal64 calcDbRef);
ViStatus _VI_FUNC rix_calcDbRef  (ViSession vi, ViReal64 calcDbRef){ return 0;}

ViStatus _VI_FUNC (* ri_calcDbRef_Q)  (ViSession vi, ViPReal64 calcDbRef);
ViStatus _VI_FUNC rix_calcDbRef_Q  (ViSession vi, ViPReal64 calcDbRef){ return 0;}

ViStatus _VI_FUNC (* ri_calcDbmRef)  (ViSession vi, ViReal64 calcDbmRef);
ViStatus _VI_FUNC rix_calcDbmRef  (ViSession vi, ViReal64 calcDbmRef){ return 0;}

ViStatus _VI_FUNC (* ri_calcDbmRef_Q)  (ViSession vi, ViPReal64 calcDbmRef);
ViStatus _VI_FUNC rix_calcDbmRef_Q  (ViSession vi, ViPReal64 calcDbmRef){ return 0;}

ViStatus _VI_FUNC (* ri_calcFunc)  (ViSession vi, ViInt16 calcFunc);
ViStatus _VI_FUNC rix_calcFunc  (ViSession vi, ViInt16 calcFunc){ return 0;}

ViStatus _VI_FUNC (* ri_calcFunc_Q ) (ViSession vi, ViPInt16 calcFunc);
ViStatus _VI_FUNC rix_calcFunc_Q  (ViSession vi, ViPInt16 calcFunc){ return 0;}

ViStatus _VI_FUNC (* ri_calcLimLow)  (ViSession vi, ViReal64 calcLimLow);
ViStatus _VI_FUNC rix_calcLimLow  (ViSession vi, ViReal64 calcLimLow){ return 0;}

ViStatus _VI_FUNC (* ri_calcLimLow_Q)  (ViSession vi, ViPReal64 calcLimLow);
ViStatus _VI_FUNC rix_calcLimLow_Q  (ViSession vi, ViPReal64 calcLimLow){ return 0;}

ViStatus _VI_FUNC (* ri_calcLimUpp)  (ViSession vi, ViReal64 calcLimUpp);
ViStatus _VI_FUNC rix_calcLimUpp  (ViSession vi, ViReal64 calcLimUpp){ return 0;}

ViStatus _VI_FUNC (* ri_calcLimUpp_Q ) (ViSession vi, ViPReal64 calcLimUpp);
ViStatus _VI_FUNC rix_calcLimUpp_Q  (ViSession vi, ViPReal64 calcLimUpp){ return 0;}

ViStatus _VI_FUNC (* ri_calcNullOffs ) (ViSession vi, ViReal64 calcNullOffs);
ViStatus _VI_FUNC rix_calcNullOffs  (ViSession vi, ViReal64 calcNullOffs){ return 0;}

ViStatus _VI_FUNC (* ri_calcNullOffs_Q ) (ViSession vi, ViPReal64 calcNullOffs);
ViStatus _VI_FUNC rix_calcNullOffs_Q  (ViSession vi, ViPReal64 calcNullOffs){ return 0;}

ViStatus _VI_FUNC (* ri_calcStat )  (ViSession vi, ViBoolean calcStat);
ViStatus _VI_FUNC rix_calcStat  (ViSession vi, ViBoolean calcStat){ return 0;}

ViStatus _VI_FUNC (* ri_calcStat_Q )  (ViSession vi, ViPBoolean calcStat);
ViStatus _VI_FUNC rix_calcStat_Q  (ViSession vi, ViPBoolean calcStat){ return 0;}

