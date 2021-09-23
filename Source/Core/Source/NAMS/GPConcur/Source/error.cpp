/////////////////////////////////////////////////////////////////////////////
//	File:	error.cpp														/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily				/
//					ErrorString()											/
//					Changed the name from error_ms to the present.			/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).	Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables.						/
//					DoDebug()												/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).  Modified the arguments to allow for/
//					the calling of t his function with a variable argument	/
//					list.													/
//					ReturnStatus()											/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <io.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <fcntl.h>
#include <errno.h>
#include <time.h>
#include <malloc.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "gpconcur.h"
#include "pipe.h"

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
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
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	ErrorString:	This function will format and print an error message to	/
//						a file that passed to this function. It will first	/
//						see if that file is opened if not, open it, then	/
//						write to it											/.
//																			/
//	Parameters:																/
//		Code:			Index into the predefined error messages, if		/
//						code = 0, then use the character string that is		/
//						passed to it.										/
//		FunctionName:	This is the name of the function the error happend.	/
//		String:			This is used if the code = 0.						/
//																			/
//	Returns:																/
//		SUCCESS:		  0		= No errors hence success.					/
//		M9_ERROR:		(-1)	= An error happened, because the file		/
//								  couldn't be opened.						/
//																			/
/////////////////////////////////////////////////////////////////////////////

int ErrorString(int Code, char *FunctionName, char *String, int DoReturnStatus)
{

	char	TmpCharString[1024];

	memset(TmpCharString, '\0', sizeof(TmpCharString));

	if (debugfp == NULL && DoReturnStatus == FALSE) {

		sprintf(TmpCharString, "%s%s", LOGLOCATION, DEBUGFILENAME);

		if ((debugfp = fopen(TmpCharString, "wb")) == NULL) {
			return(GP_ERROR);
		}
	}

	sprintf(TmpCharString, "%s in %s\r\n", Code == 0 ? String : Code < 700 ? strerror(Code) :
							ErrorMessage[ Code == 999 ? Code - 954 : Code < 800 ?
							Code - 700 : Code - 792 ], FunctionName);

	if (debugfp != NULL) {

		fprintf(debugfp, "%s", TmpCharString);
		fflush(debugfp);
	}

	if (DoReturnStatus == TRUE) {
		sprintf(PipeInfo.WriteMsg, "%s%s", ERROR, TmpCharString);
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
//	DoDebug:		This function will check to see if the DEBUG variable	/
//						is set, if so then call ErrorString. If error_ms	/
//						returns with an error then unset the DEBUG variable./
//																			/
//	Parameters:																/
//		Code:			Index into the predefined error messages.			/
//		FunctionName:	This is the name of the function the error happend.	/
//		format:			This is the format for the following variable list	/
//						that is sent to be printed.							/
//																			/
//	Returns:																/
//		NONE:		This function is a void function.						/
//																			/
/////////////////////////////////////////////////////////////////////////////

void DoDebug(int Code, char *FunctionName, char *format, ...)
{

	char	TmpBuf[MAX_MSG_SIZE];
	va_list	arglist;

	memset(TmpBuf, '\0', sizeof(TmpBuf));

	va_start(arglist, format);
	vsprintf(TmpBuf, format, arglist);
	va_end(arglist);

	if (DE_BUG) {
		if (ErrorString(Code, FunctionName, TmpBuf, FALSE)) {
			DE_BUG = FALSE;
		}
	}

	ErrInfo.ErrorCode = Code;
	sprintf(ErrInfo.FuncName, "%s", FunctionName);
	sprintf(ErrInfo.ErrorString, "%s", TmpBuf);

}

/////////////////////////////////////////////////////////////////////////////
//	ReturnStatus:	This function will return th status of the m9concur program
//						to the m910nam program.
//
//	Parameters:
//		none:		This function will use preset variables in a structure.
//
//	Returns:
//		none:		If this function fails, it was doing a return status anyway.
//																			/
/////////////////////////////////////////////////////////////////////////////

void ReturnStatus(void)
{

//
// First look at the GPConInfo.GPErrno to see if it is ! SUCCESS.  If it is not
// SUCCESS then something bad (ERROR) happen.  This we will send back as best as
// as we can to the gpnam program.  If the gpnam program can receive a proper
// response from us it will terminate us.
//

	GPConInfo.GPErrno = SUCCESS;

	if (GPConInfo.GPErrno != SUCCESS && GPConInfo.GPErrno != PROCESS_NOT_RUN) {

		ErrorString(ErrInfo.ErrorCode, ErrInfo.FuncName, ErrInfo.ErrorString, TRUE);
		WriteMessage();
	}

	else {

		switch(GPConInfo.GPErrno) {

			case SUCCESS:

				sprintf(PipeInfo.WriteMsg, "%s", "Success");
				PipeInfo.WriteMsg[strlen("Success")] = '\0';

				break;

			case PROCESS_NOT_RUN:

				sprintf(PipeInfo.WriteMsg, "%s", "FAILED");
				PipeInfo.WriteMsg[strlen("FAILED")] = '\0';

				break;

			default:

				sprintf(PipeInfo.WriteMsg, "%s", "ERROR Something unknown happened\r\n");
				PipeInfo.WriteMsg[strlen("ERROR Something unknown happened\r\n")] = '\0';

			break;

		}

		WriteMessage();

	}
}