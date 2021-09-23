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
extern int doCNTR_Close ();
extern int doCNTR_Connect ();
extern int doCNTR_Disconnect ();
extern int doCNTR_Fetch ();
extern int doCNTR_Init ();
extern int doCNTR_Load ();
extern int doCNTR_Open ();
extern int doCNTR_Reset ();
extern int doCNTR_Setup ();
extern int doCNTR_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"CNTR_1:CH1",
	"CNTR_1:CH10",
	"CNTR_1:CH101",
	"CNTR_1:CH102",
	"CNTR_1:CH103",
	"CNTR_1:CH106",
	"CNTR_1:CH107",
	"CNTR_1:CH108",
	"CNTR_1:CH109",
	"CNTR_1:CH11",
	"CNTR_1:CH110",
	"CNTR_1:CH113",
	"CNTR_1:CH114",
	"CNTR_1:CH115",
	"CNTR_1:CH118",
	"CNTR_1:CH119",
	"CNTR_1:CH12",
	"CNTR_1:CH122",
	"CNTR_1:CH123",
	"CNTR_1:CH126",
	"CNTR_1:CH127",
	"CNTR_1:CH13",
	"CNTR_1:CH130",
	"CNTR_1:CH131",
	"CNTR_1:CH132",
	"CNTR_1:CH133",
	"CNTR_1:CH134",
	"CNTR_1:CH135",
	"CNTR_1:CH136",
	"CNTR_1:CH137",
	"CNTR_1:CH138",
	"CNTR_1:CH139",
	"CNTR_1:CH14",
	"CNTR_1:CH140",
	"CNTR_1:CH141",
	"CNTR_1:CH142",
	"CNTR_1:CH143",
	"CNTR_1:CH144",
	"CNTR_1:CH145",
	"CNTR_1:CH146",
	"CNTR_1:CH147",
	"CNTR_1:CH148",
	"CNTR_1:CH15",
	"CNTR_1:CH150",
	"CNTR_1:CH151",
	"CNTR_1:CH152",
	"CNTR_1:CH153",
	"CNTR_1:CH16",
	"CNTR_1:CH17",
	"CNTR_1:CH18",
	"CNTR_1:CH19",
	"CNTR_1:CH2",
	"CNTR_1:CH20",
	"CNTR_1:CH21",
	"CNTR_1:CH213",
	"CNTR_1:CH214",
	"CNTR_1:CH22",
	"CNTR_1:CH23",
	"CNTR_1:CH24",
	"CNTR_1:CH25",
	"CNTR_1:CH26",
	"CNTR_1:CH27",
	"CNTR_1:CH3",
	"CNTR_1:CH4",
	"CNTR_1:CH5",
	"CNTR_1:CH6",
	"CNTR_1:CH7",
	"CNTR_1:CH8",
	"CNTR_1:CH9",
};
DECLAREC int DevCnt = 71;
int CCALLBACK Wrapper_CNTR_1_1_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_1_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_10_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_101_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_102_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_103_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_106_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_107_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_108_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_109_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_11_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_110_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_113_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_114_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_115_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_118_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_119_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_12_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_122_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_123_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_126_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_127_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_13_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_130_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_131_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_132_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_133_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_134_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_135_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_136_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_137_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_138_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_139_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_14_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_140_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_141_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_142_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_143_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_144_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_145_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_146_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_147_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_148_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_15_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_150_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_151_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_152_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_153_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_16_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_17_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_18_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_19_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_2_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_20_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_21_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_213_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_214_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_22_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_23_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_24_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_25_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_26_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_27_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_3_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_4_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_5_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_6_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_7_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_8_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Close(void)
{
	if (doCNTR_Close() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Connect(void)
{
	if (doCNTR_Connect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Disconnect(void)
{
	if (doCNTR_Disconnect() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Fetch(void)
{
	if (doCNTR_Fetch() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Init(void)
{
	if (doCNTR_Init() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Load(void)
{
	if (doCNTR_Load() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Open(void)
{
	if (doCNTR_Open() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Reset(void)
{
	if (doCNTR_Reset() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Setup(void)
{
	if (doCNTR_Setup() < 0)
		BusErr ("CNTR_1");
	return 0;
}
int CCALLBACK Wrapper_CNTR_1_9_Status(void)
{
	if (doCNTR_Status() < 0)
		BusErr ("CNTR_1");
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
//	CNTR_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQR);  // freq-ratio
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PANG);  // phase-angle
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_CNTR_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_CNTR_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_CNTR_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_CNTR_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_CNTR_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_CNTR_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_CNTR_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_CNTR_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_CNTR_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_CNTR_1_1_Status;
//
//	CNTR_1:CH10
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 10;
	DevDat[3].d_acts[A_CLS] = Wrapper_CNTR_1_10_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_CNTR_1_10_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_CNTR_1_10_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_CNTR_1_10_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_CNTR_1_10_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_CNTR_1_10_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_CNTR_1_10_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_CNTR_1_10_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_CNTR_1_10_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_CNTR_1_10_Status;
//
//	CNTR_1:CH101
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQR);  // freq-ratio
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PANG);  // phase-angle
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 101;
	DevDat[4].d_acts[A_CLS] = Wrapper_CNTR_1_101_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_CNTR_1_101_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_CNTR_1_101_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_CNTR_1_101_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_CNTR_1_101_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_CNTR_1_101_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_CNTR_1_101_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_CNTR_1_101_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_CNTR_1_101_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_CNTR_1_101_Status;
//
//	CNTR_1:CH102
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQR);  // freq-ratio
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PANG);  // phase-angle
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 102;
	DevDat[5].d_acts[A_CLS] = Wrapper_CNTR_1_102_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_CNTR_1_102_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_CNTR_1_102_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_CNTR_1_102_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_CNTR_1_102_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_CNTR_1_102_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_CNTR_1_102_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_CNTR_1_102_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_CNTR_1_102_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_CNTR_1_102_Status;
//
//	CNTR_1:CH103
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQR);  // freq-ratio
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PANG);  // phase-angle
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 103;
	DevDat[6].d_acts[A_CLS] = Wrapper_CNTR_1_103_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_CNTR_1_103_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_CNTR_1_103_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_CNTR_1_103_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_CNTR_1_103_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_CNTR_1_103_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_CNTR_1_103_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_CNTR_1_103_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_CNTR_1_103_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_CNTR_1_103_Status;
//
//	CNTR_1:CH106
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 106;
	DevDat[7].d_acts[A_CLS] = Wrapper_CNTR_1_106_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_CNTR_1_106_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_CNTR_1_106_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_CNTR_1_106_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_CNTR_1_106_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_CNTR_1_106_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_CNTR_1_106_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_CNTR_1_106_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_CNTR_1_106_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_CNTR_1_106_Status;
//
//	CNTR_1:CH107
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 107;
	DevDat[8].d_acts[A_CLS] = Wrapper_CNTR_1_107_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_CNTR_1_107_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_CNTR_1_107_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_CNTR_1_107_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_CNTR_1_107_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_CNTR_1_107_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_CNTR_1_107_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_CNTR_1_107_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_CNTR_1_107_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_CNTR_1_107_Status;
//
//	CNTR_1:CH108
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 108;
	DevDat[9].d_acts[A_CLS] = Wrapper_CNTR_1_108_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_CNTR_1_108_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_CNTR_1_108_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_CNTR_1_108_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_CNTR_1_108_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_CNTR_1_108_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_CNTR_1_108_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_CNTR_1_108_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_CNTR_1_108_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_CNTR_1_108_Status;
//
//	CNTR_1:CH109
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 109;
	DevDat[10].d_acts[A_CLS] = Wrapper_CNTR_1_109_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_CNTR_1_109_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_CNTR_1_109_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_CNTR_1_109_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_CNTR_1_109_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_CNTR_1_109_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_CNTR_1_109_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_CNTR_1_109_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_CNTR_1_109_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_CNTR_1_109_Status;
//
//	CNTR_1:CH11
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COUN);  // count
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TIME);  // time
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 11;
	DevDat[11].d_acts[A_CLS] = Wrapper_CNTR_1_11_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_CNTR_1_11_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_CNTR_1_11_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_CNTR_1_11_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_CNTR_1_11_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_CNTR_1_11_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_CNTR_1_11_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_CNTR_1_11_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_CNTR_1_11_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_CNTR_1_11_Status;
//
//	CNTR_1:CH110
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 110;
	DevDat[12].d_acts[A_CLS] = Wrapper_CNTR_1_110_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_CNTR_1_110_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_CNTR_1_110_Disconnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_CNTR_1_110_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_CNTR_1_110_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_CNTR_1_110_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_CNTR_1_110_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_CNTR_1_110_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_CNTR_1_110_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_CNTR_1_110_Status;
//
//	CNTR_1:CH113
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 113;
	DevDat[13].d_acts[A_CLS] = Wrapper_CNTR_1_113_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_CNTR_1_113_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_CNTR_1_113_Disconnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_CNTR_1_113_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_CNTR_1_113_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_CNTR_1_113_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_CNTR_1_113_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_CNTR_1_113_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_CNTR_1_113_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_CNTR_1_113_Status;
//
//	CNTR_1:CH114
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 114;
	DevDat[14].d_acts[A_CLS] = Wrapper_CNTR_1_114_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_CNTR_1_114_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_CNTR_1_114_Disconnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_CNTR_1_114_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_CNTR_1_114_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_CNTR_1_114_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_CNTR_1_114_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_CNTR_1_114_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_CNTR_1_114_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_CNTR_1_114_Status;
//
//	CNTR_1:CH115
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQR);  // freq-ratio
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PANG);  // phase-angle
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 115;
	DevDat[15].d_acts[A_CLS] = Wrapper_CNTR_1_115_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_CNTR_1_115_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_CNTR_1_115_Disconnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_CNTR_1_115_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_CNTR_1_115_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_CNTR_1_115_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_CNTR_1_115_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_CNTR_1_115_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_CNTR_1_115_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_CNTR_1_115_Status;
//
//	CNTR_1:CH118
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 118;
	DevDat[16].d_acts[A_CLS] = Wrapper_CNTR_1_118_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_CNTR_1_118_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_CNTR_1_118_Disconnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_CNTR_1_118_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_CNTR_1_118_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_CNTR_1_118_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_CNTR_1_118_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_CNTR_1_118_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_CNTR_1_118_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_CNTR_1_118_Status;
//
//	CNTR_1:CH119
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 119;
	DevDat[17].d_acts[A_CLS] = Wrapper_CNTR_1_119_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_CNTR_1_119_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_CNTR_1_119_Disconnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_CNTR_1_119_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_CNTR_1_119_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_CNTR_1_119_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_CNTR_1_119_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_CNTR_1_119_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_CNTR_1_119_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_CNTR_1_119_Status;
//
//	CNTR_1:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COUN);  // count
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TIME);  // time
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[18].d_modlst = p_mod;
	DevDat[18].d_fncP = 12;
	DevDat[18].d_acts[A_CLS] = Wrapper_CNTR_1_12_Close;
	DevDat[18].d_acts[A_CON] = Wrapper_CNTR_1_12_Connect;
	DevDat[18].d_acts[A_DIS] = Wrapper_CNTR_1_12_Disconnect;
	DevDat[18].d_acts[A_FTH] = Wrapper_CNTR_1_12_Fetch;
	DevDat[18].d_acts[A_INX] = Wrapper_CNTR_1_12_Init;
	DevDat[18].d_acts[A_LOD] = Wrapper_CNTR_1_12_Load;
	DevDat[18].d_acts[A_OPN] = Wrapper_CNTR_1_12_Open;
	DevDat[18].d_acts[A_RST] = Wrapper_CNTR_1_12_Reset;
	DevDat[18].d_acts[A_FNC] = Wrapper_CNTR_1_12_Setup;
	DevDat[18].d_acts[A_STA] = Wrapper_CNTR_1_12_Status;
//
//	CNTR_1:CH122
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[19].d_modlst = p_mod;
	DevDat[19].d_fncP = 122;
	DevDat[19].d_acts[A_CLS] = Wrapper_CNTR_1_122_Close;
	DevDat[19].d_acts[A_CON] = Wrapper_CNTR_1_122_Connect;
	DevDat[19].d_acts[A_DIS] = Wrapper_CNTR_1_122_Disconnect;
	DevDat[19].d_acts[A_FTH] = Wrapper_CNTR_1_122_Fetch;
	DevDat[19].d_acts[A_INX] = Wrapper_CNTR_1_122_Init;
	DevDat[19].d_acts[A_LOD] = Wrapper_CNTR_1_122_Load;
	DevDat[19].d_acts[A_OPN] = Wrapper_CNTR_1_122_Open;
	DevDat[19].d_acts[A_RST] = Wrapper_CNTR_1_122_Reset;
	DevDat[19].d_acts[A_FNC] = Wrapper_CNTR_1_122_Setup;
	DevDat[19].d_acts[A_STA] = Wrapper_CNTR_1_122_Status;
//
//	CNTR_1:CH123
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[20].d_modlst = p_mod;
	DevDat[20].d_fncP = 123;
	DevDat[20].d_acts[A_CLS] = Wrapper_CNTR_1_123_Close;
	DevDat[20].d_acts[A_CON] = Wrapper_CNTR_1_123_Connect;
	DevDat[20].d_acts[A_DIS] = Wrapper_CNTR_1_123_Disconnect;
	DevDat[20].d_acts[A_FTH] = Wrapper_CNTR_1_123_Fetch;
	DevDat[20].d_acts[A_INX] = Wrapper_CNTR_1_123_Init;
	DevDat[20].d_acts[A_LOD] = Wrapper_CNTR_1_123_Load;
	DevDat[20].d_acts[A_OPN] = Wrapper_CNTR_1_123_Open;
	DevDat[20].d_acts[A_RST] = Wrapper_CNTR_1_123_Reset;
	DevDat[20].d_acts[A_FNC] = Wrapper_CNTR_1_123_Setup;
	DevDat[20].d_acts[A_STA] = Wrapper_CNTR_1_123_Status;
//
//	CNTR_1:CH126
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[21].d_modlst = p_mod;
	DevDat[21].d_fncP = 126;
	DevDat[21].d_acts[A_CLS] = Wrapper_CNTR_1_126_Close;
	DevDat[21].d_acts[A_CON] = Wrapper_CNTR_1_126_Connect;
	DevDat[21].d_acts[A_DIS] = Wrapper_CNTR_1_126_Disconnect;
	DevDat[21].d_acts[A_FTH] = Wrapper_CNTR_1_126_Fetch;
	DevDat[21].d_acts[A_INX] = Wrapper_CNTR_1_126_Init;
	DevDat[21].d_acts[A_LOD] = Wrapper_CNTR_1_126_Load;
	DevDat[21].d_acts[A_OPN] = Wrapper_CNTR_1_126_Open;
	DevDat[21].d_acts[A_RST] = Wrapper_CNTR_1_126_Reset;
	DevDat[21].d_acts[A_FNC] = Wrapper_CNTR_1_126_Setup;
	DevDat[21].d_acts[A_STA] = Wrapper_CNTR_1_126_Status;
//
//	CNTR_1:CH127
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[22].d_modlst = p_mod;
	DevDat[22].d_fncP = 127;
	DevDat[22].d_acts[A_CLS] = Wrapper_CNTR_1_127_Close;
	DevDat[22].d_acts[A_CON] = Wrapper_CNTR_1_127_Connect;
	DevDat[22].d_acts[A_DIS] = Wrapper_CNTR_1_127_Disconnect;
	DevDat[22].d_acts[A_FTH] = Wrapper_CNTR_1_127_Fetch;
	DevDat[22].d_acts[A_INX] = Wrapper_CNTR_1_127_Init;
	DevDat[22].d_acts[A_LOD] = Wrapper_CNTR_1_127_Load;
	DevDat[22].d_acts[A_OPN] = Wrapper_CNTR_1_127_Open;
	DevDat[22].d_acts[A_RST] = Wrapper_CNTR_1_127_Reset;
	DevDat[22].d_acts[A_FNC] = Wrapper_CNTR_1_127_Setup;
	DevDat[22].d_acts[A_STA] = Wrapper_CNTR_1_127_Status;
//
//	CNTR_1:CH13
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[23].d_modlst = p_mod;
	DevDat[23].d_fncP = 13;
	DevDat[23].d_acts[A_CLS] = Wrapper_CNTR_1_13_Close;
	DevDat[23].d_acts[A_CON] = Wrapper_CNTR_1_13_Connect;
	DevDat[23].d_acts[A_DIS] = Wrapper_CNTR_1_13_Disconnect;
	DevDat[23].d_acts[A_FTH] = Wrapper_CNTR_1_13_Fetch;
	DevDat[23].d_acts[A_INX] = Wrapper_CNTR_1_13_Init;
	DevDat[23].d_acts[A_LOD] = Wrapper_CNTR_1_13_Load;
	DevDat[23].d_acts[A_OPN] = Wrapper_CNTR_1_13_Open;
	DevDat[23].d_acts[A_RST] = Wrapper_CNTR_1_13_Reset;
	DevDat[23].d_acts[A_FNC] = Wrapper_CNTR_1_13_Setup;
	DevDat[23].d_acts[A_STA] = Wrapper_CNTR_1_13_Status;
//
//	CNTR_1:CH130
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIME);  // time
	p_mod = BldModDat (p_mod, (short) M_EVTF);  // event-time-from
	p_mod = BldModDat (p_mod, (short) M_EVTT);  // event-time-to
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[24].d_modlst = p_mod;
	DevDat[24].d_fncP = 130;
	DevDat[24].d_acts[A_CLS] = Wrapper_CNTR_1_130_Close;
	DevDat[24].d_acts[A_CON] = Wrapper_CNTR_1_130_Connect;
	DevDat[24].d_acts[A_DIS] = Wrapper_CNTR_1_130_Disconnect;
	DevDat[24].d_acts[A_FTH] = Wrapper_CNTR_1_130_Fetch;
	DevDat[24].d_acts[A_INX] = Wrapper_CNTR_1_130_Init;
	DevDat[24].d_acts[A_LOD] = Wrapper_CNTR_1_130_Load;
	DevDat[24].d_acts[A_OPN] = Wrapper_CNTR_1_130_Open;
	DevDat[24].d_acts[A_RST] = Wrapper_CNTR_1_130_Reset;
	DevDat[24].d_acts[A_FNC] = Wrapper_CNTR_1_130_Setup;
	DevDat[24].d_acts[A_STA] = Wrapper_CNTR_1_130_Status;
//
//	CNTR_1:CH131
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[25].d_modlst = p_mod;
	DevDat[25].d_fncP = 131;
	DevDat[25].d_acts[A_CLS] = Wrapper_CNTR_1_131_Close;
	DevDat[25].d_acts[A_CON] = Wrapper_CNTR_1_131_Connect;
	DevDat[25].d_acts[A_DIS] = Wrapper_CNTR_1_131_Disconnect;
	DevDat[25].d_acts[A_FTH] = Wrapper_CNTR_1_131_Fetch;
	DevDat[25].d_acts[A_INX] = Wrapper_CNTR_1_131_Init;
	DevDat[25].d_acts[A_LOD] = Wrapper_CNTR_1_131_Load;
	DevDat[25].d_acts[A_OPN] = Wrapper_CNTR_1_131_Open;
	DevDat[25].d_acts[A_RST] = Wrapper_CNTR_1_131_Reset;
	DevDat[25].d_acts[A_FNC] = Wrapper_CNTR_1_131_Setup;
	DevDat[25].d_acts[A_STA] = Wrapper_CNTR_1_131_Status;
//
//	CNTR_1:CH132
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[26].d_modlst = p_mod;
	DevDat[26].d_fncP = 132;
	DevDat[26].d_acts[A_CLS] = Wrapper_CNTR_1_132_Close;
	DevDat[26].d_acts[A_CON] = Wrapper_CNTR_1_132_Connect;
	DevDat[26].d_acts[A_DIS] = Wrapper_CNTR_1_132_Disconnect;
	DevDat[26].d_acts[A_FTH] = Wrapper_CNTR_1_132_Fetch;
	DevDat[26].d_acts[A_INX] = Wrapper_CNTR_1_132_Init;
	DevDat[26].d_acts[A_LOD] = Wrapper_CNTR_1_132_Load;
	DevDat[26].d_acts[A_OPN] = Wrapper_CNTR_1_132_Open;
	DevDat[26].d_acts[A_RST] = Wrapper_CNTR_1_132_Reset;
	DevDat[26].d_acts[A_FNC] = Wrapper_CNTR_1_132_Setup;
	DevDat[26].d_acts[A_STA] = Wrapper_CNTR_1_132_Status;
//
//	CNTR_1:CH133
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[27].d_modlst = p_mod;
	DevDat[27].d_fncP = 133;
	DevDat[27].d_acts[A_CLS] = Wrapper_CNTR_1_133_Close;
	DevDat[27].d_acts[A_CON] = Wrapper_CNTR_1_133_Connect;
	DevDat[27].d_acts[A_DIS] = Wrapper_CNTR_1_133_Disconnect;
	DevDat[27].d_acts[A_FTH] = Wrapper_CNTR_1_133_Fetch;
	DevDat[27].d_acts[A_INX] = Wrapper_CNTR_1_133_Init;
	DevDat[27].d_acts[A_LOD] = Wrapper_CNTR_1_133_Load;
	DevDat[27].d_acts[A_OPN] = Wrapper_CNTR_1_133_Open;
	DevDat[27].d_acts[A_RST] = Wrapper_CNTR_1_133_Reset;
	DevDat[27].d_acts[A_FNC] = Wrapper_CNTR_1_133_Setup;
	DevDat[27].d_acts[A_STA] = Wrapper_CNTR_1_133_Status;
//
//	CNTR_1:CH134
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[28].d_modlst = p_mod;
	DevDat[28].d_fncP = 134;
	DevDat[28].d_acts[A_CLS] = Wrapper_CNTR_1_134_Close;
	DevDat[28].d_acts[A_CON] = Wrapper_CNTR_1_134_Connect;
	DevDat[28].d_acts[A_DIS] = Wrapper_CNTR_1_134_Disconnect;
	DevDat[28].d_acts[A_FTH] = Wrapper_CNTR_1_134_Fetch;
	DevDat[28].d_acts[A_INX] = Wrapper_CNTR_1_134_Init;
	DevDat[28].d_acts[A_LOD] = Wrapper_CNTR_1_134_Load;
	DevDat[28].d_acts[A_OPN] = Wrapper_CNTR_1_134_Open;
	DevDat[28].d_acts[A_RST] = Wrapper_CNTR_1_134_Reset;
	DevDat[28].d_acts[A_FNC] = Wrapper_CNTR_1_134_Setup;
	DevDat[28].d_acts[A_STA] = Wrapper_CNTR_1_134_Status;
//
//	CNTR_1:CH135
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[29].d_modlst = p_mod;
	DevDat[29].d_fncP = 135;
	DevDat[29].d_acts[A_CLS] = Wrapper_CNTR_1_135_Close;
	DevDat[29].d_acts[A_CON] = Wrapper_CNTR_1_135_Connect;
	DevDat[29].d_acts[A_DIS] = Wrapper_CNTR_1_135_Disconnect;
	DevDat[29].d_acts[A_FTH] = Wrapper_CNTR_1_135_Fetch;
	DevDat[29].d_acts[A_INX] = Wrapper_CNTR_1_135_Init;
	DevDat[29].d_acts[A_LOD] = Wrapper_CNTR_1_135_Load;
	DevDat[29].d_acts[A_OPN] = Wrapper_CNTR_1_135_Open;
	DevDat[29].d_acts[A_RST] = Wrapper_CNTR_1_135_Reset;
	DevDat[29].d_acts[A_FNC] = Wrapper_CNTR_1_135_Setup;
	DevDat[29].d_acts[A_STA] = Wrapper_CNTR_1_135_Status;
//
//	CNTR_1:CH136
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[30].d_modlst = p_mod;
	DevDat[30].d_fncP = 136;
	DevDat[30].d_acts[A_CLS] = Wrapper_CNTR_1_136_Close;
	DevDat[30].d_acts[A_CON] = Wrapper_CNTR_1_136_Connect;
	DevDat[30].d_acts[A_DIS] = Wrapper_CNTR_1_136_Disconnect;
	DevDat[30].d_acts[A_FTH] = Wrapper_CNTR_1_136_Fetch;
	DevDat[30].d_acts[A_INX] = Wrapper_CNTR_1_136_Init;
	DevDat[30].d_acts[A_LOD] = Wrapper_CNTR_1_136_Load;
	DevDat[30].d_acts[A_OPN] = Wrapper_CNTR_1_136_Open;
	DevDat[30].d_acts[A_RST] = Wrapper_CNTR_1_136_Reset;
	DevDat[30].d_acts[A_FNC] = Wrapper_CNTR_1_136_Setup;
	DevDat[30].d_acts[A_STA] = Wrapper_CNTR_1_136_Status;
//
//	CNTR_1:CH137
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[31].d_modlst = p_mod;
	DevDat[31].d_fncP = 137;
	DevDat[31].d_acts[A_CLS] = Wrapper_CNTR_1_137_Close;
	DevDat[31].d_acts[A_CON] = Wrapper_CNTR_1_137_Connect;
	DevDat[31].d_acts[A_DIS] = Wrapper_CNTR_1_137_Disconnect;
	DevDat[31].d_acts[A_FTH] = Wrapper_CNTR_1_137_Fetch;
	DevDat[31].d_acts[A_INX] = Wrapper_CNTR_1_137_Init;
	DevDat[31].d_acts[A_LOD] = Wrapper_CNTR_1_137_Load;
	DevDat[31].d_acts[A_OPN] = Wrapper_CNTR_1_137_Open;
	DevDat[31].d_acts[A_RST] = Wrapper_CNTR_1_137_Reset;
	DevDat[31].d_acts[A_FNC] = Wrapper_CNTR_1_137_Setup;
	DevDat[31].d_acts[A_STA] = Wrapper_CNTR_1_137_Status;
//
//	CNTR_1:CH138
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[32].d_modlst = p_mod;
	DevDat[32].d_fncP = 138;
	DevDat[32].d_acts[A_CLS] = Wrapper_CNTR_1_138_Close;
	DevDat[32].d_acts[A_CON] = Wrapper_CNTR_1_138_Connect;
	DevDat[32].d_acts[A_DIS] = Wrapper_CNTR_1_138_Disconnect;
	DevDat[32].d_acts[A_FTH] = Wrapper_CNTR_1_138_Fetch;
	DevDat[32].d_acts[A_INX] = Wrapper_CNTR_1_138_Init;
	DevDat[32].d_acts[A_LOD] = Wrapper_CNTR_1_138_Load;
	DevDat[32].d_acts[A_OPN] = Wrapper_CNTR_1_138_Open;
	DevDat[32].d_acts[A_RST] = Wrapper_CNTR_1_138_Reset;
	DevDat[32].d_acts[A_FNC] = Wrapper_CNTR_1_138_Setup;
	DevDat[32].d_acts[A_STA] = Wrapper_CNTR_1_138_Status;
//
//	CNTR_1:CH139
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[33].d_modlst = p_mod;
	DevDat[33].d_fncP = 139;
	DevDat[33].d_acts[A_CLS] = Wrapper_CNTR_1_139_Close;
	DevDat[33].d_acts[A_CON] = Wrapper_CNTR_1_139_Connect;
	DevDat[33].d_acts[A_DIS] = Wrapper_CNTR_1_139_Disconnect;
	DevDat[33].d_acts[A_FTH] = Wrapper_CNTR_1_139_Fetch;
	DevDat[33].d_acts[A_INX] = Wrapper_CNTR_1_139_Init;
	DevDat[33].d_acts[A_LOD] = Wrapper_CNTR_1_139_Load;
	DevDat[33].d_acts[A_OPN] = Wrapper_CNTR_1_139_Open;
	DevDat[33].d_acts[A_RST] = Wrapper_CNTR_1_139_Reset;
	DevDat[33].d_acts[A_FNC] = Wrapper_CNTR_1_139_Setup;
	DevDat[33].d_acts[A_STA] = Wrapper_CNTR_1_139_Status;
//
//	CNTR_1:CH14
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[34].d_modlst = p_mod;
	DevDat[34].d_fncP = 14;
	DevDat[34].d_acts[A_CLS] = Wrapper_CNTR_1_14_Close;
	DevDat[34].d_acts[A_CON] = Wrapper_CNTR_1_14_Connect;
	DevDat[34].d_acts[A_DIS] = Wrapper_CNTR_1_14_Disconnect;
	DevDat[34].d_acts[A_FTH] = Wrapper_CNTR_1_14_Fetch;
	DevDat[34].d_acts[A_INX] = Wrapper_CNTR_1_14_Init;
	DevDat[34].d_acts[A_LOD] = Wrapper_CNTR_1_14_Load;
	DevDat[34].d_acts[A_OPN] = Wrapper_CNTR_1_14_Open;
	DevDat[34].d_acts[A_RST] = Wrapper_CNTR_1_14_Reset;
	DevDat[34].d_acts[A_FNC] = Wrapper_CNTR_1_14_Setup;
	DevDat[34].d_acts[A_STA] = Wrapper_CNTR_1_14_Status;
//
//	CNTR_1:CH140
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[35].d_modlst = p_mod;
	DevDat[35].d_fncP = 140;
	DevDat[35].d_acts[A_CLS] = Wrapper_CNTR_1_140_Close;
	DevDat[35].d_acts[A_CON] = Wrapper_CNTR_1_140_Connect;
	DevDat[35].d_acts[A_DIS] = Wrapper_CNTR_1_140_Disconnect;
	DevDat[35].d_acts[A_FTH] = Wrapper_CNTR_1_140_Fetch;
	DevDat[35].d_acts[A_INX] = Wrapper_CNTR_1_140_Init;
	DevDat[35].d_acts[A_LOD] = Wrapper_CNTR_1_140_Load;
	DevDat[35].d_acts[A_OPN] = Wrapper_CNTR_1_140_Open;
	DevDat[35].d_acts[A_RST] = Wrapper_CNTR_1_140_Reset;
	DevDat[35].d_acts[A_FNC] = Wrapper_CNTR_1_140_Setup;
	DevDat[35].d_acts[A_STA] = Wrapper_CNTR_1_140_Status;
//
//	CNTR_1:CH141
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[36].d_modlst = p_mod;
	DevDat[36].d_fncP = 141;
	DevDat[36].d_acts[A_CLS] = Wrapper_CNTR_1_141_Close;
	DevDat[36].d_acts[A_CON] = Wrapper_CNTR_1_141_Connect;
	DevDat[36].d_acts[A_DIS] = Wrapper_CNTR_1_141_Disconnect;
	DevDat[36].d_acts[A_FTH] = Wrapper_CNTR_1_141_Fetch;
	DevDat[36].d_acts[A_INX] = Wrapper_CNTR_1_141_Init;
	DevDat[36].d_acts[A_LOD] = Wrapper_CNTR_1_141_Load;
	DevDat[36].d_acts[A_OPN] = Wrapper_CNTR_1_141_Open;
	DevDat[36].d_acts[A_RST] = Wrapper_CNTR_1_141_Reset;
	DevDat[36].d_acts[A_FNC] = Wrapper_CNTR_1_141_Setup;
	DevDat[36].d_acts[A_STA] = Wrapper_CNTR_1_141_Status;
//
//	CNTR_1:CH142
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[37].d_modlst = p_mod;
	DevDat[37].d_fncP = 142;
	DevDat[37].d_acts[A_CLS] = Wrapper_CNTR_1_142_Close;
	DevDat[37].d_acts[A_CON] = Wrapper_CNTR_1_142_Connect;
	DevDat[37].d_acts[A_DIS] = Wrapper_CNTR_1_142_Disconnect;
	DevDat[37].d_acts[A_FTH] = Wrapper_CNTR_1_142_Fetch;
	DevDat[37].d_acts[A_INX] = Wrapper_CNTR_1_142_Init;
	DevDat[37].d_acts[A_LOD] = Wrapper_CNTR_1_142_Load;
	DevDat[37].d_acts[A_OPN] = Wrapper_CNTR_1_142_Open;
	DevDat[37].d_acts[A_RST] = Wrapper_CNTR_1_142_Reset;
	DevDat[37].d_acts[A_FNC] = Wrapper_CNTR_1_142_Setup;
	DevDat[37].d_acts[A_STA] = Wrapper_CNTR_1_142_Status;
//
//	CNTR_1:CH143
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[38].d_modlst = p_mod;
	DevDat[38].d_fncP = 143;
	DevDat[38].d_acts[A_CLS] = Wrapper_CNTR_1_143_Close;
	DevDat[38].d_acts[A_CON] = Wrapper_CNTR_1_143_Connect;
	DevDat[38].d_acts[A_DIS] = Wrapper_CNTR_1_143_Disconnect;
	DevDat[38].d_acts[A_FTH] = Wrapper_CNTR_1_143_Fetch;
	DevDat[38].d_acts[A_INX] = Wrapper_CNTR_1_143_Init;
	DevDat[38].d_acts[A_LOD] = Wrapper_CNTR_1_143_Load;
	DevDat[38].d_acts[A_OPN] = Wrapper_CNTR_1_143_Open;
	DevDat[38].d_acts[A_RST] = Wrapper_CNTR_1_143_Reset;
	DevDat[38].d_acts[A_FNC] = Wrapper_CNTR_1_143_Setup;
	DevDat[38].d_acts[A_STA] = Wrapper_CNTR_1_143_Status;
//
//	CNTR_1:CH144
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[39].d_modlst = p_mod;
	DevDat[39].d_fncP = 144;
	DevDat[39].d_acts[A_CLS] = Wrapper_CNTR_1_144_Close;
	DevDat[39].d_acts[A_CON] = Wrapper_CNTR_1_144_Connect;
	DevDat[39].d_acts[A_DIS] = Wrapper_CNTR_1_144_Disconnect;
	DevDat[39].d_acts[A_FTH] = Wrapper_CNTR_1_144_Fetch;
	DevDat[39].d_acts[A_INX] = Wrapper_CNTR_1_144_Init;
	DevDat[39].d_acts[A_LOD] = Wrapper_CNTR_1_144_Load;
	DevDat[39].d_acts[A_OPN] = Wrapper_CNTR_1_144_Open;
	DevDat[39].d_acts[A_RST] = Wrapper_CNTR_1_144_Reset;
	DevDat[39].d_acts[A_FNC] = Wrapper_CNTR_1_144_Setup;
	DevDat[39].d_acts[A_STA] = Wrapper_CNTR_1_144_Status;
//
//	CNTR_1:CH145
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[40].d_modlst = p_mod;
	DevDat[40].d_fncP = 145;
	DevDat[40].d_acts[A_CLS] = Wrapper_CNTR_1_145_Close;
	DevDat[40].d_acts[A_CON] = Wrapper_CNTR_1_145_Connect;
	DevDat[40].d_acts[A_DIS] = Wrapper_CNTR_1_145_Disconnect;
	DevDat[40].d_acts[A_FTH] = Wrapper_CNTR_1_145_Fetch;
	DevDat[40].d_acts[A_INX] = Wrapper_CNTR_1_145_Init;
	DevDat[40].d_acts[A_LOD] = Wrapper_CNTR_1_145_Load;
	DevDat[40].d_acts[A_OPN] = Wrapper_CNTR_1_145_Open;
	DevDat[40].d_acts[A_RST] = Wrapper_CNTR_1_145_Reset;
	DevDat[40].d_acts[A_FNC] = Wrapper_CNTR_1_145_Setup;
	DevDat[40].d_acts[A_STA] = Wrapper_CNTR_1_145_Status;
//
//	CNTR_1:CH146
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[41].d_modlst = p_mod;
	DevDat[41].d_fncP = 146;
	DevDat[41].d_acts[A_CLS] = Wrapper_CNTR_1_146_Close;
	DevDat[41].d_acts[A_CON] = Wrapper_CNTR_1_146_Connect;
	DevDat[41].d_acts[A_DIS] = Wrapper_CNTR_1_146_Disconnect;
	DevDat[41].d_acts[A_FTH] = Wrapper_CNTR_1_146_Fetch;
	DevDat[41].d_acts[A_INX] = Wrapper_CNTR_1_146_Init;
	DevDat[41].d_acts[A_LOD] = Wrapper_CNTR_1_146_Load;
	DevDat[41].d_acts[A_OPN] = Wrapper_CNTR_1_146_Open;
	DevDat[41].d_acts[A_RST] = Wrapper_CNTR_1_146_Reset;
	DevDat[41].d_acts[A_FNC] = Wrapper_CNTR_1_146_Setup;
	DevDat[41].d_acts[A_STA] = Wrapper_CNTR_1_146_Status;
//
//	CNTR_1:CH147
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[42].d_modlst = p_mod;
	DevDat[42].d_fncP = 147;
	DevDat[42].d_acts[A_CLS] = Wrapper_CNTR_1_147_Close;
	DevDat[42].d_acts[A_CON] = Wrapper_CNTR_1_147_Connect;
	DevDat[42].d_acts[A_DIS] = Wrapper_CNTR_1_147_Disconnect;
	DevDat[42].d_acts[A_FTH] = Wrapper_CNTR_1_147_Fetch;
	DevDat[42].d_acts[A_INX] = Wrapper_CNTR_1_147_Init;
	DevDat[42].d_acts[A_LOD] = Wrapper_CNTR_1_147_Load;
	DevDat[42].d_acts[A_OPN] = Wrapper_CNTR_1_147_Open;
	DevDat[42].d_acts[A_RST] = Wrapper_CNTR_1_147_Reset;
	DevDat[42].d_acts[A_FNC] = Wrapper_CNTR_1_147_Setup;
	DevDat[42].d_acts[A_STA] = Wrapper_CNTR_1_147_Status;
//
//	CNTR_1:CH148
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[43].d_modlst = p_mod;
	DevDat[43].d_fncP = 148;
	DevDat[43].d_acts[A_CLS] = Wrapper_CNTR_1_148_Close;
	DevDat[43].d_acts[A_CON] = Wrapper_CNTR_1_148_Connect;
	DevDat[43].d_acts[A_DIS] = Wrapper_CNTR_1_148_Disconnect;
	DevDat[43].d_acts[A_FTH] = Wrapper_CNTR_1_148_Fetch;
	DevDat[43].d_acts[A_INX] = Wrapper_CNTR_1_148_Init;
	DevDat[43].d_acts[A_LOD] = Wrapper_CNTR_1_148_Load;
	DevDat[43].d_acts[A_OPN] = Wrapper_CNTR_1_148_Open;
	DevDat[43].d_acts[A_RST] = Wrapper_CNTR_1_148_Reset;
	DevDat[43].d_acts[A_FNC] = Wrapper_CNTR_1_148_Setup;
	DevDat[43].d_acts[A_STA] = Wrapper_CNTR_1_148_Status;
//
//	CNTR_1:CH15
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQR);  // freq-ratio
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PANG);  // phase-angle
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[44].d_modlst = p_mod;
	DevDat[44].d_fncP = 15;
	DevDat[44].d_acts[A_CLS] = Wrapper_CNTR_1_15_Close;
	DevDat[44].d_acts[A_CON] = Wrapper_CNTR_1_15_Connect;
	DevDat[44].d_acts[A_DIS] = Wrapper_CNTR_1_15_Disconnect;
	DevDat[44].d_acts[A_FTH] = Wrapper_CNTR_1_15_Fetch;
	DevDat[44].d_acts[A_INX] = Wrapper_CNTR_1_15_Init;
	DevDat[44].d_acts[A_LOD] = Wrapper_CNTR_1_15_Load;
	DevDat[44].d_acts[A_OPN] = Wrapper_CNTR_1_15_Open;
	DevDat[44].d_acts[A_RST] = Wrapper_CNTR_1_15_Reset;
	DevDat[44].d_acts[A_FNC] = Wrapper_CNTR_1_15_Setup;
	DevDat[44].d_acts[A_STA] = Wrapper_CNTR_1_15_Status;
//
//	CNTR_1:CH150
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[45].d_modlst = p_mod;
	DevDat[45].d_fncP = 150;
	DevDat[45].d_acts[A_CLS] = Wrapper_CNTR_1_150_Close;
	DevDat[45].d_acts[A_CON] = Wrapper_CNTR_1_150_Connect;
	DevDat[45].d_acts[A_DIS] = Wrapper_CNTR_1_150_Disconnect;
	DevDat[45].d_acts[A_FTH] = Wrapper_CNTR_1_150_Fetch;
	DevDat[45].d_acts[A_INX] = Wrapper_CNTR_1_150_Init;
	DevDat[45].d_acts[A_LOD] = Wrapper_CNTR_1_150_Load;
	DevDat[45].d_acts[A_OPN] = Wrapper_CNTR_1_150_Open;
	DevDat[45].d_acts[A_RST] = Wrapper_CNTR_1_150_Reset;
	DevDat[45].d_acts[A_FNC] = Wrapper_CNTR_1_150_Setup;
	DevDat[45].d_acts[A_STA] = Wrapper_CNTR_1_150_Status;
//
//	CNTR_1:CH151
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[46].d_modlst = p_mod;
	DevDat[46].d_fncP = 151;
	DevDat[46].d_acts[A_CLS] = Wrapper_CNTR_1_151_Close;
	DevDat[46].d_acts[A_CON] = Wrapper_CNTR_1_151_Connect;
	DevDat[46].d_acts[A_DIS] = Wrapper_CNTR_1_151_Disconnect;
	DevDat[46].d_acts[A_FTH] = Wrapper_CNTR_1_151_Fetch;
	DevDat[46].d_acts[A_INX] = Wrapper_CNTR_1_151_Init;
	DevDat[46].d_acts[A_LOD] = Wrapper_CNTR_1_151_Load;
	DevDat[46].d_acts[A_OPN] = Wrapper_CNTR_1_151_Open;
	DevDat[46].d_acts[A_RST] = Wrapper_CNTR_1_151_Reset;
	DevDat[46].d_acts[A_FNC] = Wrapper_CNTR_1_151_Setup;
	DevDat[46].d_acts[A_STA] = Wrapper_CNTR_1_151_Status;
//
//	CNTR_1:CH152
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[47].d_modlst = p_mod;
	DevDat[47].d_fncP = 152;
	DevDat[47].d_acts[A_CLS] = Wrapper_CNTR_1_152_Close;
	DevDat[47].d_acts[A_CON] = Wrapper_CNTR_1_152_Connect;
	DevDat[47].d_acts[A_DIS] = Wrapper_CNTR_1_152_Disconnect;
	DevDat[47].d_acts[A_FTH] = Wrapper_CNTR_1_152_Fetch;
	DevDat[47].d_acts[A_INX] = Wrapper_CNTR_1_152_Init;
	DevDat[47].d_acts[A_LOD] = Wrapper_CNTR_1_152_Load;
	DevDat[47].d_acts[A_OPN] = Wrapper_CNTR_1_152_Open;
	DevDat[47].d_acts[A_RST] = Wrapper_CNTR_1_152_Reset;
	DevDat[47].d_acts[A_FNC] = Wrapper_CNTR_1_152_Setup;
	DevDat[47].d_acts[A_STA] = Wrapper_CNTR_1_152_Status;
//
//	CNTR_1:CH153
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[48].d_modlst = p_mod;
	DevDat[48].d_fncP = 153;
	DevDat[48].d_acts[A_CLS] = Wrapper_CNTR_1_153_Close;
	DevDat[48].d_acts[A_CON] = Wrapper_CNTR_1_153_Connect;
	DevDat[48].d_acts[A_DIS] = Wrapper_CNTR_1_153_Disconnect;
	DevDat[48].d_acts[A_FTH] = Wrapper_CNTR_1_153_Fetch;
	DevDat[48].d_acts[A_INX] = Wrapper_CNTR_1_153_Init;
	DevDat[48].d_acts[A_LOD] = Wrapper_CNTR_1_153_Load;
	DevDat[48].d_acts[A_OPN] = Wrapper_CNTR_1_153_Open;
	DevDat[48].d_acts[A_RST] = Wrapper_CNTR_1_153_Reset;
	DevDat[48].d_acts[A_FNC] = Wrapper_CNTR_1_153_Setup;
	DevDat[48].d_acts[A_STA] = Wrapper_CNTR_1_153_Status;
//
//	CNTR_1:CH16
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[49].d_modlst = p_mod;
	DevDat[49].d_fncP = 16;
	DevDat[49].d_acts[A_CLS] = Wrapper_CNTR_1_16_Close;
	DevDat[49].d_acts[A_CON] = Wrapper_CNTR_1_16_Connect;
	DevDat[49].d_acts[A_DIS] = Wrapper_CNTR_1_16_Disconnect;
	DevDat[49].d_acts[A_FTH] = Wrapper_CNTR_1_16_Fetch;
	DevDat[49].d_acts[A_INX] = Wrapper_CNTR_1_16_Init;
	DevDat[49].d_acts[A_LOD] = Wrapper_CNTR_1_16_Load;
	DevDat[49].d_acts[A_OPN] = Wrapper_CNTR_1_16_Open;
	DevDat[49].d_acts[A_RST] = Wrapper_CNTR_1_16_Reset;
	DevDat[49].d_acts[A_FNC] = Wrapper_CNTR_1_16_Setup;
	DevDat[49].d_acts[A_STA] = Wrapper_CNTR_1_16_Status;
//
//	CNTR_1:CH17
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[50].d_modlst = p_mod;
	DevDat[50].d_fncP = 17;
	DevDat[50].d_acts[A_CLS] = Wrapper_CNTR_1_17_Close;
	DevDat[50].d_acts[A_CON] = Wrapper_CNTR_1_17_Connect;
	DevDat[50].d_acts[A_DIS] = Wrapper_CNTR_1_17_Disconnect;
	DevDat[50].d_acts[A_FTH] = Wrapper_CNTR_1_17_Fetch;
	DevDat[50].d_acts[A_INX] = Wrapper_CNTR_1_17_Init;
	DevDat[50].d_acts[A_LOD] = Wrapper_CNTR_1_17_Load;
	DevDat[50].d_acts[A_OPN] = Wrapper_CNTR_1_17_Open;
	DevDat[50].d_acts[A_RST] = Wrapper_CNTR_1_17_Reset;
	DevDat[50].d_acts[A_FNC] = Wrapper_CNTR_1_17_Setup;
	DevDat[50].d_acts[A_STA] = Wrapper_CNTR_1_17_Status;
//
//	CNTR_1:CH18
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[51].d_modlst = p_mod;
	DevDat[51].d_fncP = 18;
	DevDat[51].d_acts[A_CLS] = Wrapper_CNTR_1_18_Close;
	DevDat[51].d_acts[A_CON] = Wrapper_CNTR_1_18_Connect;
	DevDat[51].d_acts[A_DIS] = Wrapper_CNTR_1_18_Disconnect;
	DevDat[51].d_acts[A_FTH] = Wrapper_CNTR_1_18_Fetch;
	DevDat[51].d_acts[A_INX] = Wrapper_CNTR_1_18_Init;
	DevDat[51].d_acts[A_LOD] = Wrapper_CNTR_1_18_Load;
	DevDat[51].d_acts[A_OPN] = Wrapper_CNTR_1_18_Open;
	DevDat[51].d_acts[A_RST] = Wrapper_CNTR_1_18_Reset;
	DevDat[51].d_acts[A_FNC] = Wrapper_CNTR_1_18_Setup;
	DevDat[51].d_acts[A_STA] = Wrapper_CNTR_1_18_Status;
//
//	CNTR_1:CH19
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[52].d_modlst = p_mod;
	DevDat[52].d_fncP = 19;
	DevDat[52].d_acts[A_CLS] = Wrapper_CNTR_1_19_Close;
	DevDat[52].d_acts[A_CON] = Wrapper_CNTR_1_19_Connect;
	DevDat[52].d_acts[A_DIS] = Wrapper_CNTR_1_19_Disconnect;
	DevDat[52].d_acts[A_FTH] = Wrapper_CNTR_1_19_Fetch;
	DevDat[52].d_acts[A_INX] = Wrapper_CNTR_1_19_Init;
	DevDat[52].d_acts[A_LOD] = Wrapper_CNTR_1_19_Load;
	DevDat[52].d_acts[A_OPN] = Wrapper_CNTR_1_19_Open;
	DevDat[52].d_acts[A_RST] = Wrapper_CNTR_1_19_Reset;
	DevDat[52].d_acts[A_FNC] = Wrapper_CNTR_1_19_Setup;
	DevDat[52].d_acts[A_STA] = Wrapper_CNTR_1_19_Status;
//
//	CNTR_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQR);  // freq-ratio
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PANG);  // phase-angle
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[53].d_modlst = p_mod;
	DevDat[53].d_fncP = 2;
	DevDat[53].d_acts[A_CLS] = Wrapper_CNTR_1_2_Close;
	DevDat[53].d_acts[A_CON] = Wrapper_CNTR_1_2_Connect;
	DevDat[53].d_acts[A_DIS] = Wrapper_CNTR_1_2_Disconnect;
	DevDat[53].d_acts[A_FTH] = Wrapper_CNTR_1_2_Fetch;
	DevDat[53].d_acts[A_INX] = Wrapper_CNTR_1_2_Init;
	DevDat[53].d_acts[A_LOD] = Wrapper_CNTR_1_2_Load;
	DevDat[53].d_acts[A_OPN] = Wrapper_CNTR_1_2_Open;
	DevDat[53].d_acts[A_RST] = Wrapper_CNTR_1_2_Reset;
	DevDat[53].d_acts[A_FNC] = Wrapper_CNTR_1_2_Setup;
	DevDat[53].d_acts[A_STA] = Wrapper_CNTR_1_2_Status;
//
//	CNTR_1:CH20
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[54].d_modlst = p_mod;
	DevDat[54].d_fncP = 20;
	DevDat[54].d_acts[A_CLS] = Wrapper_CNTR_1_20_Close;
	DevDat[54].d_acts[A_CON] = Wrapper_CNTR_1_20_Connect;
	DevDat[54].d_acts[A_DIS] = Wrapper_CNTR_1_20_Disconnect;
	DevDat[54].d_acts[A_FTH] = Wrapper_CNTR_1_20_Fetch;
	DevDat[54].d_acts[A_INX] = Wrapper_CNTR_1_20_Init;
	DevDat[54].d_acts[A_LOD] = Wrapper_CNTR_1_20_Load;
	DevDat[54].d_acts[A_OPN] = Wrapper_CNTR_1_20_Open;
	DevDat[54].d_acts[A_RST] = Wrapper_CNTR_1_20_Reset;
	DevDat[54].d_acts[A_FNC] = Wrapper_CNTR_1_20_Setup;
	DevDat[54].d_acts[A_STA] = Wrapper_CNTR_1_20_Status;
//
//	CNTR_1:CH21
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[55].d_modlst = p_mod;
	DevDat[55].d_fncP = 21;
	DevDat[55].d_acts[A_CLS] = Wrapper_CNTR_1_21_Close;
	DevDat[55].d_acts[A_CON] = Wrapper_CNTR_1_21_Connect;
	DevDat[55].d_acts[A_DIS] = Wrapper_CNTR_1_21_Disconnect;
	DevDat[55].d_acts[A_FTH] = Wrapper_CNTR_1_21_Fetch;
	DevDat[55].d_acts[A_INX] = Wrapper_CNTR_1_21_Init;
	DevDat[55].d_acts[A_LOD] = Wrapper_CNTR_1_21_Load;
	DevDat[55].d_acts[A_OPN] = Wrapper_CNTR_1_21_Open;
	DevDat[55].d_acts[A_RST] = Wrapper_CNTR_1_21_Reset;
	DevDat[55].d_acts[A_FNC] = Wrapper_CNTR_1_21_Setup;
	DevDat[55].d_acts[A_STA] = Wrapper_CNTR_1_21_Status;
//
//	CNTR_1:CH213
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[56].d_modlst = p_mod;
	DevDat[56].d_fncP = 213;
	DevDat[56].d_acts[A_CLS] = Wrapper_CNTR_1_213_Close;
	DevDat[56].d_acts[A_CON] = Wrapper_CNTR_1_213_Connect;
	DevDat[56].d_acts[A_DIS] = Wrapper_CNTR_1_213_Disconnect;
	DevDat[56].d_acts[A_FTH] = Wrapper_CNTR_1_213_Fetch;
	DevDat[56].d_acts[A_INX] = Wrapper_CNTR_1_213_Init;
	DevDat[56].d_acts[A_LOD] = Wrapper_CNTR_1_213_Load;
	DevDat[56].d_acts[A_OPN] = Wrapper_CNTR_1_213_Open;
	DevDat[56].d_acts[A_RST] = Wrapper_CNTR_1_213_Reset;
	DevDat[56].d_acts[A_FNC] = Wrapper_CNTR_1_213_Setup;
	DevDat[56].d_acts[A_STA] = Wrapper_CNTR_1_213_Status;
//
//	CNTR_1:CH214
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_SETT);  // settle-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[57].d_modlst = p_mod;
	DevDat[57].d_fncP = 214;
	DevDat[57].d_acts[A_CLS] = Wrapper_CNTR_1_214_Close;
	DevDat[57].d_acts[A_CON] = Wrapper_CNTR_1_214_Connect;
	DevDat[57].d_acts[A_DIS] = Wrapper_CNTR_1_214_Disconnect;
	DevDat[57].d_acts[A_FTH] = Wrapper_CNTR_1_214_Fetch;
	DevDat[57].d_acts[A_INX] = Wrapper_CNTR_1_214_Init;
	DevDat[57].d_acts[A_LOD] = Wrapper_CNTR_1_214_Load;
	DevDat[57].d_acts[A_OPN] = Wrapper_CNTR_1_214_Open;
	DevDat[57].d_acts[A_RST] = Wrapper_CNTR_1_214_Reset;
	DevDat[57].d_acts[A_FNC] = Wrapper_CNTR_1_214_Setup;
	DevDat[57].d_acts[A_STA] = Wrapper_CNTR_1_214_Status;
