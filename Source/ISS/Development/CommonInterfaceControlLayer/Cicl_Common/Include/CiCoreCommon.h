//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    CiCoreCommon.h
//
// Date:	    11OCT05
//
// Purpose:	    CiCore Components Exposed Functions
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline                       
///////////////////////////////////////////////////////////////////////////////
#if     _MSC_VER > 1000
#pragma once
#endif


#ifndef ATXML_COMMON_H
#define ATXML_COMMON_H

#pragma warning(disable:4786)
#pragma warning(disable:4996)

#include <string>
#include <time.h>
#include <windows.h>

// Macros
#define strnzcpy(x,y,z) {strncpy(x,y,z); x[z - 1] = '\0';}

// Defines
#define ATXML_MAX_AVAIL_VALUE   56
#define ATXML_MAX_NAME          56
#define ATXML_MAX_DATE          56

// Typedefs
typedef char ATXML_ATML_Snippet;
typedef char ATXML_ATML_String;
typedef char ATXML_XML_Snippet;
typedef char ATXML_XML_Filename;
typedef char ATXML_XML_String;

// Procedure Type constants for Initialie call 
#define ATXML_PROC_TYPE_PAWS_WRTS_INT  1
#define ATXML_PROC_TYPE_PAWS_NAM_INT   2
#define ATXML_PROC_TYPE_SFP_INT        3
#define ATXML_PROC_TYPE_TPS_INT        4
#define ATXML_PROC_TYPE_SFT_INT        5
#define ATXML_PROC_TYPE_OTHER_INT      6

// Availability values declared in AtXmlInterfaceApiC.h
/*
#define ATXML_AVAIL_AVAILABLE      "Available"
#define ATXML_AVAIL_AVAILABLE_IN_USE "AvailableInUse"
#define ATXML_AVAIL_FAILED_ST      "FailedST"/>
#define ATXML_AVAIL_FAILED_IST     "FailedIST"/>
#define ATXML_AVAIL_CAL_EXPIRED    "CalExpired"/>
#define ATXML_AVAIL_NO_RESPONSE    "NoResponse"/>
#define ATXML_AVAIL_NOT_FOUND      "NotFound"/>
#define ATXML_AVAIL_SIMULATED      "Simulated"/>
#define ATXML_AVAIL_SIMULATED_IN_USE "SimulatedInUse"/>
*/

#define ATXML_AVAIL_NO_INIT          0
#define ATXML_AVAIL_AVAILABLE_INT    1
#define ATXML_AVAIL_FAILED_ST_INT    2
#define ATXML_AVAIL_FAILED_IST_INT   3
#define ATXML_AVAIL_CAL_EXPIRED_INT  4
#define ATXML_AVAIL_NO_RESPONSE_INT  5
#define ATXML_AVAIL_NOT_FOUND_INT    6
#define ATXML_AVAIL_SIMULATED_INT    7
#define ATXML_AVAIL_SIMULATED2_INT   8/*no wrapper*/

// Later make these dynamic arrays
#define ATXML_MAX_INST          50
#define ATXML_MAX_WRAPPERS      50
#define ATXML_MAX_RESOURCES     150

/*
Static Data elements:
•	Instrument Number
•	ResourceID
•	InstrumentQueryID
•	InstrumentDriverName
•	InstrumentDriverHandle
•	InstrumentDriverComponentID
•	PrimaryAddress
•	SecondaryAddress
•	SubModuleAddress

Dynamic Data elements:
•	AvailabilityStatus
•	CalDataHandle
•	ClientCount
*/
typedef struct WRAPPER_CLASS_STRUCT{
    char  WrapperName[ATXML_MAX_NAME];
    void *WrapperClass;  // Place holder for class instance pointer
}WRAPPER_CLASS;

