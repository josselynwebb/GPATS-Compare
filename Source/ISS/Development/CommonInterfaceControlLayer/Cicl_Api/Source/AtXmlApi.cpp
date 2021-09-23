//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlApi.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXML Interface API (AtXmlApi)
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
#include <process.h>
#include "AtXmlInterfaceApiC.h"
//#include "AtXmlInterfaceApiCpp.h"
#include "soapH.h"
#include "AtXmlInterface.nsmap"
#include "plugin.h"

#define ATXML_KEEPALIVE
// Macros
#define strnzcpy(x,y,z) {strncpy(x,y,z); x[z - 1] = '\0';}

// Local Variables
static int          s_ApiHandle = 0; // Api Handle assigned at soap init time to 
                              // transfer with all messages
static char         s_ApiGuid[80]; // Hold for atxml_Close
static int          s_ApiPid = 0;
static struct soap *s_soap;

// Local Defines
typedef struct ErrMsgStruct
{
    char  ModuleName[ATXML_MAX_NAME];
    char  LeadText[ATXML_MAX_NAME];
    int   ErrorCode;
    char *Msg;
}S_ERRMSGSTRUCT;
typedef struct DbgMsgStruct
{
    char  ModuleName[ATXML_MAX_NAME];
    int   DbgLevel;
    char *Msg;
}S_DBGMSGSTRUCT;
typedef struct ErrDbgStruct
{
    int   ErrCount;
    int   NextErr;
    S_ERRMSGSTRUCT *ErrMsgs;
    int   DbgCount;
    int   NextDbg;
    S_DBGMSGSTRUCT *DbgMsgs;
}S_ERRDBGSTRUCT;

typedef struct ArgListStruct
{
    int   ArgType;
    char  ArgXmlString[ATXML_MAX_NAME];
    void *RetPtr;
    char *ValueString;
    int   Size;
}ARGLIST;

//   -------------- DeviceFunction Parameter Types --------------
#define DF_PARAM_TYPE_Void               1
#define DF_PARAM_TYPE_Handle             2
#define DF_PARAM_TYPE_SrcDbl             3
#define DF_PARAM_TYPE_SrcDblPtr          4
#define DF_PARAM_TYPE_RetDbl           105
#define DF_PARAM_TYPE_RetDblPtr        106       
#define DF_PARAM_TYPE_SrcFlt             7 
#define DF_PARAM_TYPE_SrcFltPtr          8       
#define DF_PARAM_TYPE_RetFlt           109 
#define DF_PARAM_TYPE_RetFltPtr        110        
#define DF_PARAM_TYPE_SrcUInt8          11    
#define DF_PARAM_TYPE_SrcUInt8Ptr       12         
#define DF_PARAM_TYPE_SrcInt8           13   
#define DF_PARAM_TYPE_SrcInt8Ptr        14        
#define DF_PARAM_TYPE_RetUInt8         115    
#define DF_PARAM_TYPE_RetUInt8Ptr      116         
#define DF_PARAM_TYPE_RetInt8          117   
#define DF_PARAM_TYPE_RetInt8Ptr       118        
#define DF_PARAM_TYPE_SrcUInt16         19     
#define DF_PARAM_TYPE_SrcUInt16Ptr      20          
#define DF_PARAM_TYPE_SrcInt16          21    
#define DF_PARAM_TYPE_SrcInt16Ptr       22         
#define DF_PARAM_TYPE_RetUInt16        123     
#define DF_PARAM_TYPE_RetUInt16Ptr     124          
#define DF_PARAM_TYPE_RetInt16         125    
#define DF_PARAM_TYPE_RetInt16Ptr      126         
#define DF_PARAM_TYPE_SrcUInt32         27     
#define DF_PARAM_TYPE_SrcUInt32Ptr      28          
#define DF_PARAM_TYPE_SrcInt32          29    
#define DF_PARAM_TYPE_SrcInt32Ptr       30         
#define DF_PARAM_TYPE_RetUInt32        131     
#define DF_PARAM_TYPE_RetUInt32Ptr     132          
#define DF_PARAM_TYPE_RetInt32         133    
#define DF_PARAM_TYPE_RetInt32Ptr      134         
#define DF_PARAM_TYPE_SrcStrPtr         35         
#define DF_PARAM_TYPE_RetStrPtr        136
#define DF_PARAM_TYPE_SrcBool           37    
#define DF_PARAM_TYPE_SrcBoolPtr        38         
#define DF_PARAM_TYPE_RetBool          139     
#define DF_PARAM_TYPE_RetBoolPtr       140          

