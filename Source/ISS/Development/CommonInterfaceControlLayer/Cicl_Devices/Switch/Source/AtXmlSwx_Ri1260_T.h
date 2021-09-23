//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlSwx_Ri1260_T.h
//
// Date:	    11OCT05
//
// Purpose:	    Instrument Driver for Swx_Ri1260
//
// Instrument:	Swx_Ri1260  <device description> (<device type>)
//
//
// Revision History described in AtXmlSwx_Ri1260_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;
    char  *StringPtr;
}AttributeStruct;

#define SWX_TYPE_RI1260_38   1
#define SWX_TYPE_RI1260_39   2
#define SWX_TYPE_RI1260_58   3
#define SWX_TYPE_RI1260_66   4

typedef struct Ri1260ConnectInfoStruct
{
    int   SwitchType;
    int   ModAddress;  //PAWS Blk
    int   InterConSelect; //PAWS = 0
    int   Mux_RelayType_SwitchNo;  //PAWS Mod
    int   Chan_RelNo_Pole; //PAWS Pth
}Ri1260ConnectInfo;

#define MAX_SWITCH_PATHS  20

class CSwx_Ri1260_T
{
private:
    // Static data
    int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
    char      m_InitString[ATXMLW_MAX_NAME+20];
    // Dynamic data
    ATXMLW_XML_HANDLE m_SignalDescription;
    int       m_Action;
    char      m_SigResourceName[ATXMLW_MAX_NAME];
    int       m_SwitchCount;
    Ri1260ConnectInfo m_SwitchInfo[MAX_SWITCH_PATHS];
    

    // Attributes
    AttributeStruct m_PawsPath;


public:
     CSwx_Ri1260_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CSwx_Ri1260_T(void);
public:
    int StatusSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalSwx_Ri1260(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalSwx_Ri1260(ATXMLW_INTF_CALDATA* CalData);
    int ResetSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstSwx_Ri1260(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsSwx_Ri1260(ATXMLW_INTF_INSTCMD* InstrumentCommands, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallSwx_Ri1260(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  ConnectSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisconnectSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorSwx_Ri1260(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoSwx_Ri1260(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetSwitchInfo(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateSwx_Ri1260(void);
    void NullCalDataSwx_Ri1260(void);
	void SanitizeRi1260(void);
};
