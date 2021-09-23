/////////////////////////////////////////////////////////////////////////////
//	File:	gpnam.cpp														/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		0.31	Assigned it a version number.								/
//		0.32	In the function insert_into_fault_file(), modified the what	/
//				I am looking for.  Now after I find Guided I now look for a	/
//				'D', 'P', 'O', 'T'.  These are what I have found that		/
//				Teradyne uses for different fault statements. After I find  /
//				the above I then look for DIAGNOSED FA for the D,			/
//				Possible Fee for the P, Test is Unst for the T and			/
//				OPEN detecte for the O.	Also had to redo how I check for	/
//				end of the line on the previous character, this is do to	/
//				Possible being at the start of the line where the others	/
//				start at the fourth charater on the line.  I use a variable /
//				not_first_time to solve this.								/
//		1.0.0.0	Needed to recode the entire nam.  Could't suspend ATLAS		/
//				while the nam ran and allow the VB program to run			/
//				concurrently.												/
//		1.0.1.0	gpnam_main.cpp												/
//				Changed the exit call at the end of the function to a return/
//				call.  The exit call is improper and a return is the correct/
//				way to return from ther function.							/
//				insert_file.cpp												/
//				Removed the variable declaration of the FILE *dtbfp and		/
//				the declaration int dtbfd.									/
//				Moved the _stat function call up to just before the opening	/
//				of the files for simplicity.								/
//				Changed the fopen function call to a _open function call	/
//				opening the dtb_file_buf.									/
//				Changed the fread function call to a _read function call	/
//				this seems to be the problem with Microsoft API call when	/
//				the file size was 1312 bytes long, the OS would buffer it	/
//				and would not release the buffer so the file pointer could	/
//				be closed.  Fixed the call byu going to an unbuffered		/
//				function call.												/
//				Removed the 2 fclose function calls and added the function	/
//				call _close right after the _read dtbfd function call.  I	/
//				only needed the 1 function call instead of the 2 and the	/
//				function call had to be changed to _close any way.			/
//				Modified the for loop in t he Guided_detected section.		/
//				Thought that this was causing a buffer overrun the way it	/
//				done, it wasn't, but this way will prevent it from happening/
//				Instead of looking for the last character being a NULL, I	/
//				now look for i < dtbfz.st_size.								/
// 		1.1.0.0 gpnam.h														/
// 				Added the #defines IP_ADDRESS; ADDRESS; MASK; NAME; PSWD;	/
// 				PSEXECDIR; and PSEXEC.  Added address and mask to gp_info	/
// 				structure.  These were added to the gpnam to allow for the	/
// 				setting of the ip address of the controller by either an	/
// 				ATLAS or any executable program.  This will allow for a		/
// 				non-privilege user to set the ip address.  Also added the	/
// 				function call setAddress and setArgumentValue for the same.	/
// 				The #define ADDRESS_FILE was also added for the above same.	/
// 				setAddress.cpp												/
// 				Assigned it a version number.  The function call was added	/
// 				to allow for a non-privilege user (operator) to set the ip	/
// 				address of the controller.									/
// 				gpnam_main.cpp												/
// 				Changed the way the gpnam_main function works.  First the	/
// 				function will find out if it is being called by an ATLAS	/
// 				program or from another executable program.  It will do this/
// 				by checking to see if the .tmp is present and setting the	/
// 				flag ATLAS depending if the file is present or not.  Added	/
// 				function call setArgumentValue to check for proper setting	/
// 				of the command line variables if either an ATLAS program or	/
// 				a non-ATLAS program.  Added an element to the case statement/
// 				IP_ADDRESS  the element will check to ensure an address and	/
// 				a mask was sent and then call the function setAddress.		/
// 				Added the function call setArgumentValue to simplify the	/
// 				checking for proper types of variables being passed to the	/
// 				gpnam on the command line of an ATLAS or a non-ATLAS program/
//		1.2.0.0	gpconcur.h													/
//				Deleted the variable PROG_BIN_DIR, this is no longer needed	/
//				the path environmental takes its' place						/
//				gpnam.h														/
//				Added the defines INSERT_ADDITONAL_INFO - allow for the		/
//				insertion of user specified info on fault callout, IDE XML -/
//				used to determine the file type that is to have the user	/
//				specified info inserted into,LOCALNETWORK, GIGABIT1,		/
//				GIGABIT2, RESETPORT, GATEWAY, and PORTNAME - used to set or	/
//				reset the ethernet ports. Added IADS_3_2_7 and IADS_3_4_13 -/
//				used to see which iads executable was used. Added			/
//				GIGABIT1ADDR, GIGABIT1GATE, GIGABIT2ADDR, GIGABIT2GATE, and	/
//				DEFAULTMASK - these are port address used to reset the		/
//				different ethernet ports. Added Gateway and NetworkName to	/
//				the GP_INFO structure used to determine which port is to be	/
//				set/reset. Added new functions definitions for the new		/
//				capabilities added to gpnam.								/
//				gpnam_main.cpp												/
//				gpnam_main() - Added the variable NetworkPort - this is set	/
//				to the name passed to program. Remove the error checking for/
//				the number arguments being passed, now the number can be	/
//				unknown. Using the new reset function for the case statement/
//				RESET_IP. Using the new set ip function for the case		/
//				statement IP_ADDRESS.										/
//				setArgumentValue() - Added the element StringSize to the	/
//				passed variables, this is used to prevent buffer overflow.	/
//				Changed the sprintf to _snprintf to prevent buffer overflow./
//				Deleted one dodebug statement in the case element			/
//				CMD_LINE_INT.												/
//				InsertAdditionalInfo.cpp									/
//				InsertAdditionalInfo() - New function to allow for the test	/
//				program developer to insert additional information into the	/
//				fault file and FHDB.mdb.									/
//				FixTmpFile() - New this function insert the new info into	/
//				m910nam .ide/.xml.											/
//				process_utils.cpp											/
//				start_poroc() - Changed the sprintf call for the			/
//				concur_process, no longer need PROG_BIN_DIR, the env path is/
//				set to look in the directories for the executables that are	/
//				used.														/
//				setAddress.cpp												/
//				setAddress() - Corrected the function header info. Deletede	/
//				the variables finalSet, set, and reset, added new variable	/
//				SetIP. This was required because the function now performs	/
//				more capabilities plus depending on what is being set the	/
//				setting is different. Changed the if test to check for		/
//				RESET_IP, this is where the different settings are done.	/
//				Changed the fprintf to accommodate the above changes.		/
//				DoResetIP() - This is a new function, it will do the setting/
//				of variable NetworkPort.									/
//				DoSetIP() - This is a new function, it will do the setting	/
//				of variable NetworkPort.									/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <sys/types.h>
#include <sys/stat.h>
#include <wchar.h>
#pragma warning (disable : 4035 4068)
#include <windows.h>
#pragma warning (default : 4035 4068)
#include <winnls.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <direct.h>
#include <io.h>
#include <wchar.h>
#include "Constants.h"
#include "pipe.h"
#include "gpnam.h"
#include "gpconcur.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

