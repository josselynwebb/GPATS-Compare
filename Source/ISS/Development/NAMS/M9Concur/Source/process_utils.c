/****************************************************************************
 *	File:	process_utils.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		1.0		Assigned it a version number.								*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	

#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include <stdio.h>
#include <stdlib.h>
#include <Psapi.h>
#include "visa.h"
#include "visatype.h"
#include "terM9.h"
#include "m9concur.h"

/****************************************************************************
 *	Local Constants															*
 ***************************************************************************/

/****************************************************************************
 *	Modules																	*
 ***************************************************************************/

/*
 *	start_proc:			This function will try to start the requested process.
 *							
 *	Parameters:
 *		none:
 *		
 *	Returns:
 *		SUCCESS:		0    = No errors were encountered.
 *		M9_ERROR:		(-1) = Couldn't perform one of the required tasks.
 */
int	start_proc(void)
{

	if ((concur_info.threadhd = CreateThread(NULL, 0, do_dtb, NULL, 0, &concur_info.threadid)) == NULL) 
	{
		dodebug(CONCURRENT_ERROR, "start_proc()", NULL);
		return(M9_ERROR);
	}

	return(SUCCESS);
}