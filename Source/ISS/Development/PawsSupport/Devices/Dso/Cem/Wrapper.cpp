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
extern int doDSO_Close ();
extern int doDSO_Connect ();
extern int doDSO_Disconnect ();
extern int doDSO_Fetch ();
extern int doDSO_Init ();
extern int doDSO_Load ();
extern int doDSO_Open ();
extern int doDSO_Reset ();
extern int doDSO_Setup ();
extern int doDSO_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"DSO_1:CH1",
	"DSO_1:CH101",
	"DSO_1:CH102",
	"DSO_1:CH103",
	"DSO_1:CH104",
	"DSO_1:CH105",
	"DSO_1:CH106",
	"DSO_1:CH107",
	"DSO_1:CH108",
	"DSO_1:CH109",
	"DSO_1:CH11",
	"DSO_1:CH110",
	"DSO_1:CH111",
	"DSO_1:CH112",
	"DSO_1:CH113",
	"DSO_1:CH114",
	"DSO_1:CH115",
	"DSO_1:CH116",
	"DSO_1:CH117",
	"DSO_1:CH118",
	"DSO_1:CH119",
	"DSO_1:CH12",
	"DSO_1:CH120",
	"DSO_1:CH121",
	"DSO_1:CH122",
	"DSO_1:CH123",
	"DSO_1:CH124",
	"DSO_1:CH125",
	"DSO_1:CH126",
	"DSO_1:CH127",
	"DSO_1:CH128",
	"DSO_1:CH129",
	"DSO_1:CH13",
	"DSO_1:CH132",
	"DSO_1:CH133",
	"DSO_1:CH134",
	"DSO_1:CH135",
	"DSO_1:CH136",
	"DSO_1:CH137",
	"DSO_1:CH138",
	"DSO_1:CH139",
	"DSO_1:CH14",
	"DSO_1:CH140",
	"DSO_1:CH142",
	"DSO_1:CH143",
	"DSO_1:CH144",
	"DSO_1:CH145",
	"DSO_1:CH146",
	"DSO_1:CH147",
	"DSO_1:CH148",
	"DSO_1:CH149",
	"DSO_1:CH150",
	"DSO_1:CH152",
	"DSO_1:CH153",
	"DSO_1:CH154",
	"DSO_1:CH155",
	"DSO_1:CH156",
	"DSO_1:CH157",
	"DSO_1:CH158",
	"DSO_1:CH159",
	"DSO_1:CH160",
	"DSO_1:CH161",
	"DSO_1:CH162",
	"DSO_1:CH163",
	"DSO_1:CH2",
	"DSO_1:CH201",
	"DSO_1:CH202",
	"DSO_1:CH203",
	"DSO_1:CH204",
	"DSO_1:CH205",
	"DSO_1:CH206",
	"DSO_1:CH207",
	"DSO_1:CH208",
	"DSO_1:CH209",
	"DSO_1:CH210",
	"DSO_1:CH211",
	"DSO_1:CH212",
	"DSO_1:CH213",
	"DSO_1:CH214",
	"DSO_1:CH215",
	"DSO_1:CH216",
	"DSO_1:CH217",
	"DSO_1:CH218",
	"DSO_1:CH219",
	"DSO_1:CH220",
	"DSO_1:CH221",
	"DSO_1:CH222",
	"DSO_1:CH223",
	"DSO_1:CH224",
	"DSO_1:CH225",
	"DSO_1:CH226",
	"DSO_1:CH227",
	"DSO_1:CH228",
	"DSO_1:CH229",
	"DSO_1:CH230",
	"DSO_1:CH232",
	"DSO_1:CH233",
	"DSO_1:CH234",
	"DSO_1:CH235",
	"DSO_1:CH236",
	"DSO_1:CH237",
	"DSO_1:CH238",
	"DSO_1:CH239",
	"DSO_1:CH240",
	"DSO_1:CH241",
	"DSO_1:CH242",
	"DSO_1:CH243",
	"DSO_1:CH244",
	"DSO_1:CH245",
	"DSO_1:CH246",
	"DSO_1:CH247",
	"DSO_1:CH248",
	"DSO_1:CH249",
	"DSO_1:CH250",
	"DSO_1:CH251",
	"DSO_1:CH252",
	"DSO_1:CH253",
	"DSO_1:CH254",
	"DSO_1:CH255",
	"DSO_1:CH3",
	"DSO_1:CH4",
	"DSO_1:CH5",
	"DSO_1:CH6",
	"DSO_1:CH7",
};
DECLAREC int DevCnt = 126;
int CCALLBACK Wrapper_DSO_1_1_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_1_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_101_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_102_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_103_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_104_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_105_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_106_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_107_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_108_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_109_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_11_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_110_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_111_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_112_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_113_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_114_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_115_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_116_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_117_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_118_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_119_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_12_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_120_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_121_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_122_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_123_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_124_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_125_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_126_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_127_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_128_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_129_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_13_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_132_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_133_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_134_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_135_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_136_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_137_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_138_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_139_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_14_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_140_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_142_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_143_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_144_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_145_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_146_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_147_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_148_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_149_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_150_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_152_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_153_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_154_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_155_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_156_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_157_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_158_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_159_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_160_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_161_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_162_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_163_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_2_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_201_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_202_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_203_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_204_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_205_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_206_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_207_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_208_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_209_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_210_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_211_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_212_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_213_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_214_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_215_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_216_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_217_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_218_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_219_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_220_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_221_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_222_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_223_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_224_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_225_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_226_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_227_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_228_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_229_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_230_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_232_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_233_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_234_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_235_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_236_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_237_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_238_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_239_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_240_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_241_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_242_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_243_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_244_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_245_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_246_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_247_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_248_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_249_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_250_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_251_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_252_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_253_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_254_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_255_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_3_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_4_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_5_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_6_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Close(void)
{
	if (doDSO_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Connect(void)
{
	if (doDSO_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Disconnect(void)
{
	if (doDSO_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Fetch(void)
{
	if (doDSO_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Init(void)
{
	if (doDSO_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Load(void)
{
	if (doDSO_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Open(void)
{
	if (doDSO_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Reset(void)
{
	if (doDSO_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Setup(void)
{
	if (doDSO_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DSO_1_7_Status(void)
{
	if (doDSO_Status() < 0)
		BusErr ("DSO_1");
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
//	DSO_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SBFM);  // subtract-from
	p_mod = BldModDat (p_mod, (short) M_SBTO);  // subtract-to
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_DSO_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_DSO_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_DSO_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_DSO_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_DSO_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_DSO_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_DSO_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_DSO_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_DSO_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_DSO_1_1_Status;
//
//	DSO_1:CH101
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 101;
	DevDat[3].d_acts[A_CLS] = Wrapper_DSO_1_101_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_DSO_1_101_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_DSO_1_101_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_DSO_1_101_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_DSO_1_101_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_DSO_1_101_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_DSO_1_101_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_DSO_1_101_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_DSO_1_101_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_DSO_1_101_Status;
//
//	DSO_1:CH102
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 102;
	DevDat[4].d_acts[A_CLS] = Wrapper_DSO_1_102_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_DSO_1_102_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_DSO_1_102_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_DSO_1_102_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_DSO_1_102_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_DSO_1_102_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_DSO_1_102_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_DSO_1_102_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_DSO_1_102_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_DSO_1_102_Status;
//
//	DSO_1:CH103
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 103;
	DevDat[5].d_acts[A_CLS] = Wrapper_DSO_1_103_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_DSO_1_103_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_DSO_1_103_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_DSO_1_103_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_DSO_1_103_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_DSO_1_103_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_DSO_1_103_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_DSO_1_103_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_DSO_1_103_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_DSO_1_103_Status;
//
//	DSO_1:CH104
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 104;
	DevDat[6].d_acts[A_CLS] = Wrapper_DSO_1_104_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_DSO_1_104_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_DSO_1_104_Disconnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_DSO_1_104_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_DSO_1_104_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_DSO_1_104_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_DSO_1_104_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_DSO_1_104_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_DSO_1_104_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_DSO_1_104_Status;
//
//	DSO_1:CH105
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 105;
	DevDat[7].d_acts[A_CLS] = Wrapper_DSO_1_105_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_DSO_1_105_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_DSO_1_105_Disconnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_DSO_1_105_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_DSO_1_105_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_DSO_1_105_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_DSO_1_105_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_DSO_1_105_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_DSO_1_105_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_DSO_1_105_Status;
//
//	DSO_1:CH106
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 106;
	DevDat[8].d_acts[A_CLS] = Wrapper_DSO_1_106_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_DSO_1_106_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_DSO_1_106_Disconnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_DSO_1_106_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_DSO_1_106_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_DSO_1_106_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_DSO_1_106_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_DSO_1_106_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_DSO_1_106_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_DSO_1_106_Status;
//
//	DSO_1:CH107
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 107;
	DevDat[9].d_acts[A_CLS] = Wrapper_DSO_1_107_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_DSO_1_107_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_DSO_1_107_Disconnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_DSO_1_107_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_DSO_1_107_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_DSO_1_107_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_DSO_1_107_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_DSO_1_107_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_DSO_1_107_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_DSO_1_107_Status;
//
//	DSO_1:CH108
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 108;
	DevDat[10].d_acts[A_CLS] = Wrapper_DSO_1_108_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_DSO_1_108_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_DSO_1_108_Disconnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_DSO_1_108_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_DSO_1_108_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_DSO_1_108_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_DSO_1_108_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_DSO_1_108_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_DSO_1_108_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_DSO_1_108_Status;
//
//	DSO_1:CH109
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 109;
	DevDat[11].d_acts[A_CLS] = Wrapper_DSO_1_109_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_DSO_1_109_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_DSO_1_109_Disconnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_DSO_1_109_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_DSO_1_109_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_DSO_1_109_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_DSO_1_109_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_DSO_1_109_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_DSO_1_109_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_DSO_1_109_Status;
//
//	DSO_1:CH11
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 11;
	DevDat[12].d_acts[A_CLS] = Wrapper_DSO_1_11_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_DSO_1_11_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_DSO_1_11_Disconnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_DSO_1_11_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_DSO_1_11_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_DSO_1_11_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_DSO_1_11_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_DSO_1_11_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_DSO_1_11_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_DSO_1_11_Status;
//
//	DSO_1:CH110
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 110;
	DevDat[13].d_acts[A_CLS] = Wrapper_DSO_1_110_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_DSO_1_110_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_DSO_1_110_Disconnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_DSO_1_110_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_DSO_1_110_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_DSO_1_110_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_DSO_1_110_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_DSO_1_110_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_DSO_1_110_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_DSO_1_110_Status;
//
//	DSO_1:CH111
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 111;
	DevDat[14].d_acts[A_CLS] = Wrapper_DSO_1_111_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_DSO_1_111_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_DSO_1_111_Disconnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_DSO_1_111_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_DSO_1_111_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_DSO_1_111_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_DSO_1_111_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_DSO_1_111_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_DSO_1_111_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_DSO_1_111_Status;
//
//	DSO_1:CH112
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 112;
	DevDat[15].d_acts[A_CLS] = Wrapper_DSO_1_112_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_DSO_1_112_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_DSO_1_112_Disconnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_DSO_1_112_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_DSO_1_112_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_DSO_1_112_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_DSO_1_112_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_DSO_1_112_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_DSO_1_112_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_DSO_1_112_Status;
//
//	DSO_1:CH113
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 113;
	DevDat[16].d_acts[A_CLS] = Wrapper_DSO_1_113_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_DSO_1_113_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_DSO_1_113_Disconnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_DSO_1_113_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_DSO_1_113_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_DSO_1_113_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_DSO_1_113_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_DSO_1_113_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_DSO_1_113_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_DSO_1_113_Status;
//
//	DSO_1:CH114
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 114;
	DevDat[17].d_acts[A_CLS] = Wrapper_DSO_1_114_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_DSO_1_114_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_DSO_1_114_Disconnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_DSO_1_114_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_DSO_1_114_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_DSO_1_114_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_DSO_1_114_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_DSO_1_114_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_DSO_1_114_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_DSO_1_114_Status;
//
//	DSO_1:CH115
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[18].d_modlst = p_mod;
	DevDat[18].d_fncP = 115;
	DevDat[18].d_acts[A_CLS] = Wrapper_DSO_1_115_Close;
	DevDat[18].d_acts[A_CON] = Wrapper_DSO_1_115_Connect;
	DevDat[18].d_acts[A_DIS] = Wrapper_DSO_1_115_Disconnect;
	DevDat[18].d_acts[A_FTH] = Wrapper_DSO_1_115_Fetch;
	DevDat[18].d_acts[A_INX] = Wrapper_DSO_1_115_Init;
	DevDat[18].d_acts[A_LOD] = Wrapper_DSO_1_115_Load;
	DevDat[18].d_acts[A_OPN] = Wrapper_DSO_1_115_Open;
	DevDat[18].d_acts[A_RST] = Wrapper_DSO_1_115_Reset;
	DevDat[18].d_acts[A_FNC] = Wrapper_DSO_1_115_Setup;
	DevDat[18].d_acts[A_STA] = Wrapper_DSO_1_115_Status;
//
//	DSO_1:CH116
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[19].d_modlst = p_mod;
	DevDat[19].d_fncP = 116;
	DevDat[19].d_acts[A_CLS] = Wrapper_DSO_1_116_Close;
	DevDat[19].d_acts[A_CON] = Wrapper_DSO_1_116_Connect;
	DevDat[19].d_acts[A_DIS] = Wrapper_DSO_1_116_Disconnect;
	DevDat[19].d_acts[A_FTH] = Wrapper_DSO_1_116_Fetch;
	DevDat[19].d_acts[A_INX] = Wrapper_DSO_1_116_Init;
	DevDat[19].d_acts[A_LOD] = Wrapper_DSO_1_116_Load;
	DevDat[19].d_acts[A_OPN] = Wrapper_DSO_1_116_Open;
	DevDat[19].d_acts[A_RST] = Wrapper_DSO_1_116_Reset;
	DevDat[19].d_acts[A_FNC] = Wrapper_DSO_1_116_Setup;
	DevDat[19].d_acts[A_STA] = Wrapper_DSO_1_116_Status;
//
//	DSO_1:CH117
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[20].d_modlst = p_mod;
	DevDat[20].d_fncP = 117;
	DevDat[20].d_acts[A_CLS] = Wrapper_DSO_1_117_Close;
	DevDat[20].d_acts[A_CON] = Wrapper_DSO_1_117_Connect;
	DevDat[20].d_acts[A_DIS] = Wrapper_DSO_1_117_Disconnect;
	DevDat[20].d_acts[A_FTH] = Wrapper_DSO_1_117_Fetch;
	DevDat[20].d_acts[A_INX] = Wrapper_DSO_1_117_Init;
	DevDat[20].d_acts[A_LOD] = Wrapper_DSO_1_117_Load;
	DevDat[20].d_acts[A_OPN] = Wrapper_DSO_1_117_Open;
	DevDat[20].d_acts[A_RST] = Wrapper_DSO_1_117_Reset;
	DevDat[20].d_acts[A_FNC] = Wrapper_DSO_1_117_Setup;
	DevDat[20].d_acts[A_STA] = Wrapper_DSO_1_117_Status;
//
//	DSO_1:CH118
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[21].d_modlst = p_mod;
	DevDat[21].d_fncP = 118;
	DevDat[21].d_acts[A_CLS] = Wrapper_DSO_1_118_Close;
	DevDat[21].d_acts[A_CON] = Wrapper_DSO_1_118_Connect;
	DevDat[21].d_acts[A_DIS] = Wrapper_DSO_1_118_Disconnect;
	DevDat[21].d_acts[A_FTH] = Wrapper_DSO_1_118_Fetch;
	DevDat[21].d_acts[A_INX] = Wrapper_DSO_1_118_Init;
	DevDat[21].d_acts[A_LOD] = Wrapper_DSO_1_118_Load;
	DevDat[21].d_acts[A_OPN] = Wrapper_DSO_1_118_Open;
	DevDat[21].d_acts[A_RST] = Wrapper_DSO_1_118_Reset;
	DevDat[21].d_acts[A_FNC] = Wrapper_DSO_1_118_Setup;
	DevDat[21].d_acts[A_STA] = Wrapper_DSO_1_118_Status;
//
//	DSO_1:CH119
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[22].d_modlst = p_mod;
	DevDat[22].d_fncP = 119;
	DevDat[22].d_acts[A_CLS] = Wrapper_DSO_1_119_Close;
	DevDat[22].d_acts[A_CON] = Wrapper_DSO_1_119_Connect;
	DevDat[22].d_acts[A_DIS] = Wrapper_DSO_1_119_Disconnect;
	DevDat[22].d_acts[A_FTH] = Wrapper_DSO_1_119_Fetch;
	DevDat[22].d_acts[A_INX] = Wrapper_DSO_1_119_Init;
	DevDat[22].d_acts[A_LOD] = Wrapper_DSO_1_119_Load;
	DevDat[22].d_acts[A_OPN] = Wrapper_DSO_1_119_Open;
	DevDat[22].d_acts[A_RST] = Wrapper_DSO_1_119_Reset;
	DevDat[22].d_acts[A_FNC] = Wrapper_DSO_1_119_Setup;
	DevDat[22].d_acts[A_STA] = Wrapper_DSO_1_119_Status;
//
//	DSO_1:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[23].d_modlst = p_mod;
	DevDat[23].d_fncP = 12;
	DevDat[23].d_acts[A_CLS] = Wrapper_DSO_1_12_Close;
	DevDat[23].d_acts[A_CON] = Wrapper_DSO_1_12_Connect;
	DevDat[23].d_acts[A_DIS] = Wrapper_DSO_1_12_Disconnect;
	DevDat[23].d_acts[A_FTH] = Wrapper_DSO_1_12_Fetch;
	DevDat[23].d_acts[A_INX] = Wrapper_DSO_1_12_Init;
	DevDat[23].d_acts[A_LOD] = Wrapper_DSO_1_12_Load;
	DevDat[23].d_acts[A_OPN] = Wrapper_DSO_1_12_Open;
	DevDat[23].d_acts[A_RST] = Wrapper_DSO_1_12_Reset;
	DevDat[23].d_acts[A_FNC] = Wrapper_DSO_1_12_Setup;
	DevDat[23].d_acts[A_STA] = Wrapper_DSO_1_12_Status;
//
//	DSO_1:CH120
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[24].d_modlst = p_mod;
	DevDat[24].d_fncP = 120;
	DevDat[24].d_acts[A_CLS] = Wrapper_DSO_1_120_Close;
	DevDat[24].d_acts[A_CON] = Wrapper_DSO_1_120_Connect;
	DevDat[24].d_acts[A_DIS] = Wrapper_DSO_1_120_Disconnect;
	DevDat[24].d_acts[A_FTH] = Wrapper_DSO_1_120_Fetch;
	DevDat[24].d_acts[A_INX] = Wrapper_DSO_1_120_Init;
	DevDat[24].d_acts[A_LOD] = Wrapper_DSO_1_120_Load;
	DevDat[24].d_acts[A_OPN] = Wrapper_DSO_1_120_Open;
	DevDat[24].d_acts[A_RST] = Wrapper_DSO_1_120_Reset;
	DevDat[24].d_acts[A_FNC] = Wrapper_DSO_1_120_Setup;
	DevDat[24].d_acts[A_STA] = Wrapper_DSO_1_120_Status;
//
//	DSO_1:CH121
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[25].d_modlst = p_mod;
	DevDat[25].d_fncP = 121;
	DevDat[25].d_acts[A_CLS] = Wrapper_DSO_1_121_Close;
	DevDat[25].d_acts[A_CON] = Wrapper_DSO_1_121_Connect;
	DevDat[25].d_acts[A_DIS] = Wrapper_DSO_1_121_Disconnect;
	DevDat[25].d_acts[A_FTH] = Wrapper_DSO_1_121_Fetch;
	DevDat[25].d_acts[A_INX] = Wrapper_DSO_1_121_Init;
	DevDat[25].d_acts[A_LOD] = Wrapper_DSO_1_121_Load;
	DevDat[25].d_acts[A_OPN] = Wrapper_DSO_1_121_Open;
	DevDat[25].d_acts[A_RST] = Wrapper_DSO_1_121_Reset;
	DevDat[25].d_acts[A_FNC] = Wrapper_DSO_1_121_Setup;
	DevDat[25].d_acts[A_STA] = Wrapper_DSO_1_121_Status;
//
//	DSO_1:CH122
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[26].d_modlst = p_mod;
	DevDat[26].d_fncP = 122;
	DevDat[26].d_acts[A_CLS] = Wrapper_DSO_1_122_Close;
	DevDat[26].d_acts[A_CON] = Wrapper_DSO_1_122_Connect;
	DevDat[26].d_acts[A_DIS] = Wrapper_DSO_1_122_Disconnect;
	DevDat[26].d_acts[A_FTH] = Wrapper_DSO_1_122_Fetch;
	DevDat[26].d_acts[A_INX] = Wrapper_DSO_1_122_Init;
	DevDat[26].d_acts[A_LOD] = Wrapper_DSO_1_122_Load;
	DevDat[26].d_acts[A_OPN] = Wrapper_DSO_1_122_Open;
	DevDat[26].d_acts[A_RST] = Wrapper_DSO_1_122_Reset;
	DevDat[26].d_acts[A_FNC] = Wrapper_DSO_1_122_Setup;
	DevDat[26].d_acts[A_STA] = Wrapper_DSO_1_122_Status;
//
//	DSO_1:CH123
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[27].d_modlst = p_mod;
	DevDat[27].d_fncP = 123;
	DevDat[27].d_acts[A_CLS] = Wrapper_DSO_1_123_Close;
	DevDat[27].d_acts[A_CON] = Wrapper_DSO_1_123_Connect;
	DevDat[27].d_acts[A_DIS] = Wrapper_DSO_1_123_Disconnect;
	DevDat[27].d_acts[A_FTH] = Wrapper_DSO_1_123_Fetch;
	DevDat[27].d_acts[A_INX] = Wrapper_DSO_1_123_Init;
	DevDat[27].d_acts[A_LOD] = Wrapper_DSO_1_123_Load;
	DevDat[27].d_acts[A_OPN] = Wrapper_DSO_1_123_Open;
	DevDat[27].d_acts[A_RST] = Wrapper_DSO_1_123_Reset;
	DevDat[27].d_acts[A_FNC] = Wrapper_DSO_1_123_Setup;
	DevDat[27].d_acts[A_STA] = Wrapper_DSO_1_123_Status;
//
//	DSO_1:CH124
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[28].d_modlst = p_mod;
	DevDat[28].d_fncP = 124;
	DevDat[28].d_acts[A_CLS] = Wrapper_DSO_1_124_Close;
	DevDat[28].d_acts[A_CON] = Wrapper_DSO_1_124_Connect;
	DevDat[28].d_acts[A_DIS] = Wrapper_DSO_1_124_Disconnect;
	DevDat[28].d_acts[A_FTH] = Wrapper_DSO_1_124_Fetch;
	DevDat[28].d_acts[A_INX] = Wrapper_DSO_1_124_Init;
	DevDat[28].d_acts[A_LOD] = Wrapper_DSO_1_124_Load;
	DevDat[28].d_acts[A_OPN] = Wrapper_DSO_1_124_Open;
	DevDat[28].d_acts[A_RST] = Wrapper_DSO_1_124_Reset;
	DevDat[28].d_acts[A_FNC] = Wrapper_DSO_1_124_Setup;
	DevDat[28].d_acts[A_STA] = Wrapper_DSO_1_124_Status;
//
//	DSO_1:CH125
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[29].d_modlst = p_mod;
	DevDat[29].d_fncP = 125;
	DevDat[29].d_acts[A_CLS] = Wrapper_DSO_1_125_Close;
	DevDat[29].d_acts[A_CON] = Wrapper_DSO_1_125_Connect;
	DevDat[29].d_acts[A_DIS] = Wrapper_DSO_1_125_Disconnect;
	DevDat[29].d_acts[A_FTH] = Wrapper_DSO_1_125_Fetch;
	DevDat[29].d_acts[A_INX] = Wrapper_DSO_1_125_Init;
	DevDat[29].d_acts[A_LOD] = Wrapper_DSO_1_125_Load;
	DevDat[29].d_acts[A_OPN] = Wrapper_DSO_1_125_Open;
	DevDat[29].d_acts[A_RST] = Wrapper_DSO_1_125_Reset;
	DevDat[29].d_acts[A_FNC] = Wrapper_DSO_1_125_Setup;
	DevDat[29].d_acts[A_STA] = Wrapper_DSO_1_125_Status;
//
//	DSO_1:CH126
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[30].d_modlst = p_mod;
	DevDat[30].d_fncP = 126;
	DevDat[30].d_acts[A_CLS] = Wrapper_DSO_1_126_Close;
	DevDat[30].d_acts[A_CON] = Wrapper_DSO_1_126_Connect;
	DevDat[30].d_acts[A_DIS] = Wrapper_DSO_1_126_Disconnect;
	DevDat[30].d_acts[A_FTH] = Wrapper_DSO_1_126_Fetch;
	DevDat[30].d_acts[A_INX] = Wrapper_DSO_1_126_Init;
	DevDat[30].d_acts[A_LOD] = Wrapper_DSO_1_126_Load;
	DevDat[30].d_acts[A_OPN] = Wrapper_DSO_1_126_Open;
	DevDat[30].d_acts[A_RST] = Wrapper_DSO_1_126_Reset;
	DevDat[30].d_acts[A_FNC] = Wrapper_DSO_1_126_Setup;
	DevDat[30].d_acts[A_STA] = Wrapper_DSO_1_126_Status;
//
//	DSO_1:CH127
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[31].d_modlst = p_mod;
	DevDat[31].d_fncP = 127;
	DevDat[31].d_acts[A_CLS] = Wrapper_DSO_1_127_Close;
	DevDat[31].d_acts[A_CON] = Wrapper_DSO_1_127_Connect;
	DevDat[31].d_acts[A_DIS] = Wrapper_DSO_1_127_Disconnect;
	DevDat[31].d_acts[A_FTH] = Wrapper_DSO_1_127_Fetch;
	DevDat[31].d_acts[A_INX] = Wrapper_DSO_1_127_Init;
	DevDat[31].d_acts[A_LOD] = Wrapper_DSO_1_127_Load;
	DevDat[31].d_acts[A_OPN] = Wrapper_DSO_1_127_Open;
	DevDat[31].d_acts[A_RST] = Wrapper_DSO_1_127_Reset;
	DevDat[31].d_acts[A_FNC] = Wrapper_DSO_1_127_Setup;
	DevDat[31].d_acts[A_STA] = Wrapper_DSO_1_127_Status;
//
//	DSO_1:CH128
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[32].d_modlst = p_mod;
	DevDat[32].d_fncP = 128;
	DevDat[32].d_acts[A_CLS] = Wrapper_DSO_1_128_Close;
	DevDat[32].d_acts[A_CON] = Wrapper_DSO_1_128_Connect;
	DevDat[32].d_acts[A_DIS] = Wrapper_DSO_1_128_Disconnect;
	DevDat[32].d_acts[A_FTH] = Wrapper_DSO_1_128_Fetch;
	DevDat[32].d_acts[A_INX] = Wrapper_DSO_1_128_Init;
	DevDat[32].d_acts[A_LOD] = Wrapper_DSO_1_128_Load;
	DevDat[32].d_acts[A_OPN] = Wrapper_DSO_1_128_Open;
	DevDat[32].d_acts[A_RST] = Wrapper_DSO_1_128_Reset;
	DevDat[32].d_acts[A_FNC] = Wrapper_DSO_1_128_Setup;
	DevDat[32].d_acts[A_STA] = Wrapper_DSO_1_128_Status;
//
//	DSO_1:CH129
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[33].d_modlst = p_mod;
	DevDat[33].d_fncP = 129;
	DevDat[33].d_acts[A_CLS] = Wrapper_DSO_1_129_Close;
	DevDat[33].d_acts[A_CON] = Wrapper_DSO_1_129_Connect;
	DevDat[33].d_acts[A_DIS] = Wrapper_DSO_1_129_Disconnect;
	DevDat[33].d_acts[A_FTH] = Wrapper_DSO_1_129_Fetch;
	DevDat[33].d_acts[A_INX] = Wrapper_DSO_1_129_Init;
	DevDat[33].d_acts[A_LOD] = Wrapper_DSO_1_129_Load;
	DevDat[33].d_acts[A_OPN] = Wrapper_DSO_1_129_Open;
	DevDat[33].d_acts[A_RST] = Wrapper_DSO_1_129_Reset;
	DevDat[33].d_acts[A_FNC] = Wrapper_DSO_1_129_Setup;
	DevDat[33].d_acts[A_STA] = Wrapper_DSO_1_129_Status;
//
//	DSO_1:CH13
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[34].d_modlst = p_mod;
	DevDat[34].d_fncP = 13;
	DevDat[34].d_acts[A_CLS] = Wrapper_DSO_1_13_Close;
	DevDat[34].d_acts[A_CON] = Wrapper_DSO_1_13_Connect;
	DevDat[34].d_acts[A_DIS] = Wrapper_DSO_1_13_Disconnect;
	DevDat[34].d_acts[A_FTH] = Wrapper_DSO_1_13_Fetch;
	DevDat[34].d_acts[A_INX] = Wrapper_DSO_1_13_Init;
	DevDat[34].d_acts[A_LOD] = Wrapper_DSO_1_13_Load;
	DevDat[34].d_acts[A_OPN] = Wrapper_DSO_1_13_Open;
	DevDat[34].d_acts[A_RST] = Wrapper_DSO_1_13_Reset;
	DevDat[34].d_acts[A_FNC] = Wrapper_DSO_1_13_Setup;
	DevDat[34].d_acts[A_STA] = Wrapper_DSO_1_13_Status;
//
//	DSO_1:CH132
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[35].d_modlst = p_mod;
	DevDat[35].d_fncP = 132;
	DevDat[35].d_acts[A_CLS] = Wrapper_DSO_1_132_Close;
	DevDat[35].d_acts[A_CON] = Wrapper_DSO_1_132_Connect;
	DevDat[35].d_acts[A_DIS] = Wrapper_DSO_1_132_Disconnect;
	DevDat[35].d_acts[A_FTH] = Wrapper_DSO_1_132_Fetch;
	DevDat[35].d_acts[A_INX] = Wrapper_DSO_1_132_Init;
	DevDat[35].d_acts[A_LOD] = Wrapper_DSO_1_132_Load;
	DevDat[35].d_acts[A_OPN] = Wrapper_DSO_1_132_Open;
	DevDat[35].d_acts[A_RST] = Wrapper_DSO_1_132_Reset;
	DevDat[35].d_acts[A_FNC] = Wrapper_DSO_1_132_Setup;
	DevDat[35].d_acts[A_STA] = Wrapper_DSO_1_132_Status;
//
//	DSO_1:CH133
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[36].d_modlst = p_mod;
	DevDat[36].d_fncP = 133;
	DevDat[36].d_acts[A_CLS] = Wrapper_DSO_1_133_Close;
	DevDat[36].d_acts[A_CON] = Wrapper_DSO_1_133_Connect;
	DevDat[36].d_acts[A_DIS] = Wrapper_DSO_1_133_Disconnect;
	DevDat[36].d_acts[A_FTH] = Wrapper_DSO_1_133_Fetch;
	DevDat[36].d_acts[A_INX] = Wrapper_DSO_1_133_Init;
	DevDat[36].d_acts[A_LOD] = Wrapper_DSO_1_133_Load;
	DevDat[36].d_acts[A_OPN] = Wrapper_DSO_1_133_Open;
	DevDat[36].d_acts[A_RST] = Wrapper_DSO_1_133_Reset;
	DevDat[36].d_acts[A_FNC] = Wrapper_DSO_1_133_Setup;
	DevDat[36].d_acts[A_STA] = Wrapper_DSO_1_133_Status;
//
//	DSO_1:CH134
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[37].d_modlst = p_mod;
	DevDat[37].d_fncP = 134;
	DevDat[37].d_acts[A_CLS] = Wrapper_DSO_1_134_Close;
	DevDat[37].d_acts[A_CON] = Wrapper_DSO_1_134_Connect;
	DevDat[37].d_acts[A_DIS] = Wrapper_DSO_1_134_Disconnect;
	DevDat[37].d_acts[A_FTH] = Wrapper_DSO_1_134_Fetch;
	DevDat[37].d_acts[A_INX] = Wrapper_DSO_1_134_Init;
	DevDat[37].d_acts[A_LOD] = Wrapper_DSO_1_134_Load;
	DevDat[37].d_acts[A_OPN] = Wrapper_DSO_1_134_Open;
	DevDat[37].d_acts[A_RST] = Wrapper_DSO_1_134_Reset;
	DevDat[37].d_acts[A_FNC] = Wrapper_DSO_1_134_Setup;
	DevDat[37].d_acts[A_STA] = Wrapper_DSO_1_134_Status;
//
//	DSO_1:CH135
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[38].d_modlst = p_mod;
	DevDat[38].d_fncP = 135;
	DevDat[38].d_acts[A_CLS] = Wrapper_DSO_1_135_Close;
	DevDat[38].d_acts[A_CON] = Wrapper_DSO_1_135_Connect;
	DevDat[38].d_acts[A_DIS] = Wrapper_DSO_1_135_Disconnect;
	DevDat[38].d_acts[A_FTH] = Wrapper_DSO_1_135_Fetch;
	DevDat[38].d_acts[A_INX] = Wrapper_DSO_1_135_Init;
	DevDat[38].d_acts[A_LOD] = Wrapper_DSO_1_135_Load;
	DevDat[38].d_acts[A_OPN] = Wrapper_DSO_1_135_Open;
	DevDat[38].d_acts[A_RST] = Wrapper_DSO_1_135_Reset;
	DevDat[38].d_acts[A_FNC] = Wrapper_DSO_1_135_Setup;
	DevDat[38].d_acts[A_STA] = Wrapper_DSO_1_135_Status;
//
//	DSO_1:CH136
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[39].d_modlst = p_mod;
	DevDat[39].d_fncP = 136;
	DevDat[39].d_acts[A_CLS] = Wrapper_DSO_1_136_Close;
	DevDat[39].d_acts[A_CON] = Wrapper_DSO_1_136_Connect;
	DevDat[39].d_acts[A_DIS] = Wrapper_DSO_1_136_Disconnect;
	DevDat[39].d_acts[A_FTH] = Wrapper_DSO_1_136_Fetch;
	DevDat[39].d_acts[A_INX] = Wrapper_DSO_1_136_Init;
	DevDat[39].d_acts[A_LOD] = Wrapper_DSO_1_136_Load;
	DevDat[39].d_acts[A_OPN] = Wrapper_DSO_1_136_Open;
	DevDat[39].d_acts[A_RST] = Wrapper_DSO_1_136_Reset;
	DevDat[39].d_acts[A_FNC] = Wrapper_DSO_1_136_Setup;
	DevDat[39].d_acts[A_STA] = Wrapper_DSO_1_136_Status;
//
//	DSO_1:CH137
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[40].d_modlst = p_mod;
	DevDat[40].d_fncP = 137;
	DevDat[40].d_acts[A_CLS] = Wrapper_DSO_1_137_Close;
	DevDat[40].d_acts[A_CON] = Wrapper_DSO_1_137_Connect;
	DevDat[40].d_acts[A_DIS] = Wrapper_DSO_1_137_Disconnect;
	DevDat[40].d_acts[A_FTH] = Wrapper_DSO_1_137_Fetch;
	DevDat[40].d_acts[A_INX] = Wrapper_DSO_1_137_Init;
	DevDat[40].d_acts[A_LOD] = Wrapper_DSO_1_137_Load;
	DevDat[40].d_acts[A_OPN] = Wrapper_DSO_1_137_Open;
	DevDat[40].d_acts[A_RST] = Wrapper_DSO_1_137_Reset;
	DevDat[40].d_acts[A_FNC] = Wrapper_DSO_1_137_Setup;
	DevDat[40].d_acts[A_STA] = Wrapper_DSO_1_137_Status;
//
//	DSO_1:CH138
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[41].d_modlst = p_mod;
	DevDat[41].d_fncP = 138;
	DevDat[41].d_acts[A_CLS] = Wrapper_DSO_1_138_Close;
	DevDat[41].d_acts[A_CON] = Wrapper_DSO_1_138_Connect;
	DevDat[41].d_acts[A_DIS] = Wrapper_DSO_1_138_Disconnect;
	DevDat[41].d_acts[A_FTH] = Wrapper_DSO_1_138_Fetch;
	DevDat[41].d_acts[A_INX] = Wrapper_DSO_1_138_Init;
	DevDat[41].d_acts[A_LOD] = Wrapper_DSO_1_138_Load;
	DevDat[41].d_acts[A_OPN] = Wrapper_DSO_1_138_Open;
	DevDat[41].d_acts[A_RST] = Wrapper_DSO_1_138_Reset;
	DevDat[41].d_acts[A_FNC] = Wrapper_DSO_1_138_Setup;
	DevDat[41].d_acts[A_STA] = Wrapper_DSO_1_138_Status;
//
//	DSO_1:CH139
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[42].d_modlst = p_mod;
	DevDat[42].d_fncP = 139;
	DevDat[42].d_acts[A_CLS] = Wrapper_DSO_1_139_Close;
	DevDat[42].d_acts[A_CON] = Wrapper_DSO_1_139_Connect;
	DevDat[42].d_acts[A_DIS] = Wrapper_DSO_1_139_Disconnect;
	DevDat[42].d_acts[A_FTH] = Wrapper_DSO_1_139_Fetch;
	DevDat[42].d_acts[A_INX] = Wrapper_DSO_1_139_Init;
	DevDat[42].d_acts[A_LOD] = Wrapper_DSO_1_139_Load;
	DevDat[42].d_acts[A_OPN] = Wrapper_DSO_1_139_Open;
	DevDat[42].d_acts[A_RST] = Wrapper_DSO_1_139_Reset;
	DevDat[42].d_acts[A_FNC] = Wrapper_DSO_1_139_Setup;
	DevDat[42].d_acts[A_STA] = Wrapper_DSO_1_139_Status;
//
//	DSO_1:CH14
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_EVTI);  // event-indicator
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[43].d_modlst = p_mod;
	DevDat[43].d_fncP = 14;
	DevDat[43].d_acts[A_CLS] = Wrapper_DSO_1_14_Close;
	DevDat[43].d_acts[A_CON] = Wrapper_DSO_1_14_Connect;
	DevDat[43].d_acts[A_DIS] = Wrapper_DSO_1_14_Disconnect;
	DevDat[43].d_acts[A_FTH] = Wrapper_DSO_1_14_Fetch;
	DevDat[43].d_acts[A_INX] = Wrapper_DSO_1_14_Init;
	DevDat[43].d_acts[A_LOD] = Wrapper_DSO_1_14_Load;
	DevDat[43].d_acts[A_OPN] = Wrapper_DSO_1_14_Open;
	DevDat[43].d_acts[A_RST] = Wrapper_DSO_1_14_Reset;
	DevDat[43].d_acts[A_FNC] = Wrapper_DSO_1_14_Setup;
	DevDat[43].d_acts[A_STA] = Wrapper_DSO_1_14_Status;
//
//	DSO_1:CH140
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[44].d_modlst = p_mod;
	DevDat[44].d_fncP = 140;
	DevDat[44].d_acts[A_CLS] = Wrapper_DSO_1_140_Close;
	DevDat[44].d_acts[A_CON] = Wrapper_DSO_1_140_Connect;
	DevDat[44].d_acts[A_DIS] = Wrapper_DSO_1_140_Disconnect;
	DevDat[44].d_acts[A_FTH] = Wrapper_DSO_1_140_Fetch;
	DevDat[44].d_acts[A_INX] = Wrapper_DSO_1_140_Init;
	DevDat[44].d_acts[A_LOD] = Wrapper_DSO_1_140_Load;
	DevDat[44].d_acts[A_OPN] = Wrapper_DSO_1_140_Open;
	DevDat[44].d_acts[A_RST] = Wrapper_DSO_1_140_Reset;
	DevDat[44].d_acts[A_FNC] = Wrapper_DSO_1_140_Setup;
	DevDat[44].d_acts[A_STA] = Wrapper_DSO_1_140_Status;
//
//	DSO_1:CH142
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[45].d_modlst = p_mod;
	DevDat[45].d_fncP = 142;
	DevDat[45].d_acts[A_CLS] = Wrapper_DSO_1_142_Close;
	DevDat[45].d_acts[A_CON] = Wrapper_DSO_1_142_Connect;
	DevDat[45].d_acts[A_DIS] = Wrapper_DSO_1_142_Disconnect;
	DevDat[45].d_acts[A_FTH] = Wrapper_DSO_1_142_Fetch;
	DevDat[45].d_acts[A_INX] = Wrapper_DSO_1_142_Init;
	DevDat[45].d_acts[A_LOD] = Wrapper_DSO_1_142_Load;
	DevDat[45].d_acts[A_OPN] = Wrapper_DSO_1_142_Open;
	DevDat[45].d_acts[A_RST] = Wrapper_DSO_1_142_Reset;
	DevDat[45].d_acts[A_FNC] = Wrapper_DSO_1_142_Setup;
	DevDat[45].d_acts[A_STA] = Wrapper_DSO_1_142_Status;
//
//	DSO_1:CH143
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[46].d_modlst = p_mod;
	DevDat[46].d_fncP = 143;
	DevDat[46].d_acts[A_CLS] = Wrapper_DSO_1_143_Close;
	DevDat[46].d_acts[A_CON] = Wrapper_DSO_1_143_Connect;
	DevDat[46].d_acts[A_DIS] = Wrapper_DSO_1_143_Disconnect;
	DevDat[46].d_acts[A_FTH] = Wrapper_DSO_1_143_Fetch;
	DevDat[46].d_acts[A_INX] = Wrapper_DSO_1_143_Init;
	DevDat[46].d_acts[A_LOD] = Wrapper_DSO_1_143_Load;
	DevDat[46].d_acts[A_OPN] = Wrapper_DSO_1_143_Open;
	DevDat[46].d_acts[A_RST] = Wrapper_DSO_1_143_Reset;
	DevDat[46].d_acts[A_FNC] = Wrapper_DSO_1_143_Setup;
	DevDat[46].d_acts[A_STA] = Wrapper_DSO_1_143_Status;
//
//	DSO_1:CH144
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[47].d_modlst = p_mod;
	DevDat[47].d_fncP = 144;
	DevDat[47].d_acts[A_CLS] = Wrapper_DSO_1_144_Close;
	DevDat[47].d_acts[A_CON] = Wrapper_DSO_1_144_Connect;
	DevDat[47].d_acts[A_DIS] = Wrapper_DSO_1_144_Disconnect;
	DevDat[47].d_acts[A_FTH] = Wrapper_DSO_1_144_Fetch;
	DevDat[47].d_acts[A_INX] = Wrapper_DSO_1_144_Init;
	DevDat[47].d_acts[A_LOD] = Wrapper_DSO_1_144_Load;
	DevDat[47].d_acts[A_OPN] = Wrapper_DSO_1_144_Open;
	DevDat[47].d_acts[A_RST] = Wrapper_DSO_1_144_Reset;
	DevDat[47].d_acts[A_FNC] = Wrapper_DSO_1_144_Setup;
	DevDat[47].d_acts[A_STA] = Wrapper_DSO_1_144_Status;
//
//	DSO_1:CH145
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[48].d_modlst = p_mod;
	DevDat[48].d_fncP = 145;
	DevDat[48].d_acts[A_CLS] = Wrapper_DSO_1_145_Close;
	DevDat[48].d_acts[A_CON] = Wrapper_DSO_1_145_Connect;
	DevDat[48].d_acts[A_DIS] = Wrapper_DSO_1_145_Disconnect;
	DevDat[48].d_acts[A_FTH] = Wrapper_DSO_1_145_Fetch;
	DevDat[48].d_acts[A_INX] = Wrapper_DSO_1_145_Init;
	DevDat[48].d_acts[A_LOD] = Wrapper_DSO_1_145_Load;
	DevDat[48].d_acts[A_OPN] = Wrapper_DSO_1_145_Open;
	DevDat[48].d_acts[A_RST] = Wrapper_DSO_1_145_Reset;
	DevDat[48].d_acts[A_FNC] = Wrapper_DSO_1_145_Setup;
	DevDat[48].d_acts[A_STA] = Wrapper_DSO_1_145_Status;
//
//	DSO_1:CH146
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[49].d_modlst = p_mod;
	DevDat[49].d_fncP = 146;
	DevDat[49].d_acts[A_CLS] = Wrapper_DSO_1_146_Close;
	DevDat[49].d_acts[A_CON] = Wrapper_DSO_1_146_Connect;
	DevDat[49].d_acts[A_DIS] = Wrapper_DSO_1_146_Disconnect;
	DevDat[49].d_acts[A_FTH] = Wrapper_DSO_1_146_Fetch;
	DevDat[49].d_acts[A_INX] = Wrapper_DSO_1_146_Init;
	DevDat[49].d_acts[A_LOD] = Wrapper_DSO_1_146_Load;
	DevDat[49].d_acts[A_OPN] = Wrapper_DSO_1_146_Open;
	DevDat[49].d_acts[A_RST] = Wrapper_DSO_1_146_Reset;
	DevDat[49].d_acts[A_FNC] = Wrapper_DSO_1_146_Setup;
	DevDat[49].d_acts[A_STA] = Wrapper_DSO_1_146_Status;
//
//	DSO_1:CH147
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[50].d_modlst = p_mod;
	DevDat[50].d_fncP = 147;
	DevDat[50].d_acts[A_CLS] = Wrapper_DSO_1_147_Close;
	DevDat[50].d_acts[A_CON] = Wrapper_DSO_1_147_Connect;
	DevDat[50].d_acts[A_DIS] = Wrapper_DSO_1_147_Disconnect;
	DevDat[50].d_acts[A_FTH] = Wrapper_DSO_1_147_Fetch;
	DevDat[50].d_acts[A_INX] = Wrapper_DSO_1_147_Init;
	DevDat[50].d_acts[A_LOD] = Wrapper_DSO_1_147_Load;
	DevDat[50].d_acts[A_OPN] = Wrapper_DSO_1_147_Open;
	DevDat[50].d_acts[A_RST] = Wrapper_DSO_1_147_Reset;
	DevDat[50].d_acts[A_FNC] = Wrapper_DSO_1_147_Setup;
	DevDat[50].d_acts[A_STA] = Wrapper_DSO_1_147_Status;
//
//	DSO_1:CH148
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[51].d_modlst = p_mod;
	DevDat[51].d_fncP = 148;
	DevDat[51].d_acts[A_CLS] = Wrapper_DSO_1_148_Close;
	DevDat[51].d_acts[A_CON] = Wrapper_DSO_1_148_Connect;
	DevDat[51].d_acts[A_DIS] = Wrapper_DSO_1_148_Disconnect;
	DevDat[51].d_acts[A_FTH] = Wrapper_DSO_1_148_Fetch;
	DevDat[51].d_acts[A_INX] = Wrapper_DSO_1_148_Init;
	DevDat[51].d_acts[A_LOD] = Wrapper_DSO_1_148_Load;
	DevDat[51].d_acts[A_OPN] = Wrapper_DSO_1_148_Open;
	DevDat[51].d_acts[A_RST] = Wrapper_DSO_1_148_Reset;
	DevDat[51].d_acts[A_FNC] = Wrapper_DSO_1_148_Setup;
	DevDat[51].d_acts[A_STA] = Wrapper_DSO_1_148_Status;
//
//	DSO_1:CH149
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[52].d_modlst = p_mod;
	DevDat[52].d_fncP = 149;
	DevDat[52].d_acts[A_CLS] = Wrapper_DSO_1_149_Close;
	DevDat[52].d_acts[A_CON] = Wrapper_DSO_1_149_Connect;
	DevDat[52].d_acts[A_DIS] = Wrapper_DSO_1_149_Disconnect;
	DevDat[52].d_acts[A_FTH] = Wrapper_DSO_1_149_Fetch;
	DevDat[52].d_acts[A_INX] = Wrapper_DSO_1_149_Init;
	DevDat[52].d_acts[A_LOD] = Wrapper_DSO_1_149_Load;
	DevDat[52].d_acts[A_OPN] = Wrapper_DSO_1_149_Open;
	DevDat[52].d_acts[A_RST] = Wrapper_DSO_1_149_Reset;
	DevDat[52].d_acts[A_FNC] = Wrapper_DSO_1_149_Setup;
	DevDat[52].d_acts[A_STA] = Wrapper_DSO_1_149_Status;
//
//	DSO_1:CH150
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[53].d_modlst = p_mod;
	DevDat[53].d_fncP = 150;
	DevDat[53].d_acts[A_CLS] = Wrapper_DSO_1_150_Close;
	DevDat[53].d_acts[A_CON] = Wrapper_DSO_1_150_Connect;
	DevDat[53].d_acts[A_DIS] = Wrapper_DSO_1_150_Disconnect;
	DevDat[53].d_acts[A_FTH] = Wrapper_DSO_1_150_Fetch;
	DevDat[53].d_acts[A_INX] = Wrapper_DSO_1_150_Init;
	DevDat[53].d_acts[A_LOD] = Wrapper_DSO_1_150_Load;
	DevDat[53].d_acts[A_OPN] = Wrapper_DSO_1_150_Open;
	DevDat[53].d_acts[A_RST] = Wrapper_DSO_1_150_Reset;
	DevDat[53].d_acts[A_FNC] = Wrapper_DSO_1_150_Setup;
	DevDat[53].d_acts[A_STA] = Wrapper_DSO_1_150_Status;
//
//	DSO_1:CH152
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CMWV);  // compare-wave
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_LDVW);  // load-wave
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SVFM);  // save-from
	p_mod = BldModDat (p_mod, (short) M_SVTO);  // save-to
	p_mod = BldModDat (p_mod, (short) M_SVWV);  // save-wave
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[54].d_modlst = p_mod;
	DevDat[54].d_fncP = 152;
	DevDat[54].d_acts[A_CLS] = Wrapper_DSO_1_152_Close;
	DevDat[54].d_acts[A_CON] = Wrapper_DSO_1_152_Connect;
	DevDat[54].d_acts[A_DIS] = Wrapper_DSO_1_152_Disconnect;
	DevDat[54].d_acts[A_FTH] = Wrapper_DSO_1_152_Fetch;
	DevDat[54].d_acts[A_INX] = Wrapper_DSO_1_152_Init;
	DevDat[54].d_acts[A_LOD] = Wrapper_DSO_1_152_Load;
	DevDat[54].d_acts[A_OPN] = Wrapper_DSO_1_152_Open;
	DevDat[54].d_acts[A_RST] = Wrapper_DSO_1_152_Reset;
	DevDat[54].d_acts[A_FNC] = Wrapper_DSO_1_152_Setup;
	DevDat[54].d_acts[A_STA] = Wrapper_DSO_1_152_Status;
//
//	DSO_1:CH153
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CMWV);  // compare-wave
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_LDFM);  // load-from
	p_mod = BldModDat (p_mod, (short) M_LDVW);  // load-wave
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SVWV);  // save-wave
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[55].d_modlst = p_mod;
	DevDat[55].d_fncP = 153;
	DevDat[55].d_acts[A_CLS] = Wrapper_DSO_1_153_Close;
	DevDat[55].d_acts[A_CON] = Wrapper_DSO_1_153_Connect;
	DevDat[55].d_acts[A_DIS] = Wrapper_DSO_1_153_Disconnect;
	DevDat[55].d_acts[A_FTH] = Wrapper_DSO_1_153_Fetch;
	DevDat[55].d_acts[A_INX] = Wrapper_DSO_1_153_Init;
	DevDat[55].d_acts[A_LOD] = Wrapper_DSO_1_153_Load;
	DevDat[55].d_acts[A_OPN] = Wrapper_DSO_1_153_Open;
	DevDat[55].d_acts[A_RST] = Wrapper_DSO_1_153_Reset;
	DevDat[55].d_acts[A_FNC] = Wrapper_DSO_1_153_Setup;
	DevDat[55].d_acts[A_STA] = Wrapper_DSO_1_153_Status;
//
//	DSO_1:CH154
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ALLW);  // allowance
	p_mod = BldModDat (p_mod, (short) M_CMWV);  // compare-wave
	p_mod = BldModDat (p_mod, (short) M_CMCH);  // compare-ch
	p_mod = BldModDat (p_mod, (short) M_CMTO);  // compare-to
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_LDVW);  // load-wave
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SVWV);  // save-wave
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[56].d_modlst = p_mod;
	DevDat[56].d_fncP = 154;
	DevDat[56].d_acts[A_CLS] = Wrapper_DSO_1_154_Close;
	DevDat[56].d_acts[A_CON] = Wrapper_DSO_1_154_Connect;
	DevDat[56].d_acts[A_DIS] = Wrapper_DSO_1_154_Disconnect;
	DevDat[56].d_acts[A_FTH] = Wrapper_DSO_1_154_Fetch;
	DevDat[56].d_acts[A_INX] = Wrapper_DSO_1_154_Init;
	DevDat[56].d_acts[A_LOD] = Wrapper_DSO_1_154_Load;
	DevDat[56].d_acts[A_OPN] = Wrapper_DSO_1_154_Open;
	DevDat[56].d_acts[A_RST] = Wrapper_DSO_1_154_Reset;
	DevDat[56].d_acts[A_FNC] = Wrapper_DSO_1_154_Setup;
	DevDat[56].d_acts[A_STA] = Wrapper_DSO_1_154_Status;
//
//	DSO_1:CH155
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ADFM);  // add-from
	p_mod = BldModDat (p_mod, (short) M_ADTO);  // add-to
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[57].d_modlst = p_mod;
	DevDat[57].d_fncP = 155;
	DevDat[57].d_acts[A_CLS] = Wrapper_DSO_1_155_Close;
	DevDat[57].d_acts[A_CON] = Wrapper_DSO_1_155_Connect;
	DevDat[57].d_acts[A_DIS] = Wrapper_DSO_1_155_Disconnect;
	DevDat[57].d_acts[A_FTH] = Wrapper_DSO_1_155_Fetch;
	DevDat[57].d_acts[A_INX] = Wrapper_DSO_1_155_Init;
	DevDat[57].d_acts[A_LOD] = Wrapper_DSO_1_155_Load;
	DevDat[57].d_acts[A_OPN] = Wrapper_DSO_1_155_Open;
	DevDat[57].d_acts[A_RST] = Wrapper_DSO_1_155_Reset;
	DevDat[57].d_acts[A_FNC] = Wrapper_DSO_1_155_Setup;
	DevDat[57].d_acts[A_STA] = Wrapper_DSO_1_155_Status;
//
//	DSO_1:CH156
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SBFM);  // subtract-from
	p_mod = BldModDat (p_mod, (short) M_SBTO);  // subtract-to
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[58].d_modlst = p_mod;
	DevDat[58].d_fncP = 156;
	DevDat[58].d_acts[A_CLS] = Wrapper_DSO_1_156_Close;
	DevDat[58].d_acts[A_CON] = Wrapper_DSO_1_156_Connect;
	DevDat[58].d_acts[A_DIS] = Wrapper_DSO_1_156_Disconnect;
	DevDat[58].d_acts[A_FTH] = Wrapper_DSO_1_156_Fetch;
	DevDat[58].d_acts[A_INX] = Wrapper_DSO_1_156_Init;
	DevDat[58].d_acts[A_LOD] = Wrapper_DSO_1_156_Load;
	DevDat[58].d_acts[A_OPN] = Wrapper_DSO_1_156_Open;
	DevDat[58].d_acts[A_RST] = Wrapper_DSO_1_156_Reset;
	DevDat[58].d_acts[A_FNC] = Wrapper_DSO_1_156_Setup;
	DevDat[58].d_acts[A_STA] = Wrapper_DSO_1_156_Status;
//
//	DSO_1:CH157
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MPFM);  // multp-from
	p_mod = BldModDat (p_mod, (short) M_MPTO);  // multp-to
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[59].d_modlst = p_mod;
	DevDat[59].d_fncP = 157;
	DevDat[59].d_acts[A_CLS] = Wrapper_DSO_1_157_Close;
	DevDat[59].d_acts[A_CON] = Wrapper_DSO_1_157_Connect;
	DevDat[59].d_acts[A_DIS] = Wrapper_DSO_1_157_Disconnect;
	DevDat[59].d_acts[A_FTH] = Wrapper_DSO_1_157_Fetch;
	DevDat[59].d_acts[A_INX] = Wrapper_DSO_1_157_Init;
	DevDat[59].d_acts[A_LOD] = Wrapper_DSO_1_157_Load;
	DevDat[59].d_acts[A_OPN] = Wrapper_DSO_1_157_Open;
	DevDat[59].d_acts[A_RST] = Wrapper_DSO_1_157_Reset;
	DevDat[59].d_acts[A_FNC] = Wrapper_DSO_1_157_Setup;
	DevDat[59].d_acts[A_STA] = Wrapper_DSO_1_157_Status;
//
//	DSO_1:CH158
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_DIFR);  // differentiate
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[60].d_modlst = p_mod;
	DevDat[60].d_fncP = 158;
	DevDat[60].d_acts[A_CLS] = Wrapper_DSO_1_158_Close;
	DevDat[60].d_acts[A_CON] = Wrapper_DSO_1_158_Connect;
	DevDat[60].d_acts[A_DIS] = Wrapper_DSO_1_158_Disconnect;
	DevDat[60].d_acts[A_FTH] = Wrapper_DSO_1_158_Fetch;
	DevDat[60].d_acts[A_INX] = Wrapper_DSO_1_158_Init;
	DevDat[60].d_acts[A_LOD] = Wrapper_DSO_1_158_Load;
	DevDat[60].d_acts[A_OPN] = Wrapper_DSO_1_158_Open;
	DevDat[60].d_acts[A_RST] = Wrapper_DSO_1_158_Reset;
	DevDat[60].d_acts[A_FNC] = Wrapper_DSO_1_158_Setup;
	DevDat[60].d_acts[A_STA] = Wrapper_DSO_1_158_Status;
//
//	DSO_1:CH159
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_INTG);  // integrate
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[61].d_modlst = p_mod;
	DevDat[61].d_fncP = 159;
	DevDat[61].d_acts[A_CLS] = Wrapper_DSO_1_159_Close;
	DevDat[61].d_acts[A_CON] = Wrapper_DSO_1_159_Connect;
	DevDat[61].d_acts[A_DIS] = Wrapper_DSO_1_159_Disconnect;
	DevDat[61].d_acts[A_FTH] = Wrapper_DSO_1_159_Fetch;
	DevDat[61].d_acts[A_INX] = Wrapper_DSO_1_159_Init;
	DevDat[61].d_acts[A_LOD] = Wrapper_DSO_1_159_Load;
	DevDat[61].d_acts[A_OPN] = Wrapper_DSO_1_159_Open;
	DevDat[61].d_acts[A_RST] = Wrapper_DSO_1_159_Reset;
	DevDat[61].d_acts[A_FNC] = Wrapper_DSO_1_159_Setup;
	DevDat[61].d_acts[A_STA] = Wrapper_DSO_1_159_Status;
//
//	DSO_1:CH160
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_SMPL);  // sample
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[62].d_modlst = p_mod;
	DevDat[62].d_fncP = 160;
	DevDat[62].d_acts[A_CLS] = Wrapper_DSO_1_160_Close;
	DevDat[62].d_acts[A_CON] = Wrapper_DSO_1_160_Connect;
	DevDat[62].d_acts[A_DIS] = Wrapper_DSO_1_160_Disconnect;
	DevDat[62].d_acts[A_FTH] = Wrapper_DSO_1_160_Fetch;
	DevDat[62].d_acts[A_INX] = Wrapper_DSO_1_160_Init;
	DevDat[62].d_acts[A_LOD] = Wrapper_DSO_1_160_Load;
	DevDat[62].d_acts[A_OPN] = Wrapper_DSO_1_160_Open;
	DevDat[62].d_acts[A_RST] = Wrapper_DSO_1_160_Reset;
	DevDat[62].d_acts[A_FNC] = Wrapper_DSO_1_160_Setup;
	DevDat[62].d_acts[A_STA] = Wrapper_DSO_1_160_Status;
//
//	DSO_1:CH161
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[63].d_modlst = p_mod;
	DevDat[63].d_fncP = 161;
	DevDat[63].d_acts[A_CLS] = Wrapper_DSO_1_161_Close;
	DevDat[63].d_acts[A_CON] = Wrapper_DSO_1_161_Connect;
	DevDat[63].d_acts[A_DIS] = Wrapper_DSO_1_161_Disconnect;
	DevDat[63].d_acts[A_FTH] = Wrapper_DSO_1_161_Fetch;
	DevDat[63].d_acts[A_INX] = Wrapper_DSO_1_161_Init;
	DevDat[63].d_acts[A_LOD] = Wrapper_DSO_1_161_Load;
	DevDat[63].d_acts[A_OPN] = Wrapper_DSO_1_161_Open;
	DevDat[63].d_acts[A_RST] = Wrapper_DSO_1_161_Reset;
	DevDat[63].d_acts[A_FNC] = Wrapper_DSO_1_161_Setup;
	DevDat[63].d_acts[A_STA] = Wrapper_DSO_1_161_Status;
//
//	DSO_1:CH162
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[64].d_modlst = p_mod;
	DevDat[64].d_fncP = 162;
	DevDat[64].d_acts[A_CLS] = Wrapper_DSO_1_162_Close;
	DevDat[64].d_acts[A_CON] = Wrapper_DSO_1_162_Connect;
	DevDat[64].d_acts[A_DIS] = Wrapper_DSO_1_162_Disconnect;
	DevDat[64].d_acts[A_FTH] = Wrapper_DSO_1_162_Fetch;
	DevDat[64].d_acts[A_INX] = Wrapper_DSO_1_162_Init;
	DevDat[64].d_acts[A_LOD] = Wrapper_DSO_1_162_Load;
	DevDat[64].d_acts[A_OPN] = Wrapper_DSO_1_162_Open;
	DevDat[64].d_acts[A_RST] = Wrapper_DSO_1_162_Reset;
	DevDat[64].d_acts[A_FNC] = Wrapper_DSO_1_162_Setup;
	DevDat[64].d_acts[A_STA] = Wrapper_DSO_1_162_Status;
//
//	DSO_1:CH163
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TIME);  // time
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVTF);  // event-time-from
	p_mod = BldModDat (p_mod, (short) M_EVTT);  // event-time-to
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[65].d_modlst = p_mod;
	DevDat[65].d_fncP = 163;
	DevDat[65].d_acts[A_CLS] = Wrapper_DSO_1_163_Close;
	DevDat[65].d_acts[A_CON] = Wrapper_DSO_1_163_Connect;
	DevDat[65].d_acts[A_DIS] = Wrapper_DSO_1_163_Disconnect;
	DevDat[65].d_acts[A_FTH] = Wrapper_DSO_1_163_Fetch;
	DevDat[65].d_acts[A_INX] = Wrapper_DSO_1_163_Init;
	DevDat[65].d_acts[A_LOD] = Wrapper_DSO_1_163_Load;
	DevDat[65].d_acts[A_OPN] = Wrapper_DSO_1_163_Open;
	DevDat[65].d_acts[A_RST] = Wrapper_DSO_1_163_Reset;
	DevDat[65].d_acts[A_FNC] = Wrapper_DSO_1_163_Setup;
	DevDat[65].d_acts[A_STA] = Wrapper_DSO_1_163_Status;
//
//	DSO_1:CH2
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MPFM);  // multp-from
	p_mod = BldModDat (p_mod, (short) M_MPTO);  // multp-to
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[66].d_modlst = p_mod;
	DevDat[66].d_fncP = 2;
	DevDat[66].d_acts[A_CLS] = Wrapper_DSO_1_2_Close;
	DevDat[66].d_acts[A_CON] = Wrapper_DSO_1_2_Connect;
	DevDat[66].d_acts[A_DIS] = Wrapper_DSO_1_2_Disconnect;
	DevDat[66].d_acts[A_FTH] = Wrapper_DSO_1_2_Fetch;
	DevDat[66].d_acts[A_INX] = Wrapper_DSO_1_2_Init;
	DevDat[66].d_acts[A_LOD] = Wrapper_DSO_1_2_Load;
	DevDat[66].d_acts[A_OPN] = Wrapper_DSO_1_2_Open;
	DevDat[66].d_acts[A_RST] = Wrapper_DSO_1_2_Reset;
	DevDat[66].d_acts[A_FNC] = Wrapper_DSO_1_2_Setup;
	DevDat[66].d_acts[A_STA] = Wrapper_DSO_1_2_Status;
