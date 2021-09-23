/****************************************************************************
 *                       Synchro/Resolver                           
 *                                                                          
 * Title:    na65cs4.h                                        
 * Purpose:  Synchro/Resolver                                       
 *           instrument driver declarations.                                
 *                                                                          
 ****************************************************************************/

#ifndef __NA65CS4_HEADER
#define __NA65CS4_HEADER

#include <ivi.h>

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif

/****************************************************************************
 *-----------        NA65CS4 Typedefs and related constants  ---------------*
 ****************************************************************************/
#define NA65CS4_VXI_MANF_ID       0x0E85 /* only the lower 12 bits are the manufacturer id 
                                            0xBE85 is the actual ID written to register 0.
                                            The call to viGetAttribute (VI_ATTR_MANF_ID)
                                            is masking out the 4 upper bits to 0.
                                         */   
#define NA65CS4_VXI_MODEL_CODE    0x109


typedef struct      /* typedef of structure that records all the 65CS4 found in the system */
{
  char resource [90]; // resource name
  ViInt16 slot;       // slot device is in
  ViInt16 la;         // logical address of the device
  ViInt16 model;      // model code
} NA65CS4Found;


/* Signal Format Mode Constants */
#define NA65CS4_RESOLVER    0
#define NA65CS4_SYNCHRO     1

/* Source Constants */
#define NA65CS4_INT         0
#define NA65CS4_EXT         1
#define NA65CS4_BUS         2
#define NA65CS4_TTL_00      3    
#define NA65CS4_TTL_01      4     
#define NA65CS4_TTL_02      5     
#define NA65CS4_TTL_03      6     
#define NA65CS4_TTL_04      7     
#define NA65CS4_TTL_05      8     
#define NA65CS4_TTL_06      9     
#define NA65CS4_TTL_07      10     


/* Bandwidth Constants */
#define NA65CS4_HIGH_BW     0
#define NA65CS4_LOW_BW      1

/* Rotation Mode Constants */
#define NA65CS4_CONT        0
#define NA65CS4_STEP        1

/* Update Mode Constants */
#define NA65CS4_LATCH       0
#define NA65CS4_TRACK       1

/* Trigger Slope Constants */
#define NA65CS4_NEG         0
#define NA65CS4_POS         1

/* Relay I/O State Constants */
#define NA65CS4_OPEN        0
#define NA65CS4_CLOSE       1

/* D/S Channel Grade Constants */
#define NA65CS4_INSTRUMENT_GRADE  0
#define NA65CS4_OPERATIONAL_GRADE 1

/* Language Constants */
#define NA65CS4_LANG_NATIVE 0
#define NA65CS4_LANG_CIIL   1
#define NA65CS4_LANG_SCPI   2


/****************************************************************************
 *----------------- Instrument Driver Revision Information -----------------*
 ****************************************************************************/
#define NA65CS4_MAJOR_VERSION                1       /* Instrument driver major version   */
#define NA65CS4_MINOR_VERSION                100     /* Instrument driver minor version   */
                                                                
#define NA65CS4_CLASS_SPEC_MAJOR_VERSION     2     /* Class specification major version */
#define NA65CS4_CLASS_SPEC_MINOR_VERSION     0     /* Class specification minor version */


#define NA65CS4_SUPPORTED_INSTRUMENT_MODELS  "65CS4"
#define NA65CS4_DRIVER_VENDOR                "North Atlantic Industries, Inc."
#define NA65CS4_DRIVER_DESCRIPTION           "NAII VXI 65CS4 Syncho/Resolver Measurement/Stimulus and Reference Supply Instrument Driver"
                    

/**************************************************************************** 
 *---------------------------- Attribute Defines ---------------------------* 
 ****************************************************************************/

/*- IVI Inherent Instrument Attributes ---------------------------------*/    

