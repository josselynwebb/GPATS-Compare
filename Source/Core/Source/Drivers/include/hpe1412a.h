/*= HP E1412A 6.5 Digit Multimeter ==========================================*/

/*===========================================================================*/
/*  Please do not use global variables or arrays in the include file of      */
/*  instrument drivers that will be submitted for inclusion into the         */
/*  LabWindows Instrument Driver Library.                                    */
/*===========================================================================*/
     
#ifndef __hpe1412a_HEADER
#define __hpe1412a_HEADER

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
ViStatus _VI_FUNC hpe1412a_init (ViRsrc resourceName, ViBoolean IDQuery,
                        ViBoolean resetDevice, ViPSession instrumentHandle);
ViStatus _VI_FUNC hpe1412a_appExample (ViSession instrumentHandle,
                              ViInt16 measurementFunction, ViReal64 measurementValue[],
                              ViReal64 manualDelays, ViBoolean triggerDelay, 
                              ViInt32 samples, ViReal64 resolution);
ViStatus _VI_FUNC hpe1412a_confTrigger (ViSession instrumentHandle, ViInt16 triggerMode,
                               ViReal64 manualDelays, ViBoolean triggerDelay,
                               ViInt32 samples, ViInt32 triggerCount);
ViStatus _VI_FUNC hpe1412a_confAcCurr (ViSession instrumentHandle, ViInt16 range,
                              ViReal64 resolution, ViInt16 filter);
ViStatus _VI_FUNC hpe1412a_confDcCurr (ViSession instrumentHandle, ViInt16 range,
                              ViInt16 integrationTime, ViReal64 resolution,
                              ViInt16 autozero);
ViStatus _VI_FUNC hpe1412a_confAcVolt (ViSession instrumentHandle, ViInt16 range,
                              ViInt16 filter, ViReal64 resolution);
ViStatus _VI_FUNC hpe1412a_confDcVolt (ViSession instrumentHandle, ViInt16 range,
                              ViInt16 autozero, ViInt16 integrationTime,
                              ViReal64 resolution, ViBoolean type, ViBoolean inputZ);
ViStatus _VI_FUNC hpe1412a_confFreqPer (ViSession instrumentHandle, ViInt16 range,
                               ViInt16 apertureTime, ViBoolean type);
ViStatus _VI_FUNC hpe1412a_confMath (ViSession instrumentHandle, ViInt16 operation,
                            ViReal64 nulloffsetdBreference, ViInt16 dBmReference,
                            ViReal64 lowerLimit, ViReal64 upperLimit);
ViStatus _VI_FUNC hpe1412a_confOut (ViSession instrumentHandle, ViInt16 outputLine);
ViStatus _VI_FUNC hpe1412a_confResist (ViSession instrumentHandle, ViInt16 range,
                              ViInt16 integrationTime, ViReal64 resolution,
                              ViInt16 autozero, ViBoolean type);
ViStatus _VI_FUNC hpe1412a_abort (ViSession instrumentHandle);
ViStatus _VI_FUNC hpe1412a_trigger (ViSession instrumentHandle);
ViStatus _VI_FUNC hpe1412a_initiate (ViSession instrumentHandle);
ViStatus _VI_FUNC hpe1412a_readMeas (ViSession instrumentHandle, ViReal64 measurementValue[]);
ViStatus _VI_FUNC hpe1412a_querDataPts (ViSession instrumentHandle,
                               ViPInt16 numberofDataPoints);
ViStatus _VI_FUNC hpe1412a_readMinMax (ViSession instrumentHandle, ViPReal64 minimum,
                              ViPReal64 maximum, ViPReal64 average, ViPReal64 count);
ViStatus _VI_FUNC hpe1412a_writeInstrData (ViSession instrumentHandle, ViString writeBuffer);
ViStatus _VI_FUNC hpe1412a_readInstrData (ViSession instrumentHandle,
                                 ViInt16 numberBytesToRead, ViPBuf readBuffer,
                                 ViPUInt32 numBytesRead);
ViStatus _VI_FUNC hpe1412a_reset (ViSession instrumentHandle);
ViStatus _VI_FUNC hpe1412a_selfTest (ViSession instrumentHandle, ViPInt16 selfTestResult,
                            ViChar selfTestMessage[]);
ViStatus _VI_FUNC hpe1412a_errorQuery (ViSession instrumentHandle, ViPInt32 errorCode,
                              ViChar errorMessage[]);
ViStatus _VI_FUNC hpe1412a_errorMessage (ViSession instrumentHandle, ViStatus errorCode,
                                ViChar errorMessage[]);
ViStatus _VI_FUNC hpe1412a_revisionQuery (ViSession instrumentHandle,
                                 ViChar instrumentDriverRevision[],
                                 ViChar firmwareRevision[]);
ViStatus _VI_FUNC hpe1412a_waitForRqs (ViSession instrumentHandle, ViInt32 timeoutms,
                              ViPUInt16 statusByte);
ViStatus _VI_FUNC hpe1412a_close (ViSession instrumentHandle);

#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

/*****************************************************************************/
/*=== END INCLUDE FILE ======================================================*/
/*****************************************************************************/

#endif
