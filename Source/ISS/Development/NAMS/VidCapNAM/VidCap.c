/***************************************************************************
**	                       Video Capture NAM						      **
****************************************************************************
**  VidCapNAM.c															  **
**																		  **
**	TETS-ECP-039     Dave Joiner  07/01/02								  **
**																		  **
**  Allows the User to set the Video Capture Card Configuration and Mode  **		
**  via ATLAS. As an optional argument (Empty String from ATLAS), the     **
**  user can also pass Operator instructions to the Video Capture Display.**
**  "INSTRUCTIONS" is required with ATLAS, but optional in DOS mode.      **
**	The NAM will stop execution until the Video Display is closed. The    **
**  Error code from the Video Display is read from the "VidErr.err" file. **
**  The Error Code is then converted to a positive value and the Error    **
**  Description is looked up. Both the Err Number and the Err Description **
**  are returned to ATLAS.											      **
**  This program can also be used from the DOS Command Prompt by passing  **
**  "DOS" as the first argument.										  **
***************************************************************************/
//**************************************************************************
//	Arguments to pass - "INSTRUCTIONS MUST BE PASSED IN QUOTES IF STRING 
//  CONTAINS A SPACE.
//		
//	TYPE			= argv[2]	/* RS-170 | RS-343A or Drive\Path\FileName */
//	MODE			= argv[3]	/* CONTINUOUS | SINGLE */
//  INSTRUCTIONS	= argv[4]   /* Operator Instructions */	
//	RESULT			= argv[5]	/* Error Code */
//  RESULTSTR		= argv[6]   /* Error Description */
//**************************************************************************



/***************************************************************************
*								Include Files							   *
****************************************************************************/

#include		<stdlib.h>
#include		<stdio.h>
#include		<Windows.h>
#include		<winbase.h>
//#include		<iostream.h>
#include		<ctype.h>
#include		"Shlwapi.h"
#include		"nam.h"

/***************************************************************************
*								Constants			     		           *
***************************************************************************/

#define SUFFIX ".TMP"
#define INIFILE "C:\\USR\\TYX\\BIN\\VidErr.err"
#define HEADING "NAMError"
#define KEYNAME "ErrCode"
#define DEFAULT ""
#define DISPLAYFILE "C:\\Program Files (x86)\\ATS\\ISS\\Bin\\VidDisplay.exe"
#define DOS "DOS"

#define SMALL_STRING 256
#define MEDIUM_STRING 1024
#define LARGE_STRING 4096


/***************************************************************************
*									Globals		      			           *
***************************************************************************/


/* TYX data structure - ref. TYX PAWS User Guide Vol 1 Sec 5 */ 
int g_iResults;

char g_sResultStr[SMALL_STRING];
char g_sType[SMALL_STRING];
char g_sMode[SMALL_STRING];
char g_sInstruction[LARGE_STRING];


/* Program Globals */
char		TempFileName[40];	/* Temporary File Name for ATLAS	*/
long		TypeAddr;			/* Arg "Type" Address				*/
long	    ModeAddr;		    /* Arg "Mode" Address				*/
long	    InstructionAddr;	/* Arg "Instruction" Address		*/
long	    ResultsAddr;		/* Arg "Results" Address			*/
long		ResultStrAddr;      /* Arg "ResultsStr" Address         */
int			ATLAS = 1;			/* 0 if NOT using ATLAS, 1 if using ATLAS  */
char    	sCommLine[542];		/* Command Line Argument */


/********************************************************************************
*									Modules							            *
********************************************************************************/

