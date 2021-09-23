/////////////////////////////////////////////////////////////////////////////
//	File:	Message.cpp														/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
// 		1.0.1.0		send_message()											/
// 					added the size of the character array to the function	/
// 					call setup_write_msg(), which allows the called function/
// 					to zero the passed buffer.								/
// 					setup_write_msg()										/
// 					added the int to the function call, this allows for the	/
// 					passed buffer to be zero'd before use. Also the function/
// 					call message_utility() has the int being added to it to	/
// 					allow for the zeroing capability. Modified the sprintf	/
// 					call to insure proper termination of the character		/
// 					array buffer.											/
// 					message_utility()										/
// 					added the int to the function call, this allows for the /
// 					passed buffer to be zero'd before use. Modified the way	/
// 					character array is being zero'd. Plus modified the		/
// 					statement that terminated the character array buffer.	/
//		1.0.2.0		RespondToControllingProcess()							/
//					Corrected the debug statement, added (char*) to			/
//					terminate the debug statement properly.					/
//					MessageUtility()										/
//					Corrected the debug statement, added (char*) to			/
//					terminate the debug statement properly.					/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>
#include <string.h>
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
//	MessageRequest:	This function will send the requested message, then get
//							the requested answer from the controlling process.
//							
//	Parameters:
//		message			This is the requesting message.
//		
//	Returns:
//		SUCCESS:		  0  = The message was successfully written.
//		NAM_ERROR:		(-1) = The message was not successfully written.
//
//

int MessageRequest(char *message)
{
	
	dodebug(0,"MessageRequest()","Entering function in Message class", (char*)NULL);
	sprintf(PipeInfo.WriteMsg, "%s", message);
	PipeInfo.WriteMsg[strlen(message)] = '\0';

	if (WriteMessage()) {
		return(NAM_ERROR);
	}

	if (ReadMessage()) {

		return(NAM_ERROR);
	}
	
	dodebug(0,"MessageRequest()","Leaving function in Message class", (char*)NULL);
	return(SUCCESS);
}
	
//
//	WriteMessage:		This function will write out the message that is in the
//							PipeInfo structure.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = The message was successfully written.
//		NAM_ERROR:		(-1) = The message was not successfully written.
//
//

int WriteMessage(void)
{
	dodebug(0,"WriteMessage()","Entering function in Message class", (char*)NULL);

	if ((PipeInfo.WriteReturn = WriteFile(PipeInfo.Pipehd,
											PipeInfo.WriteMsg,
											strlen(PipeInfo.WriteMsg),
											&PipeInfo.MessageSizeWritten,
											NULL))!= FALSE) {

		if(PipeInfo.MessageSizeWritten == (int)strlen(PipeInfo.WriteMsg)) {
			return(SUCCESS);
		}
	}

	// If we got this far then an error has happen so do the error routine.
	dodebug(WRITE_MSG_ERROR, "WriteMessage()", (char*)NULL, (char*)NULL);
	EtmInfo.EtmErrno = WRITE_MSG_ERROR;
	dodebug(0,"WriteMessage()","Leaving function in Message class", (char*)NULL);
	return(NAM_ERROR);
}

//
//	ReadMessage:		This function will read the message from the pipe into
//							the PIPE structure, and perform error checking.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		0    = The message was successfully written.
//		NAM_ERROR:		(-1) = The message was not successfully written.
//
//

int	ReadMessage(void)
{
	
	dodebug(0,"ReadMessage()","Entering function in Message class", (char*)NULL);
	if ((PipeInfo.ReadReturn = ReadFile(PipeInfo.Pipehd, PipeInfo.ReadMsg,
										  PipeInfo.MessageSize,
										  &PipeInfo.MessageSizeRead, NULL))
										  != FALSE) {

		if (PipeInfo.MessageSizeRead) {
			PipeInfo.ReadMsg[PipeInfo.MessageSizeRead] = '\0';
			return(SUCCESS);
		}
	}

	// If we got this far then an error has happen so do the error routine.
	dodebug(READ_MSG_ERROR, "ReadMessage()", (char*)NULL, (char*)NULL);
	EtmInfo.EtmErrno = READ_MSG_ERROR;
	dodebug(0,"ReadMessage()","Leaving function in Message class", (char*)NULL);
	return(NAM_ERROR);
}