//
//	CNTR_1:CH22
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[58].d_modlst = p_mod;
	DevDat[58].d_fncP = 22;
	DevDat[58].d_acts[A_CLS] = Wrapper_CNTR_1_22_Close;
	DevDat[58].d_acts[A_CON] = Wrapper_CNTR_1_22_Connect;
	DevDat[58].d_acts[A_DIS] = Wrapper_CNTR_1_22_Disconnect;
	DevDat[58].d_acts[A_FTH] = Wrapper_CNTR_1_22_Fetch;
	DevDat[58].d_acts[A_INX] = Wrapper_CNTR_1_22_Init;
	DevDat[58].d_acts[A_LOD] = Wrapper_CNTR_1_22_Load;
	DevDat[58].d_acts[A_OPN] = Wrapper_CNTR_1_22_Open;
	DevDat[58].d_acts[A_RST] = Wrapper_CNTR_1_22_Reset;
	DevDat[58].d_acts[A_FNC] = Wrapper_CNTR_1_22_Setup;
	DevDat[58].d_acts[A_STA] = Wrapper_CNTR_1_22_Status;
//
//	CNTR_1:CH23
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[59].d_modlst = p_mod;
	DevDat[59].d_fncP = 23;
	DevDat[59].d_acts[A_CLS] = Wrapper_CNTR_1_23_Close;
	DevDat[59].d_acts[A_CON] = Wrapper_CNTR_1_23_Connect;
	DevDat[59].d_acts[A_DIS] = Wrapper_CNTR_1_23_Disconnect;
	DevDat[59].d_acts[A_FTH] = Wrapper_CNTR_1_23_Fetch;
	DevDat[59].d_acts[A_INX] = Wrapper_CNTR_1_23_Init;
	DevDat[59].d_acts[A_LOD] = Wrapper_CNTR_1_23_Load;
	DevDat[59].d_acts[A_OPN] = Wrapper_CNTR_1_23_Open;
	DevDat[59].d_acts[A_RST] = Wrapper_CNTR_1_23_Reset;
	DevDat[59].d_acts[A_FNC] = Wrapper_CNTR_1_23_Setup;
	DevDat[59].d_acts[A_STA] = Wrapper_CNTR_1_23_Status;
