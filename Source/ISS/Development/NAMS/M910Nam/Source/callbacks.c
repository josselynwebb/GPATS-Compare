/****************************************************************************
 *	File:	callbacks.c														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *		2.3		Added the header block Static Storage						*
 *		2.5		Modified the header block for the function					*
 *				probe_button_pressed.  Made it correct sort of.				*
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
#include <process.h>
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
 *	Static Storage															*
 ***************************************************************************/
#pragma warning(disable : 4028)

CB_FUNC cb_func[] = {

/*
used	name								fp_callback							fp_callback_fp			option	*/
{ 0,		"start_of_test_callback",			DM_setStartOfTestCallback,			start_of_test,			NULL },
{ 0,		"end_of_test_callback",				DM_setEndOfTestCallback,			end_of_test,			NULL },
{ 0,		"probe_point_ready_callback",		DM_setProbePointReadyCallback,		probe_point_ready,		NULL },
{ 0,		"probe_sequence_started_callback",	DM_setProbeSequenceStartedCallback,	probe_sequence_started,	NULL },
{ 0,		"probe_sequence_ended_callback",	DM_setProbeSequenceEndedCallback,	probe_sequence_ended,	NULL },
{ 0,		"probe_button_pressed_callback",	DM_setProbeButtonPressedCallback,	probe_button_pressed,	NULL },
{ 0,		"diag_test_callback",				DM_setDiagnoseTestCallback,			diagnose_test,			NULL },
{ 0,		"start_of_pattern_callback",		DM_setStartOfPatternSetCallback,	start_of_pattern,		NULL },
{ 0,		"end_of_pattern_callback",			DM_setEndOfPatternSetCallback,		end_of_pattern,			NULL },
{ 0,		NULL,								NULL,								NULL,					NULL }
};

#pragma warning(default : 4028)

/****************************************************************************
 *	Modules																	*
 ***************************************************************************/

/*
 *	probe_point_ready:	This is call back function that is called whenever
 *							a probe point is ready to be probed, and will
 *							be passed what that probe point is. It will then
 *							perform whatever program that was passed in the 
 *							dtb file upon execution of the m910nam.
 *
 *	Parameters:
 *		probe_point:	This is a character string of the next probe point.
 *		
 *	Returns:
 *
 */

void _stdcall probe_point_ready(char *probe_point)
{

	char	*probe_point_buf;

/*
 * Now call the program that is to used for this call back.
 */

	if ((probe_point_buf = malloc(strlen(probe_point) + 5)) == NULL) {
		dodebug(0, "probe_point_ready()", "malloc failed to allocate memory");
		dtb_info.dtb_errno = P_P_R_CALL_ERROR;
	}

	else {

		sprintf(probe_point_buf, "\"%s\"", probe_point);

		if ((_spawnl(_P_WAIT, cb_func[PROBE_POINT_READY_CALLBACK - INDEX_OFFSET].options,
			 probe_point_buf, NULL)) != SUCCESS) {

			dodebug(P_P_R_RET_ERROR, "probe_point_ready()", NULL);
			dtb_info.dtb_errno = P_P_R_RET_ERROR;
		}
	}
}


/*
 *	end_of_pattern:		This is call back function that is called whenever
 *							a pattern set has finished executing in a digital
 *							test. This callback will be passed which pattern
 *							set that has just finished. This will be used to
 *							determine which if more then one program is to executed.
 *
 *	Parameters:
 *		pattern_info:	This is a character string that contains the viHandle, testName,
 *							and the patternIndex.
 *		
 *	Returns:
 *
 */

void _stdcall end_of_pattern(char *pattern_info)
{

	int		index_num;
	char	*pattern_info_buf;
	char	*lexp;

/*
 * First we need to put this into the proper format to be sent to
 * the executable program.
 */


	if ((pattern_info_buf = malloc(strlen(pattern_info) + 5)) == NULL) {
		dodebug(0, "end_of_pattern()", "malloc failed to allocate memory");
		dtb_info.dtb_errno = E_O_P_CALL_ERROR;
	}

	else {

		sprintf(pattern_info_buf, "\"%s\"", pattern_info);

/*
 * Next we see if there are to be different callback programs for the different patterns
 * sets that may be aviable.
 */

		index_num = atoi(cb_func[END_OF_PATTERN_CALLBACK - INDEX_OFFSET].options);

		if (index_num == (-1)) {

			if ((_spawnl(_P_WAIT, cb_info[index_num + 1].program, pattern_info_buf, NULL)) != SUCCESS) {

				dodebug(E_O_P_RET_ERROR, "end_of_pattern()", NULL);
				dtb_info.dtb_errno = E_O_P_RET_ERROR;
			}
		}

		else {

			if ((lexp = strrchr(pattern_info, ' ')) != NULL) {

				if (cb_info[atoi(lexp+1)].used == TRUE) {
					if ((_spawnl(_P_WAIT, cb_info[atoi(lexp+1)].program,
						pattern_info_buf, NULL)) != SUCCESS) {

						dodebug(E_O_P_RET_ERROR, "end_of_pattern()", NULL);
						dtb_info.dtb_errno = E_O_P_RET_ERROR;
					}
				}
			}

			else {
				dodebug(E_O_P_CALL_ERROR, "end_of_pattern()", NULL);
				dtb_info.dtb_errno = E_O_P_CALL_ERROR;
			}
		}
	}
}

