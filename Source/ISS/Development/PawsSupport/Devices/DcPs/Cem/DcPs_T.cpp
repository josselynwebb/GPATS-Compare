//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    DcPs_T.cpp
//
// Date:	    03-Jan-05
//
// Purpose:	    Instrument Driver for DcPs
//
// Instrument:	DcPs  DC Power Supplies for GPATS (DCPS)
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
//////// Place DcPs specific data here //////////////

//   1  source   dc signal
//   2  source   dc signal
//   3  source   dc signal
//   4  source   dc signal
//   5  source   dc signal
//   6  source   dc signal
//   7  source   dc signal
//   8  source   dc signal
//   9  source   dc signal
//  10  source   dc signal

//  12  source   dc signal
//  13  source   dc signal
//  14  source   dc signal
//  15  source   dc signal
//  16  source   dc signal
//  17  source   dc signal
//  18  source   dc signal
//  19  source   dc signal

//  23  source   dc signal
//  24  source   dc signal
//  25  source   dc signal
//  26  source   dc signal
//  27  source   dc signal
//  28  source   dc signal
//  29  source   dc signal

//  34  source   dc signal
//  35  source   dc signal
//  36  source   dc signal
//  37  source   dc signal
//  38  source   dc signal
//  39  source   dc signal

//  45  source   dc signal
//  46  source   dc signal
//  47  source   dc signal
//  48  source   dc signal
//  49  source   dc signal
//
//  56  source   dc signal
//  57  source   dc signal
//  58  source   dc signal
//  59  source   dc signal

//  67  source   dc signal
//  68  source   dc signal
//  69  source   dc signal

//  78  source   dc signal
//  79  source   dc signal

//  89  source   dc signal

// 210  sensor (voltage) dc signal
// 211  sensor (current) dc signal
// 212  sensor (voltage) dc signal
// 213  sensor (current) dc signal
// 214  sensor (voltage) dc signal
// 215  sensor (current) dc signal
// 216  sensor (voltage) dc signal
// 217  sensor (current) dc signal
// 218  sensor (voltage) dc signal
// 219  sensor (current) dc signal
// 220  sensor (voltage) dc signal
// 221  sensor (current) dc signal
// 222  sensor (voltage) dc signal
// 223  sensor (current) dc signal
// 224  sensor (voltage) dc signal
// 225  sensor (current) dc signal
// 226  sensor (voltage) dc signal
// 227  sensor (current) dc signal
// 228  sensor (voltage) dc signal
// 229  sensor (current) dc signal

// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  03JAN06  Baseline                           D.N. Thiakos-EADS
// 1.3.0.1  01May07  Added code in Fetch to force into  M.Hendricksen, EADS
//                      into SetupDcPs() again to capture the
//                      the measurment attribute and send Wrapper
//                      a "Fetch" statment from Fetch() instead of "Setup".
//                      This accomodates single action verbs like READ.
//                  Corrected code in Reset and Open to not base the reset
//                      on if a measurment characteric is present.
// 1.3.0.2 01May07	Corrected code in Reset() and Open() to		M.Hendricksen, EADS
//						accomodate both single/multi action verbs.
// 1.4.0.1 03Jul07	Corrected code to prevent incorrect reading		D.Bubenik, EADS
//						of voltage and current datums. PCR 166
// 1.4.1.0 30Jul09  Add code to implement CHANGE verb for parallel  EADS
//                      supplies
///////////////////////////////////////////////////////////////////////////////
// Includes

#include <float.h>
#include <math.h>
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "DcPs_T.h"

// Local Defines

// Function codes

//////// Place DcPs specific data here //////////////
#define FNC_PS1_DC     1
#define FNC_PS2_DC     2
#define FNC_PS3_DC     3
#define FNC_PS4_DC     4
#define FNC_PS5_DC     5
#define FNC_PS6_DC     6
#define FNC_PS7_DC     7
#define FNC_PS8_DC     8
#define FNC_PS9_DC     9
#define FNC_PS10_DC    10

#define FNC_MS12_DC			12
#define FNC_MS123_DC		13
#define FNC_MS1234_DC		14
#define FNC_MS12345_DC		15
#define FNC_MS123456_DC     16
#define FNC_MS1234567_DC    17
#define FNC_MS12345678_DC   18
#define FNC_MS123456789_DC  19

#define FNC_MS23_DC			23
#define FNC_MS234_DC		24
#define FNC_MS2345_DC		25
#define FNC_MS23456_DC		26
#define FNC_MS234567_DC		27
#define FNC_MS2345678_DC	28
#define FNC_MS23456789_DC	29

