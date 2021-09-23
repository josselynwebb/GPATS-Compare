/////////////////////////////////////////////////////////////////////////////
//	File:	process_utils.c													/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of etmnam to implement Iads ver 3.2	/
//					software.												/
// 		2.0.1.0		terminate_process()										/
// 					modified the way the character array was being zero'd/	/
// 					initialized.											/
// 					find_process()											/
// 					modified the way the character array is being zero'd or /
// 					initialized.											/
// 					start_proc()											/
// 					This function was completely rewritten.  Need to change	/
//					what the executed process inherited from this process	/
//					and _spawn required more work then just changing to		/
//					CreateProcess().  etm_monitor was inheriting the opened	/
//					file handles from ATLAS via etm nam and this would lock	/
//					the c:/aps/data/fault-file for access from other		/
//					programs.												/
//		3.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//		3.0.1.0		StartProc()												/
//					modified what is sprintf into the commandString, removed/
//					the PROG_BIN_DIR, from the commandString, this is set in/
//					the path environmental variable path.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////	

#include <windows.h>
#include <stdio.h>
#include <process.h>
#include <Psapi.h>
#include <string.h>
#include "etmnam.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

//
//	TerminateTheProcess:	This function will terminate the process whose name
//							resides in the EtmInfo.ProcessName.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = The process was terminated.
//		NAM_ERROR:		(-1) = Couldn't kill the process, 
//
//

int	TerminateTheProcess(void)
{
	dodebug(0,"TerminateTheProcess()","Entering function in process_utils class", (char*)NULL);

	EtmInfo.ProcessRunning = TRUE;

	while (EtmInfo.ProcessRunning == TRUE) {

		if(!FindProcess()) {
			if (TerminateProcess()) {
				return(NAM_ERROR);
			}
		}
	}
	
	dodebug(0,"TerminateTheProcess()","Leaving function in process_utils class", (char*)NULL);
	return(SUCCESS);
}

//
//	TerminateProcess:	This function will terminate the process whose name
//							resides in the EtmInfo.ProcessName.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = The process was terminated.
//		NAM_ERROR:		(-1) = Couldn't kill the process
//
//

int	TerminateProcess(void)
{

	int			HadError = 0;
	char		TmpProcessName[NAM_MAX_PATH];
	DWORD		BytesReturned;
	HANDLE		processhd;
	HMODULE		modulehd;
	
	dodebug(0,"TerminateProcess()","Entering function in process_utils class", (char*)NULL);
	memset(TmpProcessName, '\0', sizeof(TmpProcessName));

	// Open the process with the TERMINATE flag set.
	if ((processhd = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ |
								 PROCESS_TERMINATE, FALSE,
								 EtmInfo.ProcessId)) != NULL) {


		// Get the Module handle to get the process name.
		if (EnumProcessModules(processhd, &modulehd,
							   sizeof(modulehd), &BytesReturned)) {

			/*Get the process name for comparasion to make sure the correct
			process is getting terminated.*/
			if (GetModuleBaseName(processhd, modulehd, TmpProcessName,
								  sizeof(TmpProcessName))) {

				// Compare the 2 names to make sure that they agree.
				if (!_strnicmp(TmpProcessName, EtmInfo.ProcessName,
							  strlen(EtmInfo.ProcessName))) {

					// Verify the correct process will now be TERMINATED.
					if (!TerminateProcess(processhd, 0)) {
						dodebug(PROCESS_WONT_DIE, "TerminateProcess()",
							   (char *)NULL, (char *)NULL);
						HadError++;
					}
					else {
						EtmInfo.ProcessRunning = FALSE;
					}
				}
				else {
					dodebug(PROCESS_DONT_AGREE, "TerminateProcess()",
						   (char *)NULL, (char *)NULL);
					HadError++;
				}
			}
		}
	}

	if (processhd != NULL) {
		CloseHandle(processhd);
	}
	
	dodebug(0,"TerminateProcess()","Leaving function in process_utils class", (char*)NULL);
	return(HadError == FALSE ? SUCCESS : NAM_ERROR);
}

//
//	FindProcess:		This function will try to find the requested process.
//							If the requested process is running then the
//							EtmInfo structure will be filled out with the
//							proper information.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't do a proper search status of the running
//							   process is unknown.
//
//

