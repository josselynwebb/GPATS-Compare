

#ifndef ATXMLW_WRAPPER_H
#define  ATXMLW_WRAPPER_H

#pragma warning(disable:4100)
#pragma warning(disable:4244)
#pragma warning(disable:4511)
#pragma warning(disable:4512)

#include  <string>
#include  <float.h>
#include  <windows.h>


#pragma warning(disable:4786)
#pragma warning(disable:4996)

#ifdef ATXMLW_EXPORTS
#define ATXMLW_WRAP_FNC extern "C" __declspec(dllexport)
#else
#define ATXMLW_WRAP_FNC extern "C" __declspec(dllimport)
#endif

//++++/////////////////////////////////////////////////////////////////////////
// Definitions and prototypes for AtXmlWwrappers
///////////////////////////////////////////////////////////////////////////////
// Macros
#define strnzcpy(x,y,z) {strncpy(x,y,z); x[z - 1] = '\0';}
// IFNSIM assumes the existance of m_Sim
#define IFNSIM(x) { \
                    if(!m_Sim) { x ;} \
                  }
// ATXMLW_DEBUG Assumes the existance of m_Dbg and m_ResourceName
#define ATXMLW_DEBUG(lvl, msg, resp, size)  atxmlw_DebugMsg(m_Dbg, m_ResourceName, lvl, msg, resp, size)
#define ATXMLW_ERROR(err, lead, msg, resp, size) atxmlw_ErrorResponse(m_ResourceName, resp, size, lead, err, msg) 

// ISELEMENT assumes the existance of Name = element's name attribute value,
//           and Element = <"element" .../>
#define ISELEMENT(x) (strcmp(Element,x)==0)

// ISSTRATTR assumes the existance of m_SignalDescription, Name = element's name attribute value.
#define ISSTRATTR(x,y) (atxmlw_Get1641StringAttribute(m_SignalDescription, Name, x, y))

// ISDBLATTR assumes the existance of m_SignalDescription, Name = element's name attribute value.
#define ISDBLATTR(x,y,z) (atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, x, y,z))
#define ISDBLATTRFULL(x,q,y,z,a,b,c) (atxmlw_Get1641DoubleAttributeFull(m_SignalDescription, Name,x,q,y,z,a,b,c))
//ISINTATTR assumes the existance of m_Signaldescription, Name = element's name attribute value
#define ISINTATTR(x,y) (atxmlw_Get1641IntAttribute(m_SignalDescription, Name, x, y))

// Size definitions etc.
#define MAX_BC_INST_ID     40
#define ATXMLW_MAX_NAME    56

#define ATXMLW_RESTYPE_BASE      1
#define ATXMLW_RESTYPE_PHYSICAL  2
#define ATXMLW_RESTYPE_VIRTUAL   3
// Data Structures
typedef struct INSTRUMENT_ADDRESS_STRUCT
{
    // Resource Module Address of main controller
    int   ResourceAddress;
    // ResourceMan Addressing by Instrument ID
    char  InstrumentQueryID[ATXMLW_MAX_NAME];  // Response from *IDN?
    int   InstrumentTypeNumber;
    // Visa Open type device addressing "VXI<controllerNumber>::<PrimaryAddress>:INST"
    char  ControllerType[ATXMLW_MAX_NAME];  // VXI, GPIB, Etc.
    int   ControllerNumber;   // VXI chassis number etc.
    int   PrimaryAddress;
    int   SecondaryAddress; 
    int   SubModuleAddress;
}ATXMLW_INSTRUMENT_ADDRESS;

typedef struct DeviceDataStruct
{
    int   InstNo;       // System Assigned Instrument number
    int   ResourceType;
    char  ResourceName[ATXMLW_MAX_NAME];
    ATXMLW_INSTRUMENT_ADDRESS AddressInfo;
    int   Sim;          // Simulate flag value (0-false, 1-true)
    int   Dbg;          // Debug flag value (0- 9)
    void *DriverClass;  // Place holder for class instance pointer
} ATXMLW_DEVINFO;

typedef struct DoubleVectorStruct
{
    char     Prefix[ATXMLW_MAX_NAME];
    double   Value;
    char     Unit[ATXMLW_MAX_NAME];
} ATXMLW_DBL_VECTOR;

// External data typedefs
typedef char ATXMLW_INTF_RESPONSE;
typedef char ATXMLW_XML_FILENAME;
typedef char ATXMLW_INTF_CALDATA;
typedef char ATXMLW_INTF_SIGDESC;
typedef char ATXMLW_INTF_INSTCMD;
typedef char ATXMLW_INTF_DRVRFNC;
typedef char ATXMLW_INSTCMD_RESPONSE_TYPE;

