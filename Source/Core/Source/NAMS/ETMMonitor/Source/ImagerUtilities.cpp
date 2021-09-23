/////////////////////////////////////////////////////////////////////////////
//	File:	ImagerUtilities.cpp												/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		PerformImagerFunctiuon()								/
//					Changed the variable WinInfo.NotUsed to .ReadrNotUsed	/
//					needed info for both readr and imager windows.			/
//					ImagerCommandLine()										/
//					Modified the sprintf(endString to change which x and y	/
//					values that are being used. Added a debugstatement.		/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/ 
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
//	PerformImagerFunction:	This function will start the process of getting
//								the data required to execute the imager software
//								program. 
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int PerformImagerFunction(void)
{
	/* Get the command that we are to perform from the controlling process.
	Check to see what IADS version we need to use.*/
	dodebug(0,"PerformImagerFunction()","Entering function in ImagerUtilities class", (char*)NULL);
	if (SetUpIads()) {
		return(NAM_ERROR);
	}

	if (ProcessGatheredData()) {
		return(NAM_ERROR);
	}
	
	if (ProcInfo.ImagerRunning) {
		if (TerminateTheImager()) {
			return(NAM_ERROR);
		}
	}
	
	if (StartProc()) {
		return(NAM_ERROR);
	}

	do {
		EnumWindows(EnumWindProc, EtmInfo.Mode);
	}
	while (continueEnum);
	
	if (WinInfo.NotUsed == FALSE) {
		if (SetAppWindow()) {
			return(NAM_ERROR);
		}
	}
	else {
		ShowWindow(WinInfo.ImageWhndl, SW_SHOWNOACTIVATE);
	}

	if (EtmInfo.Concurrent == FALSE) {

		ShowWindow(WinInfo.ImageWhndl, SW_SHOWMINIMIZED);
		Sleep(90);
		ShowWindow(WinInfo.ImageWhndl, SW_SHOWNORMAL);

		if (WaitOnViewer(ProcInfo.ImageProcInfo.dwProcessId)) {
			return(NAM_ERROR);
		}
	}

	else {

		ShowWindow(WinInfo.ImageWhndl, SW_SHOWMINIMIZED);
		Sleep(90);
		ShowWindow(WinInfo.ImageWhndl, SW_SHOWNORMAL);
	}

	if (SendMessage(BYE_MSG)) {
		return(NAM_ERROR);
	}
	
	dodebug(0,"PerformImagerFunction()","Leaving function in ImagerUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	ImagerCommandLine:	This function will copy into the character arrays
//							passed to it, the proper information required to
//							allow the imager program to be executed.
//							
//	Parameters:
//		programString:		This is the character string that will contain the
//							executable program full path.
//		midString:			This is the string that follows the programString in
//							the command line.
//		endString:			This is the string that contains the options for the
//							imager image.
//		
//	Returns:
//		none:
//
//

void ImagerCommandLine(char *programString, char *midString, char *endString)
{
	
	dodebug(0,"ImagerCommandLine()","Entering function in ImagerUtilities class", (char*)NULL);

	sprintf(midString, "'");

	if (ImagInfo.NotUsed == FALSE) {
		if (ImagInfo.DefinedUsed == TRUE) {
			sprintf(endString, "-%s", ImagInfo.Defined);
		}
		else {
			sprintf(endString, "-xoff %d -yoff %d -xlen %d -ylen %d",
				ImagInfo.xoff, ImagInfo.yoff, ImagInfo.xlen, ImagInfo.ylen);


			dodebug(0, "ImagerCommandLine()", "End string is (%s)", endString, (char*)NULL);
		}
	}

	if (ImagInfo.NotUsed) {
		sprintf(endString, "");
	}
	
	dodebug(0, "ImagerCommandLine()", "End string 1 is (%s)", endString, (char*)NULL); 
	sprintf(programString, "\"%s%s\"", EtmInfo.ProgramBase, IMAGER_PROG);
	dodebug(0,"ImagerCommandLine()","Leaving function in ImagerUtilities class", (char*)NULL);
}
