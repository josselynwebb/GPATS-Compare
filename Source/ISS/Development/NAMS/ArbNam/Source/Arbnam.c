/*	File: Arbnam.c
 *	
 *  This program is used as a NAM call from an ATLAS program.  The ATLAS program
 *	passes an argument which is the name of an Arb configuration File.  The NAM program
 *	then examines the ATLAS program name.TMP file and extracts the argument.
 *	The NAM opens the configuration File and programs the Arb to produce the signal.
 *
 *	File Creation Date: 1/12/99
 *  Version: 1.6
 *  Created By: Grady Johnson
 *  Last Revision by: Jeff Hill
 *
 *	Revision Log:
 *	
 *	V1.1 4/13/99 GJohnson
 *		Update Arbname.exe to correct problems identified during FAT testing related to the
 *		Error Quarry function.  Added Delay Function.  Implemented per ECO-3047-224
 *	V1.2 6/30/00 GJohnson
 *		Modified the attributes used to open the *.seg file from "r+b" to "rb".  The
 *		"r+b" indicates read and write capability with binary access to the file.  The
 *		"rb" indicates only a read capability with binary access to the file.  The 
 *		read/write attribute did not allow the Arbnam to execute when reading a *.seg
 *		file from a CDROM.  This corrections resulted from Government STR Ticket 
 *		number 242, ManTech ID # 431 and was implemented per ECO-3047-454.
 *	V1.3 11/24/00 JHill, ManTech DR #97, ECO-3047-502.
 *		Problem: The ARB generates a 'data out of range' error when:
 *			1. a sequence of segment files is included in the ARB configuration,
 *			2. and a non-zero :VOLT:OFFS setting is used,
 *			3. and any waveform point exceeds the :VOLT setting + :VOLT:OFFS setting.
 *		Solution: Modify the code to:
 *			1. Add global variable 'Offset'.
 *			2. Detect the :VOLT:OFFS command in the PRE_SEGMENT_LOAD_COMMANDS section
 *				and extract the offset value into the new variable.
 *			3. Subtract Offset from each voltage point before sending it to the ARB. 
 *	V1.4 6/8/01 JHill, ManTech DR #171, ECO-3047-531.
 *		Problems:
 *			1. Generates NAM error -108, usually with ARB error "Query Interrupted" on
 *				a 770Z controller with some test programs. OK on 770E.
 *			2. The next call to ARBNAM would cause Dr. Watson for ARBNAM.EXE and/or cause
 *				a subsequent DTS STIMULATE or SENSE statement to hang up.
 *		Solutions:
 *			1. The Delay function called by the WriteCommandToArb function was not actually
 *				working because it was being passed a literal integer (2) while it was 
 *				expecting a float. After sending the proper format, other Dr. Watson errors 
 *				occurred within it. Deleted the delay(2) function and use Sleep(100) 
 *				instead. That's 100 mSec. Added #include <windows.h> to support it.
 *			2. Changed all calls to CloseNAM so that the 2nd argument passed is never a
 *				literal string. Now, the desired string is copied into a string variable
 *				and the variable is passed. Don't know why it works with a 770E but causes
 *				problems with the 770Z.
 *	V1.5 9/20/01 JHill, ManTech DR #230 (764), ECO-3047-559.
 *		Problems:
 *			1. The VISA timeout of 10 seconds is not long enough for large waveforms.
 *			2. Does not display VISA errors. The 'if' statement in VisaErrorMessage has
 *        reversed logic.
 *		Solutions:
 *      1. Investigation has shown that the transfer rate is about 450 waveform points
 *        per second. Seems to be based on MXI-2. No difference between 770E vs. 770Z
 *        controller. Modified DownLoadSegment() to set timeout based on download rate of
 *        400 points per second. Then set it back to 10 sec when done with that waveform.
 *      2. Removed it because VisaErrorMessage is only called when there is an error
 *        anyway. Also removed local ReadBuffer declaration. Now uses the global one.
 *        Changed three sections that display VISA errors to also display the VISA 
 *        error code in hexadecimal format.
 *	V1.6 3/16/05 JHill, ManTech DR #324, ECO-3047-680.
 *		Problem:
 *			Need to speed up waveform download time to facilitate using the ARB as a 
 *			video generator for EO tests.
 *		Solution:
 *			Changed the method of downloading a waveform to the instrument from sending 
 *			a comma-delimited list of voltage points in text format to sending numeric 
 *			values representing ARB DAC codes via shared VXI memory.
 *			Performed these modifications in functions DownLoadSegment() and main().
 *
 * V1.7  4/8/06 EADS NA Updated to Opwerate thru Common Interface Contril Layer
 */

