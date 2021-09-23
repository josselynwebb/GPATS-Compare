/*= Racal Instruments 3151 Arbitrary Waveform Generator ===================*/
/* VXI Plug&Play WIN Framework Instrument Driver                           */
/* Original Release: 12/18/95                                              */
/* By: S. Javed/D Masters                                                  */
/* Instrument Driver Revision 2.1                                          */
/* VXI Plug&Play WIN Framework Revision: 3.0                               */
/* 3151 Minimum Hardware Revision:                                         */
/* 3151 Minimum Firmware Revision: 1.0                                     */
/* Modification History: None                                              */
/* Modification History:                                                   */
/*      Rev    Date    Comment                                             */
/*      1.1  12/18/95  Original Release                                    */
/*      2.1  09/16/96  Added support for 3152 and LabWindows/CVI 4.0 on    */
/*                     Windows/95 and Windows/NT                           */
/*=========================================================================*/

#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <visa.h>
#include <ctype.h>
#include <ri3151.h>


#define RI3151_REVISION				"2.1"
#define RI3151_MAX_CMD				((ViInt16) 100)
#define RI3151_READ_LEN				((ViInt32) (RI3151_MAX_CMD - 1))
#define RI3151_MANF_ID				((ViAttrState) 0xFFB)
#define RI3151_MODEL_CODE			((ViAttrState) 3151)
#define RI3152_MODEL_CODE			((ViAttrState) 3152)

#define RI3151_MAX_CMDS		100
#define RI3151_MAX_SEQ		1024

#define RI3151_MAX_INSTR_ERROR			(RI3151_ERR(1))
#define RI3151_NSUP_ATTR_VALUE			(RI3151_ERR(2))
#define RI3151_AMPLITUDE_ERROR			(RI3151_ERR(3))
#define RI3151_OFFSET_ERROR				(RI3151_ERR(4))
#define RI3151_AMPL_OFFSET_ERROR		(RI3151_ERR(5))
#define RI3151_INVALID_REPLY			(RI3151_ERR(6))
#define RI3151_PERCENT_ERROR1			(RI3151_ERR(7))
#define RI3151_PERCENT_ERROR2			(RI3151_ERR(8))
#define RI3151_CANT_OPEN_FILE			(RI3151_ERR(9))
#define RI3151_INVALID_FILE_DATA		(RI3151_ERR(10))
#define RI3151_BINARY_DOWNLOAD_FAILED	(RI3151_ERR(11))
#define RI3151_WAVECAD_FORMAT_1			(RI3151_ERR(12))
#define RI3151_WAVECAD_FORMAT_2			(RI3151_ERR(13))
#define RI3151_WAVECAD_FORMAT_3			(RI3151_ERR(14))
#define RI3151_WAVECAD_FORMAT_4			(RI3151_ERR(15))
#define RI3151_WAVECAD_FORMAT_5			(RI3151_ERR(16))
#define RI3151_WAVECAD_FORMAT_6			(RI3151_ERR(17))
#define RI3151_WAVECAD_FORMAT_7			(RI3151_ERR(18))
#define RI3151_WAVECAD_FORMAT_8			(RI3151_ERR(19))
#define RI3151_WAVECAD_FORMAT_9			(RI3151_ERR(20))
#define RI3151_WAVECAD_FORMAT_10		(RI3151_ERR(21))
#define RI3151_WAVECAD_FORMAT_11		(RI3151_ERR(22))
#define RI3151_PHASE_LOCK_NOT_SUPP		(RI3151_ERR(23))
#define RI3151_PHASE_MODE_CONFLICT		(RI3151_ERR(24))

#define VI_ERROR_PARAMETER9		((ViStatus) (VI_ERROR_PARAMETER8 + 1))
#define VI_ERROR_PARAMETER10		((ViStatus) (VI_ERROR_PARAMETER9 + 1))

/*= LOCAL DATA TYPES =====================================================*/
/*
 * This structure contains information for forming a 3151 command from
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
static ViChar readbuf[RI3151_MAX_CMD+1];
static ViSession initialized[RI3151_MAX_INSTR] =
    {
    RI3151_NO_SESSION, RI3151_NO_SESSION, RI3151_NO_SESSION,
    RI3151_NO_SESSION, RI3151_NO_SESSION, RI3151_NO_SESSION,
    RI3151_NO_SESSION, RI3151_NO_SESSION, RI3151_NO_SESSION,
    RI3151_NO_SESSION, RI3151_NO_SESSION, RI3151_NO_SESSION
    };

static ViBoolean large_memory[RI3151_MAX_INSTR] =
	{
	VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE,
	VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE
	};

/* used for checking the amplitude & offset ranges */
static ViBoolean tight_range_checking[RI3151_MAX_INSTR];

/* used for restricting access to 3152-only operations */
static ViInt16 model_codes[RI3151_MAX_INSTR];

/* global file flag for ri3151_get_next_data_point() function */
static int global_file_flag = 0;

/* Revision information (from SCCS) */

/* used inside the ri3151_load_wavecad_file function */
static WaveCADCmdStruct cmd_table[RI3151_MAX_CMDS];
static ViInt32 segment_loops[RI3151_MAX_SEQ];
static ViInt16 segment_numbers[RI3151_MAX_SEQ];

/*= DEVICE DEPENDENT STRING ARRAYS ========================================*/

/*= INTERNAL FUNCTIONS ====================================================*/
static ViStatus ri3151_real_range(ViReal64  val,ViReal64  min,ViReal64  max,ViStatus  err_code);
static ViStatus ri3151_int_range(ViInt16  val,ViInt16  min,ViInt16  max,ViStatus  err_code);
static ViStatus ri3151_int32_range(ViInt32  val,ViInt32  min,ViInt32  max,ViStatus  err_code);
static ViStatus ri3151_boolean_range(ViBoolean  val,ViStatus  err_code);
static ViStatus ri3151_read_value(ViSession vi, ViReal64 *val_ptr);
static ViStatus ri3151_initCleanUp (ViSession rmSession, ViSession *vi,
                                       ViInt16 index, ViStatus currentStatus);
static ViInt32 ri3151_get_mem_size(ViSession vi);
static ViStatus ri3151_check_ampl_and_offset(ViSession vi, ViReal64 amplitude, ViReal64 offset);
static ViInt16 ri3151_get_next_data_point(FILE *fp, ViInt32 *data_point);

static ViStatus ri3151_parse_wavecad_state(WaveCADCmdStruct *table_entry, 
                                    		char *fileline);

static ViStatus ri3151_parse_wavecad_cmd(WaveCADCmdStruct cmd_table[], 
                                  	int cmd_count, char *fileline);
static ViStatus ri3151_parse_wavecad_seq(char *fileline, 
                                  	ViInt16 *link, 
                                  	ViInt16 *segment, 
                                  	ViInt32 *loop_count,
                                  	ViInt32 *segment_length,
                                  	char *wave_file_name);
static ViStatus ri3151_create_path_name(char *wave_file_name, char *file_name);
static ViStatus ri3151_output_wavecad_commands(ViSession vi, 
                                        	WaveCADCmdStruct cmd_table[], 
                                        	int cmd_count);

/*= DLL CODE ==============================================================*/
#if defined(WIN32) || defined(_WIN32) || defined(__WIN32__) || defined(__NT__)
/* This is required to produce a Win32 (95/NT) .DLL file */
#elif defined(_WINDOWS)
/* This is required to produce a Win16 .DLL file */
#include <windows.h>
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
ViStatus _VI_FUNC ri3151_init (ViRsrc    RsrcName,
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
    error = ri3151_int_range (id_query, 0, 1, VI_ERROR_PARAMETER2);
    if (error < 0) return error;
    error = ri3151_int_range (reset, 0, 1, VI_ERROR_PARAMETER3);
    if (error < 0) return error;

    /* Initialize entry in Instrument Table and interface for instrument. */
    error = viOpenDefaultRM (&rmSession);
    if (error < 0) return error;
    error = viOpen (rmSession, RsrcName, VI_NULL, VI_NULL, vi);
    if (error < 0) return error;

    /* Find an empty position in the global data array. */
    i=0;
    while (i<RI3151_MAX_INSTR && initialized[i] != RI3151_NO_SESSION)
        i++;

    /* Did we find an empty spot?  If so, record the index in the User Data
           Attribute provided by VISA. */
    if (i < RI3151_MAX_INSTR)
    {
        error = viSetAttribute (*vi, VI_ATTR_USER_DATA, (ViUInt32) i);

        /* If we get an error or a warning from the call to viSetAttribute,
                  then our index may not have been stored properly, so return an error. */
        if  (error != VI_SUCCESS)
        {
            if (error > 0)
                error = RI3151_NSUP_ATTR_VALUE;
            return ri3151_initCleanUp(rmSession, vi, i, error);
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
        return RI3151_MAX_INSTR_ERROR;
    }

/* -- Default Setup -------------------------------------------------------*/
    /* Set timeout to 10 seconds */
    error = viSetAttribute(*vi, VI_ATTR_TMO_VALUE, (ViUInt32)10000);
    if (error < 0)
        return (ri3151_initCleanUp(rmSession, vi, i, error));

    /* Send END on the last byte of transfer */
    error = viSetAttribute(*vi, VI_ATTR_SEND_END_EN, (ViAttrState)VI_TRUE);
    if (error < 0)
        return (ri3151_initCleanUp(rmSession, vi, i, error));

    /* Set the input/output buffers to 256 characters */
    error = viSetBuf (*vi, VI_READ_BUF|VI_WRITE_BUF, (ViUInt32) 256);
    if (error < 0)
        return (ri3151_initCleanUp(rmSession, vi, i, error));

    /* Make sure the input buffer flushes after every viScanf call */
    error = viSetAttribute(*vi, VI_ATTR_RD_BUF_OPER_MODE, VI_FLUSH_ON_ACCESS);
    if (error < 0)
        return (ri3151_initCleanUp(rmSession, vi, i, error));

	/* set the model code to 3152, update to 3151 only if ID query is */
	/* performed */
	model_codes[i] = RI3152_MODEL_CODE;

	/* assume the tighter range checking is NOT performed, unless */
	/* the ID query is performed */
	tight_range_checking[i] = VI_FALSE;

    /* Perform the ID Query if requested */
    if (id_query)
    {
        /* Read the manufacturer ID */
        error = viGetAttribute (*vi, VI_ATTR_MANF_ID, &manf_ID);
        if (error < 0)
            return ri3151_initCleanUp(rmSession, vi, i, error);

        /* Read the model code */
        error = viGetAttribute (*vi, VI_ATTR_MODEL_CODE, &model_code);
        if (error < 0)
            return ri3151_initCleanUp(rmSession, vi, i, error);

        /* mask to leave 12 LSBs of model code */
        model_code &= 0xFFF;

        /* check for proper model code and manufacter ID */
        if (((model_code != RI3151_MODEL_CODE) && (model_code != RI3152_MODEL_CODE))
        ||   (manf_ID != RI3151_MANF_ID))
            return ri3151_initCleanUp(rmSession, vi, i, error);
            
		model_codes[i] = model_code;

		/* check revision of firmware if 3151.  If it is a 3151, use */
		/* tighter range checking */
		if (model_code == RI3151_MODEL_CODE)
		{
			error = viPrintf(*vi, "*IDN?\n");
			if (error < 0)
				return ri3151_initCleanUp(rmSession, vi, i, error);

			error = viRead(*vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt);
			if (error < 0)
				return ri3151_initCleanUp(rmSession, vi, i, error);

			readbuf[cnt] = '\0';
			
			/* look for the LAST comma in the string, which precedes */
			/* the firmware revision */
			cp = strrchr ((char*) readbuf, ',');
			
			if ( !cp )
				return ri3151_initCleanUp(rmSession, vi, i, RI3151_INVALID_REPLY);

			/* convert the firmware revision number to a double precision value */
			firmware_rev = strtod (cp+1, NULL);
			
			/* early versions (beta) of 3152 used 3151 model code */
			/* check for 3152 using *idn? reply */
			if (strstr (readbuf, "3152") != NULL)
				model_codes[i] = model_code = RI3152_MODEL_CODE;
			else if (firmware_rev < 1.6)
				tight_range_checking[i] = VI_TRUE;
		}
    }
/*-- End of ID Query ------------------------------------------------------*/

    /* Reset the instrument if requested. */
    if (reset)
    {
        error = viPrintf (*vi, "*RST\n");
        if (error < 0)
            return ri3151_initCleanUp(rmSession, vi, i, error);
    }


/*-- check for large (512K) or small (64K) memory installed */
	if ((error = viPrintf(*vi, "*OPT?\n")) != VI_SUCCESS)
	{
		return ri3151_initCleanUp(rmSession, vi, i, error);
	}
	
	error = viRead(*vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt);
	if (error < 0)
		return ri3151_initCleanUp(rmSession, vi, i, error);


	if (readbuf[0] == '1')
		large_memory[i] = VI_TRUE;
	else
		large_memory[i] = VI_FALSE;


	return error;
}



/*=========================================================================*/
/* This function generates a sine wave (standard waveform)                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_sine_wave (ViSession vi,
                                    ViReal64 frequency,
                                    ViReal64 amplitude,
                                    ViReal64 offset,
                                    ViInt16 phase,
                                    ViInt16 powerSinex)
{
    ViStatus error;

    /* check parameter ranges */
    if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_SIN, RI3151_MAX_FREQ_SIN,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );


	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    if ((error = ri3151_int_range (phase,RI3151_MIN_PHASE , RI3151_MAX_PHASE, VI_ERROR_PARAMETER5)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_int_range (powerSinex, RI3151_POWER_1, RI3151_POWER_9, VI_ERROR_PARAMETER6)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:SIN %.5g,%.4g,%.4g,%d,%d\n",
                        frequency, amplitude, offset, phase, powerSinex);

    return( error );
}



