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

#define  SYS12B			/* Windows NT - MS Visual C++ 2.0	*/
#define  SYSNAME  "Windows NT - MSVC 2.0"
#define  SYSOSWNT
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

#ifndef _TP_FUNC_H_

#define _TP_FUNC_H_

#ifdef _PROTO_

extern	char	*VerDate( void );

extern  void	 BCcomm( void );
extern  void	 CScomm( void );
extern  void	 DTcomm( void );
extern  void	 IOcomm( void );
extern  void	 LFcomm( void );
extern  void	 NWcomm( void );
extern  void	 OScomm( void );
extern  void	 SLcomm( void );
extern  void	 VMcomm( void );

#else

extern	char	*VerDate();

extern  void	 BCcomm();
extern  void	 CScomm();
extern  void	 DTcomm();
extern  void	 IOcomm();
extern  void	 LFcomm();
extern  void	 NWcomm();
extern  void	 OScomm();
extern  void	 SLcomm();
extern  void	 VMcomm();

#endif		/* _PROTO_ */

#endif		/* _TP_FUNC_H_ */

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

#ifndef _CS_FUNC_H_

#define _CS_FUNC_H_

#ifdef _PROTO_

extern  void	CSca2c( int, char *, char * );
extern	void	CSca2i( int, char *, long * );
extern  int     CScat( char *, char *, char * );
extern  void    CScb2w( char *, short *, int );
extern  int	CScc2a( int, char *, char * );
extern  void    CScd2f( char *, double * );
extern  int     CScf2d( double *, char * );
extern  void    CSch2w( char *, short *, int );
extern	int	CSci2a( int, long *, char * );
extern  int     CSci2b( long *, char *, int );
extern  int     CSci2n( long *, char * );
extern  int     CSci2o( long *, char *, int );
extern  int     CScj2n( int, char * );
extern  int     CScl2u( char *, char * );
extern  void    CScn2i( char *, long * );
extern  void    CSco2i( char *, long * );
extern  void    CSco2w( char *, short *, int );
extern  int     CScopy( char *, char * );
extern  void    CScs2i( char *, long * );
extern  int     CScu2l( char *, char * );
extern  int     CScw2b( short *, int, char * );
extern  int     CScw2h( short *, int, char * );
extern  int     CScw2o( short *, int, char * );
extern  int     CSflc( char *, int );
extern  int     CSftc( char *, int );
extern	int	CSgf( char *, char *, int );
extern  int     CSgn( char *, char * );
extern	int	CSgw( char *, char * );
extern	int	CSgx( char *, char *, int );
extern  void    CSilc( char *, char *, int, int );
extern  void    CSitc( char *, char *, int, int );
extern  int     CSks( char *, char * );
extern  int     CSksb( char *, char * );
extern  int     CSrlb( char *, char * );
extern  int     CSrlw( char *, char * );
extern  int     CSrtb( char *, char * );
extern  int     CSrtw( char *, char * );
extern  int     CSrwm( char *, char * );
extern  int     CSsow( char * );
extern  int     CSstw( char * );
extern  int     CSva( int, char * );
extern  int     CSvd( char * );
extern  int     CSvh( char * );
extern  int     CSvn( char * );
extern  int     CSvnp( char * );
extern  int     CSvo( char * );

#else

extern  void	CSca2c( );
extern	void	CSca2i( );
extern  int     CScat( );
extern  void    CScb2w( );
extern  int	CScc2a( );
extern  void    CScd2f( );
extern  int     CScf2d( );
extern  void    CSch2w( );
extern	int	CSci2a( );
extern  int     CSci2b( );
extern  int     CSci2n( );
extern  int     CSci2o( );
extern  int     CScj2n( );
extern  int     CScl2u( );
extern  void    CScn2i( );
extern  void    CSco2i( );
extern  void    CSco2w( );
extern  int     CScopy( );
extern  void    CScs2i( );
extern  int     CScu2l( );
extern  int     CScw2b( );
extern  int     CScw2h( );
extern  int     CScw2o( );
extern  int     CSflc( );
extern  int     CSftc( );
extern	int	CSgf( );
extern  int     CSgn( );
extern	int	CSgw( );
extern	int	CSgx( );
extern  void    CSilc( );
extern  void    CSitc( );
extern  int     CSks( );
extern  int     CSksb( );
extern  int     CSrlb( );
extern  int     CSrlw( );
extern  int     CSrtb( );
extern  int     CSrtw( );
extern  int     CSrwm( );
extern  int     CSsow( );
extern  int     CSstw( );
extern  int     CSva( );
extern  int     CSvd( );
extern  int     CSvh( );
extern  int     CSvn( );
extern  int     CSvnp( );
extern  int     CSvo( );

#endif		/* _PROTO_ */

#endif		/* _CS_FUNC_H_ */

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
#define  OSRSERR  (-1)		/* Error				*/
#define  OSRSOK     0		/* No errors detected			*/
#define  OSRSEOF    0		/* End-of-File				*/
#define  OSRSDTO    0           /* Bus Device Timeout Occurred Flag     */
#define  OSRSYES    1		/* "Yes", "True",  etc.			*/
#define  OSRSNO     2		/* "No",  "False", etc.			*/
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
/*
 *  Maximum Integers
 */
