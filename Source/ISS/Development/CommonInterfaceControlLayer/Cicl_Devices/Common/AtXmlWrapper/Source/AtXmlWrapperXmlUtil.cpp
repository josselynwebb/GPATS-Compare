///////////////////////////////////////////////////////////////////////////////
// File    : AtXmlWwrapperXmlUtil.cpp
//
// Purpose : Functions for supporting the AtXmlWwrapper interface with Xml Utilities;
//
//
// Date:	11OCT05
//
// Functions
// Name						
// ==========================================================================
// int     atxmlw_Parse1641Xml(ATXMLW_XML_STRING *XmlString, ATXMLW_XML_HANDLE *XmlHandle,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// int     atxmlw_Close1641Xml(ATXMLW_XML_HANDLE *XmlHandle);
// int     atxmlw_Get1641SignalAction(ATXMLW_XML_HANDLE *XmlHandle,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// int     atxmlw_Get1641SignalResource(ATXMLW_XML_HANDLE XmlHandle, char*Resource,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// int     atxmlw_Get1641SignalOut(ATXMLW_XML_HANDLE *XmlHandle, char *Name, char *Element,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// int     atxmlw_Get1641SignalIn(ATXMLW_XML_HANDLE *XmlHandle, char **InString,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// bool    atxmlw_Get1641DoubleAttribute(ATXMLW_XML_HANDLE *XmlHandle, char *Name, char *Attribute,
//                       double *Value, char *Unit);
// bool    atxmlw_Get1641DoubleArrayAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute,
//                       ATXMLW_INT_ARRAY *DblArray, int *Size);
// bool    atxmlw_Get1641IntAttribute(ATXMLW_XML_HANDLE *XmlHandle, char *Name, char *Attribute,
//                       double *Value, char *Unit);
// bool    atxmlw_Get1641IntArrayAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute,
//                       ATXMLW_INT_ARRAY *DblArray, int *Size);
// bool    atxmlw_Get1641StringAttribute(ATXMLW_XML_HANDLE *XmlHandle, char *Name, char *Attribute,
//                       char *Value);
// bool    atxmlw_Get1641ElementByName(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Element);
// bool    atxmlw_Get1641StdMeasChar(ATXMLW_XML_HANDLE XmlHandle, char *Name,
//                       ATXMLW_STD_MEAS_INFO *MeasInfo);
// bool    atxmlw_IsPortName(char *PortNames, char *PortName);
// bool    atxmlw_IsPortNameTsf(ATXMLW_XML_HANDLE XmlHandle, char *PortNames, char *PortName,
//                       char**InContinue);
// bool    atxmlw_Get1641StdTrigChar(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *SigInString, 
//                       ATXMLW_STD_TRIG_INFO *TrigInfo);
// bool    atxmlw_Get1641StdGateChar(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *SigInString, 
//                       ATXMLW_STD_GATE_INFO *GateInfo);
//
// int     atxmlw_ParseDriverFunction(int DeviceHandle,
//                        ATXMLW_INTF_DRVRFNC* DriverFunction,
//                        ATXMLW_XML_HANDLE *XmlHandle,
//                        ATXMLW_INTF_RESPONSE* Response, int BufferSize);
// int     atxmlw_CloseDFXml(ATXMLW_XML_HANDLE XmlHandle);
// void    atxmlw_GetDFName(ATXMLW_XML_HANDLE DfHandle, char *FuncName);
// void    atxmlw_ReturnDFResponse(ATXMLW_XML_HANDLE DfHandle,ATXMLW_DF_VAL RetVal,
//                        ATXMLW_INTF_RESPONSE* Response, int BufferSize);
// unsigned char  atxmlw_dfGetUInt8(ATXMLW_XML_HANDLE,int);
// unsigned short atxmlw_dfGetUInt16(ATXMLW_XML_HANDLE,int);
// unsigned long  atxmlw_dfGetUInt32(ATXMLW_XML_HANDLE,int);
// unsigned char* atxmlw_dfGetPUInt8(ATXMLW_XML_HANDLE,int);
//
// Revision History
// Rev	  Date                  Reason							Author
// ===  ========  =======================================  ====================
// 1.0  11OCT05   Initial baseline release.                T.G.McQuillen EADS
//
///////////////////////////////////////////////////////////////////////////////
#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>             
#include <string.h>
#include <fstream>
#include "AtXmlWrapper.h"
#import <msxml3.dll> named_guids
using namespace MSXML2;

#define PROC_GATE_START   0
#define PROC_GATE_STOP    1

// Local private structures
// 1641 Parsed structures
typedef struct Attr1641Struct
{
    char  Attribute[ATXMLW_MAX_NAME];
    char *Value;
} ATXMLW_ATTR1641;

typedef struct Signal1641Struct
{
    char Name[ATXMLW_MAX_NAME];
    char Element[ATXMLW_MAX_NAME];
    ATXMLW_ATTR1641 *AttrArray;
} ATXMLW_SIGNAL1641;

typedef struct Parsed1641Struct
{
    char   Action[ATXMLW_MAX_NAME];
    char   Resouce[ATXMLW_MAX_NAME];
    double TimeOut;
    char   SignalName[ATXMLW_MAX_NAME];
    char  *SignalPortNames;
    ATXMLW_SIGNAL1641 *SignalArray;
} ATXMLW_PARSED1641;


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




// Local Static Variables
struct MeasTypeStruct
{
    char *cType;
    int   iType;
}s_MeasType[]=
{
    "Voltage",     STD_TYPE_VOLTAGE,
    "Frequency",   STD_TYPE_FREQ,
    "Current",     STD_TYPE_CURRENT,
    "Time",        STD_TYPE_TIME,
    "Plane_Angle", STD_TYPE_PLANE_ANGLE,
    "Power",       STD_TYPE_POWER,
    "Resistance",  STD_TYPE_RES,
    NULL, 0
};

// Local Function Prototypes
static ATXMLW_SIGNAL1641* s_Find1641SignalByName(ATXMLW_PARSED1641 *Parsed1641, char *Name);
static ATXMLW_ATTR1641*   s_Find1641Attribute(ATXMLW_SIGNAL1641 *Signal1641, char *Attribute);
static void               s_ExtractDoubleInfo(char *ValueString, char *Prefix, double *Value, char *Unit,
                               double *Plus, double *Minus, char *PmUnit);
static void               s_Normalize(double *Value, char *Unit);
static void               s_ExtractIntInfo(char *ValueString, int *Value);
static bool               s_RecurseStdTrig(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               char *SigInString, ATXMLW_STD_TRIG_INFO *TrigInfo);
static bool               s_RecurseStdGate(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               char *SigInString, int StartStop, ATXMLW_STD_GATE_INFO *GateInfo);
static void               s_GetMeasAttr(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               ATXMLW_STD_MEAS_INFO *MeasInfo);
static int                s_GetDfParamType(char *RetString);
static void               s_SaveDfParamValue(int ParamType, char *ValueString,
                               ATXMLW_DF_VAL *Value, int *Size, int DeviceHandle);
static void               s_PutDfResponse(ATXMLW_DF_PARAM *ParamPtr,
                               char *Response, int BufferSize);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Parse1641Xml
