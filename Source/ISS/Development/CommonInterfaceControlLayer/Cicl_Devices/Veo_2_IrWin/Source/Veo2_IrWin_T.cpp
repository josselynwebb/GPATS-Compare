// 9/13/10 r. scroble, look for // dme added
// added code to pass the video H/V format for INTERNAL cameras, this is used to
//   determine Center-X and Center-Y pixel locations for returning LOS meas.
// added code to pass ROI for "diffBoresightAngleLaser" function. 
// 9/20/10 r. scroble, look for // dme added
// added conversion code for "boresightanglelaser" and "diffboresightanglelaser"
//   measurements fetch to output actual measurements from
//   video image center x,y using correct Internal Camera FOV.
//
//345678901234567890123456789012345678901234567890123456789012345678901234567890
////////////////////////////////////////////////////////////////////////////////
// File:	    Veo2_IrWin_T.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXMLW Instrument Driver for Veo2_IrWin
//
// Instrument:	Veo2_IrWin  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  =====================================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ===========================================================
// 1.0.0.0  11OCT05  Baseline Release               
// 1.0.0.1  17MAR08  Added fclose() for LARRS data file    
// 1.1.0.0  04NOV08  Phase II Release      
// 1.1.0.1  10JUL09  Deleted calls to IRWIN_SHUTDOWN. Was causing WRTS exception.
// 1.1.0.2  27JUL10  STR9676 - Hard coded Infrared MTF target to PIESECTOR.
// 1.1.0.3  28JUL10  Hard coded Visible MTF target to PIESECTOR.
// 1.1.0.4  29JUL10  Deleted function CalculateMTFRoiBlock()
// 1.1.0.5  30JUL10  Corrected algorithm for setting correction curve for MTF IR
// 1.1.0.6  03AUG10  Added debug for MTF VIS fetch array size.
// 1.1.0.7  03AUG10  Corrected code in fetch for MTF VIS.
// 1.1.0.8  09AUG10  Removed deleted functions per ref J of VEO2 API.
// 1.1.0.9  07SEP10  Added DME changes
// 1.1.0.10 07SEP10  Fixed READY_BIT condition, WaitForReady() do/while condition.
// 1.1.0.11 10SEP10  Removed calls to VEO2 lib in destructor, reorganize opening
//                   and closing of VEO2 library.
// 1.1.0.12 10SEP10  Disble remove all.
// 1.1.0.13 29SEP10  Update code for laser source per STR 9677.
// 1.1.0.14 01OCT10  Corrected reset for laser source.
// 1.1.0.15 01OCT10  Corrected parsing for waveLength attribute
// 1.1.0.16 26JAN11  Added code to process target wheel update targets:
//                      TARG-CROSS17
//                      TARG-4BAR66
//                      TARG-4BAR10
//                      TARG-SQUARE21
//                   Added programmable target to MTF IF.
//                   Added programmable target to MRF VIS.
//                   Added programmable target to UNIFORMITY VIS.
//                   Updated radiance values for min of 0.0005 uw/cm2/sr
// 1.1.0.17 31MAY11  Modified MRTD to set blackbody and target position,
//                   and then only return blackbody temp on fetch. MRTD
//                   now controlled in CEM. 
// 1.1.0.18 02JUN11  Updated ready bit to look for 0x0281 per SBIR.
// 2.0.0.0  04APR19	 Bringing to 1.3.2.0 changes
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// Integration Notes                                                          //
// ========================================================================== //
// 1. Power Down. The power for the VEO2 will be up and down multiple times   //
//    during the execution of a TPS. Prior to removing power the command      //
//    IRWIN_SHUTDOWN() needs to be executed. As power is provided by the DCPS //
//    and the VEO2 wrapper has no method for determining when the power is    //
//    applied or removed, on a reset when no signals are active the shutdown  //
//    command will be executed. On entering setup if the state is powered     //
//    down, a RESET_MODULE_INITIATE() command will be issued. This makes the  //
//    VEO2 ready to execute. The power on delay will be the responsibility of //
//    the TPS.                                                                //
// 2. IFNSIMVEO2 macro. This macro was added and looks not only for m_Sim to  //
//    be false but also for m_PowerState to be true, or POWER_ON.             //
////////////////////////////////////////////////////////////////////////////////
#pragma warning(disable : 4100)
#pragma warning(disable : 4511)
#pragma warning(disable : 4512)

// Includes
#include <stdio.h>
#include <stdarg.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "AtxmlWrapper.h"
#include "Veo2_IrWin_T.h"
#include "visa.h"
#include <string.h>
#include "Veo2_FuncPtrs.h"
#include <math.h>
#include <time.h>
#include <windows.h>

#pragma warning(default : 4511)
#pragma warning(default : 4512)
// Local Defines

#define ISDBLARRAYATTR(x,y,z) (atxmlw_Get1641DoubleArrayAttribute(m_SignalDescription, Name, x, y, z))

#define CAL_TIME       31536000		//(86400 * 365) one year */
#define MAX_MSG_SIZE    1024

#define SOURCE	1
#define SENSOR	2

#define INFRARED	1
#define LIGHT		2
#define LASER		3
#define	LARRS		4

#define FOCAL_LENGTH	656.08		// size in mm
#define PIXEL_SIZE		0.025		// size in mm

#define IDLE	0
#define ACTIVE	1

#define POWER_OFF	0
#define POWER_ON	1

#define	CAMERA_STAGE	3
#define	ENERGY_METER	2
#define	STAGE_NONE		1

#define LASER_TRIG_FREE_RUN	1
#define LASER_TRIG_LASR		2
#define LASER_TRIG_EXT		3

// Source constants for updated IrWindows DLL
#define SOURCE_NONE			1
#define SOURCE_BLACKBODY	2
#define SOURCE_VIS			3
#define SOURCE_VIS_ALIGN	4
#define SOURCE_LASER		5
#define SOURCE_LASER_ALIGN	6

#define READY_BIT		0x0281

// Static Variables
const int DIM_HORIZ = 0;
const int DIM_VERT  = 1;
const int DIODE_1570 = 0;
const int DIODE_1540 = 1;
const int DIODE_1064 = 2;
const int TARG_OPNAPR = 0;
const int TARG_BRSGHT = 1;
const int TARG_PIESECTOR = 2;
const int TARG_4BAR5 = 3;
const int TARG_4BAR383 = 4;
const int TARG_4BAR267 = 5;
const int TARG_4BAR15  = 6;
const int TARG_4BAR33  = 7;
const int TARG_DIAGLN  = 8;
const int TARG_ETCHED  = 9;
const int TARG_SPNPINHL = 10;
const int TARG_TGTGRP07 = 11;
const int TARG_TGTGRP1  = 12;
const int TARG_TGTGRP2  = 13;
const int TARG_GRYSCL   = 14;
const int TARG_IRBS00   = 15;
const int TARG_IRBS01   = 16;
const int TARG_IRBS02   = 17;
const int TARG_IRBS03   = 18;
const int TARG_IRBS04   = 19;
const int TARG_IRBS05   = 20;
const int TARG_IRBS06   = 21;
const int TARG_IRBS07   = 22;
const int TARG_IRBS08   = 23;
const int TARG_IRBS09   = 24;
const int TARG_IRBS10   = 25;
const int TARG_TVBS01   = 26;
const int TARG_TVBS02   = 27;
const int TARG_TVBS03   = 28;
const int TARG_TVBS04   = 29;
const int TARG_TVBS05   = 30;
const int TARG_TVBS06   = 31;
const int TARG_TVBS07   = 32;
const int TARG_TVBS08   = 33;
const int TARG_TVBS09   = 34;
const int TARG_TVBS10   = 35;
const int TARG_TVBS11   = 36;
const int TARG_TVBS12   = 37;
const int TARG_TVBS13   = 38;
const int TARG_TVBS14   = 39;
const int TARG_TVBS15   = 40;
const int TARG_CROSS17	 = 10;
const int TARG_4BAR10	 = 12;
const int TARG_4BAR66	 = 13;
const int TARG_SQUARE21	 = 14;
const int TARG_UNKNOWN	 = 41;
const int VIDEO_INTERNAL_1 = 1;
const int VIDEO_INTERNAL_2 = 2;
const int VIDEO_RS170	= 3;
const int VIDEO_RS343_675_4_3	= 4;
const int VIDEO_RS343_675_1_1	= 5;
const int VIDEO_RS343_729_4_3	= 6;
const int VIDEO_RS343_729_1_1	= 7;
const int VIDEO_RS343_875_4_3	= 8;
const int VIDEO_RS343_875_1_1	= 9;
const int VIDEO_RS343_945_4_3	= 10;
const int VIDEO_RS343_945_1_1	= 11;
const int VIDEO_RS343_1023_4_3	= 12;
const int VIDEO_RS343_1023_1_1	= 13;
const int VIDEO_UNKNOWN			= 14;
const int CONFIG_NONE           = 1;
const int CONFIG_BLACKBODY      = 2;
const int CONFIG_VISIBLE        = 3;
const int CONFIG_VISIBLE_ALIGN  = 4;
const int CONFIG_LASER          = 5;
const int CONFIG_LASER_ALIGN    = 6;

// debug message constants
const char MTFDIR[2][6] = {"HORIZ", "VERT"};
const char DIODES[3][11] = {"DIODE_1570", "DIODE_1540", "DIODE_1064"};
const char TARGETS[15][15] = {"TARG_OPNAPR", "TARG_BRSGHT", "TARG_PIESECTOR",
							  "TARG_4BAR5", "TARG_4BAR383", "TARG_4BAR267",
							  "TARG_4BAR15", "TARG_4BAR33", "TARG_DIAGLN",
							  "TARG_ETCHED", "TARG_SPNPIHHL", "TARG_TGTGRP07",
							  "TARG_TGTGRP1", "TARG_TGTGRP2", "TARG_GRYSCL"};
const char VIDEO[12][15] = {"RS170", "RS343_675_4_3", "RS343_675_1_1", 
							"RS343_729_4_3", "RS343_729_1_1", "RS343_875_4_3",
							"RS343_875_1_1", "RS343_945_4_3", "RS343_945_1_1",
							"RS343_1023_4_3", "RS343_1023_1_1"};
const char TRIGMODE[6][9] = {"", "FREE_RUN", "LASR", "EXT"};
const char TRIGSLOPE[3][4] = {"", "POS", "NEG"};
const char CONFIG[7][21] = {"", "CONFIG_NONE", "CONFIG_BLACKBODY", "CONFIG_VISIBLE",
							"CONFIG_VISIBLE_ALIGN", "CONFIG_LASER", "CONFIG_LASER_ALIGN"};

int	DE_BUG = 0;
FILE *debugfp = 0;

//++++//////////////////////////////////////////////////////////////////////////
// Exposed Functions
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// Function: CVeo2_IrWin_T
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Instno           int             System assigned instrument number
// ResourceType     int             Type of Resource Base, Physical, Virtual
// ResourceName     char*           Station resource name
// Sim              int             Simulation flag value (0/1)
// Dbglvl           int             Debug flag value
// AddressInfoPtr   InstAddPtr*     Contains all address information available
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ========================================
// Response         ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
////////////////////////////////////////////////////////////////////////////////

CVeo2_IrWin_T::CVeo2_IrWin_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
	memset(Response, '\0', Buffersize);

    // Save Device Info
    m_InstNo = Instno;
    m_ResourceType = ResourceType;
    m_ResourceName[0] = '\0';

    if(ResourceName)
    {
        strnzcpy(m_ResourceName,ResourceName,ATXMLW_MAX_NAME);
    }

    m_Sim = Sim;
    m_Dbg = Dbglvl;

    m_Handle = NULL;

    InitPrivateVeo2_IrWin();

	InitVeo2FuncPtrs(Response, Buffersize);

	//////////////////////////////////////////////////////////////////
	// Set power and state variables
	// The VEO2 will have power applied and removed multiple times
	// during the execution of a TPS. When power is applied a 60 sec
	// delay is required. As the power is applied from the DCPS the 
	// delay is the responsibility of the ATLAS TPS. No commands will
	// be issued until setup when the power state has been off. When
	// a setup command comes in and the state is off, a RESET_MODULE()
	// will be issued. 
	// After the last signal has been removed, an IRWIN_SHUTDOWN() 
	// command will be issued. A new macro, IFNSIMVEO2VEO2 will be added
	// that will also look at m_PowerOn before issueing a command.
	//////////////////////////////////////////////////////////////////
	m_SourceState = IDLE;
	m_SensorState = IDLE;
	m_PowerState = POWER_OFF;

	ReadLarrsDat();
	
    return;
}


////////////////////////////////////////////////////////////////////////////////
// Function: ~CVeo2_IrWin_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
//
// Return:
//    Class instance destroyed.
//
////////////////////////////////////////////////////////////////////////////////
CVeo2_IrWin_T::~CVeo2_IrWin_T()
{
    char Dummy[1024];

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CVeo2_IrWin Class Destructor called "),\
		Dummy,1024);

	ResetVeo2FuncPtrs();

    return;
}

////////////////////////////////////////////////////////////////////////////////
// Function: StatusVeo2_IrWin
//
// Purpose: Perform the Status action for this driver instance
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// 
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::StatusVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

	memset(Response, '\0', BufferSize);

    // Status action for the Veo2_IrWin
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusVeo2_IrWin called "),\
		Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "StatusVeo2_IrWin()", "Wrap-StatusVeo2_IrWin called", (char*)0));
    // Check for any pending error messages
    Status = ErrorVeo2_IrWin(0, Response, BufferSize);

    return(Status);
}

////////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalVeo2_IrWin
//
// Purpose: Perform the IEEE 1641 / Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// SignalDescription  ATXMLW_INTF_SIGDESC*   The Allocated FNC code
// 
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::IssueSignalVeo2_IrWin(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	
	memset(Response, '\0', BufferSize);

    // IEEE 1641 Issue Signal action for the Veo2_IrWin
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalVeo2_IrWin Signal: "),
                              Response, BufferSize);

    ISDODEBUG(dodebug(0, "IssueSignalVeo2_IrWin()", "Wrap-IssueSignalVeo2_IrWin Signal %s", SignalDescription, (char*)0));

	if ((Status = GetStmtInfoVeo2_IrWin(SignalDescription, Response, BufferSize)) == 0) {

		Status = CallSignalVeo2_IrWin(m_Action, Response, BufferSize);
	} 

    return(Status);
}

////////////////////////////////////////////////////////////////////////////////
// Function : CallSignalVeo2_IrWin
//
// Purpose: Perform the IEEE 1641 / Action action for this driver instance
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::CallSignalVeo2_IrWin(int action, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

    int       Status = 0;

    switch(action)
    {
		case ATXMLW_SA_APPLY:
			if ((Status = CallSignalVeo2_IrWin(ATXMLW_SA_SETUP, Response, BufferSize)) != 0)
				break;
			Status = CallSignalVeo2_IrWin(ATXMLW_SA_ENABLE, Response, BufferSize);
			break;
		case ATXMLW_SA_REMOVE:
			if ((Status = CallSignalVeo2_IrWin(ATXMLW_SA_DISABLE, Response, BufferSize)) != 0)
				break;
			Status = CallSignalVeo2_IrWin(ATXMLW_SA_RESET, Response, BufferSize);
			break;
		case ATXMLW_SA_MEASURE:
			Status = CallSignalVeo2_IrWin(ATXMLW_SA_SETUP, Response, BufferSize);
			break;
		case ATXMLW_SA_READ:
			if ((Status = CallSignalVeo2_IrWin(ATXMLW_SA_ENABLE, Response, BufferSize)) != 0)
				break;
			Status = CallSignalVeo2_IrWin(ATXMLW_SA_FETCH, Response, BufferSize);
			break;
		case ATXMLW_SA_RESET:
			Status = ResetVeo2_IrWin(Response, BufferSize);
			break;
		case ATXMLW_SA_SETUP:
			Status = SetupVeo2_IrWin(Response, BufferSize);
			break;
		case ATXMLW_SA_CONNECT:
			break;
		case ATXMLW_SA_ENABLE:
			Status = EnableVeo2_IrWin(Response, BufferSize);
			break;
		case ATXMLW_SA_DISABLE:
			Status = DisableVeo2_IrWin(Response, BufferSize);
			break;
		case ATXMLW_SA_FETCH:
			Status = FetchVeo2_IrWin(Response, BufferSize);
			break;
		case ATXMLW_SA_DISCONNECT:
			break;
		case ATXMLW_SA_STATUS:
			Status = StatusVeo2_IrWin(Response, BufferSize);
			break;
		default:
			ATXMLW_ERROR(-1, "CallSignalVeo2_IrWin", "Invalid Action passed to function", Response, BufferSize);
			Status = -1;
			break;
    }

	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalVeo2_IrWin
//
// Purpose: Register/Provide the Calibration data for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// CalData            ATXMLW_INTF_CALDATA*   Xml description of the calibration data
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::RegCalVeo2_IrWin(ATXMLW_INTF_CALDATA* CalData)
{
    char      Dummy[1024];

    // Setup action for the Veo2_IrWin
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalVeo2_IrWin CalData: %s", 
                               CalData),Dummy,1024);
    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: ResetVeo2_IrWin
//
// Purpose: Perform the Reset action for this driver instance
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// 
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::ResetVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	int targetPosition = 0;
	int laserState = 0;
	double delta = 0.0;
	time_t start = 0;
	time_t current = 0;

	memset(Response, '\0', BufferSize);

    // Reset action for the Veo2_IrWin
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetVeo2_IrWin called "),Response, BufferSize);

 	ISDODEBUG(dodebug(0, "ResetVeo2_IrWin()", "Wrap-ResetVeo2_IrWin called", (char*)0));
 	ISDODEBUG(dodebug(0, "ResetVeo2_IrWin()", "BufferSize is %d", BufferSize, (char*)0));

	if (m_SignalType == SOURCE)
	{
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("Reseting VEO2 Source"), Response, BufferSize);
	 	ISDODEBUG(dodebug(0, "ResetVeo2_IrWin()", "Reseting VEO2 Source", (char*)0));
		if (m_SignalNoun == INFRARED)
		{
			IFNSIMVEO2(SET_TARGET_POSITION_INITIATE(0));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_INITIATE(0)"), Response, BufferSize);

			time(&start);
			do
			{
				IFNSIMVEO2(SET_TARGET_POSITION_FETCH(&targetPosition));
				time(&current);
				delta = difftime(current, start);
			} while ((delta < 60.0) && (targetPosition != 0));

			// dme added 4-9-10 rscroble, turn off IR diff temp when reset, from here
			IFNSIMVEO2(SET_TEMP_DIFFERENTIAL_IR_INITIATE((float)0.0));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TEMP_DIFFERENTIAL_IR_INITIATE(0.0)"), Response, BufferSize);
			WaitForReady(60, &delta, Response, BufferSize);
			// dme added to here
		}
		else if (m_SignalNoun == LASER)
		{
			IFNSIMVEO2(SET_OPERATION_LASER_INITIATE(0));
			WaitForReady(60, &delta, Response, BufferSize);

 			ISDODEBUG(dodebug(0, "ResetVeo2_IrWin()", "SET_OPERATION_LASER_INITIATE(0)", (char*)0));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_OPERATION_LASER_INITIATE(0)"), Response, BufferSize);

			time(&start);
			do {
				Sleep(1000);

				IFNSIMVEO2(SET_OPERATION_LASER_FETCH(&laserState));

				time(&current);
				delta = difftime(current, start);
			}while((delta < 60.0) && (laserState != 0));

			ISDODEBUG(dodebug(0, "ResetVeo2_IrWin()", "laserState = %d delta = %f", laserState, delta, (char*)0));

			if (m_LastPulseRange.Exists)
			{

				float	TmpVal = 10.0;

				IFNSIMVEO2(SET_PULSE2_DELAY_LASER_INITIATE(0));

				time(&start);
				do {
					Sleep(1000);

					IFNSIMVEO2(SET_PULSE2_DELAY_LASER_FETCH(&TmpVal));

					time(&current);
					delta = difftime(current, start);
				}while((delta < 60.0) && (TmpVal != 0));
			}
		}
		else if (m_SignalNoun == LIGHT)
		{
			IFNSIMVEO2(SET_TARGET_POSITION_INITIATE(0));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_INITIATE(0)"), Response, BufferSize);

			IFNSIMVEO2(SET_ANGULAR_RATE_VIS_INITIATE((float)0.0));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_ANGULAR_RATE_VIS_INITIATE(0.0)"), Response, BufferSize);
			WaitForReady(60.0, &delta, Response, BufferSize);

			time(&start);
			do
			{
				IFNSIMVEO2(SET_TARGET_POSITION_FETCH(&targetPosition));
				time(&current);
				delta = difftime(current, start);
			} while ((delta < 60.0) && (targetPosition != 0));

			IFNSIMVEO2(SET_RADIANCE_VIS_INITIATE((float)0.0));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_RADIANCE_VIS_INITIATE(0.0)"), Response, BufferSize);
			WaitForReady(60.0, &delta, Response, BufferSize);
		}
		// set source state to idle
		m_SourceState = IDLE;
	}
	else if (m_SignalType == SENSOR)
	{
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("Reset for VEO2 Sensor"), Response, BufferSize);
		// set sensor state to idle
		m_SensorState = IDLE;
	}

	////////////////////////////////////////////////////////////////////////////
	// If power is on, sensor is idle, and source is idle, power down module
	////////////////////////////////////////////////////////////////////////////
	if ((m_PowerState == POWER_ON) && (m_SensorState == IDLE) && (m_SourceState == IDLE))
	{
		/*if ((IRWIN_SHUTDOWN == NULL) && (!m_Sim))
		{
			ATXMLW_ERROR(0, "ResetVeo2_IrWin()",\
				"IRWIN_SHUTDOWN bad function pointer", Response, BufferSize);
			return 0;
		}*/

		IFNSIM(IRWIN_SHUTDOWN());
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("IRWIN_SHUTDOWN();"), Response, BufferSize);

	 	ISDODEBUG(dodebug(0, "ResetVeo2_IrWin()", "IRWIN_SHUTDOWN()", (char*)0));
		m_PowerState = POWER_OFF;
	}

    InitPrivateVeo2_IrWin();

    return(Status);
}

////////////////////////////////////////////////////////////////////////////////
// Function: IstVeo2_IrWin
//
// Purpose: Perform the SelfTest Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Level              ATXMLW_INTF_STLEVEL    Indicates the Level To Be Performed
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::IstVeo2_IrWin(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	
	memset(Response, '\0', BufferSize);

    // Reset action for the Veo2_IrWin
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstVeo2_IrWin called Level %d", 
                              Level), Response, BufferSize);
    switch(Level)
    {
    case ATXMLW_IST_LVL_PST:
        Status = StatusVeo2_IrWin(Response,BufferSize);
        break;
    case ATXMLW_IST_LVL_IST:
        break;
    case ATXMLW_IST_LVL_CNF:
        Status = StatusVeo2_IrWin(Response,BufferSize);
        break;
    default:
        break;
    }

    if(Status)
        ErrorVeo2_IrWin(Status, Response, BufferSize);

    InitPrivateVeo2_IrWin();

    return(Status);
}

////////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsVeo2_IrWin
//
// Purpose: Issue Native nstrument commands for to this instrument
//          Return in the response values in Response
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// InstrumentCmds     ATXMLW_INTF_INSTCMD*   Xml description of the Native 
//                                           Instrument commands
// 
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::IssueNativeCmdsVeo2_IrWin(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	memset(Response, '\0', BufferSize);

    // Setup action for the Veo2_IrWin
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsVeo2_IrWin "),Response, BufferSize);

    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallVeo2_IrWin
//
// Purpose: Issue Instrument Driver function calls for to this instrument
//          Return in the response values in Response
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// DriverFunction     ATXMLW_INTF_DRVRFNC*   Xml description of the IVI 
//                                           Instrument commands
// 
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::IssueDriverFunctionCallVeo2_IrWin(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	memset(Response, '\0', BufferSize);

    // Setup action for the Veo2_IrWin
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallVeo2_IrWin", Response, BufferSize);

    return(0);
}

//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions                                                    //
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWin
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	memset(Response, '\0', BufferSize);

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "Wrap-SetupVeo2_IrWin called  BufferSize = %d", BufferSize, (char*)0));
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupVeo2_IrWin called"), Response, BufferSize);

	//////////////////////////////////////////////////////////////////////////////////
	// Check for power state, If power state is off, issue a RESET_MODULE() command	//
	// ATLAS TPS is required to wait for 180 sec after power is applied to the VEO2	//
	//////////////////////////////////////////////////////////////////////////////////
	if (m_PowerState == POWER_OFF) 	{
		IFNSIM(RESET_MODULE_INITIATE());
		m_PowerState = POWER_ON;

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "RESET_MODULE_INITIATE()", (char*)0));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("RESET_MODULE_INITIATE();	[POWER_ON]"),
					 Response, BufferSize);
	}

	if (m_SignalType == SENSOR) {
		return (SetupVeo2_IrWinSensors(Response, BufferSize));
	}

	if (m_SignalType == SOURCE) {
		return (SetupVeo2_IrWinSources(Response, BufferSize));
	}

    return(0);
}


//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions                                                    //
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinSensors
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWinSensors(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Sensor action for attribute %s", m_MeasInfo.cyType), Response, BufferSize);

	m_SensorState = ACTIVE;

	SetMeasDefaults(Response, BufferSize);

	////////////////////////////////////////////////////////////////////////
	// INFRARED SENSOR ACTIONS                                            //
	////////////////////////////////////////////////////////////////////////
	
	if ((strstr(m_MeasInfo.cyType, "IR")) != 0) {
		return (SetupVeo2_IrWinIRSensors(Response, BufferSize));
	}

	////////////////////////////////////////////////////////////////////////
	// LASER SENSOR ACTIONS                                               //
	////////////////////////////////////////////////////////////////////////
	else if ((strstr(m_MeasInfo.cyType, "Laser")) != 0) {
		return (SetupVeo2_IrWinLaserSensors(Response, BufferSize));
	}

	////////////////////////////////////////////////////////////////////////
	// VISIBLE LIGHT SENSOR ACTIONS                                       //
	////////////////////////////////////////////////////////////////////////
	else if ((strstr(m_MeasInfo.cyType, "Vis")) != 0) {
		return (SetupVeo2_IrWinVisSensors(Response, BufferSize));
	}

    return(0);
}

