/////////////////////////////////////////////////////////////////////////////
//	File:	Error.cpp														*
//																			*
//	Creation Date:	2 March 2005											*
//																			*
//	Created By:		Richard Chaffin											*
//																			*
//	Revision Log:															*
//		1.0.0.0		Assigned it a version number.							*
// 		1.0.1.0		added the #include <string.h> statement.				*
// 					error_ms()												*
// 					modified the way the character array is being zero'd or	*
// 					initialized.											*
// 					dodebug()												*
// 					modified the way the character array is being zero'd or	*
// 					initialized.											*
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/ 
//																			*
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>
#include <errno.h>
#include <stdarg.h>
#include <string.h>
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"
#include "..\..\Common\Include\pipe.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

char	*ErrorMessage[] = {
	"",
	"An error occur'd while writing a file",				//FILE_WRITE_ERROR
	"An error occur'd while reading a file",				//FILE_READ_ERROR
	"The arguments passed are of incorrect number",			//ARGNUM
	"Couldn't open the requested File",						//FILE_OPEN_ERROR
	"Failed to start the FaultFileViewer process properly",	//FAULT_FILE_ERROR
	"Failed to get a handle to the named PIPE file",		//PIPE_HANDLE_ERROR
	"Failed to write the message to the pipe",				//WRITE_MSG_ERROR
	"Failed to read the message from the pipe",				//READ_MSG_ERROR
	"Failed to start the concurrent process properly",		//CONCURRENT_ERROR
};

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

//
//	ErrorMs:		This function will format and print an error message to a
//						file that passed to this function. It will first see if
//						that file is opened if not, open it, then write to it.
//
//	Parameters:
//		code:			Index into the predefined error messages, if code = 0,
//						then use the character string that is passed to it.
//		FunctionName:	This is the name of the function the error happend.
//		string:			This is used if the code = 0.
//
//	Returns:
//		SUCCESS:		  0		= No errors hence success.
//		NAM_ERROR:		(-1)	= An error happened, because the file couldn't
//								  be opened.
//
//

int ErrorMs(int code, char *FunctionName, char *string, int DoReturnStatus)
{

	char	TmpCharString[NAM_MAX_PATH];

	if (debugfp == NULL && DoReturnStatus == FALSE) {

		_snprintf(TmpCharString, NAM_MAX_PATH, "%s%s", LOGLOCATION, DEBUGFILENAME);

		if ((debugfp = fopen(TmpCharString, "wb")) == NULL) {
			return(NAM_ERROR);
		}
	}

	memset(TmpCharString, '\0', sizeof(TmpCharString));

	_snprintf(TmpCharString, MAX_MSG_SIZE, "%s in %s\r\n", code == 0 ? string :
			code < 700 ? strerror(code) : ErrorMessage[ code == 999 ?
			code - 954 : code < 800 ? code - 700 : code - 792 ], FunctionName);

	if (debugfp != NULL) {

		fprintf(debugfp, "%s", TmpCharString);
		fflush(NULL);
	}

	if (DoReturnStatus == TRUE) {
		sprintf(PipeInfo.WriteMsg, "%s %s", "ERROR", TmpCharString);
	}

	return(SUCCESS);
}

//
//	dodebug:		This function will check to see if the DEBUG variable is
//						set, if so then call error_ms. If error_ms returns
//						with an error then unset the DEBUG variable.
//
// Parameters:
//		code:			Index into the predefined error messages, if code = 0,
//						then use the character string that is passed to it.
//		function_name:	This is the name of the function the error happened.
//		format:			This is the format for the following variable list that
//						is sent to be printed;
//
//	Returns:
//		NONE:		This function is a void function.
//
//

void dodebug(int code, char *FunctionName, char *format, ...)
{

	char	TmpBuf[MAX_MSG_SIZE];
	va_list	arglist;

	memset(TmpBuf, '\0', sizeof(TmpBuf));

	va_start(arglist, format);
	vsprintf(TmpBuf, format, arglist);
	va_end(arglist);

	if (DE_BUG) {
		if (ErrorMs(code, FunctionName, TmpBuf, FALSE)) {
			DE_BUG = FALSE;
		}
	}

	ErrInfo.ErrorCode = code;
	sprintf(ErrInfo.FuncName, "%s", FunctionName);
	sprintf(ErrInfo.ErrorString, "%s", TmpBuf);

}

//
//	ReturnStatus:	This function will return th status of the etm_monitor program
//						to the etm program.
//
//	Parameters:
//		none:		This function will use preset variables in a structure.
//
//	Returns:
//		none:		If this function fails, it was doing a return status anyway.
//
//

void ReturnStatus()
{

	/*If EtmInfo.gp_errnoit is not SUCCESS then we have an error.  Send back as
	to the etm program.  If the etm program can receive a proper response it will 
	terminate us.*/
	if ((EtmInfo.EtmErrno != SUCCESS) && 
		(EtmInfo.EtmErrno != PROCESS_NOT_RUN)) {

		ErrorMs(ErrInfo.ErrorCode, ErrInfo.FuncName, ErrInfo.ErrorString, TRUE);
		WriteMessage();
	}

	else {

		switch(EtmInfo.EtmErrno) {

			case SUCCESS:

				sprintf(PipeInfo.WriteMsg, "%s", "Success");
				PipeInfo.WriteMsg[strlen("Success")] = '\0';

				break;

			case PROCESS_NOT_RUN:

				sprintf(PipeInfo.WriteMsg, "%s", "FAILED");
				PipeInfo.WriteMsg[strlen("FAILED")] = '\0';

				break;

			default:

				sprintf(PipeInfo.WriteMsg, "%s",
						"ERROR Something unknown happened\r\n");
				PipeInfo.WriteMsg[strlen(PipeInfo.WriteMsg)] = '\0';

			break;

		}

		WriteMessage();

	}
}
