//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_Can_T.h
//
// Date:	    26-Jun-06
//
// Purpose:	    Instrument Driver for Bus_Can
//
// Instrument:	Bus_Can  <device description> (<device type>)
//
//
// Revision History described in Bus_Can_T.cpp
// 1.1.0.0  30MAY19  Updated for CAN bus Start problem        J. Witcher
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
#define MAX_DATA 5000

#define _TL_TEQT		1	// test-equip-talk
#define _TL_TEQL		2	// test-equip-listen
#define _TL_UUTT		4	// uut talker
#define _TL_UUTL		8	// uuut listener

class CBus_Can_T
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
    /////// Place Bus_Can Modifier structures here /////////
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

	ModStruct m_BusTimeout;
    ModStruct m_NoCommandTimeout;
    ModStruct m_NoResponseTimeout;
	ModStruct m_InterruptAckTimeout;
	ModStruct m_BaseVector;
	ModStruct m_AddressReg;
	ModStruct m_Command;

	ModStruct m_TimingValue;
	ModStruct m_ThreeSamples;
	ModStruct m_SingleFilter;
	ModStruct m_AcceptanceCode;
	ModStruct m_AcceptanceMask;
	ModStruct m_Talker;
	ModStruct m_TalkerListener;


public:
     CBus_Can_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CBus_Can_T(void);
public:
    int StatusBus_Can(int);
    int SetupBus_Can(int);
    int InitiateBus_Can(int);
    int FetchBus_Can(int);
    int OpenBus_Can(int);
    int CloseBus_Can(int);
    int ResetBus_Can(int);
private:
    int  ErrorBus_Can(int Status, char *ErrMsg);
    int  GetStmtInfoBus_Can(int Fnc);
    void InitPrivateBus_Can(void);
    void NullCalDataBus_Can(void);
	void SendSetupMods(int Fnc, char type []);
};
