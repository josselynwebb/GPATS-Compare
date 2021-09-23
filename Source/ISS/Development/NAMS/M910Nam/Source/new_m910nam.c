/******************************************************************************
 *	M910NAM.c  -  Source code for:
 *	NAM to interface with M910 DTI
 *
 *	File Creation Date: 5/19/98
 *  Created By: Grady Johnson
 *
 *	Revision Log:
 *  V1.1 7/8/99 GJohnson
 *		Added the use of Teradyne Function DM_setDiagnosticOutputDir
 *		to set the directory that the Diagnostics file is written
 *		to.  Also added code to rename file and add the UTC time
 *		stamp to the end of the file name so multiple files could
 *		be written for the same UUT.
 *  V1.2 6/15/00 GJohnson
 *		Added two additional diagnostic Flag Values: 2 = "Just 
 *		Fault History Database" 3 = "Just Guided Probe".  Value 1 =
 *		"Seeded Guided Probe Diagnostics".  Removed the strcat command
 *		used to add the file extention to the Circuit File name.  
 *		Commented out the term9_initializeInstrument command.  This 
 *		command was causing the M910 to reset even when the DTSResetFlag 
 *		was set not to reset.  These corrections were a result of 
 *		Government STR Ticket number 227 and 446, ManTech ID # 422 and
 *		432.  They were implemented per ECO-3047-433.
 *  V1.3 4/4/01 GJohnson, DRs 77, 126, 127, ECO-3047-536
 *		1. Removed multiple DIA file creation.
 *		2. Add functions and case statements to write specific failure
 *		   information for Component/Pin/Net and Failure Data into the
 *		   testname.DIA file.
 *		3. Add two additional arguments which pass back the failure 
 *		   information to the ATLAS program.  
 *		4. Added function to create a *.IDE file out of the contents 
 *		   of the *.DIA file for viewing within the IETM NAM in the 
 *		   IADS Document Reader.
 *  V1.4, 8/2/01 JHill, DR 127, ECO-3047-549
 *		1. Updated project with latest versions of:
 *		   dm_services.h
 *		   diagnostics.h
 *		   visa.h, visatype.h, vpptype.h
 *		   terM9.h, Term9.lib
 *		   DiagMgr.lib
 *		2. Rename "<DTBFile>.dia/.ide" to "M910NAM.dia/.ide" to 
 *		   further reduce the number of leftover files in C:\APS\Data.
 *  V1.5, 8/7/01 JHill, DR 127, ECO-3047-549
 *		1. Changed the IDELogFile variable, which gets returned as the
 *		   ResultsFile, to the new generic name: LOGLOCATION"M910NAM.IDE".
 *		2. When creating the .IDE file from the .DIA file, changed
 *		   all occurances of < and > to [ and ]. This is necessary
 *		   to prevent the IADS viewer from interpreting strings
 *		   with < or > in them as SGML tags.
 *	V2.0, 
 *		1. This is a complete rewrite which will allow backward compatablitiy
 *		   with released APS, but will allow the M910NAM to be more flexiable
 *		   for future use.
 *	V2.1   16 Oct 2002 R. Chaffin
 *		1. Removed a DM_getMaxSeedingFaultSetsValue and fprintf function call
 *		   from the function do_diag.  The fprintf function call was attempting
 *		   to write to an unopen file.
 *		2. Removed a fprintf function call from the function fill_in_struct_data
 *		   function.  The fprintf function call was attempting to write to an
 *		   unopen file.
 *	V2.2   14 Nov 2002 R. Chaffin
 *		1. Forgot to added the function dm_setProbeResetEnable in the function
 *		   do_diag.
 *		2. In the function fill_in_struct_data() the case statement for
 *		   CIRCUIT_FILE, this case statement was modified to first see if the
 *		   file was either an absolute or relative file. If	relative then prefix
 *		   the file name with the current working directory, else do nothing.
 *		   It use to remove any path info and just get the file name but this
 *		   caused the error checking that was added at a latter time to not be
 *		   able to find the *.cxs file.
 *	V2.3   13 May 2003 R. Chaffin
 *		1. Added the Block Header Static Storage to the file callbacks.c
 *		2. In the function create_ide() added the declaration FILE *tmpfp, char 
 *		   dummy_info which was filled with an info statement.  Added a comment
 *		   above the call to the function create_file.  The call to the function
 *		   create_file was modified to correct for an error that was caused when
 *		   a static program is run.  The running of a static program does create a
 *		   .dia file, so error checking is done to see if the function call to create
 *		   fails, if so then see if the file was going to be copied exist.  If it
 *		   doesn't create it with the info in the character string dummy_info, and
 *		   try the function call create_file one more time. If it does	exist then
 *		   error out because Microsoft has problems.
 *		3. Modified the if statement just before the passing back the arguments
 *		   Fault Callout and Fault Message.  The if statement now also checks to
 *		   insure that the arguments were passed to the M910nam.  This error checking
 *		   slipped through the crack caused by poor checking of the M910nam.
 *		4. Modified the end of the function oldm910way(), instead of checking for
 *		   argc count being > 6 it now checks for argc > 8 also added an additional
 *		   check to see if the programmer the Fault Callout and Fault Message passed back.
 *		5. Added the initilization of 2 more variables to the begining of the main
 *		   function. These are fault_call set to "none" and fault_mess set to " "
 *	V2.4   2 Sept 2003 R. Chaffin
 *		1. Corrected an error induced by #5 of Version 2.3, was comparing argc
 *		   with a constant when I should have been comparing (argc -1) against
 *		   the constant in the function main() where I checked to see if they
 *		   want the Fault Callout and Message.
 *	V2.5   18 Oct 2004 R. Chaffin
 *		1. Modified the header block for the function probe_button_pressed.
 *		   Made it correct sort of.
 *		2. Corrected the header block for the function do_diag_output.
 *		   Modified the way the variable diagtype is set was changed.
 *		   Modified the return message and fault callout to meet the documented M910nam
 *		   user guide.
 *		   Added the new function set_dir_file which is used to set the effective working
 *		   directory to the digital directory.
 *		   Rewrote the function diag_setup, easier to read.
 *		   Corrected the header block forthe function do_diag.
 *		   Modified the way the output directory was set.  This was needed to correct
 *		   STR#744.  This now gets done in the function set_dir_file.
 *		   Modified the code in the DM_diagnoseTest function block to allow for more debuging
 *		   if an error happens, and to allow the ATLAS programmers more info on failure.
 *		   Added the calls to change_dir to the function do_diag, this call allows for
 *		   the fix that was done for STR#744.
 *		3. Corrected the header block for the function format_error.
 *		   Corrected the header block for the function close_dti.
 *		   Corrected the header block for the function do_dtb.
 *		   Added the calls to the change_dir function, these were required to fix the
 *		   problem related to STR#744.
 *		4. Added the function call fflush to the end of the function create_file.
 *		   In the function insert_text, a break statement was added to the for loop where
 *		   the insertation happens.  Once an error was found I didn't break out of the loop
 *		   which could cause a Dr. Watson.
 *		   Corrected the header block of the function check_dtb_cxs_exten.
 *		   In the function check_dtb_cxs_exten a call to the function convert_files_set_dir was
 *		   added.  This was done due to the fact that the directory that the digital program
 *		   resides was needed so that the working directory could be changedto that prior to
 *		   execution of the digital burst.
 *		   Corrected the header block of the function create_ide
 *		   In the fill_in_struct_data function correct the lower limit check for both
 *		   PROBE_STABILITY_COUNT and MAX_SEEDING_VALUE the value zero is not allowed.
 *		   Added the function change_dir, this is used to change to the directory of where
 *		   the digital program or the ATLAS program resides to fix STR#744.
 *		   Added the convert_files_set_dir function, this will change the .dtb and .cxs files
 *		   to just the file names with no path info.  It will also set the dtb_info.digital_dir
 *		   from the path info ofthe full dtb path name.  to fix STR#744
 *		5. Removed the return statement from the case TERM9_RESULT_FAIL statement in the
 *		   new910way function.  Now it increments a had_error flag.  This was done to allow
 *		   for a diagnose message to be return to the ATLAS program.
 *		   Changed the casing of the dtb_info.fault_call to match the M910NAM user guide.
 *		6. Removed the return statement from the case TERM9_RESULT_FAIL statement in the
 *		   olm910way function.  Now it increments a had_error flag.  This was done to allow
 *		   for a diagnose message to be return to the ATLAS program.
 *		7. Added the include declaration for sys/stat.h which is used for the new setting of
 *		   the DE_BUG flag.  Also added the declaration of the tmp_buf in the main function
 *		   which will be used in the call _stat, which is also used for the new way of setting
 *		   the DE_BUG flag.  Also modifed the code where the setting of the DE_BUG flag is
 *		   changed from checking the enviromental variable to checking to see if a file exists.
 *		   Changed the casing of the variable dtb_info.fault_call to match the M910NAM user's
 *		   guide.
 *		   Added the call to initialize the variable dtb_info.debug_option which is the location
 *		   and file name to check to see if debug should be set.
 *		   In the main function modified the code on where it is checking weither or not to pass
 *		   back a fault callout and fault message.  This was done to meet the user's guide.
 *		   In the fill_in_data function modified the code on where it is checking the circuit
 *		   file that was passed to it.  Left off the checking for the '/' element of the path
 *		   which is allowed.
 *	V2.6   27 June 2016 K. D'Arcangelis
 *		 1. Moved to New NAM Library 
 *				
 ******************************************************************************/

 /****************************************************************************
*		Constants														*
****************************************************************************/

