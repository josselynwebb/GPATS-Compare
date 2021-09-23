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
	"COM","DCF","DCS","DGT","DIS","DME","DOP","EAR",
	"EMF","EPW","EVS","EVT","EXC","FLU","FMS","HEA",
	"IFF","ILS","IMP","INF","LAS","LCL","LDT","LGT",
	"LLD","LRF","LTR","MAN","MDS","MIF","MIL","PAC",
	"PAM","PAT","PDC","PDP","PDT","PMS","RDN","RDS",
	"RPS","RSL","RTN","SCS","SHT","SIM","SIN","SQW",
	"STM","STS","SYN","TAC","TDG","TED","TMI","TMR",
	"TRI","VBR","VID","VOR","WAV",};
//
//	MODIFIERS
//
DECLAREC int m__cnt = M__CNT;
DECLAREC char *MCiilTxt [] = {
	"","ACCF","ACCP","ADFM","ADLN","ADTO","AGER","ALLW",
	"ALTI","ALTR","AMCP","AMCU","AMFQ","AMMC","AMMF","AMPL",
	"AMSH","AMSR","ANAC","ANAX","ANAY","ANAZ","ANGL","ANGP",
	"ANGT","ANGX","ANGY","ANGZ","ANRT","ANRX","ANRY","ANRZ",
	"ANSD","ATMS","ATTE","ATTN","AUCO","BAND","BARP","BDTH",
	"BITP","BITR","BKPH","BRAN","BTRN","BURD","BURR","BURS",
	"BUSM","BUSS","CAMP","CAPA","CCOM","CDAT","CFRQ","CHAN",
	"CHID","CHIT","CHRM","CLKS","CMCH","CMDW","CMPL","CMTO",
	"CMWB","CMWV","COMD","COND","COUN","CPHS","CPKN","CPKP",
	"CPLG","CRSD","CRSF","CSTS","CTRQ","CUPK","CUPP","CUR0",
	"CUR1","CURA","CURI","CURL","CURQ","CURR","CURT","CWLV",
	"DATA","DATL","DATP","DATS","DATT","DATW","DBLI","DBND",
	"DBRC","DBRS","DCOF","DDMD","DEEM","DELA","DEST","DEWP",
	"DFBA","DIFR","DIFT","DIGS","DISP","DIST","DIVG","DIVS",
	"DMDS","DPFR","DPSH","DROO","DSFC","DSTR","DSTX","DSTY",
	"DSTZ","DTCT","DTMD","DTOR","DTSC","DUTY","DVPN","DVPP",
	"DWBT","EDLN","EDUT","EFCY","EFFI","EGDR","EINM","ERRI",
	"ERRO","EVAO","EVDL","EVEO","EVEV","EVFO","EVGB","EVGF",
	"EVGR","EVGT","EVOU","EVSB","EVSF","EVSL","EVSW","EVTF",
	"EVTI","EVTR","EVTT","EVUN","EVWH","EVXE","EVXM","EXAE",
	"EXNM","EXPO","FALL","FCLN","FCNT","FDST","FDVW","FIAL",
	"FILT","FLTC","FLTS","FLUT","FMCP","FMCU","FMFQ","FMSR",
	"FRCE","FRCR","FREQ","FRMT","FRQ0","FRQ1","FRQD","FRQP",
	"FRQQ","FRQR","FRQW","FUEL","FXDN","FXIP","FXQD","GAMA",
	"GSLP","HAPW","HARM","HARN","HARP","HARV","HFOV","HIZZ",
	"HLAE","HMDF","HRAG","HSRM","HTAG","HTOF","HUMY","IASP",
	"ICWB","IDSE","IDSF","IDSG","IDSM","IDWB","IJIT","ILLU",
	"INDU","INTG","INTL","IRAT","ISTI","ISWB","ITER","ITRO",
	"IVCW","IVDL","IVDP","IVDS","IVDT","IVDW","IVMG","IVOA",
	"IVRT","IVSW","IVWC","IVWG","IVWL","IVZA","IVZC","LDFM",
	"LDTO","LDVW","LINE","LIPF","LMDF","LMIN","LOCL","LRAN",
	"LSAE","LSTG","LUMF","LUMI","LUMT","MAGB","MAGR","MAMP",
	"MANI","MASF","MASK","MATH","MAXT","MDPN","MDPP","MDSC",
	"MGAP","MMOD","MODD","MODE","MODF","MODO","MODP","MPFM",
	"MPTO","MRCO","MRKB","MRTD","MSKZ","MSNR","MTFD","MTFP",
	"MTFU","NEDT","NEGS","NHAR","NLIN","NOAD","NOAV","NOIS",
	"NOPD","NOPK","NOPP","NOTR","NPWT","OAMP","OTMP","OVER",
	"P3DV","P3LV","PAMP","PANG","PARE","PARO","PATH","PATT",
	"PCCU","PCLS","PCSR","PDEV","PDGN","PDRP","PERI","PHPN",
	"PHPP","PJIT","PKDV","PLAN","PLAR","PLEG","PLID","PLSE",
	"PLSI","PLWD","PMCU","PMFQ","PMSR","PODN","POSI","POSS",
	"POWA","POWP","POWR","PPOS","PPWT","PRCD","PRDF","PRFR",
	"PRIO","PROA","PROF","PRSA","PRSG","PRSR","PSHI","PSHT",
	"PSPC","PSPE","PSPT","PWRL","QFAC","QUAD","RADL","RADR",
	"RDNC","REAC","REFF","REFI","REFM","REFP","REFR","REFU",
	"REFV","REFX","RELB","RELH","RELW","REPT","RESB","RESI",
	"RESP","RESR","RING","RISE","RLBR","RLVL","RMNS","RMOD",
	"RMPS","ROUN","RPDV","RPEC","RPHF","RPLD","RPLE","RPLI",
	"RPLX","RSPH","RSPO","RSPT","RSPZ","RTRS","SASP","SATM",
	"SBCF","SBCM","SBEV","SBFM","SBTO","SCNT","SDEL","SERL",
	"SERM","SESA","SETT","SGNO","SGTF","SHFS","SIMU","SITF",
	"SKEW","SLEW","SLRA","SLRG","SLRR","SLSD","SLSL","SMAV",
	"SMPL","SMPW","SMTH","SNAD","SNSR","SPCG","SPED","SPGR",
	"SPTM","SQD1","SQD2","SQD3","SQTD","SQTR","SRFR","SSMD",
	"STAT","STBM","STIM","STLN","STMH","STMO","STMP","STMR",
	"STMZ","STOP","STPA","STPG","STPR","STPT","STRD","STRT",
	"STUT","STWD","SUSP","SVCP","SVFM","SVTO","SVWV","SWBT",
	"SWPT","SWRA","SYDL","SYEV","SYNC","TASP","TASY","TCAP",
	"TCUR","TEFC","TEMP","TEQL","TEQT","TGMD","TGPL","TGTA",
	"TGTD","TGTH","TGTP","TGTR","THRT","TIEV","TILT","TIME",
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
	"WTRN","XAGR","XBAG","XTAR","YAGR","YBAG","YTAR","ZAMP",
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
	"","1553A","1553B","ALLLS","AMI","AR429","ARDC","BIP",
	"CONMD","CONRT","CSM","CSN","CSOC","HDB","ICAN","ICAO",
	"IEEE488","LNGTH","MASTR","MIP","MONTR","NRZ","OFF","ON",
	"PARA","PRIM","PRTY","REDT","RS232","RS422","RTCON","RTRT",
	"RZ","SERL","SERM","SLAVE","SYNC","TLKLS","TR","WADC",
	};