//
//	CNTR_1:CH24
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[60].d_modlst = p_mod;
	DevDat[60].d_fncP = 24;
	DevDat[60].d_acts[A_CLS] = Wrapper_CNTR_1_24_Close;
	DevDat[60].d_acts[A_CON] = Wrapper_CNTR_1_24_Connect;
	DevDat[60].d_acts[A_DIS] = Wrapper_CNTR_1_24_Disconnect;
	DevDat[60].d_acts[A_FTH] = Wrapper_CNTR_1_24_Fetch;
	DevDat[60].d_acts[A_INX] = Wrapper_CNTR_1_24_Init;
	DevDat[60].d_acts[A_LOD] = Wrapper_CNTR_1_24_Load;
	DevDat[60].d_acts[A_OPN] = Wrapper_CNTR_1_24_Open;
	DevDat[60].d_acts[A_RST] = Wrapper_CNTR_1_24_Reset;
	DevDat[60].d_acts[A_FNC] = Wrapper_CNTR_1_24_Setup;
	DevDat[60].d_acts[A_STA] = Wrapper_CNTR_1_24_Status;
//
//	CNTR_1:CH25
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[61].d_modlst = p_mod;
	DevDat[61].d_fncP = 25;
	DevDat[61].d_acts[A_CLS] = Wrapper_CNTR_1_25_Close;
	DevDat[61].d_acts[A_CON] = Wrapper_CNTR_1_25_Connect;
	DevDat[61].d_acts[A_DIS] = Wrapper_CNTR_1_25_Disconnect;
	DevDat[61].d_acts[A_FTH] = Wrapper_CNTR_1_25_Fetch;
	DevDat[61].d_acts[A_INX] = Wrapper_CNTR_1_25_Init;
	DevDat[61].d_acts[A_LOD] = Wrapper_CNTR_1_25_Load;
	DevDat[61].d_acts[A_OPN] = Wrapper_CNTR_1_25_Open;
	DevDat[61].d_acts[A_RST] = Wrapper_CNTR_1_25_Reset;
	DevDat[61].d_acts[A_FNC] = Wrapper_CNTR_1_25_Setup;
	DevDat[61].d_acts[A_STA] = Wrapper_CNTR_1_25_Status;
