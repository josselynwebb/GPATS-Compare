/* File: M910alig.c
 * 
 * Calls terM9_executeSystemAlignment function
 *
 *
 * Required arguments -
 * Lasar DTB File Name - excluding extension
 *
 ***********************************************************
 ***********************************************************
 *	File Creation Date: 8/31/98
 *  Version: 1.0
 *  Created By: Grady Johnson
 *  Last Revision by:
 *
 *	Revision Log:
 *
 ***********************************************************
 ***********************************************************
 */

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <dos.h>
#include <visa.h>
#include "terM9.h"				// M910 type and function definitions
#include "nam.h"				// Nam type definitions
#include "dm_services.h"		// Diagnostics type definitions
#include <Windows.h>

#define TEMPSUFFIX ".TMP"		// TYX virtual memory file extension
#define DTBSUFFIX ".dtb"		// Lasar DTB file extension

#define STRING_SIZE 2048

#define UR14_RESOURCE_NAME "VXI0::36::INSTR"
#define TALON_MANF_ID 3855

/***********************
 * Function Prototypes *
 ***********************/

void _VI_FUNC chkstatTerM9(ViStatus func, ViStatus status_expected);  // Prints error code from M910
void _VI_FUNC abort_test (void);				 // Shuts down communication to M910
												 // in event of Error.

/* TYX data structure - ref. TYX PAWS User Guide Vol 1 Sec 5 */
char DtbName[STRING_SIZE];
int Result;
long DtbNameAddress, ResultAddress;

/*
 * Variable Declarations *
 */
char TempFileName[40];
ViSession vi;				  // Instrument Handle
ViInt32 testHandle;			  // Digital Test handle
ViInt32 alignmentResult;	  // System Alignment Results
char DTBFile[256];		      // Lasar DTB file name
	
							  
/******************************
 * This is the MAIN FUNCTION! *
 ******************************/

void main (int argc, char *argv[])
{
	/* Local Variables */
	ViSession rmSession = 0;
	ViSession t940Session = 0;
	ViInt16 manufacturerId = 0;
	ViStatus t940Status = 0;
	
	//MessageBox(NULL, "M910 Align Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	/* Define ATLAS temp file name and open it */
	/* ref. TYX PAWS User Guide Vol 1 Sec 5 */
	strcpy(TempFileName, argv[1]);
	strcat(TempFileName, TEMPSUFFIX);
	vmOpen(TempFileName);

	/* Locate address to arguments in *.TMP file */
	DtbNameAddress = atol(argv[2]);
	ResultAddress = atol(argv[3]);

	/* Move arguments into defined structure DAT_ITM */
	/* ref. TYX PAWS User Guide Vol 1 Sec 5 */
	vmGetText(DtbNameAddress, DtbName, STRING_SIZE);
	Result = vmGetInteger(ResultAddress);
	
	/* copy arguments into variables */
	strcpy (DTBFile, DtbName);
	
	/* Add suffix to filenames */
    strcat (DTBFile, DTBSUFFIX);
	
    /* Print passed arguments to screen for debug */
	printf ("Name of DTB File: %s\n", DTBFile);

	//check for presence of T940 Digital
	viOpenDefaultRM(&rmSession);
	viOpen(rmSession, UR14_RESOURCE_NAME, 0, 1.0, &t940Session);

	viGetAttribute(t940Session, VI_ATTR_MANF_ID, &manufacturerId);
	if(manufacturerId == TALON_MANF_ID)
	{
		//T940 Is aligned when LDE software is initialized and reset
		t940Status = terM9_init("TERM9", VI_TRUE, VI_TRUE, &vi);
		if(!t940Status)
			alignmentResult = TERM9_RESULT_PASS;
		else
			alignmentResult = TERM9_RESULT_FAIL;
		goto SkipM9Align;
	}
		
	/* Open VI Session with DTI */

	printf ("Executing terM9_init...");
	chkstatTerM9(terM9_init("TERM9", VI_FALSE, VI_TRUE, &vi), VI_SUCCESS);
	printf ("\t\t\t\t\tcomplete. \n");

	/* Initialize Instrument */
	printf ("Executing terM9_initializeInstrument...");
	chkstatTerM9(terM9_initializeInstrument(vi), VI_SUCCESS);
	printf ("\t\t\tcomplete. \n");

	/* Open digital test */
	printf ("Executing terM9_openDigitalTest...");
	chkstatTerM9(terM9_openDigitalTest(vi, DTBFile, &testHandle), VI_SUCCESS);
	printf ("\t\t\tcomplete. \n");

	/* Load the setup data specified in the digital test */
	printf ("Executing terM9_configureDigitalTestSetup...");
	chkstatTerM9(terM9_configureDigitalTestSetup(vi, testHandle), VI_SUCCESS);
	printf ("\tcomplete. \n");
	
	/* Execute the system-level alignment procedure. */
	printf ("Executing terM9_executeSystemAlignment...");
    chkstatTerM9(terM9_executeSystemAlignment(vi, &alignmentResult), VI_SUCCESS);
	printf ("\t\tcomplete. \n");
	
	/* Close Communications */
	printf ("Executing terM9_close...");
	chkstatTerM9(terM9_close(vi), VI_SUCCESS);
	printf ("\t\t\t\tcomplete. \n");
	
SkipM9Align:
	/* Return Burst result and Log file name to ATLAS */
	/* ref TYX PAWS User Guide Vol 1 Sec 5 */
	Result = alignmentResult;
	vmSetInteger(ResultAddress, Result);
	
	/* Close virtual memory file */
	vmClose();
    exit(0);
}


/* Here are the definitions for the local functions that are
 * called from with the MAIN function.
 */

 /* chkstatTerM9 - checks the return value of any terM9 function
  * and evaluates the status.  If error then prints to screen and to
  * log file the string containing the definition of the error */
void _VI_FUNC chkstatTerM9(ViStatus func, ViStatus status_expected) {

	ViStatus status_actual=func;
    ViChar status_string[256];

	if (status_actual != status_expected) {

		/* Capture M910 Failure Message */
		terM9_errorMessage (vi, status_actual, status_string);
		printf ("ERROR: Aborting due to...%s\n\n",&status_string);
		
		/* Close Virtual Memory File */
	    vmClose();

		/* Close communicaitons to the M910 */
	    abort_test(); 
	}
}


/* abort_test - resets then closes communication to M910 hardware */
void _VI_FUNC abort_test () { 
	terM9_reset(vi);
	terM9_close (vi);
	exit(EXIT_FAILURE);
}