#define ATXMLW_RESTYPE_BASE      1
#define ATXMLW_RESTYPE_PHYSICAL  2
#define ATXMLW_RESTYPE_VIRTUAL   3
// Data Structures
//////// Test Station Instance Information /////////
typedef struct INSTRUMENT_ADDRESS_STRUCT
{
    // Resource Module Address of main controller
    int   ResourceAddress;
    // ResourceMan Addressing by Instrument ID
    char  InstrumentQueryID[ATXML_MAX_NAME];  // Response from *IDN?
    int   InstrumentTypeNumber;
    // Visa Open type device addressing "VXI<controllerNumber>::<PrimaryAddress>:INST"
    char  ControllerType[ATXML_MAX_NAME];  // VXI, GPIB, etc.
    int   ControllerNumber;   // VXI chassis number etc.
    int   PrimaryAddress;
    int   SecondaryAddress; 
    int   SubModuleAddress;
}INSTRUMENT_ADDRESS;

typedef struct PHYSICAL_RESOURCE_STRUCT{
    char ResourceID[ATXML_MAX_NAME];
    char ResourceName[ATXML_MAX_NAME];
    INSTRUMENT_ADDRESS InstrumentAddress;
    int  SimulationLevel;
    int  DebugLevel;
    char Availability[ATXML_MAX_AVAIL_VALUE];
    int  AvailInt;
    int  ActiveProc;
    bool InUse;
    int  ClientCount;
}PHYSICAL_RESOURCE;

typedef struct VIRTUAL_RESOURCE_STRUCT{
    char  ResourceID[ATXML_MAX_NAME];
    char  ResourceName[ATXML_MAX_NAME];
    char *PhysicalResourcesUsed;
    int   PhysicalResourceCount;
    PHYSICAL_RESOURCE **PhysResources;
    int   SimulationLevel;
    int   DebugLevel;
    char  Availability[ATXML_MAX_AVAIL_VALUE];
    int   AvailInt;
}VIRTUAL_RESOURCE;

typedef struct INST_DATA_STRUCT{
    int                Instno;
    char               InstrumentName[ATXML_MAX_NAME];  // DMM_1, DCPS_1, etc.
    INSTRUMENT_ADDRESS InstrumentAddress;
    int                SimulationLevel;
    int                DebugLevel;
    int                PhysCount;
    PHYSICAL_RESOURCE *PhysicalResources; // DMM, FTIM, for combined instruments
    int                VirtCount;
    VIRTUAL_RESOURCE  *VirtualResources;  // TACAN resource that uses several phys resources
    WRAPPER_CLASS     *WrapperInfo;
    char               Availability[ATXML_MAX_AVAIL_VALUE];
    int                AvailInt;
    int                ActiveProc;
    bool               InUse;
    int                ClientCount;
}INST_DATA;

//////// Allocation File Information /////////
typedef struct RESOURCE_STRUCT{
    char       ResourceName[ATXML_MAX_NAME];
    char       ResourceId[ATXML_MAX_NAME];
    char       SignalName[ATXML_MAX_NAME];
    char       SignalId[ATXML_MAX_NAME];
    int        Instno;
    char       StationResourceName[ATXML_MAX_NAME];
    bool       Validated;
    INST_DATA *InstPtr;
    PHYSICAL_RESOURCE *PhysicalPtr;
    VIRTUAL_RESOURCE  *VirtualPtr;
}RESOURCE_INFO;
typedef struct ProcessResourceStruct{
    int            ResourceCount;
    RESOURCE_INFO  Resources[ATXML_MAX_RESOURCES];
}PROC_RESOURCES;

//////// Multi-Procedure (Thread) Information /////////
#define MAX_PROCS  40
typedef struct ProcInfoStruct
{
    int   Handle;// should match g_ProcInfo Index
    int   Thread;
    int   Type;
	char  Guid[80];
	int   Pid;
    PROC_RESOURCES Resources;
    int   RespBuf1Size;
    char *RespBuf1;
    int   RespBuf2Size;
    char *RespBuf2;
}PROC_INFO;

