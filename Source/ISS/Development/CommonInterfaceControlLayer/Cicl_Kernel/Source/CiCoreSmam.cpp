///////////////////////////////////////////////////////////////////////////////
// File    : CiCoreSmam.cpp
//
// Purpose : Functions for supporting the AtXml instrument support ;
//
//
// Date:	11OCT05
//
// Functions
// Name						Purpose
// =======================  ===================================================
//
// Revision History
// Rev	    Date                  Reason							Author
// =======  ========  =======================================  ====================
// 1.1.0.0  30OCT06   Initial baseline release.                T.G.McQuillen EADS
// 1.1.0.1  13NOV06   Updated "InUse" flag setting             T.G.McQuillen EADS
// 1.3.0.1  15MAY07   Modified RemoveAll sequence              T.G.McQuillen EADS
// 1.3.1.0  04APR09   Modified code to fix memory overrun      EADS
// 1.4.2.0  29JUL09   Modified code in function
//                    s_ProcessRemoveAll to fix bug that 
//                    dropped out of sequence when supplies
//                    were cycled on/off/on.                   E. Larson EADS
///////////////////////////////////////////////////////////////////////////////
#define _WIN32_DCOM
#include <fstream>
#include <time.h>
#include "CiCoreCommon.h"
#include "WrapperInf.h"

#define DYNAMIC_REMOVE_ALL
// Availability values declared in AtXmlInterfaceApiC.h
/**/
#define ATXML_AVAIL_AVAILABLE      "Available"
#define ATXML_AVAIL_FAILED_ST      "FailedST"
#define ATXML_AVAIL_CAL_EXPIRED    "CalExpired"
#define ATXML_AVAIL_NO_RESPONSE    "NoResponse"
#define ATXML_AVAIL_NOT_FOUND      "NotFound"
#define ATXML_AVAIL_SIMULATED      "Simulated"
/**/

// Global Data Instanced In This Module
// Set SmamDebug flag
bool g_SmamDbg;

INST_DATA          g_InstData[ATXML_MAX_INST];
int                g_InstDataCount = 0;
int                g_WrapperClassCount;
WRAPPER_CLASS      g_WrapperClass[ATXML_MAX_WRAPPERS];
REMOVE_SEQUENCE    g_SystemRemoveAll;
REMOVE_SEQUENCE   *g_ProgrammableRemoveAll=NULL;
APPLY_SEQUENCE     g_SystemApplyAll;
APPLY_SEQUENCE    *g_ProgrammableApplyAll=NULL;

// Local Defines
typedef struct IcAvailabilityStruct {
    char ResourceName[ATXML_MAX_NAME];
    char Availability[ATXML_MAX_AVAIL_VALUE];
} SMAM_ICAVAILABILITY;

typedef struct InstStatusStruct {
    char SignalID[ATXML_MAX_NAME];
    char SignalName[ATXML_MAX_NAME];
    char ResourceID[ATXML_MAX_NAME];
    char ResourceName[ATXML_MAX_NAME];
    char StationInstName[ATXML_MAX_NAME];
    char StationResourceName[ATXML_MAX_NAME];
    char Availability[ATXML_MAX_AVAIL_VALUE];
    char AllocatedTo[ATXML_MAX_AVAIL_VALUE];
	time_t LastCalTime;
	time_t LastSelfTestTpsDate;
	bool   LastSelfTestTpsStatus;
	time_t LastIstDate;
	bool   LastIstStatus;
} SMAM_INST_STATUS;

// Local Static Variables
char s_ConfigPath[256] = "";
#ifdef DYNAMIC_REMOVE_ALL
bool s_FirstTimeMasterReset = true;
#else
bool s_FirstTimeMasterReset = false;
#endif

#ifdef DYNAMIC_APPLY_ALL
bool s_FirstTimeMasterApply = true;
#else
bool s_FirstTimeMasterApply = false;
#endif

// Local Function Prototypes
static int  s_ParseTestStationCfgFile(void);
static int  s_ParseAllocationFile(int ProcHandle,
                       ATXML_XML_Filename *Allocation,
                       ATXML_XML_String *Response, int BufferSize);
static void s_GetConfigPath(void);
static int  s_Allocate(int ProcHandle,
                       char *ResourceName, char *DeviceName, char *StationResourceName,
                       ATXML_XML_String *Response, int BufferSize);
static int  s_InitInstrument(INST_DATA *InstPtr, char *ResourceName,
                                  ATXML_XML_String *Response, int BufferSize);
static WRAPPER_CLASS *s_InsertWrapperClass(char *WrapperName);
static INST_DATA     *s_LocateInstrment(int ProcHandle,
                                    ATXML_ATML_Snippet *SignalDescription,
                                    ATXML_ATML_Snippet *WrapSignalDescription,
                                    int *ResourceType, char *ResourceName,
                                    RESOURCE_INFO **ResourcePtr,
                                    ATXML_XML_String *Response, int BufferSize);
static void s_InsertPhysResource(INST_DATA *InstPtr);
static void s_InsertVirtResource(INST_DATA *InstPtr);
static void s_InitPhysResourcePtrs(INST_DATA *InstPtr, VIRTUAL_RESOURCE * VirtPtr,
                                   char *ReourcesUsed);
static void  s_InitAllInstruments(void);
static void  s_FillSystemRemoveAllSequence(void);
static void  s_FillProgRemoveAllSequence(int ProcHandle);
static int   s_ParseRemoveSequence(int ProcHandle, ATXML_XML_String *RemoveSequence,
                                   ATXML_XML_String *Response, int BufferSize);
static int   s_ParseApplySequence(int ProcHandle, ATXML_XML_String *ApplySequence,
                                   ATXML_XML_String *Response, int BufferSize);
static int   s_ProcessRemoveAll(REMOVE_SEQUENCE *RemoveSequence, bool MasterReset,
                                ATXML_XML_String *Response, int BufferSize);
static int   s_ProcessApplyAll(APPLY_SEQUENCE *ApplySequence, bool MasterApply,
                                ATXML_XML_String *Response, int BufferSize);
static void  s_PurgeProgRemoveAll(int ProcHandle);
static void  s_PurgeProgApplyAll(int ProcHandle);

static RESOURCE_INFO *s_FindResourceByName(char *ResourceName, PROC_RESOURCES *ResourceInfo);
static void  s_FillInstStatusInfo(RESOURCE_INFO *ResourcePtr,SMAM_INST_STATUS *InstStatusPtr);
static void  s_IssueResourceStatus(SMAM_INST_STATUS *InstStatusPtr,char *Response,int BufferSize);

static bool s_TcStartIssueSignal(int ProcHandle, RESOURCE_INFO *ResourcePtr,
                                 int Reset, bool *SendFlag);
static void s_TcStopIssueSignal(int ProcHandle, RESOURCE_INFO *ResourcePtr, int Reset);
static bool s_TcStartIst(int ProcHandle, RESOURCE_INFO *ResourcePtr,bool *SendFlag);
static void s_TcStopIst(int ProcHandle, RESOURCE_INFO *ResourcePtr);
static bool s_TcStartIssueCmds(int ProcHandle, RESOURCE_INFO *ResourcePtr,
                                 int Reset, bool *SendFlag);
static void s_TcStopIssueCmds(int ProcHandle, RESOURCE_INFO *ResourcePtr, int Reset);
static bool s_TcStartDriverFunction(int ProcHandle, RESOURCE_INFO *ResourcePtr,
                                 int Reset, bool *SendFlag);
static void s_TcStopDriverFunction(int ProcHandle, RESOURCE_INFO *ResourcePtr, int Reset);
static bool s_TcStartReset(int ProcHandle, RESOURCE_INFO *ResourcePtr, bool *SendFlag);
static void s_TcStopReset(int ProcHandle,RESOURCE_INFO *ResourcePtr);
static bool s_TcNotActive(int ProcHandle,RESOURCE_INFO *ResourcePtr,bool *SendFlag);
static void s_TcStopActive(int ProcHandle, RESOURCE_INFO *ResourcePtr);
static void s_TcRegisterInUse(int ProcHandle, RESOURCE_INFO *ResourcePtr, bool On);
static int  s_TcGetVirtualActiveValue(int ProcHandle,VIRTUAL_RESOURCE *VirtualPtr);
static void s_TcSetVirtualActiveValue(int ProcHandle,VIRTUAL_RESOURCE *VirtualPtr);
static void s_TcSetVirtualInUseValue(bool InUse, VIRTUAL_RESOURCE *VirtualPtr);
static bool s_TcGetVirtualInUseValue(VIRTUAL_RESOURCE *VirtualPtr);
static bool s_TcCheckStatus(int AvailInt,bool *SendFlag);

