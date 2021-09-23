/*= Racal Instruments 3152a Arbitrary Waveform Generator ===================*/
/* VXI Plug&Play WIN Framework Instrument Driver                           */
/* Original Release: 12/18/95                                              */
/* By: S. Javed/D Masters                                                  */
/* Instrument Driver Revision 2.1                                          */
/* VXI Plug&Play WIN Framework Revision: 3.0                               */
/* 3152a Minimum Hardware Revision:                                        */
/* 3152a Minimum Firmware Revision: 1.0                                    */
/* Modification History: None                                              */
/* Modification History:                                                   */
/*      Rev    Date    Comment                                             */
/*      A	  7/13/01  Original Release                                    */
/*																		   */
/*      B	  5/06/03  Updated to fix the *IDN? and changed PLL		JDT    */
/*					   command to PHASE2 command						   */
/*=========================================================================*/

#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <visa.h>
#include <ctype.h>
#include "ri3152a.h"
#include <utility.h>


#define RI3152A_REVISION				"B"
#define RI3152A_MAX_CMD				((ViInt16) 255)
#define RI3152A_READ_LEN				((ViInt32) (RI3152A_MAX_CMD - 1))
#define RI3152A_MANF_ID				((ViAttrState) 0xFFB)
#define RI3152A_MODEL_CODE			((ViAttrState) 3152)

#define RI3152A_MAX_CMDS		100
#define RI3152A_MAX_SEQ			1024

#define RI3152A_MAX_INSTR_ERROR			(RI3152A_ERR(1))
#define RI3152A_NSUP_ATTR_VALUE			(RI3152A_ERR(2))
#define RI3152A_AMPLITUDE_ERROR			(RI3152A_ERR(3))
#define RI3152A_OFFSET_ERROR				(RI3152A_ERR(4))
#define RI3152A_AMPL_OFFSET_ERROR		(RI3152A_ERR(5))
#define RI3152A_INVALID_REPLY			(RI3152A_ERR(6))
#define RI3152A_PERCENT_ERROR1			(RI3152A_ERR(7))
#define RI3152A_PERCENT_ERROR2			(RI3152A_ERR(8))
#define RI3152A_CANT_OPEN_FILE			(RI3152A_ERR(9))
#define RI3152A_INVALID_FILE_DATA		(RI3152A_ERR(10))
#define RI3152A_BINARY_DOWNLOAD_FAILED	(RI3152A_ERR(11))
#define RI3152A_WAVECAD_FORMAT_1			(RI3152A_ERR(12))
#define RI3152A_WAVECAD_FORMAT_2			(RI3152A_ERR(13))
#define RI3152A_WAVECAD_FORMAT_3			(RI3152A_ERR(14))
#define RI3152A_WAVECAD_FORMAT_4			(RI3152A_ERR(15))
#define RI3152A_WAVECAD_FORMAT_5			(RI3152A_ERR(16))
#define RI3152A_WAVECAD_FORMAT_6			(RI3152A_ERR(17))
#define RI3152A_WAVECAD_FORMAT_7			(RI3152A_ERR(18))
#define RI3152A_WAVECAD_FORMAT_8			(RI3152A_ERR(19))
#define RI3152A_WAVECAD_FORMAT_9			(RI3152A_ERR(20))
#define RI3152A_WAVECAD_FORMAT_10		(RI3152A_ERR(21))
#define RI3152A_WAVECAD_FORMAT_11		(RI3152A_ERR(22))
#define RI3152A_PHASE_LOCK_NOT_SUPP		(RI3152A_ERR(23))
#define RI3152A_PHASE_MODE_CONFLICT		(RI3152A_ERR(24))
#define RI3152A_LOAD_BINARY_NOT_SUPP		(RI3152A_ERR(25))
#define RI3152A_BINARY_NOT_MULT4		(RI3152A_ERR(26))
#define RI3152A_OUT_OF_MEMORY			(RI3152A_ERR(27))

#define VI_ERROR_PARAMETER9		((ViStatus) (VI_ERROR_PARAMETER8 + 1))
#define VI_ERROR_PARAMETER10		((ViStatus) (VI_ERROR_PARAMETER9 + 1))

/*= LOCAL DATA TYPES =====================================================*/
/*
 * This structure contains information for forming a 3152a command from
 * the WaveCAD ".CAD" file
 */
typedef struct wavecad_cmd
	{
	int cmd_id;
	double value;
	char cmd_text[40];
	char cmd_param[50];
	} WaveCADCmdStruct;


/*= INTERNAL DATA =========================================================*/
/* readbuf is a buffer for VXI I/O strings */
static ViChar readbuf[RI3152A_MAX_CMD+1];
static ViSession initialized[RI3152A_MAX_INSTR] =
    {
    RI3152A_NO_SESSION, RI3152A_NO_SESSION, RI3152A_NO_SESSION,
    RI3152A_NO_SESSION, RI3152A_NO_SESSION, RI3152A_NO_SESSION,
    RI3152A_NO_SESSION, RI3152A_NO_SESSION, RI3152A_NO_SESSION,
    RI3152A_NO_SESSION, RI3152A_NO_SESSION, RI3152A_NO_SESSION
    };

static ViBoolean large_memory[RI3152A_MAX_INSTR] =
	{
	VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE,
	VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE
	};

/* used for checking the amplitude & offset ranges */
static ViBoolean tight_range_checking[RI3152A_MAX_INSTR];

/* used for restricting access to 3152-only operations */
static ViInt16 model_codes;
static ViBoolean fast_mode[RI3152A_MAX_INSTR] =
    {
    VI_FALSE, VI_FALSE, VI_FALSE,
    VI_FALSE, VI_FALSE, VI_FALSE,
    VI_FALSE, VI_FALSE, VI_FALSE,
    VI_FALSE, VI_FALSE, VI_FALSE
    };;

/* global file flag for ri3152a_get_next_data_point() function */
static int global_file_flag = 0;

/* Revision information (from SCCS) */
static ViChar *SCCS_id = "@(#) ri3152a.c 1.1 6/20/01";

/* used inside the ri3152a_load_wavecad_file function */
static WaveCADCmdStruct cmd_table[RI3152A_MAX_CMDS];
static ViInt32 segment_loops[RI3152A_MAX_SEQ];
static ViInt16 segment_numbers[RI3152A_MAX_SEQ];

/*= DEVICE DEPENDENT STRING ARRAYS ========================================*/

/*= INTERNAL FUNCTIONS ====================================================*/
static ViStatus ri3152a_real_range(ViReal64  val,ViReal64  min,ViReal64  max,ViStatus  err_code);
static ViStatus ri3152a_int_range(ViInt16  val,ViInt16  min,ViInt16  max,ViStatus  err_code);
static ViStatus ri3152a_int32_range(ViInt32  val,ViInt32  min,ViInt32  max,ViStatus  err_code);
static ViStatus ri3152a_boolean_range(ViBoolean  val,ViStatus  err_code);
static ViStatus ri3152a_read_value(ViSession vi, ViReal64 *val_ptr);
static ViStatus ri3152a_initCleanUp (ViSession rmSession, ViSession *vi,
                                       ViInt16 index, ViStatus currentStatus);
static ViInt32 ri3152a_get_mem_size(ViSession vi);
static ViStatus ri3152a_check_ampl_and_offset(ViSession vi, ViReal64 amplitude, ViReal64 offset);
static ViInt16 ri3152a_get_next_data_point(FILE *fp, ViInt32 *data_point);

static ViStatus ri3152a_parse_wavecad_state(WaveCADCmdStruct *table_entry, 
                                    		char *fileline);

static ViStatus ri3152a_parse_wavecad_cmd(WaveCADCmdStruct cmd_table[], 
                                  	int cmd_count, char *fileline);
static ViStatus ri3152a_parse_wavecad_seq(char *fileline, 
                                  	ViInt16 *link, 
                                  	ViInt16 *segment, 
                                  	ViInt32 *loop_count,
                                  	ViInt32 *segment_length,
                                  	char *wave_file_name);
static ViStatus ri3152a_create_path_name(char *wave_file_name, char *file_name);
static ViStatus ri3152a_output_wavecad_commands(ViSession vi, 
                                        	WaveCADCmdStruct cmd_table[], 
                                        	int cmd_count);
static ViStatus ri3152a_itoa (ViInt32 number_of_bytes, ViString temp_buf);

/*= DLL CODE ==============================================================*/
#if defined(WIN32) || defined(_WIN32) || defined(__WIN32__) || defined(__NT__)
/* This is required to produce a Win32 (95/NT) .DLL file */
#elif defined(_WINDOWS)
/* This is required to produce a Win16 .DLL file */
#include <windows.h>
#include <utility.h>
int _VI_FUNC
LibMain(/*Handle, WORD, WORD, LPSTR*/)
{
    return 1; /* signifies success */
}

int _VI_FUNC
WEP(int arg)
{
    return 1; /* signifies success */
}
#endif


/*=========================================================================*/
/* This function opens the instrument, queries for ID, and initializes the */
/* instrument to a known state.                                            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_init (ViRsrc    RsrcName,
                               ViBoolean id_query,
                               ViBoolean reset,
                               ViSession *vi)
{
    ViInt16 i;
    ViUInt32  cnt;
    ViUInt16 manf_ID, model_code;
    ViStatus error;
    ViSession rmSession;
    char *cp;
    double firmware_rev;

    /* Range check the parameters. */
    *vi = VI_NULL;
    error = ri3152a_int_range (id_query, 0, 1, VI_ERROR_PARAMETER2);
    if (error < 0) return error;
    error = ri3152a_int_range (reset, 0, 1, VI_ERROR_PARAMETER3);
    if (error < 0) return error;

    /* Initialize entry in Instrument Table and interface for instrument. */
    error = viOpenDefaultRM (&rmSession);
    if (error < 0) return error;
    error = viOpen (rmSession, RsrcName, VI_NULL, VI_NULL, vi);
    if (error < 0) return error;

    /* Find an empty position in the global data array. */
    i=0;
    while (i<RI3152A_MAX_INSTR && initialized[i] != RI3152A_NO_SESSION)
        i++;

    /* Did we find an empty spot?  If so, record the index in the User Data
           Attribute provided by VISA. */
    if (i < RI3152A_MAX_INSTR)
    {
        error = viSetAttribute (*vi, VI_ATTR_USER_DATA, (ViUInt32) i);

        /* If we get an error or a warning from the call to viSetAttribute,
                  then our index may not have been stored properly, so return an error. */
        if  (error != VI_SUCCESS)
        {
            if (error > 0)
                error = RI3152A_NSUP_ATTR_VALUE;
            return ri3152a_initCleanUp(rmSession, vi, i, error);
        }

        /* The call to viSetAttribute was OK, so mark this spot in the
               global data array. */
        initialized[i] = *vi;
    }

    /* If there was not an empty spot in the global array, return an error. */
    else
    {
        viClose(*vi);
        viClose(rmSession);
        *vi = VI_NULL;
        return RI3152A_MAX_INSTR_ERROR;
    }

/* -- Default Setup -------------------------------------------------------*/
    /* Set timeout to 10 seconds */
    error = viSetAttribute(*vi, VI_ATTR_TMO_VALUE, (ViUInt32)10000);
    if (error < 0)
        return (ri3152a_initCleanUp(rmSession, vi, i, error));

    /* Send END on the last byte of transfer */
    error = viSetAttribute(*vi, VI_ATTR_SEND_END_EN, (ViAttrState)VI_TRUE);
    if (error < 0)
        return (ri3152a_initCleanUp(rmSession, vi, i, error));

    /* Set the input/output buffers to 256 characters */
    error = viSetBuf (*vi, VI_READ_BUF|VI_WRITE_BUF, (ViUInt32) 256);
    if (error < 0)
        return (ri3152a_initCleanUp(rmSession, vi, i, error));

    /* Make sure the input buffer flushes after every viScanf call */
    error = viSetAttribute(*vi, VI_ATTR_RD_BUF_OPER_MODE, VI_FLUSH_ON_ACCESS);
    if (error < 0)
        return (ri3152a_initCleanUp(rmSession, vi, i, error));

	/* assume the tighter range checking is NOT performed, unless */
	/* the ID query is performed */
	tight_range_checking[i] = VI_FALSE;

    /* Perform the ID Query if requested */
    if (id_query)
    {
        /* Read the manufacturer ID */
        error = viGetAttribute (*vi, VI_ATTR_MANF_ID, &manf_ID);
        if (error < 0)
            return ri3152a_initCleanUp(rmSession, vi, i, error);

        /* Read the model code */
        error = viGetAttribute (*vi, VI_ATTR_MODEL_CODE, &model_code);
        if (error < 0)
            return ri3152a_initCleanUp(rmSession, vi, i, error);

        /* mask to leave 12 LSBs of model code */
        model_code &= 0xFFF;

        /* check for proper model code and manufacter ID */
        if ((model_code != RI3152A_MODEL_CODE) ||   (manf_ID != RI3152A_MANF_ID))
            return ri3152a_initCleanUp(rmSession, vi, i, VI_ERROR_FAIL_ID_QUERY);
            
    }
/*-- End of ID Query ------------------------------------------------------*/

	/* Set the instrument to normal mode */
    error = viPrintf (*vi, "INST:MODE NORM\n");
    error = viPrintf (*vi, "0150A0\n"); 
    /* Clear out any error */
    error = viPrintf (*vi, "*CLS\n"); 
    
	fast_mode[i] = VI_FALSE;

    /* Reset the instrument if requested. */
    if (reset)
    {
        error = viPrintf (*vi, "%s\n", (fast_mode[i]) ? "14200" : "*RST");
        if (error < 0)
            return ri3152a_initCleanUp(rmSession, vi, i, error);

		fast_mode[i] = VI_FALSE;            
    }


/*-- check for large (512K) or small (64K) memory installed */
	if ((error = viPrintf(*vi, "%s\n", (fast_mode[i]) ? "1507@?" : "*OPT?")) < 0)
	{
		return ri3152a_initCleanUp(rmSession, vi, i, error);
	}
	
	error = viRead(*vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt);
	if (error < 0)
		return ri3152a_initCleanUp(rmSession, vi, i, error);

	if (readbuf[0] == '2')
		large_memory[i] = VI_TRUE;
	else
		large_memory[i] = VI_FALSE;

	return error;
}



/*=========================================================================*/
/* This function generates a sine wave (standard waveform)                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sine_wave (ViSession vi,
                                    ViReal64 frequency,
                                    ViReal64 amplitude,
                                    ViReal64 offset,
                                    ViInt16 phase,
                                    ViInt16 powerSinex)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_SIN, RI3152A_MAX_FREQ_SIN,
                                    VI_ERROR_PARAMETER2))
        return( error );


	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    if (error = ri3152a_int_range (phase,RI3152A_MIN_PHASE , RI3152A_MAX_PHASE, VI_ERROR_PARAMETER5))
        return( error );

    if (error = ri3152a_int_range (powerSinex, RI3152A_POWER_1, RI3152A_POWER_9, VI_ERROR_PARAMETER6))
        return( error );

    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0301E" : ":SOUR:APPLY:SIN ";
    
    error = viPrintf(vi, "%s%.5f,%.4f,%.4f,%d,%d\n", cmd,
	                        frequency, amplitude, offset, phase, powerSinex);

    return( error );
}



/*=========================================================================*/
/* This function generates a triangular wave (standard waveform)           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_triangular_wave (ViSession vi,
                                          ViReal64 frequency,
                                          ViReal64 amplitude,
                                          ViReal64 offset,
                                          ViInt16 phase,
                                          ViInt16 powerTriangularx)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_TRI, RI3152A_MAX_FREQ_TRI,
                                      VI_ERROR_PARAMETER2))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    if (error = ri3152a_int_range (phase, RI3152A_MIN_PHASE, RI3152A_MAX_PHASE,
                                        VI_ERROR_PARAMETER5))
        return( error );

    if (error = ri3152a_int_range (powerTriangularx, RI3152A_POWER_1, RI3152A_POWER_9,
                                        VI_ERROR_PARAMETER6))
        return( error );

    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0302E" : ":SOUR:APPLY:TRI ";
    
    error = viPrintf(vi, "%s%.5f,%.4f,%.4f,%d,%d\n", cmd,
                    frequency, amplitude, offset, phase, powerTriangularx);

    return( error );
}
/*=========================================================================*/
/* This function generates a square wave (standard waveform)               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_square_wave (ViSession vi,
                                      ViReal64 frequency,
                                      ViReal64 amplitude,
                                      ViReal64 offset,
                                      ViInt16 duty_cycle)

{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;


    /* check parameter ranges */
    if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_SQU, RI3152A_MAX_FREQ_SQU,
                                    VI_ERROR_PARAMETER2))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    if (error = ri3152a_int_range (duty_cycle, RI3152A_MIN_DUTY_CYCLE , RI3152A_MAX_DUTY_CYCLE,
                                      VI_ERROR_PARAMETER5))
        return( error );

    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0303D" : ":SOUR:APPLY:SQU ";
    
    error = viPrintf(vi, "%s%.5f,%.4f,%.4f,%d\n", cmd,
                    frequency, amplitude, offset, duty_cycle);

    return( error );
}

/*=========================================================================*/
/* This function generates a pulse wave (standard waveform)                */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_pulse_wave (ViSession vi,
                                    ViReal64 frequency,
                                    ViReal64 amplitude,
                                    ViReal64 offset,
                                    ViReal64 delay,
                                    ViReal64 rise_time, 
                                    ViReal64 high_time, 
                                    ViReal64 fall_time)
{
    ViStatus error;
    ViReal64 max_percent_period;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_PULS, RI3152A_MAX_FREQ_PULS,
                                    VI_ERROR_PARAMETER2))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    max_percent_period = delay + rise_time + high_time + fall_time;

    if (max_percent_period > 100)
        return( RI3152A_PERCENT_ERROR1 );

    if (error = ri3152a_real_range (delay, RI3152A_MIN_DELAY_PULSE , RI3152A_MAX_DELAY_PULSE, VI_ERROR_PARAMETER5))
        return( error );

    if (error = ri3152a_real_range (rise_time, RI3152A_MIN_RISE_TIME_PULSE, RI3152A_MAX_RISE_TIME_PULSE,
            VI_ERROR_PARAMETER6))
        return( error );

    if (error = ri3152a_real_range (high_time, RI3152A_MIN_HIGH_TIME_PULSE, RI3152A_MAX_HIGH_TIME_PULSE,
            VI_ERROR_PARAMETER7))
        return( error );

    if (error = ri3152a_real_range (fall_time, RI3152A_MIN_FALL_TIME_PULSE, RI3152A_MAX_FALL_TIME_PULSE,
            VI_ERROR_PARAMETER8))
        return( error );
	
    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0304G" : ":SOUR:APPLY:PULS ";
    
    error = viPrintf(vi, "%s%.5f,%.4f,%.4f,%.4f,%4.1f,%4.1f,%4.1f\n", cmd,
                    frequency, amplitude, offset, delay, high_time, rise_time, fall_time);

    return( error );
}

