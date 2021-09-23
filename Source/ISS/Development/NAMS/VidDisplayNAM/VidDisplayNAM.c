/***************************************************************************
**                        Video DisplayNAM 					              **
****************************************************************************
**  VidDisplayNAM.c														  **
**																		  **
**	Written By:     Robert Giumarra  06/16/07							  **
**																		  **
**  Purpose:                                                              **
**  Provides an ATLAS interface for the Video Tools Application. This NAM **
**  will accept four different modes.                                     **
**                                                                        **
**  [FOV] - (Field-of-View)                                               **
**          This mode accepts (7) arguments IN and (4) arguments OUT. If  **
**          Instructions, HelpFile, or HelpText are not used, an empty    **
**          string must be passed. This mode will open the VidTools       **
**			Executable, Capture an image, and allow the user to draw an   **
**          ROI box around the target. The Field-of-View is then          **
**          calculated based on the Target Dimensions passed. These two   **
**          values are then passed back to ATLAS as Horizontal            **
**          Field-of-View and Vertical Field-of-View, unit of measure     **
**          will be in Radians.                                           **
**  [TGTCOORD] - (Target Coordinates)                                     **
**          This mode accepts (5) arguments IN and (6) arguments OUT.     **
**          It will open the VidTools executable, Capture an Image, and   **
**          allow the user draw an ROI box around the target. Once        **
**          accepted, the Start X,Y and End X,Y coordinates are returned  **
**          to ATLAS in Pixel positions.                                  **
**  [ALIGN] - (Alignment)                                                 **
**  [DISPLAY_ONLY] - (No Mode, Video)                                     **
**          These modes accept (3) arguments IN and (2) arguments OUT. In **
**          ALIGN mode the VidTool executable is opened allowing the user **
**          to see   a continuously updated image with a Cross Hair       **
**          overlay.                                                      **
**          In NOMODE mode the VidTool executable is opened allowing the  **
**          user to see a continuously updated image.                     **
**                                                                        **
**          TYPE - Will accept a Video Configuration File Name (This is   **
**                 a required field). The file name may be passed without **
**                 a path and extension if it's located in the            **
**                 "C:\USR\TYX\BIN\VidConfig" directory (This directory   **
**                 contains all configurations supported by IR Windows).  **
**                 This argument may also be passed with                  **
**                 "Drive\Path\Name" information and will accept spaces   **
**                 in the path.                                           **
**          Instructions, HelpFile, and HelpText arguments will also      **
**          accept spaces.                                                **
**																		  **
***************************************************************************/
//**************************************************************************
//	Arguments - "STRINGS MUST BE PASSED IN QUOTES IF THEY CONTAIN A SPACE."
//**************************************************************************
//**     This NAM will execute in four different modes, arguments are     **
//**     dependant upon the MODE argument passed.	                      **
//**************************************************************************
//
//	MODE			= argv[2]	/* FOV | TGTCOORD | ALIGN | DISPLAY_ONLY
//
/******************************  FOV MODE  *********************************
//----- IN ------
//	TYPE			= argv[3]	/* Video Configuration File
//  INSTRUCTIONS	= argv[4]   /* Operator Instructions (FoV Form)
//  HelpFile        = argv[5]   /* Help Graphic File to display
//  HelpText        = argv[6]   /* Text to display with the Graphic
//  TargDimX        = argv[7]   /* Horizontal Target Dimension (mRads)
//  TargDimY        = argv[8]   /* Vertical Target Dimension   (mRads)
//------ OUT ------
//  HorzFOV         = argv[9]   /* Horizontal Field of View in Radians
//  VertFOV	        = argv[10]  /* Vertical Field of View in Radians
//	RESULT			= argv[11]	/* Error Code 
//  RESULTSTR		= argv[12]  /* Error Description
//
/****************************  TGTCOORD MODE  ******************************
//----- IN ------
//	TYPE			= argv[3]	/* Video Configuration File
//  INSTRUCTIONS	= argv[4]   /* Operator Instructions (FoV Form)
//  HelpFile        = argv[5]   /* Help Graphic File to display
//  HelpText        = argv[6]   /* Text to display with the Graphic
//------ OUT ------
//  StartX          = argv[7]   /* Starting Horizontal point of ROI Box
//  StartY          = argv[8]   /* Starting Vertical point of ROI Box
//  EndX            = argv[9]   /* Ending Horizontal point of ROI Box
//  EndY	        = argv[10]  /* Ending Vertical point of ROI Box
//	RESULT			= argv[11]	/* Error Code 
//  RESULTSTR		= argv[12]  /* Error Description
//
/*****************************  ALIGN MODE  ********************************
//----- IN ------
//	TYPE			= argv[3]	/* Video Configuration File
//  INSTRUCTIONS	= argv[4]   /* Operator Instructions (Alignment Form)
//  HelpFile        = argv[5]   /* Help Graphic File to display
//  HelpText        = argv[6]   /* Text to display with the Graphic
//------ OUT ------
//	RESULT			= argv[7]	/* Error Code 
//  RESULTSTR		= argv[8]   /* Error Description
//
/*************************  DISPLAY_ONLY MODE  *****************************
//----- IN ------
//	TYPE			= argv[3]	/* Video Configuration File
//  INSTRUCTIONS	= argv[4]   /* Operator Instructions (Video Capture)
//------ OUT ------
//	RESULT			= argv[5]	/* Error Code 
//  RESULTSTR		= argv[6]   /* Error Description
//  
//**************************************************************************
//
/***************************************************************************
*								Include Files							   *
****************************************************************************/

