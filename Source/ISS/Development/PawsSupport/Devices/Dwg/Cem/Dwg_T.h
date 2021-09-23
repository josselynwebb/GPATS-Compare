 //2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Dwg_T.h
//
// Date:	    12-May-04
//
// Purpose:	    Instrument Driver for Dwg
//
// Instrument:	Dwg  - Digital Test Instrument 
//
//
// Revision History described in Dwg_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
#include "visa.h"
#include "terM9.h"
#include "cemsupport.h"
#include "functionCodes_TETS.h"

#define SIMCICL   // 08/13/07 added back because wrapper no longer gets handle
//#define SIMCICLDEBUG   // 02/19/07 added for debug remove or comment out for a release compile. 

#define TOTAL_PINS        192//480
//#define TOTAL_PINS        64  // for lashup debug
#define PINS_PER_CARD     64//48
#define TOTAL_CARDS       (TOTAL_PINS / PINS_PER_CARD)
#define TOTAL_HVPINS      32

typedef void* HDWG;

typedef struct tag_DwgEvent
{
	int		hEvent;
	double	dValue;
	int		hRef;
} DwgEvent, *HEV;

typedef struct responseBuffer
{
	struct PINS {int count;int list[TOTAL_PINS];} pins;
	struct SIZE {int wordCount;int wordSize;} size;
	struct REF  {int *array;} ref;
	struct MASK {int *array;} mask;

} responseBuffer;

typedef struct TRM9level 
        {
           struct USED { BOOL stim; BOOL resp; } used;
           struct VI { double h; double l;} vi;  // VIH and VIL
           struct VO { double h; double l;} vo;  // VOH and VOL
           struct LOAD {double ioh; double iol; double vcom;} load;  // IOH, IOL and VCOM
           int slewrate;
        } TRM9level;
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
typedef struct TRM9pinlvlidx
        {
           struct PINLVLIDX { int stim; int resp; } lvlidx; // 0 not used, 1 idx 0, 2 idx 1
        } TRM9pinlvlidx;
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
typedef struct TRM9cardset 
        {
           TRM9level Level[2];
           TRM9pinlvlidx PinIdx[PINS_PER_CARD];
           int SetGndLvl;
        } TRM9cardset;  // Used for terM9_setLevelSet function
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
typedef struct TRM9pin 
        {
//          BOOL reset;       // 1: Pins that need to be reset by Disable Digital Configuration
			int reset;			// 0 is not used, otherwise it is the FNC value. ie apply(11), Prove(22)...
			BOOL current;     // Currently used
		  // int chnlnum;
        }TRM9pin;         // variable used to keep track of the static pin settings
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
typedef struct TRM9timing
        {
           struct Phase { double assertEdge ; double returnEdge;} phase; 
           struct Window { double openEdge; double closeEdge;} window;   
        } TRM9timing;  // Used for terM9_setTimingSet function
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
typedef struct ModStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;

}ModStruct;

typedef struct HighVoltagePinsStruct
{ int hvchnnl;
  ViInt32 hvdrive_detectNot;
  ViInt32 hvState;
} modHvPins;

#define CAL_DATA_COUNT 2
 
class CDwg_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];

    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
	
	int		  m_referenceWordCount;
	int		  m_referenceWordSize;
	int		  m_referenceIndex;
	int		  *m_referenceByteArray;
	bool      m_referenceByteArrayflag;

	int		  m_maskWordCount;
	int		  m_maskWordSize;
	int       *m_maskByteArray; // 02/16/07-
	bool      m_maskByteArrayflag;

	int		  m_errorWordCount;
//	int		  m_errorWordSize;
	int		  m_CompareWordSize;
	int		  *m_comparewordsArray; // 02/16/07-
	bool      m_comparewordsArrayflag;

	int		  *m_errorwordsArray; // 02/16/07-
	int       *m_errorIdx;  // 02/16/07-
	int		  m_errorIndex;
	bool      m_errorwordsArrayflag;
	bool      m_errorIdxflag;

	int			m_tsetIndex;
	int			m_stimFlag;
	bool		m_firstTime;
	int			m_globalPatternIndex;
	int			m_globalStimPatternIndex;

	responseBuffer		m_respBuff[256];
	int			m_respBuffCount;
	int			m_fetchCounter;
	bool		m_wasDynamic;

	int m_iterations;
	bool		goforcnxflag;
    int Fnc1st;
	DATUM * pdatumcnx;

