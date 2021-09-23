
/**
*
*  BTI1553 DRIVER EXAMPLE 3  (08/12/2015)
*  Copyright (c) 2001-2015
*  Ballard Technology, Inc.
*  www.ballardtech.com
*  support@ballardtech.com
*  ALL RIGHTS RESERVED
*
*  NAME:   EXAMP3.C -- Simulating an RT
*
**/

/**
*
*  This example configures Card Number 0 as an RT to receive
*  the BC-RT data transmitted by the bus controller in
*  Example 2.
*
*  It continuously receives and displays the data words.
*
**/

#include "BTICard.H"
#include "BTI1553.H"
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <Windows.h>

extern void RTRead1553(int indx);
extern void BCWrite1553(int indx);

#define	CARDNUM		  0
#define	MAX_CORES     4
#define	MAX_CHANNELS  32
#define TERMADDR	1
#define SUBADDR		2

HCARD   hCard;					/*Handle to card*/
HCORE   hCore;					/*Handle to core*/
ERRVAL  errval;					/*Error return value*/
INT     corenum;				/*Core number*/
INT     channum;				/*Channel number*/
int msgindx = 0;

MSGADDR RTMsg;
MSGADDR RTXmt;
USHORT  Data[32];

void examplemain(void)
{

	printf("\n    **********************************************************************");
	printf("\n    *                                                                    *");
	printf("\n    *  EXAMP3  (08/12/2015)                                              *");
	printf("\n    *  Copyright 2001-2015  Ballard Technology, Inc.  Everett, WA, USA.  *");
	printf("\n    *  All rights reserved.                                              *");
	printf("\n    *  Go to www.ballardtech.com or email support@ballardtech.com        *");
	printf("\n    *                                                                    *");
	printf("\n    *  BTI1553 Example 3                                                 *");
	printf("\n    *  \"Simulating an RT example.\"                                       *");
	printf("\n    *                                                                    *");
	printf("\n    **********************************************************************");
	printf("\n");

/**
*
*  Open the card with the specified card number.
*  An error value is returned which is tested
*  to determine if an error occurred.
*
**/

	errval = BTICard_CardOpen(&hCard,CARDNUM);

	if (errval)
	{
		printf("\nError:  Either card number %u is not present, or",CARDNUM);
		printf("\n        an error occurred (%i) opening the card.",errval);
		printf("\n        (%s)",BTICard_ErrDescStr(errval,hCard));
		printf("\n");
		exit(1);
	}

	printf("\nUsing BTI1553 card #%d",CARDNUM);

/**
*
*  Find the first MIL-STD-1553 core and channel.
*
**/

	for (corenum=0;corenum<MAX_CORES;corenum++)
	{
		errval = BTICard_CoreOpen(&hCore,corenum,hCard);
		if (errval) break;

		for (channum=0;channum<MAX_CHANNELS;channum++)
			if (BTI1553_ChIs1553(channum,hCore)) break;

		if (channum != MAX_CHANNELS) break;
	}

	if (errval || corenum == MAX_CORES || channum == MAX_CHANNELS)
	{
		printf("\nError:  No MIL-STD-1553 channels present in cardnum %d.",CARDNUM);
		printf("\n");
		BTICard_CardClose(hCard);
		exit(1);
	}

	printf("\nUsing MIL-STD-1553 core #%d",corenum);
	printf("\nUsing MIL-STD-1553 channel #%d",channum);
	printf("\n");

/**
*
*  Configure the channel for remote terminal mode.
*
**/

	BTICard_CardReset(hCore);

	errval = BTI1553_RTConfig(RTCFG1553_DEFAULT,TERMADDR,channum,hCore);

	if (errval < 0)
	{
		printf("\nError:  An error was encountered (%i) while configuring for",errval);
		printf("\n        remote terminal mode on channel #%d on core #%d.",channum,corenum);
		printf("\n        (%s)",BTICard_ErrDescStr(errval,hCard));
		printf("\n");
		BTICard_CardClose(hCard);
		exit(1);
	}

/**
*
*  Get the message address for a receive subaddress.
*
**/

	RTMsg = BTI1553_RTGetMsg(SUBADDRESS,TERMADDR,RCV,SUBADDR,channum,hCore);
	RTXmt = BTI1553_RTGetMsg(SUBADDRESS,TERMADDR,XMT,SUBADDR,channum,hCore);


/**
*
*  Start the card to begin responding to transmissions.
*
**/

	BTICard_CardStart(hCore);

/**
*
*  Loop until a key is hit.
*
**/
	printf("\nThis is the RT Version");
wrongentry:
	printf("\nEnter # (0-9) of errors to inssert into data\n");

	int numberoferrors = 0;
	while( numberoferrors = !_kbhit());
	numberoferrors = _getch();
	if( numberoferrors >= '0' && numberoferrors <= '9')
	{
		numberoferrors -= '0';
	}
	else
	{
		printf("\rNice Try, Try again....\n");
		goto wrongentry;
	}
		printf("\n%d error words inserted\n",numberoferrors);

	printf("\nPress any key to exit....\n\n");
	ULONG ii = 0;

	int indx = 0;
	while (!_kbhit())
	{
		int j = 0;
		int xmtcmdword = BTI1553_MsgFieldRd( FIELD1553_CWD1, RTXmt, hCore);
		int rcvcmdword = BTI1553_MsgFieldRd( FIELD1553_CWD1, RTMsg, hCore);
		int xmterrorword = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTXmt, hCore);
		int rcverrorword = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTMsg, hCore);
		if( (rcverrorword & 1) == 0 && (rcverrorword & 1) == 0 )
		{
			Sleep(100);
			continue;
		}
		ii = BTI1553_MsgFieldRd( FIELD1553_CWD1, RTMsg, hCore);
		ii = BTI1553_MsgFieldWr( 0, FIELD1553_ERROR, RTMsg, hCore);
		int datacount = BTI1553_MsgFieldRd( FIELD1553_COUNT, RTMsg, hCore);
		int cmdword = BTI1553_MsgFieldRd( FIELD1553_CWD1, RTMsg, hCore);
		xmtcmdword = BTI1553_MsgFieldRd( FIELD1553_CWD1, RTXmt, hCore);
		if( (xmtcmdword & 0x0400) != 0 )
		{
			BTI1553_MsgDataWr(Data,32,RTXmt,hCore);	/*Write data received from the BC*/
		}
		else
		{
			BTI1553_MsgDataRd(Data,32,RTMsg,hCore);	/*Read data received from the BC*/
		}
		if( (xmtcmdword & 0x0400) == 0 )
		{
			printf("\nRCV %04d %02d %04X {%04X %04X %04X %04X %04X %04X %04X %04X %04X %04X",
					++indx, datacount, xmtcmdword == 0 ? rcvcmdword : xmtcmdword,
					Data[0],Data[1],Data[2],Data[3],Data[4],Data[5],Data[6],Data[7],Data[8],Data[9]);
		}
		for( int i = 0; i < numberoferrors; i++)
			Data[i] += 1;
		numberoferrors = 0;
		if( (xmtcmdword & 0x0400) != 0 )
		{
			printf("\nXMT %04d %02d %04X {%04X %04X %04X %04X %04X %04X %04X %04X %04X %04X",
				++indx, datacount, xmtcmdword == 0 ? rcvcmdword : xmtcmdword,
				Data[0],Data[1],Data[2],Data[3],Data[4],Data[5],Data[6],Data[7],Data[8],Data[9]);
		}
		ii = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTXmt, hCore);
		BTI1553_MsgDataWr(Data,32,RTXmt,hCore);	/*Write data received from the BC*/
		ii = BTI1553_MsgFieldWr( 0, FIELD1553_ERROR, RTMsg, hCore);
		if( (rcvcmdword & 0x0400) != 0 || (xmtcmdword & 0x0400) != 0)
		{
			BTI1553_MsgDataWr(Data,32,RTXmt,hCore);	/*Write data received from the BC*/
			ii = BTI1553_MsgFieldWr( 0, FIELD1553_ERROR, RTMsg, hCore);
			for( j = 0; j < 1000; j++ )
			{
				ii = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTXmt, hCore);
				if( (ii & 1) == 0 )
				{
					Sleep(100);
					continue;
				}
				break;
			}
		}
		ii = BTI1553_MsgFieldWr( 0, FIELD1553_ERROR, RTMsg, hCore);
		ii = BTI1553_MsgFieldWr( 0, FIELD1553_CWD1, RTMsg, hCore);
		ii = BTI1553_MsgFieldWr( 0, FIELD1553_ERROR, RTXmt, hCore);
		ii = BTI1553_MsgFieldWr( 0, FIELD1553_CWD1, RTXmt, hCore);
		Sleep(100);
	}

