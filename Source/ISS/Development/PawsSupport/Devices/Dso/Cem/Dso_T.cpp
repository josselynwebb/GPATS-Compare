//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Dso_T.cpp
//
// Date:	    05-APR-06
//
// Purpose:	    Instrument Driver for Dso
//
// Instrument:	Dso  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     cem.lib              \usr\tyx\lib
//     cemsupport.lib       ..\..\Common\Lib
//
// ATLAS Subset: PAWS-85
//
//
//                          Function Number Map
//
// FNC                           Signal Description
// -------  -------------------------------------------------------------------------
// 101/201  sensor (freq) ac signal
// 102/202  sensor (period) ac signal
// 103/203  sensor (voltage-pp) ac signal
// 104/204  sensor (av-voltage) ac signal
// 105/205  sensor (voltage-p) ac signal
// 106/206  sensor (voltage) ac signal
// 107/207  sensor (voltage) dc signal
// 108/208  sensor (voltage-pp) pulsed dc
// 109/209  sensor (voltage-p-pos) pulsed dc
// 110/210  sensor (voltage-p-neg) pulsed dc
// 111/211  sensor (prf) pulsed dc
// 112/212  sensor (rise-time) pulsed dc
// 113/213  sensor (fall-time) pulsed dc
// 114/214  sensor (pulse-width) pulsed dc
// 115/215  sensor (duty-cycle) pulsed dc
// 116/216  sensor (overshoot) pulsed dc
// 117/217  sensor (preshoot) pulsed dc
// 118/218  sensor (neg-pulse-width) pulsed dc
// 119/219  sensor (pos-pulse-width) pulsed dc
// 120/220  sensor (voltage-p) pulsed dc
// 121/221  sensor (period) pulsed dc;
// 122/222  sensor (voltage-pp) ramp signal
// 123/223  sensor (freq) ramp signal
// 124/224  sensor (rise-time) ramp signal
// 125/225  sensor (fall-time) ramp signal
// 126/226  sensor (voltage-p) ramp signal
// 127/227  sensor (voltage-p-pos) ramp signal
// 128/228  sensor (voltage-p-neg) ramp signal
// 129/229  sensor (period) ramp signal
// 132/232  sensor (voltage-pp) square wave
// 133/233  sensor (freq) square wave
// 134/234  sensor (rise-time) square wave
// 135/235  sensor (fall-time) square wave
// 136/236  sensor (duty-cycle) square wave
// 137/237  sensor (voltage-p) square wave
// 138/238  sensor (voltage-p-pos) square wave
// 139/239  sensor (voltage-p-neg) square wave
// 140/240  sensor (period) square wave;
// 142/242  sensor (voltage-pp) triangular wave signal
// 143/243  sensor (freq) triangular wave signal
// 144/244  sensor (rise-time) triangular wave signal
// 145/245  sensor (fall-time) triangular wave signal
// 146/246  sensor (duty-cycle) triangular wave signal
// 147/247  sensor (voltage-p) triangular wave signal
// 148/248  sensor (voltage-p-pos) triangular wave signal
// 149/249  sensor (voltage-p-neg) triangular wave signal
// 150/250  sensor (period) triangular wave signal
// 152/252  sensor (save-wave) waveform
// 153/253  sensor (load-wave) waveform
// 154/254  sensor (compare-wave) waveform
// 155/255  sensor (math) waveform; add
// 156/1    sensor (math) waveform; subtract
// 157/2    sensor (math) waveform; multiply
// 158/3    sensor (math) waveform; differentiate
// 159/4    sensor (math) waveform; integrate
// 160/5    sensor(sample)waveform;
//
// Revision History
// Rev	     Date                  Reason						AUTHOR
// =======  =======  =======================================	=================
// 1.1.0.0  10/27/06 Initial Release							D. Bubenik, EADS North America Defense
// 1.1.0.1  11/07/06 Corrected the Bandwidth ATXML string, it	M. Hendricksen, EADS North America Defense
//					  was trying to send a string for an float 
//				      value causing to throw "unexception handle"
// 1.2.1.1  02/23/07 Corrected code in setup for ACS to send	M. Hendricksen, EADS North America Defense
//						over the Vpk no RMS peak.
// 1.4.0.1  07/03/07 Corrected code so external triggering     	D. Bubenik, EADS North America Defense
//						will function properly for waveform
//                      sample. PCR 168
// 1.4.0.2  08/09/07 Corrected code so xml return arrays		D. Bubenik, EADS North America Defense
//						are large enough. PCR 168
// 1.4.0.3  08/13/07 Corrected code so triggering would 		D. Bubenik, EADS North America Defense
//						function properly for waveform 
//                      sample. PCR 168
// 1.4.1.0  07/24/09 Corrected signal elements for STROBE-TO-EVENT EADS North America Defense
//                   for pulsed dc, square wave, ramp signal, 
//                   triangular wave signal, and waveform
// 1.4.2.0  05/24/11 Corrected DC SIGNAL 1641 formation         E. Larson, EADS 
// 1.4.3.0  05/27/11 Corrected Issuence of MAX TIME.
///////////////////////////////////////////////////////////////////////////////

//Hide 'truncated debug information' warnings
#pragma warning(disable : 4786)

// Includes
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Dso_T.h"
#include <map>
using namespace std;

// Local Defines

// Function codes

#define FNC_VOLTS_DC           1

#define CAL_TIME       (86400 * 365) /* one year */
#define SQRT_2         1.414213562373 //As defined 
#define SQRT_3         1.73205080808 //As defined

//Attribute Strings
#define ATTRIB_ACS_VOLT		"acs_volt"	//1-206
#define ATTRIB_ACS_VLPK		"acs_vlpk"	//1-205
#define ATTRIB_ACS_VLPP		"acs_vlpp"	//1-203
#define ATTRIB_ACS_FREQ		"acs_freq"	//1-201
#define ATTRIB_ACS_PERI		"acs_peri"	//1-202
#define ATTRIB_ACS_VLAV		"acs_valv"	//1-217

#define ATTRIB_DCS_VOLT		"dcs_volt"	//1-207

#define ATTRIB_PDC_VLPK		"pdc_vlpk"	//1-220
#define ATTRIB_PDC_PRFR		"pdc_prfr"	//1-211
#define ATTRIB_PDC_PERI		"pdc_peri"	//1-221
#define ATTRIB_PDC_RISE		"pdc_rise"	//1-212
#define ATTRIB_PDC_FALL		"pdc_fall"	//1-213
#define ATTRIB_PDC_PLWD		"pdc_plwd"	//1-214
#define ATTRIB_PDC_DUTY		"pdc_duty"	//1-215
#define ATTRIB_PDC_PSHT		"pdc_psht"	//1-217
#define ATTRIB_PDC_OVER		"pdc_over"	//1-216
#define ATTRIB_PDC_VLPP		"pdc_vlpp"	//1-208
#define ATTRIB_PDC_VPKP		"pdc_vpkp"	//1-209
#define ATTRIB_PDC_VPKN		"pdc_vpkn"	//1-210
#define ATTRIB_PDC_PPWT		"pdc_ppwt"	//1-219
#define ATTRIB_PDC_NPWT		"pdc_npwt"	//1-218

#define ATTRIB_SQW_VLPK		"sqw_vlpk"	//1-237
#define ATTRIB_SQW_VLPP		"sqw_vlpp"	//1-232
#define ATTRIB_SQW_VPKP		"sqw_vpkp"	//1-238
#define ATTRIB_SQW_VPKN		"sqw_vpkn"	//1-239
#define ATTRIB_SQW_FREQ		"sqw_freq"	//1-233
#define ATTRIB_SQW_PERI		"sqw_peri"	//1-240
#define ATTRIB_SQW_RISE		"sqw_rise"	//1-234
#define ATTRIB_SQW_FALL		"sqw_fall"	//1-235
#define ATTRIB_SQW_DUTY		"sqw_duty"	//1-236

#define ATTRIB_RPS_VLPK		"rps_vlpk"	//1-226
#define ATTRIB_RPS_VLPP		"rps_vlpp"	//1-222
#define ATTRIB_RPS_VPKP		"rps_vpkp"	//1-227
#define ATTRIB_RPS_VPKN		"rps_vpkn"	//1-228
#define ATTRIB_RPS_FREQ		"rps_freq"	//1-223
#define ATTRIB_RPS_PERI		"rps_peri"	//1-229
#define ATTRIB_RPS_RISE		"rps_rise"	//1-224
#define ATTRIB_RPS_FALL		"rps_fall"	//1-225

#define ATTRIB_TRI_VLPK		"tri_vlpk"	//1-247
#define ATTRIB_TRI_VLPP		"tri_vlpp"	//1-242
#define ATTRIB_TRI_VPKP		"tri_vpkp"	//1-248
#define ATTRIB_TRI_VPKN		"tri_vpkn"	//1-249
#define ATTRIB_TRI_FREQ		"tri_freq"	//1-243
#define ATTRIB_TRI_PERI		"tri_peri"	//1-250
#define ATTRIB_TRI_RISE		"tri_rise"	//1-244
#define ATTRIB_TRI_FALL		"tri_fall"	//1-245
#define ATTRIB_TRI_DUTY		"tri_duty"	//1-246

#define ATTRIB_WAV_SMPL		"wav_smpl"	//1-260
#define ATTRIB_WAV_MATH		"wav_math"	//1-2
#define ATTRIB_WAV_CMWV		"wav_cmwv"	//1-2
#define ATTRIB_WAV_LDVW		"wav_ldvm"	//1-2
#define ATTRIB_WAV_SVWV		"wav_svmv"	//1-2

#define ATTRIB_TMI_TIME		"tmi_time"	//1-2

#define ATTRIB_VOLTS_DC          "dc_ampl"
#define ATTRIB_VOLTS_AC          "ac_ampl"
#define ATTRIB_VOLTS_AC_P        "ac_ampl_p"
#define ATTRIB_VOLTS_AC_PP       "ac_ampl_pp"
#define ATTRIB_VOLTS_AC_R        "ac_ratio"
#define ATTRIB_IMP               "resistance"
#define ATTRIB_FREQ_AC           "ac_freq"
#define ATTRIB_CURR_AC           "ac_current"
#define ATTRIB_CURR_DC           "dc_current"
#define ATTRIB_PERIOD_AC         "ac_period"
#define ATTRIB_VOLTS_DC_R        "dc_ratio"
#define ATTRIB_VOLTS_DC_AV       "dc_ampl_av"
#define ATTRIB_VOLTS_AC_AV       "ac_ampl_av"
#define ATTRIB_CURR_DC_AV        "dc_current_av"
#define ATTRIB_CURR_AC_AV        "ac_current_av"
#define ATTRIB_AC_COMP_DC        "dc_ac_comp"
#define ATTRIB_AC_COMP_DC_F      "dc_ac_comp_f"
#define ATTRIB_DC_OFF_AC         "ac_dc_offset"

#define ATTRIB_VOLTS_SQ          "sq_ampl"
#define ATTRIB_VOLTS_SQ_P        "sq_ampl_p"
#define ATTRIB_VOLTS_SQ_PP       "sq_ampl_pp"
#define ATTRIB_FREQ_SQ           "sq_freq"
#define ATTRIB_PERIOD_SQ         "sq_period"

#define ATTRIB_VOLTS_TR          "tr_ampl"
#define ATTRIB_VOLTS_TR_P        "tr_ampl_p"
#define ATTRIB_VOLTS_TR_PP       "tr_ampl_pp"
#define ATTRIB_FREQ_TR           "tr_freq"
#define ATTRIB_PERIOD_TR         "tr_period"

// Static Variables
bool sFetchCalled;				// used to indicate in setup that it was called from fetch
								// further indicating not to send over "setup" just set up 
								// the signal string.
char sMeasChar[32];				// used to hold the modifier being measured
		
// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CDso_T(int Bus, int PrimaryAdr, int SecondaryAdr,
//                      int Dbg, int Sim)
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Bus              int             Bus number
// PrimaryAdr       int             Primary Address (MTA/MLA)
// SecondaryAdr     int             Secondary Address (MSA)
// Dbg              int             Debug flag value
// Sim              int             Simulation flag value (0/1)
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    Class instance.
//
///////////////////////////////////////////////////////////////////////////////
CDso_T::CDso_T(char *DeviceName, int Bus, int PrimaryAdr, int SecondaryAdr, int Dbg, int Sim)
{
    m_Bus = Bus;
    m_PrimaryAdr = PrimaryAdr;
    m_SecondaryAdr = SecondaryAdr;
    m_Dbg = Dbg;
    m_Sim = Sim;
    m_Handle= NULL;
    int Status = 0;

    if(DeviceName)
	{
        strcpy(m_DeviceName,DeviceName);
	}

    InitPrivateDso();
	NullCalDataDso();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - CDso_T(Dev [%s], Bus [%d], PrAddr [%d], Dbg [%d], Sim [%d])", DeviceName, Bus, PrimaryAdr, Dbg, Sim));

    // Initialize the Dso - not required in ATML mode
    // Check Cal Status and Resource Availability
    //Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CDso_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    Class instance destroyed.
//
///////////////////////////////////////////////////////////////////////////////
CDso_T::~CDso_T()
{
    // Reset the Dso
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - ~CDso_T()"));

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusDso(int Fnc)
//
// Purpose: Perform the Status action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDso_T::StatusDso(int Fnc)
{
    int Status = 0;

    // Status action for the Dso
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - StatusDso (Fnc [%d])", Fnc));

    // Check for any pending error messages
	if(Fnc < 50 || Fnc == 161 || Fnc == 162 || Fnc == 261 || Fnc == 262)
	{   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - StatusDso - EVENT FNC, no 1641 to send"));
    }
	else
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, m_SignalDescription, NULL, 0)));
		CEMDEBUG(9,cs_FmtMsg("[Dso_T] - StatusDso - IssueAtmlSignal(Status) Signal Desc [%s] Status [%lf]", m_SignalDescription, Status));
	}
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupDso_T(int Fnc)
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDso_T::SetupDso(int Fnc)
{
    int  Status = 0;
	double  dTemp = 0, dTemp2 = 0, dTemp3 = 0;
	char stringinput[1024];
	char tempstring[512];
	//char string[1024];
	char sChan[16] = "";
	double dBandMin = -1, dBandMax = -1;
	double rise=0, fall=0, period=0;
	bool duty=false;
	short measchar;
	

	const double RMS_PK = 1.414213562373;
	const double RMS_PP = 2 * RMS_PK;
    
	m_Noun = GetCurNoun();
	m_MeasChar = GetCurMChar();

    // Setup action for the Dso
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - SetupDso (Fnc [%d]) - Noun [%d], MeasChar [%d]", Fnc, m_Noun, m_MeasChar));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0) return(0);
    
	// Acquiring the information from the Atlas
    if((Status = GetStmtInfoDso(Fnc)) != 0) return(0);

	if((Fnc>5 && Fnc < 50) || Fnc == 161 || Fnc == 162 || Fnc == 6 || Fnc == 7)
	{   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - EVENT FNC, no 1641 to send"));
    }
	else
	{

		switch(m_Noun)
		{
			//***AC SIGNAL***/////////////////////////////////////////////////////////////////////////////////////////////
			case N_ACS:
			{
		        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found ATLAS NOUN [AC SIGNAL]"));

				// Creating the IEEE1641 string
				sprintf(stringinput, "<Signal name=\"DSO_AC_SIGNAL\" Out=\"Measure\" In=\"CHA CHB EXT INT\">\n");
				
				// 1) AC Signal component
				// Modifier value: Voltage related, frequency related
				strcat(stringinput, "<Sinusoid name=\"AC_COMP\"");
				
				if(m_VlppMax.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_VlppMax.Real);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPP Max [%3.3lf]", m_VlppMax.Real));
				}
				else if (m_VlpkMax.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_VlpkMax.Real * 2);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPk Max [%3.3lf]", m_VlpkMax.Real * 2));
				}
				else if (m_VlavMax.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_VlavMax.Real);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VAv Max [%3.3lf]", m_VlavMax.Real));
				}
				else if (m_VoltMax.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_VoltMax.Real);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Volts Max [%3.3lf]", m_VoltMax.Real));
				}
				else if (m_Vlpp.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_Vlpp.Real);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPP [%3.3lf]", m_Vlpp.Real));
				}
				else if (m_Vlpk.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_Vlpk.Real * 2);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPk [%3.3lf]", m_Vlpk.Real * 2));
				}
				else if (m_Vlav.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_Vlav.Real);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VAv [%3.3lf]", m_Vlav.Real));
				}
				else if (m_Volt.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_Volt.Real);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Volts [%3.3lf]", m_Volt.Real));
				}
				else if (m_VlppMin.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_VlppMin.Real);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPP Min [%3.3lf]", m_VlppMin.Real));
				}
				else if (m_VlpkMin.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_VlpkMin.Real * 2);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPk Min [%3.3lf]", m_VlpkMin.Real * 2));
				}
				else if (m_VlavMin.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_VlavMin.Real);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VAv Min [%3.3lf]", m_VlavMin.Real));
				}
				else if (m_VoltMin.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", m_VoltMin.Real);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Volts Min [%3.3lf]", m_VoltMin.Real));
				}

				if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists ||	m_VlpkMax.Exists || m_Vlpk.Exists || m_VlpkMin.Exists || 
				m_Vlav.Exists || m_VlavMax.Exists || m_VlavMin.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMax.Exists)
				{
					strcat(stringinput,tempstring);
				}

				if(m_FreqMax.Exists)
				{
					sprintf(tempstring, " frequency=\"%10.2lfHz\"", m_FreqMax.Real);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Freq Max [%10.2lf]", m_FreqMax.Real));
				}
				else if (m_PeriMin.Exists && m_PeriMin.Real != 0)
				{
					sprintf(tempstring, " frequency=\"%10.2lfHz\"", 1/m_PeriMin.Real);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Period Min [%10.2lf]", 1/m_PeriMin.Real));
				}
				else if(m_Freq.Exists)
				{
					sprintf(tempstring, " frequency=\"%10.2lfHz\"", m_Freq.Real);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Freq [%10.2lf]", m_Freq.Real));
				}
				else if (m_Peri.Exists && m_Peri.Real != 0)
				{
					sprintf(tempstring, " frequency=\"%10.2lfHz\"", 1/m_Peri.Real);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Period [%10.2lf]", 1/m_Peri.Real));
				}
				else if(m_FreqMin.Exists)
				{
					sprintf(tempstring, " frequency=\"%10.2lfHz\"", m_FreqMin.Real);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Freq Min [%10.2lf]", m_FreqMin.Real));
				}
				else if (m_PeriMax.Exists && m_PeriMax.Real != 0)
				{
					sprintf(tempstring, " frequency=\"%10.2lfHz\"", 1/m_PeriMax.Real);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Period Max [%10.2lf]", 1/m_PeriMax.Real));
				}

				if(m_FreqMax.Exists || m_Freq.Exists || m_FreqMin.Exists ||	m_PeriMax.Exists || m_Peri.Exists || m_PeriMin.Exists)
				{
					strcat(stringinput,tempstring);
				}

				strcat(stringinput,"/>\n");    // BSC terminator

				// 2) Constant component
				// Modifier value: dc-offset (default: 0)
				if(m_Dcof.Exists)
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\"/>\n", m_Dcof.Real);
					strcat(stringinput,tempstring);
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found DC Offset [%3.3lf]", m_Dcof.Real));
				}
				else
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\"/>\n");
					strcat(stringinput,tempstring);
				}

				// 3) Adding AC Signal and Constant components
				sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\"/>\n");
				strcat(stringinput,tempstring);

				// 4) Filters + Coupling
				CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Channel [%d]", m_Channumb));

				if (m_Channumb == 2)
				{
					sprintf(sChan, "CHB");
				}
				else
				{
					sprintf(sChan, "CHA");
				}

				if(m_BandMax.Exists)
				{
					dBandMax = m_BandMax.Real;
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Bandwidth Max [%3.3lf]", m_BandMax.Real));
				}
				else if(m_FrqwMax.Exists)
				{
					dBandMax = m_FrqwMax.Real;
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Freq Window Max [%3.3lf]", m_FrqwMax.Real));
				}
				else
				{
					dBandMax = 20e+9;
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Defaulted BandWidth Max to [%3.3lf]", dBandMax));
				}

				//if (m_BandMin.Exists || m_FrqwMin.Exists)
				sprintf(tempstring, "<LowPass name=\"LowPass\" cutoff=\"%3.3lfHZ\" In=\"%s\"/>\n", dBandMax, sChan);
				strcat(stringinput,tempstring);

				// If AC coupling and HigPass both exist, send error
				if (m_BandMin.Exists && m_Cplg.Exists)
				{
					CEMERROR(EB_SEVERITY_ERROR|EB_ACTION_HALT, "AC COUPLE and BANDWIDTH/FREQ-WINDOW Min cannot coexist in this IEEE1641 model\n");
					return (-1);
				}

				if(m_BandMin.Exists)
				{
					dBandMin = m_BandMin.Real;
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Bandwidth Min [%3.3lf]", m_BandMin.Real));
				}
				else if(m_FrqwMin.Exists)
				{
					dBandMin = m_FrqwMin.Real;
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Freq Window Min [%3.3lf]", m_FrqwMin.Real));
				}
				else if (m_Cplg.Exists)
				{
					dBandMin = 0.0;
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Defaulted BandWidth Min to [%3.3lf]", dBandMin));
				}

				if (m_BandMin.Exists || m_FrqwMin.Exists || m_Cplg.Exists)
				{
					sprintf(tempstring, "<HighPass name=\"HighPass\" cutoff=\"%3.3lfHZ\" In=\"LowPass\"/>\n", dBandMin);
					strcat(stringinput,tempstring);
				}

				// 5) Attenuation
				int nGain = 0;

				if((m_Volt.Exists && m_Volt.Real > 5.0) || (m_VoltMax.Exists && m_VoltMax.Real > 5.0) || (m_VoltMin.Exists && m_VoltMin.Real > 5.0))
				{
					nGain = 10;
				}
				else
				{
					nGain = 0;
				}

				CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Attenuation Set to [%d]", nGain));

				if (m_BandMin.Exists || m_FrqwMin.Exists || m_Cplg.Exists)
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%d\" In=\"HighPass\"/>\n", nGain);
				}
				else
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%d\" In=\"LowPass\"/>\n", nGain);
				}

				strcat(stringinput,tempstring);

				// 6) Load
				sprintf(tempstring, "<Load name=\"Load\" resistance=\"%3.3lfOHM\" In=\"Attenuator\"/>\n", m_Timp.Real);
				strcat(stringinput,tempstring);
				CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Impedance [%3.3lf]", m_Timp.Real));
				
				// 8a)Trigger information
				if (m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					if(m_Trdl.Exists)
					{
						sprintf(tempstring, "<SignalDelay name=\"Trigger_Start\" delay=\"%gs\" In=\"Trigger\"/>", m_Trdl.Real);
						strcat(stringinput,tempstring);
						sprintf(tempstring, "<Instantaneous name=\"Trigger\"");
						strcat(stringinput,tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Delay [%3.3lf]", m_Trdl.Real));
					}
					else
					{
						sprintf(tempstring, "<Instantaneous name=\"Trigger_Start\"");
						strcat(stringinput,tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - No Trigger Delay Specified"));
					}

					if (m_Trlv.Exists)
					{
						sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Trlv.Real);
						strcat(stringinput,tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Level [%3.3lf]", m_Trlv.Real));
					}

					if (m_Trsl.Exists)
					{
						if(m_Trsl.Int == +1)
						{
							sprintf(tempstring, " condition=\"GT\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Slope [Pos]"));
						}
						else if (m_Trsl.Int == -1)
						{
							sprintf(tempstring, " condition=\"LT\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Slope [Neg]"));
						}

						strcat(stringinput,tempstring);
					}

					if (m_Trgs.Exists)
					{
						if (m_Trgs.Int == 1)
						{
							sprintf(tempstring, " In=\"CHA\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [%d]", m_Trgs.Int));
						}
						
						if (m_Trgs.Int == 2)
						{
							sprintf(tempstring, " In=\"CHB\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [%d]", m_Trgs.Int));
						}

						if (m_Trgs.Int == 3)
						{
							sprintf(tempstring, " In=\"EXT\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [Ext]"));
						}

						if (m_Trgs.Int == 4)
						{
							sprintf(tempstring, " In=\"INT\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [Int]"));
						}

						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, " In=\"%s\"", sChan);
						strcat(stringinput, tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [%s]", sChan));
					}

					strcat(stringinput,"/>\n");
				}

				// 8b) Measure
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Signal\" In=\"Load\"");
				strcat(stringinput,tempstring);
				
				// Sample-count
				if(m_Scnt.Exists)
				{
					sprintf(tempstring, " samples=\"%d\"", m_Scnt.Int);
					strcat(stringinput,tempstring);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Sample Count [%d]", m_Scnt.Int));
				}

				// If Trigger information
				if(m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					sprintf(tempstring, " Sync=\"Trigger_Start\"");
					strcat(stringinput,tempstring);
				}

				// Meas. char.
				measchar = GetCurDevMChar();
				switch(measchar)	//add attribute to measure statement
				{
					case M_FREQ:					
						// Freq
						strcpy(sMeasChar, ATTRIB_ACS_FREQ);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_ACS_FREQ);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Measurement Characteristic [FREQ]"));
						break;
					case M_PERI:					
						// Period
						strcpy(sMeasChar, ATTRIB_ACS_PERI);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_ACS_PERI);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Measurement Characteristic [PERIOD]"));
						break;
					case M_VLPP:
						// Voltage-pp
						strcpy(sMeasChar, ATTRIB_ACS_VLPP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_ACS_VLPP);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Measurement Characteristic [VOLTS-PP]"));
						break;
					case M_VLAV:
						// av-voltage
						strcpy(sMeasChar, ATTRIB_ACS_VLAV);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_ACS_VLAV);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Measurement Characteristic [AV-VOLTS]"));
						break;
					case M_VLPK:
						//voltage-p
						strcpy(sMeasChar, ATTRIB_ACS_VLPK);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_ACS_VLPK);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Measurement Characteristic [VOLTS-P]"));
						break;
					case M_VOLT:
						// voltage
						strcpy(sMeasChar, ATTRIB_ACS_VOLT);
						sprintf(tempstring, " attribute=\"%s\" ",ATTRIB_ACS_VOLT);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Measurement Characteristic [VOLTS]"));
						break;
				}

				strcat(stringinput,tempstring);

				// 10) End of AC SIGNAL
				sprintf(tempstring,"/>\n</Signal>\n");
				strcat(stringinput,tempstring);
			}

			break;

			//***DC SIGNAL***////////////////////////////////////////////////////////////////////////////////////////////
			case N_DCS:
			{
				CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found ATLAS NOUN [DC SIGNAL]"));

				// Creating the IEEE1641 string
				strcpy(stringinput, "<Signal name=\"DSO_DC_SIGNAL\" Out=\"Measure\" In=\"CHA CHB EXT INT\">\n");

				// 1) DC Signal component
				// Modifier value: Voltage related, frequency related
				strcat(stringinput, "\t<Constant name=\"DC_COMP\"");
				
				if(m_VoltMax.Exists)
				{
					dTemp = m_VoltMax.Real;
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VOLTS Max [%3.3lf]", m_VoltMax.Real));
				}
				else if (m_VoltMin.Exists)
				{
					dTemp = m_VoltMin.Real;
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VOLTS Min [%3.3lf]", m_VoltMin.Real));
				}
				else if (m_Volt.Exists)
				{
					dTemp = m_Volt.Real;
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VOLTS [%3.3lf]", m_Volt.Real));
				}
				else if (m_VlppMin.Exists)  // Is this case every going to be true???? - JLW
				{
					dTemp = m_VoltMin.Real;
			        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VOLTS Min [%3.3lf]", m_VoltMin.Real));
				}
				else
				{
					INSTERROR(EB_SEVERITY_ERROR|EB_ACTION_HALT, "[Dso_T] - SetupDso - No Voltage information for DC SIGNAL");	// !!! Error
					return 0;
				}

				if(m_VoltMax.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", dTemp);
					strcat(stringinput,tempstring);
				}
				
				strcat(stringinput,"/>\n");    // BSC terminator
				
				// Channel
				CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Channel [%d]", m_Channumb));

				if (m_Channumb == 2)
				{
					sprintf(sChan, "CHB");
				}
				else
				{
					sprintf(sChan, "CHA");
				}

				// 2) Load
				sprintf(tempstring, "\t<Load name=\"Load\" resistance=\"%3.3lfOHM\" In=\"%s\"/>\n", m_Timp.Real, sChan);
				strcat(stringinput, tempstring);
				CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Impedance [%3.3lf]", m_Timp.Real));
				
				// 2.5) Coupling
				if (m_Cplg.Exists)
				{
					dBandMin = 0.0;
					sprintf(tempstring, "\t<HighPass name=\"HighPass\" cutoff=\"%3.3lfHZ\" In=\"LowPass\"/>\n", dBandMin);
					strcat(stringinput,tempstring);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Defaulted BandWidth Min to [%3.3lf]", dBandMin));
				}
				
				// STR10030
				// 2.6)Trigger information
				if (m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					if(m_Trdl.Exists)
					{
						sprintf(tempstring, "\t<SignalDelay name=\"Trigger_Start\" delay=\"%gs\" In=\"Trigger\"/>", m_Trdl.Real);
						strcat(stringinput,tempstring);
						sprintf(tempstring, "\t<Instantaneous name=\"Trigger\"");
						strcat(stringinput,tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Delay [%3.3lf]", m_Trdl.Real));
					}
					else
					{
						sprintf(tempstring, "\t<Instantaneous name=\"Trigger_Start\"");
						strcat(stringinput,tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - No Trigger Delay Specified"));
					}
				
					if (m_Trlv.Exists)
					{
						sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Trlv.Real);
						strcat(stringinput,tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Level [%3.3lf]", m_Trlv.Real));
					}
					
					if (m_Trsl.Exists)
					{
						if(m_Trsl.Int == +1)
						{
							strcpy(tempstring, " condition=\"GT\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Slope [Pos]"));
						}
						else if (m_Trsl.Int == -1)
						{
							strcpy(tempstring, " condition=\"LT\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Slope [Neg]"));
						}

						strcat(stringinput,tempstring);
					}

					if (m_Trgs.Exists)
					{
						if (m_Trgs.Int == 1)
						{
							sprintf(tempstring, " In=\"CHA\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [%d]", m_Trgs.Int));
						}

						if (m_Trgs.Int == 2)
						{
							sprintf(tempstring, " In=\"CHB\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [%d]", m_Trgs.Int));
						}

						if (m_Trgs.Int == 3)
						{
							sprintf(tempstring, " In=\"EXT\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [Ext]"));
						}

						if (m_Trgs.Int == 4)
						{
							sprintf(tempstring, " In=\"INT\"");
							CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [Int]"));
						}

						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, " In=\"%s\"", sChan);
						strcat(stringinput, tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Trigger Source Chan [%s]", sChan));
					}
					
					strcat(stringinput, "/>\n");
				}

				// 3) Measure
				if (m_Cplg.Exists)
				{
					strcat(stringinput, "\t<Measure name=\"Measure\" As=\"DC_COMP\" In=\"HighPass\"");
				}
				else
				{
					strcat(stringinput, "\t<Measure name=\"Measure\" As=\"DC_COMP\" In=\"Load\"");
				}

				//STR10030
				// If Trigger information
				if(m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					strcpy(tempstring, " Sync=\"Trigger_Start\"");
					strcat(stringinput,tempstring);
				}
				
				if(m_Scnt.Exists)
				{
					sprintf(tempstring, " samples=\"%d\"", m_Scnt.Int);
					strcat(stringinput,tempstring);
					CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Sample Count [%d]", m_Scnt.Int));
				}
				
				//get the measured character
				measchar = GetCurDevMChar();

				switch(measchar)	//add attribute to measure statement
				{
					case M_VOLT:
						// voltage
						strcpy(sMeasChar, ATTRIB_DCS_VOLT);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_DCS_VOLT);
						strcat(stringinput,tempstring);
						CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Measurement Characteristic [VOLTS]"));
						break;
				}
				
				// 4) End of DC SIGNAL
				sprintf(tempstring,"/>\n</Signal>\n");
				strcat(stringinput, tempstring);
			}
				
			break;

			//***PULSED DC***////////////////////////////////////////////////////////////////////////////////////////////
			case N_PDC:
			{
		        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found ATLAS NOUN [PULSED DC]"));

				// Creating the IEEE1641 string
				sprintf(stringinput, "<Signal name=\"DSO_PULSED_DC\" Out=\"Measure\" In=\"CHA CHB EXT INT\">\n");
			
				// 1) PULSED DC component
				// Modifier value: Voltage related, frequency related
				strcat(stringinput, "<Trapezoid name=\"AC_COMP\"");
				
				// Sweeptime
				if(m_PpwtMax.Exists)
				{
					dTemp = m_PpwtMax.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found POS Pulsewidth Max [%3.3lf]", m_PpwtMax.Real));
				}
				else if(m_Ppwt.Exists)
				{
					dTemp = m_Ppwt.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found POS Pulsewidth [%3.3lf]", m_Ppwt.Real));
				}
				else if(m_PpwtMin.Exists)
				{
					dTemp = m_PpwtMin.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found POS Pulsewidth Min [%3.3lf]", m_PpwtMin.Real));
				}
				else if(m_NpwtMax.Exists)
				{
					dTemp = m_NpwtMax.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found NEG Pulsewidth Max [%3.3lf]", m_NpwtMax.Real));
				}
				else if(m_Npwt.Exists)
				{
					dTemp = m_Npwt.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found NEG Pulsewidth [%3.3lf]", m_Npwt.Real));
				}
				else if(m_NpwtMin.Exists)
				{
					dTemp = m_NpwtMin.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found NEG Pulsewidth Min [%3.3lf]", m_NpwtMin.Real));
				}
				else if(m_PlwdMax.Exists)
				{
					dTemp = m_PlwdMax.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Pulsewidth Max [%3.3lf]", m_PlwdMax.Real));
				}
				else if(m_Plwd.Exists)
				{
					dTemp = m_Plwd.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Pulsewidth [%3.3lf]", m_Plwd.Real));
				}
				else if(m_PlwdMin.Exists)
				{
					dTemp = m_PlwdMin.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Pulsewidth Min [%3.3lf]", m_PlwdMin.Real));
				}

				if(m_PpwtMax.Exists || m_Ppwt.Exists || m_PpwtMin.Exists || m_NpwtMax.Exists || m_Npwt.Exists || 
				m_NpwtMin.Exists || m_PlwdMax.Exists || m_Plwd.Exists || m_PlwdMin.Exists)
				{
					sprintf(tempstring, " pulseWidth=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}


				if(m_RiseMax.Exists)
				{
					dTemp = m_RiseMax.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Risetime Max [%3.3lf]", m_RiseMax.Real));
				}
				else if(m_Rise.Exists)
				{
					dTemp = m_Rise.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Risetime [%3.3lf]", m_Rise.Real));
				}
				else if(m_RiseMin.Exists)
				{
					dTemp = m_RiseMin.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Risetime Min [%3.3lf]", m_RiseMin.Real));
				}

				if(m_RiseMax.Exists || m_Rise.Exists || m_RiseMin.Exists)
				{
					sprintf(tempstring, " riseTime=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}


				if(m_FallMax.Exists)
				{
					dTemp = m_FallMax.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Falltime Max [%3.3lf]", m_FallMax.Real));
				}
				else if(m_Fall.Exists)
				{
					dTemp = m_Fall.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Falltime [%3.3lf]", m_Fall.Real));
				}
				else if(m_FallMin.Exists)
				{
					dTemp = m_FallMin.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found Falltime Min [%3.3lf]", m_FallMin.Real));
				}

				if(m_FallMax.Exists || m_Fall.Exists || m_FallMin.Exists)
				{
					sprintf(tempstring, " fallTime=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}

				// Algorithm for amplitude (subject to change)
				if (m_VlppMax.Exists)
				{
					dTemp = m_VlppMax.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPP Max [%3.3lf]", m_VlppMax.Real));
				}
				else if (m_Vlpp.Exists)
				{
					dTemp = m_Vlpp.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPP [%3.3lf]", m_Vlpp.Real));
				}
				else if (m_VlppMin.Exists)
				{
					dTemp = m_VlppMin.Real;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPP Min [%3.3lf]", m_VlppMin.Real));
				}
				else if (m_VlpkMax.Exists)
				{
					dTemp = m_VlpkMax.Real * 2;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPk Max [%3.3lf]", m_VlpkMax.Real * 2));
				}
				else if (m_Vlpk.Exists)
				{
					dTemp= m_Vlpk.Real * 2;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPk [%3.3lf]", m_Vlpk.Real * 2));
				}
				else if (m_VlpkMin.Exists)
				{
					dTemp = m_VlpkMin.Real * 2;
				    CEMDEBUG(5, cs_FmtMsg("[Dso_T] - SetupDso - Found VPk Min [%3.3lf]", m_VlpkMin.Real * 2));
				}
				else if (m_VpkpMax.Exists)
				{
					dTemp = m_VpkpMax.Real * 2;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found VPk-POS Max %lf", m_VpkpMax.Real * 2));
				}
				else if (m_Vpkp.Exists)
				{
					dTemp= m_Vpkp.Real * 2;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found VPk-POS %lf", m_Vpkp.Real * 2));
				}
				else if (m_VpkpMin.Exists)
				{
					dTemp = m_VpkpMin.Real * 2;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found VPk-POS Min %lf", m_VpkpMin.Real * 2));
				}
				else if (m_VpknMax.Exists)
				{
					dTemp = m_VpknMax.Real * 2;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found VPk-NEG Max %lf", m_VpknMax.Real * 2));
				}
				else if (m_Vpkn.Exists)
				{
					dTemp= m_Vpkn.Real * 2;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found VPk-NEG %lf", m_Vpkn.Real * 2));
				}
				else if (m_VpknMin.Exists)
				{
					dTemp = m_VpknMin.Real * 2;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found VPk-NEG Min %lf", m_VpknMin.Real * 2));
				}
				else if (m_VoltMax.Exists)
				{
					dTemp = m_VoltMax.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Volt Max %lf", m_VoltMax.Real));
				}
				else if (m_Volt.Exists)
				{
					dTemp = m_Volt.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Volt %lf", m_Volt.Real));
				}
				else if (m_VoltMin.Exists)
				{
					dTemp = m_VoltMin.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Volt Min %lf", m_VoltMin.Real));
				}
				
				if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
				m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
				{
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", dTemp);
					strcat(stringinput,tempstring);
				}
				

				if(m_PrfrMax.Exists)
				{
					dTemp = 1/m_PrfrMax.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Prf Max [%lf]", 1/m_PrfrMax.Real));
				}
				else if(m_Prfr.Exists)
				{
					dTemp = 1/m_Prfr.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Prf [%lf]", 1/m_Prfr.Real));
				}
				else if(m_PrfrMin.Exists)
				{
					dTemp = 1/m_PrfrMin.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Prf Min [%lf]", 1/m_PrfrMin.Real));
				}
				else if(m_PeriMax.Exists)
				{
					dTemp = m_PeriMax.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Period Max [%lf]", m_PeriMax.Real));
				}
				else if(m_Peri.Exists)
				{
					dTemp = m_Peri.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Period [%lf]", m_Peri.Real));
				}
				else if(m_PeriMin.Exists)
				{
					dTemp = m_PeriMin.Real;
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - Found Period Min [%lf]", m_PeriMin.Real));
				}
						
				if(m_PrfrMax.Exists || m_Prfr.Exists || m_PrfrMin.Exists ||	m_PeriMax.Exists || m_Peri.Exists || m_PeriMin.Exists)
				{
					sprintf(tempstring, " period=\"%GS\"", dTemp);
					strcat(stringinput,tempstring);
				}
				
				strcat(stringinput,"/>\n");    // BSC terminator

				// 2) Constant component
				// Modifier value: dc-offset (default: 0)
				if(m_Dcof.Exists)
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\"/>\n", m_Dcof.Real);
					strcat(stringinput,tempstring);
				}
				else
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\"/>\n");
					strcat(stringinput,tempstring);
				}

				// 3) Adding AC Signal and Constant components
				sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\"/>\n");
				strcat(stringinput,tempstring);
				
				// 4) Coupling
				if (m_Channumb == 2)
				{
					sprintf(sChan, "CHB");
				}
				else
				{
					sprintf(sChan, "CHA");
				}

				if (m_Cplg.Exists)
				{
					sprintf(tempstring, "<HighPass name=\"HighPass\" In=\"%s\"/>\n", sChan);
					strcat(stringinput,tempstring);
				}

				// 5) Attenuation
				if(m_Volt.Exists && m_Volt.Real > 5.0)
				{
					dTemp = 10.0;
				}
				else
				{
					dTemp = 0.0;
				}

				if (m_Cplg.Exists)
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"HighPass\"/>\n", dTemp);
				}
				else
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"%s\"/>\n", dTemp, sChan);
				}

				strcat(stringinput,tempstring);
				
				// 6) Load
				sprintf(tempstring, "<Load name=\"Load\" resistance=\"%3.3lfOHM\" In=\"Attenuator\"/>\n", m_Timp.Real);
				strcat(stringinput,tempstring);
				
				// 8a)Trigger information
				if (m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					if(m_Trdl.Exists)
					{
						sprintf(tempstring, "<SignalDelay name=\"Trigger_Start\" delay=\"%gs\" In=\"Trigger\"/>", m_Trdl.Real);
						strcat(stringinput,tempstring);
						sprintf(tempstring, "<Instantaneous name=\"Trigger\"");
						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, "<Instantaneous name=\"Trigger_Start\"");
						strcat(stringinput,tempstring);
					}
					
					if (m_Trlv.Exists)
					{
						sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Trlv.Real);
						strcat(stringinput,tempstring);
					}
					
					if (m_Trsl.Exists)
					{
						if(m_Trsl.Int == +1)
						{
							sprintf(tempstring, " condition=\"GT\"");
						}
						else if (m_Trsl.Int == -1)
						{
							sprintf(tempstring, " condition=\"LT\"");
						}

						strcat(stringinput,tempstring);
					}
					
					if (m_Trgs.Exists)
					{
						if (m_Trgs.Int == 1)
						{
							sprintf(tempstring, " In=\"CHA\"");
						}
						if (m_Trgs.Int == 2)
						{
							sprintf(tempstring, " In=\"CHB\"");
						}
						if (m_Trgs.Int == 3)
						{
							sprintf(tempstring, " In=\"EXT\"");
						}
						if (m_Trgs.Int == 4)
						{
							sprintf(tempstring, " In=\"INT\"");
						}

						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, " In=\"%s\"", sChan);
						strcat(stringinput, tempstring);
					}

					strcat(stringinput,"/>\n");
				}
				
				// 8b) Measure
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Signal\" In=\"Load\"");
				strcat(stringinput,tempstring);
				
				// Sample-count
				if(m_Scnt.Exists)
				{
					sprintf(tempstring, " samples=\"%d\"", m_Scnt.Int);
					strcat(stringinput,tempstring);
				}
				
				// If Trigger information
				if(m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					sprintf(tempstring, " Sync=\"Trigger_Start\"");
					strcat(stringinput,tempstring);
				}
				
				// Meas. char.
				//get the measured character
				measchar = GetCurDevMChar();

				switch(measchar)	//add attribute to measure statement
				{
					case M_VLPP:
						// voltage-pp
						strcpy(sMeasChar, ATTRIB_PDC_VLPP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_VLPP);
						break;
					case M_VPKP:
						// voltage-p-pos
						strcpy(sMeasChar, ATTRIB_PDC_VPKP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_VPKP);
						break;
					case M_VPKN:
						// voltage-p-neg
						strcpy(sMeasChar, ATTRIB_PDC_VPKN);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_VPKN);
						break;
					case M_PRFR:
						// prf
						strcpy(sMeasChar, ATTRIB_PDC_PRFR);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_PRFR);
						break;
					case M_RISE:
						// rise-time
						strcpy(sMeasChar, ATTRIB_PDC_RISE);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_RISE);
						break;
					case M_FALL:
						// fall-time
						strcpy(sMeasChar, ATTRIB_PDC_FALL);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_FALL);
						break;
					case M_PLWD:
						// pulse-width
						strcpy(sMeasChar, ATTRIB_PDC_PLWD);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_PLWD);
						break;
					case M_DUTY:
						// duty-cycle
						strcpy(sMeasChar, ATTRIB_PDC_DUTY);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_DUTY);
						break;
					case M_OVER:
						// overshoot
						strcpy(sMeasChar, ATTRIB_PDC_OVER);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_OVER);
						break;
					case M_PSHT:
						// preshoot
						strcpy(sMeasChar, ATTRIB_PDC_PSHT);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_PSHT);
						break;
					case M_NPWT:
						// neg-pulse-width
						strcpy(sMeasChar, ATTRIB_PDC_NPWT);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_NPWT);
						break;
					case M_PPWT:
						// pos-pulse-width
						strcpy(sMeasChar, ATTRIB_PDC_PPWT);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_PPWT);
						break;
					case M_VLPK:
						// voltage-p
						strcpy(sMeasChar, ATTRIB_PDC_VLPK);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_VLPK);
						break;
					case M_PERI:
						// period
						strcpy(sMeasChar, ATTRIB_PDC_PERI);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_PDC_PERI);
						break;
				}

				strcat(stringinput, tempstring);
				
				// 10) End of PULSE DC
				sprintf(tempstring,"/>\n</Signal>\n");
				strcat(stringinput,tempstring);
			}
			break;

			//***SQUARE WAVE***////////////////////////////////////////////////////////////////////////////////////////////
			case N_SQW:
			{
				// Creating the IEEE1641 string
				sprintf(stringinput, "<Signal Name=\"DSO_SQUARE_WAVE\" Out=\"Measure\" In=\"CHA CHB EXT INT\">\n");
			
				if(m_Rise.Exists || m_RiseMax.Exists || m_RiseMin.Exists || m_Fall.Exists || m_FallMin.Exists || m_FallMax.Exists) //use trapazoid
				{
					// 1) TRAPAZOID WAVE component
					// Modifier value: Voltage related, frequency related
					strcat(stringinput, "<Trapezoid name=\"AC_COMP\"");

					// Algorithm for amplitude (subject to change)
					if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
					m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
					{
						if (m_VlppMax.Exists)
						{
							dTemp = m_VlppMax.Real;  // was div by 2 ?? - JLW
						}
						else if (m_Vlpp.Exists)
						{
							dTemp = m_Vlpp.Real;  // was div by 2
						}
						else if (m_VlppMin.Exists)
						{
							dTemp = m_VlppMin.Real;  // was div by 2
						}
						else if (m_VlpkMax.Exists)
						{
							dTemp = m_VlpkMax.Real * 2;
						}
						else if (m_Vlpk.Exists)
						{
							dTemp= m_Vlpk.Real * 2;
						}
						else if (m_VlpkMin.Exists)
						{
							dTemp = m_VlpkMin.Real * 2;
						}
						else if (m_VpkpMax.Exists)
						{
							dTemp = m_VpkpMax.Real * 2;
						}
						else if (m_Vpkp.Exists)
						{
							dTemp= m_Vpkp.Real * 2;
						}
						else if (m_VpkpMin.Exists)
						{
							dTemp = m_VpkpMin.Real * 2;
						}
						else if (m_VpknMax.Exists)
						{
							dTemp = m_VpknMax.Real * 2;
						}
						else if (m_Vpkn.Exists)
						{
							dTemp= m_Vpkn.Real * 2;
						}
						else if (m_VpknMin.Exists)
						{
							dTemp = m_VpknMin.Real * 2;
						}
						else if (m_VoltMax.Exists)
						{
							dTemp = m_VoltMax.Real;
						}
						else if (m_Volt.Exists)
						{
							dTemp = m_Volt.Real;
						}
						else if (m_VoltMin.Exists)
						{
							dTemp = m_VoltMin.Real;
						}
					}

					if (m_OverMax.Exists || m_Over.Exists || m_OverMin.Exists)
					{
						if (m_OverMax.Exists)
						{
							dTemp2 = m_OverMax.Real;
						}
						else if (m_Over.Exists)
						{
							dTemp2 = m_Over.Real;
						}
						else if (m_OverMin.Exists)
						{
							dTemp2 = m_OverMin.Real;
						}
					}

					if (m_PshtMax.Exists || m_Psht.Exists || m_PshtMin.Exists)
					{
						if (m_PshtMax.Exists)
						{
							dTemp3 = m_PshtMax.Real;
						}
						else if (m_Psht.Exists)
						{
							dTemp3 = m_Psht.Real;
						}
						else if (m_PshtMin.Exists)
						{
							dTemp3 = m_PshtMin.Real;
						}
					}

					dTemp *= ((dTemp2>dTemp3)?(dTemp2/100+1):(dTemp3/100+1));
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", dTemp);					
					
					if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
					m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMax.Exists)
					{
						strcat(stringinput,tempstring);
					}
					
					if(m_PeriMax.Exists)
					{
						dTemp = m_PeriMax.Real/2;
					}
					else if(m_Peri.Exists)
					{
						dTemp = m_Peri.Real/2;
					}
					else if(m_PeriMin.Exists)
					{
						dTemp = m_PeriMin.Real/2;
					}
					else if(m_FreqMax.Exists)
					{
						dTemp = 1/m_FreqMax.Real/2;
					}
					else if(m_Freq.Exists)
					{
						dTemp = 1/m_Freq.Real/2;
					}
					else if(m_FreqMin.Exists)
					{
						dTemp = 1/m_FreqMin.Real/2;
					}
					else if(m_Plwd.Exists)
					{
						dTemp = m_Plwd.Real;
					}
					else if(m_PlwdMax.Exists)
					{
						dTemp = m_PlwdMax.Real;
					}
					else if(m_PlwdMin.Exists)
					{
						dTemp = m_PlwdMin.Real;
					}

					sprintf(tempstring, " pulseWidth=\"%gs\"", dTemp);
					
					if(m_FreqMax.Exists || m_Freq.Exists || m_FreqMin.Exists || m_PeriMax.Exists || m_Peri.Exists || m_PeriMin.Exists)
					{
						strcat(stringinput,tempstring);
					}
					
					if(m_RiseMax.Exists)
					{
						dTemp = m_RiseMax.Real;
					}
					else if(m_Rise.Exists)
					{
						dTemp = m_Rise.Real;
					}
					else if(m_RiseMin.Exists)
					{
						dTemp = m_RiseMin.Real;
					}

					sprintf(tempstring, " riseTime=\"%gs\"", dTemp);
					
					if(m_Rise.Exists || m_RiseMin.Exists || m_RiseMax.Exists)
					{
						strcat(stringinput,tempstring);
					}

					if(m_FallMax.Exists)
					{
						dTemp = m_FallMax.Real;
					}
					else if(m_Fall.Exists)
					{
						dTemp = m_Fall.Real;
					}
					else if(m_FallMin.Exists)
					{
						dTemp = m_FallMin.Real;
					}

					sprintf(tempstring, " fallTime=\"%gs\"", dTemp);
					
					if(m_Fall.Exists || m_FallMin.Exists || m_FallMax.Exists)
					{
						strcat(stringinput,tempstring);
					}
				}
				else
				{
					// 1) SQUARE WAVE component
					// Modifier value: Voltage related, frequency related
					strcat(stringinput, "<SquareWave name=\"AC_COMP\"");
					// Algorithm for amplitude (subject to change)
					if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
					m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
					{
						if (m_VlppMax.Exists)
						{
							dTemp = m_VlppMax.Real;  // was div by 2
						}
						else if (m_Vlpp.Exists)
						{
							dTemp = m_Vlpp.Real;  // was div by 2
						}
						else if (m_VlppMin.Exists)
						{
							dTemp = m_VlppMin.Real;  // was div by 2
						}
						else if (m_VlpkMax.Exists)
						{
							dTemp = m_VlpkMax.Real * 2;
						}
						else if (m_Vlpk.Exists)
						{
							dTemp= m_Vlpk.Real * 2;
						}
						else if (m_VlpkMin.Exists)
						{
							dTemp = m_VlpkMin.Real * 2;
						}
						else if (m_VpkpMax.Exists)
						{
							dTemp = m_VpkpMax.Real * 2;
						}
						else if (m_Vpkp.Exists)
						{
							dTemp= m_Vpkp.Real * 2;
						}
						else if (m_VpkpMin.Exists)
						{
							dTemp = m_VpkpMin.Real * 2;
						}
						else if (m_VpknMax.Exists)
						{
							dTemp = m_VpknMax.Real * 2;
						}
						else if (m_Vpkn.Exists)
						{
							dTemp= m_Vpkn.Real * 2;
						}
						else if (m_VpknMin.Exists)
						{
							dTemp = m_VpknMin.Real * 2;
						}
						else if (m_VoltMax.Exists)
						{
							dTemp = m_VoltMax.Real;
						}
						else if (m_Volt.Exists)
						{
							dTemp = m_Volt.Real;
						}
						else if (m_VoltMin.Exists)
						{
							dTemp = m_VoltMin.Real;
						}
					}

					if (m_OverMax.Exists || m_Over.Exists || m_OverMin.Exists)
					{
						if (m_OverMax.Exists)
						{
							dTemp2 = m_OverMax.Real;
						}
						else if (m_Over.Exists)
						{
							dTemp2 = m_Over.Real;
						}
						else if (m_OverMin.Exists)
						{
							dTemp2 = m_OverMin.Real;
						}
					}

					if (m_PshtMax.Exists || m_Psht.Exists || m_PshtMin.Exists)
					{
						if (m_PshtMax.Exists)
						{
							dTemp3 = m_PshtMax.Real;
						}
						else if (m_Psht.Exists)
						{
							dTemp3 = m_Psht.Real;
						}
						else if (m_PshtMin.Exists)
						{
							dTemp3 = m_PshtMin.Real;
						}
					}

					dTemp *= ((dTemp2>dTemp3)?(dTemp2/100+1):(dTemp3/100+1));
					sprintf(tempstring, " amplitude=\"%3.3lfV\"", dTemp);
					
					if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
					m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
					{
						strcat(stringinput, tempstring);
					}
					
					if(m_PeriMax.Exists)
					{
						dTemp = m_PeriMax.Real;
					}
					else if(m_Peri.Exists)
					{
						dTemp = m_Peri.Real;
					}
					else if(m_PeriMin.Exists)
					{
						dTemp = m_PeriMin.Real;
					}
					else if(m_FreqMax.Exists)
					{
						dTemp = 1/m_FreqMax.Real;
					}
					else if(m_Freq.Exists)
					{
						dTemp = 1/m_Freq.Real;
					}
					else if(m_FreqMin.Exists)
					{
						dTemp = 1/m_FreqMin.Real;
					}
					else if(m_Plwd.Exists)
					{
						dTemp = 2*m_Plwd.Real;
					}
					else if(m_PlwdMax.Exists)
					{
						dTemp = 2*m_PlwdMax.Real;
					}
					else if(m_PlwdMin.Exists)
					{
						dTemp = 2*m_PlwdMin.Real;
					}

					sprintf(tempstring, " period=\"%gs\"", dTemp);
					
					if(m_FreqMax.Exists || m_Freq.Exists || m_FreqMin.Exists ||	m_PeriMax.Exists || m_Peri.Exists || m_PeriMin.Exists)
					{
						strcat(stringinput, tempstring);
					}
				}

				strcat(stringinput,"/>\n");    // BSC terminator

				// 2) Constant component
				// Modifier value: dc-offset (default: 0)
				if(m_Dcof.Exists)
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\"/>\n", m_Dcof.Real);
					strcat(stringinput,tempstring);
				}
				else
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\"/>\n");
					strcat(stringinput,tempstring);
				}

				// 3) Adding AC Signal and Constant components
				sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\"/>\n");
				strcat(stringinput,tempstring);
				
				// 4) Filters + Coupling
				// If AC coupling and HigPass both exist, send error
				if (m_Channumb == 2)
				{
					sprintf(sChan, "CHB");
				}
				else
				{
					sprintf(sChan, "CHA");
				}
				
				if(m_BandMax.Exists)
				{
					dBandMax = m_BandMax.Real;
				}
				else
				{
					dBandMax = 20e+9;
				}

				sprintf(tempstring, "<LowPass name=\"LowPass\" cutoff=\"%3.3lfHZ\" In=\"%s\"/>\n", dBandMax,sChan);
				strcat(stringinput,tempstring);
				
				if (m_BandMin.Exists && m_Cplg.Exists)
				{
					CEMERROR(EB_SEVERITY_ERROR|EB_ACTION_HALT, "AC COUPLE and BANDWIDTH/FREQ-WINDOW Min cannot coexist in this IEEE1641 model\n");
					return (-1);
				}
				
				if (m_BandMin.Exists)
				{
					dBandMin = m_BandMin.Real;
				}
				else if (m_Cplg.Exists)
				{
					dBandMin = 0.0;
				}
				
				if (m_BandMin.Exists || m_Cplg.Exists)
				{
					sprintf(tempstring, "<HighPass name=\"HighPass\" cutoff=\"%3.3lfHZ\" In=\"%s\"/>\n", dBandMin, sChan);
					strcat(stringinput,tempstring);
				}
				
				// 5) Attenuation
				int nGain = 0;
				
				if(m_Volt.Exists && m_Volt.Real > 5.0)
				{
					nGain = 10;
				}
				else 
				{
					nGain = 0;
				}
				
				if (m_BandMin.Exists || m_Cplg.Exists)
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"HighPass\"/>\n", nGain);
				}
				else
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"LowPass\"/>\n", nGain);
				}
				
				strcat(stringinput,tempstring);
				
				// 6) Load
				sprintf(tempstring, "<Load name=\"Load\" resistance=\"%3.3lfOHM\" In=\"Attenuator\"/>\n", m_Timp.Real);
				strcat(stringinput,tempstring);
				
				// 7) TwoWire
				if (m_Channumb == 2)
				{
					sprintf(tempstring, "<TwoWire name=\"TwoWire\" hi=\"chb\" channelWidth=\"1\" In=\"Load\"/>\n");
				}
				else
				{
					sprintf(tempstring, "<TwoWire name=\"TwoWire\" hi=\"cha\" channelWidth=\"1\" In=\"Load\"/>\n");
				}

				strcat(stringinput,tempstring);
				
				// 8a)Trigger information
				if (m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					if(m_Trdl.Exists)
					{
						sprintf(tempstring, "<SignalDelay name=\"Trigger_Start\" delay=\"%gs\" In=\"Trigger\"/>", m_Trdl.Real);
						strcat(stringinput,tempstring);
						sprintf(tempstring, "<Instantaneous name=\"Trigger\"");
						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, "<Instantaneous name=\"Trigger_Start\"");
						strcat(stringinput,tempstring);
					}
				
					if (m_Trlv.Exists)
					{
						sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Trlv.Real);
						strcat(stringinput,tempstring);
					}
					
					if (m_Trsl.Exists)
					{
						if(m_Trsl.Int == +1)
						{
							sprintf(tempstring, " condition=\"GT\"");
						}
						else if (m_Trsl.Int == -1)
						{
							sprintf(tempstring, " condition=\"LT\"");
						}

						strcat(stringinput,tempstring);
					}
					
					if (m_Trgs.Exists)
					{
						if (m_Trgs.Int == 1)
						{
							sprintf(tempstring, " In=\"CHA\"");
						}
						if (m_Trgs.Int == 2)
						{
							sprintf(tempstring, " In=\"CHB\"");
						}
						if (m_Trgs.Int == 3)
						{
							sprintf(tempstring, " In=\"EXT\"");
						}
						if (m_Trgs.Int == 4)
						{
							sprintf(tempstring, " In=\"INT\"");
						}

						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, " In=\"%s\"", sChan);
						strcat(stringinput, tempstring);
					}

					strcat(stringinput, "/>\n");
				}
				
				// 8b) Measure
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Signal\" In=\"Load\"");
				strcat(stringinput,tempstring);
				
				// Sample-count
				if(m_Scnt.Exists)
				{
					sprintf(tempstring, " samples=\"%d\"", m_Scnt.Int);
					strcat(stringinput,tempstring);
				}
				
				// If Trigger information
				if(m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					sprintf(tempstring, " Sync=\"Trigger_Start\"");
					strcat(stringinput,tempstring);
				}
				
				// Meas. char.
				//get the measured character
				measchar = GetCurDevMChar();
				switch(measchar)	//add attribute to measure statement
				{
					case M_VLPP:
						// voltage-pp
						strcpy(sMeasChar, ATTRIB_SQW_VLPP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_VLPP);
						break;
					case M_FREQ:
						// freq
						strcpy(sMeasChar, ATTRIB_SQW_FREQ);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_FREQ);
						break;
					case M_RISE:
						// rise-time
						strcpy(sMeasChar, ATTRIB_SQW_RISE);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_RISE);
						break;
					case M_FALL:
						// fall-time
						strcpy(sMeasChar, ATTRIB_SQW_FALL);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_FALL);
						break;
					case M_DUTY:
						// duty-cycle
						strcpy(sMeasChar, ATTRIB_SQW_DUTY);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_DUTY);
						break;
					case M_VLPK:
						// voltage-p
						strcpy(sMeasChar, ATTRIB_SQW_VLPK);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_VLPK);
						break;
					case M_VPKP:
						// voltage-p-pos
						strcpy(sMeasChar, ATTRIB_SQW_VPKP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_VPKP);
						break;
					case M_VPKN:
						// voltage-p-neg
						strcpy(sMeasChar, ATTRIB_SQW_VPKN);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_VPKN);
						break;
					case M_PERI:
						// period
						strcpy(sMeasChar, ATTRIB_SQW_PERI);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_SQW_PERI);
						break;
				}

				strcat(stringinput, tempstring);
				
				// 10) End of SQUARE WAVE
				sprintf(tempstring,"/>\n</Signal>\n");
				strcat(stringinput, tempstring);
			}
			break;

			//***RAMP SIGNAL***/////////////////////////////////////////////////////////////////////////////////
			case N_RPS:
			{
				// Creating the IEEE1641 string
				sprintf(stringinput, "<Signal Name=\"DSO_RAMP_SIGNAL\" Out=\"Measure\" In=\"CHA CHB EXT INT\">\n");
			
				// 1) RAMP SIGNAL component
				// Modifier value: Voltage related, frequency related
				strcat(stringinput, "<Ramp name=\"AC_COMP\"");
				
				if(m_RiseMax.Exists || m_Rise.Exists || m_RiseMin.Exists)
				{
					if(m_RiseMax.Exists)
					{
						dTemp = m_RiseMax.Real;
					}
					else if(m_Rise.Exists)
					{
						dTemp = m_Rise.Real;
					}
					else if(m_RiseMin.Exists)
					{
						dTemp = m_RiseMin.Real;
					}

					sprintf(tempstring, " riseTime=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}
				else if(m_FallMax.Exists || m_Fall.Exists || m_FallMin.Exists)
				{
					if(m_Fall.Exists)
					{
						dTemp2=m_Fall.Real;
					}
					else if(m_FallMin.Exists)
					{
						dTemp2=m_FallMin.Real;
					}
					else if(m_FallMax.Exists)
					{
						dTemp2=m_FallMax.Real;
					}

					if(m_PeriMax.Exists)
					{
						dTemp = m_PeriMax.Real - dTemp2;
					}
					else if(m_Peri.Exists)
					{
						dTemp = m_Peri.Real - dTemp2;
					}
					else if(m_PeriMin.Exists)
					{
						dTemp = m_PeriMin.Real - dTemp2;
					}
					else if(m_FreqMax.Exists)
					{
						dTemp = 1/m_FreqMax.Real - dTemp2;
					}
					else if(m_Freq.Exists)
					{
						dTemp = 1/m_Freq.Real - dTemp2;
					}
					else if(m_FreqMin.Exists)
					{
						dTemp = 1/m_FreqMin.Real - dTemp2;
					}
			
					sprintf(tempstring, " riseTime=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}

				// Algorithm for amplitude (subject to change)
				if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
				m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
				{
					if (m_VlppMax.Exists)
					{
						dTemp = m_VlppMax.Real;  // was div by 2
					}
					else if (m_Vlpp.Exists)
					{
						dTemp = m_Vlpp.Real;  // was div by 2
					}
					else if (m_VlppMin.Exists)
					{
						dTemp = m_VlppMin.Real;  // was div by 2
					}
					else if (m_VlpkMax.Exists)
					{
						dTemp = m_VlpkMax.Real * 2;
					}
					else if (m_Vlpk.Exists)
					{
						dTemp= m_Vlpk.Real * 2;
					}
					else if (m_VlpkMin.Exists)
					{
						dTemp = m_VlpkMin.Real * 2;
					}
					else if (m_VpkpMax.Exists)
					{
						dTemp = m_VpkpMax.Real * 2;
					}
					else if (m_Vpkp.Exists)
					{
						dTemp= m_Vpkp.Real * 2;
					}
					else if (m_VpkpMin.Exists)
					{
						dTemp = m_VpkpMin.Real * 2;
					}
					else if (m_VpknMax.Exists)
					{
						dTemp = m_VpknMax.Real * 2;
					}
					else if (m_Vpkn.Exists)
					{
						dTemp= m_Vpkn.Real * 2;
					}
					else if (m_VpknMin.Exists)
					{
						dTemp = m_VpknMin.Real * 2;
					}
					else if (m_VoltMax.Exists)
					{
						dTemp = m_VoltMax.Real;
					}
					else if (m_Volt.Exists)
					{
						dTemp = m_Volt.Real;
					}
					else if (m_VoltMin.Exists)
					{
						dTemp = m_VoltMin.Real;
					}
				}

				sprintf(tempstring, " amplitude=\"%3.3lfV\"", dTemp);
				
				if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
				m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
				{
					strcat(stringinput,tempstring);
				}
				
				if(m_PeriMax.Exists)
				{
					dTemp = m_PeriMax.Real;
				}
				else if(m_Peri.Exists)
				{
					dTemp = m_Peri.Real;
				}
				else if(m_PeriMin.Exists)
				{
					dTemp = m_PeriMin.Real;
				}
				else if(m_FreqMax.Exists)
				{
					dTemp = 1/m_FreqMax.Real;
				}
				else if(m_Freq.Exists)
				{
					dTemp = 1/m_Freq.Real;
				}
				else if(m_FreqMin.Exists)
				{
					dTemp = 1/m_FreqMin.Real;
				}
				else
				{
					dTemp2=dTemp3=0;
				
					if(m_Rise.Exists)
					{
						dTemp2=m_Rise.Real;
					}
					else if(m_RiseMin.Exists)
					{
						dTemp2=m_FallMin.Real;
					}
					else if(m_RiseMax.Exists)
					{
						dTemp2=m_RiseMax.Real;
					}

					if(m_Fall.Exists)
					{
						dTemp3=m_Fall.Real;
					}
					else if(m_FallMin.Exists)
					{
						dTemp3=m_FallMin.Real;
					}
					else if(m_FallMax.Exists)
					{
						dTemp3=m_FallMax.Real;
					}

					dTemp=dTemp2+dTemp3;
				}

				sprintf(tempstring, " period=\"%gs\"", dTemp);
				
				if(m_FreqMax.Exists || m_Freq.Exists || m_FreqMin.Exists ||	m_PeriMax.Exists || m_Peri.Exists || m_PeriMin.Exists)
				{
					strcat(stringinput,tempstring);
				}
				
				strcat(stringinput,"/>\n");    // BSC terminator
				
				// 2) Constant component
				// Modifier value: dc-offset (default: 0)
				if(m_Dcof.Exists)
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\"/>\n", m_Dcof.Real);
					strcat(stringinput,tempstring);
				}
				else
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\"/>\n");
					strcat(stringinput,tempstring);
				}
				
				// 3) Adding AC Signal and Constant components
				sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\"/>\n");
				strcat(stringinput,tempstring);
				
				// 4) Filters + Coupling
				// If AC coupling and HigPass both exist, send error
				if (m_Channumb == 2)
				{
					sprintf(sChan, "CHB");
				}
				else
				{
					sprintf(sChan, "CHA");
				}
				
				if(m_BandMax.Exists)
				{
					dBandMax = m_BandMax.Real;
				}
				else
				{
					dBandMax = 20e+9;
				}

				sprintf(tempstring, "<LowPass name=\"LowPass\" cutoff=\"%3.3lfHZ\" In=\"%s\"/>\n", dBandMax, sChan);
				strcat(stringinput,tempstring);
				
				if (m_BandMin.Exists && m_Cplg.Exists)
				{
					CEMERROR(EB_SEVERITY_ERROR|EB_ACTION_HALT, "AC COUPLE and BANDWIDTH/FREQ-WINDOW Min cannot coexist in this IEEE1641 model\n");
					return (-1);
				}

				if (m_BandMin.Exists)
				{
					dBandMin = m_BandMin.Real;
				}
				else if (m_Cplg.Exists)
				{
					dBandMin = 0.0;
				}

				if(m_BandMin.Exists || m_Cplg.Exists)
				{
					sprintf(tempstring, "<HighPass name=\"HighPass\" cutoff=\"%3.3lfHZ\" In=\"%s\"/>\n", dBandMin, sChan);
					strcat(stringinput,tempstring);
				}
				else if (m_Cplg.Exists)
				{
					sprintf(tempstring, "<HighPass name=\"AC_COUPLE\" cutoff=\"0HZ\" In=\"%s\"/>\n", sChan);
					strcat(stringinput,tempstring);
				}
				
				// 5) Attenuation
				int nGain = 0;
				if(m_Volt.Exists && m_Volt.Real > 5.0)
				{
					nGain = 10;
				}
				else
				{
					nGain = 0;
				}

				if (m_BandMin.Exists || m_Cplg.Exists)
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"HighPass\"/>\n", nGain);
				}
				else
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"LowPass\"/>\n", nGain);
				}

				strcat(stringinput,tempstring);
				
				// 6) Load
				sprintf(tempstring, "<Load name=\"Load\" resistance=\"%3.3lfOHM\" In=\"Attenuator\"/>\n", m_Timp.Real);
				strcat(stringinput,tempstring);
				
				// 7) TwoWire
				//if (m_Channumb == 2)
				//	sprintf(tempstring, "<TwoWire name=\"TwoWire\" hi=\"chb\" channelWidth=\"1\" In=\"Load\"/>\n");
				//else
				//	sprintf(tempstring, "<TwoWire name=\"TwoWire\" hi=\"cha\" channelWidth=\"1\" In=\"Load\"/>\n");
				//strcat(stringinput,tempstring);
				
				// 8a)Trigger information
				if (m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					if(m_Trdl.Exists)
					{
						sprintf(tempstring, "<SignalDelay name=\"Trigger_Start\" delay=\"%gs\" In=\"Trigger\"/>", m_Trdl.Real);
						strcat(stringinput,tempstring);
						sprintf(tempstring, "<Instantaneous name=\"Trigger\"");
						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, "<Instantaneous name=\"Trigger_Start\"");
						strcat(stringinput,tempstring);
					}

					if (m_Trlv.Exists)
					{
						sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Trlv.Real);
						strcat(stringinput,tempstring);
					}

					if (m_Trsl.Exists)
					{
						if(m_Trsl.Int == +1)
						{
							sprintf(tempstring, " condition=\"GT\"");
						}
						else if (m_Trsl.Int == -1)
						{
							sprintf(tempstring, " condition=\"LT\"");
						}

						strcat(stringinput,tempstring);
					}

					if (m_Trgs.Exists)
					{
						if (m_Trgs.Int == 1)
						{
							sprintf(tempstring, " In=\"CHA\"");
						}

						if (m_Trgs.Int == 2)
						{
							sprintf(tempstring, " In=\"CHB\"");
						}

						if (m_Trgs.Int == 3)
						{
							sprintf(tempstring, " In=\"EXT\"");
						}

						if (m_Trgs.Int == 4)
						{
							sprintf(tempstring, " In=\"INT\"");
						}

						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, " In=\"%s\"", sChan);
						strcat(stringinput, tempstring);
					}

					strcat(stringinput, "/>\n");
				}

				// 8b) Measure
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Signal\" In=\"Load\"");
				strcat(stringinput,tempstring);
				
				// Sample-count
				if(m_Scnt.Exists)
				{
					sprintf(tempstring, " samples=\"%d\"", m_Scnt.Int);
					strcat(stringinput,tempstring);
				}
				
				// If Trigger information
				if(m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					sprintf(tempstring, " Sync=\"Trigger_Start\"");
					strcat(stringinput,tempstring);
				}
				
				// Meas. char.
				//get the measured character
				measchar = GetCurDevMChar();

				switch(measchar)	//add attribute to measure statement
				{
					case M_VLPP:
						// voltage-pp
						strcpy(sMeasChar, ATTRIB_RPS_VLPP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_RPS_VLPP);
						break;
					case M_FREQ:
						// freq
						strcpy(sMeasChar, ATTRIB_RPS_FREQ);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_RPS_FREQ);
						break;
					case M_RISE:
						// risetime
						strcpy(sMeasChar, ATTRIB_RPS_RISE);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_RPS_RISE);
						break;
					case M_FALL:
						// falltime
						strcpy(sMeasChar, ATTRIB_RPS_FALL);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_RPS_FALL);
						break;
					case M_VLPK:
						// voltage-p
						strcpy(sMeasChar, ATTRIB_RPS_VLPK);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_RPS_VLPK);
						break;
					case M_VPKP:
						// voltage-p-pos
						strcpy(sMeasChar, ATTRIB_RPS_VPKP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_RPS_VPKP);
						break;
					case M_VPKN:
						// voltage-p-neg
						strcpy(sMeasChar, ATTRIB_RPS_VPKN);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_RPS_VPKN);
						break;
					case M_PERI:
						// period
						strcpy(sMeasChar, ATTRIB_RPS_PERI);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_RPS_PERI);
						break;
				}
				
				// 10) End of RAMP SIGNAL
				strcat(stringinput,tempstring);
				sprintf(tempstring,"/>\n</Signal>\n");
				strcat(stringinput,tempstring);
			}
			
			break;

			//***TRIANGULAR WAVE SIGNAL***/////////////////////////////////////////////////////////////////////////////////
			case N_TRI:
			{
				// Creating the IEEE1641 string
				sprintf(stringinput, "<Signal Name=\"DSO_TRIANGULAR_WAVE_SIGNAL\" Out=\"Measure\" In=\"CHA CHB EXT INT\">\n");
			
				// 1) TRIANGULAR WAVE SIGNAL component
				// Modifier value: Voltage related, frequency related
				strcat(stringinput, "<Triangle name=\"AC_COMP\"");
				if( m_DutyMax.Exists || m_Duty.Exists || m_DutyMin.Exists)
				{
					if(m_DutyMax.Exists)
					{
						dTemp = m_DutyMax.Real;
					}
					else if(m_Duty.Exists)
					{
						dTemp = m_Duty.Real;
					}
					else if(m_DutyMin.Exists)
					{
						dTemp = m_DutyMin.Real;
					}
					
					sprintf(tempstring, " riseTime=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}

				if(m_RiseMax.Exists || m_Rise.Exists || m_RiseMin.Exists)
				{
					if(m_RiseMax.Exists)
					{
						dTemp = m_RiseMax.Real;
					}
					else if(m_Rise.Exists)
					{
						dTemp = m_Rise.Real;
					}
					else if(m_RiseMin.Exists)
					{
						dTemp = m_RiseMin.Real;
					}

					sprintf(tempstring, " riseTime=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}

				if(m_FallMax.Exists || m_Fall.Exists || m_FallMin.Exists)
				{
					if(m_FallMax.Exists)
					{
						dTemp = m_FallMax.Real;
					}
					else if(m_Fall.Exists)
					{
						dTemp = m_Fall.Real;
					}
					else if(m_FallMin.Exists)
					{
						dTemp = m_FallMin.Real;
					}

					sprintf(tempstring, " fallTime=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}

				// Algorithm for amplitude (subject to change)
				if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
				m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
				{
					if (m_VlppMax.Exists)
					{
						dTemp = m_VlppMax.Real;  // was div by 2
					}
					else if (m_Vlpp.Exists)
					{
						dTemp = m_Vlpp.Real; // was div by 2
					}
					else if (m_VlppMin.Exists)
					{
						dTemp = m_VlppMin.Real; // was div by 2
					}
					else if (m_VlpkMax.Exists)
					{
						dTemp = m_VlpkMax.Real * 2;
					}
					else if (m_Vlpk.Exists)
					{
						dTemp= m_Vlpk.Real * 2;
					}
					else if (m_VlpkMin.Exists)
					{
						dTemp = m_VlpkMin.Real * 2;
					}
					else if (m_VpkpMax.Exists)
					{
						dTemp = m_VpkpMax.Real * 2;
					}
					else if (m_Vpkp.Exists)
					{
						dTemp= m_Vpkp.Real * 2;
					}
					else if (m_VpkpMin.Exists)
					{
						dTemp = m_VpkpMin.Real * 2;
					}
					else if (m_VpknMax.Exists)
					{
						dTemp = m_VpknMax.Real * 2;
					}
					else if (m_Vpkn.Exists)
					{
						dTemp= m_Vpkn.Real * 2;
					}
					else if (m_VpknMin.Exists)
					{
						dTemp = m_VpknMin.Real * 2;
					}
					else if (m_VoltMax.Exists)
					{
						dTemp = m_VoltMax.Real;
					}
					else if (m_Volt.Exists)
					{
						dTemp = m_Volt.Real;
					}
					else if (m_VoltMin.Exists)
					{
						dTemp = m_VoltMin.Real;
					}
				}

				sprintf(tempstring, " amplitude=\"%3.3lfV\"", dTemp);
				
				if(m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists|| m_VpkpMax.Exists || 
				m_VpkpMin.Exists || m_Vpkp.Exists|| m_VpknMax.Exists || m_VpknMin.Exists || m_Vpkn.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists)
				{
					strcat(stringinput,tempstring);
				}
				
				if(m_PeriMax.Exists)
				{
					dTemp = m_PeriMax.Real;
				}
				else if(m_Peri.Exists)
				{
					dTemp = m_Peri.Real;
				}
				else if(m_PeriMin.Exists)
				{
					dTemp = m_PeriMin.Real;
				}
				else if(m_FreqMax.Exists)
				{
					dTemp = 1/m_FreqMax.Real;
				}
				else if(m_Freq.Exists)
				{
					dTemp = 1/m_Freq.Real;
				}
				else if(m_FreqMin.Exists)
				{
					dTemp = 1/m_FreqMin.Real;
				}

				sprintf(tempstring, " period=\"%gs\"", dTemp);
				
				if(m_FreqMax.Exists || m_Freq.Exists || m_FreqMin.Exists ||	m_PeriMax.Exists || m_Peri.Exists || m_PeriMin.Exists)
				{
					strcat(stringinput,tempstring);
				}
				
				strcat(stringinput, "/>\n");

				// 2) Constant component
				// Modifier value: dc-offset (default: 0)
				if(m_Dcof.Exists)
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\"/>\n", m_Dcof.Real);
					strcat(stringinput,tempstring);
				}
				else
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\"/>\n");
					strcat(stringinput,tempstring);
				}

				// 3) Adding AC Signal and Constant components
				sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\"/>\n");
				strcat(stringinput,tempstring);
				
				// 4) Filters + Coupling
				// If AC coupling and HigPass both exist, send error
				if (m_Channumb == 2)
				{
					sprintf(sChan, "CHB");
				}
				else
				{
					sprintf(sChan, "CHA");
				}

				if(m_BandMax.Exists)
				{
					dBandMax = m_BandMax.Real;
				}
				else
				{
					dBandMax = 20e+9;
				}

				sprintf(tempstring, "<LowPass name=\"LowPass\" cutoff=\"%3.3lfHZ\" In=\"%s\"/>\n", dBandMax, sChan);
				strcat(stringinput,tempstring);
				
				if (m_BandMin.Exists && m_Cplg.Exists)
				{
					CEMERROR(EB_SEVERITY_ERROR|EB_ACTION_HALT, "AC COUPLE and BANDWIDTH/FREQ-WINDOW Min cannot coexist in this IEEE1641 model\n");
					return (-1);
				}
				
				if (m_BandMin.Exists)
				{
					dBandMin = m_BandMin.Real;
				}
				else if (m_Cplg.Exists)
				{
					dBandMin = 0.0;
				}

				if (m_BandMin.Exists || m_Cplg.Exists)
				{
					sprintf(tempstring, "<HighPass name=\"HighPass\" cutoff=\"%3.3lfHZ\" In=\"%s\"/>\n", dBandMin, sChan);
					strcat(stringinput,tempstring);
				}
				
				// 5) Attenuation
				int nGain = 0;
				
				if(m_Volt.Exists && m_Volt.Real > 5.0)
				{
					nGain = 10;
				}
				else
				{
					nGain = 0;
				}

				if (m_BandMin.Exists || m_Cplg.Exists)
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"HighPass\"/>\n", nGain);
				}
				else
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"LowPass\"/>\n", nGain);
				}

				strcat(stringinput,tempstring);
				
				// 6) Load
				sprintf(tempstring, "<Load name=\"Load\" resistance=\"%3.3lfOHM\" In=\"Attenuator\"/>\n", m_Timp.Real);
				strcat(stringinput,tempstring);
				
				// 7) TwoWire
				if (m_Channumb == 2)
				{
					sprintf(tempstring, "<TwoWire name=\"TwoWire\" hi=\"chb\" channelWidth=\"1\" In=\"Load\"/>\n");
				}
				else
				{
					sprintf(tempstring, "<TwoWire name=\"TwoWire\" hi=\"cha\" channelWidth=\"1\" In=\"Load\"/>\n");
				}

				strcat(stringinput,tempstring);
				
				// 8a)Trigger information
				if (m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					if(m_Trdl.Exists)
					{
						sprintf(tempstring, "<SignalDelay name=\"Trigger_Start\" delay=\"%gs\" In=\"Trigger\"/>", m_Trdl.Real);
						strcat(stringinput,tempstring);
						sprintf(tempstring, "<Instantaneous name=\"Trigger\"");
						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, "<Instantaneous name=\"Trigger_Start\"");
						strcat(stringinput,tempstring);
					}
					
					if (m_Trlv.Exists)
					{
						sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Trlv.Real);
						strcat(stringinput,tempstring);
					}
					
					if (m_Trsl.Exists)
					{
						if(m_Trsl.Int == +1)
						{
							sprintf(tempstring, " condition=\"GT\"");
						}
						else if (m_Trsl.Int == -1)
						{
							sprintf(tempstring, " condition=\"LT\"");
						}

						strcat(stringinput,tempstring);
					}

					if (m_Trgs.Exists)
					{
						if (m_Trgs.Int == 1)
						{
							sprintf(tempstring, " In=\"CHA\"");
						}
						
						if (m_Trgs.Int == 2)
						{
							sprintf(tempstring, " In=\"CHB\"");
						}

						if (m_Trgs.Int == 3)
						{
							sprintf(tempstring, " In=\"EXT\"");
						}

						if (m_Trgs.Int == 4)
						{
							sprintf(tempstring, " In=\"INT\"");
						}

						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, " In=\"%s\"", sChan);
						strcat(stringinput, tempstring);
					}
					strcat(stringinput, "/>\n");
				}

				// 8b) Measure
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Signal\" In=\"Load\"");
				strcat(stringinput,tempstring);
				
				// Sample-count
				if(m_Scnt.Exists)
				{
					sprintf(tempstring, " samples=\"%d\"", m_Scnt.Int);
					strcat(stringinput,tempstring);
				}
				
				// If Trigger information
				if(m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					sprintf(tempstring, " Sync=\"Trigger_Start\"");
					strcat(stringinput,tempstring);
				}
				
				// Meas. char.
				//get the measured character
				measchar = GetCurDevMChar();

				switch(measchar)	//add attribute to measure statement
				{
					case M_VLPP:
						// voltage-pp
						strcpy(sMeasChar, ATTRIB_TRI_VLPP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_VLPP);
						break;
					case M_FREQ:
						// frequency
						strcpy(sMeasChar, ATTRIB_TRI_FREQ);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_FREQ);
						break;
					case M_RISE:
						// risetime
						strcpy(sMeasChar, ATTRIB_TRI_RISE);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_RISE);
						break;
					case M_FALL:
						// falltime
						strcpy(sMeasChar, ATTRIB_TRI_FALL);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_FALL);
						break;
					case M_DUTY:
						// duty-cycle
						strcpy(sMeasChar, ATTRIB_TRI_DUTY);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_DUTY);
						break;
					case M_VLPK:
						// voltage-p
						strcpy(sMeasChar, ATTRIB_TRI_VLPK);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_VLPK);
						break;
					case M_VPKP:
						// voltage-p-pos
						strcpy(sMeasChar, ATTRIB_TRI_VPKP);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_VPKP);
						break;
					case M_VPKN:
						// voltage-p-neg
						strcpy(sMeasChar, ATTRIB_TRI_VPKN);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_VPKN);
						break;
					case M_PERI:
						// period
						strcpy(sMeasChar, ATTRIB_TRI_PERI);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TRI_PERI);
						break;
				}

				strcat(stringinput, tempstring);
				
				// 10) End of TRIANGULAR WAVE SIGNAL
				sprintf(tempstring,"/>\n</Signal>\n");
				strcat(stringinput, tempstring);
			}
			break;

			//***WAVEFORM***////////////////////////////////////////////////////////////////////////////////////////////
			case N_WAV:
			{
				// Creating the IEEE1641 string
				sprintf(stringinput, "<Signal name=\"DSO_WAVEFORM\" Out=\"Measure\" In=\"CHA CHB EXT INT\">\n");
				strcat(stringinput, "<Instantaneous name=\"Waveform\"");
		           
				// Samples
				if (m_SmplMax.Exists || m_Smpl.Exists || m_SmplMin.Exists || m_Scnt.Exists)
				{
					if (m_SmplMax.Exists)
					{
						sprintf(tempstring, " samples=\"%d\"", (int)m_SmplMax.Real);
						strcat(stringinput,tempstring);
					}
					else if (m_Smpl.Exists)
					{
						sprintf(tempstring, " samples=\"%d\"", (int)m_Smpl.Real);
						strcat(stringinput,tempstring);
					}
					else if (m_SmplMin.Exists)
					{
						sprintf(tempstring, " samples=\"%d\"", (int)m_SmplMin.Real);
						strcat(stringinput,tempstring);
					}
					else if (m_Resp.Exists)
					{
						sprintf(tempstring, " samples=\"%d\"", (int)m_Resp.Real);
						strcat(stringinput,tempstring);
					}
				}
			
				// Sample Interval
				if(m_Satm.Exists)
				{
					dTemp = m_Satm.Real;
					sprintf(tempstring, " samplingInterval=\"%gs\"", dTemp);
					strcat(stringinput,tempstring);
				}
				
				// Algorithm for amplitude (subject to change)
				if (m_VlppMax.Exists || m_Vlpp.Exists || m_VlppMin.Exists || m_VoltMax.Exists || m_Volt.Exists || m_VoltMin.Exists || m_VlpkMax.Exists || m_VlpkMin.Exists || m_Vlpk.Exists)
				{
					if (m_VlppMax.Exists)
					{
						dTemp = m_VlppMax.Real;
					}
					else if (m_Vlpp.Exists)
					{
						dTemp = m_Vlpp.Real;
					}
					else if (m_VlppMin.Exists)
					{
						dTemp = m_VlppMin.Real;
					}
					else if (m_VoltMax.Exists)
					{
						dTemp = m_VoltMax.Real;
					}
					else if (m_Volt.Exists)
					{
						dTemp = m_Volt.Real;
					}
					else if (m_VoltMin.Exists)
					{
						dTemp = m_VoltMin.Real;
					}
					else if (m_VlpkMax.Exists) // Not sure this case will be true
					{
						dTemp = m_VlpkMax.Real;
					}
					else if (m_Vlpk.Exists) // Not sure this case will be true
					{
						dTemp = m_Vlpk.Real;
					}
					else if (m_VlpkMin.Exists) // Not sure this case will be true
					{
						dTemp = m_VlpkMin.Real;
					}
					
					sprintf(tempstring, " nominal=\"%4.3lfV\"", dTemp);
					strcat(stringinput,tempstring);
				}

				strcat(stringinput,"/>\n");

				// 2) Constant component
				// Modifier value: dc-offset (default: 0)
				if(m_Dcof.Exists)
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\"/>\n", m_Dcof.Real);
					strcat(stringinput,tempstring);
				}
				else
				{
					sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\"/>\n");
					strcat(stringinput,tempstring);
				}

				// 3) Adding Waveform Signal and Constant components
				sprintf(tempstring, "<Sum name=\"Signal\" In=\"Waveform DC_COMP\"/>\n");
				strcat(stringinput,tempstring);
				
				// 4) Coupling
				if (m_Channumb == 2)
				{
					sprintf(sChan, "CHB");
				}
				else
				{
					sprintf(sChan, "CHA");
				}

				if (m_Cplg.Exists)
				{
					sprintf(tempstring, "<HighPass name=\"HighPass\" In=\"%s\"/>\n", sChan);
					strcat(stringinput,tempstring);
				}

				// 5) Attenuation
				if(m_Volt.Exists && m_Volt.Real > 5.0)
				{
					dTemp = 10.0;
				}
				else
				{
					dTemp = 0.0;
				}

				if (m_Cplg.Exists)
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"HighPass\"/>\n", dTemp);
				}
				else
				{
					sprintf(tempstring, "<Attenuator name=\"Attenuator\" gain=\"%3.3lf\" In=\"%s\"/>\n", dTemp, sChan);
				}

				strcat(stringinput,tempstring);

				// 6) Load
				sprintf(tempstring, "<Load name=\"Load\" resistance=\"%3.3lfOHM\" In=\"Attenuator\"/>\n", m_Timp.Real);
				strcat(stringinput,tempstring);
				
				// 7) TwoWire
				if (m_Channumb == 2)
				{
					sprintf(tempstring, "<TwoWire name=\"TwoWire\" hi=\"CHB\" channelWidth=\"1\" In=\"Load\"/>\n");
				}
				else
				{
					sprintf(tempstring, "<TwoWire name=\"TwoWire\" hi=\"CHA\" channelWidth=\"1\" In=\"Load\"/>\n");
				}

				strcat(stringinput,tempstring);
				
				// 8a)Trigger information
				if (m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					if(m_Trdl.Exists)
					{
						sprintf(tempstring, "<SignalDelay name=\"Trigger_Start\" delay=\"%gs\" In=\"Trigger\"/>", m_Trdl.Real);
						strcat(stringinput,tempstring);
						sprintf(tempstring, "<Instantaneous name=\"Trigger\"");
						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, "<Instantaneous name=\"Trigger_Start\"");
						strcat(stringinput,tempstring);
					}
				
					if (m_Trlv.Exists)
					{
						sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Trlv.Real);
						strcat(stringinput,tempstring);
					}
					
					if (m_Trsl.Exists)
					{
						if(m_Trsl.Int == +1)
						{
							sprintf(tempstring, " condition=\"GT\"");
						}
						else if (m_Trsl.Int == -1)
						{
							sprintf(tempstring, " condition=\"LT\"");
						}

						strcat(stringinput,tempstring);
					}
					
					if (m_Trgs.Exists)
					{
						if (m_Trgs.Int == 1)
						{
							sprintf(tempstring, " In=\"CHA\"");
						}
						
						if (m_Trgs.Int == 2)
						{
							sprintf(tempstring, " In=\"CHB\"");
						}
						
						if (m_Trgs.Int == 3)
						{
							sprintf(tempstring, " In=\"EXT\"");
						}

						if (m_Trgs.Int == 4)
						{
							sprintf(tempstring, " In=\"INT\"");
						}

						strcat(stringinput,tempstring);
					}
					else
					{
						sprintf(tempstring, " In=\"%s\"", sChan);
						strcat(stringinput, tempstring);
					}
					strcat(stringinput, "/>\n");
				}

				// 8b) Measure
				//sprintf(tempstring, "<Measure name=\"Measure\" As=\"Signal\" In=\"Load\"");
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Signal\" In=\"TwoWire\"");
				strcat(stringinput,tempstring);

				if(m_Svfm.Exists)
				{
					sprintf(tempstring, " saveFrom=\"%s\"", m_Svfm.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Svto.Exists)
				{
					sprintf(tempstring, " saveTo=\"%s\"", m_Svto.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Ldfm.Exists)
				{
					sprintf(tempstring, " loadFrom=\"%s\"", m_Ldfm.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Cmch.Exists)
				{
					sprintf(tempstring, " compareCh=\"%s\"", m_Cmch.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Cmto.Exists)
				{
					sprintf(tempstring, " compareTo=\"%s\"", m_Cmto.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Adfm.Exists)
				{
					sprintf(tempstring, " addFrom=\"%s\"", m_Adfm.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Adto.Exists)
				{
					sprintf(tempstring, " addTo=\"%s\"", m_Adto.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Sbfm.Exists)
				{
					sprintf(tempstring, " subtractFrom=\"%s\"", m_Sbfm.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Sbto.Exists)
				{
					sprintf(tempstring, " subtractTo=\"%s\"", m_Sbto.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Mpfm.Exists)
				{
					sprintf(tempstring, " multiplyFrom=\"%s\"", m_Mpfm.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Mpto.Exists)
				{
					sprintf(tempstring, " multiplyTo=\"%s\"", m_Mpto.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Difr.Exists)
				{
					sprintf(tempstring, " difrFrom=\"%s\"", m_Difr.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Intg.Exists)
				{
					sprintf(tempstring, " intgTo=\"%s\"", m_Intg.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Dest.Exists)
				{
					sprintf(tempstring, " destination=\"%s\"", m_Dest.Text);
					strcat(stringinput, tempstring);
				}
				
				if(m_Allw.Exists)
				{
					sprintf(tempstring, " allowance=\"%g\"", m_Allw.Real);
					strcat(stringinput, tempstring);
				}
				
				// If Trigger information
				if(m_Trlv.Exists || m_Trsl.Exists || m_Trgs.Exists)
				{
					sprintf(tempstring, " Sync=\"Trigger_Start\"");
					strcat(stringinput,tempstring);
				}
				
				// Meas. char.
				//get the measured character
				measchar = GetCurDevMChar();
				switch(measchar)	//add attribute to measure statement
				{
					case M_SMPL:
						// sample
						strcpy(sMeasChar, ATTRIB_WAV_SMPL);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_WAV_SMPL);
						break;
					case M_SVWV:
						// sample wave
						strcpy(sMeasChar, ATTRIB_WAV_SVWV);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_WAV_SVWV);
						break;
					case M_LDVW:
						// load wave
						strcpy(sMeasChar, ATTRIB_WAV_LDVW);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_WAV_LDVW);
						break;
					case M_CMWV:
						// compare-wave
						strcpy(sMeasChar, ATTRIB_WAV_CMWV);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_WAV_CMWV);
						break;
					case M_MATH: 
						// math
						strcpy(sMeasChar, ATTRIB_WAV_MATH);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_WAV_MATH);
						break;
				}

				strcat(stringinput, tempstring);
				
				// 10) End of WAVEFORM
				sprintf(tempstring,"/>\n</Signal>\n");
				strcat(stringinput,tempstring);
			}
			
			break;
			
			//***TIME INTERVAL***////////////////////////////////////////////////////////////////////////////////////////////
			case N_TMI:
				// Creating the IEEE1641 string
				sprintf(stringinput, "<Signal Name=\"DSO_TIME\" Out=\"Measure\" In=\"CHA CHB\">\n");
				
				if(m_TimeMax.Exists || m_Time.Exists || m_TimeMin.Exists)
				{
					if(m_TimeMax.Exists)
					{
						sprintf(tempstring, "<SquareWave name=\"Signal\" period=\"%gs\"/>\n", m_TimeMax.Real);
					}
					else if(m_Time.Exists)
					{
						sprintf(tempstring, "<SquareWave name=\"Signal\" period=\"%gs\"/>\n", m_Time.Real);
					}
					else if(m_TimeMin.Exists)
					{
						sprintf(tempstring, "<SquareWave name=\"Signal\" period=\"%gs\"/>\n", m_TimeMin.Real);
					}

					strcat(stringinput,tempstring);
				}

				// 1) Events from and to
				if (m_Evtf.Exists && m_Evtt.Exists)
				{
					// Event-from
					strcat(stringinput, "<Instantaneous name=\"GateStart\"");
					sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Evtf.voltage);
					strcat(stringinput,tempstring);
					
					if(m_Evtf.slope == +1)
					{
						sprintf(tempstring, " condition=\"GT\"");
					}
					else if (m_Evtf.slope == -1)
					{
						sprintf(tempstring, " condition=\"LT\"");
					}

					strcat(stringinput,tempstring);
					
					if (m_Evtf.port == 1)
					{
						sprintf(tempstring, " In=\"CHA\"");
					}
					
					if (m_Evtf.port == 2)
					{
						sprintf(tempstring, " In=\"CHB\"");
					}

					strcat(stringinput,tempstring);
					strcat(stringinput,"/>\n");
					
					// Event-to
					sprintf(tempstring, "<Instantaneous name=\"GateStop\"");
					strcat(stringinput,tempstring);
					sprintf(tempstring, " nominal=\"%3.3lfV\"", m_Evtt.voltage);
					strcat(stringinput,tempstring);
					
					if(m_Evtt.slope == +1)
					{
						sprintf(tempstring, " condition=\"GT\"");
					}
					else if (m_Evtt.slope == -1)
					{
						sprintf(tempstring, " condition=\"LT\"");
					}

					strcat(stringinput,tempstring);
					
					if (m_Evtt.port == 1)
					{
						sprintf(tempstring, " In=\"CHA\"");
					}

					if (m_Evtt.port == 2)
					{
						sprintf(tempstring, " In=\"CHB\"");
					}

					strcat(stringinput,tempstring);
					sprintf(tempstring,"/>\n");
					strcat(stringinput,tempstring);
				}

				// 2) Adding events together
				sprintf(tempstring, "<EventedEvent name=\"Gate\" In=\"GateStart GateStop\"/>\n");
				strcat(stringinput,tempstring);
				
				// 3) Measure
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Signal\" Gate=\"Gate\" In=\"Load\"");
				strcat(stringinput,tempstring);

				// Meas. char.
				//get the measured character
				measchar = GetCurDevMChar();

				switch(measchar)	//add attribute to measure statement
				{
					case M_TIME:
						// time
						strcpy(sMeasChar, ATTRIB_TMI_TIME);
						sprintf(tempstring, " attribute=\"%s\"",ATTRIB_TMI_TIME);
						//strcat(string,tempstring);
						break;
				}
				
				strcat(stringinput, tempstring);
				
				// 4) End of TIME
				sprintf(tempstring,"/>\n</Signal>\n");
				strcat(stringinput,tempstring);

				break;
		}

		sprintf(m_SignalDescription, stringinput);

		// if this function was not called by fetch
		if(!sFetchCalled)
		{

			if (m_Noun == N_ACS || m_Noun == N_DCS || m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS ||
			m_Noun == N_TRI || m_Noun == N_WAV || m_Noun == N_TMI )
			{
				char    Response[4096];
				
				if(m_Maxt.Exists)
				{
					IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Setup", m_DeviceName, m_Maxt.Real, m_SignalDescription, Response, 4096)));
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - SetupDso - IssueAtmlSignalMaxTime(Setup) Max-Time [%lf] Status [%d]", m_Maxt.Real, Status));
				}
				else
				{
					IFNSIM((Status = cs_IssueAtmlSignal("Setup", m_DeviceName, m_SignalDescription, Response, 4096)));
					CEMDEBUG(9,cs_FmtMsg("[Dso_T] - SetupDso - IssueAtmlSignal(Setup) Signal Desc[%s] Status [%d]", m_SignalDescription, Status));
				}
			}
		}
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateDso(int Fnc)
//
// Purpose: Perform the Initiate action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDso_T::InitiateDso(int Fnc)
{
    int   Status = 0;
	DATUM    *pDatum = NULL;

    // Initiate action for the Dso
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - InitiateDso (Fnc [%d])", Fnc));

	// Max-Time
	if((pDatum = GetDatum(M_MAXT, K_SET)))
	{
		m_Maxt.Exists = TRUE;
		m_Maxt.Real = DECDatVal(pDatum, 0);
		CEMDEBUG(10,cs_FmtMsg("[Dso_T] - InitiateDso - Found Max-Time [%lf]", m_Maxt.Real));
		//FreeDatum(pDatum);
	}

    // Internal Trigger
	if((Fnc>5 && Fnc < 50) || Fnc == 161 || Fnc == 162 || Fnc == 6 || Fnc == 7)
	{   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - InitiateDso - EVENT FNC, no 1641 to send"));
    }
	else
	{
		if (m_Maxt.Exists)
		{
			IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Enable", m_DeviceName, m_Maxt.Real, m_SignalDescription, NULL, 0)));
	        CEMDEBUG(9,cs_FmtMsg("[Dso_T] - InitiateDso - IssueAtmlSignalMaxTime(Enable) Signal Desc [%s] Status [%d]", m_SignalDescription, Status));
		}
		else
		{
			IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SignalDescription, NULL, 0)));
	        CEMDEBUG(9,cs_FmtMsg("[Dso_T] - InitiateDso - IssueAtmlSignal(Enable) Signal Desc [%s] Status [%d]", m_SignalDescription, Status));
		}
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchDso(int Fnc)
//
// Purpose: Perform the Fetch action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDso_T::FetchDso(int Fnc)
{
    DATUM   *fdat = 0;
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5;
    char    XmlValue[500000];    
	double  Waveform[8000] = {};		    	//The Waveform The Digitizer Aquires(Waveform Array)
	int     nIdx = 0;
	int     Idx = 0, i = 0;
	int	    nFthCnt = 0;
	int     Size=8000;
	
    
    // Check MaxTime Modifier
    if(m_Maxt.Exists)
	{
        MaxTime = m_Maxt.Real;
	}
		
    // Fetch action for the Dso
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - FetchDso (Fnc [%d])", Fnc));

    // Fetch data
	if((Fnc>5 && Fnc < 50) || Fnc == 161 || Fnc == 162 || Fnc == 261 || Fnc == 262)
	{   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - FetchDso - EVENT FNC, no 1641 to send"));
    }
	else
	{
		sFetchCalled = true;

		SetupDso(Fnc);

		// replace or send proper Measure attribute name in m_SignalDescription
		IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Fetch", m_DeviceName, MaxTime, m_SignalDescription, XmlValue, 500000)));
		
		sFetchCalled = false;

		if(strcmp(sMeasChar,"wav_smpl")==0 || strcmp(sMeasChar, "wav_svmv")==0 || strcmp(sMeasChar, "wav_ldvm")==0 || 
		strcmp(sMeasChar, "wav_cmwv")==0 || strcmp(sMeasChar, "wav_math")==0)
		{
			cs_GetDblArrayValue(XmlValue, sMeasChar, Waveform, &Size);

			fdat = FthDat();
			nFthCnt=DatCnt(fdat);
			
			//// return data back to paws
			for (i = 0; i < nFthCnt; i++)
			{
			    //sscanf(MeasString, "%lf %lf", &MeasValue, &MeasString);
				//if(i<=Size)
				DECDatVal(fdat, i) = Waveform[i];
				//else
				//	DECDatVal(fdat, i) = 0;
			}

			// report number of items returned
			FthCnt(nFthCnt);
		}
		else
		{
			IFNSIM(cs_GetSingleDblValue(XmlValue, sMeasChar, &MeasValue));

			fdat = FthDat();
			DECDatVal(fdat, 0) = MeasValue;
			
			FthCnt(1);
		}

		// This code does nothing
		//if(Status)
		//{
		//	MeasValue = FLT_MAX;
		//}
	}

    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: OpenDso(int Fnc)
//
// Purpose: Perform the Open action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDso_T::OpenDso(int Fnc)
{
    // Open action for the Dso
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - OpenDso (Fnc [%d])", Fnc));

	if((Fnc>5 && Fnc < 50) || Fnc == 161 || Fnc == 162 || Fnc == 261 || Fnc == 262)
	{   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - OpenDso - EVENT FNC, no 1641 to send"));
    }
	else
	{
		IFNSIM((cs_IssueAtmlSignal("Disable", m_DeviceName, m_SignalDescription, NULL, 0)));
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseDso(int Fnc)
//
// Purpose: Perform the Close action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDso_T::CloseDso(int Fnc)
{
    // Close action for the Dso
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - CloseDso (Fnc [%d])", Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetDso(int Fnc)
//
// Purpose: Perform the Reset action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDso_T::ResetDso(int Fnc)
{
    int   Status = 0;

    // Reset action for the Dso
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - ResetDso (Fnc [%d])", Fnc));

    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
    if((Fnc>5 && Fnc < 50) || Fnc == 161 || Fnc == 162 || Fnc == 261 || Fnc == 262)
	{   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("[Dso_T] - ResetDso - EVENT FNC, no 1641 to send"));
    }
	else
	{
        // Reset the Dso
        IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, "<Signal />", NULL, 0)));
    }

    InitPrivateDso();

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoDso(int Fnc)
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             Device Database Function value
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDso_T::GetStmtInfoDso(int Fnc)
{
    CEMDEBUG(5,cs_FmtMsg("[Dso_T] - GetStmtInfoDso (Fnc [%d])", Fnc));

	DATUM    *pDatum = NULL;

	// For all Fncs addressing cha or chb
	if ((Fnc > 99 || Fnc<6)&& (Fnc != 161 && Fnc != 162 && Fnc != 6 && Fnc != 7))
	{
		// Getting the channel number
		m_Channumb = (int)(Fnc/100);

		// compensate for illogical FNC numbering 
		if (Fnc == 5)
		{
			m_Channumb = 2;
		}

		// Getting the noun
		// AC Signal
		m_Noun = GetCurNoun();
		CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Noun int. value [%d]", m_Noun));

		// Extracting common modifiers
		// Max-Time
		if((pDatum = RetrieveDatum(M_MAXT, K_SET)))
		{
			m_Maxt.Exists = TRUE;
			m_Maxt.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Max-Time [%lf]", m_Maxt.Real));
			FreeDatum(pDatum);
		}

		// Coupling
		m_Cplg.Int = 0;
		if((pDatum = RetrieveDatum(M_CPLG, K_SET)))
		{
			sprintf (m_Cplg.Text, "%s", GetTXTDatVal(pDatum, 0));
			if (strcmp(m_Cplg.Text, "AC") == 0)
			{
				m_Cplg.Exists = true;
				m_Cplg.Int = 1;
			}
			CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Coupling [%s]", m_Cplg.Text));
			FreeDatum(pDatum);
		}

		// Test-equip-imp
		m_Timp.Int = 1000000;
		m_Timp.Real = 1e+6;
		if((pDatum = RetrieveDatum(M_TIMP, K_SET)))
		{
			m_Timp.Exists = TRUE;
			m_Timp.Real = DECDatVal(pDatum, 0);
			m_Timp.Int = (int)(m_Timp.Real + 0.5);
			CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Test-Equip-Imp [%lf]", m_Timp.Real));
			FreeDatum(pDatum);
		}

		// Strobe-To-Event
		if((pDatum = RetrieveDatum(M_SBEV, K_SET)))
		{
			m_Sbev.Exists = TRUE;
			m_Sbev.Int = INTDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Strobe-to-event [%d]", m_Sbev.Int));
			// Extracting the associated event information
			
			multimap<int, TrigInfo>::const_iterator it = m_Trig.begin();
			
			for (; it != m_Trig.end(); it++)
			{
				// If channel 1, m_Sbev.Int is offset by -1, if channel 2 no offset
				int x = 0;

				if (m_Channumb == 1)
				{
					x = 1;
				}

				if (it->first == m_Sbev.Int - x)
				{
					m_Trlv.Real = it->second.m_dLevel;
					m_Trlv.Exists = true;
					m_Trsl.Int = it->second.m_nSlope;
					m_Trsl.Exists = true;
					// Channel available via it->second.m_nPort
					m_Trgs.Int = it->second.m_nPort;
					m_Trgs.Exists = true;
					// Delay (not used for DSO) available via it->second.m_dDelay
					//m_Trdl.Real = it->second.m_dDelay;
					//m_Trdl.Exists = true;
				}
			}

			FreeDatum(pDatum);
		}

		//EVENT-DELAY 
		if((pDatum = RetrieveDatum(M_DELA, K_SET)))
		{
			m_Trdl.Exists = TRUE;
			m_Trdl.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Event-Delay [%g]", m_Trdl.Real));
		}
		
		// event-time-from and event-time-to
		if((pDatum = RetrieveDatum(M_EVTF, K_SET)))
		{
			m_Evtf.Exists = TRUE;
			m_Evtf.handle = INTDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Strobe-to-event [%d]", m_Evtf.handle));
			// Extracting the associated event information
			
			multimap<int, TrigInfo>::const_iterator it = m_Trig.begin();
		
			for (; it != m_Trig.end(); it++)
			{
				if (it->first == m_Evtf.handle)
				{
					m_Evtf.voltage = it->second.m_dLevel;
					m_Evtf.slope = it->second.m_nSlope;
					m_Evtf.port = it->second.m_nPort;
					m_Evtf.delay = it->second.m_dDelay;
				}
			}
			
			FreeDatum(pDatum);
		}

		if((pDatum = RetrieveDatum(M_EVTT, K_SET)))
		{
			m_Evtt.Exists = TRUE;
			m_Evtt.handle = INTDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Strobe-to-event [%d]", m_Evtt.handle));
			// Extracting the associated event information
			
			multimap<int, TrigInfo>::const_iterator it = m_Trig.begin();
		
			for (; it != m_Trig.end(); it++)
			{
				if (it->first == m_Evtt.handle)
				{
					m_Evtt.voltage = it->second.m_dLevel;
					m_Evtt.slope = it->second.m_nSlope;
					m_Evtt.port = it->second.m_nPort;
					m_Evtt.delay = it->second.m_dDelay;
				}
			}
			
			FreeDatum(pDatum);
		}

		// Extracting modifiers for specific signals
		// ACS: AC SIGNAL
		// DCS: DC SIGNAL
		// PDC: PULSED DC
		// SQW: SQUARE WAVE
		// RPS: RAMP SIGNAL
		// TRI: TRIANGULAR WAVE SIGNAL
		// WAV: WAVEFORM
		// TMI: TIME INTERVAL

		////////////////////////////////
		// Measured Characteristics
		// Voltage
		
		if (m_Noun == N_ACS || m_Noun == N_DCS)
		{
			if((pDatum = RetrieveDatum(M_VOLT, K_SET)))
			{
				m_Volt.Exists = TRUE;
				m_Volt.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Voltage [%lf]", m_Volt.Real));
				FreeDatum(pDatum);
			}
		
			if((pDatum = RetrieveDatum(M_VOLT, K_SRX)))
			{
				m_VoltMax.Exists = TRUE;
				m_VoltMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Voltage Max [%lf]", m_VoltMax.Real));
				FreeDatum(pDatum);
			}
			if((pDatum = RetrieveDatum(M_VOLT, K_SRN)))
			{
				m_VoltMin.Exists = TRUE;
				m_VoltMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Voltage Min [%lf]", m_VoltMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Voltage-p
		if (m_Noun == N_ACS || m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_VLPK, K_SET)))
			{
				m_Vlpk.Exists = TRUE;
				m_Vlpk.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Voltage-p [%lf]", m_Vlpk.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VLPK, K_SRX)))
			{
				m_VlpkMax.Exists = TRUE;
				m_VlpkMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Voltage-p Max [%lf]", m_VlpkMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VLPK, K_SRN)))
			{
				m_VlpkMin.Exists = TRUE;
				m_VlpkMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Voltage-p Min [%lf]", m_VlpkMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Prf
		if (m_Noun == N_PDC)
		{
			if((pDatum = RetrieveDatum(M_PRFR, K_SET)))
			{
				m_Prfr.Exists = TRUE;
				m_Prfr.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Prf [%lf]", m_Prfr.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PRFR, K_SRX)))
			{
				m_PrfrMax.Exists = TRUE;
				m_PrfrMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Prf Max [%lf]", m_PrfrMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PRFR, K_SRN)))
			{
				m_PrfrMin.Exists = TRUE;
				m_PrfrMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Prf Min [%lf]", m_PrfrMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Voltage-pp
		if (m_Noun == N_ACS || m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI || m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_VLPP, K_SET)))
			{
				m_Vlpp.Exists = TRUE;
				m_Vlpp.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Voltage-pp [%lf]", m_Vlpp.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VLPP, K_SRX)))
			{
				m_VlppMax.Exists = TRUE;
				m_VlppMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Votlage-pp Max [%lf]", m_VlppMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VLPP, K_SRN)))
			{
				m_VlppMin.Exists = TRUE;
				m_VlppMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Voltage-pp Min [%lf]", m_VlppMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Freq
		if (m_Noun == N_ACS || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_FREQ, K_SET)))
			{
				m_Freq.Exists = TRUE;
				m_Freq.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Freq [%lf]", m_Freq.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_FREQ, K_SRX)))
			{
				m_FreqMax.Exists = TRUE;
				m_FreqMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Freq Max [%lf]", m_FreqMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_FREQ, K_SRN)))
			{
				m_FreqMin.Exists = TRUE;
				m_FreqMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Freq Min [%lf]", m_FreqMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Period
		if (m_Noun == N_ACS || m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_PERI, K_SET)))
			{
				m_Peri.Exists = TRUE;
				m_Peri.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Period %lf", m_Peri.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PERI, K_SRX)))
			{
				m_PeriMax.Exists = TRUE;
				m_PeriMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Period Max %lf", m_PeriMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PERI, K_SRN)))
			{
				m_PeriMin.Exists = TRUE;
				m_PeriMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Period Min %lf", m_PeriMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Rise-time
		if (m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_RISE, K_SET)))
			{
				m_Rise.Exists = TRUE;
				m_Rise.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Rise-Time %lf", m_Rise.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_RISE, K_SRX)))
			{
				m_RiseMax.Exists = TRUE;
				m_RiseMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Rise-Time Max %lf", m_RiseMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_RISE, K_SRN)))
			{
				m_RiseMin.Exists = TRUE;
				m_RiseMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Rise-Time Min %lf", m_RiseMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Fall-time
		if (m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_FALL, K_SET)))
			{
				m_Fall.Exists = TRUE;
				m_Fall.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Fall-Time %lf", m_Fall.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_FALL, K_SRX)))
			{
				m_FallMax.Exists = TRUE;
				m_FallMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Fall-Time Max %lf", m_FallMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_FALL, K_SRN)))
			{
				m_FallMin.Exists = TRUE;
				m_FallMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Fall-Time Min %lf", m_FallMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Pulse-width
		if (m_Noun == N_PDC)
		{
			if((pDatum = RetrieveDatum(M_PLWD, K_SET)))
			{
				m_Plwd.Exists = TRUE;
				m_Plwd.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Pulse-Width %lf", m_Plwd.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PLWD, K_SRX)))
			{
				m_PlwdMax.Exists = TRUE;
				m_PlwdMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Pulse-Width Max %lf", m_PlwdMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PLWD, K_SRN)))
			{
				m_PlwdMin.Exists = TRUE;
				m_PlwdMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Pulse-Width Min %lf", m_PlwdMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Duty-cycle
		if (m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_DUTY, K_SET)))
			{
				m_Duty.Exists = TRUE;
				m_Duty.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Duty-cycle %lf", m_Duty.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_DUTY, K_SRX)))
			{
				m_DutyMax.Exists = TRUE;
				m_DutyMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Duty-cycle Max %lf", m_DutyMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_DUTY, K_SRN)))
			{
				m_DutyMin.Exists = TRUE;
				m_DutyMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Duty-cycle Min %lf", m_DutyMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Preshoot
		if (m_Noun == N_PDC)
		{
			if((pDatum = RetrieveDatum(M_PSHT, K_SET)))
			{
				m_Psht.Exists = TRUE;
				m_Psht.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Preshoot %lf", m_Psht.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PSHT, K_SRX)))
			{
				m_PshtMax.Exists = TRUE;
				m_PshtMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Preshoot Max %lf", m_PshtMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PSHT, K_SRN)))
			{
				m_PshtMin.Exists = TRUE;
				m_PshtMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Preshoot Min %lf", m_PshtMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Overshoot
		if (m_Noun == N_PDC)
		{
			if((pDatum = RetrieveDatum(M_OVER, K_SET)))
			{
				m_Over.Exists = TRUE;
				m_Over.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Overshoot %lf", m_Over.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_OVER, K_SRX)))
			{
				m_OverMax.Exists = TRUE;
				m_OverMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Overshoot Max %lf", m_OverMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_OVER, K_SRN)))
			{
				m_OverMin.Exists = TRUE;
				m_OverMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Overshoot Min %lf", m_OverMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Av-voltage
		if (m_Noun == N_ACS)
		{
			if((pDatum = RetrieveDatum(M_VLAV, K_SET)))
			{
				m_Vlav.Exists = TRUE;
				m_Vlav.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Av-Voltage %lf", m_Vlav.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VLAV, K_SRX)))
			{
				m_VlavMax.Exists = TRUE;
				m_VlavMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Av-Voltage Max %lf", m_VlavMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VLAV, K_SRN)))
			{
				m_VlavMin.Exists = TRUE;
				m_VlavMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Av-Voltage Min %lf", m_VlavMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Voltage-p-pos
		if (m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_VPKP, K_SET)))
			{
				m_Vpkp.Exists = TRUE;
				m_Vpkp.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Voltage-p-pos %lf", m_Vpkp.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VPKP, K_SRX)))
			{
				m_VpkpMax.Exists = TRUE;
				m_VpkpMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Votlage-p-pos Max %lf", m_VpkpMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VPKP, K_SRN)))
			{
				m_VpkpMin.Exists = TRUE;
				m_VpkpMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Voltage-p-pos Min %lf", m_VpkpMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Voltage-p-neg
		if (m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_VPKN, K_SET)))
			{
				m_Vpkn.Exists = TRUE;
				m_Vpkn.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Voltage-p-neg %lf", m_Vpkn.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VPKN, K_SRX)))
			{
				m_VpknMax.Exists = TRUE;
				m_VpknMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Votlage-p-neg Max %lf", m_VpknMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_VPKN, K_SRN)))
			{
				m_VpknMin.Exists = TRUE;
				m_VpknMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Voltage-p-neg Min %lf", m_VpknMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Pos-pulse-width
		if (m_Noun == N_PDC)
		{
			if((pDatum = RetrieveDatum(M_PPWT, K_SET)))
			{
				m_Ppwt.Exists = TRUE;
				m_Ppwt.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Pos-pulse-width %lf", m_Ppwt.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PPWT, K_SRX)))
			{
				m_PpwtMax.Exists = TRUE;
				m_PpwtMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Pos-pulse-width Max %lf", m_PpwtMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_PPWT, K_SRN)))
			{
				m_PpwtMin.Exists = TRUE;
				m_PpwtMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Pos-pulse-width Min %lf", m_PpwtMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Neg-pulse-width
		if (m_Noun == N_PDC)
		{
			if((pDatum = RetrieveDatum(M_NPWT, K_SET)))
			{
				m_Npwt.Exists = TRUE;
				m_Npwt.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Neg-pulse-width %lf", m_Npwt.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_NPWT, K_SRX)))
			{
				m_NpwtMax.Exists = TRUE;
				m_NpwtMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Neg-pulse-width Max %lf", m_NpwtMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_NPWT, K_SRN)))
			{
				m_NpwtMin.Exists = TRUE;
				m_NpwtMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Neg-pulse-width Min %lf", m_NpwtMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Sample
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_SMPL, K_SET)))
			{
				m_Smpl.Exists = TRUE;
				m_Smpl.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Sample %lf", m_Smpl.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_SMPL, K_SRX)))
			{
				m_SmplMax.Exists = TRUE;
				m_SmplMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Sample Max %lf", m_SmplMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_SMPL, K_SRN)))
			{
				m_SmplMin.Exists = TRUE;
				m_SmplMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Sample Min %lf", m_SmplMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Math
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_MATH, K_SET)))
			{
				m_Math.Exists = TRUE;
				m_Smpl.Exists = TRUE;
				m_Smpl.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Math %lf", m_Smpl.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_MATH, K_SRX)))
			{
				m_MathMax.Exists = TRUE;
				m_SmplMax.Exists = TRUE;
				m_SmplMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Math Max %lf", m_SmplMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_MATH, K_SRN)))
			{
				m_MathMin.Exists = TRUE;
				m_SmplMin.Exists = TRUE;
				m_SmplMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Math Min %lf", m_SmplMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Compare-wave
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_CMWV, K_SET)))
			{
				m_Cmwv.Exists = TRUE;
				m_Smpl.Exists = TRUE;
				m_Smpl.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Compare-wave %lf", m_Smpl.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_CMWV, K_SRX)))
			{
				m_CmwvMax.Exists = TRUE;
				m_SmplMax.Exists = TRUE;
				m_SmplMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Compare-wave Max %lf", m_SmplMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_CMWV, K_SRN)))
			{
				m_CmwvMin.Exists = TRUE;
				m_SmplMin.Exists = TRUE;
				m_SmplMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Compare-wave Min %lf", m_SmplMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Load-wave
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_LDVW, K_SET)))
			{
				m_Ldvw.Exists = TRUE;
				m_Smpl.Exists = TRUE;
				m_Smpl.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Load-wave %lf", m_Smpl.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_LDVW, K_SRX)))
			{
				m_LdvwMax.Exists = TRUE;
				m_SmplMax.Exists = TRUE;
				m_SmplMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Load-wave Max %lf", m_SmplMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_LDVW, K_SRN)))
			{
				m_LdvwMin.Exists = TRUE;
				m_SmplMin.Exists = TRUE;
				m_SmplMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Load-wave Min %lf", m_SmplMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Save-wave
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_SVWV, K_SET)))
			{
				m_Svwv.Exists = TRUE;
				m_Smpl.Exists = TRUE;
				m_Smpl.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Save-wave %lf", m_Smpl.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_SVWV, K_SRX)))
			{
				m_SvwvMax.Exists = TRUE;
				m_SmplMax.Exists = TRUE;
				m_SmplMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Save-wave Max %lf", m_SmplMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_SVWV, K_SRN)))
			{
				m_SvwvMin.Exists = TRUE;
				m_SmplMin.Exists = TRUE;
				m_SmplMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Save-wave Min %lf", m_SmplMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Time
		if (m_Noun == N_TMI)
		{
			if((pDatum = RetrieveDatum(M_TIME, K_SET)))
			{
				m_Time.Exists = TRUE;
				m_Time.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Time %lf", m_Time.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_TIME, K_SRX)))
			{
				m_TimeMax.Exists = TRUE;
				m_TimeMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Time Max %lf", m_TimeMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_TIME, K_SRN)))
			{
				m_TimeMin.Exists = TRUE;
				m_TimeMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Time Min %lf", m_TimeMin.Real));
				FreeDatum(pDatum);
			}
		}

		/////////////////////////////////
		// Standard modifiers
		// Dc-offset
		if (m_Noun == N_ACS || m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI || m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_DCOF, K_SET)))
			{
				m_Dcof.Exists = TRUE;
				m_Dcof.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Dc-offset %lf", m_Dcof.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_DCOF, K_SRX)))
			{
				m_Dcof.Exists = TRUE;
				m_Dcof.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Dc-offset %lf", m_Dcof.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_DCOF, K_SRN)))
			{
				m_Dcof.Exists = TRUE;
				m_Dcof.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Dc-offset %lf", m_Dcof.Real));
				FreeDatum(pDatum);
			}
		}

		// Freq-window
		if (m_Noun == N_ACS)
		{
			if((pDatum = RetrieveDatum(M_FRQW, K_SRX)))
			{
				m_FrqwMax.Exists = TRUE;
				m_FrqwMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Freq-window Max %lf", m_FrqwMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_FRQW, K_SRN)))
			{
				m_FrqwMin.Exists = TRUE;
				m_FrqwMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Freq-window Min %lf", m_FrqwMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Bandwidth
		if (m_Noun == N_ACS || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_BAND, K_SRX)))
			{
				m_BandMax.Exists = TRUE;
				m_BandMax.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Bandwidth Max%lf", m_BandMax.Real));
				FreeDatum(pDatum);
			}

			if((pDatum = RetrieveDatum(M_BAND, K_SRN)))
			{
				m_BandMin.Exists = TRUE;
				m_BandMin.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Bandwidth Min%lf", m_BandMin.Real));
				FreeDatum(pDatum);
			}
		}

		// Sample-count
		if (m_Noun == N_ACS || m_Noun == N_DCS || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_SCNT, K_SET)))
			{
				m_Scnt.Exists = TRUE;
				m_Scnt.Int = (int)DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Sample-count %d", m_Scnt.Int));
				FreeDatum(pDatum);
			}
		}

		// Trigger-slope
		if (m_Noun == N_ACS || m_Noun == N_DCS || m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_TRSL, K_SET)))
			{
				// Error if both TRIG-SLOPE and event are present
				if (m_Trsl.Exists == TRUE)
				{
					CEMERROR(EB_SEVERITY_ERROR|EB_ACTION_HALT, "[Dso_T]::GetStmtInfoDsoTrigger() - slope information both in Event and Atlas statement\n");
				}
				else
				{
					m_Trsl.Exists = TRUE;
					sprintf (m_Trsl.Text, "%s", GetTXTDatVal(pDatum, 0));
					CEMDEBUG(10,cs_FmtMsg("[Dso_T] - GetStmtInfoDso - Found Trigger-slope [%s]", m_Trsl.Text));
					
					if (strcmp(m_Trsl.Text, "POS") == 0)
					{
						m_Trsl.Int = 1;
					}
					else if (strcmp(m_Trsl.Text, "NEG") == 0)
					{
						m_Trsl.Int = -1;
					}
				}

				FreeDatum(pDatum);
			}
		}

		// Trigger-level
		if (m_Noun == N_ACS || m_Noun == N_DCS || m_Noun == N_PDC || m_Noun == N_SQW || m_Noun == N_RPS || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_TRLV, K_SET)))
			{
				if (m_Trlv.Exists == TRUE)
				{
					CEMERROR(EB_SEVERITY_ERROR|EB_ACTION_HALT, "Trigger-level information both in Event and Atlas statement\n");
				}
				else
				{
					m_Trlv.Exists = TRUE;
					m_Trlv.Real = DECDatVal(pDatum, 0);
					CEMDEBUG(10,cs_FmtMsg("Found Trigger-level %lf", m_Trlv.Real));
				}

				FreeDatum(pDatum);
			}
		}

		// Preshoot
		if (m_Noun == N_SQW)
		{
			if((pDatum = RetrieveDatum(M_PSHT, K_SET)))
			{
				m_Psht.Exists = TRUE;
				m_Psht.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Preshoot %lf", m_Psht.Real));
				FreeDatum(pDatum);
			}
		}

		// Overshoot
		if (m_Noun == N_SQW)
		{
			if((pDatum = RetrieveDatum(M_OVER, K_SET)))
			{
				m_Over.Exists = TRUE;
				m_Over.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Overshoot %lf", m_Over.Real));
				FreeDatum(pDatum);
			}
		}

		// Pulse-width
		if (m_Noun == N_SQW || m_Noun == N_TRI)
		{
			if((pDatum = RetrieveDatum(M_PLWD, K_SET)))
			{
				m_Plwd.Exists = TRUE;
				m_Plwd.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Pulse-Width %lf", m_Plwd.Real));
				FreeDatum(pDatum);
			}
		}

		// Sample-time
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_SATM, K_SET)))
			{
				m_Satm.Exists = TRUE;
				m_Satm.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Sample-time %lf", m_Satm.Real));
				FreeDatum(pDatum);
			}
		}

		// Resp
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_RESP, K_SET)))
			{
				m_Resp.Exists = TRUE;
				m_Resp.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Resp %lf", m_Resp.Real));
				FreeDatum(pDatum);
			}
		}

		// Save-from
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_SVFM, K_SET)))
			{
				m_Svfm.Exists = TRUE;
				sprintf (m_Svfm.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Save-from %s", m_Svfm.Text));
				FreeDatum(pDatum);
			}
		}

		// Save-to
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_SVTO, K_SET)))
			{
				m_Svto.Exists = TRUE;
				sprintf (m_Svto.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Save-to %s", m_Svto.Text));
				FreeDatum(pDatum);
			}
		}

		// Load-from
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_LDFM, K_SET)))
			{
				m_Ldfm.Exists = TRUE;
				sprintf (m_Ldfm.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Load-from %s", m_Ldfm.Text));
				FreeDatum(pDatum);
			}
		}

		// Compare-ch
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_CMCH, K_SET)))
			{
				m_Cmch.Exists = TRUE;
				sprintf (m_Cmch.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Compare-ch %s", m_Cmch.Text));
				FreeDatum(pDatum);
			}
		}

		// Compare-to
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_CMTO, K_SET)))
			{
				m_Cmto.Exists = TRUE;
				sprintf (m_Cmto.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Compare-to %s", m_Cmto.Text));
				FreeDatum(pDatum);
			}
		}

		// Allowance
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_ALLW, K_SET)))
			{
				m_Allw.Exists = TRUE;
				m_Allw.Real = DECDatVal(pDatum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Allowance %g", m_Allw.Real));
				FreeDatum(pDatum);
			}
		}

		// Add-from
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_ADFM, K_SET)))
			{
				m_Adfm.Exists = TRUE;
				sprintf (m_Adfm.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Add-from %s", m_Adfm.Text));
				FreeDatum(pDatum);
			}
		}

		// Add-to
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_ADTO, K_SET)))
			{
				m_Adto.Exists = TRUE;
				sprintf (m_Adto.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Add-to %s", m_Adto.Text));
				FreeDatum(pDatum);
			}
		}

		// subtract-from
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_SBFM, K_SET)))
			{
				m_Sbfm.Exists = TRUE;
				sprintf (m_Sbfm.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found  subtract-from %s", m_Sbfm.Text));
				FreeDatum(pDatum);
			}
		}

		// Subtract-to
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_SBTO, K_SET)))
			{
				m_Sbto.Exists = TRUE;
				sprintf (m_Sbto.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Subtract-to %s", m_Sbto.Text));
				FreeDatum(pDatum);
			}
		}

		// Multp-from
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_MPFM, K_SET)))
			{
				m_Mpfm.Exists = TRUE;
				sprintf (m_Mpfm.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Multp-from %s", m_Mpfm.Text));
				FreeDatum(pDatum);
			}
		}

		// Multp-to
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_MPTO, K_SET)))
			{
				m_Mpto.Exists = TRUE;
				sprintf (m_Mpto.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Multp-to %s", m_Mpto.Text));
				FreeDatum(pDatum);
			}
		}

		// Differentiate
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_DIFR, K_SET)))
			{
				m_Difr.Exists = TRUE;
				sprintf (m_Difr.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Differentiate %s", m_Difr.Text));
				FreeDatum(pDatum);
			}
		}

		// Destination
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_DEST, K_SET)))
			{
				m_Dest.Exists = TRUE;
				sprintf (m_Dest.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Destination %s", m_Dest.Text));
				FreeDatum(pDatum);
			}
		}

		// Integrate
		if (m_Noun == N_WAV)
		{
			if((pDatum = RetrieveDatum(M_INTG, K_SET)))
			{
				m_Intg.Exists = TRUE;
				sprintf (m_Intg.Text, "%s", GetTXTDatVal(pDatum, 0));
				CEMDEBUG(10,cs_FmtMsg("Found Integrate %s", m_Intg.Text));
				FreeDatum(pDatum);
			}
		}
	}
	// For Time interval Start and Stop events
	// for Events
	else if ((Fnc>5 && Fnc <=99) || Fnc == 161 || Fnc == 162 || Fnc == 261 || Fnc == 262)
	{
		int nEvou = 0;
		int nEvsl = 1;
		double dVolt = 0;
		int nPort = 0;
		double dDelay = 0;
		
		//EVENT-DELAY 
		if((pDatum = RetrieveDatum(M_DELA, K_SET)))
		{
			m_Trdl.Exists = TRUE;
			m_Trdl.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Event-Delay %g", m_Trdl.Real));
		}

		// 1 //
		if((pDatum = RetrieveDatum(M_EVOU, K_SET)))
		{
			nEvou = INTDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Event-Out %d", nEvou));
			FreeDatum(pDatum);
		}

		// 2 //
		if((pDatum = RetrieveDatum(M_EVSL, K_SET)))
		{
			char sTemp[256]="";
			sprintf (sTemp, "%s", GetTXTDatVal(pDatum, 0));
			
			if (strcmp(sTemp, "NEG") == 0)
			{
				nEvsl = -1;
			}
			
			CEMDEBUG(10,cs_FmtMsg("Found Event Slope %s", sTemp));
			FreeDatum(pDatum);
		}

		// 3 //
		if((pDatum = RetrieveDatum(M_VOLT, K_SET)))
		{
			dVolt = DECDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Voltage %f", dVolt));
			FreeDatum(pDatum);
		}

		// 4 //
		if (Fnc < 99 && Fnc != 6 && Fnc != 7)
		{
			nPort = Fnc%10;
		}
		else if (Fnc == 161 || Fnc == 162 || Fnc == 6 || Fnc == 7)
		{
			nPort = Fnc%5;
		}
		
		CEMDEBUG(10,cs_FmtMsg("Found Trigger channel %d", nPort));
		// 5 //
		if((pDatum = RetrieveDatum(M_EVDL, K_SET)))
		{
			dDelay = DECDatVal(pDatum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Event Delay %f", dDelay));
			FreeDatum(pDatum);
		}

		//o.insert(multimap<int, ModifierValue>::value_type(0, ModifierValue(45, (CComVariant)12.4)));
		m_Trig.insert(multimap<int, TrigInfo>::value_type(nEvou, TrigInfo(nEvsl, dVolt, nPort, dDelay)));

	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateDso()
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CDso_T::InitPrivateDso(void)
{
	CEMDEBUG(10,cs_FmtMsg("[Dso_T] - InitPrivateDso()"));

	sFetchCalled = false;

	m_Freq.Exists = false;
	m_FreqMax.Exists = false;
	m_FreqMin.Exists = false;
	m_Evtf.Exists=false;
	m_Evtt.Exists=false;
	// Prf
	m_Prfr.Exists = false;
	m_PrfrMax.Exists = false;
	m_PrfrMin.Exists = false;
	// Rise-time
	m_Rise.Exists = false;
	m_RiseMax.Exists = false;
	m_RiseMin.Exists = false;
	// Fall-time
	m_Fall.Exists = false;
	m_FallMax.Exists = false;
	m_FallMin.Exists = false;
	// Period
	m_Peri.Exists = false;
	m_PeriMax.Exists = false;
	m_PeriMin.Exists = false;
	// Pulse-Width
	m_Plwd.Exists = false;
	m_PlwdMax.Exists = false;
	m_PlwdMin.Exists = false;
	// Duty-cycle
	m_Duty.Exists = false;
	m_DutyMax.Exists = false;
	m_DutyMin.Exists = false;
	// Preshoot
	m_Psht.Exists = false;
	m_PshtMax.Exists = false;
	m_PshtMin.Exists = false;
	// Overshoot
	m_Over.Exists = false;
	m_OverMax.Exists = false;
	m_OverMin.Exists = false;
	// Voltage-pp
	m_Vlpp.Exists = false;
	m_VlppMax.Exists = false;
	m_VlppMin.Exists = false;
	// Voltage-p-pos
	m_Vpkp.Exists = false;
	m_VpkpMax.Exists = false;
	m_VpkpMin.Exists = false;
	// Voltage-p-neg
	m_Vpkn.Exists = false;
	m_VpknMax.Exists = false;
	m_VpknMin.Exists = false;
	// Pos-Pulse-Width
	m_Ppwt.Exists = false;
	m_PpwtMax.Exists = false;
	m_PpwtMin.Exists = false;
	// Neg-Pulse-Width
	m_Npwt.Exists = false;
	m_NpwtMax.Exists = false;
	m_NpwtMin.Exists = false;
	// Av-voltage
	m_Vlav.Exists = false;
	m_VlavMax.Exists = false;
	m_VlavMin.Exists = false;
	// Voltage-p
	m_Vlpk.Exists = false;
	m_VlpkMax.Exists = false;
	m_VlpkMin.Exists = false;
	// Voltage
	m_Volt.Exists = false;
	m_VoltMax.Exists = false;
	m_VoltMin.Exists = false;
	// Sample
	m_Smpl.Exists = false;
	m_SmplMax.Exists = false;
	m_SmplMin.Exists = false;
	// Math
	m_Math.Exists = false;
	m_MathMax.Exists = false;
	m_MathMin.Exists = false;
	// Compare-Wave
	m_Cmwv.Exists = false;
	m_CmwvMax.Exists = false;
	m_CmwvMin.Exists = false;
	// Load-Wave
	m_Ldvw.Exists = false;
	m_LdvwMax.Exists = false;
	m_LdvwMin.Exists = false;
	// Save-Wave
	m_Svwv.Exists = false;
	m_SvwvMax.Exists = false;
	m_SvwvMin.Exists = false;
	/////////////////////////
	// Dc-offset
	m_Dcof.Exists = false;
	// Freq-window
	m_FrqwMax.Exists = false;
	m_FrqwMin.Exists = false;
	// Sample-count
	m_Scnt.Exists = false;
	// Trig-level
	m_Trlv.Exists = false;
	// Trig-slope
	m_Trsl.Exists = false;
	// Bandwidth
	m_BandMax.Exists = false;
	m_BandMin.Exists = false;
	// Max-time
	m_Maxt.Exists = false;
	// Coulpling
	m_Cplg.Exists = false;
	// Test-Equip-Imp
	m_Timp.Exists = false;
	// Sample-time
	m_Satm.Exists = false;
	// Resp
	m_Resp.Exists = false;
	// Save-from
	m_Svfm.Exists = false;
	// Save-to
	m_Svto.Exists = false;
	// Load-from
	m_Ldfm.Exists = false;
	// Compare-ch
	m_Cmch.Exists = false;
	// Compare-to
	m_Cmto.Exists = false;
	// Allowance
	m_Allw.Exists = false;
	// Add-from
	m_Adfm.Exists = false;
	// Add-to
	m_Adto.Exists = false;
	// Subtract-from
	m_Sbfm.Exists = false;
	// Subtract-to
	m_Sbto.Exists = false;
	// Multp-from
	m_Mpfm.Exists = false;
	// Multp-to
	m_Mpto.Exists = false;
	// Differentiate
	m_Difr.Exists = false;
	// Destination
	m_Dest.Exists = false;
	// Integrate
	m_Intg.Exists = false;
	// Time
	m_Time.Exists = false;
	// Strobe-To-Event
	m_Sbev.Exists = false;
	// Trig source and delay
	m_Trgs.Exists = false;
	m_Trdl.Exists = false;
    //////////////////////////////////////////////////////////
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataDso()
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CDso_T::NullCalDataDso(void)
{
	CEMDEBUG(10,cs_FmtMsg("[Dso_T] - NullCalDataDso()"));
    
	m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
    return;
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