#define OSESOK 0
#define OSRSERR 1
#define STRING_SIZE 2048
/****************************************************************************
*		Include Files														*
****************************************************************************/

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <dos.h>
#include <time.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/timeb.h>
#include <direct.h>
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include "visa.h"
#include "visatype.h"
#include "terM9.h"
#include "nam.h"
#include "diagnostics.h"
#include "DM_Services.h"
#include "callbacks.h"
#include "m910nam.h"
#include "m9concur.h"


/****************************************************************************
 *		Declarations														*
 ***************************************************************************/

int				DE_BUG;
FILE			*debugfp;
CB_INFO			*cb_info;
DTB_INFO		dtb_info;
CONCUR_INFO		concur_info;
PIPE_INFO		pipe_info;
char			x_String[STRING_SIZE];
int				x_Integer;
long			x_Long;
FILE			*FaultCalloutFp, *FaultMessageFp;
int				ATLAS = 0;



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
	
	//MessageBox(NULL, "M910 Nam", "clean_up", MB_OK);  //This is used for debugging purposes, do not remove
	if (dtb_info.opened) 
	{
		if (!dtb_info.pin_state)
		{
			terM9_reset(dtb_info.dti_handle);
		}
		terM9_close(dtb_info.dti_handle);
	}

	if (pipe_info.pipehd != NULL) 
	{
		CloseHandle(pipe_info.pipehd);
	}

	if (dtb_info.alldone == FALSE) 
	{
		vmClose();
	}

}

