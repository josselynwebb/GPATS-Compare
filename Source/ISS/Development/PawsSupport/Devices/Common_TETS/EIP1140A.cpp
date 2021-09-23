// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2020-07-06 16:01:5#$: Date of last commit
//    $Rev:: 27851              $: Revision of last commit

// $Header: svn://ratbert/SST/ATE/USMC/CIC/ATEP%20GPATS/branches/3.0.0.0016/Source/ISS/Development/PawsSupport/Devices/Common_TETS/EIP1140A.cpp 27851 2020-07-06 20:01:53Z wileyj $

/***************************************************************************/
/*  Copyright 1996 National Instruments Corporation.  All Rights Reserved. */
/***************************************************************************/

#include <ansi_c.h>
#include <formatio.h>
#include <utility.h>
#include <visa.h>
#include "eip1140a.h"

/*= DEFINES =================================================================*/
#define eip1140a_REVISION     "Rev 1.0, 2/96, CVI 3.1" /*  Instrument driver revision */
#define BUFFER_SIZE         512L                       /*  File I/O buffer size       */
#define eip1140a_MANF_ID      0x0fff                   /*  Instrument manufacturer ID */
#define eip1140a_MODEL_CODE   0x0243                   /*  Instrument model code      */

/*= HP E1412A 6.5 Digit Multimeter ==========================================*/    
/* LabWindows/CVI 3.1 Instrument Driver                                      */
/* Original Release:                                                         */
/* By: EJR, NI                                                               */
/*     PH. (800) 433-3488   Fax (512) 794-5678                               */
/*                                                                           */
/* Modification History: None                                                */
/*===========================================================================*/
 
/*****************************************************************************/
/*= INSTRUMENT-DEPENDENT COMMAND ARRAYS =====================================*/
/*****************************************************************************/

/*****************************************************************************/
/*= INSTRUMENT-DEPENDENT STATUS/RANGE STRUCTURE  ============================*/
/*****************************************************************************/

/* eip1140a_stringValPair is used in the eip1140a_errorMessage function      */
/* eip1140a_statusDataRanges is used to track session dependent status & ranges*/
/*===========================================================================*/
typedef struct  eip1140a_stringValPair
{
   ViStatus stringVal;
   ViString stringName;
}  eip1140a_tStringValPair;

/*===========================================================================*/
/* Change to reflect the global status variables that your driver needs      */
/* to keep track of.  For example trigger mode of each session               */
/*===========================================================================*/
struct eip1140a_statusDataRanges {
    ViInt16 triggerMode;
    ViInt16 val2;
    ViInt16 val3;
    ViChar instrDriverRevision[256];
};

typedef struct eip1140a_statusDataRanges *eip1140a_instrRange;

/*****************************************************************************/
/*= UTILITY ROUTINE DECLARATIONS (Non-Exportable Functions) =================*/
/*****************************************************************************/
ViBoolean eip1140a_invalidViBooleanRange (ViBoolean val);
ViBoolean eip1140a_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max);
ViBoolean eip1140a_invalidViInt32Range (ViInt32 val, ViInt32 min, ViInt32 max);
ViBoolean eip1140a_invalidViUInt16Range (ViUInt16 val, ViUInt16 min, ViUInt16 max);
ViBoolean eip1140a_invalidViUInt32Range (ViUInt32 val, ViUInt32 min, ViUInt32 max);
ViBoolean eip1140a_invalidViReal32Range (ViReal32 val, ViReal32 min, ViReal32 max);
ViBoolean eip1140a_invalidViReal64Range (ViReal64 val, ViReal64 min, ViReal64 max);
ViStatus eip1140a_initCleanUp (ViSession openRMSession, ViPSession openInstrSession, ViStatus currentStatus);
ViStatus eip1140a_readToFile (ViSession instrSession, ViString filename, ViUInt32 readBytes, ViPUInt32 retCount);
ViStatus eip1140a_writeFromFile (ViSession instrSession, ViString filename, ViUInt32 writeBytes, ViPUInt32 retCount);
ViStatus eip1140a_defaultInstrSetup (ViSession openInstrSession);

extern ViStatus _VI_FUNC eip1140a_waitForRqs (ViSession instrSession, 
                        ViInt32 timeout, ViInt16 *statusByte);

