// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2021-07-02 10:35:5#$: Date of last commit
//    $Rev:: 28275              $: Revision of last commit
/****************************************************************************
*  FILENAME   :  Dig_ScopeAdd.c
*
*  REVISION   :  1.3
*
*  DESCRIPTION:  PAWS Driver for the HPE1428AB Digitizing Oscilloscope
*                used in the ManTech TETS System.
*****************************************************************************
*                     SOURCE REVISION HISTORY
*
*  VER  DATE     DESCRIPTION                                 AUTHOR
*  ---  -------  -------------------------                   --------------------------
*  1.0 21APR99   Initial Release                             Y. Shikarpuri
*  1.1 01MAR00   Modified DC Voltage Measurement and added 
*                DC-Offset to expand the vertical axis.      Y. Shikarpuri
*  1.2 11MAY00   Added a "WriteHPe1428AB(hp1428_string);"
*		     statement in "mea_voltage_dc" function  	 Mike Eckart, MTSI
*		     to tell scope which channel to 
*		     take measurement at.  Also, commented out many 
*		     "Sleep(5000)" delay statements. 
*  1.3 11OCT01   Within scope_setup added code to support    J.Joiner, MTSI 
*                sample count and the trigger for pulsed dc,
*                triangle, AC, ramp and square wave.(ECO-563)
*  x.x  20AUG02  Jeff Hill, MTSI, DR 287 (1157)
*				 Refer to Common.C for change description
*  x.x			 Refer to Common.C for all future change descriptions
*  3.0  28JUN17  Updated for GPATSCIC                         Jewitcher,Astronics
*****************************************************************************
*   NOTES:
*     PnP Drivers Used:
*     hpe1428a_writeInstrData As ViStatus (ByVal instrumentHandle As ViSession,
*                                          ByVal Out_Buf As ViString)
*
*     hpe1428a_readInstrData As ViStatus  (ByVal instrumentHandle As ViSession,
*                                          ByVal numberBytesToRead As ViInt16,
*                                          readBuffer As ViChar,
*                                          BytesRead As ViInt32)
*
*     hpe1428a_errorMessage As ViStatus   (ByVal instrumentHandle As ViSession,
*                                          ByVal errorCode As ViStatus,
*                                          errorMessage As ViChar))
*
*     hpe1428a_close As ViStatus (ByVal instrumentHandle As ViSession)
*
*     hpe1428a_reset As ViStatus (ByVal instrumentHandle As ViSession)
*
*     hpe1428a_init as ViStatus(ByVal resourceName as ViRsrc,
*                               ByVal IDQuery as ViBoolean,
*                               ByVal resetDevice as ViBoolean,
*                               instrumentHandle as ViSession);
****************************************************************************/

#include "key.h"
#pragma warning(disable : 4115)
#include "cem.h"
#pragma warning(default : 4115)
#include <string.h>
#include <stdio.h>
#include <math.h>
#include <conio.h>
#include <stdlib.h>
#include <malloc.h>
#include "visa.h"
#include <winbase.h>
#include "tets.h"
#include "common.h"
#include "..\..\..\..\ATS\Include\AtXmlDriverFunc.h"
#include "..\..\..\..\ATS\Include\AtXmlInterfaceApiC.h"
#include <Windows.h>
#include <chrono>
#include <string>

extern int IsSimOrDeb(char dev_name[20]); 

#define FLAG	10
#define	CHECK	0
#define	NOCHECK	1
#define WAVEBYTES 17000

#define VI_SESSION_NOT_USED 0

DATUM		*amplitude_datum, *frequency_datum, *period_datum, *time_datum, *smplcnt_datum,
			*slope_datum, *trglvl_datum, *fdatum, *smpltime_datum, *risetime_datum, *src_datum,
			*falltime_datum, *pulsewidth_datum, *dest_datum, *loadSrc_datum, *allow_datum,*cmpSrc_datum,
			*cmpDst_datum, *source1_datum, *source2_datum, *result_datum;

int cnt, i ;
int			set_lev = 0, chan, autoscl,string1,string2,comp_flag;
char		*event_slope,*saveSrc,*dest,*loadSrc,*compSrc,*source1,*source2,*test_ch,
			*compDst,*dfrResult,/**dfrResult,*/*intResult,*sum,*product,*diff,*violation;
ViInt32	BytesRead;	// was ViUInt32 CJW
ViString	Out_Buf, Mea_Buf;
ViChar		*errorMessage;
char junk[17001];// size was 4100
ViPBuf		DigScopeRdBuf = (ViPBuf)&junk[0];
ViReal64	measurementValue;
ViStatus	errorCode, scanReturn;
ViBuf		hp1428_string[256];
double		amplitude, offset, frequency, period, event_triglev, timevar, sampletime, risetime, falltime, pulsewidth, sweeptime, sample_cnt, allow,*comp_holder; 
char		hp1428_msg_buf[256], fstring[80], fromdata[20], todata[20], measource[20], tmpBuf[256],	coupling[80];
static int	hp1428_debug_flag = 0, av_meas_flag = 0;
int			trg_flg = 0, noProbe = 0;
static int	scope_reset_flag = 1;
static int	scopeInReset = 0;
static int	isAllocated = 0;
bool bInit = false;

/* Prototypes */
void WriteHPe1428AB(ViString, int); /*function to send strings to instrument */
void CheckHPe1428ABErrors(void);   /*function to check instrument for errors */
void formatAndWrite(int, char*, ...);
extern int WaitForProbeButt (double MaxT);
extern void display_error();
extern void reset_scope();
extern int read_wave_test();

const double OVERRANGE   = 9.9e37;
char* RESET              = "*RST";
char* STOP               = "STOP";
char* AUTOSCALE          = "AUT";
char* MEAS_CLEAR_QUE     = "MEAS:SCR";
char* MEAS_SOURCE        = "MEAS:SOUR ";
char* MEAS_FREQ          = "MEAS:FREQ?";
char* MEAS_PERIOD        = "MEAS:PER?";
char* MEAS_DUTYCYCLE     = "MEAS:DUT?";
char* MEAS_VOLTS_PP      = "MEAS:VPP?";
char* MEAS_VOLTS_MAX     = "MEAS:VMAX?";
char* MEAS_VOLTS_AMP     = "MEAS:VAMP?";
char* MEAS_VOLTS_MIN     = "MEAS:VMIN?";
char* MEAS_VOLTS_AVG     = "MEAS:VAV?";
char* MEAS_RISETIME      = "MEAS:RIS?";
char* MEAS_FALLTIME      = "MEAS:FALL?";
char* MEAS_PRESHOOT      = "MEAS:PRES?";
char* MEAS_OVERSHOOT     = "MEAS:OVER?";
char* MEAS_PULSEWDTH_POS = "MEAS:PWID?";
char* MEAS_PULSEWDTH_NEG = "MEAS:NWID?";

char* TIME_REF_LEFT      = "TIM:REF LEFT";
char* TIME_REF_CENTER    = "TIM:REF CENT";
char* TIME_REF_RIGHT     = "TIM:REF RIGH";

char* TIME_MODE_AUTO     = "TIM:MODE AUTO";
char* TIME_MODE_TRIGGER  = "TIM:MODE TRIG";
char* TIME_MODE_SINGLE   = "TIM:MODE SING";

char* TIME_SAMPLE_REAL   = "TIM:SAMP REAL";
char* TIME_SAMPLE_REPEAT = "TIM:SAMP REP";

char* ACQUISITION_TYPE_NORM = "ACQ:TYPE NORM";
char* ACQUISITION_TYPE_AVG  = "ACQ:TYPE AVER";
char* ACQUISITION_TYPE_ENV  = "ACQ:TYPE ENV";
char* ACQUISITION_TYPE_RAW  = "ACQ:TYPE RAWD";

char* ACQUISITION_COUNT     = "ACQ:COUN ";

enum timeMode
{
	AUTO,
	TRIGGER,
	SINGLE
};

enum timeReference
{
	LEFT,
	CENTER,
	RIGHT
};

enum samplingMode
{
	REAL,
	REPEAT
};

enum acquisitionType
{
	NORMAL,
	AVERAGE,
	ENVELOPE,
	RAW_DATA
};

void checkinstrumentinit()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - checkinstrumentinit()\033[m\n");
	}

	//MessageBox(0, "Scope WCEM", "checkinstrumentinit" , MB_OK);
	static int do_once = 0;
	if (do_once != 0 || DigScopeRdBuf == 0)
	{
		return;
	}

	do_once = 1;
	allocateDigScoprReadBuffer(ALLOC);
}

void abort_acquisition()
{
	formatAndWrite(NOCHECK, STOP);

	return;
}

void clear_measure_que()
{
	formatAndWrite(CHECK, MEAS_CLEAR_QUE);

	return;
}

void set_time_mode(timeMode mode)
{
	switch (mode)
	{
	case AUTO:
		formatAndWrite(CHECK, TIME_MODE_AUTO);
		break;

	case TRIGGER:
		formatAndWrite(CHECK, TIME_MODE_TRIGGER);
		break;

	case SINGLE:
		formatAndWrite(CHECK, TIME_MODE_SINGLE);
		break;
	}
	
	return;
}

void set_time_ref(timeReference reference)
{
	switch (reference)
	{
	case LEFT:
		formatAndWrite(CHECK, TIME_REF_LEFT);
		break;
	
	case CENTER:
		formatAndWrite(CHECK, TIME_REF_CENTER);
		break;

	case RIGHT:
		formatAndWrite(CHECK, TIME_REF_RIGHT);
		break;
	}

	return;
}

void set_sampling_mode(samplingMode mode)
{
	switch (mode)
	{
	case REAL:
		formatAndWrite(CHECK, TIME_SAMPLE_REAL);
		break;
	case REPEAT:
		formatAndWrite(CHECK, TIME_SAMPLE_REPEAT);
		break;
	}

	return;
}

/*********************************************************************************/
/*  			Set Acquisition Type (NORMAL, AVG, ENVELOPE, RAW)	 			 */
/*********************************************************************************/
void set_acquisition_type(acquisitionType type)
{
	switch(type)
	{
	case NORMAL:
		formatAndWrite(CHECK, ACQUISITION_TYPE_NORM);
		break;
	case AVERAGE:
		formatAndWrite(CHECK, ACQUISITION_TYPE_AVG);
		break;
	case ENVELOPE:
		formatAndWrite(CHECK, ACQUISITION_TYPE_ENV);
		break;
	case RAW_DATA:
		formatAndWrite(CHECK, ACQUISITION_TYPE_RAW);
		break;
	}

	return;
}

/*********************************************************************************/
/*  			Set Acquisition Count (number of points to average)	 			 */
/*********************************************************************************/
void set_acquisition_count(double count)
{
	if (count != NULL)
	{
		char msg[250];
		char val[10];
		sprintf_s(msg, ACQUISITION_COUNT);
		sprintf_s(val, "%f", count);
		strcat_s(msg, val);

		formatAndWrite(CHECK, msg);
	}
	
	return;
}

/*********************************************************************************/
/*  			Setup Trigger source, slope and level 		 					 */
/*********************************************************************************/
void get_trigger(int trgprt)
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - get_trigger()\033[m\n");
		//MessageBox(0, "Scope WCEM", "Debug:  get_trigger" , MB_OK);
	}

	scope_reset_flag = 0;
	set_time_mode(TRIGGER);
	formatAndWrite(CHECK, "%s","TRIG:MODE EDGE");

	switch(trgprt)
	{
		case 1:
			formatAndWrite(CHECK, "%s","TRIG:SOUR CHAN1");
			break;
		case 2:
			formatAndWrite(CHECK, "%s","TRIG:SOUR CHAN2");
			break;
		case 3:
			formatAndWrite(CHECK, "%s","TRIG:SOUR EXT");
			break;
		default:
			formatAndWrite(CHECK, "%s","TRIG:SOUR CHAN1");
			break;
	}

	trglvl_datum = GetDatum(M_VOLT,K_SET);

	if (trglvl_datum != 0)
	{
		event_triglev = DECDatVal(trglvl_datum, 0);
	}
	else
	{
		event_triglev = 0;
	}

	formatAndWrite(CHECK, "TRIG:LEV %f",event_triglev);
	slope_datum = GetDatum(M_EVSL,K_SET);

	if (slope_datum != 0)
	{
		event_slope = GetTXTDatVal(slope_datum, 0);
	}
	else
	{
		event_slope = "POS";
	}

	formatAndWrite(CHECK, "TRIG:SLOP %s",event_slope);
	autoscl = 0;

	// Implemented when comparing scope to VIPERT, Need to check on this one
	//formatAndWrite(CHECK, "TRIG:DEL TIME, 3E-5");
}

void set_coupling()
{
	DATUM	*couple_datum;

	sprintf_s(coupling,80,"");
	couple_datum = GetDatum(M_CPLG,K_SET);

	if (couple_datum !=0)
	{
		sprintf_s(coupling,80,GetTXTDatVal(couple_datum,0));

		formatAndWrite(CHECK, "CHAN%d:COUP %s", chan, coupling);
	}

	return;
}

void set_impedance()
{
	DATUM	*imp_datum;
	double imp_value;
	int imped;

	imp_datum = GetDatum(M_TIMP,K_SET);

	if (imp_datum !=0)
	{
		imp_value = DECDatVal(imp_datum,0);
		imped = (int) imp_value;		
	
		if (imp_value > 50)
		{
			imped = 1000000;
		}
		else
		{
			imped = 50;
		}

		if (imped == 50)
		{
			formatAndWrite(CHECK, "CHAN%d:COUP DCF", chan);  /* DC coupling, Fifty ohms */
		}
	}

	return;
}

