/////////////////////////////////////////////////////////////////////////////
//	File:	process_utils.cpp												/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		start_poroc()											/
//					Changed the sprintf call for the concur_process, no		/
//					longer need PROG_BIN_DIR, the env path is set to look in/
//					the directories for the executables that are used.		/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#pragma warning (disable : 4035 4068)
#include <windows.h>
#pragma warning (default : 4035 4068)
#include <stdio.h>
#include <stdlib.h>
#include <process.h>
#include <Psapi.h>
#include "gpnam.h"
#include "gpconcur.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	terminate_process_on_startup:	This function will terminate the process/
//									if a failure occurrs durning the startup/
//									initialization process.					/
//																			/
//	Parameters:																/
//		NONE			This function will use the concur_info structure and/
//							gp_info for its information.					/
//																			/
//	Returns:																/
//		SUCCESS:		  0  = The process was terminated.					/
//		GP_ERROR:		(-1) = Couldn't kill the process.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	terminate_process_on_startup(void)
{


	if (find_process(concur_info.process_name)) {
		gp_info.return_value = CHILD_RUN_WILD;
		return(GP_ERROR);
	}
	else if (concur_info.concurrent_running) {

		if (terminate_process()) {
			gp_info.return_value = CHILD_RUN_WILD;
			return(GP_ERROR);
		}
		else {
			gp_info.return_value = CONCURRENT_ERROR;
			return(GP_ERROR);
		}
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
//	terminate_process:	This function will terminate the process whose name	/
//							resides in the concur_info.process_name. This	/
//							will until further modifications be process		/
//							gpconcur.exe.									/
//																			/
//	Parameters:																/
//		NONE			This function will use the concur_info structure for/
//							its information.								/
//																			/
//	Returns:																/
//		SUCCESS:		  0  = The process was terminated.					/
//		GP_ERROR:		(-1) = Couldn't kill the process.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	terminate_process(void)
{

	int			had_error = 0;
	char		tmp_process_name[GP_MAX_PATH];
	DWORD		bytes_returned;
	HANDLE		processhd;
	HMODULE		modulehd;

//
// Here I will open the process with the TERMINATE flag set.
//

	if ((processhd = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_TERMINATE,
								 FALSE, (DWORD)concur_info.processid)) != NULL) {

//
// Here I will get the Module handle so I can get the process name.
//

		if (EnumProcessModules(processhd, &modulehd, sizeof(modulehd), &bytes_returned)) {

//
// Here I will get the process name for comparasion to make sure the correct process is
// getting terminated.
//

			if (GetModuleBaseName(processhd, modulehd, tmp_process_name,
								  sizeof(tmp_process_name))) {

//
// Now lets compare the 2 names to make sure that they agree.
//

				if (!_strnicmp(tmp_process_name, concur_info.process_name,
							  strlen(concur_info.process_name))) {

//
// Now if everything has gone right the correct will now be TERMINATED hopefuly.
//

					if (!TerminateProcess(processhd, 0)) {
						dodebug(CONCUR_WONT_DIE, "terminate_process()", NULL);
						gp_info.return_value = CONCUR_WONT_DIE;
						had_error++;
					}
				}
				else {
					dodebug(PROCESS_DONT_AGREE, "terminate_process()", NULL);
					gp_info.return_value = PROCESS_DONT_AGREE;
					had_error++;
				}
			}
		}
	}

	if (processhd != NULL) {
		CloseHandle(processhd);
	}

	return(had_error == FALSE ? SUCCESS : GP_ERROR);
}

/////////////////////////////////////////////////////////////////////////////
//	find_process:	This function will try to find the requested process.	/
//						If the requested process is running then the		/
//						concur_info structure will be filled out with the	/
//						proper information.									/
//																			/
//	Parameters:																/
//		process_name:	This is the name the function will use in its search/
//							through the presently running process, checking	/
//							for a match.									/
//																			/
//	Returns:																/
//		SUCCESS:		0    = No errors were encountered.					/
//		GP_ERROR:		(-1) = Couldn't do a proper search, status of the	/
//							   running process is unknown					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	find_process(char *process_name)
{

	unsigned int	i;
	char			tmp_process_name[GP_MAX_PATH];
	DWORD			process_array[MAX_PROCESS], process_enum, bytes_returned;
	HANDLE			processhd;
	HMODULE			modulehd;

//
// First thing is to get a complete list running processes.
//

	processhd = NULL;

	if (!EnumProcesses(process_array, sizeof(process_array), &bytes_returned)) {
		dodebug(0, "find_process()", "Couldn't get a list of runnning process see Sysadmin");
		return(GP_ERROR);
	}

//
// Now calculate how many process identifiers that were returned.
//

	process_enum = bytes_returned / sizeof(DWORD);

//
// Now do the comparision one process at a time.
//

	concur_info.concurrent_running = FALSE;

	for (i = 0; i < process_enum; i++) {

//
// Here I will open the process with the TERMINATE flag set.
//

		if ((processhd = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ,
									 FALSE, process_array[i])) != NULL) {

//
// Here I will get the Module handle so I can get the process name.
//

			if (EnumProcessModules(processhd, &modulehd, sizeof(modulehd), &bytes_returned)) {

//
// Here I will get the process name for comparasion to make sure the correct process is
// getting terminated.
//

				if (GetModuleBaseName(processhd, modulehd, tmp_process_name,
									  sizeof(tmp_process_name))) {

//
// Now lets compare the 2 names to make sure that they agree. If they do
// fill in the concur_info structure.
//

					if (!_strnicmp(tmp_process_name, process_name, strlen(process_name))) {
						concur_info.concurrent_running = TRUE;
						concur_info.processid = (unsigned long)process_array[i];
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

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
//	start_proc:				This function will try to start the requested	/
//								process.									/
//																			/
//	Parameters:																/
//		option_to_perform:	1 = Execute the FilePrintViewer program to print/
//								the results of a test program run.			/
//							2 = Parse the FAULT-FILE into the FaultFile		/
//								database.									/
//																			/
//	Returns:																/
//		SUCCESS:			0  = No errors were encountered.				/
//		GP_ERROR:		  (-1) = Couldn't perform one of the required tasks./
//																			/
/////////////////////////////////////////////////////////////////////////////

int	start_proc(int option_to_perform)
{

	char	pipe_file_name[32];
	char	job_to_do[4];
	char	concur_process[GP_MAX_PATH];

//
// First lets build the argument list that will be passed to the process that we are 
// about to fire off.
//

	sprintf(pipe_file_name, "%s", PIPE_FILE);

	sprintf(job_to_do, "%d", option_to_perform);

	sprintf(concur_process, "%s%s", PROG_BIN_DIR, PROCESS_NAME);

	if ((_spawnl(_P_DETACH, concur_process, job_to_do, pipe_file_name, NULL)) != SUCCESS) {
		dodebug(CONCURRENT_ERROR, "start_proc()", NULL);
		return(GP_ERROR);
	}

	return(SUCCESS);
}