/* User Options */
#define NA65CS4_ATTR_RANGE_CHECK                   IVI_ATTR_RANGE_CHECK                    /* ViBoolean */
#define NA65CS4_ATTR_QUERY_INSTRUMENT_STATUS       IVI_ATTR_QUERY_INSTRUMENT_STATUS        /* ViBoolean */
#define NA65CS4_ATTR_CACHE                         IVI_ATTR_CACHE                          /* ViBoolean */
#define NA65CS4_ATTR_SIMULATE                      IVI_ATTR_SIMULATE                       /* ViBoolean */
#define NA65CS4_ATTR_RECORD_COERCIONS              IVI_ATTR_RECORD_COERCIONS               /* ViBoolean */
#define NA65CS4_ATTR_INTERCHANGE_CHECK             IVI_ATTR_INTERCHANGE_CHECK              /* ViBoolean */
        
/* Driver Information  */
#define NA65CS4_ATTR_SPECIFIC_DRIVER_PREFIX        IVI_ATTR_SPECIFIC_DRIVER_PREFIX         /* ViString, read-only  */
#define NA65CS4_ATTR_SUPPORTED_INSTRUMENT_MODELS   IVI_ATTR_SUPPORTED_INSTRUMENT_MODELS    /* ViString, read-only  */
#define NA65CS4_ATTR_GROUP_CAPABILITIES            IVI_ATTR_GROUP_CAPABILITIES             /* ViString, read-only  */
#define NA65CS4_ATTR_INSTRUMENT_MANUFACTURER       IVI_ATTR_INSTRUMENT_MANUFACTURER        /* ViString, read-only  */
#define NA65CS4_ATTR_INSTRUMENT_MODEL              IVI_ATTR_INSTRUMENT_MODEL               /* ViString, read-only  */
#define NA65CS4_ATTR_INSTRUMENT_FIRMWARE_REVISION  IVI_ATTR_INSTRUMENT_FIRMWARE_REVISION              /* ViString, read-only  */
#define NA65CS4_ATTR_SPECIFIC_DRIVER_REVISION      IVI_ATTR_SPECIFIC_DRIVER_REVISION       /* ViString, read-only  */
#define NA65CS4_ATTR_SPECIFIC_DRIVER_VENDOR        IVI_ATTR_SPECIFIC_DRIVER_VENDOR         /* ViString, read-only  */
#define NA65CS4_ATTR_SPECIFIC_DRIVER_DESCRIPTION   IVI_ATTR_SPECIFIC_DRIVER_DESCRIPTION    /* ViString, read-only  */
#define NA65CS4_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MAJOR_VERSION IVI_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MAJOR_VERSION /* ViInt32, read-only */
#define NA65CS4_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MINOR_VERSION IVI_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MINOR_VERSION /* ViInt32, read-only */

/* Advanced Session Information */
#define NA65CS4_ATTR_LOGICAL_NAME                  IVI_ATTR_LOGICAL_NAME                   /* ViString, read-only  */
#define NA65CS4_ATTR_IO_RESOURCE_DESCRIPTOR        IVI_ATTR_IO_RESOURCE_DESCRIPTOR         /* ViString, read-only  */
#define NA65CS4_ATTR_DRIVER_SETUP                  IVI_ATTR_DRIVER_SETUP                   /* ViString, read-only  */        

/*- Instrument-Specific Attributes -------------------------------------*/
#define NA65CS4_ATTR_VXI_MANF_ID      (IVI_SPECIFIC_PUBLIC_ATTR_BASE + 1L)    /* ViInt32 (Read Only) */
#define NA65CS4_ATTR_VXI_MODEL_CODE   (IVI_SPECIFIC_PUBLIC_ATTR_BASE + 2L)    /* ViInt32 (Read Only) */

/**************************************************************************** 
 *------------------------ Attribute Value Defines -------------------------* 
 ****************************************************************************/

/* Instrument specific attribute value definitions */

/**************************************************************************** 
 *---------------- Instrument Driver Function Declarations -----------------* 
 ****************************************************************************/

/*- Instrument Find and Information Functions ------------------------------*/
ViStatus _VI_FUNC na65cs4_find (NA65CS4Found *na65CS4inSystem, ViInt16 *cnt);

ViStatus _VI_FUNC na65cs4_getModelInfo (ViString modelName,
                                        ViInt16 *maxSDInstChan,
                                        ViInt16 *maxSDOperChan, 
                                        ViInt16 *maxRefChan,
                                        ViInt16 *maxDSInstChan,
                                        ViInt16 *maxDSOperChan);

