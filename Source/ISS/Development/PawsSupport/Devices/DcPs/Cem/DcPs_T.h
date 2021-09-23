//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    DcPs_T.h
//
// Date:	    03-Jan-05
//
// Purpose:	    Instrument Driver for DcPs
//
// Instrument:	DcPs  DC Power Supplies for GPATS (DCPS)
//
//
// Revision History described in DcPs_T.cpp
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
#define MAX_DEVICES    120

class CDcPs_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
	char      m_SignalDescription[4096];
	char      m_ResourceName[MAX_DEVICES][35];
	char	  m_VirtResourceName[35];
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
	double    m_CalData[CAL_DATA_COUNT];
    // Modifiers
    /////// Place DcPs Modifier structures here /////////
    ModStruct m_VoltLimit[MAX_DEVICES];
    ModStruct m_Voltage[MAX_DEVICES];
	ModStruct m_VoltageMax[MAX_DEVICES];
    ModStruct m_Current[MAX_DEVICES];
	ModStruct m_CurrentMax[MAX_DEVICES];
    ModStruct m_CurrentLimit[MAX_DEVICES];
    ModStruct m_MaxTime[MAX_DEVICES];
	ModStruct m_Measure[MAX_DEVICES];

public:
     CDcPs_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CDcPs_T(void);
public:
    int StatusDcPs(int);
    int SetupDcPs(int);
    int InitiateDcPs(int);
    int FetchDcPs(int);
    int OpenDcPs(int);
    int CloseDcPs(int);
    int ResetDcPs(int);
private:
    int  ErrorDcPs(int Status, char *ErrMsg);
    int  GetStmtInfoDcPs(int Fnc);
    void InitPrivateDcPs(int ChanIndex);
    void NullCalDataDcPs(void);
	void GetChanIdxDcPs(int Fnc);
};
