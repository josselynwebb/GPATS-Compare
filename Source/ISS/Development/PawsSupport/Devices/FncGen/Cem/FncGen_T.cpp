//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    FncGen_T.cpp
//
// Date:	    30-JAN-06
//
// Purpose:	    Instrument Driver for FncGen
//
// Instrument:	FncGen  <Device Description> (<device Type>)
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
// ---  -------------------------------------------------------------------------
//////// Place FncGen specific data here //////////////
//   1  source ac signal
//   2  source square wave
//   3  source triangle wave
//   4  source ramp wave
//   5  source pulsed dc  
//   6  source sinc wave  
//   7  source exp pulse 
//   8  source dc signal  
//   9  source waveform   
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  30JAN06  Baseline                         
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "FncGen_T.h"

// Local Defines

// Function codes
#define FNC_AC_SIGNAL		 1
#define FNC_SQUARE_WAVE		 2
#define FNC_TRIANGLE_WAVE    3
#define FNC_RAMP_SIGNAL		 4
#define FNC_PULSED_DC		 5
#define FNC_SINC_WAVE		 6
#define FNC_EXP_PULSE		 7
#define FNC_DC_SIGNAL		 8
#define FNC_WAVEFORM		 9

#define FNC_AC_SIGNAL_S		 21
#define FNC_SQUARE_WAVE_S	 22
#define FNC_TRIANGLE_WAVE_S  23
#define FNC_RAMP_SIGNAL_S	 24
#define FNC_PULSED_DC_S		 25
#define FNC_SINC_WAVE_S		 26
#define FNC_EXP_PULSE_S		 27
#define FNC_DC_SIGNAL_S		 28
#define FNC_WAVEFORM_S		 29

#define FNC_TRG				 102
#define FNC_GATE_FROM		 103
#define FNC_GATE_TO			 104

#define CAL_TIME       (86400 * 365) /* one year */
#define SQRT_2			1.414213562373
#define SQRT_3			1.73205080808

#define POS_SLOPE			1
#define NEG_SLOPE			2

// Static Variables

// Local Function Prototypes
int TXTtoINT(char * TXT);
void array_to_string(double array [], int size, char output []);
void rm_white(char * fix);


