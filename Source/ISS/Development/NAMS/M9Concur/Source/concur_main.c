/****************************************************************************
 *	File:	m9concur.c														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		1.0		Assigned it a version number.								*
 *		1.3		Removed unused code from the concur_main function that was	*
 *				left over from previous builds.								*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	

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
#include "visatype.h"
#include "terM9.h"
#include "m9concur.h"

/****************************************************************************
 *		Declarations														*
 ***************************************************************************/

DTB_INFO		dtb_info;
CONCUR_INFO		concur_info;
PIPE_INFO		pipe_info;
ERR_INFO		err_info;

/****************************************************************************
*		Modules																*
****************************************************************************/

/*
 *	clean_up:	This program is called every time the m910nam exits. It will
 *					clean up any left over running junk that happen due to an
 *					error.
 *
 *	Parameters:
 *
 *	Returns:	This is a void function.
 *
 */

void clean_up(void)
{
	if (dtb_info.opened) 
	{
		if (!dtb_info.pin_state)
		{
			terM9_reset(dtb_info.dti_handle);
		}
		terM9_close(dtb_info.dti_handle);
	}

	if (DE_BUG) 
	{
		fclose(debugfp);
	}

	if (pipe_info.pipehd != NULL) 
	{
		CloseHandle(pipe_info.pipehd);
	}

	if (dtb_info.alldone == FALSE) 
	{
	}

}

/*
 * concur_main:		This program will check the variables that were passed to
 *					it and if correct will set up the defaults and then
 *					call the different functions in proper order.
 *
 * Parameters:
 *		argc:		The # of arguments passed to concur_main. Duh
 *		argv:		Character list of the arguments passed to concur_main. Duh
 *
 * Returns:
 *		SUCCESS:	0    = Successful completion of the function.
 *		M9_ERROR:	(-1) = An error had occurr'd, what else.
 */

int concur_main (int argc, char *argv[])
{
	int	leading_spaces = TRUE;
	//MessageBox(NULL, "M9 Concur Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	//Setup some defaults and initilize some variables to their defaults.
	pipe_info.message_size	= MAX_MSG_SIZE + 24;
	pipe_info.pipehd		= NULL;
	dtb_info.dtb_errno		= M9_ERROR;
	dtb_info.reset_flag		= TRUE;
	dtb_info.concurrent		= FALSE;
	dtb_info.pin_state		= FALSE;
	dtb_info.alldone		= FALSE;
	dtb_info.opened			= FALSE;

	sprintf(dtb_info.dtb_file, "%s", "null");
	sprintf(dtb_info.log_file, "%s", LOGFILE);

	/*Check to see ifthe proper # of arguments were passed.  If not we
	will exit with a message to the debug file if elected.*/
	if (argc != 2) 
	{
		dtb_info.dtb_errno = ARGNUM;
		dodebug(dtb_info.dtb_errno, "main()", NULL);
		return(M9_ERROR);
	}
	else 
	{
		sprintf(dtb_info.file_name, "%s", argv[FILE_ARG]);
		sprintf(pipe_info.pipe_file, "%s%s", PIPE_FILE_HEADER, argv[PIPE_ARG]);
	}

	/*Check to see if the file that was passed is of proper type, see if it has
	the magic header in it. */
	if (check_header(dtb_info.file_name, MAGIC_HEADER, (-1), sizeof(MAGIC_HEADER) - 1))
	{
		if (dtb_info.dtb_errno == FALSE) 
		{
			dtb_info.dtb_errno = FILE_FORMAT_ERROR;
		}

		return_status();
		return(M9_ERROR);
	}

	//Parse the bugger to its proper structures and/or variables.

	if (parse_file(dtb_info.file_name, sizeof(MAGIC_HEADER) - 1, leading_spaces)) 
	{
		if (dtb_info.dtb_errno == FALSE) 
		{
			dtb_info.dtb_errno = FILE_FORMAT_ERROR;
		}

		return_status();
		return(M9_ERROR);
	}

	//See if at least the minumum info has been put into the dtb info file.
	if (!strnicmp(dtb_info.dtb_file, "null", strlen("null"))) 
	{
		dtb_info.dtb_errno = NO_DTB_STATED;
		dodebug(dtb_info.dtb_errno, "newm910way()", NULL);
		return_status();
		return(M9_ERROR);
	}

	//Check to see if the integer values passed were of proper value.
	if (checkintvalues()) 
	{
		return_status();
		return(M9_ERROR);
	}

	/*Put or remove the file extensions of the dtb and circuit file. Also
	check that they are there. No need to go any futher if they aren't there.*/
	if (fileexten()) 
	{
		return_status();
		return(M9_ERROR);
	}

	//Get this concurrent dtb process running.

	perform_concurrent_op();

	close_dti();

	TerminateThread(concur_info.threadhd, 0);

	return(SUCCESS);
}

/*
 * checkintvalues:	This function will check the integer values that were
 *						passed to it from ATLAS, to see if they are of proper
 *						values. If not set errno to the proper error and return
 *						with the proper return value.
 *
 * Parameters:
 *
 * Returns:
 *		SUCCESS:		0    - successful of this function.
 *		M9_ERROR:		(-1) - improper value was used.
 *
 */

int checkintvalues(void) 
{
	if (dtb_info.reset_flag < 0 || dtb_info.reset_flag > 1 || dtb_info.pin_state  < 0 || dtb_info.pin_state  > 1  || dtb_info.diag_type  < 0 || dtb_info.diag_type  > 3) 
	{
		char	tmpbuf[256];

		sprintf(tmpbuf,
				"DTS Reset, Pin State, or Diag Type value is incorrect\r\n%s - %d\r\n%s - %d\r\n%s - %d\r\n",
				"DTS Reset should either 0 or 1 what was passed was", dtb_info.reset_flag,
				"Pin State should either 0 or 1 what was passed was", dtb_info.pin_state,
				"DIag Type should be between 0 and 3 what was passed was", dtb_info.diag_type);
		dodebug(0, "checkintvalues()", tmpbuf);
		dtb_info.dtb_errno = DIAG_STATE_VAL;
		return(M9_ERROR);
	}

	return(SUCCESS);

}