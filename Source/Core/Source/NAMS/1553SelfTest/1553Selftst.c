/*	File: 1553selftst.c
 *	
 *  This program combines DDC self test source code into one file.  
 *  
 *
 *	File Creation Date: 3/8/06
 *  Version: 1.0
 *  Created By: Robert Giumarra
 * 
 *
 *		ENHANCED MINI-ACE Software Toolbox
 *
 *			Copyright (c) 2005 Data Device Corporation
 *			All Rights Reserved.
 *
 * tester.c
 *           Performs detailed selftest of ACE using built in
 *           library test routines. The TestResult structure
 *           returns information on the failure.
 *
 */
#include <windows.h>
//#include <conio.h>
#include "stdemace.h"
#include "oldnam.h"


/* Declare functions used to interface with nam.lib */
#define SUFFIX ".TMP"
extern void v_init(void);
extern void	v_opn(int, char*, int);
extern void	mov_dfv(struct DAT_ITM*, long);
extern void	mov_dtv(long, struct DAT_ITM*);
extern void	v_cls(int);

/* Define Global Variables */
/* TYX data structure - ref. TYX PAWS User Guide Vol 1 Sec 5 */
struct DAT_ITM Result;
long ResultAddr;
char TempFileName[40];
int ATLAS; /* 0 indicates NOT using atlas, 1 indicates using atlas */	
S16BIT     DevNum  = 0x0000;
S16BIT     nResult = 0x0000;
//static BuTest_t Test;/* ACE library Test status type   */

/*-----------------------------------------------------------------------
Name:	PressAKey()

Description:
	Allows application to pause to allow screen contents to be read.

In		none
Out		none
------------------------------------------------------------------------*/
//void PressAKey()
//{
//	printf("\nPress <ENTER> to continue...");
//	getch();
//}

/*-----------------------------------------------------------------------
Name:	PrintHeader

Description:
	Prints the sample program header.

In		none
Out		none
------------------------------------------------------------------------*/
void PrintHeader()
{
	U16BIT     wLibVer;
	
	wLibVer = aceGetLibVersion();
	printf("*********************************************\n");
	printf("Enhanced Mini-ACE Integrated 1553 Terminal  *\n");
	printf("BU-69090S0 Runtime Library            	    *\n");
	printf("Release Rev %d.%d.%d                           *\n",
		    (wLibVer>>8),			// Major
			(wLibVer&0xF0)>>4,		// Minor
			(wLibVer&0xF));			// Devel
	if ((wLibVer&0xF)!=0)
	printf("=-=-=-=-=-=-DEVELOPMENT VERSION-=-=-=-=-=-=-*\n");
	printf("Copyright (c) 2005 Data Device Corporation  *\n");
	printf("*********************************************\n");
	printf("Tester.c Self-Test operations [BIST]        *\n");
	printf("*********************************************\n\n");
}

/*-----------------------------------------------------------------------
Name:	PrintOutError

Description:
	This function prints out errors returned from library functions.

In		nResult = The error number.
Out		none
------------------------------------------------------------------------*/
void PrintOutError(S16BIT nResult)
{
	char buf[80];
	aceErrorStr(nResult,buf,80);
	printf("RTL Function Failure-> %s.\n",buf);
}

/*-----------------------------------------------------------------------
Name:	TestRegisters

Description:
	Tests register operation on an Enhanced Mini-ACE.

In		DevNum  = ID associated with the hardware being accessed (0-31).
In		sTest   = TestResult structure.
Out		none
------------------------------------------------------------------------*/
void TestRegisters(S16BIT DevNum,TESTRESULT *pTest)
{
	S16BIT nResult = 0x0000;

	printf("Testing.....");
	
	/* Test card registers */
	nResult = aceTestRegisters(DevNum,pTest);

	/* Capture error */
	if(nResult)
	{
		PrintOutError(nResult);
		return;
	}

	/* Display Test results */
	if(pTest->wResult == ACE_TEST_PASSED)
	{
		printf("Registers Passed test.\n");
	}else
	{
		printf("Register Failed test, expected=%04x read=%04x!!!\n",
				pTest->wExpData,pTest->wActData);
	}
}

