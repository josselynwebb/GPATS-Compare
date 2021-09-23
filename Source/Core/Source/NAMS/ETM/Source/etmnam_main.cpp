/////////////////////////////////////////////////////////////////////////////
//	File:	etmnam_main.cpp													/
//																			/
//	Creation Date:	10 Jan 2005												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of etmnam to implement Iads ver 3.2	/
//					software.												/
//		2.0.1.0		Uncommented the cleanup function, no longer using nt4.0	/
//					etmnam_main()											/
//					properly initilized character arrays with memset.		/
// 					parse_command()											/
// 					properly zero'd the character array of the EtmInfo.		/
//					GetValue()												/
//					added the error checking for the integer to insure that	/
//					the requested value is within documented requirement.	/
//		3.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/ 
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////	

#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <windows.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
#include "stdafx.h"
#include "etmnam.h"
#include "..\..\Common\Include\nam.h"
#include "cplus.h"
#include "Constants.h"
 #pragma comment (lib, "User32.lib")
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

//
// CleanUp:	This function will perform the freeing of all allocated memory
//					that has yet to be freed.
//
// Parameters:
//		none:
//
// Returns:
//		none:
//

void CleanUp(void)
{
	// This works for Window 2000 but not for NT so it is commented out for now.
	//int	i, j;
	
	dodebug(0,"CleanUp()","Entering function in emtnam _main class", (char*)NULL);
	/*for (i = 0; i < COMMAND_NUM; i++) 
	{
		for (j = 0; j < SendInfo[i].numOfVals; j++) 
		{
			FreeItem(SendInfo[i].u.charValue[j]);
		}
		FreeItem(SendInfo[i].cmdToSend);
	}*/
	dodebug(0,"CleanUp()","Leaving function in emtnam _main class", (char*)NULL);
}

//
// EtmnamMain:	This function will check the variables that were passed to
//					it and by these variables determine which function to call.
//					Will also initilize the TYX interface.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//

int EtmnamMain (int argc, char *argv[])
{

	int				ProcessFound = SUCCESS;
	char			TempFileName[NAM_MAX_PATH];
	struct _stat	tmpBuf;
	dodebug(0,"EtmnamMain()","Entering function in emtnam _main class", (char*)NULL);
	if(argc < 2)
		return (NAM_ERROR);

	EtmInfo.NotAtlas = FALSE;
	
   // MessageBox(NULL, "EtmnamMain", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	memset(TempFileName, '\0', sizeof(TempFileName));
	sprintf(TempFileName, "%s%s", argv[TMP_FILE], SUFFIX);

	if ((_stat(TempFileName, &tmpBuf)) == NAM_ERROR) {
		dodebug(0, "EtmnamMain()", "Not an ATLAS program", (char*)NULL);
		EtmInfo.NotAtlas = TRUE;
	}

	if (EtmInfo.NotAtlas == FALSE) {
	
		if(vmOpen(TempFileName) < 0 )
			return (NAM_ERROR);
	}

	
	// Check to see if at least the minimum/maximum amount of arguments were passed.
	if (argc < ARGC_MIN - EtmInfo.NotAtlas || argc > ARGC_MAX - EtmInfo.NotAtlas) {
		dodebug(ARGNUM, "EtmnamMain()", (char *)NULL, (char *)NULL);
		EtmInfo.ReturnValue = ARGNUM;
	}

	if (EtmInfo.NotAtlas == FALSE) {

		if (ParseCommandAtlas(argc, argv)) {
			return(NAM_ERROR);
		}
	}

	if (EtmInfo.NotAtlas == TRUE) {

		if (ParseCommandLine(argc, argv)) {
			return(NAM_ERROR);
		}
	}
	
	//After opening ATLAS interface verify etn_monitor process is runnig, if not start it.
	if ((ProcessFound = FindProcess()) == NAM_ERROR) {
		dodebug(0, "EtmnamMain()",
				"Couldn't access the running process", (char *)NULL);
		return(NAM_ERROR);
	}
	
	if (EtmInfo.Mode == TERMINATE) {
		if (EtmInfo.ProcessRunning == FALSE) {
			return(SUCCESS);
		}
		if (PerformTerminate(EtmInfo.TermType)) {
			return(NAM_ERROR);
		}
		else {
			return(SUCCESS);
		}
	}	

	if (EtmInfo.Mode == READ_ONLY) {
		PerformReader();
	}

	else if (EtmInfo.Mode == READ_TARGET) {
		PerformTarget();
	}

	else if (EtmInfo.Mode == ZOOM_VIEW) {
		PerformZoom();
	}

	else {
		dodebug(0, "EtmnamMain()", "Improper mode of operation", (char *)NULL);
		return(NAM_ERROR);
	}

	if (EtmInfo.NotAtlas == FALSE) {
		if(vmClose() < 0 )
			return (NAM_ERROR);
	}

	if (EtmInfo.ReturnValue != SUCCESS) {
		if (TerminateTheProcess()) {
			dodebug(0, "EtmnamMain()", "Process will not die", (char *)NULL);
		}
	}
	dodebug(0,"EtmnamMain()","Leaving function in emtnam _main class", (char*)NULL);

	return(EtmInfo.ReturnValue != SUCCESS ? NAM_ERROR : SUCCESS);
}