void set_dc_offset(double offsetArg=0.0)
{
	DATUM	*offset_datum;

	offset = 0.0;

	/* this will default the offset to 0.0 volts if nothing is provided,
	   if a value is provided by the calling process it will be used,
	   otherwise it will check the datum for a value */

	if (offsetArg == 0.0)
	{
		offset_datum = GetDatum(M_DCOF, K_SET);

		if (offset_datum != 0)
		{
			offset = DECDatVal(offset_datum,0);
		}
	}
	else
	{
		offset = offsetArg;
	}
	
	formatAndWrite(CHECK, "CHAN%d:OFFS %f", chan, offset);

	return;
}

/*********************************************************************************/
/*  					Setup The Amplitude			 							 */
/*********************************************************************************/
void set_scope_ampl()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - set_scope_ampl()\033[m\n");
		//MessageBox(0, "Scope WCEM", "set_scope_ampl" , MB_OK);
	}

	// Check Voltage Peak-to-peak MAX first


	amplitude_datum = GetDatum(M_VLPP, K_SRX);

	if (amplitude_datum != 0)
	{
		amplitude = DECDatVal(amplitude_datum, 0);
	}
	else // No VLPP MAX
	{
		amplitude_datum = GetDatum(M_VLPP, K_SET); // VLPP value is next
		
		if (amplitude_datum != 0)
		{
			amplitude = DECDatVal(amplitude_datum, 0);
		}
		else // No VLPP
		{
			amplitude_datum = GetDatum(M_VLPP, K_SRN); // VLPP MIN is last

			if (amplitude_datum != 0)
			{
				amplitude = DECDatVal(amplitude_datum, 0);
			}
			else  // Check Voltage Peak
			{
				amplitude_datum = GetDatum(M_VLPK, K_SRX);
				
				if (amplitude_datum != 0)
				{
					amplitude = DECDatVal(amplitude_datum, 0) * 2;
				}
				else
				{
					amplitude_datum = GetDatum(M_VLPK, K_SET);

					if (amplitude_datum != 0)
					{
						amplitude = DECDatVal(amplitude_datum, 0) * 2;
					}
					else
					{
						amplitude_datum = GetDatum(M_VLPK, K_SRN);

						if (amplitude_datum != 0)
						{
							amplitude = DECDatVal(amplitude_datum, 0) * 2;
						}
						else // Check Voltage Peak Pos
						{
							amplitude_datum = GetDatum(M_VPKP, K_SRX);

							if (amplitude_datum != 0)
							{
								amplitude = DECDatVal(amplitude_datum, 0) * 2;
							}
							else
							{
								amplitude_datum = GetDatum(M_VPKP, K_SET);

								if (amplitude_datum != 0)
								{
									amplitude = DECDatVal(amplitude_datum, 0) * 2;
								}
								else
								{
									amplitude_datum = GetDatum(M_VPKP, K_SRN);

									if (amplitude_datum != 0)
									{
										amplitude = DECDatVal(amplitude_datum, 0) * 2;
									}
									else // Check Voltage Peak Neg
									{
										amplitude_datum = GetDatum(M_VPKN, K_SRX);

										if (amplitude_datum != 0)
										{
											amplitude = DECDatVal(amplitude_datum, 0) * 2;
										}
										else
										{
											amplitude_datum = GetDatum(M_VPKN, K_SET);

											if (amplitude_datum != 0)
											{
												amplitude = DECDatVal(amplitude_datum, 0) * 2;
											}
											else
											{
												amplitude_datum = GetDatum(M_VPKN, K_SRN);

												if (amplitude_datum != 0)
												{
													amplitude = DECDatVal(amplitude_datum, 0) * 2;
												}
												else // Check Voltage
												{ 
													amplitude_datum = GetDatum(M_VOLT, K_SRX);

													if (amplitude_datum != 0)
													{
														amplitude = DECDatVal(amplitude_datum, 0);
													}
													else
													{
														amplitude_datum = GetDatum(M_VOLT, K_SET);

														if (amplitude_datum != 0)
														{
															amplitude = DECDatVal(amplitude_datum, 0);
														}
														else
														{
															amplitude_datum = GetDatum(M_VOLT, K_SRN);

															if (amplitude_datum != 0)
															{
																amplitude = DECDatVal(amplitude_datum, 0);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		sprintf_s(msg_buf,sizeof(msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - set_scope_ampl() - found amplitude [%.7f]\n",amplitude);
		Display(msg_buf);

		//MessageBox(0, "Scope WCEM", "set_scope_ampl" , MB_OK);
	}


	if (amplitude_datum != 0)
	{
		amplitude = amplitude * 1.5;

		if (GetCurMChar() == M_VOLT && coupling[0] == 'D')
		{
			// only if measuring dc volts
			amplitude = amplitude * 2;
		}

		// adjust scale to account for dc offset
		amplitude = amplitude + offset;

		if(amplitude > 40)
		{
			/* Display message indicating that value has been clipped */
			Display("\033[33;40m *** Amplitude exceeded 40 volts.  Will be set to 40 volts. ***\033[m\n");
			amplitude = 40;
		}
	}
	else
	{
		amplitude = 4; /* default */

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - set_scope_ampl() - amplitude set to default value of 4\033[m\n");
			//MessageBox(0, "Scope WCEM", "set_scope_ampl" , MB_OK);
		}
	}	
	formatAndWrite(CHECK, "CHAN%d:RANG %f", chan, amplitude);

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		sprintf_s(msg_buf,sizeof(msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - set_scope_ampl() - amplitude set to %.7f\n", amplitude);
		Display(msg_buf);

		//MessageBox(0, "Scope WCEM", "set_scope_ampl" , MB_OK);
	}
}

void set_probe_atten()
{
	DATUM	*attn_datum;
	double atten = 0.0;

	attn_datum = GetDatum(M_VOLT, K_SRX); // Check M_VOLT MAX first

	if (attn_datum != 0)
	{
		atten = DECDatVal(attn_datum, 0);
	}
	else // No VOLT MAX
	{
		attn_datum = GetDatum(M_VOLT, K_SET); // M_VOLT value is next

		if (attn_datum != 0)
		{
			atten = DECDatVal(attn_datum, 0);
		}
		else // No VOLT
		{
			attn_datum = GetDatum(M_VOLT, K_SRN); // VOLT MIN is last

			if (attn_datum != 0)
			{
				atten = DECDatVal(attn_datum, 0);
			}
		}
	}

	if (attn_datum !=0)
	{
		if (atten > 5)
		{
			atten = 10;
		}
	}
	else
	{
		atten = 1; // default to 1:1 probe attenuation value
	}

	formatAndWrite(CHECK, "CHAN%d:PROB %f", chan, atten);
	
	return;
}

/*********************************************************************************/
/*  					Setup The Sweeptime			 							 */
/*********************************************************************************/
void set_scope_sweeptime()
{
	const int DIVISIONS = 10;
	const double PULSES = 1.5;
	const double WAVES = 2.5;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - set_scope_sweeptime()\033[m\n");
		//MessageBox(0, "Scope WCEM", "set_scope_sweeptime" , MB_OK);
	}

	// Checking frequency datum first
	// ******************************
	frequency_datum = GetDatum(M_FREQ, K_SRX); // Check FREQ MAX first

	if (frequency_datum != 0)
	{
		frequency = DECDatVal(frequency_datum, 0);
	}
	else // No FREQ MAX
	{
		frequency_datum = GetDatum(M_FREQ, K_SET); // FREQ value is next

		if (frequency_datum != 0)
		{
			frequency = DECDatVal(frequency_datum, 0);
		}
		else // No FREQ
		{
			frequency_datum = GetDatum(M_FREQ, K_SRN); // FREQ MIN is last

			if (frequency_datum != 0)
			{
				frequency = DECDatVal(frequency_datum, 0);
			}
			else // Check Pulse Repitition Frequency Datum
			{
				frequency_datum = GetDatum(M_PRFR, K_SRX); // Check PRF MAX first

				if (frequency_datum != 0)
				{
					frequency = DECDatVal(frequency_datum, 0);
				}
				else // No PRF MAX
				{
					frequency_datum = GetDatum(M_PRFR, K_SET); // FREQ value is next

					if (frequency_datum != 0)
					{
						frequency = DECDatVal(frequency_datum, 0);
					}
					else // No PRF
					{
						frequency_datum = GetDatum(M_PRFR, K_SRN); // PRF MIN is last
			
						if (frequency_datum != 0)
						{
							frequency = DECDatVal(frequency_datum, 0);
						}
					}
				}
			}
		}
	}

	if (frequency_datum != 0)
	{
		sweeptime = (1/frequency) * WAVES; // was 4 - JLW
	}
	else // Checking Period datum
	{
		period_datum = GetDatum(M_PERI, K_SRX); // Check period MAX first

		if (period_datum != 0)
		{
			period = DECDatVal(period_datum, 0);
		}
		else // No PERIOD MAX
		{
			period_datum = GetDatum(M_PERI, K_SET); // period value is next

			if (period_datum != 0)
			{
				period = DECDatVal(period_datum, 0);
			}
			else // No period
			{
				period_datum = GetDatum(M_PERI, K_SRN); // period MIN is last

				if (period_datum != 0)
				{
					period = DECDatVal(period_datum, 0);
				}
			}
		}

		if (period_datum != 0)
		{
			sweeptime = period * WAVES; // was 2 - JLW
		}
		else // Checking Time Datum
		{ 
			time_datum = GetDatum(M_TIME, K_SRX);

			if (time_datum !=0)
			{
				timevar = DECDatVal(time_datum,0);
   			}
		
			if (time_datum != 0)
			{
				sweeptime = timevar * 1.5;
			}
			else // Checking Sample Time Datum
			{
				smpltime_datum = GetDatum(M_SATM, K_SET);

				if (smpltime_datum != 0)
				{
					sampletime = DECDatVal(smpltime_datum,0);				
				}

				if (smpltime_datum != 0)
				{
					// the instrument only sweeps 500 points per time period
					// while user selected a time period for 8000-point sweep
					sweeptime = sampletime / 16;
				}
				else // Checking Rise Time Datum
				{
					risetime_datum = GetDatum(M_RISE, K_SRX);   // get risetime max

					if (risetime_datum != 0)
					{
						risetime = DECDatVal(risetime_datum, 0);
					}
					else // No risetime MAX
					{
						risetime_datum = GetDatum(M_RISE, K_SET); // risetime value is next

						if (risetime_datum != 0)
						{
							risetime = DECDatVal(risetime_datum, 0);
						}
						else // No risetime
						{
							risetime_datum = GetDatum(M_RISE, K_SRN); // risetime MIN is last

							if (risetime_datum != 0)
							{
								risetime = DECDatVal(risetime_datum, 0);
							}
						}
					}

					if (risetime_datum != 0)
					{
						sweeptime = risetime * PULSES;
					}
					else // Checking Fall Time Datum
					{
						falltime_datum = GetDatum(M_FALL, K_SRX);   // get falltime max

						if (falltime_datum != 0)
						{
							falltime = DECDatVal(falltime_datum, 0);
						}
						else // No falltime MAX
						{
							falltime_datum = GetDatum(M_FALL, K_SET); // falltime value is next

							if (falltime_datum != 0)
							{
								falltime = DECDatVal(falltime_datum, 0);
							}
							else // No falltime
							{
								falltime_datum = GetDatum(M_FALL, K_SRN); // falltime MIN is last

								if (falltime_datum != 0)
								{
									falltime = DECDatVal(falltime_datum, 0);
								}
							}
						}

						if (falltime_datum != 0)
						{
							sweeptime = falltime * PULSES;
						}
						else // Checking Pulse Width Datum
						{
							pulsewidth_datum = GetDatum(M_PLWD, K_SRX);   // get pulsewidth max

							if (pulsewidth_datum != 0)
							{
								pulsewidth = DECDatVal(pulsewidth_datum, 0);
							}
							else // No pulsewidth MAX
							{
								pulsewidth_datum = GetDatum(M_PLWD, K_SET); // pulsewidth value is next
	
								if (pulsewidth_datum != 0)
								{
									pulsewidth = DECDatVal(pulsewidth_datum, 0);
								}
								else // No pulsewidth
								{
									pulsewidth_datum = GetDatum(M_PLWD, K_SRN); // pulsewidth MIN is last
		
									if (pulsewidth_datum != 0)
									{
										pulsewidth = DECDatVal(pulsewidth_datum, 0);
									}
								}
							}

							if (pulsewidth_datum != 0)
							{
								sweeptime = pulsewidth * PULSES; // was multiplied by 3 - JLW
							}
							else // Checking Negative Pulse Width Datum
							{
								pulsewidth_datum = GetDatum(M_NPWT,K_SRX); // get pulsewidth max

								if (pulsewidth_datum != 0)
								{
									pulsewidth = DECDatVal(pulsewidth_datum, 0);
								}
								else // No pulsewidth MAX
								{
									pulsewidth_datum = GetDatum(M_NPWT, K_SET); // pulsewidth value is next
	
									if (pulsewidth_datum != 0)
									{
										pulsewidth = DECDatVal(pulsewidth_datum, 0);
									}
									else // No pulsewidth
									{
										pulsewidth_datum = GetDatum(M_NPWT, K_SRN); // pulsewidth MIN is last
		
										if (pulsewidth_datum != 0)
										{
											pulsewidth = DECDatVal(pulsewidth_datum, 0);
										}
									}
								}

								if (pulsewidth_datum != 0)
								{
									sweeptime = pulsewidth * PULSES; // was multiplied by 3 - JLW
								}
								else
								{ // Checking Positive Pulse Width Datum
							
									pulsewidth_datum = GetDatum(M_PPWT,K_SRX); // get pulsewidth max

									if (pulsewidth_datum != 0)
									{
										pulsewidth = DECDatVal(pulsewidth_datum, 0);
									}
									else // No pulsewidth MAX
									{
										pulsewidth_datum = GetDatum(M_PPWT, K_SET); // pulsewidth value is next
	
										if (pulsewidth_datum != 0)
										{
											pulsewidth = DECDatVal(pulsewidth_datum, 0);
										}
										else // No pulsewidth
										{
											pulsewidth_datum = GetDatum(M_PPWT, K_SRN); // pulsewidth MIN is last
		
											if (pulsewidth_datum != 0)
											{
												pulsewidth = DECDatVal(pulsewidth_datum, 0);
											}
										}
									}

									if (pulsewidth_datum != 0)
									{
										sweeptime = pulsewidth * PULSES; // was multiplied by 3 - JLW
									}
									else
									{
										sweeptime = .001;	// Default

										if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
										{
											sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - set_scope_sweeptime() - sweeptime set to default value of %d\033[m\n", sweeptime);
											Display(hp1428_msg_buf);
											//MessageBox(0, "Scope WCEM", "set_scope_sweeptime" , MB_OK);
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	if(sweeptime > 50)
	{
		/* Display message indicating that value has been clipped */
		Display("\033[33;40m *** Sweeptime exceeded 50 hz.  Will be set to 50 hz. ***\033[m\n");
		sweeptime = 50;
	}

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		sprintf_s(msg_buf,sizeof(msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - set_scope_sweeptime() - Sweeptime = %.7f\n",sweeptime);
		Display(msg_buf);
	}

	formatAndWrite(CHECK, "TIM:RANG %.8f",sweeptime);  // horizontal fullscale setting
}

void set_filter()
{
	DATUM	*freqwin_datum;
	double		freqwin_min, freqwin_max;

	freqwin_datum = GetDatum(M_FRQW, K_SRN);

	if(freqwin_datum !=0)
	{
		freqwin_min = DECDatVal(freqwin_datum,0);

		if(freqwin_min > 450)
		{
			formatAndWrite(CHECK, "CHAN%d:LFR ON",chan);  /* Lo freq rejection */
		}
		else
		{
			formatAndWrite(CHECK, "CHAN%d:LFR OFF",chan);  // default
		}

		freqwin_datum = GetDatum(M_FRQW, K_SRX);
		freqwin_max = DECDatVal(freqwin_datum,0);

		if(freqwin_max < 30000000)
		{
			formatAndWrite(CHECK, "CHAN%d:HFR ON",chan);  /* Hi freq rejection */
		}
		else
		{
			formatAndWrite(CHECK, "CHAN%d:HRF OFF", chan);  // default
		}
	}

	return;
}

void set_trigger(double level, char* slope)
{
	formatAndWrite(CHECK, "TRIG:LEV %f", level);
	formatAndWrite(CHECK, "TRIG:SLOP %s", slope);
	
	return;
}

void set_autoscale()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		sprintf_s(msg_buf,sizeof(msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - set_autoscale()\n");
		Display(msg_buf);
	}
	formatAndWrite(NOCHECK, AUTOSCALE);
}

/*********************************************************************************/
/*  					Setup The Oscilloscope		 							 */
/*********************************************************************************/
void scope_setup()

{
	ViUInt32	TmoVal;
	double		triglev;
	char		*slope;
				
	sprintf_s(instrument,sizeof(instrument),"hpe1428a");

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		//MessageBox(0, "Scope WCEM", "scope_setup", MB_OK);
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - scope_setup()\033[m\n");
		err = atxmlDF_viGetAttribute("DSO_1", VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE, &TmoVal);
		sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - scope_setup() - TMO=%d, handle=%d\033[m\n",TmoVal,VI_SESSION_NOT_USED);
		Display(hp1428_msg_buf);
	}
	
	scope_reset_flag = 0;

	formatAndWrite(CHECK, "VIEW CHAN%d", chan);
	
	set_coupling();
	set_impedance();
	set_filter();
	set_probe_atten();
	set_dc_offset();
	set_time_ref(LEFT);
	set_sampling_mode(REAL);
	set_acquisition_type(NORMAL);

	if (autoscl == 0)
	{
		set_scope_sweeptime();
		set_scope_ampl();
	}

	if (set_lev == 1)
	{
		set_time_mode(TRIGGER);
		set_trigger(event_triglev, event_slope);
		set_lev = 0;
	}
	else
	{
		set_time_mode(AUTO);
	}

    if (av_meas_flag == 0)
    {
	   smplcnt_datum = GetDatum(M_SCNT, K_SET);   /* get Sample-Count number */

	   if (smplcnt_datum != 0)
       {
		    sample_cnt = DECDatVal(smplcnt_datum,0);
            set_sampling_mode(REPEAT);   /* Set Repetitive Sampling */
            set_acquisition_type(AVERAGE);   /* Set Averaging Mode */
			set_acquisition_count(sample_cnt);  /* Set Average Count */
       
            /****Sets up the triger on the scope for proper sample count technique****/

			set_time_mode(TRIGGER);
			formatAndWrite(CHECK, "%s","TRIG:MODE EDGE");
			formatAndWrite(CHECK, "TRIG:SOUR CHAN%d",chan);    /* Set trig on current chan used */
			trglvl_datum = GetDatum(M_TRLV,K_SET);

			if (trglvl_datum != 0)
			{
				triglev = DECDatVal(trglvl_datum, 0);
			}
			else
			{
				triglev = 0;
			}

			slope_datum = GetDatum(M_TRSL,K_SET);

			if (slope_datum != 0)
			{
				slope = GetTXTDatVal(slope_datum, 0);
			}
			else
			{
				slope = "POS";
			}
  
			set_trigger(triglev, slope);
       }
    }
}

/*********************************************************************************/
void setup_internal_trig()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		//MessageBox(0, "Scope WCEM", "setup_internal_trig" , MB_OK);
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_int_trig()\033[m\n");
	}

	trglvl_datum = GetDatum(M_VOLT, K_SET);

	if (trglvl_datum != 0)
	{
		event_triglev = DECDatVal(trglvl_datum, 0);
	}
	else
	{
		event_triglev = 0;
	}

	slope_datum = GetDatum(M_EVSL,K_SET);

	if (slope_datum != 0)
	{
		event_slope = GetTXTDatVal(slope_datum, 0);
	}
	else
	{
		event_slope = "POS";
	}

	set_lev = 1;
}
/*********************************************************************************/
/*  					Frequency Measurement		 							 */
/*********************************************************************************/
void mea_frequency()
{	
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_frequency()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_frequency" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan);  /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_FREQ);              /* Frequency measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_frequency1()
{	
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_frequency1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_frequency1" , MB_OK);
	}

	chan = 1;
	mea_frequency();
}

/*********************************************************************************/
void setup_frequency2()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_frequency2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_frequency2" , MB_OK);
	}

	chan = 2;
	mea_frequency();
}

/*********************************************************************************/
/*  					Period Measurement			 							 */
/*********************************************************************************/
void mea_period()
{
	DATUM		*period_datum;
	double		period = 0.0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_period()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_period" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_PERIOD);              /* Period measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_period1()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_period1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_period1" , MB_OK);
	}

	chan = 1;
	mea_period();
}

/*********************************************************************************/
void setup_period2()
{
	
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_period2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_period2" , MB_OK);
	}

	chan = 2;
	mea_period();
}

/*********************************************************************************/
/*  					Voltage-PP Measurement									 */
/*********************************************************************************/
void mea_voltage_pp()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_voltage_pp()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_voltage_pp" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_VOLTS_PP);              /* Voltage Peak-to-Peak measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_voltage_pp1()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_pp1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_pp1" , MB_OK);
	}

	chan = 1;
	mea_voltage_pp();
}

/*********************************************************************************/
void setup_voltage_pp2()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_pp2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_pp2" , MB_OK);
	}

	chan = 2;
	mea_voltage_pp();
}

/*********************************************************************************/
/*  					Voltage-P Positive Measurement							 */
/*********************************************************************************/
void mea_voltage_p_pos()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_voltage_p_pos()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_voltage_p_pos" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_VOLTS_MAX);              /* Voltage Max measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_voltage_p_pos1()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_p_pos1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_p_pos1" , MB_OK);
	}

	chan = 1;
	mea_voltage_p_pos();
}

/*********************************************************************************/
void setup_voltage_p_pos2()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_p_pos2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_p_pos2" , MB_OK);
	}

	chan = 2;
	mea_voltage_p_pos();
}

/*********************************************************************************/
/*  					Voltage-P Measurement									 */
/*********************************************************************************/
void mea_voltage_p()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_voltage_p()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_voltage_p" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_VOLTS_AMP);              /* Amplitude Voltage measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_voltage_p1()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_p1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_p1" , MB_OK);
	}
	
	chan = 1;
	mea_voltage_p();
}

/*********************************************************************************/
void setup_voltage_p2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_p2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_p2" , MB_OK);
	}

	chan = 2;
	mea_voltage_p();
}

/*********************************************************************************/
/*  					Voltage-P Negative Measurement							 */
/*********************************************************************************/
void mea_voltage_p_neg()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_voltage_p_neg()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_voltage_p_neg" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_VOLTS_MIN);              /* Voltage Min measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_voltage_p_neg1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_p_neg1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_p_neg1" , MB_OK);
	}

	chan = 1;
	mea_voltage_p_neg();
}

/*********************************************************************************/
void setup_voltage_p_neg2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_p_neg2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_p_neg2" , MB_OK);
	}

	chan = 2;
	mea_voltage_p_neg();
  
}

/*********************************************************************************/
/*  					Voltage DC Measurement									 */
/*********************************************************************************/
void mea_voltage_dc()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_voltage_dc()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_voltage_dc" , MB_OK);
	}

	autoscl = 0;
	set_scope_sweeptime();
	set_coupling();
	set_scope_ampl();
	set_dc_offset();

	if (amplitude < 0)
	{
		amplitude = -amplitude;
	}

	scope_reset_flag = 0;
	formatAndWrite(CHECK, "VIEW CHAN%d", chan);
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_VOLTS_AVG);       /* DC Voltage measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_voltage_dc1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_dc1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_dc1" , MB_OK);
	}

	chan = 1;
	mea_voltage_dc();
}

/*********************************************************************************/
void setup_voltage_dc2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_dc2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_dc2" , MB_OK);
	}

	chan = 2;
	mea_voltage_dc();
}

