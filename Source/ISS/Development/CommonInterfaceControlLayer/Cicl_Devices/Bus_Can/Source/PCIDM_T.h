//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    PCIDM_T.h
//
// Date:	    26JUN06
//
// Purpose:	    Instrument Driver for IcPCIDM
//
// Instrument:	IcPCIDM  <device description> (<device type>)
//
//
// Revision History described in PCIDM_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
#include "CAN.h"

#define CAL_DATA_COUNT 2
#define MAX_DATA 5000
class CIcPCIDM_T
{
private:
    int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
    HINSTANCE m_myhDLL;
	COMM_HANDLE m_Handle;
    char      m_InitString[ATXMLW_MAX_NAME+20];
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
	//WSADATA m_wsaData;
	
    // Modifiers
    /////// Place IcPCIDM Attribute structures here /////////
    AttributeStruct m_LocalIP;
    AttributeStruct m_LocalMask;
    AttributeStruct m_LocalGateway;
	AttributeStruct m_RemoteIP;
	AttributeStruct m_RemotePort;
	AttributeStruct m_DataSize;
	AttributeStruct m_MaxTime;
	AttributeStruct m_Attribute;
	int m_TransType;
    char m_Data[MAX_DATA];
	char m_RetData[MAX_DATA];
    

public:
     CIcPCIDM_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CIcPCIDM_T(void);
public:
    int Open(char *VXIAddress);	// CODE_CHECK: ANTHONY ADDED THIS
	int StatusIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalIcPCIDM(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalIcPCIDM(ATXMLW_INTF_CALDATA* CalData);
    int ResetIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstIcPCIDM(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsIcPCIDM(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallIcPCIDM(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorIcPCIDM(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoIcPCIDM(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateIcPCIDM(void);
    void NullCalDataIcPCIDM(void);
};