/*****************************************************************************/
/*====== USER-CALLABLE FUNCTIONS (Exportable Functions) =====================*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Initialize                                                      */
/* Purpose:  This function opens the instrument, queries the instrument      */
/*           for its ID, and initializes the instrument to a known state.    */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_init (ViRsrc resourceName, ViBoolean IDQuery,
                    ViBoolean resetDev, ViPSession instrSession)
{
    ViStatus eip1140a_status = VI_SUCCESS;
    ViSession rmSession = 0;
    ViUInt16 manfID = 0, modelCode = 0;    

    /*- Check input parameter ranges ----------------------------------------*/
    if (eip1140a_invalidViBooleanRange (IDQuery))
        return VI_ERROR_PARAMETER2;
    if (eip1140a_invalidViBooleanRange (resetDev))
        return VI_ERROR_PARAMETER3;

    /*- Open instrument session ---------------------------------------------*/
    if ((eip1140a_status = viOpenDefaultRM (&rmSession)) < 0)
        return eip1140a_status;
    if ((eip1140a_status = viOpen (rmSession, resourceName, VI_NULL, VI_NULL, instrSession)) < 0) {
        viClose (rmSession);
        return eip1140a_status;
    }

    /*- Configure VISA Formatted I/O ----------------------------------------*/
    if ((eip1140a_status = viSetAttribute (*instrSession, VI_ATTR_TMO_VALUE, 10000)) < 0)
            return eip1140a_initCleanUp (rmSession, instrSession, eip1140a_status);
    if ((eip1140a_status = viSetBuf (*instrSession, VI_READ_BUF|VI_WRITE_BUF, 4000)) < 0)
            return eip1140a_initCleanUp (rmSession, instrSession, eip1140a_status);
    if ((eip1140a_status = viSetAttribute (*instrSession, VI_ATTR_WR_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS)) < 0)
            return eip1140a_initCleanUp (rmSession, instrSession, eip1140a_status);
    if ((eip1140a_status = viSetAttribute (*instrSession, VI_ATTR_RD_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS)) < 0)
            return eip1140a_initCleanUp (rmSession, instrSession, eip1140a_status);

    /*- Identification Query ------------------------------------------------*/
    if (IDQuery) {
        if ((eip1140a_status = viGetAttribute (*instrSession, VI_ATTR_MANF_ID, &manfID)) < 0)
            return eip1140a_initCleanUp (rmSession, instrSession, eip1140a_status);
        if ((eip1140a_status = viGetAttribute (*instrSession, VI_ATTR_MODEL_CODE, &modelCode)) < 0)
            return eip1140a_initCleanUp (rmSession, instrSession, eip1140a_status);
        
        if (manfID != eip1140a_MANF_ID && modelCode != eip1140a_MODEL_CODE)
            return eip1140a_initCleanUp (rmSession, instrSession, VI_ERROR_FAIL_ID_QUERY);
    }

    /*- Reset instrument ----------------------------------------------------*/
    if (resetDev) {
        if ((eip1140a_status = eip1140a_reset (*instrSession)) < 0)
            return eip1140a_initCleanUp (rmSession, instrSession, eip1140a_status);
    }       
    else  /*- Send Default Instrument Setup ---------------------------------*/
        if ((eip1140a_status = eip1140a_defaultInstrSetup (*instrSession)) < 0)
            return eip1140a_initCleanUp (rmSession, instrSession, eip1140a_status);

    return eip1140a_status;
}


/*===========================================================================*/
/* Function: Write To Instrument                                             */
/* Purpose:  This function writes a command string to the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_writeInstrData (ViSession instrSession, 
                        ViString writeBuffer)
{
    ViStatus eip1140a_status = VI_SUCCESS;
    
    if ((eip1140a_status = viPrintf (instrSession, "%s", writeBuffer)) < 0)
        return eip1140a_status;

    return eip1140a_status;
}

/*===========================================================================*/
/* Function: Read Instrument Buffer                                          */
/* Purpose:  This function reads the output buffer of the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_readInstrData (ViSession instrSession, 
                        ViInt16 numBytes, ViPBuf rdBuf, ViPInt32 bytesRead)
 {
    ViStatus eip1140a_status = VI_SUCCESS;
    *bytesRead = 0L;
        

    if ((eip1140a_status = viRead (instrSession, rdBuf, numBytes, (ViPUInt32) bytesRead)) < 0)
        return eip1140a_status;

    return eip1140a_status;
}

/*===========================================================================*/
/* Function: Reset                                                           */
/* Purpose:  This function resets the instrument.                            */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_reset (ViSession instrSession)
{
    ViStatus eip1140a_status = VI_SUCCESS;

    /*  Initialize the instrument to a known state.  */
	if ((eip1140a_status = viPrintf(instrSession, "%s", "*RST")) != VI_SUCCESS)
		return eip1140a_status;

    if ((eip1140a_status = eip1140a_defaultInstrSetup (instrSession)) < 0)  
        return eip1140a_status;
        
    return eip1140a_status;
}

