/*****************************************************************************/
/*                                                                           */                                                                          
/*  Copyright 2004 ZTEC Instruments, Incorporated. All rights reserved.      */
/*                                                                           */                                                                          
/*  ZTEC Instruments ZT1428VXI Digitizing Oscilloscope ===================== */
/*  LabWindows 6.0 Instrument Driver                                         */
/*  By: ZTEC Instruments, Albuquerque, NM                                    */
/*  Version History:    :                                                    */
/*  1.11  03/16/04 - Initial Beta Release for ZT1428VXI                      */
/*  1.12  0?/??/04 - Second Beta Release for ZT1428VXI                       */
/*	1.13  03/29/04 - Third Beta Release for ZT1428VXI						 */
/*                                                                           */                                                                          
/*****************************************************************************/ 
#define DRIVER_VER			1.13		/*  Instrument driver version  */
#define zt1428_MANF_ID      0xE80       /*  Instrument manufacturer ID */
#define zt1428_MODEL_CODE   0x594       /*  Instrument model code      */

/*****************************************************************************/

#include <utility.h>
#include <gpib.h>
#include <formatio.h>
#include <nivxi.h>
#include <ansi_c.h>
#include <visa.h>
#include "zt1428.h"

/* = UTILITY ROUTINES ====================================================== */
int zt1428_open_instr (ViRsrc, ViSession *);
int zt1428_device_closed (ViSession);
int zt1428_get_error (ViSession);
int zt1428_read_data (ViSession, char *, int);
int zt1428_write_data (ViSession, char *, int);
int zt1428_read_reg (ViSession, int, int *);
int zt1428_write_reg (ViSession, int, int);
ViStatus zt1428_initCleanUp (ViSession openRMSession, ViPSession openinstrID, ViStatus currentStatus);

/* = INSTRUMENT TABLE ====================================================== */
/*   base_addr array: contains the base address in A16 space for the instr.  */
/*   log_addr array: contains the logical addresses of opened instruments.   */
/*   bd array: contains device descriptors returned by OpenDev (GPIB only)   */
/*   interface array: specifies the type of interface (VXI or GPIB).         */
/*   zt1428_err: the error variable for the instrument module                */
/* ========================================================================= */
//static char	 cmd[2100000];
//static short wav[2000000];

/* auto generated */
typedef struct  zt1428_stringValPair
{
   ViStatus stringVal;
   ViString stringName;
}  zt1428_tStringValPair;
 
struct zt1428_statusDataRanges {
    ViInt16 triggerMode;
    ViInt16 val2;
    ViInt16 val3;
    ViChar instrDriverRevision[256];
};

typedef struct zt1428_statusDataRanges *zt1428_instrRange;


/* ========================================================================= */
/*  Function: Initialize                                                     */
/*  Purpose:  This function opens the instrument, queries for ID, and        */
/*            initializes the instrument to a known state.                   */
/* ========================================================================= */
int _VI_FUNC zt1428_init (ViRsrc resource_name,ViSession *instrID)
{
	int zt1428_err;
	
	zt1428_err = zt1428_init_with_options(resource_name, 1, 1, instrID);
    return zt1428_err;
}

/*===========================================================================*/
/* Function: Initialize With Options                                         */
/* Purpose:  This function opens the instrument, queries the instrument      */
/*           for its ID, and initializes the instrument to a known state.    */
/*===========================================================================*/
ViStatus _VI_FUNC zt1428_init_with_options (ViRsrc resourceName, ViBoolean IDQuery,
                    ViBoolean resetDevice, ViPSession instrID)
{
    ViStatus zt1428_err = VI_SUCCESS;
    ViSession rmSession = 0;
    ViUInt32 retCnt = 0;
    ViUInt16 manfID = 0 , modelCode = 0;  
    int AdvMode;
    char cmd[256];

    /*- Open instrument session ---------------------------------------------*/
    if ((zt1428_err = viOpenDefaultRM (&rmSession)) < 0)
        return zt1428_err;

    if ((zt1428_err = viOpen (rmSession, resourceName, VI_NULL, VI_NULL, instrID)) < 0) {
        viClose (rmSession);
        return zt1428_err;
    }

    /*- Configure VISA Formatted I/O ----------------------------------------*/
    if ((zt1428_err = viSetAttribute (*instrID, VI_ATTR_TMO_VALUE, 10000)) < 0)
            return zt1428_initCleanUp (rmSession, instrID, zt1428_err);
  
    if ((zt1428_err = viSetBuf (*instrID, VI_READ_BUF|VI_WRITE_BUF, 4000)) < 0)
            return zt1428_initCleanUp (rmSession, instrID, zt1428_err);
  
    if ((zt1428_err = viSetAttribute (*instrID, VI_ATTR_WR_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS)) < 0)
            return zt1428_initCleanUp (rmSession, instrID, zt1428_err);
    
    if ((zt1428_err = viSetAttribute (*instrID, VI_ATTR_RD_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS)) < 0)
            return zt1428_initCleanUp (rmSession, instrID, zt1428_err);

    /*- Identification Query ------------------------------------------------*/
    if (IDQuery) {
        if ((zt1428_err = viGetAttribute (*instrID, VI_ATTR_MANF_ID, &manfID)) < 0)
            return zt1428_initCleanUp (rmSession, instrID, zt1428_err);
        if ((zt1428_err = viGetAttribute (*instrID, VI_ATTR_MODEL_CODE, &modelCode)) < 0)
            return zt1428_initCleanUp (rmSession, instrID, zt1428_err);

        if (manfID != zt1428_MANF_ID || modelCode != zt1428_MODEL_CODE)
            return zt1428_initCleanUp (rmSession, instrID, VI_ERROR_FAIL_ID_QUERY);
    }
	/*- Reset instrument ----------------------------------------------------*/
    if (resetDevice) {
        if ((zt1428_err = zt1428_reset (*instrID)) < 0)
            return zt1428_initCleanUp (rmSession, instrID, zt1428_err);
    }
    
    /* check for ADVANCED mode */
    if ((zt1428_err = zt1428_write_data (*instrID, "SYST:ADV?", 9)) != 0)
    	return zt1428_initCleanUp (rmSession, instrID, zt1428_err);
    if ((zt1428_err = zt1428_read_data (*instrID, cmd, 100)) != 0)
    	return zt1428_initCleanUp (rmSession, instrID, zt1428_err);

    Scan (cmd, "%s>%d[b4]", &AdvMode);
    if (AdvMode==0)	 
    {
	    /* if SYST:ADV off, turn on SYSY:ADV, flag error to rerun resource manager */
	    if ((zt1428_err = zt1428_write_data (*instrID, "SYST:ADV ON", 11)) != 0)
    		return zt1428_initCleanUp (rmSession, instrID, zt1428_err);
	    
	    zt1428_err = ZT1428_ADV_MODE_ERR;
	    return zt1428_err;
    }
    return zt1428_err;
}


/* ========================================================================= */
/*  Function: Auto Setup                                                     */
/*  Purpose:  This function runs the auto scale function.                    */
/* ========================================================================= */
int _VI_FUNC zt1428_auto_setup (ViSession instrID)
{
    int zt1428_err;
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;

    if ((zt1428_err = zt1428_write_data (instrID, ":AUT", 4)) != 0)
        return zt1428_err;

    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Auto Logic Setup                                               */
/*  Purpose:  This function runs the auto setup for standard logic levels.   */
/* ========================================================================= */
int _VI_FUNC zt1428_auto_logic (ViSession instrID, int channel, int logic)
{
	static char *zt1428_source[3];
	static char *zt1428_logic[2];
	int zt1428_err;
    char cmd[256];
	
    zt1428_source[ZT1428_CHAN1] = "CHAN1";
    zt1428_source[ZT1428_CHAN2] = "CHAN2";

    zt1428_logic[ZT1428_LOGIC_TTL] = "TTL";
    zt1428_logic[ZT1428_LOGIC_ECL] = "ECL";
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;

    Fmt (cmd, "%s:%s", zt1428_source[channel], zt1428_logic[logic]); 
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;

    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Vertical                                                       */
/*  Purpose:  This function configures the vertical settings on a channel.   */
/* ========================================================================= */
int _VI_FUNC zt1428_vertical (ViSession instrID, int channel, int coupling,
                              int lowpass_filter, double probe_atten,
                              double range, double offset)
{
	static char *zt1428_coup[4];
	static char *zt1428_filter[3];
	int zt1428_err;
    char cmd[256];
	
    zt1428_coup[ZT1428_VERT_COUP_AC]    = "AC;LFR 0";
    zt1428_coup[ZT1428_VERT_COUP_ACLFR] = "AC;LFR 1";
    zt1428_coup[ZT1428_VERT_COUP_DC]    = "DC";
    zt1428_coup[ZT1428_VERT_COUP_DCF]   = "DCF";

    zt1428_filter[ZT1428_VERT_FILT_OFF]   = "HFR 0";
    zt1428_filter[ZT1428_VERT_FILT_30MHZ] = "HFR 1;HFR:FREQ 30e6";
    zt1428_filter[ZT1428_VERT_FILT_1MHZ]  = "HFR 1;HFR:FREQ 1e6";
  
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;

    if( channel == ZT1428_CHAN_BOTH )
    	Fmt (cmd, "CHAN1:COUP %s;%s;PROB %f;RANG %f;OFFS %f;:VIEW CHAN1;:CHAN2:COUP %s;%s;PROB %f;RANG %f;OFFS %f;:VIEW CHAN2", 
    		 zt1428_coup[coupling], zt1428_filter[lowpass_filter], probe_atten, range, offset, zt1428_coup[coupling], 
    		 zt1428_filter[lowpass_filter], probe_atten, range, offset);
    else
		Fmt (cmd, "CHAN%d[b4]:COUP %s;%s;PROB %f;RANG %f;OFFS %f;:VIEW CHAN%d[b4]", channel, zt1428_coup[coupling],
        	 zt1428_filter[lowpass_filter], probe_atten, range, offset, channel);
    	
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    
    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Query Vertical                                                 */
/*  Purpose:  This function queries the vertical input channel settings.     */
/* ========================================================================= */
int _VI_FUNC zt1428_query_vertical (ViSession instrID, int channel, int *coupling,
                                    int *lowpass_filter, double *probe_atten,
                                    double *range, double *offset)
{
	unsigned int i;
	unsigned int value;
	char string[20];
	double fvalue;
	static char *zt1428_source[3];
	static char *zt1428_coup[4];
	int zt1428_err;
    char cmd[256];
	
    zt1428_source[ZT1428_CHAN1] = "CHAN1";
    zt1428_source[ZT1428_CHAN2] = "CHAN2";

    zt1428_coup[ZT1428_VERT_COUP_AC]    = "AC";
    zt1428_coup[ZT1428_VERT_COUP_ACLFR] = "AC";
    zt1428_coup[ZT1428_VERT_COUP_DC]    = "DC";
    zt1428_coup[ZT1428_VERT_COUP_DCF]   = "DCF";
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    
	Fmt (cmd, "CHAN%d[b4]:COUP?;LFR?", channel);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t59];%d[b4]", string, &value);
    for(i=0;i<4;i++)
    {
    	if(strcmp(string,zt1428_coup[i])==0)
    	{
			if(i ==  ZT1428_VERT_COUP_AC)
			{
				if(value > 0)
					i = ZT1428_VERT_COUP_ACLFR;
			}
			*coupling = i;
			break;
		}
	}

	Fmt (cmd, "CHAN%d[b4]:HFR?;HFR:FREQ?", channel);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%d[b4];%f", &value, &fvalue);
    if(value == 0)
    	*lowpass_filter = ZT1428_VERT_FILT_OFF;
    else if(fvalue == 30e6)
    	*lowpass_filter = ZT1428_VERT_FILT_30MHZ;
	else
    	*lowpass_filter = ZT1428_VERT_FILT_1MHZ;

	Fmt (cmd, "CHAN%d[b4]:RANG?;OFFS?;PROB?", channel);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%f;%f;%f", range, offset, probe_atten);

    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;    
}


/* ========================================================================= */
/*  Function: Acquisition                                                    */
/*  Purpose:  This function configures the horizontal timebase and       .   */
/*            acquisition settings.                                      .   */
/* ========================================================================= */
int _VI_FUNC zt1428_acquisition (ViSession instrID, int points,
                                 double sample_interval, int timebase_ref,
                                 double timebase_delay, int trigger_mode,
                                 int acquire_type, int acquire_count)
{
    int samp_mode;
	static char *zt1428_acq_mode[3];
	static char *zt1428_sample[2];
	static char *zt1428_ref[3];
	static char *zt1428_time_mode[3];
	int zt1428_err;
    char cmd[256];
	
    zt1428_acq_mode[ZT1428_ACQ_NORM] = "NORM";
    zt1428_acq_mode[ZT1428_ACQ_AVER] = "AVER";
    zt1428_acq_mode[ZT1428_ACQ_ENV]  = "ENV";
 
    zt1428_sample[ZT1428_ACQ_RTIME] = "REAL";
    zt1428_sample[ZT1428_ACQ_REP]   = "REP";

	zt1428_ref[ZT1428_ACQ_LEFT]  = "LEFT";
    zt1428_ref[ZT1428_ACQ_CENT]  = "CENT";
    zt1428_ref[ZT1428_ACQ_RIGHT] = "RIGH";
 
    zt1428_time_mode[ZT1428_ACQ_AUTO] = "AUTO";
    zt1428_time_mode[ZT1428_ACQ_SING] = "SING";
    zt1428_time_mode[ZT1428_ACQ_TRIG] = "TRIG";

    if ((zt1428_err = (zt1428_err = zt1428_device_closed (instrID))) != 0)
        return zt1428_err;
    
    if(acquire_type == ZT1428_ACQ_NORM)
    	samp_mode = ZT1428_ACQ_RTIME;		/*realtime mode for normal acquisitions*/
    else
    	samp_mode = ZT1428_ACQ_REP;			/*repetative mode for average or envelope acquisitions*/
    
    Fmt (cmd, ":TIM:SAMP %s;MODE %s;REF %s;INT %f;DEL %f;ACQ:TYPE %s;COUN %d[b4];POIN %d[b4]", 
    	zt1428_sample[samp_mode], zt1428_time_mode[trigger_mode], zt1428_ref[timebase_ref], sample_interval, timebase_delay,
    	zt1428_acq_mode[acquire_type], acquire_count, points);

    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
        
    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;    
}

/* ========================================================================= */
/*  Function: Query Acquisition                                              */
/*  Purpose:  This function queries the timebase and acquisition settings.   */
/* ========================================================================= */
int _VI_FUNC zt1428_query_acquisition (ViSession instrID, int *points,
                                       double *sample_interval,
                                       int *timebase_ref,
                                       double *timebase_delay, int *trigger_mode,
                                       int *acquire_type, int *acquire_count)
{
	unsigned int i;
    int samp_mode;
	char string1[20];
	char string2[20];
	char string3[20];
	static char *zt1428_acq_mode[3];
	static char *zt1428_sample[2];
	static char *zt1428_ref[3];
	static char *zt1428_time_mode[3];
	int zt1428_err;
    char cmd[256];

    zt1428_acq_mode[ZT1428_ACQ_NORM] = "NORM";
    zt1428_acq_mode[ZT1428_ACQ_AVER] = "AVER";
    zt1428_acq_mode[ZT1428_ACQ_ENV]  = "ENV";

    zt1428_sample[ZT1428_ACQ_RTIME] = "REAL";
    zt1428_sample[ZT1428_ACQ_REP]   = "REP";

	zt1428_ref[ZT1428_ACQ_LEFT]  = "LEFT";
    zt1428_ref[ZT1428_ACQ_CENT]  = "CENT";
    zt1428_ref[ZT1428_ACQ_RIGHT] = "RIGH";

    zt1428_time_mode[ZT1428_ACQ_AUTO] = "AUTO";
    zt1428_time_mode[ZT1428_ACQ_SING] = "SING";
    zt1428_time_mode[ZT1428_ACQ_TRIG] = "TRIG";
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    
	Fmt (cmd, "TIM:SAMP?;MODE?;REF?;INT?;DEL?");
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t59];%s[t59];%s[t59];%f;%f", string1, string2, string3, sample_interval, timebase_delay);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_sample[i])==0)
			samp_mode = i;
	}
    for(i=0;i<3;i++)
    {
    	if(strcmp(string2,zt1428_time_mode[i])==0)
			*trigger_mode = i;
	}
    for(i=0;i<3;i++)
    {
    	if(strcmp(string3,zt1428_ref[i])==0)
			*timebase_ref = i;
	}

	Fmt (cmd, "ACQ:TYPE?;COUN?;POIN?");
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t59];%d[b4];%d[b4]", string1, acquire_count, points);
    if(samp_mode ==  ZT1428_ACQ_RTIME)
    	*acquire_type = ZT1428_ACQ_NORM;
    else
    {
	    for(i=0;i<3;i++)
	    {
	    	if(strcmp(string1,zt1428_acq_mode[i])==0)
				*acquire_type = i;
		}
	}

    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;    
}