/*********************************************************************************/
/*  					Voltage Average Measurement								 */
/*********************************************************************************/
void mea_voltage_av()
{

	sample_cnt = 8.0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_voltage_av()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_voltage_av" , MB_OK);
	}

	autoscl = 0;
	scope_reset_flag = 0;
	smplcnt_datum = GetDatum(M_SCNT, K_SET);   /* get Sample-Count number */

	if (smplcnt_datum != 0)
	{
		sample_cnt = DECDatVal(smplcnt_datum, 0);
	}

    av_meas_flag = 1;
	scope_setup();
    av_meas_flag = 0;
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_VOLTS_AVG);              /* Voltage Average measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_voltage_av1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_av1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_av1" , MB_OK);
	}

	chan = 1;
	mea_voltage_av();
}

/*********************************************************************************/
void setup_voltage_av2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_voltage_av2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_voltage_av2" , MB_OK);
	}

	chan = 2;
	mea_voltage_av();
}

/*********************************************************************************/
/*  					Risetime Measurement									 */
/*********************************************************************************/
void mea_risetime()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_risetime()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_risetime" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_RISETIME);              /* Risetime measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_risetime1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_risetime1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_risetime1" , MB_OK);
	}

	chan =1;
	mea_risetime();
}

/*********************************************************************************/
void setup_risetime2()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_risetime2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_risetime2" , MB_OK);
	}

	chan =2;
	mea_risetime();
}

/*********************************************************************************/
/*  					Preshoot Measurement									 */
/*********************************************************************************/
void mea_preshoot()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_preshoot()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_preshoot" , MB_OK);
	}

	autoscl = 0;
	scope_setup();  
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_PRESHOOT);              /* Preshoot measurement */
	Mea_Buf = &fstring[0];
}
/*********************************************************************************/
void setup_preshoot1()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_preshoot1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_preshoot1" , MB_OK);
	}

	chan =1;
	mea_preshoot();
}

/*********************************************************************************/
void setup_preshoot2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_preshoot2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_preshoot2" , MB_OK);
	}

	chan =2;
	mea_preshoot();
}

/*********************************************************************************/
/*  					Overshoot Measurement									 */
/*********************************************************************************/
void mea_overshoot()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_overshoot()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_overshoot" , MB_OK);
	}

	autoscl = 0;
	scope_setup();  
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_OVERSHOOT);              /* Overshoot measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_overshoot1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_overshoot1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_overshoot1" , MB_OK);
	}

	chan =1;
	mea_overshoot();
}

/*********************************************************************************/
void setup_overshoot2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_overshoot2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_overshoot2" , MB_OK);
	}

	chan =2;
	mea_overshoot();
}

/*********************************************************************************/
/*  					Falltime Measurement									 */
/*********************************************************************************/
void mea_falltime()
{

	//MessageBox(0, "Scope WCEM", "mea_falltime" , MB_OK);

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_falltime()\033[m\n");
	}

	autoscl = 0;
	scope_setup();
	formatAndWrite(CHECK, "%s","TRIG:SLOP NEG");
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_FALLTIME);              /* Falltime measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_falltime1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_falltime1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_falltime1" , MB_OK);
	}

	chan = 1;
	mea_falltime();
}

/*********************************************************************************/
void setup_falltime2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_falltime2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_falltime2" , MB_OK);
	}

	chan = 2;
	mea_falltime();
}

/*********************************************************************************/
/*  					Pulsewidth Measurement									 */
/*********************************************************************************/
void mea_pulsewidth()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_pulsewidth()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_pulsewidth" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_PULSEWDTH_POS);              /* Pulse Width measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
/*  					Neg Pulsewidth Measurement								 */
/*********************************************************************************/
void mea_neg_pulsewidth()
{

	DATUM		*pulsewidth_datum;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_pulsewidth()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_neg_pulsewidth" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_PULSEWDTH_NEG);              /* Pulse Width measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
/*  					Pos Pulsewidth Measurement								 */
/*********************************************************************************/
void mea_pos_pulsewidth()
{

	DATUM		*pulsewidth_datum;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_pulsewidth()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_pos_pulsewidth" , MB_OK);
	}

	autoscl = 0;
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_PULSEWDTH_POS);              /* Pulse Width measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_pulsewidth1()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_pulsewidth1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_pulsewidth1" , MB_OK);
	}

	chan = 1;
	mea_pulsewidth();
}

/*********************************************************************************/
void setup_pulsewidth2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_pulsewidth2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_pulsewidth2" , MB_OK);
	}

	chan = 2;
	mea_pulsewidth();;
}