#define  OSPMAXINT  2147483647  /* Positive max int                     */
#define  OSNMAXINT -2147483647  /* Negative max int                     */
/*
 *  Bus Error Codes for OSberr
 */
#define  OSBNORM  0		/* No Error				*/
#define  OSBUNKN  (-1)		/* Unknown Error			*/
#define  OSBTIMO  (-2)		/* Timeout Occurred			*/
/*  
 *  Define Flags For OSbwait
 */
#define OSWSRQ  1
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
 *		TRADE RESTRICTIVE NOTICE
 *
 * The content of this material is proprietary and constitutes a trade
 * secret.  It is furnished pursuant to written agreements or instructions
 * limiting the extent of disclosure.  Its further disclosure in any form
 * without the written permission of its owner, TYX Corporation,
 * is prohibited.
 */
/************************************************************************/

#ifndef _TYX_OSCB_H_
#   define _TYX_OSCB_H_

/*
 *  Structure for File System Control Block.
 */
	struct OSCB {
		int    oscbflag;    /* Flags (see below)                */
		char  *oscbpcfn;    /* Ptr: File Name                   */
		FID    oscbfid;     /* Mailbox Handle / File ID         */
	};


//
// A lot of these flags are not used any more, as indicated by being
// commented out
//
// We need to leave them there because DOS library might still use it

#define  OSCBOPN  0100000   /* File is Open                     */
#define  OSCBRTL  0040000   /* File is a Raw Terminal Line      */
#define  OSCBCCL  0020000   /* File is a Computer-to-Computer Line	*/
#define  OSCBDIR  0010000   /* File is a Directory              */
#define  OSCBITC  0004000   /* File is an Inter-Task Channel    */
#define  OSCBIEB  0002000   /* File is an IEEE-488 Bus          */
#define  OSCBPAR  0000400   /* File belongs to a Parent         */
#define  OSCBNWT  0000100   /* File is NOWAIT on Input          */
#define  OSCBMRK  0000040   /* File is Marked                   */
#define  OSCBMFI  0000020   /* File is Marked: Filter Standard Input */
#define  OSCBMFO  0000010   /* File is Marked: Filter Standard Output */
#define  OSCBDEV  0000004   /* File is a Device                 */
#define  OSCBREA  0000002   /* File is open for Reading         */
#define  OSCBWRT  0000001   /* File is open for Writing         */


#endif /* end of _TYX_OSCB_H_ */

/************************************************************************/
/*
 * OS library function prototype
 */
#ifndef _OS_FUNC_H_

#   define _OS_FUNC_H_

extern  int     OSadisable( void );
extern  int     OSaenable( void );
extern  int     OSainitialize( int, int, int );
extern  int     OSaterminate( void );
extern  int     OSatest( void );
extern  int     OSbclose( int );
extern  int     OSbopen( char * );
extern  int     OSbra( int, char *, int );
extern  int     OSbrb( int, char *, int );
extern  int     OSbreset( int );
extern	int	OSbstat( int, int );
extern  int     osbtmo( void);
extern  int     _osbtmo (int);
extern  int     OSbwa( int, char *, int );
extern  int     OSbwait( int, unsigned int );
extern  int     OSbwb( int, char *, int );
extern  int     OSbwc( int, char *, int );
extern  int     OScclose( int );
extern  int     OSccreate( char * );
extern  int     OScfilter( char *, int );
extern  int     OScmark( char *, int );
extern  void    osconf( void );
extern  int     OScopen( char *, int );
extern  int     OScread( int, char *, int );
extern  int     OScremove( char * );
extern  int     OScwrite( int, char *, int );
extern  void    OSdata( void );
extern  int     OSdclos( int );
extern  int     OSdcrea( char * );
extern  int     OSdopen( char * );
extern  char  * OSdread( int );
extern  int     OSdremv( char * );
extern  int     OSeclose( void );
extern  char  * OSeget( char * );
extern  int     OSeopen( void );
extern  char  * OSeread( void );
extern  void    oserr( char *, char * );
extern  void    OSexit( int );
extern  int     osfacb( char *, int );
extern  int     OSfclose( int );
extern  int     OSfcreate( char *, int, int, int );
extern  int     OSfopen( char *, int, int, int );
extern  long    OSfpos (int);
extern  int     OSfread( int, char *, int );
extern  int     OSfremove( char * );
extern  int     OSfseek( int, long, int );
extern  long    OSfsize( int );
extern  int     OSfwrite( int, char *, int );
extern  void    OSkill( int );
extern  int     oskssa( char * );
extern  void    OSmain( int *, char *** );
extern	char	**OSwmain (char *, char *, int *);
extern  char	*OSmalloc( unsigned, int );
extern  char	*OSmrealloc( char *, unsigned );
extern  void    OSmfree( char * );
extern  int     OSpfalloc( int );
extern  int     OSpfdealloc( int );
extern  int     OSpfdisable( int );
extern  int     OSpfenable( int );
extern  int     OSpfreset( int );
extern  int     OSpfset( int, int );
extern  int     OSpftest( int );
extern  int     OSrun( int, char *, char **, char **, int * );
extern  int     OSrunforeign( char *, char **, int * );
extern  void    OSsleep( int );
extern  int     OSstart( void );
extern  int     OStdup( int, int );
extern  void    OStime( long * );
extern  long    OStimes( long * );
extern  int     OStread( int, char *, int );
extern  int     OStreset( int );
extern  int     OStsetup( int );
extern  int     OStwrite( int, char *, int );