#include		<stdlib.h>
#include		<stdio.h>
#include		<string.h>
#include		<Windows.h>
#include		<winbase.h>
#include		<sys/stat.h>
#include		"nam.h"


/***************************************************************************
*								Constants			     		           *
***************************************************************************/

#define SUFFIX      ".TMP"

#define ATSINIFILE "C:\\USERS\\PUBLIC\\DOCUMENTS\\ATS\\ATS.INI"	   /* ATS.INI file Key Name           */
#define DATFILE     "C:\\USR\\TYX\\BIN\\Video.dat"
//#define DISPLAYFILE "C:\\Program Files (x86)\\ATS\\ISS\\bin\\VidTool.exe"

#define NAMERROR    "NAMError"
#define ERRCODE     "ErrCode"
#define ERRSTR      "ErrString"

#define RETDATA     "Return_Data"
#define STARTX      "X1"
#define STARTY      "Y1"
#define ENDX        "X2"
#define ENDY        "Y2"
#define HORZFOV     "Hfov"
#define VERTFOV     "Vfov"

#define INSTRUCT    "Instructions"
#define OP_INSTRUT	"OpInstructions"
#define HELP_TEXT	"HelpText"
#define NoHelpFile  "NONE"

#define DEFAULT     ""

#define FOV				0
#define TGTCOORD		1
#define ALIGN			2
#define DISPLAY_ONLY    3

#define SMALL_STRING	256
#define MEDIUM_STRING   1024
#define LARGE_STRING    4096


/***************************************************************************
*									Globals		      			           *
***************************************************************************/


/* TYX data structure - ref. TYX PAWS User Guide Vol 1 Sec 5 */


int Result;

int iStartX;
int iStopX;
int iStartY;
int iStopY;

double fHorzFoV_EndX;
double fVertFoV_EndY; 

char sTDimX_TopX_HFile[SMALL_STRING];
char sTDimY_TopY_HText[SMALL_STRING];
char sHorzFoV_EndX[SMALL_STRING];
char sVertFoV_EndY[SMALL_STRING]; 

char Mode[SMALL_STRING];
char Type[SMALL_STRING];
char Instructions[LARGE_STRING];
char HelpFile_Result[SMALL_STRING];
char HelpText_ResultStr[SMALL_STRING];
char ResultStr[SMALL_STRING];


/* Program Globals */
char		TempFileName[SMALL_STRING];	     /* Temporary File Name for ATLAS	 */

long	    ModeAddr;		         /* Arg "Mode" Address				 */
long		TypeAddr;			     /* Arg "Type" Address				 */
long	    InstructionsAddr;	     /* Arg "FovInstructions" Address	 */
long		HelpFile_ResultAddr;     /* Arg "HelpFile_Result" Address    */
long        HelpText_ResultStrAddr;	 /* Arg "TDimX_TopX_HFile" Address   */
long		TDimX_TopX_HFileAddr;	 /* Arg "TargDimX_StartX" Address    */
long		TDimY_TopY_HTextAddr;	 /* Arg "TDimY_TopY_HText" Address   */
long		HorzFoV_EndXAddr;		 /* Arg "HorzFoV_EndXAddr" Address   */
long        VertFoV_EndYAddr;		 /* Arg "VertFoV_EndYAddr" Address   */
long	    ResultAddr;		         /* Arg "Result" Address			 */
long		ResultStrAddr;           /* Arg "ResultStr" Address          */

