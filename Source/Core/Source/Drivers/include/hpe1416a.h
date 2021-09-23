/*= HP E1416A RF Power Meter ==========================================*/

/*===========================================================================*/
/*  Please do not use global variables or arrays in the include file of      */
/*  instrument drivers that will be submitted for inclusion into the         */
/*  LabWindows Instrument Driver Library.                                    */
/*===========================================================================*/
     
#ifndef __hpe1416a_HEADER
#define __hpe1416a_HEADER

#include <vpptype.h>

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif

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
ViStatus _VI_FUNC hpe1416a_init (ViRsrc resourceName, ViBoolean IDQuery,
                        ViBoolean resetDevice, ViPSession instrumentHandle);
ViStatus _VI_FUNC hpe1416a_writeInstrData (ViSession instrumentHandle, ViString writeBuffer);
ViStatus _VI_FUNC hpe1416a_readInstrData (ViSession instrumentHandle,
                                 ViInt16 numberBytesToRead, ViPBuf readBuffer,
                                 ViPUInt32 numBytesRead);
ViStatus _VI_FUNC hpe1416a_reset (ViSession instrumentHandle);
ViStatus _VI_FUNC hpe1416a_selfTest (ViSession instrumentHandle, ViPInt16 selfTestResult,
                           ViChar selfTestMessage[]);
ViStatus _VI_FUNC hpe1416a_errorQuery (ViSession instrumentHandle, ViPInt32 errorCode,
                              ViChar errorMessage[]);
ViStatus _VI_FUNC hpe1416a_errorMessage (ViSession instrumentHandle, ViStatus errorCode,
                                ViChar errorMessage[]);
ViStatus _VI_FUNC hpe1416a_revisionQuery (ViSession instrumentHandle,
                                 ViChar instrumentDriverRevision[],
                                 ViChar firmwareRevision[]);
ViStatus _VI_FUNC hpe1416a_waitForRqs (ViSession instrumentHandle, ViInt32 timeoutms,
                              ViUInt16 statusByte);
ViStatus _VI_FUNC hpe1416a_close (ViSession instrumentHandle);

#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

/*****************************************************************************/
/*=== END INCLUDE FILE ======================================================*/
/*****************************************************************************/

#endif