//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CFncGen_T(int Bus, int PrimaryAdr, int SecondaryAdr,
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
CFncGen_T::CFncGen_T(char *DeviceName, 
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

    InitPrivateFncGen();
	NullCalDataFncGen();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("FncGen Class called with Device [%s], "
                         "Sim %d, Dbg %d", 
                          DeviceName, Sim, Dbg));

    // Initialize the FncGen - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CFncGen_T()
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
CFncGen_T::~CFncGen_T()
{
    // Reset the FncGen
    CEMDEBUG(5,cs_FmtMsg("FncGen Class Distructor called for Device [%s], ",
                          m_DeviceName));


    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusFncGen(int Fnc)
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
int CFncGen_T::StatusFncGen(int Fnc)
{
    int Status = 0;

    // Status action for the FncGen
    CEMDEBUG(5,cs_FmtMsg("StatusFncGen (%s) called FNC %d", 
                          m_DeviceName, Fnc));
    IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, "<Signal name=\"Fgen_SIGNAL\" Out=\"Waveform\"/>\n", 
                                       NULL, 0)));
    //CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %lf",
    //                             SignalDescription));

	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupFncGen_T(int Fnc)
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
int CFncGen_T::SetupFncGen(int Fnc)
{
    int       Status = 0;
	char stringinput[2048];
	char tempstring[2048];
	char string[2048];
	char *WaveformString;
	char *data;

    // Setup action for the Dmm
    CEMDEBUG(5,cs_FmtMsg("SetupDmm (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
        return(0);
    
	if((Status = GetStmtInfoFncGen(Fnc)) != 0)
        return(0);

	//Pulsed DC signal with duty cycle specified is treated like a square wave
	if((Fnc==FNC_PULSED_DC ||Fnc==FNC_PULSED_DC_S) && (!m_RiseTime.Exists && !m_FallTime.Exists && !m_PulseWidth.Exists))
	{
		if(Fnc==FNC_PULSED_DC)
			Fnc=FNC_SQUARE_WAVE;
		else
			Fnc=FNC_SQUARE_WAVE_S;
	}
		
    sprintf(stringinput, "<Signal Name=\"Fgen_SIGNAL\" Out=\"Waveform\">\n");
    switch(Fnc)
    {
	case FNC_AC_SIGNAL:
	case FNC_AC_SIGNAL_S:
		sprintf(string, "<Sinusoid name=\"AC_COMP\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" ", m_Volt.Real);
		    strcat(string,tempstring);
        }
		if(m_Freq.Exists)
        {
			sprintf(tempstring, "frequency=\"%gHz\" ", m_Freq.Real);
		    strcat(string,tempstring);
        }
		if(m_Burst.Exists)
		{
			sprintf(tempstring, "Gate=\"Burst\" ");
            strcat(string,tempstring);
        }
		strcat(string,"/>\n");

		if(m_Burst.Exists)
		{
			sprintf(tempstring, "<PulsedEvent name=\"Burst\" pulses=\"%d\" ", m_Burst.Int);
            strcat(string,tempstring);
			if(m_BurstRepRate.Exists)
			{
				sprintf(tempstring, "repetition=\"%gHz\" ", m_BurstRepRate.Real);
				strcat(string,tempstring);
			}
			strcat(string,"/>\n");
		}

		if(m_DCOffset.Exists)
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%gV\" />\n", m_DCOffset.Real);
		    strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
            strcat(string,tempstring);
		}

		sprintf(tempstring, "<Sum name=\"Combination\" In=\"AC_COMP DC_COMP\" />\n");
		strcat(string,tempstring);

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Signal\" resistance=\"%dOhm\" In=\"Combination\" />\n", m_TestEquipImp.Int);
		strcat(string,tempstring);

		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
			}
		}
		
		sprintf(tempstring, "<HighPass name=\"Waveform\" ");
		strcat(string,tempstring);
		if(m_Bandwidth.Exists)
		{
			sprintf(tempstring, "cutoff=\"%dHz\" ", m_Bandwidth.Int);
			strcat(string,tempstring);
		}
		else
        {
			sprintf(tempstring, "cutoff=\"25000000Hz\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "Sync=\"Trigger\" ");
				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "Gate=\"Gated\" ");
				strcat(string,tempstring);
			}
		}
		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_AC_SIGNAL_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}

		sprintf(tempstring, "In=\"Signal\" />\n");
		strcat(string,tempstring);
		
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		
		strcat(stringinput, string);
		break;
	case FNC_SQUARE_WAVE:
	case FNC_SQUARE_WAVE_S:
		sprintf(string, "<SquareWave name=\"AC_COMP\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" ", m_Volt.Real);
		    strcat(string,tempstring);
        }

		if(m_Freq.Exists)
        {
			sprintf(tempstring, "period=\"%gs\" ", 1/m_Freq.Real);
		    strcat(string,tempstring);
        }

		if(m_DutyCycle.Exists)
        {
			sprintf(tempstring, "dutyCycle=\"%d", m_DutyCycle.Int);
			strcat(tempstring, "%\" ");
            strcat(string,tempstring);
        }
		if(m_Burst.Exists)
		{
			sprintf(tempstring, "Gate=\"Burst\" ");
            strcat(string,tempstring);
        }
		strcat(string,"/>\n");

		if(m_Burst.Exists)
		{
			sprintf(tempstring, "<PulsedEvent name=\"Burst\" pulses=\"%d\" ", m_Burst.Int);
            strcat(string,tempstring);
			if(m_BurstRepRate.Exists)
			{
				sprintf(tempstring, "repetition=\"%gHz\" ", m_BurstRepRate.Real);
				strcat(string,tempstring);
			}
			strcat(string,"/>\n");
		}
		if(m_DCOffset.Exists)
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%gV\" />\n", m_DCOffset.Real);
		    strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
            strcat(string,tempstring);
		}

		sprintf(tempstring, "<Sum name=\"Combination\" In=\"AC_COMP DC_COMP\" />\n");
		strcat(string,tempstring);

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Signal\" resistance=\"%dOhm\" In=\"Combination\" />\n", m_TestEquipImp.Int);
		strcat(string,tempstring);

		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
			}
		}
		
		sprintf(tempstring, "<HighPass name=\"Waveform\" ");
		strcat(string,tempstring);
		if(m_Bandwidth.Exists)
		{
			sprintf(tempstring, "cutoff=\"%dHz\" ", m_Bandwidth.Int);
			strcat(string,tempstring);
		}
		else
        {
			sprintf(tempstring, "cutoff=\"25000000Hz\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "Sync=\"Trigger\" ");
				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "Gate=\"Gated\" ");
				strcat(string,tempstring);
			}
		}
		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_SQUARE_WAVE_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}

		sprintf(tempstring, "In=\"Signal\" />\n");
		strcat(string,tempstring);
		
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		
		strcat(stringinput, string);
		break;
	case FNC_TRIANGLE_WAVE:
	case FNC_TRIANGLE_WAVE_S:
		sprintf(string, "<Triangle name=\"AC_COMP\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" ", m_Volt.Real);
            strcat(string,tempstring);
        }
		if(m_Freq.Exists)
        {
			sprintf(tempstring, "period=\"%gs\" ", 1/m_Freq.Real);
		    strcat(string,tempstring);
        }
		if(m_Burst.Exists)
		{
			sprintf(tempstring, "Gate=\"Burst\" ");
            strcat(string,tempstring);
        }
		strcat(string,"/>\n");

		if(m_Burst.Exists)
		{
			sprintf(tempstring, "<PulsedEvent name=\"Burst\" pulses=\"%d\" ", m_Burst.Int);
            strcat(string,tempstring);
			if(m_BurstRepRate.Exists)
			{
				sprintf(tempstring, "repetition=\"%gHz\" ", m_BurstRepRate.Real);
				strcat(string,tempstring);
			}
			strcat(string,"/>\n");
		}

		if(m_DCOffset.Exists)
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%gV\" />\n", m_DCOffset.Real);
            strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
            strcat(string,tempstring);
		}

		sprintf(tempstring, "<Sum name=\"Combination\" In=\"AC_COMP DC_COMP\" />\n");
		strcat(string,tempstring);

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Signal\" resistance=\"%dOhm\" In=\"Combination\" />\n", m_TestEquipImp.Int);
		strcat(string,tempstring);
		
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
			}
		}
		

		sprintf(tempstring, "<HighPass name=\"Waveform\" ");
		strcat(string,tempstring);
		if(m_Bandwidth.Exists)
		{
			sprintf(tempstring, "cutoff=\"%dHz\" ", m_Bandwidth.Int);
			strcat(string,tempstring);
		}
		else
        {
			sprintf(tempstring, "cutoff=\"25000000Hz\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "Sync=\"Trigger\" ");
				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "Gate=\"Gated\" ");
				strcat(string,tempstring);
			}
		}
		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_TRIANGLE_WAVE_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}

		sprintf(tempstring, "In=\"Signal\" />\n");
		strcat(string,tempstring);
		
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		
		strcat(stringinput, string);
		break;
	case FNC_RAMP_SIGNAL:
	case FNC_RAMP_SIGNAL_S:
		sprintf(string, "<Ramp name=\"AC_COMP\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" ", m_Volt.Real);
            strcat(string,tempstring);
        }

		if(m_Freq.Exists)
        {
			sprintf(tempstring, "period=\"%gs\" ", 1/m_Freq.Real);
            strcat(string,tempstring);
        }
		
		if(m_RiseTime.Exists)
		{
			sprintf(tempstring, "riseTime=\"%gs\" ", m_RiseTime.Real);
            strcat(string,tempstring);
        }
		if(m_FallTime.Exists)
		{
			if(m_Freq.Exists && !m_RiseTime.Exists) //rise time needs to be set
			{
				sprintf(tempstring, "riseTime=\"%gs\" ", (1/m_Freq.Real+m_FallTime.Real));
				strcat(string,tempstring);
			}
			if(m_RiseTime.Exists && !m_Freq.Exists) //period needs to be set
			{
				sprintf(tempstring, "period=\"%gs\" ", (m_RiseTime.Real+m_FallTime.Real));
				strcat(string,tempstring);
			}
		}
		if(m_Burst.Exists)
		{
			sprintf(tempstring, "Gate=\"Burst\" ");
            strcat(string,tempstring);
        }
		strcat(string,"/>\n");

		if(m_Burst.Exists)
		{
			sprintf(tempstring, "<PulsedEvent name=\"Burst\" pulses=\"%d\" ", m_Burst.Int);
            strcat(string,tempstring);
			if(m_BurstRepRate.Exists)
			{
				sprintf(tempstring, "repetition=\"%gHz\" ", m_BurstRepRate.Real);
				strcat(string,tempstring);
			}
			strcat(string,"/>\n");
		}
		if(m_DCOffset.Exists)
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%gV\" />\n", m_DCOffset.Real);
            strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
            strcat(string,tempstring);
		}

		sprintf(tempstring, "<Sum name=\"Combination\" In=\"AC_COMP DC_COMP\" />\n");
		strcat(string,tempstring);

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Signal\" resistance=\"%dOhm\" In=\"Combination\" />\n", m_TestEquipImp.Int);
		strcat(string,tempstring);
		
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
			}
		}
		

		sprintf(tempstring, "<HighPass name=\"Waveform\" ");
		strcat(string,tempstring);
		if(m_Bandwidth.Exists)
		{
			sprintf(tempstring, "cutoff=\"%dHz\" ", m_Bandwidth.Int);
			strcat(string,tempstring);
		}
		else
        {
			sprintf(tempstring, "cutoff=\"25000000Hz\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "Sync=\"Trigger\" ");
				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "Gate=\"Gated\" ");
				strcat(string,tempstring);
			}
		}
		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_RAMP_SIGNAL_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}

		sprintf(tempstring, "In=\"Signal\" />\n");
		strcat(string,tempstring);
		
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		
		strcat(stringinput, string);
		break;
	case FNC_PULSED_DC:
	case FNC_PULSED_DC_S:
		sprintf(string, "<Trapezoid name=\"AC_COMP\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" ", m_Volt.Real);
            strcat(string,tempstring);
        }

		if(!m_Freq.Exists)
				m_Freq.Real=1;
		sprintf(tempstring, "period=\"%gs\" ", 1/m_Freq.Real);
        strcat(string,tempstring);

		if(!m_RiseTime.Exists)
			m_RiseTime.Real=.1*(1/m_Freq.Real);
		sprintf(tempstring, "riseTime=\"%gs\" ", m_RiseTime.Real);
        strcat(string,tempstring);

		if(!m_FallTime.Exists)
			m_FallTime.Real=.1*(1/m_Freq.Real);
		sprintf(tempstring, "fallTime=\"%gs\" ", m_FallTime.Real);
        strcat(string,tempstring);

		if(!m_PulseWidth.Exists)
			m_PulseWidth.Real=.1*(1/m_Freq.Real);
		sprintf(tempstring, "pulseWidth=\"%gs\" ", m_PulseWidth.Real);
        strcat(string,tempstring);
		
		if(m_Burst.Exists)
		{
			sprintf(tempstring, "Gate=\"Burst\" ");
            strcat(string,tempstring);
        }
		strcat(string,"/>\n");

		if(m_Burst.Exists)
		{
			sprintf(tempstring, "<PulsedEvent name=\"Burst\" pulses=\"%d\" ", m_Burst.Int);
            strcat(string,tempstring);
			if(m_BurstRepRate.Exists)
			{
				sprintf(tempstring, "repetition=\"%gHz\" ", m_BurstRepRate.Real);
				strcat(string,tempstring);
			}
			strcat(string,"/>\n");
		}
		if(m_DCOffset.Exists)
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%gV\" />\n", m_DCOffset.Real);
            strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
            strcat(string,tempstring);
		}

		sprintf(tempstring, "<Sum name=\"Combination\" In=\"AC_COMP DC_COMP\" />\n");
		strcat(string,tempstring);

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Waveform\" resistance=\"%dOhm\" In=\"Combination\" ", m_TestEquipImp.Int);
		strcat(string,tempstring);

		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_PULSED_DC_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				strcat(string,"Sync=\"Trigger\" />\n");// end sum
				
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
			}
			else
			{
				strcat(string,"Gate=\"Gated\" />\n");// end sum

				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
			}
		}
		else
		{
			strcat(string, "/>\n"); //finish sum
		}
			
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		
		strcat(stringinput, string);
		break;
	case FNC_SINC_WAVE:
	case FNC_SINC_WAVE_S:
		sprintf(string, "<Sinc name=\"AC_COMP\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" ", m_Volt.Real);
            strcat(string,tempstring);
        }
		if(m_Freq.Exists)
        {
			sprintf(tempstring, "frequency=\"%gHz\" ", m_Freq.Real);
            strcat(string,tempstring);
        }
		if(m_Burst.Exists)
		{
			sprintf(tempstring, "Gate=\"Burst\" ");
            strcat(string,tempstring);
        }
		strcat(string,"/>\n");

		if(m_Burst.Exists)
		{
			sprintf(tempstring, "<PulsedEvent name=\"Burst\" pulses=\"%d\" ", m_Burst.Int);
            strcat(string,tempstring);
			if(m_BurstRepRate.Exists)
			{
				sprintf(tempstring, "repetition=\"%gHz\" ", m_BurstRepRate.Real);
				strcat(string,tempstring);
			}
			strcat(string,"/>\n");
		}

		if(m_DCOffset.Exists)
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%gV\" />\n", m_DCOffset.Real);
            strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
            strcat(string,tempstring);
		}

		sprintf(tempstring, "<Sum name=\"Combination\" In=\"AC_COMP DC_COMP\" />\n");
		strcat(string,tempstring);

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Signal\" resistance=\"%dOhm\" In=\"Combination\" />\n", m_TestEquipImp.Int);
		strcat(string,tempstring);
		
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
			}
		}
		

		sprintf(tempstring, "<HighPass name=\"Waveform\" ");
		strcat(string,tempstring);
		if(m_Bandwidth.Exists)
		{
			sprintf(tempstring, "cutoff=\"%dHz\" ", m_Bandwidth.Int);
			strcat(string,tempstring);
		}
		else
        {
			sprintf(tempstring, "cutoff=\"25000000Hz\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "Sync=\"Trigger\" ");
				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "Gate=\"Gated\" ");
				strcat(string,tempstring);
			}
		}
		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_SINC_WAVE_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}

		sprintf(tempstring, "In=\"Signal\" />\n");
		strcat(string,tempstring);
		
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		
		strcat(stringinput, string);
		break;
	case FNC_EXP_PULSE:
	case FNC_EXP_PULSE_S:
		sprintf(string, "<ExponentialWave name=\"AC_COMP\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" ", m_Volt.Real);
            strcat(string,tempstring);
        }

		if(m_Freq.Exists)
        {
			sprintf(tempstring, "period=\"%gs\" ", 1/m_Freq.Real);
            strcat(string,tempstring);
        }

		if(m_Exponent.Exists)
        {
			sprintf(tempstring, "exponent=\"%g\" ", m_Exponent.Real);
			strcat(string,tempstring);
        }
		if(m_Burst.Exists)
		{
			sprintf(tempstring, "Gate=\"Burst\" ");
            strcat(string,tempstring);
        }
		strcat(string,"/>\n");

		if(m_Burst.Exists)
		{
			sprintf(tempstring, "<PulsedEvent name=\"Burst\" pulses=\"%d\" ", m_Burst.Int);
            strcat(string,tempstring);
			if(m_BurstRepRate.Exists)
			{
				sprintf(tempstring, "repetition=\"%gHz\" ", m_BurstRepRate.Real);
				strcat(string,tempstring);
			}
			strcat(string,"/>\n");
		}
		if(m_DCOffset.Exists)
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%gV\" />\n", m_DCOffset.Real);
            strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
            strcat(string,tempstring);
		}

		sprintf(tempstring, "<Sum name=\"Combination\" In=\"AC_COMP DC_COMP\" />\n");
		strcat(string,tempstring);

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Signal\" resistance=\"%dOhm\" In=\"Combination\" />\n", m_TestEquipImp.Int);
		strcat(string,tempstring);
		
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
			}
		}
		

		sprintf(tempstring, "<HighPass name=\"Waveform\" ");
		strcat(string,tempstring);
		if(m_Bandwidth.Exists)
		{
			sprintf(tempstring, "cutoff=\"%dHz\" ", m_Bandwidth.Int);
			strcat(string,tempstring);
		}
		else
        {
			sprintf(tempstring, "cutoff=\"25000000Hz\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				sprintf(tempstring, "Sync=\"Trigger\" ");
				strcat(string,tempstring);
			}
			else
			{
				sprintf(tempstring, "Gate=\"Gated\" ");
				strcat(string,tempstring);
			}
		}
		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_EXP_PULSE_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}

		sprintf(tempstring, "In=\"Signal\" />\n");
		strcat(string,tempstring);
		
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		
		strcat(stringinput, string);
		break;
	case FNC_DC_SIGNAL:
	case FNC_DC_SIGNAL_S:
		sprintf(string, "<Constant name=\"Combination\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" />\n", m_Volt.Real);
            strcat(string,tempstring);
        }

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Waveform\" resistance=\"%dOhm\" In=\"Combination\" ", m_TestEquipImp.Int);
		strcat(string,tempstring);

		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_DC_SIGNAL_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				strcat(string,"Sync=\"Trigger\" />\n");// end sum
				
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
			}
			else
			{
				strcat(string,"Gate=\"Gated\" />\n");// end sum

				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
			}
		}
		else
		{
			strcat(string,"/>\n"); // end constant
		}
		
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		
		strcat(stringinput, string);
		break;
	case FNC_WAVEFORM:
	case FNC_WAVEFORM_S:
		WaveformString= new char[(11*m_StimSize.Int+512)];
		data= new char[(11*m_StimSize.Int+1)];
		WaveformString[0]='\0';
		data[0]='\0';
	    sprintf(WaveformString, "<Signal Name=\"Fgen_SIGNAL\" Out=\"Waveform\">\n");

		sprintf(string, "<WaveformStep name=\"AC_COMP\" ");
            
        if(m_Volt.Exists)
        {
			sprintf(tempstring, "amplitude=\"%gV\" ", m_Volt.Real);
            strcat(string,tempstring);
        }

		if(m_SampleSpacing.Exists)
        {
			sprintf(tempstring, "samplingInterval=\"%gs\" ", m_SampleSpacing.Real);
            strcat(string,tempstring);
        }

		if(m_StimSize.Exists)
        {
			strcat(WaveformString, string);
			strcat(WaveformString,"points=\"");
        	array_to_string(m_StimData, m_StimSize.Int, &data[0]);
//			strcat(WaveformString, string);
			strcat(WaveformString, data);
			strcat(WaveformString, "\" ");
		}
		strcat(string,"/>\n");
		strcat(WaveformString,"/>\n");

		if(m_DCOffset.Exists)
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"%gV\" />\n", m_DCOffset.Real);
            strcat(string,tempstring);
			strcat(WaveformString,tempstring);
		}
		else
		{
			sprintf(tempstring, "<Constant name=\"DC_COMP\" amplitude=\"0V\" />\n");
            strcat(string,tempstring);
			strcat(WaveformString,tempstring);
		}

		sprintf(tempstring, "<Sum name=\"Combination\" In=\"AC_COMP DC_COMP\" />\n");
		strcat(string,tempstring);
		strcat(WaveformString,tempstring);

		if(!m_TestEquipImp.Exists)
			m_TestEquipImp.Int=50;

		sprintf(tempstring, "<Load name=\"Waveform\" resistance=\"%dOhm\" In=\"Combination\" ", m_TestEquipImp.Int);
		strcat(string,tempstring);
		strcat(WaveformString,tempstring);

		if(m_MaxTime.Exists)
		{
			sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
			strcat(string,tempstring);
		}
		if(Fnc==FNC_WAVEFORM_S)
		{
			sprintf(tempstring, "syncOut=\"on\" ");
			strcat(string,tempstring);
		}
		else
		{
			sprintf(tempstring, "syncOut=\"off\" ");
			strcat(string,tempstring);
		}
		if(m_Event.Exists) //triggered
		{
			if(m_Event.Dim==FNC_TRG)
			{
				strcat(string,"Sync=\"Trigger\" />\n");// end sum
				strcat(WaveformString,"Sync=\"Trigger\" />\n");
					
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%gs\" In=\"Trig_Signal\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Trigger\" ");
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "nominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "nominal=\"1.6V\" ");
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "condition=\"LT\" In=\"Trigger_Delay\" />\n");
				else
					sprintf(tempstring, "condition=\"GT\" In=\"Trigger_Delay\" />\n");

				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
			}
			else
			{
				strcat(string,"Gate=\"Gated\" />\n");// end sum
				strcat(WaveformString,"Gate=\"Gated\" />\n");
				
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_To\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
				sprintf(tempstring, "<SquareWave name=\"Trig_Signal_From\" amplitude=\"5V\" />\n");
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);

				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_To\" delay=\"%gs\" In=\"Trig_Signal_To\" />\n", m_TriggerDelayTo.Real);
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
				sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay_From\" delay=\"%gs\" In=\"Trig_Signal_From\" />\n", m_TriggerDelay.Real);
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
				
				sprintf(tempstring, "<Instantaneous name=\"Gated\" ");
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
				if(m_TriggerVolt.Exists)
					sprintf(tempstring, "startNominal=\"%gV\" ", m_TriggerVolt.Real);
				else
					sprintf(tempstring, "startNominal=\"1.6V\" ");
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "startCondition=\"LT\" In=\"Trigger_Delay_From\" ");
				else
					sprintf(tempstring, "startCondition=\"GT\" In=\"Trigger_Delay_From\" ");

				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
				if(m_TriggerVoltTo.Exists)
					sprintf(tempstring, "stopNominal=\"%gV\" ", m_TriggerVoltTo.Real);
				else
					sprintf(tempstring, "stopNominal=\"1.6V\" ");
				strcat(string,tempstring);
				strcat(WaveformString,tempstring);

				if(m_TriggerSlope.Dim==NEG_SLOPE)
					sprintf(tempstring, "stopCondition=\"LT\" />\n");
				else
					sprintf(tempstring, "stopCondition=\"GT\" />\n");

				strcat(string,tempstring);
				strcat(WaveformString,tempstring);
			}
		}
		else
		{
			strcat(string, "/>\n"); // end Sum
			strcat(WaveformString,"/>\n");
		}
		
		sprintf(tempstring, "</Signal>\n");
		strcat(string,tempstring);
		strcat(WaveformString,tempstring);
		
		strcat(stringinput, string);

		strcpy(m_SignalDescription, stringinput); //save for later

		IFNSIM((Status = cs_IssueAtmlSignal("Setup", m_DeviceName, WaveformString, NULL, 0)));
		CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", WaveformString, Status));
		delete WaveformString;
		delete data;
		break;
	}

	if(Fnc!=FNC_WAVEFORM && Fnc!=FNC_WAVEFORM_S && Fnc<100)
	{
		strcpy(m_SignalDescription, stringinput); //save for later

		IFNSIM((Status = cs_IssueAtmlSignal("Setup", m_DeviceName, m_SignalDescription, NULL, 0)));
		CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", m_SignalDescription, Status));
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateFncGen(int Fnc)
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
int CFncGen_T::InitiateFncGen(int Fnc)
{
    int   Status = 0;

    // Initiate action for the FncGen
    CEMDEBUG(5,cs_FmtMsg("InitiateFncGen (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchFncGen(int Fnc)
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
int CFncGen_T::FetchFncGen(int Fnc)
{
    DATUM    *fdat;
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5000;
    char    XmlValue[1024];
    char    MeasFunc[32];
    
    /* Begin TEMPLATE_SAMPLE_CODE 
    int       MeasFunc = FncGen_VAL_DC_VOLTS;
/* End TEMPLATE_SAMPLE_CODE */

    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
        MaxTime = m_MaxTime.Real * 1000;

    // Fetch action for the FncGen
    CEMDEBUG(5,cs_FmtMsg("FetchFncGen (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    // Fetch data
    //////// Place FncGen specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE 
    // Accomidate multiple MeasChars as applicable
    switch(Fnc)
    {
    case  FNC_VOLTS_DC: // (voltage)   dc signal
            strcpy(MeasFnc,"dc_ampl");
            break;
    case  FNC_AC_COMP_DC: // (ac-comp)   dc signal
            strcpy(MeasFnc,"ac_rms");
            break;
    case  FNC_VOLTS_AC: // (voltage)   ac signal
            strcpy(MeasFnc,"ac_rms");
            break;
    case  FNC_DC_OFF_AC: // (dc-offset) ac signal
            strcpy(MeasFnc,"dc_ampl");
            break;
    }
    // replace or send proper Measure attribute name in m_SignalDescription
    IFNSIM((Status = cs_IssueAtmlSignal("Fetch", m_DeviceName, m_SignalDescription,
                                        XmlValue, 1024)));
*/
IFNSIM((Status = cs_IssueAtmlSignal("Fetch", m_DeviceName, "<SignalDescription/>",
                                        XmlValue, 1024)));
strcpy(MeasFunc,"ac_ampl");
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////

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
// Function: OpenFncGen(int Fnc)
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
int CFncGen_T::OpenFncGen(int Fnc)
{
    // Open action for the FncGen
    CEMDEBUG(5,cs_FmtMsg("OpenFncGen (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    //////// Place FncGen specific data here //////////////

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseFncGen(int Fnc)
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
int CFncGen_T::CloseFncGen(int Fnc)
{
	int Status=0;
    // Close action for the FncGen
    CEMDEBUG(5,cs_FmtMsg("CloseFncGen (%s) called FNC %d", 
                          m_DeviceName, Fnc));

	if(Fnc<100)
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SignalDescription,
										NULL, 0)));
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetFncGen(int Fnc)
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
int CFncGen_T::ResetFncGen(int Fnc)
{
    int   Status = 0;

    // Reset action for the FncGen
    CEMDEBUG(5,cs_FmtMsg("ResetFncGen (%s) called FNC %d", 
                          m_DeviceName, Fnc));
    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
    if(Fnc != 0)
    {
        // Reset the FncGen
        IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, "<Signal Name=\"Fgen_SIGNAL\" Out=\"Waveform\"/>\n", NULL, 0)));
    }

    InitPrivateFncGen();

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoFncGen(int Fnc)
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
int CFncGen_T::GetStmtInfoFncGen(int Fnc)
{
    DATUM    *datum;
    char *cp;

	//voltage M_VOLT
	if((datum = RetrieveDatum(M_VOLT, K_SET)))
    {
		if(Fnc>100)
		{
			m_TriggerVolt.Exists = TRUE;
			m_Event.Exists=true;
			m_Event.Dim=Fnc;
			switch(Fnc)
			{
			case FNC_TRG:
				m_TriggerVolt.Real = DECDatVal(datum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Trigger-Voltage %lf",m_TriggerVolt.Real));
			break;
			case FNC_GATE_FROM:
				m_TriggerVolt.Real = DECDatVal(datum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Gate-From Voltage %lf",m_TriggerVolt.Real));
			break;
			case FNC_GATE_TO:
				m_TriggerVoltTo.Real = DECDatVal(datum, 0);
				CEMDEBUG(10,cs_FmtMsg("Found Gate-To %lf",m_TriggerVoltTo.Real));
				break;
			}
		}
		else
		{
			m_Volt.Exists = TRUE;
			m_Volt.Real = DECDatVal(datum, 0);
			if(Fnc!=FNC_DC_SIGNAL && Fnc!=FNC_DC_SIGNAL_S)
				m_Volt.Real = m_Volt.Real * SQRT_2; //all voltages stored as peak

			CEMDEBUG(10,cs_FmtMsg("Found Volt %lf",m_Volt.Real));
		}
		FreeDatum(datum);
    }

    //voltage-p M_VLPK
	if((datum = RetrieveDatum(M_VLPK, K_SET)))
    {
        m_Volt.Exists = TRUE;
        m_Volt.Real = DECDatVal(datum, 0); //all voltages stored as peak
        CEMDEBUG(10,cs_FmtMsg("Found Volt %lf",m_Volt.Real));
        FreeDatum(datum);
    }
	
	//voltage-pp M_VLPP
    if((datum = RetrieveDatum(M_VLPP, K_SET)))
    {
        m_Volt.Exists = TRUE;
        m_Volt.Real = DECDatVal(datum, 0) / 2; //all voltages stored as peak
        CEMDEBUG(10,cs_FmtMsg("Found Volt %lf",m_Volt.Real));
        FreeDatum(datum);
    }
	
	//dc-offset M_DCOF
	if((datum = RetrieveDatum(M_DCOF, K_SET)))
    {
        m_DCOffset.Exists = TRUE;
        m_DCOffset.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found DC-Offset %lf",m_DCOffset.Real));
        FreeDatum(datum);
    }

    //freq M_FREQ
	if((datum = RetrieveDatum(M_FREQ, K_SET)))
    {
        m_Freq.Exists = TRUE;
        m_Freq.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Freq %lf",m_Freq.Real));
        FreeDatum(datum);
    }

	 //period M_PERI
	if((datum = RetrieveDatum(M_PERI, K_SET)))
    {
        m_Freq.Exists = TRUE;
        m_Freq.Real = 1/DECDatVal(datum, 0); //store period as frequency
        CEMDEBUG(10,cs_FmtMsg("Found Freq %lf",m_Freq.Real));
        FreeDatum(datum);
    }

	//pulse-width M_PLWD
	if((datum = RetrieveDatum(M_PLWD, K_SET)))
    {
        m_PulseWidth.Exists = TRUE;
        m_PulseWidth.Real = DECDatVal(datum, 0); 
        CEMDEBUG(10,cs_FmtMsg("Found Pulse-Width %lf",m_PulseWidth.Real));
        FreeDatum(datum);
    }

	//prf M_PRFR
	if((datum = RetrieveDatum(M_PRFR, K_SET)))
    {
		//burst rate
		if(Fnc==FNC_EXP_PULSE || Fnc==FNC_EXP_PULSE)
		{
			m_BurstRepRate.Exists = TRUE;
			m_BurstRepRate.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Prf %lf",m_BurstRepRate.Real));
		}
		else //frequency
		{
			m_Freq.Exists = TRUE;
			m_Freq.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Prf %lf",m_Freq.Real));
		}
		FreeDatum(datum);
    }

	//burst-rep-rate M_BURR
	if((datum = RetrieveDatum(M_BURR, K_SET)))
    {
        m_BurstRepRate.Exists = TRUE;
        m_BurstRepRate.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Burst-Rep-Rate %lf",m_Freq.Real));
        FreeDatum(datum);
    }

    //burst M_BURS
	if((datum = RetrieveDatum(M_BURS, K_SET)))
    {
        m_Burst.Exists = TRUE;
        m_Burst.Int = (int)DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found Burst %d",m_Burst.Int));
        FreeDatum(datum);
    }

    //bandwidth M_BAND
	if((datum = RetrieveDatum(M_BAND, K_SET)))
    {
        m_Bandwidth.Exists = TRUE;
        m_Bandwidth.Int = (int)DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found Bandwidth %d",m_Bandwidth.Int));
        FreeDatum(datum);
    }

	//duty-cycle M_DUTY
	if((datum = RetrieveDatum(M_DUTY, K_SET)))
    {
        m_DutyCycle.Exists = TRUE;
        m_DutyCycle.Int = (int)DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found Duty-Cycle %d",m_DutyCycle.Int));
        FreeDatum(datum);
    }

    //rise-time M_RISE
	if((datum = RetrieveDatum(M_RISE, K_SET)))
    {
        m_RiseTime.Exists = TRUE;
        m_RiseTime.Real = DECDatVal(datum, 0); 
        CEMDEBUG(10,cs_FmtMsg("Found Rise-Time %lf",m_RiseTime.Real));
        FreeDatum(datum);
    }

    //fall-time M_FALL
	if((datum = RetrieveDatum(M_FALL, K_SET)))
    {
        m_FallTime.Exists = TRUE;
        m_FallTime.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Fall-Time %lf",m_FallTime.Real));
        FreeDatum(datum);
    }
	
	//exponent M_EXPO
	if((datum = RetrieveDatum(M_EXPO, K_SET)))
    {
        m_Exponent.Exists = TRUE;
        m_Exponent.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Exponent %lf",m_Exponent.Real));
        FreeDatum(datum);
    }

    //stim M_STIM
	if((datum = RetrieveDatum(M_STIM, K_SET)))
    {
        m_StimSize.Exists = true;
        m_StimSize.Int = DatCnt (datum);   //get # of vars
        
        for (int i = 0; i < m_StimSize.Int; i++)
        {
            m_StimData[i]=DECDatVal(datum,i);
            CEMDEBUG(10,  cs_FmtMsg("Stim word %d is %lf",i, m_StimData[i]));
        }
		FreeDatum(datum);
    }

    //sample-spacing M_SASP
	if((datum = RetrieveDatum(M_SASP, K_SET)))
    {
        m_SampleSpacing.Exists = TRUE;
        m_SampleSpacing.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Sample-Spacing %lf",m_SampleSpacing.Real));
        FreeDatum(datum);
    }

    //test-equip-imp M_TIMP
	if((datum = RetrieveDatum(M_TIMP, K_SET)))
    {
        m_TestEquipImp.Exists = TRUE;
        m_TestEquipImp.Int = (int)DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found Test-Equip-Imp %d",m_TestEquipImp.Int));
        FreeDatum(datum);
    }

    //max-time M_MAXT
	if((datum = RetrieveDatum(M_MAXT, K_SET)))
    {
        m_MaxTime.Exists = TRUE;
        m_MaxTime.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Max-Time %lf",m_MaxTime.Real));
        FreeDatum(datum);
    }

	//event-slope M_EVSL
	if((datum = RetrieveDatum(M_EVSL, K_SET)))
    {		
        m_TriggerSlope.Exists = true;
        cp = GetTXTDatVal(datum, 0);
		m_Event.Exists=true;
		m_Event.Dim=Fnc;
		switch(Fnc)
		{
		case FNC_TRG:
			m_TriggerSlope.Dim = TXTtoINT(cp);
			CEMDEBUG(9,cs_FmtMsg("Found Event-Slope %d",m_TriggerSlope.Dim));
			break;
		case FNC_GATE_FROM:
			m_TriggerSlope.Dim = TXTtoINT(cp);
			CEMDEBUG(9,cs_FmtMsg("Found Gate-From Slope %d",m_TriggerSlope.Dim));
			break;
		case FNC_GATE_TO:
			m_TriggerSlopeTo.Dim = TXTtoINT(cp);
			CEMDEBUG(9,cs_FmtMsg("Found Gate-To Slope %d",m_TriggerSlopeTo.Dim));
			break;
		}
		FreeDatum(datum);
    }

	//event-delay/delay M_EVDL/M_DELA
	if((datum = RetrieveDatum(M_EVDL, K_SET)) ||
		(datum = RetrieveDatum(M_DELA, K_SET)))
    {
        m_TriggerDelay.Exists = TRUE;
		m_Event.Exists=true;
		m_Event.Dim=Fnc;
		switch(Fnc)
		{
		case FNC_TRG:
			m_TriggerDelay.Real = DECDatVal(datum, 0);
        	CEMDEBUG(10,cs_FmtMsg("Found Trigger-Delay %lf",m_TriggerDelay.Real));
        break;
		case FNC_GATE_FROM:
			m_TriggerDelay.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Gate-from Delay %lf",m_TriggerDelay.Real));
        break;
		case FNC_GATE_TO:
			m_TriggerDelayTo.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Gate-to Delay %lf",m_TriggerDelayTo.Real));
			break;
		}
        FreeDatum(datum);
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateFncGen()
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
void CFncGen_T::InitPrivateFncGen(void)
{
	m_Volt.Exists=false;
	m_DCOffset.Exists=false;
	m_Freq.Exists=false;
	m_Burst.Exists=false;
	m_Bandwidth.Exists=false;
	m_DutyCycle.Exists=false;
	m_BurstRepRate.Exists=false;
	m_RiseTime.Exists=false;
	m_FallTime.Exists=false;
	m_Exponent.Exists=false;
	m_StimSize.Exists=false;
	m_SampleSpacing.Exists=false;
	m_TestEquipImp.Exists=false;
	m_MaxTime.Exists=false;
	m_TriggerDelay.Exists=false;
	m_TriggerSlope.Exists=false;
	m_TriggerVolt.Exists=false;
	m_PulseWidth.Exists=false;
	m_TriggerDelayTo.Exists=false;
    m_TriggerSlopeTo.Exists=false;
	m_TriggerVoltTo.Exists=false;
	m_Event.Exists=false;
	
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateFncGen()
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
void CFncGen_T::NullCalDataFncGen(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
    return;
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: TXTtoINT(char * TXT)
//
// Purpose: Accepts a string from ATLAS and returns a corresponding integer
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// TXT				char *			String value
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: int
//
///////////////////////////////////////////////////////////////////////////////
int TXTtoINT(char * TXT)
{
	if(strcmp(TXT, "POS")==0) //Positive
		return 1;
	if(strcmp(TXT, "NEG")==0) //Negative
		return 2;
	
	return 1;  //default Positive
}
///////////////////////////////////////////////////////////////////////////////
// Function: array_to_string()
//
// Purpose: Converts an double array to a Null terminated string containing
//           integer values separated by ", "
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            double *        double array to be converted
// size             int             size of integer array to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// output           char *          Null terminated string in 1641 format
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void array_to_string(double array [], int size, char output [])
{
	char temp[20]="";
    output[0]='\0';
	for(int i=0; i<size; i++)
	{
		sprintf(temp,"%9.5lf", array[i]);
		rm_white(&temp[0]);
		if(i!=size-1)
			strcat(temp,", ");
		strcat(output,temp);
		temp[0]='\0';
	}
    return;
}

void rm_white(char * fix)
{
	if(!fix)
		return;

	int start=0;
	char temp[512];

	int i=0;
	while(fix[i]!='\0')
	{
		if(fix[i]==' ')
			start=i+1;
		i++;
	}
	if(i!=start)//only try to remove whitespace from the beginning
	{
		strncpy(temp, (fix+start), (size_t)(i-start+1));
		strcpy(fix, temp);
	}
	return;
}