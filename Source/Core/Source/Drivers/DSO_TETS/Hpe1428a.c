/***************************************************************************/
/*  Copyright 1996 National Instruments Corporation.  All Rights Reserved. */
/***************************************************************************/

#include <ansi_c.h>
#include <formatio.h>
#include <utility.h>
#include <visa.h>
#include "hpe1428a.h"

/*= DEFINES =================================================================*/
#define hpe1428a_REVISION     "Rev 1.1, 2/99, CVI 3.1" /*  Instrument driver revision */
#define BUFFER_SIZE          512L                       /*  File I/O buffer size       */
#define hpe1428a_MANF_ID      0x0fff                   /*  Instrument manufacturer ID */
#define hpe1428a_MODEL_CODE   0x0243                   /*  Instrument model code      */
#define MSG_RESP_REG          0xA	                   /*  Register offset where DOR bit is */

/*= HP E1428A Digitizing Oscilloscope =======================================*/    
/* LabWindows/CVI 3.1 Instrument Driver                                      */
/* Original Release:                                                         */
/* By: EJR, NI                                                               */
/*     PH. (800) 433-3488   Fax (512) 794-5678                               */
/*                                                                           */
/* Modification History: Version 1.0 David W. Hartley                        */
/* Extraneous subroutines were removed.                                      */
/* The A24 Memory routines were modified to change the Offset Data Type to   */
/* Long insteas of integer.                                                  */
/*===========================================================================*/
 
/*****************************************************************************/
/*= INSTRUMENT-DEPENDENT COMMAND ARRAYS =====================================*/
/*****************************************************************************/

/*****************************************************************************/
/*= INSTRUMENT-DEPENDENT STATUS/RANGE STRUCTURE  ============================*/
/*****************************************************************************/
/* hpe1428a_stringValPair is used in the hpe1428a_errorMessage function      */
/* hpe1428a_statusDataRanges is used to track session dependent status & ranges*/
/*===========================================================================*/
typedef struct  hpe1428a_stringValPair
{
   ViStatus stringVal;
   ViString stringName;
}  hpe1428a_tStringValPair;

/*===========================================================================*/
/* Change to reflect the global status variables that your driver needs      */
/* to keep track of.  For example trigger mode of each session               */
/*===========================================================================*/
struct hpe1428a_statusDataRanges {
    ViInt16 triggerMode;
    ViInt16 val2;
    ViInt16 val3;
    ViChar instrDriverRevision[256];
};

typedef struct hpe1428a_statusDataRanges *hpe1428a_instrRange;
/*****************************************************************************/
/* Memory Declarations                                                       */
/*****************************************************************************/
unsigned long Channel1;
unsigned long Channel2;
unsigned long MathSpace;
unsigned int MemoryMapped = 0;
unsigned int WavePoints;
long instptr;
int Chanl;
int TotalChannels;
int AquireType;
float YOff;
float YInc;
float XOff;
float XInc;

/*****************************************************************************/
/*= UTILITY ROUTINE DECLARATIONS (Non-Exportable Functions) =================*/
/*****************************************************************************/
ViBoolean hpe1428a_invalidViBooleanRange (ViBoolean val);
ViBoolean hpe1428a_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max);
ViBoolean hpe1428a_invalidViInt32Range (ViInt32 val, ViInt32 min, ViInt32 max);
ViBoolean hpe1428a_invalidViUInt16Range (ViUInt16 val, ViUInt16 min, ViUInt16 max);
ViBoolean hpe1428a_invalidViUInt32Range (ViUInt32 val, ViUInt32 min, ViUInt32 max);
ViBoolean hpe1428a_invalidViReal32Range (ViReal32 val, ViReal32 min, ViReal32 max);
ViBoolean hpe1428a_invalidViReal64Range (ViReal64 val, ViReal64 min, ViReal64 max);
ViStatus hpe1428a_initCleanUp (ViSession openRMSession, ViPSession openInstrSession, ViStatus currentStatus);
ViStatus hpe1428a_readToFile (ViSession instrSession, ViString filename, ViUInt32 readBytes, ViPUInt32 retCount);
ViStatus hpe1428a_writeFromFile (ViSession instrSession, ViString filename, ViUInt32 writeBytes, ViPUInt32 retCount);
ViStatus hpe1428a_defaultInstrSetup (ViSession openInstrSession);

