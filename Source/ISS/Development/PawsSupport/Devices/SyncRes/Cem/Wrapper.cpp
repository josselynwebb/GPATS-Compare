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
	"HFOV","HIZZ","HLAE","HMDF","HP01","HP02","HP03","HP04",
	"HP05","HP06","HP07","HP08","HP09","HP10","HP11","HP12",
	"HP13","HP14","HP15","HP16","HPA0","HPA1","HPA2","HPA3",
	"HPA4","HPA5","HPA6","HPX1","HPX2","HPX3","HPX4","HPX5",
	"HPX6","HPX7","HPX8","HPX9","HPZ1","HPZ2","HPZ3","HPZ4",
	"HPZ5","HPZ6","HPZ7","HPZ8","HPZ9","HRAG","HSRM","HTAG",
	"HTOF","HUMY","HV01","HV02","HV03","HV04","HV05","HV06",
	"HV07","HV08","HV09","HV10","HV11","HV12","HV13","HV14",
	"HV15","HV16","HVA0","HVA1","HVA2","HVA3","HVA4","HVA5",
	"HVA6","HVX1","HVX2","HVX3","HVX4","HVX5","HVX6","HVX7",
	"HVX8","HVX9","HVZ1","HVZ2","HVZ3","HVZ4","HVZ5","HVZ6",
	"HVZ7","HVZ8","HVZ9","IASP","ICWB","IDSE","IDSF","IDSG",
	"IDSM","IDWB","IJIT","ILLU","INDU","INTG","INTL","IRAT",
	"ISTI","ISWB","ITER","ITRO","IVCW","IVDL","IVDP","IVDS",
	"IVDT","IVDW","IVMG","IVOA","IVRT","IVSW","IVWC","IVWG",
	"IVWL","IVZA","IVZC","LDFM","LDTO","LDVW","LINE","LIPF",
	"LMDF","LMIN","LOCL","LRAN","LSAE","LSTG","LUMF","LUMI",
	"LUMT","MAGB","MAGR","MAMP","MANI","MASF","MASK","MATH",
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
	"PTCP","PUDP","PWRL","QFAC","QUAD","RADL","RADR","RAIL",
	"RASP","RAST","RCVS","RDNC","REAC","REFF","REFI","REFM",
	"REFP","REFR","REFS","REFU","REFV","REFX","RELB","RELH",
	"RELW","REPT","RERR","RESB","RESI","RESP","RESR","RING",
	"RISE","RLBR","RLVL","RMNS","RMOD","RMPS","ROUN","RPDV",
	"RPEC","RPHF","RPLD","RPLE","RPLI","RPLX","RSPH","RSPO",
	"RSPT","RSPZ","RTRS","SASP","SATM","SBAT","SBCF","SBCM",
	"SBEV","SBFM","SBTO","SCNT","SDEL","SERL","SERM","SESA",
	"SETT","SGNO","SGTF","SHFS","SIMU","SITF","SKEW","SLEW",
	"SLRA","SLRG","SLRR","SLSD","SLSL","SMAV","SMPL","SMPW",
	"SMTH","SNAD","SNSR","SPCG","SPED","SPGR","SPRT","SPTM",
	"SQD1","SQD2","SQD3","SQTD","SQTR","SRFR","SSMD","STAT",
	"STBM","STIM","STLN","STMH","STMO","STMP","STMR","STMZ",
	"STOP","STPA","STPB","STPG","STPR","STPT","STRD","STRT",
	"STUT","STWD","SUSP","SVCP","SVFM","SVTO","SVWV","SWBT",
	"SWPT","SWRA","SYDL","SYEV","SYNC","TASP","TASY","TCAP",
	"TCBT","TCLT","TCRT","TCTP","TCUR","TDAT","TEFC","TEMP",
	"TEQL","TEQT","TERM","TGMD","TGPL","TGTA","TGTD","TGTH",
	"TGTP","TGTR","TGTS","THRT","TIEV","TILT","TIME","TIMP",
	"TIND","TIUN","TIWH","TJIT","TLAX","TMON","TOPA","TOPG",
	"TOPR","TORQ","TPHD","TPHY","TREA","TRES","TRGS","TRIG",
	"TRLV","TRN0","TRN1","TRNP","TRNS","TROL","TRSL","TRUE",
	"TRUN","TRWH","TSAC","TSCC","TSIM","TSPC","TSTF","TTMP",
	"TVOL","TYPE","UNDR","UNFY","UNIT","UUPL","UUTL","UUTT",
	"VALU","VBAC","VBAN","VBAP","VBPP","VBRT","VBTR","VDIV",
	"VEAO","VEDL","VEEO","VEFO","VEGF","VETF","VFOV","VINS",
	"VIST","VLAE","VLAV","VLPK","VLPP","VLT0","VLT1","VLTL",
	"VLTQ","VLTR","VLTS","VOLF","VOLR","VOLT","VPHF","VPHM",
	"VPKN","VPKP","VRAG","VRMS","VSRM","VTAG","VTOF","WAIT",
	"WAVE","WDLN","WDRT","WGAP","WILD","WIND","WRDC","WTRN",
	"XACE","XAGR","XBAG","XTAR","YACE","YAGR","YBAG","YTAR",
	"ZAMP","ZCRS","ZERO",};
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
	"","1553A","1553B","AFAPD","ALLLS","AMI","AR429","ARDC",
	"BIP","CANA","CANB","CDDI","CONMD","CONRT","CSM","CSN",
	"CSOC","ETHERNET","EVEN","HDB","ICAN","ICAO","IDL","IEEE488",
	"LNGTH","MARK","MASTR","MIC","MIP","MONTR","MTS","NONE",
	"NRZ","ODD","OFF","ON","PARA","PRIM","PRTY","REDT",
	"RS232","RS422","RS485","RTCON","RTRT","RZ","SERL","SERM",
	"SLAVE","SYNC","TACFIRE","TLKLS","TR","WADC",};
