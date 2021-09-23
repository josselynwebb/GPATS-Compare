#include "cem.h"
#include "key.h"
#include <limits.h>
#include <float.h>
EXTERNC int lod_mode;
//
//	VERBS
//
DECLAREC int v__cnt = V__CNT;
DECLAREC char *VCiilTxt [] = {
	"","APP","ARM","CAL","CHN","CLS","CMP","CON",
	"CPL","CRE","DCL","DEF","DEL","DIS","DO","DSB",
	"ELS","ENB","END","EST","FIN","FOR","FTH","GTO",
	"IDY","IF","INC","INP","INX","LVE","MEA","MON",
	"OPN","OUT","PFM","PRV","RD","REM","REQ","RES",
	"RST","SET","SNS","STI","TRM","UCP","UPD","VER",
	"WHL","WTF","XTN",};
//
//	NOUNS
//
DECLAREC int n__cnt = N__CNT;
DECLAREC char *NCiilTxt [] = {
	"","ACS","ADF","AMB","AMS","ATC","BUS","BUT",
	"COM","DCF","DCS","DGT","DIS","DME","DOP","EAR",
	"EMF","EPW","EVS","EVT","EXC","FLU","FMS","HEA",
	"IFF","ILS","IMP","INF","LAS","LCL","LDT","LGT",
	"LLD","LRF","LTR","MAN","MDS","MIF","MIL","PAC",
	"PAM","PAT","PDC","PDP","PDT","PMS","RDN","RDS",
	"RPS","RSL","RTN","SCS","SHT","SIM","SIN","SQW",
	"STM","STS","SYN","TAC","TDG","TED","TMI","TMR",
	"TRI","VBR","VID","VOR","WAV",};
