/****************************************************************************
 *	File:	concurrent.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		1.0		Assigned it a version number.								*
 *																			*
 *		1.2		In perform_concurrent_op(), a new function was added		*
 *				open_dti(), the order was modified, now get_pipe_hd is		*
 *				because microsoft software doesn't work correctly, then		*
 *				open_dti(), next start_proc(), last							*
 *				respon_to_controlling_process().							*
 *		1.3		Removed unused code from perform_concurrent_op function that*
 *				was left over from previous builds.							*
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
#include "m9concur.h"

/****************************************************************************
 *	Local Constants															*
 ***************************************************************************/

/****************************************************************************
 *	Modules																	*
 ***************************************************************************/

/*
 *	get_pipe_hd:		This function will first create a named pipe, then
 *							attempt to connect to this named
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
	DWORD	dw;

	//Create the named pipe and become the named file server.
	if ((pipe_info.pipehd = CreateNamedPipe((LPCTSTR)pipe_info.pipe_file, PIPE_ACCESS_DUPLEX,  PIPE_TYPE_MESSAGE | PIPE_WAIT | PIPE_READMODE_MESSAGE,
									   2, pipe_info.message_size, pipe_info.message_size,  INFINITE, NULL)) != INVALID_HANDLE_VALUE) 
	{
		if (!ConnectNamedPipe(pipe_info.pipehd, NULL))
		{
			if ((dw = GetLastError()) != ERROR_PIPE_CONNECTED) 
			{
				char	tmpstring[1024];
				sprintf(tmpstring, "ConnectNamedPipe return'd %ul", dw);
				dodebug(0, "get_pipe_hd()", tmpstring);
				return(M9_ERROR);
			}
		}
	}
	
	if (pipe_info.pipehd == INVALID_HANDLE_VALUE) 
	{
		dodebug(PIPE_HANDLE_ERROR, "get_pipe_hd()", NULL);
		return(M9_ERROR);
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
 *		None:
 *
 */
void perform_concurrent_op(void)
{
	/*Get a handle to the controlling process before trying to start
	the dti up.*/
	if (get_pipe_hd()) 
	{
		dtb_info.dtb_errno = CONCURRENT_ERROR;
		return;
	}

	/*Next we will open the DTI and load the .dtb into memory.  This will get
	most of everything ready to run so that all we have to do is start the 
	burst going. */

	if (open_dti()) 
	{
		return;
	}

	//Now start the thread process.
	if (start_proc())
	{
		dtb_info.dtb_errno = CONCURRENT_ERROR;
		return;
	}

	/*Attempt to communicate to the controlling process
	through the pipe between the 2 processes.*/
	respond_to_controlling_process();

}