//
//	GetResponse:		This function will read the message from the pipe_infp
//							structure and compare it to the allowed messages and
//							 will return the index of the compare.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int GetResponse(void)
{

	int		index;
	char	*ReadResponse[] = {
				FILE_MSG,
				STATUS_MSG,
				WINDOW_MSG,
				OPTIONS_MSG,
				REFERENCE_MSG,
				FUNCTIONAL_MSG,
				OPERATIONAL_MSG,
				BYE_MSG,
				NONE_MSG,
				QUIT_MSG,
				ZOOM_MSG,
				READ_MSG,
				CONCURRENT_MSG,
				TARGET_MSG,
				SUCCESS_MSG,
				ILLEGAL_MSG,
				CONSECUTIVE_MSG,
				TERMINATE_ALL_MSG,
				TERMINATE_READER_MSG,
				TERMINATE_ZOOM_MSG,
				NULL };
	
	dodebug(0,"GetResponse()","Entering function in Message class", (char*)NULL);
	if (ReadMessage()) {
		return(NAM_ERROR);
	}

	for (index = 0; ReadResponse[index] != NULL; index++) {
		if (!_stricmp(PipeInfo.ReadMsg, ReadResponse[index])) {
			break;
		}
	}

	PipeInfo.MessageValue = index;
	
	dodebug(0,"GetResponse()","Leaving function in Message class", (char*)NULL);
	return(SUCCESS);

}

//
//	RespondToControllingProcess:		This function will read the messages
//										from the pipe and from the message will
//										perform the required task.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		none:
//
//

void RespondToControllingProcess(void)
{

	int		LoopForAnotherReading = TRUE;
	int		ByeWasReceived = FALSE;
	int		QuitWasReceived = FALSE;
	
	dodebug(0,"RespondToControllingProcess()","Entering function in Message class", (char*)NULL);
	/* Loop until we are killed by the controlling process.  When receive the string "BYE",
	this is when we finish with the controlling process on the startup of this program.  After 
	receiving "BYE" disconnect from the the named pipeand then reconnect to the named pipe, then 
	go into a wait state until the controlling process restarts.*/
	while (LoopForAnotherReading) {

		if (GetResponse()) {

			LoopForAnotherReading = FALSE;
		}

		switch(PipeInfo.MessageValue) {

			DWORD	dw;

			case BYE_CMD:

				ByeWasReceived = TRUE;

			break;

			case STATUS_CMD:

				ReturnStatus();

				if (QuitWasReceived) {

					LoopForAnotherReading = FALSE;

					break;
				}

				if (!ByeWasReceived) {

					break;
				}

				Sleep(200);

				if (!DisconnectNamedPipe(PipeInfo.Pipehd)) {

					if ((dw = GetLastError()) != ERROR_PIPE_CONNECTED) {
						dodebug(0, "RespondToControllingProcess()",	"DisconnectNamedPipe return'd %ul", dw, (char*)NULL);  
						return;
					}
				}

				Sleep(100);

				if (EtmInfo.Concurrent == TRUE) {
					HWND	wHnd;
					wHnd = EtmInfo.Mode == ZOOM ?
					WinInfo.ImageWhndl : WinInfo.ReadWhndl;

					Sleep(100);
					ShowWindow(wHnd, SW_RESTORE);
				}

				if (!ConnectNamedPipe(PipeInfo.Pipehd, NULL)) {

					if ((dw = GetLastError()) != ERROR_PIPE_CONNECTED) {
						dodebug(0, "RespondToControllingProcess()",	"ConnectNamedPipe return'd %ul", dw, (char*)NULL);  
						return;
					}
				}

				ByeWasReceived = FALSE;

			break;

			case QUIT_CMD:

				QuitWasReceived = TRUE;

			break;

			case READER_CMD:

				if (GetMode(OPERATIONAL)) {
					break;
				}

				PerformReaderFunction();

			break;

			case ZOOM_CMD:

				if (GetMode(OPERATIONAL)) {
					break;
				}

				EtmInfo.Mode = ZOOM;

				PerformImagerFunction();

			break;

			case TERMINATE_ALL_CMD:

				if (ProcInfo.ReaderRunning) {
					TerminateTheReader();
				}

				if (ProcInfo.ImagerRunning) {
					TerminateTheImager();
				}

			break;

			case TERMINATE_READER_CMD:

				if (ProcInfo.ReaderRunning) {
					TerminateTheReader();
				}

			break;

			case TERMINATE_ZOOM_CMD:

				if (ProcInfo.ImagerRunning) {
					TerminateTheImager();
				}

			break;

			default:

				ErrInfo.ErrorCode = 0;
				sprintf(ErrInfo.FuncName, "%s", "RespondToControllingProcess()");
				sprintf(ErrInfo.ErrorString, "%s", "Illegal Command");

			break;

		}
	}
	dodebug(0,"RespondToControllingProcess()","Leaving function in Message class", (char*)NULL);
}

