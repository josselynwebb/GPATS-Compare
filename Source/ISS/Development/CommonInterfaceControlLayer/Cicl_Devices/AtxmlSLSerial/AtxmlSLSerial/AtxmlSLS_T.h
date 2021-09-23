//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    SLS232A_T.h
//
// Date:	    13FEB06
//
// Purpose:	    Instrument Driver for SLS232A
//
// Instrument:	SLS232A  <device description> (<device type>)
//
//
// Revision History described in SLS232A_T.cpp
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

#define CAL_DATA_COUNT 2
#define MAX_DATA 10240
#define INSTOFFSET 12

#define	DEBUG_PCI		"c:/aps/data/debugit_pci"
#define	DEBUGIT_PCI		"c:/aps/data/pciDebug.txt"

#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }

typedef struct portinfo
 {
	HANDLE hport;
	bool alive;
	char comnme[10];
	char resourcenme[16];
	unsigned char intfConfig;
	unsigned long baudrate;
	COMMTIMEOUTS cts;
	DCB comStatus;
	int sizeblock;
	int numofbytes;
 }portinfo;

class CSLS232A_T
{
private:
    int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
    HANDLE    m_Handle;
    char      m_InitString[ATXMLW_MAX_NAME+20];
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];

	int		DEBUG_IT;
	FILE	*DebugitFp;

	portinfo resource;

	//variables to handle incoming data
	OVERLAPPED m_RxTransfer;
//	TCHAR m_RxTransName[30];
	char m_RxTransName[30];
	char m_RetData[MAX_DATA];
	ULONG m_RetDataCnt;
    
	//variables to handle outgoing data
	OVERLAPPED m_TxTransfer;
//	TCHAR m_TxTransName[30];
	char m_TxTransName[30];

	// Modifiers
	AttributeStruct m_BusSpec;
	char m_Data[MAX_DATA];
	AttributeStruct m_DataSize;
    AttributeStruct m_BusMode;
    AttributeStruct m_Proceed;
    AttributeStruct m_Wait;
	AttributeStruct m_BitRate;
	AttributeStruct m_StopBits;
	AttributeStruct m_Delay;
	AttributeStruct m_Parity;
	AttributeStruct m_MaxTime;
	AttributeStruct m_Exnm;
	AttributeStruct m_UutTalker;
	AttributeStruct m_UutListener;
	AttributeStruct m_TestEquipTalker;
	AttributeStruct m_TestEquipListener;
	AttributeStruct m_Terminated;
	AttributeStruct m_Attribute;
	AttributeStruct m_WordLength;
	AttributeStruct m_ReadLength;

public:
     CSLS232A_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CSLS232A_T(void);
public:
    int StatusSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalSLS232A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalSLS232A(ATXMLW_INTF_CALDATA* CalData);
    int ResetSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstSLS232A(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsSLS232A(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallSLS232A(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorSLS232A(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoSLS232A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateSLS232A(void);
    void NullCalDataSLS232A(void);
};
void dodebug(int code, char *function_name, char *format, ...);