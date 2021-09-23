/////////////////////////////////////////////////////////////////////////////
//	File:	etmnam.cpp														/
//																			/
//	Creation Date:	10 Mar 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of etmnam to implement Iads ver 3.2.7	/
//					software.												/
//		2.0.1.0		etmnam.cpp												/
// 					WinMain()												/
// 					added the pragma capabilities to stop the warnings that	/
//					have been researched and found not to effect anything.	/
//					properly initilized character arrays with memset.		/
// 					init_variables()										/
//					added the send_info to init_variables and insured that	/
//					the structure is properly initilized with ZeroMemory.	/
//		3.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//		3.0.2.0		process_utils.cpp										/
//					StartProc()												/
//					modified what is sprintf into the commandString, removed/
//					the PROG_BIN_DIR, from the commandString, this is set in/
//					the path environmental variable path.					/
//					error.ccp												/
//					dodebug()												/
//					Added fflush in the out putting of the debug message to	/
//					insure all information that needs to be sent to the		/
//					debug file before a crash is sent.						/
//					etmnam.h												/
//					removed the #define PROG_BIN_DIR "c:/usr/tyx/bin", this	/
//					is set using the environmental variable path.			/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////	

#include <sys/types.h>
#include <sys/stat.h>
#include <windows.h>
#include <stdio.h>
#include <wchar.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
#include "etmnam.h"
#include "Constants.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

int				DE_BUG;
FILE			*debugfp;
ETM_INFO		EtmInfo;
WIN_INFO		WinInfo;
PIPE_INFO		PipeInfo;

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
//						command line for the application, excluding the
//						program name.
//		nCmdShow:		Specifies how the window is to be shown NOT USED.
//
//	Returns:
//		SUCCESS:		  0  - No error.
//		NAM_ERROR:		(-1) - Whoops had an error, So Sorry.
//
//

#pragma warning(disable : 4100)

int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
					 LPSTR lpCmdLine, int nCmdShow)
{

	int				nargs, i, ConvertedChars, HadError = 0;
	WCHAR			**CmdLineList;
	char			**argv;
	char			ConvertedCmdLineArg[NAM_MAX_PATH];
	struct _stat	TmpBuf;
		
	/* Setup the call to atexit so I can clean up any allocated memory
	that I have allocated.*/
	
	dodebug(0,"WinMain()","Entering function in emtnam class", (char*)NULL);
	//MessageBox(NULL, "ETM Main Debug", "Note", MB_OK);
	atexit(CleanUp);

	/* Setup some defaults, that are depended upon to be set to the proper
	values.*/

	InitVariables();

	/* Deternmine if the operator wants to have debug info.  If so  make sure the 
	file debugit_etm is there.  If there then attempt to open debug file and set 
	DE_BUG to TRUE, otherwise set DE_BUG to FALSE.*/

	DE_BUG = _stat(EtmInfo.DebugOption, &TmpBuf) == NAM_ERROR ? FALSE : TRUE;

	if (DE_BUG) {

		char	debugfile[NAM_MAX_PATH];

		memset(debugfile, '\0', sizeof(debugfile));
		sprintf(debugfile, "%s%s", EtmInfo.LogLocation, DEBUGFILENAME);

		if ((debugfp = fopen(debugfile, "w+b")) == NULL) {
			DE_BUG = FALSE;
		}
	}

	if ((CmdLineList = CommandLineToArgvW(GetCommandLineW(),
											&nargs)) == NULL) {
		dodebug(0, "WinMain()", "%s", "Failed to get command line argments");
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
	}

	/* Allocate the space for the number of characters strings that
	will be in the argv list that we are going to build. */


	else if ((argv = (char **)malloc(sizeof(char *) * nargs)) == NULL) {
		dodebug(0, "WinMain()", "%s",
				"Malloc failed to allocate the required memory");
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		if (CmdLineList != NULL) {
			GlobalFree(CmdLineList);
		}
	}

	/* Loop through the number of arguments to convert them from wide characters 
   to ansi characters.  Utilize the _strdup function to allocate the required 
   string length for each argument and put them into *argv[] to be used with
   the already waiting program without any changes.*/

	else {
		for (i = 0; i < nargs; i++) {

			if ((ConvertedChars = WideCharToMultiByte(CP_UTF8, 0,
												CmdLineList[i], -1,
												ConvertedCmdLineArg,
												sizeof(ConvertedCmdLineArg),
												NULL, NULL)) == 0) {
				dodebug(0, "WinMain()", "%s",
						"Failed to convert from wide characters to ansi");
				HadError++;
				break;
			}

			if ((argv[i] = _strdup(ConvertedCmdLineArg)) == NULL) {

				int	j;

				for (j = 0; j < i; j++) {
					FreeItem(argv[j]);
				}

				dodebug(0, "WinMain()", "%s",
						"strdup failed to allocate the required memory");
				HadError++;
				break;
			}

		}

		if (EtmnamMain(nargs, argv)) {
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
	}
	
	dodebug(0,"WinMain()","Leaving function in emtnam class", (char*)NULL);
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

	ZeroMemory(&EtmInfo, sizeof(ETM_INFO));
	ZeroMemory(&WinInfo, sizeof(WIN_INFO));
	ZeroMemory(&SendInfo, sizeof(SEND_INFO));
	
	dodebug(0,"InitVariables()","Entering function in emtnam class", (char*)NULL);
	WinInfo.win_x1 = 0;
	WinInfo.win_x2 = 100;
	WinInfo.win_y1 = 0;
	WinInfo.win_y2 = 100;
	WinInfo.pic_x1 = 0;
	WinInfo.pic_x2 = 100;
	WinInfo.pic_y1 = 0;
	WinInfo.pic_y2 = 100;

	PipeInfo.MessageSize = MAX_MSG_SIZE;

	sprintf(EtmInfo.LogLocation, "%s", LOGLOCATION);
	sprintf(EtmInfo.DebugOption, "%s%s", EtmInfo.LogLocation, DEBUGIT);
	sprintf(EtmInfo.ProcessName, "%s", ETM_CONTROLLER);	
	sprintf(PipeInfo.PipeFile, "%s%s", PIPE_FILE_HEADER, PIPE_FILE);
	dodebug(0,"InitVariables()","Leaving function in emtnam class", (char*)NULL);	
}

