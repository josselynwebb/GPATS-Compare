/*	File: SensorNAM.c
 *	
 *  This program is used as a NAM call from an ATLAS program.  The ATLAS program
 *	passes an argument which is the sensor operation.  The NAM program
 *	then examines the ATLAS program name.TMP file and extracts the argument.
·*	The NAM opens the configuration File and programs the Arb to produce the signal.
 *
 *	File Creation Date: 5/29/06
 *  Version: 1.0
 *  Created By: David Bubenik
 *
 *	Revision Log:
 *	
 *	V1.1 x/x/xxx
 *		xxx.
 */

#include "stdafx.h"
#include "rfms.h"
#include <time.h>
#include <windows.h>
#include <stdio.h>
#include <atlconv.h>
#include "NAM.H"
#include "SensorNAM_TETS.h"


#include "visa.h"
extern "C"
{
	#include "nam.h"
}

#define RESOURCE_ADDR "VXI::28"
#define SUFFIX ".TMP"
#define DOR_MASK  0x2000
#define MSG_RESP_REG 10
#define MEDIUM_STRING 1024
#define MAX_RETRY 3

/* Define Global Variables */

IRFPm_if RFPmObj;

/* Visa Variables */
//ViSession	defaultRM;
//ViSession	InstrHndl;
ViStatus	status;
//ViUInt32	retCnt;


/* TYX data structure - ref. TYX PAWS User Guide Vol 1 Sec 5 */
int Results;

char Operation[MEDIUM_STRING];
char ResultStr[MEDIUM_STRING];

/* Program Globals */
long		nOperation, ResultsAddr, ResultStrAddr;
char		TempFileName[256];
char		ErrorStr[499];
ViChar		ReadBuffer[256];
ViStatus	lSystErr;
int			iGetSnFlag;


/************************************************************/
/* FUNCTIONS                                                */
/************************************************************/

/* strtoupper - Converts a char string to upper case */
char *strtoupper(char *str)
{
	int i;
	for (i=0; str[i] != '\0'; i++)
		str[i] = toupper(str[i]);
	return(str);
}

/* CloseNAM - De-allocates TMP file and closes program */
void CloseNAM(long CloseStatus, char ErrorStr[499])
{
	/* Return Results and Result String to ATLAS */
	/* ref TYX PAWS User Guide Vol 1 Sec 5 */
	Results = CloseStatus;
	strcpy(ResultStr, ErrorStr);
	vmSetInteger(ResultsAddr, Results);
	vmSetText(ResultStrAddr, ResultStr);

	if (!iGetSnFlag) 
	{
		RFPmObj.close(); //hpe1416a_close (InstrHndl);
		CoUninitialize();
	}


	/* Close virtual memory file */
	vmClose();
	exit(0);
}


/* VisaErrorMessage - Collects Visa error message */
char *VisaErrorMessage(ViStatus ErrorStatus)
{
	static ViChar ReadBuffer[256];
	static long err;
	long ErrorCode, ErrorSeverity;
	BSTR	bstr_ErrorDesc;
	BSTR	bstr_MoreErrorInfo;
	

	USES_CONVERSION;

	if (ErrorStatus) 
	{
		bstr_ErrorDesc = SysAllocStringLen(OLESTR(""), 80);
		bstr_MoreErrorInfo = SysAllocStringLen(OLESTR(""), 80);

		err = RFPmObj.getError(&ErrorCode, &ErrorSeverity, &bstr_ErrorDesc, 80, &bstr_MoreErrorInfo, 80);
		sprintf(ReadBuffer, "%s - %s", W2A(bstr_ErrorDesc), W2A(bstr_MoreErrorInfo));

		::SysFreeString(bstr_ErrorDesc);
		::SysFreeString(bstr_MoreErrorInfo);
	}


	return(ReadBuffer);
}

/* WaitForResponse - Waits for a message to be placed in the instruments output buffer */