/*****************************************************************************/
/*====== USER-CALLABLE FUNCTIONS (Exportable Functions) =====================*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Initialize                                                      */
/* Purpose:  This function opens the instrument, queries the instrument      */
/*           for its ID, and initializes the instrument to a known state.    */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_init (ViRsrc resourceName, ViBoolean IDQuery,
                    ViBoolean resetDev, ViPSession instrSession)
{
    ViStatus hpe1428a_status = VI_SUCCESS;
    ViSession rmSession = 0;
    ViUInt16 manfID = 0, modelCode = 0;    

    /*- Check input parameter ranges ----------------------------------------*/
    if (hpe1428a_invalidViBooleanRange (IDQuery))
        return VI_ERROR_PARAMETER2;
    if (hpe1428a_invalidViBooleanRange (resetDev))
        return VI_ERROR_PARAMETER3;

    /*- Open instrument session ---------------------------------------------*/
    if ((hpe1428a_status = viOpenDefaultRM (&rmSession)) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viOpen (rmSession, resourceName, VI_NULL, VI_NULL, instrSession)) < 0) {
        viClose (rmSession);
        return hpe1428a_status;
    }

    /*- Configure VISA Formatted I/O ----------------------------------------*/
    if ((hpe1428a_status = viSetAttribute (*instrSession, VI_ATTR_TMO_VALUE, 10000)) < 0)
            return hpe1428a_initCleanUp (rmSession, instrSession, hpe1428a_status);
    if ((hpe1428a_status = viSetBuf (*instrSession, VI_READ_BUF|VI_WRITE_BUF, 4000)) < 0)
            return hpe1428a_initCleanUp (rmSession, instrSession, hpe1428a_status);
    if ((hpe1428a_status = viSetAttribute (*instrSession, VI_ATTR_WR_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS)) < 0)
            return hpe1428a_initCleanUp (rmSession, instrSession, hpe1428a_status);
    if ((hpe1428a_status = viSetAttribute (*instrSession, VI_ATTR_RD_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS)) < 0)
            return hpe1428a_initCleanUp (rmSession, instrSession, hpe1428a_status);

    /*- Identification Query ------------------------------------------------*/
    if (IDQuery) {
        if ((hpe1428a_status = viGetAttribute (*instrSession, VI_ATTR_MANF_ID, &manfID)) < 0)
            return hpe1428a_initCleanUp (rmSession, instrSession, hpe1428a_status);
        if ((hpe1428a_status = viGetAttribute (*instrSession, VI_ATTR_MODEL_CODE, &modelCode)) < 0)
            return hpe1428a_initCleanUp (rmSession, instrSession, hpe1428a_status);
        
        if (manfID != hpe1428a_MANF_ID && modelCode != hpe1428a_MODEL_CODE)
            return hpe1428a_initCleanUp (rmSession, instrSession, VI_ERROR_FAIL_ID_QUERY);
    }

    /*- Reset instrument ----------------------------------------------------*/
    if (resetDev) {
        if ((hpe1428a_status = hpe1428a_reset (*instrSession)) < 0)
            return hpe1428a_initCleanUp (rmSession, instrSession, hpe1428a_status);
    }       
    else  /*- Send Default Instrument Setup ---------------------------------*/
        if ((hpe1428a_status = hpe1428a_defaultInstrSetup (*instrSession)) < 0)
            return hpe1428a_initCleanUp (rmSession, instrSession, hpe1428a_status);

    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: hpe1428a_WvData                                                 */
/* Purpose:  This function passes waveform memory data to this PnP driver    */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_WvData (ViSession instrSession, ViReal32 YIncrement, ViReal32 YOffset, 
	ViReal32 XIncrement, ViReal32 XOffset, ViUInt16 WchArea, ViUInt16 TotChan, ViUInt16 AquType)
{
	instptr = instrSession;
	Chanl = WchArea;
	YOff = YOffset;
	YInc = YIncrement;
	XOff = XOffset;
	XInc = XIncrement;
	TotalChannels = TotChan;
	AquireType = AquType;
	return WchArea;
}

