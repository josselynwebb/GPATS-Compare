/////////////////////////////////////////////////////////////////////////////
//	File:	message.cpp														/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of etmnam to implement Iads ver 3.2	/
//					software.												/
//		2.0.1.0		setup_write_msg()										/
// 					modified the way the sprintf() call was being used,		/
// 					this to prevent the possibility of not terminating the	/
// 					character string.										/
// 					format_send_message()									/
//					changed the way the multiple character arrays were		/
//					being zero'd/initialized.								/
//		3.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/ 
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////	

#include <windows.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
#include "etmnam.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Static Storage															/
/////////////////////////////////////////////////////////////////////////////
 
SEND_INFO SendInfo[] = {

/*
Number	int or	Command		Mesage				End				Format
  Of	char	To
Values	values	Send		 Type				Message			Type		*/
{  0,	NULL,	NULL,		BYE_MSG,			END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		FILE_MSG,			END_FILE,		STRING_TYPE	},
{  0,	NULL,	NULL,		NONE_MSG,			END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		QUIT_MSG,			END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		READ_MSG,			END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		ZOOM_MSG,			END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		READER_MSG,			END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		CONCURRENT_MSG,		END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		STATUS_MSG,			STATUS_END,		NONE_TYPE	},
{  0,	NULL,	NULL,		TARGET_MSG,			END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		WINDOW_MSG,			WINDOW_END,		DIGIT_TYPE	},
{  0,	NULL,	NULL,		OPTIONS_MSG,		OPTION_END,		NONE_TYPE	},
{  0,	NULL,	NULL,		SUCCESS_MSG,		END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		ILLEGAL_MSG,		END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		CONSECUTIVE_MSG,	END_MSG,		NONE_TYPE	},
{  0,	NULL,	NULL,		REFERENCE_MSG,		REFERENCE_END,	STRING_TYPE	},
{  0,	NULL,	NULL,		FUNCTIONAL_MSG,		END_MSG,		STRING_TYPE	},
{  0,	NULL,	NULL,		OPERATIONAL_MSG,	END_MSG,		STRING_TYPE	},
{  0,	NULL,	NULL,		NULL,				NULL,			NONE_TYPE	},
};

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////
 
//
//	RequestResponse:		This function First NULL the message buffer, then
//								send the message. Next check the read message
//								buffer to see if it equals what is in
//								msgToRecv.  If it doesn't then send
//								wrongMsgRecvMsg, and try one more time.
//								If still not correct message being sent then set
//								the error code and close the pipe handle.
//							
//	Parameters:
//		msgToSend:			This is the message that will be sent.
//		msgToRecv:			This is the message that we expect to receive.
//		wrongMsgRecvMsg:	This is the message if the wrong message is received
//		
//	Returns:
//		SUCCESS:		  0  = The message was successfully received.
//		NAM_ERROR:		(-1) = The message was not successfully received.
//
//

int RequestResponse(char *msgToSend, char *msgToRecv, char *wrongMsgRecvMsg)
{
	
	dodebug(0,"RequestResponse()","Entering function in message class", (char*)NULL);
	SetupWriteMsg(PipeInfo.WriteMsg, msgToSend);

	if (WriteMessage()) {

		if (WriteMessage()) {
			CloseHandle(PipeInfo.Pipehd);
			return(NAM_ERROR);
		}
	}

	if (ReadMessage()) {

		if (ReadMessage()) {
			CloseHandle(PipeInfo.Pipehd);
			return(NAM_ERROR);
		}
	}

	if (_strnicmp(PipeInfo.ReadMsg, msgToRecv, PipeInfo.ReadMsgSize)) {

		SetupWriteMsg(PipeInfo.WriteMsg, wrongMsgRecvMsg);

		WriteMessage();

		SetupWriteMsg(PipeInfo.WriteMsg, msgToSend);

		WriteMessage();

		ReadMessage();

		if (_strnicmp(PipeInfo.ReadMsg, msgToRecv, PipeInfo.ReadMsgSize)) {
			Sleep(200);

			EtmInfo.ReturnValue = CONCURRENT_ERROR;
			CloseHandle(PipeInfo.Pipehd);
			return(NAM_ERROR);
		}
	}
	
	dodebug(0,"RequestResponse()","Leaving function in message class", (char*)NULL);
	return(SUCCESS);
}
	
