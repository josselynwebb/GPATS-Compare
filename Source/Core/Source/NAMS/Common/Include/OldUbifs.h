#ifndef	_OLD_UBIFS_H_
#define	_OLD_UBIFS_H_

//
//  This file is used for distribution in "<usr>\tyx\include" only.
//	

#include "Typedef.h"

#ifdef	__cplusplus
extern "C" {
#endif	// __cplusplus

// for backward compatibility
typedef struct DAT_ITM  {
	TWord		d_sztyp;
	union {
		TWord	d_bval;				/* Boolean      */
		TLong	d_ival;				/* Integer      */
		TDouble	d_fval;				/* Float        */
		char	d_cval[512];		/* Text         */
		TWord	d_dval[32];			/* Digital      */
	} d_val;
} TDatItm, *PDatItm;

void mov_dfv (PDatItm pTo, TLong vad);
void mov_dtv (TLong vad, PDatItm pFrom);

#define U8		TByte
#define S16		TShort
#define U16		TWord
#define S32		TLong
#define U32		TULong
#define REAL	float
#define FLT		TDouble
#define SVA		TWord		/* Short Virtual Address */
#define LVA		TLong		/* Long Virtual Address  */

#define	DType(x)	((x) & 0x0f)
#define	DSubT(x)	(((x) >> 4) & 0x0f)
#define	DSize(x)	(((TWord) x) >> 8)

#define	D_bval		d_val.d_bval
#define	D_cval		d_val.d_cval
#define	D_dval		d_val.d_dval
#define	D_fval		d_val.d_fval
#define	D_ival		d_val.d_ival

#define	IsDTYPE(x)	(DType((x).d_sztyp) == DTYPE)
#define	IsITYPE(x)	(DType((x).d_sztyp) == ITYPE)
#define	IsRTYPE(x)	(DType((x).d_sztyp) == RTYPE)
#define	IsTTYPE(x)	(DType((x).d_sztyp) == TTYPE)

#define	GetArgVad(n)	pArgs[(n)-1]

#ifdef	__cplusplus
}	// extern "C"
#endif


#endif	// _OLD_UBIFS_H_