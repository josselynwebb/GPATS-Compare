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
	"LGT","LLD","LRF","LRR","LTR","MAN","MDS","MIF",
	"MIL","PAC","PAM","PAT","PDC","PDP","PDT","PMS",
	"RDN","RDS","RPS","RSL","RTN","SCS","SHT","SIM",
	"SIN","SQW","STM","STS","SYN","TAC","TDG","TED",
	"TMI","TMR","TRI","VBR","VID","VOR","WAV",};
//
//	MODIFIERS
//
DECLAREC int m__cnt = M__CNT;
DECLAREC char *MCiilTxt [] = {
	"","ACCD","ACCF","ACCP","ACMK","ADFM","ADLN","ADRG",
	"ADTO","AGER","ALLW","ALTI","ALTR","AMBT","AMCP","AMCU",
	"AMFQ","AMMC","AMMF","AMPL","AMSH","AMSR","ANAC","ANAX",
	"ANAY","ANAZ","ANGL","ANGP","ANGT","ANGX","ANGY","ANGZ",
	"ANRT","ANRX","ANRY","ANRZ","ANSD","ATMS","ATTE","ATTN",
	"AUCO","AZIM","BAND","BARP","BDTH","BITP","BITR","BKPH",
	"BLKT","BRAN","BSTO","BSVT","BTRN","BURD","BURR","BURS",
	"BUSM","BUSS","CAMG","CAMP","CAPA","CCOM","CDAT","CFRQ",
	"CHAN","CHID","CHIT","CHRM","CLKS","CMCH","CMDW","CMPL",
	"CMTO","CMWB","CMWV","COMD","COND","COUN","CPHS","CPKN",
	"CPKP","CPLG","CRSD","CRSF","CSTS","CTRQ","CUPK","CUPP",
	"CUR0","CUR1","CURA","CURI","CURL","CURQ","CURR","CURT",
	"CWLV","DATA","DATL","DATP","DATS","DATT","DATW","DBLI",
	"DBND","DBRC","DBRS","DCOF","DDMD","DEEM","DELA","DEST",
	"DEWP","DFBA","DIFR","DIFT","DIGS","DISP","DIST","DIVG",
	"DIVS","DMDS","DPFR","DPSH","DROO","DSFC","DSPC","DSTP",
	"DSTR","DSTX","DSTY","DSTZ","DTCT","DTER","DTIL","DTMD",
	"DTOR","DTSC","DTSP","DTST","DUTY","DVPN","DVPP","DWBT",
	"DYRA","EDLN","EDUT","EFCY","EFFI","EGDR","EINM","ELEV",
	"ERRI","ERRO","EVAO","EVDL","EVEO","EVEV","EVFO","EVGB",
	"EVGF","EVGR","EVGT","EVOU","EVSB","EVSF","EVSL","EVSW",
	"EVTF","EVTI","EVTR","EVTT","EVUN","EVWH","EVXE","EVXM",
	"EXAE","EXNM","EXPO","FALL","FCLN","FCNT","FDST","FDVW",
	"FIAL","FILT","FLTC","FLTS","FLUT","FMCP","FMCU","FMFQ",
	"FMSR","FORW","FRCE","FRCR","FREQ","FRMT","FRQ0","FRQ1",
	"FRQD","FRQP","FRQQ","FRQR","FRQW","FRSP","FUEL","FXDN",
	"FXIP","FXQD","GAMA","GSLP","GSRE","HAPW","HARM","HARN",
	"HARP","HARV","HFOV","HIZZ","HLAE","HMDF","HP01","HP02",
	"HP03","HP04","HP05","HP06","HP07","HP08","HP09","HP10",
	"HP11","HP12","HP13","HP14","HP15","HP16","HPA0","HPA1",
	"HPA2","HPA3","HPA4","HPA5","HPA6","HPX1","HPX2","HPX3",
	"HPX4","HPX5","HPX6","HPX7","HPX8","HPX9","HPZ1","HPZ2",
	"HPZ3","HPZ4","HPZ5","HPZ6","HPZ7","HPZ8","HPZ9","HRAG",
	"HSRM","HTAG","HTOF","HUMY","HV01","HV02","HV03","HV04",
	"HV05","HV06","HV07","HV08","HV09","HV10","HV11","HV12",
	"HV13","HV14","HV15","HV16","HVA0","HVA1","HVA2","HVA3",
	"HVA4","HVA5","HVA6","HVX1","HVX2","HVX3","HVX4","HVX5",
	"HVX6","HVX7","HVX8","HVX9","HVZ1","HVZ2","HVZ3","HVZ4",
	"HVZ5","HVZ6","HVZ7","HVZ8","HVZ9","IASP","IATO","ICWB",
	"IDSE","IDSF","IDSG","IDSM","IDWB","IJIT","ILLU","INDU",
	"INTG","INTL","IRAT","ISTI","ISWB","ITER","ITRO","IVCW",
	"IVDL","IVDP","IVDS","IVDT","IVDW","IVMG","IVOA","IVRT",
	"IVSW","IVWC","IVWG","IVWL","IVZA","IVZC","LDFM","LDTO",
	"LDVW","LINE","LIPF","LMDF","LMIN","LOCL","LRAN","LSAE",
	"LSTG","LUMF","LUMI","LUMT","MAGB","MAGR","MAMP","MANI",
	"MASF","MASK","MATH","MAXT","MBAT","MDPN","MDPP","MDSC",
	"MGAP","MMOD","MODD","MODE","MODF","MODO","MODP","MPFM",
	"MPTO","MRCO","MRKB","MRTD","MSKZ","MSNR","MTFD","MTFP",
	"MTFU","NCTO","NEDT","NEGS","NHAR","NLIN","NOAD","NOAV",
	"NOIS","NOPD","NOPK","NOPP","NOTR","NPWT","NRTO","OAMP",
	"OTMP","OVER","P3DV","P3LV","PAMP","PANG","PARE","PARO",
	"PAST","PATH","PATN","PATT","PCCU","PCLS","PCSR","PDEV",
	"PDGN","PDRP","PDVN","PDVP","PERI","PEST","PHPN","PHPP",
	"PJIT","PKDV","PLAN","PLAR","PLEG","PLID","PLSE","PLSI",
	"PLWD","PMCU","PMFQ","PMSR","PODN","POLR","POSI","POSS",
	"POWA","POWP","POWR","PPOS","PPST","PPWT","PRCD","PRDF",
	"PRFR","PRIO","PROA","PROF","PRSA","PRSG","PRSR","PRTY",
	"PSHI","PSHT","PSPC","PSPE","PSPT","PSRC","PTCP","PUDP",
	"PWRL","QFAC","QUAD","RADL","RADR","RAIL","RASP","RAST",
	"RCVS","RDNC","REAC","REFF","REFI","REFM","REFP","REFR",
	"REFS","REFU","REFV","REFX","RELB","RELH","RELW","REPT",
	"RERR","RESB","RESI","RESP","RESR","RING","RISE","RLBR",
	"RLVL","RMNS","RMOD","RMPS","ROUN","RPDV","RPEC","RPHF",
	"RPLD","RPLE","RPLI","RPLX","RSPH","RSPO","RSPT","RSPZ",
	"RTRS","SASP","SATM","SBCF","SBCM","SBEV","SBFM","SBTO",
	"SCNT","SDEL","SERL","SERM","SESA","SETT","SGNO","SGTF",
	"SHFS","SIMU","SITF","SKEW","SLEW","SLRA","SLRG","SLRR",
	"SLSD","SLSL","SMAV","SMPL","SMPW","SMTH","SNAD","SNFL",
	"SNSR","SPCG","SPED","SPGR","SPRT","SPTM","SQD1","SQD2",
	"SQD3","SQTD","SQTR","SRFR","SSMD","STAT","STIM","STLN",
	"STMH","STMO","STMP","STMR","STMZ","STOP","STPA","STPB",
	"STPG","STPR","STPT","STRD","STRT","STUT","STWD","SUSP",
	"SVCP","SVFM","SVTO","SVWV","SWBT","SWPT","SWRA","SYDL",
	"SYEV","SYNC","TASP","TASY","TCAP","TCBT","TCLT","TCRT",
	"TCTP","TCUR","TDAT","TEFC","TEMP","TEQL","TEQT","TERM",
	"TGMD","TGPL","TGTA","TGTD","TGTH","TGTP","TGTR","TGTS",
	"THRT","THSM","TIEV","TILT","TIME","TIMP","TIND","TIUN",
	"TIWH","TJIT","TLAX","TMON","TMVL","TOPA","TOPG","TOPR",
	"TORQ","TPHD","TPHY","TREA","TRES","TRGS","TRIG","TRLV",
	"TRN0","TRN1","TRNP","TRNS","TROL","TRSL","TRUE","TRUN",
	"TRWH","TSAC","TSCC","TSIM","TSPC","TSTF","TTMP","TVOL",
	"TYPE","UNDR","UNFY","UNIT","UUPL","UUTL","UUTT","VALU",
	"VBAC","VBAN","VBAP","VBPP","VBRT","VBTR","VDIV","VEAO",
	"VEDL","VEEO","VEFO","VEGF","VETF","VFOV","VINS","VIST",
	"VLAE","VLAV","VLPK","VLPP","VLT0","VLT1","VLTL","VLTQ",
	"VLTR","VLTS","VOLF","VOLR","VOLT","VPHF","VPHM","VPKN",
	"VPKP","VRAG","VRMS","VSRM","VTAG","VTOF","WAIT","WAVE",
	"WDLN","WDRT","WGAP","WILD","WIND","WRDC","WTRN","XACE",
	"XAGR","XBAG","XTAR","YACE","YAGR","YBAG","YTAR","ZAMP",
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
	"","1553A","1553B","AFAPD","ALLLS","AMI","AR429","ARDC",
	"BIP","CAN","CDDI","CONMD","CONRT","CSM","CSN","CSOC",
	"ETHERNET","EVEN","HDB","ICAN","ICAO","IDL","IEEE488","LNGTH",
	"MARK","MASTR","MIC","MIP","MONTR","MTS","NONE","NRZ",
	"ODD","OFF","ON","PARA","PRIM","PRTY","REDT","RS232",
	"RS422","RS485","RTCON","RTRT","RZ","SERL","SERM","SLAVE",
	"SYNC","TACFIRE","TLKLS","TR","WADC",};