struct DfParamTypeListStruct
{
    char   *TypeString;
    int     Type;
}s_DfParamTypeList[] = {
     "Void", DF_PARAM_TYPE_Void,
     "Handle", DF_PARAM_TYPE_Handle,
     "SrcStrPtr", DF_PARAM_TYPE_SrcStrPtr,
     "RetStrPtr", DF_PARAM_TYPE_RetStrPtr,
     "SrcDbl", DF_PARAM_TYPE_SrcDbl,
     "SrcDblPtr", DF_PARAM_TYPE_SrcDblPtr,
     "RetDbl", DF_PARAM_TYPE_RetDbl,
     "RetDblPtr", DF_PARAM_TYPE_RetDblPtr,
     "SrcFlt", DF_PARAM_TYPE_SrcFlt,
     "SrcFltPtr", DF_PARAM_TYPE_SrcFltPtr,
     "RetFlt", DF_PARAM_TYPE_RetFlt,
     "RetFltPtr", DF_PARAM_TYPE_RetFltPtr,
     "SrcUInt8", DF_PARAM_TYPE_SrcUInt8,
     "SrcUInt8Ptr", DF_PARAM_TYPE_SrcUInt8Ptr,
     "SrcInt8", DF_PARAM_TYPE_SrcInt8,
     "SrcInt8Ptr", DF_PARAM_TYPE_SrcInt8Ptr,
     "RetUInt8", DF_PARAM_TYPE_RetUInt8,
     "RetUInt8Ptr", DF_PARAM_TYPE_RetUInt8Ptr,
     "RetInt8", DF_PARAM_TYPE_RetInt8,
     "RetInt8Ptr", DF_PARAM_TYPE_RetInt8Ptr,
     "SrcUInt16", DF_PARAM_TYPE_SrcUInt16,
     "SrcUInt16Ptr", DF_PARAM_TYPE_SrcUInt16Ptr,
     "SrcInt16", DF_PARAM_TYPE_SrcInt16,
     "SrcInt16Ptr", DF_PARAM_TYPE_SrcInt16Ptr,
     "RetUInt16", DF_PARAM_TYPE_RetUInt16,
     "RetUInt16Ptr", DF_PARAM_TYPE_RetUInt16Ptr,
     "RetInt16", DF_PARAM_TYPE_RetInt16,
     "RetInt16Ptr", DF_PARAM_TYPE_RetInt16Ptr,
     "SrcUInt32", DF_PARAM_TYPE_SrcUInt32,
     "SrcUInt32Ptr", DF_PARAM_TYPE_SrcUInt32Ptr,
     "SrcInt32", DF_PARAM_TYPE_SrcInt32,
     "SrcInt32Ptr", DF_PARAM_TYPE_SrcInt32Ptr,
     "RetUInt32", DF_PARAM_TYPE_RetUInt32,
     "RetUInt32Ptr", DF_PARAM_TYPE_RetUInt32Ptr,
     "RetInt32", DF_PARAM_TYPE_RetInt32,
     "RetInt32Ptr", DF_PARAM_TYPE_RetInt32Ptr,
     "SrcBool", DF_PARAM_TYPE_SrcBool,
     "SrcBoolPtr", DF_PARAM_TYPE_SrcBoolPtr,
     "RetBool", DF_PARAM_TYPE_RetBool,
     "RetBoolPtr", DF_PARAM_TYPE_RetBoolPtr,
     NULL,0
};
typedef union 
{
char*    PString;
double    Double;
double*  PDouble;
float     Float;
float*   PFloat;
long      Int32;
long*    PInt32;
short     Int16;
short*   PInt16;
unsigned char Int8;
unsigned char*    PInt8;
bool      Bool;
bool*    PBool;
}ATXML_DF_VAL; 

