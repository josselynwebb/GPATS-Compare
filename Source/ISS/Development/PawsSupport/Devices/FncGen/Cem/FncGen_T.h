//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    FncGen_T.h
//
// Date:	    12-May-04
//
// Purpose:	    Instrument Driver for FncGen
//
// Instrument:	FncGen  <device description> (<device type>)
//
//
// Revision History described in FncGen_T.cpp
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
#define MAX_MEM 64536

class CFncGen_T
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
    // Modifiers
    ModStruct m_Volt;
	ModStruct m_DCOffset;
	ModStruct m_Freq;
	ModStruct m_Burst;
	ModStruct m_Bandwidth;
	ModStruct m_DutyCycle;
	ModStruct m_BurstRepRate;
	ModStruct m_RiseTime;
	ModStruct m_FallTime;
	ModStruct m_Exponent;
	ModStruct m_StimSize;
	double m_StimData[MAX_MEM];
	ModStruct m_SampleSpacing;
	ModStruct m_TestEquipImp;
	ModStruct m_MaxTime;
    ModStruct m_TriggerDelay;
    ModStruct m_TriggerSlope;
	ModStruct m_TriggerVolt;
    ModStruct m_TriggerDelayTo;
    ModStruct m_TriggerSlopeTo;
	ModStruct m_TriggerVoltTo;
	ModStruct m_Event;
	ModStruct m_PulseWidth;
	
public:
     CFncGen_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CFncGen_T(void);
public:
    int StatusFncGen(int);
    int SetupFncGen(int);
    int InitiateFncGen(int);
    int FetchFncGen(int);
    int OpenFncGen(int);
    int CloseFncGen(int);
    int ResetFncGen(int);
private:
    int  ErrorFncGen(int Status, char *ErrMsg);
    int  GetStmtInfoFncGen(int Fnc);
    void InitPrivateFncGen(void);
    void NullCalDataFncGen(void);
};
