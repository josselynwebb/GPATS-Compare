//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Mic_T.h
//
// Date:	    26JUN06
//
// Purpose:	    Instrument Driver for Mic_TPMC806
//
// Instrument:	Mic_TPMC806  <device description> (<device type>)
//
//
// Revision History described in Mic_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
#include "CAN.h"

#define CAL_DATA_COUNT 2
#define MAX_DATA 5000
class CMic_TPMC806_T
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
    /////// Place Mic_TPMC806 Attribute structures here /////////
    AttributeStruct m_BusTimeout;
    AttributeStruct m_NoCommandTimeout;
    AttributeStruct m_NoResponseTimeout;
	AttributeStruct m_InterruptAckTimeout;
	AttributeStruct m_BaseVector;
	AttributeStruct m_AddressReg;
	AttributeStruct m_Command;
	AttributeStruct m_DataSize;
	AttributeStruct m_MaxTime;
	AttributeStruct m_Channel;
	AttributeStruct m_Attribute;
	char m_Data[MAX_DATA];
	char m_RetData[MAX_DATA];
    

public:
     CMic_TPMC806_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CMic_TPMC806_T(void);
public:
    int Open(char *VXIAddress);	// CODE_CHECK: ANTHONY ADDED THIS
	int StatusMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalMic_TPMC806(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalMic_TPMC806(ATXMLW_INTF_CALDATA* CalData);
    int ResetMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstMic_TPMC806(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsMic_TPMC806(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallMic_TPMC806(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorMic_TPMC806(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoMic_TPMC806(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateMic_TPMC806(void);
    void NullCalDataMic_TPMC806(void);
};