/*=========================================================================*/
/* This function generates a triangular wave (standard waveform)           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_triangular_wave (ViSession vi,
                                          ViReal64 frequency,
                                          ViReal64 amplitude,
                                          ViReal64 offset,
                                          ViInt16 phase,
                                          ViInt16 powerTriangularx)
{
    ViStatus error;

    /* check parameter ranges */
    if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_TRI, RI3151_MAX_FREQ_TRI,
                                      VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    if ((error = ri3151_int_range (phase, RI3151_MIN_PHASE, RI3151_MAX_PHASE,
                                        VI_ERROR_PARAMETER5)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_int_range (powerTriangularx, RI3151_POWER_1, RI3151_POWER_9,
                                        VI_ERROR_PARAMETER6)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:TRI %.5g,%.4g,%.4g,%d,%d\n",
                        frequency, amplitude, offset, phase, powerTriangularx);

    return( error );
}
/*=========================================================================*/
/* This function generates a square wave (standard waveform)               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_square_wave (ViSession vi,
                                      ViReal64 frequency,
                                      ViReal64 amplitude,
                                      ViReal64 offset,
                                      ViInt16 duty_cycle)

{
    ViStatus error;

    /* check parameter ranges */
    if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_SQU, RI3151_MAX_FREQ_SQU,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    if ((error = ri3151_int_range (duty_cycle, RI3151_MIN_DUTY_CYCLE , RI3151_MAX_DUTY_CYCLE,
                                      VI_ERROR_PARAMETER5)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:SQU %.5g,%.4g,%.4g,%d\n",
                        frequency, amplitude, offset, duty_cycle);

    return( error );
}

/*=========================================================================*/
/* This function generates a pulse wave (standard waveform)                */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_pulse_wave (ViSession vi,
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

    /* check parameter ranges */
    if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_PULS, RI3151_MAX_FREQ_PULS,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    max_percent_period = delay + rise_time + high_time + fall_time;

    if (max_percent_period > 100)
        return( RI3151_PERCENT_ERROR1 );

    if ((error = ri3151_real_range (delay, RI3151_MIN_DELAY_PULSE , RI3151_MAX_DELAY_PULSE, VI_ERROR_PARAMETER5)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_real_range (rise_time, RI3151_MIN_RISE_TIME_PULSE, RI3151_MAX_RISE_TIME_PULSE,
            VI_ERROR_PARAMETER6)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_real_range (high_time, RI3151_MIN_HIGH_TIME_PULSE, RI3151_MAX_HIGH_TIME_PULSE,
            VI_ERROR_PARAMETER7)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_real_range (fall_time, RI3151_MIN_FALL_TIME_PULSE, RI3151_MAX_FALL_TIME_PULSE,
            VI_ERROR_PARAMETER8)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:PULS %.5g,%.4g,%.4g,%.4g,%4.1f,%4.1f,%4.1f\n",
                        frequency, amplitude, offset, delay, high_time, rise_time, fall_time);

    return( error );
}

/*=========================================================================*/
/* This function generates a ramp wave (standard waveform)                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_ramp_wave (ViSession vi,
                                    ViReal64 frequency,
                                    ViReal64 amplitude,
                                    ViReal64 offset,
                                    ViReal64 delay,
                                    ViReal64 rise_time, ViReal64 fall_time)
{
    ViStatus error;
    ViReal64 max_percent_period;

    /* check parameter ranges */
    if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_RAMP, RI3151_MAX_FREQ_RAMP,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    max_percent_period = delay + rise_time + fall_time;
    if (max_percent_period > 100)
        return( RI3151_PERCENT_ERROR2 );

    if ((error = ri3151_real_range (delay, RI3151_MIN_DELAY_RAMP , RI3151_MAX_DELAY_RAMP, VI_ERROR_PARAMETER5)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_real_range (rise_time, RI3151_MIN_RISE_TIME_RAMP, RI3151_MAX_RISE_TIME_RAMP,
            VI_ERROR_PARAMETER6)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_real_range (fall_time, RI3151_MIN_FALL_TIME_RAMP, RI3151_MAX_FALL_TIME_RAMP,
            VI_ERROR_PARAMETER7)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:RAMP %.5g,%.4g,%.4g,%4.1f,%4.1f,%4.1f\n",
                        frequency, amplitude, offset, delay, rise_time, fall_time);

    return( error );
}

/*=========================================================================*/
/* This function generates a sinc wave (standard waveform)                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_sinc_wave (ViSession vi,
                                    ViReal64 frequency,
                                    ViReal64 amplitude,
                                    ViReal64 offset,
                                    ViInt16 cycle_number)

{
    ViStatus error;

    /* check parameter ranges */
    if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_SINC, RI3151_MAX_FREQ_SINC,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    if ((error = ri3151_int_range (cycle_number, RI3151_MIN_CYCLE_NUMBER , RI3151_MAX_CYCLE_NUMBER,
                VI_ERROR_PARAMETER5)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:SINC %.5g,%.4g,%.4g,%d\n",
                        frequency, amplitude, offset, cycle_number);

    return( error );
}


/*=========================================================================*/
/* This function generates a exponential wave (standard waveform)          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_exponential_wave (ViSession vi,
                                           ViReal64 frequency,
                                           ViReal64 amplitude,
                                           ViReal64 offset,
                                           ViReal64 exponent)

{
    ViStatus error;

    /* check parameter ranges */
    if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_EXP, RI3151_MAX_FREQ_EXP,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    if ((error = ri3151_real_range (exponent, RI3151_MIN_EXPONENT_EXP, RI3151_MAX_EXPONENT_EXP,
                VI_ERROR_PARAMETER5)) != VI_SUCCESS)
        return( error );

	if (exponent == 0.0)
		return( VI_ERROR_PARAMETER5 );

    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:EXP %.5g,%.4g,%.4g,%.4g\n",
                        frequency, amplitude, offset, exponent);

    return( error );
}


/*=========================================================================*/
/* This function generates a gaussian wave (standard waveform)             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_gaussian_wave (ViSession vi,
                                        ViReal64 frequency,
                                        ViReal64 amplitude,
                                        ViReal64 offset,
                                        ViInt16 exponent)

{
    ViStatus error;

    /* check parameter ranges */
    if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_GAU, RI3151_MAX_FREQ_GAU,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    if ((error = ri3151_int_range (exponent, RI3151_MIN_EXPONENT_GAU, RI3151_MAX_EXPONENT_GAU,
                VI_ERROR_PARAMETER5)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:GAUS %.5g,%.4g,%.4g,%d\n",
                        frequency, amplitude, offset, exponent);

    return( error );
}

/*=========================================================================*/
/* This function generates a DC signal (standard waveform)                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_dc_signal (ViSession vi,
                                    ViInt16 percent_amplitude)

{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_int_range (percent_amplitude, RI3151_MIN_PERCENT_AMP, RI3151_MAX_PERCENT_AMP,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );


    /* format the command and output it to the generator */
    error = viPrintf(vi, ":SOUR:APPLY:DC %d\n", percent_amplitude);
    return( error );
}

/*=========================================================================*/
/* This function produces Amplitude Modulation by acquiring the Amplitude  */
/* percentage and the Internal Frequency.                                  */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_amplitude_modulation (ViSession vi,
                                    ViInt16 percent_amplitude, ViInt32 internal_frequency)

{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_int_range (percent_amplitude, 
                                  RI3151_MIN_AM_PERCENT, 
                                  RI3151_MAX_AM_PERCENT,
                                  VI_ERROR_PARAMETER2)) != VI_SUCCESS)
      return( error );

    if ((error = ri3151_int32_range (internal_frequency, RI3151_MIN_AM_FREQ,
                                   RI3151_MAX_AM_FREQ, VI_ERROR_PARAMETER3)) != VI_SUCCESS)
      return( error );


    /* format the command and output it to the generator */
    if ((error = viPrintf(vi, "AM %d\n", percent_amplitude)) != VI_SUCCESS)
    return( error );


    if ((error =  viPrintf(vi, "AM:INT:FREQ %d\n", internal_frequency)) != VI_SUCCESS)
    return( error );


    if ((error =  viPrintf(vi, "AM:EXEC \n")) != VI_SUCCESS)
    return( error );

  return( error );
}

