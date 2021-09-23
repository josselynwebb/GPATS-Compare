//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    CDDI_T.h
//
// Date:	    26JUN06
//
// Purpose:	    Instrument Driver for C212SysCDDI
//
// Instrument:	C212SysCDDI  <device description> (<device type>)
//
//
// Revision History described in CDDI_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
#include "CAN.h"

#define CAL_DATA_COUNT 2
#define MAX_DATA 5000
class CC212SysCDDI_T
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
    /////// Place C212SysCDDI Attribute structures here /////////
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
     CC212SysCDDI_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CC212SysCDDI_T(void);
public:
    int Open(char *VXIAddress);	// CODE_CHECK: ANTHONY ADDED THIS
	int StatusC212SysCDDI(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalC212SysCDDI(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalC212SysCDDI(ATXMLW_INTF_CALDATA* CalData);
    int ResetC212SysCDDI(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstC212SysCDDI(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsC212SysCDDI(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallC212SysCDDI(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupC212SysCDDI(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableC212SysCDDI(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableC212SysCDDI(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetC212SysCDDI(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchC212SysCDDI(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorC212SysCDDI(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoC212SysCDDI(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateC212SysCDDI(void);
    void NullCalDataC212SysCDDI(void);
};
