//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:        Srs_T.h
//
// Date:        09-Feb-06
//
// Purpose:     Instrument Driver for Srs
//
// Instrument:  Synchro-Resolver (SRS)
//
//
// Revision History described in Srs_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

typedef struct ModStructStruct
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;
} ModStruct;

// Function codes
//   stimulus (simulator)
#define FNC_SYNC_OUT_1         9
#define FNC_RES_OUT_1         11
#define FNC_SYNC_OUT_2        17
#define FNC_RES_OUT_2         19
#define FNC_SYNC_MULTI        41
#define FNC_RES_MULTI         43

//   measure (indicator)
#define FNC_ANG_SYNC_1         8
#define FNC_ANG_RES_1         10
#define FNC_RATE_SYNC_1       12
#define FNC_RATE_RES_1        14
#define FNC_ANG_SYNC_2        16
#define FNC_ANG_RES_2         18
#define FNC_RATE_SYNC_2       20
#define FNC_RATE_RES_2        22
#define FNC_ANG_SYNC_MULTI    40
#define FNC_ANG_RES_MULTI     42
#define FNC_RATE_SYNC_MULTI   44
#define FNC_RATE_RES_MULTI    46

//   events
#define FNC_SYNC_EXT          64
#define FNC_SYNC_TTL         128

// FNC masks
#define STIM_MASK           0x01
#define RES_MASK            0x02
#define RATE_MASK           0x04
#define CHAN_MASK           0x18
#define MULTI_MASK          0x20
#define EVENT_MASK          0xC0

#define CAL_DATA_COUNT 2
#define CAL_TIME (86400 * 365) /* one year */

// Local Defines
#define SYNCHRO     1
#define RESOLVER    2
#define STIMULUS    3
#define MEASUREMENT 4

// declare CHANNELS to be 1 more than the number of channels
#define CHANNELS    3

#define CR          "\n"
#define MAXTIME     5.0
#define MAX_EVENTS  2

class CSrs_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
    char      m_ResourceSim[CHANNELS][32];
    char      m_ResourceInd[CHANNELS][32];
    double    m_CalData[CAL_DATA_COUNT];

    // Modifiers
    ModStruct m_Angle[CHANNELS];
    ModStruct m_AngleRate[CHANNELS];
    ModStruct m_VoltageOut[CHANNELS];
    ModStruct m_FreqSim[CHANNELS];
    ModStruct m_FreqInd[CHANNELS];
    ModStruct m_MaxTime[CHANNELS];
    ModStruct m_SyncToEvent[CHANNELS];
    ModStruct m_RefVoltSim[CHANNELS];
    ModStruct m_RefVoltInd[CHANNELS];
    ModStruct m_RatioSim[CHANNELS];
    ModStruct m_RatioInd[CHANNELS];
    bool      m_ExtRefSim[CHANNELS];
    bool      m_ExtRefInd[CHANNELS];

    struct {
        int       Fnc;
        int       EventNum;
        bool      Active;
        char      Source[10];
        ModStruct Slope;
    } m_Events[MAX_EVENTS];

public:
     CSrs_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CSrs_T(void);
public:
    int StatusSrs(int Fnc);
    int SetupSrs(int Fnc);
    int InitiateSrs(int Fnc);
    int FetchSrs(int Fnc);
    int OpenSrs(int Fnc);
    int CloseSrs(int Fnc);
    int ResetSrs(int Fnc);
private:
    int  ErrorSrs(int Status);
    int  GetStmtInfoSrs(int Fnc);
    void InitPrivateSrs(int ChanIndex);
    void NullCalDataSrs(void);
    void GetChan(int Fnc);
    inline int FindAvailEvent(void);
    inline int FindEventByNum(int num);
    inline int FindEventByFnc(int fnc);
};
