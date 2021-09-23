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
	"CHID","CHIT","CHRM","CLKS","CMCH","CMDW","CMPL","CMTO",
	"CMWB","CMWV","COMD","COND","COUN","CPHS","CPKN","CPKP",
	"CPLG","CRSD","CRSF","CSTS","CTRQ","CUPK","CUPP","CUR0",
	"CUR1","CURA","CURI","CURL","CURQ","CURR","CURT","CWLV",
	"DATA","DATL","DATP","DATS","DATT","DATW","DBLI","DBND",
	"DBRC","DBRS","DCOF","DDMD","DEEM","DELA","DEST","DEWP",
	"DFBA","DIFR","DIFT","DIGS","DISP","DIST","DIVG","DIVS",
	"DMDS","DPFR","DPSH","DROO","DSFC","DSPC","DSTP","DSTR",
	"DSTX","DSTY","DSTZ","DTCT","DTER","DTIL","DTMD","DTOR",
	"DTSC","DTSP","DTST","DUTY","DVPN","DVPP","DWBT","DYRA",
	"EDLN","EDUT","EFCY","EFFI","EGDR","EINM","ERRI","ERRO",
	"EVAO","EVDL","EVEO","EVEV","EVFO","EVGB","EVGF","EVGR",
	"EVGT","EVOU","EVSB","EVSF","EVSL","EVSW","EVTF","EVTI",
	"EVTR","EVTT","EVUN","EVWH","EVXE","EVXM","EXAE","EXNM",
	"EXPO","FALL","FCLN","FCNT","FDST","FDVW","FIAL","FILT",
	"FLTC","FLTS","FLUT","FMCP","FMCU","FMFQ","FMSR","FORW",
	"FRCE","FRCR","FREQ","FRMT","FRQ0","FRQ1","FRQD","FRQP",
	"FRQQ","FRQR","FRQW","FRSP","FUEL","FXDN","FXIP","FXQD",
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
	"HVZ7","HVZ8","HVZ9","IASP","IATO","ICWB","IDSE","IDSF",
	"IDSG","IDSM","IDWB","IJIT","ILLU","INDU","INTG","INTL",
	"IRAT","ISTI","ISWB","ITER","ITRO","IVCW","IVDL","IVDP",
	"IVDS","IVDT","IVDW","IVMG","IVOA","IVRT","IVSW","IVWC",
	"IVWG","IVWL","IVZA","IVZC","LDFM","LDTO","LDVW","LINE",
	"LIPF","LMDF","LMIN","LOCL","LRAN","LSAE","LSTG","LUMF",
	"LUMI","LUMT","MAGB","MAGR","MAMP","MANI","MASF","MASK",
	"MATH","MAXT","MBAT","MDPN","MDPP","MDSC","MGAP","MMOD",
	"MODD","MODE","MODF","MODO","MODP","MPFM","MPTO","MRCO",
	"MRKB","MRTD","MSKZ","MSNR","MTFD","MTFP","MTFU","NCTO",
	"NEDT","NEGS","NHAR","NLIN","NOAD","NOAV","NOIS","NOPD",
	"NOPK","NOPP","NOTR","NPWT","NRTO","OAMP","OTMP","OVER",
	"P3DV","P3LV","PAMP","PANG","PARE","PARO","PAST","PATH",
	"PATN","PATT","PCCU","PCLS","PCSR","PDEV","PDGN","PDRP",
	"PDVN","PDVP","PERI","PEST","PHPN","PHPP","PJIT","PKDV",
	"PLAN","PLAR","PLEG","PLID","PLSE","PLSI","PLWD","PMCU",
	"PMFQ","PMSR","PODN","POSI","POSS","POWA","POWP","POWR",
	"PPOS","PPST","PPWT","PRCD","PRDF","PRFR","PRIO","PROA",
	"PROF","PRSA","PRSG","PRSR","PRTY","PSHI","PSHT","PSPC",
	"PSPE","PSPT","PSRC","PTCP","PUDP","PWRL","QFAC","QUAD",
	"RADL","RADR","RAIL","RASP","RAST","RCVS","RDNC","REAC",
	"REFF","REFI","REFM","REFP","REFR","REFS","REFU","REFV",
	"REFX","RELB","RELH","RELW","REPT","RERR","RESB","RESI",
	"RESP","RESR","RING","RISE","RLBR","RLVL","RMNS","RMOD",
	"RMPS","ROUN","RPDV","RPEC","RPHF","RPLD","RPLE","RPLI",
	"RPLX","RSPH","RSPO","RSPT","RSPZ","RTRS","SASP","SATM",
	"SBCF","SBCM","SBEV","SBFM","SBTO","SCNT","SDEL","SERL",
	"SERM","SESA","SETT","SGNO","SGTF","SHFS","SIMU","SITF",
	"SKEW","SLEW","SLRA","SLRG","SLRR","SLSD","SLSL","SMAV",
	"SMPL","SMPW","SMTH","SNAD","SNFL","SNSR","SPCG","SPED",
	"SPGR","SPRT","SPTM","SQD1","SQD2","SQD3","SQTD","SQTR",
	"SRFR","SSMD","STAT","STIM","STLN","STMH","STMO","STMP",
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
extern int doDMM_Close ();
extern int doDMM_Connect ();
extern int doDMM_Disconnect ();
extern int doDMM_Fetch ();
extern int doDMM_Init ();
extern int doDMM_Load ();
extern int doDMM_Open ();
extern int doDMM_Reset ();
extern int doDMM_Setup ();
extern int doDMM_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"DMM_1:CH1",
	"DMM_1:CH10",
	"DMM_1:CH101",
	"DMM_1:CH102",
	"DMM_1:CH103",
	"DMM_1:CH104",
	"DMM_1:CH105",
	"DMM_1:CH106",
	"DMM_1:CH107",
	"DMM_1:CH108",
	"DMM_1:CH109",
	"DMM_1:CH11",
	"DMM_1:CH110",
	"DMM_1:CH111",
	"DMM_1:CH112",
	"DMM_1:CH113",
	"DMM_1:CH114",
	"DMM_1:CH115",
	"DMM_1:CH116",
	"DMM_1:CH117",
	"DMM_1:CH118",
	"DMM_1:CH119",
	"DMM_1:CH12",
	"DMM_1:CH120",
	"DMM_1:CH121",
	"DMM_1:CH122",
	"DMM_1:CH123",
	"DMM_1:CH125",
	"DMM_1:CH126",
	"DMM_1:CH127",
	"DMM_1:CH128",
	"DMM_1:CH13",
	"DMM_1:CH14",
	"DMM_1:CH15",
	"DMM_1:CH16",
	"DMM_1:CH17",
	"DMM_1:CH18",
	"DMM_1:CH19",
	"DMM_1:CH2",
	"DMM_1:CH20",
	"DMM_1:CH202",
	"DMM_1:CH21",
	"DMM_1:CH22",
	"DMM_1:CH23",
	"DMM_1:CH25",
	"DMM_1:CH26",
	"DMM_1:CH27",
	"DMM_1:CH28",
	"DMM_1:CH3",
	"DMM_1:CH4",
	"DMM_1:CH5",
	"DMM_1:CH6",
	"DMM_1:CH7",
	"DMM_1:CH8",
	"DMM_1:CH9",
};
DECLAREC int DevCnt = 57;
int CCALLBACK Wrapper_DMM_1_1_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_1_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_10_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_101_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_102_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_103_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_104_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_105_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_106_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_107_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_108_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_109_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_11_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_110_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_111_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_112_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_113_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_114_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_115_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_116_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_117_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_118_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_119_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_12_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_120_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_121_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_122_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_123_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_125_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_126_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_127_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_128_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_13_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_14_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_15_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_16_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_17_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_18_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_19_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_2_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_20_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_202_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_21_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_22_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_23_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_25_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_26_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_27_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_28_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_3_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_4_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_5_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_6_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_7_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_8_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Close(void)
{
	if (doDMM_Close() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Connect(void)
{
	if (doDMM_Connect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Disconnect(void)
{
	if (doDMM_Disconnect() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Fetch(void)
{
	if (doDMM_Fetch() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Init(void)
{
	if (doDMM_Init() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Load(void)
{
	if (doDMM_Load() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Open(void)
{
	if (doDMM_Open() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Reset(void)
{
	if (doDMM_Reset() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Setup(void)
{
	if (doDMM_Setup() < 0)
		BusErr ("DMM_1");
	return 0;
}
int CCALLBACK Wrapper_DMM_1_9_Status(void)
{
	if (doDMM_Status() < 0)
		BusErr ("DMM_1");
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
//	DMM_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_DMM_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_DMM_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_DMM_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_DMM_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_DMM_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_DMM_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_DMM_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_DMM_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_DMM_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_DMM_1_1_Status;
//
//	DMM_1:CH10
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 10;
	DevDat[3].d_acts[A_CLS] = Wrapper_DMM_1_10_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_DMM_1_10_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_DMM_1_10_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_DMM_1_10_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_DMM_1_10_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_DMM_1_10_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_DMM_1_10_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_DMM_1_10_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_DMM_1_10_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_DMM_1_10_Status;
//
//	DMM_1:CH101
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 101;
	DevDat[4].d_acts[A_CLS] = Wrapper_DMM_1_101_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_DMM_1_101_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_DMM_1_101_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_DMM_1_101_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_DMM_1_101_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_DMM_1_101_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_DMM_1_101_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_DMM_1_101_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_DMM_1_101_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_DMM_1_101_Status;
//
//	DMM_1:CH102
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 102;
	DevDat[5].d_acts[A_CLS] = Wrapper_DMM_1_102_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_DMM_1_102_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_DMM_1_102_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_DMM_1_102_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_DMM_1_102_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_DMM_1_102_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_DMM_1_102_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_DMM_1_102_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_DMM_1_102_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_DMM_1_102_Status;
//
//	DMM_1:CH103
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFR);  // ref-res
	p_mod = BldModDat (p_mod, (short) M_RESI);  // res
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_FORW);  // four-wire
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 103;
	DevDat[6].d_acts[A_CLS] = Wrapper_DMM_1_103_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_DMM_1_103_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_DMM_1_103_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_DMM_1_103_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_DMM_1_103_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_DMM_1_103_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_DMM_1_103_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_DMM_1_103_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_DMM_1_103_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_DMM_1_103_Status;
//
//	DMM_1:CH104
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 104;
	DevDat[7].d_acts[A_CLS] = Wrapper_DMM_1_104_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_DMM_1_104_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_DMM_1_104_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_DMM_1_104_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_DMM_1_104_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_DMM_1_104_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_DMM_1_104_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_DMM_1_104_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_DMM_1_104_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_DMM_1_104_Status;
//
//	DMM_1:CH105
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 105;
	DevDat[8].d_acts[A_CLS] = Wrapper_DMM_1_105_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_DMM_1_105_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_DMM_1_105_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_DMM_1_105_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_DMM_1_105_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_DMM_1_105_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_DMM_1_105_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_DMM_1_105_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_DMM_1_105_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_DMM_1_105_Status;
//
//	DMM_1:CH106
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 106;
	DevDat[9].d_acts[A_CLS] = Wrapper_DMM_1_106_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_DMM_1_106_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_DMM_1_106_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_DMM_1_106_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_DMM_1_106_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_DMM_1_106_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_DMM_1_106_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_DMM_1_106_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_DMM_1_106_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_DMM_1_106_Status;
//
//	DMM_1:CH107
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 107;
	DevDat[10].d_acts[A_CLS] = Wrapper_DMM_1_107_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_DMM_1_107_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_DMM_1_107_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_DMM_1_107_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_DMM_1_107_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_DMM_1_107_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_DMM_1_107_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_DMM_1_107_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_DMM_1_107_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_DMM_1_107_Status;
//
//	DMM_1:CH108
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VOLR);  // voltage-ratio
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 108;
	DevDat[11].d_acts[A_CLS] = Wrapper_DMM_1_108_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_DMM_1_108_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_DMM_1_108_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_DMM_1_108_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_DMM_1_108_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_DMM_1_108_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_DMM_1_108_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_DMM_1_108_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_DMM_1_108_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_DMM_1_108_Status;
//
//	DMM_1:CH109
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 109;
	DevDat[12].d_acts[A_CLS] = Wrapper_DMM_1_109_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_DMM_1_109_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_DMM_1_109_Disconnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_DMM_1_109_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_DMM_1_109_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_DMM_1_109_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_DMM_1_109_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_DMM_1_109_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_DMM_1_109_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_DMM_1_109_Status;
//
//	DMM_1:CH11
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURA);  // av-current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 11;
	DevDat[13].d_acts[A_CLS] = Wrapper_DMM_1_11_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_DMM_1_11_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_DMM_1_11_Disconnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_DMM_1_11_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_DMM_1_11_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_DMM_1_11_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_DMM_1_11_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_DMM_1_11_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_DMM_1_11_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_DMM_1_11_Status;
//
//	DMM_1:CH110
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 110;
	DevDat[14].d_acts[A_CLS] = Wrapper_DMM_1_110_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_DMM_1_110_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_DMM_1_110_Disconnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_DMM_1_110_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_DMM_1_110_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_DMM_1_110_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_DMM_1_110_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_DMM_1_110_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_DMM_1_110_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_DMM_1_110_Status;
//
//	DMM_1:CH111
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURA);  // av-current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 111;
	DevDat[15].d_acts[A_CLS] = Wrapper_DMM_1_111_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_DMM_1_111_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_DMM_1_111_Disconnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_DMM_1_111_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_DMM_1_111_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_DMM_1_111_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_DMM_1_111_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_DMM_1_111_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_DMM_1_111_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_DMM_1_111_Status;
//
//	DMM_1:CH112
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURA);  // av-current
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 112;
	DevDat[16].d_acts[A_CLS] = Wrapper_DMM_1_112_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_DMM_1_112_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_DMM_1_112_Disconnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_DMM_1_112_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_DMM_1_112_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_DMM_1_112_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_DMM_1_112_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_DMM_1_112_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_DMM_1_112_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_DMM_1_112_Status;
//
//	DMM_1:CH113
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ACCP);  // ac-comp
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 113;
	DevDat[17].d_acts[A_CLS] = Wrapper_DMM_1_113_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_DMM_1_113_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_DMM_1_113_Disconnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_DMM_1_113_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_DMM_1_113_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_DMM_1_113_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_DMM_1_113_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_DMM_1_113_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_DMM_1_113_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_DMM_1_113_Status;
//
//	DMM_1:CH114
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ACCF);  // ac-comp-freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[18].d_modlst = p_mod;
	DevDat[18].d_fncP = 114;
	DevDat[18].d_acts[A_CLS] = Wrapper_DMM_1_114_Close;
	DevDat[18].d_acts[A_CON] = Wrapper_DMM_1_114_Connect;
	DevDat[18].d_acts[A_DIS] = Wrapper_DMM_1_114_Disconnect;
	DevDat[18].d_acts[A_FTH] = Wrapper_DMM_1_114_Fetch;
	DevDat[18].d_acts[A_INX] = Wrapper_DMM_1_114_Init;
	DevDat[18].d_acts[A_LOD] = Wrapper_DMM_1_114_Load;
	DevDat[18].d_acts[A_OPN] = Wrapper_DMM_1_114_Open;
	DevDat[18].d_acts[A_RST] = Wrapper_DMM_1_114_Reset;
	DevDat[18].d_acts[A_FNC] = Wrapper_DMM_1_114_Setup;
	DevDat[18].d_acts[A_STA] = Wrapper_DMM_1_114_Status;
//
//	DMM_1:CH115
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[19].d_modlst = p_mod;
	DevDat[19].d_fncP = 115;
	DevDat[19].d_acts[A_CLS] = Wrapper_DMM_1_115_Close;
	DevDat[19].d_acts[A_CON] = Wrapper_DMM_1_115_Connect;
	DevDat[19].d_acts[A_DIS] = Wrapper_DMM_1_115_Disconnect;
	DevDat[19].d_acts[A_FTH] = Wrapper_DMM_1_115_Fetch;
	DevDat[19].d_acts[A_INX] = Wrapper_DMM_1_115_Init;
	DevDat[19].d_acts[A_LOD] = Wrapper_DMM_1_115_Load;
	DevDat[19].d_acts[A_OPN] = Wrapper_DMM_1_115_Open;
	DevDat[19].d_acts[A_RST] = Wrapper_DMM_1_115_Reset;
	DevDat[19].d_acts[A_FNC] = Wrapper_DMM_1_115_Setup;
	DevDat[19].d_acts[A_STA] = Wrapper_DMM_1_115_Status;
//
//	DMM_1:CH116
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[20].d_modlst = p_mod;
	DevDat[20].d_fncP = 116;
	DevDat[20].d_acts[A_CLS] = Wrapper_DMM_1_116_Close;
	DevDat[20].d_acts[A_CON] = Wrapper_DMM_1_116_Connect;
	DevDat[20].d_acts[A_DIS] = Wrapper_DMM_1_116_Disconnect;
	DevDat[20].d_acts[A_FTH] = Wrapper_DMM_1_116_Fetch;
	DevDat[20].d_acts[A_INX] = Wrapper_DMM_1_116_Init;
	DevDat[20].d_acts[A_LOD] = Wrapper_DMM_1_116_Load;
	DevDat[20].d_acts[A_OPN] = Wrapper_DMM_1_116_Open;
	DevDat[20].d_acts[A_RST] = Wrapper_DMM_1_116_Reset;
	DevDat[20].d_acts[A_FNC] = Wrapper_DMM_1_116_Setup;
	DevDat[20].d_acts[A_STA] = Wrapper_DMM_1_116_Status;
//
//	DMM_1:CH117
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[21].d_modlst = p_mod;
	DevDat[21].d_fncP = 117;
	DevDat[21].d_acts[A_CLS] = Wrapper_DMM_1_117_Close;
	DevDat[21].d_acts[A_CON] = Wrapper_DMM_1_117_Connect;
	DevDat[21].d_acts[A_DIS] = Wrapper_DMM_1_117_Disconnect;
	DevDat[21].d_acts[A_FTH] = Wrapper_DMM_1_117_Fetch;
	DevDat[21].d_acts[A_INX] = Wrapper_DMM_1_117_Init;
	DevDat[21].d_acts[A_LOD] = Wrapper_DMM_1_117_Load;
	DevDat[21].d_acts[A_OPN] = Wrapper_DMM_1_117_Open;
	DevDat[21].d_acts[A_RST] = Wrapper_DMM_1_117_Reset;
	DevDat[21].d_acts[A_FNC] = Wrapper_DMM_1_117_Setup;
	DevDat[21].d_acts[A_STA] = Wrapper_DMM_1_117_Status;
//
//	DMM_1:CH118
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[22].d_modlst = p_mod;
	DevDat[22].d_fncP = 118;
	DevDat[22].d_acts[A_CLS] = Wrapper_DMM_1_118_Close;
	DevDat[22].d_acts[A_CON] = Wrapper_DMM_1_118_Connect;
	DevDat[22].d_acts[A_DIS] = Wrapper_DMM_1_118_Disconnect;
	DevDat[22].d_acts[A_FTH] = Wrapper_DMM_1_118_Fetch;
	DevDat[22].d_acts[A_INX] = Wrapper_DMM_1_118_Init;
	DevDat[22].d_acts[A_LOD] = Wrapper_DMM_1_118_Load;
	DevDat[22].d_acts[A_OPN] = Wrapper_DMM_1_118_Open;
	DevDat[22].d_acts[A_RST] = Wrapper_DMM_1_118_Reset;
	DevDat[22].d_acts[A_FNC] = Wrapper_DMM_1_118_Setup;
	DevDat[22].d_acts[A_STA] = Wrapper_DMM_1_118_Status;
//
//	DMM_1:CH119
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[23].d_modlst = p_mod;
	DevDat[23].d_fncP = 119;
	DevDat[23].d_acts[A_CLS] = Wrapper_DMM_1_119_Close;
	DevDat[23].d_acts[A_CON] = Wrapper_DMM_1_119_Connect;
	DevDat[23].d_acts[A_DIS] = Wrapper_DMM_1_119_Disconnect;
	DevDat[23].d_acts[A_FTH] = Wrapper_DMM_1_119_Fetch;
	DevDat[23].d_acts[A_INX] = Wrapper_DMM_1_119_Init;
	DevDat[23].d_acts[A_LOD] = Wrapper_DMM_1_119_Load;
	DevDat[23].d_acts[A_OPN] = Wrapper_DMM_1_119_Open;
	DevDat[23].d_acts[A_RST] = Wrapper_DMM_1_119_Reset;
	DevDat[23].d_acts[A_FNC] = Wrapper_DMM_1_119_Setup;
	DevDat[23].d_acts[A_STA] = Wrapper_DMM_1_119_Status;
//
//	DMM_1:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURA);  // av-current
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[24].d_modlst = p_mod;
	DevDat[24].d_fncP = 12;
	DevDat[24].d_acts[A_CLS] = Wrapper_DMM_1_12_Close;
	DevDat[24].d_acts[A_CON] = Wrapper_DMM_1_12_Connect;
	DevDat[24].d_acts[A_DIS] = Wrapper_DMM_1_12_Disconnect;
	DevDat[24].d_acts[A_FTH] = Wrapper_DMM_1_12_Fetch;
	DevDat[24].d_acts[A_INX] = Wrapper_DMM_1_12_Init;
	DevDat[24].d_acts[A_LOD] = Wrapper_DMM_1_12_Load;
	DevDat[24].d_acts[A_OPN] = Wrapper_DMM_1_12_Open;
	DevDat[24].d_acts[A_RST] = Wrapper_DMM_1_12_Reset;
	DevDat[24].d_acts[A_FNC] = Wrapper_DMM_1_12_Setup;
	DevDat[24].d_acts[A_STA] = Wrapper_DMM_1_12_Status;
//
//	DMM_1:CH120
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[25].d_modlst = p_mod;
	DevDat[25].d_fncP = 120;
	DevDat[25].d_acts[A_CLS] = Wrapper_DMM_1_120_Close;
	DevDat[25].d_acts[A_CON] = Wrapper_DMM_1_120_Connect;
	DevDat[25].d_acts[A_DIS] = Wrapper_DMM_1_120_Disconnect;
	DevDat[25].d_acts[A_FTH] = Wrapper_DMM_1_120_Fetch;
	DevDat[25].d_acts[A_INX] = Wrapper_DMM_1_120_Init;
	DevDat[25].d_acts[A_LOD] = Wrapper_DMM_1_120_Load;
	DevDat[25].d_acts[A_OPN] = Wrapper_DMM_1_120_Open;
	DevDat[25].d_acts[A_RST] = Wrapper_DMM_1_120_Reset;
	DevDat[25].d_acts[A_FNC] = Wrapper_DMM_1_120_Setup;
	DevDat[25].d_acts[A_STA] = Wrapper_DMM_1_120_Status;
//
//	DMM_1:CH121
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[26].d_modlst = p_mod;
	DevDat[26].d_fncP = 121;
	DevDat[26].d_acts[A_CLS] = Wrapper_DMM_1_121_Close;
	DevDat[26].d_acts[A_CON] = Wrapper_DMM_1_121_Connect;
	DevDat[26].d_acts[A_DIS] = Wrapper_DMM_1_121_Disconnect;
	DevDat[26].d_acts[A_FTH] = Wrapper_DMM_1_121_Fetch;
	DevDat[26].d_acts[A_INX] = Wrapper_DMM_1_121_Init;
	DevDat[26].d_acts[A_LOD] = Wrapper_DMM_1_121_Load;
	DevDat[26].d_acts[A_OPN] = Wrapper_DMM_1_121_Open;
	DevDat[26].d_acts[A_RST] = Wrapper_DMM_1_121_Reset;
	DevDat[26].d_acts[A_FNC] = Wrapper_DMM_1_121_Setup;
	DevDat[26].d_acts[A_STA] = Wrapper_DMM_1_121_Status;
//
//	DMM_1:CH122
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[27].d_modlst = p_mod;
	DevDat[27].d_fncP = 122;
	DevDat[27].d_acts[A_CLS] = Wrapper_DMM_1_122_Close;
	DevDat[27].d_acts[A_CON] = Wrapper_DMM_1_122_Connect;
	DevDat[27].d_acts[A_DIS] = Wrapper_DMM_1_122_Disconnect;
	DevDat[27].d_acts[A_FTH] = Wrapper_DMM_1_122_Fetch;
	DevDat[27].d_acts[A_INX] = Wrapper_DMM_1_122_Init;
	DevDat[27].d_acts[A_LOD] = Wrapper_DMM_1_122_Load;
	DevDat[27].d_acts[A_OPN] = Wrapper_DMM_1_122_Open;
	DevDat[27].d_acts[A_RST] = Wrapper_DMM_1_122_Reset;
	DevDat[27].d_acts[A_FNC] = Wrapper_DMM_1_122_Setup;
	DevDat[27].d_acts[A_STA] = Wrapper_DMM_1_122_Status;
//
//	DMM_1:CH123
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[28].d_modlst = p_mod;
	DevDat[28].d_fncP = 123;
	DevDat[28].d_acts[A_CLS] = Wrapper_DMM_1_123_Close;
	DevDat[28].d_acts[A_CON] = Wrapper_DMM_1_123_Connect;
	DevDat[28].d_acts[A_DIS] = Wrapper_DMM_1_123_Disconnect;
	DevDat[28].d_acts[A_FTH] = Wrapper_DMM_1_123_Fetch;
	DevDat[28].d_acts[A_INX] = Wrapper_DMM_1_123_Init;
	DevDat[28].d_acts[A_LOD] = Wrapper_DMM_1_123_Load;
	DevDat[28].d_acts[A_OPN] = Wrapper_DMM_1_123_Open;
	DevDat[28].d_acts[A_RST] = Wrapper_DMM_1_123_Reset;
	DevDat[28].d_acts[A_FNC] = Wrapper_DMM_1_123_Setup;
	DevDat[28].d_acts[A_STA] = Wrapper_DMM_1_123_Status;
//
//	DMM_1:CH125
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[29].d_modlst = p_mod;
	DevDat[29].d_fncP = 125;
	DevDat[29].d_acts[A_CLS] = Wrapper_DMM_1_125_Close;
	DevDat[29].d_acts[A_CON] = Wrapper_DMM_1_125_Connect;
	DevDat[29].d_acts[A_DIS] = Wrapper_DMM_1_125_Disconnect;
	DevDat[29].d_acts[A_FTH] = Wrapper_DMM_1_125_Fetch;
	DevDat[29].d_acts[A_INX] = Wrapper_DMM_1_125_Init;
	DevDat[29].d_acts[A_LOD] = Wrapper_DMM_1_125_Load;
	DevDat[29].d_acts[A_OPN] = Wrapper_DMM_1_125_Open;
	DevDat[29].d_acts[A_RST] = Wrapper_DMM_1_125_Reset;
	DevDat[29].d_acts[A_FNC] = Wrapper_DMM_1_125_Setup;
	DevDat[29].d_acts[A_STA] = Wrapper_DMM_1_125_Status;
//
//	DMM_1:CH126
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[30].d_modlst = p_mod;
	DevDat[30].d_fncP = 126;
	DevDat[30].d_acts[A_CLS] = Wrapper_DMM_1_126_Close;
	DevDat[30].d_acts[A_CON] = Wrapper_DMM_1_126_Connect;
	DevDat[30].d_acts[A_DIS] = Wrapper_DMM_1_126_Disconnect;
	DevDat[30].d_acts[A_FTH] = Wrapper_DMM_1_126_Fetch;
	DevDat[30].d_acts[A_INX] = Wrapper_DMM_1_126_Init;
	DevDat[30].d_acts[A_LOD] = Wrapper_DMM_1_126_Load;
	DevDat[30].d_acts[A_OPN] = Wrapper_DMM_1_126_Open;
	DevDat[30].d_acts[A_RST] = Wrapper_DMM_1_126_Reset;
	DevDat[30].d_acts[A_FNC] = Wrapper_DMM_1_126_Setup;
	DevDat[30].d_acts[A_STA] = Wrapper_DMM_1_126_Status;
//
//	DMM_1:CH127
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[31].d_modlst = p_mod;
	DevDat[31].d_fncP = 127;
	DevDat[31].d_acts[A_CLS] = Wrapper_DMM_1_127_Close;
	DevDat[31].d_acts[A_CON] = Wrapper_DMM_1_127_Connect;
	DevDat[31].d_acts[A_DIS] = Wrapper_DMM_1_127_Disconnect;
	DevDat[31].d_acts[A_FTH] = Wrapper_DMM_1_127_Fetch;
	DevDat[31].d_acts[A_INX] = Wrapper_DMM_1_127_Init;
	DevDat[31].d_acts[A_LOD] = Wrapper_DMM_1_127_Load;
	DevDat[31].d_acts[A_OPN] = Wrapper_DMM_1_127_Open;
	DevDat[31].d_acts[A_RST] = Wrapper_DMM_1_127_Reset;
	DevDat[31].d_acts[A_FNC] = Wrapper_DMM_1_127_Setup;
	DevDat[31].d_acts[A_STA] = Wrapper_DMM_1_127_Status;
//
//	DMM_1:CH128
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[32].d_modlst = p_mod;
	DevDat[32].d_fncP = 128;
	DevDat[32].d_acts[A_CLS] = Wrapper_DMM_1_128_Close;
	DevDat[32].d_acts[A_CON] = Wrapper_DMM_1_128_Connect;
	DevDat[32].d_acts[A_DIS] = Wrapper_DMM_1_128_Disconnect;
	DevDat[32].d_acts[A_FTH] = Wrapper_DMM_1_128_Fetch;
	DevDat[32].d_acts[A_INX] = Wrapper_DMM_1_128_Init;
	DevDat[32].d_acts[A_LOD] = Wrapper_DMM_1_128_Load;
	DevDat[32].d_acts[A_OPN] = Wrapper_DMM_1_128_Open;
	DevDat[32].d_acts[A_RST] = Wrapper_DMM_1_128_Reset;
	DevDat[32].d_acts[A_FNC] = Wrapper_DMM_1_128_Setup;
	DevDat[32].d_acts[A_STA] = Wrapper_DMM_1_128_Status;
//
//	DMM_1:CH13
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ACCP);  // ac-comp
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[33].d_modlst = p_mod;
	DevDat[33].d_fncP = 13;
	DevDat[33].d_acts[A_CLS] = Wrapper_DMM_1_13_Close;
	DevDat[33].d_acts[A_CON] = Wrapper_DMM_1_13_Connect;
	DevDat[33].d_acts[A_DIS] = Wrapper_DMM_1_13_Disconnect;
	DevDat[33].d_acts[A_FTH] = Wrapper_DMM_1_13_Fetch;
	DevDat[33].d_acts[A_INX] = Wrapper_DMM_1_13_Init;
	DevDat[33].d_acts[A_LOD] = Wrapper_DMM_1_13_Load;
	DevDat[33].d_acts[A_OPN] = Wrapper_DMM_1_13_Open;
	DevDat[33].d_acts[A_RST] = Wrapper_DMM_1_13_Reset;
	DevDat[33].d_acts[A_FNC] = Wrapper_DMM_1_13_Setup;
	DevDat[33].d_acts[A_STA] = Wrapper_DMM_1_13_Status;
//
//	DMM_1:CH14
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ACCF);  // ac-comp-freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[34].d_modlst = p_mod;
	DevDat[34].d_fncP = 14;
	DevDat[34].d_acts[A_CLS] = Wrapper_DMM_1_14_Close;
	DevDat[34].d_acts[A_CON] = Wrapper_DMM_1_14_Connect;
	DevDat[34].d_acts[A_DIS] = Wrapper_DMM_1_14_Disconnect;
	DevDat[34].d_acts[A_FTH] = Wrapper_DMM_1_14_Fetch;
	DevDat[34].d_acts[A_INX] = Wrapper_DMM_1_14_Init;
	DevDat[34].d_acts[A_LOD] = Wrapper_DMM_1_14_Load;
	DevDat[34].d_acts[A_OPN] = Wrapper_DMM_1_14_Open;
	DevDat[34].d_acts[A_RST] = Wrapper_DMM_1_14_Reset;
	DevDat[34].d_acts[A_FNC] = Wrapper_DMM_1_14_Setup;
	DevDat[34].d_acts[A_STA] = Wrapper_DMM_1_14_Status;
//
//	DMM_1:CH15
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[35].d_modlst = p_mod;
	DevDat[35].d_fncP = 15;
	DevDat[35].d_acts[A_CLS] = Wrapper_DMM_1_15_Close;
	DevDat[35].d_acts[A_CON] = Wrapper_DMM_1_15_Connect;
	DevDat[35].d_acts[A_DIS] = Wrapper_DMM_1_15_Disconnect;
	DevDat[35].d_acts[A_FTH] = Wrapper_DMM_1_15_Fetch;
	DevDat[35].d_acts[A_INX] = Wrapper_DMM_1_15_Init;
	DevDat[35].d_acts[A_LOD] = Wrapper_DMM_1_15_Load;
	DevDat[35].d_acts[A_OPN] = Wrapper_DMM_1_15_Open;
	DevDat[35].d_acts[A_RST] = Wrapper_DMM_1_15_Reset;
	DevDat[35].d_acts[A_FNC] = Wrapper_DMM_1_15_Setup;
	DevDat[35].d_acts[A_STA] = Wrapper_DMM_1_15_Status;
//
//	DMM_1:CH16
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[36].d_modlst = p_mod;
	DevDat[36].d_fncP = 16;
	DevDat[36].d_acts[A_CLS] = Wrapper_DMM_1_16_Close;
	DevDat[36].d_acts[A_CON] = Wrapper_DMM_1_16_Connect;
	DevDat[36].d_acts[A_DIS] = Wrapper_DMM_1_16_Disconnect;
	DevDat[36].d_acts[A_FTH] = Wrapper_DMM_1_16_Fetch;
	DevDat[36].d_acts[A_INX] = Wrapper_DMM_1_16_Init;
	DevDat[36].d_acts[A_LOD] = Wrapper_DMM_1_16_Load;
	DevDat[36].d_acts[A_OPN] = Wrapper_DMM_1_16_Open;
	DevDat[36].d_acts[A_RST] = Wrapper_DMM_1_16_Reset;
	DevDat[36].d_acts[A_FNC] = Wrapper_DMM_1_16_Setup;
	DevDat[36].d_acts[A_STA] = Wrapper_DMM_1_16_Status;
//
//	DMM_1:CH17
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[37].d_modlst = p_mod;
	DevDat[37].d_fncP = 17;
	DevDat[37].d_acts[A_CLS] = Wrapper_DMM_1_17_Close;
	DevDat[37].d_acts[A_CON] = Wrapper_DMM_1_17_Connect;
	DevDat[37].d_acts[A_DIS] = Wrapper_DMM_1_17_Disconnect;
	DevDat[37].d_acts[A_FTH] = Wrapper_DMM_1_17_Fetch;
	DevDat[37].d_acts[A_INX] = Wrapper_DMM_1_17_Init;
	DevDat[37].d_acts[A_LOD] = Wrapper_DMM_1_17_Load;
	DevDat[37].d_acts[A_OPN] = Wrapper_DMM_1_17_Open;
	DevDat[37].d_acts[A_RST] = Wrapper_DMM_1_17_Reset;
	DevDat[37].d_acts[A_FNC] = Wrapper_DMM_1_17_Setup;
	DevDat[37].d_acts[A_STA] = Wrapper_DMM_1_17_Status;
//
//	DMM_1:CH18
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[38].d_modlst = p_mod;
	DevDat[38].d_fncP = 18;
	DevDat[38].d_acts[A_CLS] = Wrapper_DMM_1_18_Close;
	DevDat[38].d_acts[A_CON] = Wrapper_DMM_1_18_Connect;
	DevDat[38].d_acts[A_DIS] = Wrapper_DMM_1_18_Disconnect;
	DevDat[38].d_acts[A_FTH] = Wrapper_DMM_1_18_Fetch;
	DevDat[38].d_acts[A_INX] = Wrapper_DMM_1_18_Init;
	DevDat[38].d_acts[A_LOD] = Wrapper_DMM_1_18_Load;
	DevDat[38].d_acts[A_OPN] = Wrapper_DMM_1_18_Open;
	DevDat[38].d_acts[A_RST] = Wrapper_DMM_1_18_Reset;
	DevDat[38].d_acts[A_FNC] = Wrapper_DMM_1_18_Setup;
	DevDat[38].d_acts[A_STA] = Wrapper_DMM_1_18_Status;
//
//	DMM_1:CH19
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[39].d_modlst = p_mod;
	DevDat[39].d_fncP = 19;
	DevDat[39].d_acts[A_CLS] = Wrapper_DMM_1_19_Close;
	DevDat[39].d_acts[A_CON] = Wrapper_DMM_1_19_Connect;
	DevDat[39].d_acts[A_DIS] = Wrapper_DMM_1_19_Disconnect;
	DevDat[39].d_acts[A_FTH] = Wrapper_DMM_1_19_Fetch;
	DevDat[39].d_acts[A_INX] = Wrapper_DMM_1_19_Init;
	DevDat[39].d_acts[A_LOD] = Wrapper_DMM_1_19_Load;
	DevDat[39].d_acts[A_OPN] = Wrapper_DMM_1_19_Open;
	DevDat[39].d_acts[A_RST] = Wrapper_DMM_1_19_Reset;
	DevDat[39].d_acts[A_FNC] = Wrapper_DMM_1_19_Setup;
	DevDat[39].d_acts[A_STA] = Wrapper_DMM_1_19_Status;
//
//	DMM_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[40].d_modlst = p_mod;
	DevDat[40].d_fncP = 2;
	DevDat[40].d_acts[A_CLS] = Wrapper_DMM_1_2_Close;
	DevDat[40].d_acts[A_CON] = Wrapper_DMM_1_2_Connect;
	DevDat[40].d_acts[A_DIS] = Wrapper_DMM_1_2_Disconnect;
	DevDat[40].d_acts[A_FTH] = Wrapper_DMM_1_2_Fetch;
	DevDat[40].d_acts[A_INX] = Wrapper_DMM_1_2_Init;
	DevDat[40].d_acts[A_LOD] = Wrapper_DMM_1_2_Load;
	DevDat[40].d_acts[A_OPN] = Wrapper_DMM_1_2_Open;
	DevDat[40].d_acts[A_RST] = Wrapper_DMM_1_2_Reset;
	DevDat[40].d_acts[A_FNC] = Wrapper_DMM_1_2_Setup;
	DevDat[40].d_acts[A_STA] = Wrapper_DMM_1_2_Status;
//
//	DMM_1:CH20
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[41].d_modlst = p_mod;
	DevDat[41].d_fncP = 20;
	DevDat[41].d_acts[A_CLS] = Wrapper_DMM_1_20_Close;
	DevDat[41].d_acts[A_CON] = Wrapper_DMM_1_20_Connect;
	DevDat[41].d_acts[A_DIS] = Wrapper_DMM_1_20_Disconnect;
	DevDat[41].d_acts[A_FTH] = Wrapper_DMM_1_20_Fetch;
	DevDat[41].d_acts[A_INX] = Wrapper_DMM_1_20_Init;
	DevDat[41].d_acts[A_LOD] = Wrapper_DMM_1_20_Load;
	DevDat[41].d_acts[A_OPN] = Wrapper_DMM_1_20_Open;
	DevDat[41].d_acts[A_RST] = Wrapper_DMM_1_20_Reset;
	DevDat[41].d_acts[A_FNC] = Wrapper_DMM_1_20_Setup;
	DevDat[41].d_acts[A_STA] = Wrapper_DMM_1_20_Status;
//
//	DMM_1:CH202
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DELA);  // delay
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[42].d_modlst = p_mod;
	DevDat[42].d_fncP = 202;
	DevDat[42].d_acts[A_CLS] = Wrapper_DMM_1_202_Close;
	DevDat[42].d_acts[A_CON] = Wrapper_DMM_1_202_Connect;
	DevDat[42].d_acts[A_DIS] = Wrapper_DMM_1_202_Disconnect;
	DevDat[42].d_acts[A_FTH] = Wrapper_DMM_1_202_Fetch;
	DevDat[42].d_acts[A_INX] = Wrapper_DMM_1_202_Init;
	DevDat[42].d_acts[A_LOD] = Wrapper_DMM_1_202_Load;
	DevDat[42].d_acts[A_OPN] = Wrapper_DMM_1_202_Open;
	DevDat[42].d_acts[A_RST] = Wrapper_DMM_1_202_Reset;
	DevDat[42].d_acts[A_FNC] = Wrapper_DMM_1_202_Setup;
	DevDat[42].d_acts[A_STA] = Wrapper_DMM_1_202_Status;
//
//	DMM_1:CH21
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[43].d_modlst = p_mod;
	DevDat[43].d_fncP = 21;
	DevDat[43].d_acts[A_CLS] = Wrapper_DMM_1_21_Close;
	DevDat[43].d_acts[A_CON] = Wrapper_DMM_1_21_Connect;
	DevDat[43].d_acts[A_DIS] = Wrapper_DMM_1_21_Disconnect;
	DevDat[43].d_acts[A_FTH] = Wrapper_DMM_1_21_Fetch;
	DevDat[43].d_acts[A_INX] = Wrapper_DMM_1_21_Init;
	DevDat[43].d_acts[A_LOD] = Wrapper_DMM_1_21_Load;
	DevDat[43].d_acts[A_OPN] = Wrapper_DMM_1_21_Open;
	DevDat[43].d_acts[A_RST] = Wrapper_DMM_1_21_Reset;
	DevDat[43].d_acts[A_FNC] = Wrapper_DMM_1_21_Setup;
	DevDat[43].d_acts[A_STA] = Wrapper_DMM_1_21_Status;
//
//	DMM_1:CH22
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[44].d_modlst = p_mod;
	DevDat[44].d_fncP = 22;
	DevDat[44].d_acts[A_CLS] = Wrapper_DMM_1_22_Close;
	DevDat[44].d_acts[A_CON] = Wrapper_DMM_1_22_Connect;
	DevDat[44].d_acts[A_DIS] = Wrapper_DMM_1_22_Disconnect;
	DevDat[44].d_acts[A_FTH] = Wrapper_DMM_1_22_Fetch;
	DevDat[44].d_acts[A_INX] = Wrapper_DMM_1_22_Init;
	DevDat[44].d_acts[A_LOD] = Wrapper_DMM_1_22_Load;
	DevDat[44].d_acts[A_OPN] = Wrapper_DMM_1_22_Open;
	DevDat[44].d_acts[A_RST] = Wrapper_DMM_1_22_Reset;
	DevDat[44].d_acts[A_FNC] = Wrapper_DMM_1_22_Setup;
	DevDat[44].d_acts[A_STA] = Wrapper_DMM_1_22_Status;
//
//	DMM_1:CH23
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[45].d_modlst = p_mod;
	DevDat[45].d_fncP = 23;
	DevDat[45].d_acts[A_CLS] = Wrapper_DMM_1_23_Close;
	DevDat[45].d_acts[A_CON] = Wrapper_DMM_1_23_Connect;
	DevDat[45].d_acts[A_DIS] = Wrapper_DMM_1_23_Disconnect;
	DevDat[45].d_acts[A_FTH] = Wrapper_DMM_1_23_Fetch;
	DevDat[45].d_acts[A_INX] = Wrapper_DMM_1_23_Init;
	DevDat[45].d_acts[A_LOD] = Wrapper_DMM_1_23_Load;
	DevDat[45].d_acts[A_OPN] = Wrapper_DMM_1_23_Open;
	DevDat[45].d_acts[A_RST] = Wrapper_DMM_1_23_Reset;
	DevDat[45].d_acts[A_FNC] = Wrapper_DMM_1_23_Setup;
	DevDat[45].d_acts[A_STA] = Wrapper_DMM_1_23_Status;
//
//	DMM_1:CH25
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[46].d_modlst = p_mod;
	DevDat[46].d_fncP = 25;
	DevDat[46].d_acts[A_CLS] = Wrapper_DMM_1_25_Close;
	DevDat[46].d_acts[A_CON] = Wrapper_DMM_1_25_Connect;
	DevDat[46].d_acts[A_DIS] = Wrapper_DMM_1_25_Disconnect;
	DevDat[46].d_acts[A_FTH] = Wrapper_DMM_1_25_Fetch;
	DevDat[46].d_acts[A_INX] = Wrapper_DMM_1_25_Init;
	DevDat[46].d_acts[A_LOD] = Wrapper_DMM_1_25_Load;
	DevDat[46].d_acts[A_OPN] = Wrapper_DMM_1_25_Open;
	DevDat[46].d_acts[A_RST] = Wrapper_DMM_1_25_Reset;
	DevDat[46].d_acts[A_FNC] = Wrapper_DMM_1_25_Setup;
	DevDat[46].d_acts[A_STA] = Wrapper_DMM_1_25_Status;
//
//	DMM_1:CH26
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[47].d_modlst = p_mod;
	DevDat[47].d_fncP = 26;
	DevDat[47].d_acts[A_CLS] = Wrapper_DMM_1_26_Close;
	DevDat[47].d_acts[A_CON] = Wrapper_DMM_1_26_Connect;
	DevDat[47].d_acts[A_DIS] = Wrapper_DMM_1_26_Disconnect;
	DevDat[47].d_acts[A_FTH] = Wrapper_DMM_1_26_Fetch;
	DevDat[47].d_acts[A_INX] = Wrapper_DMM_1_26_Init;
	DevDat[47].d_acts[A_LOD] = Wrapper_DMM_1_26_Load;
	DevDat[47].d_acts[A_OPN] = Wrapper_DMM_1_26_Open;
	DevDat[47].d_acts[A_RST] = Wrapper_DMM_1_26_Reset;
	DevDat[47].d_acts[A_FNC] = Wrapper_DMM_1_26_Setup;
	DevDat[47].d_acts[A_STA] = Wrapper_DMM_1_26_Status;
//
//	DMM_1:CH27
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[48].d_modlst = p_mod;
	DevDat[48].d_fncP = 27;
	DevDat[48].d_acts[A_CLS] = Wrapper_DMM_1_27_Close;
	DevDat[48].d_acts[A_CON] = Wrapper_DMM_1_27_Connect;
	DevDat[48].d_acts[A_DIS] = Wrapper_DMM_1_27_Disconnect;
	DevDat[48].d_acts[A_FTH] = Wrapper_DMM_1_27_Fetch;
	DevDat[48].d_acts[A_INX] = Wrapper_DMM_1_27_Init;
	DevDat[48].d_acts[A_LOD] = Wrapper_DMM_1_27_Load;
	DevDat[48].d_acts[A_OPN] = Wrapper_DMM_1_27_Open;
	DevDat[48].d_acts[A_RST] = Wrapper_DMM_1_27_Reset;
	DevDat[48].d_acts[A_FNC] = Wrapper_DMM_1_27_Setup;
	DevDat[48].d_acts[A_STA] = Wrapper_DMM_1_27_Status;
//
//	DMM_1:CH28
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[49].d_modlst = p_mod;
	DevDat[49].d_fncP = 28;
	DevDat[49].d_acts[A_CLS] = Wrapper_DMM_1_28_Close;
	DevDat[49].d_acts[A_CON] = Wrapper_DMM_1_28_Connect;
	DevDat[49].d_acts[A_DIS] = Wrapper_DMM_1_28_Disconnect;
	DevDat[49].d_acts[A_FTH] = Wrapper_DMM_1_28_Fetch;
	DevDat[49].d_acts[A_INX] = Wrapper_DMM_1_28_Init;
	DevDat[49].d_acts[A_LOD] = Wrapper_DMM_1_28_Load;
	DevDat[49].d_acts[A_OPN] = Wrapper_DMM_1_28_Open;
	DevDat[49].d_acts[A_RST] = Wrapper_DMM_1_28_Reset;
	DevDat[49].d_acts[A_FNC] = Wrapper_DMM_1_28_Setup;
	DevDat[49].d_acts[A_STA] = Wrapper_DMM_1_28_Status;
//
//	DMM_1:CH3
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFR);  // ref-res
	p_mod = BldModDat (p_mod, (short) M_RESI);  // res
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_FORW);  // four-wire
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[50].d_modlst = p_mod;
	DevDat[50].d_fncP = 3;
	DevDat[50].d_acts[A_CLS] = Wrapper_DMM_1_3_Close;
	DevDat[50].d_acts[A_CON] = Wrapper_DMM_1_3_Connect;
	DevDat[50].d_acts[A_DIS] = Wrapper_DMM_1_3_Disconnect;
	DevDat[50].d_acts[A_FTH] = Wrapper_DMM_1_3_Fetch;
	DevDat[50].d_acts[A_INX] = Wrapper_DMM_1_3_Init;
	DevDat[50].d_acts[A_LOD] = Wrapper_DMM_1_3_Load;
	DevDat[50].d_acts[A_OPN] = Wrapper_DMM_1_3_Open;
	DevDat[50].d_acts[A_RST] = Wrapper_DMM_1_3_Reset;
	DevDat[50].d_acts[A_FNC] = Wrapper_DMM_1_3_Setup;
	DevDat[50].d_acts[A_STA] = Wrapper_DMM_1_3_Status;
//
//	DMM_1:CH4
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[51].d_modlst = p_mod;
	DevDat[51].d_fncP = 4;
	DevDat[51].d_acts[A_CLS] = Wrapper_DMM_1_4_Close;
	DevDat[51].d_acts[A_CON] = Wrapper_DMM_1_4_Connect;
	DevDat[51].d_acts[A_DIS] = Wrapper_DMM_1_4_Disconnect;
	DevDat[51].d_acts[A_FTH] = Wrapper_DMM_1_4_Fetch;
	DevDat[51].d_acts[A_INX] = Wrapper_DMM_1_4_Init;
	DevDat[51].d_acts[A_LOD] = Wrapper_DMM_1_4_Load;
	DevDat[51].d_acts[A_OPN] = Wrapper_DMM_1_4_Open;
	DevDat[51].d_acts[A_RST] = Wrapper_DMM_1_4_Reset;
	DevDat[51].d_acts[A_FNC] = Wrapper_DMM_1_4_Setup;
	DevDat[51].d_acts[A_STA] = Wrapper_DMM_1_4_Status;
//
//	DMM_1:CH5
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[52].d_modlst = p_mod;
	DevDat[52].d_fncP = 5;
	DevDat[52].d_acts[A_CLS] = Wrapper_DMM_1_5_Close;
	DevDat[52].d_acts[A_CON] = Wrapper_DMM_1_5_Connect;
	DevDat[52].d_acts[A_DIS] = Wrapper_DMM_1_5_Disconnect;
	DevDat[52].d_acts[A_FTH] = Wrapper_DMM_1_5_Fetch;
	DevDat[52].d_acts[A_INX] = Wrapper_DMM_1_5_Init;
	DevDat[52].d_acts[A_LOD] = Wrapper_DMM_1_5_Load;
	DevDat[52].d_acts[A_OPN] = Wrapper_DMM_1_5_Open;
	DevDat[52].d_acts[A_RST] = Wrapper_DMM_1_5_Reset;
	DevDat[52].d_acts[A_FNC] = Wrapper_DMM_1_5_Setup;
	DevDat[52].d_acts[A_STA] = Wrapper_DMM_1_5_Status;
//
//	DMM_1:CH6
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[53].d_modlst = p_mod;
	DevDat[53].d_fncP = 6;
	DevDat[53].d_acts[A_CLS] = Wrapper_DMM_1_6_Close;
	DevDat[53].d_acts[A_CON] = Wrapper_DMM_1_6_Connect;
	DevDat[53].d_acts[A_DIS] = Wrapper_DMM_1_6_Disconnect;
	DevDat[53].d_acts[A_FTH] = Wrapper_DMM_1_6_Fetch;
	DevDat[53].d_acts[A_INX] = Wrapper_DMM_1_6_Init;
	DevDat[53].d_acts[A_LOD] = Wrapper_DMM_1_6_Load;
	DevDat[53].d_acts[A_OPN] = Wrapper_DMM_1_6_Open;
	DevDat[53].d_acts[A_RST] = Wrapper_DMM_1_6_Reset;
	DevDat[53].d_acts[A_FNC] = Wrapper_DMM_1_6_Setup;
	DevDat[53].d_acts[A_STA] = Wrapper_DMM_1_6_Status;
//
//	DMM_1:CH7
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[54].d_modlst = p_mod;
	DevDat[54].d_fncP = 7;
	DevDat[54].d_acts[A_CLS] = Wrapper_DMM_1_7_Close;
	DevDat[54].d_acts[A_CON] = Wrapper_DMM_1_7_Connect;
	DevDat[54].d_acts[A_DIS] = Wrapper_DMM_1_7_Disconnect;
	DevDat[54].d_acts[A_FTH] = Wrapper_DMM_1_7_Fetch;
	DevDat[54].d_acts[A_INX] = Wrapper_DMM_1_7_Init;
	DevDat[54].d_acts[A_LOD] = Wrapper_DMM_1_7_Load;
	DevDat[54].d_acts[A_OPN] = Wrapper_DMM_1_7_Open;
	DevDat[54].d_acts[A_RST] = Wrapper_DMM_1_7_Reset;
	DevDat[54].d_acts[A_FNC] = Wrapper_DMM_1_7_Setup;
	DevDat[54].d_acts[A_STA] = Wrapper_DMM_1_7_Status;
//
//	DMM_1:CH8
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VOLR);  // voltage-ratio
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[55].d_modlst = p_mod;
	DevDat[55].d_fncP = 8;
	DevDat[55].d_acts[A_CLS] = Wrapper_DMM_1_8_Close;
	DevDat[55].d_acts[A_CON] = Wrapper_DMM_1_8_Connect;
	DevDat[55].d_acts[A_DIS] = Wrapper_DMM_1_8_Disconnect;
	DevDat[55].d_acts[A_FTH] = Wrapper_DMM_1_8_Fetch;
	DevDat[55].d_acts[A_INX] = Wrapper_DMM_1_8_Init;
	DevDat[55].d_acts[A_LOD] = Wrapper_DMM_1_8_Load;
	DevDat[55].d_acts[A_OPN] = Wrapper_DMM_1_8_Open;
	DevDat[55].d_acts[A_RST] = Wrapper_DMM_1_8_Reset;
	DevDat[55].d_acts[A_FNC] = Wrapper_DMM_1_8_Setup;
	DevDat[55].d_acts[A_STA] = Wrapper_DMM_1_8_Status;
//
//	DMM_1:CH9
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_REFV);  // ref-volt
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_SMPW);  // sample-width
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[56].d_modlst = p_mod;
	DevDat[56].d_fncP = 9;
	DevDat[56].d_acts[A_CLS] = Wrapper_DMM_1_9_Close;
	DevDat[56].d_acts[A_CON] = Wrapper_DMM_1_9_Connect;
	DevDat[56].d_acts[A_DIS] = Wrapper_DMM_1_9_Disconnect;
	DevDat[56].d_acts[A_FTH] = Wrapper_DMM_1_9_Fetch;
	DevDat[56].d_acts[A_INX] = Wrapper_DMM_1_9_Init;
	DevDat[56].d_acts[A_LOD] = Wrapper_DMM_1_9_Load;
	DevDat[56].d_acts[A_OPN] = Wrapper_DMM_1_9_Open;
	DevDat[56].d_acts[A_RST] = Wrapper_DMM_1_9_Reset;
	DevDat[56].d_acts[A_FNC] = Wrapper_DMM_1_9_Setup;
	DevDat[56].d_acts[A_STA] = Wrapper_DMM_1_9_Status;
	return 0;
}
