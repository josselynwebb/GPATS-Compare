/*****************************************************************************/
/*   Copyright 1995 National Instruments Corporation.  All Rights Reserved.  */
/*****************************************************************************/

#include <formatio.h>


#include <visa.h>
#include <ansi_c.h>
#include "aps6062.h"

#define aps6062_REVISION     "Rev 1.0b, 9/95, CVI 3.1" /*  Instrument driver revision */
#define BUFFER_SIZE     256L         /* File I/O buffer size */
#define MAX_CMD_LENGTH  256L
    
/* = APS6062 Programmable Power Source(VISA) =============================== */
/*  LabWindows 3.1 Instrument Driver                                         */
/*  Original Release: October, 1992                                          */
/*  By: HG, LWILDP, National Instruments, Austin, TX                         */
/*     PH. (800)433-3488   Fax (512)794-5678                                 */
/*  Originally written in C                                                  */
/*  Modification History:                                                    */
/*                                                                           */
/*****************************************************************************/
/*= INSTRUMENT-DEPENDENT COMMAND ARRAYS =====================================*/
/*****************************************************************************/
static ViString funcStr[] = {"VOLT:AC", "VOLT", "VOLT:RAT", "CURR:AC", "CURR", "RES","FRES",    
                             "FREQ", "PER", "CONT", "DIOD"}; 
static ViString func2Str[] = {"VOLT:AC","VOLT","VOLT:RAT","CURR:AC","CURR","RES","FRES",    
                              "FREQ:VOLT" ,"PER:VOLT"};
static ViString resolStr[] = {"MAX","DEF","MIN"};
static ViString resolModeStr[] = {"MIN","MAX"};
static ViString mathOpStr[] = {"NULL","AVER","DB","DBM","LIM"};
static ViString trigSrceStr[] = {"IMM", "EXT", "BUS"};
static ViString terminalsStr[] = {"REAR","FRON"};
static ViString autoZeroStr[] = {"OFF","ON","ONCE"};
static ViString autoImpedStr[] = {"OFF","ON"};
static ViString beeperState[] = {"OFF","ON"};
static ViReal64 funLowRange[] = {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 3.0, 0.0000033};
static ViReal64 funHighRange[] = {100.0, 100.0, 0.0, 3.0, 3.0, 100000000.0, 100000000.0,
                                  300000.0, 0.33};
  
/*****************************************************************************/
/*= INSTRUMENT-DEPENDENT STATUS/RANGE STRUCTURE  ============================*/
/*****************************************************************************/
/* aps6062_stringValPair is used in the aps6062_errorMessage function      */
/* aps6062_statusDataRanges is used to track session dependent status & ranges*/
/*===========================================================================*/
typedef struct  aps6062_stringValPair
{
   ViStatus stringVal;
   ViString stringName;
}  aps6062_tStringValPair;
 
struct aps6062_statusDataRanges {
    ViInt16 triggerMode;
    ViInt16 val2;
    ViInt16 val3;
    ViChar instrDriverRevision[256];
};

typedef struct aps6062_statusDataRanges *aps6062_instrRange;

/*****************************************************************************/
/*= UTILITY ROUTINE DECLARATIONS (Non-Exportable Functions) =================*/
/*****************************************************************************/
ViBoolean aps6062_invalidViBooleanRange (ViBoolean val);
ViBoolean aps6062_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max);
ViBoolean aps6062_invalidViInt32Range (ViInt32 val, ViInt32 min, ViInt32 max);
ViBoolean aps6062_invalidViUInt16Range (ViUInt16 val, ViUInt16 min, ViUInt16 max);
ViBoolean aps6062_invalidViUInt32Range (ViUInt32 val, ViUInt32 min, ViUInt32 max);
ViBoolean aps6062_invalidViReal32Range (ViReal32 val, ViReal32 min, ViReal32 max);
ViBoolean aps6062_invalidViReal64Range (ViReal64 val, ViReal64 min, ViReal64 max);
ViStatus aps6062_initCleanUp (ViSession openRMSession, ViPSession openInstrSession, ViStatus currentStatus);
ViStatus aps6062_readToFile (ViSession instrSession, ViString filename, ViUInt32 readBytes, ViPUInt32 retCount);
ViStatus aps6062_writeFromFile (ViSession instrSession, ViString filename, ViUInt32 writeBytes, ViPUInt32 retCount);
ViStatus aps6062_defaultInstrSetup (ViSession openInstrSession);
ViInt16 aps6062_checkInstrStatus (ViStatus instrSession);
ViInt16 aps6062_stringToInt (ViChar *array_of_strings[],ViInt16 start, ViInt16 stop,
                                ViChar *string,ViInt16 * index);

