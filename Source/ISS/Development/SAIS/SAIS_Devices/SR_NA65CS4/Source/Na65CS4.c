#include <ansi_c.h>
#include "NA65CS4.h"

/*****************************************************************************
 *  Synchro/Resolver Instrument Driver                               
 *  LabWindows/CVI Instrument Driver                                     
 *  Original Release: Monday, July 11, 2005                                  
 *  By: , North Atlantic Industries                              
 *                                                                           
 *  Modification History:                                                    
 *                                                                           
 *       Monday, July 11, 2005 - Instrument Driver Created.                  
 *                                                                           
 *****************************************************************************/

#include <string.h>
#include <stdio.h>  
#include <formatio.h>
#include "na65cs4.h"

/*****************************************************************************
 *--------------------- Hidden Attribute Declarations -----------------------*
 *****************************************************************************/

#define NA65CS4_ATTR_OPC_TIMEOUT      (IVI_SPECIFIC_PRIVATE_ATTR_BASE + 1L)       /* ViInt32 */
#define NA65CS4_ATTR_USE_SPECIFIC_SIMULATION  (IVI_INHERENT_ATTR_BASE + 23L)  /* ViBoolean */
#define NA65CS4_ATTR_VISA_RM_SESSION  (IVI_SPECIFIC_PRIVATE_ATTR_BASE + 5L)   /* ViSession */
#define NA65CS4_ATTR_IO_SESSION   (IVI_INHERENT_ATTR_BASE + 322L)         /* ViSession */
#define NA65CS4_ATTR_OPC_CALLBACK     (IVI_SPECIFIC_PRIVATE_ATTR_BASE + 6L)   /* ViAddr */
#define NA65CS4_ATTR_CHECK_STATUS_CALLBACK    (IVI_SPECIFIC_PRIVATE_ATTR_BASE + 7L)   /* ViAddr */
#define NA65CS4_ATTR_USER_INTERCHANGE_CHECK_CALLBACK  (IVI_SPECIFIC_PRIVATE_ATTR_BASE + 8L)   /* ViAddr */

/*****************************************************************************
 *-------- Hidden Part Number Designation Support Declarations --------------*
 *****************************************************************************/

typedef struct
{
    char  szPartNumber[100];   /* Part Number Designation supported by this driver */
    int   nSDInstChanCnt;      /* Number of Instrument-Grade SD Channels */
    int   nSDOperChanCnt;      /* Number of Operational-Grade SD Channels */ 
    int   nDSInstChanCnt;      /* Number of Instrument-Grade DS Channels */
    int   nDSOperChanCnt;      /* Number of Operational-Grade DS Channels */
    int   nRefChanCnt;         /* Number of Reference Channels */
    int   nLanguage;           /* Language Interface */
    float fMinRefFreq;         /* Minimum Reference frequency */
    float fMaxRefFreq;         /* Maximum Reference frequency */
    float fRef1PowerOutput;    /* Reference Channel 1 Power Output */
    float fRef1MinVolt;        /* Reference Channel 1 Min Volt */
    float fRef1MaxVolt;        /* Reference Channel 1 Max Volt */
    float fRef2PowerOutput;    /* Reference Channel 2 Power Output */
    float fRef2MinVolt;        /* Reference Channel 2 Min Volt */
    float fRef2MaxVolt;        /* Reference Channel 2 Max Volt */
} PART_NUMBER_DESIGNATION; 


static PART_NUMBER_DESIGNATION PartNumberTable[] = 
{
    "65CS4-221NG-xx", 2, 0, 2, 0, 1, NA65CS4_LANG_NATIVE, 47.0, 5000.0, 5.2, 2.0, 28.0, 5.2, 115.0, 115.0,
    
};

/* default language to NATIVE */
int LANG = NA65CS4_LANG_NATIVE;

/* Calculate size of unspecified dimension */
const int PartNumberTableCnt = sizeof PartNumberTable / sizeof( PART_NUMBER_DESIGNATION );


/*****************************************************************************
 *---------------------------- Useful Macros --------------------------------*
 *****************************************************************************/

/*- I/O buffer size -----------------------------------------------------*/
#define BUFFER_SIZE                        512L        

/*- 488.2 Event Status Register (ESR) Bits ------------------------------*/
#define IEEE_488_2_ERROR_MASK              0x3C


/*****************************************************************************
 *-------------- Utility Function Declarations (Non-Exported) ---------------*
 *****************************************************************************/
static ViStatus na65cs4_InitAttributes (ViSession vi);
static ViStatus na65cs4_DefaultInstrSetup (ViSession openInstrSession);
static ViStatus na65cs4_CheckStatus (ViSession vi);
static ViStatus na65cs4_WaitForOPC (ViSession vi, ViInt32 maxTime);

/*- File I/O Utility Functions -*/
static ViStatus na65cs4_ReadToFile (ViSession vi, ViConstString filename, 
                                     ViInt32 maxBytesToRead, ViInt32 fileAction, ViInt32 *totalBytesWritten);
static ViStatus na65cs4_WriteFromFile (ViSession vi, ViConstString filename, 
                                        ViInt32 maxBytesToWrite, ViInt32 byteOffset, 
                                        ViInt32 *totalBytesWritten);

/*- String manipulation Functions -*/
ViStatus na65cs4_TrimAndRemoveCRLF(ViChar msg[], ViInt16 nMsgSize, ViChar msgNew[]);

/*****************************************************************************
 *----------------- Callback Declarations (Non-Exported) --------------------*
 *****************************************************************************/

/*- Global Session Callbacks --------------------------------------------*/
    
static ViStatus _VI_FUNC na65cs4_CheckStatusCallback (ViSession vi, ViSession io);
static ViStatus _VI_FUNC na65cs4_WaitForOPCCallback (ViSession vi, ViSession io);

/*- Attribute callbacks -------------------------------------------------*/

static ViStatus _VI_FUNC na65cs4AttrDriverRevision_ReadCallback (ViSession vi,
                                                                  ViSession io, 
                                                                  ViConstString channelName,
                                                                  ViAttr attributeId, 
                                                                  const ViConstString cacheValue);
static ViStatus _VI_FUNC na65cs4AttrInstrumentManufacturer_ReadCallback (ViSession vi, 
                                                                          ViSession io,
                                                                          ViConstString channelName, 
                                                                          ViAttr attributeId,
                                                                          const ViConstString cacheValue);
static ViStatus _VI_FUNC na65cs4AttrInstrumentModel_ReadCallback (ViSession vi, 
                                                                   ViSession io,
                                                                   ViConstString channelName, 
                                                                   ViAttr attributeId,
                                                                   const ViConstString cacheValue);

static ViStatus _VI_FUNC na65cs4AttrVXIManfID_ReadCallback (ViSession vi,
                                                             ViSession io, 
                                                             ViConstString channelName, 
                                                             ViAttr attributeId, 
                                                             ViInt32* value);

static ViStatus _VI_FUNC na65cs4AttrVXIModelCode_ReadCallback (ViSession vi,
                                                                ViSession io, 
                                                                ViConstString channelName, 
                                                                ViAttr attributeId, 
                                                                ViInt32* value);

/*****************************************************************************
 *------------ User-Callable Functions (Exportable Functions) ---------------*
 *****************************************************************************/

/*=============================================================================
 * Function: na65cs4_find                                                    
 * Purpose:  This function finds all 65CS4 in the sytem and stores the       
 *           resource name, slot, logic address and model code in a          
 *           na65CS4Found structure. Before calling this function you must   
 *           allocate an array of these structures as large as the possible  
 *           number of 65CS4s expected in your system.                       
 *===========================================================================*/
ViStatus _VI_FUNC na65cs4_find (NA65CS4Found *na65CS4inSystem, ViInt16 *cnt)
{
    ViStatus    error = VI_SUCCESS;
    
    ViSession   rm;  
    ViSession   vi;
    ViChar      expr[] = "?*VXI[0-9]*::?*INSTR";
    ViFindList  list;
    ViUInt32    retCnt = 0;
    ViRsrc      res[80];  
    ViPRsrc     resourceName = (ViPRsrc)res; 
    int         i;
    int         nFoundCnt = 0;
    unsigned short attr;
//  cnt = 0;
    
    /* Check to make sure output variables are not null */
    if ((na65CS4inSystem == VI_NULL) || (cnt == VI_NULL))
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER5, "Null address for na65cs4_find parameters");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }
    
    /* Open instrument session */
    checkErr( viOpenDefaultRM(&rm)); 
                
    /* Find resources */
    checkErr( viFindRsrc(rm, expr, &list, &retCnt, resourceName));  

    /* Look for 65CS4 devices */
    for (i = 0; i < (int)retCnt; i++)
    {
        /* Open Instrument */
        checkErr( viOpen(rm, resourceName, VI_NULL, VI_NULL, &vi));
        
        /* Get Instrument Manufacturer ID */
        checkErr( viGetAttribute(vi, VI_ATTR_MANF_ID, &attr));
        
        /* Check to see if the Manufacturer is for NA65CS4 */
        if (attr == NA65CS4_VXI_MANF_ID) 
        {
            /* Get Instrument Model Code */
            checkErr( viGetAttribute(vi, VI_ATTR_MODEL_CODE, &attr));
            
            /* Check to see if the Model Code is for NA65CS4 */
            if (attr == NA65CS4_VXI_MODEL_CODE)
            {
                /* Fill in the NA65CS4 resource name information in the array structure */
                strcpy(na65CS4inSystem->resource, resourceName);
                
                /* Fill in the NA65CS4 model code information in the array structure */
                na65CS4inSystem->model = attr;
                
                /* Get the Instrument Slot location */
                checkErr ( viGetAttribute(vi, VI_ATTR_SLOT, &attr));
                
                /* Fill in the NA65CS4 slot information in the array structure */
                na65CS4inSystem->slot = attr;
                
                /* Get the Instrument Logical Address */
                checkErr ( viGetAttribute(vi, VI_ATTR_VXI_LA, &attr));
                
                /* Fill in the NA65CS4 logical address in the array structure */
                na65CS4inSystem->la = attr;
                
                /* Increment the array pointer to the next structure */
                na65CS4inSystem++;
                
                /* Increment the counter for the number of NA65CS4 found in the system */
                nFoundCnt++;
            }
        }
        
        /* Close Instrument */
        checkErr( viClose(vi));
        
        /* Get the next Instrument in list */
        if (viFindNext(list, resourceName) == VI_ERROR_RSRC_NFOUND)
        {
            /* When there are no more resources in the list this is a
               valid condition, set the return status to VI_SUCCESS */
            error = VI_SUCCESS; 
        }
        
    }
        
Error:
    /* Close the default session */
    checkErr ( viClose(rm));
    
    /* Set the number of NA65CS4 found in the system */
    *cnt = nFoundCnt;

    return error;
}

/*============================================================================
 * Function: na65cs4_getModelInfo                                            
 * Purpose:  This function provides information about the maximum number of  
 *           SD, Reference Generator and  
 *           DS channels supported by the Model.                             
/*===========================================================================*/
ViStatus _VI_FUNC na65cs4_getModelInfo (ViString modelName,
                                        ViInt16 *maxSDInstChan,
                                        ViInt16 *maxSDOperChan,
                                        ViInt16 *maxRefChan,
                                        ViInt16 *maxDSInstChan,
                                        ViInt16 *maxDSOperChan)
{
    ViStatus    error = VI_SUCCESS;

    /* Check to make sure output variables are not null */
    if ((maxSDInstChan == VI_NULL) || (maxSDOperChan == VI_NULL) ||
        (maxRefChan == VI_NULL) ||
        (maxDSInstChan == VI_NULL) || (maxDSOperChan == VI_NULL))
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER5, "Null address for na65cs4_getModelInfo parameters");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }
    
    /* Initialize output variables */
    *maxSDInstChan = 0;
    *maxSDOperChan = 0; 
    *maxRefChan = 0;
    *maxDSInstChan = 0;
    *maxDSOperChan = 0;
    
    if (strcmp(modelName, NA65CS4_SUPPORTED_INSTRUMENT_MODELS) == 0)
    {
        /* Set the max channels for this instrument model */
        *maxSDInstChan = 2;
        *maxSDOperChan = 0;
        *maxRefChan = 1;
        *maxDSInstChan = 2;
        *maxDSOperChan = 0;
    }
    else
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER5, "Invalid modelName for na65cs4_getModelInfo parameters");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }

Error:
    return error;
}

/*============================================================================
 * Function: na65cs4_getModelParts                                        
 * Purpose:  This function provides a list of supported part number         
 *           designations.
 *===========================================================================*/
