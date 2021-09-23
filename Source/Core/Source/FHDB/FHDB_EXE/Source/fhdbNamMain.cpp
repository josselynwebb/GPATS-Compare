/////////////////////////////////////////////////////////////////////////////
//	File:	fhdbNamMain.cpp													/
//																			/
//	Creation Date:	30 June 2008											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of fhdb nam, visual dll software no	/
//					longer available.  Include the dll code into the nam	/
//					program.												/
//		2.0.1.0		Added another debug statement to the argument checking	/
//					function in fhdbNamMain(),to display total arguments	/
//					passed to the program.									/
//		2.1.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//		2.1.1.0		Adding changes from VIPERT 1.3.2.0						/
//																			/
//                                                                          /
//  Updated 8/31/2015 by Jess Gillespie                                     /
//      - Update code from vs2006 to VS2012                                 /
//                                                                          /
//                                                                          /
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////	

#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <windows.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
#include "stdafx.h"
#include "fhdb.h"
#include "nam.h"


/////////////////////////////////////////////////////////////////////////////
//		External Variables and Routines										/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////

#define IsBTYPE(x)	(DType((x).d_sztyp) == NTYPE)

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

int		NotAtlas;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// fhdbNamMain:	This function will check the variables that were passed to
//					it and by these variables determine which function to call.
//					Will also initilize the TYX interface.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//
int fhdbNamMain (int argc, char *argv[])
{
	//long			xad;
	char			TempFileName[NAM_MAX_PATH];
	//long	x;
	struct _stat	tmpBuf;

	if(argc < 2)
	{
		return (NAM_ERROR);
	}

	NotAtlas = FALSE;

	sprintf(TempFileName, "%s%s", argv[TMP_FILE], SUFFIX);

	if ((_stat(TempFileName, &tmpBuf)) == NAM_ERROR) 
	{
		dodebug(NO_PRE_DEFINE, "fhdbnamMain()", "Not an ATLAS program", (char*)NULL);
		NotAtlas = TRUE;
	}

	if (NotAtlas == FALSE) 
	{		
		if(vmOpen(argv[TMP_FILE]) < SUCCESS)
		{
			dodebug (NO_PRE_DEFINE, "fhdbNamMain()", "Cannot open virtual memory file %s", argv[1], (char*)NULL);
			return (NAM_ERROR);
		}
	}

	/*if (argc < (ARGC_MIN - NotAtlas) || argc > (ARGC_MAX - NotAtlas)) 
	{
		dodebug(ARGNUM, "fhdbnamMain()", (char*)NULL, (char*)NULL);
		fhdbInfo.returnValue = ARGNUM;
		return(NAM_ERROR);
	}*/

	if (opendb()) 
	{
		return(fhdbInfo.returnValue = NAM_ERROR);
	}

	if (NotAtlas == FALSE) 
	{
		if (parseArguments(argc, argv)) 
		{
			fhdbInfo.returnValue = NAM_ERROR;
		}
	}
	else if (NotAtlas == TRUE) 
	{
		if (parseCommandLine(argc, argv))
		{
			fhdbInfo.returnValue = NAM_ERROR;
		}
	}

	if (getTemperature()) 
	{
		fhdbInfo.returnValue = NAM_ERROR;
	}

	if (fillInDataBase()) 
	{
		fhdbInfo.returnValue = NAM_ERROR;
	}

	if (NotAtlas == FALSE) 
	{
		long rtnAddress;
		rtnAddress = atol(argv[RETURN_STATUS]);
		vmSetInteger(rtnAddress, fhdbInfo.returnValue);
		vmClose();

		/*xad = atol(argv[(ARGC_MAX - NotAtlas) - 1]);
		//x = vmGetDecimal(xad);
		x = fhdbInfo.returnValue;
		//vmSetDecimal(xad, x);
		if(vmClose() < 0)
			return (NAM_ERROR);*/
	}

	return(fhdbInfo.returnValue != SUCCESS ? NAM_ERROR : SUCCESS);
}

