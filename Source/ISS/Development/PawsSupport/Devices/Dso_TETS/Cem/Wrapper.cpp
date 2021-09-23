// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2021-03-03 14:22:4#$: Date of last commit
//    $Rev:: 28146              $: Revision of last commit

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
	"RCUR","RCVS","RDNC","REAC","REFF","REFI","REFM","REFP",
	"REFR","REFS","REFU","REFV","REFX","RELB","RELH","RELW",
	"REPT","RERR","RESB","RESI","RESP","RESR","RING","RISE",
	"RLBR","RLVL","RMC1","RMC2","RMNS","RMOD","RMPS","ROUN",
	"RPDV","RPEC","RPHF","RPLD","RPLE","RPLI","RPLX","RSPH",
	"RSPO","RSPT","RSPZ","RTRS","SASP","SATM","SBCF","SBCM",
	"SBEV","SBFM","SBTO","SCNT","SDEL","SERL","SERM","SESA",
	"SETT","SGNO","SGTF","SHFS","SIMU","SITF","SKEW","SLEW",
	"SLRA","SLRG","SLRR","SLSD","SLSL","SMAV","SMPL","SMPW",
	"SMTH","SNAD","SNFL","SNSR","SPCG","SPED","SPGR","SPRT",
	"SPTM","SQD1","SQD2","SQD3","SQTD","SQTR","SRFR","SSMD",
	"STAT","STIM","STLN","STMH","STMO","STMP","STMR","STMZ",
	"STOP","STPA","STPB","STPG","STPR","STPT","STRD","STRT",
	"STUT","STWD","SUSP","SVCP","SVFM","SVTO","SVWV","SWBT",
	"SWPT","SWRA","SYDL","SYEV","SYNC","TASP","TASY","TCAP",
	"TCBT","TCLT","TCRT","TCTP","TCUR","TDAT","TEFC","TEMP",
	"TEQL","TEQT","TERM","TGMD","TGPL","TGTA","TGTD","TGTH",
	"TGTP","TGTR","TGTS","THRT","THSM","TIEV","TILT","TIME",
	"TIMP","TIND","TIUN","TIWH","TJIT","TLAX","TMON","TMVL",
	"TOPA","TOPG","TOPR","TORQ","TPHD","TPHY","TREA","TRES",
	"TRGS","TRIG","TRLV","TRN0","TRN1","TRNP","TRNS","TROL",
	"TRSL","TRUE","TRUN","TRWH","TSAC","TSCC","TSIM","TSPC",
	"TSTF","TTMP","TVOL","TYPE","UNDR","UNFY","UNIT","UUPL",
	"UUTL","UUTT","VALU","VBAC","VBAN","VBAP","VBPP","VBRT",
	"VBTR","VDIV","VEAO","VEDL","VEEO","VEFO","VEGF","VETF",
	"VFOV","VINS","VIST","VLAE","VLAV","VLPK","VLPP","VLT0",
	"VLT1","VLTL","VLTQ","VLTR","VLTS","VOLF","VOLR","VOLT",
	"VPHF","VPHM","VPKN","VPKP","VRAG","VRMS","VSRM","VTAG",
	"VTOF","WAIT","WAVE","WDLN","WDRT","WGAP","WILD","WIND",
	"WRDC","WTRN","XACE","XAGR","XBAG","XTAR","YACE","YAGR",
	"YBAG","YTAR","ZAMP","ZCRS","ZERO",};
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
extern int IsSimOrDeb(char dev_name[20]);
extern int doDIG_SCOPE_101_Close ();
extern int doDIG_SCOPE_101_Connect ();
extern int doDIG_SCOPE_101_DisConnect ();
extern double doDIG_SCOPE_101_FetchFreq ();
extern int doDIG_SCOPE_101_Init ();
extern int doDIG_SCOPE_101_Load ();
extern int doDIG_SCOPE_101_Open ();
extern int doDIG_SCOPE_101_Reset ();
extern int doDIG_SCOPE_101_Setup ();
extern int doDIG_SCOPE_101_Status ();
extern int doDIG_SCOPE_102_Close ();
extern int doDIG_SCOPE_102_Connect ();
extern int doDIG_SCOPE_102_DisConnect ();
extern double doDIG_SCOPE_102_FetchPeriod ();
extern int doDIG_SCOPE_102_Init ();
extern int doDIG_SCOPE_102_Load ();
extern int doDIG_SCOPE_102_Open ();
extern int doDIG_SCOPE_102_Reset ();
extern int doDIG_SCOPE_102_Setup ();
extern int doDIG_SCOPE_102_Status ();
extern int doDIG_SCOPE_103_Close ();
extern int doDIG_SCOPE_103_Connect ();
extern int doDIG_SCOPE_103_DisConnect ();
extern double doDIG_SCOPE_103_FetchVoltagePp ();
extern int doDIG_SCOPE_103_Init ();
extern int doDIG_SCOPE_103_Load ();
extern int doDIG_SCOPE_103_Open ();
extern int doDIG_SCOPE_103_Reset ();
extern int doDIG_SCOPE_103_Setup ();
extern int doDIG_SCOPE_103_Status ();
extern int doDIG_SCOPE_104_Close ();
extern int doDIG_SCOPE_104_Connect ();
extern int doDIG_SCOPE_104_DisConnect ();
extern double doDIG_SCOPE_104_FetchVoltage ();
extern int doDIG_SCOPE_104_Init ();
extern int doDIG_SCOPE_104_Load ();
extern int doDIG_SCOPE_104_Open ();
extern int doDIG_SCOPE_104_Reset ();
extern int doDIG_SCOPE_104_Setup ();
extern int doDIG_SCOPE_104_Status ();
extern int doDIG_SCOPE_107_Close ();
extern int doDIG_SCOPE_107_Connect ();
extern int doDIG_SCOPE_107_DisConnect ();
extern double doDIG_SCOPE_107_FetchVoltagePp ();
extern int doDIG_SCOPE_107_Init ();
extern int doDIG_SCOPE_107_Load ();
extern int doDIG_SCOPE_107_Open ();
extern int doDIG_SCOPE_107_Reset ();
extern int doDIG_SCOPE_107_Setup ();
extern int doDIG_SCOPE_107_Status ();
extern int doDIG_SCOPE_108_Close ();
extern int doDIG_SCOPE_108_Connect ();
extern int doDIG_SCOPE_108_DisConnect ();
extern double doDIG_SCOPE_108_FetchVoltagePPos ();
extern int doDIG_SCOPE_108_Init ();
extern int doDIG_SCOPE_108_Load ();
extern int doDIG_SCOPE_108_Open ();
extern int doDIG_SCOPE_108_Reset ();
extern int doDIG_SCOPE_108_Setup ();
extern int doDIG_SCOPE_108_Status ();
extern int doDIG_SCOPE_109_Close ();
extern int doDIG_SCOPE_109_Connect ();
extern int doDIG_SCOPE_109_DisConnect ();
extern double doDIG_SCOPE_109_FetchVoltagePNeg ();
extern int doDIG_SCOPE_109_Init ();
extern int doDIG_SCOPE_109_Load ();
extern int doDIG_SCOPE_109_Open ();
extern int doDIG_SCOPE_109_Reset ();
extern int doDIG_SCOPE_109_Setup ();
extern int doDIG_SCOPE_109_Status ();
extern int doDIG_SCOPE_110_Close ();
extern int doDIG_SCOPE_110_Connect ();
extern int doDIG_SCOPE_110_DisConnect ();
extern double doDIG_SCOPE_110_FetchPrf ();
extern int doDIG_SCOPE_110_Init ();
extern int doDIG_SCOPE_110_Load ();
extern int doDIG_SCOPE_110_Open ();
extern int doDIG_SCOPE_110_Reset ();
extern int doDIG_SCOPE_110_Setup ();
extern int doDIG_SCOPE_110_Status ();
extern int doDIG_SCOPE_111_Close ();
extern int doDIG_SCOPE_111_Connect ();
extern int doDIG_SCOPE_111_DisConnect ();
extern double doDIG_SCOPE_111_FetchRiseTime ();
extern int doDIG_SCOPE_111_Init ();
extern int doDIG_SCOPE_111_Load ();
extern int doDIG_SCOPE_111_Open ();
extern int doDIG_SCOPE_111_Reset ();
extern int doDIG_SCOPE_111_Setup ();
extern int doDIG_SCOPE_111_Status ();
extern int doDIG_SCOPE_112_Close ();
extern int doDIG_SCOPE_112_Connect ();
extern int doDIG_SCOPE_112_DisConnect ();
extern double doDIG_SCOPE_112_FetchFallTime ();
extern int doDIG_SCOPE_112_Init ();
extern int doDIG_SCOPE_112_Load ();
extern int doDIG_SCOPE_112_Open ();
extern int doDIG_SCOPE_112_Reset ();
extern int doDIG_SCOPE_112_Setup ();
extern int doDIG_SCOPE_112_Status ();
extern int doDIG_SCOPE_113_Close ();
extern int doDIG_SCOPE_113_Connect ();
extern int doDIG_SCOPE_113_DisConnect ();
extern double doDIG_SCOPE_113_FetchPulseWidth ();
extern int doDIG_SCOPE_113_Init ();
extern int doDIG_SCOPE_113_Load ();
extern int doDIG_SCOPE_113_Open ();
extern int doDIG_SCOPE_113_Reset ();
extern int doDIG_SCOPE_113_Setup ();
extern int doDIG_SCOPE_113_Status ();
extern int doDIG_SCOPE_114_Close ();
extern int doDIG_SCOPE_114_Connect ();
extern int doDIG_SCOPE_114_DisConnect ();
extern double doDIG_SCOPE_114_FetchDutyCycle ();
extern int doDIG_SCOPE_114_Init ();
extern int doDIG_SCOPE_114_Load ();
extern int doDIG_SCOPE_114_Open ();
extern int doDIG_SCOPE_114_Reset ();
extern int doDIG_SCOPE_114_Setup ();
extern int doDIG_SCOPE_114_Status ();
extern int doDIG_SCOPE_115_Close ();
extern int doDIG_SCOPE_115_Connect ();
extern int doDIG_SCOPE_115_DisConnect ();
extern double doDIG_SCOPE_115_FetchOvershoot ();
extern int doDIG_SCOPE_115_Init ();
extern int doDIG_SCOPE_115_Load ();
extern int doDIG_SCOPE_115_Open ();
extern int doDIG_SCOPE_115_Reset ();
extern int doDIG_SCOPE_115_Setup ();
extern int doDIG_SCOPE_115_Status ();
extern int doDIG_SCOPE_116_Close ();
extern int doDIG_SCOPE_116_Connect ();
extern int doDIG_SCOPE_116_DisConnect ();
extern double doDIG_SCOPE_116_FetchPreshoot ();
extern int doDIG_SCOPE_116_Init ();
extern int doDIG_SCOPE_116_Load ();
extern int doDIG_SCOPE_116_Open ();
extern int doDIG_SCOPE_116_Reset ();
extern int doDIG_SCOPE_116_Setup ();
extern int doDIG_SCOPE_116_Status ();
extern int doDIG_SCOPE_117_Close ();
extern int doDIG_SCOPE_117_Connect ();
extern int doDIG_SCOPE_117_DisConnect ();
extern double doDIG_SCOPE_117_FetchAvVoltage ();
extern int doDIG_SCOPE_117_Init ();
extern int doDIG_SCOPE_117_Load ();
extern int doDIG_SCOPE_117_Open ();
extern int doDIG_SCOPE_117_Reset ();
extern int doDIG_SCOPE_117_Setup ();
extern int doDIG_SCOPE_117_Status ();
extern int doDIG_SCOPE_118_Close ();
extern int doDIG_SCOPE_118_Connect ();
extern int doDIG_SCOPE_118_DisConnect ();
extern double doDIG_SCOPE_118_FetchNegPulseWidth ();
extern int doDIG_SCOPE_118_Init ();
extern int doDIG_SCOPE_118_Load ();
extern int doDIG_SCOPE_118_Open ();
extern int doDIG_SCOPE_118_Reset ();
extern int doDIG_SCOPE_118_Setup ();
extern int doDIG_SCOPE_118_Status ();
extern int doDIG_SCOPE_119_Close ();
extern int doDIG_SCOPE_119_Connect ();
extern int doDIG_SCOPE_119_DisConnect ();
extern double doDIG_SCOPE_119_FetchPosPulseWidth ();
extern int doDIG_SCOPE_119_Init ();
extern int doDIG_SCOPE_119_Load ();
extern int doDIG_SCOPE_119_Open ();
extern int doDIG_SCOPE_119_Reset ();
extern int doDIG_SCOPE_119_Setup ();
extern int doDIG_SCOPE_119_Status ();
extern int doDIG_SCOPE_11_Close ();
extern int doDIG_SCOPE_11_Connect ();
extern int doDIG_SCOPE_11_DisConnect ();
extern int doDIG_SCOPE_11_Fetch ();
extern int doDIG_SCOPE_11_Init ();
extern int doDIG_SCOPE_11_Load ();
extern int doDIG_SCOPE_11_Open ();
extern int doDIG_SCOPE_11_Reset ();
extern int doDIG_SCOPE_11_Setup ();
extern int doDIG_SCOPE_11_Status ();
extern int doDIG_SCOPE_120_Close ();
extern int doDIG_SCOPE_120_Connect ();
extern int doDIG_SCOPE_120_DisConnect ();
extern int doDIG_SCOPE_120_FetchSample (
	// sample reading [list of decimals]
	double* pSMPL, int nCntSMPL);

extern int doDIG_SCOPE_120_Init ();
extern int doDIG_SCOPE_120_Load ();
extern int doDIG_SCOPE_120_Open ();
extern int doDIG_SCOPE_120_Reset ();
extern int doDIG_SCOPE_120_Setup ();
extern int doDIG_SCOPE_120_Status ();
extern int doDIG_SCOPE_121_Close ();
extern int doDIG_SCOPE_121_Connect ();
extern int doDIG_SCOPE_121_DisConnect ();
extern int doDIG_SCOPE_121_Fetch ();
extern int doDIG_SCOPE_121_Init ();
extern int doDIG_SCOPE_121_Load ();
extern int doDIG_SCOPE_121_Open ();
extern int doDIG_SCOPE_121_Reset ();
extern int doDIG_SCOPE_121_Setup ();
extern int doDIG_SCOPE_121_Status ();
extern int doDIG_SCOPE_122_Close ();
extern int doDIG_SCOPE_122_Connect ();
extern int doDIG_SCOPE_122_DisConnect ();
extern int doDIG_SCOPE_122_Fetch ();
extern int doDIG_SCOPE_122_Init ();
extern int doDIG_SCOPE_122_Load ();
extern int doDIG_SCOPE_122_Open ();
extern int doDIG_SCOPE_122_Reset ();
extern int doDIG_SCOPE_122_Setup ();
extern int doDIG_SCOPE_122_Status ();
extern int doDIG_SCOPE_123_Close ();
extern int doDIG_SCOPE_123_Connect ();
extern int doDIG_SCOPE_123_Disconnect ();
extern double doDIG_SCOPE_123_FetchVoltage ();
extern int doDIG_SCOPE_123_Init ();
extern int doDIG_SCOPE_123_Load ();
extern int doDIG_SCOPE_123_Open ();
extern int doDIG_SCOPE_123_Reset ();
extern int doDIG_SCOPE_123_Setup ();
extern int doDIG_SCOPE_123_Status ();
extern int doDIG_SCOPE_124_Close ();
extern int doDIG_SCOPE_124_Connect ();
extern int doDIG_SCOPE_124_Disconnect ();
extern double doDIG_SCOPE_124_FetchVoltageP ();
extern int doDIG_SCOPE_124_Init ();
extern int doDIG_SCOPE_124_Load ();
extern int doDIG_SCOPE_124_Open ();
extern int doDIG_SCOPE_124_Reset ();
extern int doDIG_SCOPE_124_Setup ();
extern int doDIG_SCOPE_124_Status ();
extern int doDIG_SCOPE_12_Close ();
extern int doDIG_SCOPE_12_Connect ();
extern int doDIG_SCOPE_12_DisConnect ();
extern int doDIG_SCOPE_12_Fetch ();
extern int doDIG_SCOPE_12_Init ();
extern int doDIG_SCOPE_12_Load ();
extern int doDIG_SCOPE_12_Open ();
extern int doDIG_SCOPE_12_Reset ();
extern int doDIG_SCOPE_12_Setup ();
extern int doDIG_SCOPE_12_Status ();
extern int doDIG_SCOPE_130_Close ();
extern int doDIG_SCOPE_130_Connect ();
extern int doDIG_SCOPE_130_DisConnect ();
extern int doDIG_SCOPE_130_FetchSaveWave (
		// save-wave reading [list of decimals]
		double* pSVWV, int nCntSVWV);
extern int doDIG_SCOPE_130_Init ();
extern int doDIG_SCOPE_130_Load ();
extern int doDIG_SCOPE_130_Open ();
extern int doDIG_SCOPE_130_Reset ();
extern int doDIG_SCOPE_130_Setup ();
extern int doDIG_SCOPE_130_Status ();
extern int doDIG_SCOPE_131_Close ();
extern int doDIG_SCOPE_131_Connect ();
extern int doDIG_SCOPE_131_DisConnect ();
extern int doDIG_SCOPE_131_FetchLoadWave (
		// save-wave reading [list of decimals]
		double* pSVWV, int nCntSVWV);
extern int doDIG_SCOPE_131_Init ();
extern int doDIG_SCOPE_131_Load ();
extern int doDIG_SCOPE_131_Open ();
extern int doDIG_SCOPE_131_Reset ();
extern int doDIG_SCOPE_131_Setup ();
extern int doDIG_SCOPE_131_Status ();
extern int doDIG_SCOPE_132_Close ();
extern int doDIG_SCOPE_132_Connect ();
extern int doDIG_SCOPE_132_DisConnect ();
extern double doDIG_SCOPE_132_FetchCompareWave ();
extern int doDIG_SCOPE_132_Init ();
extern int doDIG_SCOPE_132_Load ();
extern int doDIG_SCOPE_132_Open ();
extern int doDIG_SCOPE_132_Reset ();
extern int doDIG_SCOPE_132_Setup ();
extern int doDIG_SCOPE_132_Status ();
extern int doDIG_SCOPE_133_Close ();
extern int doDIG_SCOPE_133_Connect ();
extern int doDIG_SCOPE_133_DisConnect ();
extern int doDIG_SCOPE_133_FetchMath (
	// save-wave reading [list of decimals]
	double* pSVWV, int nCntSVWV);
extern int doDIG_SCOPE_133_Init ();
extern int doDIG_SCOPE_133_Load ();
extern int doDIG_SCOPE_133_Open ();
extern int doDIG_SCOPE_133_Reset ();
extern int doDIG_SCOPE_133_Setup ();
extern int doDIG_SCOPE_133_Status ();
extern int doDIG_SCOPE_134_Close ();
extern int doDIG_SCOPE_134_Connect ();
extern int doDIG_SCOPE_134_DisConnect ();
extern int doDIG_SCOPE_134_FetchMath (
	// save-wave reading [list of decimals]
	double* pSVWV, int nCntSVWV);
extern int doDIG_SCOPE_134_Init ();
extern int doDIG_SCOPE_134_Load ();
extern int doDIG_SCOPE_134_Open ();
extern int doDIG_SCOPE_134_Reset ();
extern int doDIG_SCOPE_134_Setup ();
extern int doDIG_SCOPE_134_Status ();
extern int doDIG_SCOPE_135_Close ();
extern int doDIG_SCOPE_135_Connect ();
extern int doDIG_SCOPE_135_DisConnect ();
extern int doDIG_SCOPE_135_FetchMath (
	// save-wave reading [list of decimals]
	double* pSVWV, int nCntSVWV);
extern int doDIG_SCOPE_135_Init ();
extern int doDIG_SCOPE_135_Load ();
extern int doDIG_SCOPE_135_Open ();
extern int doDIG_SCOPE_135_Reset ();
extern int doDIG_SCOPE_135_Setup ();
extern int doDIG_SCOPE_135_Status ();
extern int doDIG_SCOPE_136_Close ();
extern int doDIG_SCOPE_136_Connect ();
extern int doDIG_SCOPE_136_DisConnect ();
extern int doDIG_SCOPE_136_FetchMath (
	// save-wave reading [list of decimals]
	double* pSVWV, int nCntSVWV);
extern int doDIG_SCOPE_136_Init ();
extern int doDIG_SCOPE_136_Load ();
extern int doDIG_SCOPE_136_Open ();
extern int doDIG_SCOPE_136_Reset ();
extern int doDIG_SCOPE_136_Setup ();
extern int doDIG_SCOPE_136_Status ();
extern int doDIG_SCOPE_137_Close ();
extern int doDIG_SCOPE_137_Connect ();
extern int doDIG_SCOPE_137_DisConnect ();
extern int doDIG_SCOPE_137_FetchMath (
	// save-wave reading [list of decimals]
	double* pSVWV, int nCntSVWV);