//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions                                                    //
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinIRSensors
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWinIRSensors(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int		nIdx		= 0;
	int		Source		= 0;
	int		dTopPixel	= 0;
	int		dBotPixel	= 0;
	int		dLeftPixel	= 0;
	int		dRightPixel	= 0;
	int		ambientBlockTopLeftX  = 0;
	int		ambientBlockTopLeftY  = 0;
	int		ambientBlockBotRightX = 0;
	int		ambientBlockBotRightY = 0;
	float	*DistortionPositions  = NULL;
	double	fTopmRads   = 0.0;
	double	fBotmRads   = 0.0;
	double	fLeftmRads  = 0.0;
	double	fRightmRads = 0.0;
	double	elapsedTime = 0.0;
	char	sMsg[255]   = "";

	if (strcmp(m_MeasInfo.cyType, "losAlignErrorIR") == 0)
	{
		if ((m_TargetType.Int == TARG_BRSGHT) || (m_TargetType.Int == TARG_OPNAPR))
		{
			Source = SOURCE_BLACKBODY;
		}
		else if ((m_TargetType.Int >= TARG_IRBS00) && (m_TargetType.Int <= TARG_IRBS10))
		{
			Source = SOURCE_LASER_ALIGN;
		}

		IFNSIMVEO2(BORESIGHT_IR_SETUP(Source,\
									  m_MeasInfo.Attrs.samples,\
									  (float)m_HorizFieldOfView.Real,\
									  (float)m_VertFieldOfView.Real,\
									  (float)m_DifferentialTemp.Real,\
									  TARG_BRSGHT,\
									  m_CenterX,\
									  m_CenterY,\
									  m_SignalBlockTopLeftX,\
									  m_SignalBlockTopLeftY,\
									  m_SignalBlockBotRightX,\
									  m_SignalBlockBotRightY,\
									  m_Format.Int,\
									  (float)m_IntensityRatio.Real));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_IR_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %lf);",\
														 Source,\
														 m_MeasInfo.Attrs.samples,\
														 (float)m_HorizFieldOfView.Real,\
														 (float)m_VertFieldOfView.Real,\
														 (float)m_DifferentialTemp.Real,\
														 TARG_BRSGHT,\
														 m_CenterX,\
														 m_CenterY,\
														 m_SignalBlockTopLeftX,\
														 m_SignalBlockTopLeftY,\
														 m_SignalBlockBotRightX,\
														 m_SignalBlockBotRightY,\
														 m_Format.Int,\
														 (float)m_IntensityRatio.Real), Response, BufferSize);
	}
	// MODULATION TRANSFER FUNCTION IR
	else if (strcmp(m_MeasInfo.cyType, "modulationTransferFunctionIR") == 0)
	{
		Source = SOURCE_BLACKBODY;
		// Notes: The correction curve is set by the wavelength,
		//        1 for 8-12 um, 2 for 3-5 um
		IFNSIMVEO2(MODULATION_TRANSFER_FUNCTION_IR_SETUP(Source,\
														 m_MeasInfo.Attrs.samples,\
														 (float)m_HorizFieldOfView.Real,\
														 (float)m_VertFieldOfView.Real,\
														 (float)m_DifferentialTemp.Real,\
														 m_TargetType.Int,\
														 m_CenterX,\
														 m_CenterY,\
														 m_SignalBlockTopLeftX,\
														 m_SignalBlockTopLeftY,\
														 m_SignalBlockBotRightX,\
														 m_SignalBlockBotRightY,\
														 m_Format.Int,\
														 m_MtfDirection.Int,\
														 m_Filter.Int,\
														 m_Smoothing,\
														 m_MtfFreqPoints.Int,\
														 m_CorrectionCurve));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SETUP_MODULATION_TRANSFER_FUNCTION_IR_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d);",
																				   Source,\
																				   m_MeasInfo.Attrs.samples,\
																				   (float)m_HorizFieldOfView.Real,\
																				   (float)m_VertFieldOfView.Real,\
																				   (float)m_DifferentialTemp.Real,\
																				   m_TargetType.Int,\
																				   m_CenterX,\
																				   m_CenterY,\
																				   m_SignalBlockTopLeftX,\
																				   m_SignalBlockTopLeftY,\
																				   m_SignalBlockBotRightX,\
																				   m_SignalBlockBotRightY,\
																				   m_Format.Int,\
																				   m_MtfDirection.Int,\
																				   m_Filter.Int,\
																				   m_Smoothing,\
																				   m_MtfFreqPoints.Int,\
																				   m_CorrectionCurve), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "distortionIR") == 0)
	{
		Source = SOURCE_BLACKBODY;
		DistortionPositions = new float[m_DistortionPositions.Size];
		for (nIdx = 0; nIdx < m_DistortionPositions.Size; nIdx++)
			DistortionPositions[nIdx] = (float)m_DistortionPositions.val[nIdx];

		IFNSIMVEO2(GEOMETRIC_FIDELITY_DISTORTION_IR_SETUP(Source,\
														  m_MeasInfo.Attrs.samples,\
														  (float)m_HorizFieldOfView.Real,\
														  (float)m_VertFieldOfView.Real,\
														  (float)m_DifferentialTemp.Real,\
														  m_TargetType.Int,\
														  m_CenterX,\
														  m_CenterY,\
														  m_SignalBlockTopLeftX,\
														  m_SignalBlockTopLeftY,\
														  m_SignalBlockBotRightX,\
														  m_SignalBlockBotRightY,\
														  m_Format.Int,
														  m_DistPosCount.Int,
														  DistortionPositions));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GEOMETRIC_FIDELITY_DISTORTION_IR_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %d, [%lf ...]);",\
																			  Source,\
																			  m_MeasInfo.Attrs.samples,\
																			  (float)m_HorizFieldOfView.Real,\
																			  (float)m_VertFieldOfView.Real,\
																			  (float)m_DifferentialTemp.Real,\
																			  m_TargetType.Int,\
																			  m_CenterX,\
																			  m_CenterY,\
																			  m_SignalBlockTopLeftX,\
																			  m_SignalBlockTopLeftY,\
																			  m_SignalBlockBotRightX,\
																			  m_SignalBlockBotRightY,\
																			  m_Format.Int,\
																			  m_DistPosCount.Int,\
																			  DistortionPositions[0]), Response, BufferSize);

		delete [] DistortionPositions;
	}
	else if (strcmp(m_MeasInfo.cyType, "noiseEqDiffTempIR") == 0)
	{
		// setup target boundries
		switch (m_TargetType.Int)
		{
			case TARG_OPNAPR:
				fLeftmRads  = -18.75;
				fRightmRads =  18.75;
				fTopmRads   = -18.75;
				fBotmRads   =  18.75;
				break;
			case TARG_4BAR33:
				fLeftmRads  = -2.274;
				fRightmRads = -0.758;
				fTopmRads   = -5.306;
				fBotmRads   =  5.306;
				break;
			case TARG_PIESECTOR:
				fLeftmRads  = -16.0;
				fRightmRads =   0.0;
				fTopmRads   =   0.0;
				fBotmRads   =  16.0;
				break;
			case TARG_SQUARE21 :
				fLeftmRads  = -10.5;
				fRightmRads =  10.5;
				fTopmRads   = -10.5;
				fBotmRads   =  10.5;
				break;
			default:
				break;
		}
		// correct for LOS errors
		fLeftmRads  -= m_HorizLosAlignError.Real;
		fRightmRads -= m_HorizLosAlignError.Real;
		fTopmRads   -= m_VertLosAlignError.Real;
		fBotmRads   -= m_VertLosAlignError.Real;

		// calculate pixels
		dLeftPixel  = m_CenterX + (int)(fLeftmRads  * m_HorzScale);
		dRightPixel = m_CenterX + (int)(fRightmRads * m_HorzScale);
		dTopPixel   = m_CenterY + (int)(fTopmRads   * m_VertScale);
		dBotPixel   = m_CenterY + (int)(fBotmRads   * m_VertScale);

		// error check
		if (m_SignalBlockTopLeftX < dLeftPixel)
		{
			sprintf(sMsg, "Left edge of ROI (%d) not within open area (%d)", m_SignalBlockTopLeftX, dLeftPixel);
			ATXMLW_ERROR(0, "SetupVeo2_IrWin()", sMsg, Response, BufferSize);
		}
		if (m_SignalBlockBotRightX > dRightPixel)
		{
			sprintf(sMsg, "Right edge of ROI (%d) not within open area (%d)", m_SignalBlockBotRightX, dRightPixel);
			ATXMLW_ERROR(0, "SetupVeo2_IrWin()", sMsg, Response, BufferSize);
		}
		if (m_SignalBlockTopLeftY < dTopPixel)
		{
			sprintf(sMsg, "Top edge of ROI (%d) not within open area (%d)", m_SignalBlockTopLeftY, dTopPixel);
			ATXMLW_ERROR(0, "SetupVeo2_IrWin()", sMsg, Response, BufferSize);
		}
		if (m_SignalBlockBotRightY > dBotPixel)
		{
			sprintf(sMsg, "Bottom edge of ROI (%d) not within open area (%d)", m_SignalBlockBotRightY, dBotPixel);
			ATXMLW_ERROR(0, "SetupVeo2_IrWin()", sMsg, Response, BufferSize);
		}

		// Calculate Ambient signal block
		ambientBlockTopLeftY  = m_SignalBlockTopLeftY;
		ambientBlockBotRightY = m_SignalBlockBotRightY;

		if ((m_TargetType.Int == TARG_OPNAPR)  || (m_TargetType.Int == TARG_SQUARE21))
		{
			ambientBlockBotRightX = m_VertLines - 1;	// right edge = right edge of image
			ambientBlockTopLeftX = ambientBlockBotRightX - (m_SignalBlockBotRightX - m_SignalBlockTopLeftX);

			if (ambientBlockTopLeftX <= m_SignalBlockBotRightX)
			{
				// Error - left edge of ambient block overlaps ROI - ROI too wide
				ATXMLW_ERROR(-1, "SetupVeo2_IrWin()", "Left edge of ambient block overlaps ROI. ROI too wide",\
							 Response, BufferSize);
			}
		}
		else
		{
			ambientBlockTopLeftX = dRightPixel + (dRightPixel - m_SignalBlockBotRightX);
			ambientBlockBotRightX = ambientBlockTopLeftX + (m_SignalBlockBotRightX - m_SignalBlockTopLeftX);
		}

		if (ambientBlockTopLeftX > (m_VertLines - 1))
		{
			sprintf(sMsg, "Left edge of Ambient Block (%d) is not within image (%d). H-FOV too narrow.", 
					ambientBlockTopLeftX, (int)(m_VertLines-1));
			ATXMLW_ERROR(-1, "SetupVeo2_IrWin()", sMsg, Response, BufferSize);
		}
		else if (ambientBlockBotRightX > (m_VertLines - 1))
		{
			sprintf(sMsg, "Right edge of Ambient Block(%d) is not within image (%d). May be too much LOS-ALIGN-ERROR.",
					ambientBlockBotRightX, (m_VertLines-1));
			ATXMLW_ERROR(-1, "SetupVeo2_IrWin()", sMsg, Response, BufferSize);
		}

		Source = SOURCE_BLACKBODY;

		IFNSIMVEO2(NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP(Source,\
																	  m_MeasInfo.Attrs.samples,\
																	  (float)m_HorizFieldOfView.Real,\
																	  (float)m_VertFieldOfView.Real,\
																	  (float)m_DifferentialTemp.Real,\
																	  m_TargetType.Int,\
																	  m_CenterX,\
																	  m_CenterY,\
																	  m_SignalBlockTopLeftX,\
																	  m_SignalBlockTopLeftY,\
																	  m_SignalBlockBotRightX,\
																	  m_SignalBlockBotRightY,\
																	  m_Format.Int,\
																	  (float)m_DiffTempStart.Real,\
																	  (float)m_DiffTempStop.Real,\
																	  (float)m_DiffTempInterval.Real,\
																	  ambientBlockTopLeftX,\
																	  ambientBlockTopLeftY,\
																	  ambientBlockBotRightX,\
																	  ambientBlockBotRightY));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %lf, %lf,%lf, %d, %d, %d, %d);",\
																						  Source,\
																						  m_MeasInfo.Attrs.samples,\
																						  (float)m_HorizFieldOfView.Real,\
																						  (float)m_VertFieldOfView.Real,\
																						  (float)m_DifferentialTemp.Real,\
																						  m_TargetType.Int,\
																						  m_CenterX,\
																						  m_CenterY,\
																						  m_SignalBlockTopLeftX,\
																						  m_SignalBlockTopLeftY,\
																						  m_SignalBlockBotRightX,\
																						  m_SignalBlockBotRightY,\
																						  m_Format.Int,\
																						  (float)m_DiffTempStart.Real,\
																						  (float)m_DiffTempStop.Real,\
																						  (float)m_DiffTempInterval.Real,\
																						  ambientBlockTopLeftX,\
																						  ambientBlockTopLeftY,\
																						  ambientBlockBotRightX,\
																						  ambientBlockBotRightY), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "uniformityIR") == 0)
	{
		Source = SOURCE_BLACKBODY;

		IFNSIMVEO2(IMAGE_UNIFORMITY_IR_SETUP(Source,\
											 m_MeasInfo.Attrs.samples,\
											 (float)m_HorizFieldOfView.Real,\
											 (float)m_VertFieldOfView.Real,\
											 (float)m_DifferentialTemp.Real,\
											 m_TargetType.Int,\
											 m_CenterX,\
											 m_CenterY,\
											 m_SignalBlockTopLeftX,\
											 m_SignalBlockTopLeftY,\
											 m_SignalBlockBotRightX,\
											 m_SignalBlockBotRightY,\
											 m_Format.Int));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("IMAGE_UNIFORMITY_IR_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d);",\
																 Source,\
																 m_MeasInfo.Attrs.samples,\
																 (float)m_HorizFieldOfView.Real,\
																 (float)m_VertFieldOfView.Real,\
																 (float)m_DifferentialTemp.Real,\
																 m_TargetType.Int,\
																 m_CenterX,\
																 m_CenterY,\
																 m_SignalBlockTopLeftX,\
																 m_SignalBlockTopLeftY,\
																 m_SignalBlockBotRightX,\
																 m_SignalBlockBotRightY,\
																 m_Format.Int), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "chanIntegrityIR") == 0)
	{
		Source = SOURCE_BLACKBODY;

		IFNSIMVEO2(CHANNEL_INTEGRITY_IR_SETUP(Source,\
											  m_MeasInfo.Attrs.samples,\
											  (float)m_HorizFieldOfView.Real,\
											  (float)m_VertFieldOfView.Real,\
											  (float)m_DifferentialTemp.Real,\
											  m_TargetType.Int,\
											  m_CenterX,\
											  m_CenterY,\
											  m_SignalBlockTopLeftX,\
											  m_SignalBlockTopLeftY,\
											  m_SignalBlockBotRightX,\
											  m_SignalBlockBotRightY,\
											  m_Format.Int,\
											  (float)m_LinesPerChannel.Int,\
											  (float)m_FirstActiveLine.Int,\
											  (float)m_NoiseEqDiffTemp.Real));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("CHANNEL_INTEGRITY_IR_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %lf, %lf, %lf);",\
																  Source,\
																  m_MeasInfo.Attrs.samples,\
																  (float)m_HorizFieldOfView.Real,\
																  (float)m_VertFieldOfView.Real,\
																  (float)m_DifferentialTemp.Real,\
																  m_TargetType.Int,\
																  m_CenterX,\
																  m_CenterY,\
																  m_SignalBlockTopLeftX,\
																  m_SignalBlockTopLeftY,\
																  m_SignalBlockBotRightX,\
																  m_SignalBlockBotRightY,\
																  m_Format.Int,\
																  (float)m_LinesPerChannel.Int,\
																  (float)m_FirstActiveLine.Int,\
																  (float)m_NoiseEqDiffTemp.Real), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "diffBoresightAngleIR") == 0)
	{
		if ((m_TargetType.Int == TARG_BRSGHT) || (m_TargetType.Int == TARG_OPNAPR))
		{
			Source = SOURCE_BLACKBODY;
		}
		else if ((m_TargetType.Int >= TARG_IRBS00) && (m_TargetType.Int <= TARG_IRBS10))
		{
			Source = SOURCE_LASER_ALIGN;
		}

		IFNSIMVEO2(BORESIGHT_IR_SETUP(Source,\
									  m_MeasInfo.Attrs.samples,\
									  (float)m_HorizFieldOfView.Real,\
									  (float)m_VertFieldOfView.Real,\
									  (float)m_DifferentialTemp.Real,\
									  TARG_BRSGHT,\
									  m_CenterX,\
									  m_CenterY,\
									  m_SignalBlockTopLeftX,\
									  m_SignalBlockTopLeftY,\
									  m_SignalBlockBotRightX,\
									  m_SignalBlockBotRightY,\
									  m_Format.Int,\
									  (float)m_IntensityRatio.Real));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_IR_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %lf);",\
														  Source,\
														  m_MeasInfo.Attrs.samples,\
														  (float)m_HorizFieldOfView.Real,\
														  (float)m_VertFieldOfView.Real,\
														  (float)m_DifferentialTemp.Real,\
														  TARG_BRSGHT,\
														  m_CenterX,\
														  m_CenterY,\
														  m_SignalBlockTopLeftX,\
														  m_SignalBlockTopLeftY,\
														  m_SignalBlockBotRightX,\
														  m_SignalBlockBotRightY,\
														  m_Format.Int,\
														  (float)m_IntensityRatio.Real), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "minResolvTempDiffIR") == 0)
	{
		// This test has been modified to only set the blackbody temperature and
		// target wheel position. The CEM will prompt the user as to the visibility
		// of the target. On fetch, the blackbody temperature will be returned.
		IFNSIMVEO2(SET_TARGET_POSITION_INITIATE(m_TargetPosition[1]));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_INITIATE(%d);", m_TargetPosition[1]), Response, BufferSize);

		// 1. Set differential temp error
		IFNSIMVEO2(SET_RDY_WINDOW_IR_INITIATE((float)0.005));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_RDY_WINDOW_IR_INITIATE(0.005);"), Response, BufferSize);

		// 2. Set differential temp
		IFNSIMVEO2(SET_TEMP_DIFFERENTIAL_IR_INITIATE((float)m_StartingDiffTemps[1]));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TEMP_DIFFERENTIAL_IR_INITIATE(%lf);", m_StartingDiffTemps[1]), Response, BufferSize);

		WaitForReady(100.0, &elapsedTime, Response, BufferSize);

	}
	else if ((strcmp(m_MeasInfo.cyType, "differentialTempIR") == 0) ||
			 (strcmp(m_MeasInfo.cyType, "ambientTempIR")      == 0) ||
			 (strcmp(m_MeasInfo.cyType, "blackbodyTempIR")    == 0))
	{
		;	// no setup action required - INITIATE / FETCH only
	}

	return (0);
}

//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions                                                    //
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinVisSensors
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWinVisSensors(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int		nIdx		= 0;
	int		Size		= 0;
	int		Source		= 0;
	float	ColorTemperature	  = 2856.0;
	float	*DistortionPositions  = NULL;

	if (strcmp(m_MeasInfo.cyType, "losAlignErrorVis") == 0)
	{
		Source = SOURCE_VIS_ALIGN;

		IFNSIMVEO2(BORESIGHT_TV_VIS_SETUP(Source,\
										  m_MeasInfo.Attrs.samples,\
										  (float)m_HorizFieldOfView.Real,\
										  (float)m_VertFieldOfView.Real,\
										  (float)m_Radiance.Real,\
										  m_TargetType.Int,\
										  m_CenterX,\
										  m_CenterY,\
										  m_SignalBlockTopLeftX,\
										  m_SignalBlockTopLeftY,\
										  m_SignalBlockBotRightX,\
										  m_SignalBlockBotRightY,\
										  m_Format.Int,\
										  ColorTemperature,\
										  (float)m_IntensityRatio.Real));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_TV_VIS_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %f, %lf);",\
															  Source,\
															  m_MeasInfo.Attrs.samples,\
															  (float)m_HorizFieldOfView.Real,\
															  (float)m_VertFieldOfView.Real,\
															  (float)m_Radiance.Real,\
															  m_TargetType.Int,\
															  m_CenterX,\
															  m_CenterY,\
															  m_SignalBlockTopLeftX,\
															  m_SignalBlockTopLeftY,\
															  m_SignalBlockBotRightX,\
															  m_SignalBlockBotRightY,\
															  m_Format.Int,\
															  ColorTemperature,\
															  (float)m_IntensityRatio.Real), Response, BufferSize);

		Sleep(1000);
	}
	else if (strcmp(m_MeasInfo.cyType, "modulationTransferFunctionVis") == 0)
	{
		int nCorrectionCurve = 0;
		int nSmoothing = 0;

		Source = SOURCE_VIS;

		IFNSIMVEO2(MODULATION_TRANSFER_FUNCTION_VIS_SETUP(Source,\
														  m_MeasInfo.Attrs.samples,\
														  (float)m_HorizFieldOfView.Real,\
														  (float)m_VertFieldOfView.Real,\
														  (float)m_Radiance.Real,\
														  m_TargetType.Int,\
														  m_CenterX,\
														  m_CenterY,\
														  m_SignalBlockTopLeftX,\
														  m_SignalBlockTopLeftY,\
														  m_SignalBlockBotRightX,\
														  m_SignalBlockBotRightY,\
														  m_Format.Int,\
														  ColorTemperature,\
														  m_MtfDirection.Int,\
														  m_Filter.Int,\
														  nSmoothing,\
														  m_MtfFreqPoints.Int,\
														  nCorrectionCurve));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("MODULATION_TRANSFER_FUNCTION_VIS_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %f, %d, %d, %d, %d, %d);",
																			  Source,\
																			  m_MeasInfo.Attrs.samples,\
																			  (float)m_HorizFieldOfView.Real,\
																			  (float)m_VertFieldOfView.Real,\
																			  (float)m_Radiance.Real,\
																			  m_TargetType.Int,\
																			  m_CenterX,\
																			  m_CenterY,\
																			  m_SignalBlockTopLeftX,\
																			  m_SignalBlockTopLeftY,\
																			  m_SignalBlockBotRightX,\
																			  m_SignalBlockBotRightY,\
																			  m_Format.Int,\
																			  ColorTemperature,\
																			  m_MtfDirection.Int,\
																			  m_Filter.Int,\
																			  nSmoothing,\
																			  m_MtfFreqPoints.Int,\
																			  nCorrectionCurve), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "distortionVis") == 0)
	{
		Source = SOURCE_VIS;
		DistortionPositions = new float[m_DistortionPositions.Size];

		for (nIdx = 0; nIdx < m_DistortionPositions.Size; nIdx++)
			DistortionPositions[nIdx] = (float)m_DistortionPositions.val[nIdx];

		IFNSIMVEO2(GEOMETRIC_FIDELITY_DISTORTION_VIS_SETUP(Source,\
														   m_MeasInfo.Attrs.samples,\
														   (float)m_HorizFieldOfView.Real,\
														   (float)m_VertFieldOfView.Real,\
														   (float)m_Radiance.Real,\
														   m_TargetType.Int,\
														   m_CenterX,\
														   m_CenterY,\
														   m_SignalBlockTopLeftX,\
														   m_SignalBlockTopLeftY,\
														   m_SignalBlockBotRightX,\
														   m_SignalBlockBotRightY,\
														   m_Format.Int,\
														   ColorTemperature,\
														   m_DistPosCount.Int,\
														   DistortionPositions));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GEOMETRIC_FIDELITY_DISTORTION_VIS_SETUP(%d, %d, %f, %f, %f, %d, %d, %d, %d, %d, %d, %d, %d, %f, %d, %f);",\
																			   Source,\
																			   m_MeasInfo.Attrs.samples,\
																			   (float)m_HorizFieldOfView.Real,\
																			   (float)m_VertFieldOfView.Real,\
																			   (float)m_Radiance.Real,\
																			   m_TargetType.Int,\
																			   m_CenterX,\
																			   m_CenterY,\
																			   m_SignalBlockTopLeftX,\
																			   m_SignalBlockTopLeftY,\
																			   m_SignalBlockBotRightX,\
																			   m_SignalBlockBotRightY,\
																			   m_Format.Int,\
																			   ColorTemperature,\
																			   m_DistPosCount.Int,\
																			   DistortionPositions[0]), Response, BufferSize);

		delete [] DistortionPositions;
	}
	else if (strcmp(m_MeasInfo.cyType, "uniformityVis") == 0)
	{
		Source = SOURCE_VIS;

		IFNSIMVEO2(CAMERA_UNIFORMITY_VIS_SETUP(Source,\
											   m_MeasInfo.Attrs.samples,\
											   (float)m_HorizFieldOfView.Real,\
											   (float)m_VertFieldOfView.Real,\
											   (float)m_Radiance.Real,\
											   m_TargetType.Int,\
											   m_CenterX,\
											   m_CenterY,\
											   m_SignalBlockTopLeftX,\
											   m_SignalBlockTopLeftY,\
											   m_SignalBlockBotRightX,\
											   m_SignalBlockBotRightY,\
											   m_Format.Int,\
											   ColorTemperature));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("CAMERA_UNIFORMITY_VIS_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %f);",
																   Source,\
																   m_MeasInfo.Attrs.samples,\
																   (float)m_HorizFieldOfView.Real,\
																   (float)m_VertFieldOfView.Real,\
																   (float)m_Radiance.Real,\
																   m_TargetType.Int,\
																   m_CenterX,\
																   m_CenterY,\
																   m_SignalBlockTopLeftX,\
																   m_SignalBlockTopLeftY,\
																   m_SignalBlockBotRightX,\
																   m_SignalBlockBotRightY,\
																   m_Format.Int,\
																   ColorTemperature), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "cameraGainVis") == 0)
	{
		Source = SOURCE_VIS;

		IFNSIMVEO2(GAIN_VIS_SETUP(Source,\
								  m_MeasInfo.Attrs.samples,\
								  (float)m_HorizFieldOfView.Real,\
								  (float)m_VertFieldOfView.Real,\
								  (float)m_Radiance.Real,\
								  m_TargetType.Int,\
								  m_CenterX,\
								  m_CenterY,\
								  m_SignalBlockTopLeftX,\
								  m_SignalBlockTopLeftY,\
								  m_SignalBlockBotRightX,\
								  m_SignalBlockBotRightY,\
								  m_Format.Int,\
								  ColorTemperature,\
								  (float)m_RadianceStart.Real,\
								  (float)m_RadianceStop.Real,\
								  (float)m_RadianceInterval.Real));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GAIN_VIS_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %f, %lf, %lf, %lf);",\
													  Source,\
													  m_MeasInfo.Attrs.samples,\
													  (float)m_HorizFieldOfView.Real,\
													  (float)m_VertFieldOfView.Real,\
													  (float)m_Radiance.Real,\
													  m_TargetType.Int,\
													  m_CenterX,\
													  m_CenterY,\
													  m_SignalBlockTopLeftX,\
													  m_SignalBlockTopLeftY,\
													  m_SignalBlockBotRightX,\
													  m_SignalBlockBotRightY,\
													  m_Format.Int,\
													  ColorTemperature,\
													  (float)m_RadianceStart.Real,\
													  (float)m_RadianceStop.Real,\
													  (float)m_RadianceInterval.Real), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "dynamicRangeVis") == 0)
	{
		Source = SOURCE_VIS;

		IFNSIMVEO2(GAIN_VIS_SETUP(Source,\
								  m_MeasInfo.Attrs.samples,\
								  (float)m_HorizFieldOfView.Real,\
								  (float)m_VertFieldOfView.Real,\
								  (float)m_Radiance.Real,\
								  m_TargetType.Int,\
								  m_CenterX,\
								  m_CenterY,\
								  m_SignalBlockTopLeftX,\
								  m_SignalBlockTopLeftY,\
								  m_SignalBlockBotRightX,\
								  m_SignalBlockBotRightY,\
								  m_Format.Int,\
								  ColorTemperature,\
								  (float)m_RadianceStart.Real,\
								  (float)m_RadianceStop.Real,\
								  (float)m_RadianceInterval.Real));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GAIN_VIS_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %f, %lf, %lf, %lf);",\
													  Source,\
													  m_MeasInfo.Attrs.samples,\
													  (float)m_HorizFieldOfView.Real,\
													  (float)m_VertFieldOfView.Real,\
													  (float)m_Radiance.Real,\
													  m_TargetType.Int,\
													  m_CenterX,\
													  m_CenterY,\
													  m_SignalBlockTopLeftX,\
													  m_SignalBlockTopLeftY,\
													  m_SignalBlockBotRightX,\
													  m_SignalBlockBotRightY,\
													  m_Format.Int,\
													  ColorTemperature,\
													  (float)m_RadianceStart.Real,\
													  (float)m_RadianceStop.Real,\
													  (float)m_RadianceInterval.Real), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "grayScaleResolutionVis") == 0)
	{
		Source = SOURCE_VIS;

		IFNSIMVEO2(SHADES_OF_GRAY_VIS_SETUP(Source,\
											m_MeasInfo.Attrs.samples,\
											(float)m_HorizFieldOfView.Real,\
											(float)m_VertFieldOfView.Real,\
											(float)m_Radiance.Real,\
											m_TargetType.Int,\
											m_CenterX,\
											m_CenterY,\
											m_SignalBlockTopLeftX,\
											m_SignalBlockTopLeftY,\
											m_SignalBlockBotRightX,\
											m_SignalBlockBotRightY,\
											m_Format.Int,\
											ColorTemperature));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SHADES_OF_GRAY_VIS_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %f);",\
																Source,\
																m_MeasInfo.Attrs.samples,\
																(float)m_HorizFieldOfView.Real,\
																(float)m_VertFieldOfView.Real,\
																(float)m_Radiance.Real,\
																m_TargetType.Int,\
																m_CenterX,\
																m_CenterY,\
																m_SignalBlockTopLeftX,\
																m_SignalBlockTopLeftY,\
																m_SignalBlockBotRightX,\
																m_SignalBlockBotRightY,\
																m_Format.Int,\
																ColorTemperature), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "diffBoresightAngleVis") == 0)
	{
		SetCameraPower(POWER_ON, Response, BufferSize);

		Source = SOURCE_VIS_ALIGN;

		IFNSIMVEO2(BORESIGHT_TV_VIS_SETUP(Source,\
										  m_MeasInfo.Attrs.samples,\
										  (float)m_HorizFieldOfView.Real,\
										  (float)m_VertFieldOfView.Real,\
										  (float)m_Radiance.Real,\
										  m_TargetType.Int,\
										  m_CenterX,\
										  m_CenterY,\
										  m_SignalBlockTopLeftX,\
										  m_SignalBlockTopLeftY,\
										  m_SignalBlockBotRightX,\
										  m_SignalBlockBotRightY,\
										  m_Format.Int,\
										  ColorTemperature,\
										  (float)m_IntensityRatio.Real));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_TV_VIS_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %d, %d, %d, %d, %f, %lf);",\
															  Source,
															  m_MeasInfo.Attrs.samples,\
															  (float)m_HorizFieldOfView.Real,\
															  (float)m_VertFieldOfView.Real,\
															  (float)m_Radiance.Real,\
															  m_TargetType.Int,\
															  m_CenterX,\
															  m_CenterY,\
															  m_SignalBlockTopLeftX,\
															  m_SignalBlockTopLeftY,\
															  m_SignalBlockBotRightX,\
															  m_SignalBlockBotRightY,\
															  m_Format.Int,\
															  ColorTemperature,\
															  (float)m_IntensityRatio.Real), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "minResolvContrastDiffVis") == 0)
	{

		int		nIdx2 = 0;
		int		*pTargetPosition;
		int		*pTargetFeature;
		float	*pStartingRadiance;

		Size = m_TargetData.Size / 2;
		pStartingRadiance = new float[Size+1];
		pTargetPosition   = new int[Size+1];
		pTargetFeature    = new int[Size+1];

		pStartingRadiance[0] = (float)Size;

		for (nIdx = 0; nIdx < Size; nIdx++)
		{
			pStartingRadiance[nIdx+1] = (float)m_TargetData.val[nIdx];
		}

		pTargetPosition[0] = Size;
		pTargetFeature[0] = Size;

		// Note: The Group 1 and Group 2 targets removed as part of TARGET UPDATE.
		//       Decision to leave targets in this loop to support legacy for now.
		for (nIdx = Size, nIdx2 = 0; (nIdx < (Size*2)) && (nIdx2 < m_TestPointCount.Int); nIdx++, nIdx2++)
		{
			if (m_TargetData.val[nIdx]      == 0.20)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP2;	pTargetFeature[nIdx2+1] = 1; }
			else if (m_TargetData.val[nIdx] == 0.28)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP2;	pTargetFeature[nIdx2+1] = 2; }
			else if (m_TargetData.val[nIdx] == 0.36)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 7; }
			else if (m_TargetData.val[nIdx] == 0.45)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 8; }
			else if (m_TargetData.val[nIdx] == 0.56)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 9; }
			else if (m_TargetData.val[nIdx] == 0.64)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 1; }
			else if (m_TargetData.val[nIdx] == 0.71)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 2; }
			else if (m_TargetData.val[nIdx] == 0.80)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 3; }
			else if (m_TargetData.val[nIdx] == 0.90)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 4; }
			else if (m_TargetData.val[nIdx] == 1.01)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 5; }
			else if (m_TargetData.val[nIdx] == 1.13)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP1;	pTargetFeature[nIdx2+1] = 6; }
			else if (m_TargetData.val[nIdx] == 1.27)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 1; }
			else if (m_TargetData.val[nIdx] == 1.43)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 2; }
			else if (m_TargetData.val[nIdx] == 1.60)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 3; }
			else if (m_TargetData.val[nIdx] == 1.80)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 4; }
			else if (m_TargetData.val[nIdx] == 2.02)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 5; }
			else if (m_TargetData.val[nIdx] == 2.26)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 6; }
			else if (m_TargetData.val[nIdx] == 2.54)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 7; }
			else if (m_TargetData.val[nIdx] == 2.85)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 8; }
			else if (m_TargetData.val[nIdx] == 3.20)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 9; }
			else if (m_TargetData.val[nIdx] == 3.59)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 10; }
			else if (m_TargetData.val[nIdx] == 4.03)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 11; }
			else if (m_TargetData.val[nIdx] == 4.53)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 12; }
			else if (m_TargetData.val[nIdx] == 5.08)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 13; }
			else if (m_TargetData.val[nIdx] == 5.70)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 14; }
			else if (m_TargetData.val[nIdx] == 6.40)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 15; }
			else if (m_TargetData.val[nIdx] == 7.18)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 16; }
			else if (m_TargetData.val[nIdx] == 8.06)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 17; }
			else if (m_TargetData.val[nIdx] == 9.05)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 18; }
			else if (m_TargetData.val[nIdx] == 10.16)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 19; }
			else if (m_TargetData.val[nIdx] == 11.40)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 20; }
			else if (m_TargetData.val[nIdx] == 12.80)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 21; }
			else if (m_TargetData.val[nIdx] == 14.37)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 22; }
			else if (m_TargetData.val[nIdx] == 16.13)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 23; }
			else if (m_TargetData.val[nIdx] == 18.10)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 24; }
			else if (m_TargetData.val[nIdx] == 20.32)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 25; }
			else if (m_TargetData.val[nIdx] == 22.81)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 26; }
			else if (m_TargetData.val[nIdx] == 25.60)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 27; }
			else if (m_TargetData.val[nIdx] == 28.74)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 28; }
			else if (m_TargetData.val[nIdx] == 32.26)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 29; }
			else if (m_TargetData.val[nIdx] == 36.20)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 30; }
			else if (m_TargetData.val[nIdx] == 40.64)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 31; }
			else if (m_TargetData.val[nIdx] == 45.62)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 32; }
			else if (m_TargetData.val[nIdx] == 51.20)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 33; }
			else if (m_TargetData.val[nIdx] == 57.47)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 34; }
			else if (m_TargetData.val[nIdx] == 64.50)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 35; }
			else if (m_TargetData.val[nIdx] == 72.40)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 36; }
			else if (m_TargetData.val[nIdx] == 81.28)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 37; }
			else if (m_TargetData.val[nIdx] == 91.23)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 38; }
			else if (m_TargetData.val[nIdx] == 102.40)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 39; }
			else if (m_TargetData.val[nIdx] == 114.95)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 40; }
			else if (m_TargetData.val[nIdx] == 129.00)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 41; }
			else if (m_TargetData.val[nIdx] == 144.80)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 42; }
			else if (m_TargetData.val[nIdx] == 162.56)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 43; }
			else if (m_TargetData.val[nIdx] == 182.47)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 44; }
			else if (m_TargetData.val[nIdx] == 204.80)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 45; }
			else if (m_TargetData.val[nIdx] == 229.90)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 46; }
			else if (m_TargetData.val[nIdx] == 258.00)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 47; }
			else if (m_TargetData.val[nIdx] == 289.65)	{ pTargetPosition[nIdx2+1] = TARG_TGTGRP07;	pTargetFeature[nIdx2+1] = 48; }
			else nIdx = 999;
			
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("Index [%d] : start rad  %.2lf : target pos %d : target feature %d", nIdx2, \
						 pStartingRadiance[nIdx2+1], pTargetPosition[nIdx2+1], pTargetFeature[nIdx2+1]), Response, BufferSize);

		}

		Source = SOURCE_VIS;

		IFNSIMVEO2(MINIMUM_RESOLVABLE_CONTRAST_VIS_SETUP(Source,\
														 m_TestPointCount.Int,\
														 pStartingRadiance,\
														 pTargetPosition,\
														 pTargetFeature));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("MINIMUM_RESOLVABLE_CONTRAST_VIS_SETUP(%d, %d, %lf, %d, %d);",\
																			 Source,\
																			 m_TestPointCount.Int,\
																			 pStartingRadiance[0],\
																			 pTargetPosition[0],\
																			 pTargetFeature[0]), Response, BufferSize);

		delete [] pStartingRadiance;
		delete [] pTargetPosition;
		delete [] pTargetFeature;
	}

	return(0);
}

