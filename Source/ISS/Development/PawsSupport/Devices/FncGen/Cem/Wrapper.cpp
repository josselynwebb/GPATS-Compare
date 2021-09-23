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
	"CLX","COM","DCF","DCS","DGT","DIS","DME","DOP",
	"EAR","EMF","EPW","EVS","EVT","EXC","FLU","FMS",
	"HEA","IFF","ILS","IMP","INF","LAS","LCL","LDT",
	"LGT","LLD","LRF","LTR","MAN","MDS","MIF","MIL",
	"PAC","PAM","PAT","PDC","PDP","PDT","PMS","RDN",
	"RDS","RPS","RSL","RTN","SCS","SHT","SIM","SIN",
	"SQW","STM","STS","SYN","TAC","TDG","TED","TMI",
	"TMR","TRI","VBR","VID","VOR","WAV",};
//
//	MODIFIERS
//
DECLAREC int m__cnt = M__CNT;
DECLAREC char *MCiilTxt [] = {
	"","ACCF","ACCP","ADFM","ADLN","ADTO","AGER","ALLW",
	"ALTI","ALTR","AMBT","AMCP","AMCU","AMFQ","AMMC","AMMF",
	"AMPL","AMSH","AMSR","ANAC","ANAX","ANAY","ANAZ","ANGL",
	"ANGP","ANGT","ANGX","ANGY","ANGZ","ANRT","ANRX","ANRY",
	"ANRZ","ANSD","ATMS","ATTE","ATTN","AUCO","BAND","BARP",
	"BDTH","BITP","BITR","BKPH","BLKT","BRAN","BTRN","BURD",
	"BURR","BURS","BUSM","BUSS","CAMG","CAMP","CAPA","CCOM",
	"CDAT","CFRQ","CHAN","CHCT","CHID","CHIT","CHRM","CLKS",
	"CMCH","CMDW","CMPL","CMTO","CMWB","CMWV","COMD","COND",
	"COUN","CPHS","CPKN","CPKP","CPLG","CRSD","CRSF","CSTS",
	"CTRQ","CUPK","CUPP","CUR0","CUR1","CURA","CURI","CURL",
	"CURQ","CURR","CURT","CWLV","DATA","DATL","DATP","DATS",
	"DATT","DATW","DBLI","DBND","DBRC","DBRS","DCOF","DDMD",
	"DEEM","DELA","DEST","DEWP","DFBA","DIFR","DIFT","DIGS",
	"DISP","DIST","DIVG","DIVS","DMDS","DPFR","DPSH","DROO",
	"DSFC","DSTR","DSTX","DSTY","DSTZ","DTCT","DTER","DTIL",
	"DTMD","DTOR","DTSC","DTSP","DTST","DUTY","DVPN","DVPP",
	"DWBT","DYRA","EDLN","EDUT","EFCY","EFFI","EGDR","EINM",
	"ERRI","ERRO","EVAO","EVDL","EVEO","EVEV","EVFO","EVGB",
	"EVGF","EVGR","EVGT","EVOU","EVSB","EVSF","EVSL","EVSW",
	"EVTF","EVTI","EVTR","EVTT","EVUN","EVWH","EVXE","EVXM",
	"EXAE","EXNM","EXPO","FALL","FCLN","FCNT","FDST","FDVW",
	"FIAL","FILT","FLTC","FLTS","FLUT","FMCP","FMCU","FMFQ",
	"FMSR","FRCE","FRCR","FREQ","FRMT","FRQ0","FRQ1","FRQD",
	"FRQP","FRQQ","FRQR","FRQW","FUEL","FXDN","FXIP","FXQD",
	"GAMA","GSLP","GSRE","HAPW","HARM","HARN","HARP","HARV",
	"HFOV","HIZZ","HLAE","HMDF","HRAG","HSRM","HTAG","HTOF",
	"HUMY","IASP","ICWB","IDSE","IDSF","IDSG","IDSM","IDWB",
	"IJIT","ILLU","INDU","INTG","INTL","IRAT","ISTI","ISWB",
	"ITER","ITRO","IVCW","IVDL","IVDP","IVDS","IVDT","IVDW",
	"IVMG","IVOA","IVRT","IVSW","IVWC","IVWG","IVWL","IVZA",
	"IVZC","LDFM","LDTO","LDVW","LINE","LIPF","LMDF","LMIN",
	"LOCL","LRAN","LSAE","LSTG","LUMF","LUMI","LUMT","LVLO",
	"LVLZ","MAGB","MAGR","MAMP","MANI","MASF","MASK","MATH",
	"MAXT","MBAT","MDPN","MDPP","MDSC","MGAP","MMOD","MODD",
	"MODE","MODF","MODO","MODP","MPFM","MPTO","MRCO","MRKB",
	"MRTD","MSKZ","MSNR","MTFD","MTFP","MTFU","NEDT","NEGS",
	"NHAR","NLIN","NOAD","NOAV","NOIS","NOPD","NOPK","NOPP",
	"NOTR","NPWT","OAMP","OTMP","OVER","P3DV","P3LV","PAMP",
	"PANG","PARE","PARO","PAST","PATH","PATN","PATT","PCCU",
	"PCLS","PCSR","PDEV","PDGN","PDRP","PDVN","PDVP","PERI",
	"PEST","PHPN","PHPP","PJIT","PKDV","PLAN","PLAR","PLEG",
	"PLID","PLSE","PLSI","PLWD","PMCU","PMFQ","PMSR","PODN",
	"POSI","POSS","POWA","POWP","POWR","PPOS","PPST","PPWT",
	"PRCD","PRDF","PRFR","PRIO","PROA","PROF","PRSA","PRSG",
	"PRSR","PRTY","PSHI","PSHT","PSPC","PSPE","PSPT","PSRC",
	"PWRL","QFAC","QUAD","RADL","RADR","RAIL","RASP","RAST",
	"RCVS","RDNC","REAC","REFF","REFI","REFM","REFP","REFR",
	"REFS","REFU","REFV","REFX","RELB","RELH","RELW","REPT",
	"RERR","RESB","RESI","RESP","RESR","RING","RISE","RLBR",
	"RLVL","RMNS","RMOD","RMPS","ROUN","RPDV","RPEC","RPHF",
	"RPLD","RPLE","RPLI","RPLX","RSPH","RSPO","RSPT","RSPZ",
	"RTRS","SASP","SATM","SBAT","SBCF","SBCM","SBEV","SBFM",
	"SBTO","SCNT","SDEL","SERL","SERM","SESA","SETT","SGNO",
	"SGTF","SHFS","SIMU","SITF","SKEW","SLEW","SLRA","SLRG",
	"SLRR","SLSD","SLSL","SMAV","SMPL","SMPW","SMTH","SNAD",
	"SNSR","SPCG","SPED","SPGR","SPRT","SPTM","SQD1","SQD2",
	"SQD3","SQTD","SQTR","SRFR","SSMD","STAT","STBM","STIM",
	"STLN","STMH","STMO","STMP","STMR","STMZ","STOP","STPA",
	"STPB","STPG","STPR","STPT","STRD","STRT","STUT","STWD",
	"SUSP","SVCP","SVFM","SVTO","SVWV","SWBT","SWPT","SWRA",
	"SYDL","SYEV","SYNC","TASP","TASY","TCAP","TCBT","TCLT",
	"TCRT","TCTP","TCUR","TDAT","TEFC","TEMP","TEQL","TEQT",
	"TGMD","TGPL","TGTA","TGTD","TGTH","TGTP","TGTR","TGTS",
	"THRT","TIEV","TILT","TIME","TIMP","TIND","TIUN","TIWH",
	"TJIT","TLAX","TMON","TOPA","TOPG","TOPR","TORQ","TPHD",
	"TPHY","TREA","TRES","TRGS","TRIG","TRLV","TRN0","TRN1",
	"TRNP","TRNS","TROL","TRSL","TRUE","TRUN","TRWH","TSAC",
	"TSCC","TSIM","TSPC","TSTF","TTMP","TVOL","TYPE","UNDR",
	"UNFY","UNIT","UUPL","UUTL","UUTT","VALU","VBAC","VBAN",
	"VBAP","VBPP","VBRT","VBTR","VDIV","VEAO","VEDL","VEEO",
	"VEFO","VEGF","VETF","VFOV","VINS","VIST","VLAE","VLAV",
	"VLPK","VLPP","VLT0","VLT1","VLTL","VLTQ","VLTR","VLTS",
	"VOLF","VOLR","VOLT","VPHF","VPHM","VPKN","VPKP","VRAG",
	"VRMS","VSRM","VTAG","VTOF","WAIT","WAVE","WDLN","WDRT",
	"WGAP","WILD","WIND","WRDC","WTRN","XACE","XAGR","XBAG",
	"XTAR","YACE","YAGR","YBAG","YTAR","ZAMP","ZCRS","ZERO",
	};
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
	"","1000BASET","100BASET","10BASET","1553A","1553B","AFAPD","ALLLS",
	"AMI","AR429","ARDC","BIP","CANA","CANB","CDDI","CONMD",
	"CONRT","CSM","CSN","CSOC","HDB","ICAN","ICAO","IDL",
	"IEEE488","LNGTH","MASTR","MIC","MIP","MONTR","MTS","NRZ",
	"OFF","ON","PARA","PRIM","PRTY","REDT","RS232","RS422",
	"RS485","RTCON","RTRT","RZ","SERL","SERM","SLAVE","SYNC",
	"TACFIRE","TLKLS","TR","WADC",};
