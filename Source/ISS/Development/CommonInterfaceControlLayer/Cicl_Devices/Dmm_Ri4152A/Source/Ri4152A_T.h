//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Ri4152A_T.h
//
// Date:	    11OCT05
//
// Purpose:	    Instrument Driver for Ri4152A
//
// Instrument:	Ri4152A  <device description> (<device type>)
//
//
// Revision History described in Ri4152A_T.cpp
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
class CRi4152A_T
{
private:
	int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
    char      m_InitString[ATXMLW_MAX_NAME+20];

    ViSession m_Handle;
    double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
	char      m_Attribute[10];

    // Modifiers
	AttributeStruct m_Volt;
    AttributeStruct m_RefVolt;
    AttributeStruct m_SampleWidth;
    AttributeStruct m_Bandwidth;
    AttributeStruct m_DCOffset;
	AttributeStruct m_VoltRatio;
	AttributeStruct m_Res;
    AttributeStruct m_RefRes;
    AttributeStruct m_SampleCount;
    AttributeStruct m_Freq;
    AttributeStruct m_Current;
    AttributeStruct m_Period;
    AttributeStruct m_AvVolt;
    AttributeStruct m_AvCurrent;
    AttributeStruct m_ACComp;
    AttributeStruct m_ACCompFreq;
	AttributeStruct m_MaxTime;
	AttributeStruct m_EventSampleCount;
	AttributeStruct m_Delay;
	AttributeStruct m_EventDelay;
	

public:
     CRi4152A_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CRi4152A_T(void);
public:
    int StatusRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalRi4152A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalRi4152A(ATXMLW_INTF_CALDATA* CalData);
    int ResetRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstRi4152A(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsRi4152A(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallRi4152A(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorRi4152A(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoRi4152A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateRi4152A(void);
    void NullCalDataRi4152A(void);
	void SanitizeRi4152(void);
};