/*-----------------------------------------------------------------------
Name:	TestMemory

Description:
	Tests hardware memory on an Enhanced Mini-ACE.

In		DevNum  = ID associated with the hardware being accessed (0-31).
In		sTest   = TestResult structure.
In		wValue  = the value to written and verified.
Out		none
------------------------------------------------------------------------*/
void TestMemory(S16BIT DevNum,TESTRESULT *pTest,U16BIT wValue)
{
	S16BIT nResult = 0x0000;

	printf("Testing.....");

	/* Test card memory */
	nResult = aceTestMemory(DevNum,pTest,wValue);
	
	/* Capture error */
	if(nResult)
	{
		PrintOutError(nResult);
		return;
	}

	/* Display test results */
	if(pTest->wResult == ACE_TEST_PASSED)
	{
		printf("Ram Passed %04x test.\n",wValue);
	}else
	{
		printf("Ram Failed %04x test, data read = %04x addr = %d!!!\n",
		    	wValue,pTest->wActData,pTest->wAceAddr);
	}
}

/*-----------------------------------------------------------------------
Name:	TestProtocol

Description:
	Tests hardware protocol on an Enhanced Mini-ACE.

In		DevNum  = ID associated with the hardware being accessed (0-31).
In		sTest   = TestResult structure.
Out		none
------------------------------------------------------------------------*/
void TestProtocol(S16BIT DevNum,TESTRESULT *pTest)
{
	
	S16BIT nResult = 0x0000;

	printf("Testing.....");

	/* Test card Protocol */
	nResult = aceTestProtocol(DevNum,pTest);
	
	/* Capture error */
	if(nResult)
	{
		PrintOutError(nResult);
		return;
	}
	
	/* Display test results */
	if(pTest->wResult == ACE_TEST_PASSED)
	{
		printf("Protocol Unit Passed test.\n");
	}else
	{
		printf("Protocol Unit Failed test, expected=%04x read=%04x addr=%04x!!!\n",
			    pTest->wExpData,pTest->wActData,pTest->wAceAddr);
	}
}

/*-----------------------------------------------------------------------
Name:	TestInterrupts

Description:
	Tests hardware interrupts on an Enhanced Mini-ACE.

In		DevNum  = ID associated with the hardware being accessed (0-31).
In		sTest   = TestResult structure.
Out		none
------------------------------------------------------------------------*/
void TestInterrupts(S16BIT DevNum,TESTRESULT *pTest)
{
	S16BIT nResult = 0x0000;
	
	printf("Testing.....");

	/* Test Card Interrupts */
	nResult = aceTestIrqs(DevNum,pTest);

	/* Capture error */
	if(nResult)
	{
		PrintOutError(nResult);
		return;
	}

	/* Display test results */
	if(pTest->wResult == ACE_TEST_PASSED)
	{
		printf("Interrupt Occurred, Passed test.\n");
	}else
	{
		printf("Interrupt Test Failure, %s %s!!!\n",
			  (pTest->wCount&1)?"NO TimeTag Rollover":"",
			  (pTest->wCount&2)?"NO IRQ":"");
	}
}

/*-----------------------------------------------------------------------
Name:	TestVectors

Description:
	Tests the Enhanced Mini-ACE using a Test Vector File called Test.vec.

In		DevNum  = ID associated with the hardware being accessed (0-31).
In		pTest   = TestResult structure.
Out		none
------------------------------------------------------------------------*/
void TestVectors(S16BIT DevNum,TESTRESULT *pTest)
{
	S16BIT nResult = 0x0000;

	printf("Testing.....");
	
	/* Test card vectors  NOTE: Must have file 'Test.vec' */
#ifdef INTEGRITY
	nResult = aceTestVectorsStatic(DevNum,pTest);
#else
	nResult = aceTestVectors(DevNum,pTest,"Test.vec");
#endif
	
	
	/* Capture error */
	if(nResult)
	{
		PrintOutError(nResult);
		return;
	}

	/* Display test results */
	if(pTest->wResult == ACE_TEST_PASSED)
	{
		printf("Vectors Passed, EOF at line #%d.\n",pTest->wCount);
	}else
	{
		printf("Vectors Failed!\n");
		printf("Failure...at line #%d\n",pTest->wCount);
		printf("          location=%s\n",
  			  (pTest->wResult==ACE_TEST_FAILED_MVECTOR)?"memory":"register");
		printf("          address=%04x\n",pTest->wAceAddr);
		printf("          expected=%04x\n",pTest->wExpData);
		printf("          actual=%04x\n",pTest->wActData);
	}
}