/*=========================================================================*/
/* This function generates a ramp wave (standard waveform)                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_ramp_wave (ViSession vi,
                                    ViReal64 frequency,
                                    ViReal64 amplitude,
                                    ViReal64 offset,
                                    ViReal64 delay,
                                    ViReal64 rise_time, ViReal64 fall_time)
{
    ViStatus error;
    ViReal64 max_percent_period;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_RAMP, RI3152A_MAX_FREQ_RAMP,
                                    VI_ERROR_PARAMETER2))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    max_percent_period = delay + rise_time + fall_time;
    if (max_percent_period > 100)
        return( RI3152A_PERCENT_ERROR2 );

    if (error = ri3152a_real_range (delay, RI3152A_MIN_DELAY_RAMP , RI3152A_MAX_DELAY_RAMP, VI_ERROR_PARAMETER5))
        return( error );

    if (error = ri3152a_real_range (rise_time, RI3152A_MIN_RISE_TIME_RAMP, RI3152A_MAX_RISE_TIME_RAMP,
            VI_ERROR_PARAMETER6))
        return( error );

    if (error = ri3152a_real_range (fall_time, RI3152A_MIN_FALL_TIME_RAMP, RI3152A_MAX_FALL_TIME_RAMP,
            VI_ERROR_PARAMETER7))
        return( error );

    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0305F" : ":SOUR:APPLY:RAMP ";
    
    error = viPrintf(vi, "%s%.5f,%.4f,%.4f,%4.1f,%4.1f,%4.1f\n", cmd,
                    frequency, amplitude, offset, delay, rise_time, fall_time);

    return( error );
}

/*=========================================================================*/
/* This function generates a sinc wave (standard waveform)                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sinc_wave (ViSession vi,
                                    ViReal64 frequency,
                                    ViReal64 amplitude,
                                    ViReal64 offset,
                                    ViInt16 cycle_number)

{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_SINC, RI3152A_MAX_FREQ_SINC,
                                    VI_ERROR_PARAMETER2))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    if (error = ri3152a_int_range (cycle_number, RI3152A_MIN_CYCLE_NUMBER , RI3152A_MAX_CYCLE_NUMBER,
                VI_ERROR_PARAMETER5))
        return( error );

    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0310D" : ":SOUR:APPLY:SINC ";
    
	error = viPrintf(vi, "%s%.5f,%.4f,%.4f,%d\n", cmd,
                    frequency, amplitude, offset, cycle_number);

    return( error );
}


/*=========================================================================*/
/* This function generates a exponential wave (standard waveform)          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_exponential_wave (ViSession vi,
                                           ViReal64 frequency,
                                           ViReal64 amplitude,
                                           ViReal64 offset,
                                           ViInt16 exponent)

{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_EXP, RI3152A_MAX_FREQ_EXP,
                                    VI_ERROR_PARAMETER2))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    if (error = ri3152a_real_range (exponent, RI3152A_MIN_EXPONENT_EXP, RI3152A_MAX_EXPONENT_EXP,
                VI_ERROR_PARAMETER5))
        return( error );

	if (exponent == 0.0)
		return( VI_ERROR_PARAMETER5 );

    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0307D" : ":SOUR:APPLY:EXP ";
    
	error = viPrintf(vi, "%s%.5f,%.4f,%.4f,%d\n", cmd,
                    frequency, amplitude, offset, exponent);

    return( error );
}


/*=========================================================================*/
/* This function generates a gaussian wave (standard waveform)             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_gaussian_wave (ViSession vi,
                                        ViReal64 frequency,
                                        ViReal64 amplitude,
                                        ViReal64 offset,
                                        ViInt16 exponent)

{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_GAU, RI3152A_MAX_FREQ_GAU,
                                    VI_ERROR_PARAMETER2))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    if (error = ri3152a_int_range (exponent, RI3152A_MIN_EXPONENT_GAU, RI3152A_MAX_EXPONENT_GAU,
                VI_ERROR_PARAMETER5))
        return( error );

    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0306D" : ":SOUR:APPLY:GAUS ";
    
    error = viPrintf(vi, "%s%.5f,%.4f,%.4f,%d\n", cmd,
                    frequency, amplitude, offset, exponent);

    return( error );
}

/*=========================================================================*/
/* This function generates a DC signal (standard waveform)                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_dc_signal (ViSession vi,
                                    ViInt16 percent_amplitude)

{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int_range (percent_amplitude, RI3152A_MIN_PERCENT_AMP, RI3152A_MAX_PERCENT_AMP,
                                    VI_ERROR_PARAMETER2))
        return( error );


    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0308A" : ":SOUR:APPLY:DC ";
    
	error = viPrintf(vi, "%s%d\n", cmd, percent_amplitude);

    return( error );
}

/*=========================================================================*/
/* This function produces Amplitude Modulation by acquiring the Amplitude  */
/* percentage and the Internal Frequency.                                  */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_amplitude_modulation (ViSession vi,
                                    ViInt16 percent_amplitude, ViInt32 internal_frequency)

{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

	error = ri3152a_int_range (percent_amplitude, 
                                  RI3152A_MIN_AM_PERCENT, 
                                  RI3152A_MAX_AM_PERCENT,
                                  VI_ERROR_PARAMETER2);
	if (error < 0) return( error );

    error = ri3152a_int32_range (internal_frequency, RI3152A_MIN_AM_FREQ,
                                   RI3152A_MAX_AM_FREQ, VI_ERROR_PARAMETER3);
	if (error < 0) return( error );


    /* format the command and output it to the generator */
    cmd = (fast_mode[(ViInt16)index]) ? "0780A" : "AM ";
    if ((error = viPrintf(vi, "%s%d\n", cmd, percent_amplitude)) < 0)
    	return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0781A" : "AM:INT:FREQ ";
    if ((error =  viPrintf(vi, "%s%d\n", cmd, internal_frequency)) < 0)
    	return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "07820" : "AM:EXEC ";
    if ((error =  viPrintf(vi, "%s\n", cmd)) < 0)
    	return( error );

 	return( error );
}

/*=========================================================================*/
/* This function selects the Waveform mode                                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_select_waveform_mode  (ViSession vi,
                                          		ViInt16 waveform_type)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int_range (waveform_type, RI3152A_MODE_STD,
                                    RI3152A_MODE_SEQ, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */

    switch ( waveform_type )
		{
		case RI3152A_MODE_STD:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0610A0" : "FUNC:MODE FIX");
								break;

		case RI3152A_MODE_ARB:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0610A1" : "FUNC:MODE USER");
								break;
								
		case RI3152A_MODE_SWEEP:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0610A3" : "FUNC:MODE SWE");
								break;

		case RI3152A_MODE_SEQ:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0610A2" : "FUNC:MODE SEQ");
								break;
		}

	return( error );
}


/*=========================================================================*/
/* This function selects the Waveform mode                                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_select_standard_waveform  (ViSession vi,
                                          		ViInt16 waveform_type)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int_range (waveform_type, RI3152A_SINE,
                                    RI3152A_DC, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */

    switch ( waveform_type )
		{
		case RI3152A_SINE:		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A0" : "FUNC:SHAP SIN");
								break;

		case RI3152A_TRIANGLE:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A1" : "FUNC:SHAP TRI");
								break;

		case RI3152A_SQUARE:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A2" : "FUNC:SHAP SQU");
								break;

		case RI3152A_PULSE:		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A3" : "FUNC:SHAP PULS");
								break;

		case RI3152A_RAMP:		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A4" : "FUNC:SHAP RAMP");
								break;

		case RI3152A_SINC:		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A5" : "FUNC:SHAP SINC");
								break;

		case RI3152A_GAUSSIAN:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A6" : "FUNC:SHAP GAUS");
								break;

		case RI3152A_EXPONENTIAL:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A7" : "FUNC:SHAP EXP");
									break;

		case RI3152A_DC:		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A8" : "FUNC:SHAP DC");
								break;

		}

	return( error );
}