// Utility typedefs
typedef char  ATXMLW_XML_STRING;
//typedef char* ATXMLW_XML_HANDLE;
typedef void* ATXMLW_XML_HANDLE;

// Enumerated Types
// ATXMLW_IST_LVL
#define ATXMLW_IST_LVL_PST   1  // Power Up Self Test
#define ATXMLW_IST_LVL_CNF   2  // Confidence Test (Are you alive)
#define ATXMLW_IST_LVL_IST   3  // Internal Self Test (IST*)
#define ATXMLW_IST_LVL_BIT   4  // Multiple BIT levels supported as ATXMLW_IST_LVL_BIT + 1 etc.

// ATXMLW_SA  1641 Signal Description Actions
#define ATXMLW_SA_APPLY          1
#define ATXMLW_SA_REMOVE         2
#define ATXMLW_SA_MEASURE        3
#define ATXMLW_SA_READ           4
#define ATXMLW_SA_RESET          5
#define ATXMLW_SA_SETUP          6
#define ATXMLW_SA_CONNECT        7
#define ATXMLW_SA_ENABLE         8
#define ATXMLW_SA_DISABLE        9
#define ATXMLW_SA_FETCH         10
#define ATXMLW_SA_DISCONNECT    11
#define ATXMLW_SA_STATUS        12
#define ATXMLW_SA_IDENTIFY      13

#define STD_PERIODIC_RAMP         1
#define STD_PERIODIC_SINUSOID     2
#define STD_PERIODIC_SQUAREWAVE   3
#define STD_PERIODIC_TRAPEZOID    4
#define STD_PERIODIC_TRIANGLE     5
#define STD_PERIODIC_WAVEFORMRAMP 6
#define STD_PERIODIC_WAVEFORMSTEP 7

#define STD_MEAS_GENERIC        1
#define STD_MEAS_AVG            2
#define STD_MEAS_COUNTER        3
#define STD_MEAS_INST           4
#define STD_MEAS_MAX_INST       5
#define STD_MEAS_MIN_INST       6
#define STD_MEAS_PEAK           7
#define STD_MEAS_PK_NEG         8
#define STD_MEAS_PK_PK          9
#define STD_MEAS_RMS           10
#define STD_MEAS_TIME_INT      11

#define STD_TYPE_VOLTAGE        1
#define STD_TYPE_FREQ           2
#define STD_TYPE_CURRENT        3
#define STD_TYPE_TIME           4
#define STD_TYPE_PLANE_ANGLE    5
#define STD_TYPE_POWER          6
#define STD_TYPE_RES            7

typedef struct MeasCharAttrStruct
{
    double  nominal;
    char    unit[ATXMLW_MAX_NAME];
    char    condition[ATXMLW_MAX_NAME];
    int     samples;
    double  gateTime;
    char    gtUnits[ATXMLW_MAX_NAME];
}ATXML_STD_MEAS_ATTR;

typedef struct StdMeasInfoStruct
{
    bool   MeasExists;
    int    StdType;  // STD_MEAS_...
    int    yType; // -1 when non std used - value in cyType STD_TYPE_...
    int    xRefType; // -1 when non std used - value in cxRefType
    char   cyType[256];  // "attribute" value when StdType = STD_MEAS_GENERIC
    char   cxRefType[ATXMLW_MAX_NAME];
    ATXML_STD_MEAS_ATTR Attrs; //Various Attributes
    double TimeOut;
}ATXMLW_STD_MEAS_INFO;

typedef struct StdTrigInfoStruct
{
    bool   TrigExists;
    bool   TrigExt;
    char   TrigPort[ATXMLW_MAX_NAME];
    double TrigLevel;
    bool   TrigSlopePos;
    double TrigDelay;
}ATXMLW_STD_TRIG_INFO;

typedef struct StdGateInfoStruct
{
    bool   GateExists;
    bool   GateStartExt;
    char   GateStartPort[ATXMLW_MAX_NAME];
    double GateStartLevel;
    bool   GateStartSlopePos;
    double GateStartDelay;
    bool   GateStopExt;
    char   GateStopPort[ATXMLW_MAX_NAME];
    double GateStopLevel;
    bool   GateStopSlopePos;
}ATXMLW_STD_GATE_INFO;

// Wrapper Standard Error Messages
#define ATXMLW_WRAPPER_ERRCD_TOO_MANY_DEVICES   -1001
#define ATXMLW_WRAPPER_ERRMSG_TOO_MANY_DEVICES         "Initialization Error - Too many Devices"

#define ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND   -1002
#define ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND         "Device Not Found"

#define ATXMLW_WRAPPER_ERRCD_MAX_TIME           -1003
#define ATXMLW_WRAPPER_ERRMSG_MAX_TIME                 "Device Timeout"

