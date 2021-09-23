/***************************************************************************
**	                          ScopeNAM	        					      **
****************************************************************************
**  ScopeNAM.c															  **
**																		  **
**	     Dave Joiner  05/23/03		             						  **
**																		  **
**  Allows the User to launch the Digital Oscilloscope and set to a       **		
**  preconfigured setting via ATLAS. The developer can set the mode to    **
**  either  "SINGLE" or "CONTINUOUS" mode. The Scope configuration file   **
**	is created by the Scope soft front panel and saved as a "Name.DSO"    **
**  file, this file must reside in the project directory for proper       **
**  execution. This NAM can be run from either DOS or ATLAS.              **
**  When the NAM is called, the Scope is launched to the predefined  	  **
**  setting in thee selected Mode. The NAM will suspend ATLAS execution   **
**	until the user closes the Scope. The "STATUS" key will be read from   **
**  the ATS.INI file. If "READY" is in the returned string, the call was **
**  Successful, otherwise, the Error will be returned.                    **
**															              **
/**************************************************************************/
/*																		  */
/*	MODE			= argv[2]	/* CONTINUOUS | SINGLE | MEAS_ONLY        */
/*	DSO_File		= argv[3]	/* DSO File Name                          */
/*  MEASUREMENT     = argv[4]   /* (Optional measurement return value)    */
/*					            (argv[4], if measurement not returned)    */
/*  RESULT			= argv[5] 	/* Error Code                             */
/*					            (argv[5], if measurement not returned)    */
/*  RESULTSTR		= argv[6]   /* Error Description                      */
/**************************************************************************/
///////////////////////////////////////////////////////////////////////////
// File:	    ScopeNAM.c
//
// Date:	    27-OCT-06
//
// Purpose:	    Instrument Driver for Arb
//
// Instrument:	Arb  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     cem.lib              \usr\tyx\lib
//     cemsupport.lib       ..\..\Common\Lib
//
// ATLAS Subset: PAWS-85
//
//
//
// Revision History
// Rev	     Date                  Reason										Author
// =======  =======		=======================================					=============================
// 1.1.0.0  27OCT06		Initial Release										    EADS, North America Defense
// 1.1.0.1  17NOV06		Corrected the INI file name to VIPERT					M.Hendricksen, EADS
// 2.0.0.0	14DEC18		Adding VIPERT 1320 source changes						J. Webb, USMC
//////////////////////////////////////////////////////////////////////////////////////////////////////////////



/***************************************************************************
*								Include Files							   *
****************************************************************************/

#include        <string.h>
#include		<stdlib.h>
#include		<stdio.h>
#include		<Windows.h>
#include		<winbase.h>
#include		<sys/stat.h>
#include		"nam.h"

/***************************************************************************
*					External Variables and Routines		     			   *
****************************************************************************/

/***************************************************************************
*									Globals		      			           *
***************************************************************************/


/* TYX data structure - ref. TYX PAWS User Guide Vol 1 Sec 5 */
#define STRING_SIZE 2048

char Mode[STRING_SIZE];
char DSO_File[STRING_SIZE];
float Measurement_Result;
int ResultInteger;
char ResultStr[STRING_SIZE];

/* Program Globals */
char		TempFileName[40];	/* Temporary File Name for ATLAS		  */
int	    ModeAddr;		    /* Arg "DSO_File" Address			      */
int		DSO_FileAddr;		/* Arg "Type" Address				      */
int        Meas_ResultAddr;    /* Either Arg Measurement or Results Add  */
int	    ResultIntegerAddr;	/* Arg Result or Result Srting Add	  */
int		ResultStrAddr;      /* Arg "ResultsStr" Address               */
char    	sCommLine[257];		/* Command Line Argument                  */
char		sScopePath[257];    /* DScope Executable Path                 */
LPDWORD        ExitCode;           /* Exit Code from VB executable           */
FILE        *filepointer;	    /* Pointer to Sequence input file         */
char		*char_line;	 	    /* Used to store a line of data out of data file */
short int   bMeas;				/* Flag: Return Measurement Value         */