ViStatus _VI_FUNC na65cs4_getModelParts (ViString modelName, ViInt16 maxPartCnt,
                                         ViInt16 maxPartLength, ViInt16 *partCnt,
                                         char *supportedParts[])
{
    ViStatus    error = VI_SUCCESS;
    int         i;
    char        *pParts = supportedParts[0];
    

    /* Check to make sure output variables are not null */
    if ((supportedParts == VI_NULL) || (partCnt == VI_NULL))
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER5, "Null address for na65cs4_getModelParts parameters");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }
    
    /* Initialize output variables */
    *partCnt = 0;
    strcpy(pParts, ""); 
    
    if (strcmp(modelName, NA65CS4_SUPPORTED_INSTRUMENT_MODELS) == 0)
    {
        for (i = 0; i < PartNumberTableCnt; i++)
        {
            /* Only return up to the maxPartCnt requested by caller */
            if (i < maxPartCnt)
            {
                /* If the part number exceed the length of string buffer setup by
                   caller, copy only up to that maxlength - 1 */
                if (strlen(PartNumberTable[i].szPartNumber) >= maxPartLength)
                {
                    strncpy(pParts, PartNumberTable[i].szPartNumber, maxPartLength-1);
                    /* Increment the pointer to the end of the string */
                    pParts += (maxPartLength-1);
                    /* Terminate the string */
                    *pParts = '\0';
                    /* Increment the character pointer to move to the next string */
                    pParts++;
                }
                else
                {
                    strcpy(pParts, PartNumberTable[i].szPartNumber);
                    /* Increment the character pointer to move to the next string */
                    pParts += maxPartLength;
                }
            }
            else
                break;
        }
        *partCnt = i;
    }
    else
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER5, "Invalid modelName for na65cs4_getModelInfo parameters");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }
    
Error:
    return error;
}


/*===========================================================================
 * Function: na65cs4_getPartSpecInfo                                         
 * Purpose:  This function will return the specifications for the number of
 *           measurement (S/D) channels, the number of instrument-grade
 *           stimulus (D/S) channels, the number of operational-grade
 *           stimulus (D/S) channels, the number of reference channels, the
 *           frequency range for the reference channels, the reference output
 *           for Reference 1 and Reference 2, and the supported language
 *           interface for a given part specification.
/*===========================================================================*/
ViStatus _VI_FUNC na65cs4_getPartSpecInfo (ViString partSpecification,
                                           ViInt16 *numSDInstChan,
                                           ViInt16 *numSDOperChan,
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
                                           ViInt16 *langInterface)
{
    ViStatus    error = VI_SUCCESS;
    int i;
    ViBoolean   bFoundPart = VI_FALSE;

    /* Check to make sure output variables are not null */
    if ((numSDInstChan == VI_NULL) ||  (numSDOperChan == VI_NULL) ||
        (numDSInstChan == VI_NULL) || (numDSOperChan == VI_NULL) ||
        (numRefChan == VI_NULL) || (minRefFreq == VI_NULL) || (maxRefFreq == VI_NULL) || 
        (ref1PowerOutput == VI_NULL) || (ref2PowerOutput == VI_NULL) || (langInterface == VI_NULL))
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER5, "Null address for na65cs4_getPartSpecInfo parameters");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }
    
    /* Initialize output variables */
    *numSDInstChan = 0;
    *numSDOperChan = 0; 
    *numDSInstChan = 0;
    *numDSOperChan = 0;
    *numRefChan = 0;
    *minRefFreq = 0.0;
    *maxRefFreq = 0.0;
    *ref1PowerOutput = 0.0;
    *ref2PowerOutput = 0.0;
    *langInterface = NA65CS4_LANG_NATIVE;

    for (i = 0; i < PartNumberTableCnt; i++)
    {
        if (strcmp(partSpecification, PartNumberTable[i].szPartNumber) == 0)
        {
            *numSDInstChan = PartNumberTable[i].nSDInstChanCnt;
            *numSDOperChan = PartNumberTable[i].nSDOperChanCnt; 
            *numDSInstChan = PartNumberTable[i].nDSInstChanCnt;
            *numDSOperChan = PartNumberTable[i].nDSOperChanCnt;
            *numRefChan = PartNumberTable[i].nRefChanCnt;  
            *minRefFreq = PartNumberTable[i].fMinRefFreq;  
            *maxRefFreq = PartNumberTable[i].fMaxRefFreq;  
            *ref1PowerOutput = PartNumberTable[i].fRef1PowerOutput; 
            *ref1MinVolt = PartNumberTable[i].fRef1MinVolt;
            *ref1MaxVolt = PartNumberTable[i].fRef1MaxVolt;
            *ref2PowerOutput = PartNumberTable[i].fRef2PowerOutput;  
            *ref2MinVolt = PartNumberTable[i].fRef2MinVolt;
            *ref2MaxVolt = PartNumberTable[i].fRef2MaxVolt;
            *langInterface = PartNumberTable[i].nLanguage;
            
            bFoundPart = VI_TRUE;
            break;
        }
    }

    if (bFoundPart == VI_FALSE)
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER5, "Invalid Part Specification for na65cs4_getPartSpecInfo parameters");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }
    
Error:
    return error;
}


/*****************************************************************************
 * Function: na65cs4_query_language   
 * Purpose:  This function will query the instrument using the *LANG? command
 *           to determine the language support. Possible return values are
 *           Native", "SCPI", or "Mate CILL".
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_language (ViSession vi, ViChar language[])
{
    ViStatus    error = VI_SUCCESS;
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];

    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        viCheckErr( viPrintf (io, "*LANG?\r\n"));
        
        /* Get the result */
        tmpMsg[0] = '\0';
        viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
        
        /* Strip out the "\r\n" if there are any in the testMessage */
        na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, language);
    }
    
    checkErr( na65cs4_DefaultInstrSetup (vi));                                
    
    checkErr( na65cs4_CheckStatus (vi));                                      

Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
    
}


/*****************************************************************************
 * Function: na65cs4_query_id   
 * Purpose:  This function will query the instrument using the *IDN? command
 *           for instrument identification. Return the value in the
 *           following format: "north atlantic,<part number>,<serial #>,
 *           <firmware revision>
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_id (ViSession vi, ViChar identification[])
{
    ViStatus    error = VI_SUCCESS;
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];

    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        viCheckErr( viPrintf (io, "*IDN?\r\n"));
        
        /* Get the result */
        tmpMsg[0] = '\0';
        viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
        
        /* Strip out the "\r\n" if there are any in the testMessage */
        na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, identification);
    }
    
    checkErr( na65cs4_DefaultInstrSetup (vi));                                
    
    checkErr( na65cs4_CheckStatus (vi));                                      

Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_init   
 * Purpose:  VXIplug&play required function.  Calls the   
 *           na65cs4_InitWithOptions function.   
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_init (ViRsrc resourceName, ViBoolean IDQuery,
                                ViBoolean reset, ViSession *instrumentHandle)
{
    ViStatus    error = VI_SUCCESS;

    if (instrumentHandle == VI_NULL)
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER4, "Null address for Instrument Handle");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }

    checkErr( na65cs4_InitWithOptions (resourceName, IDQuery, reset, "", instrumentHandle));

Error:
    return error;
}


/*****************************************************************************
 * Function: na65cs4_InitWithOptions                                       
 * Purpose:  This function creates a new IVI session and calls the 
 *           IviInit function.                                     
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_InitWithOptions (ViRsrc resourceName,
                                           ViBoolean IDQuery, ViBoolean reset,
                                           ViString optionString,
                                           ViSession *instrumentHandle)
{
    ViStatus    error = VI_SUCCESS;
    ViSession   vi = VI_NULL;
    ViChar      newResourceName[IVI_MAX_MESSAGE_BUF_SIZE];
    ViChar      newOptionString[IVI_MAX_MESSAGE_BUF_SIZE];
    ViBoolean   isLogicalName;
    
    if (instrumentHandle == VI_NULL)
    {
        Ivi_SetErrorInfo (VI_NULL, VI_FALSE, IVI_ERROR_INVALID_PARAMETER, 
                          VI_ERROR_PARAMETER5, "Null address for Instrument Handle");
        checkErr( IVI_ERROR_INVALID_PARAMETER);
    }

    *instrumentHandle = VI_NULL;
    checkErr( Ivi_GetInfoFromResourceName (resourceName, (ViString) optionString, newResourceName,
                                           newOptionString, &isLogicalName));
    
    /* create a new interchangeable driver */
    checkErr( Ivi_SpecificDriverNew ("na65cs4", newOptionString, &vi));  
    if (!isLogicalName)
    {
        ViInt32 oldFlag = 0;
        
        checkErr (Ivi_GetAttributeFlags (vi, IVI_ATTR_IO_RESOURCE_DESCRIPTOR, &oldFlag));
        checkErr (Ivi_SetAttributeFlags (vi, IVI_ATTR_IO_RESOURCE_DESCRIPTOR, oldFlag & 0xfffb | 0x0010));
        checkErr (Ivi_SetAttributeViString (vi, "", IVI_ATTR_IO_RESOURCE_DESCRIPTOR, 0, newResourceName));
    }
        /* init the driver */
    checkErr( na65cs4_IviInit (newResourceName, IDQuery, reset, vi)); 
    if (isLogicalName)
        checkErr( Ivi_ApplyDefaultSetup (vi));
    *instrumentHandle = vi;
    
Error:
    if (error < VI_SUCCESS) 
        Ivi_Dispose (vi);
        
    return error;
}

/*****************************************************************************
 * Function: na65cs4_IviInit                                                       
 * Purpose:  This function is called by na65cs4_InitWithOptions  
 *           or by an IVI class driver.  This function initializes the I/O 
 *           interface, optionally resets the device, optionally performs an
 *           ID query, and sends a default setup to the instrument.                
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_IviInit (ViRsrc resourceName, ViBoolean IDQuery,
                                    ViBoolean reset, ViSession vi)
{
    ViStatus    error = VI_SUCCESS;
    ViSession   io = VI_NULL;
    ViChar      szBuffer[BUFFER_SIZE];
    ViChar      szLanguage[BUFFER_SIZE];    

     /* Add attributes */
    checkErr( na65cs4_InitAttributes (vi));

    if (!Ivi_Simulating(vi))
    {
        ViSession   rmSession = VI_NULL;

        /* Open instrument session */
        checkErr( Ivi_GetAttributeViSession (vi, VI_NULL, IVI_ATTR_VISA_RM_SESSION, 0,
                                             &rmSession)); 
        viCheckErr( viOpen (rmSession, resourceName, VI_NULL, VI_NULL, &io));
        /* io session owned by driver now */
        checkErr( Ivi_SetAttributeViSession (vi, VI_NULL, IVI_ATTR_IO_SESSION, 0, io));  

        /* Configure VISA Formatted I/O */
        viCheckErr( viSetAttribute (io, VI_ATTR_TMO_VALUE, 5000 ));
        viCheckErr( viSetBuf (io, VI_READ_BUF | VI_WRITE_BUF, 4000));
        viCheckErr( viSetAttribute (io, VI_ATTR_WR_BUF_OPER_MODE, VI_FLUSH_ON_ACCESS));
        viCheckErr( viSetAttribute (io, VI_ATTR_RD_BUF_OPER_MODE, VI_FLUSH_ON_ACCESS));
    }
        
    /*- Reset instrument ----------------------------------------------------*/
    if (reset) 
        checkErr( na65cs4_reset (vi));
    else  /*- Send Default Instrument Setup ---------------------------------*/
        checkErr( na65cs4_DefaultInstrSetup (vi));
    
    /*- Identification Query ------------------------------------------------*/
    if (IDQuery)
    {
        ViInt32 manfID = 0;
        ViInt32 modelCode = 0;
    
        checkErr( Ivi_GetAttributeViInt32 (vi, VI_NULL,
                                           NA65CS4_ATTR_VXI_MANF_ID,
                                           0, &manfID));
    
        checkErr( Ivi_GetAttributeViInt32 (vi, VI_NULL,
                                           NA65CS4_ATTR_VXI_MODEL_CODE,
                                           0, &modelCode));
    
        if ((manfID != NA65CS4_VXI_MANF_ID) || (modelCode != NA65CS4_VXI_MODEL_CODE))
            viCheckErr( VI_ERROR_FAIL_ID_QUERY);
    }

    checkErr( na65cs4_CheckStatus (vi));
    
    /* Also send the IDN command to power on the card and enable operation */
    checkErr( na65cs4_query_id(vi, szBuffer));
    
    /*- Language Query ------------------------------------------------*/
    /* TODO - *LANG? is new message and will be added to 65CS4 */
    /* error = na65cs4_query_language (vi, szLanguage);   */
    strcpy(szLanguage, "NATIVE");
    if (error == VI_SUCCESS)
    {
        if (strcmp(szLanguage, "SCPI") == 0)
            LANG = NA65CS4_LANG_SCPI;
        else if (strcmp(szLanguage, "Mate CILL") == 0)
            LANG = NA65CS4_LANG_CIIL;
        else
            LANG = NA65CS4_LANG_NATIVE;
    }
    else
        /* If there is an error, set the default language to Native */
        LANG = NA65CS4_LANG_NATIVE;

Error:
    if (error < VI_SUCCESS)
    {
        if (!Ivi_Simulating (vi) && io)
            viClose (io);
    }
    return error;
}

/*****************************************************************************
 * Function: na65cs4_close                                                           
 * Purpose:  This function closes the instrument.                            
 *
 *           Note:  This function must unlock the session before calling
 *           Ivi_Dispose.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_close (ViSession vi)
{
    ViStatus    error = VI_SUCCESS;
    
    checkErr( Ivi_LockSession (vi, VI_NULL));
    
    checkErr( na65cs4_IviClose (vi));

Error:    
    Ivi_UnlockSession (vi, VI_NULL);
    Ivi_Dispose (vi);  

    return error;
}

/*****************************************************************************
 * Function: na65cs4_IviClose                                                        
 * Purpose:  This function performs all of the drivers clean-up operations   
 *           except for closing the IVI session.  This function is called by 
 *           na65cs4_close or by an IVI class driver. 
 *
 *           Note:  This function must close the I/O session and set 
 *           IVI_ATTR_IO_SESSION to 0.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_IviClose (ViSession vi)
{
    ViStatus error = VI_SUCCESS;
    ViSession io = VI_NULL;

    /* Do not lock here.  The caller manages the lock. */

    checkErr( Ivi_GetAttributeViSession (vi, VI_NULL, IVI_ATTR_IO_SESSION, 0, &io));

