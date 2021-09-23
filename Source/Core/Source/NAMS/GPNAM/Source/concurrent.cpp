/////////////////////////////////////////////////////////////////////////////
//	File:	concurrent.cpp													/
//																			/
//	Creation Date:	19 Aug 2004												/
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
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <direct.h>
#include <windows.h>
#include "..\..\Common\Include\Constants.h"
#include "..\..\Common\Include\pipe.h"
#include "gpnam.h"
#include "gpconcur.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	pipeHandle:			This function will call get_pipe_hd and test for	/
//							errors if there are any.						/
//	Parameters:																/
//		NONE			This function will use gp_info structure for its	/
//							information.									/
//																			/	
//	Returns:																/
//		SUCCESS:		0	 =  The function got a handle.					/
//		GP_ERROR:		(-1) =  The function didn't get a handle.			/
//																			/
/////////////////////////////////////////////////////////////////////////////

int pipeHandle(void)
{

	if (get_pipe_hd()) {

//
// Here we failed to get a pipe handle so we will have to kill the process without 
// properly shutting it down.  We will return SUCCESS if the process was however
// killed without telling it to shutdown, but we will return to the ATLAS program
// the error code for improper shutdown of the concurrent process.
//

		if (terminate_process()) {
			return(GP_ERROR);
		}
		else {
			gp_info.return_value = IMPROPER_SHUTDOWN;
			return(GP_ERROR);
		}
	}

	return(SUCCESS);

}

/////////////////////////////////////////////////////////////////////////////
//	get_pipe_hd:		This function will attempt to connect to a known	/
//						maned pipe and return a handle to this pipe file.	/
//																			/
//	Parameters:																/
//		NONE			This function will use the pipe_info structure and	/
//							gp_info structure for its information.			/
//																			/
//	Returns:																/
//		SUCCESS:		0    = The handle was established.					/
//		GP_ERROR:		(-1) = The handle could not established				/
//																			/
/////////////////////////////////////////////////////////////////////////////