//
//	DSO_1:CH201
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[67].d_modlst = p_mod;
	DevDat[67].d_fncP = 201;
	DevDat[67].d_acts[A_CLS] = Wrapper_DSO_1_201_Close;
	DevDat[67].d_acts[A_CON] = Wrapper_DSO_1_201_Connect;
	DevDat[67].d_acts[A_DIS] = Wrapper_DSO_1_201_Disconnect;
	DevDat[67].d_acts[A_FTH] = Wrapper_DSO_1_201_Fetch;
	DevDat[67].d_acts[A_INX] = Wrapper_DSO_1_201_Init;
	DevDat[67].d_acts[A_LOD] = Wrapper_DSO_1_201_Load;
	DevDat[67].d_acts[A_OPN] = Wrapper_DSO_1_201_Open;
	DevDat[67].d_acts[A_RST] = Wrapper_DSO_1_201_Reset;
	DevDat[67].d_acts[A_FNC] = Wrapper_DSO_1_201_Setup;
	DevDat[67].d_acts[A_STA] = Wrapper_DSO_1_201_Status;
//
//	DSO_1:CH202
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[68].d_modlst = p_mod;
	DevDat[68].d_fncP = 202;
	DevDat[68].d_acts[A_CLS] = Wrapper_DSO_1_202_Close;
	DevDat[68].d_acts[A_CON] = Wrapper_DSO_1_202_Connect;
	DevDat[68].d_acts[A_DIS] = Wrapper_DSO_1_202_Disconnect;
	DevDat[68].d_acts[A_FTH] = Wrapper_DSO_1_202_Fetch;
	DevDat[68].d_acts[A_INX] = Wrapper_DSO_1_202_Init;
	DevDat[68].d_acts[A_LOD] = Wrapper_DSO_1_202_Load;
	DevDat[68].d_acts[A_OPN] = Wrapper_DSO_1_202_Open;
	DevDat[68].d_acts[A_RST] = Wrapper_DSO_1_202_Reset;
	DevDat[68].d_acts[A_FNC] = Wrapper_DSO_1_202_Setup;
	DevDat[68].d_acts[A_STA] = Wrapper_DSO_1_202_Status;