//
//	WriteMessage:		This function will write out the message that is in the
//						PipeInfo structure and perform error checking on it.
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
	
	dodebug(0,"WriteMessage()","Entering function in message class", (char*)NULL);
	if ((PipeInfo.WriteReturn = WriteFile(PipeInfo.Pipehd,
											PipeInfo.WriteMsg,
											strlen(PipeInfo.WriteMsg),
											&PipeInfo.WriteMsgSize,
											NULL)) != FALSE) {

		if (PipeInfo.WriteMsgSize == (unsigned int)strlen(PipeInfo.WriteMsg)) {
			return(SUCCESS);
		}
	}

	// If we got this far then an error has happen so do the error routine.
	dodebug(WRITE_MSG_ERROR, "WriteMessage()", (char *)NULL, (char *)NULL);
	EtmInfo.ReturnValue = WRITE_MSG_ERROR;
	dodebug(0,"WriteMessage()","Leaving function in message class", (char*)NULL);
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
//		SUCCESS:		  0  = The message was successfully written.
//		NAM_ERROR:		(-1) = The message was not successfully written.
//
//

int	ReadMessage(void)
{
	
	dodebug(0,"ReadMessage()","Entering function in message class", (char*)NULL);
	MessageUtility(NULL_IT, PipeInfo.ReadMsg, (char *)NULL);

	if ((PipeInfo.ReadReturn = ReadFile(PipeInfo.Pipehd, PipeInfo.ReadMsg,
										  PipeInfo.MessageSize,
										  &PipeInfo.ReadMsgSize, NULL))
										  != FALSE) {

		if (PipeInfo.ReadMsgSize) {
			PipeInfo.ReadMsg[PipeInfo.ReadMsgSize] = '\0';
			return(SUCCESS);
		}
	}

// If we got this far then an error has happen so do the error routine.
	dodebug(READ_MSG_ERROR, "ReadMessage()", (char *)NULL, (char *)NULL);
	EtmInfo.ReturnValue = READ_MSG_ERROR;
	dodebug(0,"ReadMessage()","Leaving function in message class", (char*)NULL);
	return(NAM_ERROR);

}

//
//	MessageUtility:		This function will perform the action of the
//								variable action to the variable buffer.
//							
//	Parameters:
//		action:			NULL_IT = Null the buffer.
//						END_IT  = Terminate the buffer to the size of the
//								  variable stringInBuffer.
//		buffer:			The variable that the action is to happen to.
//		stringInBuffer:	This is used for setting the NULL at the buffer's end.
//		
//	Returns:
//		none:
//
//

void MessageUtility(int action, char *buffer, char *stringInBuffer)
{
	
	dodebug(0,"MessageUtility()","Entering function in message class", (char*)NULL);
	if (action == NULL_IT) {

		buffer[0] = '\0';
	}
	else if (action == END_IT) {

		buffer[strlen(stringInBuffer)] = '\0';
	}
	else {
		dodebug(0, "MessageUtility()", "Wrong action sent %d", action);
		EtmInfo.ReturnValue = NAM_ERROR;
	}
	dodebug(0,"MessageUtility()","Leaving function in message class", (char*)NULL);
}

//
//	SetupWriteMsg:	This function will insure the buffer that is used to
//							send a message to the other process is properly
//							formatted.  First the buffer will be null'd, then
//							the message to be sent will be inserted into the
//							buffer and then the buffer is insured to be
//							properly terminated.
//							
//	Parameters:
//		buffer:			The variable that will hold the message to be sent.
//		stringToSend:	The message that needs to be sent.
//		
//	Returns:
//		none:
//
//

void SetupWriteMsg(char *buffer, char *stringToSend)
{
	
	dodebug(0,"SetupWriteMsg()","Entering function in message class", (char*)NULL);
	MessageUtility(NULL_IT, buffer, (char *)NULL);

	sprintf(buffer, "%s", stringToSend);

	MessageUtility(END_IT, buffer, stringToSend);
	dodebug(0,"SetupWriteMsg()","Leaving function in message class", (char*)NULL);
}

