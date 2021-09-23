/***************************************************************************/
/*  Copyright 1996 National Instruments Corporation.  All Rights Reserved. */
/***************************************************************************/

#include <ansi_c.h>
#include <formatio.h>
#include <utility.h>
#include <visa.h>
#include "hpe1445a.h"

/*= DEFINES =================================================================*/
//#define hpe1445a_REVISION     "Rev 1.0, 2/96, CVI 3.1" /*  Instrument driver revision */
#define hpe1445a_REVISION     "Rev 1.1, 8/98, CVI 4.0" /*  Instrument driver revision */
#define BUFFER_SIZE         512L                       /*  File I/O buffer size       */
#define hpe1445a_MANF_ID      0x0fff                   /*  Instrument manufacturer ID */
#define hpe1445a_MODEL_CODE   0x0243                   /*  Instrument model code      */

/*= HP E1445A Arbitrary Waveform Generator===================================*/    
/* LabWindows/CVI 3.1 Instrument Driver                                      */
/* Original Release:                                                         */
/* By: EJR, NI                                                               */
/*     PH. (800) 433-3488   Fax (512) 794-5678                               */
/*                                                                           */
/* Modification History:                                                     */
/*	08/03/98	J. Hill														 */
/*		Changed _writeInstrData from 'viPrintf' to 'viWrite'.  Otherwise, it */
/*		truncated at 512 bytes.												 */
/*===========================================================================*/
 
/*****************************************************************************/
/*= INSTRUMENT-DEPENDENT COMMAND ARRAYS =====================================*/
/*****************************************************************************/
/* hpe1445a_confAcCurr */
static ViReal64 acRes[2][2] = {1E-6, 3E-6, 100E-6, 300E-6 };
static ViChar *curAcRange[] = {":AUTO 1;", " 1;", " 3;"};                              
static ViInt16 bw[]         = {3, 20, 200};
/* hpe1445a_confDcCurr */
static ViChar *curRange[]   = {":AUTO 1;", " .01;", " .1;", " 1;", " 3;"};
static ViReal64 itime[]     = {0.333E-3, 3.33E-3, 16.7E-3, 167E-3, 1.67};
static ViChar *autoZero[]   = {"0;", "1;", "ONCE;"};
/* hpe1445a_confAcVolt */
static ViReal64 acVRes[2][5]= {100E-9, 1E-6, 10E-6, 100E-6, 1E-3, 10E-6, 100E-6, 1E-3, 10E-3, 100E-3};
static ViChar *volRange[]   = {":AUTO 1;", " .1;", " 1;", " 10;", " 100;", " 300;"};
/* hpe1445a_confDcVolt */
static ViChar *dcType[]     = {"", ":RATIO"};
/* hpe1445a_confFreqPer */
static ViChar *aTime[]      = {"10E-3;", "100E-3;", "1;"};
/* hpe1445a_confResist */
static ViChar *resRange[]   = {":AUTO 1;", " 100;", " 1E3;", " 10E3;", " 100E3;", 
                               " 1E6;", " 10E6;", " 100E6;"};
/* hpe1445a_confTrigger */
static ViChar *trigSour[]   = {"BUS:",  "EXT;", "IMM;", "TTLTRG0;", "TTLTRG1;",
                               "TTLTRG2;", "TTLTRG3;", "TTLTRG4;", "TTLTRG5;",
                               "TTLTRG6;", "TTLTRG7;"};
/* hpe1445a_confMath */
static ViInt16 dBmRef[]     = {50, 75, 93, 110, 124, 125, 135, 150, 250, 300, 500, 
                               600, 800, 900, 1000, 1200, 8000};
/* hpe1445a_confOut */
static ViChar *ttlLines[]   = {":OUTP:TTLT0 0;", ":OUTP:TTLT0 1;", ":OUTP:TTLT1 1;", 
                               ":OUTP:TTLT2 1;", ":OUTP:TTLT3 1;", ":OUTP:TTLT4 1;", 
                               ":OUTP:TTLT5 1;", ":OUTP:TTLT6 1;", ":OUTP:TTLT7 1;"};

/*****************************************************************************/
/*= INSTRUMENT-DEPENDENT STATUS/RANGE STRUCTURE  ============================*/
/*****************************************************************************/
/* hpe1445a_stringValPair is used in the hpe1445a_errorMessage function      */
/* hpe1445a_statusDataRanges is used to track session dependent status & ranges*/
/*===========================================================================*/
typedef struct  hpe1445a_stringValPair
{
   ViStatus stringVal;
   ViString stringName;
}  hpe1445a_tStringValPair;

/*===========================================================================*/
/* Change to reflect the global status variables that your driver needs      */
/* to keep track of.  For example trigger mode of each session               */
/*===========================================================================*/
struct hpe1445a_statusDataRanges {
    ViInt16 triggerMode;
    ViInt16 val2;
    ViInt16 val3;
    ViChar instrDriverRevision[256];
};

