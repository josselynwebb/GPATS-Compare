/////////////////////////////////////////////////////////////////////////////
//	File:	utilities.cpp													/
//																			/
//	Creation Date:	30 June 2008											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of fhdb nam, visual dll software no	/
//					longer available.  Include the dll code into the nam	/
//					program.												/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////	

#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <io.h>
#include <fcntl.h>
#include <windows.h>
#include <string.h>
#include <math.h>
#include <stdlib.h>
#include <time.h>
#include "fhdb.h"
#include <nam.h>

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
//	setTimeValue:	This function will extract the time elements from the
//						character string passed to it and set the correct time
//						structure's values.  To get the year we will use the
//						localtime() function.
//
//	Parameters:
//		timeString:		This a character string containing an ATLAS/TYX
//						formatted date time.
//		timeElement:	This is a integer indicating which time we are getting.
//
//	Returns:
//		SUCCESS:		  0  - No errors
//		NAM_ERROR:		(-1) - Error occured
//
//
int setTimeValue(char *timeString, int timeElement)
{
	int			i, j = 0, sscanfReturn;
	int			dcYear = 0, dcSecond = 0;
	char		tmpTimeBuf[22];
	time_t		curtp;
	struct tm	*tmpt;
	TIME		*p;

	time(&curtp);
	tmpt = localtime(&curtp);

	tmpTimeBuf[0] = '\0';

	for (i = 0, j = 0; timeString[i] != '\0'; i++, j++) 
	{
		if (j == 2 || j == 5 || j == 8 || j == 11 || j == 14 || j == 17) 
		{
			tmpTimeBuf[j] = ';';
			j++;
			tmpTimeBuf[j] = timeString[i];
		}
		else 
		{
			tmpTimeBuf[j] = timeString[i];
		}
	}

	tmpTimeBuf[j] = '\0';

	p = timeElement == STARTTIME ? &dataCollectionfp.dateTimeStart : &dataCollectionfp.dateTimeStop;

	if ((sscanfReturn = sscanf(tmpTimeBuf, "%d;%u;%u;%u;%u;%u;%d", &dcYear,
							   &p->month, &p->day, &p->hour, &p->min, &p->sec, &dcSecond)) != 7)
	{

		dodebug(NO_PRE_DEFINE, "setTimeValue()", "%s%d%s", "sscanf only converted ", sscanfReturn,
				" and needed to do 7 conversions", (char*)NULL);
		return(NAM_ERROR);
	}

	p->year = (short)(tmpt->tm_year + 1900);

	return(SUCCESS);
}