Error:
    Ivi_SetAttributeViSession (vi, VI_NULL, IVI_ATTR_IO_SESSION, 0, VI_NULL);
    if(io)                                                      
    {
        viClose (io);
    }
    return error;   
}

/*****************************************************************************
 * Function: na65cs4_reset                                                         
 * Purpose:  This function resets the instrument.                          
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_reset (ViSession vi)
{
    ViStatus    error = VI_SUCCESS;

    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        viCheckErr( viPrintf (io, "*RST\r\n"));
    }
    
    checkErr( na65cs4_DefaultInstrSetup (vi));                                
    
    checkErr( na65cs4_CheckStatus (vi));                                      

Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_ResetWithDefaults
 * Purpose:  This function resets the instrument and applies default settings
 *           from the IVI Configuration Store based on the logical name
 *           from which the session was created.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_ResetWithDefaults (ViSession vi)
{
    ViStatus error = VI_SUCCESS;
    
    checkErr( Ivi_LockSession (vi, VI_NULL));
    checkErr( na65cs4_reset(vi));
    checkErr( Ivi_ApplyDefaultSetup(vi));
    
Error:
    Ivi_UnlockSession(vi, VI_NULL);
    return error;
}

/**************************************************************************** 
 *  Function: na65cs4_Disable
 *  Purpose:  This function places the instrument in a quiescent state as 
 *            quickly as possible.
 ****************************************************************************/
ViStatus _VI_FUNC na65cs4_Disable (ViSession vi)
{
    ViStatus error = VI_SUCCESS;
    
    checkErr( Ivi_LockSession (vi, VI_NULL));
    
    if (!Ivi_Simulating(vi)) 
    {
        /* NA65CS4 does not support the disabling of the instrument. */
    }
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_self_test                                                       
 * Purpose:  This function executes the instrument self-test and returns the 
 *           result.                                                         
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_self_test (ViSession vi, ViInt16 *testResult, 
                                      ViChar testMessage[])
{
    ViStatus    error = VI_SUCCESS;
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];

    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (testResult == VI_NULL)
        viCheckParm( IVI_ERROR_INVALID_PARAMETER, 2, "Null address for Test Result");
    if (testMessage == VI_NULL)
        viCheckParm( IVI_ERROR_INVALID_PARAMETER, 3, "Null address for Test Message");

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
    
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        viCheckErr( viPrintf (io, "*TST?\r\n"));
            
        /* Set timeout to 60 seconds */
        viCheckErr( viSetAttribute (io, VI_ATTR_TMO_VALUE, 60000 ));
        
        /* Get the result */
        tmpMsg[0] = '\0';
        viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
        
        /* Strip out the "\r\n" if there are any in the testMessage */
        na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, testMessage);
        
        if (strcmp(testMessage, "No error"))
            /* NA65CS4 does not report an error code. Set this value to 0 when there is no error */
            *testResult = 0;
        else
            /* NA65CS4 does not report an error code. Set this value to 1 when there is an error */
            *testResult = 1;
    
    }
    else
    {
        /* Simulate Self Test */
        *testResult = 0;
        strcpy (testMessage, "No error");
    }
    
    checkErr( na65cs4_CheckStatus (vi));

Error:
    Ivi_UnlockSession(vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_revision_query                                                  
 * Purpose:  This function returns the driver and instrument revisions.      
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_revision_query (ViSession vi, ViChar driverRev[], 
                                           ViChar instrRev[])
{
    ViStatus    error = VI_SUCCESS;
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (driverRev == VI_NULL)
        viCheckParm( IVI_ERROR_INVALID_PARAMETER, 2, "Null address for Driver Revision");
    if (instrRev == VI_NULL)
        viCheckParm( IVI_ERROR_INVALID_PARAMETER, 3, "Null address for Instrument Revision");

    checkErr( Ivi_GetAttributeViString (vi, VI_NULL, NA65CS4_ATTR_SPECIFIC_DRIVER_REVISION, 
                                        0, 256, driverRev));
    checkErr( Ivi_GetAttributeViString (vi, "", NA65CS4_ATTR_INSTRUMENT_FIRMWARE_REVISION, 
                                        0, 256, instrRev));
    checkErr( na65cs4_CheckStatus (vi));

Error:    
    Ivi_UnlockSession(vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_error_query                                                     
 * Purpose:  This function queries the instrument error queue and returns   
 *           the result.                                                     
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_error_query (ViSession vi, ViInt32 *errCode, 
                                        ViChar errMessage[])
{
    ViStatus    error = VI_SUCCESS;
    
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));
    
    if (errCode == VI_NULL)
        viCheckParm( IVI_ERROR_INVALID_PARAMETER, 2, "Null address for Error Code");
    if (errMessage == VI_NULL)
        viCheckParm( IVI_ERROR_INVALID_PARAMETER, 3, "Null address for Error Message");

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
    
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        viCheckErr( viPrintf(io, "*ERR?\r\n"));
    
        tmpMsg[0] = '\0';
        viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
        
        /* Strip out the "\r\n" if there are any in the errMessage */
        na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, errMessage);
        
        if (strcmp(errMessage, "No error"))
            /* NA65CS4 does not report an error code. Set this value to 0 when there is no error */
            *errCode = 0;
        else
            /* NA65CS4 does not report an error code. Set this value to 1 when there is an error */
            *errCode = 1;
        
    }
    else
    {
        /* Simulate Error Query */
        *errCode = 0;
        strcpy (errMessage, "No error");
    }