//
//	CNTR_1:CH26
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[62].d_modlst = p_mod;
	DevDat[62].d_fncP = 26;
	DevDat[62].d_acts[A_CLS] = Wrapper_CNTR_1_26_Close;
	DevDat[62].d_acts[A_CON] = Wrapper_CNTR_1_26_Connect;
	DevDat[62].d_acts[A_DIS] = Wrapper_CNTR_1_26_Disconnect;
	DevDat[62].d_acts[A_FTH] = Wrapper_CNTR_1_26_Fetch;
	DevDat[62].d_acts[A_INX] = Wrapper_CNTR_1_26_Init;
	DevDat[62].d_acts[A_LOD] = Wrapper_CNTR_1_26_Load;
	DevDat[62].d_acts[A_OPN] = Wrapper_CNTR_1_26_Open;
	DevDat[62].d_acts[A_RST] = Wrapper_CNTR_1_26_Reset;
	DevDat[62].d_acts[A_FNC] = Wrapper_CNTR_1_26_Setup;
	DevDat[62].d_acts[A_STA] = Wrapper_CNTR_1_26_Status;
//
//	CNTR_1:CH27
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[63].d_modlst = p_mod;
	DevDat[63].d_fncP = 27;
	DevDat[63].d_acts[A_CLS] = Wrapper_CNTR_1_27_Close;
	DevDat[63].d_acts[A_CON] = Wrapper_CNTR_1_27_Connect;
	DevDat[63].d_acts[A_DIS] = Wrapper_CNTR_1_27_Disconnect;
	DevDat[63].d_acts[A_FTH] = Wrapper_CNTR_1_27_Fetch;
	DevDat[63].d_acts[A_INX] = Wrapper_CNTR_1_27_Init;
	DevDat[63].d_acts[A_LOD] = Wrapper_CNTR_1_27_Load;
	DevDat[63].d_acts[A_OPN] = Wrapper_CNTR_1_27_Open;
	DevDat[63].d_acts[A_RST] = Wrapper_CNTR_1_27_Reset;
	DevDat[63].d_acts[A_FNC] = Wrapper_CNTR_1_27_Setup;
	DevDat[63].d_acts[A_STA] = Wrapper_CNTR_1_27_Status;
