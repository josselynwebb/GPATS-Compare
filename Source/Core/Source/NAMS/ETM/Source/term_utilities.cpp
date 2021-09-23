/////////////////////////////////////////////////////////////////////////////
//	File:	term_utilities.cpp												/
//																			/
//	Creation Date:	10 Jan 2005												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0	Complete rebuild of etmnam to implement Iads ver 3.2.7		/
//				software.													/
// 		2.0.1.0	perform_terminate()											/
// 				modified the way the character array is being zero'd or		/
// 				initialized.												/
//		3.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/ 
//  																		/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>
#include <string.h>
#include "etmnam.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// PerformTerminate:	This program will first insure that termination is what
//							the operator wants.  Next it will resolve both the
//							terminate and terminate all into TERMINATE_ALL.
//							Then get a pipe handle and the status of the
//							monitoring process. Finally call the send terminate
//							function.
//
// Parameters:
//		type:		This is the type of termination that is tp be performed.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//
//

int PerformTerminate(char *type)
{
	/* Check to see what was sent to us and if it is legal.  If TERMINATE was sent
	will change it to TERMINATE_ALL.*/	
	dodebug(0,"PerformTerminate()","Entering function in term_utilities class", (char*)NULL);
	if ((!_stricmp(type, "TERMINATE"))          ||
		(!_stricmp(type, "TERMINATE_ALL"))      ||
		(!_stricmp(type, "TERMINATE_ZOOMVIEW")) ||
		(!_stricmp(type, "TERMINATE_READER"))) {
		
		if (!_stricmp(type, "TERMINATE")) {
			memset(EtmInfo.TermType, '\0', sizeof(EtmInfo.TermType));
			sprintf(EtmInfo.TermType, "%s", "TERMINATE_ALL");
		}
	}
	else {
		dodebug(0, "PerformTerminate()", "Improper terminate sent %s",
				EtmInfo.TermType);
		EtmInfo.ReturnValue = NAM_ERROR;
		return(NAM_ERROR);
	}
	
	if (GetPipeHd()) {
		return(NAM_ERROR);
	}

	if (StartupStatus()) {
		return(NAM_ERROR);
	}

	if (SendTerminate(EtmInfo.TermType)) {
		return(NAM_ERROR);
	}
	
	dodebug(0,"PerformTerminate()","Leaving function in term_utilities class", (char*)NULL);
	return(SUCCESS);
}

//
// SendTerminate:	This program will first send the proper terminate mesage to
//						the monitoring process, then request the status back
//						from the process, then send the closing message to the
//						process, then finally get the final status message.
//
// Parameters:
//		type:		This is the type of termination that is tp be performed.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//
//

int SendTerminate(char *type)
{
	int		HadError = FALSE;	
	
	dodebug(0,"SendTerminate()","Entering function in term_utilities class", (char*)NULL);
	/* See if we can get a handle to the communication pipe between the 
	2 process.*/
	if (!SendMessage(type)) {

		if (!RequestResponse(STATUS_MSG, SUCCESS_MSG, ILLEGAL_MSG)) {

			if (!SendMessage(BYE_MSG)) {

				if (RequestResponse(STATUS_MSG, SUCCESS_MSG, ILLEGAL_MSG)) {
					HadError++;
				}
			}
			else {
				HadError++;
			}
		}
		else {
			HadError++;
		}
	}
	else {	
		HadError++;
	}

	CloseHandle(PipeInfo.Pipehd);

	EtmInfo.ReturnValue = HadError != FALSE ?
							NAM_ERROR : EtmInfo.ReturnValue;
	
	dodebug(0,"SendTerminate()","Leaving function in term_utilities class", (char*)NULL);
	return(HadError != FALSE ? NAM_ERROR : SUCCESS);
}