//
//	DIMS-B
//
DECLAREC int r__cnt = R__CNT;
DECLAREC char *RCiilTxt [] = {
	"",};
extern int doVEO2_Close ();
extern int doVEO2_Connect ();
extern int doVEO2_Disconnect ();
extern int doVEO2_Fetch ();
extern int doVEO2_Init ();
extern int doVEO2_Load ();
extern int doVEO2_Open ();
extern int doVEO2_Reset ();
extern int doVEO2_Setup ();
extern int doVEO2_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"VEO2_1:CH1",
	"VEO2_1:CH10",
	"VEO2_1:CH11",
	"VEO2_1:CH12",
	"VEO2_1:CH13",
	"VEO2_1:CH14",
	"VEO2_1:CH15",
	"VEO2_1:CH16",
	"VEO2_1:CH17",
	"VEO2_1:CH18",
	"VEO2_1:CH19",
	"VEO2_1:CH2",
	"VEO2_1:CH20",
	"VEO2_1:CH21",
	"VEO2_1:CH22",
	"VEO2_1:CH23",
	"VEO2_1:CH24",
	"VEO2_1:CH25",
	"VEO2_1:CH26",
	"VEO2_1:CH28",
	"VEO2_1:CH29",
	"VEO2_1:CH3",
	"VEO2_1:CH30",
	"VEO2_1:CH31",
	"VEO2_1:CH32",
	"VEO2_1:CH33",
	"VEO2_1:CH34",
	"VEO2_1:CH35",
	"VEO2_1:CH37",
	"VEO2_1:CH38",
	"VEO2_1:CH4",
	"VEO2_1:CH40",
	"VEO2_1:CH41",
	"VEO2_1:CH42",
	"VEO2_1:CH43",
	"VEO2_1:CH44",
	"VEO2_1:CH45",
	"VEO2_1:CH46",
	"VEO2_1:CH5",
	"VEO2_1:CH6",
	"VEO2_1:CH7",
	"VEO2_1:CH8",
	"VEO2_1:CH9",
};
DECLAREC int DevCnt = 45;
int CCALLBACK Wrapper_VEO2_1_1_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_1_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_10_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_11_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_12_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_13_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_14_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_15_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_16_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_17_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_18_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_19_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_2_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_20_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_21_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_22_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_23_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_24_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_25_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_26_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_28_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_29_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_3_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_30_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_31_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_32_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_33_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_34_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_35_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_37_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_38_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_4_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_40_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_41_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_42_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_43_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_44_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_45_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_46_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_5_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_6_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_7_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_8_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Close(void)
{
	if (doVEO2_Close() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Connect(void)
{
	if (doVEO2_Connect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Disconnect(void)
{
	if (doVEO2_Disconnect() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Fetch(void)
{
	if (doVEO2_Fetch() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Init(void)
{
	if (doVEO2_Init() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Load(void)
{
	if (doVEO2_Load() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Open(void)
{
	if (doVEO2_Open() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Reset(void)
{
	if (doVEO2_Reset() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Setup(void)
{
	if (doVEO2_Setup() < 0)
		BusErr ("VEO2_1");
	return 0;
}
int CCALLBACK Wrapper_VEO2_1_9_Status(void)
{
	if (doVEO2_Status() < 0)
		BusErr ("VEO2_1");
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
//	VEO2_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_DIFT);  // differential-temp
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_LSAE);  // los-align-error
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_XACE);  // x-autocollimation-error
	p_mod = BldModDat (p_mod, (short) M_YACE);  // y-autocollimation-error
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_VEO2_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_VEO2_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_VEO2_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_VEO2_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_VEO2_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_VEO2_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_VEO2_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_VEO2_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_VEO2_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_VEO2_1_1_Status;
//
//	VEO2_1:CH10
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_AMBT);  // ambient-temp
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 10;
	DevDat[3].d_acts[A_CLS] = Wrapper_VEO2_1_10_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_VEO2_1_10_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_VEO2_1_10_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_VEO2_1_10_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_VEO2_1_10_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_VEO2_1_10_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_VEO2_1_10_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_VEO2_1_10_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_VEO2_1_10_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_VEO2_1_10_Status;
//
//	VEO2_1:CH11
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_BLKT);  // blackbody-temp
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 11;
	DevDat[4].d_acts[A_CLS] = Wrapper_VEO2_1_11_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_VEO2_1_11_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_VEO2_1_11_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_VEO2_1_11_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_VEO2_1_11_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_VEO2_1_11_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_VEO2_1_11_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_VEO2_1_11_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_VEO2_1_11_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_VEO2_1_11_Status;
//
//	VEO2_1:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 12;
	DevDat[5].d_acts[A_CLS] = Wrapper_VEO2_1_12_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_VEO2_1_12_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_VEO2_1_12_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_VEO2_1_12_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_VEO2_1_12_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_VEO2_1_12_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_VEO2_1_12_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_VEO2_1_12_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_VEO2_1_12_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_VEO2_1_12_Status;
//
//	VEO2_1:CH13
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 13;
	DevDat[6].d_acts[A_CLS] = Wrapper_VEO2_1_13_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_VEO2_1_13_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_VEO2_1_13_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_VEO2_1_13_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_VEO2_1_13_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_VEO2_1_13_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_VEO2_1_13_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_VEO2_1_13_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_VEO2_1_13_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_VEO2_1_13_Status;
//
//	VEO2_1:CH14
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_PEST);  // pulse-energy-stab
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 14;
	DevDat[7].d_acts[A_CLS] = Wrapper_VEO2_1_14_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_VEO2_1_14_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_VEO2_1_14_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_VEO2_1_14_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_VEO2_1_14_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_VEO2_1_14_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_VEO2_1_14_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_VEO2_1_14_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_VEO2_1_14_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_VEO2_1_14_Status;
//
//	VEO2_1:CH15
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_PEST);  // pulse-energy-stab
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 15;
	DevDat[8].d_acts[A_CLS] = Wrapper_VEO2_1_15_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_VEO2_1_15_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_VEO2_1_15_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_VEO2_1_15_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_VEO2_1_15_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_VEO2_1_15_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_VEO2_1_15_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_VEO2_1_15_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_VEO2_1_15_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_VEO2_1_15_Status;
//
//	VEO2_1:CH16
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 16;
	DevDat[9].d_acts[A_CLS] = Wrapper_VEO2_1_16_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_VEO2_1_16_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_VEO2_1_16_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_VEO2_1_16_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_VEO2_1_16_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_VEO2_1_16_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_VEO2_1_16_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_VEO2_1_16_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_VEO2_1_16_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_VEO2_1_16_Status;
//
//	VEO2_1:CH17
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 17;
	DevDat[10].d_acts[A_CLS] = Wrapper_VEO2_1_17_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_VEO2_1_17_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_VEO2_1_17_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_VEO2_1_17_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_VEO2_1_17_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_VEO2_1_17_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_VEO2_1_17_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_VEO2_1_17_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_VEO2_1_17_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_VEO2_1_17_Status;
//
//	VEO2_1:CH18
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_PAST);  // pulse-ampl-stab
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 18;
	DevDat[11].d_acts[A_CLS] = Wrapper_VEO2_1_18_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_VEO2_1_18_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_VEO2_1_18_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_VEO2_1_18_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_VEO2_1_18_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_VEO2_1_18_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_VEO2_1_18_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_VEO2_1_18_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_VEO2_1_18_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_VEO2_1_18_Status;
//
//	VEO2_1:CH19
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_PAST);  // pulse-ampl-stab
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 19;
	DevDat[12].d_acts[A_CLS] = Wrapper_VEO2_1_19_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_VEO2_1_19_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_VEO2_1_19_Disconnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_VEO2_1_19_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_VEO2_1_19_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_VEO2_1_19_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_VEO2_1_19_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_VEO2_1_19_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_VEO2_1_19_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_VEO2_1_19_Status;
//
//	VEO2_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_FILT);  // filter
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_DIFT);  // differential-temp
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_MTFU);  // modulation-transfer-function
	p_mod = BldModDat (p_mod, (short) M_MTFD);  // mtf-direction
	p_mod = BldModDat (p_mod, (short) M_MTFP);  // mtf-freq-points
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 2;
	DevDat[13].d_acts[A_CLS] = Wrapper_VEO2_1_2_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_VEO2_1_2_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_VEO2_1_2_Disconnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_VEO2_1_2_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_VEO2_1_2_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_VEO2_1_2_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_VEO2_1_2_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_VEO2_1_2_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_VEO2_1_2_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_VEO2_1_2_Status;
//
//	VEO2_1:CH20
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 20;
	DevDat[14].d_acts[A_CLS] = Wrapper_VEO2_1_20_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_VEO2_1_20_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_VEO2_1_20_Disconnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_VEO2_1_20_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_VEO2_1_20_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_VEO2_1_20_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_VEO2_1_20_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_VEO2_1_20_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_VEO2_1_20_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_VEO2_1_20_Status;
//
//	VEO2_1:CH21
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_TRGS);  // trig-source
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 21;
	DevDat[15].d_acts[A_CLS] = Wrapper_VEO2_1_21_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_VEO2_1_21_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_VEO2_1_21_Disconnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_VEO2_1_21_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_VEO2_1_21_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_VEO2_1_21_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_VEO2_1_21_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_VEO2_1_21_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_VEO2_1_21_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_VEO2_1_21_Status;
//
//	VEO2_1:CH22
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 22;
	DevDat[16].d_acts[A_CLS] = Wrapper_VEO2_1_22_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_VEO2_1_22_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_VEO2_1_22_Disconnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_VEO2_1_22_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_VEO2_1_22_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_VEO2_1_22_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_VEO2_1_22_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_VEO2_1_22_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_VEO2_1_22_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_VEO2_1_22_Status;
//
//	VEO2_1:CH23
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_PLEG);  // pulse-energy
	p_mod = BldModDat (p_mod, (short) M_PPST);  // pulse-period-stab
	p_mod = BldModDat (p_mod, (short) M_MBAT);  // main-beam-atten
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 23;
	DevDat[17].d_acts[A_CLS] = Wrapper_VEO2_1_23_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_VEO2_1_23_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_VEO2_1_23_Disconnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_VEO2_1_23_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_VEO2_1_23_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_VEO2_1_23_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_VEO2_1_23_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_VEO2_1_23_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_VEO2_1_23_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_VEO2_1_23_Status;
//
//	VEO2_1:CH24
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TGMD);  // trigger-mode
	p_mod = BldModDat (p_mod, (short) M_DFBA);  // diff-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_XBAG);  // x-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_YBAG);  // y-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[18].d_modlst = p_mod;
	DevDat[18].d_fncP = 24;
	DevDat[18].d_acts[A_CLS] = Wrapper_VEO2_1_24_Close;
	DevDat[18].d_acts[A_CON] = Wrapper_VEO2_1_24_Connect;
	DevDat[18].d_acts[A_DIS] = Wrapper_VEO2_1_24_Disconnect;
	DevDat[18].d_acts[A_FTH] = Wrapper_VEO2_1_24_Fetch;
	DevDat[18].d_acts[A_INX] = Wrapper_VEO2_1_24_Init;
	DevDat[18].d_acts[A_LOD] = Wrapper_VEO2_1_24_Load;
	DevDat[18].d_acts[A_OPN] = Wrapper_VEO2_1_24_Open;
	DevDat[18].d_acts[A_RST] = Wrapper_VEO2_1_24_Reset;
	DevDat[18].d_acts[A_FNC] = Wrapper_VEO2_1_24_Setup;
	DevDat[18].d_acts[A_STA] = Wrapper_VEO2_1_24_Status;
//
//	VEO2_1:CH25
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TGMD);  // trigger-mode
	p_mod = BldModDat (p_mod, (short) M_DIVG);  // divergence
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[19].d_modlst = p_mod;
	DevDat[19].d_fncP = 25;
	DevDat[19].d_acts[A_CLS] = Wrapper_VEO2_1_25_Close;
	DevDat[19].d_acts[A_CON] = Wrapper_VEO2_1_25_Connect;
	DevDat[19].d_acts[A_DIS] = Wrapper_VEO2_1_25_Disconnect;
	DevDat[19].d_acts[A_FTH] = Wrapper_VEO2_1_25_Fetch;
	DevDat[19].d_acts[A_INX] = Wrapper_VEO2_1_25_Init;
	DevDat[19].d_acts[A_LOD] = Wrapper_VEO2_1_25_Load;
	DevDat[19].d_acts[A_OPN] = Wrapper_VEO2_1_25_Open;
	DevDat[19].d_acts[A_RST] = Wrapper_VEO2_1_25_Reset;
	DevDat[19].d_acts[A_FNC] = Wrapper_VEO2_1_25_Setup;
	DevDat[19].d_acts[A_STA] = Wrapper_VEO2_1_25_Status;
//
//	VEO2_1:CH26
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TGMD);  // trigger-mode
	p_mod = BldModDat (p_mod, (short) M_BRAN);  // boresight-angle
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_XACE);  // x-autocollimation-error
	p_mod = BldModDat (p_mod, (short) M_YACE);  // y-autocollimation-error
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[20].d_modlst = p_mod;
	DevDat[20].d_fncP = 26;
	DevDat[20].d_acts[A_CLS] = Wrapper_VEO2_1_26_Close;
	DevDat[20].d_acts[A_CON] = Wrapper_VEO2_1_26_Connect;
	DevDat[20].d_acts[A_DIS] = Wrapper_VEO2_1_26_Disconnect;
	DevDat[20].d_acts[A_FTH] = Wrapper_VEO2_1_26_Fetch;
	DevDat[20].d_acts[A_INX] = Wrapper_VEO2_1_26_Init;
	DevDat[20].d_acts[A_LOD] = Wrapper_VEO2_1_26_Load;
	DevDat[20].d_acts[A_OPN] = Wrapper_VEO2_1_26_Open;
	DevDat[20].d_acts[A_RST] = Wrapper_VEO2_1_26_Reset;
	DevDat[20].d_acts[A_FNC] = Wrapper_VEO2_1_26_Setup;
	DevDat[20].d_acts[A_STA] = Wrapper_VEO2_1_26_Status;
//
//	VEO2_1:CH28
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_RERR);  // range-error
	p_mod = BldModDat (p_mod, (short) M_RCVS);  // receiver-sensitivity
	p_mod = BldModDat (p_mod, (short) M_TSPC);  // test-point-count
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[21].d_modlst = p_mod;
	DevDat[21].d_fncP = 28;
	DevDat[21].d_acts[A_CLS] = Wrapper_VEO2_1_28_Close;
	DevDat[21].d_acts[A_CON] = Wrapper_VEO2_1_28_Connect;
	DevDat[21].d_acts[A_DIS] = Wrapper_VEO2_1_28_Disconnect;
	DevDat[21].d_acts[A_FTH] = Wrapper_VEO2_1_28_Fetch;
	DevDat[21].d_acts[A_INX] = Wrapper_VEO2_1_28_Init;
	DevDat[21].d_acts[A_LOD] = Wrapper_VEO2_1_28_Load;
	DevDat[21].d_acts[A_OPN] = Wrapper_VEO2_1_28_Open;
	DevDat[21].d_acts[A_RST] = Wrapper_VEO2_1_28_Reset;
	DevDat[21].d_acts[A_FNC] = Wrapper_VEO2_1_28_Setup;
	DevDat[21].d_acts[A_STA] = Wrapper_VEO2_1_28_Status;
