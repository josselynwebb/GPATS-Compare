/////////////////////////////////////////////////////////////////////////////
//	File:	reader.cpp														/
//																			/
//	Creation Date:	10 Jan 2005												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0	Complete rebuild of etmnam to implement Iads ver 3.2.7		/
//				software.													/
//		3.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>
#include "etmnam.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// DoReadParse:	This program will parse through the argv list, checking
//						for proper usage and required task to perform the
//						reader capability.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//
//

int DoReadParse(int argc, char *argv[])
{
	/* Check if all the arguments have already been parsed.  
	Should expect that the file name and the frame reference have been
	parsed.*/
	dodebug(0,"DoReadParse()","Entering function in reader class", (char*)NULL);
	if (EtmInfo.NotAtlas == FALSE) {

		if (ParseArgumentsAtlas(argc, argv, READ_ONLY)) {
			return(NAM_ERROR);
		}
	}
	else {

		if (ParseArguments(argc, argv, READ_ONLY)) {
			return(NAM_ERROR);
		}
	}

	if (TestForFile()) {
		return(NAM_ERROR);
	}
	
	CorrectTargetFrame();

	if (FillInInfo(EtmInfo.Mode)) {
		return(NAM_ERROR);
	}
	
	dodebug(0,"DoReadParse()","Leaving function in reader class", (char*)NULL);
	return(SUCCESS);
}

//
// PerformRreader:	This program will first check to see if the monitoring
//						process is running, if not then start it up.  Then get
//						the status of the monitoring process, break it out of
//						the loop in the respond to controlling process function
//						by sending the READ_MSG, then finally send the reader
//						information as the monitoring process request it.
//
// Parameters:
//		none:
//
// Returns:
//		none:
//
//

void PerformReader(void)
{
	
	dodebug(0,"PerformReader()","Entering function in reader class", (char*)NULL);
	if (EtmInfo.ProcessRunning == FALSE) {
		if (StartUpTheProcess()) {
			return;
		}
	}
	else {
		if (GetPipeHd()) {

			if (TerminateTheProcess()) {
				EtmInfo.ReturnValue = NAM_ERROR;
				return;
			}

			EtmInfo.ReturnValue = NAM_ERROR;
		}
	}

	if (StartupStatus()) {
		return;
	}
	
	if (SendMessage(READ_MSG)) {
		return;
	}
	
	SendReaderInfo();
	
	if (SendMessage(BYE_MSG)) {
		return;
	}
	
	RequestResponse(STATUS_MSG, SUCCESS_MSG, QUIT_MSG);
	EtmInfo.ReturnValue = SUCCESS;
	dodebug(0,"PerformReader()","Leaving function in reader class", (char*)NULL);
	
	return;
}

//
// SendReaderInfo:	This program will keep looping through getting request
//							from the monitoring process until it receives the
//							BYE_CMD from the process.  When it receives the
//							BYE_CMD fro mthe process it will then exit the loop.
//							Until it gets the BYE_CMD it will call the function
//							format_send_message passing the CMD that needs to be
//							preformed.
//
// Parameters:
//		none:
//
// Returns:
//		none:
//
//

void SendReaderInfo(void)
{

	int		LoopForAnotherReading = TRUE;
	
	dodebug(0,"SendReaderInfo()","Entering function in reader class", (char*)NULL);
	while (LoopForAnotherReading) {

		if (GetResponse()) {
			break;
		}

		switch(PipeInfo.MessageValue) {

			case BYE_CMD:

				LoopForAnotherReading = FALSE;

			break;

			case OPERATIONAL_CMD:

				if (EtmInfo.Concurrent) {
					if (FormatSendMessage(CONCURRENT_CMD)) {
						return;
					}
				}
				else {
					if (FormatSendMessage(CONSECUTIVE_CMD)) {
						return;
					}
				}

			break;

			case FUNCTIONAL_CMD:

				if (FormatSendMessage(READER_CMD)) {
					return;
				}

			break;

			case REFERENCE_CMD:

			case FILE_CMD:

			case WINDOW_CMD:

				if (FormatSendMessage(PipeInfo.MessageValue)) {
					return;
				}

			break;

			default:

				EtmInfo.ReturnValue = NAM_ERROR;
				dodebug(0, "SendReaderInfo()", "PipeInfo.MessageValue = %d",
						PipeInfo.MessageValue);

			break;
		}
	}
	
	dodebug(0,"SendReaderInfo()","Leaving function in reader class", (char*)NULL);
	return;
}
