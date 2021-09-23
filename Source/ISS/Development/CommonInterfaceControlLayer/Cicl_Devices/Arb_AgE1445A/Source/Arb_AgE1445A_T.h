//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Arb_AgE1445A_T.h
//
// Date:	    20JAN06
//
// Purpose:	    Instrument Driver for Arb_AgE1445A
//
// Instrument:	Arb_AgE1445A  <device description> (<device type>)
//
//
// Revision History described in Arb_AgE1445A_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
#include "visa.h"
typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    char   Dim[ATXMLW_MAX_NAME];
    double Real;

}AttributeStruct;

extern int DE_BUG;
#define	DEBUG_1445Arb		"c:/aps/data/debugit_1445Arb"
#define	DEBUGIT_1445Arb	"c:/aps/data/1445ArbDebug.txt"
#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }
#define MAX_MEM 64536
#define CAL_DATA_COUNT 2
#define MAX_DATA 262144
class CArb_AgE1445A_T
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
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
	char	  StringToInst[256]; 				// Strings to be sent to Instrument 
    char      m_OutputChannel[ATXMLW_MAX_NAME]; // For Sources
	double	  OffsetMin;
	double	  OffsetMax;
	double	  TempDcOffset;
	double	  SweepTime;
	double	  ArbWaveFequency;
	ViUInt32	  TimeOut;
    int		  Status;
	int		  SyncEvent;
	int		  Points;
	//int		  StimCount;
	int		  i;
    char	  LclUnit[ATXMLW_MAX_NAME];
	char	 *WaveForm; 		
	double	  RawData;
	//ATXMLW_DBL_VECTOR StimData [MAX_DATA];

    // Modifiers
    /////// Place Arb_AgE1445A Attribute structures here /////////
	double m_StimData[MAX_DATA];
	AttributeStruct m_StimSize;
    AttributeStruct m_AgeRate;
    AttributeStruct m_Amplitude;
	AttributeStruct m_Attenuator;
	AttributeStruct m_Bandwidth;
	AttributeStruct m_Burst;
	AttributeStruct m_BurstRepRate;
	AttributeStruct m_CenterFreq;
	AttributeStruct m_DcComp;
	AttributeStruct m_EventDelay;
	AttributeStruct m_EventSlope;
	AttributeStruct m_EventVoltage;
	AttributeStruct m_Freq;
	AttributeStruct m_FreqWindow;
	AttributeStruct m_FreqWindowMin;
    AttributeStruct m_MaxTime;
 	AttributeStruct m_Period;
 	AttributeStruct m_Periodic;
	AttributeStruct m_PulseWidth;
	AttributeStruct m_RiseTime;
	AttributeStruct m_SampleSpacing;
	AttributeStruct m_Sync;
	AttributeStruct m_TEI;

    ////// The following are standard attributes parsed by
    ////// atxmlw_Get1641StdTrigChar, atxmlw_Get1641StdGateChar, and
    ////// atxmlw_Get1641StdMeasChar respectively
    ATXMLW_STD_TRIG_INFO m_TrigInfo;
    ATXMLW_STD_GATE_INFO m_GateInfo;
 

    char   Name[ATXMLW_MAX_NAME];
    char   Element[ATXMLW_MAX_NAME];

public:
     CArb_AgE1445A_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CArb_AgE1445A_T(void);
public:
    int StatusArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalArb_AgE1445A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalArb_AgE1445A(ATXMLW_INTF_CALDATA* CalData);
    int ResetArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstArb_AgE1445A(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsArb_AgE1445A(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallArb_AgE1445A(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorArb_AgE1445A(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoArb_AgE1445A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateArb_AgE1445A(void);
    void NullCalDataArb_AgE1445A(void);
    bool GetSignalSrcChar(char *Name, char *InputNames, char *Response, int BufferSize);
	void SanitizeArb_AgE1445A(void);
};


void dodebug(int code, char *function_name, char *format, ...);