//
//	VEO2_1:CH29
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_RERR);  // range-error
	p_mod = BldModDat (p_mod, (short) M_TGTS);  // last-pulse-range
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[22].d_modlst = p_mod;
	DevDat[22].d_fncP = 29;
	DevDat[22].d_acts[A_CLS] = Wrapper_VEO2_1_29_Close;
	DevDat[22].d_acts[A_CON] = Wrapper_VEO2_1_29_Connect;
	DevDat[22].d_acts[A_DIS] = Wrapper_VEO2_1_29_Disconnect;
	DevDat[22].d_acts[A_FTH] = Wrapper_VEO2_1_29_Fetch;
	DevDat[22].d_acts[A_INX] = Wrapper_VEO2_1_29_Init;
	DevDat[22].d_acts[A_LOD] = Wrapper_VEO2_1_29_Load;
	DevDat[22].d_acts[A_OPN] = Wrapper_VEO2_1_29_Open;
	DevDat[22].d_acts[A_RST] = Wrapper_VEO2_1_29_Reset;
	DevDat[22].d_acts[A_FNC] = Wrapper_VEO2_1_29_Setup;
	DevDat[22].d_acts[A_STA] = Wrapper_VEO2_1_29_Status;
//
//	VEO2_1:CH3
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DTOR);  // distortion
	p_mod = BldModDat (p_mod, (short) M_DSTP);  // distortion-positions
	p_mod = BldModDat (p_mod, (short) M_DSPC);  // dist-pos-count
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_DIFT);  // differential-temp
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[23].d_modlst = p_mod;
	DevDat[23].d_fncP = 3;
	DevDat[23].d_acts[A_CLS] = Wrapper_VEO2_1_3_Close;
	DevDat[23].d_acts[A_CON] = Wrapper_VEO2_1_3_Connect;
	DevDat[23].d_acts[A_DIS] = Wrapper_VEO2_1_3_Disconnect;
	DevDat[23].d_acts[A_FTH] = Wrapper_VEO2_1_3_Fetch;
	DevDat[23].d_acts[A_INX] = Wrapper_VEO2_1_3_Init;
	DevDat[23].d_acts[A_LOD] = Wrapper_VEO2_1_3_Load;
	DevDat[23].d_acts[A_OPN] = Wrapper_VEO2_1_3_Open;
	DevDat[23].d_acts[A_RST] = Wrapper_VEO2_1_3_Reset;
	DevDat[23].d_acts[A_FNC] = Wrapper_VEO2_1_3_Setup;
	DevDat[23].d_acts[A_STA] = Wrapper_VEO2_1_3_Status;
