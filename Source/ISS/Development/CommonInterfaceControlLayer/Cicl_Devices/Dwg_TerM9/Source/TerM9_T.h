//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    TerM9_T.h
//
// Date:	    11OCT05
//
// Purpose:	    Instrument Driver for TerM9
//
// Instrument:	TerM9  <device description> (<device type>)
//
//
// Revision History described in TerM9_T.cpp
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
class CTerM9_T
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
    char      m_OutputChannel[ATXMLW_MAX_NAME];

    // Modifiers
    /////// Place TerM9 Attribute structures here /////////
    /////// These are captured in the GetSignalChar() and GetSignalCond()
    /////// routines in the TerM9_T.cpp 
    AttributeStruct m_DcComp; // DC Component
    AttributeStruct m_Periodic; // AC/periodioc component:
                                // .int == type in AtXmlWrapper.h
                                //         e.g. STD_PERIODIC_SINUSOID
                                // .real == the value of "amplitude"
                                // .Dim  == the unit string of "amplitude"
    AttributeStruct m_RiseTime;
    AttributeStruct m_FallTime;
    AttributeStruct m_PulseWidth;
    AttributeStruct m_Freq;

    AttributeStruct m_TEI;
    AttributeStruct m_HighPassFilter;
    AttributeStruct m_Attenuator;

    ////// The following are standard attributes parsed by
    ////// atxmlw_Get1641StdTrigChar, atxmlw_Get1641StdGateChar, and
    ////// atxmlw_Get1641StdMeasChar respectively
    ATXMLW_STD_TRIG_INFO m_TrigInfo;
    ATXMLW_STD_GATE_INFO m_GateInfo;
    ATXMLW_STD_MEAS_INFO m_MeasInfo;

public:
     CTerM9_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CTerM9_T(void);
public:
    int StatusTerM9(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalTerM9(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalTerM9(ATXMLW_INTF_CALDATA* CalData);
    int ResetTerM9(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstTerM9(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsTerM9(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallTerM9(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    ////// Utility functions
    int  ErrorTerM9(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateTerM9(void);
    void NullCalDataTerM9(void);
};