Error:
    Ivi_UnlockSession(vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_error_message                                                  
 * Purpose:  This function translates the error codes returned by this       
 *           instrument driver into user-readable strings.  
 *
 *           Note:  The caller can pass VI_NULL for the vi parameter.  This 
 *           is useful if one of the init functions fail.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_error_message (ViSession vi, ViStatus errorCode,
                                          ViChar errorMessage[256])
{
    ViStatus    error = VI_SUCCESS;
    
    static      IviStringValueTable errorTable = 
        {
        /*=CHANGE:================================================================*
            Insert instrument driver specific error codes here.  Example:                        

            {NA65CS4_ERROR_TOO_MANY_SAMPLES,   "Sample Count cannot exceed 512."},   
         *=============================================================END=CHANGE=*/
            {VI_NULL,                               VI_NULL}
        };
        
    if (vi)
        Ivi_LockSession(vi, VI_NULL);

        /* all VISA and IVI error codes are handled as well as codes in the table */
    if (errorMessage == VI_NULL)
        viCheckParm( IVI_ERROR_INVALID_PARAMETER, 3, "Null address for Error Message");

    checkErr( Ivi_GetSpecificDriverStatusDesc(vi, errorCode, errorMessage, errorTable));

Error:
    if (vi)
        Ivi_UnlockSession(vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_InvalidateAllAttributes
 * Purpose:  This function invalidates the cached value of all attributes.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_InvalidateAllAttributes (ViSession vi)
{
    return Ivi_InvalidateAllAttributes(vi);
}


/*****************************************************************************
 * Function: na65cs4_GetError and na65cs4_ClearError Functions                       
 * Purpose:  These functions enable the instrument driver user to  
 *           get or clear the error information the driver associates with the
 *           IVI session.                                                        
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_GetError (ViSession vi, 
                                     ViStatus *errorCode, 
                                     ViInt32 bufferSize,
                                     ViChar description[])  
{
    ViStatus error = VI_SUCCESS;
    ViStatus primary = VI_SUCCESS,
             secondary = VI_SUCCESS;
    ViChar   elaboration[256] = "";
    ViChar   errorMessage[1024] = "";
    ViChar  *appendPoint = errorMessage;
    ViInt32  actualSize = 0;
    ViBoolean locked = VI_FALSE;

    /* Lock Session */
    if (vi != 0)
    {
        checkErr( Ivi_LockSession(vi, &locked));
    }

    /* Test for nulls and acquire error data */
    if (bufferSize != 0)
    {
        if (errorCode == VI_NULL)
        {
            viCheckParm( IVI_ERROR_INVALID_PARAMETER, 2, "Null address for Error");
        }
        if (description == VI_NULL)
        {
            viCheckParm( IVI_ERROR_INVALID_PARAMETER, 4, "Null address for Description");
        }
        checkErr( Ivi_GetErrorInfo (vi, &primary, &secondary, elaboration));
    }

    else
    {
        checkErr( Ivi_GetAttributeViString(vi, VI_NULL, IVI_ATTR_ERROR_ELABORATION, 0, 256, elaboration));
        checkErr( Ivi_GetAttributeViInt32(vi, VI_NULL, IVI_ATTR_SECONDARY_ERROR, 0, &secondary));
        checkErr( Ivi_GetAttributeViInt32(vi, VI_NULL, IVI_ATTR_PRIMARY_ERROR, 0, &primary));
    }
        
    /* Format data */
    if (primary != VI_SUCCESS)
    {
        ViChar msg[256] = "";
        checkErr( na65cs4_error_message (vi, primary, msg));
        appendPoint += sprintf(appendPoint, "Primary Error: (Hex 0x%08X) %s\n", primary, msg);
    }
    
    if (secondary != VI_SUCCESS)
    {
        ViChar msg[256] = "";
        checkErr( na65cs4_error_message (vi, secondary, msg));
        appendPoint += sprintf(appendPoint, "Secondary Error: (Hex 0x%08X) %s\n", secondary, msg);
    }
    
    if (elaboration[0])
    {
        sprintf(appendPoint, "Elaboration: %s", elaboration);
    }
    
    actualSize = strlen(errorMessage) + 1;
    
    /* Prepare return values */
    if (bufferSize == 0)
    {
        error = actualSize;
    }
    else 
    {
        if (bufferSize > 0)
        {
            if (actualSize > bufferSize)
            {
                error = actualSize;
                actualSize = bufferSize;
            }
        }
        memcpy(description, errorMessage, actualSize-1);
        description[actualSize-1] = '\0';
    }
    
    if (errorCode) 
    {
        *errorCode = primary;
    }
    
Error:
    /* Unlock Session */
    Ivi_UnlockSession (vi, &locked);
    return error;
}   

ViStatus _VI_FUNC na65cs4_ClearError (ViSession vi)                                                        
{                                                                                                           
    return Ivi_ClearErrorInfo (vi);                                                                             
}

/*****************************************************************************
 * Function: na65cs4_GetNextCoercionRecord                        
 * Purpose:  This function enables the instrument driver user to obtain
 *           the coercion information associated with the IVI session.                                                              
 *           This function retrieves and clears the oldest instance in which 
 *           the instrument driver coerced a value the instrument driver user
 *           specified to another value.                     
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_GetNextCoercionRecord (ViSession vi,
                                                  ViInt32 bufferSize,
                                                  ViChar  record[])
{
    return Ivi_GetNextCoercionString (vi, bufferSize, record);
}

/**************************************************************************** 
 *  Function: na65cs4_GetNextInterchangeWarning,
 *            na65cs4_ResetInterchangeCheck, and
 *            na65cs4_ClearInterchangeWarnings
 *  Purpose:  These functions allow the user to retrieve interchangeability
 *            warnings, reset the driver's interchangeability checking 
 *            state, and clear all previously logged interchangeability warnings.
 ****************************************************************************/
ViStatus _VI_FUNC na65cs4_GetNextInterchangeWarning (ViSession vi, 
                                                      ViInt32 bufferSize, 
                                                      ViChar warnString[])
{
    return Ivi_GetNextInterchangeCheckString (vi, bufferSize, warnString);
}

ViStatus _VI_FUNC na65cs4_ResetInterchangeCheck (ViSession vi)
{
    return Ivi_ResetInterchangeCheck (vi);
}

ViStatus _VI_FUNC na65cs4_ClearInterchangeWarnings (ViSession vi)
{
    return Ivi_ClearInterchangeWarnings (vi);
}

/*****************************************************************************
 * Function: na65cs4_LockSession and na65cs4_UnlockSession Functions                        
 * Purpose:  These functions enable the instrument driver user to lock the 
 *           session around a sequence of driver calls during which other
 *           execution threads must not disturb the instrument state.
 *                                                                          
 *           NOTE:  The callerHasLock parameter must be a local variable 
 *           initialized to VI_FALSE and passed by reference, or you can pass 
 *           VI_NULL.                     
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_LockSession (ViSession vi, ViBoolean *callerHasLock)  
{                                              
    return Ivi_LockSession(vi,callerHasLock);      
}                                              
ViStatus _VI_FUNC na65cs4_UnlockSession (ViSession vi, ViBoolean *callerHasLock) 
{                                              
    return Ivi_UnlockSession(vi,callerHasLock);    
}   


/*****************************************************************************
 * Function: na65cs4_GetAttribute<type> Functions                                    
 * Purpose:  These functions enable the instrument driver user to get 
 *           attribute values directly.  There are typesafe versions for 
 *           ViInt32, ViReal64, ViString, ViBoolean, and ViSession attributes.                                         
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_GetAttributeViInt32 (ViSession vi, ViConstString channelName, 
                                                ViAttr attributeId, ViInt32 *value)
{                                                                                                           
    return Ivi_GetAttributeViInt32 (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                    value);
}                                                                                                           
ViStatus _VI_FUNC na65cs4_GetAttributeViReal64 (ViSession vi, ViConstString channelName, 
                                                 ViAttr attributeId, ViReal64 *value)
{                                                                                                           
    return Ivi_GetAttributeViReal64 (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                     value);
}                                                                                                           
ViStatus _VI_FUNC na65cs4_GetAttributeViString (ViSession vi, ViConstString channelName, 
                                                 ViAttr attributeId, ViInt32 bufSize, 
                                                 ViChar value[]) 
{   
    return Ivi_GetAttributeViString (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                     bufSize, value);
}   
ViStatus _VI_FUNC na65cs4_GetAttributeViBoolean (ViSession vi, ViConstString channelName, 
                                                  ViAttr attributeId, ViBoolean *value)
{                                                                                                           
    return Ivi_GetAttributeViBoolean (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                      value);
}                                                                                                           
ViStatus _VI_FUNC na65cs4_GetAttributeViSession (ViSession vi, ViConstString channelName, 
                                                  ViAttr attributeId, ViSession *value)
{                                                                                                           
    return Ivi_GetAttributeViSession (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                      value);
}                                                                                                           

/*****************************************************************************
 * Function: na65cs4_config_SD                                    
 * Purpose:  This functions sets the channel's configurable settings. 
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SD (ViSession vi, ViInt16 channel,
                                     ViInt16 channelGrade,
                                     ViInt16 signalMode, ViInt16 bandwidth,
                                     ViInt16 refSourceMode, ViInt16 DCScale,
                                     ViInt16 ratio, ViInt16 relayIOState,
                                     ViInt16 updateMode, ViInt16 maxWaitTime_Sec)
{
    ViStatus    error = VI_SUCCESS;
    
    checkErr ( na65cs4_config_SDSignalMode(vi, channel,channelGrade, signalMode));
    checkErr ( na65cs4_config_SDBandwidth(vi, channel,channelGrade, bandwidth));
    checkErr ( na65cs4_config_SDRefSrcMode(vi, channel,channelGrade, refSourceMode));
    checkErr ( na65cs4_config_SDDCScale(vi, channel,channelGrade, DCScale));
    checkErr ( na65cs4_config_SDRatio(vi, channel,channelGrade, ratio));
    checkErr ( na65cs4_config_SDRelayIOState(vi, channel,channelGrade, relayIOState));
    checkErr ( na65cs4_config_SDUpdateMode(vi, channel,channelGrade, updateMode));
    checkErr ( na65cs4_config_SDMaxAngSettleTime(vi, channel,channelGrade, maxWaitTime_Sec));

Error:
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_SDDCScale                                    
 * Purpose:  This function sets the measurement channel DC output scale. Full
 *           scale is 10 Volts. Range: 100 <= DC Scale <= 1000.
 *
 * The value entered for the DC output scale determines the DC
 * output voltage. For example:
 * DC output scale value = 100 sets +/-100 deg/sec = +/-10 volts
 * DC output scale value = 1000 sets +/-1000 deg/sec = +/-10 volts 
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SDDCScale (ViSession vi, ViInt16 channel,
                                            ViInt16 channelGrade, 
                                            ViInt16 DCScale)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
  
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
           if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");     
           if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");    
                                                                                        
           sprintf(DutCmd,"%s%u DC_SCALE %u\r\n",ChanPrefix,channel,DCScale);                
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi));   

Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_SDBandwidth                                    
 * Purpose:  This function sets bandwidth for the measurement channel. High
 *           Bandwidth for 100 Hz or Low Bandwidth for 10 Hz.  Note, use Low
 *           Bandwidth for carrier (reference) frequency < 300 Hz.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SDBandwidth (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade, 
                                              ViInt16 bandwidth)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
   
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
           if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");       
           if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");      
                                                                                          
               if (bandwidth == NA65CS4_HIGH_BW)                                        
                   sprintf(DutCmd,"%s%u BANDWIDTH HIGH\r\n",ChanPrefix,channel);                
                                                                                          
               else if (bandwidth == NA65CS4_LOW_BW)                                    
                   sprintf(DutCmd,"%s%u BANDWIDTH LOW\r\n",ChanPrefix,channel);                
        
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi));   
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_SDMaxAngSettleTime                                    
 * Purpose:  This function sets the maximum wait time in seconds to wait for
 *           the API (S/D) channel to settle before reading the measurement.
 *           Allowable range is 0 to 20.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SDMaxAngSettleTime (ViSession vi,ViInt16 channel,
                                                     ViInt16 channelGrade, 
                                                     ViInt16 maxWaitTime_Sec)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
          if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");   
          if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");  
                                                                                     
          sprintf(DutCmd,"%s%u MAXT %u\r\n",ChanPrefix,channel,maxWaitTime_Sec);              
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi));   
 
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_SDSignalMode                                    
 * Purpose:  This function sets the measurement channel signal format mode to
 *           either Resolver or Synchro.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SDSignalMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade, 
                                               ViInt16 mode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
          if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
          if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                      
              if (mode == NA65CS4_RESOLVER)                                          
                  sprintf(DutCmd,"%s%u MODE RSL\r\n",ChanPrefix,channel);       
                                                                                      
              else if (mode == NA65CS4_SYNCHRO)                                     
                  sprintf(DutCmd,"%s%u MODE SYN\r\n",ChanPrefix,channel);       
        
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_SDRatio                                    
 * Purpose:  This function sets the measurement channel 2-speed/Multi-speed
 *           ratio for EVEN channels only. The ratio cannot be set for ODD
 *           channels and will be ignored. Valid ratio range is 1 to 255.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SDRatio (ViSession vi, ViInt16 channel,
                                          ViInt16 channelGrade, 
                                          ViInt16 ratio)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
           if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
           if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                   
           sprintf(DutCmd,"%s%u RATIO %u\r\n",ChanPrefix,channel,ratio);       
        
   
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi));   
 
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_SDRefSrcMode                                    
 * Purpose:  This function sets the measurement channel's reference source to
 *           either Internal or External. Note, Internal Reference Source for
 *           Channels 3 and 4 is Reference 2.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SDRefSrcMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade, 
                                               ViInt16 refSourceMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
          if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");   
          if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");  
                                                                                     
              if (refSourceMode == NA65CS4_INT)                                        
                  sprintf(DutCmd,"%s%u REF_SOURCE INT\r\n",ChanPrefix,channel);          
                                                                                     
              else if (refSourceMode == NA65CS4_EXT)                                  
                  sprintf(DutCmd,"%s%u REF_SOURCE EXT\r\n",ChanPrefix,channel);         
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_SDRelayIOState                                    
 * Purpose:  This function sets the measurement channel's I/O isolation relay
 *           state to either Open or Close.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SDRelayIOState (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade, 
                                                 ViInt16 relayIOState)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
           if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
           if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                       
               if (relayIOState == NA65CS4_OPEN)                                        
                   sprintf(DutCmd,"%s%u STATE OPEN\r\n",ChanPrefix,channel);         
                                                                                       
               else if (relayIOState == NA65CS4_CLOSE)                                   
                   sprintf(DutCmd,"%s%u STATE CLOSE\r\n",ChanPrefix,channel);         
        
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_configSDUpdateMode                                    
 * Purpose:  This function sets the measurement channel's update mode to
 *           either Latch or Track.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_SDUpdateMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 updateMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = "";
    
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
        if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");
        if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");
        
            if (updateMode == NA65CS4_LATCH)
                sprintf(DutCmd,"%s%u UPDATE LATCH\r\n",ChanPrefix,channel);
                
            else if (updateMode == NA65CS4_TRACK)
                sprintf(DutCmd,"%s%u UPDATE TRACK\r\n",ChanPrefix,channel);
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSRotation                                    
 * Purpose:  This functions sets the stimulus (DS) channel's configurable
 *           settings for angle rotation.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSRotation (ViSession vi, ViInt16 channel,
                                             ViInt16 channelGrade,
                                             ViReal64 rotationRate,
                                             ViReal64 rotationStopAngle,
                                             ViInt16 rotationMode)
{
    ViStatus    error = VI_SUCCESS;
    
    checkErr ( na65cs4_config_DSRotationRate(vi, channel, channelGrade, rotationRate));
    checkErr ( na65cs4_config_DSRotateStopAng(vi, channel, channelGrade, rotationStopAngle));
    checkErr ( na65cs4_config_DSRotationMode(vi, channel, channelGrade, rotationMode));

Error:
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSAngle                                    
 * Purpose:  This function sets the stimulus channel output angle in degrees.
 *           The range for the output angle is -359.9999 to 359.9999 degrees.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSAngle (ViSession vi, ViInt16 channel,
                                          ViInt16 channelGrade, ViReal64 angle)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
         if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");     
         if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");    
                                                                                        
           sprintf(DutCmd,"%s%u ANGLE %f\r\n",ChanPrefix,channel,angle);     
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSDCScale                                    
 * Purpose:  This function sets the stimulus channel's DC output scale. Full
 *           scale is 10 Volts. Range: 100 <= DC Scale <= 1000.
 *
 * The value entered for the DC output scale determines the DC
 * output voltage. For example:
 * DC output scale value = 100 sets +/-100 deg/sec = +/-10 volts
 * DC output scale value = 1000 sets +/-1000 deg/sec = +/-10 volts
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSDCScale (ViSession vi, ViInt16 channel,
                                            ViInt16 channelGrade,
                                            ViInt16 DCScale)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
         if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");     
         if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");    
                                                                                        
           sprintf(DutCmd,"%s%u DC_SCALE %u\r\n",ChanPrefix,channel,DCScale);       
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSSignalMode                                    
 * Purpose:  This function sets the stimulus channel signal format mode to
 *           either Resolver or Synchro.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSSignalMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 signalMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
          if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");
          if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");
        
            if (signalMode == NA65CS4_SYNCHRO)
                sprintf(DutCmd,"%s%u MODE SYN\r\n",ChanPrefix,channel);
                
            else if (signalMode == NA65CS4_RESOLVER)
                sprintf(DutCmd,"%s%u MODE RSL\r\n",ChanPrefix,channel);  
            
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_DSRatio                                    
 * Purpose:  This function sets the stimulus channel 2-speed/Multi-speed
 *           ratio for EVEN channels only. The ratio cannot be set for ODD
 *           channels and will be ignored. Valid ratio range is 1 to 255.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSRatio (ViSession vi, ViInt16 channel,
                                          ViInt16 channelGrade, ViInt16 ratio)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
          if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");     
          if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");    
                                                                                        
           sprintf(DutCmd,"%s%u RATIO %u\r\n",ChanPrefix,channel,ratio);    
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_DSRefSrcMode                                    
 * Purpose:  This function sets the stimulus channel reference source to
 *           either Internal or External. Note, Internal Reference Source for
 *           Channels 1 and 2 is Reference 1 and Internal Reference Source
 *           for Channels 3 and 4 is Reference 2.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSRefSrcMode (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 refSourceMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
         if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");
         if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");
        
            if (refSourceMode == NA65CS4_INT)
                sprintf(DutCmd,"%s%u REF_SOURCE INT\r\n",ChanPrefix,channel);
                
            else if (refSourceMode == NA65CS4_EXT)
                sprintf(DutCmd,"%s%u REF_SOURCE EXT\r\n",ChanPrefix,channel);   
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSRelayIOState                                    
 * Purpose:  This function sets the stimulus channel I/O isolation relay
 *           state to either Open or Close.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSRelayIOState (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViInt16 relayIOState)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
         if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");
         if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");
        
            if (relayIOState == NA65CS4_OPEN)
                sprintf(DutCmd,"%s%u STATE OPEN\r\n",ChanPrefix,channel);
                
            else if (relayIOState == NA65CS4_CLOSE)
                sprintf(DutCmd,"%s%u STATE CLOSE\r\n",ChanPrefix,channel);   
            
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSLineToLineVoltage                                    
 * Purpose:  This function sets the stimulus channel line-to-line voltage in
 *           volts. The valid range for the line-to-line voltage is 1 to 90
 *           volts.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSLineToLineVoltage (ViSession vi,
                                                      ViInt16 channel,
                                                      ViInt16 channelGrade,
                                                      ViReal64 lineToLine_v)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
         if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");     
         if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");    
                                                                                        
           sprintf(DutCmd,"%s%u VLL_VOLT %f\r\n",ChanPrefix,channel,lineToLine_v);     
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSInputRefVolt                                    
 * Purpose:  This function sets the stimulus channel input reference voltage
 *           in Volts. This does not apply when the channel reference source
 *           is internal and will be ignored. The valid range for the input
 *           reference voltage is 2 to 115 volts.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSInputRefVolt (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViReal64 inputRefVoltage_v)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
          if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");     
          if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");    
                                                                                        
           sprintf(DutCmd,"%s%u REF_VOLT_IN %f\r\n",ChanPrefix,channel,inputRefVoltage_v);    
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSRotationRate                                    
 * Purpose:  This function sets the stimulus channel rotation rate in
 *           revolutions per second (RPS). The valid range for the rotation
 *           rate is 0.15 to 13.60 and 0 to stop rotation.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSRotationRate (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViReal64 rotationRate)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
         if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");     
         if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");    
                                                                                        
           sprintf(DutCmd,"%s%u ROT_RATE %f\r\n",ChanPrefix,channel,rotationRate);     
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSRotateStopAng                                    
 * Purpose:  This function sets the stimulus channel rotation stop angle in
 *           degrees. The valid range for the rotation stop angle is
 *           -359.9999 to 359.9999 degrees.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSRotateStopAng (ViSession vi, ViInt16 channel,
                                                  ViInt16 channelGrade,
                                                  ViReal64 rotationStopAngle)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
         if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");     
         if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");    
                                                                                        
           sprintf(DutCmd,"%s%u ROT_STOP_ANGLE %f\r\n",ChanPrefix,channel,rotationStopAngle);     
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_DSRotationMode                                    
 * Purpose:  This function sets the stimulus channel rotation mode to either
 *           Continuous or Step.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSRotationMode (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViInt16 rotationMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
          if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");
          if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");
        
            if (rotationMode == NA65CS4_CONT)
                sprintf(DutCmd,"%s%u ROT_MODE CONT\r\n",ChanPrefix,channel);
                
            else if (rotationMode == NA65CS4_STEP)
                sprintf(DutCmd,"%s%u ROT_MODE STEP\r\n",ChanPrefix,channel);  
            
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_DSTriggerSrc                                    
 * Purpose:  This function sets the stimulus channel trigger source to Bus,
 *           Internal, External, or TTL Level.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSTriggerSrc (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViInt16 triggerSource)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            /* Only Instrument Grade DS Channels are supported in the 65CS4 */
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE)
            {
             if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");
             if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");
        
            if (triggerSource == NA65CS4_INT)
                sprintf(DutCmd,"%s%u TRIG_SOURCE INT\r\n",ChanPrefix,channel);
                
            else if (triggerSource == NA65CS4_EXT)
                sprintf(DutCmd,"%s%u TRIG_SOURCE EXT\r\n",ChanPrefix,channel);
            
            else if (triggerSource == NA65CS4_BUS)
                sprintf(DutCmd,"%s%u TRIG_SOURCE BUS\r\n",ChanPrefix,channel);
                
            else if (triggerSource == NA65CS4_TTL_00)
                sprintf(DutCmd,"%s%u TRIG_SOURCE TTL0\r\n",ChanPrefix,channel);    
            
            else if (triggerSource == NA65CS4_TTL_01)
                sprintf(DutCmd,"%s%u TRIG_SOURCE TTL1\r\n",ChanPrefix,channel);
            
            else if (triggerSource == NA65CS4_TTL_02)
                sprintf(DutCmd,"%s%u TRIG_SOURCE TTL2\r\n",ChanPrefix,channel);
                
            else if (triggerSource == NA65CS4_TTL_03)
                sprintf(DutCmd,"%s%u TRIG_SOURCE TTL3\r\n",ChanPrefix,channel);
                
            else if (triggerSource == NA65CS4_TTL_04)
                sprintf(DutCmd,"%s%u TRIG_SOURCE TTL4\r\n",ChanPrefix,channel);
                
            else if (triggerSource == NA65CS4_TTL_05)
                sprintf(DutCmd,"%s%u TRIG_SOURCE TTL5\r\n",ChanPrefix,channel);
                
            else if (triggerSource == NA65CS4_TTL_06)
                sprintf(DutCmd,"%s%u TRIG_SOURCE TTL6\r\n",ChanPrefix,channel);
                
            else if (triggerSource == NA65CS4_TTL_07)
                sprintf(DutCmd,"%s%u TRIG_SOURCE TTL7\r\n",ChanPrefix,channel);
           }
            
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_DSTriggerSrc                                    
 * Purpose:  This function sets the stimulus channel trigger slope to either
 *           Negative or Positive.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSTriggerSlope (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViInt32 triggerSlope)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
         if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");
         if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");
        
            if (triggerSlope == NA65CS4_NEG)
                sprintf(DutCmd,"%s%u TRIG_SLOPE NEG\r\n",ChanPrefix,channel);
                
            else if (triggerSlope == NA65CS4_POS)
                sprintf(DutCmd,"%s%u TRIG_SLOPE POS\r\n",ChanPrefix,channel);  
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_DSPhaseShift                                    
 * Purpose:  This function sets the stimulus channel phase shift in degrees.
 *           The valid range for the phase shift is -179.9 to 179.9 degrees.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_DSPhaseShift (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade,
                                               ViReal64 phaseShift)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
          if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");     
          if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");    
                                                                                        
           sprintf(DutCmd,"%s%u PHASE %f\r\n",ChanPrefix,channel,phaseShift);     
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_REF                                    
 * Purpose:  This functions sets the stimulus (DS) channel's configurable
 *           settings for angle rotation.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_REF (ViSession vi, ViInt16 channel,
                                      ViReal64 frequency_hz, ViReal64 voltage_v,
                                      ViInt16 relayIOState)
{
    ViStatus    error = VI_SUCCESS;
    
    checkErr ( na65cs4_config_REFFreq(vi, channel, frequency_hz));
    checkErr ( na65cs4_config_REFVolt(vi, channel, voltage_v));
    checkErr ( na65cs4_config_REFRelayIOState(vi, channel, relayIOState));

Error:
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_REFFreq                                    
 * Purpose:  This function sets the reference generator channel frequency in
 *           hertz. The valid range for the frequency is based on the Part
 *           Number Designation.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_REFFreq (ViSession vi, ViInt16 channel,
                                          ViReal64 frequency_Hz)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
            sprintf(DutCmd,"REF_GEN%u FREQ %f\r\n",channel,frequency_Hz);
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi));   

Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_REFVolt                                    
 * Purpose:  This function sets the reference generator channel voltage in
 *           volts. The valid range for the reference generator voltage is 2
 *           to 115 volts..
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_REFVolt (ViSession vi, ViInt16 channel,
                                          ViReal64 voltage_v)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
            sprintf(DutCmd,"REF_GEN%u VOLT %f\r\n",channel,voltage_v);
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi));   

Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}