//
//	setCharString:	This function will extract the time elements from the
//						character string passed to it and set the correct time
//						structure's values.  To get the year we will use the
//						localtime() function.
//
//	Parameters:
//		charString:		This a character string containing the information that the
//						structElement is set to.
//		structElement:	This is a integer indicating which structure element that
//						charString is for.
//
//	Returns:
//		SUCCESS:		  0  - No errors
//		NAM_ERROR:		(-1) - Error occured
//
//
int setCharString(char *charString, int structElement)
{
	int		snprintfReturn = 0;

	switch(structElement + NotAtlas) 
	{
		case ERONUMBER:
			if ((snprintfReturn = _snprintf(dataCollectionfp.eroNo, ERO_SIZE + TERMINATOR, charString)) < SUCCESS) 
			{
				dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the ERO No failed", (char*)NULL);
			}
			break;
    
		case TPCCNUMBER:
			if ((snprintfReturn = _snprintf(dataCollectionfp.tpsProgCntrlNo, TPCCN_SIZE + TERMINATOR, charString)) < SUCCESS)
			{
				dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the TPCCN failed", (char*)NULL);
			}
			break;
    
		case UUTSERIALNO:
			if ((snprintfReturn = _snprintf(dataCollectionfp.uutSerialNo, UUT_SERIAL_SIZE + TERMINATOR, charString)) < SUCCESS) 
			{
				dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the UUT Serial No failed", (char*)NULL);
			}
			break;
    
		case UUTREVNUMBER:
			if ((snprintfReturn = _snprintf(dataCollectionfp.uutRev, REVISION_SIZE + TERMINATOR, charString)) < SUCCESS) 
			{
				dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the UUT Revision failed", (char*)NULL);
			}
			break;
    
		case IDSERIALNUMBER:
			if ((snprintfReturn = _snprintf(dataCollectionfp.idSerialNo, ID_SERIAL_SIZE + TERMINATOR, charString)) < SUCCESS) 
			{
				dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the ID Serial No failed", (char*)NULL);
			}
			break;
    
		case FAILEDSTEP:
			if ((snprintfReturn = _snprintf(dataCollectionfp.failedStep, FAILED_STEP_SIZE + TERMINATOR, charString)) < SUCCESS) 
			{
				dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the Failed Step No failed", (char*)NULL);
			}
			break;
    
		case FAULTMESSAGE:
			if ((extractFaultInfo(charString)) == NO_FILE) 
			{
				if ((snprintfReturn = _snprintf(dataCollectionfp.faultCallout, FAULT_CALLOUT_SIZE + TERMINATOR, charString)) < SUCCESS)
				{
					dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the Fault Callout failed", (char*)NULL);
				}
				ConvertCRLF(dataCollectionfp.faultCallout);
			}
			break;
    
		case DIMENSION:
			if ((snprintfReturn = _snprintf(dataCollectionfp.dimension, DIMENSION_SIZE + TERMINATOR, charString)) < SUCCESS) 
			{
				dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the Dimension failed", (char*)NULL);
			}
			break;
    
		case OPERCOMMENT:
			if ((snprintfReturn = _snprintf(dataCollectionfp.operatorComments, OP_COMMENT_SIZE + TERMINATOR, charString)) < SUCCESS) 
			{
				dodebug(NO_PRE_DEFINE, "setCharString()", "Setting of the Operator Comments", (char*)NULL);
			}
			ConvertCRLF(dataCollectionfp.operatorComments);
			break;
    
		default :
			dodebug(NO_PRE_DEFINE,"setCharString()", "%s%d", "Improper value passed - ", structElement + NotAtlas);
			fhdbInfo.functionStatus = NAM_ERROR;
			break;
	}
	
	return(fhdbInfo.functionStatus);
}

//
//	extractFaultInfo:	This function will extract the fault message, indicating the
//							cause of the failing TPS.  This could be either a path to
//							a file containg the fault message, or the message itself.
//							First will determine if it is a file name, then if not put
//							the message into the proper variable.
//
//	Parameters:
//		charString:		This a character string containing an ATLAS/TYX
//						formatted date time.
//
//	Returns:
//		SUCCESS:		  0  - No errors
//		NAM_ERROR:		(-1) - Error occured
//
int extractFaultInfo(char *charString)
{
	struct _stat	tmpBuf;

	if (!(_stat(charString, &tmpBuf))) 
	{
		if ((checkForDigitalFaultCallout(charString)) == TRUE) 
		{
			if (dtbFaultCallout(charString)) 
			{
				return(NAM_ERROR);
			}
			return (SUCCESS);
		}
		else 
		{
			if (getBigMessage(charString)) 
			{
				return(NAM_ERROR);
			}
			return (SUCCESS);
		}
		// return(NAM_ERROR);  // Unreachable
	}

	return(NO_FILE);
}