/* ========================================================================= */
/*  Function: Function                                                       */
/*  Purpose:  This function sets the math operation for each function.       */
/* ========================================================================= */
int _VI_FUNC zt1428_function (ViSession instrID, int func_num,
                              int operation, int source1, int source2,
                              int func_state, double range, double offset)
{
	static char *zt1428_source[9];
	static char *zt1428_func_oper[7];
	int zt1428_err;
    char cmd[256];
	
	zt1428_source[ZT1428_CHAN1] = "CHAN1";
    zt1428_source[ZT1428_CHAN2] = "CHAN2";
    zt1428_source[ZT1428_WMEM1] = "WMEM1";
    zt1428_source[ZT1428_WMEM2] = "WMEM2";
    zt1428_source[ZT1428_WMEM3] = "WMEM3";
    zt1428_source[ZT1428_WMEM4] = "WMEM4";
    zt1428_source[ZT1428_FUNC1] = "FUNC1";
    zt1428_source[ZT1428_FUNC2] = "FUNC2";
    
    zt1428_func_oper[ZT1428_FUNC_ADD]  = "ADD";
    zt1428_func_oper[ZT1428_FUNC_SUB]  = "SUBT";
    zt1428_func_oper[ZT1428_FUNC_MULT] = "MULT";
    zt1428_func_oper[ZT1428_FUNC_DIFF] = "DIFF";
    zt1428_func_oper[ZT1428_FUNC_INT]  = "INT";
    zt1428_func_oper[ZT1428_FUNC_INV]  = "INV";
    zt1428_func_oper[ZT1428_FUNC_ONLY] = "ONLY";
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    

	if (func_state == ZT1428_FUNC_ON)
	{
		if ((operation == ZT1428_FUNC_ADD)||(operation == ZT1428_FUNC_SUB)||(operation == ZT1428_FUNC_MULT)) 	/*two sources*/
			 Fmt (cmd, "%s<VIEW %s;:%s:%s %s,%s", zt1428_source[func_num], zt1428_source[func_num], 
			      zt1428_func_oper[operation], zt1428_source[source1], zt1428_source[source2]);
	    else 	/*one source*/
			 Fmt (cmd, "%s<VIEW %s;:%s:%s %s", zt1428_source[func_num], zt1428_source[func_num], 
			      zt1428_func_oper[operation], zt1428_source[source1]);
		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        	return zt1428_err;	 
		 
		if (range > 0) 	/*if range set to zero, let instrument autoset range and offset, i.e. don't force*/
		{
			Fmt (cmd, "%s<:%s:RANG %f;OFFS %f", zt1428_source[func_num], range, offset);
			if (zt1428_write_data (instrID, cmd, NumFmtdBytes ()) != 0)
        		return zt1428_err;
        }

	}
	else 	/*if func turned off, just blank the waveform*/
	{
		Fmt (cmd, "%s<BLAN %s", zt1428_source[func_num]);
		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        	return zt1428_err;	
    }
		 
    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Query Function                                                 */
/*  Purpose:  This function queries the waveform math function settings.     */
/* ========================================================================= */
int _VI_FUNC zt1428_query_function (ViSession instrID, int func_num,
                                    int *func_state, double *range, 
                                    double *offset)
{
	static char * zt1428_source[9];
	int zt1428_err;
    char cmd[256];
	
	zt1428_source[ZT1428_CHAN1] = "CHAN1";
    zt1428_source[ZT1428_CHAN2] = "CHAN2";
    zt1428_source[ZT1428_WMEM1] = "WMEM1";
    zt1428_source[ZT1428_WMEM2] = "WMEM2";
    zt1428_source[ZT1428_WMEM3] = "WMEM3";
    zt1428_source[ZT1428_WMEM4] = "WMEM4";
    zt1428_source[ZT1428_FUNC1] = "FUNC1";
    zt1428_source[ZT1428_FUNC2] = "FUNC2";
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
	Fmt (cmd, "STAT? %s", zt1428_source[func_num]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%d[b4]", func_state);

	Fmt (cmd, "%s<:%s:RANG?;OFFS?", zt1428_source[func_num]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%f;%f", range, offset);

    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}


/* ========================================================================= */
/*  Function: External Input                                                 */
/*  Purpose:  This function sets the external trigger/clock input settings.  */
/* ========================================================================= */
int _VI_FUNC zt1428_ext_input (ViSession instrID, int external_mode,
                               double external_level, int external_impedance)
{
    int running;
	char *zt1428_ext_imp[2];
	int zt1428_err;
    char cmd[256];
	char trg_str[20];
	
	zt1428_ext_imp[ZT1428_EXT_IMP_1M] = "DC";
    zt1428_ext_imp[ZT1428_EXT_IMP_50] = "DCF";
	
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    /* get original trig source - use value to reset at end of routine */
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:SOUR?", 10)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s", trg_str);

	if(external_mode == ZT1428_EXT_MODE_CLK)
	{
	    /*   Check to see if instrument in run state   */
	    if ((zt1428_err = zt1428_write_data (instrID, "RUN?", 4)) != 0)
	        return zt1428_err;
	    if ((zt1428_err = zt1428_read_data (instrID, cmd, 5)) != 0)
	        return zt1428_err;
	    Scan (cmd, "%s>%d[b4]", &running);
	    if (running > 0)
	        if ((zt1428_err = zt1428_write_data (instrID, "STOP", 4)) != 0)
	            return zt1428_err;
		if ((zt1428_err = zt1428_write_data (instrID, "TIM:SAMP:CLOC:MODE EXT", 22)) != 0)
        	return zt1428_err;
		Fmt (cmd, "%s<TIM:SAMP:CLOC:LEV %f;IMP %s", external_level, zt1428_ext_imp[external_impedance]);
		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        	return zt1428_err;
    }
    else	/* external_mode == ZT1428_EXT_MODE_TRIG */
    {
    	if ((zt1428_err = zt1428_write_data (instrID, "TIM:SAMP:CLOC:MODE INT", 22)) != 0)
        	return zt1428_err;
    	Fmt (cmd, "%s<TRIG:SOUR EXT;COUP %s;LEV %f", zt1428_ext_imp[external_impedance], external_level);
		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        	return zt1428_err;
    }
	
    /* restore trigger source */
    Fmt (cmd, "%s<TRIG:SOUR %s", trg_str);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;	 

	zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Query External Input                                           */
/*  Purpose:  This function queries the external input settings.             */
/* ========================================================================= */
int _VI_FUNC zt1428_query_ext_input (ViSession instrID, int *external_mode,
                                     double *external_level,
                                     int *external_impedance)
{
	unsigned int i;
	char string[20];
	char *zt1428_ext_imp[2];
	char *zt1428_ext_mode[2];
	int zt1428_err;
	char trg_str[20];
    char cmd[256];
	
	zt1428_ext_imp[ZT1428_EXT_IMP_1M] = "DC";
    zt1428_ext_imp[ZT1428_EXT_IMP_50] = "DCF";
	zt1428_ext_mode[ZT1428_EXT_MODE_TRIG] = "INT";
    zt1428_ext_mode[ZT1428_EXT_MODE_CLK]  = "EXT";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    /* get original trig source - use value to reset at end of routine */
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:SOUR?", 10)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s", trg_str);

	if ((zt1428_err = zt1428_write_data (instrID, "TIM:SAMP:CLOC:MODE?", 19)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s", string);
    if(strcmp(string,zt1428_ext_mode[ZT1428_EXT_MODE_CLK])==0)
    {
		*external_mode = ZT1428_EXT_MODE_CLK;
		Fmt (cmd, "%s<TIM:SAMP:CLOC:IMP?;LEV?");
	}
	else 	/* ZT1428_EXT_MODE_TRIG */
	{
		*external_mode = ZT1428_EXT_MODE_TRIG;
    	Fmt (cmd, "%s<TRIG:SOUR EXT;COUP?;LEV?");
	}
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t59];%f", string, external_level);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string,zt1428_ext_imp[i])==0)
			*external_impedance = i;
	}
    
   /* restore trigger source */
    Fmt (cmd, "%s<TRIG:SOUR %s", trg_str);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;	 

	zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}


/* ========================================================================= */
/*  Function: Outputs                                                        */
/*  Purpose:  This function sets the BNC connector and ECLTRGn outputs.      */
/* ========================================================================= */
int _VI_FUNC zt1428_outputs (ViSession instrID, int bnc_output, double bnc_voltage,
                             int ecl0, int ecl1)
{
	int out_enable;
	static char *zt1428_bnc[6];
    int zt1428_err;
    char cmd[256];
    
    zt1428_bnc[ZT1428_OUT_BNC_PROB] = "PROB";
    zt1428_bnc[ZT1428_OUT_BNC_TRIG] = "TRIG";
    zt1428_bnc[ZT1428_OUT_BNC_DC]   = "DCCAL";
    zt1428_bnc[ZT1428_OUT_BNC_0V]   = "ZVOL";
    zt1428_bnc[ZT1428_OUT_BNC_5V]   = "FVOL";
    zt1428_bnc[ZT1428_OUT_BNC_SCL]  = "SCL";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    if((ecl0 == ZT1428_OUT_OFF) && (ecl1 == ZT1428_OUT_OFF) && (bnc_output != ZT1428_OUT_BNC_TRIG))
    	out_enable = ZT1428_OUT_OFF;
    else
    	out_enable = ZT1428_OUT_ON;
    Fmt (cmd, "%s<OUTP:STAT %d[b4];ECLT0 %d[b4];ECLT1 %d[b4]", out_enable, ecl0, ecl1);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;

    Fmt (cmd, "%s<BNC %s,%f", zt1428_bnc[bnc_output], bnc_voltage);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;

	zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Query Outputs                                                  */
/*  Purpose:  This function queries the BNC and ECLTRGn output settings.     */
/* ========================================================================= */
int _VI_FUNC zt1428_query_outputs (ViSession instrID, int *bnc_output,
                                   double *bnc_voltage, int *ecl0, int *ecl1)
{
	unsigned int i;
	char string[20];
	static char *zt1428_bnc[6];
    int zt1428_err;
    char cmd[256];
    
    zt1428_bnc[ZT1428_OUT_BNC_PROB] = "PROB";
    zt1428_bnc[ZT1428_OUT_BNC_TRIG] = "TRIG";
    zt1428_bnc[ZT1428_OUT_BNC_DC]   = "DCCAL";
    zt1428_bnc[ZT1428_OUT_BNC_0V]   = "ZVOL";
    zt1428_bnc[ZT1428_OUT_BNC_5V]   = "FVOL";
    zt1428_bnc[ZT1428_OUT_BNC_SCL]  = "SCL";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
	if ((zt1428_err = zt1428_write_data (instrID, "OUTP:ECLT0?;ECLT1?", 18)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%d[b4];%d[b4]", ecl0, ecl1);

	if ((zt1428_err = zt1428_write_data (instrID, "BNC?", 4)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t44],%f", string, bnc_voltage);
    for(i=0;i<6;i++)
    {
    	if(strcmp(string,zt1428_bnc[i])==0)
			*bnc_output = i;
	}

	zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}


/* ========================================================================= */
/*  Function: Edge Trigger                                                   */
/*  Purpose:  This function sets the edge trigger mode options.              */
/* ========================================================================= */
int _VI_FUNC zt1428_edge_trigger (ViSession instrID, int source, double level,
                                  int slope, int sensitivity)
{
	static char *zt1428_trig_source[6];
	static char *zt1428_trig_slop[2];
	static char *zt1428_trig_sens[4];
	int zt1428_err;
    char cmd[256];
	
	zt1428_trig_source[ZT1428_TRG_CHAN1] = 	"CHAN1";
    zt1428_trig_source[ZT1428_TRG_CHAN2] = 	"CHAN2";
    zt1428_trig_source[ZT1428_TRG_EXT] = 	"EXT";
    zt1428_trig_source[ZT1428_TRG_ECL0] = 	"ECLT0";
    zt1428_trig_source[ZT1428_TRG_ECL1] = 	"ECLT1";
 
    zt1428_trig_slop[ZT1428_TRG_SLOPE_NEG] = "NEG";
    zt1428_trig_slop[ZT1428_TRG_SLOPE_POS] = "POS";
 
    zt1428_trig_sens[ZT1428_TRG_SENS_NORM] = "NORM";
    zt1428_trig_sens[ZT1428_TRG_SENS_LOW] = "LOW";
	zt1428_trig_sens[ZT1428_TRG_SENS_LFR] = "LFR";
    zt1428_trig_sens[ZT1428_TRG_SENS_HFR] = "HFR";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    if ((zt1428_err = zt1428_write_data (instrID, "TRIG:MODE EDGE", 14)) != 0)
            return zt1428_err;
    
    Fmt (cmd, "%s<TRIG:SOUR %s;SLOP %s", zt1428_trig_source[source], zt1428_trig_slop[slope]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;	 
            
    if ( (source == ZT1428_TRG_CHAN1) || (source == ZT1428_TRG_CHAN2) )
    {
    	Fmt (cmd, "%s<TRIG:LEV %f;SENS %s", level, zt1428_trig_sens[sensitivity]);
    	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
            return zt1428_err;
    }
    else if (source == ZT1428_TRG_EXT)
    {
    	Fmt (cmd, "%s<TRIG:LEV %f", level);
	    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;	 
    }
		 
    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Query Trigger                                                  */
/*  Purpose:  This function queries the common edge trigger settings.        */
/* ========================================================================= */
int _VI_FUNC zt1428_query_trigger (ViSession instrID, int *source,
                                   int *trigger_mode, double *level_chan1,
                                   double *level_chan2, double *level_ext,
                                   int *sens_chan1, int *sens_chan2,
                                   int *slope_chan1, int *slope_chan2,
                                   int *slope_ext, int *slope_ecl0, int *slope_ecl1)
{
	unsigned int i;
	char string1[20];
	char string2[20];
	static char *zt1428_trig_source[6];
	static char *zt1428_trig_slop[2];
	static char *zt1428_trig_sens[4];
	int zt1428_err;
    char cmd[256];
	
	zt1428_trig_source[0] = "";
	zt1428_trig_source[ZT1428_TRG_CHAN1] = 	"CHAN1";
    zt1428_trig_source[ZT1428_TRG_CHAN2] = 	"CHAN2";
    zt1428_trig_source[ZT1428_TRG_EXT] = 	"EXT";
    zt1428_trig_source[ZT1428_TRG_ECL0] = 	"ECLT0";
    zt1428_trig_source[ZT1428_TRG_ECL1] = 	"ECLT1";

    zt1428_trig_slop[ZT1428_TRG_SLOPE_NEG] = "NEG";
    zt1428_trig_slop[ZT1428_TRG_SLOPE_POS] = "POS";

    zt1428_trig_sens[ZT1428_TRG_SENS_NORM] = "NORM";
    zt1428_trig_sens[ZT1428_TRG_SENS_LOW] = "LOW";
	zt1428_trig_sens[ZT1428_TRG_SENS_LFR] = "LFR";
    zt1428_trig_sens[ZT1428_TRG_SENS_HFR] = "HFR";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    /* get original trig source - use value to reset at end of routine */
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:SOUR?", 10)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s", string1);
    for(i=0;i<6;i++)
    {
    	if(strcmp(string1,zt1428_trig_source[i])==0)
			*source = i;
	}

	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:SOUR CHAN1;SLOP?;SENS?;LEV?", 32)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t59];%s[t59];%f", string1, string2, level_chan1);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_trig_slop[i])==0)
			*slope_chan1 = i;
	}
    for(i=0;i<4;i++)
    {
    	if(strcmp(string2,zt1428_trig_sens[i])==0)
			*sens_chan1 = i;
	}
    
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:SOUR CHAN2;SLOP?;SENS?;LEV?", 32)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t59];%s[t59];%f", string1, string2, level_chan2);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_trig_slop[i])==0)
			*slope_chan2 = i;
	}
    for(i=0;i<4;i++)
    {
    	if(strcmp(string2,zt1428_trig_sens[i])==0)
			*sens_chan2 = i;
	}
    
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:SOUR EXT;SLOP?;LEV?", 24)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t59];%f", string1, level_ext);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_trig_slop[i])==0)
			*slope_ext = i;
	}
    
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:SOUR ECLT0;SLOP?", 21)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s", string1);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_trig_slop[i])==0)
			*slope_ecl0 = i;
	}
    
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:SOUR ECLT1;SLOP?", 21)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s", string1);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_trig_slop[i])==0)
			*slope_ecl1 = i;
	}
    
    /* restore trigger source */
    Fmt (cmd, "%s<TRIG:SOUR %s", zt1428_trig_source[*source]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;	 
            
    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}


