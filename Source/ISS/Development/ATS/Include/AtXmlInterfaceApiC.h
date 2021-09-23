///////////////////////////////////////////////////////////////////////////////
// File:	AtXmlInterfaceApiC.h
//
// Purpose: Prototypes of ATML Interface API utility functions
//
// Date:	10/12/05
//
//
// Revision History
//           Revisions tracked in appropriate .cpp file
//
///////////////////////////////////////////////////////////////////////////////
#include <windows.h>

#if     _MSC_VER > 1000
#pragma once
#endif

#define CALL_TYPE __stdcall

#ifndef ATXML_INTERFACE_C_API_H
#define ATXML_INTERFACE_C_API_H

#pragma warning(disable:4786)
#pragma warning(disable:4996)

#ifndef __cplusplus

#ifdef ATXML_EXPORTS
#define ATXML_FNC __declspec(dllexport)
#else
#define ATXML_FNC __declspec(dllimport)
#endif

#else

#ifdef ATXML_EXPORTS
#define ATXML_FNC "C" __declspec(dllexport)
#else
#define ATXML_FNC "C" __declspec(dllimport)
#endif


#endif

//++++/////////////////////////////////////////////////////////////////////////
// Definitions and prototypes for ATML Interface C API .dll
///////////////////////////////////////////////////////////////////////////////
// Defines
#define ATXML_MAX_AVAIL_VALUE   56
#define ATXML_MAX_NAME          56

enum ATXML_REMALL_STATUS {
    ATXML_REMALL_FAILED           = 1,
    ATXML_REMALL_COMPLETE         = 2,
    ATXML_REMALL_DEFAULT_COMPLETE = 3
};

#define ATXML_CALLBACKPTR int*
// Typedefs
typedef char ATXML_ATML_Snippet;
typedef char ATXML_ATML_String;
typedef char ATXML_XML_Snippet;
typedef char ATXML_XML_Filename;
typedef char ATXML_XML_String;
typedef void *ATXML_XML_Handle;

// atxml_IssueIst Levels
#define ATXML_IST_LVL_PST   1  // Power Up Self Test
#define ATXML_IST_LVL_CNF   2  // Confidence Test (Are you alive)
#define ATXML_IST_LVL_IST   3  // Internal Self Test (IST*)
#define ATXML_IST_LVL_BIT   4  // Multiple BIT levels supported as instrument allows

// Procedure Type constants for Initialie call 
#define ATXML_PROC_TYPE_PAWS_WRTS  "PAWS_WRTS"
#define ATXML_PROC_TYPE_PAWS_NAM   "PAWS_NAM"
#define ATXML_PROC_TYPE_SFP        "SFP"
#define ATXML_PROC_TYPE_TPS        "TPS"
#define ATXML_PROC_TYPE_SFT        "SFT_TPS"

// Availability values
#define ATXML_AVAIL_AVAILABLE      "Available"
#define ATXML_AVAIL_AVAILABLE_IN_USE "AvailableInUse"
#define ATXML_AVAIL_FAILED_ST      "FailedST"/>
#define ATXML_AVAIL_FAILED_IST     "FailedIST"/>
#define ATXML_AVAIL_CAL_EXPIRED    "CalExpired"/>
#define ATXML_AVAIL_NO_RESPONSE    "NoResponse"/>
#define ATXML_AVAIL_NOT_FOUND      "NotFound"/>
#define ATXML_AVAIL_SIMULATED      "Simulated"/>
#define ATXML_AVAIL_SIMULATED_IN_USE "SimulatedInUse"/>

typedef struct AvailabilityStruct {
    char ResourceName[ATXML_MAX_NAME];
    char Availability[ATXML_MAX_AVAIL_VALUE];
} ATXML_AVAILABILITY;


/* Type designators for DriverFunction type string
     "Void", "Handle",
     "SrcStrPtr", "RetStrPtr", 
     "SrcDbl", "SrcDblPtr", "RetDbl", "RetDblPtr", 
     "SrcFlt", "SrcFltPtr", "RetFlt", "RetFltPtr", 
     "SrcBool", "SrcBoolPtr", "RetBool", "RetBoolPtr",
     "SrcUInt8", "SrcUInt8Ptr", "SrcInt8", "SrcInt8Ptr", 
     "RetUInt8", "RetUInt8Ptr", "RetInt8", "RetInt8Ptr", 
     "SrcUInt16", "SrcUInt16Ptr", "SrcInt16", "SrcInt16Ptr", 
     "RetUInt16", "RetUInt16Ptr", "RetInt16", "RetInt16Ptr", 
     "SrcUInt32", "SrcUInt32Ptr", "SrcInt32", "SrcInt32Ptr", 
     "RetUInt32", "RetUInt32Ptr", "RetInt32", "RetInt32Ptr", 
*/