//
//	DSO_1:CH203
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[69].d_modlst = p_mod;
	DevDat[69].d_fncP = 203;
	DevDat[69].d_acts[A_CLS] = Wrapper_DSO_1_203_Close;
	DevDat[69].d_acts[A_CON] = Wrapper_DSO_1_203_Connect;
	DevDat[69].d_acts[A_DIS] = Wrapper_DSO_1_203_Disconnect;
	DevDat[69].d_acts[A_FTH] = Wrapper_DSO_1_203_Fetch;
	DevDat[69].d_acts[A_INX] = Wrapper_DSO_1_203_Init;
	DevDat[69].d_acts[A_LOD] = Wrapper_DSO_1_203_Load;
	DevDat[69].d_acts[A_OPN] = Wrapper_DSO_1_203_Open;
	DevDat[69].d_acts[A_RST] = Wrapper_DSO_1_203_Reset;
	DevDat[69].d_acts[A_FNC] = Wrapper_DSO_1_203_Setup;
	DevDat[69].d_acts[A_STA] = Wrapper_DSO_1_203_Status;
//
//	DSO_1:CH204
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[70].d_modlst = p_mod;
	DevDat[70].d_fncP = 204;
	DevDat[70].d_acts[A_CLS] = Wrapper_DSO_1_204_Close;
	DevDat[70].d_acts[A_CON] = Wrapper_DSO_1_204_Connect;
	DevDat[70].d_acts[A_DIS] = Wrapper_DSO_1_204_Disconnect;
	DevDat[70].d_acts[A_FTH] = Wrapper_DSO_1_204_Fetch;
	DevDat[70].d_acts[A_INX] = Wrapper_DSO_1_204_Init;
	DevDat[70].d_acts[A_LOD] = Wrapper_DSO_1_204_Load;
	DevDat[70].d_acts[A_OPN] = Wrapper_DSO_1_204_Open;
	DevDat[70].d_acts[A_RST] = Wrapper_DSO_1_204_Reset;
	DevDat[70].d_acts[A_FNC] = Wrapper_DSO_1_204_Setup;
	DevDat[70].d_acts[A_STA] = Wrapper_DSO_1_204_Status;