int				DE_BUG;
int				ATLAS;
unsigned int	DriveType;
FILE			*debugfp;
GP_INFO			gp_info;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	WinMain:	This is the main entry point for this program. This function/
//					has been incorperated into an already existing program	/
//					so that a console window will not appear.				/
//																			/
//	Parameters:																/
//		hInstance:		A Microsoft object handle for this program NOT USED./
//		hPrevInstance:	A Microsoft object handle NOT USED.					/
//		lpCmdLine:		Pointer to a null-terminated string specifying the	/
//							command line for the application, excluding the	/
//							program name.									/
//		nCmdShow:		Specifies how the window is to be shown.			/
//																			/
//	Returns:																/
//		SUCCESS:		0    - No errors, just like it is suppose to do.	/
//		GP_ERROR:		(-1) - Whoops had an error.							/
//																			/
/////////////////////////////////////////////////////////////////////////////

#pragma warning(disable : 4100)

int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{

	int				nargs, i, converted_chars, had_error = 0;
	WCHAR			**cmd_line_list;
	char			**argv;
	char			converted_cmd_line_arg[GP_MAX_PATH];
	struct _stat	tmp_buf;
	dodebug(0, "GPNAM()", "Test", (char*)NULL);
	//MessageBox(NULL, "GP NAM", "Debug", MB_OK);
//
// Setup some defaults, that are depended upon to be set to the proper
// values for whatever.
//
	sprintf(gp_info.log_location, "%s", LOGLOCATION);
	sprintf(gp_info.log_file, "%s", FAULT_FILE);
	sprintf(PipeInfo.PipeFile, "%s%s", PIPE_FILE_HEADER, PIPE_FILE);
	sprintf(gp_info.debug_option, "%s%s", gp_info.log_location, DEBUGIT);

//
// Find out if the operator wants to have debug info.  If it is to provide
// debug inf then the env DEBUGSOURCE should be set.  If set then try to open
// debug file and set DE_BUG TRUE, if can't be opened then set DE_BUG to FALSE.
//

	DE_BUG = _stat(gp_info.debug_option, &tmp_buf) == GP_ERROR ? 0 : 1;

	if (DE_BUG) {

		char	debugfile[GP_MAX_PATH];

		sprintf(debugfile, "%s%s", gp_info.log_location, DEBUGFILENAME);

		if ((debugfp = fopen(debugfile, "w+b")) == NULL) {
			DE_BUG = FALSE;
		}
	}


// Get the command line arguments.

	if ((cmd_line_list = CommandLineToArgvW(GetCommandLineW(), &nargs)) == NULL) {
		dodebug(0, "WinMain()", "%s", "Failed to get the command line argments");
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
	}

//
// If we make it this far then we know the number of characters strings that will
// be in the argv list that we are going to build.  So now we will malloc the space
// for them.
//

	else if ((argv = (char **)malloc(sizeof(char *) * nargs)) == NULL) {
		dodebug(0, "WinMain()", "%s", "Malloc failed to allocate the required memory");
		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
		if (cmd_line_list != NULL) {
			GlobalFree(cmd_line_list);
		}
	}

//
// Now we will loop through the number of arguments and convert them from gates
// wide characters to ansi characters.  Then use the _strdup function to allocate
// the required string length for each argument and put them into *argv[] so I can
// use the already waiting program without any changes.
//

	else {
		for (i = 0; i < nargs; i++) {

			if ((converted_chars = WideCharToMultiByte(CP_UTF8, 0, cmd_line_list[i], -1,
													   converted_cmd_line_arg,
													   sizeof(converted_cmd_line_arg),
													   NULL, NULL)) == 0) {
				dodebug(0, "WinMain()", "%s", "Failed to convert from wide characters to ansi");
				had_error++;
				break;
			}

			if ((argv[i] = _strdup(converted_cmd_line_arg)) == NULL) {

				int	j;

				for (j = 0; j < i; j++) {
					free(argv[j]);
				}

				dodebug(0, "WinMain()", "%s", "strdup failed to allocate the required memory");
				had_error++;
				break;
			}

		}
		
		//dodebug(0, "WinMain()", "Argv is [%s] [%s] [%s] [%s] [%s] [%s] [%s] [%s] [%s] [%s]", argv[0],argv[1],argv[2],argv[3],argv[4],argv[5],argv[6],argv[7],argv[8],argv[9], (char*)NULL);
		if (gpnam_main(nargs, argv)) {
			had_error++;
		}

		if (argv != NULL) {
			free(argv);
		}

		if (cmd_line_list != NULL) {
			GlobalFree(cmd_line_list);
		}

		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = 0;
		}
	}

	return(!had_error ? SUCCESS : GP_ERROR);
}

#pragma warning(default : 4100)
