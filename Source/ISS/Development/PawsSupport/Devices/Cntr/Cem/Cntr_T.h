//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////////
// File:            Cntr_T.h
//
// Date:            14FEB06
//
// Purpose:         Instrument Driver for Cntr
//
// Instrument:      Universal Counter Virtual Device (Cntr)
//
//
// Revision History described in Cntr_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

#define RMS_PK  1.414213562373
#define RMS_PP  (RMS_PK * 2)
#define CR      "\n"

typedef struct ModStructStruct
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;

}ModStruct;

#define MAX_SIG_DESC 4096
#define MAX_MEAS_ATTR 32
#define CAL_DATA_COUNT 2
#define MAX_EVENTS 5
class CCntr_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
    int       m_Noun;
    double    m_CalData[CAL_DATA_COUNT];
    char      m_SigDesc[MAX_SIG_DESC];
    char      m_MeasAttr[MAX_MEAS_ATTR];

    // Modifiers
    ModStruct m_Coupling;
    ModStruct m_DcOffset;
    ModStruct m_EventGateFrom;
    ModStruct m_EventGateTo;
    ModStruct m_EventSlope;
    ModStruct m_EventTimeFrom;
    ModStruct m_EventTimeTo;
    ModStruct m_FallTime;
    ModStruct m_FreqRatio;
    ModStruct m_Frequency;
    ModStruct m_MaxTime;
    ModStruct m_Period;
    ModStruct m_PhaseAngle;
    ModStruct m_PulseWidth;
    ModStruct m_RiseTime;
    ModStruct m_Slope;
    ModStruct m_StrobeToEvent;
    ModStruct m_TestEquipImp;
    ModStruct m_TimeInterval;
    ModStruct m_Trig;
    ModStruct m_Voltage;

    struct {
        int       Fnc;
        int       EventNum;
        bool      Active;
        char      Source[10];
        ModStruct Coupling;
        ModStruct SettleTime;
        ModStruct Slope;
        ModStruct TestEquipImp;
        ModStruct TrigVolt;
        ModStruct VoltMax;
    } m_Events[MAX_EVENTS];

public:
     CCntr_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CCntr_T(void);
public:
    int StatusCntr(int);
    int SetupCntr(int);
    int InitiateCntr(int);
    int FetchCntr(int);
    int OpenCntr(int);
    int CloseCntr(int);
    int ResetCntr(int);
private:
    int  ErrorCntr(int Status, char *ErrMsg);
    int  GetStmtInfoCntr(int Fnc);
    void InitPrivateCntr(int fnc);
    void NullCalDataCntr(void);
    inline int FindAvailEvent(void);
    inline int FindEventByNum(int num);
    inline int FindEventByFnc(int fnc);
};
