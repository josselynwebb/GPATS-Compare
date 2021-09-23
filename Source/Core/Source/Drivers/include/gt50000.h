
#ifndef __gt50000_HEADER
#define __gt50000_HEADER

#include <vpptype.h>
     
#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif

/***************************************************************/
/* KSI Inc. Modifications for rev 1.11
/* Add set status byte mask capabilities (sep[erate panel)
/* Add frequemcy list mode (seperate panel)
/* Add full functions to self test (seperate panel)
/* Add fast and scan mode for AM modulation
/*****************************************************************************/
/*= Instrument Specific Logical Address =========================================*/
/*****************************************************************************/
#define	SYNTHESIZER_LOGICAL_ADDRESS	2

/*****************************************************************************/
/*= Instrument Specific Error Codes =========================================*/
/*****************************************************************************/
#define VI_INSTR_ERROR_OFFSET           (_VI_ERROR+0x3FFC0900L)
#define GT50000_ERROR_FILE_OPEN 		(_VI_ERROR+0x3FFC0800L)
#define GT50000_ERROR_FILE_WRITE        (_VI_ERROR+0x3FFC0801L)
#define GT50000_ERROR_FILE_READ	        (_VI_ERROR+0x3FFC0802L)
#define GT50000_ERROR_INVALID_TABLE     (_VI_ERROR+0x3FFC0803L)
#define GT50000_ERROR_INV_CORRECTIONS   (_VI_ERROR+0x3FFC0804L)
#define GT50000_ERROR_INV_FREQUENCY     (_VI_ERROR+0x3FFC0805L)

/*****************************************************************************/
/*= GLOBAL USER-CALLABLE FUNCTION DECLARATIONS (Exportable Functions) =======*/
/*****************************************************************************/
ViStatus _VI_FUNC gt50000_init (ViRsrc resourceName, ViBoolean IDQuery, ViBoolean resetDevice, 
								ViPSession instrSession);
ViStatus _VI_FUNC gt50000_writeInstrData (ViSession instrSession, ViString writeBuffer);
ViStatus _VI_FUNC gt50000_readInstrData (ViSession instrSession, ViUInt16 numberBytesToRead, 
										 ViPBuf readBuffer, ViPUInt32 numBytesRead);
ViStatus _VI_FUNC gt50000_languageSyntax (ViSession instrSession, ViUInt16 syntax);
ViStatus _VI_FUNC gt50000_reset (ViSession instrSession);
ViStatus _VI_FUNC gt50000_selfTest (ViSession instrSession, ViBoolean stc,ViPInt16 selfTestResult, 
									ViChar _VI_FAR selfTestMessage[],ViReal64 _VI_FAR *test_start,
									ViReal64 _VI_FAR *test_stop,ViReal64 _VI_FAR *test_step,
ViReal64 _VI_FAR *fine_pwr,ViReal64 _VI_FAR *crs_pwr,ViReal64 _VI_FAR *failed_freq);
//**************************************************************
ViStatus _VI_FUNC gt50000_test_freq (ViSession instrSession, ViReal64 test_start,ViReal64 test_stop,
									 ViReal64 test_step, ViReal64 fine_pwr, ViReal64 crs_pwr);

ViStatus _VI_FUNC gt50000_test_abort (ViSession instrSession);
ViStatus _VI_FUNC gt50000_errorQuery (ViSession instrSession, ViPInt16 errorCode, 
									  ViChar _VI_FAR errorMessage[]);
ViStatus _VI_FUNC gt50000_errorMessage (ViSession instrSession, ViStatus errorCode, 
										ViChar _VI_FAR errorMessage[]);
ViStatus _VI_FUNC gt50000_revisionQuery (ViSession instrSession, ViChar _VI_FAR driverRevision[], 
										 ViChar _VI_FAR firmwareRevision[]);
ViStatus _VI_FUNC gt50000_close (ViSession instrSession);

ViStatus _VI_FUNC gt50000_confExternMod(ViSession instrSession, ViUInt16 am, ViUInt16 fm, 
										ViUInt16 pm,ViInt16 ot_mod);
ViStatus _VI_FUNC gt50000_confRFOutput(ViSession instrSession, ViUInt16 rf);
ViStatus _VI_FUNC gt50000_confLeveled(ViSession instrSession, ViUInt16 ul);
ViStatus _VI_FUNC gt50000_confStepAtten(ViSession instrSession, ViUInt16 sa);
/*************************************/
ViStatus _VI_FUNC gt50000_freqSweep(ViSession instrSession, ViReal64 freq, ViReal64 stop,
								   ViReal64 step, ViReal64 dwell, ViInt16 mode,ViInt16 trig_mode);

ViStatus _VI_FUNC gt50000_freqSweepHaltResume(ViSession instrSession, ViUInt16 mode);
ViStatus _VI_FUNC gt50000_setFreq(ViSession instrSession, ViReal64 freq);
ViStatus _VI_FUNC gt50000_getFreq(ViSession instrSession, ViPReal64 freq);
ViStatus _VI_FUNC gt50000_setPower(ViSession instrSession, ViReal64 power);
/* Rev 1.11mods */
ViStatus _VI_FUNC gt50000_freq_list(ViSession instrSession,ViInt16 l_cont,ViInt16 index,
									ViReal64 set_freq,ViReal64 _VI_FAR *curr_freq,
									ViInt16 _VI_FAR *n_index);
ViStatus _VI_FUNC gt50000_setmask(ViSession instrSession,ViInt16 srq_byte,ViInt16 even_byte,
								  ViInt16 Ques_byte,ViInt16 Ques_freq,ViInt16 Ques_pwr);

/**********************************/
ViStatus _VI_FUNC gt50000_getPower(ViSession instrSession, ViPReal64 power);
ViStatus _VI_FUNC gt50000_setTimeout (ViSession instrSession, ViUInt16 tmo);
								   
ViStatus _VI_FUNC gt50000_query1(ViSession instrSession, ViInt16 _VI_FAR *Amstat,
								 ViInt16 _VI_FAR *RFstat,ViInt16 _VI_FAR *FMstat,
								 ViInt16 _VI_FAR *PMstat,ViReal64 _VI_FAR *Pout);						
ViStatus _VI_FUNC gt50000_query11(ViSession instrSession, ViPReal64 Stfreq, ViPReal64 Sofreq,
								  ViPReal64 Freqstep, ViPReal64 dwell,ViChar _VI_FAR *swmode,
								  ViPReal64 cwfreq); 

ViStatus _VI_FUNC gt50000_statbyte (ViSession instrSession, ViInt16 _VI_FAR *srq,
									ViInt16 _VI_FAR *event,ViInt16 _VI_FAR *ques,
									ViInt16 _VI_FAR *quesf,ViInt16 _VI_FAR *quesp,
									ViInt16 _VI_FAR *oper);
ViStatus _VI_FUNC gt50000_selectCorrectionsTable(ViString FilePath,ViString TableName);
ViStatus _VI_FUNC gt50000_setCorrectedPower(ViSession instrSession,double Frequency,float Power);

#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

/*****************************************************************************/
/*=== END INCLUDE FILE ======================================================*/
/*****************************************************************************/

#endif
