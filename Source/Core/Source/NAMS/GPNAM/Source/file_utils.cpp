/////////////////////////////////////////////////////////////////////////////
//	File:	file_utils.cpp													/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		0.31	Assigned it a version number.								/
//		1.0.0.0	Added 2 include statements to include direct.h and windows.h/
//				Added a new function get_drive_type.  This was done to see	/
//				what drive the test program was being run off of.  If it was/
//				not the CDROM then it is not a field developed TP and the	/
//				info doesn't need to be capatured or saved in the FaultFile	/
//				database used by the FaultFilePrintViewer program.			/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <sys/types.h>
#include <sys/stat.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <malloc.h>
#include <direct.h>
#pragma warning (disable : 4035 4068)
#include <windows.h>
#pragma warning (default : 4035 4068)
#include "gpnam.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// get_drive_type:	This program will find out what drive the TP is running	/
//						from.  This will be used to determine if the TP is	/
//						in a development mode or not.  This is the only way	/
//						now that this can be determined.					/
//																			/
// Parameters:																/
//		NONE			This function will set the global DriveType.		/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
/////////////////////////////////////////////////////////////////////////////

void get_drive_type(void)
{

	char	tmpbuf[GP_MAX_PATH];

//
// First I will get the current drive that we are working off of, then see if
// it is the CDROM Drive.
//

	tmpbuf[0] = '\0';

	sprintf(tmpbuf, "%c:\\", (_getdrive() + 'A' - 1));

	DriveType = GetDriveType((LPCTSTR)tmpbuf);

	return;
}

/////////////////////////////////////////////////////////////////////////////
// :	This program will find out what drive the TP is running	/
//						from.  This will be used to determine if the TP is	/
//						in a development mode or not.  This is the only way	/
//						now that this can be determined.					/
//																			/
// Parameters:																/
//		NONE			This function will set the global DriveType.		/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
/////////////////////////////////////////////////////////////////////////////

int GetSystemName(char *argv[])
{

	int		ReturnValue = 0;
	DWORD	ComputerSize;
	char	ComputerName[24] = {0};
	char	ReturnComputerName[MAX_COMPUTERNAME_LENGTH + 1] = {0};
	char	OutPutFileName[_MAX_PATH];
	FILE	*OutPutfp = NULL;

	if (ATLAS == 0) {
		sprintf(OutPutFileName, "%s%s", gp_info.log_location, COMPUTERNAME);

		if ((OutPutfp = fopen(OutPutFileName, "wb+")) == NULL) {
			gp_info.return_value = GP_ERROR;
			return gp_info.return_value;
		}
	}

	if (setArgumentValue(ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR, argv[COMPUTER + ATLAS],
						 ComputerName, sizeof(ComputerName), NULL)) {
	
		dodebug(0, "GetSystemName()", "Failed to get option value", (char *)NULL);
		gp_info.return_value = GP_ERROR;
		return(gp_info.return_value);
	}

	ComputerSize = sizeof(ReturnComputerName) / sizeof(TCHAR);

	if ((GetComputerName(ReturnComputerName, &ComputerSize)) == 0) {
		dodebug(0, "GetSystemName()", "Failed to get option value", (char *)NULL);
		retrieveErrorMessage("GetSystemName()", "Received this error");
		gp_info.return_value = GP_ERROR;
		return(gp_info.return_value);
	}


	ReturnValue = _strnicmp(ReturnComputerName, ComputerName, COMPUTERNAMESIZE);
	ReturnValue = ReturnValue == 0 ? 1 : 0;
		
//	returnArgumentValue (ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT, 
//						ATLAS == 1 ? argv[PINGNUMBER + ATLAS] : NULL, NULL, 0, 
//						MaxTime, OutPutfp);
	returnArgumentValue(ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT,
						ATLAS == 1 ? argv[NAMERETURN + ATLAS] : NULL, NULL, 0,
						ReturnValue, OutPutfp);

	return(gp_info.return_value);
}

/////////////////////////////////////////////////////////////////////////////
// :	This program will find out what the current working directory is	/
//			return this to the calling process.								/
//																			/
// Parameters:																/
//		argc:		The # of arguments passed to main.						/
//		argv:		Character list of the arguments passed to main.			/
//																			/
// Returns:																	/
//		none:		This is a void function call.							/
//																			/
/////////////////////////////////////////////////////////////////////////////


void GetCurrentWorkingDir(char *argv[])
{

	char	DirectoryPath[GP_MAX_PATH];
	char	OutPutFileName[GP_MAX_PATH];
	FILE	*OutPutfp = NULL;

	if (ATLAS == 0) {
		sprintf(OutPutFileName, "%s%s", gp_info.log_location, DIROUTPUTPATH);

		if ((OutPutfp = fopen(OutPutFileName, "wb+")) == NULL) {
			gp_info.return_value = GP_ERROR;
			return;
		}
	}

	if ( _getcwd(DirectoryPath, GP_MAX_PATH) == NULL) {
		return;
	}

	returnArgumentValue(ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR,
						ATLAS == 1 ? argv[DIRECTORYPATH + ATLAS] : NULL,
						DirectoryPath, strlen(DirectoryPath), 0, OutPutfp);

	return;
}