//
//	StartupStatus:		This function will request the status of the other
//							program either after it has been executed and
//							established a connection or just established a
//							connection.  After the status is received from the
//							process it will be examined to see if the process
//							is working proerly.
//							
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = The proper mesage was successfully received.
//		NAM_ERROR:		(-1) = The proper message was not successfully received.
//
//

int	StartupStatus(void)
{
	
	dodebug(0,"StartupStatus()","Entering function in message class", (char*)NULL);
	if (RequestResponse(STATUS_MSG, SUCCESS_MSG, BYE_MSG)) {
		EtmInfo.ReturnValue = NAM_ERROR;
		return(NAM_ERROR);
	}
	dodebug(0,"StartupStatus()","Leaving function in message class", (char*)NULL);

	return(SUCCESS);
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
	
	dodebug(0,"SendMessage()","Entering function in message class", (char*)NULL);
	SetupWriteMsg(PipeInfo.WriteMsg, messageToSend);
	
	if (WriteMessage()) {

		EtmInfo.ReturnValue = SUCCESS;
		
		if (WriteMessage()) {
			CloseHandle(PipeInfo.Pipehd);
			return(NAM_ERROR);
		}
	}
	
	dodebug(0,"SendMessage()","Leaving function in message class", (char*)NULL);
	return(SUCCESS);
}

//
//	GetResponse:		This function will read the message from the PipeInfo
//							structure and compare it to the allowed messages and
//							will return the index of the compare.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = There was a successfull read.
//		NAM_ERROR:		(-1) = The read failed.
//
//
int GetResponse(void)
{
	int		index;	
	char	*ReadResponse[] = {
		BYE_MSG,
		FILE_MSG,
		NONE_MSG,
		QUIT_MSG,
		READ_MSG,
		ZOOM_MSG,
		READER_MSG,
		CONCURRENT_MSG,
		STATUS_MSG,
		TARGET_MSG,
		WINDOW_MSG,
		OPTIONS_MSG,
		SUCCESS_MSG,
		ILLEGAL_MSG,
		CONSECUTIVE_MSG,
		REFERENCE_MSG,
		FUNCTIONAL_MSG,
		OPERATIONAL_MSG,
		NULL };

	dodebug(0,"GetResponse()","Entering function in message class", (char*)NULL);
	if (ReadMessage()) {
		return(NAM_ERROR);
	}

	for (index = 0; ReadResponse[index] != NULL; index++) {
		if (!_stricmp(PipeInfo.ReadMsg, ReadResponse[index])) {
			break;
		}
	}

	if (ReadResponse[index] == NULL) {
		EtmInfo.ReturnValue = NAM_ERROR;
		return(NAM_ERROR);
	}

	PipeInfo.MessageValue = index;
	
	dodebug(0,"GetResponse()","Leaving function in message class", (char*)NULL);
	return(SUCCESS);
}

//
//	FormatSendMessage:	This function will format the message that needs to
//							be sent utilizing the SendInfo structure.  This
//							function will use the variable cmd to index into the
//							structure for the data that needs to be sent.  There
//							are three different ways that the message needs to
//							be structure depending on the formatType element of
//							the structure.
//							
//	Parameters:
//		cmd:			Contains the index value of the SendInfo structure.
//		
//	Returns:
//		SUCCESS:		  0  = The message was successfully written.
//		NAM_ERROR:		(-1) = The message was not successfully written.
//
//