/*****************************************************************************/
/*====== USER-CALLABLE FUNCTIONS (Exportable Functions) =====================*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Initialize                                                      */
/* Purpose:  This function opens the instrument, queries the instrument      */
/*           for its ID, and initializes the instrument to a known state.    */
/*===========================================================================*/
ViStatus _VI_FUNC aps6062_init (ViRsrc resourceName, ViBoolean IDQuery,
                    ViBoolean reset, ViPSession instrSession)
{
    ViStatus aps6062_status = VI_SUCCESS;
    ViUInt32 retCnt = 0;
    ViByte rdBuffer[BUFFER_SIZE];
    ViSession rmSession = 0;

    /*- Check input parameter ranges ----------------------------------------*/
    if (aps6062_invalidViBooleanRange (IDQuery))
        return VI_ERROR_PARAMETER2;
    if (aps6062_invalidViBooleanRange (reset))
        return VI_ERROR_PARAMETER3;

    /*- Open instrument session ---------------------------------------------*/
    if ((aps6062_status = viOpenDefaultRM (&rmSession)) < 0)
        return aps6062_status;

    if ((aps6062_status = viOpen (rmSession, resourceName, VI_NULL, VI_NULL, instrSession)) < 0) {
        viClose (rmSession);
        return aps6062_status;
    }

    /*- Configure VISA Formatted I/O ----------------------------------------*/
    if ((aps6062_status = viSetAttribute (*instrSession, VI_ATTR_TMO_VALUE, 10000)) < 0)
        return aps6062_initCleanUp (rmSession, instrSession, aps6062_status);
    if ((aps6062_status = viSetBuf (*instrSession, VI_READ_BUF|VI_WRITE_BUF, 4000)) < 0)
        return aps6062_initCleanUp (rmSession, instrSession, aps6062_status);
    if ((aps6062_status = viSetAttribute (*instrSession, VI_ATTR_WR_BUF_OPER_MODE, VI_FLUSH_ON_ACCESS)) < 0)
        return aps6062_initCleanUp (rmSession, instrSession, aps6062_status);
    if ((aps6062_status = viSetAttribute (*instrSession, VI_ATTR_RD_BUF_OPER_MODE, VI_FLUSH_ON_ACCESS)) < 0)
        return aps6062_initCleanUp (rmSession, instrSession, aps6062_status);

    /*- Identification Query ------------------------------------------------*/
    if (IDQuery) {
        if ((aps6062_status = viWrite (*instrSession, "*IDN?", 5, &retCnt)) < 0)
            return aps6062_initCleanUp (rmSession, instrSession, aps6062_status);
        if ((aps6062_status = viRead (*instrSession, rdBuffer, BUFFER_SIZE, &retCnt)) < 0)
            return aps6062_status;

        Scan (rdBuffer, "HEWLETT-PACKARD,34401A");
        if (NumFmtdBytes () != 22) 
            return aps6062_initCleanUp (rmSession, instrSession, VI_ERROR_FAIL_ID_QUERY);
    }

    /*- Reset instrument ----------------------------------------------------*/
    if (reset) {
        if ((aps6062_status = aps6062_reset (*instrSession)) < 0)
            return aps6062_initCleanUp (rmSession, instrSession, aps6062_status);
    }       
    else  /*- Send Default Instrument Setup ---------------------------------*/
        if ((aps6062_status = aps6062_defaultInstrSetup (*instrSession)) < 0)
            return aps6062_initCleanUp (rmSession, instrSession, aps6062_status);
                
    return aps6062_status;
}

