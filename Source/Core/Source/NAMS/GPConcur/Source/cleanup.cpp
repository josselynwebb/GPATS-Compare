/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <ctype.h>
#include <direct.h>
#include <atldbcli.h>
#include "gpconcur.h"
#include "FaultFile.h"

/////////////////////////////////////////////////////////////////////////////
//		External Variables and Routines										/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// CleanUp:		This program will move the contents of the C:\APS\DATA\FAULT-FILE
//					into a data base that will be accessed by a VB program.
//					This is not my Idea of making anything work, VB is the worse
//					choice for any program.
//
// Parameters:
//		NONE		This function will use the GPConInfo structure for its information.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		OSRSERR:	(-1)	= failure of a required task.
//																			/
/////////////////////////////////////////////////////////////////////////////

int CleanUp(void)
{

	char	TmpBufCurrent[GP_MAX_PATH];
	char	TmpBufOld[GP_MAX_PATH];
	FILE	*TmpbufFp;

	fflush(NULL);
	DoDebug(0, "GPCONCUR", "Cleanup.cpp CleanUP()", (char*)NULL);
	memset(TmpBufCurrent, '\0', sizeof(TmpBufCurrent));
	memset(TmpBufOld, '\0', sizeof(TmpBufOld));

	sprintf(TmpBufCurrent, "%s%s", GPConInfo.LogLocation, GPConInfo.LogFile);
	sprintf(TmpBufOld, "%s%s", TmpBufCurrent, ".old");

	CreateFile(TmpBufCurrent, TmpBufOld);

	if ((TmpbufFp = fopen(TmpBufCurrent, "w")) == NULL) {
		DoDebug(FILE_OPEN_ERROR, "CleanUp()", (char*)NULL);
		GPConInfo.GPErrno = FILE_OPEN_ERROR;
		return(GP_ERROR);
	}
	else {
		fprintf(TmpbufFp, "1\r\n\r\n");
		fclose(TmpbufFp);
	}

	fflush(NULL);

	if (OpenDataBase()) {
		DoDebug(0, "CleanUp()", "%s", "No need to go any further db won't open", (char*)NULL);
		return(GP_ERROR);
	}

	ExtractInfo(TmpBufOld, CLEANUP);

	CloseDataBase();

	return(SUCCESS);

}