//345678901234567890123456789012345678901234567890123456789012345678901234567890
////////////////////////////////////////////////////////////////////////////////
// File:	    Arb_Gen_T.cpp
//
// Date:	    11-OCT-05
//
// Purpose:	    Instrument Driver for Arb
//
// Instrument:	Arb  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  =====================================================
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
//////// Place Arb specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE 
//   1  sensor (voltage)   dc signal
//   2  sensor (ac-comp)   dc signal
//
//  11  sensor (voltage)   ac signal
//  12  sensor (dc-offset) ac signal
/* End TEMPLATE_SAMPLE_CODE */
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline                         
// 1.4.1.0  23FEB09  Fixed freeing of pointer in setup that caused exception,
//                   change debug display for IssueSignal so only strlen < 1000
//                   are displayed in debug.
// 1.4.2.0  17APR09  Modified code to allow large number of Waveform array points 
// 1.4.2.1  27MAY09  added code in Setup and Reset that will exit the function 
//                   after getting the modifier data for events
////////////////////////////////////////////////////////////////////////////////

#pragma warning (disable : 4996)

// Includes
/* Begin TEMPLATE_SAMPLE_CODE 
#include <float.h>
#include <math.h>
/* End TEMPLATE_SAMPLE_CODE */
#include <math.h>
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Arb_Gen_T.h"
#include <stdio.h>

// Local Defines

// Function codes
#define  FNC_UNDEFINED			0
#define  FNC_AC_SIGNAL          1
#define  FNC_SQUARE_WAVE        2
#define  FNC_RAMP_SIGNAL        3
#define  FNC_WAVEFORM           4
#define  FNC_TRIANGULAR_WAVE    6
#define  FNC_DC_SIGNAL          7
#define  FNC_EVENT            102


#define SQRT_2				1.414213562373
#define SQRT_3				1.73205080808

#define CHANNEL_1A                   1
#define CHANNEL_1B                   2
#define CHANNEL_2A                   3
#define CHANNEL_2B                   4

#define POS_SLOPE			1
#define NEG_SLOPE			2
//////////////////////////////////////////////////////////

#define CAL_TIME       (86400 * 365) /* one year */

#define BASE_SIZE	4096
// Static Variables