/* ========================================================================= */
/*  Function: Soft Trigger                                                   */
/*  Purpose:  This function gerenates a software trigger event.              */
/* ========================================================================= */
int _VI_FUNC zt1428_soft_trigger (ViSession instrID)
{
    int zt1428_err;
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    if ((zt1428_err = zt1428_write_data (instrID, "*TRG", 4)) != 0)
        return zt1428_err;

    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
}


/* ========================================================================= */
/*  Function: Set Trigger to Offset                                          */
/*  Purpose:  This function centers the trigger level at the vertical offset.*/
/* ========================================================================= */
int _VI_FUNC zt1428_trigger_center (ViSession instrID, int source)
{
	static char *zt1428_trig_source[4];
	int zt1428_err;
    char cmd[256];
	
	zt1428_trig_source[0] = "";
	zt1428_trig_source[ZT1428_TRG_CHAN1] = 	"CHAN1";
    zt1428_trig_source[ZT1428_TRG_CHAN2] = 	"CHAN2";
    zt1428_trig_source[ZT1428_TRG_EXT] = 	"EXT";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    Fmt (cmd, "%s<TRIG:SOUR %s;CENT", zt1428_trig_source[source]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;	 

    zt1428_err = zt1428_get_error(instrID);
        
    return zt1428_err;
    
}

/* ========================================================================= */
/*  Function: Trigger Holdoff                                                */
/*  Purpose:  This function sets the trigger holdoff.                        */
/* ========================================================================= */
int _VI_FUNC zt1428_trigger_holdoff (ViSession instrID, int holdoff_type,
                                     int holdoff_events, double holdoff_time)
{
 	int zt1428_err;
     char cmd[256];
	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
    
 	if (holdoff_type == ZT1428_TRG_HOLD_EVENT)
  		Fmt (cmd, "%s<TRIG:HOLD EVEN,%d",holdoff_events);
 	else 	/* ZT1428_TRG_HOLD_EVENT */
  		Fmt (cmd, "%s<TRIG:HOLD TIME,%f",holdoff_time);     
 	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
  		return zt1428_err;
 
 	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;
 }


/* ========================================================================= */
/*  Function: Set Pattern Trigger                                            */
/*  Purpose:  This function sets the pattern trigger mode.                   */
/* ========================================================================= */
int _VI_FUNC zt1428_pattern_trigger (ViSession instrID, char logic[],
                                     int condition, double gt_time, double lt_time,
                                     double level_chan1, double level_chan2,
                                     double level_ext, int sens_chan1, int sens_chan2)
{
	unsigned int truth;
	unsigned int trg_src;
	static char *zt1428_trig_source[6];
	static char *zt1428_trig_sens[4];
	static char *zt1428_trig_logic[3];
	static char *zt1428_trig_condition[5];
	int zt1428_err;
    char cmd[256];

	zt1428_trig_source[0] = "";
	zt1428_trig_source[ZT1428_TRG_CHAN1] = 	"CHAN1";
    zt1428_trig_source[ZT1428_TRG_CHAN2] = 	"CHAN2";
    zt1428_trig_source[ZT1428_TRG_EXT] = 	"EXT";
    zt1428_trig_source[ZT1428_TRG_ECL0] = 	"ECLT0";
    zt1428_trig_source[ZT1428_TRG_ECL1] = 	"ECLT1";

    zt1428_trig_sens[ZT1428_TRG_SENS_NORM] = "NORM";
    zt1428_trig_sens[ZT1428_TRG_SENS_LOW] = "LOW";
	zt1428_trig_sens[ZT1428_TRG_SENS_LFR] = "LFR";
    zt1428_trig_sens[ZT1428_TRG_SENS_HFR] = "HFR";

    zt1428_trig_logic[ZT1428_TRG_PATT_LOW]  = "LOW";
    zt1428_trig_logic[ZT1428_TRG_PATT_HIGH] = "HIGH";
    zt1428_trig_logic[ZT1428_TRG_PATT_DONT] = "DONT";

    zt1428_trig_condition[ZT1428_TRG_PATT_ENTER] = "ENT";
    zt1428_trig_condition[ZT1428_TRG_PATT_EXIT]  = "EXIT";
    zt1428_trig_condition[ZT1428_TRG_PATT_GT]    = "GT";
    zt1428_trig_condition[ZT1428_TRG_PATT_LT]    = "LT";
    zt1428_trig_condition[ZT1428_TRG_PATT_RANGE] = "RANG";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    if ((zt1428_err = zt1428_write_data (instrID, "TRIG:MODE PATT", 14)) != 0)
            return zt1428_err;
		 
	Fmt (cmd, "%s<TRIG:SOUR CHAN1;LEV %f;SENS %s", level_chan1, zt1428_trig_sens[sens_chan1]);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
		return zt1428_err;

	Fmt (cmd, "%s<TRIG:SOUR CHAN2;LEV %f;SENS %s", level_chan2, zt1428_trig_sens[sens_chan2]);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
		return zt1428_err;

	Fmt (cmd, "%s<TRIG:SOUR EXT;LEV %f", level_ext);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
		return zt1428_err;
    
    for( trg_src=ZT1428_TRG_CHAN1; trg_src<=ZT1428_TRG_ECL1; trg_src++ )
    {
	    if(logic[trg_src-1] == 'X')
	    	truth =  ZT1428_TRG_PATT_DONT;
	    else if(logic[trg_src-1] == 'H')
	    	truth =  ZT1428_TRG_PATT_HIGH;
	    else /* 'L' */
	    	truth =  ZT1428_TRG_PATT_LOW;
	    Fmt (cmd, "%s<TRIG:PATH %s;LOG %s", zt1428_trig_source[trg_src], zt1428_trig_logic[truth]);
	    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;
	}
	
	if((condition == ZT1428_TRG_PATT_ENTER) || (condition == ZT1428_TRG_PATT_EXIT) )
		Fmt (cmd, "%s<TRIG:COND %s", zt1428_trig_condition[condition]);
	else if(condition == ZT1428_TRG_PATT_GT)
		Fmt (cmd, "%s<TRIG:COND %s,%f", zt1428_trig_condition[condition], gt_time);
	else if(condition == ZT1428_TRG_PATT_LT)
		Fmt (cmd, "%s<TRIG:COND %s,%f", zt1428_trig_condition[condition], lt_time);
	else 	/* ZT1428_TRG_PATT_RANGE */
		Fmt (cmd, "%s<TRIG:COND %s,%f,%f", zt1428_trig_condition[condition], gt_time, lt_time);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;

	zt1428_err = zt1428_get_error(instrID);
	
    return zt1428_err;
}
 

/* ========================================================================= */
/*  Function: Set State Trigger                                              */
/*  Purpose:  This function sets the state trigger mode.                     */
/* ========================================================================= */
int _VI_FUNC zt1428_state_trigger (ViSession instrID, char logic[], int source,
                                   int condition, int slope, double level_chan1,
                                   double level_chan2, double level_ext,
                                   int sens_chan1, int sens_chan2)
{
	unsigned int truth;
	unsigned int trg_src;
	static char *zt1428_trig_source[6];
	static char *zt1428_trig_slop[2];
	static char *zt1428_trig_sens[4];
	static char *zt1428_trig_logic[3];
	static char *zt1428_trig_condition[2];
	int zt1428_err;
    char cmd[256];
	
	zt1428_trig_source[0] = "";
	zt1428_trig_source[ZT1428_TRG_CHAN1] = 	"CHAN1";
    zt1428_trig_source[ZT1428_TRG_CHAN2] = 	"CHAN2";
    zt1428_trig_source[ZT1428_TRG_EXT] = 	"EXT";
    zt1428_trig_source[ZT1428_TRG_ECL0] = 	"ECLT0";
    zt1428_trig_source[ZT1428_TRG_ECL1] = 	"ECLT1";

    zt1428_trig_slop[ZT1428_TRG_SLOPE_NEG] = "NEG";
    zt1428_trig_slop[ZT1428_TRG_SLOPE_POS] = "POS";

    zt1428_trig_sens[ZT1428_TRG_SENS_NORM] = "NORM";
    zt1428_trig_sens[ZT1428_TRG_SENS_LOW] = "LOW";
	zt1428_trig_sens[ZT1428_TRG_SENS_LFR] = "LFR";
    zt1428_trig_sens[ZT1428_TRG_SENS_HFR] = "HFR";

    zt1428_trig_logic[ZT1428_TRG_PATT_LOW]  = "LOW";
    zt1428_trig_logic[ZT1428_TRG_PATT_HIGH] = "HIGH";
    zt1428_trig_logic[ZT1428_TRG_PATT_DONT] = "DONT";

    zt1428_trig_condition[ZT1428_TRG_STAT_FALSE] = "FALS";
    zt1428_trig_condition[ZT1428_TRG_STAT_TRUE]  = "TRUE";
	

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    if ((zt1428_err = zt1428_write_data (instrID, "TRIG:MODE STAT", 14)) != 0)
            return zt1428_err;
		 
	Fmt (cmd, "%s<TRIG:SOUR CHAN1;LEV %f;SENS %s", level_chan1, zt1428_trig_sens[sens_chan1]);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
		return zt1428_err;

	Fmt (cmd, "%s<TRIG:SOUR CHAN2;LEV %f;SENS %s", level_chan2, zt1428_trig_sens[sens_chan2]);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
		return zt1428_err;

	Fmt (cmd, "%s<TRIG:SOUR EXT;LEV %f", level_ext);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
		return zt1428_err;
    
    for( trg_src=ZT1428_TRG_CHAN1; trg_src<=ZT1428_TRG_ECL1; trg_src++ )
    {
	    if(logic[trg_src-1] == 'X')
	    	truth =  ZT1428_TRG_PATT_DONT;
	    else if(logic[trg_src-1] == 'H')
	    	truth =  ZT1428_TRG_PATT_HIGH;
	    else /* 'L' */
	    	truth =  ZT1428_TRG_PATT_LOW;
	    Fmt (cmd, "%s<TRIG:PATH %s;LOG %s", zt1428_trig_source[trg_src], zt1428_trig_logic[truth]);
	    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;
	}
		 
	Fmt (cmd, "%s<TRIG:SOUR %s;SLOP %s", zt1428_trig_source[source], zt1428_trig_slop[slope]);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
		return zt1428_err;
    
	Fmt (cmd, "%s<TRIG:COND %s", zt1428_trig_condition[condition]);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
		return zt1428_err;

	zt1428_err = zt1428_get_error(instrID);
	
    return zt1428_err;
}
 

/* ========================================================================= */
/*  Function: Set TV Trigger                                                 */
/*  Purpose:  This function sets the TV trigger mode.                        */
/* ========================================================================= */
int _VI_FUNC zt1428_tv_trigger (ViSession instrID, int standard, int field,
                                int line, int slope, int source, double level,
                                int sensitivity)
{
	unsigned int truth;
	unsigned int trg_src;
	static char *zt1428_trig_source[6];
	static char *zt1428_trig_slop[2];
	static char *zt1428_trig_sens[4];
	int zt1428_err;
    char cmd[256];
	
	zt1428_trig_source[0] = "";
	zt1428_trig_source[ZT1428_TRG_CHAN1] = 	"CHAN1";
    zt1428_trig_source[ZT1428_TRG_CHAN2] = 	"CHAN2";
    zt1428_trig_source[ZT1428_TRG_EXT] = 	"EXT";
    zt1428_trig_source[ZT1428_TRG_ECL0] = 	"ECLT0";
    zt1428_trig_source[ZT1428_TRG_ECL1] = 	"ECLT1";

    zt1428_trig_slop[ZT1428_TRG_SLOPE_NEG] = "NEG";
    zt1428_trig_slop[ZT1428_TRG_SLOPE_POS] = "POS";

    zt1428_trig_sens[ZT1428_TRG_SENS_NORM] = "NORM";
    zt1428_trig_sens[ZT1428_TRG_SENS_LOW] = "LOW";
	zt1428_trig_sens[ZT1428_TRG_SENS_LFR] = "LFR";
    zt1428_trig_sens[ZT1428_TRG_SENS_HFR] = "HFR";
	

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;    
    
    if ((zt1428_err = zt1428_write_data (instrID, "TRIG:MODE TV", 12)) != 0)
            return zt1428_err;
		 
    Fmt (cmd, "%s<TRIG:SOUR %s;SLOP %s", zt1428_trig_source[source], zt1428_trig_slop[slope]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;	 
            
    if ( (source == ZT1428_TRG_CHAN1) || (source == ZT1428_TRG_CHAN2) )
    {
    	Fmt (cmd, "%s<TRIG:LEV %f;SENS %s", level, zt1428_trig_sens[sensitivity]);
    	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
            return zt1428_err;
    }
    else if (source == ZT1428_TRG_EXT)
    {
    	Fmt (cmd, "%s<TRIG:LEV %f", level);
	    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;	 
    }
		 
    Fmt (cmd, "%s<TRIG:STAN %d[b4];FIEL %d[b4];LINE %d[b4]", standard, field, line);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;	 
            
	zt1428_err = zt1428_get_error(instrID);
	
    return zt1428_err;
}
 
/* ========================================================================= */
/*  Function: Query Advanced Trigger                                         */
/*  Purpose:  This function queries the advanced trigger settings.           */
/* ========================================================================= */
int _VI_FUNC zt1428_query_adv_trigger (ViSession instrID, int *holdoff_type, 
                                       double *holdoff_value, char logic[], 
                                       int *pattern_condition,
                                       double *gt_time, double *lt_time,
                                       int *state_condition, int *standard,
                                       int *field, int *line)
{
	unsigned int i;
	unsigned int trg_mode;
	unsigned int trg_src;
	double value;
	char string1[20];
	static char *zt1428_trig_source[6];
	static char *zt1428_trig_mode[4];
	static char *zt1428_trig_holdoff[2];
	static char *zt1428_trig_logic[3];
	static char *zt1428_trig_patt_condition[5];
	static char *zt1428_trig_stat_condition[2];
	int zt1428_err;
    char cmd[256];
	
	zt1428_trig_source[0] = "";
	zt1428_trig_source[ZT1428_TRG_CHAN1] = 	"CHAN1";
    zt1428_trig_source[ZT1428_TRG_CHAN2] = 	"CHAN2";
    zt1428_trig_source[ZT1428_TRG_EXT] = 	"EXT";
    zt1428_trig_source[ZT1428_TRG_ECL0] = 	"ECLT0";
    zt1428_trig_source[ZT1428_TRG_ECL1] = 	"ECLT1";

    zt1428_trig_mode[ZT1428_TRG_MODE_EDGE] = "EDGE";
    zt1428_trig_mode[ZT1428_TRG_MODE_PATT] = "PATT";
    zt1428_trig_mode[ZT1428_TRG_MODE_STAT] = "STAT";
    zt1428_trig_mode[ZT1428_TRG_MODE_TV]   = "TV";

    zt1428_trig_holdoff[ZT1428_TRG_HOLD_TIME]   = "TIME";
    zt1428_trig_holdoff[ZT1428_TRG_HOLD_EVENT]  = "EVEN";

    zt1428_trig_logic[ZT1428_TRG_PATT_LOW]  = "LOW";
    zt1428_trig_logic[ZT1428_TRG_PATT_HIGH] = "HIGH";
    zt1428_trig_logic[ZT1428_TRG_PATT_DONT] = "DONT";

    zt1428_trig_patt_condition[ZT1428_TRG_PATT_ENTER] = "ENT";
    zt1428_trig_patt_condition[ZT1428_TRG_PATT_EXIT]  = "EXIT";
    zt1428_trig_patt_condition[ZT1428_TRG_PATT_GT]    = "GT";
    zt1428_trig_patt_condition[ZT1428_TRG_PATT_LT]    = "LT";
    zt1428_trig_patt_condition[ZT1428_TRG_PATT_RANGE] = "RANG";

    zt1428_trig_stat_condition[ZT1428_TRG_STAT_FALSE] = "FALS";
    zt1428_trig_stat_condition[ZT1428_TRG_STAT_TRUE]  = "TRUE";

 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
    
	/* save trigger mode to restore when done */
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:MODE?", 10)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s", string1);
    for(i=0;i<4;i++)
    {
    	if(strcmp(string1,zt1428_trig_mode[i])==0)
			trg_mode = i;
	}

	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:HOLD?", 10)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t44],%f", string1, holdoff_value);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_trig_holdoff[i])==0)
			*holdoff_type = i;
	}

    /* reset input data */
    *gt_time = 0;
    *lt_time = 0;
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:MODE PATT;COND?", 20)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    /* strip old characters from command string */ 
    Scan (cmd, "%s>%s[t13t10t0]", cmd);
    Scan (cmd, "%s>%s[t44]", string1);
    for(i=0;i<5;i++)
    {
    	if(strcmp(string1,zt1428_trig_patt_condition[i])==0)
			*pattern_condition = i;
	}
	if(*pattern_condition ==  ZT1428_TRG_PATT_GT)
		Scan (cmd, "%s>%s[t44],%f", string1, gt_time);
	else if(*pattern_condition ==  ZT1428_TRG_PATT_LT)
		Scan (cmd, "%s>%s[t44],%f", string1, lt_time);
	else if(*pattern_condition ==  ZT1428_TRG_PATT_RANGE)
		Scan (cmd, "%s>%s[t44],%f,%f", string1, gt_time, lt_time);

    for( trg_src=ZT1428_TRG_CHAN1; trg_src<=ZT1428_TRG_ECL1; trg_src++ )
    {
	    Fmt (cmd, "%s<TRIG:PATH %s;LOG?", zt1428_trig_source[trg_src]);
	    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;
	    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
	        return zt1428_err;
	    Scan (cmd, "%s>%s", string1);
	    if(strcmp(string1,zt1428_trig_logic[ZT1428_TRG_PATT_LOW])==0)
	    	logic[trg_src-1] = 'L';
	    else if(strcmp(string1,zt1428_trig_logic[ZT1428_TRG_PATT_HIGH])==0)
	    	logic[trg_src-1] = 'H';
	    else 	/* ZT1428_TRG_PATT_DONT */
	    	logic[trg_src-1] = 'X';
	}
		 
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:MODE STAT;COND?", 20)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s", string1);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_trig_stat_condition[i])==0)
			*state_condition = i;
	}
	
	if ((zt1428_err = zt1428_write_data (instrID, "TRIG:MODE TV;STAN?;FIEL?;LINE?", 30)) != 0)
    	return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%d[b4];%d[b4];%d[b4];", standard, field, line);
	
	zt1428_err = zt1428_get_error(instrID);
	
    Fmt (cmd, "%s<TRIG:MODE %s", zt1428_trig_mode[trg_mode]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;

	return zt1428_err;
    
}


