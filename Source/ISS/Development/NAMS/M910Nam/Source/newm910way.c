/****************************************************************************
 *	File:	newm910way.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *		2.5		Removed the return statement from the case TERM9_RESULT_FAIL*
 *				statement.  Now it increments a had_error flag.  This was	*
 *				done to allow for a diagnose message to be return to the	*
 *				ATLAS program.												*
 *				Changed the casing of the dtb_info.fault_call to match the	*
 *				M910NAM user guide.											*
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
#include <process.h>
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
 * new910way:	This program will perform the M910NAM in the new and improved
 *					way, hense the name. Here everything has been rethought
 *					on the way things were done and now is performed in a logical
 *					and meaningful way.
 *
 * Parameters:
 *		argc:			The # of arguments passed to main.
 *		argv:			Character list of the arguments passed to main.
 *
 * Returns:
 *		SUCCESS:	0    - successful completion of the function.
 *		M9_ERROR:	(-1) - An error happen.
 */

							  
int newm910way(int argc, char *argv[])
{

	int		had_error = 0;
	int		leading_spaces;

	//Set the variable leading_white_spaces
	leading_spaces = TRUE;

	/* Set all the defauls incase they are not specified in the
	dtb info file.*/
	dtb_info.reset_flag			= TRUE;
	dtb_info.concurrent			= FALSE;
	dtb_info.mismatch_value		= UNSET_VALUE;
	dtb_info.seed_value			= UNSET_VALUE;
	dtb_info.stability_count	= UNSET_VALUE;
	dtb_info.pin_state			= FALSE;
	dtb_info.diag_type			= NO_DIAG;
	dtb_info.probe_reset		= DIAG_ENABLE;

	//Check to see if the arguments passed to the function are of proper type.
	if (checkargs(argc, argv, dtb_info.method)) 
	{
		return(M9_ERROR);
	}

	/*Check to see if the file that was passed is of proper type, see if it has
	the magic header in it. */
	if (check_header(dtb_info.file_name, MAGIC_HEADER, (-1), sizeof(MAGIC_HEADER) - 1)) 
	{
		dtb_info.dtb_errno = FILE_FORMAT_ERROR;
		return(M9_ERROR);
	}

	//Parse the bugger to its proper structures and/or variables.
	if (parse_file(dtb_info.file_name, sizeof(MAGIC_HEADER) - 1, leading_spaces)) 
	{
		dtb_info.dtb_errno = FILE_FORMAT_ERROR;
		return(M9_ERROR);
	}

	//Check if at least the minumum info has been put into the dtb info file.
	if (!_strnicmp(dtb_info.dtb_file, "null", strlen("null"))) 
	{

		if (dtb_info.concurrent != HALT_IT &&
			(!_strnicmp(dtb_info.execute_prog, "null", strlen("null")))) 
		{
			dodebug(NO_DTB_STATED, "newm910way()", NULL);
			dtb_info.dtb_errno = BURST_NOT_RUN;
			return(M9_ERROR);
		}
	}

	//Check to see if the integer values passed were of proper value.
	if (checkintvalues()) 
	{
		dtb_info.dtb_errno = DIAG_STATE_VAL;
		return(M9_ERROR);
	}

	/*Put or remove the file extensions of the dtb and circuit file. Also
	check that they are there. */
	if (!_strnicmp(dtb_info.execute_prog, "null", strlen("null"))) 
	{

		if (check_dtb_cxs_exten()) {
			dtb_info.dtb_errno = BURST_NOT_RUN;
			return(M9_ERROR);
		}
	}

	/*Check the concurrent structure and the dtb structure to determine 
	if we go concurrent or terminate one that is running. */
	if (concur_info.concurrent_running && dtb_info.concurrent == HALT_IT) 
	{
		return(terminate_running_m910());
	}

	if (concur_info.concurrent_running && dtb_info.concurrent != HALT_IT) 
	{
		dtb_info.dtb_errno = BURST_NOT_RUN;
		dodebug(CONCURRENT_RUNNING, "newm910way()", 0);
		return(M9_ERROR);
	}

	if (!concur_info.concurrent_running && dtb_info.concurrent == HALT_IT) 
	{
		dtb_info.dtb_errno = BURST_FAILED;
		dodebug(IMPROPER_SHUTDOWN, "newm910way()", 0);
		return(M9_ERROR);
	}

	/*If no concurrent process running, check if the programmer wants one.  
	Check the dtb info flag for concurrence and if so perform the m910 concurrence call. */
	if (dtb_info.concurrent == START_IT) 
	{

		return(perform_concurrent_op());
	}

	/*Perform the DTB and if the return is BURST_FAILED then check to see if
	there was requested diagnostics, if so doit, else return with errno set to the
	proper fault. */
	if (!_strnicmp(dtb_info.execute_prog, "null", strlen("null"))) 
	{
		if (do_dtb() == SUCCESS)
		{
			had_error = 0;

			switch(dtb_info.test_status)
			{

				case TERM9_RESULT_PASS:

					dodebug(0, "newm910way()", "The burst passed");
					dtb_info.dtb_errno = BURST_PASSED;

					break;

				case TERM9_RESULT_FAIL:

					if (dtb_info.diag_type) {
						if (do_diag() == M9_ERROR) {
							dtb_info.dtb_errno = BURST_FAILED;
							had_error++;
						}
					}

					dodebug(0, "newm910way()", "The burst failed");
					dtb_info.dtb_errno = BURST_FAILED;

					break;

				case TERM9_RESULT_NOT_RUN:

					dodebug(0, "newm910way()", "The burst was not run/didn't run properly");
					dtb_info.dtb_errno = BURST_NOT_RUN;
					had_error++;

					break;

				case TERM9_RESULT_MBT:

					dodebug(0, "newm910way()", "The burst reached maximum time allowed");
					dtb_info.dtb_errno = BURST_MAX_TIME;
					had_error++;

					break;

				default :

					dodebug(0, "new910way()", "Something happened and there is nothing to do about it");
					dtb_info.dtb_errno = BURST_NOT_RUN;
					had_error++;

					break;

			}
		}

		if (close_dti()) 
		{
			had_error++;
		}
	}

	if (_strnicmp(dtb_info.execute_prog, "null", strlen("null"))) 
	{
		int	Return_Status = 0;

		if ((Return_Status = _spawnl(_P_WAIT, dtb_info.execute_prog, NULL)) != SUCCESS) 
		{
			char	tmpbuf[80];

			sprintf(tmpbuf, "Program %s return %d for its' exit status", dtb_info.execute_prog, Return_Status);

			dodebug(0, "newm910way()", tmpbuf);
			had_error++;
			dtb_info.dtb_errno = PROGRAM_FAILED;
		}
		else 
		{
			dtb_info.dtb_errno = PROGRAM_PASSED;
		}
	}

	if (argc > 5 || (!ATLAS) && argc > 2) 
	{
		if (_strnicmp(dtb_info.execute_prog, "null", strlen("null")))
		{
			sprintf(dtb_info.fault_call, "None");
			dtb_info.fault_mess[0] = '\0';
		}
		else 
		{
			if (create_ide()) 
			{
				dodebug(0, "newm910way()", "create_ide failed check error code");
				had_error++;
			}
		}
		dtb_info.fault_info_requested = 1;
	}

	return(had_error == FALSE ? SUCCESS : M9_ERROR);
}