extern int doDIG_SCOPE_137_Init ();
extern int doDIG_SCOPE_137_Load ();
extern int doDIG_SCOPE_137_Open ();
extern int doDIG_SCOPE_137_Reset ();
extern int doDIG_SCOPE_137_Setup ();
extern int doDIG_SCOPE_137_Status ();
extern int doDIG_SCOPE_13_Close ();
extern int doDIG_SCOPE_13_Connect ();
extern int doDIG_SCOPE_13_DisConnect ();
extern int doDIG_SCOPE_13_Fetch ();
extern int doDIG_SCOPE_13_Init ();
extern int doDIG_SCOPE_13_Load ();
extern int doDIG_SCOPE_13_Open ();
extern int doDIG_SCOPE_13_Reset ();
extern int doDIG_SCOPE_13_Setup ();
extern int doDIG_SCOPE_13_Status ();
extern int doDIG_SCOPE_14_Close ();
extern int doDIG_SCOPE_14_Connect ();
extern int doDIG_SCOPE_14_DisConnect ();
extern int doDIG_SCOPE_14_Fetch ();
extern int doDIG_SCOPE_14_Init ();
extern int doDIG_SCOPE_14_Load ();
extern int doDIG_SCOPE_14_Open ();
extern int doDIG_SCOPE_14_Reset ();
extern int doDIG_SCOPE_14_Setup ();
extern int doDIG_SCOPE_14_Status ();
extern int doDIG_SCOPE_201_Close ();
extern int doDIG_SCOPE_201_Connect ();
extern int doDIG_SCOPE_201_DisConnect ();
extern double doDIG_SCOPE_201_FetchFreq ();
extern int doDIG_SCOPE_201_Init ();
extern int doDIG_SCOPE_201_Load ();
extern int doDIG_SCOPE_201_Open ();
extern int doDIG_SCOPE_201_Reset ();
extern int doDIG_SCOPE_201_Setup ();
extern int doDIG_SCOPE_201_Status ();
extern int doDIG_SCOPE_202_Close ();
extern int doDIG_SCOPE_202_Connect ();
extern int doDIG_SCOPE_202_DisConnect ();
extern double doDIG_SCOPE_202_FetchPeriod ();
extern int doDIG_SCOPE_202_Init ();
extern int doDIG_SCOPE_202_Load ();
extern int doDIG_SCOPE_202_Open ();
extern int doDIG_SCOPE_202_Reset ();
extern int doDIG_SCOPE_202_Setup ();
extern int doDIG_SCOPE_202_Status ();
extern int doDIG_SCOPE_203_Close ();
extern int doDIG_SCOPE_203_Connect ();
extern int doDIG_SCOPE_203_DisConnect ();
extern double doDIG_SCOPE_203_FetchVoltagePp ();
extern int doDIG_SCOPE_203_Init ();
extern int doDIG_SCOPE_203_Load ();
extern int doDIG_SCOPE_203_Open ();
extern int doDIG_SCOPE_203_Reset ();
extern int doDIG_SCOPE_203_Setup ();
extern int doDIG_SCOPE_203_Status ();
extern int doDIG_SCOPE_204_Close ();
extern int doDIG_SCOPE_204_Connect ();
extern int doDIG_SCOPE_204_DisConnect ();
extern double doDIG_SCOPE_204_FetchVoltage ();
extern int doDIG_SCOPE_204_Init ();
extern int doDIG_SCOPE_204_Load ();
extern int doDIG_SCOPE_204_Open ();
extern int doDIG_SCOPE_204_Reset ();
extern int doDIG_SCOPE_204_Setup ();
extern int doDIG_SCOPE_204_Status ();
extern int doDIG_SCOPE_207_Close ();
extern int doDIG_SCOPE_207_Connect ();
extern int doDIG_SCOPE_207_DisConnect ();
extern double doDIG_SCOPE_207_FetchVoltagePp ();
extern int doDIG_SCOPE_207_Init ();
extern int doDIG_SCOPE_207_Load ();
extern int doDIG_SCOPE_207_Open ();
extern int doDIG_SCOPE_207_Reset ();
extern int doDIG_SCOPE_207_Setup ();
extern int doDIG_SCOPE_207_Status ();
extern int doDIG_SCOPE_208_Close ();
extern int doDIG_SCOPE_208_Connect ();
extern int doDIG_SCOPE_208_DisConnect ();
extern double doDIG_SCOPE_208_FetchVoltagePPos ();
extern int doDIG_SCOPE_208_Init ();
extern int doDIG_SCOPE_208_Load ();
extern int doDIG_SCOPE_208_Open ();
extern int doDIG_SCOPE_208_Reset ();
extern int doDIG_SCOPE_208_Setup ();
extern int doDIG_SCOPE_208_Status ();
extern int doDIG_SCOPE_209_Close ();
extern int doDIG_SCOPE_209_Connect ();
extern int doDIG_SCOPE_209_DisConnect ();
extern double doDIG_SCOPE_209_FetchVoltagePNeg ();
extern int doDIG_SCOPE_209_Init ();
extern int doDIG_SCOPE_209_Load ();
extern int doDIG_SCOPE_209_Open ();
extern int doDIG_SCOPE_209_Reset ();
extern int doDIG_SCOPE_209_Setup ();
extern int doDIG_SCOPE_209_Status ();
extern int doDIG_SCOPE_210_Close ();
extern int doDIG_SCOPE_210_Connect ();
extern int doDIG_SCOPE_210_DisConnect ();
extern double doDIG_SCOPE_210_FetchPrf ();
extern int doDIG_SCOPE_210_Init ();
extern int doDIG_SCOPE_210_Load ();
extern int doDIG_SCOPE_210_Open ();
extern int doDIG_SCOPE_210_Reset ();
extern int doDIG_SCOPE_210_Setup ();
extern int doDIG_SCOPE_210_Status ();
extern int doDIG_SCOPE_211_Close ();
extern int doDIG_SCOPE_211_Connect ();
extern int doDIG_SCOPE_211_DisConnect ();
extern double doDIG_SCOPE_211_FetchRiseTime ();
extern int doDIG_SCOPE_211_Init ();
extern int doDIG_SCOPE_211_Load ();
extern int doDIG_SCOPE_211_Open ();
extern int doDIG_SCOPE_211_Reset ();
extern int doDIG_SCOPE_211_Setup ();
extern int doDIG_SCOPE_211_Status ();
extern int doDIG_SCOPE_212_Close ();
extern int doDIG_SCOPE_212_Connect ();
extern int doDIG_SCOPE_212_DisConnect ();
extern double doDIG_SCOPE_212_FetchFallTime ();
extern int doDIG_SCOPE_212_Init ();
extern int doDIG_SCOPE_212_Load ();
extern int doDIG_SCOPE_212_Open ();
extern int doDIG_SCOPE_212_Reset ();
extern int doDIG_SCOPE_212_Setup ();
extern int doDIG_SCOPE_212_Status ();
extern int doDIG_SCOPE_213_Close ();
extern int doDIG_SCOPE_213_Connect ();
extern int doDIG_SCOPE_213_DisConnect ();
extern double doDIG_SCOPE_213_FetchPulseWidth ();
extern int doDIG_SCOPE_213_Init ();
extern int doDIG_SCOPE_213_Load ();
extern int doDIG_SCOPE_213_Open ();
extern int doDIG_SCOPE_213_Reset ();
extern int doDIG_SCOPE_213_Setup ();
extern int doDIG_SCOPE_213_Status ();
extern int doDIG_SCOPE_214_Close ();
extern int doDIG_SCOPE_214_Connect ();
extern int doDIG_SCOPE_214_DisConnect ();
extern double doDIG_SCOPE_214_FetchDutyCycle ();
extern int doDIG_SCOPE_214_Init ();
extern int doDIG_SCOPE_214_Load ();
extern int doDIG_SCOPE_214_Open ();
extern int doDIG_SCOPE_214_Reset ();
extern int doDIG_SCOPE_214_Setup ();
extern int doDIG_SCOPE_214_Status ();
extern int doDIG_SCOPE_215_Close ();
extern int doDIG_SCOPE_215_Connect ();
extern int doDIG_SCOPE_215_DisConnect ();
extern double doDIG_SCOPE_215_FetchOvershoot ();
extern int doDIG_SCOPE_215_Init ();
extern int doDIG_SCOPE_215_Load ();
extern int doDIG_SCOPE_215_Open ();
extern int doDIG_SCOPE_215_Reset ();
extern int doDIG_SCOPE_215_Setup ();
extern int doDIG_SCOPE_215_Status ();
extern int doDIG_SCOPE_216_Close ();
extern int doDIG_SCOPE_216_Connect ();
extern int doDIG_SCOPE_216_DisConnect ();
extern double doDIG_SCOPE_216_FetchPreshoot ();
extern int doDIG_SCOPE_216_Init ();
extern int doDIG_SCOPE_216_Load ();
extern int doDIG_SCOPE_216_Open ();
extern int doDIG_SCOPE_216_Reset ();
extern int doDIG_SCOPE_216_Setup ();
extern int doDIG_SCOPE_216_Status ();
extern int doDIG_SCOPE_217_Close ();
extern int doDIG_SCOPE_217_Connect ();
extern int doDIG_SCOPE_217_DisConnect ();
extern double doDIG_SCOPE_217_FetchAvVoltage ();
extern int doDIG_SCOPE_217_Init ();
extern int doDIG_SCOPE_217_Load ();
extern int doDIG_SCOPE_217_Open ();
extern int doDIG_SCOPE_217_Reset ();
extern int doDIG_SCOPE_217_Setup ();
extern int doDIG_SCOPE_217_Status ();
extern int doDIG_SCOPE_218_Close ();
extern int doDIG_SCOPE_218_Connect ();
extern int doDIG_SCOPE_218_DisConnect ();
extern double doDIG_SCOPE_218_FetchNegPulseWidth ();
extern int doDIG_SCOPE_218_Init ();
extern int doDIG_SCOPE_218_Load ();
extern int doDIG_SCOPE_218_Open ();
extern int doDIG_SCOPE_218_Reset ();
extern int doDIG_SCOPE_218_Setup ();
extern int doDIG_SCOPE_218_Status ();
extern int doDIG_SCOPE_219_Close ();
extern int doDIG_SCOPE_219_Connect ();
extern int doDIG_SCOPE_219_DisConnect ();
extern double doDIG_SCOPE_219_FetchPosPulseWidth ();
extern int doDIG_SCOPE_219_Init ();
extern int doDIG_SCOPE_219_Load ();
extern int doDIG_SCOPE_219_Open ();
extern int doDIG_SCOPE_219_Reset ();
extern int doDIG_SCOPE_219_Setup ();
extern int doDIG_SCOPE_219_Status ();
extern int doDIG_SCOPE_220_Close ();
extern int doDIG_SCOPE_220_Connect ();
extern int doDIG_SCOPE_220_DisConnect ();
extern int doDIG_SCOPE_220_FetchSample (
		// math reading [list of decimals]
		double* pSMPL, int nCntSMPL);
extern int doDIG_SCOPE_220_Init ();
extern int doDIG_SCOPE_220_Load ();
extern int doDIG_SCOPE_220_Open ();
extern int doDIG_SCOPE_220_Reset ();
extern int doDIG_SCOPE_220_Setup ();
extern int doDIG_SCOPE_220_Status ();
extern int doDIG_SCOPE_221_Close ();
extern int doDIG_SCOPE_221_Connect ();
extern int doDIG_SCOPE_221_DisConnect ();
extern int doDIG_SCOPE_221_Fetch ();
extern int doDIG_SCOPE_221_Init ();
extern int doDIG_SCOPE_221_Load ();
extern int doDIG_SCOPE_221_Open ();
extern int doDIG_SCOPE_221_Reset ();
extern int doDIG_SCOPE_221_Setup ();
extern int doDIG_SCOPE_221_Status ();
extern int doDIG_SCOPE_222_Close ();
extern int doDIG_SCOPE_222_Connect ();
extern int doDIG_SCOPE_222_DisConnect ();
extern int doDIG_SCOPE_222_Fetch ();
extern int doDIG_SCOPE_222_Init ();
extern int doDIG_SCOPE_222_Load ();
extern int doDIG_SCOPE_222_Open ();
extern int doDIG_SCOPE_222_Reset ();
extern int doDIG_SCOPE_222_Setup ();
extern int doDIG_SCOPE_222_Status ();
extern int doDIG_SCOPE_223_Close ();
extern int doDIG_SCOPE_223_Connect ();
extern int doDIG_SCOPE_223_Disconnect ();
extern double doDIG_SCOPE_223_FetchVoltage ();
extern int doDIG_SCOPE_223_Init ();
extern int doDIG_SCOPE_223_Load ();
extern int doDIG_SCOPE_223_Open ();
extern int doDIG_SCOPE_223_Reset ();
extern int doDIG_SCOPE_223_Setup ();
extern int doDIG_SCOPE_223_Status ();
extern int doDIG_SCOPE_224_Close ();
extern int doDIG_SCOPE_224_Connect ();
extern int doDIG_SCOPE_224_Disconnect ();
extern double doDIG_SCOPE_224_FetchVoltage ();
extern int doDIG_SCOPE_224_Init ();
extern int doDIG_SCOPE_224_Load ();
extern int doDIG_SCOPE_224_Open ();
extern int doDIG_SCOPE_224_Reset ();
extern int doDIG_SCOPE_224_Setup ();
extern int doDIG_SCOPE_224_Status ();
extern int doDIG_SCOPE_230_Close ();
extern int doDIG_SCOPE_230_Connect ();
extern int doDIG_SCOPE_230_DisConnect ();
extern int doDIG_SCOPE_230_FetchSaveWave (
		// math reading [list of decimals]
		double* pMATH, int nCntMATH);
extern int doDIG_SCOPE_230_Init ();
extern int doDIG_SCOPE_230_Load ();
extern int doDIG_SCOPE_230_Open ();
extern int doDIG_SCOPE_230_Reset ();
extern int doDIG_SCOPE_230_Setup ();
extern int doDIG_SCOPE_230_Status ();
extern int doDIG_SCOPE_231_Close ();
extern int doDIG_SCOPE_231_Connect ();
extern int doDIG_SCOPE_231_DisConnect ();
extern int doDIG_SCOPE_231_FetchLoadWave (
		// math reading [list of decimals]
		double* pMATH, int nCntMATH);
extern int doDIG_SCOPE_231_Init ();
extern int doDIG_SCOPE_231_Load ();
extern int doDIG_SCOPE_231_Open ();
extern int doDIG_SCOPE_231_Reset ();
extern int doDIG_SCOPE_231_Setup ();
extern int doDIG_SCOPE_231_Status ();
extern int doDIG_SCOPE_232_Close ();
extern int doDIG_SCOPE_232_Connect ();
extern int doDIG_SCOPE_232_DisConnect ();
extern double doDIG_SCOPE_232_FetchCompareWave ();
extern int doDIG_SCOPE_232_Init ();
extern int doDIG_SCOPE_232_Load ();
extern int doDIG_SCOPE_232_Open ();
extern int doDIG_SCOPE_232_Reset ();
extern int doDIG_SCOPE_232_Setup ();
extern int doDIG_SCOPE_232_Status ();
extern int doDIG_SCOPE_233_Close ();
extern int doDIG_SCOPE_233_Connect ();
extern int doDIG_SCOPE_233_DisConnect ();
extern int doDIG_SCOPE_233_FetchMath (
		// math reading [list of decimals]
		double* pMATH, int nCntMATH);
extern int doDIG_SCOPE_233_Init ();
extern int doDIG_SCOPE_233_Load ();
extern int doDIG_SCOPE_233_Open ();
extern int doDIG_SCOPE_233_Reset ();
extern int doDIG_SCOPE_233_Setup ();
extern int doDIG_SCOPE_233_Status ();
extern int doDIG_SCOPE_234_Close ();
extern int doDIG_SCOPE_234_Connect ();
extern int doDIG_SCOPE_234_DisConnect ();
extern int doDIG_SCOPE_234_FetchMath (
		// math reading [list of decimals]
		double* pMATH, int nCntMATH);
extern int doDIG_SCOPE_234_Init ();
extern int doDIG_SCOPE_234_Load ();
extern int doDIG_SCOPE_234_Open ();
extern int doDIG_SCOPE_234_Reset ();
extern int doDIG_SCOPE_234_Setup ();
extern int doDIG_SCOPE_234_Status ();
extern int doDIG_SCOPE_235_Close ();
extern int doDIG_SCOPE_235_Connect ();
extern int doDIG_SCOPE_235_DisConnect ();
extern int doDIG_SCOPE_235_FetchMath (
		// math reading [list of decimals]
		double* pMATH, int nCntMATH);
extern int doDIG_SCOPE_235_Init ();
extern int doDIG_SCOPE_235_Load ();
extern int doDIG_SCOPE_235_Open ();
extern int doDIG_SCOPE_235_Reset ();
extern int doDIG_SCOPE_235_Setup ();
extern int doDIG_SCOPE_235_Status ();
extern int doDIG_SCOPE_236_Close ();
extern int doDIG_SCOPE_236_Connect ();
extern int doDIG_SCOPE_236_DisConnect ();
extern int doDIG_SCOPE_236_FetchMath (
		// math reading [list of decimals]
		double* pMATH, int nCntMATH);
extern int doDIG_SCOPE_236_Init ();
extern int doDIG_SCOPE_236_Load ();
extern int doDIG_SCOPE_236_Open ();
extern int doDIG_SCOPE_236_Reset ();
extern int doDIG_SCOPE_236_Setup ();
extern int doDIG_SCOPE_236_Status ();
extern int doDIG_SCOPE_237_Close ();
extern int doDIG_SCOPE_237_Connect ();
extern int doDIG_SCOPE_237_DisConnect ();
extern int doDIG_SCOPE_237_FetchMath (
		// math reading [list of decimals]
		double* pMATH, int nCntMATH);