/* ========================================================================= */
/*  Function: Get Trigger Event                                              */
/*  Purpose:  This function queries the trigger event status         .       */
/* ========================================================================= */
int _VI_FUNC zt1428_trigger_event (ViSession instrID, int *trigger_event)
{
 	int zt1428_err;
    char cmd[256];
 	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
    
   	if ((zt1428_err = zt1428_write_data (instrID, "TER?", 4)) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%d[b4]", trigger_event);
        
	zt1428_err = zt1428_get_error(instrID);
	
	return zt1428_err;
}

/* ========================================================================= */
/*  Function: Measurement                                                    */
/*  Purpose:  This function sets the instrument to take a measurement.       */
/* ========================================================================= */
int _VI_FUNC zt1428_measurement (ViSession instrID, int primary_source,
                                 int secondary_source, int measurement,
                                 double *result)
{
    static char *zt1428_source[9];
    static char *zt1428_measure[21];
    int zt1428_err;
    char cmd[256];
    
    zt1428_source[ZT1428_CHAN1] = "CHAN1";
    zt1428_source[ZT1428_CHAN2] = "CHAN2";
    zt1428_source[ZT1428_WMEM1] = "WMEM1";
    zt1428_source[ZT1428_WMEM2] = "WMEM2";
    zt1428_source[ZT1428_WMEM3] = "WMEM3";
    zt1428_source[ZT1428_WMEM4] = "WMEM4";
    zt1428_source[ZT1428_FUNC1] = "FUNC1";
    zt1428_source[ZT1428_FUNC2] = "FUNC2";
    
    zt1428_measure[ZT1428_MEAS_RISE] = "RIS";
    zt1428_measure[ZT1428_MEAS_FALL] = "FALL";
    zt1428_measure[ZT1428_MEAS_FREQ] = "FREQ";
    zt1428_measure[ZT1428_MEAS_PER]  = "PER";
    zt1428_measure[ZT1428_MEAS_PWID] = "PWID";
    zt1428_measure[ZT1428_MEAS_NWID] = "NWID";
    zt1428_measure[ZT1428_MEAS_VAMP] = "VAMP";
    zt1428_measure[ZT1428_MEAS_VBAS] = "VBAS";
    zt1428_measure[ZT1428_MEAS_VTOP] = "VTOP";
    zt1428_measure[ZT1428_MEAS_VPP]  = "VPP";
    zt1428_measure[ZT1428_MEAS_VAVG] = "VAV";
    zt1428_measure[ZT1428_MEAS_VMAX] = "VMAX";
    zt1428_measure[ZT1428_MEAS_VMIN] = "VMIN";
    zt1428_measure[ZT1428_MEAS_VACR] = "VACR";
    zt1428_measure[ZT1428_MEAS_VDCR] = "VDCR";
    zt1428_measure[ZT1428_MEAS_DUTY] = "DUT";
    zt1428_measure[ZT1428_MEAS_DEL]  = "DEL";
    zt1428_measure[ZT1428_MEAS_OVER] = "OVER";
    zt1428_measure[ZT1428_MEAS_PRE]  = "PRES";
    zt1428_measure[ZT1428_MEAS_TMAX] = "TMAX";
    zt1428_measure[ZT1428_MEAS_TMIN] = "TMIN";
    
    if(secondary_source == ZT1428_NONE)
    	Fmt( cmd, "%s<MEAS:SCR;SOUR %s", zt1428_source[primary_source]);
    else	/* two sources */
    	Fmt( cmd, "%s<MEAS:SCR;SOUR %s,%s", zt1428_source[primary_source], zt1428_source[secondary_source]);
	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;    
    
    Fmt( cmd, "%s<MEAS:%s?",zt1428_measure[measurement]); 
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%f", result);
        
	zt1428_err = zt1428_get_error(instrID);
	
	return zt1428_err;

}

/* ========================================================================= */
/*  Function: Set Measurement Level                                          */
/*  Purpose:  This function sets the user defined measurement levels.        */
/* ========================================================================= */
int _VI_FUNC zt1428_measurement_level (ViSession instrID, int user_mode,
                                       int units, double upper_limit,
                                       double lower_limit)
{
	static char *zt1428_meas_units[2];
	int zt1428_err;
    char cmd[256];
	
	zt1428_meas_units[ZT1428_MEAS_USER_PCT]  = "PERC";
    zt1428_meas_units[ZT1428_MEAS_USER_VOLT] = "VOLT";
	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    
	if (user_mode == ZT1428_MEAS_MODE_STAN)
	{
		if ((zt1428_err = zt1428_write_data (instrID, "MEAS:MODE STAN", 14)) != 0)
        	return zt1428_err;	
    }
    else /* ZT1428_MEAS_MODE_USER */
    {
    	Fmt (cmd, "%s<MEAS:MODE USER;:MEAS:UNIT %s;:MEAS:UPP %f;:MEAS:LOW %f", zt1428_meas_units[units], upper_limit, lower_limit);
   		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        	return zt1428_err;
    }
    
    zt1428_err = zt1428_get_error(instrID);
	
	return zt1428_err;
}


/* ========================================================================= */
/*  Function: Set Delay Parameters                                           */
/*  Purpose:  This function sets the delay measurement parameters.           */
/* ========================================================================= */
int _VI_FUNC zt1428_delay_parameters (ViSession instrID, int start_slope,
                                      int start_edge, int start_level,
                                      int stop_slope, int stop_edge, int stop_level)
{
	static char *zt1428_del_slope[2];
	static char *zt1428_del_level[3];
    int zt1428_err;
    char cmd[256];
   
	zt1428_del_slope[ZT1428_DEL_SLOP_NEG] = "NEG";
	zt1428_del_slope[ZT1428_DEL_SLOP_POS] = "POS";

	zt1428_del_level[ZT1428_DEL_LEV_LOW] = "LOW";
	zt1428_del_level[ZT1428_DEL_LEV_MID] = "MIDD";
	zt1428_del_level[ZT1428_DEL_LEV_UPP] = "UPP";
 
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    
    Fmt (cmd, "%s<MEAS:DEF DEL,%s,%d,%s,%s,%d,%s",zt1428_del_slope[start_slope],start_edge,zt1428_del_level[start_level],
         zt1428_del_slope[stop_slope],stop_edge,zt1428_del_level[stop_level]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
  		return zt1428_err;
    
 	zt1428_err = zt1428_get_error(instrID);
 
 	return zt1428_err;
}

/* ========================================================================= */
/*  Function: Set Width Parameters                                           */
/*  Purpose:  This function sets the width measurement parameters.           */
/* ========================================================================= */
int _VI_FUNC zt1428_width_parameters (ViSession instrID, int pos_width_level,
                                      int neg_width_level)
{
    int zt1428_err;
	static char *zt1428_del_level[3];
    char cmd[256];
 
	zt1428_del_level[ZT1428_DEL_LEV_LOW] = "LOW";
	zt1428_del_level[ZT1428_DEL_LEV_MID] = "MIDD";
	zt1428_del_level[ZT1428_DEL_LEV_UPP] = "UPP";
 
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    
    Fmt (cmd, "%s<MEAS:DEF PWID,%s;DEF NWID,%s",zt1428_del_level[pos_width_level],zt1428_del_level[neg_width_level]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
  		return zt1428_err;
    
 	zt1428_err = zt1428_get_error(instrID);
 
 	return zt1428_err;
}


/* ========================================================================= */
/*  Function: Query Measurement                                              */
/*  Purpose:  This function queries the user defined measurement settings.   */
/* ========================================================================= */
int _VI_FUNC zt1428_query_measurement (ViSession instrID, int *user_mode,
                                       int *units, double *upper_limit,
                                       double *lower_limit, int *start_slope,
                                       int *stop_slope, int *start_edge,
                                       int *stop_edge, int *start_level,
                                       int *stop_level, int *pos_width_level,
                                       int *neg_width_level)
{
	unsigned int i;
	char string1[20];
	char string2[20];
	char string3[20];
	char string4[20];
	static char *zt1428_meas_mode[2];
	static char *zt1428_meas_units[2];
	static char *zt1428_del_slope[2];
	static char *zt1428_del_level[3];
	int zt1428_err;
    char cmd[256];
 
	zt1428_meas_mode[ZT1428_MEAS_MODE_STAN] = "STAN";
    zt1428_meas_mode[ZT1428_MEAS_MODE_USER] = "USER";

	zt1428_meas_units[ZT1428_MEAS_USER_PCT]  = "PERC";
    zt1428_meas_units[ZT1428_MEAS_USER_VOLT] = "VOLT";

	zt1428_del_slope[ZT1428_DEL_SLOP_NEG] = "NEG";
	zt1428_del_slope[ZT1428_DEL_SLOP_POS] = "POS";

	zt1428_del_level[ZT1428_DEL_LEV_LOW] = "LOW";
	zt1428_del_level[ZT1428_DEL_LEV_MID] = "MIDD";
	zt1428_del_level[ZT1428_DEL_LEV_UPP] = "UPP";
	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    
	if ((zt1428_err = zt1428_write_data (instrID, "MEAS:MODE?;UNIT?;UPP?;LOW?", 26)) != 0)
    	return zt1428_err;
     if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t59];%s[t59];%f;%f", string1, string2, upper_limit, lower_limit);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_meas_mode[i])==0)
			*user_mode = i;
	}
    for(i=0;i<2;i++)
    {
    	if(strcmp(string2,zt1428_meas_units[i])==0)
			*units = i;
	}
   
    if ((zt1428_err = zt1428_write_data (instrID, "MEAS:DEF? DEL", 13)) != 0)
    	return zt1428_err;
     if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t44],%s[t44],%d[b4],%s[t44],%s[t44],%d[b4],%s", string1, string1, start_edge, string2, string3, stop_edge, string4);
    for(i=0;i<2;i++)
    {
    	if(strcmp(string1,zt1428_del_slope[i])==0)
			*start_slope = i;
	}
    for(i=0;i<3;i++)
    {
    	if(strcmp(string2,zt1428_del_level[i])==0)
			*start_level = i;
	}
    for(i=0;i<2;i++)
    {
    	if(strcmp(string3,zt1428_del_slope[i])==0)
			*stop_slope = i;
	}
    for(i=0;i<3;i++)
    {
    	if(strcmp(string4,zt1428_del_level[i])==0)
			*stop_level = i;
	}

    if ((zt1428_err = zt1428_write_data (instrID, "MEAS:DEF? PWID", 14)) != 0)
    	return zt1428_err;
     if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t44],%s", string1, string1);
    for(i=0;i<3;i++)
    {
    	if(strcmp(string1,zt1428_del_level[i])==0)
			*pos_width_level = i;
	}

    if ((zt1428_err = zt1428_write_data (instrID, "MEAS:DEF? NWID", 14)) != 0)
    	return zt1428_err;
     if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%s[t44],%s", string1, string1);
    for(i=0;i<3;i++)
    {
    	if(strcmp(string1,zt1428_del_level[i])==0)
			*neg_width_level = i;
	}

    zt1428_err = zt1428_get_error(instrID);
	
	return zt1428_err;
}


