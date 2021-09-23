/****************************************************************************
 *	File:	diag_routine.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *		2.1		Removed DM_getMaxSeedingFaultSetsValue and the fprintf		*
 *				statement in do_diag after the								*
 *				DM_setMaxSeedingFaultSetsValue function call.  This was		*
 *				causing the m910nam to core dump.  It was trying to print	*
 *				a file that wasn't open for reading and writing.			*
 *		2.2		Forgot to added the function dm_setProbeResetEnable in the	*
 *				function do_diag.  Brain Farts.								*
 *		2.5		Corrected the header block for the function do_diag_output.	*
 *				Modified the way the variable diagtype is set was changed.	*
 *				Modified the return message and fault callout to meet the	*
 *				documented M910nam user guide.								*
 *				Added the new function set_dir_file which is used to set	*
 *				the effective working directory to the digital directory.	*
 *				Rewrote the function diag_setup, easier to read.			*
 *				Corrected the header block forthe function do_diag.			*
 *				Modified the way the output directory was set.  This was	*
 *				needed to correct STR#744.  This now gets done in the		*
 *				function set_dir_file.										*
 *				Modified the code in the DM_diagnoseTest function block to	*
 *				allow for more debuging if an error happens, and to allow	*
 *				the ATLAS programmers more info on failure.					*
 *				Added the calls to change_dir to the function do_diag, this	*
 *				call allows for the fix that was done for STR#744.			*
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
#include "visatype.h"
#include "diagnostics.h"
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
 *	do_diag_output:	This function will output the last fault, which will be used
 *						for the command line arguments fault callout and fault
 *						message.
 *
 *	Parameters:
 *		NONE			This function will use the dtb_info structure for
 *						its information.
 *		
 *	Returns:
 *		SUCCESS:	Everything worked, no joke, it did, honest.
 *		M9_ERROR:	All's not well in code land, see errno for the reason.
 *
 */

DiagStatus do_diag_output(void) {

	DiagType			diagtype;
	DiagStatus			result;
	DM_DiagFaultHandle	prev, next;

/*
 * Initialize a few variables so we can get started, this is my way of doing it.
 */

	diagtype = dtb_info.diag_type == FAULT_DICT ? 
									 DIAG_TYPE_FAULT_DICTIONARY : DIAG_TYPE_GUIDED_PROBE;
	prev = next = NULL;

/*
 * Now loop through the faults if any to get to the last one. That is all this does.
 */

	do {

		result = DM_getFaultInfo(dtb_info.dtb_file, diagtype, MAX_MSG_SIZE,
								 dtb_info.fault_call, dtb_info.fault_mess, prev, &next);
		prev = next;
	}
	while (next != NULL);

/*
 * Now check the return status and determined if the info in the fault_call and
 * fault_mess are real character string, if not return them to the default.
 */

	if (next == NULL && result != DIAG_SUCCESS) {

		sprintf(dtb_info.fault_call, "%s", "Unable to Retrieve Fault Information");
		sprintf(dtb_info.fault_mess, "For More Info You Will Need to View Fault Diagnosis");
		dodebug(0, "do_diag_output()", "DM_getFaultInfo function fail check your code");
	}

	return(result);
}

/*
 *	set_dir_file:	This function will set the output directory and file name
 *						of the diagnostics.
 *
 *	Parameters:
 *		NONE:		This function will use the dtb_info structure for its
 *						information.
 *
 *	Returns:
 *		SUCCESS:	Everything worked.
 *		M9_ERROR:	The function could not set the dir or file.
 *
 *	errno values:
 *
 */

int set_dir_file(void)
{

	char	tmpbuf[M9_MAX_PATH];

/* 
Set the output dir location, and file name if it fails then try 
to set it to the default directory and file name
*/


	if (_strnicmp(dtb_info.log_file, "null", strlen("null"))) {

		if (DM_setDiagnosticOutputDir(dtb_info.log_location) != DIAG_SUCCESS) {

			sprintf(tmpbuf, "%s", LOGLOCATION);

			if (DM_setDiagnosticOutputDir(tmpbuf) != DIAG_SUCCESS) {
				dodebug(0, "do_diag()", "Teradyne software failed to set the output dir Check perms");
				dtb_info.dtb_errno = BURST_FAILED;
				return(M9_ERROR);
			}

			sprintf(tmpbuf, "%s", LOGFILE);

			if (DM_setDiagnosticOutputFile(tmpbuf) != DIAG_SUCCESS) {
				dodebug(0, "do_diag()", "Teradyne software failed to set the output file Check perms");
				dtb_info.dtb_errno = BURST_FAILED;
				return(M9_ERROR);
			}

			sprintf(dtb_info.log_location, "%s", LOGLOCATION);
			sprintf(dtb_info.log_file, "%s", LOGFILE);
			
		}

		else if (DM_setDiagnosticOutputFile(dtb_info.log_file) != DIAG_SUCCESS) {

			sprintf(tmpbuf, "%s", LOGLOCATION);

			if (DM_setDiagnosticOutputDir(tmpbuf) != DIAG_SUCCESS) {
				dodebug(0, "do_diag()", "Teradyne software failed to set the output dir Check perms");
				dtb_info.dtb_errno = BURST_FAILED;
				return(M9_ERROR);
			}

			sprintf(tmpbuf, "%s", LOGFILE);

			if (DM_setDiagnosticOutputFile(tmpbuf) != DIAG_SUCCESS) {
				dodebug(0, "do_diag()", "Teradyne software failed to set the output file Check perms");
				dtb_info.dtb_errno = BURST_FAILED;
				return(M9_ERROR);
			}

			sprintf(dtb_info.log_location, "%s", LOGLOCATION);
			sprintf(dtb_info.log_file, "%s", LOGFILE);
			
		}
	}

	else {

		sprintf(tmpbuf, "%s", LOGLOCATION);

		if (DM_setDiagnosticOutputDir(tmpbuf) != DIAG_SUCCESS) {
			dodebug(0, "do_diag()", "Teradyne software failed to set the output dir Check perms");
			dtb_info.dtb_errno = BURST_FAILED;
			return(M9_ERROR);
		}

		sprintf(tmpbuf, "%s", LOGFILE);

		if (DM_setDiagnosticOutputFile(tmpbuf) != DIAG_SUCCESS) {
			dodebug(0, "do_diag()", "Teradyne software failed to set the output file Check perms");
			dtb_info.dtb_errno = BURST_FAILED;
			return(M9_ERROR);
		}

		sprintf(dtb_info.log_location, "%s", LOGLOCATION);
		sprintf(dtb_info.log_file, "%s", LOGFILE);
	}

	return(SUCCESS);
}
 