/*
 * main:		This program will check the variables that were passed to
 *					it and by these variables determine which program to
 *					execute and how it is executed.  If the value of argc
 *					is 9 or greater then the main will call a program that
 *					does the old way of the M910NAM.  If the value is less
 *					then 7 then it must be the new way, or there was an error.
 *
 * Parameters:
 *		argc:		The # of arguments passed to main. Duh
 *		argv:		Character list of the arguments passed to main. Duh
 *
 * Returns:
 *		OSESOK:		0		= Value known to TYX RTS for success.
 *		OSRSERR:	(-1)	= Value known to TYX RTS for an error.
 */

int main (int argc, char *argv[])
{

	int		Return_Status, Fault_Callout, Fault_Message;
	int		Fault_File, process_found;
	long	xad;
	char	*tmppt;
	char	TempFileName[M9_MAX_PATH];
	struct _stat	statBuf;  
	
	//MessageBox(NULL, "M910 Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	//Setup the function atexit so cleanup can be done.
	atexit(clean_up);

	/* Setup some defaults, just in case they aren't used or this is being run in the
	old mode of operation. */
	pipe_info.message_size = MAX_MSG_SIZE;
	sprintf(dtb_info.dtb_file, "%s", "null");
	sprintf(dtb_info.execute_prog, "%s", "null");
	sprintf(pipe_info.pipe_file, "%s%s", PIPE_FILE_HEADER, PIPE_FILE);
	sprintf(dtb_info.log_location, "%s", LOGLOCATION);
	sprintf(dtb_info.log_file, "%s", LOGFILE);
	sprintf(dtb_info.fault_call, "%s", "None");
	sprintf(dtb_info.fault_mess, "%s", " ");
	sprintf(dtb_info.debug_option, "%s%s", dtb_info.log_location, DEBUGIT);

	/*Find out if the program should be run in the DEBUG mode. If it is then open
	 * the Debug file, if that doesn't work then set DEBUG back to 0.  DEBUGSOURCE
	 * doesn't have to be set just used. */

	DE_BUG = _stat(dtb_info.debug_option, &statBuf) == M9_ERROR ? 0 : 1; 
	if (DE_BUG) 
	{

		char	debugfile[256];

		sprintf(debugfile, "%s%s", dtb_info.log_location, DEBUGFILENAME);

		if ((debugfp = fopen(debugfile, "w+b")) == NULL) 
		{
			DE_BUG = FALSE;
		}
	}

	

	
	// First find out if program is being run from ATLAS or not

	ZeroMemory(TempFileName, sizeof(TempFileName));
	sprintf(TempFileName, "%s.%s", argv[TMP_FILE], TMPSUFFIX);
	dodebug(0, "main()", TempFileName);
	ATLAS = _stat(TempFileName, &statBuf) == M9_ERROR ? 0 : 1;
	{
		char	tmpString[80];
		sprintf(tmpString, "ATLAS is %d", ATLAS);
		dodebug(0, "main()", tmpString);
	}

	// Find out which iads version is being used by etm/etm_monitor
	dtb_info.iads_version = _stat(IADS_3_2_7,  &statBuf) == 0 ? BASE327 : _stat(IADS_3_4_13, &statBuf) == 0 ? BASE3413 : (!ATLAS) ? BASE3413 : M9_ERROR; 

	// Initialize the communication library if an ATLAS program is executing this
	// program otherwise do nothing here.

	if (ATLAS) 
	{
		//Define ATLAS temp file name and open it
		sprintf(TempFileName, "%s.%s", argv[1], TMPSUFFIX);
		vmOpen(TempFileName);
	}



	//Set up some defaults before we get into it.
	dtb_info.probe_nowait		= DIAG_ENABLE;
	dtb_info.alldone			= FALSE;
	dtb_info.concurrent_running = FALSE;
		
	sprintf(TempFileName, "%s%s", LOGLOCATION, FAULT_CALLOUT);

	if ((FaultCalloutFp = fopen(TempFileName, "w+")) == NULL) {
		FaultCalloutFp = NULL;
	}
	
	sprintf(TempFileName, "%s%s", LOGLOCATION, FAULT_MESSAGE);

	if ((FaultMessageFp = fopen(TempFileName, "w+")) == NULL) {
		FaultMessageFp = NULL;
	}

	/*Check if the program m9_concur.exe is running.  If it is
	set the bit concur_info.concurrent_running to TRUE for later use. */

	sprintf(concur_info.process_name, "%s", PROCESS_NAME);

	if ((process_found = find_process(concur_info.process_name)) == M9_ERROR) 
	{
		dodebug(0, "main()", "Couldn't access the running process at all get sysadmin help");
	}

	/* If argc is 9 or 11 then this was called under the old M910NAM, so we will
	 * do it the old way, with a slight modification on how the .ide file
	 * is created.*/
	if (((ATLAS) && (argc == 9 || argc == 11) && process_found != M9_ERROR)) 
	{  

		dtb_info.method = OLD;
		dtb_info.return_value = oldm910way(argc, argv) == SUCCESS ? OSESOK : OSRSERR;

		if (dtb_info.dtb_errno == BURST_FAILED && dtb_info.diag_type) 
		{
			xad = atol(argv[FAULT_FILE_OLD_WAY]);

			vmGetText(xad, x_String, STRING_SIZE);
			sprintf(x_String, "%s%s", LOGLOCATION, LOGFILE);
			vmSetText(xad, x_String);

		}

	}

	//If argc is 5 or 7 then the programmer wants to do it the new way.
	else if (((argc == 5 || argc == 7)  && process_found != M9_ERROR) || (argc < 4 && (!ATLAS))) 
	{
		dtb_info.method = NEW;
		dtb_info.return_value = newm910way(argc, argv) == SUCCESS ? OSESOK : OSRSERR;
	}

	else 
	{
		if(ATLAS)
		{ 
			vmClose();
		}
		dtb_info.alldone = TRUE;

		if (DE_BUG) 
		{
			fclose(debugfp);
		}

		exit((!ATLAS) ? BURST_NOT_RUN : dtb_info.return_value);  
	}

	// Set the indexing values for argv so the proper variable will be properly accessed.
	// This is needed to keep from remembering the values for the different ways this program
	// is called.

	Return_Status = dtb_info.method == NEW ? RETURN_STATUS_NEW_WAY : RETURN_STATUS_OLD_WAY;
	Fault_Callout = dtb_info.method == NEW ? FAULT_CALLOUT_NEW_WAY : FAULT_CALLOUT_OLD_WAY;
	Fault_Message = dtb_info.method == NEW ? FAULT_MESSAGE_NEW_WAY : FAULT_MESSAGE_OLD_WAY;
	Fault_File    = dtb_info.method == NEW ? FAULT_FILE_NEW_WAY    : FAULT_FILE_OLD_WAY;

	/*Check to see diag was enabled and the programmer wants the info passed back.
	to Atlas.  Also see if they passed the argument so it can be filled. */
	if ((dtb_info.fault_info_requested) && ((dtb_info.method == NEW && (argc - 1) == FAULT_MESSAGE_NEW_WAY) || (dtb_info.method == OLD && (argc - 1) == FAULT_MESSAGE_OLD_WAY) || (!ATLAS))) 
	{

		/*Get the fault call out and stuff it back into the variable that Atlas uses.
		Only using 59 characters because that is all that will be displayed in fault
		Display, the rest are truncated. */

		if ((strlen(dtb_info.fault_call)) > 60) 
		{
			dtb_info.fault_call[59] = '\0';
		}

		if ((strlen(dtb_info.fault_mess)) > 60) 
		{
			dtb_info.fault_mess[59] = '\0';
		}

		if(ATLAS)
		{
			xad = atol(argv[Fault_Callout]);
			vmGetText(xad, x_String, STRING_SIZE);
			sprintf(x_String, "%s", dtb_info.fault_call);
			vmSetText(xad, x_String);

			/*Get the fault message and stuff it back into the variable that Atlas uses.
			Only using 59 characters because that is all that will be displayed in fault
			Display, the rest are truncated. */

			xad = atol(argv[Fault_Message]);
			vmGetText(xad, x_String, STRING_SIZE);
			sprintf(x_String, "%s", dtb_info.fault_mess);
			vmSetText(xad, x_String);
		}
		else
		{
			if (FaultCalloutFp != NULL) 
			{
				fprintf(FaultCalloutFp, "%s\n", dtb_info.fault_call);
			}
			if (FaultMessageFp != NULL) 
			{
				fprintf(FaultMessageFp, "%s\n", dtb_info.fault_mess);
			}
		}
	}

	//Return back to ATLAS the m910 result's file.
	sprintf(TempFileName, "%s%s", dtb_info.log_location, dtb_info.log_file);

	if ((tmppt = exten_routine(TempFileName, "dia", dtb_info.iads_version == BASE3413 ? "xml" : "ide")) != NULL) 
	{
		sprintf(TempFileName, "%s", tmppt);

	}
	else 
	{
		sprintf(TempFileName, "%s%s", dtb_info.log_location, dtb_info.iads_version == BASE3413 ? "m910nam.xml" : "m910nam.ide");
	}

	dodebug(0, "main()", TempFileName);


	/*Return the m910nam dtb/diag results variable.  Only pass back to the ATLAS program 
	if the burst passed; failed; didn't run; or an improper value was sent.  It was determined 
	that that is all the operator needed to know, the programmer can still find out what went wrong by viewing the
	debug file. */
	
	if (dtb_info.dtb_errno != BURST_FAILED   && dtb_info.dtb_errno != BURST_PASSED   && dtb_info.dtb_errno != BURST_NOT_RUN  
		&& dtb_info.dtb_errno != BURST_MAX_TIME && dtb_info.dtb_errno != DIAG_STATE_VAL) 
	{

		dtb_info.dtb_errno = BURST_NOT_RUN;
	}

	if(ATLAS)
	{
		xad = atol(argv[Fault_File]);
		vmGetText(xad, x_String, STRING_SIZE);
		sprintf(x_String, "%s", TempFileName);
		vmSetText(xad, x_String);

		xad = atol(argv[Return_Status]);
		x_Integer = vmGetInteger(xad);
		x_Integer = dtb_info.dtb_errno;
		vmSetInteger(xad, x_Integer);

		sprintf(TempFileName, "dtb_info.dtb_errno = %d", dtb_info.dtb_errno);
		dodebug(0, "newm910nam()", TempFileName);

		if(vmClose() < 0)
		{
			return (-1);
		}

	}
	

	dtb_info.alldone = TRUE;

	if (DE_BUG) 
	{
		fclose(debugfp);
	}

	exit((!ATLAS) ? dtb_info.dtb_errno : dtb_info.return_value);

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

int checkintvalues(void) {
	
	//MessageBox(NULL, "M910 Nam", "checkintvalues", MB_OK);  //This is used for debugging purposes, do not remove
	if (dtb_info.reset_flag < 0 || dtb_info.reset_flag > 1 || dtb_info.pin_state  < 0 || dtb_info.pin_state  > 1  || dtb_info.diag_type  < 0 || dtb_info.diag_type  > 3) 
	{
		char	tmpbuf[256];

		sprintf(tmpbuf,	"DTS Reset, Pin State, or Diag Type value is incorrect\r\n%s - %d\r\n%s - %d\r\n%s - %d\r\n", "DTS Reset should either 0 or 1 what was passed was", dtb_info.reset_flag,
				"Pin State should either 0 or 1 what was passed was", dtb_info.pin_state, "DIag Type should be between 0 and 3 what was passed was", dtb_info.diag_type);
		dodebug(0, "checkintvalues()", tmpbuf);
		dtb_info.dtb_errno = DIAG_STATE_VAL;
		return(M9_ERROR);
	}
	else 
	{
		return(SUCCESS);
	}
}
		

/*
 * fill_in_data:	This function will initilize and fill in the required
 *						elements that need to be.
 *
 * Parameters:
 *		index:			The index value pointing into the argv list.
 *		way:			This is the way the M910 nam was used.
 *
 * Returns:
 *		SUCCESS:		0 - successful, right always returns this.
 */
int fill_in_data(int index, int way, char *argv[])
{
	char	dirpath[M9_MAX_PATH];
	
	//MessageBox(NULL, "M910 Nam", "fill_in_data", MB_OK);  //This is used for debugging purposes, do not remove
	/*This is one of the fixes that will join m910nam.c with m910nama.c, so we can do away
	with all these different nams.	 */
	if ((_getcwd(dirpath, M9_MAX_PATH)) == NULL) 
	{
		dodebug(errno, "fill_in_data()", NULL);
		dtb_info.dtb_errno = BURST_NOT_RUN;
		return(M9_ERROR);
	}

	if (dirpath[(strlen(dirpath) - 1)] == '\\') 
	{
		dirpath[(strlen(dirpath) - 1)] = '\0';
	}

	switch(index) 
	{
		case DTB_FILE_NAME:

			dodebug(0, "fill_in_data()", "DTB_FILE_NAME");
			if (way == OLD) 
			{
				if ((x_String[0] == '\\') || (x_String[0] == '/') ||
				   (x_String[1] == ':' && x_String[2] == '\\')) {
					sprintf(dtb_info.dtb_file, "%s", x_String);
				}
				else 
				{
					sprintf(dtb_info.dtb_file, "%s\\%s", dirpath, x_String);
				}
			}

			else 
			{
				if (ATLAS) 
				{
					sprintf(dtb_info.file_name, "%s", x_String);
					dodebug(0, "fill_in_data()", dtb_info.file_name);
				}
				else
				{
					sprintf(dtb_info.file_name, "%s", argv[index - 1]);
					dodebug(0, "fill_in_data()", dtb_info.file_name);
				}
			}

			break;

		case CIRCUIT_FILE_NAME:

			if (way == OLD)
			{
				if ((x_String[0] == '\\') || (x_String[0] == '/') ||  (x_String[1] == ':' && x_String[2] == '\\')) 
				{
						sprintf(dtb_info.cir_file, "%s", x_String);
				}
				else 
				{
					sprintf(dtb_info.cir_file, "%s\\%s", dirpath, x_String);
				}
			}

			break;

		case RESET_DTI:

			if (way == OLD) 
			{
				dtb_info.reset_flag = x_Integer;
			}

			break;

		case DIAG_TYPE:

			if (way == NEW) 
			{
				dtb_info.fault_info_requested = 1;
			}
			else 
			{
				dtb_info.diag_type = x_Integer;
			}

			break;

		case PIN_STATE:

			if (way == OLD) 
			{
				dtb_info.pin_state = x_Integer;
			}

			break;

		case FAULT_INFO:

			dtb_info.fault_info_requested = 1;

			break;

		default:

			break;

	}
	return(SUCCESS);
}

/*
 * checkargs:	This function will check the arguments that were passed to the
 *					program are of proper type, if they weren't GOD only
 *					knows what will happen.  Arguments 3,4,8,10 & 11 if used
 *					will be of text type. Arguments 5,6,7 & 9 will be of 
 *					integer type. If the arguments aren't what they are
 *					suppose to be then error.
 *
 * Parameters:
 *		argc:		The # of arguments passed to main.
 *		argv:		Character list of the arguments passed to main.
 *		way:		This is which method the M910 nam was used.
 *
 * Returns:
 *		SUCCESS:	0    - successful completion of the function.
 *		M9_ERROR:	(-1) - something went wrong.
 */

int checkargs(int argc, char *argv[], int way)
{
	int		i;
	long	xad;
	
	//MessageBox(NULL, "M910 Nam", "checkargs", MB_OK);  //This is used for debugging purposes, do not remove
	{
		char	tmpString[80];
		sprintf(tmpString, "ATLAS is %d", ATLAS);
		dodebug(0, "checkargs()", tmpString);
	}

	for (i = ((!ATLAS) ? 1 : 2); i < argc; i++) 
	{
		/*Verify that the arguments are character strings*/
		if ((way == OLD && i < 4  || way == OLD && i == 7 || way == OLD && i > 8) || (way == NEW && i == 6 || way == NEW && i == 4 || way == NEW && i == 5 || way == NEW && i == 2 && ATLAS)) 
		{
			xad = atol(argv[i]);
			if (vmGetDataType(xad)!=TTYPE) 
			{
				char	tmpbuf[256];
				sprintf(tmpbuf, "The incorrect type of variable was used, needs to character");
				dodebug(0, "checkargs()", tmpbuf);

				if (way == OLD)
				{
					dtb_info.dtb_errno = BURST_NOT_RUN;
				}
				else 
				{
					dtb_info.dtb_errno = IMPROPER_ARG;
				}
				return(M9_ERROR);
			}
			else 
			{
				dodebug(0, "checkargs()", "Calling fill_in");
				vmGetText(xad, x_String, STRING_SIZE);
				fill_in_data(i, way, NULL);
			}
		}
		/*Verify that the arguments are integers*/
		else if ((way == OLD && i == 4 || way == OLD && i == 5 || way == OLD && i == 6 ||  way == OLD && i == 8) || (way == NEW && i == 3))
		{
			xad = atol(argv[i]);
			if (vmGetDataType(xad)!=ITYPE) 
			{
				char	tmpbuf[256];
				sprintf(tmpbuf, "The incorrect type of variable was used, needs to interger");
				dodebug(0, "checkargs()", tmpbuf);

				if (way == OLD)
				{
					dtb_info.dtb_errno = BURST_NOT_RUN;
				}
				else 
				{
					dtb_info.dtb_errno = IMPROPER_ARG;
				}
				return(M9_ERROR);
			}
			else 
			{
				x_Integer = vmGetInteger(xad);
				fill_in_data(i, way, NULL);
			}
		}
		// Here check for the arguments for the non-ATLAS way
		else if ((way == NEW && i == 2 && (!ATLAS)) || (way == NEW && i == 1 && (!ATLAS)))
		{
			dodebug(0, "checkargs()", "hi");
			fill_in_data((i == 1 ? (i + 1) : (i + 7)), way, argv);
		}
		// Should never get here, but you never know with Microslop software.
		else 
		{
			dodebug(0, "checkargs()", "Something is not well arg # didn't match");
			dtb_info.dtb_errno = BURST_NOT_RUN;
			return(M9_ERROR);
		}
	}

	return(SUCCESS);
}
