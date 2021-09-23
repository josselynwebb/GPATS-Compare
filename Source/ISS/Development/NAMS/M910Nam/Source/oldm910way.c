/****************************************************************************
 *	File:	oldm910way.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *		2.3		Modified the end of the function oldm910way(), instead of	*
 *				checking for argc count being > 6 it now checks for argc > 8*
 *				also added an additional check to see if the programmer		*
 *				the Fault Callout and Fault Message passed back.			*
 *		2.5		Removed the return statement from the case TERM9_RESULT_FAIL*
 *				statement.  Now it increments a had_error flag.  This was	*
 *				done to allow for a diagnose message to be return to the	*
 *				ATLAS program.												*
 *																			*
 ***************************************************************************/

/****************************************************************************
*		Include Files														*
****************************************************************************/

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <dos.h>
#include <time.h>
#include <sys/types.h>
#include <sys/timeb.h>
#include <direct.h>
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include "visa.h"
#include "terM9.h"
#include "DM_Services.h"
#include "callbacks.h"
#include "m910nam.h"
#include "m9concur.h"

/****************************************************************************
*		Modules																*
****************************************************************************/

/*
 * olm910way:	This program will perform the M910NAM as if it used in the old
 *					fashion, hense the function name. There have been some
 *					modifications to check for errors, and along this a way
 *					for the developer to debug if something doesn't work properly.
 *					DEBUG variable is set to TRUE if the enviro variable
 *					DEBUGSOURCE is used.
 *
 * Parameters:
 *		argc:			The # of arguments passed to main.
 *		argv:			Character list of the arguments passed to main.
 *
 * Returns:
 *		SUCCESS:	0    - successful completion of the function.
 *		M9_ERROR:	(-1) - An error happpened what else.
 */

							  
int oldm910way(int argc, char *argv[])
{
	int	had_error = 0;

	//Check if a concurrent process is running.
	if (concur_info.concurrent_running) 
	{
		dodebug(CONCURRENT_RUNNING, "oldm910way()", NULL);
		dtb_info.dtb_errno = BURST_NOT_RUN;
	}

	//Check to see if the arguments passed to the function are of proper type.
	if (checkargs(argc, argv, dtb_info.method))
	{
		return(M9_ERROR);
	}

	//Check to see if the integer values passed were of proper value.
	if (checkintvalues()) 
	{
		dtb_info.dtb_errno = DIAG_STATE_VAL;
		return(M9_ERROR);
	}

	/*Put or remove the file extensions of the dtb and circuit file. Also
	 * check that they are there.*/
	if (check_dtb_cxs_exten()) 
	{
		dtb_info.dtb_errno = BURST_NOT_RUN;
		return(M9_ERROR);
	}

	/*Perform the DTB and if the return is BURST_FAILED then check to see if
	there was requested diagnostics, if so doit, else return with errno set to the
	proper fault.*/
	if (do_dtb() == SUCCESS) 
	{
		had_error = 0;

		switch(dtb_info.test_status) 
		{
			case TERM9_RESULT_PASS:

				dodebug(0, "old910way()", "The burst passed");
				dtb_info.dtb_errno = BURST_PASSED;

				break;

			case TERM9_RESULT_FAIL:

				if (dtb_info.diag_type) {
					if (do_diag() == M9_ERROR) {
						dtb_info.dtb_errno = BURST_FAILED;
						had_error++;
					}
				}

				dodebug(0, "old910way()", "The burst failed");
				dtb_info.dtb_errno = BURST_FAILED;

				break;

			case TERM9_RESULT_NOT_RUN:

				dodebug(0, "old910way()", "The burst was not run/didn't run properly");
				dtb_info.dtb_errno = BURST_NOT_RUN;
				had_error++;

				break;

			case TERM9_RESULT_MBT:

				dodebug(0, "newm910way()", "The burst reached maximum time allowed");
				dtb_info.dtb_errno = BURST_MAX_TIME;
				had_error++;

				break;

			default :

				dodebug(0, "old910way()", "Something is very wrong in Micro Slop world");
				dtb_info.dtb_errno = BURST_NOT_RUN;
				had_error++;

				break;

		}
	}

	if (close_dti())
	{
		had_error++;
	}

	if (argc > 8) 
	{
		//Check if they want the fault call out and message.
		if (argc > 9) 
		{
			dtb_info.fault_info_requested = 1;
		}
	
		if (create_ide()) 
		{
			dodebug(0, "old910way()", "create_ide failed check error code");
			had_error++;
		}
	}

	return(had_error == FALSE ? SUCCESS : M9_ERROR);
}