/* CloseNAM - De-allocates TMP file and closes program */
void ClosePrg(int CloseStatus)
{
	/* Return Results and Result String to ATLAS */
	/* ref TYX PAWS User Guide Vol 1 Sec 5 */
	if (ATLAS){
		Result.d_val.d_ival = CloseStatus; /* 1 Pass, 0 Fail */
		mov_dtv(ResultAddr, &Result);
		
		/* Close virtual memory file */
		v_cls(DATASPAC);
	}
	else
	{
		if (CloseStatus) printf("\n1553 Selftest PASSED\n");
		else printf("\n1553 Selftest FAILED\n");
	}

	nResult = aceFree(DevNum);
	exit(0);
}

int main (int argc, char* argv[])
{

  S16BIT     DevNum  = 0x0000;
  S16BIT     nResult = 0x0000;
  TESTRESULT sTest;
	U16BIT		dummy;
 	int		count = 0;
//	HANDLE     hConsole;
		
	/* Setup Windows Console Screen */
//	hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
//	SetConsoleTitle("DDC Enhanced Mini-ACE RTL BC Loop Back Test ");





  /* Figure out if Dos Command Line or ATLAS program called */
  /* Copy passed arguments */
    if (argc < 3) ATLAS = 0;else ATLAS = 1;

	/* Print out sample header */
	PrintHeader();
	
	/* Set Logical Device # */
	DevNum = 0;
	printf("\n** Testing Device Number %d  **\n\n",DevNum);

		/* Initialize Device */
	nResult = aceInitialize(DevNum,ACE_ACCESS_CARD,ACE_MODE_TEST,0,0,0);

	if (ATLAS) /* use ATLAS */
	{
	/* Use Nam.h and Nam.lib to copy arguments passed arguments
	   from ATLAS*/
	/* Initialize the communication library */
		OScomm();
		CScomm();
		OSmain(&argc, &argv);
		v_init();
	/* Define ATLAS temp file name and open it */
		strcpy(TempFileName, argv[1]);
		strcat(TempFileName, SUFFIX);
		v_opn (DATASPAC, TempFileName, OSACRW);
	/* Copy passed arguments */
		ResultAddr = atol(argv[2]);
		mov_dfv(&Result, ResultAddr);
		if (nResult) ClosePrg(0);
	}
	else
	{
		if(nResult) {
			printf("Initialize ");
			PrintOutError(nResult);
			ClosePrg(0); /* 0 Indicates Failed, 1 Indicates Pass */
			return;
		}
	
	/* Run Card Diagnostic Tests */
		TestRegisters(DevNum,&sTest);
		TestMemory(DevNum,&sTest,0xaaaa);
		TestMemory(DevNum,&sTest,0xaa55);
		TestMemory(DevNum,&sTest,0x55aa);
		TestMemory(DevNum,&sTest,0x5555);
		TestMemory(DevNum,&sTest,0xffff);
		TestMemory(DevNum,&sTest,0x1111);
		TestMemory(DevNum,&sTest,0x8888);
		TestMemory(DevNum,&sTest,0x0000);
		TestInterrupts(DevNum,&sTest);
		if(!aceIsFlexcore(DevNum))
		{
			TestProtocol(DevNum,&sTest);
			TestVectors(DevNum,&sTest);
		}
	}

  if (argc > 1)
  {

		/* Initialize Device */
		nResult = aceInitialize(DevNum,ACE_ACCESS_CARD,ACE_MODE_TEST,0,0,0);
		if(nResult)
		{
			printf("Initialize ");
			PrintOutError(nResult);
			return;
		}

	  /* Test A looped into B */
	  printf("\nRunning Loop Test A=>B\n");
	  aceRegWrite(DevNum,0x0003,0x0001); /* reset */
	  aceRegWrite(DevNum,0x0007,0x8000); /* config 3: enhanced mode */
	  aceRegWrite(DevNum,0x0001,0x4000); /* config 1: word monitor mode */
	  aceMemWrite(DevNum,0x0100,0x0000); /* initialize stack to 0 */
	  aceMemWrite(DevNum,0x0000,0x0000); /* zero first monitor stack entry (rx word) */
	  aceMemWrite(DevNum,0x0001,0x0000); /* zero first monitor stack entry (tag word) */
	  aceRegWrite(DevNum,0x0003,0x0002); /* start monitor */

	  aceRegWrite(DevNum,0x0008,0x0002); /* config 4: encoder test mode */
	  aceRegWrite(DevNum,0x000D,0x1234); /* place tx word in aux 1 */

	  /* step 1: assert encoder enable and tx enable with desired channel and sync type */
		aceRegWrite(DevNum,0x0011,0xb820);
  /* step 2: remove encoder enable immediately to ensure only 1 word is transmitted */
	  aceRegWrite(DevNum,0x0011,0xa820);

	  /* wait for word to be transfered */
	  count = 0;
	  do
	  {
	    dummy = aceMemRead(DevNum,0x0001);
	    count++;
	  } while ((!dummy) && (count < 100));

	  if((dummy&0x00ff) != 0x00ED)  /* notice that gap time is masked */
		{
			printf("...Failed loopback Test.  Expected Monitor Tag word 0xXXED received 0x%04x\n", dummy);
			ClosePrg(0);                /* 0 indicates Failed, 1 indicates Pass */
		}
		else printf("...Passed, correct Tag word.\n");

	  dummy = aceMemRead(DevNum,0x0000);
	  if(dummy != 0x1234)
		{
			printf("...Failed loopback Test.  Expected Monitored Word 0x1234 received 0x%04x\n", dummy);
			ClosePrg(0);                /* 0 indicates Failed, 1 indicates Pass */
		}
		else printf("...Passed, correct monitored word.\n");

	  aceRegWrite(DevNum,0x0003,0x0001); /* reset the EMACE to return it to a known state */


  /* Now test B looped into A */
	  printf("\nRunning Loop Test B=>A\n");
	  aceRegWrite(DevNum,0x0003,0x0001); /* reset */
	  aceRegWrite(DevNum,0x0007,0x8000); /* config 3: enhanced mode */
	  aceRegWrite(DevNum,0x0001,0x4000); /* config 1: word monitor mode */
	  aceMemWrite(DevNum,0x0100,0x0000); /* initialize stack to 0 */
	  aceMemWrite(DevNum,0x0000,0x0000); /* zero first monitor stack entry (rx word) */
	  aceMemWrite(DevNum,0x0001,0x0000); /* zero first monitor stack entry (tag word) */
	  aceRegWrite(DevNum,0x0003,0x0002); /* start monitor */

	  aceRegWrite(DevNum,0x0008,0x0002); /* config 4: encoder test mode */
	  aceRegWrite(DevNum,0x000D,0x1234); /* place tx word in aux 1 */

	  /* step 1: assert encoder enable and tx enable with desired channel and sync type */
	  aceRegWrite(DevNum,0x0011,0x3820);
		/* step 2: remove encoder enable immediately to ensure only 1 word is transmitted */
	  aceRegWrite(DevNum,0x0011,0x2820);

	  /* wait for word to be transfered */
	  count = 0;
	  do
	    {
	    dummy = aceMemRead(DevNum,0x0001);
	    count++;
	    } while ((!dummy) && (count < 100));

	  if((dummy&0x00ff) != 0x00E9)  /* notice that gap time is masked */
		{
			printf("...Failed loopback Test.  Expected Monitor Tag word 0xXXED received 0x%04x\n", dummy);
			ClosePrg(0);                /* 0 indicates Failed, 1 indicates Pass */
		}
		else printf("...Passed, correct Tag word.\n");

	  dummy = aceMemRead(DevNum,0x0000);
	  if(dummy != 0x1234)
		{
			printf("...Failed loopback Test.  Expected Monitored Word 0x1234 received 0x%04x\n", dummy);
			ClosePrg(0);                /* 0 indicates Failed, 1 indicates Pass */
		}
		else printf("...Passed, correct monitored word.\n");

	  aceRegWrite(DevNum,0x0003,0x0001); /* reset the EMACE to return it to a known state */
	}

	/* Free Device */
	nResult = aceFree(DevNum);
	
	if(nResult)
	{
		printf("Free ");
		PrintOutError(nResult);
		return;
	}
	
	ClosePrg(1);
}
	
	
	
	
	
	
	
	
	
	