/*********************************************************************************/
void setup_pos_pulsewidth1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_pos_pulsewidth1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_pos_pulsewidth1" , MB_OK);
	}

	chan = 1;
	mea_pos_pulsewidth();
}

/*********************************************************************************/
void setup_pos_pulsewidth2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_pos_pulsewidth2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_pos_pulsewidth2" , MB_OK);
	}

	chan = 2;
	mea_pos_pulsewidth();
}

/*********************************************************************************/
void setup_neg_pulsewidth1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_neg_pulsewidth1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_neg_pulsewidth1" , MB_OK);
	}

	chan = 1;
	mea_neg_pulsewidth();
}

/*********************************************************************************/
void setup_neg_pulsewidth2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_neg_pulsewidth2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_neg_pulsewidth2" , MB_OK);
	}

	chan = 2;
	mea_neg_pulsewidth();
}

/*********************************************************************************/
/*  					Duty Cycle Measurement									 */
/*********************************************************************************/
void mea_dutycycle()
{

	DATUM		*prf_datum;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - mea_dutycycle()\033[m\n");
		//MessageBox(0, "Scope WCEM", "mea_dutycycle" , MB_OK);
	}

	autoscl = 0;  
	scope_setup();
	clear_measure_que();
	formatAndWrite(CHECK, "MEAS:SOUR CHAN%d", chan); /* Select source for measurement */
	sprintf_s(fstring,80,MEAS_DUTYCYCLE);              /* Duty Cycle measurement */
	Mea_Buf = &fstring[0];
}

/*********************************************************************************/
void setup_dutycycle1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_dutycycle1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_dutycycle1" , MB_OK);
	}

	chan =1;
	mea_dutycycle();
}

/*********************************************************************************/
void setup_dutycycle2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_dutycycle2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_dutycycle2" , MB_OK);
	}

	chan = 2;
	mea_dutycycle();
}

/*********************************************************************************/
/*  						Get Event Data										 */
/*********************************************************************************/
void get_eventdata()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - get_eventdata()\033[m\n");
		//MessageBox(0, "Scope WCEM", "get_eventdata" , MB_OK);
	}

	// What is this for???????
	formatAndWrite(CHECK, "%s","MEAS:MODE USER");
	////formatAndWrite(CHECK, "%s","MEAS:UNIT VOLT");
	trglvl_datum = GetDatum(M_VOLT, K_SET);

	if (trglvl_datum != 0)
	{
		event_triglev = DECDatVal(trglvl_datum, 0);
	}
	else
	{
		event_triglev = 0;
	}

	slope_datum = GetDatum(M_EVSL,K_SET);

	if (slope_datum != 0)
	{
		event_slope = GetTXTDatVal(slope_datum, 0);
	}
	else
	{
		event_slope = "POS";
	}
}

/*********************************************************************************/
void setup_event_from1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_event_from1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_event_from1" , MB_OK);
	}

	autoscl = 1;
	chan = 1;
	scope_setup();
	get_eventdata();
	formatAndWrite(CHECK, "MEAS:UPP %f",event_triglev);
	sprintf_s(fromdata,20,"%s,1,UPP",event_slope);
}

/*********************************************************************************/
void setup_event_to1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_event_to1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_event_to1" , MB_OK);
	}

	autoscl = 1;
	chan = 1;
	scope_setup();
	get_eventdata();
	formatAndWrite(CHECK, "MEAS:LOW %f",event_triglev);
	sprintf_s(todata,20,"%s,1,LOW",event_slope);
}

/*********************************************************************************/
void setup_event_from2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_event_from2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_event_from2" , MB_OK);
	}

	autoscl = 1;
	chan = 2;
	scope_setup();
	set_scope_ampl();
	get_eventdata();
	formatAndWrite(CHECK, "MEAS:UPP %f",event_triglev);
	sprintf_s(fromdata,20,"%s,1,UPP",event_slope);
}

/*********************************************************************************/
void setup_event_to2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_event_to2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_event_to2" , MB_OK);
	}

	autoscl = 1;
	chan = 2;
	scope_setup();
	set_scope_ampl();
	get_eventdata();
	formatAndWrite(CHECK, "MEAS:LOW %f",event_triglev);
	sprintf_s(todata,20,"%s,1,LOW",event_slope);
}

/*********************************************************************************/
void setup_delay_ab()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_delay_ab()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_delay_ab" , MB_OK);
	}

	scope_reset_flag = 0;
	set_scope_sweeptime();
	sprintf_s(measource,20,"CHAN1,CHAN2");
}

/*********************************************************************************/
void setup_delay_aa()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_delay_aa()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_delay_aa" , MB_OK);
	}

	scope_reset_flag = 0;
	set_scope_sweeptime();
	sprintf_s(measource,20,"CHAN1,CHAN1");
}

/*********************************************************************************/
void setup_delay_ba()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_delay_ba()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_delay_ba" , MB_OK);
	}

	scope_reset_flag = 0;
	set_scope_sweeptime();
	sprintf_s(measource,20,"CHAN2,CHAN1");
}

/*********************************************************************************/
void setup_delay_bb()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_delay_bb()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_delay_bb" , MB_OK);
	}

	scope_reset_flag = 0;
	set_scope_sweeptime();
	sprintf_s(measource,20,"CHAN2,CHAN2");
}

/*********************************************************************************/
/*  				Setup Waveform Measurement for channel 1					 */
/*********************************************************************************/
void setup_wave1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_wave1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "DEBUG:  setup_wave1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;

    if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_wave1() - calling scope_setup()\033[m\n");
	}

	scope_setup();
	Sleep(2000);
	CheckHPe1428ABErrors();
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","ACQ:POIN 8000");
}

/*********************************************************************************/
/*  				Setup Waveform Measurement for Channel 2					 */
/*********************************************************************************/
void setup_wave2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_wave2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_wave2" , MB_OK);
	}

	chan = 2;
	autoscl = 0;

    if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_wave2() - calling scope_setup()\033[m\n");
	}

	scope_setup();
	Sleep(2000);
	CheckHPe1428ABErrors();
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","SENS:SWEEP:POIN 8000");
}

/*********************************************************************************/
/*  					Save Waveform into memory								 */
/*********************************************************************************/
void save_waveform()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - save_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "save_waveform" , MB_OK);
	}

	scope_reset_flag = 0;
	Sleep(2000);
	sweeptime = .001;	/* Default */
	src_datum = GetDatum(M_SVFM,K_SET);

	if (src_datum != 0)
	{
		saveSrc = GetTXTDatVal(src_datum,0);
		/* Digitize the waveform only if it's an active channel(1,2)*/
		string1 = strcmp(saveSrc,"CHAN1");
		string2 = strcmp(saveSrc,"CHAN2");

		if (string1==0||string2==0)
		{
			formatAndWrite(CHECK, "DIG %s", saveSrc);
			formatAndWrite(CHECK, "DIG %s", saveSrc); /* Digitize the Channel  */
			set_autoscale();

			Sleep(2000);
			CheckHPe1428ABErrors();
		}
	}

	else
	{
		saveSrc = "CHAN1";  /* default source channel 1 */
		formatAndWrite(CHECK, "DIG %s", saveSrc); /* Digitize the Channel  */
		set_autoscale();

		CheckHPe1428ABErrors();
	}

	formatAndWrite(CHECK, "VIEW %s",saveSrc);
	dest_datum = GetDatum(M_SVTO,K_SET);

	if (dest_datum != 0)
	{
		dest = GetTXTDatVal(dest_datum, 0);
	}
	else
	{
		dest = "WMEM1"; /* default destination memory 1 */
	}

	formatAndWrite(CHECK, "STOR %s,%s",saveSrc,dest);
	Sleep(5000); /* Give the scope long enough time to save the waveform */
	formatAndWrite(CHECK, "BLAN %s",saveSrc);
}

/*********************************************************************************/
void setup_save_wave1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_save_wave1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_save_wave1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	save_waveform();
}

/*********************************************************************************/
void setup_save_wave2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_save_wave2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_save_wave2" , MB_OK);
	}

	chan = 2;
	autoscl = 0;
	save_waveform();
}

/*********************************************************************************/
/*  					Load Waveform from memory								 */
/*********************************************************************************/
void load_waveform()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - load_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "load_waveform" , MB_OK);
	}

	scope_reset_flag = 0;
	Sleep(2000);
	loadSrc_datum = GetDatum(M_LDFM,K_SET);

	if (loadSrc_datum != 0)
	{
		loadSrc = GetTXTDatVal(loadSrc_datum, 0);
	}

	else
	{
		loadSrc = "WMEM1";
	}

	formatAndWrite(CHECK, "VIEW %s",loadSrc);
	Sleep(5000);
	test_ch = loadSrc;
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","ACQ:POIN 500");
}

/*********************************************************************************/
void setup_load_wave1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_load_wave1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_load_wave1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	load_waveform();
}

/*********************************************************************************/
void setup_load_wave2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_load_wave2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_load_wave2" , MB_OK);
	}

	chan = 2;
	autoscl = 0;
	load_waveform();
}

/*********************************************************************************/
/*  					Compare Two Waveforms									 */
/*********************************************************************************/
void compare_waveform()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - compare_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "compare_waveform" , MB_OK);
	}

	scope_reset_flag = 0;
	Sleep(2000);
	cmpSrc_datum = GetDatum(M_CMCH,K_SET);

	if (cmpSrc_datum != 0)
	{
		source1 = GetTXTDatVal(cmpSrc_datum, 0);
	}
	else
	{
		source1 = "CHAN1"; /* Default Channel */
	}

	formatAndWrite(CHECK, "DIG %s", source1); /* Digitize the Channel */
	set_autoscale();
	Sleep(2000);
	CheckHPe1428ABErrors();
	cmpDst_datum = GetDatum(M_CMTO,K_SET);

	if (cmpDst_datum != 0)
	{
		source2 = GetTXTDatVal(cmpDst_datum, 0);
	}
	else
	{
		source2 = "WMEM1"; /* Default memory location */
	}

	allow_datum = GetDatum(M_ALLW,K_SET);

	if (allow_datum != 0)
	{
		allow = DECDatVal(allow_datum, 0);
	}
	else
	{
		allow = 1.0;
	}

	diff = "FUNC1";
	formatAndWrite(CHECK, "%s:SUBT %s,%s",diff,source1,source2);
	Sleep(5000); /* Give the scope time to do the math operation. */
	formatAndWrite(CHECK, "VIEW %s",diff);
	test_ch = diff;
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","ACQ:POIN 500");
}

/*********************************************************************************/
void setup_comp_wave1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_comp_wave1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_comp_wave1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	compare_waveform();
}

/*********************************************************************************/
void setup_comp_wave2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_comp_wave2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_comp_wave2" , MB_OK);
	}

	chan = 2;
	autoscl = 0;
	compare_waveform();
}

/*********************************************************************************/
/*  					Add Two Waveforms (Math)								 */
/*********************************************************************************/
void add_waveform()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - add_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "add_waveform" , MB_OK);
	}

	scope_reset_flag = 0;
	source1_datum = GetDatum(M_ADFM,K_SET);

	if (source1_datum != 0)
	{
		source1 = GetTXTDatVal(source1_datum,0);
		string1 = strcmp(source1,"CHAN1");
		string2 = strcmp(source1,"CHAN2");

		if (string1==0||string2==0)
		{
			formatAndWrite(CHECK, "DIG %s", source1); /* Digitize the Channel  */
			set_autoscale();
			Sleep(2000);
			CheckHPe1428ABErrors();
		}
	}

	else
	{
		source1 = "WMEM1";
	}
	
	source2_datum = GetDatum(M_ADTO,K_SET);

	if (source2_datum != 0)
	{
		source2 = GetTXTDatVal(source2_datum, 0);
	}
	else
	{
		source2 = "CHAN1";
	}

	string1 = strcmp(source2,"CHAN1");
	string2 = strcmp(source2,"CHAN2");

	if (string1==0||string2==0)
	{
		formatAndWrite(CHECK, "DIG %s", source2); /* Digitize the Channel  */
		set_autoscale();
		Sleep(2000);
		CheckHPe1428ABErrors();
	}

	Sleep(5000);
	result_datum = GetDatum(M_DEST,K_SET);

	if (result_datum != 0)
	{
		sum = GetTXTDatVal(result_datum, 0);
	}
	else
	{
		sum = "FUNC1"; /* Default location to put the summation result in */
	}

	formatAndWrite(CHECK, "%s:ADD %s,%s",sum,source1,source2);
	Sleep(7000); /* Give the scope time to do the math operation. */
	formatAndWrite(CHECK, "VIEW %s",sum);
	test_ch = sum;
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","ACQ:POIN 500");
}

/*********************************************************************************/
void setup_add1()
{
	
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_add1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_add1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	add_waveform();
}

/*********************************************************************************/
void setup_add2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_add2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_add2" , MB_OK);
	}

	chan = 2;
	autoscl = 0;
	add_waveform();
}