//
//	VEO2_1:CH30
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_LSAE);  // los-align-error
	p_mod = BldModDat (p_mod, (short) M_RDNC);  // radiance
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_XACE);  // x-autocollimation-error
	p_mod = BldModDat (p_mod, (short) M_YACE);  // y-autocollimation-error
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[24].d_modlst = p_mod;
	DevDat[24].d_fncP = 30;
	DevDat[24].d_acts[A_CLS] = Wrapper_VEO2_1_30_Close;
	DevDat[24].d_acts[A_CON] = Wrapper_VEO2_1_30_Connect;
	DevDat[24].d_acts[A_DIS] = Wrapper_VEO2_1_30_Disconnect;
	DevDat[24].d_acts[A_FTH] = Wrapper_VEO2_1_30_Fetch;
	DevDat[24].d_acts[A_INX] = Wrapper_VEO2_1_30_Init;
	DevDat[24].d_acts[A_LOD] = Wrapper_VEO2_1_30_Load;
	DevDat[24].d_acts[A_OPN] = Wrapper_VEO2_1_30_Open;
	DevDat[24].d_acts[A_RST] = Wrapper_VEO2_1_30_Reset;
	DevDat[24].d_acts[A_FNC] = Wrapper_VEO2_1_30_Setup;
	DevDat[24].d_acts[A_STA] = Wrapper_VEO2_1_30_Status;
