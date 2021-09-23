#ifndef	_NAM_H_INCLUDED_
#define	_NAM_H_INCLUDED_

//
//  This file is used for distribution in "<usr>\tyx\include" only.
//	

#include "typedef.h"

/* max length of Text and Dig Item */
#define	MAX_TXT			512		/* 512 char maximum			*/
#define	MAX_DIG			32		/* 32 words (512 bits max)	*/

/* boolean values */
#define	BTrue	((TBool) 0xffff)
#define	BFalse	((TBool) 0)

/* data types */
#define	NTYPE	0	/* boolean			*/
#define	ITYPE	1	/* integer (TLong)	*/
#define	RTYPE	2	/* real (TDouble)	*/
#define	TTYPE	4	/* text				*/
#define	DTYPE	7	/* digital data		*/

#define EXCP_VM_BASE	0x12345678
#define EXCP_VM_SET		EXCP_VM_BASE+1
#define EXCP_VM_GET		EXCP_VM_BASE+2

/************************************************************************/

#define DllExport	__declspec(dllexport)
// function assumed not to throw an exception but does
// __declspec(nothrow) or throw() was specified on the function
#pragma warning(disable:4297)	

#ifdef	__cplusplus
extern "C" {
#endif	/* __cplusplus */

int DllExport vmOpen (PCChar fname);
int DllExport vmClose ();
int DllExport vmCloseEx (char* fnameBuffer, unsigned int fnameBufferSize);

int DllExport vmGetDataSize (TLong vad);
int DllExport vmGetDataType (TLong vad);

TBool DllExport vmGetBool (TLong vad);
void DllExport vmSetBool (TLong vad, TBool bval);	

TLong DllExport vmGetInteger (TLong vad);	
void DllExport vmSetInteger (TLong vad, TLong ival);

TDouble DllExport vmGetDecimal (TLong vad);
void DllExport vmSetDecimal (TLong vad, TDouble rval);	

/* vmGetText return the length of the string, the returned
   text is in rTxt. (rTxt must be declared to be at least nMax chars) */
int DllExport vmGetText (TLong vad, PChar prTxt, int nMax);
/* pTxt must be 0 terminated	*/
void DllExport vmSetText (TLong vad, PCChar pTxt);

/* vmGetDig return the # of Words of digital data, the returned
   digital data is in rDig. (rDig must be at least nMax WORDS) */
int DllExport vmGetDig (TLong vad, PWord prDig, int nMax);
void DllExport vmSetDig (TLong vad, PCWord pDig, int nWords);

#ifdef	__cplusplus
}		/* extern "C" */
#endif

/************************************************************************/

#endif	/* _NAM_H_INCLUDED_ */