/*********************************************************************************/
/*  					Subtract Two Waveforms (Math)							 */
/*********************************************************************************/
void subtract_waveform()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - subtract_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "subtract_waveform" , MB_OK);
	}

	scope_reset_flag = 0;
	source1_datum = GetDatum(M_SBFM,K_SET);

	if (source1_datum != 0)
	{
		source1 = GetTXTDatVal(source1_datum,0);
		string1 = strcmp(source1,"CHAN1");
		string2 = strcmp(source1,"CHAN2");

		if (string1==0||string2==0)
		{
			formatAndWrite(CHECK, "DIG %s", source1); /* Digitize the Channel */
			set_autoscale();
			Sleep(2000);
			CheckHPe1428ABErrors();
		}
	}
	else
	{
		source1 = "WMEM1";
	}
	
	source2_datum = GetDatum(M_SBTO,K_SET);

	if (source2_datum != 0)
	{
		source2 = GetTXTDatVal(source2_datum, 0);
	}
	else
	{
		source2 = "CHAN1";
	}

	Sleep(5000);
	string1 = strcmp(source2,"CHAN1");
	string2 = strcmp(source2,"CHAN2");

	if (string1==0||string2==0)
	{
		formatAndWrite(CHECK, "DIG %s", source2); /* Digitize the Channel  */
		set_autoscale();
		Sleep(2000);
		CheckHPe1428ABErrors();
	}

	result_datum = GetDatum(M_DEST,K_SET);

	if (result_datum != 0)
	{
		diff = GetTXTDatVal(result_datum, 0);
	}
	else
	{
		diff = "FUNC1";
	}

	formatAndWrite(CHECK, "%s:SUBT %s,%s",diff,source1,source2);
	Sleep(7000); /* Give the scope time to do the math operation. */
	formatAndWrite(CHECK, "VIEW %s",diff);
	test_ch = diff;
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","ACQ:POIN 500");
}

/*********************************************************************************/
void setup_subtract1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_subtract1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_subtract1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	subtract_waveform();
}

/*********************************************************************************/
void setup_subtract2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_subtract2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_subtract2" , MB_OK);
	}

	chan = 2;
	autoscl = 0;
	subtract_waveform();
}

/*********************************************************************************/
/*  					Multiply Two Waveforms (Math)							 */
/*********************************************************************************/
void multiply_waveform()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - multiply_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "multiply_waveform" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	scope_reset_flag = 0;
	source1_datum = GetDatum(M_MPFM,K_SET);

	if (source1_datum != 0)
	{
		source1 = GetTXTDatVal(source1_datum,0);
		string1 = strcmp(source1,"CHAN1");
		string2 = strcmp(source1,"CHAN2");

		if (string1==0||string2==0)
		{
			formatAndWrite(CHECK, "DIG %s", source1); /* Digitize the Channel  */
			formatAndWrite(NOCHECK, "%s", "AUT");
			Sleep(2000);
			CheckHPe1428ABErrors();
		}
	}
	else
	{
		source1 = "WMEM1";
	}

	source2_datum = GetDatum(M_MPTO,K_SET);

	if (source2_datum != 0)
	{
		source2 = GetTXTDatVal(source2_datum, 0);
	}
	else
	{
		source2 = "CHAN1";
	}

	string1 = strcmp(source2,"CHAN1");
	string2 = strcmp(source2,"CHAN2");

	if (string1==0||string2==0)
	{
		formatAndWrite(CHECK, "DIG %s", source2); /* Digitize the Channel  */
		set_autoscale();
		Sleep(2000);
		CheckHPe1428ABErrors();
	}

	result_datum = GetDatum(M_DEST,K_SET);

	if (result_datum != 0)
	{
		product = GetTXTDatVal(result_datum, 0);
	}
	else
	{
		product = "FUNC1";
	}

	formatAndWrite(CHECK, "%s:MULT %s,%s",product,source1,source2);
	Sleep(5000); /* Give the scope time to do the math operation. */
	formatAndWrite(CHECK, "VIEW %s",product);
	test_ch = product;
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","ACQ:POIN 500");
}

/*********************************************************************************/
void setup_multiply1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_multiply1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_multiply1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	multiply_waveform();
}

/*********************************************************************************/
void setup_multiply2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_multiply2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_multiply2" , MB_OK);	
	}

	chan = 2;
	autoscl = 0;
	multiply_waveform();
}

/*********************************************************************************/
/*  					Integrate a Waveform (Math)								 */
/*********************************************************************************/
void integrate_waveform()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - integrate_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "integrate_waveform" , MB_OK);
	}

	scope_reset_flag = 0;
	source1_datum = GetDatum(M_INTG,K_SET);

	if (source1_datum != 0)
	{
		source1 = GetTXTDatVal(source1_datum, 0);
	}
	else
	{
		source1 = "WMEM1";
	}

	string1 = strcmp(source1,"CHAN1");
	string2 = strcmp(source1,"CHAN2");

	if (string1==0||string2==0)
	{
		formatAndWrite(CHECK, "DIG %s", source1); /* Digitize the Channel  */
		set_autoscale();
		Sleep(2000);
		CheckHPe1428ABErrors();
	}
	
	result_datum = GetDatum(M_DEST,K_SET);

	if (result_datum != 0)
	{
		intResult = GetTXTDatVal(result_datum, 0);
	}
	else
	{
		intResult = "FUNC1";
	}

	formatAndWrite(CHECK, "%s:INT %s",intResult, source1);
	Sleep(5000); /* Give the scope time to do the math operation. */
	formatAndWrite(CHECK, "VIEW %s",intResult);
	test_ch = intResult;
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","ACQ:POIN 500");
}

/*********************************************************************************/
void setup_integrate1()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_integrate1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_integrate1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	integrate_waveform();
}

/*********************************************************************************/
void setup_integrate2()
{
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_integrate2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_integrate2" , MB_OK);
	}

	chan = 2;
	autoscl = 0;
	integrate_waveform();

}

/*********************************************************************************/
/*  					Differentiate a Waveform (Math)							 */
/*********************************************************************************/
void differentiate_waveform()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - differentiate_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "differentiate_waveform" , MB_OK);
	}

	scope_reset_flag = 0;
	source1_datum = GetDatum(M_DIFR,K_SET);

	if (source1_datum != 0)
	{
		source1 = GetTXTDatVal(source1_datum, 0);
	}
	else
	{
		source1 = "WMEM1";
	}
	
	string1 = strcmp(source1,"CHAN1");
	string2 = strcmp(source1,"CHAN2");

	if (string1==0||string2==0)
	{
		formatAndWrite(CHECK, "DIG %s", source1); /* Digitize the Channel  */
		set_autoscale();
		Sleep(2000);
		CheckHPe1428ABErrors();
	}

	result_datum = GetDatum(M_DEST,K_SET);

	if (result_datum != 0)
	{
		dfrResult = GetTXTDatVal(result_datum, 0);
	}
	else
	{
		dfrResult = "FUNC1";
	}

	formatAndWrite(CHECK, "%s:DIFF %s",dfrResult, source1);
	Sleep(5000); /* Give the scope time to do the math operation. */
	formatAndWrite(CHECK, "VIEW %s",dfrResult);
	test_ch = dfrResult;
	formatAndWrite(CHECK, "%s","ACQ:COMP 100");
	formatAndWrite(CHECK, "%s","ACQ:POIN 500");
}

/*********************************************************************************/
void setup_differentiate1()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_differentiate1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_differentiate1" , MB_OK);
	}

	chan = 1;
	autoscl = 0;
	differentiate_waveform();
}

/*********************************************************************************/
void setup_differentiate2()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_differentiate2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_differentiate2" , MB_OK);
	}

	chan = 2;
	autoscl = 0;
	differentiate_waveform();
}

/*********************************************************************************/
void setup_ch1_trig()
{

	int trgsrc = 1;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_ch1_trig()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_ch1_trig" , MB_OK);
	}

	get_trigger(trgsrc);
	trg_flg = 1;
}

/*********************************************************************************/
void setup_ch2_trig()
{

	int trgsrc = 2;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_ch2_trig()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_ch2_trig" , MB_OK);
	}

	get_trigger(trgsrc);
	trg_flg = 1;
}

/*********************************************************************************/
void setup_ext_trig()
{
	int trgsrc = 3;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - setup_ext_trig()\033[m\n");
		//MessageBox(0, "Scope WCEM", "setup_ext_trig" , MB_OK);
	}

	get_trigger(trgsrc);
	trg_flg = 1;
}