//
//	VEO2_1:CH31
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_FILT);  // filter
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_MTFU);  // modulation-transfer-function
	p_mod = BldModDat (p_mod, (short) M_MTFD);  // mtf-direction
	p_mod = BldModDat (p_mod, (short) M_MTFP);  // mtf-freq-points
	p_mod = BldModDat (p_mod, (short) M_RDNC);  // radiance
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[25].d_modlst = p_mod;
	DevDat[25].d_fncP = 31;
	DevDat[25].d_acts[A_CLS] = Wrapper_VEO2_1_31_Close;
	DevDat[25].d_acts[A_CON] = Wrapper_VEO2_1_31_Connect;
	DevDat[25].d_acts[A_DIS] = Wrapper_VEO2_1_31_Disconnect;
	DevDat[25].d_acts[A_FTH] = Wrapper_VEO2_1_31_Fetch;
	DevDat[25].d_acts[A_INX] = Wrapper_VEO2_1_31_Init;
	DevDat[25].d_acts[A_LOD] = Wrapper_VEO2_1_31_Load;
	DevDat[25].d_acts[A_OPN] = Wrapper_VEO2_1_31_Open;
	DevDat[25].d_acts[A_RST] = Wrapper_VEO2_1_31_Reset;
	DevDat[25].d_acts[A_FNC] = Wrapper_VEO2_1_31_Setup;
	DevDat[25].d_acts[A_STA] = Wrapper_VEO2_1_31_Status;
//
//	VEO2_1:CH32
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DTOR);  // distortion
	p_mod = BldModDat (p_mod, (short) M_DSTP);  // distortion-positions
	p_mod = BldModDat (p_mod, (short) M_DSPC);  // dist-pos-count
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_RDNC);  // radiance
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[26].d_modlst = p_mod;
	DevDat[26].d_fncP = 32;
	DevDat[26].d_acts[A_CLS] = Wrapper_VEO2_1_32_Close;
	DevDat[26].d_acts[A_CON] = Wrapper_VEO2_1_32_Connect;
	DevDat[26].d_acts[A_DIS] = Wrapper_VEO2_1_32_Disconnect;
	DevDat[26].d_acts[A_FTH] = Wrapper_VEO2_1_32_Fetch;
	DevDat[26].d_acts[A_INX] = Wrapper_VEO2_1_32_Init;
	DevDat[26].d_acts[A_LOD] = Wrapper_VEO2_1_32_Load;
	DevDat[26].d_acts[A_OPN] = Wrapper_VEO2_1_32_Open;
	DevDat[26].d_acts[A_RST] = Wrapper_VEO2_1_32_Reset;
	DevDat[26].d_acts[A_FNC] = Wrapper_VEO2_1_32_Setup;
	DevDat[26].d_acts[A_STA] = Wrapper_VEO2_1_32_Status;
//
//	VEO2_1:CH33
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_RDNC);  // radiance
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_UNFY);  // uniformity
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[27].d_modlst = p_mod;
	DevDat[27].d_fncP = 33;
	DevDat[27].d_acts[A_CLS] = Wrapper_VEO2_1_33_Close;
	DevDat[27].d_acts[A_CON] = Wrapper_VEO2_1_33_Connect;
	DevDat[27].d_acts[A_DIS] = Wrapper_VEO2_1_33_Disconnect;
	DevDat[27].d_acts[A_FTH] = Wrapper_VEO2_1_33_Fetch;
	DevDat[27].d_acts[A_INX] = Wrapper_VEO2_1_33_Init;
	DevDat[27].d_acts[A_LOD] = Wrapper_VEO2_1_33_Load;
	DevDat[27].d_acts[A_OPN] = Wrapper_VEO2_1_33_Open;
	DevDat[27].d_acts[A_RST] = Wrapper_VEO2_1_33_Reset;
	DevDat[27].d_acts[A_FNC] = Wrapper_VEO2_1_33_Setup;
	DevDat[27].d_acts[A_STA] = Wrapper_VEO2_1_33_Status;
//
//	VEO2_1:CH34
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_CAMG);  // camera-gain
	p_mod = BldModDat (p_mod, (short) M_RAST);  // radiance-start
	p_mod = BldModDat (p_mod, (short) M_RASP);  // radiance-stop
	p_mod = BldModDat (p_mod, (short) M_RAIL);  // radiance-interval
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[28].d_modlst = p_mod;
	DevDat[28].d_fncP = 34;
	DevDat[28].d_acts[A_CLS] = Wrapper_VEO2_1_34_Close;
	DevDat[28].d_acts[A_CON] = Wrapper_VEO2_1_34_Connect;
	DevDat[28].d_acts[A_DIS] = Wrapper_VEO2_1_34_Disconnect;
	DevDat[28].d_acts[A_FTH] = Wrapper_VEO2_1_34_Fetch;
	DevDat[28].d_acts[A_INX] = Wrapper_VEO2_1_34_Init;
	DevDat[28].d_acts[A_LOD] = Wrapper_VEO2_1_34_Load;
	DevDat[28].d_acts[A_OPN] = Wrapper_VEO2_1_34_Open;
	DevDat[28].d_acts[A_RST] = Wrapper_VEO2_1_34_Reset;
	DevDat[28].d_acts[A_FNC] = Wrapper_VEO2_1_34_Setup;
	DevDat[28].d_acts[A_STA] = Wrapper_VEO2_1_34_Status;