#include <sys/types.h>
#include <sys/stat.h>
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)

#include <stdlib.h>
#include <ctype.h>
#include <string.h>
#include <stdio.h>
#include "visa.h"
#include "hpe1445a.h"
#include <process.h>
#include "nam.h"

#define ARB_NAM_GUID "{88CE4B4A-6155-4ba5-A659-5331EB2D4C52}"
//#define ATLAS 0 /* 0 indicates NOT using atlas, 1 indicates using atlas */
#define HEADER_ID 0xABCD4567ul	//Header id for Segment files
#define RESOURCE_ADDR "VXI::48"
#define SUFFIX ".TMP"
#define SEG_SUFFIX ".SEG"
#define ARB_SUFFIX ".arb"
#define PRE_SEGMENT "[PRE_SEGMENT_LOAD_COMMANDS]"
#define SEGMENT_LIST "[SEGMENT_LIST]\n"
#define POST_SEGMENT "[POST_SEGMENT_LOAD_COMMANDS]\n"
#define REMOVE "REMOVE"

#define STRING_SIZE 2048

//#define IFLOGOK(x)	if (logfile != NULL) {x;}


/* Define Visa Variables */
ViSession	defaultRM;
ViSession	InstrHndl;
ViStatus	status;
ViUInt32	retCnt;

/* Define Global Variables */

/* TYX data structure - ref. TYX PAWS User Guide Vol 1 Sec 5 */
char ARBFileName[STRING_SIZE];
int Results = 0;
char ResultStr[STRING_SIZE];
long ARBFileNameAddr, ResultsAddr, ResultStrAddr;
char TempFileName[40];
int count;
char line[10];
FILE *filepointer = NULL;		// Pointer to Sequence input file 
char ErrorStr[499];		// Used to store an error string 
char *char_line;	 	// Used to store a line of data out of data file 
ViChar	ReadBuffer[256];	// Used to store Visa Error string
int ATLAS;					/* 0 indicates NOT using atlas, 1 indicates using atlas */	
double Offset;			// Holds offset voltage to apply to voltage points. V1.2 JHill
double dScale;			// Verticle scale factor. V1.6 JHill

/************************************************************/
/* FUNCTIONS                                                */
/************************************************************/

/* strtoupper - Converts a char string to upper case */
char *strtoupper(char *str)
{
	int i;
	for (i=0; str[i] != '\0'; i++)
	{
		str[i] = toupper(str[i]);
	}
	return(str);
}

/* rmcr - removes a cariage return off of a string */
char *rmcr(char *str)
{
	int i;
	for (i=0; str[i] != '\n'; i++)
	{
		str[i] = str[i];
	}

	str[i] = '\0';
	return(str);
}

/* shrtcmd - short down command to only 400 char for print statements */
char *shrtcmd(char *str)
{
	int i;
	for (i=0; ((str[i] != '\0') && (i <= 400)); i++)
		str[i] = str[i];
	str[i] = '\0';
	return(str);
}