/*===========================================================================*/
/* Function: Write To Instrument                                             */
/* Purpose:  This function writes a command string to the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC aps6062_writeInstrData (ViSession instrSession, ViString writeBuffer)
{
 	ViUInt32 retCnt = 0;     
    ViStatus aps6062_status = VI_SUCCESS;
    
     if ((aps6062_status = viWrite (instrSession, writeBuffer, 3, &retCnt)) < 0)
        return aps6062_status;

    return aps6062_status;
}

/*===========================================================================*/
/* Function: Read Instrument Buffer                                          */
/* Purpose:  This function reads the output buffer of the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC aps6062_readInstrData (ViSession instrSession, ViInt16 numBytes,
                    ViChar rdBuf[], ViPInt32 bytesRead)
{
    ViStatus aps6062_status = VI_SUCCESS;
    *bytesRead = 0L;
        
    if ((aps6062_status = viRead (instrSession, rdBuf, numBytes, bytesRead)) < 0)
        return aps6062_status;

    return aps6062_status;
}

/*===========================================================================*/
/* Function: Reset                                                           */
/* Purpose:  This function resets the instrument.  If the reset function     */
/*           is not supported by the instrument, this function returns       */
/*           the warning VI_WARN_NSUP_RESET.                                 */
/*===========================================================================*/
ViStatus _VI_FUNC aps6062_reset (ViSession instrSession)
{
    ViUInt32 retCnt = 0;
    ViStatus aps6062_status = VI_SUCCESS;

    /*  Initialize the instrument to a known state.  */
    if ((aps6062_status = viWrite (instrSession, "*CLS\r\n", 6, &retCnt)) < 0)
        return aps6062_status;

    if ((aps6062_status = aps6062_defaultInstrSetup (instrSession)) < 0)  
        return aps6062_status;
        
     /*  Set timeout to 10 seconds  */
    aps6062_status = viSetAttribute (instrSession, VI_ATTR_TMO_VALUE,10000);
   
    return aps6062_status;
}

/*===========================================================================*/
/* Function: Self-Test                                                       */
/* Purpose:  This function executes the instrument self-test and returns     */
/*           the result. If the self test function is not supported by the   */
/*           instrument, this function returns the warning                   */
/*           VI_WARN_NSUP_SELF_TEST.                                         */
/*===========================================================================*/
ViStatus _VI_FUNC aps6062_selfTest (ViSession instrSession, ViPInt16 testResult,
                    ViChar testMessage[])
{
    ViUInt32 retCnt = 0;
    ViStatus aps6062_status = VI_SUCCESS;
    
    if ((aps6062_status = viWrite (instrSession, "*TST?\r\n", 7, &retCnt)) < 0)
        return aps6062_status;

    if ((aps6062_status = viScanf (instrSession, "%hd", testResult)) < 0)
        return aps6062_status;
    Fmt (testMessage, "%s<%d[b2]", *testResult);
    
    return aps6062_status;
}

/*===========================================================================*/
/* Function: Error Query                                                     */
/* Purpose:  This function queries the instrument error queue, and returns   */
/*           the result. If the error query function is not supported by the */
/*           instrument, this function returns the warning                   */
/*           VI_WARN_NSUP_ERROR_QUERY.                                       */
/*===========================================================================*/
ViStatus _VI_FUNC aps6062_errorQuery (ViSession instrSession, ViPInt32 errCode,
                    ViChar errMessage[])
{
    ViUInt32 retCnt = 0;
    ViStatus aps6062_status = VI_SUCCESS;

    if ((aps6062_status = viWrite (instrSession, ":SYST:ERR?\r\n", 12, &retCnt)) < 0)
        return aps6062_status;
    
    if ((aps6062_status = viScanf (instrSession, "%ld,\"%[^\"]", errCode, errMessage)) < 0)
        return aps6062_status;


    return aps6062_status;
}