/*****************************************************************************
 * Function: na65cs4_config_REFRelayIOState                                    
 * Purpose:  This function sets the reference generator channel I/O isolation
 *           relay state to either Open or Close.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_config_REFRelayIOState (ViSession vi, ViInt16 channel,
                                                  ViInt16 relayIOState)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (relayIOState == NA65CS4_OPEN)
                sprintf(DutCmd,"REF_GEN%u STATE OPEN\r\n",channel);
            else if (relayIOState == NA65CS4_CLOSE)
                sprintf(DutCmd,"REF_GEN%u STATE CLOSE\r\n",channel);
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_config_REFRelayIOState                                    
 * Purpose:  This function initiates the rotation of the channel output.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_initiate_DSRotation (ViSession vi, ViInt16 channel,
                                               ViInt16 channelGrade)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = "";  
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
             if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");
             if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");
        
                sprintf(DutCmd,"%s%u ROT_INIT\r\n",ChanPrefix,channel);
        }
        
        viCheckErr( viPrintf(io,DutCmd) );
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_get_DSRotationComplete                                    
 * Purpose:  This function returns the Step Rotation Status.  This only
 *           applies when in Step Rotation Mode.  This function will set the
 *           Rotation_Complete variable to VI_TRUE if the Step Rotation is
 *           Done, otherwise it will return VI_FALSE.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_get_DSRotationComplete (ViSession vi, ViInt16 channel,
                                                  ViInt16 channelGrade,
                                                  ViBoolean *rotationComplete)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
           
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u ROT_DONE?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) );
                
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
        
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                if (stricmp(tmpMsg2, "YES") == 0)
                    *rotationComplete = VI_TRUE;
                else
                    *rotationComplete = VI_FALSE;
         
        }
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDAngle                                    
 * Purpose:  This function returns the measurement channel angle position in
 *           degrees. The range returned for the angle position is 0 to
 *           359.999.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDAngle (ViSession vi, ViInt16 channel,
                                         ViInt16 channelGrade,
                                         ViReal64 *angle)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");   
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");  
        
            sprintf(DutCmd, "%s%u ANGLE?\r\n",ChanPrefix,channel);
            viCheckErr( viPrintf(io,DutCmd) );
                
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            *angle = atof(tmpMsg2);
        
        }
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDVelocity                                    
 * Purpose:  This function returns the measurement channel velocity in
 *           degrees/second. The range returned for the velocity is -32767
 *           and 32767.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDVelocity (ViSession vi, ViInt16 channel,
                                            ViInt16 channelGrade,
                                            ViReal64 *velocity)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u VEL?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) );
                
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            *velocity = atof(tmpMsg2);
        
        }
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDDCScale                                    
 * Purpose:  This function returns the measurement channel DC Scale value.
 *           The range returned for the DC Scale is 100 to 1000.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDDCScale (ViSession vi, ViInt16 channel,
                                           ViInt16 channelGrade,
                                           ViInt16 *DCScale)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u DC_SCALE?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) );   
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            *DCScale = atol(tmpMsg2);
        
        }
        
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDBandwidth                                    
 * Purpose:  This function returns the measurement channel bandwidth setting.
 *           The possible return values are:
 *
 *           NA65CS4_HIGH_BW = 0   (HIGH bandwidth)
 *           NA65CS4_LOW_BW  = 1   (LOW bandwidth)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDBandwidth (ViSession vi, ViInt16 channel,
                                             ViInt16 channelGrade,
                                             ViInt16 *bandwidth)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u BANDWIDTH?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) );  
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            
            if (stricmp(tmpMsg2, "HIGH") == 0)
                *bandwidth = NA65CS4_HIGH_BW;
            else
                *bandwidth = NA65CS4_LOW_BW;
        
        }
        
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDMaxAngTime                                    
 * Purpose:  This function returns the measurement channel maximum angle wait
 *           time in seconds. The range returned for the maximum angle settle
 *           time is 0 to 20.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDMaxAngTime (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *maxAngleSettleTime)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u MAXT?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) ); 
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            *maxAngleSettleTime = atof(tmpMsg2);
        
        }
        
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDSignalMode                                    
 * Purpose:  This function returns the measurement channel signal format mode
 *           setting. The possible return values are:
 *
 *           NA65CS4_RESOLVER = 0   (RESOLVER Signal Format Mode)
 *           NA65CS4_SYNCHRO  = 1   (SYNCRHO Signal Format Mode)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDSignalMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *signalMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u MODE?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) );  
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            
            if (stricmp(tmpMsg2, "RSL") == 0)
                *signalMode = NA65CS4_RESOLVER;
            else
                *signalMode = NA65CS4_SYNCHRO;
        
        }
        
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDRatio                                    
 * Purpose:  This function returns the measurement channel
 *           one-speed/two-speed/multi-speed ratio setting. The range
 *           returned for the ratio is 1 to 255 for EVEN channels, 1 if an
 *           ODD channel is specified.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDRatio (ViSession vi, ViInt16 channel,
                                         ViInt16 channelGrade,
                                         ViInt16 *ratio)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u RATIO?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) );  
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            *ratio = atol(tmpMsg2);
        
        }
        
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDRefSrcMode                                    
 * Purpose:  This function returns the measurement reference source setting.
 *           The possible return values are:
 *
 *           NA65CS4_INT = 0   (INTERNAL Reference Source)
 *           NA65CS4_EXT = 1   (EXTERNAL Reference Source)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDRefSrcMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *refSourceMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u REF_SOURCE?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) ); 
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            
            if (stricmp(tmpMsg2, "INT") == 0)
                *refSourceMode = NA65CS4_INT;
            else
                *refSourceMode = NA65CS4_EXT;
        
        }
        
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDRelayIOState                                    
 * Purpose:  This function returns the measurement channel relay I/O state.
 *           The possible return values are:
 *
 *           NA65CS4_OPEN  = 0   (OPENED relay status)
 *           NA65CS4_CLOSE = 1   (CLOSED relay status)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDRelayIOState (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViInt16 *relayIOState)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u STATE?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) ); 
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            
            if (stricmp(tmpMsg2, "OPEN") == 0)
                *relayIOState = NA65CS4_OPEN;
            else
                *relayIOState = NA65CS4_CLOSE;
        
        }
  
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_SDUpdateMode                                    
 * Purpose:  This function returns the measurement update mode setting.
 *           The possible return values are:
 *
 *           NA65CS4_LATCH  = 0   (LATCH Update Mode)
 *           NA65CS4_TRACK  = 1   (TRACK Update Mode)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_SDUpdateMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *updateMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"SDH");    
            if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"SDL");   
                                                                                        
            sprintf(DutCmd, "%s%u UPDATE?\r\n",ChanPrefix,channel);                      
            viCheckErr( viPrintf(io,DutCmd) );   
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            
            if (stricmp(tmpMsg2, "LATCHED") == 0)
                *updateMode = NA65CS4_LATCH;
            else
                *updateMode = NA65CS4_TRACK;
        
        }
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSAngle                                    
 * Purpose:  This function returns the measurement channel angle position in
 *           degrees. The range returned for the angle position is 0 to
 *           359.999.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSAngle (ViSession vi, ViInt16 channel,
                                         ViInt16 channelGrade, ViReal64 *angle)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
           
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u ANGLE?\r\n",ChanPrefix,channel);   
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                *angle = atof(tmpMsg2);
            }
         
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSDCScale                                    
 * Purpose:  This function returns the measurement channel DC Scale value.
 *           The range returned for the DC Scale is 100 to 1000.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSDCScale (ViSession vi, ViInt16 channel,
                                           ViInt16 channelGrade,
                                           ViInt16 *DCScale)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u DC_SCALE?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                *DCScale = atol(tmpMsg2);
            }
           
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSSignalMode                                    
 * Purpose:  This function returns the stimulus channel signal format mode
 *           setting. The possible return values are:
 *
 *           NA65CS4_RESOLVER = 0   (RESOLVER Signal Format Mode)
 *           NA65CS4_SYNCHRO  = 1   (SYNCRHO Signal Format Mode)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSSignalMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *signalMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    ViChar      ChanPrefix[25] = ""; 
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u MODE?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                if (stricmp(tmpMsg2, "RSL") == 0)
                    *signalMode = NA65CS4_RESOLVER;
                else
                    *signalMode = NA65CS4_SYNCHRO;
         }
           
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSRatio                                    
 * Purpose:  This function returns the stimulus channel
 *           one-speed/two-speed/multi-speed ratio setting. The range
 *           returned for the ratio is 1 to 255 for EVEN channels, 1 if an
 *           ODD channel is specified.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSRatio (ViSession vi, ViInt16 channel,
                                         ViInt16 channelGrade, ViInt16 *ratio)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u RATIO?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                *ratio = atol(tmpMsg2);

            }
           
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSRefSrcMode                                    
 * Purpose:  This function returns the stimulus reference source setting.
 *           The possible return values are:
 *
 *           NA65CS4_INT = 0   (INTERNAL Reference Source)
 *           NA65CS4_EXT = 1   (EXTERNAL Reference Source)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSRefSrcMode (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *refSourceMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u REF_SOURCE?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                if (stricmp(tmpMsg2, "INT") == 0)
                    *refSourceMode = NA65CS4_INT;
                else
                    *refSourceMode = NA65CS4_EXT;
            }
         
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSRelayIOState                                    
 * Purpose:  This function returns the stimulus channel relay I/O state.
 *           The possible return values are:
 *
 *           NA65CS4_OPEN  = 0   (OPENED relay status)
 *           NA65CS4_CLOSE = 1   (CLOSED relay status)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSRelayIOState (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViInt16 *relayIOState)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u STATE?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                if (stricmp(tmpMsg2, "OPEN") == 0)
                    *relayIOState = NA65CS4_OPEN;
                else
                    *relayIOState = NA65CS4_CLOSE;
            }
          
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSLineToLineVoltage                                    
 * Purpose:  This function returns the channel line-to-line voltage in Volts.
 *           The range returned for the line-to-line voltage is 1 to 90
 *           volts.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSLineToLineVoltage (ViSession vi,
                                                     ViInt16 channel,
                                                     ViInt16 channelGrade,
                                                     ViReal64 *LinetoLineVoltage)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u VLL_VOLT?\r\n",ChanPrefix,channel);   
     
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                *LinetoLineVoltage = atof(tmpMsg2);
            }
          
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSInputRefVolt                                    
 * Purpose:  This function returns the channel input reference voltage in
 *           Volts. This does not apply when the channel reference source is
 *           internal. The  range returned for the input reference voltage is
 *           2 to 115 volts if the channel reference source is external,
 *           otherwise 0 volts is returned.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSInputRefVolt (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViReal64 *inputRefVoltage_V)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u REF_VOLT_IN?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                *inputRefVoltage_V = atof(tmpMsg2);
            }
           
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSRotationRate                                    
 * Purpose:  This function returns the stimulus channel rotation rate in
 *           revolutions per second (RPS). The range returned for the
 *           rotation rate is 0.15 to 13.60 or 0.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSRotationRate (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViReal64 *rotationRate)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u ROT_RATE?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                *rotationRate = atof(tmpMsg2);
            }
          
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSRotateStopAng                                    
 * Purpose:  This function returns the channel rotation stop angle in
 *           degrees. The range returned for the rotation stop angle is 0 to
 *           359.9999 degrees.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSRotateStopAng (ViSession vi, ViInt16 channel,
                                                 ViInt16 channelGrade,
                                                 ViReal64 *rotationStopAngle)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u ROT_STOP_ANGLE?\r\n",ChanPrefix,channel);   
                
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                *rotationStopAngle = atof(tmpMsg2);
            }
          
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSRotationMode                                    
 * Purpose:  This function returns the channel rotation mode. The possible
 *           return values are:
 *
 *           NA65CS4_CONT = 0   (CONTINUOUS Rotation Mode)
 *           NA65CS4_STEP = 1   (STEP Rotation Mode)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSRotationMode (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViInt16 *rotationMode)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
               if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
               if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u ROT_MODE?\r\n",ChanPrefix,channel);    
             
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                if (stricmp(tmpMsg2, "CONTINUOUS") == 0)
                    *rotationMode = NA65CS4_CONT;
                else
                    *rotationMode = NA65CS4_STEP;
            }
           
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSTriggerSrc                                    
 * Purpose:  This function returns the channel trigger source. Possible
 *           return values are:
 *
 *           NA65CS4_INT = 0   (INTERNAL Trigger Source)
 *           NA65CS4_EXT = 1   (EXTERNAL Trigger Source)
 *           NA65CS4_BUS = 2   (BUS Trigger Source)
 *           NA65CS4_TTL = 3   (TTL Level Trigger Source)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSTriggerSrc (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViInt16 *triggerSource)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u TRIG_SOURCE?\r\n",ChanPrefix,channel);   
             
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                if (stricmp(tmpMsg2, "INT") == 0)
                    *triggerSource = NA65CS4_INT;
                else if (stricmp(tmpMsg2, "BUS") == 0)
                    *triggerSource = NA65CS4_BUS;
                else if (stricmp(tmpMsg2, "EXT") == 0)
                    *triggerSource = NA65CS4_EXT;
                else if (stricmp(tmpMsg2, "TTL0") == 0)
                    *triggerSource = NA65CS4_TTL_00;
                else if (stricmp(tmpMsg2, "TTL1") == 0)
                    *triggerSource = NA65CS4_TTL_01;
                else if (stricmp(tmpMsg2, "TTL2") == 0)
                    *triggerSource = NA65CS4_TTL_02;
                else if (stricmp(tmpMsg2, "TTL3") == 0)
                    *triggerSource = NA65CS4_TTL_03;
                else if (stricmp(tmpMsg2, "TTL4") == 0)
                    *triggerSource = NA65CS4_TTL_04;    
                else if (stricmp(tmpMsg2, "TTL5") == 0)
                    *triggerSource = NA65CS4_TTL_05;    
                else if (stricmp(tmpMsg2, "TTL6") == 0)
                    *triggerSource = NA65CS4_TTL_06;    
                else if (stricmp(tmpMsg2, "TTL7") == 0)
                    *triggerSource = NA65CS4_TTL_07;    
                else
                    *triggerSource = NA65CS4_INT;
            }
          
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSTriggerSlope                                    
 * Purpose:  This function returns the channel trigger slope. The possible
 *           return values are:
 *
 *           NA65CS4_NEG = 0   (NEGATIVE going level)
 *           NA65CS4_POS = 1   (POSITIVE going level)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSTriggerSlope (ViSession vi, ViInt16 channel,
                                                ViInt16 channelGrade,
                                                ViInt16 *triggerSlope)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
                if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
                if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u TRIG_SLOPE?\r\n",ChanPrefix,channel);   
            
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                if (stricmp(tmpMsg2, "NEG") == 0)
                    *triggerSlope = NA65CS4_NEG;
                else
                    *triggerSlope = NA65CS4_POS;
            }
          
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_DSPhaseShift                                    
 * Purpose:  This function returns the channel phase shift in degrees. The
 *           range returned for the phase shift is -179.9 to 179.9 degrees.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_DSPhaseShift (ViSession vi, ViInt16 channel,
                                              ViInt16 channelGrade,
                                              ViReal64 *phaseShift)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            
               if (channelGrade == NA65CS4_INSTRUMENT_GRADE) sprintf(ChanPrefix,"DSH");    
               if (channelGrade == NA65CS4_OPERATIONAL_GRADE) sprintf(ChanPrefix,"DSL");   
                                                                                        
                sprintf(DutCmd, "%s%u PHASE?\r\n",ChanPrefix,channel);    
            
                viCheckErr( viPrintf(io,DutCmd) ); 
            
                /* Get the result */
                tmpMsg[0] = '\0';
                viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
                /* Strip out the "\r\n" if there are any in the testMessage */
                na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
                
                *phaseShift = atof(tmpMsg2);
            }
           
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_RefFreq                                    
 * Purpose:  This function returns the reference generator frequency in
 *           hertz. The range  returned for the frequency is based on the
 *           Part Number Designation.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_RefFreq (ViSession vi, ViInt16 channel,
                                         ViReal64 *frequency)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViChar      ChanPrefix[25] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            sprintf(DutCmd, "REF_GEN%u FREQ?\r\n",channel);
            viCheckErr( viPrintf(io,DutCmd) );
                
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            *frequency = atof(tmpMsg2);
        
        }
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_RefVolt                                    
 * Purpose:  This function returns the reference generator voltage in volts.
 *           The range returned for the reference generator voltage is 2 to
 *           115 volts.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_RefVolt (ViSession vi, ViInt16 channel,
                                         ViReal64 *voltage)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            sprintf(DutCmd, "REF_GEN%u VOLT?\r\n",channel);
            viCheckErr( viPrintf(io,DutCmd) );
                
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            *voltage = atof(tmpMsg2);
        
        }
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_query_RefRelayIOState                                    
 * Purpose:  This function returns the reference generator channel relay I/O
 *           state.  The possible return values are:
 *
 *           NA65CS4_OPEN  = 0   (OPENED relay status)
 *           NA65CS4_CLOSE = 1   (CLOSED relay status)
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_query_RefRelayIOState (ViSession vi, ViInt16 channel,
                                                 ViInt16 *relayIOState)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      DutCmd[255] = ""; 
    ViUInt32    retCnt = 0;
    ViChar      tmpMsg[BUFFER_SIZE];
    ViChar      tmpMsg2[BUFFER_SIZE];
    
    checkErr( Ivi_LockSession (vi, VI_NULL));

    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */
        
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        if (LANG == NA65CS4_LANG_NATIVE) 
        {
            sprintf(DutCmd, "REF_GEN%u STATE?\r\n",channel);
            viCheckErr( viPrintf(io,DutCmd) ); 
            
            /* Get the result */
            tmpMsg[0] = '\0';
            viCheckErr( viRead(io, tmpMsg, BUFFER_SIZE-1, &retCnt));
            
            /* Strip out the "\r\n" if there are any in the testMessage */
            na65cs4_TrimAndRemoveCRLF(tmpMsg, retCnt, tmpMsg2);
            
            if (stricmp(tmpMsg2, "OPEN") == 0)
                *relayIOState = NA65CS4_OPEN;
            else
                *relayIOState = NA65CS4_CLOSE;
        
        }
  
    }
    
    checkErr( na65cs4_CheckStatus (vi)); 
    
