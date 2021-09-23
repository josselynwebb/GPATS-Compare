//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Dmm_T.h
//
// Date:	    12-May-04
//
// Purpose:	    Instrument Driver for Dmm
//
// Instrument:	Dmm  <device description> (<device type>)
//
//
// Revision History described in Dmm_T.cpp
//
// 1.0.1    08Apr09  Added variable m_StrobeToEvent
//                   Changed prototype of InitPrivateDmm() to 
//                   InitPrivateDmm(intFnt);
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
class CDmm_T
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
    ModStruct m_ACComp;
    ModStruct m_ACCompFreq;
    ModStruct m_AvCurrent;
    ModStruct m_AvVolt;
    ModStruct m_Bandwidth;
    ModStruct m_Current;
    ModStruct m_DCOffset;
	ModStruct m_Delay;
	ModStruct m_FourWire;
    ModStruct m_Freq;
	ModStruct m_MaxTime;
    ModStruct m_Period;
    ModStruct m_RefRes;
    ModStruct m_RefVolt;
	ModStruct m_Res;
    ModStruct m_SampleCount;
    ModStruct m_SampleWidth;
	ModStruct m_StrobeToEvent;	// added ewl 20090408
    ModStruct m_Volt;
	ModStruct m_Voltage;		// added ewl 20110524
	ModStruct m_VoltageP;		// added ewl 20110524
	ModStruct m_VoltagePP;		// added ewl 20110524
	ModStruct m_VoltRatio;
	ModStruct m_EventSampleCount;
	ModStruct m_EventDelay;
	bool	  m_SignalActive;	// added ewl 20090408

	bool TriggerFlag;
	bool m_AutoRange;
    
public:
     CDmm_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CDmm_T(void);
public:
    int StatusDmm(int);
    int SetupDmm(int);
    int InitiateDmm(int);
    int FetchDmm(int);
    int OpenDmm(int);
    int CloseDmm(int);
    int ResetDmm(int);
private:
    int  ErrorDmm(int Status, char *ErrMsg);
    int  GetStmtInfoDmm(int Fnc);
    void InitPrivateDmm(int Fnc);	// change to include Fnc ewl 20090408
    void NullCalDataDmm(void);
};
