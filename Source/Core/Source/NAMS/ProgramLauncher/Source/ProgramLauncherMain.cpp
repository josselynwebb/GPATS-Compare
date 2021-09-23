/////////////////////////////////////////////////////////////////////////////
//	File:	ProgramLauncherMain.cpp											/
//																			/
//	Creation Date:	14 March 2013											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0	Assigned it a version number.								/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <windows.h>
#include "Constants.h"
#include "ProgramLauncher.h"
#include "nam.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

PROG_INFO		ProgInfo;
PROC_INFO		ProcInfo;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// ProgramLauncherMain:	This program will check the variables that were passed
//						to it and if correct will set up the defaults and then
//						call the different functions in proper order.
//
// Parameters:
//		argc:		The # of arguments passed to concur_main.
//		argv:		Character list of the arguments passed to concur_main.
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int ProgramLauncherMain (int argc, char *argv[])
{
	//MessageBox(NULL, "Program Launcher Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	long	ArgumentAddr;

	if ((DoInitalSetup(argc, argv)) != SUCCESS) {
		return(ProgInfo.ProgErrno);
	}

	if (ParseArguments(argc, argv)) {
		ProgInfo.ProgErrno = NAM_ERROR;
	}

	if (!TestForFile()) {

		BuildCommandString();

		if (!StartProc()) {

			if (ProgInfo.LaunchMode) {

				if (WaitOnTheProcess()) {

					TerminateTheProcess();
					ProgInfo.ProgErrno = NAM_ERROR;
				}
			}
		}
		else {
			ProgInfo.ProgErrno = NAM_ERROR;
		}
	}
	else {
		ProgInfo.ProgErrno = NAM_ERROR;
	}

	ArgumentAddr = atol(argv[RETURN_STATUS]);

	if (ProgInfo.ProgErrno == 0) {
		vmSetInteger(ArgumentAddr, ProgInfo.ExitStatus);
	}
	else {
		vmSetInteger(ArgumentAddr, ProgInfo.ProgErrno);
	}

	vmClose();

	return(SUCCESS);
}

//
//	DoInitalSetup:	This function connection to the Atlas vm, check to
//					see if the correct number of variables were passed.
//
//	Parameters:
//		argc:		The number of arguments passed to the program.
//		argv:		The list of said passed arguments.
//
//	Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a task.
//

int DoInitalSetup(int argc, char *argv[])
{

	if (argc != 6) {
		ProgInfo.ProgErrno = ARGNUM;
		dodebug(ARGNUM, "DoInitalSetup()", (char*)NULL, (char*)NULL);
		return(NAM_ERROR);
	}

	if (vmOpen(argv[TMP_FILE]) < 0) {
		ProgInfo.ProgErrno = FILE_OPEN_ERROR;
		dodebug(0, "DoInitalSetup()", "Cannot open virtual menory %s", argv[TMP_FILE], (char*)NULL);
		return(NAM_ERROR);
	}

	return(SUCCESS);
}
//
// ParseArguments:	This function will parse through the argv list, checking
//						for proper usage.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
//

int ParseArguments(int argc, char *argv[])
{

	int		i = 0;
	long	ArgumentAddr;
	char	TmpBuf[1024];

	for (i = 2; i < argc; i++) {

		ArgumentAddr = atol(argv[i]);
		memset(TmpBuf, '\0', sizeof(TmpBuf));

		if (i < 5) {

			if (vmGetDataType(ArgumentAddr) != TTYPE) {
				dodebug(DATACHAR, "ParseArguments()", (char*)NULL, (char*)NULL);
				ProgInfo.ProgErrno = DATACHAR;
				return(NAM_ERROR);
			}
		}
		else if (i == 5) {

			if (vmGetDataType(ArgumentAddr) != ITYPE) {
				dodebug(DATAINT, "ParseArguments()", (char*)NULL, (char*)NULL);
				ProgInfo.ProgErrno = DATAINT;
				return(NAM_ERROR);
			}
		}
		else {

			dodebug(UNKOPT, "ParseArguments()", (char*)NULL, (char*)NULL);
			dodebug(0, "parseArguments()", "i = %d", i, (char*)NULL);
			ProgInfo.ProgErrno = UNKOPT;
			return(NAM_ERROR);
		}

		switch (i) {

			case EXECUTE_PROGRAM :

				vmGetText(ArgumentAddr, TmpBuf, sizeof(TmpBuf));
				sprintf(ProgInfo.FileName, "%s", TmpBuf);

				break;

			case CMD_LINE_OPTION :

				vmGetText(ArgumentAddr, TmpBuf, sizeof(TmpBuf));
				if (_strnicmp(TmpBuf, "NONE", strlen("NONE"))) {
					sprintf(ProgInfo.CommandOptions, "%s", TmpBuf);
				}

				break;

			case OPERATION_MODE :

				vmGetText(ArgumentAddr, TmpBuf, sizeof(TmpBuf));
				ProgInfo.LaunchMode = (_strnicmp(TmpBuf, "Normal", strlen("Normal"))) == 0 ? 1 : 0;
				dodebug(0, "ParseArguments()", "ProgInfo.LaunchMode = %d, string passed = %s",
						ProgInfo.LaunchMode, TmpBuf, (char*)NULL);

				break;
		}
	}

	return(SUCCESS);
}