Error:
    Ivi_UnlockSession (vi, VI_NULL);
    return error;
}

/*****************************************************************************
 * Function: na65cs4_SetAttribute<type> Functions                                    
 * Purpose:  These functions enable the instrument driver user to set 
 *           attribute values directly.  There are typesafe versions for 
 *           ViInt32, ViReal64, ViString, ViBoolean, and ViSession datatypes.                                         
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_SetAttributeViInt32 (ViSession vi, ViConstString channelName, 
                                                ViAttr attributeId, ViInt32 value)
{                                                                                                           
    return Ivi_SetAttributeViInt32 (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                    value);
}                                                                                                           
ViStatus _VI_FUNC na65cs4_SetAttributeViReal64 (ViSession vi, ViConstString channelName, 
                                                 ViAttr attributeId, ViReal64 value)
{                                                                                                           
    return Ivi_SetAttributeViReal64 (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                     value);
}                                                                                                           
ViStatus _VI_FUNC na65cs4_SetAttributeViString (ViSession vi, ViConstString channelName, 
                                                 ViAttr attributeId, ViConstString value) 
{   
    return Ivi_SetAttributeViString (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                     value);
}   
ViStatus _VI_FUNC na65cs4_SetAttributeViBoolean (ViSession vi, ViConstString channelName, 
                                                  ViAttr attributeId, ViBoolean value)
{                                                                                                           
    return Ivi_SetAttributeViBoolean (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                      value);
}                                                                                                           
ViStatus _VI_FUNC na65cs4_SetAttributeViSession (ViSession vi, ViConstString channelName, 
                                                  ViAttr attributeId, ViSession value)
{                                                                                                           
    return Ivi_SetAttributeViSession (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                      value);
}                                                                                                           

/*****************************************************************************
 * Function: na65cs4_CheckAttribute<type> Functions                                  
 * Purpose:  These functions enable the instrument driver user to check  
 *           attribute values directly.  These functions check the value you
 *           specify even if you set the NA65CS4_ATTR_RANGE_CHECK 
 *           attribute to VI_FALSE.  There are typesafe versions for ViInt32, 
 *           ViReal64, ViString, ViBoolean, and ViSession datatypes.                         
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_CheckAttributeViInt32 (ViSession vi, ViConstString channelName, 
                                                  ViAttr attributeId, ViInt32 value)
{                                                                                                           
    return Ivi_CheckAttributeViInt32 (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                      value);
}
ViStatus _VI_FUNC na65cs4_CheckAttributeViReal64 (ViSession vi, ViConstString channelName, 
                                                   ViAttr attributeId, ViReal64 value)
{                                                                                                           
    return Ivi_CheckAttributeViReal64 (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                       value);
}                                                                                                           
ViStatus _VI_FUNC na65cs4_CheckAttributeViString (ViSession vi, ViConstString channelName, 
                                                   ViAttr attributeId, ViConstString value)  
{   
    return Ivi_CheckAttributeViString (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                       value);
}   
ViStatus _VI_FUNC na65cs4_CheckAttributeViBoolean (ViSession vi, ViConstString channelName, 
                                                    ViAttr attributeId, ViBoolean value)
{                                                                                                           
    return Ivi_CheckAttributeViBoolean (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                        value);
}                                                                                                           
ViStatus _VI_FUNC na65cs4_CheckAttributeViSession (ViSession vi, ViConstString channelName, 
                                                    ViAttr attributeId, ViSession value)
{                                                                                                           
    return Ivi_CheckAttributeViSession (vi, channelName, attributeId, IVI_VAL_DIRECT_USER_CALL, 
                                        value);
}                                                                                                           


/*****************************************************************************
 * Function: WriteInstrData and ReadInstrData Functions                      
 * Purpose:  These functions enable the instrument driver user to  
 *           write and read commands directly to and from the instrument.            
 *                                                                           
 *           Note:  These functions bypass the IVI attribute state caching.  
 *                  WriteInstrData invalidates the cached values for all 
 *                  attributes.
 *****************************************************************************/