/////// Programable Remove Sequence
typedef struct RemoveSequenceEntryStruct{
    int   SequenceNumber; // should be the same as the index
    int   Instno;
    char  StationResourceName[ATXML_MAX_NAME];
    INST_DATA *InstPtr;
    PHYSICAL_RESOURCE *PhysicalPtr;
    VIRTUAL_RESOURCE  *VirtualPtr;
}REMOVE_SEQUENCE_ENTRY;
typedef struct RemoveSequenceStruct{
    int   ProcHandle; // For programmable - who programmed it
    bool  Dynamic; // For programmable - Last On First Off
    int   RemSeqCount;
    REMOVE_SEQUENCE_ENTRY RemSeqEntries[ATXML_MAX_RESOURCES];
}REMOVE_SEQUENCE;

/////// Programable Apply Sequence
typedef struct ApplySequenceEntryStruct{
    int   SequenceNumber; // should be the same as the index
    int   Instno;
    char  StationResourceName[ATXML_MAX_NAME];
    INST_DATA *InstPtr;
    PHYSICAL_RESOURCE *PhysicalPtr;
    VIRTUAL_RESOURCE  *VirtualPtr;
}APPLY_SEQUENCE_ENTRY;
typedef struct ApplySequenceStruct{
    int   ProcHandle; // For programmable - who programmed it
    bool  Dynamic; // For programmable - Last On First Off
    int   ApplySeqCount;
    APPLY_SEQUENCE_ENTRY ApplySeqEntries[ATXML_MAX_RESOURCES];
}APPLY_SEQUENCE;

#define API_STATUS_STOPPED   1
#define API_STATUS_STARTING  2
#define API_STATUS_RUNNING   3
#define API_STATUS_HALT      4

// Station Log File Flags
#define ATXML_LOG_DEBUG      1
#define ATXML_LOG_EVENTS     2
// Station Log File Macros
extern  bool g_CiCoreDbg;
#define CICOREDBGLOG(x) {if(g_CiCoreDbg)atxml_Log(ATXML_LOG_DEBUG,x);}

// Thread status flag instanced by CiclKernelW.cpp
// Global Data Instanced
extern DWORD     g_ApiThread;
extern int       g_ApiThreadStatus;
extern PROC_INFO g_ProcInfo[MAX_PROCS];
extern HANDLE    g_InstDataMutex;

// Instrument Tracking library prototypes AtXmlSmam.cpp
// Global Data Instanced
extern int              g_InstDataCount;
extern INST_DATA        g_InstData[ATXML_MAX_INST];
extern int              g_WrapperClassCount;
extern WRAPPER_CLASS    g_WrapperClass[ATXML_MAX_WRAPPERS];
extern int              g_ResourceInfoCount;
extern RESOURCE_INFO    g_ResourceInfo[ATXML_MAX_RESOURCES];
extern REMOVE_SEQUENCE  g_SystemRemoveAll;
extern REMOVE_SEQUENCE *g_ProgrammableRemoveAll;

// Kernal Prototypes
bool kern_GetMutex(HANDLE Mutex, int TimeOut);
void kern_ReleaseMutex(HANDLE Mutex);

// Api Interface Prototypes
extern int     api_GetProcIdx(int ProcHandle);
// Smam Prototypes
extern int     smam_MainInitialize(void);
extern int     smam_MainClose(void);
extern int     smam_Initialize(int Handle, char *ProcUuid, int ProcType,
                                         int Pid);
extern int     smam_Close(int ProcHandle);
extern int     smam_ValidateRequirements(int ProcHandle,
                              ATXML_ATML_Snippet* TestRequirements,
                              ATXML_XML_Filename* Allocation,
                              ATXML_XML_String* Availability, int BufferSize);
extern int     smam_IssueSignal(int ProcHandle,
                              ATXML_ATML_Snippet* SignalDescription,
                              ATXML_XML_String* Response, int BufferSize);