ViStatus _VI_FUNC na65cs4_getModelParts (ViString modelName, ViInt16 maxPartCnt,
                                         ViInt16 maxPartLength, ViInt16 *partCnt,
                                         char *supportedParts[]);

ViStatus _VI_FUNC na65cs4_getPartSpecInfo (ViString partSpecification,
                                           ViInt16 *maxSDInstChan,
                                           ViInt16 *maxSDOperChan, 
                                           ViInt16 *numDSInstChan,
                                           ViInt16 *numDSOperChan,
                                           ViInt16 *numRefChan,
                                           ViReal64 *minRefFreq,
                                           ViReal64 *maxRefFreq,
                                           ViReal64 *ref1PowerOutput,
                                           ViReal64 *ref1MinVolt,
                                           ViReal64 *ref1MaxVolt,
                                           ViReal64 *ref2PowerOutput,
                                           ViReal64 *ref2MinVolt,
                                           ViReal64 *ref2MaxVolt,
                                           ViInt16 *langInterface);

ViStatus _VI_FUNC na65cs4_query_language (ViSession vi, ViChar language[]);

ViStatus _VI_FUNC na65cs4_query_id (ViSession vi, ViChar identification[]);

/*- Init and Close Functions -------------------------------------------*/
ViStatus _VI_FUNC na65cs4_init (ViRsrc resourceName, ViBoolean IDQuery,
                                ViBoolean reset, ViSession *instrumentHandle);

ViStatus _VI_FUNC na65cs4_InitWithOptions (ViRsrc resourceName,
                                           ViBoolean IDQuery, ViBoolean reset,
                                           ViString optionString,
                                           ViSession *instrumentHandle);

ViStatus _VI_FUNC  na65cs4_close (ViSession vi);   


/*- Instrument Configuration Functions ---------------------------------*/

/**** Measurement Channel Configuration (i.e. API or SD Channel) ****/
ViStatus _VI_FUNC na65cs4_config_SD (ViSession vi, ViInt16 channel,
                                     ViInt16 channelGrade,
                                     ViInt16 signalMode, ViInt16 bandwidth,
                                     ViInt16 refSourceMode, ViInt16 DCScale,
                                     ViInt16 ratio, ViInt16 relayIOState,
                                     ViInt16 updateMode, ViInt16 maxWaitTime_Sec);

ViStatus _VI_FUNC na65cs4_config_SDDCScale (ViSession vi, ViInt16 channel,
                                            ViInt16 channelGrade,
                                            ViInt16 DCScale);

ViStatus _VI_FUNC na65cs4_config_SDBandwidth (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 bandwidth);

ViStatus _VI_FUNC na65cs4_config_SDMaxAngSettleTime (ViSession vi,
                                                     ViInt16 channelGrade,
                                                     ViInt16 channel,
                                                     ViInt16 maxWaitTime_Sec);

ViStatus _VI_FUNC na65cs4_config_SDSignalMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 mode);

ViStatus _VI_FUNC na65cs4_config_SDRatio (ViSession vi, ViInt16 channel,
                                          ViInt16 channelGrade,
                                          ViInt16 ratio);

ViStatus _VI_FUNC na65cs4_config_SDRefSrcMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 refSourceMode);

ViStatus _VI_FUNC na65cs4_config_SDRelayIOState (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViInt16 relayIOState);

ViStatus _VI_FUNC na65cs4_config_SDUpdateMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 updateMode);

/**** Stimulus Channel Configuration (i.e. DS Channel) ****/
ViStatus _VI_FUNC na65cs4_config_DSRotation (ViSession vi, ViInt16 channel,
                                             ViInt16 channelGrade,
                                             ViReal64 rotationRate,
                                             ViReal64 rotationStopAngle,
                                             ViInt16 rotationMode);

ViStatus _VI_FUNC na65cs4_config_DSAngle (ViSession vi, ViInt16 channel,
                                          ViInt16 channelGrade, ViReal64 angle);

ViStatus _VI_FUNC na65cs4_config_DSDCScale (ViSession vi, ViInt16 channel,
                                            ViInt16 channelGrade,
                                            ViInt16 DCScale);

ViStatus _VI_FUNC na65cs4_config_DSSignalMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 signalMode);