int	FindProcess(void)
{

	unsigned int	i;
	char			TmpProcessName[NAM_MAX_PATH];
	DWORD			ProcessArray[MAX_PROCESS], ProcessEenum, BytesReturned;
	HANDLE			processhd;
	HMODULE			modulehd;
	
	dodebug(0,"FindProcess()","Entering function in process_utils class", (char*)NULL);
	ZeroMemory(&processhd, sizeof(HANDLE));

	// Get a complete list running processes.
	if (!EnumProcesses(ProcessArray, sizeof(ProcessArray), &BytesReturned)) {

		dodebug(0, "FindProcess()",
				"Couldn't get a list of runnning process see Sysadmin",
				(char *)NULL);
		EtmInfo.ReturnValue = NAM_ERROR;
		return(NAM_ERROR);
	}

	// Calculate how many process identifiers that were returned.
	ProcessEenum = BytesReturned / sizeof(DWORD);

	// Do the comparision one process at a time.
	EtmInfo.ProcessRunning = FALSE;
	
	for (i = 0; i < ProcessEenum; i++) {
		// Open the process with the QUERY flag set.
		if ((processhd = OpenProcess(PROCESS_QUERY_INFORMATION|PROCESS_VM_READ,
									 FALSE, ProcessArray[i])) != NULL) {

			// Get the Module handle so to get the process name.
			if (EnumProcessModules(processhd, &modulehd,
								   sizeof(modulehd), &BytesReturned)) {

				// Get the process name for comparasion to make sure the correct
				// process is getting found.
				memset(TmpProcessName, '\0', sizeof(TmpProcessName));
				if (GetModuleBaseName(processhd, modulehd, TmpProcessName,
									  sizeof(TmpProcessName))) {

					// Compare the 2 names to make sure that they agree. If they do
					// fill in the EtmInfo structure.
					if (!_strnicmp(TmpProcessName, EtmInfo.ProcessName, 
								   strlen(EtmInfo.ProcessName))) {
						EtmInfo.ProcessRunning = TRUE;
						EtmInfo.ProcessId = (unsigned long)ProcessArray[i];
						break;
					}
				}
			}
		}

		if (processhd != NULL) {
			CloseHandle(processhd);
			processhd = NULL;
		}
	}

	if (processhd != NULL) {
		CloseHandle(processhd);
	}
	
	dodebug(0,"FindProcess()","Leaving function in process_utils class", (char*)NULL);
	return(SUCCESS);
}

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
//
int	StartProc(void)
{

	char					commandString[NAM_MAX_PATH];
	STARTUPINFO				startUpInfo;
	PROCESS_INFORMATION		procInfo;
	
	dodebug(0,"StartProc()","Entering function in process_utils class", (char*)NULL);
	memset(commandString, '\0', sizeof(commandString));
	ZeroMemory(&startUpInfo, sizeof(STARTUPINFO));
	ZeroMemory(&procInfo, sizeof(PROCESS_INFORMATION));
	
	startUpInfo.cb = sizeof(STARTUPINFO);
	sprintf(commandString, "%s%s \"%s\"", PROG_BIN_DIR, ETM_CONTROLLER, PIPE_FILE);
	//sprintf(commandString, "%s \"%s\"", ETM_CONTROLLER, PIPE_FILE);

	if ((CreateProcess(NULL, commandString, NULL, NULL, FALSE, DETACHED_PROCESS,
					  NULL, NULL, &startUpInfo, &procInfo)) == 0) {

		dodebug(0, "StartProc()", "Command string (%s)", commandString);
		EtmInfo.ReturnValue = CONCURRENT_ERROR;
		return(NAM_ERROR);
	}
	
	dodebug(0,"StartProc()","Leaving function in process_utils class", (char*)NULL);
	return(SUCCESS);
}

//
//	StartUpTheProcess:	This function will try to start the requested
//								process.  Then after the process is started the
//								function will try to get a connection to the
//								pipe for communication.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		0    = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//
//

int	StartUpTheProcess(void)
{
	dodebug(0,"StartUpTheProcess()","Entering function in process_utils class", (char*)NULL);
	// Start the process executer.
	if (StartProc()) {

		if (TerminateTheProcess()) {
			return(NAM_ERROR);
		}

		return(NAM_ERROR);
	}

	// Get a handle to the communication pipe between the 2 process.
	if (GetPipeHd()) {

		if (TerminateTheProcess()) {
			return(NAM_ERROR);
		}

		return(NAM_ERROR);
	}
	
	dodebug(0,"StartUpTheProcess()","Leaving function in process_utils class", (char*)NULL);
	return(SUCCESS);		
}

//
//	GetPipeHd:		This function will attempt to connect to a known named
//							pipe and return a handle to this pipe file.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = The handle was established.
//		NAM_ERROR:		(-1) = The handle could not established
//
//

int GetPipeHd(void)
{

	int		i;
	BOOL	Result;
	DWORD	Mode;
	
	dodebug(0,"GetPipeHd()","Entering function in process_utils class", (char*)NULL);
	// Loop for a set number of times to connect to a named pipe.
	for (i = 0; i < 40; i++) {
		//Added sleep to allow for proper connection
		Sleep(1000);
		if ((PipeInfo.Pipehd = CreateFile((LPCTSTR)PipeInfo.PipeFile,
										   GENERIC_READ | GENERIC_WRITE,
										   0, NULL, OPEN_EXISTING, 0, NULL))
										   != INVALID_HANDLE_VALUE) {
			break;
		}

		WaitNamedPipe(PipeInfo.PipeFile, 2000);
	}

	if (PipeInfo.Pipehd == INVALID_HANDLE_VALUE) {
		dodebug(PIPE_HANDLE_ERROR, "GetPipeHd()", (char *)NULL, (char *)NULL);
		return(NAM_ERROR);
	}

	Mode = PIPE_READMODE_MESSAGE | PIPE_WAIT;

	if ((Result = SetNamedPipeHandleState(PipeInfo.Pipehd, &Mode,
										 NULL, NULL)) != TRUE) {
		dodebug(0, "GetPipeHd()", "Failed to set pipe Mode", (char *)NULL);
		return(NAM_ERROR);
	}
	
	dodebug(0,"GetPipeHd()","Leaving function in process_utils class", (char*)NULL);
	return(SUCCESS);
}
