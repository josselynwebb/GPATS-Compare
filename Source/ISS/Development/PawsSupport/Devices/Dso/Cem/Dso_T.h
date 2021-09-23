//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Dso_T.h
//
// Date:	    05-APR-06
//
// Purpose:	    Instrument Driver for Dso
//
// Instrument:	Dso  <device description> (<device type>)
//
//
// Revision History described in Dso_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
#include <map>
using namespace std;

typedef struct ModStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;
	char Text[256];

}ModStruct;

#define CAL_DATA_COUNT 2
class CDso_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
	char      m_SignalDescription[4096];
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
	double    m_CalData[CAL_DATA_COUNT];

	// Trigger structure
	struct TrigInfo
	{
		TrigInfo(int nSlope, double dLevel, int nPort, double dDelay):
			m_nSlope(nSlope),
			m_dLevel(dLevel),
			m_nPort(nPort),
			m_dDelay(dDelay)
		{
			// nothing here
		}
	public:
		int m_nSlope;
		double m_dLevel;
		int m_nPort;
		double m_dDelay;
	};

	// Getting the map for the Trigger information
	multimap<int, TrigInfo> m_Trig;

/*struct ModifierValue
{
	ModifierValue(int nCIIL, VARIANT var) : 
		m_nCIIL(nCIIL),
		m_varValue(var)
	{
		// nothing here
	}
	void Print()const
	{
		cout << "CIIL " << m_nCIIL << " ";
		if (m_varValue.vt == VT_R8)
		cout << "double value: " << m_varValue.dblVal << endl;
	}

	private:
		int m_nCIIL;
		CComVariant m_varValue;
};
*/
	// Parameters
	int m_Channumb;
    // Modifiers
    /////// Place Dso Modifier structures here /////////
	short m_Noun;
	// Meas Char
	short m_MeasChar;

	// Freq
	ModStruct m_Freq;
	ModStruct m_FreqMax;
	ModStruct m_FreqMin;
	// PRF
	ModStruct m_Prfr;
	ModStruct m_PrfrMax;
	ModStruct m_PrfrMin;
	// Period
	ModStruct m_Peri;
	ModStruct m_PeriMax;
	ModStruct m_PeriMin;
	// Rise-time
	ModStruct m_Rise;
	ModStruct m_RiseMax;
	ModStruct m_RiseMin;
	// Fall-time
	ModStruct m_Fall;
	ModStruct m_FallMax;
	ModStruct m_FallMin;
	// Pulse-width
	ModStruct m_Plwd;
	ModStruct m_PlwdMax;
	ModStruct m_PlwdMin;
	// Duty-cycle
	ModStruct m_Duty;
	ModStruct m_DutyMax;
	ModStruct m_DutyMin;
	// Preshoot
	ModStruct m_Psht;
	ModStruct m_PshtMax;
	ModStruct m_PshtMin;
	// Overshoot
	ModStruct m_Over;
	ModStruct m_OverMax;
	ModStruct m_OverMin;
	// Voltage-pp
	ModStruct m_Vlpp;
	ModStruct m_VlppMax;
	ModStruct m_VlppMin;
	// Av-voltage
	ModStruct m_Vlav;
	ModStruct m_VlavMax;
	ModStruct m_VlavMin;
	// Voltage-p
	ModStruct m_Vlpk;
	ModStruct m_VlpkMax;
	ModStruct m_VlpkMin;
	// Voltage-p-pos
	ModStruct m_Vpkp;
	ModStruct m_VpkpMax;
	ModStruct m_VpkpMin;
	// Voltage-p-neg
	ModStruct m_Vpkn;
	ModStruct m_VpknMax;
	ModStruct m_VpknMin;
	// Pos-pulse-width
	ModStruct m_Ppwt;
	ModStruct m_PpwtMax;
	ModStruct m_PpwtMin;
	// Neg-pulse-width
	ModStruct m_Npwt;
	ModStruct m_NpwtMax;
	ModStruct m_NpwtMin;
	// Voltage
	ModStruct m_Volt;
	ModStruct m_VoltMax;
	ModStruct m_VoltMin;
	// Sample
	ModStruct m_Smpl;
	ModStruct m_SmplMax;
	ModStruct m_SmplMin;
	// Math
	ModStruct m_Math;
	ModStruct m_MathMax;
	ModStruct m_MathMin;
	// Compare-Wave
	ModStruct m_Cmwv;
	ModStruct m_CmwvMax;
	ModStruct m_CmwvMin;
	// Load-Wave
	ModStruct m_Ldvw;
	ModStruct m_LdvwMax;
	ModStruct m_LdvwMin;
	// Save-Wave
	ModStruct m_Svwv;
	ModStruct m_SvwvMax;
	ModStruct m_SvwvMin;
	// Time
	ModStruct m_Time;
	ModStruct m_TimeMax;
	ModStruct m_TimeMin;
	//////////////////
	// Other modifiers
	// Dc-offset
	ModStruct m_Dcof;
	// Freq-window
	ModStruct m_FrqwMax;
	ModStruct m_FrqwMin;
	// Sample-count
	ModStruct m_Scnt;
	// Trig-level
	ModStruct m_Trlv;
	// Trig-slope
	ModStruct m_Trsl;
	// Bandwidth
	ModStruct m_BandMax;
	ModStruct m_BandMin;
	// Max-time
	ModStruct m_Maxt;
	// Coupling
	ModStruct m_Cplg;
	// Test-equip-imp
	ModStruct m_Timp;
	// Sample-time
	ModStruct m_Satm;
	// Resp
	ModStruct m_Resp;
	// Save-from
	ModStruct m_Svfm;
	// Save-to
	ModStruct m_Svto;
	// Load-from
	ModStruct m_Ldfm;
	// Compare-ch
	ModStruct m_Cmch;
	// Compare-to
	ModStruct m_Cmto;
	// Allowance
	ModStruct m_Allw;
	// Add-from
	ModStruct m_Adfm;
	// Add-to
	ModStruct m_Adto;
	// Subtract-from
	ModStruct m_Sbfm;
	// Subtract-to
	ModStruct m_Sbto;
	// Multp-from
	ModStruct m_Mpfm;
	// Multp-to
	ModStruct m_Mpto;
	// Differentiate
	ModStruct m_Difr;
	// Destination
	ModStruct m_Dest;
	// Integrate
	ModStruct m_Intg;
	// Strobe-To-Event
	ModStruct m_Sbev;
	//////////////////////////////
	// Pseudo modifiers (not in Atlas)
	// Trig source
	ModStruct m_Trgs;
	// Trig delay
	ModStruct m_Trdl;
	//////////////////////////////
	struct
	{
		bool Exists;
		double delay;
		int slope;
		double voltage;
		int port;
		int handle;
	} m_Evtf, m_Evtt;

public:
     CDso_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CDso_T(void);
public:
    int StatusDso(int);
    int SetupDso(int);
    int InitiateDso(int);
    int FetchDso(int);
    int OpenDso(int);
    int CloseDso(int);
    int ResetDso(int);
private:
    int  ErrorDso(int Status, char *ErrMsg);
    int  GetStmtInfoDso(int Fnc);
    void InitPrivateDso(void);
    void NullCalDataDso(void);
};