//
// ParseArgumentsAtlas:	This function will parse through the argv list, checking
//						for proper usage and required task to perform. Also
//						the WinInfo structure will be filled out if any window
//						information is present.  Also checks for concurrent
//						operations checking for RESUME.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//		mode:		For loop offset to index the argv properly.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
int ParseArgumentsAtlas(int argc, char *argv[], int mode)
{
	int				i;
	int				dimension[2] = {0, 0};
	long			xad;
	char			x_Text[MAX_STRING]; 
	
	dodebug(0,"ParseArgumentsAtlas()","Entering function in emtnam _main class", (char*)NULL);
	WinInfo.winNum = 0;
	WinInfo.offsetNum = 0;

	for (i = ARGC_MIN + mode; i < argc; i++) {

		xad = atol(argv[i]);
		
		// Check to see if the proper type of argument was sent, must be of type text.
		if (vmGetDataType(xad) == TTYPE) {
			vmGetText(xad, x_Text, MAX_STRING);
		}
		else
		{
			dodebug(DATACHAR, "ParseArguments()", (char *)NULL, (char *)NULL);
			EtmInfo.ReturnValue = DATACHAR;
			return(NAM_ERROR);
		}
		if (!(_strnicmp(x_Text, "RESUME", strlen("RESUME")))) {
			EtmInfo.Concurrent = TRUE;
		}
		else if (!(_strnicmp(x_Text, "WINOFF", strlen("WINOFF")))) {
			i++;
			if (GetValue(2, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.win_x1 = dimension[0];
			WinInfo.win_y1 = dimension[1];
			WinInfo.winNum += 2;
		}
		else if (!(_strnicmp(x_Text, "WINSIZE", strlen("WINSIZE")))) {
			i++;
			if (GetValue(2, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.win_x2 = dimension[0];
			WinInfo.win_y2 = dimension[1];
			WinInfo.winNum += 2;
		}
		else if(!(_strnicmp(x_Text, "OFFSET", strlen("OFFSET")))) {
			i++;
			if (GetValue(2, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_x1 = dimension[0];
			WinInfo.pic_y1 = dimension[1];
			WinInfo.offsetNum += 2;
		}
		else if(!(_strnicmp(x_Text, "LENGTH", strlen("LENGTH")))) {
			i++;
			if (GetValue(2, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_x2 = dimension[0];
			WinInfo.pic_y2 = dimension[1];
			WinInfo.offsetNum += 2;
		}
		else if(!(_strnicmp(x_Text, "XOFFSET", strlen("XOFF")))) {
			i++;
			if (GetValue(1, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_x1 = dimension[0];
			WinInfo.offsetNum++;
		}
		else if(!(_strnicmp(x_Text, "YOFFSET", strlen("YOFF")))) {
			i++;
			if (GetValue(1, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_y1 = dimension[0];
			WinInfo.offsetNum++;
		}
		else if(!(_strnicmp(x_Text, "XLENGTH", strlen("XLEN")))) {
			i++;
			if (GetValue(1, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_x2 = dimension[0];
			WinInfo.offsetNum++;
		}
		else if(!(_strnicmp(x_Text, "YLENGTH", strlen("YLEN")))) {
			i++;
			if (GetValue(1, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_y2 = dimension[0];
			WinInfo.offsetNum++;
		}
		else if(!(_strnicmp(x_Text, "UL", strlen("UL"))) ||
				!(_strnicmp(x_Text, "UR", strlen("UR"))) ||
				!(_strnicmp(x_Text, "LL", strlen("LL"))) ||
				!(_strnicmp(x_Text, "LR", strlen("LR"))) ||
				!(_strnicmp(x_Text, "CTR", strlen("CTR")))) {
			sprintf(WinInfo.predefine, "%s", x_Text);
			SendInfo[OPTIONS_CMD].formatType = STRING_TYPE;
		}
		else {
			dodebug(UNKOPT, "ParseArguments()", (char *)NULL, (char *)NULL);
			EtmInfo.ReturnValue = UNKOPT;
			return(NAM_ERROR);
		}
	}	
	
	dodebug(0,"ParseArgumentsAtlas()","Leaving function in emtnam _main class", (char*)NULL);
	return(SUCCESS);
}

//
// ParseArguments:	This function will parse through the argv list, checking
//						for proper usage and required task to perform. Also
//						the WinInfo structure will be filled out if any window
//						information is present.  Also checks for concurrent
//						operations checking for RESUME.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//		mode:		For loop offset to index the argv properly.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
int ParseArguments(int argc, char *argv[], int mode)
{
	int		i;
	int		dimension[2] = {0, 0};
	char	TmpBuf[NAM_MAX_PATH];
	
	
	dodebug(0,"ParseArguments()","Entering function in emtnam _main class", (char*)NULL);
	WinInfo.winNum = 0;
	WinInfo.offsetNum = 0;

	for (i = (ARGC_MIN + mode - EtmInfo.NotAtlas); i < argc; i++) {

		// First check to see if the proper type of argument was sent, must be of type text.
		memset(TmpBuf, '\0', sizeof(TmpBuf));
		sprintf(TmpBuf, "%s", argv[i]);

		if (!(_strnicmp(TmpBuf, "RESUME", strlen("RESUME")))) {
			EtmInfo.Concurrent = TRUE;
		}
		else if (!(_strnicmp(TmpBuf, "WINOFF", strlen("WINOFF")))) {
			i++;
			if (GetValue(2, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.win_x1 = dimension[0];
			WinInfo.win_y1 = dimension[1];
			WinInfo.winNum += 2;
		}
		else if (!(_strnicmp(TmpBuf, "WINSIZE", strlen("WINSIZE")))) {
			i++;
			if (GetValue(2, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.win_x2 = dimension[0];
			WinInfo.win_y2 = dimension[1];
			WinInfo.winNum += 2;
		}
		else if(!(_strnicmp(TmpBuf, "OFFSET", strlen("OFFSET")))) {
			i++;
			if (GetValue(2, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_x1 = dimension[0];
			WinInfo.pic_y1 = dimension[1];
			WinInfo.offsetNum += 2;
		}
		else if(!(_strnicmp(TmpBuf, "LENGTH", strlen("LENGTH")))) {
			i++;
			if (GetValue(2, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_x2 = dimension[0];
			WinInfo.pic_y2 = dimension[1];
			WinInfo.offsetNum += 2;
		}
		else if(!(_strnicmp(TmpBuf, "XOFFSET", strlen("XOFF")))) {
			i++;
			if (GetValue(1, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_x1 = dimension[0];
			WinInfo.offsetNum++;
		}
		else if(!(_strnicmp(TmpBuf, "YOFFSET", strlen("YOFF")))) {
			i++;
			if (GetValue(1, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_y1 = dimension[0];
			WinInfo.offsetNum++;
		}
		else if(!(_strnicmp(TmpBuf, "XLENGTH", strlen("XLEN")))) {
			i++;
			if (GetValue(1, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_x2 = dimension[0];
			WinInfo.offsetNum++;
		}
		else if(!(_strnicmp(TmpBuf, "YLENGTH", strlen("YLEN")))) {
			i++;
			if (GetValue(1, &i, argc, argv, dimension)) {
				return(NAM_ERROR);
			}
			WinInfo.pic_y2 = dimension[0];
			WinInfo.offsetNum++;
		}
		else if(!(_strnicmp(TmpBuf, "UL", strlen("UL"))) ||
				!(_strnicmp(TmpBuf, "UR", strlen("UR"))) ||
				!(_strnicmp(TmpBuf, "LL", strlen("LL"))) ||
				!(_strnicmp(TmpBuf, "LR", strlen("LR"))) ||
				!(_strnicmp(TmpBuf, "CTR", strlen("CTR")))) {
			sprintf(WinInfo.predefine, "%s", TmpBuf);
			SendInfo[OPTIONS_CMD].formatType = STRING_TYPE;
		}
		else {
			dodebug(UNKOPT, "ParseArguments()", (char *)NULL, (char *)NULL);
			EtmInfo.ReturnValue = UNKOPT;
			return(NAM_ERROR);
		}
	}
	
	dodebug(0,"ParseArguments()","Leaving function in emtnam _main class", (char*)NULL);
	return(SUCCESS);
}

//
// ParseCommandAtlas:	This function will parse through the argv list, checking
//						for proper usage and required task to perform. Also will
//						fill in the EtmInfo structure with the proper
//						information.  Also will detect what mode will be used.
//						Then will call the proper function to finish the parsing
//						of the remaining arguments.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//
int ParseCommandAtlas(int argc, char *argv[])
{
	long			xad, yad;
	char			x_Text[MAX_STRING];
	char			y_Text[MAX_STRING];
	
	dodebug(0,"ParseCommandAtlas()","Entering function in emtnam _main class", (char*)NULL);
	// Get the first argument, it has to be a character string.
	xad = atol(argv[ARGC_MIN - 1]);
	if (vmGetDataType(xad) == TTYPE) {
		vmGetText(xad, x_Text, MAX_STRING);
	}
	else
	{
		dodebug(DATACHAR, "ParseCommand()", (char *)NULL, (char *)NULL);
		EtmInfo.ReturnValue = DATACHAR;
		return(NAM_ERROR);
	}

	/* Check to see if the 3 argument is equal to TERM. If it is then terminate all
	Iads viewers.*/
	if (argc == TERM) {

		memset(EtmInfo.TermType, '\0', sizeof(EtmInfo.TermType));
		EtmInfo.Mode = TERMINATE;
		sprintf(EtmInfo.TermType, "%s", x_Text);
		return(SUCCESS);
	}

	// Get the first variable which has to be the filename.
	else {
		sprintf(EtmInfo.FileLocation, "%s", x_Text);
	}

	/* The next element has to be one of three things.  The word TARGET, ZOOM, or
	it is a character string of the frame reference used by the reader.  All else
	will create an error somewhere down the line.*/
	xad = atol(argv[ARGC_MIN]);
	if (vmGetDataType(xad) == TTYPE) {
		vmGetText(xad, x_Text, MAX_STRING);
	}
	else
	{
		dodebug(DATACHAR, "ParseCommand()", (char *)NULL, (char *)NULL);
		EtmInfo.ReturnValue = DATACHAR;
		return(NAM_ERROR);
	}
	if (!(_strnicmp(x_Text, "TARGET", strlen("TARGET")))) {
		yad = atol(argv[ARGC_MIN + 1]);
		if (vmGetDataType(yad) == TTYPE) {
			vmGetText(yad, y_Text, MAX_STRING);
			sprintf(EtmInfo.FrameTarget, "%s", y_Text);
		}
		else {
			dodebug(DATACHAR, "ParseCommand()", (char *)NULL, (char *)NULL);
			EtmInfo.ReturnValue = DATACHAR;
			return(NAM_ERROR);
		}

		EtmInfo.Mode = READ_TARGET;

		if (DoTargetParse(argc, argv)) {
			return(NAM_ERROR);
		}
	}
	else if (!(_strnicmp(x_Text, "ZOOM", strlen("ZOOM")))) {

		EtmInfo.Mode = ZOOM_VIEW;

		if (DoZoomParse(argc, argv)) {
			return(NAM_ERROR);
		}
	}
	else {

		EtmInfo.Mode = READ_ONLY;
		sprintf(EtmInfo.FrameTarget, "%s", x_Text);

		if (DoReadParse(argc, argv)) {
			return(NAM_ERROR);
		}
	}
	
	dodebug(0,"ParseCommandAtlas()","Leaving function in emtnam _main class", (char*)NULL);
	return(EtmInfo.ReturnValue != SUCCESS ? NAM_ERROR : SUCCESS);
}

//
// ParseCommandLine:	This function will parse through the argv list, checking
//						for proper usage and required task to perform. Also will
//						fill in the EtmInfo structure with the proper
//						information.  Also will detect what mode will be used.
//						Then will call the proper function to finish the parsing
//						of the remaining arguments.
//
// Parameters:
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

int ParseCommandLine(int argc, char *argv[])
{

	char	TmpBuf[NAM_MAX_PATH];
	
	dodebug(0,"ParseCommandLine()","Entering function in emtnam _main class", (char*)NULL);
	memset(TmpBuf, '\0', sizeof(TmpBuf));

	/* Check to see if the 2 argument is equal to TERM. If it is then terminate all
	Iads viewers.*/
	if (argc == (TERM - EtmInfo.NotAtlas)) {

		memset(EtmInfo.TermType, '\0', sizeof(EtmInfo.TermType));
		EtmInfo.Mode = TERMINATE;
		sprintf(EtmInfo.TermType, "%s", argv[FILENAME]);
		return(SUCCESS);
	}

	// Get the first variable which has to be the filename.
	else {
		sprintf(EtmInfo.FileLocation, "%s", argv[FILENAME]);
	}

	/* The next element has to be one of three things.  The word TARGET, ZOOM, or
	it is a character string of the frame reference used by the reader.  All else
	will create an error somewhere down the line.*/

	sprintf(TmpBuf, "%s", argv[TARGETZOOM]);

	if (!(_strnicmp(TmpBuf, "TARGET", strlen("TARGET")))) {

		sprintf(EtmInfo.FrameTarget, "%s", argv[TARGET]);
		EtmInfo.Mode = READ_TARGET;

		if (DoTargetParse(argc, argv)) {
			return(NAM_ERROR);
		}
	}
	else if (!(_strnicmp(TmpBuf, "ZOOM", strlen("ZOOM")))) {

		EtmInfo.Mode = ZOOM_VIEW;

		if (DoZoomParse(argc, argv)) {
			return(NAM_ERROR);
		}
	}
	else {

		EtmInfo.Mode = READ_ONLY;
		sprintf(EtmInfo.FrameTarget, "%s", TmpBuf);

		if (DoReadParse(argc, argv)) {
			return(NAM_ERROR);
		}
	}
	dodebug(0,"ParseCommandLine()","Leaving function in emtnam _main class", (char*)NULL);

	return(EtmInfo.ReturnValue != SUCCESS ? NAM_ERROR : SUCCESS);
}

//
// GetValue:	This function will parse through the argv list, checking
//						for proper usage and retrieve the requested value.
//
// Parameters:
//		num:		The # of arguments to parse.
//		flcount:	The for loop index counter from the calling process.
//		argc:		The # of arguments passed to main.
//		argv:		Character list of the arguments passed to main.
//		value:		The location for the value to be stored.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

int GetValue(int num, int *flcount, int argc, char *argv[], int *value)
{

	int				i;
	int				TmpVal = 0;
	long			xad;

	
	dodebug(0,"GetValue()","Entering function in emtnam _main class", (char*)NULL);
	for (i = 0; i < num; i++) {

		if (*flcount >= argc) {
			dodebug(ARGNUM, "GetValue()", (char *)NULL, (char *)NULL);
			EtmInfo.ReturnValue = DATACHAR;
			return(NAM_ERROR);
		}

		if (EtmInfo.NotAtlas == FALSE) {
			xad = atol(argv[*flcount]);

			if (vmGetDataType(xad) == ITYPE) {
				TmpVal = vmGetInteger(xad);
				dodebug(DATAINT, "GetValue()", (char *)NULL, (char *)NULL);
			}
			else
			{
				dodebug(DATAINT, "GetValue()", (char *)NULL, (char *)NULL);
				EtmInfo.ReturnValue = DATACHAR;
				return(NAM_ERROR);
			}

		}
		else {

			TmpVal = atoi(argv[*flcount]);
		}

		if (TmpVal < 0 || TmpVal > 100) {
			dodebug(0, "GetValue()", "The value is not within specs %d", TmpVal, (char*)NULL);
			EtmInfo.ReturnValue = NAM_ERROR;
			return(NAM_ERROR);
		}

		*(value+i) = TmpVal;
		*flcount += 1;
	}

	*flcount -= 1;
	dodebug(0,"GetValue()","Leaving function in emtnam _main class", (char*)NULL);
	return(SUCCESS);
}