//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions                                                    //
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinLaserSensors
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWinLaserSensors(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int		TrigSlope = 0;
	int		TrigSource = 0;
	int		PulseSelect = 0;
	float   period = 50.0; // not used for ATLAS purposes
	float	Pulse2Delay = 0.0;

	if ((strcmp(m_MeasInfo.cyType, "powerPCh1Laser") == 0)			||
		(strcmp(m_MeasInfo.cyType, "powerPCh2Laser") == 0)			||
		(strcmp(m_MeasInfo.cyType, "pulseEnergyCh1Laser") == 0)		||
		(strcmp(m_MeasInfo.cyType, "pulseEnergyCh2Laser") == 0)     ||
		(strcmp(m_MeasInfo.cyType, "pulseAmplStabCh1Laser") == 0)   ||
		(strcmp(m_MeasInfo.cyType, "pulseAmplStabCh2Laser") == 0)   ||
		(strcmp(m_MeasInfo.cyType, "pulseEnergyStabCh1Laser") == 0) ||
		(strcmp(m_MeasInfo.cyType, "pulseEnergyStabCh2Laser") == 0))
	{

		int	ChannelNumber = (strstr(m_MeasInfo.cyType, "Ch1") != 0) ? 1 : 2;

		TrigSlope = (m_TrigInfo.TrigExists == true) ? (m_TrigInfo.TrigSlopePos == true) ? 1 : 2 : 1;	//Set to Positive if not set.

		if (strcmp(m_TrigInfo.TrigPort, "CH1") == 0)
			TrigSource = 1;
		else if (strcmp(m_TrigInfo.TrigPort, "CH2") == 0)
			TrigSource = 2;
		else if (strcmp(m_TrigInfo.TrigPort, "EXT") == 0)
			TrigSource = 3;
		else if (ChannelNumber == 1)
			TrigSource = 1;
		else
			TrigSource = 2;

		CalculateInputScaleData(); 
		CalculateTimeBaseSetting();

		IFNSIMVEO2(PULSE_ENERGY_MEASUREMENTS_LASER_SETUP(m_MeasInfo.Attrs.samples,\
														 ChannelNumber,\
														 m_VoltScale,\
														 m_TimeBase,\
														 TrigSource,\
														 TrigSlope,\
														 (float)m_TrigInfo.TrigLevel,\
														 m_WaveLength.Int));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_ENERGY_MEASUREMENTS_LASER_SETUP(%d, %d, %d, %d, %d, %s, %.2lf, %d);",\
																			 m_MeasInfo.Attrs.samples,\
																			 ChannelNumber,\
																			 m_VoltScale,\
																			 m_TimeBase,\
																			 TrigSource,\
																			 TRIGSLOPE[TrigSlope],\
																			 m_TrigInfo.TrigLevel,\
																			 m_WaveLength.Int), Response, BufferSize);

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "PULSE_ENERGY_MEASUREMENTS_LASER_SETUP(%d, %d, %d, %d, %d, %s, %.2lf, %d);",
						  m_MeasInfo.Attrs.samples, ChannelNumber, m_VoltScale, m_TimeBase, TrigSource, TRIGSLOPE[TrigSlope],
						  m_TrigInfo.TrigLevel, m_WaveLength.Int, (char*)0));
	}
	else if ((strcmp(m_MeasInfo.cyType, "pulseWidthCh1Laser") == 0) ||
			 (strcmp(m_MeasInfo.cyType, "pulseWidthCh2Laser") == 0))
	{

		int	ChannelNumber = (strstr(m_MeasInfo.cyType, "Ch1") != 0) ? 1 : 2;

		TrigSlope = (m_TrigInfo.TrigExists == true) ? (m_TrigInfo.TrigSlopePos == true) ? 1 : 2 : 1;	//Set to Positive if not set.

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "The Channel Number is %d", ChannelNumber, (char*)0));
		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "The TrigSlope is %d", TrigSlope, (char*)0));

		if (strcmp(m_TrigInfo.TrigPort, "CH1") == 0)
			TrigSource = 1;
		else if (strcmp(m_TrigInfo.TrigPort, "CH2") == 0)
			TrigSource = 2;
		else if (strcmp(m_TrigInfo.TrigPort, "EXT") == 0)
			TrigSource = 3;
		else if (ChannelNumber == 1)
			TrigSource = 1;
		else
			TrigSource = 2;

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "The TrigSource is %d", TrigSource, (char*)0));

		CalculateInputScaleData();
		CalculateTimeBaseSetting();

		IFNSIMVEO2(PULSE_WIDTH_LASER_SETUP(m_MeasInfo.Attrs.samples,\
										   ChannelNumber,\
										   m_VoltScale,\
										   m_TimeBase,\
										   TrigSource,\
										   TrigSlope,
										   (float)m_TrigInfo.TrigLevel));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_WIDTH_LASER_SETUP(%d, %d, %d, %d, %d, %s, %lf);",\
									  m_MeasInfo.Attrs.samples,\
									  ChannelNumber,\
									  m_VoltScale,\
									  m_TimeBase,\
									  TrigSource,\
									  TRIGSLOPE[TrigSlope],\
									  (float)m_TrigInfo.TrigLevel), Response, BufferSize);

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "PULSE_WIDTH_LASER_SETUP(%d, %d, %d, %d, %d, %s, %lf);",
						  m_MeasInfo.Attrs.samples, ChannelNumber, m_VoltScale, m_TimeBase, TrigSource,
						  TRIGSLOPE[TrigSlope], m_TrigInfo.TrigLevel, (char*)0));
	}
	else if ((strcmp(m_MeasInfo.cyType, "pulsePeriodStabLaser") == 0) ||
			 (strcmp(m_MeasInfo.cyType, "prfLaser") == 0))
	{
		TrigSlope = (m_TrigInfo.TrigExists == true) ? (m_TrigInfo.TrigSlopePos == true) ? 1 : 2 : 1;	//Set to Positive if not set.

		IFNSIMVEO2(PULSE_REPETITION_FREQUENCY_LASER_SETUP(m_MeasInfo.Attrs.samples,\
														  TrigSlope,\
														  (float)m_TrigInfo.TrigLevel,\
														  (float)(m_TrigInfo.TrigDelay * 1000.0)));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_REPETITION_FREQUENCY_LASER_SETUP(%d, %s, %lf, %d);",\
																			  m_MeasInfo.Attrs.samples,\
																			  TRIGSLOPE[TrigSlope],\
																			  (float)m_TrigInfo.TrigLevel,\
																			  (int)(m_TrigInfo.TrigDelay * 1000.0)), Response, BufferSize);
	}
	else if ((strcmp(m_MeasInfo.cyType, "diffBoresightAngleLaser") == 0) ||
			 (strcmp(m_MeasInfo.cyType, "boresightAngleLaser")     == 0))
	{
		// Notes 1. diff-boresight-angle and boresight-angle call the same API in the legacy
		//          The x/y-boresight-angle or h/v-los-align-error is subtracted from reading
		//          in fetch.
		// uses laser class 1 parameter table
		IFNSIMVEO2(BORESIGHT_LASER_SETUP(m_MeasInfo.Attrs.samples,\
										 m_SignalBlockTopLeftX,\
										 m_SignalBlockTopLeftY,\
										 m_SignalBlockBotRightX,\
										 m_SignalBlockBotRightY,\
										 (float)(m_TrigInfo.TrigDelay * 1000.0),\
										 m_CameraTrigger,\
										 (float)m_IntensityRatio.Real));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_LASER_SETUP(%d, %d, %d, %d, %d, %lf, %d, %lf);",\
															 m_MeasInfo.Attrs.samples,\
															 m_SignalBlockTopLeftX,\
															 m_SignalBlockTopLeftY,\
															 m_SignalBlockBotRightX,\
															 m_SignalBlockBotRightY,\
															 (float)(m_TrigInfo.TrigDelay * 1000.0),\
															 m_CameraTrigger,\
															 (float)m_IntensityRatio.Real), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "divergenceLaser") == 0)
	{

		SetSensorStage(CAMERA_STAGE, Response, BufferSize);
		SetCameraPower(POWER_ON, Response, BufferSize);

		// uses laser class 1 parameter table
		IFNSIMVEO2(BEAM_DIVERGENCE_LASER_SETUP(m_MeasInfo.Attrs.samples,\
											   m_SignalBlockTopLeftX,\
											   m_SignalBlockTopLeftY,\
											   m_SignalBlockBotRightX,\
											   m_SignalBlockBotRightY,\
											   (float)(m_TrigInfo.TrigDelay * 1000.0),\
											   m_CameraTrigger));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BEAM_DIVERGENCE_LASER_SETUP(%d, %d, %d, %d, %d, %lf, %d);",\
																   m_MeasInfo.Attrs.samples,\
																   m_SignalBlockTopLeftX,\
																   m_SignalBlockTopLeftY,\
																   m_SignalBlockBotRightX,\
																   m_SignalBlockBotRightY,\
																   (float)(m_TrigInfo.TrigDelay * 1000.0),\
																   m_CameraTrigger), Response, BufferSize);
	}
	/////////////////////////////////////////////////////////////
	// Category II laser table measurements                    //
	/////////////////////////////////////////////////////////////
	else if (strcmp(m_MeasInfo.cyType, "receiverSensitivityLaser") == 0)
	{
		IFNSIMVEO2(RECEIVER_SENSITIVITY_LASER_SETUP(m_TrigInfoInt,\
													m_WaveLength.Int,\
													(float)(m_PowerDensity.Real * 1.0e9),\
													period,\
													(float)(m_PulseWidth.Real * 1.0e9),\
													m_TargetRange.Int,\
													(int)Pulse2Delay,\
													PulseSelect,\
													m_IntensityRatio.Int,\
													m_RangeCriteria));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("RECEIVER_SENSITIVITY_LASER_SETUP(%d, %d, %lf, %lf, %lf, %d, %d, %d, %d, %.2f);",\
																	    m_TrigInfoInt,\
																	    m_WaveLength.Int,\
																	    (float)(m_PowerDensity.Real * 1.0e9),\
																	    period,\
																	    (float)(m_PulseWidth.Real * 1.0e9),\
																	    m_TargetRange.Int,\
																	    (int)Pulse2Delay,\
																	    PulseSelect,\
																	    m_IntensityRatio.Int,
																	    m_RangeCriteria), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "rangeErrorLaser") == 0)
	{
		if (m_LastPulseRange.Exists)
		{
			Pulse2Delay = CalcPulse2Delay(m_TargetRange.Real, m_LastPulseRange.Real);
		}

		IFNSIMVEO2(RANGE_FINDER_ACCURACY_LASER_SETUP(m_TrigInfoInt,\
													 m_WaveLength.Int,\
													 (float)(m_PowerDensity.Real * 1.0e9),\
													 period,\
													 (float)(m_PulseWidth.Real * 1.0e9),\
													 m_TargetRange.Int,\
													 (int)Pulse2Delay,\
													 PulseSelect,\
													 m_IntensityRatio.Int,
													 m_RangeCriteria));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("RANGE_FINDER_ACCURACY_LASER_SETUP(%d, %d, %.2lf, %.2f, %.2lf, %d, %d, %d, %d, %.2f);",\
																		 m_TrigInfoInt,\
																		 m_WaveLength.Int,\
																		 (float)(m_PowerDensity.Real * 1.0e9),\
																		 period,\
																		 (float)(m_PulseWidth.Real * 1.0e9),\
																		 m_TargetRange.Int,\
																		 (int)Pulse2Delay,\
																		 PulseSelect,\
																		 m_IntensityRatio.Int,\
																		 m_RangeCriteria), Response, BufferSize);

	}

	return (0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinSources
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////

int CVeo2_IrWin_T::SetupVeo2_IrWinSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

 	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "m_SignalType == SOURCE", (char*)0));

	SetSourceDefaults();

	m_SourceState = ACTIVE;

	////////////////////////////////////////////////////////////////////////
	// INFRARED SOURCE ACTIONS                                            //
	////////////////////////////////////////////////////////////////////////
	
	if (m_SignalNoun == INFRARED) {
		return (SetupVeo2_IrWinIRSources(Response, BufferSize));
	}

	////////////////////////////////////////////////////////////////////////
	// LASER SOURCE ACTIONS                                               //
	////////////////////////////////////////////////////////////////////////
	else if (m_SignalNoun == LASER) {
		return (SetupVeo2_IrWinLaserSources(Response, BufferSize));
	}

	////////////////////////////////////////////////////////////////////////
	// VISIBLE LIGHT SOURCE ACTIONS                                       //
	////////////////////////////////////////////////////////////////////////
	else if (m_SignalNoun == LIGHT) {
		return (SetupVeo2_IrWinVisSources(Response, BufferSize));
	}

	////////////////////////////////////////////////////////////////////////
	// LARRS SOURCE ACTIONS                                               //
	////////////////////////////////////////////////////////////////////////
	else if (m_SignalNoun == LARRS) {
		return (SetupVeo2_IrWinLarrsSources(Response, BufferSize));
	}

    return(0);
}

//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions                                                    //
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinIRSources
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWinIRSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int		targetPosition = -1;
	double	timeRemaining;
	double  deltaTime = 0.0;
	double	elapsedTime = 0.0;
	time_t	timeStart;
	time_t  timeCurrent;

	if ((m_TargetType.Exists) && ((m_TargetType.Int >= TARG_IRBS00) && (m_TargetType.Int <= TARG_IRBS10)))
	{

		if (!m_SettleTime.Exists)
		{
			m_SettleTime.Exists = true;
			m_SettleTime.Real = 60.0;
		}

		timeRemaining = m_SettleTime.Real;
		
		if (!m_TargetType.Exists)
		{
			m_TargetType.Exists = true;
			m_TargetType.Int = TARG_IRBS01;
		}

		IFNSIMVEO2(SET_TARGET_POSITION_INITIATE(TARG_BRSGHT));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_INITIATE(%d);",\
					 TARG_BRSGHT), Response, BufferSize);

		time(&timeStart);
		do
		{
			Sleep(1000);

			IFNSIMVEO2(SET_TARGET_POSITION_FETCH(&targetPosition));

			time(&timeCurrent);
			deltaTime = difftime(timeCurrent, timeStart);
		} while ((deltaTime < 60.0) && (targetPosition != TARG_BRSGHT));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_FETCH(%d);", targetPosition),\
					 Response, BufferSize);

		IFNSIMVEO2(SET_TEMP_DIFFERENTIAL_IR_INITIATE((float)m_DifferentialTemp.Real));
		WaitForReady(timeRemaining, &elapsedTime, Response, BufferSize);

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TEMP_DIFFERENTIAL_IR_INITIATE(%lf);",\
					 m_DifferentialTemp.Real), Response, BufferSize);
	}
	////////////////////////////////////////////////////////////////////
	//                       source infrared                          //
	////////////////////////////////////////////////////////////////////
	else
	{
		if (!m_SettleTime.Exists)
		{
			m_SettleTime.Exists = true;
			m_SettleTime.Real = 10.0;
		}

		timeRemaining = m_SettleTime.Real;

		if (m_DifferentialTemp.Exists)
		{
			if (m_DiffTempError.Exists)										// 1. Set differential temp error
			{
				IFNSIMVEO2(SET_RDY_WINDOW_IR_INITIATE((float)m_DiffTempError.Real));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_RDY_WINDOW_IR_INITIATE(%lf);",\
							 m_DiffTempError.Real), Response, BufferSize);
			}

			IFNSIMVEO2(SET_TEMP_DIFFERENTIAL_IR_INITIATE((float)m_DifferentialTemp.Real));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TEMP_DIFFERENTIAL_IR_INITIATE(%lf);",\
						 m_DifferentialTemp.Real), Response, BufferSize);

			WaitForReady(timeRemaining, &elapsedTime, Response, BufferSize);
			timeRemaining -= elapsedTime;
		}
		
		if (!m_TargetType.Exists)		// 3. set target wheel - not conditional, required element in the ATLAS
		{
			m_TargetType.Exists = true;
			m_TargetType.Int = TARG_OPNAPR;
		}

		IFNSIMVEO2(SET_TARGET_POSITION_INITIATE(m_TargetType.Int));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_INITIATE(%d);", m_TargetType.Int),\
					 Response, BufferSize);

		time(&timeStart);
		do {		// verify target wheel is in correct position
			Sleep(1000);

			IFNSIMVEO2(SET_TARGET_POSITION_FETCH(&targetPosition));

			time(&timeCurrent);
			deltaTime = difftime(timeCurrent, timeStart);
		} while ((deltaTime < timeRemaining) && (targetPosition != m_TargetType.Int));

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_FETCH(%d)", targetPosition),\
					 Response, BufferSize);
	}

	return (0);
}

//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions                                                    //
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinVisSources
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWinVisSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int		targetPosition = 0;
	double	timeRemaining;
	double  deltaTime = 0.0;
	double	elapsedTime = 0.0;
	time_t	timeStart;
	time_t  timeCurrent;

	if (!m_SettleTime.Exists)
	{
		m_SettleTime.Real = 10.0;
	}

	timeRemaining = m_SettleTime.Real;

	if ((m_TargetType.Exists) && (m_TargetType.Int >= TARG_TVBS01) &&	// source multi-sensor-light
		(m_TargetType.Int <= TARG_TVBS15))
	{
		if (!m_SettleTime.Exists)
		{
			m_SettleTime.Real = 10.0;
		}

		timeRemaining = m_SettleTime.Real;

		if (m_TargetType.Exists)		// 1. set target wheel
		{
			IFNSIMVEO2(SET_TARGET_POSITION_INITIATE(m_TargetType.Int));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_INITIATE(%d);", m_TargetType.Int),\
						 Response, BufferSize);

			time(&timeStart);
			do
			{
				Sleep(1000);

				IFNSIMVEO2(SET_TARGET_POSITION_FETCH(&targetPosition));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_FETCH(%d)", targetPosition),\
							 Response, BufferSize);

				time(&timeCurrent);
				deltaTime = difftime(timeCurrent, timeStart);
			} while((deltaTime < timeRemaining) && (targetPosition != m_TargetType.Int));

			timeRemaining -= deltaTime;
		}

		if (m_Radiance.Exists)
		{
			IFNSIMVEO2(SET_RADIANCE_VIS_INITIATE((float)m_Radiance.Real));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_RADIANCE_VIS_INITIATE(%lf);",\
						 m_Radiance.Real), Response, BufferSize);

			WaitForReady(timeRemaining, &elapsedTime, Response, BufferSize);
			timeRemaining -= elapsedTime;
		}
	}
	else		// source light
	{
		if (m_TargetType.Exists)		// 1. set target wheel
		{
			IFNSIMVEO2(SET_TARGET_POSITION_INITIATE(m_TargetType.Int));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_INITIATE(%d);", m_TargetType.Int),\
						 Response, BufferSize);

			time(&timeStart);
			do
			{
				Sleep(1000);

				IFNSIMVEO2(SET_TARGET_POSITION_FETCH(&targetPosition));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TARGET_POSITION_FETCH(%d)", targetPosition),\
							 Response, BufferSize);

				time(&timeCurrent);
				deltaTime = difftime(timeCurrent, timeStart);
			} while((deltaTime < timeRemaining) && (targetPosition != m_TargetType.Int));

			timeRemaining -= deltaTime;
		}

		if (m_Radiance.Exists)		// set radiance
		{
			IFNSIMVEO2(SET_RADIANCE_VIS_INITIATE((float)m_Radiance.Real));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_RADIANCE_VIS_INITIATE(%lf);",\
						 m_Radiance.Real), Response, BufferSize);

			WaitForReady(timeRemaining, &elapsedTime, Response, BufferSize);
			timeRemaining -= elapsedTime;
		}
	}

    return(0);
}


