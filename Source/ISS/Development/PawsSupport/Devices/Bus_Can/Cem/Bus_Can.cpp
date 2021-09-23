//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_Can.cpp
//
// Date:	    26-JUN-06
//
// Purpose:	    Instrument Driver for DeviceXxx
//
// Instrument:	Bus_Can  <device description> (CAN)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     cem.lib              \usr\tyx\lib (TYX CEM Library functions)
//     cemsupport.lib       ..\..\Common\lib  (ARC CEM support functions)
//
// ATLAS Subset: All
//
//
//       This initial program was generated via the CEM Wizard.
//       The "ctlr.cpp" functions were copied to this file to
//       allow the use of "static" variables vs. "global" variables.
//
// Revision History described in Bus_Can_T.cpp
// 1.1.0.0  30MAY19  Updated for CAN bus Start problem        J. Witcher
//
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include <limits.h>
#include <float.h>
#include "cemsupport.h"
#include "Bus_Can_T.h"

// Local Defines
#define IEEE_716 /*using 716.89 compiler with CLOSE/OPEN missing*/

#define MAX_DEVICES  1
// Static Variables
static int        s_NumDev;
static CS_DEVINFO s_DevInfo[MAX_DEVICES]; // Device and BusConfi information
static int        s_DoDclCount = 0;

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

//++++/////////////////////////////////////////////////////////////////////////
// Callback Functions moved from "ctlr.cpp"
///////////////////////////////////////////////////////////////////////////////

//BEGIN{DFW}:DCL
int CCALLBACK doDcl (void)
//END{DFW}
{
    if(s_DevInfo[s_DoDclCount].DriverClass != NULL)
	{
		((CBus_Can_T *)(s_DevInfo[s_DoDclCount].DriverClass))->ResetBus_Can(0);
	}

	// Signal system reset in process
	cs_SysResetDevice(s_DevInfo[s_DoDclCount].DeviceName);

    s_DoDclCount++;
    
	if(s_DoDclCount >= s_NumDev)
	{
        s_DoDclCount = 0;
	}

    return 0;
}

//BEGIN{DFW}:OPEN
int CCALLBACK doOpen (void)
//END{DFW}
{
    int i;
    // DoOpen called prior to each NAM execution
    static bool DoneOnce = FALSE;
    
	if(DoneOnce)
    {
        return 0;
    }

    DoneOnce = TRUE;


	// inhibit multiple reset calls
	DclResetDisable("AllDevices");

    // Check if already initialized
    if(s_NumDev)
    {
        return(0);
    }

    memset(s_DevInfo,0,sizeof(s_DevInfo));

    s_NumDev = cs_GetDevInfo("CAN", 1, s_DevInfo, MAX_DEVICES);
    
	if(s_NumDev == 0)
    {
        // Error can't find instrument
        return(0);
    }

    for(i = 0; i < s_NumDev; i++)
    {
        s_DevInfo[i].DriverClass = new CBus_Can_T(
                              s_DevInfo[i].DeviceName,
                              s_DevInfo[i].BusNo,
                              s_DevInfo[i].PrimaryAdr,
                              s_DevInfo[i].SecondaryAdr,
                              s_DevInfo[i].Dbg, s_DevInfo[i].Sim);

    }

	return 0;
}

//BEGIN{DFW}:DISCONNECT
int CCALLBACK doUnload (void)
//END{DFW}
{
    int i;

    for(i = 0; i < s_NumDev; i++)
    {
        if(s_DevInfo[i].DriverClass != NULL)
		{
            delete((CBus_Can_T *)(s_DevInfo[i].DriverClass));
		}

        s_DevInfo[i].DriverClass = NULL;

    }

    s_NumDev = 0;

	return 0;
}

//++++/////////////////////////////////////////////////////////////////////////
// Wizard Generated Functions
///////////////////////////////////////////////////////////////////////////////

//BEGIN{DFW}:Bus_Can:1:0
int doCAN_Setup (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);
    
	// Any Errors already diagnosed
    if(Status == 0)
	{
        ((CBus_Can_T *)(DriverClass))->SetupBus_Can(Fnc);
	}

#ifdef IEEE_716
    // Any Errors already diagnosed
    Status = ((CBus_Can_T *)(DriverClass))->CloseBus_Can(Fnc);
#endif

	return (int)0;
}

//BEGIN{DFW}:Bus_Can:1:1
int doCAN_Status (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
	{
        ((CBus_Can_T *)(DriverClass))->StatusBus_Can(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:Bus_Can:1:2
int doCAN_Fetch (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);
    
	// Any Errors already diagnosed
    if(Status == 0)
	{
        ((CBus_Can_T *)(DriverClass))->FetchBus_Can(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:Bus_Can:1:3
int doCAN_Init (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
        ((CBus_Can_T *)(DriverClass))->InitiateBus_Can(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:DBus_Can:1:4
int doCAN_Reset (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

#ifdef IEEE_716
    // Any Errors already diagnosed
    Status = ((CBus_Can_T *)(DriverClass))->OpenBus_Can(Fnc);
#endif

    // Any Errors already diagnosed
    if(Status == 0)
	{
        ((CBus_Can_T *)(DriverClass))->ResetBus_Can(Fnc);
	}
	
	return (int) 0;
}

//BEGIN{DFW}:Bus_Can:1:5
int doCAN_Close (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
        ((CBus_Can_T *)(DriverClass))->CloseBus_Can(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:Bus_Can:1:6
int doCAN_Open (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
        ((CBus_Can_T *)(DriverClass))->OpenBus_Can(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:Bus_Can:1:7
int doCAN_Connect (void)
//END{DFW}
{
    cs_DoSwitching(M_PATH);
    return (int) 0;
}

//BEGIN{DFW}:Bus_Can:1:8
int doCAN_Disconnect (void)
//END{DFW}
{
    cs_DoSwitching(M_PATH);

	return (int) 0;
}


//BEGIN{DFW}:Bus_Can:1:9
int doCAN_Load (void)
//END{DFW}
{

	return (int) 0;
}


//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