static void s_SetDynamicRemoveAll(int ProcHandle, RESOURCE_INFO *ResourcePtr, bool Add);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: int smam_MainInitalize
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
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int smam_MainInitialize(void)
{
    int Status = 0;

    CICOREDBGLOG("Smam Opening");

    // Init System Remove All Sequence
    memset(&g_SystemRemoveAll, 0, sizeof(REMOVE_SEQUENCE));

	// Main CoInitialize
	HRESULT hr = CoInitializeEx(NULL,COINIT_MULTITHREADED);
    // Get TestStationInfo
    CICOREDBGLOG("s_ParseTestStationCfgFile:");
    Status = s_ParseTestStationCfgFile();
    CICOREDBGLOG(atxml_FmtMsg("  Status - %d",Status));

    s_InitAllInstruments();

    s_FillSystemRemoveAllSequence();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int smam_MainClose
//
// Purpose: Close the SMAM
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
int smam_MainClose(void)
{
    int   Status = 0;
    int   Idx,i;
    //Reset all devices
    smam_InvokeRemoveAllSequence(0,NULL,0);
    //Free dataspaces
    //// g_ProgrammableRemoveAll Data Space
    if(g_ProgrammableRemoveAll)
    {
        delete(g_ProgrammableRemoveAll);
        g_ProgrammableRemoveAll = NULL;
    }
    //// Instrument Data space
    for(Idx=0; Idx<g_InstDataCount; Idx++)
    {
        if(g_InstData[Idx].PhysicalResources != NULL)
            delete (g_InstData[Idx].PhysicalResources);
        g_InstData[Idx].PhysicalResources = NULL;
        g_InstData[Idx].PhysCount = 0;
        if((g_InstData[Idx].VirtualResources != NULL))
        {
            for(i=0;i<g_InstData[Idx].VirtCount;i++)
            {
                if((g_InstData[Idx].VirtualResources[i].PhysicalResourcesUsed != NULL))
                    delete(g_InstData[Idx].VirtualResources[i].PhysicalResourcesUsed);
                g_InstData[Idx].VirtualResources[i].PhysicalResourcesUsed = NULL;
                if((g_InstData[Idx].VirtualResources[i].PhysResources != NULL))
                    delete(g_InstData[Idx].VirtualResources[i].PhysResources);
                g_InstData[Idx].VirtualResources[i].PhysResources = NULL;
                g_InstData[Idx].VirtualResources[i].PhysicalResourceCount = 0;
            }
        }
    }
    // Purge Wrapper classes to close COM stuff
    for(Idx=0; Idx<g_WrapperClassCount; Idx++)
    {
        if(g_WrapperClass[Idx].WrapperClass)
            delete((CWrapperInf*)g_WrapperClass[Idx].WrapperClass);
        g_WrapperClass[Idx].WrapperClass = NULL;
        g_WrapperClass[Idx].WrapperName[0] = '\0';
    }

	// Main CoUninitialize
	CoUninitialize();
    CICOREDBGLOG("Smam Closing");
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: int smam_Initalize
//
// Purpose: Initialize a new AtXml Interface Connection
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
int smam_Initialize(int Handle, char *ProcUuid, int ProcType,
                                         int Pid)
{
    int Status = 0;

    CICOREDBGLOG(atxml_FmtMsg("atxml_Initialize(%d, %s, %d, %d)",
                Handle,ProcUuid,ProcType,Pid));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int smam_Close
//
// Purpose: Close the AtXml Interface Connection
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
int smam_Close(int ProcHandle)
{
    int   Status = 0;
    int   ProcType;

    CICOREDBGLOG(atxml_FmtMsg("atxml_Close(%d)",ProcHandle));

    ProcType = g_ProcInfo[api_GetProcIdx(ProcHandle)].Type;
    if((ProcType == ATXML_PROC_TYPE_PAWS_WRTS_INT) ||
    (ProcType == ATXML_PROC_TYPE_TPS_INT) ||
    (ProcType == ATXML_PROC_TYPE_SFT_INT))
    {
        // Reset all the Assets for this Client
        smam_InvokeRemoveAllSequence(ProcHandle, NULL, 0);

	    s_PurgeProgRemoveAll(ProcHandle);

		s_PurgeProgApplyAll(ProcHandle);
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int smam_ValidateRequirements
//
// Purpose: Process the TestRequirements and the
//          name of an XML file that assigns station assets to test requirements
//          requirements for allocation and to verify asset availability.
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
int smam_ValidateRequirements(int ProcHandle,
                              ATXML_ATML_Snippet* TestRequirements,
                              ATXML_XML_Filename* Allocation,
                              ATXML_XML_String* Availability, int BufferSize)
{
    int   Status = 0;
    char *cptr;
    int   bufidx;
    int   i;
    SMAM_ICAVAILABILITY ReqAvail[ATXML_MAX_RESOURCES];
    int   ReqAvailCount = 0;
    char  TempBuf[512];
    char *AvailStr = NULL;
    int   ProcType;
	bool  TpsType = false;
	bool  InUse;
    PROC_RESOURCES *ResourceInfo;
	RESOURCE_INFO *ResourcePtr;

    // empty Availability
    if(Availability != NULL)
        *Availability = '\0';

    // Parse Test Requirements and place in ReqAvail Table
    if(TestRequirements == NULL)
    {
        atxml_ErrorResponse(Availability, BufferSize, 
                           "Validate Requirements", 
                           ATXML_ERRCD_TEST_REQUIREMENTS, ATXML_ERRMSG_TEST_REQUIREMENTS);
        return(ATXML_ERRCD_TEST_REQUIREMENTS); //FIX Later Diagnose
    }
/*
        <AgAtXmlstRequirements>
		    <ResourceRequirement>
			    <ResourceType>Source</ResourceType>
			    <SignalResourceName>DMM_1</SignalResourceName>
		    </ResourceRequirement>
	    </AgAtXmlstRequirements>
*/
    ProcType = g_ProcInfo[api_GetProcIdx(ProcHandle)].Type;
    if((ProcType == ATXML_PROC_TYPE_PAWS_WRTS_INT) ||
    (ProcType == ATXML_PROC_TYPE_TPS_INT) ||
    (ProcType == ATXML_PROC_TYPE_SFT_INT))
	    TpsType = true;

    CICOREDBGLOG(atxml_FmtMsg("smam_ValidateRequirements - ProcHandle %d\n%s",ProcHandle,TestRequirements));
    // Find ResourceNames for requested availability
    if((cptr = strstr(TestRequirements,"<SignalResourceName>")) == NULL)
        atxml_ErrorResponse(Availability, BufferSize, 
                           "Validate Requirements", 
                           ATXML_ERRCD_TEST_REQUIREMENTS, ATXML_ERRMSG_TEST_REQUIREMENTS);
    while(cptr)
    {
        if(sscanf(cptr,"<SignalResourceName> %[^< \t\n]",
                      ReqAvail[ReqAvailCount].ResourceName) >0)
        {
            strcpy(ReqAvail[ReqAvailCount].Availability,ATXML_AVAIL_NOT_FOUND); 
            ReqAvailCount++;
        }
        cptr = strstr(++cptr,"<SignalResourceName>");
    }
    // Point at this ResourceInfo Entry
    ResourceInfo = &(g_ProcInfo[api_GetProcIdx(ProcHandle)].Resources);
    // Find Availability of resource
    for(i=0; i<ReqAvailCount; i++)
    {
        // Go read Alloc file and "noinit" instrument
        if((ResourceInfo->ResourceCount == 0) && Allocation)
        {
            // Parse into g_InstData array - Set Available to "noinit"
            Status = s_ParseAllocationFile(ProcHandle,Allocation, Availability, BufferSize);
            Status = 0;// Ignore errors for now
        }

        // Find Resource and return availability
		if((ResourcePtr = s_FindResourceByName(ReqAvail[i].ResourceName,ResourceInfo)))
        {
			InUse = false;
            // has Instrument been allocated
            if(ResourcePtr->InstPtr != NULL)
            {
                ResourcePtr->Validated = true;
                if(ResourcePtr->PhysicalPtr)
                {
                    AvailStr = ResourcePtr->PhysicalPtr->Availability;
					InUse = ResourcePtr->PhysicalPtr->InUse;
                }
                else if(ResourcePtr->VirtualPtr)
                {
                    AvailStr = ResourcePtr->VirtualPtr->Availability;
					// non-TPSs don't use virtual devices
                }
                else
                {
                    AvailStr = ResourcePtr->InstPtr->Availability;
					InUse = ResourcePtr->InstPtr->InUse;
                }
                strcpy(ReqAvail[i].Availability,AvailStr);
				if(!TpsType && InUse &&
				   ((strcmp(AvailStr,ATXML_AVAIL_AVAILABLE)==0) ||
				    (strcmp(AvailStr,ATXML_AVAIL_SIMULATED)==0)))
					strcat(ReqAvail[i].Availability,"InUse");

            }
        }
    }

    // This may cause some churning of the remove sequence for individual
    // validates, but try it for now.
    if(TpsType)
    {
        s_FillProgRemoveAllSequence(ProcHandle);
    }
    // Output Availability string for requested resources
    for(i=0; i<ReqAvailCount; i++)
    {
        bufidx = strlen(Availability);
        sprintf(TempBuf,
            "<AtXmlResponse>"
            "   <Availability>"
            "       <ResourceAvailability resourceName=\"%s\" availability=\"%s\" />"
            "   </Availability> "
            "</AtXmlResponse>",
                ReqAvail[i].ResourceName,ReqAvail[i].Availability);
        if((bufidx + (int)strlen(TempBuf)) < BufferSize)
        {
            sprintf(&Availability[bufidx], TempBuf);
        }

    }
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int smam_IssueSignal
//
// Purpose: Send an ATML IEEE 1641 (BSC) signal description to the wrapper
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
int smam_IssueSignal(int ProcHandle,
                     ATXML_ATML_Snippet *SignalDescription,
                     ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;
    INST_DATA *InstPtr;
    char *LclSignalDescription;
    int ResourceType=0;
    char ResourceName[ATXML_MAX_NAME];
    RESOURCE_INFO *ResourcePtr=NULL;
    char Action[ATXML_MAX_NAME], *cptr;
    int ResetFlag=0;
    bool SendOk;

    Action[0]='\0';
    if((LclSignalDescription = new char[(strlen(SignalDescription)+ATXML_MAX_NAME+20)])==NULL)
        return(Status);
    //CICOREDBGLOG(atxml_FmtMsg("smam_IssueSignal - ProcHandle %d\n%s",ProcHandle,SignalDescription));
    InstPtr = s_LocateInstrment(ProcHandle, SignalDescription, LclSignalDescription, 
                            &ResourceType, ResourceName, &ResourcePtr,
                            Response, BufferSize);
    CICOREDBGLOG(atxml_FmtMsg("smam_IssueSignal - ResourceType %d ResourceName [%s]",ResourceType, ResourceName));
    if((cptr = strstr(SignalDescription,"<SignalAction")))
    {
        sscanf(cptr,"<SignalAction > %[^< ]",Action);
        if((strcmp(Action,"Reset")==0))
            ResetFlag = 1;
        else if((strcmp(Action,"Status")==0))
            ResetFlag = -1; // Ignore 
    }
    if((InstPtr != NULL) && (s_TcStartIssueSignal(ProcHandle,ResourcePtr,ResetFlag,&SendOk)))
    {
        if(SendOk)
        {
            Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CIssueSignal(
                                  InstPtr->Instno, ResourceType, ResourceName,
                                  LclSignalDescription,
                                  Response, BufferSize);
        }
        s_TcStopIssueSignal(ProcHandle,ResourcePtr,ResetFlag);
    }
    else
    {
        atxml_ErrorResponse(Response, BufferSize, 
                           "IssueSignal", 
                           ATXML_ERRCD_RESOURCE_BUSY, ATXML_ERRMSG_RESOURCE_BUSY);
    }
    delete(LclSignalDescription);
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_RegisterRemoveSequence
//
// Purpose: Register the sequence for the station assets to be reset
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= =================  ===========================================
// RemoveSequence    ATXML_XML_String*  Remove sequence
// Output Parameters
// Parameter		 Type			    Purpose
// ================= =================  ===========================================
// Response          ATXML_XML_String*  Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int smam_RegisterRemoveSequence(int ProcHandle,
                                 ATXML_XML_String* RemoveSequence,
                                 ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;

    CICOREDBGLOG(atxml_FmtMsg("smam_RegisterRemoveSequence - ProcHandle %d\n%s",ProcHandle,RemoveSequence));
    // check if remove all sequence already programmed
    if(g_ProgrammableRemoveAll)
    {
        delete(g_ProgrammableRemoveAll);
        g_ProgrammableRemoveAll = NULL;
    }
    g_ProgrammableRemoveAll = new REMOVE_SEQUENCE;
    if(g_ProgrammableRemoveAll)
    {
        // Init System Remove All Sequence
        memset(g_ProgrammableRemoveAll, 0, sizeof(REMOVE_SEQUENCE));
        g_ProgrammableRemoveAll->ProcHandle = ProcHandle;
        if(strstr(RemoveSequence,"<RemoveDynamic"))
        {
            g_ProgrammableRemoveAll->Dynamic = true;
        }
        else
        {
            Status = s_ParseRemoveSequence(ProcHandle,RemoveSequence,Response,BufferSize);
        }

    }

    return(Status);
}
///////////////////////////////////////////////////////////////////////////////
// Function: int smam_InvokeRemoveAllSequence
//
// Purpose: Reset All the instruments according to the System or Programmable
//             RemoveAll Sequence.
//          Note: ProcHandle=0 is a master system reset and will reset all
//                Instruments.  The normal reset only resets those instruments
//                "Available/OutOfCal" and "InUse".
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
int smam_InvokeRemoveAllSequence(int ProcHandle,
                                 ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;
    bool MasterReset = false;

    CICOREDBGLOG(atxml_FmtMsg("smam_InvokeRemoveAll - ProcHandle %d ",ProcHandle));

    //Is there a programmable sequence available
    if(g_ProgrammableRemoveAll)
    {
        CICOREDBGLOG(atxml_FmtMsg("  Invoke Programmable Sequence:"));
        Status = s_ProcessRemoveAll(g_ProgrammableRemoveAll,MasterReset,Response,BufferSize);
    }


    // Is this a master reset
    if((ProcHandle == 0) || 
       (g_ProgrammableRemoveAll == NULL) ||
       (s_FirstTimeMasterReset))
    {
        MasterReset = (ProcHandle ? false : true);
        // If first time dynamic reset - Do a MasterReset
        if(s_FirstTimeMasterReset)
            MasterReset = true;
        s_FirstTimeMasterReset = false;

        CICOREDBGLOG(atxml_FmtMsg(atxml_FmtMsg("  Invoke System Sequence %d:",MasterReset)));
        Status = s_ProcessRemoveAll(&g_SystemRemoveAll, MasterReset,
                             Response, BufferSize);
    }

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC smam_RegisterApplySequence
//
// Purpose: Register the sequence for the station assets to be applied
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= =================  ===========================================
// ApplySequence    ATXML_XML_String*  Apply sequence
// Output Parameters
// Parameter		 Type			    Purpose
// ================= =================  ===========================================
// Response          ATXML_XML_String*  Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
int smam_RegisterApplySequence(int ProcHandle,
                                 ATXML_XML_String* ApplySequence,
                                 ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;

    CICOREDBGLOG(atxml_FmtMsg("smam_RegisterApplySequence - ProcHandle %d\n%s",ProcHandle,ApplySequence));
    // check if Apply all sequence already programmed
    if(g_ProgrammableApplyAll)
    {
        delete(g_ProgrammableApplyAll);
        g_ProgrammableApplyAll = NULL;
    }
    g_ProgrammableApplyAll = new APPLY_SEQUENCE;
    if(g_ProgrammableApplyAll)
    {
        // Init System Apply All Sequence
        memset(g_ProgrammableApplyAll, 0, sizeof(APPLY_SEQUENCE));
        g_ProgrammableApplyAll->ProcHandle = ProcHandle;
        if(strstr(ApplySequence,"<ApplyDynamic"))
        {
            g_ProgrammableApplyAll->Dynamic = true;
        }
        else
        {
            Status = s_ParseApplySequence(ProcHandle,ApplySequence,Response,BufferSize);
        }

    }

    return(Status);
}
///////////////////////////////////////////////////////////////////////////////
// Function: int smam_InvokeApplyAllSequence
//
// Purpose: Applies All the instruments according to the System or Programmable
//             ApplyAll Sequence.
//          Note: ProcHandle=0 is a master system reset and will reset all
//                Instruments.  The normal reset only resets those instruments
//                "Available/OutOfCal" and "InUse".
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
int smam_InvokeApplyAllSequence(int ProcHandle,
                                 ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;
    bool MasterReset = false;

    CICOREDBGLOG(atxml_FmtMsg("smam_InvokeApplyAll - ProcHandle %d ",ProcHandle));

    //Is there a programmable sequence available
    if(g_ProgrammableApplyAll)
    {
        CICOREDBGLOG(atxml_FmtMsg("  Invoke Programmable Sequence:"));
        Status = s_ProcessApplyAll(g_ProgrammableApplyAll,MasterReset,Response,BufferSize);
    }

    return(Status);
}



///////////////////////////////////////////////////////////////////////////////
// Function: int smam_TestStationStatus
//
// Purpose: Report the current status of the resources allocated to this procedre
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// TestRequirements ATXML_ATML_Snippet*  List of resources to status - NULL report all
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
int smam_TestStationStatus(int ProcHandle,
                     ATXML_ATML_Snippet *TestRequirements,
                     ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;
    char *cptr;
    int   i;
    SMAM_INST_STATUS ReqStatus[ATXML_MAX_RESOURCES];
    int   ReqStatusCount = 0;
	char  ResourceName[ATXML_MAX_NAME];
	bool  TooSmall;
    char  TempBuf[2048];
    PROC_RESOURCES *ResourceInfo;
	RESOURCE_INFO  *ResourcePtr;

	// If Response NULL whats the point?
	if((Response == NULL) || (BufferSize < 500))
		return(0);
	Response[0] = '\0';

    CICOREDBGLOG(atxml_FmtMsg("smam_TestStationStatus - ProcHandle %d\n",ProcHandle));
    // Point at this ResourceInfo Entry
    ResourceInfo = &(g_ProcInfo[api_GetProcIdx(ProcHandle)].Resources);
    // Parse Test Requirements to fill ReqStatus Table
    if(TestRequirements)
    {
		/*
				<AgAtXmlstRequirements>
					<ResourceRequirement>
						<ResourceType>Source</ResourceType>
						<SignalResourceName>DMM_1</SignalResourceName>
					</ResourceRequirement>
				</AgAtXmlstRequirements>
		*/
		// Find ResourceNames for requested availability
		if((cptr = strstr(TestRequirements,"<SignalResourceName>")) == NULL)
			atxml_ErrorResponse(Response, BufferSize, 
							   "TestStationStatus", 
							   ATXML_ERRCD_TEST_REQUIREMENTS, ATXML_ERRMSG_TEST_REQUIREMENTS);
		while(cptr)
		{
			if(sscanf(cptr,"<SignalResourceName> %[^< \t\n]",
						  ResourceName) >0)
			{
				ResourcePtr = s_FindResourceByName(ResourceName,ResourceInfo);
				if(ResourcePtr && ResourcePtr->Validated)
				{
					s_FillInstStatusInfo(ResourcePtr,&ReqStatus[ReqStatusCount++]);
				}
			}
			cptr = strstr(++cptr,"<SignalResourceName>");
		}
	}
	else
	{
		// Fill ReqStatus from validated resources
		for(i=0;i<ResourceInfo->ResourceCount;i++)
		{
			ResourcePtr = &(ResourceInfo->Resources[i]);
            if(ResourcePtr->Validated)
			    s_FillInstStatusInfo(ResourcePtr,&ReqStatus[ReqStatusCount++]);
		}
	}

	// Issue the response
	if(BufferSize < (500 + (ReqStatusCount * 300)))
	{
		atxml_ErrorResponse(Response, BufferSize, 
						   "TestStationStatus", 
						   ATXML_ERRCD_RESPONSE_BUFFER, ATXML_ERRMSG_RESPONSE_BUFFER);
		return(0);
	}
	// Issue the response
	/*
	<AtXmlResponse>
		<TestStationStatus>
			<ResourceStatus resourceName="DMM_1"
					availability="Available" 
					stationInstrumentName="" 
					stationResourceName="" 
					allocatedTo="" />
		</TestStationStatus>
	</AtXmlResponse>
	*/
	TooSmall = false;
	strcpy(Response,
		"<AtXmlResponse>\n"
        "  <TestStationStatus>\n");
	for(i=0; i<ReqStatusCount; i++)
	{
		TempBuf[0] = '\0';
		s_IssueResourceStatus(&ReqStatus[i],TempBuf,2048);
		if((BufferSize - strlen(Response)) < (strlen(TempBuf) + 200))
		{
			TooSmall = true;
			break;
		}
		strcat(Response,TempBuf);
	}
	strcat(Response,
        "  </TestStationStatus>\n"
		"</AtXmlResponse>\n");

    CICOREDBGLOG(atxml_FmtMsg("smam_TestStationStatus Return - ProcHandle %d\n%s",ProcHandle,Response));
	return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Instrument Specific (Cheat) Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: int smam_IssueIst
//
// Purpose: Call Ist interface to wrapper and return response data
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// InstSelfTest    ATXML_XML_String*    Issue Ist AtXml message
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
int smam_IssueIst(int ProcHandle,
                         ATXML_XML_String* InstSelfTest,
                         ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;
    INST_DATA *InstPtr;
    char *LclInstSelfTest;
    int ResourceType;
    char ResourceName[ATXML_MAX_NAME];
    RESOURCE_INFO *ResourcePtr;
    char *cptr;
    bool  SendOk;
    int Level = 1;

    if((LclInstSelfTest = new char[(strlen(InstSelfTest)+ATXML_MAX_NAME+20)])==NULL)
        return(Status);
    CICOREDBGLOG(atxml_FmtMsg("smam_IssueIst - ProcHandle %d\n%s",ProcHandle,InstSelfTest));
    InstPtr = s_LocateInstrment(ProcHandle, InstSelfTest, LclInstSelfTest,
                            &ResourceType, ResourceName, &ResourcePtr,
                            Response, BufferSize);
    CICOREDBGLOG(atxml_FmtMsg("smam_IssueIst - ResourceType %d ResourceName [%s]",ResourceType, ResourceName));
    // Get Level
    if((cptr = strstr(LclInstSelfTest,"Level")) && (cptr = strstr(cptr,">")))
    {
        Level = atoi(++cptr);
    }
    if((InstPtr != NULL) && (s_TcStartIst(ProcHandle,ResourcePtr,&SendOk)))
    {
        if(SendOk)
        {
            Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CIst(
                                InstPtr->Instno, ResourceType, ResourceName,
                                Level,
                                Response, BufferSize);
        }
        s_TcStopIst(ProcHandle,ResourcePtr);
    }
    else
    {
        atxml_ErrorResponse(Response, BufferSize, 
                           "IssueIst", 
                           ATXML_ERRCD_RESOURCE_BUSY, ATXML_ERRMSG_RESOURCE_BUSY);
    }
    delete(LclInstSelfTest);
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int smam_IssueNativeCmds
//
// Purpose: Send Native Instrument Commands to wrapper
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// InstrumentCmds    ATXML_ATML_Snippet* Native Instrument Commands + resource
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
int smam_IssueNativeCmds(int ProcHandle,
                         ATXML_ATML_Snippet* InstrumentCmds,
                         ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;
    INST_DATA *InstPtr;
    char *LclInstrumentCmds;
    int ResourceType;
    char ResourceName[ATXML_MAX_NAME];
    RESOURCE_INFO *ResourcePtr;
    int ResetFlag = 0;
    bool SendOk;

    if((LclInstrumentCmds = new char[(strlen(InstrumentCmds)+ATXML_MAX_NAME+20)])==NULL)
        return(Status);
    InstPtr = s_LocateInstrment(ProcHandle, InstrumentCmds, LclInstrumentCmds,
                            &ResourceType, ResourceName, &ResourcePtr,
                            Response, BufferSize);
    if((strstr(InstrumentCmds,"*RST")!=NULL) ||
                (strstr(InstrumentCmds,"RST ")!=NULL) ||
                (strstr(InstrumentCmds,"RST;")!=NULL))
                ResetFlag = 1;
    else if((strstr(InstrumentCmds,"?")!=NULL) ||
            (strstr(InstrumentCmds,"*CLS")!=NULL) ||
            (strstr(InstrumentCmds,"<Expected")!=NULL))
            ResetFlag = -1;

    if((InstPtr != NULL) && (s_TcStartIssueCmds(ProcHandle,ResourcePtr,ResetFlag,&SendOk)))
    {
        if(SendOk)
        {
            Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CIssueNativeCmds(
                                  InstPtr->Instno, ResourceType, ResourceName,
                                   LclInstrumentCmds,
                                  Response, BufferSize);
        }
        s_TcStopIssueCmds(ProcHandle,ResourcePtr,ResetFlag);
    }
    else
    {
        atxml_ErrorResponse(Response, BufferSize, 
                           "IssueNativeCmds", 
                           ATXML_ERRCD_RESOURCE_BUSY, ATXML_ERRMSG_RESOURCE_BUSY);
    }
    delete(LclInstrumentCmds);
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int smam_IssueDriverFunctionCall
//
// Purpose: Send Native Instrument Commands to wrapper
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// DriverFunction    ATXML_XML_String*   DriverFunction + resource
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
int smam_IssueDriverFunctionCall(int ProcHandle,
                                 ATXML_XML_String* DriverFunction,
                                 ATXML_XML_String* Response, int BufferSize)
{
    int Status = 0;
    INST_DATA *InstPtr;
    char *LclDriverFunction;
    int ResourceType;
    char ResourceName[ATXML_MAX_NAME];
    RESOURCE_INFO *ResourcePtr;
    char Function[ATXML_MAX_NAME], *cptr;
    int ResetFlag = 0;
    bool SendOk;

    if((LclDriverFunction = new char[(strlen(DriverFunction)+ATXML_MAX_NAME+20)])==NULL)
        return(Status);
    InstPtr = s_LocateInstrment(ProcHandle, DriverFunction, LclDriverFunction,
                            &ResourceType, ResourceName, &ResourcePtr,
                            Response, BufferSize);
    if((cptr = strstr(DriverFunction,"<FunctionName")))
    {
        sscanf(cptr,"<FunctionName > %[^< ]",Function);
        if(((strstr(Function,"_reset")!=NULL) ||
                    (strstr(Function,"ViReset ")!=NULL) ||
                    (strstr(Function,"_Reset")!=NULL)))
            ResetFlag = 1;
        else if(((strstr(Function,"_read")!=NULL) ||
                    (strstr(Function,"ViRead ")!=NULL) ||
                    (strstr(Function,"_Read")!=NULL) ||
                    (strstr(Function,"?")!=NULL)))
            ResetFlag = -1;
    }
    if((InstPtr != NULL) && (s_TcStartDriverFunction(ProcHandle,ResourcePtr,ResetFlag,&SendOk)))
    {
        if(SendOk)
        {
            Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CIssueDriverFunctionCall(
                                  InstPtr->Instno, ResourceType, ResourceName,
                                   LclDriverFunction,
                                  Response, BufferSize);
        }
        s_TcStopDriverFunction(ProcHandle,ResourcePtr,ResetFlag);
    }
    else
    {
        atxml_ErrorResponse(Response, BufferSize, 
                           "DriverFunction", 
                           ATXML_ERRCD_RESOURCE_BUSY, ATXML_ERRMSG_RESOURCE_BUSY);
    }
    delete(LclDriverFunction);
    return(Status);
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////
// Function: s_ParseTestStationCfgFile
//
// Purpose: Parse the TestStationInstance XML into the g_InstData g_WrapperClass.
//          Will look in local directory and \TETS\Config\
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
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
#define PHYSICAL_RESOURCE_ACTIVE 1
#define VIRTUAL_RESOURCE_ACTIVE  2
static int  s_ParseTestStationCfgFile(void)
{
    FILE *Fid;
    char  Line[512],*cptr;
    char  WrapperName[ATXML_MAX_NAME];
    char  ResourcesUsed[512];
    int   InstIdx=-1;
    int   Physical_Virtual = 0;

	// Find Root Directory Name
	s_GetConfigPath();

    if((Fid = fopen("TestStationInstance.xml", "rt")) == NULL)
    {
        if((Fid = fopen((atxml_FmtMsg("%s\\TestStationInstance.xml",s_ConfigPath)), "rt")) == NULL)
             return(-1);
    }
    // Read file and parse into g_InstData
/*
	<PublicAssets>
	<!-- Restrictions for DBG version Case sensitive and no blanks in names e.g. "<Asset" --> 
		<Asset>
			<InstrumentNumber>1</InstrumentNumber>
			<InstrumentName>DMM_1</InstrumentName>
			<InstrumentDriver>AtXmlDeviceXxx.dll</InstrumentDriver>
			<InstrumentAddress>
				<InstrumentQueryID>Ri4112</InstrumentQueryID>
				<InstrumentTypeNumber>1</InstrumentTypeNumber>
				<ControllerType>VXI</ControllerType>
				<ControllerNumber>0</ControllerNumber>
				<PrimaryAddress>52</PrimaryAddress>
				<SecondaryAddress></SecondaryAddress>
				<SubModuleAddress></SubModuleAddress>
			</InstrumentAddress>
			<SimulationLevel>0</SimulationLevel>
			<DebugLevel>10</DebugLevel>
			<PhysicalResources>
				<PhysicalResource>
					<ResourceID>1234567890</ResourceID>
					<ResourceName>DMM</ResourceName>
				</PhysicalResource>
			</PhysicalResources>
		</Asset>
		<Asset>
			<InstrumentNumber>5</InstrumentNumber>
			<InstrumentName>SWITCH</InstrumentName>
			<InstrumentDriver>AtXmlSwx_Ri1260.dll</InstrumentDriver>
			<PhysicalResources>
				<PhysicalResource>
					<ResourceID>1234567891</ResourceID>
					<ResourceName>Ri1260-16a_1</ResourceName>
						<InstrumentAddress>
							<InstrumentQueryID>Ri1260-16A</InstrumentQueryID>
							<InstrumentTypeNumber>1</InstrumentTypeNumber>
							<ControllerType>VXI</ControllerType>
							<ControllerNumber>0</ControllerNumber>
							<PrimaryAddress>7</PrimaryAddress>
						</InstrumentAddress>
				</PhysicalResource>
			</PhysicalResources>
			<VirtualResources>
				<VirtualResource>
					<ResourceID>1234567891</ResourceID>
					<ResourceName>PAWS_SWITCH</ResourceName>
					<PhysicalResourceUsed>Ri1260-16a_1</PhysicalResourceUsed>
				</VirtualResource>
			</VirtualResources>
		</Asset>
	</PublicAssets>
</TestStationInstance>
*/
    g_InstDataCount = 0;
    g_WrapperClassCount = 0;
    memset(g_WrapperClass,0,sizeof(g_WrapperClass));
    Physical_Virtual = 0;
    while(fgets(Line,256,Fid) != NULL)
    {
        if((cptr = strstr(Line,"<!--")) !=NULL)
            continue;
        if((cptr = strstr(Line,"<Asset"))!=NULL)
        {
            InstIdx++;
            g_InstDataCount++;
            memset(&g_InstData[InstIdx],0,sizeof(INST_DATA));
            strcpy(g_InstData[InstIdx].Availability,"noinit");
/*
            g_InstData[InstIdx].Instno = 0;
            g_InstData[InstIdx].InstrumentName[0] = '\0';
            g_InstData[InstIdx].InstrumentAddress.InstrumentQueryID[0] = '\0';
            g_InstData[InstIdx].InstrumentAddress.PrimaryAddress = 0;
            g_InstData[InstIdx].InstrumentAddress.SecondaryAddress = 0;
            g_InstData[InstIdx].InstrumentAddress.SubModuleAddress = 0;
            g_InstData[InstIdx].SimulationLevel = 0;
            g_InstData[InstIdx].DebugLevel = 0;
            g_InstData[InstIdx].PhysCount = 0;
            g_InstData[InstIdx].PhysicalResources = NULL;
            g_InstData[InstIdx].VirtCount = 0;
            g_InstData[InstIdx].VirtualResources = NULL;
            g_InstData[InstIdx].WrapperInfo = NULL;
            strcpy(g_InstData[InstIdx].Availability,"noinit");
            g_InstData[InstIdx].ClientCount = 0;
*/
        }
        // Don't start gathering info until first <Asset is found
        if(InstIdx < 0)
            continue;
        if((cptr = strstr(Line,"InstrumentNumber"))!=NULL)
        {
            sscanf(cptr,"InstrumentNumber > %d",&g_InstData[InstIdx].Instno);
        }
        if((cptr = strstr(Line,"InstrumentName"))!=NULL)
        {
            sscanf(cptr,"InstrumentName > %[^< ]",&g_InstData[InstIdx].InstrumentName[0]);
        }
        if((cptr = strstr(Line,"InstrumentDriver"))!=NULL)
        {
            if(sscanf(cptr,"InstrumentDriver > %[^< ]",WrapperName)>0)
                g_InstData[InstIdx].WrapperInfo = s_InsertWrapperClass(WrapperName);
        }
        if((cptr = strstr(Line,"<PhysicalResource>"))!=NULL)
        {
            Physical_Virtual = PHYSICAL_RESOURCE_ACTIVE;
            s_InsertPhysResource(&g_InstData[InstIdx]);
        }
        if((cptr = strstr(Line,"<VirtualResource>"))!=NULL)
        {
            Physical_Virtual = VIRTUAL_RESOURCE_ACTIVE;
            s_InsertVirtResource(&g_InstData[InstIdx]);
        }

        if((cptr = strstr(Line,"</PhysicalResource>"))!=NULL)
        {
            Physical_Virtual = 0;
        }
        if((cptr = strstr(Line,"</VirtualResource>"))!=NULL)
        {
            Physical_Virtual = 0;
        }
        switch(Physical_Virtual)
        {
        case PHYSICAL_RESOURCE_ACTIVE:
            //FIX Really crude assumes one per line!!!
            if((cptr = strstr(Line,"<ResourceName"))!=NULL)
            {
                sscanf(cptr,"<ResourceName > %[^< ]",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].ResourceName);
            }
            if((cptr = strstr(Line,"<ResourceAddress"))!=NULL)
            {
                sscanf(cptr,"<ResourceAddress > %[^< ]",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].InstrumentAddress.ResourceAddress);
            }
            // Instrument Address
            if((cptr = strstr(Line,"InstrumentQueryID"))!=NULL)
            {
                sscanf(cptr,"InstrumentQueryID > %[^< ]",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].InstrumentAddress.InstrumentQueryID[0]);
            }
            if((cptr = strstr(Line,"InstrumentTypeNumber"))!=NULL)
            {
                sscanf(cptr,"InstrumentTypeNumber > %d",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].InstrumentAddress.InstrumentTypeNumber);
            }
            if((cptr = strstr(Line,"ControllerType"))!=NULL)
            {
                sscanf(cptr,"ControllerType > %[^< ]",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].InstrumentAddress.ControllerType[0]);
            }
            if((cptr = strstr(Line,"ControllerNumber"))!=NULL)
            {
                sscanf(cptr,"ControllerNumber > %d",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].InstrumentAddress.ControllerNumber);
            }
            if((cptr = strstr(Line,"PrimaryAddress"))!=NULL)
            {
                sscanf(cptr,"PrimaryAddress > %d",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].InstrumentAddress.PrimaryAddress);
            }
            if((cptr = strstr(Line,"SecondaryAddress"))!=NULL)
            {
                sscanf(cptr,"SecondaryAddress > %d",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].InstrumentAddress.SecondaryAddress);
            }
            if((cptr = strstr(Line,"SubModuleAddress"))!=NULL)
            {
                sscanf(cptr,"SubModuleAddress > %d",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].InstrumentAddress.SubModuleAddress);
            }
            if((cptr = strstr(Line,"SimulationLevel"))!=NULL)
            {
                sscanf(cptr,"SimulationLevel > %d",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].SimulationLevel);
            }
            if((cptr = strstr(Line,"DebugLevel"))!=NULL)
            {
                sscanf(cptr,"DebugLevel > %d",
                    &g_InstData[InstIdx].PhysicalResources[g_InstData[InstIdx].PhysCount-1].DebugLevel);
            }
            break;
        case VIRTUAL_RESOURCE_ACTIVE:
            if((cptr = strstr(Line,"SimulationLevel"))!=NULL)
            {
                sscanf(cptr,"SimulationLevel > %d",
                    &g_InstData[InstIdx].VirtualResources[g_InstData[InstIdx].VirtCount-1].SimulationLevel);
            }
            if((cptr = strstr(Line,"DebugLevel"))!=NULL)
            {
                sscanf(cptr,"DebugLevel > %d",
                    &g_InstData[InstIdx].VirtualResources[g_InstData[InstIdx].VirtCount-1].DebugLevel);
            }
            if((cptr = strstr(Line,"<ResourceName"))!=NULL)
            {
                sscanf(cptr,"<ResourceName > %[^< ]",
                    &g_InstData[InstIdx].VirtualResources[g_InstData[InstIdx].VirtCount-1].ResourceName);
            }
            if((cptr = strstr(Line,"<PhysicalResourceUsed"))!=NULL)
            {
                ResourcesUsed[0] = '\0';
                sscanf(cptr,"<PhysicalResourceUsed > %[^<]", ResourcesUsed);
                g_InstData[InstIdx].VirtualResources[g_InstData[InstIdx].VirtCount-1].PhysicalResourcesUsed =
                                        new char[(strlen(ResourcesUsed)+8)];
                if(g_InstData[InstIdx].VirtualResources[g_InstData[InstIdx].VirtCount-1].PhysicalResourcesUsed)
                    strcpy(g_InstData[InstIdx].VirtualResources[g_InstData[InstIdx].VirtCount-1].PhysicalResourcesUsed,
                           ResourcesUsed);
                s_InitPhysResourcePtrs(&g_InstData[InstIdx],
                                       &g_InstData[InstIdx].VirtualResources[g_InstData[InstIdx].VirtCount-1],
                                       ResourcesUsed);
            }
            break;
        default:
            if((cptr = strstr(Line,"InstrumentQueryID"))!=NULL)
            {
                sscanf(cptr,"InstrumentQueryID > %[^< ]",&g_InstData[InstIdx].InstrumentAddress.InstrumentQueryID[0]);
            }
            if((cptr = strstr(Line,"InstrumentTypeNumber"))!=NULL)
            {
                sscanf(cptr,"InstrumentTypeNumber > %d",
                    &g_InstData[InstIdx].InstrumentAddress.InstrumentTypeNumber);
            }
            if((cptr = strstr(Line,"ControllerType"))!=NULL)
            {
                sscanf(cptr,"ControllerType > %[^< ]",
                    &g_InstData[InstIdx].InstrumentAddress.ControllerType[0]);
            }
            if((cptr = strstr(Line,"ControllerNumber"))!=NULL)
            {
                sscanf(cptr,"ControllerNumber > %d",
                    &g_InstData[InstIdx].InstrumentAddress.ControllerNumber);
            }
            if((cptr = strstr(Line,"PrimaryAddress"))!=NULL)
            {
                sscanf(cptr,"PrimaryAddress > %d",&g_InstData[InstIdx].InstrumentAddress.PrimaryAddress);
            }
            if((cptr = strstr(Line,"SecondaryAddress"))!=NULL)
            {
                sscanf(cptr,"SecondaryAddress > %d",&g_InstData[InstIdx].InstrumentAddress.SecondaryAddress);
            }
            if((cptr = strstr(Line,"SubModuleAddress"))!=NULL)
            {
                sscanf(cptr,"SubModuleAddress > %d",&g_InstData[InstIdx].InstrumentAddress.SubModuleAddress);
            }
            if((cptr = strstr(Line,"SimulationLevel"))!=NULL)
            {
                sscanf(cptr,"SimulationLevel > %d",&g_InstData[InstIdx].SimulationLevel);
            }
            if((cptr = strstr(Line,"DebugLevel"))!=NULL)
            {
                sscanf(cptr,"DebugLevel > %d",&g_InstData[InstIdx].DebugLevel);
            }
            break;
        }
    }
    fclose(Fid);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_ParseAllocationFile
//
// Purpose: Parse the Allocation XML into the ResourceInfo struct.
//          Will look in local directory and \TETS\Config\
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// Allocation        ATXML_XML_Filename*  Filename of Allocation File
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
static int  s_ParseAllocationFile(int ProcHandle,
                                  ATXML_XML_Filename *Allocation,
                                  ATXML_XML_String *Response, int BufferSize)
{
    FILE *Fid;
    char  Line[256],*cptr;
    char  ResourceName[ATXML_MAX_NAME];
    char  DeviceName[ATXML_MAX_NAME];
    char  StationResourceName[ATXML_MAX_NAME];

    // Check for existance of Allocation File Option
    if((Allocation == NULL) || (*Allocation == '\0'))
        return(0);

    // Open the file
    if((Fid = fopen(Allocation, "rt")) == NULL)
    {
        if((Fid = fopen(atxml_FmtMsg("%s\\%s", s_ConfigPath, Allocation), "rt")) == NULL)
        {
             atxml_ErrorResponse(Response, BufferSize, 
                           "Parsing Allocation File", ATXML_ERRCD_FILE_NOT_FOUND, 
                           atxml_FmtMsg("%s - %s",ATXML_ERRMSG_FILE_NOT_FOUND,Allocation));
             return(ATXML_ERRCD_FILE_NOT_FOUND);
        }
    }
    // Read file and parse into ResourceInfo

/*
	<xs:complexType name="AtXmlAllocationType">
		<xs:attribute name="signalID" type="xs:string" default=""/>
		<xs:attribute name="signalName" type="xs:string" default=""/>
		<xs:attribute name="resourceID" type="xs:string" default=""/>
		<xs:attribute name="resourceName" type="xs:string" default=""/>
		<xs:attribute name="stationInstrumentName" type="xs:string" use="required"/>
		<xs:attribute name="stationResourceName" type="xs:string" use="required"/>
	</xs:complexType>
	<AtXmlAllocation>
			<AllcationAssignment resourceName="DCPS_100V3A_L3S4"
                      stationInstrumentName="DCPS" stationResourceName="DCPS_100V3A_L3S4"/>
	</AtXmlAllocation>

*/
    ResourceName[0] = '\0';
    DeviceName[0] = '\0';
    StationResourceName[0] = '\0';
    while(fgets(Line,256,Fid) != NULL)
    {
        if((cptr = strstr(Line,"resourceName"))!=NULL)
        {
            sscanf(cptr,"resourceName = \" %55[^\"]",&ResourceName[0]);
        }
        if((cptr = strstr(Line,"stationInstrumentName"))!=NULL)
        {
            sscanf(cptr,"stationInstrumentName = \" %55[^\"]",&DeviceName[0]);
        }
        if((cptr = strstr(Line,"stationResourceName"))!=NULL)
        {
            sscanf(cptr,"stationResourceName = \" %55[^\"]",&StationResourceName[0]);
            // Try to allocate the Resource to a device
            s_Allocate(ProcHandle,ResourceName,DeviceName,StationResourceName,Response,BufferSize);
            ResourceName[0] = '\0';
            DeviceName[0] = '\0';
            StationResourceName[0] = '\0';
        }
    }
    fclose(Fid);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_GetConfigPath
//
// Purpose: Parse the VIPERT.INI file for a config file root path definition
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//      none
//
///////////////////////////////////////////////////////////////////////////////
static void s_GetConfigPath(void)
{
	DWORD dwType = REG_SZ;
	HKEY hKey = 0;
	char path[1024];
	DWORD value_length = 1024;
	const char* subkey = "SOFTWARE\\ATS";
	RegOpenKey(HKEY_LOCAL_MACHINE,subkey,&hKey);
	if(RegQueryValueEx(hKey, "IniFilePath", NULL, &dwType, (LPBYTE)&path, &value_length) != ERROR_SUCCESS)
	{
		strcpy(s_ConfigPath, "");
		return;
	}

	// Get string from SAVEO.ini file
    GetPrivateProfileString(
		     "File Locations",
		     "ISS_PATH",
			 "\\ATS\\Config",
			 s_ConfigPath, 256,
			 path);
	if(strcmp(s_ConfigPath,"\\ATS\\Config"))
		strcat(s_ConfigPath,"\\ISS\\Config");
}
///////////////////////////////////////////////////////////////////////////////
// Function: s_Allocate
//
// Purpose: Search the ResourceInfo table for an allocation match
//          and add if not already there
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// ResourceName      char*               Name of Resource
// DeviceName        char*               Station Asset name to be assigned
// StationResourceName char*             Station Asset resource name to be assigned
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
static int  s_Allocate(int ProcHandle,
                       char *ResourceName, char *DeviceName, char *StationResourceName,
                       ATXML_XML_String *Response, int BufferSize)
{
    int   InstIdx,ResIdx;
    bool  Found = false;
    RESOURCE_INFO *ResourcePtr = NULL;
    PROC_RESOURCES *ResourceInfo;
    char ErrMsg[1024];

    if((ResourceName == NULL) || (DeviceName == NULL) || (StationResourceName == NULL))
    {
        //FIX send error later
        return(-1);
    }
    // Point at this ResourceInfo Entry
    ResourceInfo = &(g_ProcInfo[api_GetProcIdx(ProcHandle)].Resources);

	ResourcePtr = s_FindResourceByName(ResourceName,ResourceInfo);
    // If not found Create
    if((ResourcePtr == NULL) && (ResourceInfo->ResourceCount < ATXML_MAX_RESOURCES))
    {
        ResourcePtr = &(ResourceInfo->Resources[ResourceInfo->ResourceCount]);
        strnzcpy(ResourcePtr->ResourceName, ResourceName, ATXML_MAX_NAME);
        ResourcePtr->ResourceId[0] = '\0';
        ResourcePtr->SignalName[0] = '\0';
        ResourcePtr->SignalId[0] = '\0';
        ResourcePtr->StationResourceName[0] = '\0';
        ResourcePtr->Validated = false;
        ResourcePtr->Instno = 0;
        ResourcePtr->InstPtr = NULL;
        ResourcePtr->PhysicalPtr = NULL;
        ResourcePtr->VirtualPtr = NULL;
        (ResourceInfo->ResourceCount)++;
    }
    if(ResourcePtr == NULL)
    {
        atxml_ErrorResponse(Response, BufferSize, 
                           "Allocate", ATXML_ERRCD_TOO_MANY_RESOURCES, ATXML_ERRMSG_TOO_MANY_RESOURCES);
        return(ATXML_ERRCD_TOO_MANY_RESOURCES);
    }

    // Allocate to an instrument if possible
    for(InstIdx=0; InstIdx < g_InstDataCount; InstIdx++)
    {
        if(strncmp(g_InstData[InstIdx].InstrumentName,DeviceName,ATXML_MAX_NAME) == 0)
        {
            for(ResIdx=0; ResIdx < g_InstData[InstIdx].PhysCount; ResIdx++)
            {
                if(strncmp(g_InstData[InstIdx].PhysicalResources[ResIdx].ResourceName,
                                      StationResourceName, ATXML_MAX_NAME) == 0)
                {
                    Found = true;
                    ResourcePtr->PhysicalPtr = &g_InstData[InstIdx].PhysicalResources[ResIdx];
                    break;
                }
            }
            if(ResourcePtr->PhysicalPtr == NULL)
            {
                for(ResIdx=0; ResIdx < g_InstData[InstIdx].VirtCount; ResIdx++)
                {
                    if(strncmp(g_InstData[InstIdx].VirtualResources[ResIdx].ResourceName,
                                        StationResourceName, ATXML_MAX_NAME) == 0)
                    {
                        Found = true;
                        ResourcePtr->VirtualPtr = &g_InstData[InstIdx].VirtualResources[ResIdx];
                        break;
                    }
                }
            }
            if((ResourcePtr->PhysicalPtr != NULL) || (ResourcePtr->VirtualPtr != NULL) ||
               (StationResourceName[0] == '\0'))
            {
                Found = true;
                ResourcePtr->InstPtr = &g_InstData[InstIdx];
                ResourcePtr->Instno = g_InstData[InstIdx].Instno;
                if(StationResourceName[0] == '\0')
                {
                    strnzcpy(ResourcePtr->StationResourceName, DeviceName, ATXML_MAX_NAME);
                }
                else
                {
                    strnzcpy(ResourcePtr->StationResourceName, StationResourceName, ATXML_MAX_NAME);
                }
            }
        }
        if(Found)
            break;
    }
    if(!Found)
    {
        // Resource not found
        
        sprintf(ErrMsg,"%s-%s %s %s",ATXML_ERRMSG_RESOURCE_NOT_FOUND,
            ResourceName,DeviceName,StationResourceName);
        atxml_ErrorResponse(Response, BufferSize, 
                           "Allocate", ATXML_ERRCD_RESOURCE_NOT_FOUND, ErrMsg);
        return(ATXML_ERRCD_RESOURCE_NOT_FOUND);
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_InitInstrument
//
// Purpose: Insure wrapper loaded and Initialize the instrument and resource
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// InstPtr           INST_DATA*          Instrument Data Pointer
// ResourceName      char*               Resource being inited (ignored)
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
static int s_InitInstrument(INST_DATA *InstPtr, char *ResourceName,
                                  ATXML_XML_String *Response, int Buffersize)
{
    int   Status = 0;
    int   Idx;

    if((InstPtr == NULL) || (InstPtr->WrapperInfo == NULL))
    {
        atxml_ErrorResponse(Response, Buffersize,
                   "InitInstrument", ATXML_ERRCD_DEVICE_NOT_FOUND, ATXML_ERRMSG_DEVICE_NOT_FOUND);
        return(ATXML_ERRCD_DEVICE_NOT_FOUND);
    }
    // Instance the Class if necessary
    if((InstPtr->WrapperInfo != NULL) && (InstPtr->WrapperInfo->WrapperClass == NULL))
    {
        InstPtr->WrapperInfo->WrapperClass = new CWrapperInf(InstPtr->WrapperInfo->WrapperName);
    }
    // Initialize the instrument
    if((InstPtr->WrapperInfo != NULL) && (InstPtr->WrapperInfo->WrapperClass != NULL) &&
       (strcmp(InstPtr->Availability,"noinit")==0))
    {
        Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CInitialize(
                         InstPtr->Instno,
                         ATXMLW_RESTYPE_BASE,
                         InstPtr->InstrumentName,
                         InstPtr->SimulationLevel, InstPtr->DebugLevel,
                         &(InstPtr->InstrumentAddress),
                         Response, Buffersize);
        if(Status == 0)
        {
            if(InstPtr->SimulationLevel)
            {
                InstPtr->AvailInt = ATXML_AVAIL_SIMULATED_INT;
                strcpy(InstPtr->Availability, ATXML_AVAIL_SIMULATED);
            }
            else
            {
                InstPtr->AvailInt = ATXML_AVAIL_AVAILABLE_INT;
                strcpy(InstPtr->Availability, ATXML_AVAIL_AVAILABLE);
            }
        }
        else if(Status == ATXML_ERRCD_INIT_WRAPPER)
        {
                InstPtr->AvailInt = ATXML_AVAIL_NOT_FOUND_INT;
                strcpy(InstPtr->Availability, ATXML_AVAIL_NOT_FOUND);
        }
        else
        {
            InstPtr->AvailInt = ATXML_AVAIL_NO_RESPONSE_INT;
            strcpy(InstPtr->Availability, ATXML_AVAIL_NO_RESPONSE);
        }
        CICOREDBGLOG(atxml_FmtMsg("     %s Status %d %s",
                     InstPtr->InstrumentName,Status,InstPtr->Availability));
        atxml_Log(ATXML_LOG_EVENTS,atxml_FmtMsg("     %s Status %d %s",
                     InstPtr->InstrumentName,Status,InstPtr->Availability));
        /* Init all resources not just the instrument
        if(strcmp(ResourceName,InstPtr->InstrumentName) == 0)
            return(Status);
        */
    }
    // Is Resource being Inited a Physical Device
    for(Idx = 0; Idx<InstPtr->PhysCount; Idx++)
    {
        if((strcmp(InstPtr->PhysicalResources[Idx].Availability,"noinit")==0))
        {
            Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CInitialize(
                         InstPtr->Instno,
                         ATXMLW_RESTYPE_PHYSICAL,
                         InstPtr->PhysicalResources[Idx].ResourceName,
                         InstPtr->PhysicalResources[Idx].SimulationLevel,
                         InstPtr->PhysicalResources[Idx].DebugLevel,
                         &(InstPtr->PhysicalResources[Idx].InstrumentAddress),
                         Response, Buffersize);
            if(Status == 0)
            {
                if(InstPtr->PhysicalResources[Idx].SimulationLevel)
                {
                    InstPtr->PhysicalResources[Idx].AvailInt = ATXML_AVAIL_SIMULATED_INT;
                    strcpy(InstPtr->PhysicalResources[Idx].Availability, ATXML_AVAIL_SIMULATED);
                }
                else
                {
                    InstPtr->PhysicalResources[Idx].AvailInt = ATXML_AVAIL_AVAILABLE_INT;
                    strcpy(InstPtr->PhysicalResources[Idx].Availability, ATXML_AVAIL_AVAILABLE);
                }
            }
                else
            {
                InstPtr->PhysicalResources[Idx].AvailInt = ATXML_AVAIL_NO_RESPONSE_INT;
                strcpy(InstPtr->PhysicalResources[Idx].Availability, ATXML_AVAIL_NO_RESPONSE);
            }

            CICOREDBGLOG(atxml_FmtMsg("         %s Status %d %s",
                        InstPtr->PhysicalResources[Idx].ResourceName,Status,
                        InstPtr->PhysicalResources[Idx].Availability));
            atxml_Log(ATXML_LOG_EVENTS,atxml_FmtMsg("         %s Status %d %s",
                        InstPtr->PhysicalResources[Idx].ResourceName,Status,
                        InstPtr->PhysicalResources[Idx].Availability));
            //return(Status);
        }
    }
    // Is Resource being Inited a Virtual Device
    for(Idx = 0; Idx<InstPtr->VirtCount; Idx++)
    {
        if((strcmp(InstPtr->VirtualResources[Idx].Availability,"noinit")==0))
        {
            Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CInitialize(
                         InstPtr->Instno,
                         ATXMLW_RESTYPE_VIRTUAL,
                         InstPtr->VirtualResources[Idx].ResourceName,
                         InstPtr->VirtualResources[Idx].SimulationLevel,
                         InstPtr->VirtualResources[Idx].DebugLevel,
                         NULL,
                         Response, Buffersize);
            if(Status == 0)
            {
                if(InstPtr->VirtualResources[Idx].SimulationLevel)
                {
                    InstPtr->VirtualResources[Idx].AvailInt = ATXML_AVAIL_SIMULATED_INT;
                    strcpy(InstPtr->VirtualResources[Idx].Availability, ATXML_AVAIL_SIMULATED);
                }
                else
                {
                    InstPtr->VirtualResources[Idx].AvailInt = ATXML_AVAIL_AVAILABLE_INT;
                    strcpy(InstPtr->VirtualResources[Idx].Availability, ATXML_AVAIL_AVAILABLE);
                }
            }
                else
            {
                InstPtr->VirtualResources[Idx].AvailInt = ATXML_AVAIL_NO_RESPONSE_INT;
                strcpy(InstPtr->VirtualResources[Idx].Availability, ATXML_AVAIL_NO_RESPONSE);
            }

            CICOREDBGLOG(atxml_FmtMsg("         %s Status %d %s",
                        InstPtr->VirtualResources[Idx].ResourceName,Status,
                        InstPtr->VirtualResources[Idx].Availability));
            atxml_Log(ATXML_LOG_EVENTS,atxml_FmtMsg("         %s Status %d %s",
                        InstPtr->VirtualResources[Idx].ResourceName,Status,
                        InstPtr->VirtualResources[Idx].Availability));
            //return(Status);
        }
    }
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_InsertWrapperClass
//
// Purpose: Insert the warpper into the g_WrapperClass array and instiantiate the
//          class if not already instiated
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// WrapperName       char*               Name of Wrapper.dll file
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    Pointer to g_WrapperClass entry on success.
//    NULL on failure
//
///////////////////////////////////////////////////////////////////////////////
static WRAPPER_CLASS *s_InsertWrapperClass(char *WrapperName)
{
    WRAPPER_CLASS *WrapperPtr = NULL;
    int  WrapIdx;

    for(WrapIdx=0; WrapIdx < g_WrapperClassCount; WrapIdx++)
    {
        if(strcmp(WrapperName, g_WrapperClass[WrapIdx].WrapperName) == 0)
        {
            WrapperPtr = &g_WrapperClass[WrapIdx];
            break;
        }
    }
    // Not there - Add it
    if((WrapperPtr == NULL)  && (g_WrapperClassCount < ATXML_MAX_WRAPPERS))
    {
        // Add it to the list and instance the Class
        WrapperPtr = &g_WrapperClass[g_WrapperClassCount];
        strnzcpy(g_WrapperClass[g_WrapperClassCount].WrapperName,WrapperName, ATXML_MAX_NAME);
        g_WrapperClassCount++;
    }

    return(WrapperPtr);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_LocateInstrment
//
// Purpose: Parse the signal description to get a resourceName and locate instrument
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
//    INST_DATA Pointer on success.
//    NULL on failure
//
///////////////////////////////////////////////////////////////////////////////
static INST_DATA *s_LocateInstrment(int ProcHandle, 
                                    ATXML_ATML_Snippet *SignalDescription,
                                    ATXML_ATML_Snippet *WrapSignalDescription,
                                    int *ResourceType, char *ResourceName,
                                    RESOURCE_INFO **ResourcePtr,
                                    ATXML_XML_String *Response, int BufferSize)
{
    int   i;
    char *cptr;
    INST_DATA *InstPtr = NULL;
    PROC_RESOURCES *ResourceInfo;

    //FIX - for now just look for a Resource Name
    if((cptr = strstr(SignalDescription,"<SignalResourceName>")) == NULL)
    {
        return(NULL);
    }

    // Point at this ResourceInfo Entry
    ResourceInfo = &(g_ProcInfo[api_GetProcIdx(ProcHandle)].Resources);

    // Find ResourceName in resource table
    ResourceName[0] = '\0';
    sscanf(cptr,"<SignalResourceName> %50[^< \n]",&ResourceName[0]);
    for(i=0; i<ResourceInfo->ResourceCount; i++)
    {
        if(strncmp(ResourceInfo->Resources[i].ResourceName, ResourceName, ATXML_MAX_NAME) == 0)
        {
            InstPtr = ResourceInfo->Resources[i].InstPtr;
            *ResourcePtr = &(ResourceInfo->Resources[i]);
            strcpy(ResourceName,ResourceInfo->Resources[i].StationResourceName);

            *ResourceType = ATXMLW_RESTYPE_BASE;
            if(ResourceInfo->Resources[i].PhysicalPtr)
                *ResourceType = ATXMLW_RESTYPE_PHYSICAL;
            if(ResourceInfo->Resources[i].VirtualPtr)
                *ResourceType = ATXMLW_RESTYPE_VIRTUAL;
            break;
        }

    }
    // Copy signal description to WrapSignalDescription
    cptr = strstr(cptr,">");
    *cptr = '\0';
    strcpy(WrapSignalDescription,SignalDescription);
    *cptr = '>';
    strcat(WrapSignalDescription,">");
    strcat(WrapSignalDescription,ResourceName);
    cptr = strstr(cptr,"<");
    strcat(WrapSignalDescription,cptr);

    return(InstPtr);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_InsertPhysResource
//
// Purpose: Parse the signal description to get a resourceName and locate instrument
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// ResourceName      char*               Starion Resource Name
// ResourceAddress   char*               Starion Resource sub Address
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    None
//
///////////////////////////////////////////////////////////////////////////////
static void s_InsertPhysResource(INST_DATA *InstPtr)
{
    if(InstPtr == NULL)
        return;
    if(InstPtr->PhysicalResources == NULL)
    {
        // Create Physical Resource buffer
        //FIX for now just do 101
        InstPtr->PhysicalResources = new PHYSICAL_RESOURCE[101];
        InstPtr->PhysCount = 0;
        memset(InstPtr->PhysicalResources,0,(sizeof(PHYSICAL_RESOURCE) * 100));
    }
    InstPtr->PhysicalResources[InstPtr->PhysCount].SimulationLevel = 
        InstPtr->SimulationLevel;
    InstPtr->PhysicalResources[InstPtr->PhysCount].DebugLevel = 
        InstPtr->DebugLevel;
    InstPtr->PhysicalResources[InstPtr->PhysCount].AvailInt = ATXML_AVAIL_NO_INIT;
    strcpy(InstPtr->PhysicalResources[InstPtr->PhysCount].Availability,"noinit");
    InstPtr->PhysCount++;

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: int s_InsertVirtResource
//
// Purpose: Parse the signal description to get a resourceName and locate instrument
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// ResourceName      char*               Starion Resource Name
// ResourcesUsed     char*               Starion Resources used
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    None
//
///////////////////////////////////////////////////////////////////////////////
static void s_InsertVirtResource(INST_DATA *InstPtr)
{
    if(InstPtr == NULL)
        return;
    if(InstPtr->VirtualResources == NULL)
    {
        // Create Virtual Resource buffer
        //FIX for now just do 50
        InstPtr->VirtualResources = new VIRTUAL_RESOURCE[101];
        InstPtr->VirtCount = 0;
        memset(InstPtr->VirtualResources,0,(sizeof(VIRTUAL_RESOURCE) * 100));
    }
    InstPtr->VirtualResources[InstPtr->VirtCount].SimulationLevel = 
        InstPtr->SimulationLevel;
    InstPtr->VirtualResources[InstPtr->VirtCount].DebugLevel = 
        InstPtr->DebugLevel;
    InstPtr->VirtualResources[InstPtr->VirtCount].AvailInt = ATXML_AVAIL_NO_INIT;
    strcpy(InstPtr->VirtualResources[InstPtr->VirtCount].Availability,"noinit");
    InstPtr->VirtCount++;

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_InitPhysResourcePtrs
//
// Purpose: Initialize the Vitual Resource pointers to the physical resources used.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// InstPtr           INST_DATA*          Pointer to Instrument data being inited
// VirPtr            VIRTUAL_RESOURCE*   Pointer to Virtual Resource data to be inited
// ResourcesUsed     char*               List of physical resources used
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    None
//
///////////////////////////////////////////////////////////////////////////////
static void s_InitPhysResourcePtrs(INST_DATA *InstPtr,
                                   VIRTUAL_RESOURCE * VirtPtr,
                                   char *ResourcesUsed)
{
    int   PhysUsedCount = 0;
    char *cptr;
    int   PhysCount;
    int   i, UsedIdx;
    PHYSICAL_RESOURCE *PhysPtr;
    char  PhysResUsed[ATXML_MAX_NAME];

    if((InstPtr==NULL) || (VirtPtr==NULL) ||
       (ResourcesUsed==NULL) || (*ResourcesUsed=='\0'))
        return;
    PhysCount = InstPtr->PhysCount;
    if((PhysCount == 0) || ((PhysPtr = InstPtr->PhysicalResources) == NULL))
        return;
    cptr = ResourcesUsed;
    // Get the number of resoures
    while(cptr && *cptr)
    {
        PhysUsedCount++;
        while(*cptr && (!isspace(*cptr)))
            cptr++;
        while(*cptr && (isspace(*cptr)))
            cptr++;
    }
    if(PhysUsedCount == 0)
        return;
    // create the space for the pointers
    VirtPtr->PhysResources = new PHYSICAL_RESOURCE*[PhysUsedCount+1];
    if(VirtPtr->PhysResources == NULL)
        return;
    memset(VirtPtr->PhysResources,0,(sizeof(PHYSICAL_RESOURCE*) * PhysUsedCount));
    cptr = ResourcesUsed;
    UsedIdx = 0;
    while(cptr && *cptr)
    {
        if(sscanf(cptr,"%s",PhysResUsed) !=1)
            break;
        PhysPtr = InstPtr->PhysicalResources;
        for(i=0; i<PhysCount; i++)
        {
            if(strcmp(PhysPtr->ResourceName,PhysResUsed) == 0)
            {
                VirtPtr->PhysResources[UsedIdx++] = PhysPtr;
                break;
            }
            PhysPtr++;
        }
        while(*cptr && (!isspace(*cptr)))
            cptr++;
        while(*cptr && (isspace(*cptr)))
            cptr++;
    }
    VirtPtr->PhysicalResourceCount = PhysUsedCount;
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_InitAllInstruments
//
// Purpose: Initialize the instruments specified in the test station instance.
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
//    None
//
///////////////////////////////////////////////////////////////////////////////
static void s_InitAllInstruments(void)
{
    int InstIdx;
    int Status;

    CICOREDBGLOG("Begin Instrument Initialization");
    atxml_Log(ATXML_LOG_EVENTS,"Begin Instrument Initialization");
    for(InstIdx=0;InstIdx<g_InstDataCount;InstIdx++)
    {
        //Check Sim > 1 don't load wrapper
        if(g_InstData[InstIdx].SimulationLevel > 1)
        {
            strcpy(g_InstData[InstIdx].Availability,ATXML_AVAIL_SIMULATED);
            g_InstData[InstIdx].AvailInt = ATXML_AVAIL_SIMULATED2_INT;
            CICOREDBGLOG(atxml_FmtMsg("     %s Sim2 NoWrapper %s",
                        g_InstData[InstIdx].InstrumentName,
                        g_InstData[InstIdx].Availability));
            atxml_Log(ATXML_LOG_EVENTS,atxml_FmtMsg("     %s Sim2 NoWrapper %s",
                        g_InstData[InstIdx].InstrumentName,
                        g_InstData[InstIdx].Availability));
            continue;
        }
        // Init the base instrument
        Status = s_InitInstrument(&g_InstData[InstIdx], g_InstData[InstIdx].InstrumentName,
                                  NULL, 0);
    }
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_FillSystemRemoveAllSequence
//
// Purpose: Initialize the default RemoveAll sequence. The sequence is the reverse
//              order in the TestStationInstance of the Instruments and the physical
//              devices in assending order.
//FIX  later possibly use Source/Power/Digital/Buses/I-O/Sensors/Switch
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		Type			     Purpose
// ===============  ===================  ===========================================
//
// Return:
//    None
//
///////////////////////////////////////////////////////////////////////////////
static void  s_FillSystemRemoveAllSequence(void)
{
    int  InstIdx,PhysIdx,SeqIdx;
    bool PhysicalReset;

    g_SystemRemoveAll.Dynamic = false;
    g_SystemRemoveAll.ProcHandle = 0;
    g_SystemRemoveAll.RemSeqCount = 0; // set while programming
    SeqIdx = 0;
	// Program default reset sequence for Forward TestStationInstance Sequence
    for(InstIdx = 0; InstIdx<g_InstDataCount; InstIdx++)
    {
        PhysicalReset = false;
        // Physical Instruments
        for(PhysIdx=0;PhysIdx<g_InstData[InstIdx].PhysCount;PhysIdx++)
        {
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].Instno =
                    g_InstData[InstIdx].Instno;
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].InstPtr =
                    &g_InstData[InstIdx];
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].PhysicalPtr =
                    &(g_InstData[InstIdx].PhysicalResources[PhysIdx]);
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].SequenceNumber = SeqIdx;
            strcpy(g_SystemRemoveAll.RemSeqEntries[SeqIdx].StationResourceName,
                    g_InstData[InstIdx].PhysicalResources[PhysIdx].ResourceName);
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].VirtualPtr = NULL;
            SeqIdx++;
            PhysicalReset = true;
        }
        if(!PhysicalReset)
        {
            // Base Instrument
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].Instno =
                    g_InstData[InstIdx].Instno;
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].InstPtr =
                    &g_InstData[InstIdx];
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].PhysicalPtr = NULL;
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].SequenceNumber = SeqIdx;
            strcpy(g_SystemRemoveAll.RemSeqEntries[SeqIdx].StationResourceName,
                    g_InstData[InstIdx].InstrumentName);
            g_SystemRemoveAll.RemSeqEntries[SeqIdx].VirtualPtr = NULL;
            SeqIdx++;
        }

    }
    g_SystemRemoveAll.RemSeqCount = SeqIdx;
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_FillProgRemoveAllSequence
//
// Purpose: Initialize the programmable RemoveAll sequence with Resources. 
//              The sequence is the reverse order in the Allocation of the 
//              Resources (Including Virtual Instruments).
//FIX  later possibly use Source/Power/Digital/Buses/I-O/Sensors/Switch
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		Type			     Purpose
// ===============  ===================  ===========================================
//
// Return:
//    None
//
///////////////////////////////////////////////////////////////////////////////
static void  s_FillProgRemoveAllSequence(int ProcHandle)
{
#ifndef DYNAMIC_REMOVE_ALL
    int  i;
    PROC_RESOURCES *ResourceInfo;
    RESOURCE_INFO *ResourcePtr;
#endif
    int SeqIdx=0;

    // check if remove all sequence already programmed
    if(g_ProgrammableRemoveAll)
    {
        delete(g_ProgrammableRemoveAll);
        g_ProgrammableRemoveAll = NULL;
    }
    g_ProgrammableRemoveAll = new REMOVE_SEQUENCE;
    if(g_ProgrammableRemoveAll == NULL)
        return;
    
    // Init System Remove All Sequence
    memset(g_ProgrammableRemoveAll, 0, sizeof(REMOVE_SEQUENCE));
    g_ProgrammableRemoveAll->ProcHandle = ProcHandle;
    g_ProgrammableRemoveAll->RemSeqCount = 0; // set while programming
#ifdef DYNAMIC_REMOVE_ALL
    g_ProgrammableRemoveAll->Dynamic = true;
    s_FirstTimeMasterReset = true;
#else
    g_ProgrammableRemoveAll->Dynamic = false;
    // Point at this ResourceInfo Entry
    ResourceInfo = &(g_ProcInfo[api_GetProcIdx(ProcHandle)].Resources);
    // For Now use Forward Allocation Order and only instruments allocated
    for(i=0; i<ResourceInfo->ResourceCount; i++)
    {
        ResourcePtr = &(ResourceInfo->Resources[i]);
        if(!ResourcePtr->Validated)
            continue;
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].Instno =
                ResourcePtr->InstPtr->Instno;
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].InstPtr =
                ResourcePtr->InstPtr;
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].PhysicalPtr = 
                ResourcePtr->PhysicalPtr;
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].SequenceNumber = SeqIdx;
        strcpy(g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].StationResourceName,
                ResourcePtr->StationResourceName);
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].VirtualPtr = 
                ResourcePtr->VirtualPtr;
        SeqIdx++;
    }
    g_ProgrammableRemoveAll->RemSeqCount = SeqIdx;
