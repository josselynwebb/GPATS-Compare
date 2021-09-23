
/**
*
*  BTI1553 DRIVER EXAMPLE 5  (08/12/2015)
*  Copyright (c) 2001-2015
*  Ballard Technology, Inc.
*  www.ballardtech.com
*  support@ballardtech.com
*  ALL RIGHTS RESERVED
*
*  NAME:   EXAMP5.C -- Simultaneous BC, RT, and monitor simulation
*
**/

/**
*
*  This example requires a Ballard 1553 Level C Device or higher.
*  It configures Card Number 0 as a BC, an RT, and a Bus Monitor.
*  The BC transmits one BC-RT message (command word 0843H).
*  The RT is configured with terminal address 1, so the
*  simulated RT receives the simulated BC's transmissions.
*  The data words transmitted by the RT are incremented and
*  displayed.  The data words received by the RT are read and
*  also displayed.
*
**/

#include "BTICard.H"
#include "BTI1553.H"
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <Windows.h>
//#include <io.h>
//#include <WinBase.h>

#define	CARDNUM			0
#define	MAX_CORES		4
#define	MAX_CHANNELS	32
#define	DATASIZE10		10
#define	DATASIZE5		5

#define BCADDR			10
#define BCSUBADDR		1

XMITFIELDS1553 BCXmitMsg;

#define	FRAMETIME	100000			/*Frame time in microseconds*/
void fillxmtbuffer(void);
int numberoferrors = 0;

