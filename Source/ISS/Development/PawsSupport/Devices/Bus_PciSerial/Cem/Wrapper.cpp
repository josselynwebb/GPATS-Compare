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
	"FRQP","FRQQ","FRQR","FRQW","FRSP","FUEL","FXDN","FXIP",
	"FXQD","GAMA","GSLP","GSRE","HAPW","HARM","HARN","HARP",
	"HARV","HFOV","HIZZ","HLAE","HMDF","HP01","HP02","HP03",
	"HP04","HP05","HP06","HP07","HP08","HP09","HP10","HP11",
	"HP12","HP13","HP14","HP15","HP16","HPA0","HPA1","HPA2",
	"HPA3","HPA4","HPA5","HPA6","HPX1","HPX2","HPX3","HPX4",
	"HPX5","HPX6","HPX7","HPX8","HPX9","HPZ1","HPZ2","HPZ3",
	"HPZ4","HPZ5","HPZ6","HPZ7","HPZ8","HPZ9","HRAG","HSRM",
	"HTAG","HTOF","HUMY","HV01","HV02","HV03","HV04","HV05",
	"HV06","HV07","HV08","HV09","HV10","HV11","HV12","HV13",
	"HV14","HV15","HV16","HVA0","HVA1","HVA2","HVA3","HVA4",
	"HVA5","HVA6","HVX1","HVX2","HVX3","HVX4","HVX5","HVX6",
	"HVX7","HVX8","HVX9","HVZ1","HVZ2","HVZ3","HVZ4","HVZ5",
	"HVZ6","HVZ7","HVZ8","HVZ9","IASP","ICWB","IDSE","IDSF",
	"IDSG","IDSM","IDWB","IJIT","ILLU","INDU","INTG","INTL",
	"IRAT","ISTI","ISWB","ITER","ITRO","IVCW","IVDL","IVDP",
	"IVDS","IVDT","IVDW","IVMG","IVOA","IVRT","IVSW","IVWC",
	"IVWG","IVWL","IVZA","IVZC","LDFM","LDTO","LDVW","LINE",
	"LIPF","LMDF","LMIN","LOCL","LRAN","LSAE","LSTG","LUMF",
	"LUMI","LUMT","MAGB","MAGR","MAMP","MANI","MASF","MASK",
	"MATH","MAXT","MBAT","MDPN","MDPP","MDSC","MGAP","MMOD",
	"MODD","MODE","MODF","MODO","MODP","MPFM","MPTO","MRCO",
	"MRKB","MRTD","MSKZ","MSNR","MTFD","MTFP","MTFU","NEDT",
	"NEGS","NHAR","NLIN","NOAD","NOAV","NOIS","NOPD","NOPK",
	"NOPP","NOTR","NPWT","OAMP","OTMP","OVER","P3DV","P3LV",
	"PAMP","PANG","PARE","PARO","PAST","PATH","PATN","PATT",
	"PCCU","PCLS","PCSR","PDEV","PDGN","PDRP","PDVN","PDVP",
	"PERI","PEST","PHPN","PHPP","PJIT","PKDV","PLAN","PLAR",
	"PLEG","PLID","PLSE","PLSI","PLWD","PMCU","PMFQ","PMSR",
	"PODN","POSI","POSS","POWA","POWP","POWR","PPOS","PPST",
	"PPWT","PRCD","PRDF","PRFR","PRIO","PROA","PROF","PRSA",
	"PRSG","PRSR","PRTY","PSHI","PSHT","PSPC","PSPE","PSPT",
	"PSRC","PTCP","PUDP","PWRL","QFAC","QUAD","RADL","RADR",
	"RAIL","RASP","RAST","RCVS","RDNC","REAC","REFF","REFI",
	"REFM","REFP","REFR","REFS","REFU","REFV","REFX","RELB",
	"RELH","RELW","REPT","RERR","RESB","RESI","RESP","RESR",
	"RING","RISE","RLBR","RLVL","RMNS","RMOD","RMPS","ROUN",
	"RPDV","RPEC","RPHF","RPLD","RPLE","RPLI","RPLX","RSPH",
	"RSPO","RSPT","RSPZ","RTRS","SASP","SATM","SBAT","SBCF",
	"SBCM","SBEV","SBFM","SBTO","SCNT","SDEL","SERL","SERM",
	"SESA","SETT","SGNO","SGTF","SHFS","SIMU","SITF","SKEW",
	"SLEW","SLRA","SLRG","SLRR","SLSD","SLSL","SMAV","SMPL",
	"SMPW","SMTH","SNAD","SNSR","SPCG","SPED","SPGR","SPRT",
	"SPTM","SQD1","SQD2","SQD3","SQTD","SQTR","SRFR","SSMD",
	"STAT","STBM","STIM","STLN","STMH","STMO","STMP","STMR",
	"STMZ","STOP","STPA","STPB","STPG","STPR","STPT","STRD",
	"STRT","STUT","STWD","SUSP","SVCP","SVFM","SVTO","SVWV",
	"SWBT","SWPT","SWRA","SYDL","SYEV","SYNC","TASP","TASY",
	"TCAP","TCBT","TCLT","TCRT","TCTP","TCUR","TDAT","TEFC",
	"TEMP","TEQL","TEQT","TERM","TGMD","TGPL","TGTA","TGTD",
	"TGTH","TGTP","TGTR","TGTS","THRT","TIEV","TILT","TIME",
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
	"WTRN","XACE","XAGR","XBAG","XTAR","YACE","YAGR","YBAG",
	"YTAR","ZAMP","ZCRS","ZERO",};
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
extern int doRS485_Close ();
extern int doRS485_Connect ();
extern int doRS485_Disconnect ();
extern int doRS485_Fetch ();
extern int doRS485_Init ();
extern int doRS485_Load ();
extern int doRS485_Open ();
extern int doRS485_Reset ();
extern int doRS485_Setup ();
extern int doRS485_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"PCISERIAL_1:CH1",
	"PCISERIAL_1:CH10",
	"PCISERIAL_1:CH2",
	"PCISERIAL_1:CH3",
	"PCISERIAL_1:CH4",
	"PCISERIAL_1:CH5",
	"PCISERIAL_1:CH6",
	"PCISERIAL_1:CH7",
	"PCISERIAL_1:CH8",
	"PCISERIAL_1:CH9",
};
DECLAREC int DevCnt = 12;
int CCALLBACK Wrapper_PCISERIAL_1_1_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_1_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_2_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_3_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
//
int CCALLBACK Wrapper_PCISERIAL_1_4_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_4_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
//
int CCALLBACK Wrapper_PCISERIAL_1_5_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_5_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
//
int CCALLBACK Wrapper_PCISERIAL_1_6_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_6_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
///////////////////
//
int CCALLBACK Wrapper_PCISERIAL_1_7_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_7_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
//
int CCALLBACK Wrapper_PCISERIAL_1_8_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_8_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
///////////////////
//
int CCALLBACK Wrapper_PCISERIAL_1_9_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_9_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
//
int CCALLBACK Wrapper_PCISERIAL_1_10_Close(void)
{
	if (doRS485_Close() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Connect(void)
{
	if (doRS485_Connect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Disconnect(void)
{
	if (doRS485_Disconnect() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Fetch(void)
{
	if (doRS485_Fetch() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Init(void)
{
	if (doRS485_Init() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Load(void)
{
	if (doRS485_Load() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Open(void)
{
	if (doRS485_Open() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Reset(void)
{
	if (doRS485_Reset() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Setup(void)
{
	if (doRS485_Setup() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
int CCALLBACK Wrapper_PCISERIAL_1_10_Status(void)
{
	if (doRS485_Status() < 0)
		BusErr ("PCISERIAL_1");
	return 0;
}
///////////////////
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
//	PCISERIAL_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BITR);  // bit-rate
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_STPB);  // stop-bits
	p_mod = BldModDat (p_mod, (short) M_WDLN);  // word-length
	p_mod = BldModDat (p_mod, (short) M_PRTY);  // parity
	p_mod = BldModDat (p_mod, (short) M_PRCD);  // proceed
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_PCISERIAL_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_PCISERIAL_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_PCISERIAL_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_PCISERIAL_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_PCISERIAL_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_PCISERIAL_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_PCISERIAL_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_PCISERIAL_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_PCISERIAL_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_PCISERIAL_1_1_Status;
//
//	PCISERIAL_1:CH10
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BITR);  // bit-rate
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_STPB);  // stop-bits
	p_mod = BldModDat (p_mod, (short) M_WDLN);  // word-length
	p_mod = BldModDat (p_mod, (short) M_PRTY);  // parity
	p_mod = BldModDat (p_mod, (short) M_PRCD);  // proceed
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 10;
	DevDat[3].d_acts[A_CLS] = Wrapper_PCISERIAL_1_10_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_PCISERIAL_1_10_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_PCISERIAL_1_10_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_PCISERIAL_1_10_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_PCISERIAL_1_10_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_PCISERIAL_1_10_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_PCISERIAL_1_10_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_PCISERIAL_1_10_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_PCISERIAL_1_10_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_PCISERIAL_1_10_Status;
//
//	PCISERIAL_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BITR);  // bit-rate
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_STPB);  // stop-bits
	p_mod = BldModDat (p_mod, (short) M_WDLN);  // word-length
	p_mod = BldModDat (p_mod, (short) M_PRTY);  // parity
	p_mod = BldModDat (p_mod, (short) M_PRCD);  // proceed
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_TERM);  // terminated
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 2;
	DevDat[4].d_acts[A_CLS] = Wrapper_PCISERIAL_1_2_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_PCISERIAL_1_2_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_PCISERIAL_1_2_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_PCISERIAL_1_2_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_PCISERIAL_1_2_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_PCISERIAL_1_2_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_PCISERIAL_1_2_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_PCISERIAL_1_2_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_PCISERIAL_1_2_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_PCISERIAL_1_2_Status;
//
//	PCISERIAL_1:CH3
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 3;
	DevDat[5].d_acts[A_CLS] = Wrapper_PCISERIAL_1_3_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_PCISERIAL_1_3_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_PCISERIAL_1_3_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_PCISERIAL_1_3_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_PCISERIAL_1_3_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_PCISERIAL_1_3_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_PCISERIAL_1_3_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_PCISERIAL_1_3_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_PCISERIAL_1_3_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_PCISERIAL_1_3_Status;
//
//	PCISERIAL_1:CH4
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 4;
	DevDat[6].d_acts[A_CLS] = Wrapper_PCISERIAL_1_4_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_PCISERIAL_1_4_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_PCISERIAL_1_4_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_PCISERIAL_1_4_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_PCISERIAL_1_4_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_PCISERIAL_1_4_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_PCISERIAL_1_4_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_PCISERIAL_1_4_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_PCISERIAL_1_4_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_PCISERIAL_1_4_Status;
//
//	PCISERIAL_1:CH5
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 5;
	DevDat[7].d_acts[A_CLS] = Wrapper_PCISERIAL_1_5_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_PCISERIAL_1_5_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_PCISERIAL_1_5_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_PCISERIAL_1_5_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_PCISERIAL_1_5_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_PCISERIAL_1_5_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_PCISERIAL_1_5_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_PCISERIAL_1_5_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_PCISERIAL_1_5_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_PCISERIAL_1_5_Status;
//
//	PCISERIAL_1:CH6
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 6;
	DevDat[8].d_acts[A_CLS] = Wrapper_PCISERIAL_1_6_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_PCISERIAL_1_6_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_PCISERIAL_1_6_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_PCISERIAL_1_6_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_PCISERIAL_1_6_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_PCISERIAL_1_6_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_PCISERIAL_1_6_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_PCISERIAL_1_6_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_PCISERIAL_1_6_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_PCISERIAL_1_6_Status;
	///////////////////////
	//
//	PCISERIAL_1:CH7
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 7;
	DevDat[9].d_acts[A_CLS] = Wrapper_PCISERIAL_1_7_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_PCISERIAL_1_7_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_PCISERIAL_1_7_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_PCISERIAL_1_7_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_PCISERIAL_1_7_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_PCISERIAL_1_7_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_PCISERIAL_1_7_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_PCISERIAL_1_7_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_PCISERIAL_1_7_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_PCISERIAL_1_7_Status;
//
//	PCISERIAL_1:CH9
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 8;
	DevDat[10].d_acts[A_CLS] = Wrapper_PCISERIAL_1_8_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_PCISERIAL_1_8_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_PCISERIAL_1_8_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_PCISERIAL_1_8_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_PCISERIAL_1_8_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_PCISERIAL_1_8_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_PCISERIAL_1_8_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_PCISERIAL_1_8_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_PCISERIAL_1_8_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_PCISERIAL_1_8_Status;
//
//	PCISERIAL_1:CH9
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 9;
	DevDat[11].d_acts[A_CLS] = Wrapper_PCISERIAL_1_9_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_PCISERIAL_1_9_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_PCISERIAL_1_9_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_PCISERIAL_1_9_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_PCISERIAL_1_9_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_PCISERIAL_1_9_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_PCISERIAL_1_9_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_PCISERIAL_1_9_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_PCISERIAL_1_9_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_PCISERIAL_1_9_Status;

	return 0;
}
