//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_Ethernet_Gigabit.cpp
//
// Date:	    11-OCT-05
//
// Purpose:	    Instrument Driver for Bus_Ethernet_Gigabit
//
// Instrument:	Bus_Ethernet_Gigabit  <device description> (ETHERNET)
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
// Revision History described in Bus_Ethernet_Gigabit_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include <limits.h>
#include <float.h>
#include "cemsupport.h"
#include "Bus_Ethernet_Gigabit_T.h"

// Local Defines
#define IEEE_716 /*using 716.89 compiler with CLOSE/OPEN missing*/

#define MAX_DEVICES  2
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
            ((CBus_Ethernet_Gigabit_T *)(s_DevInfo[s_DoDclCount].DriverClass))->ResetBus_Ethernet_Gigabit(0);

	// Signal system reset in process
	cs_SysResetDevice(s_DevInfo[s_DoDclCount].DeviceName);

    s_DoDclCount++;
    if(s_DoDclCount >= s_NumDev)
        s_DoDclCount = 0;

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

    s_NumDev = cs_GetDevInfo("ETHERNET", 1, s_DevInfo, MAX_DEVICES);
    if(s_NumDev == 0)
    {
        // Error can't find instrument
        return(0);
    }
    for(i = 0; i < s_NumDev; i++)
    {
        s_DevInfo[i].DriverClass = new CBus_Ethernet_Gigabit_T(
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
            delete((CBus_Ethernet_Gigabit_T *)(s_DevInfo[i].DriverClass));

        s_DevInfo[i].DriverClass = NULL;

    }

    s_NumDev = 0;
	return 0;
}

//++++/////////////////////////////////////////////////////////////////////////
// Wizard Generated Functions
///////////////////////////////////////////////////////////////////////////////

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:0
int doETHERNET_Setup (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CBus_Ethernet_Gigabit_T *)(DriverClass))->SetupBus_Ethernet_Gigabit(Fnc);


#ifdef IEEE_716
    // Any Errors already diagnosed
    Status = ((CBus_Ethernet_Gigabit_T *)(DriverClass))->CloseBus_Ethernet_Gigabit(Fnc);
#endif

	return (int)0;
}

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:1
int doETHERNET_Status (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CBus_Ethernet_Gigabit_T *)(DriverClass))->StatusBus_Ethernet_Gigabit(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:2
int doETHERNET_Fetch (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CBus_Ethernet_Gigabit_T *)(DriverClass))->FetchBus_Ethernet_Gigabit(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:3
int doETHERNET_Init (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CBus_Ethernet_Gigabit_T *)(DriverClass))->InitiateBus_Ethernet_Gigabit(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:4
int doETHERNET_Reset (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
#ifdef IEEE_716
    // Any Errors already diagnosed
    Status = ((CBus_Ethernet_Gigabit_T *)(DriverClass))->OpenBus_Ethernet_Gigabit(Fnc);
#endif
    // Any Errors already diagnosed
    if(Status == 0)
        ((CBus_Ethernet_Gigabit_T *)(DriverClass))->ResetBus_Ethernet_Gigabit(Fnc);



	return (int) 0;
}

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:5
int doETHERNET_Close (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CBus_Ethernet_Gigabit_T *)(DriverClass))->CloseBus_Ethernet_Gigabit(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:6
int doETHERNET_Open (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CBus_Ethernet_Gigabit_T *)(DriverClass))->OpenBus_Ethernet_Gigabit(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:7
int doETHERNET_Connect (void)
//END{DFW}
{
    cs_DoSwitching(M_PATH);
    return (int) 0;
}

//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:8
int doETHERNET_Disconnect (void)
//END{DFW}
{
    cs_DoSwitching(M_PATH);

	return (int) 0;
}


//BEGIN{DFW}:Bus_Ethernet_Gigabit:1:9
int doETHERNET_Load (void)
//END{DFW}
{

	return (int) 0;
}


//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