// Local Function Prototypes
static int  s_getArgType(char *String, ARGLIST *ArgPtr);
static void s_GenDblValue(double Value,ARGLIST *ArgPtr);
static void s_GenDblPtrValue(double *Value,ARGLIST *ArgPtr);
static void s_GenInt32Value(unsigned long Value,ARGLIST *ArgPtr);
static void s_GenInt32PtrValue(unsigned long *Value,ARGLIST *ArgPtr);
static void s_GenInt16Value(unsigned short Value,ARGLIST *ArgPtr);
static void s_GenInt16PtrValue(unsigned short *Value,ARGLIST *ArgPtr);
static void s_GenInt8Value(unsigned char Value,ARGLIST *ArgPtr);
static void s_GenInt8PtrValue(unsigned char *Value,ARGLIST *ArgPtr);
static void s_GenBoolValue(bool Value,ARGLIST *ArgPtr);
static void s_GenBoolPtrValue(bool *Value,ARGLIST *ArgPtr);
static void s_GenStrPtrValue(char *Value,ARGLIST *ArgPtr);
static void s_PutDblValue(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutDblPtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutFltPtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutInt32Value(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutInt32PtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutInt16Value(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutInt16PtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutInt8Value(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutInt8PtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutBoolValue(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutBoolPtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr);
static void s_PutStrPtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_Initialize
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
static bool InitOnce = false;
extern ATXML_FNC int CALL_TYPE atxml_Initialize(char *ProcType, char *Guid)
{
    int Status = 0;
    if(InitOnce)
        return(Status);
    InitOnce = true;
    s_soap = soap_new();
    if(s_soap == NULL)
        Status = -1;
	else
	{
#ifdef ATXML_KEEPALIVE
        soap_init2(s_soap, SOAP_IO_KEEPALIVE, SOAP_IO_KEEPALIVE);
		//soap_register_plugin(s_soap, plugin);  //Don't remove this is used for troubleshooting
#endif
		strnzcpy(s_ApiGuid,Guid,70);
		s_ApiPid = _getpid();
		Status = soap_call_atxml__Initialize(s_soap,NULL,NULL,
			                 ProcType, s_ApiGuid, s_ApiPid, &s_ApiHandle);
	}
	return (Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_Close
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

extern ATXML_FNC int CALL_TYPE atxml_Close(void)
{
	int Status = 0;
    if(s_soap == NULL)
        return(-1);

#ifdef ATXML_KEEPALIVE
    soap_clr_imode(s_soap, SOAP_IO_KEEPALIVE);
    soap_clr_omode(s_soap, SOAP_IO_KEEPALIVE);
#endif
	soap_call_atxml__Close(s_soap,NULL,NULL,s_ApiHandle,
			                 s_ApiGuid, s_ApiPid,
							 &Status);
    soap_destroy(s_soap);
    soap_end(s_soap);
    soap_done(s_soap);
    s_soap = NULL;
	return (0);
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
extern ATXML_FNC int CALL_TYPE atxml_RegisterTSF(ATXML_XML_Filename* TSFSignalDefinition, 
                              ATXML_XML_Filename* TSFLibrary,
                              ATXML_XML_Filename* STDTSF,
                              ATXML_XML_Filename* STDBSC)
{
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
extern ATXML_FNC int CALL_TYPE atxml_ValidateRequirements(ATXML_ATML_Snippet* TestRequirements,
                              ATXML_XML_Filename* Allocation,
                              ATXML_XML_String* Availability, int BufferSize)
{
    atxml__ValidateRequirementsResponse ReturnValues;

    if(Availability)
        *Availability = '\0';
    ReturnValues.Availability = NULL;

    soap_call_atxml__ValidateRequirements(s_soap,NULL,NULL,s_ApiHandle,
                      TestRequirements, Allocation, BufferSize, ReturnValues);
    if(Availability && ReturnValues.Availability)
    {
        strnzcpy(Availability,ReturnValues.Availability,BufferSize);
    }

    return(ReturnValues.result);
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

extern ATXML_FNC int CALL_TYPE atxml_IssueSignal(ATXML_ATML_Snippet* SignalDescription,
                ATXML_XML_String* Response, int BufferSize)
{
    atxml__IssueSignalResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.Response = NULL;

    soap_call_atxml__IssueSignal(s_soap,NULL,NULL,s_ApiHandle,
                      SignalDescription, BufferSize, ReturnValues);
    if(Response && ReturnValues.Response)
    {
        strnzcpy(Response,ReturnValues.Response,BufferSize);
    }
    return(ReturnValues.result);
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
extern ATXML_FNC int CALL_TYPE atxml_TestStationStatus(ATXML_ATML_String* Response, int BufferSize)
{

    atxml__TestStationStatusResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.TestStationStatus = NULL;

    soap_call_atxml__TestStationStatus(s_soap,NULL,NULL,s_ApiHandle,
                      BufferSize,ReturnValues);
    if(Response && ReturnValues.TestStationStatus)
    {
        strnzcpy(Response,ReturnValues.TestStationStatus,BufferSize);
    }
    return(ReturnValues.result);
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
extern ATXML_FNC int CALL_TYPE atxml_RegisterInstStatus(ATXML_XML_String* InstStatus,
                       ATXML_XML_String* Response, int BufferSize)
{
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_RegisterRemoveSequence
//
// Purpose: Register the UUT dependant Resource removal sequence
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// RemoveSequence    ATXML_XML_Snippet*  AtXml Remove Sequence message  
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
extern ATXML_FNC int CALL_TYPE atxml_RegisterRemoveSequence(ATXML_XML_Snippet* RemoveSequence,
                       ATXML_XML_String* Response, int BufferSize)
{
    atxml__RegisterRemoveSequenceResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.Response = NULL;

    soap_call_atxml__RegisterRemoveSequence(s_soap,NULL,NULL,s_ApiHandle,
                      RemoveSequence,BufferSize,ReturnValues);
    if(Response && ReturnValues.Response)
    {
        strnzcpy(Response,ReturnValues.Response,BufferSize);
    }
    return(ReturnValues.result);
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
extern ATXML_FNC int CALL_TYPE atxml_InvokeRemoveAllSequence(ATXML_XML_String* Response, int BufferSize)
{
    atxml__InvokeRemoveAllSequenceResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.Response = NULL;

    soap_call_atxml__InvokeRemoveAllSequence(s_soap,NULL,NULL,s_ApiHandle,
                      BufferSize,ReturnValues);
    if(Response && ReturnValues.Response)
    {
        strnzcpy(Response,ReturnValues.Response,BufferSize);
    }
    return(ReturnValues.result);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_RegisterApplySequence
//
// Purpose: Register the UUT dependant Resource removal sequence
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// ApplySequence    ATXML_XML_Snippet*  AtXml Apply Sequence message  
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
extern ATXML_FNC int CALL_TYPE atxml_RegisterApplySequence(ATXML_XML_Snippet* ApplySequence,
                       ATXML_XML_String* Response, int BufferSize)
{
    atxml__RegisterApplySequenceResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.Response = NULL;

    soap_call_atxml__RegisterApplySequence(s_soap,NULL,NULL,s_ApiHandle,
                      ApplySequence,BufferSize,ReturnValues);
    if(Response && ReturnValues.Response)
    {
        strnzcpy(Response,ReturnValues.Response,BufferSize);
    }
    return(ReturnValues.result);
}
///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_InvokeApplyAllSequence
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
extern ATXML_FNC int CALL_TYPE atxml_InvokeApplyAllSequence(ATXML_XML_String* Response, int BufferSize)
{
    atxml__InvokeApplyAllSequenceResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.Response = NULL;

    soap_call_atxml__InvokeApplyAllSequence(s_soap,NULL,NULL,s_ApiHandle,
                      BufferSize,ReturnValues);
    if(Response && ReturnValues.Response)
    {
        strnzcpy(Response,ReturnValues.Response,BufferSize);
    }
    return(ReturnValues.result);
}



//++++/////////////////////////////////////////////////////////////////////////
// Special Non-ATML Cheat Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_IssueIst
//
// Purpose: Register the latest Calibration Factors to the AtXml
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// InstSelfTest      ATXML_XML_String*   XML String for intsrument self test
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
extern ATXML_FNC int CALL_TYPE atxml_IssueIst(ATXML_XML_String* InstSelfTest,
                       ATXML_XML_String* Response, int BufferSize)
{
    atxml__IssueIstResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.Response = NULL;

    soap_call_atxml__IssueIst(s_soap,NULL,NULL,s_ApiHandle,
                      InstSelfTest, BufferSize, ReturnValues);
    if(Response && ReturnValues.Response)
    {
        strnzcpy(Response,ReturnValues.Response,BufferSize);
    }
    return(ReturnValues.result);
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
extern ATXML_FNC int CALL_TYPE atxml_IssueNativeCmds(ATXML_XML_String* InstrumentCmds,
                                  ATXML_XML_String* Response, int BufferSize)
{
    atxml__IssueNativeCmdsResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.Response = NULL;

    soap_call_atxml__IssueNativeCmds(s_soap,NULL,NULL,s_ApiHandle,
                      InstrumentCmds, BufferSize, ReturnValues);
    if(Response && ReturnValues.Response)
    {
        strnzcpy(Response,ReturnValues.Response,BufferSize);
    }
    return(ReturnValues.result);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_IssueDriverFunctionCall
//
// Purpose: Invoke Instrument Driver Call and return response
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
extern ATXML_FNC int CALL_TYPE atxml_IssueDriverFunctionCall(ATXML_XML_String* DriverFunction,
                                  ATXML_XML_String* Response, int BufferSize)
{
    atxml__IssueDriverFunctionCallResponse ReturnValues;

    if(Response && (BufferSize > 0))
        *Response = '\0';
    ReturnValues.Response = NULL;

    soap_call_atxml__IssueDriverFunctionCall(s_soap,NULL,NULL,s_ApiHandle,
                      DriverFunction, BufferSize, ReturnValues);
    if(Response && ReturnValues.Response)
    {
        strnzcpy(Response,ReturnValues.Response,BufferSize);
    }
    return(ReturnValues.result);
}

                                  
//++++/////////////////////////////////////////////////////////////////////////
// Utility Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_WriteCmds
//
// Purpose: AtXml equivelant to viWrite for instrument commands
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// ResourceName      char*               Virtual resource name that has been allocated
//                                       via atxml_ValidateRequirements
// InstrumentCmds    char*               Instrument Command String to be written
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// ActWriteLen      int*                Return actual bytes written count
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int CALL_TYPE atxml_WriteCmds(char *ResourceName, char* InstrumentCmds,
                                         int *ActWriteLen)
{
    char *XmlBuf;
    char  Response[2048];
    char *cptr;
    int   Status = 0;

    if((ResourceName == NULL) || (InstrumentCmds == NULL) || (ActWriteLen == NULL))
    {
        return(-1);
    }

    XmlBuf = new char[strlen(InstrumentCmds)+200];
    sprintf(XmlBuf,
	"<AtXmlIssueInstrumentCommands>\n"
	"	<SignalResourceName>%s</SignalResourceName>\n"
	"	<InstrumentCommands>\n"
	"		<Commands>%s</Commands>\n"
	"	</InstrumentCommands>\n"
	"</AtXmlIssueInstrumentCommands>\n",
    ResourceName,InstrumentCmds);
    Status = atxml_IssueNativeCmds(XmlBuf, Response, 2048);
    delete(XmlBuf);
    *ActWriteLen = 0;
    if((cptr=strstr(Response,"IcWriteLen")) && (cptr=strstr(cptr,"value")))
    {
        sscanf(cptr,"value = \" %d",ActWriteLen);
    }

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_WriteCmds
//
// Purpose: AtXml equivelant to viWrite for instrument commands
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// ResourceName      char*               Virtual resource name that has been allocated
//                                       via atxml_ValidateRequirements
// ReadBuffer        char*               Buffer for read string
// BufferSize        int                 Size of ReadBuffer
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// ActReadLen       int*                Return actual bytes read count
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int CALL_TYPE atxml_ReadCmds(char *ResourceName, char* ReadBuffer,
                                        int BufferSize, int *ActReadLen)
{
    char  XmlBuf[500];
    char *Response;
    int   RespLen;
    char *cptr,*BegPtr,*EndPtr;
    int   Status = 0;

    if((ResourceName == NULL) || (ReadBuffer == NULL) || (ActReadLen == NULL))
    {
        return(-1);
    }

    RespLen = BufferSize+1024;
    if((Response = new char[RespLen]) == NULL)
        return(-2);
    sprintf(XmlBuf,
	"<AtXmlIssueInstrumentCommands>\n"
	"	<SignalResourceName>%s</SignalResourceName>\n"
	"	<InstrumentCommands>\n"
	"		<ExpectedResponseString MaxLength=\"%d\"/>\n"
	"	</InstrumentCommands>\n"
	"</AtXmlIssueInstrumentCommands>\n",
    ResourceName,BufferSize);
    Status = atxml_IssueNativeCmds(XmlBuf, Response, RespLen);
    /*
	    <Attribute>IcReadString</Attribute>
	    <Value>
	    	<c:Datum xsi:type="c:string" unit=""><c:Value>READ STRING</c:Value></c:Datum>
	    </Value>
    */
    *ActReadLen = 0;
    if((cptr=strstr(Response,"IcReadString")) && 
       (cptr=strstr(cptr,"Datum")) &&
       (cptr=strstr(cptr,"Value")) &&
       (BegPtr=strstr(cptr,">")) &&
       (EndPtr=strstr(BegPtr,"</")))
    {
        BegPtr++;
        *EndPtr = '\0';
        strnzcpy(ReadBuffer,BegPtr,BufferSize);
        *ActReadLen = strlen(BegPtr);
        *EndPtr = '<';
    }
    delete(Response);
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_CallDriverFunction
//
// Purpose: AtXml Helper function to build XML string and pass to IssueDriverFunction
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// FunctionName      char*               String that matches Driver Function Name
// ArgTypeString     char*               String of argument type specifiers
// Argumets          ...                 The normal calling arguments for the
//                                       Driver Function Call
//                                       The First Argument (0) is the Driver Function
//                                       Return value.
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
extern ATXML_FNC int  CALL_TYPE atxml_CallDriverFunction(
                                            char *ResourceName, 
                                            char *FunctionName, char *ArgTypeString, ...)
{
    char *OutString;
    int   OutSize;
    ARGLIST *ArgList;
    va_list Args;
    char *cptr;
    int ArgCount=0;
    int ArgIdx;
    int ValueLen;
    int RetValueCount;
    char *Response;
    char RetAttrName[ATXML_MAX_NAME];
/*
    ArgTypeString for:  (FuncReturn Type = Param0)
    int TestParam(unsigned int Param1,unsigned short Param2,
                double *Param3,const char *Param4,
                unsigned char *RetParam5, char*RetParam6);
       Param0  Param1   Param2   Param3       Param4    RetParam5     RetParam6
    "RetInt32,SrcInt32,SrcUInt16,SrcDblPtr 3,SrcStrPtr,RetUInt8, RetStrPtr 1024"
*/
    // Determin number of arguments
    cptr = ArgTypeString;
    while(cptr)
    {
        ArgCount++;
        if((cptr = strstr(cptr,",")))
            cptr++;
    }
    // allocate ArgList array
    ArgList = new ARGLIST[ArgCount + 1];
    if(ArgList == NULL)
        return(-1);//FIX document and defin error returns
    memset(ArgList,0,(sizeof(ARGLIST) * (ArgCount + 1)));
    // Fill in Type info into ArgList array
    cptr = ArgTypeString;
    ArgIdx = 0;
    while(cptr && *cptr &&(ArgIdx<ArgCount))
    {
        if(s_getArgType(cptr,&(ArgList[ArgIdx])))
            return(-2);
        if((cptr = strstr(cptr,",")))
            cptr++;
        ArgIdx++;
    }
    // Fill in Value Strings for ArgList
    ValueLen = 0;
    RetValueCount = 0;
    if(ArgCount)
    {
        va_start(Args,ArgTypeString);
        ArgIdx = 0;
        while(ArgIdx<ArgCount)
        {
            if(ArgList[ArgIdx].ArgType > 100)
            {
                ArgList[ArgIdx].RetPtr = va_arg(Args,void*);
            }
            else switch(ArgList[ArgIdx].ArgType)
            {
            case DF_PARAM_TYPE_Void:
            case DF_PARAM_TYPE_Handle:
                va_arg(Args,int);// Discard
                break;
            case DF_PARAM_TYPE_SrcDbl:
			case DF_PARAM_TYPE_SrcFlt:
                s_GenDblValue(va_arg(Args,double),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcDblPtr:
			case DF_PARAM_TYPE_SrcFltPtr:
                s_GenDblPtrValue(va_arg(Args,double*),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcUInt32:
            case DF_PARAM_TYPE_SrcInt32:
                s_GenInt32Value(va_arg(Args,unsigned long),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcUInt32Ptr:
            case DF_PARAM_TYPE_SrcInt32Ptr:
                s_GenInt32PtrValue(va_arg(Args,unsigned long*),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcUInt16:
            case DF_PARAM_TYPE_SrcInt16:
                s_GenInt16Value(va_arg(Args,unsigned short),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcUInt16Ptr:
            case DF_PARAM_TYPE_SrcInt16Ptr:
                s_GenInt16PtrValue(va_arg(Args,unsigned short*),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcUInt8:
            case DF_PARAM_TYPE_SrcInt8:
                s_GenInt8Value(va_arg(Args,unsigned char),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcUInt8Ptr:
            case DF_PARAM_TYPE_SrcInt8Ptr:
                s_GenInt8PtrValue(va_arg(Args,unsigned char*),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcBool:
                s_GenBoolValue(va_arg(Args,bool),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcBoolPtr:
                s_GenBoolPtrValue(va_arg(Args,bool*),&(ArgList[ArgIdx]));
                break;
            case DF_PARAM_TYPE_SrcStrPtr:
                s_GenStrPtrValue(va_arg(Args,char*),&(ArgList[ArgIdx]));
                break;
            }
            if(ArgList[ArgIdx].ValueString)
                ValueLen += strlen(ArgList[ArgIdx].ValueString);
            if(ArgList[ArgIdx].ArgType > 100)//Return value
                RetValueCount += ArgList[ArgIdx].Size;
            ArgIdx++;
        }
        va_end(Args);
    }
    // Allocate the Output string
    OutSize = 1024 + (80 * ArgCount) + ValueLen;
    OutString = new char[OutSize];
    if(OutString == NULL)
        return(-3);//FIX clean up as above
    // Put Out XML String
    sprintf(OutString,
        "<AtXmlDriverFunctionCall>\n"
        "  <SignalResourceName>%s</SignalResourceName>\n"
        "  <DriverFunctionCall>\n"
        "    <FunctionName>%s</FunctionName>\n",
        ResourceName, FunctionName);
    for(ArgIdx=0; ArgIdx<ArgCount; ArgIdx++)
    {
        sprintf(&OutString[strlen(OutString)],
            "    <Parameter ParamNumber=\"%d\" ParamType=\"%s\"",
            ArgIdx, ArgList[ArgIdx].ArgXmlString);
        if(ArgList[ArgIdx].ValueString)
        {
            sprintf(&OutString[strlen(OutString)],
                " Value=\"%s\"", ArgList[ArgIdx].ValueString);
        }
        if(ArgList[ArgIdx].Size > 0)
        {
            sprintf(&OutString[strlen(OutString)],
                " Size=\"%d\"", ArgList[ArgIdx].Size);
        }
        strcat(OutString, "/>\n");
    }
    strcat(OutString,
        "  </DriverFunctionCall>\n"
        "</AtXmlDriverFunctionCall>\n");


    // Call Driver Function Call
    OutSize = 4096 + (80 * RetValueCount);
    Response = new char[OutSize+4];
    if(Response == NULL)
        return(-4);//FIX document as above
    atxml_IssueDriverFunctionCall(OutString, Response, OutSize);
    // Handle return values
    for(ArgIdx=0; ArgIdx<ArgCount; ArgIdx++)
    {
        if(ArgList[ArgIdx].ArgType < 100)
            continue;
        sprintf(RetAttrName,"Param%d",ArgIdx);
        switch(ArgList[ArgIdx].ArgType)
        {
        case DF_PARAM_TYPE_RetDbl:
            s_PutDblValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetDblPtr:
            s_PutDblPtrValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
		case DF_PARAM_TYPE_RetFltPtr:
            s_PutFltPtrValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetUInt32:
        case DF_PARAM_TYPE_RetInt32:
            s_PutInt32Value(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetUInt32Ptr:
        case DF_PARAM_TYPE_RetInt32Ptr:
            s_PutInt32PtrValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetUInt16:
        case DF_PARAM_TYPE_RetInt16:
            s_PutInt16Value(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetUInt16Ptr:
        case DF_PARAM_TYPE_RetInt16Ptr:
            s_PutInt16PtrValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetUInt8:
        case DF_PARAM_TYPE_RetInt8:
            s_PutInt8Value(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetUInt8Ptr:
        case DF_PARAM_TYPE_RetInt8Ptr:
            s_PutInt8PtrValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetBool:
            s_PutBoolValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetBoolPtr:
            s_PutBoolPtrValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        case DF_PARAM_TYPE_RetStrPtr:
            s_PutStrPtrValue(Response, RetAttrName, &(ArgList[ArgIdx]));
            break;
        }
    }
    // Clean up memory allocations
    delete(Response);
    for(ArgIdx=0; ArgIdx<ArgCount; ArgIdx++)
    {
        if(ArgList[ArgIdx].ValueString)
            delete(ArgList[ArgIdx].ValueString);
    }
    delete(ArgList);

    
    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: s_Gen...
//
// Purpose: CallDriver function XML Datavalue helper routines
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
//    void
//
///////////////////////////////////////////////////////////////////////////////

static int s_getArgType(char *String, ARGLIST *ArgPtr)
{
    char TempString[ATXML_MAX_NAME];
    int  Size=0;
    int  Fields;
    int  i=0;

    TempString[0] = '\0';
    Fields = sscanf(String," %[^, ] %d",TempString,&Size);
    if(Fields < 1)
        return(-1);
    while(s_DfParamTypeList[i].TypeString)
    {
        if(strcmp(TempString,s_DfParamTypeList[i].TypeString) == 0)
        {
            strcpy(ArgPtr->ArgXmlString, s_DfParamTypeList[i].TypeString);
            ArgPtr->ArgType = s_DfParamTypeList[i].Type;
            if(Fields == 2)
                ArgPtr->Size = Size;
            return(0);
        }
        i++;
    }

    return(-2);
}
static void s_GenDblValue(double Value,ARGLIST *ArgPtr)
{
    ArgPtr->ValueString = new char[32];
    if(ArgPtr->ValueString == NULL)
        return;
    sprintf(ArgPtr->ValueString,"%20.12e",Value);
    return;
}

static void s_GenDblPtrValue(double *Value,ARGLIST *ArgPtr)
{
    int Len;
    int i;

    if(Value == NULL)
        return;
    Len = ArgPtr->Size * 24;
    ArgPtr->ValueString = new char[Len];
    if(ArgPtr->ValueString == NULL)
        return;
    ArgPtr->ValueString[0]='\0';
    for(i=0; i<ArgPtr->Size; i++)
    {
        if(i)
            strcat(ArgPtr->ValueString," ");
        sprintf(&(ArgPtr->ValueString[strlen(ArgPtr->ValueString)]),"%20.12e",Value[i]);
    }
    return;
}

static void s_GenInt32Value(unsigned long Value,ARGLIST *ArgPtr)
{
    ArgPtr->ValueString = new char[20];
    if(ArgPtr->ValueString == NULL)
        return;
    sprintf(ArgPtr->ValueString,"0x%08x",Value);
    return;
}

static void s_GenInt32PtrValue(unsigned long *Value,ARGLIST *ArgPtr)
{
    int Len;
    int i;

    if(Value == NULL)
        return;
    Len = ArgPtr->Size * 13;
    ArgPtr->ValueString = new char[Len];
    if(ArgPtr->ValueString == NULL)
        return;
    ArgPtr->ValueString[0]='\0';
    for(i=0; i<ArgPtr->Size; i++)
    {
        if(i)
            strcat(ArgPtr->ValueString," ");
        sprintf(&(ArgPtr->ValueString[strlen(ArgPtr->ValueString)]),"0x%08x",Value[i]);
    }
    return;
}

static void s_GenInt16Value(unsigned short Value,ARGLIST *ArgPtr)
{
    ArgPtr->ValueString = new char[20];
    if(ArgPtr->ValueString == NULL)
        return;
    sprintf(ArgPtr->ValueString,"0x%04x",Value);
    return;
}

static void s_GenInt16PtrValue(unsigned short *Value,ARGLIST *ArgPtr)
{
    int Len;
    int i;

    if(Value == NULL)
        return;
    Len = ArgPtr->Size * 10;
    ArgPtr->ValueString = new char[Len];
    if(ArgPtr->ValueString == NULL)
        return;
    ArgPtr->ValueString[0]='\0';
    for(i=0; i<ArgPtr->Size; i++)
    {
        if(i)
            strcat(ArgPtr->ValueString," ");
        sprintf(&(ArgPtr->ValueString[strlen(ArgPtr->ValueString)]),"0x%04x",Value[i]);
    }
    return;
}

static void s_GenInt8Value(unsigned char Value,ARGLIST *ArgPtr)
{
    ArgPtr->ValueString = new char[20];
    if(ArgPtr->ValueString == NULL)
        return;
    sprintf(ArgPtr->ValueString,"0x%02x",Value);
    return;
}

static void s_GenInt8PtrValue(unsigned char *Value,ARGLIST *ArgPtr)
{
    int Len;
    int i;

    if(Value == NULL)
        return;
    Len = ArgPtr->Size * 8;
    ArgPtr->ValueString = new char[Len];
    if(ArgPtr->ValueString == NULL)
        return;
    ArgPtr->ValueString[0]='\0';
    for(i=0; i<ArgPtr->Size; i++)
    {
        if(i)
            strcat(ArgPtr->ValueString," ");
        sprintf(&(ArgPtr->ValueString[strlen(ArgPtr->ValueString)]),"0x%02x",Value[i]);
    }
    return;
}

static void s_GenBoolValue(bool Value,ARGLIST *ArgPtr)
{
    ArgPtr->ValueString = new char[20];
    if(ArgPtr->ValueString == NULL)
        return;
    sprintf(ArgPtr->ValueString,"%d",Value ? 1 : 0);
    return;
}

static void s_GenBoolPtrValue(bool *Value,ARGLIST *ArgPtr)
{
    int Len;
    int i;

    if(Value == NULL)
        return;
    Len = ArgPtr->Size * 8;
    ArgPtr->ValueString = new char[Len];
    if(ArgPtr->ValueString == NULL)
        return;
    ArgPtr->ValueString[0]='\0';
    for(i=0; i<ArgPtr->Size; i++)
    {
        if(i)
            strcat(ArgPtr->ValueString," ");
        sprintf(&(ArgPtr->ValueString[strlen(ArgPtr->ValueString)]),"%d",Value[i] ? 1 : 0);
    }
    return;
}

static void s_GenStrPtrValue(char *Value,ARGLIST *ArgPtr)
{
    int Len;

    if(Value == NULL)
        return;
    Len = strlen(Value) + 4;
    ArgPtr->ValueString = new char[Len];
    if(ArgPtr->ValueString == NULL)
        return;
    strcpy(ArgPtr->ValueString, Value);
    return;
}

static void s_PutDblValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    atxml_GetSingleDblValue(Response, AttrName, (double*)(ArgPtr->RetPtr));
    return;
}

static void s_PutDblPtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int Size;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    Size = ArgPtr->Size;
    atxml_GetDblArrayValue(Response, AttrName,(double*)(ArgPtr->RetPtr), &Size);
    return;
}

static void s_PutFltPtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int Size;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    Size = ArgPtr->Size;
    atxml_GetFltArrayValue(Response, AttrName,(float*)(ArgPtr->RetPtr), &Size);
    return;
}

static void s_PutInt32Value(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    atxml_GetSingleIntValue(Response, AttrName, (int*)(ArgPtr->RetPtr));
    return;
}

static void s_PutInt32PtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int Size;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    Size = ArgPtr->Size;
    atxml_GetIntArrayValue(Response, AttrName, (int*)(ArgPtr->RetPtr), &Size);
    return;
}

static void s_PutInt16Value(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int Int;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    atxml_GetSingleIntValue(Response, AttrName, &Int);
    *((unsigned short*)(ArgPtr->RetPtr)) = (unsigned short)(Int & 0xffff);
    return;
}

static void s_PutInt16PtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int *IntArray;
    int Size, i;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    Size = ArgPtr->Size;
    IntArray = new int[Size + 4];
    if(IntArray == NULL)
        return;
    if(!atxml_GetIntArrayValue(Response, AttrName, IntArray, &Size))
    {
        for(i=0; i<Size; i++)
        {
            ((unsigned short*)(ArgPtr->RetPtr))[i] = (unsigned short)(IntArray[i] & 0xffff);
        }
    }
    delete(IntArray);
    return;
}

static void s_PutInt8Value(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int Int;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    atxml_GetSingleIntValue(Response, AttrName, &Int);
    *((unsigned char*)(ArgPtr->RetPtr)) = (unsigned char)(Int & 0xff);
    return;
}

static void s_PutInt8PtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int *IntArray;
    int Size, i;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    Size = ArgPtr->Size;
    IntArray = new int[Size + 4];
    if(IntArray == NULL)
        return;
    if(!atxml_GetIntArrayValue(Response, AttrName, IntArray, &Size))
    {
        for(i=0; i<Size; i++)
        {
            ((unsigned char*)(ArgPtr->RetPtr))[i] = (unsigned char)(IntArray[i] & 0xff);
        }
    }
    delete(IntArray);
    return;
}

static void s_PutBoolValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int Int;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    atxml_GetSingleIntValue(Response, AttrName, &Int);
    *((bool*)(ArgPtr->RetPtr)) = Int ? true : false;
    return;
}

static void s_PutBoolPtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    int *IntArray;
    int Size, i;
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    Size = ArgPtr->Size;
    IntArray = new int[Size + 4];
    if(IntArray == NULL)
        return;
    if(!atxml_GetIntArrayValue(Response, AttrName, IntArray, &Size))
    {
        for(i=0; i<Size; i++)
        {
            ((bool*)(ArgPtr->RetPtr))[i] = IntArray[i] ? true : false;
        }
    }
    delete(IntArray);
    return;
}

static void s_PutStrPtrValue(char *Response, char *AttrName,ARGLIST *ArgPtr)
{
    if((ArgPtr == NULL) || (ArgPtr->RetPtr == NULL))
        return;
    atxml_GetStringValue(Response, AttrName, (char*)(ArgPtr->RetPtr), ArgPtr->Size);
    return;
}


