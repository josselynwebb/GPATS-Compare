/////////////////////////////////////////////////////////////////////////////
//	File:	Error.cpp														*
//																			*
//	Creation Date:	2 March 2005											*
//																			*
//	Created By:		Richard Chaffin											*
//																			*
//	Revision Log:															*
//		1.0.0.0		Assigned it a version number.							*
//		2.0.0.0		Combined Source from Astronics with	/
//					source from VIPERT 1.3.1.0.  							/
//		2.1.0.0		Added hnges from VIPERT 1.3.2.0							/
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
#include "CoyoteNam.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

char	*ErrorMessage[] = {
	"",
	"Improper Number of Arguments sent to NAM",			//ARGNUM
	"Improper Data Type, should have been char string",	//DATACHAR
	"Improper Data Type, should have been an intenger",	//DATAINT
	"Improper Data Type, should have been a decimal",	//DATADEC
	"Improper Data Type, should have been a boolean",	//DATABOOL
	"Unknown option sent",								//UNKOPT
	"Couldn't open the requested file",					//FILEOPEN
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

			sprintf(tmpbuf, "%s%s", CoyoteInfo.LogLocation, DEBUGFILENAME);

			if ((debugfp = fopen(tmpbuf, "wb")) == NULL) {
				DE_BUG = FALSE;
			}
		}
	}

	if (DE_BUG) {

		if (code != 0) {
			fprintf(debugfp, "%s in %s\r\n", (code < 800 || code > 807) ?
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

	fflush (debugfp);
}