//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions                                                    //
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinLaserSources
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWinLaserSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int		val = 0;
	float	TmpVal = 0.0;
	float	Pulse2Delay = 0.0;
	double	timeRemaining;
	double  deltaTime = 0.0;
	double	elapsedTime = 0.0;
	time_t	timeStart;
	time_t  timeCurrent;

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "m_SignalNoun == LASER - laser source", (char*)0));
	////////////////////////////////////////////////////////////////
	// Laser source command sequence                              //
	//  1. SET_SYSTEM_CONFIGURATION_INITIATE                      //
	//  2. SET_PULSE_AMPLITUDE_LASER                              //
	//  3. SET_TRIGGER_SOURCE_LASER                               //
	//   ....                                                     //
	//  n. SET_OPERATION_LASER_INITIATE(1)                        //
	////////////////////////////////////////////////////////////////

	timeRemaining = 200.0;

	m_PowerP.Real = (m_TargetRange.Exists && m_PowerDensity.Exists) ? (m_PowerDensity.Real * 1.0e9) : m_PowerP.Real;

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "m_PowerDensity.Real = %e\nm_PowerP.Real = %e",\
					  m_PowerDensity.Real, m_PowerP.Real, (char*)0));

	IFNSIMVEO2(SET_SYSTEM_CONFIGURATION_INITIATE(CONFIG_LASER));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_SYSTEM_CONFIGURATION_INITIATE(%s)", CONFIG[CONFIG_LASER]),\
				 Response, BufferSize);
	WaitForReady(timeRemaining, &elapsedTime, Response, BufferSize);

	time(&timeStart);
	do {
		Sleep(1000);

		IFNSIMVEO2(SET_SYSTEM_CONFIGURATION_FETCH(&val));

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((deltaTime < 20.0) && (val != CONFIG_LASER));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SET_SYSTEM_CONFIGURATION_FETCH configuration = %d",\
					  val, (char*)0));

	IFNSIMVEO2(SET_TRIGGER_SOURCE_LASER_INITIATE(m_TrigInfoInt));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TRIGGER_SOURCE_LASER_INITIATE(%s);", TRIGMODE[m_TrigInfoInt]),\
				 Response, BufferSize);
	
	time(&timeStart);
	do {
		Sleep(1000);

		IFNSIMVEO2(SET_TRIGGER_SOURCE_LASER_FETCH(&val));

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((deltaTime < 20.0) && (val != m_TrigInfoInt));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SET_TRIGGER_SOURCE_LASER_FETCH trigger = %d",\
					  val, (char*)0));

	if (m_TargetRange.Exists)
	{
		IFNSIMVEO2(SET_RANGE_EMULATION_LASER_INITIATE((float)m_TargetRange.Real));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_RANGE_EMULATION_LASER_INITIATE(%.1lf)", m_TargetRange.Real),\
					 Response, BufferSize);
		
		time(&timeStart);
		do {
			Sleep(1000);

			IFNSIMVEO2(SET_RANGE_EMULATION_LASER_FETCH(&TmpVal));

			time(&timeCurrent);
			deltaTime = difftime(timeCurrent, timeStart);
		} while((deltaTime < 20.0) && (TmpVal != m_TargetRange.Real));

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SET_RANGE_EMULATION_LASER_FETCH TargetRange = %.1lf",\
						  TmpVal, (char*)0));

	}

	if (m_LastPulseRange.Exists)
	{
	
		Pulse2Delay = CalcPulse2Delay(m_TargetRange.Real, m_LastPulseRange.Real);
		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "target range = %lf and last pulse = %lf",\
						  m_TargetRange.Real, m_LastPulseRange.Real, (char*)0));

		IFNSIMVEO2(SET_PULSE2_DELAY_LASER_INITIATE(Pulse2Delay));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_PULSE2_DELAY_LASER_INITIATE(%.1f nsec)", Pulse2Delay),\
					 Response, BufferSize);
		
		time(&timeStart);
		do {
			Sleep(1000);

			IFNSIMVEO2(SET_PULSE2_DELAY_LASER_FETCH(&TmpVal));

			time(&timeCurrent);
			deltaTime = difftime(timeCurrent, timeStart);
		} while((deltaTime < 20.0) && (TmpVal != Pulse2Delay));

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SET_PULSE2_DELAY_LASER_FETCH Pulse2Delay = %lf",\
						  TmpVal, (char*)0));

		IFNSIMVEO2(SELECT_LARGER_PULSE_LASER_INITIATE(0));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SELECT_LARGER_PULSE_LASER_INITIATE(0)"), Response, BufferSize);
		
		time(&timeStart);
		do {
			Sleep(1000);

			IFNSIMVEO2(SET_PULSE_PERCENTAGE_LASER_FETCH(&TmpVal));

			time(&timeCurrent);
			deltaTime = difftime(timeCurrent, timeStart);
		} while((deltaTime < 20.0) && (TmpVal != 0));

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWinLaserSources()", "out of SET_PULSE_PERCENTAGE_LASER_FETCH(%d)", val, (char*)0));
		
		IFNSIMVEO2(SET_PULSE_PERCENTAGE_LASER_INITIATE(100));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_PULSE_PERCENTAGE_LASER_INITIATE(100)"), Response, BufferSize);
		
		time(&timeStart);
		do {
			Sleep(1000);

			IFNSIMVEO2(SET_PULSE_PERCENTAGE_LASER_FETCH(&TmpVal));

			time(&timeCurrent);
			deltaTime = difftime(timeCurrent, timeStart);
		} while((deltaTime < 20.0) && (TmpVal != 100));

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SET_PULSE_PERCENTAGE_LASER_FETCH(%d)", val, (char*)0));
	}

	IFNSIMVEO2(SELECT_DIODE_LASER_FETCH(&val));		//Find out the last selected diode;

	if (val == m_WaveLength.Int) {					//If current equal to requested then need to change.
													//to allow for the requested to cool so system will go ready
		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "Diode requested equals selected %d", val, (char*)0));

		val = (m_WaveLength.Int < DIODE_1064) ? DIODE_1064 : DIODE_1570;

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "Diode requested %d selected %d new diode is %d",\
						  m_WaveLength.Int, m_WaveLength.Int, val, (char*)0));

		IFNSIMVEO2(SELECT_DIODE_LASER_INITIATE(val));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SELECT_DIODE_LASER_INITIATE(%s);", DIODES[val]), Response, BufferSize);

		WaitForReady(timeRemaining, &elapsedTime, Response, BufferSize);
	
		time(&timeStart);
		do {
			Sleep(1000);

			IFNSIMVEO2(SELECT_DIODE_LASER_FETCH(&val));

			time(&timeCurrent);
			deltaTime = difftime(timeCurrent, timeStart);
		} while((deltaTime < 20.0) && (val != m_WaveLength.Int));

		ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SELECT_DIODE_LASER_FETCH diode = %d", val, (char*)0));
	
	}

	IFNSIMVEO2(SELECT_DIODE_LASER_INITIATE(m_WaveLength.Int));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SELECT_DIODE_LASER_INITIATE(%s);", DIODES[m_WaveLength.Int]),\
				 Response, BufferSize);
	WaitForReady(timeRemaining, &elapsedTime, Response, BufferSize);
	
	time(&timeStart);
	do {
		Sleep(1000);

		IFNSIMVEO2(SELECT_DIODE_LASER_FETCH(&val));

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((deltaTime < 20.0) && (val != m_WaveLength.Int));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SELECT_DIODE_LASER_FETCH diode = %d", val, (char*)0));
	
	IFNSIMVEO2(SET_PULSE_AMPLITUDE_LASER_INITIATE((float)m_PowerP.Real));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_PULSE_AMPLITUDE_LASER_INITIATE(%lf)", m_PowerP.Real),\
				 Response, BufferSize);
	
	time(&timeStart);
	do {
		Sleep(1000);

		IFNSIMVEO2(SET_PULSE_AMPLITUDE_LASER_FETCH(&TmpVal));

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((deltaTime < 20.0) && (TmpVal != m_PowerP.Real));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SET_PULSE_AMPLITUDE_LASER_FETCH power = %lf",\
					  TmpVal, (char*)0));

	IFNSIMVEO2(SET_PULSE_PERIOD_LASER_INITIATE((float)m_Period.Real));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_PULSE_PERIOD_LASER_INITIATE(%e);", m_Period.Real),\
				 Response, BufferSize);
	
	time(&timeStart);
	do {
		Sleep(1000);

		IFNSIMVEO2(SET_PULSE_PERIOD_LASER_FETCH(&TmpVal));

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((deltaTime < 20.0) && (TmpVal != m_Period.Real));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SET_PULSE_PERIOD_LASER_FETCH period = %lf",\
					  TmpVal, (char*)0));

	IFNSIMVEO2(SET_OPERATION_LASER_INITIATE(1));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_OPERATION_LASER_INITIATE(1);"), Response, BufferSize);

	WaitForReady(timeRemaining, &elapsedTime, Response, BufferSize);
	
	time(&timeStart);
	do {
		Sleep(1000);

		IFNSIMVEO2(SET_OPERATION_LASER_FETCH(&val));

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((deltaTime < 20.0) && (val != 2));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "out of SET_OPERATION_LASER_INITIATE initiate = %d",\
					  val, (char*)0));

	if (val != 2)
	{
		MessageBox(NULL, "The VEO2 failed to initialize and has timed out.", "VEO2 Error", MB_OK | MB_ICONWARNING);
	}

    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_IrWinLarrsSources
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Fnc           int                    The Allocated FNC code
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::SetupVeo2_IrWinLarrsSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int		TmpIntVal;
	int		val	= 0;
	double  deltaTime = 0.0;
	time_t	timeStart;
	time_t  timeCurrent;

	/////////////////////////////////////////////////////////////////
	// Apply LaRRS Data                                            //
	// Program the LaRRS azimuth and elevation parameters from     //
	// LARRS.DAT.                                                  //
	/////////////////////////////////////////////////////////////////

	TmpIntVal = (m_Azimuth.Exists == 1) ? m_Azimuth.Int : m_AzPos;

	IFNSIMVEO2(SET_LARRS_AZ_LASER_INITIATE(TmpIntVal));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_LARRS_AZ_LASER_INITIATE(%d)", TmpIntVal),
								  Response, BufferSize);

	time(&timeStart);
	do
	{
		Sleep(5000);

		IFNSIMVEO2(SET_LARRS_AZ_LASER_FETCH(&val));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_LARRS_AZ_LASER_FETCH(%d);", val), 
					 Response, BufferSize);

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((val != TmpIntVal) && (deltaTime < 120.0));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWinLarrsSources()", "Azimuth = %d and deltatime = %g", val, deltaTime, (char*)0));
	TmpIntVal = m_Elevation.Exists == 1 ? m_Elevation.Int : m_ElPos;

	IFNSIMVEO2(SET_LARRS_EL_LASER_INITIATE(TmpIntVal));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_LARRS_EL_LASER_INITIATE(%d)", TmpIntVal),
				 Response, BufferSize);

	time(&timeStart);
	do
	{
		Sleep(5000);

		IFNSIMVEO2(SET_LARRS_EL_LASER_FETCH(&val));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_LARRS_EL_LASER_FETCH(%d);", val),
					 Response, BufferSize);

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((val != TmpIntVal) && (deltaTime < 120.0));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWinLarrsSources()", "Elevation = %d and deltatime = %g", val, deltaTime, (char*)0));
	TmpIntVal = m_Polarize.Exists == 1 ? m_Polarize.Int : 0;

	IFNSIMVEO2(SET_LARRS_POLARIZE_LASER_INITIATE(TmpIntVal));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_LARRS_POLARIZE_LASER_INITIATE(%d)", TmpIntVal),
								  Response, BufferSize);

	time(&timeStart);
	do
	{
		Sleep(5000);

		IFNSIMVEO2(SET_LARRS_POLARIZE_LASER_FETCH(&val));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_LARRS_POLARIZE_LASER_FETCH(%d);", val),
					 Response, BufferSize);

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);
	} while((val != TmpIntVal) && (deltaTime < 120.0));

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWinLarrsSources()", "Polarize = %d and deltatime = %g", val, deltaTime, (char*)0));

	return (0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: FetchVeo2_IrWin
//
// Purpose: Perform the Fetch action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// ===============  =====================  =====================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// ===============  =====================  =====================================
// Response         ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::FetchVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int			Status = 0;
	int			*IntReturnArray   = NULL;
	int			*IntFormattedArray = NULL;
	int			Size = 0;
	int			IntParam1 = 0;
	int			Idx = 0;
	int			Idx2 = 0;
	int			channel = 0;
	float		RealParam1 = 0.0;
	float		RealParam2 = 0.0;
	float		RealParam3 = 0.0;
	float		*FloatReturnArray = NULL;
	float		AvgPulseWidth = 0.0;
	double		*RealReturnArray  = NULL;
	char		StatusMsg[100];
	VisErrMsg	VisMeasType;
	LsrErrMsg	LsrMeasType;
	IrErrMsg	IrMeasType;

	memset(Response, '\0', BufferSize);

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchVeo2_IrWin called "), Response, BufferSize);

	////////////////////////////////////////////////////////////////////////////
	// INFRARED Signal returns
	////////////////////////////////////////////////////////////////////////////
	if (strcmp(m_MeasInfo.cyType, "losAlignErrorIR") == 0)
	{
		IFNSIMVEO2(BORESIGHT_IR_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_IR_FETCH(%f, %f, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		
		RealReturnArray = new double[2];

		if (_isnan(RealParam1))
			RealParam1 = (float)-999.9;
		if (_isnan(RealParam2))
			RealParam2 = (float)-999.9;

		RealReturnArray[0] = (double)(RealParam1 * 0.001);	// assign values and convert from milliradians to radians
		RealReturnArray[1] = (double)(RealParam2 * 0.001);

		if (Status)
		{
			IrMeasType = BoreSightIr;

			RealReturnArray[0] = FLT_MAX;
			RealReturnArray[1] = FLT_MAX;

			GetIrStatusMessage(Status, IrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin();", StatusMsg,\
						 Response, BufferSize);
		}

		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "rad", RealReturnArray,\
								 2, Response, BufferSize);
		delete [] RealReturnArray;
	}
	else if (strcmp(m_MeasInfo.cyType, "modulationTransferFunctionIR") == 0)
	{
		Size = (m_MtfFreqPoints.Int * 2) + 1;

		FloatReturnArray = new float[Size];
		RealReturnArray = new double[Size];

		IFNSIMVEO2(MODULATION_TRANSFER_FUNCTION_IR_FETCH(FloatReturnArray, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("MODULATION_TRANSFER_FUNCTION_IR_FETCH(%f..., %d);",\
					 FloatReturnArray[0], Status), Response, BufferSize);
		if (Status)
		{
			IrMeasType = ModTranFunIr;

			for (Idx = 0; Idx < Size; Idx++)
				RealReturnArray[Idx] = FLT_MAX;

			GetIrStatusMessage(Status, IrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin();", StatusMsg, Response, BufferSize);
		}

		for (Idx = 0; Idx < Size; Idx++)
			RealReturnArray[Idx] = (double)FloatReturnArray[Idx];

		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "rad",\
								(double*)RealReturnArray, Size, Response, BufferSize);

		delete [] FloatReturnArray;
		delete [] RealReturnArray;
	}
	else if (strcmp(m_MeasInfo.cyType, "distortionIR") == 0)
	{
		IFNSIMVEO2(GEOMETRIC_FIDELITY_DISTORTION_IR_FETCH(&RealParam1, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GEOMETRIC_FIDELITY_DISTORTION_IR_FETCH(%f, %d);",
					 RealParam1, Status), Response, BufferSize);
		if (Status)
		{
			IrMeasType = GeoFidDistIr;

			GetIrStatusMessage(Status, IrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin();", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "pct", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "noiseEqDiffTempIR") == 0)
	{
		IFNSIMVEO2(NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH(&RealParam1, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH(%f, %d);",
					 RealParam1, Status), Response, BufferSize);
		if (Status)
		{
			IrMeasType = NoiseEquivIr;

			GetIrStatusMessage(Status, IrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin();", StatusMsg, Response, BufferSize);
		}
		if (BufferSize > (int)(strlen(Response) + 200))
		{
			atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "degc", RealParam1, Response, BufferSize);
		}
	}
	else if (strcmp(m_MeasInfo.cyType, "uniformityIR") == 0)
	{
		IFNSIMVEO2(IMAGE_UNIFORMITY_IR_FETCH(&RealParam1, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("IMAGE_UNIFORMITY_IR_FETCH(%f, %d);",
					 RealParam1, Status), Response, BufferSize);
		if (Status)
		{
			IrMeasType = ImageUniform;

			GetIrStatusMessage(Status, IrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin();", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "pct", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "chanIntegrityIR") == 0)
	{
		Size = ((m_HorzLines / m_LinesPerChannel.Int) * 2) +2;
		IntReturnArray = new int[Size];

		IFNSIMVEO2(CHANNEL_INTEGRITY_IR_FETCH(IntReturnArray, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("CHANNEL_INTEGRITY_IR_FETCH(%d..., %d);",\
					 IntReturnArray[0], Status), Response, BufferSize);
		if (Status)
		{
			IrMeasType = ChanIntegIr;

			for (Idx = 0; Idx < Size; Idx++)
				IntReturnArray[Idx] = INT_MAX;

			GetIrStatusMessage(Status, IrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin();", StatusMsg, Response, BufferSize);
		}

		Size = IntReturnArray[0] + 2;
		IntFormattedArray = new int[Size];
		IntFormattedArray[0] = IntReturnArray[0];
		IntFormattedArray[1] = IntReturnArray[1];

		for (Idx = 2, Idx2 = 2; Idx < Size; Idx++, Idx2+=2)
			IntFormattedArray[Idx] = IntReturnArray[Idx2];

		atxmlw_IntegerArrayReturn(m_MeasInfo.cyType, "", IntFormattedArray, Size, Response, BufferSize);

		delete [] IntReturnArray;
		delete [] IntFormattedArray;
	}
	else if (strcmp(m_MeasInfo.cyType, "diffBoresightAngleIR") == 0)
	{
		IFNSIMVEO2(BORESIGHT_IR_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_IR_FETCH(%f, %f, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);

		RealReturnArray = new double[2];

		RealReturnArray[0] = (double)RealParam1;
		RealReturnArray[1] = (double)RealParam2;

		if ((m_XBoresightAngle.Exists) && (m_YBoresightAngle.Exists))
		{
			RealReturnArray[0] -= m_XBoresightAngle.Real;	// Subtract boresight to get differential
			RealReturnArray[1] -= m_YBoresightAngle.Real;
		}
		else if ((m_HorizLosAlignError.Exists) && (m_VertLosAlignError.Exists))
		{
			RealReturnArray[0] -= m_HorizLosAlignError.Real;
			RealReturnArray[1] -= m_VertLosAlignError.Real;
		}

		RealReturnArray[0] = RealReturnArray[0] * 0.001;	// convert values from milliradians to radians
		RealReturnArray[1] = RealReturnArray[1] * 0.001;

		if (Status)
		{
			IrMeasType = BoreSightIr;

			RealReturnArray[0] = FLT_MAX;
			RealReturnArray[1] = FLT_MAX;

			GetIrStatusMessage(Status, IrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin();", StatusMsg, Response, BufferSize);
		}


		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "rad",\
								(double*)RealReturnArray, 2, Response, BufferSize);

		delete [] RealReturnArray;
	}
	else if (strcmp(m_MeasInfo.cyType, "minResolvTempDiffIR") == 0)
	{
		IFNSIMVEO2(SET_TEMP_DIFFERENTIAL_IR_FETCH(&RealParam1));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TEMP_DIFFERENTIAL_IR_FETCH(%.3f);",RealParam1),\
					 Response, BufferSize);

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "degc", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "differentialTempIR") == 0)
	{
		Sleep(1000);

		IFNSIMVEO2(SET_TEMP_DIFFERENTIAL_IR_FETCH(&RealParam1));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TEMP_DIFFERENTIAL_IR_FETCH(%f);",\
					 RealParam1), Response, BufferSize);

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "degc", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "ambientTempIR") == 0)
	{
		IFNSIMVEO2(GET_TEMP_TARGET_IR_FETCH(&RealParam1));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GET_TEMP_TARGET_IR_FETCH(%f);",\
					 RealParam1), Response, BufferSize);

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "degc", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "blackbodyTempIR") == 0)
	{
		IFNSIMVEO2(SET_TEMP_ABSOLUTE_IR_FETCH(&RealParam1));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_TEMP_ABSOLUTE_IR_FETCH(%f);",\
					 RealParam1), Response, BufferSize);

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "degc", RealParam1, Response, BufferSize);
	}

	////////////////////////////////////////////////////////////////////////////
	// LASER Signal Return
	////////////////////////////////////////////////////////////////////////////
	else if ((strcmp(m_MeasInfo.cyType, "pulseEnergyCh1Laser") == 0) ||
			 (strcmp(m_MeasInfo.cyType, "pulseEnergyCh2Laser") == 0))
	{
		IFNSIMVEO2(PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(%f, %f, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		if (Status)
		{
			LsrMeasType = PulseEngLsr;
			RealParam2 = FLT_MAX;

			GetLsrStatusMessage(Status, LsrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "j", (RealParam2 * m_Atten), Response, BufferSize);
	}
	else if ((strcmp(m_MeasInfo.cyType, "pulseEnergyStabCh1Laser") == 0) ||
		     (strcmp(m_MeasInfo.cyType, "pulseEnergyStabCh2Laser") == 0))
	{
		IFNSIMVEO2(PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(%f, %f, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		if (Status)
		{
			LsrMeasType = PulseEngLsr;
			RealParam1 = FLT_MAX;

			GetLsrStatusMessage(Status, LsrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "pct", RealParam1, Response, BufferSize);
	}
	else if ((strcmp(m_MeasInfo.cyType, "powerPCh1Laser") == 0) ||
	         (strcmp(m_MeasInfo.cyType, "powerPCh2Laser") == 0))
	{
		IFNSIMVEO2(PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(%f, %f, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		if (Status)
		{
			LsrMeasType = PulseEngLsr;
			RealParam2 = FLT_MAX;

			GetLsrStatusMessage(Status, LsrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		AvgPulseWidth = GetAveragePulseWidth(channel, Response, BufferSize);	// Get average pulse width
		RealParam2 = (AvgPulseWidth > 0.0) ? (RealParam2 / AvgPulseWidth) : RealParam2;

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "W", RealParam2, Response, BufferSize);
	}
	else if ((strcmp(m_MeasInfo.cyType, "pulseAmplStabCh1Laser") == 0) ||
	         (strcmp(m_MeasInfo.cyType, "pulseAmplStabCh2Laser") == 0))
	{
		IFNSIMVEO2(PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(%f, %f, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		if (Status)
		{
			LsrMeasType = PulseEngLsr;
			RealParam1 = FLT_MAX;

			GetLsrStatusMessage(Status, LsrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "pct", RealParam1, Response, BufferSize);
	}
	else if ((strcmp(m_MeasInfo.cyType, "pulseWidthCh1Laser") == 0) ||
			 (strcmp(m_MeasInfo.cyType, "pulseWidthCh2Laser") == 0))
	{
		IFNSIMVEO2(PULSE_WIDTH_LASER_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_WIDTH_LASER_FETCH(%lf, %lf, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		if (Status)
		{
			LsrMeasType = PulseWidLsr;
			RealParam1 = FLT_MAX;

			GetLsrStatusMessage(Status, LsrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "sec", fabs((double)RealParam1), Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "prfLaser") == 0)
	{
		IFNSIMVEO2(PULSE_REPETITION_FREQUENCY_LASER_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_REPETITION_FREQUENCY_LASER_FETCH(%e, %e, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);

		RealParam2 = (RealParam2 != 0.0) ? (float)(1.0 / RealParam2) : RealParam2;

		if (Status)
		{
			LsrMeasType = PulseRepLsr;
			RealParam1 = FLT_MAX;

			GetLsrStatusMessage(Status, LsrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "hz", RealParam2, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "pulsePeriodStabLaser") == 0)
	{
		IFNSIMVEO2(PULSE_REPETITION_FREQUENCY_LASER_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_REPETITION_FREQUENCY_LASER_FETCH(%e, %e, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		if (Status)
		{
			LsrMeasType = PulseRepLsr;
			RealParam2 = FLT_MAX;

			GetLsrStatusMessage(Status, LsrMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "pct", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "diffBoresightAngleLaser") == 0)
	{
		IFNSIMVEO2(BORESIGHT_LASER_FETCH(&RealParam1, &RealParam2, &RealParam3, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_LASER_FETCH(%lf, %lf, %lf, %d);",\
					 RealParam1, RealParam2, RealParam3, Status), Response, BufferSize);

		RealReturnArray = new double[2];
		RealReturnArray[0] = (double)RealParam1;
		RealReturnArray[1] = (double)RealParam2;

		// IRWindows uses FOV_X = 320mRad, FOV_Y = 256mRad, Internal Camera actual is 12.16,9.728 
        // IRWindows returned measurements are from upper left pixel (0,0)
		// Convert to offset from Center-X,Y and actual Camera FOV

		RealReturnArray[0] = (RealReturnArray[0] - 160) * 0.038;
		RealReturnArray[1] = (RealReturnArray[1] - 128) * 0.038;

		if ((m_XBoresightAngle.Exists) && (m_YBoresightAngle.Exists))
		{
			RealReturnArray[0] -= m_XBoresightAngle.Real;	// Subtract boresight to get differential
			RealReturnArray[1] -= m_YBoresightAngle.Real;
		}
		else if ((m_HorizLosAlignError.Exists) && (m_VertLosAlignError.Exists))
		{
			RealReturnArray[0] -= m_HorizLosAlignError.Real;
			RealReturnArray[1] -= m_VertLosAlignError.Real;
		}

		RealReturnArray[0] = RealReturnArray[0] * 0.001;	// convert from milliradians to radians
		RealReturnArray[1] = RealReturnArray[1] * 0.001;

		if (Status)
		{
			RealReturnArray[0] = FLT_MAX;
			RealReturnArray[1] = FLT_MAX;

			GetCat1LaserStatusMessage(Status, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "rad", RealReturnArray, 2, Response, BufferSize);

		delete [] RealReturnArray;
	}
	else if (strcmp(m_MeasInfo.cyType, "divergenceLaser") == 0)
	{

		ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "seein if BEAM_DIVERGENCE_LASER_FETCH != NULL", (char*)0));

		if ((!BEAM_DIVERGENCE_LASER_FETCH) & (!m_Sim))
		{
			ATXMLW_ERROR(-1, "FetchVeo2_IrWin()",\
						 "BEAM_DIVERGENCE_LASER_FETCH bad function pointer", Response, BufferSize);
			return -1;
		}

		IFNSIMVEO2(BEAM_DIVERGENCE_LASER_FETCH(&RealParam1, &Status));
		ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "RealParam1 = %f", RealParam1, (char*)0));
		ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "Status = %d", Status, (char*)0));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BEAM_DIVERGENCE_LASER_FETCH(%f, %d);",\
					 RealParam1, Status), Response, BufferSize);

		RealParam1 = (float)(RealParam1 * 0.001);	// convert from milliradians to radians
		ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "RealParam1 = %f", RealParam1, (char*)0));

		// Conversion from FOV used for measurements to actual Camera FOV
        // IRWindows uses FOV_X = 320mRad, FOV_Y = 256mRad, Internal Camera actual is 12.16,9.728 

		RealParam1 = (float)(RealParam1 * 0.038);
		ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "RealParam1 = %f", RealParam1, (char*)0));

		if (Status)
		{
			RealParam1 = FLT_MAX;

			GetCat1LaserStatusMessage(Status, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		SetSensorStage(STAGE_NONE, Response, BufferSize);
		SetCameraPower(POWER_OFF, Response, BufferSize);

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "rad", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "boresightAngleLaser") == 0)
	{
		IFNSIMVEO2(BORESIGHT_LASER_FETCH(&RealParam1, &RealParam2, &RealParam3, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_LASER_FETCH(%lf, %lf, %lf, %d);",\
					 RealParam1, RealParam2, RealParam3, Status), Response, BufferSize);

		RealReturnArray = new double[2];
		RealReturnArray[0] = (double)RealParam1;
		RealReturnArray[1] = (double)RealParam2;

        // IRWindows uses FOV_X = 320mRad, FOV_Y = 256mRad, Internal Camera actual is 12.16,9.728 
        // IRWindows returned measurements are from upper left pixel (0,0)
		// Convert to offset from Center-X,Y and actual Camera FOV

		RealReturnArray[0] = (RealReturnArray[0] - 160) * 0.038;
		RealReturnArray[1] = (RealReturnArray[1] - 128) * 0.038;

		RealReturnArray[0] = RealReturnArray[0] * 0.001;	// Assign value and convert from milliradians to radians
		RealReturnArray[1] = RealReturnArray[1] * 0.001;

		if (Status)
		{
			RealReturnArray[0] = FLT_MAX;
			RealReturnArray[1] = FLT_MAX;

			GetCat1LaserStatusMessage(Status, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "rad", RealReturnArray, 2, Response, BufferSize);

		delete [] RealReturnArray;
	}
	else if (strcmp(m_MeasInfo.cyType, "receiverSensitivityLaser") == 0)
	{
		IFNSIMVEO2(RECEIVER_SENSITIVITY_LASER_FETCH(&RealParam1, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("RECEIVER_SENSITIVITY_LASER_FETCH(%f, %d);",\
					 RealParam1, Status), Response, BufferSize);
		if (Status)
		{
			RealParam1 = FLT_MAX;

			GetCat2LaserStatusMessage(Status, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "pct", RealParam2, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "rangeErrorLaser") == 0)
	{
		IFNSIMVEO2(RANGE_FINDER_ACCURACY_LASER_FETCH(&RealParam1, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("RANGE_FINDER_ACCURACY_LASER_FETCH(%f, %d);",\
					 RealParam1, Status), Response, BufferSize);
		if (Status)
		{
			RealParam1 = FLT_MAX;

			GetCat2LaserStatusMessage(Status, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "m", RealParam2, Response, BufferSize);
	}

	////////////////////////////////////////////////////////////////////////////
	// LIGHT Sensor Actions
	////////////////////////////////////////////////////////////////////////////
	else if (strcmp(m_MeasInfo.cyType, "losAlignErrorVis") == 0)
	{
		IFNSIMVEO2(BORESIGHT_TV_VIS_FETCH(&RealParam1, &RealParam2, &RealParam3, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_TV_VIS_FETCH(%f, %f, %f, %d);", \
					 RealParam1, RealParam2, RealParam3, Status), Response, BufferSize);

		RealReturnArray = new double[2];
		RealReturnArray[0] = (double)RealParam1; 
		RealReturnArray[1] = (double)RealParam2;

		RealReturnArray[0] = RealReturnArray[0] * 0.001;	// convert from mrad to rad
		RealReturnArray[1] = RealReturnArray[1] * 0.001;

		if (Status)
		{
			VisMeasType = BoreSightTv;

			RealReturnArray[0] = FLT_MAX;
			RealReturnArray[1] = FLT_MAX;

			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "rad", RealReturnArray, 2, Response, BufferSize);
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("atxmlw_DoubleArrayReturn(%s, rad, [%lf, %lf], 2, Response, BufferSize",\
					 m_MeasInfo.cyType, RealReturnArray[0], RealReturnArray[1]),	Response, BufferSize);

		delete [] RealReturnArray;

		Sleep(1000);
		SetCameraPower(POWER_OFF, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "modulationTransferFunctionVis") == 0)
	{
		Size = (m_MtfFreqPoints.Int * 2) + 1;

		FloatReturnArray = new float[Size];
		RealReturnArray = new double[Size];

		ATXMLW_DEBUG(5, atxmlw_FmtMsg("Allocating memory for %d readings", Size), Response, BufferSize);

		IFNSIMVEO2(MODULATION_TRANSFER_FUNCTION_VIS_FETCH(FloatReturnArray, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("MODULATION_TRANSFER_FUNCTION_VIS_FETCH([%f, %f, %f,...], %d);",\
					 FloatReturnArray[0], FloatReturnArray[1], FloatReturnArray[2], Status), Response, BufferSize);

		if (Status)
		{
			VisMeasType = ModTranFunVi;

			for (Idx = 1; Idx < Size; Idx++) {
				RealReturnArray[Idx] = FLT_MAX;
			}

			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		for (Idx = 0; Idx < Size; Idx++) {
			RealReturnArray[Idx] = (double)FloatReturnArray[Idx];
		}

		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "rad",\
								(double*)RealReturnArray, Size, Response, BufferSize);

		delete [] FloatReturnArray;
		delete [] RealReturnArray;

		Sleep(1000);
	}
	else if (strcmp(m_MeasInfo.cyType, "distortionVis") == 0)
	{
		IFNSIMVEO2(GEOMETRIC_FIDELITY_DISTORTION_VIS_FETCH(&RealParam1, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GEOMETRIC_FIDELITY_DISTORTION_VIS_FETCH(%lf, %d);",\
					 RealParam1, Status), Response, BufferSize);
		if (Status)
		{
			VisMeasType = GeoFidDistVi;
			RealParam1 = FLT_MAX;

			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "pct", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "uniformityVis") == 0)
	{
		IFNSIMVEO2(CAMERA_UNIFORMITY_VIS_FETCH(&RealParam1, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("CAMERA_UNIFORMITY_VIS_FETCH(%lf, %d);",
					 RealParam1, Status), Response, BufferSize);
		if (Status)
		{
			VisMeasType = CameraUnifrm;

			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin();", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "pct", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "cameraGainVis") == 0)
	{
		IFNSIMVEO2(GAIN_VIS_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GAIN_VIS_FETCH(%lf, %lf, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		if (Status)
		{
			VisMeasType = GainVisible;
			RealParam1 = FLT_MAX;

			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "uw/cm2-sr", RealParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "dynamicRangeVis") == 0)
	{
		IFNSIMVEO2(GAIN_VIS_FETCH(&RealParam1, &RealParam2, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GAIN_VIS_FETCH(%lf, %lf, %d);",\
					 RealParam1, RealParam2, Status), Response, BufferSize);
		if (Status)
		{
			VisMeasType = GainVisible;
			RealParam2 = FLT_MAX;

			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerDoubleReturn(m_MeasInfo.cyType, "dB", RealParam2, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "grayScaleResolutionVis") == 0)
	{
		IFNSIMVEO2(SHADES_OF_GRAY_VIS_FETCH(&IntParam1, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SHADES_OF_GRAY_VIS_FETCH(%d, %d);",\
					 IntParam1, RealParam2, Status), Response, BufferSize);

		if (Status)
		{
			VisMeasType = ShadeOfGray;
			IntParam1 = 0;

			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_ScalerIntegerReturn(m_MeasInfo.cyType, "", IntParam1, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "diffBoresightAngleVis") == 0)
	{

		ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "In diffBoresightAngleVis part", (char*)0));
		
		if (BORESIGHT_TV_VIS_FETCH == NULL) {
			ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "Call to BORE is NULL", (char*)0));
		}
	
		IFNSIMVEO2(BORESIGHT_TV_VIS_FETCH(&RealParam1, &RealParam2, &RealParam3, &Status));
		ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "Just Called BORE", (char*)0));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("BORESIGHT_TV_VIS_FETCH(%f, %f, %f, %d);", \
					 RealParam1, RealParam2, RealParam3, Status), Response, BufferSize);

		RealReturnArray = new double[2];
		RealReturnArray[0] = (double)RealParam1; 
		RealReturnArray[1] = (double)RealParam2;

		if ((m_XBoresightAngle.Exists) && (m_YBoresightAngle.Exists))
		{
			RealReturnArray[0] -= m_XBoresightAngle.Real;	// Subtract boresight to get differential
			RealReturnArray[1] -= m_YBoresightAngle.Real;
		}
		else if ((m_HorizLosAlignError.Exists) && (m_VertLosAlignError.Exists))
		{
			ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "Horizonal and Vertical", (char*)0));
			RealReturnArray[0] -= m_HorizLosAlignError.Real;
			RealReturnArray[1] -= m_VertLosAlignError.Real;
		}

		RealReturnArray[0] = RealReturnArray[0] * 0.001;	// convert from mrad to rad
		RealReturnArray[1] = RealReturnArray[1] * 0.001;

		ISDODEBUG(dodebug(0, "FetchVeo2_IrWin()", "Status = %d", Status, (char*)0));

		if (Status)
		{
			VisMeasType = BoreSightTv;

			RealReturnArray[0] = FLT_MAX;
			RealReturnArray[1] = FLT_MAX;

			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "rad", RealReturnArray, 2, Response, BufferSize);
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("atxmlw_DoubleArrayReturn(%s, rad, [%lf, %lf], 2, Response, BufferSize",\
					 m_MeasInfo.cyType, RealReturnArray[0], RealReturnArray[1]),	Response, BufferSize);

		delete [] RealReturnArray;

		Sleep(1000);

		SetCameraPower(POWER_OFF, Response, BufferSize);
	}
	else if (strcmp(m_MeasInfo.cyType, "minResolvContrastDiffVis") == 0)
	{
		Size = 3;
		RealReturnArray = new double[Size];
		FloatReturnArray = new float[Size];

		IFNSIMVEO2(MINIMUM_RESOLVABLE_CONTRAST_VIS_FETCH(FloatReturnArray, &Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("MINIMUM_RESOLVABLE_CONTRAST_VIS_FETCH([%.1f, %.4f, %.4f], %x);",\
					 FloatReturnArray[0], FloatReturnArray[1], FloatReturnArray[2], Status), Response, BufferSize);
		if (Status)
		{
			VisMeasType = MinResolCont;
			GetVisStatusMessage(Status, VisMeasType, StatusMsg);
			ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
		}

		for (Idx = 0; Idx < Size; Idx++)
			RealReturnArray[Idx] = (double)FloatReturnArray[Idx];

		atxmlw_DoubleArrayReturn(m_MeasInfo.cyType, "", RealReturnArray, Size, Response, BufferSize);

		delete [] RealReturnArray;
		delete [] FloatReturnArray;
	}

    return(0);
}



////////////////////////////////////////////////////////////////////////////////
// Function: DisableVeo2_IrWin
//
// Purpose: Perform the Open action for this driver instance
//
// Input Parameters
// Parameter	Type			       Purpose
// ===========  =====================  =========================================
// 
// Output Parameters
// Parameter	Type			       Purpose
// ===========  =====================  =========================================
// Response     ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::DisableVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	memset(Response, '\0', BufferSize);

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenVeo2_IrWin called "), Response, BufferSize);

    return(0);
}


////////////////////////////////////////////////////////////////////////////////
// Function: EnableVeo2_IrWin
//
// Purpose: Perform the Close action for this driver instance
//
// Input Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// 
// Output Parameters
// Parameter	 Type			        Purpose
// ============  =====================  ========================================
// Response      ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::EnableVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	char FunctionName[60] = "";

	memset(Response, '\0', BufferSize);

	ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableVeo2_IrWin called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "EnableVeo2_IrWin()", "Entering the Enable Function", (char*)0));

	INITIATE = NULL;

	if (m_SignalType == SENSOR)
	{
		if (strcmp(m_MeasInfo.cyType, "losAlignErrorIR") == 0)
		{
			if ((m_TargetType.Int == TARG_BRSGHT) || (m_TargetType.Int == TARG_OPNAPR))
			{
				strcpy(FunctionName, "BORESIGHT_IR_INITIATE");
			}
			else if ((m_TargetType.Int >= TARG_IRBS00) && (m_TargetType.Int <= TARG_IRBS10))
			{
				ATXMLW_ERROR(-1, "EnableVeo2_IrWin();", \
							 "BORESIGHT IR with IRBS not implemented at this time",\
							 Response, BufferSize);
			}
		}
		else if (strcmp(m_MeasInfo.cyType, "modulationTransferFunctionIR") == 0)
		{
			strcpy(FunctionName, "MODULATION_TRANSFER_FUNCTION_IR_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "distortionIR") == 0)
		{
			strcpy(FunctionName, "GEOMETRIC_FIDELITY_DISTORTION_IR_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "noiseEqDiffTempIR") == 0)
		{
			strcpy(FunctionName, "NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "uniformityIR") == 0)
		{
			strcpy(FunctionName, "IMAGE_UNIFORMITY_IR_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "chanIntegrityIR") == 0)
		{
			strcpy(FunctionName, "CHANNEL_INTEGRITY_IR_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "diffBoresightAngleIR") == 0)
		{
			strcpy(FunctionName, "BORESIGHT_IR_INITIATE");
		}
		else if ((strcmp(m_MeasInfo.cyType, "blackbodyTempIR")     == 0) ||
                 (strcmp(m_MeasInfo.cyType, "differentialTempIR")  == 0) ||
                 (strcmp(m_MeasInfo.cyType, "minResolvTempDiffIR") == 0))
		{
			strcpy(FunctionName, "UNKNOWN");
		}
		else if (strcmp(m_MeasInfo.cyType, "ambientTempIR") == 0)
		{
			strcpy(FunctionName, "UNKNOWN");

			IFNSIMVEO2(GET_TEMP_TARGET_IR_INITIATE());
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("GET_TEMP_TARGET_IR_INITIATE();"),\
						 Response, BufferSize);
		}
		else if ((strcmp(m_MeasInfo.cyType, "powerPCh1Laser")          == 0) ||
				 (strcmp(m_MeasInfo.cyType, "powerPCh2Laser")          == 0) ||
                 (strcmp(m_MeasInfo.cyType, "pulseEnergyCh1Laser")     == 0) ||
				 (strcmp(m_MeasInfo.cyType, "pulseEnergyCh2Laser")     == 0) ||
				 (strcmp(m_MeasInfo.cyType, "pulseAmplStabCh1Laser")   == 0) ||
				 (strcmp(m_MeasInfo.cyType, "pulseAmplStabCh2Laser")   == 0) ||
		         (strcmp(m_MeasInfo.cyType, "pulseEnergyStabCh1Laser") == 0) ||
				 (strcmp(m_MeasInfo.cyType, "pulseEnergyStabCh2Laser") == 0))
		{
			strcpy(FunctionName, "PULSE_ENERGY_MEASUREMENTS_LASER_INITIATE");
		}
		else if ((strcmp(m_MeasInfo.cyType, "pulseWidthCh1Laser") == 0) ||
		         (strcmp(m_MeasInfo.cyType, "pulseWidthCh2Laser") == 0))
		{
			strcpy(FunctionName, "PULSE_WIDTH_LASER_INITIATE");
		}
		else if ((strcmp(m_MeasInfo.cyType, "prfLaser")             == 0) ||
                 (strcmp(m_MeasInfo.cyType, "pulsePeriodStabLaser") == 0))
		{
			strcpy(FunctionName, "PULSE_REPETITION_FREQUENCY_LASER_INITIATE");
		}
		else if ((strcmp(m_MeasInfo.cyType, "boresightAngleLaser")     == 0) ||
                 (strcmp(m_MeasInfo.cyType, "diffBoresightAngleLaser") == 0))
		{
			strcpy(FunctionName, "BORESIGHT_LASER_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "divergenceLaser") == 0)
		{
			strcpy(FunctionName, "BEAM_DIVERGENCE_LASER_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "autocollimationErrorLaser") == 0)
		{
			strcpy(FunctionName, "AUTOCOLLIMATE_LASER_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "receiverSensitivityLaser") == 0)
		{
			strcpy(FunctionName, "RECEIVER_SENSITIVITY_LASER_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "rangeErrorLaser") == 0)
		{
			strcpy(FunctionName, "RANGE_FINDER_ACCURACY_LASER_INITIATE");
			ISDODEBUG(dodebug(0, "EnableVeo2_IrWin()", "RANGE_FINDER_ACCURACY_LASER_INITIATE called", (char*)0));
		}
		else if ((strcmp(m_MeasInfo.cyType, "losAlignErrorVis")      == 0) ||
                 (strcmp(m_MeasInfo.cyType, "diffBoresightAngleVis") == 0))
		{
			strcpy(FunctionName, "BORESIGHT_TV_VIS_INITIATE");
		}
		else if ((strcmp(m_MeasInfo.cyType, "cameraGainVis")   == 0) ||
                 (strcmp(m_MeasInfo.cyType, "dynamicRangeVis") == 0))
		{
			strcpy(FunctionName, "GAIN_VIS_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "modulationTransferFunctionVis") == 0)
		{
			strcpy(FunctionName, "MODULATION_TRANSFER_FUNCTION_VIS_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "distortionVis") == 0)
		{
			strcpy(FunctionName, "GEOMETRIC_FIDELITY_DISTORTION_VIS_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "uniformityVis") == 0)
		{
			strcpy(FunctionName, "CAMERA_UNIFORMITY_VIS_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "grayScaleResolutionVis") == 0)
		{
			strcpy(FunctionName, "SHADES_OF_GRAY_VIS_INITIATE");
		}
		else if (strcmp(m_MeasInfo.cyType, "minResolvContrastDiffVis") == 0)
		{
			strcpy(FunctionName, "MINIMUM_RESOLVABLE_CONTRAST_VIS_INITIATE");
		}
		
		if (strcmp(FunctionName, "UNKNOWN") != 0)
		{
			ISDODEBUG(dodebug(0, "EnableVeo2_IrWin()", "%s(); GetProcAddress", FunctionName, (char*)0));
			INITIATE = (TN_INITIATE)GetProcAddress(m_Handle, FunctionName);
			ISDODEBUG(dodebug(0, "EnableVeo2_IrWin()", "%s(); GotProcAddress", FunctionName, (char*)0));

			if ((INITIATE == NULL) && (m_Sim == false))
			{
				ATXMLW_ERROR(-1, "EnableVeo2_IrWin()",\
							 atxmlw_FmtMsg("%s bad function pointer", FunctionName),\
							 Response, BufferSize);
				return 0;
			}

			ISDODEBUG(dodebug(0, "EnableVeo2_IrWin()", "%s(); doing the initiate", FunctionName, (char*)0));
			IFNSIMVEO2(INITIATE());

			ISDODEBUG(dodebug(0, "EnableVeo2_IrWin()", "%s(); happened", FunctionName, (char*)0));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%s();", FunctionName), Response, BufferSize);
			Sleep(1000);
		}

	}
    
    return(0);
}


////////////////////////////////////////////////////////////////////////////////
// Function: ErrorVeo2_IrWin
//
// Purpose: Query Veo2_IrWin for the error text and send to WRTS
//
// Input Parameters
// Parameter   Type			          Purpose
// ==========  =====================  ==========================================
// Status      int                    Error code returned from driver
// 
// Output Parameters
// Parameter   Type			          Purpose
// ==========  =====================  ==========================================
// Response    ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int  CVeo2_IrWin_T::ErrorVeo2_IrWin(int Status,
                                  ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
     char     Msg[MAX_MSG_SIZE];

	memset(Response, '\0', BufferSize);
	memset(Msg, '\0', MAX_MSG_SIZE);

	if (!m_Sim)
	{
		if (GET_STATUS_BYTE_MESSAGE_INITIATE == NULL)
		{
			ATXMLW_ERROR(-1, "StatusVeo2_IrWin()", \
						 "GET_STATUS_BYTE_MESSAGE_INITIATE invalid function pointer",\
						 Response, BufferSize);
			return 0;
		}
		if (GET_STATUS_BYTE_MESSAGE_FETCH == NULL)
		{
			ATXMLW_ERROR(-1, "StatusVeo2_IrWin()", \
						 "GET_STATUS_BYTE_MESSAGE_FETCH invalid function pointer",\
						 Response, BufferSize);
			return 0;
		}
	}

    if(Status)
    {
		IFNSIMVEO2(GET_BIT_DATA_FETCH(&Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GET_BIT_DATA_FETCH(%d);", Status), Response, BufferSize);

		GetErrorMessage(Status, Msg);

        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",\
                             Status, Msg);
    }

	Status = 0;		// Retrieve any pending errors in the device

	do
	{
		IFNSIMVEO2(GET_BIT_DATA_INITIATE());
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GET_BIT_DATA_INITIATE();"), Response, BufferSize);

		Sleep(1000);

		IFNSIMVEO2(GET_BIT_DATA_FETCH(&Status));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("GET_BIT_DATA_FETCH(%d);", Status), Response, BufferSize);

		GetErrorMessage(Status, Msg);

        if(Status)
        {
	        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",\
								 Status, Msg);
        }
	} while(Status);


    return(Status);
}


////////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoVeo2_IrWin
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// 
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_IrWin_T::GetStmtInfoVeo2_IrWin(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
   int		Status = 0;
    char	Name[ATXMLW_MAX_NAME];
    char	*NextName;
    char	*InputNames;

	memset(Response, '\0', BufferSize);

	ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "SignalDescription is \n%s", SignalDescription, (char*)0));

    if ((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)) != 0)
         return(Status);

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);

    if ((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]",
                     m_SignalName, m_SignalElement), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "Signal Name and Signal Element [%s] [%s]",\
						  m_SignalName, m_SignalElement, (char*)0));

    strncpy(Name, m_SignalName, ATXMLW_MAX_NAME);

	if (strstr((char *)SignalDescription, "Measure name=") != NULL)		// sensor signals
	{

		m_SignalType = SENSOR;

		if(atxmlw_Get1641SignalIn(m_SignalDescription, &InputNames))		//  Get Input signal "Port" names
		{
		    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, InputNames),
		                 Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "WrapGSI-Found %s In [%s]",\
							  Name, InputNames, (char*)0));
		}
		if ((atxmlw_Get1641StdMeasChar(m_SignalDescription, Name, &m_MeasInfo)))	//  Get Measurement Type
		{
		    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s MeasChar %d", Name, m_MeasInfo.StdType),
		                 Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "WrapGSI-Found %s MeasChar %d",\
							  Name, m_MeasInfo.StdType, (char*)0));
		}
		if(ISSTRATTR("Sync",&NextName))		//  Get Trigger Info from SYNC on Measure
		{
		    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Sync [%s]", Name, NextName),
		                 Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "WrapGSI-Found %s Sync [%s]",\
							  Name, NextName, (char*)0));

		    if ((atxmlw_Get1641StdTrigChar(m_SignalDescription, NextName, InputNames, &m_TrigInfo)))
		        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Trig True for [%s]", NextName),
		                     Response, BufferSize);
				ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "WrapGSI-Found Trig True for [%s]", NextName, (char*)0));
		}
		if(ISSTRATTR("Gate", &NextName))		//  Get Gate Info from GATE on Measure
		{
		    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Gate [%s]", Name, NextName),
		                 Response, BufferSize);

		    if ((atxmlw_Get1641StdGateChar(m_SignalDescription, NextName, InputNames, &m_GateInfo)))
		        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Gate True for [%s]", NextName),
                              Response, BufferSize);
		}
		if(ISSTRATTR("As", &NextName))		// Get Signal Characteristics from As chain
		{
		    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]", Name, NextName),
		                 Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "WrapGSI-Found %s As [%s]", Name, NextName, (char*)0));

		    if ((GetSignalChar(NextName, Response, BufferSize)))
		        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", NextName),
		                     Response, BufferSize);
				ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "WrapGSI-Found Signal Chars for [%s]", NextName, (char*)0));
		}
	    if(ISSTRATTR("In", &NextName))		// Get Signal Conditioning from the In chain
	    {
	        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, NextName),
	                     Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "WrapGSI-Found %s In [%s]", Name, NextName, (char*)0));

	        if ((GetSignalCond(NextName, InputNames, Response, BufferSize)))
	            ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", NextName),
	                         Response, BufferSize);
				ISDODEBUG(dodebug(0, "GetStmtInfoVeo2_IrWin()", "WrapGSI-Found Signal Chars for [%s]", NextName, (char*)0));
	    }
	}
	else		// source signals
	{
		m_SignalType = SOURCE;

		if(atxmlw_Get1641SignalIn(m_SignalDescription, &InputNames))
		{
		    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, InputNames),
		                 Response, BufferSize);
			if (strstr(InputNames, "INT") != NULL)
			{
				m_TrigInfo.TrigExists = true;
				m_TrigInfoInt = LASER_TRIG_FREE_RUN;
			}
			else if (strstr(InputNames, "EXT") != NULL)
			{
				m_TrigInfo.TrigExists = true;
				m_TrigInfoInt = LASER_TRIG_EXT;
			}
			else if (strstr(InputNames, "LASR") != NULL)
			{
				m_TrigInfo.TrigExists = true;
				m_TrigInfoInt = LASER_TRIG_LASR;
			}
			else if (strstr(InputNames, "AUTO") != NULL)
			{
				m_TrigInfo.TrigExists = true;
				m_TrigInfoInt = LASER_TRIG_FREE_RUN;
			}
		}
		if ((GetSignalChar(Name, Response, BufferSize)))
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", Name),
		                 Response, BufferSize);

		//  Get Trigger Info from SYNC on Measure
		if(ISSTRATTR("Sync",&NextName))
		{
		    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Sync [%s]", Name, NextName),
		                 Response, BufferSize);

		    if ((atxmlw_Get1641StdTrigChar(m_SignalDescription, NextName, InputNames, &m_TrigInfo)))
		        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Trig True for [%s]", NextName),
		                     Response, BufferSize);
		}

	}
	atxmlw_Close1641Xml(&m_SignalDescription);

    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateVeo2_IrWin
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// 
// Output Parameters
// Parameter		  Type			         Purpose
// =================  ===================    ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::InitPrivateVeo2_IrWin(void)
{
	int			Idx = 0;
	static int	FirstPass = 1;

	ClearAttributeStruct(m_Azimuth);
	ClearAttributeStruct(m_Delay);
	ClearAttributeStruct(m_DifferentialTemp);
	ClearAttributeStruct(m_DiffTempError);
	ClearAttributeStruct(m_DiffTempInterval);
	ClearAttributeStruct(m_DiffTempStart);
	ClearAttributeStruct(m_DiffTempStop);
	ClearAttributeStruct(m_DistPosCount);

	m_DistortionPositions.Exists = false;
	m_DistortionPositions.Size = 0;

	if (FirstPass == false) {
		delete [] m_DistortionPositions.val;
	}

	m_DistortionPositions.val = NULL;

	ClearAttributeStruct(m_Elevation);
	ClearAttributeStruct(m_Filter);
	ClearAttributeStruct(m_FirstActiveLine);
	ClearAttributeStruct(m_IntensityRatio);
	ClearAttributeStruct(m_LinesPerChannel);
	ClearAttributeStruct(m_MainBeamAtten);
	ClearAttributeStruct(m_MtfDirection);
	ClearAttributeStruct(m_MtfFreqPoints);
	ClearAttributeStruct(m_NoiseEqDiffTemp);
	ClearAttributeStruct(m_Period);
	ClearAttributeStruct(m_Polarize);
	ClearAttributeStruct(m_PowerP);
	ClearAttributeStruct(m_PowerDensity);
	ClearAttributeStruct(m_PulseWidth);
	ClearAttributeStruct(m_Radiance);
	ClearAttributeStruct(m_RadianceInterval);
	ClearAttributeStruct(m_RadianceStart);
	ClearAttributeStruct(m_RadianceStop);
	ClearAttributeStruct(m_SampleCount);
	ClearAttributeStruct(m_SampleTime);
	ClearAttributeStruct(m_SettleTime);
	ClearAttributeStruct(m_TestPointCount);
	ClearAttributeStruct(m_WaveLength);
	ClearAttributeStruct(m_HorizTargetAngle);
	ClearAttributeStruct(m_VertTargetAngle);
	ClearAttributeStruct(m_HorizTargetOffset);
	ClearAttributeStruct(m_VertTargetOffset);
	ClearAttributeStruct(m_LastPulseRange);
	ClearAttributeStruct(m_RangeError);
	ClearAttributeStruct(m_TgtCoordinateTop);
	ClearAttributeStruct(m_TgtCoordinateLeft);
	ClearAttributeStruct(m_TgtCoordinateBottom);
	ClearAttributeStruct(m_TgtCoordinateRight);

	m_TargetData.Exists = false;
	m_TargetData.Size = 0;

	if (FirstPass == false) {
		delete [] m_TargetData.val;
	}

	m_TargetData.val = NULL;

	ClearAttributeStruct(m_TargetRange);
	ClearAttributeStruct(m_TargetType);
	ClearAttributeStruct(m_HorizLosAlignError);
	ClearAttributeStruct(m_VertLosAlignError);
	ClearAttributeStruct(m_XAutocollimationError);
	ClearAttributeStruct(m_YAutocollimationError);
	ClearAttributeStruct(m_XBoresightAngle);
	ClearAttributeStruct(m_YBoresightAngle);
	ClearAttributeStruct(m_Format);
	ClearAttributeStruct(m_HorizFieldOfView);
	ClearAttributeStruct(m_VertFieldOfView);

	m_BoresightIntensity = 0;
	m_SignalType = 0;
	m_SignalNoun = 0;

    memset(&m_TrigInfo, '\0', sizeof(m_TrigInfo));
    memset(&m_GateInfo, '\0', sizeof(m_GateInfo));
    memset(&m_MeasInfo, '\0', sizeof(m_MeasInfo));

	m_CenterX = 0;
	m_CenterY = 0;
	m_SignalBlockTopLeftX = 0;
	m_SignalBlockTopLeftY = 0;
	m_SignalBlockBotRightX = 0;
	m_SignalBlockBotRightY = 0;
	m_CorrectionCurve = 0;
	m_Smoothing = 0;
	
	for (Idx = 0; Idx < 9; Idx++)
	{
		m_TargetPosition[Idx]    = 0;
		m_TargetFeatures[Idx]    = 0;
		m_StartingDiffTemps[Idx] = 0.0;
		m_StartingRadiance[Idx]  = 0.0;
	}
	
	m_VertLines     = 0;
	m_HorzLines     = 0;
	m_VoltScale     = 0;
	m_TimeBase      = 0;
	m_CameraTrigger = 0;
	m_NumberOfSteps = 0;
	m_VertScale     = 0.0;
	m_HorzScale     = 0.0;
	m_VideoVoltRes  = 0.0;
	m_RangeCriteria = 0.0;
	FirstPass       = false;

    return;
}


////////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataVeo2_IrWin
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::NullCalDataVeo2_IrWin(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;

    return;
}


////////////////////////////////////////////////////////////////////////////////
// Function: GetSignalChar
//
// Purpose: Parse the 1641 for signal charistics starting with Name
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
//
// Return: 
//      0 - OK
//      - - Failure code
//   m_Signal char filled in accordingly
//
////////////////////////////////////////////////////////////////////////////////
bool CVeo2_IrWin_T::GetSignalChar(char *Name, char *Response, int BufferSize)
{
	bool				RetVal = true;
	int					Idx = 0;
	int					Size = 50;
	double				DblValue;
	char				Element[ATXMLW_MAX_NAME];
	char				*TempPtr;
	ATXMLW_DBL_VECTOR	TmpDblArry[50];

	memset(Element, '\0', ATXMLW_MAX_NAME);

	ISDODEBUG(dodebug(0, "GetSignalChar()", "Name [%s]", Name, (char*)0));

	if ((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
		if(ISELEMENT("Infrared"))
		{
			m_SignalNoun = INFRARED;

			if(ISDBLATTR("differentialTemperature", &(m_DifferentialTemp.Real), m_DifferentialTemp.Dim))
			{
				m_DifferentialTemp.Exists = true;
			}
			if(ISDBLATTR("differentialTemperatureError", &(m_DiffTempError.Real), m_DiffTempError.Dim))
			{
				m_DiffTempError.Exists = true;
			}
			if(ISDBLATTR("differentialTemperatureInterval", &(m_DiffTempInterval.Real), m_DiffTempInterval.Dim))
			{
				m_DiffTempInterval.Exists = true;
			}
			if(ISDBLATTR("differentialTemperatureStart", &(m_DiffTempStart.Real), m_DiffTempStart.Dim))
			{
				m_DiffTempStart.Exists = true;
			}
			if(ISDBLATTR("differentialTemperatureStop", &(m_DiffTempStop.Real), m_DiffTempStop.Dim))
			{
				m_DiffTempStop.Exists = true;
			}
			if(ISDBLATTR("noiseEquivalentDifferentialTemperature", &(m_NoiseEqDiffTemp.Real), m_NoiseEqDiffTemp.Dim))
			{
				m_NoiseEqDiffTemp.Exists = true;
			}
			if(ISINTATTR("filter", &(m_Filter.Int)))
			{
				m_Filter.Exists = true;
			}
			if(ISINTATTR("firstActiveLine", &(m_FirstActiveLine.Int)))
			{
				m_FirstActiveLine.Exists = true;
			}
			if(ISDBLATTR("intensityRatio", &(m_IntensityRatio.Real), m_IntensityRatio.Dim))
			{
				m_IntensityRatio.Exists = true;
			}
			if(ISINTATTR("linesPerChannel", &(m_LinesPerChannel.Int)))
			{
				m_LinesPerChannel.Exists = true;
			}
			if(ISSTRATTR("mtfDirection", &TempPtr))
			{
				m_MtfDirection.Int = (strcmp(TempPtr, "HORIZ") == 0) ? DIM_HORIZ : DIM_VERT;
				m_MtfDirection.Exists = true;
			}
			if(ISINTATTR("mtfFrequencyPoints", &(m_MtfFreqPoints.Int)))
			{
				m_MtfFreqPoints.Exists = true;
			}
			if(ISDBLATTR("noiseEqDiffTemp", &(m_NoiseEqDiffTemp.Real), m_NoiseEqDiffTemp.Dim))
			{
				m_NoiseEqDiffTemp.Exists = false;
			}
			if(ISDBLATTR("settleTime", &(m_SettleTime.Real), m_SettleTime.Dim))
			{
				m_SettleTime.Exists = true;
			}
			if(ISDBLATTR("waveLength", &(m_WaveLength.Real), m_WaveLength.Dim))
			{
				m_WaveLength.Exists = true;
			}
			if(ISINTATTR("testPointCount", &(m_TestPointCount.Int)))
			{
				m_TestPointCount.Exists = true;
			}
		}
		else if(ISELEMENT("Light"))
		{
			m_SignalNoun = LIGHT;

			if(ISINTATTR("filter", &(m_Filter.Int)))
			{
				m_Filter.Exists = true;
			}
			if(ISDBLATTR("intensityRatio", &(DblValue), m_IntensityRatio.Dim))
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = DblValue;
			}
			if(ISSTRATTR("mtfDirection", &TempPtr))
			{
				m_MtfDirection.Int = (strcmp(TempPtr, "HORIZ") == 0) ? DIM_HORIZ : DIM_VERT;
				m_MtfDirection.Exists = true;
			}
			if(ISINTATTR("mtfFrequencyPoints", &(m_MtfFreqPoints.Int)))
			{
				m_MtfFreqPoints.Exists = true;

				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Found mtfFrequencyPoints : %d", m_MtfFreqPoints.Int),\
							 Response, BufferSize);
			}
			if(ISDBLATTR("radiance", &(m_Radiance.Real), m_Radiance.Dim))
			{
				m_Radiance.Exists = true;
			}
			if(ISDBLATTR("radianceInterval", &(m_RadianceInterval.Real), m_RadianceInterval.Dim))
			{
				m_RadianceInterval.Exists = true;
			}
			if(ISDBLATTR("radianceStart", &(m_RadianceStart.Real), m_RadianceStart.Dim))
			{
				m_RadianceStart.Exists = true;
			}
			if(ISDBLATTR("radianceStop", &(m_RadianceStop.Real), m_RadianceStop.Dim))
			{
				m_RadianceStop.Exists = true;
			}
			if(ISDBLATTR("settleTime", &(m_SettleTime.Real), m_SettleTime.Dim))
			{
				m_SettleTime.Exists = true;
			}
			if(ISINTATTR("testPointCount", &(m_TestPointCount.Int)))
			{
				m_TestPointCount.Exists = true;
			}
		}
		else if(ISELEMENT("Laser") || ISELEMENT("LaserTargetReturn"))
		{
			m_SignalNoun = LASER;

			if(ISDBLATTR("intensityRatio", &(DblValue), m_IntensityRatio.Dim))
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = DblValue;
				m_IntensityRatio.Int = (int) DblValue;
			}
			if(ISDBLATTR("mainBeamAttenuation", &(m_MainBeamAtten.Real), m_MainBeamAtten.Dim))
			{
				m_MainBeamAtten.Exists = true;
			}
			if(ISDBLATTR("peakPower", &(m_PowerP.Real), m_PowerP.Dim))
			{
				m_PowerP.Exists = true;
			}
			if(ISDBLATTR("powerDensity", &(m_PowerDensity.Real), m_PowerDensity.Dim))
			{
				m_PowerDensity.Exists = true;
			}
			if(ISDBLATTR("pulseEnergy", &(m_PulseEnergy.Real), m_PowerP.Dim))	// scale period to ms
			{
				m_PulseEnergy.Exists = true;
				ISDODEBUG(dodebug(0, "GetSignalChar()", "Name [%s]", "pulseEnergy", (char*)0));
			}
			if(ISDBLATTR("settleTime", &(m_SettleTime.Real), m_SettleTime.Dim))
			{
				m_SettleTime.Exists = true;
			}
			if(ISINTATTR("testPointCount", &(m_TestPointCount.Int)))
			{
				m_TestPointCount.Exists = true;
			}
			if(ISDBLATTR("waveLength", &(m_WaveLength.Real), m_WaveLength.Dim))
			{
				ISDODEBUG(dodebug(0, "GetSignalChar()", "Laser waveLength here found %g", m_WaveLength.Real, (char*)0));
				m_WaveLength.Exists = true;

				m_WaveLength.Int = ((m_WaveLength.Real > 1.569e-6) && (m_WaveLength.Real < 1.571e-6)) ? DIODE_1570 :
								   ((m_WaveLength.Real > 1.539e-6) && (m_WaveLength.Real < 1.541e-6)) ? DIODE_1540 : DIODE_1064;

				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found waveLength %e %s", m_WaveLength.Real, m_WaveLength.Dim),\
							 Response, BufferSize);
			}
		}
		else if(ISELEMENT("MultiSensorInfrared"))
		{
			m_SignalNoun = INFRARED;

			if(ISDBLATTR("settleTime", &(m_SettleTime.Real), m_SettleTime.Dim))
			{
				m_SettleTime.Exists = true;
			}
		}
		else if(ISELEMENT("MultiSensorLight"))
		{
			m_SignalNoun = LIGHT;
		}
		else if(ISELEMENT("Instantaneous"))
		{
			;
		}
		else if(ISELEMENT("Trapezoid"))
		{
			ISDODEBUG(dodebug(0, "GetSignalChar()", "Name [%s]", "Trapezoid", (char*)0));

			if(ISDBLATTR("pulseWidth", &(m_PulseWidth.Real), m_PulseWidth.Dim))
			{
				m_PulseWidth.Exists = true;
				ISDODEBUG(dodebug(0, "GetSignalChar()", "Name [%s]", "pulseWidth", (char*)0));
			}
			if(ISDBLATTR("timeBase", &(m_SampleTime.Real), m_SampleTime.Dim))
			{
				m_SampleTime.Exists = true;
				ISDODEBUG(dodebug(0, "GetSignalChar()", "Name [%s]", "timeBase", (char*)0));
			}
			if(ISDBLATTR("period", &(m_Period.Real), m_Period.Dim))
			{
				m_Period.Exists = true;
				m_Period.Real = m_Period.Real * 1.0e3;
				ISDODEBUG(dodebug(0, "GetSignalChar()", "Name [%s]", "period", (char*)0));
			}
			if(ISDBLATTR("amplitude", &(m_PowerP.Real), m_PowerP.Dim))
			{
				m_PowerP.Exists = true;
				m_PowerP.Real = m_PowerP.Real * 1.0e9;	// scale to nW
				ISDODEBUG(dodebug(0, "GetSignalChar()", "Name [%s]", "amplitude", (char*)0));
			}
		}
		if(ISELEMENT("Video"))
		{
            if(ISSTRATTR("videoFormat", &TempPtr))
            {
				m_Format.Exists = true;

				m_Format.Int = (strstr(TempPtr, "RS170"         ) != 0) ? VIDEO_RS170 :
							   (strstr(TempPtr, "INTERNAL1"     ) != 0) ? VIDEO_INTERNAL_1 :
							   (strstr(TempPtr, "INTERNAL2"     ) != 0) ? VIDEO_INTERNAL_2 :
							   (strstr(TempPtr, "RS343-675-4-3" ) != 0) ? VIDEO_RS343_675_4_3  :
							   (strstr(TempPtr, "RS343-675-1-1" ) != 0) ? VIDEO_RS343_675_1_1  :
							   (strstr(TempPtr, "RS343-729-4-3" ) != 0) ? VIDEO_RS343_729_4_3  :
							   (strstr(TempPtr, "RS343-729-1-1" ) != 0) ? VIDEO_RS343_729_1_1  :
							   (strstr(TempPtr, "RS343-875-4-3" ) != 0) ? VIDEO_RS343_875_4_3  :
							   (strstr(TempPtr, "RS343-875-1-1" ) != 0) ? VIDEO_RS343_875_1_1  :
							   (strstr(TempPtr, "RS343-945-4-3" ) != 0) ? VIDEO_RS343_945_4_3  :
							   (strstr(TempPtr, "RS343-945-1-1" ) != 0) ? VIDEO_RS343_945_1_1  :
							   (strstr(TempPtr, "RS343-1023-4-3") != 0) ? VIDEO_RS343_1023_4_3 :
							   (strstr(TempPtr, "RS343-1023-1-1") != 0) ? VIDEO_RS343_1023_1_1 :
							   VIDEO_UNKNOWN;

			}
			if(ISDBLATTR("horizontalFieldOfView", &(m_HorizFieldOfView.Real), m_HorizFieldOfView.Dim))
			{
				m_HorizFieldOfView.Real  = m_HorizFieldOfView.Real * 1000.0;	// convert value from radians to milliradians
				m_HorizFieldOfView.Exists = true;
			}
			if(ISDBLATTR("verticalFieldOfView", &(m_VertFieldOfView.Real), m_VertFieldOfView.Dim))
			{
				m_VertFieldOfView.Real  = m_VertFieldOfView.Real * 1000.0;	// convert value from radians to milliradians
				m_VertFieldOfView.Exists = true;
			}
			if(ISDBLATTR("sampleTime", &(m_SampleTime.Real), m_SampleTime.Dim))
			{
				m_SampleTime.Exists = true;
			}
		}
		else if(ISELEMENT("Target"))
		{
			if(ISINTATTR("distortionPositionCount", &(m_DistPosCount.Int)))
			{
				m_DistPosCount.Exists = true;
			}
			if(ISDBLARRAYATTR("distortionPositions", TmpDblArry, &(Size)))
			{
				m_DistortionPositions.Exists = true;
				m_DistortionPositions.Size = Size;
				if (m_DistortionPositions.Size != 0)
					m_DistortionPositions.val = new double[m_DistortionPositions.Size];
				m_DistortionPositions.val[0] = TmpDblArry[0].Value;		// assign list length
				for (Idx = 1; Idx < m_DistortionPositions.Size; Idx++)
					m_DistortionPositions.val[Idx] = TmpDblArry[Idx].Value * 1000.0; // convert from radians to milliradians
			}
			if(ISDBLATTR("horizontalTargetAngle", &(m_HorizTargetAngle.Real), m_HorizTargetAngle.Dim))
			{
				m_HorizTargetAngle.Real = m_HorizTargetAngle.Real * 1000.0;	// convert value from radians to milliradians
				m_HorizTargetAngle.Exists = true;
			}
			if(ISDBLATTR("horizontalTargetOffset", &(m_HorizTargetOffset.Real), m_HorizTargetOffset.Dim))
			{
				m_HorizTargetOffset.Real = m_HorizTargetOffset.Real * 1000.0;	// convert value from radians to milliradians
				m_HorizTargetOffset.Exists = true;
			}
			if(ISDBLATTR("lastPulseRange", &(m_LastPulseRange.Real), m_LastPulseRange.Dim))
			{
				m_LastPulseRange.Exists = true;
				m_LastPulseRange.Real = m_LastPulseRange.Real * 1000.0;
				m_LastPulseRange.Int = (int)m_LastPulseRange.Real;

			}
			if(ISDBLATTR("rangeError", &(m_RangeError.Real), m_RangeError.Dim))
			{
				m_RangeError.Exists = true;
				m_RangeError.Real = m_RangeError.Real * 1000.0; //Common function s_Normalize converted it to milli
			}
			if(ISINTATTR("targetCoordinateTop", &(m_TgtCoordinateTop.Int)))
			{
				m_TgtCoordinateTop.Exists = true;
			}
			if(ISINTATTR("targetCoordinateLeft", &(m_TgtCoordinateLeft.Int)))
			{
				m_TgtCoordinateLeft.Exists = true;
			}
			if(ISINTATTR("targetCoordinateBottom", &(m_TgtCoordinateBottom.Int)))
			{
				m_TgtCoordinateBottom.Exists = true;
			}
			if(ISINTATTR("targetCoordinateRight", &(m_TgtCoordinateRight.Int)))
			{
				m_TgtCoordinateRight.Exists = true;
			}
			if(ISDBLARRAYATTR("targetData", TmpDblArry, &(Size)))
			{
				m_TargetData.Exists = true;
				m_TargetData.Size = Size;
				if (m_TargetData.Size != 0)
					m_TargetData.val = new double[m_TargetData.Size+1];
				for (Idx = 0; Idx <= m_TargetData.Size; Idx++)
					m_TargetData.val[Idx] = TmpDblArry[Idx].Value;
			}
			if(ISDBLATTR("targetRange", &(m_TargetRange.Real), m_TargetRange.Dim))
			{
				m_TargetRange.Exists = true;
				m_TargetRange.Real = m_TargetRange.Real * 1000.0; //Common function s_Normalize converted it to milli
				m_TargetRange.Int = (int)m_TargetRange.Real; // some use as int
			}
			if(ISSTRATTR("targetType", &TempPtr))
			{
				m_TargetType.Exists = true;

				m_TargetType.Int = (strstr(TempPtr, "4BAR5")     != 0) ? TARG_4BAR5    :
								   (strstr(TempPtr, "BRSGHT")    != 0) ? TARG_BRSGHT    :
								   (strstr(TempPtr, "4BAR15")    != 0) ? TARG_4BAR15    :
								   (strstr(TempPtr, "4BAR33")    != 0) ? TARG_4BAR33    :
								   (strstr(TempPtr, "DIAGLN")    != 0) ? TARG_DIAGLN    :
								   (strstr(TempPtr, "ETCHED")    != 0) ? TARG_ETCHED    :
								   (strstr(TempPtr, "GRYSCL")    != 0) ? TARG_GRYSCL    :
								   (strstr(TempPtr, "4BAR10")    != 0) ? TARG_4BAR10    :
								   (strstr(TempPtr, "4BAR66")    != 0) ? TARG_4BAR66    :
								   (strstr(TempPtr, "OPNAPR")    != 0) ? TARG_OPNAPR     :
								   (strstr(TempPtr, "4BAR383")   != 0) ? TARG_4BAR383   :
								   (strstr(TempPtr, "4BAR267")   != 0) ? TARG_4BAR267   :
								   (strstr(TempPtr, "TGTGRP1")   != 0) ? TARG_TGTGRP1   :
								   (strstr(TempPtr, "TGTGRP2")   != 0) ? TARG_TGTGRP2   :
								   (strstr(TempPtr, "CROSS17")   != 0) ? TARG_CROSS17   :
								   (strstr(TempPtr, "SQUARE21")  != 0) ? TARG_SQUARE21  :
								   (strstr(TempPtr, "SPNPINHL")  != 0) ? TARG_SPNPINHL  :
								   (strstr(TempPtr, "TGTGRP07")  != 0) ? TARG_TGTGRP07  :
								   (strstr(TempPtr, "PIESECTOR") != 0) ? TARG_PIESECTOR :
								   TARG_UNKNOWN;

				if (strstr(TempPtr, "BS0") != 0)
				{
					m_TargetType.Int = TARG_BRSGHT;
					m_DifferentialTemp.Exists = true;
					m_DifferentialTemp.Real = (strstr(TempPtr, "IRBS0")  != 0) ? 0.0  :
											  (strstr(TempPtr, "IRBS01") != 0) ? 0.1  :
											  (strstr(TempPtr, "IRBS02") != 0) ? 0.2  :
											  (strstr(TempPtr, "IRBS03") != 0) ? 0.4  :
											  (strstr(TempPtr, "IRBS04") != 0) ? 0.7  :
											  (strstr(TempPtr, "IRBS05") != 0) ? 1.0  :
											  (strstr(TempPtr, "IRBS06") != 0) ? 2.0  :
											  (strstr(TempPtr, "IRBS07") != 0) ? 4.0  :
											  (strstr(TempPtr, "IRBS08") != 0) ? 6.0  :
											  (strstr(TempPtr, "IRBS09") != 0) ? 8.0  :
											  (strstr(TempPtr, "IRBS10") != 0) ? 10   :
											  (strstr(TempPtr, "TVBS01") != 0) ? 20   :
											  (strstr(TempPtr, "TVBS02") != 0) ? 100  :
											  (strstr(TempPtr, "TVBS03") != 0) ? 300  :
											  (strstr(TempPtr, "TVBS04") != 0) ? 500  :
											  (strstr(TempPtr, "TVBS05") != 0) ? 700  :
											  (strstr(TempPtr, "TVBS06") != 0) ? 1000 :
											  (strstr(TempPtr, "TVBS07") != 0) ? 1300 :
											  (strstr(TempPtr, "TVBS08") != 0) ? 1600 :
											  (strstr(TempPtr, "TVBS09") != 0) ? 2000 :
											  (strstr(TempPtr, "TVBS10") != 0) ? 2500 :
											  (strstr(TempPtr, "TVBS11") != 0) ? 3000 :
											  (strstr(TempPtr, "TVBS12") != 0) ? 3500 :
											  (strstr(TempPtr, "TVBS13") != 0) ? 4000 :
											  (strstr(TempPtr, "TVBS14") != 0) ? 4500 :
											  (strstr(TempPtr, "TVBS15") != 0) ? 5000 : 0;
				}
			}
			if(ISDBLATTR("verticalTargetAngle", &(m_VertTargetAngle.Real), m_VertTargetAngle.Dim))
			{
				m_VertTargetAngle.Real = m_VertTargetAngle.Real * 1000.0;	// convert value from radians to milliradians
				m_VertTargetAngle.Exists = true;
			}
			if(ISDBLATTR("verticalTargetOffset", &(m_VertTargetOffset.Real), m_VertTargetOffset.Dim))
			{
				m_VertTargetOffset.Real = m_VertTargetOffset.Real * 1000.0;	// convert value from radians to milliradians
				m_VertTargetOffset.Exists = true;
			}
		}
		else if(ISELEMENT("Boresight"))
		{
			if(ISDBLATTR("horizontalLineOfSightAlignmentError", &(m_HorizLosAlignError.Real), m_HorizLosAlignError.Dim))
			{
				m_HorizLosAlignError.Real = m_HorizLosAlignError.Real * 1000.0;	// convert value from radians to milliradians
				m_HorizLosAlignError.Exists = true;
			}
			if(ISDBLATTR("verticalLineOfSightAlignmentError", &(m_VertLosAlignError.Real), m_VertLosAlignError.Dim))
			{
				m_VertLosAlignError.Real = m_VertLosAlignError.Real * 1000.0;	// convert value from radians to milliradians
				m_VertLosAlignError.Exists = true;
			}
			if(ISDBLATTR("xAutocollimationError", &(m_XAutocollimationError.Real), m_XAutocollimationError.Dim))
			{
				m_XAutocollimationError.Real = m_XAutocollimationError.Real * 1000.0;	// convert value from radians to milliradians
				m_XAutocollimationError.Exists = true;
			}
			if(ISDBLATTR("yAutocollimationError", &(m_YAutocollimationError.Real), m_YAutocollimationError.Dim))
			{
				m_YAutocollimationError.Real = m_YAutocollimationError.Real * 1000.0;	// convert value from radians to milliradians
				m_YAutocollimationError.Exists = true;
			}
			if(ISDBLATTR("xBoresightAngle", &(m_XBoresightAngle.Real), m_XBoresightAngle.Dim))
			{
				m_XBoresightAngle.Real = m_XBoresightAngle.Real * 1000.0;	// convert value from radians to milliradians
				m_XBoresightAngle.Exists = true;
			}
			if(ISDBLATTR("yBoresightAngle", &(m_YBoresightAngle.Real), m_YBoresightAngle.Dim))
			{
				m_YBoresightAngle.Real = m_YBoresightAngle.Real * 1000.0;	// convert value from radians to milliradians
				m_YBoresightAngle.Exists = true;
			}
		}
		else if(ISELEMENT("Larrs"))
		{
			m_SignalNoun = LARRS;

			if(ISINTATTR("azimuth", &(m_Azimuth.Int)))
			{
				m_Azimuth.Exists = true;
			}
			if(ISINTATTR("elevation", &(m_Elevation.Int)))
			{
				m_Elevation.Exists = true;
			}
			if(ISINTATTR("polarize", &(m_Polarize.Int)))
			{
				m_Polarize.Exists = true;
			}
		}
         
        if(ISSTRATTR("In", &TempPtr))		// Find the next signal name
        {
            GetSignalChar(TempPtr, Response, BufferSize);
        } 
    }
    else	// Unknown name ??
    {
        RetVal = false;
    }

    return(RetVal);
}

////////////////////////////////////////////////////////////////////////////////
// Function: GetSignalCond
//
// Purpose: Parse the 1641 for signal conditioners starting with Name
//          FILTER-ON, AC-COUPLE, etc.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
//
// Return: 
//      0 - OK
//      - - Failure code
//   m_Signal char filled in accordingly
//
////////////////////////////////////////////////////////////////////////////////
bool CVeo2_IrWin_T::GetSignalCond(char *Name, char *InputNames, char *Response, int BufferSize)
{

    bool	RetVal = true;
    char	Element[ATXMLW_MAX_NAME];
    char	Unit[ATXMLW_MAX_NAME];
    char	*TempPtr;

	memset(Element, '\0', ATXMLW_MAX_NAME);
	memset(Unit, '\0', ATXMLW_MAX_NAME);

    if(atxmlw_IsPortNameTsf(m_SignalDescription, InputNames, Name, &TempPtr))
    {
        strnzcpy(m_InputChannel, Name, ATXMLW_MAX_NAME);	//Found the Input Port Name
        
		if(TempPtr)
            GetSignalCond(TempPtr, InputNames, Response, BufferSize);
    }
    else if ((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
		if(ISELEMENT("SignalDelay"))
		{
			if(ISDBLATTR("delay", &(m_Delay.Real), m_Delay.Dim))
			{
				m_Delay.Exists = true;
			}
		}
        if(ISSTRATTR("In", &TempPtr))		// Find the next signal name or port name
        {
            GetSignalCond(TempPtr, InputNames, Response, BufferSize);
        } 
    }
    else	// Unknown name ??
    {
        RetVal = false;
    }

    return(RetVal);
}

////////////////////////////////////////////////////////////////////////////////
// Function : GetAveragePulseWidth(char * Response, int BufferSize)           //
//                                                                            //
// Purpose  : Retrieve the average pulse from the VEO2 for use in calculating //
//            POWER-P and PULSE-AMP-STAB.                                     //
//                                                                            //
// Input Parameters                                                           //
// Parameter    Type			     Purpose                                  //
// ===========  ===================  ======================================== //
// attr         char *               measurement attribute                    //
//                                                                            //
// Output Parameters                                                          //
// Parameter	Type			     Purpose                                  //
// ===========  ===================  ======================================== //
//                                                                            //
// Return: Void                                                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float CVeo2_IrWin_T::GetAveragePulseWidth(int channel, ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
int		Timebase;
	int		TrigSlope;
	int		Status = 0;
	float	RealParam1 = 0.0, RealParam2 = 0.0;
	char	StatusMsg[100];

	memset(Response, '\0', BufferSize);

	Timebase = 7;
	TrigSlope = (m_TrigInfo.TrigExists == true) ? (m_TrigInfo.TrigSlopePos == true) ? 1 : 2 : 1;	//Set to Positive if not set.
	CalculateInputScaleData();

	IFNSIMVEO2(PULSE_WIDTH_LASER_SETUP(m_MeasInfo.Attrs.samples,\
									   channel,
									   5,\
									   Timebase,\
									   channel,\
									   TrigSlope,
									   (float)m_TrigInfo.TrigLevel));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_WIDTH_LASER_SETUP(%d, %d, 5, %d, %d, %s, %lf);",\
				 m_MeasInfo.Attrs.samples,\
				 channel,\
				 Timebase,\
				 channel,\
				 TRIGSLOPE[TrigSlope],\
				 m_TrigInfo.TrigLevel), Response, BufferSize);

	// Initiate the measurement
	INITIATE = (TN_INITIATE)GetProcAddress(m_Handle, "PULSE_WIDTH_LASER_INITIATE");

	if (INITIATE == NULL)
	{
		ATXMLW_ERROR(-1, "EnableVeo2_IrWin()",\
					 atxmlw_FmtMsg("PULSE_WIDTH_LASER_INITIATE bad function pointer"),\
					 Response, BufferSize);
		return 0;
	}

	IFNSIMVEO2(INITIATE());
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_WIDTH_LASER_INITIATE();"), Response, BufferSize);

	Sleep(1000);

	// Fetch the value
	IFNSIMVEO2(PULSE_WIDTH_LASER_FETCH(&RealParam1, &RealParam2, &Status));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("PULSE_WIDTH_LASER_FETCH(%lf, %lf, %d);",\
				 RealParam1, RealParam2, Status), Response, BufferSize);
	if (Status)
	{
		LsrErrMsg	LsrMeasType = PulseWidLsr;
		RealParam1 = FLT_MAX;

		GetLsrStatusMessage(Status, LsrMeasType, StatusMsg);
		ATXMLW_ERROR(Status, "FetchVeo2_IrWin()", StatusMsg, Response, BufferSize);
	}

	return RealParam1;
}

////////////////////////////////////////////////////////////////////////////////
// Function : SetMeasDefaults(char * Response, int BufferSize)                //
//                                                                            //
// Purpose : Set the required parameter defaults for each signal and report   //
//           missing required parameters.                                     //
//                                                                            //
//                                                                            // 
// Input Parameters                                                           //
// Parameter    Type			     Purpose                                  //
// ===========  ===================  ======================================== //
// attr         char *               measurement attribute                    //
//                                                                            //
// Output Parameters                                                          //
// Parameter	Type			     Purpose                                  //
// ===========  ===================  ======================================== //
//                                                                            //
// Return: Void                                                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::SetMeasDefaults(char * Response, int BufferSize)
{
	int	Idx             = 0;
	int HorizTargetSize = 0;
	int VertTargetSize  = 0;

	memset(Response, '\0', BufferSize);

	ProcessCameraSelection(Response, BufferSize);

	if (strstr(m_MeasInfo.cyType, "IR") != NULL)
	{
		if ((strcmp(m_MeasInfo.cyType, "minResolvTempDiffIR") != 0) &&	// minResolvTempDiff does not use default params
			(strcmp(m_MeasInfo.cyType, "differentialTempIR")  != 0) &&
			(strcmp(m_MeasInfo.cyType, "ambientTempIR")       != 0) &&
			(strcmp(m_MeasInfo.cyType, "blackbodyTempIR")     != 0))
		{
			// Establish IR Defaults
			// Parameter 1 Image_Num_Frames
			if (m_MeasInfo.Attrs.samples <= 0)
				m_MeasInfo.Attrs.samples = 1;

			// Parameter 2 H Field of view
			if (!m_HorizFieldOfView.Exists)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Required modifier H-FIELD-OF-VIEW not present - setting to 1000 mrad"),\
							 Response, BufferSize);

				m_HorizFieldOfView.Exists = true;
				m_HorizFieldOfView.Real = 1000.0;
			}
			// Parameter 3 V Field of view
			if (!m_VertFieldOfView.Exists)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Required modifier V-FIELD-OF-VIEW not present - setting to 1000 mrad"),\
							 Response, BufferSize);

				m_VertFieldOfView.Exists = true;
				m_VertFieldOfView.Real = 1000.0;
			}
			// Parameter 4 diff_temp
			if (!m_DifferentialTemp.Exists)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Required modifier DIFFERENTIAL-TEMP not present - defaulting to 0.0 degc"),\
							 Response, BufferSize);

				m_DifferentialTemp.Exists = true;
				m_DifferentialTemp.Real = 0.0;
			}
			// Parameter 5 target_position - set in individual measurement
			// Parameter 6 Center_X - set in ProcessCameraSelection compensate for LOS-ALIGN-ERROR
			m_CenterX += (int)(m_HorizLosAlignError.Real * m_HorzScale); 
			// Parameter 7 Center_Y - set in ProcessCameraSelection
			m_CenterY += (int)(m_VertLosAlignError.Real * m_VertScale);

			// target coordintes
			if ((m_TgtCoordinateLeft.Exists)   && (m_TgtCoordinateTop.Exists) &&
				(m_TgtCoordinateBottom.Exists) && (m_TgtCoordinateRight.Exists))
			{
				m_SignalBlockTopLeftX  = m_TgtCoordinateLeft.Int;
				m_SignalBlockTopLeftY  = m_TgtCoordinateTop.Int;
				m_SignalBlockBotRightX = m_TgtCoordinateRight.Int;
				m_SignalBlockBotRightY = m_TgtCoordinateBottom.Int;
			}
			else if ((m_HorizTargetAngle.Exists) && (m_VertTargetAngle.Exists))
			{
				HorizTargetSize = (static_cast<int>(m_HorizTargetAngle.Real / m_HorizFieldOfView.Real)) * m_HorzLines;
				VertTargetSize  = (static_cast<int>(m_VertTargetAngle.Real  / m_VertFieldOfView.Real))  * m_VertLines;
				m_SignalBlockTopLeftX = (m_HorzLines - HorizTargetSize) / 2;
				m_SignalBlockTopLeftY = (m_VertLines - VertTargetSize)  / 2;
				m_SignalBlockBotRightX = m_HorzLines - ((m_HorzLines - HorizTargetSize) / 2);
				m_SignalBlockBotRightY = m_VertLines - ((m_VertLines - VertTargetSize)  / 2);
			}
			else
			{
				ATXMLW_ERROR(-1, "SetupVeo2_IrWin",	"Either x-TARGET-ANGLE or TGT-COORDINATEs must be defined",\
							 Response, BufferSize);
			}
			// Parameter 12 Camera_Selection
			if (!m_Format.Exists)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Required modifier FORMAT missing, defaulting to RS170"),\
							 Response, BufferSize);

				m_Format.Exists = true;
				m_Format.Int = 1;
			}
			if (m_HorizFieldOfView.Real != 0.0)
				m_HorzScale = (double)(m_VertLines)/m_HorizFieldOfView.Real;
			if (m_VertFieldOfView.Real != 0.0)
				m_VertScale = (double)(m_HorzLines)/m_VertFieldOfView.Real;
		}
		////////////////////////////////////////////////////////////////////////
		// MODULATION TRANSFER FUNCTION INFRARED                              //
		////////////////////////////////////////////////////////////////////////
		if (strcmp(m_MeasInfo.cyType, "modulationTransferFunctionIR") == 0)
		{
			if (!m_WaveLength.Exists)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Required modifier WAVELENGTH missing, defaulting to 5.0 um"),\
							 Response, BufferSize);

				m_CorrectionCurve = 1;
			}
			else
			{
				if ((m_WaveLength.Real >= 8.0e-6) && (m_WaveLength.Real <= 12.0e-6))
					m_CorrectionCurve = 1;
				else if ((m_WaveLength.Real >= 3.0e-6) && (m_WaveLength.Real <= 5.0e-6)) 
					m_CorrectionCurve = 2;

				ISDODEBUG(dodebug(0, "SetMeasDefaults()", "WAVELENGTH %e setting correction curve to %d", m_WaveLength.Real, m_CorrectionCurve, (char*)0));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WAVELENGTH %e setting correction curve to %d", m_WaveLength.Real, m_CorrectionCurve),\
							 Response, BufferSize);
			}

			m_Smoothing = 1;
			m_HorzScale = (double)(m_VertLines) / m_HorizFieldOfView.Real;
			m_VertScale = (double)(m_HorzLines) / m_VertFieldOfView.Real;

		}
		////////////////////////////////////////////////////////////////////////
		// MINIMUM RESOLVABLE TEMPERATURE DIFFERENCE INFRARED                 //
		////////////////////////////////////////////////////////////////////////
		if (strcmp(m_MeasInfo.cyType, "minResolvTempDiffIR") == 0)
		{
			m_StartingDiffTemps[0] = (float)((m_TestPointCount.Int) + 1.0);
			m_TargetPosition[0] = m_TestPointCount.Int + 1;	// Change - count should be list length, not test point count
			m_TargetFeatures[0] = m_TestPointCount.Int + 1;

			for (Idx = 0; Idx < m_TestPointCount.Int; Idx++)
			{
				m_StartingDiffTemps[Idx+1] = (float)m_TargetData.val[Idx];

				if ((m_TargetData.val[Idx+m_TestPointCount.Int] > 0.3) &&
					(m_TargetData.val[Idx+m_TestPointCount.Int] < 0.36))
					m_TargetPosition[Idx+1] = TARG_4BAR33;
				else if ((m_TargetData.val[Idx+m_TestPointCount.Int] > 0.63) && // 20110204 added traps for new 4bar66 and 4bar10 targets.
						 (m_TargetData.val[Idx+m_TestPointCount.Int] < 0.69))
					m_TargetPosition[Idx+1] = TARG_4BAR66;
				else if ((m_TargetData.val[Idx+m_TestPointCount.Int] > 0.97) &&
						 (m_TargetData.val[Idx+m_TestPointCount.Int] < 1.03))
					m_TargetPosition[Idx+1] = TARG_4BAR10;
				else if ((m_TargetData.val[Idx+m_TestPointCount.Int] > 1.4) &&
						 (m_TargetData.val[Idx+m_TestPointCount.Int] < 1.6))
					m_TargetPosition[Idx+1] = TARG_4BAR15;
				else if ((m_TargetData.val[Idx+m_TestPointCount.Int] > 2.6) &&
						 (m_TargetData.val[Idx+m_TestPointCount.Int] < 2.7))
					m_TargetPosition[Idx+1] = TARG_4BAR267;
				else if ((m_TargetData.val[Idx+m_TestPointCount.Int] > 3.8) &&
						 (m_TargetData.val[Idx+m_TestPointCount.Int] < 3.9))
					m_TargetPosition[Idx+1] = TARG_4BAR383;
				else if ((m_TargetData.val[Idx+m_TestPointCount.Int] > 4.9) &&
						 (m_TargetData.val[Idx+m_TestPointCount.Int] < 5.1))
					m_TargetPosition[Idx+1] = TARG_4BAR5;

				m_TargetFeatures[Idx+1] = 1;
			}
		}
	}
	////////////////////////////////////////////////////////////////////////////
	//                      LASER DEFAULTS                                    //
	////////////////////////////////////////////////////////////////////////////
	else if (strstr(m_MeasInfo.cyType, "Laser") != NULL)
	{
		// Category 1 Laser Parameters
		if ((strcmp(m_MeasInfo.cyType, "divergenceLaser")			== 0) ||
			(strcmp(m_MeasInfo.cyType, "boresightAngleLaser")		== 0) ||
			(strcmp(m_MeasInfo.cyType, "diffBoresightAngleLaser")	== 0) ||
			(strcmp(m_MeasInfo.cyType, "autocollimationErrorLaser") == 0))
		{
			m_HorizFieldOfView.Real = 7.559;
			m_VertFieldOfView.Real = 5.669;
			m_HorizTargetOffset.Real = 0.0;
			m_VertTargetOffset.Real = 0.0;

			if ((m_TgtCoordinateTop.Exists) &&
				(m_TgtCoordinateLeft.Exists) &&
				(m_TgtCoordinateBottom.Exists) &&
				(m_TgtCoordinateRight.Exists))
			{
				m_SignalBlockTopLeftX = m_TgtCoordinateLeft.Int;
				m_SignalBlockTopLeftY = m_TgtCoordinateTop.Int;
				m_SignalBlockBotRightX = m_TgtCoordinateRight.Int;
				m_SignalBlockBotRightY = m_TgtCoordinateBottom.Int;
			}
			else 
			{
				m_SignalBlockTopLeftX = 316;
				m_SignalBlockTopLeftY = 237;
				m_SignalBlockBotRightX = 324;
				m_SignalBlockBotRightY = 243;
			}

			if (strcmp(m_TrigInfo.TrigPort, "INT") == 0)
				m_CameraTrigger = 0;
			else if (strcmp(m_TrigInfo.TrigPort, "EXT") == 0)
				m_CameraTrigger = 1;
			else if (strcmp(m_TrigInfo.TrigPort, "LASR") == 0)
				m_CameraTrigger = 2;
			else
				m_CameraTrigger = 0;

		}
		// Category 2 Laser Parameters
		if ((strcmp(m_MeasInfo.cyType, "rangeErrorLaser") == 0)				||
			(strcmp(m_MeasInfo.cyType, "receiverSensitivityLaser") == 0))
		{
			if (m_TrigInfo.TrigExists == false)
			{
				m_TrigInfo.TrigExists = true;
				m_TrigInfoInt = LASER_TRIG_LASR;
			}

			if (m_WaveLength.Exists == false)
			{
				m_WaveLength.Exists = true;
				m_WaveLength.Int = DIODE_1064;
			}

			if (m_IntensityRatio.Exists == false)
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Int = 50;
			}
			if (m_TargetRange.Exists == false)
			{
				m_TargetRange.Exists = true;
				m_TargetRange.Real = 500.0;
			}
			if (m_RangeError.Exists == true)
			{
				m_RangeCriteria = (float)((m_RangeError.Real / m_TargetRange.Real) * 100.0);
			}
			else
			{
				m_RangeCriteria = 5.0;
			}
		}
	}
	////////////////////////////////////////////////////////////////////////////
	//                       VISIBLE LIGHT DEFAULTS                           //
	////////////////////////////////////////////////////////////////////////////
	else if (strstr(m_MeasInfo.cyType, "Vis") != NULL)
	{
		if (strcmp(m_MeasInfo.cyType, "minResolvContrastDiffVis") != 0)	// minResolvContrastDiffVis does not use default params
		{
			// Establish IR Defaults
			// Parameter 2 Image_Num_Frames
			if (m_MeasInfo.Attrs.samples <= 0)
				m_MeasInfo.Attrs.samples = 1;
			// Parameter 3 H Field of view
			if (!m_HorizFieldOfView.Exists)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Required modifier H-FIELD-OF-VIEW not present - setting to 1000 mrad"),\
							 Response, BufferSize);
				m_HorizFieldOfView.Exists = true;
				m_HorizFieldOfView.Real = 1000.0;
			}
			// Parameter 4 V Field of view
			if (!m_VertFieldOfView.Exists)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Required modifier V-FIELD-OF-VIEW not present - setting to 1000 mrad"),\
							 Response, BufferSize);
				m_VertFieldOfView.Exists = true;
				m_VertFieldOfView.Real = 1000.0;
			}
			// Parameter 5 diff_temp
			if (!m_Radiance.Exists)
			{
				if (m_RadianceStart.Exists)
				{
					m_Radiance.Real = m_RadianceStart.Real;
				}
				else 
				{
					m_Radiance.Real = 1234.0;
				}
			}
			// Parameter 6 target_position - set in individual measurement
			// Parameter 7 Center_X - set in ProcessCameraSelection
			m_CenterX += (int)(m_HorizLosAlignError.Real * m_HorzScale);
			// Parameter 8 Center_Y - set in ProcessCameraSelection
			m_CenterY += (int)(m_VertLosAlignError.Real * m_VertScale);
			// target coordintes params 9-12
			if ((m_TgtCoordinateLeft.Exists) &&
				(m_TgtCoordinateTop.Exists) &&
				(m_TgtCoordinateBottom.Exists) && 
				(m_TgtCoordinateRight.Exists))
			{
				m_SignalBlockTopLeftX = m_TgtCoordinateLeft.Int;
				m_SignalBlockTopLeftY = m_TgtCoordinateTop.Int;
				m_SignalBlockBotRightX = m_TgtCoordinateRight.Int;
				m_SignalBlockBotRightY = m_TgtCoordinateBottom.Int;
			}
			else if ((m_HorizTargetAngle.Exists) && (m_VertTargetAngle.Exists))
			{
				HorizTargetSize = (static_cast<int>(m_HorizTargetAngle.Real / m_HorizFieldOfView.Real)) * m_HorzLines;
				VertTargetSize  = (static_cast<int>(m_VertTargetAngle.Real  / m_VertFieldOfView.Real))  * m_VertLines;
				m_SignalBlockTopLeftX = (m_HorzLines - HorizTargetSize) / 2;
				m_SignalBlockTopLeftY = (m_VertLines - VertTargetSize)  / 2;
				m_SignalBlockBotRightX = m_HorzLines - ((m_HorzLines - HorizTargetSize) / 2);
				m_SignalBlockBotRightY = m_VertLines - ((m_VertLines - VertTargetSize)  / 2);
			}
			else
			{
				ATXMLW_ERROR(-1, "SetupVeo2_IrWin",	"Either x-TARGET-ANGLE or TGT-COORDINATEs must be defined",\
							 Response, BufferSize);
			}
			// Parameter 13 Camera_Selection
			if (!m_Format.Exists)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("Required modifier FORMAT missing, defaulting to RS170"),\
							 Response, BufferSize);
				m_Format.Exists = true;
				m_Format.Int = 1;
			}
		}
		else	// params for minResolvContrastDiffVis
		{
			if ((m_RadianceInterval.Exists) && (m_RadianceStart.Exists) && (m_RadianceStop.Exists))
			{
				m_NumberOfSteps = (int)((m_RadianceStop.Real - m_RadianceStart.Real) / m_RadianceInterval.Real);
				m_StartingRadiance[0] = (float)m_NumberOfSteps;
				m_StartingRadiance[1] = (float)m_RadianceStart.Real;
				for (Idx = 2; Idx <= m_NumberOfSteps; Idx++)
				{
					m_StartingRadiance[Idx] = (float)(m_StartingRadiance[Idx-1] + m_RadianceInterval.Real);
				}
			}
			else
			{
				// error
			}
		}
	}
}

////////////////////////////////////////////////////////////////////////////////
// Function : SetSoureDefaults(char * Response, int BufferSize)               //
//                                                                            //
// Purpose : Set the required parameter defaults for each signal and report   //
//           missing required parameters.                                     //
//                                                                            //
//                                                                            // 
// Input Parameters                                                           //
// Parameter    Type			     Purpose                                  //
// ===========  ===================  ======================================== //
// attr         char *               measurement attribute                    //
//                                                                            //
// Output Parameters                                                          //
// Parameter	Type			     Purpose                                  //
// ===========  ===================  ======================================== //
//                                                                            //
// Return: Void                                                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::SetSourceDefaults(void)
{
	if (m_SignalNoun == INFRARED)
	{
		if (!m_DiffTempError.Exists)
		{
			m_DiffTempError.Exists = true;
			m_DiffTempError.Real = 0.03;
		}
	}
	else if (m_SignalNoun == LASER)
	{
		if (!m_SampleTime.Exists)
		{
			m_SampleTime.Exists = true;
			m_SampleTime.Real = 33.3e-3;
		}
		if (!m_Delay.Exists)
		{
			m_Delay.Exists = true;
			m_Delay.Real = 0.0;
		}
		if (!m_WaveLength.Exists)
		{
			m_WaveLength.Exists = true;
			m_WaveLength.Int = DIODE_1064;
		}
		if (!m_PulseWidth.Exists)
		{
			m_PulseWidth.Exists = true;
			m_PulseWidth.Real = 20.0e-9;
		}
		if (strcmp(m_TrigInfo.TrigPort, "EXT") == 0)
			m_CameraTrigger = 1;
		else if (strcmp(m_TrigInfo.TrigPort, "LASR") == 0)
			m_CameraTrigger = 2;
		else
			m_CameraTrigger = 0;
			
	}
	else if (m_SignalNoun == LIGHT)
	{
		if (!m_Radiance.Exists)
		{
			m_Radiance.Exists = true;
			m_Radiance.Real = 0.0;
		}
		if (!m_SettleTime.Exists)
		{
			m_SettleTime.Exists = true;
			m_SettleTime.Real = 10.0;
		}
	}
}

////////////////////////////////////////////////////////////////////////////////
// Function : WaitForReady(double timeout, char * Response, int BufferSize)   //
//                                                                            //
// Purpose : Waits for VEO2 to report ready. Reports timeout if timeout is    //
//           exceeded. Returns true if successful, false if timeout.          //
//                                                                            //
//                                                                            // 
// Input Parameters                                                           //
// Parameter    Type			     Purpose                                  //
// ===========  ===================  ======================================== //
// timeout      double               timeout value, max-time, settle time etc //
//                                                                            //
// Output Parameters                                                          //
// Parameter	Type			     Purpose                                  //
// ===========  ===================  ======================================== //
//                                                                            //
// Return: bool                                                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
bool CVeo2_IrWin_T::WaitForReady(double timeout, double * elapsed, char * Response, int BufferSize)
{
	bool	Status = false;
	int		StatusByte = 0;
	time_t	timeStart = 0;
	time_t	timeCurrent = 0;
	double	deltaTime = 0.0;

	memset(Response, '\0', BufferSize);

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("WaitForReady(%lf, Response, %d);", timeout, BufferSize),\
				 Response, BufferSize);

	// Check busy bit 0x0281, to be 0
	time(&timeStart);

	IFNSIMVEO2(GET_STATUS_BYTE_MESSAGE_INITIATE());

	do
	{
		IFNSIMVEO2(GET_STATUS_BYTE_MESSAGE_FETCH(&StatusByte));

		Sleep(1000);

		time(&timeCurrent);
		deltaTime = difftime(timeCurrent, timeStart);

		IFNSIMVEO2(GET_STATUS_BYTE_MESSAGE_INITIATE());

	} while ((deltaTime < timeout) && ((StatusByte & READY_BIT) != READY_BIT));

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("deltatime = %lf\t timeout=%lf\t status byte=0x%x", deltaTime, timeout, StatusByte),\
				 Response, BufferSize);
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("GET_STATUS_BYTE_MESSAGE_FETCH(%x);", StatusByte), Response, BufferSize);
 	ISDODEBUG(dodebug(0, "WaitForReady()", "deltaTime = %f StatusByte = 0x%x", deltaTime, StatusByte, (char*)0));

	*elapsed = deltaTime;

	// report max-time if exceeded
	if (deltaTime >= timeout)
	{
		Status = false;
		atxmlw_ScalerIntegerReturn("timeOut", "", 1, Response, BufferSize);
	}
	else
	{
		Status = true;
	}
	return Status;
}


////////////////////////////////////////////////////////////////////////////////
// Function : ProcessCameraSelection
// Purpose  : Sets camera releated parameters such as  center x/y,
//            number of vertical and horizontal lines, etc.
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
//
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//		Void
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::ProcessCameraSelection(ATXMLW_INTF_RESPONSE * Response, int BufferSize)
{
	ATXMLW_DEBUG(5, "ProcessCameraSelection();", Response, BufferSize);

	if (m_Format.Exists)
	{
		switch (m_Format.Int)
		{
			case VIDEO_INTERNAL_1 :
				m_HorzLines = 256;
				m_VertLines = 320;
				break;
			case VIDEO_INTERNAL_2 :
				m_HorzLines = 256;
				m_VertLines = 320;
				break;
			case VIDEO_RS170 :
				m_HorzLines = 480;
				m_VertLines = 640;
				break;
			case VIDEO_RS343_675_4_3 :
				m_HorzLines = 624;
				m_VertLines = 832;
				break;
			case VIDEO_RS343_675_1_1 :
				m_HorzLines = 624;
				m_VertLines = 624;
				break;
			case VIDEO_RS343_729_4_3 :
				m_HorzLines = 672;
				m_VertLines = 896;
				break;
			case VIDEO_RS343_729_1_1 :
				m_HorzLines = 672;
				m_VertLines = 672;
				break;
			case VIDEO_RS343_875_4_3 :
				m_HorzLines =  808;
				m_VertLines = 1080;
				break;
			case VIDEO_RS343_875_1_1 :
				m_HorzLines = 808;
				m_VertLines = 808;
				break;
			case VIDEO_RS343_945_4_3 :
				m_HorzLines =  872;
				m_VertLines = 1160;
				break;
			case VIDEO_RS343_945_1_1 :
				m_HorzLines = 872;
				m_VertLines = 872;
				break;
			case VIDEO_RS343_1023_4_3 :
				m_HorzLines =  944;
				m_VertLines = 1244;
				break;
			case VIDEO_RS343_1023_1_1 :
				m_HorzLines = 944;
				m_VertLines = 944;
				break;
			default :
				break;
		}
	}
	else
	{
		m_HorzLines = 512;
		m_VertLines = 512;
	}

	// calcuate volts/adc count for 8 bit frame grabber
	m_VideoVoltRes = m_Format.Int == VIDEO_RS170 ? (1.0 - 0.075) : (0.714 - 0.054);
	m_VideoVoltRes /= 256.0;

	m_CenterX = m_VertLines / 2;
	m_CenterY = m_HorzLines / 2;

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : ReadLarrsDat
// Purpose  : Reads LARRS specific parameters from file LARRS.DAT
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
//
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//		Void
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::ReadLarrsDat()
{
	int		MessageBoxReturn;
	char	TmpBuf[_MAX_PATH];
	FILE	*fp = NULL;

	sprintf(TmpBuf, LARRS_DAT);

	fp = fopen(TmpBuf, "r");
	if (!fp)
	{
		memset(TmpBuf, '\0', sizeof(TmpBuf));
		sprintf(TmpBuf, "%s%s%s\r\n%s\r\n\t\t%s\r\n\t%s",
				"The file ", LARRS_DAT, " is missing.",
				"Select Retry if the file is restored to its' location,",
				"or", "Cancel to continue the program");
		do {
			MessageBoxReturn = MessageBox(NULL, TmpBuf, "LaRRS.dat is Missing", 
										  MB_RETRYCANCEL | MB_ICONWARNING | MB_DEFBUTTON2 | MB_TOPMOST);
		} while (MessageBoxReturn == IDRETRY);

		if (MessageBoxReturn == IDCANCEL) {
			fclose(fp);
			m_AzPos = 0;
			m_ElPos = 0;
			return;
		}
	}

	fscanf(fp, "%d,%d,%d,%d,%d,%d,%d,%d", &m_AzPos, &m_ElPos, &m_LAreaLo, &m_LAreaHi,
		&m_XCentroidLo, &m_XCentroidHi, &m_YCentroidLo, &m_YCentroidHi);

	fclose(fp);

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : SetCameraPower
// Purpose  : Sets VEO2 camera power to on or off
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// setting             int                   camera setting
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//		Void
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::SetCameraPower(int setting, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int cameraPower = 0;
	int cnt = 0;

	IFNSIMVEO2(SET_CAMERA_POWER_INITIATE(setting));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_CAMERA_POWER(%d);", setting), Response, BufferSize);

	do
	{
		Sleep(1000);

		IFNSIMVEO2(SET_CAMERA_POWER_FETCH(&cameraPower));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_CAMERA_POWER_FETCH(%d);", cameraPower), Response, BufferSize);

		cnt++;
	} while ((cameraPower != setting) && (cnt < 60));

	return;

}

////////////////////////////////////////////////////////////////////////////////
// Function : SetSensorStage
// Purpose  : Sets the sensor stage to one of three positions
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// setting             int                   camera setting
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//		Void
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::SetSensorStage(int setting, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

	int SensorStage = 0;
	int cnt = 0;

	IFNSIMVEO2(SET_SENSOR_STAGE_LASER_INITIATE(setting));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_SENSOR_STAGE_LASER_INITIATE(%d);", setting), Response, BufferSize);

	do
	{
		Sleep(1000);

		IFNSIMVEO2(SET_SENSOR_STAGE_LASER_FETCH(&SensorStage));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("SET_SENSOR_STAGE_LASER_FETCH(%d);", SensorStage), Response, BufferSize);

		cnt++;
	} while ((SensorStage != setting) && (cnt < 60));

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : CalcInputScaleData                                              //
// Purpose  : Calculates voltage range and trigger data for scope and ctr     //
// Return   : Void                                                            //
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::CalculateInputScaleData(void)
{

	float	JoulesConvert = 25;		 // Volts per Joule = ~25
	float	VoltageRange =  0.0;
	double	ConvertValue =  4.90874; // = pi * (diameter /20)^2   diameter = 25mm
									 // 20 = 2 * diameter conversion from cm to mm

	ISDODEBUG(dodebug(0, "CalculateInputScaleData()", "Here in function", (char*)0));

	if (m_MainBeamAtten.Exists)
		m_Atten = (float)(pow(10, m_MainBeamAtten.Real));
	else
		m_Atten = 1.0;

	if (m_PulseEnergy.Exists)
	{
		ISDODEBUG(dodebug(0, "CalculateInputScaleData()", "m_PulseEnergy.Exists", (char*)0));
		VoltageRange = (float)((m_PulseEnergy.Real / m_Atten) * JoulesConvert);
		ISDODEBUG(dodebug(0, "CalculateInputScaleData()", "m_PulseEnergy.Exists VoltageRange = %lf", VoltageRange, (char*)0));
	}
	else if (m_PowerDensity.Exists)
	{
		ISDODEBUG(dodebug(0, "CalculateInputScaleData()", "m_PowerDensity.Exists", (char*)0));
		VoltageRange = (float)(((m_PowerDensity.Real * ConvertValue) / m_Atten) * JoulesConvert);
		ISDODEBUG(dodebug(0, "CalculateInputScaleData()", "m_PowerDensity.Exists VoltageRange = %lf", VoltageRange, (char*)0));
	}
	else
	{
		ISDODEBUG(dodebug(0, "CalculateInputScaleData()", "m_Powerp.Exists and shouldn't be here", (char*)0));
		VoltageRange = (float)((m_PowerP.Real / m_Atten) * m_PulseWidth.Real * JoulesConvert);
		ISDODEBUG(dodebug(0, "CalculateInputScaleData()", "m_Powerp.Exists and shouldn't be here VoltageRange = %lf", VoltageRange, (char*)0));
	}

	m_VoltScale = (VoltageRange <= 0.02) ? 11 :
				  (VoltageRange <= 0.04) ? 10 :
				  (VoltageRange <= 0.1 ) ?  9 :
				  (VoltageRange <= 0.2 ) ?  8 :
				  (VoltageRange <= 0.4 ) ?  7 :
				  (VoltageRange <= 1.0 ) ?  6 :
				  (VoltageRange <= 2.0 ) ?  5 :
				  (VoltageRange <= 4.0 ) ?  4 :
				  (VoltageRange <= 10.0) ?  3 :
				  (VoltageRange <= 20.0) ?  2 :
				  (VoltageRange <= 40.0) ?  1 : 0;

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : CalculateTimeBaseSetting                                              //
// Purpose  : Calculates timebase setting for scope                           //
// Return   : Void                                                            //
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::CalculateTimeBaseSetting(void)
{

	double	TimeBase = 0;

	ISDODEBUG(dodebug(0, "CalculateTimeBaseSetting()", "Here in function", (char*)0));

	if (m_SampleTime.Exists == true) {
		ISDODEBUG(dodebug(0, "CalculateTimeBaseSetting()", "m_SampleTime.Exists", (char*)0));
		TimeBase = m_SampleTime.Real;
		ISDODEBUG(dodebug(0, "CalculateTimeBaseSetting()", "m_SampleTime.Exists TimeBase = %G", TimeBase, (char*)0));
	}
	else if (m_PulseWidth.Exists) {
		ISDODEBUG(dodebug(0, "CalculateTimeBaseSetting()", "m_PulseWidth.Exists", (char*)0));
		TimeBase = m_PulseWidth.Real * 10;
		ISDODEBUG(dodebug(0, "CalculateTimeBaseSetting()", "m_PulseWidth.Exists TimeBase = %G", TimeBase, (char*)0));
	}

	m_TimeBase = (TimeBase <=   5.0e-9) ? 11 :
				 (TimeBase <=  10.0e-9) ? 10 :
				 (TimeBase <=  50.0e-9) ?  9 :
				 (TimeBase <= 100.0e-9) ?  8 :
				 (TimeBase <= 500.0e-9) ?  7 :
				 (TimeBase <=   1.0e-6) ?  6 :
				 (TimeBase <=   5.0e-6) ?  5 :
				 (TimeBase <=  10.0e-6) ?  4 :
				 (TimeBase <=  50.0e-6) ?  3 :
				 (TimeBase <= 100.0e-6) ?  2 :
				 (TimeBase <= 500.0e-6) ?  1 : 0;

	ISDODEBUG(dodebug(0, "CalculateTimeBaseSetting()", "m_TimeBase = %d", m_TimeBase, (char*)0));

	return;
}


//++++//////////////////////////////////////////////////////////////////////////
// Local Static Functions
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// Function: s_IssueDriverFunctionCallVeo2_IrWin
//
// Purpose: Parse the DriverFunction XML and execute requested Driver Function
//
// Input Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// DeviceHandle       int                    Instrument PnP/Vi Handle 
// DriverFunction     ATXMLW_INTF_DRVRFNC*   Pointer to DriverFunction XML
//                                           String
//
// Output Parameters
// Parameter		  Type			         Purpose
// =================  =====================  ===================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return: 
//      void
//
////////////////////////////////////////////////////////////////////////////////
void s_IssueDriverFunctionCallVeo2_IrWin(int DeviceHandle, ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char				Name[ATXMLW_MAX_NAME];
    ATXMLW_DF_VAL		RetVal;
    ATXMLW_XML_HANDLE	DfHandle=NULL;

	memset(Response, '\0', BufferSize);

    if ((atxmlw_ParseDriverFunction(DeviceHandle, DriverFunction, &DfHandle, Response, BufferSize)) ||
       (DfHandle == NULL))
        return;

    atxmlw_GetDFName(DfHandle,Name);
    RetVal.Int32 = 0;

    ///////// Implement Supported Driver Function Calls ///////////////////////
    if(ISDFNAME("TestParam"))
        RetVal.Int32 = s_TestParam(RetInt8(5), RetStrPtr(6));
    else if(ISDFNAME("TestParamAll"))
        s_TestParamAll(RetDbl(11),   RetDblPtr(12),
					   RetInt32(13), RetInt32Ptr(14),
					   RetUInt16(15),RetUInt16Ptr(16),
					   RetUInt8(17), RetUInt8Ptr(18),
					   RetStrPtr(19));
    else if ((strncmp("vi", Name, 2) == 0) && atxmlw_doViDrvrFunc(DfHandle, Name, &RetVal))
        ;
    else
    {
        atxmlw_ErrorResponse("", Response, BufferSize, "IssueDriverFunction ",\
                             ATXMLW_WRAPPER_ERRCD_INVALID_ACTION,\
                             atxmlw_FmtMsg(" Invalid/Unimplemented Function [%s]", Name));
    }

    atxmlw_ReturnDFResponse(DfHandle, RetVal, Response,BufferSize);
    atxmlw_CloseDFXml(DfHandle);
    return;
}

static int s_TestParam(unsigned char *Param5, char *Param6)
{
    strcpy(Param6,"xx yy zz");
    *Param5 = 255;
    return(1);
}

static void s_TestParamAll(double *RetDblVal, double *RetDblArray,
                unsigned long *RetIntVal, unsigned long *RetIntArray,
                unsigned short *RetShortVal, unsigned short *RetShortArray,
                unsigned char *RetCharVal, unsigned char *RetCharArray,
                char *RetStr)
{
    *RetDblVal = 35.6e3;
    RetDblArray[0] = 35.e3;
    RetDblArray[1] = 36.0;
    RetDblArray[2] = 37.0e5;
    RetDblArray[3] = 0.0;
    RetDblArray[4] = 0.0;
    *RetIntVal = 0x55555555;
    RetIntArray[0] = 0xdddddddd;
    RetIntArray[1] = 0xeeeeeeee;
    RetIntArray[2] = 0xffffffff;
    RetIntArray[3] = 0;
    RetIntArray[4] = 0;
    *RetShortVal = 0x5555;
    RetShortArray[0] = 0x4444;
    RetShortArray[1] = 0x5555;
    RetShortArray[2] = 0x6666;
    RetShortArray[3] = 0;
    RetShortArray[4] = 0;
    *RetCharVal = 0x50;
    RetCharArray[0] = 0x99;
    RetCharArray[1] = 0xaa;
    RetCharArray[2] = 0xbb;
    RetCharArray[3] = 0;
    RetCharArray[4] = 0;
    strcpy(RetStr,"xx yy zz");

    return;
}

void ClearAttributeStruct(AttributeStruct & val)
{
	strcpy(val.Dim, "");
	val.Exists = false;
	val.Int = 0;
	val.Real = 0.0;
}

////////////////////////////////////////////////////////////////////////////////
// Function: InitVeo2FuncPtrs(void)
//
// Purpose: Assign function pointers to veo2 dll
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
//
// Return: 
//      void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::InitVeo2FuncPtrs(char * Response, int BufferSize)
{
		char LibName[50] = "c:/irwin2001/veo2.dll";

	memset(Response, '\0', BufferSize);

	IFNSIM(m_Handle = LoadLibrary(LibName));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("%x = LoadLibrary(%s);", m_Handle, LibName),\
		Response, BufferSize);

	// if call to LoadLibrary fails, force simulation mode
	if (!m_Handle)
	{
		m_Sim = true;
		return;
	}

	//////////////////////////////////////////////////
	// IR Sensor function pointers                  //
	//////////////////////////////////////////////////

	BORESIGHT_IR_SETUP = (TN_BORESIGHT_IR_SETUP)GetProcAddress(m_Handle, "BORESIGHT_IR_SETUP");
	BORESIGHT_IR_FETCH = (TN_BORESIGHT_IR_FETCH)GetProcAddress(m_Handle, "BORESIGHT_IR_FETCH");

	CHANNEL_INTEGRITY_IR_SETUP = (TN_CHANNEL_INTEGRITY_IR_SETUP)GetProcAddress(m_Handle, "CHANNEL_INTEGRITY_IR_SETUP");
	CHANNEL_INTEGRITY_IR_FETCH = (TN_CHANNEL_INTEGRITY_IR_FETCH)GetProcAddress(m_Handle, "CHANNEL_INTEGRITY_IR_FETCH");

	GEOMETRIC_FIDELITY_DISTORTION_IR_SETUP = (TN_GEOMETRIC_FIDELITY_DISTORTION_IR_SETUP)GetProcAddress(m_Handle, "GEOMETRIC_FIDELITY_DISTORTION_IR_SETUP");
	GEOMETRIC_FIDELITY_DISTORTION_IR_FETCH = (TN_GEOMETRIC_FIDELITY_DISTORTION_IR_FETCH)GetProcAddress(m_Handle, "GEOMETRIC_FIDELITY_DISTORTION_IR_FETCH");

	IMAGE_UNIFORMITY_IR_SETUP = (TN_IMAGE_UNIFORMITY_IR_SETUP)GetProcAddress(m_Handle, "IMAGE_UNIFORMITY_IR_SETUP");
	IMAGE_UNIFORMITY_IR_FETCH = (TN_IMAGE_UNIFORMITY_IR_FETCH)GetProcAddress(m_Handle, "IMAGE_UNIFORMITY_IR_FETCH");

	MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_SETUP = (TN_MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_SETUP)GetProcAddress(m_Handle, "MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_SETUP");
	MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_FETCH = (TN_MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_FETCH)GetProcAddress(m_Handle, "MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_FETCH");

	MODULATION_TRANSFER_FUNCTION_IR_SETUP = (TN_MODULATION_TRANSFER_FUNCTION_IR_SETUP)GetProcAddress(m_Handle, "MODULATION_TRANSFER_FUNCTION_IR_SETUP");
	MODULATION_TRANSFER_FUNCTION_IR_FETCH = (TN_MODULATION_TRANSFER_FUNCTION_IR_FETCH)GetProcAddress(m_Handle, "MODULATION_TRANSFER_FUNCTION_IR_FETCH");

	NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP = (TN_NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP)GetProcAddress(m_Handle, "NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP");
	NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH = (TN_NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH)GetProcAddress(m_Handle, "NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH");

	//////////////////////////////////////////////////
	// LASER Sensor function pointers               //
	//////////////////////////////////////////////////

	BEAM_DIVERGENCE_LASER_SETUP = (TN_BEAM_DIVERGENCE_LASER_SETUP)GetProcAddress(m_Handle, "BEAM_DIVERGENCE_LASER_SETUP");
	BEAM_DIVERGENCE_LASER_FETCH = (TN_BEAM_DIVERGENCE_LASER_FETCH)GetProcAddress(m_Handle, "BEAM_DIVERGENCE_LASER_FETCH");

	BORESIGHT_LASER_SETUP = (TN_BORESIGHT_LASER_SETUP)GetProcAddress(m_Handle, "BORESIGHT_LASER_SETUP");
	BORESIGHT_LASER_FETCH = (TN_BORESIGHT_LASER_FETCH)GetProcAddress(m_Handle, "BORESIGHT_LASER_FETCH");

	RANGE_FINDER_ACCURACY_LASER_SETUP = (TN_RANGE_FINDER_ACCURACY_LASER_SETUP)GetProcAddress(m_Handle, "RANGE_FINDER_ACCURACY_LASER_SETUP");
	RANGE_FINDER_ACCURACY_LASER_FETCH = (TN_RANGE_FINDER_ACCURACY_LASER_FETCH)GetProcAddress(m_Handle, "RANGE_FINDER_ACCURACY_LASER_FETCH");

	RECEIVER_SENSITIVITY_LASER_SETUP = (TN_RECEIVER_SENSITIVITY_LASER_SETUP)GetProcAddress(m_Handle, "RECEIVER_SENSITIVITY_LASER_SETUP");
	RECEIVER_SENSITIVITY_LASER_FETCH = (TN_RECEIVER_SENSITIVITY_LASER_FETCH)GetProcAddress(m_Handle, "RECEIVER_SENSITIVITY_LASER_FETCH");

	PULSE_REPETITION_FREQUENCY_LASER_SETUP = (TN_PULSE_REPETITION_FREQUENCY_LASER_SETUP)GetProcAddress(m_Handle, "PULSE_REPETITION_FREQUENCY_LASER_SETUP");
	PULSE_REPETITION_FREQUENCY_LASER_FETCH = (TN_PULSE_REPETITION_FREQUENCY_LASER_FETCH)GetProcAddress(m_Handle, "PULSE_REPETITION_FREQUENCY_LASER_FETCH");

	PULSE_WIDTH_LASER_SETUP = (TN_PULSE_WIDTH_LASER_SETUP)GetProcAddress(m_Handle, "PULSE_WIDTH_LASER_SETUP");
	PULSE_WIDTH_LASER_FETCH = (TN_PULSE_WIDTH_LASER_FETCH)GetProcAddress(m_Handle, "PULSE_WIDTH_LASER_FETCH");

	PULSE_ENERGY_MEASUREMENTS_LASER_SETUP = (TN_PULSE_ENERGY_MEASUREMENTS_LASER_SETUP)GetProcAddress(m_Handle, "PULSE_ENERGY_MEASUREMENTS_LASER_SETUP");
	PULSE_ENERGY_MEASUREMENTS_LASER_FETCH = (TN_PULSE_ENERGY_MEASUREMENTS_LASER_FETCH)GetProcAddress(m_Handle, "PULSE_ENERGY_MEASUREMENTS_LASER_FETCH");

	//////////////////////////////////////////////////
	// VIS Sensor function pointers                 //
	//////////////////////////////////////////////////

	BORESIGHT_TV_VIS_SETUP = (TN_BORESIGHT_TV_VIS_SETUP)GetProcAddress(m_Handle, "BORESIGHT_TV_VIS_SETUP");
	BORESIGHT_TV_VIS_FETCH = (TN_BORESIGHT_TV_VIS_FETCH)GetProcAddress(m_Handle, "BORESIGHT_TV_VIS_FETCH");

	GEOMETRIC_FIDELITY_DISTORTION_VIS_SETUP = (TN_GEOMETRIC_FIDELITY_DISTORTION_VIS_SETUP)GetProcAddress(m_Handle, "GEOMETRIC_FIDELITY_DISTORTION_VIS_SETUP");
	GEOMETRIC_FIDELITY_DISTORTION_VIS_FETCH = (TN_GEOMETRIC_FIDELITY_DISTORTION_VIS_FETCH)GetProcAddress(m_Handle, "GEOMETRIC_FIDELITY_DISTORTION_VIS_FETCH");

	CAMERA_UNIFORMITY_VIS_SETUP = (TN_CAMERA_UNIFORMITY_VIS_SETUP)GetProcAddress(m_Handle, "CAMERA_UNIFORMITY_VIS_SETUP");
	CAMERA_UNIFORMITY_VIS_FETCH = (TN_CAMERA_UNIFORMITY_VIS_FETCH)GetProcAddress(m_Handle, "CAMERA_UNIFORMITY_VIS_FETCH");

	GAIN_VIS_SETUP = (TN_GAIN_VIS_SETUP)GetProcAddress(m_Handle, "GAIN_VIS_SETUP");
	GAIN_VIS_FETCH = (TN_GAIN_VIS_FETCH)GetProcAddress(m_Handle, "GAIN_VIS_FETCH");

	MINIMUM_RESOLVABLE_CONTRAST_VIS_SETUP = (TN_MINIMUM_RESOLVABLE_CONTRAST_VIS_SETUP)GetProcAddress(m_Handle, "MINIMUM_RESOLVABLE_CONTRAST_VIS_SETUP");
	MINIMUM_RESOLVABLE_CONTRAST_VIS_FETCH = (TN_MINIMUM_RESOLVABLE_CONTRAST_VIS_FETCH)GetProcAddress(m_Handle, "MINIMUM_RESOLVABLE_CONTRAST_VIS_FETCH");

	MODULATION_TRANSFER_FUNCTION_VIS_SETUP = (TN_MODULATION_TRANSFER_FUNCTION_VIS_SETUP)GetProcAddress(m_Handle, "MODULATION_TRANSFER_FUNCTION_VIS_SETUP");
	MODULATION_TRANSFER_FUNCTION_VIS_FETCH = (TN_MODULATION_TRANSFER_FUNCTION_VIS_FETCH)GetProcAddress(m_Handle, "MODULATION_TRANSFER_FUNCTION_VIS_FETCH");

	SHADES_OF_GRAY_VIS_SETUP = (TN_SHADES_OF_GRAY_VIS_SETUP)GetProcAddress(m_Handle, "SHADES_OF_GRAY_VIS_SETUP");
	SHADES_OF_GRAY_VIS_FETCH = (TN_SHADES_OF_GRAY_VIS_FETCH)GetProcAddress(m_Handle, "SHADES_OF_GRAY_VIS_FETCH");

	NEI_VIS_SETUP = (TN_NEI_VIS_SETUP)GetProcAddress(m_Handle, "NEI_VIS_SETUP");
	NEI_VIS_FETCH = (TN_NEI_VIS_FETCH)GetProcAddress(m_Handle, "NEI_VIS_FETCH");

	//////////////////////////////////////////////////
	// Source Function pointers                     //
	//////////////////////////////////////////////////
	// 3.1
	RESET_MODULE_INITIATE = (TN_RESET_MODULE_INITIATE)GetProcAddress(m_Handle, "RESET_MODULE_INITIATE");

	// 3.2
	GET_BIT_DATA_INITIATE = (TN_GET_BIT_DATA_INITIATE)GetProcAddress(m_Handle, "GET_BIT_DATA_INITIATE");
	GET_BIT_DATA_FETCH = (TN_GET_BIT_DATA_FETCH)GetProcAddress(m_Handle, "GET_BIT_DATA_FETCH");

	// 3.3
	GET_MODULE_ID_INITIATE = (TN_GET_MODULE_ID_INITIATE)GetProcAddress(m_Handle, "GET_MODULE_ID_INITIATE");
	GET_MODULE_ID_FETCH = (TN_GET_MODULE_ID_FETCH)GetProcAddress(m_Handle, "GET_MODULE_ID_FETCH");

	// 3.4
	GET_STATUS_BYTE_MESSAGE_INITIATE = (TN_GET_STATUS_BYTE_MESSAGE_INITIATE)GetProcAddress(m_Handle, "GET_STATUS_BYTE_MESSAGE_INITIATE");
	GET_STATUS_BYTE_MESSAGE_FETCH = (TN_GET_STATUS_BYTE_MESSAGE_FETCH)GetProcAddress(m_Handle, "GET_STATUS_BYTE_MESSAGE_FETCH");

	// 3.5
	GET_TEMP_TARGET_IR_INITIATE = (TN_GET_TEMP_TARGET_IR_INITIATE)GetProcAddress(m_Handle, "GET_TEMP_TARGET_IR_INITIATE");
	GET_TEMP_TARGET_IR_FETCH = (TN_GET_TEMP_TARGET_IR_FETCH)GetProcAddress(m_Handle, "GET_TEMP_TARGET_IR_FETCH");

	// 3.6
	SET_RDY_WINDOW_IR_INITIATE = (TN_SET_RDY_WINDOW_IR_INITIATE)GetProcAddress(m_Handle, "SET_RDY_WINDOW_IR_INITIATE");
	SET_RDY_WINDOW_IR_FETCH = (TN_SET_RDY_WINDOW_IR_FETCH)GetProcAddress(m_Handle, "SET_RDY_WINDOW_IR_FETCH");

	// 3.7
	SET_TARGET_POSITION_INITIATE = (TN_SET_TARGET_POSITION_INITIATE)GetProcAddress(m_Handle, "SET_TARGET_POSITION_INITIATE");
	SET_TARGET_POSITION_FETCH = (TN_SET_TARGET_POSITION_FETCH)GetProcAddress(m_Handle, "SET_TARGET_POSITION_FETCH");

	// 3.8
	SET_TEMP_ABSOLUTE_IR_INITIATE = (TN_SET_TEMP_ABSOLUTE_IR_INITIATE)GetProcAddress(m_Handle, "SET_TEMP_ABSOLUTE_IR_INITIATE");
	SET_TEMP_ABSOLUTE_IR_FETCH = (TN_SET_TEMP_ABSOLUTE_IR_FETCH)GetProcAddress(m_Handle, "SET_TEMP_ABSOLUTE_IR_FETCH");

	// 3.9
	SET_TEMP_DIFFERENTIAL_IR_INITIATE = (TN_SET_TEMP_DIFFERENTIAL_IR_INITIATE)GetProcAddress(m_Handle, "SET_TEMP_DIFFERENTIAL_IR_INITIATE");
	SET_TEMP_DIFFERENTIAL_IR_FETCH = (TN_SET_TEMP_DIFFERENTIAL_IR_FETCH)GetProcAddress(m_Handle, "SET_TEMP_DIFFERENTIAL_IR_FETCH");

	// 3.10
	SET_CAMERA_TRIGGER_LASER_INITIATE = (TN_SET_CAMERA_TRIGGER_LASER_INITIATE)GetProcAddress(m_Handle, "SET_CAMERA_TRIGGER_LASER_INITIATE");
	SET_CAMERA_TRIGGER_LASER_FETCH = (TN_SET_CAMERA_TRIGGER_LASER_FETCH)GetProcAddress(m_Handle, "SET_CAMERA_TRIGGER_LASER_FETCH");

	// 3.11
	SET_CAMERA_DELAY_LASER_INITIATE = (TN_SET_CAMERA_DELAY_LASER_INITIATE)GetProcAddress(m_Handle, "SET_CAMERA_DELAY_LASER_INITIATE");
	SET_CAMERA_DELAY_LASER_FETCH = (TN_SET_CAMERA_DELAY_LASER_FETCH)GetProcAddress(m_Handle, "SET_CAMERA_DELAY_LASER_FETCH");

	// 3.12
	SET_SOURCE_STAGE_LASER_INITIATE = (TN_SET_SOURCE_STAGE_LASER_INITIATE)GetProcAddress(m_Handle, "SET_SOURCE_STAGE_LASER_INITIATE");
	SET_SOURCE_STAGE_LASER_FETCH = (TN_SET_SOURCE_STAGE_LASER_FETCH)GetProcAddress(m_Handle, "SET_SOURCE_STAGE_LASER_FETCH");

	// 3.13
	SET_SENSOR_STAGE_LASER_INITIATE = (TN_SET_SENSOR_STAGE_LASER_INITIATE)GetProcAddress(m_Handle, "SET_SENSOR_STAGE_LASER_INITIATE");
	SET_SENSOR_STAGE_LASER_FETCH = (TN_SET_SENSOR_STAGE_LASER_FETCH)GetProcAddress(m_Handle, "SET_SENSOR_STAGE_LASER_FETCH");

	// 3.16
	SELECT_DIODE_LASER_INITIATE = (TN_SELECT_DIODE_LASER_INITIATE)GetProcAddress(m_Handle, "SELECT_DIODE_LASER_INITIATE");
	SELECT_DIODE_LASER_FETCH = (TN_SELECT_DIODE_LASER_FETCH)GetProcAddress(m_Handle, "SELECT_DIODE_LASER_FETCH");

	// 3.17
	SET_TRIGGER_SOURCE_LASER_INITIATE = (TN_SET_TRIGGER_SOURCE_LASER_INITIATE)GetProcAddress(m_Handle, "SET_TRIGGER_SOURCE_LASER_INITIATE");
	SET_TRIGGER_SOURCE_LASER_FETCH = (TN_SET_TRIGGER_SOURCE_LASER_FETCH)GetProcAddress(m_Handle, "SET_TRIGGER_SOURCE_LASER_FETCH");

	// 3.18
	SET_COARSE_EVOA_LASER_INITIATE = (TN_SET_COARSE_EVOA_LASER_INITIATE)GetProcAddress(m_Handle, "SET_COARSE_EVOA_LASER_INITIATE");
	SET_COARSE_EVOA_LASER_FETCH = (TN_SET_COARSE_EVOA_LASER_FETCH)GetProcAddress(m_Handle, "SET_COARSE_EVOA_LASER_FETCH");

	// 3.19
	SET_FINE_EVOA_LASER_INITIATE = (TN_SET_FINE_EVOA_LASER_INITIATE)GetProcAddress(m_Handle, "SET_FINE_EVOA_LASER_INITIATE");
	SET_FINE_EVOA_LASER_FETCH = (TN_SET_FINE_EVOA_LASER_FETCH)GetProcAddress(m_Handle, "SET_FINE_EVOA_LASER_FETCH");

	// 3.20
	SET_PULSE_AMPLITUDE_LASER_INITIATE = (TN_SET_PULSE_AMPLITUDE_LASER_INITIATE)GetProcAddress(m_Handle, "SET_PULSE_AMPLITUDE_LASER_INITIATE");
	SET_PULSE_AMPLITUDE_LASER_FETCH = (TN_SET_PULSE_AMPLITUDE_LASER_FETCH)GetProcAddress(m_Handle, "SET_PULSE_AMPLITUDE_LASER_FETCH");

	// 3.21
	SET_PULSE_PERIOD_LASER_INITIATE = (TN_SET_PULSE_PERIOD_LASER_INITIATE)GetProcAddress(m_Handle, "SET_PULSE_PERIOD_LASER_INITIATE");
	SET_PULSE_PERIOD_LASER_FETCH = (TN_SET_PULSE_PERIOD_LASER_FETCH)GetProcAddress(m_Handle, "SET_PULSE_PERIOD_LASER_FETCH");

	// 3.23
	SET_RANGE_EMULATION_LASER_INITIATE = (TN_SET_RANGE_EMULATION_LASER_INITIATE)GetProcAddress(m_Handle, "SET_RANGE_EMULATION_LASER_INITIATE");
	SET_RANGE_EMULATION_LASER_FETCH = (TN_SET_RANGE_EMULATION_LASER_FETCH)GetProcAddress(m_Handle, "SET_RANGE_EMULATION_LASER_FETCH");

	// 3.24
	SET_PULSE2_DELAY_LASER_INITIATE = (TN_SET_PULSE2_DELAY_LASER_INITIATE)GetProcAddress(m_Handle, "SET_PULSE2_DELAY_LASER_INITIATE");
	SET_PULSE2_DELAY_LASER_FETCH = (TN_SET_PULSE2_DELAY_LASER_FETCH)GetProcAddress(m_Handle, "SET_PULSE2_DELAY_LASER_FETCH");

	// 3.25
	SELECT_LARGER_PULSE_LASER_INITIATE = (TN_SELECT_LARGER_PULSE_LASER_INITIATE)GetProcAddress(m_Handle, "SELECT_LARGER_PULSE_LASER_INITIATE");
	SELECT_LARGER_PULSE_LASER_FETCH = (TN_SELECT_LARGER_PULSE_LASER_FETCH)GetProcAddress(m_Handle, "SELECT_LARGER_PULSE_LASER_FETCH");

	// 3.26
	SET_PULSE_PERCENTAGE_LASER_INITIATE = (TN_SET_PULSE_PERCENTAGE_LASER_INITIATE)GetProcAddress(m_Handle, "SET_PULSE_PERCENTAGE_LASER_INITIATE");
	SET_PULSE_PERCENTAGE_LASER_FETCH = (TN_SET_PULSE_PERCENTAGE_LASER_FETCH)GetProcAddress(m_Handle, "SET_PULSE_PERCENTAGE_LASER_FETCH");

	// 3.27
	SET_OPERATION_LASER_INITIATE = (TN_SET_OPERATION_LASER_INITIATE)GetProcAddress(m_Handle, "SET_OPERATION_LASER_INITIATE");
	SET_OPERATION_LASER_FETCH = (TN_SET_OPERATION_LASER_FETCH)GetProcAddress(m_Handle, "SET_OPERATION_LASER_FETCH");

	// 3.28
	SET_ANGULAR_RATE_VIS_INITIATE = (TN_SET_ANGULAR_RATE_VIS_INITIATE)GetProcAddress(m_Handle, "SET_ANGULAR_RATE_VIS_INITIATE");
	SET_ANGULAR_RATE_VIS_FETCH = (TN_SET_ANGULAR_RATE_VIS_FETCH)GetProcAddress(m_Handle, "SET_ANGULAR_RATE_VIS_FETCH");

	// 3.29
	SET_RADIANCE_VIS_INITIATE = (TN_SET_RADIANCE_VIS_INITIATE)GetProcAddress(m_Handle, "SET_RADIANCE_VIS_INITIATE");
	SET_RADIANCE_VIS_FETCH = (TN_SET_RADIANCE_VIS_FETCH)GetProcAddress(m_Handle, "SET_RADIANCE_VIS_FETCH");

	// 3.30
	SET_LARRS_AZ_LASER_INITIATE = (TN_SET_LARRS_AZ_LASER_INITIATE)GetProcAddress(m_Handle, "SET_LARRS_AZ_LASER_INITIATE");
	SET_LARRS_AZ_LASER_FETCH = (TN_SET_LARRS_AZ_LASER_FETCH)GetProcAddress(m_Handle, "SET_LARRS_AZ_LASER_FETCH");

	// 3.31
	SET_LARRS_EL_LASER_INITIATE = (TN_SET_LARRS_EL_LASER_INITIATE)GetProcAddress(m_Handle, "SET_LARRS_EL_LASER_INITIATE");
	SET_LARRS_EL_LASER_FETCH = (TN_SET_LARRS_EL_LASER_FETCH)GetProcAddress(m_Handle, "SET_LARRS_EL_LASER_FETCH");

	// 3.32
	SET_LARRS_POLARIZE_LASER_INITIATE = (TN_SET_LARRS_POLARIZE_LASER_INITIATE)GetProcAddress(m_Handle, "SET_LARRS_POLARIZE_LASER_INITIATE");
	SET_LARRS_POLARIZE_LASER_FETCH = (TN_SET_LARRS_POLARIZE_LASER_FETCH)GetProcAddress(m_Handle, "SET_LARRS_POLARIZE_LASER_FETCH");

	// 3.33
	SET_CAMERA_POWER_INITIATE = (TN_SET_CAMERA_POWER_INITIATE)GetProcAddress(m_Handle, "SET_CAMERA_POWER_INITIATE");
	SET_CAMERA_POWER_FETCH = (TN_SET_CAMERA_POWER_FETCH)GetProcAddress(m_Handle, "SET_CAMERA_POWER_FETCH");

	// 3.34
	GET_CALIBRATION_VALUE_INITIATE = (TN_GET_CALIBRATION_VALUE_INITIATE)GetProcAddress(m_Handle, "GET_CALIBRATION_VALUE_INITIATE");
	GET_CALIBRATION_VALUE_FETCH = (TN_GET_CALIBRATION_VALUE_FETCH)GetProcAddress(m_Handle, "GET_CALIBRATION_VALUE_FETCH");

	// 3.35
	SET_SYSTEM_CONFIGURATION_INITIATE = (TN_SET_SYSTEM_CONFIGURATION_INITIATE)GetProcAddress(m_Handle, "SET_SYSTEM_CONFIGURATION_INITIATE");
	SET_SYSTEM_CONFIGURATION_FETCH = (TN_SET_SYSTEM_CONFIGURATION_FETCH)GetProcAddress(m_Handle, "SET_SYSTEM_CONFIGURATION_FETCH");

	// 3.36
	SET_LASER_TEST_INITIATE = (TN_SET_LASER_TEST_INITIATE)GetProcAddress(m_Handle, "SET_LASER_TEST_INITIATE");
	SET_LASER_TEST_FETCH = (TN_SET_LASER_TEST_FETCH)GetProcAddress(m_Handle, "SET_LASER_TEST_FETCH");
	
	// 3.37
	IRWIN_SHUTDOWN = (TN_IRWIN_SHUTDOWN)GetProcAddress(m_Handle, "IRWIN_SHUTDOWN");

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function: ResetVeo2FuncPtrs(void)
//
// Purpose: Assign function pointers to veo2 dll
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  =====================================
//
// Return: 
//      void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_IrWin_T::ResetVeo2FuncPtrs(void)
{
if (m_Handle != NULL)
		IFNSIM(FreeLibrary(m_Handle));
	m_Handle = NULL;

	//////////////////////////////////////////////////
	// IR Sensor function pointers                  //
	//////////////////////////////////////////////////

	BORESIGHT_IR_SETUP = NULL;
	BORESIGHT_IR_FETCH = NULL;

	CHANNEL_INTEGRITY_IR_SETUP = NULL;
	CHANNEL_INTEGRITY_IR_FETCH = NULL;

	GEOMETRIC_FIDELITY_DISTORTION_IR_SETUP = NULL;
	GEOMETRIC_FIDELITY_DISTORTION_IR_FETCH = NULL;

	IMAGE_UNIFORMITY_IR_SETUP = NULL;
	IMAGE_UNIFORMITY_IR_FETCH = NULL;

	MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_SETUP = NULL;
	MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_FETCH = NULL;

	MODULATION_TRANSFER_FUNCTION_IR_SETUP = NULL;
	MODULATION_TRANSFER_FUNCTION_IR_FETCH = NULL;

	NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP = NULL;
	NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH = NULL;

	//////////////////////////////////////////////////
	// LASER Sensor function pointers               //
	//////////////////////////////////////////////////

	BEAM_DIVERGENCE_LASER_SETUP = NULL;
	BEAM_DIVERGENCE_LASER_FETCH = NULL;

	BORESIGHT_LASER_SETUP = NULL;
	BORESIGHT_LASER_FETCH = NULL;

	RANGE_FINDER_ACCURACY_LASER_SETUP = NULL;
	RANGE_FINDER_ACCURACY_LASER_FETCH = NULL;

	RECEIVER_SENSITIVITY_LASER_SETUP = NULL;
	RECEIVER_SENSITIVITY_LASER_FETCH = NULL;

	PULSE_REPETITION_FREQUENCY_LASER_SETUP = NULL;
	PULSE_REPETITION_FREQUENCY_LASER_FETCH = NULL;

	PULSE_WIDTH_LASER_SETUP = NULL;
	PULSE_WIDTH_LASER_FETCH = NULL;

	PULSE_ENERGY_MEASUREMENTS_LASER_SETUP = NULL;
	PULSE_ENERGY_MEASUREMENTS_LASER_FETCH = NULL;

	//////////////////////////////////////////////////
	// VIS Sensor function pointers                 //
	//////////////////////////////////////////////////

	BORESIGHT_TV_VIS_SETUP = NULL;
	BORESIGHT_TV_VIS_FETCH = NULL;

	GEOMETRIC_FIDELITY_DISTORTION_VIS_SETUP = NULL;
	GEOMETRIC_FIDELITY_DISTORTION_VIS_FETCH = NULL;

	CAMERA_UNIFORMITY_VIS_SETUP = NULL;
	CAMERA_UNIFORMITY_VIS_FETCH = NULL;

	GAIN_VIS_SETUP = NULL;
	GAIN_VIS_FETCH = NULL;

	MINIMUM_RESOLVABLE_CONTRAST_VIS_SETUP = NULL;
	MINIMUM_RESOLVABLE_CONTRAST_VIS_FETCH = NULL;

	MODULATION_TRANSFER_FUNCTION_VIS_SETUP = NULL;
	MODULATION_TRANSFER_FUNCTION_VIS_FETCH = NULL;

	SHADES_OF_GRAY_VIS_SETUP = NULL;
	SHADES_OF_GRAY_VIS_FETCH = NULL;

	NEI_VIS_SETUP = NULL;
	NEI_VIS_FETCH = NULL;

	//////////////////////////////////////////////////
	// Source Function pointers                     //
	//////////////////////////////////////////////////
	// 3.1
	RESET_MODULE_INITIATE = NULL;

	// 3.2
	GET_BIT_DATA_INITIATE = NULL;
	GET_BIT_DATA_FETCH = NULL;

	// 3.3
	GET_MODULE_ID_INITIATE = NULL;
	GET_MODULE_ID_FETCH = NULL;

	// 3.4
	GET_STATUS_BYTE_MESSAGE_INITIATE = NULL;
	GET_STATUS_BYTE_MESSAGE_FETCH = NULL;

	// 3.5
	GET_TEMP_TARGET_IR_INITIATE = NULL;
	GET_TEMP_TARGET_IR_FETCH = NULL;

	// 3.6
	SET_RDY_WINDOW_IR_INITIATE = NULL;
	SET_RDY_WINDOW_IR_FETCH = NULL;

	// 3.7
	SET_TARGET_POSITION_INITIATE = NULL;
	SET_TARGET_POSITION_FETCH = NULL;

	// 3.8
	SET_TEMP_ABSOLUTE_IR_INITIATE = NULL;
	SET_TEMP_ABSOLUTE_IR_FETCH = NULL;

	// 3.9
	SET_TEMP_DIFFERENTIAL_IR_INITIATE = NULL;
	SET_TEMP_DIFFERENTIAL_IR_FETCH = NULL;

	// 3.10
	SET_CAMERA_TRIGGER_LASER_INITIATE = NULL;
	SET_CAMERA_TRIGGER_LASER_FETCH = NULL;

	// 3.11
	SET_CAMERA_DELAY_LASER_INITIATE = NULL;
	SET_CAMERA_DELAY_LASER_FETCH = NULL;

	// 3.12
	SET_SOURCE_STAGE_LASER_INITIATE = NULL;
	SET_SOURCE_STAGE_LASER_FETCH = NULL;

	// 3.13
	SET_SENSOR_STAGE_LASER_INITIATE = NULL;
	SET_SENSOR_STAGE_LASER_FETCH = NULL;

	// 3.16
	SELECT_DIODE_LASER_INITIATE = NULL;
	SELECT_DIODE_LASER_FETCH = NULL;

	// 3.17
	SET_TRIGGER_SOURCE_LASER_INITIATE = NULL;
	SET_TRIGGER_SOURCE_LASER_FETCH = NULL;

	// 3.18
	SET_COARSE_EVOA_LASER_INITIATE = NULL;
	SET_COARSE_EVOA_LASER_FETCH = NULL;

	// 3.19
	SET_FINE_EVOA_LASER_INITIATE = NULL;
	SET_FINE_EVOA_LASER_FETCH = NULL;

	// 3.20
	SET_PULSE_AMPLITUDE_LASER_INITIATE = NULL;
	SET_PULSE_AMPLITUDE_LASER_FETCH = NULL;

	// 3.21
	SET_PULSE_PERIOD_LASER_INITIATE = NULL;
	SET_PULSE_PERIOD_LASER_FETCH = NULL;

	// 3.23
	SET_RANGE_EMULATION_LASER_INITIATE = NULL;
	SET_RANGE_EMULATION_LASER_FETCH = NULL;

	// 3.24
	SET_PULSE2_DELAY_LASER_INITIATE = NULL;
	SET_PULSE2_DELAY_LASER_FETCH = NULL;

	// 3.25
	SELECT_LARGER_PULSE_LASER_INITIATE = NULL;
	SELECT_LARGER_PULSE_LASER_FETCH = NULL;

	// 3.26
	SET_PULSE_PERCENTAGE_LASER_INITIATE = NULL;
	SET_PULSE_PERCENTAGE_LASER_FETCH = NULL;

	// 3.27
	SET_OPERATION_LASER_INITIATE = NULL;
	SET_OPERATION_LASER_FETCH = NULL;

	// 3.28
	SET_ANGULAR_RATE_VIS_INITIATE = NULL;
	SET_ANGULAR_RATE_VIS_FETCH = NULL;

	// 3.29
	SET_RADIANCE_VIS_INITIATE = NULL;
	SET_RADIANCE_VIS_FETCH = NULL;

	// 3.30
	SET_LARRS_AZ_LASER_INITIATE = NULL;
	SET_LARRS_AZ_LASER_FETCH = NULL;

	// 3.31
	SET_LARRS_EL_LASER_INITIATE = NULL;
	SET_LARRS_EL_LASER_FETCH = NULL;

	// 3.32
	SET_LARRS_POLARIZE_LASER_INITIATE = NULL;
	SET_LARRS_POLARIZE_LASER_FETCH = NULL;

	// 3.33
	SET_CAMERA_POWER_INITIATE = NULL;
	SET_CAMERA_POWER_FETCH = NULL;

	// 3.34
	GET_CALIBRATION_VALUE_INITIATE = NULL;
	GET_CALIBRATION_VALUE_FETCH = NULL;

	// 3.35
	SET_SYSTEM_CONFIGURATION_INITIATE = NULL;
	SET_SYSTEM_CONFIGURATION_FETCH = NULL;

	// 3.36
	SET_LASER_TEST_INITIATE = NULL;
	SET_LASER_TEST_FETCH = NULL;
	
	// 3.37
	IRWIN_SHUTDOWN = NULL;

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : GetErrorMessage(int, char[]);                                   //
// Purpose  : Return text for error code                                      //
////////////////////////////////////////////////////////////////////////////////
void GetErrorMessage(int ecode, char *msg)
{

	int		ErrorMessageIndx;
	int		DefaultMessage = 40;

	char	*ErrorMessages[] = {
		"",
		"The blackbody temperature is aboce its temperature range.",		//ecode 01
		"After stabilizing the blackbody has moved outof the ready window.",//ecode 02
		"The blackbody is unable to reach its set point",					//ecode 03
		"An unrecognized command has been received over the remote bus.",	//ecode 04
		"An out-of-range command has been received.",						//ecode 05
		"EEPROM confidence check has failed.",								//ecode 10
		"Internal communication has failed.",								//ecode 11
		"Internal execution error.",										//ecode 12
		"Invalid calibration point.",										//ecode 22
		"Shorted thermistor",												//ecode 23
		"Open thermistor",													//ecode 24
		"EEPROM dropout",													//ecode 25
		"ADC2 failed",														//ecode 26
		"ADC1 failed",														//ecode 27
		"Serial buffer overflow",											//ecode 28
		"EEPROM checksum error",											//ecode 29
		"Detent not set while in idle",										//ecode 30
		"Time-out, motor not in position",									//ecode 31
		"DAC failure",														//ecode 34
		"Power Amplifier failure",											//ecode 35
		"Thermoelectric Cooler (TEC) failure",								//ecode 36
		"Power supplies failure",											//ecode 37
		"Reset failure",													//ecode 38
		"Following error, rotor not keeping up with commanded velocity",	//ecode 39
		"Encoder BIT failure",												//ecode 40
		"Any lamp failure",													//ecode 42
		"General communications error",										//ecode 45
		"Fan failure",														//ecode 46
		"Camera failure",													//ecode 47
		"Camera message error",												//ecode 48
		"Trigger failure",													//ecode 49
		"Interlock not set",												//ecode 50
		"Power Supply 1 failure",											//ecode 51
		"Power Supply 2 failure",											//ecode 52
		"The second Smart Probe has failed (Collimator)",					//ecode 91
		"The first Smart Probe has failed",									//ecode 95
		"The power amplifier card has failed",								//ecode 97
		"The GCC has failed",												//ecode 98
		"The ACC has failed",												//ecode 99
		"An improper error code received",									//ecode ??
	};

	ErrorMessageIndx = (ecode >  0 && ecode <   6) ? ecode      :
					   (ecode >  9 && ecode <  13) ? ecode - 4  :
					   (ecode > 21 && ecode <  32) ? ecode - 13 :
					   (ecode > 33 && ecode <  41) ? ecode - 15 :
					   (ecode > 41 && ecode <  43) ? ecode - 16 :
					   (ecode > 44 && ecode <  53) ? ecode - 18 :
					   (ecode > 90 && ecode <  92) ? ecode - 56 :
					   (ecode > 94 && ecode <  96) ? ecode - 59 :
					   (ecode > 96 && ecode < 100) ? ecode - 60 : 
					   DefaultMessage;

	sprintf(msg, ErrorMessages[ErrorMessageIndx]);

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : GetLsrStatusMessage(int, LsrErrMsg, char*);                     //
// Purpose  : Return text for error code                                      //
////////////////////////////////////////////////////////////////////////////////
void GetLsrStatusMessage(int Status, LsrErrMsg LsrMeasType, char *msg)
{

	int		ErrorMessageIndx;
	int		DefaultMessage = 12;

	char	*ErrorMessages[] = {
		"",
		"BIT error",
		"Number of Measurements out of range",
		"SC_Input_Channel out of rangee",
		"SC_Input_Range out of range",
		"SC_Timebase out of range",
		"SC_Trigger_Source out of range",
		"SC_Trigger_Slope out of range",
		"SC_Trigger_Level out of range",
		"TC_Trigger_Slope out of range",
		"TC_Trigger_Level out of range",
		"TC_Delay out of range",
		"Unknown error"
	};

	ErrorMessageIndx =	((Status & 0x0001) == 0x0001) ? 1 :
						((Status & 0x0002) == 0x0002) ? 2 :
						(LsrMeasType == PulseWidLsr && (Status & 0x0004) == 0x0004) ?  3 :
						(LsrMeasType == PulseWidLsr && (Status & 0x0008) == 0x0008) ?  4 :
						(LsrMeasType == PulseWidLsr && (Status & 0x0010) == 0x0010) ?  5 :
						(LsrMeasType == PulseWidLsr && (Status & 0x0020) == 0x0020) ?  6 :
						(LsrMeasType == PulseWidLsr && (Status & 0x0040) == 0x0040) ?  7 :
						(LsrMeasType == PulseWidLsr && (Status & 0x0080) == 0x0080) ?  8 :
						(LsrMeasType == PulseRepLsr && (Status & 0x0004) == 0x0004) ?  9 :
						(LsrMeasType == PulseRepLsr && (Status & 0x0008) == 0x0008) ? 10 :
						(LsrMeasType == PulseRepLsr && (Status & 0x0010) == 0x0010) ? 11 :
						DefaultMessage;

	sprintf(msg, ErrorMessages[ErrorMessageIndx]);

	return;
}


////////////////////////////////////////////////////////////////////////////////
// Function : GetIrStatusMessage(int, char *, char*);                         //
// Purpose  : Return text for error code                                      //
////////////////////////////////////////////////////////////////////////////////
void GetIrStatusMessage(int Status, IrErrMsg IrMeasType, char *msg)
{

	int		ErrorMessageIndx;
	int		DefaultMessage = 26;

	char	*ErrorMessages[] = {
		"",
		"BIT error",
		"Number of Image Frames out of range",
		"Hor Field of View out of range",
		"Vert Field of View out of range",
		"Diff Temp out of range",
		"Target Position out of range",
		"Center X or Y coordinate out of range",
		"Signal Block coordinates of Upper left corner exceed Lower right corner",
		"Unknown camera",
		"Intensity Ratio out of range",
		"Orientation",
		"Pedestal_Filter out of range",
		"Smoothing out of range",
		"Num_Freq_Points out of range",
		"Correction Curve out of range",
		"Num positions out of range",
		"Position out of range",
		"Begin_Temp > End_Temp",
		"Begin_Temp out of range",
		"End_Temp out of range",
		"Temp_Interval out of range",
		"Ambient Block out of range",
		"Lines_Per_Channel out of range",
		"Lines_First_Channel out of range",
		"Noise Criteria is out of range",
		"Unknown error"
	};

	ErrorMessageIndx =	((Status & 0x0001) == 0x0001) ? 1 :
						((Status & 0x0002) == 0x0002) ? 2 :
						((Status & 0x0004) == 0x0004) ? 3 :
						((Status & 0x0008) == 0x0008) ? 4 :
						((Status & 0x0010) == 0x0010) ? 5 :
						((Status & 0x0020) == 0x0020) ? 6 :
						((Status & 0x0040) == 0x0040) ? 7 :
						((Status & 0x0080) == 0x0080) ? 8 :
						((Status & 0x0100) == 0x0100) ? 9 :
						(IrMeasType == BoreSightIr  && (Status & 0x0200) == 0x0200) ? 10 :
						(IrMeasType == ModTranFunIr && (Status & 0x0200) == 0x0200) ? 11 :
						(IrMeasType == ModTranFunIr && (Status & 0x0400) == 0x0400) ? 12 :
						(IrMeasType == ModTranFunIr && (Status & 0x0800) == 0x0800) ? 13 :
						(IrMeasType == ModTranFunIr && (Status & 0x1000) == 0x1000) ? 14 :
						(IrMeasType == ModTranFunIr && (Status & 0x2000) == 0x2000) ? 15 :
						(IrMeasType == GeoFidDistIr && (Status & 0x0200) == 0x0200) ? 16 :
						(IrMeasType == GeoFidDistIr && (Status & 0x0400) == 0x0400) ? 17 :
						(IrMeasType == NoiseEquivIr && (Status & 0x0200) == 0x0200) ? 18 :
						(IrMeasType == NoiseEquivIr && (Status & 0x0400) == 0x0400) ? 19 :
						(IrMeasType == NoiseEquivIr && (Status & 0x0800) == 0x0800) ? 20 :
						(IrMeasType == NoiseEquivIr && (Status & 0x1000) == 0x1000) ? 21 :
						(IrMeasType == NoiseEquivIr && (Status & 0x2000) == 0x2000) ? 22 :
						(IrMeasType == ChanIntegIr  && (Status & 0x0200) == 0x0200) ? 23 :
						(IrMeasType == ChanIntegIr  && (Status & 0x0400) == 0x0400) ? 24 :
						(IrMeasType == ChanIntegIr  && (Status & 0x0800) == 0x0800) ? 25 :
						DefaultMessage;

	sprintf(msg, ErrorMessages[ErrorMessageIndx]);

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : GetCat1LaserStatusMessage(int, char *, char *);                 //
// Purpose  : Gets category 1 laser status messges.                           //
////////////////////////////////////////////////////////////////////////////////
void GetCat1LaserStatusMessage(int Status, char * msg)
{

	int		ErrorMessageIndx;
	int		DefaultMessage = 7;

	char	*ErrorMessages[] = {
		"",
		"BIT error",
		"Number of Image Frames out of range",
		"Signal block uppper left > lower right",
		"Camera Delay Time out of range",
		"Camera Trigger out of range",
		"Intensity Ratio out of range",
		"Unknown error"
	};


	ISDODEBUG(dodebug(0, "GetCat1LaserStatusMessage()", "In GetCat1LaserStatusMessageLaser", (char*)0));

	ErrorMessageIndx =	((Status & 0x0001) == 0x0001) ?  1 :
						((Status & 0x0002) == 0x0002) ?  2 :
						((Status & 0x0004) == 0x0004) ?  3 :
						((Status & 0x0008) == 0x0008) ?  4 :
						((Status & 0x0010) == 0x0010) ?  5 :
						((Status & 0x0020) == 0x0020) ?  6 :
						DefaultMessage;

	sprintf(msg, ErrorMessages[ErrorMessageIndx]);

	ISDODEBUG(dodebug(0, "GetCat1LaserStatusMessage()", "msg (%s)", msg, (char*)0));

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : GetCat2LaserStatusMessage(int, char *, char *);                 //
// Purpose  : Gets category 2 laser status messges.                           //
////////////////////////////////////////////////////////////////////////////////
void GetCat2LaserStatusMessage(int Status, char * msg)
{

	int		ErrorMessageIndx;
	int		DefaultMessage = 11;

	char	*ErrorMessages[] = {
		"",
		"BIT error",
		"Trigger Mode out of range",
		"Diode Selection out of range",
		"Pulse Amplitude out of range",
		"Pulse Period out of range",
		"Pulse Width out of range",
		"Emulated Range out of range",
		"Pulse 2 Delay out of range",
		"Pulse Select out of range",
		"Amplitude Percentage out of range",
		"Unknown error",
	};


	ErrorMessageIndx =	((Status & 0x0001) == 0x0001) ?  1 :
						((Status & 0x0002) == 0x0002) ?  2 :
						((Status & 0x0004) == 0x0004) ?  3 :
						((Status & 0x0008) == 0x0008) ?  4 :
						((Status & 0x0010) == 0x0010) ?  5 :
						((Status & 0x0020) == 0x0020) ?  6 :
						((Status & 0x0040) == 0x0040) ?  7 :
						((Status & 0x0080) == 0x0080) ?  8 :
						((Status & 0x0100) == 0x0100) ?  9 :
						((Status & 0x0200) == 0x0200) ? 10 :
						DefaultMessage;

	sprintf(msg, ErrorMessages[ErrorMessageIndx]);

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : GetVisStatusMessage(int, char *, char *);                       //
// Purpose  : Gets visual status messges.                                     //
////////////////////////////////////////////////////////////////////////////////
void GetVisStatusMessage(int Status, VisErrMsg VisMeasType, char * msg)
{
	int		ErrorMessageIndx;
	int		DefaultMessage = 28;

	char	*ErrorMessages[] = {
		"",
		"Number_of_steps out of range",
		"Starting_Radiance out of range",
		"Target_Position out of range",
		"Target_Feature out of range",
		"Intensity Ratio out of range",
		"Orientation",
		"Pedestal_Filter out of range",
		"Smoothing out of range",
		"Numb_Freq_Points out of range",
		"Correction_Curve out of range",
		"Numb positions out of range",
		"Begin_Rad, End-Rad, or Rad_Interval out of range; or Begin_Rad>End_Rad",
		"Begin Rad > End Rad",
		"Begin Rad out of range",
		"End Rad out of range",
		"Rad Interval out of range",
		"Ambient Block out of range",
		"BIT error",
		"Image_Num_Frames out of range",
		"H_Field_Of_View out of range",
		"V_Field_Of_View out of range",
		"Radiance out of range",
		"Target Position out of range",
		"Center X or Y coordinate out of range",
		"Signal Block coordinates of upper left corner exceeds lower right corner",
		"Unknown Camera out of range",
		"Color Temperature out of range",
		"Unknown Error"
	};

	ErrorMessageIndx =	(VisMeasType == MinResolCont && (Status & 0x0002) == 0x0001) ?  1 :
						(VisMeasType == MinResolCont && (Status & 0x0004) == 0x0002) ?  2 :
						(VisMeasType == MinResolCont && (Status & 0x0008) == 0x0004) ?  3 :
						(VisMeasType == MinResolCont && (Status & 0x0010) == 0x0008) ?  4 :
						(VisMeasType == BoreSightTv  && (Status & 0x0400) == 0x0400) ?  5 :
						(VisMeasType == ModTranFunVi && (Status & 0x0400) == 0x0400) ?  6 :
						(VisMeasType == ModTranFunVi && (Status & 0x0800) == 0x0800) ?  7 :
						(VisMeasType == ModTranFunVi && (Status & 0x1000) == 0x1000) ?  8 :
						(VisMeasType == ModTranFunVi && (Status & 0x2000) == 0x2000) ?  9 :
						(VisMeasType == ModTranFunVi && (Status & 0x4000) == 0x4000) ? 10 :
						(VisMeasType == GeoFidDistVi && (Status & 0x0800) == 0x0800) ? 11 :
						(VisMeasType == GainVisible  && (Status & 0x0400) == 0x0400) ? 12 :
						(VisMeasType == ShadeOfGray  && (Status & 0x0400) == 0x0400) ? 13 :
						(VisMeasType == ShadeOfGray  && (Status & 0x0800) == 0x0800) ? 14 :
						(VisMeasType == ShadeOfGray  && (Status & 0x1000) == 0x1000) ? 15 :
						(VisMeasType == ShadeOfGray  && (Status & 0x2000) == 0x2000) ? 16 :
						(VisMeasType == ShadeOfGray  && (Status & 0x4000) == 0x4000) ? 17 :
						((Status & 0x0001) == 0x0001) ? 18 :
						((Status & 0x0002) == 0x0002) ? 19 :
						((Status & 0x0004) == 0x0004) ? 20 :
						((Status & 0x0008) == 0x0008) ? 21 :
						((Status & 0x0010) == 0x0010) ? 22 :
						((Status & 0x0020) == 0x0020) ? 23 :
						((Status & 0x0040) == 0x0040) ? 24 :
						((Status & 0x0080) == 0x0080) ? 25 :
						((Status & 0x0100) == 0x0100) ? 26 :
						((Status & 0x0200) == 0x0200) ? 27 :
						DefaultMessage;

	sprintf(msg, ErrorMessages[ErrorMessageIndx]);

	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : CalcPulse2Delay(double, double);                                //
// Purpose  : Converts target range to time delay for laser.                  //
// Return   : delay in nsec.                                                  //
////////////////////////////////////////////////////////////////////////////////
float CalcPulse2Delay(double target, double pulse2)
{
	int		IntTimeValue;
	int		StepIncrment = 20;
	float	distance;
	float	time;

	distance = (float)(pulse2 - target);

	time = (float)((distance / 0.299792458) - 9.9999);

	IntTimeValue = (int)time;

	ISDODEBUG(dodebug(0, "SetupVeo2_IrWin()", "target range = %lf and last pulse = %lf",\
					  target, pulse2, (char*)0));
//
// Here find time in steps of 20, that is what API takes.
// If over a step of 20 then go to the next step of 20.
//

	if ((IntTimeValue % StepIncrment) > 0 || (time - IntTimeValue > 0.0)) {
		IntTimeValue = (int)(time / StepIncrment) + 1;
		time = (float)IntTimeValue * StepIncrment;
	}

	return time;
}

////////////////////////////////////////////////////////////////////////////////
// Function : dodebug(int, char*, char* format, ...);                         //
// Purpose  : Print the message to a debug file.                              //
// Return   : None if it don't work o well                                    //
////////////////////////////////////////////////////////////////////////////////
void dodebug(int code, char *function_name, char *format, ...)
{

	static int		FirstRun = 0;
	char			TmpBuf[_MAX_PATH];

	if (DE_BUG == 1 && FirstRun == 0) {

		sprintf(TmpBuf, "%s", DEBUGIT_VEO2);
		if ((debugfp = fopen(TmpBuf, "w+b")) == NULL) {
			DE_BUG = 0;
			return;
		}
		FirstRun++;
	}

	va_list	arglist;

	if (DE_BUG) {

		if (code != 0) {
			fprintf(debugfp, "%s in %s\r\n", strerror(code), function_name);
		}
		else {
			va_start(arglist, format);
			fprintf(debugfp, "In function %s ", function_name);
			vfprintf(debugfp, format, arglist);
			fprintf(debugfp, "\r\n");
			va_end(arglist);
		}

		fflush(debugfp);
	}

	return;
}

#pragma warning(default : 4100)
