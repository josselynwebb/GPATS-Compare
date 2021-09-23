/////////////////////////////////////////////////////////////////////////////
//	File:	EtmMonitorMain.cpp												/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0	Assigned it a version number.								/
//		1.0.1.0	EtmMonitorMain()											/
//				changed the checking of argc from 1 to 2, due to the way the/
//				etm nam needs to exec the etm_monitor.  This was done to	/
//				allow for the non inheritance of open file handles.			/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <stdio.h>
#include <windows.h>
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"
#include "..\..\Common\Include\pipe.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

int				continueEnum;
ERR_INFO		ErrInfo;
ETM_INFO		EtmInfo;
WIN_INFO		WinInfo;
IMAG_INFO		ImagInfo;
PIPE_INFO		PipeInfo;
PROC_INFO		ProcInfo;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// EtmMonitorMain:	This program will check the variables that were passed
//						to it and if correct will set up the defaults and then
//						call the different functions in proper order.
//
// Parameters:
//		argc:		The # of arguments passed to concur_main.
//		argv:		Character list of the arguments passed to concur_main.
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int EtmMonitorMain (int argc, char *argv[])
{
	// Setup some defaults and initilize some variables to their defaults.
	PipeInfo.MessageSize	= MAX_MSG_SIZE + 24;
	PipeInfo.Pipehd		= NULL;
	
    //MessageBox(NULL, "EtmMonitorMain", "Note", MB_OK);  This is used for debugging purposes, do not remove
	dodebug(0,"EtmMonitorMain()","Entering function in EtmMonitorMain class", (char*)NULL);
	// Check to see if the proper # of arguments were passed.  If not exit with a message to the debug file if elected.
	if (argc != 2) {

		EtmInfo.EtmErrno = ARGNUM;
		dodebug(EtmInfo.EtmErrno, "EtmMonitorMain()", (char*)NULL, (char*)NULL);
		return(NAM_ERROR);
	}
	else {

		sprintf(PipeInfo.PipeFile, "%s%s", PIPE_FILE_HEADER, argv[PIPE_ARG]);
	}

	// Get the concurrent dtb process running.
	PerformConcurrentOp();
	
	dodebug(0,"EtmMonitorMain()","Leaving function in EtmMonitor class", (char*)NULL);
	return(SUCCESS);

}