char    	sCommLine[MEDIUM_STRING];		     /* Command Line Argument            */
char		sInstrumentExec[SMALL_STRING]=  DEFAULT;   // Full Drive\Path\Name of Instrument's SFP 

/***************************************************************************
*	                     Error Codes and Definitions				       *
***************************************************************************/

// ERROR CODE: 0
#define SUCCESS          "Successful"
//ERROR CODE: -100
#define INVALID_PARAM    "ERROR - Invalid number of parameters: "
//ERROR CODE: -101
#define INVALID_MODE     "ERROR - Invalid MODE: "
//ERROR CODE: -102
#define INVALID_EXE      "ERROR - Unable to find the Display executable: "
//ERROR CODE: -103
#define INVALID_KEY      "ERROR - Data key empty, no data retrieved. "
//ERROR CODE: -104
#define INVALID_DATFILE  "ERROR - Unable to find the Data File: "
//ERROR CODE: -105
#define EXE_ERROR        "ERROR - Display Execution Error: "
//ERROR CODE: -106
#define DISPLAY_ERROR    "ERROR - Display Error: "
//ERROR CODE: -107
#define EPIX_ERROR       "ERROR - Epix Error: "
//ERROR CODE: -108
#define INVALID_RANGE    "ERROR - Target Dimension outside range of 0 - 24 mRads."
// ERROR CODE: -109
#define INVALID_INI   "ERROR - Unable to find or open ATS.INI file: "				


#define SAIS_HEADING "SAIS"			/* ATS.INI file Section Heading    */
#define VIDTOOL_KEY  "EOV"			/* ATS.INI file Key Name           */
#define SAIS_PATH "PATH"			/* ATS.INI file Section Heading    */

/********************************************************************************
*									Modules							            *
********************************************************************************/