// Local Function Prototypes
int TXTtoINT(char * TXT);
void array_to_string(double array [], int size, char output []);
void rm_white(char * fix);
int convertBinary(int number, double array []);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CArb_Gen_T(int Bus, int PrimaryAdr, int SecondaryAdr,
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
CArb_Gen_T::CArb_Gen_T(char *DeviceName, 
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

	m_SignalDescription = NULL;

    InitPrivateArb_Gen();
	NullCalDataArb_Gen();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("Arb Class called with Device [%s], Sim %d, Dbg %d", DeviceName, Sim, Dbg));

    // Initialize the Arb - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CArb_Gen_T()
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
CArb_Gen_T::~CArb_Gen_T()
{
    // Reset the Arb
    CEMDEBUG(5,cs_FmtMsg("Arb Class Destructor called for Device [%s], ", m_DeviceName));


    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusArb_Gen(int Fnc)
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_Gen_T::StatusArb_Gen(int Fnc)
{
    int Status = 0;

	if (Fnc >100)
	{
		//If Trigger function, don't invoke wrapper
	}
	else
	{
		// Status action for the Arb
		CEMDEBUG(5,cs_FmtMsg("StatusArb (%s) called FNC %d", m_DeviceName, Fnc));
		// Check for any pending error messages
	 
		IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, m_SignalDescription, NULL, 0)));
		//CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %d", m_SignalDescription, Status));
		if (strlen(m_SignalDescription) < 1000)
			CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", m_SignalDescription, Status));
		else
			CEMDEBUG(9,cs_FmtMsg("IssueSignal [Status] %d", Status));
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupArb_Gen_T(int Fnc)
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_Gen_T::SetupArb_Gen(int Fnc)
{
    int Status = 0;
	double  MaxTime = 5.5;

	char headerstring[256] = "";	// used to hold the signal name
	char constring[256] = "";	    // used to hold the the channel number of signals
	char triggerstring[256] = "";	// holds any trigger/gated components
	char *modes = NULL;				// holds the xml mode strings that arb is capable
	char sumstring[1028] = "";		// used for the summing of the signals
	char burststring[512] = "";		// used to hold the burst components of the signals
	char filterstring[512] = "";	// used to hold the filter components of the signals
	char loadstring[512] = "";		// used to hold the load (Test-Equip-imp) of a signals
	char signalname[512] = "";		// holds the signal name of the main signal for input into others
	char tempstring[1028] = "";		// used temporarily to hold strings
	char *data = NULL;				// used to hold waveform data


    char string[1028] = "";			// generic use

	int stringsize = 0;    
	double   square_root2;

	square_root2 = 1.41421;
    
   
    //////////////////////////////////////////////////////////

    // Setup action for the Arb
    CEMDEBUG(5,cs_FmtMsg("SetupArb (%s) called FNC %d", m_DeviceName, Fnc));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
        return(0);
    
	if((Status = GetStmtInfoArb_Gen(Fnc)) != 0)
        return(0);

	// Only process mod data for events.
	if (Fnc > 100)
		return 0;

	// see if we have a trigger or gate signals 
	if(m_EventSync.Exists == true) //triggered
	{
			
		sprintf(triggerstring, "<Instantaneous name=\"Trigger\" ");
		sprintf(tempstring, "nominal=\"1.6V\" ");
		strcat(triggerstring,tempstring);

		if(m_vEvents[m_EventSync.Int].Slope.Dim == NEG_SLOPE)
		{
			sprintf(tempstring, "condition=\"LT\" In=\"TrigHi\"/>\n");
		}
		else if(m_vEvents[m_EventSync.Int].Slope.Dim == POS_SLOPE)
		{
			sprintf(tempstring, "condition=\"GT\" In=\"TrigHi\"/>\n");
		}
		strcat(triggerstring,tempstring);
		sprintf(signalname, "Trigger");

		if(m_EventDelay.Exists)
		{
			sprintf(tempstring, "<SignalDelay name=\"Trigger_Delay\" delay=\"%g sec\" In=\"Trig_Signal\" />\n", m_vEvents[m_EventSync.Int].EventDelay.Real);
			strcat(triggerstring,tempstring);
			sprintf(signalname, "Trigger_Delay");
		}
	}		
	else if (m_EventGatedBy.Exists == true)
	{			
		sprintf(triggerstring, "<Instantaneous name=\"Trigger\" ");			
		sprintf(signalname, "Trigger");
		sprintf(tempstring, "nominal=\"1.6V\" ");
		
		strcat(triggerstring,tempstring);

		if(m_vEvents[m_EventGatedBy.Int].Slope.Dim == NEG_SLOPE)
		{
			sprintf(tempstring, "condition=\"LT\" In=\"Gate\"/>\n");
		}
		else if(m_vEvents[m_EventGatedBy.Int].Slope.Dim == POS_SLOPE)
		{
			sprintf(tempstring, "condition=\"GT\" In=\"Gate\"/>\n");
		}
		strcat(triggerstring,tempstring);				

		if(m_EventDelay.Exists)
		{
			sprintf(tempstring, "<SignalDelay name=\"Gated_Delay\" delay=\"%g s\" In=\"Gated\" />\n", m_vEvents[m_EventGatedBy.Int].EventDelay.Real);
			strcat(triggerstring,tempstring);
			sprintf(signalname,"Gated_Delay");
		}
	}

    switch(Fnc)
    {		
		case FNC_AC_SIGNAL:

			// allocate memory for mode
			modes = new char[1024];

			// see if we have a trigger
			if( (m_EventSync.Exists) || (m_EventGatedBy.Exists) )
			{
				if(m_EventSync.Exists)
				{	
					sprintf(modes, "<Sinusoid name=\"AC_Component\" Sync=\"Trigger\"");						
				}
				else if(m_EventSync.Exists)
				{					
					sprintf(modes, "<Sinusoid name=\"AC_Component\" Gate=\"Gated\"");
				}
			}			
			else
			{
				sprintf(modes, "<Sinusoid name=\"AC_Component\"");
			}

			// store the signal name so it can be used later			
			sprintf(signalname, "AC_Component");

			if(m_Voltage.Exists)
			{                
				sprintf(string, " amplitude=\"%g V\"", m_Voltage.Real);
				strcat(modes, string);
			}
			
			if(m_Frequency.Exists)
			{
				sprintf(string, " frequency=\"%g Hz\"", m_Frequency.Real);
				strcat(modes, string);
			}
			else if(m_Period.Exists)
			{
				sprintf(string, " frequency=\"%g Hz\"", (1 / m_Period.Real));
					strcat(modes, string);
			}

			if(m_FreqWindow.Exists)
			{
				sprintf(string, " frequencyBand=\"%g Hz\"", (m_FreqWindowMax.Real - m_FreqWindowMin.Real));
				strcat(modes, string);
				sprintf(string, " centerFrequency=\"%g Hz\"", ((m_FreqWindowMax.Real - m_FreqWindowMin.Real) /2.0));
				strcat(modes, string);
			
				if(m_AgeRate.Exists)
				{
					sprintf(string, " ageRate=\"%d Hz/sec\"", m_AgeRate.Int);
					strcat(modes, string);
				}
			}
			
			strcat(modes, "/>\n");				
			sprintf(string, "<Constant name=\"DC_Level\" amplitude=\"%g V\"/>\n", m_DCOffset.Real);				
			strcat(modes, string);
            
			// sum up the ac and dc components
			sprintf(sumstring, "<Sum name=\"Sum\" In=\"DC_Level %s\"/>\n", signalname);
			sprintf(signalname, "Sum");
					
			// set up the burst if it exists
			if(m_Burst.Exists)
			{
				if(!m_Frequency.Exists)
					m_Frequency.Real=1;

				if(m_BurstRepRate.Exists)
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						0,             //repeat infinate 
						signalname);
				}
				else
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						1,             //repeat once 
						signalname);
				}
				sprintf(signalname, "Burst");
			}				

			// set up the bandwidth filter if it exists
			if(!m_Bandwidth.Exists) 
			{	
				m_Bandwidth.Real = 10000000.0;
			}
			sprintf(filterstring, "<LowPass name=\"Filter\" cutoff=\"%fHz\" In=\"%s\" />\n", m_Bandwidth.Real, signalname);				
			sprintf(signalname, "Filter");

			if(m_MaxTime.Exists)
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\" maxTime=\"%g s\"/>\n", m_TestEquipImp.Int, signalname, m_MaxTime.Real);	
			}
			else
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\"/>\n", m_TestEquipImp.Int, signalname);	
			}
			sprintf(signalname, "Load");		

			break;

        case FNC_RAMP_SIGNAL:

			// allocate memory for modes
			modes = new char[1024];

			// see if we have a trigger
			if( (m_EventSync.Exists) || (m_EventGatedBy.Exists) )
			{
				if(m_EventSync.Exists)
				{	
					sprintf(modes, "<Ramp name=\"Ramp_Component\" Sync=\"Trigger\"");						
				}
				else if(m_EventSync.Exists)
				{					
					sprintf(modes, "<Ramp name=\"Ramp_Component\" Gate=\"Gated\"");
				}
			}			
			else
			{
				sprintf(modes, "<Ramp name=\"Ramp_Component\"");
			}

			// store the signal name so it can be used later
			sprintf(signalname, "Ramp_Component");
					
			if(m_Voltage.Exists)
			{                
				sprintf(string, " amplitude=\"%g V\"", m_Voltage.Real);
				strcat(modes, string);
			}
			
			if (m_Period.Exists)
			{
				sprintf(string, " period=\"%g Sec\"", m_Period.Real);
				strcat(modes, string);
			}
			else if(m_Frequency.Exists)
			{
				sprintf(string, " period=\"%g Sec\"", (1 / m_Frequency.Real));
				strcat(modes, string);
			}

			strcat(modes, "/>\n");				
			sprintf(string, "<Constant name=\"DC_Level\" amplitude=\"%g V\"/>\n", m_DCOffset.Real);				
			strcat(modes, string);
            
			// sum up the ac and dc components
            sprintf(sumstring, "<Sum name=\"Sum\" In=\"DC_Level %s\"/>\n", signalname);
			sprintf(signalname, "Sum");
						
			// set up the burst if it exists
			if(m_Burst.Exists)
			{
				if(!m_Frequency.Exists)
					m_Frequency.Real=1;

				if(m_BurstRepRate.Exists)
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						0,             //repeat infinate 
						signalname);
				}
				else
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						1,             //repeat once 
						signalname);
				}
				sprintf(signalname, "Burst");
			}				

			// set up the bandwidth filter if it exists
			if(!m_Bandwidth.Exists) 
			{	
				m_Bandwidth.Real = 10000000.0;
			}
			sprintf(filterstring, "<LowPass name=\"Filter\" cutoff=\"%fHz\" In=\"%s\" />\n", m_Bandwidth.Real, signalname);				
			sprintf(signalname, "Filter");

			if(m_MaxTime.Exists)
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\" maxTime=\"%g s\"/>\n", m_TestEquipImp.Int, signalname, m_MaxTime.Real);	
			}
			else
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\"/>\n", m_TestEquipImp.Int, signalname);	
			}
			sprintf(signalname, "Load");			

			break;

		case FNC_SQUARE_WAVE:

			// allocate memory for modes
			modes  = new char[1024];

			// see if we have a trigger
			if( (m_EventSync.Exists) || (m_EventGatedBy.Exists) )
			{
				if(m_EventSync.Exists)
				{	
					sprintf(modes, "<SquareWave name=\"SquareWave_Component\" Sync=\"Trigger\"");						
				}
				else if(m_EventSync.Exists)
				{					
					sprintf(modes, "<SquareWave name=\"SquareWave_Component\" Gate=\"Gated\"");
				}
			}			
			else
			{
				sprintf(modes, "<SquareWave name=\"SquareWave_Component\"");
			}

			// store the signal name so it can be used later			
			sprintf(signalname, "SquareWave_Component");

			if(m_Voltage.Exists)
			{                
				sprintf(string, " amplitude=\"%g V\"", m_Voltage.Real);
				strcat(modes, string);
			}
			
			if (m_Period.Exists)
			{
				sprintf(string, " period=\"%g Sec\"", m_Period.Real);
				strcat(modes, string);
			}
			else if(m_Frequency.Exists)
			{
				sprintf(string, " period=\"%g Sec\"", (1 / m_Frequency.Real));
				strcat(modes, string);
			}

			strcat(modes, "/>\n");				
			sprintf(string, "<Constant name=\"DC_Level\" amplitude=\"%g V\"/>\n", m_DCOffset.Real);				
			strcat(modes, string);
            
			// sum up the ac and dc components
            sprintf(sumstring, "<Sum name=\"Sum\" In=\"DC_Level %s\"/>\n", signalname);
			sprintf(signalname, "Sum");
						
			// set up the burst if it exists
			if(m_Burst.Exists)
			{
				if(!m_Frequency.Exists)
					m_Frequency.Real=1;

				if(m_BurstRepRate.Exists)
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						0,             //repeat infinate 
						signalname);
				}
				else
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						1,             //repeat once 
						signalname);
				}
				sprintf(signalname, "Burst");
			}				

			// set up the bandwidth filter if it exists
			if(!m_Bandwidth.Exists) 
			{	
				m_Bandwidth.Real = 10000000.0;
			}
			sprintf(filterstring, "<LowPass name=\"Filter\" cutoff=\"%fHz\" In=\"%s\" />\n", m_Bandwidth.Real, signalname);				
			sprintf(signalname, "Filter");

			if(m_MaxTime.Exists)
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\" maxTime=\"%g s\"/>\n", m_TestEquipImp.Int, signalname, m_MaxTime.Real);	
			}
			else
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\"/>\n", m_TestEquipImp.Int, signalname);	
			}
			sprintf(signalname, "Load");			

			break;

		case FNC_TRIANGULAR_WAVE:

            // allocate memory for modes
			modes = new char[1024];

			// see if we have a trigger
			if( (m_EventSync.Exists) || (m_EventGatedBy.Exists) )
			{
				if(m_EventSync.Exists)
				{	
					sprintf(modes, "<Triangle name=\"TriangleWave_Component\" Sync=\"Trigger\"");						
				}
				else if(m_EventSync.Exists)
				{					
					sprintf(modes, "<Triangle name=\"TriangleWave_Component\" Gate=\"Gated\"");
				}
			}			
			else
			{
				sprintf(modes, "<Triangle name=\"TriangleWave_Component\"");
			}
 
			// store the signal name so it can be used later
			sprintf(signalname, "TriangleWave_Component");

			if(m_Voltage.Exists)
			{                
				sprintf(string, " amplitude=\"%g V\"", m_Voltage.Real);
				strcat(modes, string);
			}
			
			if (m_Period.Exists)
			{
				sprintf(string, " period=\"%g Sec\"", m_Period.Real);
				strcat(modes, string);
			}
			else if(m_Frequency.Exists)
			{
				sprintf(string, " period=\"%g Sec\"", (1 / m_Frequency.Real));
				strcat(modes, string);
			}

			strcat(modes, "/>\n");				
			sprintf(string, "<Constant name=\"DC_Level\" amplitude=\"%g V\"/>\n", m_DCOffset.Real);				
			strcat(modes, string);
            
			// sum up the ac and dc components
            sprintf(sumstring, "<Sum name=\"Sum\" In=\"DC_Level %s\"/>\n", signalname);
			sprintf(signalname, "Sum");
						
			// set up the burst if it exists
			if(m_Burst.Exists)
			{
				if(!m_Frequency.Exists)
					m_Frequency.Real=1;

				if(m_BurstRepRate.Exists)
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						0,             //repeat infinate 
						signalname);
				}
				else
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						1,             //repeat once 
						signalname);
				}
				sprintf(signalname, "Burst");
			}				

			// set up the bandwidth filter if it exists
			if(!m_Bandwidth.Exists) 
			{	
				m_Bandwidth.Real = 10000000.0;
			}
			sprintf(filterstring, "<LowPass name=\"Filter\" cutoff=\"%fHz\" In=\"%s\" />\n", m_Bandwidth.Real, signalname);				
			sprintf(signalname, "Filter");

			if(m_MaxTime.Exists)
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\" maxTime=\"%g s\"/>\n", m_TestEquipImp.Int, signalname, m_MaxTime.Real);	
			}
			else
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\"/>\n", m_TestEquipImp.Int, signalname);	
			}
			sprintf(signalname, "Load");			

			break;



		case FNC_WAVEFORM:

			// allocate memory for data array and modes
			data = new char[m_Stim.Int * 16];
			modes = new char[(m_Stim.Int * 16) + 1024];

			// see if we have a trigger
			if( (m_EventSync.Exists) || (m_EventGatedBy.Exists) )
			{
				if(m_EventSync.Exists)
				{	
					sprintf(modes, "<WaveformStep name=\"Waveform_Component\" Sync=\"Trigger\"");						
				}
				else if(m_EventSync.Exists)
				{					
					sprintf(modes, "<WaveformStep name=\"Waveform_Component\" Gate=\"Gated\"");
				}
			}			
			else
			{
				sprintf(modes, "<WaveformStep name=\"Waveform_Component\"");
			}
 
			// store the signal name so it can be used later			
			sprintf(signalname, "Waveform_Component");

	
			if(m_Voltage.Exists)
			{                
				sprintf(string, " amplitude=\"%g V\"", m_Voltage.Real);
				strcat(modes, string);
			}
			
			if(m_Frequency.Exists)
			{
				sprintf(string, " frequency=\"%g Hz\"", m_Frequency.Real);
				strcat(modes, string);
			}
			else if(m_Period.Exists)
			{
				sprintf(string, " frequency=\"%g Hz\"", (1 / m_Period.Real));
				strcat(modes, string);
			}

			// set up the Sample-Spacing if it exists
			if(m_SampleSpacing.Exists)
			{
				sprintf(string, " samplingInterval=\"%8.12lf sec\"", m_SampleSpacing.Real);
				strcat(modes, string);
			}

			if(m_Stim.Exists)
			{
				//sprintf(string," points=\"");
				strcat(modes," points=\"");
        		array_to_string(m_StimData, m_Stim.Int, data);
				strcat(modes, data);
				strcat(modes, "\" ");
			}
							
			delete [] data;

			strcat(modes, "/>\n");				
			sprintf(string, "<Constant name=\"DC_Level\" amplitude=\"%g V\"/>\n", m_DCOffset.Real);				
			strcat(modes, string);
            
			// sum up the ac and dc components
            sprintf(sumstring, "<Sum name=\"Sum\" In=\"DC_Level %s\"/>\n", signalname);
			sprintf(signalname, "Sum");
		
			// set up the burst if it exists
			if(m_Burst.Exists)
			{
				if(!m_Frequency.Exists)
					m_Frequency.Real=1;

				if(m_BurstRepRate.Exists)
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						0,             //repeat infinate 
						signalname);
				}
				else
				{
					sprintf(burststring, "<PulseTrain name=\"Burst\" pulses=\"[(0,%g,1),(%g,%g,0)]\" repetition=\"%d cycles\" In=\"%s\" />\n", 
						((double)m_Burst.Int/m_Frequency.Real), //time of burst
						((double)m_Burst.Int/m_Frequency.Real), 
						(1./m_BurstRepRate.Int)-((double)m_Burst.Int/m_Frequency.Real),  //rest of time
						1,             //repeat once 
						signalname);
				}
				sprintf(signalname, "Burst");
			}				

			// set up the bandwidth filter if it exists
			if(!m_Bandwidth.Exists) 
			{	
				m_Bandwidth.Real = 10000000.0;
			}
			sprintf(filterstring, "<LowPass name=\"Filter\" cutoff=\"%fHz\" In=\"%s\" />\n", m_Bandwidth.Real, signalname);				
			sprintf(signalname, "Filter");

			if(m_MaxTime.Exists)
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\" maxTime=\"%g s\"/>\n", m_TestEquipImp.Int, signalname, m_MaxTime.Real);	
			}
			else
			{
				sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\"/>\n", m_TestEquipImp.Int, signalname);	
			}
			sprintf(signalname, "Load");			


			break; //end of waveform
				
		case FNC_DC_SIGNAL:  		
						      
			// allocate memory for modes
			modes = new char[1024];

			// see if we have a trigger
			if( (m_EventSync.Exists) || (m_EventGatedBy.Exists) )
			{
				if(m_EventSync.Exists)
				{	
					sprintf(modes, "<Constant name=\"DC_Level\" Sync=\"Trigger\"");						
				}
				else if(m_EventSync.Exists)
				{					
					sprintf(modes, "<Constant name=\"DC_Level\" Gate=\"Gated\"");
				}
			}			
			else
			{
				sprintf(modes, "<Constant name=\"DC_Level\"");
			}

			// store the signal name so it can be used later
			sprintf(signalname, "DC_Level");
		
			if(m_Voltage.Exists)
			{                
				sprintf(string, " amplitude=\"%g V\"", m_Voltage.Real);
				strcat(modes, string);
			}

			// close off the active open strings
			strcat(modes, "/>\n");
		
			sprintf(loadstring, "<Load name=\"Load\" resistance=\"%d Ohm\" In=\"%s\"/>\n", m_TestEquipImp.Int, signalname);	
			
			// store the signal name so it can be used later
			sprintf(signalname, "Load");

			break;

		default:
			break;

	} // end of switch statement

	// get the information to build the 1641 signal strings
	if((m_EventSync.Exists) || (m_EventGatedBy.Exists))
	{
		sprintf(headerstring, "<Signal Name=\"ARB_SIGNAL\" Out=\"Waveform\" In=\"TrigHi\">\n");
		sprintf(constring,  "<port name=\"Waveform\" In=\"%s\"/>\n", signalname);
		strcat(constring,   "<port name=\"TrigHi\"/>\n");
	}
	else
	{
		sprintf(headerstring, "<Signal Name=\"ARB_SIGNAL\" Out=\"Waveform\">\n");
		sprintf(constring,  "<port name=\"Waveform\" In=\"%s\"/>\n", signalname);
	}

	// Calculate size for final string
	stringsize = strlen(headerstring);
	stringsize += strlen(constring);
	stringsize += strlen(triggerstring);
	stringsize += strlen(modes);
	stringsize += strlen(sumstring);
	stringsize += strlen(burststring);
	stringsize += strlen(filterstring);
	stringsize += strlen(loadstring);
	stringsize += 1024;		// add size for overhead
	m_SignalDescription = new char[stringsize];

	// cat together resultant string 
	strcpy(m_SignalDescription, headerstring);
	strcat(m_SignalDescription, constring);
	strcat(m_SignalDescription, triggerstring);
	strcat(m_SignalDescription, modes);
	strcat(m_SignalDescription, sumstring);
	strcat(m_SignalDescription, burststring);
	strcat(m_SignalDescription, filterstring);
	strcat(m_SignalDescription, loadstring);
	strcat(m_SignalDescription, "\n</Signal>");

  
	// Free up memory for modes
	delete [] modes;

	if(Fnc > 100)
	{
		//If trigger function, do not invoke wrapper
	}
	else 
	{
	// Check MaxTime Modifier
		if(m_MaxTime.Exists)
		{
			MaxTime = m_MaxTime.Real;
			IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Setup", m_DeviceName, MaxTime, m_SignalDescription, NULL, 0)));
		}
		else
		{
			IFNSIM((Status = cs_IssueAtmlSignal("Setup", m_DeviceName, m_SignalDescription, NULL, 0)));
		}
		if (strlen(m_SignalDescription) < 1000)
			CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", m_SignalDescription, Status));
		else
			CEMDEBUG(9,cs_FmtMsg("IssueSignal [Setup] %d", Status));

	}


    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateArb_Gen(int Fnc)
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_Gen_T::InitiateArb_Gen(int Fnc)
{
    int   Status = 0;

    // Initiate action for the Arb
    CEMDEBUG(5,cs_FmtMsg("InitiateArb (%s) called FNC %d", m_DeviceName, Fnc));

     // Initiate action for the Arb
    CEMDEBUG(5,cs_FmtMsg("InitiateArb (%s) called FNC %d", m_DeviceName, Fnc));

    //IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SignalDescription,
    //                                   NULL, 0)));


    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchArb_Gen(int Fnc)
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_Gen_T::FetchArb_Gen(int Fnc)
{
    DATUM    *fdat;
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5.5;
    char    XmlValue[1024];
    char    MeasFunc[32];
    
    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
        MaxTime = m_MaxTime.Real;

    // Fetch action for the Arb
    CEMDEBUG(5,cs_FmtMsg("FetchArb (%s) called FNC %d", m_DeviceName, Fnc));

    // Fetch data
 
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
// Function: OpenArb_Gen(int Fnc)
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_Gen_T::OpenArb_Gen(int Fnc)
{
	int Status = 0;
    // Open action for the Arb
    CEMDEBUG(5,cs_FmtMsg("OpenArb (%s) called FNC %d", m_DeviceName, Fnc));

//	 IFNSIM((Status = cs_IssueAtmlSignal("Open", m_DeviceName, m_SignalDescription,
 //                                      NULL, 0)));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseArb_Gen(int Fnc)
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_Gen_T::CloseArb_Gen(int Fnc)
{
	int Status = 0;
    // Close action for the Arb
    CEMDEBUG(5,cs_FmtMsg("CloseArb (%s) called FNC %d", m_DeviceName, Fnc));

	if (Fnc > 100)
	{
		//Trigger handling
	}
	else 
	{
	   IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SignalDescription, NULL, 0)));
	}
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetArb_Gen(int Fnc)
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_Gen_T::ResetArb_Gen(int Fnc)
{
    int   Status = 0;

    // Reset action for the Arb
    CEMDEBUG(5,cs_FmtMsg("ResetArb (%s) called FNC %d", m_DeviceName, Fnc));

	// do not reset events
	if (Fnc > 100)
		return 0;

	if(Fnc != 0)
    {
        // Reset the Arb
         IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, m_SignalDescription, NULL, 0)));
    }

    InitPrivateArb_Gen();

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoArb_Gen(int Fnc)
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_Gen_T::GetStmtInfoArb_Gen(int Fnc)
{
	DATUM    *datum;
	DATUM    *datumMin;
	DATUM    *datumMax;
	double fWaveformMax = 0.0;
	double tempValue = 0.0;
    double fWaveformMin = 0.0;
	double * StimArray = NULL;
	int i = 0;	
    char *s_tring=NULL;
	int EventFunction = 0;
	EventStructure	tempEventStructure;
	int				nEventIndex;
    char			*pChar;

	this->InitEventStructure(&tempEventStructure);

	if(Fnc > 100)
	{
		if (datum = RetrieveDatum(M_EVOU, K_SET))
		{
			// Get event number
			this->m_vEvents.push_back(tempEventStructure);
			nEventIndex = this->m_vEvents.size() - 1;
			this->m_vEvents[nEventIndex].EventNum = INTDatVal(datum, 0);

				//event-delay M_EVDL
			if(	(datum = RetrieveDatum(M_EVDL, K_SET)) ||
				(datum = RetrieveDatum(M_EVDL, K_SRX)) ||
				(datum = RetrieveDatum(M_EVDL, K_SRN)) )		
			{
				this->m_vEvents[nEventIndex].EventDelay.Exists = true;
				this->m_vEvents[nEventIndex].EventDelay.Real = DECDatVal(datum, 0);

				FreeDatum(datum);
			}

			//// EVENT-SLOPE
			if(datum = GetDatum(M_EVSL, K_SET))
			{
				if(datum != NULL)
				{
					pChar = GetTXTDatVal(datum, 0);
					if(pChar != NULL)
					{
						this->m_vEvents[nEventIndex].Slope.Exists = true;
						if(strcmp(pChar,"NEG") == 0)
							this->m_vEvents[nEventIndex].Slope.Dim = NEG_SLOPE;
						else
							this->m_vEvents[nEventIndex].Slope.Dim = POS_SLOPE;

						CEMDEBUG(10, cs_FmtMsg("Found EVENT-SLOPE: %s", pChar));
					}
				}
			}

			// VOLTAGE
			if(
			   (datum = GetDatum(M_VOLT, K_SET)) ||
			   (datum = GetDatum(M_VOLT, K_SRX)) ||
			   (datum = GetDatum(M_VOLT, K_SRN)) )
			{
				this->m_vEvents[nEventIndex].TrigVolt.Exists = true;
				this->m_vEvents[nEventIndex].TrigVolt.Real = DECDatVal(datum, 0);

				CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE %lf", this->m_vEvents[nEventIndex].TrigVolt.Real));
			}

			CEMDEBUG(10,cs_FmtMsg("Found EVENT-OUT (%d) for FNC %d", tempEventStructure.EventNum, Fnc));
		}
	}
	else
	{
		//voltage M_VOLT 
		if(	(datum = RetrieveDatum(M_VOLT, K_SET)) ||
			(datum = RetrieveDatum(M_VOLT, K_SRX)) ||
			(datum = RetrieveDatum(M_VOLT, K_SRN)) )
		{	

			m_Voltage.Exists = true;
			m_Voltage.Real = DECDatVal(datum, 0);
			
			// convert to v-peak for 1641
			switch(Fnc)
			{
				case FNC_AC_SIGNAL:
					m_Voltage.Real = m_Voltage.Real * SQRT_2;
					break;
				case FNC_TRIANGULAR_WAVE:
					m_Voltage.Real = m_Voltage.Real * SQRT_3;
					break;
				case FNC_WAVEFORM:
				case FNC_DC_SIGNAL:
				case FNC_RAMP_SIGNAL:
				default:
					break;

			}// end switch	

			CEMDEBUG(10,cs_FmtMsg("Found Volt %lf",m_Voltage.Real));
			
			FreeDatum(datum);
		}  	

	  // VOLTAGE-P
		if( (datum = RetrieveDatum(M_VLPK, K_SET)) ||
			(datum = RetrieveDatum(M_VLPK, K_SRX)) ||
			(datum = RetrieveDatum(M_VLPK, K_SRN)) )
		{
			m_Voltage.Exists = true;
			m_Voltage.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE-P: %lf",m_Voltage.Real));

			FreeDatum(datum);
		}
			
	   // VOLTAGE-PP
		if( (datum = RetrieveDatum(M_VLPP, K_SET)) ||
			(datum = RetrieveDatum(M_VLPP, K_SRX)) ||
			(datum = RetrieveDatum(M_VLPP, K_SRN)) )
		{
			m_Voltage.Exists = true;
			m_Voltage.Real = DECDatVal(datum, 0);
			
			// convert to v-peak for 1641
			switch(Fnc)
			{
				case FNC_AC_SIGNAL:
				case FNC_TRIANGULAR_WAVE:
				case FNC_SQUARE_WAVE:
					m_Voltage.Real = m_Voltage.Real / 2;
					break;
				case FNC_WAVEFORM:
				case FNC_DC_SIGNAL:
				case FNC_RAMP_SIGNAL:
				default:
					break;
			}// end switch

			CEMDEBUG(10,cs_FmtMsg("Found VOLTAGE-P-P: %lf",m_Voltage.Real));

			FreeDatum(datum);
		}

		// DC Offset	
		if( (datum = RetrieveDatum(M_DCOF, K_SET)) ||
			(datum = RetrieveDatum(M_DCOF, K_SRX)) ||
			(datum = RetrieveDatum(M_DCOF, K_SRN)) )
		{
			m_DCOffset.Exists = true;
			m_DCOffset.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found DC-Offset: %lf",m_DCOffset.Real));

			FreeDatum(datum);
		}

		// Age-Rate
		if(
			(datum = RetrieveDatum(M_AGER, K_SET)) ||
			(datum = RetrieveDatum(M_AGER, K_SRX)) ||
			(datum = RetrieveDatum(M_AGER, K_SRN)) )
		{
			m_AgeRate.Exists = true;
			m_AgeRate.Int = (int)INTDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Age-Rate: %d",m_AgeRate.Int));

			FreeDatum(datum);
		}

		// Bandwidth
		if( (datum = RetrieveDatum(M_BAND, K_SET)) ||
			(datum = RetrieveDatum(M_BAND, K_SRX)) ||
			(datum = RetrieveDatum(M_BAND, K_SRN)) )
		{
			m_Bandwidth.Exists = true;
			m_Bandwidth.Real = (int)DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Bandwidth: %lf",m_Bandwidth.Real));

			FreeDatum(datum);
		}

		// Frequency - seperate out to us for a sweep if there is one
		if(datum = RetrieveDatum(M_FREQ, K_SET))         
		{
			m_Frequency.Exists = true;
			m_Frequency.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Frequency: %lf",m_Frequency.Real));

			FreeDatum(datum);
		}

		// Period
		if(	(datum = RetrieveDatum(M_PERI, K_SET)) ||
			(datum = RetrieveDatum(M_PERI, K_SRX)) ||
			(datum = RetrieveDatum(M_PERI, K_SRN)) )
		{
			// since all waveforms to the ARB takes freq, translate to freq
			m_Period.Exists = true;
			m_Period.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found period: %lf",m_Frequency.Real));

			FreeDatum(datum);
		}
	  
		// Frequency Window
		if(
			(datumMin = GetDatum(M_FRQW, K_SRN)) &&
			(datumMax = GetDatum(M_FRQW, K_SRX)))
		{
			this->m_FreqWindow.Exists = true;
			this->m_FreqWindowMin.Exists = true;
			this->m_FreqWindowMin.Real = DECDatVal(datumMin, 0);
			this->m_FreqWindowMax.Exists = true;
			this->m_FreqWindowMax.Real = DECDatVal(datumMax, 0);

			CEMDEBUG(10,cs_FmtMsg("Found FREQ-WINDOW %lf TO %lf",this->m_FreqWindowMin.Real, m_FreqWindowMax.Real));
		}
		
		// Sample-Spacing
		if(	(datum = RetrieveDatum(M_SASP, K_SET)) ||
			(datum = RetrieveDatum(M_SASP, K_SRX)) ||
			(datum = RetrieveDatum(M_SASP, K_SRN)) )
		{		
			m_SampleSpacing.Exists = true;
			m_SampleSpacing.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Sample-Spacing: %lf",m_SampleSpacing.Real));

			FreeDatum(datum);
		}

		//BURST
		if(	(datum = RetrieveDatum(M_BURS, K_SET)) ||
			(datum = RetrieveDatum(M_BURS, K_SRX)) ||
			(datum = RetrieveDatum(M_BURS, K_SRN)) )
		{
			m_Burst.Exists = true;
			m_Burst.Int = (int)DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Burst: %d",m_Burst.Int));

			FreeDatum(datum);
		}	

		//BURST-REP-RATE
		if(	(datum = RetrieveDatum(M_BURR, K_SET)) ||
			(datum = RetrieveDatum(M_BURR, K_SRX)) ||
			(datum = RetrieveDatum(M_BURR, K_SRN)) )
		{
			m_BurstRepRate.Exists = true;
			m_BurstRepRate.Int = (int)DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Burst-Rep-Rate: %d",m_BurstRepRate.Int));

			FreeDatum(datum);
		}	

		// Age Rate
		if(	(datum = RetrieveDatum(M_AGER, K_SET)) ||
			(datum = RetrieveDatum(M_AGER, K_SRX)) ||
			(datum = RetrieveDatum(M_AGER, K_SRN)) )
		{    
			m_AgeRate.Exists = true;
			m_AgeRate.Int = (int)INTDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Age-Rate: %d",m_AgeRate.Int));

			FreeDatum(datum);
		}

		// Test Equipment Imp
		if(	(datum = RetrieveDatum(M_TIMP, K_SET)) ||
			(datum = RetrieveDatum(M_TIMP, K_SRX)) ||
			(datum = RetrieveDatum(M_TIMP, K_SRN)) )
		{
			m_TestEquipImp.Exists = true;
			m_TestEquipImp.Int = (int)DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Test-Equip-Imp: %d",m_TestEquipImp.Int));

			FreeDatum(datum);
		}
		else
		{	m_TestEquipImp.Exists = true;
			m_TestEquipImp.Int   = 1000000;
		}

		//stim M_STIM
		if((datum = RetrieveDatum(M_STIM, K_SET)))
		{
			m_Stim.Exists = true;
			m_Stim.Int = DatCnt (datum);   //get # of vars
			CEMDEBUG(10, cs_FmtMsg("Found %d STIM values", m_Stim.Int));
			CEMDEBUG(10, cs_FmtMsg("STIM data size %d", DatSiz(datum)));
	        
			for (int i = 0; i < m_Stim.Int; i++)
			{
				m_StimData[i]=DECDatVal(datum,i);
			}
			FreeDatum(datum);
		}
		//////////////////////////////////////////////////////////
		//					common mods

		// MAX-TIME
		if( (datum = RetrieveDatum(M_MAXT, K_SET)) ||
			(datum = RetrieveDatum(M_MAXT, K_SRX)) ||
			(datum = RetrieveDatum(M_MAXT, K_SRN)) )
		{
			m_MaxTime.Exists = true;
			m_MaxTime.Real = DECDatVal(datum, 0);
			CEMDEBUG(10,cs_FmtMsg("Found Max-Time %lf",m_MaxTime.Real));
			FreeDatum(datum);
		}

		// Event Sync
		if( datum = RetrieveDatum(M_SYNC, K_SET))
		{
			m_EventSync.Exists = true;
			this->m_EventSync.Int = this->FindEventIndex(INTDatVal(datum, 0));
			CEMDEBUG(10,cs_FmtMsg("Found Event-Sync"));

			FreeDatum(datum);
		}

		// Gated by
		if( datum = RetrieveDatum(M_EVGB, K_SET))
		{
			m_EventGatedBy.Exists = true;
			this->m_EventGatedBy.Int = this->FindEventIndex(INTDatVal(datum, 0));
			CEMDEBUG(10,cs_FmtMsg("Found Gated-By"));

			FreeDatum(datum);
		}
		
	}

    //////////////////////////////////////////////////////////
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateArb_Gen()
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
void CArb_Gen_T::InitPrivateArb_Gen(void)
{
	m_AgeRate.Exists = false;
	m_AgeRate.Dim = 0;
	m_AgeRate.Int = 0;
	m_AgeRate.Real = 0.0;

	m_Bandwidth.Exists = false;
	m_Bandwidth.Int = 0;
	m_Bandwidth.Dim = 0;
	m_Bandwidth.Real = 0.0;

	m_Burst.Exists = false;
	m_Burst.Int = 0;
	m_Burst.Dim = 0;
	m_Burst.Real = 0.0;
	m_BurstRepRate.Exists = false;

	m_DCOffset.Exists = false;
	m_DCOffset.Int = 0;
	m_DCOffset.Dim = 0;
	m_DCOffset.Real = 0.0;

	m_Frequency.Exists = false;
	m_Frequency.Int = 0;
	m_Frequency.Dim = 0;
	m_Frequency.Real = 0.0;

    m_FreqWindow.Exists = false;
	m_FreqWindow.Int = 0;
	m_FreqWindow.Dim = 0;
	m_FreqWindow.Real = 0.0;
	
    m_FreqWindowMax.Exists = false;
	m_FreqWindowMax.Int = 0;
	m_FreqWindowMax.Dim = 0;
	m_FreqWindowMax.Real = 0.0;
	
    m_FreqWindowMin.Exists = false;
	m_FreqWindowMin.Int = 0;
	m_FreqWindowMin.Dim = 0;
	m_FreqWindowMin.Real = 0.0;
	
	m_EventDelay.Exists = false;
	m_EventDelay.Int = 0;
	m_EventDelay.Dim = 0;
	m_EventDelay.Real = 0.0;
	
	m_EventSlope.Exists = false;
	m_EventSlope.Int = 0;
	m_EventSlope.Dim = 0;
	m_EventSlope.Real = 0.0;

	m_EventGatedBy.Exists = false;
	m_EventGatedBy.Int = 0;
	m_EventGatedBy.Dim = 0;
	m_EventGatedBy.Real = 0.0;

	m_EventOut.Exists = false;
	m_EventOut.Int = 0;
	m_EventOut.Dim = 0;
	m_EventOut.Real = 0.0;

	m_EventSync.Exists = false;
	m_EventSync.Int = 0;
	m_EventSync.Dim = 0;
	m_EventSync.Real = 0.0;
	
	m_Frequency.Exists = false;
	m_Frequency.Int = 0;
	m_Frequency.Dim = 0;
	m_Frequency.Real = 0.0;

	m_MaxTime.Exists = false;
	m_MaxTime.Int = 0;
	m_MaxTime.Dim = 0;
	m_MaxTime.Real = 0.0;

	m_Period.Exists = false;
	m_Period.Int = 0;
	m_Period.Dim = 0;
	m_Period.Real = 0.0;
	
	m_SampleSpacing.Exists = false;
	m_SampleSpacing.Int = 0;
	m_SampleSpacing.Dim = 0;
	m_SampleSpacing.Real = 0.0;
   
	m_Stim.Exists=false;
	m_Stim.Dim = 0;
	m_Stim.Int = 0;
	m_Stim.Real = 0.0;

	m_TestEquipImp.Exists = false;
	m_TestEquipImp.Int = 0;
	m_TestEquipImp.Dim = 0;
	m_TestEquipImp.Real = 0.0;

	m_Voltage.Exists = false;	
	m_Voltage.Int = 0;
	m_Voltage.Dim = 0;
	m_Voltage.Real = 0.0;

	if (m_SignalDescription != NULL)
	{
		delete [] m_SignalDescription;
		m_SignalDescription = NULL;
	}


    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateArb_Gen()
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
void CArb_Gen_T::NullCalDataArb_Gen(void)
{
    //////// Place Arb specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE */
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
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
	for(long i=0; i<size; i++)
	{
		sprintf(temp,"%g", array[i]);
		rm_white(&temp[0]);
		if(i!=size-1)
			strcat(temp,", ");
		strcat(output,temp);
		temp[0]='\0';
	}
    return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: string_to_array()
//
// Purpose: Converts an string to an array
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// number             int           converts the integer to a binary number
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            char *          Null terminated string in 1641 format
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
int convertBinary(int number, double array [])
{
	int result = 0;
	
	int i = 0;
	int base = 2;	// base of 2 for binary
	int temp;
	temp = (sizeof(number) * 2) + 1; // should be 9
	
	
	if( (number > 0) && (number < 255 ))
	{
		for(i = 0; i < temp; i++)
		{ 	
				//store in the most significant bit 
				array[i] = (number % base); 
				number /= base;
				
		}
		
	}
	return(i-1);
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

void CArb_Gen_T::InitEventStructure(EventStructure * pStructure)
{
	pStructure->Fnc = 0;
	pStructure->EventNum = 0;
	strcpy(pStructure->Source, "");
	InitModStructure(&pStructure->EventDelay);
	InitModStructure(&pStructure->Slope);
	InitModStructure(&pStructure->Coupling);
	InitModStructure(&pStructure->Freq);
	InitModStructure(&pStructure->MaxTime);
	InitModStructure(&pStructure->SettleTime);
	InitModStructure(&pStructure->TestEquipImp);
	InitModStructure(&pStructure->TrigVolt);
	InitModStructure(&pStructure->VoltMax);
}

void CArb_Gen_T::InitModStructure(ModStruct * pStructure)
{
	pStructure->Exists = false;
	pStructure->Int = 0;
	pStructure->Real = 0.0;
	pStructure->Dim = FNC_UNDEFINED;
}

int CArb_Gen_T::FindEventIndex(int nEventNumber)
{
    int nEventIndex = 0;

    for(nEventIndex = 0; nEventIndex < (int)this->m_vEvents.size(); nEventIndex++)
	{
		if(this->m_vEvents[nEventIndex].EventNum == nEventNumber)
		{
			CEMDEBUG(10, cs_FmtMsg("Event at index %d corresponds to event number %d", nEventIndex, nEventNumber));        
			return(nEventIndex);
		}
	}

    // Can't find an event in event vector that contains the event number
    CEMDEBUG(0, cs_FmtMsg("Can't find event number %d in event vector", nEventNumber));
    return(-1);
}
