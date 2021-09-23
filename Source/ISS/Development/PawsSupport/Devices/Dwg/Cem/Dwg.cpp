//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Dwg.cpp
//
// Date:	    11-OCT-05
//
// Purpose:	    Instrument Driver for Dwg
//
// Instrument:	Dwg  <device description> (DWG)
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
// Revision History described in Dwg_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include <limits.h>
#include <float.h>
#include "cemsupport.h"
#include "Dwg_T.h"

// Local Defines
//#define IEEE_716 /*using 716.89 compiler with CLOSE/OPEN missing*/

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
#ifdef SIMCICL
    if(s_DevInfo[s_DoDclCount].DriverClass != NULL)
            ((CDwg_T *)(s_DevInfo[s_DoDclCount].DriverClass))->ResetDwg(0);
#endif
#ifndef SIMCICL  // 01/08/07
	// Signal system reset in process
 if(s_DevInfo[s_DoDclCount].DriverClass != NULL)
    ((CDwg_T *)(s_DevInfo[s_DoDclCount].DriverClass))->ResetDwg(101); // 03/13/07-

	cs_SysResetDevice(s_DevInfo[s_DoDclCount].DeviceName); 

#endif
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
    {  // 10/12/06 PUT A RESET DWG HERE
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

    s_NumDev = cs_GetDevInfo("DWG", 1, s_DevInfo, MAX_DEVICES);
    if(s_NumDev == 0)
    {
        // Error can't find instrument
        return(0);
    }
    for(i = 0; i < s_NumDev; i++)
    {
        s_DevInfo[i].DriverClass = new CDwg_T(
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
            delete((CDwg_T *)(s_DevInfo[i].DriverClass));
        s_DevInfo[i].DriverClass = NULL;
    }
    s_NumDev = 0;
	return 0;
}

//++++/////////////////////////////////////////////////////////////////////////
// Wizard Generated Functions
///////////////////////////////////////////////////////////////////////////////

//BEGIN{DFW}:Dwg:1:0
int doDWG_Setup (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;
	//int m_Dbg = 10;  //03/13/07-
	// char m_DeviceName[MAX_BC_DEV_NAME]= "dwg_1";   //03/13/07-

	// CEMDEBUG(10,cs_FmtMsg("Doing a Setup"));   //03/13/07-


    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);

	// Any Errors already diagnosed

    if (GetCurVerb()!= V_CHN)
    {
      if(Status == 0)
          ((CDwg_T *)(DriverClass))->SetupDwg(Fnc);
      // CEMDEBUG(10,cs_FmtMsg("retrieved a function number %d ",Fnc));    //03/13/07-

      #ifdef IEEE_716
          // Any Errors already diagnosed
          Status = ((CDwg_T *)(DriverClass))->CloseDwg(Fnc);
      #endif
    }
    if (GetCurVerb()== V_CHN)
    {
      Fnc += 900;
      if(Status == 0)
          ((CDwg_T *)(DriverClass))->SetupDwg(Fnc);
      if(Status == 0)
          ((CDwg_T *)(DriverClass))->StatusDwg(Fnc);
      if(Status == 0)
          ((CDwg_T *)(DriverClass))->CloseDwg(Fnc);
    }

	return (int)0;
}

//BEGIN{DFW}:Dwg:1:1
int doDWG_Status (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CDwg_T *)(DriverClass))->StatusDwg(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Dwg:1:2
int doDWG_Fetch (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CDwg_T *)(DriverClass))->FetchDwg(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Dwg:1:3
int doDWG_Init (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CDwg_T *)(DriverClass))->InitiateDwg(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Dwg:1:4
int doDWG_Reset (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;
//	int m_Dbg = 10;
//	char m_DeviceName[MAX_BC_DEV_NAME]= "dwg_1";

//	CEMDEBUG(10,cs_FmtMsg("Doing a reset"));

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
	
//	CEMDEBUG(10,cs_FmtMsg("retrieved a function number %d ",Fnc));

#ifdef IEEE_716
    // Any Errors already diagnosed
    Status = ((CDwg_T *)(DriverClass))->OpenDwg(Fnc);
#endif
    // Any Errors already diagnosed
    if(Status == 0)
//        Status = ((CDwg_T *)(DriverClass))->ResetDwg(Fnc);
        ((CDwg_T *)(DriverClass))->ResetDwg(Fnc);

//	CEMDEBUG(10,cs_FmtMsg("finished a reset function %d status %d ",Fnc, Status));

	return (int) 0;
}

//BEGIN{DFW}:Dwg:1:5
int doDWG_Close (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CDwg_T *)(DriverClass))->CloseDwg(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Dwg:1:6
int doDWG_Open (void)
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CDwg_T *)(DriverClass))->OpenDwg(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Dwg:1:7
int doDWG_Connect (void)
//END{DFW}
{	
    //cs_DoSwitching(M_PATH);

	void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES,
                       &DriverClass, &Fnc);
    // Any Errors already diagnosed
    if(Status == 0)
        ((CDwg_T *)(DriverClass))->getcnx(Fnc);

	return (int) 0;
}

//BEGIN{DFW}:Dwg:1:8
int doDWG_Disconnect (void)
//END{DFW}
{	
    //cs_DoSwitching(M_PATH);

	return (int) 0;
}


//BEGIN{DFW}:Dwg:1:9
int doDWG_Load (void)
//END{DFW}
{

	return (int) 0;
}


//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


