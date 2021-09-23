/////////////////////////////////////////////////////////////////////////////
//	File:	Error.cpp														*
//																			*
//	Creation Date:	2 March 2005											*
//																			*
//	Created By:		Richard Chaffin											*
//																			*
//	Revision Log:															*
//		1.0.0.0		Assigned it a version number.							*
//																			*
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////
#include <stdio.h>
#include <stdarg.h>
#include <errno.h>
#include <windows.h>
#include "Constants.h"
#include "ProgramLauncher.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////
#define	ARGNUM				801
#define	DATACHAR			802
#define	DATAINT				803
#define	UNKOPT				804
#define	FILE_OPEN_ERROR		805
#define	PROCESS_ERROR		806

char	*ErrorMessage[] = {
	"",
	"Improper Number of Arguments sent to ETM NAM",		//ARGNUM
	"Improper Data Type, should have been char string",	//DATACHAR
	"Improper Data Type, should have been an intenger",	//DATAINT
	"Unknown option sent",								//UNKOPT
	"Couldn't open the requested file",					//FILEOPEN
	"Failed to start the requested process properly",	//PROCESS_ERROR   Look at the below and pt them in correctly
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

void dodebug(int code, char *functionName, char *format, ...)
{

	va_list	arglist;

	if (DE_BUG) {
		if(debugfp == NULL) {

			char	tmpbuf[NAM_MAX_PATH];

			sprintf(tmpbuf, "%s%s", ProgInfo.LogLocation, DEBUGFILENAME);

			if ((debugfp = fopen(tmpbuf, "wb")) == NULL) {
				DE_BUG = FALSE;
			}
		}
	}

	if (DE_BUG) {

		if (code != 0) {
			fprintf(debugfp, "%s in %s\r\n", code < 800 ?
					strerror(code) : ErrorMessage[code - 800], functionName);
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