#endif /* end of _OS_FUNC_H_ */
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

#ifndef _TYX_OSMAIN_H_
#   define _TYX_OSMAIN_H_

/*
 *  Operating System Control Block Data.
 */
#ifndef OSCBMAX1
#define  OSCBMAX1  5
#endif
#ifndef OSCBMAX2
#define  OSCBMAX2  5
#endif
#define  OSCBMAX  OSCBMAX1+OSCBMAX2
	struct OSCB  OSascb[OSCBMAX] BSS;
	int          OSncbx = OSCBMAX;

/*
 *  SIGINT Handling Flag.
 */
	int   OSignsig = 1;

#endif /* end of _TYX_OSMAIN_H_ */
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
#ifndef	_CONSTANTS_H_
#define	_CONSTANTS_H_

#define ASSZ            60
#define ATI             1       /* Asynchronous Terminal Interrupt      */

#define ATIPF           1       /* ATI Program Flag - Child Number      */
#define INTT            2       /* Interpreter      - Child Number      */
#define CEM             3       /* CIIL Emulator    - Child Number      */

#define INTToCEM        "INTToCEM"
#define CEMToINT        "CEMToINT"
#define CEMInpCH	"CEMInpCH"

#define BACKSPACE       0010
#define BLANK           040
#define CR              015
#define CSSZ            20
#define DATASPAC        1
#define DELCHAR         0177
#define EOS             0
#define BFALSE           0X0000
#define FIRSTVA         3
#define K_SIZ           (512 - 36)
#define LEXSPAC         2
#define MAXCLK          10
#define MAXCOL          72
#define MSGSPAC         3
#define NEWLINE         0012
#define NO              0

#ifndef NULL
#define NULL            0
#endif

#define OBJSPAC         0
#define STM_AIL_SIZ     21L
#define ONE             1
#define PAGESZ          512
#define BYTSPSEG        1024
#define RUN             0
#define TASK            0
#define UTRMDEF         "ute.dtf"
#define TERMDEF         "pte.dtf"
#define BTRUE           0XFFFF
#define YES             1
/*
 *      BusLog Functions
 */
#define READ            0
#define WRITE           1
#define COMMAND         2
#define RESET           3

/*
 *      Bus Address State
 */
#define UAD     0                       /* Device Unaddressed           */
#define TLK     1                       /* DeviceTalk Addressed         */
#define LST     2                       /* DeviceListen Addressed       */
#define UNK     99                      /* Unknown                      */

#endif 	/* _CONSTANTS_H_ */
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
 * rts & cem
 */

#define U8      char
#ifndef OLD_SYS04C
#define U16     unsigned short
#else
#define U16     unsigned
#endif
#define S16     short
#define U32     unsigned long
#define S32     long
#define FLT     double
#define REAL    float
#define LVA     U32            /* Long Virtual Address  */
#define SVA     U16            /* Short Virtual Address */

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
 *      Configured for MATE Compatible Interfaces
 *      February 28 1984  -- Jack Hyde
 */
#define CODE_X  0X00FF          /* Instruction Mask             */
#define SKIP_N  0X0100          /* Skip - Unconditional         */
#define EXTD_N  0X0200          /* Extended Node                */
#define IPRO_N  0X0400          /* Prototype Code               */
#define IMAC_N  0X0800          /* Macro     Code               */
#define FLAG_N  0X1000          /* Flag (Usage - Various)       */
#define LABL_N  0X2000          /* Labeled Node                 */
#define SIGX_N  0X4000          /* Signal Description - C.lop   */
#define FLSH_N  0X8000          /* End-Of-Record  NOT IN AC     */


#define Mask(n) ((0XFFFF << (n)) ^ 0XFFFF)

#define NN_SZ   7
#define NN_MX   Mask(NN_SZ)
#define NN_SX   (16 - NN_SZ)

#define MD_SZ   9
#define MD_MX   Mask(MD_SZ)
#define MD_SX   (16 - MD_SZ)

#define MC_SZ   MD_SZ
#define MC_MX   MD_MX
#define MC_SX   MD_SX

#define DM_SZ   7 
#define DM_MX   Mask(DM_SZ)
#define DM_SX   (16 - DM_SZ)

#define STM     71
#define MAXCODE 135

#define CIN     108                     /* IN  - Instruction            */
#define COUT    111                     /* OUT - Instruction            */

#define C_res   res.sres.c_res
#define C_resq  res.sres.c_resq
#define C_lres  res.c_lres
#define C_op1   op.sop.c_op1
#define C_op2   op.sop.c_op2
#define C_lop   op.c_lop12
#define C_op3   opx.sopx.c_op3
#define C_op4   opx.sopx.c_op4
#define C_lop12 op.c_lop12
#define C_lop34 opx.c_lop34

#define STMSIZ  sizeof(struct OBJ_STM)
#define EXNSIZ  sizeof(struct OBJ_EXN) 
#define OBNSIZ  (EXNSIZ - 4)

/*
 *      <file>.DAT Data Constants
 */