int FormatSendMessage(int cmd)
{

	int		i;
	char	tmpSprintfBuf[NAM_MAX_PATH];
	char	tmpFormatBuf[NAM_MAX_PATH];
	
	dodebug(0,"FormatSendMessage()","Entering function in message class", (char*)NULL);
	memset(tmpSprintfBuf, '\0', sizeof(tmpSprintfBuf));
	memset(tmpFormatBuf, '\0', sizeof(tmpFormatBuf));

	/* Check for the messages that will have digits in them.  Will first
	put in the mesage type, then the digits, then finally the ending message.*/
	if (SendInfo[cmd].formatType == DIGIT_TYPE) {

		sprintf(tmpSprintfBuf, "%s %d", SendInfo[cmd].msgType,
				SendInfo[cmd].u.intValue[0]);

		strcpy(tmpFormatBuf, tmpSprintfBuf);

		if (SendInfo[cmd].numOfVals > 1) {

			for (i = 1; i < SendInfo[cmd].numOfVals; i++) {

				memset(tmpSprintfBuf, '\0', sizeof(tmpSprintfBuf));
				sprintf(tmpSprintfBuf, ", %d", SendInfo[cmd].u.intValue[i]);
				strcat(tmpFormatBuf, tmpSprintfBuf);
			}
		}

		strcat(tmpFormatBuf, " ");
		strcat(tmpFormatBuf, SendInfo[cmd].endString);

		if ((SendInfo[cmd].cmdToSend = _strdup(tmpFormatBuf)) == NULL) {
			dodebug(0, "FormatSendMessage()",
					"Failed to allocate memory", (char *)NULL);
			EtmInfo.ReturnValue = NAM_ERROR;
			return(NAM_ERROR);
		}
	}


	/*If not DIGIT_TYPE then check for STRING_TYPE.  If STRING_TYPE do the same 
	as DIGIT_TYPE, except for the union portion.  Also for right now there is only 
	1 element for the STRING_TYPE, but went ahead and made it where it could except more.*/
 	else if (SendInfo[cmd].formatType == STRING_TYPE) {

		sprintf(tmpSprintfBuf, "%s %s ", SendInfo[cmd].msgType,
				SendInfo[cmd].u.charValue[0]);

		strcpy(tmpFormatBuf, tmpSprintfBuf);

		if (SendInfo[cmd].numOfVals > 1) {

			for (i = 1; i < SendInfo[cmd].numOfVals; i++) {

				memset(tmpSprintfBuf, '\0', sizeof(tmpSprintfBuf));
				sprintf(tmpSprintfBuf, ", %s", SendInfo[cmd].u.charValue[i]);
				strcpy(tmpFormatBuf, tmpSprintfBuf);
			}
		}

		strcat(tmpFormatBuf, SendInfo[cmd].endString);

		if ((SendInfo[cmd].cmdToSend = _strdup(tmpFormatBuf)) == NULL) {
			dodebug(0, "FormatSendMessage()",
					"Failed to allocate memory", (char *)NULL);
			EtmInfo.ReturnValue = NAM_ERROR;
			return(NAM_ERROR);
		}
	}


	/*The final one should be of NONE_TYPE.  To format the string sprint the 
	different elements into the buffer, then into cmdToSend.*/
	else if (SendInfo[cmd].formatType == NONE_TYPE) {

		sprintf(tmpSprintfBuf, "%s %s",
				SendInfo[cmd].msgType, SendInfo[cmd].endString);

		strcpy(tmpFormatBuf, tmpSprintfBuf);

		if ((SendInfo[cmd].cmdToSend = _strdup(tmpFormatBuf)) == NULL) {
			dodebug(0, "FormatSendMessage()",
			"Failed to allocate memory", (char *)NULL);
			EtmInfo.ReturnValue = NAM_ERROR;
			return(NAM_ERROR);
		}
	}

	// If it gets here, report error
	else {
		dodebug(0, "FormatSendMessage()",
				"Illegal cmd type passed", (char *)NULL);
		EtmInfo.ReturnValue = NAM_ERROR;
		return(NAM_ERROR);
	}

	if (SendMessage(SendInfo[cmd].cmdToSend)) {
		return(NAM_ERROR);
	}
	
	dodebug(0,"FormatSendMessage()","Leaving function in message class", (char*)NULL);
	return(SUCCESS);	
}

//
//	FreeItem:		This function will check the variable passed to it to see if
//						is empty or not.  If the variable is not empty then the
//						function will free the memory that was allocated for it.
//							
//	Parameters:
//		item:		The variable that needs to have the memory freed that was
//					allocated for it.
//		
//	Returns:
//		none:
//
//

void FreeItem(char *item)
{
	
	dodebug(0,"FreeItem()","Entering function in message class", (char*)NULL);
	if (item != NULL) {
		free(item);
	}
	dodebug(0,"FreeItem()","Leaving function in message class", (char*)NULL);
}