/*=========================================================================*/
/* This function selects the operation mode                                */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_operating_mode  (ViSession vi,
                                          ViInt16 operating_mode)
{
    ViStatus error;
	ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int_range (operating_mode, RI3152A_MODE_CONT,
                                    RI3152A_MODE_BURST, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */

	switch ( operating_mode )
		{
		case RI3152A_MODE_CONT:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1110A1" : "INIT:CONT ON");
								break;

		case RI3152A_MODE_TRIG:	cmd = (fast_mode[(ViInt16)index]) ? "1110A0;1150A0;1120A0" : "INIT:CONT OFF;:TRIG:GATE OFF;:TRIG:BURST OFF";
								error = viPrintf(vi, "%s\n", cmd);
								break;

		case RI3152A_MODE_GATED:cmd = (fast_mode[(ViInt16)index]) ? "1110A0;1120A0;1150A1" : "INIT:CONT OFF;:TRIG:BURST OFF;:TRIG:GATE ON";	
								error = viPrintf(vi, "%s\n", cmd);
								break;

		case RI3152A_MODE_BURST:cmd = (fast_mode[(ViInt16)index]) ? "1110A0;1150A0;1120A1" : "INIT:CONT OFF;:TRIG:GATE OFF;:TRIG:BURST ON";		
								error = viPrintf(vi, "%s\n", cmd);
								break;
		}

	return( error );
}

/*=========================================================================*/
/* Function: Query Operating Mode                                          */
/* Description: This function reads the presently selected operating mode  */
/* Parameters: vi -- instrument handle                                     */
/*     		 : operating_mode -- the operating mode to select              */
/*=========================================================================*/

ViStatus _VI_FUNC ri3152a_query_operating_mode (ViSession vi, ViPInt16 operating_mode)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
  

	/* if we are in continuous trigger mode, then the operating mode is continuous */
	if (fast_mode[(ViInt16)index])
		error = viPrintf(vi, "1110A?\n");
	else			
		error = viPrintf(vi, "INIT:CONT?\n");
		
	if (error < 0) return (error);		
	
	/* read the reply back from the instrument */
	if ((error = viRead(vi, (ViBuf) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (readbuf[0] == '1')
		*operating_mode = RI3152A_MODE_CONT;
	else {
		/* we are not in continuous trigger mode, now we must check other items */
		
		/* check gate mode and burst mode */
		if (fast_mode[(ViInt16)index])
			error = viPrintf(vi, "1150A?;1120A?\n");
		else
			error = viPrintf(vi, "TRIG:GATE?;:TRIG:BURST?\n");
			
		if (error < 0) return( error );
	
		/* read the reply back from the instrument */
		if ((error = viRead(vi, (ViBuf) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
			return( error );
		
		/* null-byte terminate the reply */
		readbuf[(ViInt16) cnt] = 0;
		
		if (!strncmp(readbuf, "0;0", 3))
			*operating_mode = RI3152A_MODE_TRIG;
		else if (!strncmp(readbuf, "1;0", 3))
			*operating_mode = RI3152A_MODE_GATED;
		else if (!strncmp(readbuf, "0;1", 3))
			*operating_mode = RI3152A_MODE_BURST;
		else
			return( RI3152A_INVALID_REPLY );
	}

	return( error );
}


/*=========================================================================*/
/* This function programs the frequency for the present waveform           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_frequency (ViSession vi, ViReal64 frequency)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_real_range (frequency, RI3152A_MIN_FREQ_SIN, 
                                              RI3152A_MAX_FREQ_SIN,
                                              VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0400A" : "FREQ ";
    
	error = viPrintf (vi, "%s%.5f\n", cmd, frequency);
		
	return (error);
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_frequency_query (ViSession vi,
										ViReal64 *frequency)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0400A?" : "FREQ?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *frequency = strtod(readbuf, NULL);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function programs the frequency for the present waveform           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_amplitude (ViSession vi, ViReal64 amplitude)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	error = ri3152a_real_range (amplitude, RI3152A_MIN_AMPLITUDE, 
                                              RI3152A_MAX_AMPLITUDE,
                                              VI_ERROR_PARAMETER2);
	if (error < 0) return( error );
		
    cmd = (fast_mode[(ViInt16)index]) ? "0500A" : "VOLT ";

	return( viPrintf(vi, "%s%.4f\n", cmd, amplitude) );
}

/*=========================================================================*/
/* This function programs the frequency for the present waveform           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_offset (ViSession vi, ViReal64 offset)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_real_range (offset, RI3152A_MIN_OFFSET, 
                                           RI3152A_MAX_OFFSET,
                                           VI_ERROR_PARAMETER2))
		return( error );
		
    cmd = (fast_mode[(ViInt16)index]) ? "0501A" : "VOLT:OFFSET ";

	return( viPrintf(vi, "%s%.4f\n", cmd, offset) );
}

/*=========================================================================*/
/* This function selects the source of the trigger                         */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_source (ViSession vi, ViInt16 trigger_source)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_int_range (trigger_source, RI3152A_TRIGGER_INTERNAL,
                                    RI3152A_TRIGGER_TTLTRG7, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	switch (trigger_source  )
		{
		case RI3152A_TRIGGER_INTERNAL:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1140A3" : "TRIG:SOUR:ADV INT");
			break;
		
		case RI3152A_TRIGGER_EXTERNAL:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1140A4" : "TRIG:SOUR:ADV EXT");
			break;

		case RI3152A_TRIGGER_TTLTRG0:
		case RI3152A_TRIGGER_TTLTRG1:
		case RI3152A_TRIGGER_TTLTRG2:
		case RI3152A_TRIGGER_TTLTRG3:
		case RI3152A_TRIGGER_TTLTRG4:
		case RI3152A_TRIGGER_TTLTRG5:
		case RI3152A_TRIGGER_TTLTRG6:
		case RI3152A_TRIGGER_TTLTRG7:	
			if (fast_mode[(ViInt16)index])
				error = viPrintf (vi, "1140A%d\n", trigger_source + 6);
			else
				error = viPrintf(vi, "TRIG:SOUR:ADV TTLTRG%d\n", 
								  trigger_source - RI3152A_TRIGGER_TTLTRG0);
			break;
		}
    return( error );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_source_adv_query (ViSession vi,
										ViPInt16 trig_source)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1140A?" : "TRIG:SOUR:ADV?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (!(strncmp(readbuf, "INT", 3))) *trig_source = 0;	/* INT */
	else if (!(strncmp(readbuf, "EXT", 3))) *trig_source = 1;
	else if (!(strncmp(readbuf, "TTLT0", 5))) *trig_source = 2;	/* TTLT0 */
	else if (!(strncmp(readbuf, "TTLT1", 5))) *trig_source = 3;
	else if (!(strncmp(readbuf, "TTLT2", 5))) *trig_source = 4;
	else if (!(strncmp(readbuf, "TTLT3", 5))) *trig_source = 5;
	else if (!(strncmp(readbuf, "TTLT4", 5))) *trig_source = 6;
	else if (!(strncmp(readbuf, "TTLT5", 5))) *trig_source = 7;
	else if (!(strncmp(readbuf, "TTLT6", 5))) *trig_source = 8;
	else if (!(strncmp(readbuf, "TTLT7", 5))) *trig_source = 9;
	else return( RI3152A_INVALID_REPLY );
	
	return (VI_SUCCESS);
}




/*=========================================================================*/
/* This function programs the trigger rate for the internal trigger        */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_rate (ViSession vi, ViReal64 triggerRate)
{
	ViStatus error;
	ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	error = ri3152a_real_range(triggerRate, RI3152A_TRIGGER_RATE_MIN,
	                          RI3152A_TRIGGER_RATE_MAX, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "1170A" : "TRIG:TIM ";
    
	return( viPrintf(vi, "%s%f\n", cmd, triggerRate) );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_rate_query (ViSession vi,
										ViReal64 *triggerRate)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1170A?" : "TRIG:TIM?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *triggerRate = strtod(readbuf, NULL);
	
	return (VI_SUCCESS);
}




/*=========================================================================*/
/* This function programs the trigger rate for the internal trigger        */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_trigger_level (ViSession vi, ViReal64 triggerLevel)
{
	ViStatus error;
	ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	error = ri3152a_real_range(triggerLevel, RI3152A_TRIGGER_LEVEL_MIN,
	                          RI3152A_TRIGGER_LEVEL_MAX, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "1180A" : "TRIG:LEV ";
    
	return( viPrintf(vi, "%s%f\n", cmd, triggerLevel) );
}


/*=========================================================================*/
/* This function returns the value of the current Voltage, Offset and      */
/* Frequency value as well as the status and type of the Filter assigned   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_level_query (ViSession vi, ViReal64* trigger_level_value)
{
    ViStatus error;
    ViUInt32 cnt;
    ViInt16 filter_state;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;


    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1180A?" : "TRIG:LEV?");
    if (error < 0)
        return (error);


  /*   read the reply back from the instrument */

    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );


    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;
    *trigger_level_value = strtod(readbuf, NULL);

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* This function selects the slope of the trigger for the external trigger */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_slope (ViSession vi, ViBoolean triggerSlope)
{
	ViStatus error;
	ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	error = ri3152a_boolean_range(triggerSlope, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );
		
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "1160A%s\n", (triggerSlope) ? "1" : "0");
	else
       error = viPrintf(vi, "TRIG:SLOP %s\n", (triggerSlope) ? "POS" : "NEG");
    
	return( error );
}								 
	


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_slope_query (ViSession vi,
										ViPInt16 trig_slope)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1160A?" : "TRIG:SLOP?");

	/* read the reply back from the instrument */								
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (!(strncmp(readbuf, "POS", 3))) *trig_slope = 1;  	/* POS */
	else if (!(strncmp(readbuf, "NEG", 3))) *trig_slope = 0;
	else return( RI3152A_INVALID_REPLY );
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function selects the trigger delay number of triggers till action  */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_delay (ViSession vi, ViInt32 triggerDelay)
{
	ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	if (triggerDelay != 0)
		{
		error = ri3152a_real_range(triggerDelay, RI3152A_TRIGGER_DELAY_MIN,
								RI3152A_TRIGGER_DELAY_MAX, VI_ERROR_PARAMETER2);
		if (error < 0)
			return( error );
			
		/* odd number of triggers is not allowed */
		if (error & 0x1)
			return( VI_ERROR_PARAMETER2 );
		}
		
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "1131A%ld\n", triggerDelay);
	else
       error = viPrintf(vi, "TRIG:DEL %ld\n", triggerDelay);
		
	return(error);
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_delay_query (ViSession vi,
										ViPInt32 delay)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1131A?" : "TRIG:DEL?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*delay = strtol (readbuf, NULL, 10);
	
	return (VI_SUCCESS);
}



/*=========================================================================*/
/* This function enables or disables the phase lock loop mode for a 3152   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trig_delay_state (ViSession vi, ViBoolean on_off)
{
	ViStatus error;
	ViInt32 long_index;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    if (error = ri3152a_boolean_range (on_off, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "1130A%s\n", (on_off) ? "1" : "0");
	else
       error = viPrintf(vi, "TRIG:DEL:STAT %s\n", (on_off) ? "ON" : "OFF");

	return( error );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trig_delay_state_query (ViSession vi,
										ViPInt16 delay_state)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1130A?" : "TRIG:DEL:STAT?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*delay_state = atoi (readbuf);
	
	return (VI_SUCCESS);
}



/*=========================================================================*/
/* This function selects the output trigger source, TTLTRG line, trigger   */
/* point, and segment in which the trigger is output                       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_trigger (ViSession vi, 
                                         ViInt16 outputTriggerSource,
                                         ViInt16 outputTriggerLine, 
                                         ViInt32 BITTriggerPoint,
                                         ViInt16 LCOMSegment,
                                         ViInt16 outputWidth)
{
	ViStatus error;
	ViInt32 max_points;
	ViInt16 i;
	ViChar *param;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	error = ri3152a_int_range(outputTriggerSource, RI3152A_TRIGGER_INTERNAL,
								RI3152A_TRIGGER_LCOM, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );
		
	error = ri3152a_int_range(outputTriggerLine, RI3152A_TRIGGER_TTLTRG0,
								RI3152A_TRIGGER_NONE, VI_ERROR_PARAMETER3);
	if (error < 0)
		return( error );

	max_points = ri3152a_get_mem_size(vi) - 1;
	
	error = ri3152a_int32_range(BITTriggerPoint, 2, max_points,
							   VI_ERROR_PARAMETER4);
	if (error < 0)
		return( error );

	/*
	 * BIT Trigger point must be even number
	 */
	if (BITTriggerPoint % 2)
		return( VI_ERROR_PARAMETER4 );



	error = ri3152a_int_range(LCOMSegment, RI3152A_MIN_SEG_NUMBER,
								RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER5);
	if (error < 0)
		return( error );

	error = ri3152a_int_range(outputWidth, RI3152A_MIN_WIDTH,
								RI3152A_MAX_WIDTH, VI_ERROR_PARAMETER6);
	if (error < 0)
		return( error );

	switch( outputTriggerSource )
		{
		case RI3152A_TRIGGER_INTERNAL:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0220A3" : "OUTPUT:TRIG:SOURCE INT");
			break;
		
		case RI3152A_TRIGGER_EXTERNAL:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0220A4" : "OUTPUT:TRIG:SOURCE EXT");
			break;
		
		case RI3152A_TRIGGER_BIT:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0220A1" : "OUTPUT:TRIG:SOURCE BIT");
			break;

		case RI3152A_TRIGGER_LCOM:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0220A2" : "OUTPUT:TRIG:SOURCE LCOM");
			break;
		}

	if (error < 0)
		return( error );

	if (outputTriggerLine == RI3152A_TRIGGER_NONE)
	{
		for (i = 0;  i < 8;  ++i)
		{
			if (fast_mode[(ViInt16)index])
				error = viPrintf(vi, "0230B%d,0\n", i); 
			else				
				error = viPrintf(vi, "OUTP:TTLTRG%d OFF\n", i); 

			if (error < 0)
				return( error );
		}
			
		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0210B0,0" : "OUTPUT:ECLTRG0 OFF");
		if (error < 0) return( error );				 
	}
	else
	{
		if (outputTriggerLine == RI3152A_TRIGGER_ECLTRG0)
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0210B0,1" : "OUTPUT:ECLTRG0 ON");
		else
		{
			if (fast_mode[(ViInt16)index])
				error = viPrintf(vi, "0230B%d,1\n", 
					outputTriggerLine - RI3152A_TRIGGER_TTLTRG0);
			else
				error = viPrintf(vi, "OUTPUT:TTLTRG%d ON\n", 
					outputTriggerLine - RI3152A_TRIGGER_TTLTRG0);
		}	
			
		if (error < 0)
			return( error );
	}


	if (outputTriggerSource == RI3152A_TRIGGER_BIT
	||  outputTriggerSource == RI3152A_TRIGGER_LCOM)
	{
		error = viPrintf(vi, "%s%ld\n", (fast_mode[(ViInt16)index]) ? "0242A" : "OUTPUT:SYNC:POS ", BITTriggerPoint);
		if (error < 0)
			return( error );
	}
	
		
	error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "0243A" : "OUTP:SYNC:WIDT ", outputWidth);
	if (error < 0)
		return( error );

	if (outputTriggerSource == RI3152A_TRIGGER_LCOM)
	{
		error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1030A" : "TRACE:SEL ", LCOMSegment);
		if (error < 0)
			return( error );
	}
		

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger_source_query (ViSession vi,
										ViString trig_sour)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0220A?" : "OUTP:TRIG:SOUR?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt-2] = 0;

	strcpy (trig_sour, readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function selects the output sync position                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_sync_pos (ViSession vi, ViInt32 syncPos)
{
	ViInt32 max_points;
	ViChar *cmd;
	ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	
	max_points = ri3152a_get_mem_size(vi) - 1;
	
	error = ri3152a_int32_range(syncPos, 0, max_points,
							   VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );
	/*
	 * SYNC Position must be even number
	 */
	if (syncPos % 2)
		return( VI_ERROR_PARAMETER2 );
		
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "0242A%ld\n", syncPos);
	else
       error = viPrintf(vi, "OUTP:SYNC:POS %ld\n", syncPos);
		
	return(error);
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sync_pos_query (ViSession vi,
										ViInt32 *pos)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0242A?" : "OUTP:SYNC:POS?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*pos = strtol (readbuf, NULL, 10);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function selects the output sync width                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_sync_width (ViSession vi, ViInt32 syncWidth)
{
	ViInt32 max_points;
	ViChar *cmd;
	ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	error = ri3152a_int_range(syncWidth, RI3152A_MIN_WIDTH,
								RI3152A_MAX_WIDTH, VI_ERROR_PARAMETER6);
	if (error < 0)
		return( error );
	
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "0243A%ld\n", syncWidth);
	else
       error = viPrintf(vi, "OUTP:SYNC:WIDT %ld\n", syncWidth);
		
	return(error);
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sync_width_query (ViSession vi,
										ViInt32 *width)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0243A?" : "OUTP:SYNC:WIDT?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*width = strtol (readbuf, NULL, 10);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function programs the SYNC output pulse                            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_sync (ViSession vi, 
									  ViInt16 SYNCPulseSource,
                                      ViInt32 BITSYNCPoint,
                                      ViInt16 outputWidth,
                                      ViInt16 LCOMSegment)
{
	ViInt32 max_points;
	ViChar *cmd;
	ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	error = ri3152a_int_range(SYNCPulseSource, RI3152A_SYNC_OFF,
								RI3152A_SYNC_PULSE, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );
		

	max_points = ri3152a_get_mem_size(vi) - 1;
	
	error = ri3152a_int32_range(BITSYNCPoint, 0, max_points,
							   VI_ERROR_PARAMETER3);
	if (error < 0)
		return( error );


	/*
	 * SYNC Point must be even number
	 */
	if (BITSYNCPoint % 2)
		return( VI_ERROR_PARAMETER3 );


	error = ri3152a_int_range(outputWidth, RI3152A_MIN_WIDTH,
								RI3152A_MAX_WIDTH, VI_ERROR_PARAMETER4);
	if (error < 0)
		return( error );
		
	error = ri3152a_int_range(LCOMSegment, RI3152A_MIN_SEG_NUMBER,
								RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER5);
	if (error < 0)
		return( error );


	switch( SYNCPulseSource )
	{
		case RI3152A_SYNC_OFF:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0240A0" : "OUTPUT:SYNC OFF");
			break;
		
		case RI3152A_SYNC_BIT:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0241A0;0240A1" : "OUTPUT:SYNC:SOURCE BIT;:OUTPUT:SYNC ON");
			break;
		
		case RI3152A_SYNC_LCOM:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0241A1;0240A1" : "OUTPUT:SYNC:SOURCE LCOM;:OUTPUT:SYNC ON");
			break;

		case RI3152A_SYNC_SSYNC:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0241A2;0240A1" : "OUTPUT:SYNC:SOURCE SSYNC;:OUTPUT:SYNC ON");
			break;

		case RI3152A_SYNC_HCLOCK:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0241A3;0240A1" : "OUTPUT:SYNC:SOURCE HCLOCK;:OUTPUT:SYNC ON");
			break;
			
		case RI3152A_SYNC_PULSE:
			error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0241A4;0240A1" : "OUTPUT:SYNC:SOURCE PULSE;:OUTPUT:SYNC ON");
			break;
	}

	if (error < 0)
		return( error );


	if (SYNCPulseSource == RI3152A_SYNC_BIT
	||  SYNCPulseSource == RI3152A_SYNC_LCOM)
	{
		error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "0242A" : "OUTPUT:SYNC:POS ", BITSYNCPoint);
		if (error < 0)
			return( error );
	}
	
	error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "0243A" : "OUTPUT:SYNC:WIDT ", outputWidth);
	if (error < 0)
		return( error );
	
	if (SYNCPulseSource == RI3152A_SYNC_LCOM)
	{
		error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1030A" : "TRACE:SEL ", LCOMSegment);
		if (error < 0)
			return( error );
	}

	return( VI_SUCCESS );
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_sync_query (ViSession vi,
										ViInt16 *sync_state)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0240A?" : "OUTP:SYNC?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	if (readbuf[0] == '0')
		*sync_state = 0;
	else		
		*sync_state = 1;
	
	return (VI_SUCCESS);
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sync_source_query (ViSession vi,
										ViString source)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0241A?" : "OUTP:SYNC:SOUR?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt-2] = 0;

	strcpy (source, readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function sends an immediate trigger ("soft trigger") to the Arb    */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_immediate_trigger (ViSession vi)
{
    ViAttrState index;
    ViStatus error;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    return( viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "11000" : "*TRG") );
}


/*=========================================================================*/
/* This function selects the 3152a as the phase master unit                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_phase_lock_null (ViSession vi)
{
	ViChar *cmd;
    ViAttrState index;
    ViStatus error;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
    cmd = (fast_mode[(ViInt16)index]) ? "08500" : "PHASE:LOCK:NULL";
    
	return( viPrintf(vi, "%s\n", cmd) );
}

/*=========================================================================*/
/* This function selects the 3152a as the phase master unit                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_phase_master (ViSession vi)
{
	ViChar *cmd;
    ViAttrState index;
    ViStatus error;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
    cmd = (fast_mode[(ViInt16)index]) ? "0820A1;0800A1" : "PHASE:LOCK ON;:PHASE:SOURCE MASTER";
    
	return( viPrintf(vi, "%s\n", cmd) );
}

							
/*=========================================================================*/
/* This function selects the 3152a as the phase master unit                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_phase_slave (ViSession vi, ViInt16 phaseOffset)
{
	ViStatus error;
	ViChar *cmd, *temp;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = ri3152a_int_range(phaseOffset, RI3152A_MIN_PHASE, RI3152A_MAX_PHASE,
								VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );
		
    cmd = (fast_mode[(ViInt16)index]) ? "0820A0" : ":PHAS:LOCK OFF";
	error = viPrintf(vi, "%s\n",cmd);
	if ( error < 0) return error;

	if (fast_mode[(ViInt16)index])
		error = viPrintf(vi, "0810A%d\n", phaseOffset);
	else		
		error = viPrintf(vi, ":PHAS:ADJ %d\n", phaseOffset);
		
	if (error < 0) return (error);

    cmd = (fast_mode[(ViInt16)index]) ? "0820A1" : ":PHAS:LOCK ON";
	error = viPrintf(vi, "%s\n",cmd);
	if (error < 0) return (error);

    cmd = (fast_mode[(ViInt16)index]) ? "0800A2" : ":PHAS:SOUR SLAV";
	error = viPrintf(vi, "%s\n",cmd);
	
	return( error );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_phase_query (ViSession vi,
										ViPInt16 phase_offset, ViPInt16 phase_state,
										ViPInt16 phase_source)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0810A?" : "PHAS:ADJ?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *phase_offset = strtod(readbuf, NULL);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0820A?" : "PHAS:LOCK?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*phase_state = atoi (readbuf);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0800A?" : "PHAS:SOUR?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (!(strncmp(readbuf, "MAS", 3))) *phase_source = 0;		/* Master */
	else if (!(strncmp(readbuf, "SLA", 3))) *phase_source = 1;	/* Slave */
	else return( RI3152A_INVALID_REPLY );
	
	return (VI_SUCCESS);
}



/*=========================================================================*/
/* This function enables or disables the phase lock loop mode for a 3152   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_phase_lock_loop (ViSession vi, ViBoolean on_off)
{
	ViStatus error;
	ViInt32 long_index;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    if (error = ri3152a_boolean_range (on_off, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "0920A%s\n", (on_off) ? "1" : "0");
	else
       error = viPrintf(vi, "PHASE2:LOCK %s\n", (on_off) ? "ON" : "OFF");

	return( error );
}

/*=========================================================================*/
/* This function sets the phase for a phase locked 3152.  When arbitrary   */
/* waveform mode is active, this sets only the coarse phase.  When standard*/
/* waveform mode is active, this programs both coarse and fine phase       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_pll_phase (ViSession vi, ViReal64 phase)
{
	ViStatus error;
	ViInt16 waveform_mode, standard_waveform;
	ViInt32 long_index;
	ViReal64 frequency, dont_care;
	ViInt16 idiscard;
	ViReal64 coarse_phase, fine_phase, resolution;
	ViInt32 num_points;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	

    if (error = ri3152a_real_range (phase, RI3152A_PLL_MIN_PHASE, RI3152A_PLL_MAX_PHASE,
                                      VI_ERROR_PARAMETER2))
        return( error );

	/* check the mode/standard waveform to check for fine phase adjust */
	error = ri3152a_mode_query(vi, &waveform_mode, &standard_waveform);
	if (error < 0)
		return( error );
		
	/* if we are in sequence mode, phase adjust doesn't work */
	if (waveform_mode == RI3152A_MODE_SEQ)
		return( RI3152A_PHASE_MODE_CONFLICT );
		
	/* we must be in standard waveform mode, get the standard waveform */
	/* and the frequency, and compute the number of points */
	error = ri3152a_status_query (vi, &dont_care, &frequency, &dont_care, &idiscard);
	if (error < 0)
		return( error );

	switch( standard_waveform )
		{
		case RI3152A_SINE:
		case RI3152A_SQUARE:
			if (frequency <= 200.0E3)
				num_points = 500;
			else if (frequency <= 10.0E6)
				num_points = (ViInt32) (100.0E6 / frequency);
			else
				num_points = 10;
			break;
			
		case RI3152A_RAMP:
		case RI3152A_PULSE:
		case RI3152A_GAUSSIAN:
		case RI3152A_EXPONENTIAL:
			if (frequency < 100.0E3)
				num_points = 1000;
			else
				num_points = (ViInt32) (100.0E6 / frequency);
			break;

		case RI3152A_TRIANGLE:
		case RI3152A_SINC:
			if (frequency <= 200.0E3)
				num_points = 500;
			else
				num_points = (ViInt32) (200.0E3 / frequency);
			break;
			
		default:
			num_points = 10;
		}


	/* minimum number of points is always (in effect) 10 */
	if (num_points < 10)
		num_points = 10;

	/* calculate the resolution based on the number of points */
	resolution = 360.0 / num_points;
	
	coarse_phase = (ViReal64) (((ViInt32) (phase / resolution)) * resolution);
	fine_phase = phase - coarse_phase;

	if (fast_mode[(ViInt16)index])
	{
		/* if we are in arbitrary mode, just do coarse phase adjust */
		if (waveform_mode == RI3152A_MODE_ARB)
			return( viPrintf(vi, "0900A%.2lf", phase) );
			
 		error = viPrintf(vi, "0900A%.2lf;0910A%.2lf\n", 
								coarse_phase, fine_phase);
	}
	else
	{
		/* if we are in arbitrary mode, just do coarse phase adjust */
		if (waveform_mode == RI3152A_MODE_ARB)
			return( viPrintf(vi, "PHASE2:ADJ %.2lf", phase) );
			
 		error = viPrintf(vi, ":PHASE2:ADJ %.2lf;:PHASE2:FINE %.2lf\n", 
								coarse_phase, fine_phase);
	}
		
	return(error);
}
		
/*=========================================================================*/
/* This function sets the fine phase for a phase locked 3152.  This would  */
/* be used when the arbitrary waveform mode is active, or to override the  */
/* fine phase adjustment made for the standard waveform mode               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_pll_fine_phase (ViSession vi, ViReal64 phase)
{
	ViStatus error;
	ViInt32 long_index;
	ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	

    if (error = ri3152a_real_range (phase, RI3152A_MIN_FINE_PHASE, RI3152A_MAX_FINE_PHASE,
                                      VI_ERROR_PARAMETER2))
        return( error );
	
    cmd = (fast_mode[(ViInt16)index]) ? "0910A" : "PHASE2:FINE ";
    
	return( viPrintf(vi, "%s%.2lf\n", cmd, phase) );
}


/*=========================================================================*/
/* This function allows the user to select various features of the         */
/* Burst Mode                                                              */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_burst_mode  (ViSession vi,
                                      ViInt32 number_of_cycles)

{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int32_range (number_of_cycles, RI3152A_BURST_MIN_CYCLE,
                                    RI3152A_BURST_MAX_CYCLE, VI_ERROR_PARAMETER2))
        return( error );

	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "1110A0;1121A%ld;1150A0;1120A1\n", number_of_cycles);
	else
       error = viPrintf(vi, "INIT:CONT OFF;:TRIG:BURST:COUNT %ld;:TRIG:GATE OFF;:TRIG:BURST ON\n", number_of_cycles);

    if (error <0)
       return (error);


    return( error );
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_burst_count_query (ViSession vi,
										ViPInt32 count)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1121A?" : "TRIG:BURS:COUN?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*count = strtol (readbuf, NULL, 10);
	
	return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function selects a LP Filter or turns the filter off               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_filter    (ViSession vi,
                                    ViInt16 lp_filter)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int_range (lp_filter, RI3152A_FILTER_OFF,
                                    RI3152A_FILTER_50MHZ, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */

	if (fast_mode[(ViInt16)index])
	{
		switch ( lp_filter )
			{
			case  RI3152A_FILTER_OFF:   error = viPrintf(vi, "0202A1\n");
	                                   break;

			case RI3152A_FILTER_20MHZ:  error = viPrintf(vi, "0202A0\n");
	                                   error = viPrintf(vi, "0201A1\n");
	                                   break;

			case RI3152A_FILTER_25MHZ:  error = viPrintf(vi, "0202A0\n");
	                                   error = viPrintf(vi, "0201A2\n");
	                                   break;

			case RI3152A_FILTER_50MHZ:  error = viPrintf(vi, "0202A0\n");
	                                   error = viPrintf(vi, "0201A3\n");
	                                   break;
			}
	}
	else
	{
		switch ( lp_filter )
			{
			case  RI3152A_FILTER_OFF:   error = viPrintf(vi, "OUTP:FILT:STAT OFF\n");
	                                   break;

			case RI3152A_FILTER_20MHZ:  error = viPrintf(vi, "OUTP:FILT:STAT ON\n");
	                                   error = viPrintf(vi, "OUTP:FILT:FREQ 20MHz\n");
	                                   break;

			case RI3152A_FILTER_25MHZ:  error = viPrintf(vi, "OUTP:FILT:STAT ON\n");
	                                   error = viPrintf(vi, "OUTP:FILT:FREQ 25MHz\n");
	                                   break;

			case RI3152A_FILTER_50MHZ:  error = viPrintf(vi, "OUTP:FILT:STAT ON\n");
	                                   error = viPrintf(vi, "OUTP:FILT:FREQ 50MHz\n");
	                                   break;
			}
	}

    return( error );
}

