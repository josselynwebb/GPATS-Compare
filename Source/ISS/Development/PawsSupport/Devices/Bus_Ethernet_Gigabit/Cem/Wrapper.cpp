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
extern int doETHERNET_Close ();
extern int doETHERNET_Connect ();
extern int doETHERNET_Disconnect ();
extern int doETHERNET_Fetch ();
extern int doETHERNET_Init ();
extern int doETHERNET_Load ();
extern int doETHERNET_Open ();
extern int doETHERNET_Reset ();
extern int doETHERNET_Setup ();
extern int doETHERNET_Status ();
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"ETHERNET_1:CH1",
	"ETHERNET_1:CH2",
	"ETHERNET_2:CH1",
	"ETHERNET_2:CH2",
};
DECLAREC int DevCnt = 6;
int CCALLBACK Wrapper_ETHERNET_1_1_Close(void)
{
	if (doETHERNET_Close() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Connect(void)
{
	if (doETHERNET_Connect() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Disconnect(void)
{
	if (doETHERNET_Disconnect() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Fetch(void)
{
	if (doETHERNET_Fetch() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Init(void)
{
	if (doETHERNET_Init() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Load(void)
{
	if (doETHERNET_Load() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Open(void)
{
	if (doETHERNET_Open() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Reset(void)
{
	if (doETHERNET_Reset() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Setup(void)
{
	if (doETHERNET_Setup() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_1_Status(void)
{
	if (doETHERNET_Status() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Close(void)
{
	if (doETHERNET_Close() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Connect(void)
{
	if (doETHERNET_Connect() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Disconnect(void)
{
	if (doETHERNET_Disconnect() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Fetch(void)
{
	if (doETHERNET_Fetch() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Init(void)
{
	if (doETHERNET_Init() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Load(void)
{
	if (doETHERNET_Load() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Open(void)
{
	if (doETHERNET_Open() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Reset(void)
{
	if (doETHERNET_Reset() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Setup(void)
{
	if (doETHERNET_Setup() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_1_2_Status(void)
{
	if (doETHERNET_Status() < 0)
		BusErr ("ETHERNET_1");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Close(void)
{
	if (doETHERNET_Close() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Connect(void)
{
	if (doETHERNET_Connect() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Disconnect(void)
{
	if (doETHERNET_Disconnect() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Fetch(void)
{
	if (doETHERNET_Fetch() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Init(void)
{
	if (doETHERNET_Init() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Load(void)
{
	if (doETHERNET_Load() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Open(void)
{
	if (doETHERNET_Open() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Reset(void)
{
	if (doETHERNET_Reset() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Setup(void)
{
	if (doETHERNET_Setup() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_1_Status(void)
{
	if (doETHERNET_Status() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Close(void)
{
	if (doETHERNET_Close() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Connect(void)
{
	if (doETHERNET_Connect() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Disconnect(void)
{
	if (doETHERNET_Disconnect() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Fetch(void)
{
	if (doETHERNET_Fetch() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Init(void)
{
	if (doETHERNET_Init() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Load(void)
{
	if (doETHERNET_Load() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Open(void)
{
	if (doETHERNET_Open() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Reset(void)
{
	if (doETHERNET_Reset() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Setup(void)
{
	if (doETHERNET_Setup() < 0)
		BusErr ("ETHERNET_2");
	return 0;
}
int CCALLBACK Wrapper_ETHERNET_2_2_Status(void)
{
	if (doETHERNET_Status() < 0)
		BusErr ("ETHERNET_2");
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
//	ETHERNET_1:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRCD);  // proceed
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_PTCP);  // tcp
	p_mod = BldModDat (p_mod, (short) M_PUDP);  // udp
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_ETHERNET_1_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_ETHERNET_1_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_ETHERNET_1_1_Disconnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_ETHERNET_1_1_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_ETHERNET_1_1_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_ETHERNET_1_1_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_ETHERNET_1_1_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_ETHERNET_1_1_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_ETHERNET_1_1_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_ETHERNET_1_1_Status;
//
//	ETHERNET_1:CH2
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
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 2;
	DevDat[3].d_acts[A_CLS] = Wrapper_ETHERNET_1_2_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_ETHERNET_1_2_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_ETHERNET_1_2_Disconnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_ETHERNET_1_2_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_ETHERNET_1_2_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_ETHERNET_1_2_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_ETHERNET_1_2_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_ETHERNET_1_2_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_ETHERNET_1_2_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_ETHERNET_1_2_Status;
//
//	ETHERNET_2:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_PRCD);  // proceed
	p_mod = BldModDat (p_mod, (short) M_STRD);  // standard
	p_mod = BldModDat (p_mod, (short) M_PTCP);  // tcp
	p_mod = BldModDat (p_mod, (short) M_PUDP);  // udp
	p_mod = BldModDat (p_mod, (short) M_WAIT);  // wait
	p_mod = BldModDat (p_mod, (short) M_BUSS);  // bus-spec
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 1;
	DevDat[4].d_acts[A_CLS] = Wrapper_ETHERNET_2_1_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_ETHERNET_2_1_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_ETHERNET_2_1_Disconnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_ETHERNET_2_1_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_ETHERNET_2_1_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_ETHERNET_2_1_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_ETHERNET_2_1_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_ETHERNET_2_1_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_ETHERNET_2_1_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_ETHERNET_2_1_Status;
//
//	ETHERNET_2:CH2
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
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 2;
	DevDat[5].d_acts[A_CLS] = Wrapper_ETHERNET_2_2_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_ETHERNET_2_2_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_ETHERNET_2_2_Disconnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_ETHERNET_2_2_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_ETHERNET_2_2_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_ETHERNET_2_2_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_ETHERNET_2_2_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_ETHERNET_2_2_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_ETHERNET_2_2_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_ETHERNET_2_2_Status;
	return 0;
}
