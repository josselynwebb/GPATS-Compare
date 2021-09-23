///////////////////////////////////////////////////////////////////////////////
// File    : SwxSrvr.cpp
//
// Purpose : Defines the entry point for the DLL application.
//
// Revision History
// Rev	    Date                  Reason							Author
// =======  ========  =======================================  ====================
// 1.0.0.0  11OCT05   Initial baseline release.                T.G.McQuillen ARC
// 1.4.1.0  02APR09   Modified code in TETS_UniqFnc to allow large array size
//                    large array size pass thrus
// 1.4.1.1  05May09   Modified code in function CemIssueAtmlSignal  EADS NA
//                    in file TETS_UniqFnc.cpp to increase buffer
//                    size for longer RESOUCE names
//
///////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include "SwxSrvr.h"
#include "SwxSrvrGlbl.h"

#define CEMOUTPUT (WM_USER + 211)

// Global Variable Instances
int g_Dbg=0;
int g_Sim=0;

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
    switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			;
    }
    return TRUE;
}

///////////////////////////////////////////////////////////////////////////////
// Function: g_FmtMsgchar *Fmt,...)
//
// Purpose: Format a print string (i.e. sprintf returning a pointer).
// Note:    This function uses the static char array s_Msg and thus is not
//          thread proof!
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fmt ...          char*           printf format and subsequent arguments
//
//
// Return: pointer to formated string or empty string
//
///////////////////////////////////////////////////////////////////////////////
char s_Msg[256];
char *g_FmtMsg(char *Fmt,...)
{
    va_list Args;

    s_Msg[0] = '\0';
    // prepare to use printf type arguments
    va_start(Args,Fmt);
    // Create Message
    vsprintf(s_Msg,Fmt,Args);

    return(s_Msg);
}

///////////////////////////////////////////////////////////////////////////////
// Function: g_DebugMsg(int MemLevel, int ReqLevel, char *MemDevName, 
//                       char *Fmt,...)
//
// Purpose: Displays a CEMDEBUG message according to the current Debug setting.
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// MemLevel         int             The current member debug level
// MemDeviceName    char*           The current member variable device name
// ReqLevel         int             The requested debug level for the message
// Fmt ...          char*           printf format and subsequent arguments
//
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void g_DebugMsg(int MemLevel, int ReqLevel, char *MemDevName, char *UsrMsg)
{
    char Msg[MAX_MSG_SIZE];
    LPCSTR p;
    HWND hwndRTS;

    // Determine if to display or not
    if(MemLevel < ReqLevel)
        return;
    // Create Message
    sprintf(Msg,"\033[32;40mCEMDEBUG: %s - %s\033[m\n",MemDevName,UsrMsg);
    // Display it
	p = Msg;
	hwndRTS = ::FindWindow("WRTS", NULL);
	if (hwndRTS)
		::SendMessage(hwndRTS, CEMOUTPUT, 0, (LPARAM) p);

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: g_ErrorMsg(char *MemDevName, 
//                       char *Fmt,...)
//
// Purpose: Displays a CEMERROR message..
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// MemDeviceName    char*           The current member variable device name
// Fmt ...          char*           printf format and subsequent arguments
//
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void g_ErrorMsg(char *MemDevName, char *UsrMsg)
{
    char Msg[MAX_MSG_SIZE];
    LPCSTR p;
    HWND hwndRTS;

    // Create Message
    sprintf(Msg,"\033[31;40mCEMERROR: %s - %s\033[m\n",MemDevName,UsrMsg);
    // Display it
	p = Msg;
	hwndRTS = ::FindWindow("WRTS", NULL);
	if (hwndRTS)
		::SendMessage(hwndRTS, CEMOUTPUT, 0, (LPARAM) p);

    return;
}