extern int     smam_RegisterRemoveSequence(int ProcHandle,
                              ATXML_ATML_Snippet* RemoveSequence,
                              ATXML_XML_String* Response, int BufferSize);
extern int     smam_InvokeRemoveAllSequence(int ProcHandle,
                              ATXML_XML_String* Response, int BufferSize);
extern int     smam_RegisterApplySequence(int ProcHandle,
                              ATXML_ATML_Snippet* ApplySequence,
                              ATXML_XML_String* Response, int BufferSize);
extern int     smam_InvokeApplyAllSequence(int ProcHandle,
                              ATXML_XML_String* Response, int BufferSize);
extern int     smam_TestStationStatus(int ProcHandle,
                              ATXML_ATML_Snippet* TestRequirements,
                              ATXML_XML_String* Response, int BufferSize);
extern int     smam_IssueIst(int ProcHandle,
                              ATXML_XML_String* InstSelfTest,
                              ATXML_XML_String* Response, int BufferSize);
extern int     smam_IssueNativeCmds(int ProcHandle,
                              ATXML_ATML_Snippet* InstrumentCmds,
                              ATXML_XML_String* Response, int BufferSize);
extern int     smam_IssueDriverFunctionCall(int ProcHandle,
                              ATXML_XML_String* DriverFunction,
                              ATXML_XML_String* Response, int BufferSize);
// Response/Message library prototypes AtXmlResponse.cpp
extern void    atxml_ErrorResponse(ATXML_XML_String *Response, int BufferSize, 
                           char *LeadText, int ErrCode, char *ErrMsg);
extern void    atxml_DebugMsg(int MemDbgLvl, int ReqLvl, char *Msg,
                             ATXML_XML_String *Response, int BufferSize);
extern char   *atxml_FmtMsg(char *Fmt,...);
extern void    atxml_SystemError(int ErrLevel, char *LeadText, int ErrCode, char *ErrMsg);
extern void    atxml_LogInit(int Mode, char *FileName, bool Append);
extern void    atxml_Log(int Mode, char *Message);


//++++/////////////////////////////////////////////////////////////////////////
// ATXML ERROR MESSAGES
///////////////////////////////////////////////////////////////////////////////

// AtXml Standard Error Messages
#define ATXML_ERRCD_INIT_WRAPPER       -5001
#define ATXML_ERRMSG_INIT_WRAPPER              "Wrapper Initialization Error"

#define ATXML_ERRCD_DEVICE_NOT_FOUND   -5002
#define ATXML_ERRMSG_DEVICE_NOT_FOUND         "Device Not Found"

#define ATXML_ERRCD_MAX_TIME           -5003
#define ATXML_ERRMSG_MAX_TIME                 "Device Timeout"

#define ATXML_ERRCD_FILE_NOT_FOUND     -5004
#define ATXML_ERRMSG_FILE_NOT_FOUND           "File Not Found"

#define ATXML_ERRCD_TOO_MANY_RESOURCES -5005
#define ATXML_ERRMSG_TOO_MANY_RESOURCES       "Attempt To Allocate Too Many Resources"

#define ATXML_ERRCD_TEST_REQUIREMENTS  -5006
#define ATXML_ERRMSG_TEST_REQUIREMENTS        "TestRequirements Invalid or Missing"

#define ATXML_ERRCD_RESOURCE_NOT_FOUND -5007
#define ATXML_ERRMSG_RESOURCE_NOT_FOUND       "Resource Not Allocated"

#define ATXML_ERRCD_RESOURCE_BUSY      -5008
#define ATXML_ERRMSG_RESOURCE_BUSY            "Resource Busy"

#define ATXML_ERRCD_RESPONSE_BUFFER    -5009
#define ATXML_ERRMSG_RESPONSE_BUFFER          "Response Buffer To Small"

#endif // ATXML_COMMON_H