#define DType(x)    ((x) & ((U16) 0XF))         /* Isolate Data Type    */
#define DSubT(x)    (((x) >> 4) & ((U16) 0XF))  /* Isolate Data SubType */
#define NTYPE   0                       /* Boolean                      */
#define ITYPE   1                       /* Integer                      */
#define RTYPE   2                       /* Float                        */
#define ATYPE   3                       /* Pointer                      */
#define TTYPE   4                       /* Text                         */
#define ZTYPE   5                       /* Digital Pin Set              */
#define X89TP	6			/* Conn - rts89 only		*/
#define DTYPE   7                       /* Digital                      */
#define FTYPE   8                       /* File Control Block           */
#define XTYPE   9                       /* Conn - 1985 only             */
#define QTYPE   10                      /* Indirect Path                */
#define FTTYP   11                      /* Path <from> / <to> Table     */
#define MPTYP   12                      /* Macro Path type              */

#define DSize(x)    ((x) >> 8)          /* Isolate Data Size            */
#define NSIZE   2                       /* Boolean                      */
#define ISIZE   3                       /* Integer                      */
#define RSIZE   5                       /* Float                        */
#define ASIZE   3                       /* Pointer                      */
#define TSIZE   1                       /* Text            (Overhead)   */
#define ZSIZE   1                       /* Digital Pin Set (Overhead)   */
#define X89SZ	5			/* rts89 Conn 	   (Overhead)   */
#define DSIZE   1                       /* Digital         (Overhead)   */
#define FSIZE   11                      /* FCB             (Overhead)   */
#define XSIZE   2                       /* Conn            (Overhead)   */
#define QSIZE   3                       /* Indirect Path   (Overhead)   */
#define FTSIZ   1                       /* <from> / <to>   (Overhead)   */
#define MPSIZ   2                       /* Dynamic (sztyp & d_mpth_cnt) */

#define QESIZ   8                       /* Indirect Path   (Entry   )   */
#define TESIZ   4                       /* <from> / <to>   (Entry   )   */


#define NSZTYP  ((NSIZE << 8) | NTYPE)  /* Boolean                      */
#define ISZTYP  ((ISIZE << 8) | ITYPE)  /* Integer                      */
#define ASZTYP  ((ASIZE << 8) | ATYPE)  /* Integer                      */
#define RSZTYP  ((RSIZE << 8) | RTYPE)  /* Float                        */
#define X89ST	((X89SZ << 8) | X89TP)	/* Conn -- rts89		*/

#define D_offset	d_val.d_ival	/* Offset	-- rts89	*/
#define D_index		d_val.d_ival	/* Subscript    -- rts 85	*/

/*
 *      The 'Lex' File Data Constants
 */
#define	PAWSIEEE7161989		101		/* subset 1989		*/
#define	MATESUBSET		1066		/* MATE subset		*/

#define CTAB_V  10                              /* Verb         CIIL    */
#define CTAB_N  11                              /* Noun         CIIL    */
#define CTAB_P  12                              /* Reserved             */
#define CTAB_D  13                              /* Dimension    CIIL    */
#define CTAB_M  14                              /* Modifier     CIIL    */
#define CTAB_T  16                              /* Module's     CIIL    */
#define CMP_VT  24                              /* COMPARE Verb Token   */
#define VER_VT  25                              /* VERIFY Verb Token    */
#define MON_VT  26                              /* MONITOR Verb Token   */
#define DO_VT   27				/* DO Verb Token	*/
#define TRM_VT	28				/* TERMINATE Verb Token	*/
#define	MEA_VT	29				/* MEASURE Verb Token	*/
#define	REA_VT	30				/* READ    Verb Token	*/
#define FLG_89  100				/* 1989 flag		*/

/*
 *      FCB Usage:
 *
 *              F_cnt   Number of bytes on last Read (Input Only)
 *              F_idx   Index to next byte to "Get" or "Put"   
 *              F_buf   Actual Buffer
 *              F_siz   Size of Buffer
 *              F_fd    File Descriptor
 *              F_flg   Flags 
 *              F_nam   VAD of File Name
 *
 */

#define F_cnt   d_val.d_fcb.d_fxxx
#define F_typ   d_val.d_fcb.d_fyyy
#define F_idx   d_val.d_fcb.d_frix
#define F_buf   d_val.d_fcb.d_fbuf
#define F_siz   d_val.d_fcb.d_frsz
#define F_fd    d_val.d_fcb.d_fdsc
#define F_flg   d_val.d_fcb.d_faxc
#define F_nam   d_val.d_fcb.d_fname
#define F_blk	d_val.d_fcb.d_fpsz

/*
 * Values for F_flg
 */
#define FX_RO	((U16) 0x0000)		/* READ ONLY			*/
#define FX_WO	((U16) 0x0001)		/* WRITE ONLY			*/
					/*   (and CREATE TEXT file	*/
					/*    if rts 85)		*/
#define FX_RW	((U16) 0x0002)		/* READ & WRITE			*/
					/*   (and CREATE/OPEN BINARY    */
					/*    file if rts 85)		*/
#define FX_MOD	((U16) 0x0008)		/* Modified Buffer -- 89 only   */
#define FX_OP	((U16) 0x0010)		/* OPEN   -- 89 only		*/
#define FX_CR	((U16) 0x0020)		/* CREATE -- 89 only		*/
#define FX_EOF	((U16) 0x0040)		/* EOF -- 89 only		*/
#define FX_UT	((U16) 0x0100)		/* UNTYPED - 89 only		*/
#define FX_TT	((U16) 0x0200)		/* TEXT   -- 89 only		*/
#define FX_KT	((U16) 0x0300)		/* KNOWN TYPE -- 89 only	*/
#define FX_VM	((U16) 0x0400)		/* VAX/VMS file - CASS only	*/