//
//	DSO_1:CH205
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[71].d_modlst = p_mod;
	DevDat[71].d_fncP = 205;
	DevDat[71].d_acts[A_CLS] = Wrapper_DSO_1_205_Close;
	DevDat[71].d_acts[A_CON] = Wrapper_DSO_1_205_Connect;
	DevDat[71].d_acts[A_DIS] = Wrapper_DSO_1_205_Disconnect;
	DevDat[71].d_acts[A_FTH] = Wrapper_DSO_1_205_Fetch;
	DevDat[71].d_acts[A_INX] = Wrapper_DSO_1_205_Init;
	DevDat[71].d_acts[A_LOD] = Wrapper_DSO_1_205_Load;
	DevDat[71].d_acts[A_OPN] = Wrapper_DSO_1_205_Open;
	DevDat[71].d_acts[A_RST] = Wrapper_DSO_1_205_Reset;
	DevDat[71].d_acts[A_FNC] = Wrapper_DSO_1_205_Setup;
	DevDat[71].d_acts[A_STA] = Wrapper_DSO_1_205_Status;
//
//	DSO_1:CH206
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[72].d_modlst = p_mod;
	DevDat[72].d_fncP = 206;
	DevDat[72].d_acts[A_CLS] = Wrapper_DSO_1_206_Close;
	DevDat[72].d_acts[A_CON] = Wrapper_DSO_1_206_Connect;
	DevDat[72].d_acts[A_DIS] = Wrapper_DSO_1_206_Disconnect;
	DevDat[72].d_acts[A_FTH] = Wrapper_DSO_1_206_Fetch;
	DevDat[72].d_acts[A_INX] = Wrapper_DSO_1_206_Init;
	DevDat[72].d_acts[A_LOD] = Wrapper_DSO_1_206_Load;
	DevDat[72].d_acts[A_OPN] = Wrapper_DSO_1_206_Open;
	DevDat[72].d_acts[A_RST] = Wrapper_DSO_1_206_Reset;
	DevDat[72].d_acts[A_FNC] = Wrapper_DSO_1_206_Setup;
	DevDat[72].d_acts[A_STA] = Wrapper_DSO_1_206_Status;