/*
 *	diag_setup:		This function will setup the DTI for the proper type of
 *						diagnostics to be performed.
 *
 *	Parameters:
 *		NONE			This function will use the dtb_info structure for
 *						its information.
 *
 *	Returns:
 *		SUCCESS:	Everything worked.
 *		M9_ERROR:	The function was called with an improper argument.
 *
 */


int diag_setup(void) {

	DM_setDiagnosticEnable(DIAG_TYPE_FAULT_DICTIONARY,
						   dtb_info.diag_type == PROBE_ONLY ? DIAG_DISABLE : DIAG_ENABLE);

	DM_setDiagnosticEnable(DIAG_TYPE_GUIDED_PROBE,
						   dtb_info.diag_type == FAULT_DICT ? DIAG_DISABLE : DIAG_ENABLE);

	DM_setDiagnosticEnable(DIAG_TYPE_BOUNDARY_SCAN, DIAG_DISABLE);

	return(SUCCESS);
}

/*
 *	do_diag:		This function will setup the DTI for the proper type of
 *						diagnostics to be performed.
 *
 *	Parameters:
 *		NONE			This function will use the dtb_info structure for
 *						its information.
 *
 *	Returns:
 *		SUCCESS:	Everything worked.
 *		M9_ERROR:	The function was called with an improper argument.
 *
 */


int do_diag(void) {

	int			i;
	DiagStatus	status;

//Call the change_dir function.  

	if (change_dir(NEW)) {
		return(M9_ERROR);
	}

/*
 * Now we will set the output directory and file name.
 */

	if (set_dir_file()) {
		return(M9_ERROR);
	}

/*
 * Here we will cycle through the callback structure to see if any callbacks are used. If they are
 * used then register the callback.
 */

	for (i = 0 ; cb_func[i].name != NULL; i++) {
		if (cb_func[i].used) {
			cb_func[i].fp_callback(cb_func[i].u.fp_callback_fp);
		}
	}

/*
 * Set up what type of diagnostics that we are going to do.
 */

	if (diag_setup()) {
		return(M9_ERROR);
	}

/*
 * See if the programmer has selected a probe mismatch value that he/she wants
 * to be used, if so use it.
 */

	if (dtb_info.mismatch_value != UNSET_VALUE) {

		DM_setProbeMismatchValue(dtb_info.mismatch_value);
	}

/*
 * See if the programmer has selected to seed the probing with his/her value.
 */

	if (dtb_info.seed_value != UNSET_VALUE) {

		DM_setMaxSeedingFaultSetsValue(dtb_info.seed_value);
	}

/*
 * See if the programmer has set the stability count, if so set it also.
 */

	if (dtb_info.stability_count != UNSET_VALUE) {

		DM_setProbeStabilityCount(dtb_info.stability_count);
	}

/*
 * Now set the probe reset if it is suppose to be set.
 */

	DM_setProbeResetEnable(dtb_info.probe_reset);

/*
 * Set the Diagnostic Manger to either start right off with probing or have the
 * operator enable the start of the probing.
 */

	DM_setProbeNoWaitEnable(dtb_info.probe_nowait);

// Now do the diagnostics

	if ((status = DM_diagnoseTest(dtb_info.dtb_file, dtb_info.cir_file,
						  NULL, dtb_info.dti_handle)) != DIAG_SUCCESS) {

		char	tmpString[4096];

		sprintf(tmpString, "The return status is %d", status);

		dodebug(0, "do_diag()", "Teradyne software failed to operate properly");
		dodebug(0, "do_diag()", tmpString);

		tmpString[0] = '\0';

		if (dtb_info.diag_type != FAULT_DICT) {

			DM_getDiagnosisText(DIAG_TYPE_GUIDED_PROBE, 4096, tmpString);
			dodebug(0, "do_diag()", tmpString);

		}

		tmpString[0] = '\0';
		
		if (dtb_info.diag_type != PROBE_ONLY) {

			DM_getDiagnosisText(DIAG_TYPE_FAULT_DICTIONARY, 4096, tmpString);
			dodebug(0, "do_diag()", tmpString);

		}

		dtb_info.dtb_errno = BURST_FAILED;
	}

/*
 * Now go and get the info for fault callout and messge.
 */

	if (dtb_info.fault_info_requested) {

		if ((do_diag_output()) != DIAG_SUCCESS) {
			return(M9_ERROR);
		}
	}

/*
 * Now change back to the directory the ATLAS program is running from.
 */

	if (change_dir(OLD)) {
		return(M9_ERROR);
	}

	return(SUCCESS);
}