#define FX_89	((U16) 0x0300)		/* how to determine 89 FCB */

#define AccMode(pfcb)	((unsigned int)((pfcb)->F_flg & (U16)0x0003))
#define IsWOfile(pfcb)	((pfcb)->F_flg & FX_WO)
#define IsROfile(pfcb)	(((pfcb)->F_flg & (U16)0x0003) == 0)
#define ClrAcc(pfcb)	{if (Is89FCB(pfcb)) (pfcb)->F_flg &= ~0x0003;}

#define IsCassFCB(pfcb)	((pfcb)->F_flg & FX_VM)

/*#define Is89FCB(pfcb)	(((pfcb)->F_flg > FX_RW) && ((pfcb)->F_flg < FX_VM))*/
#define Is89FCB(pfcb)	((pfcb)->F_flg & FX_89) 

#define Getc(p) (((p)->F_idx) < ((p)->F_cnt)                    ?       \
		 (((int) (p)->F_buf[((p)->F_idx)++]) &0XFF)     :       \
		 f_getc(p)                                              \
		)

/*
 *	89 only
 */
#define RstFCB(pfcb)	(pfcb)->F_flg &= ~(FX_EOF|(U16)0x0003)
#define SetMod(pfcb)	(pfcb)->F_flg |= FX_MOD
#define RstMod(pfcb)	(pfcb)->F_flg &= ~FX_MOD
#define ModBuf(pfcb)	((pfcb)->F_flg & FX_MOD)		

#define SetEof(pfcb)	(pfcb)->F_flg |= FX_EOF
#define RstEof(pfcb)	(pfcb)->F_flg &= ~FX_EOF
/*
 *	ChkEof is implemented as a function in files.c
 *
#define ChkEof(pfcb)	((pfcb)->F_flg & FX_EOF)		
 *
 */
#define Blkno(pfcb)		((U32) (pfcb)->F_blk)		
#define SetBlkno(pfcb, b)	(pfcb)->F_blk = (U16) b		
#define NxtBlkno(pfcb)		((U32) ((pfcb)->F_blk + 1))	

#define Bin89File(pfcb)	(((pfcb)->F_flg & FX_KT) != FX_TT)	
#define Txt89File(pfcb)	(((pfcb)->F_flg & FX_KT) == FX_TT)	
#define Unt89File(pfcb)	(((pfcb)->F_flg & FX_KT) == FX_UT)	
#define Kwn89File(pfcb)	(((pfcb)->F_flg & FX_KT) == FX_KT)	
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

#ifndef _STRUCTS_H_
#   define _STRUCTS_H_

#define	MAXINPUT	255	/* maximun number of char input */

/*
 *	CIIL table pointer in LEX file
 */

#define	CIIL_LOW	10

#define	CTAB_V		10		/* VERB		*/
#define	CTAB_N		11		/* NOUN		*/
#define	CTAB_P		12		/* PORT		*/
#define	CTAB_D		13		/* DIMENSION	*/
#define	CTAB_M		14		/* MODIFIER	*/
#define	NOT_CIIL	15	/**** Not a CIIL table **/
#define	CTAB_MT		16		/* MODULE_TYPE	*/

#define CIIL_HIGH	17

#define	IsCIILtable(i)	((i >= CIIL_LOW) && (i < CIIL_HIGH) && (i != NOT_CIIL))

/*
 *      CIIL table descriptors
 */
struct CIIL_DSC
	{
	 U16    ciil_cnt;               /* Number of entries    */
	 U16    ciil_siz;               /* Element size - words */ 
	 SVA    ciil_vad;               /* VA of CIIL table     */

#ifdef OPT_CIIL			/* optimize ciil */

#define MAX_CIIL	2048	/* maximum ciil entries */

	 char	*ciil_str[MAX_CIIL];	/* ptr to ciil strings */

#endif

	};


/* 
 *      Delay Clocks
 */
struct  CLOCK {
	       S16      k_flags;        /* Flags (See Below)	*/
	       S32      k_time;         /* Time of event        */
	       S32      k_vlc;          /* VA of location       */
	       S32	k_start;	/* Clock Start Time	*/
	      };
/*
 *	Values For k_flags
 */
#define K_ALLOC		((S16) 0x0001)		/* In Use	*/
#define K_USER		((S16) 0x0002)		/* User Clock	*/

#define	K_TYPE_I	((S16) 0x0100)		/* Type 1 clock	*/
#define K_TYPE_II       ((S16) 0x0200)          /* Type 2 clock */
#define K_TYPE_III	((S16) 0x0400)		/* Type 3 clock	*/
#define K_TYPE_IV	((S16) 0x0800)		/* Type 4 clock	*/

#define	CLOCK_TYPE_MASK	((S16) 0x0F00)		/* Mask		*/

#define	ClockType(i)	(0x0001 << (7 + i))	/* Type bit on	*/

#define	IsClockType(t, i) ((t & CLOCK_TYPE_MASK) == ClockType(i))
						/* Is clock of type i ? */