/*===========================================================================*/
/* Function: hpe1428a_GtWv                                                   */
/* Purpose:  This function passes waveform memory data from this PnP driver  */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_GtWv (ViPReal32 WaveArray, ViPReal32 TimeArray)
{
	ViStatus ErrCode;
	ViUInt16 DataPointer[16002];
	ViUInt16 Count;
	ViUInt32 MemoryOffset = 0L;
	ViUInt16 TempCalc1;
	ViUInt16 TempCalc2;
	
	if (Chanl == 0) MemoryOffset = Channel1;
	else
		if (Chanl == 1) MemoryOffset = Channel2;
	else
		if (Chanl == 2) MemoryOffset = MathSpace;
	else
		return -1;
	
	if (AquireType == 0)
	{
		ErrCode = viMoveIn16 (instptr, VI_A24_SPACE, MemoryOffset, WavePoints, DataPointer);
		if (*DataPointer == 65535)
		{
			for (Count = 0; Count < WavePoints; Count ++)
			{
				*(WaveArray + (Count * TotalChannels)) = 0;
			 	*(TimeArray + Count) = XOff + (XInc * Count);
			}
		}
		else
		{
			for (Count = 0; Count < WavePoints; Count ++)
			{
				*(WaveArray + (Count * TotalChannels)) = YOff + (DataPointer[Count] * YInc);
			 	*(TimeArray + Count) = XOff + (XInc * Count);
			}
		}
	}
	else
	{
		ErrCode = viMoveIn16 (instptr, VI_A24_SPACE, MemoryOffset, WavePoints * 2, DataPointer);
		for (Count = 0; Count < WavePoints; Count ++)
		{
			TempCalc1 = *(DataPointer + (Count * 2)) * 256;
			TempCalc2 = *(DataPointer + (Count * 2) + 1) / 256;
			*(WaveArray + (Count * TotalChannels)) = ((TempCalc1 + TempCalc2) * YInc) + YOff;
			*(TimeArray + Count) = XOff + (XInc * Count);
		}
	}
	return 0;
}