/*
 *	start_of_pattern:		This is call back function that is called whenever
 *							a pattern set is about to start executing in a digital
 *							test. This callback will be passed which pattern
 *							set that is to be perform. This will be used to
 *							determine which if more then one program is to executed.
 *
 *	Parameters:
 *		pattern_info:	This is a character string that contains the viHandle, testName,
 *							and the patternIndex.
 *
 *	Parameters:
 *		probe_point:	This is a character string of the next probe point.
 *		
 *	Returns:
 *
 */

void _stdcall start_of_pattern(char *pattern_info)
{

	int		index_num;
	char	*pattern_info_buf;
	char	*lexp;

/*
 * First we need to put this into the proper format to be sent to
 * the executable program.
 */


	if ((pattern_info_buf = malloc(strlen(pattern_info) + 5)) == NULL) {
		dodebug(0, "start_of_pattern()", "malloc failed to allocate memory");
		dtb_info.dtb_errno = S_O_P_CALL_ERROR;
	}

	else {

		sprintf(pattern_info_buf, "\"%s\"", pattern_info);

/*
 * Next we see if there are to be different callback programs for the different patterns
 * sets that may be aviable.
 */

		index_num = atoi(cb_func[START_OF_PATTERN_CALLBACK - INDEX_OFFSET].options);

		if (index_num == (-1)) {

			if ((_spawnl(_P_WAIT, cb_info[index_num + 1].program, pattern_info_buf, NULL)) != SUCCESS) {

				dodebug(S_O_P_RET_ERROR, "end_of_pattern()", NULL);
				dtb_info.dtb_errno = S_O_P_RET_ERROR;
			}
		}

		else {

			if ((lexp = strrchr(pattern_info, ' ')) != NULL) {

				if (cb_info[atoi(lexp+1)].used == TRUE) {
					if ((_spawnl(_P_WAIT, cb_info[atoi(lexp+1)].program,
						pattern_info_buf, NULL)) != SUCCESS) {

						dodebug(S_O_P_RET_ERROR, "end_of_pattern()", NULL);
						dtb_info.dtb_errno = S_O_P_RET_ERROR;
					}
				}
			}

			else {
				dodebug(S_O_P_CALL_ERROR, "end_of_pattern()", NULL);
				dtb_info.dtb_errno = S_O_P_CALL_ERROR;
			}
		}
	}
}

/*
 *	probe_button_pressed:	This is call back function that is called whenever
 *							a probe button is pressed and the guided probe
 *							utility is ready to probe.  The argument passed
 *							to this function is empty string.  Therefore the
 *							program to be executed will not pass any arguments.
 *
 *	Parameters:
 *		probe_pressed:		This is an empty character string, don't ask me why,
 *							this is the wat Teradyne does it.
 *		
 *	Returns:
 *
 */

void _stdcall probe_button_pressed(char *probe_pressed)
{

	char	*probe_pressed_buf;


/*
 * First we need to put this into the proper format to be sent to
 * the executable program.
 */


	if ((probe_pressed_buf = malloc(strlen(probe_pressed) + 5)) == NULL) {
		dodebug(0, "probe_button_pressed()", "malloc failed to allocate memory");
		dtb_info.dtb_errno = P_B_P_CALL_ERROR;
	}

	else {

		sprintf(probe_pressed_buf, "\"%s\"", probe_pressed);

/*
 * Now call the program that is to used for this call back.
 */

		if ((_spawnl(_P_WAIT, cb_func[PROBE_BUTTON_PRESSED_CALLBACK - INDEX_OFFSET].options,
			 probe_pressed_buf, NULL)) != SUCCESS) {

			dodebug(P_B_P_RET_ERROR, "probe_button_pressed()", NULL);
			dtb_info.dtb_errno = P_B_P_RET_ERROR;
		}
	}
}

/*
 *	probe_sequence_started:	This is call back function that is called whenever
 *							a probe sequence is starting.  The argument that
 *							will be passed is the net name that is about to be
 *							probed.
 *
 *	Parameters:
 *		probe_point:	This is a character string of the probe point to be probed.
 *		
 *	Returns:
 *
 */