/***************************************************************************
*								Constants			     		           *
***************************************************************************/

#define SUFFIX ".TMP"				   /* Temp file extension              */
#define SINGLE "TIP_RunSetup"		   /* TIP equivalent Cmd Line Argument */
#define CONTINUOUS "TIP_RunPersist"	   /* TIP equivalent Cmd Line Argument */
#define MEAS_ONLY "TIP_MeasOnly"       /* TIP equivalent Cmd Line Argument */
#define SAIS_HEADING "SAIS"			   /* ATS.INI file Section Heading    */
#define SCOPE_KEY "DSCOPE"			   /* ATS.INI file Key Name           */
#define ATSINIFILE "C:\\USERS\\PUBLIC\\DOCUMENTS\\ATS\\ATS.INI"	   /* ATS.INI file Key Name           */
#define SAIS_PATH "PATH"			   /* ATS.INI file Section Heading    */
#define TIPS_HEADING "TIPS"			   /* ATS.INI file Section Heading    */
#define CMD_KEY "CMD"				   /* ATS.INI file Key Name           */
#define STATUS_KEY "STATUS"			   /* ATS.INI file Key Name           */
#define DEFAULT ""					   /* Default String Value             */



/*-------------------------------------------------------------------------**
**                        General Error Descriptions                       **
**-------------------------------------------------------------------------*/

// ERROR CODE: 0
#define SUCCESS "Successful"
// ERROR CODE: 100											
#define INVALID_PARAM "ERROR - Invalid number of parameters: "					
// ERROR CODE: 101
#define INVALID_MODE  "ERROR - Invalid MODE: "									
// ERROR CODE: 102
#define INVALID_DSO   "ERROR - Unable to find or open the DSO configuration file: "	
// ERROR CODE: 103
#define INVALID_INI   "ERROR - Unable to find or open ATS.INI file: "				
// ERROR CODE: 104
#define INVALID_DSO_X "ERROR - Unable to find or open the Digital Oscilloscope executable: " 
// ERROR CODE: 105
#define MEM_ERR       "ERROR - Unable to allocate memory"
// ERROR CODE: 106
#define STATUS_ERR    "ERROR - Status key in the ATS.INI file is empty."					
// ERROR CODE: 200
#define INSTR_ERROR   "Instrument error: "							


/********************************************************************************
*									Modules							            *
********************************************************************************/

void StartDisplay(char CommLine[257])
/********************************************************************************
 **     Call the Digital Oscilloscope Soft Front Panel Executable and pass     **
 **     command line arguments to set the MODE.								   **  
 **     Stop program execution until the Digital Oscilloscope Soft Front Panel **
 **     is terminated.														   **
 ********************************************************************************/

		/* Command Line String						*/
	
{
    STARTUPINFO si;				/* Points to the STARTUPINFO Struct         */
    PROCESS_INFORMATION pi;		/* Points to the PROCESS_INFORMATION Struct */

    ZeroMemory( &si, sizeof(si) );
    si.cb = sizeof(si);
    ZeroMemory( &pi, sizeof(pi) );

	printf ("Creating Process.....\n");  //For Debugging

    // Start the process. 
    if( !CreateProcess ( 
		sScopePath,		  // Executable 
        CommLine,		  // Command line. 
        NULL,             // Process handle not inheritable. 
        NULL,             // Thread handle not inheritable. 
        FALSE,            // Set handle inheritance to FALSE. 
        0,                // No creation flags. 
        NULL,             // Use parent's environment block. 
        NULL,             // Use parent's starting directory. 
        &si,              // Pointer to STARTUPINFO structure.
        &pi )             // Pointer to PROCESS_INFORMATION structure.
    ) 

	/*NOTE: "WaitForSingleObject" always seems to fail without this printf statement.*/
	printf ("WaitForSingleObject has failed!\n");  //Debugging

    /* Wait until child process exits.   */
    WaitForSingleObject( pi.hProcess, INFINITE );
	printf ("Getting Exit Code.....\n");           //Debugging

	/* Get the Exit Code from the Digital Oscilloscope SFP */
	GetExitCodeProcess(pi.hProcess, ExitCode);
	printf ("Exit Code = %s\n", ExitCode);         //Debugging

    /* Close process and thread handles  */
    CloseHandle( pi.hProcess );
    CloseHandle( pi.hThread );
	printf ("***  Process and Thread Handles Closed  ***\n");  //Debugging
}