/* ========================================================================= */
/*  Function: Set Limit Test                                                 */
/*  Purpose:  This function sets up limit test and statistical measurements. */
/* ========================================================================= */
int _VI_FUNC zt1428_limit_test (ViSession instrID, int limit_test, int statistics,
                                int primary_source, int secondary_source,
                                int measurement, double upper_limit,
                                double lower_limit, int postfailure,
                                int destination)
{
    unsigned int meas_on;
    static char *zt1428_source[9];
    static char *zt1428_measure[19];
    static char *zt1428_measure_post[2];
    int zt1428_err;
    char cmd[256];
    
    zt1428_source[ZT1428_NONE]  = "OFF";
    zt1428_source[ZT1428_CHAN1] = "CHAN1";
    zt1428_source[ZT1428_CHAN2] = "CHAN2";
    zt1428_source[ZT1428_WMEM1] = "WMEM1";
    zt1428_source[ZT1428_WMEM2] = "WMEM2";
    zt1428_source[ZT1428_WMEM3] = "WMEM3";
    zt1428_source[ZT1428_WMEM4] = "WMEM4";
    zt1428_source[ZT1428_FUNC1] = "FUNC1";
    zt1428_source[ZT1428_FUNC2] = "FUNC2";
    
    zt1428_measure[ZT1428_MEAS_RISE] = "RIS";
    zt1428_measure[ZT1428_MEAS_FALL] = "FALL";
    zt1428_measure[ZT1428_MEAS_FREQ] = "FREQ";
    zt1428_measure[ZT1428_MEAS_PER]  = "PER";
    zt1428_measure[ZT1428_MEAS_PWID] = "PWID";
    zt1428_measure[ZT1428_MEAS_NWID] = "NWID";
    zt1428_measure[ZT1428_MEAS_VAMP] = "VAMP";
    zt1428_measure[ZT1428_MEAS_VBAS] = "VBAS";
    zt1428_measure[ZT1428_MEAS_VTOP] = "VTOP";
    zt1428_measure[ZT1428_MEAS_VPP]  = "VPP";
    zt1428_measure[ZT1428_MEAS_VAVG] = "VAV";
    zt1428_measure[ZT1428_MEAS_VMAX] = "VMAX";
    zt1428_measure[ZT1428_MEAS_VMIN] = "VMIN";
    zt1428_measure[ZT1428_MEAS_VACR] = "VACR";
    zt1428_measure[ZT1428_MEAS_VDCR] = "VDCR";
    zt1428_measure[ZT1428_MEAS_DUTY] = "DUT";
    zt1428_measure[ZT1428_MEAS_DEL]  = "DEL";
    zt1428_measure[ZT1428_MEAS_OVER] = "OVER";
    zt1428_measure[ZT1428_MEAS_PRE]  = "PRES";
    
    zt1428_measure_post[ZT1428_MEAS_POST_STOP] = "STOP";
    zt1428_measure_post[ZT1428_MEAS_POST_CONT] = "CONT";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    
   	meas_on = 0;
   	if (limit_test == ZT1428_MEAS_LIM_ON)
   	{
    	meas_on++;
	    Fmt( cmd, "%s<MEAS:LIM MEAS;POST %s;DEST %s", zt1428_measure_post[postfailure], zt1428_source[destination]);
		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        	return zt1428_err;
    }
    else 	/* ZT1428_MEAS_LIM_OFF */
    {
    	if ((zt1428_err = zt1428_write_data (instrID, "MEAS:LIM OFF", 12)) != 0)
        	return zt1428_err;
	}    
   	
   	if (statistics == ZT1428_MEAS_STAT_ON)
   	{
    	meas_on++;
    	if ((zt1428_err = zt1428_write_data (instrID, "MEAS:STAT ON", 12)) != 0)
        	return zt1428_err;
    }
    else 	/* ZT1428_MEAS_STAT_OFF */
    {
    	if ((zt1428_err = zt1428_write_data (instrID, "MEAS:STAT OFF", 13)) != 0)
        	return zt1428_err;
	}    
   	
   	/* configure measurement if limit test or statistics on */
   	if (meas_on > 0)
   	{
	    if(secondary_source == ZT1428_NONE)
	    	Fmt( cmd, "%s<MEAS:SOUR %s", zt1428_source[primary_source]);
	    else	/* two sources */
	    	Fmt( cmd, "%s<MEAS:SOUR %s,%s", zt1428_source[primary_source], zt1428_source[secondary_source]);
		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;    

	    Fmt( cmd, "%s<MEAS:%s;COMP %s,%f,%f",zt1428_measure[measurement], zt1428_measure[measurement], upper_limit, lower_limit); 
	    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;
    }

	zt1428_err = zt1428_get_error(instrID);
 
    return zt1428_err;
}


