/*= HP E1412A 6.5 Digit Multimeter ==========================================*/

/*===========================================================================*/
/*  Please do not use global variables or arrays in the include file of      */
/*  instrument drivers that will be submitted for inclusion into the         */
/*  LabWindows Instrument Driver Library.                                    */
/*===========================================================================*/
     
#ifndef __hpe1428a_HEADER
#define __hpe1428a_HEADER

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
ViStatus _VI_FUNC hpe1428a_init (ViRsrc resourceName, ViBoolean IDQuery,
                        ViBoolean resetDevice, ViPSession instrumentHandle);
ViStatus _VI_FUNC hpe1428a_writeInstrData (ViSession instrumentHandle, ViString writeBuffer);
ViStatus _VI_FUNC hpe1428a_readInstrData (ViSession instrumentHandle,
                                 ViInt16 numberBytesToRead, ViPBuf readBuffer,
                                 ViInt32 *numBytesRead);
ViStatus _VI_FUNC hpe1428a_reset (ViSession instrumentHandle);
ViStatus _VI_FUNC hpe1428a_selfTest (ViSession instrumentHandle, ViPInt16 selfTestResult,
                           ViChar selfTestMessage[]);
ViStatus _VI_FUNC hpe1428a_errorQuery(ViSession instrSession,
                                              ViPInt32 errCode, ViChar *errMessage);
ViStatus _VI_FUNC hpe1428a_errorMessage (ViSession instrumentHandle, ViStatus errorCode,
                                ViChar errorMessage[]);
ViStatus _VI_FUNC hpe1428a_revisionQuery (ViSession instrumentHandle,
                                 ViChar instrumentDriverRevision[],
                                 ViChar firmwareRevision[]);
ViStatus _VI_FUNC hpe1428a_waitForRqs (ViSession instrumentHandle, ViInt32 timeoutms,
                              ViPUInt16 statusByte);
ViStatus _VI_FUNC hpe1428a_close (ViSession instrumentHandle);
ViStatus _VI_FUNC hpe1428a_msgWaiting (ViSession instrSession, ViPInt32 buffFull);
ViStatus _VI_FUNC hpe1428a_GtWv (ViPReal32 WaveArray, ViPReal32 TimeArray);
ViStatus _VI_FUNC hpe1428a_WvData (ViSession instrSession, ViReal32 YIncrement, ViReal32 YOffset, ViReal32 XIncrement, ViReal32 XOffset, ViUInt16 WhichArea, ViUInt16 TotChan, ViUInt16 AquType);
ViStatus _VI_FUNC hpe1428a_softReset (ViSession instrSession);
ViStatus _VI_FUNC hpe1428a_FindInst (ViSession instrSession, ViUInt32 MemOffset1, ViUInt32 MemOffset2, ViUInt32 MathOffset, ViUInt16 MemoryPoints, ViPUInt32 VMEAddress);
ViStatus _VI_FUNC hpe1428a_ResetTM (ViSession instrSession);
#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

/*****************************************************************************/
/*=== END INCLUDE FILE ======================================================*/
/*****************************************************************************/

#endif
