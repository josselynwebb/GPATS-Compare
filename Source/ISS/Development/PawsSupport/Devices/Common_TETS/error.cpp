// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2020-07-06 16:01:5#$: Date of last commit
//    $Rev:: 27851              $: Revision of last commit

#pragma warning(disable : 4115)
#include "cem.h"
#pragma warning(default : 4115)
#include <stdio.h>
#include "Common.h"

static char ebuf[256];

/***************************************************************
 *	Local Consants
 **************************************************************/

char	*error_message[] = {
		"Improper Number of Arguments sent to GPNAM",					/*ARGNUM*/
		"Improper Option sent, value needs to be between 1 & 3",		/*OPTION_SENT*/
		"Improper Data Type sent, should have been an intenger",		/*INT_TYPE*/
		"An error occur'd while reading a file",						/*FILE_READ_ERROR*/
		"Unable to position to requested position in file",				/*FILE_POSITION_ERROR*/
		"An erroor occur'd while writing to a file",					/*FILE_WRITE_ERRO*/
		"Couldn't open the requested File",								/*FILE_OPEN_ERROR*/
		"Failed to start the FaultFileViewer process properly",			/*FAULT_FILE_ERROR*/
		" ",
		"The M910 is still running a concurrent program, Improper USE",	/*CONCURRENT_RUNNING*/
		"Couldn't kill the process either do it manual or REBOOT",		/*CONCUR_WONT_DIE*/
		"The process to be terminated is not the same as requested",	/*PROCESS_DONT_AGREE*/
		"Failed to get a handle to the named PIPE file",				/*PIPE_HANDLE_ERROR*/
		"The concurrent was improperly killed, not with quit",			/*IMPROPER_SHUTDOWN*/
		"Failed to write the message to the pipe",						/*WRITE_MSG_ERROR*/
		"Failed to read the message from the pipe",						/*READ_MSG_ERROR*/
		"Failed to start the concurrent process properly",				/*CONCURRENT_ERROR*/
		"The child process is running with no way to kill it"			/*CHILD_RUN_WILD*/
};

int TypeErr (const char *pModName)
{
	sprintf_s(ebuf, sizeof(ebuf), "Type Error For Modifier: %s\n", pModName);
	ErrMsg (7, ebuf);
	return 0;
}

int BusErr (const char *pDevName)
{
	sprintf_s(ebuf, sizeof(ebuf), "Bus Error For Device: %s\n", pDevName);
	ErrMsg (7, ebuf);
	return 0;
}

/*
 * dodebug:	This function will format and print an error message to a
 *					file that is pointed to by global debugfp.  If for some
 *					reason that debugfp is NULL this function will try to
 *					reopen the debug file and reset the debugfp to this file.
 *					Else it will error out which should end all calls to this
 *					function, hence no file info.
 *
 * Parameters:
 *		code:			Index into the predefined error messages, if code = 0,
 *						then use the character string that is passed to it.
 *		function_name:	This is the name of the function the error happened.
 *		format:			This is the format for the following variable list that
 *						is sent to be printed;
 *
 * Returns:
 *		none:			Void function
 *
 */

void	dodebug(int code, char *functionName, char *format, ...)
{

	va_list	arglist;

	if (DE_BUG) {
		if(debugfp == NULL) {

			char	tmpbuf[CEM_MAX_PATH];

			tmpbuf[0] = '\0';
			sprintf_s(tmpbuf, CEM_MAX_PATH, "%s%s", cemInfo.logLocation, DEBUGFILENAME);

			if (( fopen_s( &debugfp, tmpbuf, "wb")) != 0) {
				DE_BUG = FALSE;
			}
		}
	}

	if (DE_BUG) {

		if (code != 0) {
			char tmpmsg[256];
			tmpmsg[0] = 0;
			if( code < 800 )
				strerror_s(tmpmsg, sizeof(tmpmsg), code) ;
			else
				error_message[code - 800],
			fprintf(debugfp, "%s in %s\r\n", tmpmsg, functionName);
		}
		else {
			va_start(arglist, format);
			fprintf(debugfp, "In function %s ", functionName);
			vfprintf(debugfp, format, arglist);
			fprintf(debugfp, "\r\n");
			va_end(arglist);
		}
	}
}

void retrieveErrorMessage(char *functionName, char *message)
{

	LPTSTR	formattedMessageBuf;
	char	*formattedErrorMessage;
	DWORD	errorValue;

	errorValue = GetLastError();

	dodebug(0, "retrieveErrorMessage()", "error = %d", errorValue);

	if (FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM |
					  FORMAT_MESSAGE_IGNORE_INSERTS, NULL, errorValue, 0,
					  (LPTSTR) &formattedMessageBuf, 0, NULL) == 0) {

		dodebug(0, functionName, "Unknown Error: %d happened", errorValue);
	}

	else {

		formattedErrorMessage = formattedMessageBuf;
		dodebug(0, functionName, "%s %d - %s", message, errorValue, formattedMessageBuf);
	}

	if (formattedMessageBuf) {
		LocalFree(formattedMessageBuf);
	}
}
