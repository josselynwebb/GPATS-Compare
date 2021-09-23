/*	File: SensorNAM.c
 *	
 *  This program is used as a NAM call from an ATLAS program.  The ATLAS program
 *	passes an argument which is the sensor operation.  The NAM program
 *	then examines the ATLAS program name.TMP file and extracts the argument.
·*	The NAM opens the configuration File and programs the Arb to produce the signal.
 *
 *	File Creation Date: 3/7/01
 *  Version: 1.0
 *  Created By: Jeff Hill
 *
 *	Revision Log:
 *	
 *	V1.1 x/x/xx Manny Tech
 *		xxx.
 */

#include <time.h>
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include <stdio.h>

#include "visa.h"
#include "hpe1416a.h"
#include "NAM.H"

#define RESOURCE_ADDR "VXI::28"
#define SUFFIX ".TMP"
#define DOR_MASK  0x2000
#define MSG_RESP_REG 10
#define MEDIUM_STRING 1024

/* Define Global Variables */

/* Visa Variables */
ViSession	defaultRM;
ViSession	InstrHndl;
ViStatus	TETSstatus;
ViUInt32	retCnt;

/* TYX data structure - ref. TYX PAWS User Guide Vol 1 Sec 5 */
int TETSResults;
char TETSOperation[MEDIUM_STRING];
char TETSResultStr[MEDIUM_STRING];

/* Program Globals */
long			TETSnOperation, TETSResultsAddr, TETSResultStrAddr;
char			TETSErrorStr[499];
ViStatus		TETSlSystErr;
int				TETSiGetSnFlag;

/************************************************************/
/* FUNCTIONS                                                */
/************************************************************/

/* strtoupper - Converts a char string to upper case */
char *strtoupper(char *str)
{
	int i;
	for (i=0; str[i] != '\0'; i++)
		str[i] = (char)toupper(str[i]);
	return(str);
}

/* CloseNAM - De-allocates TMP file and closes program */
void CloseNAM(long CloseStatus, char ErrorStr[499])
{
	/* Return Results and Result String to ATLAS */
	/* ref TYX PAWS User Guide Vol 1 Sec 5 */
	TETSResults = CloseStatus;
	strcpy(TETSResultStr, ErrorStr);
	vmSetInteger(TETSResultsAddr, TETSResults);
	vmSetText(TETSResultStrAddr, TETSResultStr);

	if (!TETSiGetSnFlag) hpe1416a_close (InstrHndl);

	/* Close virtual memory file */
	vmClose();
	exit(0);
}


/* VisaErrorMessage - Collects Visa error message */
char *VisaErrorMessage(ViStatus ErrorStatus)
{
	static ViChar ReadBuffer[256];
	static long err;

	if (!ErrorStatus) err = hpe1416a_errorMessage(InstrHndl, ErrorStatus, ReadBuffer);
	return(ReadBuffer);
}

/* WriteCommand - Sends command and checks for Visa error message */
char *WriteCommand(char *command)
{

	static char	ReadBuffer[256];
	ViInt32		ErrorCode;			// Used to store Error code
	
	// Send commands to instument
	TETSstatus = viWrite (InstrHndl, (unsigned char *) command, (ViUInt32) strlen(command), &retCnt);
	if (TETSstatus) 
	{
		strcpy(TETSErrorStr, "ERROR - VISA error. \n");
		strcat(TETSErrorStr, VisaErrorMessage(TETSstatus));
		CloseNAM(-103, TETSErrorStr);
	}

	else
	{
		if (strchr(command, '?') == NULL) 
		{
			TETSstatus = hpe1416a_errorQuery(InstrHndl, &ErrorCode, ReadBuffer);
			Sleep(100);
			if (ErrorCode) 
			{
				sprintf(TETSErrorStr, "ERROR - Power Meter Error Message.\n Instument Command: %s caused -\n", command);
				strcat(TETSErrorStr, ReadBuffer);
				CloseNAM(-104, TETSErrorStr);
			}
		}
	}

	return(ReadBuffer);
}