//
// checkFor:		This program will use the task parameter to perform the
//						required function.
// Parameters:
//		task:		Determines which one of the four function is to be performed.
//		i:			Used to index into the diaFile string.
//		k:			Used to index into the fcBuf.
//		diaFile:	The character string containing the dia file contents unmodified.
//		fcBuf:		The character string containing the dia file modified contents.
//
// Returns:
//		none:		Void function - NO RETURN, It just happens MAGICALLY
//
void checkFor(int task, int *i, int *k, char *diaFile, char *fcBuf)
{
	switch(task) {

		case NEWLINESPACE:
			// Here I am checking for the line (              Lead:  4).
			// I am deleting from the \n to the number, and inserting a (,)
			// and a space before the number.		
			if (!strncmp(&diaFile[*i], "\n                   ", strlen("\n                   "))) 
			{
				for (; diaFile[*i] != '\0'; (*i)++) 
				{
					if (diaFile[*i] == ':') 
					{							
						for (; diaFile[*i] != ' '; (*i)++)
						{
							;
						}
							
						break;
					}
				}

				if (diaFile[*i] != '\0') 
				{
					fcBuf[*k] = ',';
					(*k)++;
				}

			}
			break;
		
		case NINESPACES:

			// Here I am checking for the line with 9 spaces.
			// I am deleting the spaces and inserting 2 \t in their place.
			if (!strncmp(&diaFile[*i], "         ", strlen("         "))) 
			{
				for (; diaFile[*i] != '\0'; (*i)++) 
				{
					if (diaFile[*i] != ' ') 
					{	
						fcBuf[*k] = '\t';
						(*k)++;
						fcBuf[*k] = '\t';
						(*k)++;
						break;
					}
				}
			}
			break;

		case DETECTPOINTS:
			// Here I am checking for the line that starts Detect points.
			// I am deleting this line.			
			if (!strncmp(&diaFile[*i], "    Detect points", strlen("    Detect points"))) 
			{
				for (; diaFile[*i] != '\0'; (*i)++) 
				{
					if (diaFile[*i] == 'F')
					{
						if (!strncmp(&diaFile[*i], "Fau", 3)) 
						{
							fcBuf[*k] = '\n';
							(*k)++;
							fcBuf[*k] = ' ';
							break;
						}
					}
				}
			}
			break;

		case FOURSPACES:

			// Here I am checking for the line with 4 spaces.
			// I am deleting the spaces and inserting 1 \t in their place.
			if (!strncmp(&diaFile[*i], "    ", strlen("    "))) 
			{
				for (; diaFile[*i] != '\0'; (*i)++) 
				{
					if (diaFile[*i] != ' ') 
					{	
						fcBuf[*k] = '\t';
						(*k)++;
						break;
					}
				}
			}
			break;

		default :

			dodebug(NO_PRE_DEFINE, "checkFor()", "Invalid task sent %d", task, (char*)NULL);
			break;
	}
}


//
// getTemperature:	This program will if it can fill in the data base fhdb.mdb.
//
// Parameters:
//		none:		This function will use the m_Faults, tm and faultfp structures.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
int getTemperature(void)
{
	int	returnValue = 0;
	char windersDirectory[NAM_MAX_PATH];
	char iniFullPath[NAM_MAX_PATH];
	char iniTemperatureReading[TEMPERATURE_SIZE];
	DWORD dwType = REG_SZ;
	HKEY hKey = 0;
	char path[1024];
	DWORD value_length = 1024;
	const char* subkey = "SOFTWARE\\ATS";

	returnValue = GetWindowsDirectory(windersDirectory, NAM_MAX_PATH);
	
	if (returnValue > NAM_MAX_PATH) 
	{
		dodebug(NO_PRE_DEFINE, "getTemperature()", "Failed to get winders dir.  size is larger then %d",
				NAM_MAX_PATH, (char*)NULL);
		fhdbInfo.returnValue = NAM_ERROR;
	}
	else if (returnValue == FALSE) 
	{
		
		DWORD	errorValue = 0;

		errorValue = GetLastError();

		dodebug(errorValue, "getTemperature()", (char*)NULL, (char*)NULL);
		fhdbInfo.returnValue = NAM_ERROR;
	}
	else 
	{
	
		RegOpenKey(HKEY_LOCAL_MACHINE,subkey,&hKey);
		if(RegQueryValueEx(hKey, "IniFilePath", NULL, &dwType, (LPBYTE)&path, &value_length) != ERROR_SUCCESS)
		{
			strcpy(iniFullPath, "");
			return NAM_ERROR;
		}

		sprintf(iniFullPath, "%s", path);

		GetPrivateProfileString(APPNAME, KEYNAME, "", iniTemperatureReading, 12, iniFullPath);

		if ((dataCollectionfp.temperature = atof(iniTemperatureReading)) == 0.0) 
		{
			dodebug(NO_PRE_DEFINE, "getTemperature()", "Failed to get tester's temperature", (char*)NULL);
			fhdbInfo.returnValue = NAM_ERROR;
		}
		else
		{
			fhdbInfo.returnValue = SUCCESS;
		}
	}

	return (fhdbInfo.returnValue == FALSE ? SUCCESS : NAM_ERROR);
}