/*=========================================================================*/
/* This function turn the output signal ON or OFF                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output    (ViSession vi,
                                    ViBoolean output_switch)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_boolean_range (output_switch, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "0200A%s\n", (output_switch) ? "0" : "1");
	else
       error = viPrintf(vi, "OUTP %s\n", (output_switch) ? "ON" : "OFF");

    return( error );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_output (ViSession vi,
										ViPInt16 output_state)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0200A?" : "OUTP?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*output_state = atoi (readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function turn the output signal ON or OFF                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_shunt    (ViSession vi,
                                    ViBoolean output_switch)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_boolean_range (output_switch, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "0250A%s\n", (output_switch) ? "1" : "0");
	else
       error = viPrintf(vi, "OUTP:SHUN %s\n", (output_switch) ? "ON" : "OFF");

    return( error );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_output_shunt (ViSession vi,
										ViPInt16 output_state)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0250A?" : "OUTP:SHUN?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*output_state = atoi (readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function turn the output signal ON or OFF                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_eclt    (ViSession vi,
                                    ViBoolean output_switch)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_boolean_range (output_switch, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "0210B0,%s\n", (output_switch) ? "1" : "0");
	else
       error = viPrintf(vi, "OUTP:ECLT0 %s\n", (output_switch) ? "ON" : "OFF");

    return( error );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_output_eclt (ViSession vi,
										ViPInt16 output_state)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0210B?" : "OUTP:ECLT0?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*output_state = atoi (readbuf);
	
	return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function turn the output signal ON or OFF                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_change_mode    (ViSession vi,
                                    ViBoolean mode)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_boolean_range (mode, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	if (fast_mode[(ViInt16)index] && !mode)		/* Currently in fast mode, want normal */
       error = viPrintf(vi, "0150A0\n");
	else if (!fast_mode[(ViInt16)index] && mode)	/* Currently in normal mode, want fast */
       error = viPrintf(vi, "INST:MODE FAST\n");
	else
		;		/* Do nothing since the mode specified is the same */
		
	fast_mode[(ViInt16)index] = mode;

    return( error );
}


/*=========================================================================*/
/* This function turn the output signal ON or OFF                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_format_border    (ViSession vi,
                                    ViBoolean format)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_boolean_range (format, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "0100A%s\n", (format) ? "1" : "0");
	else
       error = viPrintf(vi, "FORM:BORD %s\n", (format) ? "SWAP" : "NORM");

    return( error );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_format_border_query (ViSession vi,
										ViString format)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0100A?" : "FORM:BORD?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt-2] = 0;

	strcpy (format, readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function turn the output signal ON or OFF                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_format_wave    (ViSession vi,
                                    ViBoolean format)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_boolean_range (format, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */
	if (fast_mode[(ViInt16)index])
       error = viPrintf(vi, "0101A%s\n", (format) ? "1" : "0");
	else
       error = viPrintf(vi, "FORM:WAVE %s\n", (format) ? "USER" : "NORM");

    return( error );
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_format_wave_query (ViSession vi,
										ViString format)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0101A?" : "FORM:WAVE?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt-2] = 0;

	strcpy (format, readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function takes the parameters for Arbitrary Waveform generation.   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_define_arb_segment (ViSession vi, ViInt16 segment_number,
                                            ViInt32 segment_size)


{
    ViStatus error;
    ViInt32 max_size;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
    
	max_size = ri3152a_get_mem_size(vi);
	
    /* check parameter ranges */

    if (error = ri3152a_int_range (segment_number, RI3152A_MIN_SEG_NUMBER,
                                    RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2))
        return( error );

    if (error = ri3152a_int32_range (segment_size, RI3152A_MIN_SEGMENT_SIZE,
                                    max_size, VI_ERROR_PARAMETER3))
        return( error );
        
	if (segment_size % 2)
		return( VI_ERROR_PARAMETER3 );

	if (fast_mode[(ViInt16)index])
	{
	    error = viPrintf(vi, "10102%d,%ld\n", segment_number, segment_size);
	    if (error <0)
	       return (error);

	    error = viPrintf(vi, "1030A%d\n", segment_number);
	    if (error <0)
	       return (error);
	}
	else
	{
	    error = viPrintf(vi, "TRAC:DEF %d,%ld\n", segment_number, segment_size);
	    if (error <0)
	       return (error);

	    error = viPrintf(vi, "TRAC:SEL %d\n", segment_number);
	    if (error <0)
	       return (error);
	}


    return( error );

}


/*=========================================================================*/
/* This function Deletes one or more segment numbers.                      */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_delete_segments (ViSession vi, ViInt16 segment_number,
                                            ViBoolean delete_all_segments)

{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int_range (segment_number, RI3152A_MIN_SEG_NUMBER,
                                   RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2))
        return( error );

    if (error = ri3152a_boolean_range (delete_all_segments, VI_ERROR_PARAMETER3))
        return( error );

	if (fast_mode[(ViInt16)index])
	{
	    if (delete_all_segments)
		{
	             error = viPrintf(vi, "10210\n");
	             if (error <0)
	                return (error);
		}
		else
		{
		    error = viPrintf(vi, "09201%d\n", segment_number);
		    if (error <0)
		       return (error);
		}
	}
	else
	{
	    if (delete_all_segments)
		{
	             error = viPrintf(vi, "TRAC:DEL:ALL\n");
	             if (error <0)
	                return (error);
		}
		else
		{
		    error = viPrintf(vi, "TRAC:DEL %d\n", segment_number);
		    if (error <0)
		       return (error);
		}	       

	}

    return( error );

}

/*=========================================================================*/
/* This function loads arbitrary data into the specified segment           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_load_arb_data (ViSession vi, 
											ViInt16 segment_number,
											ViUInt16 *data_pts,
                                            ViInt32 number_of_points)
{
    ViInt32 max_size;
    ViStatus error;
    ViStatus err2;
    ViUInt32 cnt;
    ViChar *cmd;
    
	ViJobId job;
	ViEventType etype;
	ViEvent event;
	ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;


	/* range check the parameters */
    if (error = ri3152a_int_range (segment_number, RI3152A_MIN_SEG_NUMBER,
                                    RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2))
        return( error );


	max_size = ri3152a_get_mem_size(vi);
    if (error = ri3152a_int32_range (number_of_points, RI3152A_MIN_SEGMENT_SIZE,
                                    max_size, VI_ERROR_PARAMETER4))
        return( error );


	/* select the arbitrary waveform segment */
	error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1030A" : "TRAC:SEL ", segment_number);
	if (error < 0) return( error );

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
	if (error < 0) return( error );

	/* get the device ready to download via A24 space */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1200A1" : "SMEM:MODE WRITE");
	if (error < 0) return( error );

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A1" : "SMEM:STATE ON");
	if (error < 0) return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "1509@?" : "*OPC?";
	error = viPrintf(vi, "%s\n", cmd);
	if (error < 0) return( error );

	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );
		
	/* try again */
	if (readbuf[0] != '1')
	{
		if ((error = viPrintf(vi, "%s\n", cmd)) < 0)
			return( error );

		if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
			return( error );
		
		if (readbuf[0] != '1')
			return( RI3152A_BINARY_DOWNLOAD_FAILED );
	}

	
	error = viMoveOut16 (vi, VI_A24_SPACE, 0L, number_of_points, data_pts);
	err2 = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
	if (error < 0)
		return( error );
	
	return( err2 );
}


/*=========================================================================*/
/* This function loads arbitrary data into the specified segment           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_load_and_shift_arb_data (ViSession vi,
                                                  ViInt16 segment_number,
                                                  ViInt16 *data_pts,
                                                  ViInt32 number_of_points)
{
    ViInt32 max_size;
    ViStatus error;
    ViStatus err2;
    ViUInt32 cnt;
    ViChar *cmd;

	ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;


	/* range check the parameters */
    if (error = ri3152a_int_range (segment_number, RI3152A_MIN_SEG_NUMBER,
                                    RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2))
        return( error );


	max_size = ri3152a_get_mem_size(vi);
    if (error = ri3152a_int32_range (number_of_points, RI3152A_MIN_SEGMENT_SIZE,
                                    max_size, VI_ERROR_PARAMETER4))
        return( error );


	/* select the arbitrary waveform segment */
	if ((error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1030A" : "TRAC:SEL ", segment_number)) < 0)
		return( error );

	if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF")) < 0)
		return( error );

	/* get the device ready to download via A24 space */
	if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1200A1" : "SMEM:MODE WRITE")) < 0)
		return( error );

	if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A1" : "SMEM:STATE ON")) < 0)
		return( error );

	cmd = (fast_mode[(ViInt16)index]) ? "1509@?" : "*OPC?";
	if ((error = viPrintf(vi, "%s\n", cmd)) < 0)
		return( error );

	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );
		
	/* try again */
	if (readbuf[0] != '1')
		{
		if ((error = viPrintf(vi, "%s\n", cmd)) < 0)
			return( error );

		if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
			return( error );
		
		if (readbuf[0] != '1')
			return( RI3152A_BINARY_DOWNLOAD_FAILED );
		}


	/* download the data points via A24 address */
	error = viMoveOut16 (vi, VI_A24_SPACE, 0L, number_of_points, (ViPUInt16) data_pts);
	err2 = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
	if (error < 0)
		return( error );
	
	return( err2 );
}

/*=========================================================================*/
/* This function loads arbitrary data from an ASCII data file              */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_load_ascii_file (ViSession vi, 
											ViInt16 segment_number,
											ViString file_name,
                                            ViInt32 number_of_points)
{
    ViInt32 max_size, file_count;
    ViUInt32 cnt;
    short *databuf;
    ViInt32 data_point;
    FILE *fp;
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;
    int value;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	/* range check the parameters */
    if (error = ri3152a_int_range (segment_number, RI3152A_MIN_SEG_NUMBER,
                                    RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2))
        return( error );


	max_size = ri3152a_get_mem_size(vi);
    if (error = ri3152a_int32_range (number_of_points, RI3152A_MIN_SEGMENT_SIZE,
                                    max_size, VI_ERROR_PARAMETER4))
        return( error );

	/* try to open the data file */
	if (! (fp = fopen(file_name, "r")))
		return( RI3152A_CANT_OPEN_FILE );

	/* set the flag to indicate first use of the file */
	global_file_flag = 0;

	databuf = (short *) malloc(number_of_points*sizeof(short));
	if (!databuf) {
		fclose(fp);
		return ( RI3152A_OUT_OF_MEMORY );
	}
	
	error = viPrintf(vi, "%s%d,%d\n", (fast_mode[(ViInt16)index]) ? "10102" : "TRAC:DEF ", 
								segment_number, (int) number_of_points);
	if (error < 0)
		{
		free(databuf);
		fclose(fp);
		return( error );
		}


	/* select the arbitrary waveform segment */
	error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1030A" : "TRAC:SEL ", segment_number);
	if (error < 0)
	{
		free(databuf);
		fclose(fp);
		return( error );
	}

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
	if (error < 0)
	{
		free(databuf);
		fclose(fp);
		return( error );
	}

	/* get the device ready to download via A24 space */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1200A1" : "SMEM:MODE WRITE");
	if (error < 0)
	{
		free(databuf);
		fclose(fp);
		return( error );
	}

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A1" : "SMEM:STATE ON");
	if (error < 0)
	{
		free(databuf);
		fclose(fp);
		return( error );
	}

    cmd = (fast_mode[(ViInt16)index]) ? "1509@?" : "*OPC?";
	if ((error = viPrintf(vi, "%s\n", cmd)) < 0)
	{
		free(databuf);
		fclose(fp);
		return( error );
	}

	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
	{
		free(databuf);
		fclose(fp);
		return( error );
	}
		
	/* try again */
	if (readbuf[0] != '1')
	{
		if ((error = viPrintf(vi, "%s\n", cmd)) < 0)
		{
			free(databuf);
			fclose(fp);
			return( error );
		}

		if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		{
			free(databuf);
			fclose(fp);
			return( error );
		}
		
		if (readbuf[0] != '1')
		{
			free(databuf);
			fclose(fp);
			return( RI3152A_BINARY_DOWNLOAD_FAILED );
		}
	}

	/* download the data points via A24 address */
	file_count = 0;
	
	while( file_count < number_of_points )
	{
		value = fscanf (fp, "%d", &data_point);
			
		if (data_point < -2048 || data_point > 2047)
		{
			viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
			free(databuf);
			fclose(fp);
			return( RI3152A_INVALID_FILE_DATA );
		}
		
		/* scale data in the range -2048 to 2047 to DAC range 0 to 4095 */
		/* then shift left into DAC bit positions */
		data_point = (data_point + 2048) << 4;
		databuf[file_count] = (short) (data_point & 0xFFF0);
		
		++file_count;
	}		
	
	error = viMoveOut16 (vi, VI_A24_SPACE, 0L, number_of_points, (ViPUInt16) databuf);
	if (error < 0)
	{
		viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
		free(databuf);
		fclose(fp);
		return( error );
	}

	free(databuf);
	fclose(fp);
	
    return( viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF") );

}



/*=========================================================================*/
/* Function: ri3152a_load_wavecad_file()                                    */
/* Purpose:  This function reads a WaveCAD 2.0 data file and configures    */
/*           the instrument as indicated by the file                       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_load_wavecad_file (ViSession vi, ViChar filename[])
{
	int count, cmd_count;
	FILE *fp;
	ViStatus error;
	char filebuffer[100];
	ViBoolean first_wave;
	ViInt16 link, segment, max_link;
	ViInt32 loop_count, segment_length;
	char wave_file_name[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* open the file */
	fp = fopen(filename, "r");
	if (!fp)
		return( RI3152A_CANT_OPEN_FILE );

	/* File Format: */
	/*
	 * "#Instrument"
	 * <Instrument Name>  (Presumably "Racal 3152a")
	 * "#Channels:"
	 * <Number of Channels>  (Presumably "Chan 1")
	 * "#Param_values__Idnum__Cur______Def_____Min_____Max"
	 * <State Entry>
	 * <State Entry>
	 * <State Entry>
	 *    .
	 *    .
	 *    .
	 *  <State Entry>
	 * "#Gpib_comm:<Id>+command+[switch_param]"
	 * <Command Entry>
	 * <Command Entry>
	 * <Command Entry>
	 *      etc.
	 * <Command Entry>
	 * "#Sequencer:+link+segm+loop+wlength+wave_1+wave_2"
	 * <Sequence Entry>
	 * <Sequence Entry>
	 * <Sequence Entry>
	 *      etc.
	 *
	 *  End of File
	 */
	
	/*
	 * find the third '#' to begin the line
	 */

	count = 0;
	while ((count < 3) && fgets(filebuffer, sizeof(filebuffer), fp))
		{
		if (filebuffer[0] == '#')
			++count;
		}
		
	if ((count != 3) || strncmp(filebuffer, "#Param", 6))
		{
		fclose(fp);
		return( RI3152A_WAVECAD_FORMAT_1 );
		}

	/*
	 * clear out the command table
	 */
	for (cmd_count = 0;  cmd_count < RI3152A_MAX_CMDS;  ++cmd_count)
		{
		cmd_table[cmd_count].cmd_id = 0;
		cmd_table[cmd_count].cmd_text[0] = 0;
		cmd_table[cmd_count].cmd_param[0] = 0;
		}
		
	/*
	 * read each <State Entry> up until the next '#'
	 * each <State Entry> is of the form:
	 * <Description with no spaces> <Command ID> <Current Value> <Default> <Max> <Min>
	 */
	 cmd_count = 0;
	 while (fgets(filebuffer, sizeof(filebuffer), fp) && (filebuffer[0] != '#'))
	 	{
	 	if (cmd_count >= RI3152A_MAX_CMDS)
	 		{
	 		fclose(fp);
	 		return( RI3152A_WAVECAD_FORMAT_2 );
	 		}

	 	error = ri3152a_parse_wavecad_state(&cmd_table[cmd_count], filebuffer);
	 	if (error < 0)
	 		{
	 		fclose(fp);
	 		return( error );
	 		}
	 		
	 	++cmd_count;
	 	}
	 	
	 /*
	  * ensure that the '#' is the '#Gpib_comm' line
	  */
	 if (strncmp(filebuffer, "#Gpib", 5))
	 	{
	 	fclose(fp);
	 	return( RI3152A_WAVECAD_FORMAT_3 );
	 	}

	/*
	 * now read each command entry, and update the command table
	 * the number of commands MUST be less than or equal to the number
	 * of state entries
	 */
	 count = 0;
	 while (fgets(filebuffer, sizeof(filebuffer), fp) && (filebuffer[0] != '#'))
	 	{
	 	if (count >= cmd_count)
	 		{
	 		fclose(fp);
	 		return( RI3152A_WAVECAD_FORMAT_4 );
	 		}
	 		
	 	error = ri3152a_parse_wavecad_cmd(cmd_table, cmd_count, filebuffer);
	 	if (error < 0)
	 		{
	 		fclose(fp);
	 		return( error );
	 		}
	 		
	 	++count;
	 	}

	/*
	 * ensure the '#' is for the '#Sequencer' line
	 */
	if (strncmp(filebuffer, "#Sequencer", 10))
		{
		fclose(fp);
		return( RI3152A_WAVECAD_FORMAT_5 );
		}

	max_link = 0;
	first_wave = 1;
	
	while( fgets(filebuffer, sizeof(filebuffer), fp) )
		{
		/*
		 * Line format is:
		 *     Link# <link> <segment> <loop> <num points> <wave file name>
		 */
		if (strncmp(filebuffer, " Link#", 6))
			{
			fclose(fp);
			return( RI3152A_WAVECAD_FORMAT_6 );
			}
			
		/*
		 * skip the first line with a waveform/sequence.
		 * This line indicates the wave which is 
		 * presently loaded into WaveCAD
		 */
		if (first_wave)
			{
			first_wave = 0;
			continue;
			}
			
		error = ri3152a_parse_wavecad_seq(filebuffer, &link, &segment, &loop_count,
		                                 &segment_length, wave_file_name);
		if (error < 0)
			{
			fclose(fp);
			return( error );
			}
			
		if ((link >= RI3152A_MAX_SEQ) || (link < 1))
			{
			fclose(fp);
			return( RI3152A_WAVECAD_FORMAT_7 );
			}
	
		if (link > max_link)
			max_link = link;
			
		segment_numbers[link-1] = segment;
		segment_loops[link-1] = loop_count;
		
		error = ri3152a_create_path_name(wave_file_name, filename);
		if (error < 0)
			{
			fclose(fp);
			return( error );
			}
			
		error = ri3152a_load_wavecad_wave_file(vi, segment, wave_file_name);
		if (error < 0)
			{
			fclose(fp);
			return( error );
			}
		}

	/* close the file */
	fclose(fp);
	
	/*
	 * now, download the sequence (if defined in .CAD file)
	 */

	if (max_link > 0)
		{
		error = ri3152a_define_sequence(vi, max_link, segment_numbers, segment_loops);
		if (error < 0)
			return( error );
		}
		
	/*
	 * finally, form all of the relevent commands and output
	 * them to the 3152a
	 */
	return( ri3152a_output_wavecad_commands(vi, cmd_table, cmd_count) );
}