/*=========================================================================*/
/* This function selects the Waveform mode                                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_select_waveform_mode  (ViSession vi,
                                          		ViInt16 waveform_type)
{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_int_range (waveform_type, RI3151_MODE_STD,
                                    RI3151_MODE_SEQ, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */

    switch ( waveform_type )
		{
		case RI3151_MODE_STD:	error = viPrintf(vi, "FUNC:MODE FIX\n");
								break;

		case RI3151_MODE_ARB:	error = viPrintf(vi, "FUNC:MODE USER\n");
								break;

		case RI3151_MODE_SEQ:	error = viPrintf(vi, "FUNC:MODE SEQ\n");
								break;
		}

	return( error );
}

/*=========================================================================*/
/* This function selects the operation mode                                */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_operating_mode  (ViSession vi,
                                          ViInt16 operating_mode)
{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_int_range (operating_mode, RI3151_MODE_CONT,
                                    RI3151_MODE_BURST, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */

	switch ( operating_mode )
		{
		case RI3151_MODE_CONT:	error = viPrintf(vi, "INIT:CONT ON\n");
								break;

		case RI3151_MODE_TRIG:	error = viPrintf(vi, "INIT:CONT OFF;:TRIG:GATE OFF;:TRIG:BURST OFF\n");
								break;

		case RI3151_MODE_GATED:	error = viPrintf(vi, "INIT:CONT OFF;:TRIG:BURST OFF;:TRIG:GATE ON\n");
								break;

		case RI3151_MODE_BURST:	error = viPrintf(vi, "INIT:CONT OFF;:TRIG:GATE OFF;:TRIG:BURST ON\n");
								break;
		}

	return( error );
}

/*=========================================================================*/
/* This function programs the frequency for the present waveform           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_set_frequency (ViSession vi, ViReal64 frequency)
{
    ViStatus error;

    /* check parameter ranges */
	if ((error = ri3151_real_range (frequency, RI3151_MIN_FREQ_SIN, 
                                              RI3151_MAX_FREQ_SIN,
                                              VI_ERROR_PARAMETER2)) != VI_SUCCESS)
		return( error );

	return( viPrintf(vi, "FREQ %.5g\n", frequency) );
}

/*=========================================================================*/
/* This function programs the frequency for the present waveform           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_set_amplitude (ViSession vi, ViReal64 amplitude)
{
    ViStatus error;

    /* check parameter ranges */
	if ((error = ri3151_real_range (amplitude, RI3151_MIN_AMPLITUDE, 
                                              RI3151_MAX_AMPLITUDE,
                                              VI_ERROR_PARAMETER2)) != VI_SUCCESS)
		return( error );

	return( viPrintf(vi, "VOLT %.4g\n", amplitude) );
}

/*=========================================================================*/
/* This function programs the frequency for the present waveform           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_set_offset (ViSession vi, ViReal64 offset)
{
    ViStatus error;

    /* check parameter ranges */
	if ((error = ri3151_real_range (offset, RI3151_MIN_OFFSET, 
                                           RI3151_MAX_OFFSET,
                                           VI_ERROR_PARAMETER2)) != VI_SUCCESS)
		return( error );

	return( viPrintf(vi, "VOLT:OFFSET %.4g\n", offset) );
}