/********************************************************************************/
/* 	 					Initialize the scope									*/
/********************************************************************************/
void init_scope()
{
	const int MAX_TRIES = 30;
	DATUM	*maxt_datum;
	double	maxt;
	ViChar complete[READBUFSIZE];
	int iCount = 0;
	int iWait = 0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope()\033[m\n");
		//MessageBox(0, "Scope WCEM", "init_scope" , MB_OK);
	}

	scope_reset_flag = 0;

    /* set VISA TMO_VALUE, if max-time is specified */
	maxt_datum = GetDatum(M_MAXT, K_SET);

	if (maxt_datum != 0)
	{
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - MAX-TIME Found\033[m\n");
			Display(hp1428_msg_buf);
		}
			
		maxt = DECDatVal(maxt_datum, 0);
		iWait = maxt;
		maxt *= 1000; /*convert to milliseconds */
		err = atxmlDF_viSetAttribute("DSO_1", VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE, (unsigned long)maxt);
		display_error();
			
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - VISA TMO = %f\033[m\n", maxt);
			Display(hp1428_msg_buf);
		}
	}

	if (probe)
	{
		if (ResetEvent(probeInfo.hMakeMeasurementHandle)) 
		{
			sysmon_err = WaitForProbeButt(maxtimeout);
		}
		else 
		{
			retrieveErrorMessage("init_scope()", "Recieved an error: ");
			sysmon_err = CEM_ERROR;
		}

		if (sysmon_err != 0)
		{
			switch(sysmon_err)
			{
				case -1:
					Display("HPE1428 Warning: ");
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[31;40m Probe button not pressed before Max-Time of %f sec.\033[m\n",maxtimeout);
					Display(hp1428_msg_buf);
					break;
				case 53:
					Display("\033[33;40m HPE1428 Warning: SYSMON.EXE not running\033[m\n");
					break;
				default:
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[33;40m HPE1428 Warning: Invalid probe return: %d\033[m\n",sysmon_err);
					Display(hp1428_msg_buf);
					break;
			}
			return;
		}
		else
		{
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - STOP \033[m\n");
			}
			abort_acquisition();
			
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - digitizing channel \033[m\n");
			}
			formatAndWrite(NOCHECK, "DIG CHAN%d", chan);
				
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - %s\n", hp1428_string);
				//Display(hp1428_msg_buf);
			}

			//added counter in loop below in the case that no stimulus is present at scope or probe button not working
			do
			{
				scanReturn = 0;
				memset(DigScopeRdBuf, '\0', READBUFSIZE);
				memset(complete, '\0', READBUFSIZE);

				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - checking for operation complete\033[m\n");
				}

				formatAndWrite(NOCHECK, "%s", "*OPC?\n");
				err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

				if (err != 0 && err != VI_ERROR_TMO)
				{
					display_error();
				}
				
				DigScopeRdBuf[BytesRead] = '\0';
				scanReturn = sscanf((char *)DigScopeRdBuf, "%s", complete);
				
				complete[BytesRead] = '\0';
				iCount++;

                if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - waiting for Scope to digitize\033[m\n");
				}

			} while (strcmp(complete, "+1") != 0 && iCount <= MAX_TRIES);
		}
	}
	else
	{

        if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - STOP \033[m\n");
		}
		abort_acquisition();
		
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - digitizing channel \033[m\n");
		}
		formatAndWrite(NOCHECK, "DIG CHAN%d", chan);
			
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() %s\n", hp1428_string);
			//Display(hp1428_msg_buf);
		}

		scanReturn = 0;
		memset(DigScopeRdBuf, '\0', READBUFSIZE);

		if(trg_flg == 1)
		{
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - triggered acquistion flag is set, 2 sec wait\033[m\n");
			}
			Sleep(2000);   // Allow scope to digest before trigger can happen
		}
	}

    /* If setup for external trigger and using single-action verbs, the remaining
       code in this function will cause the RTS to hang up during INIT before the 
       supposed next ATLAS statement that generates the actual external trigger can
       execute. Therefore, the RTS MONITOR display persistance fix below cannot
       be used in this case.
    */
	if (trg_flg == 1)
	{
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - triggered acquisition flag is set, returning\033[m\n");
		}

		return;
	}

    /*	These commands cause this function to wait until the measurement is complete 
		or the VISA timeout at read. This is necessary so the 
		persistance of the RTS-displayed value during a MONITOR statement is readable.
    	Otherwise, the time spent waiting for the Scope to complete measurement is 
		during the FETCH part, when the RTS has already blanked-out the measurement 
		display.
	*/
	
	scanReturn = 0;
	
	do
	{
		scanReturn = 0;
		memset(DigScopeRdBuf, '\0', READBUFSIZE);
		memset(complete, '\0', READBUFSIZE);

		/*if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - checking for operation complete\033[m\n");
		}*/

		formatAndWrite(NOCHECK, "%s", "*OPC?\n");
		err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

		if (err != 0 && err != VI_ERROR_TMO)
		{
			display_error();
		}
				
		DigScopeRdBuf[BytesRead] = '\0';
		scanReturn = sscanf((char *)DigScopeRdBuf, "%s", complete);
				
		complete[BytesRead] = '\0';
		iCount++;

        /*if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - waiting for Scope to digitize\033[m\n");
		}*/

	} while (strcmp(complete, "+1") != 0 && iCount <= MAX_TRIES);
	
	memset(DigScopeRdBuf, '\0', READBUFSIZE);
	//err =  hpe1428a_readInstrData(oscope_handle, 256, (ViChar*) DigScopeRdBuf, &BytesRead);
	err = atxml_ReadCmds( "DSO_1", (char *) DigScopeRdBuf, 256, (int *) &BytesRead);
	DigScopeRdBuf[BytesRead] = '\0';

	//Do not display timeout error. Timeout may occur if externally triggered and the trigger is not present.
	if ((err != 0) && (err != VI_ERROR_TMO))
	{
		display_error();
	}

	if (err == VI_ERROR_TMO) /* Time Out error */
	{
		ErrMsg(5, "MAX-TIME Exceeded");
	}

	err = atxmlDF_viSetAttribute("DSO_1", VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE,10000);
	display_error();

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope() - return \033[m\n");
	}
}

/********************************************************************************/
/* 	 					Initialize the scope									*/
/********************************************************************************/
void init_scope1()
{
	const int MAX_TRIES = 10;
	DATUM	*maxt_datum;
	double	maxt;
	ViChar complete[READBUFSIZE];
	int iCount = 0;
	int iWait = 0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1()\033[m\n");
		//MessageBox(0, "Scope WCEM", "init_scope1" , MB_OK);
	}

	chan = 1;
	scope_reset_flag = 0;

	/* set VISA TMO_VALUE, if max-time is specified */
	maxt_datum = GetDatum(M_MAXT, K_SET);

	if (maxt_datum != 0)
	{
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - MAX-TIME Found\033[m\n");
			Display(hp1428_msg_buf);
		}
			
		maxt = DECDatVal(maxt_datum, 0);
		iWait = maxt;
		maxt *= 1000; /*convert to milliseconds */
		err = atxmlDF_viSetAttribute("DSO_1", VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE, (unsigned long)maxt);
		display_error();
			
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - VISA TMO = %f\033[m\n", maxt);
			Display(hp1428_msg_buf);
		}
	}

	if (probe)
	{
		if (ResetEvent(probeInfo.hMakeMeasurementHandle))
		{
			sysmon_err = WaitForProbeButt(maxtimeout);
		}
		else
		{
			retrieveErrorMessage("init_scope1()", "Recieved an error: ");
			sysmon_err = CEM_ERROR;
		}

		if (sysmon_err != 0)
		{
			switch (sysmon_err)
			{
			case -1:
				Display("HPE1428 Warning: ");
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[31;40m Probe button not pressed before Max-Time of %f sec.\033[m\n", maxtimeout);
				Display(hp1428_msg_buf);
				break;
			case 53:
				Display("\033[33;40m HPE1428 Warning: SYSMON.EXE not running\033[m\n");
				break;
			default:
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[33;40m HPE1428 Warning: Invalid probe return: %d\033[m\n", sysmon_err);
				Display(hp1428_msg_buf);
				break;
			}
			return;
		}
		else
		{
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - STOP \033[m\n");
			}
			abort_acquisition();
			
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - digitizing channel \033[m\n");
			}
			formatAndWrite(NOCHECK, "DIG CHAN%d", chan);

			/*if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() %s\n", hp1428_string);
				Display(hp1428_msg_buf);
			}*/

			//added counter in loop below in the case that no stimulus is present at scope or probe button not working
			do
			{
				scanReturn = 0;
				memset(DigScopeRdBuf, '\0', READBUFSIZE);
				memset(complete, '\0', READBUFSIZE);

                if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - checking for operation complete\033[m\n");
				}
				formatAndWrite(NOCHECK, "%s", "*OPC?\n");
				
				err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

				if (err != 0 && err != VI_ERROR_TMO)
				{
					display_error();
				}
				
				DigScopeRdBuf[BytesRead] = '\0';
				scanReturn = sscanf((char *)DigScopeRdBuf, "%s", complete);
				
				complete[BytesRead] = '\0';
				iCount++;
				
				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - waiting for Scope to digitize\033[m\n");
				}

			} while (strcmp(complete, "+1") != 0 && iCount <= MAX_TRIES);
		}
	}
	else
	{

        if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - STOP \033[m\n");
		}
		abort_acquisition();
		
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - digitizing channel \033[m\n");
		}
		formatAndWrite(NOCHECK, "DIG CHAN%d", chan);

		/*if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() %s\n", hp1428_string);
			//Display(hp1428_msg_buf);
		}*/

		scanReturn = 0;
		memset(DigScopeRdBuf, '\0', READBUFSIZE);

		if (trg_flg == 1)
		{
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - triggered acquistion flag is set, 2 sec wait\033[m\n");
			}
			Sleep(2000);   // Allow scope to digest before trigger can happen
		}
	}

	/* If setup for external trigger and using single-action verbs, the remaining
	code in this function will cause the RTS to hang up during INIT before the
	supposed next ATLAS statement that generates the actual external trigger can
	execute. Therefore, the RTS MONITOR display persistance fix below cannot
	be used in this case.
	*/
	if (trg_flg == 1)
	{
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - triggered acquisition flag is set, returning\033[m\n");
		}

		return;
	}

	/*	These commands cause this function to wait until the measurement is complete
	or the VISA timeout at read. This is necessary so the
	persistance of the RTS-displayed value during a MONITOR statement is readable.
	Otherwise, the time spent waiting for the Scope to complete measurement is
	during the FETCH part, when the RTS has already blanked-out the measurement
	display.
	*/

	iCount = 0;

	do
	{
		scanReturn = 0;
		memset(DigScopeRdBuf, '\0', READBUFSIZE);
		memset(complete, '\0', READBUFSIZE);

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - checking for operation complete\033[m\n");
		}
		formatAndWrite(NOCHECK, "%s", "*OPC?\n");
	
		err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

		if (err != 0 && err != VI_ERROR_TMO)
		{
			display_error();
		}
	
		DigScopeRdBuf[BytesRead] = '\0';
		scanReturn = sscanf((char *)DigScopeRdBuf, "%s", complete);

	    complete[BytesRead] = '\0';
		iCount++;

	} while (strcmp(complete, "+1") != 0 && iCount <= MAX_TRIES);

	memset(DigScopeRdBuf, '\0', READBUFSIZE);

	return;

	// Not sure any of this is really necessary so I'm returning here

	//err =  hpe1428a_readInstrData(oscope_handle, 256, (ViChar*) DigScopeRdBuf, &BytesRead);
	err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);
	DigScopeRdBuf[BytesRead] = '\0';

	//Do not display timeout error. Timeout may occur if externally triggered and the trigger is not present.
	if ((err != 0) && (err != VI_ERROR_TMO))
	{
		display_error();
	}

	if (err == VI_ERROR_TMO) /* Time Out error */
	{
		ErrMsg(5, "MAX-TIME Exceeded");
	}

	err = atxmlDF_viSetAttribute("DSO_1", VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE, 10000);
	display_error();

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - return \033[m\n");
	}
}

/********************************************************************************/
/* 	 					Initialize scope chan 2									*/
/********************************************************************************/
void init_scope2()
{
	const int MAX_TRIES = 10;
	DATUM	*maxt_datum;
	double	maxt;
	ViChar complete[READBUFSIZE];
	int iCount = 0;
	int iWait = 0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2()\033[m\n");
		//MessageBox(0, "Scope WCEM", "init_scope2" , MB_OK);
	}

	chan = 2;
	scope_reset_flag = 0;

    /* set VISA TMO_VALUE, if max-time is specified */
	maxt_datum = GetDatum(M_MAXT, K_SET);

	if (maxt_datum != 0)
	{
		/*if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - MAX-TIME Found\033[m\n");
			Display(hp1428_msg_buf);
		}*/
			
		maxt = DECDatVal(maxt_datum, 0);
		iWait = maxt;
		maxt *= 1000; /*convert to milliseconds */
		err = atxmlDF_viSetAttribute("DSO_1", VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE, (unsigned long)maxt);
		display_error();
			
		/*if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - VISA TMO = %f\033[m\n", maxt);
			Display(hp1428_msg_buf);
		}*/
	}

	if (probe)
	{
		if (ResetEvent(probeInfo.hMakeMeasurementHandle))
		{
			sysmon_err = WaitForProbeButt(maxtimeout);
		}
		else
		{
			retrieveErrorMessage("init_scope2()", "Recieved an error: ");
			sysmon_err = CEM_ERROR;
		}

		if (sysmon_err != 0)
		{
			switch (sysmon_err)
			{
			case -1:
				Display("HPE1428 Warning: ");
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[31;40m Probe button not pressed before Max-Time of %f sec.\033[m\n", maxtimeout);
				Display(hp1428_msg_buf);
				break;
			case 53:
				Display("\033[33;40m HPE1428 Warning: SYSMON.EXE not running\033[m\n");
				break;
			default:
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[33;40m HPE1428 Warning: Invalid probe return: %d\033[m\n", sysmon_err);
				Display(hp1428_msg_buf);
				break;
			}
			return;
		}
		else
		{
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - STOP \033[m\n");
			}
			abort_acquisition();
			
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - digitizing channel \033[m\n");
			}
			formatAndWrite(NOCHECK, "DIG CHAN%d", chan);

			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() %s\n", hp1428_string);
				//Display(hp1428_msg_buf);
			}

			//added counter in loop below in the case that no stimulus is present at scope or probe button not working
			do
			{
				scanReturn = 0;
				memset(DigScopeRdBuf, '\0', READBUFSIZE);
				memset(complete, '\0', READBUFSIZE);

				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - checking for operation complete\033[m\n");
				}
				formatAndWrite(NOCHECK, "%s", "*OPC?\n");

				err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

				if (err != 0 && err != VI_ERROR_TMO)
				{
					display_error();
				}
				
				DigScopeRdBuf[BytesRead] = '\0';
				scanReturn = sscanf((char *)DigScopeRdBuf, "%s", complete);
				
				complete[BytesRead] = '\0';
				iCount++;

                if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - waiting for Scope to digitize\033[m\n");
				}

			} while (strcmp(complete, "+1") != 0 && iCount <= MAX_TRIES);
		}
	}
	else
	{

        if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - STOP \033[m\n");
		}
		abort_acquisition();
		
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - digitizing channel \033[m\n");
		}
		formatAndWrite(NOCHECK, "DIG CHAN%d", chan);

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() %s\n", hp1428_string);
			//Display(hp1428_msg_buf);
		}

		scanReturn = 0;
		memset(DigScopeRdBuf, '\0', READBUFSIZE);

		if (trg_flg == 1)
		{
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - triggered acquistion flag is set, 2 sec wait\033[m\n");
			}
			Sleep(2000);   // Allow scope to digest before trigger can happen
		}
	}

	/* If setup for external trigger and using single-action verbs, the remaining
	code in this function will cause the RTS to hang up during INIT before the
	supposed next ATLAS statement that generates the actual external trigger can
	execute. Therefore, the RTS MONITOR display persistance fix below cannot
	be used in this case.
	*/
	if (trg_flg == 1)
	{
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope1() - triggered acquisition flag is set, returning\033[m\n");
		}

		return;
	}

	/*	These commands cause this function to wait until the measurement is complete
	or the VISA timeout at read. This is necessary so the
	persistance of the RTS-displayed value during a MONITOR statement is readable.
	Otherwise, the time spent waiting for the Scope to complete measurement is
	during the FETCH part, when the RTS has already blanked-out the measurement
	display.
	*/

	iCount = 0;

	do
	{
		scanReturn = 0;
		memset(DigScopeRdBuf, '\0', READBUFSIZE);
		memset(complete, '\0', READBUFSIZE);

		//if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		//{
		//	Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - checking for operation complete\033[m\n");
		//}
		formatAndWrite(NOCHECK, "%s", "*OPC?\n");
	
		err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

		if (err != 0 && err != VI_ERROR_TMO)
		{
			display_error();
		}
	
		DigScopeRdBuf[BytesRead] = '\0';
		scanReturn = sscanf((char *)DigScopeRdBuf, "%s", complete);

	    complete[BytesRead] = '\0';
		iCount++;

	} while (strcmp(complete, "+1") != 0 && iCount <= MAX_TRIES);

	memset(DigScopeRdBuf, '\0', READBUFSIZE);

	return;

	// Not sure any of this is really necessry so I'm returning here

	//err =  hpe1428a_readInstrData(oscope_handle, 256, (ViChar*) DigScopeRdBuf, &BytesRead);
	err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);
	DigScopeRdBuf[BytesRead] = '\0';

	//Do not display timeout error. Timeout may occur if externally triggered and the trigger is not present.
	if ((err != 0) && (err != VI_ERROR_TMO))
	{
		display_error();
	}

	if (err == VI_ERROR_TMO) /* Time Out error */
	{
		ErrMsg(5, "MAX-TIME Exceeded");
	}

	err = atxmlDF_viSetAttribute("DSO_1", VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE, 10000);
	
	if (err != 0)
	{
		display_error();
	}
	
	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_scope2() - return \033[m\n");
	}
}

/********************************************************************************/
/* 	 					Fetch the scope											*/
/********************************************************************************/
double fetch_scope()
{

	const int MAX_TRIES = 10;
	ViChar complete[READBUFSIZE];
	int iCount = 0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_scope()\033[m\n");
		sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_scope() %s, time=%d\n",fstring,timeGetTime());
		Display(hp1428_msg_buf);
		//MessageBox(0, "Scope WCEM", "fetch_scope" , MB_OK);
	}
	
	scope_reset_flag = 0;

	// set VISA TMO_VALUE, if max-time is specified
	/*maxt_datum = GetDatum(M_MAXT, K_SET);

	if (maxt_datum != 0)
	{
		maxt = DECDatVal(maxt_datum,0);
		maxt *= 1000; // convert to milliseconds
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_scope() Setting TMO value to %f \033[m\n", maxt);
			Display(hp1428_msg_buf);
		}
		err = atxmlDF_viSetAttribute("DSO_1",VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE,(unsigned long) maxt);
		display_error();
	}*/

	fdatum=FthDat();
  
	if (fdatum !=0)
	{
		cnt=DatCnt(fdatum);
		
		for (i=0;i<cnt;i++)
		{
			int rtncnt = 0;
			//err = hpe1428a_writeInstrData(oscope_handle,fstring);
			err = atxml_WriteCmds( "DSO_1", fstring, &rtncnt);

			/* Check for timeout */
			if (err == VI_ERROR_TMO) /* Time Out error */
			{
				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_scope() Error Code = %x\n",err);
					Display(hp1428_msg_buf);
				}
				
				ErrMsg(5,"Trigger Not Found - MAX TIME Exceeded");
				reset_scope();
				return 0;			
			}
			else
			{
				display_error();
			}

			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - %s\n", fstring);
				Display(hp1428_msg_buf);
			}

			memset(DigScopeRdBuf, '\0', READBUFSIZE);
			//err = hpe1428a_readInstrData(oscope_handle,256,(ViChar*)DigScopeRdBuf,&BytesRead);
			err = atxml_ReadCmds( "DSO_1", (char *) DigScopeRdBuf, 256, (int *) &BytesRead);
			DigScopeRdBuf[BytesRead] = '\0';

			/* Check for timeout */
			if (err == VI_ERROR_TMO) /* Time Out error */
			{
				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - Error Code = %x\n",err);
					Display(hp1428_msg_buf);
				}

				ErrMsg(5,"Trigger Not Found - MAX TIME Exceeded");
				reset_scope();
				return 0;
			}
			else
			{
				display_error();			//CheckHPe1428ABErrors();
			}

			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_scope-read() time=%d\n",timeGetTime());
				Display(hp1428_msg_buf);
			}

			scanReturn = sscanf( (char*) DigScopeRdBuf, "%lf", &measurementValue);

			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - measurementValue = %f\n",measurementValue);
				Display(hp1428_msg_buf);
			}

			// ***********************************************************
			// This logic below will have to be removed in the near future
			// ***********************************************************

			if (measurementValue >= OVERRANGE)  // invalid reading, try autoscale
			{
				int rtncnt = 0;
				err = atxmlDF_viSetAttribute("DSO_1",VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE, 8000);
				display_error();
				
				set_autoscale();

				// Allow up to 8 seconds to perform autoscale
				do
				{
					scanReturn = 0;
					memset(DigScopeRdBuf, '\0', READBUFSIZE);
					memset(complete, '\0', READBUFSIZE);

					//if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
					//{
					//	Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_scope() - checking for operation complete\033[m\n");
					//}
					formatAndWrite(NOCHECK, "%s", "*OPC?\n");
	
					err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

					if (err != 0 && err != VI_ERROR_TMO)
					{
						display_error();
					}
	
					DigScopeRdBuf[BytesRead] = '\0';
					scanReturn = sscanf((char *)DigScopeRdBuf, "%s", complete);

				    complete[BytesRead] = '\0';
					iCount++;

				} while (strcmp(complete, "+1") != 0 && iCount <= MAX_TRIES);

				memset(DigScopeRdBuf, '\0', READBUFSIZE);

				//if ((err =  hpe1428a_readInstrData(oscope_handle, 256, (ViChar *)DigScopeRdBuf, &BytesRead)) != 0)
				/*if ((err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead)) != 0)
				{
					display_error();
				}*/

				DigScopeRdBuf[BytesRead] = '\0';

				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - %s\n", hp1428_string);
					//Display(hp1428_msg_buf);
				}

				//err = hpe1428a_writeInstrData(oscope_handle,Mea_Buf);
				err = atxml_WriteCmds( "DSO_1", Mea_Buf, &rtncnt);
				display_error();

				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - %s\n", Mea_Buf);
					Display(hp1428_msg_buf);
				}
				
				memset(DigScopeRdBuf, '\0', READBUFSIZE);
				//err = hpe1428a_readInstrData(oscope_handle,256,(ViChar *)DigScopeRdBuf,&BytesRead);
				err = atxml_ReadCmds( "DSO_1", (char *) DigScopeRdBuf, 256, (int *) &BytesRead);
				display_error();
				DigScopeRdBuf[BytesRead] = '\0';

				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_scope-read() time=%d\n",timeGetTime());
					Display(hp1428_msg_buf);
				}

				scanReturn = sscanf( (char *) DigScopeRdBuf, "%lf", &measurementValue);

				if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
				{
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - measurementValue = %f\n",measurementValue);
					Display(hp1428_msg_buf);
				}
			}

			DECDatVal(fdatum,i) = measurementValue;
		}
      
		FthCnt(cnt);
	}
	else
	{
		int rtncnt;
		//CJWTEMPerr = hpe1428a_errorMessage(oscope_handle, errorCode, errorMessage);
		errorCode = 123;
		while( errorCode != 0 )
		{
			err = atxml_WriteCmds( "DSO_1", "SYST:ERR?", &rtncnt);
			err = atxml_ReadCmds( "DSO_1", (char *) errorMessage, 256, (int *) &rtncnt);
			errorMessage[rtncnt] = 0;
			sscanf( errorMessage, "%d", &errorCode );
		}
		display_error();
	}

	if (errorCode !=0)
	{
		Display(errorMessage);
	}

	/* return VISA TMO_VALUE to default of 10000 */
	err = atxmlDF_viSetAttribute("DSO_1",VI_SESSION_NOT_USED, VI_ATTR_TMO_VALUE, 10000);
	display_error();
	
	reset_scope();

	return measurementValue;
}