/*===========================================================================*/
/* Function: Error Message                                                   */
/* Purpose:  This function translates the error return value from the        */
/*           instrument driver into a user-readable string.                  */
/*===========================================================================*/
ViStatus _VI_FUNC aps6062_errorMessage (ViSession instrSession, ViStatus errorCode,
                    ViChar errMessage[])
{
    ViStatus aps6062_status = VI_SUCCESS;
    ViInt16 i;
    static aps6062_tStringValPair statusDescArray[] = {
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
        {VI_ERROR_SECURITY_CODE,    "ERROR: Security code too long"},
        {VI_ERROR_STAMP,    "ERROR: STAMP TOO LONG"},
        {VI_ERROR_OUTOFF_RANGE, "ERROR: Data outoff range."},
        {VI_NULL, VI_NULL}
    };

    aps6062_status = viStatusDesc (instrSession, errorCode, errMessage);
    if (aps6062_status == VI_WARN_UNKNOWN_STATUS) {
        for (i=0; statusDescArray[i].stringName; i++) {
            if (statusDescArray[i].stringVal == errorCode) {
                strcpy (errMessage, statusDescArray[i].stringName);
                return (VI_SUCCESS);
            }
        }
        sprintf (errMessage, "Unknown Error 0x%08lX", errorCode);
        return (VI_WARN_UNKNOWN_STATUS);
    }
    
    aps6062_status = VI_SUCCESS;
    return aps6062_status;
}


