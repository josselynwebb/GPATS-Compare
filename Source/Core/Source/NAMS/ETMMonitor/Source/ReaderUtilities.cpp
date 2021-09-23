/////////////////////////////////////////////////////////////////////////////
//	File:	ReaderUtilities.cpp												/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		PerformReaderFunction()									/
//					Removed the function call to SetUpIads this was moved to/
//					the ProcessGatheredData function, which allowed for all	/
//					values to be known. Changed the variable WinInfo.NotUsed/
//					to WinInfo.ReadrNotUsed, nneded to know which window was/
//					being used.												/
//					ReadrCommandLine()										/
//					Modified the function to check to see if the iads which	/
//					iads version was being used and set the proper variables/
//					that need to be used, and added a debug statement.		/
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
//	PerformReaderFunction:	This function will start the process of getting
//								the data required to execute the reader software
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

int PerformReaderFunction()
{
	// Get the command that we are to perform from the controlling process.
	// Check to see what IADS version we need to use.
	
	dodebug(0,"PerformReaderFunction()","Entering function in ReaderUtilities class", (char*)NULL);

	if (SetUpIads()) {
		return(NAM_ERROR);
	}


	if (GetMode(FUNCTIONAL)) {
		return(NAM_ERROR);
	}

	if (ProcessGatheredData()) {
		return(NAM_ERROR);
	}

	if (ProcInfo.ReaderRunning) {
		if (TerminateTheReader()) {
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
		ShowWindow(WinInfo.ReadWhndl, SW_SHOWNOACTIVATE);
	}

	if (EtmInfo.Concurrent == FALSE) {
	
		ShowWindow(WinInfo.ReadWhndl, SW_SHOWMINIMIZED);
		Sleep(90);
		ShowWindow(WinInfo.ReadWhndl, SW_SHOWNORMAL);

		if (WaitOnViewer(ProcInfo.ReadProcInfo.dwProcessId)) {
			return(NAM_ERROR);
		}
	}

	else {
		ShowWindow(WinInfo.ReadWhndl, SW_SHOWMINIMIZED);
		Sleep(90);
		ShowWindow(WinInfo.ReadWhndl, SW_SHOWNORMAL);
	}
	
	if (SendMessage(BYE_MSG)) {
		return(NAM_ERROR);
	}
	
	dodebug(0,"PerformReaderFunction()","Entering function in ReaderUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	ReaderCommandLine:	This function will copy into the character arrays
//							passed to it, the proper information required to
//							allow the imager program to be executed.
//							
//	Parameters:
//		programString:		This is the character string that will contain the
//							executable program full path.
//		midString:			This is the string that follows the programString in
//							the command line.
//		endString:			This is the string that will end out the command
//							line command except for the window options.
//		
//	Returns:
//		none:
//
//

void ReaderCommandLine(char *programString, char *midString, char *endString)
{
	
	dodebug(0,"ReaderCommandLine()","Entering function in ReaderUtilities class", (char*)NULL);
	sprintf(programString, "\"%s%s\"", EtmInfo.ProgramBase, READER_PROG);

	if (EtmInfo.Mode == READER_ONLY) {
		sprintf(midString, "%s \"", USERID);
	}
	else {
		sprintf(midString, "%s %s \"", USERID, "-TARGET");
	}
	
	sprintf(endString, "%s", "\"");
	 
	
	dodebug(0, "ReaderCommandLine()", "File name is (%s)", EtmInfo.FileName, (char*)NULL); 
	
	dodebug(0,"ReaderCommandLine()","Leaving function in ReaderUtilities class", (char*)NULL);
}
