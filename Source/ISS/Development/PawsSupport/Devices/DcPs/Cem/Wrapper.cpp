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
extern int doDCP_Close ();
extern int doDCP_Connect ();
extern int doDCP_Disconnect ();
extern int doDCP_Fetch ();
extern int doDCP_Init ();
extern int doDCP_Load ();
extern int doDCP_Open ();
extern int doDCP_Reset ();
extern int doDCP_Setup ();
extern int doDCP_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"DCP_1:CH1",
	"DCP_1:CH10",
	"DCP_1:CH100",
	"DCP_1:CH103",
	"DCP_1:CH104",
	"DCP_1:CH105",
	"DCP_1:CH106",
	"DCP_1:CH107",
	"DCP_1:CH108",
	"DCP_1:CH109",
	"DCP_1:CH110",
	"DCP_1:CH114",
	"DCP_1:CH115",
	"DCP_1:CH116",
	"DCP_1:CH117",
	"DCP_1:CH118",
	"DCP_1:CH119",
	"DCP_1:CH12",
	"DCP_1:CH120",
	"DCP_1:CH125",
	"DCP_1:CH126",
	"DCP_1:CH127",
	"DCP_1:CH128",
	"DCP_1:CH129",
	"DCP_1:CH13",
	"DCP_1:CH130",
	"DCP_1:CH136",
	"DCP_1:CH137",
	"DCP_1:CH138",
	"DCP_1:CH139",
	"DCP_1:CH14",
	"DCP_1:CH140",
	"DCP_1:CH147",
	"DCP_1:CH148",
	"DCP_1:CH149",
	"DCP_1:CH15",
	"DCP_1:CH150",
	"DCP_1:CH158",
	"DCP_1:CH159",
	"DCP_1:CH16",
	"DCP_1:CH160",
	"DCP_1:CH169",
	"DCP_1:CH17",
	"DCP_1:CH170",
	"DCP_1:CH18",
	"DCP_1:CH180",
	"DCP_1:CH181",
	"DCP_1:CH182",
	"DCP_1:CH183",
	"DCP_1:CH184",
	"DCP_1:CH185",
	"DCP_1:CH186",
	"DCP_1:CH187",
	"DCP_1:CH188",
	"DCP_1:CH189",
	"DCP_1:CH19",
	"DCP_1:CH190",
	"DCP_1:CH191",
	"DCP_1:CH192",
	"DCP_1:CH193",
	"DCP_1:CH194",
	"DCP_1:CH195",
	"DCP_1:CH196",
	"DCP_1:CH197",
	"DCP_1:CH198",
	"DCP_1:CH199",
	"DCP_1:CH2",
	"DCP_1:CH210",
	"DCP_1:CH211",
	"DCP_1:CH212",
	"DCP_1:CH213",
	"DCP_1:CH214",
	"DCP_1:CH215",
	"DCP_1:CH216",
	"DCP_1:CH217",
	"DCP_1:CH218",
	"DCP_1:CH219",
	"DCP_1:CH220",
	"DCP_1:CH221",
	"DCP_1:CH222",
	"DCP_1:CH223",
	"DCP_1:CH224",
	"DCP_1:CH225",
	"DCP_1:CH226",
	"DCP_1:CH227",
	"DCP_1:CH228",
	"DCP_1:CH229",
	"DCP_1:CH23",
	"DCP_1:CH24",
	"DCP_1:CH25",
	"DCP_1:CH26",
	"DCP_1:CH27",
	"DCP_1:CH28",
	"DCP_1:CH29",
	"DCP_1:CH3",
	"DCP_1:CH34",
	"DCP_1:CH35",
	"DCP_1:CH36",
	"DCP_1:CH37",
	"DCP_1:CH38",
	"DCP_1:CH39",
	"DCP_1:CH4",
	"DCP_1:CH45",
	"DCP_1:CH46",
	"DCP_1:CH47",
	"DCP_1:CH48",
	"DCP_1:CH49",
	"DCP_1:CH5",
	"DCP_1:CH56",
	"DCP_1:CH57",
	"DCP_1:CH58",
	"DCP_1:CH59",
	"DCP_1:CH6",
	"DCP_1:CH67",
	"DCP_1:CH68",
	"DCP_1:CH69",
	"DCP_1:CH7",
	"DCP_1:CH78",
	"DCP_1:CH79",
	"DCP_1:CH8",
	"DCP_1:CH89",
	"DCP_1:CH9",
	"DCP_1:CH92",
	"DCP_1:CH93",
	"DCP_1:CH94",
	"DCP_1:CH95",
	"DCP_1:CH96",
	"DCP_1:CH97",
	"DCP_1:CH98",
	"DCP_1:CH99",
};
DECLAREC int DevCnt = 132;
int CCALLBACK Wrapper_DCP_1_1_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_1_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_10_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_100_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_103_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_104_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_105_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_106_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_107_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_108_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_109_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_110_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_114_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_115_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_116_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_117_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_118_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_119_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_12_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_120_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_125_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_126_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_127_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_128_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_129_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_13_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_130_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_136_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_137_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_138_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_139_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_14_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_140_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_147_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_148_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_149_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_15_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_150_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_158_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_159_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_16_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_160_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_169_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_17_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_170_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_18_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_180_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_181_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_182_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_183_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_184_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_185_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_186_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_187_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_188_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_189_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_19_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_190_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_191_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_192_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_193_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_194_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_195_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_196_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_197_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_198_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_199_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_2_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_210_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_211_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_212_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_213_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_214_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_215_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_216_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_217_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_218_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_219_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_220_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_221_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_222_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_223_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_224_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_225_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_226_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_227_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_228_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_229_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_23_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_24_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_25_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_26_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_27_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_28_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_29_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_3_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_34_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_35_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_36_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_37_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_38_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_39_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_4_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_45_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_46_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_47_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_48_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_49_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_5_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_56_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_57_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_58_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_59_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_6_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_67_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_68_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_69_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_7_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_78_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_79_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_8_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_89_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_9_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_92_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_93_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_94_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_95_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_96_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_97_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_98_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Close(void)
{
	if (doDCP_Close() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Connect(void)
{
	if (doDCP_Connect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Disconnect(void)
{
	if (doDCP_Disconnect() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Fetch(void)
{
	if (doDCP_Fetch() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Init(void)
{
	if (doDCP_Init() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Load(void)
{
	if (doDCP_Load() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Open(void)
{
	if (doDCP_Open() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Reset(void)
{
	if (doDCP_Reset() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Setup(void)
{
	if (doDCP_Setup() < 0)
		BusErr ("DCP_1");
	return 0;
}
int CCALLBACK Wrapper_DCP_1_99_Status(void)
{
	if (doDCP_Status() < 0)
		BusErr ("DCP_1");
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
//	DCP_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_DCP_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_DCP_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_DCP_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_DCP_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_DCP_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_DCP_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_DCP_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_DCP_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_DCP_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_DCP_1_1_Status;
//
//	DCP_1:CH10
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 10;
	DevDat[3].d_acts[A_CLS] = Wrapper_DCP_1_10_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_DCP_1_10_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_DCP_1_10_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_DCP_1_10_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_DCP_1_10_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_DCP_1_10_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_DCP_1_10_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_DCP_1_10_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_DCP_1_10_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_DCP_1_10_Status;
//
//	DCP_1:CH100
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 100;
	DevDat[4].d_acts[A_CLS] = Wrapper_DCP_1_100_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_DCP_1_100_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_DCP_1_100_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_DCP_1_100_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_DCP_1_100_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_DCP_1_100_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_DCP_1_100_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_DCP_1_100_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_DCP_1_100_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_DCP_1_100_Status;
//
//	DCP_1:CH103
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 103;
	DevDat[5].d_acts[A_CLS] = Wrapper_DCP_1_103_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_DCP_1_103_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_DCP_1_103_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_DCP_1_103_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_DCP_1_103_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_DCP_1_103_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_DCP_1_103_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_DCP_1_103_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_DCP_1_103_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_DCP_1_103_Status;
//
//	DCP_1:CH104
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 104;
	DevDat[6].d_acts[A_CLS] = Wrapper_DCP_1_104_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_DCP_1_104_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_DCP_1_104_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_DCP_1_104_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_DCP_1_104_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_DCP_1_104_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_DCP_1_104_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_DCP_1_104_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_DCP_1_104_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_DCP_1_104_Status;
//
//	DCP_1:CH105
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 105;
	DevDat[7].d_acts[A_CLS] = Wrapper_DCP_1_105_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_DCP_1_105_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_DCP_1_105_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_DCP_1_105_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_DCP_1_105_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_DCP_1_105_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_DCP_1_105_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_DCP_1_105_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_DCP_1_105_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_DCP_1_105_Status;
//
//	DCP_1:CH106
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 106;
	DevDat[8].d_acts[A_CLS] = Wrapper_DCP_1_106_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_DCP_1_106_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_DCP_1_106_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_DCP_1_106_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_DCP_1_106_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_DCP_1_106_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_DCP_1_106_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_DCP_1_106_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_DCP_1_106_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_DCP_1_106_Status;
//
//	DCP_1:CH107
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 107;
	DevDat[9].d_acts[A_CLS] = Wrapper_DCP_1_107_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_DCP_1_107_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_DCP_1_107_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_DCP_1_107_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_DCP_1_107_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_DCP_1_107_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_DCP_1_107_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_DCP_1_107_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_DCP_1_107_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_DCP_1_107_Status;
//
//	DCP_1:CH108
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 108;
	DevDat[10].d_acts[A_CLS] = Wrapper_DCP_1_108_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_DCP_1_108_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_DCP_1_108_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_DCP_1_108_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_DCP_1_108_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_DCP_1_108_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_DCP_1_108_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_DCP_1_108_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_DCP_1_108_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_DCP_1_108_Status;
//
//	DCP_1:CH109
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 109;
	DevDat[11].d_acts[A_CLS] = Wrapper_DCP_1_109_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_DCP_1_109_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_DCP_1_109_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_DCP_1_109_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_DCP_1_109_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_DCP_1_109_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_DCP_1_109_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_DCP_1_109_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_DCP_1_109_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_DCP_1_109_Status;
//
//	DCP_1:CH110
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 110;
	DevDat[12].d_acts[A_CLS] = Wrapper_DCP_1_110_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_DCP_1_110_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_DCP_1_110_Disconnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_DCP_1_110_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_DCP_1_110_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_DCP_1_110_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_DCP_1_110_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_DCP_1_110_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_DCP_1_110_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_DCP_1_110_Status;
//
//	DCP_1:CH114
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 114;
	DevDat[13].d_acts[A_CLS] = Wrapper_DCP_1_114_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_DCP_1_114_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_DCP_1_114_Disconnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_DCP_1_114_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_DCP_1_114_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_DCP_1_114_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_DCP_1_114_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_DCP_1_114_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_DCP_1_114_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_DCP_1_114_Status;
//
//	DCP_1:CH115
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 115;
	DevDat[14].d_acts[A_CLS] = Wrapper_DCP_1_115_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_DCP_1_115_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_DCP_1_115_Disconnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_DCP_1_115_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_DCP_1_115_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_DCP_1_115_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_DCP_1_115_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_DCP_1_115_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_DCP_1_115_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_DCP_1_115_Status;
//
//	DCP_1:CH116
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 116;
	DevDat[15].d_acts[A_CLS] = Wrapper_DCP_1_116_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_DCP_1_116_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_DCP_1_116_Disconnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_DCP_1_116_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_DCP_1_116_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_DCP_1_116_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_DCP_1_116_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_DCP_1_116_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_DCP_1_116_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_DCP_1_116_Status;
//
//	DCP_1:CH117
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 117;
	DevDat[16].d_acts[A_CLS] = Wrapper_DCP_1_117_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_DCP_1_117_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_DCP_1_117_Disconnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_DCP_1_117_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_DCP_1_117_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_DCP_1_117_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_DCP_1_117_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_DCP_1_117_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_DCP_1_117_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_DCP_1_117_Status;
//
//	DCP_1:CH118
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 118;
	DevDat[17].d_acts[A_CLS] = Wrapper_DCP_1_118_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_DCP_1_118_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_DCP_1_118_Disconnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_DCP_1_118_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_DCP_1_118_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_DCP_1_118_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_DCP_1_118_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_DCP_1_118_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_DCP_1_118_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_DCP_1_118_Status;
//
//	DCP_1:CH119
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[18].d_modlst = p_mod;
	DevDat[18].d_fncP = 119;
	DevDat[18].d_acts[A_CLS] = Wrapper_DCP_1_119_Close;
	DevDat[18].d_acts[A_CON] = Wrapper_DCP_1_119_Connect;
	DevDat[18].d_acts[A_DIS] = Wrapper_DCP_1_119_Disconnect;
	DevDat[18].d_acts[A_FTH] = Wrapper_DCP_1_119_Fetch;
	DevDat[18].d_acts[A_INX] = Wrapper_DCP_1_119_Init;
	DevDat[18].d_acts[A_LOD] = Wrapper_DCP_1_119_Load;
	DevDat[18].d_acts[A_OPN] = Wrapper_DCP_1_119_Open;
	DevDat[18].d_acts[A_RST] = Wrapper_DCP_1_119_Reset;
	DevDat[18].d_acts[A_FNC] = Wrapper_DCP_1_119_Setup;
	DevDat[18].d_acts[A_STA] = Wrapper_DCP_1_119_Status;
//
//	DCP_1:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[19].d_modlst = p_mod;
	DevDat[19].d_fncP = 12;
	DevDat[19].d_acts[A_CLS] = Wrapper_DCP_1_12_Close;
	DevDat[19].d_acts[A_CON] = Wrapper_DCP_1_12_Connect;
	DevDat[19].d_acts[A_DIS] = Wrapper_DCP_1_12_Disconnect;
	DevDat[19].d_acts[A_FTH] = Wrapper_DCP_1_12_Fetch;
	DevDat[19].d_acts[A_INX] = Wrapper_DCP_1_12_Init;
	DevDat[19].d_acts[A_LOD] = Wrapper_DCP_1_12_Load;
	DevDat[19].d_acts[A_OPN] = Wrapper_DCP_1_12_Open;
	DevDat[19].d_acts[A_RST] = Wrapper_DCP_1_12_Reset;
	DevDat[19].d_acts[A_FNC] = Wrapper_DCP_1_12_Setup;
	DevDat[19].d_acts[A_STA] = Wrapper_DCP_1_12_Status;
//
//	DCP_1:CH120
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[20].d_modlst = p_mod;
	DevDat[20].d_fncP = 120;
	DevDat[20].d_acts[A_CLS] = Wrapper_DCP_1_120_Close;
	DevDat[20].d_acts[A_CON] = Wrapper_DCP_1_120_Connect;
	DevDat[20].d_acts[A_DIS] = Wrapper_DCP_1_120_Disconnect;
	DevDat[20].d_acts[A_FTH] = Wrapper_DCP_1_120_Fetch;
	DevDat[20].d_acts[A_INX] = Wrapper_DCP_1_120_Init;
	DevDat[20].d_acts[A_LOD] = Wrapper_DCP_1_120_Load;
	DevDat[20].d_acts[A_OPN] = Wrapper_DCP_1_120_Open;
	DevDat[20].d_acts[A_RST] = Wrapper_DCP_1_120_Reset;
	DevDat[20].d_acts[A_FNC] = Wrapper_DCP_1_120_Setup;
	DevDat[20].d_acts[A_STA] = Wrapper_DCP_1_120_Status;
//
//	DCP_1:CH125
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[21].d_modlst = p_mod;
	DevDat[21].d_fncP = 125;
	DevDat[21].d_acts[A_CLS] = Wrapper_DCP_1_125_Close;
	DevDat[21].d_acts[A_CON] = Wrapper_DCP_1_125_Connect;
	DevDat[21].d_acts[A_DIS] = Wrapper_DCP_1_125_Disconnect;
	DevDat[21].d_acts[A_FTH] = Wrapper_DCP_1_125_Fetch;
	DevDat[21].d_acts[A_INX] = Wrapper_DCP_1_125_Init;
	DevDat[21].d_acts[A_LOD] = Wrapper_DCP_1_125_Load;
	DevDat[21].d_acts[A_OPN] = Wrapper_DCP_1_125_Open;
	DevDat[21].d_acts[A_RST] = Wrapper_DCP_1_125_Reset;
	DevDat[21].d_acts[A_FNC] = Wrapper_DCP_1_125_Setup;
	DevDat[21].d_acts[A_STA] = Wrapper_DCP_1_125_Status;
//
//	DCP_1:CH126
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[22].d_modlst = p_mod;
	DevDat[22].d_fncP = 126;
	DevDat[22].d_acts[A_CLS] = Wrapper_DCP_1_126_Close;
	DevDat[22].d_acts[A_CON] = Wrapper_DCP_1_126_Connect;
	DevDat[22].d_acts[A_DIS] = Wrapper_DCP_1_126_Disconnect;
	DevDat[22].d_acts[A_FTH] = Wrapper_DCP_1_126_Fetch;
	DevDat[22].d_acts[A_INX] = Wrapper_DCP_1_126_Init;
	DevDat[22].d_acts[A_LOD] = Wrapper_DCP_1_126_Load;
	DevDat[22].d_acts[A_OPN] = Wrapper_DCP_1_126_Open;
	DevDat[22].d_acts[A_RST] = Wrapper_DCP_1_126_Reset;
	DevDat[22].d_acts[A_FNC] = Wrapper_DCP_1_126_Setup;
	DevDat[22].d_acts[A_STA] = Wrapper_DCP_1_126_Status;
//
//	DCP_1:CH127
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[23].d_modlst = p_mod;
	DevDat[23].d_fncP = 127;
	DevDat[23].d_acts[A_CLS] = Wrapper_DCP_1_127_Close;
	DevDat[23].d_acts[A_CON] = Wrapper_DCP_1_127_Connect;
	DevDat[23].d_acts[A_DIS] = Wrapper_DCP_1_127_Disconnect;
	DevDat[23].d_acts[A_FTH] = Wrapper_DCP_1_127_Fetch;
	DevDat[23].d_acts[A_INX] = Wrapper_DCP_1_127_Init;
	DevDat[23].d_acts[A_LOD] = Wrapper_DCP_1_127_Load;
	DevDat[23].d_acts[A_OPN] = Wrapper_DCP_1_127_Open;
	DevDat[23].d_acts[A_RST] = Wrapper_DCP_1_127_Reset;
	DevDat[23].d_acts[A_FNC] = Wrapper_DCP_1_127_Setup;
	DevDat[23].d_acts[A_STA] = Wrapper_DCP_1_127_Status;
//
//	DCP_1:CH128
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[24].d_modlst = p_mod;
	DevDat[24].d_fncP = 128;
	DevDat[24].d_acts[A_CLS] = Wrapper_DCP_1_128_Close;
	DevDat[24].d_acts[A_CON] = Wrapper_DCP_1_128_Connect;
	DevDat[24].d_acts[A_DIS] = Wrapper_DCP_1_128_Disconnect;
	DevDat[24].d_acts[A_FTH] = Wrapper_DCP_1_128_Fetch;
	DevDat[24].d_acts[A_INX] = Wrapper_DCP_1_128_Init;
	DevDat[24].d_acts[A_LOD] = Wrapper_DCP_1_128_Load;
	DevDat[24].d_acts[A_OPN] = Wrapper_DCP_1_128_Open;
	DevDat[24].d_acts[A_RST] = Wrapper_DCP_1_128_Reset;
	DevDat[24].d_acts[A_FNC] = Wrapper_DCP_1_128_Setup;
	DevDat[24].d_acts[A_STA] = Wrapper_DCP_1_128_Status;
//
//	DCP_1:CH129
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[25].d_modlst = p_mod;
	DevDat[25].d_fncP = 129;
	DevDat[25].d_acts[A_CLS] = Wrapper_DCP_1_129_Close;
	DevDat[25].d_acts[A_CON] = Wrapper_DCP_1_129_Connect;
	DevDat[25].d_acts[A_DIS] = Wrapper_DCP_1_129_Disconnect;
	DevDat[25].d_acts[A_FTH] = Wrapper_DCP_1_129_Fetch;
	DevDat[25].d_acts[A_INX] = Wrapper_DCP_1_129_Init;
	DevDat[25].d_acts[A_LOD] = Wrapper_DCP_1_129_Load;
	DevDat[25].d_acts[A_OPN] = Wrapper_DCP_1_129_Open;
	DevDat[25].d_acts[A_RST] = Wrapper_DCP_1_129_Reset;
	DevDat[25].d_acts[A_FNC] = Wrapper_DCP_1_129_Setup;
	DevDat[25].d_acts[A_STA] = Wrapper_DCP_1_129_Status;
//
//	DCP_1:CH13
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[26].d_modlst = p_mod;
	DevDat[26].d_fncP = 13;
	DevDat[26].d_acts[A_CLS] = Wrapper_DCP_1_13_Close;
	DevDat[26].d_acts[A_CON] = Wrapper_DCP_1_13_Connect;
	DevDat[26].d_acts[A_DIS] = Wrapper_DCP_1_13_Disconnect;
	DevDat[26].d_acts[A_FTH] = Wrapper_DCP_1_13_Fetch;
	DevDat[26].d_acts[A_INX] = Wrapper_DCP_1_13_Init;
	DevDat[26].d_acts[A_LOD] = Wrapper_DCP_1_13_Load;
	DevDat[26].d_acts[A_OPN] = Wrapper_DCP_1_13_Open;
	DevDat[26].d_acts[A_RST] = Wrapper_DCP_1_13_Reset;
	DevDat[26].d_acts[A_FNC] = Wrapper_DCP_1_13_Setup;
	DevDat[26].d_acts[A_STA] = Wrapper_DCP_1_13_Status;
//
//	DCP_1:CH130
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[27].d_modlst = p_mod;
	DevDat[27].d_fncP = 130;
	DevDat[27].d_acts[A_CLS] = Wrapper_DCP_1_130_Close;
	DevDat[27].d_acts[A_CON] = Wrapper_DCP_1_130_Connect;
	DevDat[27].d_acts[A_DIS] = Wrapper_DCP_1_130_Disconnect;
	DevDat[27].d_acts[A_FTH] = Wrapper_DCP_1_130_Fetch;
	DevDat[27].d_acts[A_INX] = Wrapper_DCP_1_130_Init;
	DevDat[27].d_acts[A_LOD] = Wrapper_DCP_1_130_Load;
	DevDat[27].d_acts[A_OPN] = Wrapper_DCP_1_130_Open;
	DevDat[27].d_acts[A_RST] = Wrapper_DCP_1_130_Reset;
	DevDat[27].d_acts[A_FNC] = Wrapper_DCP_1_130_Setup;
	DevDat[27].d_acts[A_STA] = Wrapper_DCP_1_130_Status;
//
//	DCP_1:CH136
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[28].d_modlst = p_mod;
	DevDat[28].d_fncP = 136;
	DevDat[28].d_acts[A_CLS] = Wrapper_DCP_1_136_Close;
	DevDat[28].d_acts[A_CON] = Wrapper_DCP_1_136_Connect;
	DevDat[28].d_acts[A_DIS] = Wrapper_DCP_1_136_Disconnect;
	DevDat[28].d_acts[A_FTH] = Wrapper_DCP_1_136_Fetch;
	DevDat[28].d_acts[A_INX] = Wrapper_DCP_1_136_Init;
	DevDat[28].d_acts[A_LOD] = Wrapper_DCP_1_136_Load;
	DevDat[28].d_acts[A_OPN] = Wrapper_DCP_1_136_Open;
	DevDat[28].d_acts[A_RST] = Wrapper_DCP_1_136_Reset;
	DevDat[28].d_acts[A_FNC] = Wrapper_DCP_1_136_Setup;
	DevDat[28].d_acts[A_STA] = Wrapper_DCP_1_136_Status;
//
//	DCP_1:CH137
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[29].d_modlst = p_mod;
	DevDat[29].d_fncP = 137;
	DevDat[29].d_acts[A_CLS] = Wrapper_DCP_1_137_Close;
	DevDat[29].d_acts[A_CON] = Wrapper_DCP_1_137_Connect;
	DevDat[29].d_acts[A_DIS] = Wrapper_DCP_1_137_Disconnect;
	DevDat[29].d_acts[A_FTH] = Wrapper_DCP_1_137_Fetch;
	DevDat[29].d_acts[A_INX] = Wrapper_DCP_1_137_Init;
	DevDat[29].d_acts[A_LOD] = Wrapper_DCP_1_137_Load;
	DevDat[29].d_acts[A_OPN] = Wrapper_DCP_1_137_Open;
	DevDat[29].d_acts[A_RST] = Wrapper_DCP_1_137_Reset;
	DevDat[29].d_acts[A_FNC] = Wrapper_DCP_1_137_Setup;
	DevDat[29].d_acts[A_STA] = Wrapper_DCP_1_137_Status;
//
//	DCP_1:CH138
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[30].d_modlst = p_mod;
	DevDat[30].d_fncP = 138;
	DevDat[30].d_acts[A_CLS] = Wrapper_DCP_1_138_Close;
	DevDat[30].d_acts[A_CON] = Wrapper_DCP_1_138_Connect;
	DevDat[30].d_acts[A_DIS] = Wrapper_DCP_1_138_Disconnect;
	DevDat[30].d_acts[A_FTH] = Wrapper_DCP_1_138_Fetch;
	DevDat[30].d_acts[A_INX] = Wrapper_DCP_1_138_Init;
	DevDat[30].d_acts[A_LOD] = Wrapper_DCP_1_138_Load;
	DevDat[30].d_acts[A_OPN] = Wrapper_DCP_1_138_Open;
	DevDat[30].d_acts[A_RST] = Wrapper_DCP_1_138_Reset;
	DevDat[30].d_acts[A_FNC] = Wrapper_DCP_1_138_Setup;
	DevDat[30].d_acts[A_STA] = Wrapper_DCP_1_138_Status;
//
//	DCP_1:CH139
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[31].d_modlst = p_mod;
	DevDat[31].d_fncP = 139;
	DevDat[31].d_acts[A_CLS] = Wrapper_DCP_1_139_Close;
	DevDat[31].d_acts[A_CON] = Wrapper_DCP_1_139_Connect;
	DevDat[31].d_acts[A_DIS] = Wrapper_DCP_1_139_Disconnect;
	DevDat[31].d_acts[A_FTH] = Wrapper_DCP_1_139_Fetch;
	DevDat[31].d_acts[A_INX] = Wrapper_DCP_1_139_Init;
	DevDat[31].d_acts[A_LOD] = Wrapper_DCP_1_139_Load;
	DevDat[31].d_acts[A_OPN] = Wrapper_DCP_1_139_Open;
	DevDat[31].d_acts[A_RST] = Wrapper_DCP_1_139_Reset;
	DevDat[31].d_acts[A_FNC] = Wrapper_DCP_1_139_Setup;
	DevDat[31].d_acts[A_STA] = Wrapper_DCP_1_139_Status;
//
//	DCP_1:CH14
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[32].d_modlst = p_mod;
	DevDat[32].d_fncP = 14;
	DevDat[32].d_acts[A_CLS] = Wrapper_DCP_1_14_Close;
	DevDat[32].d_acts[A_CON] = Wrapper_DCP_1_14_Connect;
	DevDat[32].d_acts[A_DIS] = Wrapper_DCP_1_14_Disconnect;
	DevDat[32].d_acts[A_FTH] = Wrapper_DCP_1_14_Fetch;
	DevDat[32].d_acts[A_INX] = Wrapper_DCP_1_14_Init;
	DevDat[32].d_acts[A_LOD] = Wrapper_DCP_1_14_Load;
	DevDat[32].d_acts[A_OPN] = Wrapper_DCP_1_14_Open;
	DevDat[32].d_acts[A_RST] = Wrapper_DCP_1_14_Reset;
	DevDat[32].d_acts[A_FNC] = Wrapper_DCP_1_14_Setup;
	DevDat[32].d_acts[A_STA] = Wrapper_DCP_1_14_Status;
//
//	DCP_1:CH140
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[33].d_modlst = p_mod;
	DevDat[33].d_fncP = 140;
	DevDat[33].d_acts[A_CLS] = Wrapper_DCP_1_140_Close;
	DevDat[33].d_acts[A_CON] = Wrapper_DCP_1_140_Connect;
	DevDat[33].d_acts[A_DIS] = Wrapper_DCP_1_140_Disconnect;
	DevDat[33].d_acts[A_FTH] = Wrapper_DCP_1_140_Fetch;
	DevDat[33].d_acts[A_INX] = Wrapper_DCP_1_140_Init;
	DevDat[33].d_acts[A_LOD] = Wrapper_DCP_1_140_Load;
	DevDat[33].d_acts[A_OPN] = Wrapper_DCP_1_140_Open;
	DevDat[33].d_acts[A_RST] = Wrapper_DCP_1_140_Reset;
	DevDat[33].d_acts[A_FNC] = Wrapper_DCP_1_140_Setup;
	DevDat[33].d_acts[A_STA] = Wrapper_DCP_1_140_Status;
//
//	DCP_1:CH147
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[34].d_modlst = p_mod;
	DevDat[34].d_fncP = 147;
	DevDat[34].d_acts[A_CLS] = Wrapper_DCP_1_147_Close;
	DevDat[34].d_acts[A_CON] = Wrapper_DCP_1_147_Connect;
	DevDat[34].d_acts[A_DIS] = Wrapper_DCP_1_147_Disconnect;
	DevDat[34].d_acts[A_FTH] = Wrapper_DCP_1_147_Fetch;
	DevDat[34].d_acts[A_INX] = Wrapper_DCP_1_147_Init;
	DevDat[34].d_acts[A_LOD] = Wrapper_DCP_1_147_Load;
	DevDat[34].d_acts[A_OPN] = Wrapper_DCP_1_147_Open;
	DevDat[34].d_acts[A_RST] = Wrapper_DCP_1_147_Reset;
	DevDat[34].d_acts[A_FNC] = Wrapper_DCP_1_147_Setup;
	DevDat[34].d_acts[A_STA] = Wrapper_DCP_1_147_Status;
//
//	DCP_1:CH148
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[35].d_modlst = p_mod;
	DevDat[35].d_fncP = 148;
	DevDat[35].d_acts[A_CLS] = Wrapper_DCP_1_148_Close;
	DevDat[35].d_acts[A_CON] = Wrapper_DCP_1_148_Connect;
	DevDat[35].d_acts[A_DIS] = Wrapper_DCP_1_148_Disconnect;
	DevDat[35].d_acts[A_FTH] = Wrapper_DCP_1_148_Fetch;
	DevDat[35].d_acts[A_INX] = Wrapper_DCP_1_148_Init;
	DevDat[35].d_acts[A_LOD] = Wrapper_DCP_1_148_Load;
	DevDat[35].d_acts[A_OPN] = Wrapper_DCP_1_148_Open;
	DevDat[35].d_acts[A_RST] = Wrapper_DCP_1_148_Reset;
	DevDat[35].d_acts[A_FNC] = Wrapper_DCP_1_148_Setup;
	DevDat[35].d_acts[A_STA] = Wrapper_DCP_1_148_Status;
//
//	DCP_1:CH149
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[36].d_modlst = p_mod;
	DevDat[36].d_fncP = 149;
	DevDat[36].d_acts[A_CLS] = Wrapper_DCP_1_149_Close;
	DevDat[36].d_acts[A_CON] = Wrapper_DCP_1_149_Connect;
	DevDat[36].d_acts[A_DIS] = Wrapper_DCP_1_149_Disconnect;
	DevDat[36].d_acts[A_FTH] = Wrapper_DCP_1_149_Fetch;
	DevDat[36].d_acts[A_INX] = Wrapper_DCP_1_149_Init;
	DevDat[36].d_acts[A_LOD] = Wrapper_DCP_1_149_Load;
	DevDat[36].d_acts[A_OPN] = Wrapper_DCP_1_149_Open;
	DevDat[36].d_acts[A_RST] = Wrapper_DCP_1_149_Reset;
	DevDat[36].d_acts[A_FNC] = Wrapper_DCP_1_149_Setup;
	DevDat[36].d_acts[A_STA] = Wrapper_DCP_1_149_Status;
//
//	DCP_1:CH15
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[37].d_modlst = p_mod;
	DevDat[37].d_fncP = 15;
	DevDat[37].d_acts[A_CLS] = Wrapper_DCP_1_15_Close;
	DevDat[37].d_acts[A_CON] = Wrapper_DCP_1_15_Connect;
	DevDat[37].d_acts[A_DIS] = Wrapper_DCP_1_15_Disconnect;
	DevDat[37].d_acts[A_FTH] = Wrapper_DCP_1_15_Fetch;
	DevDat[37].d_acts[A_INX] = Wrapper_DCP_1_15_Init;
	DevDat[37].d_acts[A_LOD] = Wrapper_DCP_1_15_Load;
	DevDat[37].d_acts[A_OPN] = Wrapper_DCP_1_15_Open;
	DevDat[37].d_acts[A_RST] = Wrapper_DCP_1_15_Reset;
	DevDat[37].d_acts[A_FNC] = Wrapper_DCP_1_15_Setup;
	DevDat[37].d_acts[A_STA] = Wrapper_DCP_1_15_Status;
//
//	DCP_1:CH150
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[38].d_modlst = p_mod;
	DevDat[38].d_fncP = 150;
	DevDat[38].d_acts[A_CLS] = Wrapper_DCP_1_150_Close;
	DevDat[38].d_acts[A_CON] = Wrapper_DCP_1_150_Connect;
	DevDat[38].d_acts[A_DIS] = Wrapper_DCP_1_150_Disconnect;
	DevDat[38].d_acts[A_FTH] = Wrapper_DCP_1_150_Fetch;
	DevDat[38].d_acts[A_INX] = Wrapper_DCP_1_150_Init;
	DevDat[38].d_acts[A_LOD] = Wrapper_DCP_1_150_Load;
	DevDat[38].d_acts[A_OPN] = Wrapper_DCP_1_150_Open;
	DevDat[38].d_acts[A_RST] = Wrapper_DCP_1_150_Reset;
	DevDat[38].d_acts[A_FNC] = Wrapper_DCP_1_150_Setup;
	DevDat[38].d_acts[A_STA] = Wrapper_DCP_1_150_Status;
//
//	DCP_1:CH158
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[39].d_modlst = p_mod;
	DevDat[39].d_fncP = 158;
	DevDat[39].d_acts[A_CLS] = Wrapper_DCP_1_158_Close;
	DevDat[39].d_acts[A_CON] = Wrapper_DCP_1_158_Connect;
	DevDat[39].d_acts[A_DIS] = Wrapper_DCP_1_158_Disconnect;
	DevDat[39].d_acts[A_FTH] = Wrapper_DCP_1_158_Fetch;
	DevDat[39].d_acts[A_INX] = Wrapper_DCP_1_158_Init;
	DevDat[39].d_acts[A_LOD] = Wrapper_DCP_1_158_Load;
	DevDat[39].d_acts[A_OPN] = Wrapper_DCP_1_158_Open;
	DevDat[39].d_acts[A_RST] = Wrapper_DCP_1_158_Reset;
	DevDat[39].d_acts[A_FNC] = Wrapper_DCP_1_158_Setup;
	DevDat[39].d_acts[A_STA] = Wrapper_DCP_1_158_Status;
//
//	DCP_1:CH159
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[40].d_modlst = p_mod;
	DevDat[40].d_fncP = 159;
	DevDat[40].d_acts[A_CLS] = Wrapper_DCP_1_159_Close;
	DevDat[40].d_acts[A_CON] = Wrapper_DCP_1_159_Connect;
	DevDat[40].d_acts[A_DIS] = Wrapper_DCP_1_159_Disconnect;
	DevDat[40].d_acts[A_FTH] = Wrapper_DCP_1_159_Fetch;
	DevDat[40].d_acts[A_INX] = Wrapper_DCP_1_159_Init;
	DevDat[40].d_acts[A_LOD] = Wrapper_DCP_1_159_Load;
	DevDat[40].d_acts[A_OPN] = Wrapper_DCP_1_159_Open;
	DevDat[40].d_acts[A_RST] = Wrapper_DCP_1_159_Reset;
	DevDat[40].d_acts[A_FNC] = Wrapper_DCP_1_159_Setup;
	DevDat[40].d_acts[A_STA] = Wrapper_DCP_1_159_Status;
//
//	DCP_1:CH16
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[41].d_modlst = p_mod;
	DevDat[41].d_fncP = 16;
	DevDat[41].d_acts[A_CLS] = Wrapper_DCP_1_16_Close;
	DevDat[41].d_acts[A_CON] = Wrapper_DCP_1_16_Connect;
	DevDat[41].d_acts[A_DIS] = Wrapper_DCP_1_16_Disconnect;
	DevDat[41].d_acts[A_FTH] = Wrapper_DCP_1_16_Fetch;
	DevDat[41].d_acts[A_INX] = Wrapper_DCP_1_16_Init;
	DevDat[41].d_acts[A_LOD] = Wrapper_DCP_1_16_Load;
	DevDat[41].d_acts[A_OPN] = Wrapper_DCP_1_16_Open;
	DevDat[41].d_acts[A_RST] = Wrapper_DCP_1_16_Reset;
	DevDat[41].d_acts[A_FNC] = Wrapper_DCP_1_16_Setup;
	DevDat[41].d_acts[A_STA] = Wrapper_DCP_1_16_Status;
//
//	DCP_1:CH160
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[42].d_modlst = p_mod;
	DevDat[42].d_fncP = 160;
	DevDat[42].d_acts[A_CLS] = Wrapper_DCP_1_160_Close;
	DevDat[42].d_acts[A_CON] = Wrapper_DCP_1_160_Connect;
	DevDat[42].d_acts[A_DIS] = Wrapper_DCP_1_160_Disconnect;
	DevDat[42].d_acts[A_FTH] = Wrapper_DCP_1_160_Fetch;
	DevDat[42].d_acts[A_INX] = Wrapper_DCP_1_160_Init;
	DevDat[42].d_acts[A_LOD] = Wrapper_DCP_1_160_Load;
	DevDat[42].d_acts[A_OPN] = Wrapper_DCP_1_160_Open;
	DevDat[42].d_acts[A_RST] = Wrapper_DCP_1_160_Reset;
	DevDat[42].d_acts[A_FNC] = Wrapper_DCP_1_160_Setup;
	DevDat[42].d_acts[A_STA] = Wrapper_DCP_1_160_Status;
//
//	DCP_1:CH169
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[43].d_modlst = p_mod;
	DevDat[43].d_fncP = 169;
	DevDat[43].d_acts[A_CLS] = Wrapper_DCP_1_169_Close;
	DevDat[43].d_acts[A_CON] = Wrapper_DCP_1_169_Connect;
	DevDat[43].d_acts[A_DIS] = Wrapper_DCP_1_169_Disconnect;
	DevDat[43].d_acts[A_FTH] = Wrapper_DCP_1_169_Fetch;
	DevDat[43].d_acts[A_INX] = Wrapper_DCP_1_169_Init;
	DevDat[43].d_acts[A_LOD] = Wrapper_DCP_1_169_Load;
	DevDat[43].d_acts[A_OPN] = Wrapper_DCP_1_169_Open;
	DevDat[43].d_acts[A_RST] = Wrapper_DCP_1_169_Reset;
	DevDat[43].d_acts[A_FNC] = Wrapper_DCP_1_169_Setup;
	DevDat[43].d_acts[A_STA] = Wrapper_DCP_1_169_Status;
//
//	DCP_1:CH17
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[44].d_modlst = p_mod;
	DevDat[44].d_fncP = 17;
	DevDat[44].d_acts[A_CLS] = Wrapper_DCP_1_17_Close;
	DevDat[44].d_acts[A_CON] = Wrapper_DCP_1_17_Connect;
	DevDat[44].d_acts[A_DIS] = Wrapper_DCP_1_17_Disconnect;
	DevDat[44].d_acts[A_FTH] = Wrapper_DCP_1_17_Fetch;
	DevDat[44].d_acts[A_INX] = Wrapper_DCP_1_17_Init;
	DevDat[44].d_acts[A_LOD] = Wrapper_DCP_1_17_Load;
	DevDat[44].d_acts[A_OPN] = Wrapper_DCP_1_17_Open;
	DevDat[44].d_acts[A_RST] = Wrapper_DCP_1_17_Reset;
	DevDat[44].d_acts[A_FNC] = Wrapper_DCP_1_17_Setup;
	DevDat[44].d_acts[A_STA] = Wrapper_DCP_1_17_Status;
//
//	DCP_1:CH170
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[45].d_modlst = p_mod;
	DevDat[45].d_fncP = 170;
	DevDat[45].d_acts[A_CLS] = Wrapper_DCP_1_170_Close;
	DevDat[45].d_acts[A_CON] = Wrapper_DCP_1_170_Connect;
	DevDat[45].d_acts[A_DIS] = Wrapper_DCP_1_170_Disconnect;
	DevDat[45].d_acts[A_FTH] = Wrapper_DCP_1_170_Fetch;
	DevDat[45].d_acts[A_INX] = Wrapper_DCP_1_170_Init;
	DevDat[45].d_acts[A_LOD] = Wrapper_DCP_1_170_Load;
	DevDat[45].d_acts[A_OPN] = Wrapper_DCP_1_170_Open;
	DevDat[45].d_acts[A_RST] = Wrapper_DCP_1_170_Reset;
	DevDat[45].d_acts[A_FNC] = Wrapper_DCP_1_170_Setup;
	DevDat[45].d_acts[A_STA] = Wrapper_DCP_1_170_Status;
//
//	DCP_1:CH18
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[46].d_modlst = p_mod;
	DevDat[46].d_fncP = 18;
	DevDat[46].d_acts[A_CLS] = Wrapper_DCP_1_18_Close;
	DevDat[46].d_acts[A_CON] = Wrapper_DCP_1_18_Connect;
	DevDat[46].d_acts[A_DIS] = Wrapper_DCP_1_18_Disconnect;
	DevDat[46].d_acts[A_FTH] = Wrapper_DCP_1_18_Fetch;
	DevDat[46].d_acts[A_INX] = Wrapper_DCP_1_18_Init;
	DevDat[46].d_acts[A_LOD] = Wrapper_DCP_1_18_Load;
	DevDat[46].d_acts[A_OPN] = Wrapper_DCP_1_18_Open;
	DevDat[46].d_acts[A_RST] = Wrapper_DCP_1_18_Reset;
	DevDat[46].d_acts[A_FNC] = Wrapper_DCP_1_18_Setup;
	DevDat[46].d_acts[A_STA] = Wrapper_DCP_1_18_Status;
//
//	DCP_1:CH180
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[47].d_modlst = p_mod;
	DevDat[47].d_fncP = 180;
	DevDat[47].d_acts[A_CLS] = Wrapper_DCP_1_180_Close;
	DevDat[47].d_acts[A_CON] = Wrapper_DCP_1_180_Connect;
	DevDat[47].d_acts[A_DIS] = Wrapper_DCP_1_180_Disconnect;
	DevDat[47].d_acts[A_FTH] = Wrapper_DCP_1_180_Fetch;
	DevDat[47].d_acts[A_INX] = Wrapper_DCP_1_180_Init;
	DevDat[47].d_acts[A_LOD] = Wrapper_DCP_1_180_Load;
	DevDat[47].d_acts[A_OPN] = Wrapper_DCP_1_180_Open;
	DevDat[47].d_acts[A_RST] = Wrapper_DCP_1_180_Reset;
	DevDat[47].d_acts[A_FNC] = Wrapper_DCP_1_180_Setup;
	DevDat[47].d_acts[A_STA] = Wrapper_DCP_1_180_Status;
//
//	DCP_1:CH181
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[48].d_modlst = p_mod;
	DevDat[48].d_fncP = 181;
	DevDat[48].d_acts[A_CLS] = Wrapper_DCP_1_181_Close;
	DevDat[48].d_acts[A_CON] = Wrapper_DCP_1_181_Connect;
	DevDat[48].d_acts[A_DIS] = Wrapper_DCP_1_181_Disconnect;
	DevDat[48].d_acts[A_FTH] = Wrapper_DCP_1_181_Fetch;
	DevDat[48].d_acts[A_INX] = Wrapper_DCP_1_181_Init;
	DevDat[48].d_acts[A_LOD] = Wrapper_DCP_1_181_Load;
	DevDat[48].d_acts[A_OPN] = Wrapper_DCP_1_181_Open;
	DevDat[48].d_acts[A_RST] = Wrapper_DCP_1_181_Reset;
	DevDat[48].d_acts[A_FNC] = Wrapper_DCP_1_181_Setup;
	DevDat[48].d_acts[A_STA] = Wrapper_DCP_1_181_Status;
//
//	DCP_1:CH182
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[49].d_modlst = p_mod;
	DevDat[49].d_fncP = 182;
	DevDat[49].d_acts[A_CLS] = Wrapper_DCP_1_182_Close;
	DevDat[49].d_acts[A_CON] = Wrapper_DCP_1_182_Connect;
	DevDat[49].d_acts[A_DIS] = Wrapper_DCP_1_182_Disconnect;
	DevDat[49].d_acts[A_FTH] = Wrapper_DCP_1_182_Fetch;
	DevDat[49].d_acts[A_INX] = Wrapper_DCP_1_182_Init;
	DevDat[49].d_acts[A_LOD] = Wrapper_DCP_1_182_Load;
	DevDat[49].d_acts[A_OPN] = Wrapper_DCP_1_182_Open;
	DevDat[49].d_acts[A_RST] = Wrapper_DCP_1_182_Reset;
	DevDat[49].d_acts[A_FNC] = Wrapper_DCP_1_182_Setup;
	DevDat[49].d_acts[A_STA] = Wrapper_DCP_1_182_Status;
//
//	DCP_1:CH183
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[50].d_modlst = p_mod;
	DevDat[50].d_fncP = 183;
	DevDat[50].d_acts[A_CLS] = Wrapper_DCP_1_183_Close;
	DevDat[50].d_acts[A_CON] = Wrapper_DCP_1_183_Connect;
	DevDat[50].d_acts[A_DIS] = Wrapper_DCP_1_183_Disconnect;
	DevDat[50].d_acts[A_FTH] = Wrapper_DCP_1_183_Fetch;
	DevDat[50].d_acts[A_INX] = Wrapper_DCP_1_183_Init;
	DevDat[50].d_acts[A_LOD] = Wrapper_DCP_1_183_Load;
	DevDat[50].d_acts[A_OPN] = Wrapper_DCP_1_183_Open;
	DevDat[50].d_acts[A_RST] = Wrapper_DCP_1_183_Reset;
	DevDat[50].d_acts[A_FNC] = Wrapper_DCP_1_183_Setup;
	DevDat[50].d_acts[A_STA] = Wrapper_DCP_1_183_Status;
//
//	DCP_1:CH184
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[51].d_modlst = p_mod;
	DevDat[51].d_fncP = 184;
	DevDat[51].d_acts[A_CLS] = Wrapper_DCP_1_184_Close;
	DevDat[51].d_acts[A_CON] = Wrapper_DCP_1_184_Connect;
	DevDat[51].d_acts[A_DIS] = Wrapper_DCP_1_184_Disconnect;
	DevDat[51].d_acts[A_FTH] = Wrapper_DCP_1_184_Fetch;
	DevDat[51].d_acts[A_INX] = Wrapper_DCP_1_184_Init;
	DevDat[51].d_acts[A_LOD] = Wrapper_DCP_1_184_Load;
	DevDat[51].d_acts[A_OPN] = Wrapper_DCP_1_184_Open;
	DevDat[51].d_acts[A_RST] = Wrapper_DCP_1_184_Reset;
	DevDat[51].d_acts[A_FNC] = Wrapper_DCP_1_184_Setup;
	DevDat[51].d_acts[A_STA] = Wrapper_DCP_1_184_Status;
//
//	DCP_1:CH185
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[52].d_modlst = p_mod;
	DevDat[52].d_fncP = 185;
	DevDat[52].d_acts[A_CLS] = Wrapper_DCP_1_185_Close;
	DevDat[52].d_acts[A_CON] = Wrapper_DCP_1_185_Connect;
	DevDat[52].d_acts[A_DIS] = Wrapper_DCP_1_185_Disconnect;
	DevDat[52].d_acts[A_FTH] = Wrapper_DCP_1_185_Fetch;
	DevDat[52].d_acts[A_INX] = Wrapper_DCP_1_185_Init;
	DevDat[52].d_acts[A_LOD] = Wrapper_DCP_1_185_Load;
	DevDat[52].d_acts[A_OPN] = Wrapper_DCP_1_185_Open;
	DevDat[52].d_acts[A_RST] = Wrapper_DCP_1_185_Reset;
	DevDat[52].d_acts[A_FNC] = Wrapper_DCP_1_185_Setup;
	DevDat[52].d_acts[A_STA] = Wrapper_DCP_1_185_Status;
//
//	DCP_1:CH186
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[53].d_modlst = p_mod;
	DevDat[53].d_fncP = 186;
	DevDat[53].d_acts[A_CLS] = Wrapper_DCP_1_186_Close;
	DevDat[53].d_acts[A_CON] = Wrapper_DCP_1_186_Connect;
	DevDat[53].d_acts[A_DIS] = Wrapper_DCP_1_186_Disconnect;
	DevDat[53].d_acts[A_FTH] = Wrapper_DCP_1_186_Fetch;
	DevDat[53].d_acts[A_INX] = Wrapper_DCP_1_186_Init;
	DevDat[53].d_acts[A_LOD] = Wrapper_DCP_1_186_Load;
	DevDat[53].d_acts[A_OPN] = Wrapper_DCP_1_186_Open;
	DevDat[53].d_acts[A_RST] = Wrapper_DCP_1_186_Reset;
	DevDat[53].d_acts[A_FNC] = Wrapper_DCP_1_186_Setup;
	DevDat[53].d_acts[A_STA] = Wrapper_DCP_1_186_Status;
//
//	DCP_1:CH187
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[54].d_modlst = p_mod;
	DevDat[54].d_fncP = 187;
	DevDat[54].d_acts[A_CLS] = Wrapper_DCP_1_187_Close;
	DevDat[54].d_acts[A_CON] = Wrapper_DCP_1_187_Connect;
	DevDat[54].d_acts[A_DIS] = Wrapper_DCP_1_187_Disconnect;
	DevDat[54].d_acts[A_FTH] = Wrapper_DCP_1_187_Fetch;
	DevDat[54].d_acts[A_INX] = Wrapper_DCP_1_187_Init;
	DevDat[54].d_acts[A_LOD] = Wrapper_DCP_1_187_Load;
	DevDat[54].d_acts[A_OPN] = Wrapper_DCP_1_187_Open;
	DevDat[54].d_acts[A_RST] = Wrapper_DCP_1_187_Reset;
	DevDat[54].d_acts[A_FNC] = Wrapper_DCP_1_187_Setup;
	DevDat[54].d_acts[A_STA] = Wrapper_DCP_1_187_Status;
//
//	DCP_1:CH188
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[55].d_modlst = p_mod;
	DevDat[55].d_fncP = 188;
	DevDat[55].d_acts[A_CLS] = Wrapper_DCP_1_188_Close;
	DevDat[55].d_acts[A_CON] = Wrapper_DCP_1_188_Connect;
	DevDat[55].d_acts[A_DIS] = Wrapper_DCP_1_188_Disconnect;
	DevDat[55].d_acts[A_FTH] = Wrapper_DCP_1_188_Fetch;
	DevDat[55].d_acts[A_INX] = Wrapper_DCP_1_188_Init;
	DevDat[55].d_acts[A_LOD] = Wrapper_DCP_1_188_Load;
	DevDat[55].d_acts[A_OPN] = Wrapper_DCP_1_188_Open;
	DevDat[55].d_acts[A_RST] = Wrapper_DCP_1_188_Reset;
	DevDat[55].d_acts[A_FNC] = Wrapper_DCP_1_188_Setup;
	DevDat[55].d_acts[A_STA] = Wrapper_DCP_1_188_Status;
//
//	DCP_1:CH189
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[56].d_modlst = p_mod;
	DevDat[56].d_fncP = 189;
	DevDat[56].d_acts[A_CLS] = Wrapper_DCP_1_189_Close;
	DevDat[56].d_acts[A_CON] = Wrapper_DCP_1_189_Connect;
	DevDat[56].d_acts[A_DIS] = Wrapper_DCP_1_189_Disconnect;
	DevDat[56].d_acts[A_FTH] = Wrapper_DCP_1_189_Fetch;
	DevDat[56].d_acts[A_INX] = Wrapper_DCP_1_189_Init;
	DevDat[56].d_acts[A_LOD] = Wrapper_DCP_1_189_Load;
	DevDat[56].d_acts[A_OPN] = Wrapper_DCP_1_189_Open;
	DevDat[56].d_acts[A_RST] = Wrapper_DCP_1_189_Reset;
	DevDat[56].d_acts[A_FNC] = Wrapper_DCP_1_189_Setup;
	DevDat[56].d_acts[A_STA] = Wrapper_DCP_1_189_Status;
//
//	DCP_1:CH19
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[57].d_modlst = p_mod;
	DevDat[57].d_fncP = 19;
	DevDat[57].d_acts[A_CLS] = Wrapper_DCP_1_19_Close;
	DevDat[57].d_acts[A_CON] = Wrapper_DCP_1_19_Connect;
	DevDat[57].d_acts[A_DIS] = Wrapper_DCP_1_19_Disconnect;
	DevDat[57].d_acts[A_FTH] = Wrapper_DCP_1_19_Fetch;
	DevDat[57].d_acts[A_INX] = Wrapper_DCP_1_19_Init;
	DevDat[57].d_acts[A_LOD] = Wrapper_DCP_1_19_Load;
	DevDat[57].d_acts[A_OPN] = Wrapper_DCP_1_19_Open;
	DevDat[57].d_acts[A_RST] = Wrapper_DCP_1_19_Reset;
	DevDat[57].d_acts[A_FNC] = Wrapper_DCP_1_19_Setup;
	DevDat[57].d_acts[A_STA] = Wrapper_DCP_1_19_Status;
//
//	DCP_1:CH190
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[58].d_modlst = p_mod;
	DevDat[58].d_fncP = 190;
	DevDat[58].d_acts[A_CLS] = Wrapper_DCP_1_190_Close;
	DevDat[58].d_acts[A_CON] = Wrapper_DCP_1_190_Connect;
	DevDat[58].d_acts[A_DIS] = Wrapper_DCP_1_190_Disconnect;
	DevDat[58].d_acts[A_FTH] = Wrapper_DCP_1_190_Fetch;
	DevDat[58].d_acts[A_INX] = Wrapper_DCP_1_190_Init;
	DevDat[58].d_acts[A_LOD] = Wrapper_DCP_1_190_Load;
	DevDat[58].d_acts[A_OPN] = Wrapper_DCP_1_190_Open;
	DevDat[58].d_acts[A_RST] = Wrapper_DCP_1_190_Reset;
	DevDat[58].d_acts[A_FNC] = Wrapper_DCP_1_190_Setup;
	DevDat[58].d_acts[A_STA] = Wrapper_DCP_1_190_Status;
//
//	DCP_1:CH191
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[59].d_modlst = p_mod;
	DevDat[59].d_fncP = 191;
	DevDat[59].d_acts[A_CLS] = Wrapper_DCP_1_191_Close;
	DevDat[59].d_acts[A_CON] = Wrapper_DCP_1_191_Connect;
	DevDat[59].d_acts[A_DIS] = Wrapper_DCP_1_191_Disconnect;
	DevDat[59].d_acts[A_FTH] = Wrapper_DCP_1_191_Fetch;
	DevDat[59].d_acts[A_INX] = Wrapper_DCP_1_191_Init;
	DevDat[59].d_acts[A_LOD] = Wrapper_DCP_1_191_Load;
	DevDat[59].d_acts[A_OPN] = Wrapper_DCP_1_191_Open;
	DevDat[59].d_acts[A_RST] = Wrapper_DCP_1_191_Reset;
	DevDat[59].d_acts[A_FNC] = Wrapper_DCP_1_191_Setup;
	DevDat[59].d_acts[A_STA] = Wrapper_DCP_1_191_Status;
//
//	DCP_1:CH192
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[60].d_modlst = p_mod;
	DevDat[60].d_fncP = 192;
	DevDat[60].d_acts[A_CLS] = Wrapper_DCP_1_192_Close;
	DevDat[60].d_acts[A_CON] = Wrapper_DCP_1_192_Connect;
	DevDat[60].d_acts[A_DIS] = Wrapper_DCP_1_192_Disconnect;
	DevDat[60].d_acts[A_FTH] = Wrapper_DCP_1_192_Fetch;
	DevDat[60].d_acts[A_INX] = Wrapper_DCP_1_192_Init;
	DevDat[60].d_acts[A_LOD] = Wrapper_DCP_1_192_Load;
	DevDat[60].d_acts[A_OPN] = Wrapper_DCP_1_192_Open;
	DevDat[60].d_acts[A_RST] = Wrapper_DCP_1_192_Reset;
	DevDat[60].d_acts[A_FNC] = Wrapper_DCP_1_192_Setup;
	DevDat[60].d_acts[A_STA] = Wrapper_DCP_1_192_Status;
//
//	DCP_1:CH193
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[61].d_modlst = p_mod;
	DevDat[61].d_fncP = 193;
	DevDat[61].d_acts[A_CLS] = Wrapper_DCP_1_193_Close;
	DevDat[61].d_acts[A_CON] = Wrapper_DCP_1_193_Connect;
	DevDat[61].d_acts[A_DIS] = Wrapper_DCP_1_193_Disconnect;
	DevDat[61].d_acts[A_FTH] = Wrapper_DCP_1_193_Fetch;
	DevDat[61].d_acts[A_INX] = Wrapper_DCP_1_193_Init;
	DevDat[61].d_acts[A_LOD] = Wrapper_DCP_1_193_Load;
	DevDat[61].d_acts[A_OPN] = Wrapper_DCP_1_193_Open;
	DevDat[61].d_acts[A_RST] = Wrapper_DCP_1_193_Reset;
	DevDat[61].d_acts[A_FNC] = Wrapper_DCP_1_193_Setup;
	DevDat[61].d_acts[A_STA] = Wrapper_DCP_1_193_Status;
//
//	DCP_1:CH194
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[62].d_modlst = p_mod;
	DevDat[62].d_fncP = 194;
	DevDat[62].d_acts[A_CLS] = Wrapper_DCP_1_194_Close;
	DevDat[62].d_acts[A_CON] = Wrapper_DCP_1_194_Connect;
	DevDat[62].d_acts[A_DIS] = Wrapper_DCP_1_194_Disconnect;
	DevDat[62].d_acts[A_FTH] = Wrapper_DCP_1_194_Fetch;
	DevDat[62].d_acts[A_INX] = Wrapper_DCP_1_194_Init;
	DevDat[62].d_acts[A_LOD] = Wrapper_DCP_1_194_Load;
	DevDat[62].d_acts[A_OPN] = Wrapper_DCP_1_194_Open;
	DevDat[62].d_acts[A_RST] = Wrapper_DCP_1_194_Reset;
	DevDat[62].d_acts[A_FNC] = Wrapper_DCP_1_194_Setup;
	DevDat[62].d_acts[A_STA] = Wrapper_DCP_1_194_Status;
//
//	DCP_1:CH195
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[63].d_modlst = p_mod;
	DevDat[63].d_fncP = 195;
	DevDat[63].d_acts[A_CLS] = Wrapper_DCP_1_195_Close;
	DevDat[63].d_acts[A_CON] = Wrapper_DCP_1_195_Connect;
	DevDat[63].d_acts[A_DIS] = Wrapper_DCP_1_195_Disconnect;
	DevDat[63].d_acts[A_FTH] = Wrapper_DCP_1_195_Fetch;
	DevDat[63].d_acts[A_INX] = Wrapper_DCP_1_195_Init;
	DevDat[63].d_acts[A_LOD] = Wrapper_DCP_1_195_Load;
	DevDat[63].d_acts[A_OPN] = Wrapper_DCP_1_195_Open;
	DevDat[63].d_acts[A_RST] = Wrapper_DCP_1_195_Reset;
	DevDat[63].d_acts[A_FNC] = Wrapper_DCP_1_195_Setup;
	DevDat[63].d_acts[A_STA] = Wrapper_DCP_1_195_Status;
//
//	DCP_1:CH196
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[64].d_modlst = p_mod;
	DevDat[64].d_fncP = 196;
	DevDat[64].d_acts[A_CLS] = Wrapper_DCP_1_196_Close;
	DevDat[64].d_acts[A_CON] = Wrapper_DCP_1_196_Connect;
	DevDat[64].d_acts[A_DIS] = Wrapper_DCP_1_196_Disconnect;
	DevDat[64].d_acts[A_FTH] = Wrapper_DCP_1_196_Fetch;
	DevDat[64].d_acts[A_INX] = Wrapper_DCP_1_196_Init;
	DevDat[64].d_acts[A_LOD] = Wrapper_DCP_1_196_Load;
	DevDat[64].d_acts[A_OPN] = Wrapper_DCP_1_196_Open;
	DevDat[64].d_acts[A_RST] = Wrapper_DCP_1_196_Reset;
	DevDat[64].d_acts[A_FNC] = Wrapper_DCP_1_196_Setup;
	DevDat[64].d_acts[A_STA] = Wrapper_DCP_1_196_Status;
//
//	DCP_1:CH197
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[65].d_modlst = p_mod;
	DevDat[65].d_fncP = 197;
	DevDat[65].d_acts[A_CLS] = Wrapper_DCP_1_197_Close;
	DevDat[65].d_acts[A_CON] = Wrapper_DCP_1_197_Connect;
	DevDat[65].d_acts[A_DIS] = Wrapper_DCP_1_197_Disconnect;
	DevDat[65].d_acts[A_FTH] = Wrapper_DCP_1_197_Fetch;
	DevDat[65].d_acts[A_INX] = Wrapper_DCP_1_197_Init;
	DevDat[65].d_acts[A_LOD] = Wrapper_DCP_1_197_Load;
	DevDat[65].d_acts[A_OPN] = Wrapper_DCP_1_197_Open;
	DevDat[65].d_acts[A_RST] = Wrapper_DCP_1_197_Reset;
	DevDat[65].d_acts[A_FNC] = Wrapper_DCP_1_197_Setup;
	DevDat[65].d_acts[A_STA] = Wrapper_DCP_1_197_Status;
//
//	DCP_1:CH198
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[66].d_modlst = p_mod;
	DevDat[66].d_fncP = 198;
	DevDat[66].d_acts[A_CLS] = Wrapper_DCP_1_198_Close;
	DevDat[66].d_acts[A_CON] = Wrapper_DCP_1_198_Connect;
	DevDat[66].d_acts[A_DIS] = Wrapper_DCP_1_198_Disconnect;
	DevDat[66].d_acts[A_FTH] = Wrapper_DCP_1_198_Fetch;
	DevDat[66].d_acts[A_INX] = Wrapper_DCP_1_198_Init;
	DevDat[66].d_acts[A_LOD] = Wrapper_DCP_1_198_Load;
	DevDat[66].d_acts[A_OPN] = Wrapper_DCP_1_198_Open;
	DevDat[66].d_acts[A_RST] = Wrapper_DCP_1_198_Reset;
	DevDat[66].d_acts[A_FNC] = Wrapper_DCP_1_198_Setup;
	DevDat[66].d_acts[A_STA] = Wrapper_DCP_1_198_Status;
//
//	DCP_1:CH199
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[67].d_modlst = p_mod;
	DevDat[67].d_fncP = 199;
	DevDat[67].d_acts[A_CLS] = Wrapper_DCP_1_199_Close;
	DevDat[67].d_acts[A_CON] = Wrapper_DCP_1_199_Connect;
	DevDat[67].d_acts[A_DIS] = Wrapper_DCP_1_199_Disconnect;
	DevDat[67].d_acts[A_FTH] = Wrapper_DCP_1_199_Fetch;
	DevDat[67].d_acts[A_INX] = Wrapper_DCP_1_199_Init;
	DevDat[67].d_acts[A_LOD] = Wrapper_DCP_1_199_Load;
	DevDat[67].d_acts[A_OPN] = Wrapper_DCP_1_199_Open;
	DevDat[67].d_acts[A_RST] = Wrapper_DCP_1_199_Reset;
	DevDat[67].d_acts[A_FNC] = Wrapper_DCP_1_199_Setup;
	DevDat[67].d_acts[A_STA] = Wrapper_DCP_1_199_Status;
//
//	DCP_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[68].d_modlst = p_mod;
	DevDat[68].d_fncP = 2;
	DevDat[68].d_acts[A_CLS] = Wrapper_DCP_1_2_Close;
	DevDat[68].d_acts[A_CON] = Wrapper_DCP_1_2_Connect;
	DevDat[68].d_acts[A_DIS] = Wrapper_DCP_1_2_Disconnect;
	DevDat[68].d_acts[A_FTH] = Wrapper_DCP_1_2_Fetch;
	DevDat[68].d_acts[A_INX] = Wrapper_DCP_1_2_Init;
	DevDat[68].d_acts[A_LOD] = Wrapper_DCP_1_2_Load;
	DevDat[68].d_acts[A_OPN] = Wrapper_DCP_1_2_Open;
	DevDat[68].d_acts[A_RST] = Wrapper_DCP_1_2_Reset;
	DevDat[68].d_acts[A_FNC] = Wrapper_DCP_1_2_Setup;
	DevDat[68].d_acts[A_STA] = Wrapper_DCP_1_2_Status;
//
//	DCP_1:CH210
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[69].d_modlst = p_mod;
	DevDat[69].d_fncP = 210;
	DevDat[69].d_acts[A_CLS] = Wrapper_DCP_1_210_Close;
	DevDat[69].d_acts[A_CON] = Wrapper_DCP_1_210_Connect;
	DevDat[69].d_acts[A_DIS] = Wrapper_DCP_1_210_Disconnect;
	DevDat[69].d_acts[A_FTH] = Wrapper_DCP_1_210_Fetch;
	DevDat[69].d_acts[A_INX] = Wrapper_DCP_1_210_Init;
	DevDat[69].d_acts[A_LOD] = Wrapper_DCP_1_210_Load;
	DevDat[69].d_acts[A_OPN] = Wrapper_DCP_1_210_Open;
	DevDat[69].d_acts[A_RST] = Wrapper_DCP_1_210_Reset;
	DevDat[69].d_acts[A_FNC] = Wrapper_DCP_1_210_Setup;
	DevDat[69].d_acts[A_STA] = Wrapper_DCP_1_210_Status;
//
//	DCP_1:CH211
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[70].d_modlst = p_mod;
	DevDat[70].d_fncP = 211;
	DevDat[70].d_acts[A_CLS] = Wrapper_DCP_1_211_Close;
	DevDat[70].d_acts[A_CON] = Wrapper_DCP_1_211_Connect;
	DevDat[70].d_acts[A_DIS] = Wrapper_DCP_1_211_Disconnect;
	DevDat[70].d_acts[A_FTH] = Wrapper_DCP_1_211_Fetch;
	DevDat[70].d_acts[A_INX] = Wrapper_DCP_1_211_Init;
	DevDat[70].d_acts[A_LOD] = Wrapper_DCP_1_211_Load;
	DevDat[70].d_acts[A_OPN] = Wrapper_DCP_1_211_Open;
	DevDat[70].d_acts[A_RST] = Wrapper_DCP_1_211_Reset;
	DevDat[70].d_acts[A_FNC] = Wrapper_DCP_1_211_Setup;
	DevDat[70].d_acts[A_STA] = Wrapper_DCP_1_211_Status;
//
//	DCP_1:CH212
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[71].d_modlst = p_mod;
	DevDat[71].d_fncP = 212;
	DevDat[71].d_acts[A_CLS] = Wrapper_DCP_1_212_Close;
	DevDat[71].d_acts[A_CON] = Wrapper_DCP_1_212_Connect;
	DevDat[71].d_acts[A_DIS] = Wrapper_DCP_1_212_Disconnect;
	DevDat[71].d_acts[A_FTH] = Wrapper_DCP_1_212_Fetch;
	DevDat[71].d_acts[A_INX] = Wrapper_DCP_1_212_Init;
	DevDat[71].d_acts[A_LOD] = Wrapper_DCP_1_212_Load;
	DevDat[71].d_acts[A_OPN] = Wrapper_DCP_1_212_Open;
	DevDat[71].d_acts[A_RST] = Wrapper_DCP_1_212_Reset;
	DevDat[71].d_acts[A_FNC] = Wrapper_DCP_1_212_Setup;
	DevDat[71].d_acts[A_STA] = Wrapper_DCP_1_212_Status;
//
//	DCP_1:CH213
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[72].d_modlst = p_mod;
	DevDat[72].d_fncP = 213;
	DevDat[72].d_acts[A_CLS] = Wrapper_DCP_1_213_Close;
	DevDat[72].d_acts[A_CON] = Wrapper_DCP_1_213_Connect;
	DevDat[72].d_acts[A_DIS] = Wrapper_DCP_1_213_Disconnect;
	DevDat[72].d_acts[A_FTH] = Wrapper_DCP_1_213_Fetch;
	DevDat[72].d_acts[A_INX] = Wrapper_DCP_1_213_Init;
	DevDat[72].d_acts[A_LOD] = Wrapper_DCP_1_213_Load;
	DevDat[72].d_acts[A_OPN] = Wrapper_DCP_1_213_Open;
	DevDat[72].d_acts[A_RST] = Wrapper_DCP_1_213_Reset;
	DevDat[72].d_acts[A_FNC] = Wrapper_DCP_1_213_Setup;
	DevDat[72].d_acts[A_STA] = Wrapper_DCP_1_213_Status;
//
//	DCP_1:CH214
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[73].d_modlst = p_mod;
	DevDat[73].d_fncP = 214;
	DevDat[73].d_acts[A_CLS] = Wrapper_DCP_1_214_Close;
	DevDat[73].d_acts[A_CON] = Wrapper_DCP_1_214_Connect;
	DevDat[73].d_acts[A_DIS] = Wrapper_DCP_1_214_Disconnect;
	DevDat[73].d_acts[A_FTH] = Wrapper_DCP_1_214_Fetch;
	DevDat[73].d_acts[A_INX] = Wrapper_DCP_1_214_Init;
	DevDat[73].d_acts[A_LOD] = Wrapper_DCP_1_214_Load;
	DevDat[73].d_acts[A_OPN] = Wrapper_DCP_1_214_Open;
	DevDat[73].d_acts[A_RST] = Wrapper_DCP_1_214_Reset;
	DevDat[73].d_acts[A_FNC] = Wrapper_DCP_1_214_Setup;
	DevDat[73].d_acts[A_STA] = Wrapper_DCP_1_214_Status;
//
//	DCP_1:CH215
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[74].d_modlst = p_mod;
	DevDat[74].d_fncP = 215;
	DevDat[74].d_acts[A_CLS] = Wrapper_DCP_1_215_Close;
	DevDat[74].d_acts[A_CON] = Wrapper_DCP_1_215_Connect;
	DevDat[74].d_acts[A_DIS] = Wrapper_DCP_1_215_Disconnect;
	DevDat[74].d_acts[A_FTH] = Wrapper_DCP_1_215_Fetch;
	DevDat[74].d_acts[A_INX] = Wrapper_DCP_1_215_Init;
	DevDat[74].d_acts[A_LOD] = Wrapper_DCP_1_215_Load;
	DevDat[74].d_acts[A_OPN] = Wrapper_DCP_1_215_Open;
	DevDat[74].d_acts[A_RST] = Wrapper_DCP_1_215_Reset;
	DevDat[74].d_acts[A_FNC] = Wrapper_DCP_1_215_Setup;
	DevDat[74].d_acts[A_STA] = Wrapper_DCP_1_215_Status;
//
//	DCP_1:CH216
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[75].d_modlst = p_mod;
	DevDat[75].d_fncP = 216;
	DevDat[75].d_acts[A_CLS] = Wrapper_DCP_1_216_Close;
	DevDat[75].d_acts[A_CON] = Wrapper_DCP_1_216_Connect;
	DevDat[75].d_acts[A_DIS] = Wrapper_DCP_1_216_Disconnect;
	DevDat[75].d_acts[A_FTH] = Wrapper_DCP_1_216_Fetch;
	DevDat[75].d_acts[A_INX] = Wrapper_DCP_1_216_Init;
	DevDat[75].d_acts[A_LOD] = Wrapper_DCP_1_216_Load;
	DevDat[75].d_acts[A_OPN] = Wrapper_DCP_1_216_Open;
	DevDat[75].d_acts[A_RST] = Wrapper_DCP_1_216_Reset;
	DevDat[75].d_acts[A_FNC] = Wrapper_DCP_1_216_Setup;
	DevDat[75].d_acts[A_STA] = Wrapper_DCP_1_216_Status;
//
//	DCP_1:CH217
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[76].d_modlst = p_mod;
	DevDat[76].d_fncP = 217;
	DevDat[76].d_acts[A_CLS] = Wrapper_DCP_1_217_Close;
	DevDat[76].d_acts[A_CON] = Wrapper_DCP_1_217_Connect;
	DevDat[76].d_acts[A_DIS] = Wrapper_DCP_1_217_Disconnect;
	DevDat[76].d_acts[A_FTH] = Wrapper_DCP_1_217_Fetch;
	DevDat[76].d_acts[A_INX] = Wrapper_DCP_1_217_Init;
	DevDat[76].d_acts[A_LOD] = Wrapper_DCP_1_217_Load;
	DevDat[76].d_acts[A_OPN] = Wrapper_DCP_1_217_Open;
	DevDat[76].d_acts[A_RST] = Wrapper_DCP_1_217_Reset;
	DevDat[76].d_acts[A_FNC] = Wrapper_DCP_1_217_Setup;
	DevDat[76].d_acts[A_STA] = Wrapper_DCP_1_217_Status;
//
//	DCP_1:CH218
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[77].d_modlst = p_mod;
	DevDat[77].d_fncP = 218;
	DevDat[77].d_acts[A_CLS] = Wrapper_DCP_1_218_Close;
	DevDat[77].d_acts[A_CON] = Wrapper_DCP_1_218_Connect;
	DevDat[77].d_acts[A_DIS] = Wrapper_DCP_1_218_Disconnect;
	DevDat[77].d_acts[A_FTH] = Wrapper_DCP_1_218_Fetch;
	DevDat[77].d_acts[A_INX] = Wrapper_DCP_1_218_Init;
	DevDat[77].d_acts[A_LOD] = Wrapper_DCP_1_218_Load;
	DevDat[77].d_acts[A_OPN] = Wrapper_DCP_1_218_Open;
	DevDat[77].d_acts[A_RST] = Wrapper_DCP_1_218_Reset;
	DevDat[77].d_acts[A_FNC] = Wrapper_DCP_1_218_Setup;
	DevDat[77].d_acts[A_STA] = Wrapper_DCP_1_218_Status;
//
//	DCP_1:CH219
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[78].d_modlst = p_mod;
	DevDat[78].d_fncP = 219;
	DevDat[78].d_acts[A_CLS] = Wrapper_DCP_1_219_Close;
	DevDat[78].d_acts[A_CON] = Wrapper_DCP_1_219_Connect;
	DevDat[78].d_acts[A_DIS] = Wrapper_DCP_1_219_Disconnect;
	DevDat[78].d_acts[A_FTH] = Wrapper_DCP_1_219_Fetch;
	DevDat[78].d_acts[A_INX] = Wrapper_DCP_1_219_Init;
	DevDat[78].d_acts[A_LOD] = Wrapper_DCP_1_219_Load;
	DevDat[78].d_acts[A_OPN] = Wrapper_DCP_1_219_Open;
	DevDat[78].d_acts[A_RST] = Wrapper_DCP_1_219_Reset;
	DevDat[78].d_acts[A_FNC] = Wrapper_DCP_1_219_Setup;
	DevDat[78].d_acts[A_STA] = Wrapper_DCP_1_219_Status;
//
//	DCP_1:CH220
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[79].d_modlst = p_mod;
	DevDat[79].d_fncP = 220;
	DevDat[79].d_acts[A_CLS] = Wrapper_DCP_1_220_Close;
	DevDat[79].d_acts[A_CON] = Wrapper_DCP_1_220_Connect;
	DevDat[79].d_acts[A_DIS] = Wrapper_DCP_1_220_Disconnect;
	DevDat[79].d_acts[A_FTH] = Wrapper_DCP_1_220_Fetch;
	DevDat[79].d_acts[A_INX] = Wrapper_DCP_1_220_Init;
	DevDat[79].d_acts[A_LOD] = Wrapper_DCP_1_220_Load;
	DevDat[79].d_acts[A_OPN] = Wrapper_DCP_1_220_Open;
	DevDat[79].d_acts[A_RST] = Wrapper_DCP_1_220_Reset;
	DevDat[79].d_acts[A_FNC] = Wrapper_DCP_1_220_Setup;
	DevDat[79].d_acts[A_STA] = Wrapper_DCP_1_220_Status;
//
//	DCP_1:CH221
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[80].d_modlst = p_mod;
	DevDat[80].d_fncP = 221;
	DevDat[80].d_acts[A_CLS] = Wrapper_DCP_1_221_Close;
	DevDat[80].d_acts[A_CON] = Wrapper_DCP_1_221_Connect;
	DevDat[80].d_acts[A_DIS] = Wrapper_DCP_1_221_Disconnect;
	DevDat[80].d_acts[A_FTH] = Wrapper_DCP_1_221_Fetch;
	DevDat[80].d_acts[A_INX] = Wrapper_DCP_1_221_Init;
	DevDat[80].d_acts[A_LOD] = Wrapper_DCP_1_221_Load;
	DevDat[80].d_acts[A_OPN] = Wrapper_DCP_1_221_Open;
	DevDat[80].d_acts[A_RST] = Wrapper_DCP_1_221_Reset;
	DevDat[80].d_acts[A_FNC] = Wrapper_DCP_1_221_Setup;
	DevDat[80].d_acts[A_STA] = Wrapper_DCP_1_221_Status;
//
//	DCP_1:CH222
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[81].d_modlst = p_mod;
	DevDat[81].d_fncP = 222;
	DevDat[81].d_acts[A_CLS] = Wrapper_DCP_1_222_Close;
	DevDat[81].d_acts[A_CON] = Wrapper_DCP_1_222_Connect;
	DevDat[81].d_acts[A_DIS] = Wrapper_DCP_1_222_Disconnect;
	DevDat[81].d_acts[A_FTH] = Wrapper_DCP_1_222_Fetch;
	DevDat[81].d_acts[A_INX] = Wrapper_DCP_1_222_Init;
	DevDat[81].d_acts[A_LOD] = Wrapper_DCP_1_222_Load;
	DevDat[81].d_acts[A_OPN] = Wrapper_DCP_1_222_Open;
	DevDat[81].d_acts[A_RST] = Wrapper_DCP_1_222_Reset;
	DevDat[81].d_acts[A_FNC] = Wrapper_DCP_1_222_Setup;
	DevDat[81].d_acts[A_STA] = Wrapper_DCP_1_222_Status;
//
//	DCP_1:CH223
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[82].d_modlst = p_mod;
	DevDat[82].d_fncP = 223;
	DevDat[82].d_acts[A_CLS] = Wrapper_DCP_1_223_Close;
	DevDat[82].d_acts[A_CON] = Wrapper_DCP_1_223_Connect;
	DevDat[82].d_acts[A_DIS] = Wrapper_DCP_1_223_Disconnect;
	DevDat[82].d_acts[A_FTH] = Wrapper_DCP_1_223_Fetch;
	DevDat[82].d_acts[A_INX] = Wrapper_DCP_1_223_Init;
	DevDat[82].d_acts[A_LOD] = Wrapper_DCP_1_223_Load;
	DevDat[82].d_acts[A_OPN] = Wrapper_DCP_1_223_Open;
	DevDat[82].d_acts[A_RST] = Wrapper_DCP_1_223_Reset;
	DevDat[82].d_acts[A_FNC] = Wrapper_DCP_1_223_Setup;
	DevDat[82].d_acts[A_STA] = Wrapper_DCP_1_223_Status;
//
//	DCP_1:CH224
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[83].d_modlst = p_mod;
	DevDat[83].d_fncP = 224;
	DevDat[83].d_acts[A_CLS] = Wrapper_DCP_1_224_Close;
	DevDat[83].d_acts[A_CON] = Wrapper_DCP_1_224_Connect;
	DevDat[83].d_acts[A_DIS] = Wrapper_DCP_1_224_Disconnect;
	DevDat[83].d_acts[A_FTH] = Wrapper_DCP_1_224_Fetch;
	DevDat[83].d_acts[A_INX] = Wrapper_DCP_1_224_Init;
	DevDat[83].d_acts[A_LOD] = Wrapper_DCP_1_224_Load;
	DevDat[83].d_acts[A_OPN] = Wrapper_DCP_1_224_Open;
	DevDat[83].d_acts[A_RST] = Wrapper_DCP_1_224_Reset;
	DevDat[83].d_acts[A_FNC] = Wrapper_DCP_1_224_Setup;
	DevDat[83].d_acts[A_STA] = Wrapper_DCP_1_224_Status;
//
//	DCP_1:CH225
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[84].d_modlst = p_mod;
	DevDat[84].d_fncP = 225;
	DevDat[84].d_acts[A_CLS] = Wrapper_DCP_1_225_Close;
	DevDat[84].d_acts[A_CON] = Wrapper_DCP_1_225_Connect;
	DevDat[84].d_acts[A_DIS] = Wrapper_DCP_1_225_Disconnect;
	DevDat[84].d_acts[A_FTH] = Wrapper_DCP_1_225_Fetch;
	DevDat[84].d_acts[A_INX] = Wrapper_DCP_1_225_Init;
	DevDat[84].d_acts[A_LOD] = Wrapper_DCP_1_225_Load;
	DevDat[84].d_acts[A_OPN] = Wrapper_DCP_1_225_Open;
	DevDat[84].d_acts[A_RST] = Wrapper_DCP_1_225_Reset;
	DevDat[84].d_acts[A_FNC] = Wrapper_DCP_1_225_Setup;
	DevDat[84].d_acts[A_STA] = Wrapper_DCP_1_225_Status;
//
//	DCP_1:CH226
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[85].d_modlst = p_mod;
	DevDat[85].d_fncP = 226;
	DevDat[85].d_acts[A_CLS] = Wrapper_DCP_1_226_Close;
	DevDat[85].d_acts[A_CON] = Wrapper_DCP_1_226_Connect;
	DevDat[85].d_acts[A_DIS] = Wrapper_DCP_1_226_Disconnect;
	DevDat[85].d_acts[A_FTH] = Wrapper_DCP_1_226_Fetch;
	DevDat[85].d_acts[A_INX] = Wrapper_DCP_1_226_Init;
	DevDat[85].d_acts[A_LOD] = Wrapper_DCP_1_226_Load;
	DevDat[85].d_acts[A_OPN] = Wrapper_DCP_1_226_Open;
	DevDat[85].d_acts[A_RST] = Wrapper_DCP_1_226_Reset;
	DevDat[85].d_acts[A_FNC] = Wrapper_DCP_1_226_Setup;
	DevDat[85].d_acts[A_STA] = Wrapper_DCP_1_226_Status;
//
//	DCP_1:CH227
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[86].d_modlst = p_mod;
	DevDat[86].d_fncP = 227;
	DevDat[86].d_acts[A_CLS] = Wrapper_DCP_1_227_Close;
	DevDat[86].d_acts[A_CON] = Wrapper_DCP_1_227_Connect;
	DevDat[86].d_acts[A_DIS] = Wrapper_DCP_1_227_Disconnect;
	DevDat[86].d_acts[A_FTH] = Wrapper_DCP_1_227_Fetch;
	DevDat[86].d_acts[A_INX] = Wrapper_DCP_1_227_Init;
	DevDat[86].d_acts[A_LOD] = Wrapper_DCP_1_227_Load;
	DevDat[86].d_acts[A_OPN] = Wrapper_DCP_1_227_Open;
	DevDat[86].d_acts[A_RST] = Wrapper_DCP_1_227_Reset;
	DevDat[86].d_acts[A_FNC] = Wrapper_DCP_1_227_Setup;
	DevDat[86].d_acts[A_STA] = Wrapper_DCP_1_227_Status;
//
//	DCP_1:CH228
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[87].d_modlst = p_mod;
	DevDat[87].d_fncP = 228;
	DevDat[87].d_acts[A_CLS] = Wrapper_DCP_1_228_Close;
	DevDat[87].d_acts[A_CON] = Wrapper_DCP_1_228_Connect;
	DevDat[87].d_acts[A_DIS] = Wrapper_DCP_1_228_Disconnect;
	DevDat[87].d_acts[A_FTH] = Wrapper_DCP_1_228_Fetch;
	DevDat[87].d_acts[A_INX] = Wrapper_DCP_1_228_Init;
	DevDat[87].d_acts[A_LOD] = Wrapper_DCP_1_228_Load;
	DevDat[87].d_acts[A_OPN] = Wrapper_DCP_1_228_Open;
	DevDat[87].d_acts[A_RST] = Wrapper_DCP_1_228_Reset;
	DevDat[87].d_acts[A_FNC] = Wrapper_DCP_1_228_Setup;
	DevDat[87].d_acts[A_STA] = Wrapper_DCP_1_228_Status;
//
//	DCP_1:CH229
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[88].d_modlst = p_mod;
	DevDat[88].d_fncP = 229;
	DevDat[88].d_acts[A_CLS] = Wrapper_DCP_1_229_Close;
	DevDat[88].d_acts[A_CON] = Wrapper_DCP_1_229_Connect;
	DevDat[88].d_acts[A_DIS] = Wrapper_DCP_1_229_Disconnect;
	DevDat[88].d_acts[A_FTH] = Wrapper_DCP_1_229_Fetch;
	DevDat[88].d_acts[A_INX] = Wrapper_DCP_1_229_Init;
	DevDat[88].d_acts[A_LOD] = Wrapper_DCP_1_229_Load;
	DevDat[88].d_acts[A_OPN] = Wrapper_DCP_1_229_Open;
	DevDat[88].d_acts[A_RST] = Wrapper_DCP_1_229_Reset;
	DevDat[88].d_acts[A_FNC] = Wrapper_DCP_1_229_Setup;
	DevDat[88].d_acts[A_STA] = Wrapper_DCP_1_229_Status;
//
//	DCP_1:CH23
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[89].d_modlst = p_mod;
	DevDat[89].d_fncP = 23;
	DevDat[89].d_acts[A_CLS] = Wrapper_DCP_1_23_Close;
	DevDat[89].d_acts[A_CON] = Wrapper_DCP_1_23_Connect;
	DevDat[89].d_acts[A_DIS] = Wrapper_DCP_1_23_Disconnect;
	DevDat[89].d_acts[A_FTH] = Wrapper_DCP_1_23_Fetch;
	DevDat[89].d_acts[A_INX] = Wrapper_DCP_1_23_Init;
	DevDat[89].d_acts[A_LOD] = Wrapper_DCP_1_23_Load;
	DevDat[89].d_acts[A_OPN] = Wrapper_DCP_1_23_Open;
	DevDat[89].d_acts[A_RST] = Wrapper_DCP_1_23_Reset;
	DevDat[89].d_acts[A_FNC] = Wrapper_DCP_1_23_Setup;
	DevDat[89].d_acts[A_STA] = Wrapper_DCP_1_23_Status;
//
//	DCP_1:CH24
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[90].d_modlst = p_mod;
	DevDat[90].d_fncP = 24;
	DevDat[90].d_acts[A_CLS] = Wrapper_DCP_1_24_Close;
	DevDat[90].d_acts[A_CON] = Wrapper_DCP_1_24_Connect;
	DevDat[90].d_acts[A_DIS] = Wrapper_DCP_1_24_Disconnect;
	DevDat[90].d_acts[A_FTH] = Wrapper_DCP_1_24_Fetch;
	DevDat[90].d_acts[A_INX] = Wrapper_DCP_1_24_Init;
	DevDat[90].d_acts[A_LOD] = Wrapper_DCP_1_24_Load;
	DevDat[90].d_acts[A_OPN] = Wrapper_DCP_1_24_Open;
	DevDat[90].d_acts[A_RST] = Wrapper_DCP_1_24_Reset;
	DevDat[90].d_acts[A_FNC] = Wrapper_DCP_1_24_Setup;
	DevDat[90].d_acts[A_STA] = Wrapper_DCP_1_24_Status;
//
//	DCP_1:CH25
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[91].d_modlst = p_mod;
	DevDat[91].d_fncP = 25;
	DevDat[91].d_acts[A_CLS] = Wrapper_DCP_1_25_Close;
	DevDat[91].d_acts[A_CON] = Wrapper_DCP_1_25_Connect;
	DevDat[91].d_acts[A_DIS] = Wrapper_DCP_1_25_Disconnect;
	DevDat[91].d_acts[A_FTH] = Wrapper_DCP_1_25_Fetch;
	DevDat[91].d_acts[A_INX] = Wrapper_DCP_1_25_Init;
	DevDat[91].d_acts[A_LOD] = Wrapper_DCP_1_25_Load;
	DevDat[91].d_acts[A_OPN] = Wrapper_DCP_1_25_Open;
	DevDat[91].d_acts[A_RST] = Wrapper_DCP_1_25_Reset;
	DevDat[91].d_acts[A_FNC] = Wrapper_DCP_1_25_Setup;
	DevDat[91].d_acts[A_STA] = Wrapper_DCP_1_25_Status;
//
//	DCP_1:CH26
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[92].d_modlst = p_mod;
	DevDat[92].d_fncP = 26;
	DevDat[92].d_acts[A_CLS] = Wrapper_DCP_1_26_Close;
	DevDat[92].d_acts[A_CON] = Wrapper_DCP_1_26_Connect;
	DevDat[92].d_acts[A_DIS] = Wrapper_DCP_1_26_Disconnect;
	DevDat[92].d_acts[A_FTH] = Wrapper_DCP_1_26_Fetch;
	DevDat[92].d_acts[A_INX] = Wrapper_DCP_1_26_Init;
	DevDat[92].d_acts[A_LOD] = Wrapper_DCP_1_26_Load;
	DevDat[92].d_acts[A_OPN] = Wrapper_DCP_1_26_Open;
	DevDat[92].d_acts[A_RST] = Wrapper_DCP_1_26_Reset;
	DevDat[92].d_acts[A_FNC] = Wrapper_DCP_1_26_Setup;
	DevDat[92].d_acts[A_STA] = Wrapper_DCP_1_26_Status;
//
//	DCP_1:CH27
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[93].d_modlst = p_mod;
	DevDat[93].d_fncP = 27;
	DevDat[93].d_acts[A_CLS] = Wrapper_DCP_1_27_Close;
	DevDat[93].d_acts[A_CON] = Wrapper_DCP_1_27_Connect;
	DevDat[93].d_acts[A_DIS] = Wrapper_DCP_1_27_Disconnect;
	DevDat[93].d_acts[A_FTH] = Wrapper_DCP_1_27_Fetch;
	DevDat[93].d_acts[A_INX] = Wrapper_DCP_1_27_Init;
	DevDat[93].d_acts[A_LOD] = Wrapper_DCP_1_27_Load;
	DevDat[93].d_acts[A_OPN] = Wrapper_DCP_1_27_Open;
	DevDat[93].d_acts[A_RST] = Wrapper_DCP_1_27_Reset;
	DevDat[93].d_acts[A_FNC] = Wrapper_DCP_1_27_Setup;
	DevDat[93].d_acts[A_STA] = Wrapper_DCP_1_27_Status;
//
//	DCP_1:CH28
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[94].d_modlst = p_mod;
	DevDat[94].d_fncP = 28;
	DevDat[94].d_acts[A_CLS] = Wrapper_DCP_1_28_Close;
	DevDat[94].d_acts[A_CON] = Wrapper_DCP_1_28_Connect;
	DevDat[94].d_acts[A_DIS] = Wrapper_DCP_1_28_Disconnect;
	DevDat[94].d_acts[A_FTH] = Wrapper_DCP_1_28_Fetch;
	DevDat[94].d_acts[A_INX] = Wrapper_DCP_1_28_Init;
	DevDat[94].d_acts[A_LOD] = Wrapper_DCP_1_28_Load;
	DevDat[94].d_acts[A_OPN] = Wrapper_DCP_1_28_Open;
	DevDat[94].d_acts[A_RST] = Wrapper_DCP_1_28_Reset;
	DevDat[94].d_acts[A_FNC] = Wrapper_DCP_1_28_Setup;
	DevDat[94].d_acts[A_STA] = Wrapper_DCP_1_28_Status;
//
//	DCP_1:CH29
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[95].d_modlst = p_mod;
	DevDat[95].d_fncP = 29;
	DevDat[95].d_acts[A_CLS] = Wrapper_DCP_1_29_Close;
	DevDat[95].d_acts[A_CON] = Wrapper_DCP_1_29_Connect;
	DevDat[95].d_acts[A_DIS] = Wrapper_DCP_1_29_Disconnect;
	DevDat[95].d_acts[A_FTH] = Wrapper_DCP_1_29_Fetch;
	DevDat[95].d_acts[A_INX] = Wrapper_DCP_1_29_Init;
	DevDat[95].d_acts[A_LOD] = Wrapper_DCP_1_29_Load;
	DevDat[95].d_acts[A_OPN] = Wrapper_DCP_1_29_Open;
	DevDat[95].d_acts[A_RST] = Wrapper_DCP_1_29_Reset;
	DevDat[95].d_acts[A_FNC] = Wrapper_DCP_1_29_Setup;
	DevDat[95].d_acts[A_STA] = Wrapper_DCP_1_29_Status;
//
//	DCP_1:CH3
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[96].d_modlst = p_mod;
	DevDat[96].d_fncP = 3;
	DevDat[96].d_acts[A_CLS] = Wrapper_DCP_1_3_Close;
	DevDat[96].d_acts[A_CON] = Wrapper_DCP_1_3_Connect;
	DevDat[96].d_acts[A_DIS] = Wrapper_DCP_1_3_Disconnect;
	DevDat[96].d_acts[A_FTH] = Wrapper_DCP_1_3_Fetch;
	DevDat[96].d_acts[A_INX] = Wrapper_DCP_1_3_Init;
	DevDat[96].d_acts[A_LOD] = Wrapper_DCP_1_3_Load;
	DevDat[96].d_acts[A_OPN] = Wrapper_DCP_1_3_Open;
	DevDat[96].d_acts[A_RST] = Wrapper_DCP_1_3_Reset;
	DevDat[96].d_acts[A_FNC] = Wrapper_DCP_1_3_Setup;
	DevDat[96].d_acts[A_STA] = Wrapper_DCP_1_3_Status;
//
//	DCP_1:CH34
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[97].d_modlst = p_mod;
	DevDat[97].d_fncP = 34;
	DevDat[97].d_acts[A_CLS] = Wrapper_DCP_1_34_Close;
	DevDat[97].d_acts[A_CON] = Wrapper_DCP_1_34_Connect;
	DevDat[97].d_acts[A_DIS] = Wrapper_DCP_1_34_Disconnect;
	DevDat[97].d_acts[A_FTH] = Wrapper_DCP_1_34_Fetch;
	DevDat[97].d_acts[A_INX] = Wrapper_DCP_1_34_Init;
	DevDat[97].d_acts[A_LOD] = Wrapper_DCP_1_34_Load;
	DevDat[97].d_acts[A_OPN] = Wrapper_DCP_1_34_Open;
	DevDat[97].d_acts[A_RST] = Wrapper_DCP_1_34_Reset;
	DevDat[97].d_acts[A_FNC] = Wrapper_DCP_1_34_Setup;
	DevDat[97].d_acts[A_STA] = Wrapper_DCP_1_34_Status;
//
//	DCP_1:CH35
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[98].d_modlst = p_mod;
	DevDat[98].d_fncP = 35;
	DevDat[98].d_acts[A_CLS] = Wrapper_DCP_1_35_Close;
	DevDat[98].d_acts[A_CON] = Wrapper_DCP_1_35_Connect;
	DevDat[98].d_acts[A_DIS] = Wrapper_DCP_1_35_Disconnect;
	DevDat[98].d_acts[A_FTH] = Wrapper_DCP_1_35_Fetch;
	DevDat[98].d_acts[A_INX] = Wrapper_DCP_1_35_Init;
	DevDat[98].d_acts[A_LOD] = Wrapper_DCP_1_35_Load;
	DevDat[98].d_acts[A_OPN] = Wrapper_DCP_1_35_Open;
	DevDat[98].d_acts[A_RST] = Wrapper_DCP_1_35_Reset;
	DevDat[98].d_acts[A_FNC] = Wrapper_DCP_1_35_Setup;
	DevDat[98].d_acts[A_STA] = Wrapper_DCP_1_35_Status;
//
//	DCP_1:CH36
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[99].d_modlst = p_mod;
	DevDat[99].d_fncP = 36;
	DevDat[99].d_acts[A_CLS] = Wrapper_DCP_1_36_Close;
	DevDat[99].d_acts[A_CON] = Wrapper_DCP_1_36_Connect;
	DevDat[99].d_acts[A_DIS] = Wrapper_DCP_1_36_Disconnect;
	DevDat[99].d_acts[A_FTH] = Wrapper_DCP_1_36_Fetch;
	DevDat[99].d_acts[A_INX] = Wrapper_DCP_1_36_Init;
	DevDat[99].d_acts[A_LOD] = Wrapper_DCP_1_36_Load;
	DevDat[99].d_acts[A_OPN] = Wrapper_DCP_1_36_Open;
	DevDat[99].d_acts[A_RST] = Wrapper_DCP_1_36_Reset;
	DevDat[99].d_acts[A_FNC] = Wrapper_DCP_1_36_Setup;
	DevDat[99].d_acts[A_STA] = Wrapper_DCP_1_36_Status;
//
//	DCP_1:CH37
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[100].d_modlst = p_mod;
	DevDat[100].d_fncP = 37;
	DevDat[100].d_acts[A_CLS] = Wrapper_DCP_1_37_Close;
	DevDat[100].d_acts[A_CON] = Wrapper_DCP_1_37_Connect;
	DevDat[100].d_acts[A_DIS] = Wrapper_DCP_1_37_Disconnect;
	DevDat[100].d_acts[A_FTH] = Wrapper_DCP_1_37_Fetch;
	DevDat[100].d_acts[A_INX] = Wrapper_DCP_1_37_Init;
	DevDat[100].d_acts[A_LOD] = Wrapper_DCP_1_37_Load;
	DevDat[100].d_acts[A_OPN] = Wrapper_DCP_1_37_Open;
	DevDat[100].d_acts[A_RST] = Wrapper_DCP_1_37_Reset;
	DevDat[100].d_acts[A_FNC] = Wrapper_DCP_1_37_Setup;
	DevDat[100].d_acts[A_STA] = Wrapper_DCP_1_37_Status;
//
//	DCP_1:CH38
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[101].d_modlst = p_mod;
	DevDat[101].d_fncP = 38;
	DevDat[101].d_acts[A_CLS] = Wrapper_DCP_1_38_Close;
	DevDat[101].d_acts[A_CON] = Wrapper_DCP_1_38_Connect;
	DevDat[101].d_acts[A_DIS] = Wrapper_DCP_1_38_Disconnect;
	DevDat[101].d_acts[A_FTH] = Wrapper_DCP_1_38_Fetch;
	DevDat[101].d_acts[A_INX] = Wrapper_DCP_1_38_Init;
	DevDat[101].d_acts[A_LOD] = Wrapper_DCP_1_38_Load;
	DevDat[101].d_acts[A_OPN] = Wrapper_DCP_1_38_Open;
	DevDat[101].d_acts[A_RST] = Wrapper_DCP_1_38_Reset;
	DevDat[101].d_acts[A_FNC] = Wrapper_DCP_1_38_Setup;
	DevDat[101].d_acts[A_STA] = Wrapper_DCP_1_38_Status;
//
//	DCP_1:CH39
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[102].d_modlst = p_mod;
	DevDat[102].d_fncP = 39;
	DevDat[102].d_acts[A_CLS] = Wrapper_DCP_1_39_Close;
	DevDat[102].d_acts[A_CON] = Wrapper_DCP_1_39_Connect;
	DevDat[102].d_acts[A_DIS] = Wrapper_DCP_1_39_Disconnect;
	DevDat[102].d_acts[A_FTH] = Wrapper_DCP_1_39_Fetch;
	DevDat[102].d_acts[A_INX] = Wrapper_DCP_1_39_Init;
	DevDat[102].d_acts[A_LOD] = Wrapper_DCP_1_39_Load;
	DevDat[102].d_acts[A_OPN] = Wrapper_DCP_1_39_Open;
	DevDat[102].d_acts[A_RST] = Wrapper_DCP_1_39_Reset;
	DevDat[102].d_acts[A_FNC] = Wrapper_DCP_1_39_Setup;
	DevDat[102].d_acts[A_STA] = Wrapper_DCP_1_39_Status;
//
//	DCP_1:CH4
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[103].d_modlst = p_mod;
	DevDat[103].d_fncP = 4;
	DevDat[103].d_acts[A_CLS] = Wrapper_DCP_1_4_Close;
	DevDat[103].d_acts[A_CON] = Wrapper_DCP_1_4_Connect;
	DevDat[103].d_acts[A_DIS] = Wrapper_DCP_1_4_Disconnect;
	DevDat[103].d_acts[A_FTH] = Wrapper_DCP_1_4_Fetch;
	DevDat[103].d_acts[A_INX] = Wrapper_DCP_1_4_Init;
	DevDat[103].d_acts[A_LOD] = Wrapper_DCP_1_4_Load;
	DevDat[103].d_acts[A_OPN] = Wrapper_DCP_1_4_Open;
	DevDat[103].d_acts[A_RST] = Wrapper_DCP_1_4_Reset;
	DevDat[103].d_acts[A_FNC] = Wrapper_DCP_1_4_Setup;
	DevDat[103].d_acts[A_STA] = Wrapper_DCP_1_4_Status;
//
//	DCP_1:CH45
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[104].d_modlst = p_mod;
	DevDat[104].d_fncP = 45;
	DevDat[104].d_acts[A_CLS] = Wrapper_DCP_1_45_Close;
	DevDat[104].d_acts[A_CON] = Wrapper_DCP_1_45_Connect;
	DevDat[104].d_acts[A_DIS] = Wrapper_DCP_1_45_Disconnect;
	DevDat[104].d_acts[A_FTH] = Wrapper_DCP_1_45_Fetch;
	DevDat[104].d_acts[A_INX] = Wrapper_DCP_1_45_Init;
	DevDat[104].d_acts[A_LOD] = Wrapper_DCP_1_45_Load;
	DevDat[104].d_acts[A_OPN] = Wrapper_DCP_1_45_Open;
	DevDat[104].d_acts[A_RST] = Wrapper_DCP_1_45_Reset;
	DevDat[104].d_acts[A_FNC] = Wrapper_DCP_1_45_Setup;
	DevDat[104].d_acts[A_STA] = Wrapper_DCP_1_45_Status;
//
//	DCP_1:CH46
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[105].d_modlst = p_mod;
	DevDat[105].d_fncP = 46;
	DevDat[105].d_acts[A_CLS] = Wrapper_DCP_1_46_Close;
	DevDat[105].d_acts[A_CON] = Wrapper_DCP_1_46_Connect;
	DevDat[105].d_acts[A_DIS] = Wrapper_DCP_1_46_Disconnect;
	DevDat[105].d_acts[A_FTH] = Wrapper_DCP_1_46_Fetch;
	DevDat[105].d_acts[A_INX] = Wrapper_DCP_1_46_Init;
	DevDat[105].d_acts[A_LOD] = Wrapper_DCP_1_46_Load;
	DevDat[105].d_acts[A_OPN] = Wrapper_DCP_1_46_Open;
	DevDat[105].d_acts[A_RST] = Wrapper_DCP_1_46_Reset;
	DevDat[105].d_acts[A_FNC] = Wrapper_DCP_1_46_Setup;
	DevDat[105].d_acts[A_STA] = Wrapper_DCP_1_46_Status;
//
//	DCP_1:CH47
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[106].d_modlst = p_mod;
	DevDat[106].d_fncP = 47;
	DevDat[106].d_acts[A_CLS] = Wrapper_DCP_1_47_Close;
	DevDat[106].d_acts[A_CON] = Wrapper_DCP_1_47_Connect;
	DevDat[106].d_acts[A_DIS] = Wrapper_DCP_1_47_Disconnect;
	DevDat[106].d_acts[A_FTH] = Wrapper_DCP_1_47_Fetch;
	DevDat[106].d_acts[A_INX] = Wrapper_DCP_1_47_Init;
	DevDat[106].d_acts[A_LOD] = Wrapper_DCP_1_47_Load;
	DevDat[106].d_acts[A_OPN] = Wrapper_DCP_1_47_Open;
	DevDat[106].d_acts[A_RST] = Wrapper_DCP_1_47_Reset;
	DevDat[106].d_acts[A_FNC] = Wrapper_DCP_1_47_Setup;
	DevDat[106].d_acts[A_STA] = Wrapper_DCP_1_47_Status;
//
//	DCP_1:CH48
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[107].d_modlst = p_mod;
	DevDat[107].d_fncP = 48;
	DevDat[107].d_acts[A_CLS] = Wrapper_DCP_1_48_Close;
	DevDat[107].d_acts[A_CON] = Wrapper_DCP_1_48_Connect;
	DevDat[107].d_acts[A_DIS] = Wrapper_DCP_1_48_Disconnect;
	DevDat[107].d_acts[A_FTH] = Wrapper_DCP_1_48_Fetch;
	DevDat[107].d_acts[A_INX] = Wrapper_DCP_1_48_Init;
	DevDat[107].d_acts[A_LOD] = Wrapper_DCP_1_48_Load;
	DevDat[107].d_acts[A_OPN] = Wrapper_DCP_1_48_Open;
	DevDat[107].d_acts[A_RST] = Wrapper_DCP_1_48_Reset;
	DevDat[107].d_acts[A_FNC] = Wrapper_DCP_1_48_Setup;
	DevDat[107].d_acts[A_STA] = Wrapper_DCP_1_48_Status;
//
//	DCP_1:CH49
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[108].d_modlst = p_mod;
	DevDat[108].d_fncP = 49;
	DevDat[108].d_acts[A_CLS] = Wrapper_DCP_1_49_Close;
	DevDat[108].d_acts[A_CON] = Wrapper_DCP_1_49_Connect;
	DevDat[108].d_acts[A_DIS] = Wrapper_DCP_1_49_Disconnect;
	DevDat[108].d_acts[A_FTH] = Wrapper_DCP_1_49_Fetch;
	DevDat[108].d_acts[A_INX] = Wrapper_DCP_1_49_Init;
	DevDat[108].d_acts[A_LOD] = Wrapper_DCP_1_49_Load;
	DevDat[108].d_acts[A_OPN] = Wrapper_DCP_1_49_Open;
	DevDat[108].d_acts[A_RST] = Wrapper_DCP_1_49_Reset;
	DevDat[108].d_acts[A_FNC] = Wrapper_DCP_1_49_Setup;
	DevDat[108].d_acts[A_STA] = Wrapper_DCP_1_49_Status;
//
//	DCP_1:CH5
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[109].d_modlst = p_mod;
	DevDat[109].d_fncP = 5;
	DevDat[109].d_acts[A_CLS] = Wrapper_DCP_1_5_Close;
	DevDat[109].d_acts[A_CON] = Wrapper_DCP_1_5_Connect;
	DevDat[109].d_acts[A_DIS] = Wrapper_DCP_1_5_Disconnect;
	DevDat[109].d_acts[A_FTH] = Wrapper_DCP_1_5_Fetch;
	DevDat[109].d_acts[A_INX] = Wrapper_DCP_1_5_Init;
	DevDat[109].d_acts[A_LOD] = Wrapper_DCP_1_5_Load;
	DevDat[109].d_acts[A_OPN] = Wrapper_DCP_1_5_Open;
	DevDat[109].d_acts[A_RST] = Wrapper_DCP_1_5_Reset;
	DevDat[109].d_acts[A_FNC] = Wrapper_DCP_1_5_Setup;
	DevDat[109].d_acts[A_STA] = Wrapper_DCP_1_5_Status;
//
//	DCP_1:CH56
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[110].d_modlst = p_mod;
	DevDat[110].d_fncP = 56;
	DevDat[110].d_acts[A_CLS] = Wrapper_DCP_1_56_Close;
	DevDat[110].d_acts[A_CON] = Wrapper_DCP_1_56_Connect;
	DevDat[110].d_acts[A_DIS] = Wrapper_DCP_1_56_Disconnect;
	DevDat[110].d_acts[A_FTH] = Wrapper_DCP_1_56_Fetch;
	DevDat[110].d_acts[A_INX] = Wrapper_DCP_1_56_Init;
	DevDat[110].d_acts[A_LOD] = Wrapper_DCP_1_56_Load;
	DevDat[110].d_acts[A_OPN] = Wrapper_DCP_1_56_Open;
	DevDat[110].d_acts[A_RST] = Wrapper_DCP_1_56_Reset;
	DevDat[110].d_acts[A_FNC] = Wrapper_DCP_1_56_Setup;
	DevDat[110].d_acts[A_STA] = Wrapper_DCP_1_56_Status;
//
//	DCP_1:CH57
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[111].d_modlst = p_mod;
	DevDat[111].d_fncP = 57;
	DevDat[111].d_acts[A_CLS] = Wrapper_DCP_1_57_Close;
	DevDat[111].d_acts[A_CON] = Wrapper_DCP_1_57_Connect;
	DevDat[111].d_acts[A_DIS] = Wrapper_DCP_1_57_Disconnect;
	DevDat[111].d_acts[A_FTH] = Wrapper_DCP_1_57_Fetch;
	DevDat[111].d_acts[A_INX] = Wrapper_DCP_1_57_Init;
	DevDat[111].d_acts[A_LOD] = Wrapper_DCP_1_57_Load;
	DevDat[111].d_acts[A_OPN] = Wrapper_DCP_1_57_Open;
	DevDat[111].d_acts[A_RST] = Wrapper_DCP_1_57_Reset;
	DevDat[111].d_acts[A_FNC] = Wrapper_DCP_1_57_Setup;
	DevDat[111].d_acts[A_STA] = Wrapper_DCP_1_57_Status;
//
//	DCP_1:CH58
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[112].d_modlst = p_mod;
	DevDat[112].d_fncP = 58;
	DevDat[112].d_acts[A_CLS] = Wrapper_DCP_1_58_Close;
	DevDat[112].d_acts[A_CON] = Wrapper_DCP_1_58_Connect;
	DevDat[112].d_acts[A_DIS] = Wrapper_DCP_1_58_Disconnect;
	DevDat[112].d_acts[A_FTH] = Wrapper_DCP_1_58_Fetch;
	DevDat[112].d_acts[A_INX] = Wrapper_DCP_1_58_Init;
	DevDat[112].d_acts[A_LOD] = Wrapper_DCP_1_58_Load;
	DevDat[112].d_acts[A_OPN] = Wrapper_DCP_1_58_Open;
	DevDat[112].d_acts[A_RST] = Wrapper_DCP_1_58_Reset;
	DevDat[112].d_acts[A_FNC] = Wrapper_DCP_1_58_Setup;
	DevDat[112].d_acts[A_STA] = Wrapper_DCP_1_58_Status;
//
//	DCP_1:CH59
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[113].d_modlst = p_mod;
	DevDat[113].d_fncP = 59;
	DevDat[113].d_acts[A_CLS] = Wrapper_DCP_1_59_Close;
	DevDat[113].d_acts[A_CON] = Wrapper_DCP_1_59_Connect;
	DevDat[113].d_acts[A_DIS] = Wrapper_DCP_1_59_Disconnect;
	DevDat[113].d_acts[A_FTH] = Wrapper_DCP_1_59_Fetch;
	DevDat[113].d_acts[A_INX] = Wrapper_DCP_1_59_Init;
	DevDat[113].d_acts[A_LOD] = Wrapper_DCP_1_59_Load;
	DevDat[113].d_acts[A_OPN] = Wrapper_DCP_1_59_Open;
	DevDat[113].d_acts[A_RST] = Wrapper_DCP_1_59_Reset;
	DevDat[113].d_acts[A_FNC] = Wrapper_DCP_1_59_Setup;
	DevDat[113].d_acts[A_STA] = Wrapper_DCP_1_59_Status;
//
//	DCP_1:CH6
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[114].d_modlst = p_mod;
	DevDat[114].d_fncP = 6;
	DevDat[114].d_acts[A_CLS] = Wrapper_DCP_1_6_Close;
	DevDat[114].d_acts[A_CON] = Wrapper_DCP_1_6_Connect;
	DevDat[114].d_acts[A_DIS] = Wrapper_DCP_1_6_Disconnect;
	DevDat[114].d_acts[A_FTH] = Wrapper_DCP_1_6_Fetch;
	DevDat[114].d_acts[A_INX] = Wrapper_DCP_1_6_Init;
	DevDat[114].d_acts[A_LOD] = Wrapper_DCP_1_6_Load;
	DevDat[114].d_acts[A_OPN] = Wrapper_DCP_1_6_Open;
	DevDat[114].d_acts[A_RST] = Wrapper_DCP_1_6_Reset;
	DevDat[114].d_acts[A_FNC] = Wrapper_DCP_1_6_Setup;
	DevDat[114].d_acts[A_STA] = Wrapper_DCP_1_6_Status;
//
//	DCP_1:CH67
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[115].d_modlst = p_mod;
	DevDat[115].d_fncP = 67;
	DevDat[115].d_acts[A_CLS] = Wrapper_DCP_1_67_Close;
	DevDat[115].d_acts[A_CON] = Wrapper_DCP_1_67_Connect;
	DevDat[115].d_acts[A_DIS] = Wrapper_DCP_1_67_Disconnect;
	DevDat[115].d_acts[A_FTH] = Wrapper_DCP_1_67_Fetch;
	DevDat[115].d_acts[A_INX] = Wrapper_DCP_1_67_Init;
	DevDat[115].d_acts[A_LOD] = Wrapper_DCP_1_67_Load;
	DevDat[115].d_acts[A_OPN] = Wrapper_DCP_1_67_Open;
	DevDat[115].d_acts[A_RST] = Wrapper_DCP_1_67_Reset;
	DevDat[115].d_acts[A_FNC] = Wrapper_DCP_1_67_Setup;
	DevDat[115].d_acts[A_STA] = Wrapper_DCP_1_67_Status;
//
//	DCP_1:CH68
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[116].d_modlst = p_mod;
	DevDat[116].d_fncP = 68;
	DevDat[116].d_acts[A_CLS] = Wrapper_DCP_1_68_Close;
	DevDat[116].d_acts[A_CON] = Wrapper_DCP_1_68_Connect;
	DevDat[116].d_acts[A_DIS] = Wrapper_DCP_1_68_Disconnect;
	DevDat[116].d_acts[A_FTH] = Wrapper_DCP_1_68_Fetch;
	DevDat[116].d_acts[A_INX] = Wrapper_DCP_1_68_Init;
	DevDat[116].d_acts[A_LOD] = Wrapper_DCP_1_68_Load;
	DevDat[116].d_acts[A_OPN] = Wrapper_DCP_1_68_Open;
	DevDat[116].d_acts[A_RST] = Wrapper_DCP_1_68_Reset;
	DevDat[116].d_acts[A_FNC] = Wrapper_DCP_1_68_Setup;
	DevDat[116].d_acts[A_STA] = Wrapper_DCP_1_68_Status;
//
//	DCP_1:CH69
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[117].d_modlst = p_mod;
	DevDat[117].d_fncP = 69;
	DevDat[117].d_acts[A_CLS] = Wrapper_DCP_1_69_Close;
	DevDat[117].d_acts[A_CON] = Wrapper_DCP_1_69_Connect;
	DevDat[117].d_acts[A_DIS] = Wrapper_DCP_1_69_Disconnect;
	DevDat[117].d_acts[A_FTH] = Wrapper_DCP_1_69_Fetch;
	DevDat[117].d_acts[A_INX] = Wrapper_DCP_1_69_Init;
	DevDat[117].d_acts[A_LOD] = Wrapper_DCP_1_69_Load;
	DevDat[117].d_acts[A_OPN] = Wrapper_DCP_1_69_Open;
	DevDat[117].d_acts[A_RST] = Wrapper_DCP_1_69_Reset;
	DevDat[117].d_acts[A_FNC] = Wrapper_DCP_1_69_Setup;
	DevDat[117].d_acts[A_STA] = Wrapper_DCP_1_69_Status;
//
//	DCP_1:CH7
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[118].d_modlst = p_mod;
	DevDat[118].d_fncP = 7;
	DevDat[118].d_acts[A_CLS] = Wrapper_DCP_1_7_Close;
	DevDat[118].d_acts[A_CON] = Wrapper_DCP_1_7_Connect;
	DevDat[118].d_acts[A_DIS] = Wrapper_DCP_1_7_Disconnect;
	DevDat[118].d_acts[A_FTH] = Wrapper_DCP_1_7_Fetch;
	DevDat[118].d_acts[A_INX] = Wrapper_DCP_1_7_Init;
	DevDat[118].d_acts[A_LOD] = Wrapper_DCP_1_7_Load;
	DevDat[118].d_acts[A_OPN] = Wrapper_DCP_1_7_Open;
	DevDat[118].d_acts[A_RST] = Wrapper_DCP_1_7_Reset;
	DevDat[118].d_acts[A_FNC] = Wrapper_DCP_1_7_Setup;
	DevDat[118].d_acts[A_STA] = Wrapper_DCP_1_7_Status;
//
//	DCP_1:CH78
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[119].d_modlst = p_mod;
	DevDat[119].d_fncP = 78;
	DevDat[119].d_acts[A_CLS] = Wrapper_DCP_1_78_Close;
	DevDat[119].d_acts[A_CON] = Wrapper_DCP_1_78_Connect;
	DevDat[119].d_acts[A_DIS] = Wrapper_DCP_1_78_Disconnect;
	DevDat[119].d_acts[A_FTH] = Wrapper_DCP_1_78_Fetch;
	DevDat[119].d_acts[A_INX] = Wrapper_DCP_1_78_Init;
	DevDat[119].d_acts[A_LOD] = Wrapper_DCP_1_78_Load;
	DevDat[119].d_acts[A_OPN] = Wrapper_DCP_1_78_Open;
	DevDat[119].d_acts[A_RST] = Wrapper_DCP_1_78_Reset;
	DevDat[119].d_acts[A_FNC] = Wrapper_DCP_1_78_Setup;
	DevDat[119].d_acts[A_STA] = Wrapper_DCP_1_78_Status;
//
//	DCP_1:CH79
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[120].d_modlst = p_mod;
	DevDat[120].d_fncP = 79;
	DevDat[120].d_acts[A_CLS] = Wrapper_DCP_1_79_Close;
	DevDat[120].d_acts[A_CON] = Wrapper_DCP_1_79_Connect;
	DevDat[120].d_acts[A_DIS] = Wrapper_DCP_1_79_Disconnect;
	DevDat[120].d_acts[A_FTH] = Wrapper_DCP_1_79_Fetch;
	DevDat[120].d_acts[A_INX] = Wrapper_DCP_1_79_Init;
	DevDat[120].d_acts[A_LOD] = Wrapper_DCP_1_79_Load;
	DevDat[120].d_acts[A_OPN] = Wrapper_DCP_1_79_Open;
	DevDat[120].d_acts[A_RST] = Wrapper_DCP_1_79_Reset;
	DevDat[120].d_acts[A_FNC] = Wrapper_DCP_1_79_Setup;
	DevDat[120].d_acts[A_STA] = Wrapper_DCP_1_79_Status;
//
//	DCP_1:CH8
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[121].d_modlst = p_mod;
	DevDat[121].d_fncP = 8;
	DevDat[121].d_acts[A_CLS] = Wrapper_DCP_1_8_Close;
	DevDat[121].d_acts[A_CON] = Wrapper_DCP_1_8_Connect;
	DevDat[121].d_acts[A_DIS] = Wrapper_DCP_1_8_Disconnect;
	DevDat[121].d_acts[A_FTH] = Wrapper_DCP_1_8_Fetch;
	DevDat[121].d_acts[A_INX] = Wrapper_DCP_1_8_Init;
	DevDat[121].d_acts[A_LOD] = Wrapper_DCP_1_8_Load;
	DevDat[121].d_acts[A_OPN] = Wrapper_DCP_1_8_Open;
	DevDat[121].d_acts[A_RST] = Wrapper_DCP_1_8_Reset;
	DevDat[121].d_acts[A_FNC] = Wrapper_DCP_1_8_Setup;
	DevDat[121].d_acts[A_STA] = Wrapper_DCP_1_8_Status;
//
//	DCP_1:CH89
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[122].d_modlst = p_mod;
	DevDat[122].d_fncP = 89;
	DevDat[122].d_acts[A_CLS] = Wrapper_DCP_1_89_Close;
	DevDat[122].d_acts[A_CON] = Wrapper_DCP_1_89_Connect;
	DevDat[122].d_acts[A_DIS] = Wrapper_DCP_1_89_Disconnect;
	DevDat[122].d_acts[A_FTH] = Wrapper_DCP_1_89_Fetch;
	DevDat[122].d_acts[A_INX] = Wrapper_DCP_1_89_Init;
	DevDat[122].d_acts[A_LOD] = Wrapper_DCP_1_89_Load;
	DevDat[122].d_acts[A_OPN] = Wrapper_DCP_1_89_Open;
	DevDat[122].d_acts[A_RST] = Wrapper_DCP_1_89_Reset;
	DevDat[122].d_acts[A_FNC] = Wrapper_DCP_1_89_Setup;
	DevDat[122].d_acts[A_STA] = Wrapper_DCP_1_89_Status;
//
//	DCP_1:CH9
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[123].d_modlst = p_mod;
	DevDat[123].d_fncP = 9;
	DevDat[123].d_acts[A_CLS] = Wrapper_DCP_1_9_Close;
	DevDat[123].d_acts[A_CON] = Wrapper_DCP_1_9_Connect;
	DevDat[123].d_acts[A_DIS] = Wrapper_DCP_1_9_Disconnect;
	DevDat[123].d_acts[A_FTH] = Wrapper_DCP_1_9_Fetch;
	DevDat[123].d_acts[A_INX] = Wrapper_DCP_1_9_Init;
	DevDat[123].d_acts[A_LOD] = Wrapper_DCP_1_9_Load;
	DevDat[123].d_acts[A_OPN] = Wrapper_DCP_1_9_Open;
	DevDat[123].d_acts[A_RST] = Wrapper_DCP_1_9_Reset;
	DevDat[123].d_acts[A_FNC] = Wrapper_DCP_1_9_Setup;
	DevDat[123].d_acts[A_STA] = Wrapper_DCP_1_9_Status;
//
//	DCP_1:CH92
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[124].d_modlst = p_mod;
	DevDat[124].d_fncP = 92;
	DevDat[124].d_acts[A_CLS] = Wrapper_DCP_1_92_Close;
	DevDat[124].d_acts[A_CON] = Wrapper_DCP_1_92_Connect;
	DevDat[124].d_acts[A_DIS] = Wrapper_DCP_1_92_Disconnect;
	DevDat[124].d_acts[A_FTH] = Wrapper_DCP_1_92_Fetch;
	DevDat[124].d_acts[A_INX] = Wrapper_DCP_1_92_Init;
	DevDat[124].d_acts[A_LOD] = Wrapper_DCP_1_92_Load;
	DevDat[124].d_acts[A_OPN] = Wrapper_DCP_1_92_Open;
	DevDat[124].d_acts[A_RST] = Wrapper_DCP_1_92_Reset;
	DevDat[124].d_acts[A_FNC] = Wrapper_DCP_1_92_Setup;
	DevDat[124].d_acts[A_STA] = Wrapper_DCP_1_92_Status;
//
//	DCP_1:CH93
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[125].d_modlst = p_mod;
	DevDat[125].d_fncP = 93;
	DevDat[125].d_acts[A_CLS] = Wrapper_DCP_1_93_Close;
	DevDat[125].d_acts[A_CON] = Wrapper_DCP_1_93_Connect;
	DevDat[125].d_acts[A_DIS] = Wrapper_DCP_1_93_Disconnect;
	DevDat[125].d_acts[A_FTH] = Wrapper_DCP_1_93_Fetch;
	DevDat[125].d_acts[A_INX] = Wrapper_DCP_1_93_Init;
	DevDat[125].d_acts[A_LOD] = Wrapper_DCP_1_93_Load;
	DevDat[125].d_acts[A_OPN] = Wrapper_DCP_1_93_Open;
	DevDat[125].d_acts[A_RST] = Wrapper_DCP_1_93_Reset;
	DevDat[125].d_acts[A_FNC] = Wrapper_DCP_1_93_Setup;
	DevDat[125].d_acts[A_STA] = Wrapper_DCP_1_93_Status;
//
//	DCP_1:CH94
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[126].d_modlst = p_mod;
	DevDat[126].d_fncP = 94;
	DevDat[126].d_acts[A_CLS] = Wrapper_DCP_1_94_Close;
	DevDat[126].d_acts[A_CON] = Wrapper_DCP_1_94_Connect;
	DevDat[126].d_acts[A_DIS] = Wrapper_DCP_1_94_Disconnect;
	DevDat[126].d_acts[A_FTH] = Wrapper_DCP_1_94_Fetch;
	DevDat[126].d_acts[A_INX] = Wrapper_DCP_1_94_Init;
	DevDat[126].d_acts[A_LOD] = Wrapper_DCP_1_94_Load;
	DevDat[126].d_acts[A_OPN] = Wrapper_DCP_1_94_Open;
	DevDat[126].d_acts[A_RST] = Wrapper_DCP_1_94_Reset;
	DevDat[126].d_acts[A_FNC] = Wrapper_DCP_1_94_Setup;
	DevDat[126].d_acts[A_STA] = Wrapper_DCP_1_94_Status;
//
//	DCP_1:CH95
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[127].d_modlst = p_mod;
	DevDat[127].d_fncP = 95;
	DevDat[127].d_acts[A_CLS] = Wrapper_DCP_1_95_Close;
	DevDat[127].d_acts[A_CON] = Wrapper_DCP_1_95_Connect;
	DevDat[127].d_acts[A_DIS] = Wrapper_DCP_1_95_Disconnect;
	DevDat[127].d_acts[A_FTH] = Wrapper_DCP_1_95_Fetch;
	DevDat[127].d_acts[A_INX] = Wrapper_DCP_1_95_Init;
	DevDat[127].d_acts[A_LOD] = Wrapper_DCP_1_95_Load;
	DevDat[127].d_acts[A_OPN] = Wrapper_DCP_1_95_Open;
	DevDat[127].d_acts[A_RST] = Wrapper_DCP_1_95_Reset;
	DevDat[127].d_acts[A_FNC] = Wrapper_DCP_1_95_Setup;
	DevDat[127].d_acts[A_STA] = Wrapper_DCP_1_95_Status;
//
//	DCP_1:CH96
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[128].d_modlst = p_mod;
	DevDat[128].d_fncP = 96;
	DevDat[128].d_acts[A_CLS] = Wrapper_DCP_1_96_Close;
	DevDat[128].d_acts[A_CON] = Wrapper_DCP_1_96_Connect;
	DevDat[128].d_acts[A_DIS] = Wrapper_DCP_1_96_Disconnect;
	DevDat[128].d_acts[A_FTH] = Wrapper_DCP_1_96_Fetch;
	DevDat[128].d_acts[A_INX] = Wrapper_DCP_1_96_Init;
	DevDat[128].d_acts[A_LOD] = Wrapper_DCP_1_96_Load;
	DevDat[128].d_acts[A_OPN] = Wrapper_DCP_1_96_Open;
	DevDat[128].d_acts[A_RST] = Wrapper_DCP_1_96_Reset;
	DevDat[128].d_acts[A_FNC] = Wrapper_DCP_1_96_Setup;
	DevDat[128].d_acts[A_STA] = Wrapper_DCP_1_96_Status;
//
//	DCP_1:CH97
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[129].d_modlst = p_mod;
	DevDat[129].d_fncP = 97;
	DevDat[129].d_acts[A_CLS] = Wrapper_DCP_1_97_Close;
	DevDat[129].d_acts[A_CON] = Wrapper_DCP_1_97_Connect;
	DevDat[129].d_acts[A_DIS] = Wrapper_DCP_1_97_Disconnect;
	DevDat[129].d_acts[A_FTH] = Wrapper_DCP_1_97_Fetch;
	DevDat[129].d_acts[A_INX] = Wrapper_DCP_1_97_Init;
	DevDat[129].d_acts[A_LOD] = Wrapper_DCP_1_97_Load;
	DevDat[129].d_acts[A_OPN] = Wrapper_DCP_1_97_Open;
	DevDat[129].d_acts[A_RST] = Wrapper_DCP_1_97_Reset;
	DevDat[129].d_acts[A_FNC] = Wrapper_DCP_1_97_Setup;
	DevDat[129].d_acts[A_STA] = Wrapper_DCP_1_97_Status;
//
//	DCP_1:CH98
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[130].d_modlst = p_mod;
	DevDat[130].d_fncP = 98;
	DevDat[130].d_acts[A_CLS] = Wrapper_DCP_1_98_Close;
	DevDat[130].d_acts[A_CON] = Wrapper_DCP_1_98_Connect;
	DevDat[130].d_acts[A_DIS] = Wrapper_DCP_1_98_Disconnect;
	DevDat[130].d_acts[A_FTH] = Wrapper_DCP_1_98_Fetch;
	DevDat[130].d_acts[A_INX] = Wrapper_DCP_1_98_Init;
	DevDat[130].d_acts[A_LOD] = Wrapper_DCP_1_98_Load;
	DevDat[130].d_acts[A_OPN] = Wrapper_DCP_1_98_Open;
	DevDat[130].d_acts[A_RST] = Wrapper_DCP_1_98_Reset;
	DevDat[130].d_acts[A_FNC] = Wrapper_DCP_1_98_Setup;
	DevDat[130].d_acts[A_STA] = Wrapper_DCP_1_98_Status;
//
//	DCP_1:CH99
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CURR);  // current
	p_mod = BldModDat (p_mod, (short) M_CURL);  // current-lmt
	p_mod = BldModDat (p_mod, (short) M_VLTL);  // volt-lmt
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[131].d_modlst = p_mod;
	DevDat[131].d_fncP = 99;
	DevDat[131].d_acts[A_CLS] = Wrapper_DCP_1_99_Close;
	DevDat[131].d_acts[A_CON] = Wrapper_DCP_1_99_Connect;
	DevDat[131].d_acts[A_DIS] = Wrapper_DCP_1_99_Disconnect;
	DevDat[131].d_acts[A_FTH] = Wrapper_DCP_1_99_Fetch;
	DevDat[131].d_acts[A_INX] = Wrapper_DCP_1_99_Init;
	DevDat[131].d_acts[A_LOD] = Wrapper_DCP_1_99_Load;
	DevDat[131].d_acts[A_OPN] = Wrapper_DCP_1_99_Open;
	DevDat[131].d_acts[A_RST] = Wrapper_DCP_1_99_Reset;
	DevDat[131].d_acts[A_FNC] = Wrapper_DCP_1_99_Setup;
	DevDat[131].d_acts[A_STA] = Wrapper_DCP_1_99_Status;
	return 0;
}