ViStatus _VI_FUNC na65cs4_WriteInstrData (ViSession vi, ViConstString writeBuffer)   
{   
    return Ivi_WriteInstrData (vi, writeBuffer);    
}   
ViStatus _VI_FUNC na65cs4_ReadInstrData (ViSession vi, ViInt32 numBytes, 
                                          ViChar rdBuf[], ViInt32 *bytesRead)  
{   
    return Ivi_ReadInstrData (vi, numBytes, rdBuf, bytesRead);   
} 

/*****************************************************************************
 *-------------------- Utility Functions (Not Exported) ---------------------*
 *****************************************************************************/


/*****************************************************************************
 * Function: na65cs4_CheckStatus                                                 
 * Purpose:  This function checks the status of the instrument to detect 
 *           whether the instrument has encountered an error.  This function  
 *           is called at the end of most exported driver functions.  If the    
 *           instrument reports an error, this function returns      
 *           IVI_ERROR_INSTRUMENT_SPECIFIC.  The user can set the 
 *           NA65CS4_ATTR_QUERY_INSTRUMENT_STATUS attribute to VI_FALSE to disable this 
 *           check and increase execution speed.                                   
 *
 *           Note:  Call this function only when the session is locked.
 *****************************************************************************/
static ViStatus na65cs4_CheckStatus (ViSession vi)
{
    ViStatus    error = VI_SUCCESS;

    if (Ivi_QueryInstrStatus (vi) && Ivi_NeedToCheckStatus (vi) && !Ivi_Simulating (vi))
    {
        checkErr( na65cs4_CheckStatusCallback (vi, Ivi_IOSession(vi)));
        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_FALSE));
    }
        
Error:
    return error;
}

/*****************************************************************************
 * Function: na65cs4_WaitForOPC                                                  
 * Purpose:  This function waits for the instrument to complete the      
 *           execution of all pending operations.  This function is a        
 *           wrapper for the WaitForOPCCallback.  It can be called from 
 *           other instrument driver functions. 
 *
 *           The maxTime parameter specifies the maximum time to wait for
 *           operation complete in milliseconds.
 *
 *           Note:  Call this function only when the session is locked.
 *****************************************************************************/
static ViStatus na65cs4_WaitForOPC (ViSession vi, ViInt32 maxTime) 
{
    ViStatus    error = VI_SUCCESS;

    if (!Ivi_Simulating(vi))
        {
        ViInt32  oldOPCTimeout; 
        
        checkErr( Ivi_GetAttributeViInt32 (vi, VI_NULL, NA65CS4_ATTR_OPC_TIMEOUT, 
                                           0, &oldOPCTimeout));
        Ivi_SetAttributeViInt32 (vi, VI_NULL, NA65CS4_ATTR_OPC_TIMEOUT,        
                                 0, maxTime);

        error = na65cs4_WaitForOPCCallback (vi, Ivi_IOSession(vi));

        Ivi_SetAttributeViInt32 (vi, VI_NULL, NA65CS4_ATTR_OPC_TIMEOUT, 
                                 0, oldOPCTimeout);
        viCheckErr( error);
        }
Error:
    return error;
}

/*****************************************************************************
 * Function: na65cs4_DefaultInstrSetup                                               
 * Purpose:  This function sends a default setup to the instrument.  The    
 *           na65cs4_reset function calls this function.  The 
 *           na65cs4_IviInit function calls this function when the
 *           user passes VI_FALSE for the reset parameter.  This function is 
 *           useful for configuring settings that other instrument driver 
 *           functions require.    
 *
 *           Note:  Call this function only when the session is locked.
 *****************************************************************************/
static ViStatus na65cs4_DefaultInstrSetup (ViSession vi)
{
    ViStatus    error = VI_SUCCESS;
        
    /* Invalidate all attributes */
    checkErr( Ivi_InvalidateAllAttributes (vi));
    
    if (!Ivi_Simulating(vi))
    {
        ViSession   io = Ivi_IOSession(vi); /* call only when locked */

        checkErr( Ivi_SetNeedToCheckStatus (vi, VI_TRUE));
        
        /* NA65CS4 does not have another command other than the reset command
           to execute the default setup for the instrument. */
    }
Error:
    return error;
}


/*****************************************************************************
 * Function: ReadToFile and WriteFromFile Functions                          
 * Purpose:  Functions for instrument driver developers to read/write        
 *           instrument data to/from a file.                                 
 *****************************************************************************/
static ViStatus na65cs4_ReadToFile (ViSession vi, ViConstString filename, 
                                     ViInt32 maxBytesToRead, ViInt32 fileAction, 
                                     ViInt32 *totalBytesWritten)  
{   
    return Ivi_ReadToFile (vi, filename, maxBytesToRead, fileAction, totalBytesWritten);  
}   
static ViStatus na65cs4_WriteFromFile (ViSession vi, ViConstString filename, 
                                        ViInt32 maxBytesToWrite, ViInt32 byteOffset, 
                                        ViInt32 *totalBytesWritten) 
{   
    return Ivi_WriteFromFile (vi, filename, maxBytesToWrite, byteOffset, totalBytesWritten); 
}

/*****************************************************************************
 *------------------------ Global Session Callbacks -------------------------*
 *****************************************************************************/

/*****************************************************************************
 * Function: na65cs4_CheckStatusCallback                                               
 * Purpose:  This function queries the instrument to determine if it has 
 *           encountered an error.  If the instrument has encountered an 
 *           error, this function returns the IVI_ERROR_INSTRUMENT_SPECIFIC 
 *           error code.  This function is called by the 
 *           na65cs4_CheckStatus utility function.  The 
 *           Ivi_SetAttribute and Ivi_GetAttribute functions invoke this 
 *           function when the optionFlags parameter includes the
 *           IVI_VAL_DIRECT_USER_CALL flag.
 *           
 *           The user can disable calls to this function by setting the 
 *           NA65CS4_QUERY_INSTRUMENT_STATUS attribute to VI_FALSE.  The driver can 
 *           disable the check status callback for a particular attribute by 
 *           setting the IVI_VAL_DONT_CHECK_STATUS flag.
 *****************************************************************************/
static ViStatus _VI_FUNC na65cs4_CheckStatusCallback (ViSession vi, ViSession io)
{
    ViStatus    error = VI_SUCCESS;
    
    ViInt32     errCode;
    ViChar      errMsgs[256];
    
    /* The NA65CS4 device does not have a query that lets us request whether
       it has encountered an error.  As a workaround, we will call the
       na65cs4_error_query and see if there is an error.  If the errCode is 0,
       then there is no error.
    */
    checkErr ( na65cs4_error_query(vi, &errCode, &errMsgs[0])) ;

    /* If the errCode is not 0, the instrument encountered an error */
    if (errCode != 0)
        viCheckErr( IVI_ERROR_INSTR_SPECIFIC);

Error:
    return error;
}

/*****************************************************************************
 * Function: na65cs4_WaitForOPCCallback                                               
 * Purpose:  This function waits until the instrument has finished processing 
 *           all pending operations.  This function is called by the 
 *           na65cs4_WaitForOPC utility function.  The IVI engine invokes
 *           this function in the following two cases:
 *           - Before invoking the read callback for attributes for which the 
 *             IVI_VAL_WAIT_FOR_OPC_BEFORE_READS flag is set.
 *           - After invoking the write callback for attributes for which the 
 *             IVI_VAL_WAIT_FOR_OPC_AFTER_WRITES flag is set.
 *****************************************************************************/
static ViStatus _VI_FUNC na65cs4_WaitForOPCCallback (ViSession vi, ViSession io)
{
    ViStatus    error = VI_SUCCESS;
    /*=CHANGE:===============================================================*
       Change this function to wait for operation complete for your specific 
       instrument.  This example function is based on the IEEE 488.2 Common  
       System Commands.  This example shows how to use the 
       NA65CS4_ATTR_OPC_TIMEOUT hidden attribute to control the
       length of time this function waits.  This function assumes that the 
       instrument does not generate service requests for other reasons.  This 
       example performs the following operations:
       
       - Enables the service request event.
       - Sends a command that forces the instrument to generate a service 
         request.
       - Waits for the service request.
       - Cleans up after the service request.

    ViInt32     opcTimeout;
    ViUInt16    statusByte;

    checkErr( Ivi_GetAttributeViInt32 (vi, VI_NULL, NA65CS4_ATTR_OPC_TIMEOUT, 
                                       0, &opcTimeout));

    viCheckErr( viEnableEvent (io, VI_EVENT_SERVICE_REQ, VI_QUEUE, VI_NULL));

    viCheckErr( viPrintf (io, "*OPC"));

        // wait for SRQ 
    viCheckErr( viWaitOnEvent (io, VI_EVENT_SERVICE_REQ, opcTimeout, VI_NULL, VI_NULL));
    viCheckErr( viDisableEvent (io, VI_EVENT_SERVICE_REQ, VI_QUEUE));

        // clean up after SRQ 
    viCheckErr( viBufWrite (io, "*CLS", 4, VI_NULL));
    viCheckErr( viReadSTB (io, &statusByte));
    
Error:
    viDiscardEvents (io, VI_EVENT_SERVICE_REQ, VI_QUEUE);
     *============================================================END=CHANGE=*/

    return error;
}

/*****************************************************************************
 *----------------- Attribute Range Tables and Callbacks --------------------*
 *****************************************************************************/

/*- NA65CS4_ATTR_VXI_MANF_ID -*/

static ViStatus _VI_FUNC na65cs4AttrVXIManfID_ReadCallback (ViSession vi, 
                                                             ViSession io,
                                                             ViConstString channelName, 
                                                             ViAttr attributeId,
                                                             ViInt32* value)
{
    ViStatus error = VI_SUCCESS;
    ViUInt16 manfID = 0;

    viCheckErr( viGetAttribute (io, VI_ATTR_MANF_ID, &manfID));
    *value = (ViInt32)manfID;

Error:
    return error;
}

/*- NA65CS4_ATTR_VXI_MODEL_CODE -*/

static ViStatus _VI_FUNC na65cs4AttrVXIModelCode_ReadCallback (ViSession vi,
                                                                ViSession io, 
                                                                ViConstString channelName, 
                                                                ViAttr attributeId, 
                                                                ViInt32* value)
{
    ViStatus error = VI_SUCCESS;
    ViUInt16 modelCode = 0;

    viCheckErr( viGetAttribute (io, VI_ATTR_MODEL_CODE, &modelCode));
    *value = (ViInt32)modelCode;

Error:
    return error;
}
    
/*- NA65CS4_ATTR_SPECIFIC_DRIVER_REVISION -*/

static ViStatus _VI_FUNC na65cs4AttrDriverRevision_ReadCallback (ViSession vi, 
                                                                  ViSession io,
                                                                  ViConstString channelName, 
                                                                  ViAttr attributeId,
                                                                  const ViConstString cacheValue)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      driverRevision[256];
    
    
    sprintf (driverRevision, 
             "Driver: na65cs4 %.2f, Compiler: %s %3.2f, "
             "Components: IVIEngine %.2f, VISA-Spec %.2f",
             NA65CS4_MAJOR_VERSION + NA65CS4_MINOR_VERSION/1000.0, 
             IVI_COMPILER_NAME, IVI_COMPILER_VER_NUM, 
             IVI_ENGINE_MAJOR_VERSION + IVI_ENGINE_MINOR_VERSION/1000.0, 
             Ivi_ConvertVISAVer(VI_SPEC_VERSION));

    checkErr( Ivi_SetValInStringCallback (vi, attributeId, driverRevision));    
Error:
    return error;
}


/*- NA65CS4_ATTR_INSTRUMENT_FIRMWARE_REVISION -*/

static ViStatus _VI_FUNC na65cs4AttrFirmwareRevision_ReadCallback (ViSession vi, 
                                                                    ViSession io,
                                                                    ViConstString channelName, 
                                                                    ViAttr attributeId,
                                                                    const ViConstString cacheValue)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      instrRev[BUFFER_SIZE]="";
    
    if (!Ivi_Simulating(vi))                /* call only when locked */
    {
        ViSession   io = Ivi_IOSession(vi); 
   
        /*=CHANGE:=============================================================*
            Modify this code to query the firmware revision of the instrument.
        
        viCheckErr( viPrintf (io, "*IDN?"));

    viCheckErr( viScanf (io, "%*[^,],%*[^,],%*[^,],%256[^\n]", instrRev));
                              
         *==========================================================END=CHANGE=*/
    }
    checkErr( Ivi_SetValInStringCallback (vi, attributeId, instrRev));
    