//
//	DSO_1:CH207
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[73].d_modlst = p_mod;
	DevDat[73].d_fncP = 207;
	DevDat[73].d_acts[A_CLS] = Wrapper_DSO_1_207_Close;
	DevDat[73].d_acts[A_CON] = Wrapper_DSO_1_207_Connect;
	DevDat[73].d_acts[A_DIS] = Wrapper_DSO_1_207_Disconnect;
	DevDat[73].d_acts[A_FTH] = Wrapper_DSO_1_207_Fetch;
	DevDat[73].d_acts[A_INX] = Wrapper_DSO_1_207_Init;
	DevDat[73].d_acts[A_LOD] = Wrapper_DSO_1_207_Load;
	DevDat[73].d_acts[A_OPN] = Wrapper_DSO_1_207_Open;
	DevDat[73].d_acts[A_RST] = Wrapper_DSO_1_207_Reset;
	DevDat[73].d_acts[A_FNC] = Wrapper_DSO_1_207_Setup;
	DevDat[73].d_acts[A_STA] = Wrapper_DSO_1_207_Status;
//
//	DSO_1:CH208
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[74].d_modlst = p_mod;
	DevDat[74].d_fncP = 208;
	DevDat[74].d_acts[A_CLS] = Wrapper_DSO_1_208_Close;
	DevDat[74].d_acts[A_CON] = Wrapper_DSO_1_208_Connect;
	DevDat[74].d_acts[A_DIS] = Wrapper_DSO_1_208_Disconnect;
	DevDat[74].d_acts[A_FTH] = Wrapper_DSO_1_208_Fetch;
	DevDat[74].d_acts[A_INX] = Wrapper_DSO_1_208_Init;
	DevDat[74].d_acts[A_LOD] = Wrapper_DSO_1_208_Load;
	DevDat[74].d_acts[A_OPN] = Wrapper_DSO_1_208_Open;
	DevDat[74].d_acts[A_RST] = Wrapper_DSO_1_208_Reset;
	DevDat[74].d_acts[A_FNC] = Wrapper_DSO_1_208_Setup;
	DevDat[74].d_acts[A_STA] = Wrapper_DSO_1_208_Status;
//
//	DSO_1:CH209
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[75].d_modlst = p_mod;
	DevDat[75].d_fncP = 209;
	DevDat[75].d_acts[A_CLS] = Wrapper_DSO_1_209_Close;
	DevDat[75].d_acts[A_CON] = Wrapper_DSO_1_209_Connect;
	DevDat[75].d_acts[A_DIS] = Wrapper_DSO_1_209_Disconnect;
	DevDat[75].d_acts[A_FTH] = Wrapper_DSO_1_209_Fetch;
	DevDat[75].d_acts[A_INX] = Wrapper_DSO_1_209_Init;
	DevDat[75].d_acts[A_LOD] = Wrapper_DSO_1_209_Load;
	DevDat[75].d_acts[A_OPN] = Wrapper_DSO_1_209_Open;
	DevDat[75].d_acts[A_RST] = Wrapper_DSO_1_209_Reset;
	DevDat[75].d_acts[A_FNC] = Wrapper_DSO_1_209_Setup;
	DevDat[75].d_acts[A_STA] = Wrapper_DSO_1_209_Status;
//
//	DSO_1:CH210
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[76].d_modlst = p_mod;
	DevDat[76].d_fncP = 210;
	DevDat[76].d_acts[A_CLS] = Wrapper_DSO_1_210_Close;
	DevDat[76].d_acts[A_CON] = Wrapper_DSO_1_210_Connect;
	DevDat[76].d_acts[A_DIS] = Wrapper_DSO_1_210_Disconnect;
	DevDat[76].d_acts[A_FTH] = Wrapper_DSO_1_210_Fetch;
	DevDat[76].d_acts[A_INX] = Wrapper_DSO_1_210_Init;
	DevDat[76].d_acts[A_LOD] = Wrapper_DSO_1_210_Load;
	DevDat[76].d_acts[A_OPN] = Wrapper_DSO_1_210_Open;
	DevDat[76].d_acts[A_RST] = Wrapper_DSO_1_210_Reset;
	DevDat[76].d_acts[A_FNC] = Wrapper_DSO_1_210_Setup;
	DevDat[76].d_acts[A_STA] = Wrapper_DSO_1_210_Status;
//
//	DSO_1:CH211
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[77].d_modlst = p_mod;
	DevDat[77].d_fncP = 211;
	DevDat[77].d_acts[A_CLS] = Wrapper_DSO_1_211_Close;
	DevDat[77].d_acts[A_CON] = Wrapper_DSO_1_211_Connect;
	DevDat[77].d_acts[A_DIS] = Wrapper_DSO_1_211_Disconnect;
	DevDat[77].d_acts[A_FTH] = Wrapper_DSO_1_211_Fetch;
	DevDat[77].d_acts[A_INX] = Wrapper_DSO_1_211_Init;
	DevDat[77].d_acts[A_LOD] = Wrapper_DSO_1_211_Load;
	DevDat[77].d_acts[A_OPN] = Wrapper_DSO_1_211_Open;
	DevDat[77].d_acts[A_RST] = Wrapper_DSO_1_211_Reset;
	DevDat[77].d_acts[A_FNC] = Wrapper_DSO_1_211_Setup;
	DevDat[77].d_acts[A_STA] = Wrapper_DSO_1_211_Status;
//
//	DSO_1:CH212
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[78].d_modlst = p_mod;
	DevDat[78].d_fncP = 212;
	DevDat[78].d_acts[A_CLS] = Wrapper_DSO_1_212_Close;
	DevDat[78].d_acts[A_CON] = Wrapper_DSO_1_212_Connect;
	DevDat[78].d_acts[A_DIS] = Wrapper_DSO_1_212_Disconnect;
	DevDat[78].d_acts[A_FTH] = Wrapper_DSO_1_212_Fetch;
	DevDat[78].d_acts[A_INX] = Wrapper_DSO_1_212_Init;
	DevDat[78].d_acts[A_LOD] = Wrapper_DSO_1_212_Load;
	DevDat[78].d_acts[A_OPN] = Wrapper_DSO_1_212_Open;
	DevDat[78].d_acts[A_RST] = Wrapper_DSO_1_212_Reset;
	DevDat[78].d_acts[A_FNC] = Wrapper_DSO_1_212_Setup;
	DevDat[78].d_acts[A_STA] = Wrapper_DSO_1_212_Status;
//
//	DSO_1:CH213
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[79].d_modlst = p_mod;
	DevDat[79].d_fncP = 213;
	DevDat[79].d_acts[A_CLS] = Wrapper_DSO_1_213_Close;
	DevDat[79].d_acts[A_CON] = Wrapper_DSO_1_213_Connect;
	DevDat[79].d_acts[A_DIS] = Wrapper_DSO_1_213_Disconnect;
	DevDat[79].d_acts[A_FTH] = Wrapper_DSO_1_213_Fetch;
	DevDat[79].d_acts[A_INX] = Wrapper_DSO_1_213_Init;
	DevDat[79].d_acts[A_LOD] = Wrapper_DSO_1_213_Load;
	DevDat[79].d_acts[A_OPN] = Wrapper_DSO_1_213_Open;
	DevDat[79].d_acts[A_RST] = Wrapper_DSO_1_213_Reset;
	DevDat[79].d_acts[A_FNC] = Wrapper_DSO_1_213_Setup;
	DevDat[79].d_acts[A_STA] = Wrapper_DSO_1_213_Status;
//
//	DSO_1:CH214
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[80].d_modlst = p_mod;
	DevDat[80].d_fncP = 214;
	DevDat[80].d_acts[A_CLS] = Wrapper_DSO_1_214_Close;
	DevDat[80].d_acts[A_CON] = Wrapper_DSO_1_214_Connect;
	DevDat[80].d_acts[A_DIS] = Wrapper_DSO_1_214_Disconnect;
	DevDat[80].d_acts[A_FTH] = Wrapper_DSO_1_214_Fetch;
	DevDat[80].d_acts[A_INX] = Wrapper_DSO_1_214_Init;
	DevDat[80].d_acts[A_LOD] = Wrapper_DSO_1_214_Load;
	DevDat[80].d_acts[A_OPN] = Wrapper_DSO_1_214_Open;
	DevDat[80].d_acts[A_RST] = Wrapper_DSO_1_214_Reset;
	DevDat[80].d_acts[A_FNC] = Wrapper_DSO_1_214_Setup;
	DevDat[80].d_acts[A_STA] = Wrapper_DSO_1_214_Status;
//
//	DSO_1:CH215
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[81].d_modlst = p_mod;
	DevDat[81].d_fncP = 215;
	DevDat[81].d_acts[A_CLS] = Wrapper_DSO_1_215_Close;
	DevDat[81].d_acts[A_CON] = Wrapper_DSO_1_215_Connect;
	DevDat[81].d_acts[A_DIS] = Wrapper_DSO_1_215_Disconnect;
	DevDat[81].d_acts[A_FTH] = Wrapper_DSO_1_215_Fetch;
	DevDat[81].d_acts[A_INX] = Wrapper_DSO_1_215_Init;
	DevDat[81].d_acts[A_LOD] = Wrapper_DSO_1_215_Load;
	DevDat[81].d_acts[A_OPN] = Wrapper_DSO_1_215_Open;
	DevDat[81].d_acts[A_RST] = Wrapper_DSO_1_215_Reset;
	DevDat[81].d_acts[A_FNC] = Wrapper_DSO_1_215_Setup;
	DevDat[81].d_acts[A_STA] = Wrapper_DSO_1_215_Status;