/* CloseNAM - De-allocates TMP file and closes program */
void CloseNAM(long CloseStatus, char ErrorStr[499])
{
	/* Return Results and Result String to ATLAS */
	/* ref TYX PAWS User Guide Vol 1 Sec 5 */
	if (filepointer != NULL)
	{
		fclose(filepointer);
		filepointer = NULL;
	}

	if (ATLAS)
	{
		Results = CloseStatus;
		strcpy(ResultStr, shrtcmd(ErrorStr));
		vmSetInteger(ResultsAddr, Results);
		vmSetText(ResultStrAddr, ResultStr);

		/* Close virtual memory file */
		hpe1445a_close (InstrHndl);
		vmClose();
	}
	else
	{
		printf ("%s", ErrorStr);
		hpe1445a_close (InstrHndl);
	}

	exit(0);
}


/* VisaErrorMessage - Collects Visa error message */
char *VisaErrorMessage(ViStatus ErrorStatus)
{
	sprintf(ReadBuffer,"Instrument Error Code %08x",ErrorStatus); //FIX later
	return(ReadBuffer);
}

/* WriteCommandToArb - Collects Visa error message */
/* User global variables char_line, status, retCnt, ReadBuffer */
void WriteCommandToArb(char *command)
{
	ViInt32	ErrorCode = 0;			// Used to store Error code
	ViUInt32 retCnt = 0;

	if (!ATLAS) 
	{
		printf ("Instrument command: %s\n",command);
		printf ("Num chars: %d\n",strlen(command));
	}
	// Send commands to instument
	status = viWrite(InstrHndl, (ViBuf)command, (ViUInt32)strlen(command), &retCnt);
	if (!ATLAS)
		printf ("Status: %X\n", status);
	if (status)
	{
		sprintf(ErrorStr, "ERROR - VISA error.\n%X: ", status); //V1.5 JHill
		strcat(ErrorStr, VisaErrorMessage(status));
		CloseNAM(-107, ErrorStr);
	}
	else
	{
		if (strchr(command, '?') == NULL)
		{
			do
			{
				status = hpe1445a_errorQuery(InstrHndl, &ErrorCode, ReadBuffer);
				ErrorCode = 0; //FIX later
				if (ErrorCode)
				{
					sprintf(ErrorStr, "ERROR - Arbitrary Waveform Generator Error Message.\n Instument Command: %s caused -\n", shrtcmd(command));
					strcat(ErrorStr, ReadBuffer);
					CloseNAM(-108, ErrorStr);
				}
				Sleep(100);	//100 mSec
			} while (ErrorCode);
		}
	}
}

