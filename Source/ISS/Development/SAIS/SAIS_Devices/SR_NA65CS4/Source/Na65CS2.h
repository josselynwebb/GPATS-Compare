#include <cvidef.h>

#ifndef __na65CS2_HEADER
#define __na65CS2_HEADER

#include <vpptype.h>
	 
#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif

/* DUT Specific Parameters */
#define RESOLVER 1
#define SYNCHRO 0
#define TRACK 0
#define HOLD 1
#define GENERATE 1
#define MEASURE 0
#define HOLD 1
#define OPEN 0
#define CLOSED 1   
#define HIGH 0
#define LOW 1
#define EXTERNAL 0
#define INTERNAL 1
#define WAIT 1
#define IMEDIATE 0
#define na65CS2_OFF 0
#define na65CS2_ON 1
 
#define na65CS2_MODEL_CODE (ViAttrState)0x0104
#define na65CS2_MANF_ID  (ViAttrState)0xe85
 
#define na65CS2_STABILITY_ERROR   _VI_ERROR+0x3FFC0D00L

#define VI_ERROR_PARAMETER9      _VI_ERROR+0x3FFC0009L
 
struct na65CS2Found       /*structure for recording all DUTs in sytem */
		{
		char resource[90];
		ViInt16 slot; 
		ViInt16 la;
		ViInt16 model;
		};


/*****************************************************************************/
/*= Define Instrument Specific Error Codes Here =============================*/
/*****************************************************************************/
#define VI_INSTR_ERROR_OFFSET           (_VI_ERROR+0x3FFC0900L)
#define VI_ERROR_FILE_OPEN              (_VI_ERROR+0x3FFC0800L)
#define VI_ERROR_FILE_WRITE             (_VI_ERROR+0x3FFC0801L)
#define VI_ERROR_INTERPRETING_RESPONSE  (_VI_ERROR+0x3FFC0803L)

/*****************************************************************************/
/*= GLOBAL USER-CALLABLE FUNCTION DECLARATIONS (Exportable Functions) =======*/
/*****************************************************************************/

ViStatus _VI_FUNC na65CS2_find (struct na65CS2Found *na65CS2inSystem, ViPUInt16 cnt);  
ViStatus _VI_FUNC na65CS2_init (ViRsrc resourceName, ViBoolean IDQuery,
					ViBoolean reset, ViSession *instrSession);



ViStatus _VI_FUNC na65CS2_config_SDmode (ViSession instrumentHandle, 
					ViInt16 channel,ViInt16 mode );
ViStatus _VI_FUNC na65CS2_query_SDmode (ViSession instrumentHandle, 
					ViInt16 channel, ViChar _VI_FAR mode[]);
					
ViStatus _VI_FUNC na65CS2_config_DSmode (ViSession instrumentHandle, 
					ViInt16 channel, ViInt16 mode);
					
ViStatus _VI_FUNC na65CS2_query_DSmode (ViSession instrumentHandle, 
					ViInt16 channel, ViChar _VI_FAR  mode[]);

ViStatus _VI_FUNC na65CS2_query_SDangle (ViSession instrumentHandle, 
					ViInt16 channel,ViString VorA, ViReal64 *angle);					

ViStatus _VI_FUNC na65CS2_config_DSangle (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 angle);
					
ViStatus _VI_FUNC na65CS2_query_DSangle (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 *angle);
					
ViStatus _VI_FUNC na65CS2_config_DSref (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 ref);
					
ViStatus _VI_FUNC na65CS2_query_DSref (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 *ref);
					
ViStatus _VI_FUNC na65CS2_config_DSvolt (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 lineToLine);
ViStatus _VI_FUNC na65CS2_query_DSvolt (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 *lineToLine);
					
ViStatus _VI_FUNC na65CS2_config_DSphase (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 phase);
ViStatus _VI_FUNC na65CS2_query_DSphase (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 *phase);

ViStatus _VI_FUNC na65CS2_config_SDio (ViSession instrumentHandle, 
					ViInt16 channel, ViInt16 io);
