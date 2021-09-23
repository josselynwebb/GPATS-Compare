//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    SBSGigabit_T.h
//
// Date:	    20FEB06
//
// Purpose:	    Instrument Driver for SBSGigabit
//
// Instrument:	SBSGigabit  <device description> (<device type>)
//
//
// Revision History described in SBSGigabit_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;
	char Address[50];

}AttributeStruct;

extern int DE_BUG;
#define	DEBUG_GIGABIT		"c:/aps/data/debugit_GIGABIT"
#define	DEBUGIT_GIGABIT	"c:/aps/data/GIGABITDebug.txt"
#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }

#define CAL_DATA_COUNT 2
#define MAX_DATA 5000
class CSBSGigabit_T
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
	WSADATA m_wsaData;
	
    // Modifiers
    /////// Place SBSGigabit Attribute structures here /////////
    AttributeStruct m_LocalIP;
    AttributeStruct m_LocalMask;
    AttributeStruct m_LocalGateway;
	AttributeStruct m_RemoteIP;
	AttributeStruct m_RemotePort;
	AttributeStruct m_DataSize;
	AttributeStruct m_MaxTime;
	AttributeStruct m_Attribute;
	AttributeStruct m_LinkSpeed;
	int m_TransType;
    char m_Data[MAX_DATA];
	char m_RetData[MAX_DATA];
    

public:
     CSBSGigabit_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CSBSGigabit_T(void);
public:
    int StatusSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalSBSGigabit(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalSBSGigabit(ATXMLW_INTF_CALDATA* CalData);
    int ResetSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstSBSGigabit(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsSBSGigabit(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallSBSGigabit(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorSBSGigabit(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoSBSGigabit(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateSBSGigabit(void);
    void NullCalDataSBSGigabit(void);
	SOCKET connect_by_name(const char * ip_or_fqdn, unsigned short port, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int send_udp(const char * ip_or_fqdn, unsigned short port, const char * send_word, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int recieve_udp(const char * ip_or_fqdn, unsigned short port, char * recieve_word, int max_len, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int set_ip(char *Interface, char *IP, char *Subnet, char *Gateway, bool DHCP=false);
	int set_LinkSpeed(char *LinkSpeed);
	SOCKET bind_local(bool TCPPORT, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
};

void dodebug(int code, char *function_name, char *format, ...);
