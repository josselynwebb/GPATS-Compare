\\**************************************************************************
\\
\\  Filename: lexcon.h
\\  Version:  2.0
\\  Purpose:  A source file used to build the TYX PAWS ATLAS lexical
\\            database file LexDB.LEX.
\\
\\ VER   DATE     ENGINEER, ORGANIZATION, DR# (STR #): DESCRIPTION
\\ ---   -------  --------------------------------------------------------------
\\ 1.3   01Nov00  LMIS:
\\                Update for Electro-Optical additions.
\\
\\ 2.0   28Apr09 EADS:
\\                 Baselined VIPER/T version number per DME PCR VSYS-450
\\                 based on USMC comment
\\
\\**************************************************************************
\\
 constant;
	VERBRULE        ==      012;
\\        NOUNRULE        ==      3;
\\        MODRULE         ==      051;
	DIMRULE         ==      071;
	PORTRULE        ==      051;
	PINRULE         ==      051;
	MODULERULE      ==      1;
	UUTPINRULE==071;        \ DASHES+DIGITS+SLASHES uut pins as opposed to atlas pin descriptors

	OLD == 0;
	NEW == 1;

	DIMTYPE==0;
	MODTYPE==1;
	NOUNTYPE==2;
	VERBTYPE==3; 
	PINTYPE==4;

	ALIAS_L == 16;         \Must be the same as ALIAS in paledefs_h

	MAXNOUNS == 120;        \\ InCreased From 80 for Grumman ATLAS
	MAXMOD == 2048;         \\ Not used but for KNMATRIXSZ

\\
\\      Token Classes
\\
	DIG_NC  ==      #100;           \\ Digital      <Noun>
	GND_NC  ==      #200;           \\ Ground       <Noun>
	PLS_NC  ==      #400;           \\ Pulse Train  <Noun>
	NCX_NC  ==      #800;           \\ No CNX       <Noun>
	NUS_NC  ==      #1000;          \\ Optional USING (626)
	CFN_NC  ==      #2000;          \\ COMPLEX FUNCTION (CASS)

	DSC_MC  ==      64;             \\ Descriptor               <Modifier>
	NTC_MC  ==      32;             \\ Not Compiler             <Modifier>
	NDM_MC  ==      32;             \\ Nul DIM allowed (CASS)
	LST_MC  ==      16;             \\ Allow List (ListRange)   <Modifier>
	YYY_MC  ==      8;              \\ Various Local Usages     <Modifier>
	XXX_MC  ==      4;              \\ Various Local Usages     <Modifier>

	XDT_MC  ==      2;              \\ DIGITAL Data Type        <Modifier>
	IDT_MC  ==      1;              \\ INTEGER Date Type        <Modifier>
	ADT_MC  ==      3;              \\ All     Data Type        <Modifier>
	DDT_MC  ==      0;              \\ DECIMAL Data Type        <Modifier>

	DOD_MC  ==      XXX_MC;         \\ DO DIGITAL TEST          <Modifier>
	BDT_MC  ==      XXX_MC;         \\ BOOLEAN Data Type (1989) <Modifier>       

	SUB_MC  ==      YYY_MC;         \\ Mod Sub Table (CASS VIDEO SIGNAL, NoMore)
	MSG_MC  ==      YYY_MC;         \\ MSGCHAR Data Type (NESECSD, CASS)

        MODMASK         ==      63;

	NSD_DC  ==      2;              \\ Non Standard Dimension   <Dimension>

	EVT_PC  ==      #100;           \\ EVENT Port
	PSD_PC  ==      #200;           \\ Pseudo Port (IFTE: CARD, BUS etc)

\\
\\ Verb Classes
\\
	PRE_VC          ==      #800;
	FLOW_VC         ==      #400;
	S_VC            ==      #200;
	SIG_VC          ==      #100;
	SENT_VC         ==      #1000;
	SBDY_VC         ==      #2000;          \\ UNUSED CONFLICT SMA_VC
	SEX_VC          ==      #3000;          \\ UNUSED CONFLICT SENT_VC, SBDY_VC, etc
	SSA_VC          ==      #1000;
	SMA_VC          ==      #2000;
	SMAS_VC         ==      SMA_VC ++ #4000;
	PCMP_VC         ==      #3000;
	DSP_VC          ==      #1000;
	CUC_VC          ==      #8000;