//
//	MODIFIERS
//
DECLAREC int m__cnt = M__CNT;
DECLAREC char *MCiilTxt [] = {
	"","ACCF","ACCP","ADFM","ADLN","ADTO","AGER","ALLW",
	"ALTI","ALTR","AMCP","AMCU","AMFQ","AMMC","AMMF","AMPL",
	"AMSH","AMSR","ANAC","ANAX","ANAY","ANAZ","ANGL","ANGP",
	"ANGT","ANGX","ANGY","ANGZ","ANRT","ANRX","ANRY","ANRZ",
	"ANSD","ATMS","ATTE","ATTN","AUCO","BAND","BARP","BDTH",
	"BITP","BITR","BKPH","BRAN","BTRN","BURD","BURR","BURS",
	"BUSM","BUSS","CAMP","CAPA","CCOM","CDAT","CFRQ","CHAN",
	"CHID","CHIT","CHRM","CLKS","CMCH","CMDW","CMPL","CMTO",
	"CMWB","CMWV","COMD","COND","COUN","CPHS","CPKN","CPKP",
	"CPLG","CRSD","CRSF","CSTS","CTRQ","CUPK","CUPP","CUR0",
	"CUR1","CURA","CURI","CURL","CURQ","CURR","CURT","CWLV",
	"DATA","DATL","DATP","DATS","DATT","DATW","DBLI","DBND",
	"DBRC","DBRS","DCOF","DDMD","DEEM","DELA","DEST","DEWP",
	"DFBA","DIFR","DIFT","DIGS","DISP","DIST","DIVG","DIVS",
	"DMDS","DPFR","DPSH","DROO","DSFC","DSTR","DSTX","DSTY",
	"DSTZ","DTCT","DTMD","DTOR","DTSC","DUTY","DVPN","DVPP",
	"DWBT","EDLN","EDUT","EFCY","EFFI","EGDR","EINM","ERRI",
	"ERRO","EVAO","EVDL","EVEO","EVEV","EVFO","EVGB","EVGF",
	"EVGR","EVGT","EVOU","EVSB","EVSF","EVSL","EVSW","EVTF",
	"EVTI","EVTR","EVTT","EVUN","EVWH","EVXE","EVXM","EXAE",
	"EXNM","EXPO","FALL","FCLN","FCNT","FDST","FDVW","FIAL",
	"FILT","FLTC","FLTS","FLUT","FMCP","FMCU","FMFQ","FMSR",
	"FRCE","FRCR","FREQ","FRMT","FRQ0","FRQ1","FRQD","FRQP",
	"FRQQ","FRQR","FRQW","FUEL","FXDN","FXIP","FXQD","GAMA",
	"GSLP","HAPW","HARM","HARN","HARP","HARV","HFOV","HIZZ",
	"HLAE","HMDF","HRAG","HSRM","HTAG","HTOF","HUMY","IASP",
	"ICWB","IDSE","IDSF","IDSG","IDSM","IDWB","IJIT","ILLU",
	"INDU","INTG","INTL","IRAT","ISTI","ISWB","ITER","ITRO",
	"IVCW","IVDL","IVDP","IVDS","IVDT","IVDW","IVMG","IVOA",
	"IVRT","IVSW","IVWC","IVWG","IVWL","IVZA","IVZC","LDFM",
	"LDTO","LDVW","LINE","LIPF","LMDF","LMIN","LOCL","LRAN",
	"LSAE","LSTG","LUMF","LUMI","LUMT","MAGB","MAGR","MAMP",
	"MANI","MASF","MASK","MATH","MAXT","MDPN","MDPP","MDSC",
	"MGAP","MMOD","MODD","MODE","MODF","MODO","MODP","MPFM",
	"MPTO","MRCO","MRKB","MRTD","MSKZ","MSNR","MTFD","MTFP",
	"MTFU","NEDT","NEGS","NHAR","NLIN","NOAD","NOAV","NOIS",
	"NOPD","NOPK","NOPP","NOTR","NPWT","OAMP","OTMP","OVER",
	"P3DV","P3LV","PAMP","PANG","PARE","PARO","PATH","PATT",
	"PCCU","PCLS","PCSR","PDEV","PDGN","PDRP","PERI","PHPN",
	"PHPP","PJIT","PKDV","PLAN","PLAR","PLEG","PLID","PLSE",
	"PLSI","PLWD","PMCU","PMFQ","PMSR","PODN","POSI","POSS",
	"POWA","POWP","POWR","PPOS","PPWT","PRCD","PRDF","PRFR",
	"PRIO","PROA","PROF","PRSA","PRSG","PRSR","PSHI","PSHT",
	"PSPC","PSPE","PSPT","PWRL","QFAC","QUAD","RADL","RADR",
	"RDNC","REAC","REFF","REFI","REFM","REFP","REFR","REFU",
	"REFV","REFX","RELB","RELH","RELW","REPT","RESB","RESI",
	"RESP","RESR","RING","RISE","RLBR","RLVL","RMNS","RMOD",
	"RMPS","ROUN","RPDV","RPEC","RPHF","RPLD","RPLE","RPLI",
	"RPLX","RSPH","RSPO","RSPT","RSPZ","RTRS","SASP","SATM",
	"SBCF","SBCM","SBEV","SBFM","SBTO","SCNT","SDEL","SERL",
	"SERM","SESA","SETT","SGNO","SGTF","SHFS","SIMU","SITF",
	"SKEW","SLEW","SLRA","SLRG","SLRR","SLSD","SLSL","SMAV",
	"SMPL","SMPW","SMTH","SNAD","SNSR","SPCG","SPED","SPGR",
	"SPTM","SQD1","SQD2","SQD3","SQTD","SQTR","SRFR","SSMD",
	"STAT","STBM","STIM","STLN","STMH","STMO","STMP","STMR",
	"STMZ","STOP","STPA","STPG","STPR","STPT","STRD","STRT",
	"STUT","STWD","SUSP","SVCP","SVFM","SVTO","SVWV","SWBT",
	"SWPT","SWRA","SYDL","SYEV","SYNC","TASP","TASY","TCAP",
	"TCUR","TEFC","TEMP","TEQL","TEQT","TGMD","TGPL","TGTA",
	"TGTD","TGTH","TGTP","TGTR","THRT","TIEV","TILT","TIME",
	"TIMP","TIND","TIUN","TIWH","TJIT","TLAX","TMON","TOPA",
	"TOPG","TOPR","TORQ","TPHD","TPHY","TREA","TRES","TRGS",
	"TRIG","TRLV","TRN0","TRN1","TRNP","TRNS","TROL","TRSL",
	"TRUE","TRUN","TRWH","TSAC","TSCC","TSIM","TSPC","TSTF",
	"TTMP","TVOL","TYPE","UNDR","UNFY","UNIT","UUPL","UUTL",
	"UUTT","VALU","VBAC","VBAN","VBAP","VBPP","VBRT","VBTR",
	"VDIV","VEAO","VEDL","VEEO","VEFO","VEGF","VETF","VFOV",
	"VINS","VIST","VLAE","VLAV","VLPK","VLPP","VLT0","VLT1",
	"VLTL","VLTQ","VLTR","VLTS","VOLF","VOLR","VOLT","VPHF",
	"VPHM","VPKN","VPKP","VRAG","VRMS","VSRM","VTAG","VTOF",
	"WAIT","WAVE","WDLN","WDRT","WGAP","WILD","WIND","WRDC",
	"WTRN","XAGR","XBAG","XTAR","YAGR","YBAG","YTAR","ZAMP",
	"ZCRS","ZERO",};
