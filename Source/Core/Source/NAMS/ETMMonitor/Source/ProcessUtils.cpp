/////////////////////////////////////////////////////////////////////////////
//	File:	ProcessUtils.cpp												/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
// 		1.0.1.0		terminate_the_reader()									/
// 					added the variable DWORD procStatus and initialized it	/
// 					to STILL_ACTIVE; removed the variable HANDLE pId;		/
// 					Changed the function from opening the process to first	/
// 					see if the process has exited by using the function call/
// 					GetExitCodeProcess() and checking the variable			/
// 					procStatus to see it still equals STILL_ACTIVE, return 	/
// 					if not else do what it would have done.					/
// 					terminate_the_imager()									/
// 					added the variable DWORD procStatus and initialized it	/
// 					to STILL_ACTIVE; removed the variable HANDLE pId;		/
// 					Changed the function from opening the process to first	/
// 					see if the process has exited by using the function call/
// 					GetExitCodeProcess() and checking the variable			/
// 					procStatus to see it still equals STILL_ACTIVE, return 	/
// 					if not else do what it would have done.					/
//		1.0.2.0		StartProc()												/
//					Added debug statements and properly terminated the debug/
//					statement at the end of the function.					/
//					TerminateTheReader()									/
//					Change the variable WinInfo.NotUsed to .ReadrNotUsed	/
//					to allow for the different window parameters.  Changed	/
//					the variable WinInfo.NotUsed to .ImagNotUsed for the	/
//					reason as above.										/
//					TerminateTheImager()									/
//					Change the variable WinInfo.NotUsed to .ImagNotUsed		/
//					to allow for the different window parameters.  Changed	/
//					the variable WinInfo.NotUsed to .ReadrNotUsed for the	/
//					reason as above.										/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <stdio.h>
#include <windows.h>
#include "Psapi.h"
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

//
//	StartProc:			This function will try to start the requested process.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	StartProc(void)
{

	STARTUPINFO				startUpInfo;
	PROCESS_INFORMATION		procInfo;
	
	ZeroMemory(&startUpInfo, sizeof(STARTUPINFO));
	ZeroMemory(&procInfo, sizeof(PROCESS_INFORMATION));
	
	dodebug(0,"StartProc()","Entering function in ProcessUtils class", (char*)NULL);
	startUpInfo.cb = sizeof(STARTUPINFO);

	dodebug(0, "StartProc()", "CommandString string (%s)", EtmInfo.CommandString, (char*)NULL);

	dodebug(0, "StartProc()", "Here setting the window size", (char*)NULL);
	startUpInfo.dwX = WinInfo.Ix1;
	startUpInfo.dwXSize = WinInfo.Ix2;
	startUpInfo.dwY = WinInfo.Iy1;
	startUpInfo.dwYSize = WinInfo.Iy2; 

	if (CreateProcess(NULL, EtmInfo.CommandString, 0, 0, FALSE,
					  DETACHED_PROCESS, 0, 0, &startUpInfo, &procInfo)) {
		if (EtmInfo.Mode == TARGET || EtmInfo.Mode == READER_ONLY) {
			ProcInfo.ReadProcInfo.dwThreadId = procInfo.dwThreadId;
			ProcInfo.ReadProcInfo.dwProcessId = procInfo.dwProcessId;
			ProcInfo.ReadProcInfo.hProcess = procInfo.hProcess;
			ProcInfo.ReaderRunning = TRUE;
		}
		else {
			ProcInfo.ImageProcInfo.dwThreadId = procInfo.dwThreadId;
			ProcInfo.ImageProcInfo.dwProcessId = procInfo.dwProcessId;
			ProcInfo.ImageProcInfo.hProcess = procInfo.hProcess;
			ProcInfo.ImagerRunning = TRUE;
		}
	}
	else {
		dodebug(0, "StartProc()", "CommandString string (%s)", EtmInfo.CommandString, (char*)NULL); 
		EtmInfo.EtmErrno = PROCESS_NOT_RUN;
		return(NAM_ERROR);
	}
	
	dodebug(0,"StartProc()","Leaving function in ProcessUtils class", (char*)NULL);
	return(SUCCESS);
}

