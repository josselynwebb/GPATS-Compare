/////////////////////////////////////////////////////////////////////////////
//	File:	message.cpp														/
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

#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
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
//	write_message:		This function will write out the message that is in	/
//							the PIPE_INFO structure and perform error		/
//							checking on it.									/
//																			/
//	Parameters:																/
//		NONE			This function will use the pipe_info structure and	/
//							gp_info structure for its information.			/
//																			/
//	Returns:																/
//		SUCCESS:		  0  = The message was successfully written.		/
//		GP_ERROR:		(-1) = The message was not successfully written.	/
//																			/
/////////////////////////////////////////////////////////////////////////////

int write_message(void)
{

	if ((PipeInfo.WriteReturn = WriteFile(PipeInfo.Pipehd, PipeInfo.WriteMsg,
											strlen(PipeInfo.WriteMsg),
											&PipeInfo.MessageSizeWritten, NULL))
											!= FALSE) {

		if (PipeInfo.MessageSizeWritten == (int)strlen(PipeInfo.WriteMsg)) {
			return(SUCCESS);
		}
	}

//
// If we got this far then an error has happen so do the error routine.
//

	dodebug(WRITE_MSG_ERROR, "write_message()", NULL);
	gp_info.return_value = WRITE_MSG_ERROR;
	return(GP_ERROR);

}

/////////////////////////////////////////////////////////////////////////////
//	read_message:		This function will read the message from the pipe	/
//							into the PIPE structure, and perform error		/
//							checking.										/
//																			/
//	Parameters:																/
//		NONE			This function will use the pipe_info structure and	/
//							gp_info structure for its information.			/
//																			/
//	Returns:																/
//		SUCCESS:		  0  = The message was successfully written.		/
//		GP_ERROR:		(-1) = The message was not successfully written.	/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	read_message(void)
{

	if ((PipeInfo.ReadReturn = ReadFile(PipeInfo.Pipehd, PipeInfo.ReadMsg,
										  PipeInfo.MessageSize,
										  &PipeInfo.MessageSizeRead, NULL))
										  != FALSE) {

		if (PipeInfo.MessageSizeRead) {
			PipeInfo.ReadMsg[PipeInfo.MessageSizeRead] = '\0';
			return(SUCCESS);
		}
	}

//
// If we got this far then an error has happen so do the error routine.
//

	dodebug(READ_MSG_ERROR, "read_message()", NULL);
	gp_info.return_value = READ_MSG_ERROR;
	return(GP_ERROR);

}