/****************************************************************************
 *	File:	process_utils.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	

#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include <stdio.h>
#include <stdlib.h>
#include <process.h>
#include <Psapi.h>
#include "visa.h"
#include "visatype.h"
#include "terM9.h"
#include "diagnostics.h"
#include "DM_Services.h"
#include "m910nam.h"
#include "m9concur.h"

/****************************************************************************
 *	Local Constants															*
 ***************************************************************************/

/****************************************************************************
 *	Modules																	*
 ***************************************************************************/

/*
 *	terminate_process_on_startup:	This function will terminate the process 
 *									if a failure occurrs durning the startup
 *									initialization process.
 *							
 *	Parameters:
 *		NONE			This function will use the concur_info structure for its
 *							information.
 *		
 *	Returns:
 *		SUCCESS:		0    = The process was terminated.
 *		M9_ERROR:		(-1) = Couldn't kill the process.
 *
 */

int	terminate_process_on_startup(void)
{


	if (find_process(concur_info.process_name)) {
		dtb_info.dtb_errno = CHILD_RUN_WILD;
		return(M9_ERROR);
	}
	else if (concur_info.concurrent_running) {

		if (terminate_process()) {
			dtb_info.dtb_errno = CHILD_RUN_WILD;
			return(M9_ERROR);
		}
		else {
			dtb_info.dtb_errno = CONCURRENT_ERROR;
			return(M9_ERROR);
		}
	}

	return(SUCCESS);
}

/*
 *	terminate_process:	This function will terminate the process whose name
 *							resides in the concur_info.process_name. This will
 *							until further modifications be process m9concur.exe.
 *							
 *	Parameters:
 *		NONE			This function will use the concur_info structure for its
 *							information.
 *		
 *	Returns:
 *		SUCCESS:		0    = The process was terminated.
 *		M9_ERROR:		(-1) = Couldn't kill the process.
 *
 */

int	terminate_process(void)
{

	int			had_error = 0;
	char		tmp_process_name[M9_MAX_PATH];
	DWORD		bytes_returned;
	HANDLE		processhd;
	HMODULE		modulehd;

/*
 * Here I will open the process with the TERMINATE flag set.
 */

	if ((processhd = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_TERMINATE,
								 FALSE, (DWORD)concur_info.processid)) != NULL) {

/*
 * Here I will get the Module handle so I can get the process name.
 */

		if (EnumProcessModules(processhd, &modulehd, sizeof(modulehd), &bytes_returned)) {

/*
 * Here I will get the process name for comparasion to make sure the correct process is
 * getting terminated.
 */

			if (GetModuleBaseName(processhd, modulehd, tmp_process_name,
								  sizeof(tmp_process_name))) {

/*
 * Now lets compare the 2 names to make sure that they agree.
 */

				if (!_strnicmp(tmp_process_name, concur_info.process_name,
							  strlen(concur_info.process_name))) {

/*
 * Now if everything has gone right the correct will now be TERMINATED hopefuly.
 */

					if (!TerminateProcess(processhd, 0)) {
						dodebug(CONCUR_WONT_DIE, "terminate_process()", NULL);
						dtb_info.dtb_errno = CONCUR_WONT_DIE;
						had_error++;
					}
				}
				else {
					dodebug(PROCESS_DONT_AGREE, "terminate_process()", NULL);
					dtb_info.dtb_errno = PROCESS_DONT_AGREE;
					had_error++;
				}
			}
		}
	}

	if (processhd != NULL) {
		CloseHandle(processhd);
	}

	return(had_error == FALSE ? SUCCESS : M9_ERROR);
}

/*
 *	find_process:		This function will try to find the requested process.  If the 
 *							requested process is running then the concur_info structure
 *							will be filled out with the proper information.
 *							
 *	Parameters:
 *		process_name:	This is the name the function will use in its search through the
 *							presently running process, checking for a match.
 *		
 *	Returns:
 *		SUCCESS:		0    = No errors were encountered.
 *		M9_ERROR:		(-1) = Couldn't do a proper search, status of the running process
 *							   is unknown.
 */

int	find_process(char *process_name)
{

	unsigned int	i;
	char			tmp_process_name[M9_MAX_PATH];
	DWORD			process_array[MAX_PROCESS], process_enum, bytes_returned;
	HANDLE			processhd;
	HMODULE			modulehd;

/*
 * First thing is to get a complete list running processes.
 */

	if (!EnumProcesses(process_array, sizeof(process_array), &bytes_returned)) {
		dodebug(0, "find_process()", "Couldn't get a list of runnning process see Sysadmin");
		return(M9_ERROR);
	}

/*
 * Now calculate how many process identifiers that were returned.
 */

	process_enum = bytes_returned / sizeof(DWORD);

/*
 * Now do the comparision one process at a time.
 */

	concur_info.concurrent_running = FALSE;

	for (i = 0; i < process_enum; i++) {

/*
 * Here I will open the process with the TERMINATE flag set.
 */

		if ((processhd = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ,
									 FALSE, process_array[i])) != NULL) {

/*
 * Here I will get the Module handle so I can get the process name.
 */

			if (EnumProcessModules(processhd, &modulehd, sizeof(modulehd), &bytes_returned)) {

/*
 * Here I will get the process name for comparasion to make sure the correct process is
 * getting terminated.
 */

				if (GetModuleBaseName(processhd, modulehd, tmp_process_name,
									  sizeof(tmp_process_name))) {

/*
 * Now lets compare the 2 names to make sure that they agree. If they do
 * fill in the concur_info structure.
 */

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

	return(SUCCESS);
}

/*
 *	start_proc:			This function will try to start the requested process.
 *							
 *	Parameters:
 *		proc_to_exec:	This is the space seperated command line argument list which
 *							will indicate which program and arguments are to run.
 *		
 *	Returns:
 *		SUCCESS:		0    = No errors were encountered.
 *		M9_ERROR:		(-1) = Couldn't perform one of the required tasks.
 */

int	start_proc(void)
{

	char	dtb_info_file[M9_MAX_PATH];
	char	pipe_file_name[32];
	char	concur_dtb[M9_MAX_PATH];
	char	commandString[2048] = {""};

	STARTUPINFO				startUpInfo;
	PROCESS_INFORMATION		procInfo;
	
	memset(commandString, '\0', sizeof(commandString));
	ZeroMemory(&startUpInfo, sizeof(STARTUPINFO));
	ZeroMemory(&procInfo, sizeof(PROCESS_INFORMATION));
	
	startUpInfo.cb = sizeof(STARTUPINFO);

/*
 * First lets build the argument list that will be passed to the process that we are 
 * about to fire off.
 */

	sprintf(dtb_info_file, "\"%s\"", dtb_info.file_name);

	sprintf(pipe_file_name, "%s", PIPE_FILE);

	sprintf(concur_dtb, "%s%s", PROG_BIN_DIR, PROCESS_NAME);

	sprintf(commandString, "%s \"%s\"", dtb_info_file, pipe_file_name);

	if ((CreateProcess(concur_dtb, commandString, NULL, NULL, FALSE, DETACHED_PROCESS,
					NULL, NULL, &startUpInfo, &procInfo)) == 0) {

	dodebug(CONCURRENT_ERROR, "StartProc()", commandString);
	return(M9_ERROR);

	}

	
	return(SUCCESS);
}