\\      Verb Codes

	STW_VX          ==      1;
	SPW_VX          ==      2;
	SYN_VX          ==      3;
	DO_VX           ==      4;
	TRM_VX          ==      5;
	SET_VX          ==      6;
	BGN_VX          ==      7;
	IF_VX           ==      8;
	END_VX          ==      9;
	LVE_VX          ==      10;
	RES_VX          ==      11;
	RD_VX           ==      12; 
	REQ_VX          ==      13;
	REM_VX          ==      14;
	DSP_VX          ==      15;
	IND_VX          ==      16;
	RCD_VX          ==      17;
	PRN_VX          ==      18;
	DEF_VX          ==      19;
	VER_VX          ==      20; 
	MON_VX          ==      21;
	UUT_VX          ==      22;
	CLS_VX          ==      23;
	OPN_VX          ==      24;
	APP_VX          ==      25;
	CAL_VX          ==      26;
	CMP_VX          ==      27;
	CON_VX          ==      28;
	CPL_VX          ==      29;
	DLY_VX          ==      30;
	DCL_VX          ==      31;
	ELS_VX          ==      32;
	FTH_VX          ==      33;
	FIL_VX          ==      34;
	FIN_VX          ==      35;
	FOR_VX          ==      36;
	INT_VX          ==      37;
	MEA_VX          ==      38;
	PFM_VX          ==      39;
	WHL_VX          ==      40;
	DIS_VX          ==      41;
	UCP_VX          ==      42;
	WTF_VX          ==      43;
	ENB_VX          ==      44;
	DSB_VX          ==      45;
	CTW_VX          ==      46;
	GWN_VX          ==      47;
	INC_VX          ==      48;
	INP_VX          ==      49;
	OUT_VX          ==      50;
	EXA_VX          ==      51;
	SRT_VX          ==      52;
	STP_VX          ==      53;
	GTO_VX          ==      54;
	SAV_VX          ==      55;
	IDN_VX          ==      56;
	PRP_VX          ==      57;     \\ PREPARE      (416, HTS, 616)
	EXC_VX          ==      58;     \\ EXECUTE      (416, HTS)
	MDE_VX          ==      59;     \\ MODE         (HTS)
	LKE_VX          ==      60;     \\ LINK ENTRY   (HTS)
	SYC_VX          ==      61;     \\ SYNC         (616)
	STL_VX          ==      62;     \\ STIMULATE    (616)
	SNS_VX          ==      63;     \\ SENSE        (616)
	PRV_VX          ==      64;     \\ PROVE        (616)
	SUS_VX          ==      65;     \\ SUSPEND      (616)
	CNT_VX          ==      66;     \\ CONTINUE     (616) + ADTS
	REL_VX          ==      67;     \\ RELEASE      (616)
	RQT_VX          ==      68;     \\ REQUEST      (616)
	ADJ_VX          ==      69;     \\ ADJUST       (616)
	TMX_VX          ==      70;     \\ TO MAXIMIZE  (616)
	TMN_VX          ==      71;     \\ TO MINIMIZE  (616)
	TRC_VX          ==      72;     \\ TO REACH     (616)
	UPD_VX          ==      73;     \\ UPDATE       (616)
	SPC_VX          ==      74;     \\ SPECIFY      (616)
	COM_VX          ==      75;     \\ COMMENCE     (616)
	STF_VX          ==      76;     \\ SET          (B1B)
	RST_VX          ==      77;     \\ RESET        (B1B)
	CHG_VX          ==      78;     \\ CHANGE       (HTS) & (IEEE71689)
	ARM_VX          ==      79;     \\ ARM          (IEEE71689)
	XTN_VX          ==      80;     \\ EXTEND       (IEEE71689)
	EST_VX          ==      81;     \\ ESTABLISH    (IEEE71689)
	FLC_VX          ==      82;     \\ FILECLOSE    (RADCOM)
	FLO_VX          ==      83;     \\ FILEOPEN     (RADCOM)
	TTB_VX          ==      84;     \\ TEST BIT     (RADCOM)
	CHN_VX          ==      85;     \\ CHAIN        (RADCOM)
	ASN_VX          ==      86;     \\ ASSIGN       (RADCOM)
	DRW_VX          ==      87;     \\ DRAW         (RADCOM)
	ERA_VX          ==      88;     \\ ERASE        (RADCOM)
	MVG_VX          ==      89;     \\ MOVE         (RADCOM)
	CRE_VX          ==      90;     \\ CREATE       (626)
	DEL_VX          ==      91;     \\ DELETE       (626)
	ELX_VX          ==      92;     \\ ELSE IF      (626)
	POS_VX          ==      93;     \\ POSITION     (MATE)
	OFL_VX          ==      94;     \\ OFFLOAD      (RADATS)
	REN_VX          ==      95;     \\ RENAME       (ADTS)
	LAT_VX          ==      59;     \\ LATCH        (TORNADO)
	UNL_VX          ==      60;     \\ UNLATCH      (TORNADO)
	DCN_VX          ==      90;     \\ DECISION-TABLE(TORNADO)
	PAR_VX          ==      91;     \\ PARTITION    (TORNADO)
	RPT_VX          ==      92;     \\ REPEAT       (TORNADO)


        BGA_VX          ==      254;


        VERBMASK        ==      255;
	NOUNMASK        ==      255;