//
//	setValue:	This function will set the value of the structure element
//						that is indicated by the structElement variable;
//
//	Parameters:
//		valueToSet:		This is the value that the structure element will be set to.
//		structElement:	This is a integer indicating which structure element that
//						charString is for.
//
//	Returns:
//		none:		Void function - NO RETURN, It just happens MAGICALLY
//
//
void setValue(double valueToSet, int structElement)
{
	switch(structElement + NotAtlas) 
	{
		case MEASUREVALUE:
			dataCollectionfp.measureValue = valueToSet;
			break;
    
		case UPPERLIMIT:
			dataCollectionfp.upperLimit = valueToSet;
			break;
    
		case LOWERLIMIT:
			dataCollectionfp.lowerLimit = valueToSet;
			break;
        
		default :
			dodebug(NO_PRE_DEFINE,"setValue()", "%s%d", "Improper value passed - ", structElement + NotAtlas);
			fhdbInfo.functionStatus = NAM_ERROR;
			break;
	}
	
}


//
// checkForDigitalFaultCallout:	This function will check to see if the file
// 								corresponding to the name of the file being passed
// 								in the variable fileToCheck contains any digital
// 								diagnosis of is just a file that was used becaused
// 								the fault callout was larger then 256 characters.
// 								The first thing is to check the name of the file.
// 								It can be one of two names, for which the file
// 								contents should be known.  If not then open the
// 								file and search for two characters strings that
// 								will determine if it is or isn't a digital diagnosis
// 								callout. Will still check even if the file name
// 								is of a digital diagnosis.
//
// Parameters:
//		fileToCheck:	Name of file to check to see if this is a digital burst
//							or just a fault callout that is larger then 256 characters
//							and was put into a file.
//
// Returns:
//		FALSE:		  0		= Not a digital diagnosis fault callout file.
//		TRUE:		  1		= Is a digital diagnosis fault callout file.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
int checkForDigitalFaultCallout(char *fileToCheck)
{
	int				i, digitalDetected = 0;
	int				dtbfd;
	unsigned int	readValue;
	char			dtbFileBuf[NAM_MAX_PATH];
	char			*dtbFileContents;
	struct _stat	dtbfz;

	// Make sure that all files are written and the info is there before
	// this programs opens the files for reading and writing.
	fflush(NULL);

	sprintf(dtbFileBuf, "%s%s", LOGLOCATION, SHELLTMPPCOF);

	if (!(_strnicmp(fileToCheck, dtbFileBuf, strlen(dtbFileBuf)))) 
	{
		return PCOF_TMP;
	}

	ZeroMemory(dtbFileBuf, NAM_MAX_PATH);

	// Set up the buffers with the proper file names.
	sprintf(dtbFileBuf, "%s", fileToCheck);

	// Now first get the stats, read size, then open the file.  Then allocate
	// the memory for the buffer that the file will be read into. Then close
	// the file, not needed any more.
	if ((_stat(dtbFileBuf, &dtbfz)) == NAM_ERROR) 
	{
		dodebug(errno, "checkForDigitalFaultCallout()", NULL, NULL);
		dodebug(NO_PRE_DEFINE, "checkForDigitalFaultCallout()", "File name is %s", dtbFileBuf, (char*)NULL);
		return(NAM_ERROR);
	}

	if ((dtbfd = _open(dtbFileBuf, _O_RDONLY | _O_BINARY)) == NAM_ERROR) 
	{
		dodebug(errno, "checkForDigitalFaultCallout()", NULL, NULL);
		fhdbInfo.returnValue = FILEOPEN;
		return(NAM_ERROR);
	}

	if ((dtbFileContents = (char*)malloc((size_t)dtbfz.st_size)) != NULL) 
	{
		if ((readValue = _read(dtbfd, dtbFileContents, (size_t)dtbfz.st_size)) != (size_t)dtbfz.st_size) 
		{
			free(dtbFileContents);
			dodebug(errno, "checkForDigitalFaultCallout()", NULL, NULL);
			fhdbInfo.returnValue = FILEREAD;
			return(NAM_ERROR);
		}
		dtbFileContents[(size_t)dtbfz.st_size] = '\0';
	}
	else 
	{
		dodebug(errno, "checkForDigitalFaultCallout()", NULL, NULL);
		fhdbInfo.returnValue = FILEREAD;
		return(NAM_ERROR);
	}
		
	_close(dtbfd);

	digitalDetected = FALSE;

	// Here will check for one of the two strings that should determine if this is a digital
	// diagnosis fault callout. Guided Probe Diagnosis: or Fault Dictionary Diagnosis:
	for (i = 0; (size_t)i < (size_t)dtbfz.st_size; i++) 
	{
		if (dtbFileContents[i] == 'G') 
		{
			if (!_strnicmp(&dtbFileContents[i], "Guided Probe Diagnosis:", strlen("Guided Probe Diagnosis:"))) 
			{
				digitalDetected++;
				break;
			}
		}
		if (dtbFileContents[i] == 'F')
		{
			if (!_strnicmp(&dtbFileContents[i], "Fault Dictionary Diagnosis:", strlen("Fault Dictionary Diagnosis:"))) {
				digitalDetected++;
				break;
			}
		}
	}

	return (digitalDetected == FALSE ? FALSE : TRUE);
}