extern int doDIG_SCOPE_237_Init ();
extern int doDIG_SCOPE_237_Load ();
extern int doDIG_SCOPE_237_Open ();
extern int doDIG_SCOPE_237_Reset ();
extern int doDIG_SCOPE_237_Setup ();
extern int doDIG_SCOPE_237_Status ();
extern int TypeErr (const char *);
extern int BusErr (const char *);
DECLAREC char *DevTxt [] = {
	"",
	"!Controller:CH0",
	"DSO_1:CH101",
	"DSO_1:CH102",
	"DSO_1:CH103",
	"DSO_1:CH104",
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
	"DSO_1:CH13",
	"DSO_1:CH130",
	"DSO_1:CH131",
	"DSO_1:CH132",
	"DSO_1:CH133",
	"DSO_1:CH134",
	"DSO_1:CH135",
	"DSO_1:CH136",
	"DSO_1:CH137",
	"DSO_1:CH14",
	"DSO_1:CH201",
	"DSO_1:CH202",
	"DSO_1:CH203",
	"DSO_1:CH204",
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
	"DSO_1:CH230",
	"DSO_1:CH231",
	"DSO_1:CH232",
	"DSO_1:CH233",
	"DSO_1:CH234",
	"DSO_1:CH235",
	"DSO_1:CH236",
	"DSO_1:CH237",
};
DECLAREC int DevCnt = 66;
int CCALLBACK Wrapper_DIG_SCOPE_101_Close(void)
{
	if (doDIG_SCOPE_101_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_Connect(void)
{
	if (doDIG_SCOPE_101_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_DisConnect(void)
{
	if (doDIG_SCOPE_101_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_Fetch(void)
{
	if (doDIG_SCOPE_101_FetchFreq() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_Init(void)
{
	if (doDIG_SCOPE_101_Init() < 0)
		BusErr ("DSO_1");

	// if statement below doesn't seem to ever evaluate to true and belongs in the fetch method
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_FREQ) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("freq");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_101_FetchFreq();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_Load(void)
{
	if (doDIG_SCOPE_101_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_Open(void)
{
	if (doDIG_SCOPE_101_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_Reset(void)
{
	if (doDIG_SCOPE_101_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_Setup(void)
{
	if (doDIG_SCOPE_101_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_101_Status(void)
{
	if (doDIG_SCOPE_101_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Close(void)
{
	if (doDIG_SCOPE_102_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Connect(void)
{
	if (doDIG_SCOPE_102_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_DisConnect(void)
{
	if (doDIG_SCOPE_102_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Fetch(void)
{
	//if (doDIG_SCOPE_102_FetchPeriod() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_PERI) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("period");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_102_FetchPeriod();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Init(void)
{
	if (doDIG_SCOPE_102_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Load(void)
{
	if (doDIG_SCOPE_102_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Open(void)
{
	if (doDIG_SCOPE_102_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Reset(void)
{
	if (doDIG_SCOPE_102_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Setup(void)
{
	if (doDIG_SCOPE_102_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_102_Status(void)
{
	if (doDIG_SCOPE_102_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Close(void)
{
	if (doDIG_SCOPE_103_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Connect(void)
{
	if (doDIG_SCOPE_103_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_DisConnect(void)
{
	if (doDIG_SCOPE_103_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Fetch(void)
{
	//if (doDIG_SCOPE_103_FetchVoltagePp() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_VLPP) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage-pp");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_103_FetchVoltagePp();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Init(void)
{
	if (doDIG_SCOPE_103_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Load(void)
{
	if (doDIG_SCOPE_103_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Open(void)
{
	if (doDIG_SCOPE_103_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Reset(void)
{
	if (doDIG_SCOPE_103_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Setup(void)
{
	if (doDIG_SCOPE_103_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_103_Status(void)
{
	if (doDIG_SCOPE_103_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Close(void)
{
	if (doDIG_SCOPE_104_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Connect(void)
{
	if (doDIG_SCOPE_104_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_DisConnect(void)
{
	if (doDIG_SCOPE_104_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Fetch(void)
{
	//if (doDIG_SCOPE_104_FetchVoltage() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_VOLT) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_104_FetchVoltage();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Init(void)
{
	if (doDIG_SCOPE_104_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Load(void)
{
	if (doDIG_SCOPE_104_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Open(void)
{
	if (doDIG_SCOPE_104_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Reset(void)
{
	if (doDIG_SCOPE_104_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Setup(void)
{
	if (doDIG_SCOPE_104_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_104_Status(void)
{
	if (doDIG_SCOPE_104_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Close(void)
{
	if (doDIG_SCOPE_107_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Connect(void)
{
	if (doDIG_SCOPE_107_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_DisConnect(void)
{
	if (doDIG_SCOPE_107_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Fetch(void)
{
	//if (doDIG_SCOPE_107_FetchVoltagePp() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
int ii = FthMod();
int iii = M_VLPP;
	if (pFetchDatum && FthMod () == M_VLPP) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage-pp");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_107_FetchVoltagePp();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Init(void)
{
	if (doDIG_SCOPE_107_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Load(void)
{
	if (doDIG_SCOPE_107_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Open(void)
{
	if (doDIG_SCOPE_107_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Reset(void)
{
	if (doDIG_SCOPE_107_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Setup(void)
{
	if (doDIG_SCOPE_107_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_107_Status(void)
{
	if (doDIG_SCOPE_107_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Close(void)
{
	if (doDIG_SCOPE_108_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Connect(void)
{
	if (doDIG_SCOPE_108_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_DisConnect(void)
{
	if (doDIG_SCOPE_108_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Fetch(void)
{
	//if (doDIG_SCOPE_108_FetchVoltagePPos() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_VPKP) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage-p-pos");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_108_FetchVoltagePPos();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Init(void)
{
	if (doDIG_SCOPE_108_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Load(void)
{
	if (doDIG_SCOPE_108_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Open(void)
{
	if (doDIG_SCOPE_108_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Reset(void)
{
	if (doDIG_SCOPE_108_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Setup(void)
{
	if (doDIG_SCOPE_108_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_108_Status(void)
{
	if (doDIG_SCOPE_108_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Close(void)
{
	if (doDIG_SCOPE_109_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Connect(void)
{
	if (doDIG_SCOPE_109_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_DisConnect(void)
{
	if (doDIG_SCOPE_109_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Fetch(void)
{
	//if (doDIG_SCOPE_109_FetchVoltagePNeg() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_VPKN) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage-p-neg");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_109_FetchVoltagePNeg();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Init(void)
{
	if (doDIG_SCOPE_109_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Load(void)
{
	if (doDIG_SCOPE_109_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Open(void)
{
	if (doDIG_SCOPE_109_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Reset(void)
{
	if (doDIG_SCOPE_109_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Setup(void)
{
	if (doDIG_SCOPE_109_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_109_Status(void)
{
	if (doDIG_SCOPE_109_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Close(void)
{
	if (doDIG_SCOPE_11_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Connect(void)
{
	if (doDIG_SCOPE_11_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_DisConnect(void)
{
	if (doDIG_SCOPE_11_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Fetch(void)
{
	if (doDIG_SCOPE_11_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Init(void)
{
	if (doDIG_SCOPE_11_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Load(void)
{
	if (doDIG_SCOPE_11_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Open(void)
{
	if (doDIG_SCOPE_11_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Reset(void)
{
	if (doDIG_SCOPE_11_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Setup(void)
{
	if (doDIG_SCOPE_11_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_11_Status(void)
{
	if (doDIG_SCOPE_11_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Close(void)
{
	if (doDIG_SCOPE_110_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Connect(void)
{
	if (doDIG_SCOPE_110_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_DisConnect(void)
{
	if (doDIG_SCOPE_110_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Fetch(void)
{
	//if (doDIG_SCOPE_110_FetchPrf() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_PRFR) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("prf");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_110_FetchPrf();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Init(void)
{
	if (doDIG_SCOPE_110_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Load(void)
{
	if (doDIG_SCOPE_110_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Open(void)
{
	if (doDIG_SCOPE_110_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Reset(void)
{
	if (doDIG_SCOPE_110_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Setup(void)
{
	if (doDIG_SCOPE_110_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_110_Status(void)
{
	if (doDIG_SCOPE_110_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Close(void)
{
	if (doDIG_SCOPE_111_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Connect(void)
{
	if (doDIG_SCOPE_111_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_DisConnect(void)
{
	if (doDIG_SCOPE_111_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Fetch(void)
{
	//if (doDIG_SCOPE_111_FetchRiseTime() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_RISE) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("rise-time");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_111_FetchRiseTime();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Init(void)
{
	if (doDIG_SCOPE_111_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Load(void)
{
	if (doDIG_SCOPE_111_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Open(void)
{
	if (doDIG_SCOPE_111_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Reset(void)
{
	if (doDIG_SCOPE_111_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Setup(void)
{
	if (doDIG_SCOPE_111_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_111_Status(void)
{
	if (doDIG_SCOPE_111_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Close(void)
{
	if (doDIG_SCOPE_112_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Connect(void)
{
	if (doDIG_SCOPE_112_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_DisConnect(void)
{
	if (doDIG_SCOPE_112_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Fetch(void)
{
	//if (doDIG_SCOPE_112_FetchFallTime() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_FALL) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("fall-time");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_112_FetchFallTime();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Init(void)
{
	if (doDIG_SCOPE_112_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Load(void)
{
	if (doDIG_SCOPE_112_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Open(void)
{
	if (doDIG_SCOPE_112_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Reset(void)
{
	if (doDIG_SCOPE_112_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Setup(void)
{
	if (doDIG_SCOPE_112_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_112_Status(void)
{
	if (doDIG_SCOPE_112_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Close(void)
{
	if (doDIG_SCOPE_113_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Connect(void)
{
	if (doDIG_SCOPE_113_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_DisConnect(void)
{
	if (doDIG_SCOPE_113_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Fetch(void)
{
	if (doDIG_SCOPE_113_FetchPulseWidth() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Init(void)
{
	if (doDIG_SCOPE_113_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Load(void)
{
	if (doDIG_SCOPE_113_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Open(void)
{
	if (doDIG_SCOPE_113_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Reset(void)
{
	if (doDIG_SCOPE_113_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Setup(void)
{
	if (doDIG_SCOPE_113_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_113_Status(void)
{
	if (doDIG_SCOPE_113_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Close(void)
{
	if (doDIG_SCOPE_114_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Connect(void)
{
	if (doDIG_SCOPE_114_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_DisConnect(void)
{
	if (doDIG_SCOPE_114_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Fetch(void)
{
	if (doDIG_SCOPE_114_FetchDutyCycle() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Init(void)
{
	if (doDIG_SCOPE_114_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Load(void)
{
	if (doDIG_SCOPE_114_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Open(void)
{
	if (doDIG_SCOPE_114_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Reset(void)
{
	if (doDIG_SCOPE_114_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Setup(void)
{
	if (doDIG_SCOPE_114_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_114_Status(void)
{
	if (doDIG_SCOPE_114_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Close(void)
{
	if (doDIG_SCOPE_115_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Connect(void)
{
	if (doDIG_SCOPE_115_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_DisConnect(void)
{
	if (doDIG_SCOPE_115_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Fetch(void)
{
	if (doDIG_SCOPE_115_FetchOvershoot() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Init(void)
{
	if (doDIG_SCOPE_115_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Load(void)
{
	if (doDIG_SCOPE_115_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Open(void)
{
	if (doDIG_SCOPE_115_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Reset(void)
{
	if (doDIG_SCOPE_115_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Setup(void)
{
	if (doDIG_SCOPE_115_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_115_Status(void)
{
	if (doDIG_SCOPE_115_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Close(void)
{
	if (doDIG_SCOPE_116_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Connect(void)
{
	if (doDIG_SCOPE_116_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_DisConnect(void)
{
	if (doDIG_SCOPE_116_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Fetch(void)
{
	if (doDIG_SCOPE_116_FetchPreshoot() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Init(void)
{
	if (doDIG_SCOPE_116_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Load(void)
{
	if (doDIG_SCOPE_116_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Open(void)
{
	if (doDIG_SCOPE_116_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Reset(void)
{
	if (doDIG_SCOPE_116_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Setup(void)
{
	if (doDIG_SCOPE_116_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_116_Status(void)
{
	if (doDIG_SCOPE_116_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Close(void)
{
	if (doDIG_SCOPE_117_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Connect(void)
{
	if (doDIG_SCOPE_117_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_DisConnect(void)
{
	if (doDIG_SCOPE_117_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Fetch(void)
{
	if (doDIG_SCOPE_117_FetchAvVoltage() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Init(void)
{
	if (doDIG_SCOPE_117_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Load(void)
{
	if (doDIG_SCOPE_117_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Open(void)
{
	if (doDIG_SCOPE_117_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Reset(void)
{
	if (doDIG_SCOPE_117_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Setup(void)
{
	if (doDIG_SCOPE_117_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_117_Status(void)
{
	if (doDIG_SCOPE_117_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Close(void)
{
	if (doDIG_SCOPE_118_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Connect(void)
{
	if (doDIG_SCOPE_118_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_DisConnect(void)
{
	if (doDIG_SCOPE_118_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Fetch(void)
{
	if (doDIG_SCOPE_118_FetchNegPulseWidth() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Init(void)
{
	if (doDIG_SCOPE_118_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Load(void)
{
	if (doDIG_SCOPE_118_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Open(void)
{
	if (doDIG_SCOPE_118_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Reset(void)
{
	if (doDIG_SCOPE_118_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Setup(void)
{
	if (doDIG_SCOPE_118_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_118_Status(void)
{
	if (doDIG_SCOPE_118_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Close(void)
{
	if (doDIG_SCOPE_119_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Connect(void)
{
	if (doDIG_SCOPE_119_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_DisConnect(void)
{
	if (doDIG_SCOPE_119_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Fetch(void)
{
	if (doDIG_SCOPE_119_FetchPosPulseWidth() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Init(void)
{
	if (doDIG_SCOPE_119_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Load(void)
{
	if (doDIG_SCOPE_119_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Open(void)
{
	if (doDIG_SCOPE_119_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Reset(void)
{
	if (doDIG_SCOPE_119_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Setup(void)
{
	if (doDIG_SCOPE_119_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_119_Status(void)
{
	if (doDIG_SCOPE_119_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Close(void)
{
	if (doDIG_SCOPE_12_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Connect(void)
{
	if (doDIG_SCOPE_12_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_DisConnect(void)
{
	if (doDIG_SCOPE_12_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Fetch(void)
{
	if (doDIG_SCOPE_12_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Init(void)
{
	if (doDIG_SCOPE_12_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Load(void)
{
	if (doDIG_SCOPE_12_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Open(void)
{
	if (doDIG_SCOPE_12_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Reset(void)
{
	if (doDIG_SCOPE_12_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Setup(void)
{
	if (doDIG_SCOPE_12_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_12_Status(void)
{
	if (doDIG_SCOPE_12_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Close(void)
{
	if (doDIG_SCOPE_120_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Connect(void)
{
	if (doDIG_SCOPE_120_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_DisConnect(void)
{
	if (doDIG_SCOPE_120_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Fetch(void)
{
//	if (doDIG_SCOPE_120_FetchSample() < 0)
//		BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pSMPL = NULL; int nCntSMPL = 0;
	pFetchDatum = FthDat();

	if (pFetchDatum && FthMod() == M_SMPL) 
	{
		if (DatTyp(pFetchDatum) != DECV)
		{
			return TypeErr("sample");
		}

		pSMPL = (double*) pFetchDatum->dat_val.dat_dec;
		nCntSMPL = DatCnt (pFetchDatum);
	}

	nRtn = doDIG_SCOPE_120_FetchSample(pSMPL, nCntSMPL);

	if (nRtn < 0)
	{
		BusErr("DSO_1");
	}

	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Init(void)
{
	if (doDIG_SCOPE_120_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Load(void)
{
	if (doDIG_SCOPE_120_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Open(void)
{
	if (doDIG_SCOPE_120_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Reset(void)
{
	if (doDIG_SCOPE_120_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Setup(void)
{
	if (doDIG_SCOPE_120_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_120_Status(void)
{
	if (doDIG_SCOPE_120_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Close(void)
{
	if (doDIG_SCOPE_121_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Connect(void)
{
	if (doDIG_SCOPE_121_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_DisConnect(void)
{
	if (doDIG_SCOPE_121_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Fetch(void)
{
	if (doDIG_SCOPE_121_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Init(void)
{
	if (doDIG_SCOPE_121_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Load(void)
{
	if (doDIG_SCOPE_121_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Open(void)
{
	if (doDIG_SCOPE_121_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Reset(void)
{
	if (doDIG_SCOPE_121_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Setup(void)
{
	if (doDIG_SCOPE_121_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_121_Status(void)
{
	if (doDIG_SCOPE_121_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Close(void)
{
	if (doDIG_SCOPE_122_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Connect(void)
{
	if (doDIG_SCOPE_122_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_DisConnect(void)
{
	if (doDIG_SCOPE_122_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Fetch(void)
{
	if (doDIG_SCOPE_122_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Init(void)
{
	if (doDIG_SCOPE_122_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Load(void)
{
	if (doDIG_SCOPE_122_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Open(void)
{
	if (doDIG_SCOPE_122_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Reset(void)
{
	if (doDIG_SCOPE_122_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Setup(void)
{
	if (doDIG_SCOPE_122_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_122_Status(void)
{
	if (doDIG_SCOPE_122_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}

int CCALLBACK Wrapper_DIG_SCOPE_123_Close(void)
{
	if (doDIG_SCOPE_123_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Connect(void)
{
	if (doDIG_SCOPE_123_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Disconnect(void)
{
	if (doDIG_SCOPE_123_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Fetch(void)
{
	
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_VOLT) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_123_FetchVoltage();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Init(void)
{
	if (doDIG_SCOPE_123_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Load(void)
{
	if (doDIG_SCOPE_123_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Open(void)
{
	if (doDIG_SCOPE_123_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Reset(void)
{
	if (doDIG_SCOPE_123_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Setup(void)
{
	if (doDIG_SCOPE_123_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_123_Status(void)
{
	if (doDIG_SCOPE_123_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Close(void)
{
	if (doDIG_SCOPE_124_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Connect(void)
{
	if (doDIG_SCOPE_124_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Disconnect(void)
{
	if (doDIG_SCOPE_124_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Fetch(void)
{
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_VLPK) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage-p");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_124_FetchVoltageP();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Init(void)
{
	if (doDIG_SCOPE_124_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Load(void)
{
	if (doDIG_SCOPE_124_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Open(void)
{
	if (doDIG_SCOPE_124_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Reset(void)
{
	if (doDIG_SCOPE_124_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Setup(void)
{
	if (doDIG_SCOPE_124_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_124_Status(void)
{
	if (doDIG_SCOPE_124_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Close(void)
{
	if (doDIG_SCOPE_13_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Connect(void)
{
	if (doDIG_SCOPE_13_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_DisConnect(void)
{
	if (doDIG_SCOPE_13_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Fetch(void)
{
	if (doDIG_SCOPE_13_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Init(void)
{
	if (doDIG_SCOPE_13_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Load(void)
{
	if (doDIG_SCOPE_13_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Open(void)
{
	if (doDIG_SCOPE_13_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Reset(void)
{
	if (doDIG_SCOPE_13_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Setup(void)
{
	if (doDIG_SCOPE_13_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_13_Status(void)
{
	if (doDIG_SCOPE_13_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Close(void)
{
	if (doDIG_SCOPE_130_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Connect(void)
{
	if (doDIG_SCOPE_130_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_DisConnect(void)
{
	if (doDIG_SCOPE_130_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Fetch(void)
{
	//if (doDIG_SCOPE_130_FetchSaveWave() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pSVWV = NULL; int nCntSVWV = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_SVWV) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("save-wave");
		pSVWV = (double*) pFetchDatum->dat_val.dat_dec;
		nCntSVWV = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_130_FetchSaveWave(pSVWV, nCntSVWV);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Init(void)
{
	if (doDIG_SCOPE_130_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Load(void)
{
	if (doDIG_SCOPE_130_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Open(void)
{
	if (doDIG_SCOPE_130_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Reset(void)
{
	if (doDIG_SCOPE_130_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Setup(void)
{
	if (doDIG_SCOPE_130_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_130_Status(void)
{
	if (doDIG_SCOPE_130_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Close(void)
{
	if (doDIG_SCOPE_131_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Connect(void)
{
	if (doDIG_SCOPE_131_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_DisConnect(void)
{
	if (doDIG_SCOPE_131_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Fetch(void)
{
	//if (doDIG_SCOPE_131_FetchLoadWave() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pLDVW = NULL; int nCntLDVW = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_LDVW) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("load-wave");
		pLDVW = (double*) pFetchDatum->dat_val.dat_dec;
		nCntLDVW = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_131_FetchLoadWave(pLDVW, nCntLDVW);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Init(void)
{
	if (doDIG_SCOPE_131_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Load(void)
{
	if (doDIG_SCOPE_131_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Open(void)
{
	if (doDIG_SCOPE_131_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Reset(void)
{
	if (doDIG_SCOPE_131_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Setup(void)
{
	if (doDIG_SCOPE_131_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_131_Status(void)
{
	if (doDIG_SCOPE_131_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Close(void)
{
	if (doDIG_SCOPE_132_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Connect(void)
{
	if (doDIG_SCOPE_132_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_DisConnect(void)
{
	if (doDIG_SCOPE_132_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Fetch(void)
{
	if (doDIG_SCOPE_132_FetchCompareWave() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Init(void)
{
	if (doDIG_SCOPE_132_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Load(void)
{
	if (doDIG_SCOPE_132_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Open(void)
{
	if (doDIG_SCOPE_132_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Reset(void)
{
	if (doDIG_SCOPE_132_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Setup(void)
{
	if (doDIG_SCOPE_132_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_132_Status(void)
{
	if (doDIG_SCOPE_132_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Close(void)
{
	if (doDIG_SCOPE_133_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Connect(void)
{
	if (doDIG_SCOPE_133_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_DisConnect(void)
{
	if (doDIG_SCOPE_133_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Fetch(void)
{
	//if (doDIG_SCOPE_133_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_133_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Init(void)
{
	if (doDIG_SCOPE_133_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Load(void)
{
	if (doDIG_SCOPE_133_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Open(void)
{
	if (doDIG_SCOPE_133_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Reset(void)
{
	if (doDIG_SCOPE_133_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Setup(void)
{
	if (doDIG_SCOPE_133_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_133_Status(void)
{
	if (doDIG_SCOPE_133_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Close(void)
{
	if (doDIG_SCOPE_134_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Connect(void)
{
	if (doDIG_SCOPE_134_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_DisConnect(void)
{
	if (doDIG_SCOPE_134_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Fetch(void)
{
	//if (doDIG_SCOPE_134_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_134_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Init(void)
{
	if (doDIG_SCOPE_134_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Load(void)
{
	if (doDIG_SCOPE_134_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Open(void)
{
	if (doDIG_SCOPE_134_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Reset(void)
{
	if (doDIG_SCOPE_134_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Setup(void)
{
	if (doDIG_SCOPE_134_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_134_Status(void)
{
	if (doDIG_SCOPE_134_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Close(void)
{
	if (doDIG_SCOPE_135_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Connect(void)
{
	if (doDIG_SCOPE_135_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_DisConnect(void)
{
	if (doDIG_SCOPE_135_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Fetch(void)
{
	//if (doDIG_SCOPE_135_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_135_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Init(void)
{
	if (doDIG_SCOPE_135_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Load(void)
{
	if (doDIG_SCOPE_135_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Open(void)
{
	if (doDIG_SCOPE_135_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Reset(void)
{
	if (doDIG_SCOPE_135_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Setup(void)
{
	if (doDIG_SCOPE_135_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_135_Status(void)
{
	if (doDIG_SCOPE_135_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Close(void)
{
	if (doDIG_SCOPE_136_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Connect(void)
{
	if (doDIG_SCOPE_136_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_DisConnect(void)
{
	if (doDIG_SCOPE_136_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Fetch(void)
{
	//if (doDIG_SCOPE_136_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_136_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Init(void)
{
	if (doDIG_SCOPE_136_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Load(void)
{
	if (doDIG_SCOPE_136_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Open(void)
{
	if (doDIG_SCOPE_136_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Reset(void)
{
	if (doDIG_SCOPE_136_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Setup(void)
{
	if (doDIG_SCOPE_136_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_136_Status(void)
{
	if (doDIG_SCOPE_136_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Close(void)
{
	if (doDIG_SCOPE_137_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Connect(void)
{
	if (doDIG_SCOPE_137_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_DisConnect(void)
{
	if (doDIG_SCOPE_137_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Fetch(void)
{
	//if (doDIG_SCOPE_137_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_137_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Init(void)
{
	if (doDIG_SCOPE_137_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Load(void)
{
	if (doDIG_SCOPE_137_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Open(void)
{
	if (doDIG_SCOPE_137_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Reset(void)
{
	if (doDIG_SCOPE_137_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Setup(void)
{
	if (doDIG_SCOPE_137_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_137_Status(void)
{
	if (doDIG_SCOPE_137_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Close(void)
{
	if (doDIG_SCOPE_14_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Connect(void)
{
	if (doDIG_SCOPE_14_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_DisConnect(void)
{
	if (doDIG_SCOPE_14_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Fetch(void)
{
	if (doDIG_SCOPE_14_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Init(void)
{
	if (doDIG_SCOPE_14_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Load(void)
{
	if (doDIG_SCOPE_14_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Open(void)
{
	if (doDIG_SCOPE_14_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Reset(void)
{
	if (doDIG_SCOPE_14_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Setup(void)
{
	if (doDIG_SCOPE_14_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_14_Status(void)
{
	if (doDIG_SCOPE_14_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Close(void)
{
	if (doDIG_SCOPE_201_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Connect(void)
{
	if (doDIG_SCOPE_201_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_DisConnect(void)
{
	if (doDIG_SCOPE_201_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Fetch(void)
{
	if (doDIG_SCOPE_201_FetchFreq() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Init(void)
{
	if (doDIG_SCOPE_201_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Load(void)
{
	if (doDIG_SCOPE_201_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Open(void)
{
	if (doDIG_SCOPE_201_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Reset(void)
{
	if (doDIG_SCOPE_201_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Setup(void)
{
	if (doDIG_SCOPE_201_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_201_Status(void)
{
	if (doDIG_SCOPE_201_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Close(void)
{
	if (doDIG_SCOPE_202_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Connect(void)
{
	if (doDIG_SCOPE_202_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_DisConnect(void)
{
	if (doDIG_SCOPE_202_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Fetch(void)
{
	if (doDIG_SCOPE_202_FetchPeriod() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Init(void)
{
	if (doDIG_SCOPE_202_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Load(void)
{
	if (doDIG_SCOPE_202_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Open(void)
{
	if (doDIG_SCOPE_202_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Reset(void)
{
	if (doDIG_SCOPE_202_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Setup(void)
{
	if (doDIG_SCOPE_202_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_202_Status(void)
{
	if (doDIG_SCOPE_202_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Close(void)
{
	if (doDIG_SCOPE_203_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Connect(void)
{
	if (doDIG_SCOPE_203_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_DisConnect(void)
{
	if (doDIG_SCOPE_203_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Fetch(void)
{
	if (doDIG_SCOPE_203_FetchVoltagePp() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Init(void)
{
	if (doDIG_SCOPE_203_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Load(void)
{
	if (doDIG_SCOPE_203_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Open(void)
{
	if (doDIG_SCOPE_203_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Reset(void)
{
	if (doDIG_SCOPE_203_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Setup(void)
{
	if (doDIG_SCOPE_203_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_203_Status(void)
{
	if (doDIG_SCOPE_203_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Close(void)
{
	if (doDIG_SCOPE_204_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Connect(void)
{
	if (doDIG_SCOPE_204_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_DisConnect(void)
{
	if (doDIG_SCOPE_204_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Fetch(void)
{
	if (doDIG_SCOPE_204_FetchVoltage() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Init(void)
{
	if (doDIG_SCOPE_204_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Load(void)
{
	if (doDIG_SCOPE_204_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Open(void)
{
	if (doDIG_SCOPE_204_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Reset(void)
{
	if (doDIG_SCOPE_204_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Setup(void)
{
	if (doDIG_SCOPE_204_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_204_Status(void)
{
	if (doDIG_SCOPE_204_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Close(void)
{
	if (doDIG_SCOPE_207_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Connect(void)
{
	if (doDIG_SCOPE_207_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_DisConnect(void)
{
	if (doDIG_SCOPE_207_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Fetch(void)
{
	if (doDIG_SCOPE_207_FetchVoltagePp() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Init(void)
{
	if (doDIG_SCOPE_207_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Load(void)
{
	if (doDIG_SCOPE_207_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Open(void)
{
	if (doDIG_SCOPE_207_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Reset(void)
{
	if (doDIG_SCOPE_207_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Setup(void)
{
	if (doDIG_SCOPE_207_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_207_Status(void)
{
	if (doDIG_SCOPE_207_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Close(void)
{
	if (doDIG_SCOPE_208_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Connect(void)
{
	if (doDIG_SCOPE_208_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_DisConnect(void)
{
	if (doDIG_SCOPE_208_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Fetch(void)
{
	if (doDIG_SCOPE_208_FetchVoltagePPos() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Init(void)
{
	if (doDIG_SCOPE_208_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Load(void)
{
	if (doDIG_SCOPE_208_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Open(void)
{
	if (doDIG_SCOPE_208_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Reset(void)
{
	if (doDIG_SCOPE_208_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Setup(void)
{
	if (doDIG_SCOPE_208_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_208_Status(void)
{
	if (doDIG_SCOPE_208_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Close(void)
{
	if (doDIG_SCOPE_209_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Connect(void)
{
	if (doDIG_SCOPE_209_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_DisConnect(void)
{
	if (doDIG_SCOPE_209_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Fetch(void)
{
	if (doDIG_SCOPE_209_FetchVoltagePNeg() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Init(void)
{
	if (doDIG_SCOPE_209_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Load(void)
{
	if (doDIG_SCOPE_209_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Open(void)
{
	if (doDIG_SCOPE_209_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Reset(void)
{
	if (doDIG_SCOPE_209_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Setup(void)
{
	if (doDIG_SCOPE_209_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_209_Status(void)
{
	if (doDIG_SCOPE_209_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Close(void)
{
	if (doDIG_SCOPE_210_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Connect(void)
{
	if (doDIG_SCOPE_210_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_DisConnect(void)
{
	if (doDIG_SCOPE_210_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Fetch(void)
{
	if (doDIG_SCOPE_210_FetchPrf() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Init(void)
{
	if (doDIG_SCOPE_210_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Load(void)
{
	if (doDIG_SCOPE_210_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Open(void)
{
	if (doDIG_SCOPE_210_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Reset(void)
{
	if (doDIG_SCOPE_210_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Setup(void)
{
	if (doDIG_SCOPE_210_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_210_Status(void)
{
	if (doDIG_SCOPE_210_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Close(void)
{
	if (doDIG_SCOPE_211_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Connect(void)
{
	if (doDIG_SCOPE_211_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_DisConnect(void)
{
	if (doDIG_SCOPE_211_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Fetch(void)
{
	if (doDIG_SCOPE_211_FetchRiseTime() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Init(void)
{
	if (doDIG_SCOPE_211_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Load(void)
{
	if (doDIG_SCOPE_211_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Open(void)
{
	if (doDIG_SCOPE_211_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Reset(void)
{
	if (doDIG_SCOPE_211_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Setup(void)
{
	if (doDIG_SCOPE_211_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_211_Status(void)
{
	if (doDIG_SCOPE_211_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Close(void)
{
	if (doDIG_SCOPE_212_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Connect(void)
{
	if (doDIG_SCOPE_212_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_DisConnect(void)
{
	if (doDIG_SCOPE_212_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Fetch(void)
{
	if (doDIG_SCOPE_212_FetchFallTime() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Init(void)
{
	if (doDIG_SCOPE_212_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Load(void)
{
	if (doDIG_SCOPE_212_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Open(void)
{
	if (doDIG_SCOPE_212_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Reset(void)
{
	if (doDIG_SCOPE_212_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Setup(void)
{
	if (doDIG_SCOPE_212_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_212_Status(void)
{
	if (doDIG_SCOPE_212_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Close(void)
{
	if (doDIG_SCOPE_213_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Connect(void)
{
	if (doDIG_SCOPE_213_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_DisConnect(void)
{
	if (doDIG_SCOPE_213_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Fetch(void)
{
	if (doDIG_SCOPE_213_FetchPulseWidth() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Init(void)
{
	if (doDIG_SCOPE_213_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Load(void)
{
	if (doDIG_SCOPE_213_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Open(void)
{
	if (doDIG_SCOPE_213_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Reset(void)
{
	if (doDIG_SCOPE_213_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Setup(void)
{
	if (doDIG_SCOPE_213_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_213_Status(void)
{
	if (doDIG_SCOPE_213_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Close(void)
{
	if (doDIG_SCOPE_214_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Connect(void)
{
	if (doDIG_SCOPE_214_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_DisConnect(void)
{
	if (doDIG_SCOPE_214_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Fetch(void)
{
	if (doDIG_SCOPE_214_FetchDutyCycle() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Init(void)
{
	if (doDIG_SCOPE_214_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Load(void)
{
	if (doDIG_SCOPE_214_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Open(void)
{
	if (doDIG_SCOPE_214_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Reset(void)
{
	if (doDIG_SCOPE_214_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Setup(void)
{
	if (doDIG_SCOPE_214_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_214_Status(void)
{
	if (doDIG_SCOPE_214_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Close(void)
{
	if (doDIG_SCOPE_215_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Connect(void)
{
	if (doDIG_SCOPE_215_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_DisConnect(void)
{
	if (doDIG_SCOPE_215_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Fetch(void)
{
	if (doDIG_SCOPE_215_FetchOvershoot() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Init(void)
{
	if (doDIG_SCOPE_215_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Load(void)
{
	if (doDIG_SCOPE_215_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Open(void)
{
	if (doDIG_SCOPE_215_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Reset(void)
{
	if (doDIG_SCOPE_215_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Setup(void)
{
	if (doDIG_SCOPE_215_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_215_Status(void)
{
	if (doDIG_SCOPE_215_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Close(void)
{
	if (doDIG_SCOPE_216_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Connect(void)
{
	if (doDIG_SCOPE_216_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_DisConnect(void)
{
	if (doDIG_SCOPE_216_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Fetch(void)
{
	if (doDIG_SCOPE_216_FetchPreshoot() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Init(void)
{
	if (doDIG_SCOPE_216_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Load(void)
{
	if (doDIG_SCOPE_216_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Open(void)
{
	if (doDIG_SCOPE_216_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Reset(void)
{
	if (doDIG_SCOPE_216_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Setup(void)
{
	if (doDIG_SCOPE_216_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_216_Status(void)
{
	if (doDIG_SCOPE_216_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Close(void)
{
	if (doDIG_SCOPE_217_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Connect(void)
{
	if (doDIG_SCOPE_217_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_DisConnect(void)
{
	if (doDIG_SCOPE_217_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Fetch(void)
{
	if (doDIG_SCOPE_217_FetchAvVoltage() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Init(void)
{
	if (doDIG_SCOPE_217_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Load(void)
{
	if (doDIG_SCOPE_217_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Open(void)
{
	if (doDIG_SCOPE_217_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Reset(void)
{
	if (doDIG_SCOPE_217_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Setup(void)
{
	if (doDIG_SCOPE_217_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_217_Status(void)
{
	if (doDIG_SCOPE_217_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Close(void)
{
	if (doDIG_SCOPE_218_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Connect(void)
{
	if (doDIG_SCOPE_218_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_DisConnect(void)
{
	if (doDIG_SCOPE_218_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Fetch(void)
{
	if (doDIG_SCOPE_218_FetchNegPulseWidth() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Init(void)
{
	if (doDIG_SCOPE_218_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Load(void)
{
	if (doDIG_SCOPE_218_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Open(void)
{
	if (doDIG_SCOPE_218_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Reset(void)
{
	if (doDIG_SCOPE_218_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Setup(void)
{
	if (doDIG_SCOPE_218_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_218_Status(void)
{
	if (doDIG_SCOPE_218_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Close(void)
{
	if (doDIG_SCOPE_219_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Connect(void)
{
	if (doDIG_SCOPE_219_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_DisConnect(void)
{
	if (doDIG_SCOPE_219_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Fetch(void)
{
	if (doDIG_SCOPE_219_FetchPosPulseWidth() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Init(void)
{
	if (doDIG_SCOPE_219_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Load(void)
{
	if (doDIG_SCOPE_219_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Open(void)
{
	if (doDIG_SCOPE_219_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Reset(void)
{
	if (doDIG_SCOPE_219_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Setup(void)
{
	if (doDIG_SCOPE_219_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_219_Status(void)
{
	if (doDIG_SCOPE_219_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Close(void)
{
	if (doDIG_SCOPE_220_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Connect(void)
{
	if (doDIG_SCOPE_220_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_DisConnect(void)
{
	if (doDIG_SCOPE_220_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Fetch(void)
{
	//if (doDIG_SCOPE_220_FetchSample() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pSMPL = NULL; int nCntSMPL = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_SMPL) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("sample");
		pSMPL = (double*) pFetchDatum->dat_val.dat_dec;
		nCntSMPL = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_220_FetchSample(pSMPL, nCntSMPL);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Init(void)
{
	if (doDIG_SCOPE_220_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Load(void)
{
	if (doDIG_SCOPE_220_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Open(void)
{
	if (doDIG_SCOPE_220_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Reset(void)
{
	if (doDIG_SCOPE_220_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Setup(void)
{
	if (doDIG_SCOPE_220_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_220_Status(void)
{
	if (doDIG_SCOPE_220_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Close(void)
{
	if (doDIG_SCOPE_221_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Connect(void)
{
	if (doDIG_SCOPE_221_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_DisConnect(void)
{
	if (doDIG_SCOPE_221_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Fetch(void)
{
	if (doDIG_SCOPE_221_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Init(void)
{
	if (doDIG_SCOPE_221_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Load(void)
{
	if (doDIG_SCOPE_221_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Open(void)
{
	if (doDIG_SCOPE_221_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Reset(void)
{
	if (doDIG_SCOPE_221_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Setup(void)
{
	if (doDIG_SCOPE_221_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_221_Status(void)
{
	if (doDIG_SCOPE_221_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Close(void)
{
	if (doDIG_SCOPE_222_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Connect(void)
{
	if (doDIG_SCOPE_222_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_DisConnect(void)
{
	if (doDIG_SCOPE_222_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Fetch(void)
{
	if (doDIG_SCOPE_222_Fetch() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Init(void)
{
	if (doDIG_SCOPE_222_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Load(void)
{
	if (doDIG_SCOPE_222_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Open(void)
{
	if (doDIG_SCOPE_222_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Reset(void)
{
	if (doDIG_SCOPE_222_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Setup(void)
{
	if (doDIG_SCOPE_222_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_222_Status(void)
{
	if (doDIG_SCOPE_222_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}

int CCALLBACK Wrapper_DIG_SCOPE_223_Close(void)
{
	if (doDIG_SCOPE_223_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Connect(void)
{
	if (doDIG_SCOPE_223_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Disconnect(void)
{
	if (doDIG_SCOPE_223_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Fetch(void)
{
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_VOLT) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_223_FetchVoltage();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Init(void)
{
	if (doDIG_SCOPE_223_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Load(void)
{
	if (doDIG_SCOPE_223_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Open(void)
{
	if (doDIG_SCOPE_223_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Reset(void)
{
	if (doDIG_SCOPE_223_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Setup(void)
{
	if (doDIG_SCOPE_223_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_223_Status(void)
{
	if (doDIG_SCOPE_223_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Close(void)
{
	if (doDIG_SCOPE_224_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Connect(void)
{
	if (doDIG_SCOPE_224_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Disconnect(void)
{
	if (doDIG_SCOPE_224_Disconnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Fetch(void)
{
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_VLPK) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("voltage");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_224_FetchVoltage();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Init(void)
{
	if (doDIG_SCOPE_224_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Load(void)
{
	if (doDIG_SCOPE_224_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Open(void)
{
	if (doDIG_SCOPE_224_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Reset(void)
{
	if (doDIG_SCOPE_224_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Setup(void)
{
	if (doDIG_SCOPE_224_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_224_Status(void)
{
	if (doDIG_SCOPE_224_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Close(void)
{
	if (doDIG_SCOPE_230_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Connect(void)
{
	if (doDIG_SCOPE_230_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_DisConnect(void)
{
	if (doDIG_SCOPE_230_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Fetch(void)
{
	//if (doDIG_SCOPE_230_FetchSaveWave() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pSVWV = NULL; int nCntSVWV = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_SVWV) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("save-wave");
		pSVWV = (double*) pFetchDatum->dat_val.dat_dec;
		nCntSVWV = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_230_FetchSaveWave(pSVWV, nCntSVWV);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Init(void)
{
	if (doDIG_SCOPE_230_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Load(void)
{
	if (doDIG_SCOPE_230_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Open(void)
{
	if (doDIG_SCOPE_230_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Reset(void)
{
	if (doDIG_SCOPE_230_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Setup(void)
{
	if (doDIG_SCOPE_230_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_230_Status(void)
{
	if (doDIG_SCOPE_230_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Close(void)
{
	if (doDIG_SCOPE_231_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Connect(void)
{
	if (doDIG_SCOPE_231_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_DisConnect(void)
{
	if (doDIG_SCOPE_231_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Fetch(void)
{
	//if (doDIG_SCOPE_231_FetchLoadWave() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pLDVW = NULL; int nCntLDVW = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_LDVW) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("load-wave");
		pLDVW = (double*) pFetchDatum->dat_val.dat_dec;
		nCntLDVW = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_231_FetchLoadWave(pLDVW, nCntLDVW);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Init(void)
{
	if (doDIG_SCOPE_231_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Load(void)
{
	if (doDIG_SCOPE_231_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Open(void)
{
	if (doDIG_SCOPE_231_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Reset(void)
{
	if (doDIG_SCOPE_231_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Setup(void)
{
	if (doDIG_SCOPE_231_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_231_Status(void)
{
	if (doDIG_SCOPE_231_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Close(void)
{
	if (doDIG_SCOPE_232_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Connect(void)
{
	if (doDIG_SCOPE_232_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_DisConnect(void)
{
	if (doDIG_SCOPE_232_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Fetch(void)
{
	//if (doDIG_SCOPE_232_FetchCompareWave() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod () == M_CMWV) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("compare-wave");
		pFetchDatum->dat_val.dat_dec[0] = doDIG_SCOPE_232_FetchCompareWave();
		nRtn = 1;
	}
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Init(void)
{
	if (doDIG_SCOPE_232_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Load(void)
{
	if (doDIG_SCOPE_232_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Open(void)
{
	if (doDIG_SCOPE_232_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Reset(void)
{
	if (doDIG_SCOPE_232_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Setup(void)
{
	if (doDIG_SCOPE_232_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_232_Status(void)
{
	if (doDIG_SCOPE_232_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Close(void)
{
	if (doDIG_SCOPE_233_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Connect(void)
{
	if (doDIG_SCOPE_233_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_DisConnect(void)
{
	if (doDIG_SCOPE_233_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Fetch(void)
{
	//if (doDIG_SCOPE_233_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_233_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Init(void)
{
	if (doDIG_SCOPE_233_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Load(void)
{
	if (doDIG_SCOPE_233_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Open(void)
{
	if (doDIG_SCOPE_233_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Reset(void)
{
	if (doDIG_SCOPE_233_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Setup(void)
{
	if (doDIG_SCOPE_233_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_233_Status(void)
{
	if (doDIG_SCOPE_233_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Close(void)
{
	if (doDIG_SCOPE_234_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Connect(void)
{
	if (doDIG_SCOPE_234_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_DisConnect(void)
{
	if (doDIG_SCOPE_234_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Fetch(void)
{
	//if (doDIG_SCOPE_234_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_234_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Init(void)
{
	if (doDIG_SCOPE_234_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Load(void)
{
	if (doDIG_SCOPE_234_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Open(void)
{
	if (doDIG_SCOPE_234_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Reset(void)
{
	if (doDIG_SCOPE_234_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Setup(void)
{
	if (doDIG_SCOPE_234_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_234_Status(void)
{
	if (doDIG_SCOPE_234_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Close(void)
{
	if (doDIG_SCOPE_235_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Connect(void)
{
	if (doDIG_SCOPE_235_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_DisConnect(void)
{
	if (doDIG_SCOPE_235_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Fetch(void)
{
	//if (doDIG_SCOPE_235_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_235_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Init(void)
{
	if (doDIG_SCOPE_235_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Load(void)
{
	if (doDIG_SCOPE_235_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Open(void)
{
	if (doDIG_SCOPE_235_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Reset(void)
{
	if (doDIG_SCOPE_235_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Setup(void)
{
	if (doDIG_SCOPE_235_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_235_Status(void)
{
	if (doDIG_SCOPE_235_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Close(void)
{
	if (doDIG_SCOPE_236_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Connect(void)
{
	if (doDIG_SCOPE_236_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_DisConnect(void)
{
	if (doDIG_SCOPE_236_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Fetch(void)
{
	//if (doDIG_SCOPE_236_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_236_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Init(void)
{
	if (doDIG_SCOPE_236_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Load(void)
{
	if (doDIG_SCOPE_236_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Open(void)
{
	if (doDIG_SCOPE_236_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Reset(void)
{
	if (doDIG_SCOPE_236_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Setup(void)
{
	if (doDIG_SCOPE_236_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_236_Status(void)
{
	if (doDIG_SCOPE_236_Status() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Close(void)
{
	if (doDIG_SCOPE_237_Close() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Connect(void)
{
	if (doDIG_SCOPE_237_Connect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_DisConnect(void)
{
	if (doDIG_SCOPE_237_DisConnect() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Fetch(void)
{
	//if (doDIG_SCOPE_237_FetchMath() < 0)
	//	BusErr ("DSO_1");
	DATUM *pFetchDatum;
	int nRtn;
	double* pMATH = NULL; int nCntMATH = 0;
	pFetchDatum = FthDat();
	if (pFetchDatum && FthMod() == M_MATH) {
		if (DatTyp(pFetchDatum) != DECV)
			return TypeErr("math");
		pMATH = (double*) pFetchDatum->dat_val.dat_dec;
		nCntMATH = DatCnt (pFetchDatum);
	}
	nRtn = doDIG_SCOPE_237_FetchMath(pMATH, nCntMATH);
	if (nRtn < 0)
		BusErr ("DSO_1");
	FthCnt(nRtn);
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Init(void)
{
	if (doDIG_SCOPE_237_Init() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Load(void)
{
	if (doDIG_SCOPE_237_Load() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Open(void)
{
	if (doDIG_SCOPE_237_Open() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Reset(void)
{
	if (doDIG_SCOPE_237_Reset() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Setup(void)
{
	if (doDIG_SCOPE_237_Setup() < 0)
		BusErr ("DSO_1");
	return 0;
}
int CCALLBACK Wrapper_DIG_SCOPE_237_Status(void)
{
	if (doDIG_SCOPE_237_Status() < 0)
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
//
//	DIG_SCOPE:CH101
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_FMSR);  // fm-source
	p_mod = BldModDat (p_mod, (short) M_HV09);  // harm-9-voltage
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RPLE);  // reply-eff
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[2].d_modlst = p_mod;
	DevDat[2].d_fncP = 101;
	DevDat[2].d_acts[A_CLS] = Wrapper_DIG_SCOPE_101_Close;
	DevDat[2].d_acts[A_CON] = Wrapper_DIG_SCOPE_101_Connect;
	DevDat[2].d_acts[A_DIS] = Wrapper_DIG_SCOPE_101_DisConnect;
	DevDat[2].d_acts[A_FTH] = Wrapper_DIG_SCOPE_101_Fetch;
	DevDat[2].d_acts[A_INX] = Wrapper_DIG_SCOPE_101_Init;
	DevDat[2].d_acts[A_LOD] = Wrapper_DIG_SCOPE_101_Load;
	DevDat[2].d_acts[A_OPN] = Wrapper_DIG_SCOPE_101_Open;
	DevDat[2].d_acts[A_RST] = Wrapper_DIG_SCOPE_101_Reset;
	DevDat[2].d_acts[A_FNC] = Wrapper_DIG_SCOPE_101_Setup;
	DevDat[2].d_acts[A_STA] = Wrapper_DIG_SCOPE_101_Status;
//
//	DIG_SCOPE:CH102
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_FMSR);  // fm-source
	p_mod = BldModDat (p_mod, (short) M_HV09);  // harm-9-voltage
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RPLE);  // reply-eff
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[3].d_modlst = p_mod;
	DevDat[3].d_fncP = 102;
	DevDat[3].d_acts[A_CLS] = Wrapper_DIG_SCOPE_102_Close;
	DevDat[3].d_acts[A_CON] = Wrapper_DIG_SCOPE_102_Connect;
	DevDat[3].d_acts[A_DIS] = Wrapper_DIG_SCOPE_102_DisConnect;
	DevDat[3].d_acts[A_FTH] = Wrapper_DIG_SCOPE_102_Fetch;
	DevDat[3].d_acts[A_INX] = Wrapper_DIG_SCOPE_102_Init;
	DevDat[3].d_acts[A_LOD] = Wrapper_DIG_SCOPE_102_Load;
	DevDat[3].d_acts[A_OPN] = Wrapper_DIG_SCOPE_102_Open;
	DevDat[3].d_acts[A_RST] = Wrapper_DIG_SCOPE_102_Reset;
	DevDat[3].d_acts[A_FNC] = Wrapper_DIG_SCOPE_102_Setup;
	DevDat[3].d_acts[A_STA] = Wrapper_DIG_SCOPE_102_Status;
//
//	DIG_SCOPE:CH103
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_FMSR);  // fm-source
	p_mod = BldModDat (p_mod, (short) M_HV09);  // harm-9-voltage
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RPLE);  // reply-eff
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[4].d_modlst = p_mod;
	DevDat[4].d_fncP = 103;
	DevDat[4].d_acts[A_CLS] = Wrapper_DIG_SCOPE_103_Close;
	DevDat[4].d_acts[A_CON] = Wrapper_DIG_SCOPE_103_Connect;
	DevDat[4].d_acts[A_DIS] = Wrapper_DIG_SCOPE_103_DisConnect;
	DevDat[4].d_acts[A_FTH] = Wrapper_DIG_SCOPE_103_Fetch;
	DevDat[4].d_acts[A_INX] = Wrapper_DIG_SCOPE_103_Init;
	DevDat[4].d_acts[A_LOD] = Wrapper_DIG_SCOPE_103_Load;
	DevDat[4].d_acts[A_OPN] = Wrapper_DIG_SCOPE_103_Open;
	DevDat[4].d_acts[A_RST] = Wrapper_DIG_SCOPE_103_Reset;
	DevDat[4].d_acts[A_FNC] = Wrapper_DIG_SCOPE_103_Setup;
	DevDat[4].d_acts[A_STA] = Wrapper_DIG_SCOPE_103_Status;
//
//	DIG_SCOPE:CH104
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[5].d_modlst = p_mod;
	DevDat[5].d_fncP = 104;
	DevDat[5].d_acts[A_CLS] = Wrapper_DIG_SCOPE_104_Close;
	DevDat[5].d_acts[A_CON] = Wrapper_DIG_SCOPE_104_Connect;
	DevDat[5].d_acts[A_DIS] = Wrapper_DIG_SCOPE_104_DisConnect;
	DevDat[5].d_acts[A_FTH] = Wrapper_DIG_SCOPE_104_Fetch;
	DevDat[5].d_acts[A_INX] = Wrapper_DIG_SCOPE_104_Init;
	DevDat[5].d_acts[A_LOD] = Wrapper_DIG_SCOPE_104_Load;
	DevDat[5].d_acts[A_OPN] = Wrapper_DIG_SCOPE_104_Open;
	DevDat[5].d_acts[A_RST] = Wrapper_DIG_SCOPE_104_Reset;
	DevDat[5].d_acts[A_FNC] = Wrapper_DIG_SCOPE_104_Setup;
	DevDat[5].d_acts[A_STA] = Wrapper_DIG_SCOPE_104_Status;
//
//	DIG_SCOPE:CH107
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[6].d_modlst = p_mod;
	DevDat[6].d_fncP = 107;
	DevDat[6].d_acts[A_CLS] = Wrapper_DIG_SCOPE_107_Close;
	DevDat[6].d_acts[A_CON] = Wrapper_DIG_SCOPE_107_Connect;
	DevDat[6].d_acts[A_DIS] = Wrapper_DIG_SCOPE_107_DisConnect;
	DevDat[6].d_acts[A_FTH] = Wrapper_DIG_SCOPE_107_Fetch;
	DevDat[6].d_acts[A_INX] = Wrapper_DIG_SCOPE_107_Init;
	DevDat[6].d_acts[A_LOD] = Wrapper_DIG_SCOPE_107_Load;
	DevDat[6].d_acts[A_OPN] = Wrapper_DIG_SCOPE_107_Open;
	DevDat[6].d_acts[A_RST] = Wrapper_DIG_SCOPE_107_Reset;
	DevDat[6].d_acts[A_FNC] = Wrapper_DIG_SCOPE_107_Setup;
	DevDat[6].d_acts[A_STA] = Wrapper_DIG_SCOPE_107_Status;
//
//	DIG_SCOPE:CH108
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[7].d_modlst = p_mod;
	DevDat[7].d_fncP = 108;
	DevDat[7].d_acts[A_CLS] = Wrapper_DIG_SCOPE_108_Close;
	DevDat[7].d_acts[A_CON] = Wrapper_DIG_SCOPE_108_Connect;
	DevDat[7].d_acts[A_DIS] = Wrapper_DIG_SCOPE_108_DisConnect;
	DevDat[7].d_acts[A_FTH] = Wrapper_DIG_SCOPE_108_Fetch;
	DevDat[7].d_acts[A_INX] = Wrapper_DIG_SCOPE_108_Init;
	DevDat[7].d_acts[A_LOD] = Wrapper_DIG_SCOPE_108_Load;
	DevDat[7].d_acts[A_OPN] = Wrapper_DIG_SCOPE_108_Open;
	DevDat[7].d_acts[A_RST] = Wrapper_DIG_SCOPE_108_Reset;
	DevDat[7].d_acts[A_FNC] = Wrapper_DIG_SCOPE_108_Setup;
	DevDat[7].d_acts[A_STA] = Wrapper_DIG_SCOPE_108_Status;
//
//	DIG_SCOPE:CH109
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[8].d_modlst = p_mod;
	DevDat[8].d_fncP = 109;
	DevDat[8].d_acts[A_CLS] = Wrapper_DIG_SCOPE_109_Close;
	DevDat[8].d_acts[A_CON] = Wrapper_DIG_SCOPE_109_Connect;
	DevDat[8].d_acts[A_DIS] = Wrapper_DIG_SCOPE_109_DisConnect;
	DevDat[8].d_acts[A_FTH] = Wrapper_DIG_SCOPE_109_Fetch;
	DevDat[8].d_acts[A_INX] = Wrapper_DIG_SCOPE_109_Init;
	DevDat[8].d_acts[A_LOD] = Wrapper_DIG_SCOPE_109_Load;
	DevDat[8].d_acts[A_OPN] = Wrapper_DIG_SCOPE_109_Open;
	DevDat[8].d_acts[A_RST] = Wrapper_DIG_SCOPE_109_Reset;
	DevDat[8].d_acts[A_FNC] = Wrapper_DIG_SCOPE_109_Setup;
	DevDat[8].d_acts[A_STA] = Wrapper_DIG_SCOPE_109_Status;
//
//	DIG_SCOPE:CH11
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_TRUE);  // true
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[9].d_modlst = p_mod;
	DevDat[9].d_fncP = 11;
	DevDat[9].d_acts[A_CLS] = Wrapper_DIG_SCOPE_11_Close;
	DevDat[9].d_acts[A_CON] = Wrapper_DIG_SCOPE_11_Connect;
	DevDat[9].d_acts[A_DIS] = Wrapper_DIG_SCOPE_11_DisConnect;
	DevDat[9].d_acts[A_FTH] = Wrapper_DIG_SCOPE_11_Fetch;
	DevDat[9].d_acts[A_INX] = Wrapper_DIG_SCOPE_11_Init;
	DevDat[9].d_acts[A_LOD] = Wrapper_DIG_SCOPE_11_Load;
	DevDat[9].d_acts[A_OPN] = Wrapper_DIG_SCOPE_11_Open;
	DevDat[9].d_acts[A_RST] = Wrapper_DIG_SCOPE_11_Reset;
	DevDat[9].d_acts[A_FNC] = Wrapper_DIG_SCOPE_11_Setup;
	DevDat[9].d_acts[A_STA] = Wrapper_DIG_SCOPE_11_Status;
//
//	DIG_SCOPE:CH110
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[10].d_modlst = p_mod;
	DevDat[10].d_fncP = 110;
	DevDat[10].d_acts[A_CLS] = Wrapper_DIG_SCOPE_110_Close;
	DevDat[10].d_acts[A_CON] = Wrapper_DIG_SCOPE_110_Connect;
	DevDat[10].d_acts[A_DIS] = Wrapper_DIG_SCOPE_110_DisConnect;
	DevDat[10].d_acts[A_FTH] = Wrapper_DIG_SCOPE_110_Fetch;
	DevDat[10].d_acts[A_INX] = Wrapper_DIG_SCOPE_110_Init;
	DevDat[10].d_acts[A_LOD] = Wrapper_DIG_SCOPE_110_Load;
	DevDat[10].d_acts[A_OPN] = Wrapper_DIG_SCOPE_110_Open;
	DevDat[10].d_acts[A_RST] = Wrapper_DIG_SCOPE_110_Reset;
	DevDat[10].d_acts[A_FNC] = Wrapper_DIG_SCOPE_110_Setup;
	DevDat[10].d_acts[A_STA] = Wrapper_DIG_SCOPE_110_Status;
//
//	DIG_SCOPE:CH111
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[11].d_modlst = p_mod;
	DevDat[11].d_fncP = 111;
	DevDat[11].d_acts[A_CLS] = Wrapper_DIG_SCOPE_111_Close;
	DevDat[11].d_acts[A_CON] = Wrapper_DIG_SCOPE_111_Connect;
	DevDat[11].d_acts[A_DIS] = Wrapper_DIG_SCOPE_111_DisConnect;
	DevDat[11].d_acts[A_FTH] = Wrapper_DIG_SCOPE_111_Fetch;
	DevDat[11].d_acts[A_INX] = Wrapper_DIG_SCOPE_111_Init;
	DevDat[11].d_acts[A_LOD] = Wrapper_DIG_SCOPE_111_Load;
	DevDat[11].d_acts[A_OPN] = Wrapper_DIG_SCOPE_111_Open;
	DevDat[11].d_acts[A_RST] = Wrapper_DIG_SCOPE_111_Reset;
	DevDat[11].d_acts[A_FNC] = Wrapper_DIG_SCOPE_111_Setup;
	DevDat[11].d_acts[A_STA] = Wrapper_DIG_SCOPE_111_Status;
//
//	DIG_SCOPE:CH112
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[12].d_modlst = p_mod;
	DevDat[12].d_fncP = 112;
	DevDat[12].d_acts[A_CLS] = Wrapper_DIG_SCOPE_112_Close;
	DevDat[12].d_acts[A_CON] = Wrapper_DIG_SCOPE_112_Connect;
	DevDat[12].d_acts[A_DIS] = Wrapper_DIG_SCOPE_112_DisConnect;
	DevDat[12].d_acts[A_FTH] = Wrapper_DIG_SCOPE_112_Fetch;
	DevDat[12].d_acts[A_INX] = Wrapper_DIG_SCOPE_112_Init;
	DevDat[12].d_acts[A_LOD] = Wrapper_DIG_SCOPE_112_Load;
	DevDat[12].d_acts[A_OPN] = Wrapper_DIG_SCOPE_112_Open;
	DevDat[12].d_acts[A_RST] = Wrapper_DIG_SCOPE_112_Reset;
	DevDat[12].d_acts[A_FNC] = Wrapper_DIG_SCOPE_112_Setup;
	DevDat[12].d_acts[A_STA] = Wrapper_DIG_SCOPE_112_Status;
//
//	DIG_SCOPE:CH113
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[13].d_modlst = p_mod;
	DevDat[13].d_fncP = 113;
	DevDat[13].d_acts[A_CLS] = Wrapper_DIG_SCOPE_113_Close;
	DevDat[13].d_acts[A_CON] = Wrapper_DIG_SCOPE_113_Connect;
	DevDat[13].d_acts[A_DIS] = Wrapper_DIG_SCOPE_113_DisConnect;
	DevDat[13].d_acts[A_FTH] = Wrapper_DIG_SCOPE_113_Fetch;
	DevDat[13].d_acts[A_INX] = Wrapper_DIG_SCOPE_113_Init;
	DevDat[13].d_acts[A_LOD] = Wrapper_DIG_SCOPE_113_Load;
	DevDat[13].d_acts[A_OPN] = Wrapper_DIG_SCOPE_113_Open;
	DevDat[13].d_acts[A_RST] = Wrapper_DIG_SCOPE_113_Reset;
	DevDat[13].d_acts[A_FNC] = Wrapper_DIG_SCOPE_113_Setup;
	DevDat[13].d_acts[A_STA] = Wrapper_DIG_SCOPE_113_Status;
//
//	DIG_SCOPE:CH114
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[14].d_modlst = p_mod;
	DevDat[14].d_fncP = 114;
	DevDat[14].d_acts[A_CLS] = Wrapper_DIG_SCOPE_114_Close;
	DevDat[14].d_acts[A_CON] = Wrapper_DIG_SCOPE_114_Connect;
	DevDat[14].d_acts[A_DIS] = Wrapper_DIG_SCOPE_114_DisConnect;
	DevDat[14].d_acts[A_FTH] = Wrapper_DIG_SCOPE_114_Fetch;
	DevDat[14].d_acts[A_INX] = Wrapper_DIG_SCOPE_114_Init;
	DevDat[14].d_acts[A_LOD] = Wrapper_DIG_SCOPE_114_Load;
	DevDat[14].d_acts[A_OPN] = Wrapper_DIG_SCOPE_114_Open;
	DevDat[14].d_acts[A_RST] = Wrapper_DIG_SCOPE_114_Reset;
	DevDat[14].d_acts[A_FNC] = Wrapper_DIG_SCOPE_114_Setup;
	DevDat[14].d_acts[A_STA] = Wrapper_DIG_SCOPE_114_Status;
//
//	DIG_SCOPE:CH115
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[15].d_modlst = p_mod;
	DevDat[15].d_fncP = 115;
	DevDat[15].d_acts[A_CLS] = Wrapper_DIG_SCOPE_115_Close;
	DevDat[15].d_acts[A_CON] = Wrapper_DIG_SCOPE_115_Connect;
	DevDat[15].d_acts[A_DIS] = Wrapper_DIG_SCOPE_115_DisConnect;
	DevDat[15].d_acts[A_FTH] = Wrapper_DIG_SCOPE_115_Fetch;
	DevDat[15].d_acts[A_INX] = Wrapper_DIG_SCOPE_115_Init;
	DevDat[15].d_acts[A_LOD] = Wrapper_DIG_SCOPE_115_Load;
	DevDat[15].d_acts[A_OPN] = Wrapper_DIG_SCOPE_115_Open;
	DevDat[15].d_acts[A_RST] = Wrapper_DIG_SCOPE_115_Reset;
	DevDat[15].d_acts[A_FNC] = Wrapper_DIG_SCOPE_115_Setup;
	DevDat[15].d_acts[A_STA] = Wrapper_DIG_SCOPE_115_Status;
//
//	DIG_SCOPE:CH116
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[16].d_modlst = p_mod;
	DevDat[16].d_fncP = 116;
	DevDat[16].d_acts[A_CLS] = Wrapper_DIG_SCOPE_116_Close;
	DevDat[16].d_acts[A_CON] = Wrapper_DIG_SCOPE_116_Connect;
	DevDat[16].d_acts[A_DIS] = Wrapper_DIG_SCOPE_116_DisConnect;
	DevDat[16].d_acts[A_FTH] = Wrapper_DIG_SCOPE_116_Fetch;
	DevDat[16].d_acts[A_INX] = Wrapper_DIG_SCOPE_116_Init;
	DevDat[16].d_acts[A_LOD] = Wrapper_DIG_SCOPE_116_Load;
	DevDat[16].d_acts[A_OPN] = Wrapper_DIG_SCOPE_116_Open;
	DevDat[16].d_acts[A_RST] = Wrapper_DIG_SCOPE_116_Reset;
	DevDat[16].d_acts[A_FNC] = Wrapper_DIG_SCOPE_116_Setup;
	DevDat[16].d_acts[A_STA] = Wrapper_DIG_SCOPE_116_Status;
//
//	DIG_SCOPE:CH117
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_FMSR);  // fm-source
	p_mod = BldModDat (p_mod, (short) M_HV09);  // harm-9-voltage
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RPLE);  // reply-eff
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[17].d_modlst = p_mod;
	DevDat[17].d_fncP = 117;
	DevDat[17].d_acts[A_CLS] = Wrapper_DIG_SCOPE_117_Close;
	DevDat[17].d_acts[A_CON] = Wrapper_DIG_SCOPE_117_Connect;
	DevDat[17].d_acts[A_DIS] = Wrapper_DIG_SCOPE_117_DisConnect;
	DevDat[17].d_acts[A_FTH] = Wrapper_DIG_SCOPE_117_Fetch;
	DevDat[17].d_acts[A_INX] = Wrapper_DIG_SCOPE_117_Init;
	DevDat[17].d_acts[A_LOD] = Wrapper_DIG_SCOPE_117_Load;
	DevDat[17].d_acts[A_OPN] = Wrapper_DIG_SCOPE_117_Open;
	DevDat[17].d_acts[A_RST] = Wrapper_DIG_SCOPE_117_Reset;
	DevDat[17].d_acts[A_FNC] = Wrapper_DIG_SCOPE_117_Setup;
	DevDat[17].d_acts[A_STA] = Wrapper_DIG_SCOPE_117_Status;
//
//	DIG_SCOPE:CH118
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[18].d_modlst = p_mod;
	DevDat[18].d_fncP = 118;
	DevDat[18].d_acts[A_CLS] = Wrapper_DIG_SCOPE_118_Close;
	DevDat[18].d_acts[A_CON] = Wrapper_DIG_SCOPE_118_Connect;
	DevDat[18].d_acts[A_DIS] = Wrapper_DIG_SCOPE_118_DisConnect;
	DevDat[18].d_acts[A_FTH] = Wrapper_DIG_SCOPE_118_Fetch;
	DevDat[18].d_acts[A_INX] = Wrapper_DIG_SCOPE_118_Init;
	DevDat[18].d_acts[A_LOD] = Wrapper_DIG_SCOPE_118_Load;
	DevDat[18].d_acts[A_OPN] = Wrapper_DIG_SCOPE_118_Open;
	DevDat[18].d_acts[A_RST] = Wrapper_DIG_SCOPE_118_Reset;
	DevDat[18].d_acts[A_FNC] = Wrapper_DIG_SCOPE_118_Setup;
	DevDat[18].d_acts[A_STA] = Wrapper_DIG_SCOPE_118_Status;
//
//	DIG_SCOPE:CH119
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[19].d_modlst = p_mod;
	DevDat[19].d_fncP = 119;
	DevDat[19].d_acts[A_CLS] = Wrapper_DIG_SCOPE_119_Close;
	DevDat[19].d_acts[A_CON] = Wrapper_DIG_SCOPE_119_Connect;
	DevDat[19].d_acts[A_DIS] = Wrapper_DIG_SCOPE_119_DisConnect;
	DevDat[19].d_acts[A_FTH] = Wrapper_DIG_SCOPE_119_Fetch;
	DevDat[19].d_acts[A_INX] = Wrapper_DIG_SCOPE_119_Init;
	DevDat[19].d_acts[A_LOD] = Wrapper_DIG_SCOPE_119_Load;
	DevDat[19].d_acts[A_OPN] = Wrapper_DIG_SCOPE_119_Open;
	DevDat[19].d_acts[A_RST] = Wrapper_DIG_SCOPE_119_Reset;
	DevDat[19].d_acts[A_FNC] = Wrapper_DIG_SCOPE_119_Setup;
	DevDat[19].d_acts[A_STA] = Wrapper_DIG_SCOPE_119_Status;
//
//	DIG_SCOPE:CH12
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_TRUE);  // true
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[20].d_modlst = p_mod;
	DevDat[20].d_fncP = 12;
	DevDat[20].d_acts[A_CLS] = Wrapper_DIG_SCOPE_12_Close;
	DevDat[20].d_acts[A_CON] = Wrapper_DIG_SCOPE_12_Connect;
	DevDat[20].d_acts[A_DIS] = Wrapper_DIG_SCOPE_12_DisConnect;
	DevDat[20].d_acts[A_FTH] = Wrapper_DIG_SCOPE_12_Fetch;
	DevDat[20].d_acts[A_INX] = Wrapper_DIG_SCOPE_12_Init;
	DevDat[20].d_acts[A_LOD] = Wrapper_DIG_SCOPE_12_Load;
	DevDat[20].d_acts[A_OPN] = Wrapper_DIG_SCOPE_12_Open;
	DevDat[20].d_acts[A_RST] = Wrapper_DIG_SCOPE_12_Reset;
	DevDat[20].d_acts[A_FNC] = Wrapper_DIG_SCOPE_12_Setup;
	DevDat[20].d_acts[A_STA] = Wrapper_DIG_SCOPE_12_Status;
//
//	DIG_SCOPE:CH120
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_LUMT);  // lum-temp
	p_mod = BldModDat (p_mod, (short) M_MSNR);  // minimum-sense-rate
	p_mod = BldModDat (p_mod, (short) M_MODO);  // mod-offset
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[21].d_modlst = p_mod;
	DevDat[21].d_fncP = 120;
	DevDat[21].d_acts[A_CLS] = Wrapper_DIG_SCOPE_120_Close;
	DevDat[21].d_acts[A_CON] = Wrapper_DIG_SCOPE_120_Connect;
	DevDat[21].d_acts[A_DIS] = Wrapper_DIG_SCOPE_120_DisConnect;
	DevDat[21].d_acts[A_FTH] = Wrapper_DIG_SCOPE_120_Fetch;
	DevDat[21].d_acts[A_INX] = Wrapper_DIG_SCOPE_120_Init;
	DevDat[21].d_acts[A_LOD] = Wrapper_DIG_SCOPE_120_Load;
	DevDat[21].d_acts[A_OPN] = Wrapper_DIG_SCOPE_120_Open;
	DevDat[21].d_acts[A_RST] = Wrapper_DIG_SCOPE_120_Reset;
	DevDat[21].d_acts[A_FNC] = Wrapper_DIG_SCOPE_120_Setup;
	DevDat[21].d_acts[A_STA] = Wrapper_DIG_SCOPE_120_Status;
//
//	DIG_SCOPE:CH121
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[22].d_modlst = p_mod;
	DevDat[22].d_fncP = 121;
	DevDat[22].d_acts[A_CLS] = Wrapper_DIG_SCOPE_121_Close;
	DevDat[22].d_acts[A_CON] = Wrapper_DIG_SCOPE_121_Connect;
	DevDat[22].d_acts[A_DIS] = Wrapper_DIG_SCOPE_121_DisConnect;
	DevDat[22].d_acts[A_FTH] = Wrapper_DIG_SCOPE_121_Fetch;
	DevDat[22].d_acts[A_INX] = Wrapper_DIG_SCOPE_121_Init;
	DevDat[22].d_acts[A_LOD] = Wrapper_DIG_SCOPE_121_Load;
	DevDat[22].d_acts[A_OPN] = Wrapper_DIG_SCOPE_121_Open;
	DevDat[22].d_acts[A_RST] = Wrapper_DIG_SCOPE_121_Reset;
	DevDat[22].d_acts[A_FNC] = Wrapper_DIG_SCOPE_121_Setup;
	DevDat[22].d_acts[A_STA] = Wrapper_DIG_SCOPE_121_Status;
//
//	DIG_SCOPE:CH122
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[23].d_modlst = p_mod;
	DevDat[23].d_fncP = 122;
	DevDat[23].d_acts[A_CLS] = Wrapper_DIG_SCOPE_122_Close;
	DevDat[23].d_acts[A_CON] = Wrapper_DIG_SCOPE_122_Connect;
	DevDat[23].d_acts[A_DIS] = Wrapper_DIG_SCOPE_122_DisConnect;
	DevDat[23].d_acts[A_FTH] = Wrapper_DIG_SCOPE_122_Fetch;
	DevDat[23].d_acts[A_INX] = Wrapper_DIG_SCOPE_122_Init;
	DevDat[23].d_acts[A_LOD] = Wrapper_DIG_SCOPE_122_Load;
	DevDat[23].d_acts[A_OPN] = Wrapper_DIG_SCOPE_122_Open;
	DevDat[23].d_acts[A_RST] = Wrapper_DIG_SCOPE_122_Reset;
	DevDat[23].d_acts[A_FNC] = Wrapper_DIG_SCOPE_122_Setup;
	DevDat[23].d_acts[A_STA] = Wrapper_DIG_SCOPE_122_Status;
//
//	DSO_1:CH123
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[24].d_modlst = p_mod;
	DevDat[24].d_fncP = 123;
	DevDat[24].d_acts[A_CLS] = Wrapper_DIG_SCOPE_123_Close;
	DevDat[24].d_acts[A_CON] = Wrapper_DIG_SCOPE_123_Connect;
	DevDat[24].d_acts[A_DIS] = Wrapper_DIG_SCOPE_123_Disconnect;
	DevDat[24].d_acts[A_FTH] = Wrapper_DIG_SCOPE_123_Fetch;
	DevDat[24].d_acts[A_INX] = Wrapper_DIG_SCOPE_123_Init;
	DevDat[24].d_acts[A_LOD] = Wrapper_DIG_SCOPE_123_Load;
	DevDat[24].d_acts[A_OPN] = Wrapper_DIG_SCOPE_123_Open;
	DevDat[24].d_acts[A_RST] = Wrapper_DIG_SCOPE_123_Reset;
	DevDat[24].d_acts[A_FNC] = Wrapper_DIG_SCOPE_123_Setup;
	DevDat[24].d_acts[A_STA] = Wrapper_DIG_SCOPE_123_Status;
//
//	DSO_1:CH124
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[25].d_modlst = p_mod;
	DevDat[25].d_fncP = 124;
	DevDat[25].d_acts[A_CLS] = Wrapper_DIG_SCOPE_124_Close;
	DevDat[25].d_acts[A_CON] = Wrapper_DIG_SCOPE_124_Connect;
	DevDat[25].d_acts[A_DIS] = Wrapper_DIG_SCOPE_124_Disconnect;
	DevDat[25].d_acts[A_FTH] = Wrapper_DIG_SCOPE_124_Fetch;
	DevDat[25].d_acts[A_INX] = Wrapper_DIG_SCOPE_124_Init;
	DevDat[25].d_acts[A_LOD] = Wrapper_DIG_SCOPE_124_Load;
	DevDat[25].d_acts[A_OPN] = Wrapper_DIG_SCOPE_124_Open;
	DevDat[25].d_acts[A_RST] = Wrapper_DIG_SCOPE_124_Reset;
	DevDat[25].d_acts[A_FNC] = Wrapper_DIG_SCOPE_124_Setup;
	DevDat[25].d_acts[A_STA] = Wrapper_DIG_SCOPE_124_Status;
//
//	DIG_SCOPE:CH13
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_TRUE);  // true
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[24].d_modlst = p_mod;
	DevDat[24].d_fncP = 13;
	DevDat[24].d_acts[A_CLS] = Wrapper_DIG_SCOPE_13_Close;
	DevDat[24].d_acts[A_CON] = Wrapper_DIG_SCOPE_13_Connect;
	DevDat[24].d_acts[A_DIS] = Wrapper_DIG_SCOPE_13_DisConnect;
	DevDat[24].d_acts[A_FTH] = Wrapper_DIG_SCOPE_13_Fetch;
	DevDat[24].d_acts[A_INX] = Wrapper_DIG_SCOPE_13_Init;
	DevDat[24].d_acts[A_LOD] = Wrapper_DIG_SCOPE_13_Load;
	DevDat[24].d_acts[A_OPN] = Wrapper_DIG_SCOPE_13_Open;
	DevDat[24].d_acts[A_RST] = Wrapper_DIG_SCOPE_13_Reset;
	DevDat[24].d_acts[A_FNC] = Wrapper_DIG_SCOPE_13_Setup;
	DevDat[24].d_acts[A_STA] = Wrapper_DIG_SCOPE_13_Status;
//
//	DIG_SCOPE:CH130
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CHRM);  // car-harmonics
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HP04);  // harm-4-power
	p_mod = BldModDat (p_mod, (short) M_MDPN);  // mod-pneg
	p_mod = BldModDat (p_mod, (short) M_MDPP);  // mod-ppos
	p_mod = BldModDat (p_mod, (short) M_MDSC);  // mod-source
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[25].d_modlst = p_mod;
	DevDat[25].d_fncP = 130;
	DevDat[25].d_acts[A_CLS] = Wrapper_DIG_SCOPE_130_Close;
	DevDat[25].d_acts[A_CON] = Wrapper_DIG_SCOPE_130_Connect;
	DevDat[25].d_acts[A_DIS] = Wrapper_DIG_SCOPE_130_DisConnect;
	DevDat[25].d_acts[A_FTH] = Wrapper_DIG_SCOPE_130_Fetch;
	DevDat[25].d_acts[A_INX] = Wrapper_DIG_SCOPE_130_Init;
	DevDat[25].d_acts[A_LOD] = Wrapper_DIG_SCOPE_130_Load;
	DevDat[25].d_acts[A_OPN] = Wrapper_DIG_SCOPE_130_Open;
	DevDat[25].d_acts[A_RST] = Wrapper_DIG_SCOPE_130_Reset;
	DevDat[25].d_acts[A_FNC] = Wrapper_DIG_SCOPE_130_Setup;
	DevDat[25].d_acts[A_STA] = Wrapper_DIG_SCOPE_130_Status;
//
//	DIG_SCOPE:CH131
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CHRM);  // car-harmonics
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HP02);  // harm-2-power
	p_mod = BldModDat (p_mod, (short) M_HP04);  // harm-4-power
	p_mod = BldModDat (p_mod, (short) M_MDSC);  // mod-source
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[26].d_modlst = p_mod;
	DevDat[26].d_fncP = 131;
	DevDat[26].d_acts[A_CLS] = Wrapper_DIG_SCOPE_131_Close;
	DevDat[26].d_acts[A_CON] = Wrapper_DIG_SCOPE_131_Connect;
	DevDat[26].d_acts[A_DIS] = Wrapper_DIG_SCOPE_131_DisConnect;
	DevDat[26].d_acts[A_FTH] = Wrapper_DIG_SCOPE_131_Fetch;
	DevDat[26].d_acts[A_INX] = Wrapper_DIG_SCOPE_131_Init;
	DevDat[26].d_acts[A_LOD] = Wrapper_DIG_SCOPE_131_Load;
	DevDat[26].d_acts[A_OPN] = Wrapper_DIG_SCOPE_131_Open;
	DevDat[26].d_acts[A_RST] = Wrapper_DIG_SCOPE_131_Reset;
	DevDat[26].d_acts[A_FNC] = Wrapper_DIG_SCOPE_131_Setup;
	DevDat[26].d_acts[A_STA] = Wrapper_DIG_SCOPE_131_Status;
//
//	DIG_SCOPE:CH132
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ADTO);  // add-to
	p_mod = BldModDat (p_mod, (short) M_CHRM);  // car-harmonics
	p_mod = BldModDat (p_mod, (short) M_CPHS);  // car-phase
	p_mod = BldModDat (p_mod, (short) M_CRSD);  // car-resid
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HP04);  // harm-4-power
	p_mod = BldModDat (p_mod, (short) M_MDSC);  // mod-source
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[27].d_modlst = p_mod;
	DevDat[27].d_fncP = 132;
	DevDat[27].d_acts[A_CLS] = Wrapper_DIG_SCOPE_132_Close;
	DevDat[27].d_acts[A_CON] = Wrapper_DIG_SCOPE_132_Connect;
	DevDat[27].d_acts[A_DIS] = Wrapper_DIG_SCOPE_132_DisConnect;
	DevDat[27].d_acts[A_FTH] = Wrapper_DIG_SCOPE_132_Fetch;
	DevDat[27].d_acts[A_INX] = Wrapper_DIG_SCOPE_132_Init;
	DevDat[27].d_acts[A_LOD] = Wrapper_DIG_SCOPE_132_Load;
	DevDat[27].d_acts[A_OPN] = Wrapper_DIG_SCOPE_132_Open;
	DevDat[27].d_acts[A_RST] = Wrapper_DIG_SCOPE_132_Reset;
	DevDat[27].d_acts[A_FNC] = Wrapper_DIG_SCOPE_132_Setup;
	DevDat[27].d_acts[A_STA] = Wrapper_DIG_SCOPE_132_Status;
//
//	DIG_SCOPE:CH133
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ACCD);  // acceptance-code
	p_mod = BldModDat (p_mod, (short) M_ACMK);  // acceptance-mask
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[28].d_modlst = p_mod;
	DevDat[28].d_fncP = 133;
	DevDat[28].d_acts[A_CLS] = Wrapper_DIG_SCOPE_133_Close;
	DevDat[28].d_acts[A_CON] = Wrapper_DIG_SCOPE_133_Connect;
	DevDat[28].d_acts[A_DIS] = Wrapper_DIG_SCOPE_133_DisConnect;
	DevDat[28].d_acts[A_FTH] = Wrapper_DIG_SCOPE_133_Fetch;
	DevDat[28].d_acts[A_INX] = Wrapper_DIG_SCOPE_133_Init;
	DevDat[28].d_acts[A_LOD] = Wrapper_DIG_SCOPE_133_Load;
	DevDat[28].d_acts[A_OPN] = Wrapper_DIG_SCOPE_133_Open;
	DevDat[28].d_acts[A_RST] = Wrapper_DIG_SCOPE_133_Reset;
	DevDat[28].d_acts[A_FNC] = Wrapper_DIG_SCOPE_133_Setup;
	DevDat[28].d_acts[A_STA] = Wrapper_DIG_SCOPE_133_Status;
//
//	DIG_SCOPE:CH134
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_PLAN);  // pla
	p_mod = BldModDat (p_mod, (short) M_PLAR);  // pla-rate
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[29].d_modlst = p_mod;
	DevDat[29].d_fncP = 134;
	DevDat[29].d_acts[A_CLS] = Wrapper_DIG_SCOPE_134_Close;
	DevDat[29].d_acts[A_CON] = Wrapper_DIG_SCOPE_134_Connect;
	DevDat[29].d_acts[A_DIS] = Wrapper_DIG_SCOPE_134_DisConnect;
	DevDat[29].d_acts[A_FTH] = Wrapper_DIG_SCOPE_134_Fetch;
	DevDat[29].d_acts[A_INX] = Wrapper_DIG_SCOPE_134_Init;
	DevDat[29].d_acts[A_LOD] = Wrapper_DIG_SCOPE_134_Load;
	DevDat[29].d_acts[A_OPN] = Wrapper_DIG_SCOPE_134_Open;
	DevDat[29].d_acts[A_RST] = Wrapper_DIG_SCOPE_134_Reset;
	DevDat[29].d_acts[A_FNC] = Wrapper_DIG_SCOPE_134_Setup;
	DevDat[29].d_acts[A_STA] = Wrapper_DIG_SCOPE_134_Status;
//
//	DIG_SCOPE:CH135
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_HPX2);  // harm-002-power
	p_mod = BldModDat (p_mod, (short) M_HPX3);  // harm-003-power
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[30].d_modlst = p_mod;
	DevDat[30].d_fncP = 135;
	DevDat[30].d_acts[A_CLS] = Wrapper_DIG_SCOPE_135_Close;
	DevDat[30].d_acts[A_CON] = Wrapper_DIG_SCOPE_135_Connect;
	DevDat[30].d_acts[A_DIS] = Wrapper_DIG_SCOPE_135_DisConnect;
	DevDat[30].d_acts[A_FTH] = Wrapper_DIG_SCOPE_135_Fetch;
	DevDat[30].d_acts[A_INX] = Wrapper_DIG_SCOPE_135_Init;
	DevDat[30].d_acts[A_LOD] = Wrapper_DIG_SCOPE_135_Load;
	DevDat[30].d_acts[A_OPN] = Wrapper_DIG_SCOPE_135_Open;
	DevDat[30].d_acts[A_RST] = Wrapper_DIG_SCOPE_135_Reset;
	DevDat[30].d_acts[A_FNC] = Wrapper_DIG_SCOPE_135_Setup;
	DevDat[30].d_acts[A_STA] = Wrapper_DIG_SCOPE_135_Status;
//
//	DIG_SCOPE:CH136
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_DEEM);  // de-emphasis
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[31].d_modlst = p_mod;
	DevDat[31].d_fncP = 136;
	DevDat[31].d_acts[A_CLS] = Wrapper_DIG_SCOPE_136_Close;
	DevDat[31].d_acts[A_CON] = Wrapper_DIG_SCOPE_136_Connect;
	DevDat[31].d_acts[A_DIS] = Wrapper_DIG_SCOPE_136_DisConnect;
	DevDat[31].d_acts[A_FTH] = Wrapper_DIG_SCOPE_136_Fetch;
	DevDat[31].d_acts[A_INX] = Wrapper_DIG_SCOPE_136_Init;
	DevDat[31].d_acts[A_LOD] = Wrapper_DIG_SCOPE_136_Load;
	DevDat[31].d_acts[A_OPN] = Wrapper_DIG_SCOPE_136_Open;
	DevDat[31].d_acts[A_RST] = Wrapper_DIG_SCOPE_136_Reset;
	DevDat[31].d_acts[A_FNC] = Wrapper_DIG_SCOPE_136_Setup;
	DevDat[31].d_acts[A_STA] = Wrapper_DIG_SCOPE_136_Status;
//
//	DIG_SCOPE:CH137
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_HARP);  // harm-phase
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[32].d_modlst = p_mod;
	DevDat[32].d_fncP = 137;
	DevDat[32].d_acts[A_CLS] = Wrapper_DIG_SCOPE_137_Close;
	DevDat[32].d_acts[A_CON] = Wrapper_DIG_SCOPE_137_Connect;
	DevDat[32].d_acts[A_DIS] = Wrapper_DIG_SCOPE_137_DisConnect;
	DevDat[32].d_acts[A_FTH] = Wrapper_DIG_SCOPE_137_Fetch;
	DevDat[32].d_acts[A_INX] = Wrapper_DIG_SCOPE_137_Init;
	DevDat[32].d_acts[A_LOD] = Wrapper_DIG_SCOPE_137_Load;
	DevDat[32].d_acts[A_OPN] = Wrapper_DIG_SCOPE_137_Open;
	DevDat[32].d_acts[A_RST] = Wrapper_DIG_SCOPE_137_Reset;
	DevDat[32].d_acts[A_FNC] = Wrapper_DIG_SCOPE_137_Setup;
	DevDat[32].d_acts[A_STA] = Wrapper_DIG_SCOPE_137_Status;
//
//	DIG_SCOPE:CH14
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_TRUE);  // true
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[33].d_modlst = p_mod;
	DevDat[33].d_fncP = 14;
	DevDat[33].d_acts[A_CLS] = Wrapper_DIG_SCOPE_14_Close;
	DevDat[33].d_acts[A_CON] = Wrapper_DIG_SCOPE_14_Connect;
	DevDat[33].d_acts[A_DIS] = Wrapper_DIG_SCOPE_14_DisConnect;
	DevDat[33].d_acts[A_FTH] = Wrapper_DIG_SCOPE_14_Fetch;
	DevDat[33].d_acts[A_INX] = Wrapper_DIG_SCOPE_14_Init;
	DevDat[33].d_acts[A_LOD] = Wrapper_DIG_SCOPE_14_Load;
	DevDat[33].d_acts[A_OPN] = Wrapper_DIG_SCOPE_14_Open;
	DevDat[33].d_acts[A_RST] = Wrapper_DIG_SCOPE_14_Reset;
	DevDat[33].d_acts[A_FNC] = Wrapper_DIG_SCOPE_14_Setup;
	DevDat[33].d_acts[A_STA] = Wrapper_DIG_SCOPE_14_Status;
//
//	DIG_SCOPE:CH201
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_FMSR);  // fm-source
	p_mod = BldModDat (p_mod, (short) M_HV09);  // harm-9-voltage
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RPLE);  // reply-eff
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[34].d_modlst = p_mod;
	DevDat[34].d_fncP = 201;
	DevDat[34].d_acts[A_CLS] = Wrapper_DIG_SCOPE_201_Close;
	DevDat[34].d_acts[A_CON] = Wrapper_DIG_SCOPE_201_Connect;
	DevDat[34].d_acts[A_DIS] = Wrapper_DIG_SCOPE_201_DisConnect;
	DevDat[34].d_acts[A_FTH] = Wrapper_DIG_SCOPE_201_Fetch;
	DevDat[34].d_acts[A_INX] = Wrapper_DIG_SCOPE_201_Init;
	DevDat[34].d_acts[A_LOD] = Wrapper_DIG_SCOPE_201_Load;
	DevDat[34].d_acts[A_OPN] = Wrapper_DIG_SCOPE_201_Open;
	DevDat[34].d_acts[A_RST] = Wrapper_DIG_SCOPE_201_Reset;
	DevDat[34].d_acts[A_FNC] = Wrapper_DIG_SCOPE_201_Setup;
	DevDat[34].d_acts[A_STA] = Wrapper_DIG_SCOPE_201_Status;
//
//	DIG_SCOPE:CH202
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_FMSR);  // fm-source
	p_mod = BldModDat (p_mod, (short) M_HV09);  // harm-9-voltage
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RPLE);  // reply-eff
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[35].d_modlst = p_mod;
	DevDat[35].d_fncP = 202;
	DevDat[35].d_acts[A_CLS] = Wrapper_DIG_SCOPE_202_Close;
	DevDat[35].d_acts[A_CON] = Wrapper_DIG_SCOPE_202_Connect;
	DevDat[35].d_acts[A_DIS] = Wrapper_DIG_SCOPE_202_DisConnect;
	DevDat[35].d_acts[A_FTH] = Wrapper_DIG_SCOPE_202_Fetch;
	DevDat[35].d_acts[A_INX] = Wrapper_DIG_SCOPE_202_Init;
	DevDat[35].d_acts[A_LOD] = Wrapper_DIG_SCOPE_202_Load;
	DevDat[35].d_acts[A_OPN] = Wrapper_DIG_SCOPE_202_Open;
	DevDat[35].d_acts[A_RST] = Wrapper_DIG_SCOPE_202_Reset;
	DevDat[35].d_acts[A_FNC] = Wrapper_DIG_SCOPE_202_Setup;
	DevDat[35].d_acts[A_STA] = Wrapper_DIG_SCOPE_202_Status;
//
//	DIG_SCOPE:CH203
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_FMSR);  // fm-source
	p_mod = BldModDat (p_mod, (short) M_HV09);  // harm-9-voltage
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RPLE);  // reply-eff
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[36].d_modlst = p_mod;
	DevDat[36].d_fncP = 203;
	DevDat[36].d_acts[A_CLS] = Wrapper_DIG_SCOPE_203_Close;
	DevDat[36].d_acts[A_CON] = Wrapper_DIG_SCOPE_203_Connect;
	DevDat[36].d_acts[A_DIS] = Wrapper_DIG_SCOPE_203_DisConnect;
	DevDat[36].d_acts[A_FTH] = Wrapper_DIG_SCOPE_203_Fetch;
	DevDat[36].d_acts[A_INX] = Wrapper_DIG_SCOPE_203_Init;
	DevDat[36].d_acts[A_LOD] = Wrapper_DIG_SCOPE_203_Load;
	DevDat[36].d_acts[A_OPN] = Wrapper_DIG_SCOPE_203_Open;
	DevDat[36].d_acts[A_RST] = Wrapper_DIG_SCOPE_203_Reset;
	DevDat[36].d_acts[A_FNC] = Wrapper_DIG_SCOPE_203_Setup;
	DevDat[36].d_acts[A_STA] = Wrapper_DIG_SCOPE_203_Status;
//
//	DIG_SCOPE:CH204
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[37].d_modlst = p_mod;
	DevDat[37].d_fncP = 204;
	DevDat[37].d_acts[A_CLS] = Wrapper_DIG_SCOPE_204_Close;
	DevDat[37].d_acts[A_CON] = Wrapper_DIG_SCOPE_204_Connect;
	DevDat[37].d_acts[A_DIS] = Wrapper_DIG_SCOPE_204_DisConnect;
	DevDat[37].d_acts[A_FTH] = Wrapper_DIG_SCOPE_204_Fetch;
	DevDat[37].d_acts[A_INX] = Wrapper_DIG_SCOPE_204_Init;
	DevDat[37].d_acts[A_LOD] = Wrapper_DIG_SCOPE_204_Load;
	DevDat[37].d_acts[A_OPN] = Wrapper_DIG_SCOPE_204_Open;
	DevDat[37].d_acts[A_RST] = Wrapper_DIG_SCOPE_204_Reset;
	DevDat[37].d_acts[A_FNC] = Wrapper_DIG_SCOPE_204_Setup;
	DevDat[37].d_acts[A_STA] = Wrapper_DIG_SCOPE_204_Status;
//
//	DIG_SCOPE:CH207
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[38].d_modlst = p_mod;
	DevDat[38].d_fncP = 207;
	DevDat[38].d_acts[A_CLS] = Wrapper_DIG_SCOPE_207_Close;
	DevDat[38].d_acts[A_CON] = Wrapper_DIG_SCOPE_207_Connect;
	DevDat[38].d_acts[A_DIS] = Wrapper_DIG_SCOPE_207_DisConnect;
	DevDat[38].d_acts[A_FTH] = Wrapper_DIG_SCOPE_207_Fetch;
	DevDat[38].d_acts[A_INX] = Wrapper_DIG_SCOPE_207_Init;
	DevDat[38].d_acts[A_LOD] = Wrapper_DIG_SCOPE_207_Load;
	DevDat[38].d_acts[A_OPN] = Wrapper_DIG_SCOPE_207_Open;
	DevDat[38].d_acts[A_RST] = Wrapper_DIG_SCOPE_207_Reset;
	DevDat[38].d_acts[A_FNC] = Wrapper_DIG_SCOPE_207_Setup;
	DevDat[38].d_acts[A_STA] = Wrapper_DIG_SCOPE_207_Status;
//
//	DIG_SCOPE:CH208
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[39].d_modlst = p_mod;
	DevDat[39].d_fncP = 208;
	DevDat[39].d_acts[A_CLS] = Wrapper_DIG_SCOPE_208_Close;
	DevDat[39].d_acts[A_CON] = Wrapper_DIG_SCOPE_208_Connect;
	DevDat[39].d_acts[A_DIS] = Wrapper_DIG_SCOPE_208_DisConnect;
	DevDat[39].d_acts[A_FTH] = Wrapper_DIG_SCOPE_208_Fetch;
	DevDat[39].d_acts[A_INX] = Wrapper_DIG_SCOPE_208_Init;
	DevDat[39].d_acts[A_LOD] = Wrapper_DIG_SCOPE_208_Load;
	DevDat[39].d_acts[A_OPN] = Wrapper_DIG_SCOPE_208_Open;
	DevDat[39].d_acts[A_RST] = Wrapper_DIG_SCOPE_208_Reset;
	DevDat[39].d_acts[A_FNC] = Wrapper_DIG_SCOPE_208_Setup;
	DevDat[39].d_acts[A_STA] = Wrapper_DIG_SCOPE_208_Status;
//
//	DIG_SCOPE:CH209
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[40].d_modlst = p_mod;
	DevDat[40].d_fncP = 209;
	DevDat[40].d_acts[A_CLS] = Wrapper_DIG_SCOPE_209_Close;
	DevDat[40].d_acts[A_CON] = Wrapper_DIG_SCOPE_209_Connect;
	DevDat[40].d_acts[A_DIS] = Wrapper_DIG_SCOPE_209_DisConnect;
	DevDat[40].d_acts[A_FTH] = Wrapper_DIG_SCOPE_209_Fetch;
	DevDat[40].d_acts[A_INX] = Wrapper_DIG_SCOPE_209_Init;
	DevDat[40].d_acts[A_LOD] = Wrapper_DIG_SCOPE_209_Load;
	DevDat[40].d_acts[A_OPN] = Wrapper_DIG_SCOPE_209_Open;
	DevDat[40].d_acts[A_RST] = Wrapper_DIG_SCOPE_209_Reset;
	DevDat[40].d_acts[A_FNC] = Wrapper_DIG_SCOPE_209_Setup;
	DevDat[40].d_acts[A_STA] = Wrapper_DIG_SCOPE_209_Status;
//
//	DIG_SCOPE:CH210
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[41].d_modlst = p_mod;
	DevDat[41].d_fncP = 210;
	DevDat[41].d_acts[A_CLS] = Wrapper_DIG_SCOPE_210_Close;
	DevDat[41].d_acts[A_CON] = Wrapper_DIG_SCOPE_210_Connect;
	DevDat[41].d_acts[A_DIS] = Wrapper_DIG_SCOPE_210_DisConnect;
	DevDat[41].d_acts[A_FTH] = Wrapper_DIG_SCOPE_210_Fetch;
	DevDat[41].d_acts[A_INX] = Wrapper_DIG_SCOPE_210_Init;
	DevDat[41].d_acts[A_LOD] = Wrapper_DIG_SCOPE_210_Load;
	DevDat[41].d_acts[A_OPN] = Wrapper_DIG_SCOPE_210_Open;
	DevDat[41].d_acts[A_RST] = Wrapper_DIG_SCOPE_210_Reset;
	DevDat[41].d_acts[A_FNC] = Wrapper_DIG_SCOPE_210_Setup;
	DevDat[41].d_acts[A_STA] = Wrapper_DIG_SCOPE_210_Status;
//
//	DIG_SCOPE:CH211
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[42].d_modlst = p_mod;
	DevDat[42].d_fncP = 211;
	DevDat[42].d_acts[A_CLS] = Wrapper_DIG_SCOPE_211_Close;
	DevDat[42].d_acts[A_CON] = Wrapper_DIG_SCOPE_211_Connect;
	DevDat[42].d_acts[A_DIS] = Wrapper_DIG_SCOPE_211_DisConnect;
	DevDat[42].d_acts[A_FTH] = Wrapper_DIG_SCOPE_211_Fetch;
	DevDat[42].d_acts[A_INX] = Wrapper_DIG_SCOPE_211_Init;
	DevDat[42].d_acts[A_LOD] = Wrapper_DIG_SCOPE_211_Load;
	DevDat[42].d_acts[A_OPN] = Wrapper_DIG_SCOPE_211_Open;
	DevDat[42].d_acts[A_RST] = Wrapper_DIG_SCOPE_211_Reset;
	DevDat[42].d_acts[A_FNC] = Wrapper_DIG_SCOPE_211_Setup;
	DevDat[42].d_acts[A_STA] = Wrapper_DIG_SCOPE_211_Status;
//
//	DIG_SCOPE:CH212
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[43].d_modlst = p_mod;
	DevDat[43].d_fncP = 212;
	DevDat[43].d_acts[A_CLS] = Wrapper_DIG_SCOPE_212_Close;
	DevDat[43].d_acts[A_CON] = Wrapper_DIG_SCOPE_212_Connect;
	DevDat[43].d_acts[A_DIS] = Wrapper_DIG_SCOPE_212_DisConnect;
	DevDat[43].d_acts[A_FTH] = Wrapper_DIG_SCOPE_212_Fetch;
	DevDat[43].d_acts[A_INX] = Wrapper_DIG_SCOPE_212_Init;
	DevDat[43].d_acts[A_LOD] = Wrapper_DIG_SCOPE_212_Load;
	DevDat[43].d_acts[A_OPN] = Wrapper_DIG_SCOPE_212_Open;
	DevDat[43].d_acts[A_RST] = Wrapper_DIG_SCOPE_212_Reset;
	DevDat[43].d_acts[A_FNC] = Wrapper_DIG_SCOPE_212_Setup;
	DevDat[43].d_acts[A_STA] = Wrapper_DIG_SCOPE_212_Status;
//
//	DIG_SCOPE:CH213
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[44].d_modlst = p_mod;
	DevDat[44].d_fncP = 213;
	DevDat[44].d_acts[A_CLS] = Wrapper_DIG_SCOPE_213_Close;
	DevDat[44].d_acts[A_CON] = Wrapper_DIG_SCOPE_213_Connect;
	DevDat[44].d_acts[A_DIS] = Wrapper_DIG_SCOPE_213_DisConnect;
	DevDat[44].d_acts[A_FTH] = Wrapper_DIG_SCOPE_213_Fetch;
	DevDat[44].d_acts[A_INX] = Wrapper_DIG_SCOPE_213_Init;
	DevDat[44].d_acts[A_LOD] = Wrapper_DIG_SCOPE_213_Load;
	DevDat[44].d_acts[A_OPN] = Wrapper_DIG_SCOPE_213_Open;
	DevDat[44].d_acts[A_RST] = Wrapper_DIG_SCOPE_213_Reset;
	DevDat[44].d_acts[A_FNC] = Wrapper_DIG_SCOPE_213_Setup;
	DevDat[44].d_acts[A_STA] = Wrapper_DIG_SCOPE_213_Status;
//
//	DIG_SCOPE:CH214
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[45].d_modlst = p_mod;
	DevDat[45].d_fncP = 214;
	DevDat[45].d_acts[A_CLS] = Wrapper_DIG_SCOPE_214_Close;
	DevDat[45].d_acts[A_CON] = Wrapper_DIG_SCOPE_214_Connect;
	DevDat[45].d_acts[A_DIS] = Wrapper_DIG_SCOPE_214_DisConnect;
	DevDat[45].d_acts[A_FTH] = Wrapper_DIG_SCOPE_214_Fetch;
	DevDat[45].d_acts[A_INX] = Wrapper_DIG_SCOPE_214_Init;
	DevDat[45].d_acts[A_LOD] = Wrapper_DIG_SCOPE_214_Load;
	DevDat[45].d_acts[A_OPN] = Wrapper_DIG_SCOPE_214_Open;
	DevDat[45].d_acts[A_RST] = Wrapper_DIG_SCOPE_214_Reset;
	DevDat[45].d_acts[A_FNC] = Wrapper_DIG_SCOPE_214_Setup;
	DevDat[45].d_acts[A_STA] = Wrapper_DIG_SCOPE_214_Status;
//
//	DIG_SCOPE:CH215
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[46].d_modlst = p_mod;
	DevDat[46].d_fncP = 215;
	DevDat[46].d_acts[A_CLS] = Wrapper_DIG_SCOPE_215_Close;
	DevDat[46].d_acts[A_CON] = Wrapper_DIG_SCOPE_215_Connect;
	DevDat[46].d_acts[A_DIS] = Wrapper_DIG_SCOPE_215_DisConnect;
	DevDat[46].d_acts[A_FTH] = Wrapper_DIG_SCOPE_215_Fetch;
	DevDat[46].d_acts[A_INX] = Wrapper_DIG_SCOPE_215_Init;
	DevDat[46].d_acts[A_LOD] = Wrapper_DIG_SCOPE_215_Load;
	DevDat[46].d_acts[A_OPN] = Wrapper_DIG_SCOPE_215_Open;
	DevDat[46].d_acts[A_RST] = Wrapper_DIG_SCOPE_215_Reset;
	DevDat[46].d_acts[A_FNC] = Wrapper_DIG_SCOPE_215_Setup;
	DevDat[46].d_acts[A_STA] = Wrapper_DIG_SCOPE_215_Status;
//
//	DIG_SCOPE:CH216
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[47].d_modlst = p_mod;
	DevDat[47].d_fncP = 216;
	DevDat[47].d_acts[A_CLS] = Wrapper_DIG_SCOPE_216_Close;
	DevDat[47].d_acts[A_CON] = Wrapper_DIG_SCOPE_216_Connect;
	DevDat[47].d_acts[A_DIS] = Wrapper_DIG_SCOPE_216_DisConnect;
	DevDat[47].d_acts[A_FTH] = Wrapper_DIG_SCOPE_216_Fetch;
	DevDat[47].d_acts[A_INX] = Wrapper_DIG_SCOPE_216_Init;
	DevDat[47].d_acts[A_LOD] = Wrapper_DIG_SCOPE_216_Load;
	DevDat[47].d_acts[A_OPN] = Wrapper_DIG_SCOPE_216_Open;
	DevDat[47].d_acts[A_RST] = Wrapper_DIG_SCOPE_216_Reset;
	DevDat[47].d_acts[A_FNC] = Wrapper_DIG_SCOPE_216_Setup;
	DevDat[47].d_acts[A_STA] = Wrapper_DIG_SCOPE_216_Status;
//
//	DIG_SCOPE:CH217
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_FXDN);  // flux-dens
	p_mod = BldModDat (p_mod, (short) M_FMSR);  // fm-source
	p_mod = BldModDat (p_mod, (short) M_HV09);  // harm-9-voltage
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RPLE);  // reply-eff
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[48].d_modlst = p_mod;
	DevDat[48].d_fncP = 217;
	DevDat[48].d_acts[A_CLS] = Wrapper_DIG_SCOPE_217_Close;
	DevDat[48].d_acts[A_CON] = Wrapper_DIG_SCOPE_217_Connect;
	DevDat[48].d_acts[A_DIS] = Wrapper_DIG_SCOPE_217_DisConnect;
	DevDat[48].d_acts[A_FTH] = Wrapper_DIG_SCOPE_217_Fetch;
	DevDat[48].d_acts[A_INX] = Wrapper_DIG_SCOPE_217_Init;
	DevDat[48].d_acts[A_LOD] = Wrapper_DIG_SCOPE_217_Load;
	DevDat[48].d_acts[A_OPN] = Wrapper_DIG_SCOPE_217_Open;
	DevDat[48].d_acts[A_RST] = Wrapper_DIG_SCOPE_217_Reset;
	DevDat[48].d_acts[A_FNC] = Wrapper_DIG_SCOPE_217_Setup;
	DevDat[48].d_acts[A_STA] = Wrapper_DIG_SCOPE_217_Status;
//
//	DIG_SCOPE:CH218
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[49].d_modlst = p_mod;
	DevDat[49].d_fncP = 218;
	DevDat[49].d_acts[A_CLS] = Wrapper_DIG_SCOPE_218_Close;
	DevDat[49].d_acts[A_CON] = Wrapper_DIG_SCOPE_218_Connect;
	DevDat[49].d_acts[A_DIS] = Wrapper_DIG_SCOPE_218_DisConnect;
	DevDat[49].d_acts[A_FTH] = Wrapper_DIG_SCOPE_218_Fetch;
	DevDat[49].d_acts[A_INX] = Wrapper_DIG_SCOPE_218_Init;
	DevDat[49].d_acts[A_LOD] = Wrapper_DIG_SCOPE_218_Load;
	DevDat[49].d_acts[A_OPN] = Wrapper_DIG_SCOPE_218_Open;
	DevDat[49].d_acts[A_RST] = Wrapper_DIG_SCOPE_218_Reset;
	DevDat[49].d_acts[A_FNC] = Wrapper_DIG_SCOPE_218_Setup;
	DevDat[49].d_acts[A_STA] = Wrapper_DIG_SCOPE_218_Status;
//
//	DIG_SCOPE:CH219
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_DSTZ);  // distance-z
	p_mod = BldModDat (p_mod, (short) M_DIVS);  // divisions
	p_mod = BldModDat (p_mod, (short) M_HPX1);  // harm-001-power
	p_mod = BldModDat (p_mod, (short) M_HPA5);  // harm-015-power
	p_mod = BldModDat (p_mod, (short) M_HVZ6);  // harm-06-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX2);  // harm-002-voltage
	p_mod = BldModDat (p_mod, (short) M_HVX8);  // harm-008-voltage
	p_mod = BldModDat (p_mod, (short) M_HVA3);  // harm-013-voltage
	p_mod = BldModDat (p_mod, (short) M_MAXT);  // max-time
	p_mod = BldModDat (p_mod, (short) M_MODD);  // mod-dist
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RADL);  // radial
	p_mod = BldModDat (p_mod, (short) M_RADR);  // radial-rate
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RESB);  // resolution-bandwidth
	p_mod = BldModDat (p_mod, (short) M_RESP);  // resp
	p_mod = BldModDat (p_mod, (short) M_RSPH);  // resp-hiz
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[50].d_modlst = p_mod;
	DevDat[50].d_fncP = 219;
	DevDat[50].d_acts[A_CLS] = Wrapper_DIG_SCOPE_219_Close;
	DevDat[50].d_acts[A_CON] = Wrapper_DIG_SCOPE_219_Connect;
	DevDat[50].d_acts[A_DIS] = Wrapper_DIG_SCOPE_219_DisConnect;
	DevDat[50].d_acts[A_FTH] = Wrapper_DIG_SCOPE_219_Fetch;
	DevDat[50].d_acts[A_INX] = Wrapper_DIG_SCOPE_219_Init;
	DevDat[50].d_acts[A_LOD] = Wrapper_DIG_SCOPE_219_Load;
	DevDat[50].d_acts[A_OPN] = Wrapper_DIG_SCOPE_219_Open;
	DevDat[50].d_acts[A_RST] = Wrapper_DIG_SCOPE_219_Reset;
	DevDat[50].d_acts[A_FNC] = Wrapper_DIG_SCOPE_219_Setup;
	DevDat[50].d_acts[A_STA] = Wrapper_DIG_SCOPE_219_Status;
//
//	DIG_SCOPE:CH220
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CURQ);  // current-quies
	p_mod = BldModDat (p_mod, (short) M_LUMT);  // lum-temp
	p_mod = BldModDat (p_mod, (short) M_MSNR);  // minimum-sense-rate
	p_mod = BldModDat (p_mod, (short) M_MODO);  // mod-offset
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_RSPO);  // resp-one
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[51].d_modlst = p_mod;
	DevDat[51].d_fncP = 220;
	DevDat[51].d_acts[A_CLS] = Wrapper_DIG_SCOPE_220_Close;
	DevDat[51].d_acts[A_CON] = Wrapper_DIG_SCOPE_220_Connect;
	DevDat[51].d_acts[A_DIS] = Wrapper_DIG_SCOPE_220_DisConnect;
	DevDat[51].d_acts[A_FTH] = Wrapper_DIG_SCOPE_220_Fetch;
	DevDat[51].d_acts[A_INX] = Wrapper_DIG_SCOPE_220_Init;
	DevDat[51].d_acts[A_LOD] = Wrapper_DIG_SCOPE_220_Load;
	DevDat[51].d_acts[A_OPN] = Wrapper_DIG_SCOPE_220_Open;
	DevDat[51].d_acts[A_RST] = Wrapper_DIG_SCOPE_220_Reset;
	DevDat[51].d_acts[A_FNC] = Wrapper_DIG_SCOPE_220_Setup;
	DevDat[51].d_acts[A_STA] = Wrapper_DIG_SCOPE_220_Status;
//
//	DIG_SCOPE:CH221
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[52].d_modlst = p_mod;
	DevDat[52].d_fncP = 221;
	DevDat[52].d_acts[A_CLS] = Wrapper_DIG_SCOPE_221_Close;
	DevDat[52].d_acts[A_CON] = Wrapper_DIG_SCOPE_221_Connect;
	DevDat[52].d_acts[A_DIS] = Wrapper_DIG_SCOPE_221_DisConnect;
	DevDat[52].d_acts[A_FTH] = Wrapper_DIG_SCOPE_221_Fetch;
	DevDat[52].d_acts[A_INX] = Wrapper_DIG_SCOPE_221_Init;
	DevDat[52].d_acts[A_LOD] = Wrapper_DIG_SCOPE_221_Load;
	DevDat[52].d_acts[A_OPN] = Wrapper_DIG_SCOPE_221_Open;
	DevDat[52].d_acts[A_RST] = Wrapper_DIG_SCOPE_221_Reset;
	DevDat[52].d_acts[A_FNC] = Wrapper_DIG_SCOPE_221_Setup;
	DevDat[52].d_acts[A_STA] = Wrapper_DIG_SCOPE_221_Status;
//
//	DIG_SCOPE:CH222
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TGTD);  // target-range
	p_mod = BldModDat (p_mod, (short) M_TRIG);  // trig
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[53].d_modlst = p_mod;
	DevDat[53].d_fncP = 222;
	DevDat[53].d_acts[A_CLS] = Wrapper_DIG_SCOPE_222_Close;
	DevDat[53].d_acts[A_CON] = Wrapper_DIG_SCOPE_222_Connect;
	DevDat[53].d_acts[A_DIS] = Wrapper_DIG_SCOPE_222_DisConnect;
	DevDat[53].d_acts[A_FTH] = Wrapper_DIG_SCOPE_222_Fetch;
	DevDat[53].d_acts[A_INX] = Wrapper_DIG_SCOPE_222_Init;
	DevDat[53].d_acts[A_LOD] = Wrapper_DIG_SCOPE_222_Load;
	DevDat[53].d_acts[A_OPN] = Wrapper_DIG_SCOPE_222_Open;
	DevDat[53].d_acts[A_RST] = Wrapper_DIG_SCOPE_222_Reset;
	DevDat[53].d_acts[A_FNC] = Wrapper_DIG_SCOPE_222_Setup;
	DevDat[53].d_acts[A_STA] = Wrapper_DIG_SCOPE_222_Status;
	//
//	DSO_1:CH223
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[56].d_modlst = p_mod;
	DevDat[56].d_fncP = 223;
	DevDat[56].d_acts[A_CLS] = Wrapper_DIG_SCOPE_223_Close;
	DevDat[56].d_acts[A_CON] = Wrapper_DIG_SCOPE_223_Connect;
	DevDat[56].d_acts[A_DIS] = Wrapper_DIG_SCOPE_223_Disconnect;
	DevDat[56].d_acts[A_FTH] = Wrapper_DIG_SCOPE_223_Fetch;
	DevDat[56].d_acts[A_INX] = Wrapper_DIG_SCOPE_223_Init;
	DevDat[56].d_acts[A_LOD] = Wrapper_DIG_SCOPE_223_Load;
	DevDat[56].d_acts[A_OPN] = Wrapper_DIG_SCOPE_223_Open;
	DevDat[56].d_acts[A_RST] = Wrapper_DIG_SCOPE_223_Reset;
	DevDat[56].d_acts[A_FNC] = Wrapper_DIG_SCOPE_223_Setup;
	DevDat[56].d_acts[A_STA] = Wrapper_DIG_SCOPE_223_Status;
//
//	DSO_1:CH224
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CPLG);  // coupling
	p_mod = BldModDat (p_mod, (short) M_DCOF);  // dc-offset
	p_mod = BldModDat (p_mod, (short) M_FREQ);  // freq
	p_mod = BldModDat (p_mod, (short) M_FRQW);  // freq-window
	p_mod = BldModDat (p_mod, (short) M_PERI);  // period
	p_mod = BldModDat (p_mod, (short) M_SCNT);  // sample-count
	p_mod = BldModDat (p_mod, (short) M_TIMP);  // test-equip-imp
	p_mod = BldModDat (p_mod, (short) M_TRLV);  // trig-level
	p_mod = BldModDat (p_mod, (short) M_TRSL);  // trig-slope
	p_mod = BldModDat (p_mod, (short) M_VOLT);  // voltage
	p_mod = BldModDat (p_mod, (short) M_VLAV);  // av-voltage
	p_mod = BldModDat (p_mod, (short) M_VLPK);  // voltage-p
	p_mod = BldModDat (p_mod, (short) M_VLPP);  // voltage-pp
	p_mod = BldModDat (p_mod, (short) M_SBEV);  // strobe-to-event
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[57].d_modlst = p_mod;
	DevDat[57].d_fncP = 224;
	DevDat[57].d_acts[A_CLS] = Wrapper_DIG_SCOPE_224_Close;
	DevDat[57].d_acts[A_CON] = Wrapper_DIG_SCOPE_224_Connect;
	DevDat[57].d_acts[A_DIS] = Wrapper_DIG_SCOPE_224_Disconnect;
	DevDat[57].d_acts[A_FTH] = Wrapper_DIG_SCOPE_224_Fetch;
	DevDat[57].d_acts[A_INX] = Wrapper_DIG_SCOPE_224_Init;
	DevDat[57].d_acts[A_LOD] = Wrapper_DIG_SCOPE_224_Load;
	DevDat[57].d_acts[A_OPN] = Wrapper_DIG_SCOPE_224_Open;
	DevDat[57].d_acts[A_RST] = Wrapper_DIG_SCOPE_224_Reset;
	DevDat[57].d_acts[A_FNC] = Wrapper_DIG_SCOPE_224_Setup;
	DevDat[57].d_acts[A_STA] = Wrapper_DIG_SCOPE_224_Status;
//
//	DIG_SCOPE:CH230
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CHRM);  // car-harmonics
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HP04);  // harm-4-power
	p_mod = BldModDat (p_mod, (short) M_MDPN);  // mod-pneg
	p_mod = BldModDat (p_mod, (short) M_MDPP);  // mod-ppos
	p_mod = BldModDat (p_mod, (short) M_MDSC);  // mod-source
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[54].d_modlst = p_mod;
	DevDat[54].d_fncP = 230;
	DevDat[54].d_acts[A_CLS] = Wrapper_DIG_SCOPE_230_Close;
	DevDat[54].d_acts[A_CON] = Wrapper_DIG_SCOPE_230_Connect;
	DevDat[54].d_acts[A_DIS] = Wrapper_DIG_SCOPE_230_DisConnect;
	DevDat[54].d_acts[A_FTH] = Wrapper_DIG_SCOPE_230_Fetch;
	DevDat[54].d_acts[A_INX] = Wrapper_DIG_SCOPE_230_Init;
	DevDat[54].d_acts[A_LOD] = Wrapper_DIG_SCOPE_230_Load;
	DevDat[54].d_acts[A_OPN] = Wrapper_DIG_SCOPE_230_Open;
	DevDat[54].d_acts[A_RST] = Wrapper_DIG_SCOPE_230_Reset;
	DevDat[54].d_acts[A_FNC] = Wrapper_DIG_SCOPE_230_Setup;
	DevDat[54].d_acts[A_STA] = Wrapper_DIG_SCOPE_230_Status;
//
//	DIG_SCOPE:CH231
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_CHRM);  // car-harmonics
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HP02);  // harm-2-power
	p_mod = BldModDat (p_mod, (short) M_HP04);  // harm-4-power
	p_mod = BldModDat (p_mod, (short) M_MDSC);  // mod-source
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[55].d_modlst = p_mod;
	DevDat[55].d_fncP = 231;
	DevDat[55].d_acts[A_CLS] = Wrapper_DIG_SCOPE_231_Close;
	DevDat[55].d_acts[A_CON] = Wrapper_DIG_SCOPE_231_Connect;
	DevDat[55].d_acts[A_DIS] = Wrapper_DIG_SCOPE_231_DisConnect;
	DevDat[55].d_acts[A_FTH] = Wrapper_DIG_SCOPE_231_Fetch;
	DevDat[55].d_acts[A_INX] = Wrapper_DIG_SCOPE_231_Init;
	DevDat[55].d_acts[A_LOD] = Wrapper_DIG_SCOPE_231_Load;
	DevDat[55].d_acts[A_OPN] = Wrapper_DIG_SCOPE_231_Open;
	DevDat[55].d_acts[A_RST] = Wrapper_DIG_SCOPE_231_Reset;
	DevDat[55].d_acts[A_FNC] = Wrapper_DIG_SCOPE_231_Setup;
	DevDat[55].d_acts[A_STA] = Wrapper_DIG_SCOPE_231_Status;
//
//	DIG_SCOPE:CH232
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ADTO);  // add-to
	p_mod = BldModDat (p_mod, (short) M_CHRM);  // car-harmonics
	p_mod = BldModDat (p_mod, (short) M_CPHS);  // car-phase
	p_mod = BldModDat (p_mod, (short) M_CRSD);  // car-resid
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_HP04);  // harm-4-power
	p_mod = BldModDat (p_mod, (short) M_MDSC);  // mod-source
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[56].d_modlst = p_mod;
	DevDat[56].d_fncP = 232;
	DevDat[56].d_acts[A_CLS] = Wrapper_DIG_SCOPE_232_Close;
	DevDat[56].d_acts[A_CON] = Wrapper_DIG_SCOPE_232_Connect;
	DevDat[56].d_acts[A_DIS] = Wrapper_DIG_SCOPE_232_DisConnect;
	DevDat[56].d_acts[A_FTH] = Wrapper_DIG_SCOPE_232_Fetch;
	DevDat[56].d_acts[A_INX] = Wrapper_DIG_SCOPE_232_Init;
	DevDat[56].d_acts[A_LOD] = Wrapper_DIG_SCOPE_232_Load;
	DevDat[56].d_acts[A_OPN] = Wrapper_DIG_SCOPE_232_Open;
	DevDat[56].d_acts[A_RST] = Wrapper_DIG_SCOPE_232_Reset;
	DevDat[56].d_acts[A_FNC] = Wrapper_DIG_SCOPE_232_Setup;
	DevDat[56].d_acts[A_STA] = Wrapper_DIG_SCOPE_232_Status;
//
//	DIG_SCOPE:CH233
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_ACCD);  // acceptance-code
	p_mod = BldModDat (p_mod, (short) M_ACMK);  // acceptance-mask
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[57].d_modlst = p_mod;
	DevDat[57].d_fncP = 233;
	DevDat[57].d_acts[A_CLS] = Wrapper_DIG_SCOPE_233_Close;
	DevDat[57].d_acts[A_CON] = Wrapper_DIG_SCOPE_233_Connect;
	DevDat[57].d_acts[A_DIS] = Wrapper_DIG_SCOPE_233_DisConnect;
	DevDat[57].d_acts[A_FTH] = Wrapper_DIG_SCOPE_233_Fetch;
	DevDat[57].d_acts[A_INX] = Wrapper_DIG_SCOPE_233_Init;
	DevDat[57].d_acts[A_LOD] = Wrapper_DIG_SCOPE_233_Load;
	DevDat[57].d_acts[A_OPN] = Wrapper_DIG_SCOPE_233_Open;
	DevDat[57].d_acts[A_RST] = Wrapper_DIG_SCOPE_233_Reset;
	DevDat[57].d_acts[A_FNC] = Wrapper_DIG_SCOPE_233_Setup;
	DevDat[57].d_acts[A_STA] = Wrapper_DIG_SCOPE_233_Status;
//
//	DIG_SCOPE:CH234
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_PLAN);  // pla
	p_mod = BldModDat (p_mod, (short) M_PLAR);  // pla-rate
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[58].d_modlst = p_mod;
	DevDat[58].d_fncP = 234;
	DevDat[58].d_acts[A_CLS] = Wrapper_DIG_SCOPE_234_Close;
	DevDat[58].d_acts[A_CON] = Wrapper_DIG_SCOPE_234_Connect;
	DevDat[58].d_acts[A_DIS] = Wrapper_DIG_SCOPE_234_DisConnect;
	DevDat[58].d_acts[A_FTH] = Wrapper_DIG_SCOPE_234_Fetch;
	DevDat[58].d_acts[A_INX] = Wrapper_DIG_SCOPE_234_Init;
	DevDat[58].d_acts[A_LOD] = Wrapper_DIG_SCOPE_234_Load;
	DevDat[58].d_acts[A_OPN] = Wrapper_DIG_SCOPE_234_Open;
	DevDat[58].d_acts[A_RST] = Wrapper_DIG_SCOPE_234_Reset;
	DevDat[58].d_acts[A_FNC] = Wrapper_DIG_SCOPE_234_Setup;
	DevDat[58].d_acts[A_STA] = Wrapper_DIG_SCOPE_234_Status;
//
//	DIG_SCOPE:CH235
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_HPX2);  // harm-002-power
	p_mod = BldModDat (p_mod, (short) M_HPX3);  // harm-003-power
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[59].d_modlst = p_mod;
	DevDat[59].d_fncP = 235;
	DevDat[59].d_acts[A_CLS] = Wrapper_DIG_SCOPE_235_Close;
	DevDat[59].d_acts[A_CON] = Wrapper_DIG_SCOPE_235_Connect;
	DevDat[59].d_acts[A_DIS] = Wrapper_DIG_SCOPE_235_DisConnect;
	DevDat[59].d_acts[A_FTH] = Wrapper_DIG_SCOPE_235_Fetch;
	DevDat[59].d_acts[A_INX] = Wrapper_DIG_SCOPE_235_Init;
	DevDat[59].d_acts[A_LOD] = Wrapper_DIG_SCOPE_235_Load;
	DevDat[59].d_acts[A_OPN] = Wrapper_DIG_SCOPE_235_Open;
	DevDat[59].d_acts[A_RST] = Wrapper_DIG_SCOPE_235_Reset;
	DevDat[59].d_acts[A_FNC] = Wrapper_DIG_SCOPE_235_Setup;
	DevDat[59].d_acts[A_STA] = Wrapper_DIG_SCOPE_235_Status;
//
//	DIG_SCOPE:CH236
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_DEEM);  // de-emphasis
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[60].d_modlst = p_mod;
	DevDat[60].d_fncP = 236;
	DevDat[60].d_acts[A_CLS] = Wrapper_DIG_SCOPE_236_Close;
	DevDat[60].d_acts[A_CON] = Wrapper_DIG_SCOPE_236_Connect;
	DevDat[60].d_acts[A_DIS] = Wrapper_DIG_SCOPE_236_DisConnect;
	DevDat[60].d_acts[A_FTH] = Wrapper_DIG_SCOPE_236_Fetch;
	DevDat[60].d_acts[A_INX] = Wrapper_DIG_SCOPE_236_Init;
	DevDat[60].d_acts[A_LOD] = Wrapper_DIG_SCOPE_236_Load;
	DevDat[60].d_acts[A_OPN] = Wrapper_DIG_SCOPE_236_Open;
	DevDat[60].d_acts[A_RST] = Wrapper_DIG_SCOPE_236_Reset;
	DevDat[60].d_acts[A_FNC] = Wrapper_DIG_SCOPE_236_Setup;
	DevDat[60].d_acts[A_STA] = Wrapper_DIG_SCOPE_236_Status;
//
//	DIG_SCOPE:CH237
//
	p_mod = (MODDAT *) 0;
	p_mod = BldModDat (p_mod, (short) M_COMD);  // command
	p_mod = BldModDat (p_mod, (short) M_CWLV);  // cw-level
	p_mod = BldModDat (p_mod, (short) M_HARP);  // harm-phase
	p_mod = BldModDat (p_mod, (short) M_HP12);  // harm-12-power
	p_mod = BldModDat (p_mod, (short) M_PROA);  // press-osc-ampl
	p_mod = BldModDat (p_mod, (short) M_RELW);  // relative-wind
	p_mod = BldModDat (p_mod, (short) M_TRES);  // temp-coeff-res
	p_mod = BldModDat (p_mod, (short) M_PATH);  // path
	DevDat[61].d_modlst = p_mod;
	DevDat[61].d_fncP = 237;
	DevDat[61].d_acts[A_CLS] = Wrapper_DIG_SCOPE_237_Close;
	DevDat[61].d_acts[A_CON] = Wrapper_DIG_SCOPE_237_Connect;
	DevDat[61].d_acts[A_DIS] = Wrapper_DIG_SCOPE_237_DisConnect;
	DevDat[61].d_acts[A_FTH] = Wrapper_DIG_SCOPE_237_Fetch;
	DevDat[61].d_acts[A_INX] = Wrapper_DIG_SCOPE_237_Init;
	DevDat[61].d_acts[A_LOD] = Wrapper_DIG_SCOPE_237_Load;
	DevDat[61].d_acts[A_OPN] = Wrapper_DIG_SCOPE_237_Open;
	DevDat[61].d_acts[A_RST] = Wrapper_DIG_SCOPE_237_Reset;
	DevDat[61].d_acts[A_FNC] = Wrapper_DIG_SCOPE_237_Setup;
	DevDat[61].d_acts[A_STA] = Wrapper_DIG_SCOPE_237_Status;
	return 0;
}
