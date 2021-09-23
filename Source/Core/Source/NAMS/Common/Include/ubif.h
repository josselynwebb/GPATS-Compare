#ifndef	_USR_BIF_H_
#define	_USR_BIF_H_
//
//  This file is used for distribution in "<usr>\tyx\include" only.
//	

#include "typedef.h"

// max length of Text and Dig Item
#define	MAX_TXT			512		// 512 char maximum
#define	MAX_DIG			32		// 32 words (512 bits max)

// return value from user bif
#define	BRSOK			0		// ok
#define	BRSERR			-1		// error
#define	BRSUNKNOWN		999		// unknown bif

// first user bif number
#define BIF_USER		128

// boolean values
#define	BTrue	((TBool) 0xffff)
#define	BFalse	((TBool) 0)

// calculate the number of words needed to hold n Bits of
// digital data
#define	NUM_WORD(nBits)		(((nBits) + 15) / 16)

// data types
#define	NTYPE	0	// boolean
#define	ITYPE	1	// integer (TLong)
#define	RTYPE	2	// real (TDouble)
#define	TTYPE	4	// text
#define	DTYPE	7	// digital data

#ifdef	__cplusplus
extern "C" {
#endif	// __cplusplus

// (1) Data Access

int rtsGetDataSize (TLong vad);	// return -1 if failed
int rtsGetDataType (TLong vad);	// return -1 if failed

int rtsGetLength (TLong vad);	// get the declared length
								// of:
								//		dig ==> in BITS
								//		txt ==> in chars
								//		other: ==> -1

// set fncrtn
void rtsSetFncRtn (int nFncRtn);

TBool rtsGetBool (TLong vad);
void rtsSetBool (TLong vad, TBool bval);	

TLong rtsGetInteger (TLong vad);	
void rtsSetInteger (TLong vad, TLong ival);

TDouble rtsGetDecimal (TLong vad);
void rtsSetDecimal (TLong vad, TDouble rval);	

// GetText return the length of the string, the returned
// text is in rTxt. (rTxt must be declared to be at least 
// nMax chars)
int rtsGetText (TLong vad, PChar prTxt, int nMax /* = MAX_TXT */);
void rtsSetText (TLong vad, PCChar pTxt);	// pTxt must be 0 terminated

// GetDig return the # of Words of the digital data, the returned
// digital data is in rDig. (rDig must be at least nMax WORDS)
int rtsGetDig (TLong vad, PWord prDig, int nMax);
void rtsSetDig (TLong vad, PCWord pDig, int nWords);

// (2) User Interface
void rtsDisplay (PCChar pMsg);			// display a message in message window
void rtsLog (PCChar pMsg);				// log a message in the log file
int rtsGetInput (PCChar pPrompt, PChar prTxt, int nMax);	// ask user input with prompt

// (3) flush the internal buffer of RTS, return -1 if error
int rtsFlush ();

// (4) The Function User must provide
int UserBif (int nFnc, int nArgs, PCLong pArgs);

#ifdef	__cplusplus
}	// extern "C"
#endif

// (4) for backward compatiblity (untyped access)
#include "OldUbifs.h"

#endif	// _USR_BIF_H_