void _stdcall probe_sequence_started(char *probe_point)
{

	char	*probe_point_buf;

/*
 * Now call the program that is to used for this call back.
 */

	if ((probe_point_buf = malloc(strlen(probe_point) + 5)) == NULL) {
		dodebug(0, "probe_sequence_started()", "malloc failed to allocate memory");
		dtb_info.dtb_errno = P_S_S_CALL_ERROR;
	}

	else {

		sprintf(probe_point_buf, "\"%s\"", probe_point);

		if ((_spawnl(_P_WAIT, cb_func[PROBE_SEQUENCE_STARTED_CALLBACK - INDEX_OFFSET].options,
			 probe_point_buf, NULL)) != SUCCESS) {

			dodebug(P_S_S_RET_ERROR, "probe_sequence_started()", NULL);
			dtb_info.dtb_errno = P_S_S_RET_ERROR;
		}
	}
}

/*
 *	probe_sequence_ended:	This is call back function that is called whenever
 *							a probe sequence has ended.  The argument that
 *							will be passed is the net name that has just been
 *							probed.
 *
 *	Parameters:
 *		probe_point:	This is a character string of the probe point just probed.
 *		
 *	Returns:
 *
 */

void _stdcall probe_sequence_ended(char *probe_point)
{

	char	*probe_point_buf;

/*
 * Now call the program that is to used for this call back.
 */

	if ((probe_point_buf = malloc(strlen(probe_point) + 5)) == NULL) {
		dodebug(0, "probe_sequence_ended()", "malloc failed to allocate memory");
		dtb_info.dtb_errno = P_S_E_CALL_ERROR;
	}

	else {

		sprintf(probe_point_buf, "\"%s\"", probe_point);

		if ((_spawnl(_P_WAIT, cb_func[PROBE_SEQUENCE_ENDED_CALLBACK - INDEX_OFFSET].options,
			 probe_point_buf, NULL)) != SUCCESS) {

			dodebug(P_S_E_RET_ERROR, "probe_sequence_ended()", NULL);
			dtb_info.dtb_errno = P_S_E_RET_ERROR;
		}
	}
}

/*
 *	end_of_test:	This is call back function that is called when the end of a digital
 *						test has been reached.
 *
 *	Parameters:
 *		test_info:	This is a character string of the viHandle and testname seperated
 *							by a white space.
 *		
 *	Returns:
 *
 */

void _stdcall end_of_test(char *test_info)
{

	char	*test_info_buf;

/*
 * Now call the program that is to used for this call back.
 */

	if ((test_info_buf = malloc(strlen(test_info) + 5)) == NULL) {
		dodebug(0, "end_of_test()", "malloc failed to allocate memory");
		dtb_info.dtb_errno = E_O_T_CALL_ERROR;
	}

	else {

		sprintf(test_info_buf, "\"%s\"", test_info);

		if ((_spawnl(_P_WAIT, cb_func[END_OF_TEST_CALLBACK - INDEX_OFFSET].options,
			 test_info_buf, NULL)) != SUCCESS) {

			dodebug(E_O_T_RET_ERROR, "end_of_test()", NULL);
			dtb_info.dtb_errno = E_O_T_RET_ERROR;
		}
	}
}

/*
 *	start_of_test:	This is call back function that is called when a digital test
 *						begins to execute.
 *
 *	Parameters:
 *		test_info:	This is a character string of the viHandle and testname seperated
 *							by a white space.
 *		
 *	Returns:
 *
 */

void _stdcall start_of_test(char *test_info)
{

	char	*test_info_buf;

/*
 * Now call the program that is to used for this call back.
 */

	if ((test_info_buf = malloc(strlen(test_info) + 5)) == NULL) {
		dodebug(0, "start_of_test()", "malloc failed to allocate memory");
		dtb_info.dtb_errno = S_O_T_CALL_ERROR;
	}

	else {

		sprintf(test_info_buf, "\"%s\"", test_info);

		if ((_spawnl(_P_WAIT, cb_func[START_OF_TEST_CALLBACK - INDEX_OFFSET].options,
			 test_info_buf, NULL)) != SUCCESS) {

			dodebug(S_O_T_RET_ERROR, "start_of_test()", NULL);
			dtb_info.dtb_errno = S_O_T_RET_ERROR;
		}
	}
}

/*
 *	diagnose_test:	This is call back function that is called when the diagnosis
 *						test has completed.
 *
 *	Parameters:
 *		status:		This is the completion code of the diagnosis test.
 *		
 *	Returns:
 *
 */

void _stdcall diagnose_test(DiagStatus status)
{

	char	status_value[32];

/*
 * Now call the program that is to used for this call back.
 */

	sprintf(status_value, "%i", status);

	if ((_spawnl(_P_WAIT, cb_func[DIAG_TEST_CALLBACK - INDEX_OFFSET].options,
		 status_value, NULL)) != SUCCESS) {

		dodebug(D_T_RET_ERROR, "diagnose_test()", NULL);
		dtb_info.dtb_errno = D_T_RET_ERROR;
	}
}