ViStatus _VI_FUNC na65cs4_config_DSRatio (ViSession vi, ViInt16 channel,
                                          ViInt16 channelGrade, ViInt16 ratio);

ViStatus _VI_FUNC na65cs4_config_DSRefSrcMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 refSourceMode);

ViStatus _VI_FUNC na65cs4_config_DSRelayIOState (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViInt16 relayIOState);

ViStatus _VI_FUNC na65cs4_config_DSLineToLineVoltage (ViSession vi,
                                                      ViInt16 channel,
                                                      ViInt16 channelGrade,
                                                      ViReal64 lineToLine_v);

ViStatus _VI_FUNC na65cs4_config_DSInputRefVolt (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViReal64 inputRefVoltage_v);

ViStatus _VI_FUNC na65cs4_config_DSRotationRate (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViReal64 rotationRate);

ViStatus _VI_FUNC na65cs4_config_DSRotateStopAng (ViSession vi, ViInt16 channel,
                                                  ViInt16 channelGrade,
                                                  ViReal64 rotationStopAngle);

ViStatus _VI_FUNC na65cs4_config_DSRotationMode (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViInt16 rotationMode);

ViStatus _VI_FUNC na65cs4_config_DSTriggerSrc (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 triggerSource);

ViStatus _VI_FUNC na65cs4_config_DSTriggerSlope (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViInt32 triggerSlope);

ViStatus _VI_FUNC na65cs4_config_DSPhaseShift (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViReal64 phaseShift);

/**** Reference Channel Configuration ****/

ViStatus _VI_FUNC na65cs4_config_REF (ViSession vi, ViInt16 channel,
                                      ViReal64 frequency_hz, ViReal64 voltage_v,
                                      ViInt16 relayIOState);

ViStatus _VI_FUNC na65cs4_config_REFFreq (ViSession vi, ViInt16 channel,
                                          ViReal64 frequency_Hz);

ViStatus _VI_FUNC na65cs4_config_REFVolt (ViSession vi, ViInt16 channel,
                                          ViReal64 voltage_v);

ViStatus _VI_FUNC na65cs4_config_REFRelayIOState (ViSession vi, ViInt16 channel,
                                                  ViInt16 relayIOState);


/*- Instrument Action/Status Functions ---------------------------------*/
ViStatus _VI_FUNC na65cs4_initiate_DSRotation (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade);

ViStatus _VI_FUNC na65cs4_get_DSRotationComplete (ViSession vi, ViInt16 channel,
                                                  ViInt16 channelGrade,
                                                  ViBoolean *rotationComplete);

/*- Instrument Query Functions ---------------------------------*/

/**** Measurement Channel Query (i.e. API or SD Channel) ****/
ViStatus _VI_FUNC na65cs4_query_SDAngle (ViSession vi, ViInt16 channel,
                                         ViInt16 channelGrade,
                                         ViReal64 *angle);

ViStatus _VI_FUNC na65cs4_query_SDVelocity (ViSession vi, ViInt16 channel,
                                            ViInt16 channelGrade,
                                            ViReal64 *velocity);

ViStatus _VI_FUNC na65cs4_query_SDDCScale (ViSession vi, ViInt16 channel,
                                           ViInt16 channelGrade,
                                           ViInt16 *DCScale);

ViStatus _VI_FUNC na65cs4_query_SDBandwidth (ViSession vi, ViInt16 channel,
                                             ViInt16 channelGrade,
                                             ViInt16 *bandwidth);

ViStatus _VI_FUNC na65cs4_query_SDMaxAngTime (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *maxAngleSettleTime);

ViStatus _VI_FUNC na65cs4_query_SDSignalMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *signalMode);

ViStatus _VI_FUNC na65cs4_query_SDRatio (ViSession vi, ViInt16 channel,
                                         ViInt16 channelGrade,
                                         ViInt16 *ratio);

ViStatus _VI_FUNC na65cs4_query_SDRefSrcMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *refSourceMode);

ViStatus _VI_FUNC na65cs4_query_SDRelayIOState (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViInt16 *relayIOState);

ViStatus _VI_FUNC na65cs4_query_SDUpdateMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,  
                                              ViInt16 *updateMode);

