///////////////////////////////////////////////////////////////////////////////
// File    : TETS_UniqFnc.cpp
//
// Purpose : Perform station unique status actions.
//
//
// Date    : 05/12/04
//
// Functions
// Name						Purpose
// =======================  ===================================================
// CheckUniqStationStatus() This function checks the station ready status
//                          E.g. ICA interlock closed.
// GetUniqCalAvail()			This function Looks up the last cal date for the
//                          device specified.
//
// Revision History
//           Revisions tracked in SwxSrvr.cpp file
//
///////////////////////////////////////////////////////////////////////////////
#include <windows.h>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include "swxsrvr.h"
#include "SwxSrvrGlbl.h"
#include "cem.h" // for Special Reset Code
#include "AtXmlInterfaceApiC.h"

//#define MONITOR_LOOP
//#define TGMDEBUG

#define MON_STATUS_STOPPED   0
#define MON_STATUS_STARTING  1
#define MON_STATUS_RUNNING   2
#define MON_STATUS_STOP      3

// Local Static Variables
static int   s_StaInit = 0;
//static int   s_MonThreadStatus = MON_STATUS_STOPPED;
//static DWORD s_MonThread = 0;
//static HANDLE s_IcaMonitorMutex = NULL;

//static bool  s_UutPwrEnabled = false;
static char  s_ErrMsgBuf[4096];
static char  s_SysResetDeviceName[128];
static char  s_SysResetDeviceSimFlag = 1;// look for last non-simulated

typedef vector<ATXML_AVAILABILITY> INSTR_STATUS_VECTOR;

static int                 s_InstrStatusCount = 0;
static INSTR_STATUS_VECTOR s_InstrStatus;


#define VERB_COUNT 11