//
//	DIMS-B
//
DECLAREC int r__cnt = R__CNT;
DECLAREC char *RCiilTxt [] = {
	"",};
extern int doFUNC_GEN_Close ();
extern int doFUNC_GEN_Connect ();
extern int doFUNC_GEN_Disconnect ();
extern int doFUNC_GEN_Fetch ();
extern int doFUNC_GEN_Init ();
extern int doFUNC_GEN_Load ();
extern int doFUNC_GEN_Open ();
extern int doFUNC_GEN_Reset ();
extern int doFUNC_GEN_Setup ();
extern int doFUNC_GEN_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"FUNC_GEN_1:CH1",
	"FUNC_GEN_1:CH102",
	"FUNC_GEN_1:CH103",
	"FUNC_GEN_1:CH104",
	"FUNC_GEN_1:CH2",
	"FUNC_GEN_1:CH21",
	"FUNC_GEN_1:CH22",
	"FUNC_GEN_1:CH23",
	"FUNC_GEN_1:CH24",
	"FUNC_GEN_1:CH25",
	"FUNC_GEN_1:CH26",
	"FUNC_GEN_1:CH27",
	"FUNC_GEN_1:CH28",
	"FUNC_GEN_1:CH29",
	"FUNC_GEN_1:CH3",
	"FUNC_GEN_1:CH4",
	"FUNC_GEN_1:CH5",
	"FUNC_GEN_1:CH6",
	"FUNC_GEN_1:CH7",
	"FUNC_GEN_1:CH8",
	"FUNC_GEN_1:CH9",
};
DECLAREC int DevCnt = 23;
int CCALLBACK Wrapper_FUNC_GEN_1_1_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_1_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_102_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_103_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_104_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_2_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_21_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_22_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_23_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_24_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_25_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_26_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_27_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_28_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_29_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_3_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_4_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_5_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_6_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_7_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_8_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Close(void)
{
	if (doFUNC_GEN_Close() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Connect(void)
{
	if (doFUNC_GEN_Connect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Disconnect(void)
{
	if (doFUNC_GEN_Disconnect() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Fetch(void)
{
	if (doFUNC_GEN_Fetch() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Init(void)
{
	if (doFUNC_GEN_Init() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Load(void)
{
	if (doFUNC_GEN_Load() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Open(void)
{
	if (doFUNC_GEN_Open() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Reset(void)
{
	if (doFUNC_GEN_Reset() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Setup(void)
{
	if (doFUNC_GEN_Setup() < 0)
		BusErr ("FUNC_GEN_1");
	return 0;
}
int CCALLBACK Wrapper_FUNC_GEN_1_9_Status(void)
{
	if (doFUNC_GEN_Status() < 0)
		BusErr ("FUNC_GEN_1");
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
//	FUNC_GEN_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_FUNC_GEN_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_FUNC_GEN_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_FUNC_GEN_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_FUNC_GEN_1_1_Status;
//
//	FUNC_GEN_1:CH102
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 102;
	DevDat[3].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_102_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_FUNC_GEN_1_102_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_102_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_102_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_FUNC_GEN_1_102_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_102_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_102_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_FUNC_GEN_1_102_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_102_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_FUNC_GEN_1_102_Status;
//
//	FUNC_GEN_1:CH103
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 103;
	DevDat[4].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_103_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_FUNC_GEN_1_103_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_103_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_103_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_FUNC_GEN_1_103_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_103_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_103_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_FUNC_GEN_1_103_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_103_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_FUNC_GEN_1_103_Status;
//
//	FUNC_GEN_1:CH104
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 104;
	DevDat[5].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_104_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_FUNC_GEN_1_104_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_104_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_104_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_FUNC_GEN_1_104_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_104_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_104_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_FUNC_GEN_1_104_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_104_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_FUNC_GEN_1_104_Status;
//
//	FUNC_GEN_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_BURR);  // burst-rep-rate
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 2;
	DevDat[6].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_2_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_FUNC_GEN_1_2_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_2_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_2_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_FUNC_GEN_1_2_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_2_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_2_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_FUNC_GEN_1_2_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_2_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_FUNC_GEN_1_2_Status;
//
//	FUNC_GEN_1:CH21
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 21;
	DevDat[7].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_21_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_FUNC_GEN_1_21_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_21_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_21_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_FUNC_GEN_1_21_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_21_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_21_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_FUNC_GEN_1_21_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_21_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_FUNC_GEN_1_21_Status;
//
//	FUNC_GEN_1:CH22
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_BURR);  // burst-rep-rate
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 22;
	DevDat[8].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_22_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_FUNC_GEN_1_22_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_22_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_22_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_FUNC_GEN_1_22_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_22_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_22_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_FUNC_GEN_1_22_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_22_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_FUNC_GEN_1_22_Status;
//
//	FUNC_GEN_1:CH23
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
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 23;
	DevDat[9].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_23_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_FUNC_GEN_1_23_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_23_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_23_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_FUNC_GEN_1_23_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_23_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_23_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_FUNC_GEN_1_23_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_23_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_FUNC_GEN_1_23_Status;
//
//	FUNC_GEN_1:CH24
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_BURR);  // burst-rep-rate
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 24;
	DevDat[10].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_24_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_FUNC_GEN_1_24_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_24_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_24_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_FUNC_GEN_1_24_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_24_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_24_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_FUNC_GEN_1_24_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_24_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_FUNC_GEN_1_24_Status;
//
//	FUNC_GEN_1:CH25
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 25;
	DevDat[11].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_25_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_FUNC_GEN_1_25_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_25_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_25_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_FUNC_GEN_1_25_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_25_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_25_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_FUNC_GEN_1_25_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_25_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_FUNC_GEN_1_25_Status;
//
//	FUNC_GEN_1:CH26
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 26;
	DevDat[12].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_26_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_FUNC_GEN_1_26_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_26_Disconnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_26_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_FUNC_GEN_1_26_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_26_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_26_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_FUNC_GEN_1_26_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_26_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_FUNC_GEN_1_26_Status;
//
//	FUNC_GEN_1:CH27
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_EXPO);  // exponent
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 27;
	DevDat[13].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_27_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_FUNC_GEN_1_27_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_27_Disconnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_27_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_FUNC_GEN_1_27_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_27_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_27_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_FUNC_GEN_1_27_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_27_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_FUNC_GEN_1_27_Status;
//
//	FUNC_GEN_1:CH28
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 28;
	DevDat[14].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_28_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_FUNC_GEN_1_28_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_28_Disconnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_28_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_FUNC_GEN_1_28_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_28_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_28_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_FUNC_GEN_1_28_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_28_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_FUNC_GEN_1_28_Status;
//
//	FUNC_GEN_1:CH29
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SASP);  // sample-spacing
	p_mod = BldModDat (p_mod, (short) M_STIM);  // stim
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 29;
	DevDat[15].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_29_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_FUNC_GEN_1_29_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_29_Disconnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_29_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_FUNC_GEN_1_29_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_29_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_29_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_FUNC_GEN_1_29_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_29_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_FUNC_GEN_1_29_Status;
//
//	FUNC_GEN_1:CH3
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
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 3;
	DevDat[16].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_3_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_FUNC_GEN_1_3_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_3_Disconnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_3_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_FUNC_GEN_1_3_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_3_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_3_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_FUNC_GEN_1_3_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_3_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_FUNC_GEN_1_3_Status;
//
//	FUNC_GEN_1:CH4
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_BURR);  // burst-rep-rate
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 4;
	DevDat[17].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_4_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_FUNC_GEN_1_4_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_4_Disconnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_4_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_FUNC_GEN_1_4_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_4_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_4_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_FUNC_GEN_1_4_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_4_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_FUNC_GEN_1_4_Status;
//
//	FUNC_GEN_1:CH5
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[18].d_modlst = p_mod;
	DevDat[18].d_fncP = 5;
	DevDat[18].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_5_Close;
	DevDat[18].d_acts[A_CON] = Wrapper_FUNC_GEN_1_5_Connect;
	DevDat[18].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_5_Disconnect;
	DevDat[18].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_5_Fetch;
	DevDat[18].d_acts[A_INX] = Wrapper_FUNC_GEN_1_5_Init;
	DevDat[18].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_5_Load;
	DevDat[18].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_5_Open;
	DevDat[18].d_acts[A_RST] = Wrapper_FUNC_GEN_1_5_Reset;
	DevDat[18].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_5_Setup;
	DevDat[18].d_acts[A_STA] = Wrapper_FUNC_GEN_1_5_Status;
//
//	FUNC_GEN_1:CH6
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[19].d_modlst = p_mod;
	DevDat[19].d_fncP = 6;
	DevDat[19].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_6_Close;
	DevDat[19].d_acts[A_CON] = Wrapper_FUNC_GEN_1_6_Connect;
	DevDat[19].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_6_Disconnect;
	DevDat[19].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_6_Fetch;
	DevDat[19].d_acts[A_INX] = Wrapper_FUNC_GEN_1_6_Init;
	DevDat[19].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_6_Load;
	DevDat[19].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_6_Open;
	DevDat[19].d_acts[A_RST] = Wrapper_FUNC_GEN_1_6_Reset;
	DevDat[19].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_6_Setup;
	DevDat[19].d_acts[A_STA] = Wrapper_FUNC_GEN_1_6_Status;
//
//	FUNC_GEN_1:CH7
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_BURS);  // burst
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_EXPO);  // exponent
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[20].d_modlst = p_mod;
	DevDat[20].d_fncP = 7;
	DevDat[20].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_7_Close;
	DevDat[20].d_acts[A_CON] = Wrapper_FUNC_GEN_1_7_Connect;
	DevDat[20].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_7_Disconnect;
	DevDat[20].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_7_Fetch;
	DevDat[20].d_acts[A_INX] = Wrapper_FUNC_GEN_1_7_Init;
	DevDat[20].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_7_Load;
	DevDat[20].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_7_Open;
	DevDat[20].d_acts[A_RST] = Wrapper_FUNC_GEN_1_7_Reset;
	DevDat[20].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_7_Setup;
	DevDat[20].d_acts[A_STA] = Wrapper_FUNC_GEN_1_7_Status;
//
//	FUNC_GEN_1:CH8
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[21].d_modlst = p_mod;
	DevDat[21].d_fncP = 8;
	DevDat[21].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_8_Close;
	DevDat[21].d_acts[A_CON] = Wrapper_FUNC_GEN_1_8_Connect;
	DevDat[21].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_8_Disconnect;
	DevDat[21].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_8_Fetch;
	DevDat[21].d_acts[A_INX] = Wrapper_FUNC_GEN_1_8_Init;
	DevDat[21].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_8_Load;
	DevDat[21].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_8_Open;
	DevDat[21].d_acts[A_RST] = Wrapper_FUNC_GEN_1_8_Reset;
	DevDat[21].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_8_Setup;
	DevDat[21].d_acts[A_STA] = Wrapper_FUNC_GEN_1_8_Status;
//
//	FUNC_GEN_1:CH9
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SASP);  // sample-spacing
	p_mod = BldModDat (p_mod, (short) M_STIM);  // stim
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVEO);  // event-each-occurrence
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_EVGB);  // event-gated-by
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[22].d_modlst = p_mod;
	DevDat[22].d_fncP = 9;
	DevDat[22].d_acts[A_CLS] = Wrapper_FUNC_GEN_1_9_Close;
	DevDat[22].d_acts[A_CON] = Wrapper_FUNC_GEN_1_9_Connect;
	DevDat[22].d_acts[A_DIS] = Wrapper_FUNC_GEN_1_9_Disconnect;
	DevDat[22].d_acts[A_FTH] = Wrapper_FUNC_GEN_1_9_Fetch;
	DevDat[22].d_acts[A_INX] = Wrapper_FUNC_GEN_1_9_Init;
	DevDat[22].d_acts[A_LOD] = Wrapper_FUNC_GEN_1_9_Load;
	DevDat[22].d_acts[A_OPN] = Wrapper_FUNC_GEN_1_9_Open;
	DevDat[22].d_acts[A_RST] = Wrapper_FUNC_GEN_1_9_Reset;
	DevDat[22].d_acts[A_FNC] = Wrapper_FUNC_GEN_1_9_Setup;
	DevDat[22].d_acts[A_STA] = Wrapper_FUNC_GEN_1_9_Status;
	return 0;
}
