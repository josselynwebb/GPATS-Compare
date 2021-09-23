//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_PciSerial_T.h
//
// Date:	    13-Feb-06
//
// Purpose:	    Instrument Driver for Bus_PciSerial
//
// Instrument:	Bus_PciSerial  <device description> (<device type>)
//
//
// Revision History described in Bus_PciSerial_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

extern  int DE_BUG;
extern  int activereset;
extern	int com1active;
extern	int com2active;
extern	int com3active;
extern	int com4active;
extern	int com5active;

typedef struct ModStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;

}ModStruct;

#define CAL_DATA_COUNT 2
#define MAX_DATA 32
#define MAX_RTN_DATA 512
#define SIGNAL_DESCRIPTION_SIZE 1024
#define XML_VALUE_SIZE 1024

class CBus_RS485_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
    char      m_SignalDescription[SIGNAL_DESCRIPTION_SIZE];
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
	double    m_CalData[CAL_DATA_COUNT];
	bool      m_initiate;
	char      m_XmlValue[XML_VALUE_SIZE];
    
    // Modifiers
    /////// Place Bus_RS485 Modifier structures here /////////
    ModStruct m_BusSpec;
	int m_Data[MAX_DATA];
    ModStruct m_DataSize;
    ModStruct m_Terminated;
    ModStruct m_BusMode;
    ModStruct m_Proceed;
    ModStruct m_Wait;
	ModStruct m_BitRate;
	ModStruct m_StopBits;
	ModStruct m_Delay;
	ModStruct m_Parity;
	ModStruct m_MaxTime;
	ModStruct m_Exnm;
	ModStruct m_UutTalker;
	ModStruct m_UutListener;
	ModStruct m_TestEquipTalker;
	ModStruct m_TestEquipListener;
	ModStruct m_Attribute;
	ModStruct m_WordLength;

public:
     CBus_RS485_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CBus_RS485_T(void);
public:
    int StatusBus_RS485(int);
    int SetupBus_RS485(int);
    int InitiateBus_RS485(int);
    int FetchBus_RS485(int);
    int OpenBus_RS485(int);
    int CloseBus_RS485(int);
    int ResetBus_RS485(int);
private:
    int  ErrorBus_RS485(int Status, char *ErrMsg);
    int  GetStmtInfoBus_RS485(int Fnc);
    void InitPrivateBus_RS485(void);
    void NullCalDataBus_RS485(void);
	void SendSetupMods(int Fnc, char type []);
};
// Local Function Prototypes