//
//	VEO2_1:CH35
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_DYRA);  // dynamic-range
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_RAST);  // radiance-start
	p_mod = BldModDat (p_mod, (short) M_RASP);  // radiance-stop
	p_mod = BldModDat (p_mod, (short) M_RAIL);  // radiance-interval
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[29].d_modlst = p_mod;
	DevDat[29].d_fncP = 35;
	DevDat[29].d_acts[A_CLS] = Wrapper_VEO2_1_35_Close;
	DevDat[29].d_acts[A_CON] = Wrapper_VEO2_1_35_Connect;
	DevDat[29].d_acts[A_DIS] = Wrapper_VEO2_1_35_Disconnect;
	DevDat[29].d_acts[A_FTH] = Wrapper_VEO2_1_35_Fetch;
	DevDat[29].d_acts[A_INX] = Wrapper_VEO2_1_35_Init;
	DevDat[29].d_acts[A_LOD] = Wrapper_VEO2_1_35_Load;
	DevDat[29].d_acts[A_OPN] = Wrapper_VEO2_1_35_Open;
	DevDat[29].d_acts[A_RST] = Wrapper_VEO2_1_35_Reset;
	DevDat[29].d_acts[A_FNC] = Wrapper_VEO2_1_35_Setup;
	DevDat[29].d_acts[A_STA] = Wrapper_VEO2_1_35_Status;
//
//	VEO2_1:CH37
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_DFBA);  // diff-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_RDNC);  // radiance
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_XBAG);  // x-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_YBAG);  // y-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[30].d_modlst = p_mod;
	DevDat[30].d_fncP = 37;
	DevDat[30].d_acts[A_CLS] = Wrapper_VEO2_1_37_Close;
	DevDat[30].d_acts[A_CON] = Wrapper_VEO2_1_37_Connect;
	DevDat[30].d_acts[A_DIS] = Wrapper_VEO2_1_37_Disconnect;
	DevDat[30].d_acts[A_FTH] = Wrapper_VEO2_1_37_Fetch;
	DevDat[30].d_acts[A_INX] = Wrapper_VEO2_1_37_Init;
	DevDat[30].d_acts[A_LOD] = Wrapper_VEO2_1_37_Load;
	DevDat[30].d_acts[A_OPN] = Wrapper_VEO2_1_37_Open;
	DevDat[30].d_acts[A_RST] = Wrapper_VEO2_1_37_Reset;
	DevDat[30].d_acts[A_FNC] = Wrapper_VEO2_1_37_Setup;
	DevDat[30].d_acts[A_STA] = Wrapper_VEO2_1_37_Status;
//
//	VEO2_1:CH38
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_MRCO);  // min-resolv-contrast
	p_mod = BldModDat (p_mod, (short) M_TDAT);  // target-data
	p_mod = BldModDat (p_mod, (short) M_TSPC);  // test-point-count
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[31].d_modlst = p_mod;
	DevDat[31].d_fncP = 38;
	DevDat[31].d_acts[A_CLS] = Wrapper_VEO2_1_38_Close;
	DevDat[31].d_acts[A_CON] = Wrapper_VEO2_1_38_Connect;
	DevDat[31].d_acts[A_DIS] = Wrapper_VEO2_1_38_Disconnect;
	DevDat[31].d_acts[A_FTH] = Wrapper_VEO2_1_38_Fetch;
	DevDat[31].d_acts[A_INX] = Wrapper_VEO2_1_38_Init;
	DevDat[31].d_acts[A_LOD] = Wrapper_VEO2_1_38_Load;
	DevDat[31].d_acts[A_OPN] = Wrapper_VEO2_1_38_Open;
	DevDat[31].d_acts[A_RST] = Wrapper_VEO2_1_38_Reset;
	DevDat[31].d_acts[A_FNC] = Wrapper_VEO2_1_38_Setup;
	DevDat[31].d_acts[A_STA] = Wrapper_VEO2_1_38_Status;
//
//	VEO2_1:CH4
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_DIFT);  // differential-temp
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_HTOF);  // h-target-offset
	p_mod = BldModDat (p_mod, (short) M_NEDT);  // noise-eq-diff-temp
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_VTOF);  // v-target-offset
	p_mod = BldModDat (p_mod, (short) M_DTST);  // diff-temp-start
	p_mod = BldModDat (p_mod, (short) M_DTSP);  // diff-temp-stop
	p_mod = BldModDat (p_mod, (short) M_DTIL);  // diff-temp-interval
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[32].d_modlst = p_mod;
	DevDat[32].d_fncP = 4;
	DevDat[32].d_acts[A_CLS] = Wrapper_VEO2_1_4_Close;
	DevDat[32].d_acts[A_CON] = Wrapper_VEO2_1_4_Connect;
	DevDat[32].d_acts[A_DIS] = Wrapper_VEO2_1_4_Disconnect;
	DevDat[32].d_acts[A_FTH] = Wrapper_VEO2_1_4_Fetch;
	DevDat[32].d_acts[A_INX] = Wrapper_VEO2_1_4_Init;
	DevDat[32].d_acts[A_LOD] = Wrapper_VEO2_1_4_Load;
	DevDat[32].d_acts[A_OPN] = Wrapper_VEO2_1_4_Open;
	DevDat[32].d_acts[A_RST] = Wrapper_VEO2_1_4_Reset;
	DevDat[32].d_acts[A_FNC] = Wrapper_VEO2_1_4_Setup;
	DevDat[32].d_acts[A_STA] = Wrapper_VEO2_1_4_Status;
//
//	VEO2_1:CH40
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_DIFT);  // differential-temp
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_DTER);  // diff-temp-error
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[33].d_modlst = p_mod;
	DevDat[33].d_fncP = 40;
	DevDat[33].d_acts[A_CLS] = Wrapper_VEO2_1_40_Close;
	DevDat[33].d_acts[A_CON] = Wrapper_VEO2_1_40_Connect;
	DevDat[33].d_acts[A_DIS] = Wrapper_VEO2_1_40_Disconnect;
	DevDat[33].d_acts[A_FTH] = Wrapper_VEO2_1_40_Fetch;
	DevDat[33].d_acts[A_INX] = Wrapper_VEO2_1_40_Init;
	DevDat[33].d_acts[A_LOD] = Wrapper_VEO2_1_40_Load;
	DevDat[33].d_acts[A_OPN] = Wrapper_VEO2_1_40_Open;
	DevDat[33].d_acts[A_RST] = Wrapper_VEO2_1_40_Reset;
	DevDat[33].d_acts[A_FNC] = Wrapper_VEO2_1_40_Setup;
	DevDat[33].d_acts[A_STA] = Wrapper_VEO2_1_40_Status;