int get_pipe_hd(void)
{

	int		i;
	BOOL	Result;
	DWORD	Mode;

//
// Loop for a set number of times to connect to a named pipe
//

	for (i = 0; i < 4; i++) {

//
// Add sleep at the start of the for loop to allow for connection
//
		Sleep(500);

		if ((PipeInfo.Pipehd = CreateFile((LPCTSTR)PipeInfo.PipeFile,
										   GENERIC_READ | GENERIC_WRITE,
										   0, NULL, OPEN_EXISTING, 0, NULL))
										   != INVALID_HANDLE_VALUE) {
			break;
		}

		WaitNamedPipe(PipeInfo.PipeFile, 2000);
	}

	if (PipeInfo.Pipehd == INVALID_HANDLE_VALUE) {
		dodebug(PIPE_HANDLE_ERROR, "get_pipe_hd()", NULL);
		return(GP_ERROR);
	}

	Mode = PIPE_READMODE_MESSAGE | PIPE_WAIT;

	if ((Result = SetNamedPipeHandleState(PipeInfo.Pipehd, &Mode,
										 NULL, NULL)) != TRUE) {
		dodebug(0, "get_pipe_hd()", "Failed to set pipe Mode");
		return(GP_ERROR);
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
//	execute_gpconcur:	This function will setup all required info to start	/
//							the FilePrintViewer or to parse the FAULT-FILE	/
//							into the FaultFile.mdb.							/
//																			/
//	Parameters:																/
//		option_to_perform:	1 = Execute the FilePrintViewer program to		/
//								print the results of a test program run.	/
//							2 = Parse the FAULT-FILE into the FaultFile		/
//								database.									/
//																			/
//	Returns:																/
//		SUCCESS:		  0  = The process was executed.					/
//		GP_ERROR:		(-1) = Couldn't start the concurrent process		/
//																			/
/////////////////////////////////////////////////////////////////////////////

int execute_gpconcur(int option_to_perform)
{

//
// Now start the process executer.
//
	dodebug(0, "execute_gpconcur()", "calling start_proc(%d) ", option_to_perform, (char*)NULL); 
	if (start_proc(option_to_perform)) {

		if (terminate_process_on_startup()) {
			return(GP_ERROR);
		}

		gp_info.return_value = CONCURRENT_ERROR;
		return(GP_ERROR);
	}

//
// Well we this far things must be working, lets see if we now can get a handle
// to the communication pipe between the 2 processes.
//

	if (pipeHandle()) {
		return(GP_ERROR);
	}

//
// Well we got the pipe handle so now we can communicate with the concurrent
// process to see how it is doing. First we will ask the process for its
// status on startup.
//

	do {
		sprintf(PipeInfo.WriteMsg, "status");
		PipeInfo.WriteMsg[strlen("status")] = '\0';

		if (write_message()) {

			if (write_message()) {
				CloseHandle(PipeInfo.Pipehd);
				return(GP_ERROR);
			}
		}

		if (read_message()) {

			if (read_message()) {
				CloseHandle(PipeInfo.Pipehd);
				return(GP_ERROR);
			}
		}
	}
	while (!_strnicmp("illegal command", PipeInfo.ReadMsg, strlen(PipeInfo.ReadMsg)));

	if (_strnicmp(PipeInfo.ReadMsg, "success", PipeInfo.MessageSizeRead)) {

		sprintf(PipeInfo.WriteMsg, "%s", "quit");
		PipeInfo.WriteMsg[strlen("quit")] = '\0';

		write_message();

		sprintf(PipeInfo.WriteMsg, "%s", "status");
		PipeInfo.WriteMsg[strlen("status")] = '\0';

		write_message();

		read_message();

		Sleep(200);

		if (terminate_process_on_startup()) {
			CloseHandle(PipeInfo.Pipehd);
			return(GP_ERROR);
		}

		gp_info.return_value = CONCURRENT_ERROR;
		CloseHandle(PipeInfo.Pipehd);
		return(GP_ERROR);
	}

	do {
		sprintf(PipeInfo.WriteMsg, "%s", "doit");
		PipeInfo.WriteMsg[strlen("doit")] = '\0';

		if (write_message()) {

			if (write_message()) {
				CloseHandle(PipeInfo.Pipehd);
				return(GP_ERROR);
			}
		}

		sprintf(PipeInfo.WriteMsg, "status");
		PipeInfo.WriteMsg[strlen("status")] = '\0';

		if (write_message()) {

			if (write_message()) {
				CloseHandle(PipeInfo.Pipehd);
				return(GP_ERROR);
			}
		}

		if (read_message()) {

			if (read_message()) {
				dodebug(0, "execute_gpconcur()", "Read Message was %s size = %d",
						PipeInfo.ReadMsg, PipeInfo.MessageSizeRead);
				CloseHandle(PipeInfo.Pipehd);
				return(GP_ERROR);
			}
		}
	}
	while (!_strnicmp("illegal command", PipeInfo.ReadMsg, strlen(PipeInfo.ReadMsg)));

	do {
		sprintf(PipeInfo.WriteMsg, "%s", "quit");
		PipeInfo.WriteMsg[strlen("quit")] = '\0';

		if (write_message()) {

			if (write_message()) {
				CloseHandle(PipeInfo.Pipehd);
				return(GP_ERROR);
			}
		}

		sprintf(PipeInfo.WriteMsg, "status");
		PipeInfo.WriteMsg[strlen("status")] = '\0';

		if (write_message()) {

			if (write_message()) {
				CloseHandle(PipeInfo.Pipehd);
				return(GP_ERROR);
			}
		}

		if (read_message()) {

			if (read_message()) {
				CloseHandle(PipeInfo.Pipehd);
				return(GP_ERROR);
			}
		}
	}
	while (!_strnicmp("illegal command", PipeInfo.ReadMsg, strlen(PipeInfo.ReadMsg)));

	if (_strnicmp(PipeInfo.ReadMsg, "success", PipeInfo.MessageSizeRead)) {

		Sleep(200);

		if (terminate_process_on_startup()) {
			CloseHandle(PipeInfo.Pipehd);
			return(GP_ERROR);
		}

		gp_info.return_value = CONCURRENT_ERROR;
		CloseHandle(PipeInfo.Pipehd);
		return(GP_ERROR);
	}

	gp_info.return_value = SUCCESS;
	CloseHandle(PipeInfo.Pipehd);

	return(SUCCESS);
}