/*=========================================================================*/
/* Function: ri3152a_load_wavecad_wave_file()                               */
/* Purpose:  This function reads a WaveCAD 2.0 data file and configures    */
/*           the instrument as indicated by the file                       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_load_wavecad_wave_file (ViSession vi,
                                        		 ViInt16 segment,
                                        		 ViChar filename[])
		
{
#define PTS_PER_LOOP	((size_t) 512)
	FILE *fp;
	short *databuf;
	short data_point;
	int num_read, i;
    ViStatus error;
    ViUInt32 cnt;
    long file_size;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter range */
    if (error = ri3152a_int_range (segment, RI3152A_MIN_SEG_NUMBER, 
    									   RI3152A_MAX_SEG_NUMBER,
                                    	   VI_ERROR_PARAMETER2))
        return( error );
	
	fp = fopen(filename, "rb");
	if (!fp)
		return( RI3152A_CANT_OPEN_FILE );

	/*
	 * calculate the file size by setting to end of file and
	 * using ftell() to determine where you are
	 */
	fseek (fp, 0L, SEEK_END);
	file_size = ftell (fp) / 2;
	fseek (fp, 0L, SEEK_SET);

	databuf = (short *) malloc(file_size*sizeof(short));
	if (!databuf) {
		fclose(fp);
		return ( RI3152A_OUT_OF_MEMORY );
	}
	
	/*
	 * define the trace
	 */
	error = viPrintf(vi, "%s%d,%d\n", (fast_mode[(ViInt16)index]) ? "10102" : "TRAC:DEF ", 
								segment, (int) file_size);
	if (error < 0)
		{
		free(databuf);
		fclose(fp);
		return( error );
		}
	
	/* select the segment and get ready for downloading data */
	/* select the arbitrary waveform segment */
	error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1030A" : "TRAC:SEL ", segment);
	if (error < 0)
		{
		free(databuf);
		fclose(fp);
		return( error );
		}

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
	if (error < 0)
		{
		free(databuf);
		fclose(fp);
		return( error );
		}

	/* get the device ready to download via A24 space */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1200A1" : "SMEM:MODE WRITE");
	if (error < 0)
		{
		free(databuf);
		fclose(fp);
		return( error );
		}

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A1" : "SMEM:STATE ON");
	if (error < 0)
		{
		free(databuf);
		fclose(fp);
		return( error );
		}

    cmd = (fast_mode[(ViInt16)index]) ? "1509@?" : "*OPC?";
	if ((error = viPrintf(vi, "%s\n", cmd)) < 0)
		{
		free(databuf);
		fclose(fp);
		return( error );
		}

	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		{
		free(databuf);
		fclose(fp);
		return( error );
		}
		
	/* try again */
	if (readbuf[0] != '1')
		{
		if ((error = viPrintf(vi, "%s\n", cmd)) < 0)
			{
			free(databuf);
			fclose(fp);
			return( error );
			}

		if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
			{
			free(databuf);
			fclose(fp);
			return( error );
			}
		
		if (readbuf[0] != '1')
			{
			free(databuf);
			fclose(fp);
			return( RI3152A_BINARY_DOWNLOAD_FAILED );
			}
		}

	/* read/download up to file_size points */
	if ((num_read = fread (databuf, sizeof(short), file_size, fp)) > 0) {
		/* check each data point/convert to upper 12-bits */
		for (i = 0;  i < num_read;  ++i) {
			data_point = databuf[i];
			
			if (databuf[i] < -2048 || databuf[i] > 2047) {
				viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
				free(databuf);
				fclose(fp);
				return( RI3152A_INVALID_FILE_DATA );
			}
		
			/* scale data in the range -2048 to 2047 to DAC range 0 to 4095 */
			/* then shift left into DAC bit positions */
			databuf[i] = (databuf[i] + 2048) << 4;
		}
		
		/* download the data points via A24 address */
		error = viMoveOut16 (vi, VI_A24_SPACE, 0L, num_read, (ViPUInt16) databuf);
		if (error < 0) {
			viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF");
			free(databuf);
			fclose(fp);
			return( RI3152A_INVALID_FILE_DATA );
		}
	}

	free(databuf);
	fclose(fp);

	/* return the 3152a control of shared memory */
    return( viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A0" : "SMEM:STATE OFF"));
}

		

/*=========================================================================*/
/* Function: ri3152a_arb_sampling_freq()                                    */
/* Purpose:  This function sets the sampling clock for arbitrary waveforms.*/
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_arb_sampling_freq (ViSession vi,
											ViReal64 sampling_clock)
{
	ViStatus error;
	ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	if (error = ri3152a_real_range (sampling_clock, RI3152A_MIN_SAMP_CLK, RI3152A_MAX_SAMP_CLK, 
										   VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0410A" : "FREQ:RAST ";
    
	error = viPrintf(vi, "%s%f\n", cmd, sampling_clock) ;
		
	return (error);
	
}


/*=========================================================================*/
/* This function outputs arbitrary waveforms.                              */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_arb_waveform (ViSession vi, ViInt16 segment_number,
                                              ViReal64 sampling_clock,
                                              ViReal64 amplitude,
                                              ViReal64 offset,
                                              ViInt16 clock_source)


{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int_range (segment_number, RI3152A_MIN_SEG_NUMBER, RI3152A_MAX_SEG_NUMBER,
                                    VI_ERROR_PARAMETER2))
        return( error );

    if (error = ri3152a_real_range (sampling_clock, RI3152A_MIN_SAMP_CLK, RI3152A_MAX_SAMP_CLK,
                                    VI_ERROR_PARAMETER3))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    if (error = ri3152a_int_range (clock_source, RI3152A_CLK_SOURCE_INT, RI3152A_CLK_SOURCE_EXT,
                                   VI_ERROR_PARAMETER6))
        return( error );


    /* format the command and output it to the generator */
	switch ( clock_source )
		{
        case RI3152A_CLK_SOURCE_INT:  error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0411A3" : "FREQ:RAST:SOUR INT");
                                     break;

        case RI3152A_CLK_SOURCE_EXT:  error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0411A4" : "FREQ:RAST:SOUR EXT");
                                     break;
                                     
     }

      if (error < 0)
         return (error);



    error = viPrintf (vi, "%s%d,%.5g,%.4g,%.4g\n", (fast_mode[(ViInt16)index]) ? "0309D" : "APPLY:USER ",
                        segment_number, sampling_clock, amplitude, offset);

    if (error < 0)
        return( error );

    error = viPrintf (vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0610A1" : "FUNC:MODE USER");
	
    return( error );

}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sclk_source_query (ViSession vi,
										ViPInt16 source)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0411A?" : "FREQ:RAST:SOUR?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (strncmp(readbuf, "INT", 3) == 0) *source = 0;
	else if (strncmp(readbuf, "EXT", 3) == 0) *source = 1;
	else if (strncmp(readbuf, "ECL", 3) == 0) *source = 2;
	else return( RI3152A_INVALID_REPLY );
	
	return (VI_SUCCESS);
}




/*=========================================================================*/
/* This function takes the sequence parameters                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_define_sequence (ViSession vi, 
                                          ViInt16 number_of_steps,
                                          ViInt16 *segment_number,
                                          ViInt32 *repeat_number)

{
    ViStatus error;
    int i;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	if (error = ri3152a_int_range (number_of_steps, RI3152A_MIN_NUM_STEPS,
                                    RI3152A_MAX_NUM_STEPS, VI_ERROR_PARAMETER2))

		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "13103" : "SEQ:DEF ";
    
	for ( i= 0; i < number_of_steps; ++i)
		{
		if (error = ri3152a_int_range (segment_number[i], RI3152A_MIN_SEG_NUMBER,
                                              RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER3))
			return( error );

		if (error = ri3152a_int32_range (repeat_number[i], RI3152A_MIN_NUM_REPEAT,
                                              RI3152A_MAX_NUM_REPEAT, VI_ERROR_PARAMETER4))
			return( error );

		error = viPrintf (vi, "%s%d,%d,%d\n", cmd,
                        i+1, segment_number[i], repeat_number[i]);

		if (error < 0)
			return( error );
		}

	return (error);
}



/*=========================================================================*/
/* This function takes the sequence parameters                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_load_sequence_binary (ViSession vi, 
                                          ViInt16 number_of_steps,
                                          ViInt16 *segment_number,
                                          ViInt32 *repeat_number)

{
    ViStatus error;
	ViInt8 byte1, byte2, byte3, byte4, byte5;
	ViInt16 number_of_bytes, seg_num;
	ViChar buffer[100];
	ViChar cmd[20500];
    int i, len;
	ViInt32 long_index;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
    number_of_bytes = number_of_steps * 5;
    
    ri3152a_itoa (number_of_bytes, buffer);
	
	for ( i = 0; i < number_of_steps; ++i)
	{
		if (error = ri3152a_int_range (segment_number[i], RI3152A_MIN_SEG_NUMBER,
                                              RI3152A_MAX_SEG_NUMBER, VI_ERROR_PARAMETER3))
			return( error );


		if (error = ri3152a_int32_range (repeat_number[i], RI3152A_MIN_NUM_REPEAT,
                                              RI3152A_MAX_NUM_REPEAT, VI_ERROR_PARAMETER4))
			return( error );

		if (error < 0)
			return( error );
		
		seg_num = segment_number[i] - 1;
		
		byte2 = seg_num & 0xFF;
		byte1 = (seg_num >> 8) & 0xFF;
		
		/* Separate byte-by-byte */
		byte4 = (repeat_number[i] & 0xFF);
		byte5 = (repeat_number[i] >> 16) & 0xF;
		byte3 = (repeat_number[i] >> 8) & 0xFF;
		
		/* Packing all binary data */
		if (i == 0) 
		{
			cnt = sprintf (cmd, "%s#%d%d", (fast_mode[(ViInt16)index]) ? "13401" : ":SEQ:DATA", strlen (buffer), number_of_bytes);
			len = strlen (cmd);
		}
		
		/* Send in restructured in appropriate order */
		cmd[len++] = byte1;
		cmd[len++] = byte2;
		cmd[len++] = byte3;
		cmd[len++] = byte4;
		cmd[len++] = byte5;
	}
	
	cmd[len] = '\n';
	
	error = viWrite(vi, (ViBuf) cmd, len, &cnt);
	
	return (error);
}


/* ========================================================================= */
/* Function: Load Segment Table                                              */
/* Description: This function loads the segment table using the internal     */
/*              format of the 3153                                           */
/* Parameters: vi -- the instrument handle                                   */
/*             num_segments: the number of segments in the table             */
/*             wavesize -- the wave size of the segment table                */
/* ========================================================================= */

ViStatus _VI_FUNC ri3152a_load_segment_binary (ViSession vi,
											 ViInt16 number_of_segments,
											 ViAInt32 segment_size)
{
	ViStatus error;
	char cmd[200500], byteSize[100];
	int i, j;
	ViInt32 len, mem_address = 0x01F4;
	ViInt8 byte1, byte2, byte3, byte4, byte5, remain_bits;
	ViInt16 number_of_bytes = 0;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	number_of_bytes = number_of_segments * 5;
    
    ri3152a_itoa (number_of_bytes, byteSize);
	
	for (i = 0; i < number_of_segments; i++) 
	{
		byte1 = mem_address & 0xFF;
		byte2 = (mem_address >> 8) & 0xFF;
		byte3 = ((mem_address >> 16) & 0x3) | 0x4;
		if ( i == (number_of_segments - 1) )
			byte3 |= 0x8;
		else
			byte3 &= 0x7;
	
		remain_bits = (segment_size[i] >> 16) & 0xF;
		
		byte3 |= (remain_bits << 4);				
		
		byte5 = segment_size[i] & 0xFF;
		byte4 = (segment_size[i] >> 8) & 0xFF;
		
		if (i == 0)
		{
			cnt = sprintf (cmd, "%s#%d%d", (fast_mode[(ViInt16)index]) ? "10501" : ":SEGM:DATA", strlen (byteSize), number_of_bytes);
			len = strlen (cmd);
		}
		
		/* Send in restructured in appropriate order */
		cmd[len++] = byte1;
		cmd[len++] = byte2;
		cmd[len++] = byte3;
		cmd[len++] = byte4;
		cmd[len++] = byte5;
		
		mem_address += (segment_size[i] / 2);
	}
	
	cmd[len] = '\n';
	
	error = viWrite(vi, (ViBuf) cmd, len, &cnt);
	
	return (error);
}

/*=========================================================================*/
/* This function Deletes one or more sequence numbers.                     */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_delete_sequence (ViSession vi, ViInt16 stepNumber)

{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
	error = ri3152a_int_range (stepNumber, 0,
							          RI3152A_MAX_NUM_STEPS,
							          VI_ERROR_PARAMETER2);
	if (error <0) return error;
	
	if (stepNumber == 0)
		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "13210" : "SEQ:DEL:ALL");
	else
		error = viPrintf(vi, "%s%hd\n", (fast_mode[(ViInt16)index]) ? "13201" : "SEQ:DEL ", stepNumber);
		
    return( error );
}

/*=========================================================================*/
/* This function gets the sequence parameters and outputs the sequential   */
/* waveform.                                                               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_output_sequence_waveform (ViSession vi,
                                                   ViReal64 sampling_clock,
                                                   ViReal64 amplitude,
                                                   ViReal64 offset,
                                                   ViBoolean sequence_mode)
{
    ViStatus error;
    ViChar *param;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
    if (error = ri3152a_real_range (sampling_clock, RI3152A_MIN_SAMP_CLK, RI3152A_MAX_SAMP_CLK,
                                    VI_ERROR_PARAMETER2))
        return( error );

	if((error = ri3152a_check_ampl_and_offset(vi, amplitude, offset)) < 0)
		return( error );
		
    /* check parameter ranges */
    if (error = ri3152a_boolean_range (sequence_mode, VI_ERROR_PARAMETER4))
        return( error );

	/* set the sequence advance mode */
	if (fast_mode[(ViInt16)index])
		error = viPrintf(vi, "1300A%s\n", (sequence_mode == RI3152A_SEQ_AUTO) ? "0" : "1");
	else
		error = viPrintf(vi, "SEQ:ADV %s\n", (sequence_mode == RI3152A_SEQ_AUTO) ? "AUTO" : "TRIG");
	if (error < 0)
		return( error );

	/* format the command and output it to the generator */
    error = viPrintf (vi, "%s,%f,%f,%f\n", (fast_mode[(ViInt16)index]) ? "0309D1" : "APPLY:USER 1",
						sampling_clock, amplitude, offset);

    if (error < 0)
        return( error );


    return( viPrintf (vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0610A2" : "FUNC:MODE SEQ") );
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sequence_mode_query (ViSession vi,
										ViPInt16 sequence_mode)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1300A?" : "SEQ:ADV?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (!(strncmp(readbuf, "AUTO", 4))) *sequence_mode = 0;	
	else if (!(strncmp(readbuf, "TRIG", 4))) *sequence_mode = 1;	
	else return( RI3152A_INVALID_REPLY );
	
	return (VI_SUCCESS);
}



/*=========================================================================*/
/* Function: Set Sweep Time                                                */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sweep_time (ViSession vi,
							   			  ViReal64 sweep_time)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* check parameter ranges */
	if (error = ri3152a_real_range (sweep_time, RI3152A_SWEEP_MIN_TIME, RI3152A_SWEEP_MAX_TIME,
									VI_ERROR_PARAMETER2))
		return( error );

	/* send the command */
	error = viPrintf(vi, "%s%.3lf\n", (fast_mode[(ViInt16)index]) ? "1600A" : "SWE:TIME ", sweep_time);
	return (error);
}

/*=========================================================================*/
/* Function: Query Sweep Time                                              */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sweep_time (ViSession vi,
							   			  ViReal64 *sweep_time)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
    ViUInt32  cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
		
	/* format the command */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1600A?" : "SWE:TIME?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *sweep_time = strtod(readbuf, NULL);

	return (VI_SUCCESS);
}



/*=========================================================================*/
/* Function: Set Sweep Time                                                */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sweep_step (ViSession vi,
							   			  ViInt16 sweep_step)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* check parameter ranges */
	if (error = ri3152a_real_range (sweep_step, RI3152A_SWEEP_MIN_STEP, RI3152A_SWEEP_MAX_STEP,
									VI_ERROR_PARAMETER2))
		return( error );

	/* send the command */
	error = viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1603A" : "SWE:STEP ", sweep_step);
	return (error);
}

/*=========================================================================*/
/* Function: Query Sweep Time                                              */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sweep_step (ViSession vi,
							   			  ViInt16 *sweep_step)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
    ViUInt32  cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
		
	/* format the command */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1603A?" : "SWE:STEP?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*sweep_step = atoi (readbuf);

	return (VI_SUCCESS);
}




/*=========================================================================*/
/* Function: Set Sweep Time                                                */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sweep_direction (ViSession vi,
							   			  ViBoolean mode)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* send the command */
	if (mode)
		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1601A1" : "SWE:DIR DOWN");
	else		
		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1601A0" : "SWE:DIR UP");
		
	return (error);
}

/*=========================================================================*/
/* Function: Query Sweep Time                                              */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sweep_direction (ViSession vi,
							   			  ViBoolean *mode)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
    ViUInt32  cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
		
	/* format the command */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1601A?" : "SWE:DIR?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (strncmp("UP", readbuf, 2) == 0)
		*mode = 0;
	else if (strncmp("DOWN", readbuf, 4) == 0)
		*mode = 1;
	else
		return( RI3152A_INVALID_REPLY );

	return (VI_SUCCESS);
}



/*=========================================================================*/
/* Function: Set Sweep Time                                                */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sweep_spacing (ViSession vi,
							   			  ViBoolean mode)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* send the command */
	if (mode)
		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1602A1" : "SWE:SPAC LOG");
	else		
		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1602A0" : "SWE:SPAC LIN");
		
	return (error);
}

/*=========================================================================*/
/* Function: Query Sweep Time                                              */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sweep_spacing (ViSession vi,
							   			  ViBoolean *mode)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
    ViUInt32  cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
		
	/* format the command */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1602A?" : "SWE:SPAC?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (strncmp("LIN", readbuf, 3) == 0)
		*mode = 0;
	else if (strncmp("LOG", readbuf, 3) == 0)
		*mode = 1;
	else
		return( RI3152A_INVALID_REPLY );

	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sweep Time                                                */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sweep_freq_start (ViSession vi,
							   			  ViReal64 freq)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* check parameter ranges */
	if (error = ri3152a_real_range (freq, RI3152A_SWEEP_MIN_FREQ, RI3152A_SWEEP_MAX_FREQ,
									VI_ERROR_PARAMETER2))
		return( error );

	/* send the command */
	error = viPrintf(vi, "%s%.3lf\n", (fast_mode[(ViInt16)index]) ? "1610A" : "SWE:FREQ ", freq);
	return (error);
}

/*=========================================================================*/
/* Function: Query Sweep Time                                              */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sweep_freq_start (ViSession vi,
							   			  ViReal64 *freq)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
    ViUInt32  cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
		
	/* format the command */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1610A?" : "SWE:FREQ?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *freq = strtod(readbuf, NULL);

	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sweep Time                                                */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sweep_freq_stop (ViSession vi,
							   			  ViReal64 freq)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* check parameter ranges */
	if (error = ri3152a_real_range (freq, RI3152A_SWEEP_MIN_FREQ_STOP, RI3152A_SWEEP_MAX_FREQ_STOP,
									VI_ERROR_PARAMETER2))
		return( error );

	/* send the command */
	error = viPrintf(vi, "%s%.3lf\n", (fast_mode[(ViInt16)index]) ? "1611A" : "SWE:FREQ:STOP ", freq);
	return (error);
}