//
//	TerminateTheReader:	This function will try to start the requested process.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	TerminateTheReader(void)
{

	RECT				position;
	DWORD				procStatus;
	WINDOWPLACEMENT		wPlaceStr;
	
	dodebug(0,"TerminateTheReader()","Entering function in ProcessUtils class", (char*)NULL);
	ZeroMemory(&position, sizeof(RECT));
	ZeroMemory(&wPlaceStr, sizeof(WINDOWPLACEMENT));
	wPlaceStr.length = sizeof(WINDOWPLACEMENT);
	procStatus = STILL_ACTIVE;

	/* Check if the process is still running or that operator didn't close it
	already. */
	if (!(GetExitCodeProcess(ProcInfo.ReadProcInfo.hProcess, &procStatus))) {
		dodebug(0, "TerminateTheReader()", "GetExitCodeProcess() failed: %d",
		GetLastError(), (char*)NULL);
		return(NAM_ERROR);
	}

	if (procStatus != STILL_ACTIVE) {
		dodebug(0, "TerminateTheReader", "Process not active", (char*)NULL);
		return(SUCCESS);
	}
	
	if (WinInfo.ReadrNotUsed == FALSE) { 
		if (TerminateReader()) {
			return(NAM_ERROR);
		}

		return(SUCCESS);
	}
		
	do {
		EnumWindows(EnumWindProc, EtmInfo.Mode);
	}
	while (continueEnum);

	if (GetWindowPlacement(WinInfo.ReadWhndl, &wPlaceStr)) {
		if (wPlaceStr.showCmd == SW_SHOWMINIMIZED) {
			wPlaceStr.showCmd = SW_SHOWNOACTIVATE;
			if (!SetWindowPlacement(WinInfo.ReadWhndl, &wPlaceStr)) {
				dodebug(0, "TerminateTheReader()",
						"Failed to set window placement", (char*)NULL);
				return(NAM_ERROR);
			}
		}			
	}
	else {
		dodebug(0, "TerminateTheReader()",
				"Failed to get window placement", (char*)NULL);
		return(NAM_ERROR);
	}

	if (GetWindowRect(WinInfo.ReadWhndl, &position)) {
		WinInfo.Rx1 = position.left;
		WinInfo.Ry1 = position.top;
		WinInfo.Rx2 = position.right - position.left;
		WinInfo.Ry2 = position.bottom - position.top;
		WinInfo.NotUsed = FALSE;
	}
	else {
		dodebug(0, "TerminateTheReader()",
				"Failed to get window rectangle", (char*)NULL);
		return(NAM_ERROR);
	}
		
	if (TerminateReader()) {
		return(NAM_ERROR);
	}
	dodebug(0,"TerminateTheReader()","Leaving function in ProcessUtils class", (char*)NULL);
	
	return(SUCCESS);
}

//
//	TerminateReader:	This function will try to start the requested process.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	TerminateReader(void)
{
	
	dodebug(0,"TerminateReader()","Entering function in ProcessUtils class", (char*)NULL);
	if (!TerminateProcess(ProcInfo.ReadProcInfo.hProcess, 0)) {
		dodebug(0, "TerminateReader()", "IADS Reader won't die", (char*)NULL);
		EtmInfo.EtmErrno = NAM_ERROR;
		return(NAM_ERROR);
	}

	ProcInfo.ReaderRunning = FALSE;
	dodebug(0,"TerminateReader()","Leaving function in ProcessUtils class", (char*)NULL);
	return(SUCCESS);
}