/**
*
*  Stop the card.
*
**/

	BTICard_CardStop(hCore);

/**
*
*  The card MUST be closed before exiting the program.
*
**/

	BTICard_CardClose(hCard);
}

void RTRead1553(int indx)
{
		int ii = 0;
		int j = 0;
		BTI1553_MsgDataRd(Data,32,RTMsg,hCore);	/*Read data received from the BC*/
		ii = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTXmt, hCore);
		ii = ii & 1;
		while( ii == 0)
		{
			BTI1553_MsgDataRd(Data,32,RTMsg,hCore);	/*Read data received from the BC*/
			ii = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTMsg, hCore);

			ii = ii & 1;
			if( ii == 0 )
			{
				Sleep(100);
				continue;
			}
			break;
		}
		ii = BTI1553_MsgFieldWr( 0, FIELD1553_ERROR, RTMsg, hCore);
		int datacount = BTI1553_MsgFieldRd( FIELD1553_COUNT, RTMsg, hCore);
		int cmdword = BTI1553_MsgFieldRd( FIELD1553_CWD1, RTMsg, hCore);
		printf("\r%04d %02d %04X {%04X %04X %04X %04X %04X %04X %04X %04X %04X %04X",
				++msgindx, datacount, cmdword,
				Data[0],Data[1],Data[2],Data[3],Data[4],Data[5],Data[6],Data[7],Data[8],Data[9]);
		Data[0] += 1;
		Data[1] += 1;
		Data[2] += 1;
		ii = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTXmt, hCore);
		BTI1553_MsgDataWr(Data,32,RTXmt,hCore);	/*Read data received from the BC*/
		while( (ii & 1) == 0 )
		{
			ii = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTXmt, hCore);
			if( (ii & 1) == 0 )
			{
				Sleep(100);
				continue;
			}
			break;
		}
		Sleep(100);
}