//
//	VEO2_1:CH41
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[34].d_modlst = p_mod;
	DevDat[34].d_fncP = 41;
	DevDat[34].d_acts[A_CLS] = Wrapper_VEO2_1_41_Close;
	DevDat[34].d_acts[A_CON] = Wrapper_VEO2_1_41_Connect;
	DevDat[34].d_acts[A_DIS] = Wrapper_VEO2_1_41_Disconnect;
	DevDat[34].d_acts[A_FTH] = Wrapper_VEO2_1_41_Fetch;
	DevDat[34].d_acts[A_INX] = Wrapper_VEO2_1_41_Init;
	DevDat[34].d_acts[A_LOD] = Wrapper_VEO2_1_41_Load;
	DevDat[34].d_acts[A_OPN] = Wrapper_VEO2_1_41_Open;
	DevDat[34].d_acts[A_RST] = Wrapper_VEO2_1_41_Reset;
	DevDat[34].d_acts[A_FNC] = Wrapper_VEO2_1_41_Setup;
	DevDat[34].d_acts[A_STA] = Wrapper_VEO2_1_41_Status;
//
//	VEO2_1:CH42
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TGMD);  // trigger-mode
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_TGTS);  // last-pulse-range
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[35].d_modlst = p_mod;
	DevDat[35].d_fncP = 42;
	DevDat[35].d_acts[A_CLS] = Wrapper_VEO2_1_42_Close;
	DevDat[35].d_acts[A_CON] = Wrapper_VEO2_1_42_Connect;
	DevDat[35].d_acts[A_DIS] = Wrapper_VEO2_1_42_Disconnect;
	DevDat[35].d_acts[A_FTH] = Wrapper_VEO2_1_42_Fetch;
	DevDat[35].d_acts[A_INX] = Wrapper_VEO2_1_42_Init;
	DevDat[35].d_acts[A_LOD] = Wrapper_VEO2_1_42_Load;
	DevDat[35].d_acts[A_OPN] = Wrapper_VEO2_1_42_Open;
	DevDat[35].d_acts[A_RST] = Wrapper_VEO2_1_42_Reset;
	DevDat[35].d_acts[A_FNC] = Wrapper_VEO2_1_42_Setup;
	DevDat[35].d_acts[A_STA] = Wrapper_VEO2_1_42_Status;
//
//	VEO2_1:CH43
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_POWP);  // power-p
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TGMD);  // trigger-mode
	p_mod = BldModDat (p_mod, (short) M_WAVE);  // wave-length
	p_mod = BldModDat (p_mod, (short) M_PODN);  // power-dens
	p_mod = BldModDat (p_mod, (short) M_TGTS);  // last-pulse-range
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[36].d_modlst = p_mod;
	DevDat[36].d_fncP = 43;
	DevDat[36].d_acts[A_CLS] = Wrapper_VEO2_1_43_Close;
	DevDat[36].d_acts[A_CON] = Wrapper_VEO2_1_43_Connect;
	DevDat[36].d_acts[A_DIS] = Wrapper_VEO2_1_43_Disconnect;
	DevDat[36].d_acts[A_FTH] = Wrapper_VEO2_1_43_Fetch;
	DevDat[36].d_acts[A_INX] = Wrapper_VEO2_1_43_Init;
	DevDat[36].d_acts[A_LOD] = Wrapper_VEO2_1_43_Load;
	DevDat[36].d_acts[A_OPN] = Wrapper_VEO2_1_43_Open;
	DevDat[36].d_acts[A_RST] = Wrapper_VEO2_1_43_Reset;
	DevDat[36].d_acts[A_FNC] = Wrapper_VEO2_1_43_Setup;
	DevDat[36].d_acts[A_STA] = Wrapper_VEO2_1_43_Status;
//
//	VEO2_1:CH44
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_RDNC);  // radiance
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[37].d_modlst = p_mod;
	DevDat[37].d_fncP = 44;
	DevDat[37].d_acts[A_CLS] = Wrapper_VEO2_1_44_Close;
	DevDat[37].d_acts[A_CON] = Wrapper_VEO2_1_44_Connect;
	DevDat[37].d_acts[A_DIS] = Wrapper_VEO2_1_44_Disconnect;
	DevDat[37].d_acts[A_FTH] = Wrapper_VEO2_1_44_Fetch;
	DevDat[37].d_acts[A_INX] = Wrapper_VEO2_1_44_Init;
	DevDat[37].d_acts[A_LOD] = Wrapper_VEO2_1_44_Load;
	DevDat[37].d_acts[A_OPN] = Wrapper_VEO2_1_44_Open;
	DevDat[37].d_acts[A_RST] = Wrapper_VEO2_1_44_Reset;
	DevDat[37].d_acts[A_FNC] = Wrapper_VEO2_1_44_Setup;
	DevDat[37].d_acts[A_STA] = Wrapper_VEO2_1_44_Status;
//
//	VEO2_1:CH45
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[38].d_modlst = p_mod;
	DevDat[38].d_fncP = 45;
	DevDat[38].d_acts[A_CLS] = Wrapper_VEO2_1_45_Close;
	DevDat[38].d_acts[A_CON] = Wrapper_VEO2_1_45_Connect;
	DevDat[38].d_acts[A_DIS] = Wrapper_VEO2_1_45_Disconnect;
	DevDat[38].d_acts[A_FTH] = Wrapper_VEO2_1_45_Fetch;
	DevDat[38].d_acts[A_INX] = Wrapper_VEO2_1_45_Init;
	DevDat[38].d_acts[A_LOD] = Wrapper_VEO2_1_45_Load;
	DevDat[38].d_acts[A_OPN] = Wrapper_VEO2_1_45_Open;
	DevDat[38].d_acts[A_RST] = Wrapper_VEO2_1_45_Reset;
	DevDat[38].d_acts[A_FNC] = Wrapper_VEO2_1_45_Setup;
	DevDat[38].d_acts[A_STA] = Wrapper_VEO2_1_45_Status;
//
//	VEO2_1:CH46
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_AZIM);  // azimuth
	p_mod = BldModDat (p_mod, (short) M_ELEV);  // elevation
	p_mod = BldModDat (p_mod, (short) M_POLR);  // polarize
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[39].d_modlst = p_mod;
	DevDat[39].d_fncP = 46;
	DevDat[39].d_acts[A_CLS] = Wrapper_VEO2_1_46_Close;
	DevDat[39].d_acts[A_CON] = Wrapper_VEO2_1_46_Connect;
	DevDat[39].d_acts[A_DIS] = Wrapper_VEO2_1_46_Disconnect;
	DevDat[39].d_acts[A_FTH] = Wrapper_VEO2_1_46_Fetch;
	DevDat[39].d_acts[A_INX] = Wrapper_VEO2_1_46_Init;
	DevDat[39].d_acts[A_LOD] = Wrapper_VEO2_1_46_Load;
	DevDat[39].d_acts[A_OPN] = Wrapper_VEO2_1_46_Open;
	DevDat[39].d_acts[A_RST] = Wrapper_VEO2_1_46_Reset;
	DevDat[39].d_acts[A_FNC] = Wrapper_VEO2_1_46_Setup;
	DevDat[39].d_acts[A_STA] = Wrapper_VEO2_1_46_Status;
