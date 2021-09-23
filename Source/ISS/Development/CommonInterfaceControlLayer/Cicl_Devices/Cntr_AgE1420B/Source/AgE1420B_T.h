////////////////////////////////////////////////////////////////////////////////
// File:	    AgE1420B_T.h
//
// Date:	    14FEB06
//
// Purpose:	    Instrument Driver for AgE1420B
//
// Instrument:	AgE1420B  
//
//
// Revision History described in AgE1420B_T.cpp
//
////////////////////////////////////////////////////////////////////////////////
typedef struct AttStructStruct 
{
	bool   Exists;
	int    Int;
	int    Dim;
	double Real;
}AttributeStruct;

extern int DE_BUG;
#define	DEBUG_1420Arb		"c:/aps/data/debugit_1420Arb"
#define	DEBUGIT_1420Arb	"c:/aps/data/1420ArbDebug.txt"
#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }

#define CAL_DATA_COUNT 2
class CAgE1420B_T
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
	ATXMLW_XML_HANDLE m_SigDesc;
	char      m_SignalName[ATXMLW_MAX_NAME];
	char      m_SignalElement[ATXMLW_MAX_NAME];
	char      m_Port[ATXMLW_MAX_NAME];
	char      m_RefPort[ATXMLW_MAX_NAME];
	char      m_MeasAttr[ATXMLW_MAX_NAME];

	// Modifiers
	int             m_Atten;
	AttributeStruct m_MaxTime;
	AttributeStruct m_Coupling;
	AttributeStruct m_DcOffset;
	AttributeStruct m_EventGateFrom;
	AttributeStruct m_EventGateTo;
	AttributeStruct m_EventSlope;
	AttributeStruct m_EventTimeFrom;
	AttributeStruct m_EventTimeTo;
	AttributeStruct m_Expected;
	AttributeStruct m_Slope;
	AttributeStruct m_StrobeToEvent;
	AttributeStruct m_TestEquipImp;
	AttributeStruct m_Trig;
	AttributeStruct m_Voltage;

	bool m_Common;

	ATXMLW_STD_MEAS_INFO m_MeasInfo;

	struct event {
		bool            Exists;
		char            Port[ATXMLW_MAX_NAME];
		AttributeStruct Coupling;
		AttributeStruct SettleTime;
		AttributeStruct Slope;
		AttributeStruct TestEquipImp;
		AttributeStruct TrigVolt;
		AttributeStruct VoltMax;
	} m_Start, m_Stop, m_Sync;

public:
	 CAgE1420B_T(int Instno, int ResourceType, char* ResourceName,
			int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
			ATXMLW_INTF_RESPONSE* Response, int Buffersize);
	~CAgE1420B_T(void);
public:
	int StatusAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int IssueSignalAgE1420B(ATXMLW_INTF_SIGDESC* SignalDescription,
			ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int RegCalAgE1420B(ATXMLW_INTF_CALDATA* CalData);
	int ResetAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int IstAgE1420B(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int IssueNativeCmdsAgE1420B(ATXMLW_INTF_INSTCMD* InstrumentCommands,
			ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int IssueDriverFunctionCallAgE1420B(ATXMLW_INTF_DRVRFNC* DriverFunction,
			ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
	/////// Single Action Functionality
	int  SetupAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  EnableAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  DisableAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SaResetAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  FetchAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	////// Utility functions
	int  ErrorAgE1420B(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  GetStmtInfoAgE1420B(ATXMLW_INTF_SIGDESC* SignalDescription,
			ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void InitPrivateAgE1420B(void);
	void NullCalDataAgE1420B(void);
	void Instantaneous(ATXMLW_XML_HANDLE SigDesc, char *Name, char *In,
			AttributeStruct *Trig, AttributeStruct *Slope, char *Gate,
			ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void SigTree(ATXMLW_XML_HANDLE SigDesc, char *Name, char *Port, char *RefPort,
			AttributeStruct *SigAmp, AttributeStruct *DcOffset, AttributeStruct *Coupling,
			AttributeStruct *Impedance, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void PortSetup(char *Port, AttributeStruct Coupling,
			AttributeStruct Impedance, AttributeStruct Trigger, AttributeStruct Slope,
			ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void GateSetup(event Start, event Stop, event Sync,
		ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void SetAtten(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void SanitizeAgE1420B(void);
};


void dodebug(int code, char *function_name, char *format, ...);