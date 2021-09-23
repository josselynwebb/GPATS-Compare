/****************************************************************************
 *	File:	error.c															*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
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
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include <time.h>
#include <malloc.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "visa.h"
#include "visatype.h"
#include "DM_Services.h"
#include "callbacks.h"
#include "m910nam.h"

/****************************************************************************
 *	Local Constants															*
 ***************************************************************************/

char	*error_message[] = {
	"",
	"Improper file name passed to create_file()",					/*FILE_NAME*/
	"An error occur'd while writing a file",						/*FILE_WRITE_ERROR*/
	"An error occur'd while reading a file",						/*FILE_READ_ERROR*/
	"Improper usage of this function",								/*USAGE_ERROR*/
	"Character list passed don't equate",							/*LIST_ERROR*/
	"The arguments passed are of incorrect number",					/*ARGNUM*/
	"The system command call didn't work go figure",				/*SYS_CMD_ERROR*/
	"Improper file format, please check the magic header",			/*FILE_FORMAT_ERROR*/
	"The dtb file was not listed",									/*NO_DTB_STSTED*/
	"The circuit file for diagonstics was not listed",				/*NO_CIR_FILE*/
	"",
	"Improper character used, only Y or YES or N or NO allowed",	/*RESET_VALUE*/
	"Improper diagnostic value was passed, only n s p f allowed",	/*DIAG_VALUE*/
	"Improper character used, only Y or YES or N or NO allowed",	/*PIN_STATE*/
	"Improper character used, only Y or YES or N or NO allowed",	/*CONCURRENT*/
	"Improper permissions for stated directory",					/*DIR_WRITE_ERROR*/
	"Improper permissions or file not there for Diagnostics",		/*DIAG_FILE_WRITE_ERROR*/
	"Start_of_test_callback program does not exist",				/*S_O_T_CALL_ERROR*/
	"End_of_test_callback program does not exist",					/*E_O_T_CALL_ERROR*/
	"Probe_point_ready_callback program does not exist",			/*P_P_R_CALL_ERROR*/
	"Probe_sequence_started_callback program does not exist",		/*P_S_S_CALL_ERROR*/
	"Probe_sequence_ended_callback program does not exist",			/*P_S_E_CALL_ERROR*/
	"Probe_button_pressed_callback program does not exist",			/*P_B_P_CALL_ERROR*/
	"Diag_test_callback program does not exist",					/*D_T_CALL_ERROR*/
	"Start_of_pattern_callback program does not exist",				/*S_O_P_CALL_ERROR*/
	"End_of_pattern_callback program does not exist",				/*E_O_P_CALL_ERROR*/
	"Start_of_test_callback program ended with an error",			/*S_O_T_RET_ERROR*/
	"End_of_test_callback program ended with an error",				/*E_O_T_RET_ERROR*/
	"Probe_point_ready_callback program ended with an error",		/*P_P_R_RET_ERROR*/
	"Probe_sequence_started_callback program ended with an error",	/*P_S_S_RET_ERROR*/
	"Probe_sequence_ended_callback program ended with an error",	/*P_S_E_RET_ERROR*/
	"Probe_button_pressed_callback program ended with an error",	/*P_B_P_RET_ERROR*/
	"Diag_test_callback program ended with an error",				/*D_T_RET_ERROR*/
	"Start_of_pattern_callback program ended with an error",		/*S_O_P_RET_ERROR*/
	"End_of_pattern_callback program ended with an error",			/*E_O_P_RET_ERROR*/
	"Improper value used, Allowed values are from 0 - 9 only",		/*PROBE_STAB_VAL*/
	"Improper character used, only Y or YES or N or NO allowed",	/*PROBE_RESET_VAL*/
	"Improper value used, Allowed values are from 0 - 9 only",		/*PROBE_MIS_VAL*/
	"Improper value used, Allowed values are from 0 - 9 only",		/*MAX_SEED_VAL*/
	"",
	"",
	"",
	"",
	"Diagnostic directory name is improper",						/*DIAG_DIR_ERROR*/
	"Diagnostic directory name is a file",							/*DIAG_DIR_IS_FILE*/
	"Improper character used, only Y or YES or N or NO allowed",	/*PROBE_NOWAIT*/
	"The M910 is still running a concurrent program, Improper USE",	/*CONCURRENT_RUNNING*/
	"Couldn't kill the process either do it manual or REBOOT",		/*CONCUR_WONT_DIE*/
	"The process to be terminated is not the same as requested",	/*PROCESS_DONT_AGREE*/
	"Failed to get a handle to the named PIPE file",				/*PIPE_HANDLE_ERROR*/
	"The concurrent was improperly killed, not with quit",			/*IMPROPER_SHUTDOWN*/
	"Failed to write the message to the pipe",						/*WRITE_MSG_ERROR*/
	"Failed to read the message from the pipe",						/*READ_MSG_ERROR*/
	"Failed to start the concurrent process properly",				/*CONCURRENT_ERROR*/
	"The child process is running with no way to kill it",			/*CHILD_RUN_WILD*/
	"Callback program does not exist, Please check for file name",	/*CALLBACK_FILE_ERROR*/
};

/****************************************************************************
 *	Modules																	*
 ***************************************************************************/

/*
 *	error_ms:		This function will format and print an error message to a
 *						file that passed to this function. It will first see if
 *						that file is opened if not, open it, then write to it.
 *
 *	Parameters:
 *		code:			Index into the predefined error messages, if code = 0,
 *						then use the character string that is passed to it.
 *		function_name:	This is the name of the function the error happend.
 *		string:			This is used if the code = 0.
 *
 *	Returns:
 *		SUCCESS:		  0		= No errors hence success.
 *		M9_ERROR:		(-1)	= An error happened, because the file couldn't be opened.
 *
 */

int error_ms(int code, char *function_name, char *string)
{

	if (debugfp == NULL) {

		char	tmpfilename[1024];

		sprintf(tmpfilename, "%s%s", LOGLOCATION, DEBUGFILENAME);

		if ((debugfp = fopen(tmpfilename, "wb")) == NULL) {
			return(M9_ERROR);
		}
	}

	
	fprintf(debugfp, "%s in %s\r\n", code == 0 ? string : code < 700 ? strerror(code) :
							error_message[ code == 999 ? code - 954 : code < 800 ?
							code - 700 : code - 792 ], function_name);

	fflush(debugfp);

	return(SUCCESS);
}


/*
 *	dodebug:		This function will check to see if the DEBUG variable is
 *						set, if so then call error_ms. If error_ms returns
 *						with an error then unset the DEBUG variable.
 *
 *	Parameters:
 *		code:			Index into the predefined error messages.
 *		function_name:	This is the name of the function the error happend.
 *		string:			This is used if the code = 0.
 *
 *	Returns:
 *		NONE:		This function is a void function.
 *
 */

void	dodebug(int code, char *function_name, char *string)
{

	if (DE_BUG) {
		if (error_ms(code, function_name, string)) {
			DE_BUG = FALSE;
		}
	}
}
