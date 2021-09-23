/////////////////////////////////////////////////////////////////////////////
//	File:	CoyoteNamMain.cpp												/
//																			/
//	Creation Date:	12 February 2016										/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0	Assigned it a version number.								/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//		2.1.0.0		Added hnges from VIPERT 1.3.2.0							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <windows.h>
#include <nam.h>
#include "Constants.h"
#include "CoyoteNam.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

COYOTE_INFO		CoyoteInfo;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// CoyoteNamMain:	This function will check the variables that were passed
//					to it and if correct will set up the defaults and then
//					call the different functions in proper order.
//
// Parameters:
//		argc:		The # of arguments passed to concur_main.
//		argv:		Character list of the arguments passed to concur_main.
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int CoyoteNamMain (int argc, char *argv[])
{

	int				IsAtlas;
	char			TempFileName[NAM_MAX_PATH];
	struct _stat	StatBuf;
	//MessageBox(NULL, "Coyote Nam Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	sprintf(TempFileName, "%s%s", argv[TMP_FILE], SUFFIX);
	IsAtlas = _stat(TempFileName, &StatBuf) == NAM_ERROR ? 0 : 1;

	if (IsAtlas) {
		if (DoInitalSetup(argc, argv)) {
			CoyoteInfo.CoyoteErrno = NAM_ERROR;
			return (NAM_ERROR);
		}
	}

	if (CoyoteInfo.CoyoteErrno != NAM_ERROR) {
		if (InitializeVeo2ForNam()) {
			CoyoteInfo.CoyoteErrno = NAM_ERROR;
		}
	}

	if (CoyoteInfo.CoyoteErrno != NAM_ERROR) {
		if (FillInVariables(argc, argv, IsAtlas)) {
			CoyoteInfo.CoyoteErrno = NAM_ERROR;
		}
	}

	if (CoyoteInfo.CoyoteErrno != NAM_ERROR) {
		if (StartTheCamera()) {
			CoyoteInfo.CoyoteErrno = NAM_ERROR;
		}
	}

	if (CoyoteInfo.CoyoteErrno != NAM_ERROR) {
		if (StartTheExecutable()) {
			CoyoteInfo.CoyoteErrno = NAM_ERROR;
		}
	}

	Sleep (1000);

	if (CoyoteInfo.CoyoteErrno != NAM_ERROR) {
		if (SendKeyStrokes()) {
			CoyoteInfo.CoyoteErrno = NAM_ERROR;
		}
	}

	if (CoyoteInfo.CoyoteErrno != NAM_ERROR) {
	    WaitForSingleObject(CoyoteInfo.CoyoteHd, INFINITE);
	}

	if (CoyoteInfo.CoyoteErrno != NAM_ERROR) {
		if (InitializeVeo2ForNam()) {
			CoyoteInfo.CoyoteErrno = NAM_ERROR;
		}
	}

	if (CoyoteInfo.CoyoteErrno != NAM_ERROR) {
		if (StopTheCamera()) {
			CoyoteInfo.CoyoteErrno = NAM_ERROR;
		}
	}

	DoCloseout(IsAtlas, argv);

	return CoyoteInfo.CoyoteErrno;
}

//
// DoInitalSetup:	This function will make connection to the ATLAS vm,
//					check to see if the correct number of variables were
//					passed and then call the function to check those variables.
//
// Parameters:
//		argc:		The argument passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//

int DoInitalSetup(int argc, char *argv[])
{

	if (vmOpen (argv[TMP_FILE]) < 0) {
		dodebug (0, "DoInitalSetup()", "Cannot open virtual memory file %s", argv[1], (char*)NULL);
		return NAM_ERROR;
	}

	if (argc >= MIM_NUMBER) {
		CheckTheArguments(argc, argv);
	}
	else {
		dodebug(ARGNUM, "DoInitalSetup()", (char*)NULL, (char*)NULL);
		return NAM_ERROR;
	}

	return SUCCESS;
}

//
// CheckTheArguments:	This function will call the correct function
//						to check if the passed variable is of the 
//						correct type.
//
// Parameters:
//		argc:		Number of character list in argv.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//