/* DownLoadSegment - Downloads segment file to ARB */
int DownLoadSegment(char *Segmentfilename, char *str)
{
	FILE			*SegFP;
	unsigned long	FileHeader;
	long			SegmentDataBytes;
	long			nSegmentPoints;  //V1.5 JHill
	double			value;
	long			n;
	ViUInt16		iDacCode;
	char			lpSegName[13];		// Segment Name (12 char max IAW ARB manual)

	/* Send Wavefile name to the ARB */
	sprintf(str, "LIST:SEL %s", Segmentfilename);
	WriteCommandToArb(str);
			
	//Copy Segment Name before adding extension
	strncpy(lpSegName, Segmentfilename, 13);

	/* Add File Extension */ 
	if (strchr(Segmentfilename, '.') == NULL)
		strcat(Segmentfilename, SEG_SUFFIX);
	if (!ATLAS)
		printf ("Segment File: %s\n", Segmentfilename);

	/* Open Sequence File for Input */
	SegFP = fopen(Segmentfilename, "rb");
	if (SegFP == (FILE *) NULL)
	{
		sprintf(ErrorStr,"ERROR - Unable to open %s segment data file.\n", Segmentfilename); 
		CloseNAM(-109, ErrorStr);
	}
	
	/* Compare header */
	rewind(SegFP);
	fread( &FileHeader, sizeof( unsigned long ), 1, SegFP );
	if (!ATLAS)
		printf ("File Header: %u\n", FileHeader);
	if (FileHeader != HEADER_ID) 
	{
		sprintf(ErrorStr,"ERROR - Incorrect header in %s segment data file.\n", Segmentfilename); 
		if (SegFP)
		{
			fclose( SegFP );
			SegFP = NULL;
		}
		CloseNAM(-110, ErrorStr);
	}
	/* Get file size */
	fread( &SegmentDataBytes, sizeof( unsigned long ), 1, SegFP );
	if (!ATLAS)
		printf ("Segment Data Bytes: %u\n", SegmentDataBytes);
	if ((SegmentDataBytes % 8) != 0)
	{
		sprintf(ErrorStr,"ERROR - Incorrect Segment Data Bytes Number in header in %s segment data file.\n", Segmentfilename); 
		if (SegFP)
		{
			fclose( SegFP );
			SegFP = NULL;
		}
		CloseNAM(-111, ErrorStr);
	}
	/* Allocate variable for number of Datapoints, 
	   Send LIST:DEF command to ARB */
	nSegmentPoints = SegmentDataBytes / 8;  //V1.5 JHill
	sprintf(str, "LIST:DEF %i", nSegmentPoints);
	WriteCommandToArb(str);
	
	sprintf(str, ":ARB:DOWN VXI,%s,%d", lpSegName, nSegmentPoints);
	WriteCommandToArb(str);

	WriteCommandToArb("*OPC?");

	status = viRead(InstrHndl, (ViPBuf)str, (ViInt32) sizeof(str), &retCnt);
	if (!ATLAS)
		printf ("Status: %X, *OPC? Return Val %s\n", status,str);
	if (status)
	{
		sprintf(ErrorStr, "ERROR - VISA error.\n%X: ", status);
		strcat(ErrorStr, VisaErrorMessage(status));
		if (SegFP)
		{
			fclose( SegFP );
			SegFP = NULL;
		}
		CloseNAM(-107, ErrorStr);
	}

 	/* Reposition File pointer to byte 257 */
 	if (fseek(SegFP, 256L, SEEK_SET) != 0) 
	{
	 	sprintf(ErrorStr,"ERROR - Unable to set pointer location in %s segment data file.\n",
			Segmentfilename); 
		if (SegFP)
		{
			fclose( SegFP );
			SegFP = NULL;
		}
 		CloseNAM(-112, ErrorStr);
 	}

	//Go up to the third to last element as fast as possible
	for (n = 1; n <= nSegmentPoints - 4; n++) 
	{
		fread(&value, 8, 1, SegFP );					//Read voltage point value
		iDacCode = ((ViUInt16) ((value - Offset) / dScale)) * 8;	//Calculate DAC code, then shift left 3 bits
		if (!ATLAS)
			printf ("File Voltage Point: %g, Calculated DAC Code %d\n", value, iDacCode);

		//Write the DAC code list to register 38 (decimal) in A24 address space
		status = viOut16(InstrHndl, VI_A24_SPACE, 38, iDacCode);

		if (status) 
		{
			sprintf(ErrorStr, "ERROR - VISA error.\n%X: ", status);
			strcat(ErrorStr, VisaErrorMessage(status));
			if (SegFP)
			{
				fclose( SegFP );
				SegFP = NULL;
			}
			CloseNAM(-107, ErrorStr);
		}
	}

	//Now do last 3
	for(n = n; n <= nSegmentPoints; n++)
	{
		//Set "last point" bit at NumSamps& - 3
		fread(&value, 8, 1, SegFP );					//Read voltage point value
		iDacCode = ((ViUInt16) ((value - Offset) / dScale)) * 8;	//Calculate DAC code, then shift left 3 bits
		
		//Set bit in 3rd to last only
	    if(n == nSegmentPoints - 3)
			iDacCode = (ViUInt16) iDacCode + 1;

		if (!ATLAS)	printf ("File Voltage Point: %g, Calculated DAC Code %d\n", value, iDacCode);

		//Write the DAC code list to register 38 (decimal) in A24 address space
		status = viOut16(InstrHndl, VI_A24_SPACE, 38, iDacCode);
		if (status) 
		{
			sprintf(ErrorStr, "ERROR - VISA error.\n%X: ", status);
			strcat(ErrorStr, VisaErrorMessage(status));
			if (SegFP)
			{
				fclose( SegFP );
				SegFP = NULL;
			}
			CloseNAM(-107, ErrorStr);
		}
	}

	WriteCommandToArb(":ARB:DOWN:COMP");
	if (SegFP)
	{
		fclose( SegFP );
		SegFP = NULL;
	}

	return(0);

}



