//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Can2_T.h
//
// Date:	    3Aug16
//
// Purpose:	    Instrument Driver for Second Can_TDR010
//
// Instrument:	Can_TDR010  <device description> (<device type>)
//
//
// Revision History described in Can2_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
#include "CANs.h"
#include "tdrv010api.h"

#define CAL_DATA_COUNT 2
#define MAX_DATA 5000
class CCan2_TDR010_T
{
private:
    int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
		TDRV010_HANDLE CanHandle;
    //HINSTANCE m_myhDLL;
	COMM_HANDLE m_Handle;
    char      m_InitString[ATXMLW_MAX_NAME+20];
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
		int m_device;
	UCHAR mchannel;
	//WSADATA m_wsaData;
	
    // Modifiers
    /////// Place Can_TDR010 Attribute structures here /////////
    AttributeStruct m_TimingValue;
	AttributeStruct m_ThreeSamples;
	AttributeStruct m_SingleFilter;
	AttributeStruct m_AcceptanceCode;
	AttributeStruct m_AcceptanceMask;
	AttributeStruct m_DataSize;
	AttributeStruct m_MaxTime;
	AttributeStruct m_Channel;
	AttributeStruct m_Attribute;
	char m_Data[MAX_DATA];
	//char m_RetData[MAX_DATA];
	unsigned char* m_RetData;


public:
     CCan2_TDR010_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CCan2_TDR010_T(void);
public:
    int Open(char *VXIAddress);	// CODE_CHECK: AJP ADDED THIS
	int StatusCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalCan_TDR010(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalCan_TDR010(ATXMLW_INTF_CALDATA* CalData);
    int ResetCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstCan_TDR010(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsCan_TDR010(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallCan_TDR010(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
		int  ErrorCan_TDR010(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);

private:
    /////// Single Action Functionality
    int  SetupCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  GetStmtInfoCan_TDR010(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateCan_TDR010(void);
    void NullCalDataCan_TDR010(void);
		void bin_string_to_array(char input[], int length, int array[]);
		void array_to_bin_string(int array[], int size, int length, char output[]);
};
