/////////////////////////////////////////////////////////////////////////////
//	File:	concur_rts.cpp													/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily				/
//					GetPipeHd()												/
//					Changed the way the variables are written				/
//					(camel back style). Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables. Deleted the			/
//					declaration of the char TmpString, the function call to	/
//					memset, and the sprintf function call after the call to	/
//					!ConnectNamedPipe(), all is now done in t he DoDebug	/
//					function call.											/
//					PerformConcurrentOp()									/
//					Changed the way the variables are written				/
//					(camel back style).										/
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
#include "gpconcur.h"
#include "pipe.h"

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	GetPipeHd:			This function will first create a named pipe, then	/
//							attempt to connect to this named pipe and		/
//							return a handle to this pipe file.				/
//																			/
//	Parameters:																/
//		NONE			This function will use the PipeInfo structure for	/
//							its information.								/
//																			/
//	Returns:																/
//		SUCCESS:		0    = The handle was established.					/
//		M9_ERROR:		(-1) = The handle could not established				/
//																			/
/////////////////////////////////////////////////////////////////////////////

int GetPipeHd(void)
{

	DWORD	dw;

//
// Here I will create the named pipe and become the named file server.
//

	if ((PipeInfo.Pipehd = CreateNamedPipe((LPCTSTR)PipeInfo.PipeFile, PIPE_ACCESS_DUPLEX,
									   PIPE_TYPE_MESSAGE | PIPE_WAIT | PIPE_READMODE_MESSAGE,
									   2, PipeInfo.MessageSize, PipeInfo.MessageSize,
									   INFINITE, NULL)) != INVALID_HANDLE_VALUE) {

		if (!ConnectNamedPipe(PipeInfo.Pipehd, NULL)) {

			if ((dw = GetLastError()) != ERROR_PIPE_CONNECTED) {

				DoDebug(0, "GetPipeHd()", "ConnectNamedPipe return'd %ul", dw, (char*)NULL);
				return(GP_ERROR);
			}
		}
	}


	if (PipeInfo.Pipehd == INVALID_HANDLE_VALUE) {
		DoDebug(PIPE_HANDLE_ERROR, "GetPipeHd()", (char*)NULL);
		return(GP_ERROR);
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
//	PerformConcurrentOp:	This function will setup all required info to	/
//								start the concurrent operation.				/
//																			/
//	Parameters:																/
//		NONE			This function will use the GPConInfo structure for	/
//							its information.								/
//																			/
//	Returns:																/
//		None:																/
//																			/
/////////////////////////////////////////////////////////////////////////////

void PerformConcurrentOp(void)
{

//
// We need to get a handle to the controlling process before trying to start
// the dti up.  Bill G. crapware doesn't work right in the controlling process,
// it is suppose to wait up to 2000 ms but will only wait for 5 ms get software
// from you know who.
//

	if (GetPipeHd()) {

		GPConInfo.GPErrno = CONCURRENT_ERROR;
		return;
	}

//
// We made it this far things must be working, lets see if we now can communication
// to the controlling process through the pipe between the 2 processes.
//

	RespondToControllingProcess();

	return;

}