int file_exists(char *file_name) 
/*******************************************************************************
**                          Check for file exists                             **
********************************************************************************/

{
       struct stat file_stat;
       return( (stat(file_name, &file_stat) == 0) ? 1:0);
}



char *strtoupper(char *str)
/*******************************************************************************
**           strtoupper - Converts a char string to upper case                **
********************************************************************************/
{
	int i;
	for (i=0; str[i] != '\0'; i++)
		str[i] = toupper(str[i]);
	return(str);
}



void CloseNAM(long nCloseStatus, char sErrorStr[257])
/*******************************************************************************
**                     Report Error Results to ATLAS                          **
********************************************************************************/

{
	/* Return Results and Result String to ATLAS */
	/* ref TYX PAWS User Guide Vol 1 Sec 5 */

		printf ("Closing NAM.....\n");		//Debugging
		
		if(bMeas == TRUE) {
			// Error Code
			ResultInteger = nCloseStatus;
			vmSetInteger(ResultIntegerAddr, ResultInteger);
			// Error Description
			strcpy(ResultStr, sErrorStr);
			vmSetText(ResultStrAddr, ResultStr);
		}
		else
		{
			// Error Code
			ResultInteger = nCloseStatus;
			vmSetInteger(ResultIntegerAddr, ResultInteger);
			// Error Description
			strcpy(ResultStr, sErrorStr);
			vmSetText(ResultStrAddr, ResultStr);
		}


		/* Close virtual memory file */
		vmClose();

		printf ("Error Number: %d\n", nCloseStatus);		//Debugging 
		printf ("Error Description: %s\n", sErrorStr);		//Debugging

	exit(0);		// Exit application
}


/*******************************************************************************/
/**************        Main Program Execution begins here         **************/
/*******************************************************************************/