typedef struct hpe1445a_statusDataRanges *hpe1445a_instrRange;

/*****************************************************************************/
/*= UTILITY ROUTINE DECLARATIONS (Non-Exportable Functions) =================*/
/*****************************************************************************/
ViBoolean hpe1445a_invalidViBooleanRange (ViBoolean val);
ViBoolean hpe1445a_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max);
ViBoolean hpe1445a_invalidViInt32Range (ViInt32 val, ViInt32 min, ViInt32 max);
ViBoolean hpe1445a_invalidViUInt16Range (ViUInt16 val, ViUInt16 min, ViUInt16 max);
ViBoolean hpe1445a_invalidViUInt32Range (ViUInt32 val, ViUInt32 min, ViUInt32 max);
ViBoolean hpe1445a_invalidViReal32Range (ViReal32 val, ViReal32 min, ViReal32 max);
ViBoolean hpe1445a_invalidViReal64Range (ViReal64 val, ViReal64 min, ViReal64 max);
ViStatus hpe1445a_initCleanUp (ViSession openRMSession, ViPSession openInstrSession, ViStatus currentStatus);
ViStatus hpe1445a_readToFile (ViSession instrSession, ViString filename, ViUInt32 readBytes, ViPUInt32 retCount);
ViStatus hpe1445a_writeFromFile (ViSession instrSession, ViString filename, ViUInt32 writeBytes, ViPUInt32 retCount);
ViStatus hpe1445a_defaultInstrSetup (ViSession openInstrSession);

/*****************************************************************************/
/*====== USER-CALLABLE FUNCTIONS (Exportable Functions) =====================*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Initialize                                                      */
/* Purpose:  This function opens the instrument, queries the instrument      */
/*           for its ID, and initializes the instrument to a known state.    */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_init (ViRsrc resourceName, ViBoolean IDQuery,
                    ViBoolean resetDev, ViPSession instrSession)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViSession rmSession = 0;
    ViUInt32 retCnt = 0;
    ViUInt16 manfID = 0, modelCode = 0;    

    /*- Check input parameter ranges ----------------------------------------*/
    if (hpe1445a_invalidViBooleanRange (IDQuery))
        return VI_ERROR_PARAMETER2;
    if (hpe1445a_invalidViBooleanRange (resetDev))
        return VI_ERROR_PARAMETER3;

    /*- Open instrument session ---------------------------------------------*/
    if ((hpe1445a_status = viOpenDefaultRM (&rmSession)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viOpen (rmSession, resourceName, VI_NULL, VI_NULL, instrSession)) < 0) {
        viClose (rmSession);
        return hpe1445a_status;
    }

    /*- Configure VISA Formatted I/O ----------------------------------------*/
    if ((hpe1445a_status = viSetAttribute (*instrSession, VI_ATTR_TMO_VALUE, 10000)) < 0)
            return hpe1445a_initCleanUp (rmSession, instrSession, hpe1445a_status);
    if ((hpe1445a_status = viSetBuf (*instrSession, VI_READ_BUF|VI_WRITE_BUF, 4000)) < 0)
            return hpe1445a_initCleanUp (rmSession, instrSession, hpe1445a_status);
    if ((hpe1445a_status = viSetAttribute (*instrSession, VI_ATTR_WR_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS)) < 0)
            return hpe1445a_initCleanUp (rmSession, instrSession, hpe1445a_status);
    if ((hpe1445a_status = viSetAttribute (*instrSession, VI_ATTR_RD_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS)) < 0)
            return hpe1445a_initCleanUp (rmSession, instrSession, hpe1445a_status);

    /*- Identification Query ------------------------------------------------*/
    if (IDQuery) {
        if ((hpe1445a_status = viGetAttribute (*instrSession, VI_ATTR_MANF_ID, &manfID)) < 0)
            return hpe1445a_initCleanUp (rmSession, instrSession, hpe1445a_status);
        if ((hpe1445a_status = viGetAttribute (*instrSession, VI_ATTR_MODEL_CODE, &modelCode)) < 0)
            return hpe1445a_initCleanUp (rmSession, instrSession, hpe1445a_status);
        
        if (manfID != hpe1445a_MANF_ID && modelCode != hpe1445a_MODEL_CODE)
            return hpe1445a_initCleanUp (rmSession, instrSession, VI_ERROR_FAIL_ID_QUERY);
    }

    /*- Reset instrument ----------------------------------------------------*/
    if (resetDev) {
        if ((hpe1445a_status = hpe1445a_reset (*instrSession)) < 0)
            return hpe1445a_initCleanUp (rmSession, instrSession, hpe1445a_status);
    }       
    else  /*- Send Default Instrument Setup ---------------------------------*/
        if ((hpe1445a_status = hpe1445a_defaultInstrSetup (*instrSession)) < 0)
            return hpe1445a_initCleanUp (rmSession, instrSession, hpe1445a_status);

    return hpe1445a_status;
}