/* ========================================================================= */
/*  Function: Set Mask Test                                                  */
/*  Purpose:  This function sets up the waveform mask comparison testing     */
/* ========================================================================= */
int _VI_FUNC zt1428_mask_test (ViSession instrID, int mask_test, int source,
                               int mask, double allowance, int postfailure,
                               int destination)
{
    static char *zt1428_source[9];
    static char *zt1428_measure_post[2];
    int zt1428_err;
    char cmd[256];
    
    zt1428_source[ZT1428_NONE]  = "OFF";
    zt1428_source[ZT1428_CHAN1] = "CHAN1";
    zt1428_source[ZT1428_CHAN2] = "CHAN2";
    zt1428_source[ZT1428_WMEM1] = "WMEM1";
    zt1428_source[ZT1428_WMEM2] = "WMEM2";
    zt1428_source[ZT1428_WMEM3] = "WMEM3";
    zt1428_source[ZT1428_WMEM4] = "WMEM4";
    zt1428_source[ZT1428_FUNC1] = "FUNC1";
    zt1428_source[ZT1428_FUNC2] = "FUNC2";
    
    zt1428_measure_post[ZT1428_MEAS_POST_STOP] = "STOP";
    zt1428_measure_post[ZT1428_MEAS_POST_CONT] = "CONT";

    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    
   	if (mask_test == ZT1428_MEAS_MASK_ON)
   	{
	    Fmt( cmd, "%s<MEAS:WCOM:WTEST MEAS;POST %s;DEST %s;ALL %f", zt1428_measure_post[postfailure], zt1428_source[destination], allowance);
		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        	return zt1428_err;

	    Fmt( cmd, "%s<MEAS:WCOM:COMP %s,%s",zt1428_source[source], zt1428_source[mask]); 
	    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;
    }
    else 	/* ZT1428_MEAS_MASK_OFF */
    {
    	if ((zt1428_err = zt1428_write_data (instrID, "MEAS:WCOM:WTEST OFF", 19)) != 0)
        	return zt1428_err;
    }

	zt1428_err = zt1428_get_error(instrID);
 
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Get Stat / Limit Result                                        */
/*  Purpose:  This function returns the statistical or limit test results    */
/*            of a measurement.                                              */
/* ========================================================================= */
int _VI_FUNC zt1428_result_stats (ViSession instrID, int measurement,
                                  double *current, double *minimum,
                                  double *maximum, double *avg_pass,
                                  int *limit_test)
{
	unsigned int i, j;
	int value;
	char string[20];
    static char *zt1428_measure[19];
    double curr, min, max, avg;
	int zt1428_err;
    char cmd[256];
	
    zt1428_measure[ZT1428_MEAS_RISE] = "RIS";
    zt1428_measure[ZT1428_MEAS_FALL] = "FALL";
    zt1428_measure[ZT1428_MEAS_FREQ] = "FREQ";
    zt1428_measure[ZT1428_MEAS_PER]  = "PER";
    zt1428_measure[ZT1428_MEAS_PWID] = "PWID";
    zt1428_measure[ZT1428_MEAS_NWID] = "NWID";
    zt1428_measure[ZT1428_MEAS_VAMP] = "VAMP";
    zt1428_measure[ZT1428_MEAS_VBAS] = "VBAS";
    zt1428_measure[ZT1428_MEAS_VTOP] = "VTOP";
    zt1428_measure[ZT1428_MEAS_VPP]  = "VPP";
    zt1428_measure[ZT1428_MEAS_VAVG] = "VAV";
    zt1428_measure[ZT1428_MEAS_VMAX] = "VMAX";
    zt1428_measure[ZT1428_MEAS_VMIN] = "VMIN";
    zt1428_measure[ZT1428_MEAS_VACR] = "VACR";
    zt1428_measure[ZT1428_MEAS_VDCR] = "VDCR";
    zt1428_measure[ZT1428_MEAS_DUTY] = "DUT";
    zt1428_measure[ZT1428_MEAS_DEL]  = "DEL";
    zt1428_measure[ZT1428_MEAS_OVER] = "OVER";
    zt1428_measure[ZT1428_MEAS_PRE]  = "PRES";
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;

    /* clear input data */
	*current = ZT_MEAS_INVALID;
	*minimum = ZT_MEAS_INVALID;
	*maximum = ZT_MEAS_INVALID;
	*avg_pass = ZT_MEAS_INVALID;
    curr = ZT_MEAS_INVALID;
    min = ZT_MEAS_INVALID;
    max = ZT_MEAS_INVALID;
    avg = ZT_MEAS_INVALID;
    
    if ((zt1428_err = zt1428_write_data (instrID, "MEAS:RES?", 9)) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    /* read number of measurements */ 
    Scan (cmd, "%s>%d[b4];%s[t13t10t0]", &value, cmd);

    for(i=0;i<value;i++)
    {
	    Scan (cmd, "%s>%s[t32] %f,%f,%f,%f;%s[t13t10t0]", string, &curr, &min, &max, &avg, cmd);
	    for(j=0;j<19;j++)
	    {
	    	/* break when returned measurement string found */
	    	if(strcmp(string,zt1428_measure[j])==0)
				break;
		}
		if(j == measurement)
		{
			/* break when requested measurement found */
			*current = curr;
			*minimum = min;
			*maximum = max;
			*avg_pass = avg;
			break;
		}
	}

    if ((zt1428_err = zt1428_write_data (instrID, "LTER?", 5)) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 200)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%d[b4]", limit_test);

	zt1428_err = zt1428_get_error(instrID);
 
    return zt1428_err;
}


/* ========================================================================= */
/*  Function: Digitize Waveform                                              */
/*  Purpose:  This function digitizes a waveform.                            */
/* ========================================================================= */
int _VI_FUNC zt1428_digitize_waveform (ViSession instrID, int channel, int mode)
{
	static char *zt1428_chan[11];
    int zt1428_err;
    char cmd[256];
    
	zt1428_chan[ZT1428_CHAN1] = "CHAN1";
 	zt1428_chan[ZT1428_CHAN2] = "CHAN2";
 	zt1428_chan[ZT1428_CHAN_BOTH] = "CHAN1,CHAN2";
 
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
    	return zt1428_err;
    
    Fmt (cmd, "%s<VIEW %s", zt1428_chan[channel]);
 	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    
    /* blank unused channel */ 
    if(channel ==  ZT1428_CHAN1)
    {
	    Fmt (cmd, "%s<BLAN %s", zt1428_chan[ZT1428_CHAN2]);
	 	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;
	}
    if(channel ==  ZT1428_CHAN2)
    {
	    Fmt (cmd, "%s<BLAN %s", zt1428_chan[ZT1428_CHAN1]);
	 	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
	        return zt1428_err;
	}
    
 	if (mode == ZT1428_DIG_NORM)
 	{
  		if ((zt1428_err = zt1428_write_data (instrID, "*CLS;*SRE 32;*ESE 1", 19)) != 0)
         	return zt1428_err;  
        if ((zt1428_err = zt1428_write_data (instrID, "*ESR?", 5)) != 0)
         	return zt1428_err;
        if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
         	return zt1428_err;
  		Fmt (cmd, "%s<DIG %s;*OPC?", zt1428_chan[channel]);
  		if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
         	return zt1428_err;
        if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
         	return zt1428_err;
    }
    else /*ZT1428_DIG_ASYN*/
    {
  		if ((zt1428_err = zt1428_write_data (instrID, "*CLS;*SRE 32;*ESE 1", 19)) != 0)
         	return zt1428_err;  
        if ((zt1428_err = zt1428_write_data (instrID, "*ESR?", 5)) != 0)
         	return zt1428_err;
        if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
         	return zt1428_err;
	  	Fmt (cmd, "%s<DIG %s;*OPC", zt1428_chan[channel]);
	  	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
         	return zt1428_err;
    }

	zt1428_err = zt1428_get_error(instrID);
 
 	return zt1428_err;
}
 
/* ========================================================================= */
/*  Function: Get Digitize Complete                                          */
/*  Purpose:  Perform ESR? for use in checking completeness of DIG.          */
/* ========================================================================= */
int _VI_FUNC zt1428_dig_complete (ViSession instrID, int *dig_complete)
{
    int zt1428_err;
    char cmd[256];
    
 	if ((zt1428_err = zt1428_device_closed (instrID) != 0))
        return zt1428_err;    
 
 	if ((zt1428_err = zt1428_write_data (instrID, "*ESR?", 5)) != 0)
  		return zt1428_err;
  
 	if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
  		return zt1428_err;
 
 	Scan( cmd, "%s>%d[b4]", dig_complete);
 	
 	*dig_complete = *dig_complete & 0x01;
  
    zt1428_err = zt1428_get_error(instrID);
 
 	return zt1428_err;
}


static short lwav[2000000];

/* ========================================================================= */
/*  Function: Read Waveform                                                  */
/*  Purpose:  This function reads a wave from the instrument into an array.  */
/* ========================================================================= */
int _VI_FUNC zt1428_read_waveform (ViSession instrID, int source,
                                   int xfer_type, double wave_array[],
                                   int *num_points, int *acq_count,
                                   double *sample_interval, double *time_offset,
                                   int *xref, double *volt_increment,
                                   double *volt_offset, int *yref)
{
    double test[1];
    int acq_mode = 0;
    int lwav_ptr = 0;
    int byte_count;
    int point_count;
    int block_size;
    int length;
    int i;
    int pos;
    int a32_addr = 0;
    int a32_size = 0;
    char res_name[128];
    ViSession res_man_handle;
    ViSession instr_handle;
    static char * zt1428_source[9];
    int zt1428_err;
    char cmd[33000];

    
 	zt1428_source[ZT1428_CHAN1] = "CHAN1";
	zt1428_source[ZT1428_CHAN2] = "CHAN2";
	zt1428_source[ZT1428_WMEM1] = "WMEM1";
	zt1428_source[ZT1428_WMEM2] = "WMEM2";
	zt1428_source[ZT1428_WMEM3] = "WMEM3";
	zt1428_source[ZT1428_WMEM4] = "WMEM4";
	zt1428_source[ZT1428_FUNC1] = "FUNC1";
	zt1428_source[ZT1428_FUNC2] = "FUNC2";

  	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    Fmt (cmd, "%s<VIEW %s;:WAV:SOUR %s;FORM WORD;:WAV:FORM:BYT MSBF;:WAV:PRE?", zt1428_source[source], zt1428_source[source]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 1000)) != 0)
        return zt1428_err;
  	Scan (cmd, "%s>%d[b4d],%d[b4],%d[b4],%d[b4],%f,%f,%d[b4],%f,%f,%d[b4]", &acq_mode, num_points, acq_count, sample_interval, 
  	      time_offset, xref, volt_increment, volt_offset, yref);
    
    if (xfer_type != ZT1428_TRAN_PRE) {  
       
    	acq_mode--; /*make preamble definition line up with code definition of acq type*/
    
    	if(xfer_type == ZT1428_TRAN_SER)
    	{
    	 	if ((zt1428_err = zt1428_write_data (instrID, "WAV:DATA?", 9)) != 0)
      			return zt1428_err;
     		/*DL waveform in pieces if larger than 32000 bytes*/
        	if ((zt1428_err = zt1428_read_data (instrID, cmd, 10)) != 0)
        	    return zt1428_err;
        	if (Scan (cmd, "%s[i2]>%i[b4w8]", &byte_count) != 1)
        	{
        	    zt1428_err = ZT1428_COM_ERR;
        	    return zt1428_err;
        	}
        	point_count = byte_count/2;
        	block_size = (ZT_DL_SIZE < byte_count)?ZT_DL_SIZE:byte_count;/*min ZT_DL_SIZE, byte_count*/
        	while(byte_count > 0)
        	{
         		if ((zt1428_err = zt1428_read_data (instrID, cmd, block_size)) != 0)
             		return zt1428_err;
             	byte_count -= block_size;
         		if (Scan (cmd, "%*i[zb2o10]>%*d[b2]", block_size/2, block_size/2, &(lwav[lwav_ptr])) != 1)  
         		{
         	   		zt1428_err = ZT1428_COM_ERR;
         	   		return zt1428_err;
         		}
         		lwav_ptr += block_size/2;
        	}
			
	    	for (i = 0; i < point_count; i++)
	     	   wave_array[i] = (*volt_increment * ((double)lwav[i] - *yref)) + *volt_offset;
    	}
    	else if(xfer_type == ZT1428_TRAN_A32)
    	{
  			Fmt (cmd, "%s<MEM:VME:MAP? %s", zt1428_source[source]);
  			if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
   				return zt1428_err;
  			if ((zt1428_err = zt1428_read_data (instrID, cmd, 1000)) != 0)
   				return zt1428_err;
  			Scan (cmd, "%s>%s[t44d],#H%x,#H%x", &a32_addr, &a32_size);
 		
   			a32_size /= 2;	/*convert from byte size to word size*/
 
  			i = viMoveIn16 (instrID, VI_A32_SPACE, a32_addr, a32_size, lwav);
 
     		if (i != 0)  
     		{
      		   	zt1428_err = ZT1428_COM_ERR;
      		   	return zt1428_err;
     		}

	  	  	for (i = 0; i < a32_size; i++)
	    	    wave_array[i] = (*volt_increment/2 * (double)lwav[i]) + *volt_offset;
 		}
 	}

 	zt1428_err = zt1428_get_error(instrID);
 
 	return zt1428_err;
}