//
//	CNTR_1:CH3
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQR);  // freq-ratio
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PANG);  // phase-angle
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[64].d_modlst = p_mod;
	DevDat[64].d_fncP = 3;
	DevDat[64].d_acts[A_CLS] = Wrapper_CNTR_1_3_Close;
	DevDat[64].d_acts[A_CON] = Wrapper_CNTR_1_3_Connect;
	DevDat[64].d_acts[A_DIS] = Wrapper_CNTR_1_3_Disconnect;
	DevDat[64].d_acts[A_FTH] = Wrapper_CNTR_1_3_Fetch;
	DevDat[64].d_acts[A_INX] = Wrapper_CNTR_1_3_Init;
	DevDat[64].d_acts[A_LOD] = Wrapper_CNTR_1_3_Load;
	DevDat[64].d_acts[A_OPN] = Wrapper_CNTR_1_3_Open;
	DevDat[64].d_acts[A_RST] = Wrapper_CNTR_1_3_Reset;
	DevDat[64].d_acts[A_FNC] = Wrapper_CNTR_1_3_Setup;
	DevDat[64].d_acts[A_STA] = Wrapper_CNTR_1_3_Status;
//
//	CNTR_1:CH4
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[65].d_modlst = p_mod;
	DevDat[65].d_fncP = 4;
	DevDat[65].d_acts[A_CLS] = Wrapper_CNTR_1_4_Close;
	DevDat[65].d_acts[A_CON] = Wrapper_CNTR_1_4_Connect;
	DevDat[65].d_acts[A_DIS] = Wrapper_CNTR_1_4_Disconnect;
	DevDat[65].d_acts[A_FTH] = Wrapper_CNTR_1_4_Fetch;
	DevDat[65].d_acts[A_INX] = Wrapper_CNTR_1_4_Init;
	DevDat[65].d_acts[A_LOD] = Wrapper_CNTR_1_4_Load;
	DevDat[65].d_acts[A_OPN] = Wrapper_CNTR_1_4_Open;
	DevDat[65].d_acts[A_RST] = Wrapper_CNTR_1_4_Reset;
	DevDat[65].d_acts[A_FNC] = Wrapper_CNTR_1_4_Setup;
	DevDat[65].d_acts[A_STA] = Wrapper_CNTR_1_4_Status;
