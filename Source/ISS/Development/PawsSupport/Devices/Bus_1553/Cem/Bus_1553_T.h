//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_1553_T.h
//
// Date:	    1-Jan-06
//
// Purpose:	    Instrument Driver for Bus_1553
//
// Instrument:	Bus_1553  <device description> (<device type>)
//
//
// Revision History described in Bus_1553_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

typedef struct ModStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;

}ModStruct;

#define CAL_DATA_COUNT 2
class CBus_1553_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
	char      m_SignalDescription[1024];
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
	double    m_CalData[CAL_DATA_COUNT];
	bool      m_initiate;
	char    m_XmlValue[2048];

	// Modifiers
    int m_Data[32];
	int m_SendAddress[33];
	int m_RecieveAddress[33];
	int m_Fetched[32];
    ModStruct m_attribute;
    ModStruct m_BusSpec;
	ModStruct m_FaultTest;
	ModStruct m_Standard;
	ModStruct m_DataSize;
	ModStruct m_Command[2];
	ModStruct m_Status;
	ModStruct m_BusMode;
	ModStruct m_Proceed;
	ModStruct m_Wait;
	ModStruct m_TestEquipRole;
	ModStruct m_MessageGap;
	ModStruct m_ResponseTime;
	ModStruct m_MaxTime;
	ModStruct m_SendAddressSize;
	ModStruct m_RecieveAddressSize;
	ModStruct m_Exnm;
 
public:
     CBus_1553_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CBus_1553_T(void);
public:
    int StatusBus_1553(int);
    int SetupBus_1553(int);
    int InitiateBus_1553(int);
    int FetchBus_1553(int);
    int OpenBus_1553(int);
    int CloseBus_1553(int);
    int ResetBus_1553(int);
private:
    int  ErrorBus_1553(int Status, char *ErrMsg);
    int  GetStmtInfoBus_1553(int Fnc);
    void InitPrivateBus_1553(void);
    void NullCalDataBus_1553(void);
	void SendSetupMods(int Fnc, char type []);
};
