/////////////////////////////////////////////////////////////////////////////
//	File:	gpconcur.cpp													/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily				/
//					WinMain()												/
//					Changed the way the variables are written				/
//					(camel back style). Changed the name of some of the		/
//					variables to correctly reflect what they are(nargs -	/
//					NumberOfArgs; tmp_buf - StructTmpBuf). Added a for loop	/
//					tp free allocated memory that wasn't being freed.		/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <sys/types.h>
#include <sys/stat.h>
#include <wchar.h>
#include <windows.h>
#include <shellapi.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <io.h>
#include <wchar.h>
#include <winnls.h>
#include "stdafx.h"
#include "gpconcur.h"

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

int				DE_BUG;
FILE			*debugfp;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	WinMain:	This is the main entry point for this program. This			/
//					function has been incorperated into an already existing	/
//					program so that a console window will not appear.		/
//																			/
//	Parameters:																/
//		hInstance:		A Microsoft object handle for this program NOT USED./
//		hPrevInstance:	A Microsoft object handle NOT USED.					/
//		lpCmdLine:		Pointer to a null-terminated string specifying the	/
//							command line for the application, excluding the	/
//							program name.									/
//		nCmdShow:		Specifies how the window is to be shown.			/
//																			/
//	Returns:																/
//		SUCCESS:		0    - No errors, just like it is suppose to do.	/
//		M9_ERROR:		(-1) - Whoops had an error.							/
//																			/
/////////////////////////////////////////////////////////////////////////////
#pragma warning(disable : 4100)

int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{

	int				NumberOfArgs, i, ConvertedChars, HadError;
	WCHAR			**CmdLineList;
	char			**argv;
	char			ConvertedCmdLineArg[GP_MAX_PATH];
	struct _stat	StructTmpBuf;
	
	//MessageBox(NULL, "GP Concur", "Debug", MB_OK);
//
// Setup some defaults and initilize some variables to their defaults.
//

	GPConInfo.GPErrno = SUCCESS;
	sprintf(GPConInfo.LogLocation, "%s", LOGLOCATION);
	sprintf(GPConInfo.LogFile, "%s", FAULT_FILE);
	sprintf(GPConInfo.DebugOption, "%s%s", GPConInfo.LogLocation, DEBUGIT);
	ConvertedChars = 0;
	HadError = 0;

	::CoInitialize(NULL);

//
// Find out if the program should be run in the DEBUG mode. If it is then open
// the Debug file, if that doesn't work then set DEBUG back to 0.  DEBUGSOURCE
// doesn't have to be set just used, that is all.
//

	DE_BUG = _stat(GPConInfo.DebugOption, &StructTmpBuf) == GP_ERROR ? 0 : 1;

	if (DE_BUG) {

		char	debugfile[256];

		sprintf(debugfile, "%s%s", GPConInfo.LogLocation, DEBUGFILENAME);

		if ((debugfp = fopen(debugfile, "w+b")) == NULL) {
			DE_BUG = FALSE;
		}
	}

//
// Here we will use the microsoft silly function to get the command line arguments,
// all the command line arguments.  But knowing microslop they add extra work by
// making this list into wide characters. Thanks Billy G.
//

	if ((CmdLineList = CommandLineToArgvW(GetCommandLineW(), &NumberOfArgs)) == NULL) {
		DoDebug(0, "WinMain()", "%s", "Failed to get the command line argments", (char*)NULL);
		GPConInfo.GPErrno = PROCESS_NOT_RUN;
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		return(GPConInfo.GPErrno);
	}

//
// If we make it this far then we know the number of characters strings that will
// be in the argv list that we are going to build.  So now we will malloc the space
// for them.
//

	if ((argv = (char **)malloc(sizeof(char *) * NumberOfArgs)) == NULL) {
		DoDebug(0, "WinMain()", "%s", "Malloc failed to allocate the required memory", (char*)NULL);
		GPConInfo.GPErrno = PROCESS_NOT_RUN;
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		if (CmdLineList != NULL) {
			GlobalFree(CmdLineList);
		}
		return(GPConInfo.GPErrno);
	}

//
// Now we will loop through the number of arguments and convert them from gates
// wide characters to ansi characters.  Then use the _strdup function to allocate
// the required string length for each argument and put them into *argv[] so I can
// use the already waiting program without any changes.
//

	for (i = 0; i < NumberOfArgs; i++) {

		if ((ConvertedChars = WideCharToMultiByte(CP_UTF8, 0, CmdLineList[i], -1,
												   ConvertedCmdLineArg,
												   sizeof(ConvertedCmdLineArg),
												   NULL, NULL)) == 0) {
			DoDebug(0, "WinMain()", "%s", "Failed to convert from wide characters to ansi", (char*)NULL);
			GPConInfo.GPErrno = PROCESS_NOT_RUN;
			HadError++;
			break;
		}

		if ((argv[i] = _strdup(ConvertedCmdLineArg)) == NULL) {

			int	j;

			for (j = 0; j < i; j++) {
				free(argv[j]);
			}

			DoDebug(0, "WinMain()", "%s", "strdup failed to allocate the required memory", (char*)NULL);
			HadError++;
			GPConInfo.GPErrno = PROCESS_NOT_RUN;
			break;
		}

	}

	if (ConcurMain(NumberOfArgs, argv)) {
		HadError++;
	}

	for (i = 0; i < NumberOfArgs; i++) {

		if (argv[i] != NULL) {
			free(argv[i]);
		}
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

	return(!HadError ? SUCCESS : GP_ERROR);
}

#pragma warning(default : 4100)
