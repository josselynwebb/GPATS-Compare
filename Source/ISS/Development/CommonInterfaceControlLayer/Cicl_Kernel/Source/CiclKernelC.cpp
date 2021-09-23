///////////////////////////////////////////////////////////////////////////////
// File    : CiclKernal.cpp
//
// Purpose : Kernal executable of the CICL Core;
//
//
// Date:	11OCT05
//
// Functions
// Name						Purpose
// =======================  ===================================================
//
// Revision History
// Rev	  Date                  Reason							Author
// ===  ========  =======================================  ====================
// 1.0  11OCT05   Initial baseline release.                T.G.McQuillen EADS
//
///////////////////////////////////////////////////////////////////////////////
#include "windows.h"
#include <fstream>
#include "time.h"
#include "CiCoreCommon.h"

// Api Interface prototypes AtXmlInterface.cpp
extern int atxml_IntfInitialize(void);
extern int atxml_IntfClose(void);

// Global Data Instanced In This Module
PROC_INFO g_ProcInfo[MAX_PROCS];

DWORD  g_ApiThread = 0;
// FIX Possibly use InterfaceMutex
int    g_ApiThreadStatus = API_STATUS_STOPPED;
HANDLE g_InstDataMutex = NULL;
bool   g_CiCoreDbg = true;

// Local Defines

// Local Static Variables

// Local Function Prototypes
static int             s_LaunchInterfaceThread(void);
static DWORD __stdcall s_ApiThread(void* Arg);

int main(int argc, char* argv[])
{
    int Status = 0;
	bool done = false;
	int c;
    
    // Init Debug Log in overwrite mode
	// FIX later maybe make optional via arg
    atxml_LogInit(ATXML_LOG_DEBUG, "CiclDbgLog.txt", false);
    atxml_Log(ATXML_LOG_DEBUG,"CICL Start-up");

    // Open Event Log in append mode
    atxml_LogInit(ATXML_LOG_EVENTS, "CiclEventLog.txt", true);
    atxml_Log(ATXML_LOG_EVENTS,"CICL Start-up");

    // Init Mutexes
    g_InstDataMutex = CreateMutex(NULL, false, NULL);

    // Init Smam
    CICOREDBGLOG("Call smam_MainInitialize()");
    Status = smam_MainInitialize();

	////////// Launch API Thread //////////
    CICOREDBGLOG("Call atxml_IntfInitialize()");
	Status = s_LaunchInterfaceThread();
    CICOREDBGLOG(atxml_FmtMsg("atxml_IntfInitialise() = %d",Status));

    // Spin waiting for 'x\n' to be typed
    while (!done) {
        // check to see if we're done
        c = getchar();
        switch(c)
        {
        case 'x':
        case 'X':
            done = true;
            break;
        case 'R':
            smam_InvokeRemoveAllSequence(0,NULL,0);
            break;
		case 'd':
		case 'D': // Toggle Debug Trace
			g_CiCoreDbg = g_CiCoreDbg ? false : true;
			if(g_CiCoreDbg)
				atxml_Log(ATXML_LOG_DEBUG,"Debug Trace Enabled");
			else
				atxml_Log(ATXML_LOG_DEBUG,"Debug Trace Disabled");
			break;

        }
        Sleep(300);
    }
    g_ApiThreadStatus = API_STATUS_HALT;
    Sleep(1);

    smam_MainClose();
    // Close Mutexes
    if(g_InstDataMutex)
        CloseHandle(g_InstDataMutex);
	return(0);
}
//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: kern_GetMutex
//           kern_ReleaseMutex
//
// Purpose: Initialize the SMAM
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    true on success.
//   false on failure
//
///////////////////////////////////////////////////////////////////////////////
bool kern_GetMutex(HANDLE Mutex, int Timeout)
{
    if(Mutex == NULL)
        return(true);

    if((Timeout < 0) &&
        (WaitForSingleObject(Mutex, INFINITE) == WAIT_OBJECT_0))
    {
        return (true);
    }
    else if((WaitForSingleObject(Mutex, Timeout) == WAIT_OBJECT_0))
    {
        return (true);
    }
    return(false);
}

void kern_ReleaseMutex(HANDLE Mutex)
{
    ReleaseMutex(Mutex);
    return;
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Functions
///////////////////////////////////////////////////////////////////////////////

int s_LaunchInterfaceThread(void)
{
    int ApiStatus;
	time_t When;

    // Launch API Interface thread
    if(g_ApiThreadStatus == API_STATUS_STOPPED)
    {
        g_ApiThreadStatus = API_STATUS_STARTING;
        if(CreateThread(NULL, 0, s_ApiThread, NULL, 0, &g_ApiThread) == 0)
        {
            //FIX Later Diagnose
            return(-1);
        }
        When = time(NULL) + 1;
        ApiStatus = API_STATUS_STARTING;
        while(ApiStatus == API_STATUS_STARTING)
        {
            Sleep(100);
            if(time(NULL) > When)
                break;
            ApiStatus = g_ApiThreadStatus;
        }

	}
	return(0);
}


DWORD __stdcall s_ApiThread(void* Arg)
{
	g_ApiThreadStatus = API_STATUS_RUNNING;

	atxml_IntfInitialize();

    g_ApiThreadStatus = API_STATUS_STOPPED;
	return(0);
}
