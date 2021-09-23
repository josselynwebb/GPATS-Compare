/////////////////////////////////////////////////////////////////////////////
//	File:	printfile.cpp													/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily				/
//					PrintFaultFile()										/
//					Changed the way the variables are written				/
//					(camel back style).										/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <ctype.h>
#include <process.h>
#include "gpconcur.h"

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
// PrintFaultFile:		This program will extract the last complete run of	/
//							an ATLAS test and print just that info to a		/
//							printer if the printer is connected and			/
//							installed correctly. If the printer is not		/
//							present or improperly installed then there will	/
//							be a typical Microsoft idiot popup box letting	/
//							the operator know what has gone wrong. The		/
//							operator will have to fix the problem and try	/
//							to print again, because this program will have	/
//							exited.											/
//																			/
// Parameters:																/
//		None:																/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		OSRSERR:	(-1)	= failure of a required task.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int PrintFaultFile(void)
{

	char	TmpBuf[GP_MAX_PATH];

	fflush(NULL);

	if (OpenDataBase()) {
		DoDebug(0, "PrintFaultFile()", "%s", "No need to go any further db won't open", (char*)NULL);
		return(GP_ERROR);
	}

	memset(TmpBuf, '\0', sizeof(TmpBuf));

	sprintf(TmpBuf, "%s%s", GPConInfo.LogLocation, GPConInfo.LogFile);

	if (ExtractInfo(TmpBuf, PRINTIT)) {
		GPConInfo.GPErrno = SUCCESS;
//		GPConInfo.GPErrno = GP_ERROR;
	}

	fflush(NULL);
	CloseDataBase();

	return(SUCCESS);

}