/*
 *	Call Stack
 */
struct  STACK {
		S32  		stkvlc,		/* Return address	    */
		     		stkstn,		/* Current statement number */
		     		stkbad;		/* Current Base Address	    */
		struct CLOCK 	*stkclk;	/* Current p_clk	    */
		S16             stkbus;         /* Current Bus Number       */
		S16		stkfrt;		/* Current fncrtn	    */
		U8   		stkhlt;		/* Current Halt State	    */
		U8		stklvl;		/* Current Run Level  	    */
		U32             stksrf;         /* Current Source Statement */
		U16             stkchn;         /* Current Dev Channel      */
	      };
/*
 *      <file>.OBJ Data Structures
 */
struct OBJ_HEAD {
		 LVA    chd_info;       /* VAC - 'Info'                 */
		 U8     chd_date[32],   /* Date of Creation             */
			chd_name[16];   /* ATLAS Program Name           */
		 U32    chd_version;    /* Version                      */
		 U8     ckhd_date[32],  /* 'Lex' File Date of Creation  */
			ckhd_name[16];  /* 'Lex' File Name              */
		 U16    ckhd_version;   /* 'Lex' File Version           */
		};

struct OBJ_INFO {
		 LVA    cin_begin,      /* VAC - BEGIN ATLAS            */
			cin_start,      /* VAC - First Executable       */
			cin_sttab,      /* VAC - Entry Point Table      */
			cin_satab,      /* VAC - Assignment Table       */
			cin_ddtab;      /* VAC - Device Table           */

		 U32    cin_symtab;     /* VAW - Symbol Table           */

		 LVA    cin_fopn,       /* VAC - File Open  Procedure   */
			cin_fcls;       /* VAC - File Close Procedure   */

					/* Named cin_rbase in Compiler  */
					/* Now Used to Indicate Format  */
					/* of From / To Table           */

		 short  cin_fttfmt;     /* From / To Table Format       */

		 SVA    cin_dadd_max,   /* VAD - Maximum Global         */
			cin_GO,         /* VAD - GO                     */
			cin_HI,         /* VAD - HI                     */
			cin_LO,         /* VAD - LO                     */
			cin_MANINT,     /* VAD - MANUAL INTERVENTION    */
			cin_TL,         /* VAD - TIME LIMIT             */
			cin_MT,         /* VAD - MAX TIME               */
			cin_M,          /* VAD - MEASUREMENT            */
			cin_UL,         /* VAD - UPPER LIMIT            */
			cin_LL,         /* VAD - LOWER LIMIT            */
			cin_NOM,        /* VAD - NOMINAL                */
			cin_DT,         /* VAD - DATE TIME              */
			cin_TMP;        /* VAD - ????                   */

		 S16    cin_comp_cnt,   /* Compilation Count            */
			cin_flow_cnt,   /* Flow Count                   */
			cin_alloc_cnt,  /* Allocation Count             */
			cin_lnk_cnt,    /* Linker Count                 */
			cin_cal_file;   /* Flag - Calibration File      */

		 SVA    cin_T,          /* VAD - TRUE                   */
			cin_F,          /* VAD - FALSE                  */
			cin_DC_dadd,    /* VAD - DIM                    */
			cin_DCK_dadd;   /* VAD - COND                   */

		 S16    cin_spares[1];  /* SPARE                        */

		 LVA    cin_bsav;       /* VAC - BEGIN ATLAS Save       */

		 LVA    cin_path_vad;   /* VAD of PATHINFO Array        */

		 union  {
			 SVA    cin_ary[10];/* Miscellaneous Usage      */
			 struct {
				 SVA    cin_SECURE;     /* (HUGHES) [ 0]*/
				 SVA    cin_NOSTEP;     /* (HUGHES) [ 1]*/
				 SVA    cin_SYSTIME;    /* (HUGHES) [ 2]*/
				 short  cin_R345;       /*          [ 3]*/
				 SVA    cin_UUTSNO;     /* (MARTIN) [ 4]*/
				 SVA    cin_CRITSEQ;    /* (MARTIN) [ 5]*/
				 SVA    cin_STNACT;     /* (DATSA)  [ 6]*/
				 SVA    cin_BUSERR;     /* (GDE)    [ 7]*/
				 SVA    cin_BUSSTS;     /* (GDE)    [ 8]*/
				 short  cin_UNUSED;     /*          [ 9]*/
				} cin_itm;
			} cin_msc;
		};

/*
 *      A Single Page is Allocated for both 
 *      the STAT_TAB & SADD_TAB Structures
 */
struct STAT_TAB {
		 U16    cst_cnt;                /* Number of entries    */
		 LVA    cst_sva;                /* VAC - SADD_TAB       */
		 U32    cst_stn[126];           /* Entry <statno>       */
		};

struct SADD_TAB {
		 LVA    cst_vlc[126];           /* VAC of Entry points  */
		};

struct DEV_ATAB {                               /* Assignment Table     */
		 U16    ddid;                   /* Left byte -  Channel */
		};                              /* Right byte - Index   */

struct DEV_DTAB {                               /* Device Table         */
		 U16    dncnt,                  /* Number of Entry's    */
			dnsiz;                  /* Size of Entry        */
		};