//
//	CNTR_1:CH5
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[66].d_modlst = p_mod;
	DevDat[66].d_fncP = 5;
	DevDat[66].d_acts[A_CLS] = Wrapper_CNTR_1_5_Close;
	DevDat[66].d_acts[A_CON] = Wrapper_CNTR_1_5_Connect;
	DevDat[66].d_acts[A_DIS] = Wrapper_CNTR_1_5_Disconnect;
	DevDat[66].d_acts[A_FTH] = Wrapper_CNTR_1_5_Fetch;
	DevDat[66].d_acts[A_INX] = Wrapper_CNTR_1_5_Init;
	DevDat[66].d_acts[A_LOD] = Wrapper_CNTR_1_5_Load;
	DevDat[66].d_acts[A_OPN] = Wrapper_CNTR_1_5_Open;
	DevDat[66].d_acts[A_RST] = Wrapper_CNTR_1_5_Reset;
	DevDat[66].d_acts[A_FNC] = Wrapper_CNTR_1_5_Setup;
	DevDat[66].d_acts[A_STA] = Wrapper_CNTR_1_5_Status;
//
//	CNTR_1:CH6
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[67].d_modlst = p_mod;
	DevDat[67].d_fncP = 6;
	DevDat[67].d_acts[A_CLS] = Wrapper_CNTR_1_6_Close;
	DevDat[67].d_acts[A_CON] = Wrapper_CNTR_1_6_Connect;
	DevDat[67].d_acts[A_DIS] = Wrapper_CNTR_1_6_Disconnect;
	DevDat[67].d_acts[A_FTH] = Wrapper_CNTR_1_6_Fetch;
	DevDat[67].d_acts[A_INX] = Wrapper_CNTR_1_6_Init;
	DevDat[67].d_acts[A_LOD] = Wrapper_CNTR_1_6_Load;
	DevDat[67].d_acts[A_OPN] = Wrapper_CNTR_1_6_Open;
	DevDat[67].d_acts[A_RST] = Wrapper_CNTR_1_6_Reset;
	DevDat[67].d_acts[A_FNC] = Wrapper_CNTR_1_6_Setup;
	DevDat[67].d_acts[A_STA] = Wrapper_CNTR_1_6_Status;
