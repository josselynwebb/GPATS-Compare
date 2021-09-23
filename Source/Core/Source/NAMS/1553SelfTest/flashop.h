/*	Data Device Corporation
 *	105 Wilbur Place
 *	Bohemia N.Y. 11716
 *	(631) 567-5600
 *
 *		ENHANCED MINI-ACE 'C' Run Time Library
 *
 *			Copyright (c) 2003 by Data Device Corporation
 *			All Rights Reserved.
 *
 *	flashop.h (FLASH memory operations)
 *
 *	Created 4/29/03 KWH
 *
 */

#ifndef __FLASHOP_H__
#define __FLASHOP_H__

/* FLASH status patterns. */		/* PUT THESE IN ERRDEF.H !!!	kwh */
#define FLASH_STATUS_INVALID_OP		0x20000	/* Invalid op code */
#define FLASH_STATUS_WRITE_FAIL		0x30000	/* FLASH write failure */
#define FLASH_STATUS_ERASE_FAIL		0x40000	/* Sector erase failure */
#define FLASH_STATUS_PROTECTED		0x50000	/* Address protected */

/* MIA region protection options. */
#define FLASH_UNPROT		1		/* MIA data unprotected */
#define FLASH_PROT			0		/* MIA data protected */

/* M' Auto-Init initiation and termination patterns. */
#define SAI1		0xFEED	/* Start Auto-init patterns */
#define SAI2		0xDEAD
#define SAI3		0xBEEF
#define EAI			0xC0DE	/* End Auto-Init word. */

/*-----------------------------------------------------------------------
Name:	aceFlashWrite

Description:
	This function writes the passed in data 
	word to the passed in FLASH memory address.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed
In		dwAddr = address to write
In		wData  = word to write
Out		return = error condition
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceFlashWrite(S16BIT DevNum, U32BIT dwAddr, U16BIT wData);

/*-----------------------------------------------------------------------
Name:	aceFlashRead

Description:
	This function reads the data word from 
	the passed in FLASH memory address.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed
In		dwAddr = address to read
Out		pwData = word pointer for read data
Out		return = error condition
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceFlashRead(S16BIT DevNum, U32BIT dwAddr, U16BIT *pwData);

/*-----------------------------------------------------------------------
Name:	aceFlashErase

Description:
	This function erases the sector that 
	contains the passed in FLASH memory address.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed
In		dwAddr = sector containing address to erase
Out		return = error condition
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceFlashErase(S16BIT DevNum, U32BIT dwAddr);

/*-----------------------------------------------------------------------
Name:	aceFlashProtect

Description:
	This function sets the protection status of the M' Auto-Init portion 
	of the FLASH memory, on sector boundries.  This region is protected
	following a successful MAI.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed
In		bProt  = protection setting
Out		return = error condition
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceFlashProtect(S16BIT DevNum, U8BIT bProt);

_EXTERN S16BIT _DECL flexFlashRead(S16BIT DevNum, U32BIT dwAddr, U16BIT *pwData);

_EXTERN S16BIT _DECL flexFlashWrite(S16BIT DevNum, U32BIT dwAddr, U16BIT wData);

_EXTERN S16BIT _DECL flexFlashErase(S16BIT DevNum, U32BIT dwAddr);


#endif /* __FLASHOP_H__ */