/* ========================================================================= */
/*  Function: Store Waveform to Memory                                       */
/*  Purpose:  This function stores a waveform to memory.                     */
/* ========================================================================= */
int _VI_FUNC zt1428_store_waveform (ViSession instrID, int source,
                                    int destination)
{
    static char *zt1428_source[9];
    int zt1428_err;
    char cmd[265];
   
 	zt1428_source[ZT1428_CHAN1] = "CHAN1";
 	zt1428_source[ZT1428_CHAN2] = "CHAN2";
 	zt1428_source[ZT1428_WMEM1] = "WMEM1";
 	zt1428_source[ZT1428_WMEM2] = "WMEM2";
 	zt1428_source[ZT1428_WMEM3] = "WMEM3";
 	zt1428_source[ZT1428_WMEM4] = "WMEM4";
 	zt1428_source[ZT1428_FUNC1] = "FUNC1";
 	zt1428_source[ZT1428_FUNC2] = "FUNC2";
    
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
 
 	Fmt (cmd, "%s<VIEW %s;STOR %s,%s", zt1428_source[destination], zt1428_source[source], zt1428_source[destination]);
 	if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
  		return zt1428_err;
    
 	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;  
}

/* ========================================================================= */
/*  Function: Load Array to Memory                                           */
/*  Purpose:  This function loads waveform array data to memory.             */
/* ========================================================================= */
#define MAX_REF_SIZE	65508

int _VI_FUNC zt1428_load_array (ViSession instrID, int destination,
                                double wave_array[], int num_points,
                                double sample_interval, double time_offset,
                                int xref, double volt_increment,
                                double volt_offset, int yref)
{
    int zeros;
    int length;
    int temp;
    char temp8;
    unsigned int i;
    char *zero_str;
    static char *zt1428_source[9];
    int zt1428_err;
    char cmd[65536];
    
    zt1428_source[ZT1428_WMEM1] = "WMEM1";
    zt1428_source[ZT1428_WMEM2] = "WMEM2";
    zt1428_source[ZT1428_WMEM3] = "WMEM3";
    zt1428_source[ZT1428_WMEM4] = "WMEM4";

 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
 
    Fmt (cmd, "%s<VIEW %s;WAV:SOUR %s;FORM WORD;BYT MSBF", zt1428_source[destination], zt1428_source[destination]);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    
    Fmt (cmd, "%s<WAV:PRE 2,1,%d[b4],1,%f[e1],%f[e1],%d[b4],%f[e1],%f[e1],%d[b4]", num_points, sample_interval, time_offset,
         xref, volt_increment, volt_offset, yref);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
    
    for (i = 0; i < num_points; i++)
    {
    	temp = (((wave_array[i] - volt_offset) /  volt_increment) + yref);
		temp = (temp>0x7f80)?0x7f80:temp;
		temp = (temp<0)?0:temp;
		lwav[i] = (short)temp;
	}

    length = 2 * num_points;	/* convert to bytes for word format */
    if(length > MAX_REF_SIZE) 			/* maximum ref size in bytes */
    {
		zt1428_err =  ZT1428_WAVE_ERR;
		return zt1428_err;
	}
    else if(length >= 10000)
		zero_str = "0";
    else if(length >= 1000)
		zero_str = "00";
    else
		zero_str = "000";

    /* need to convert decimal to binary data for load */
	temp = 6;
	Fmt (cmd, "%s<:WAV:DATA #%d[b4]%s%d[b4]", temp, zero_str, length);
	temp = 9;
	Fmt (cmd, "%*d[i*b2]<%*i[b2o10]", length, temp, length, lwav);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, length+(temp*2))) != 0)
        return zt1428_err;
    
 	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;  
}
 

/* ========================================================================= */
/*  Function: Reset                                                          */
/*  Purpose:  This function resets the instrument.                           */
/* ========================================================================= */
int _VI_FUNC zt1428_reset (ViSession instrID)
{
 	int zt1428_err;
 	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
 
 	if ((zt1428_err = zt1428_write_data (instrID, "*RST", 4)) != 0)
  		return zt1428_err;
 
 	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;
}