#define ATXMLW_WRAPPER_ERRCD_INVALID_ACTION     -1004
#define ATXMLW_WRAPPER_ERRMSG_INVALID_ACTION           "Invalid Action Specified"

#define ATXMLW_WRAPPER_ERRCD_INVALID_SYSTEM_DATA -1005
#define ATXMLW_WRAPPER_ERRMSG_INVALID_SYSTEM_DATA      "Invalid System Data"

#define ATXMLW_WRAPPER_ERRCD_RANGE_ERROR         -1006
#define ATXMLW_WRAPPER_ERRMSG_RANGE_ERROR              "Parameter Range Error"

#define ATXMLW_WRAPPER_ERRCD_REGISTER_TSF_NOT_IMPLEMENTED -1100

// AtXmlWwrapperSupport library prototypes
// Functions in  AtXmlWrapperResponse.cpp
extern void    atxmlw_ScalerIntegerReturn(char *Attribute, char *Unit, int Value, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern void    atxmlw_IntegerArrayReturn(char *Attribute, char *Unit, int *Value, int Count, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern void    atxmlw_ScalerDoubleReturn(char *Attribute, char *Unit, double Value, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern void    atxmlw_DoubleArrayReturn(char *Attribute, char *Unit, double *DblArray, int Count, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern void    atxmlw_ScalerStringReturn(char *Attribute, char *Unit, char *Value, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern void    atxmlw_ErrorResponse(char *InstId, ATXMLW_INTF_RESPONSE *Response, int BufferSize, char *LeadText, int ErrCode, char *ErrMsg);
extern void    atxmlw_DebugMsg(int MemDbgLvl, char *InstId, int ReqLvl, char *Msg, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern char   *atxmlw_FmtMsg(char *Fmt,...);

// Functions in  AtXmlWrapperXmlUtil.cpp
//   -------------- 1641 Stuff --------------
extern int     atxmlw_Parse1641Xml(ATXMLW_XML_STRING *XmlString, ATXMLW_XML_HANDLE *XmlHandle, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern int     atxmlw_Close1641Xml(ATXMLW_XML_HANDLE *XmlHandle);
extern int     atxmlw_Get1641SignalAction(ATXMLW_XML_HANDLE XmlHandle, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern int     atxmlw_Get1641SignalResource(ATXMLW_XML_HANDLE XmlHandle, char*Resource, ATXMLW_INTF_RESPONSE *Response, int BufferSize);
extern bool    atxmlw_Get1641SignalOut(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Element);
extern bool    atxmlw_Get1641SignalIn(ATXMLW_XML_HANDLE XmlHandle, char **PortNames);
extern bool    atxmlw_Get1641ElementByName(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Element);
extern bool    atxmlw_Get1641DoubleAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute, double *Value, char *Unit);
extern bool    atxmlw_Get1641DoubleAttributeFull(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute, char *Prefix, double *Value, char *Unit, double *Plus, double *Minus, char *PmUnit);
extern bool    atxmlw_Get1641DoubleArrayAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute, ATXMLW_DBL_VECTOR *DblArray, int *Size);
extern bool    atxmlw_Get1641IntAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute, int *Value);
extern bool    atxmlw_Get1641IntArrayAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute, int *IntArray, int *Size);
extern bool    atxmlw_Get1641StringAttribute(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *Attribute, char **Value);
extern bool    atxmlw_Get1641StdMeasChar(ATXMLW_XML_HANDLE XmlHandle, char *Name, ATXMLW_STD_MEAS_INFO *MeasInfo);
extern bool    atxmlw_Get1641StdTrigChar(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *SigInString, ATXMLW_STD_TRIG_INFO *TrigInfo);
extern bool    atxmlw_Get1641StdGateChar(ATXMLW_XML_HANDLE XmlHandle, char *Name, char *SigInString, ATXMLW_STD_GATE_INFO *GateInfo);
extern bool    atxmlw_IsPortName(char *PortNames, char *PortName);
extern bool    atxmlw_IsPortNameTsf(ATXMLW_XML_HANDLE XmlHandle, char *PortNames, char *PortName, char **InContinue);
//   -------------- 1641 Stuff --------------

//   -------------- DeviceFunction Stuff --------------
// ATXMLW_DF Driver Function Support
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
#define DF_PARAM_TYPE_RetUInt32         31     
#define DF_PARAM_TYPE_RetUInt32Ptr     132          
#define DF_PARAM_TYPE_RetInt32         133    
#define DF_PARAM_TYPE_RetInt32Ptr      134         
#define DF_PARAM_TYPE_SrcStrPtr         35         
#define DF_PARAM_TYPE_RetStrPtr        136         
#define DF_PARAM_TYPE_SrcBool           37    
#define DF_PARAM_TYPE_SrcBoolPtr        38         
#define DF_PARAM_TYPE_RetBool          139     
#define DF_PARAM_TYPE_RetBoolPtr       140          

/*
	<AtXmlDriverFunctionCall>
		<SignalResourceName>DEVX_1</SignalResourceName>
		<SignalChannelName>Chan1</SignalChannelName>
		<DriverFunctionCall>
			<FunctionName>viIn8</FunctionName>
			<Parameter   ParamNumber="0" ParamType="RetInt32" Value=""/>
			<Parameter   ParamNumber="1" ParamType="Handle" Value=""/>
			<Parameter   ParamNumber="2" ParamType="SrcUInt16" Value="0x3B05"/>
			<Parameter   ParamNumber="3" ParamType="SrcUInt32" Value="35000"/>
			<Parameter   ParamNumber="4" ParamType="RetInt8" Value="0x3B05"/>
		</DriverFunctionCall>
	</AtXmlDriverFunctionCall>
*/

typedef union 
{
char*    PString;
double   Double;
double*  PDouble;
float    Float;
float*   PFloat;
bool     Bool;
bool*    PBool;
unsigned long   Int32;
unsigned long*  PInt32;
unsigned short  Int16;
unsigned short* PInt16;
unsigned char   Int8;
unsigned char*  PInt8;
}ATXMLW_DF_VAL; 

typedef struct DFParameterStruct
{
    int    ParamNumber;
    int    ParamType;
    ATXMLW_DF_VAL Value;
    int    Size;
}ATXMLW_DF_PARAM;

typedef struct ParsedDFStruct
{
    char   ResouceName[ATXMLW_MAX_NAME];
    char   ChannelName[ATXMLW_MAX_NAME];
    char   FunctionName[ATXMLW_MAX_NAME];
    int    DeviceHandle;
    int    ParamCount;
    ATXMLW_DF_PARAM *ParamArray;
} ATXMLW_PARSED_DF;
/**/
// ISDFNAME assumes the existance of Name = Driver Function Name.
#define ISDFNAME(x) (strcmp(Name,x)==0)
// The following functions assume the existance of DfHandle = DF Structure pointer
#define Void(x) 
#define Handle(x) (((ATXMLW_PARSED_DF*)DfHandle)->DeviceHandle)
#define SrcStrPtr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PString)
#define RetStrPtr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PString)
#define SrcDbl(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Double)
#define SrcDblPtr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PDouble)
#define RetDbl(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Double))
#define RetDblPtr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PDouble)
#define SrcFlt(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Float)
#define SrcFltPtr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PFloat)
#define RetFlt(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Float))
#define RetFltPtr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PFloat)
#define SrcUInt8(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int8)
#define SrcUInt8Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt8)
#define SrcInt8(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int8)
#define SrcInt8Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt8)
#define RetUInt8(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int8))
#define RetUInt8Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt8)
#define RetInt8(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int8))
#define RetInt8Ptr(x)(((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt8)
#define SrcBool(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Bool)
#define SrcBoolPtr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PBool)
#define RetBool(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Bool))
#define RetBoolPtr(x)(((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PBool)
#define SrcUInt16(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int16)
#define SrcUInt16Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt16)
#define SrcInt16(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int16)
#define SrcInt16Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt16)
#define RetUInt16(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int16))
#define RetUInt16Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt16)
#define RetInt16(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int16))
#define RetInt16Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt16)
#define SrcUInt32(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int32)
#define SrcUInt32Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt32)
#define SrcInt32(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int32)
#define SrcInt32Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt32)
#define RetUInt32(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int32))
#define RetUInt32Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt32)
#define RetInt32(x) &((((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.Int32))
#define RetInt32Ptr(x) (((ATXMLW_PARSED_DF*)DfHandle)->ParamArray[x].Value.PInt32)

extern int     atxmlw_ParseDriverFunction(int DeviceHandle, ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_XML_HANDLE *XmlHandle, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
extern int     atxmlw_CloseDFXml(ATXMLW_XML_HANDLE XmlHandle);
extern void    atxmlw_GetDFName(ATXMLW_XML_HANDLE DfHandle, char *FuncName);
extern void    atxmlw_ReturnDFResponse(ATXMLW_XML_HANDLE DfHandle,ATXMLW_DF_VAL RetVal, ATXMLW_INTF_RESPONSE* Response, int BufferSize);

// Functions in  AtXmlWrapperSignal.cpp
extern int     atxmlw_InstrumentCommands(int ViHandle, ATXMLW_INTF_INSTCMD *InstrumentCmds, ATXMLW_INTF_RESPONSE *Response, int BufferSize, int Dbg, int Sim);
extern bool    atxmlw_doViDrvrFunc(ATXMLW_XML_HANDLE DfHandle,char *Name,ATXMLW_DF_VAL *RetValPtr);

#endif // ATXMLW_WRAPPER_H