//
//	TerminateTheImager:	This function will try to start the requested process.
//							
//	Parameters:			
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	TerminateTheImager(void)
{

	DWORD				procStatus;
	RECT				position;
	WINDOWPLACEMENT		wPlaceStr;
	
	dodebug(0,"TerminateTheImager()","Entering function in ProcessUtils class", (char*)NULL);
	ZeroMemory(&position, sizeof(RECT));
	ZeroMemory(&wPlaceStr, sizeof(WINDOWPLACEMENT));
	wPlaceStr.length = sizeof(WINDOWPLACEMENT);
	procStatus = STILL_ACTIVE;
	
	/* Check if the process is still running or that operator didn't close it
	already*/
	if (!(GetExitCodeProcess(ProcInfo.ImageProcInfo.hProcess, &procStatus))) {
		dodebug(0, "TerminateTheImager()", "GetExitCodeProcess() failed: %d",
				GetLastError(), (char*)NULL);
		return(NAM_ERROR);
	}

	if (procStatus != STILL_ACTIVE) {
		return(SUCCESS);
	}
	
	if (WinInfo.NotUsed == FALSE) {  
		if (TerminateImager()) {
			return(NAM_ERROR);
		}

		return(SUCCESS);
	}
		
	do {
		EnumWindows(EnumWindProc, EtmInfo.Mode);
	}
	while (continueEnum);
		
	if (GetWindowPlacement(WinInfo.ImageWhndl, &wPlaceStr)) {
		if (wPlaceStr.showCmd == SW_SHOWMINIMIZED) {
			wPlaceStr.showCmd = SW_SHOWNOACTIVATE;
			if (!SetWindowPlacement(WinInfo.ImageWhndl, &wPlaceStr)) {
				dodebug(0, "TerminateTheImager()",
						"Failed to set window placement", (char*)NULL);
				return(NAM_ERROR);
			}
		}			
	}
	else {
		dodebug(0, "TerminateTheImager()",
				"Failed to get window placement", (char*)NULL);
		return(NAM_ERROR);
	}

	if (GetWindowRect(WinInfo.ImageWhndl, &position)) {
		WinInfo.Ix1 = position.left;
		WinInfo.Iy1 = position.top;
		WinInfo.Ix2 = position.right - position.left;
		WinInfo.Iy2 = position.bottom - position.top;
		WinInfo.NotUsed = FALSE;  
	}
	else {
		dodebug(0, "TerminateTheImager()",
				"Failed to get window rectangle", (char*)NULL);
		return(NAM_ERROR);
	}
		
	if (TerminateImager()) {
		return(NAM_ERROR);
	}
	dodebug(0,"TerminateTheImager()","Leaving function in ProcessUtils class", (char*)NULL);
	
	return(SUCCESS);
}

//
//	TerminateImager:	This function will try to start the requested process.
//							
//	Parameters:			
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	TerminateImager(void)
{
	
	dodebug(0,"TerminateImager()","Entering function in ProcessUtils class", (char*)NULL);
	if (!TerminateProcess(ProcInfo.ImageProcInfo.hProcess, 0)) {
		dodebug(0, "TerminateImager()", "IADS Imager won't die", (char*)NULL);
		EtmInfo.EtmErrno = NAM_ERROR;
		return(NAM_ERROR);
	}

	ProcInfo.ImagerRunning = FALSE;
	dodebug(0,"TerminateImager()","Leaving function in ProcessUtils class", (char*)NULL);
	return(SUCCESS);
}

//
//	WaitOnViewer:	This function will try to start the requested process.
//							
//	Parameters:			
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	WaitOnViewer(DWORD procId)
{

	DWORD	waitReturn;
	HANDLE	pHnd;
	
	dodebug(0,"WaitOnViewer()","Entering function in ProcessUtils class", (char*)NULL);
	if ((pHnd = OpenProcess(SYNCHRONIZE, 0, procId)) == NULL) {
		dodebug(0, "WaitOnViewer()",
				"Failed to get Synchronized handle to viewer", (char*)NULL);
		return(NAM_ERROR);
	}
	if ((waitReturn = WaitForSingleObject(pHnd, INFINITE)) == WAIT_FAILED) {
		dodebug(0, "WaitOnViewer()",
				"Failed to wait for process to end", (char*)NULL);
		CloseHandle(pHnd);
		return(NAM_ERROR);
	}

	CloseHandle(pHnd);
	dodebug(0,"WaitOnViewer()","Leaving function in ProcessUtils class", (char*)NULL);
	return(SUCCESS);
}