//
// parseArguments:	This function will parse through the argv list, checking
//						for proper usage.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//
int parseArguments(int argc, char *argv[])
{
	int		ArgumentNumber = 0;
	int		FirstDataArg = 2;
	long	ArgumentAddr = 0;
	double	tempDouble = 0.0;
	char	tmpBuf[FAULT_CALLOUT_SIZE + TERMINATOR];

	for(ArgumentNumber = FirstDataArg; ArgumentNumber < argc - THE_RETURN; ArgumentNumber++)
	{
		ArgumentAddr = atol(argv[ArgumentNumber]);

		switch(ArgumentNumber) 
		{

			case STARTTIME:
			case STOPTIME:

				if (CheckCharValues(ArgumentNumber, argv)) 
				{
					return (NAM_ERROR);
				}

				vmGetText(ArgumentAddr, tmpBuf, sizeof(tmpBuf) / sizeof(char));

				if (setTimeValue(tmpBuf, ArgumentNumber)) 
				{
					return(NAM_ERROR);
				}

				break;

			case ERONUMBER:
			case TPCCNUMBER:
			case UUTSERIALNO:
			case UUTREVNUMBER:
			case IDSERIALNUMBER:
			case FAILEDSTEP:
			case FAULTMESSAGE:
			case DIMENSION:
			case OPERCOMMENT:

				if (CheckCharValues(ArgumentNumber, argv)) 
				{
					return (NAM_ERROR);
				}

				vmGetText(ArgumentAddr, tmpBuf, sizeof(tmpBuf));

				if (setCharString(tmpBuf, ArgumentNumber)) 
				{
					return(NAM_ERROR);
				}

				break;

			case TESTSTATUS:

				if (CheckBoolValues(ArgumentNumber, argv)) 
				{
					return (NAM_ERROR);
				}

				dataCollectionfp.testStatus = vmGetBool(ArgumentAddr);

				break;

			case UPPERLIMIT:
			case LOWERLIMIT:
			case MEASUREVALUE:


				if (CheckDecimalValues(ArgumentNumber, argv)) 
				{
					return (NAM_ERROR);
				}

				tempDouble = vmGetDecimal(ArgumentAddr);
				setValue(tempDouble, ArgumentNumber);

				break;

			default:

				dodebug(UNKOPT, "parseArguments()", (char*)NULL, (char*)NULL);
				dodebug(NO_PRE_DEFINE, "parseArguments()", "i = %d", ArgumentNumber, (char*)NULL);
				fhdbInfo.returnValue = UNKOPT;
				return(NAM_ERROR);

				break;
		}
	}
	return(SUCCESS);
	/*int i = 0;
	long xad=0;
	char tmpBuf[5001];
	int	x_Integer=0;
	long x_Long=0;
	TBool x_Bool=false;
	char x_Text[MAX_STRING];
	int	ArgumentNumber = 0;


	for (i = 2; i < argc; i++) {

		xad = atol(argv[i]);
		memset(tmpBuf, '\0', sizeof(tmpBuf));

		if ((i > 1 && i < 9) || (i == 10 || i == 11 || i == 13 || i == 16)) {

			if (vmGetDataType(xad) != TTYPE) { 
				dodebug(DATACHAR, "parseArguments()", (char*)NULL, (char*)NULL);
				fhdbInfo.returnValue = DATACHAR;
				return(NAM_ERROR);
			}
			else
			{
				vmGetText(xad, x_Text, MAX_STRING);
			}

			if (i == STARTTIME || i == STOPTIME) {

				if ((_snprintf(tmpBuf, 16, "%s", x_Text)) != 15) {
					dodebug(0, "parseArguments()", "Failed snprintf function", (char*)NULL);
					fhdbInfo.returnValue = NAM_ERROR;
					return(NAM_ERROR);
				}
				
				if (setTimeValue(tmpBuf, i)) {
					return(NAM_ERROR);
				}

			}
			else {
				if (setCharString(x_Text, i)) {
					return(NAM_ERROR);
				}
			}
		}
		else if (i == 12 || (i == 14 || i == 15)) {

			if (vmGetDataType(xad) != RTYPE) {				
				dodebug(DATAREAL, "parseArgument()", (char*)NULL, (char*)NULL);
				fhdbInfo.returnValue = DATAREAL;
				return(NAM_ERROR);
			}
			else
			{
				x_Long = (long) vmGetDecimal(xad);
				setValue(x_Long, i);
			}
		}
		else if (i == 9) {

			if (vmGetDataType(xad) == NTYPE) { 
				x_Bool = vmGetBool(xad);
				dataCollectionfp.testStatus = x_Bool;
				dodebug(DATABOOL, "parseArguments()", (char*)NULL, (char*)NULL);
				fhdbInfo.returnValue = DATABOOL;
			}
			else if (vmGetDataType(xad) == TTYPE) { 
				vmGetText(xad, x_Text, MAX_STRING);
				if(!_stricmp(x_Text, "PASSED"))
					dataCollectionfp.testStatus = x_Bool = true;
				else if(!_stricmp(x_Text, "FAILED"))
					dataCollectionfp.testStatus = x_Bool = false;
				dodebug(DATABOOL, "parseArguments()", (char*)NULL, (char*)NULL);
				fhdbInfo.returnValue = DATABOOL;
			}
			else {	
				return(NAM_ERROR);
			}
		}
		else if (i == 17) {

			if (vmGetDataType(xad) == ITYPE) {
				dodebug(DATAINT, "parseArguments()", (char*)NULL, (char*)NULL);
				fhdbInfo.returnValue = DATAINT;
				return(NAM_ERROR);
			}
			else
			{
				x_Integer = vmGetInteger(xad);
			}
		}
		else {
			dodebug(UNKOPT, "parseArguments()", (char*)NULL, (char*)NULL);
			dodebug(0, "parseArguments()", "i = %d", i, (char*)NULL);
			fhdbInfo.returnValue = UNKOPT;
			return(NAM_ERROR);
		}
	}

	return(SUCCESS);*/
}

