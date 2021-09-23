/////////////////////////////////////////////////////////////////////////////
//	File:	message.cpp														/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily				/
//					WriteMessage()											/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					ReadMessage()											/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					GetResponse()											/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					RespondToControllingProcess()							/
//					Changed the way the variables are written				/
//					(camel back style).										/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include "gpconcur.h"
#include "pipe.h"

/////////////////////////////////////////////////////////////////////////////
//		External Variables and Routines										/
/////////////////////////////////////////////////////////////////////////////

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
//	WriteMessage:		This function will write out the message that is in	/
//							the PIPE_INFO structure and perform error		/
//							checking on it.									/
//																			/
//	Parameters:																/
//		NONE			This function will use the GPConInfo structure and	/
//							PipeInfo structure for its information.			/
//																			/
//	Returns:																/
//		SUCCESS:		  0  = The message was successfully written.		/
//		GP_ERROR:		(-1) = The message was not successfully written.	/
//																			/
/////////////////////////////////////////////////////////////////////////////

int WriteMessage(void)
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

	DoDebug(WRITE_MSG_ERROR, "WriteMessage()", (char*)NULL);
	GPConInfo.GPErrno = WRITE_MSG_ERROR;
	return(GP_ERROR);

}

/////////////////////////////////////////////////////////////////////////////
//	ReadMessage:		This function will read the message from the pipe	/
//							into the PIPE structure, and perform error		/
//							checking.										/
//																			/
//	Parameters:																/
//		NONE			This function will use the GPConInfo structure and	/
//							PipeInfo structure for its information.			/
//																			/
//	Returns:																/
//		SUCCESS:		0    = The message was successfully written.		/
//		GP_ERROR:		(-1) = The message was not successfully written.	/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	ReadMessage(void)
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

	DoDebug(READ_MSG_ERROR, "ReadMessage()", (char*)NULL);
	GPConInfo.GPErrno = READ_MSG_ERROR;
	return(GP_ERROR);

}

/////////////////////////////////////////////////////////////////////////////
//	GetResponse:		This function will read the message from the		/
//							pipe_infp structure	and compare it to the		/
//							allowed messages and will return the Index of	/
//							the compare.									/
//																			/
//	Parameters:																/
//		NONE			This function will use the PipeInfo structure		/
//																			/
//	Returns:																/
//		SUCCESS:		  0  = There was a successfull read.				/
//		GP_ERROR:		(-1) = The read failed.								/
//																			/
/////////////////////////////////////////////////////////////////////////////

int GetResponse(void)
{

	int		Index;
	char	*ReadResponse[] = {
				"",
				"QUIT",
				"STATUS",
				"DOIT",
				NULL };

	if (ReadMessage()) {

		return(GP_ERROR);
	}

	for (Index = 0; ReadResponse[Index] != NULL; Index++) {
		if (!_stricmp(PipeInfo.ReadMsg, ReadResponse[Index])) {
			break;
		}
	}

	PipeInfo.MessageValue = Index;

	return(SUCCESS);

}

/////////////////////////////////////////////////////////////////////////////
//	RespondToControllingProcess:	This function will read the messages	/
//										from the pipe and from the command	/
//										will perform the required task.		/
//																			/
//	Parameters:																/
//		NONE			This function will use the ErrInfo structure		/
//																			/
//	Returns:																/
//		None:																/
//																			/
/////////////////////////////////////////////////////////////////////////////

void RespondToControllingProcess(void)
{

	int		LoopForAnotherReading = TRUE;
	int		QuitWasReceived = FALSE;

//
// Well we got the pipe handle so now we will go into a loop until we either are
// killed by the m910nam.exe or we receive the ascii string "QUIT".  Also we are
// looking for the string "BYE", this is when we finish with the m910nam.exe on
// the startup of this program.  When we receive "BYE" we will disconnect from the
// named pipe and then reconnect to the named pipe, where we will go into a wait
// state until the m910nam restarts so that it can tell us to quit or whatever.
//

	while (LoopForAnotherReading) {

		if (GetResponse()) {

			LoopForAnotherReading = FALSE;
		}

		switch(PipeInfo.MessageValue) {

			case STATUS:

				ReturnStatus();

				if (QuitWasReceived) {

					LoopForAnotherReading = FALSE;

					break;
				}

			break;

			case QUIT:

				QuitWasReceived = TRUE;

			break;

			case DOIT:

				StartProcess();

			break;

			default:

				ErrInfo.ErrorCode = 0;
				sprintf(ErrInfo.FuncName, "%s", "RespondToControllingProcess()");
				sprintf(ErrInfo.ErrorString, "%s", "Illegal Command");

			break;

		}
	}
}