/*===========================================================================*/
/* Function: Configure AC Current                                            */
/* Purpose:  This function configures the instrument to take a specified     */
/*           AC current measurement.                                         */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confAcCurr (ViSession instrSession, 
                        ViInt16 range, ViReal64 res, ViInt16 filter)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
    
    if (hpe1445a_invalidViInt16Range (range, 0, 2))
        return VI_ERROR_PARAMETER2;
    if (range)    
      if (hpe1445a_invalidViReal64Range (res, acRes[0][range-1],acRes[1][range-1]))
          return VI_ERROR_PARAMETER3;
    if (hpe1445a_invalidViInt16Range (filter, 0, 2))      
        return VI_ERROR_PARAMETER4;

    if (!range) 
        Fmt (wrtBuf, "%s<CONF:CURR:AC;:CURR:AC:RANG%s:DET:BAND %d[b2];", 
                      curAcRange[range], bw[filter]);
    else
        Fmt (wrtBuf, "%s<CONF:CURR:AC;:CURR:AC:RANG%sRES %f;:DET:BAND %d[b2];", 
                      curAcRange[range], res, bw[filter]);
                      
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Configure DC Current                                            */
/* Purpose:  This function configures the instrument to take a specified     */
/*           DC current measurement.                                         */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confDcCurr (ViSession instrSession, 
                        ViInt16 range, ViInt16 time, ViReal64 res, ViInt16 zero)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
    
    if (hpe1445a_invalidViInt16Range (range, 0, 4))
        return VI_ERROR_PARAMETER2;
    if (hpe1445a_invalidViInt16Range (time, 0, 4))      
        return VI_ERROR_PARAMETER3;
    if (range)
      if (hpe1445a_invalidViReal64Range (res, 1E-9, 1.0))
          return VI_ERROR_PARAMETER4;
    if (hpe1445a_invalidViInt16Range (zero, 0, 2))      
        return VI_ERROR_PARAMETER5;

    if ((range == 0) || (res < 0))
        Fmt (wrtBuf, "%s<CONF:CURR;:CURR:RANG%s:CURR:APER %f;:ZERO:AUTO %s", 
                      curRange[range], itime[time], autoZero[zero]);
    else
        Fmt (wrtBuf, "%s<CONF:CURR;:CURR:RANG%sAPER %f;RES %f;:ZERO:AUTO %s", 
                      curRange[range], itime[time], res, autoZero[zero]);
                      
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Configure AC Voltage                                            */
/* Purpose:  This function configures the instrument to take a specified     */
/*           AC voltage measurement.                                         */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confAcVolt (ViSession instrSession,
                        ViInt16 range, ViInt16 filter, ViReal64 res)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
    
    if (hpe1445a_invalidViInt16Range (range, 0, 5))
        return VI_ERROR_PARAMETER2;
    if (range)
      if (hpe1445a_invalidViReal64Range (res, acVRes[0][range-1], acVRes[1][range-1]))
          return VI_ERROR_PARAMETER3;
    if (hpe1445a_invalidViInt16Range (filter, 0, 2))      
        return VI_ERROR_PARAMETER4;

    if (range == 0)
        Fmt (wrtBuf, "%s<CONF:VOLT:AC;:VOLT:AC:RANG%s:DET:BAND %d[b2];", 
                      volRange[range], bw[filter]);
    else
        Fmt (wrtBuf, "%s<CONF:CURR:AC;:CURR:AC:RANG%sRES %f;:DET:BAND %d[b2]", 
                      volRange[range], res, bw[filter]);
                      
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Configure DC Voltage                                            */
/* Purpose:  This function configures the instrument to take a specified     */
/*           DC voltage measurement.                                         */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confDcVolt (ViSession instrSession,
                        ViInt16 range, ViInt16 zero, ViInt16 time,  
                        ViReal64 res, ViBoolean type, ViBoolean inpZ)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
    
    if (hpe1445a_invalidViInt16Range (range, 0, 4))
        return VI_ERROR_PARAMETER2;
    if (hpe1445a_invalidViInt16Range (zero, 0, 2))      
        return VI_ERROR_PARAMETER3;
    if (hpe1445a_invalidViInt16Range (time, 0, 4))      
        return VI_ERROR_PARAMETER4;
    if (hpe1445a_invalidViReal64Range (res, -1.0, 100.0))
        return VI_ERROR_PARAMETER5;
    if (hpe1445a_invalidViBooleanRange (type))
        return VI_ERROR_PARAMETER6;
    if (hpe1445a_invalidViBooleanRange (inpZ))
        return VI_ERROR_PARAMETER7;

    if ((range == 0) || (res < 0))
        Fmt (wrtBuf, "%s<CONF:VOLT%s;:VOLT:RANG%s:VOLT:APER %f;:ZERO:AUTO %s:INP:IMP:AUTO %d[b2];", 
                      dcType[type], volRange[range], itime[time], autoZero[zero], inpZ);
    else
        Fmt (wrtBuf, "%s<CONF:VOLT%s;:VOLT:RANG%s:VOLT:APER %f;RES %f;:ZERO:AUTO %s:INP:IMP:AUTO %d[b2];", 
                      dcType[type], volRange[range], itime[time], res, autoZero[zero], inpZ);
                      
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Configure Frequency/Period                                      */
/* Purpose:  This function configures the instrument to take a specified     */
/*           frequency or period measurement.                                */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confFreqPer (ViSession instrSession, 
                        ViInt16 range, ViInt16 time, ViBoolean freqMode)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
    
    if (hpe1445a_invalidViInt16Range (range, 0, 4))
        return VI_ERROR_PARAMETER2;
    if (hpe1445a_invalidViInt16Range (time, 0, 2))      
        return VI_ERROR_PARAMETER3;
    if (hpe1445a_invalidViBooleanRange (freqMode))
        return VI_ERROR_PARAMETER4;

    if (freqMode)
        Fmt (wrtBuf, "%s<CONF:FREQ;:FREQ:VOLT:RANG%s:FREQ:APER %s", 
                      volRange[range], aTime[time]);
    else
        Fmt (wrtBuf, "%s<CONF:PER;:PER:VOLT:RANG%s:PER:APER %s", 
                      volRange[range], aTime[time]);
                      
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Configure Resistance                                            */
/* Purpose:  This function configures the instrument to take a specified     */
/*           resistance measurement.                                         */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confResist (ViSession instrSession, 
                        ViInt16 range, ViInt16 time, ViReal64 res, 
                        ViInt16 zero, ViBoolean type)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
    
    if (hpe1445a_invalidViInt16Range (range, 0, 7))
        return VI_ERROR_PARAMETER2;
    if (hpe1445a_invalidViInt16Range (time, 0, 4))      
        return VI_ERROR_PARAMETER3;
    if (hpe1445a_invalidViReal64Range (res, -1.0, 1E6))
        return VI_ERROR_PARAMETER4;
    if (hpe1445a_invalidViInt16Range (zero, 0, 2))      
        return VI_ERROR_PARAMETER5;
    if (hpe1445a_invalidViBooleanRange (type))
        return VI_ERROR_PARAMETER6;

    if (type) {
        if ((range == 0) || (res < 0))
            Fmt (wrtBuf, "%s<CONF:RES;:RES:RANG%s:RES:APER %f;:ZERO:AUTO %s", 
                          resRange[range], itime[time], autoZero[zero]);
        else
            Fmt (wrtBuf, "%s<CONF:RES;:RES:RANG%s:RES:APER %f;RES %f;:ZERO:AUTO %s", 
                          resRange[range], itime[time], res, autoZero[zero]);
    }
    else {
        if ((range == 0) || (res < 0))
            Fmt (wrtBuf, "%s<CONF:FRES;:FRES:RANG%s:FRES:APER %f;:ZERO:AUTO %s", 
                          resRange[range], itime[time], autoZero[zero]);
        else
            Fmt (wrtBuf, "%s<CONF:FRES;:FRES:RANG%s:FRES:APER %f;RES %f;:ZERO:AUTO %s", 
                          resRange[range], itime[time], res, autoZero[zero]);

    }
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Configure Trigger                                               */
/* Purpose:  This function configures the trigger functions of the           */
/*           instrument.                                                     */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confTrigger (ViSession instrSession,
                        ViInt16 triggerMode, ViReal64 triggerDelays,
                        ViBoolean delayMode, ViInt32 sample, ViInt32 cnt)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;

    if (hpe1445a_invalidViInt16Range (triggerMode, 0, 10))
        return VI_ERROR_PARAMETER2;
    if (hpe1445a_invalidViReal64Range (triggerDelays, 0.0, 3600.0))
        return VI_ERROR_PARAMETER3;
    if (hpe1445a_invalidViBooleanRange (delayMode))
        return VI_ERROR_PARAMETER4;
    if (hpe1445a_invalidViInt32Range (sample, 1, 50000))
        return VI_ERROR_PARAMETER5;
    if (hpe1445a_invalidViInt32Range (cnt, 1, 50000))
        return VI_ERROR_PARAMETER6;

    if (delayMode)
        Fmt (wrtBuf, ":TRIG:SOUR %s:TRIG:DEL %f;:TRIG:COUN %d[b4];:SAMP:COUN %d[b4];",
                      trigSour[triggerMode], triggerDelays, sample, cnt);
    else
        Fmt (wrtBuf, ":TRIG:SOUR %s:TRIG:DEL:AUTO ON;:TRIG:COUN %d[b4];:SAMP:COUN %d[b4];",
                      trigSour[triggerMode], sample, cnt);
    
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
        
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function:  Configure Math                                                 */
/* Purpose: This function configures the math mode of the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confMath (ViSession instrSession, 
                        ViInt16 oper, ViReal64 offset, ViInt16 dBm,
                        ViReal64 llimit, ViReal64 ulimit)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;

    if (hpe1445a_invalidViInt16Range (oper, 0, 5))
        return VI_ERROR_PARAMETER2;
    if (oper == 2) 
        if (hpe1445a_invalidViReal64Range (offset, -120.0, 120.0))
            return VI_ERROR_PARAMETER3;
    if (oper == 3)
        if (hpe1445a_invalidViReal64Range (offset, -200.0, 200.0))
            return VI_ERROR_PARAMETER3;
    if (hpe1445a_invalidViInt32Range (dBm, 0, 16))
        return VI_ERROR_PARAMETER4;
    if (hpe1445a_invalidViReal64Range (llimit, -120.0, 120.0))
        return VI_ERROR_PARAMETER5;
    if (hpe1445a_invalidViReal64Range (ulimit, -120.0, 120.0))
        return VI_ERROR_PARAMETER6;

    switch (oper) {
        case 0:  Fmt (wrtBuf, ":CALC:STAT OFF;");
                 break;
        case 1:  Fmt (wrtBuf, ":CALC:FUNC AVER;:CALC:STAT ON;");
                 break;
        case 2:  Fmt (wrtBuf, ":CALC:FUNC NULL;:CALC:STAT ON;:CALC:NULL:OFFS %f;",offset);
                 break;
        case 3:  Fmt (wrtBuf, ":CALC:FUNC DB;:CALC:STAT ON;:CALC:DB:REF %f;",offset);
                 break;
        case 4:  Fmt (wrtBuf, ":CALC:FUNC DBM;:CALC:STAT ON;:CALC:DBM:REF %d[b2];",dBmRef[dBm]);
                 break;
        case 5:  Fmt (wrtBuf, ":CALC:FUNC LIM;:CALC:STAT ON;:CALC:LIM:LOW %f;:CALC:LIM:UPP %f;",
                      llimit, ulimit);
                 break;
    }
    
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
        
    return hpe1445a_status;
}    

/*===========================================================================*/
/* Function: Configure Output                                                */
/* Purpose:  This function enables routing of voltmeter complete signals     */
/* to VXIbus TTL trigger lines.                                              */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_confOut (ViSession instrSession, ViInt16 line)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
    
    if (hpe1445a_invalidViInt16Range (line, 0, 8))
        return VI_ERROR_PARAMETER2;
        
    Fmt (wrtBuf, "%s<%s;", ttlLines[line]);
    if ((hpe1445a_status = viWrite(instrSession, wrtBuf, NumFmtdBytes(), &retCnt)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}   

/*===========================================================================*/
/* Function: Trigger Measurement                                             */
/* Purpose:  This function immediately triggers the instrument.              */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_trigger (ViSession instrSession)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;

    if ((hpe1445a_status = viWrite(instrSession, "*TRG;", 5, &retCnt)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Initiate                                                        */
/* Purpose:  This function places the instrument in a wait for trigger state.*/
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_initiate (ViSession instrSession)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;

    if ((hpe1445a_status = viWrite(instrSession, "INIT", 4, &retCnt)) < 0)
        return hpe1445a_status;
        
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Abort                                                           */
/* Purpose:  Removes multimeter from wait-for-trigger state.                 */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_abort (ViSession instrSession)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE];
    ViUInt32 retCnt;

    if ((hpe1445a_status = viWrite(instrSession, "ABOR", 4, &retCnt)) < 0)
        return hpe1445a_status;
        
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Read Measurement                                                */
/* Purpose:  This function immediately triggers the instrument.              */
/*===========================================================================*/ 
ViStatus _VI_FUNC hpe1445a_readMeas (ViSession instrSession,       
                        ViPReal64 values)                 
{                                                                   
    ViStatus hpe1445a_status = VI_SUCCESS;                          
    ViChar   wrtBuf[BUFFER_SIZE], rdBuf[BUFFER_SIZE];               
    ViUInt32 retCnt;
    ViInt16  *statusByte, i = 0, j = 0, n = 1;

    if ((hpe1445a_status = viWrite (instrSession, "*SRE 16;FETC?", 13, &retCnt)) < 0)           
        return hpe1445a_status;
    if ((hpe1445a_status = hpe1445a_waitForRqs (instrSession, 30000, statusByte)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viWrite (instrSession, "*SRE 0", 6, &retCnt)) < 0)
        return hpe1445a_status;
    
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
    
    return hpe1445a_status;  
}

/*===========================================================================*/
/* Function: Read Min/Max/Avg/Count                                          */
/* Purpose: This function returns the stored values of the min/max operation.*/
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_readMinMax (ViSession instrSession,
                        ViPReal64 min, ViPReal64 max, ViPReal64 avg, 
                        ViPReal64 cnt)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE], rdBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
     
    if ((hpe1445a_status = viWrite (instrSession, ":CALC:AVER:MIN?;", 16, &retCnt)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1445a_status;
    if (Scan (rdBuf, "%s>%f", min) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
        
    if ((hpe1445a_status = viWrite (instrSession, ":CALC:AVER:MAX?;", 16, &retCnt)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1445a_status;
    if (Scan (rdBuf, "%s>%f", max) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
    
    if ((hpe1445a_status = viWrite (instrSession, ":CALC:AVER:AVER?;", 17, &retCnt)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1445a_status;
    if (Scan (rdBuf, "%s>%f", avg) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
    
    if ((hpe1445a_status = viWrite (instrSession, ":CALC:AVER:COUN?;", 17, &retCnt)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1445a_status;
    if (Scan (rdBuf, "%s>%f", avg) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
        
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Query Data Points                                               */
/* Purpose:  This function returns the number of data points stored.         */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_querDataPts (ViSession instrSession, ViInt16 *num)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE], rdBuf[BUFFER_SIZE];
    ViUInt32 retCnt;

    if ((hpe1445a_status = viWrite (instrSession, "DATA:POIN?", 10, &retCnt)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viRead (instrSession, rdBuf, BUFFER_SIZE, &retCnt)) < 0)
        return hpe1445a_status;
    if (Scan (rdBuf, "%s>%d[b2]", num) != 1)
        return VI_ERROR_INTERPRETING_RESPONSE;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Write To Instrument                                             */
/* Purpose:  This function writes a command string to the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_writeInstrData (ViSession instrSession, 
                        ViString writeBuffer)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViPUInt32 retCnt;
    
/*    if ((hpe1445a_status = viPrintf (instrSession, "%s", writeBuffer)) < 0)
        return hpe1445a_status;*/

    if ((hpe1445a_status = viWrite (instrSession, writeBuffer, strlen(writeBuffer), retCnt)) < 0)
        return hpe1445a_status;

    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Read Instrument Buffer                                          */
/* Purpose:  This function reads the output buffer of the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_readInstrData (ViSession instrSession, 
                        ViInt16 numBytes, ViChar rdBuf[], ViPInt32 bytesRead)
 {
    ViStatus hpe1445a_status = VI_SUCCESS;
    *bytesRead = 0L;
        
    if ((hpe1445a_status = viRead (instrSession, rdBuf, numBytes, bytesRead)) < 0)
        return hpe1445a_status;

    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Reset                                                           */
/* Purpose:  This function resets the instrument.                            */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_reset (ViSession instrSession)
{
    ViUInt32 retCnt = 0;
    ViStatus hpe1445a_status = VI_SUCCESS;

    /*  Initialize the instrument to a known state.  */
    if ((hpe1445a_status = viWrite (instrSession, "*RST", 4, &retCnt)) < 0)
        return hpe1445a_status;

    if ((hpe1445a_status = hpe1445a_defaultInstrSetup (instrSession)) < 0)  
        return hpe1445a_status;
        
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Self-Test                                                       */
/* Purpose:  This function executes the instrument self-test and returns     */
/*           the result.                                                     */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_selfTest (ViSession instrSession, 
                        ViPInt16 testResult, ViChar testMessage[])
{
    ViUInt32 retCnt = 0;
    ViStatus hpe1445a_status = VI_SUCCESS;

    if ((hpe1445a_status = viWrite (instrSession, "*TST?", 5, &retCnt)) < 0)
        return hpe1445a_status;

    if ((hpe1445a_status = viScanf (instrSession, "%hd", testResult)) < 0)
        return hpe1445a_status;
        
    if (*testResult == 0)
        Fmt (testMessage, "%s<Self-Test Passed");
    else
        Fmt (testMessage, "%s<See Manual for Self-Test Code Description");

    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Error Query                                                     */
/* Purpose:  This function queries the instrument error queue, and returns   */
/*           the result.                                                     */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_errorQuery (ViSession instrSession, 
                        ViPInt32 errCode, ViChar errMessage[])
{
    ViUInt32 retCnt = 0;
    ViStatus hpe1445a_status = VI_SUCCESS;

    if ((hpe1445a_status = viWrite (instrSession, ":SYST:ERR?", 10, &retCnt)) < 0)
        return hpe1445a_status;

    if ((hpe1445a_status = viScanf (instrSession, "%ld,\"%[^\"]", errCode, errMessage)) < 0)
        return hpe1445a_status;
    
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Error Message                                                   */
/* Purpose:  This function translates the error return value from the        */
/*           instrument driver into a user-readable string.                  */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_errorMessage (ViSession instrSession, 
                        ViStatus errorCode, ViChar errMessage[])
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViInt16 i;
    static hpe1445a_tStringValPair statusDescArray[] = {
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
/*        need to be inserted in the include file "hpe1445a.h".  The codes     */
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

    hpe1445a_status = viStatusDesc (instrSession, errorCode, errMessage);
    if (hpe1445a_status == VI_WARN_UNKNOWN_STATUS) {
        for (i=0; statusDescArray[i].stringName; i++) {
            if (statusDescArray[i].stringVal == errorCode) {
                strcpy (errMessage, statusDescArray[i].stringName);
                return (VI_SUCCESS);
            }
        }
        sprintf (errMessage, "Unknown Error 0x%08lX", errorCode);
        return (VI_WARN_UNKNOWN_STATUS);
    }
    
    hpe1445a_status = VI_SUCCESS;
    return hpe1445a_status;
}
 
/*===========================================================================*/
/* Function: Revision Query                                                  */
/* Purpose:  This function returns the driver and instrument revisions.      */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_revisionQuery (ViSession instrSession,
                        ViChar driverRev[], ViChar instrRev[])
{
    ViUInt32 retCnt = 0;
    ViStatus hpe1445a_status = VI_SUCCESS;

    if ((hpe1445a_status = viWrite (instrSession, "*IDN?", 5, &retCnt)) < 0)
        return hpe1445a_status;

    if ((hpe1445a_status = viScanf (instrSession, "%*[^,],%*[^,],%*[^,],%[^\n]", instrRev)) < 0)
        return hpe1445a_status;

    strcpy (driverRev, hpe1445a_REVISION);
    
    return hpe1445a_status;
}
 
/*===========================================================================*/
/* Function: Close                                                           */
/* Purpose:  This function closes the instrument.                            */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_close (ViSession instrSession)
{
    hpe1445a_instrRange instrPtr;
    ViSession rmSession;
    ViStatus hpe1445a_status = VI_SUCCESS;

    if ((hpe1445a_status = viGetAttribute (instrSession, VI_ATTR_RM_SESSION, &rmSession)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viGetAttribute (instrSession, VI_ATTR_USER_DATA, &instrPtr)) < 0)
        return hpe1445a_status;
    
    /*- Disable Event SRQ ---------------------------------------------------*/
    if ((hpe1445a_status = viDisableEvent(instrSession, VI_EVENT_SERVICE_REQ, VI_QUEUE)) < 0) 
        return hpe1445a_status;
    
    free (instrPtr);
    
    hpe1445a_status = viClose (instrSession);
    viClose (rmSession);

    return hpe1445a_status;
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
ViBoolean hpe1445a_invalidViBooleanRange (ViBoolean val)
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
ViBoolean hpe1445a_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max)
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
ViBoolean hpe1445a_invalidViInt32Range  (ViInt32 val, ViInt32 min, ViInt32 max)
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
ViBoolean hpe1445a_invalidViUInt16Range  (ViUInt16 val, ViUInt16 min, ViUInt16 max)
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
ViBoolean hpe1445a_invalidViUInt32Range  (ViUInt32 val, ViUInt32 min, ViUInt32 max)
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
ViBoolean hpe1445a_invalidViReal32Range  (ViReal32 val, ViReal32 min, ViReal32 max)
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
ViBoolean hpe1445a_invalidViReal64Range  (ViReal64 val, ViReal64 min, ViReal64 max)
{
    return ((val < min || val > max) ? VI_TRUE : VI_FALSE);
}

/*===========================================================================*/
/* Function: Initialize Clean Up                                             */
/* Purpose:  This function is used only by the hpe1445a_init function.  When   */
/*           an error is detected this function is called to close the       */
/*           open resource manager and instrument object sessions and to     */
/*           set the instrSession that is returned from hpe1445a_init to       */
/*           VI_NULL.                                                        */
/*===========================================================================*/
ViStatus hpe1445a_initCleanUp (ViSession openRMSession,
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
ViStatus hpe1445a_readToFile (ViSession instrSession, ViString filename,
                        ViUInt32 readBytes, ViPUInt32 retCount)
{
    ViStatus  hpe1445a_status = VI_SUCCESS;
    ViByte    buffer[BUFFER_SIZE];
    ViUInt32  bytesReadInstr = 0, bytesWrittenFile = 0;
    FILE     *targetFile;

    *retCount = 0L;
    if ((targetFile = fopen (filename, "wb")) == VI_NULL)
        return VI_ERROR_FILE_OPEN; /* not defined by VTL */

    for (;;) {
        if (readBytes > BUFFER_SIZE)
            hpe1445a_status = viRead (instrSession, buffer, BUFFER_SIZE, &bytesReadInstr);
        else
            hpe1445a_status = viRead (instrSession, buffer, readBytes, &bytesReadInstr);

        bytesWrittenFile = fwrite (buffer, sizeof (ViByte), (size_t)bytesReadInstr, targetFile);
        *retCount += bytesWrittenFile;
        if (bytesWrittenFile < bytesReadInstr)
            hpe1445a_status = VI_ERROR_FILE_WRITE; /* not defined by VTL */

        if ((readBytes <= BUFFER_SIZE) || (hpe1445a_status <= 0) || (hpe1445a_status == VI_SUCCESS_TERM_CHAR))
            break;

        readBytes -= BUFFER_SIZE;
    }

    fclose (targetFile);
    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Write From File To Instrument                                   */
/* Purpose:  This function is used to read data from a user specified file   */
/*           and write it to the instrument.                                 */
/*===========================================================================*/
ViStatus hpe1445a_writeFromFile (ViSession instrSession, ViString filename,
                        ViUInt32 writeBytes, ViPUInt32 retCount)
{
    ViStatus  hpe1445a_status = VI_SUCCESS;
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
            hpe1445a_status = viWrite (instrSession, buffer, BUFFER_SIZE, &bytesWritten);
            viSetAttribute (instrSession, VI_ATTR_SEND_END_EN, sendEnd);
            writeBytes -= BUFFER_SIZE;
            *retCount += bytesWritten;
            if (hpe1445a_status < 0)
                break;
        }
        else {
            hpe1445a_status = viWrite (instrSession, buffer, ((bytesRead < writeBytes) ? bytesRead : writeBytes), &bytesWritten);
            *retCount += bytesWritten;
            break;
        }
    }

    fclose (sourceFile);
    return hpe1445a_status;
}

/*****************************************************************************/
/*----------- INSERT INSTRUMENT-DEPENDENT UTILITY ROUTINES HERE -------------*/
/*****************************************************************************/

/*===========================================================================*/
/* Function: Default Instrument Setup                                        */
/* Purpose:  This function sends a default setup to the instrument.  This    */
/*           function is called by the hpe1445a_reset operation and by the     */
/*           hpe1445a_init function if the reset option has not been           */
/*           selected.  This function is useful for configuring any          */
/*           instrument settings that are required by the rest of the        */
/*           instrument driver functions such as turning headers ON or OFF   */
/*           or using the long or short form for commands, queries, and data.*/                                    
/*===========================================================================*/
ViStatus hpe1445a_defaultInstrSetup (ViSession instrSession)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViUInt32 retCnt = 0;
    
    hpe1445a_instrRange instrPtr;
    instrPtr = malloc (sizeof (struct hpe1445a_statusDataRanges));

/*===========================================================================*/
/* Change to reflect the global status variables that your driver needs      */
/* to keep track of.  For example trigger mode of each session               */
/*===========================================================================*/
    instrPtr -> triggerMode = 0;
    instrPtr -> val2 = 0;
    instrPtr -> val3 = 0;
    strcpy (instrPtr -> instrDriverRevision, hpe1445a_REVISION);
    
    if (viSetAttribute (instrSession, VI_ATTR_USER_DATA, (ViUInt32)instrPtr) != 0)
        return hpe1445a_status;

    /*- Enable Event SRQ ----------------------------------------------------*/
    if ((hpe1445a_status = viEnableEvent(instrSession, VI_EVENT_SERVICE_REQ, VI_QUEUE, VI_NULL)) < 0) 
        return hpe1445a_status;

    return hpe1445a_status;
}

/*===========================================================================*/
/* Function: Wait for RQS                                                    */
/* Purpose:  This function waits for a service request.                      */
/*===========================================================================*/
ViStatus _VI_FUNC hpe1445a_waitForRqs (ViSession instrSession, 
                        ViInt32 timeout, ViInt16 *statusByte)
{
    ViStatus hpe1445a_status = VI_SUCCESS;
    ViChar   wrtBuf[BUFFER_SIZE], rdBuf[BUFFER_SIZE];
    ViUInt32 retCnt;
    ViEventType eventType;
    ViEvent  dupHandle;
    
    if ((hpe1445a_status = viWaitOnEvent (instrSession, VI_EVENT_SERVICE_REQ, timeout, &eventType, &dupHandle)) < 0)
        return hpe1445a_status;
    if ((hpe1445a_status = viReadSTB (instrSession, statusByte)) < 0)
        return hpe1445a_status;
    hpe1445a_status = viClose (dupHandle);

    return hpe1445a_status;
}


/*****************************************************************************/
/*=== END INSTRUMENT DRIVER SOURCE CODE =====================================*/
/*****************************************************************************/