/* ========================================================================= */
/*  Function: Device Clear                                                   */
/*  Purpose:  This function performs a low level clear of the interface.     */
/* ========================================================================= */
int _VI_FUNC zt1428_device_clear (ViSession instrID)
{
 
 	int zt1428_err;
 	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
  
 	zt1428_write_reg( instrID, 0xE, 0xFFFF );
 
 	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;
}


/* ========================================================================= */
/*  Function: Identification and Version Query                               */
/*  Purpose:  This function gets instrument id and driver version.           */
/* ========================================================================= */
int _VI_FUNC zt1428_id_version (ViSession instrID, char idn_string[],
                                double *driver_version)
{
 	int zt1428_err;
    char cmd[256];
 	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
 
 	if ((zt1428_err = zt1428_write_data (instrID, "*IDN?", 5)) != 0)
  		return zt1428_err;
 	if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
  		return zt1428_err;
    Scan (cmd, "%s>%s", idn_string); 
  
 	*driver_version = DRIVER_VER;
 
 	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;
}

/* ========================================================================= */
/*  Function: Self Test                                                      */
/*  Purpose:  This function performs a self test on the instrument.          */
/* ========================================================================= */
int _VI_FUNC zt1428_self_test (ViSession instrID, int *result)
{
    char cmd[256];
	int zt1428_err;
 	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
        
    if ((zt1428_err = zt1428_write_data (instrID, "*RST;:SUMM:PRES;:TEST:TALL", 26)) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_write_data (instrID, "SUMM:QUES:TEST?", 15)) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 1000)) != 0)
        return zt1428_err;
    Scan (cmd, "%s>%d[b4]", result);
  
  	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;  
}


/* ========================================================================= */
/*  Function: Run/Stop                                                       */
/*  Purpose:  This function starts or stops acquiring data.                  */
/* ========================================================================= */
int _VI_FUNC zt1428_run_stop (ViSession instrID, int state)
{
    int zt1428_err;
    char cmd[256];
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;

    if (state == ZT1428_RUN)
        Fmt (cmd, "%s<RUN");
    else
        Fmt (cmd, "%s<STOP");
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;
 	
 	zt1428_err = zt1428_get_error(instrID);

    return zt1428_err;
}


/* ========================================================================= */
/*  Function: Calibrate                                                      */
/*  Purpose:  This function performs calibration routines on the instrument. */
/* ========================================================================= */
int _VI_FUNC zt1428_calibrate (ViSession instrID, int *result)
{
	int zt1428_err;
    char cmd[256];
	
	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
    
 	if ((zt1428_err = zt1428_write_data (instrID, "SUMM:PRES;CAL:SCAL:VERT;BCAL", 28)) != 0)
  		return zt1428_err;
 
 	if ((zt1428_err = zt1428_write_data (instrID, "SUMM:QUES:CAL?", 14)) != 0)
  		return zt1428_err;
 
 	if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
  		return zt1428_err;
 	Scan (cmd, "%s>%d[b4]", result);
 
 	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;     
}


/* ========================================================================= */
/*  Function: Save / Recall State                                            */
/*  Purpose:  This function saves or recalls instrument settings             */
/* ========================================================================  */
int _VI_FUNC zt1428_save_recall (ViSession instrID, int setup, int state_number)
{
    int zt1428_err;
    char cmd[256];
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
 	
    if (setup == ZT1428_SAVE)
        Fmt (cmd, "%s<*SAV %d[b4]", state_number);
    else
        Fmt (cmd, "%s<*RCL %d[b4]", state_number);
    if ((zt1428_err = zt1428_write_data (instrID, cmd, NumFmtdBytes ())) != 0)
        return zt1428_err;

 	zt1428_err = zt1428_get_error(instrID);

    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Get Error                                                      */
/*  Purpose:  This function gets the first error message from the instrument,*/
/*  then clears any queued errors.                                           */
/* ========================================================================= */
int _VI_FUNC zt1428_error (ViSession instrID, int *instrument_error)
{
 
    int zt1428_err;
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
     	return zt1428_err;
 
 	zt1428_err = zt1428_get_error(instrID);
 	*instrument_error = zt1428_err;
 
 	return zt1428_err;
}
 
/* ========================================================================= */
/*  Function: Get Running                                                    */
/*  Purpose:  This function gets the instrument run/stop state.              */
/* ========================================================================= */
int _VI_FUNC zt1428_running (ViSession instrID, int *state)
{
 	int zt1428_err;
    char cmd[256];

 
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
     	return zt1428_err;
 
 	if ((zt1428_err = zt1428_write_data (instrID, "RUN?", 4)) != 0)
  		return zt1428_err;
 	if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
  		return zt1428_err;
 	Scan (cmd, "%s>%d[b4]", state);
 	
 	zt1428_err = zt1428_get_error(instrID);
 
 	return zt1428_err;
}
 

/* ========================================================================= */
/*  Function: Wait for Operation Complete                                    */
/*  Purpose:  This function waits until any currently running operations are */
/*    complete.                                   */
/* ========================================================================= */
int _VI_FUNC zt1428_wait_op_complete (ViSession instrID)
{
 	int zt1428_err;
    char cmd[256];
	
 	if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
  		return zt1428_err;
 
 	if ((zt1428_err = zt1428_write_data (instrID, "*OPC?", 5)) != 0)
  		return zt1428_err;
 	if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
  		return zt1428_err;
  
 	zt1428_err = zt1428_get_error(instrID);
    
 	return zt1428_err;
}


/*===========================================================================*/
/* Function: Close                                                           */
/* Purpose:  This function closes the instrument.                            */
/*===========================================================================*/
ViStatus _VI_FUNC zt1428_close (ViSession instrID)
{
    zt1428_instrRange instrPtr;
    ViSession rmSession;
    ViStatus zt1428_status = VI_SUCCESS;

    if ((zt1428_status = viGetAttribute (instrID, VI_ATTR_RM_SESSION, &rmSession)) < 0)
        return zt1428_status;
    if ((zt1428_status = viGetAttribute (instrID, VI_ATTR_USER_DATA, &instrPtr)) < 0)
        return zt1428_status;
            
    if (instrPtr != NULL) 
        free (instrPtr);
    
    zt1428_status = viClose (instrID);
    viClose (rmSession);

    return zt1428_status;
}
/* ========================================================================= */
/*  Function: Close                                                          */
/*  Purpose:  This function closes the instrument.                           */
/* ========================================================================= */
//int _VI_FUNC zt1428_close (ViSession instrID)
//{
//	int status;
//    ViSession rmSession;
//  	
//    if ((status = viGetAttribute (instrID, VI_ATTR_RM_SESSION, &rmSession)) < 0)
//        return status;
//
//    status = viClose (instrID);
//    viClose (rmSession);
//    
//    return status;
//}



/* ====== UTILITY ROUTINES ================================================= */
/* ========================================================================= */
/*  Function: Open Instrument                                                */
/*  Purpose:  This function locates and initializes an entry in the          */
/*            Instrument Table.  The variable platform equals 1 for VXI      */
/*            and 2 for GPIB. The size of the Instrument Table can be        */
/*            changed in the include file by altering the constant           */
/*            ZT1428_MAX_INSTR. The return value of this function is equal   */
/*            to the error variable.                                  */
/* ========================================================================= */
int zt1428_open_instr (ViRsrc resource_name, ViSession *id)
{
    int i;
    ViSession instrID;
    ViSession res_man_handle;
	int zt1428_err;

    instrID = 0;
	zt1428_err = 0;

    /*   Initialize the interface.   */
    if(viOpenDefaultRM (&res_man_handle) != 0)
	{
		zt1428_err = ZT1428_COM_ERR;
		return zt1428_err;
	}

	if(viOpen (res_man_handle, resource_name, VI_NULL, VI_NULL, &instrID) != 0)
	{
	   	zt1428_err = ZT1428_COM_ERR;
	   	return zt1428_err;
	}

    *id = instrID;
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Local Device Error                                             */
/*  Purpose:  Local error function.  This function gets the first error      */
/*  message from the instrument, then clears any queued errors.              */
/* ========================================================================= */
int zt1428_get_error (ViSession instrID)
{
	int i = 50;
	int temp;
	int error;
    int zt1428_err;
    char cmd[256];
    
    if ((zt1428_err = zt1428_device_closed (instrID)) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_write_data (instrID, "SYST:ERR?", 9)) != 0)
        return zt1428_err;
    if ((zt1428_err = zt1428_read_data (instrID, cmd, 100)) != 0)
        return zt1428_err;
    if (Scan (cmd, "%s>%d[b4]", &error) != 1)  
    {
	    zt1428_err = ZT1428_COM_ERR;
	    return zt1428_err;
    }
    temp = error;
    
    while((temp != 0) && (i > 0))	/* error queue is less than 50 entries */
    {
	  	zt1428_write_data (instrID, "SYST:ERR?", 9);
	  	zt1428_read_data (instrID, cmd, 100);
	  	Scan (cmd, "%s>%d[b4]", &temp); 	/* only keep first error in queue */
	  	i--;
 	}
    
    return error;
}

/* ========================================================================= */
/*  Function: Device Closed                                                  */
/*  Purpose:  This function checks to see if the instrument has been         */
/*            initialized. The return value is equal to the error     */
/*            variable.                                                      */
/* ========================================================================= */
int zt1428_device_closed (ViSession instrID)
{
	int zt1428_err;
    
    zt1428_err = 0;
    
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Read Data                                                      */
/*  Purpose:  This function reads a buffer of data from the instrument. The  */
/*            return value is equal to the error variable.            */
/* ========================================================================= */
int zt1428_read_data (ViSession instrID, char *buf, int count)
{
    unsigned long rtn_count;
    int status;
	int zt1428_err;

    zt1428_err = 0;
    
	status = viRead (instrID, (unsigned char *)buf, count, &rtn_count);
	if(status == 0x3FFF0006)
		status = 0;
    if(status != 0)
    	zt1428_err = status;
    	
    return zt1428_err;
}

/* ========================================================================= */
/*  Function: Write Data                                                     */
/*  Purpose:  This function writes a buffer of data to the instrument. The   */
/*            return value is equal to the error variable.            */
/* ========================================================================= */
int zt1428_write_data (ViSession instrID, char *buf, int count)
{
    unsigned long rtn_count;
    int status;
	int zt1428_err;
	
	zt1428_err = 0;
    
	status = viWrite (instrID, (unsigned char *)buf, count, &rtn_count);
	if(status != 0)
		zt1428_err = status;
    	
	return zt1428_err;
}

/* ========================================================================= */
/*  Function: Read Register                                                  */
/*  Purpose:  This function reads an instrument register.  The return value  */
/*            is equal to the error variable.                         */
/* ========================================================================= */
int zt1428_read_reg (ViSession instrID, int rel_addr, int *value)
{

    int status;
    unsigned short val;
	int zt1428_err;

	zt1428_err = 0;
    
    status = viIn16 (instrID, VI_A16_SPACE, rel_addr, &val);
	if(status != 0)
		zt1428_err = status;
	*value = val;
    	
	return zt1428_err;
}

/* ========================================================================= */
/*  Function: Write Register                                                 */
/*  Purpose:  This function writes an integer value to an instrument         */
/*            register.  The return value is equal to the error       */
/*            variable.                                                      */
/* ========================================================================= */
int zt1428_write_reg (ViSession instrID, int rel_addr, int value)
{

    int status;
	int zt1428_err;

	zt1428_err = 0;
    
    status = viOut16 (instrID, VI_A16_SPACE, rel_addr, value);
	if(status != 0)
		zt1428_err = status;
    	
	return zt1428_err;
}


/*===========================================================================*/
/* Function: Initialize Clean Up                                             */
/* Purpose:  This function is used only by the zt1428_init function.  When   */
/*           an error is detected this function is called to close the       */
/*           open resource manager and instrument object sessions and to     */
/*           set the instrID that is returned from zt1428_init to       */
/*           VI_NULL.                                                        */
/*===========================================================================*/
ViStatus zt1428_initCleanUp (ViSession openRMSession,
                    ViPSession openinstrID, ViStatus currentStatus)
{
    zt1428_instrRange instrPtr;
    
    if (viGetAttribute (*openinstrID, VI_ATTR_USER_DATA, &instrPtr) >= 0)
        if (instrPtr != NULL) 
            free (instrPtr);

    viClose (*openinstrID);
    viClose (openRMSession);
    *openinstrID = VI_NULL;
    
    return currentStatus;
}