//
// getBigMessage:	This function will insert the file contents pointed to by the variable
//						fileToCheck into the variable dataCollection.faultCallout.  
//
// Parameters:
//		fileToCheck:	Name of file whose contents are to be inserted into the
//							variable dataCollection.faultCallout.
//
// Returns:
//		SUCCESS:	  0		= The function did what is was suppose to dooo.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
int getBigMessage(char *fileToCheck)
{
	int				faultfd, snprintfReturn = 0;
	unsigned int	readValue;
	char			faultFileBuf[NAM_MAX_PATH];
	char			*faultFileContents;
	struct _stat	faultfz;

	// Make sure that all files are written and the info is there before
	// this programs opens the files for reading and writing.
	fflush(NULL);

	// Set up the buffers with the proper file names.
	sprintf(faultFileBuf, "%s", fileToCheck);

	// Now first get the stats, read size, then open the file.  Then allocate
	// the memory for the buffer that the file will be read into. Then close
	// the file, not needed any more.
	if ((_stat(faultFileBuf, &faultfz)) == NAM_ERROR) 
	{
		dodebug(errno, "getBigMessage()", NULL, NULL);
		dodebug(NO_PRE_DEFINE, "getBigMessage()", "File name is %s", faultFileBuf, (char*)NULL);
		fhdbInfo.returnValue = FILEREAD;
		return(NAM_ERROR);
	}

	if ((faultfd = _open(faultFileBuf, _O_RDONLY | _O_BINARY)) == NAM_ERROR) 
	{
		dodebug(errno, "getBigMessage()", NULL, NULL);
		fhdbInfo.returnValue = FILEOPEN;
		return(NAM_ERROR);
	}

	if ((faultFileContents = (char*)malloc((size_t)faultfz.st_size)) != NULL) 
	{
		if ((readValue = _read(faultfd, faultFileContents, (size_t)faultfz.st_size)) != (size_t)faultfz.st_size) 
		{
			free(faultFileContents);
			dodebug(errno, "getBigMessage()", NULL, NULL);
			fhdbInfo.returnValue = FILEREAD;
			return(NAM_ERROR);
		}
		faultFileContents[(size_t)faultfz.st_size] = '\0';
	}
	else
	{
		dodebug(errno, "getBigMessage()", NULL, NULL);
		fhdbInfo.returnValue = FILEREAD;
		return(NAM_ERROR);
	}
		
	_close(faultfd);

	if ((snprintfReturn = _snprintf(dataCollectionfp.faultCallout, 20000, faultFileContents)) < 0)
	{
		dodebug(NO_PRE_DEFINE, "getBigMessage()", "Setting of the Fault Callout failed", (char*)NULL);
	}

	dataCollectionfp.faultCallout[(strlen(faultFileContents)) > 20000 ? 20000 : strlen(faultFileContents)] = '\0';
	
	return SUCCESS;
}

