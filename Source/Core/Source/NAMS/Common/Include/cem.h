/************************************************************************/
/*
 *              TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#ifndef TYX_CEM_CEMHDR_H
#define  TYX_CEM_CEMHDR_H

#define  USERMAKE

/************************************************************************/
/************************************************************************/
/*
 * File cemhdr2.h - MS Windows DLL.
 */

#ifndef CCSNLI
#define  CCSNLI
#endif

#ifndef DOSWINDOW
#define  DOSWINDOW
#endif

#ifndef _PROTO_
#define  _PROTO_
#endif

#include  <windows.h>
#include  <windowsx.h>

/************************************************************************/
/************************************************************************/
/*
 * File cemhdr4.h
 */

#ifdef __cplusplus

#define  CCALLBACK  CALLBACK
#define  DECLAREC   extern "C"
#define  EXTERNC    extern "C"

extern	"C" {

#else	/*** Not __cplusplus ***/

#define  CCALLBACK
#define  DECLAREC
#define  EXTERNC    extern

#endif	/*** Not __cplusplus ***/

/************************************************************************/
/************************************************************************/
/*
 *		TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */

#define  SYSNAME      "Windows NT"
#define  SYSCONFNAME  "Windows NT"
#define  SYSCONFDATE  "20050601"

#define  SYSOSWNT
#define  SYSMAXPATH  250	/* Maximum Size of Pathname (in Bytes)	*/
#define  SYS32BIT
#define  SYSTIGUI

#define	 SYSNT
#define  SYSOSDOS

#define  DOSWINDOW

#define  SYSFPF03		/* Floating-Point Format		*/
#define  SYSLIF02		/* Long Integer Format			*/
#define  SYSSIF02		/* Short Integer Format			*/
#define  BSS			/* Unitialized Data			*/

/************************************************************************/
/***********************************************************************/
/*
 *		TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

/*
 *      !!PR99-013      1990220 VM Pagesize (VABOF)
 */

#ifndef	TYX_PTE_INSTAL_H
#define  TYX_PTE_INSTAL_H

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

#ifndef USERMAKE

#ifdef CCSNLI
#include  <system.h>
#else	/*** Not CCSNLI ***/
#include  "system.h"
#endif	/*** Not CCSNLI ***/

#endif	/*** Not USERMAKE ***/

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

#define  VABOF      12
#define  IBSZ     4096			/* IB Buffer Size		*/
#define  FAST
#define  LALIGN

#undef   SYSTIGUI
#define  SYSTICHR

#define  NBYTPINT  2		/* Number of Bytes per Integer          */
#define  NBYTPLNG  4		/* Number of Bytes per Long             */

#define  PTE_FAR_OBJ
#define  PTE_HUGE_OBJ

#define  VISIBLE
#undef	 RTS_CEM_DLL

#undef   RTS_HUGE_MEM
#undef   RTS_HUGE_MEM_CEM

#define  AILSWX

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

#ifdef  OLD_SYS04C                      /* HP A900 / RTEA               */
#undef  VABOF
#define VABOF 9
#undef  IBSZ
#define IBSZ    250
#undef  FAST
#undef  LALIGN
#define NOSTRUCTINIT
#endif

#ifdef SYS21A				/* 16-Bit -- MS-DOS		*/
#undef  VABOF
#define VABOF 9
#endif

#ifdef  SYS16W				/* 16-Bit -- DOS Windows 3.x	*/
#undef  VABOF
#define VABOF 9
#undef  PTE_FAR_OBJ
#define PTE_FAR_OBJ   far
#undef  PTE_HUGE_OBJ
#define PTE_HUGE_OBJ  huge
#define RTS_CEM_DLL
#endif

#ifdef  SYSOSWNT			/* 32-Bit -- Windows NT		*/
#undef  IBSZ
#define IBSZ    65000                   /* IB Buffer Size               */
#define RTS_CEM_DLL
#endif

#ifdef  SYSOSVMS			/* VAX/VMS			*/
#undef  LALIGN
#endif

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

#ifdef XWINDOW
#define  SYSTIGUI
#undef   SYSTICHR
#endif	/*** XWINDOW ***/

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

#ifdef DOSWINDOW
#define	 SYSTIGUI
#undef   SYSTICHR
#define  CLOSELOGONHALT
#endif	/*** DOSWINDOW ***/

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

#endif	/*** TYX_PTE_INSTAL_H ***/

/************************************************************************/
/***********************************************************************/
/*
 *		TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#ifndef	_TYX_LFDEFS_H_
#define	_TYX_LFDEFS_H_

#endif	/*** _TYX_LFDEFS_H_ ***/

/***********************************************************************/
/************************************************************************/
/*
 *		TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#ifndef _TYX_LF_FUNC_H_
#define _TYX_LF_FUNC_H_

#ifdef _PROTO_

extern	void	LFclos( void );
extern	void	LFcrea( char * );
extern	int	LFinit( void );
extern	void	LFopen( char * );
extern	void	LFput1( int, char * );
extern	void	LFput2( int, char *, char * );
extern	void	LFput3( int, char *, char *, char * );
extern	void	LFput4( int, char *, char *, char *, char * );
extern	void	LFput5( int, char *, char *, char *, char *, char * );
extern	void	LFputs( int, char * );

#else	/*** _PROTO_ ***/

extern	void	LFclos();
extern	void	LFcrea();
extern	int	LFinit();
extern	void	LFopen();
extern	void	LFput1();
extern	void	LFput2();
extern	void	LFput3();
extern	void	LFput4();
extern	void	LFput5();
extern	void	LFputs();

#endif	/*** _PROTO_ ***/

#endif	/*** _TYX_LF_FUNC_H_ ***/

/************************************************************************/
/************************************************************************/
/*
 *		TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#ifndef _TYX_OS_H_
#   define  _TYX_OS_H_

/*
 *  File Descriptor Definitions.
 */
#define  OSFDSI  0		/* Standard Input			*/
#define  OSFDSO  1		/* Standard Output			*/
#define  OSFDSE  2		/* Standard Error			*/
#define  OSFDAU  3		/* Auxiliary I/O			*/
#define  OSFDPR  4		/* Printer Output			*/
/*
 *  Return Status Definitions.
 */
#define  OSRSTIMO  (-2)		/* Timeout				*/
#define  OSRSERR   (-1)		/* Error				*/
#define  OSRSOK      0		/* No errors detected			*/
#define  OSRSEOF     0		/* End-of-File				*/
#define  OSRSYES     1		/* "Yes", "True",  etc.			*/
#define  OSRSNO      2		/* "No",  "False", etc.			*/
/*
 *  File Type Definitions.
 */
#define  OSFTTXT  0000001	/* Text File				*/
#define  OSFTVMS  0000002	/* Virtual Memory File			*/
/*
 *  Creation Mode Definitions.
 */
#define  OSCM444  0444		/* Owner=r--  Group=r--  Other=r--	*/
#define  OSCM664  0664		/* Owner=rw-  Group=rw-  Other=r--	*/
#define  OSCM666  0666		/* Owner=rw-  Group=rw-  Other=rw-	*/
#define  OSCM775  0775		/* Owner=rwx  Group=rwx  Other=r-x	*/
#define  OSCM777  0777		/* Owner=rwx  Group=rwx  Other=rwx	*/
/*
 *  Access Code Definitions.
 */
#define  OSACRO  0		/* Open for Reading Only		*/
#define  OSACWO  1		/* Open for Writing Only		*/
#define  OSACRW  2		/* Open for Reading and Writing		*/
/*
 *  Position Code Definitions.
 */
#define  OSPCBEG  0		/* Beginning of File			*/
#define  OSPCCUR  1		/* Current Position of File		*/
#define  OSPCEND  2		/* End of File				*/
/*
 *  Directory Definitions.
 */
#define  OSDEMAX  14		/* Directory Entry: Max Size (in Bytes)	*/
/*
 *  Memory Allocation Definitions.
 */
#define  OSMCLEAN  1		/* Allocate and clear Memory		*/
#define  OSMDIRTY  2		/* Allocate but do not clear Memory	*/
/*
 *  Exit Status Definitions.
 */
#define  OSESOK   0		/* Good Status				*/
#define  OSESERR  1		/* Bad Status				*/
#define  OSESRUN  2		/* Bad Status: Run Fail			*/
#define  OSESMEM  3		/* Bad Status: Memory Fail		*/
/*
 *  Maximum Integers
 */
#define  OSPMAXINT  2147483647  /* Positive max int                     */
#define  OSNMAXINT -2147483647  /* Negative max int                     */
/*
 *  Bus Error Codes for OSberr.
 */
#define  OSBNORM    0		/* No Error				*/
#define  OSBUNKN  (-1)		/* Unknown Error			*/
#define  OSBTIMO  (-2)		/* Timeout Occurred			*/
/*
 *  Define Flags For OSbwait
 */
#define  OSWSRQ  1
/*
 *  Define Flags for OSbstat().
 */
#define  OSBGSTAT  0		/* Get Status				*/
#define  OSBTSRQ   1		/* Test Status: SRQ			*/
/*
 *  Define Terminal Mode
 */
#define  RAW	 0
#define  COOKED  1
/*
 *  Define Flags For Process Wait
 */
#define  PWAIT	 1

typedef  int   FID;

#ifndef	NULL
#define	NULL	(void *) 0
#endif	/* NULL */

#endif      /* end of _TYX_OS_H_ */

/************************************************************************/
/************************************************************************/
/*
 *              TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/
/*
 *      Actions
 */
#define A_FNC   ((short) 0 )            /* Action : FncDef && LodDef        */
#define A_STA   ((short) 1 )            /* Action : StaDef                  */
#define A_FTH   ((short) 2 )            /* Action : FthDef                  */
#define A_INX   ((short) 3 )            /* Action : InxDef                  */
#define A_RST   ((short) 4 )            /* Action : RstDef                  */
#define A_CLS   ((short) 5 )            /* Action : ClsOpnDef (CLS)         */
#define A_OPN   ((short) 6 )            /* Action : ClsOpnDef (OPN)         */
#define A_CON   ((short) 7 )            /* Action : CnxDef (CON)            */
#define A_DIS   ((short) 8 )            /* Action : CnxDef (DIS)            */
#define A_LOD	((short) 9 )		/* Action : LodDef (LOD)	    */
#define N_ACT            10             /* Number of Handler Functions      */
					/*  per device                      */
/*
 *  Structure containing data values.
 */
typedef struct DTTAG {
	  struct DTTAG
			* dat_nxt;      /* Pointer to next DATUM            */
                 short
			dat_qual,       /* Qualifier : index into kword[]   */
			dat_flg,        /* Flags (see DATFLG_x...x below)   */
			dat_typ;        /* int/decimal/digital/txt/dsc/bln  */
		 unsigned
			dat_siz,        /* size in bytes of digital data    */
			dat_cnt;        /* number of values                 */
		 union  {
			 long     * dat_int;  /* Ptr to array of T_INTV     */
			 double   * dat_dec;  /* Ptr to array of T_DECV     */
			 short    * dat_dsc;  /* Ptr to array of T_DSCV     */
			 char    ** dat_txt;  /* Ptr to array of T_MSGV     */
			 char    ** dat_dig;  /* Ptr to array of T_DIGV     */
			 short    * dat_bln;  /* Ptr to array of T_BLNV	    */
			 char	  * dat_dft;  /* Ptr to default flag	    */
			 char     * dat_ciildig; /* Ptr to generate CIIL    */
			} dat_val;
		 char
		      * dat_unt;        /* Ptr to T_DIMN value              */

			long  dat_fncId;	/*	FNC Identifier	*/
                } DATUM;
/*
 *  DATUM Flags.
 */
#define  DATFLG_USNEW  ((short)0x0001)	/* User Stub New DATUM or Values */
/*
 *  Structure containing modifier information for each qualifier.
 */
typedef struct MDTAG
	{	struct MDTAG  *m_next;		/* Ptr:	Next Modifier	*/
		short	       m_mod;		/* Ix:	Modifier Table	*/
		DATUM	      *m_datum;		/* Ptr:	First DATUM	*/
		struct MDTAG  *m_group;		/* Ptr:	First Group Mod	*/
	} MODDAT;
/*
 *      Handler function pointer type.
 */
#ifndef CCALLBACK
#define  CCALLBACK
#endif	/*** CCALLBACK ***/

#ifdef  _PROTO_
typedef int     (CCALLBACK * ACTFNC)(void);
#else   /**** Not _PROTO_ ****/
typedef int     (CCALLBACK * ACTFNC)();
#endif  /**** Not _PROTO_ ****/

/*
 *      Stucture of each entry in device table.
 */
typedef struct {
                 ACTFNC
			   d_acts[N_ACT];
					 /* Handler Functions                */
		 MODDAT
			*  d_modlst;     /* modifier data                    */
		 char
			 * d_errmsg;     /* Pointer to error message         */
                 short
			   d_fncN,       /* FncDef Noun                      */
			   d_fncM,       /* FncDef Mode(Meas Characteristic) */
			   d_fncP;       /* FncDef Port                      */
		 short
			   d_errno;      /* Error Number.                    */
		 struct {
			 short  a_ctl;	 /* Controller	*/
			 char	a_mta;	 /* MTA		*/
			 char	a_mla;	 /* MLA		*/
			 char	a_msa;	 /* MSA		*/
			 char	a_flg;	 /* Flags(Below)*/
			} d_addr;	 /* IEEE-488 Address Information    */

			long  d_flags;		/*	Flags		*/
			long  d_fncId;		/*	FNC Identifier	*/

			char  d_DbgLvl;		/*	Degug Level	*/
			char  d_SimLvl;		/*	Simulation Level */
			short d_Spare;		/*	Not Used	*/
	       } DEVDAT;
/*
 *  Values for d_addr.a_flg (see above)
 */
#define F_TALK	(char) 0x01	/* Currently Talk Addressed	*/
#define F_LISN	(char) 0x02	/* Currently Listen Addressed	*/
/*
 *      Structure of data descriptor
 */
typedef struct DDSCR {
		 short t_type;    /* data type : I_TYPE = int, R_TYPE = dec  */
				  /*             D_TYPE = dig, T_TYPE = txt  */
				  /*             C_TYPE = dsc, B_TYPE = bln  */
		 int
		       t_size,    /* data size                               */
		       t_count;   /* data count                              */
	       } DDSCR;

#define DEVSIZ  ((int) sizeof (DEVDAT))
#define MODSIZ  ((int) sizeof (MODDAT))
#define DATSIZ  ((int) sizeof (DATUM) )
#define INTV_SIZ ((int) sizeof (long))
#define DECV_SIZ ((int) sizeof (double))
#define DSCV_SIZ ((int) sizeof (short))
#define CPTR_SIZ ((int) sizeof (char *))
#define BLNV_SIZ ((int) sizeof (short))

#define A_CTL(x)	(DevDat[x].d_addr.a_ctl)
#define A_MTA(x)	(DevDat[x].d_addr.a_mta)
#define A_MLA(x)	(DevDat[x].d_addr.a_mla)
#define A_MSA(x)	(DevDat[x].d_addr.a_msa)
#define A_FLG(x)	(DevDat[x].d_addr.a_flg)

#define CTLK	((short) 0)	/* Controller Talk Address	*/
#define CLSN	((short) 1)	/* Controller Listen Address	*/
#define DTLK	((short) 2)	/* Device Talk Address		*/
#define DLSN	((short) 3)	/* Device Listen Address	*/
#define DSEC	((short) 4)	/* Device Secondary Address	*/

/************************************************************************/
/************************************************************************/
/*
 *              TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#define I_TYPE  ((short) 0)                /* Integer type         */
#define R_TYPE  ((short) 1)                /* Decimal type         */
#define D_TYPE  ((short) 2)                /* Digital type         */
#define T_TYPE  ((short) 3)                /* Text type            */
#define C_TYPE  ((short) 4)                /* Descriptor type      */
/*
 * mod_typ: 	initial: NO_TYPE
 *		if (value) -> I_TYPE .. C_TYPE, B_TYPE
 *		else if (set) -> NO_VAL_TYPE
 */
#define NO_TYPE ((short) 5)                /* default "type" (clear) */

#define NO_VAL_TYPE	((short) NO_TYPE + 1)   /* no value type   */
/*
 * the value of B_TYPE is after NO_VAL_TYPE for binary compatibility's sake.
 * my fault to squeeze the space between C_TYPE and NO_TYPE
 */
#define B_TYPE  ((short) NO_VAL_TYPE + 1)  /* Boolean type	   */

/************************************************************************/
/************************************************************************/
/*
 *              TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#define	LL0		0X1000			/* level-logic-zero	*/
#define LL1		0X2000			/* level-logic-one	*/
#define	LLQ		0X3000			/* level-logic-quies	*/
#define LLZ		0X4000			/* level-logic-hiz	*/

/************************************************************************/

/***********************************************************************/
/*
 *		TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#ifndef _ERR_ID_H_
#   define _ERR_ID_H_

/*
 * 08/93
 *
 * definition of fatal error message id:
 *	shared via CEM and RTS
 */

#define FATAL_UNLOAD		1030

/*--- others to be defined ---*/

#endif /* _ERR_ID_H_ */
/************************************************************************/
/*
 *              TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/
/*
 *     KEYWORDS (Index Into k_tok[])
 */
#define K_BGN  (0)
#define K_CDP  (1)
#define K_CLS  (2)
#define K_CON  (3)
#define K_DIS  (4)
#define K_DNM  (5)
#define K_END  (6)
#define K_EVL  (7)
#define K_FDD  (8)
#define K_FNC  (9)
#define K_FTH  (10)
#define K_IDY  (11)
#define K_INX  (12)
#define K_LOD  (13)
#define K_NUM  (14)
#define K_OPN  (15)
#define K_RST  (16)
#define K_SBF  (17)
#define K_SEA  (18)
#define K_SEP  (19)
#define K_SET  (20)
#define K_SRN  (21)
#define K_SRX  (22)
#define K_SSC  (23)
#define K_STA  (24)
#define K__CNT (25)

/************************************************************************/
/************************************************************************/
/*
 *              TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/
/*
 * File dev.h - CEM Device Information.
 */

#ifdef DOSWINDOW

#include  <windows.h>
#include  <windowsx.h>

#endif	/*** DOSWINDOW ***/

/************************************************************************/

#ifdef _PROTO_
extern	char PTE_HUGE_OBJ  *OShalloc( long, int );
extern	void		    OShfree(  char PTE_HUGE_OBJ * );
#else	/*** Not _PROTO_ ***/
extern	char PTE_HUGE_OBJ  *OShalloc();
extern	void		    OShfree();
#endif	/*** Not _PROTO_ ***/

#define  Room(n)  OShalloc((long)(n),OSMCLEAN)

/************************************************************************/

#define  d_fncs  d_acts

extern	DEVDAT	*DevDat;
extern	MODDAT	*fth_curr;

/************************************************************************/
/************************************************************************/
/*
 *              TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/
/*
 * errbits.h - Error Bits for use with CEM Macro ErrMsgBits().
 */

#ifndef TYX_CEM_ERRBITS_H
#define  TYX_CEM_ERRBITS_H

#define  EB_SEVERITY_MASK	0x0003
#define  EB_SEVERITY_INFO	0x0001
#define  EB_SEVERITY_WARNING	0x0002
#define  EB_SEVERITY_ERROR	0x0003

#define  EB_ACTION_MASK		0x000C
#define  EB_ACTION_HALT		0x0004
#define  EB_ACTION_RESET	0x0008
#define  EB_ACTION_ABORT	0x000C

#define  EB_SET_MASK		0x0010
#define  EB_SET_MAXTIME		0x0010

#endif	/*** TYX_CEM_ERRBITS_H ***/

/************************************************************************/
/************************************************************************/
/*
 *              TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#define DNULL   ((DATUM  *) NULL)
#define MNULL   ((MODDAT *) NULL)
/*
 *      Data Types
 */
#define INTV     I_TYPE
#define DECV     R_TYPE
#define DSCV     C_TYPE
#define TXTV     T_TYPE
#define DIGV     D_TYPE
#define BLNV	 B_TYPE
/*
 *	Modifier Prefix Codes
 */
#define	LEVEL_LOGIC_ZERO	LL0
#define	LEVEL_LOGIC_ONE		LL1
#define LEVEL_LOGIC_QUIES	LLQ
#define	LEVEL_LOGIC_HIZ		LLZ
/*
 *	User macros and functions
 */

/*
 *  Data Accessing Functions and Macros:
 *
 *    FthCnt()			Set Current FTH Byte Count
 *    FthDat()			Get Current FTH DATUM
 *    FthQual()			Get Current FTH Qualifier
 *    _GetIEEEadd()		Access IEEE Address Information
 *    cvt_mne_str_2_num		Convert Mnemonic String to Number
 *    datum_free()		Free a DATUM returned via retrieve_datum
 *    get_cur_action()		Get Current Action (A_xxx/K_BGN/K_END)
 *    get_cur_chn_num()		Get Current Channel Number
 *    get_cur_ctl_adr()		Get Current Controller Addresses
 *    get_cur_ctl_nam()		Get Current Controller Name
 *    get_cur_dev_adr()		Get Current Device Addresses
 *    get_cur_dev_chn()		Get Current Device / Channel Names
 *    get_cur_dev_level()	Get Current Devicee Level
 *    get_cur_dev_mchar()	Get Current Device Measured Char (M_xxxx)
 *    get_cur_dev_noun()	Get Current Device Noun (N_xxx)
 *    get_cur_linenum()		Get Current Line Number
 *    get_cur_mchar()		Get Current Measured Characteristic (M_xxxx)
 *    get_cur_modname()		Get Current Module Name
 *    get_cur_noun()		Get Current Noun (N_xxx)
 *    get_cur_statno()		Get Current Statement Number
 *    get_cur_verb()		Get Current Verb (V_xxx)
 *    get_datum()		Access a DATUM
 *    get_moddat()		Check if DATUM is a Modifier
 *    group_mods_control()	Control Group Modifier processing
 *    group_mods_get_info()	Get Group Modifier information
 *    remove_moddat()		Remove all DATUMS of 'm' from kernel
 *    retrieve_datum()		Access and remove a DATUM
 *    test_datum()		Test a DATUM.
 */
#ifdef _PROTO_
extern	void		 FthCnt( int );
extern	DATUM		*FthDat( void );
extern	int		 FthQual( void );
extern	short		 _GetIEEEadd( short );
extern	short		 cvt_mne_str_2_num( char * );
extern	void		 datum_free( DATUM * );
extern	short		 get_cur_action( void );
extern	int		 get_cur_chn_num( void );
extern	int		 get_cur_ctl_adr( int );
extern	char		*get_cur_ctl_nam( void );
extern	int		 get_cur_dev_adr( int );
extern	char		*get_cur_dev_chn( int );
extern	int		 get_cur_dev_level( int );
extern	short		 get_cur_dev_mchar( void );
extern	short		 get_cur_dev_noun( void );
extern	long		 get_cur_linenum( void );
extern	short		 get_cur_mchar( void );
extern	char		*get_cur_modname( void );
extern	short		 get_cur_noun( void );
extern	long		 get_cur_statno( void );
extern	short		 get_cur_verb( void );
extern	DATUM		*get_datum( short, short );
extern	int		 get_moddat( short );
extern	int		 group_mods_control(  int, int );
extern	int		 group_mods_get_info( int );
extern	int		 remove_moddat( short );
extern	DATUM		*retrieve_datum( short, short );
extern	int		 test_datum( DATUM *, int );
#else	/*** Not _PROTO_ ***/
extern	void		 FthCnt();
extern	DATUM		*FthDat();
extern	int		 FthQual();
extern	short		 _GetIEEEadd();
extern	short		 cvt_mne_str_2_num();
extern	void		 datum_free();
extern	short		 get_cur_action();
extern	int		 get_cur_chn_num();
extern	int		 get_cur_ctl_adr();
extern	char		*get_cur_ctl_nam();
extern	int		 get_cur_dev_adr();
extern	char		*get_cur_dev_chn();
extern	int		 get_cur_dev_level();
extern	short		 get_cur_dev_mchar();
extern	short		 get_cur_dev_noun();
extern	long		 get_cur_linenum();
extern	short		 get_cur_mchar();
extern	char		*get_cur_modname();
extern	short		 get_cur_noun();
extern	long		 get_cur_statno();
extern	short		 get_cur_verb();
extern	DATUM		*get_datum();
extern	int		 get_moddat();
extern	int		 group_mods_control();
extern	int		 group_mods_get_info();
extern	int		 remove_moddat();
extern	DATUM		*retrieve_datum();
extern	int		 test_datum();
#endif	/*** Not _PROTO_ ***/

#define  CvtMneStr2Num(s)	cvt_mne_str_2_num(s)
#define  GetDatum(m,q)		get_datum(m,q)
#define  RetrieveDatum(m,q)	retrieve_datum(m,q)
/*
 * free datum returned via RetrieveDatum()
 * NOTE: if datum is returned via GetDatum, this function call will mess
 *       up the kernel
 * some safety ??
 */

#define  AbortUserAction(x)     abort_user_action(x)

#define  IsActionBegin		( get_cur_action() == K_BGN )
#define  IsActionEnd		( get_cur_action() == K_END )
#define  IsActionLoad		( get_cur_action() == A_LOD )

#define  IsOkCurDevAddr()	get_cur_dev_adr( 5 )

#define  GetCurChnNam()		get_cur_dev_chn( 2 )
#define  GetCurChnNum()		get_cur_chn_num()
#define  GetCurCtlMLA()		get_cur_ctl_adr( 2 )
#define  GetCurCtlMTA()		get_cur_ctl_adr( 1 )
#define  GetCurCtlNam()		get_cur_ctl_nam()
#define  GetCurDevChnNam()	get_cur_dev_chn( 3 )
#define  GetCurDevCtl()		get_cur_dev_adr( 1 )
#define  GetCurDevDbgLvl()	get_cur_dev_level( 1 )
#define  GetCurDevMChar()	get_cur_dev_mchar()
#define  GetCurDevMLA()		get_cur_dev_adr( 3 )
#define  GetCurDevMSA()		get_cur_dev_adr( 4 )
#define  GetCurDevMTA()		get_cur_dev_adr( 2 )
#define  GetCurDevNam()		get_cur_dev_chn( 1 )
#define  GetCurDevNoun()	get_cur_dev_noun()
#define  GetCurDevSimLvl()	get_cur_dev_level( 2 )
#define  GetCurLineNum()	get_cur_linenum()
#define  GetCurMChar()		get_cur_mchar()
#define  GetCurModName()	get_cur_modname()
#define  GetCurNoun()		get_cur_noun()
#define  GetCurStatNo()		get_cur_statno()
#define  GetCurVerb()		get_cur_verb()

#define  SetGroupModsON(m)	group_mods_control(1,m)
#define  SetGroupModsMOD(m)	group_mods_control(2,m)
#define  SetGroupModsLEVEL()	group_mods_control(3,0)
#define  SetGroupModsGROUP()	group_mods_control(4,0)
#define  SetGroupModsOFF()	group_mods_control(5,0)
#define  SetGroupModsRESET()	group_mods_control(6,0)

#define  GetCurGroupModMOD()	group_mods_get_info(1)
#define  GetCurGroupModLEVEL()	group_mods_get_info(2)
#define  GetCurGroupModGROUP()	group_mods_get_info(3)

#define  IsOkDatumFresh(p)	test_datum(p,(-1))

#define  IsDatumFresh(p)	test_datum(p,1)

#define FreeDatum(p)		datum_free(p)

#define IsMod(m)		get_moddat(m)
				/*  Is Modifier - m = Modifier 		    */

#define	RemoveMod(m)		remove_moddat(m)
				/*  Remove all DATUMs of m from kernel      */

#define FthMod()	((fth_curr == MNULL) ? 0 : fth_curr->m_mod)
				/*  Current Fetch Modifier		    */

#define DatTyp(p)       (p)->dat_typ
#define DatSiz(p)       (p)->dat_siz
#define DatCnt(p)       (p)->dat_cnt

#define INTDatVal(p, i) 	DatVal(p, dat_int, i)
#define DECDatVal(p, i) 	DatVal(p, dat_dec, i)
#define DSCDatVal(p, i) 	DatVal(p, dat_dsc, i)
#define DIGDatVal(p, i) 	DatVal(p, dat_dig, i)
#define BLNDatVal(p, i)		DatVal(p, dat_bln, i)

#define DIGDatByte(pc, b)	*((pc) + b)

#define GetTXTDatVal(p, i) 		DatVal(p, dat_txt, i)
#define PutTXTDatVal(p, i, sTr) 	CScopy(sTr, DatVal(p, dat_txt, i))

#define DatUnit(p)      (p)->dat_unt
/*
 *      Data Value
 *
 *      p = Pointer to DATUM (From Kernel)
 *      i = Index
 */
#define DatVal(p, t, i) (p)->dat_val.t[i]
/*
 *	Error Message
 *
 *	n = message number
 *	m = message
 */
#define ErrMsg(n,m)		(_Derror((short)n, m))
#define ErrMsgBits(n,m)		(_DerrorBits((short)n, m))
/*
 *	Macros for accessing IEEE-488 address information
 */
#define CntTlkAdd()	_GetIEEEadd(CTLK)
#define CntLsnAdd()	_GetIEEEadd(CLSN)
#define DevTlkAdd()	_GetIEEEadd(DTLK)
#define DevLsnAdd()	_GetIEEEadd(DLSN)
#define DevSecAdd()	_GetIEEEadd(DSEC)
/*
 *	Print to CEM.LOG
 *
 *	Print1 -- print a string and newline
 *	Prints -- print a string without newline
 */
#define Print1(x)	_usrlog(x,1)
#define Prints(x)	_usrlog(x,0)
/*
 *	Terminal Output
 */
#define Display(x)	_Display(x)
/*
 *	Terminal Input
 *	p : prompt	b : User buffer
 */
#define GetStr(p, b)	_GetStr(p, b)
/*
 *	Interrupt Macros
 */
#define SetFault(n)	_SetIntr(n)
#define SetATEflt()	_SetIntr(2)	/* ELTA		*/
#define SetUUTflt()	_SetIntr(1)	/* ELTA		*/
/*
 *  Flag Information (ELTA)
 */
#ifdef _PROTO_
extern	unsigned int	 DebugDriver( void );
extern	unsigned int	 GpibString( void );
#else	/*** Not _PROTO_ ***/
extern	unsigned int	 DebugDriver();
extern	unsigned int	 GpibString();
#endif	/*** Not _PROTO_ ***/
/*
 *  Addition Function Prototypes for Functions referenced in Macros.
 */
#ifdef _PROTO_
extern	void		 _Derror( short, char * );
extern	void		 _DerrorBits( short, char * );
extern	void		 _Display( const char * );
extern	void		 _GetStr( char *, char * );
extern	void		 _SetIntr( int );
extern	void		 _usrlog( char *, int );
extern	int		 CScopy( char *, char * );
#else	/*** Not _PROTO_ ***/
extern	void		 _Derror();
extern	void		 _DerrorBits();
extern	void		 _Display();
extern	void		 _GetStr();
extern	void		 _SetIntr();
extern	void		 _usrlog();
extern	int		 CScopy();
#endif	/*** Not _PROTO_ ***/
/*
 *  Function Prototypes for Functions related to User Actions.
 */
#ifdef _PROTO_
extern	void		 abort_user_action( int );
#else	/*** Not _PROTO_ ***/
extern	void		 abort_user_action();
#endif	/*** Not _PROTO_ ***/
/*
 *  Function Prototypes for Miscellaneous Functions.
 */
#ifdef _PROTO_
extern	void		 CemReqUnload(        void );
extern	int		 DclResetDisable(     char * );
extern	int		 DclResetEnable(      char * );
extern	int		 SetCurDev(           char * );
extern	int		 SubChanParent(       char * );
extern	int		 SubChanResetDisable( char * );
extern	int		 SubChanResetEnable(  char * );
#else	/*** Not _PROTO_ ***/
extern	void		 CemReqUnload();
extern	int		 DclResetDisable();
extern	int		 DclResetEnable();
extern	int		 SetCurDev();
extern	int		 SubChanParent();
extern	int		 SubChanResetDisable();
extern	int		 SubChanResetEnable();
#endif	/*** Not _PROTO_ ***/
/************************************************************************/
/***********************************************************************/
/*
 *		TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#ifndef	TYX_CEM_USERFUNC_H
#define  TYX_CEM_USERFUNC_H

#ifdef _PROTO_
extern	void	 userInit(     void );
extern	void	 userMiscTerm( void );
extern	void	 userTerm(     int );
#else	/*** Not _PROTO_ ***/
extern	void	 userInit();
extern	void	 userMiscTerm();
extern	void	 userTerm();
#endif	/*** Not _PROTO_ ***/

#ifdef _PROTO_
extern	void	 userStubPrintDeviceInfo(   char * );
extern	void	 userStubPrintOnly(         void );
extern	void	 userStubSetLevel(          int  );
extern	DATUM	*userStubFindDatum(         char *, char *,  char *, int );
extern	void	 userStubPrintValue(        char *, DATUM *, long );
extern	int	 userStubNaaCLOSE(          void );
extern	int	 userStubNaaCONNECT(        void );
extern	int	 userStubNaaDEVICECLEAR(    void );
extern	int	 userStubNaaDISCONNECT(     void );
extern	int	 userStubNaaINTERFACECLEAR( void );
extern	int	 userStubNaaINTERRUPT(      void );
extern	int	 userStubNaaOPEN(           void );
extern	int	 userStubCLOSE(             void );
extern	int	 userStubCONNECT(           void );
extern	int	 userStubDISCONNECT(        void );
extern	int	 userStubFETCH(             void );
extern	int	 userStubINIT(              void );
extern	int	 userStubLOAD(              void );
extern	int	 userStubOPEN(              void );
extern	int	 userStubRESET(             void );
extern	int	 userStubSETUP(             void );
extern	int	 userStubSTATUS(            void );
#else	/*** Not _PROTO_ ***/
extern	void	 userStubPrintDeviceInfo();
extern	void	 userStubPrintOnly();
extern	void	 userStubSetLevel();
extern	DATUM	*userStubFindDatum();
extern	void	 userStubPrintValue();
extern	int	 userStubNaaCLOSE();
extern	int	 userStubNaaCONNECT();
extern	int	 userStubNaaDEVICECLEAR();
extern	int	 userStubNaaDISCONNECT();
extern	int	 userStubNaaINTERFACECLEAR();
extern	int	 userStubNaaINTERRUPT();
extern	int	 userStubNaaOPEN();
extern	int	 userStubCLOSE();
extern	int	 userStubCONNECT();
extern	int	 userStubDISCONNECT();
extern	int	 userStubFETCH();
extern	int	 userStubINIT();
extern	int	 userStubLOAD();
extern	int	 userStubOPEN();
extern	int	 userStubRESET();
extern	int	 userStubSETUP();
extern	int	 userStubSTATUS();
#endif	/*** Not _PROTO_ ***/

#endif	/*** TYX_CEM_USERFUNC_H ***/

/************************************************************************/
/************************************************************************/
/*
 * File cemhdr9.h
 */

#ifdef __cplusplus
}
#endif	/*** __cplusplus ***/

#endif	/*** TYX_CEM_CEMHDR_H ***/

/************************************************************************/