//	bool m_checkerrors;  // initially have the compare flag on until a respone is retrieved.  // 03/08/07-
//	bool m_returnvalues; // initially have the a response not done.//  // 03/08/07-
//eads

	//Static Stim

	ViInt32			*m_pStimPins;  // 02/16/07-
	ViInt32			m_pStimPinsCnt;
	ViInt32			*m_pStimValue;

	ViInt32			m_StimPcnt;
//	ViInt32			*m_StimPins[TOTAL_PINS]; // 02/16/07-
	ViInt32			m_StimPins[TOTAL_PINS+4]; // 03/14/07-
	//Static Response
	// ViInt32			*m_pRespPins; // 02/16/07-
	ViInt32			m_pRespPinsCnt;
	ViInt32			*m_pRespValue; 
//	int			*m_pRespValue; // 03/14/07a-
//	char			*m_digitalValue;
	int runOnce;
	ViInt32			m_patternCount;
	char 			m_patternBuffer[32768][TOTAL_PINS+1];//0 reset 1 vil 2 vih 3 vol 
	int				m_inDynamic;
	ViInt32			m_response[1024];
	int				m_singlePattern;
	int				m_hvStimCount;
	int				m_hvRespCount;
	ViReal64		m_hvThreshold;
	int				runOnceHV;
	ViReal64		m_retFormat;

//eads-end
	
    ViSession       m_Handle;
	double			m_CalData[CAL_DATA_COUNT];
	char			 m_ResourceName[32];
      char          m_ResouceName[32];
    bool			m_PrevReset;            // Flag to reduce multiple resets
	TRM9cardset		m_Td925setup[TOTAL_CARDS+1]; // Structure of Setup levels 
	TRM9pin			m_Td925pin[TOTAL_PINS]; // Structure of 480 pins and their characteristics
	TRM9timing		m_Td925timing;          // Used for future upgrades
	ViInt32			m_RespPins[TOTAL_PINS+4];    //12/18/06 change to *m_RespPins;         // Variable needed to convert resp-only pin lists as a parameter of PNP and keep for fetch
	int				m_RespPcnt;             // Response Pin count used for fetch.
	int				m_PatCount;             // Response data count (number of patterns) used for fetch
	int				m_RespOnly;             // Flag to specify Response-Only mode (no REF available)
	int				m_StmRespSave;          // Flag to specify StimRespSave mode (no REF available)
	//BOOL			*m_MaskBinData;	      // Array of Binary data that has been retrieved from MASK
   //unsigned char	*m_RespByteData;         // Array of Byte data that will be returned for fetch RESP
	int				m_RespByteCnt;          // Response Byte count used for fetch.
    int				m_RespBytesPerPat;      // Width of response pattern
   unsigned char	*m_ErrByteData;          // Array of Byte data that will be returned for fetch ERROR
	int				m_ErrByteCnt;           // Error Byte count used for fetch.
 	int				m_ErrIdxCnt;            // ErrI count used for fetch.
	int				m_FaultCnt;             // Fault Count used for fetch.

    int				m_HVdetectPinlist[32];  // Pin list retreived from atlas
	int				m_HVdetectPcnt;         // Number of "read" HV pins
	int				m_littleWord;
	int				m_wordWidth;
	int             m_BitCount;
//	int			   m_RespChnlCnt;

	//int				m_staticOrDynamic;

    void InitMemberVars(void);