#endif
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_ParseRemoveSequence
//
// Purpose: Initialize the programmable RemoveAll sequence with provided.
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		Type			     Purpose
// ===============  ===================  ===========================================
//
// Return:
//    None
//
///////////////////////////////////////////////////////////////////////////////
static int s_ParseRemoveSequence(int ProcHandle, ATXML_XML_String *RemoveSequence,
                           ATXML_XML_String *Response, int BufferSize)
{
    int   SeqNo;
    int   SeqCount = 0;
    char *cptr,*cptrseqno,*cptrresname;
    char  ResourceName[ATXML_MAX_NAME];
    PROC_RESOURCES *Resources;
    RESOURCE_INFO  *ResourcePtr;
/*
	<AtXmlRemoveSequence>
		<RemoveAssignment sequenceNumber="0" resourceName="DMM_1"/>
		<RemoveAssignment sequenceNumber="1" resourceName="DCPS_1"/>
	</AtXmlRemoveSequence>
*/
    if((RemoveSequence == NULL) || (g_ProgrammableRemoveAll == NULL))
        return(0);
    cptr = RemoveSequence;
    while(cptr && (cptr = strstr(cptr,"<RemoveAssignment")))
    {
        // both attributes present?
        if(!((cptrseqno = strstr(cptr,"sequenceNumber")) &&
           (cptrresname = strstr(cptr,"resourceName"))))
           break;//OOPs something wrong
        sscanf(cptrseqno,"sequenceNumber = \" %d",&SeqNo);
        sscanf(cptrresname,"resourceName = \" %50[^\"]",ResourceName);
        cptr++;
        if(SeqNo >= ATXML_MAX_RESOURCES)
        {
            atxml_ErrorResponse(Response, BufferSize, 
                           "ParseRemoveAllSequence", 
                           ATXML_ERRCD_TOO_MANY_RESOURCES, 
                           atxml_FmtMsg("%s - SeqNo %d",ATXML_ERRMSG_TOO_MANY_RESOURCES,SeqNo));
            continue;
        }
        Resources = &(g_ProcInfo[api_GetProcIdx(ProcHandle)].Resources);
		ResourcePtr = s_FindResourceByName(ResourceName,Resources);
        if(ResourcePtr &&
           (strcmp(ResourcePtr->ResourceName,ResourceName)==0))
        {
            g_ProgrammableRemoveAll->RemSeqEntries[SeqNo].Instno =
                    ResourcePtr->Instno;
            g_ProgrammableRemoveAll->RemSeqEntries[SeqNo].InstPtr =
                    ResourcePtr->InstPtr;
            g_ProgrammableRemoveAll->RemSeqEntries[SeqNo].PhysicalPtr = 
                    ResourcePtr->PhysicalPtr;
            g_ProgrammableRemoveAll->RemSeqEntries[SeqNo].SequenceNumber = SeqNo;
            strcpy(g_ProgrammableRemoveAll->RemSeqEntries[SeqNo].StationResourceName,
                    ResourcePtr->StationResourceName);
            g_ProgrammableRemoveAll->RemSeqEntries[SeqNo].VirtualPtr = 
                    ResourcePtr->VirtualPtr;
            if((SeqNo + 1) > SeqCount)
                SeqCount = (SeqNo + 1); 
        }
    }
    g_ProgrammableRemoveAll->RemSeqCount = SeqCount;
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_ParseApplySequence
//
// Purpose: Initialize the programmable ApplyAll sequence with provided.
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		Type			     Purpose
// ===============  ===================  ===========================================
//
// Return:
//    None
//
///////////////////////////////////////////////////////////////////////////////
static int s_ParseApplySequence(int ProcHandle, ATXML_XML_String *ApplySequence,
                           ATXML_XML_String *Response, int BufferSize)
{
    int   SeqNo;
    int   SeqCount = 0;
    char *cptr,*cptrseqno,*cptrresname;
    char  ResourceName[ATXML_MAX_NAME];
    PROC_RESOURCES *Resources;
    RESOURCE_INFO  *ResourcePtr;
/*
	<AtXmlApplySequence>
		<ApplyAssignment sequenceNumber="0" resourceName="DMM_1"/>
		<ApplyAssignment sequenceNumber="1" resourceName="DCPS_1"/>
	</AtXmlApplySequence>
*/
    if((ApplySequence == NULL) || (g_ProgrammableApplyAll == NULL))
        return(0);
    cptr = ApplySequence;
    while(cptr && (cptr = strstr(cptr,"<ApplyAssignment")))
    {
        // both attributes present?
        if(!((cptrseqno = strstr(cptr,"sequenceNumber")) &&
           (cptrresname = strstr(cptr,"resourceName"))))
           break;//OOPs something wrong
        sscanf(cptrseqno,"sequenceNumber = \" %d",&SeqNo);
        sscanf(cptrresname,"resourceName = \" %50[^\"]",ResourceName);
        cptr++;
        if(SeqNo >= ATXML_MAX_RESOURCES)
        {
            atxml_ErrorResponse(Response, BufferSize, 
                           "ParseApplyAllSequence", 
                           ATXML_ERRCD_TOO_MANY_RESOURCES, 
                           atxml_FmtMsg("%s - SeqNo %d",ATXML_ERRMSG_TOO_MANY_RESOURCES,SeqNo));
            continue;
        }
        Resources = &(g_ProcInfo[api_GetProcIdx(ProcHandle)].Resources);
		ResourcePtr = s_FindResourceByName(ResourceName,Resources);
        if(ResourcePtr &&
           (strcmp(ResourcePtr->ResourceName,ResourceName)==0))
        {
            g_ProgrammableApplyAll->ApplySeqEntries[SeqNo].Instno =
                    ResourcePtr->Instno;
            g_ProgrammableApplyAll->ApplySeqEntries[SeqNo].InstPtr =
                    ResourcePtr->InstPtr;
            g_ProgrammableApplyAll->ApplySeqEntries[SeqNo].PhysicalPtr = 
                    ResourcePtr->PhysicalPtr;
            g_ProgrammableApplyAll->ApplySeqEntries[SeqNo].SequenceNumber = SeqNo;
            strcpy(g_ProgrammableApplyAll->ApplySeqEntries[SeqNo].StationResourceName,
                    ResourcePtr->StationResourceName);
            g_ProgrammableApplyAll->ApplySeqEntries[SeqNo].VirtualPtr = 
                    ResourcePtr->VirtualPtr;
            if((SeqNo + 1) > SeqCount)
                SeqCount = (SeqNo + 1); 
        }
    }
    g_ProgrammableApplyAll->ApplySeqCount = SeqCount;
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: int s_ProcessRemoveAll
//
// Purpose: Reset All the instruments according to the RemoveAll Sequence.
//          Note: MasterReset==true is a master system reset and will reset all
//                Instruments.  The normal reset only resets those instruments
//                "Available/OutOfCal" and "InUse".
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// RemoveSequence    REMOVE_SEQUENCE *   Pointer to the remove sequence to process
// MasterReset       bool                Flag to indicate "MasterReset"
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
static int   s_ProcessRemoveAll(REMOVE_SEQUENCE *RemoveSequence, bool MasterReset,
                                ATXML_XML_String *Response, int BufferSize)
{
    int   Status = 0;
    int   SeqIdx;
	int   SeqIncr;
    INST_DATA *InstPtr;
    bool  InUse;
    int   ResourceType;
    char  ResourceName[ATXML_MAX_NAME];
    bool  SendOk;
    int   ProcHandle;
    RESOURCE_INFO TempResource;
	// Test
	int	RemCnt = 0;

    if(RemoveSequence == NULL)
        return(0);

	if(RemoveSequence->Dynamic)
	{
		// Process dynamic bottom up
		SeqIdx = (RemoveSequence->RemSeqCount) ? 
			              (RemoveSequence->RemSeqCount - 1) : 0;
		SeqIncr = -1;
	}
	else
	{
		SeqIdx = 0;
		SeqIncr = 1;
	}
	// Test
	RemCnt = RemoveSequence->RemSeqCount;

	//////////////////////////////////////////////////
	// Note: Test case for DME PCR. When DCPS in the middle of the reset list
	// was removed, the RemSeqCount would jump from 4 to 2, and the loop would
	// terminate before list was done. Using intermediate variable 
	// locks in original count.
	//////////////////////////////////////////////////


    //for(;(SeqIdx >= 0) && (SeqIdx<RemoveSequence->RemSeqCount);SeqIdx += SeqIncr)
    for(;(SeqIdx >= 0) && (SeqIdx<RemCnt);SeqIdx += SeqIncr)
    {
        if((InstPtr = RemoveSequence->RemSeqEntries[SeqIdx].InstPtr) == NULL)
            continue;
        strcpy(ResourceName,RemoveSequence->RemSeqEntries[SeqIdx].StationResourceName);
        ResourceType = ATXMLW_RESTYPE_BASE;
        InUse = InstPtr->InUse;
        InstPtr->InUse = false;
        if(RemoveSequence->RemSeqEntries[SeqIdx].PhysicalPtr)
        {
            InUse = RemoveSequence->RemSeqEntries[SeqIdx].PhysicalPtr->InUse;
            ResourceType = ATXMLW_RESTYPE_PHYSICAL;
        }
        else if(RemoveSequence->RemSeqEntries[SeqIdx].VirtualPtr)
        {
            InUse = s_TcGetVirtualInUseValue(RemoveSequence->RemSeqEntries[SeqIdx].VirtualPtr);
            ResourceType = ATXMLW_RESTYPE_VIRTUAL;
        }

        CICOREDBGLOG(atxml_FmtMsg("     ResourceType %d ResourceName [%s]",ResourceType, ResourceName));
        if(MasterReset || InUse)
        {
            ProcHandle = RemoveSequence->ProcHandle ? RemoveSequence->ProcHandle : MAX_PROCS;
            TempResource.InstPtr = InstPtr;
            TempResource.PhysicalPtr = RemoveSequence->RemSeqEntries[SeqIdx].PhysicalPtr;
            TempResource.VirtualPtr = RemoveSequence->RemSeqEntries[SeqIdx].VirtualPtr;
            s_TcStartReset(ProcHandle,&TempResource,&SendOk);// possibly wait for other access to complete
            if(SendOk)
            {
                Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CReset(
                                  InstPtr->Instno, ResourceType, ResourceName,
                                  Response, BufferSize);
            }
            s_TcStopReset(ProcHandle,&TempResource);
            CICOREDBGLOG(atxml_FmtMsg("            Reset Status %d",Status));
        }
        else
        {
            CICOREDBGLOG(atxml_FmtMsg("            Not Reset - MasterReset %d, InUse %d",MasterReset,InUse));
        }
    }

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_ProcessApplyAll
//
// Purpose: Reset All the instruments according to the ApplyAll Sequence.
//          Note: MasterReset==true is a master system reset and will reset all
//                Instruments.  The normal reset only resets those instruments
//                "Available/OutOfCal" and "InUse".
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// ApplySequence    APPLY_SEQUENCE *   Pointer to the remove sequence to process
// MasterReset       bool                Flag to indicate "MasterReset"
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
static int   s_ProcessApplyAll(APPLY_SEQUENCE *ApplySequence, bool MasterApply,
                                ATXML_XML_String *Response, int BufferSize)
{
    int   Status = 0;
    int   SeqIdx;
	int   SeqIncr;
    INST_DATA *InstPtr;
    bool  InUse;
    int   ResourceType;
    char  ResourceName[ATXML_MAX_NAME];
    bool  SendOk = true;
    int   ProcHandle;
    RESOURCE_INFO TempResource;
	// Test
	int	ApplyCnt = 0;
	ATML_INTF_SIGDESC atml_intf_sigdesc[1024] = {""};

    if(ApplySequence == NULL)
        return(0);

	if(ApplySequence->Dynamic)
	{
		// Process dynamic bottom up
		SeqIdx = (ApplySequence->ApplySeqCount) ? 
			              (ApplySequence->ApplySeqCount - 1) : 0;
		SeqIncr = -1;
	}
	else
	{
		SeqIdx = 0;
		SeqIncr = 1;
	}
	// Test
	ApplyCnt = ApplySequence->ApplySeqCount;

	//////////////////////////////////////////////////
	// Note: Test case for DME PCR. When DCPS in the middle of the reset list
	// was removed, the RemSeqCount would jump from 4 to 2, and the loop would
	// terminate before list was done. Using intermediate variable 
	// locks in original count.
	//////////////////////////////////////////////////


    //for(;(SeqIdx >= 0) && (SeqIdx<ApplySequence->RemSeqCount);SeqIdx += SeqIncr)
    for(;(SeqIdx >= 0) && (SeqIdx<ApplyCnt);SeqIdx += SeqIncr)
    {
		//clear atxml signal string
		strcpy(atml_intf_sigdesc, "");
        if((InstPtr = ApplySequence->ApplySeqEntries[SeqIdx].InstPtr) == NULL)
            continue;
        strcpy(ResourceName,ApplySequence->ApplySeqEntries[SeqIdx].StationResourceName);
        ResourceType = ATXMLW_RESTYPE_BASE;
        InUse = InstPtr->InUse;
        InstPtr->InUse = false;
        if(ApplySequence->ApplySeqEntries[SeqIdx].PhysicalPtr)
        {
            InUse = ApplySequence->ApplySeqEntries[SeqIdx].PhysicalPtr->InUse;
            ResourceType = ATXMLW_RESTYPE_PHYSICAL;
        }
        else if(ApplySequence->ApplySeqEntries[SeqIdx].VirtualPtr)
        {
            InUse = s_TcGetVirtualInUseValue(ApplySequence->ApplySeqEntries[SeqIdx].VirtualPtr);
            ResourceType = ATXMLW_RESTYPE_VIRTUAL;
        }

        CICOREDBGLOG(atxml_FmtMsg("     ResourceType %d ResourceName [%s]",ResourceType, ResourceName));
       
        ProcHandle = ApplySequence->ProcHandle ? ApplySequence->ProcHandle : MAX_PROCS;
        TempResource.InstPtr = InstPtr;
        TempResource.PhysicalPtr = ApplySequence->ApplySeqEntries[SeqIdx].PhysicalPtr;
        TempResource.VirtualPtr = ApplySequence->ApplySeqEntries[SeqIdx].VirtualPtr;
        if(SendOk)
        {
			strcat(atml_intf_sigdesc, "<AtXmlSignalDescription xmlns:atXml=\"ATXML_TSF\">");
			strcat(atml_intf_sigdesc, "<SignalAction>Enable</SignalAction>");
			strcat(atml_intf_sigdesc, "<SignalResourceName>\"");
			strcat(atml_intf_sigdesc, ResourceName);
			strcat(atml_intf_sigdesc, "\"</SignalResourceName>");
			strcat(atml_intf_sigdesc, "</AtXmlSignalDescription>");
            Status = ((CWrapperInf *)(InstPtr->WrapperInfo->WrapperClass))->CIssueSignal(
                                InstPtr->Instno, ResourceType, ResourceName, atml_intf_sigdesc,
                                Response, BufferSize);
        }
        s_TcStopReset(ProcHandle,&TempResource);
        CICOREDBGLOG(atxml_FmtMsg("            Apply Status %d",Status)); 
	}

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_PurgeProgRemoveAll
//
// Purpose: Purge the programmable RemoveAll Sequence to release memory etc.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ================== ========================================
// ProcHandle        int                ProcHandle of calling procedure
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    none
//
///////////////////////////////////////////////////////////////////////////////
static void  s_PurgeProgRemoveAll(int ProcHandle)
{
    // check if this Proc programmed the remove all sequence
    if(g_ProgrammableRemoveAll &&
       (g_ProgrammableRemoveAll->ProcHandle == ProcHandle))
    {
        delete(g_ProgrammableRemoveAll);
        g_ProgrammableRemoveAll = NULL;
    }

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: s_PurgeProgApplyAll
//
// Purpose: Purge the programmable ApplyAll Sequence to release memory etc.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ================== ========================================
// ProcHandle        int                ProcHandle of calling procedure
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    none
//
///////////////////////////////////////////////////////////////////////////////
static void  s_PurgeProgApplyAll(int ProcHandle)
{
    // check if this Proc programmed the remove all sequence
    if(g_ProgrammableApplyAll &&
       (g_ProgrammableApplyAll->ProcHandle == ProcHandle))
    {
        delete(g_ProgrammableApplyAll);
        g_ProgrammableApplyAll = NULL;
    }

    return;
}



///////////////////////////////////////////////////////////////////////////////
// Function: s_FindResourceByName
//
// Purpose: Search the Resource table for the ResourceName provided
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ================== ========================================
// ResourceName      char *             Name of resource to find
// ResourceInfo      PROC_RESOURCES*    Pointer to resource table to search
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    NULL - Fail
//    Pointer to Resource Information - succeed
//
///////////////////////////////////////////////////////////////////////////////
static RESOURCE_INFO *s_FindResourceByName(char *ResourceName, PROC_RESOURCES *ResourceInfo)
{
	RESOURCE_INFO *ResourcePtr = NULL;
	int i;

	if((ResourceInfo == NULL) || (ResourceName == NULL) || !ResourceName[0])
		return(NULL);

	for(i=0;i<ResourceInfo->ResourceCount;i++)
	{
		if(strcmp(ResourceName,ResourceInfo->Resources[i].ResourceName)==0)
		{
			ResourcePtr = &(ResourceInfo->Resources[i]);
			break;
		}
	}

	return(ResourcePtr);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_FillInstStatusInfo
//
// Purpose: Fill in the instrument status information for the resource
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ================== ========================================
// ResourcePtr       RESOURCE_INFO*     Pointer to resource table entry
// InstStatusPtr     SMAM_INST_STATUS * Pointer to status entry to fill
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    none
//
///////////////////////////////////////////////////////////////////////////////
static void  s_FillInstStatusInfo(RESOURCE_INFO *ResourcePtr,SMAM_INST_STATUS *InstStatusPtr)
{
	char *ResName;
	char *AvailStr;

	if(InstStatusPtr == NULL)
		return;
	memset(InstStatusPtr,0,sizeof(SMAM_INST_STATUS));

	if(ResourcePtr == NULL)
		return;

    strcpy(InstStatusPtr->SignalID,"");
    strcpy(InstStatusPtr->SignalName,"");
    strcpy(InstStatusPtr->ResourceID,"");
    strcpy(InstStatusPtr->ResourceName,ResourcePtr->ResourceName);
	ResName = ResourcePtr->InstPtr->InstrumentName;
	AvailStr = ResourcePtr->InstPtr->Availability;
    strcpy(InstStatusPtr->StationInstName,ResName);
	if(ResourcePtr->PhysicalPtr != NULL)
	{
		ResName = ResourcePtr->PhysicalPtr->ResourceName;
		AvailStr = ResourcePtr->PhysicalPtr->Availability;
	}
	if(ResourcePtr->VirtualPtr != NULL)
	{
		ResName = ResourcePtr->VirtualPtr->ResourceName;
		AvailStr = ResourcePtr->VirtualPtr->Availability;
	}
    strcpy(InstStatusPtr->StationResourceName,ResName);
    strcpy(InstStatusPtr->Availability,AvailStr);
    strcpy(InstStatusPtr->AllocatedTo,"");
	InstStatusPtr->LastCalTime = 0;
	InstStatusPtr->LastSelfTestTpsDate = 0;
	InstStatusPtr->LastSelfTestTpsStatus = false;
	InstStatusPtr->LastIstDate = 0;
	InstStatusPtr->LastIstStatus = false;

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_IssueResourceStatus
//
// Purpose: Issue the XML for the instrument status to the Reponse Buffer
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ================== ========================================
// InstStatusPtr     SMAM_INST_STATUS * Pointer to status entry to print
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response          char*              Respnse Buffer Pointer
//
// Return:
//    none
//
///////////////////////////////////////////////////////////////////////////////
static void  s_IssueResourceStatus(SMAM_INST_STATUS *InstStatusPtr,char *Response,int BufferSize)
{
	int  RespLen, StrLen;
    char TempBuf[2048];

	if((InstStatusPtr == NULL) ||(Response == NULL))
		return;

    sprintf(TempBuf,
		" <ResourceStatus resourceName=\"%s\"\n"
        "   availability=\"%s\"\n"
        "   stationInstrumentName=\"%s\"\n" 
        "   stationResourceName=\"%s\"\n"
        "   allocatedTo=\"%s\" />\n",
        InstStatusPtr->ResourceName,
        InstStatusPtr->Availability,
        InstStatusPtr->StationInstName,
        InstStatusPtr->StationResourceName,
        InstStatusPtr->AllocatedTo);

    RespLen = strlen(Response);
    StrLen = strlen(TempBuf);
	if((BufferSize - RespLen) < StrLen)
		return;

    strcat(Response,TempBuf);

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: int s_Tc...
//
// Purpose: Traffic Cop control routines.  Montor and control usage of instruments
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// Prochandle        int                 Process Handle
// ...
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    true = Ok to proceed with command
//    false = Do not proceed with command
//
///////////////////////////////////////////////////////////////////////////////
static bool s_TcStartIssueSignal(int ProcHandle, RESOURCE_INFO *ResourcePtr,
                                 int Reset, bool *SendFlag)
{
    bool RetVal = false;

    // at a minimum wait for current activity to complete
    if(s_TcNotActive(ProcHandle,ResourcePtr,SendFlag))
        RetVal = true;
    if(Reset == 0)
        s_TcRegisterInUse(ProcHandle,ResourcePtr,true); // all but Reset

    return(RetVal);
}
static void s_TcStopIssueSignal(int ProcHandle, RESOURCE_INFO *ResourcePtr,
                                int Reset)
{

    s_TcStopActive(ProcHandle,ResourcePtr);
    if(Reset == 1)
        s_TcRegisterInUse(ProcHandle,ResourcePtr,false);
    return;
}
static bool s_TcStartIst(int ProcHandle, RESOURCE_INFO *ResourcePtr, bool *SendFlag)
{
    bool RetVal = false;

    // at a minimum wait for current activity to complete
    if(s_TcNotActive(ProcHandle,ResourcePtr,SendFlag))
        RetVal = true;
    s_TcRegisterInUse(ProcHandle,ResourcePtr,true); // all but Reset
    return(RetVal);
}
static void s_TcStopIst(int ProcHandle, RESOURCE_INFO *ResourcePtr)
{
    s_TcStopActive(ProcHandle,ResourcePtr);
    s_TcRegisterInUse(ProcHandle,ResourcePtr,false);
    return;
}
static bool s_TcStartIssueCmds(int ProcHandle, RESOURCE_INFO *ResourcePtr,
                               int Reset, bool *SendFlag)
{
    bool RetVal = false;

    // at a minimum wait for current activity to complete
    if(s_TcNotActive(ProcHandle,ResourcePtr,SendFlag))
        RetVal = true;
    if(Reset == 0)
        s_TcRegisterInUse(ProcHandle,ResourcePtr,true); // all but Reset
    return(RetVal);
}
static void s_TcStopIssueCmds(int ProcHandle, RESOURCE_INFO *ResourcePtr, int Reset)
{
    s_TcStopActive(ProcHandle,ResourcePtr);
    if(Reset == 1)
        s_TcRegisterInUse(ProcHandle,ResourcePtr,false);
    return;
}
static bool s_TcStartDriverFunction(int ProcHandle, RESOURCE_INFO *ResourcePtr,
                                    int Reset, bool *SendFlag)
{
    bool RetVal = false;

    // at a minimum wait for current activity to complete
    if(s_TcNotActive(ProcHandle,ResourcePtr,SendFlag))
        RetVal = true;
    if(Reset == 0)
        s_TcRegisterInUse(ProcHandle,ResourcePtr,true); // all but Reset
    return(RetVal);
}
static void s_TcStopDriverFunction(int ProcHandle, RESOURCE_INFO *ResourcePtr, int Reset)
{
    s_TcStopActive(ProcHandle,ResourcePtr);
    if(Reset == 1)
        s_TcRegisterInUse(ProcHandle,ResourcePtr,false);
    return;
}
static bool s_TcStartReset(int ProcHandle, RESOURCE_INFO *ResourcePtr, bool *SendFlag)
{
    bool RetVal = true; // Allow Reset to override any hung comand

    // at a minimum wait for current activity to complete
    if(s_TcNotActive(ProcHandle,ResourcePtr,SendFlag))
        RetVal = true;
    return(RetVal);
}
static void s_TcStopReset(int ProcHandle, RESOURCE_INFO *ResourcePtr)
{
    s_TcStopActive(ProcHandle,ResourcePtr);
    s_TcRegisterInUse(ProcHandle,ResourcePtr,false);
    return;
}

static bool s_TcNotActive(int ProcHandle,RESOURCE_INFO *ResourcePtr,bool*SendFlag)
{
    bool  RetVal = false;
    int  *ActivityFlag = NULL;
    bool  VirtualPtr = false;
    int   LclActivityFlag;
    int   AvailInt;
    time_t When;

    
    if(!ProcHandle || (ResourcePtr == NULL) || (ResourcePtr->InstPtr == NULL))
        return(RetVal);

    AvailInt = ResourcePtr->InstPtr->AvailInt;
    ActivityFlag = &(ResourcePtr->InstPtr->ActiveProc);
    if(ResourcePtr->PhysicalPtr)
    {
        AvailInt = ResourcePtr->PhysicalPtr->AvailInt;
        ActivityFlag = &(ResourcePtr->PhysicalPtr->ActiveProc);
    }
    if(ResourcePtr->VirtualPtr)
    {
        AvailInt = ResourcePtr->VirtualPtr->AvailInt;
    }
    // Check if available, simulsted, no wrapper, etc.
    if(!s_TcCheckStatus(AvailInt,SendFlag))
        return(false);

    kern_GetMutex(g_InstDataMutex, 1000);
    if(ResourcePtr->VirtualPtr)
    {
        VirtualPtr = true;
        ActivityFlag = &LclActivityFlag;
        LclActivityFlag = s_TcGetVirtualActiveValue(ProcHandle,ResourcePtr->VirtualPtr);
    }
    if(*ActivityFlag && (g_ProcInfo[api_GetProcIdx(*ActivityFlag)].Type == ATXML_PROC_TYPE_SFT_INT))
        When = time(NULL) + 600; // Some instruments can take up to 10 Min to execute SFT
    else
        When = time(NULL) + 5;

    while(*ActivityFlag && (When > time(NULL)))
    {
        kern_ReleaseMutex(g_InstDataMutex);
        Sleep(5);
        kern_GetMutex(g_InstDataMutex, 1000);
        if(VirtualPtr)
            LclActivityFlag = s_TcGetVirtualActiveValue(ProcHandle,ResourcePtr->VirtualPtr);
    }
    if(!(*ActivityFlag))
    {
        // Set Activity Flag to this proc
        if(VirtualPtr)
            s_TcSetVirtualActiveValue(ProcHandle,ResourcePtr->VirtualPtr);
        else
            *ActivityFlag = ProcHandle;
        RetVal = true;
    }
    kern_ReleaseMutex(g_InstDataMutex); 

    return(RetVal);
}

static void s_TcStopActive(int ProcHandle, RESOURCE_INFO *ResourcePtr)
{
    int  *ActivityFlag = NULL;
    bool  VirtualPtr = false;

    if(!ProcHandle || (ResourcePtr == NULL) || (ResourcePtr->InstPtr == NULL))
        return;

    ActivityFlag = &(ResourcePtr->InstPtr->ActiveProc);
    if(ResourcePtr->PhysicalPtr)
        ActivityFlag = &(ResourcePtr->PhysicalPtr->ActiveProc);
    if(ResourcePtr->VirtualPtr)
        VirtualPtr = true;

    // Set Activity Flag to this 0
    kern_GetMutex(g_InstDataMutex, 1000);
    if(VirtualPtr)
        s_TcSetVirtualActiveValue(0,ResourcePtr->VirtualPtr);
    else
        *ActivityFlag = 0;
    kern_ReleaseMutex(g_InstDataMutex); 

    return;
}

static void s_TcRegisterInUse(int ProcHandle, RESOURCE_INFO *ResourcePtr, bool InUse)
{
    int   i;
    bool  LclInUse;

    if(!ProcHandle || (ResourcePtr == NULL) || (ResourcePtr->InstPtr == NULL))
        return;

    // Set InUse Flag 
    kern_GetMutex(g_InstDataMutex, 1000);
    if(ResourcePtr->VirtualPtr)
        s_TcSetVirtualInUseValue(InUse,ResourcePtr->VirtualPtr);
    else if(ResourcePtr->PhysicalPtr)
        ResourcePtr->PhysicalPtr->InUse = InUse;
    else
    {
        // Basic Instrument implies all physical
        for(i=0; i < ResourcePtr->InstPtr->PhysCount; i++)
        {
            ResourcePtr->InstPtr->PhysicalResources[i].InUse = InUse;
        }
    }
    
	// Add or Remove from dynamic removeall list
	s_SetDynamicRemoveAll(ProcHandle, ResourcePtr, InUse);

    // Handle Instrument status
    LclInUse = false;
    if(InUse)
    {
        // if any sub devices are inuse then so is the Instrument
        ResourcePtr->InstPtr->InUse = InUse;
    }
    else
    {
        // The reset is more complicated - need to check physicals
        for(i=0; i < ResourcePtr->InstPtr->PhysCount; i++)
        {
            if(ResourcePtr->InstPtr->PhysicalResources[i].InUse)
                LclInUse = true;

        }
        ResourcePtr->InstPtr->InUse = LclInUse;

    }

    kern_ReleaseMutex(g_InstDataMutex); 
    return;
}

static int s_TcGetVirtualActiveValue(int ProcHandle, VIRTUAL_RESOURCE *VirtualPtr)
{
    int RetVal = 0;
    int i;
    PHYSICAL_RESOURCE *PhysPtr;
    
    for(i=0; i<VirtualPtr->PhysicalResourceCount; i++)
    {
        PhysPtr = VirtualPtr->PhysResources[i];
        if(PhysPtr && PhysPtr->ActiveProc)
        {
            RetVal = PhysPtr->ActiveProc;
            break;
        }
    }
    return(RetVal);
}
static void s_TcSetVirtualActiveValue(int ProcHandle,VIRTUAL_RESOURCE *VirtualPtr)
{
    int i;
    
    for(i=0; i<VirtualPtr->PhysicalResourceCount; i++)
    {
        if(VirtualPtr->PhysResources[i])
            VirtualPtr->PhysResources[i]->ActiveProc = ProcHandle;
    }
    return;
}

static void s_TcSetVirtualInUseValue(bool InUse, VIRTUAL_RESOURCE *VirtualPtr)
{
    int i;
    
    for(i=0; i<VirtualPtr->PhysicalResourceCount; i++)
    {
        if(VirtualPtr->PhysResources[i])
            VirtualPtr->PhysResources[i]->InUse = InUse;
    }
    return;
}

static bool s_TcGetVirtualInUseValue(VIRTUAL_RESOURCE *VirtualPtr)
{
    int i;
    
    for(i=0; i<VirtualPtr->PhysicalResourceCount; i++)
    {
        if(VirtualPtr->PhysResources[i] && VirtualPtr->PhysResources[i]->InUse)
            return(true);
    }
    return(false);
}


static bool s_TcCheckStatus(int AvailInt,bool *SendFlag)
{
    bool RetVal = true;

    if(SendFlag == NULL)
        return(false);

    switch(AvailInt)
    {
    case ATXML_AVAIL_AVAILABLE_INT:
    case ATXML_AVAIL_FAILED_ST_INT:
    case ATXML_AVAIL_FAILED_IST_INT:
    case ATXML_AVAIL_CAL_EXPIRED_INT:
    case ATXML_AVAIL_SIMULATED_INT:
        RetVal = true;
        *SendFlag = true;
        break;
    case ATXML_AVAIL_SIMULATED2_INT:
        RetVal = true;
        *SendFlag = false;
        break;
    case ATXML_AVAIL_NO_INIT:
    case ATXML_AVAIL_NO_RESPONSE_INT:
    case ATXML_AVAIL_NOT_FOUND_INT:
    default:
        RetVal = false;
        *SendFlag = false;
        break;
    }
    return(RetVal);
}

void s_SetDynamicRemoveAll(int ProcHandle, RESOURCE_INFO *ResourcePtr, bool Add)
{
	int  SeqIdx;
	bool InList=false;

	if((g_ProgrammableRemoveAll == NULL) || 
	   (!g_ProgrammableRemoveAll->Dynamic) ||
	   (g_ProgrammableRemoveAll->ProcHandle != ProcHandle))
		return;

	//Is it in the list?
	for(SeqIdx=0; SeqIdx<g_ProgrammableRemoveAll->RemSeqCount; SeqIdx++)
	{
		if((g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].InstPtr ==
                ResourcePtr->InstPtr) &&
		   (g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].PhysicalPtr == 
                ResourcePtr->PhysicalPtr) &&
		   (g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].VirtualPtr == 
                ResourcePtr->VirtualPtr))
		{
			InList = true;
			break;
		}
	}
	if(Add)
	{
		// already in list no need to add again
		if(InList)
			return;
		// Add the new resource to the bottom
		SeqIdx = g_ProgrammableRemoveAll->RemSeqCount;
		if(SeqIdx >= ATXML_MAX_RESOURCES)
		{
			// FIX pack remove all sequence
			SeqIdx = ATXML_MAX_RESOURCES - 1;
		}
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].Instno =
                ResourcePtr->InstPtr->Instno;
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].InstPtr =
                ResourcePtr->InstPtr;
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].PhysicalPtr = 
                ResourcePtr->PhysicalPtr;
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].SequenceNumber = SeqIdx;
        strcpy(g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].StationResourceName,
                ResourcePtr->StationResourceName);
        g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].VirtualPtr = 
                ResourcePtr->VirtualPtr;
		g_ProgrammableRemoveAll->RemSeqCount = ++SeqIdx;

	}
	else
	{
		// Remove from list
		if(!InList)
			return; //???
		g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].Instno = 0;
		g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].InstPtr = NULL;
		//Collect Garbage if last entry and reset count
		if(SeqIdx == (g_ProgrammableRemoveAll->RemSeqCount - 1))
		{
			for(; SeqIdx >= 0; SeqIdx--)
			{
				if(g_ProgrammableRemoveAll->RemSeqEntries[SeqIdx].InstPtr)
					break;
			}
			g_ProgrammableRemoveAll->RemSeqCount = ++SeqIdx;
		}
	}
}