void StartDisplay(char CommLine[542])
/********************************************************************************
 **     Call the Video Display and pass command line aurguments to provide     **  
 **     configuration settings. Stop program execution until the Video		   **
 **     Display application is terminated.                                     **
 ********************************************************************************/
{
    STARTUPINFO si;				/* Points to the STARTUPINFO Struct */
    PROCESS_INFORMATION pi;		/* Points to the PROCESS_INFORMATION Struct */

    ZeroMemory( &si, sizeof(si) );
    si.cb = sizeof(si);
    ZeroMemory( &pi, sizeof(pi) );

    // Start the process. 
    if( !CreateProcess ( 
		DISPLAYFILE,      // Executable 
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
	printf ("WaitForSingleObject.....");  //For Debugging
    // Wait until child process exits.
    WaitForSingleObject( pi.hProcess, INFINITE );

    // Close process and thread handles. 
    CloseHandle( pi.hProcess );
    CloseHandle( pi.hThread );
}


void CloseNAM(long CloseStatus, char ErrorStr[257])
/*******************************************************************************
**                  Report Error Results to ATLAS                             **
********************************************************************************/
{
	/* Return Results and Result String to ATLAS */
	/* ref TYX PAWS User Guide Vol 1 Sec 5 */
	if (ATLAS){
		g_iResults = CloseStatus;
		strcpy(g_sResultStr, ErrorStr);
		vmSetInteger(ResultsAddr, g_iResults);
		vmSetText(ResultStrAddr, g_sResultStr);

		/* Close virtual memory file */
		vmClose();
	}
	else
	{
		printf ("%s", ErrorStr);
	}
	exit (0);
}

/*******************************************************************************/
/**************        Main Program Execution begins here         **************/
/*******************************************************************************/

int main (int argc, char *argv[])
{
	
/*******************************************************************************
 ****	                     Delcare Variables 	           	               *****
 *******************************************************************************/
	/* Declare and Initialize Return variable  */	
	char sErrStatus[257]     =   "";

	/* Declare and Initialize variables  */
	char sType[257]			=   "";		/* Video Configuration Type */
	char sMode[11]			=   "";     /* Video Configuration Mode */
	char sInstruction[501]	=   "";		/* Operator Instructions to pass */
	long nErrCode			=	 0;		/* Error Code returned (Numerical Value) */
	char sErrReturn[257]	=	"";		/* Error Code String to pass to ATLAS */
	char sCodeout[33]       =   "";		/* Error Code returned (String Value) */
	long nRet				=	 0;		/* Return from building Command Line Arguments */
	long nSize				=	 0;		/* Error Return from .err file size */
	long nretv              =    0;		/* Error Return from "GetPrivateProfileString" */
 	char pBuffer[33]        =   "";		/* Buffer for Error Return from .err file */
	char sArgString[26]     =   "";		/* Truncated Argument String */
    int i;
	
/* Check number of Arguments Passed */
	
	//MessageBox(NULL, "Vid Cap Nam Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
		
	/* Rule: if calling from DOS, use "DOS" as the first argument  *
	 * This is used to Flag NAM in DOS Mode                    */

	if (_strnicmp (argv[1], DOS, 3) !=0) ATLAS = 1; else ATLAS = 0;

	printf ( "argc = %d\n", argc );
	printf ( "argv[1] = %s\n", argv[1] );
	if (ATLAS)
	{
		if (argc != 7) 		/* If not 7 Arguments, then it's not ATLAS. */
		{
			printf ( "ATLAS\n" );
			CloseNAM( -100, "Invalid number of parameters.\n");
		}
	}
	else		//DOS Mode
	{
		if (argc < 4 || argc > 5)		/* If less than 4 or greater than 6 Arguments, INVALID.  */
		{
			printf ( "DOS\n" );
			CloseNAM( -100, "Invalid number of parameters.\n");
		}
	}

			if (strlen(argv[2]) > 256)
		{
			StrCpyN(sArgString,argv[2],25);
			sprintf(sErrReturn, "Invalid parameter length: %s", sArgString);
			CloseNAM( -104, sErrReturn);
		}
	

		if (strlen(argv[3]) > 11)
		{
			StrCpyN(sArgString,argv[3],25);
			sprintf(sErrReturn, "Invalid parameter length: %s", sArgString);
			CloseNAM( -104, sErrReturn);
		}

	if (argc > 4)		//Test for optional argument from DOS
	{
			if (strlen(argv[4]) > 500)
		{
			StrCpyN(sArgString,argv[4],25);
			sprintf(sErrReturn, "Invalid parameter length: %s", sArgString);
			CloseNAM( -104, sErrReturn);
		}
	}

if (ATLAS)
{
	printf ( "Executing in ATLAS Mode.\n" );
}
else
{
		printf ( "Executing in DOS Mode.\n" );
}

printf ( "argc = %d\n", argc );
for (i = 0; i < argc; i++ )
	printf ( "Argument %d = %s\n", i, argv[i] );
	

	/* Execute ATLAS specific */ 
	if (ATLAS)	/* Use ATLAS */
	{

/****************************************************************/
/****	    Initialize the communication library			 ****/
/****************************************************************/

	/* argv[0] = The calling program's name       */
	/* argv[1] = The name of the ATLAS TEMP file  */

	/* Define ATLAS temp file name and open it */
	strcpy(TempFileName, argv[1]);
	strcat(TempFileName, SUFFIX);

	/* Open Atlas Virtual Memory Space */
	vmOpen(TempFileName);

/****************************************************************/
/****			     Copy passed arguments					 ****
/****************************************************************/
/*																*
 * Takes 5 arguments (from argv[2] to argv[6])		     		*
 * and assigns the address, moves address to the struct,		*
 * then copies the value to the data element.					*	
 ****************************************************************/


		/* Video Type from ATLAS */
		TypeAddr = atol(argv[2]);
		vmGetText(TypeAddr, g_sType, SMALL_STRING);
		strcpy(sType, g_sType);
	
		/* Video Mode from ATLAS */
		ModeAddr = atol(argv[3]);
		vmGetText(ModeAddr, g_sMode, SMALL_STRING);
		strcpy(sMode, g_sMode);
					
    	/* Operator Instructions from ATLAS */
		InstructionAddr = atol(argv[4]);
		vmGetText(InstructionAddr, g_sInstruction, LARGE_STRING);
		strcpy(sInstruction, g_sInstruction);
		
		/**** argv[5] is used to hold the Error Number ****/
	    /* The Error Number to pass back to ATLAS */
		ResultsAddr = atol(argv[5]);
		g_iResults = vmGetInteger(ResultsAddr);

		/**** argv[6] is used to hold the Error Description ****/
	    /* The Error Description to pass back to ATLAS */
		ResultStrAddr = atol(argv[6]);
		vmGetText(ResultStrAddr, g_sResultStr, SMALL_STRING);

				
	}
	else	/* ATLAS = 0, called from a DOS command line  */
	{
		printf("argc = %d\n",argc);				/* For Debugging from DOS Prompt */
		
		/**** If from DOS, Copy passed these arguments ****/
		// argv[0] = Calling program Drive\Path\FileName
		// argv[1] = "DOS" (Flag to signal DOS Mode)

		/* Video Type */
		strcpy(sType, (argv[2]));
		printf("argv[2] = %s\n",sType);		/* For Debugging from DOS Prompt */

		/* Video Mode */
		strcpy(sMode, (argv[3]));
		printf("argv[3] = %s\n",sMode);		/* For Debugging from DOS Prompt */

		/* If only 3 Command line arguments were sent from DOS, assign
	       an empty string to sInstructions.                           */
		if (argc == 4)
		{
			sprintf( sInstruction, "");		/* No Operator Instructions */
		}
			else
		{
			/* Operator Instructions */
			strcpy(sInstruction, (argv[4]));
			printf("argv[4] = %s\n",sInstruction);	/* For Debugging from DOS Prompt */
		}
	}		

	/* Build Command Line Argument to pass to VidDisplay */
	/* VidCapNAM.exe is used as the first argument arg[0]. */
	nRet = sprintf( sCommLine, "VidCapNAM.exe \"%s\" %s \"%s\"", sType, sMode, sInstruction);

	if (!ATLAS)		/* For Debugging from DOS Prompt */
	{
		printf ("Command Line = %s\n", sCommLine);
	}
	// Launch program Video Display Program	
	StartDisplay(sCommLine);

/*******************************************************************
***            Get Display Error Code from ini file              ***
*********************************************************************/

	nretv = GetPrivateProfileString ( HEADING, KEYNAME, DEFAULT, pBuffer, 257, INIFILE );  
	
	if (!ATLAS)		/* For Debugging from DOS Prompt */
	{
		printf ( "GetPrivateProfileString Return Value = %d\n", nretv);		
	}

	sprintf (sCodeout, (char*)pBuffer);
	nErrCode = atol(sCodeout);	/* Convert Error Code from the ini file to data type long */

	printf ( "Error Code Returned = %d\n", nErrCode);
	
	if (nErrCode != 0)		/* If not Successful */
	{
	
		if (nErrCode == -101)   //Invalid/Not found File name in "TYPE" argument
		{
			/* Check for a "\" character. If there is a Drive\Path\FileName do not add ".cnf".*/
			if ( strchr(sType, '\\' ) != NULL)
				sprintf(sErrReturn, "Invalid TYPE: %s file not found or invalid.",sType);
			else
				sprintf(sErrReturn, "Invalid TYPE: %s.cnf file not found or invalid.",sType);
			
			CloseNAM(nErrCode, sErrReturn);

		}

		if (nErrCode == -102)	//Invalid "MODE" parameter
		{

			sprintf(sErrReturn, "Invalid MODE: %s.",argv[3]);
			CloseNAM(nErrCode, sErrReturn);

		}


		if (nErrCode == 103)	//Invalid Acquistion Module
		{

			sprintf(sErrReturn, "Invalid Acquistion Module.");
			CloseNAM(nErrCode, sErrReturn);

		}
	}
	/* If all other Error traps failed, Application was successful */
	CloseNAM( nErrCode, "Successful");
}
		
		
