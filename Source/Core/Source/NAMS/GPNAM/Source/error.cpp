/////////////////////////////////////////////////////////////////////////////
//	File:	error.cpp														/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		0.31	Assigned it a version number.								/
//		1.0.0.0	Added a new predefined error message, and removed the last	/
//				3 error messages.  They didn't belong to this program. Also	/
//				added the setting of the tmpbuf in the function dodebug to	/
//				have nothing in it, due to multiple use.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <io.h>
#include <stdlib.h>
#include <stdio.h>
#include <stdarg.h>
#include <string.h>
#include <fcntl.h>
#include <errno.h>
#include <time.h>
#include <sys/types.h>
#include <ctype.h>
#include "gpnam.h"
#pragma warning (disable : 4035 4068)
#include <windows.h>
#pragma warning (default : 4035 4068)

/////////////////////////////////////////////////////////////////////////////
//	Local Consants
/////////////////////////////////////////////////////////////////////////////

char	*error_message[] = {
		"Improper Number of Arguments sent to GPNAM",					//ARGNUM
		"Improper Option sent, value needs to be between 1 & 9",		//OPTION_SENT
		"Improper Data Type sent, should have been an intenger",		//INT_TYPE
		"An error occur'd while reading a file",						//FILE_READ_ERROR
		"Unable to position to requested position in file",				//FILE_POSITION_ERROR
		"An erroor occur'd while writing to a file",					//FILE_WRITE_ERRO
		"Couldn't open the requested File",								//FILE_OPEN_ERROR
		"Failed to start the FaultFileViewer process properly",			//FAULT_FILE_ERROR
		" ",
		"The M910 is still running a concurrent program, Improper USE",	//CONCURRENT_RUNNING
		"Couldn't kill the process either do it manual or REBOOT",		//CONCUR_WONT_DIE
		"The process to be terminated is not the same as requested",	//PROCESS_DONT_AGREE
		"Failed to get a handle to the named PIPE file",				//PIPE_HANDLE_ERROR
		"The concurrent was improperly killed, not with quit",			//IMPROPER_SHUTDOWN
		"Failed to write the message to the pipe",						//WRITE_MSG_ERROR
		"Failed to read the message from the pipe",						//READ_MSG_ERROR
		"Failed to start the concurrent process properly",				//CONCURRENT_ERROR
		"The child process is running with no way to kill it"			//CHILD_RUN_WILD
};

/////////////////////////////////////////////////////////////////////////////
//	Modules
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// dodebug:	This function will format and print an error message to a		/
//					file that is pointed to by global debugfp.  If for some	/
//					reason that debugfp is NULL this function will try to	/
//					reopen the debug file and reset the debugfp to this.	/
//					file. Else it will error out which should end all calls	/
//					to this function, hence no file info.					/
//																			/
// Parameters:																/
//		code:			Index into the predefined error messages,			/
//						if code = 0, then use the character string that is	/
//						passed to it.										/
//		function_name:	This is the name of the function the error			/
//						happened in.										/
//		format:			This is the format for the following variable list	/
//						that is sent to be printed;							/
//																			/
// Returns:																	/
//		none:			Void function										/
//																			/
/////////////////////////////////////////////////////////////////////////////

void	dodebug(int code, char *function_name, char *format, ...)
{

	va_list	arglist;

	if (DE_BUG) {
		if(debugfp == NULL) {

			char	tmpbuf[GP_MAX_PATH];

			tmpbuf[0] = '\0';
			sprintf(tmpbuf, "%s%s", gp_info.log_location, DEBUGFILENAME);

			if ((debugfp = fopen(tmpbuf, "wb")) == NULL) {
				DE_BUG = FALSE;
			}
		}
	}

	if (DE_BUG) {

		if (code != 0) {
			fprintf(debugfp, "%s in %s\r\n", code < 800 ?
					strerror(code) : error_message[code - 800], function_name);
		}
		else {
			va_start(arglist, format);
			fprintf(debugfp, "In function %s ", function_name);
			vfprintf(debugfp, format, arglist);
			fprintf(debugfp, "\r\n");
			va_end(arglist);
		}
		fflush(debugfp);
	}

	return;
}

void retrieveErrorMessage(char *functionName, char *message)
{

	LPTSTR	formattedMessageBuf;
	DWORD	errorValue;

	errorValue = GetLastError();

	dodebug(0, "retrieveErrorMessage()", "error = %d", errorValue);

	if (FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM |
					  FORMAT_MESSAGE_IGNORE_INSERTS, NULL, errorValue, 0,
					  (LPTSTR) &formattedMessageBuf, 0, NULL) == 0) {

		dodebug(0, functionName, "Unknown Error: %d happened", errorValue);
	}

	else {

		dodebug(0, functionName, "%s %d - %s", message, errorValue, formattedMessageBuf);
	}

	if (formattedMessageBuf) {
		LocalFree(formattedMessageBuf);
	}

	return;
}