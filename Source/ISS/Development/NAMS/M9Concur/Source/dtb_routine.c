/****************************************************************************
 *	File:	dtb_routine.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		1.0		Assigned it a version number.								*
 *																			*
 *		1.1		In function do_dtb when calling function terM9_init			*
 *				corrected the mistake of equating to zero to test to see if *
 *				it is zero.  Pulled a bonehead mistake.  RAC 16 Sept 02		*
 *																			*
 *		1.2		Added new function open_dti().  This function moved the		*
 *				terM9_init() out of do_dtb().  Also							*
 *				the function terM9_executeDigitalTest() was broken down into*
 *				smaller parts to get everything ready to go so all that had	*
 *				to be done in do_dtb was run the burst(s).  This function	*
 *				split into four different functions and part was transfered	*
 *				to open_dti(). The function do_dtb() now has two of the		*
 *				four function that makeup terM9_executeDigitalTest() they	*
 *				are terM9_configureDigitalTestPatternSet()	and 			*
 *				terM9_runDynamicPatternSet() these two deal with just the	*
 *				running of the burst only.  The other two which deal with	*
 *				loading of the digital burst pattern into memory and getting*
 *				the dti ready for the burst to be run are now in open_dti().*
 *				These are terM9_openDigitalTest() and						*
 *				terM9_configureDigitalTestSetup().  The new function		*
 *				open_dti() deals with opening up and loading into memory the*
 *				dtb.														*
 *		1.3		Removed unused code from open_dti function that was left	*
 *				over from previous builds.									*
 *				In the do_dtb function removed left over code from previous	*
 *				builds, also initialized the result variable to stop the	*
 *				compiler from complaining, changed the return on the if		*
 *				had_error statement.										*
 *				In the close_dti function added the call to the API function*
 *				terM9_closeDigitalTest, this was done after in-depth		*
 *				research of Teradyne's documentation while debugging a		*
 *				software problem, this should be the proper way of closing a*
 *				digital burst.												*
 *				In the halt_running_dtb function the test of the status		*
 *				variable was changed to just testing for not success.  This	*
 *				allows for the debugging of all errors.						*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	

#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
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
#include "m9concur.h"

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
 *
 *	Returns:
 *		None:		This is a void function.
 *
 */
void format_error(ViStatus result, char *function)
{
	ViChar		message[256];

	if (terM9_errorMessage(dtb_info.dti_handle, result, message) == VI_SUCCESS) 
	{
		dodebug(0, function, message);
	}
}

/*
 *	do_dtb:			This function will grab ahold of the instrument and run
 *						the required burst. This function will do the simplest
 *						task which is also least flexible.
 *
 *	Parameters:
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
#pragma warning(disable : 4100)
DWORD WINAPI do_dtb(LPVOID *nothing_is_here_this_is_a_NULL_pointer_that_is_all)
{
	int			had_error;
	ViStatus	result, status = VI_SUCCESS;
	ViInt32		ps_results = VI_SUCCESS, ps_index = 0;
	had_error = 0;
	result    = 0;

	while (status != TERM9_ERROR_PATSETIDX)
	{
		status = terM9_configureDigitalTestPatternSet(dtb_info.dti_handle, dtb_info.dtb_hd, ps_index++);

		if (status == VI_SUCCESS) 
		{
			result = terM9_runDynamicPatternSet(dtb_info.dti_handle, VI_TRUE, &ps_results);
		}
	}

	if (result != VI_SUCCESS)
	{
		format_error(result, "do_dtb()");
		dtb_info.dtb_errno = BURST_NOT_RUN;
		had_error++;
	}

	dtb_info.dtb_errno = ps_results;

	if (had_error) 
	{
		return(ps_results);
	}

	return(had_error == FALSE ? SUCCESS : M9_ERROR);
}

#pragma warning(default : 4100)

/*
 *	close_dti:		This function will close the dti session and properly adjust
 *						the dti to the proper state, reset or not.
 *
 *	Parameters:
 *		None:
 *
 *	Returns:
 *		None:
 *
 *	errno values:
 *
 */
void close_dti(void)
{
	int			had_error;
	ViStatus	results;
	had_error = 0;

	if ((results = terM9_closeDigitalTest(dtb_info.dti_handle, dtb_info.dtb_hd)) != VI_SUCCESS)
	{
		format_error(results, "close_dti()");
		dtb_info.dtb_errno = CHILD_RUN_WILD;
	}

	if (!dtb_info.pin_state) 
	{		
		if ((results = terM9_reset(dtb_info.dti_handle)) != VI_SUCCESS) 
		{
			format_error(results, "close_dti()");
			dtb_info.dtb_errno = CHILD_RUN_WILD;
		}
	}
	if ((results = terM9_close(dtb_info.dti_handle)) != VI_SUCCESS) 
	{
		format_error(results, "close_dti()");
		dtb_info.dtb_errno = CHILD_RUN_WILD;
	}
	else 
	{

		dtb_info.opened = FALSE;
	}
}

/*
 *	halt_running_dtb:		This function will halt the running of a digital pattern
 *								that may be in an infinite loop for purpose the programmer
 *								required. The return from the terM9 call is of no use so
 *								therefore it won't be use or tested or return'd.
 *
 *	Parameters:
 *		None:
 *
 *	Returns:
 *		None:		This is a void function.
 *
 *
 */
void halt_running_dtb(void)
{
	ViStatus	status;
	status = terM9_stopDynamicPatternSetExecution(dtb_info.dti_handle);

	if (status != VI_SUCCESS)
	{
		dodebug(0, "halt_running_dtb()", "An unknown error happened");
		dtb_info.dtb_errno = CHILD_RUN_WILD;
	}

}

/*
 *	open_dti:		This function will open the dti session and properly adjust
 *						the dti to the proper state, reset or not.  Then it will
 *						load the .dtb file into memory.
 *
 *	Parameters:
 *		None:
 *
 *	Returns:
 *		None:
 *
 *	errno values:
 *
 */
int	open_dti(void)
{
	int			had_error;
	ViStatus	result;
	had_error = 0;

	if ((result = terM9_init("TERM9", VI_FALSE, (ViBoolean)(dtb_info.reset_flag == 0 ? VI_FALSE : VI_TRUE), &dtb_info.dti_handle)) == VI_SUCCESS) 
	{
		dtb_info.opened = TRUE;

		if ((result = terM9_openDigitalTest(dtb_info.dti_handle, dtb_info.dtb_file, &dtb_info.dtb_hd)) == VI_SUCCESS) 
		{
			if ((result = terM9_configureDigitalTestSetup(dtb_info.dti_handle, dtb_info.dtb_hd)) != VI_SUCCESS)
			{
				format_error(result, "do_dtb()");
				had_error++;
			}
		}
		else 
		{
			format_error(result, "do_dtb()");
			had_error++;
		}
	}
	else 
	{
		format_error(result, "do_dtb()");
		had_error++;
		dodebug(0, "open_dti()", "Failed to init m910");
	}

	if (had_error) 
	{
		dtb_info.dtb_errno = BURST_NOT_RUN;
		return(M9_ERROR);
	}

	return(SUCCESS);
}