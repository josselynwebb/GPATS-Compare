/****************************************************************************
 *	File:	m9concur.c														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		1.0		Assigned it a version number.								*
 *		1.3		Added two new include statements, which are required for the*
 *				testing to see if the program is to be put into the debug	*
 *				mode.														*
 *				In the WinMain function added the declaration of the dummy	*
 *				structure element that will be used on the _stat function	*
 *				call.  This is used for setting the bebug flag.	Also added	*
 *				the code of setting the variable debug_option which is used	*
 *				as previous stated.											*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	

#include <sys/types.h>
#include <sys/stat.h>
#include <wchar.h>
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include <shellapi.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <io.h>
#include <wchar.h>
#include <winnls.h>
#include "visa.h"
#include "visatype.h"
#include "terM9.h"
#include "stdafx.h"
#include "m9concur.h"

/****************************************************************************
 *		Declarations														*
 ***************************************************************************/

int				DE_BUG;
FILE			*debugfp;

/****************************************************************************
 *		Modules																*
 ***************************************************************************/

/*
 *	WinMain:	This is the main entry point for this program.  This function
 *					has been incorperated into an already existing program so
 *					that a console window will not appear.
 *
 *	Parameters:
 *		hInstance:		A Microsoft object handle for this program NOT USED.
 *		hPrevInstance:	A Microsoft object handle NOT USED.
 *		lpCmdLine:		Pointer to a null-terminated string specifying the
 *							command line for the application, excluding the
 *							program name.
 *		nCmdShow:		Specifies how the window is to be shown.
 *
 *	Returns:
 *		SUCCESS:		0    - No errors, just like it is suppose to do.
 *		M9_ERROR:		(-1) - Whoops had an error.
 *
 */
#pragma warning(disable : 4100)
int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR     lpCmdLine,                     int       nCmdShow)
{

	int				nargs, i, converted_chars, had_error;
	WCHAR			**cmd_line_list;
	char			**argv;
	char			converted_cmd_line_arg[M9_MAX_PATH];
	struct _stat	tmp_buf;
	
	//MessageBox(NULL, "M9ConcurMain", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	//Setup some defaults and initilize some variables to their defaults.
	sprintf(dtb_info.log_location, "%s", LOGLOCATION);
	sprintf(dtb_info.debug_option, "%s%s", dtb_info.log_location, DEBUGIT);
	converted_chars = 0;
	had_error = 0;

	//Setup the function atexit so cleanup can be done.
	atexit(clean_up);

	/*Find out if the program should be run in the DEBUG mode. If it is then open
	the Debug file, if that doesn't work then set DEBUG back to 0.  DEBUGSOURCE
	doesn't have to be set just used.*/
	DE_BUG = _stat(dtb_info.debug_option, &tmp_buf) == M9_ERROR ? 0 : 1;

	if (DE_BUG) 
	{
		char	debugfile[256];
		sprintf(debugfile, "%s%s", dtb_info.log_location, DEBUGFILENAME);

		if ((debugfp = fopen(debugfile, "w+b")) == NULL) 
		{
			DE_BUG = FALSE;
		}
	}

	//Get the command line arguments
	if ((cmd_line_list = CommandLineToArgvW(GetCommandLineW(), &nargs)) == NULL) {
		dodebug(0, "WinMain()", "Failed to get the command line argments");
		dtb_info.dtb_errno = BURST_NOT_RUN;
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		exit(dtb_info.dtb_errno);
	}

	//Allocate space for argv list to be built
	if ((argv = (char **)malloc(sizeof(char *) * nargs)) == NULL) 
	{
		dodebug(0, "WinMain()", "Malloc failed to allocate the required memory");
		dtb_info.dtb_errno = BURST_NOT_RUN;
		if (DE_BUG) 
		{
			fclose(debugfp);
			DE_BUG = 0;
		}
		if (cmd_line_list != NULL) 
		{
			GlobalFree(cmd_line_list);
		}
		exit(dtb_info.dtb_errno);
	}

	/*Loop through the number of arguments and convert them from wide characters to 
	ansi characters.  Then use the _strdup function to allocate the required string 
	length for each argument and put them into *argv[] so I can use the already 
	waiting program without any changes.*/
	for (i = 0; i < nargs; i++)
	{
		if ((converted_chars = WideCharToMultiByte(CP_UTF8, 0, cmd_line_list[i], -1, converted_cmd_line_arg, sizeof(converted_cmd_line_arg),  NULL, NULL)) == 0) 
		{
			dodebug(0, "WinMain()", "Failed to convert from wide characters to ansi");
			dtb_info.dtb_errno = BURST_NOT_RUN;
			had_error++;
			break;
		}

		if ((argv[i] = _strdup(converted_cmd_line_arg)) == NULL) 
		{
			int	j;
			for (j = 0; j < i; j++) 
			{
				free(argv[j]);
			}

			dodebug(0, "WinMain()", "strdup failed to allocate the required memory");
			had_error++;
			dtb_info.dtb_errno = BURST_NOT_RUN;
			break;
		}

	}

	if (concur_main(nargs, argv)) 
	{
		had_error++;
	}

	if (argv != NULL) 
	{
		free(argv);
	}

	if (cmd_line_list != NULL) 
	{
		GlobalFree(cmd_line_list);
	}

	if (DE_BUG) 
	{
		fclose(debugfp);
		DE_BUG = 0;
	}

	exit(!had_error ? SUCCESS : M9_ERROR);
}

#pragma warning(default : 4100)