void StartDisplay(char CommLine[MEDIUM_STRING])
/********************************************************************************
 **     Call the Video Display and pass command line arguments to provide      **  
 **     configuration settings. Stop program execution until the Video		   **
 **     Display application is terminated.                                     **
 ********************************************************************************/	
{
    STARTUPINFO si;				/* Points to the STARTUPINFO Struct */
    PROCESS_INFORMATION pi;		/* Points to the PROCESS_INFORMATION Struct */
	BOOL x = FALSE;
    ZeroMemory( &si, sizeof(si) );
    si.cb = sizeof(si);
    ZeroMemory( &pi, sizeof(pi) );

	    // Start the process. 
    if( !CreateProcess ( 
		sInstrumentExec,  // Executable 
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
    // Wait until child process exits.
    WaitForSingleObject( pi.hProcess, INFINITE );

    // Close process and thread handles. 
    CloseHandle( pi.hProcess );
    CloseHandle( pi.hThread );
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



int file_exists(char *file_name)
/*******************************************************************************
**                           Check for File Exists                            **
********************************************************************************/
{
	struct stat file_stat;
	return( (stat(file_name, &file_stat) == 0) ? 1:0);
}



void CloseNAM(long CloseStatus, char *ErrorStr)
/*******************************************************************************
**                  Report Error Results to ATLAS                             **
********************************************************************************/
{
	/* Return Results and Result String to ATLAS */

	Result = CloseStatus;
	strcpy(ResultStr, ErrorStr);
	vmSetInteger(ResultAddr, Result);
	vmSetText(ResultStrAddr, ResultStr);

	/* Close virtual memory file */
	vmClose();
	printf ("%s", ErrorStr);

	//Exit NAM
	exit(0);

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
	char    sErrStatus[257]     =   DEFAULT;    /* Error Description String                   */

	/* Declare and Initialize variables  */
	char    sMode[10]			=   DEFAULT;    /* Video Configuration Mode                    */
	char    sType[257]			=   DEFAULT;	/* Video Configuration Type                    */
	char    sInstructions[500]	=   DEFAULT;	/* Operator Instructions to pass               */
	char    sHelpFile[257]      =   DEFAULT;    /* Help Graphic File Name and Path             */
	char    sHelpText[257]      =   DEFAULT;    /* Help Text to display with Graphic           */
	
	double  dHorzFoV			=    0;		/* The Horizontal F-o-V returned (Rad)         */
	double  dVertFoV            =    0;		/* The Vertical F-o-V returned (Rad)           */	
    long    nEndX               =    0;     /* The End X point returned                    */
	long    nEndY               =    0;     /* The End Y point returned                    */
	char    sTargDimX[10]       =   DEFAULT;	/* Width of the Target (mRad)				   */
	char    sTargDimY[10]       =   DEFAULT;	/* Height of the Target (mRad)				   */
	long    nStartX			    =	 0;		/* Starting X coordinate					   */
	long    nStartY			    =	 0;		/* Starting Y coordinate					   */
	
	long    nErrCode			=	 0;		/* Error Code returned (Numerical Value)       */
	char    sErrReturn[257]	    =	DEFAULT;	/* Error Code String to pass to ATLAS          */
	char    sErrString[257]     =   DEFAULT;    /* Used to Build Error String.                 */

	long    nRet				=	 0;		/* Return from building Command Line Arguments */
	long    nSize				=	 0;		/* Error Return from .err file size            */
	long    nretv               =    0;		/* Function Return Value                       */
	char    sCodeout[33]        =   DEFAULT;		/* Error Code returned (String Value)          */
 	char    pBuffer[257]        =   DEFAULT;		/* Buffer for Error Return from .dat file      */
	int     iMode				=   -1;     /* Mode identifier                             */
	
	char sATS_INI_Path[SMALL_STRING]  =  DEFAULT;   // Full ATS.INI file path	
	char sSAIS_Path[SMALL_STRING]     =  DEFAULT;   // File Path for the SAIS executables 

	//debug
	//MessageBox(0, "VidDisplayNAM Debug", "VidDisplayNAM Debug", 0);
	
	
	//MessageBox(NULL, "Vid Display Nam Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	/** Minimum Arguments passed = 7 in either Alignment or No Mode  **/	
	/** Maximum number of arguments passed = 13 in either FOV or ALIGN Mode **/

	if (argc < 7) {
		printf("Error -100: Too few COMMAND LINE arguments.\n");
		sprintf( sErrString, "%s %d arguments passed.", INVALID_PARAM, (argc - 2)); 
		CloseNAM( -100, sErrString );
	}
	if (argc > 13) {
		printf("Error -100: Too many COMMAND LINE arguments.\n");
		sprintf( sErrString, "%s %d arguments passed.", INVALID_PARAM, (argc - 2)); 
		CloseNAM( -100, sErrString );
	}


/****************************************************************/
/****	      Build Path and verify ATS.INI exists			 ****/
/****************************************************************/

	/* Build ATS.ini file full path	*/	
	sprintf(sATS_INI_Path, "%s", ATSINIFILE);

	/* Verify the file exists */
	if(file_exists(sATS_INI_Path) != 1) {
 		sprintf (sErrString, "%s %s",INVALID_INI, sATS_INI_Path);
		CloseNAM( -109, sErrString);
	}

	/* Read the SAIS Path from the ATS.INI file */
	nretv = GetPrivateProfileString ( SAIS_HEADING, SAIS_PATH, 
		                              DEFAULT, pBuffer, 257, sATS_INI_Path );
	sprintf (sSAIS_Path, (char*)pBuffer);
	if(nretv == 0) {
		sprintf (sErrString, "%s %s",sErrString, sSAIS_Path);
		CloseNAM( -110, sErrString);
	}

/****************************************************************/
/****	 Build Path and verify Scope executable exists		 ****/
/****************************************************************/

	/* Read the VidTool Executable Name */
	nretv = GetPrivateProfileString ( SAIS_HEADING, VIDTOOL_KEY, 
		                              DEFAULT, pBuffer, 257, sATS_INI_Path );
	sprintf (sInstrumentExec, (char*)pBuffer);
	if(nretv == 0) {
		sprintf (sErrString, "%s %s",INVALID_EXE, sInstrumentExec);
		CloseNAM( -102, sErrString);
	}

   /* Verify the file exists */
   if(file_exists(sInstrumentExec) != 1) {
		sprintf (sErrString, "%s %s",INVALID_EXE, sInstrumentExec);
		CloseNAM( -102, sErrString);
   }

/****************************************************************/
/****	    Initialize the communication library			 ****/
/****************************************************************/

	/* argv[0] = The calling program's name       */
	/* argv[1] = The name of the ATLAS TEMP file  */

	/* Define ATLAS temp file name and open it */
	strcpy(TempFileName, argv[1]);
	strcat(TempFileName, SUFFIX);


	/*Open ATLAS Virtual Memory Space*/
	vmOpen(TempFileName);

/****************************************************************/
/****			    Determine Display Mode					 ****
/****************************************************************/
		
		/* Find out Display Mode is from ATLAS */
		ModeAddr = atol(argv[2]);
		vmGetText(ModeAddr, Mode, SMALL_STRING);
		/* Convert Mode to upper case before going into switch */
		strtoupper(strcpy(sMode, Mode));

/****************************************************************/
/****     Copy passed arguments and Build Command Line		 ****
/****************************************************************/	
		
  /*-----------------  Common to all Modes  -----------------*/

		/* Video Type from ATLAS */
		TypeAddr = atol(argv[3]);
		vmGetText(TypeAddr, Type, SMALL_STRING);
		strcpy(sType, Type);
		
   		/* Operator Instructions from ATLAS */
		InstructionsAddr = atol(argv[4]);
		vmGetText(InstructionsAddr, Instructions, LARGE_STRING);
		strcpy(sInstructions, Instructions);
		
  /*----------------------------------------------------------*/


		
/****************************************************************/
/****					Determine Mode		                ****
/****************************************************************/		
		if (strcmp(sMode, "FOV") == 0) 
				iMode = FOV;
		
		else if (strcmp(sMode, "TGTCOORD") == 0) 
				iMode = TGTCOORD;
		
		else if (strcmp(sMode, "ALIGN") == 0) 
				iMode = ALIGN;
		
		else if (strcmp(sMode, "DISPLAY_ONLY") == 0) 
				iMode = DISPLAY_ONLY;
		
		else {
			//---------- Unrecognized mode ----------
			sprintf( sErrString, "%s %s mode passed.", INVALID_MODE, sMode);
			CloseNAM( -101, sErrString );
		}


	//Find out if the Data File exists, if so delete it
	if (file_exists (DATFILE) == 1)  {
		// Start with a clean Data File
		remove (DATFILE);
	}

	// Insert Error Code: -105 (Display Execution Error)
	nretv = WritePrivateProfileString(NAMERROR, ERRCODE, "-105", DATFILE);

	if (nretv == 0) {
		printf ("ERROR writing to Data File: %s  \n",DATFILE);
		sprintf( sErrString, "%s %s", INVALID_DATFILE, DATFILE);
		CloseNAM( -104, sErrString );
	}
	// Insert Error Description (Display Execution Error)
	nretv = WritePrivateProfileString(NAMERROR, ERRSTR, EXE_ERROR, DATFILE);
	if (nretv == 0) {
		printf ("ERROR writing to Data File: %s  \n",DATFILE);
		sprintf( sErrString, "%s %s", INVALID_DATFILE, DATFILE);
		CloseNAM( -104, sErrString );
	}	

/****************************************************************/
/****	    Assign passed arguments based on Mode	        ****
/****	    and build Command Line.	                        ****
/****************************************************************/
				
		switch(iMode)   {


			case FOV:
				//---------- Field of Veiw Mode ----------

   	            /* Help Graphic Name/Path from ATLAS */
		        HelpFile_ResultAddr = atol(argv[5]);
				vmGetText(HelpFile_ResultAddr, HelpFile_Result, SMALL_STRING);
				strcpy(sHelpFile, HelpFile_Result);

  	            /* Help Graphic Text from ATLAS */
		        HelpText_ResultStrAddr = atol(argv[6]);
				vmGetText(HelpText_ResultStrAddr, HelpText_ResultStr, SMALL_STRING);
				strcpy(sHelpText, HelpText_ResultStr);
 	            				
				/* Hoizontal Target Dimension from ATLAS */
		        TDimX_TopX_HFileAddr = atol(argv[7]);
				vmGetText(TDimX_TopX_HFileAddr, sTDimX_TopX_HFile, SMALL_STRING);
				strcpy(sTargDimX, sTDimX_TopX_HFile);

		/*--------- If a valid Help File is not passed, insert a Place Holder -----*/
				if (file_exists (sHelpFile) != 1)  {
					sprintf ( sHelpFile, "%s", NoHelpFile);
				}

 	            /* Vertical Target Dimension from ATLAS */
		        TDimY_TopY_HTextAddr = atol(argv[8]);
				vmGetText(TDimY_TopY_HTextAddr, sTDimY_TopY_HText, SMALL_STRING);
				strcpy(sTargDimY, sTDimY_TopY_HText);

			//<<<<<<<<<<<<<<<< Return Arguments <<<<<<<<<<<<<<<<<<<<<	
				
				/* Horizontal Field of View to pass back to ATLAS */
				HorzFoV_EndXAddr = atol(argv[9]);

				/* Vertical Field of View to pass back to ATLAS */
				VertFoV_EndYAddr = atol(argv[10]);
	
				/**** argv[11] is used to hold the Error Number ****/
				/* The Error Number to pass back to ATLAS */
				ResultAddr = atol(argv[11]);

				/**** argv[12] is used to hold the Error Description ****/
				/* The Error Description to pass back to ATLAS */
				ResultStrAddr = atol(argv[12]);

				// Pass sHelpText and sInstructions Strings to the Data File
				// These two arguments contain spaces, it will make it easier to retrieve
				nretv = WritePrivateProfileString(INSTRUCT, HELP_TEXT, sHelpText, DATFILE);
				if (nretv == 0) {
					printf ("ERROR writing to Data File: %s  \n",DATFILE);
					sprintf( sErrString, "%s %s", INVALID_DATFILE, DATFILE);
					CloseNAM( -104, sErrString );
				}
				
				nretv = WritePrivateProfileString(INSTRUCT, OP_INSTRUT, sInstructions, DATFILE);
				if (nretv == 0) {
					printf ("ERROR writing to Data File: %s  \n",DATFILE);
					sprintf( sErrString, "%s %s", INVALID_DATFILE, DATFILE);
					CloseNAM( -104, sErrString );
				}

				printf("Help File = %s\n", sHelpFile);
				/* Build Command Line Argument to pass to VidDisplay */
				nRet = sprintf( sCommLine, "VideoDisplay.exe %s \"%s\" \"%s\" %s %s", 
					            sMode, sType, sHelpFile, sTargDimX, sTargDimY );
				break;

			case TGTCOORD:
				//---------- Target Coordinate Mode ----------

   	            /* Help Graphic Name/Path from ATLAS */
		        HelpFile_ResultAddr = atol(argv[5]);
				vmGetText(HelpFile_ResultAddr, HelpFile_Result, SMALL_STRING);
				strcpy(sHelpFile, HelpFile_Result);

  	            /* Help Graphic Text from ATLAS */
		        HelpText_ResultStrAddr = atol(argv[6]);
				vmGetText(HelpText_ResultStrAddr, HelpText_ResultStr, SMALL_STRING); 
				strcpy(sHelpText, HelpText_ResultStr);


		/*--------- If a valid Help File is not passed, insert a Place Holder -----*/
				if (file_exists (sHelpFile) != 1)  {
					sprintf ( sHelpFile, "%s", NoHelpFile);
				}


			//<<<<<<<<<<<<<<<< Return Arguments <<<<<<<<<<<<<<<<<<<<<
 	            				
				/* Hoizontal Starting Point to pass back to ATLAS */
		        TDimX_TopX_HFileAddr = atol(argv[7]);

 	            /* Vertical Starting Point to pass back to ATLAS */
		        TDimY_TopY_HTextAddr = atol(argv[8]);				

				/* Hoizontal Ending Point to pass back to ATLAS */
				HorzFoV_EndXAddr = atol(argv[9]);

				/* Vertical Starting Point to pass back to ATLAS */
				VertFoV_EndYAddr = atol(argv[10]);
				
				/**** argv[11] is used to hold the Error Number ****/
				/* The Error Number to pass back to ATLAS */
				ResultAddr = atol(argv[11]);

				/**** argv[12] is used to hold the Error Description ****/
				/* The Error Description to pass back to ATLAS */
				ResultStrAddr = atol(argv[12]);

				// Pass sHelpText and sInstructions Strings to the Data File
				// These two arguments contain spaces, it will make it easier to retrieve
				nretv = WritePrivateProfileString( INSTRUCT, HELP_TEXT, sHelpText, DATFILE);
				if (nretv == 0) {
					printf ("ERROR writing to Data File: %s  \n",DATFILE);
					sprintf( sErrString, "%s %s", INVALID_DATFILE, DATFILE);
					CloseNAM( -104, sErrString );
				}
				
				nretv = WritePrivateProfileString( INSTRUCT, OP_INSTRUT, sInstructions, DATFILE);
				if (nretv == 0) {
					printf ("ERROR writing to Data File: %s  \n",DATFILE);
					sprintf( sErrString, "%s %s", INVALID_DATFILE, DATFILE);
					CloseNAM( -104, sErrString );
				}

				/* Build Command Line Argument to pass to VidDisplay */
				nRet = sprintf( sCommLine, "VideoDisplay.exe %s \"%s\" %s", 
					            sMode, sType, sHelpFile );
				break;

			case ALIGN:
				//---------- Target Alignment Mode ----------

   	            /* Help Graphic Name/Path from ATLAS */
		        TDimX_TopX_HFileAddr = atol(argv[5]);
				vmGetText(TDimX_TopX_HFileAddr, sTDimX_TopX_HFile, SMALL_STRING);
				strcpy(sHelpFile, sTDimX_TopX_HFile);

  	            /* Help Graphic Text from ATLAS */
		        TDimY_TopY_HTextAddr = atol(argv[6]);
				vmGetText(TDimY_TopY_HTextAddr, sTDimY_TopY_HText, SMALL_STRING);
				strcpy(sHelpText, sTDimY_TopY_HText);

		    /*--------- If a valid Help File is not passed, insert a Place Holder -----*/
				if (file_exists (sHelpFile) != 1)  {
					sprintf ( sHelpFile, "%s", NoHelpFile);
				}

			//<<<<<<<<<<<<<<<< Return Arguments <<<<<<<<<<<<<<<<<<<<<
				/**** argv[5] is used to hold the Error Number ****/
				/* The Error Number to pass back to ATLAS */
				ResultAddr = atol(argv[7]);

				/**** argv[6] is used to hold the Error Description ****/
				/* The Error Description to pass back to ATLAS */
				ResultStrAddr = atol(argv[8]);

				// Pass the Operator Instructions string to the Data File
				nretv = WritePrivateProfileString( INSTRUCT, OP_INSTRUT, sInstructions, DATFILE);
				if (nretv == 0) {
					printf ("ERROR writing to Data File: %s  \n",DATFILE);
					sprintf( sErrString, "%s %s", INVALID_DATFILE, DATFILE);
					CloseNAM( -104, sErrString );
				}

				/* Build Command Line Argument to pass to VidDisplay */
				nRet = sprintf( sCommLine, "VideoDisplay.exe %s \"%s\" \"%s\"", sMode, sType, sHelpFile);
				break;

			case DISPLAY_ONLY:
				//---------- Video Capture Mode ----------

			//<<<<<<<<<<<<<<<< Return Arguments <<<<<<<<<<<<<<<<<<<<<
				/**** argv[5] is used to hold the Error Number ****/
				/* The Error Number to pass back to ATLAS */
				ResultAddr = atol(argv[5]);

				/**** argv[6] is used to hold the Error Description ****/
				/* The Error Description to pass back to ATLAS */
				ResultStrAddr = atol(argv[6]);

				// Pass the Operator Instructions string to the Data File
				nretv = WritePrivateProfileString( INSTRUCT, OP_INSTRUT, sInstructions, DATFILE);
				if (nretv == 0) {
					printf ("ERROR writing to Data File: %s  \n",DATFILE);
					sprintf( sErrString, "%s %s", INVALID_DATFILE, DATFILE);
					CloseNAM( -104, sErrString );
				}

				/* Build Command Line Argument to pass to VidDisplay */
				nRet = sprintf( sCommLine, "VideoDisplay.exe %s \"%s\"", sMode, sType );
				break;
		
			default:
				//---------- Unrecognized mode ----------
				printf ("ERROR: %s Mode is not valid. \n",sMode);
				sprintf( sErrString, "%s %s mode passed.", INVALID_MODE, sMode);
				CloseNAM( -101, sErrString );
				break;

		}

	// Launch program Video Display Program	
	StartDisplay(sCommLine);	

/*******************************************************************
***             Read Data from the Video.dat File                ***
*********************************************************************/

	
		switch(iMode)   {


			case FOV:
				//---------- Field of Veiw Mode ----------	
	
				nretv = GetPrivateProfileString ( RETDATA, HORZFOV, DEFAULT, pBuffer, 257, DATFILE );
				sprintf (sCodeout, (char*)pBuffer);
				
				if (nretv == 0) {
					printf ("ERROR -103: %s data not retreived from the data file. \n",HORZFOV);
					sprintf( sErrString, "%s %s", INVALID_KEY, HORZFOV);
					CloseNAM( -103, sErrString );

				}
				//Make sure Data was retrieved before converting
				dHorzFoV = atof(sCodeout);	/* Convert to data type float */


				nretv = GetPrivateProfileString ( RETDATA, VERTFOV, DEFAULT, pBuffer, 257, DATFILE );
				sprintf (sCodeout, (char*)pBuffer);

				if (nretv == 0) {
					printf ("ERROR -103: %s data not retreived from the data file. \n",VERTFOV);
					sprintf( sErrString, "%s %s", INVALID_KEY, VERTFOV);
					CloseNAM( -103, sErrString );
				}
				//Make sure Data was retrieved before converting
				dVertFoV = atof(sCodeout);	/* Convert to data type float */

				
				/*** Pass Horizortal and Vertical Field-of-View back to ATLAS ***/
				fHorzFoV_EndX = dHorzFoV;
				vmSetDecimal(HorzFoV_EndXAddr, fHorzFoV_EndX);

				fVertFoV_EndY = dVertFoV;
				vmSetDecimal(VertFoV_EndYAddr, fVertFoV_EndY);			

				break;

			
			case TGTCOORD:
				//---------- Target Coordinate Mode ----------
				
				nretv = GetPrivateProfileString ( RETDATA, STARTX, DEFAULT, pBuffer, 257, DATFILE );
				sprintf (sCodeout, (char*)pBuffer);
								
				if (nretv == 0) {
					printf ("ERROR 103: %s data not retreived from the data file. \n",STARTX);
					sprintf( sErrString, "%s %s", INVALID_KEY, STARTX);
					CloseNAM( -103, sErrString );
				}
				//Make sure Data was retrieved before converting
				nStartX = atol(sCodeout);	/* Convert to data type long */	
				printf("Start X: %d\n", nStartX); 	

				
				nretv = GetPrivateProfileString ( RETDATA, STARTY, DEFAULT, pBuffer, 257, DATFILE );
				sprintf (sCodeout, (char*)pBuffer);
				
				if (nretv == 0) {
					printf ("ERROR: %s data not retreived from the data file. \n",STARTY);
					sprintf( sErrString, "%s %s", INVALID_KEY, STARTY);
					CloseNAM( -103, sErrString );
				}

				//Make sure Data was retrieved before converting
				nStartY = atol(sCodeout);	/* Convert to data type long */
				printf("Start Y: %d\n", nStartY); 

				nretv = GetPrivateProfileString ( RETDATA, ENDX, DEFAULT, pBuffer, 257, DATFILE );
				sprintf (sCodeout, (char*)pBuffer);
				
				if (nretv == 0) {
					printf ("ERROR: %s data not retreived from the data file. \n",ENDX);
					sprintf( sErrString, "%s %s", INVALID_KEY, ENDX);
					CloseNAM( -103, sErrString );
				}

				//Make sure Data was retrieved before converting
				nEndX = atol(sCodeout);	/* Convert to data type long */		

				
				nretv = GetPrivateProfileString ( RETDATA, ENDY, DEFAULT, pBuffer, 257, DATFILE );
				sprintf (sCodeout, (char*)pBuffer);
				
				if (nretv == 0) {
					printf ("ERROR: %s data not retreived from the data file. \n",ENDY);
					sprintf( sErrString, "%s %s", INVALID_KEY, ENDY);
					CloseNAM( -103, sErrString );
				}
				//Make sure Data was retrieved before converting
				nEndY = atol(sCodeout);	/* Convert to data type long */

				/***** Pass Coorindinates back to ATLAS *****/
				iStartX = nStartX;
				vmSetInteger(TDimX_TopX_HFileAddr, iStartX);

				iStartY = nStartY;
				vmSetInteger(TDimY_TopY_HTextAddr, iStartY);

				iStopX = nEndX;
				vmSetInteger(HorzFoV_EndXAddr, iStopX);

				iStopY = nEndY;
				vmSetInteger(VertFoV_EndYAddr, iStopY);
		
				break;
		}
	
		//**********  Get Error Code and Description  **********
		nretv = GetPrivateProfileString ( NAMERROR, ERRCODE, DEFAULT, pBuffer, 257, DATFILE );
		sprintf (sCodeout, (char*)pBuffer);

		if (nretv == 0) {
			printf ("ERROR: %s data not retreived from the data file. \n",ERRCODE);
			sprintf( sErrString, "%s %s", INVALID_KEY, STARTY);
			CloseNAM( -103, sErrString );
		}
		//Make sure Data was retrieved before converting
	    nErrCode = atol(sCodeout);	/* Convert to data type long */

		/**** If an Error String is passed back, append it any existing ****/
		//If Error Code is either -106 or -107, Expect an Error String
		if (nErrCode < -105) {
		nretv = GetPrivateProfileString ( NAMERROR, ERRSTR, DEFAULT, pBuffer, 257, DATFILE );	
		sprintf (sErrString, (char*)pBuffer);

			if (nErrCode == -106)  
				sprintf(sErrReturn, "%s   %s", DISPLAY_ERROR, sErrString);
			else if (nErrCode == -107)  
				sprintf(sErrReturn, "%s   %s", EPIX_ERROR, sErrString);
		}
		else 
			sprintf(sErrReturn, "%s", sErrString);
				
		// If Successful, Assign String
		if (nErrCode = 0) {
			sprintf(sErrReturn, "Successful");
		}

		/* The Error Status to pass back to ATLAS */
		CloseNAM( nErrCode, sErrReturn);

}