// Local Function Prototypes
static DWORD __stdcall s_MonitorThread(void* arg);
static int  s_InitICAMonitor(char **ErrMsg);
static int  s_SetUutEnable(char **ErrMsg);
static int  s_SetUutDisable(char **ErrMsg);
static int  s_GetUutEnableState(char **ErrMsg);
// ATXML Interface modules
static int  s_CheckAvailability(char *DevName, char *AvailValue, char *Response, int BufSize);
static int  s_DeviceAvailabilityQuery(char *DevName, char *AvailValue, char *Response, int BufSize);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: CheckUniqStationStatus()
// Purpose : This function checks the station ready status
//           E.g. ICA interlock closed.
// Return:
//         0 - AOK
//        -1 - Not Ready
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int CheckUniqStationStatus(char **ErrMsg)
{
    int RetVal = 0;

    //FIX Foe now assume enabled
    if(RetVal == 0)return(0);

    *ErrMsg = "CheckUniqStationStatus - UnKnown error";
    // Check if simulated / ignored
    if(s_StaInit < 0)
        return(RetVal);
/*
    int UutEnabled;
    // Place System Specific Status Stuff Here!
#ifdef MONITOR_LOOP
    RetVal = 1;
#else
    // Check if ICA interlock engaged and UUT power enabled
    RetVal = s_GetUutEnableState(ErrMsg);
#endif
    if(RetVal < 0)
        return(RetVal);

    // Check for manual green button push!!!
    if(s_IcaMonitorMutex)
        WaitForSingleObject(s_IcaMonitorMutex, 1000);
    UutEnabled = s_UutPwrEnabled;
    if(s_IcaMonitorMutex)
        ReleaseMutex(s_IcaMonitorMutex);
    if((RetVal == 1) && !UutEnabled)
    {
        // Power not really enabled
        RetVal = 0;
    }
    if(RetVal == 0)
    {
        *ErrMsg = "CheckUniqStationStatus - UUT Power Disabled!";
        // Check if Handle Pulled Prior to Remove All
        if(UutEnabled)
            return(SU_STA_INTERLOCK);

        // Attempt to re-enable the UUT power
        RetVal = s_SetUutEnable(ErrMsg);
        if(RetVal < 0)
            return(RetVal);


        // Re-check UUT enabled
        RetVal = s_GetUutEnableState(ErrMsg);
        if(RetVal < 0)
            return(RetVal);
        if(RetVal == 0)
        {
           // Nope - Interlock Open
            return(SU_STA_INTERLOCK);
        }
        if(s_IcaMonitorMutex)
            WaitForSingleObject(s_IcaMonitorMutex, 1000);
        s_UutPwrEnabled = true;
        if(s_IcaMonitorMutex)
            ReleaseMutex(s_IcaMonitorMutex);

    }
/**/
    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: ClearUniqStationStatus()
// Purpose : This function is called to clear the station ready status
//           E.g. End of REMOVE, ALL.
// Return:
//         0 - AOK
//        -1 - Not Ready
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int ClearUniqStationStatus(char **ErrMsg)
{
    int RetVal = 0;


    *ErrMsg = "ClearUniqStationStatus - UnKnown error";

    // Check if simulated / ignored / not initialized
    if(s_StaInit < 0)
        return(0);

    if(s_StaInit == 0)
        s_InitICAMonitor(ErrMsg);

    if(s_StaInit <=0)
        return(0);

/*
    // Reset Enable UUT Power Flag
    if(s_IcaMonitorMutex)
        WaitForSingleObject(s_IcaMonitorMutex, 1000);
    s_UutPwrEnabled = false;
    if(s_IcaMonitorMutex)
        ReleaseMutex(s_IcaMonitorMutex);

    // Check if ICA interlock engaged and UUT power enabled
    RetVal = s_GetUutEnableState(ErrMsg);
    if(RetVal < 0)
        return(RetVal);

    if(RetVal)
    {
        // Attempt to disable the UUT power
        RetVal = s_SetUutDisable(ErrMsg);
        if(RetVal < 0)
            return(RetVal);


        // Re-check UUT enabled
        RetVal = s_GetUutEnableState(ErrMsg);
        if(RetVal < 0)
            return(RetVal);
        if(RetVal != 0)
        {
           // UUT Power still enabled is an error
            return(-1);
        }

    }
*/
    *ErrMsg = "";
    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: CloseUniqStationStatus()
// Purpose : This function closes any assets used by CheckUniqStationStatus.
// Return:
//         0 - AOK
//        -1 - Not Closed
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int CloseUniqStationStatus(void)
{
    int RetVal = 0;
    char *ErrMsg;

    ErrMsg = "CloseUniqStationStatus - UnKnown error";

    // Check if simulated / ignored / not initialized
    if(s_StaInit <= 0)
        return(RetVal);

    atxml_Close();
/*
    time_t When;
    // Check if ICA interlock engaged and UUT power enabled
    RetVal = s_GetUutEnableState(&ErrMsg);

    if(RetVal != 0)
    {
        // Attempt to disable the UUT power
        RetVal = s_SetUutDisable(&ErrMsg);
    }

    // Reset Enable UUT Power Flag
    if(s_IcaMonitorMutex)
        WaitForSingleObject(s_IcaMonitorMutex, 1000);
    s_UutPwrEnabled = false;
    if(s_IcaMonitorMutex)
        ReleaseMutex(s_IcaMonitorMutex);


    ErrMsg = "";

    // Shut down thread
    s_MonThreadStatus = MON_STATUS_STOP;
#ifndef MONITOR_LOOP
    s_MonThreadStatus = MON_STATUS_STOPPED;
#endif
    // Wait for thread to stop
    When = time(NULL) + 2;
    while(s_MonThreadStatus == MON_STATUS_STOP)
    {
        Sleep(1);
        if(time(NULL) > When)
            break;
    }
    if(s_MonThreadStatus != MON_STATUS_STOPPED)
    {
        // Shouldn't happen, but something is wrong
        TerminateThread((HANDLE)s_MonThread, 0);
        s_MonThreadStatus = MON_STATUS_STOPPED;
    }
    if(s_IcaMonitorMutex)
        CloseHandle(s_IcaMonitorMutex);
    s_IcaMonitorMutex = NULL;
*/
    return(RetVal);
}

////////////////////////////////////////////////////////////////////////////////
// Function: GetUniqCalAvail()
// Purpose : This function Looks up the last cal date for the device specified.
//           It may also look up the expected version of the .Dll and verify that
//           the correct version is present.
// Return:
//         0 - AOK
//        -1 - Error with ErrMsg passed back to caller
////////////////////////////////////////////////////////////////////////////////
static char s_CalCfgMsg[256];
static int  s_CalCfgInit = 0;
extern "C" SWXSRVR_API int GetUniqCalAvail(char *DevName, time_t ExpTs,
                                         double *CalData, int Count,
                                         char *Response, int BufferSize, int Sim)
{
    int Status = 0;
    char AvailabilityValue[ATXML_MAX_AVAIL_VALUE];
    int  len;
    char *cptr;


    // Ignore if couldn't find cal file
    if(s_CalCfgInit < 0)
        return(0);

    if(s_CalCfgInit == 0)
    {
		// Init sysreset device name
		s_SysResetDeviceName[0] = '\0';

		// Open connection via AtXmlApi
        IFNSIM(Sim,(Status = atxml_Initialize(ATXML_PROC_TYPE_PAWS_WRTS,
                                     SWXSRVR_UUID)));
        if(Status)
        {
            if(Response && (BufferSize > 400))
                sprintf(Response,"%s%s%s",
                "<AtXmlResponse> "
                    "<ErrStatus moduleName=\"SwxSrvr\" leadText=\"",
                    DevName,
                    "\" errCode=\"-1\" errText=\"API Connection Failed\"></ErrStatus>"
                "</AtXmlResponse>");
            s_CalCfgInit = -1;
            return(-1);
        }
    }
    s_CalCfgInit = 1;

	
    IFNSIM(Sim,(Status = s_CheckAvailability(DevName, AvailabilityValue,
                            Response, BufferSize)));
    if(Status != 0)
    {
        return(Status);
    }
    if(!Sim)
    {
        len = strlen(Response);
        cptr = &Response[len];
        if((strcmp(AvailabilityValue,"Available")) &&
           (strcmp(AvailabilityValue,"Simulated")) &&
           (strcmp(AvailabilityValue,"InUse"))
           )
        {
            if(Response)
            {
				if( strcmp( DevName, "PCISERIAL_1") != 0 )	//CJW ignore PCISERIAL_1
				{
					if(BufferSize > (len+400))
						sprintf(Response,"%s%s%s%s%s",
						"<AtXmlResponse> "
							"<ErrStatus moduleName=\"SwxSrvr\" leadText=\"",
							DevName,
							"\" errCode=\"-2\" errText=\"Validation Error - ",
							AvailabilityValue,
							"\"></ErrStatus>"
						"</AtXmlResponse>");
				}
            }
        }
    }

    //FIX - Analyse AvailabilityValue
	// save name of first device that is not simulated for SysReset invokation
	if(!Sim || (Sim && s_SysResetDeviceSimFlag))
	{
		strnzcpy(s_SysResetDeviceName,DevName,124);
		s_SysResetDeviceSimFlag = Sim;
	}
    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: SetUniqCalCfg()
// Purpose : This function Sets / Saves the calibration information as required.
// Return:
//         0 - AOK
//        -1 - Error with ErrMsg passed back to caller
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int SetUniqCalCfg(char *DevName, time_t ExpTs,
                                         double *CalData, int Count,
                                         char *Response, int BufferSize, int Sim)
{
    int   Status = 0;
 
    // Ignore if couldn't find cal file
    if(s_CalCfgInit < 0)
        return(0);

    if(s_CalCfgInit == 0)
    {
        s_CalCfgInit = 1;
    }
    //FIX for Cal stuff
    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: SysResetDevice()
// Purpose : This function performs a "REMOVE ALL" when the DevName was the first
//           Non-Simulated allocated device.
// Return:
//         0 - AOK
//        -1 - Error with ErrMsg passed back to caller
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int SysResetDevice(char *DevName,
						                 char *Response, int BufferSize)
{
	int  Status = 0;
    int  RemAllStatus = 0;
	char *ErrMsg;


	if(DevName && (strncmp(DevName,s_SysResetDeviceName,128)==0))
	{

		CEMDEBUG(g_Dbg, 5, "SWXSRVR", "TETS Switch Server Reset");

		IFNSIM(s_SysResetDeviceSimFlag,(Status = atxml_InvokeRemoveAllSequence(Response, BufferSize)));

		//if(Status || (RemAllStatus == ATXML_REMALL_FAILED))
			;//FIX diagnose???

		// Reset the UUT Power Enable Flag
		ClearUniqStationStatus(&ErrMsg);
	}
	return(0);
}
////////////////////////////////////////////////////////////////////////////////
// Function: CemIssueSignal()
// Purpose : This function issues a signal description.
// Return:
//         0 - AOK
//        -1 - Error with ErrMsg passed back to caller
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int  CemIssueAtmlSignal(char *Verb, char *DevName, double MaxTime, char *SignalDescription,
                                         char *Response, int BufferSize)
{
    int   Status = 0;
    //char  XmlBuf[4096];
	char *XmlBuf;
	int len = 0;

	len = strlen(SignalDescription) + 500;

	XmlBuf = new char[len];
 
    sprintf(XmlBuf,
		"<AtXmlSignalDescription xmlns:atXml=\"ATXML_TSF\">\n"
		"  <SignalAction>%s</SignalAction>\n"
        "  <SignalResourceName>%s</SignalResourceName>\n"
		"  <SignalSnippit>\n"
        "        %s\n"
        "  </SignalSnippit>\n",
        Verb,
        DevName,
        SignalDescription
        );
    if(MaxTime < 0.0)
        strcat(&XmlBuf[strlen(XmlBuf)],
		    "</AtXmlSignalDescription>\n");
    else
        sprintf(&XmlBuf[strlen(XmlBuf)],
            "  <SignalTimeOut>%lf</SignalTimeOut>\n"
		    "</AtXmlSignalDescription>\n",
            MaxTime);

    Status = atxml_IssueSignal(XmlBuf, Response, BufferSize);

	delete [] XmlBuf;

    return(Status);
}


////////////////////////////////////////////////////////////////////////////////
// Function: IssueIst()
// Purpose : This function issues .
// Return:
//         0 - AOK
//        -1 - Error with ErrMsg passed back to caller
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int IssueIst(char *DevName, int Level, char *Response, int BufferSize)
{
    int   Status = 0;
    char  XmlBuf[4096];
 
    sprintf(XmlBuf,
		"<AtXmlIssueIst xmlns:atXml=\"ATXML_TSF\">\n"
        "  <SignalResourceName>%s</SignalResourceName>\n"
        "  <Level>%d</Level>\n"
        "</AtXmlSignalDescription>\n",
        DevName, Level
        );

    Status = atxml_IssueIst(XmlBuf, Response, BufferSize);
    return(Status);
}
//+++++////////////////////////////////////////////////////////////////////////
// Response processing pass through functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: ParseErrDbgResponse,
//           ErrDbgNextError,
//           ErrDbgNextDebug,
//
// Purpose: Scan the Response XML for errors and debuf statements.
//          Copy back to Response non-error/non-debug elements
//
// Input Parameters
// Parameter		 Type			 Purpose
// ===============   ==============  ===========================================
// ref API interface definition
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ref API interface definition
//
// Return: ref API interface definition
//
///////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int ParseErrDbgResponse(ATXML_XML_String* Response, int BufferSize,
                                                              bool Strip, ATXML_XML_Handle *ErrDbgHandle)
{
    return(atxml_ParseErrDbgResponse(Response, BufferSize, Strip, ErrDbgHandle));
}
extern "C" SWXSRVR_API bool ErrDbgNextError(ATXML_XML_Handle ErrDbgHandle, char **ModuleName, char **LeadText,
                                                              int *ErrCode, char **Message)
{
    return((bool)atxml_ErrDbgNextError(ErrDbgHandle, ModuleName, LeadText,
                                      ErrCode, Message));
}
extern "C" SWXSRVR_API bool ErrDbgNextDebug(ATXML_XML_Handle ErrDbgHandle, char **ModuleName, 
                                                              int *DbgLevel, char **Message)
{
    return((bool)atxml_ErrDbgNextDebug(ErrDbgHandle, ModuleName,
                                      DbgLevel, Message));
}
extern "C" SWXSRVR_API void ErrDbgClose(ATXML_XML_Handle ErrDbgHandle)
{
    atxml_ErrDbgClose(ErrDbgHandle);
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: ReturnData parsing pass throughs
//
// Purpose: Scan the Response XML for the specified "attribute" and return the data
//
// Input Parameters
// Parameter		 Type			 Purpose
// ===============   ==============  ===========================================
// ref API interface definition
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ref API interface definition
//
// Return: ref API interface definition
//
///////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int GetSingleIntValue(char *XmlValueResponse, char *Attribute, int *Value)
{
    return(atxml_GetSingleIntValue(XmlValueResponse,Attribute,Value));
}
extern "C" SWXSRVR_API int GetIntArrayValue(char *XmlValueResponse, char *Attribute, 
                             int *Value, int *ArraySize)
{
    return(atxml_GetIntArrayValue(XmlValueResponse,Attribute,Value,ArraySize));
}

extern "C" SWXSRVR_API int GetSingleDblValue(char *XmlValueResponse, char *Attribute, double *Value)
{
    return(atxml_GetSingleDblValue(XmlValueResponse,Attribute,Value));
}

extern "C" SWXSRVR_API int GetDblArrayValue(char *XmlValueResponse, char *Attribute,
                             double *Value, int *ArraySize)
{
    return(atxml_GetDblArrayValue(XmlValueResponse,Attribute,Value,ArraySize));
}

extern "C" SWXSRVR_API int GetStringValue(char *XmlValueResponse, char *Attribute,
                             char *Value, int BufferSize)
{
    return(atxml_GetStringValue(XmlValueResponse,Attribute,Value,BufferSize));
}

//+++++////////////////////////////////////////////////////////////////////////
// Local Fnctions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function:	s_MonitorThread(void* Arg)
//
// Purpose:		Thread routine for monitoring the station status:
//                Temperature
//                Power stability
//                ICA Interlock
//
///////////////////////////////////////////////////////////////////////////////
/*
DWORD __stdcall s_MonitorThread(void* Arg)
{
    int MonitorTimer = 0;
    int status = 0;
    int Interlock = 1;
	int Green = 1, Red = 0;
    int UutEnabled;

#ifdef TGMDEBUG
FILE* sfid;

sfid = fopen("tgm.log","w");
fprintf(sfid," Starting Thread\n");
fclose(sfid);
#endif
//    status = s_CartDisable();
#ifdef TGMDEBUG
sfid = fopen("tgm.log","a");
fprintf(sfid," s_CartDisable %d\n",status);
fclose(sfid);
#endif
    if(s_IcaMonitorMutex)
        WaitForSingleObject(s_IcaMonitorMutex, 1000);
    s_MonThreadStatus = MON_STATUS_RUNNING;
    if(s_IcaMonitorMutex)
        ReleaseMutex(s_IcaMonitorMutex);
    while(s_MonThreadStatus == MON_STATUS_RUNNING)
    {
        if(++MonitorTimer > 50) // check at 5 sec (30*100ms) intervals
        {
            MonitorTimer = 0;
            if(s_IcaMonitorMutex)
                WaitForSingleObject(s_IcaMonitorMutex, 1000);
            //IFNSIM(g_Sim,(status = GetICAInterlockState( &Interlock)));
//            IFNSIM(g_Sim,(status = GetUUTEnableState( &Green, &Red )));
            if(s_IcaMonitorMutex)
                ReleaseMutex(s_IcaMonitorMutex);
            if(status != 0)
            {
                Interlock = 1;
				Green = 1;
            }
#ifdef TGMDEBUG
sfid = fopen("tgm.log","a");
fprintf(sfid," Tick %d, status %d, Interlock %d, Green %d\n",time(NULL),status,Interlock,Green);
fclose(sfid);
#endif
            if(s_IcaMonitorMutex)
                WaitForSingleObject(s_IcaMonitorMutex, 1000);
            UutEnabled = s_UutPwrEnabled;
            if(s_IcaMonitorMutex)
                ReleaseMutex(s_IcaMonitorMutex);
            if(UutEnabled && !Green)
            {

#ifdef TGMDEBUG
sfid = fopen("tgm.log","a");
fprintf(sfid," Reset WRTS %d\n",time(NULL));
fclose(sfid);
#endif
		        // 1) Signal the WRTS
		        HWND hwndRts = ::FindWindow("WRTS", NULL);
		        // check if RTS is running
		        if (hwndRts)
		        {
			        // close the RTS
			        //::PostMessage(hwndRts, WM_CLOSE, 0, 0);
			        // Halt + reset the RTS
			        ::PostMessage(hwndRts, WM_COMMAND, 40303, 0); // HALT
			        ::PostMessage(hwndRts, WM_COMMAND, 40402, 0); // RESET
			        //::PostMessage(hwndRts, WM_COMMAND, 57664, 0);
                    ;
		        }
            }
        }
        Sleep(100);
    }
    if(s_IcaMonitorMutex)
        WaitForSingleObject(s_IcaMonitorMutex, 1000);
    s_MonThreadStatus = MON_STATUS_STOPPED;
    if(s_IcaMonitorMutex)
        ReleaseMutex(s_IcaMonitorMutex);
#ifdef TGMDEBUG
sfid = fopen("tgm.log","a");
fprintf(sfid,"Thread Stopping\n\n");
fclose(sfid);
#endif
    ExitThread(0);
    return(0);
}
/**/
///////////////////////////////////////////////////////////////////////////////
// Function:	s_InitICAMonitor
//
// Purpose:		Init ICA interlock and UUT power indicators
//
///////////////////////////////////////////////////////////////////////////////
static int s_InitICAMonitor(char **ErrMsg)
{
    int status = 0;
//    int MonStatus;
#ifdef MONITOR_LOOP
	time_t When;
#endif

	if(s_StaInit < 0)
        return(0);

	//if(g_Sim) s_StaInit = -1;
    // Check if initialized
    if(s_StaInit == 0)
    {
        s_StaInit = 1;

		CEMDEBUG(g_Dbg, 5, "SWXSRVR", "s_InitICAMonitor");
/*        
        // Launch monitor thread
        if(s_MonThreadStatus == MON_STATUS_STOPPED)
        {
#ifdef MONITOR_LOOP
            s_IcaMonitorMutex = CreateMutex(NULL, FALSE, NULL);
            s_MonThreadStatus = MON_STATUS_STARTING;
            if(CreateThread(NULL, 0, s_MonitorThread, NULL, 0, &s_MonThread) == 0)
            {
                *ErrMsg = "Station Monitor thread could not be launched";
                s_StaInit = -1;
                return(-1);
            }
            When = time(NULL) + 1;
            MonStatus = MON_STATUS_STARTING;
            while(MonStatus == MON_STATUS_STARTING)
            {
                Sleep(1);
                if(time(NULL) > When)
                    break;
                if(s_IcaMonitorMutex)
                    WaitForSingleObject(s_IcaMonitorMutex, 1000);
                MonStatus = s_MonThreadStatus;
                if(s_IcaMonitorMutex)
                    ReleaseMutex(s_IcaMonitorMutex);
            }
#else
    s_MonThreadStatus = MON_STATUS_RUNNING;
#endif
        }
        
        s_MonThreadStatus = MON_STATUS_RUNNING;
*/
    }
    return(0);
}

/*
static char UUTmsg[1024];
///////////////////////////////////////////////////////////////////////////////
// Function:	s_GetUutEnableState
//
// Purpose:		Retrieve the current UUT power enable light state
//
///////////////////////////////////////////////////////////////////////////////
bool s_SimUutEnabled = false;
static int s_GetUutEnableState(char **ErrMsg)
{
    int   status = 0, Red = 0, Green = 0;
    int out_relay;
    int cnt;
	char msg[1024];

    CEMDEBUG(g_Dbg, 5, "SWXSRVR", "s_GetUutEnableState");
    msg[0]='\0';

    // Verify that the Enable Light is on
    if(s_IcaMonitorMutex)
        status = WaitForSingleObject(s_IcaMonitorMutex, 1000);
//    IFNSIM(g_Sim,(status = GetUUTEnableState( &Green, &Red )));
    if(s_IcaMonitorMutex)
        ReleaseMutex(s_IcaMonitorMutex);
	cnt = 5;
    // 258 is intermittant communication error
	while((status == 258) && (cnt-- > 0))
	{
		Sleep(100);
		if(status) 
		{
			sprintf(msg,"s_GetUutEnableState Status = %d",status);
			CEMDEBUG(g_Dbg, 5, "SWXSRVR", msg);
		}
        if(s_IcaMonitorMutex)
            status = WaitForSingleObject(s_IcaMonitorMutex, 1000);
//		IFNSIM(g_Sim,(status = GetUUTEnableState( &Green, &Red )));
        if(s_IcaMonitorMutex)
            ReleaseMutex(s_IcaMonitorMutex);
	}
    if(status && (status != 258))
    {
 //       IFNSIM(g_Sim,(ErrorMessage(status, msg)));
	    sprintf(UUTmsg,"s_GetUutEnableState Status = %d, [%s]",status,msg);
        CEMDEBUG(g_Dbg, 5, "SWXSRVR", UUTmsg);
        *ErrMsg = UUTmsg;
        return(SU_STA_INIT);
    }
    out_relay = Green ? 1 : 0;
    if(g_Sim) out_relay = s_SimUutEnabled;
    return(out_relay);
}


///////////////////////////////////////////////////////////////////////////////
// Function:	s_SetUutEnable
//
// Purpose:		Set the UUT power enable light state (green)
//
///////////////////////////////////////////////////////////////////////////////
static int s_SetUutEnable(char **ErrMsg)
{
    int status = 0;
	char msg[1024];
    int cnt;

    CEMDEBUG(g_Dbg, 5, "SWXSRVR", "s_SetUutEnable");

    msg[0]='\0';

    // Set green light
//    IFNSIM(g_Sim,(status = SetUUTEnableFrontPanel(false)));
	cnt = 5;
    // 258 is intermittant communication error
	while((status == 258) && (cnt-- > 0))
	{
		Sleep(100);
		if(status) 
		{
			sprintf(msg,"s_SetUutEnable SetUUTEnableFrontPanel Status = %d",status);
			CEMDEBUG(g_Dbg, 5, "SWXSRVR", msg);
		}
//		IFNSIM(g_Sim,(status = SetUUTEnableFrontPanel(false)));
	}
    if(status && (status != 258))
    {
//        IFNSIM(g_Sim,(ErrorMessage(status, msg)));
	    sprintf(UUTmsg,"s_SetUutEnable SetUUTEnableFrontPanel Status = %d, [%s]",status,msg);
        CEMDEBUG(g_Dbg, 5, "SWXSRVR", UUTmsg);
        *ErrMsg = UUTmsg;
        return(SU_STA_INTERLOCK);
    }

    // Enable power supplies
//	IFNSIM(g_Sim,(status = EnableUUTAll()));
	cnt = 5;
    // 258 is intermittant communication error
	while((status == 258) && (cnt-- > 0))
	{
		Sleep(100);
		if(status) 
		{
			sprintf(msg,"s_SetUutEnable EnableUUTAll Status = %d",status);
			CEMDEBUG(g_Dbg, 5, "SWXSRVR", msg);
		}
//	    IFNSIM(g_Sim,(status = EnableUUTAll()));
	}
    if(status && (status != 258))
    {
//        IFNSIM(g_Sim,(ErrorMessage(status, msg)));
	    sprintf(UUTmsg,"s_SetUutEnable EnableUUTAll Status = %d, [%s]",status,msg);
        CEMDEBUG(g_Dbg, 5, "SWXSRVR", UUTmsg);
        *ErrMsg = UUTmsg;
        return(SU_STA_INTERLOCK);
	}
    if(g_Sim) s_SimUutEnabled = true;
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function:	s_SetUutDisable
//
// Purpose:		Set the UUT power disable light state (red)
//
///////////////////////////////////////////////////////////////////////////////
static int s_SetUutDisable(char **ErrMsg)
{
    int status = 0;
	char msg[1024];
    int cnt;

    CEMDEBUG(g_Dbg, 5, "SWXSRVR", "s_SetUutDisable");

    msg[0]='\0';

    //Disable the power supplies
//	IFNSIM(g_Sim,(status = DisableUUTAll()));
	cnt = 5;
    // 258 is intermittant communication error
	while((status == 258) && (cnt-- > 0))
	{
		Sleep(100);
		if(status) 
		{
			sprintf(msg,"s_SetUutDisable DisableUUTAll Status = %d",status);
			CEMDEBUG(g_Dbg, 5, "SWXSRVR", msg);
		}
//		IFNSIM(g_Sim,(status = DisableUUTAll()));
	}
    if(status && (status != 258) && (status != 2833)) // 2833 is "Already disabled message
    {
//        IFNSIM(g_Sim,(ErrorMessage(status, msg)));
	    sprintf(UUTmsg,"s_SetUutDisable DisableUUTAll Status = %d, [%s]",status,msg);
        CEMDEBUG(g_Dbg, 5, "SWXSRVR", UUTmsg);
        *ErrMsg = UUTmsg;
        return(SU_STA_INIT);
	}

    // Set the red light
//	IFNSIM(g_Sim,(status = SetUUTInterruptFrontPanel(false)));
	cnt = 5;
    // 258 is intermittant communication error
	while((status == 258) && (cnt-- > 0))
	{
		Sleep(100);
		if(status) 
		{
			sprintf(msg,"s_SetUutDisable SetUUTInterruptFrontPanel Status = %d",status);
			CEMDEBUG(g_Dbg, 5, "SWXSRVR", msg);
		}
//		IFNSIM(g_Sim,(status = SetUUTInterruptFrontPanel(false)));
	}
    if(status && (status != 258) && (status != 2833)) // 2833 is "Already disabled message
    {
//        IFNSIM(g_Sim,(ErrorMessage(status, msg)));
	    sprintf(UUTmsg,"s_SetUutDisable SetUUTInterruptFrontPanel Status = %d, [%s]",status,msg);
        CEMDEBUG(g_Dbg, 5, "SWXSRVR", UUTmsg);
        *ErrMsg = UUTmsg;
        return(SU_STA_INIT);
	}

    if(g_Sim) s_SimUutEnabled = false;
    return(0);
}
/**/

///////////////////////////////////////////////////////////////////////////////
// Function:	s_CheckAvailability
//
// Purpose:		Query the station for device availability
//
///////////////////////////////////////////////////////////////////////////////
static int  s_CheckAvailability(char *DevName, char *AvailValue,
                                char *Response, int BufferSize)
{
    int i;
    int Status = 0;

    for(i=0; i<s_InstrStatusCount; i++)
    {
        if(strcmp(DevName, s_InstrStatus[i].ResourceName) == 0)
        {
            strncpy(AvailValue, s_InstrStatus[i].Availability,ATXML_MAX_AVAIL_VALUE);
            return(0);
        }
    }

    // If we fell out of loop the device is not in the table yet
    Status = s_DeviceAvailabilityQuery(DevName, AvailValue, Response, BufferSize);

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function:	s_DeviceAvailabilityQuery
//
// Purpose:		Query the station for device availability
//
///////////////////////////////////////////////////////////////////////////////
static int  s_DeviceAvailabilityQuery(char *DevName, char *AvailValue,
                                      char *Response, int BufferSize)
{
    int Status = 0;
    ATXML_XML_String XmlTestReauirements[1024];

    if(s_InstrStatusCount == 0)
    {
        s_InstrStatus.resize(50);
    }
    // Create a single test requirement XML Snippet
    sprintf(XmlTestReauirements,
        "<AtXmlTestRequirements> "
		"    <ResourceRequirement> "
		"	    <ResourceType>Source</ResourceType> "
		"	    <SignalResourceName>%s</SignalResourceName> "
		"    </ResourceRequirement> "
    	"</AtXmlTestRequirements>",
        DevName);

    // Query system for availability
    Status = atxml_ValidateRequirements(XmlTestReauirements, "PawsAllocation.xml",
                             Response, BufferSize);
    if(Status)
    {
        return(Status);
    }
    Status = atxml_ParseAvailability(Response,
                             &s_InstrStatus[s_InstrStatusCount++], 1);
    strnzcpy(AvailValue, s_InstrStatus[s_InstrStatusCount-1].Availability,ATXML_MAX_AVAIL_VALUE);

    return(Status);
}