/**** Stimulus Channel Query (i.e. DS Channel) ****/
ViStatus _VI_FUNC na65cs4_query_DSAngle (ViSession vi, ViInt16 channel,
                                         ViInt16 channelGrade, ViReal64 *angle);

ViStatus _VI_FUNC na65cs4_query_DSDCScale (ViSession vi, ViInt16 channel,
                                           ViInt16 channelGrade,
                                           ViInt16 *DCScale);

ViStatus _VI_FUNC na65cs4_query_DSSignalMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *signalMode);

ViStatus _VI_FUNC na65cs4_query_DSRatio (ViSession vi, ViInt16 channel,
                                         ViInt16 channelGrade, ViInt16 *ratio);

ViStatus _VI_FUNC na65cs4_query_DSRefSrcMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *refSourceMode);

ViStatus _VI_FUNC na65cs4_query_DSRelayIOState (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViInt16 *relayIOState);

ViStatus _VI_FUNC na65cs4_query_DSLineToLineVoltage (ViSession vi,
                                                     ViInt16 channel,
                                                     ViInt16 channelGrade,
                                                     ViReal64 *LinetoLineVoltage);

ViStatus _VI_FUNC na65cs4_query_DSInputRefVolt (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViReal64 *inputRefVoltage_V);

ViStatus _VI_FUNC na65cs4_query_DSRotationRate (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViReal64 *rotationRate);

ViStatus _VI_FUNC na65cs4_query_DSRotateStopAng (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViReal64 *rotationStopAngle);

ViStatus _VI_FUNC na65cs4_query_DSRotationMode (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViInt16 *rotationMode);

ViStatus _VI_FUNC na65cs4_query_DSTriggerSrc (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *triggerSource);

ViStatus _VI_FUNC na65cs4_query_DSTriggerSlope (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViInt16 *triggerSlope);

ViStatus _VI_FUNC na65cs4_query_DSPhaseShift (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViReal64 *phaseShift);

/**** Reference Channel Query ****/
ViStatus _VI_FUNC na65cs4_query_RefFreq (ViSession vi, ViInt16 channel,
                                         ViReal64 *frequency);

ViStatus _VI_FUNC na65cs4_query_RefVolt (ViSession vi, ViInt16 channel,
                                         ViReal64 *voltage);

ViStatus _VI_FUNC na65cs4_query_RefRelayIOState (ViSession vi, ViInt16 channel,
                                                 ViInt16 *relayIOState);

/*- Coercion Info Functions --------------------------------------------*/
ViStatus _VI_FUNC  na65cs4_GetNextCoercionRecord (ViSession vi,
                                                   ViInt32 bufferSize,
                                                   ViChar record[]);
    
/*- Locking Functions --------------------------------------------------*/
ViStatus _VI_FUNC  na65cs4_LockSession (ViSession vi, ViBoolean *callerHasLock);   
ViStatus _VI_FUNC  na65cs4_UnlockSession (ViSession vi, ViBoolean *callerHasLock);

    
/*- Error Functions ----------------------------------------------------*/
ViStatus _VI_FUNC  na65cs4_error_query (ViSession vi, ViInt32 *errorCode,
                                         ViChar errorMessage[]);

ViStatus _VI_FUNC  na65cs4_GetError (ViSession vi, ViStatus *code, 
                                      ViInt32 bufferSize, ViChar description[]);
ViStatus _VI_FUNC  na65cs4_ClearError (ViSession vi);

ViStatus _VI_FUNC  na65cs4_error_message (ViSession vi, ViStatus errorCode,
                                           ViChar errorMessage[256]);
    

/*- Interchangeability Checking Functions ------------------------------*/
ViStatus _VI_FUNC na65cs4_GetNextInterchangeWarning (ViSession vi, 
                                                      ViInt32 bufferSize, 
                                                      ViChar warnString[]);
ViStatus _VI_FUNC na65cs4_ResetInterchangeCheck (ViSession vi);
ViStatus _VI_FUNC na65cs4_ClearInterchangeWarnings (ViSession vi);

/*- Utility Functions --------------------------------------------------*/
ViStatus _VI_FUNC na65cs4_InvalidateAllAttributes (ViSession vi);