/*=========================================================================*/
/* This function selects the source of the trigger                         */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_trigger_source (ViSession vi, ViInt16 trigger_source)
{
    ViStatus error;

    /* check parameter ranges */
    if ((error = ri3151_int_range (trigger_source, RI3151_TRIGGER_INTERNAL,
                                    RI3151_TRIGGER_TTLTRG7, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
	switch (trigger_source  )
		{
		case RI3151_TRIGGER_INTERNAL:
			error = viPrintf(vi, "TRIG:SOUR:ADV INT\n");
			break;
			
		case RI3151_TRIGGER_EXTERNAL:
			error = viPrintf(vi, "TRIG:SOUR:ADV EXT\n");
			break;

		case RI3151_TRIGGER_TTLTRG0:
		case RI3151_TRIGGER_TTLTRG1:
		case RI3151_TRIGGER_TTLTRG2:
		case RI3151_TRIGGER_TTLTRG3:
		case RI3151_TRIGGER_TTLTRG4:
		case RI3151_TRIGGER_TTLTRG5:
		case RI3151_TRIGGER_TTLTRG6:
		case RI3151_TRIGGER_TTLTRG7:
			error = viPrintf(vi, "TRIG:SOUR:ADV TTLTRG%d\n", 
								  trigger_source - RI3151_TRIGGER_TTLTRG0);
			break;
		}

    return( error );
}


/*=========================================================================*/
/* This function programs the trigger rate for the internal trigger        */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_trigger_rate (ViSession vi, ViReal64 triggerRate)
{
	ViStatus error;
	
	error = ri3151_real_range(triggerRate, RI3151_TRIGGER_RATE_MIN,
	                          RI3151_TRIGGER_RATE_MAX, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );

	return( viPrintf(vi, "TRIG:TIMER %f\n", triggerRate) );
}


/*=========================================================================*/
/* This function selects the slope of the trigger for the external trigger */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_trigger_slope (ViSession vi, ViBoolean triggerSlope)
{
	ViStatus error;
	
	error = ri3151_boolean_range(triggerSlope, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );
		
	return( viPrintf(vi, "TRIG:SLOPE %s\n", triggerSlope ? "POS" : "NEG") );
}
	

/*=========================================================================*/
/* This function selects the trigger delay number of triggers till action  */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_trigger_delay (ViSession vi, ViInt32 triggerDelay)
{
	ViStatus error;
	
	if (triggerDelay != 0)
		{
		error = ri3151_real_range(triggerDelay, RI3151_TRIGGER_DELAY_MIN,
								RI3151_TRIGGER_DELAY_MAX, VI_ERROR_PARAMETER2);
		if (error < 0)
			return( error );
			
		/* odd number of triggers is not allowed */
		if (error & 0x1)
			return( VI_ERROR_PARAMETER2 );
		}
		
	return( viPrintf(vi, "TRIG:DELAY %ld\n", triggerDelay) );
}


/*=========================================================================*/
/* This function selects the output trigger source, TTLTRG line, trigger   */
/* point, and segment in which the trigger is output                       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_output_trigger (ViSession vi, 
                                         ViInt16 outputTriggerSource,
                                         ViInt16 outputTriggerLine, 
                                         ViInt32 BITTriggerPoint,
                                         ViInt16 LCOMSegment)
{
	ViStatus error;
	ViInt32 max_points;
	ViInt16 i;
	ViChar *param;
	
	error = ri3151_int_range(outputTriggerSource, RI3151_TRIGGER_INTERNAL,
								RI3151_TRIGGER_LCOM, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );
		
	error = ri3151_int_range(outputTriggerLine, RI3151_TRIGGER_TTLTRG0,
								RI3151_TRIGGER_NONE, VI_ERROR_PARAMETER3);
	if (error < 0)
		return( error );

	max_points = ri3151_get_mem_size(vi) - 1;
	
	error = ri3151_int32_range(BITTriggerPoint, 2, max_points,
							   VI_ERROR_PARAMETER4);
	if (error < 0)
		return( error );

	/*
	 * BIT Trigger point must be even number
	 */
	if (BITTriggerPoint % 2)
		return( VI_ERROR_PARAMETER4 );



	error = ri3151_int_range(LCOMSegment, RI3151_MIN_SEG_NUMBER,
								RI3151_MAX_SEG_NUMBER, VI_ERROR_PARAMETER5);
	if (error < 0)
		return( error );


	switch( outputTriggerSource )
		{
		case RI3151_TRIGGER_INTERNAL:
			param = "INT";
			break;
			
		case RI3151_TRIGGER_EXTERNAL:
			param = "EXT";
			break;
			
		case RI3151_TRIGGER_BIT:
			param = "BIT";
			break;

		case RI3151_TRIGGER_LCOM:
			param = "LCOM";
			break;
		default:
			param = "INT";
		}

	error = viPrintf(vi, "OUTPUT:TRIG:SOURCE %s\n", param);
	if (error < 0)
		return( error );

	if (outputTriggerLine == RI3151_TRIGGER_NONE)
		{
		for (i = 0;  i < 8;  ++i)
			{
			error = viPrintf(vi, "OUTPUT:TTLTRG%d OFF\n", i);
			if (error < 0)
				return( error );
			}
		}
	else
		{
		error = viPrintf(vi, "OUTPUT:TTLTRG%d ON\n", 
			outputTriggerLine - RI3151_TRIGGER_TTLTRG0);
		if (error < 0)
			return( error );
		}


	if (outputTriggerSource == RI3151_TRIGGER_BIT
	||  outputTriggerSource == RI3151_TRIGGER_LCOM)
		{
		error = viPrintf(vi, "OUTPUT:SYNC:POS %ld\n", BITTriggerPoint);
		if (error < 0)
			return( error );
		}
		
	if (outputTriggerSource == RI3151_TRIGGER_LCOM)
		{
		error = viPrintf(vi, "TRACE:SEL %d\n", LCOMSegment);
		if (error < 0)
			return( error );
		}

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* This function programs the SYNC output pulse                            */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_output_sync (ViSession vi, 
									  ViInt16 SYNCPulseSource,
                                      ViInt32 BITSYNCPoint, 
                                      ViInt16 LCOMSegment)
{
	ViInt32 max_points;
	ViChar *cmd;
	ViStatus error;
	
	error = ri3151_int_range(SYNCPulseSource, RI3151_SYNC_OFF,
								RI3151_SYNC_HCLOCK, VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );
		

	max_points = ri3151_get_mem_size(vi) - 1;
	
	error = ri3151_int32_range(BITSYNCPoint, 2, max_points,
							   VI_ERROR_PARAMETER3);
	if (error < 0)
		return( error );


	/*
	 * SYNC Point must be even number
	 */
	if (BITSYNCPoint % 2)
		return( VI_ERROR_PARAMETER3 );

	error = ri3151_int_range(LCOMSegment, RI3151_MIN_SEG_NUMBER,
								RI3151_MAX_SEG_NUMBER, VI_ERROR_PARAMETER4);
	if (error < 0)
		return( error );


	switch( SYNCPulseSource )
		{
		case RI3151_SYNC_OFF:
			cmd = "OUTPUT:SYNC OFF\n";
			break;
			
		case RI3151_SYNC_BIT:
			cmd = "OUTPUT:SYNC:SOURCE BIT;:OUTPUT:SYNC ON\n";
			break;
			
		case RI3151_SYNC_LCOM:
			cmd = "OUTPUT:SYNC:SOURCE LCOM;:OUTPUT:SYNC ON\n";
			break;

		case RI3151_SYNC_SSYNC:
			cmd = "OUTPUT:SYNC:SOURCE SSYNC;:OUTPUT:SYNC ON\n";
			break;

		case RI3151_SYNC_HCLOCK:
			cmd = "OUTPUT:SYNC:SOURCE HCLOCK;:OUTPUT:SYNC ON\n";
			break;
		default:
			cmd = "OUTPUT:SYNC OFF\n";
		}

	error = viPrintf(vi, cmd);
	if (error < 0)
		return( error );


	if (SYNCPulseSource == RI3151_SYNC_BIT
	||  SYNCPulseSource == RI3151_SYNC_LCOM)
		{
		error = viPrintf(vi, "OUTPUT:SYNC:POS %d\n", BITSYNCPoint);
		if (error < 0)
			return( error );
		}
		
	if (SYNCPulseSource == RI3151_SYNC_LCOM)
		{
		error = viPrintf(vi, "TRACE:SEL %d\n", LCOMSegment);
		if (error < 0)
			return( error );
		}

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* This function sends an immediate trigger ("soft trigger") to the Arb    */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_immediate_trigger (ViSession vi)
{
    return( viPrintf(vi, "*TRG\n") );
}


/*=========================================================================*/
/* This function selects the 3151 as the phase master unit                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_phase_master (ViSession vi)
{
	return( viPrintf(vi, "PHASE:LOCK ON;:PHASE:SOURCE MASTER\n") );
}

/*=========================================================================*/
/* This function selects the 3151 as the phase master unit                 */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_phase_slave (ViSession vi, ViInt16 phaseOffset)
{
	ViStatus error;

	error = ri3151_int_range(phaseOffset, RI3151_MIN_PHASE, RI3151_MAX_PHASE,
								VI_ERROR_PARAMETER2);
	if (error < 0)
		return( error );

	error = viPrintf(vi, "PHASE:SOURCE SLAVE;:PHASE:LOCK ON;:PHASE %d\n",phaseOffset);
	return( error );
}

/*=========================================================================*/
/* This function enables or disables the phase lock loop mode for a 3152   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_phase_lock_loop (ViSession vi, ViBoolean on_off)
{
	ViStatus error;
	ViInt16 index;
	ViInt32 long_index;

    /* get the index into driver tables for this instance of the instrument */
    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &long_index);
    if (error < 0) return error;

	/* ensure we are a 3152 */
    index = (ViInt16) long_index;

	if (model_codes[index] != RI3152_MODEL_CODE)
		return( RI3151_PHASE_LOCK_NOT_SUPP );


    if ((error = ri3151_boolean_range (on_off, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */
	error = viPrintf(vi, "PHASE2:LOCK %s\n", (on_off) ? "ON" : "OFF");
	return( error );
}

/*=========================================================================*/
/* This function sets the phase for a phase locked 3152.  When arbitrary   */
/* waveform mode is active, this sets only the coarse phase.  When standard*/
/* waveform mode is active, this programs both coarse and fine phase       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_pll_phase (ViSession vi, ViReal64 phase)
{
	ViStatus error;
	ViInt16 waveform_mode, standard_waveform;
	ViInt16 index;
	ViInt32 long_index;
	ViReal64 frequency, dont_care;
	ViInt16 idiscard;
	ViReal64 coarse_phase, fine_phase, resolution;
	ViInt32 num_points;
	

    /* get the index into driver tables for this instance of the instrument */
    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &long_index);
    if (error < 0) return error;

	/* ensure we are a 3152 */
    index = (ViInt16) long_index;

	if (model_codes[index] != RI3152_MODEL_CODE)
		return( RI3151_PHASE_LOCK_NOT_SUPP );

    if ((error = ri3151_real_range (phase, RI3152_MIN_PHASE, RI3152_MAX_PHASE,
                                      VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	/* check the mode/standard waveform to check for fine phase adjust */
	error = ri3151_mode_query(vi, &waveform_mode, &standard_waveform);
	if (error < 0)
		return( error );
		
	/* if we are in arbitrary mode, just do coarse phase adjust */
	if (waveform_mode == RI3151_MODE_ARB)
		return( viPrintf(vi, "PHASE2:ADJ %.2lf", phase) );
		
	/* if we are in sequence mode, phase adjust doesn't work */
	if (waveform_mode == RI3151_MODE_SEQ)
		return( RI3151_PHASE_MODE_CONFLICT );
		
	/* we must be in standard waveform mode, get the standard waveform */
	/* and the frequency, and compute the number of points */
	error = ri3151_status_query (vi, &dont_care, &frequency, &dont_care, &idiscard);
	if (error < 0)
		return( error );

	switch( standard_waveform )
		{
		case RI3151_SINE:
		case RI3151_SQUARE:
			if (frequency <= 200.0E3)
				num_points = 500;
			else if (frequency <= 10.0E6)
				num_points = (ViInt32) (100.0E6 / frequency);
			else
				num_points = 10;
			break;
			
		case RI3151_RAMP:
		case RI3151_PULSE:
		case RI3151_GAUSSIAN:
		case RI3151_EXPONENTIAL:
			if (frequency < 100.0E3)
				num_points = 1000;
			else
				num_points = (ViInt32) (100.0E6 / frequency);
			break;

		case RI3151_TRIANGLE:
		case RI3151_SINC:
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
	
	return( viPrintf(vi, ":PHASE2:ADJ %.2lf;:PHASE2:FINE %.2lf\n", 
						coarse_phase, fine_phase) );
}
		
/*=========================================================================*/
/* This function sets the fine phase for a phase locked 3152.  This would  */
/* be used when the arbitrary waveform mode is active, or to override the  */
/* fine phase adjustment made for the standard waveform mode               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_pll_fine_phase (ViSession vi, ViReal64 phase)
{
	ViStatus error;
	ViInt16 index;
	ViInt32 long_index;
	

    /* get the index into driver tables for this instance of the instrument */
    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &long_index);
    if (error < 0) return error;

	/* ensure we are a 3152 */
    index = (ViInt16) long_index;

	if (model_codes[index] != RI3152_MODEL_CODE)
		return( RI3151_PHASE_LOCK_NOT_SUPP );

    if ((error = ri3151_real_range (phase, RI3152_MIN_FINE_PHASE, RI3152_MAX_FINE_PHASE,
                                      VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	
	return( viPrintf(vi, "PHASE2:FINE %.2lf\n", phase) );
}
		


/*=========================================================================*/
/* This function allows the user to select various features of the         */
/* Burst Mode                                                              */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_burst_mode  (ViSession vi,
                                      ViInt32 number_of_cycles)

{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_int32_range (number_of_cycles, RI3151_BURST_MIN_CYCLE,
                                    RI3151_BURST_MAX_CYCLE, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );


    error = viPrintf(vi, "INIT:CONT OFF;:TRIG:COUNT %ld;:TRIG:GATE OFF;:TRIG:BURST ON\n", number_of_cycles);
    if (error <0)
       return (error);


    return( error );
}


/*=========================================================================*/
/* This function selects a LP Filter or turns the filter off               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_filter    (ViSession vi,
                                    ViInt16 lp_filter)
{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_int_range (lp_filter, RI3151_FILTER_OFF,
                                    RI3151_FILTER_50MHZ, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */

	switch ( lp_filter )
		{
		case  RI3151_FILTER_OFF:   error = viPrintf(vi, "OUTP:FILT OFF\n");
                                   break;

		case RI3151_FILTER_20MHZ:  error = viPrintf(vi, "OUTP:FILT ON\n");
                                   error = viPrintf(vi, "OUTP:FILT:FREQ 20MHz\n");
                                   break;

		case RI3151_FILTER_25MHZ:  error = viPrintf(vi, "OUTP:FILT ON\n");
                                   error = viPrintf(vi, "OUTP:FILT:FREQ 25MHz\n");
                                   break;

		case RI3151_FILTER_50MHZ:  error = viPrintf(vi, "OUTP:FILT ON\n");
                                   error = viPrintf(vi, "OUTP:FILT:FREQ 50MHz\n");
                                   break;
		}

    return( error );
}

/*=========================================================================*/
/* This function turn the output signal ON or OFF                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_output    (ViSession vi,
                                    ViBoolean output_switch)
{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_boolean_range (output_switch, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    /* format the command and output it to the generator */

    if (output_switch == RI3151_OUTPUT_ON)
       error = viPrintf(vi, "OUTP:STAT ON\n");
    else
       error = viPrintf(vi, "OUTP:STAT OFF\n");

    return( error );
}


/*=========================================================================*/
/* This function takes the parameters for Arbitrary Waveform generation.   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_define_arb_segment (ViSession vi, ViInt16 segment_number,
                                            ViInt32 segment_size)


{
    ViStatus error;
    ViInt32 max_size;
    
    
	max_size = ri3151_get_mem_size(vi);
	
    /* check parameter ranges */

    if ((error = ri3151_int_range (segment_number, RI3151_MIN_SEG_NUMBER,
                                    RI3151_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_int32_range (segment_size, RI3151_MIN_SEGMENT_SIZE,
                                    max_size, VI_ERROR_PARAMETER3)) != VI_SUCCESS)
        return( error );

    error = viPrintf(vi, "TRAC:DEF %d, %ld\n", segment_number, segment_size);
    if (error <0)
       return (error);

    error = viPrintf(vi, "TRAC:SEL %d\n", segment_number);
    if (error <0)
       return (error);


    return( error );

}

/*=========================================================================*/
/* This function Deletes one or more segment numbers.                      */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_delete_segments (ViSession vi, ViInt16 segment_number,
                                            ViBoolean delete_all_segments)

{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_int_range (segment_number, RI3151_MIN_SEG_NUMBER,
                                   RI3151_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_boolean_range (delete_all_segments, VI_ERROR_PARAMETER3)) != VI_SUCCESS)
        return( error );

    error = viPrintf(vi, "TRAC:DEL %d\n", segment_number);
    if (error <0)
       return (error);

    if (delete_all_segments)

       {

             error = viPrintf(vi, "TRAC:DEL:ALL\n");
             if (error <0)
                return (error);
       }

    return( error );

}

/*=========================================================================*/
/* This function loads arbitrary data into the specified segment           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_load_arb_data (ViSession vi, 
											ViInt16 segment_number,
											ViUInt16 *data_pts,
                                            ViInt32 number_of_points)
{
    ViInt32 max_size;
    ViStatus error;
    ViUInt32 cnt;


	/* range check the parameters */
    if ((error = ri3151_int_range (segment_number, RI3151_MIN_SEG_NUMBER,
                                    RI3151_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );


	max_size = ri3151_get_mem_size(vi);
    if ((error = ri3151_int32_range (number_of_points, RI3151_MIN_SEGMENT_SIZE,
                                    max_size, VI_ERROR_PARAMETER4)) != VI_SUCCESS)
        return( error );


	/* select the arbitrary waveform segment */
	if ((error = viPrintf(vi, "TRAC:SEL %d\n", segment_number)) != VI_SUCCESS)
		return( error );

	if ((error = viPrintf(vi, "SMEM:STATE OFF\n")) != VI_SUCCESS)
		return( error );

	/* get the device ready to download via A24 space */
	if ((error = viPrintf(vi, "SMEM:MODE WRITE\n")) != VI_SUCCESS)
		return( error );

	if ((error = viPrintf(vi, "SMEM:STATE ON\n")) != VI_SUCCESS)
		return( error );

	if ((error = viPrintf(vi, "*OPC?\n")) != VI_SUCCESS)
		return( error );

	if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
		return( error );
		
	/* try again */
	if (readbuf[0] != '1')
		{
		if ((error = viPrintf(vi, "*OPC?\n")) != VI_SUCCESS)
			return( error );

		if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
			return( error );
		
		if (readbuf[0] != '1')
			return( RI3151_BINARY_DOWNLOAD_FAILED );
		}

	/* download the data points via A24 address */
	while( number_of_points-- )
		{
		if ((error = viOut16 (vi, 2, 0, *data_pts++)) != VI_SUCCESS)
			{
			viPrintf(vi, "SMEM:STATE OFF\n");
			return( error );
			}
		}		

	
    return( viPrintf(vi, "SMEM:STATE OFF\n") );

}


/*=========================================================================*/
/* This function loads arbitrary data into the specified segment           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_load_and_shift_arb_data (ViSession vi,
                                                  ViInt16 segment_number,
                                                  ViInt16 *data_pts,
                                                  ViInt32 number_of_points)
{
    ViInt32 max_size;
    ViStatus error;
    ViUInt32 cnt;


	/* range check the parameters */
    if ((error = ri3151_int_range (segment_number, RI3151_MIN_SEG_NUMBER,
                                    RI3151_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );


	max_size = ri3151_get_mem_size(vi);
    if ((error = ri3151_int32_range (number_of_points, RI3151_MIN_SEGMENT_SIZE,
                                    max_size, VI_ERROR_PARAMETER4)) != VI_SUCCESS)
        return( error );


	/* select the arbitrary waveform segment */
	if ((error = viPrintf(vi, "TRAC:SEL %d\n", segment_number)) != VI_SUCCESS)
		return( error );

	if ((error = viPrintf(vi, "SMEM:STATE OFF\n")) != VI_SUCCESS)
		return( error );

	/* get the device ready to download via A24 space */
	if ((error = viPrintf(vi, "SMEM:MODE WRITE\n")) != VI_SUCCESS)
		return( error );

	if ((error = viPrintf(vi, "SMEM:STATE ON\n")) != VI_SUCCESS)
		return( error );

	if ((error = viPrintf(vi, "*OPC?\n")) != VI_SUCCESS)
		return( error );

	if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
		return( error );
		
	/* try again */
	if (readbuf[0] != '1')
		{
		if ((error = viPrintf(vi, "*OPC?\n")) != VI_SUCCESS)
			return( error );

		if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
			return( error );
		
		if (readbuf[0] != '1')
			return( RI3151_BINARY_DOWNLOAD_FAILED );
		}


	/* download the data points via A24 address */
	while( number_of_points-- )
		{
		if ((error = viOut16 (vi, 2, 0, ((ViUInt16) *data_pts) <<4)) != VI_SUCCESS)
			{
			viPrintf(vi, "SMEM:STATE OFF\n");
			return( error );
			}
		}		

	
    return( viPrintf(vi, "SMEM:STATE OFF\n") );
}

/*=========================================================================*/
/* This function loads arbitrary data from an ASCII data file              */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_load_ascii_file (ViSession vi, 
											ViInt16 segment_number,
											ViString file_name,
                                            ViInt32 number_of_points)
{
    ViInt32 max_size, file_count;
    ViUInt32 cnt;
    ViInt32 data_point;
    FILE *fp;
    ViStatus error;

	/* range check the parameters */
    if ((error = ri3151_int_range (segment_number, RI3151_MIN_SEG_NUMBER,
                                    RI3151_MAX_SEG_NUMBER, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );


	max_size = ri3151_get_mem_size(vi);
    if ((error = ri3151_int32_range (number_of_points, RI3151_MIN_SEGMENT_SIZE,
                                    max_size, VI_ERROR_PARAMETER4)) != VI_SUCCESS)
        return( error );

	/* try to open the data file */
	if ((fp = fopen(file_name, "r")) != NULL)
		return( RI3151_CANT_OPEN_FILE );

	/* set the flag to indicate first use of the file */
	global_file_flag = 0;

	/* select the arbitrary waveform segment */
	if ((error = viPrintf(vi, "TRAC:SEL %d\n", segment_number)) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	if ((error = viPrintf(vi, "SMEM:STATE ON\n")) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	/* get the device ready to download via A24 space */
	if ((error = viPrintf(vi, "SMEM:MODE WRITE\n")) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	if ((error = viPrintf(vi, "SMEM:STATE ON\n")) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	if ((error = viPrintf(vi, "*OPC?\n")) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}
		
	/* try again */
	if (readbuf[0] != '1')
		{
		if ((error = viPrintf(vi, "*OPC?\n")) != VI_SUCCESS)
			{
			fclose(fp);
			return( error );
			}

		if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
			{
			fclose(fp);
			return( error );
			}
		
		if (readbuf[0] != '1')
			{
			fclose(fp);
			return( RI3151_BINARY_DOWNLOAD_FAILED );
			}
		}

	/* download the data points via A24 address */
	file_count = 0;
	
	while( file_count < number_of_points )
		{
		if (ri3151_get_next_data_point(fp, &data_point) != 1)
			break;
			
		if (data_point < -2048 || data_point > 2047)
			{
			viPrintf(vi, "SMEM:STATE OFF\n");
			fclose(fp);
			return( RI3151_INVALID_FILE_DATA );
			}
		
		/* scale data in the range -2048 to 2047 to DAC range 0 to 4095 */
		/* then shift left into DAC bit positions */
		data_point += 2048;
		
		/* shift it left 4 places to put it into proper format */
		if ((error = viOut16 (vi, 2, 0, ((ViUInt16) data_point) << 4)) != VI_SUCCESS)
			{
			viPrintf(vi, "SMEM:STATE OFF\n");
			fclose(fp);
			return( error );
			}
			
		++file_count;
		}		

	
	fclose(fp);
	
    return( viPrintf(vi, "SMEM:STATE OFF\n") );

}



/*=========================================================================*/
/* Function: ri3151_load_wavecad_file()                                    */
/* Purpose:  This function reads a WaveCAD 2.0 data file and configures    */
/*           the instrument as indicated by the file                       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_load_wavecad_file (ViSession vi, ViChar filename[])
{
	int count, cmd_count;
	FILE *fp;
	ViStatus error;
	char filebuffer[100];
	ViBoolean first_wave;
	ViInt16 link, segment, max_link;
	ViInt32 loop_count, segment_length;
	char wave_file_name[256];
	
	/* open the file */
	fp = fopen(filename, "r");
	if (!fp)
		return( RI3151_CANT_OPEN_FILE );

	/* File Format: */
	/*
	 * "#Instrument"
	 * <Instrument Name>  (Presumably "Racal 3151")
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
		return( RI3151_WAVECAD_FORMAT_1 );
		}

	/*
	 * clear out the command table
	 */
	for (cmd_count = 0;  cmd_count < RI3151_MAX_CMDS;  ++cmd_count)
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
	 	if (cmd_count >= RI3151_MAX_CMDS)
	 		{
	 		fclose(fp);
	 		return( RI3151_WAVECAD_FORMAT_2 );
	 		}

	 	error = ri3151_parse_wavecad_state(&cmd_table[cmd_count], filebuffer);
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
	 	return( RI3151_WAVECAD_FORMAT_3 );
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
	 		return( RI3151_WAVECAD_FORMAT_4 );
	 		}
	 		
	 	error = ri3151_parse_wavecad_cmd(cmd_table, cmd_count, filebuffer);
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
		return( RI3151_WAVECAD_FORMAT_5 );
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
			return( RI3151_WAVECAD_FORMAT_6 );
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
			
		error = ri3151_parse_wavecad_seq(filebuffer, &link, &segment, &loop_count,
		                                 &segment_length, wave_file_name);
		if (error < 0)
			{
			fclose(fp);
			return( error );
			}
			
		if ((link >= RI3151_MAX_SEQ) || (link < 1))
			{
			fclose(fp);
			return( RI3151_WAVECAD_FORMAT_7 );
			}
	
		if (link > max_link)
			max_link = link;
			
		segment_numbers[link-1] = segment;
		segment_loops[link-1] = loop_count;
		
		error = ri3151_create_path_name(wave_file_name, filename);
		if (error < 0)
			{
			fclose(fp);
			return( error );
			}
			
		error = ri3151_load_wavecad_wave_file(vi, segment, wave_file_name);
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
		error = ri3151_define_sequence(vi, max_link, segment_numbers, segment_loops);
		if (error < 0)
			return( error );
		}
		
	/*
	 * finally, form all of the relevent commands and output
	 * them to the 3151
	 */
	return( ri3151_output_wavecad_commands(vi, cmd_table, cmd_count) );
}


/*=========================================================================*/
/* Function: ri3151_load_wavecad_wave_file()                               */
/* Purpose:  This function reads a WaveCAD 2.0 data file and configures    */
/*           the instrument as indicated by the file                       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_load_wavecad_wave_file (ViSession vi,
                                        		 ViInt16 segment,
                                        		 ViChar filename[])
		
{
#define PTS_PER_LOOP	((size_t) 512)
	FILE *fp;
	short databuf[PTS_PER_LOOP], data_point;
	int num_read, i;
    ViStatus error;
    ViUInt32 cnt;
    long file_size;

    /* check parameter range */
    if ((error = ri3151_int_range (segment, RI3151_MIN_SEG_NUMBER, 
    									   RI3151_MAX_SEG_NUMBER,
                                    	   VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );
	
	fp = fopen(filename, "rb");
	if (!fp)
		return( RI3151_CANT_OPEN_FILE );

	/*
	 * calculate the file size by setting to end of file and
	 * using ftell() to determine where you are
	 */
	fseek (fp, 0L, SEEK_END);
	file_size = ftell (fp) / 2;
	fseek (fp, 0L, SEEK_SET);
	
	/*
	 * define the trace
	 */
	error = viPrintf(vi, "TRAC:DEF %d,%d\n", segment, (int) file_size);
	if (error < 0)
		{
		fclose(fp);
		return( error );
		}
	
	/* select the segment and get ready for downloading data */
	/* select the arbitrary waveform segment */
	if ((error = viPrintf(vi, "TRAC:SEL %d\n", segment)) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	if ((error = viPrintf(vi, "SMEM:STATE OFF\n")) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	/* get the device ready to download via A24 space */
	if ((error = viPrintf(vi, "SMEM:MODE WRITE\n")) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	if ((error = viPrintf(vi, "SMEM:STATE ON\n")) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}


	if ((error = viPrintf(vi, "*OPC?\n")) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}

	if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
		{
		fclose(fp);
		return( error );
		}
		
	/* try again */
	if (readbuf[0] != '1')
		{
		if ((error = viPrintf(vi, "*OPC?\n")) != VI_SUCCESS)
			{
			fclose(fp);
			return( error );
			}

		if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
			{
			fclose(fp);
			return( error );
			}
		
		if (readbuf[0] != '1')
			{
			fclose(fp);
			return( RI3151_BINARY_DOWNLOAD_FAILED );
			}
		}

	/* read/download up to PTS_PER_LOOP points per loop */
	while((num_read = fread (databuf, sizeof(short), PTS_PER_LOOP, fp)) > 0)
		{
		/* check each data point/convert to upper 12-bits */
		for (i = 0;  i < num_read;  ++i)
			{
			data_point = databuf[i];
			
			if (data_point < -2048 || data_point > 2047)
				{
				viPrintf(vi, "SMEM:STATE OFF\n");
				fclose(fp);
				return( RI3151_INVALID_FILE_DATA );
				}
		
			/* scale data in the range -2048 to 2047 to DAC range 0 to 4095 */
			/* then shift left into DAC bit positions */
			data_point += 2048;

			/* shift it left 4 places to put it into proper format */
			if ((error = viOut16 (vi, 2, 0, ((ViUInt16) data_point) << 4)) != VI_SUCCESS)
				{
				viPrintf(vi, "SMEM:STATE OFF\n");
				fclose(fp);
				return( error );
				}
			}		
		}

	fclose(fp);

	/* return the 3151 control of shared memory */
    return( viPrintf(vi, "SMEM:STATE OFF\n") );
}

		


/*=========================================================================*/
/* This function outputs arbitrary waveforms.                              */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_output_arb_waveform (ViSession vi, ViInt16 segment_number,
                                              ViReal64 sampling_clock,
                                              ViReal64 amplitude,
                                              ViReal64 offset,
                                              ViInt16 clock_source)


{
    ViStatus error;

    /* check parameter ranges */

    if ((error = ri3151_int_range (segment_number, RI3151_MIN_SEG_NUMBER, RI3151_MAX_SEG_NUMBER,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

    if ((error = ri3151_real_range (sampling_clock, RI3151_MIN_SAMP_CLK, RI3151_MAX_SAMP_CLK,
                                    VI_ERROR_PARAMETER3)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    if ((error = ri3151_int_range (clock_source, RI3151_CLK_SOURCE_INT, RI3151_CLK_SOURCE_ECLTRG0,
                                   VI_ERROR_PARAMETER6)) != VI_SUCCESS)
        return( error );


    /* format the command and output it to the generator */

	switch ( clock_source )
		{
        case RI3151_CLK_SOURCE_INT:  error = viPrintf(vi, "FREQ:RAST:SOUR INT\n");
                                     break;

        case RI3151_CLK_SOURCE_EXT:  error = viPrintf(vi, "FREQ:RAST:SOUR EXT\n");
                                     break;

        case RI3151_CLK_SOURCE_ECLTRG0: error = viPrintf(vi, "FREQ:RAST:SOUR ECLTRG0\n");
                                     break;

     }

      if (error < 0)
         return (error);



    error = viPrintf (vi, "APPLY:USER %d, %.5g, %.4g, %.4g\n",
                        segment_number, sampling_clock, amplitude, offset);

    if (error < 0)
        return( error );



    error = viPrintf (vi, "FUNC:MODE USER\n");
    return( error );

}


/*=========================================================================*/
/* This function takes the sequence parameters                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_define_sequence (ViSession vi, 
                                          ViInt16 number_of_steps,
                                          ViInt16 *segment_number,
                                          ViInt32 *repeat_number)

{
    ViStatus error;
    int i;

	if ((error = ri3151_int_range (number_of_steps, RI3151_MIN_NUM_STEPS,
                                    RI3151_MAX_NUM_STEPS, VI_ERROR_PARAMETER2)) != VI_SUCCESS)

		return( error );


	for ( i= 0; i < number_of_steps; ++i)
		{
		if ((error = ri3151_int_range (segment_number[i], RI3151_MIN_SEG_NUMBER,
                                              RI3151_MAX_SEG_NUMBER, VI_ERROR_PARAMETER3)) != VI_SUCCESS)
			return( error );


		if ((error = ri3151_int32_range (repeat_number[i], RI3151_MIN_NUM_REPEAT,
                                              RI3151_MAX_NUM_REPEAT, VI_ERROR_PARAMETER4)) != VI_SUCCESS)
			return( error );



		error = viPrintf (vi, "SEQ:DEF %d, %d, %d\n",
                        i+1, segment_number[i], repeat_number[i]);

		if (error < 0)
			return( error );
		}

	return (error);
}


/*=========================================================================*/
/* This function Deletes one or more sequence numbers.                     */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_delete_sequence (ViSession vi)

{
	return( viPrintf(vi, "SEQ:DEL:ALL\n") );
}

/*=========================================================================*/
/* This function gets the sequence parameters and outputs the sequential   */
/* waveform.                                                               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_output_sequence_waveform (ViSession vi,
                                                   ViReal64 sampling_clock,
                                                   ViReal64 amplitude,
                                                   ViReal64 offset,
                                                   ViBoolean sequence_mode)
{
    ViStatus error;
    ViChar *param;

    /* check parameter ranges */
    if ((error = ri3151_real_range (sampling_clock, RI3151_MIN_SAMP_CLK, RI3151_MAX_SAMP_CLK,
                                    VI_ERROR_PARAMETER2)) != VI_SUCCESS)
        return( error );

	if((error = ri3151_check_ampl_and_offset(vi, amplitude, offset)) != VI_SUCCESS)
		return( error );
		
    /* check parameter ranges */
    if ((error = ri3151_boolean_range (sequence_mode, VI_ERROR_PARAMETER4)) != VI_SUCCESS)
        return( error );

	/* set the sequence advance mode */
	if (sequence_mode == RI3151_SEQ_AUTO)
		param = "AUTO";
	else
		param = "TRIG";

	error = viPrintf(vi, "SEQ:ADV %s\n", param);
	if (error < 0)
		return( error );

	/* format the command and output it to the generator */
    error = viPrintf (vi, "APPLY:USER 1, %f, %f, %f\n",
						sampling_clock, amplitude, offset);

    if (error < 0)
        return( error );


    return( viPrintf (vi, "FUNC:MODE SEQ\n") );
}


/*===================================================================================*/
/* This function generates a sequential waveform consisting of a series of waveform  */
/* segment.  They include : sine wave, ramp wave and sine^3 wave.  Each of the       */
/* waveform segments is repeated twice.                                              */
/*===================================================================================*/

ViStatus _VI_FUNC ri3151_example_generate_seq_waveform (ViSession vi)


{
   ViStatus error;
   ViInt16 i;
   ViInt16 segment_number;
   ViUInt16 array[400];
   ViInt16 segment_array[] = {1,2,3};
   ViInt32 repeat_array[] = {2,2,2};
   double pi = 3.1415926;

	for ( segment_number = 1; segment_number <= 3; segment_number ++ )
		{
		error =  ri3151_define_arb_segment (vi, segment_number, 360);
		if ( error < 0)
			return (error);

		if ( segment_number == 1)
        	{  
        	/* sending sine wave ( segment # 1) */
			for ( i = 0; i< 360; i++)
				array[i] =  (((ViInt16) (2047* sin (i*pi/180))) & 0xFFF) << 4;
			}
		else if ( segment_number == 2)
			{
			/* sending ramp wave ( segment # 2) */
			for ( i = 0; i< 360; i++)
				array[i] = (((ViInt16) (i * 2047.0/360.0)) & 0xFFF) << 14;
			}
		else if ( segment_number == 3)
			{
			/* sending sine^3 wave ( segment # 3) */
			for ( i = 0; i< 360; i++)
				{
				array[i] = (ViInt16) ((2047.0 * sin (i*pi/180) * sin (i*pi/180) * sin (i*pi/180) ) ) & 0xFFF;
				array[i] <<= 4;
				}
			}

		error = ri3151_load_arb_data (vi,  segment_number, array, (ViInt32) 360);
		if ( error < 0)
			return (error);

		error = ri3151_output_arb_waveform (vi, segment_number, 100.0E6, 5.0, 0.0, 0);
		if ( error < 0)
			return ( error );
       }


	error = ri3151_define_sequence (vi, 3, segment_array, repeat_array);
	if ( error < 0)
		return (error );

	error = ri3151_output_sequence_waveform (vi, 100.0E6, 5.0, 0.0, 
												RI3151_SEQ_AUTO);
	if ( error < 0)
		return (error );


	return( error );
}

/*=========================================================================*/
/* This function sets the timeout for the instrument.                      */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_set_timeout(ViSession vi, ViAttrState timeout)
{
    /* set the time-out attribute */
    return( viSetAttribute (vi, VI_ATTR_TMO_VALUE, timeout) );
}

/*=========================================================================*/
/* This function clears the instrument.                                    */
/* This function is equivalent to ViClear() and exists solely for          */
/* compatibility with previous driver releases                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_clear (ViSession vi)
{
    return( viClear(vi) );
}

/*=========================================================================*/
/* This function triggers the instrument.                                  */
/* This function is equivalent to ViAssertTrigger() and exists solely for  */
/* compatibility with previous driver releases                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_trigger (ViSession vi)
{
    return( viAssertTrigger(vi, VI_TRIG_PROT_DEFAULT) );
}

/*=========================================================================*/
/* This function polls the instrument.                                     */
/* This function is equivalent to viReadSTB() and exists solely for        */
/* compatibility with previous driver releases                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_poll (ViSession vi, ViInt16 *response)
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
/* This function returns the value of the current Voltage, Offset and      */
/* Frequency value as well as the status and type of the Filter assigned   */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_status_query (ViSession vi, ViReal64* voltage_value,
                                       ViReal64* frequency_value,
                                       ViReal64* offset_value, ViInt16 *filter_type)
{
    ViStatus error;
    ViUInt32 cnt;
    ViInt16 filter_state;


    error = viPrintf(vi, "SOUR:VOLT?\n");
    if (error < 0)
        return (error);


  /*   read the reply back from the instrument */

    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
        return( error );


    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;
    *voltage_value = strtod(readbuf, NULL);
  /*---------------------------------------------------*/

    error = viPrintf(vi, "SOUR:FREQ?\n");
    if (error < 0)
        return (error);


  /*   read the reply back from the instrument */

    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
        return( error );


    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;

    *frequency_value = strtod(readbuf, NULL);
   /*----------------------------------------------------*/

    error = viPrintf(vi, "SOUR:VOLT:OFFS?\n");
    if (error < 0)
        return (error);


  /*   read the reply back from the instrument */

    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
        return( error );


    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;

    *offset_value = strtod(readbuf, NULL);
   /*------------------------------------------------------*/

    error = viPrintf (vi, "OUTP:FILT?\n");
    if (error < 0)
        return (error);


    error = viScanf (vi, "%d", &filter_state);
    if (error < 0)
         return (error);

	if (filter_state == 1)                   /* Low pass filter is on */
		{
		error = viPrintf (vi, "OUTP:FILT:FREQ?\n");
        if (error < 0)
        return (error);

        error =  viScanf (vi, "%d", &filter_state);
        if (error < 0)
        return (error);

        switch (filter_state)
			{
			case 20: *filter_type = RI3151_FILTER_20MHZ;
					 break;

			case 25: *filter_type = RI3151_FILTER_25MHZ;
					 break;

			case 50: *filter_type = RI3151_FILTER_50MHZ;
					 break;
			}
		}
	else
		*filter_type = RI3151_FILTER_OFF;

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* This function reads what operating mode the generator is in.  If it is  */
/* in the standard waveform mode, it also checks which waveform is selected*/
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_mode_query (ViSession vi, ViInt16 *mode,
                            		 ViInt16 *standard_waveform)
{
    ViStatus error;
    ViUInt32 cnt;

	/* query the operating mode */
    error = viPrintf(vi, "FUNC:MODE?\n");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
        return( error );

    /* null-byte terminate the reply */
    readbuf[(ViInt16) cnt] = 0;


	*standard_waveform = RI3151_NON_STANDARD;
	
	/* check what the mode is */
	if (strncmp(readbuf, "USER", 4) == 0)
		*mode = RI3151_MODE_ARB;
	else if (strncmp(readbuf, "SEQ", 3) == 0)
		*mode = RI3151_MODE_SEQ;
	else if (strncmp(readbuf, "FIX", 3) == 0)
		{
		*mode = RI3151_MODE_STD;
		
		error = viPrintf(vi, "FUNC:SHAPE?\n");
		if (error < 0)
			return( error );

		
		/* read the reply back from the instrument */
		if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
			return( error );

		/* null-byte terminate the reply */
		readbuf[(ViInt16) cnt] = 0;
		
		if (strncmp(readbuf, "SINC", 4) == 0)
			*standard_waveform = RI3151_SINC;
		else if (strncmp(readbuf, "SIN", 3) == 0)
			*standard_waveform = RI3151_SINE;
		else if (strncmp(readbuf, "SQU", 3) == 0)
			*standard_waveform = RI3151_SQUARE;
		else if (strncmp(readbuf, "PUL", 3) == 0)
			*standard_waveform = RI3151_PULSE;
		else if (strncmp(readbuf, "RAM", 3) == 0)
			*standard_waveform = RI3151_RAMP;
		else if (strncmp(readbuf, "TRI", 3) == 0)
			*standard_waveform = RI3151_TRIANGLE;
		else if (strncmp(readbuf, "EXP", 3) == 0)
			*standard_waveform = RI3151_EXPONENTIAL;
		else if (strncmp(readbuf, "GAU", 3) == 0)
			*standard_waveform = RI3151_GAUSSIAN;
		else if (strncmp(readbuf, "DC", 2) == 0)
			*standard_waveform = RI3151_DC;
		else
			return( RI3151_INVALID_REPLY );
		}
	else
		return( RI3151_INVALID_REPLY );

	return( VI_SUCCESS );
}



/*=========================================================================*/
/* This function reads the phase lock loop state, coarse and fine phase    */
/* adjustments, and external input frequency                               */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_pll_query (ViSession vi, ViBoolean *on_off,
                           ViReal64 *coarsePhase, ViReal64 *finePhase,
                           ViReal64 *extFrequency)
{
	ViStatus error;
	ViInt16 index;
	ViInt32 long_index;
	ViUInt32 cnt;

    /* get the index into driver tables for this instance of the instrument */
    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &long_index);
    if (error < 0) return error;

	/* ensure we are a 3152 */
    index = (ViInt16) long_index;

	if (model_codes[index] != RI3152_MODEL_CODE)
		{
		*on_off = VI_FALSE;
		*coarsePhase = *finePhase = *extFrequency = 0.0;
		return( VI_SUCCESS );
		}


	/* query the on/off state of the phase lock loop */
    error = viPrintf(vi, "PHASE2:LOCK?\n");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
        return( error );

	/* if the first character is a '1', then it is ON */
	*on_off = (readbuf[0] == '1') ? VI_TRUE : VI_FALSE;

	/* query the coarse phase adjustment */
    error = viPrintf(vi, "PHASE2:ADJ?\n");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
        return( error );

	/* null-byte terminate the reply buffer */
	readbuf[cnt] = 0;
	
	/* convert the reply into a double-precision value */
	*coarsePhase = atof (readbuf);
	
	/* query the fine phase adjustment */
    error = viPrintf(vi, "PHASE2:FINE?\n");
    if (error < 0)
        return (error);

	/* read the reply back from the instrument */
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
        return( error );

	/* null-byte terminate the reply buffer */
	readbuf[cnt] = 0;
	
	/* convert the reply into a double-precision value */
	*finePhase = atof (readbuf);
	
	/* query the external frequency (if phase lock is ON) */
	*extFrequency = 0.0;
	if (*on_off)
		{
	    error = viPrintf(vi, "FREQ:EXT?\n");
    	if (error < 0)
        	return (error);

		/* read the reply back from the instrument */
    	if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
        	return( error );

		/* null-byte terminate the reply buffer */
		readbuf[cnt] = 0;
	
		/* convert the reply into a double-precision value */
		*extFrequency = atof (readbuf);
		}
		
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Read Status Byte                                              */
/* Purpose:  This function reads the IEEE-488.2 defined status byte        */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_read_status_byte (ViSession vi, ViInt16 *statusByte)
{
    ViStatus error;
    ViUInt32 cnt;
    
    if ((error = viPrintf(vi, "*STB?\n")) != VI_SUCCESS)
    	return( error );
    	
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
    	return( cnt );

	*statusByte = (ViInt16) atoi( readbuf );
	
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Set SRE Register                                              */
/* Purpose:  This function programs the IEEE-488.2 defined SRE register    */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_set_SRE (ViSession vi, ViInt16 SRERegister)
{
    ViStatus error;
    
    if ((error = ri3151_int_range(SRERegister, 0, 255, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
    	return( error );
    	
    return( viPrintf(vi, "*SRE %d\n", SRERegister & 0x00BF) );
}

/*=========================================================================*/
/* Function: Read SRE Register                                             */
/* Purpose:  This function reads the IEEE-488.2 defined SRE Register       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_read_SRE (ViSession vi, ViInt16 *SRERegister)
{
    ViStatus error;
    ViUInt32 cnt;
    
    if ((error = viPrintf(vi, "*SRE?\n")) != VI_SUCCESS)
    	return( error );
    	
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
    	return( cnt );

	*SRERegister = (ViInt16) atoi( readbuf );
	
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Read ESR Register                                             */
/* Purpose:  This function reads the IEEE-488.2 defined ESR Register       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_read_ESR (ViSession vi, ViInt16 *ESRRegister)
{
    ViStatus error;
    ViUInt32 cnt;
    
    if ((error = viPrintf(vi, "*ESR?\n")) != VI_SUCCESS)
    	return( error );
    	
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
    	return( cnt );

	*ESRRegister = (ViInt16) atoi( readbuf );
	
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Set ESE Register                                              */
/* Purpose:  This function programs the IEEE-488.2 defined ESE Register    */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_set_ESE (ViSession vi, ViInt16 ESERegister)
{
    ViStatus error;
    
    if ((error = ri3151_int_range(ESERegister, 0, 255, VI_ERROR_PARAMETER2)) != VI_SUCCESS)
    	return( error );
    	
    return( viPrintf(vi, "*ESE %d\n", ESERegister) );
}

/*=========================================================================*/
/* Function: Read ESE Register                                             */
/* Purpose:  This function reads the IEEE-488.2 defined ESE Register       */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_read_ESE (ViSession vi, ViInt16 *ESERegister)
{
    ViStatus error;
    ViUInt32 cnt;
    
    if ((error = viPrintf(vi, "*ESE?\n")) != VI_SUCCESS)
    	return( error );
    	
    if ((error = viRead(vi, (unsigned char *) readbuf, RI3151_READ_LEN, &cnt)) != VI_SUCCESS)
    	return( cnt );

	*ESERegister = (ViInt16) atoi( readbuf );
	
	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Self-Test                                                     */
/* Purpose:  This function executes the instrument self-test and returns   */
/* the result.                                                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_self_test (ViSession vi,
                                    ViInt16   * test_result,
                                    ViChar    * test_message)
{
    ViStatus error;
    ViUInt32 old_timeout;
    ViUInt32 cnt;
    ViChar reply[100];

    error = viGetAttribute (vi, VI_ATTR_TMO_VALUE, &old_timeout);
    if (error < 0) return error;

    error = viSetAttribute (vi, VI_ATTR_TMO_VALUE, (ViUInt32)10000);
    if  (error != VI_SUCCESS)
    {
        if (error > 0)
            error = RI3151_NSUP_ATTR_VALUE;
        return error;
    }


    /* send the *TST? test query */
    error = viPrintf(vi, "*TST?\n");
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
ViStatus _VI_FUNC ri3151_error_query (ViSession vi,
                                      ViInt32   *error_status,
                                      ViString  error_message)
{
    ViStatus error;
    ViUInt32 cnt;
    ViChar *strptr;

    /* set up default of "No Error" */
    *error_status = 0;
    *error_message = '\0';

    /* send the error query */
    if ((error = viWrite(vi, (unsigned char *) "SYST:ERR?\n", (ViUInt32) 10, &cnt)) != VI_SUCCESS)
        return( error );
        
    cnt = (ViUInt32) (sizeof(readbuf) - 1);
    if ((error = viRead (vi, (unsigned char *) readbuf, cnt, &cnt)) != VI_SUCCESS)
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
    	return( RI3151_INVALID_REPLY );

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
/* Function: Revision                                                      */
/* Purpose:  This function returns the driver and instrument revisions.    */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_revision_query (ViSession vi,
                                         ViString  driver_rev,
                                         ViString  instr_rev)
{
    ViStatus error;
    ViUInt32 cnt;
    ViChar *cp;
    static char *ri3151_idn_response = "Racal Instruments,3151,0,";
    static char *ri3152_idn_response = "Racal Instruments,3152,0,";

    /* send the *IDN? query */
    if ((error = viWrite(vi, (unsigned char*) "*IDN?\n", (ViUInt32) 6, &cnt)) != VI_SUCCESS)
        return( error );
        
    /* read the reply */
    if ((error = viRead (vi, (unsigned char *) readbuf, RI3151_MAX_CMD, &cnt)) != VI_SUCCESS)
        return( error );

    /* null-byte terminate/remove any <CR> or <LF> */
    cp = readbuf + cnt - 1;
    
    while ( *cp && isspace(*cp) )
        {
        *cp = '\0';
        --cp;
        }
        
    /* make sure this equals the expected string, up to the firmware revision */
    if ((strncmp(readbuf, ri3151_idn_response, strlen(ri3151_idn_response)) != 0)
    &&  (strncmp(readbuf, ri3152_idn_response, strlen(ri3152_idn_response)) != 0))
        return( RI3151_INVALID_REPLY );
        
    /*
     * return the instrument revision and the driver revision
     */
    cp = readbuf + strlen(ri3151_idn_response);
    strcpy(instr_rev, cp);
    strcpy(driver_rev, RI3151_REVISION);

    return VI_SUCCESS;
}


/*=========================================================================*/
/* Function: Error Message                                                 */
/* Purpose:  This function returns a text message for a corresponding      */
/* instrument driver error code.                                           */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_error_message (ViSession vi,
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


        case RI3151_MAX_INSTR_ERROR:
                strcpy(message, "There are more than the maximum (RI3151_MAX_INSTR) sessions open.  Use ri3151_close to free up space.");
                break;

        case RI3151_NSUP_ATTR_VALUE:
                strcpy(message, "A call to viSetAttribute returned an unsupported value warning");
                break;

        case RI3151_INVALID_REPLY:
                strcpy(message, "Invalid reply received");
                break;

		case RI3151_AMPLITUDE_ERROR:
				strcpy(message, "Invalid amplitude");
				break;

		case RI3151_OFFSET_ERROR:
				strcpy(message, "Invalid offset");
				break;

		case RI3151_AMPL_OFFSET_ERROR:
                strcpy(message, "The combination of the amplitude and offset is invalid");
                break;

        case RI3151_CANT_OPEN_FILE:
                strcpy(message, "Attempt to open the specified file failed");
                break;

        case RI3151_PERCENT_ERROR1:
               strcpy(message, "The total of delay + rise_time + high_time + fall_time > 100%");
               break;

        case RI3151_PERCENT_ERROR2:
               strcpy(message, "The total of delay + rise_time + fall_time > 100%");
               break;

		case RI3151_INVALID_FILE_DATA:
				strcpy(message, "The data in the file is not in the range -2048 to +2047");
				break;

		case RI3151_WAVECAD_FORMAT_1:
				strcpy(message, "WaveCAD file format error (Missing '#Param' line)");
				break;
				
		case RI3151_WAVECAD_FORMAT_2:
				strcpy(message, "WaveCAD file form (Command count in '#Param' section is too long)");
				break;
				
		case RI3151_WAVECAD_FORMAT_3:
				strcpy(message, "WaveCAD file format error (Missing '#Gpib' line)");
				break;
				
		case RI3151_WAVECAD_FORMAT_4:
				strcpy(message, "WaveCAD file format error (Too many commands in '#Gpib' section)");
				break;
				
		case RI3151_WAVECAD_FORMAT_5:
				strcpy(message, "WaveCAD file format error (Missing '#Sequencer' line)");
				break;
				
		case RI3151_WAVECAD_FORMAT_6:
				strcpy(message, "WaveCAD file format error (Missing 'Link#' at beginning of line)");
				break;
				
		case RI3151_WAVECAD_FORMAT_7:
				strcpy(message, "WaveCAD file format error (Link number is not in range 1 to 1024)");
				break;
				
		case RI3151_WAVECAD_FORMAT_8:
				strcpy(message, "WaveCAD file format error (Command ID in '#Gpib' section not found in '#Param' section)");
				break;
				
		case RI3151_WAVECAD_FORMAT_9:
				strcpy(message, "WaveCAD file format error (SCPI command is too long)");
				break;
				
		case RI3151_WAVECAD_FORMAT_10:
				strcpy(message, "WaveCAD file format error (SCPI command parameter is too long)");
				break;

		case RI3151_WAVECAD_FORMAT_11:
				strcpy(message, "WaveCAD file format is not error (Missing '|' between SCPI command parameters)");
				break;
				
		case RI3151_BINARY_DOWNLOAD_FAILED:
				strcpy(message, "Not ready to download binary data through A24 memory");
				break;

		case RI3151_PHASE_LOCK_NOT_SUPP:
				strcpy(message,"Phase lock mode supported only on 3152, not on 3151");
				break;

		case RI3151_PHASE_MODE_CONFLICT:
				strcpy(message, "Phase Adjust not allowed in Sequence Mode");
				break;

		default:
            if(viStatusDesc(vi, error_code, message) != VI_SUCCESS)
              {
                 strcpy(message, "Unknown error for ri3151");
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
ViStatus _VI_FUNC ri3151_reset (ViSession vi)
{
    ViStatus error;

    error = viClear(vi);
    if (error < 0)
        return( error );


    /* send the instrument preset command */
    if ((error = viPrintf(vi, "*RST\n")) != VI_SUCCESS)
        return( error );

    return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Close                                                         */
/* Purpose:  This function closes the instrument.                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri3151_close (ViSession vi)

{
    ViAttrState index;
    ViStatus error;

    /* Reset the "initialized" array  for instrument */
    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error == VI_SUCCESS)
    {
        initialized[(ViInt16)index] = RI3151_NO_SESSION;
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
static ViStatus ri3151_real_range (ViReal64 val,
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
static ViStatus ri3151_int_range (ViInt16 val,
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
static ViStatus ri3151_int32_range (ViInt32 val,
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
static ViStatus ri3151_boolean_range (ViBoolean val,
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
static ViStatus ri3151_initCleanUp (ViSession rmSession, ViSession *vi,
    ViInt16 index, ViStatus currentStatus)
{
    viClose (*vi);
    viClose (rmSession);
    *vi = VI_NULL;
    initialized[index] = RI3151_NO_SESSION;
    return (currentStatus);
}

/*=========================================================================*/
/* Function: ri3151_get_mem_size()                                         */
/* Purpose:  This function gets the maximum memory (for a arbitrary data   */
/*           segment) for the specified 3151.  The 3151 may have 64K or    */
/*           512K words of memory for arb data.  There are 2K less points  */
/*           available to accommodate sequences, etc.                      */
/*=========================================================================*/
static ViInt32 ri3151_get_mem_size(ViSession vi)
{
	int i;
	
	for (i = 0;  i < RI3151_MAX_INSTR;  ++i)
		if (initialized[i] == vi)
			{
			if (large_memory[i])
				return( RI3151_MAX_512K_SEG_SIZE );
			else
				return( RI3151_MAX_64K_SEG_SIZE );
			}

	/* default case -- let VISA I/O report invalid session */
	return( RI3151_MAX_64K_SEG_SIZE );
}


/*=========================================================================*/
/* Function: ri3151_check_ampl_and_offset()                                */
/* Purpose:  To check that the amplitude and offset for a standard         */
/*           waveform are valid.  The 3151 has three output ranges which   */
/*           are automatically selected by the amplitude:  +/- 80 mV,      */
/*           +/- 800 mV, and +/- 8V.  The amplitude + offset must lie in   */
/*           the selected range.                                           */
/*=========================================================================*/
static ViStatus ri3151_check_ampl_and_offset(ViSession vi,
											 ViReal64 amplitude, 
                                             ViReal64 offset)
{
	ViReal64 half_amplitude;
	ViReal64 offset_absolute_value;
	ViInt32 long_index;
	ViInt16 index;
	ViStatus error;
	
	
	if (amplitude > RI3151_MAX_AMPLITUDE || amplitude < RI3151_MIN_AMPLITUDE)
		return( RI3151_AMPLITUDE_ERROR );
		
	if (offset > RI3151_MAX_OFFSET || offset < RI3151_MIN_OFFSET)
		return( RI3151_OFFSET_ERROR );
		
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
				return( RI3151_AMPL_OFFSET_ERROR );
			}
		else if (half_amplitude <= 0.8)
			{
			if (half_amplitude + offset_absolute_value > 0.8)
				return( RI3151_AMPL_OFFSET_ERROR );
			}
		else
			{
			if (half_amplitude + offset_absolute_value > 8.0)
				return( RI3151_AMPL_OFFSET_ERROR );
			}
		}
	else /* newer, loose range checking */
		{
		if (half_amplitude + offset_absolute_value > 8.0)
			return( RI3151_AMPL_OFFSET_ERROR );
		}
		
	return( VI_SUCCESS );
}


/*=========================================================================*/
/* Function: ri3151_get_next_data_point()                                  */
/* Purpose:  This function takes the place of fscanf() which is not        */
/*           not supported by the Microsoft C++ libraries used for a DLL   */
/* Returns:  1 if a valid number is found, 0 otherwise                     */
/*=========================================================================*/
static ViInt16 ri3151_get_next_data_point(FILE *fp, ViInt32 *data_point)
{                              
	/* maximum file line is 256 characters!!! */
	static char filebuf[256];
	static char *dataptr = 0;
	ViInt32 value;
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
/* Function: ri3151_parse_wavecad_state                                    */
/* Purpose:  Parses a state entry from the WaveCAD file                    */
/*=========================================================================*/
static ViStatus ri3151_parse_wavecad_state(WaveCADCmdStruct *table_entry, 
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
/* Function: ri3151_parse_wavecad_cmd                                      */
/* Purpose:  Parses a command entry from the WaveCAD file                  */
/*=========================================================================*/
static ViStatus ri3151_parse_wavecad_cmd(WaveCADCmdStruct cmd_table[], 
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
		return( RI3151_WAVECAD_FORMAT_8 );


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
		return( RI3151_WAVECAD_FORMAT_9 );

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
		return( RI3151_WAVECAD_FORMAT_10 );

	*dest = '\0';

	return( VI_SUCCESS );
}


/*=========================================================================*/
/* Function: ri3151_parse_wavecad_seq                                      */
/* Purpose:  Parses a sequence entry from the WaveCAD file                 */
/*=========================================================================*/
static ViStatus ri3151_parse_wavecad_seq(char *fileline, 
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
/* Function: ri3151_create_path_name                                       */
/* Purpose:  Creates a full path name from the specified wave_file_name    */
/*           and the file_name.  The path component of the file name is    */
/*           used as a prefix for the wave_file_name                       */
/*=========================================================================*/
static ViStatus ri3151_create_path_name(char *wave_file_name, char *file_name)
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
/* Function: ri3151_output_wavecad_commands                                */
/* Purpose:  Outputs each of the commands in the command table using the   */
/*           present value as found in the WaveCAD file                    */
/*=========================================================================*/
static ViStatus ri3151_output_wavecad_commands(ViSession vi, 
                                        	WaveCADCmdStruct cmd_table[], 
                                        	int cmd_count)
{
	char param[50];
	char *start, *end, *ptr;
	WaveCADCmdStruct *table_entry;
	int index;
	int int_choice;
	ViStatus error;

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
				error = viPrintf(vi, "%s %.5g\n",
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
						return( RI3151_WAVECAD_FORMAT_11 );
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