//	int meas_illegalState;  //04/24/07- added for pcr 234. 


    // Modifiers
    /////// Place Dwg Modifier structures here /////////
    ModStruct m_VoltMax;
    ModStruct m_AutoRng;
    ModStruct m_MaxTime;
    ModStruct m_Freq;
    /* e.g.
    ModStruct m_Res;
    ModStruct m_Current;
    ModStruct m_TrigSrc;
    ModStruct m_TrigDly;
    ModStruct m_SampleWidth;
    ModStruct m_RefVolt;
    */

	//*** Begin DWG ***

	//Define Digital Configuration

	ModStruct m_DigitalConfig;

	ModStruct m_VoltageOne;
	ModStruct m_VoltageZero;
	ModStruct m_VoltageQuies;
	ModStruct m_CurrentOne;
	ModStruct m_CurrentZero;

	ModStruct m_StimZero;
	ModStruct m_StimOne;

	ModStruct m_IllegalStateIndicator;
	ModStruct m_LevelLogic1Voltage;
	ModStruct m_LevelLogic0Voltage;
	ModStruct m_RiseTime;
	ModStruct m_FallTime;

	ModStruct     m_StimClockFreq;	//03/13/07-
	ModStruct     m_StimClockSource;  // 03/13/07-
	ModStruct     m_RespClockFreq;
	ModStruct     m_RespClockSource;   //03/13/07-

	//Define Digital Sensor

		//ModStruct m_LogicData	(declared above)
		//ModStruct m_SameAs (declared above)
		//ModStruct m_DigType (declared above)

	//Connect

	ModStruct m_LogicDataValue;
	ModStruct m_ValueLogicData;

	//Disconnect

		//ModStruct m_LogicDataValue (declared above)
		//ModStruct m_ValueLogicData (declared above)

	//Define <digital timing>, Digital Timing

	ModStruct m_StimEvent;
	ModStruct m_SenseEvent;

	//Identify

	ModStruct m_Event;
	ModStruct m_AsVoltageSquareEq;
	ModStruct m_PeriodRange;

	//Stimulate

	ModStruct m_Repeat;
	ModStruct m_Hiz;
	ModStruct m_On;
	ModStruct     m_VoltOne;
	ModStruct     m_VoltZero;
	ModStruct     m_RespDelay;
	ModStruct     m_Circulate;
	ModStruct     m_TrigSource;
	ModStruct     m_ThrshVolt;
	ModStruct     m_CirculateCont;

	//Sense

	ModStruct m_Value;
	ModStruct m_Into;

	//Prove
	ModStruct m_Ref;
	ModStruct m_MaskOne;
	ModStruct m_MaskZero;
	ModStruct m_SaveComp;
	ModStruct m_Error;
	ModStruct m_ErrorIndex;
	ModStruct m_FaultCount;
	ModStruct m_ZeroOn;
	ModStruct m_OneOn;
	ModStruct m_HizOn;
	
	//Apply Logic Data Value

	//Measure, (Value Into)

	//Do, Timed Digital

	ModStruct m_StimRate;
	ModStruct m_SenseDelay;
	ModStruct m_SenseRate;
	ModStruct m_Iterate;

	//Enable

	//Disable

// 10/24/06 
	modHvPins m_hvPinsResp[TOTAL_HVPINS];
	modHvPins m_hvPinsStim[TOTAL_HVPINS];
	
	//*** END DWG ***

public:
     CDwg_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CDwg_T(void);
public:
    int StatusDwg(int);
    int SetupDwg(int);
    int InitiateDwg(int);
    int FetchDwg(int);
    int OpenDwg(int);
    int CloseDwg(int);
    int ResetDwg(int);
	int getcnx(int);

private:
    int  ErrorDwg(int,int,int);
    int  GetStmtInfoDwg(int Fnc);
    void InitPrivateDwg(void);
    void NullCalDataDwg(void);
	void retrieveDwg(int,DATUM*,int);
	int CDwg_T::checklevelSets(void);

    int  SetupStim(int Fnc);
    int  SetupResp(int Fnc);
    int  DoDigitalSetup(int Fnc);
    void ClearStmtMemberVars(void);
	int  power(int,int);
	int  fnPin(int);
	int  SystemPin(int, int);
	int  SetStimCardLevel(double, double, int, int, TRM9cardset*,int);
	int  SetRespCardLevel(double, double, int, int, TRM9cardset*,int);
    int  PartialRemove(int Fnc);
	int  ProcessResponseData(int,int, int);
	int  ParseSetup(int, int*, int*);
	void  GetCardPin(int, int*, int*);
	void ErrorM9(ViSession, ViStatus, char[]);
	void runBurst(int);
	void setupHV(int);
	void resetHV(int);
	void stimHV(int);
	void fetchHV(int);
	void setupEvents(int);
};