//
//	DIMS-B
//
DECLAREC int r__cnt = R__CNT;
DECLAREC char *RCiilTxt [] = {
	"",};
extern int doSRS_Close ();
extern int doSRS_Connect ();
extern int doSRS_Disconnect ();
extern int doSRS_Fetch ();
extern int doSRS_Init ();
extern int doSRS_Load ();
extern int doSRS_Open ();
extern int doSRS_Reset ();
extern int doSRS_Setup ();
extern int doSRS_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"SRS_1:CH10",
	"SRS_1:CH11",
	"SRS_1:CH12",
	"SRS_1:CH128",
	"SRS_1:CH14",
	"SRS_1:CH16",
	"SRS_1:CH17",
	"SRS_1:CH18",
	"SRS_1:CH19",
	"SRS_1:CH20",
	"SRS_1:CH22",
	"SRS_1:CH40",
	"SRS_1:CH41",
	"SRS_1:CH42",
	"SRS_1:CH43",
	"SRS_1:CH44",
	"SRS_1:CH46",
	"SRS_1:CH64",
	"SRS_1:CH8",
	"SRS_1:CH9",
};
DECLAREC int DevCnt = 22;
int CCALLBACK Wrapper_SRS_1_10_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_10_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_11_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_12_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_128_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_14_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_16_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_17_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_18_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_19_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_20_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_22_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_40_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_41_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_42_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_43_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_44_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_46_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_64_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_8_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Close(void)
{
	if (doSRS_Close() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Connect(void)
{
	if (doSRS_Connect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Disconnect(void)
{
	if (doSRS_Disconnect() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Fetch(void)
{
	if (doSRS_Fetch() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Init(void)
{
	if (doSRS_Init() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Load(void)
{
	if (doSRS_Load() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Open(void)
{
	if (doSRS_Open() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Reset(void)
{
	if (doSRS_Reset() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Setup(void)
{
	if (doSRS_Setup() < 0)
		BusErr ("SRS_1");
	return 0;
}
int CCALLBACK Wrapper_SRS_1_9_Status(void)
{
	if (doSRS_Status() < 0)
		BusErr ("SRS_1");
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
//	SRS_1:CH10
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 10;
	DevDat[2].d_acts[A_CLS] = Wrapper_SRS_1_10_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_SRS_1_10_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_SRS_1_10_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_SRS_1_10_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_SRS_1_10_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_SRS_1_10_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_SRS_1_10_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_SRS_1_10_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_SRS_1_10_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_SRS_1_10_Status;
//
//	SRS_1:CH11
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 11;
	DevDat[3].d_acts[A_CLS] = Wrapper_SRS_1_11_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_SRS_1_11_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_SRS_1_11_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_SRS_1_11_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_SRS_1_11_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_SRS_1_11_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_SRS_1_11_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_SRS_1_11_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_SRS_1_11_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_SRS_1_11_Status;
//
//	SRS_1:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 12;
	DevDat[4].d_acts[A_CLS] = Wrapper_SRS_1_12_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_SRS_1_12_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_SRS_1_12_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_SRS_1_12_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_SRS_1_12_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_SRS_1_12_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_SRS_1_12_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_SRS_1_12_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_SRS_1_12_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_SRS_1_12_Status;
//
//	SRS_1:CH128
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 128;
	DevDat[5].d_acts[A_CLS] = Wrapper_SRS_1_128_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_SRS_1_128_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_SRS_1_128_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_SRS_1_128_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_SRS_1_128_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_SRS_1_128_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_SRS_1_128_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_SRS_1_128_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_SRS_1_128_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_SRS_1_128_Status;
//
//	SRS_1:CH14
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 14;
	DevDat[6].d_acts[A_CLS] = Wrapper_SRS_1_14_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_SRS_1_14_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_SRS_1_14_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_SRS_1_14_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_SRS_1_14_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_SRS_1_14_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_SRS_1_14_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_SRS_1_14_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_SRS_1_14_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_SRS_1_14_Status;
//
//	SRS_1:CH16
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 16;
	DevDat[7].d_acts[A_CLS] = Wrapper_SRS_1_16_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_SRS_1_16_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_SRS_1_16_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_SRS_1_16_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_SRS_1_16_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_SRS_1_16_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_SRS_1_16_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_SRS_1_16_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_SRS_1_16_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_SRS_1_16_Status;
//
//	SRS_1:CH17
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 17;
	DevDat[8].d_acts[A_CLS] = Wrapper_SRS_1_17_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_SRS_1_17_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_SRS_1_17_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_SRS_1_17_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_SRS_1_17_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_SRS_1_17_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_SRS_1_17_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_SRS_1_17_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_SRS_1_17_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_SRS_1_17_Status;
//
//	SRS_1:CH18
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 18;
	DevDat[9].d_acts[A_CLS] = Wrapper_SRS_1_18_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_SRS_1_18_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_SRS_1_18_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_SRS_1_18_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_SRS_1_18_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_SRS_1_18_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_SRS_1_18_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_SRS_1_18_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_SRS_1_18_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_SRS_1_18_Status;
//
//	SRS_1:CH19
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 19;
	DevDat[10].d_acts[A_CLS] = Wrapper_SRS_1_19_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_SRS_1_19_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_SRS_1_19_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_SRS_1_19_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_SRS_1_19_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_SRS_1_19_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_SRS_1_19_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_SRS_1_19_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_SRS_1_19_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_SRS_1_19_Status;
//
//	SRS_1:CH20
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 20;
	DevDat[11].d_acts[A_CLS] = Wrapper_SRS_1_20_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_SRS_1_20_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_SRS_1_20_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_SRS_1_20_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_SRS_1_20_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_SRS_1_20_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_SRS_1_20_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_SRS_1_20_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_SRS_1_20_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_SRS_1_20_Status;
//
//	SRS_1:CH22
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 22;
	DevDat[12].d_acts[A_CLS] = Wrapper_SRS_1_22_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_SRS_1_22_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_SRS_1_22_Disconnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_SRS_1_22_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_SRS_1_22_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_SRS_1_22_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_SRS_1_22_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_SRS_1_22_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_SRS_1_22_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_SRS_1_22_Status;
//
//	SRS_1:CH40
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SPRT);  // speed-ratio
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 40;
	DevDat[13].d_acts[A_CLS] = Wrapper_SRS_1_40_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_SRS_1_40_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_SRS_1_40_Disconnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_SRS_1_40_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_SRS_1_40_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_SRS_1_40_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_SRS_1_40_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_SRS_1_40_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_SRS_1_40_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_SRS_1_40_Status;
//
//	SRS_1:CH41
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SPRT);  // speed-ratio
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 41;
	DevDat[14].d_acts[A_CLS] = Wrapper_SRS_1_41_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_SRS_1_41_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_SRS_1_41_Disconnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_SRS_1_41_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_SRS_1_41_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_SRS_1_41_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_SRS_1_41_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_SRS_1_41_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_SRS_1_41_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_SRS_1_41_Status;
//
//	SRS_1:CH42
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SPRT);  // speed-ratio
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 42;
	DevDat[15].d_acts[A_CLS] = Wrapper_SRS_1_42_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_SRS_1_42_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_SRS_1_42_Disconnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_SRS_1_42_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_SRS_1_42_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_SRS_1_42_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_SRS_1_42_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_SRS_1_42_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_SRS_1_42_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_SRS_1_42_Status;
//
//	SRS_1:CH43
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SPRT);  // speed-ratio
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 43;
	DevDat[16].d_acts[A_CLS] = Wrapper_SRS_1_43_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_SRS_1_43_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_SRS_1_43_Disconnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_SRS_1_43_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_SRS_1_43_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_SRS_1_43_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_SRS_1_43_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_SRS_1_43_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_SRS_1_43_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_SRS_1_43_Status;
//
//	SRS_1:CH44
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SPRT);  // speed-ratio
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 44;
	DevDat[17].d_acts[A_CLS] = Wrapper_SRS_1_44_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_SRS_1_44_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_SRS_1_44_Disconnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_SRS_1_44_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_SRS_1_44_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_SRS_1_44_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_SRS_1_44_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_SRS_1_44_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_SRS_1_44_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_SRS_1_44_Status;
//
//	SRS_1:CH46
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SPRT);  // speed-ratio
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[18].d_modlst = p_mod;
	DevDat[18].d_fncP = 46;
	DevDat[18].d_acts[A_CLS] = Wrapper_SRS_1_46_Close;
	DevDat[18].d_acts[A_CON] = Wrapper_SRS_1_46_Connect;
	DevDat[18].d_acts[A_DIS] = Wrapper_SRS_1_46_Disconnect;
	DevDat[18].d_acts[A_FTH] = Wrapper_SRS_1_46_Fetch;
	DevDat[18].d_acts[A_INX] = Wrapper_SRS_1_46_Init;
	DevDat[18].d_acts[A_LOD] = Wrapper_SRS_1_46_Load;
	DevDat[18].d_acts[A_OPN] = Wrapper_SRS_1_46_Open;
	DevDat[18].d_acts[A_RST] = Wrapper_SRS_1_46_Reset;
	DevDat[18].d_acts[A_FNC] = Wrapper_SRS_1_46_Setup;
	DevDat[18].d_acts[A_STA] = Wrapper_SRS_1_46_Status;
//
//	SRS_1:CH64
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[19].d_modlst = p_mod;
	DevDat[19].d_fncP = 64;
	DevDat[19].d_acts[A_CLS] = Wrapper_SRS_1_64_Close;
	DevDat[19].d_acts[A_CON] = Wrapper_SRS_1_64_Connect;
	DevDat[19].d_acts[A_DIS] = Wrapper_SRS_1_64_Disconnect;
	DevDat[19].d_acts[A_FTH] = Wrapper_SRS_1_64_Fetch;
	DevDat[19].d_acts[A_INX] = Wrapper_SRS_1_64_Init;
	DevDat[19].d_acts[A_LOD] = Wrapper_SRS_1_64_Load;
	DevDat[19].d_acts[A_OPN] = Wrapper_SRS_1_64_Open;
	DevDat[19].d_acts[A_RST] = Wrapper_SRS_1_64_Reset;
	DevDat[19].d_acts[A_FNC] = Wrapper_SRS_1_64_Setup;
	DevDat[19].d_acts[A_STA] = Wrapper_SRS_1_64_Status;
//
//	SRS_1:CH8
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[20].d_modlst = p_mod;
	DevDat[20].d_fncP = 8;
	DevDat[20].d_acts[A_CLS] = Wrapper_SRS_1_8_Close;
	DevDat[20].d_acts[A_CON] = Wrapper_SRS_1_8_Connect;
	DevDat[20].d_acts[A_DIS] = Wrapper_SRS_1_8_Disconnect;
	DevDat[20].d_acts[A_FTH] = Wrapper_SRS_1_8_Fetch;
	DevDat[20].d_acts[A_INX] = Wrapper_SRS_1_8_Init;
	DevDat[20].d_acts[A_LOD] = Wrapper_SRS_1_8_Load;
	DevDat[20].d_acts[A_OPN] = Wrapper_SRS_1_8_Open;
	DevDat[20].d_acts[A_RST] = Wrapper_SRS_1_8_Reset;
	DevDat[20].d_acts[A_FNC] = Wrapper_SRS_1_8_Setup;
	DevDat[20].d_acts[A_STA] = Wrapper_SRS_1_8_Status;
//
//	SRS_1:CH9
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ANGL);  // angle
	p_mod = BldModDat (p_mod, (short) M_ANRT);  // angle-rate
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFS);  // ref-source
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SYNC);  // sync
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[21].d_modlst = p_mod;
	DevDat[21].d_fncP = 9;
	DevDat[21].d_acts[A_CLS] = Wrapper_SRS_1_9_Close;
	DevDat[21].d_acts[A_CON] = Wrapper_SRS_1_9_Connect;
	DevDat[21].d_acts[A_DIS] = Wrapper_SRS_1_9_Disconnect;
	DevDat[21].d_acts[A_FTH] = Wrapper_SRS_1_9_Fetch;
	DevDat[21].d_acts[A_INX] = Wrapper_SRS_1_9_Init;
	DevDat[21].d_acts[A_LOD] = Wrapper_SRS_1_9_Load;
	DevDat[21].d_acts[A_OPN] = Wrapper_SRS_1_9_Open;
	DevDat[21].d_acts[A_RST] = Wrapper_SRS_1_9_Reset;
	DevDat[21].d_acts[A_FNC] = Wrapper_SRS_1_9_Setup;
	DevDat[21].d_acts[A_STA] = Wrapper_SRS_1_9_Status;
	return 0;
}
