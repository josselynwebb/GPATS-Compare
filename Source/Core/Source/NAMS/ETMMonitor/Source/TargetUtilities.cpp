/////////////////////////////////////////////////////////////////////////////
//	File:	TargetUtilities.cpp												/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
//	PerformTargetFunction:	This function will read the message from the
//								pipe_infp structure and compare it to the
//								allowed messages and will return the index of
//								the compare.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int PerformTargetFunction()
{

//
// Let's get the command that we are to perform from the controlling process.
// First we will tell to controlling process to send the command.
//
	dodebug(0,"PerformTargetFunction()","Entering function in ReaderUtilities class", (char*)NULL);

	if (GetMode(FUNCTIONAL)) {
		return(NAM_ERROR);
	}

	if (ProcessGatheredData()) {
		return(NAM_ERROR);
	}
	
	dodebug(0,"PerformTargetFunction()","Leaving function in ReaderUtilities class", (char*)NULL);
	return(SUCCESS);
}