/*===========================================================================*/
/* Function: Error Query                                                     */
/* Purpose:  This function queries the instrument error queue, and returns   */
/*           the result.                                                     */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_errorQuery (ViSession instrSession, 
                        ViPInt32 errCode, ViChar errMessage[])
{
    ViStatus eip1140a_status = VI_SUCCESS;

	if ((eip1140a_status = viPrintf(instrSession, "%s", ":SYST:ERR?")) != VI_SUCCESS)
		return eip1140a_status;

    if ((eip1140a_status = viScanf (instrSession, "%ld,\"%[^\"]", errCode, errMessage)) < 0)
        return eip1140a_status;
    
    return eip1140a_status;
}

/*===========================================================================*/
/* Function: Error Message                                                   */
/* Purpose:  This function translates the error return value from the        */
/*           instrument driver into a user-readable string.                  */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_errorMessage (ViSession instrSession, 
                        ViStatus errorCode, ViChar errMessage[])
{
    ViStatus eip1140a_status = VI_SUCCESS;
    ViInt16 i;
    static eip1140a_tStringValPair statusDescArray[] = {
        {VI_WARN_NSUP_ID_QUERY,     "WARNING: ID Query not supported"},
        {VI_WARN_NSUP_RESET,        "WARNING: Reset not supported"},
        {VI_WARN_NSUP_SELF_TEST,    "WARNING: Self-test not supported"},
        {VI_WARN_NSUP_ERROR_QUERY,  "WARNING: Error Query not supported"},     
        {VI_WARN_NSUP_REV_QUERY,    "WARNING: Revision Query not supported"},
        {VI_ERROR_PARAMETER1,   "ERROR: Parameter 1 out of range"},
        {VI_ERROR_PARAMETER2,   "ERROR: Parameter 2 out of range"},
        {VI_ERROR_PARAMETER3,   "ERROR: Parameter 3 out of range"},
        {VI_ERROR_PARAMETER4,   "ERROR: Parameter 4 out of range"},
        {VI_ERROR_PARAMETER5,   "ERROR: Parameter 5 out of range"},
        {VI_ERROR_PARAMETER6,   "ERROR: Parameter 6 out of range"},
        {VI_ERROR_PARAMETER7,   "ERROR: Parameter 7 out of range"},
        {VI_ERROR_PARAMETER8,   "ERROR: Parameter 8 out of range"},
        {VI_ERROR_FAIL_ID_QUERY,"ERROR: Identification query failed"},
        {VI_ERROR_INV_RESPONSE, "ERROR: Interpreting instrument response"},
        {VI_ERROR_FILE_OPEN,    "ERROR: Opening the specified file"},
        {VI_ERROR_FILE_WRITE,   "ERROR: Writing to the specified file"},
        {VI_ERROR_INTERPRETING_RESPONSE, "ERROR: Interpreting the instrument's response"},

/*===========================================================================*/
/*        The define statements for instrument specific WARNINGS and ERRORS  */
/*        need to be inserted in the include file "eip1140a.h".  The codes     */
/*        must be in the range of 0xBFFC0900 to 0xBFFC0FFF and defined as    */
/*        follows:                                                           */
/*        #define INSTRUMENT_SPECIFIC_ERROR (VI_INSTR_ERROR_OFFSET+n)        */
/*        Where n represents each instrument specific error number.  Valid   */
/*        ranges for n are 0 to 0x6FF.                                       */
/*                                                                           */
/*        The INSTRUMENT_SPECIFIC_ERROR constants and string descriptor then */
/*        need to be added here.  Example:                                   */                
/*        {INSTRUMENT_SPECIFIC_ERROR, "ERROR: Instrument specific error"},   */
/*===========================================================================*/

        {VI_NULL, VI_NULL}
    };

    eip1140a_status = viStatusDesc (instrSession, errorCode, errMessage);
    if (eip1140a_status == VI_WARN_UNKNOWN_STATUS) {
        for (i=0; statusDescArray[i].stringName; i++) {
            if (statusDescArray[i].stringVal == errorCode) {
                strcpy_s(errMessage, 132, statusDescArray[i].stringName);
                return (VI_SUCCESS);
            }
        }
        sprintf_s(errMessage, 132, "Unknown Error 0x%08lX", errorCode);
        return (VI_WARN_UNKNOWN_STATUS);
    }
    
    eip1140a_status = VI_SUCCESS;
    return eip1140a_status;
}
 