Error:
    return error;
}

/*- NA65CS4_ATTR_INSTRUMENT_MANUFACTURER -*/

static ViStatus _VI_FUNC na65cs4AttrInstrumentManufacturer_ReadCallback (ViSession vi, 
                                                                          ViSession io,
                                                                          ViConstString channelName, 
                                                                          ViAttr attributeId,
                                                                          const ViConstString cacheValue)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      rdBuffer[BUFFER_SIZE];
    ViInt32      manfId;
    
    checkErr( Ivi_GetAttributeViInt32 (vi, "", NA65CS4_ATTR_VXI_MANF_ID,
                                       0, &manfId));
    sprintf (rdBuffer, "Manufacturer Id: 0x%x", manfId);

    checkErr( Ivi_SetValInStringCallback (vi, attributeId, rdBuffer));
    
Error:
    return error;
}

/*- NA65CS4_ATTR_INSTRUMENT_MODEL -*/

static ViStatus _VI_FUNC na65cs4AttrInstrumentModel_ReadCallback (ViSession vi, 
                                                                   ViSession io,
                                                                   ViConstString channelName, 
                                                                   ViAttr attributeId,
                                                                   const ViConstString cacheValue)
{
    ViStatus    error = VI_SUCCESS;
    ViChar      rdBuffer[BUFFER_SIZE];
    ViInt32      model;
    
    checkErr( Ivi_GetAttributeViInt32 (vi, "", NA65CS4_ATTR_VXI_MANF_ID,
                                       0, &model));

    /*=CHANGE:===============================================================*
        Please make sure you apply correct formatting for the model code.
     *=======================================================================*/

    sprintf (rdBuffer, "Model Code: 0x%x", model);

    checkErr( Ivi_SetValInStringCallback (vi, attributeId, rdBuffer));
    
Error:
    return error;
}
    


/*****************************************************************************
 * Function: na65cs4_InitAttributes                                                  
 * Purpose:  This function adds attributes to the IVI session, initializes   
 *           instrument attributes, and sets attribute invalidation          
 *           dependencies.                                                   
 *****************************************************************************/
static ViStatus na65cs4_InitAttributes (ViSession vi)
{
    ViStatus    error = VI_SUCCESS;
    ViInt32     flags = 0;
        /*=CHANGE:=============================================================*

            NOTE TO THE DEVELOPER:  You can add additional parameters to the
            prototype of this function.  This is useful when you want to pass
            information from the initialization functions.  The Attribute Editor 
            in LabWindows/CVI requires that the name of this function be 
            na65cs4_InitAttributes.

         *==========================================================END=CHANGE=*/
    
        /*- Initialize instrument attributes --------------------------------*/            
        /*=CHANGE:=============================================================*
            NOTE TO THE DEVELOPER:  These attributes are obsolete and may not be
            supported in future versions of IVI drivers.  
            
            If you DO NOT want this driver to be compliant with NI's Ivi Driver 
            Toolset 1.1, remove these attribute initalizations.

            If you DO want this driver to be compliant with NI's IVI Driver Toolset
            1.1, remove the comments around these attribute initalizations.

    checkErr( Ivi_SetAttributeViInt32 (vi, "", NA65CS4_ATTR_SPECIFIC_DRIVER_MAJOR_VERSION,
                                       0, NA65CS4_MAJOR_VERSION));
    checkErr( Ivi_SetAttributeViInt32 (vi, "", NA65CS4_ATTR_SPECIFIC_DRIVER_MINOR_VERSION,
                                       0, NA65CS4_MINOR_VERSION));
    checkErr( Ivi_SetAttributeViString (vi, "", NA65CS4_ATTR_IO_SESSION_TYPE,
                                        0, NA65CS4_IO_SESSION_TYPE));
         *==========================================================END=CHANGE=*/

    checkErr( Ivi_SetAttrReadCallbackViString (vi, NA65CS4_ATTR_SPECIFIC_DRIVER_REVISION,
                                               na65cs4AttrDriverRevision_ReadCallback));
    checkErr( Ivi_SetAttributeViInt32 (vi, "", NA65CS4_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MAJOR_VERSION,
                                       0, NA65CS4_CLASS_SPEC_MAJOR_VERSION));
    checkErr( Ivi_SetAttributeViInt32 (vi, "", NA65CS4_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MINOR_VERSION,
                                       0, NA65CS4_CLASS_SPEC_MINOR_VERSION));
    checkErr( Ivi_SetAttributeViString (vi, "", NA65CS4_ATTR_SUPPORTED_INSTRUMENT_MODELS,
                                        0, NA65CS4_SUPPORTED_INSTRUMENT_MODELS));


    checkErr( Ivi_GetAttributeFlags (vi, NA65CS4_ATTR_INSTRUMENT_FIRMWARE_REVISION, &flags));
    checkErr( Ivi_SetAttributeFlags (vi, NA65CS4_ATTR_INSTRUMENT_FIRMWARE_REVISION, 
                                     flags | IVI_VAL_USE_CALLBACKS_FOR_SIMULATION));
    checkErr( Ivi_SetAttrReadCallbackViString (vi, NA65CS4_ATTR_INSTRUMENT_FIRMWARE_REVISION,
                                               na65cs4AttrFirmwareRevision_ReadCallback));

    /*=CHANGE:===============================================================*
        This driver uses the id query attribute (or vxi manufacturer id
        attribute in register-based instrument case) to get the information
        about the instrument manufacturer. Therefore, callbacks do not perform
        any I/O and in order to simulate correctly, you must flag this
        attribute as "use callbacks for simulation".  If you implement this 
        attribute so that it uses instrument I/O in the callback, then you 
        must remove this call from the driver.
     *============================================================END=CHANGE=*/
    checkErr( Ivi_GetAttributeFlags (vi, NA65CS4_ATTR_INSTRUMENT_MANUFACTURER, &flags));
    checkErr( Ivi_SetAttributeFlags (vi, NA65CS4_ATTR_INSTRUMENT_MANUFACTURER, 
                                     flags | IVI_VAL_USE_CALLBACKS_FOR_SIMULATION));
    checkErr( Ivi_SetAttrReadCallbackViString (vi, NA65CS4_ATTR_INSTRUMENT_MANUFACTURER,
                                               na65cs4AttrInstrumentManufacturer_ReadCallback));

    /*=CHANGE:===============================================================*
        This driver uses the id query attribute (or vxi model code
        attribute in register-based instrument case) to get the information
        about the instrument model code. Therefore, callbacks do not perform
        any I/O and in order to simulate correctly, you must flag this
        attribute as "use callbacks for simulation".  If you implement this 
        attribute so that it uses instrument I/O in the callback, then you 
        must remove this call from the driver.
     *============================================================END=CHANGE=*/
    checkErr( Ivi_GetAttributeFlags (vi, NA65CS4_ATTR_INSTRUMENT_MODEL, &flags));
    checkErr( Ivi_SetAttributeFlags (vi, NA65CS4_ATTR_INSTRUMENT_MODEL, 
                                     flags | IVI_VAL_USE_CALLBACKS_FOR_SIMULATION));
    checkErr( Ivi_SetAttrReadCallbackViString (vi, NA65CS4_ATTR_INSTRUMENT_MODEL,
                                               na65cs4AttrInstrumentModel_ReadCallback));

    checkErr( Ivi_SetAttributeViString (vi, "", NA65CS4_ATTR_SPECIFIC_DRIVER_VENDOR,
                                        0, NA65CS4_DRIVER_VENDOR));
    checkErr( Ivi_SetAttributeViString (vi, "", NA65CS4_ATTR_SPECIFIC_DRIVER_DESCRIPTION,
                                        0, NA65CS4_DRIVER_DESCRIPTION));
    checkErr( Ivi_SetAttributeViAddr (vi, VI_NULL, IVI_ATTR_OPC_CALLBACK, 0,
                                      na65cs4_WaitForOPCCallback));
    checkErr( Ivi_SetAttributeViAddr (vi, VI_NULL, IVI_ATTR_CHECK_STATUS_CALLBACK, 0,
                                      na65cs4_CheckStatusCallback));


    /*=CHANGE:===============================================================*
        Set the group capabilities attribute, Remove the extension groups
        this driver does not support, and add additional code to dynamically
        determine how the presence of specific instrument options may affect
        this attribute.
     *============================================================END=CHANGE=*/
        
    checkErr( Ivi_SetAttributeViString (vi, "", NA65CS4_ATTR_GROUP_CAPABILITIES, 0,
                                        "None"));
    
        /*- Add instrument-specific attributes ------------------------------*/            
    
    checkErr( Ivi_AddAttributeViInt32 (vi, NA65CS4_ATTR_VXI_MANF_ID,
                                       "NA65CS4_ATTR_VXI_MANF_ID", 
                                       0xBE85,
                                       IVI_VAL_NOT_USER_WRITABLE,
                                       na65cs4AttrVXIManfID_ReadCallback,
                                       VI_NULL, VI_NULL));
                                               
    checkErr( Ivi_AddAttributeViInt32 (vi, NA65CS4_ATTR_VXI_MODEL_CODE,
                                       "NA65CS4_ATTR_VXI_MODEL_CODE", 
                                       0x105,
                                       IVI_VAL_NOT_USER_WRITABLE,
                                       na65cs4AttrVXIModelCode_ReadCallback,
                                       VI_NULL, VI_NULL));
    
    checkErr( Ivi_AddAttributeViInt32 (vi, NA65CS4_ATTR_OPC_TIMEOUT,
                                       "NA65CS4_ATTR_OPC_TIMEOUT",
                                       5000, IVI_VAL_HIDDEN | IVI_VAL_DONT_CHECK_STATUS,
                                       VI_NULL, VI_NULL, VI_NULL)); 
    checkErr( Ivi_AddAttributeViInt32 (vi,
                                       NA65CS4_ATTR_VISA_RM_SESSION,
                                       "NA65CS4_ATTR_VISA_RM_SESSION",
                                       VI_NULL,
                                       IVI_VAL_HIDDEN | IVI_VAL_DONT_CHECK_STATUS,
                                       VI_NULL, VI_NULL, VI_NULL));
    checkErr( Ivi_AddAttributeViInt32 (vi, NA65CS4_ATTR_OPC_CALLBACK,
                                       "NA65CS4_ATTR_OPC_CALLBACK",
                                       VI_NULL,
                                       IVI_VAL_HIDDEN | IVI_VAL_DONT_CHECK_STATUS,
                                       VI_NULL, VI_NULL, VI_NULL));
    checkErr( Ivi_AddAttributeViInt32 (vi,
                                       NA65CS4_ATTR_CHECK_STATUS_CALLBACK,
                                       "NA65CS4_ATTR_CHECK_STATUS_CALLBACK",
                                       VI_NULL,
                                       IVI_VAL_HIDDEN | IVI_VAL_DONT_CHECK_STATUS,
                                       VI_NULL, VI_NULL, VI_NULL));
    checkErr( Ivi_AddAttributeViInt32 (vi,
                                       NA65CS4_ATTR_USER_INTERCHANGE_CHECK_CALLBACK,
                                       "NA65CS4_ATTR_USER_INTERCHANGE_CHECK_CALLBACK",
                                       VI_NULL,
                                       IVI_VAL_HIDDEN | IVI_VAL_DONT_CHECK_STATUS,
                                       VI_NULL, VI_NULL, VI_NULL));
                                       
        /*- Setup attribute invalidations -----------------------------------*/            

    /*=CHANGE:===============================================================*
       
       Set attribute dependencies by calling the additional 
       Ivi_AddAttributeInvalidation functions here.  Remove the dependencies
       that do not apply to your instrument by deleting the calls to
       Ivi_AddAttributeInvalidation.
       When you initially add an attribute, it applies to all channels / repeated
       capability instances.  If you want it to apply to only a subset, call the 
       Ivi_RestrictAttrToChannels or Ivi_RestrictAttrToInstances function.

     *============================================================END=CHANGE=*/

Error:
    return error;
}

/*****************************************************************************
 * Function: na65cs4_TrimAndRemoveCRLF                                                  
 * Purpose:  This function removes "\r\n" and leading and trailing blanks
 *           from the msg[] argument and writes the updated string to
 *           msgNew[].
 *****************************************************************************/
ViStatus na65cs4_TrimAndRemoveCRLF(ViChar msg[], ViInt16 nMsgSize, ViChar msgNew[])
{
    ViStatus    error = VI_SUCCESS;
    int         i;
    ViBoolean   bBeginCopy = VI_FALSE;

    int nNewMsgSize = 0;
    
    /* Remove leading blanks and "\r\n" */
    for (i = 0; i < (int)nMsgSize; i++)
    {
        if (!bBeginCopy && (msg[i] != ' '))
            bBeginCopy = VI_TRUE;
            
        if (bBeginCopy == VI_TRUE)
        {
            if ((msg[i] != '\r') && (msg[i] != '\n'))
            {
                msgNew[nNewMsgSize] = msg[i];
                nNewMsgSize++;
            }
        }
    }
    
    /* Remove trailing blank characters */
    i = nNewMsgSize;
    while (msgNew[i] == ' ')
    {
        i--;
    }
    msgNew[i] = '\0';
    
Error:
    return error;

}

/*****************************************************************************
 *------------------- End Instrument Driver Source Code ---------------------*
 *****************************************************************************/
