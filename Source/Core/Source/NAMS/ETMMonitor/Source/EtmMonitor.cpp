/////////////////////////////////////////////////////////////////////////////
//	File:	EtmMonitor.cpp													/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
// 		1.0.1.0		added pragma statement to disable warnings.  These are	/
// 					added after the warning is checked out and verified to	/
// 					not cause any problems.  Some warnings can not be 		/
// 					elimitated.												/
//		1.0.2.0		EtmMonitor.h											/
//					Added the macro define DIRECTORY2_CMD, DIR2_TOP_LEVEL,	/
//					and DIRECTORY2_SEP.  These allowded for forward /		/
//					instead of back \.  Deleted NotUsed and added two new	/
//					variables ImagNotUsed and ReadrNotUsed to the WIN_INFO	/
//					structure.  This allowed for the setting of the two		/
//					different windows sizes, one for the imager and one for	/
//					the reader.  Added new variable SavedFileName to the	/
//					ETM_INFO structure.  This allowed for the m910 fault	/
//					callout file to be displayed in IADS  Added four new	/
//					variables to the IMAG_INFO structure; wxoff, wyoff,		/
//					wxlen, and wylen, Need for setting the image window size/
//					EtmMonitor.cpp											/
//					InitVariables() - Deleted the variable WinInfo.NotUsed	/
//					and added Winfo.ReadrNotUsed and WinInfo.ImagNotUsed,	/
//					needed to setup for both the readr and zoomview.		/
//					EtmMonitorRts.cpp										/
//					GetPipeHd() - corrected the debug statement.			/
//					EtmViewerUtilities.cpp									/
//					Changed the size of KeyData.ItemValue to allow for up to/
//					windows max path size.  Changed the value of			/
//					KeyData[0].ItemValue to c:\\APS\\DATA\\m910nam.xml to	/
//					allow for IADS 3.4.13 to display m910 fault callout file/
//					SetUpIads() - Changed the if statement to also test for	/
//					EtmInfo.Mode to be the readr, include a debug statement./
//					GetIadsVersion() - Added two debug statements to help	/
//					in debugging.											/
//					SetUpTheRegistry() - Had to do major overhaul, the m910	/
//					fault callout file couldn't be displayed so the rework	/
//					of this function was required.							/
//					ExtractApsKeyName() - Deleted DirBuf, nolonger need to	/
//					get _getcwd(), using the path of the EtmInfo.FileName to/
//					get the correct dir deleted the call to _getcwd().		/
//					Added a second call to GetInfo() checking for			/
//					DIRECTORY2_CMD, / instead of \.							/
//					FileUtilities.cpp										/
//					Added a new element row to GetThis: DIR2_TOP_LEVEL,		/
//					DIRECTORY2_SEP, and DIRECTORY_SIZE.  This allowed for	/
//					the checking where the directory path contain / instead	/
//					of \ .													/
//					GetInfo() - Corrected the debug statement, added/		/
//					(char*) to terminate the debug statement properly.		/
//					FindPoint() - Corrected the debug statement, added		/
//					(char*) to terminate the debug statement properly.		/
//					MoveToPoint() - Corrected the debug statement, added	/
//					(char*) to terminate the debug statement properly.		/
//					GenUtilities.cpp										/
//					SetupWindowSize() - Corrected the debug statement, added/
//					(char*) to terminate the debug statement properly.		/
//					Change the else if to an if statement. In ther if		/
//					statement EtmInfo.Mode == ZOOM, added the setting of the/
//					varaibles ImageInfo.WNotUsed, .wxoff, wxlen, .wyoff,	/
//					.wylen. This allowed for the proper setting of the		/
//					window size of the imaging window size and position. In	/
//					the else portion added the setting of the variable		/
//					WinInfo.ReadrNotUsed.									/
//					GetMode() - Corrected the debug statement, added (char*)/
//					to terminate the debug statement properly.				/
//					ProcessGatheredData() - Added the function call to		/
//					SetUpIads												/
//					ProcessData() - Added the setting of the variable		/
//					EtmInfo.SavedFileName and debug statemenmts in the first/
//					else statement. This allowed for resetting of the		/
//					registry entry for 3.4.13. In the second major else		/
//					statement modified the setting the WinInfo.ImagNotUsed	/
//					and .ReadrNotUsed depending on the EtmInfo.Mode value.	/
//					GenerateCommandLine() - In the case TARGET, modified the/
//					setting of c1 and the labelString based on which iads	/
//					was to be used, these not need for iads3.4.13. Addded	/
//					debug statement to ZOOM. Corrected the debug statement,	/
//					added (char*) to terminate the debug statement properly./
//					Added and if statement the end of the function to add	/
//					window info for the imager if used, also corrected the	/
//					debug stmt.												/
//					SetAppWindow() - Changed the WinInfo.NotUsed to an if	/
//					statement which will set the proper WinInfo element.	/
//					ImagertUtilities.cpp									/
//					PerformImagerFunctiuo - Changed the variable			/
//					WinInfo.NotUsed to .ReadrNotUsed needed info for both	/
//					readr and imager windows.								/
//					ImagerCommandLine - Modified the sprintf(endString to	/
//					change which x and y values that are being used. Added	/
//					a debugstatement.										/
//					message.cpp												/
//					RespondToControllingProcess() - Corrected the debug		/
//					statement, added (char*) to terminate the debug			/
//					statement properly.										/
//					MessageUtility() - Corrected the debug statement, added /
//					(char*) to terminate the debug statement properly.		/
//					ProcessUtils.cpp										/
//					StartProc - Added debug statements and properly			/
//					terminated the debug statement at the end of the		/
//					function.												/
//					TerminateTheReader - Change the variable WinInfo.NotUsed/
//					to .ReadrNotUsed to allow for the different window		/
//					parameters. Changed the variable WinInfo.NotUsed to		/
//					.ImagNotUsed for the reason as above.					/
//					TerminateTheImager - Change the variable WinInfo.NotUsed/
//					to .ImagNotUsed to allow for the different window		/
//					parameters. Changed the variable WinInfo.NotUsed to		/
//					.ReadrNotUsed for the reason as above.					/
//					ReaderUtilities.cpp										/
//					PerformReaderFunction - Removed the function call to	/
//					SetUpIads this was moved to the ProcessGatheredData		/
//					function, which allowed for all values to be known.		/
//					Changed the variable WinInfo.NotUsed to					/
//					WinInfo.ReadrNotUsed, nneded to know which window was	/
//					being used.												/
//					ReadrCommandLine - Modified the function to check to see/
//					if the iads which iads version was being used and set	/
//					the proper variables that need to be used, and added a	/
//					debug statement.										/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
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
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"

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
	RECT			screenSize;

	//Setup some defaults and initilize some variables to their defaults.
	dodebug(0, "WinMain", "Entering function in EtmMonitor class.", (char*)NULL);
    //MessageBox(NULL, "Monitor Main Debug", "Note", MB_OK);
	
	InitVariables();
	ZeroMemory(&screenSize, sizeof(RECT));

	/* Determine if the program should be run in the DEBUG mode. If so open
	the Debug file, if file doesn't open then set DEBUG back to 0.*/

	DE_BUG = _stat(EtmInfo.DebugOption, &TmpBuf) == NAM_ERROR ? 0 : 1;

	if (DE_BUG) {

		char	debugfile[256];

		sprintf(debugfile, "%s%s", EtmInfo.LogLocation, DEBUGFILENAME);

		if ((debugfp = fopen(debugfile, "w+b")) == NULL) {
			DE_BUG = FALSE;
		}
	}

	/*Get the system screen size.Certain variables need to be set prior to checking
	and setting the screen variables.*/
	if (SystemParametersInfo(SPI_GETWORKAREA, 0, &screenSize, 0)) {
		WinInfo.ScreenW = screenSize.right;
		WinInfo.ScreenH = screenSize.bottom;
	}
	else {
		dodebug(0, "WinMain()", "Failed to get the system screen size", (char*)NULL);
		EtmInfo.EtmErrno = PROCESS_NOT_RUN;
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		return(EtmInfo.EtmErrno);
	}

	//Get the command line arguments.
	if ((CmdLineList = CommandLineToArgvW(GetCommandLineW(),&nargs)) == NULL) {
		dodebug(0, "WinMain()", "Failed to get the command line argments", (char*)NULL);
		EtmInfo.EtmErrno = PROCESS_NOT_RUN;
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		return(EtmInfo.EtmErrno);
	}

	/* Allocate the space for the number of characters strings that
	will be in the argv list that we are going to build. */
	if ((argv = (char **)malloc(sizeof(char*) * nargs)) == NULL) {
		dodebug(0, "WinMain()", "Malloc failed to allocate the required memory", (char*)NULL);
		EtmInfo.EtmErrno = PROCESS_NOT_RUN;
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		if (CmdLineList != NULL) {
			GlobalFree(CmdLineList);
		}
		return(EtmInfo.EtmErrno);
	}

	/* Loop through the number of arguments to convert them from wide characters
	to ansi characters.  Utilize the _strdup function to allocate the required
	string length for each argument and put them into *argv[] to be used with
	the already waiting program without any changes.*/

	for (i = 0; i < nargs; i++) {

		if ((ConvertedChars = WideCharToMultiByte(CP_UTF8, 0, CmdLineList[i],
												 -1, ConvertedCmdLineArg,
												 sizeof(ConvertedCmdLineArg),
												 NULL, NULL)) == 0) {
			dodebug(0, "WinMain()", "Failed to convert from wide characters to ansi", (char*)NULL);
			EtmInfo.EtmErrno = PROCESS_NOT_RUN;
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
			EtmInfo.EtmErrno = PROCESS_NOT_RUN;
			break;
		}

	}

	if (EtmMonitorMain(nargs, argv)) {
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
	
	dodebug(0, "WinMain", "Leaving function in EtmMonitor class.", (char*)NULL);
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
	ZeroMemory(&ProcInfo, sizeof(PROC_INFO));
	ZeroMemory(&ImagInfo, sizeof(IMAG_INFO));
	
	dodebug(0,"InitVariables()","Entering function in EtmMonitor class", (char*)NULL);
	EtmInfo.EtmErrno = SUCCESS;

	ProcInfo.ReaderRunning = FALSE;
	ProcInfo.ImagerRunning = FALSE;

	ImagInfo.NotUsed = TRUE;
	ImagInfo.DefinedUsed = FALSE;

	/*WinInfo.ReadrNotUsed = TRUE;  
	WinInfo.ImagNotUsed = TRUE; */
	WinInfo.NotUsed = TRUE; 

	sprintf(EtmInfo.LogLocation, "%s", LOGLOCATION);
	sprintf(EtmInfo.DebugOption, "%s%s", EtmInfo.LogLocation, DEBUGIT);
	
	dodebug(0,"InitVariables()","Leaving function in EtmMonitor class", (char*)NULL);
}