//
//	DSO_1:CH216
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[82].d_modlst = p_mod;
	DevDat[82].d_fncP = 216;
	DevDat[82].d_acts[A_CLS] = Wrapper_DSO_1_216_Close;
	DevDat[82].d_acts[A_CON] = Wrapper_DSO_1_216_Connect;
	DevDat[82].d_acts[A_DIS] = Wrapper_DSO_1_216_Disconnect;
	DevDat[82].d_acts[A_FTH] = Wrapper_DSO_1_216_Fetch;
	DevDat[82].d_acts[A_INX] = Wrapper_DSO_1_216_Init;
	DevDat[82].d_acts[A_LOD] = Wrapper_DSO_1_216_Load;
	DevDat[82].d_acts[A_OPN] = Wrapper_DSO_1_216_Open;
	DevDat[82].d_acts[A_RST] = Wrapper_DSO_1_216_Reset;
	DevDat[82].d_acts[A_FNC] = Wrapper_DSO_1_216_Setup;
	DevDat[82].d_acts[A_STA] = Wrapper_DSO_1_216_Status;
//
//	DSO_1:CH217
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[83].d_modlst = p_mod;
	DevDat[83].d_fncP = 217;
	DevDat[83].d_acts[A_CLS] = Wrapper_DSO_1_217_Close;
	DevDat[83].d_acts[A_CON] = Wrapper_DSO_1_217_Connect;
	DevDat[83].d_acts[A_DIS] = Wrapper_DSO_1_217_Disconnect;
	DevDat[83].d_acts[A_FTH] = Wrapper_DSO_1_217_Fetch;
	DevDat[83].d_acts[A_INX] = Wrapper_DSO_1_217_Init;
	DevDat[83].d_acts[A_LOD] = Wrapper_DSO_1_217_Load;
	DevDat[83].d_acts[A_OPN] = Wrapper_DSO_1_217_Open;
	DevDat[83].d_acts[A_RST] = Wrapper_DSO_1_217_Reset;
	DevDat[83].d_acts[A_FNC] = Wrapper_DSO_1_217_Setup;
	DevDat[83].d_acts[A_STA] = Wrapper_DSO_1_217_Status;
//
//	DSO_1:CH218
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[84].d_modlst = p_mod;
	DevDat[84].d_fncP = 218;
	DevDat[84].d_acts[A_CLS] = Wrapper_DSO_1_218_Close;
	DevDat[84].d_acts[A_CON] = Wrapper_DSO_1_218_Connect;
	DevDat[84].d_acts[A_DIS] = Wrapper_DSO_1_218_Disconnect;
	DevDat[84].d_acts[A_FTH] = Wrapper_DSO_1_218_Fetch;
	DevDat[84].d_acts[A_INX] = Wrapper_DSO_1_218_Init;
	DevDat[84].d_acts[A_LOD] = Wrapper_DSO_1_218_Load;
	DevDat[84].d_acts[A_OPN] = Wrapper_DSO_1_218_Open;
	DevDat[84].d_acts[A_RST] = Wrapper_DSO_1_218_Reset;
	DevDat[84].d_acts[A_FNC] = Wrapper_DSO_1_218_Setup;
	DevDat[84].d_acts[A_STA] = Wrapper_DSO_1_218_Status;
//
//	DSO_1:CH219
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[85].d_modlst = p_mod;
	DevDat[85].d_fncP = 219;
	DevDat[85].d_acts[A_CLS] = Wrapper_DSO_1_219_Close;
	DevDat[85].d_acts[A_CON] = Wrapper_DSO_1_219_Connect;
	DevDat[85].d_acts[A_DIS] = Wrapper_DSO_1_219_Disconnect;
	DevDat[85].d_acts[A_FTH] = Wrapper_DSO_1_219_Fetch;
	DevDat[85].d_acts[A_INX] = Wrapper_DSO_1_219_Init;
	DevDat[85].d_acts[A_LOD] = Wrapper_DSO_1_219_Load;
	DevDat[85].d_acts[A_OPN] = Wrapper_DSO_1_219_Open;
	DevDat[85].d_acts[A_RST] = Wrapper_DSO_1_219_Reset;
	DevDat[85].d_acts[A_FNC] = Wrapper_DSO_1_219_Setup;
	DevDat[85].d_acts[A_STA] = Wrapper_DSO_1_219_Status;
//
//	DSO_1:CH220
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[86].d_modlst = p_mod;
	DevDat[86].d_fncP = 220;
	DevDat[86].d_acts[A_CLS] = Wrapper_DSO_1_220_Close;
	DevDat[86].d_acts[A_CON] = Wrapper_DSO_1_220_Connect;
	DevDat[86].d_acts[A_DIS] = Wrapper_DSO_1_220_Disconnect;
	DevDat[86].d_acts[A_FTH] = Wrapper_DSO_1_220_Fetch;
	DevDat[86].d_acts[A_INX] = Wrapper_DSO_1_220_Init;
	DevDat[86].d_acts[A_LOD] = Wrapper_DSO_1_220_Load;
	DevDat[86].d_acts[A_OPN] = Wrapper_DSO_1_220_Open;
	DevDat[86].d_acts[A_RST] = Wrapper_DSO_1_220_Reset;
	DevDat[86].d_acts[A_FNC] = Wrapper_DSO_1_220_Setup;
	DevDat[86].d_acts[A_STA] = Wrapper_DSO_1_220_Status;
//
//	DSO_1:CH221
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_NPWT);  // neg-pulse-width
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PPWT);  // pos-pulse-width
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PRFR);  // prf
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[87].d_modlst = p_mod;
	DevDat[87].d_fncP = 221;
	DevDat[87].d_acts[A_CLS] = Wrapper_DSO_1_221_Close;
	DevDat[87].d_acts[A_CON] = Wrapper_DSO_1_221_Connect;
	DevDat[87].d_acts[A_DIS] = Wrapper_DSO_1_221_Disconnect;
	DevDat[87].d_acts[A_FTH] = Wrapper_DSO_1_221_Fetch;
	DevDat[87].d_acts[A_INX] = Wrapper_DSO_1_221_Init;
	DevDat[87].d_acts[A_LOD] = Wrapper_DSO_1_221_Load;
	DevDat[87].d_acts[A_OPN] = Wrapper_DSO_1_221_Open;
	DevDat[87].d_acts[A_RST] = Wrapper_DSO_1_221_Reset;
	DevDat[87].d_acts[A_FNC] = Wrapper_DSO_1_221_Setup;
	DevDat[87].d_acts[A_STA] = Wrapper_DSO_1_221_Status;
//
//	DSO_1:CH222
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[88].d_modlst = p_mod;
	DevDat[88].d_fncP = 222;
	DevDat[88].d_acts[A_CLS] = Wrapper_DSO_1_222_Close;
	DevDat[88].d_acts[A_CON] = Wrapper_DSO_1_222_Connect;
	DevDat[88].d_acts[A_DIS] = Wrapper_DSO_1_222_Disconnect;
	DevDat[88].d_acts[A_FTH] = Wrapper_DSO_1_222_Fetch;
	DevDat[88].d_acts[A_INX] = Wrapper_DSO_1_222_Init;
	DevDat[88].d_acts[A_LOD] = Wrapper_DSO_1_222_Load;
	DevDat[88].d_acts[A_OPN] = Wrapper_DSO_1_222_Open;
	DevDat[88].d_acts[A_RST] = Wrapper_DSO_1_222_Reset;
	DevDat[88].d_acts[A_FNC] = Wrapper_DSO_1_222_Setup;
	DevDat[88].d_acts[A_STA] = Wrapper_DSO_1_222_Status;
//
//	DSO_1:CH223
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[89].d_modlst = p_mod;
	DevDat[89].d_fncP = 223;
	DevDat[89].d_acts[A_CLS] = Wrapper_DSO_1_223_Close;
	DevDat[89].d_acts[A_CON] = Wrapper_DSO_1_223_Connect;
	DevDat[89].d_acts[A_DIS] = Wrapper_DSO_1_223_Disconnect;
	DevDat[89].d_acts[A_FTH] = Wrapper_DSO_1_223_Fetch;
	DevDat[89].d_acts[A_INX] = Wrapper_DSO_1_223_Init;
	DevDat[89].d_acts[A_LOD] = Wrapper_DSO_1_223_Load;
	DevDat[89].d_acts[A_OPN] = Wrapper_DSO_1_223_Open;
	DevDat[89].d_acts[A_RST] = Wrapper_DSO_1_223_Reset;
	DevDat[89].d_acts[A_FNC] = Wrapper_DSO_1_223_Setup;
	DevDat[89].d_acts[A_STA] = Wrapper_DSO_1_223_Status;
//
//	DSO_1:CH224
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[90].d_modlst = p_mod;
	DevDat[90].d_fncP = 224;
	DevDat[90].d_acts[A_CLS] = Wrapper_DSO_1_224_Close;
	DevDat[90].d_acts[A_CON] = Wrapper_DSO_1_224_Connect;
	DevDat[90].d_acts[A_DIS] = Wrapper_DSO_1_224_Disconnect;
	DevDat[90].d_acts[A_FTH] = Wrapper_DSO_1_224_Fetch;
	DevDat[90].d_acts[A_INX] = Wrapper_DSO_1_224_Init;
	DevDat[90].d_acts[A_LOD] = Wrapper_DSO_1_224_Load;
	DevDat[90].d_acts[A_OPN] = Wrapper_DSO_1_224_Open;
	DevDat[90].d_acts[A_RST] = Wrapper_DSO_1_224_Reset;
	DevDat[90].d_acts[A_FNC] = Wrapper_DSO_1_224_Setup;
	DevDat[90].d_acts[A_STA] = Wrapper_DSO_1_224_Status;
//
//	DSO_1:CH225
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[91].d_modlst = p_mod;
	DevDat[91].d_fncP = 225;
	DevDat[91].d_acts[A_CLS] = Wrapper_DSO_1_225_Close;
	DevDat[91].d_acts[A_CON] = Wrapper_DSO_1_225_Connect;
	DevDat[91].d_acts[A_DIS] = Wrapper_DSO_1_225_Disconnect;
	DevDat[91].d_acts[A_FTH] = Wrapper_DSO_1_225_Fetch;
	DevDat[91].d_acts[A_INX] = Wrapper_DSO_1_225_Init;
	DevDat[91].d_acts[A_LOD] = Wrapper_DSO_1_225_Load;
	DevDat[91].d_acts[A_OPN] = Wrapper_DSO_1_225_Open;
	DevDat[91].d_acts[A_RST] = Wrapper_DSO_1_225_Reset;
	DevDat[91].d_acts[A_FNC] = Wrapper_DSO_1_225_Setup;
	DevDat[91].d_acts[A_STA] = Wrapper_DSO_1_225_Status;
//
//	DSO_1:CH226
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[92].d_modlst = p_mod;
	DevDat[92].d_fncP = 226;
	DevDat[92].d_acts[A_CLS] = Wrapper_DSO_1_226_Close;
	DevDat[92].d_acts[A_CON] = Wrapper_DSO_1_226_Connect;
	DevDat[92].d_acts[A_DIS] = Wrapper_DSO_1_226_Disconnect;
	DevDat[92].d_acts[A_FTH] = Wrapper_DSO_1_226_Fetch;
	DevDat[92].d_acts[A_INX] = Wrapper_DSO_1_226_Init;
	DevDat[92].d_acts[A_LOD] = Wrapper_DSO_1_226_Load;
	DevDat[92].d_acts[A_OPN] = Wrapper_DSO_1_226_Open;
	DevDat[92].d_acts[A_RST] = Wrapper_DSO_1_226_Reset;
	DevDat[92].d_acts[A_FNC] = Wrapper_DSO_1_226_Setup;
	DevDat[92].d_acts[A_STA] = Wrapper_DSO_1_226_Status;
//
//	DSO_1:CH227
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[93].d_modlst = p_mod;
	DevDat[93].d_fncP = 227;
	DevDat[93].d_acts[A_CLS] = Wrapper_DSO_1_227_Close;
	DevDat[93].d_acts[A_CON] = Wrapper_DSO_1_227_Connect;
	DevDat[93].d_acts[A_DIS] = Wrapper_DSO_1_227_Disconnect;
	DevDat[93].d_acts[A_FTH] = Wrapper_DSO_1_227_Fetch;
	DevDat[93].d_acts[A_INX] = Wrapper_DSO_1_227_Init;
	DevDat[93].d_acts[A_LOD] = Wrapper_DSO_1_227_Load;
	DevDat[93].d_acts[A_OPN] = Wrapper_DSO_1_227_Open;
	DevDat[93].d_acts[A_RST] = Wrapper_DSO_1_227_Reset;
	DevDat[93].d_acts[A_FNC] = Wrapper_DSO_1_227_Setup;
	DevDat[93].d_acts[A_STA] = Wrapper_DSO_1_227_Status;
//
//	DSO_1:CH228
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[94].d_modlst = p_mod;
	DevDat[94].d_fncP = 228;
	DevDat[94].d_acts[A_CLS] = Wrapper_DSO_1_228_Close;
	DevDat[94].d_acts[A_CON] = Wrapper_DSO_1_228_Connect;
	DevDat[94].d_acts[A_DIS] = Wrapper_DSO_1_228_Disconnect;
	DevDat[94].d_acts[A_FTH] = Wrapper_DSO_1_228_Fetch;
	DevDat[94].d_acts[A_INX] = Wrapper_DSO_1_228_Init;
	DevDat[94].d_acts[A_LOD] = Wrapper_DSO_1_228_Load;
	DevDat[94].d_acts[A_OPN] = Wrapper_DSO_1_228_Open;
	DevDat[94].d_acts[A_RST] = Wrapper_DSO_1_228_Reset;
	DevDat[94].d_acts[A_FNC] = Wrapper_DSO_1_228_Setup;
	DevDat[94].d_acts[A_STA] = Wrapper_DSO_1_228_Status;
//
//	DSO_1:CH229
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[95].d_modlst = p_mod;
	DevDat[95].d_fncP = 229;
	DevDat[95].d_acts[A_CLS] = Wrapper_DSO_1_229_Close;
	DevDat[95].d_acts[A_CON] = Wrapper_DSO_1_229_Connect;
	DevDat[95].d_acts[A_DIS] = Wrapper_DSO_1_229_Disconnect;
	DevDat[95].d_acts[A_FTH] = Wrapper_DSO_1_229_Fetch;
	DevDat[95].d_acts[A_INX] = Wrapper_DSO_1_229_Init;
	DevDat[95].d_acts[A_LOD] = Wrapper_DSO_1_229_Load;
	DevDat[95].d_acts[A_OPN] = Wrapper_DSO_1_229_Open;
	DevDat[95].d_acts[A_RST] = Wrapper_DSO_1_229_Reset;
	DevDat[95].d_acts[A_FNC] = Wrapper_DSO_1_229_Setup;
	DevDat[95].d_acts[A_STA] = Wrapper_DSO_1_229_Status;
//
//	DSO_1:CH230
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[96].d_modlst = p_mod;
	DevDat[96].d_fncP = 230;
	DevDat[96].d_acts[A_CLS] = Wrapper_DSO_1_230_Close;
	DevDat[96].d_acts[A_CON] = Wrapper_DSO_1_230_Connect;
	DevDat[96].d_acts[A_DIS] = Wrapper_DSO_1_230_Disconnect;
	DevDat[96].d_acts[A_FTH] = Wrapper_DSO_1_230_Fetch;
	DevDat[96].d_acts[A_INX] = Wrapper_DSO_1_230_Init;
	DevDat[96].d_acts[A_LOD] = Wrapper_DSO_1_230_Load;
	DevDat[96].d_acts[A_OPN] = Wrapper_DSO_1_230_Open;
	DevDat[96].d_acts[A_RST] = Wrapper_DSO_1_230_Reset;
	DevDat[96].d_acts[A_FNC] = Wrapper_DSO_1_230_Setup;
	DevDat[96].d_acts[A_STA] = Wrapper_DSO_1_230_Status;