/* WaitForResponse - Waits for a message to be placed in the instruments output buffer */

void WaitForResponse(int iSeconds)
{
	unsigned short	MsgRespReg;
	char			*ReadBuffer;
	time_t StartTime;
	time_t Current;
	
	time( &StartTime );
	time( &Current );

	while((StartTime + (time_t) iSeconds) > Current)
	{
		time(&Current);
		TETSlSystErr = viIn16(InstrHndl, VI_A16_SPACE, MSG_RESP_REG, &MsgRespReg);
		if (MsgRespReg & DOR_MASK)
			return;
		Sleep(1000);	// delay 1 sec
	}
	ReadBuffer = WriteCommand("*RST; *CLS");
	sprintf(TETSErrorStr, "ERROR - Operation Incomplete due to Timeout.\n");
	strcat(TETSErrorStr, ReadBuffer);
	CloseNAM(-201, TETSErrorStr);
}


/*****************************************************************************
 *  This function opens tets.ini file in the windows directory and exstracts *
 * 	nessary data about which data table should be used						 *
 *****************************************************************************/
void get_datatable(char powerhead[10], char *addtopowerhead)
{
	char 		lpBuffer[80], 
				PathTetsIni[80];				// specifies path of the tets.ini file
	char 		SerialNumberofPowerHead[100];	// specifies SN of the power head

	int 		i, 
				checkforx;						// checks if tets.ini contins section name, and key name

	// the GetWindowsDirectory function retrives the path of the Windows directory.
	//GetWindowsDirectory(lpBuffer, 80);
	//sprintf(PathTetsIni,"%s%s",lpBuffer,"\\Tets.ini");
    sprintf(PathTetsIni, "%s", "C:\\Users\\Public\\Documents\\ATS\\ATS.ini");

	// the GetPrivateProfileString function retrives a string from the specified section in an 
	// initialization file.
	GetPrivateProfileString("Serial Number", powerhead, "x", lpBuffer, 80, PathTetsIni);
	sprintf(SerialNumberofPowerHead,"%s",lpBuffer);

	// checks if the return string is x which means error
	checkforx = strcmp(SerialNumberofPowerHead, "x");

	if (checkforx == 0)
	{
		sprintf(TETSErrorStr, "ERROR - Tets.ini file does not contain S/N for requested item:\n %s", powerhead);
		CloseNAM(-200, TETSErrorStr);
	}

	if (TETSiGetSnFlag)
		// Get the whole serial number
		strcpy(addtopowerhead, SerialNumberofPowerHead);
	else
	{	// Get the last four digits of the serial number
		i = strlen(SerialNumberofPowerHead);
		strncpy(addtopowerhead, SerialNumberofPowerHead + i - 4, 4);
	}
}

