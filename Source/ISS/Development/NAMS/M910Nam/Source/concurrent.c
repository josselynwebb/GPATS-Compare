/****************************************************************************
 *	File:	concurrent.c													*
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

#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <direct.h>
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
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
 *	get_pipe_hd:		This function will attempt to connect to a known named
 *							pipe and return a handle to this pipe file.
 *							
 *	Parameters:
 *		NONE			This function will use the concur_info structure and 
 *							dtb_info structure for its information.
 *		
 *	Returns:
 *		SUCCESS:		0    = The handle was established.
 *		M9_ERROR:		(-1) = The handle could not established
 *
 */

int get_pipe_hd(void)
{

	int i;

/*
 * Loop for a set number of times to connect to a named pipe
*/

	for (i = 0; i < 40; i++) {


//Add sleep at the start of the for loop to allow for the connection/

		Sleep(50);

		if ((pipe_info.pipehd = CreateFile((LPCTSTR)pipe_info.pipe_file,
										   GENERIC_READ | GENERIC_WRITE,
										   0, NULL, OPEN_EXISTING, 0, NULL))
										   != INVALID_HANDLE_VALUE) {
			break;
		}

		WaitNamedPipe(pipe_info.pipe_file, 2000);
	}

	if (pipe_info.pipehd == INVALID_HANDLE_VALUE) {
		dodebug(PIPE_HANDLE_ERROR, "get_pipe_dh()", NULL);
		return(M9_ERROR);
	}

	return(SUCCESS);
}

/*
 *	terminate_running_m910:	This function will setup all required info to
 *								terminate a concurrent m910 digital test.
 *							
 *	Parameters:
 *		NONE			This function will use the concur_info structure and 
 *							dtb_info structure for its information.
 *		
 *	Returns:
 *		SUCCESS:		0    = The process was terminated.
 *		M9_ERROR:		(-1) = Couldn't kill the process.
 *
 */

int terminate_running_m910(void)
{


/*
 * First we will get a handle to the pipe file that should be open and waiting
 * for us to connect to it.
 */

	if (get_pipe_hd()) {

/*
 * Here we failed to get a pipe handle so we will have to kill the process without 
 * properly shutting it down.  We will return SUCCESS if the process was however
 * killed without telling it to shutdown, but we will return to the ATLAS program
 * the error code for improper shutdown of the concurrent process.
 */

		if (terminate_process()) {
			return(M9_ERROR);
		}
		else {
			dtb_info.dtb_errno = IMPROPER_SHUTDOWN;
			return(SUCCESS);
		}
	}

/*
 * Here send the message to the running process "shutitdown" to shut it down.
 * Then we will examine the response for "illegal_command", if received will send
 * the command again until we don't get illegal command.
 */

	do {
		sprintf(pipe_info.write_msg, "shutitdown");
		pipe_info.write_msg[strlen("shutitdown")] = '\0';

		if (write_message()) {

			if (write_message()) {
				return(M9_ERROR);
			}
		}

		sprintf(pipe_info.write_msg, "%s", "status");
		pipe_info.write_msg[strlen("status")] = '\0';

		if (write_message()) {

			if (write_message()) {
				return(M9_ERROR);
			}
		}

		if (read_message()) {

			if (read_message()) {
				return(M9_ERROR);
			}
		}
	}
	while (!_strnicmp("illegal command", pipe_info.read_msg, strlen(pipe_info.read_msg)));

	if (!_strnicmp("success", pipe_info.read_msg, pipe_info.message_size_read)) {

		dtb_info.dtb_errno = BURST_PASSED;
	}
	else if (!_strnicmp("failed", pipe_info.read_msg, pipe_info.message_size_read)) {

		dtb_info.dtb_errno = BURST_FAILED;
	}
	else if (!_strnicmp("burst not run", pipe_info.read_msg, pipe_info.message_size_read)) {

		dtb_info.dtb_errno = BURST_NOT_RUN;
	}
	else {

		dtb_info.dtb_errno = IMPROPER_SHUTDOWN;
	}		

	do {
		sprintf(pipe_info.write_msg, "quit");
		pipe_info.write_msg[strlen("quit")] = '\0';

		if (write_message()) {

			if (write_message()) {
				return(M9_ERROR);
			}
		}

		sprintf(pipe_info.write_msg, "%s", "status");
		pipe_info.write_msg[strlen("status")] = '\0';

		if (write_message()) {

			if (write_message()) {
				return(M9_ERROR);
			}
		}

		if (read_message()) {

			if (read_message()) {

				return(M9_ERROR);
			}
		}
	}
	while (!_strnicmp("illegal command", pipe_info.read_msg, strlen(pipe_info.read_msg)));

/*
 * The return'd message was not as expected, so we will sleep for 500 ms giving
 * the running process to hopefully terminate. Then we will check for the process
 * to not be running, if it is we will terminate the brute way.
 */

	if (_strnicmp("success", pipe_info.read_msg, pipe_info.message_size_read)) {

		Sleep(200);

		if (!find_process(concur_info.process_name)) {

			if (concur_info.concurrent_running == TRUE) {

				if (terminate_process()) {
					return(M9_ERROR);
				}
				else {
					dtb_info.dtb_errno = IMPROPER_SHUTDOWN;
					return(SUCCESS);
				}
			}
			else {
				dtb_info.dtb_errno = IMPROPER_SHUTDOWN;
				return(SUCCESS);
			}
		}
		else {
			dtb_info.dtb_errno = CONCURRENT_RUNNING;
			return(M9_ERROR);
		}
	}

	return(SUCCESS);
}