/*===========================================================================*/
/* Function: Revision Query                                                  */
/* Purpose:  This function returns the driver and instrument revisions.      */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_revisionQuery (ViSession instrSession,
                        ViChar driverRev[], ViChar instrRev[])
{
    ViStatus eip1140a_status = VI_SUCCESS;

	if ((eip1140a_status = viPrintf(instrSession, "%s", "*IDN?")) != VI_SUCCESS)
		return eip1140a_status;

    if ((eip1140a_status = viScanf (instrSession, "%*[^,],%*[^,],%*[^,],%[^\n]", instrRev)) < 0)
        return eip1140a_status;

    strcpy_s(driverRev, 132, eip1140a_REVISION);
    
    return eip1140a_status;
}
 
/*===========================================================================*/
/* Function: Close                                                           */
/* Purpose:  This function closes the instrument.                            */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_close (ViSession instrSession)
{
    eip1140a_instrRange instrPtr;
    ViSession rmSession;
    ViStatus eip1140a_status = VI_SUCCESS;

    if ((eip1140a_status = viGetAttribute (instrSession, VI_ATTR_RM_SESSION, &rmSession)) < 0)
        return eip1140a_status;
    if ((eip1140a_status = viGetAttribute (instrSession, VI_ATTR_USER_DATA, &instrPtr)) < 0)
        return eip1140a_status;
    
    /*- Disable Event SRQ ---------------------------------------------------*/
    if ((eip1140a_status = viDisableEvent(instrSession, VI_EVENT_SERVICE_REQ, VI_QUEUE)) < 0) 
        return eip1140a_status;
    
    free (instrPtr);
    
    eip1140a_status = viClose (instrSession);
    viClose (rmSession);

    return eip1140a_status;
}