#define FNC_MS34_DC			34
#define FNC_MS345_DC		35
#define FNC_MS3456_DC		36
#define FNC_MS34567_DC		37
#define FNC_MS345678_DC		38
#define FNC_MS3456789_DC	39

#define FNC_MS45_DC			45
#define FNC_MS456_DC		46
#define FNC_MS4567_DC		47
#define FNC_MS45678_DC		48
#define FNC_MS456789_DC		49

#define FNC_MS56_DC			56
#define FNC_MS567_DC		57
#define FNC_MS5678_DC		58
#define FNC_MS56789_DC		59

#define FNC_MS67_DC			67
#define FNC_MS678_DC		68
#define FNC_MS6789_DC		69

#define FNC_MS78_DC			78
#define FNC_MS789_DC		79

#define FNC_MS89_DC			89


#define FNC_PS1_VOLT_DC    210
#define FNC_PS1_CURR_DC    211
#define FNC_PS2_VOLT_DC    212
#define FNC_PS2_CURR_DC    213
#define FNC_PS3_VOLT_DC    214
#define FNC_PS3_CURR_DC    215
#define FNC_PS4_VOLT_DC    216
#define FNC_PS4_CURR_DC    217
#define FNC_PS5_VOLT_DC    218
#define FNC_PS5_CURR_DC    219
#define FNC_PS6_VOLT_DC    220
#define FNC_PS6_CURR_DC    221
#define FNC_PS7_VOLT_DC    222
#define FNC_PS7_CURR_DC    223
#define FNC_PS8_VOLT_DC    224
#define FNC_PS8_CURR_DC    225
#define FNC_PS9_VOLT_DC    226
#define FNC_PS9_CURR_DC    227
#define FNC_PS10_VOLT_DC   228
#define FNC_PS10_CURR_DC   229

//////////////////////////////////////////////////////////

#define CAL_TIME       (86400 * 365) /* one year */
#define FNC_RB_MIN		210

typedef struct FncResourceStruct
{
	int    Fnc;
	char   ResourceName[MAX_BC_DEV_NAME];
	double DefVolt;
	double DefCurr;
	double MaxCurr;  //for value check in GetStatementInfo
	double MaxVolt;
} CS_RESINFO;

