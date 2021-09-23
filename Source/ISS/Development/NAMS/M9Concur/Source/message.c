/****************************************************************************
 *	File:	message.c														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		1.0		Assigned it a version number.								*
 *		1.3		In the respond_to_controlling_process function the case		*
 *				SHUTITDOWN statement was modified to allow the program to	*
 *				halt the digital test.  The if statement was incorrect. Also*
 *				the variable pin_state was set to insure resetting of the	*
 *				DTI upon closing the digital test.							*
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
 *	write_message:		This function will write out the message that is in the
 *							PIPE_INFO structure and perform error checking on it.
 *							
 *	Parameters:
 *		NONE			This function will use the concur_info structure and 
 *							dtb_info structure for its information.
 *		
 *	Returns:
 *		SUCCESS:		0    = The message was successfully written.
 *		M9_ERROR:		(-1) = The message was not successfully written.
 *
 */
int write_message(void)
{
	if ((pipe_info.write_return = WriteFile(pipe_info.pipehd, pipe_info.write_msg, strlen(pipe_info.write_msg),	&pipe_info.message_size_written, NULL))	!= FALSE) 
	{
		if (pipe_info.message_size_written == (int)strlen(pipe_info.write_msg)) 
		{
			return(SUCCESS);
		}
	}

	//If we got this far then an error has happen so do the error routine.
	dodebug(WRITE_MSG_ERROR, "write_message()", NULL);
	dtb_info.dtb_errno = WRITE_MSG_ERROR;
	return(M9_ERROR);

}

/*
 *	read_message:		This function will read the message from the pipe into
 *							the PIPE structure, and perform error checking.
 *							
 *	Parameters:
 *		NONE			This function will use the concur_info structure and 
 *							dtb_info structure for its information.
 *		
 *	Returns:
 *		SUCCESS:		0    = The message was successfully written.
 *		M9_ERROR:		(-1) = The message was not successfully written.
 *
 */
int	read_message(void)
{
	if ((pipe_info.read_return = ReadFile(pipe_info.pipehd, pipe_info.read_msg, pipe_info.message_size, &pipe_info.message_size_read, NULL)) != FALSE) 
	{
		if (pipe_info.message_size_read) 
		{
			pipe_info.read_msg[pipe_info.message_size_read] = '\0';
			return(SUCCESS);
		}
	}

	//If we got this far then an error has happen so do the error routine.
	dodebug(READ_MSG_ERROR, "read_message()", NULL);
	dtb_info.dtb_errno = READ_MSG_ERROR;
	return(M9_ERROR);
}

/*
 *	get_response:		This function will read the message from the pipe and
 *							compare it to the allowed messages and will return
 *							the index of the compare.
 *							
 *	Parameters:
 *		NONE			This function will use the pipe_info structure
 *		
 *	Returns:
 *		SUCCESS:		There was a successfull read.
 *		M9_ERROR:		The read failed.
 *
 */

int get_response(void)
{
	int		index;
	char	*read_response[] = {
				"",
				"QUIT",
				"STATUS",
				"BYE",
				"SHUTITDOWN",
				NULL };

	if (read_message()) 
	{
		return(M9_ERROR);
	}

	for (index = 0; read_response[index] != NULL; index++)
	{
		if (!_stricmp(pipe_info.read_msg, read_response[index])) 
		{
			break;
		}
	}

	pipe_info.message_value = index;
	return(SUCCESS);

}

/*
 *	respond_to_controlling_process:		This function will read the messages from the pipe and
 *										from the command will perform the required task.
 *							
 *	Parameters:
 *		NONE			This function will use the pipe_info structure
 *		
 *	Returns:
 *		None:
 *
 */
void respond_to_controlling_process(void)
{

	int		loop_for_another_reading = TRUE;
	int		bye_was_received = FALSE;
	int		quit_was_received = FALSE;
	int		reset_it_back = FALSE;

	/*Loop until we either are killed by the m910nam.exe or we receive the ascii 
	string "QUIT".  Also we are	looking for the string "BYE", this is when we finish 
	with the m910nam.exe on	the startup of this program.  When we receive "BYE" we 
	will disconnect from the named pipe and then reconnect to the named pipe, where 
	we will go into a wait state until the m910nam restarts so that it can tell us 
	to quit.*/
	while (loop_for_another_reading) 
	{
		if (get_response()) 
		{
			loop_for_another_reading = FALSE;
		}

		switch(pipe_info.message_value) 
		{
			DWORD	dw;
			case BYE:
				bye_was_received = TRUE;
				break;

			case STATUS:
				if (dtb_info.dtb_errno == M9_ERROR)
				{
					dtb_info.dtb_errno = BURST_PASSED;
					reset_it_back++;
				}

				return_status();

				if (reset_it_back) 
				{
					dtb_info.dtb_errno = M9_ERROR;
				}

				if (quit_was_received) 
				{
					loop_for_another_reading = FALSE;
					break;
				}

				if (!bye_was_received) 
				{
					break;
				}

				Sleep(200);

				if (!DisconnectNamedPipe(pipe_info.pipehd)) 
				{
					if ((dw = GetLastError()) != ERROR_PIPE_CONNECTED) 
					{
						char	tmpstring[1024];
						sprintf(tmpstring, "ConnectNamedPipe return'd %ul", dw);
						dodebug(0, "get_pipe_hd()", tmpstring);
						return;
					}
				}

				Sleep(100);

				if (!ConnectNamedPipe(pipe_info.pipehd, NULL)) 
				{
					if ((dw = GetLastError()) != ERROR_PIPE_CONNECTED) 
					{
						char	tmpstring[1024];
						sprintf(tmpstring, "ConnectNamedPipe return'd %ul", dw);
						dodebug(0, "get_pipe_hd()", tmpstring);
						return;
					}
				}
				bye_was_received = FALSE;
				break;

			case QUIT:

				quit_was_received = TRUE;
				break;

			case SHUTITDOWN:

				halt_running_dtb();
				dtb_info.pin_state = FALSE;
				break;				

			default:

				err_info.error_code = 0;
				sprintf(err_info.func_name, "%s", "perform_concurrent_op()");
				sprintf(err_info.error_string, "%s", "Illegal Command");
				break;

		}
	}
}