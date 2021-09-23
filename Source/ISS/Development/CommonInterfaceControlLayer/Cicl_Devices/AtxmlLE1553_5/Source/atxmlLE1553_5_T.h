//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtxmlLE1553_T.h
//
// Date:	    11OCT05
//
// Purpose:	    Instrument Driver for LE1553
//
// Instrument:	LE1553  <device description> (<device type>)
//
//
// Revision History described in LE1553_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
extern int DE_BUG;
typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;

}AttributeStruct;
typedef struct FlagsandData
{
 bool Exists;
 int Integer;
 unsigned int UnInteger;
 long Long;
 unsigned long UnLong;
 double Double;
}flagsanddata;

#define CAL_DATA_COUNT 2

#define	DEBUG_1553		"c:/aps/data/debugit_1553"
#define	DEBUGIT_1553	"c:/aps/data/1553Debug.txt"
#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }

class CAtxmlLE1553_5_T
{
private:
    int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    //int       m_ResourceAddress;
		MSGADDR m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
		HCARD   m_HandleCard;
		//CJWLPHCORE  m_HCore;
		HCORE  m_HCore;
    char      m_InitString[ATXMLW_MAX_NAME+20];
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
	bool m_initialized;
	ATXMLW_STD_MEAS_INFO m_MeasInfo;
	double m_MajorFrameTime;
	flagsanddata m_controlFlags;
//	BuConf_t		 Conf;

    // Modifiers
	unsigned short m_Data[32];
	int m_SendAddress[33];
	int m_RecieveAddress[33];
    AttributeStruct m_attribute;
	AttributeStruct m_BusSpec;
	AttributeStruct m_DataSize;
	AttributeStruct m_Command[2];
	AttributeStruct m_Status;
	AttributeStruct m_BusMode;
	AttributeStruct m_Proceed;
	AttributeStruct m_Wait;
	AttributeStruct m_TestEquipRole;
	AttributeStruct m_MessageGap;
	AttributeStruct m_ResponseTime;
	AttributeStruct m_MaxTime;
	AttributeStruct m_SendAddressSize;
	AttributeStruct m_RecieveAddressSize;
	AttributeStruct m_tExnm;
	AttributeStruct m_Delay;
	AttributeStruct m_ReleaseHandle;
	int m_NumExch;
	int m_Exnm[32];
	int m_SA[32];

public:
 CAtxmlLE1553_5_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
 ~CAtxmlLE1553_5_T(void);
public:
    int StatusLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalLE1553(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalLE1553(ATXMLW_INTF_CALDATA* CalData);
    int ResetLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstLE1553(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsLE1553(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallLE1553(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
	int  CallSignalLE1553(int action, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SetupLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorLE1553(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoLE1553(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateLE1553(void);
    void NullCalDataLE1553(void);
	void Setup_BC(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_RT(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_BM(ATXMLW_INTF_RESPONSE* Response, int BufferSize);

};

void dodebug(int code, char *function_name, char *format, ...);