ViStatus _VI_FUNC na65CS2_query_SDio (ViSession instrumentHandle, 
					ViInt16 channel, ViChar _VI_FAR io[]);
					
ViStatus _VI_FUNC na65CS2_config_DSio (ViSession instrumentHandle, 
					ViInt16 channel, ViInt16 io);
ViStatus _VI_FUNC na65CS2_query_DSio (ViSession instrumentHandle, 
					ViInt16 channel, ViChar _VI_FAR io[]);

ViStatus _VI_FUNC na65CS2_config_track (ViSession instrumentHandle, 
					 ViInt16 channel, ViInt16 trackHold);
ViStatus _VI_FUNC na65CS2_query_track (ViSession instrumentHandle, 
					 ViInt16 channel,  ViChar _VI_FAR trackHold[]);

					
ViStatus _VI_FUNC na65CS2_rot_init (ViSession instrumentHandle, ViInt16 channel);

ViStatus _VI_FUNC na65CS2_config_rot (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 rot_rate, ViReal64 stopang,ViInt16 rot_mode);


ViStatus _VI_FUNC na65CS2_config_measure (ViSession instrumentHandle, 
					ViInt16 channel, ViInt16 mode, ViInt16 trackHold);

ViStatus _VI_FUNC na65CS2_config_generate (ViSession instrumentHandle,
					ViInt16 channel, ViInt16 mode,
					ViReal64 volt, ViReal64 ds_ref, ViReal64 phase);

					
ViStatus _VI_FUNC na65CS2_config_rot_rate (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 rot_rate);
ViStatus _VI_FUNC na65CS2_query_rot_rate (ViSession instrumentHandle, 
					ViInt16 channel, ViChar _VI_FAR rate[]);
					
ViStatus _VI_FUNC na65CS2_config_rot_stop (ViSession instrumentHandle, 
					ViInt16 channel, ViReal64 stop_ang);
					
ViStatus _VI_FUNC na65CS2_query_rot_stop (ViSession instrumentHandle, 
					ViInt16 channel, ViChar _VI_FAR stopAng[]);

ViStatus _VI_FUNC na65CS2_config_rot_mode (ViSession instrumentHandle,
					ViInt16 channel, ViInt16 rot_mode);


ViStatus _VI_FUNC na65CS2_query_rot_mode (ViSession instrumentHandle,
					ViInt16 channel, ViChar _VI_FAR mode[]);
					
ViStatus _VI_FUNC na65CS2_query_rot_done (ViSession instrumentHandle, 
					ViInt16 channel, ViChar _VI_FAR done[]);

ViStatus _VI_FUNC na65CS2_reset (ViSession instrSession);			 

ViStatus _VI_FUNC na65CS2_writeInstrData (ViSession instrSession, ViString writeBuffer);
ViStatus _VI_FUNC na65CS2_readInstrData (ViSession instrSession, ViInt16 numBytes,
					ViChar _VI_FAR rdBuf[], ViPInt32 bytesRead);			 
ViStatus _VI_FUNC na65CS2_self_test (ViSession instrSession, ViPInt16 testResult,
					ViChar _VI_FAR testMessage[]);
ViStatus _VI_FUNC na65CS2_errorQuery (ViSession instrSession, ViPInt32 errCode,
					ViChar _VI_FAR errMessage[]);
ViStatus _VI_FUNC na65CS2_RdDut (ViSession instrSession, ViChar _VI_FAR Message[]);    					
					
ViStatus _VI_FUNC na65CS2_errorMessage (ViSession instrSession, ViStatus errorCode,
					ViChar _VI_FAR errMessage[]);
ViStatus _VI_FUNC na65CS2_revisionQuery (ViSession instrSession,
					ViChar _VI_FAR driverRev[], ViChar _VI_FAR instrRev[]);
ViStatus _VI_FUNC na65CS2_query_id (ViSession instrSession,
					ViChar _VI_FAR instrID[]);
ViStatus _VI_FUNC na65CS2_close (ViSession instrSession);



#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

/*****************************************************************************/
/*=== END INCLUDE FILE ======================================================*/
/*****************************************************************************/

#endif