//
//	MODULETYPES
//
DECLAREC int t__cnt = T__CNT;
DECLAREC char *TCiilTxt [] = {
	"","ACS","API","ARB","ASA","DCS","DIG","DMM",
	"DWG","FNG","FTM","MBT","MFU","MTR","PAT","PCM",
	"PLG","PSS","PWM","QAL","RSV","RSY","SNG","SRS",
	"VDG",};
//
//	DIMS-A
//
DECLAREC int d__cnt = D__CNT;
DECLAREC char *DCiilTxt [] = {
	"","1553A","1553B","ALLLS","AMI","AR429","ARDC","BIP",
	"CONMD","CONRT","CSM","CSN","CSOC","HDB","ICAN","ICAO",
	"IEEE488","LNGTH","MASTR","MIP","MONTR","NRZ","OFF","ON",
	"PARA","PRIM","PRTY","REDT","RS232","RS422","RTCON","RTRT",
	"RZ","SERL","SERM","SLAVE","SYNC","TLKLS","TR","WADC",
	};
//
//	DIMS-B
//
DECLAREC int r__cnt = R__CNT;
DECLAREC char *RCiilTxt [] = {
	"",};
extern int doDWG_Close ();
extern int doDWG_Connect ();
extern int doDWG_Disconnect ();
extern int doDWG_Fetch ();
extern int doDWG_Init ();
extern int doDWG_Load ();
extern int doDWG_Open ();
extern int doDWG_Reset ();
extern int doDWG_Setup ();
extern int doDWG_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"DWG_1:CH1",
	"DWG_1:CH10",
	"DWG_1:CH11",
	"DWG_1:CH12",
	"DWG_1:CH17",
	"DWG_1:CH22",
	"DWG_1:CH31",
	"DWG_1:CH32",
	"DWG_1:CH51",
	"DWG_1:CH52",
	"DWG_1:CH53",
	"DWG_1:CH62",
	"DWG_1:CH63",
	"DWG_1:CH72",
	"DWG_1:CH73",
	"DWG_1:CH80",
};
DECLAREC int DevCnt = 18;
int CCALLBACK Wrapper_DWG_1_1_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_1_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_10_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Close(void)
{   
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_11_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_12_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_17_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_22_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_31_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_32_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_51_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_52_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_53_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_62_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_63_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_72_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_73_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Close(void)
{
	if (doDWG_Close() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Connect(void)
{
	if (doDWG_Connect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Disconnect(void)
{
	if (doDWG_Disconnect() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Fetch(void)
{
	if (doDWG_Fetch() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Init(void)
{
	if (doDWG_Init() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Load(void)
{
	if (doDWG_Load() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Open(void)
{
	if (doDWG_Open() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Reset(void)
{
	if (doDWG_Reset() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Setup(void)
{
	if (doDWG_Setup() < 0)
		BusErr ("DWG_1");
	return 0;
}
int CCALLBACK Wrapper_DWG_1_80_Status(void)
{
	if (doDWG_Status() < 0)
		BusErr ("DWG_1");
	return 0;
}
//
//	Device Init
//
EXTERNC MODDAT *BldModDat (MODDAT *, short);
DECLAREC int DevInx ()
{
	MODDAT *p_mod;
	DevDat = (DEVDAT *) Room (DevCnt *  sizeof (DEVDAT));
//
//	Controller:CH0
//
	p_mod = (MODDAT *) 0;
	DevDat[1].d_modlst = p_mod;
	DevDat[1].d_fncP = 0;
	DevDat[1].d_acts[A_STA] = doDcl;
	DevDat[1].d_acts[A_DIS] = doUnload;
	DevDat[1].d_acts[A_OPN] = doOpen;
//
//	DWG_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ITER);  // iterate
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SDEL);  // sense-delay
	p_mod = BldModDat (p_mod, (short) M_SNSR);  // sense-rate
	p_mod = BldModDat (p_mod, (short) M_STMR);  // stim-rate
	p_mod = BldModDat (p_mod, (short) M_EVXE);  // event-sense
	p_mod = BldModDat (p_mod, (short) M_EVXM);  // event-stim
	p_mod = BldModDat (p_mod, (short) M_DTMD);  // do-timed-digital
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_DWG_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_DWG_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_DWG_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_DWG_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_DWG_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_DWG_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_DWG_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_DWG_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_DWG_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_DWG_1_1_Status;
//
//	DWG_1:CH10
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_STIM);  // stim
	p_mod = BldModDat (p_mod, (short) M_STMH);  // stim-hiz
	p_mod = BldModDat (p_mod, (short) M_STMO);  // stim-one
	p_mod = BldModDat (p_mod, (short) M_STMZ);  // stim-zero
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLT1);  // voltage-one
	p_mod = BldModDat (p_mod, (short) M_VLTQ);  // voltage-quies
	p_mod = BldModDat (p_mod, (short) M_VLT0);  // voltage-zero
	p_mod = BldModDat (p_mod, (short) M_TYPE);  // type
	p_mod = BldModDat (p_mod, (short) M_REPT);  // repeat
	p_mod = BldModDat (p_mod, (short) M_ISTI);  // illegal-state-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 10;
	DevDat[3].d_acts[A_CLS] = Wrapper_DWG_1_10_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_DWG_1_10_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_DWG_1_10_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_DWG_1_10_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_DWG_1_10_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_DWG_1_10_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_DWG_1_10_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_DWG_1_10_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_DWG_1_10_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_DWG_1_10_Status;
//
//	DWG_1:CH11
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VALU);  // value
	p_mod = BldModDat (p_mod, (short) M_VLT1);  // voltage-one
	p_mod = BldModDat (p_mod, (short) M_VLT0);  // voltage-zero
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 11;
	DevDat[4].d_acts[A_CLS] = Wrapper_DWG_1_11_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_DWG_1_11_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_DWG_1_11_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_DWG_1_11_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_DWG_1_11_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_DWG_1_11_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_DWG_1_11_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_DWG_1_11_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_DWG_1_11_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_DWG_1_11_Status;
//
//	DWG_1:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VALU);  // value
	p_mod = BldModDat (p_mod, (short) M_VLT1);  // voltage-one
	p_mod = BldModDat (p_mod, (short) M_VLT0);  // voltage-zero
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 12;
	DevDat[5].d_acts[A_CLS] = Wrapper_DWG_1_12_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_DWG_1_12_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_DWG_1_12_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_DWG_1_12_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_DWG_1_12_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_DWG_1_12_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_DWG_1_12_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_DWG_1_12_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_DWG_1_12_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_DWG_1_12_Status;
//
//	DWG_1:CH17
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CUR1);  // current-one
	p_mod = BldModDat (p_mod, (short) M_CUR0);  // current-zero
	p_mod = BldModDat (p_mod, (short) M_VALU);  // value
	p_mod = BldModDat (p_mod, (short) M_VLT1);  // voltage-one
	p_mod = BldModDat (p_mod, (short) M_VLTQ);  // voltage-quies
	p_mod = BldModDat (p_mod, (short) M_VLT0);  // voltage-zero
	p_mod = BldModDat (p_mod, (short) M_WRDC);  // word-count
	p_mod = BldModDat (p_mod, (short) M_TYPE);  // type
	p_mod = BldModDat (p_mod, (short) M_FLTC);  // fault-count
	p_mod = BldModDat (p_mod, (short) M_HIZZ);  // hiz
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 17;
	DevDat[6].d_acts[A_CLS] = Wrapper_DWG_1_17_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_DWG_1_17_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_DWG_1_17_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_DWG_1_17_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_DWG_1_17_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_DWG_1_17_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_DWG_1_17_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_DWG_1_17_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_DWG_1_17_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_DWG_1_17_Status;
//
//	DWG_1:CH22
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CUR1);  // current-one
	p_mod = BldModDat (p_mod, (short) M_CUR0);  // current-zero
	p_mod = BldModDat (p_mod, (short) M_VALU);  // value
	p_mod = BldModDat (p_mod, (short) M_VLT1);  // voltage-one
	p_mod = BldModDat (p_mod, (short) M_VLTQ);  // voltage-quies
	p_mod = BldModDat (p_mod, (short) M_VLT0);  // voltage-zero
	p_mod = BldModDat (p_mod, (short) M_WRDC);  // word-count
	p_mod = BldModDat (p_mod, (short) M_TYPE);  // type
	p_mod = BldModDat (p_mod, (short) M_ERRO);  // error
	p_mod = BldModDat (p_mod, (short) M_ERRI);  // error-index
	p_mod = BldModDat (p_mod, (short) M_FLTC);  // fault-count
	p_mod = BldModDat (p_mod, (short) M_HIZZ);  // hiz
	p_mod = BldModDat (p_mod, (short) M_MASK);  // mask-one
	p_mod = BldModDat (p_mod, (short) M_MSKZ);  // mask-zero
	p_mod = BldModDat (p_mod, (short) M_REFX);  // ref
	p_mod = BldModDat (p_mod, (short) M_SVCP);  // save-comp
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 22;
	DevDat[7].d_acts[A_CLS] = Wrapper_DWG_1_22_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_DWG_1_22_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_DWG_1_22_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_DWG_1_22_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_DWG_1_22_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_DWG_1_22_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_DWG_1_22_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_DWG_1_22_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_DWG_1_22_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_DWG_1_22_Status;
//
//	DWG_1:CH31
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VALU);  // value
	p_mod = BldModDat (p_mod, (short) M_VLT1);  // voltage-one
	p_mod = BldModDat (p_mod, (short) M_VLT0);  // voltage-zero
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 31;
	DevDat[8].d_acts[A_CLS] = Wrapper_DWG_1_31_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_DWG_1_31_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_DWG_1_31_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_DWG_1_31_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_DWG_1_31_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_DWG_1_31_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_DWG_1_31_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_DWG_1_31_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_DWG_1_31_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_DWG_1_31_Status;
//
//	DWG_1:CH32
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VALU);  // value
	p_mod = BldModDat (p_mod, (short) M_VLT1);  // voltage-one
	p_mod = BldModDat (p_mod, (short) M_VLT0);  // voltage-zero
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 32;
	DevDat[9].d_acts[A_CLS] = Wrapper_DWG_1_32_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_DWG_1_32_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_DWG_1_32_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_DWG_1_32_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_DWG_1_32_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_DWG_1_32_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_DWG_1_32_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_DWG_1_32_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_DWG_1_32_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_DWG_1_32_Status;
//
//	DWG_1:CH51
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_VEDL);  // event-delay-value
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 51;
	DevDat[10].d_acts[A_CLS] = Wrapper_DWG_1_51_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_DWG_1_51_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_DWG_1_51_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_DWG_1_51_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_DWG_1_51_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_DWG_1_51_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_DWG_1_51_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_DWG_1_51_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_DWG_1_51_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_DWG_1_51_Status;
//
//	DWG_1:CH52
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_VEDL);  // event-delay-value
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 52;
	DevDat[11].d_acts[A_CLS] = Wrapper_DWG_1_52_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_DWG_1_52_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_DWG_1_52_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_DWG_1_52_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_DWG_1_52_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_DWG_1_52_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_DWG_1_52_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_DWG_1_52_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_DWG_1_52_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_DWG_1_52_Status;
//
//	DWG_1:CH53
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_VEDL);  // event-delay-value
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 53;
	DevDat[12].d_acts[A_CLS] = Wrapper_DWG_1_53_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_DWG_1_53_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_DWG_1_53_Disconnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_DWG_1_53_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_DWG_1_53_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_DWG_1_53_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_DWG_1_53_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_DWG_1_53_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_DWG_1_53_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_DWG_1_53_Status;
//
//	DWG_1:CH62
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_TIME);  // time
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 62;
	DevDat[13].d_acts[A_CLS] = Wrapper_DWG_1_62_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_DWG_1_62_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_DWG_1_62_Disconnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_DWG_1_62_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_DWG_1_62_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_DWG_1_62_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_DWG_1_62_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_DWG_1_62_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_DWG_1_62_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_DWG_1_62_Status;
//
//	DWG_1:CH63
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_TIME);  // time
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 63;
	DevDat[14].d_acts[A_CLS] = Wrapper_DWG_1_63_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_DWG_1_63_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_DWG_1_63_Disconnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_DWG_1_63_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_DWG_1_63_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_DWG_1_63_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_DWG_1_63_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_DWG_1_63_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_DWG_1_63_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_DWG_1_63_Status;
//
//	DWG_1:CH72
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 72;
	DevDat[15].d_acts[A_CLS] = Wrapper_DWG_1_72_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_DWG_1_72_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_DWG_1_72_Disconnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_DWG_1_72_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_DWG_1_72_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_DWG_1_72_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_DWG_1_72_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_DWG_1_72_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_DWG_1_72_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_DWG_1_72_Status;
//
//	DWG_1:CH73
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 73;
	DevDat[16].d_acts[A_CLS] = Wrapper_DWG_1_73_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_DWG_1_73_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_DWG_1_73_Disconnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_DWG_1_73_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_DWG_1_73_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_DWG_1_73_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_DWG_1_73_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_DWG_1_73_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_DWG_1_73_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_DWG_1_73_Status;
//
//	DWG_1:CH80
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_TIME);  // time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 80;
	DevDat[17].d_acts[A_CLS] = Wrapper_DWG_1_80_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_DWG_1_80_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_DWG_1_80_Disconnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_DWG_1_80_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_DWG_1_80_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_DWG_1_80_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_DWG_1_80_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_DWG_1_80_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_DWG_1_80_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_DWG_1_80_Status;
	return 0;
}