//
//	CNTR_1:CH7
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[68].d_modlst = p_mod;
	DevDat[68].d_fncP = 7;
	DevDat[68].d_acts[A_CLS] = Wrapper_CNTR_1_7_Close;
	DevDat[68].d_acts[A_CON] = Wrapper_CNTR_1_7_Connect;
	DevDat[68].d_acts[A_DIS] = Wrapper_CNTR_1_7_Disconnect;
	DevDat[68].d_acts[A_FTH] = Wrapper_CNTR_1_7_Fetch;
	DevDat[68].d_acts[A_INX] = Wrapper_CNTR_1_7_Init;
	DevDat[68].d_acts[A_LOD] = Wrapper_CNTR_1_7_Load;
	DevDat[68].d_acts[A_OPN] = Wrapper_CNTR_1_7_Open;
	DevDat[68].d_acts[A_RST] = Wrapper_CNTR_1_7_Reset;
	DevDat[68].d_acts[A_FNC] = Wrapper_CNTR_1_7_Setup;
	DevDat[68].d_acts[A_STA] = Wrapper_CNTR_1_7_Status;
//
//	CNTR_1:CH8
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[69].d_modlst = p_mod;
	DevDat[69].d_fncP = 8;
	DevDat[69].d_acts[A_CLS] = Wrapper_CNTR_1_8_Close;
	DevDat[69].d_acts[A_CON] = Wrapper_CNTR_1_8_Connect;
	DevDat[69].d_acts[A_DIS] = Wrapper_CNTR_1_8_Disconnect;
	DevDat[69].d_acts[A_FTH] = Wrapper_CNTR_1_8_Fetch;
	DevDat[69].d_acts[A_INX] = Wrapper_CNTR_1_8_Init;
	DevDat[69].d_acts[A_LOD] = Wrapper_CNTR_1_8_Load;
	DevDat[69].d_acts[A_OPN] = Wrapper_CNTR_1_8_Open;
	DevDat[69].d_acts[A_RST] = Wrapper_CNTR_1_8_Reset;
	DevDat[69].d_acts[A_FNC] = Wrapper_CNTR_1_8_Setup;
	DevDat[69].d_acts[A_STA] = Wrapper_CNTR_1_8_Status;