\\
\\      The following define indices into the 'item' virtual array
\\
\\
\\      Token Tables
\\
	TTAB_V  ==      0;              \\ VERB
	TTAB_N  ==      1;              \\ NOUN
	TTAB_P  ==      2;              \\ PORT
	TTAB_D  ==      3;              \\ Real DIMENSION
	TTAB_MT ==     15;              \\ MODULE_TYPE
	TTAB_PN ==     17;              \\ PSEUDO NOUN


\\
\\      Symbol Tables
\\
        STAB_V  ==      4;              \\ VERB
	STAB_N  ==      5;              \\ NOUN
	STAB_M  ==      6;              \\ MODS
	STAB_P  ==      7;              \\ PORT
	STAB_D  ==      8;              \\ DIMENSION
	STAB_MT ==      9;              \\ MODULE_TYPE VA

\\
\\      CIIL Tables
\\
	CTAB_V  ==      10;             \\ VERB
	CTAB_N  ==      11;             \\ NOUN
	CTAB_P  ==      12;             \\ PORT
	CTAB_D  ==      13;             \\ DIMENSION
	CTAB_M  ==      14;             \\ MODIFIER
	CTAB_MT ==      16;             \\ MODULE_TYPE
	CTAB_DP ==      20;             \\ DIMPF (Dimension Prefix)

\\
\\      Parse Rules
\\
	RULE_NI ==      18;             \\ Noun
	RULE_MI ==      19;             \\ Modifier
	RULE_DI ==      21;             \\ Dimension


\\      Token's that we need to know about

	CMP_VT  ==      24;             \\ COMPARE   <Verb> Token (for PTE)
	VER_VT  ==      25;             \\ VERIFY    <Verb> Token (for PTE)
	MON_VT  ==      26;             \\ MONITOR   <Verb> Token (for PTE)
	DO_VT   ==      27;             \\ DO        <Verb> Token (for PTE)
	TRM_VT  ==      28;             \\ TERMINATE <Verb> Token (for PTE)
	MEA_VT	==	29;		\\ MEASURE   <Verb> Token (for PTE)
	REA_VT	==	30;		\\ READ   	<Verb> Token (for PTE)


	SUBSET          ==  100;        \\ ATLAS Specification
	PAWSIEEE7161989 ==  101;        \\ Recognise 1989 / 626 Subset
	MATESUBSET      ==  1066;       \\ Recognise MATE Subsets
	CASSSUBSET      ==  1741;       \\ Recognise CASS Subsets
	IFTESUBSET      ==  1132;       \\ Recognise IFTE Subsets
	ADTSSUBSET      ==  1666;       \\ Recognise ADTS Subsets
	TORNADOSUBSET   ==  1902;       \\ Recognise TORNADO Subsets


	SUBSETFLAGS         ==  98;     \\ 1989 Useage
	APPLY_CONNECTSETUP  ==  #0001;  \\ APPLY = CONNECT followed by SETUP
	NO_CLOSEOPEN        ==  #0002;  \\ Don NOT generate CLOSE and OPEN


	ADD_BUS_PARAMETER       ==  400;
	ADD_PROTOCOL_PARAMETER  ==  440;