/*
 *	perform_concurrent_op:	This function will setup all required info to start
 *								a concurrent m910 digital test.
 *							
 *	Parameters:
 *		NONE			This function will use the concur_info structure and 
 *							dtb_info structure for its information.
 *		
 *	Returns:
 *		SUCCESS:		0    = The process was executed.
 *		M9_ERROR:		(-1) = Couldn't start the concurrent process
 *
 */

int perform_concurrent_op(void)
{

/*
 * Now start the process executer.
 */

	if (start_proc()) {

		if (terminate_process_on_startup()) {
			return(M9_ERROR);
		}

		dtb_info.dtb_errno = CONCURRENT_ERROR;
		return(M9_ERROR);
	}

/*
 * Well we this far things must be working, lets see if we now can get a handle
 * to the communication pipe between the 2 processes.
 */
	if (get_pipe_hd()) {

		if (terminate_process_on_startup()) {
			return(M9_ERROR);
		}

		dtb_info.dtb_errno = CONCURRENT_ERROR;
		return(M9_ERROR);
	}

/*
 * Well we got the pipe handle so now we can communicate with the concurrent
 * process to see how it is doing. First we will ask the process for its
 * status on startup.
 */

	do {
		sprintf(pipe_info.write_msg, "status");
		pipe_info.write_msg[strlen("status")] = '\0';

		if (write_message()) {

			if (write_message()) {
				return(M9_ERROR);
			}
		}

		if (read_message()) {

			if (read_message()) {
				return(M9_ERROR);
			}
		}
	}
	while (!_strnicmp("illegal command", pipe_info.read_msg, strlen(pipe_info.read_msg)));

	if (_strnicmp(pipe_info.read_msg, "success", pipe_info.message_size_read)) {

		sprintf(pipe_info.write_msg, "%s", "quit");
		pipe_info.write_msg[strlen("quit")] = '\0';

		write_message();

		if (terminate_process_on_startup()) {
			return(M9_ERROR);
		}

		dtb_info.dtb_errno = CONCURRENT_ERROR;
		return(M9_ERROR);
	}

	do {
		sprintf(pipe_info.write_msg, "bye");
		pipe_info.write_msg[strlen("bye")] = '\0';

		if (write_message()) {

			if (write_message()) {
				return(M9_ERROR);
			}
		}

		sprintf(pipe_info.write_msg, "%s", "status");
		pipe_info.write_msg[strlen("status")] = '\0';

		if (write_message()) {

			if (write_message()) {
				return(M9_ERROR);
			}
		}

		if (read_message()) {

			if (read_message()) {
				return(M9_ERROR);
			}
		}
	}
	while (!_strnicmp("illegal command", pipe_info.read_msg, strlen(pipe_info.read_msg)));

	if (_strnicmp(pipe_info.read_msg, "success", pipe_info.message_size_read)) {

		sprintf(pipe_info.write_msg, "%s", "quit");
		pipe_info.write_msg[strlen("quit")] = '\0';

		write_message();

		if (terminate_process_on_startup()) {
			return(M9_ERROR);
		}

		dtb_info.dtb_errno = CONCURRENT_ERROR;
		return(M9_ERROR);
	}

	dtb_info.dtb_errno = BURST_PASSED;

	return(SUCCESS);
}