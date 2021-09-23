/////////////////////////////////////////////////////////////////////////////
//	File:	EtmMonitorRts.cpp												/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		GetPipeHd() - corrected the debug statement.			/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <stdio.h>
#include <windows.h>
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"
#include "..\..\Common\Include\pipe.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

//
//	GetPipeHd:		This function will first create a named pipe, then
//							attempt to connect to this named
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
	DWORD	dw;
	dodebug(0,"GetPipeHd()","Entering function in EtmMonitorRts class", (char*)NULL);
	// Create the named pipe and become the named file server.
	if ((PipeInfo.Pipehd = CreateNamedPipe((LPCTSTR)PipeInfo.PipeFile,
						  PIPE_ACCESS_DUPLEX,
						  PIPE_TYPE_MESSAGE | PIPE_WAIT | PIPE_READMODE_MESSAGE,
						  2, PipeInfo.MessageSize, PipeInfo.MessageSize,
						  INFINITE, NULL)) != INVALID_HANDLE_VALUE) {

		if (!ConnectNamedPipe(PipeInfo.Pipehd, NULL)) {

			if ((dw = GetLastError()) != ERROR_PIPE_CONNECTED) {
				dodebug(0, "GetPipeHd()", "ConnectNamedPipe return'd %ul", dw, (char*)NULL);  
				return(NAM_ERROR);
			}
		}
	}


	if (PipeInfo.Pipehd == INVALID_HANDLE_VALUE) {
		dodebug(PIPE_HANDLE_ERROR, "GetPipeHd()", (char*)NULL, (char*)NULL);
		return(NAM_ERROR);
	}
	
	dodebug(0,"GetPipeHd()","Leaving function in EtmMonitorRts class", (char*)NULL);
	return(SUCCESS);
}

//
//	PerformConcurrentOp:	This function will setup all required info to start
//								the concurrent operation.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		none:
//
//

void PerformConcurrentOp(void)
{
	
	dodebug(0,"PerformConcurrentOp()","Entering function in EtmMonitorRts class", (char*)NULL);
	//Get a handle to the controlling process before trying to start the required task.
	if (GetPipeHd()) {

		EtmInfo.EtmErrno = CONCURRENT_ERROR;
		return;
	}

	//Try to communicate with the controlling process through the pipe b/w 2 processes
	RespondToControllingProcess();
	dodebug(0,"PerformConcurrentOp()","Leaving function in EtmMonitorRts class", (char*)NULL);

}
