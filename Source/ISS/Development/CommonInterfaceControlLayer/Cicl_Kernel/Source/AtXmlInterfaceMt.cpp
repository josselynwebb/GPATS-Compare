//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlInterface.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXML Api Interface 
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline Release                        
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "soapH.h"
#include "AtXmlInterface.nsmap"
#include "CiCoreCommon.h"
#include "plugin.h"

// Procedure Type constants for Initialie call from AtXmlApiInterfaceX.h 
#define ATXML_PROC_TYPE_PAWS_WRTS  "PAWS_WRTS"
#define ATXML_PROC_TYPE_PAWS_NAM   "PAWS_NAM"
#define ATXML_PROC_TYPE_SFP        "SFP"
#define ATXML_PROC_TYPE_TPS        "TPS"
#define ATXML_PROC_TYPE_SFT        "SFT_TPS"

// Local Function Prototypes
static char *s_AllocBuffer1(int BufferSize, int ProcHandle);
static char *s_AllocBuffer2(int BufferSize, int ProcHandle);
static void  s_PurgeProcInfo(PROC_INFO *ProcPtr);
static DWORD __stdcall s_SoapThread(void* soap);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_IntfInitialize
//
// Purpose: Initialize the AtXml
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////

int atxml_IntfInitialize(void)
{
   int Status = 0;
   struct soap soap, *tsoap;
   int i, m, s; // master and slave sockets

   memset(g_ProcInfo, 0, sizeof(g_ProcInfo));

   soap_init2(&soap, SOAP_IO_KEEPALIVE, SOAP_IO_KEEPALIVE);
   //soap_register_plugin(&soap, plugin);  //Don't remove this is used for troubleshooting
   //soap.max_keep_alive = 100; // optional: at most 100 calls per keep-alive session
   //soap.accept_timeout = 600; // optional: let server time out after ten minutes of inactivity
   m = soap_bind(&soap, "localhost", 7014, 100);
   if (m < 0)
      soap_print_fault(&soap, stderr);
   else
   {
      CICOREDBGLOG(atxml_FmtMsg("Socket connection successful: master socket = %d\n", m));
      for (i = 1; g_ApiThreadStatus == API_STATUS_RUNNING; i++)
      {
         s = soap_accept(&soap);
         if (s < 0)
         {
            soap_print_fault(&soap, stderr);
            break;
         }
         CICOREDBGLOG(atxml_FmtMsg("%d: accepted connection from IP=%d.%d.%d.%d socket=%d\n", i,
            (soap.ip >> 24)&0xFF, (soap.ip >> 16)&0xFF, (soap.ip >> 8)&0xFF, soap.ip&0xFF, s));
         // Keep Alive Multi-Thead Operation
         tsoap = soap_copy(&soap);
         CreateThread(NULL, 0, s_SoapThread, (void*)tsoap, 0, NULL);
         // rest is handled by thread      
      }
   }

   return (Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_IntfClose
//
// Purpose: Close the AtXml
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
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////

int atxml_IntfClose(void)
{
    int i;

    for(i=0; i< MAX_PROCS; i++)
    {
        if(g_ProcInfo[i].Handle == 0)
            continue;
        s_PurgeProcInfo(&(g_ProcInfo[i]));
    }
	return 0;
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml__Initialize
//
// Purpose: Register the UUID and Process ID for this connection.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// ProcUuid          char*               Pass in the calling programs UUID
// Pid               int                 Calling programs process id
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// result           int*                gsoap result
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__Initialize(struct soap*,
					  char *ProcType, 
					  char *ProcUuid, int Pid, int *result)
{
    int Status = 0;
    int Handle = 0;
    int i;

    // Find an empty ProcInfo entry
    for(i=1; i<MAX_PROCS; i++)
    {
        if(g_ProcInfo[i].Handle == 0)
        {
            Handle = i;
            g_ProcInfo[i].Handle = Handle;
            strnzcpy(g_ProcInfo[i].Guid,ProcUuid,80);
            g_ProcInfo[i].Pid = Pid;
            g_ProcInfo[i].Thread = GetCurrentThreadId();
            g_ProcInfo[i].Type = ATXML_PROC_TYPE_OTHER_INT;
            if(strcmp(ProcType,ATXML_PROC_TYPE_PAWS_WRTS)==0)
                g_ProcInfo[i].Type = ATXML_PROC_TYPE_PAWS_WRTS_INT;
            else if(strcmp(ProcType,ATXML_PROC_TYPE_PAWS_NAM)==0)
                g_ProcInfo[i].Type = ATXML_PROC_TYPE_PAWS_NAM_INT;
            else if(strcmp(ProcType,ATXML_PROC_TYPE_SFP)==0)
                g_ProcInfo[i].Type = ATXML_PROC_TYPE_SFP_INT;
            else if(strcmp(ProcType,ATXML_PROC_TYPE_TPS)==0)
                g_ProcInfo[i].Type = ATXML_PROC_TYPE_TPS_INT;
            else if(strcmp(ProcType,ATXML_PROC_TYPE_SFT)==0)
                g_ProcInfo[i].Type = ATXML_PROC_TYPE_SFT_INT;
            // Leave buffers unallocated until first use
            // Leave Resources until first validate
            smam_Initialize(Handle, ProcUuid, g_ProcInfo[i].Type, Pid);
            break;
        }
    }
    *result = Handle;

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml__Close
//
// Purpose: Close this connection.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// ProcUuid          char*               Pass in the calling programs UUID
// Pid               int                 Calling programs process id
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// result           int*                gsoap result
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__Close(struct soap*, int Handle, 
				         char *ProcUuid, int Pid,
				         int *result)
{
    int Status = 0;
    int ProcIdx;

    // signal Smam that the Client is closing
    smam_Close(Handle);

    // Clean-up Proc Info
    if((ProcIdx = api_GetProcIdx(Handle)) < 1)
        return(0); //FIX later diagnose
    s_PurgeProcInfo(&g_ProcInfo[ProcIdx]);

    *result = Status;
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_RegisterTSF
//
// Purpose: Send an ATML IEEE 1641 (TSF) description to the AtXml
//          For efficency sake, This function is not currently implemented
//          When implemented, It will cash the TSF locally and Interseed
//          IssueSignal via local static routines.
//          It will not pass it on to the _T processing!
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
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__RegisterTSF(struct soap*, int Handle, 
				         char *TSFSignalDefinition, char *TSFLibrary, char *STDTSF, char *STDBSC, int *result)
{
    int Status = 0;

    *result = Status;
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_ValidateRequirements
//
// Purpose: Send the ATML requirements section of the Test Description and the
//          name of an XML file that assigns station assets to test description
//          requirements to the ATXML for allocation and to verify
//          asset availability.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// TestRequirements  ATXML_ATML_Snippet   Provide the Test Requirements via Resource
//                                       Description or Resource ID
// Allocation        ATXML_XML_Filename   XML file that maps the target station asset
//                                       names to the requirement names in the
//                                       Test Requirements
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Availability     ATXML_XML_String*    XML string that identifies the 
//                                      requirement / assigned asset / and status
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__ValidateRequirements(struct soap*, int Handle, 
				      char *TestRequirements, char *Allocation,
                      int BufferSize, struct atxml__ValidateRequirementsResponse &r)
{
    int Status = 0;
    char *LclAvailability;

    LclAvailability = s_AllocBuffer1(BufferSize,Handle);
    Status = smam_ValidateRequirements(Handle, TestRequirements,
                              Allocation,
                              LclAvailability, BufferSize);
    r.Availability = LclAvailability;
    r.result = Status;
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_IssueSignal
//
// Purpose: Send an ATML IEEE 1641 (BSC) signal description to the AtXml
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// SignalDescription ATXML_ATML_Snippet*  IEEE 1641 BSC Signal description + action/resource
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXML_XML_String*    Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////

int atxml__IssueSignal(struct soap*, int Handle, 
				     char *SignalDescription, 
                     int BufferSize, struct atxml__IssueSignalResponse &r)
{
    int Status = 0;

    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_IssueSignal(Handle, SignalDescription, LclResponse, BufferSize);

    r.Response = LclResponse;
    r.result = Status;

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_TestStationStatus
//
// Purpose: Query the Test Station status
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		    Type                Purpose
// ===================  =================== ===========================================
// TestStationInstance  ATXML_XML_String*    Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__TestStationStatus(struct soap*, int Handle, 
				         int BufferSize, struct atxml__TestStationStatusResponse &r)
{

    int Status = 0;

    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_TestStationStatus(Handle, NULL, LclResponse, BufferSize);

    r.TestStationStatus = LclResponse;
    r.result = Status;
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_RegisterInstStatus
//
// Purpose: Register the latest Calibration Factors to the AtXml
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// InstStatus        ATXML_XML_String*    XML String for intsrument status / cal data
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXML_XML_String*    Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__RegisterInstStatus(struct soap*, int Handle,
					    char *InstStatus, 
                        int BufferSize, struct atxml__RegisterInstStatusResponse &r)
{
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_RegisterRemoveSequence
//
// Purpose: Register the required reset sequence
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// RemoveSequence    ATXML_XML_String*   XML String for resource list
//
// Output Parameters
// Parameter		Type			     Purpose
// ===============  ===================  ===========================================
// Response         ATXML_XML_String*    Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__RegisterRemoveSequence(struct soap*, int Handle, 
				         char *RemoveSequence, int BufferSize, struct atxml__RegisterRemoveSequenceResponse &r)
{
    int Status = 0;

    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_RegisterRemoveSequence(Handle, RemoveSequence, LclResponse, BufferSize);

    r.Response = LclResponse;
    r.result = Status;
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_InvokeRemoveAllSequence
//
// Purpose: Reset the station assets
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXML_XML_String*    Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__InvokeRemoveAllSequence(struct soap*, int Handle, 
				         int BufferSize, struct atxml__InvokeRemoveAllSequenceResponse &r)
{
    int Status = 0;

    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_InvokeRemoveAllSequence(Handle, LclResponse, BufferSize);

    r.Response = LclResponse;
    r.result = Status;

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_RegisterApplySequence
//
// Purpose: Register the required reset sequence
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// RemoveSequence    ATXML_XML_String*   XML String for resource list
//
// Output Parameters
// Parameter		Type			     Purpose
// ===============  ===================  ===========================================
// Response         ATXML_XML_String*    Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__RegisterApplySequence(struct soap*, int Handle, 
				         char *ApplySequence, int BufferSize, struct atxml__RegisterApplySequenceResponse &r)
{
    int Status = 0;

    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_RegisterApplySequence(Handle, ApplySequence, LclResponse, BufferSize);

    r.Response = LclResponse;
    r.result = Status;
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_InvokeRemoveAllSequence
//
// Purpose: Reset the station assets
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXML_XML_String*    Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__InvokeApplyAllSequence(struct soap*, int Handle, 
				         int BufferSize, struct atxml__InvokeApplyAllSequenceResponse &r)
{
    int Status = 0;

    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_InvokeApplyAllSequence(Handle, LclResponse, BufferSize);

    r.Response = LclResponse;
    r.result = Status;

    return(0);
}


//++++/////////////////////////////////////////////////////////////////////////
// Special Non-ATML Cheat Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_IssueIst
//
// Purpose: Send a command to perform Instrument Self Test
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// InstSelfTest      ATXML_XML_String*  IssueIst Message string
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXML_XML_String*    Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////

int atxml__IssueIst(struct soap*, int Handle, 
				     char *InstSelfTest, 
                     int BufferSize, struct atxml__IssueIstResponse &r)
{
    int Status = 0;

    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_IssueIst(Handle, InstSelfTest, LclResponse, BufferSize);

    r.Response = LclResponse;
    r.result = Status;

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_IssueNativeCmds
//
// Purpose: Invoke Instrument Self Test and return response
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// InstrumentCmds    ATXML_XML_String*  XML String for Instrument Commands
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXML_XML_String* Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__IssueNativeCmds(struct soap*, int Handle, 
				         char *InstrumentCmds, 
                         int BufferSize, struct atxml__IssueNativeCmdsResponse &r)
{

    int Status = 0;
    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_IssueNativeCmds(Handle, InstrumentCmds, LclResponse, BufferSize);

    r.Response = LclResponse;
    r.result = Status;
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_IssueDriverFunctionCall
//
// Purpose: Invoke Instrument Self Test and return response
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// DriverFunction    ATML_INTF_DRVRFNC*  XML String for Driver Function and parameters
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXML_XML_String* Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int atxml__IssueDriverFunctionCall(struct soap*, int Handle, 
				         char *DriverFunction, 
                        int BufferSize, struct atxml__IssueDriverFunctionCallResponse &r)
{
    int Status = 0;
    char *LclResponse;
    LclResponse = s_AllocBuffer1(BufferSize,Handle);

    Status = smam_IssueDriverFunctionCall(Handle, DriverFunction, LclResponse, BufferSize);

    r.Response = LclResponse;
    r.result = Status;
    return(0);
}

                                  
//++++/////////////////////////////////////////////////////////////////////////
// Non-Signal Interface Functions
///////////////////////////////////////////////////////////////////////////////
int atxml__RegisterInterUsed(struct soap*, int Handle, 
				         char *InterUsage, int *result)
{return(0);}
int atxml__RetrieveTpsData(struct soap*, int Handle, 
				         struct atxml__RetrieveTpsDataResponse &r)
{return(0);}
int atxml__RegisterTmaSelect(struct soap*, int Handle, 
				         char *TmaList, int *result)
{return(0);}
int atxml__SubmitUutId(struct soap*, int Handle, 
				         char *UUT_Partnumber, char *UUT_Serialnumber, int TmaBufferSize, int RaBufferSize, struct atxml__SubmitUutIdResponse &r)
{return(0);}
int atxml__QueryInterStatus(struct soap*, int Handle, 
				         int BufferSize, struct atxml__QueryInterStatusResponse &r)
{return(0);}
int atxml__IssueTestResults(struct soap*, int Handle, 
				         char *TestResults, int TPS_Status, int BufferSize, struct atxml__IssueTestResultsResponse &r)
{return(0);}
int atxml__IssueTestResultsFile(struct soap*, int Handle, 
				         char *TestResultsFile, int TPS_Status, int BufferSize, struct atxml__IssueTestResultsFileResponse &r)
{return(0);}

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Interface Utility Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: api_GetProcIdx
//
// Purpose: Convert the ProcHandle assigned to this Client to the ProcInfo index
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// ProHandle         int                 Client assigned handle
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    ProcInfo index
//    0 on failure
//
///////////////////////////////////////////////////////////////////////////////
int api_GetProcIdx(int ProcHandle)
{
    return(ProcHandle);
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: s_AllocBuffer1
//
// Purpose: Insure that this thread's Buffer 1 is big enough
//           //FIX maybe later actually shrink buffer after x tries < 4096?
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// BufferSize        int                 Size of buffer needed
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    pointer to buffer.
//    NULL on failure
//
///////////////////////////////////////////////////////////////////////////////
static char *s_AllocBuffer1(int BufferSize, int ProcHandle)
{
    int BufIdx;

    BufIdx = api_GetProcIdx(ProcHandle);
    if(g_ProcInfo[BufIdx].RespBuf1Size < BufferSize)
    {
        // Allocate more space
        if(g_ProcInfo[BufIdx].RespBuf1)
            delete(g_ProcInfo[BufIdx].RespBuf1);
        g_ProcInfo[BufIdx].RespBuf1 = new char[BufferSize+4];
    }
    if(g_ProcInfo[BufIdx].RespBuf1 == NULL)
        g_ProcInfo[BufIdx].RespBuf1Size = 0;
    else
        g_ProcInfo[BufIdx].RespBuf1[0] = '\0';

    return(g_ProcInfo[BufIdx].RespBuf1);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_AllocBuffer2
//
// Purpose: Insure that this thread's Buffer 2 is big enough
//           //FIX maybe later actually shrink buffer after x tries < 4096?
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// BufferSize        int                 Size of buffer needed
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    pointer to buffer.
//    NULL on failure
//
///////////////////////////////////////////////////////////////////////////////
static char *s_AllocBuffer2(int BufferSize, int ProcHandle)
{
    int BufIdx;

    BufIdx = api_GetProcIdx(ProcHandle);
    if(g_ProcInfo[BufIdx].RespBuf2Size < BufferSize)
    {
        // Allocate more space
        if(g_ProcInfo[BufIdx].RespBuf2)
            delete(g_ProcInfo[BufIdx].RespBuf2);
        g_ProcInfo[BufIdx].RespBuf2 = new char[BufferSize+4];
    }
    if(g_ProcInfo[BufIdx].RespBuf2 == NULL)
        g_ProcInfo[BufIdx].RespBuf2Size = 0;
    else
        g_ProcInfo[BufIdx].RespBuf2[0] = '\0';
    return(g_ProcInfo[BufIdx].RespBuf2);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_PurgeProcInfo
//
// Purpose: Purge the specified ProcInfo entry to release memory etc.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// ProcPtr           PROC_INFO*          Pointer to PROC_INFO entry to purge
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    none
//
///////////////////////////////////////////////////////////////////////////////
static void  s_PurgeProcInfo(PROC_INFO *ProcPtr)
{
    ProcPtr->Handle = 0;
    ProcPtr->Guid[0] = '\0';
    ProcPtr->Pid = 0;
    ProcPtr->Type = 0;
    ProcPtr->Thread = 0;
    // Release Proc Resources
    //Fixed Resource Buffer for the current time
    ProcPtr->Resources.ResourceCount = 0;
    // Release Response Buffers
    if(ProcPtr->RespBuf1)
        delete(ProcPtr->RespBuf1);
    ProcPtr->RespBuf1 = NULL;
    ProcPtr->RespBuf1Size = 0;
    if(ProcPtr->RespBuf2)
        delete(ProcPtr->RespBuf2);
    ProcPtr->RespBuf2 = NULL;
    ProcPtr->RespBuf2Size = 0;

    return;
}

DWORD __stdcall s_SoapThread(void* soap)
{
    //((struct soap*)soap)->recv_timeout = 300; // Timeout after 5 minutes stall on recv
    //((struct soap*)soap)->send_timeout = 60; // Timeout after 1 minute stall on send
    soap_serve((struct soap*)soap);// process RPC requests
    soap_destroy((struct soap*)soap);// clean up class instances after timeout/disconnect
    soap_end((struct soap*)soap);// clean up everything and close socket
    soap_done((struct soap*)soap);
    free(soap);
    return (0);
}