/************************************************************/
/* MAIN FUNCTION                                            */
/************************************************************/
void tetsMain (int argc, char *argv[])
{
	/* Define Local Variables */
	char			ReadBuffer[256];
	char			sOperation[256];
	unsigned long	numBytesRead;
	static char		addtopowerhead[256];
	char			sCommand[256];
	ViBuf			rBuf[256];
	ViInt32			ErrorCode;
    char			TETSTempFileName[1024] = {""};

	memset(rBuf, '\0', sizeof(rBuf));
	
	//MessageBox(NULL, "Sensor Nam TETS Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	/* Copy passed arguments */

	/* Define ATLAS temp file name and open it */
	sprintf(TETSTempFileName, "%s", argv[1]);
	strcat(TETSTempFileName, SUFFIX);
	vmOpen (TETSTempFileName);
	/* Copy passed argument */
	TETSnOperation = atol(argv[2]);
	vmGetText(TETSnOperation, TETSOperation, MEDIUM_STRING);

	TETSResultsAddr = atol(argv[3]);
	TETSResultStrAddr = atol(argv[4]);


	/* Check for a Valid Agrument */
	if ((strcmp(TETSOperation, "GET_SN_HP8481A") == 0) || (strcmp(TETSOperation, "CAL_HP8481A") == 0))
		strcpy(sOperation, "HP8481A");
	else if ((strcmp(TETSOperation, "GET_SN_HP8481D") == 0) || (strcmp(TETSOperation, "CAL_HP8481D") == 0))
		strcpy(sOperation, "HP8481D");
	else if ((strcmp(TETSOperation, "GET_SN_HP8487D") == 0) || (strcmp(TETSOperation, "CAL_HP8487D") == 0))
		strcpy(sOperation, "HP8487D");
	else if (strcmp(TETSOperation, "GET_SN_HP11708A") == 0)
		strcpy(sOperation, "HP11708A");
	else if (strcmp(TETSOperation, "ZERO") != 0)
	{
		sprintf(TETSErrorStr, "ERROR - Unrecognized argument:\n %s", TETSOperation);
		CloseNAM(-100, TETSErrorStr);
	}

	if (strncmp(TETSOperation, "GET_SN", 6) == 0)
	{
		TETSiGetSnFlag = TRUE;
		get_datatable(sOperation, TETSErrorStr);
		CloseNAM(0, TETSErrorStr);
	}

	TETSiGetSnFlag = FALSE;
	
	/* Get Instrument Handle */
	TETSstatus = hpe1416a_init (RESOURCE_ADDR, VI_FALSE, VI_FALSE, &InstrHndl);
	if (TETSstatus != VI_SUCCESS)
	{
		TETSstatus = hpe1416a_errorMessage(InstrHndl, TETSstatus, ReadBuffer);
		strcpy(TETSErrorStr, "ERROR - Unable to open INSTRUMENT HANDLE.\n");
		strcat(TETSErrorStr, ReadBuffer);
		CloseNAM(-101, TETSErrorStr);
	}
	
	/* Set Timeout value */
	TETSstatus = viSetAttribute(InstrHndl, VI_ATTR_TMO_VALUE, 2000); //2 sec.
	if (TETSstatus != VI_SUCCESS)
	{
		TETSstatus = hpe1416a_errorMessage(InstrHndl, TETSstatus, ReadBuffer);
		strcpy(TETSErrorStr, "ERROR - Unable to set VISA ATTRIBUTE.\n");
		strcat(TETSErrorStr, ReadBuffer);
		CloseNAM(-102, TETSErrorStr);
	} 
		
	
	WriteCommand("*RST;*CLS");

	if (strncmp(TETSOperation, "ZERO", 4) == 0)
	{
		WriteCommand("CAL:ZERO:AUTO ONCE;*OPC?");
		WaitForResponse (45);
		TETSlSystErr = viRead(InstrHndl, (ViPBuf)rBuf, 256, &numBytesRead);
	}
	else
	{
		get_datatable(sOperation, addtopowerhead);

		sprintf(sCommand,"CAL:CSET:SEL \"%s_%s\"", sOperation, addtopowerhead);
		WriteCommand(sCommand);

		WriteCommand("CAL:CSET:STAT ON");
		
		WriteCommand("CAL:ALL?");
		WaitForResponse (60);
		TETSlSystErr = viRead(InstrHndl, (ViPBuf)rBuf, 256, &numBytesRead);
	}
	
	// Check for errors
	if (TETSlSystErr == VI_ERROR_TMO)
	{
		sprintf(TETSErrorStr, "ERROR - Operation Incomplete due to Timeout.");
		CloseNAM(-201, TETSErrorStr);
	}

	Sleep(100);
	TETSlSystErr = hpe1416a_errorQuery(InstrHndl, &ErrorCode, ReadBuffer);
	if (ErrorCode)
	{
		sprintf(TETSErrorStr, "ERROR - Power Meter Execution Error Message: \n");
		strcat(TETSErrorStr, ReadBuffer);
		CloseNAM(-202, TETSErrorStr);
	}

	CloseNAM(0, "SensorNAM - Successful\n");
} /* End main */

