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
	"","ACCD","ACCF","ACCP","ACMK","ADFM","ADLN","ADRG",
	"ADTO","AGER","ALLW","ALTI","ALTR","AMBT","AMCP","AMCU",
	"AMFQ","AMMC","AMMF","AMPL","AMSH","AMSR","ANAC","ANAX",
	"ANAY","ANAZ","ANGL","ANGP","ANGT","ANGX","ANGY","ANGZ",
	"ANRT","ANRX","ANRY","ANRZ","ANSD","ATMS","ATTE","ATTN",
	"AUCO","BAND","BARP","BDTH","BITP","BITR","BKPH","BLKT",
	"BRAN","BSTO","BSVT","BTRN","BURD","BURR","BURS","BUSM",
	"BUSS","CAMG","CAMP","CAPA","CCOM","CDAT","CFRQ","CHAN",
	"CHCT","CHID","CHIT","CHRM","CLKS","CMCH","CMDW","CMPL",
	"CMTO","CMWB","CMWV","COMD","COND","COUN","CPHS","CPKN",
	"CPKP","CPLG","CRSD","CRSF","CSTS","CTRQ","CUPK","CUPP",
	"CUR0","CUR1","CURA","CURI","CURL","CURQ","CURR","CURT",
	"CWLV","DATA","DATL","DATP","DATS","DATT","DATW","DBLI",
	"DBND","DBRC","DBRS","DCOF","DDMD","DEEM","DELA","DEST",
	"DEWP","DFBA","DIFR","DIFT","DIGS","DISP","DIST","DIVG",
	"DIVS","DMDS","DPFR","DPSH","DROO","DSFC","DSTR","DSTX",
	"DSTY","DSTZ","DTCT","DTER","DTIL","DTMD","DTOR","DTSC",
	"DTSP","DTST","DUTY","DVPN","DVPP","DWBT","DYRA","EDLN",
	"EDUT","EFCY","EFFI","EGDR","EINM","ERRI","ERRO","EVAO",
	"EVDL","EVEO","EVEV","EVFO","EVGB","EVGF","EVGR","EVGT",
	"EVOU","EVSB","EVSF","EVSL","EVSW","EVTF","EVTI","EVTR",
	"EVTT","EVUN","EVWH","EVXE","EVXM","EXAE","EXNM","EXPO",
	"FALL","FCLN","FCNT","FDST","FDVW","FIAL","FILT","FLTC",
	"FLTS","FLUT","FMCP","FMCU","FMFQ","FMSR","FRCE","FRCR",
	"FREQ","FRMT","FRQ0","FRQ1","FRQD","FRQP","FRQQ","FRQR",
	"FRQW","FRSP","FUEL","FXDN","FXIP","FXQD","GAMA","GSLP",
	"GSRE","HAPW","HARM","HARN","HARP","HARV","HFOV","HIZZ",
	"HLAE","HMDF","HP01","HP02","HP03","HP04","HP05","HP06",
	"HP07","HP08","HP09","HP10","HP11","HP12","HP13","HP14",
	"HP15","HP16","HPA0","HPA1","HPA2","HPA3","HPA4","HPA5",
	"HPA6","HPX1","HPX2","HPX3","HPX4","HPX5","HPX6","HPX7",
	"HPX8","HPX9","HPZ1","HPZ2","HPZ3","HPZ4","HPZ5","HPZ6",
	"HPZ7","HPZ8","HPZ9","HRAG","HSRM","HTAG","HTOF","HUMY",
	"HV01","HV02","HV03","HV04","HV05","HV06","HV07","HV08",
	"HV09","HV10","HV11","HV12","HV13","HV14","HV15","HV16",
	"HVA0","HVA1","HVA2","HVA3","HVA4","HVA5","HVA6","HVX1",
	"HVX2","HVX3","HVX4","HVX5","HVX6","HVX7","HVX8","HVX9",
	"HVZ1","HVZ2","HVZ3","HVZ4","HVZ5","HVZ6","HVZ7","HVZ8",
	"HVZ9","IASP","IATO","ICWB","IDSE","IDSF","IDSG","IDSM",
	"IDWB","IJIT","ILLU","INDU","INTG","INTL","IRAT","ISTI",
	"ISWB","ITER","ITRO","IVCW","IVDL","IVDP","IVDS","IVDT",
	"IVDW","IVMG","IVOA","IVRT","IVSW","IVWC","IVWG","IVWL",
	"IVZA","IVZC","LDFM","LDTO","LDVW","LINE","LIPF","LMDF",
	"LMIN","LOCL","LRAN","LSAE","LSTG","LUMF","LUMI","LUMT",
	"MAGB","MAGR","MAMP","MANI","MASF","MASK","MATH","MAXT",
	"MBAT","MDPN","MDPP","MDSC","MGAP","MMOD","MODD","MODE",
	"MODF","MODO","MODP","MPFM","MPTO","MRCO","MRKB","MRTD",
	"MSKZ","MSNR","MTFD","MTFP","MTFU","NCTO","NEDT","NEGS",
	"NHAR","NLIN","NOAD","NOAV","NOIS","NOPD","NOPK","NOPP",
	"NOTR","NPWT","NRTO","OAMP","OTMP","OVER","P3DV","P3LV",
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
	"SMPW","SMTH","SNAD","SNFL","SNSR","SPCG","SPED","SPGR",
	"SPRT","SPTM","SQD1","SQD2","SQD3","SQTD","SQTR","SRFR",
	"SSMD","STAT","STBM","STIM","STLN","STMH","STMO","STMP",
	"STMR","STMZ","STOP","STPA","STPB","STPG","STPR","STPT",
	"STRD","STRT","STUT","STWD","SUSP","SVCP","SVFM","SVTO",
	"SVWV","SWBT","SWPT","SWRA","SYDL","SYEV","SYNC","TASP",
	"TASY","TCAP","TCBT","TCLT","TCRT","TCTP","TCUR","TDAT",
	"TEFC","TEMP","TEQL","TEQT","TERM","TGMD","TGPL","TGTA",
	"TGTD","TGTH","TGTP","TGTR","TGTS","THRT","THSM","TIEV",
	"TILT","TIME","TIMP","TIND","TIUN","TIWH","TJIT","TLAX",
	"TMON","TMVL","TOPA","TOPG","TOPR","TORQ","TPHD","TPHY",
	"TREA","TRES","TRGS","TRIG","TRLV","TRN0","TRN1","TRNP",
	"TRNS","TROL","TRSL","TRUE","TRUN","TRWH","TSAC","TSCC",
	"TSIM","TSPC","TSTF","TTMP","TVOL","TYPE","UNDR","UNFY",
	"UNIT","UUPL","UUTL","UUTT","VALU","VBAC","VBAN","VBAP",
	"VBPP","VBRT","VBTR","VDIV","VEAO","VEDL","VEEO","VEFO",
	"VEGF","VETF","VFOV","VINS","VIST","VLAE","VLAV","VLPK",
	"VLPP","VLT0","VLT1","VLTL","VLTQ","VLTR","VLTS","VOLF",
	"VOLR","VOLT","VPHF","VPHM","VPKN","VPKP","VRAG","VRMS",
	"VSRM","VTAG","VTOF","WAIT","WAVE","WDLN","WDRT","WGAP",
	"WILD","WIND","WRDC","WTRN","XACE","XAGR","XBAG","XTAR",
	"YACE","YAGR","YBAG","YTAR","ZAMP","ZCRS","ZERO",};
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
extern int doCAN_Close ();
extern int doCAN_Connect ();
extern int doCAN_Disconnect ();
extern int doCAN_Fetch ();
extern int doCAN_Init ();
extern int doCAN_Load ();
extern int doCAN_Open ();
extern int doCAN_Reset ();
extern int doCAN_Setup ();
extern int doCAN_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"CAN_1:CH1",
	"CAN_1:CH101",
	"CAN_1:CH102",
	"CAN_1:CH11",
	"CAN_1:CH111",
	"CAN_1:CH112",
	"CAN_1:CH12",
	"CAN_1:CH2",
};
DECLAREC int DevCnt = 10;
int CCALLBACK Wrapper_CAN_1_1_Close(void)
{
	if (doCAN_Close() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Connect(void)
{
	if (doCAN_Connect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Disconnect(void)
{
	if (doCAN_Disconnect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Fetch(void)
{
	if (doCAN_Fetch() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Init(void)
{
	if (doCAN_Init() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Load(void)
{
	if (doCAN_Load() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Open(void)
{
	if (doCAN_Open() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Reset(void)
{
	if (doCAN_Reset() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Setup(void)
{
	if (doCAN_Setup() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_1_Status(void)
{
	if (doCAN_Status() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Close(void)
{
	if (doCAN_Close() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Connect(void)
{
	if (doCAN_Connect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Disconnect(void)
{
	if (doCAN_Disconnect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Fetch(void)
{
	if (doCAN_Fetch() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Init(void)
{
	if (doCAN_Init() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Load(void)
{
	if (doCAN_Load() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Open(void)
{
	if (doCAN_Open() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Reset(void)
{
	if (doCAN_Reset() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Setup(void)
{
	if (doCAN_Setup() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_101_Status(void)
{
	if (doCAN_Status() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Close(void)
{
	if (doCAN_Close() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Connect(void)
{
	if (doCAN_Connect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Disconnect(void)
{
	if (doCAN_Disconnect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Fetch(void)
{
	if (doCAN_Fetch() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Init(void)
{
	if (doCAN_Init() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Load(void)
{
	if (doCAN_Load() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Open(void)
{
	if (doCAN_Open() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Reset(void)
{
	if (doCAN_Reset() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Setup(void)
{
	if (doCAN_Setup() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_102_Status(void)
{
	if (doCAN_Status() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Close(void)
{
	if (doCAN_Close() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Connect(void)
{
	if (doCAN_Connect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Disconnect(void)
{
	if (doCAN_Disconnect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Fetch(void)
{
	if (doCAN_Fetch() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Init(void)
{
	if (doCAN_Init() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Load(void)
{
	if (doCAN_Load() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Open(void)
{
	if (doCAN_Open() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Reset(void)
{
	if (doCAN_Reset() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Setup(void)
{
	if (doCAN_Setup() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_11_Status(void)
{
	if (doCAN_Status() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Close(void)
{
	if (doCAN_Close() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Connect(void)
{
	if (doCAN_Connect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Disconnect(void)
{
	if (doCAN_Disconnect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Fetch(void)
{
	if (doCAN_Fetch() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Init(void)
{
	if (doCAN_Init() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Load(void)
{
	if (doCAN_Load() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Open(void)
{
	if (doCAN_Open() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Reset(void)
{
	if (doCAN_Reset() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Setup(void)
{
	if (doCAN_Setup() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_111_Status(void)
{
	if (doCAN_Status() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Close(void)
{
	if (doCAN_Close() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Connect(void)
{
	if (doCAN_Connect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Disconnect(void)
{
	if (doCAN_Disconnect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Fetch(void)
{
	if (doCAN_Fetch() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Init(void)
{
	if (doCAN_Init() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Load(void)
{
	if (doCAN_Load() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Open(void)
{
	if (doCAN_Open() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Reset(void)
{
	if (doCAN_Reset() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Setup(void)
{
	if (doCAN_Setup() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_112_Status(void)
{
	if (doCAN_Status() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Close(void)
{
	if (doCAN_Close() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Connect(void)
{
	if (doCAN_Connect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Disconnect(void)
{
	if (doCAN_Disconnect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Fetch(void)
{
	if (doCAN_Fetch() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Init(void)
{
	if (doCAN_Init() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Load(void)
{
	if (doCAN_Load() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Open(void)
{
	if (doCAN_Open() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Reset(void)
{
	if (doCAN_Reset() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Setup(void)
{
	if (doCAN_Setup() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_12_Status(void)
{
	if (doCAN_Status() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Close(void)
{
	if (doCAN_Close() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Connect(void)
{
	if (doCAN_Connect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Disconnect(void)
{
	if (doCAN_Disconnect() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Fetch(void)
{
	if (doCAN_Fetch() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Init(void)
{
	if (doCAN_Init() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Load(void)
{
	if (doCAN_Load() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Open(void)
{
	if (doCAN_Open() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Reset(void)
{
	if (doCAN_Reset() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Setup(void)
{
	if (doCAN_Setup() < 0)
		BusErr ("CAN_1");
	return 0;
}
int CCALLBACK Wrapper_CAN_1_2_Status(void)
{
	if (doCAN_Status() < 0)
		BusErr ("CAN_1");
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
//	CAN_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ADRG);  // address-reg
	p_mod = BldModDat (p_mod, (short) M_BSVT);  // base-vector
	p_mod = BldModDat (p_mod, (short) M_BSTO);  // bus-timeout
	p_mod = BldModDat (p_mod, (short) M_IATO);  // interrupt-ack-timeout
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NCTO);  // no-command-timeout
	p_mod = BldModDat (p_mod, (short) M_NRTO);  // no-response-timeout
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_CAN_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_CAN_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_CAN_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_CAN_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_CAN_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_CAN_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_CAN_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_CAN_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_CAN_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_CAN_1_1_Status;
//
//	CAN_1:CH101
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ADRG);  // address-reg
	p_mod = BldModDat (p_mod, (short) M_BSVT);  // base-vector
	p_mod = BldModDat (p_mod, (short) M_BSTO);  // bus-timeout
	p_mod = BldModDat (p_mod, (short) M_IATO);  // interrupt-ack-timeout
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NCTO);  // no-command-timeout
	p_mod = BldModDat (p_mod, (short) M_NRTO);  // no-response-timeout
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 101;
	DevDat[3].d_acts[A_CLS] = Wrapper_CAN_1_101_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_CAN_1_101_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_CAN_1_101_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_CAN_1_101_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_CAN_1_101_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_CAN_1_101_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_CAN_1_101_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_CAN_1_101_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_CAN_1_101_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_CAN_1_101_Status;
//
//	CAN_1:CH102
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 102;
	DevDat[4].d_acts[A_CLS] = Wrapper_CAN_1_102_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_CAN_1_102_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_CAN_1_102_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_CAN_1_102_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_CAN_1_102_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_CAN_1_102_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_CAN_1_102_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_CAN_1_102_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_CAN_1_102_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_CAN_1_102_Status;
//
//	CAN_1:CH11
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ACCD);  // acceptance-code
	p_mod = BldModDat (p_mod, (short) M_ACMK);  // acceptance-mask
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SNFL);  // single-filter
	p_mod = BldModDat (p_mod, (short) M_THSM);  // three-samples
	p_mod = BldModDat (p_mod, (short) M_TMVL);  // timing-value
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 11;
	DevDat[5].d_acts[A_CLS] = Wrapper_CAN_1_11_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_CAN_1_11_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_CAN_1_11_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_CAN_1_11_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_CAN_1_11_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_CAN_1_11_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_CAN_1_11_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_CAN_1_11_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_CAN_1_11_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_CAN_1_11_Status;
//
//	CAN_1:CH111
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ACCD);  // acceptance-code
	p_mod = BldModDat (p_mod, (short) M_ACMK);  // acceptance-mask
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SNFL);  // single-filter
	p_mod = BldModDat (p_mod, (short) M_THSM);  // three-samples
	p_mod = BldModDat (p_mod, (short) M_TMVL);  // timing-value
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 111;
	DevDat[6].d_acts[A_CLS] = Wrapper_CAN_1_111_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_CAN_1_111_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_CAN_1_111_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_CAN_1_111_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_CAN_1_111_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_CAN_1_111_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_CAN_1_111_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_CAN_1_111_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_CAN_1_111_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_CAN_1_111_Status;
//
//	CAN_1:CH112
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 112;
	DevDat[7].d_acts[A_CLS] = Wrapper_CAN_1_112_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_CAN_1_112_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_CAN_1_112_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_CAN_1_112_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_CAN_1_112_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_CAN_1_112_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_CAN_1_112_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_CAN_1_112_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_CAN_1_112_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_CAN_1_112_Status;
//
//	CAN_1:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 12;
	DevDat[8].d_acts[A_CLS] = Wrapper_CAN_1_12_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_CAN_1_12_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_CAN_1_12_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_CAN_1_12_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_CAN_1_12_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_CAN_1_12_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_CAN_1_12_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_CAN_1_12_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_CAN_1_12_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_CAN_1_12_Status;
//
//	CAN_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_DATA);  // data
	p_mod = BldModDat (p_mod, (short) M_UUTL);  // uut-listener
	p_mod = BldModDat (p_mod, (short) M_UUTT);  // uut-talker
	p_mod = BldModDat (p_mod, (short) M_TEQT);  // test-equip-talker
	p_mod = BldModDat (p_mod, (short) M_TEQL);  // test-equip-listener
	p_mod = BldModDat (p_mod, (short) M_BUSM);  // bus-mode
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 2;
	DevDat[9].d_acts[A_CLS] = Wrapper_CAN_1_2_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_CAN_1_2_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_CAN_1_2_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_CAN_1_2_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_CAN_1_2_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_CAN_1_2_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_CAN_1_2_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_CAN_1_2_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_CAN_1_2_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_CAN_1_2_Status;
	return 0;
}