// Static Variables
static bool s_FetchCalled;
static int s_MeasValue = 0;
static int s_NumRes = 0;
static int s_ChanIndex = 0; //1-46; the 0 index is not used
static double s_MaxCurr = 0.0;
static double s_MaxVolt = 0.0;
static CS_RESINFO s_ResInfo[MAX_DEVICES] =
{  { 1, "DCPS_1", 10, 1, 5.0, 40.0},   // 33 V supplies 1-8
   { 2, "DCPS_2", 10, 1, 5.0, 40.0},
   { 3, "DCPS_3", 10, 1, 5.0, 40.0},
   { 4, "DCPS_4", 10, 1, 5.0, 40.0},
   { 5, "DCPS_5", 10, 1, 5.0, 40.0},
   { 6, "DCPS_6", 10, 1, 5.0, 40.0},
   { 7, "DCPS_7", 10, 1, 5.0, 40.0},
   { 8, "DCPS_8", 10, 1, 5.0, 40.0},
   { 9, "DCPS_9", 10, 1, 5.0, 40.0},   // 16 V supplies 9,10
   { 10, "DCPS_10", 10, 1, 5.0, 65.0},
   { 12, "DCPS_PAR12", 10, 6, 10.0, 40.0 },
   { 13, "DCPS_PAR123", 10, 11, 15.0, 40.0 },
   { 14, "DCPS_PAR1234", 10, 16, 20.0, 40.0 },
   { 15, "DCPS_PAR12345", 10, 21, 25.0, 40.0 },
	{ 16, "DCPS_PAR123456", 10, 26, 30.0, 40.0 },
	{ 17, "DCPS_PAR1234567", 10, 31, 35.0, 40.0 },
	{ 18, "DCPS_PAR12345678", 10, 36, 40.0, 40.0 },
	{ 19, "DCPS_PAR123456789", 10, 41, 45.0, 40.0 },
	{ 23, "DCPS_PAR23", 10, 6, 10.0, 40.0 },
	{ 24, "DCPS_PAR234", 10, 11, 15.0, 40.0 },
	{ 25, "DCPS_PAR2345", 10, 16, 20.0, 40.0},
	{ 26, "DCPS_PAR23456", 10, 21, 25.0, 40.0 },
	{ 27, "DCPS_PAR234567", 10, 26, 30.0, 40.0 },
	{ 28, "DCPS_PAR2345678", 10, 31, 35.0, 40.0 },
	{ 29, "DCPS_PAR23456789", 10, 36, 40.0, 40.0 },
	{ 34, "DCPS_PAR34", 10, 6, 10.0, 40.0 },
	{ 35, "DCPS_PAR345", 10, 11, 15.0, 40.0 },
	{ 36, "DCPS_PAR3456", 10, 16, 20.0, 40.0 },
	{ 37, "DCPS_PAR34567", 10, 21, 25.0, 40.0 },
	{ 38, "DCPS_PAR345678", 10, 26, 30.0, 40.0 },
	{ 39, "DCPS_PAR3456789", 10, 31, 35.0, 40.0 },
	{ 45, "DCPS_PAR45", 10, 6, 10.0, 40.0 },
	{ 46, "DCPS_PAR456", 10, 11, 15.0, 40.0 },
	{ 47, "DCPS_PAR4567", 10, 16, 20.0, 40.0 },
	{ 48, "DCPS_PAR45678", 10, 21, 25.0, 40.0 },
	{ 49, "DCPS_PAR456789", 10, 26, 30.0, 40.0 },
	{ 56, "DCPS_PAR56", 10, 6, 10.0, 40.0 },
	{ 57, "DCPS_PAR567", 10, 11, 15.0, 40.0 },
	{ 58, "DCPS_PAR5678", 10, 16, 20.0, 40.0 },
	{ 59, "DCPS_PAR56789", 10, 21, 25.0, 40.0 },
	{ 67, "DCPS_PAR67", 10, 6, 10.0, 40.0 },
	{ 68, "DCPS_PAR678", 10, 11, 15.0, 40.0 },
	{ 69, "DCPS_PAR6789", 10, 16, 20.0, 40.0 },
	{ 78, "DCPS_PAR78", 10, 6, 10.0, 40.0 },
	{ 79, "DCPS_PAR789", 10, 11, 15.0, 40.0 },
	{ 89, "DCPS_PAR89", 10, 6, 10.0, 40.0 },
	{  -1, "", 0.0, 0.0 }
};
// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CDcPs_T(int Bus, int PrimaryAdr, int SecondaryAdr,
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
CDcPs_T::CDcPs_T(char *DeviceName, int Bus, int PrimaryAdr, int SecondaryAdr, int Dbg, int Sim)
{
    m_Bus = Bus;
    m_PrimaryAdr = PrimaryAdr;
    m_SecondaryAdr = SecondaryAdr;
    m_Dbg = Dbg;
    m_Sim = Sim;
    m_Handle= NULL;
    int Status = 0;
	int ChanIndex, i = 0;	

	m_SignalDescription[0] = '\0';
	m_VirtResourceName[0] = '\0';

    if(DeviceName)
	{
		strcpy(m_DeviceName,DeviceName);
	}

	for(int j = 0; j < MAX_DEVICES; j++)
	{
		m_ResourceName[j][0] = '\0';
	}

    for (ChanIndex = 1; ChanIndex < MAX_DEVICES; ChanIndex++) 
	{
		InitPrivateDcPs(ChanIndex); 
	}

	NullCalDataDcPs();

	while(s_ResInfo[i].Fnc != -1) 
	{
		i++;
	}
	s_NumRes = i;

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("DcPs Class called with Device [%s], "
                        "Sim %d, Dbg %d", 
                          m_DeviceName, Sim, Dbg));

    // Initialize the DcPs - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(m_DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);
	
	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CDcPs_T()
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
CDcPs_T::~CDcPs_T()
{
    // Reset the DcPs
    CEMDEBUG(5,cs_FmtMsg("DcPs Class Distructor called for Device [%s], ", m_DeviceName));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusDcPs(int Fnc)
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
int CDcPs_T::StatusDcPs(int Fnc)
{
    int Status = 0;
	
    // Status action for the DcPs
    CEMDEBUG(5,cs_FmtMsg("StatusDcPs (%s) called FNC %d", m_DeviceName, Fnc));
    // Check for any pending error messages
	GetChanIdxDcPs(Fnc);

	if (!m_Measure[s_ChanIndex].Exists)  // 4/17
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Status", m_ResourceName[s_ChanIndex], m_SignalDescription, NULL, 0)));

	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupDcPs_T(int Fnc)
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
int CDcPs_T::SetupDcPs(int Fnc)
{
    int  Status = 0, tempFnc;
	char stringinput[512]="",
		 acoutstring[512]="",
		 dcoutstring[512]="",
		 sumstring[512]="",
		 measurestring[512]="",
		 signalstring[512]="",
		 string[512]="",
		 limitstring[512]="",
		 resourcestring[35]="",
		 Response[4096],
		 action[8] = "Setup";
	double  dV = 0.0, dI = 0.0, dVLmt, dILmt;
	double  MaxTime = 10;

    //////////////////////////////////////////////////////////

    // Setup action for the DcPs
    CEMDEBUG(5, cs_FmtMsg("SetupDcPs (%s) called FNC %d", m_DeviceName, Fnc));
	
	GetChanIdxDcPs(Fnc);

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
	{
		return(0);
	}

    if((Status = GetStmtInfoDcPs(Fnc)) != 0)
	{
		return(0);
	}
    
	//get resource name and default limits from table
	if (Fnc >= 210)
	{

		if (Fnc % 2 == 0)
		{
			tempFnc = (Fnc - 209) - (Fnc - FNC_RB_MIN)/2;
		}
		else if (Fnc%2 == 1)
		{
			tempFnc = (Fnc - 209) - (int)ceil((double)(Fnc-FNC_RB_MIN)/2);
		}
	}
	else
	{
		tempFnc = Fnc;
	}

	for (int i = 0; i < s_NumRes; i++)
	{
		if (s_ResInfo[i].Fnc == tempFnc)
		{
			strcpy(resourcestring, s_ResInfo[i].ResourceName);
			dVLmt = s_ResInfo[i].DefVolt;
			dILmt = s_ResInfo[i].DefCurr;
			break;
		}
	}

	
	
	
	if(m_MaxTime[s_ChanIndex].Exists)
	{
		MaxTime = m_MaxTime[s_ChanIndex].Real;
	}

    switch(Fnc)
    {
		case FNC_PS1_DC: //    source
		case FNC_PS2_DC:    
		case FNC_PS3_DC:    
		case FNC_PS4_DC:    
		case FNC_PS5_DC:    
		case FNC_PS6_DC:    
		case FNC_PS7_DC:    
		case FNC_PS8_DC:    
		case FNC_PS9_DC:    
		case FNC_PS10_DC:
		case FNC_MS12_DC:			
		case FNC_MS123_DC:		
		case FNC_MS1234_DC:		
        case FNC_MS12345_DC:	
        case FNC_MS123456_DC:   
        case FNC_MS1234567_DC:   
        case FNC_MS12345678_DC:  
        case FNC_MS123456789_DC: 

        case FNC_MS23_DC:
        case FNC_MS234_DC:
        case FNC_MS2345_DC:
        case FNC_MS23456_DC:		
        case FNC_MS234567_DC:		
        case FNC_MS2345678_DC:	
        case FNC_MS23456789_DC:	

        case FNC_MS34_DC:			
        case FNC_MS345_DC:		
        case FNC_MS3456_DC:		
        case FNC_MS34567_DC:		
        case FNC_MS345678_DC:		
        case FNC_MS3456789_DC:	

        case FNC_MS45_DC:			
        case FNC_MS456_DC:		
        case FNC_MS4567_DC:		
        case FNC_MS45678_DC:		
        case FNC_MS456789_DC:		

        case FNC_MS56_DC:			
        case FNC_MS567_DC:		
        case FNC_MS5678_DC:		
        case FNC_MS56789_DC:		

        case FNC_MS67_DC:			
        case FNC_MS678_DC:		
        case FNC_MS6789_DC:		

        case FNC_MS78_DC:			
        case FNC_MS789_DC:		

        case FNC_MS89_DC:	

			m_Measure[s_ChanIndex].Exists=false;
            sprintf(signalstring, "<Signal name=\"\" Out=\"DCPS_DC_SIGNAL\">\n");
			sprintf(dcoutstring, "<Constant name=\"DC_Level\"");
			if(m_Voltage[s_ChanIndex].Exists)
			{
				sprintf(string, " amplitude=\"%3.3lf V\" ", m_Voltage[s_ChanIndex].Real);
                strcat(dcoutstring, string);
			}
			if(m_Current[s_ChanIndex].Exists)
			{
				sprintf(string, " amplitude=\"%3.3lf A\" ", m_Current[s_ChanIndex].Real);
                strcat(dcoutstring, string);
			}
            sprintf(string, "/>\n");
            strcat(dcoutstring, string);

			if(m_VoltLimit[s_ChanIndex].Exists)
			{
				sprintf(limitstring, "<Limit name=\"DCPS_DC_SIGNAL\" limit=\"%3.3lf V\" In=\"DC_Level\" />", m_VoltLimit[s_ChanIndex].Real);
			}

			if(m_CurrentLimit[s_ChanIndex].Exists)
			{
				sprintf(limitstring, "<Limit name=\"DCPS_DC_SIGNAL\" limit=\"%3.3lf A\" In=\"DC_Level\" />", m_CurrentLimit[s_ChanIndex].Real);
			}
			strcat(signalstring, dcoutstring);
			strcat(signalstring, limitstring);
            break;
    case FNC_PS1_VOLT_DC: // (voltage)   dc signal
	case  FNC_PS2_VOLT_DC:
	case  FNC_PS3_VOLT_DC:
	case  FNC_PS4_VOLT_DC:
	case  FNC_PS5_VOLT_DC:
	case  FNC_PS6_VOLT_DC:
	case  FNC_PS7_VOLT_DC:
	case  FNC_PS8_VOLT_DC:
	case  FNC_PS9_VOLT_DC:
	case  FNC_PS10_VOLT_DC:
			m_Measure[s_ChanIndex].Exists=true;           
			sprintf(signalstring, "<Signal name=\"\" Out=\"DCPS_DC_SIGNAL\">\n");            
            sprintf(dcoutstring, "<Constant name=\"DC_Level\" />\n");			
			sprintf(measurestring, "<Measure name=\"DCPS_DC_SIGNAL\" As=\"DC_Level\" attribute=\"dc_ampl\" ");
			sprintf(string, "maxTime=\"%f sec\" ", MaxTime);
			strcat(measurestring, string);
			strcat(measurestring, "/>\n");
			strcat(signalstring, dcoutstring);
			strcat(signalstring, measurestring);
            break;
    case FNC_PS1_CURR_DC: // (current) dc signal
	case FNC_PS2_CURR_DC:
	case FNC_PS3_CURR_DC:
	case FNC_PS4_CURR_DC:
	case FNC_PS5_CURR_DC:
	case FNC_PS6_CURR_DC:
	case FNC_PS7_CURR_DC:
	case FNC_PS8_CURR_DC:
	case FNC_PS9_CURR_DC:
	case FNC_PS10_CURR_DC:
			m_Measure[s_ChanIndex].Exists = true;
			sprintf(signalstring, "<Signal name=\"\" Out=\"DCPS_DC_SIGNAL\">\n");
            sprintf(dcoutstring, "<Constant name=\"DC_Level\" />\n");
			sprintf(measurestring, "<Measure name=\"DCPS_DC_SIGNAL\" As=\"DC_Level\" attribute=\"dc_curr\" ");
			sprintf(string, "maxTime=\"%f sec\" ", MaxTime);
			strcat(measurestring, string);
			strcat(measurestring, "/>\n");
            strcat(signalstring, dcoutstring);
			strcat(signalstring, measurestring);
			break;
    }

    strcat(signalstring, "</Signal>");
	strcat(stringinput, signalstring);
			
	stringinput[511] = '\0';

	strnzcpy(m_ResourceName[s_ChanIndex], resourcestring, MAX_BC_DEV_NAME);
	strcpy(m_SignalDescription, stringinput); //save for later
	
	if ( (!m_Measure[s_ChanIndex].Exists) && (!s_FetchCalled) )  // 4/17
	{
		//////////////////////////////////////////////////
		// TEST: For parallel configurations send action
		//	     "Enable". Setup is noop in parallel 
		//       configurations, all PS programming is done
		//       in Enable. 
		//////////////////////////////////////////////////
		if ((GetCurVerb() == V_CHN) && (Fnc > 10))
		{
			strcpy(action, "Enable");
		}

		IFNSIM((Status = cs_IssueAtmlSignalMaxTime(action, m_ResourceName[s_ChanIndex], MaxTime, stringinput, Response, 4096)));
    
	    CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s, %s], Status = %d", action, stringinput, Status));
	}

    //////////////////////////////////////////////////////////
	if ((!m_Measure[s_ChanIndex].Exists) && (!s_FetchCalled) )
	{
		InitPrivateDcPs(s_ChanIndex); 
		strcpy(m_VirtResourceName, resourcestring);
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateDcPs(int Fnc)
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
int CDcPs_T::InitiateDcPs(int Fnc)
{
    int   Status = 0;

    // Initiate action for the DcPs
    CEMDEBUG(5,cs_FmtMsg("InitiateDcPs (%s) called FNC %d", m_DeviceName, Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchDcPs(int Fnc)
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
int CDcPs_T::FetchDcPs(int Fnc)
{
    DATUM    *fdat;
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5.5;
    char    Response[4096]="";
    char    MeasFnc[32];

    char	stringinput[512]="",
			measurestring[512]="",
			signalstring[512]="",
			string[512]="",
			resourcestring[35]="";

    s_FetchCalled = true;
    SetupDcPs(Fnc);

    Status = cs_IssueAtmlSignalMaxTime("Fetch", m_ResourceName[s_ChanIndex], MaxTime, m_SignalDescription, Response, 4096);
    s_FetchCalled = false;

	GetChanIdxDcPs(Fnc);

    // Fetch action for the DcPs
    CEMDEBUG(5,cs_FmtMsg("FetchDcPs (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    // Accomidate multiple MeasChars as applicable
    switch(Fnc)
    {
		case  FNC_PS1_VOLT_DC: // (voltage)   dc signal
		case  FNC_PS2_VOLT_DC:
		case  FNC_PS3_VOLT_DC:
		case  FNC_PS4_VOLT_DC:
		case  FNC_PS5_VOLT_DC:
		case  FNC_PS6_VOLT_DC:
		case  FNC_PS7_VOLT_DC:
		case  FNC_PS8_VOLT_DC:
		case  FNC_PS9_VOLT_DC:
		case  FNC_PS10_VOLT_DC:
            strcpy(MeasFnc,"dc_ampl");
            break;
		case  FNC_PS1_CURR_DC: // (ac-comp)   dc signal
		case  FNC_PS2_CURR_DC:
		case  FNC_PS3_CURR_DC:
		case  FNC_PS4_CURR_DC:
		case  FNC_PS5_CURR_DC:
		case  FNC_PS6_CURR_DC:
		case  FNC_PS7_CURR_DC:
		case  FNC_PS8_CURR_DC:
		case  FNC_PS9_CURR_DC:
		case  FNC_PS10_CURR_DC:
            strcpy(MeasFnc,"dc_curr");
            break;
    
    }

    if(Status)
    {
        MeasValue = FLT_MAX;
     }
    else
    {
        IFNSIM(cs_GetSingleDblValue(Response, MeasFnc, &MeasValue));
    }	

    fdat = FthDat();
    DECDatVal(fdat, 0) = MeasValue;
    FthCnt(1);

    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: OpenDcPs(int Fnc)
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
int CDcPs_T::OpenDcPs(int Fnc)
{
	int Status = 0;
	int F_Verb;
	

    // Open action for the DcPs
    CEMDEBUG(5,cs_FmtMsg("OpenDcPs (%s) called FNC %d", m_DeviceName, Fnc));

	GetChanIdxDcPs(Fnc);

	//grab the verb
	F_Verb = GetCurVerb();

	//for removing several resources - need to find correct resourcename
	for (int i = 0; i < s_NumRes; i++)
	{
		if (s_ResInfo[i].Fnc == Fnc)
		{
			strcpy(m_ResourceName[s_ChanIndex], s_ResInfo[i].ResourceName);
			break;
		}
	}

	//for removing a signal without it being applied
	if(strcmp(m_ResourceName[s_ChanIndex], "") == 0)
	{
		for (int i = 0; i < s_NumRes; i++)
		{
			if (i == s_ChanIndex)
			{
				strcpy(m_ResourceName[s_ChanIndex], s_ResInfo[i].ResourceName);
				break;
			}
		}
	}

    //////// Place DcPs specific data here //////////////
	//mh 5/1/07 if (!m_Measure[s_ChanIndex].Exists)  // 4/17
	if( (F_Verb != V_MEA) && (F_Verb != V_VER) )
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Disable", m_ResourceName[s_ChanIndex], m_SignalDescription, NULL, 0)));
	}
	
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseDcPs(int Fnc)
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
int CDcPs_T::CloseDcPs(int Fnc)
{
	int Status = 0;

	
	GetChanIdxDcPs(Fnc);

    // Close action for the DcPs
    CEMDEBUG(5,cs_FmtMsg("CloseDcPs (%s) called FNC %d", m_DeviceName, Fnc));

    //////// Place DcPs specific data here //////////////
	if (!m_Measure[s_ChanIndex].Exists)  // 4/17
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_ResourceName[s_ChanIndex], m_SignalDescription, NULL, 0)));
	}
		
	
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetDcPs(int Fnc)
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
int CDcPs_T::ResetDcPs(int Fnc)
{
    int   Status = 0;
	int   ChanIndex;
	int	  FVerb;

    // Reset action for the DcPs
    CEMDEBUG(5,cs_FmtMsg("ResetDcPs (%s) called FNC %d", m_DeviceName, Fnc));
    
	FVerb = GetCurVerb();

	// Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
    if(Fnc != 0)
    {
		GetChanIdxDcPs(Fnc);
		if((FVerb != V_MEA) && (FVerb != V_VER)) //(!m_Measure[s_ChanIndex].Exists)  && (FVerb != V_RD)// 4/17
        {		
			IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_ResourceName[s_ChanIndex], m_SignalDescription, NULL, 0)));
			InitPrivateDcPs(s_ChanIndex); 
        }
    }

    if(Fnc == 0)
    {
        for (ChanIndex = 1; ChanIndex < MAX_DEVICES; ChanIndex++) 
	    {
		    InitPrivateDcPs(ChanIndex); 
	    }
    }
	
    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoDcPs(int Fnc)
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
int CDcPs_T::GetStmtInfoDcPs(int Fnc)
{

    DATUM *datum;
	char Msg[MAX_MSG_SIZE];
    int Flags;
	double vlimit;
	int type = 0;

	GetChanIdxDcPs(Fnc);   

	if (Fnc==10) 
	{
		vlimit=65;
	}
	else 
	{
		vlimit=40;
	}

	if(datum = RetrieveDatum(M_MAXT,K_SET))
	{
		m_MaxTime[s_ChanIndex].Exists=TRUE;
		m_MaxTime[s_ChanIndex].Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Max Time : %lf V",m_MaxTime[s_ChanIndex].Real));
		if ((m_MaxTime[s_ChanIndex].Real<0) || (m_MaxTime[s_ChanIndex].Real > 1000))
		{
			Flags = EB_SEVERITY_ERROR ;
			sprintf(Msg, "Max-time value out of range\n");
			CEMERROR(Flags, Msg);
		}
	}

	FreeDatum(datum);

	if(datum = RetrieveDatum(M_VOLT,K_SET)) 
	{
		type = DatTyp(datum);
		if(type == DECV)
		{
			m_Voltage[s_ChanIndex].Exists=TRUE;
			m_Voltage[s_ChanIndex].Real=DECDatVal(datum,0);
			CEMDEBUG(10,cs_FmtMsg("Found Voltage : %lf V",m_Voltage[s_ChanIndex].Real));

			if ((m_Voltage[s_ChanIndex].Real < (-1)*s_MaxVolt) || (m_Voltage[s_ChanIndex].Real > s_MaxVolt))
			{
				Flags = EB_SEVERITY_ERROR ;
				sprintf(Msg, "Voltage value out of range\n");
				CEMERROR(Flags, Msg);
			}
		}
	}

	FreeDatum(datum);

	if(datum = RetrieveDatum(M_VOLT, K_SRX)) 
	{
		type = DatTyp(datum);
		if(type==DECV)
		{
			m_VoltageMax[s_ChanIndex].Exists = TRUE;
			m_VoltageMax[s_ChanIndex].Real = DECDatVal(datum,0);
			CEMDEBUG(10,cs_FmtMsg("Found Voltage Max: %lf V", m_VoltageMax[s_ChanIndex].Real));
			if ((m_VoltageMax[s_ChanIndex].Real < (-1)*s_MaxVolt)||(m_VoltageMax[s_ChanIndex].Real > s_MaxVolt))
			{
				Flags = EB_SEVERITY_ERROR ;
				sprintf(Msg, "Voltage-max value out of range\n");
				CEMERROR(Flags, Msg);
			}
		}
	}
	
	FreeDatum(datum);

	if(datum = RetrieveDatum(M_VLTL, K_SET))
	{
		m_VoltLimit[s_ChanIndex].Exists = TRUE;
		m_VoltLimit[s_ChanIndex].Real = DECDatVal(datum, 0);
    	CEMDEBUG(10,cs_FmtMsg("Found Voltage Limit: %lf V", m_VoltLimit[s_ChanIndex].Real));
		if ((m_VoltLimit[s_ChanIndex].Real < (-1)*s_MaxVolt) || (m_VoltLimit[s_ChanIndex].Real > s_MaxVolt))
		{
			Flags = EB_SEVERITY_ERROR ;
			sprintf(Msg, "Voltage-limit value out of range\n");
			CEMERROR(Flags, Msg);
		}
		if((Fnc > 10) && (Fnc <= 89))
		{
			Flags = EB_SEVERITY_ERROR ;
			sprintf(Msg, "No constant current mode for parallel configuration--\n Use VOLTAGE and CURRENT-LMT modifiers.");
			CEMERROR(Flags, Msg);
		}
	}

	FreeDatum(datum);

	if(datum = RetrieveDatum(M_CURR,K_SET))  
	{
		type = DatTyp(datum);
		if(type == DECV)
		{
			m_Current[s_ChanIndex].Exists = TRUE;
			m_Current[s_ChanIndex].Real=DECDatVal(datum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found Current : %lf A", m_Current[s_ChanIndex].Real));
			if ((m_Current[s_ChanIndex].Real < 1.0) || (m_Current[s_ChanIndex].Real > s_MaxCurr))
			{
				Flags = EB_SEVERITY_ERROR ;
				sprintf(Msg, "Current value out of range\n");
				CEMERROR(Flags, Msg);
			}
			if((Fnc > 10) && (Fnc <= 89))
			{
				Flags = EB_SEVERITY_ERROR;
				sprintf(Msg, "No constant current mode for parallel configuration--\n Use VOLTAGE and CURRENT-LMT modifiers.");
				CEMERROR(Flags, Msg);
			}
		}
	}
	FreeDatum(datum);

	if(datum = RetrieveDatum(M_CURR, K_SRX)) 
	{
		type = DatTyp(datum);
		if(type == DECV)
		{
			m_CurrentMax[s_ChanIndex].Exists=TRUE;
			m_CurrentMax[s_ChanIndex].Real=DECDatVal(datum,0);
			CEMDEBUG(10, cs_FmtMsg("Found Current Max: %lf A", m_CurrentMax[s_ChanIndex].Real));
			if ((m_CurrentMax[s_ChanIndex].Real < 0.0)||(m_CurrentMax[s_ChanIndex].Real > s_MaxCurr))
			{
				Flags = EB_SEVERITY_ERROR ;
				sprintf(Msg, "Current-max value out of range\n");
				CEMERROR(Flags, Msg);
			}
		}
	}
	FreeDatum(datum);

	if(datum = RetrieveDatum(M_CURL,K_SET))
	{
		m_CurrentLimit[s_ChanIndex].Exists = TRUE;
		m_CurrentLimit[s_ChanIndex].Real = DECDatVal(datum,0);
		CEMDEBUG(10, cs_FmtMsg("Found Current Limit : %lf A", m_CurrentLimit[s_ChanIndex].Real));
		if ((m_CurrentLimit[s_ChanIndex].Real < 0.0) || (m_CurrentLimit[s_ChanIndex].Real > s_MaxCurr))
		{
			Flags = EB_SEVERITY_ERROR ;
			sprintf(Msg, "Current-limit value out of range\n");
			CEMERROR(Flags, Msg);
		}
	}

	FreeDatum(datum);

	if (m_Current[s_ChanIndex].Exists && m_Voltage[s_ChanIndex].Exists)
	{
		Flags = EB_SEVERITY_WARNING;
		sprintf(Msg, "Need to use CURRENT-LMT or VOLT-LMT modifier.\n");
		CEMERROR(Flags, Msg);
	}
		
	if (m_CurrentLimit[s_ChanIndex].Exists && m_VoltLimit[s_ChanIndex].Exists)
	{
		Flags = EB_SEVERITY_WARNING;
		sprintf(Msg, "Need to use CURRENT or VOLTAGE modifier.\n");
		CEMERROR(Flags, Msg);

		//accomodate TPS cases
		if (m_Voltage[s_ChanIndex].Exists) 
		{
			m_VoltLimit[s_ChanIndex].Exists = false;
		}
			
		else if (m_Current[s_ChanIndex].Exists)
		{
			m_CurrentLimit[s_ChanIndex].Exists = false;
		}
		
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateDcPs()
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
void CDcPs_T::InitPrivateDcPs(int ChanIndex)
{
    //////// Place DcPs specific data here //////////////
    s_FetchCalled = false;
    m_VoltLimit[ChanIndex].Exists = false;
    m_Voltage[ChanIndex].Exists = false;
	m_VoltageMax[ChanIndex].Exists = false;
    m_Current[ChanIndex].Exists = false;
    m_CurrentLimit[ChanIndex].Exists = false;
	m_CurrentMax[ChanIndex].Exists = false;
	m_MaxTime[ChanIndex].Exists = false;
	m_Measure[ChanIndex].Exists = false;

    //////////////////////////////////////////////////////////
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataDcPs()
//
// Purpose: Initialize/Reset all private cal-data variables
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
void CDcPs_T::NullCalDataDcPs(void)
{
    //////// Place DcPs specific data here //////////////

    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;

    //////////////////////////////////////////////////////////
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: GetChanIdxDcPs(int Fnc)
//
// Purpose: Set the static s_ChanIndex based on function
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
//
///////////////////////////////////////////////////////////////////////////////
void CDcPs_T::GetChanIdxDcPs(int Fnc)
{
	int i = 0;
	
	while(s_ResInfo[i].Fnc != -1)
	{
		if (s_ResInfo[i].Fnc == Fnc)
		{
			s_ChanIndex = i+1;
			s_MaxCurr = s_ResInfo[i].MaxCurr;
			s_MaxVolt = s_ResInfo[i].MaxVolt;
			break;
		}
		i++;
	}

	return;	
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