//
// Purpose: Parses the XML into a structure to be queried by subsequent calls.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlString          ATXMLW_XML_SNIPPET*  String to be parsed
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to return handle
// Response           ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return:
//     0 - OK
//  ErrorCode - Error occured and registered in Response XML
//
///////////////////////////////////////////////////////////////////////////////
int atxmlw_Parse1641Xml(ATXMLW_XML_STRING *XmlString, ATXMLW_XML_HANDLE *XmlHandle,
                      ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
//  CoInitialize(NULL);	

    MSXML2::IXMLDOMDocumentPtr LclXml;
	MSXML2::IXMLDOMNodeListPtr Snippets;
	MSXML2::IXMLDOMNodePtr     Snippet;
	MSXML2::IXMLDOMNodeListPtr Elements;
	MSXML2::IXMLDOMNodePtr     Element;
	MSXML2::IXMLDOMNamedNodeMapPtr Attribs;
	MSXML2::IXMLDOMAttributePtr Attrib;
    long NumSnippets;
    long NumElements;
    long NumAttributes;
    int  SnipIdx,EleIdx,AttrIdx;
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641;
    char ElementName[ATXMLW_MAX_NAME];
    char AttributeName[ATXMLW_MAX_NAME];

    //if(*XmlHandle != NULL)
    //    atxmlw_Close1641Xml(XmlHandle);
    *XmlHandle = NULL;

    LclXml.CreateInstance("MSXML2.FreeThreadedDOMDocument");
	LclXml->async = VARIANT_FALSE;
	LclXml->loadXML( _bstr_t(XmlString));

    // Get a unique structure
    Parsed1641 = new ATXMLW_PARSED1641;
    if(Parsed1641 == NULL)
        return(-1);

    memset(Parsed1641,0,sizeof(ATXMLW_PARSED1641));
    if((Elements = LclXml->getElementsByTagName("SignalAction")) != NULL)
    {
        Elements->get_length(&NumElements);
        if(NumElements == 1)
        {
            Element = Elements->Getitem(0);
            strnzcpy(Parsed1641->Action,Element->Gettext(),ATXMLW_MAX_NAME);
        }
    }
    if((Elements = LclXml->getElementsByTagName("SignalTimeOut")) != NULL)
    {
        Elements->get_length(&NumElements);
        if(NumElements == 1)
        {
            Element = Elements->Getitem(0);
            sscanf(Element->Gettext(),"%lf",&(Parsed1641->TimeOut));
        }
    }
    if((Elements = LclXml->getElementsByTagName("SignalResourceName")) != NULL)
    {
        Elements->get_length(&NumElements);
        if(NumElements == 1)
        {
            Element = Elements->Getitem(0);
            strnzcpy(Parsed1641->Resouce,Element->Gettext(),ATXMLW_MAX_NAME);
        }
    }
    if((Elements = LclXml->getElementsByTagName("Signal")) != NULL)
    {
        Elements->get_length(&NumElements);
        if(NumElements == 1)
        {
            Element = Elements->Getitem(0);



            Attribs = Element->Getattributes();
            if(Attribs != NULL)
            {
	            Attribs->get_length(&NumAttributes);
                if(NumAttributes > 0)
                {
                    for(AttrIdx = 0; AttrIdx < NumAttributes; AttrIdx++)
                    {
			            Attrib = Attribs->Getitem(AttrIdx);
			            if(Attrib == NULL)
                            continue;
                        strnzcpy(AttributeName,Attrib->Getname(),ATXMLW_MAX_NAME);
                        //FIX s_StripNamespace(AttributePtr);
                        if(strcmp(AttributeName,"Out") == 0)
                        {
                            strnzcpy(Parsed1641->SignalName,Attrib->Gettext(),ATXMLW_MAX_NAME);
                        }
                        else if(strcmp(AttributeName,"In") == 0)
                        {
                            Parsed1641->SignalPortNames = new char[(strlen(Attrib->Gettext())+ 4)];
                            if(Parsed1641->SignalPortNames)
                                strcpy(Parsed1641->SignalPortNames,Attrib->Gettext());
                        }
                    }
                }
            }
        }
    }


    // Now Get the Signal description
	Snippets = LclXml->getElementsByTagName("Signal");
    if(Snippets == NULL)
        return(-1);
	Snippets->get_length(&NumSnippets);


    // FIX for now save string
    //*XmlHandle = (ATXMLW_XML_HANDLE)new char[(strlen(XmlString)+4)];
    //strcpy(*XmlHandle,XmlString);
    for(SnipIdx=0; SnipIdx < NumSnippets; SnipIdx++)
    {
        Snippet = Snippets->Getitem(SnipIdx);
        if(Snippet == NULL)
            continue;
        Elements = Snippet->GetchildNodes();
        if(Elements == NULL)
            continue;
	    Elements->get_length(&NumElements);
        if(NumElements > 0)
        {
            // Create Space for structure
            Parsed1641->SignalArray = new ATXMLW_SIGNAL1641[NumElements+1];
            if(Parsed1641->SignalArray == NULL)
                continue;
            Signal1641 = Parsed1641->SignalArray;
            memset(Signal1641,0,(sizeof(ATXMLW_SIGNAL1641) * (NumElements+1)));
        }
        else
            continue;
        for(EleIdx=0; EleIdx < NumElements; EleIdx++)
        {
            strcpy(Signal1641[EleIdx].Element,"X");//Dummy to catch Close delete
            Element = Elements->Getitem(EleIdx);
            if(Element == NULL)
                continue;
            strnzcpy(ElementName,Element->GetbaseName(),ATXMLW_MAX_NAME);
            //s_StripNamespace(ElementPtr);
            Attribs = Element->Getattributes();
            if(Attribs == NULL)
                continue;
	        Attribs->get_length(&NumAttributes);
            if(NumAttributes > 0)
            {
                // Create Space for structure
                Signal1641[EleIdx].AttrArray = new ATXMLW_ATTR1641[NumAttributes+1];
                if(Signal1641[EleIdx].AttrArray == NULL)
                    continue;
                memset(Signal1641[EleIdx].AttrArray,0,(sizeof(ATXMLW_ATTR1641)*(NumAttributes+1)));
            }
            else
                continue;
            for(AttrIdx = 0; AttrIdx < NumAttributes; AttrIdx++)
            {
                strcpy(Signal1641[EleIdx].AttrArray[AttrIdx].Attribute,"X");//Dummy to catch Close delete
			    Attrib = Attribs->Getitem(AttrIdx);
			    if(Attrib == NULL)
                    continue;
                strnzcpy(AttributeName,Attrib->Getname(),ATXMLW_MAX_NAME);
                //s_StripNamespace(AttributePtr);
                if(strcmp(AttributeName,"name") == 0)
                {
                    strnzcpy(Signal1641[EleIdx].Element,ElementName,ATXMLW_MAX_NAME);
                    strnzcpy(Signal1641[EleIdx].Name,Attrib->Gettext(),ATXMLW_MAX_NAME);
                }
                else
                {
                    int x = strlen(Attrib->Gettext());
                    if(x)
                    {
                        Signal1641[EleIdx].AttrArray[AttrIdx].Value = new char[x+4];
                        if(Signal1641[EleIdx].AttrArray[AttrIdx].Value == NULL)
                            continue;
                        strnzcpy(Signal1641[EleIdx].AttrArray[AttrIdx].Attribute,AttributeName,ATXMLW_MAX_NAME);
                        strcpy(Signal1641[EleIdx].AttrArray[AttrIdx].Value,Attrib->Gettext());
                    }
                }
            }
        }
    }
    if(Parsed1641 != NULL)
        *XmlHandle = (ATXMLW_XML_HANDLE)Parsed1641;

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Close1641Xml
//
// Purpose: Closes the XmlHandle.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle Handle cleared and NULLed
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//     0 - OK
//  ErrorCode - Error occured and registered in Response XML
//
///////////////////////////////////////////////////////////////////////////////
int atxmlw_Close1641Xml(ATXMLW_XML_HANDLE *XmlHandle)
{
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641 = NULL;
    ATXMLW_ATTR1641   *Attr1641;
    /*
    if(XmlHandle && *XmlHandle)
        delete(*XmlHandle);
    *XmlHandle = NULL;
    */
//  CoUninitialize();	

    if(*XmlHandle == NULL)
        return(0);
    Parsed1641 = (ATXMLW_PARSED1641*)(*XmlHandle);
    if(Parsed1641)
    {
        Signal1641 = Parsed1641->SignalArray;
        if(Parsed1641->SignalPortNames)
            delete(Parsed1641->SignalPortNames);
    }
    while(Signal1641 && Signal1641->Element[0])
    {
        Attr1641 = Signal1641->AttrArray;
        while(Attr1641 && Attr1641->Attribute[0])
        {
            if(Attr1641->Value)
                delete(Attr1641->Value);
            Attr1641++;
        }
        delete(Signal1641->AttrArray);
        Signal1641++;
    }
    if(Parsed1641->SignalArray)
        delete(Parsed1641->SignalArray);
    delete(*XmlHandle);
    *XmlHandle = NULL;

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641SignalAction
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//   + ActionCode - OK
//   - ErrorCode - Error occured and registered in Response XML
//
///////////////////////////////////////////////////////////////////////////////
int atxmlw_Get1641SignalAction(ATXMLW_XML_HANDLE XmlHandle,
                      ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
    int Action = 0;
    char ActionStr[ATXMLW_MAX_NAME];
    ATXMLW_PARSED1641 *Parsed1641;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if(Parsed1641 == NULL)
        return(-1);
    strnzcpy(ActionStr,Parsed1641->Action,ATXMLW_MAX_NAME);

    if(strcmp(ActionStr,"Apply") == 0)
        Action = ATXMLW_SA_APPLY;
    else if(strcmp(ActionStr,"Remove") == 0)
        Action = ATXMLW_SA_REMOVE;
    else if(strcmp(ActionStr,"Read") == 0)
        Action = ATXMLW_SA_READ;
    else if(strcmp(ActionStr,"Reset") == 0)
        Action = ATXMLW_SA_RESET;
    else if(strcmp(ActionStr,"Setup") == 0)
        Action = ATXMLW_SA_SETUP;
    else if(strcmp(ActionStr,"Connect") == 0)
        Action = ATXMLW_SA_CONNECT;
    else if(strcmp(ActionStr,"Status") == 0)
        Action = ATXMLW_SA_STATUS;
    else if(strcmp(ActionStr,"Enable") == 0)
        Action = ATXMLW_SA_ENABLE;
    else if(strcmp(ActionStr,"Disable") == 0)
        Action = ATXMLW_SA_DISABLE;
    else if(strcmp(ActionStr,"Fetch") == 0)
        Action = ATXMLW_SA_FETCH;
    else if(strcmp(ActionStr,"Disconnect") == 0)
        Action = ATXMLW_SA_DISCONNECT;
    else if(strcmp(ActionStr,"Measure") == 0)
        Action = ATXMLW_SA_MEASURE;
    else if(strcmp(ActionStr,"Identify") == 0)
        Action = ATXMLW_SA_IDENTIFY;
    else
        Action = ATXMLW_SA_IDENTIFY;

    return(Action);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641SignalResource
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Resource           char*                Pointer to return Resource[ATXMLW_MAX_NAME]
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//           0 - OK
//   ErrorCode - Error occured and registered in Response XML
//
///////////////////////////////////////////////////////////////////////////////
int     atxmlw_Get1641SignalResource(ATXMLW_XML_HANDLE XmlHandle, char*Resource,
                             ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
    int Status = 0;
    ATXMLW_PARSED1641 *Parsed1641;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if(Parsed1641 == NULL)
        return(-1);
    strnzcpy(Resource,Parsed1641->Resouce,ATXMLW_MAX_NAME);

    return(Status);
}
///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641DriverComponent
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Component          char*                Return Component
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//   + ActionCode - OK
//   - ErrorCode - Error occured and registered in Response XML
//
///////////////////////////////////////////////////////////////////////////////
int     atxmlw_Get1641DriverComponent(ATXMLW_XML_HANDLE XmlHandle, char *Component,
                             ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641SignalOut
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Name               char*                Return Main Signal Name
// Element            char*                Return Main Signal Element
//
// Return:
//   True  - Found Element
//   False - Element not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641SignalOut(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Element)
{
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if((Parsed1641 == NULL) || (Name == NULL) || (Element == NULL))
        return(false);
    strcpy(Name,Parsed1641->SignalName);
    if((Signal1641 = s_Find1641SignalByName(Parsed1641, Name)))
    {
        strcpy(Element,Signal1641->Element);
    }
    else
    {
        Element[0] = '\0';
        return(false);
    }

    return(true);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641SignalIn
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml. Get the port names in the "In"
//          attribute of the Signal element
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// InString           char**               pointer to return "In" string pointer
//
// Return:
//   True  - Found Element
//   False - Element not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641SignalIn(ATXMLW_XML_HANDLE XmlHandle, char **InString)
{
    ATXMLW_PARSED1641 *Parsed1641;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if((Parsed1641 == NULL) || (InString == NULL))
        return(false);

    *InString = Parsed1641->SignalPortNames;
    if(*InString == '\0')
        return(false);
    return(true);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641ElementByName
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
// Name               char*                Name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Element            char*                Pointer to return the Element name
//
// Return:
//   True  - Found Element
//   False - Element not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641ElementByName(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Element)
{
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641;
    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if((Parsed1641 == NULL) || (Name == NULL) || (Element == NULL))
        return(false);

    Element[0] = '\0';
    if((Signal1641 = s_Find1641SignalByName(Parsed1641, Name)))
    {
        strcpy(Element,Signal1641->Element);
        return(true);
    }
    return(false);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641DoubleAttribute
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
// Name               char*                Name to lookup
// Attribute          char*                Attribute of name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              Double*              Pointer to return double value normalized to
//                                            base units.
// Unit               char*                Pointer to return the Units
//
// Return:
//   True  - Found Attribute
//   False - Attribute not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641DoubleAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute,
                                         double *Value, char *Unit)
{
    return(atxmlw_Get1641DoubleAttributeFull(XmlHandle, Name, Attribute,
                             NULL, Value, Unit,
                             NULL, NULL, NULL));
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641DoubleAttribute
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
// Name               char*                Name to lookup
// Attribute          char*                Attribute of name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              Double*              Pointer to return double value normalized to
//                                            base units.
// Unit               char*                Pointer to return the Units
//
// Return:
//   True  - Found Attribute
//   False - Attribute not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641DoubleAttributeFull(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute,
                             char *Prefix, double *Value, char *Unit,
                             double *Plus, double *Minus, char *PmUnit)
{
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641;
    ATXMLW_ATTR1641   *Attr1641;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if((Parsed1641 == NULL) || (Name == NULL) || (Attribute == NULL) ||
       (Value == NULL))
        return(false);
    if((Signal1641 = s_Find1641SignalByName(Parsed1641, Name)))
    {
        if((Attr1641 = s_Find1641Attribute(Signal1641, Attribute)))
        {
            s_ExtractDoubleInfo(Attr1641->Value, Prefix, Value, Unit,
                              Plus, Minus, PmUnit);
            return(true);
        }
    }

    return(false);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641DoubleArrayAttribute
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
// Name               char*                Name to lookup
// Attribute          char*                Attribute of name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              Double*              Pointer to return double value normalized to
//                                            base units.
// Unit               char*                Pointer to return the Units
//
// Return:
//   True  - Found Attribute
//   False - Attribute not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641DoubleArrayAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute,
                                         ATXMLW_DBL_VECTOR *DblArray, int *Size)
{
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641;
    ATXMLW_ATTR1641   *Attr1641;
    char              *cptr, *endcptr;
    int                x;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if(Parsed1641 == NULL)
        return(false);
    if((Signal1641 = s_Find1641SignalByName(Parsed1641, Name)))
    {
        if((Attr1641 = s_Find1641Attribute(Signal1641, Attribute)))
        {
            cptr = Attr1641->Value;
            x = 0;
            // Skip beginning ']'
            if(cptr && (*cptr == '['))
                cptr++;
            while(cptr && *cptr && x<*Size && (*cptr != ']'))
            {
                endcptr = strstr(cptr,",");
                if(endcptr)
                    *endcptr = '\0';
                s_ExtractDoubleInfo(cptr, DblArray[x].Prefix, &DblArray[x].Value, DblArray[x].Unit,
                            NULL, NULL, NULL);
                x++;
                cptr = endcptr;
                if(cptr)
                {
                    *cptr = ',';
                    cptr++;
                }
            }
            *Size = x;
            return(true);
        }
    }

    return(false);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641IntAttribute
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
// Name               char*                Name to lookup
// Attribute          char*                Attribute of name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              int*                 Pointer to return double value normalized to
//                                            base units.
// Unit               char*                Pointer to return the Units
//
// Return:
//   True  - Found Attribute
//   False - Attribute not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641IntAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute,
                                         int *Value)
{
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641;
    ATXMLW_ATTR1641   *Attr1641;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if((Parsed1641 == NULL) || (Name == NULL) || (Attribute == NULL) ||
       (Value == NULL))
        return(false);
    if((Signal1641 = s_Find1641SignalByName(Parsed1641, Name)))
    {
        if((Attr1641 = s_Find1641Attribute(Signal1641, Attribute)))
        {
            s_ExtractIntInfo(Attr1641->Value, Value);
            return(true);
        }
    }

    return(false);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641IntArrayAttribute
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
// Name               char*                Name to lookup
// Attribute          char*                Attribute of name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              int*                 Pointer to return double value normalized to
//                                            base units.
// Unit               char*                Pointer to return the Units
//
// Return:
//   True  - Found Attribute
//   False - Attribute not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641IntArrayAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute,
                                         int *IntArray, int *Size)
{
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641;
    ATXMLW_ATTR1641   *Attr1641;
    char              *cptr, *endcptr;
    int                x;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if(Parsed1641 == NULL)
        return(false);
    if((Signal1641 = s_Find1641SignalByName(Parsed1641, Name)))
    {
        if((Attr1641 = s_Find1641Attribute(Signal1641, Attribute)))
        {
            cptr = Attr1641->Value;
            x = 0;
            // Skip beginning ']'
            if(cptr && (*cptr == '['))
                cptr++;
            while(cptr && *cptr && x<*Size && (*cptr != ']'))
            {
                endcptr = strstr(cptr,",");
                if(endcptr)
                    *endcptr = '\0';
                s_ExtractIntInfo(cptr, &IntArray[x]);
                x++;
                cptr = endcptr;
                if(cptr)
                {
                    *cptr = ',';
                    cptr++;
                }
            }
            *Size = x;
            return(true);
        }
    }

    return(false);
}



///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641StringAttribute
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle
// Name               char*                Name to lookup
// Attribute          char*                Attribute of name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              char**               Pointer to return attribute string pointer value
//
// Return:
//   True  - Found Attribute
//   False - Attribute not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641StringAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute,
                                         char **Value)
{
    ATXMLW_PARSED1641 *Parsed1641;
    ATXMLW_SIGNAL1641 *Signal1641;
    ATXMLW_ATTR1641   *Attr1641;

    Parsed1641 = (ATXMLW_PARSED1641*)XmlHandle;
    if(Parsed1641 == NULL)
        return(false);
    if((Signal1641 = s_Find1641SignalByName(Parsed1641, Name)))
    {
        if((Attr1641 = s_Find1641Attribute(Signal1641, Attribute)))
        {
            *Value = Attr1641->Value;
                return(true);
        }
    }

    return(false);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641StdMeasChar
//
// Purpose: Parse the 1641 for Measured Characterists 
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// m_SignalDescription ATXMLW_XML_HANDLE   Pointer to the parsed 1641 structure
// Name                char*               Element name attribute value
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// MeasInfo           ATXMLW_STD_MEAS_INFO* Pointer to Meas info struct to fill
//
// Return: 
//      true  Is a sensor statement
//      false Is a source statement
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641StdMeasChar(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               ATXMLW_STD_MEAS_INFO *MeasInfo)
{
    bool  RetVal=true;
    char  Element[ATXMLW_MAX_NAME];
    char *TempPtr;

    Element[0] = '\0';

    if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
        MeasInfo->MeasExists = true;
		if(ISELEMENT("Measure"))
		{
            if(ISSTRATTR("attribute",&TempPtr))
            {
                MeasInfo->StdType = STD_MEAS_GENERIC;
                strnzcpy(MeasInfo->cyType,TempPtr,256);
                s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
            }
		}
		else if(ISELEMENT("Average"))
        {
            MeasInfo->StdType = STD_MEAS_AVG;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("Counter"))
        {
            MeasInfo->StdType = STD_MEAS_COUNTER;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("Instantaneous"))
        {
            MeasInfo->StdType = STD_MEAS_INST;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("MaxInstantaneous"))
        {
            MeasInfo->StdType = STD_MEAS_MAX_INST;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("MinInstantaneous"))
        {
            MeasInfo->StdType = STD_MEAS_MIN_INST;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("Peak"))
        {
            MeasInfo->StdType = STD_MEAS_PEAK;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("PeakNeg"))
        {
            MeasInfo->StdType = STD_MEAS_PK_NEG;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("PeakToPeak"))
        {
            MeasInfo->StdType = STD_MEAS_PK_PK;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("RMS"))
        {
            MeasInfo->StdType = STD_MEAS_RMS;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
		else if(ISELEMENT("TimeInterval"))
        {
            MeasInfo->StdType = STD_MEAS_TIME_INT;
            s_GetMeasAttr(m_SignalDescription, Name, MeasInfo);
        }
        else
        {
            RetVal=false;
            MeasInfo->MeasExists = false;
        }
    }
    else
    {
        RetVal=false;
    }
    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641StdTrigChar
//
// Purpose: Parse the 1641 for trigger settings starting with Name
//          Trig-Source, Trig-level, Trig-Slope, etc.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// m_SignalDescription ATXMLW_XML_HANDLE   Pointer to the parsed 1641 structure
// Name                char*               Element name attribute value
// SigInString         char*               <Signal.../> "In" attribute value used to
//                                            identify the port when found.
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// TrigInfo           ATXMLW_STD_TRIG_INFO* Pointer to trig info struct to fill
//
// Return: 
//      true  Found trigger parameters
//      false Trigger parameters not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641StdTrigChar(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               char *SigInString, ATXMLW_STD_TRIG_INFO *TrigInfo)
{
    bool RetVal;
    RetVal = s_RecurseStdTrig(m_SignalDescription,Name,SigInString,TrigInfo);
    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_Get1641StdGateChar
//
// Purpose: Parse the 1641 for trigger settings starting with Name
//          Gate-Start-Source, Gate-Start-level, Gate-Start-Slope, etc.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// m_SignalDescription ATXMLW_XML_HANDLE   Pointer to the parsed 1641 structure
// Name                char*               Element name attribute value
// SigInString         char*               <Signal.../> "In" attribute value used to
//                                            identify the port when found.
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// GateInfo           ATXMLW_STD_GATE_INFO* Pointer to gate info struct to fill
//
// Return: 
//      true  Found gate parameters
//      false Gate parameters not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_Get1641StdGateChar(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               char *SigInString, ATXMLW_STD_GATE_INFO *GateInfo)
{
    bool RetVal;
    RetVal = s_RecurseStdGate(m_SignalDescription,Name,SigInString,PROC_GATE_START,GateInfo);
    return(RetVal);
}


///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_IsPortName
//
// Purpose: Validates that PortName is in the blank separated list (PortNames)
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// PortNames          char*                Blank separated list of names
// PortName           char*                Single Port name to match
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: 
//      true  Found PortName
//      false PortName not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_IsPortName(char *PortNames, char *PortName)
{
    char *cptr;
    char  OneName[ATXMLW_MAX_NAME];

    if((PortNames == NULL) || (PortNames == NULL))
        return(false);

    cptr = PortNames;
    while(cptr && (sscanf(cptr,"%s",OneName)== 1))
    {
        if(strcmp(OneName,PortName)== 0)
            return(true);
        cptr = strstr(cptr," ");
        if(cptr)cptr++;// skip blank
    }
    return(false);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_IsPortNameTsf
//
// Purpose: Validates that PortName is in the blank separated list (PortNames)or
//          by use of the atXml:port specifier
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// m_SignalDescription ATXMLW_XML_HANDLE   Pointer to the parsed 1641 structure
// PortNames          char*                Blank separated list of names
// PortName           char*                Single Port name to match
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// InContinue         char**                String returned for "In" on atxml:Port TSF
//
// Return: 
//      true  Found PortName
//      false PortName not found
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_IsPortNameTsf(ATXMLW_XML_HANDLE m_SignalDescription,
                       char *PortNames, char *Name, char **InContinue)
{
    char *cptr;
    char  OneName[ATXMLW_MAX_NAME];
    char  Element[ATXMLW_MAX_NAME];
    bool  RetVal = false;
    char *TempPtr;
    
    if(InContinue)
        *InContinue = NULL;
    if((PortNames == NULL) || (Name == NULL))
        return(false);

    cptr = PortNames;
    while(cptr && (sscanf(cptr,"%s",OneName)== 1))
    {
        if(strcmp(OneName,Name)== 0)
        {
            RetVal = true;
            break;
        }
        cptr = strstr(cptr," ");
        if(cptr)cptr++;// skip blank
    }
    if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
        if(ISELEMENT("port"))
        {
            RetVal = true;
            if((ISSTRATTR("In",&TempPtr)) && InContinue)
                *InContinue = TempPtr;
        }
    }
    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_ParseDriverFunction
//
// Purpose: Parses the Driver XML into a structure to be queried by subsequent calls.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// DeviceHandle       int                  Device Handle to return with "Handle" type
// DriverFunction     ATXMLW_INTF_DRVRFNC* String to be parsed
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to return handle
// Response           ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return:
//     0 - OK
//  ErrorCode - Error occured and registered in Response XML
//
///////////////////////////////////////////////////////////////////////////////
int atxmlw_ParseDriverFunction(int DeviceHandle,
                             ATXMLW_INTF_DRVRFNC* DriverFunction,
                             ATXMLW_XML_HANDLE *XmlHandle,
                             ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
//	CoInitialize(NULL);	

    int Status=0;
    MSXML2::IXMLDOMDocumentPtr LclXml;
	MSXML2::IXMLDOMNodeListPtr Params;
	MSXML2::IXMLDOMNodePtr     Param;
	MSXML2::IXMLDOMNodeListPtr Elements;
	MSXML2::IXMLDOMNodePtr     Element;
	MSXML2::IXMLDOMNamedNodeMapPtr Attribs;
	MSXML2::IXMLDOMAttributePtr Attrib;
    long NumParams;
    long NumElements;
    long NumAttributes;
    int  ParamIdx,AttrIdx;
    ATXMLW_PARSED_DF *ParsedDf;
    ATXMLW_DF_PARAM  *ParamDf;
    ATXMLW_DF_PARAM   TempDfParam;
    char AttributeName[ATXMLW_MAX_NAME];
    char *ValueString = NULL;

    //if(*XmlHandle != NULL)
    //    atxmlw_CloseDfXml(XmlHandle);
    *XmlHandle = NULL;

    LclXml.CreateInstance("MSXML2.FreeThreadedDOMDocument");
	LclXml->async = VARIANT_FALSE;
	LclXml->loadXML( _bstr_t(DriverFunction));

    // Get a unique structure
    ParsedDf = new ATXMLW_PARSED_DF;
    if(ParsedDf == NULL)
        return(-1);
    memset(ParsedDf,0,sizeof(ATXMLW_PARSED_DF));

    ParsedDf->DeviceHandle = DeviceHandle;

    if((Elements = LclXml->getElementsByTagName("SignalResourceName")) != NULL)
    {
        Elements->get_length(&NumElements);
        if(NumElements == 1)
        {
            Element = Elements->Getitem(0);
            strnzcpy(ParsedDf->ResouceName,Element->Gettext(),ATXMLW_MAX_NAME);
        }
    }
    if((Elements = LclXml->getElementsByTagName("SignalChannelName")) != NULL)
    {
        Elements->get_length(&NumElements);
        if(NumElements == 1)
        {
            Element = Elements->Getitem(0);
            strnzcpy(ParsedDf->ChannelName,Element->Gettext(),ATXMLW_MAX_NAME);
        }
    }
    if((Elements = LclXml->getElementsByTagName("FunctionName")) != NULL)
    {
        Elements->get_length(&NumElements);
        if(NumElements == 1)
        {
            Element = Elements->Getitem(0);
            strnzcpy(ParsedDf->FunctionName,Element->Gettext(),ATXMLW_MAX_NAME);
        }
    }


    // Now Get the Parameters
	Params = LclXml->getElementsByTagName("Parameter");
    if(Params == NULL)
        return(-1); //FIX Diagnose
	Params->get_length(&NumParams);


    if(NumParams > 0)
    {
        // Create Space for pramater structure
        ParsedDf->ParamArray = new ATXMLW_DF_PARAM[NumParams+1];
        if(ParsedDf->ParamArray == NULL)
            return(-2); //FIX Diagnose
        ParsedDf->ParamCount = NumParams;
        ParamDf = ParsedDf->ParamArray;
        memset(ParamDf,0,(sizeof(ATXMLW_DF_PARAM) * (NumParams+1)));
    }

    // Now Get Parameter attrbiutes
    for(ParamIdx=0; ParamIdx < NumParams; ParamIdx++)
    {
        Element = Params->Getitem(ParamIdx);
        if(Element == NULL)
            continue;
        Attribs = Element->Getattributes();
        if(Attribs == NULL)
            continue;
	    Attribs->get_length(&NumAttributes);
        if(NumAttributes <= 0)
            continue;
        // Reset TempDfParam
        memset(&TempDfParam,0,sizeof(ATXMLW_DF_PARAM));
        // Null the Value String for re-use
        if(ValueString)
        {
            delete(ValueString);
            ValueString = NULL;
        }
        for(AttrIdx = 0; AttrIdx < NumAttributes; AttrIdx++)
        {
			Attrib = Attribs->Getitem(AttrIdx);
			if(Attrib == NULL)
                continue;
            strnzcpy(AttributeName,Attrib->Getname(),ATXMLW_MAX_NAME);
            //s_StripNamespace(AttributePtr);
            if(strcmp(AttributeName,"ParamNumber") == 0)
            {
                TempDfParam.ParamNumber = atoi(Attrib->Gettext());
            }
            else if(strcmp(AttributeName,"ParamType") == 0)
            {
                TempDfParam.ParamType = 
                          s_GetDfParamType(Attrib->Gettext());
            }
            else if(strcmp(AttributeName,"Value") == 0)
            {
                ValueString = new char[strlen(Attrib->Gettext()) + 4];
                strcpy(ValueString,Attrib->Gettext());
            }
            else if(strcmp(AttributeName,"Size") == 0)
            {
                TempDfParam.Size = atoi(Attrib->Gettext());
            }
        }
        if(TempDfParam.ParamType &&
           (TempDfParam.ParamNumber >= 0) &&
           (TempDfParam.ParamNumber < NumParams))
        {
            // Sort by Param Number
            int x = TempDfParam.ParamNumber;
            ParamDf[x].ParamNumber = TempDfParam.ParamNumber;
            ParamDf[x].ParamType = TempDfParam.ParamType;
            ParamDf[x].Size = TempDfParam.Size;
            s_SaveDfParamValue(ParamDf[x].ParamType,ValueString,
                                &(ParamDf[x].Value), 
                                &(ParamDf[x].Size), DeviceHandle);
        }
    }
    if(ParsedDf != NULL)
        *XmlHandle = (ATXMLW_XML_HANDLE)ParsedDf;

    if(ValueString)
        delete ValueString;

    return(Status);
}
///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_CloseDFXml
//
// Purpose: Closes the XmlHandle.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// XmlHandle          ATXMLW_XML_HANDEL*   Pointer to Handle Handle cleared and NULLed
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//     0 - OK
//  ErrorCode - Error occured and registered in Response XML
//
///////////////////////////////////////////////////////////////////////////////
int atxmlw_CloseDFXml(ATXMLW_XML_HANDLE XmlHandle)
{
    int  Status = 0;
    ATXMLW_DF_PARAM *PAptr;
    int Count, i;

// 	CoUninitialize();	

    PAptr = ((ATXMLW_PARSED_DF*)XmlHandle)->ParamArray;
    Count = ((ATXMLW_PARSED_DF*)XmlHandle)->ParamCount;
    i = 0;
    while(PAptr && (i<Count))
    {
        switch(PAptr->ParamType)
        {
        case DF_PARAM_TYPE_SrcDblPtr:
        case DF_PARAM_TYPE_RetDblPtr:      
        case DF_PARAM_TYPE_SrcFltPtr:
        case DF_PARAM_TYPE_RetFltPtr:
        case DF_PARAM_TYPE_SrcBoolPtr:
        case DF_PARAM_TYPE_RetBoolPtr:
        case DF_PARAM_TYPE_SrcUInt8Ptr:
        case DF_PARAM_TYPE_SrcInt8Ptr:
        case DF_PARAM_TYPE_RetUInt8Ptr:
        case DF_PARAM_TYPE_RetInt8Ptr:
        case DF_PARAM_TYPE_SrcUInt16Ptr:
        case DF_PARAM_TYPE_SrcInt16Ptr:
        case DF_PARAM_TYPE_RetUInt16Ptr:
        case DF_PARAM_TYPE_RetInt16Ptr:
        case DF_PARAM_TYPE_SrcUInt32Ptr:
        case DF_PARAM_TYPE_SrcInt32Ptr:
        case DF_PARAM_TYPE_RetUInt32Ptr:
        case DF_PARAM_TYPE_RetInt32Ptr:
        case DF_PARAM_TYPE_SrcStrPtr:
        case DF_PARAM_TYPE_RetStrPtr:
            if((void*)(PAptr->Value.PDouble))
                delete((void*)(PAptr->Value.PDouble));
        }
        PAptr++;
        i++;
    }
    if(((ATXMLW_PARSED_DF*)XmlHandle)->ParamArray)
        delete(((ATXMLW_PARSED_DF*)XmlHandle)->ParamArray);
    if(XmlHandle)
        delete(XmlHandle);

    return(Status);
}
///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_... Driver Function Support Functions
//
// Purpose: Passing parameters to driver functions.
//
///////////////////////////////////////////////////////////////////////////////
void atxmlw_GetDFName(ATXMLW_XML_HANDLE DfHandle, char *FuncName)
{
    if(DfHandle)
    {
        strnzcpy(FuncName,((ATXMLW_PARSED_DF*)DfHandle)->FunctionName,ATXMLW_MAX_NAME);
    }
    return;
}

void atxmlw_ReturnDFResponse(ATXMLW_XML_HANDLE DfHandle,ATXMLW_DF_VAL RetValue,
                             ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    ATXMLW_DF_PARAM  *ParamPtr;
    int Count, i;

    if(DfHandle == NULL)
        return;
    // Process Return Parameters
    ParamPtr = ((ATXMLW_PARSED_DF*)DfHandle)->ParamArray;
    Count = ((ATXMLW_PARSED_DF*)DfHandle)->ParamCount;
    i = 0;
    while(ParamPtr && (i<Count))
    {
        // Check for return value
        if(ParamPtr->ParamNumber == 0)
        {
            ParamPtr->Value = RetValue;
        }
char *cptr = &Response[strlen(Response)];
        if((ParamPtr->ParamType > 100) || (ParamPtr->ParamType == 31))
            s_PutDfResponse(ParamPtr, Response, BufferSize);
        ParamPtr++;
        i++;
    }

    return;
}

unsigned char atxmlw_dfGetUInt8(ATXMLW_XML_HANDLE,int)
{
    unsigned char RetVal=0;


    return(RetVal);
}

unsigned short atxmlw_dfGetUInt16(ATXMLW_XML_HANDLE,int)
{
    unsigned short RetVal=0;


    return(RetVal);
}

unsigned long  atxmlw_dfGetUInt32(ATXMLW_XML_HANDLE,int)
{
    unsigned long RetVal=0;


    return(RetVal);
}

unsigned char* atxmlw_dfGetPUInt8(ATXMLW_XML_HANDLE,int)
{
    unsigned char *RetVal=NULL;


    return(RetVal);
}

unsigned short* atxmlw_dfGetPUInt16(ATXMLW_XML_HANDLE,int)
{
    unsigned short *RetVal=NULL;


    return(RetVal);
}


unsigned long* atxmlw_dfGetPUInt32(ATXMLW_XML_HANDLE,int)
{
    unsigned long *RetVal=NULL;


    return(RetVal);
}


char atxmlw_dfGetInt8(ATXMLW_XML_HANDLE,int)
{
    char RetVal=0;


    return(RetVal);
}


short atxmlw_dfGetInt16(ATXMLW_XML_HANDLE,int)
{
    short RetVal=0;


    return(RetVal);
}


long atxmlw_dfGetInt32(ATXMLW_XML_HANDLE,int)
{
    long RetVal=0;


    return(RetVal);
}

char* atxmlw_dfGetPInt8(ATXMLW_XML_HANDLE,int)
{
    char *RetVal=NULL;


    return(RetVal);
}

short* atxmlw_dfGetPInt16(ATXMLW_XML_HANDLE,int)
{
    short *RetVal=NULL;


    return(RetVal);
}

long* atxmlw_dfGetPInt32(ATXMLW_XML_HANDLE,int)
{
    long *RetVal=NULL;


    return(RetVal);
}

char* atxmlw_dfGetPString(ATXMLW_XML_HANDLE,int)
{
    char *RetVal=NULL;


    return(RetVal);
}

double atxmlw_dfGetDouble(ATXMLW_XML_HANDLE,int)
{
    double RetVal=0.0;


    return(RetVal);
}

double* atxmlw_dfGetPDouble(ATXMLW_XML_HANDLE,int)
{
    double *RetVal=NULL;


    return(RetVal);
}

float atxmlw_dfGetFloat(ATXMLW_XML_HANDLE,int)
{
    float RetVal=0.0;


    return(RetVal);
}

float* atxmlw_dfGetPFloat(ATXMLW_XML_HANDLE,int)
{
    float *RetVal=NULL;


    return(RetVal);
}


//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: s_Find1641SignalByName
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Parsed1641         ATXMLW_PARSED1641*   Pointer to Handle
// Name               char*                Name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              char*                Pointer to return attribute string value
//
// Return:
//   True  - Found Attribute
//   False - Attribute not found
//
///////////////////////////////////////////////////////////////////////////////
static ATXMLW_SIGNAL1641* s_Find1641SignalByName(ATXMLW_PARSED1641 *Parsed1641, char *Name)
{
    ATXMLW_SIGNAL1641 *Signal1641;

    Signal1641 = Parsed1641->SignalArray;

    while(Signal1641 && Signal1641->Element[0])
    {
        if(strcmp(Signal1641->Name,Name)==0)
            return(Signal1641);
        Signal1641++;
    }
    return(NULL);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_Find1641Attribute
//
// Purpose: Subsequent call to atxmlw_Parse1641Xml.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Signal1641         ATXMLW_SIGNAL1641*   Pointer to Handle
// Attribute          char*                Attribute of name to lookup
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              char*                Pointer to return attribute string value
//
// Return:
//   True  - Found Attribute
//   False - Attribute not found
//
///////////////////////////////////////////////////////////////////////////////
static ATXMLW_ATTR1641* s_Find1641Attribute(ATXMLW_SIGNAL1641 *Signal1641, char *Attribute)
{
    ATXMLW_ATTR1641 *Attr1641;

    Attr1641 = Signal1641->AttrArray;

    while(Attr1641 && Attr1641->Attribute[0])
    {
        if(strcmp(Attr1641->Attribute,Attribute)==0)
            return(Attr1641);
        Attr1641++;
    }
    return(NULL);
}


///////////////////////////////////////////////////////////////////////////////
// Function: s_ExtractDoubleInfo
//
// Purpose: Helper function to extract info from a XML double Value string.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// ValueString        char*                The value retrieved from the XML attribute
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// PreFix              char*               Pointer to return a prefix string value (e.g.Pk-Pk)
// Value               double*             Pointer to return normalized double value
// Unit                char*               Pointer to return unit text without the multiplier
// Plus                double*             Pointer to return Plus errlmt double value
// Minus               double*             Pointer to return Minus errlmt double value
// PmUnit              char*               Pointer to return errlmt unit text without the multiplier
//
// Return:
//   None
//
///////////////////////////////////////////////////////////////////////////////
static void s_ExtractDoubleInfo(char *ValueString, char *Prefix, double *Value, char *Unit,
                                double *Plus, double *Minus, char *PmUnit)
{
    char   LclPrefix[ATXMLW_MAX_NAME];
    char   LclString[256];
    char  *cptr,*ErrLmt;

    if(!Value || !Unit)
        return;

    if(Prefix)
        Prefix[0] = '\0';
    *Value = 0.0;
    if(Unit)
        Unit[0] = '\0';
    if(Plus)
        *Plus = 0.0;
    if(Minus)
        *Minus = 0.0;
    if(PmUnit)
        PmUnit[0] = '\0';

    LclPrefix[0] = '\0';
    LclPrefix[51] = '\0';
    LclString[0] = '\0';
    LclString[251] = '\0';
    if(sscanf(ValueString,"%50s %lf %250[^\"]",LclPrefix, Value,LclString) != 3)
    {
        LclPrefix[0]= '\0';
        if(sscanf(ValueString,"%lf %250[^\"]",Value,LclString) <= 0)
        {
            return;
        }
    }
    if(Prefix)
        strncpy(Prefix,LclPrefix,50);

    if(Plus && Minus && PmUnit)
    {
        ErrLmt = LclString;
        if((cptr = strstr(LclString,"errlmt")))
        {
            ErrLmt = cptr + 6;
            *cptr = '\0';
        }
        if((cptr = strstr(ErrLmt,"+-")))
        {
            sscanf(cptr,"+- %lf %50s",Plus,PmUnit);
            s_Normalize(Plus,PmUnit);
            *Minus = *Plus;
            *cptr = '\0';
        }
        else if((cptr = strstr(ErrLmt,"+")))
        {
            sscanf(cptr,"+ %lf %50s",Plus,PmUnit);
            s_Normalize(Plus,PmUnit);
            *cptr++ = '\0';
            ErrLmt = cptr;
        }
        if((cptr = strstr(ErrLmt," -")))
        {
            sscanf(cptr," - %lf %50s",Minus,PmUnit);
            s_Normalize(Minus,PmUnit);
            *cptr = '\0';
        }
    }
    strnzcpy(Unit,LclString,50);
    s_Normalize(Value,Unit);

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_Normalize
//
// Purpose: Helper function to extract info from a XML double Value string.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              double*              The value to apply the multiplier to
// Unit               char*                The unit text with the multiplier
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value               double*             Pointer to return normalized double value
// Unit                char*               Pointer to return unit text without the multiplier
//
// Return:
//   None
//
///////////////////////////////////////////////////////////////////////////////
static void s_Normalize(double *Value, char *Unit)
{
    double Multiplier = 1.0;

    // do normalization to base units
    if(Unit && Unit[0])
    {
        // watch for trailing ']'
        if(Unit[strlen(Unit)-1] == ']')
            Unit[strlen(Unit)-1] = '\0';
        switch(tolower(Unit[0]))
        {
        case 'p':
            if(tolower(Unit[1]) == 'c')// check for percent "pc"
                break;
            Multiplier = 1.0E-12;
            break;
        case 'n':
            Multiplier = 1.0E-9;
            break;
        case 'm':
            if(Unit[0] == 'M')
                Multiplier = 1.0E+6;
            else
                Multiplier = 1.0E-3;
            break;
        case 'k':
            Multiplier = 1.0E+3;
            break;
        case 'g':
            Multiplier = 1.0E+9;
            break;
        default:
            Multiplier = 1.0;
        }
        if(Unit[0] && (Multiplier != 1.0))
        {
            Unit[0] = ' ';
            *Value = *Value * Multiplier;
        }
    }
}
///////////////////////////////////////////////////////////////////////////////
// Function: s_ExtractIntInfo
//
// Purpose: Helper function to extract info from a XML integer Value string.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// ValueString        char*                The value retrieved from the XML attribute
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Value              int*                 Pointer to return normalized int value (32bit binary)
//
// Return:
//   None
//
///////////////////////////////////////////////////////////////////////////////
static void s_ExtractIntInfo(char *ValueString, int *Value)
{
    if(!Value)
        return;

    *Value = 0;

    sscanf(ValueString,"%i",Value);
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_RecurseStdTrig
//
// Purpose: Parse the 1641 for trigger settings starting with Name
//          Trig-Source, Trig-level, Trig-Slope, etc.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// m_SignalDescription ATXMLW_XML_HANDLE   Pointer to the parsed 1641 structure
// Name                char*               Element name attribute value
// SigInString         char*               <Signal.../> "In" attribute value used to
//                                            identify the port when found.
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// TrigInfo           ATXMLW_STD_TRIG_INFO* Pointer to trig info struct to fill
//
// Return: 
//      true  Found trigger parameters
//      false Trigger parameters not found
//
///////////////////////////////////////////////////////////////////////////////
static bool s_RecurseStdTrig(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               char *SigInString, ATXMLW_STD_TRIG_INFO *TrigInfo)
{
    bool  RetVal=true;
    char  Element[ATXMLW_MAX_NAME];
    char  Unit[ATXMLW_MAX_NAME];
    char *TempPtr;

    Element[0] = '\0';
    Unit[0] = '\0';

    if(atxmlw_IsPortNameTsf(m_SignalDescription,SigInString,Name,NULL))
    {
        //Found the Input Port Name
		TrigInfo->TrigExists = true; // just incase this is the primary "In"
        strnzcpy(TrigInfo->TrigPort,Name,ATXMLW_MAX_NAME);
        return(RetVal);
    }
    if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
		TrigInfo->TrigExists = true;
		//Used to determine the base signal type
		if(ISELEMENT("SignalDelay"))
		{
            ISDBLATTR("delay",&TrigInfo->TrigDelay,Unit);
		}
		else if(ISELEMENT("Instantaneous"))
		{
            ISDBLATTR("nominal", &TrigInfo->TrigLevel, Unit);
            if(ISSTRATTR("condition", &TempPtr))
            {
                if((strcmp(TempPtr,"LT")==0) ||
                   (strcmp(TempPtr,"LE")==0))
                   TrigInfo->TrigSlopePos = false;
                else
                   TrigInfo->TrigSlopePos = true;
            }
		}
        // Find the next signal name or port name
        if(ISSTRATTR("In",&TempPtr))
        {
            s_RecurseStdTrig(m_SignalDescription, TempPtr,
                               SigInString, TrigInfo);
        } 
    }
    else
    {
        // Unknown name ??
        RetVal=false;
    }

    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_RecurseStdGate
//
// Purpose: Parse the 1641 for trigger settings starting with Name
//          Gate-Start-Source, Gate-Start-level, Gate-Start-Slope, etc.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// m_SignalDescription ATXMLW_XML_HANDLE   Pointer to the parsed 1641 structure
// Name                char*               Element name attribute value
// SigInString         char*               <Signal.../> "In" attribute value used to
// StartStop           int                 0 = Processing Start Signal,
//                                         1 = Processing Stop Signal
//                                            identify the port when found.
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// GateInfo           ATXMLW_STD_GATE_INFO* Pointer to gate info struct to fill
//
// Return: 
//      true  Found gate parameters
//      false Gate parameters not found
//
///////////////////////////////////////////////////////////////////////////////
static bool s_RecurseStdGate(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               char *SigInString, int StartStop, ATXMLW_STD_GATE_INFO *GateInfo)
{
    bool   RetVal=true;
    char   Element[ATXMLW_MAX_NAME];
    double GateLevel;
    char   Unit[ATXMLW_MAX_NAME];
    char   Port[ATXMLW_MAX_NAME], Port2[ATXMLW_MAX_NAME];
    int    PortCount;
    char  *TempPtr;

    Element[0] = '\0';
    Unit[0] = '\0';

    if(atxmlw_IsPortNameTsf(m_SignalDescription,SigInString,Name,NULL))
    {
        //Found the Input Port Name
		GateInfo->GateExists = true; // just incase this is the primary "In"
        if(StartStop==PROC_GATE_START)
            {strnzcpy(GateInfo->GateStartPort,Name,ATXMLW_MAX_NAME);}
        else
            {strnzcpy(GateInfo->GateStopPort,Name,ATXMLW_MAX_NAME);}
        return(RetVal);
    }
    if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
		GateInfo->GateExists = true;
		if(ISELEMENT("Instantaneous"))
		{
            if(ISDBLATTR("nominal", &GateLevel, Unit))
                if(StartStop==PROC_GATE_START)
                    GateInfo->GateStartLevel = GateLevel;
                else
                    GateInfo->GateStopLevel = GateLevel;

            if(ISSTRATTR("condition", &TempPtr))
            {
                if((strcmp(TempPtr,"LT")==0) ||
                   (strcmp(TempPtr,"LE")==0))
                {
                    if(StartStop==PROC_GATE_START)
                        GateInfo->GateStartSlopePos = false;
                    else
                        GateInfo->GateStopSlopePos = false;
                 }
                 else
                 {
                    if(StartStop==PROC_GATE_START)
                        GateInfo->GateStartSlopePos = true;
                    else
                        GateInfo->GateStopSlopePos = true;
                 }
            }
		}
		if((StartStop==PROC_GATE_START) && (ISELEMENT("EventedEvent")))
		{
            if(ISSTRATTR("In",&TempPtr))
            {
                PortCount = sscanf(TempPtr,"%s %s",Port,Port2);
                if(PortCount == 2)
                {
                    s_RecurseStdGate(m_SignalDescription, Port,
                                 SigInString, PROC_GATE_START, GateInfo);
                    s_RecurseStdGate(m_SignalDescription, Port2,
                                 SigInString, PROC_GATE_STOP, GateInfo);
                }
                if(PortCount == 1)
                {
                    s_RecurseStdGate(m_SignalDescription, Port,
                                 SigInString, PROC_GATE_START, GateInfo);
                }
                return(RetVal); // Avoid processing "In" twice
            }
		}
		else if((StartStop==PROC_GATE_START) && (ISELEMENT("SignalDelay")))
		{
            ISDBLATTR("delay",&GateInfo->GateStartDelay,Unit);
		}
        // Find the next signal name or port name
        if(ISSTRATTR("In",&TempPtr))
        {
            s_RecurseStdGate(m_SignalDescription, TempPtr,
                               SigInString, StartStop, GateInfo);
        } 
    }
    else
    {
        // Unknown name ??
        RetVal=false;
    }

    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: s_GetMeasAttr
//
// Purpose: Parse the 1641 for Intrinsic measurement attributes
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// m_SignalDescription ATXMLW_XML_HANDLE   Pointer to the parsed 1641 structure
// Name                char*               Element name attribute value
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// MeasInfo           ATXMLW_STD_MEAS_INFO* Pointer to gate info struct to fill
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
static void s_GetMeasAttr(ATXMLW_XML_HANDLE m_SignalDescription, char *Name,
                               ATXMLW_STD_MEAS_INFO *MeasInfo)
{
    bool   RetVal=true;
    char   *StrValue;
    double DblValue;
    char   Unit[ATXMLW_MAX_NAME];
    int    i;

    if(ISSTRATTR("type",&StrValue))
    {
        // get constant that represents the type
        for(i=0; s_MeasType[i].cType; i++)
        {
            if(strncmp(s_MeasType[i].cType,StrValue,ATXMLW_MAX_NAME)==0)
            {
                MeasInfo->yType = s_MeasType[i].iType;
                break;
            }
        }
        if(s_MeasType[i].cType == NULL)
        {
            MeasInfo->yType = -1;
            strnzcpy(MeasInfo->cyType,StrValue,ATXMLW_MAX_NAME);
        }
    }
    if(ISSTRATTR("refType",&StrValue))
    {
        // get constant that represents the type
        for(i=0; s_MeasType[i].cType; i++)
        {
            if(strncmp(s_MeasType[i].cType,StrValue,ATXMLW_MAX_NAME)==0)
            {
                MeasInfo->xRefType = s_MeasType[i].iType;
                break;
            }
        }
        if(s_MeasType[i].cType == NULL)
        {
            MeasInfo->xRefType = -1;
            strnzcpy(MeasInfo->cxRefType,StrValue,ATXMLW_MAX_NAME);
        }
    }
    if(ISDBLATTR("maxTime",&DblValue,Unit))
    {
        MeasInfo->TimeOut = DblValue;
    }
    else
    {
        MeasInfo->TimeOut = ((ATXMLW_PARSED1641*)m_SignalDescription)->TimeOut;
    }
    if(ISDBLATTR("nominal",&DblValue,Unit))
    {
            MeasInfo->Attrs.nominal = DblValue;
            strnzcpy(MeasInfo->Attrs.unit,Unit,ATXMLW_MAX_NAME);
    }
    if(ISDBLATTR("gateTime",&DblValue,Unit))
    {
            MeasInfo->Attrs.gateTime = DblValue;
            strnzcpy(MeasInfo->Attrs.gtUnits,Unit,ATXMLW_MAX_NAME);
    }
    if(ISDBLATTR("condition",&DblValue,Unit))
    {
            strnzcpy(MeasInfo->Attrs.condition,Unit,ATXMLW_MAX_NAME);
    }
    if(ISDBLATTR("samples",&DblValue,Unit))
    {
            MeasInfo->Attrs.samples = (int)DblValue;
    }

    return;
}


static int s_GetDfParamType(char *RetString)
{
    int RetType = 0;
    int i;

    for(i=0; s_DfParamTypeList[i].TypeString; i++)
    {
        if(strcmp(RetString,s_DfParamTypeList[i].TypeString)==0)
        {
            RetType = s_DfParamTypeList[i].Type;
        }
    }

    return(RetType);
}

static void s_SaveDfParamValue(int ParamType, char *ValueString,
                                   ATXMLW_DF_VAL *Value, int *Size,
								   int DeviceHandle)
{
    char   *cptr;
    double *dblptr;
    float  *fltptr;
    bool   *boolptr;
    unsigned char *int8ptr;
    unsigned short  *int16ptr;
    unsigned long   *int32ptr;
    int     Count, i;
    long    Long;

    Value->PString = NULL;
    switch(ParamType)
    {
    case DF_PARAM_TYPE_Void:
        break;
    case DF_PARAM_TYPE_Handle:
        Value->Int32 = DeviceHandle;
        break;
    case DF_PARAM_TYPE_RetDbl:
    case DF_PARAM_TYPE_RetFlt:
    case DF_PARAM_TYPE_RetBool:
    case DF_PARAM_TYPE_RetUInt8:
    case DF_PARAM_TYPE_RetInt8:
    case DF_PARAM_TYPE_RetUInt16:
    case DF_PARAM_TYPE_RetInt16:
    case DF_PARAM_TYPE_RetUInt32:
    case DF_PARAM_TYPE_RetInt32:
        // already allocated in Value
        break;
    case DF_PARAM_TYPE_SrcDbl:
        Value->Double = atof(ValueString);
        break;
    case DF_PARAM_TYPE_SrcDblPtr:
        // How many
        if(*Size > 0)
            Count = *Size;
        else
        {
            cptr = ValueString;
            Count = 0;
            if(Count <= 0)
            while(cptr && (*cptr != '\0'))
            {
                Count++;
                cptr = strstr(cptr," ");
                while(cptr && (*cptr == ' '))cptr++;
            }
        }
        // Allocate space
        Value->PDouble = new double[Count+1];
        // Get values
        cptr = ValueString;
        dblptr = Value->PDouble;
        i = 0;
        while(cptr && (*cptr != '\0') && (i<Count))
        {
            i++;
            *dblptr++ = atof(cptr);
            cptr = strstr(cptr," ");
            while(cptr && (*cptr == ' '))cptr++;
        }
        *Size = Count;
        break;
    case DF_PARAM_TYPE_RetDblPtr:       
        // How many
        Count = *Size;
        // Allocate space
        Value->PDouble = new double[Count+1];
        break;
    case DF_PARAM_TYPE_SrcFlt: 
        Value->Float = (float)atof(ValueString);
        break;
    case DF_PARAM_TYPE_SrcFltPtr:
       // How many
        if(*Size > 0)
            Count = *Size;
        else
        {
            cptr = ValueString;
            Count = 0;
            if(Count <= 0)
            while(cptr && (*cptr != '\0'))
            {
                Count++;
                cptr = strstr(cptr," ");
                while(cptr && (*cptr == ' '))cptr++;
            }
        }
        // Allocate space
        Value->PFloat = new float[Count+1];
        // Get values
        cptr = ValueString;
        fltptr = Value->PFloat;
        i = 0;
        while(cptr && (*cptr != '\0') && (i<Count))
        {
            i++;
            *fltptr++ = (float)atof(cptr);
            cptr = strstr(cptr," ");
            while(cptr && (*cptr == ' '))cptr++;
        }
        *Size = Count;
        break;
    case DF_PARAM_TYPE_RetFltPtr:
        // How many
        Count = *Size;
        // Allocate space
        Value->PFloat = new float[Count+1];
        break;
    case DF_PARAM_TYPE_SrcBool:
        sscanf(ValueString,"%i",&Long);
        Value->Bool = Long ? true : false;
        break;
    case DF_PARAM_TYPE_SrcBoolPtr:
       // How many
        if(*Size > 0)
            Count = *Size;
        else
        {
            cptr = ValueString;
            Count = 0;
            if(Count <= 0)
            while(cptr && (*cptr != '\0'))
            {
                Count++;
                cptr = strstr(cptr," ");
                while(cptr && (*cptr == ' '))cptr++;
            }
        }
        // Allocate space
        Value->PBool = new bool[Count+1];
        // Get values
        cptr = ValueString;
        boolptr = Value->PBool;
        i = 0;
        while(cptr && (*cptr != '\0') && (i<Count))
        {
            i++;
            sscanf(cptr,"%i",&Long);
            *boolptr++ = Long ? true : false;
            cptr = strstr(cptr," ");
            while(cptr && (*cptr == ' '))cptr++;
        }
        *Size = Count;
        break;
    case DF_PARAM_TYPE_RetBoolPtr:
        // How many
        Count = *Size;
        // Allocate space
        Value->PBool = new bool[Count+1];
        break;
    case DF_PARAM_TYPE_SrcUInt8:
    case DF_PARAM_TYPE_SrcInt8:
        sscanf(ValueString,"%i",&Long);
        Value->Int8 = (char)Long;
        break;
    case DF_PARAM_TYPE_SrcUInt8Ptr:
    case DF_PARAM_TYPE_SrcInt8Ptr:
       // How many
        if(*Size > 0)
            Count = *Size;
        else
        {
            cptr = ValueString;
            Count = 0;
            if(Count <= 0)
            while(cptr && (*cptr != '\0'))
            {
                Count++;
                cptr = strstr(cptr," ");
                while(cptr && (*cptr == ' '))cptr++;
            }
        }
        // Allocate space
        Value->PInt8 = new unsigned char[Count+1];
        // Get values
        cptr = ValueString;
        int8ptr = Value->PInt8;
        i = 0;
        while(cptr && (*cptr != '\0') && (i<Count))
        {
            i++;
            sscanf(cptr,"%i",&Long);
            *int8ptr++ = (char)Long;
            cptr = strstr(cptr," ");
            while(cptr && (*cptr == ' '))cptr++;
        }
        *Size = Count;
        break;
    case DF_PARAM_TYPE_RetUInt8Ptr:
    case DF_PARAM_TYPE_RetInt8Ptr:
        // How many
        Count = *Size;
        // Allocate space
        Value->PInt8 = new unsigned char[Count+1];
        break;
    case DF_PARAM_TYPE_SrcUInt16:
    case DF_PARAM_TYPE_SrcInt16:
        sscanf(ValueString,"%i",&Long);
        Value->Int16 = (short)Long;
        break;
    case DF_PARAM_TYPE_SrcUInt16Ptr:
    case DF_PARAM_TYPE_SrcInt16Ptr:
       // How many
        if(*Size > 0)
            Count = *Size;
        else
        {
            cptr = ValueString;
            Count = 0;
            if(Count <= 0)
            while(cptr && (*cptr != '\0'))
            {
                Count++;
                cptr = strstr(cptr," ");
                while(cptr && (*cptr == ' '))cptr++;
            }
        }
        // Allocate space
        Value->PInt16 = new unsigned short[Count+1];
        // Get values
        cptr = ValueString;
        int16ptr = Value->PInt16;
        i = 0;
        while(cptr && (*cptr != '\0') && (i<Count))
        {
            i++;
            sscanf(cptr,"%i",&Long);
            *int16ptr++ = (short)Long;
            cptr = strstr(cptr," ");
            while(cptr && (*cptr == ' '))cptr++;
        }
        *Size = Count;
        break;
    case DF_PARAM_TYPE_RetUInt16Ptr:
    case DF_PARAM_TYPE_RetInt16Ptr:
        // How many
        Count = *Size;
        // Allocate space
        Value->PInt16 = new unsigned short[Count+1];
        break;
    case DF_PARAM_TYPE_SrcUInt32:
    case DF_PARAM_TYPE_SrcInt32:
        sscanf(ValueString,"%i",&Long);
        Value->Int32 = Long;
        break;
    case DF_PARAM_TYPE_SrcUInt32Ptr:
    case DF_PARAM_TYPE_SrcInt32Ptr:
       // How many
        if(*Size > 0)
            Count = *Size;
        else
        {
            cptr = ValueString;
            Count = 0;
            if(Count <= 0)
            while(cptr && (*cptr != '\0'))
            {
                Count++;
                cptr = strstr(cptr," ");
                while(cptr && (*cptr == ' '))cptr++;
            }
        }
        // Allocate space
        Value->PInt32 = new unsigned long[Count+1];
        // Get values
        cptr = ValueString;
        int32ptr = Value->PInt32;
        i = 0;
        while(cptr && (*cptr != '\0') && (i<Count))
        {
            i++;
            sscanf(cptr,"%i",&Long);
            *int32ptr++ = Long;
            cptr = strstr(cptr," ");
            while(cptr && (*cptr == ' '))cptr++;
        }
        *Size = Count;
        break;
    case DF_PARAM_TYPE_RetUInt32Ptr:
    case DF_PARAM_TYPE_RetInt32Ptr:
        // How many
        Count = *Size;
        // Allocate space
        Value->PInt32 = new unsigned long[Count+1];
        break;
    case DF_PARAM_TYPE_SrcStrPtr:
       // How many
        if(*Size > 0)
            Count = *Size;
        else
        {
            Count = strlen(ValueString) + 4;
        }
        // Allocate space
        Value->PString = new char[Count];
        // Get value
        strnzcpy(Value->PString,ValueString,Count);
        *Size = Count;
        break;
    case DF_PARAM_TYPE_RetStrPtr:
        // How many
        Count = *Size;
        // Allocate space
        Value->PString = new char[Count+1];
        if(Value->PString)
            Value->PString[0] = '\0';
        break;
    }
}

static void s_PutDfResponse(ATXMLW_DF_PARAM *ParamPtr, char *Response, int BufferSize)
{
    char ParamName[ATXMLW_MAX_NAME];
    double *DblArray;
    int  *IntArray;
    int   Size, i;

    sprintf(ParamName,"Param%d",ParamPtr->ParamNumber);
    Size = ParamPtr->Size;

    switch(ParamPtr->ParamType)
    {
    case DF_PARAM_TYPE_RetDbl:
        atxmlw_ScalerDoubleReturn(ParamName, NULL, ParamPtr->Value.Double,
                             Response, BufferSize);
        break;
    case DF_PARAM_TYPE_RetFlt:
        atxmlw_ScalerDoubleReturn(ParamName, NULL, ParamPtr->Value.Float,
                             Response, BufferSize);
        break;
    case DF_PARAM_TYPE_RetBool:
        atxmlw_ScalerIntegerReturn(ParamName, NULL, (ParamPtr->Value.Bool)? 1 : 0,
                             Response, BufferSize);
        break;
    case DF_PARAM_TYPE_RetUInt8:
    case DF_PARAM_TYPE_RetInt8:
        atxmlw_ScalerIntegerReturn(ParamName, NULL, ParamPtr->Value.Int8,
                             Response, BufferSize);
        break;
    case DF_PARAM_TYPE_RetUInt16:
    case DF_PARAM_TYPE_RetInt16:
        atxmlw_ScalerIntegerReturn(ParamName, NULL, ParamPtr->Value.Int16,
                             Response, BufferSize);
        break;
    case DF_PARAM_TYPE_RetUInt32:
    case DF_PARAM_TYPE_RetInt32:
        atxmlw_ScalerIntegerReturn(ParamName, NULL, ParamPtr->Value.Int32,
                             Response, BufferSize);
        break;
    case DF_PARAM_TYPE_RetDblPtr:      
        // First build DoubleVector Array
        DblArray = new double[Size+4];
        if(DblArray == NULL)
            break;
        memset(DblArray,0,sizeof(double) * (Size));
        for(i=0; i<Size; i++)
        {
            DblArray[i] = ParamPtr->Value.PDouble[i];
        }
        atxmlw_DoubleArrayReturn(ParamName, "", 
                             DblArray, Size, 
                             Response, BufferSize);
        delete(DblArray);
        break;
    case DF_PARAM_TYPE_RetFltPtr:
        // First build DoubleVector Array
        DblArray = new double[Size+4];
        if(DblArray == NULL)
            break;
        memset(DblArray,0,sizeof(double) * (Size));
        for(i=0; i<Size; i++)
        {
            DblArray[i] = ParamPtr->Value.PFloat[i];
        }
        atxmlw_DoubleArrayReturn(ParamName, "",
                             DblArray, Size,
                             Response, BufferSize);
        delete(DblArray);
        break;
    case DF_PARAM_TYPE_RetBoolPtr:
        // First build Int Array
        IntArray = new int[Size+4];
        if(IntArray == NULL)
            break;
        for(i=0; i<Size; i++)
        {
            IntArray[i] = (ParamPtr->Value.PBool[i])? 1 : 0;
        }
        atxmlw_IntegerArrayReturn(ParamName, NULL, 
                             IntArray, Size,
                             Response, BufferSize);
        delete(IntArray);
        break;
    case DF_PARAM_TYPE_RetUInt8Ptr:
    case DF_PARAM_TYPE_RetInt8Ptr:
        // First build Int Array
        IntArray = new int[Size+4];
        if(IntArray == NULL)
            break;
        for(i=0; i<Size; i++)
        {
            IntArray[i] = ParamPtr->Value.PInt8[i];
        }
        atxmlw_IntegerArrayReturn(ParamName, NULL, 
                             IntArray, Size,
                             Response, BufferSize);
        delete(IntArray);
        break;
    case DF_PARAM_TYPE_RetUInt16Ptr:
    case DF_PARAM_TYPE_RetInt16Ptr:
        // First build Int Array
        IntArray = new int[Size+4];
        if(IntArray == NULL)
            break;
        for(i=0; i<Size; i++)
        {
            IntArray[i] = ParamPtr->Value.PInt16[i];
        }
        atxmlw_IntegerArrayReturn(ParamName, NULL, 
                             IntArray, Size,
                             Response, BufferSize);
        delete(IntArray);
        break;
    case DF_PARAM_TYPE_RetUInt32Ptr:
    case DF_PARAM_TYPE_RetInt32Ptr:
        atxmlw_IntegerArrayReturn(ParamName, NULL, 
                             (int*)(ParamPtr->Value.PInt32), Size,
                             Response, BufferSize);
        break;
    case DF_PARAM_TYPE_RetStrPtr:
        atxmlw_ScalerStringReturn(ParamName, NULL, ParamPtr->Value.PString,
                             Response, BufferSize);
        break;
    }
    return;
}