//
// parseCommandLine:	This function will parse through the argv list, checking
//						for proper usage.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//
int parseCommandLine(int argc, char *argv[])
{
	int	ArgumentNumber;
	int FirstDataArgument = 1;
	double tmpDouble = 0.0;
	char tmpBuf[FAULT_CALLOUT_SIZE + TERMINATOR];

	for (ArgumentNumber = FirstDataArgument; ArgumentNumber < argc; ArgumentNumber++) 
	{
		memset(tmpBuf, '\0', sizeof(tmpBuf));

		switch(ArgumentNumber + NotAtlas)
		{

			case STARTTIME:
			case STOPTIME:

				if ((_snprintf(tmpBuf, TIME_STAMP_SIZE + TERMINATOR, "%s", argv[ArgumentNumber])) != TIME_STAMP_SIZE)
				{
					dodebug(NO_PRE_DEFINE, "parseCommandLine()", "Failed snprintf function", (char*)NULL);
					fhdbInfo.returnValue = NAM_ERROR;
					return(NAM_ERROR);
				}
				
				if (setTimeValue(tmpBuf, ArgumentNumber + NotAtlas))
				{
					return(NAM_ERROR);
				}

				break;

			case ERONUMBER:
			case TPCCNUMBER:
			case UUTSERIALNO:
			case UUTREVNUMBER:
			case IDSERIALNUMBER:
			case FAILEDSTEP:
			case FAULTMESSAGE:
			case DIMENSION:
			case OPERCOMMENT:

				if (setCharString(argv[ArgumentNumber], ArgumentNumber)) 
				{
					return(NAM_ERROR);
				}

				break;

			case TESTSTATUS:

				if (!(_strnicmp(argv[ArgumentNumber], "Failed", strlen("Failed")))) 
				{
					dataCollectionfp.testStatus = (bool)FALSE;
				}
				else if (!(_strnicmp(argv[ArgumentNumber], "Passed", strlen("Passed")))) 
				{
					dataCollectionfp.testStatus = (bool)TRUE;
				}

				break;

			case UPPERLIMIT:
			case LOWERLIMIT:
			case MEASUREVALUE:

				tmpDouble = atof(argv[ArgumentNumber]);
				setValue(tmpDouble, ArgumentNumber);

				break;

			default:

				dodebug(UNKOPT, "parseCommandLine()", (char*)NULL, (char*)NULL);
				dodebug(NO_PRE_DEFINE, "parseCommandLine()", "i = %d", ArgumentNumber, (char*)NULL);
				fhdbInfo.returnValue = UNKOPT;
				return(NAM_ERROR);

				break;
		}
	}

	return(SUCCESS);
}
//int parseCommandLine(int argc, char *argv[])
//{
//
//	int				i = 0;
//	char			tmpBuf[5001];
//
//
//	for (i = 1; i < argc; i++) {
//
//		memset(tmpBuf, '\0', sizeof(tmpBuf));
//
//		if ((i > 0 && i < 8) || (i == 9 || i == 10 || i == 12 || i == 15)) {
//
//			if (i == (STARTTIME - NotAtlas) || i == (STOPTIME - NotAtlas)) {
//
//				if ((_snprintf(tmpBuf, 16, "%s", argv[i])) != 15) {
//					dodebug(0, "parseCommandLine()", "Failed snprintf function", (char*)NULL);
//					fhdbInfo.returnValue = NAM_ERROR;
//					return(NAM_ERROR);
//				}
//				
//				if (setTimeValue(tmpBuf, i + NotAtlas)) {
//					return(NAM_ERROR);
//				}
//
//			}
//			else {
//				if (setCharString(argv[i], i)) {
//					return(NAM_ERROR);
//				}
//			}
//		}
//		else if (i == 11 || (i == 13 || i == 14)) {
//
//			double	tmpDouble = 0.0;
//
//			tmpDouble = atof(argv[i]);
//
//			setValue(tmpDouble, i);
//		}
//		else if (i == 8) {
//
//			if (!(_strnicmp(argv[i], "Failed", strlen("Failed")))) {
//				dataCollectionfp.testStatus = (bool)FALSE;
//			}
//			else if (!(_strnicmp(argv[i], "Passed", strlen("Passed")))) {
//				dataCollectionfp.testStatus = (bool)TRUE;
//			}
//		}
//		else if (i == 16) {
//			;
//
//		}
//		else {
//			dodebug(UNKOPT, "parseArguments()", (char*)NULL, (char*)NULL);
//			dodebug(0, "parseArguments()", "i = %d", i, (char*)NULL);
//			fhdbInfo.returnValue = UNKOPT;
//			return(NAM_ERROR);
//		}
//	}
//
//	return(SUCCESS);
//}
