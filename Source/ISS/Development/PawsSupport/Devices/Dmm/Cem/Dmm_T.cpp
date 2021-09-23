//345678901234567890123456789012345678901234567890123456789012345678901234567890
////////////////////////////////////////////////////////////////////////////////
// File:	    Dmm_T.cpp
//
// Date:	    11-OCT-05
//
// Purpose:	    Instrument Driver for Dmm
//
// Instrument:	Dmm  <Device Description> (<device Type>)
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
// ---  ------------------------------------------------------------------------
//   1  sensor (voltage)   dc signal
// 101  sensor (voltage)   dc signal external trigger
//   2  sensor (voltage)   ac signal
// 102  sensor (voltage)   ac signal external trigger
//  22  sensor (voltage-p) ac signal
// 122  sensor (voltage-p) ac signal external trigger
//  23  sensor (voltage-pp) ac signal
// 123  sensor (voltage-pp) ac signal external trigger
//   3  sensor (res) impedance
// 103  sensor (res) impedance external trigger
//   4  sensor (freq) ac signal
// 104  sensor (freq) ac signal external trigger
//   5  sensor (current) ac signal
// 105  sensor (current) ac signal external trigger
//   6  sensor (current) dc signal
// 106  sensor (current) dc signal external trigger
//   7  sensor (period) ac signal
// 107  sensor (period) ac signal external trigger
//   8  sensor (voltage-ratio) dc signal
// 108  sensor (voltage-ratio) dc signal external trigger
//   9  sensor (av-voltage) dc signal
// 109  sensor (av-voltage) dc signal external trigger
//  10  sensor (av-voltage) ac signal
// 110  sensor (av-voltage) ac signal external trigger
//  11  sensor (av-current) dc signal
// 111  sensor (av-current) dc signal external trigger
//  12  sensor (av-current) ac signal
// 112  sensor (av-current) ac signal external trigger
//  13  sensor (ac-comp) dc signal
// 113  sensor (ac-comp) dc signal external trigger
//  14  sensor (ac-comp-freq) dc signal
// 114  sensor (ac-comp-freq) dc signal external trigger
//  15  sensor (dc-offset) ac signal
// 115  sensor (dc-offset) ac signal external trigger
//  16  sensor (voltage) square wave
// 116  sensor (voltage) square wave external trigger
//  27  sensor (voltage-p) square wave
// 127  sensor (voltage-p) square wave external trigger
//  28  sensor (voltage-pp) square wave
// 128  sensor (voltage-pp) square wave external trigger
//  17  sensor (freq) square wave
// 117  sensor (freq) square wave external trigger
//  18  sensor (period) square wave
// 118  sensor (period) square wave external trigger
//  19  sensor (voltage) triangular wave signal
// 119  sensor (voltage) triangular wave signal external trigger
//  25  sensor (voltage-p) triangular wave signal
// 125  sensor (voltage-p) triangular wave signal external trigger
//  26  sensor (voltage-pp) triangular wave signal
// 126  sensor (voltage-pp) triangular wave signal external trigger
//  20  sensor (freq) triangular wave signal
// 120  sensor (freq) triangular wave signal external trigger
//  21  sensor (period) triangular wave signal
// 121  sensor (period) triangular wave signal external trigger
// 202  event monitor (voltage) ac signal
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline      
// 1.2.1.1  14FEB07  Setup with a STROBE-TO-EVENT was calling initiate,
//                    added code in Initiate() to ignore setup.
//					 Commented in .cpp the define for IEEE716_89, this is no
//						longer needed.
// 1.4.0.1  02JUL07  Updated so CEM would properly retrieve RES modifier if 
//                   MIN or MAX is used. PCR 168, PCR 169
// 1.6.1    05FEB09  Added FOUR-WIRE modifier and added code to process 4 wire
//                   functionality
// 1.6.2    08APR09  Modified code to allow auto ranging of RES measurement when
//                   doing hot setup.
//                   Modified InitPrivateDmm to reset event variables only on 
//                   DISABLE EVENT or REMOVE ALL
//                   Changed GetDatum call to RetrieveDatum to get rid of stale 
//                   values.
// 1.6.3.0  06MAY11  Corrected assignment of event delay value for STR10329.
// 1.6.4.0  01JUN11  Changed voltage scaling. Previously all values were normalized to
//                   Vpk. This is changed to RMS.
////////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Dmm_T.h"
#include <math.h>
// Local Defines

// Function codes

#define FNC_VOLTS_DC           1
#define FNC_VOLTS_DC_TRG     101
#define FNC_VOLTS_AC           2
#define FNC_VOLTS_AC_TRG     102
#define FNC_VOLTS_AC_P        22
#define FNC_VOLTS_AC_P_TRG   122
#define FNC_VOLTS_AC_PP       23
#define FNC_VOLTS_AC_PP_TRG  123
#define FNC_VOLTS_AC_R        24
#define FNC_VOLTS_AC_R_TRG   124
#define FNC_IMP                3
#define FNC_IMP_TRG          103
#define FNC_FREQ_AC            4
#define FNC_FREQ_AC_TRG      104
#define FNC_CURR_AC            5
#define FNC_CURR_AC_TRG      105
#define FNC_CURR_DC            6
#define FNC_CURR_DC_TRG      106
#define FNC_PERIOD_AC          7
#define FNC_PERIOD_AC_TRG    107
#define FNC_VOLTS_DC_R         8
#define FNC_VOLTS_DC_R_TRG   108
#define FNC_VOLTS_DC_AV        9
#define FNC_VOLTS_DC_AV_TRG  109
#define FNC_VOLTS_AC_AV       10
#define FNC_VOLTS_AC_AV_TRG  110
#define FNC_CURR_DC_AV        11
#define FNC_CURR_DC_AV_TRG   111
#define FNC_CURR_AC_AV        12
#define FNC_CURR_AC_AV_TRG   112
#define FNC_AC_COMP_DC        13
#define FNC_AC_COMP_DC_TRG   113
#define FNC_AC_COMP_DC_F      14
#define FNC_AC_COMP_DC_F_TRG 114
#define FNC_DC_OFF_AC         15
#define FNC_DC_OFF_AC_TRG    115

#define FNC_VOLTS_SQ          16
#define FNC_VOLTS_SQ_TRG     116
#define FNC_VOLTS_SQ_P        27
#define FNC_VOLTS_SQ_P_TRG   127
#define FNC_VOLTS_SQ_PP       28
#define FNC_VOLTS_SQ_PP_TRG  128
#define FNC_FREQ_SQ           17
#define FNC_FREQ_SQ_TRG      117
#define FNC_PERIOD_SQ         18
#define FNC_PERIOD_SQ_TRG    118

#define FNC_VOLTS_TR          19
#define FNC_VOLTS_TR_TRG     119 
#define FNC_VOLTS_TR_P        25
#define FNC_VOLTS_TR_P_TRG   125
#define FNC_VOLTS_TR_PP       26
#define FNC_VOLTS_TR_PP_TRG  126
#define FNC_FREQ_TR           20
#define FNC_FREQ_TR_TRG      120
#define FNC_PERIOD_TR         21
#define FNC_PERIOD_TR_TRG    121

//Global constants
#define CAL_TIME       (86400 * 365) /* one year */
#define SQRT_2         1.414213562373  //As defined in DmmSdd
#define SQRT_3         1.732050807568  //As defined in DmmSdd

