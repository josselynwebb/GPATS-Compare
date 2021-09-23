//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_Ethernet_Gigabit_T.h
//
// Date:	    20-Feb-06
//
// Purpose:	    Instrument Driver for Bus_Ethernet_Gigabit
//
// Instrument:	Bus_Ethernet_Gigabit  <device description> (<device type>)
//
//
// Revision History described in Bus_Ethernet_Gigabit_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

typedef struct ModStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;
	char Address[20];
}ModStruct;

#define CAL_DATA_COUNT 2
#define MAX_DATA 5000
class CBus_Ethernet_Gigabit_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
    char      m_SignalDescription[10000];
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
	double    m_CalData[CAL_DATA_COUNT];
	bool      m_initiate;
	char    m_XmlValue[10000];
    // Modifiers
    /////// Place Bus_Ethernet_Gigabit Modifier structures here /////////
    ModStruct m_BusSpec;
	char m_Data[MAX_DATA];
    ModStruct m_DataSize;
    ModStruct m_BusMode;
    ModStruct m_Proceed;
    ModStruct m_Wait;
	ModStruct m_MaxTime;
	ModStruct m_Exnm;
	ModStruct m_Attribute;
	ModStruct m_Protocol;

	ModStruct m_LocalIP;
    ModStruct m_LocalMask;
    ModStruct m_LocalGateway;
	ModStruct m_RemoteIP;
	ModStruct m_RemotePort;


public:
     CBus_Ethernet_Gigabit_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CBus_Ethernet_Gigabit_T(void);
public:
    int StatusBus_Ethernet_Gigabit(int);
    int SetupBus_Ethernet_Gigabit(int);
    int InitiateBus_Ethernet_Gigabit(int);
    int FetchBus_Ethernet_Gigabit(int);
    int OpenBus_Ethernet_Gigabit(int);
    int CloseBus_Ethernet_Gigabit(int);
    int ResetBus_Ethernet_Gigabit(int);
private:
    int  ErrorBus_Ethernet_Gigabit(int Status, char *ErrMsg);
    int  GetStmtInfoBus_Ethernet_Gigabit(int Fnc);
    void InitPrivateBus_Ethernet_Gigabit(void);
    void NullCalDataBus_Ethernet_Gigabit(void);
	void SendSetupMods(int Fnc, char type []);
};