void WaitForResponse(int iSeconds)
{
	//unsigned short MsgRespReg;
	time_t StartTime;
	time_t Current;
	long int state;

	time( &StartTime );
	time( &Current );

	while((StartTime + (time_t) iSeconds) > Current)
	{
		time(&Current);
		lSystErr = RFPmObj.getCalState(&state);
		if(lSystErr)
		{
			sprintf(ErrorStr, "ERROR - Power Meter Execution Error Message: \n");
			strcat(ErrorStr, VisaErrorMessage(lSystErr));
			CloseNAM(-202, ErrorStr);
		}
		if(state==3) 
			return;
		Sleep(1000);	// delay 1 sec
	}
	RFPmObj.reset();
	
	sprintf(ErrorStr, "ERROR - Operation Incomplete due to Timeout.\n");
	strcat(ErrorStr, VisaErrorMessage(-1));
	CloseNAM(-201, ErrorStr);
}


/*****************************************************************************
 *  This function opens ViperT.ini file in the windows directory and exstracts *
 * 	nessary data about which data table should be used						 *
 *****************************************************************************/
void get_datatable(char powerhead[10], char * addtopowerhead)
{
	const int Max_Length = 128;
	int i, checkforx;	
	char lpBuffer[Max_Length]; 
	char SerialNumberofPowerHead[Max_Length];	// specifies SN of the power head
	LONG lRValue;
	DWORD dwKeyNameLength = Max_Length;
	HKEY hOpenKey; 
    TCHAR PathATSIni[Max_Length] = {0};	

	//Added to get registry information
	//Get the registry key value to point to the ATS.INI file
	lRValue = RegOpenKeyEx(HKEY_LOCAL_MACHINE, TEXT("SOFTWARE\\ATS"), 0, KEY_READ, &hOpenKey);
	lRValue = RegQueryValueEx(hOpenKey, TEXT("IniFilePath"), 0, NULL, (UCHAR*)PathATSIni, &dwKeyNameLength);

	// the GetPrivateProfileString function retrives a string from the specified section in an ini file.
	GetPrivateProfileString("Serial Number", powerhead, "x", lpBuffer, Max_Length, PathATSIni);
	sprintf(SerialNumberofPowerHead,"%s",lpBuffer);

	// checks if the return string is x which means error
	checkforx = strcmp(SerialNumberofPowerHead, "x");

	if (checkforx == 0)
	{
		sprintf(ErrorStr, "ERROR - ATS.ini file does not contain S/N for requested item:\n %s", powerhead);
		strcat(ErrorStr, ReadBuffer);
		CloseNAM(-200, ErrorStr);
	}

	if (iGetSnFlag)
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
void main (int argc, char *argv[])
{
	//MessageBox(NULL, "Sensor Nam Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	/* Define Local Variables */
	char sArgument[16]="";
	char sOperation[9]="";
	char addtopowerhead[30]="";
	char sCommand[100]="";
	long int state;
	char sSystemType[256] = {""};
	int nSize = 256;
	int iRetry = 0;

	//check for TETS, if so goto TETS sensor NAM
	GetPrivateProfileString("System Startup", "SYSTEM_TYPE", "", sSystemType, nSize, "C:\\Users\\Public\\Documents\\ATS\\ATS.ini"); 
	if(strstr(sSystemType, "AN/USM-657(V)2") != NULL)
	{
		tetsMain(argc, argv);
	}
		
	//getchar();  //EADS - add to run with debugger
	/* Copy passed arguments */

	/* Define ATLAS temp file name and open it */
	strcpy(TempFileName, argv[1]);
	strcat(TempFileName, SUFFIX);
	vmOpen(TempFileName);
	/* Copy passed argument */
	nOperation = atol(argv[2]);
	vmGetText(nOperation, Operation, MEDIUM_STRING);
	strtoupper(strcpy(sArgument, Operation));

	ResultsAddr = atol(argv[3]);    //Address for return error number
	ResultStrAddr = atol(argv[4]);  //Address for return string

	/* Check for a Valid Agrument */
	if ((strcmp(sArgument, "GET_SN_HP8481A") == 0) || (strcmp(sArgument, "CAL_HP8481A") == 0))
		strcpy(sOperation, "HP8481A");
	else if ((strcmp(sArgument, "GET_SN_HP8481D") == 0) || (strcmp(sArgument, "CAL_HP8481D") == 0))
		strcpy(sOperation, "HP8481D");
	else if ((strcmp(sArgument, "GET_SN_HP8487D") == 0) || (strcmp(sArgument, "CAL_HP8487D") == 0))
		strcpy(sOperation, "HP8487D");
	else if (strcmp(sArgument, "GET_SN_HP11708A") == 0)
		strcpy(sOperation, "HP11708A");
	else if (strcmp(sArgument, "ZERO") != 0)
	{
		sprintf(ErrorStr, "ERROR - Unrecognized argument:\n %s", sArgument);
		CloseNAM(-100, ErrorStr);
	}

	if (strncmp(sArgument, "GET_SN", 6) == 0)
	{
		iGetSnFlag = TRUE;
		get_datatable(sOperation, &ErrorStr[0]);
		CloseNAM(0, ErrorStr);
	}

	iGetSnFlag = FALSE;
	CoInitialize(NULL);	

	/* Get Instrument Handle */
	status = 0;
	RFPmObj.CreateDispatch(_T("RFMS.RFPm_if"));
	status=RFPmObj.open(0);
	if (status != VI_SUCCESS)
	{
		strcpy(ErrorStr, "ERROR - Unable to open INSTRUMENT HANDLE.\n");
		strcat(ErrorStr, VisaErrorMessage(-1));
		CloseNAM(-101, ErrorStr);
	}
	
	/* Set Timeout value */
	status = RFPmObj.setMaxTime(2);
	if (status != VI_SUCCESS)
	{
		strcpy(ErrorStr, "ERROR - Unable to set VISA ATTRIBUTE.\n");
		strcat(ErrorStr, VisaErrorMessage(-1));
		CloseNAM(-102, ErrorStr);
	} 
		
	RFPmObj.reset();
	
	if (strncmp(sArgument, "ZERO", 4) == 0)
	{
		iRetry = 0;
		while(iRetry < MAX_RETRY)
		{
			lSystErr=RFPmObj.doZero();
			if(lSystErr)
			{
				iRetry ++;
				continue;				
			}
			else
			{
				break;
			}			
		}

		if(iRetry == MAX_RETRY && lSystErr)
		{
			sprintf(ErrorStr, "ERROR - Power Meter Execution Error Message: \n");
			strcat(ErrorStr, VisaErrorMessage(lSystErr));
			CloseNAM(-202, ErrorStr);
		}
		
		WaitForResponse (45);
		lSystErr = RFPmObj.getCalState(&state);
	}
	else
	{
		get_datatable(sOperation, &addtopowerhead[0]);

		strncpy(sCommand, sOperation+2, strlen(sOperation)-1); //don't copy first two chars
		lSystErr = RFPmObj.setPowerHead(sCommand);
		if(lSystErr)
		{
			sprintf(ErrorStr, "ERROR - Power Meter Execution Error Message: \n");
			strcat(ErrorStr, VisaErrorMessage(lSystErr));
			CloseNAM(-202, ErrorStr);
		}
		
		iRetry = 0;
		while(iRetry < MAX_RETRY)
		{
			lSystErr = RFPmObj.doZeroAndCal();
			if(lSystErr)
			{
				iRetry ++;
				continue;				
			}
			else
			{
				break;
			}			
		}
		
		if(iRetry == MAX_RETRY && lSystErr)
		{
			sprintf(ErrorStr, "ERROR - Power Meter Execution Error Message: \n");
			strcat(ErrorStr, VisaErrorMessage(lSystErr));
			CloseNAM(-202, ErrorStr);
		}
		WaitForResponse (60);
		lSystErr = RFPmObj.getCalState(&state);
	}
	
	// Check for errors
	if(lSystErr)
	{
		sprintf(ErrorStr, "ERROR - Power Meter Execution Error Message: \n");
		strcat(ErrorStr, VisaErrorMessage(lSystErr));
		CloseNAM(-202, ErrorStr);
	}

	CloseNAM(0, "SensorNAM - Successful\n");
} /* End main */