void examplemain(void)
{
	HCARD   hCard;					/*Handle to card*/
	HCORE   hCore;					/*Handle to core*/
	ERRVAL  errval;					/*Error return value*/
	INT     corenum;				/*Core number*/
	INT     channum;				/*Channel number*/
	INT i = 0;

	MSGADDR BCMsg;
	//MSGADDR RTMsg;
	//MSGADDR RTXmtMsg;
	USHORT  BCData[3] = {0,0,0};
	USHORT  RTData[3] = {0,0,0};

	//USHORT seqbuf[2048];
	//ULONG  seqcount;
	//ULONG  blkcnt;
	//LPSEQRECORD1553 pRec1553;
	//LPSEQRECORDMORE1553 pRecMore1553;
	//SEQFINDINFO sfinfo;

	printf("\n    **********************************************************************");
	printf("\n    *                                                                    *");
	printf("\n    *  EXAMP5  (08/12/2015)                                              *");
	printf("\n    *  Copyright 2001-2015  Ballard Technology, Inc.  Everett, WA, USA.  *");
	printf("\n    *  All rights reserved.                                              *");
	printf("\n    *  Go to www.ballardtech.com or email support@ballardtech.com        *");
	printf("\n    *                                                                    *");
	printf("\n    *  BTI1553 Example 5                                                 *");
	printf("\n    *  \"Simultaneous BC, RT, and monitor simulation example.\"            *");
	printf("\n    *                                                                    *");
	printf("\n    **********************************************************************");

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
*  Find the first MIL-STD-1553 core with level M, C or D capability.
*
**/

	for (corenum=0;corenum<MAX_CORES;corenum++)
	{
		errval = BTICard_CoreOpen(&hCore,corenum,hCard);
		if (errval) break;

		for (channum=0;channum<MAX_CHANNELS;channum++)
		{
			if ( BTI1553_ChGetInfo(INFO1553_MULTIMODE,channum,hCore) )
				break;
			if ( BTI1553_ChGetInfo(INFO1553_MULTIMODE,channum+1,hCore) )
				break;
		}

		if (channum != MAX_CHANNELS) break;
	}

	if (errval || corenum == MAX_CORES || channum == MAX_CHANNELS)
	{
		printf("\nError:  No MIL-STD-1553 level M, C or D channels present in card #%d.",CARDNUM);
		printf("\n");
		BTICard_CardClose(hCard);
		exit(1);
	}

	printf("\nUsing MIL-STD-1553 core #%d",corenum);
	printf("\nUsing MIL-STD-1553 channel #%d",channum);
	printf("\n");

/**
*
*  Configure the channel for bus controller mode.
*
**/

	BTICard_CardReset(hCore);									/*Reset the core*/

	errval = BTI1553_BCConfig(BCCFG1553_DEFAULT,channum,hCore);	/*Configure the core*/

	if (errval < 0)
	{
		printf("\nError:  An error was encountered (%i) while configuring for",errval);
		printf("\n        bus controller mode on channel #%d on core #%d.",channum,corenum);
		printf("\n        (%s)",BTICard_ErrDescStr(errval,hCard));
		printf("\n");
		BTICard_CardClose(hCard);
		exit(1);
	}

/**
*
*  Create a bus controller message and insert into the bus
*  controller schedule.
*
**/

	BCMsg = BTI1553_BCCreateMsg(MSGCRT1553_DEFAULT,0x0843,0,BCData,hCore);

	BTI1553_BCSchedFrame(FRAMETIME,channum,hCore);

/**
*
*  Configure the channel for monitor mode as shown in Example 4.
*
**/

	errval = BTI1553_MonConfig(MONCFG1553_DEFAULT,channum,hCore);

	if (errval < 0)
	{
		printf("\nError:  An error was encountered (%i) while configuring for",errval);
		printf("\n        monitor mode on channel #%d on core #%d.",channum,corenum);
		printf("\n        (%s)",BTICard_ErrDescStr(errval,hCard));
		printf("\n");
		BTICard_CardClose(hCard);
		exit(1);
	}

/**
*
*  Configure the sequential record.
*
**/

	errval = BTICard_SeqConfig(SEQCFG_DEFAULT,hCore);

	if (errval < 0)
	{
		printf("\nError:  An error was encountered (%i) while configuring the",errval);
		printf("\n        sequential record on on core #%d.",corenum);
		printf("\n        (%s)",BTICard_ErrDescStr(errval,hCard));
		printf("\n");
		BTICard_CardClose(hCard);
		exit(1);
	}

/**
*
*  Start the card to begin transmitting, receiving and monitoring messages.
*
**/

	BTICard_CardStart(hCore);

	memset( &BCXmitMsg, 0, sizeof(BCXmitMsg));
	BCXmitMsg.ctrlflags = MSGCRT1553_BCRT;
	BCXmitMsg.cwd1 = BTI1553_ValPackCWD( BCADDR ,RCV, BCSUBADDR, DATASIZE10); //Receive command
	BCXmitMsg.data[0] = 0x1A2B;
	BCXmitMsg.data[1] = 0x2B2B;
	BCXmitMsg.data[2] = 0x3C3C;	
	BCXmitMsg.data[3] = 0x4D4D;	
	BCXmitMsg.data[4] = 0x5E5E;	
	BCXmitMsg.data[5] = 0x6F6F;	
	BCXmitMsg.data[6] = 0x7070;	
	BCXmitMsg.data[7] = 0x8181;	
	BCXmitMsg.data[8] = 0x9292;	
	BCXmitMsg.data[9] = 0xA3A3;	

	XMITFIELDS1553 BCRecvMsg;
	memset( &BCRecvMsg, 0, sizeof(BCRecvMsg));
	BCRecvMsg.ctrlflags = MSGCRT1553_MON;
	BCRecvMsg.cwd1 = BTI1553_ValPackCWD( BCADDR, XMT, BCSUBADDR, DATASIZE10); //Receive command

	printf("\nThis is the BC Version");
	int increrrors = 0;
wrongentry:
	printf("\nEnter # (0-9) of errors to insert into data OR 'i' for incrementing count\n");

	numberoferrors = 0;
	while( numberoferrors = !_kbhit());
	numberoferrors = _getch();
	if( numberoferrors == 'i' )
	{
		increrrors = 1;
		numberoferrors = 0;
	}
	else if( numberoferrors >= '0' && numberoferrors <= '9')
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
	int indx = 0;
/**
*
*  Loop until a key is hit.
*
**/
	bool timetoexit = false;
	while (!_kbhit() && timetoexit == false )
	{
		int bufcount = 0;
		ULONG Status = 0;
		ULONG blockcount = 0;
		//USHORT seqbuf[2048];
		//SEQFINDINFO sfinfo;
		//LPSEQRECORD1553 pRec1553;

		//*****************************************************************
		//ATLAS 020017 Setup to Send the Send Data Command
		//*****************************************************************

		fillxmtbuffer();
		indx = indx % 6;
//goto skip1st;
		switch( indx )
		{
		case 0:
			printf("\nCase 0->%d\n",indx);
			if( indx != 0 )
			printf("\nCase 3->%d\n",indx);
			if( increrrors != 0 )
				numberoferrors = ++numberoferrors % 32;
			for( i = 0; i < numberoferrors; i++)
				BCXmitMsg.data[i] += 1;
			BCXmitMsg.cwd1 = BTI1553_ValPackCWD( BCADDR, RCV, BCSUBADDR, DATASIZE10); //Receive command
			BCXmitMsg.errflags = MSGERR1553_NORESP;
			while (BCXmitMsg.errflags & MSGERR1553_NORESP)		/*Was there a response?*/
			{
				Status = BTI1553_BCTransmitMsg( &BCXmitMsg, 0, hCore);
				if (BCXmitMsg.errflags & MSGERR1553_NORESP)		/*Was there a response?*/
				{
					Sleep(50);
				}
				if( _kbhit() )
				{
					timetoexit = true;
					break;
				}
			}

			{
				printf("\n");
				printf("BC->RT ");
				printf(" Cwd1:%04X", BCXmitMsg.cwd1);
				for (i = 0; i < BCXmitMsg.datacount; i++)
				{
					printf(" %04X", (BCXmitMsg.data[i]));
				}
				if( BCXmitMsg.datacount == 10 )
					i = 10;
			}
			if( timetoexit == true )
				break;
			Sleep(10);
			//
			indx++;
			break;
		case 1:

			//*****************************************************************
			//ATLAS 020017 Send the Send Data Command
			//*****************************************************************

			printf("\nCase 1->%d\n",indx);
			if( indx == 1 )
				BCRecvMsg.cwd1 = BTI1553_ValPackCWD( BCADDR, XMT, BCSUBADDR, DATASIZE5); //Receive command
			else
				BCRecvMsg.cwd1 = BTI1553_ValPackCWD( BCADDR, XMT, BCSUBADDR, DATASIZE10); //Receive command
			BCRecvMsg.errflags = MSGERR1553_NORESP;
			indx++;
			Sleep(500);	// was 2000);
			break;
		case 99:
			Status = BTI1553_BCTransmitMsg( &BCRecvMsg, 0, hCore);
			while (BCRecvMsg.errflags & (MSGERR1553_NORESP | MSGERR1553_ANYERR))		/*Was there a response? or error */
			{
				Sleep(100);
				Status = BTI1553_BCTransmitMsg( &BCRecvMsg, 0, hCore);
				if( BCRecvMsg.datacount == 5 )
					i = 5;
				if( _kbhit() )
				{
					timetoexit = true;
					break;
				}
			}
			if( timetoexit == true )
				break;
			{
				printf("\n");
				printf("\r");
				printf("BC->RT ");
				printf(" Cwd1:%04X", BCRecvMsg.cwd1);
				for (i = 0; i < BCRecvMsg.datacount; i++)
				{
					printf(" %04X", (BCRecvMsg.data[i]));
				}
				if( BCRecvMsg.datacount == 5 )
					i = 5;
			}
			Sleep(500);	// was 2000);
			indx++;
			break;
		case 2:
			//*****************************************************************
			//ATLAS 020070 Send data via a Broadcast Message
			//*****************************************************************
			printf("\nCase 2->%d\n",indx);
			if( indx == 2)
				BCXmitMsg.cwd1 = BTI1553_ValPackCWD( BCADDR, XMT, BCSUBADDR, DATASIZE5); //Receive command
			else
				BCXmitMsg.cwd1 = BTI1553_ValPackCWD( BCADDR, XMT, BCSUBADDR, DATASIZE10); //Receive command
			BCXmitMsg.errflags = MSGERR1553_NORESP;
			while (BCXmitMsg.errflags & MSGERR1553_NORESP)		/*Was there a response?*/
			{
				Status = BTI1553_BCTransmitMsg( &BCXmitMsg, 0, hCore);
				if (BCXmitMsg.errflags & MSGERR1553_NORESP)		/*Was there a response?*/
				{
					Sleep(100);
				}
				if( _kbhit() )
				{
					timetoexit = true;
					break;
				}
			}
			Sleep( 500 );	// was 1000);
			if( _kbhit() )
			{
				timetoexit = true;
				break;
			}
				printf("\r");
				printf("BC->RT ");
				printf(" Cwd1:%04X", BCXmitMsg.cwd1);
				for (i = 0; i < BCXmitMsg.datacount; i++)
				{
					printf(" %04X", (BCXmitMsg.data[i]));
				}
			Sleep( 500 );	// was1000);
			if( _kbhit() )
			{
				timetoexit = true;
				break;
			}
			indx++;
			break;
		case 3:
			if( indx != 0 )
			printf("\nCase 3->%d\n",indx);
			BCXmitMsg.cwd1 = BTI1553_ValPackCWD( BCADDR, RCV, BCSUBADDR, DATASIZE10); //Receive command
			BCXmitMsg.errflags = MSGERR1553_NORESP;
			while (BCXmitMsg.errflags & MSGERR1553_NORESP)		/*Was there a response?*/
			{
				Status = BTI1553_BCTransmitMsg( &BCXmitMsg, 0, hCore);
				if (BCXmitMsg.errflags & MSGERR1553_NORESP)		/*Was there a response?*/
				{
					Sleep(100);
				}
				if( _kbhit() )
				{
					timetoexit = true;
					break;
				}
			}

			{
				printf("\n");
				printf("BC->RT ");
				printf(" Cwd1:%04X", BCXmitMsg.cwd1);
				for (i = 0; i < BCXmitMsg.datacount; i++)
				{
					printf(" %04X", (BCXmitMsg.data[i]));
				}
				if( BCXmitMsg.datacount == 10 )
					i = 10;
			}
			if( timetoexit == true )
				break;
			Sleep(10);
			indx++;
			break;
		case 4:
			Sleep(2000);
		default:
			BCXmitMsg.errflags = MSGERR1553_NORESP;
			printf("\nCase default or %d\n", indx);
			indx++;
			break;
		}
		BCRecvMsg.errflags = MSGERR1553_NORESP;
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

void fillxmtbuffer ()
{
	memset( &BCXmitMsg, 0, sizeof(BCXmitMsg));
	BCXmitMsg.ctrlflags = MSGCRT1553_BCRT;
	BCXmitMsg.cwd1 = BTI1553_ValPackCWD( BCADDR ,RCV, BCSUBADDR, DATASIZE10); //Receive command
	BCXmitMsg.data[0] = 0x1A2B;
	BCXmitMsg.data[1] = 0x2B2B;
	BCXmitMsg.data[2] = 0x3C3C;	
	BCXmitMsg.data[3] = 0x4D4D;	
	BCXmitMsg.data[4] = 0x5E5E;	
	BCXmitMsg.data[5] = 0x6F6F;	
	BCXmitMsg.data[6] = 0x7070;	
	BCXmitMsg.data[7] = 0x8181;	
	BCXmitMsg.data[8] = 0x9292;	
	BCXmitMsg.data[9] = 0xA3A3;	
	for( int i = 0; i < numberoferrors; i++)
		BCXmitMsg.data[i] += 1;
	//	numberoferrors = 0;
}

