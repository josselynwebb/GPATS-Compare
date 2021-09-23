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
extern int doARB_GEN_Close ();
extern int doARB_GEN_Connect ();
extern int doARB_GEN_Disconnect ();
extern int doARB_GEN_Fetch ();
extern int doARB_GEN_Init ();
extern int doARB_GEN_Load ();
extern int doARB_GEN_Open ();
extern int doARB_GEN_Reset ();
extern int doARB_GEN_Setup ();
extern int doARB_GEN_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"ARB_GEN_1:CH1",
	"ARB_GEN_1:CH102",
	"ARB_GEN_1:CH2",
	"ARB_GEN_1:CH3",
	"ARB_GEN_1:CH4",
	"ARB_GEN_1:CH6",
	"ARB_GEN_1:CH7",
};
DECLAREC int DevCnt = 9;
int CCALLBACK Wrapper_ARB_GEN_1_1_Close(void)
{
	if (doARB_GEN_Close() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Connect(void)
{
	if (doARB_GEN_Connect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Disconnect(void)
{
	if (doARB_GEN_Disconnect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Fetch(void)
{
	if (doARB_GEN_Fetch() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Init(void)
{
	if (doARB_GEN_Init() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Load(void)
{
	if (doARB_GEN_Load() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Open(void)
{
	if (doARB_GEN_Open() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Reset(void)
{
	if (doARB_GEN_Reset() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Setup(void)
{
	if (doARB_GEN_Setup() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_1_Status(void)
{
	if (doARB_GEN_Status() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Close(void)
{
	if (doARB_GEN_Close() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Connect(void)
{
	if (doARB_GEN_Connect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Disconnect(void)
{
	if (doARB_GEN_Disconnect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Fetch(void)
{
	if (doARB_GEN_Fetch() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Init(void)
{
	if (doARB_GEN_Init() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Load(void)
{
	if (doARB_GEN_Load() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Open(void)
{
	if (doARB_GEN_Open() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Reset(void)
{
	if (doARB_GEN_Reset() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Setup(void)
{
	if (doARB_GEN_Setup() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_102_Status(void)
{
	if (doARB_GEN_Status() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Close(void)
{
	if (doARB_GEN_Close() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Connect(void)
{
	if (doARB_GEN_Connect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Disconnect(void)
{
	if (doARB_GEN_Disconnect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Fetch(void)
{
	if (doARB_GEN_Fetch() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Init(void)
{
	if (doARB_GEN_Init() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Load(void)
{
	if (doARB_GEN_Load() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Open(void)
{
	if (doARB_GEN_Open() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Reset(void)
{
	if (doARB_GEN_Reset() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Setup(void)
{
	if (doARB_GEN_Setup() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_2_Status(void)
{
	if (doARB_GEN_Status() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Close(void)
{
	if (doARB_GEN_Close() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Connect(void)
{
	if (doARB_GEN_Connect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Disconnect(void)
{
	if (doARB_GEN_Disconnect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Fetch(void)
{
	if (doARB_GEN_Fetch() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Init(void)
{
	if (doARB_GEN_Init() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Load(void)
{
	if (doARB_GEN_Load() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Open(void)
{
	if (doARB_GEN_Open() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Reset(void)
{
	if (doARB_GEN_Reset() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Setup(void)
{
	if (doARB_GEN_Setup() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_3_Status(void)
{
	if (doARB_GEN_Status() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Close(void)
{
	if (doARB_GEN_Close() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Connect(void)
{
	if (doARB_GEN_Connect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Disconnect(void)
{
	if (doARB_GEN_Disconnect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Fetch(void)
{
	if (doARB_GEN_Fetch() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Init(void)
{
	if (doARB_GEN_Init() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Load(void)
{
	if (doARB_GEN_Load() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Open(void)
{
	if (doARB_GEN_Open() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Reset(void)
{
	if (doARB_GEN_Reset() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Setup(void)
{
	if (doARB_GEN_Setup() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_4_Status(void)
{
	if (doARB_GEN_Status() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Close(void)
{
	if (doARB_GEN_Close() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Connect(void)
{
	if (doARB_GEN_Connect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Disconnect(void)
{
	if (doARB_GEN_Disconnect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Fetch(void)
{
	if (doARB_GEN_Fetch() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Init(void)
{
	if (doARB_GEN_Init() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Load(void)
{
	if (doARB_GEN_Load() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Open(void)
{
	if (doARB_GEN_Open() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Reset(void)
{
	if (doARB_GEN_Reset() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Setup(void)
{
	if (doARB_GEN_Setup() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_6_Status(void)
{
	if (doARB_GEN_Status() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Close(void)
{
	if (doARB_GEN_Close() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Connect(void)
{
	if (doARB_GEN_Connect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Disconnect(void)
{
	if (doARB_GEN_Disconnect() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Fetch(void)
{
	if (doARB_GEN_Fetch() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Init(void)
{
	if (doARB_GEN_Init() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Load(void)
{
	if (doARB_GEN_Load() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Open(void)
{
	if (doARB_GEN_Open() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Reset(void)
{
	if (doARB_GEN_Reset() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Setup(void)
{
	if (doARB_GEN_Setup() < 0)
		BusErr ("ARB_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_ARB_GEN_1_7_Status(void)
{
	if (doARB_GEN_Status() < 0)
		BusErr ("ARB_GEN_1");
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
//	ARB_GEN_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_AGER);  // age-rate
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_ARB_GEN_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_ARB_GEN_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_ARB_GEN_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_ARB_GEN_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_ARB_GEN_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_ARB_GEN_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_ARB_GEN_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_ARB_GEN_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_ARB_GEN_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_ARB_GEN_1_1_Status;
//
//	ARB_GEN_1:CH102
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 102;
	DevDat[3].d_acts[A_CLS] = Wrapper_ARB_GEN_1_102_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_ARB_GEN_1_102_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_ARB_GEN_1_102_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_ARB_GEN_1_102_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_ARB_GEN_1_102_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_ARB_GEN_1_102_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_ARB_GEN_1_102_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_ARB_GEN_1_102_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_ARB_GEN_1_102_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_ARB_GEN_1_102_Status;
//
//	ARB_GEN_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_BURR);  // burst-rep-rate
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 2;
	DevDat[4].d_acts[A_CLS] = Wrapper_ARB_GEN_1_2_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_ARB_GEN_1_2_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_ARB_GEN_1_2_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_ARB_GEN_1_2_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_ARB_GEN_1_2_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_ARB_GEN_1_2_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_ARB_GEN_1_2_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_ARB_GEN_1_2_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_ARB_GEN_1_2_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_ARB_GEN_1_2_Status;
//
//	ARB_GEN_1:CH3
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_BURR);  // burst-rep-rate
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 3;
	DevDat[5].d_acts[A_CLS] = Wrapper_ARB_GEN_1_3_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_ARB_GEN_1_3_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_ARB_GEN_1_3_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_ARB_GEN_1_3_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_ARB_GEN_1_3_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_ARB_GEN_1_3_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_ARB_GEN_1_3_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_ARB_GEN_1_3_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_ARB_GEN_1_3_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_ARB_GEN_1_3_Status;
//
//	ARB_GEN_1:CH4
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SASP);  // sample-spacing
	p_mod = BldModDat (p_mod, (short) M_STIM);  // stim
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 4;
	DevDat[6].d_acts[A_CLS] = Wrapper_ARB_GEN_1_4_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_ARB_GEN_1_4_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_ARB_GEN_1_4_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_ARB_GEN_1_4_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_ARB_GEN_1_4_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_ARB_GEN_1_4_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_ARB_GEN_1_4_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_ARB_GEN_1_4_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_ARB_GEN_1_4_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_ARB_GEN_1_4_Status;
//
//	ARB_GEN_1:CH6
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_BURR);  // burst-rep-rate
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 6;
	DevDat[7].d_acts[A_CLS] = Wrapper_ARB_GEN_1_6_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_ARB_GEN_1_6_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_ARB_GEN_1_6_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_ARB_GEN_1_6_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_ARB_GEN_1_6_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_ARB_GEN_1_6_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_ARB_GEN_1_6_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_ARB_GEN_1_6_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_ARB_GEN_1_6_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_ARB_GEN_1_6_Status;
//
//	ARB_GEN_1:CH7
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 7;
	DevDat[8].d_acts[A_CLS] = Wrapper_ARB_GEN_1_7_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_ARB_GEN_1_7_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_ARB_GEN_1_7_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_ARB_GEN_1_7_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_ARB_GEN_1_7_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_ARB_GEN_1_7_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_ARB_GEN_1_7_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_ARB_GEN_1_7_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_ARB_GEN_1_7_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_ARB_GEN_1_7_Status;
	return 0;
}