//
//	DSO_1:CH232
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[97].d_modlst = p_mod;
	DevDat[97].d_fncP = 232;
	DevDat[97].d_acts[A_CLS] = Wrapper_DSO_1_232_Close;
	DevDat[97].d_acts[A_CON] = Wrapper_DSO_1_232_Connect;
	DevDat[97].d_acts[A_DIS] = Wrapper_DSO_1_232_Disconnect;
	DevDat[97].d_acts[A_FTH] = Wrapper_DSO_1_232_Fetch;
	DevDat[97].d_acts[A_INX] = Wrapper_DSO_1_232_Init;
	DevDat[97].d_acts[A_LOD] = Wrapper_DSO_1_232_Load;
	DevDat[97].d_acts[A_OPN] = Wrapper_DSO_1_232_Open;
	DevDat[97].d_acts[A_RST] = Wrapper_DSO_1_232_Reset;
	DevDat[97].d_acts[A_FNC] = Wrapper_DSO_1_232_Setup;
	DevDat[97].d_acts[A_STA] = Wrapper_DSO_1_232_Status;
//
//	DSO_1:CH233
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[98].d_modlst = p_mod;
	DevDat[98].d_fncP = 233;
	DevDat[98].d_acts[A_CLS] = Wrapper_DSO_1_233_Close;
	DevDat[98].d_acts[A_CON] = Wrapper_DSO_1_233_Connect;
	DevDat[98].d_acts[A_DIS] = Wrapper_DSO_1_233_Disconnect;
	DevDat[98].d_acts[A_FTH] = Wrapper_DSO_1_233_Fetch;
	DevDat[98].d_acts[A_INX] = Wrapper_DSO_1_233_Init;
	DevDat[98].d_acts[A_LOD] = Wrapper_DSO_1_233_Load;
	DevDat[98].d_acts[A_OPN] = Wrapper_DSO_1_233_Open;
	DevDat[98].d_acts[A_RST] = Wrapper_DSO_1_233_Reset;
	DevDat[98].d_acts[A_FNC] = Wrapper_DSO_1_233_Setup;
	DevDat[98].d_acts[A_STA] = Wrapper_DSO_1_233_Status;
//
//	DSO_1:CH234
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[99].d_modlst = p_mod;
	DevDat[99].d_fncP = 234;
	DevDat[99].d_acts[A_CLS] = Wrapper_DSO_1_234_Close;
	DevDat[99].d_acts[A_CON] = Wrapper_DSO_1_234_Connect;
	DevDat[99].d_acts[A_DIS] = Wrapper_DSO_1_234_Disconnect;
	DevDat[99].d_acts[A_FTH] = Wrapper_DSO_1_234_Fetch;
	DevDat[99].d_acts[A_INX] = Wrapper_DSO_1_234_Init;
	DevDat[99].d_acts[A_LOD] = Wrapper_DSO_1_234_Load;
	DevDat[99].d_acts[A_OPN] = Wrapper_DSO_1_234_Open;
	DevDat[99].d_acts[A_RST] = Wrapper_DSO_1_234_Reset;
	DevDat[99].d_acts[A_FNC] = Wrapper_DSO_1_234_Setup;
	DevDat[99].d_acts[A_STA] = Wrapper_DSO_1_234_Status;
//
//	DSO_1:CH235
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[100].d_modlst = p_mod;
	DevDat[100].d_fncP = 235;
	DevDat[100].d_acts[A_CLS] = Wrapper_DSO_1_235_Close;
	DevDat[100].d_acts[A_CON] = Wrapper_DSO_1_235_Connect;
	DevDat[100].d_acts[A_DIS] = Wrapper_DSO_1_235_Disconnect;
	DevDat[100].d_acts[A_FTH] = Wrapper_DSO_1_235_Fetch;
	DevDat[100].d_acts[A_INX] = Wrapper_DSO_1_235_Init;
	DevDat[100].d_acts[A_LOD] = Wrapper_DSO_1_235_Load;
	DevDat[100].d_acts[A_OPN] = Wrapper_DSO_1_235_Open;
	DevDat[100].d_acts[A_RST] = Wrapper_DSO_1_235_Reset;
	DevDat[100].d_acts[A_FNC] = Wrapper_DSO_1_235_Setup;
	DevDat[100].d_acts[A_STA] = Wrapper_DSO_1_235_Status;
//
//	DSO_1:CH236
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[101].d_modlst = p_mod;
	DevDat[101].d_fncP = 236;
	DevDat[101].d_acts[A_CLS] = Wrapper_DSO_1_236_Close;
	DevDat[101].d_acts[A_CON] = Wrapper_DSO_1_236_Connect;
	DevDat[101].d_acts[A_DIS] = Wrapper_DSO_1_236_Disconnect;
	DevDat[101].d_acts[A_FTH] = Wrapper_DSO_1_236_Fetch;
	DevDat[101].d_acts[A_INX] = Wrapper_DSO_1_236_Init;
	DevDat[101].d_acts[A_LOD] = Wrapper_DSO_1_236_Load;
	DevDat[101].d_acts[A_OPN] = Wrapper_DSO_1_236_Open;
	DevDat[101].d_acts[A_RST] = Wrapper_DSO_1_236_Reset;
	DevDat[101].d_acts[A_FNC] = Wrapper_DSO_1_236_Setup;
	DevDat[101].d_acts[A_STA] = Wrapper_DSO_1_236_Status;
//
//	DSO_1:CH237
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[102].d_modlst = p_mod;
	DevDat[102].d_fncP = 237;
	DevDat[102].d_acts[A_CLS] = Wrapper_DSO_1_237_Close;
	DevDat[102].d_acts[A_CON] = Wrapper_DSO_1_237_Connect;
	DevDat[102].d_acts[A_DIS] = Wrapper_DSO_1_237_Disconnect;
	DevDat[102].d_acts[A_FTH] = Wrapper_DSO_1_237_Fetch;
	DevDat[102].d_acts[A_INX] = Wrapper_DSO_1_237_Init;
	DevDat[102].d_acts[A_LOD] = Wrapper_DSO_1_237_Load;
	DevDat[102].d_acts[A_OPN] = Wrapper_DSO_1_237_Open;
	DevDat[102].d_acts[A_RST] = Wrapper_DSO_1_237_Reset;
	DevDat[102].d_acts[A_FNC] = Wrapper_DSO_1_237_Setup;
	DevDat[102].d_acts[A_STA] = Wrapper_DSO_1_237_Status;
//
//	DSO_1:CH238
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[103].d_modlst = p_mod;
	DevDat[103].d_fncP = 238;
	DevDat[103].d_acts[A_CLS] = Wrapper_DSO_1_238_Close;
	DevDat[103].d_acts[A_CON] = Wrapper_DSO_1_238_Connect;
	DevDat[103].d_acts[A_DIS] = Wrapper_DSO_1_238_Disconnect;
	DevDat[103].d_acts[A_FTH] = Wrapper_DSO_1_238_Fetch;
	DevDat[103].d_acts[A_INX] = Wrapper_DSO_1_238_Init;
	DevDat[103].d_acts[A_LOD] = Wrapper_DSO_1_238_Load;
	DevDat[103].d_acts[A_OPN] = Wrapper_DSO_1_238_Open;
	DevDat[103].d_acts[A_RST] = Wrapper_DSO_1_238_Reset;
	DevDat[103].d_acts[A_FNC] = Wrapper_DSO_1_238_Setup;
	DevDat[103].d_acts[A_STA] = Wrapper_DSO_1_238_Status;
//
//	DSO_1:CH239
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[104].d_modlst = p_mod;
	DevDat[104].d_fncP = 239;
	DevDat[104].d_acts[A_CLS] = Wrapper_DSO_1_239_Close;
	DevDat[104].d_acts[A_CON] = Wrapper_DSO_1_239_Connect;
	DevDat[104].d_acts[A_DIS] = Wrapper_DSO_1_239_Disconnect;
	DevDat[104].d_acts[A_FTH] = Wrapper_DSO_1_239_Fetch;
	DevDat[104].d_acts[A_INX] = Wrapper_DSO_1_239_Init;
	DevDat[104].d_acts[A_LOD] = Wrapper_DSO_1_239_Load;
	DevDat[104].d_acts[A_OPN] = Wrapper_DSO_1_239_Open;
	DevDat[104].d_acts[A_RST] = Wrapper_DSO_1_239_Reset;
	DevDat[104].d_acts[A_FNC] = Wrapper_DSO_1_239_Setup;
	DevDat[104].d_acts[A_STA] = Wrapper_DSO_1_239_Status;
//
//	DSO_1:CH240
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[105].d_modlst = p_mod;
	DevDat[105].d_fncP = 240;
	DevDat[105].d_acts[A_CLS] = Wrapper_DSO_1_240_Close;
	DevDat[105].d_acts[A_CON] = Wrapper_DSO_1_240_Connect;
	DevDat[105].d_acts[A_DIS] = Wrapper_DSO_1_240_Disconnect;
	DevDat[105].d_acts[A_FTH] = Wrapper_DSO_1_240_Fetch;
	DevDat[105].d_acts[A_INX] = Wrapper_DSO_1_240_Init;
	DevDat[105].d_acts[A_LOD] = Wrapper_DSO_1_240_Load;
	DevDat[105].d_acts[A_OPN] = Wrapper_DSO_1_240_Open;
	DevDat[105].d_acts[A_RST] = Wrapper_DSO_1_240_Reset;
	DevDat[105].d_acts[A_FNC] = Wrapper_DSO_1_240_Setup;
	DevDat[105].d_acts[A_STA] = Wrapper_DSO_1_240_Status;
//
//	DSO_1:CH241
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_OVER);  // overshoot
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PSHT);  // preshoot
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[106].d_modlst = p_mod;
	DevDat[106].d_fncP = 241;
	DevDat[106].d_acts[A_CLS] = Wrapper_DSO_1_241_Close;
	DevDat[106].d_acts[A_CON] = Wrapper_DSO_1_241_Connect;
	DevDat[106].d_acts[A_DIS] = Wrapper_DSO_1_241_Disconnect;
	DevDat[106].d_acts[A_FTH] = Wrapper_DSO_1_241_Fetch;
	DevDat[106].d_acts[A_INX] = Wrapper_DSO_1_241_Init;
	DevDat[106].d_acts[A_LOD] = Wrapper_DSO_1_241_Load;
	DevDat[106].d_acts[A_OPN] = Wrapper_DSO_1_241_Open;
	DevDat[106].d_acts[A_RST] = Wrapper_DSO_1_241_Reset;
	DevDat[106].d_acts[A_FNC] = Wrapper_DSO_1_241_Setup;
	DevDat[106].d_acts[A_STA] = Wrapper_DSO_1_241_Status;
//
//	DSO_1:CH242
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[107].d_modlst = p_mod;
	DevDat[107].d_fncP = 242;
	DevDat[107].d_acts[A_CLS] = Wrapper_DSO_1_242_Close;
	DevDat[107].d_acts[A_CON] = Wrapper_DSO_1_242_Connect;
	DevDat[107].d_acts[A_DIS] = Wrapper_DSO_1_242_Disconnect;
	DevDat[107].d_acts[A_FTH] = Wrapper_DSO_1_242_Fetch;
	DevDat[107].d_acts[A_INX] = Wrapper_DSO_1_242_Init;
	DevDat[107].d_acts[A_LOD] = Wrapper_DSO_1_242_Load;
	DevDat[107].d_acts[A_OPN] = Wrapper_DSO_1_242_Open;
	DevDat[107].d_acts[A_RST] = Wrapper_DSO_1_242_Reset;
	DevDat[107].d_acts[A_FNC] = Wrapper_DSO_1_242_Setup;
	DevDat[107].d_acts[A_STA] = Wrapper_DSO_1_242_Status;
//
//	DSO_1:CH243
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[108].d_modlst = p_mod;
	DevDat[108].d_fncP = 243;
	DevDat[108].d_acts[A_CLS] = Wrapper_DSO_1_243_Close;
	DevDat[108].d_acts[A_CON] = Wrapper_DSO_1_243_Connect;
	DevDat[108].d_acts[A_DIS] = Wrapper_DSO_1_243_Disconnect;
	DevDat[108].d_acts[A_FTH] = Wrapper_DSO_1_243_Fetch;
	DevDat[108].d_acts[A_INX] = Wrapper_DSO_1_243_Init;
	DevDat[108].d_acts[A_LOD] = Wrapper_DSO_1_243_Load;
	DevDat[108].d_acts[A_OPN] = Wrapper_DSO_1_243_Open;
	DevDat[108].d_acts[A_RST] = Wrapper_DSO_1_243_Reset;
	DevDat[108].d_acts[A_FNC] = Wrapper_DSO_1_243_Setup;
	DevDat[108].d_acts[A_STA] = Wrapper_DSO_1_243_Status;
//
//	DSO_1:CH244
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[109].d_modlst = p_mod;
	DevDat[109].d_fncP = 244;
	DevDat[109].d_acts[A_CLS] = Wrapper_DSO_1_244_Close;
	DevDat[109].d_acts[A_CON] = Wrapper_DSO_1_244_Connect;
	DevDat[109].d_acts[A_DIS] = Wrapper_DSO_1_244_Disconnect;
	DevDat[109].d_acts[A_FTH] = Wrapper_DSO_1_244_Fetch;
	DevDat[109].d_acts[A_INX] = Wrapper_DSO_1_244_Init;
	DevDat[109].d_acts[A_LOD] = Wrapper_DSO_1_244_Load;
	DevDat[109].d_acts[A_OPN] = Wrapper_DSO_1_244_Open;
	DevDat[109].d_acts[A_RST] = Wrapper_DSO_1_244_Reset;
	DevDat[109].d_acts[A_FNC] = Wrapper_DSO_1_244_Setup;
	DevDat[109].d_acts[A_STA] = Wrapper_DSO_1_244_Status;
//
//	DSO_1:CH245
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[110].d_modlst = p_mod;
	DevDat[110].d_fncP = 245;
	DevDat[110].d_acts[A_CLS] = Wrapper_DSO_1_245_Close;
	DevDat[110].d_acts[A_CON] = Wrapper_DSO_1_245_Connect;
	DevDat[110].d_acts[A_DIS] = Wrapper_DSO_1_245_Disconnect;
	DevDat[110].d_acts[A_FTH] = Wrapper_DSO_1_245_Fetch;
	DevDat[110].d_acts[A_INX] = Wrapper_DSO_1_245_Init;
	DevDat[110].d_acts[A_LOD] = Wrapper_DSO_1_245_Load;
	DevDat[110].d_acts[A_OPN] = Wrapper_DSO_1_245_Open;
	DevDat[110].d_acts[A_RST] = Wrapper_DSO_1_245_Reset;
	DevDat[110].d_acts[A_FNC] = Wrapper_DSO_1_245_Setup;
	DevDat[110].d_acts[A_STA] = Wrapper_DSO_1_245_Status;
//
//	DSO_1:CH246
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[111].d_modlst = p_mod;
	DevDat[111].d_fncP = 246;
	DevDat[111].d_acts[A_CLS] = Wrapper_DSO_1_246_Close;
	DevDat[111].d_acts[A_CON] = Wrapper_DSO_1_246_Connect;
	DevDat[111].d_acts[A_DIS] = Wrapper_DSO_1_246_Disconnect;
	DevDat[111].d_acts[A_FTH] = Wrapper_DSO_1_246_Fetch;
	DevDat[111].d_acts[A_INX] = Wrapper_DSO_1_246_Init;
	DevDat[111].d_acts[A_LOD] = Wrapper_DSO_1_246_Load;
	DevDat[111].d_acts[A_OPN] = Wrapper_DSO_1_246_Open;
	DevDat[111].d_acts[A_RST] = Wrapper_DSO_1_246_Reset;
	DevDat[111].d_acts[A_FNC] = Wrapper_DSO_1_246_Setup;
	DevDat[111].d_acts[A_STA] = Wrapper_DSO_1_246_Status;