/********************************************************************************/
/* 	 				Fetch the scope for average reading							*/
/********************************************************************************/
double fetch_volt_avg()
{

	double measValue = 0.0;
	scope_reset_flag = 0;

	if (IsSimOrDeb("DSO_1") & FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_volt_avg()\033[m\n");
		//MessageBox(0, "Scope WCEM", "fetch_volt_avg" , MB_OK);
	}

	if((int)sample_cnt > 0)
	{
		double  totalMeas = 0.0;
		int counter;
				
		for (counter = 0; counter < (int)sample_cnt; counter++)
        {
            init_scope();
			measValue = fetch_scope();

			if (scope_reset_flag)
			{
				break;      //Allow escape if timeout occurs
			}
			else
			{
				totalMeas += measValue;
			}
        }
		measValue = totalMeas / counter;
	}
	else
	{
		measValue = fetch_scope();
	}

	if (IsSimOrDeb("DSO_1") & FLAG)
	{
		sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - Average Sample Count = \t%d\033[m\n",(int)sample_cnt);
		Display(hp1428_msg_buf);
	}

	return (double) measValue;
}
/********************************************************************************/
/* 	 			Fetch the scope preshoot and overshoot reading					*/
/********************************************************************************/
double fetch_Pre_OvrShoot()
{

	double measValue = 0.0;

	if (IsSimOrDeb("DSO_1") & FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_Pre_OvrShoot()\033[m\n");
		//MessageBox(0, "Scope WCEM", "fetch_Pre_OvrShoot" , MB_OK);
	}

	measValue = fetch_scope();
	measValue *= 100;

	return (double) measValue;
}
/********************************************************************************/
/* 	 				   Read the scope for data									*/
/********************************************************************************/
double read_scope()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - read_scope()\033[m\n");
		//MessageBox(0, "Scope WCEM", "read_scope" , MB_OK);
	}

	scope_reset_flag = 0;
	fdatum=FthDat();
  
	if (fdatum !=0)
	{
		cnt=DatCnt(fdatum);
		
		for (i=0;i<cnt;i++)
		{

			formatAndWrite(CHECK, "MEAS:SOUR %s", measource);
			formatAndWrite(CHECK, "MEAS:DEF DEL,%s,%s", fromdata, todata);
			formatAndWrite(NOCHECK, "%s", "MEAS:DELAY?");

			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] - read_scope() %s\n", hp1428_string);
				//Display(hp1428_msg_buf);
			}

			//err = hpe1428a_readInstrData(oscope_handle,256,(ViChar *)DigScopeRdBuf,&BytesRead);
			err = atxml_ReadCmds( "DSO_1", (char *) DigScopeRdBuf, 256, (int *) &BytesRead);
			display_error();
			CheckHPe1428ABErrors();
			scanReturn = sscanf( (CHAR *)DigScopeRdBuf, "%lf", &measurementValue);
			DECDatVal(fdatum,i) = measurementValue;
		}

		FthCnt(cnt);
	}
	else
	{
		//CJWerr = hpe1428a_errorMessage(oscope_handle, errorCode, errorMessage);
		display_error();
	}
  
	if (errorCode !=0)
	{
		Display(errorMessage);
	}
	
	reset_scope();

	return measurementValue;
}

/********************************************************************************/
/* 	 				   Fetch Values									*/
/********************************************************************************/
void fetch_values(char*source, double &yincr, double &yorigin, double &yref)
{
	//double result[3];
	char *token[10] = {0};
	char sep[] = "\t,";
	char *next_token1 = NULL;
	int i;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_values()\033[m\n");
		//MessageBox(0, "Scope WCEM", "fetch_values" , MB_OK);
	}

	for(i = 0; i == 0 || (token[i - 1] != NULL && i < 10); i++)
	{
		token[i] = strtok_s(source, sep, &next_token1);
		source = NULL;
	}

	yincr = atof(token[7]);
	yorigin = atof(token[8]);
	yref = atof(token[9]);

}


/********************************************************************************/
/* 	 				   Digitize the waveform									*/
/********************************************************************************/
int read_waveform()
{
	int	numdigits;
	int strtdata;
	int numpoints;
	int	byte1;
	int byte2;
	int j;
	float answer;
	double Result[8000];
	double yincr;
	double yorigin;
	double yref;
	ViChar preamble[READBUFSIZE];
	ViChar complete[READBUFSIZE];
	ViChar	wavedata[WAVEBYTES];
	int iCount;
	char count[10] = {0};
	char num[1];
	char* chrTime;
	time_t tTime;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - read_waveform()\033[m\n");
		//MessageBox(0, "Scope WCEM", "read_waveform" , MB_OK);
	}

	scope_reset_flag = 0;
	memset(Result, '\0', sizeof(Result));
	memset(preamble, '\0', sizeof(preamble));
	fdatum = FthDat();
	cnt = DatCnt(fdatum);
    iCount = 0;

    // Let previous operation complete before sending fetch
	do
	{
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			tTime = std::chrono::system_clock::to_time_t(std::chrono::system_clock::now());
			chrTime = ctime(&tTime);
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - read_waveform() - %s\033[m\n", chrTime);
			Display(hp1428_msg_buf);
		}

		scanReturn = 0;
		memset(DigScopeRdBuf, '\0', READBUFSIZE);
		memset(complete, '\0', READBUFSIZE);

		formatAndWrite(NOCHECK, "%s", "*OPC?\n");

		err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

		if (err != 0 && err != VI_ERROR_TMO)
		{
			display_error();
		}

		DigScopeRdBuf[BytesRead] = '\0';
		scanReturn = sscanf( (char *) DigScopeRdBuf, "%s", complete);

		complete[BytesRead] = '\0';
		iCount++;
		Sleep(500);

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			tTime = std::chrono::system_clock::to_time_t(std::chrono::system_clock::now());
			chrTime = ctime(&tTime);
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - read_waveform() - %s\033[m\n", chrTime);
			Display(hp1428_msg_buf);
		}
	} while (strcmp(complete,"+1") != 0 && iCount < 1); //  && iCount <= (TmoVal/1000)

	if (fdatum !=0)
	{
		formatAndWrite(CHECK, "%s", "WAV:FORM WORD");

        if (err == VI_ERROR_TMO) /* Time Out error */
        {
        	ErrMsg(5,"MAX-TIME Exceeded");
			
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - read_waveform() - returning due to scope time-out\033[m\n");
				//MessageBox(0, "Scope WCEM", "read_waveform" , MB_OK);
			}
			
            return 0;
        }

		formatAndWrite(CHECK, "WAV:SOUR CHAN%d", chan);
		formatAndWrite(CHECK, "%s", "WAV:PRE?");
		Sleep(500); // Delay for settling time

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] %s\n", hp1428_string);
			//Display(hp1428_msg_buf);
		}

		memset(DigScopeRdBuf, '\0', READBUFSIZE);
		//if ((err = hpe1428a_readInstrData(oscope_handle, 512, (ViChar *)DigScopeRdBuf, &BytesRead)) != 0)
		err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 512, (int *)&BytesRead);

		if (err != 0)
		{
			display_error();
			return 0;
		}

		DigScopeRdBuf[BytesRead] = '\0';
		scanReturn = sscanf( (char *) DigScopeRdBuf, "%s", preamble);
		Sleep(500); // Delay for settling time		
		CheckHPe1428ABErrors();
		
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - read_waveform() - calling fetch_values()\033[m\n");
		}
		fetch_values(preamble, yincr, yorigin, yref);

		do
		{
			scanReturn = 0;
			memset(DigScopeRdBuf, '\0', READBUFSIZE);
			memset(complete, '\0', READBUFSIZE);
			
			formatAndWrite(NOCHECK, "%s", "*OPC?\n");

			err = atxml_ReadCmds("DSO_1", (char *)DigScopeRdBuf, 256, (int *)&BytesRead);

			if (err != 0 && err != VI_ERROR_TMO)
			{
				display_error();
			}
			DigScopeRdBuf[BytesRead] = '\0';
			scanReturn = sscanf((char*)DigScopeRdBuf, "%s", complete);

		    complete[BytesRead] = '\0';

		} while (strcmp(complete, "+1") != 0);

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - read_waveform() - grabbing data points from scope\033[m\n");
		}

		formatAndWrite(NOCHECK, "%s", "WAV:DATA?");
		memset(DigScopeRdBuf, '\0', sizeof(DigScopeRdBuf));
		memset(wavedata, '\0', sizeof(wavedata));
		//err = hpe1428a_readInstrData(oscope_handle, WAVEBYTES, (ViChar *)DigScopeRdBuf, &BytesRead);
		err = atxml_ReadCmds( "DSO_1", (char *)DigScopeRdBuf, WAVEBYTES, (int *) &BytesRead);
		for (int i = 0; i < BytesRead; i++)
		{
			wavedata[i] = DigScopeRdBuf[i];
		}
		//scanReturn = sscanf( (char *)DigScopeRdBuf, "%s", wavedata);
		wavedata[BytesRead] = '\0';
		display_error();
		CheckHPe1428ABErrors();

		if (wavedata[0] == '#')
		{
			j = 0;
			num[0] = wavedata[1];
			numdigits = atoi(num);
			strtdata = numdigits + 2;
          
			for (int i=0; i<numdigits; i++)
			{
				count[i] = wavedata[i+2];
			}
          
			numpoints = atoi(count);

			for (int i=strtdata; i<(numpoints+strtdata); i++)
			{
				byte1 = wavedata[i];
				i++;
				byte2 = wavedata[i];

				//answer = (byte1 * 256) + (float)byte2;
				answer = (byte1 * 256) + (byte2 / 256);
				Result[j] = ((answer - yref) * yincr) + yorigin;
				j++;
			}
          
			for (int i=0;i<cnt;i++)
			{
				DECDatVal(fdatum,i) = Result[i];
			}

			FthCnt(cnt);
		}
		else
		{
			Display("Data is not in WAVEFORM format");
		}
	}
	else
	{
		//CJWerr = hpe1428a_errorMessage(oscope_handle, errorCode, errorMessage);
		display_error();
	}
  
	if (errorCode !=0)
	{
		Display(errorMessage);
	}

	return cnt;
}