//
//
// CheckIntValues:	This function will first get the address of the
//					variable then check to see if it is an integer type.
//
// Parameters:
//		ArgcValue:		The argument passed to main.
//		argv:			Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//
int CheckIntValues(int ArgcValue, char *argv[])
{
	long	TmpLongValue;

	TmpLongValue = atol(argv[ArgcValue]);

	if (vmGetDataType(TmpLongValue) != ITYPE) 
	{
		dodebug(DATAINT, "CheckIntValues()", (char *)NULL, (char *)NULL);
		fhdbInfo.returnValue = DATAINT;
		return(NAM_ERROR);
	}

	return SUCCESS;
}

//
//
// CheckCharValues:	This function will first get the address of the
//					variable then check to see if it is an character type.
//
// Parameters:
//		ArgcValue:		The argument passed to main.
//		argv:			Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//
int CheckCharValues(int ArgcValue, char *argv[])
{
	long	TmpLongValue;

	TmpLongValue = atol(argv[ArgcValue]);

	if (vmGetDataType(TmpLongValue) != TTYPE) 
	{
		dodebug(DATACHAR, "CheckCharValues()", (char *)NULL, (char *)NULL);
		fhdbInfo.returnValue = DATACHAR;
		return(NAM_ERROR);
	}

	return SUCCESS;
}

//
//
// CheckBoolValues:	This function will first get the address of the
//					variable then check to see if it is an boolean type.
//
// Parameters:
//		ArgcValue:		The argument passed to main.
//		argv:			Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//
int CheckBoolValues(int ArgcValue, char *argv[])
{
	long	TmpLongValue;

	TmpLongValue = atol(argv[ArgcValue]);

	if (vmGetDataType(TmpLongValue) != NTYPE) 
	{
		dodebug(DATABOOL, "CheckBoolValues()", (char *)NULL, (char *)NULL);
		fhdbInfo.returnValue = DATABOOL;
		return(NAM_ERROR);
	}

	return SUCCESS;
}

//
//
// CheckDecimalValues:	This function will first get the address of the
//					variable then check to see if it is an decimal type.
//
// Parameters:
//		ArgcValue:		The argument passed to main.
//		argv:			Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//
int CheckDecimalValues(int ArgcValue, char *argv[])
{
	long	TmpLongValue;

	TmpLongValue = atol(argv[ArgcValue]);

	if (vmGetDataType(TmpLongValue) != RTYPE) 
	{
		dodebug(DATAREAL, "CheckDecimalValues()", (char *)NULL, (char *)NULL);
		fhdbInfo.returnValue = DATABOOL;
		return(NAM_ERROR);
	}

	return SUCCESS;
}

//
// ConverCRLF:	This function will convert the \n being passed on
//					command line to the fhdb to \r\n
//
// Parameters:
//		StringToConvert:	The character string to convert..
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//

void ConvertCRLF(char *StringToConvert)
{

	int		i;
	char	TmpString1[FAULT_CALLOUT_SIZE + TERMINATOR];

	sprintf(TmpString1, "%s", StringToConvert);

	*StringToConvert = '\0';

	for (i = 0; TmpString1[i] != '\0'; i++) {
	
		if (TmpString1[i] == '\\' && TmpString1[i + 1] == 'n') {
			StringToConvert[i] = '\r';
			i++;
			StringToConvert[i] = '\n';
		}
		else {
			StringToConvert[i] = TmpString1[i];
		}
	}

	StringToConvert[i] = '\0';

	return;
}