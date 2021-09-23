///////////////////////////////////////////////////////////////////////////////
// File    : CemDevinf.cpp
//
// Purpose : Functions for accessing instrument data from BusConfig
//			 Simulation flag set as SIM and debug level is 
//			 as DBGn where n is 0 - 10.
//			 example: 
//
//				DCP BUS 1 MLA 3 MTA 3 SIM 1 DBG 2;
//
//
// Date:	11OCT05
//
// Functions
// Name						Purpose
// =======================  ===================================================
// cs_GetDevInfo()			Initializes the DevINfo from the BusConfi file
// cs_FindDevInfo()         Locates and returns the DevInfo entry requested
//
// Revision History
// Rev	  Date                  Reason							Author
// ===  ========  =======================================  ====================
// 1.0  11OCT05   Initial baseline release.                T.G.McQuillen EADS
//
///////////////////////////////////////////////////////////////////////////////
#include <fstream>
#include "cem.h"
#include "cemsupport.h"

using std::ifstream;
using std::ios;
using std::string;

// Local Static Variables
static char errmsg[MAX_MSG_SIZE];

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: cs_FindDevInfo(char *Device, CS_DEVINFO *DevInfo, int MaxDev)
//
// Purpose: Locates and returns the DevInfo entry requested.
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Device           char *          Device to be located
// DevInfo      	CS_DEVINFO *    Pointer to the DevInfo array
// MaxDev           int             Size of DevInfo array
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    pointer to DevInfo entry.
//    NULL when error occurs.
//
///////////////////////////////////////////////////////////////////////////////
CS_DEVINFO *cs_FindDevInfo(char *Device, CS_DEVINFO *DevInfo, int MaxDev)
{
    int i;
    CS_DEVINFO *Dev = NULL;

    for(i = 0; i < MaxDev; i++)
    {
        if(strcmp(Device,DevInfo[i].DeviceName) == 0)
        {
            Dev = &DevInfo[i];
            break;
        }
    }

    return(Dev);
}



///////////////////////////////////////////////////////////////////////////////
// Function: cs_GetDevInfo(const char *devprefix, CS_DEVINFO *DevInfo, int MaxDev)
//
// Purpose: Initializes the DevINfo from the BusConfi file using the
//          standard cem API functions.
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// devprefix        char *          Prefix device string e.g. "Agxxx" for "Agxxx_1"
// DevInfo      	CS_DEVINFO *    Pointer to the DevInfo array
// MaxDev           int             Size of DevInfo array
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return
//    Number of devices found
//
///////////////////////////////////////////////////////////////////////////////
int cs_GetDevInfo(const char *devprefix, int Fnc, CS_DEVINFO *DevInfo, int MaxDev)
{
    CS_DEVINFO *bcptr;
    char  DeviceName[64];
    char  DeviceChName[64];
    char  DeviceCh1Name[64];
    int   Num;
    int   DeviceIdx;
    char  *DevNoPtr;

    bcptr = DevInfo;
    DeviceIdx = 0;
    Num = 1;

    // create the device name to the _
    strnzcpy(DeviceName,devprefix,60);
    strcat(DeviceName,"_");
    DevNoPtr = &DeviceName[strlen(DeviceName)];
    while(Num <= MaxDev)
    {
        // Check for each possible device
        sprintf(DevNoPtr,"%d",Num);
        sprintf(DeviceChName,"%s:CH%d",DeviceName,Fnc);
        sprintf(DeviceCh1Name,"%s:CH1",DeviceName);
        if(( SetCurDev( DeviceChName ) == OSRSOK ) || ( SetCurDev( DeviceCh1Name ) == OSRSOK ))
        {
            if( IsOkCurDevAddr() )
            {
                // Save Device Name and Bus Number
                strnzcpy(bcptr->DeviceName,DeviceName,MAX_BC_DEV_NAME);
                bcptr->BusNo = GetCurChnNum();
                // Save the Device Number
                bcptr->DeviceNo = Num;
                // Save for Primary Addr (MLA or MTA)
                bcptr->PrimaryAdr = GetCurDevMLA();
                if(!bcptr->PrimaryAdr)
                {
                    bcptr->PrimaryAdr = GetCurDevMTA();
                }
                // Save for Secondary Addr (MSA)
                bcptr->SecondaryAdr = GetCurDevMSA();
                // Save for Debug Flag
                bcptr->Dbg = GetCurDevDbgLvl();
                // Parse for Simulator Flag
                bcptr->Sim = GetCurDevSimLvl();
                // Fill remaining values in the structure
                bcptr->Allocated = 1;
                bcptr->DriverClass = NULL;
                bcptr++;
                DeviceIdx++;
            }
            SetCurDev("");
        }
        Num++;
    }

	return DeviceIdx;
}


//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

int cs_GetCurDeviceFnc(CS_DEVINFO *DevInfo, int MaxDev,
                    void **DriverClass, int *Fnc)
{
    CS_DEVINFO *diptr;
    char *Device;

    Device = GetCurDevNam();
    diptr = cs_FindDevInfo(Device, DevInfo, MaxDev);
    if(diptr == NULL)
    {
        // Error Can't find instrument
        return -1;
    }
    *DriverClass = diptr->DriverClass;
    if(*DriverClass == NULL)
    {
        // Error Driver not initialized
        return -1;
    }

    *Fnc = GetCurChnNum();
    if(*Fnc < 0)
    {
        // Invalid FNC Number
        return -1;
    }

    return 0;
}
