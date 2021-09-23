
/*= APS6062 Programmable Power Source(VISA) =================================*/

/*===========================================================================*/
/*  Please do not use global variables or arrays in the include file of      */
/*  instrument drivers that will be submitted for inclusion into the         */
/*  LabWindows Instrument Driver Library.                                    */
/*===========================================================================*/

#ifndef __aps6062_HEADER
#define __aps6062_HEADER

#include <vpptype.h>

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif

/*****************************************************************************/
/*= Define Instrument Specific Error Codes Here =============================*/
/*****************************************************************************/
#define VI_ERROR_FILE_OPEN              (_VI_ERROR+0x3FFC0800L)
#define VI_ERROR_FILE_WRITE             (_VI_ERROR+0x3FFC0801L)
#define VI_ERROR_SECURITY_CODE          (_VI_ERROR+0x3FFC0802L)
#define VI_ERROR_INTERPRETING_RESPONSE  (_VI_ERROR+0x3FFC0803L)
#define VI_ERROR_STAMP                  (_VI_ERROR+0x3FFC0805L) 
#define VI_ERROR_OUTOFF_RANGE           (_VI_ERROR+0x3FFC0806L) 

/*****************************************************************************/
/*= GLOBAL USER-CALLABLE FUNCTION DECLARATIONS (Exportable Functions) =======*/
/*****************************************************************************/
ViStatus _VI_FUNC aps6062_init (ViRsrc resourceName, ViBoolean IDQuery,
                        ViBoolean resetDevice, ViSession *instrumentHandle);
ViStatus _VI_FUNC aps6062_writeInstrData (ViSession instrumentHandle, ViString writeBuffer);
ViStatus _VI_FUNC aps6062_readInstrData (ViSession instrumentHandle,
                                 ViInt16 numberBytesToRead, ViChar readBuffer[],
                                 ViPInt32 numBytesRead);
ViStatus _VI_FUNC aps6062_reset (ViSession instrumentHandle);
ViStatus _VI_FUNC aps6062_selfTest (ViSession instrumentHandle, ViPInt16 selfTestResult,
                            ViChar selfTestMessage[]);
ViStatus _VI_FUNC aps6062_errorQuery (ViSession instrumentHandle, ViPInt32 errorCode,
                              ViChar errorMessage[]);
ViStatus _VI_FUNC aps6062_errorMessage (ViSession instrumentHandle, ViStatus errorCode,
                                ViChar errorMessage[]);
ViStatus _VI_FUNC aps6062_close (ViSession instrumentHandle);

#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

/*****************************************************************************/
/*=== END INCLUDE FILE ======================================================*/
/*****************************************************************************/

#endif