int main (int argc, char *argv[])
{
/*******************************************************************************/
/****	                     Declare Variables 	           	               *****/
/*******************************************************************************/

/*------------------------------------------------------------------------------
	                   Declare and Initialize Return variable  
-------------------------------------------------------------------------------*/	
	char sErrStatus[257]     =   DEFAULT;   // Returned Error Description 

/*------------------------------------------------------------------------------
                          Declare and Initialize variables
-------------------------------------------------------------------------------*/
	
	char sMode[11]			 =   DEFAULT;	// Selected Instrument Mode
	char sDSO_File[257]		 =   DEFAULT;	// DSO File Name to load
	long nErrCode			 =	 0;			// Error Code returned (Numerical Value) 
	char sErrReturn[257]	 =	 DEFAULT;	// Error Code String to pass to ATLAS 
	long nRet				 =	 0;			// Return from building Command Line Arguments 
	long nSize				 =	 0;			// Error Return from .err file size 
	long nretv               =   0;			// Error Return from "GetPrivateProfileString" 
 	char pBuffer[257]        =   DEFAULT;	// Buffer for Error Return from STATUS Key
	char sATS_INI_Path[257] =   DEFAULT;   // Full ATS.INI file path	
	char sInstrumentExec[257]=   DEFAULT;   // Full Drive\Path\Name of Instrument's SFP 
	char sSAIS_Path[257]     =   DEFAULT;   // File Path for the SAIS executables 
	char sInstrumentPath[257]=   DEFAULT;	// Instrument's Full Drive\Path\Name  
	char sErrorStr[257]      =   DEFAULT;   // Error String	
	char sCommandString[257] =   DEFAULT;   // Instrument Command String
	char sINIReturnStr[257]  =   DEFAULT;   // Return String from Instrument SAIS

	struct			           _stat buf;	// Structure to hold file info
	// char WinDir[MAX_PATH + 1];				// Character array to hold Win Directory Path  // Not Used

	char sStatus[7]			=   DEFAULT;	// Ready or Error Status
	char sStatusReturn[257] =   DEFAULT;	// Measured Value or Error Description String
	double dMeasurement     =   0;			// Measured Value to return

/****************************************************************/
/****	    Initialize the communication library			 ****/
/****************************************************************/

	/* Use Nam.h and Nam.lib to copy arguments passed arguments from ATLAS*/
	/* argv[0] = The calling program's name       */
	/* argv[1] = The name of the ATLAS TEMP file  */
	
	//MessageBox(0, "ScopeNam Debug", "Debug", 0);
	
	//MessageBox(NULL, "Scope Nam Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	/* Define ATLAS temp file name and open it */
	strcpy(TempFileName, argv[1]);
	printf( "Arg 1: %s\n", TempFileName);
	strcat(TempFileName, SUFFIX);
	
	vmOpen(TempFileName);

/****************************************************************/
/****	   Check for valid number of passed arguments 		 ****/
/****************************************************************/

	if(argc == 6) {
		bMeas = FALSE;		// DO NOT Return Measurment
	}
	if(argc == 7) {
		bMeas = TRUE;		// Return Measurement
	}
	
	// Error Check
	if(argc < 6) {
		// Too few arguments
		sprintf (sErrorStr, "%s %s",INVALID_PARAM, argc);
		CloseNAM( -100, sErrorStr);
	}

	if(argc > 7) {
		// To many arguments
		sprintf (sErrorStr, "%s %s",INVALID_PARAM, argc);
		CloseNAM( -100, sErrorStr);
	}


/****************************************************************/
/****			     Copy passed arguments					 ****/
/****************************************************************/
/*																*
 * Takes 4 arguments (from argv[2] to argv[5])		     		*
 * and assigns the address, moves address to the struct,		*
 * then copies the value to the data element.					*	
 ****************************************************************/

	/* Instrument Mode from ATLAS */
	ModeAddr = atol(argv[2]);
	vmGetText(ModeAddr, Mode, STRING_SIZE);
	strtoupper(strcpy(sMode, Mode));

	/* DSO File Name from ATLAS */
	DSO_FileAddr = atol(argv[3]);
	vmGetText(DSO_FileAddr, DSO_File, STRING_SIZE);
	strcpy(sDSO_File, strtoupper(DSO_File));

	if(bMeas == TRUE)
	{
		/**** argv[4] is used to hold the Measurement ****/
		/* The Measured Value to pass back to ATLAS */
		Meas_ResultAddr = atol(argv[4]);
		Measurement_Result = vmGetDecimal(Meas_ResultAddr);

		/**** argv[5] is used to hold the Error Number ****/
		/* The Error Number to pass back to ATLAS */
		ResultIntegerAddr = atol(argv[5]);
		ResultInteger = vmGetInteger(ResultIntegerAddr);

		/**** argv[6] is used to hold the Error Description ****/
		/* The Error Description to pass back to ATLAS */
		ResultStrAddr = atol(argv[6]);
		vmGetText(ResultStrAddr, ResultStr, STRING_SIZE);
	}
	else
	{
		/**** argv[4] is used to hold the Error Number ****/
		/* The Error Number to pass back to ATLAS */
		ResultIntegerAddr = atol(argv[4]);
		ResultInteger = vmGetInteger(ResultIntegerAddr);

		/**** argv[5] is used to hold the Error Description ****/
		/* The Error Description to pass back to ATLAS */
		ResultStrAddr = atol(argv[5]);
		vmGetText(ResultStrAddr, ResultStr, STRING_SIZE);
	}

	


/****************************************************************/
/****	      Build Path and verify ATS.INI exists			 ****/
/****************************************************************/

	/* Build ATS.ini file full path	*/	
	sprintf(sATS_INI_Path, "%s", ATSINIFILE);

	/* Verify the file exists */
	if(file_exists(sATS_INI_Path) != 1) {
 		sprintf (sErrorStr, "%s %s",INVALID_INI, sATS_INI_Path);
		CloseNAM( -103, sErrorStr);
	}

	/* Read the SAIS Path from the ATS.INI file */
	nretv = GetPrivateProfileString ( SAIS_HEADING, SAIS_PATH, 
		                              DEFAULT, pBuffer, 257, sATS_INI_Path );
	sprintf (sSAIS_Path, (char*)pBuffer);
	if(nretv == 0) {
		sprintf (sErrorStr, "%s %s",INVALID_DSO_X, sSAIS_Path);
		CloseNAM( -104, sErrorStr);
	}

/****************************************************************/
/****	 Build Path and verify Scope executable exists		 ****/
/****************************************************************/

	/* Read the DScope's Executable Name */
	nretv = GetPrivateProfileString ( SAIS_HEADING, SCOPE_KEY, 
		                              DEFAULT, pBuffer, 257, sATS_INI_Path );
	sprintf (sInstrumentExec, (char*)pBuffer);
	if(nretv == 0) {
		sprintf (sErrorStr, "%s %s",INVALID_DSO_X, sInstrumentExec);
		CloseNAM( -104, sErrorStr);
	}

	/* Build Full Drive/Path/Name to DScope Executable */
	sprintf (sScopePath, "%s\\%s",sSAIS_Path, sInstrumentExec);

   /* Verify the file exists */
   if(file_exists(sScopePath) != 1) {
		sprintf (sErrorStr, "%s %s",INVALID_DSO_X, sScopePath);
		CloseNAM( -104, sErrorStr);
   }


/****************************************************************/
/**  Check MODE argument for validity and assign TIP Argument  **/
/**  SINGLE = "TIP_RunSetup"    CONTINUOUS = "TIP_RunPersist"  **/
/****************************************************************/

	/* Check MODE SINGLE | CONTINUOUS */
	if (strcmp(sMode, "SINGLE") == 0) {
		sprintf (sMode, SINGLE);
		printf("Selected Mode 1 Prefix: %s\n", sMode);  //Debug
	}
	else if (strcmp(sMode, "CONTINUOUS") == 0) {
		sprintf (sMode, CONTINUOUS);
		printf("Selected Mode 2 Prefix: %s\n", sMode);  //Debug
	}
	else if (strcmp(sMode, "MEAS_ONLY") == 0) {
		sprintf (sMode, MEAS_ONLY);
		printf("Selected Mode 2 Prefix: %s\n", sMode);  //Debug	
	}
	else  {
		sprintf (sErrorStr, "%s %s",INVALID_MODE, sMode);
		CloseNAM( -101, sErrorStr);
	}


/****************************************************************/
/**    Verify DSO file, open, read, and populate "CMD" key     **
/****************************************************************/

	/* Verify file name includes ".DSO"  */		
	if (strstr(sDSO_File, ".DSO") == 0) {
		/* Execution was Successful */
 		sprintf (sErrorStr, "%s %s",INVALID_DSO, sDSO_File);
		CloseNAM( -102, sErrorStr);
	}

	/* Make sure the DSO file exists before attempting to open */
    if(file_exists(sDSO_File) != 1)	{ 
 		sprintf (sErrorStr, "%s %s",INVALID_DSO, sDSO_File);
		CloseNAM( -102, sErrorStr);		
	}

	/* Open .DSO File for Input */
	filepointer = fopen(sDSO_File, "r");

	/* Verify File is not empty  */
	if (filepointer == (FILE *) NULL) {
 		sprintf (sErrorStr, "%s %s",INVALID_DSO, sDSO_File);
		CloseNAM( -102, sErrorStr);
	}

	/* Get data associated with .DSO File */
	_stat(sDSO_File, &buf);
	
	/* Allocate Memory to read file into */
	if ((char_line = (char *) malloc(buf.st_size)) == NULL){
		sprintf (sErrorStr, "%s",MEM_ERR);
		CloseNAM(-105, sErrorStr);
	}

	/* Read Command String from .DSO File */
	fgets (char_line, buf.st_size - 1, filepointer);
	strcpy(sCommandString,char_line);

	/* Write the Instrument Command String to the CMD Key in the ATS.INI file */
	nretv = WritePrivateProfileString ( TIPS_HEADING, CMD_KEY, 
		                                sCommandString, sATS_INI_Path );

/****************************************************************/
/**             Launch Scope Soft Front Panel                  **
/****************************************************************/
	
/*-----------------------------------------------------------------*/
/* NOTE: When reading the passed command line from Visual Basic    */
/*       the first Argument it sees is the second argument passed, */
/*       therefore, the first argument passed is the NAM Name.     */
/*-----------------------------------------------------------------*/

	/* Build Command Line String */
	sprintf( sCommLine, "ScopeNAM %s",sMode);
	
	/* Launch Digital Oscilloscope Soft Front Panel and configure  */	
	StartDisplay(sCommLine);

/*******************************************************************
***        Proccess the STATUS key in the ATS.ini file          ***
*********************************************************************/

	nretv = GetPrivateProfileString ( TIPS_HEADING, STATUS_KEY, DEFAULT, 
		                              pBuffer, 257, sATS_INI_Path );
	/* Check for Success in returning a value  */
	if(nretv == 0) {
		sprintf (sErrorStr, "%s",STATUS_ERR);
    	CloseNAM( -106, sErrorStr);
	}

	/* Assign the Return Value to sINIReturnStr  */
	sprintf (sINIReturnStr, (char*)pBuffer);

	// Copy the first 5 characters to String "sStatus"
	strncpy ( sStatus, sINIReturnStr, 5);

	// Convert to Upper Case for analysis  
	sprintf (sStatus, strtoupper(sINIReturnStr));

	// Check Status for "READY"  		
	if (strstr(sStatus, "READY") != 0) {
		
		if (bMeas == TRUE)	{

            //Verify there is a valid Return Measurement
			if(strlen(sINIReturnStr) < 7) {
				sprintf (sErrorStr, "%s",INSTR_ERROR);
    			CloseNAM( -200, sErrorStr);
			}
			
			//If "Ready", retrieve the measured value.
			strncpy ( sStatusReturn, &sINIReturnStr[6], (strlen(sINIReturnStr) - 6) );
			
			//Convert measured value to a double
			dMeasurement = atof(sStatusReturn);
			
			printf ( "Returned Measurement: %20.20f\n", dMeasurement );    //For Debugging
			//Return Measurement
			Measurement_Result = dMeasurement;
			vmSetDecimal(Meas_ResultAddr, Measurement_Result);

			/* Execution was Successful */
			printf ("Success!!, Return String = %s,\n",sINIReturnStr);     //For Debugging
			CloseNAM( 0, SUCCESS);

		}
		else
		{
			/* Execution was Successful */
			printf ("Success!!, Return String = %s,\n",sINIReturnStr);     //For Debugging
			CloseNAM( 0, SUCCESS);
		}

	}
	else
	{

		// Failed, retreive Error String...
		// An Error occurred in execution, Report Instrument Error 
		sprintf (sErrorStr, "%s %s",INSTR_ERROR, sINIReturnStr);
		CloseNAM( -200, sErrorStr);	
	}


	/**  Close NAM and exit  **/
	CloseNAM( 0, SUCCESS);
	exit(0);

}