/*****************************************************************************/
/*= UTILITY ROUTINES (Non-Exportable Functions) =============================*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Boolean Value Out Of Range - ViBoolean                          */
/* Purpose:  This function checks a Boolean to see if it is equal to VI_TRUE */
/*           or VI_FALSE. If the value is out of range, the return value is  */
/*           VI_TRUE, otherwise the return value is VI_FALSE.                */
/*===========================================================================*/
ViBoolean eip1140a_invalidViBooleanRange (ViBoolean val)
{
    return (unsigned short)((val != VI_FALSE && val != VI_TRUE) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Short Signed Integer Value Out Of Range - ViInt16               */
/* Purpose:  This function checks a short signed integer value to see if it  */  
/*           lies between a minimum and maximum value.  If the value is out  */
/*           of range, the return value is VI_TRUE, otherwise the return     */
/*           value is VI_FALSE.                                              */
/*===========================================================================*/
ViBoolean eip1140a_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max)
{
    return (unsigned short)((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Long Signed Integer Value Out Of Range - ViInt32                */
/* Purpose:  This function checks a long signed integer value to see if it   */  
/*           lies between a minimum and maximum value.  If the value is out  */
/*           of range,  the return value is VI_TRUE, otherwise the return    */
/*           value is VI_FALSE.                                              */
/*===========================================================================*/
ViBoolean eip1140a_invalidViInt32Range  (ViInt32 val, ViInt32 min, ViInt32 max)
{
    return (unsigned short)((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Short Unsigned Integer Value Out Of Range - ViUInt16            */
/* Purpose:  This function checks a short unsigned integer value to see if it*/  
/*           lies between a minimum and maximum value.  If the value is out  */
/*           of range,  the return value is VI_TRUE, otherwise the return    */
/*           value is VI_FALSE.                                              */
/*===========================================================================*/
ViBoolean eip1140a_invalidViUInt16Range  (ViUInt16 val, ViUInt16 min, ViUInt16 max)
{
    return (unsigned short)((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Long Unsigned Integer Value Out Of Range - ViUInt32             */
/* Purpose:  This function checks a long unsigned integer value to see if it */  
/*           lies between a minimum and maximum value.  If the value is out  */
/*           of range,  the return value is VI_TRUE, otherwise the return    */
/*           value is VI_FALSE.                                              */
/*===========================================================================*/
ViBoolean eip1140a_invalidViUInt32Range  (ViUInt32 val, ViUInt32 min, ViUInt32 max)
{
    return (unsigned short)((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Real (Float) Value Out Of Range - ViReal32                      */
/* Purpose:  This function checks a real (float) value to see if it lies     */  
/*           between a minimum and maximum value.  If the value is out of    */
/*           range, the return value is VI_TRUE, otherwise the return value  */
/*           is VI_FALSE.                                                    */
/*===========================================================================*/
ViBoolean eip1140a_invalidViReal32Range  (ViReal32 val, ViReal32 min, ViReal32 max)
{
    return (unsigned short)((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Real (Double) Value Out Of Range - ViReal64                     */
/* Purpose:  This function checks a real (double) value to see if it lies    */  
/*           between a minimum and maximum value.  If the value is out of    */
/*           range, the return value is VI_TRUE, otherwise the return value  */
/*           is VI_FALSE.                                                    */
/*===========================================================================*/
ViBoolean eip1140a_invalidViReal64Range  (ViReal64 val, ViReal64 min, ViReal64 max)
{
    return (unsigned short)((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Initialize Clean Up                                             */
/* Purpose:  This function is used only by the eip1140a_init function.  When   */
/*           an error is detected this function is called to close the       */
/*           open resource manager and instrument object sessions and to     */
/*           set the instrSession that is returned from eip1140a_init to       */
/*           VI_NULL.                                                        */
/*===========================================================================*/
ViStatus eip1140a_initCleanUp (ViSession openRMSession,
                        ViPSession openInstrSession, ViStatus currentStatus)
{
    viClose (*openInstrSession);
    viClose (openRMSession);
    *openInstrSession = VI_NULL;
    
    return currentStatus;
}

/*===========================================================================*/
/* Function: Read To File From Instrument                                    */
/* Purpose:  This function is used to read data from the instrument and      */
/*           write it to a user specified file.                              */
/*===========================================================================*/
ViStatus eip1140a_readToFile (ViSession instrSession, ViString filename,
                        ViUInt32 readBytes, ViPUInt32 retCount)
{
    ViStatus  eip1140a_status = VI_SUCCESS;
    ViByte    buffer[BUFFER_SIZE];
    ViUInt32  bytesReadInstr = 0, bytesWrittenFile = 0;
    FILE     *targetFile;

    *retCount = 0L;
    if ((fopen_s ( &targetFile, filename, "wb")) != VI_NULL)
        return VI_ERROR_FILE_OPEN; /* not defined by VTL */

    for (;;) {
        if (readBytes > BUFFER_SIZE)
            eip1140a_status = viRead (instrSession, buffer, BUFFER_SIZE, &bytesReadInstr);
        else
            eip1140a_status = viRead (instrSession, buffer, readBytes, &bytesReadInstr);

        bytesWrittenFile = fwrite (buffer, sizeof (ViByte), (size_t)bytesReadInstr, targetFile);
        *retCount += bytesWrittenFile;
        if (bytesWrittenFile < bytesReadInstr)
            eip1140a_status = VI_ERROR_FILE_WRITE; /* not defined by VTL */

        if ((readBytes <= BUFFER_SIZE) || (eip1140a_status <= 0) || (eip1140a_status == VI_SUCCESS_TERM_CHAR))
            break;

        readBytes -= BUFFER_SIZE;
    }

    fclose (targetFile);
    return eip1140a_status;
}

/*===========================================================================*/
/* Function: Write From File To Instrument                                   */
/* Purpose:  This function is used to read data from a user specified file   */
/*           and write it to the instrument.                                 */
/*===========================================================================*/
ViStatus eip1140a_writeFromFile (ViSession instrSession, ViString filename,
                        ViUInt32 writeBytes, ViPUInt32 retCount)
{
    ViStatus  eip1140a_status = VI_SUCCESS;
    ViByte    buffer[BUFFER_SIZE];
    ViUInt32  bytesRead = 0, bytesWritten = 0;
    FILE     *sourceFile;
    ViBoolean sendEnd = VI_FALSE;

    *retCount = 0L;
    if (( fopen_s( &sourceFile, filename, "rb")) != VI_NULL)
        return VI_ERROR_FILE_OPEN; /* not defined by VTL */

    while (!feof (sourceFile)) {
        bytesRead = (ViUInt32)fread (buffer, sizeof (ViByte), BUFFER_SIZE, sourceFile);
        if ((writeBytes > BUFFER_SIZE) && (bytesRead == BUFFER_SIZE)) {
            viGetAttribute (instrSession, VI_ATTR_SEND_END_EN, &sendEnd);
            viSetAttribute (instrSession, VI_ATTR_SEND_END_EN, VI_FALSE);
            eip1140a_status = viWrite (instrSession, buffer, BUFFER_SIZE, &bytesWritten);
            viSetAttribute (instrSession, VI_ATTR_SEND_END_EN, sendEnd);
            writeBytes -= BUFFER_SIZE;
            *retCount += bytesWritten;
            if (eip1140a_status < 0)
                break;
        }
        else {
            eip1140a_status = viWrite (instrSession, buffer, ((bytesRead < writeBytes) ? bytesRead : writeBytes), &bytesWritten);
            *retCount += bytesWritten;
            break;
        }
    }

    fclose (sourceFile);
    return eip1140a_status;
}

/*****************************************************************************/
/*----------- INSERT INSTRUMENT-DEPENDENT UTILITY ROUTINES HERE -------------*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Default Instrument Setup                                        */
/* Purpose:  This function sends a default setup to the instrument.  This    */
/*           function is called by the eip1140a_reset operation and by the     */
/*           eip1140a_init function if the reset option has not been           */
/*           selected.  This function is useful for configuring any          */
/*           instrument settings that are required by the rest of the        */
/*           instrument driver functions such as turning headers ON or OFF   */
/*           or using the long or short form for commands, queries, and data.*/                                    
/*===========================================================================*/
ViStatus eip1140a_defaultInstrSetup (ViSession instrSession)
{
    ViStatus eip1140a_status = VI_SUCCESS;
    
    eip1140a_instrRange instrPtr;
    instrPtr =(struct eip1140a_statusDataRanges *) malloc (sizeof (struct eip1140a_statusDataRanges));

/*===========================================================================*/
/* Change to reflect the global status variables that your driver needs      */
/* to keep track of.  For example trigger mode of each session               */
/*===========================================================================*/
    instrPtr -> triggerMode = 0;
    instrPtr -> val2 = 0;
    instrPtr -> val3 = 0;
    strcpy_s(instrPtr -> instrDriverRevision, sizeof(instrPtr -> instrDriverRevision), eip1140a_REVISION);
    
    if (viSetAttribute (instrSession, VI_ATTR_USER_DATA, (ViUInt32)instrPtr) != 0)
        return eip1140a_status;

    /*- Enable Event SRQ ----------------------------------------------------*/
    if ((eip1140a_status = viEnableEvent(instrSession, VI_EVENT_SERVICE_REQ, VI_QUEUE, VI_NULL)) < 0) 
        return eip1140a_status;

    return eip1140a_status;
}

/*===========================================================================*/
/* Function: Wait for RQS                                                    */
/* Purpose:  This function waits for a service request.                      */
/*===========================================================================*/
ViStatus _VI_FUNC eip1140a_waitForRqs (ViSession instrSession, 
                        ViInt32 timeout, ViPInt16 statusByte)
{
    ViStatus eip1140a_status = VI_SUCCESS;
    ViEventType eventType;
    ViEvent  dupHandle;
    
    if ((eip1140a_status = viWaitOnEvent (instrSession, VI_EVENT_SERVICE_REQ, timeout, &eventType, &dupHandle)) < 0)
        return eip1140a_status;
    if ((eip1140a_status = viReadSTB (instrSession, (ViPUInt16) statusByte)) < 0)
        return eip1140a_status;
    eip1140a_status = viClose (dupHandle);

    return eip1140a_status;
}


/*****************************************************************************/
/*=== END INSTRUMENT DRIVER SOURCE CODE =====================================*/
/*****************************************************************************/

