/****************************************************************************
 *	File:	dtb_routine.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *		2.5		Corrected the header block for the function format_error.	*
 *				Corrected the header block for the function close_dti.		*
 *				Corrected the header block for the function do_dtb.			*
 *				Added the calls to the change_dir function, these were		*
 *				required to fix the problem related to STR#744.				*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	
#include <io.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <fcntl.h>
#include <errno.h>
#include <time.h>
#include <malloc.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "visa.h"
#include "vpptype.h"
#include "terM9.h"
#include "DM_Services.h"
#include "callbacks.h"
#include "m910nam.h"

/****************************************************************************
 *	Local Constants															*
 ***************************************************************************/

/****************************************************************************
 *	Modules																	*
 ***************************************************************************/

/*
 *	format_error:	This function will format the error that is to be printed
 *						to the debug_file if that option is enabled.
 *
 *	Parameters:
 *		function:	This is the name of the function that the error occured.
 *
 *	Returns:
 *		None:		This is a void function.
 *
 */

void format_error(ViStatus result, char *function)
{

	ViChar		message[256];

	if (terM9_errorMessage(dtb_info.dti_handle, result, message) == VI_SUCCESS) {
		dodebug(0, function, message);
	}
}

/*
 *	do_dtb:			This function will grab ahold of the instrument and run
 *						the required burst. This function will do the simplest
 *						task which is also least flexible.
 *
 *	Parameters:
 *		NONE			This function will use the dtb_info structure for
 *						its information.
 *
 *	Returns:
 *		SUCCESS:	Everything worked, what a concept.
 *		M9_ERROR:	Something happened that shouldn't.  The variable errno
 *					was set to reflect the cause.
 *
 *	errno values:
 *		BURST_NOT_RUN:	Just what the error says.
 *
 */

int do_dtb(void) {

	int			had_error;
	ViStatus	result;

// Call the change_dir function.

	if (change_dir(NEW)) {
		return(M9_ERROR);
	}

	had_error = 0;

	if ((result = terM9_init("TERM9", VI_FALSE, (ViBoolean)(dtb_info.reset_flag == 0 ?
							 VI_FALSE : VI_TRUE), &dtb_info.dti_handle)) == VI_SUCCESS) {

		dtb_info.opened = TRUE;

		if ((result = terM9_setSystemEnable (dtb_info.dti_handle, VI_TRUE)) == VI_SUCCESS) {

			if ((result = terM9_executeDigitalTest(dtb_info.dti_handle, dtb_info.dtb_file, VI_FALSE,
												 &dtb_info.test_status)) == VI_SUCCESS) {
			}
			else {
				format_error(result, "do_dtb()");
				had_error++;
			}
		}
		else {
			format_error(result, "do_dtb()");
			had_error++;
		}
	}
	else {
		format_error(result, "do_dtb()");
		had_error++;
	}

	if (had_error) {
		dtb_info.dtb_errno = BURST_NOT_RUN;
	}

/*
 * Now change back to the directory the ATLAS program is running from.
 */

	if (change_dir(OLD)) {
		return(M9_ERROR);
	}

	return(had_error == 0 ? SUCCESS : M9_ERROR);
}

/*
 *	close_dti:		This function will close the dti session and properly adjust
 *						the dti to the proper state, reset or not.
 *
 *	Parameters:
 *		NONE			This function will use the dtb_info structure for
 *						its information.
 *
 *	Returns:
 *		SUCCESS:	Everything worked, what a concept.
 *		M9_ERROR:	Something happened that shouldn't.  The variable errno
 *					was set to reflect the cause.
 *
 */

int	close_dti(void)
{

	int			had_error;
	ViStatus	results;

	had_error = 0;

	if (!dtb_info.pin_state) {
		if ((results = terM9_reset(dtb_info.dti_handle)) != VI_SUCCESS) {
			format_error(results, "close_dti()");
			had_error++;
		}
	}
	if ((results = terM9_close(dtb_info.dti_handle)) != VI_SUCCESS) {
		format_error(results, "close_dti()");
		had_error++;
	}

	dtb_info.opened = FALSE;

	return(had_error == 0 ? SUCCESS : M9_ERROR);
}