// Prototypes
extern ATXML_FNC int  CALL_TYPE atxml_Initialize(char *ProcType, char *Guid);
extern ATXML_FNC int  CALL_TYPE atxml_Close();
extern ATXML_FNC int  CALL_TYPE atxml_RegisterCallback(int CallbackType, ATXML_CALLBACKPTR Function);
extern ATXML_FNC int  CALL_TYPE atxml_RegisterInterUsed(ATXML_XML_String InterUsage);
extern ATXML_FNC int  CALL_TYPE atxml_RetrieveTpsData(char* TpsName, char* TpsVersion, char* TpsFileName, char* RecommendedAction);
extern ATXML_FNC int  CALL_TYPE atxml_RegisterTSF(ATXML_XML_Filename* TSFSignalDefinition, ATXML_XML_Filename* TSFLibrary, ATXML_XML_Filename* STDTSF, ATXML_XML_Filename* STDBSC);
extern ATXML_FNC int  CALL_TYPE atxml_ValidateRequirements(ATXML_ATML_Snippet* TestRequirements, ATXML_XML_Filename* Allocation, ATXML_XML_String* Availability, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_RegisterRemoveSequence(ATXML_XML_Snippet* RemoveSequence, ATXML_XML_String* Response, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_TestStationStatus(ATXML_ATML_String* TestStationStatus, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_RegisterInstStatus(ATXML_XML_String* InstStatus, ATXML_XML_String* Response, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_RegisterTmaSelect(ATXML_XML_String* TmaList);
extern ATXML_FNC int  CALL_TYPE atxml_SubmitUutId(char* UUT_Partnumber, char* UUT_Serialnumber, int* Action, ATXML_XML_String* cTmaList, int TmaBufferSize, ATXML_XML_String* RecommendedActions, int RaBufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_IssueSignal(ATXML_ATML_Snippet* SignalDescription, ATXML_XML_String* Response, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_QueryInterStatus(ATXML_XML_String* InterStatus, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_InvokeRemoveAllSequence(ATXML_XML_String* Response, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_IssueTestResults(ATXML_ATML_String* TestResults, int TPS_Status, ATXML_XML_String* Response, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_IssueTestResultsFile(ATXML_XML_Filename* TestResultsFile, int TPS_Status, ATXML_XML_String* Response, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_IssueIst(ATXML_XML_String* InstSefTest, ATXML_XML_String* Response, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_IssueNativeCmds(ATXML_XML_String* InstrumentCmds, ATXML_XML_String* Response, int BufferSize);
extern ATXML_FNC int  CALL_TYPE atxml_IssueDriverFunctionCall(ATXML_XML_String* DriverFunction, ATXML_XML_String* Response, int BufferSize);
//---------- ATXML Parsing/Generation Utilities --------------------
extern ATXML_FNC int  CALL_TYPE atxml_ParseAvailability(ATXML_XML_String* Availability, ATXML_AVAILABILITY* AvailabilityArray, int BufferSize);
//   For Response Buffer Handeling
extern ATXML_FNC int  CALL_TYPE atxml_ParseErrDbgResponse(ATXML_XML_String* Response, int BufferSize,
                                                              BOOL Strip, ATXML_XML_Handle *ErrDbgHandle);
extern ATXML_FNC bool CALL_TYPE     atxml_ErrDbgNextError(ATXML_XML_Handle ErrDbgHandle, char **ModuleName, char **LeadText,
                                                              int *ErrCode, char **Message);
extern ATXML_FNC bool CALL_TYPE     atxml_ErrDbgNextDebug(ATXML_XML_Handle ErrDbgHandle, char **ModuleName, 
                                                              int *DbgLevel, char **Message);
extern ATXML_FNC void CALL_TYPE     atxml_ErrDbgClose(ATXML_XML_Handle ErrDbgHandle);
extern ATXML_FNC int  CALL_TYPE atxml_GetSingleIntValue(ATXML_XML_String* Response, char *Attr, int *IntValue);
extern ATXML_FNC int  CALL_TYPE atxml_GetIntArrayValue(ATXML_XML_String* Response, char *Attr,
                                                              int *IntArrayPtr, int *ArraySize);
extern ATXML_FNC int  CALL_TYPE atxml_GetSingleDblValue(ATXML_XML_String* Response, char *Attr, double *DblValue);
extern ATXML_FNC int  CALL_TYPE atxml_GetDblArrayValue(ATXML_XML_String* Response, char *Attr,
                                                              double *DblArrayPtr, int *ArraySize);
extern ATXML_FNC int  CALL_TYPE atxml_GetFltArrayValue(ATXML_XML_String* Response, char *Attr,
                                                              float *DblArrayPtr, int *ArraySize);
extern ATXML_FNC int  CALL_TYPE atxml_GetStringValue(ATXML_XML_String* Response, char *Attr, char *StrPtr, int BufferSize);
//   For building and calling atxml_IssueNativeCmds
extern ATXML_FNC int  CALL_TYPE atxml_WriteCmds(char *ResourceName, char* InstrumentCmds, int *ActWriteLen);
extern ATXML_FNC int  CALL_TYPE atxml_ReadCmds(char *ResourceName, char* ReadBuffer, int BufferSize, int *ActReadLen);
//   For building and calling atxml_DriverFunction
extern ATXML_FNC int  CALL_TYPE atxml_CallDriverFunction(char *ResourceName, char *FunctionName, char *ArgTypeString, ...);

#endif // ATML_INTERFACE_C_API_H
