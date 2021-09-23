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
 *
 *	Testop.h (RTL test operations header file)
 *
 *
 *
 *	Created 8/26/99 DL
 *
 */


#ifndef __TESTOP_H__
#define __TESTOP_H__

/*-------------------------------------------------------------
Struct:	TESTOPERATION

Description:
	This structure completely describes Test mode of operation.
---------------------------------------------------------------*/
typedef struct TESTOPERATION
{
	U32BIT  dwOptions;		/* Test Options */	

} TESTOPERATION;

/* Test result structure */
typedef struct TESTRESULT
{
	U16BIT wResult;		/* pass or fail code     */
	U16BIT wExpData;	/* expected data on fail */
	U16BIT wActData;	/* actual data on fail   */
	U16BIT wAceAddr;	/* address of failure    */
	U16BIT wCount;		/* test count index      */
	/*FLEXCORE*/    
    U32BIT dwExpData;	/* expected data 32 on fail */
	U32BIT dwActData;	/* actual data 32 on fail   */
	BOOLEAN PciMode32;

} TESTRESULT;

/**********************************/ 
/* for FLEXCORE_DMA               */
/**********************************/
#define MAX_ERROR_BUFFER		256
#define MAX_DATA_BUFFER			64*1024
#define FILL_MEMORY_BYTE		0x42

_EXTERN S16BIT _DECL NonOverlappedTests(S16BIT DevNum, U32BIT DmaTransferSize);
_EXTERN S16BIT _DECL NonOverlappedWrite(S16BIT DevNum, U32BIT DmaTransferSize);
_EXTERN S16BIT _DECL FillDmaDataBAR0(S16BIT DevNum, U32BIT PciOffSet, U32BIT DmaTransferSize);
_EXTERN S16BIT _DECL DumpDmaDataBAR0(S16BIT DevNum, U32BIT PciOffSet, U32BIT DmaTransferSize);
_EXTERN S16BIT _DECL PrintError(DWORD ErrorCode);

/* All possible result codes returned in test structure */
#define ACE_TEST_PASSED 			0x00
#define ACE_TEST_NOT_PERFORMED		0x01
#define ACE_TEST_FAILED_RAM 		0x02
#define ACE_TEST_FAILED_REGISTER	0x03
#define ACE_TEST_FAILED_PROTOCOL	0x04
#define ACE_TEST_FAILED_INTERRUPT	0x05
#define ACE_TEST_FAILED_MVECTOR	 	0x06
#define ACE_TEST_FAILED_RVECTOR 	0x07
#define ACE_TEST_FAILED_NOTOPEN 	0x08
#define ACE_TEST_FAILED_CANEBR		0x09

_EXTERN S16BIT _DECL aceTestConfigure(S16BIT DevNum,U32BIT dwOptions);

_EXTERN S16BIT _DECL aceTestRegisters(S16BIT DevNum,TESTRESULT *pTest);

_EXTERN S16BIT _DECL aceTestMemory(S16BIT DevNum,TESTRESULT *pTest,
								   U16BIT wValue);

_EXTERN S16BIT _DECL aceTestProtocol(S16BIT DevNum,TESTRESULT *pTest);

_EXTERN S16BIT _DECL aceTestIrqs(S16BIT DevNum,TESTRESULT *pTest);

_EXTERN S16BIT _DECL aceTestVectors(S16BIT DevNum,TESTRESULT *pTest,
								   char *pFileName);

_EXTERN S16BIT _DECL aceTestVectorsStatic(S16BIT DevNum,TESTRESULT *pTest);


_EXTERN S16BIT _DECL aceTestCanEbrLoop(S16BIT DevNum,
									   TESTRESULT *pTest);

_EXTERN S16BIT _DECL flexTestVectors(S16BIT DevNum,TESTRESULT *pTest,
								   char *pFileName);

#endif