//Attribute Strings
#define ATTRIB_VOLTS_DC          "dc_ampl"
#define ATTRIB_VOLTS_AC          "ac_ampl"
#define ATTRIB_VOLTS_AC_P        "ac_ampl_p"
#define ATTRIB_VOLTS_AC_PP       "ac_ampl_pp"
#define ATTRIB_VOLTS_AC_R        "ac_ratio"
#define ATTRIB_IMP               "resistance"
#define ATTRIB_IMP_4WIRE		 "resistance4Wire"
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

#define ClearModStruct(x) { x.Exists = false; x.Int = 0; x.Dim = 0; x.Real = 0.0;}
// Static Variables

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CDmm_T(int Bus, int PrimaryAdr, int SecondaryAdr,
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
CDmm_T::CDmm_T(char *DeviceName, 
                       int Bus, int PrimaryAdr, int SecondaryAdr,
                       int Dbg, int Sim)
{
    m_Bus = Bus;
    m_PrimaryAdr = PrimaryAdr;
    m_SecondaryAdr = SecondaryAdr;
    m_Dbg = Dbg;
    m_Sim = Sim;
    m_Handle= NULL;
    int Status = 0;

    if(DeviceName)
        strcpy(m_DeviceName,DeviceName);

    InitPrivateDmm(0);
	NullCalDataDmm();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("Dmm Class called with Device [%s], "
                         "Sim %d, Dbg %d", 
                          DeviceName, Sim, Dbg));

    // Initialize the Dmm - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CDmm_T()
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
CDmm_T::~CDmm_T()
{
    // Reset the Dmm
    CEMDEBUG(5,cs_FmtMsg("Dmm Class Destructor called for Device [%s], ",
                          m_DeviceName));


    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusDmm(int Fnc)
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
int CDmm_T::StatusDmm(int Fnc)
{
    int Status = 0;

    // Status action for the Dmm
    CEMDEBUG(5,cs_FmtMsg("StatusDmm (%s) called FNC %d", 
                          m_DeviceName, Fnc));
    // Check for any pending error messages
    if(Fnc < 200)
    {
        IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, m_SignalDescription,
                                       NULL, 0)));
        CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %lf",
                                 m_SignalDescription));
    }
	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupDmm_T(int Fnc)
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
int CDmm_T::SetupDmm(int Fnc)
{
    int  Status = 0;
	char stringinput[1024] = "";
	char tempstring[512] = "";
	double delay = 0.0;

    // Setup action for the Dmm
    CEMDEBUG(5,cs_FmtMsg("SetupDmm (%s) called FNC %d", m_DeviceName, Fnc));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
        return(0);
    
    if (m_SignalActive == true)
		InitPrivateDmm(Fnc);

    if((Status = GetStmtInfoDmm(Fnc)) != 0)
        return(0);

    sprintf(stringinput, "<Signal Name=\"DMM_SIGNAL\" Out=\"Measure\">\n");
    switch(Fnc)
    {
		case FNC_VOLTS_AC:
		case FNC_VOLTS_AC_TRG:
		case FNC_VOLTS_AC_P:
		case FNC_VOLTS_AC_P_TRG:
		case FNC_VOLTS_AC_PP:
		case FNC_VOLTS_AC_PP_TRG:
		case FNC_VOLTS_AC_R:
		case FNC_VOLTS_AC_R_TRG:
		case FNC_PERIOD_AC:
		case FNC_PERIOD_AC_TRG:
		case FNC_FREQ_AC:
		case FNC_FREQ_AC_TRG:
		case FNC_CURR_AC:
		case FNC_CURR_AC_TRG:
		case FNC_CURR_AC_AV:
		case FNC_CURR_AC_AV_TRG:
		case FNC_DC_OFF_AC:
		case FNC_DC_OFF_AC_TRG:
		case FNC_VOLTS_AC_AV:
		case FNC_VOLTS_AC_AV_TRG:
            strcat(stringinput, "<Sinusoid name=\"AC_COMP\" ");
            
            if(m_Volt.Exists)
            {
                sprintf(tempstring, "amplitude=\"%3.3lfV\" ", m_Volt.Real);
                strcat(stringinput,tempstring);
            }
			else if(m_Current.Exists)
            {
                sprintf(tempstring, "amplitude=\"%3.3lfA\" ", m_Current.Real);
                strcat(stringinput,tempstring);
            }

			if(m_Freq.Exists)
            {
				sprintf(tempstring, "frequency=\"%10.2lfHz\" ", m_Freq.Real);
                strcat(stringinput,tempstring);
            }
			else if(m_Period.Exists)
            {
				sprintf(tempstring, "frequency=\"%10.2lfHz\" ", (1/m_Period.Real));
                strcat(stringinput,tempstring);
            }
			strcat(stringinput,"/>\n");

			if(m_DCOffset.Exists)
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\" />\n", m_DCOffset.Real);
                strcat(stringinput,tempstring);
			}
			else
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
                strcat(stringinput,tempstring);
			}

			sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\" />\n");
			strcat(stringinput,tempstring);

			//if(Fnc>=100) //triggered added strobe-to-event test ewl 20090408
			if ((Fnc >= 100) && (TriggerFlag == true) && (m_StrobeToEvent.Exists == true))
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(stringinput,tempstring);

				if (m_Delay.Exists)
					delay = m_Delay.Real;
				else if (m_EventDelay.Exists)
					delay = m_EventDelay.Real;

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%3.3lfs\" In=\"Trig_Signal\" />\n", delay);
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<Instantaneous name=\"Trigger\" nominal=\"1.5V\" condition=\"LT\" In=\"Trigger_Delay\" />\n");
				strcat(stringinput,tempstring);
			}

			if(m_Bandwidth.Exists)
			{
				sprintf(tempstring, "<HighPass name=\"Filter\" cutoff=\"%dHz\" In=\"Signal\" />\n", m_Bandwidth.Int);
				strcat(stringinput,tempstring);
			}
			else
            {
				sprintf(tempstring, "<HighPass name=\"Filter\" cutoff=\"20Hz\" In=\"Signal\" />\n");
				strcat(stringinput,tempstring);
			}

			//add Measure Statement
			//if(Fnc<100) //no triggering added strobe-to-event test ewl 20090408
			if ((Fnc < 100) && (TriggerFlag != true) && (m_StrobeToEvent.Exists == false))
			{
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Filter\" ");
			}
			else
			{
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Filter\" Sync=\"Trigger\" ");
			}
			strcat(stringinput,tempstring);
			
			if(m_RefVolt.Exists)
			{
				sprintf(tempstring, "refVolt=\"%3.3lfV\" ", m_RefVolt.Real);
				strcat(stringinput,tempstring);
			}

			if(m_SampleWidth.Exists)
			{
				sprintf(tempstring, "gateTime=\"%1.5lfs\" ", m_SampleWidth.Real);
				strcat(stringinput,tempstring);
			}

			if(m_SampleCount.Exists)
			{
				sprintf(tempstring, "samples=\"%d\" ", m_SampleCount.Int);
				strcat(stringinput,tempstring);
			}

			if(m_MaxTime.Exists)
			{
				sprintf(tempstring, "maxTime=\"%3.3lfs\" ", m_MaxTime.Real);
				strcat(stringinput,tempstring);
			}

			if (m_AutoRange == true)
			{
				strcat(stringinput, "autorange=\"1\" ");
			}
			else
			{
				strcat(stringinput, "autorange=\"0\" ");
			}
			switch(Fnc)	//add attribute to measure statement
			{
				case FNC_VOLTS_AC:
				case FNC_VOLTS_AC_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_AC);
					break;
				case FNC_VOLTS_AC_P:
				case FNC_VOLTS_AC_P_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_AC_P);
					break;
				case FNC_VOLTS_AC_PP:
				case FNC_VOLTS_AC_PP_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_AC_PP);
					break;
				case FNC_VOLTS_AC_R:
				case FNC_VOLTS_AC_R_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ", ATTRIB_VOLTS_AC_R);
					break;
				case FNC_PERIOD_AC:
				case FNC_PERIOD_AC_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ", ATTRIB_PERIOD_AC);
					break;
				case FNC_FREQ_AC:
				case FNC_FREQ_AC_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ", ATTRIB_FREQ_AC);
					break;
				case FNC_CURR_AC:
				case FNC_CURR_AC_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_CURR_AC);
					break;
				case FNC_CURR_AC_AV:
				case FNC_CURR_AC_AV_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_CURR_AC_AV);
					break;
				case FNC_DC_OFF_AC:
				case FNC_DC_OFF_AC_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_DC_OFF_AC);
					break;
				case FNC_VOLTS_AC_AV:
				case FNC_VOLTS_AC_AV_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_AC_AV);
					break;
			}
			strcat(stringinput,tempstring);
            //complete measure statement
			strcat(stringinput,"/>\n</Signal>\n");
            break;
		case FNC_CURR_DC:
		case FNC_CURR_DC_TRG:
		case FNC_VOLTS_DC:
		case FNC_VOLTS_DC_TRG:
		case FNC_VOLTS_DC_R:
		case FNC_VOLTS_DC_R_TRG:
		case FNC_VOLTS_DC_AV:
		case FNC_VOLTS_DC_AV_TRG:
		case FNC_CURR_DC_AV:
		case FNC_CURR_DC_AV_TRG:
		case FNC_AC_COMP_DC:
		case FNC_AC_COMP_DC_TRG:
		case FNC_AC_COMP_DC_F:
		case FNC_AC_COMP_DC_F_TRG:
            if(m_Volt.Exists == true)
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\" />\n", m_Volt.Real);
                strcat(stringinput,tempstring);
			}
			else if(m_Current.Exists == true)
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfA\" />\n", m_Current.Real);
                strcat(stringinput,tempstring);
			}
			else
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" />\n");
                strcat(stringinput,tempstring);
			}
			
			strcat(stringinput, "<Sinusoid name=\"AC_COMP\" ");
                
			if(m_ACComp.Exists)
            {
				sprintf(tempstring, "amplitude=\"%3.3lfV\" ", m_ACComp.Real);
                strcat(stringinput,tempstring);
            }
			if(m_ACCompFreq.Exists)
            {
				sprintf(tempstring, "frequency=\"%10.2lfHz\" ", m_ACCompFreq.Real);
                strcat(stringinput,tempstring);
            }
			strcat(stringinput,"/>\n");

			sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\" />\n");
			strcat(stringinput,tempstring);

			//if(Fnc>=100) //triggered
			if((Fnc>=100) && (TriggerFlag == true))
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%3.3lfs\" In=\"Trig_Signal\" />\n", m_Delay.Real);
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<Instantaneous name=\"Trigger\" nominal=\"1.5V\" condition=\"LT\" In=\"Trigger_Delay\" />\n");
				strcat(stringinput,tempstring);
			}

			if(m_Bandwidth.Exists)
			{
				sprintf(tempstring, "<HighPass name=\"Filter\" cutoff=\"%dHz\" In=\"Signal\" />\n", m_Bandwidth.Int);
				strcat(stringinput,tempstring);
			}
			else
            {
				sprintf(tempstring, "<HighPass name=\"Filter\" cutoff=\"20Hz\" In=\"Signal\" />\n");
				strcat(stringinput,tempstring);
			}
			
			//add Measure Statement
			//if(Fnc<100) //no triggering added strobe-to-event test ewl 20090408
			if ((Fnc<100) && (TriggerFlag != true) && (m_StrobeToEvent.Exists == false))
			{
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Filter\" ");
			}
			else
			{
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Filter\" Sync=\"Trigger\" ");
			}
			strcat(stringinput,tempstring);

			if(m_RefVolt.Exists)
			{
				sprintf(tempstring, "refVolt=\"%3.3lfV\" ", m_RefVolt.Real);
				strcat(stringinput,tempstring);
			}

			if(m_SampleWidth.Exists)
			{
				sprintf(tempstring, "gateTime=\"%1.5lfs\" ", m_SampleWidth.Real);
				strcat(stringinput,tempstring);
			}

			if(m_SampleCount.Exists)
			{
				sprintf(tempstring, "samples=\"%d\" ", m_SampleCount.Int);
				strcat(stringinput,tempstring);
			}
			if(m_MaxTime.Exists)
			{
				sprintf(tempstring, "maxTime=\"%3.3lfs\" ", m_MaxTime.Real);
				strcat(stringinput,tempstring);
			}

			if (m_AutoRange == true)
			{
				strcat(stringinput, "autorange=\"1\" ");
			}
			else
			{
				strcat(stringinput, "autorange=\"0\" ");
			}

			switch(Fnc)	//add attribute to measure statement
			{
				case FNC_VOLTS_DC:
				case FNC_VOLTS_DC_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_DC);
					break;
				case FNC_CURR_DC:
				case FNC_CURR_DC_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_CURR_DC);
					break;
				case FNC_VOLTS_DC_R:
				case FNC_VOLTS_DC_R_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_DC_R);
					break;
				case FNC_VOLTS_DC_AV:
				case FNC_VOLTS_DC_AV_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_DC_AV);
					break;
				case FNC_CURR_DC_AV:
				case FNC_CURR_DC_AV_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_CURR_DC_AV);
					break;
				case FNC_AC_COMP_DC:
				case FNC_AC_COMP_DC_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_AC_COMP_DC);
					break;
				case FNC_AC_COMP_DC_F:
				case FNC_AC_COMP_DC_F_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_AC_COMP_DC_F);
					break;
			}
			strcat(stringinput,tempstring);
            //complete measure statement
			strcat(stringinput,"/>\n</Signal>\n");
            break;
		case FNC_IMP:
		case FNC_IMP_TRG:
			strcat(stringinput, "<Load name=\"Impedance\" ");
            
			if(m_Res.Exists)
            {
				sprintf(tempstring, "resistance=\"%12.3lfOhm\" ", m_Res.Real);
                strcat(stringinput,tempstring);
            }
			
			strcat(stringinput,"/>\n");
			//if(Fnc>=100) //triggered added strobe-to-event test ewl 20090408
			if ((Fnc >= 100) && (TriggerFlag == true) && (m_StrobeToEvent.Exists == true))
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%3.3lfs\" In=\"Trig_Signal\" />\n", m_EventDelay.Real);
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<Instantaneous name=\"Trigger\" nominal=\"1.5V\" condition=\"LT\" In=\"Trigger_Delay\" />\n");
				strcat(stringinput,tempstring);
			}

			sprintf(tempstring, "<Measure name=\"Measure\" As=\"Impedance\" ");
			strcat(stringinput,tempstring);
			if(m_RefRes.Exists)
			{
				sprintf(tempstring, "refRes=\"%12.3lfOhm\" ", m_RefRes.Real);
				strcat(stringinput,tempstring);
			}
			if(m_SampleCount.Exists)
			{
				sprintf(tempstring, "samples=\"%d\" ", m_SampleCount.Int);
				strcat(stringinput,tempstring);
			}
			if(m_MaxTime.Exists)
			{
				sprintf(tempstring, "maxTime=\"%3.3lfs\" ", m_MaxTime.Real);
				strcat(stringinput,tempstring);
			}
			if (m_AutoRange == true)
			{
				strcat(stringinput, "autorange=\"1\" ");
			}
			else
			{
				strcat(stringinput, "autorange=\"0\" ");
			}

			if(Fnc==FNC_IMP_TRG)
			{
				sprintf(tempstring, "Sync=\"Trigger\" ");
				strcat(stringinput,tempstring);
			}
			// addition 05FEB09 - process four-wire modifier
			if (m_FourWire.Exists)
				sprintf(tempstring, "Attribute=\"%s\" />\n</Signal>\n", ATTRIB_IMP_4WIRE);
			else
				sprintf(tempstring, "Attribute=\"%s\" />\n</Signal>\n",ATTRIB_IMP);
			strcat(stringinput,tempstring);
			break;
		case FNC_VOLTS_SQ:
		case FNC_VOLTS_SQ_TRG:
		case FNC_VOLTS_SQ_P:
		case FNC_VOLTS_SQ_P_TRG:
		case FNC_VOLTS_SQ_PP:
		case FNC_VOLTS_SQ_PP_TRG:
		case FNC_FREQ_SQ:
		case FNC_FREQ_SQ_TRG:
		case FNC_PERIOD_SQ:
		case FNC_PERIOD_SQ_TRG:
            strcat(stringinput, "<Squarewave name=\"AC_COMP\" ");
            
            if(m_Volt.Exists)
            {
                sprintf(tempstring, "amplitude=\"%3.3lfV\" ", m_Volt.Real);
                strcat(stringinput,tempstring);
            }
			else if(m_Current.Exists)
            {
                sprintf(tempstring, "amplitude=\"%3.3lfA\" ", m_Current.Real);
                strcat(stringinput,tempstring);
            }
			if(m_Freq.Exists)
            {
				sprintf(tempstring, "period=\"%10.2lfs\" ", 1./m_Freq.Real);
                strcat(stringinput,tempstring);
            }
			else if(m_Period.Exists)
            {
				sprintf(tempstring, "period=\"%10.2lfs\" ", m_Period.Real);
                strcat(stringinput,tempstring);
            }
			strcat(stringinput,"/>\n");

			if(m_DCOffset.Exists)
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\" />\n", m_DCOffset.Real);
                strcat(stringinput, tempstring);
			}
			else
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
                strcat(stringinput,tempstring);
			}

			sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\" />\n");
			strcat(stringinput,tempstring);

			//if(Fnc>=100) //triggered added strobe-to-event test ewl 20090408
			if ((Fnc >= 100) && (TriggerFlag == true) && (m_StrobeToEvent.Exists == true))
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%3.3lfs\" In=\"Trig_Signal\" />\n", m_EventDelay.Real);
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<Instantaneous name=\"Trigger\" nominal=\"1.5V\" condition=\"LT\" In=\"Trigger_Delay\" />\n");
				strcat(stringinput,tempstring);
			}

			if(m_Bandwidth.Exists)
			{
				sprintf(tempstring, "<HighPass name=\"Filter\" cutoff=\"%dHz\" In=\"Signal\" />\n", m_Bandwidth.Int);
				strcat(stringinput,tempstring);
			}
			else
            {
				sprintf(tempstring, "<HighPass name=\"Filter\" cutoff=\"20Hz\" In=\"Signal\" />\n");
				strcat(stringinput,tempstring);
			}
			
			//add Measure Statement
			//if(Fnc<100) //no triggering
			if ((Fnc < 100) && (TriggerFlag != true) && (m_StrobeToEvent.Exists == false))
			{
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Filter\" ");
			}
			else
			{
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Filter\" Sync=\"Trigger\" ");
			}
			strcat(stringinput,tempstring);
			if(m_MaxTime.Exists)
			{
				sprintf(tempstring, "maxTime=\"%3.3lfs\" ", m_MaxTime.Real);
				strcat(stringinput,tempstring);
			}

			if (m_AutoRange == true)
			{
				strcat(stringinput, "autorange=\"1\" ");
			}
			else
			{
				strcat(stringinput, "autorange=\"0\" ");
			}

			switch(Fnc)	//add attribute to measure statement
			{
				case FNC_VOLTS_SQ:
				case FNC_VOLTS_SQ_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_SQ);
					break;
				case FNC_VOLTS_SQ_P:
				case FNC_VOLTS_SQ_P_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_SQ_P);
					break;
				case FNC_VOLTS_SQ_PP:
				case FNC_VOLTS_SQ_PP_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_SQ_PP);
					break;
				case FNC_FREQ_SQ:
				case FNC_FREQ_SQ_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_FREQ_SQ);
					break;
				case FNC_PERIOD_SQ:
				case FNC_PERIOD_SQ_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_PERIOD_SQ);
					break;
			}
			strcat(stringinput,tempstring);
            //complete measure statement
			strcat(stringinput,"/>\n</Signal>\n");
            break;
		case FNC_VOLTS_TR:
		case FNC_VOLTS_TR_TRG:
		case FNC_VOLTS_TR_P:
		case FNC_VOLTS_TR_P_TRG:
		case FNC_VOLTS_TR_PP:
		case FNC_VOLTS_TR_PP_TRG:
		case FNC_FREQ_TR:
		case FNC_FREQ_TR_TRG:
		case FNC_PERIOD_TR:
		case FNC_PERIOD_TR_TRG:
            strcat(stringinput, "<Triangle name=\"AC_COMP\" ");
            
            if(m_Volt.Exists)
            {
                sprintf(tempstring, "amplitude=\"%3.3lfV\" ", m_Volt.Real);
                strcat(stringinput,tempstring);
            }
			else if(m_Current.Exists)
            {
                sprintf(tempstring, "amplitude=\"%3.3lfA\" ", m_Current.Real);
                strcat(stringinput,tempstring);
            }
			if(m_Freq.Exists)
            {
				sprintf(tempstring, "period=\"%10.2lfs\" ", 1./m_Freq.Real);
                strcat(stringinput,tempstring);
            }
			else if(m_Period.Exists)
            {
				sprintf(tempstring, "period=\"%10.2lfs\" ", m_Period.Real);
                strcat(stringinput,tempstring);
            }
			strcat(stringinput,"/>\n");

			if(m_DCOffset.Exists)
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%3.3lfV\" />\n", m_DCOffset.Real);
                strcat(stringinput,tempstring);
			}
			else
			{
				sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
                strcat(stringinput,tempstring);
			}

			sprintf(tempstring, "<Sum name=\"Signal\" In=\"AC_COMP DC_COMP\" />\n");
			strcat(stringinput,tempstring);

			//if(Fnc>=100) //triggered
			if ((Fnc >= 100) && (TriggerFlag == true) && (m_StrobeToEvent.Exists == true))
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%3.3lfs\" In=\"Trig_Signal\" />\n", m_EventDelay.Real);
				strcat(stringinput,tempstring);

				sprintf(tempstring, "<Instantaneous name=\"Trigger\" nominal=\"1.5V\" condition=\"LT\" In=\"Trigger_Delay\" />\n");
				strcat(stringinput,tempstring);
			}

			if(m_Bandwidth.Exists)
			{
				sprintf(tempstring, "<HighPass name=\"Filter\" cutoff=\"%dHz\" In=\"Signal\" />\n", m_Bandwidth.Int);
				strcat(stringinput,tempstring);
			}
			else
            {
				sprintf(tempstring, "<HighPass name=\"Filter\" cutoff=\"20Hz\" In=\"Signal\" />\n");
				strcat(stringinput,tempstring);
			}
			
			//add Measure Statement
			//if(Fnc<100) //no triggering
			if ((Fnc < 100) && (TriggerFlag != true) && (m_StrobeToEvent.Exists == false))
			{
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Filter\" ");
			}
			else
			{
				sprintf(tempstring, "<Measure name=\"Measure\" As=\"Filter\" Sync=\"Trigger\" ");
			}
			strcat(stringinput,tempstring);
			if(m_MaxTime.Exists)
			{
				sprintf(tempstring, "maxTime=\"%3.3lfs\" ", m_MaxTime.Real);
				strcat(stringinput,tempstring);
			}

			if (m_AutoRange == true)
			{
				strcat(stringinput, "autorange=\"1\" ");
			}
			else
			{
				strcat(stringinput, "autorange=\"0\" ");
			}

			switch(Fnc)	//add attribute to measure statement
			{
				case FNC_VOLTS_TR:
				case FNC_VOLTS_TR_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_TR);
					break;
				case FNC_VOLTS_TR_P:
				case FNC_VOLTS_TR_P_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_TR_P);
					break;
				case FNC_VOLTS_TR_PP:
				case FNC_VOLTS_TR_PP_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_VOLTS_TR_PP);
					break;
				case FNC_FREQ_TR:
				case FNC_FREQ_TR_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_FREQ_TR);
					break;
				case FNC_PERIOD_TR:
				case FNC_PERIOD_TR_TRG:
					sprintf(tempstring, "Attribute=\"%s\" ",ATTRIB_PERIOD_TR);
					break;
			}
			strcat(stringinput,tempstring);
            //complete measure statement
			strcat(stringinput,"/>\n</Signal>\n");
            break;
        default:
            break;
    }

	strcpy(m_SignalDescription, stringinput); //save for later

	if(Fnc < 200)
	{
	    IFNSIM((Status = cs_IssueAtmlSignal("Setup", m_DeviceName, stringinput, NULL, 0)));
		CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", stringinput, Status));
		m_SignalActive = true;
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateDmm(int Fnc)
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
int CDmm_T::InitiateDmm(int Fnc)
{
    int   Status = 0;
    short  nVerb = 0;

    // Initiate action for the Dmm
    CEMDEBUG(5,cs_FmtMsg("InitiateDmm (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    //Grab the verb, because "SETUP" calls INX and it should not so if verb
    //is "SETUP" just ignore the enable else call enable
    nVerb = GetCurVerb();

    if (nVerb != V_SET)
   	    IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SignalDescription, NULL, 0)));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchDmm(int Fnc)
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
int CDmm_T::FetchDmm(int Fnc)
{
    DATUM    *fdat;
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5000;
    char    XmlValue[1024];
    char    MeasFunc[32];
    
    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
        MaxTime = m_MaxTime.Real * 1000;

    // Fetch action for the Dmm
    CEMDEBUG(5,cs_FmtMsg("FetchDmm (%s) called FNC %d", 
                          m_DeviceName, Fnc));

 	IFNSIM((Status = cs_IssueAtmlSignal("Fetch", m_DeviceName, m_SignalDescription, XmlValue, 1024)));

	switch(Fnc)
	{
	case FNC_VOLTS_AC:
	case FNC_VOLTS_AC_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_AC);
		break;
	case FNC_VOLTS_AC_P:
	case FNC_VOLTS_AC_P_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_AC_P);
		break;
	case FNC_VOLTS_AC_PP:
	case FNC_VOLTS_AC_PP_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_AC_PP);
		break;
	case FNC_VOLTS_AC_R:
	case FNC_VOLTS_AC_R_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_AC_R);
		break;
	case FNC_PERIOD_AC:
	case FNC_PERIOD_AC_TRG:
		strcpy(MeasFunc,ATTRIB_PERIOD_AC);
		break;
	case FNC_FREQ_AC:
	case FNC_FREQ_AC_TRG:
		strcpy(MeasFunc,ATTRIB_FREQ_AC);
		break;
	case FNC_CURR_AC:
	case FNC_CURR_AC_TRG:
		strcpy(MeasFunc,ATTRIB_CURR_AC);
		break;
	case FNC_CURR_AC_AV:
	case FNC_CURR_AC_AV_TRG:
		strcpy(MeasFunc,ATTRIB_CURR_AC_AV);
		break;
	case FNC_DC_OFF_AC:
	case FNC_DC_OFF_AC_TRG:
		strcpy(MeasFunc,ATTRIB_DC_OFF_AC);
		break;
	case FNC_VOLTS_AC_AV:
	case FNC_VOLTS_AC_AV_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_AC_AV);
		break;
	case FNC_CURR_DC:
	case FNC_CURR_DC_TRG:
		strcpy(MeasFunc,ATTRIB_CURR_DC);
		break;
	case FNC_VOLTS_DC:
	case FNC_VOLTS_DC_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_DC);
		break;
	case FNC_VOLTS_DC_R:
	case FNC_VOLTS_DC_R_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_DC_R);
		break;
	case FNC_VOLTS_DC_AV:
	case FNC_VOLTS_DC_AV_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_DC_AV);
		break;
	case FNC_CURR_DC_AV:
	case FNC_CURR_DC_AV_TRG:
		strcpy(MeasFunc,ATTRIB_CURR_DC_AV);
		break;
	case FNC_AC_COMP_DC:
	case FNC_AC_COMP_DC_TRG:
		strcpy(MeasFunc,ATTRIB_AC_COMP_DC);
		break;
	case FNC_AC_COMP_DC_F:
	case FNC_AC_COMP_DC_F_TRG:
		strcpy(MeasFunc,ATTRIB_AC_COMP_DC_F);
		break;
	case FNC_IMP:
	case FNC_IMP_TRG:
		if (m_FourWire.Exists)
			strcpy(MeasFunc,ATTRIB_IMP_4WIRE);
		else
			strcpy(MeasFunc,ATTRIB_IMP);
		break;
	case FNC_VOLTS_SQ:
	case FNC_VOLTS_SQ_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_SQ);
		break;
	case FNC_VOLTS_SQ_P:
	case FNC_VOLTS_SQ_P_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_SQ_P);
		break;
	case FNC_VOLTS_SQ_PP:
	case FNC_VOLTS_SQ_PP_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_SQ_PP);
		break;
	case FNC_FREQ_SQ:
	case FNC_FREQ_SQ_TRG:
		strcpy(MeasFunc,ATTRIB_FREQ_SQ);
		break;
	case FNC_PERIOD_SQ:
	case FNC_PERIOD_SQ_TRG:
		strcpy(MeasFunc,ATTRIB_PERIOD_SQ);
		break;
	case FNC_VOLTS_TR:
	case FNC_VOLTS_TR_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_TR);
		break;
	case FNC_VOLTS_TR_P:
	case FNC_VOLTS_TR_P_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_TR_P);
		break;
	case FNC_VOLTS_TR_PP:
	case FNC_VOLTS_TR_PP_TRG:
		strcpy(MeasFunc,ATTRIB_VOLTS_TR_PP);
		break;
	case FNC_FREQ_TR:
	case FNC_FREQ_TR_TRG:
		strcpy(MeasFunc,ATTRIB_FREQ_TR);
		break;
	case FNC_PERIOD_TR:
	case FNC_PERIOD_TR_TRG:
		strcpy(MeasFunc,ATTRIB_PERIOD_TR);
		break;
	}
	
    if(Status)
    {
        MeasValue = FLT_MAX;
     }
    else
    {
        IFNSIM(cs_GetSingleDblValue(XmlValue, MeasFunc, &MeasValue));
    }

    fdat = FthDat();
    DECDatVal(fdat, 0) = MeasValue;
    FthCnt(1);

    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: OpenDmm(int Fnc)
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
int CDmm_T::OpenDmm(int Fnc)
{
    // Open action for the Dmm
    CEMDEBUG(5,cs_FmtMsg("OpenDmm (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseDmm(int Fnc)
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
int CDmm_T::CloseDmm(int Fnc)
{
    // Close action for the Dmm
    CEMDEBUG(5,cs_FmtMsg("CloseDmm (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetDmm(int Fnc)
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
int CDmm_T::ResetDmm(int Fnc)
{
    int   Status = 0;

    // Reset action for the Dmm
    CEMDEBUG(5,cs_FmtMsg("ResetDmm (%s) called FNC %d", 
                          m_DeviceName, Fnc));
    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
    if(Fnc != 0)
    {
        // Reset the Dmm
         IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, m_SignalDescription, NULL, 0)));
	}

	if (Fnc < 200)
		m_SignalActive = false;

    InitPrivateDmm(Fnc);

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoDmm(int Fnc)
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
int CDmm_T::GetStmtInfoDmm(int Fnc)
{
    DATUM    *datum;
	double	tmpDbl1 = 0.0;
	double	tmpDbl2 = 0.0;
	bool	mcharmax = false;
	bool	mcharmin = false;
	
	// see if it is triggered
	if(Fnc > 200)
	{
		TriggerFlag = true;

		//DELAY
		if ((datum = RetrieveDatum(M_DELA, K_SET)) != NULL)
		{
			m_Delay.Exists = true;
			m_Delay.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found DELAY %lf",m_Delay.Real));
			FreeDatum(datum);
		}
		//EVENT-DELAY
		else if ((datum = RetrieveDatum(M_EVDL, K_SET)) != NULL)
		{
			m_Delay.Exists = true;
			m_Delay.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found EVENT-DELAY %lf",m_EventDelay.Real));
			FreeDatum(datum);
		}
		// sample count
		if (datum = RetrieveDatum(M_SCNT, K_SET))
		{
			m_EventSampleCount.Exists = true;
			m_EventSampleCount.Int = INTDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Event SAMPLE-COUNT %d",m_EventSampleCount.Int));
			FreeDatum(datum);
		}
	}

	//////////////////////////////////////////////////
	// Retrieve Voltage as a measured characteristic
	//////////////////////////////////////////////////
	if ((Fnc == FNC_VOLTS_AC)     ||
		(Fnc == FNC_VOLTS_AC_TRG) ||
		(Fnc == FNC_VOLTS_DC)     ||
		(Fnc == FNC_VOLTS_DC_TRG) ||
		(Fnc == FNC_VOLTS_SQ)     ||
		(Fnc == FNC_VOLTS_SQ_TRG) ||
		(Fnc == FNC_VOLTS_TR)     ||
		(Fnc == FNC_VOLTS_TR_TRG))
	{
		tmpDbl1 = -(FLT_MAX);
		tmpDbl2 = -(FLT_MAX);
		if ((datum = RetrieveDatum(M_VOLT, K_SRX)) != NULL)
		{
			tmpDbl1 = DECDatVal(datum, 0);
			CEMDEBUG(5, cs_FmtMsg("Found VOLTAGE MAX %.3lf V", tmpDbl1));
			mcharmax = true;
			FreeDatum(datum);
		}
		if ((datum = RetrieveDatum(M_VOLT, K_SRN)) != NULL)
		{
			tmpDbl2 = DECDatVal(datum, 0);
			CEMDEBUG(5, cs_FmtMsg("Found VOLTAGE MIN %.3lf V", tmpDbl2));
			mcharmin = true;
			FreeDatum(datum);
		}
		if ((mcharmax == true) && (mcharmin == true))
		{
			if (fabs(tmpDbl1) > fabs(tmpDbl2))
				m_Voltage.Real = fabs(tmpDbl1);
			else
				m_Voltage.Real = fabs(tmpDbl2);
			m_Volt.Exists = true;
		}
		else if ((mcharmax == true) && (mcharmin == false))
		{
			m_Voltage.Real = tmpDbl1;
			m_Voltage.Exists = true;
		}
		else if ((mcharmax == false) && (mcharmin == true))
		{
			m_Voltage.Real = tmpDbl2;
			m_Voltage.Exists = true;
		}
		else
		{
			m_Voltage.Exists = false;
		}

		m_Volt.Real = m_Voltage.Real;
		m_Volt.Exists = m_Voltage.Exists;
	}

	//////////////////////////////////////////////////
	// Retrieve VOLTAGE as statement characteristic
	//////////////////////////////////////////////////
    if ((datum = RetrieveDatum(M_VOLT, K_SET)) != NULL)
    {
		if (m_Voltage.Exists == true)
		{
			CEMERROR(EB_SEVERITY_WARNING, "Amplitude already defined. Multiple definition of VOLTAGE ignored");
		}
		else if (DatTyp(datum) == DECV)
		{
			m_Volt.Exists = true;
			m_Volt.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE %lf",m_Volt.Real));
			FreeDatum(datum);
		}
    }

	//////////////////////////////////////////////////
    // Retrieve Measured Characteristic  VOLTAGE-P
	//////////////////////////////////////////////////
	if ((Fnc == FNC_VOLTS_AC_P)     ||
		(Fnc == FNC_VOLTS_AC_P_TRG) ||
		(Fnc == FNC_VOLTS_SQ_P)     ||
		(Fnc == FNC_VOLTS_SQ_P_TRG) ||
		(Fnc == FNC_VOLTS_TR_P)     ||
		(Fnc == FNC_VOLTS_TR_P_TRG))
	{
	    if ((datum = RetrieveDatum(M_VLPK, K_SRX)) != NULL)
		{
			m_VoltageP.Exists = true;
			m_VoltageP.Real = DECDatVal(datum, 0);

			// scale voltage if required
			if ((Fnc == FNC_VOLTS_AC_P) ||
				(Fnc == FNC_VOLTS_AC_P_TRG))
			{
				m_Volt.Real = m_VoltageP.Real / SQRT_2;
				m_Volt.Exists = true;
			}
			else if ((Fnc == FNC_VOLTS_SQ_P) ||
					 (Fnc == FNC_VOLTS_SQ_P_TRG))
			{
				m_Volt.Real = m_VoltageP.Real;
				m_Volt.Exists = true;
			}
			else if ((Fnc == FNC_VOLTS_TR_P) ||
					 (Fnc == FNC_VOLTS_TR_P_TRG))
			{
				m_Volt.Real = m_VoltageP.Real / SQRT_3;
				m_Volt.Exists = true;
			}
			CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE-P MAX %.2lf V",m_VoltageP.Real));
			FreeDatum(datum);
		}
	}

	//////////////////////////////////////////////////
	// VOLTAGE-P
	//////////////////////////////////////////////////
    if ((datum = RetrieveDatum(M_VLPK, K_SET)) != NULL)
    {
		if (m_Volt.Exists == true)
		{
			CEMERROR(EB_SEVERITY_WARNING, "Amplitude already defined - VOLTAGE-P redundant");
		}
		else
		{
			if (GetCurDevNoun() == N_ACS)
			{
				m_Volt.Real = DECDatVal(datum, 0) / SQRT_2;
				m_Volt.Exists = true;
			}
			else if (GetCurDevNoun() == N_SQW)
			{
				m_Volt.Real = DECDatVal(datum, 0);
				m_Volt.Exists = true;
			}
			else if (GetCurDevNoun() == N_TRI)
			{
				m_Volt.Real = DECDatVal(datum, 0) / SQRT_3;
				m_Volt.Exists = true;
			}
			CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE-P %.2lf V scaled to %.3lv V",DECDatVal(datum, 0), m_Volt.Real));
			FreeDatum(datum);

		}
    }
	
	//////////////////////////////////////////////////
	// Retrieve measured characteristic VOLTAGE-PP
	//////////////////////////////////////////////////
	if ((Fnc == FNC_VOLTS_AC_PP)     ||
		(Fnc == FNC_VOLTS_AC_PP_TRG) ||
		(Fnc == FNC_VOLTS_SQ_PP)     ||
		(Fnc == FNC_VOLTS_SQ_PP_TRG) ||
		(Fnc == FNC_VOLTS_TR_PP)     ||
		(Fnc == FNC_VOLTS_TR_PP_TRG))
	{
	    if ((datum = RetrieveDatum(M_VLPP, K_SRX)) != NULL)
		{
			m_VoltagePP.Exists = true;
			m_VoltagePP.Real = DECDatVal(datum, 0);
			if (GetCurDevNoun() == N_ACS)
			{
				m_Volt.Real = DECDatVal(datum, 0) / SQRT_2;
				m_Volt.Exists = true;
			}
			else if (GetCurDevNoun() == N_SQW)
			{
				m_Volt.Real = DECDatVal(datum, 0);
				m_Volt.Exists = true;
			}
			else if (GetCurDevNoun() == N_TRI)
			{
				m_Volt.Real = DECDatVal(datum, 0) / SQRT_3;
				m_Volt.Exists = true;
			}
			CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE-P MAX %.2lf V",m_VoltageP.Real));
			FreeDatum(datum);
		}
	}


	//////////////////////////////////////////////////
	// Retrieve statement characteristic VOLTAGE-PP
	//////////////////////////////////////////////////
    if ((datum = RetrieveDatum(M_VLPP, K_SET)) != NULL)
    {
		if (m_Volt.Exists == true)
		{
			CEMERROR(EB_SEVERITY_WARNING, "Amplitude already defined. VOLTAGE-PP redundant");
		}
		else if (DatTyp(datum) == DECV)
		{
			if (GetCurDevNoun() == N_ACS)
			{
				m_Volt.Real = (DECDatVal(datum, 0) * 0.5) / SQRT_2;
				m_Volt.Exists = true;
			}
			else if (GetCurDevNoun() == N_SQW)
			{
				m_Volt.Real = DECDatVal(datum, 0) * 0.5;
				m_Volt.Exists = true;
			}
			else if (GetCurDevNoun() == N_TRI)
			{
				m_Volt.Real = (DECDatVal(datum, 0) * 0.5) / SQRT_3;
				m_Volt.Exists = true;
			}
			CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE-P %.3lf V scaled to %.3lf V", DECDatVal(datum, 0), m_Volt.Real));
			FreeDatum(datum);
		}
    }

	//////////////////////////////////////////////////
	// REF-VOLT
	//////////////////////////////////////////////////
    if ((datum = RetrieveDatum(M_REFV, K_SET)) != NULL)
    {
		m_RefVolt.Exists = true;
        m_RefVolt.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found REF-VOLT %lf",m_RefVolt.Real));
		FreeDatum(datum);
    }

	//////////////////////////////////////////////////
	// SAMPLE-WIDTH
	//////////////////////////////////////////////////
    if (datum = RetrieveDatum(M_SMPW, K_SET))
    {
		m_SampleWidth.Exists = true;
		m_SampleWidth.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found SAMPLE-WIDTH %lf",m_SampleWidth.Real));
		FreeDatum(datum);
    }

	//////////////////////////////////////////////////
	// BANDWIDTH
	//////////////////////////////////////////////////
    if (datum = RetrieveDatum(M_BAND, K_SRN))
    {
		m_Bandwidth.Exists = true;
		m_Bandwidth.Int = (int)DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found BANDWIDTH %d",m_Bandwidth.Int));
		FreeDatum(datum);
    }

	//////////////////////////////////////////////////
	//DC-OFFSET
	//////////////////////////////////////////////////
	if ((datum = RetrieveDatum(M_DCOF, K_SET)) ||
        (datum = RetrieveDatum(M_DCOF, K_SRX)) ||
        (datum = RetrieveDatum(M_DCOF, K_SRN)))
    {
		if (DatTyp(datum) == DECV)
		{
			m_DCOffset.Exists = true;
		    m_DCOffset.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found DC-OFFSET %lf",m_DCOffset.Real));
		}
		FreeDatum(datum);
    }

	//////////////////////////////////////////////////
	// VOLTAGE-RATIO
	//////////////////////////////////////////////////
	if ((datum = RetrieveDatum(M_VOLR, K_SET)) ||
        (datum = RetrieveDatum(M_VOLR, K_SRX)) ||
        (datum = RetrieveDatum(M_VOLR, K_SRN)))
    {
		if (DatTyp(datum) == DECV)
		{
			m_VoltRatio.Exists = true;
		    m_VoltRatio.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE-RATIO %lf",m_VoltRatio.Real));
		}
		FreeDatum(datum);
    }

	//////////////////////////////////////////////////
	// RES
	//////////////////////////////////////////////////
	if ((datum = RetrieveDatum(M_RESI, K_SRX)) ||
	    (datum = RetrieveDatum(M_RESI, K_SRN)))
    {
		m_Res.Exists = true;
		m_Res.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found RES MAX %lf",m_Res.Real));
		FreeDatum(datum);
    }

	//////////////////////////////////////////////////
	// REF-RES
	//////////////////////////////////////////////////
	if (datum = RetrieveDatum(M_REFR, K_SET))
    {
		m_RefRes.Exists = true;
		m_RefRes.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found REF-RES %lf",m_RefRes.Real));
		FreeDatum(datum);
    }

	//////////////////////////////////////////////////
	// SAMPLE-COUNT
	//////////////////////////////////////////////////
	if (datum = RetrieveDatum(M_SCNT, K_SET))
    {
		m_SampleCount.Exists = true;
		m_SampleCount.Int = (int)DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found SAMPLE-COUNT %d",m_SampleCount.Int));
		FreeDatum(datum);
	}

	//////////////////////////////////////////////////
	// FREQ
	//////////////////////////////////////////////////
	if((datum = GetDatum(M_FREQ, K_SET)))
    {
		m_Freq.Exists = true;
		m_Freq.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found FREQ %lf",m_Freq.Real));
    }

	//////////////////////////////////////////////////
	// CURRENT
	//////////////////////////////////////////////////
	if(
       (datum = GetDatum(M_CURR, K_SET)) ||
       (datum = GetDatum(M_CURR, K_SRX)) ||
       (datum = GetDatum(M_CURR, K_SRN)) )
    {
		m_Current.Exists = true;
        m_Current.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found CURRENT %lf",m_Current.Real));
    }

	//////////////////////////////////////////////////
	// PERIOD
	//////////////////////////////////////////////////
	if(datum = GetDatum(M_PERI, K_SRX))
    {
		m_Period.Exists = true;
        m_Period.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found PERIOD %lf",m_Period.Real));
    }

	//////////////////////////////////////////////////
	// AV-VOLT
	//////////////////////////////////////////////////
	if(
       (datum = GetDatum(M_VLAV, K_SET)) ||
       (datum = GetDatum(M_VLAV, K_SRX)) ||
       (datum = GetDatum(M_VLAV, K_SRN)) )
    {
		m_Volt.Exists = true;
        m_Volt.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found VOLT %lf",m_Volt.Real));
    }

	//////////////////////////////////////////////////
	// AV-CURRENT
	//////////////////////////////////////////////////
	if(
       (datum = GetDatum(M_CURA, K_SET)) ||
       (datum = GetDatum(M_CURA, K_SRX)) ||
       (datum = GetDatum(M_CURA, K_SRN)) )
    {
		m_AvCurrent.Exists = true;
        m_AvCurrent.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found AV-CURRENT %lf",m_AvCurrent.Real));
    }

	//////////////////////////////////////////////////
	// AC-COMP
	//////////////////////////////////////////////////
	if(
       (datum = GetDatum(M_ACCP, K_SET)) ||
       (datum = GetDatum(M_ACCP, K_SRX)) ||
       (datum = GetDatum(M_ACCP, K_SRN)) )
    {
		m_ACComp.Exists = true;
        m_ACComp.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found AC-COMP %lf",m_ACComp.Real));
    }

	//////////////////////////////////////////////////
	// AC-COMP-FREQ
	//////////////////////////////////////////////////
	if((datum = GetDatum(M_ACCF, K_SET)))
    {
		m_ACCompFreq.Exists = true;
		m_ACCompFreq.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found AC-COMP-FREQ %lf",m_ACCompFreq.Real));
    }

	//////////////////////////////////////////////////
	// FOUR-WIRE
	//////////////////////////////////////////////////
	if(RemoveMod(M_FORW))
	{
		m_FourWire.Exists = true;
	}

	// if(Fnc>= 100)
	if( (Fnc>=100) && (TriggerFlag == true)) //triggered
	{
		;
	}

	//////////////////////////////////////////////////
	// Retrieve STROBE-TO-EVENT
	// added  20090408 ewl
	//////////////////////////////////////////////////
	if (RemoveMod(M_SBEV))
	{
		m_StrobeToEvent.Exists = true;
		CEMDEBUG(10, cs_FmtMsg("Found STROBE-TO-EVENT"));
	}

	//////////////////////////////////////////////////
	// MAX-TIME M_MAXT
	//////////////////////////////////////////////////
	if(datum = GetDatum(M_MAXT, K_SET))
    {
		m_MaxTime.Exists = true;
        m_MaxTime.Real = DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found MAX-TIME %lf",m_MaxTime.Real));
	}

	//////////////////////////////////////////////////
	// Set Auto ranging
	//////////////////////////////////////////////////
    switch (Fnc)
	{
	case FNC_VOLTS_AC :
	case FNC_VOLTS_AC_TRG :
	case FNC_VOLTS_DC :
	case FNC_VOLTS_DC_TRG :
	case FNC_VOLTS_SQ :
	case FNC_VOLTS_SQ_TRG :
	case FNC_VOLTS_TR :
	case FNC_VOLTS_TR_TRG :
		if (m_Voltage.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_VOLTS_AC_P :
	case FNC_VOLTS_AC_P_TRG :
	case FNC_VOLTS_SQ_P :
	case FNC_VOLTS_SQ_P_TRG :
	case FNC_VOLTS_TR_P :
	case FNC_VOLTS_TR_P_TRG :
		if (m_VoltageP.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_VOLTS_AC_PP :
	case FNC_VOLTS_AC_PP_TRG :
	case FNC_VOLTS_SQ_PP :
	case FNC_VOLTS_SQ_PP_TRG :
	case FNC_VOLTS_TR_PP :
	case FNC_VOLTS_TR_PP_TRG :
		if (m_VoltagePP.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_IMP :
	case FNC_IMP_TRG :
		if (m_Res.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_FREQ_AC :
	case FNC_FREQ_AC_TRG :
	case FNC_FREQ_SQ :
	case FNC_FREQ_SQ_TRG :
	case FNC_FREQ_TR :
	case FNC_FREQ_TR_TRG :
		if (m_Freq.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_CURR_AC :
	case FNC_CURR_AC_TRG :
	case FNC_CURR_DC :
	case FNC_CURR_DC_TRG :
		if (m_Current.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_PERIOD_AC :
	case FNC_PERIOD_AC_TRG :
	case FNC_PERIOD_SQ :
	case FNC_PERIOD_SQ_TRG :
	case FNC_PERIOD_TR :
	case FNC_PERIOD_TR_TRG :
		if (m_Period.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_VOLTS_DC_R :
	case FNC_VOLTS_DC_R_TRG :
		if (m_VoltRatio.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_VOLTS_DC_AV :
	case FNC_VOLTS_DC_AV_TRG :
	case FNC_VOLTS_AC_AV :
	case FNC_VOLTS_AC_AV_TRG :
		if (m_AvVolt.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_CURR_DC_AV :
	case FNC_CURR_DC_AV_TRG :
	case FNC_CURR_AC_AV :
	case FNC_CURR_AC_AV_TRG :
		if (m_AvCurrent.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_AC_COMP_DC :
	case FNC_AC_COMP_DC_TRG :
		if (m_ACComp.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_AC_COMP_DC_F :
	case FNC_AC_COMP_DC_F_TRG :
		if (m_ACCompFreq.Exists == false)
			m_AutoRange = true;
		break;
	case FNC_DC_OFF_AC :
	case FNC_DC_OFF_AC_TRG :
		if (m_DCOffset.Exists == false)
			m_AutoRange = true;
		break;
	}
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateDmm()
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
void CDmm_T::InitPrivateDmm(int Fnc)
{
	if (Fnc < 200)
	{
		ClearModStruct(m_ACComp);
		ClearModStruct(m_ACCompFreq);
		ClearModStruct(m_AvVolt);
		ClearModStruct(m_AvCurrent);
		ClearModStruct(m_Bandwidth);
		ClearModStruct(m_Current);
		ClearModStruct(m_DCOffset);
		ClearModStruct(m_Delay);
		ClearModStruct(m_FourWire);
		ClearModStruct(m_Freq);
		ClearModStruct(m_MaxTime);
		ClearModStruct(m_Period);
		ClearModStruct(m_RefRes);
		ClearModStruct(m_RefVolt);
		ClearModStruct(m_Res);
		ClearModStruct(m_SampleCount);
		ClearModStruct(m_SampleWidth);
		ClearModStruct(m_StrobeToEvent);
		ClearModStruct(m_Volt);
		ClearModStruct(m_Voltage);
		ClearModStruct(m_VoltageP);
		ClearModStruct(m_VoltagePP);
		ClearModStruct(m_VoltRatio);
		m_AutoRange = false;
	}
	else if ((Fnc >= 200) || (Fnc == 0))	// reset var's for event
	{
		TriggerFlag = false;
		ClearModStruct(m_EventDelay);
		ClearModStruct(m_EventSampleCount);
	}
	
  return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataDmm()
//
// Purpose: Resets cal data
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
void CDmm_T::NullCalDataDmm(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
    return;
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