ViStatus _VI_FUNC  na65cs4_reset (ViSession vi);
ViStatus _VI_FUNC  na65cs4_ResetWithDefaults (ViSession vi);
ViStatus _VI_FUNC  na65cs4_self_test (ViSession vi, ViInt16 *selfTestResult,
                                       ViChar selfTestMessage[]);
ViStatus _VI_FUNC  na65cs4_revision_query (ViSession vi, 
                                            ViChar instrumentDriverRevision[],
                                            ViChar firmwareRevision[]);
ViStatus _VI_FUNC na65cs4_Disable (ViSession vi);
ViStatus _VI_FUNC  na65cs4_WriteInstrData (ViSession vi, ViConstString writeBuffer); 
ViStatus _VI_FUNC  na65cs4_ReadInstrData  (ViSession vi, ViInt32 numBytes, 
                                            ViChar rdBuf[], ViInt32 *bytesRead);

/*- Set, Get, and Check Attribute Functions ----------------------------*/
ViStatus _VI_FUNC  na65cs4_GetAttributeViInt32 (ViSession vi, ViConstString channelName, ViAttr attribute, ViInt32 *value);
ViStatus _VI_FUNC  na65cs4_GetAttributeViReal64 (ViSession vi, ViConstString channelName, ViAttr attribute, ViReal64 *value);
ViStatus _VI_FUNC  na65cs4_GetAttributeViString (ViSession vi, ViConstString channelName, ViAttr attribute, ViInt32 bufSize, ViChar value[]); 
ViStatus _VI_FUNC  na65cs4_GetAttributeViSession (ViSession vi, ViConstString channelName, ViAttr attribute, ViSession *value);
ViStatus _VI_FUNC  na65cs4_GetAttributeViBoolean (ViSession vi, ViConstString channelName, ViAttr attribute, ViBoolean *value);

ViStatus _VI_FUNC  na65cs4_SetAttributeViInt32 (ViSession vi, ViConstString channelName, ViAttr attribute, ViInt32 value);
ViStatus _VI_FUNC  na65cs4_SetAttributeViReal64 (ViSession vi, ViConstString channelName, ViAttr attribute, ViReal64 value);
ViStatus _VI_FUNC  na65cs4_SetAttributeViString (ViSession vi, ViConstString channelName, ViAttr attribute, ViConstString value); 
ViStatus _VI_FUNC  na65cs4_SetAttributeViSession (ViSession vi, ViConstString channelName, ViAttr attribute, ViSession value);
ViStatus _VI_FUNC  na65cs4_SetAttributeViBoolean (ViSession vi, ViConstString channelName, ViAttr attribute, ViBoolean value);

ViStatus _VI_FUNC  na65cs4_CheckAttributeViInt32 (ViSession vi, ViConstString channelName, ViAttr attribute, ViInt32 value);
ViStatus _VI_FUNC  na65cs4_CheckAttributeViReal64 (ViSession vi, ViConstString channelName, ViAttr attribute, ViReal64 value);
ViStatus _VI_FUNC  na65cs4_CheckAttributeViString (ViSession vi, ViConstString channelName, ViAttr attribute, ViConstString value); 
ViStatus _VI_FUNC  na65cs4_CheckAttributeViSession (ViSession vi, ViConstString channelName, ViAttr attribute, ViSession value);
ViStatus _VI_FUNC  na65cs4_CheckAttributeViBoolean (ViSession vi, ViConstString channelName, ViAttr attribute, ViBoolean value);

/*********************************************************
    Functions reserved for class driver use only.
    End-users should not call these functions.  
*********************************************************/
ViStatus _VI_FUNC  na65cs4_IviInit (ViRsrc resourceName, ViBoolean IDQuery, 
                                     ViBoolean reset, ViSession vi);
ViStatus _VI_FUNC  na65cs4_IviClose (ViSession vi);   

/****************************************************************************
 *------------------------ Error And Completion Codes ----------------------*
 ****************************************************************************/

/**************************************************************************** 
 *---------------------------- End Include File ----------------------------* 
 ****************************************************************************/
#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif
#endif /* __NA65CS4_HEADER */




