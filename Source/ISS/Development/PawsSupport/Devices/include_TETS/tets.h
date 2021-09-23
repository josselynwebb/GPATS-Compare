
#define SWX_USED 78
#define SWX_LENGTH 7

static char RFMA_ADDR[20],RFDNCVTR_ADDR[20],RFCNTR_ADDR[20];

static char     msg_buf[4000];
static char            instrument[256];     //CJW was 20
extern char    path_id[SWX_USED][SWX_LENGTH]; //made path_id multi-dimensional QN 09/27/00
static int		no_rf_switch; //capture no of switch closed QN 09/27/00
static int		    lf_sw_path[442][3], no_of_switches; //added by ME 05/08/00 to capture lf switches when making res. meas.
static ViStatus err;
static int probe,
    RF_Stim = 1; // 0 Indicate EIP, 1 Indicates Giga-tronics
static int DownconverterType;	// 1 = EIP, 2 = Giga-tronics
static int sysmon_err;
static double maxtimeout, sys_loss;
static int	urgent_halt;
static unsigned long ps_reset_time[11];

extern double	ResPLCValue[3][16][48];
extern double	RFS8PLCValue[32][10];
extern double	RFS9PLCValue[36][85];

extern char	*BLK1[];
extern char *BLK2[];
extern char *BLK3[];
extern char	*Switch8[];
extern char	*Switch9[];

extern char	*PLCDataTable;

#define flag 10
#define SIMSOFT_FLAG 1
#define DEBUG_FLAG 10

#define	PWRHD1	"HP8481D"
#define	PWRHD2	"HP8481A"
#define	ININAME	"\\ATS.ini"

#define	HD1		1
#define	HD2		2

#define	CARRIAGE_RETURN		13

#define	TABLE81D	"HP8481D_"
#define	TABLE81A	"HP8481A_"
#define	RFPLCDATA	"PLC_DATA"
#define	RESPLCDATA	"PLC_LFS_DATA"
#define	STARTPOINT	"BLK 1"
#define	BLOCK1		"BLK 1"
#define	BLOCK2		"BLK 2"
#define	BLOCK3		"BLK 3"

//#define	ININAME		"\\Tets.ini"

//--------------------------------------------------------------------------------
// Dwg global variables
extern __declspec( dllexport ) ViSession dwg_handle;                      // DWG handle
static int dwgdebugflag;                          // debug flag: 0 no, 1 yes
static int dwgsimflag  ;                          // simulation flag: 0 no, 1 yes
typedef struct DWGpin {
       BOOL reset;              // Is the pins flaged for possible reset (after disconnect)
       int value;               // not used -1, level logic 1 or 0 
       int voltlindex;          // non assigned -1, voltage levels 0 or 1
       int voltltype;           // Voltage level usage type (weighted): 1:VO, 2:VI, 4:LOAD
       BOOL connect;            // currently connected or not (0 or 1)
       int type;                // -1:nothing, 0:source, 1:sensor, 2:prove 
       int state;               // -1:nothing, 0:static, 1:dynamic
       BOOL reset2;             // 1: Pins that need to be reset by Disable Digital Configuration
       BOOL illegal;            // Illegal state indicator flag for Stim (links levels for I and O)
       int slewrate;            // 0:Low, 1: Medium, 2: High
       BOOL mask;				// 0: not masked, 1:masked (for PROVE)
       int ref;                 // reference value for PROVE
       BOOL current;            // Currently used for a static FETCH after INIT
       int VLusage;             // reflects the value in nVL from DwgData.c which includes the VL usage
       int format;              // Format for the dynamic pins
       int lastAppVal;          // Last values applied for a possible following measure
       int lastAppVltIdx;       // last Voltage index used for Apply for a possible following measure
        } DWGpin;               // variable used to keep track of the static pin settings
typedef struct DWGlevels {
       struct VI { double h; double l;} vi;  // VIH and VIL
       struct VO { double h; double l;} vo;  // VOH and VOL
       struct LOAD {double ioh; double iol; double vcom;} load;  // IOH, IOL and VCOM
       int slewrate;
        } DWGlevels;  // used for terM9_setLevelSet function
typedef struct DWGsetup {
       BOOL staticdata[192];  // static data: TRUE, value is 1, FALSE, it's 0. 
                              // Used in setup until used for dwgstaticpin
       BOOL levelindex;       // 1: Low, 2: Medium, 3: High
       int  type;             // -1:nothing, 0:source, 1:sensor, 2:prove
        } DWGsetup;

typedef struct DWGtest {
       BOOL ENB_DCFstart;      // First time after Enable DCF (needed for the Voltage level usage)
       BOOL dynflag;           // Flag for dynamic testing
       BOOL lastdynflag;       // Memory of the previous dynflag
       BOOL dynfetchtime;         // 1 when it's time to fetch all the data from DO, TIMED DIGITAL
       int *pLayer;            // Pointer to the next layer
       } DWGtest;

//DWGsetup  dwgsetup;          // Stim in static mostly in the APPLY, MEASURE environment
//DWGpin    dwgpin[192];       // For static mostly, with all settings and flags for pins
//DWGlevels dwglevels[2];      // Used throughout the Atlas TPS for the Voltage levels
//DWGtest   dwgtest;           // Used as a set of reference and flags for the DTD Dynamic test.

//ViInt32  dwgsystempins[1];
static ViStatus pnpstatus;          // Status of the pnp function. OK is VI_SUCCESS
//int      dwgtotpcnt;         // total number of channel count in the system

//int  *dwg_tmppinlist; // Temporary pin list between INIT and FETCH
//BOOL *dwgsavecomp;    // For PROVE: SAVE-COMP array
//BOOL *dwgerror;       // For PROVE: ERROR array
//int  *dwgerrorindex;  // For PROVE: ERROR-INDEX array

//BOOL dwghvdata[33];   // Global variable for HV pins + 1 for the -1 terminal value
//int  dwghvpinnum;     // Number of HV pins
//BOOL dwgMfollowA;     // global boolean for when a Measure follows an Apply with the exact same list of pins

extern double			GetPLC(double, char [80][8]);
extern void				parseRFPLCDataTable(char*);
extern int				initPLCDataTables(void);
extern void				parseFillinResValue(int*, char*, int);
extern int				parseResPLCDataTable(char*);
extern char				*fillInPLCTable(char*);
extern double			GetResPLC(int [][3], int);
extern int				initPLCDataTables(void);
extern DWORD WINAPI		StartEventThread(LPVOID *);
