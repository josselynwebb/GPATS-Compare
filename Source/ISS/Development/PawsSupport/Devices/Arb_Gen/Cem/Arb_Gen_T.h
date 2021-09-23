//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Arb_Gen_T.h
//
// Date:	    12-May-04
//
// Purpose:	    Instrument Driver for Arb
//
// Instrument:	Arb  <device description> (<device type>)
//
//
// Revision History described in Arb_Gen_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

#include <vector>

typedef struct ModStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;

}ModStruct;

typedef struct IntArrayModifier {
				int     *pVal;
				double  *dVal;
				int 	 Size;
				bool	 Exists;
				double   VertSpan;
				double   Max;
				double   Min;
} IntArrMod;

struct EventStructure
{
	int				Fnc;
	int				EventNum;
	char			Source[16];
	ModStruct		EventDelay;
	ModStruct		Slope;
	ModStruct		Coupling;
	ModStruct		Freq;
	ModStruct		MaxTime;
	ModStruct		SettleTime;
	ModStruct		TestEquipImp;
	ModStruct		TrigVolt;
	ModStruct		VoltMax;
	
};

#define CAL_DATA_COUNT 2
#define MAX_MEM 64536

class CArb_Gen_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
    char      *m_SignalDescription;
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
	double    m_CalData[CAL_DATA_COUNT];
    
	// Modifiers
	ModStruct m_AgeRate;
	ModStruct m_Bandwidth;
	ModStruct m_Burst;
	ModStruct m_BurstRepRate;
	ModStruct m_DCOffset;
	ModStruct m_EventDelay;
	ModStruct m_EventEachOcc;
	ModStruct m_EventGatedBy;
	ModStruct m_EventOut;
	ModStruct m_EventSlope;
	ModStruct m_EventSync;
	ModStruct m_EventVoltage;
	ModStruct m_Frequency;
	ModStruct m_FreqWindow;
	ModStruct m_FreqWindowMin;
	ModStruct m_FreqWindowMax;
    ModStruct m_MaxTime;
	ModStruct m_Period;
	ModStruct m_SampleSpacing;
	ModStruct m_Stim;
	ModStruct m_TestEquipImp;
	double m_StimData[MAX_MEM];
	ModStruct m_Voltage;
	ModStruct m_VoltageP;
	ModStruct m_VoltagePP;

	std::vector<EventStructure>				m_vEvents;

	//   IntArrMod m_Stim;

//	char  *m_CharLowByte;
//	char  *m_CharHiByte;
	IntArrMod m_Data;  //develop later

public:
     CArb_Gen_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CArb_Gen_T(void);
public:
    int StatusArb_Gen(int);
    int SetupArb_Gen(int);
    int InitiateArb_Gen(int);
    int FetchArb_Gen(int);
    int OpenArb_Gen(int);
    int CloseArb_Gen(int);
    int ResetArb_Gen(int);
private:
    int  ErrorArb_Gen(int Status, char *ErrMsg);
    int  GetStmtInfoArb_Gen(int Fnc);
    void InitPrivateArb_Gen(void);
    void NullCalDataArb_Gen(void);
	void InitModStructure(ModStruct *);
	void InitEventStructure(EventStructure *);
	int FindEventIndex(int);
};