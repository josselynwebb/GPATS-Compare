/////////////////////////////////////////////////////////////////////////////
//	File:	ProcessUtils.cpp												/
//																			/
//	Creation Date:	2 March 2005											/
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

#include <stdio.h>
#include <windows.h>
#include <Psapi.h>
#include "constants.h"
#include "ProgramLauncher.h"

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
	
	ZeroMemory(&startUpInfo, sizeof(STARTUPINFO));
	ZeroMemory(&ProcInfo, sizeof(PROCESS_INFORMATION));
	
	startUpInfo.cb = sizeof(STARTUPINFO);

	if (CreateProcess(NULL, ProgInfo.CommandString, 0, 0, FALSE,
					  DETACHED_PROCESS, 0, 0, &startUpInfo, &ProcInfo.ProgramInfo)) {
		ProcInfo.ProgramRunning = TRUE;
		dodebug(0, "StartProc()", "CommandString string (%s)", ProgInfo.CommandString);
	}
	else {
		dodebug(0, "StartProc()", "CommandString string (%s)", ProgInfo.CommandString);
		ProgInfo.ProgErrno = PROCESS_NOT_RUN;
		return(NAM_ERROR);
	}

	return(SUCCESS);
}

//
//	TerminateTheProcess:	This function will terminate the requested process.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	TerminateTheProcess(void)
{

	DWORD	procStatus;

	procStatus = STILL_ACTIVE;

//
// First I will see if the process is still running / operator didn't close it
// already.  This is needed to prevent the program from continualy looping in
// the EnumWindow loop.
//

	if (!(GetExitCodeProcess(ProcInfo.ProgramInfo.hProcess, &procStatus))) {
		dodebug(0, "TerminateTheProcess()", "GetExitCodeProcess() failed: %d",
				GetLastError(), (char*)NULL);
		return(NAM_ERROR);
	}

	if (procStatus != STILL_ACTIVE) {
		dodebug(0, "TerminateTheProcess", "Process not active", (char*)NULL);
		return(SUCCESS);
	}

	if (TerminateProgram()) {
		return(NAM_ERROR);
	}
	
	return(SUCCESS);
}

//
//	TerminateProgram:	This function will terminate the requested process.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	TerminateProgram(void)
{

	if (!TerminateProcess(ProcInfo.ProgramInfo.hProcess, 0)) {
		dodebug(0, "TerminateProgram()", "Program won't die", (char*)NULL);
		ProgInfo.ProgErrno = NAM_ERROR;
		return(NAM_ERROR);
	}

	ProcInfo.ProgramRunning = FALSE;
	return(SUCCESS);
}

//
//	WaitOnTheProcess:	This function will wait until the program terminates.
//							
//	Parameters:			
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int	WaitOnTheProcess(void)
{

	DWORD	waitReturn;
	DWORD	procStatus;
	HANDLE	pHnd;
	
	if ((pHnd = OpenProcess(SYNCHRONIZE, 0, ProcInfo.ProgramInfo.dwProcessId)) == NULL) {
		dodebug(0, "WaitOnViewer()",
				"Failed to get Synchronized handle to viewer", (char*)NULL);
		return(NAM_ERROR);
	}

	waitReturn = WaitForSingleObject(pHnd, INFINITE);

	if (!(GetExitCodeProcess(ProcInfo.ProgramInfo.hProcess, &procStatus))) {
		dodebug(0, "WaitOnTheProcess()", "GetExitCodeProcess() failed: %d",
				GetLastError(), (char*)NULL);
		return(NAM_ERROR);
	}

	CloseHandle(pHnd);
	CloseHandle(ProcInfo.ProgramInfo.hThread);

	if (waitReturn == WAIT_FAILED) {
		dodebug(0, "WaitOnViewer()", "Failed to wait for process to end", (char*)NULL);
		return(NAM_ERROR);
	}
	else {
		ProgInfo.ExitStatus = procStatus;
		dodebug(0, "WaitOnViewer()", "The return status = %d", ProgInfo.ExitStatus);
	}

	return(SUCCESS);
}