/*===========================================================================*/
/* Function: Initiate                                                        */
/* Purpose:  This function places the instrument in a wait for trigger state.*/
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_initiate (ViSession instrSession)
{
    ViStatus hpe1428a_status = VI_SUCCESS;

    if ((hpe1428a_status = viPrintf (instrSession, "%s", "INIT")) < 0)
        return hpe1428a_status;
        
    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Abort                                                           */
/* Purpose:  Removes multimeter from wait-for-trigger state.                 */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_abort (ViSession instrSession)
{
    ViStatus hpe1428a_status = VI_SUCCESS;

    if ((hpe1428a_status = viPrintf (instrSession, "%s", "ABOR")) < 0)
        return hpe1428a_status;
        
    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Read Measurement                                                */
/* Purpose:  This function immediately triggers the instrument.              */
/*===========================================================================*/ 
ViStatus _VI_FUNC hpe1428a_readMeas (ViSession instrSession,       
                        ViPReal64 values)                 
{                                                                   
    ViStatus hpe1428a_status = VI_SUCCESS;                          
    ViPBuf   rdBuf = '\0';               
    ViUInt32 retCnt;
    ViUInt16 statusByte = 0;
    ViInt16  i = 0, j = 0, n = 1;

    if ((hpe1428a_status = viPrintf (instrSession, "%s", "*SRE 16;FETC?")) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = hpe1428a_waitForRqs (instrSession, 30000, &statusByte)) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viPrintf (instrSession, "%s", "*SRE 0")) < 0)
        return hpe1428a_status;
    
    while (rdBuf[i] != '\0') {
        if (rdBuf[i] == ',') {  
            i++;
            n++;
        }
        else {
            rdBuf[j] = rdBuf[i];
            i++;
            j++;
        }
    }
    
    if (Scan (rdBuf, "%s>%*f[x]", n, values) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
    
    return hpe1428a_status;  
}

/*===========================================================================*/
/* Function: Read Min/Max/Avg/Count                                          */
/* Purpose: This function returns the stored values of the min/max operation.*/
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_readMinMax (ViSession instrSession,
                        ViPReal64 min, ViPReal64 max, ViPReal64 avg, 
                        ViPReal64 cnt)
{
    ViStatus hpe1428a_status = VI_SUCCESS;
    ViPBuf   rdBuf = '\0';
    ViUInt32 retCnt;
     
    if ((hpe1428a_status = viPrintf (instrSession, "%s", ":CALC:AVER:MIN?;")) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1428a_status;
    if (Scan (rdBuf, "%s>%f", min) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
        
    if ((hpe1428a_status = viPrintf (instrSession, "%s", ":CALC:AVER:MAX?;")) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1428a_status;
    if (Scan (rdBuf, "%s>%f", max) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
    
    if ((hpe1428a_status = viPrintf (instrSession, "%s", ":CALC:AVER:AVER?;")) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1428a_status;
    if (Scan (rdBuf, "%s>%f", avg) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
    
    if ((hpe1428a_status = viPrintf (instrSession, "%s", ":CALC:AVER:COUN?;")) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1428a_status;
    if (Scan (rdBuf, "%s>%f", avg) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
        
    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Query Data Points                                               */
/* Purpose:  This function returns the number of data points stored.         */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_querDataPts (ViSession instrSession, ViInt16 *num)
{
    ViStatus hpe1428a_status = VI_SUCCESS;
    ViPBuf   rdBuf = '\0';
    ViUInt32 retCnt;

    if ((hpe1428a_status = viPrintf (instrSession, "%s", "DATA:POIN?")) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1428a_status;
    if (Scan (rdBuf, "%s>%d[b2]", num) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
    
    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Write To Instrument                                             */
/* Purpose:  This function writes a command string to the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_writeInstrData (ViSession instrSession, 
                        ViString writeBuffer)
{
    ViStatus hpe1428a_status = VI_SUCCESS;
    
    if ((hpe1428a_status = viPrintf (instrSession, "%s", writeBuffer)) < 0)
        return hpe1428a_status;

    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Read Instrument Buffer                                          */
/* Purpose:  This function reads the output buffer of the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_readInstrData (ViSession instrSession, 
                        ViInt16 numBytes, ViPBuf rdBuf, ViInt32 *bytesRead)
 {
    ViInt32		numRead = 0L;

    ViStatus hpe1428a_status = VI_SUCCESS;
    *bytesRead = 0L;
        
    if ((hpe1428a_status = viRead (instrSession, rdBuf, numBytes, &numRead)) < 0)
        return hpe1428a_status;
    *bytesRead = numRead;

    return hpe1428a_status;
}
/*===========================================================================*/
/* Function: Read Instrument Buffer                                          */
/* Purpose:  This function reads the output buffer of the instrument.        */
/*===========================================================================*/
/*ViStatus _VI_FUNC hpe1428a_readInstrData (ViSession instrSession, 
                        ViInt16 numBytes, ViPBuf rdBuf, ViPUInt32 bytesRead)
 {
    ViStatus hpe1428a_status = VI_SUCCESS;
        
    if ((hpe1428a_status = viRead (instrSession, rdBuf, numBytes, bytesRead)) < 0)
        return hpe1428a_status;

    return hpe1428a_status;
}
*/
/*===========================================================================*/
/* Function: Reset                                                           */
/* Purpose:  This function resets the instrument.                            */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_reset (ViSession instrSession)
{
    ViStatus hpe1428a_status = VI_SUCCESS;

    /*  Initialize the instrument to a known state.  */
    if ((hpe1428a_status = viPrintf (instrSession, "%s", "*RST")) < 0)
        return hpe1428a_status;

    if ((hpe1428a_status = hpe1428a_defaultInstrSetup (instrSession)) < 0)  
        return hpe1428a_status;
        
    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Self-Test                                                       */
/* Purpose:  This function executes the instrument self-test and returns     */
/*           the result.                                                     */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_selfTest (ViSession instrSession, 
                        ViPInt16 testResult, ViChar testMessage[])
{
    ViStatus hpe1428a_status = VI_SUCCESS;

    if ((hpe1428a_status = viPrintf (instrSession, "%s", "*TST?")) < 0)
        return hpe1428a_status;

    if ((hpe1428a_status = viScanf (instrSession, "%hd", testResult)) < 0)
        return hpe1428a_status;
        
    if (*testResult == 0)
        Fmt (testMessage, "%s<Self-Test Passed");
    else
        Fmt (testMessage, "%s<See Manual for Self-Test Code Description");

    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Error Query                                                     */
/* Purpose:  This function queries the instrument error queue, and returns   */
/*           the result.                                                     */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_errorQuery (ViSession instrSession, 
                        ViPInt32 errCode, ViChar errMessage[])
{
    ViStatus hpe1428a_status = VI_SUCCESS;

    if ((hpe1428a_status = viPrintf (instrSession, "%s", "SYST:ERR? STR")) < 0)
        return hpe1428a_status;

    if ((hpe1428a_status = viScanf (instrSession, "%ld,%[^\"]", errCode, errMessage)) < 0)
        return hpe1428a_status;
    
    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Error Message                                                   */
/* Purpose:  This function translates the error return value from the        */
/*           instrument driver into a user-readable string.                  */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_errorMessage (ViSession instrSession, 
                        ViStatus errorCode, ViChar errMessage[])
{
    ViStatus hpe1428a_status = VI_SUCCESS;
    ViInt16 i;
    static hpe1428a_tStringValPair statusDescArray[] = {
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
/*        need to be inserted in the include file "hpe1428a.h".  The codes     */
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

    hpe1428a_status = viStatusDesc (instrSession, errorCode, errMessage);
    if (hpe1428a_status == VI_WARN_UNKNOWN_STATUS) {
        for (i=0; statusDescArray[i].stringName; i++) {
            if (statusDescArray[i].stringVal == errorCode) {
                strcpy (errMessage, statusDescArray[i].stringName);
                return (VI_SUCCESS);
            }
        }
        sprintf (errMessage, "Unknown Error 0x%08lX", errorCode);
        return (VI_WARN_UNKNOWN_STATUS);
    }
    
    hpe1428a_status = VI_SUCCESS;
    return hpe1428a_status;
}
 
/*===========================================================================*/
/* Function: Revision Query                                                  */
/* Purpose:  This function returns the driver and instrument revisions.      */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_revisionQuery (ViSession instrSession,
                        ViChar driverRev[], ViChar instrRev[])
{
    ViStatus hpe1428a_status = VI_SUCCESS;

    if ((hpe1428a_status = viPrintf (instrSession, "%s", "*IDN?")) < 0)
        return hpe1428a_status;

    if ((hpe1428a_status = viScanf (instrSession, "%*[^,],%*[^,],%*[^,],%[^\n]", instrRev)) < 0)
        return hpe1428a_status;

    strcpy (driverRev, hpe1428a_REVISION);
    
    return hpe1428a_status;
}
 
/*===========================================================================*/
/* Function: Close                                                           */
/* Purpose:  This function closes the instrument.                            */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_close (ViSession instrSession)
{
    hpe1428a_instrRange instrPtr;
    ViSession rmSession;
    ViStatus hpe1428a_status = VI_SUCCESS;

    if ((hpe1428a_status = viGetAttribute (instrSession, VI_ATTR_RM_SESSION, &rmSession)) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viGetAttribute (instrSession, VI_ATTR_USER_DATA, &instrPtr)) < 0)
        return hpe1428a_status;
    
    /*- Disable Event SRQ ---------------------------------------------------*/
    if ((hpe1428a_status = viDisableEvent(instrSession, VI_EVENT_SERVICE_REQ, VI_QUEUE)) < 0) 
        return hpe1428a_status;
    
    free (instrPtr);
    
    hpe1428a_status = viClose (instrSession);
    viClose (rmSession);

    return hpe1428a_status;
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
ViBoolean hpe1428a_invalidViBooleanRange (ViBoolean val)
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
ViBoolean hpe1428a_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max)
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
ViBoolean hpe1428a_invalidViInt32Range  (ViInt32 val, ViInt32 min, ViInt32 max)
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
ViBoolean hpe1428a_invalidViUInt16Range  (ViUInt16 val, ViUInt16 min, ViUInt16 max)
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
ViBoolean hpe1428a_invalidViUInt32Range  (ViUInt32 val, ViUInt32 min, ViUInt32 max)
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
ViBoolean hpe1428a_invalidViReal32Range  (ViReal32 val, ViReal32 min, ViReal32 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Real (Double) Value Out Of Range - ViReal64                     */
/* Purpose:  This function checks a real (double) value to see if it lies    */  
/*           between a minimum and maximum value.  If the value is out of    */
/*           range, the return value is VI_TRUE, otherwise the return value  */
/*           is VI_FALSE.                                                    */
/*===========================================================================*/
ViBoolean hpe1428a_invalidViReal64Range  (ViReal64 val, ViReal64 min, ViReal64 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Initialize Clean Up                                             */
/* Purpose:  This function is used only by the hpe1428a_init function.  When   */
/*           an error is detected this function is called to close the       */
/*           open resource manager and instrument object sessions and to     */
/*           set the instrSession that is returned from hpe1428a_init to       */
/*           VI_NULL.                                                        */
/*===========================================================================*/
ViStatus hpe1428a_initCleanUp (ViSession openRMSession,
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
ViStatus hpe1428a_readToFile (ViSession instrSession, ViString filename,
                        ViUInt32 readBytes, ViPUInt32 retCount)
{
    ViStatus  hpe1428a_status = VI_SUCCESS;
    ViByte    buffer[BUFFER_SIZE];
    ViUInt32  bytesReadInstr = 0, bytesWrittenFile = 0;
    FILE     *targetFile;

    *retCount = 0L;
    if ((targetFile = fopen (filename, "wb")) == VI_NULL)
        return VI_ERROR_FILE_OPEN; /* not defined by VTL */

    for (;;) {
        if (readBytes > BUFFER_SIZE)
            hpe1428a_status = viRead (instrSession, buffer, BUFFER_SIZE, &bytesReadInstr);
        else
            hpe1428a_status = viRead (instrSession, buffer, readBytes, &bytesReadInstr);

        bytesWrittenFile = fwrite (buffer, sizeof (ViByte), (size_t)bytesReadInstr, targetFile);
        *retCount += bytesWrittenFile;
        if (bytesWrittenFile < bytesReadInstr)
            hpe1428a_status = VI_ERROR_FILE_WRITE; /* not defined by VTL */

        if ((readBytes <= BUFFER_SIZE) || (hpe1428a_status <= 0) || (hpe1428a_status == VI_SUCCESS_TERM_CHAR))
            break;

        readBytes -= BUFFER_SIZE;
    }

    fclose (targetFile);
    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Write From File To Instrument                                   */
/* Purpose:  This function is used to read data from a user specified file   */
/*           and write it to the instrument.                                 */
/*===========================================================================*/
ViStatus hpe1428a_writeFromFile (ViSession instrSession, ViString filename,
                        ViUInt32 writeBytes, ViPUInt32 retCount)
{
    ViStatus  hpe1428a_status = VI_SUCCESS;
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
            hpe1428a_status = viWrite (instrSession, buffer, BUFFER_SIZE, &bytesWritten);
            viSetAttribute (instrSession, VI_ATTR_SEND_END_EN, sendEnd);
            writeBytes -= BUFFER_SIZE;
            *retCount += bytesWritten;
            if (hpe1428a_status < 0)
                break;
        }
        else {
            hpe1428a_status = viWrite (instrSession, buffer, ((bytesRead < writeBytes) ? bytesRead : writeBytes), &bytesWritten);
            *retCount += bytesWritten;
            break;
        }
    }

    fclose (sourceFile);
    return hpe1428a_status;
}

ViStatus _VI_FUNC hpe1428a_FindInst (ViSession instrSession, ViUInt32 MemOffset1, 
											ViUInt32 MemOffset2, ViUInt32 MathOffset, 
											ViUInt16 MemoryPoints, ViPUInt32 VMEAddressIN)
{
	/* ViPAttrState VMEAddress;*/
	ViPAddr BaseAddress;
	ViStatus ErrCode;
	ViPUInt32 VMEAddress;
	ViUInt32 VMEspace;
	ViAddr AddressSpace;
	
	BaseAddress = &AddressSpace;
	VMEAddress = &VMEspace;
	
	WavePoints = MemoryPoints;
	ErrCode = viGetAttribute (instrSession, VI_ATTR_WIN_BASE_ADDR, VMEAddress);
	*VMEAddressIN = *VMEAddress;
	Channel1 = MemOffset1;
	Channel2 = MemOffset2;
	MathSpace = MathOffset;
	return ErrCode;
}

/*****************************************************************************/
/*----------- INSERT INSTRUMENT-DEPENDENT UTILITY ROUTINES HERE -------------*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Default Instrument Setup                                        */
/* Purpose:  This function sends a default setup to the instrument.  This    */
/*           function is called by the hpe1428a_reset operation and by the     */
/*           hpe1428a_init function if the reset option has not been           */
/*           selected.  This function is useful for configuring any          */
/*           instrument settings that are required by the rest of the        */
/*           instrument driver functions such as turning headers ON or OFF   */
/*           or using the long or short form for commands, queries, and data.*/                                    
/*===========================================================================*/
ViStatus hpe1428a_defaultInstrSetup (ViSession instrSession)
{
    ViStatus hpe1428a_status = VI_SUCCESS;
    
    hpe1428a_instrRange instrPtr;
    instrPtr = malloc (sizeof (struct hpe1428a_statusDataRanges));

/*===========================================================================*/
/* Change to reflect the global status variables that your driver needs      */
/* to keep track of.  For example trigger mode of each session               */
/*===========================================================================*/
    instrPtr -> triggerMode = 0;
    instrPtr -> val2 = 0;
    instrPtr -> val3 = 0;
    strcpy (instrPtr -> instrDriverRevision, hpe1428a_REVISION);
    
    if (viSetAttribute (instrSession, VI_ATTR_USER_DATA, (ViUInt32)instrPtr) != 0)
        return hpe1428a_status;

    /*- Enable Event SRQ ----------------------------------------------------*/
    if ((hpe1428a_status = viEnableEvent(instrSession, VI_EVENT_SERVICE_REQ, VI_QUEUE, VI_NULL)) < 0) 
        return hpe1428a_status;

    return hpe1428a_status;
}

/*===========================================================================*/
/* Function: Wait for RQS                                                    */
/* Purpose:  This function waits for a service request.                      */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1428a_waitForRqs (ViSession instrSession, 
                        ViInt32 timeout, ViPUInt16 statusByte)
{
    ViStatus hpe1428a_status = VI_SUCCESS;
    ViEventType eventType;
    ViEvent  dupHandle;
    
    if ((hpe1428a_status = viWaitOnEvent (instrSession, VI_EVENT_SERVICE_REQ, timeout, &eventType, &dupHandle)) < 0)
        return hpe1428a_status;
    if ((hpe1428a_status = viReadSTB (instrSession, statusByte)) < 0)
        return hpe1428a_status;
    hpe1428a_status = viClose (dupHandle);

    return hpe1428a_status;
}

ViStatus _VI_FUNC hpe1428a_msgWaiting (ViSession instrSession, ViPInt32 buffFull)
{
	  ViStatus hpe1428a_status = VI_SUCCESS;
	  ViUInt16 MsgRespReg;
	  
	  hpe1428a_status = viIn16(instrSession, VI_A16_SPACE, 10, &MsgRespReg);
      MsgRespReg = MsgRespReg & 0x2000;
      if (MsgRespReg == 0)
      	*buffFull = 0L;
      else
      	*buffFull = -1L;
	/* *buffFull = MsgRespReg; */
      return hpe1428a_status;
}

ViStatus _VI_FUNC hpe1428a_softReset (ViSession instrSession)
{
	ViStatus hpe1428a_status = VI_SUCCESS;
	
	hpe1428a_status = viOut16(instrSession, VI_A16_SPACE, 14, 0xFFFF);
	return hpe1428a_status;
}

	

ViStatus _VI_FUNC hpe1428a_ResetTM (ViSession instrSession)
{
	ViStatus hpe1428a_status = VI_SUCCESS;
	
	hpe1428a_status = viSetAttribute (instrSession, VI_ATTR_TMO_VALUE, 1);
	return hpe1428a_status;
}

/*****************************************************************************/
/*=== END INSTRUMENT DRIVER SOURCE CODE =====================================*/
/*****************************************************************************/