//
//	DSO_1:CH247
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[112].d_modlst = p_mod;
	DevDat[112].d_fncP = 247;
	DevDat[112].d_acts[A_CLS] = Wrapper_DSO_1_247_Close;
	DevDat[112].d_acts[A_CON] = Wrapper_DSO_1_247_Connect;
	DevDat[112].d_acts[A_DIS] = Wrapper_DSO_1_247_Disconnect;
	DevDat[112].d_acts[A_FTH] = Wrapper_DSO_1_247_Fetch;
	DevDat[112].d_acts[A_INX] = Wrapper_DSO_1_247_Init;
	DevDat[112].d_acts[A_LOD] = Wrapper_DSO_1_247_Load;
	DevDat[112].d_acts[A_OPN] = Wrapper_DSO_1_247_Open;
	DevDat[112].d_acts[A_RST] = Wrapper_DSO_1_247_Reset;
	DevDat[112].d_acts[A_FNC] = Wrapper_DSO_1_247_Setup;
	DevDat[112].d_acts[A_STA] = Wrapper_DSO_1_247_Status;
//
//	DSO_1:CH248
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[113].d_modlst = p_mod;
	DevDat[113].d_fncP = 248;
	DevDat[113].d_acts[A_CLS] = Wrapper_DSO_1_248_Close;
	DevDat[113].d_acts[A_CON] = Wrapper_DSO_1_248_Connect;
	DevDat[113].d_acts[A_DIS] = Wrapper_DSO_1_248_Disconnect;
	DevDat[113].d_acts[A_FTH] = Wrapper_DSO_1_248_Fetch;
	DevDat[113].d_acts[A_INX] = Wrapper_DSO_1_248_Init;
	DevDat[113].d_acts[A_LOD] = Wrapper_DSO_1_248_Load;
	DevDat[113].d_acts[A_OPN] = Wrapper_DSO_1_248_Open;
	DevDat[113].d_acts[A_RST] = Wrapper_DSO_1_248_Reset;
	DevDat[113].d_acts[A_FNC] = Wrapper_DSO_1_248_Setup;
	DevDat[113].d_acts[A_STA] = Wrapper_DSO_1_248_Status;
//
//	DSO_1:CH249
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[114].d_modlst = p_mod;
	DevDat[114].d_fncP = 249;
	DevDat[114].d_acts[A_CLS] = Wrapper_DSO_1_249_Close;
	DevDat[114].d_acts[A_CON] = Wrapper_DSO_1_249_Connect;
	DevDat[114].d_acts[A_DIS] = Wrapper_DSO_1_249_Disconnect;
	DevDat[114].d_acts[A_FTH] = Wrapper_DSO_1_249_Fetch;
	DevDat[114].d_acts[A_INX] = Wrapper_DSO_1_249_Init;
	DevDat[114].d_acts[A_LOD] = Wrapper_DSO_1_249_Load;
	DevDat[114].d_acts[A_OPN] = Wrapper_DSO_1_249_Open;
	DevDat[114].d_acts[A_RST] = Wrapper_DSO_1_249_Reset;
	DevDat[114].d_acts[A_FNC] = Wrapper_DSO_1_249_Setup;
	DevDat[114].d_acts[A_STA] = Wrapper_DSO_1_249_Status;
//
//	DSO_1:CH250
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[115].d_modlst = p_mod;
	DevDat[115].d_fncP = 250;
	DevDat[115].d_acts[A_CLS] = Wrapper_DSO_1_250_Close;
	DevDat[115].d_acts[A_CON] = Wrapper_DSO_1_250_Connect;
	DevDat[115].d_acts[A_DIS] = Wrapper_DSO_1_250_Disconnect;
	DevDat[115].d_acts[A_FTH] = Wrapper_DSO_1_250_Fetch;
	DevDat[115].d_acts[A_INX] = Wrapper_DSO_1_250_Init;
	DevDat[115].d_acts[A_LOD] = Wrapper_DSO_1_250_Load;
	DevDat[115].d_acts[A_OPN] = Wrapper_DSO_1_250_Open;
	DevDat[115].d_acts[A_RST] = Wrapper_DSO_1_250_Reset;
	DevDat[115].d_acts[A_FNC] = Wrapper_DSO_1_250_Setup;
	DevDat[115].d_acts[A_STA] = Wrapper_DSO_1_250_Status;
//
//	DSO_1:CH251
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_BAND);  // bandwidth
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_DUTY);  // duty-cycle
	p_mod = BldModDat (p_mod, (short) M_FALL);  // fall-time
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_PLWD);  // pulse-width
	p_mod = BldModDat (p_mod, (short) M_RISE);  // rise-time
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VPKN);  // voltage-p-neg
	p_mod = BldModDat (p_mod, (short) M_VPKP);  // voltage-p-pos
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[116].d_modlst = p_mod;
	DevDat[116].d_fncP = 251;
	DevDat[116].d_acts[A_CLS] = Wrapper_DSO_1_251_Close;
	DevDat[116].d_acts[A_CON] = Wrapper_DSO_1_251_Connect;
	DevDat[116].d_acts[A_DIS] = Wrapper_DSO_1_251_Disconnect;
	DevDat[116].d_acts[A_FTH] = Wrapper_DSO_1_251_Fetch;
	DevDat[116].d_acts[A_INX] = Wrapper_DSO_1_251_Init;
	DevDat[116].d_acts[A_LOD] = Wrapper_DSO_1_251_Load;
	DevDat[116].d_acts[A_OPN] = Wrapper_DSO_1_251_Open;
	DevDat[116].d_acts[A_RST] = Wrapper_DSO_1_251_Reset;
	DevDat[116].d_acts[A_FNC] = Wrapper_DSO_1_251_Setup;
	DevDat[116].d_acts[A_STA] = Wrapper_DSO_1_251_Status;
//
//	DSO_1:CH252
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CMWV);  // compare-wave
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_LDVW);  // load-wave
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SVFM);  // save-from
	p_mod = BldModDat (p_mod, (short) M_SVTO);  // save-to
	p_mod = BldModDat (p_mod, (short) M_SVWV);  // save-wave
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[117].d_modlst = p_mod;
	DevDat[117].d_fncP = 252;
	DevDat[117].d_acts[A_CLS] = Wrapper_DSO_1_252_Close;
	DevDat[117].d_acts[A_CON] = Wrapper_DSO_1_252_Connect;
	DevDat[117].d_acts[A_DIS] = Wrapper_DSO_1_252_Disconnect;
	DevDat[117].d_acts[A_FTH] = Wrapper_DSO_1_252_Fetch;
	DevDat[117].d_acts[A_INX] = Wrapper_DSO_1_252_Init;
	DevDat[117].d_acts[A_LOD] = Wrapper_DSO_1_252_Load;
	DevDat[117].d_acts[A_OPN] = Wrapper_DSO_1_252_Open;
	DevDat[117].d_acts[A_RST] = Wrapper_DSO_1_252_Reset;
	DevDat[117].d_acts[A_FNC] = Wrapper_DSO_1_252_Setup;
	DevDat[117].d_acts[A_STA] = Wrapper_DSO_1_252_Status;
//
//	DSO_1:CH253
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CMWV);  // compare-wave
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_LDFM);  // load-from
	p_mod = BldModDat (p_mod, (short) M_LDVW);  // load-wave
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SVWV);  // save-wave
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[118].d_modlst = p_mod;
	DevDat[118].d_fncP = 253;
	DevDat[118].d_acts[A_CLS] = Wrapper_DSO_1_253_Close;
	DevDat[118].d_acts[A_CON] = Wrapper_DSO_1_253_Connect;
	DevDat[118].d_acts[A_DIS] = Wrapper_DSO_1_253_Disconnect;
	DevDat[118].d_acts[A_FTH] = Wrapper_DSO_1_253_Fetch;
	DevDat[118].d_acts[A_INX] = Wrapper_DSO_1_253_Init;
	DevDat[118].d_acts[A_LOD] = Wrapper_DSO_1_253_Load;
	DevDat[118].d_acts[A_OPN] = Wrapper_DSO_1_253_Open;
	DevDat[118].d_acts[A_RST] = Wrapper_DSO_1_253_Reset;
	DevDat[118].d_acts[A_FNC] = Wrapper_DSO_1_253_Setup;
	DevDat[118].d_acts[A_STA] = Wrapper_DSO_1_253_Status;
//
//	DSO_1:CH254
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ALLW);  // allowance
	p_mod = BldModDat (p_mod, (short) M_CMWV);  // compare-wave
	p_mod = BldModDat (p_mod, (short) M_CMCH);  // compare-ch
	p_mod = BldModDat (p_mod, (short) M_CMTO);  // compare-to
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_LDVW);  // load-wave
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_SVWV);  // save-wave
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[119].d_modlst = p_mod;
	DevDat[119].d_fncP = 254;
	DevDat[119].d_acts[A_CLS] = Wrapper_DSO_1_254_Close;
	DevDat[119].d_acts[A_CON] = Wrapper_DSO_1_254_Connect;
	DevDat[119].d_acts[A_DIS] = Wrapper_DSO_1_254_Disconnect;
	DevDat[119].d_acts[A_FTH] = Wrapper_DSO_1_254_Fetch;
	DevDat[119].d_acts[A_INX] = Wrapper_DSO_1_254_Init;
	DevDat[119].d_acts[A_LOD] = Wrapper_DSO_1_254_Load;
	DevDat[119].d_acts[A_OPN] = Wrapper_DSO_1_254_Open;
	DevDat[119].d_acts[A_RST] = Wrapper_DSO_1_254_Reset;
	DevDat[119].d_acts[A_FNC] = Wrapper_DSO_1_254_Setup;
	DevDat[119].d_acts[A_STA] = Wrapper_DSO_1_254_Status;
//
//	DSO_1:CH255
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ADFM);  // add-from
	p_mod = BldModDat (p_mod, (short) M_ADTO);  // add-to
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[120].d_modlst = p_mod;
	DevDat[120].d_fncP = 255;
	DevDat[120].d_acts[A_CLS] = Wrapper_DSO_1_255_Close;
	DevDat[120].d_acts[A_CON] = Wrapper_DSO_1_255_Connect;
	DevDat[120].d_acts[A_DIS] = Wrapper_DSO_1_255_Disconnect;
	DevDat[120].d_acts[A_FTH] = Wrapper_DSO_1_255_Fetch;
	DevDat[120].d_acts[A_INX] = Wrapper_DSO_1_255_Init;
	DevDat[120].d_acts[A_LOD] = Wrapper_DSO_1_255_Load;
	DevDat[120].d_acts[A_OPN] = Wrapper_DSO_1_255_Open;
	DevDat[120].d_acts[A_RST] = Wrapper_DSO_1_255_Reset;
	DevDat[120].d_acts[A_FNC] = Wrapper_DSO_1_255_Setup;
	DevDat[120].d_acts[A_STA] = Wrapper_DSO_1_255_Status;
//
//	DSO_1:CH3
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_DIFR);  // differentiate
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[121].d_modlst = p_mod;
	DevDat[121].d_fncP = 3;
	DevDat[121].d_acts[A_CLS] = Wrapper_DSO_1_3_Close;
	DevDat[121].d_acts[A_CON] = Wrapper_DSO_1_3_Connect;
	DevDat[121].d_acts[A_DIS] = Wrapper_DSO_1_3_Disconnect;
	DevDat[121].d_acts[A_FTH] = Wrapper_DSO_1_3_Fetch;
	DevDat[121].d_acts[A_INX] = Wrapper_DSO_1_3_Init;
	DevDat[121].d_acts[A_LOD] = Wrapper_DSO_1_3_Load;
	DevDat[121].d_acts[A_OPN] = Wrapper_DSO_1_3_Open;
	DevDat[121].d_acts[A_RST] = Wrapper_DSO_1_3_Reset;
	DevDat[121].d_acts[A_FNC] = Wrapper_DSO_1_3_Setup;
	DevDat[121].d_acts[A_STA] = Wrapper_DSO_1_3_Status;
//
//	DSO_1:CH4
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DEST);  // destination
	p_mod = BldModDat (p_mod, (short) M_INTG);  // integrate
	p_mod = BldModDat (p_mod, (short) M_MATH);  // math
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[122].d_modlst = p_mod;
	DevDat[122].d_fncP = 4;
	DevDat[122].d_acts[A_CLS] = Wrapper_DSO_1_4_Close;
	DevDat[122].d_acts[A_CON] = Wrapper_DSO_1_4_Connect;
	DevDat[122].d_acts[A_DIS] = Wrapper_DSO_1_4_Disconnect;
	DevDat[122].d_acts[A_FTH] = Wrapper_DSO_1_4_Fetch;
	DevDat[122].d_acts[A_INX] = Wrapper_DSO_1_4_Init;
	DevDat[122].d_acts[A_LOD] = Wrapper_DSO_1_4_Load;
	DevDat[122].d_acts[A_OPN] = Wrapper_DSO_1_4_Open;
	DevDat[122].d_acts[A_RST] = Wrapper_DSO_1_4_Reset;
	DevDat[122].d_acts[A_FNC] = Wrapper_DSO_1_4_Setup;
	DevDat[122].d_acts[A_STA] = Wrapper_DSO_1_4_Status;
//
//	DSO_1:CH5
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_SMPL);  // sample
	p_mod = BldModDat (p_mod, (short) M_SATM);  // sample-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[123].d_modlst = p_mod;
	DevDat[123].d_fncP = 5;
	DevDat[123].d_acts[A_CLS] = Wrapper_DSO_1_5_Close;
	DevDat[123].d_acts[A_CON] = Wrapper_DSO_1_5_Connect;
	DevDat[123].d_acts[A_DIS] = Wrapper_DSO_1_5_Disconnect;
	DevDat[123].d_acts[A_FTH] = Wrapper_DSO_1_5_Fetch;
	DevDat[123].d_acts[A_INX] = Wrapper_DSO_1_5_Init;
	DevDat[123].d_acts[A_LOD] = Wrapper_DSO_1_5_Load;
	DevDat[123].d_acts[A_OPN] = Wrapper_DSO_1_5_Open;
	DevDat[123].d_acts[A_RST] = Wrapper_DSO_1_5_Reset;
	DevDat[123].d_acts[A_FNC] = Wrapper_DSO_1_5_Setup;
	DevDat[123].d_acts[A_STA] = Wrapper_DSO_1_5_Status;
//
//	DSO_1:CH6
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[124].d_modlst = p_mod;
	DevDat[124].d_fncP = 6;
	DevDat[124].d_acts[A_CLS] = Wrapper_DSO_1_6_Close;
	DevDat[124].d_acts[A_CON] = Wrapper_DSO_1_6_Connect;
	DevDat[124].d_acts[A_DIS] = Wrapper_DSO_1_6_Disconnect;
	DevDat[124].d_acts[A_FTH] = Wrapper_DSO_1_6_Fetch;
	DevDat[124].d_acts[A_INX] = Wrapper_DSO_1_6_Init;
	DevDat[124].d_acts[A_LOD] = Wrapper_DSO_1_6_Load;
	DevDat[124].d_acts[A_OPN] = Wrapper_DSO_1_6_Open;
	DevDat[124].d_acts[A_RST] = Wrapper_DSO_1_6_Reset;
	DevDat[124].d_acts[A_FNC] = Wrapper_DSO_1_6_Setup;
	DevDat[124].d_acts[A_STA] = Wrapper_DSO_1_6_Status;
//
//	DSO_1:CH7
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_EVOU);  // event-out
	p_mod = BldModDat (p_mod, (short) M_EVDL);  // event-delay
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_EVSL);  // event-slope
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[125].d_modlst = p_mod;
	DevDat[125].d_fncP = 7;
	DevDat[125].d_acts[A_CLS] = Wrapper_DSO_1_7_Close;
	DevDat[125].d_acts[A_CON] = Wrapper_DSO_1_7_Connect;
	DevDat[125].d_acts[A_DIS] = Wrapper_DSO_1_7_Disconnect;
	DevDat[125].d_acts[A_FTH] = Wrapper_DSO_1_7_Fetch;
	DevDat[125].d_acts[A_INX] = Wrapper_DSO_1_7_Init;
	DevDat[125].d_acts[A_LOD] = Wrapper_DSO_1_7_Load;
	DevDat[125].d_acts[A_OPN] = Wrapper_DSO_1_7_Open;
	DevDat[125].d_acts[A_RST] = Wrapper_DSO_1_7_Reset;
	DevDat[125].d_acts[A_FNC] = Wrapper_DSO_1_7_Setup;
	DevDat[125].d_acts[A_STA] = Wrapper_DSO_1_7_Status;
	return 0;
}
