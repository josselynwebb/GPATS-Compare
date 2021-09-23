/*	Data Device Corporation
 *	105 Wilbur Place
 *	Bohemia N.Y. 11716
 *	(631) 567-5600
 *
 *		ENHANCED MINI-ACE 'C' Run Time Library
 *
 *			Copyright (c) 1999 by Data Device Corporation
 *			All Rights Reserved.
 *
 *	Dioop.c (Discrete IO operations module)
 *
 *	Created 11/13/02 KWH
 *
 * Programmers Note:
 *  The discrete IO ports associated with a particular logical device 
 *  are also associated with up to  four other devices.  These ports 
 *  are common to cards that have up to four Enhanced Mini-ACE chips 
 *  on them.  This must be taken into consideration when working with 
 *  multiple channels on the same card while manipulating the ports.
 */

#ifndef __DIOOP_H__
#define __DIOOP_H__

/* These defines are for the calls that operate on individual ports. */
/* The individual port references. */
#define DIO_1		1
#define DIO_2		2
#define DIO_3		3
#define DIO_4		4
#define DIO_5		5
#define DIO_6		6
#define DIO_7		7
#define DIO_8		8
#define DIO_9		9
#define DIO_10		10
#define DIO_11		11
#define DIO_12		12

/* Direction options. */
#define DIO_IN		0x0000
#define DIO_OUT		0x0100

/* State options. */
#define DIO_OFF		0
#define DIO_ON		1

/* Directive to read the direction or state of a port. */
#define DIO_READ	0xFFFF

/*-----------------------------------------------------------------------
Name:	aceDioDir

Description:
	This function defines or determnes the direction of one of the 
	discrete IO's.  Upon a read command, the direction value is 
	returned in the command parameter.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed
In		bPort  = port working with ( DIO_1 - DIO_8 )
In/Out		wCmd   = direction/read command (DIO_IN, DIO_OUT, DIO_READ)
Out		return = error condition
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceDioDir(S16BIT DevNum, U8BIT bPort, U16BIT *wCmd);

/*-----------------------------------------------------------------------
Name:	aceDioCtl

Description:
	This function defines or determnes the state of one of the 
	discrete IO's.  Upon a read command, the state value is 
	returned in the command parameter.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed
In		bPort  = port working with ( DIO_1 - DIO_8 )
In/Out		wCmd   = state/read command (DIO_OFF, DIO_ON, DIO_READ)
Out		return = error condition
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceDioCtl(S16BIT DevNum, U8BIT bPort, U16BIT *wCmd);

/*-----------------------------------------------------------------------
Name:	aceDioDirBits

Description:
	This function sets the direction of all IO ports on a card.
	A mask field is provided to select individual ports to operate on.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed
In/Out		bInOut = input / output state of all ports (1=OUTPUT)
In		bMask  = logic '1' at any bit position enables port write.
		         A value of 0x00 indicates that a read is to be done.
Out		return = error condition.
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceDioDirBits(S16BIT DevNum, U8BIT *bInOut, U8BIT bMask);

/*-----------------------------------------------------------------------
Name:	aceDioCtlBits

Description:
	This function sets or determines the state of all IO ports on a card.
	A mask field is provided to select individual ports to operate on.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed.
In/Out		bState = low / high state of all eight ports (1 = ON).
In		bMask  = logic '1' at any bit position enables port write.
		         A value of 0x00 indicates that a read is to be done.
In		bPort = Port number, this is only used for EBR cards.  Has no 
				meaning for PC/104 Cards.
Out		return = error condition.
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceDioCtlBits(S16BIT DevNum, U8BIT *bState, U8BIT bMask, U8BIT bPort);

/*-----------------------------------------------------------------------
Name:	aceDioFunction

Description:
	This function sets the mode that the DIO registers will operate -
	either DIO or HUB and DIO.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed.
In		wFunc = Function type DIO = 0, HUB = 1.
Out		return = error condition.
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceDioFunction(S16BIT DevNum, U16BIT wFunc);

/*-----------------------------------------------------------------------
Name:	aceSetHubPort

Description:
	This function sets address of the HUB.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed.
In		wPort = Hub port address.
Out		return = error condition.
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceSetHubPort(S16BIT DevNum, U16BIT wPort);

/*-----------------------------------------------------------------------
Name:	aceGetHubPort

Description:
	This function gets address of the HUB.

State: READY, RUN

In		DevNum = number associated with the hardware being accessed.
In/Out	wPort = Hub port address.
Out		return = error condition.
------------------------------------------------------------------------*/
_EXTERN S16BIT _DECL aceGetHubPort(S16BIT DevNum, U16BIT *wPort);

#endif /* __DIOOP_H__ */


