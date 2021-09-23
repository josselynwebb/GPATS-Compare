/****************************************************************************
 *	File:	message.c														*
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
	if ((pipe_info.write_return = WriteFile(pipe_info.pipehd, pipe_info.write_msg,strlen(pipe_info.write_msg),&pipe_info.message_size_written, NULL))!= FALSE) 
	{
		if (pipe_info.message_size_written == (int)strlen(pipe_info.write_msg))
		{
			return(SUCCESS);
		}
	}

	/*If we got this far then an error has happen so do the error routine.*/
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

	/*If we got this far then an error has happen so do the error routine.*/
	dodebug(READ_MSG_ERROR, "read_message()", NULL);
	dtb_info.dtb_errno = READ_MSG_ERROR;
	return(M9_ERROR);

}