struct OBJ_STM  {
		 LVA    c_slink;        /* VAC - Next Node              */
		 U16    c_scode;        /* CODE ++ Flags                */
		 LVA    c_next,         /* VAC - Next STM node          */
			c_prev;         /* VAC - Previous STM node      */
		 U32    c_statnf;       /* Statement # ++ Flags         */
		 LVA    c_srctext,      /* VAC - Source Statement       */
			c_sigdef,       /* VAS - Signal Definition      */
			c_dstat;        /* VAC - Deleted Statement      */
		 U16    c_verb,         /* VERB code                    */
			c_line,         /* Source Line #                */
			c_scope;        /* Symbol table scope           */
		 U16    c_mea,          /* MCHAR Code                   */
			c_datbas,       /* Base Page of DATA            */
			c_noundim;      /* Noun ++ Dim Codes            */
		};

struct OBJ_EXN  {
		 LVA    c_link;         /* Next node                    */
		 U16    c_code;         /* CODE ++ FLAGS                */
		 union  {
			 struct {
				 U16    c_res,
					c_resq;
				} sres;
			 U32    c_lres;
			} res;
		 union  {
			 struct {
				 U16    c_op1,
					c_op2;
				} sop;
			 U32    c_lop12;
			} op;
		 union  {
			 struct {
				 U16    c_op3,
					c_op4;
				} sopx;
			 U32    c_lop34;
			} opx;
		}; 


/*
 *      <file>.DAT Data Structures
 */

/*
 *      Header
 */
struct DAT_HEAD {
		 LVA    dth_info;       /* VAD - 'Info' UNUSED          */
		 U8     dth_date[32],   /* Date of Creation             */
			dth_name[16];   /* ATLAS Program Name           */
		 U32    dth_version;    /* Version                      */
		 U8     dtk_date[32],   /* 'Lex' File Date of Creation  */
			dtk_name[16];   /* 'Lex' File Name              */
		 S16    dtk_version;    /* 'Lex' File Version           */
		};



#define FCBUFSIZ	486		/* Max FCB buffer size(bytes)	*/
#define FCBUFOFF	11 		/* Offset to FCB buffer(words)  */
/*
 *      A Data Item
 */
struct DAT_ITM  {
		 U16    d_sztyp;
		 union  {
			 S16    d_bval;         /* Boolean      */

			 S32    d_ival;         /* Integer      */

			 FLT    d_fval;         /* Float        */

			 LVA    d_aval;         /* Pointer      */

			 struct {               /* Switch path  */
				 LVA    d_pth_nxt;
				 U16    d_pth_dev;
				 U16    d_pth_swx[16];
				} d_pth;

			 struct {               /* Macro Path   */
				 U16    d_mpth_cnt;
				 struct {
					 S16 d_mpth_blk;
					 U16 d_mpth_fno,
					     d_mpth_pno;
					} d_mpth_swx[16];
				} d_mpth;

			 U8     d_cval[512];    /* Text         */

			 U16    d_zval[32];     /* Digital Pin's*/
			 
			 U16    d_dval[16];     /* Digital      */

			 struct {               /* FCB          */
				 LVA    d_fname;/* VAD Filename */
				 S16    d_faxc, /* Access Code  */
					d_frsz, /* Record Size  */
					d_fpsz, /* Page Size    */
					d_ffsz, /* File Size    */
					d_fdsc, /* File Descrip */
					d_frix, /* Record Index */
					d_fxxx, /* Spare        */
					d_fyyy; /* Spare        */
				 U8     d_fbuf[FCBUFSIZ];    /* Max  */
				} d_fcb;
			 
			 struct {               /* Conn         */
				 U16    d_xval; /* Value        */
				 U8     d_xnam[33];
				} d_con;

			 struct {		     /* Conn - rts89 	*/
				 U16	d_x89val;    /* Value	   	*/
				 LVA	d_x89rvad;   /* Root VAD	*/
				 U16	d_x89ord;    /* Ordinal	    	*/
				 U8	d_x89nam[33];
				} d_con89;

			 struct {
				 LVA    d_qfttvad;      /* <from> / <to>*/

				 struct {
					 LVA    d_qfbvad,   /* VAD - FromB  */
						d_qfxvad,   /* VAD - FromX  */
						d_qtbvad,   /* VAD - ToB    */
						d_qtxvad;   /* VAD - ToX    */
					} d_ftvad[32];
				} d_qftt;

			 struct {
				 S16    d_tfrom,        /* From Conn Ident  */
					d_tto;          /* To   Conn Ident  */
				 LVA    d_tpvad;        /* VAD - Path Entry */
				} d_ttt[64];

			} d_val;

		};

/*
 *	File Header for 89 Binary Files
 */
struct BINFILHDR  {
	U32	bf_magic;	/* Magic Number	    */
	U32	bf_size;	/* File Size(Bytes) */
};
#define BF_MAGIC	0xABCD4567L
#define BF_HDRSIZ	((int) sizeof(struct BINFILHDR))

#define D_bval		d_val.d_bval
#define D_cval		d_val.d_cval
#define D_dval		d_val.d_dval
#define D_ival		d_val.d_ival
#define D_fval		d_val.d_fval
#define D_x89val	d_val.d_con89.d_x89val 
#define D_x89rvad	d_val.d_con89.d_x89rvad 
#define D_x89ord	d_val.d_con89.d_x89ord 
#define D_x89nam	d_val.d_con89.d_x89nam