//
//	VEO2_1:CH5
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_DIFT);  // differential-temp
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_UNFY);  // uniformity
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[40].d_fncP = 5;
	DevDat[40].d_acts[A_CLS] = Wrapper_VEO2_1_5_Close;
	DevDat[40].d_acts[A_CON] = Wrapper_VEO2_1_5_Connect;
	DevDat[40].d_acts[A_DIS] = Wrapper_VEO2_1_5_Disconnect;
	DevDat[40].d_acts[A_FTH] = Wrapper_VEO2_1_5_Fetch;
	DevDat[40].d_acts[A_INX] = Wrapper_VEO2_1_5_Init;
	DevDat[40].d_acts[A_LOD] = Wrapper_VEO2_1_5_Load;
	DevDat[40].d_acts[A_OPN] = Wrapper_VEO2_1_5_Open;
	DevDat[40].d_acts[A_RST] = Wrapper_VEO2_1_5_Reset;
	DevDat[40].d_acts[A_FNC] = Wrapper_VEO2_1_5_Setup;
	DevDat[40].d_acts[A_STA] = Wrapper_VEO2_1_5_Status;

//
//	VEO2_1:CH6
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_FIAL);  // first-active-line
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_CHIT);  // chan-integrity
	p_mod = BldModDat (p_mod, (short) M_DIFT);  // differential-temp
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_LIPF);  // lines-per-channel
	p_mod = BldModDat (p_mod, (short) M_NEDT);  // noise-eq-diff-temp
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[41].d_modlst = p_mod;
	DevDat[41].d_fncP = 6;
	DevDat[41].d_acts[A_CLS] = Wrapper_VEO2_1_6_Close;
	DevDat[41].d_acts[A_CON] = Wrapper_VEO2_1_6_Connect;
	DevDat[41].d_acts[A_DIS] = Wrapper_VEO2_1_6_Disconnect;
	DevDat[41].d_acts[A_FTH] = Wrapper_VEO2_1_6_Fetch;
	DevDat[41].d_acts[A_INX] = Wrapper_VEO2_1_6_Init;
	DevDat[41].d_acts[A_LOD] = Wrapper_VEO2_1_6_Load;
	DevDat[41].d_acts[A_OPN] = Wrapper_VEO2_1_6_Open;
	DevDat[41].d_acts[A_RST] = Wrapper_VEO2_1_6_Reset;
	DevDat[41].d_acts[A_FNC] = Wrapper_VEO2_1_6_Setup;
	DevDat[41].d_acts[A_STA] = Wrapper_VEO2_1_6_Status;
//
//	VEO2_1:CH7
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_SMAV);  // sample-av
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_FRMT);  // format
	p_mod = BldModDat (p_mod, (short) M_DFBA);  // diff-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_HFOV);  // h-field-of-view
	p_mod = BldModDat (p_mod, (short) M_HLAE);  // h-los-align-error
	p_mod = BldModDat (p_mod, (short) M_HTAG);  // h-target-angle
	p_mod = BldModDat (p_mod, (short) M_ITRO);  // intensity-ratio
	p_mod = BldModDat (p_mod, (short) M_TGTP);  // target-type
	p_mod = BldModDat (p_mod, (short) M_VFOV);  // v-field-of-view
	p_mod = BldModDat (p_mod, (short) M_VLAE);  // v-los-align-error
	p_mod = BldModDat (p_mod, (short) M_VTAG);  // v-target-angle
	p_mod = BldModDat (p_mod, (short) M_XBAG);  // x-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_YBAG);  // y-boresight-angle
	p_mod = BldModDat (p_mod, (short) M_TCLT);  // tgt-coordinate-left
	p_mod = BldModDat (p_mod, (short) M_TCTP);  // tgt-coordinate-top
	p_mod = BldModDat (p_mod, (short) M_TCRT);  // tgt-coordinate-right
	p_mod = BldModDat (p_mod, (short) M_TCBT);  // tgt-coordinate-bottom
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[42].d_modlst = p_mod;
	DevDat[42].d_fncP = 7;
	DevDat[42].d_acts[A_CLS] = Wrapper_VEO2_1_7_Close;
	DevDat[42].d_acts[A_CON] = Wrapper_VEO2_1_7_Connect;
	DevDat[42].d_acts[A_DIS] = Wrapper_VEO2_1_7_Disconnect;
	DevDat[42].d_acts[A_FTH] = Wrapper_VEO2_1_7_Fetch;
	DevDat[42].d_acts[A_INX] = Wrapper_VEO2_1_7_Init;
	DevDat[42].d_acts[A_LOD] = Wrapper_VEO2_1_7_Load;
	DevDat[42].d_acts[A_OPN] = Wrapper_VEO2_1_7_Open;
	DevDat[42].d_acts[A_RST] = Wrapper_VEO2_1_7_Reset;
	DevDat[42].d_acts[A_FNC] = Wrapper_VEO2_1_7_Setup;
	DevDat[42].d_acts[A_STA] = Wrapper_VEO2_1_7_Status;
//
//	VEO2_1:CH8
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MRTD);  // min-resolv-temp-diff
	p_mod = BldModDat (p_mod, (short) M_TDAT);  // target-data
	p_mod = BldModDat (p_mod, (short) M_TSPC);  // test-point-count
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[43].d_modlst = p_mod;
	DevDat[43].d_fncP = 8;
	DevDat[43].d_acts[A_CLS] = Wrapper_VEO2_1_8_Close;
	DevDat[43].d_acts[A_CON] = Wrapper_VEO2_1_8_Connect;
	DevDat[43].d_acts[A_DIS] = Wrapper_VEO2_1_8_Disconnect;
	DevDat[43].d_acts[A_FTH] = Wrapper_VEO2_1_8_Fetch;
	DevDat[43].d_acts[A_INX] = Wrapper_VEO2_1_8_Init;
	DevDat[43].d_acts[A_LOD] = Wrapper_VEO2_1_8_Load;
	DevDat[43].d_acts[A_OPN] = Wrapper_VEO2_1_8_Open;
	DevDat[43].d_acts[A_RST] = Wrapper_VEO2_1_8_Reset;
	DevDat[43].d_acts[A_FNC] = Wrapper_VEO2_1_8_Setup;
	DevDat[43].d_acts[A_STA] = Wrapper_VEO2_1_8_Status;
//
//	VEO2_1:CH9
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_DIFT);  // differential-temp
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[44].d_modlst = p_mod;
	DevDat[44].d_fncP = 9;
	DevDat[44].d_acts[A_CLS] = Wrapper_VEO2_1_9_Close;
	DevDat[44].d_acts[A_CON] = Wrapper_VEO2_1_9_Connect;
	DevDat[44].d_acts[A_DIS] = Wrapper_VEO2_1_9_Disconnect;
	DevDat[44].d_acts[A_FTH] = Wrapper_VEO2_1_9_Fetch;
	DevDat[44].d_acts[A_INX] = Wrapper_VEO2_1_9_Init;
	DevDat[44].d_acts[A_LOD] = Wrapper_VEO2_1_9_Load;
	DevDat[44].d_acts[A_OPN] = Wrapper_VEO2_1_9_Open;
	DevDat[44].d_acts[A_RST] = Wrapper_VEO2_1_9_Reset;
	DevDat[44].d_acts[A_FNC] = Wrapper_VEO2_1_9_Setup;
	DevDat[44].d_acts[A_STA] = Wrapper_VEO2_1_9_Status;
	return 0;
}