/*=========================================================================*/
/* Function: Query Sweep Time                                              */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sweep_freq_stop (ViSession vi,
							   			  ViReal64 *freq)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
    ViUInt32  cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
		
	/* format the command */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1611A?" : "SWE:FREQ:STOP?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *freq = strtod(readbuf, NULL);

	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sweep Time                                                */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sweep_freq_raster (ViSession vi,
							   			  ViReal64 freq)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* check parameter ranges */
	if (error = ri3152a_real_range (freq, RI3152A_SWEEP_MIN_FREQ_RAST, RI3152A_SWEEP_MAX_FREQ_RAST,
									VI_ERROR_PARAMETER2))
		return( error );

	/* send the command */
	error = viPrintf(vi, "%s%.3lf\n", (fast_mode[(ViInt16)index]) ? "1612A" : "SWE:FREQ:RAST ", freq);
	return (error);
}

/*=========================================================================*/
/* Function: Query Sweep Time                                              */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sweep_freq_raster (ViSession vi,
							   			  ViReal64 *freq)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
    ViUInt32  cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
		
	/* format the command */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1612A?" : "SWE:FREQ:RAST?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *freq = strtod(readbuf, NULL);

	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sweep Time                                                */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sweep_freq_marker (ViSession vi,
							   			  ViReal64 marker)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
	/* check parameter ranges */
	if (error = ri3152a_real_range (marker, RI3152A_SWEEP_MIN_FREQ_STOP, RI3152A_SWEEP_MAX_FREQ_STOP,
									VI_ERROR_PARAMETER2))
		return( error );

	/* send the command */
	error = viPrintf(vi, "%s%.3lf\n", (fast_mode[(ViInt16)index]) ? "1613A" : "SWE:FREQ:MARK ", marker);
	return (error);
}

/*=========================================================================*/
/* Function: Query Sweep Time                                              */
/* Description: This function generates a standard sine wave               */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  frequency -- the frequency of the sine wave                */
/*           :  amplitude -- the amplitude of the sine wave                */
/*           :  offset -- the DC offset of the sine wave                   */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sweep_freq_marker (ViSession vi,
							   			  ViReal64 *marker)
{
	ViStatus error;
	ViInt16 active_channel;
	ViReal64 max_freq, min_freq;
    ViUInt32  cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
		
	/* format the command */
	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1613A?" : "SWE:FREQ:MARK?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *marker = strtod(readbuf, NULL);

	return (VI_SUCCESS);
}


/*=========================================================================*/
/* This function selects the Waveform mode                                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sweep_function  (ViSession vi,
                                          		ViInt16 function_type)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */

    if (error = ri3152a_int_range (function_type, RI3152A_SINE,
                                    RI3152A_SQUARE, VI_ERROR_PARAMETER2))
        return( error );

    /* format the command and output it to the generator */

    switch ( function_type )
		{
		case RI3152A_SINE:		error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1604A0" : "SWE:FUNC SIN");
								break;

		case RI3152A_TRIANGLE:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1604A1" : "SWE:FUNC TRI");
								break;

		case RI3152A_SQUARE:	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1604A2" : "SWE:FUNC SQU");
								break;
								
		}

	return( error );
}


/*=========================================================================*/
/* This function reads what operating mode the generator is in.  If it is  */
/* in the standard waveform mode, it also checks which waveform is selected*/
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sweep_func_query (ViSession vi,
                            		 ViInt16 *function_type)
{
    ViStatus error;
    ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	/* query the operating mode */
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1604A?" : "SWE:FUNC?");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );

    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;


	/* check what the mode is */
	if (strncmp(readbuf, "SIN", 3) == 0)
		*function_type = RI3152A_SINE;
	else if (strncmp(readbuf, "TRI", 3) == 0)
		*function_type = RI3152A_TRIANGLE;
	else if (strncmp(readbuf, "SQU", 3) == 0)
		*function_type = RI3152A_SQUARE;
		
	return( VI_SUCCESS );
}


/*===================================================================================*/
/* This function generates a sequential waveform consisting of a series of waveform  */
/* segment.  They include : sine wave, ramp wave and sine^3 wave.  Each of the       */
/* waveform segments is repeated twice.                                              */
/*===================================================================================*/

ViStatus _VI_FUNC ri3152a_example_generate_seq_waveform (ViSession vi)


{
   ViStatus error;
   ViInt16 i;
   ViInt16 segment_number;
   ViUInt16 array[400];
   ViInt16 segment_array[] = {1,2,3};
   ViInt32 repeat_array[] = {3,2,1};
   double pi = 3.1415926;
   ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = ri3152a_output (vi, 1);
	if ( error < 0)
		return (error );
		
	for ( segment_number = 1; segment_number <= 3; segment_number ++ )
		{
		error =  ri3152a_define_arb_segment (vi, segment_number, 360);
		if ( error < 0)
			return (error);

		if ( segment_number == 1)
        	{  
        	/* sending sine wave ( segment # 1) */
			for ( i = 0; i< 360; i++)
				array[i] =  (((ViUInt16) (2048 + (2047* sin (i*pi/180))) & 0xFFF)) << 4;
			}
		else if ( segment_number == 2)
			{
			/* sending ramp wave ( segment # 2) */
			for ( i = 0; i< 360; i++)
				array[i] = (((ViUInt16) (i * 4095.0/360.0)) & 0xFFF) << 4;
			}
		else if ( segment_number == 3)
			{
			/* sending 4-step wave */
			for ( i = 0; i< 360; i++)
				{
				if (i < 90)
					array[i] = 0x6000;
				else if (i < 180)
					array[i] = 0x8000;
				else if (i < 270)
					array[i] = 0xA000;
				else
					array[i] = 0xC000;
				}
			}

		error = ri3152a_load_arb_data (vi,  segment_number, array, (ViInt32) 360);
		if ( error < 0)
			return (error);

		error = ri3152a_output_arb_waveform (vi, segment_number, 100.0E6, 5.0, 0.0, 0);
		if ( error < 0)
			return ( error );
       }


	error = ri3152a_define_sequence (vi, 3, segment_array, repeat_array);
	if ( error < 0)
		return (error );

	error = ri3152a_output_sequence_waveform (vi, 100.0E6, 5.0, 0.0, 
												RI3152A_SEQ_AUTO);
	if ( error < 0)
		return (error );


	return( error );
}

/*=========================================================================*/
/* This function sets the timeout for the instrument.                      */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_timeout(ViSession vi, ViAttrState timeout)
{
    /* set the time-out attribute */
    return( viSetAttribute (vi, VI_ATTR_TMO_VALUE, timeout) );
}

/*=========================================================================*/
/* This function clears the instrument.                                    */
/* This function is equivalent to ViClear() and exists solely for          */
/* compatibility with previous driver releases                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_clear (ViSession vi)
{
	ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
	
    /* send the instrument preset command */
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "15000" : "*CLS");
    
    return( error );
}

/*=========================================================================*/
/* This function triggers the instrument.                                  */
/* This function is equivalent to ViAssertTrigger() and exists solely for  */
/* compatibility with previous driver releases                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_trigger (ViSession vi)
{
    return( viAssertTrigger(vi, VI_TRIG_PROT_DEFAULT) );
}

/*=========================================================================*/
/* This function polls the instrument.                                     */
/* This function is equivalent to viReadSTB() and exists solely for        */
/* compatibility with previous driver releases                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_poll (ViSession vi, ViInt16 *response)
{
    ViUInt16 status;
    ViStatus error;

    error = viReadSTB(vi, &status);
    if (error < 0)
        return( error );

    *response = (ViInt16) status;

    return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Query Amplitude                                               */
/* Description: This function reads the presently programmed amplitude     */
/* Parameters:  instr_num -- the instrument session                        */
/*           : amplitude -- holds the amplitude (Vpp) upon return          */ 
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_amplitude(ViSession vi, ViPReal64 amplitude)
{
	ViStatus error;
	ViUInt32 cnt;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0500A?" : "VOLT?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;
		
    *amplitude = strtod(readbuf, NULL);

	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Query Amplitude                                               */
/* Description: This function reads the presently programmed amplitude     */
/* Parameters:  instr_num -- the instrument session                        */
/*           : amplitude -- holds the amplitude (Vpp) upon return          */ 
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_offset(ViSession vi, ViPReal64 offset)
{
	ViStatus error;
	ViUInt32 cnt;
	char cmd[256];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0501A?" : "VOLT:OFFS?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;
		
    *offset = strtod(readbuf, NULL);

	return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function returns the value of the current Voltage, Offset and      */
/* Frequency value as well as the status and type of the Filter assigned   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_status_query (ViSession vi, ViReal64* voltage_value,
                                       ViReal64* frequency_value,
                                       ViReal64* offset_value, ViInt16 *filter_type)
{
    ViStatus error;
    ViUInt32 cnt;
    ViInt16 filter_state;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;


    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0500A?" : "SOUR:VOLT?");
    if (error < 0)
        return (error);


  /*   read the reply back from the instrument */

    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );


    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;
    *voltage_value = strtod(readbuf, NULL);
  /*---------------------------------------------------*/

    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0400A?" : "SOUR:FREQ?");
    if (error < 0)
        return (error);


  /*   read the reply back from the instrument */

    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );


    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;

    *frequency_value = strtod(readbuf, NULL);
   /*----------------------------------------------------*/

    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0501A?" : "SOUR:VOLT:OFFS?");
    if (error < 0)
        return (error);


  /*   read the reply back from the instrument */

    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );


    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;

    *offset_value = strtod(readbuf, NULL);
   /*------------------------------------------------------*/

    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0202A?" : "OUTP:FILT:STAT?");
    if (error < 0)
        return (error);


    error = viScanf (vi, "%d", &filter_state);
    if (error < 0)
         return (error);

	/* For fast mode, query returns 1 if OFF and 0 if ON */
	/* For normal mode, query returns 0 if OFF and 1 if ON */
	if ((filter_state == 1 && !fast_mode[(ViInt16)index]) ||
	    (filter_state == 0 && fast_mode[(ViInt16)index]))    /* Low pass filter is on */
		{
	    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0201A?" : "OUTP:FILT:FREQ?");
        if (error < 0)
        return (error);

        error =  viScanf (vi, "%d", &filter_state);
        if (error < 0)
        return (error);

        switch (filter_state)
			{
			case 20: *filter_type = RI3152A_FILTER_20MHZ;
					 break;

			case 25: *filter_type = RI3152A_FILTER_25MHZ;						 
					 break;

			case 50: *filter_type = RI3152A_FILTER_50MHZ;
					 break;
			}
		}
	else
		*filter_type = RI3152A_FILTER_OFF;

	return( VI_SUCCESS );
}

/*=========================================================================*/
/* This function returns the value of the current Voltage, Offset and      */
/* Frequency value as well as the status and type of the Filter assigned   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_query_sampling_freq (ViSession vi, ViReal64* samp_freq)
{
    ViStatus error;
    ViUInt32 cnt;
    ViInt16 filter_state;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;


    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0410A?" : "FREQ:RAST?");
    if (error < 0)
        return (error);


  /*   read the reply back from the instrument */

    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );


    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;
    *samp_freq = strtod(readbuf, NULL);

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* This function reads what operating mode the generator is in.  If it is  */
/* in the standard waveform mode, it also checks which waveform is selected*/
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_mode_query (ViSession vi, ViInt16 *mode,
                            		 ViInt16 *standard_waveform)
{
    ViStatus error;
    ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	/* query the operating mode */
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0610A?" : "FUNC:MODE?");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );

    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;


	*standard_waveform = RI3152A_NON_STANDARD;
	
	/* check what the mode is */
	if (strncmp(readbuf, "USER", 4) == 0)
		*mode = RI3152A_MODE_ARB;
	else if (strncmp(readbuf, "SEQ", 3) == 0)
		*mode = RI3152A_MODE_SEQ;
	else if (strncmp(readbuf, "SWE", 3) == 0)
		*mode = RI3152A_MODE_SWEEP;
	else if (strncmp(readbuf, "FIX", 3) == 0)
		{
		*mode = RI3152A_MODE_STD;
		
	    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0600A?" : "FUNC:SHAPE?");
		if (error < 0)
			return( error );

		
		/* read the reply back from the instrument */
		if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
			return( error );

		/* null-byte terminate the reply */
		readbuf[(ViInt16) cnt] = 0;
		
		if (strncmp(readbuf, "SINC", 4) == 0)
			*standard_waveform = RI3152A_SINC;
		else if (strncmp(readbuf, "SIN", 3) == 0)
			*standard_waveform = RI3152A_SINE;
		else if (strncmp(readbuf, "SQU", 3) == 0)
			*standard_waveform = RI3152A_SQUARE;
		else if (strncmp(readbuf, "PUL", 3) == 0)
			*standard_waveform = RI3152A_PULSE;
		else if (strncmp(readbuf, "RAM", 3) == 0)
			*standard_waveform = RI3152A_RAMP;
		else if (strncmp(readbuf, "TRI", 3) == 0)
			*standard_waveform = RI3152A_TRIANGLE;
		else if (strncmp(readbuf, "EXP", 3) == 0)
			*standard_waveform = RI3152A_EXPONENTIAL;
		else if (strncmp(readbuf, "GAU", 3) == 0)
			*standard_waveform = RI3152A_GAUSSIAN;
		else if (strncmp(readbuf, "DC", 2) == 0)
			*standard_waveform = RI3152A_DC;
		else
			return( RI3152A_INVALID_REPLY );
		}
	else
		return( RI3152A_INVALID_REPLY );

	return( VI_SUCCESS );
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_share_memory_query (ViSession vi,
										ViPInt16 smem_state,
										ViPInt16 smem_mode)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1210A?" : "SMEM?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*smem_state = atoi (readbuf);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1200A?" : "SMEM:MODE?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	if (!(strncmp(readbuf, "WRI", 3))) *smem_mode = 1;	/* Write */
	else if (!(strncmp(readbuf, "REA", 3))) *smem_mode = 0;		/* Read */
	else return( RI3152A_INVALID_REPLY );
	
	return (VI_SUCCESS);
}




/*=========================================================================*/
/* This function reads the phase lock loop state, coarse and fine phase    */
/* adjustments, and external input frequency                               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_pll_query (ViSession vi, ViBoolean *on_off,
                           ViReal64 *coarsePhase, ViReal64 *finePhase,
                           ViReal64 *extFrequency)
{
	ViStatus error;
	ViInt32 long_index;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;


	/* query the on/off state of the phase lock loop */
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0920A?" : "PHASE2:LOCK?");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );

	/* if the first character is a '1', then it is ON */
	*on_off = (readbuf[0] == '1') ? VI_TRUE : VI_FALSE;

	/* query the coarse phase adjustment */
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0900A?" : "PHASE2:ADJ?");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );

	/* null-byte terminate the reply buffer */
	readbuf[cnt] = 0;
	
	/* convert the reply into a double-precision value */
	*coarsePhase = atof (readbuf);
	
	/* query the fine phase adjustment */
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0910A?" : "PHASE2:FINE?");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        return( error );

	/* null-byte terminate the reply buffer */
	readbuf[cnt] = 0;
	
	/* convert the reply into a double-precision value */
	*finePhase = atof (readbuf);
	
	/* query the external frequency (if phase lock is ON) */
	*extFrequency = 0.0;
	if (*on_off)
		{
	    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0411@?" : "FREQ:EXT?");
    	if (error < 0)
        	return (error);

		/* read the reply back from the instrument */
    	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
        	return( error );

		/* null-byte terminate the reply buffer */
		readbuf[cnt] = 0;
	
		/* convert the reply into a double-precision value */
		*extFrequency = atof (readbuf);
		}
		
	return( VI_SUCCESS );
}


/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sine_wave_phase (ViSession vi,  ViInt16 phase)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_int_range (phase, RI3152A_MIN_PHASE, 
                                              RI3152A_MAX_PHASE,
                                              VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0700A" : "SIN:PHAS ";
    
	error = viPrintf (vi, "%s%d\n", cmd, phase);
		
	return (error);
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sine_phase_query (ViSession vi,
										ViPInt16 phase)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0700A?" : "SIN:PHAS?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*phase = atoi (readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sine_wave_power (ViSession vi,  ViInt16 power)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_int_range (power, 1, 9,  VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0701A" : "SIN:POW ";
    
	error = viPrintf (vi, "%s%d\n", cmd, power);
		
	return (error);
}

/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sine_power_query (ViSession vi,
										ViPInt16 power)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0701A?" : "SIN:POW?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*power = atoi (readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_sinc_ncycle (ViSession vi,  ViInt16 ncycle)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_int_range (ncycle, RI3152A_MIN_CYCLE_NUMBER, RI3152A_MAX_CYCLE_NUMBER,  VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0790A" : "SINC:NCYC ";
    
	error = viPrintf (vi, "%s%d\n", cmd, ncycle);
		
	return (error);
}

/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_sinc_ncycle_query (ViSession vi,
										ViPInt16 ncycle)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0790A?" : "SINC:NCYC?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*ncycle = atoi (readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_triangle_phase (ViSession vi,  ViInt16 phase)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_int_range (phase, RI3152A_MIN_PHASE, 
                                              RI3152A_MAX_PHASE,
                                              VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0710A" : "TRI:PHAS ";
    
	error = viPrintf (vi, "%s%d\n", cmd, phase);
		
	return (error);
}




/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_triangle_phase_query (ViSession vi,
										ViPInt16 phase)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0710A?" : "TRI:PHAS?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*phase = atoi (readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_triangle_power (ViSession vi,  ViInt16 power)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_int_range (power, 1,  9,   VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0711A" : "TRI:POW ";
    
	error = viPrintf (vi, "%s%d\n", cmd, power);
		
	return (error);
}




/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_triangle_power_query (ViSession vi,
										ViPInt16 power)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0711A?" : "TRI:POW?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*power = atoi (readbuf);
	
	return (VI_SUCCESS);
}




/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_square_dcycle (ViSession vi,  ViInt16 dcycle)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_int_range (dcycle, RI3152A_MIN_DUTY_CYCLE, 
                                              RI3152A_MAX_DUTY_CYCLE,
                                              VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0720A" : "SQU:DCYC ";
    
	error = viPrintf (vi, "%s%d\n", cmd, dcycle);
		
	return (error);
}




/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_square_dcycle_query (ViSession vi,
										ViPInt16 dcycle)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0720A?" : "SQU:DCYC?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*dcycle = atoi (readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_pulse_data (ViSession vi,  ViReal64 delay,
										ViReal64 width, ViReal64 tran, ViReal64 trail)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_real_range (delay, RI3152A_MIN_DELAY_PULSE, 
                                              RI3152A_MAX_DELAY_PULSE,
                                              VI_ERROR_PARAMETER2))
		return( error );

	if (error = ri3152a_real_range (width, RI3152A_MIN_HIGH_TIME_PULSE, 
                                              RI3152A_MAX_HIGH_TIME_PULSE,
                                              VI_ERROR_PARAMETER3))
		return( error );
	if (error = ri3152a_real_range (tran, RI3152A_MIN_RISE_TIME_PULSE, 
                                              RI3152A_MAX_RISE_TIME_PULSE,
                                              VI_ERROR_PARAMETER4))
		return( error );
	if (error = ri3152a_real_range (trail, RI3152A_MIN_FALL_TIME_PULSE, 
                                              RI3152A_MAX_FALL_TIME_PULSE,
                                              VI_ERROR_PARAMETER5))
		return( error );
		
    cmd = (fast_mode[(ViInt16)index]) ? "0730A" : "PULS:DEL ";
    
	error = viPrintf (vi, "%s%f\n", cmd, delay);
	if (error < 0) return (error);
		
    cmd = (fast_mode[(ViInt16)index]) ? "0731A" : "PULS:WIDT ";
    
	error = viPrintf (vi, "%s%f\n", cmd, width);
	if (error < 0) return (error);
		
    cmd = (fast_mode[(ViInt16)index]) ? "0732A" : "PULS:TRAN ";
    
	error = viPrintf (vi, "%s%f\n", cmd, tran);
	if (error < 0) return (error);
		
    cmd = (fast_mode[(ViInt16)index]) ? "0733A" : "PULS:TRAN:TRA ";
    
	error = viPrintf (vi, "%s%f\n", cmd, trail);
	
	return (error);
}




/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_pulse_data_query (ViSession vi,
										ViPReal64 delay, ViPReal64 width,
										ViPReal64 tran, ViPReal64 trail)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0730A?" : "PULS:DEL?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *delay = strtod(readbuf, NULL);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0731A?" : "PULS:WIDT?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *width = strtod(readbuf, NULL);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0732A?" : "PULS:TRAN?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *tran = strtod(readbuf, NULL);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0733A?" : "PULS:TRAN:TRA?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *trail = strtod(readbuf, NULL);
	
	return (VI_SUCCESS);
}



/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_ramp_data (ViSession vi,  ViReal64 delay,
										ViReal64 tran, ViReal64 trail)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_real_range (delay, RI3152A_MIN_DELAY_RAMP, 
                                              RI3152A_MAX_DELAY_RAMP,
                                              VI_ERROR_PARAMETER2))
		return( error );

	if (error = ri3152a_real_range (tran, RI3152A_MIN_RISE_TIME_RAMP, 
                                              RI3152A_MAX_RISE_TIME_RAMP,
                                              VI_ERROR_PARAMETER3))
		return( error );
	if (error = ri3152a_real_range (trail, RI3152A_MIN_FALL_TIME_RAMP, 
                                              RI3152A_MAX_FALL_TIME_RAMP,
                                              VI_ERROR_PARAMETER4))
		return( error );
		
    cmd = (fast_mode[(ViInt16)index]) ? "0740A" : "RAMP:DEL ";
    
	error = viPrintf (vi, "%s%f\n", cmd, delay);
	if (error < 0) return (error);
		
    cmd = (fast_mode[(ViInt16)index]) ? "0741A" : "RAMP:TRAN ";
    
	error = viPrintf (vi, "%s%f\n", cmd, tran);
	if (error < 0) return (error);
		
    cmd = (fast_mode[(ViInt16)index]) ? "0742A" : "RAMP:TRAN:TRA ";
    
	error = viPrintf (vi, "%s%f\n", cmd, trail);
	
	return (error);
}