#define IsTTYPE(x)	(DType((x).d_sztyp) == TTYPE)
#define IsDTYPE(x)	(DType((x).d_sztyp) == DTYPE)
#define IsITYPE(x)	(DType((x).d_sztyp) == ITYPE)
#define IsRTYPE(x)	(DType((x).d_sztyp) == RTYPE)

/*
 *      The 'Lex' File Data Structure
 */
struct LEX_HEAD {      
		 LVA    khd_info;               /* VAL - 'Info'         */
		 U8     khd_date[32],           /* Date of Creation     */
			khd_name[16];           /* 'Lex' File Name      */
		 U16    khd_version;            /* 'Lex' File Version   */
		};

struct LEX_INFO {
		 U32    kin_symbol;             /* Symbol Table         */
		 SVA    kin_tlink[4];           /* VAL - Local Usage    */
		 U16    kin_item[K_SIZ];        /* Special Tokens etc   */
		};
/*
 *	The Table of Screen fields
 */
struct FIELDS	{
		U16 row,                /* The screen row                    */
		    col1,               /* The screen field beginning column */
		    col2;               /* The screen field ending column    */
		};


#endif /* _STRUCTS_H_ */
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



struct V_buf                                    /* Control Blocks       */
	{
	 struct V_buf
		*v_nxt,                         /* Next                 */
		*v_pre;                         /* No longer used !!!!! */
	 U16    v_mlk,                          /* Modified + Locked    */
		v_vid;                          /* Identifier           */
	 S16    *v_dat;                         /* Data Address         */
	};

/*
 *          o   VABOF       Number of Bits in Offset
 *          o   VPSZW       Virtual Page Size Words (16 Bit)
 *          o   VPSZB       Virtual Page Size Bytes
 *          o   VOFX        Virtual address Offset Mask
 *          o   VPGX        Virtual address Page Mask
 *          o   VIDN        Virtual Identifier Bit Shift
 *          o   VMOD        Virtual Page Modified Flag
 *          o   VLKX        Virtual Page Locked Mask
 *          o   VMvid()     Build a Virtual page Identifier
 *          o   VMofst()    Extract Offset From Virtual Address
 *          o   VMmap()     Map Virtual To Real Address
 *          o   C_Map()     Map Virtual CODE Address
 *          o   D_Map()     Map Virtual DATA Address
 *          o   L_Map()     Map Virtual LEX  Address
 *          o   M_Map()     Map Virtual MESG Address
 *          o   VMmod()     Flag Most Recently Used Page as Modified
 *          o   VMlok()     Lock   Virtual page
 *          o   VMulk()     UnLock Virtual page
 *          o   GetLong()   Get LONG   Value
 *          o   GetDouble() Get DOUBLE Value
 *          o   PutLong()   Put LONG   Value
 *          o   PutDouble() Put DOUBLE Value
 *
 */


#ifndef LINT
#define VPSZW   (1 << VABOF)
#else
#define VPSZW   2
#endif

#define VPSZB   (VPSZW * sizeof (S16))

/*
 *      N.B.    Maximum 24 Bit Word Addressing Assumed  (Max 16MWord)
 */
#define VPGX    (0XFFFFFFL << VABOF)
#define VOFX    (~ VPGX)

#define VIDN    (VABOF - 4)
#define VMvid(s, v)     (((U16) (((v) & VPGX) >> VIDN)) | ((U16) (s)))

#define VMOD    (U16) 0100000                   /* Modified             */
#define VLKX    (U16) 0077777                   /* Lock Mask            */

#define VMofst(v)       ((S16) ((v) & VOFX))

#ifdef  FAST
#define VMmap(vid)      (((Vid = vid) == v_mru->v_vid) ?        \
				v_mru->v_dat :                  \
				v_map(Vid))

#define C_Map(va)       (VMmap(VMvid(OBJSPAC , va)) + VMofst(va))
#define D_Map(va)       (VMmap(VMvid(DATASPAC, va)) + VMofst(va))
#define L_Map(va)       (VMmap(VMvid(LEXSPAC , va)) + VMofst(va))
#define M_Map(va)       (VMmap(VMvid(MSGSPAC , va)) + VMofst(va))
#else
#define C_Map(va)       (v_map(VMvid(OBJSPAC , va)) + VMofst(va))
#define D_Map(va)       (v_map(VMvid(DATASPAC, va)) + VMofst(va))
#define L_Map(va)       (v_map(VMvid(LEXSPAC , va)) + VMofst(va))
#define M_Map(va)       (v_map(VMvid(MSGSPAC , va)) + VMofst(va))
#endif

#define VMmod           v_mru->v_mlk |= VMOD
#define VMlok(v)        (v)->v_mlk++
#define VMulk(v)        (v)->v_mlk--

#ifdef  LALIGN
#define GetLong(p)      getxlong(p)
#define GetReal(p)      getxdouble(p)
#define PutLong(t, v)   putxlong(t, (S16 *) (&(v)))
#define PutReal(t, v)   putxdouble(t, (S16 *) (&(v)))
#else
#define GetLong(p)      *((long *) (p))
#define GetReal(p)      *((double *) (p))
#define PutLong(t, v)   *((long *) (t)) = (v)
#define PutReal(t, v)   *((double *) (t)) = (v)
#endif

