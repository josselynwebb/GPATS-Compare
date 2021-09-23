//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Ri3152_T.h
//
// Date:	    30JAN06
//
// Purpose:	    Instrument Driver for Ri3152
//
// Instrument:	Ri3152  <device description> (<device type>)
//
//
// Revision History described in Ri3152_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;

}AttributeStruct;

#define CAL_DATA_COUNT 2
#define MAX_MEM 64536

extern int DE_BUG;
#define	DEBUG_FGAB		"c:/aps/data/debugit_FGAB"
#define	DEBUGIT_FGAB	"c:/aps/data/fgABDebug.txt"

#define IFNSIMFG(x)	{if((!m_Sim) && (m_PowerState)) { x ;} }
#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }

class CRi3152_T
{
private:
    int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
    ViSession m_Handle;
    char      m_InitString[ATXMLW_MAX_NAME+20];
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];

    // Modifiers
    /////// Place Ri3152 Attribute structures here /////////
    AttributeStruct m_Volt;
	AttributeStruct m_DCOffset;
	AttributeStruct m_Freq;
	AttributeStruct m_Burst;
	AttributeStruct m_Bandwidth;
	AttributeStruct m_DutyCycle;
	AttributeStruct m_BurstRepRate;
	AttributeStruct m_RiseTime;
	AttributeStruct m_FallTime;
	AttributeStruct m_Exponent;
	AttributeStruct m_StimSize;
	double m_StimData[MAX_MEM];
	AttributeStruct m_SampleSpacing;
	AttributeStruct m_TestEquipImp;
	AttributeStruct m_MaxTime;
    AttributeStruct m_TriggerDelay;
    AttributeStruct m_TriggerSlope;
	AttributeStruct m_TriggerVolt;
	AttributeStruct m_TriggerDelayTo;
    AttributeStruct m_TriggerSlopeTo;
	AttributeStruct m_TriggerVoltTo;
	AttributeStruct m_PulseWidth;
	AttributeStruct m_SignalType;
	AttributeStruct m_Event;
	AttributeStruct m_Sync;

public:
     CRi3152_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CRi3152_T(void);
public:
    int StatusRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalRi3152(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalRi3152(ATXMLW_INTF_CALDATA* CalData);
    int ResetRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstRi3152(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsRi3152(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallRi3152(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorRi3152(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoRi3152(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateRi3152(void);
    void NullCalDataRi3152(void);
	void Get_Variables(char *Name, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void SanitizeRi3152(void);
};

void dodebug(int code, char *function_name, char *format, ...);