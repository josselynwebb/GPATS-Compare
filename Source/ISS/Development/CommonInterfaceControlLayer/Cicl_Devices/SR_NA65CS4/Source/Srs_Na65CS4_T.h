//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Srs_Na65CS4_T.h
//
// Date:	    11OCT05
//
// Purpose:	    Instrument Driver for Srs_Na65CS4
//
// Instrument:	Srs_Na65CS4  <device description> (<device type>)
//
//
// Revision History described in Srs_Na65CS4_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    char   Dim[ATXMLW_MAX_NAME];
    double Real;

}AttributeStruct;

#define CAL_DATA_COUNT 2

// Local Defines
#define SYNCHRO     1
#define RESOLVER    2
#define STIMULUS    3
#define MEASUREMENT 4

#define CRLF        "\r\n"

// declare CHANNELS to be 1 more than the number of channels
#define CHANNELS    3

// ISINTATTR assumes the existance of m_SignalDescription, Name = element's name attribute value.
#define ISINTATTR(x,y) (atxmlw_Get1641IntAttribute(m_SignalDescription, Name, x, y))

class CSrs_Na65CS4_T
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
    char      m_InputChannel[ATXMLW_MAX_NAME];

    // Modifiers
    bool      m_RefExt;
    int       m_ChanNum;

    // Modifiers
    AttributeStruct m_Angle[CHANNELS];
    AttributeStruct m_AngleRate[CHANNELS];
    AttributeStruct m_VoltageOut[CHANNELS];
    AttributeStruct m_FreqSim[CHANNELS];
    AttributeStruct m_FreqInd[CHANNELS];
    AttributeStruct m_MaxTime[CHANNELS];
    AttributeStruct m_RefVoltSim[CHANNELS];
    AttributeStruct m_RefVoltInd[CHANNELS];
    AttributeStruct m_RatioSim[CHANNELS];
    AttributeStruct m_RatioInd[CHANNELS];
    bool            m_ExtRefSim[CHANNELS];
    bool            m_ExtRefInd[CHANNELS];

    ////// The following are standard attributes parsed by
    ////// atxmlw_Get1641StdTrigChar and
    ////// atxmlw_Get1641StdMeasChar respectively
    ATXMLW_STD_TRIG_INFO m_TrigInfo[CHANNELS];
    ATXMLW_STD_MEAS_INFO m_MeasInfo[CHANNELS];

public:
     CSrs_Na65CS4_T(int Instno, int ResourceType, char* ResourceName,
            int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
            ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CSrs_Na65CS4_T(void);
public:
    int StatusSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalSrs_Na65CS4(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalSrs_Na65CS4(ATXMLW_INTF_CALDATA* CalData);
    int ResetSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstSrs_Na65CS4(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsSrs_Na65CS4(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallSrs_Na65CS4(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorSrs_Na65CS4(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoSrs_Na65CS4(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateSrs_Na65CS4(int Chan);
    void NullCalDataSrs_Na65CS4(void);
    int  GetSignalCond(char *Name, char *InputNames, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void GetResourceInfo(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  CSrs_Na65CS4_T::SetupChan(int Chan, char *App, char *Mode, bool Coarse,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);

};
