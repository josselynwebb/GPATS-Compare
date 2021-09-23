/////////////////////////////////////////////////////////////////////////////
//	File:	error.cpp														/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of etmnam to implement Iads ver 3.2	/
//					software.												/
//		2.0.1.0		properly zero'd memory of tmpbuf using memset.			/
//		3.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//		3.0.1.0		dodebug()												/
//					Added fflush in the out putting of the debug message to	/
//					insure all information that needs to be sent to the		/
//					debug file before a crash is sent.						/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////	

#include <stdio.h>
#include <stdarg.h>
#include <errno.h>
#include <windows.h>
#include "etmnam.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Consants															/
/////////////////////////////////////////////////////////////////////////////

char	*ErrorMessage[] = {
	" ",
	"Improper Number of Arguments sent to ETM NAM",		//ARGNUM
	"Improper Data Type, should have been char string",	//DATACHAR
	"Improper Data Type, should have been an intenger",	//DATAINT
	"Unknown option sent",								//UNKOPT
	"Improper usage of X and Y values",					//XYUSAGE
	"Couldn't open the .TMP file",						//FILEOPEN
	"Improper Usage of the TARGET option",				//TARGOPT
	"Improper Usage of the RESUME option",				//CONCURRENTOPT
	"Improper Zoom option used",						//CONSUC
	"Failed to start the concurrent process properly",	//CONCURRENT_ERROR
	"The child process is running no way to kill it",	//CHILD_RUN_WILD
	"Failed to get a handle to the named PIPE file",	//PIPE_HANDLE_ERROR
	"Failed to write the message to the pipe",			//WRITE_MSG_ERROR
	"Failed to read the message from the pipe",			//READ_MSG_ERROR
	"Couldn't kill the process do it manual or REBOOT",	//PROCESS_WONT_DIE
	"The process to be terminated is not as requested",	//PROCESS_DONT_AGREE
};

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

//
// dodebug:		This function will format and print an error message to a
//					file that is pointed to by global debugfp.  If for some
//					reason that debugfp is NULL this function will try to
//					reopen the debug file and reset the debugfp to this file.
//					Else it will error out which should end all calls to this
//					function, hence no file info.
//
// Parameters:
//		code:			Index into the predefined error messages, if code = 0,
//						then use the character string that is passed to it.
//		function_name:	This is the name of the function where the error happened.
//		format:			This is the format for the following variable list that
//						is sent to be printed;
//
// Returns:
//		none:
//
//

void dodebug(int code, char *FunctionName, char *format, ...)
{

	va_list	arglist;

	if (DE_BUG) {
		if(debugfp == NULL) {

			char	TmpBuf[NAM_MAX_PATH];

			memset(TmpBuf, '\0', sizeof(TmpBuf));
			sprintf(TmpBuf, "%s%s", EtmInfo.LogLocation, DEBUGFILENAME);

			if ((debugfp = fopen(TmpBuf, "wb")) == NULL) {
				DE_BUG = FALSE;
			}
		}
	}

	if (DE_BUG) {

		if (code != 0) {
			fprintf(debugfp, "%s in %s\r\n", code < 800 ?
					strerror(code) : ErrorMessage[code - 800], FunctionName);
		}
		else {
			va_start(arglist, format);
			fprintf(debugfp, "In function %s ", FunctionName);
			vfprintf(debugfp, format, arglist);
			fprintf(debugfp, "\r\n");
			fflush(debugfp);
			va_end(arglist);
		}
	}
}
