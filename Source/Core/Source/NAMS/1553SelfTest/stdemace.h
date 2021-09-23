/*	Data Device Corporation
 *	105 Wilbur Place
 *	Bohemia N.Y. 11716
 *	(631) 567-5600
 *
 *		ENHANCED MINI-ACE 'C' Run Time Software Library
 *
 *			Copyright (c) 1999 by Data Device Corporation
 *			All Rights Reserved.
 *
 *
 *	stdmp.h (Application header file)
 *
 *	This header file contains all necessary information to access
 *	type definitions and RTL function prototypes.
 *
 *	Created 8/26/99 DL
 *	Added Discrete IO and FLASH includes 6/18/03 KWH
 *
 */

#ifndef __STDMP_H__
#define __STDMP_H__


/* Windows Declarations */
#ifdef WIN32
#include <windows.h>
#define _DECL	WINAPI
#define _EXTERN	

/* Non Windows Declarations */
#else					
#define _DECL
#define _EXTERN	

#ifdef _MSC_VER
#define getvect _dos_getvect
#define setvect _dos_setvect
#define disable _disable
#define enable  _enable
#endif

#endif

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/* RTL is written in 'C' */
#ifdef __cplusplus
extern "C" {
#endif


/* 32/16/8-bit type declarators */
#define U32BIT	unsigned long
#define S32BIT	long
#define U16BIT	unsigned short
#define S16BIT	short
#define U8BIT	unsigned char
#define S8BIT	char


/* Define boolean variable types */
#if (defined(DOS) || defined(LINUX) || defined(INTEGRITY))
#define BOOL unsigned char
#endif

#if defined(INTEGRITY)
#define DWORD	unsigned long
#define WORD	unsigned short
#define BOOLEAN unsigned char
#endif

#ifndef FALSE
#define FALSE	0
#endif
#ifndef TRUE
#define TRUE	1
#endif

#define MAX_NUM_OF_DEVICES	32

/* define card types */
#define ACE_CARD_6555X	       0 /* The card is type ACE PCMCIA */
#define ACE_CARD_65549	       1 /* The card is type ACE PCI */
#define ACE_CARD_655XX	       2 /* The card is type Enhanced Mini-ACE */
#define ACE_CARD_65565	       3 /* The card is type EMACE PMC/PCI */
#define ACE_CARD_6555X_PAGED   4 /* The card is type ACE PCMCIA PAGED */
#define ACE_CARD_65569         6 /* The card is type EMACE PCI */
#define ACE_CARD_65567	       7 /* The card is type EMACE PC/104 (4k) */
#define ACE_CARD_65568	       8 /* The card is type EMACE PC/104 (64k) */
#define ACE_CARD_65566	       9 /* The card is type EMACE PCI66 */
#define ACE_CARD_6558x        10 /* The card is type EMACE EBR */
#define ACE_CARD_FLEX         11 /* The card is type EMACE Flexcore */

/* define paging mechinisms */
#define ACE_65553_PAGING	   1 /* Indicates that the card is using 65553 paging */
#define ACE_65568_PAGING	   2 /* Indicates that the card is using 65568 paging */
#define ACE_6558x_PAGING       3 /* Indicates that the card is using 6558x paging */

/* Include all RTL header files necessary for external compiles */
#include "errordef.h"
#include "msgop.h"
#include "config.h"
#include "testop.h"
#include "mtop.h"
#include "rtop.h"
#include "rtmtop.h"
#include "bcop.h"
#include "dioop.h"
#include "flashop.h"

#ifdef __cplusplus
}
#endif


#endif


