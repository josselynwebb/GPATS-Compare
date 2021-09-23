//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Switch.cpp
//
// Purpose:	    Instrument Driver for Switch
//
// Instrument:	Switch   
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     cem.lib              $(TYXROOT)\usr\tyx\lib (TYX CEM Library functions)
//     cemsupport.lib       ..\..\Common\lib  (ARC CEM support functions)
//
// ATLAS Subset: PAWS-89
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline Release                        
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "swxsrvr.h"

// Local Defines
#define MAX_DEVICES  1
#define CAL_TIME       (86400 * 365) /* one year */
// Static Variables
static bool s_DoneOnce = FALSE;
static int   m_Sim=0, m_Dbg=0;
static char *m_DeviceName = "SWITCH";


// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

//++++/////////////////////////////////////////////////////////////////////////
// Callback Functions moved from "ctlr.cpp"
///////////////////////////////////////////////////////////////////////////////

//BEGIN{DFW}:IFC
int CCALLBACK doIfc (void)
//END{DFW}
{
    //char *ErrMsg;

	//ResetSwitch();
    // Reset the UUT Power Enable Flag
    //ClearUniqStationStatus(&ErrMsg);
	cs_SysResetDevice("PAWS_SWITCH");


    CEMDEBUG(5,cs_FmtMsg("Switch Reset via doIfc(), "
                        "Sim %d, Dbg %d", 
                         m_Sim, m_Dbg));
	return 0;
}

//BEGIN{DFW}:DCL
int CCALLBACK doDcl (void)
//END{DFW}
{
    return 0;
}

//BEGIN{DFW}:OPEN
int CCALLBACK doOpen (void)
//END{DFW}
{
    char *ErrMsg;
    char Response[4096];

    // DoOpen called prior to each NAM execution
    if(s_DoneOnce)
    {
        return 0;
    }
    s_DoneOnce = TRUE;


	// inhibit multiple reset calls
	DclResetDisable("AllDevices");
/**/
    if( SetCurDev( "SWITCH:CH1" ) == OSRSOK )
    {
        if( IsOkCurDevAddr() )
        {
            // Save for Debug Flag
            m_Dbg = GetCurDevDbgLvl();
            // Parse for Simulator Flag
            m_Sim = GetCurDevSimLvl();
        }
    }
/**/

    CEMDEBUG(5,cs_FmtMsg("Switch Initialized via doOpen(), "
                        "Sim %d, Dbg %d", 
                          m_Sim, m_Dbg));
	InitSwitch(m_Dbg, m_Sim, Response, 4096);
    cs_ParseResponseError(Response, NULL, 0);

    // Reset the UUT Power Enable Flag
    ClearUniqStationStatus(&ErrMsg);

    return 0;
}

//BEGIN{DFW}:DISCONNECT
int CCALLBACK doUnload (void)
//END{DFW}
{
    char *ErrMsg;

    // Reset the UUT Power Enable Flag
    ClearUniqStationStatus(&ErrMsg);
    // Clean up Unique Station stuff
    CloseUniqStationStatus();
	ReleaseSwitch();

    return 0;
}

//BEGIN{DFW}:INTERRUPT
int CCALLBACK doIntr ()
//END{DFW}
{
    char *ErrMsg;

    if(CheckUniqStationStatus(&ErrMsg) < 0)
    {
        // Place system actions for errors here
        CEMERROR(EB_SEVERITY_ERROR | EB_ACTION_RESET,"Oops, Intr Tripped");
    }
	return 0;
}

//++++/////////////////////////////////////////////////////////////////////////
// Wizard Generated Functions
///////////////////////////////////////////////////////////////////////////////

//BEGIN{DFW}:Switch:1:7
int doSWITCH_Connect (void)
//END{DFW}
{
//char *ErrMsg;
//CheckUniqStationStatus(&ErrMsg);
    cs_DoSwitching(M_PATH);
    return (int) 0;
}

//BEGIN{DFW}:Switch:1:8
int doSWITCH_Disconnect (void)
//END{DFW}
{
    cs_DoSwitching(M_PATH);

	return (int) 0;
}

//BEGIN{DFW}:SWITCH:2:0
int doSWITCH_Setup ()
//END{DFW}
{
char *ErrMsg;
    CheckUniqStationStatus(&ErrMsg);

	// please insert your CEM driver code here

	return 0;
}

//BEGIN{DFW}:SWITCH:2:5
int doSWITCH_Close ()
//END{DFW}
{
	// please insert your CEM driver code here
	return 0;
}

//BEGIN{DFW}:SWITCH:2:6
int doSWITCH_Open ()
//END{DFW}
{
	// please insert your CEM driver code here

	return 0;
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