//
//	DIMS-B
//
DECLAREC int r__cnt = R__CNT;
DECLAREC char *RCiilTxt [] = {
	"",};
extern int doSWITCH_Close ();
extern int doSWITCH_Connect ();
extern int doSWITCH_Disconnect ();
extern int doSWITCH_Open ();
extern int doSWITCH_Setup ();
extern int CCALLBACK doIfc (void);
extern int CCALLBACK doDcl (void);
extern int CCALLBACK doUnload (void);
extern int CCALLBACK doOpen (void);
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"SWITCH:CH1",
};
DECLAREC int DevCnt = 3;
int CCALLBACK Wrapper_SWITCH_1_Close(void)
{
	if (doSWITCH_Close() < 0)
		BusErr ("SWITCH");
	return 0;
}
int CCALLBACK Wrapper_SWITCH_1_Connect(void)
{
	if (doSWITCH_Connect() < 0)
		BusErr ("SWITCH");
	return 0;
}
int CCALLBACK Wrapper_SWITCH_1_Disconnect(void)
{
	if (doSWITCH_Disconnect() < 0)
		BusErr ("SWITCH");
	return 0;
}
int CCALLBACK Wrapper_SWITCH_1_Open(void)
{
	if (doSWITCH_Open() < 0)
		BusErr ("SWITCH");
	return 0;
}
int CCALLBACK Wrapper_SWITCH_1_Setup(void)
{
	if (doSWITCH_Setup() < 0)
		BusErr ("SWITCH");
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
	DevDat[1].d_acts[A_FNC] = doIfc;
	DevDat[1].d_acts[A_STA] = doDcl;
	DevDat[1].d_acts[A_DIS] = doUnload;
	DevDat[1].d_acts[A_OPN] = doOpen;
//
//	SWITCH:CH1
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 1;
	DevDat[2].d_acts[A_CLS] = Wrapper_SWITCH_1_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_SWITCH_1_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_SWITCH_1_Disconnect;
	DevDat[2].d_acts[A_OPN] = Wrapper_SWITCH_1_Open;
	DevDat[2].d_acts[A_FNC] = Wrapper_SWITCH_1_Setup;
	return 0;
}