/*===========================================================================*/
/* Function: Close                                                           */
/* Purpose:  This function closes the instrument.                            */
/*===========================================================================*/
ViStatus _VI_FUNC aps6062_close (ViSession instrSession)
{
    aps6062_instrRange instrPtr;
    ViSession rmSession;
    ViStatus aps6062_status = VI_SUCCESS;

    if ((aps6062_status = viGetAttribute (instrSession, VI_ATTR_RM_SESSION, &rmSession)) < 0)
        return aps6062_status;
    if ((aps6062_status = viGetAttribute (instrSession, VI_ATTR_USER_DATA, &instrPtr)) < 0)
        return aps6062_status;
    
    free (instrPtr);
    
    aps6062_status = viClose (instrSession);
    viClose (rmSession);

    return aps6062_status;
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
ViBoolean aps6062_invalidViBooleanRange (ViBoolean val)
{
    return ((val != VI_FALSE && val != VI_TRUE) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Short Signed Integer Value Out Of Range - ViInt16               */
/* Purpose:  This function checks a short signed integer value to see if it  */  
/*           lies between a minimum and maximum value.  If the value is out  */
/*           of range, the return value is VI_TRUE, otherwise the return     */
/*           value is VI_FALSE.                                              */
/*===========================================================================*/
ViBoolean aps6062_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Long Signed Integer Value Out Of Range - ViInt32                */
/* Purpose:  This function checks a long signed integer value to see if it   */  
/*           lies between a minimum and maximum value.  If the value is out  */
/*           of range,  the return value is VI_TRUE, otherwise the return    */
/*           value is VI_FALSE.                                              */
/*===========================================================================*/
ViBoolean aps6062_invalidViInt32Range  (ViInt32 val, ViInt32 min, ViInt32 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Short Unsigned Integer Value Out Of Range - ViUInt16            */
/* Purpose:  This function checks a short unsigned integer value to see if it*/  
/*           lies between a minimum and maximum value.  If the value is out  */
/*           of range,  the return value is VI_TRUE, otherwise the return    */
/*           value is VI_FALSE.                                              */
/*===========================================================================*/
ViBoolean aps6062_invalidViUInt16Range  (ViUInt16 val, ViUInt16 min, ViUInt16 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Long Unsigned Integer Value Out Of Range - ViUInt32             */
/* Purpose:  This function checks a long unsigned integer value to see if it */  
/*           lies between a minimum and maximum value.  If the value is out  */
/*           of range,  the return value is VI_TRUE, otherwise the return    */
/*           value is VI_FALSE.                                              */
/*===========================================================================*/
ViBoolean aps6062_invalidViUInt32Range  (ViUInt32 val, ViUInt32 min, ViUInt32 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Real (Float) Value Out Of Range - ViReal32                      */
/* Purpose:  This function checks a real (float) value to see if it lies     */  
/*           between a minimum and maximum value.  If the value is out of    */
/*           range, the return value is VI_TRUE, otherwise the return value  */
/*           is VI_FALSE.                                                    */
/*===========================================================================*/
ViBoolean aps6062_invalidViReal32Range  (ViReal32 val, ViReal32 min, ViReal32 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Real (Double) Value Out Of Range - ViReal64                     */
/* Purpose:  This function checks a real (ViReal64) value to see if it lies  */  
/*           between a minimum and maximum value.  If the value is out of    */
/*           range, the return value is VI_TRUE, otherwise the return value  */
/*           is VI_FALSE.                                                    */
/*===========================================================================*/
ViBoolean aps6062_invalidViReal64Range  (ViReal64 val, ViReal64 min, ViReal64 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Initialize Clean Up                                             */
/* Purpose:  This function is used only by the aps6062_init function.  When */
/*           an error is detected this function is called to close the       */
/*           open resource manager and instrument object sessions and to     */
/*           set the instrSession that is returned from aps6062_init to     */
/*           VI_NULL.                                                        */
/*===========================================================================*/
ViStatus aps6062_initCleanUp (ViSession openRMSession,
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
ViStatus aps6062_readToFile (ViSession instrSession, ViString filename,
                    ViUInt32 readBytes, ViPUInt32 retCount)
{
    ViStatus  aps6062_status = VI_SUCCESS;
    ViByte    buffer[BUFFER_SIZE];
    ViUInt32  bytesReadInstr = 0, bytesWrittenFile = 0;
    FILE     *targetFile;

    *retCount = 0L;
    if ((targetFile = fopen (filename, "wb")) == VI_NULL)
        return VI_ERROR_FILE_OPEN; /* not defined by VTL */

    for (;;) {
        if (readBytes > BUFFER_SIZE)
            aps6062_status = viRead (instrSession, buffer, BUFFER_SIZE, &bytesReadInstr);
        else
            aps6062_status = viRead (instrSession, buffer, readBytes, &bytesReadInstr);

        bytesWrittenFile = fwrite (buffer, sizeof (ViByte), (size_t)bytesReadInstr, targetFile);
        *retCount += bytesWrittenFile;
        if (bytesWrittenFile < bytesReadInstr)
            aps6062_status = VI_ERROR_FILE_WRITE; /* not defined by VTL */

        if ((readBytes <= BUFFER_SIZE) || (aps6062_status <= 0) || (aps6062_status == VI_SUCCESS_TERM_CHAR))
            break;

        readBytes -= BUFFER_SIZE;
    }

    fclose (targetFile);
    return aps6062_status;
}

/*===========================================================================*/
/* Function: Write From File To Instrument                                   */
/* Purpose:  This function is used to read data from a user specified file   */
/*           and write it to the instrument.                                 */
/*===========================================================================*/
ViStatus aps6062_writeFromFile (ViSession instrSession, ViString filename,
                    ViUInt32 writeBytes, ViPUInt32 retCount)
{
    ViStatus  aps6062_status = VI_SUCCESS;
    ViByte    buffer[BUFFER_SIZE];
    ViUInt32  bytesRead = 0, bytesWritten = 0;
    FILE     *sourceFile;
    ViBoolean sendEnd = VI_FALSE;

    *retCount = 0L;
    if ((sourceFile = fopen (filename, "rb")) == VI_NULL)
        return VI_ERROR_FILE_OPEN; /* not defined by VTL */

    while (!feof (sourceFile)) {
        bytesRead = (ViUInt32)fread (buffer, sizeof (ViByte), BUFFER_SIZE, sourceFile);
        if ((writeBytes > BUFFER_SIZE) && (bytesRead == BUFFER_SIZE)) {
            viGetAttribute (instrSession, VI_ATTR_SEND_END_EN, &sendEnd);
            viSetAttribute (instrSession, VI_ATTR_SEND_END_EN, VI_FALSE);
            aps6062_status = viWrite (instrSession, buffer, BUFFER_SIZE, &bytesWritten);
            viSetAttribute (instrSession, VI_ATTR_SEND_END_EN, sendEnd);
            writeBytes -= BUFFER_SIZE;
            *retCount += bytesWritten;
            if (aps6062_status < 0)
                break;
        }
        else {
            aps6062_status = viWrite (instrSession, buffer, ((bytesRead < writeBytes) ? bytesRead : writeBytes), &bytesWritten);
            *retCount += bytesWritten;
            break;
        }
    }

    fclose (sourceFile);
    return aps6062_status;
}

/* ========================================================================= */
/*  Function Name:     Check Instrument Status                               */
/*  Description:       Checks the status of the Instrument                   */
/*                     and sets aps6062 Error accordingly.                  */
/*  Return Value:      aps6062 Error (Instrument Driver Status)             */
/* ========================================================================= */
ViInt16 aps6062_checkInstrStatus (ViStatus instrSession)
{
ViStatus aps6062_status = VI_SUCCESS;
ViUInt32 retCnt = 0;
ViByte  wrtBuf[BUFFER_SIZE],rdBuf[BUFFER_SIZE];
ViInt16 event_status;

    if ((viWrite (instrSession,"*ESR?\r\n",7,&retCnt)) < 0)
        return aps6062_status;
    if ((aps6062_status = viRead (instrSession,rdBuf,MAX_CMD_LENGTH,&retCnt)) < 0)
        return aps6062_status;

    if (Scan (rdBuf, "%s>%d[b2]", &event_status) != 1)  
        return VI_ERROR_INTERPRETING_RESPONSE;
                    
    return aps6062_status;
}

/* ========================================================================= */
/*  Function Name: String to Integer                                         */
/*  Description:   Compares a string to an array of strings and if a match   */
/*                 is found returns the array index of the match.            */
/*  Return Value:  Error (Instr. Driver Status)                              */
/* ========================================================================= */
ViInt16 aps6062_stringToInt (ViChar *array_of_strings[],ViInt16 start, ViInt16 stop,
                                ViChar *string,ViInt16 * index)
{
ViStatus    aps6062_status = VI_SUCCESS;
    ViInt16 i;
    ViInt16 found;

    aps6062_status = 0;
    found = 0;
    for (i = start; i <= stop && !found; i++)
        if (!CompareStrings (array_of_strings[i], 0, string, 0, 0))  {
            *index = i;
            found = 1;
        }
    if (!found)
        aps6062_status =  VI_ERROR_INTERPRETING_RESPONSE;
        
    return aps6062_status;
}

/*****************************************************************************/
/*----------- INSERT INSTRUMENT-DEPENDENT UTILITY ROUTINES HERE -------------*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Default Instrument Setup                                        */
/* Purpose:  This function sends a default setup to the instrument.  This    */
/*           function is called by the aps6062_reset operation and by the   */
/*           aps6062_init function if the reset option has not been         */
/*           selected.  This function is useful for configuring any          */
/*           instrument settings that are required by the rest of the        */
/*           instrument driver functions such as turning headers ON or OFF   */
/*           or using the long or short form for commands, queries, and data.*/                                    
/*===========================================================================*/
ViStatus aps6062_defaultInstrSetup (ViSession instrSession)
{
    ViStatus aps6062_status = VI_SUCCESS;
    ViUInt32 retCnt = 0;
    
    return aps6062_status;
}

/*****************************************************************************/
/*=== END INSTRUMENT DRIVER SOURCE CODE =====================================*/
/*****************************************************************************/
