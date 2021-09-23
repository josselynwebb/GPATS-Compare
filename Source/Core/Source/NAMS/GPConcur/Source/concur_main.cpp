/////////////////////////////////////////////////////////////////////////////
//	File:	concur_main.cpp													/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily.  All calls	/
//					to DoDebug(), in all functions, have been modified to	/
//					conform to the DoDebug arguments.  DoDebug() was		/
//					modified to allow for a variable argument list to be	/
//					passed to the function and is terminated with a (char*)	/
//					NULL.													/
//					ConcurMain()											/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//					SearchForAPSName()										/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variables are zero'd out, the prior way		/
//					didn't do anything, now using memset to zero the		/
//					variables.												/
//					CleanUp()												/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style). Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables. Moved the opening	/
//					of the database to the end of the function, also moved	/
//					the ExtractInfo() to the end. This allowed the cleaning	/
//					of the faultfile even though the program failed to open	/
//					the database.											/
//					GetPipeHd()												/
//					Changed the way the variables are written				/
//					(camel back style). Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables. Deleted the			/
//					declaration of the char TmpString, the function call to	/
//					memset and the sprintf function call after the call to	/
//					!ConnectNamedPipe(), all is now done in the DoDebug		/
//					function call.											/
//					PerformConcurrentOp()									/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					ErrorString()											/
//					Changed the name from error_ms to the present.			/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).	Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables.						/
//					DoDebug()												/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).  Modified the arguments to allow	/
//					for the calling of this function with a variable		/
//					argument list.											/
//					ReturnStatus()											/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//					RemoveSpaces()											/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//					GetItem()												/
//					Changed the name from get_this to the present. Corrected/
//					the info header to correctly reflect the function.		/
//					Changed the way the variables are displayed (camel back	/
//					style). Changed the way the string variables are zero'd	/
//					out, the piror way didn't do anything, now using memset	/
//					to clear the variables. Changed the name of some of the	/
//					to correctly reflect what they are (cur_time -			/
//					CurrentTime; curtp - CurrentTimePt; tmpt - TimeStructPt;/
//					mon - MonthSent). Deleted the declaration of the char	/
//					tmpbuf1, the function call to memset and the sprintf	/
//					function call after the if (indexPos, all is now done in/
//					the DoDebug function call.								/
//					GetInfo()												/
//					Changed the name from get_this to the present. Corrected/
//					the info header to correctly reflect the function.		/
//					Changed the way the variables are written				/
//					(camel back style). Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables.						/
//					DoMonthYer()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(cur_time - CurrentTime; curtp -	/
//					CurrentTimePt; tmpt - TimeStructPt; mon - MonthSent).	/
//					GetDateTime()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the malloc to calloc to initialize the variable	/
//					to all null characters. Changed the name of some of the	/
//					variables to correctly reflect what they are (k -		/
//					ScanReturn; tmpbuf1 - ErrorMessage). Deleted the		/
//					declaration of the char tmpbuf1, the function call to	/
//					memset and the sprintf function call after the sscanf,	/
//					all is now done in the DoDebug function call.			/
//					NumberOfThis()											/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//					FindStringRight()										/
//					Changed the name from strrstr to the present. Corrected	/
//					the info header to correctly reflect the function.		/
//					Changed the way the variables are written				/
//					(camel back style). Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables. Deleted the			/
//					declaration of the char tmpbuf, the function call to	/
//					memset and the sprintf function call after the if		/
//					(StartPoint, all is now done in the DoDebug function	/
//					call.													/
//					FindPoint()												/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(pt_found - PointFound; tmpbuf -	/
//					ErrorMessage). Deleted the declaration of the char		/
//					tmpbuf, the function call to memset and the sprintf		/
//					function call after the if((PointFound, all is now done	/
//					in the DoDebug function									/
//					MoveToPoint()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(pt_found - PointFound; tmpbuf -	/
//					ErrorMessage). Deleted the declaration of the char		/
//					tmpbuf, the function call to memset and the sprintf		/
//					function call after the if((PointFound, all is now done	/
//					in the DoDebug function									/
//					GotoTermNumber()										/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(tmpbuf - ErrorMessage). Deleted	/
//					the declaration of the char	tmpbuf, the function call	/
//					to memset and the sprintf function call after the		/
//					if((MoveToPoint, all is now done in the DoDebug function/
//					NewFormat()												/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(tmpbuf1 - ErrorMessage). Changed	/
//					the function call strrstr to FindStringRight. Deleted	/
//					the declaration of the char tmpbuf1, the function call	/
//					to memset and the sprintf function call after the call	/
//					for(i = 0;, all is now done in the DoDebug function		/
//					call. Deleted the call to sprintf and the tmpbuf[0] =	/
//					'\0'; after the strrstr(), the DoDebug now does this.	/
//					OldFormat()												/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(tmpbuf1 - ErrorMessage). Changed	/
//					the function call strrstr to FindStringRight. Added code/
//					to free allocated memory if an error happened and also	/
//					after the use of it was no longer needed (DifferentUUT)	/
//					Deleted the declaration of the char tmpbuf1, the		/
//					function call to memset and the sprintf function call	/
//					after the call to strrstr, all is now done in the		/
//					DoDebug function call. Deleted the call to sprintf and	/
//					the tmpbuf[0] =	'\0'; after the strrstr(), the DoDebug	/
//					now does this.											/
//					ExtractInfo()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the malloc to calloc to initialize the variable	/
//					to all null characters. Changed the name of some of the	/
//					variables to correctly reflect what they are (faultfz -	/
//					FaultFileStatPt; fffp - FaultFilePt; tmpbuf -			/
//					ErrorMessage). Added a test to see if a variable needed	/
//					its' memory freed (FaultfileContents). Deleted the		/
//					declaration of the char tmpbuf, the function call to	/
//					memset and the sprintf function call after the call		/
//					if((num_of_this, all is now done in DoDebug function.	/
//					CreateFile()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Ghanged the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variables are zero'd out, the prior way		/
//					didn't do anything, now using memset to zero the		/
//					variables. Addedvariable ErrorMessage to the top of the	/
//					function and removed the numerous declarations of the	/
//					char tmpbuf at every error detection. Changed the name	/
//					of some of the variables to correctly reflect what they	/
//					are (ftcfp - FileToCopy; tmpfp - FileToCreate; fz -		/
//					FileStructFp). Changed the malloc to calloc to			/
//					initialize the variable to all null characters. Deleted	/
//					the declaration of the char tmpbuf, the function call to/
//					memset and the sprintf function call after the call		/
//					if((file_to_copy, all is now done in DoDebug function.	/
//					Deleted the call to sprintf and the tmpbuf[0] = '\0';	/
//					after the call if ((tmpfp = fopen and after the			/
//					following else statement, the DoDebug now does this.	/
//					OpenDataBase()											/
//					Changed the name from open_db to present. Added the info/
//					header to correctly reflect the function				/
//					FillInDataBase()										/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//					CloseDataBase()											/
//					Changed the name from close_db to present. Added the	/
//					info header to correctly reflect the function			/
//					WinMain()												/
//					Changed the way the variables are written				/
//					(camel back style). Changed the name of some of the		/
//					variables to correctly reflect what they are(nargs -	/
//					NumberOfArgs; tmp_buf - StructTmpBuf). Added a for loop	/
//					tp free allocated memory that wasn't being freed.		/
//					WriteMessage()											/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					ReadMessage()											/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					GetResponse()											/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					RespondToControllingProcess()							/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					PrintFaultFile()										/
//					Changed the way the variables are written				/
//					(camel back style).										/
//					StartProcess()											/
//					Changed the way the variables are written				/
//					(camel back style).										/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <dos.h>
#include <time.h>
#include <sys/types.h>
#include <sys/timeb.h>
#include <direct.h>
#include <windows.h>
#include "stdafx.h"
#include "gpconcur.h"
#include "pipe.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