int CheckTheArguments(int argc, char *argv[])
{

	int	CompVal = 0;

	if (argc > MIM_NUMBER) {
		if (CheckCharValues(XML_FILE_NAME, argv)) {
			return (NAM_ERROR);
		}
		CompVal = 1;
	}
	if (CheckIntValues(RETURN_STATUS + CompVal, argv)) {
		return (NAM_ERROR);
	}

	if (CheckCharValues(RETURN_STRING + CompVal, argv)) {
		return (NAM_ERROR);
	}

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

	if (vmGetDataType(TmpLongValue) != ITYPE) {
		dodebug(DATAINT, "CheckIntValues()", (char *)NULL, (char *)NULL);
		CoyoteInfo.CoyoteErrno = DATAINT;
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

	if (vmGetDataType(TmpLongValue) != TTYPE) {
		dodebug(DATACHAR, "CheckCharValues()", (char *)NULL, (char *)NULL);
		CoyoteInfo.CoyoteErrno = DATACHAR;
		return(NAM_ERROR);
	}

	return SUCCESS;
}

//
//
// GetCharValues:	This function will first get the address of the
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

int GetCharValue(int ArgcValue, char *argv[], int StringSize)
{

	long	ArgumentAddress;
	char	*TmpBuf;

	ArgumentAddress = atol(argv[ArgcValue]);

	if ((TmpBuf = (char *)calloc((size_t)StringSize, sizeof(char))) != NULL) {

		vmGetText(ArgumentAddress, TmpBuf, StringSize);
		sprintf(CoyoteInfo.FileLocation, "%s", TmpBuf);

		free(TmpBuf);
	}
			
	else {
		
		dodebug(0, "GetCharValues()", "Failed to allocate memory", (char*)NULL);
		CoyoteInfo.CoyoteErrno = DATACHAR;
		return(NAM_ERROR);
	}

	return SUCCESS;
}

//
//
// FillInVariables:	This function will call the correct function to
//					fill in the correct variable..
//
// Parameters:
//		argc:			The number argument passed to main.
//		argv:			Character list of the arguments passed to main.
//		IsAtlas:		Used to tell how the program was executed.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//

int FillInVariables(int argc, char *argv[], int IsAtlas)
{

	int	CompVal = -1;

	if (IsAtlas) {
		CompVal = 0;
	}

	if ((IsAtlas && argc > MIM_NUMBER) || (!IsAtlas && argc > STRING_VAL)) {
		if (GetCharValue(XML_FILE_NAME + CompVal, argv, NAM_MAX_PATH)) {
			return(NAM_ERROR);
		}
	}
	else {
		dodebug(0, "FillInVariables()", "Failed to allocate memory IsAtlas %s", XML_FILE, (char*)NULL);
		sprintf(CoyoteInfo.FileLocation, "%s", XML_FILE);
	}

//	if (ConvertCharacter()) {
//		return(NAM_ERROR);
//	}

	return SUCCESS;
}

//
//
// ConvertCharacter:	This function will convert \ to / and will
//						eliminate multiple \\.
//
// Parameters:
//		none:			None passed is void.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//

int ConvertCharacter(void)
{

	int		i, j;
	char	*TmpBuf;

	if ((TmpBuf = (char *)calloc((size_t)strlen(CoyoteInfo.FileLocation), sizeof(char))) != NULL) {

		for (i = 0, j = 0; CoyoteInfo.FileLocation[i] != '\0'; i++, j++) {

			if (CoyoteInfo.FileLocation[i] == BACK_SLASH) {
				TmpBuf[j] = '/';
				i++; j++;

				for (; CoyoteInfo.FileLocation[j] != '\0'; i++) {
					
					if (CoyoteInfo.FileLocation[i] != BACK_SLASH) {
						break;
					}
				}
			}
			else {
				TmpBuf[j] = CoyoteInfo.FileLocation[i];
			}
		}

		memset(CoyoteInfo.FileLocation, '\0', sizeof(CoyoteInfo.FileLocation));
		sprintf(CoyoteInfo.FileLocation, "%s", TmpBuf);

		free(TmpBuf);
	}
	else {
		
		dodebug(0, "ConvertCharacter()", "Failed to allocate memory", (char*)NULL);
		CoyoteInfo.CoyoteErrno = DATACHAR;
		return(NAM_ERROR);
	}

	return SUCCESS;
}

//
// DoCloseout:	This function will close all open handles and
//				set the returns to go back to ATLAS.
//
// Parameters:
//		none:
//
// Returns:
//		none:	void function.;
//
//

void DoCloseout(int IsAtlas, char *argv[])
{

	long	RtnAddress;

	if (IsAtlas) {
		RtnAddress = atol(argv[RETURN_STATUS]);
		vmSetInteger(RtnAddress, CoyoteInfo.CoyoteErrno);

		RtnAddress = atol(argv[RETURN_STRING]);
		vmSetText(RtnAddress,
				  CoyoteInfo.CoyoteErrno == SUCCESS    ? SUCCESSFUL  :
				  CoyoteInfo.CoyoteErrno == INVALIDEXE ? INVALID_EXE :
				  CoyoteInfo.CoyoteErrno == POWERBAD   ? POWER_BAD   :
				  CoyoteInfo.CoyoteErrno == STAGEBAD   ? STAGE_BAD   :
				  UNKNOWN);

		vmClose();
	}

	if (CoyoteInfo.Veo2Hd != NULL) {
		FreeLibrary(CoyoteInfo.Veo2Hd);
		CoyoteInfo.Veo2Hd = NULL;
	}

	if (CoyoteInfo.CoyoteHd != NULL) {
	    CloseHandle(CoyoteInfo.CoyoteHd);
		CloseHandle(CoyoteInfo.ThreadHd);
		CoyoteInfo.CoyoteHd = NULL;
		CoyoteInfo.ThreadHd = NULL;
	}

	return;
}