//
//	CNTR_1:CH9
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_NEGS);  // neg-slope
	p_mod = BldModDat (p_mod, (short) M_POSS);  // pos-slope
	p_mod = BldModDat (p_mod, (short) M_EVGF);  // event-gate-from
	p_mod = BldModDat (p_mod, (short) M_EVGT);  // event-gate-to
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[70].d_modlst = p_mod;
	DevDat[70].d_fncP = 9;
	DevDat[70].d_acts[A_CLS] = Wrapper_CNTR_1_9_Close;
	DevDat[70].d_acts[A_CON] = Wrapper_CNTR_1_9_Connect;
	DevDat[70].d_acts[A_DIS] = Wrapper_CNTR_1_9_Disconnect;
	DevDat[70].d_acts[A_FTH] = Wrapper_CNTR_1_9_Fetch;
	DevDat[70].d_acts[A_INX] = Wrapper_CNTR_1_9_Init;
	DevDat[70].d_acts[A_LOD] = Wrapper_CNTR_1_9_Load;
	DevDat[70].d_acts[A_OPN] = Wrapper_CNTR_1_9_Open;
	DevDat[70].d_acts[A_RST] = Wrapper_CNTR_1_9_Reset;
	DevDat[70].d_acts[A_FNC] = Wrapper_CNTR_1_9_Setup;
	DevDat[70].d_acts[A_STA] = Wrapper_CNTR_1_9_Status;
	return 0;
}