/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_ramp_data_query (ViSession vi,
										ViPReal64 delay,
										ViPReal64 tran, ViPReal64 trail)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0740A?" : "RAMP:DEL?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *delay = strtod(readbuf, NULL);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0741A?" : "RAMP:TRAN?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *tran = strtod(readbuf, NULL);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0742A?" : "RAMP:TRAN:TRA?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *trail = strtod(readbuf, NULL);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_gauss_exp (ViSession vi,  ViInt16 exp)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_int_range (exp, RI3152A_MIN_EXPONENT_GAU, 
                                              RI3152A_MAX_EXPONENT_GAU,
                                              VI_ERROR_PARAMETER2))
		return( error );

    cmd = (fast_mode[(ViInt16)index]) ? "0750A" : "GAUS:EXP ";
    
	error = viPrintf (vi, "%s%d\n", cmd, exp);
		
	return (error);
}




/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_gauss_exp_query (ViSession vi,
										ViPInt16 exp)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0750A?" : "GAUS:EXP?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*exp = atoi (readbuf);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Set Sine Wave Phase                                           */
/* Description: This function programs only the phase of the sine wave     */
/* Parameters:  instr_num -- the instrument session                        */
/*           :  phase -- the phase offset of the sine wave                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_exponential_exp (ViSession vi,  ViInt16 exp)
{
    ViStatus error;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* check parameter ranges */
	if (error = ri3152a_int_range (exp, RI3152A_MIN_EXPONENT_EXP, 
                                              RI3152A_MAX_EXPONENT_EXP,
                                              VI_ERROR_PARAMETER2))
		return( error );

	if (exp == 0.0)
		return( VI_ERROR_PARAMETER2 );

    cmd = (fast_mode[(ViInt16)index]) ? "0760A" : "EXP:EXP ";
    
	error = viPrintf (vi, "%s%d\n", cmd, exp);
		
	return (error);
}




/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_exponential_exp_query (ViSession vi,
										ViReal64 *exp)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0760A?" : "EXP:EXP?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

    *exp = strtod(readbuf, NULL);

	return (VI_SUCCESS);
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_dc_amplitude_query (ViSession vi,
										ViPInt16 amplitude)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0770A?" : "DC?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*amplitude = atoi (readbuf);
	
	return (VI_SUCCESS);
}



/*=========================================================================*/
/* This function queries the 10 MHz reference oscillator source            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_amplitude_modulation_query (ViSession vi,
										ViPInt16 am_val, ViInt32 *internal_freq)
{
	ViStatus error;
	ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0780A?" : "AM?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*am_val = atoi (readbuf);

	error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "0781A?" : "AM:INT:FREQ?");

	/* read the reply back from the instrument */
	if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
		return( error );

	/* null-byte terminate the reply */
	readbuf[(ViInt16) cnt] = 0;

	*internal_freq = strtol (readbuf, NULL, 10);
	
	return (VI_SUCCESS);
}


/*=========================================================================*/
/* Function: Read Status Byte                                              */
/* Purpose:  This function reads the IEEE-488.2 defined status byte        */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_read_status_byte (ViSession vi, ViInt16 *statusByte)
{
    ViStatus error;
    ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1508@?" : "*STB?");
    if (error < 0)
    	return( error );
    	
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
    	return( cnt );

	*statusByte = (ViInt16) atoi( readbuf );
	
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Set SRE Register                                              */
/* Purpose:  This function programs the IEEE-488.2 defined SRE register    */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_SRE (ViSession vi, ViInt16 SRERegister)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
    if (error = ri3152a_int_range(SRERegister, 0, 255, VI_ERROR_PARAMETER2))
    	return( error );
    	
    return( viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1503A" : "*SRE ", SRERegister & 0x00BF) );
}

/*=========================================================================*/
/* Function: Read SRE Register                                             */
/* Purpose:  This function reads the IEEE-488.2 defined SRE Register       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_read_SRE (ViSession vi, ViInt16 *SRERegister)
{
    ViStatus error;
    ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1503A?" : "*SRE?");
    if (error  < 0)
    	return( error );
    	
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
    	return( cnt );

	*SRERegister = (ViInt16) atoi( readbuf );
	
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Read ESR Register                                             */
/* Purpose:  This function reads the IEEE-488.2 defined ESR Register       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_read_ESR (ViSession vi, ViInt16 *ESRRegister)
{
    ViStatus error;
    ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1504@?" : "*ESR?");
    if (error  < 0)
    	return( error );
    	
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
    	return( cnt );

	*ESRRegister = (ViInt16) atoi( readbuf );
	
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Set ESE Register                                              */
/* Purpose:  This function programs the IEEE-488.2 defined ESE Register    */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_set_ESE (ViSession vi, ViInt16 ESERegister)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
    if (error = ri3152a_int_range(ESERegister, 0, 255, VI_ERROR_PARAMETER2))
    	return( error );
    	
    return( viPrintf(vi, "%s%d\n", (fast_mode[(ViInt16)index]) ? "1501A" : "*ESE ", ESERegister) );
}

/*=========================================================================*/
/* Function: Read ESE Register                                             */
/* Purpose:  This function reads the IEEE-488.2 defined ESE Register       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_read_ESE (ViSession vi, ViInt16 *ESERegister)
{
    ViStatus error;
    ViUInt32 cnt;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;
    
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1501A?" : "*ESE?");
    if (error  < 0)
    	return( error );
    	
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3152A_READ_LEN, &cnt)) < 0)
    	return( cnt );

	*ESERegister = (ViInt16) atoi( readbuf );
	
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Self-Test                                                     */
/* Purpose:  This function executes the instrument self-test and returns   */
/* the result.                                                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_self_test (ViSession vi,
                                    ViInt16   * test_result,
                                    ViChar    * test_message)
{
    ViStatus error;
    ViUInt32 old_timeout;
    ViUInt32 cnt;
    ViChar reply[100];
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    error = viGetAttribute (vi, VI_ATTR_TMO_VALUE, &old_timeout);
    if (error < 0) return error;

    error = viSetAttribute (vi, VI_ATTR_TMO_VALUE, (ViUInt32)10000);
    if  (error != VI_SUCCESS)
    {
        if (error > 0)
            error = RI3152A_NSUP_ATTR_VALUE;
        return error;
    }


    /* send the *TST? test query */
    error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1500@?" : "*TST?");
    if (error < 0)
        {
        viSetAttribute (vi, VI_ATTR_TMO_VALUE, old_timeout);
        return( error );
        }

	error = viRead(vi, (unsigned char *) reply, (ViInt32) sizeof(reply), &cnt);
	if (error < 0)
		return( error );
		
	reply[cnt-1] = 0;
	*test_result = atoi(reply);

    /* restore the time-out and instrument set-up */
    viSetAttribute (vi, VI_ATTR_TMO_VALUE, old_timeout);

    switch( *test_result )
        {
        case 0:     *test_message= '\0';    break;
        default: strcpy(test_message, "self-test failed, error code: ");
        		 strcat(test_message, reply);
        }

    return VI_SUCCESS;
}

/*=========================================================================*/
/* Function: Error Query                                                   */
/* Purpose:  This function queries the instrument error queue.             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_error_query (ViSession vi,
                                      ViInt32   *error_status,
                                      ViString  error_message)
{
    ViStatus error;
    ViUInt32 cnt;
    ViChar *strptr;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* set up default of "No Error" */
    *error_status = 0;
    *error_message = '\0';

    /* send the error query */
    if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1430@?" : "SYST:ERR?")) < 0)
        return( error );

    if ((error = viRead (vi, (unsigned char *) readbuf, 256, &cnt)) < 0)
    	return( error );
    
    /* null-byte terminate */
    readbuf[cnt] = '\0';
    
    /* convert the first field (before comma) to an integer */
    *error_status = atoi(readbuf);
    
    /* find the first '"' */
    strptr = readbuf;
    while (*strptr && *strptr != '"')
    	++strptr;
    	
    if ( *strptr != '"' )
    	return( RI3152A_INVALID_REPLY );

    ++strptr;
 
    /* copy up to 80 characters of the error message */
    strncpy(error_message, strptr, 80);
    
    cnt = strlen(error_message);
    
    /* remove the last '"' found in the error message */
    strptr = error_message + cnt - 1;
    while (strptr > error_message && (*strptr != '"'))
    	--strptr;
    	
    if (strptr > error_message)
    	*strptr = '\0';
 
    return VI_SUCCESS;
}

/*=========================================================================*/
/* Function: Error Query                                                   */
/* Purpose:  This function queries the instrument error queue.             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_version_query (ViSession vi,
                                      ViString  version)
{
    ViStatus error;
    ViUInt32 cnt;
    ViChar *strptr;
    ViChar *cmd, *cp;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* send the version query */
    if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1440@?" : "SYST:VERS?")) < 0)
        return( error );
        
    if ((error = viRead (vi, (unsigned char *) readbuf, 10, &cnt)) < 0)
    	return( error );

    /* null-byte terminate/remove any <CR> or <LF> and ; */
    cp = readbuf + cnt - 1;
    
    while ( *cp && (isspace(*cp) || *cp == ';'))
        {
        *cp = '\0';
        --cp;
        }
        
    strcpy (version, readbuf);
    
    return VI_SUCCESS;
}


/*=========================================================================*/
/* Function: Error Query                                                   */
/* Purpose:  This function queries the instrument error queue.             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_option_query (ViSession vi,
                                      ViString  option)
{
    ViStatus error;
    ViUInt32 cnt;
    ViChar *strptr;
    ViChar *cmd;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* send the option query */
    if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1507@?" : "*OPT?")) < 0)
        return( error );
        
    cnt = (ViUInt32) (sizeof(readbuf) - 1);
    if ((error = viRead (vi, (unsigned char *) readbuf, cnt, &cnt)) < 0)
    	return( error );
    
    /* null-byte terminate */
    readbuf[cnt-2] = '\0';
    
    strcpy (option, readbuf);
    
    return VI_SUCCESS;
}


/*=========================================================================*/
/* Function: Revision                                                      */
/* Purpose:  This function returns the driver and instrument revisions.    */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_revision_query (ViSession vi,
                                         ViString  driver_rev,
                                         ViString  instr_rev)
{
    ViStatus error;
    ViUInt32 cnt;
    ViChar *cp;
    ViChar *cmd;
    static char *ri3152a_idn_response = "Racal Instruments,3152A,0,";
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* send the *IDN? query */
    if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "1506@?" : "*IDN?")) < 0)
        return( error );
        
    /* read the reply */
    if ((error = viRead (vi, (unsigned char *) readbuf, RI3152A_MAX_CMD, &cnt)) < 0)
        return( error );

    /* null-byte terminate/remove any <CR> or <LF> and ; */
    cp = readbuf + cnt - 1;
    
    while ( *cp && (isspace(*cp) || *cp == ';'))
        {
        *cp = '\0';
        --cp;
        }
        
    /* make sure this equals the expected string, up to the firmware revision */
    if (strncmp(readbuf, ri3152a_idn_response, strlen(ri3152a_idn_response)) != 0)
        return( RI3152A_INVALID_REPLY );
    
    /*
     * return the instrument revision and the driver revision
     */
	cp = readbuf + strlen(ri3152a_idn_response);
    strcpy(instr_rev, cp);
    strcpy(driver_rev, RI3152A_REVISION);

    return VI_SUCCESS;
}


/*=========================================================================*/
/* Function: Error Message                                                 */
/* Purpose:  This function returns a text message for a corresponding      */
/* instrument driver error code.                                           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_error_message (ViSession vi,
                                        ViStatus  error_code,
                                        ViString  message)
{
    switch( error_code )
    {
        case VI_SUCCESS:
            strcpy(message, "No error");
            break;

        case VI_ERROR_FAIL_ID_QUERY:
            strcpy(message, "Instrument identification query failed");
            break;

        case VI_ERROR_INV_RESPONSE:
            strcpy(message, "Error interpreting instrument response");
            break;

        case VI_ERROR_PARAMETER1:
            strcpy(message, "First parameter is out of range");
            break;

        case VI_ERROR_PARAMETER2:
            strcpy(message, "Second parameter is out of range");
            break;

        case VI_ERROR_PARAMETER3:
            strcpy(message, "Third parameter is out of range");
            break;

        case VI_ERROR_PARAMETER4:
            strcpy(message, "Fourth parameter is out of range");
            break;

        case VI_ERROR_PARAMETER5:
            strcpy(message, "Fifth parameter is out of range");
            break;

        case VI_ERROR_PARAMETER6:
            strcpy(message, "Sixth parameter is out of range");
            break;

        case VI_ERROR_PARAMETER7:
            strcpy(message, "Seventh parameter is out of range");
            break;

        case VI_ERROR_PARAMETER8:
            strcpy(message, "Eighth parameter is out of range");
            break;

        case VI_ERROR_PARAMETER9:
            strcpy(message, "Ninth parameter is out of range");
            break;

        case VI_ERROR_PARAMETER10:
            strcpy(message, "Tenth parameter is out of range");
            break;


        case RI3152A_MAX_INSTR_ERROR:
                strcpy(message, "There are more than the maximum (RI3152A_MAX_INSTR) sessions open.  Use ri3152a_close to free up space.");
                break;

        case RI3152A_NSUP_ATTR_VALUE:
                strcpy(message, "A call to viSetAttribute returned an unsupported value warning");
                break;

        case RI3152A_INVALID_REPLY:
                strcpy(message, "Invalid reply received");
                break;

		case RI3152A_AMPLITUDE_ERROR:
				strcpy(message, "Invalid amplitude");
				break;

		case RI3152A_OFFSET_ERROR:
				strcpy(message, "Invalid offset");
				break;

		case RI3152A_AMPL_OFFSET_ERROR:
                strcpy(message, "The combination of the amplitude and offset is invalid");
                break;

        case RI3152A_CANT_OPEN_FILE:
                strcpy(message, "Attempt to open the specified file failed");
                break;

        case RI3152A_PERCENT_ERROR1:
               strcpy(message, "The total of delay + rise_time + high_time + fall_time > 100%");
               break;

        case RI3152A_PERCENT_ERROR2:
               strcpy(message, "The total of delay + rise_time + fall_time > 100%");
               break;

		case RI3152A_INVALID_FILE_DATA:
				strcpy(message, "The data in the file is not in the range -2048 to +2047");
				break;

		case RI3152A_WAVECAD_FORMAT_1:
				strcpy(message, "WaveCAD file format error (Missing '#Param' line)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_2:
				strcpy(message, "WaveCAD file form (Command count in '#Param' section is too long)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_3:
				strcpy(message, "WaveCAD file format error (Missing '#Gpib' line)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_4:
				strcpy(message, "WaveCAD file format error (Too many commands in '#Gpib' section)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_5:
				strcpy(message, "WaveCAD file format error (Missing '#Sequencer' line)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_6:
				strcpy(message, "WaveCAD file format error (Missing 'Link#' at beginning of line)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_7:
				strcpy(message, "WaveCAD file format error (Link number is not in range 1 to 1024)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_8:
				strcpy(message, "WaveCAD file format error (Command ID in '#Gpib' section not found in '#Param' section)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_9:
				strcpy(message, "WaveCAD file format error (SCPI command is too long)");
				break;
				
		case RI3152A_WAVECAD_FORMAT_10:
				strcpy(message, "WaveCAD file format error (SCPI command parameter is too long)");
				break;

		case RI3152A_WAVECAD_FORMAT_11:
				strcpy(message, "WaveCAD file format is not error (Missing '|' between SCPI command parameters)");
				break;
				
		case RI3152A_BINARY_DOWNLOAD_FAILED:
				strcpy(message, "Not ready to download binary data through A24 memory");
				break;

		case RI3152A_PHASE_LOCK_NOT_SUPP:
				strcpy(message,"Phase lock mode supported only on 3152, not on 3152a");
				break;

		case RI3152A_PHASE_MODE_CONFLICT:
				strcpy(message, "Phase Adjust not allowed in Sequence Mode");
				break;

		case RI3152A_LOAD_BINARY_NOT_SUPP:
				strcpy(message,"Load Sequence Binary supported only on 3152A");
				break;

		case RI3152A_BINARY_NOT_MULT4:
				strcpy(message,"Load Waveform Binary Block Data is not a multiple of 4");
				break;
				
		case RI3152A_OUT_OF_MEMORY:
				strcpy(message, "No available memory");
				break;
		default:
            if(viStatusDesc(vi, error_code, message) != VI_SUCCESS)
              {
                 strcpy(message, "Unknown error for ri3152a");
                      return (VI_WARN_UNKNOWN_STATUS);
              }
            break;

   }
    return VI_SUCCESS;
}

/*=========================================================================*/
/* Function: Reset                                                         */
/* Purpose:  This function resets the instrument.                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_reset (ViSession vi)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    error = viClear(vi);
    if (error < 0)
        return( error );


    /* send the instrument preset command */
    if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "14200" : "*RST")) < 0)
        return( error );
        
	fast_mode[(ViInt16)index] = VI_FALSE;        

    return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Operation Complete                                            */