//
//	SendMessage:		This function will first insure that the mesage to be
//							sent is properly terminated and the buffer that it
//							will be sent from is null'd before the message is
//							inserted into it.  Then will try to send the message
//							two times before it errors out and calls it quit.
//							
//	Parameters:
//		messageToSend:		The buffer that holds the message to send.
//		
//	Returns:
//		SUCCESS:		  0  = The message was successfully written.
//		NAM_ERROR:		(-1) = The message was not successfully written.
//
//

int	SendMessage(char *messageToSend)
{
	
	dodebug(0,"SendMessage()","Entering function in Message class", (char*)NULL);
	SetupWriteMsg(PipeInfo.WriteMsg, messageToSend, sizeof(PipeInfo.WriteMsg));
	
	if (WriteMessage()) {

		EtmInfo.EtmErrno = SUCCESS;
		
		if (WriteMessage()) {
			CloseHandle(PipeInfo.Pipehd);
			return(NAM_ERROR);
		}
	}
	
	dodebug(0,"SendMessage()","Leaving function in Message class", (char*)NULL);
	return(SUCCESS);
}

//
//	SetupWriteMsg:		This function will insure the buffer that is used to
//							send a message to the other process is properly
//							formatted.  First the buffer will be null'd, then
//							the message to be sent will be inserted into the
//							buffer and then the buffer is insured to be
//							properly terminated.
//							
//	Parameters:
//		buffer:			The variable that will hold the message to be sent.
//		stringToSend:	The message that needs to be sent.
// 		bufferSize:		The size of the buffer
//		
//	Returns:
//		none:
//
//

void SetupWriteMsg(char *buffer, char *stringToSend, int bufferSize)
{
	
	dodebug(0,"SetupWriteMsg()","Entering function in Message class", (char*)NULL);
	MessageUtility(NULL_IT, buffer, bufferSize);

	sprintf(buffer, "%s", stringToSend);

	MessageUtility(END_IT, buffer, strlen(stringToSend));
	dodebug(0,"SetupWriteMsg()","Leaving function in Message class", (char*)NULL);
}

//
//	MessageUtility:		This function will perform the action of the
//								variable action to the variable buffer.
//							
//	Parameters:
//		action:				NULL_IT = Null the buffer.
//							END_IT  = Terminate the buffer to the size of the
//									  variable stringInBuffer.
//		buffer:				The variable that the action is to happen to.
// 		bufferSize:			Size of the buffer or the buffer string length.
//		
//	Returns:
//		none:
//
//

void MessageUtility(int action, char *buffer, int bufferSize)
{
	
	dodebug(0,"MessageUtility()","Entering function in Message class", (char*)NULL);
	if (action == NULL_IT) {

		memset(buffer, '\0', bufferSize);
	}
	else if (action == END_IT) {

		buffer[bufferSize] = '\0';
	}
	else {
		dodebug(0, "MessageUtility()", "Wrong action sent %d", action, (char*)NULL); 
		EtmInfo.EtmErrno = NAM_ERROR;
	}
	dodebug(0,"MessageUtility()","Leaving function in Message class", (char*)NULL);
}