/********************************************************************************/
/* 	 				   Reset the Oscilloscope									*/
/********************************************************************************/
void reset_scope()
{

    if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - reset_scope()\033[m\n");
	}

	if (!bInit)
	{
		IsSimOrDeb("INIT_VARS");
		bInit = true;
	}

	scopeInReset = 1;
	checkinstrumentinit();

	if (scope_reset_flag == 0)
	{
		sprintf_s(instrument,sizeof(instrument),"hpe1428a");

        //was err = viClear(oscope_handle);
		err = atxmlDF_viClear("DSO_1", VI_SESSION_NOT_USED);
		display_error();

		abort_acquisition();
		formatAndWrite(CHECK, RESET);
		//CJWerr = hpe1428a_reset(oscope_handle);
		display_error();
		CheckHPe1428ABErrors();
	}

	scope_reset_flag = 1;
	probe = 0;
	trg_flg = 0;
	scopeInReset = 0;
}

/********************************************************************************/
/* 	 Initialize the scope to load waveform data from memory or math location 	*/
/********************************************************************************/
void init_test()
{

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - init_test()\033[m\n");
		//MessageBox(0, "Scope WCEM", "init_test" , MB_OK);
	}

	scope_reset_flag = 0;

	if (probe)
	{
		if (ResetEvent(probeInfo.hMakeMeasurementHandle)) 
		{
			sysmon_err = WaitForProbeButt(maxtimeout);
		}
		else 
		{
			retrieveErrorMessage("init_test()", "Recieved an error: ");
			sysmon_err = CEM_ERROR;
		}

		if (sysmon_err != 0)
		{
			switch(sysmon_err)
			{
				case -1:
					Display("HPE1428 Warning: ");
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf)," Probe button not pressed before Max-Time of %f sec.",maxtimeout);
					Display(hp1428_msg_buf);
					break;
				case 53:
					Display("HPE1428 Warning: SYSMON.EXE not running");
					break;
				default:
					sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"HPE1428 Warning: Invalid probe return: %d",sysmon_err);
					Display(hp1428_msg_buf);
					break;
			}
		}
		else
		{
			abort_acquisition();
				
			if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
			{
				//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] %s\n", hp1428_string);
				//Display(hp1428_msg_buf);
			}
		}
	}
	else
	{
		abort_acquisition();
			
		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] %s\n", hp1428_string);
			//Display(hp1428_msg_buf);
		}
	}

	if(trg_flg == 1)
	{
		Sleep(2000);   // Allow scope to digest before trigger can happen
	}
}

/********************************************************************************/
double fetch_compare()
{

	int i, fail_bit = 0, count, second = 0;

	comp_flag = 1;
	count = read_wave_test();
	scope_reset_flag = 0;
	reset_scope();
	scope_reset_flag = 0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - fetch_compare()\033[m\n");
		//MessageBox(0, "Scope WCEM", "fetch_compare" , MB_OK);
	}

	for(i=1;i<499;i++)
	{
		if(comp_holder[i] < 0)
		{
			comp_holder[i] *= -1;
		}

		if(comp_holder[i] > allow)
		{
			second++;

			if(second>5)
			{
			  fail_bit = 1;
	  		  i = 500;
			}
		}
	}

	comp_flag = 0;

	return (double) fail_bit;
}
/*********************************************************************************/
/*  	Fetch the scope to load waveform data from the Memory or Math location 	 */
/*********************************************************************************/
int read_wave_test()
{
	int	numdigits;
	int strtdata;
	int numpoints;
	int	byte1;
	int byte2;
	int j;
	int i;
	float answer;
	double Result[8000];
	double yincr;
	double yorigin;
	double yref;
	ViChar preamble[READBUFSIZE];
	ViChar	wavedata[WAVEBYTES];
	char count[10] = {0};
	char num[1];

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - read_wave_test()\033[m\n");
		//MessageBox(0, "Scope WCEM", "read_wave_test" , MB_OK);
	}

	scope_reset_flag = 0;
	memset(Result, '\0', sizeof(Result));
	fdatum = FthDat();
	cnt = DatCnt(fdatum);

	if (fdatum !=0)
	{
		formatAndWrite(CHECK, "%s", "WAV:FORM WORD");
		formatAndWrite(CHECK, "WAV:SOUR %s", test_ch);
		formatAndWrite(NOCHECK, "%s", "WAV:PRE?");
		Sleep(500); // Delay for settling time

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] %s\n", hp1428_string);
			//Display(hp1428_msg_buf);
		}
		//memset(DigScopeRdBuf, '\0', READBUFSIZE);

		//if ((err = hpe1428a_readInstrData(oscope_handle, 512, (ViChar *)DigScopeRdBuf, &BytesRead)) != 0)
		if( (err = atxml_ReadCmds( "DSO_1", (char *) DigScopeRdBuf, 512, (int *) &BytesRead)) != 0 )
		{
			display_error();
		}

		DigScopeRdBuf[BytesRead] = '\0';
		CheckHPe1428ABErrors();
		scanReturn = sscanf( (char *) DigScopeRdBuf, "%s", preamble);
		Sleep(500); // Delay for settling time
		CheckHPe1428ABErrors();
		fetch_values(preamble, yincr, yorigin, yref);
		formatAndWrite(NOCHECK, "%s", "WAV:DATA?");
		Sleep(500); // Delay for settling time

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE): [Dso_Scope_T] %s\n", hp1428_string);
			//Display(hp1428_msg_buf);
		}

		memset(wavedata, '\0', sizeof(wavedata));

		//err = hpe1428a_readInstrData(oscope_handle, WAVEBYTES, (ViChar *)DigScopeRdBuf, &BytesRead);
		//this is exceeding DigScopeRdBuf, which was 4100.  Increased it to cover wave data
		err = atxml_ReadCmds( "DSO_1", (char *) DigScopeRdBuf, WAVEBYTES, (int *) &BytesRead);
		DigScopeRdBuf[BytesRead] = '\0';
		scanReturn = sscanf( (char *) DigScopeRdBuf, "%s", wavedata);
		wavedata[BytesRead] = '\0';
		display_error();
		CheckHPe1428ABErrors();
		//DigScopeRdBuf[BytesRead] = '\0';
		//Sleep(500); // Delay for settling time
		//scanReturn = sscanf( (char *) DigScopeRdBuf, "%s", wavedata);

		if (wavedata[0] == '#')
		{
			j = 0;
			num[0] = wavedata[1];
			numdigits = atoi(num);
			strtdata = numdigits + 2;
          
			for (i=0; i<numdigits; i++)
			{
				count[i] = wavedata[i+2];
			}
          
			numpoints = atoi(count);

			for (i=strtdata; i<(numpoints+strtdata); i++)
			{
				byte1 = wavedata[i];
				i++;
				byte2 = wavedata[i];
				answer = (byte1 * 256) + (float)byte2;
				Result[j] = ((answer - yref) * yincr) + yorigin;
				j++;
			}
          
			for (i=0;i<cnt;i++)
			{
				DECDatVal(fdatum,i) = Result[i];
			}

			FthCnt(cnt);
		}
		else
		{
			Display("Data is not in WAVEFORM format");
		}
	}
	else
	{
		//CJWerr = hpe1428a_errorMessage(oscope_handle, errorCode, errorMessage);
		display_error();
	}
  
	if (errorCode !=0)
	{
		Display(errorMessage);
	}

	if(comp_flag)
	{
	   comp_holder = &Result[0];
	}

	return cnt;
}

/********************************************************************************
 * 			Function to send Out_Buf to the instrument           		        *
 ********************************************************************************/
void WriteHPe1428AB(ViString Out_Buf, int noErrorCheck)
{

	int rtncnt = 0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - WriteHPe1428AB()\033[m\n");
		//MessageBox(0, "Scope WCEM", "WriteHPe1428AB" , MB_OK);
	}

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		//sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\tCEMDEBUG (SCOPE):   %s\n",Out_Buf);
		//Display(hp1428_msg_buf);
	}

	//if ((err = hpe1428a_writeInstrData(oscope_handle,Out_Buf)) != 0)
	if ((err = atxml_WriteCmds("DSO_1", Out_Buf, &rtncnt)) != 0)
	{
		display_error();
	}

	if (noErrorCheck) 
	{
		return;
	}

	Sleep(200);
	CheckHPe1428ABErrors();
}

/********************************************************************************
 *		Function to check instrument for errors                  		     	*
 * 		11/18/98, Unable to use hpe1428a_errorQuery() until the source			*
 *		can be modified to a resolve a problem discovered during      			*
 *		integration.  Solution is to communicate directly with        			*
 * 		instrument using COMP commands                                			*
 ********************************************************************************/
void CheckHPe1428ABErrors()
{
	ViChar		errorMessage[256];
	ViInt32	    errorCode = 0, errorHolder = 0;

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - CheckHPe1428ABErrors()\033[m\n");
		//MessageBox(0, "Scope WCEM", "CheckHPe1428ABErrors" , MB_OK);
	}

	do
	{
		formatAndWrite(NOCHECK, "%s", "SYST:ERR?");
		memset(DigScopeRdBuf, '\0', READBUFSIZE);

		if( (err = atxml_ReadCmds( "DSO_1", (char *) DigScopeRdBuf, 256, (int *) &BytesRead)) != 0 )	//hpe1428a_readInstrData
		{
			display_error();
		}
	
		DigScopeRdBuf[BytesRead] = '\0';
		sprintf_s(errorMessage,sizeof(errorMessage),""); /* Flush errorMessage string */
		scanReturn = sscanf( (char *) DigScopeRdBuf, "%ld,%[^\012]", &errorCode, errorMessage);

		if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
		{
			sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf),"\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - CheckHPe1428ABErrors() - errorCode: %ld, errorMessage: %s\033[m\n", errorCode, errorMessage);
			Display(hp1428_msg_buf);
		}

		if (errorCode < 0 && (errorCode != -410 && scopeInReset != 1))
		{	//compares previous and current error to avoid displaying it twice
			//this comparioson is done only in local level (in the this function)
			if (errorCode != errorHolder)
			{
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[31;40m*** Scope Error Code: %ld ***\033[m\n", errorCode);
				Display(hp1428_msg_buf);
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[31;40m*** Scope Error Message: %s ***\033[m\n", errorMessage);
				Display(hp1428_msg_buf);
				Sleep(5000);
			}
			errorHolder = errorCode;	
		}
		else if (errorCode > 0)
		{	//compares previous and current error to avoid displaying it twice
			//this comparioson is done only in local level (in the this function)
			if (errorCode != errorHolder)
			{
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[33;40m*** Scope Warning Code: %ld ***\033[m\n", errorCode);
				Display(hp1428_msg_buf);
				sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[33;40m*** Scope Warning Message: %s ***\033[m\n", errorMessage);
				Display(hp1428_msg_buf);
				Sleep(5000);
			}
			errorHolder = errorCode;	
		}
	} while (errorCode != 0);
}

void formatAndWrite(int noErrorCheck, char *format, ...)
{
	char	tmpBuf[256];
	va_list	arglist;
	int rtncnt = 0;
	
	memset(tmpBuf, '\0', 256);
	va_start(arglist, format);
	vsprintf_s(tmpBuf, sizeof(tmpBuf), format, arglist);
	va_end(arglist);

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		sprintf_s(hp1428_msg_buf, sizeof(hp1428_msg_buf), "\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - formatAndWrite(%s)\033[m\n", tmpBuf);
		Display(hp1428_msg_buf);
		//MessageBox(0, "Scope WCEM", "formatAndWrite" , MB_OK);
	}

	atxml_WriteCmds( "DSO_1", tmpBuf, &rtncnt);	//WriteHPe1428AB

	return;
}

/*****************************************************************
 * Function to either alloc or delete alloc read buffer          *
 *****************************************************************/
int allocateDigScoprReadBuffer(int allocIt)
{	

	if ((IsSimOrDeb("DSO_1") & FLAG) == FLAG)
	{
		Display("\033[32;40m CEMDEBUG (SCOPE): [Dso_Scope_T] - allocateDigScoprReadBuffer()\033[m\n");
		//MessageBox(0, "Scope WCEM", "allocateDigScoprReadBuffer" , MB_OK);
	}
	
	if (allocIt == ALLOC && isAllocated == NOTDONE) 
	{
		if ((DigScopeRdBuf = (ViPBuf)malloc((size_t)WAVEBYTES)) != NULL) 
		{
			isAllocated = BEENDONE;
			return(0);
		}
		else 
		{
			return(-1);
		}
	}
	else if (allocIt == UNALLOC && isAllocated == BEENDONE) 
	{
		if (DigScopeRdBuf != NULL) 
		{
			isAllocated = NOTDONE;
			free(DigScopeRdBuf);
			DigScopeRdBuf = NULL;
		}
		return (0);
	}
	return(0);
}