GPCON_INFO		GPConInfo;
PIPE_INFO		PipeInfo;
ERR_INFO		ErrInfo;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// ConcurMain:		This program will check the variables that were passed	/
//					to it and if correct will set up the defaults and then	/
//					call the different functions in proper order.			/
//																			/
// Parameters:																/
//		argc:		The # of arguments passed to ConcurMain.				/
//		argv:		Character list of the arguments passed to ConcurMain.	/
//																			/
// Returns:																	/
//		SUCCESS:	  0  = Successful completion of the function.			/
//		GP_ERROR:	(-1) = An error had occurr'd, what else.				/
//																			/
/////////////////////////////////////////////////////////////////////////////

int ConcurMain (int argc, char *argv[])
{

//
// Setup some defaults and initilize some variables to their defaults.
//

	PipeInfo.MessageSize	= MAX_MSG_SIZE + 24;
	PipeInfo.Pipehd		= NULL;

//
// Now we will check to see ifthe proper # of arguments were passed.  If not we
// will exit with a message to the debug file if elected.
//

	if (argc != 2) {

		GPConInfo.GPErrno = ARGNUM;
		DoDebug(GPConInfo.GPErrno, "ConcurMain()", (char*)NULL);
		return(GP_ERROR);
	}
	else {

		GPConInfo.OptNumber = atoi(argv[OPT_ARG]);
		sprintf(PipeInfo.PipeFile, "%s%s", PIPE_FILE_HEADER, argv[PIPE_ARG]);
	}

//
// Now I guess we are ready to get this concurrent dtb process running.  Everything
// should be setup and all variables should be initialized, so let's get truckin.
//

	PerformConcurrentOp();

	return(SUCCESS);

//
// Simple.
//

}