/************************************************************/
/* MAIN FUNCTION                                            */
/************************************************************/
int main (int argc, char *argv[])
{

	/* Define Local Variables */
	char			ARB_File[80] = "";		// Sequence input file 
	char			Seg_File[80] = "";		// Segment input file
	char			*char_line;	 		// Used to store a line of data out of data file 
	int				i = 0;					// counter
	struct			_stat buf;			// Structure to hold file info
	char			*s;
	//MessageBox(NULL, "Arb Nam Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	// getchar();
	/* Figure out if Dos Command Line or ATLAS program called */
	/* Copy passed arguments */
	if (argc < 5)
		ATLAS = 0;
	else
		ATLAS = 1;

	// initialize file pointer to null
	filepointer = (FILE*)NULL;

	if (ATLAS) /* use ATLAS */
	{
		/* Use Nam.h and Nam.lib to copy arguments passed arguments
		   from ATLAS*/

		/* Define ATLAS temp file name and open it */
		strcpy(TempFileName, argv[1]);
		strcat(TempFileName, SUFFIX);
		vmOpen(TempFileName);

		/* Copy passed arguments */
		ARBFileNameAddr = atol(argv[2]);
		vmGetText(ARBFileNameAddr, ARBFileName, STRING_SIZE);
		strtoupper(strcpy(ARB_File, ARBFileName));
		ResultsAddr = atol(argv[3]);
		Results = vmGetInteger(ResultsAddr);
		ResultStrAddr = atol(argv[4]);
		vmGetText(ResultStrAddr, ResultStr, STRING_SIZE);
	}
	else /* ATLAS == 0 Use from Command line */
	{
		/* Copy command line argument and convert to upper case */
		if (argc == 2) strcpy(ARB_File, strtoupper(argv[1]));
		else 
		{
			if (argc < 2) 
			{
				sprintf(ErrorStr,"ERROR - No COMMAND LINE arguments specified.\n"); 
				CloseNAM(-100, ErrorStr);
			}
			if (argc > 2)
			{
				sprintf(ErrorStr,"ERROR - Invalid number of COMMAND LINE arguments.\n");
				CloseNAM(-101, ErrorStr);
			}
		}
	}


	/* Get Instrument Handle */
	status = hpe1445a_init (RESOURCE_ADDR, VI_TRUE, VI_TRUE, &InstrHndl);
	
	/* Set Timeout value */
	status = viSetAttribute(InstrHndl, VI_ATTR_TMO_VALUE, 10000); //10 sec.
	if (status != VI_SUCCESS)
	{
    //V1.5 JHill
		sprintf(ErrorStr, "ERROR - Unable to set VISA ATTRIBUTE.\n%X: ", status);
		strcat(ErrorStr, VisaErrorMessage(status));
		CloseNAM(-103, ErrorStr);
	} 
	else if (!ATLAS )
		printf("Timeout value: 10 sec\n");
		
	/* Check to see if REMOVE was sent to shut down ARB */
	if (strcmp(strtoupper(ARB_File), REMOVE) == 0)
	{
		WriteCommandToArb("*RST;*CLS\n");
		strcpy(ErrorStr, "ARBNAM - Successful, Signal Removed.\n");
		CloseNAM(0, ErrorStr);
	}

	/* Add File Prefix */ 
	if (strchr(ARB_File, '.') == NULL)
		strcat(ARB_File, ARB_SUFFIX);
	if (!ATLAS)
		printf ("Sequence file: %s\n",ARB_File);
	
	/* Open Sequence File for Input */
	filepointer = fopen(ARB_File, "r");
	if (filepointer == (FILE *) NULL) 
	{
		sprintf(ErrorStr,"ERROR - Unable to open %s configuration data file.\n", ARB_File); 
		CloseNAM(-104, ErrorStr);
	}
	else // Process Sequence File
	{
		Offset = 0.0;					//Default setting if "VOLT:OFFS" not in file
		dScale = 0.161869088 / 4095.0;	//Default setting if "VOLT " not in file

		/* Get data associated with Sequence file */
		_stat(ARB_File, &buf);
		
		/* Allocate Memory for char_line variable */
		if ((char_line = (char *) malloc(buf.st_size)) == NULL){
			strcpy(ErrorStr, "ERROR - Unable to allocate memory.\n");
			CloseNAM(-105, ErrorStr);
		}

		/* Read first line of Sequence File */
		fgets (char_line, buf.st_size - 1, filepointer);
		strcpy(char_line,rmcr(char_line));
		if (strcmp(char_line, PRE_SEGMENT) != 0) {
			sprintf(ErrorStr,"ERROR - %s Arbitrary configuration file corrupt.\n", ARB_File); 
			CloseNAM(-106, ErrorStr);
		}

		/* Read in PRE_SEGMENT_LOAD_COMMANDS */
		while ((!feof(filepointer)) &&
			(fgets(char_line, buf.st_size - 1, filepointer) != NULL) &&
			(strcmp(char_line, SEGMENT_LIST) != 0)) 
		{
			strcpy(char_line,rmcr(char_line));

			//Find Offset setting, if exists
			s = strstr(char_line, "VOLT:OFFS");	//V1.2 JHill
			if (s != NULL)						//V1.2 JHill
			{
				Offset = atof(s + 10);	//Point to the Offset voltage argument
				if (!ATLAS ) printf("Voltage Offset: %f\n", Offset);
			}

			//Find Volt setting to calculate scale, if exists
			s = strstr(char_line, "VOLT ");
			if (s != NULL)
			{
				dScale = atof(s + 5);	//Point to the VOLT argument
				if (!ATLAS ) printf("VOLT: %g\n", dScale);
				dScale = dScale / 4095.0;
				if (!ATLAS ) printf("VOLT Scale Factor: %g\n", dScale);
			}

			WriteCommandToArb(char_line);
		} // End While PRE_SEGMENT_LOAD_COMMANDS 

		/* Read in SEGMENT FILE NAMES */
		i = 0;
		while ((!feof(filepointer)) &&
			(fgets(char_line, buf.st_size - 1, filepointer) != NULL) &&
			(strcmp(char_line, POST_SEGMENT) != 0)) {
			i++;
			strcpy(Seg_File,rmcr(char_line));
			DownLoadSegment(Seg_File, char_line);
		} // End While SEGMENT File Names 

		/* Read in POST_SEGMENT_LOAD_COMMANDS */
		while ((!feof(filepointer)) &&
			(fgets(char_line, buf.st_size - 1, filepointer) != NULL)) {
			strcpy(char_line,rmcr(char_line));
			WriteCommandToArb(char_line);
		} // End While POST_SEGMENT_LOAD_COMMANDS 
	} // End If Statement of opening and processing Sequence File
	
	WriteCommandToArb(":INIT");
	
	strcpy(ErrorStr, "ARBNAM - Successful\n");

	CloseNAM(0, ErrorStr);
} /* End main */


