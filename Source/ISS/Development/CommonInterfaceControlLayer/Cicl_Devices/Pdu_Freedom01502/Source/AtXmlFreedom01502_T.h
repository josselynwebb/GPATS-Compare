//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlPdu_Freedom01502_T.h
//
// Date:	    07-JAN-2006
//
// Purpose:	    Instrument Driver for AtXmlPdu_Freedom01502
//
// Instrument:	AtXmlPdu_Freedom01502  Power Supplies (PDU)
//
//
// Revision History described in AtXmlPdu_Freedom01502_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

#include <time.h>
#include "visa.h"              //for type ViSession
#include "AtXmlWrapper.h"

extern int	DE_BUG;
extern FILE	*debugfp;

typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    char   Dim[ATXMLW_MAX_NAME];
    double Real;

}AttributeStruct;

#define CAL_DATA_COUNT 2
#define RESET_WAIT    2 /* Sec to wait after Reset before proceeding */
#define RESET_DELAY   4 /* Sec Delay between Reset/Setup*/

#define	DEBUG_DCPS		"c:/aps/data/debugit_dcps"
#define	DEBUGIT_DCPS	"c:/aps/data/dcpsDebug.txt"

#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }


class CAtXmlFreedom01502_Single_T
{
private:
    
	int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
	char      m_InitString[ATXMLW_MAX_NAME+20];
	int       m_ConfigType;
	int       m_ModuleNum;
	int       m_ModuleCount;
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
	char      m_MeasFunc[10];
	char      m_InputChannel[ATXMLW_MAX_NAME];  // For Sensors
    char      m_OutputChannel[ATXMLW_MAX_NAME]; // For Sources
    time_t    m_ResetTime;
	

    // Modifiers
    /////// Place AtXmlPdu_Freedom01502 Attribute structures here /////////
	//AttributeStruct m_Amplitude;
	//AttributeStruct m_Limit;
	AttributeStruct m_Measure;
    AttributeStruct m_CurrentLimit;
    AttributeStruct m_VoltageLimit;
	AttributeStruct m_Voltage;
    AttributeStruct m_Current;
	AttributeStruct m_MaxTime;

public:
     CAtXmlFreedom01502_Single_T( char *resname, int ConfigType,
		                 int ModuleNum, int modcnt, 
						 int Dbg, int Sim, ViSession Handle,
                         ATXMLW_INTF_RESPONSE* response, int bufferSize);
    ~CAtXmlFreedom01502_Single_T(void);
public:
    int StatusAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalAtXmlFreedom01502_Single(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalAtXmlFreedom01502_Single(ATXMLW_INTF_CALDATA* CalData);
    int ResetAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstAtXmlFreedom01502_Single(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsAtXmlFreedom01502_Single(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallAtXmlFreedom01502_Single(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorAtXmlFreedom01502_Single(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoAtXmlFreedom01502_Single(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateAtXmlFreedom01502_Single(void);
    void NullCalDataAtXmlFreedom01502_Single(void);
	int	 GetFirmRevAtXmlFreedom01502_Single(int ModuleNum);
	void Sanitize01502(void);
	int	CallSignalFreedom01502_Single(int action, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int WriteToDCPowerSupply(ViSession InstHandle, int DataElement1, int DataElement2, int DataElement3, int SleepValue);
};

class CAtXmlFreedom01502_Parallel_T
{
private:
    
	int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle[10];
	char      m_InitString[ATXMLW_MAX_NAME+20];
	int       m_ConfigType;
	int       m_ModuleArray[15];
	int       m_ModuleCount;
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
	char      m_MeasFunc[10];
	char      m_InputChannel[ATXMLW_MAX_NAME];  // For Sensors
    char      m_OutputChannel[ATXMLW_MAX_NAME]; // For Sources
    time_t    m_ResetTime;
	

    // Modifiers
    /////// Place AtXmlPdu_Freedom01502 Attribute structures here /////////
    //AttributeStruct m_Amplitude;
	AttributeStruct m_Measure;
    AttributeStruct m_CurrentLimit;
    AttributeStruct m_VoltageLimit;
	AttributeStruct m_Voltage;
    AttributeStruct m_Current;
	AttributeStruct m_MaxTime;

public:
     CAtXmlFreedom01502_Parallel_T( char *resname, int ConfigType,
		                 int *ModuleArr, int modcnt, 
						 int Dbg, int Sim, ViSession *Handle,
                         ATXMLW_INTF_RESPONSE* response, int bufferSize);
    ~CAtXmlFreedom01502_Parallel_T(void);
public:
    int StatusAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalAtXmlFreedom01502_Parallel(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalAtXmlFreedom01502_Parallel(ATXMLW_INTF_CALDATA* CalData);
    int ResetAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstAtXmlFreedom01502_Parallel(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsAtXmlFreedom01502_Parallel(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallAtXmlFreedom01502_Parallel(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Parallel Action Functionality
    int  SetupAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorAtXmlFreedom01502_Parallel(int Status, int Supply,ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoAtXmlFreedom01502_Parallel(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateAtXmlFreedom01502_Parallel(void);
    void NullCalDataAtXmlFreedom01502_Parallel(void);
	int	 GetFirmRevAtXmlFreedom01502_Parallel(int ModuleNum);
	int WriteToDCPowerSupply(ViSession InstHandle, int DataElement1, int DataElement2, int DataElement3, int SleepValue);
	int	CallSignalFreedom01502_Parallel(int action, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
};

void dodebug(int code, char *function_name, char *format, ...);

