/////////////////////////////////////////////////////////////////////////////
//	File:	ProgramLauncher.cpp												/
//																			/
//	Creation Date:	15 March 2013											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////
#include <sys/types.h>
#include <sys/stat.h>
#include <wchar.h>
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <string.h>
#include "stdafx.h"
#include "constants.h"
#include "ProgramLauncher.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

int				DE_BUG;
FILE			*debugfp;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
//	WinMain:	This is the main entry point for this program.  This function
//					has been incorperated into an already existing program so
//					that a console window will not appear.
//
//	Parameters:
//		hInstance:		A Microsoft object handle for this program NOT USED.
//		hPrevInstance:	A Microsoft object handle NOT USED.
//		lpCmdLine:		Pointer to a null-terminated string specifying the
//							command line for the application, excluding the
//							program name.
//		nCmdShow:		Specifies how the window is to be shown.
//
//	Returns:
//		SUCCESS:		  0  - No errors, just like it is suppose to do.
//		NAM_ERROR:		(-1) - Whoops had an error.
//
//

#pragma warning(disable : 4100)

int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{

	int				nargs, i, ConvertedChars = 0, HadError = 0;
	WCHAR			**CmdLineList;
	char			**argv;
	char			ConvertedCmdLineArg[NAM_MAX_PATH];
	struct _stat	TmpBuf;

//
// Setup some defaults and initilize some variables to their defaults.
//

	InitVariables();

//
// Find out if the program should be run in the DEBUG mode. If it is then open
// the Debug file, if that doesn't work then set DEBUG back to 0.  DEBUGSOURCE
// doesn't have to be set just used, that is all.
//

	DE_BUG = _stat(ProgInfo.DebugOption, &TmpBuf) == NAM_ERROR ? 0 : 1;

	if (DE_BUG) {

		char	debugfile[256];

		sprintf(debugfile, "%s%s", ProgInfo.LogLocation, DEBUGFILENAME);

		if ((debugfp = fopen(debugfile, "w+b")) == NULL) {
			DE_BUG = FALSE;
		}
	}

//
// Here we will use the microsoft silly function to get the command line
// arguments, all the command line arguments.  But knowing microslop they add
// extra work by making this list into wide characters. Thanks Billy G.
//

	if ((CmdLineList = CommandLineToArgvW(GetCommandLineW(),&nargs)) == NULL) {
		dodebug(0, "WinMain()", "Failed to get the command line argments", (char*)NULL);
		ProgInfo.ProgErrno = PROCESS_NOT_RUN;
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		return(ProgInfo.ProgErrno);
	}

//
// If we make it this far then we know the number of characters strings that
// will be in the argv list that we are going to build.  So now we will malloc
// the space for them.
//

	if ((argv = (char **)malloc(sizeof(char*) * nargs)) == NULL) {
		dodebug(0, "WinMain()", "Malloc failed to allocate the required memory", (char*)NULL);
		ProgInfo.ProgErrno = PROCESS_NOT_RUN;
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		if (CmdLineList != NULL) {
			GlobalFree(CmdLineList);
		}
		return(ProgInfo.ProgErrno);
	}

//
// Now we will loop through the number of arguments and convert them from gates
// wide characters to ansi characters.  Then use the _strdup function to
// allocate the required string length for each argument and put them into
// *argv[] so I can use the already waiting program without any changes.
//

	for (i = 0; i < nargs; i++) {

		if ((ConvertedChars = WideCharToMultiByte(CP_UTF8, 0, CmdLineList[i],
												 -1, ConvertedCmdLineArg,
												 sizeof(ConvertedCmdLineArg),
												 NULL, NULL)) == 0) {
			dodebug(0, "WinMain()", "Failed to convert from wide characters to ansi", (char*)NULL);
			ProgInfo.ProgErrno = PROCESS_NOT_RUN;
			HadError++;
			break;
		}

		if ((argv[i] = _strdup(ConvertedCmdLineArg)) == NULL) {

			int	j;

			for (j = 0; j < i; j++) {
				free(argv[j]);
			}

			dodebug(0, "WinMain()", "strdup failed to allocate the required memory", (char*)NULL);
			HadError++;
			ProgInfo.ProgErrno = PROCESS_NOT_RUN;
			break;
		}

	}

	if (ProgramLauncherMain(nargs, argv)) {
		HadError++;
	}

	if (argv != NULL) {
		free(argv);
	}

	if (CmdLineList != NULL) {
		GlobalFree(CmdLineList);
	}

	if (DE_BUG) {
		fclose(debugfp);
		DE_BUG = 0;
	}

	return(!HadError ? SUCCESS : NAM_ERROR);
}

#pragma warning(default : 4100)

//
//	InitVariables:		This function just does the initilaization of required
//						variables that need to be equated to something legal.
//
//	Parameters:
//		none:
//
//	Returns:
//		none:
//
//

void InitVariables(void)
{

	ZeroMemory(&ProgInfo, sizeof(PROG_INFO));
	ZeroMemory(&ProcInfo, sizeof(PROC_INFO));

	ProgInfo.ProgErrno = SUCCESS;

	ProcInfo.ProgramRunning = FALSE;

	sprintf(ProgInfo.LogLocation, "%s", LOGLOCATION);
	sprintf(ProgInfo.DebugOption, "%s%s", ProgInfo.LogLocation, DEBUGIT);
}