/* Purpose:  This function sends operation complete to the instrument      */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_opc (ViSession vi)
{
    ViStatus error;
    ViAttrState index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error < 0) return error;

    /* send the instrument preset command */
    if ((error = viPrintf(vi, "%s\n", (fast_mode[(ViInt16)index]) ? "15020" : "*OPC")) < 0)
        return( error );

    return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Close                                                         */
/* Purpose:  This function closes the instrument.                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3152a_close (ViSession vi)

{
    ViAttrState index;
    ViStatus error;

	error = ri3152a_change_mode (vi, VI_FALSE);
	
    /* Reset the "initialized" array  for instrument */
    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error == VI_SUCCESS)
    {
        initialized[(ViInt16)index] = RI3152A_NO_SESSION;
        fast_mode[(ViInt16)index] = VI_FALSE;
        return viClose (vi);
    }

    return error;
}

/*====== Example Functions ================================================*/



/*====== Utility Routines ==================================================*/
/*=========================================================================*/
/* This function checks a real number to see if it lies between a minimum  */
/* and maximum value.  If the value is out of range, set the global error  */
/* variable to the value err_code.  If the value is OK, error = 0.  The    */
/* return value is equal to the global error value.                        */
/*=========================================================================*/
static ViStatus ri3152a_real_range (ViReal64 val,
                                   ViReal64 min,
                                   ViReal64 max,
                                   ViStatus err_code)
{
    ViStatus error;

    if ((val < min) || (val > max))
        error = err_code;
    else
        error = (ViStatus) 0;

  return( error );
}

/*==========================================================================*/
/* This function checks an integer to see if it lies between a minimum      */
/* and maximum value.  If the value is out of range, set the global error   */
/* variable to the value err_code.  If the value is OK, error = 0.  The     */
/* return value is equal to the global error value.                         */
/*==========================================================================*/
static ViStatus ri3152a_int_range (ViInt16 val,
                                  ViInt16 min,
                                  ViInt16 max,
                                  ViStatus err_code)
{
    ViStatus error;

    if (val < min || val > max)
        error = err_code;
    else
        error = (ViStatus) 0;

    return( error );
}

/*==========================================================================*/
/* This function checks a long integer to see if it lies between a minimum  */
/* and maximum value.  If the value is out of range, set the global error   */
/* variable to the value err_code.  If the value is OK, error = 0.  The     */
/* return value is equal to the global error value.                         */
/*==========================================================================*/
static ViStatus ri3152a_int32_range (ViInt32 val,
                                    ViInt32 min,
                                    ViInt32 max,
                                    ViStatus err_code)
{
    ViStatus error;

    if (val < min || val > max)
        error = err_code;
    else
        error = (ViStatus) 0;

    return( error );
}

/*==========================================================================*/
/* This function checks a boolean to see if it lies between a minimum       */
/* and maximum value.  If the value is out of range, set the global error   */
/* variable to the value err_code.  If the value is OK, error = 0.  The     */
/* return value is equal to the global error value.                         */
/*==========================================================================*/
static ViStatus ri3152a_boolean_range (ViBoolean val,
                                      ViStatus err_code)

{
    ViStatus error;

    if (val < VI_FALSE || val > VI_TRUE)
        error = err_code;
    else
        error = (ViStatus) 0;

    return( error );
}

/*=========================================================================*/
/* Function: Initialize Clean Up                                           */
/* Purpose:  This function is used only by the ri1260_init function.  When */
/*           an error is detected this function is called to close the     */
/*           open resource manager and instrument object sessions and to   */
/*           set the instrSession that is returned from ri1260_init to     */
/*           VI_NULL.                                                      */
/*=========================================================================*/
static ViStatus ri3152a_initCleanUp (ViSession rmSession, ViSession *vi,
    ViInt16 index, ViStatus currentStatus)
{
    viClose (*vi);
    viClose (rmSession);
    *vi = VI_NULL;
    initialized[index] = RI3152A_NO_SESSION;
    return (currentStatus);
}

/*=========================================================================*/
/* Function: ri3152a_get_mem_size()                                         */
/* Purpose:  This function gets the maximum memory (for a arbitrary data   */
/*           segment) for the specified 3152a.  The 3152a may have 64K or    */
/*           512K words of memory for arb data.  There are 2K less points  */
/*           available to accommodate sequences, etc.                      */
/*=========================================================================*/
static ViInt32 ri3152a_get_mem_size(ViSession vi)
{
	int i;
	
	for (i = 0;  i < RI3152A_MAX_INSTR;  ++i)
		if (initialized[i] == vi)
			{
			if (large_memory[i])
				return( RI3152A_MAX_512K_SEG_SIZE );
			else
				return( RI3152A_MAX_64K_SEG_SIZE );
			}

	/* default case -- let VISA I/O report invalid session */
	return( RI3152A_MAX_64K_SEG_SIZE );
}


/*=========================================================================*/
/* Function: ri3152a_check_ampl_and_offset()                                */
/* Purpose:  To check that the amplitude and offset for a standard         */
/*           waveform are valid.  The 3152a has three output ranges which   */
/*           are automatically selected by the amplitude:  +/- 80 mV,      */
/*           +/- 800 mV, and +/- 8V.  The amplitude + offset must lie in   */
/*           the selected range.                                           */
/*=========================================================================*/
static ViStatus ri3152a_check_ampl_and_offset(ViSession vi,
											 ViReal64 amplitude, 
                                             ViReal64 offset)
{
	ViReal64 half_amplitude;
	ViReal64 offset_absolute_value;
	ViInt32 long_index;
	ViInt16 index;
	ViStatus error;
	
	
	if (amplitude > RI3152A_MAX_AMPLITUDE || amplitude < RI3152A_MIN_AMPLITUDE)
		return( RI3152A_AMPLITUDE_ERROR );
		
	if (offset > RI3152A_MAX_OFFSET || offset < RI3152A_MIN_OFFSET)
		return( RI3152A_OFFSET_ERROR );
		
    /* get the index into driver tables for this instance of the instrument */
    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &long_index);
    if (error < 0) return error;

	/* check if we are using the old style tolerance calculations */
    index = (ViInt16) long_index;

	half_amplitude = amplitude / 2.0;
	offset_absolute_value = fabs(offset);
	
	if (tight_range_checking[index])
		{
		if (half_amplitude <= 0.08)
			{
			if (half_amplitude + offset_absolute_value > 0.08)
				return( RI3152A_AMPL_OFFSET_ERROR );
			}
		else if (half_amplitude <= 0.8)
			{
			if (half_amplitude + offset_absolute_value > 0.8)
				return( RI3152A_AMPL_OFFSET_ERROR );
			}
		else
			{
			if (half_amplitude + offset_absolute_value > 8.0)
				return( RI3152A_AMPL_OFFSET_ERROR );
			}
		}
	else /* newer, loose range checking */
		{
		if (half_amplitude + offset_absolute_value > 8.0)
			return( RI3152A_AMPL_OFFSET_ERROR );
		}
		
	return( VI_SUCCESS );
}


/*=========================================================================*/
/* Function: ri3152a_get_next_data_point()                                  */
/* Purpose:  This function takes the place of fscanf() which is not        */
/*           not supported by the Microsoft C++ libraries used for a DLL   */
/* Returns:  1 if a valid number is found, 0 otherwise                     */
/*=========================================================================*/
static ViInt16 ri3152a_get_next_data_point(FILE *fp, ViInt32 *data_point)
{                              
	/* maximum file line is 256 characters!!! */
	static char filebuf[256];
	static char *dataptr = 0;
	short value;
	ViBoolean is_negative;
	          
	/* check if this is the first read of the file */
	/* if it is, then read the next buffer of data from the file */
	if (global_file_flag == 0)
		{
		global_file_flag = 1;
		if (!fgets(filebuf, sizeof(filebuf), fp))
			return( 0 );
			
		dataptr = filebuf;
		}
		

	/* did we get to the end of the line last time */
	if (dataptr == 0 || *dataptr == 0)
		{
		if (!fgets(filebuf, sizeof(filebuf), fp))
			return( 0 );       
			
		dataptr = filebuf;
		}
		
	/* get the next integer and advance the character pointer */
	/* for the next time this function is called */
	while ( isspace(*dataptr) )
		++dataptr;
	
	if ( ! *dataptr )
		return(0);
	                      
	is_negative = 0;
	
	if (*dataptr == '+')
		++dataptr;
	else if (*dataptr == '-')
		{
		is_negative = 1;
		++dataptr;
		}
		               
	value = 0;
	while (isdigit(*dataptr))
		{
		value = value * 10L + (*dataptr - '0');
		++dataptr;
		}
		
	while (isspace(*dataptr))
		++dataptr;

	if (is_negative)
		value = -value;
				
	*data_point = value;
	
	return( 1 );
}

/*=========================================================================*/
/* Function: ri3152a_parse_wavecad_state                                    */
/* Purpose:  Parses a state entry from the WaveCAD file                    */
/*=========================================================================*/
static ViStatus ri3152a_parse_wavecad_state(WaveCADCmdStruct *table_entry, 
                                    		char *fileline)
{
	char *ptr;
	char *dont_care;

	/*
	 * Each state line has the format (with a leading space):
	 *  <Description> <Command ID> <Present Value> <Default> <Min> <Max>
	 * only the <Command ID> and <Present Value> are of interest
	 * to the driver
	 */
	ptr = fileline + 1;
	while (*ptr && *ptr != ' ')
		++ptr;

	/*
	 * skip to command ID field
 	 */
 	while (*ptr == ' ')
		++ptr;

	table_entry -> cmd_id = atoi(ptr);

	/*
 	 * skip to present value field
	 */
	while (*ptr && *ptr != ' ')
		++ptr;

	while (*ptr == ' ')
		++ptr;

	table_entry -> value = strtod(ptr, &dont_care);

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* Function: ri3152a_parse_wavecad_cmd                                      */
/* Purpose:  Parses a command entry from the WaveCAD file                  */
/*=========================================================================*/
static ViStatus ri3152a_parse_wavecad_cmd(WaveCADCmdStruct cmd_table[], 
                                  	int cmd_count, char *fileline)
{
	char *ptr, *dest;
	int command_id, count, len;
	WaveCADCmdStruct *table_entry;


	/*
	 * Each command line has the format (with one or more leading spaces):
	 *  <Description> <Command ID> <Command> <Command Parameters>
	 * the <Command ID> and <Command Parameters> are of interest
	 * The <Command ID> must be located in the table to associate
	 * it with the present value to use for the command
	 */
	ptr = fileline;
	while (*ptr == ' ')
		++ptr;
		
	while (*ptr && *ptr != ' ')
		++ptr;

	/*
	 * skip to command ID field
 	 */
 	while (*ptr == ' ')
		++ptr;

	command_id = atoi(ptr);

	/*
	 * find the specified command in the command table
	 */
	table_entry = cmd_table;
	for (count = 0;  count < cmd_count;  ++count)
		{
		if (command_id == table_entry -> cmd_id)
			break;

		++table_entry;
		}

	if (count >= cmd_count)
		return( RI3152A_WAVECAD_FORMAT_8 );


	/*
 	 * skip to the command text field
	 */
	while (*ptr && *ptr != ' ')
		++ptr;

	while (*ptr == ' ')
		++ptr;

	/*
	 * copy the command text into the command table
	 */
	dest = table_entry -> cmd_text;
	len = 0;
	while ((*ptr != ' ') && (len < 30))
		{
		*dest++ = *ptr++;
		++len;
		}

	if (len >= 30)
		return( RI3152A_WAVECAD_FORMAT_9 );

	*dest = '\0';

	/*
 	 * skip to the command parameter field
	 */
	while (*ptr == ' ')
		++ptr;

	/*
	 * copy the command parameters into the command table
	 */
	dest = table_entry -> cmd_param;
	len = 0;
	while (*ptr && (*ptr != ' ') && (len < 50))
		{
		*dest++ = *ptr++;
		++len;
		}

	if (len >= 50)
		return( RI3152A_WAVECAD_FORMAT_10 );

	*dest = '\0';

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* Function: ri3152a_parse_wavecad_seq                                      */
/* Purpose:  Parses a sequence entry from the WaveCAD file                 */
/*=========================================================================*/
static ViStatus ri3152a_parse_wavecad_seq(char *fileline, 
                                  	ViInt16 *link, 
                                  	ViInt16 *segment, 
                                  	ViInt32 *loop_count,
                                  	ViInt32 *segment_length,
                                  	char *wave_file_name)
{
	char *ptr;
	char *dest;

	/*
	 * Each sequence line has the format (with one or more leading spaces):
	 *   Link# <Link #> <Segment #> <Loop Count> <Segment Length> <File Name>
	 * only the <Command ID> and <Present Value> are of interest
	 * to the driver
	 */
	ptr = fileline;
	while (*ptr == ' ')
		++ptr;

	while (*ptr && *ptr != ' ')
		++ptr;

	/*
	 * skip to Link number field
 	 */
 	while (*ptr == ' ')
		++ptr;

	*link = (ViInt16) atoi(ptr);

	/*
 	 * skip to Segment Number field
	 */
	while (*ptr && *ptr != ' ')
		++ptr;

	while (*ptr == ' ')
		++ptr;

	*segment = (ViInt16) atoi(ptr);

	/*
 	 * skip to Loop Count field
	 */
	while (*ptr && *ptr != ' ')
		++ptr;

	while (*ptr == ' ')
		++ptr;

	*loop_count = atoi(ptr);

	/*
 	 * skip to Segment Length
	 */
	while (*ptr && *ptr != ' ')
		++ptr;

	while (*ptr == ' ')
		++ptr;

	*segment_length = atoi(ptr);

	/*
 	 * skip to waveform file name field
	 */
	while (*ptr && *ptr != ' ')
		++ptr;

	while (*ptr == ' ')
		++ptr;

	dest = wave_file_name;

	while (*ptr && *ptr != ' ' && *ptr != '\n')
		*dest++ = *ptr++;

	*dest = '\0';
	
	return( VI_SUCCESS );
}


/*=========================================================================*/
/* Function: ri3152a_create_path_name                                       */
/* Purpose:  Creates a full path name from the specified wave_file_name    */
/*           and the file_name.  The path component of the file name is    */
/*           used as a prefix for the wave_file_name                       */
/*=========================================================================*/
static ViStatus ri3152a_create_path_name(char *wave_file_name, char *file_name)
{
	char hold_file_name[256];
	char *ptr;

	/*
	 * hold the base file name of the waveform file
	 */
	strcpy(hold_file_name, wave_file_name);

	/*
	 * look for the last occurrence of the path separator
	 */
	ptr = file_name + strlen(file_name) - 1;
	while ((ptr > file_name) && *ptr != '\\' && *ptr != ':')
		--ptr;

	if (ptr > file_name)
		{
		strncpy(wave_file_name, file_name, (ptr - file_name) + 1);
		wave_file_name[ptr-file_name+1] = 0;
		strcat(wave_file_name, hold_file_name);
		}

	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: ri3152a_output_wavecad_commands                                */
/* Purpose:  Outputs each of the commands in the command table using the   */
/*           present value as found in the WaveCAD file                    */
/*=========================================================================*/
static ViStatus ri3152a_output_wavecad_commands(ViSession vi, 
                                        	WaveCADCmdStruct cmd_table[], 
                                        	int cmd_count)
{
	char param[50];
	char *start, *end, *ptr;
	WaveCADCmdStruct *table_entry;
	int index;
	int int_choice;
	ViStatus error;
    ViAttrState attr_index;


    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &attr_index );
    if (error < 0) return error;

	error = 0;
	table_entry = cmd_table;
	for (index = 0;  index < cmd_count;  ++index, ++table_entry)
		{
		if (table_entry -> cmd_text[0])
			{
			/*
			 * check the parameter field to see if this
			 * is a numeric value or a one-of-N choice
			 * SPECIAL CASE:  The TTLTx command should be OUTPUT:TTLT%d ON
			 */
			if ( !strncmp(table_entry -> cmd_text, ":TTLTx", 6) )
				{
				for (index = 0;  index < 8;  ++index)
					{
					if (index != table_entry -> value)
						{
						error = viPrintf(vi, ":OUTP:TTLT%d OFF\n", index);
						}
					else
						{
						error = viPrintf(vi, ":OUTP:TTLT%d ON\n", index);
						}
						
					if (error < 0)
						break;															
					}
				}		
			else if (!strncmp(table_entry -> cmd_param, "NR3", 3))
				{
				error = viPrintf(vi, "%s%.5g\n",
						table_entry -> cmd_text,
						table_entry -> value);
				}
			else
				{
				/*
				 * check which one of N is selected
				 */
				int_choice = (int) table_entry -> value;
				
				/*
				 * skip to the parameter of choice (separated by '|')
				 */
				start = table_entry -> cmd_param;
				while( int_choice )
					{
					while (*start && *start != '|')
						++start;
						
					if (*start != '|')
						return( RI3152A_WAVECAD_FORMAT_11 );
					else
						++start;
						
					--int_choice;
					}

				end = start + 1;
				while (*end && *end != '|' && *end != ' ' && *end != '\n')
					++end;
					
				--end;
				
				ptr = param;
				while (start <= end)
					*ptr++ = *start++;

				*ptr = 0;
				
				error = viPrintf(vi, "%s %s\n", table_entry -> cmd_text,
												param);
				}
				
			if (error < 0)
				return( error );
			}
		}
				

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* Function: ri3152a_itoa                                                   */
/* Purpose:  converts a (positive) numeric to a string                     */
/*=========================================================================*/
static ViStatus ri3152a_itoa(ViInt32 number, ViString buffer)
{
	int index;
	int quotient, divisor;
	
	divisor = 10000;
	quotient = 0;
	
	while ((quotient == 0) && (divisor != 0))
		{
		quotient = number / divisor;
		number -= (quotient * divisor);
		divisor /= 10;
		}
	
	buffer[0] = '0' + quotient;
	
	index = 1;
	while (divisor > 0)
		{
		quotient = number / divisor;
		number -= (quotient * divisor);
		buffer[index++] = '0' + quotient;
		divisor /= 10;
		}
		
	buffer[index] = 0;
	
	return( VI_SUCCESS );
}


#define DIR_BIT 0x2000

