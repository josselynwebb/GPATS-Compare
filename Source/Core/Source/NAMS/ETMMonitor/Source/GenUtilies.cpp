/////////////////////////////////////////////////////////////////////////////
//	File:	GenUtilites.cpp													/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
// 		1.0.1.0		setup_window_size()										/
// 					modified the way the character array is being zero'd.	/
// 					process_data()											/
// 					modified the way the character buffers were being zero' /
// 					before use and added the free() call before the returns	/
// 					to prevent memory leeks.								/
// 					gather_data()											/
// 					added memset() calls to insure the character arrays were/
// 					zero'd prior to being filed.							/
// 					get_data()												/
// 					removed the string_to_set[0] = '\0': this array is		/
// 					being zero'd in the calling function like it should		/
// 					generate_command_line()									/
// 					properly zero'd the character arrays with memset() call	/
//		1.0.2.0		SetupWindowSize()										/
//					Corrected the debug statement, added (char*) to			/
//					terminate the debug statement properly.	Chande the else	/
//					if to an if statement.  In ther if statement			/
//					EtmInfo.Mode == ZOOM, added the setting of the varaibles/
//					ImageInfo.WNotUsed, .wxoff, wxlen, .wyoff, wylen.  This	/
//					allowed for the proper setting of the window size of the/
//					imaging window size and position. In the else portion	/
//					added the setting of the variable WinInfo.ReadrNotUsed.	/
//					GetMode()												/
//					Corrected the debug statement, added (char*) to			/
//					terminate the debug statement properly.	Chande the else	/
//					ProcessGatheredData()									/
//					Added the function call to SetUpIads					/
//					ProcessData()											/
//					Added the setting of the variable EtmInfo.SavedFileName	/
//					and debug statemenmts in the first else statement. This	/
//					allowed for resetting of the registry entry for 3.4.13	/
//					In the second major else statement modified the setting	/
//					of the WinInfo.ImagNotUsed and .ReadrNotUsed depending	/
//					on the EtmInfo.Mode value.								/
//					GenerateCommandLine()									/
//					In the case TARGET, modified the setting of c1 and the	/
//					labelString based on which iads was to be used, these	/
//					not need for iads3.4.13. Addded debug statement to ZOOM./
//					Corrected the debug statement, added (char*) to			/
//					terminate the debug statement properly. Added and if	/
//					statement the end of the function to add window info	/
//					for the imager if used, also corrected the debug stmt.	/
//					SetAppWindow()											/
//					Changed the WinInfo.NotUsed to an if statement which	/
//					will set the proper WinInfo element.					/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>
#include <string.h>
#include <math.h>
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"
#include "..\..\Common\Include\pipe.h"
#include "psapi.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
//	SetupWindowSize:	This function will setup the imag_info and WinInfo
//							structure with the parsed information from their
//							EtmInfo structure elements.
//							
//	Parameters:
//		option:			Set to choose either image or window task.
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int SetupWindowSize(int option)
{

	int		i, x1 = 0, x2 = 0, y1 = 0, y2 = 0;
	char	c;
	
	dodebug(0,"SetupWindowSize()","Entering function in GenUtilities class", (char*)NULL);
	if (option == OPTIONS_CMD) {
		if (strlen(EtmInfo.ImageOptions) < 5) {
			if ((!_stricmp(EtmInfo.ImageOptions, "UL")) ||
				(!_stricmp(EtmInfo.ImageOptions, "UR")) ||
				(!_stricmp(EtmInfo.ImageOptions, "LL")) ||
				(!_stricmp(EtmInfo.ImageOptions, "LR")) ||
				(!_stricmp(EtmInfo.ImageOptions, "CTR"))) {
				
				ImagInfo.DefinedUsed = TRUE;
				memset(ImagInfo.Defined, '\0', sizeof(ImagInfo.Defined));
				sprintf(ImagInfo.Defined, "%s", EtmInfo.ImageOptions);
			}
			else {
				dodebug(0, "SetupWindowSize()", "Improper string sent %s", EtmInfo.ImageOptions, (char*)NULL);  
				return(NAM_ERROR);
			}
		}
		else {
			if ((i = sscanf(EtmInfo.ImageOptions, "%d%c%d%c%d%c%d",
							&ImagInfo.xoff, &c, &ImagInfo.yoff, &c,
							&ImagInfo.xlen, &c, &ImagInfo.ylen)) != 7) {
				dodebug(0, "SetupWindowSize()", "Only convereted %d elements, should be 7", i, (char*)NULL);  
				return(NAM_ERROR);
			}
			ImagInfo.DefinedUsed = FALSE;
		}
	}
	else if (option == WINDOW_CMD) {
		int tmpInt;	
		double	scrH, scrW;
			
		scrH = (double)WinInfo.ScreenH / 100;
		scrW = (double)WinInfo.ScreenW / 100;

		if ((i = sscanf(EtmInfo.WindowSize, "%d%c%d%c%d%c%d", 
			&x1, &c, &y1, &c, &x2, &c, &y2)) != 7) {
			dodebug(0, "SetupWindowSize()", "Only convereted %d elements, should be 7", i, (char*)NULL);  
			return(NAM_ERROR);
		}
		if (EtmInfo.Mode == ZOOM) {
			WinInfo.Ix1 = (int)Round((double)x1 * scrW);
			tmpInt = (int)Round((double)x2 * scrW);
			WinInfo.Ix2 = tmpInt == 0 ? (int)(100 * scrW) - WinInfo.Ix1 : tmpInt;
			WinInfo.Iy1 = (int)Round((double)y1 * scrH);
			tmpInt = (int)Round((double)y2 * scrH);
			WinInfo.Iy2 = tmpInt == 0 ? (int)(100 * scrH) - WinInfo.Iy1 : tmpInt;
			/*ImagInfo.WNotUsed = FALSE;
			ImagInfo.wxoff = x1;
			ImagInfo.wxlen = x2;
			ImagInfo.wyoff = y1;
			ImagInfo.wylen = y2;*/
		}
		else {
			WinInfo.Rx1 = (int)Round((double)x1 * scrW);
			tmpInt = (int)Round((double)x2 * scrW);
			WinInfo.Rx2 = tmpInt == 0 ? (int)(100 * scrW) - WinInfo.Rx1 : tmpInt;
			WinInfo.Ry1 = (int)Round((double)y1 * scrH);
			tmpInt = (int)Round((double)y2 * scrH);
			WinInfo.Ry2 = tmpInt == 0 ? (int)(100 * scrH) - WinInfo.Ry1 : tmpInt;
			/*WinInfo.ReadrNotUsed = FALSE; */

		}
	}
	
	dodebug(0,"SetupWindowSize()","Leaving function in GenUtilities class", (char*)NULL);

	return(SUCCESS);
}

//
//	GetMode:	This function will setup variables that will be passed to the
//					function check_return depending upon the value mode equates
//					to.
//							
//	Parameters:
//		mode:		Set to indicate either OPERATIONAL or FUNCTIONAL.
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int GetMode(int mode)
{

	int		i, value[2], pairs, *variableToSet, hadError;
	char	*modeType, *checkString[2];

	hadError = FALSE;
	
	dodebug(0,"GetMode()","Entering function in GenUtilities class", (char*)NULL);
	if (mode == OPERATIONAL) {

		pairs = 2;
		value[0] = TRUE;
		value[1] = FALSE;
		variableToSet = &EtmInfo.Concurrent;
		modeType = _strdup(OPERATIONAL_MSG);
		checkString[0] = _strdup(CONCURRENT_MSG);
		checkString[1] = _strdup(CONSECUTIVE_MSG);

	}
	else if (mode == FUNCTIONAL) {

		pairs = 2;
		value[0] = TARGET;
		value[1] = READER_ONLY;
		variableToSet = &EtmInfo.Mode;
		modeType = _strdup(FUNCTIONAL_MSG);
		checkString[0] = _strdup("TARGET END");
		checkString[1] = _strdup("READER END");

	}
	else {
		dodebug(0, "GetMode()", "The mode you sent to this function was %d", mode, (char*)NULL);  
		return(NAM_ERROR);
	}

	if (CheckReturn(pairs, checkString, value, modeType, variableToSet)) {
		hadError = TRUE;
	}

	for (i = 0; i < 2; i++) {
		if (checkString[i] != NULL) {
			free(checkString[i]);
		}
	}
	dodebug(0,"GetMode()","Leaving function in GenUtilities class", (char*)NULL);
	
	return(hadError == FALSE ? SUCCESS : NAM_ERROR);
}

//
//	CheckReturn:	This function will send a message to the calling process
//						request information with the variable request.  Then the
//						function will loop comparing the array of characters
//						pointers (CheckString) with the message returned from
//						calling process.  If a match is found then the variable
//						VarToSet will be set to the matching value pair.
//							
//	Parameters:
//		pairs:			Indicates the numbers of pairs passed in the variables
//						CheckString and value.
//		CheckString:	This is a array of character pointers that will be used
//						in comparison of the message.
//		value:			This an array of values used to set the command line
//						variable var_to_set.
//		request:		Message sent to the calling process to send information
//						in response to the request.
//		VarToSet:		This is the variable that will be set upon a match with
//						message and the array check_string.
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int	CheckReturn(int pairs, char *CheckString[], int *value,
				 char *request, int *VarToSet)
{
	dodebug(0,"CheckReturn()","Entering function in GenUtilities class", (char*)NULL);

	int	i, CheckStringFound;

	CheckStringFound = FALSE;

	if (MessageRequest(request)) {
		return(NAM_ERROR);
	}
	
	for (i = 0; i < pairs; i++) {
		
		if (!_strnicmp(CheckString[i], PipeInfo.ReadMsg, strlen(CheckString[i]))) {
			*VarToSet = value[i];
			CheckStringFound = TRUE;
		}
	}

	if (CheckStringFound != TRUE) {
		dodebug(0, "CheckReturn()", "Improper string sent", (char*)NULL);
		return(NAM_ERROR);
	}
	dodebug(0,"CheckReturn()","Leaving function in GenUtilities class", (char*)NULL);

	return(SUCCESS);
}

//
//	ProcessGatheredData:	This function will first gather all currently 
//							available data and then process said data and the
//							available options.  Then start the command line
//							creation.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int ProcessGatheredData(void)
{
	dodebug(0,"ProcessGatheredData()","Entering function in GenUtilities class", (char*)NULL);

	if (GatherData()) {
		return(NAM_ERROR);
	}

	if (ProcessData()) {
		return(NAM_ERROR);
	}
	/*if(SetUpIads()){
		return(NAM_ERROR);
	}*/

	if (GenerateCommandLine(EtmInfo.Mode)) {
		return(NAM_ERROR);
	}
	dodebug(0,"ProcessGatheredData()","Leaving function in GenUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	ProcessData:	This function will parse out only the required data for the
//						window size and imager options.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int ProcessData(void)
{

	char	*TmpBuf;
	
	dodebug(0,"ProcessData()","Entering function in GenUtilities class", (char*)NULL);
	if ((TmpBuf = GetInfo(EtmInfo.FileName, FILE_CMD)) == NULL) {
		dodebug(0, "ProcessData()", "Failed to get the file info, nothing there?",(char*)NULL);
		return(NAM_ERROR);
	}
	else {

		memset(EtmInfo.FileName, '\0', sizeof(EtmInfo.FileName));
		//memset(EtmInfo.SavedFileName, '\0', sizeof(EtmInfo.SavedFileName));  
		sprintf(EtmInfo.FileName, "%s", TmpBuf);
		//sprintf(EtmInfo.SavedFileName, "%s", TmpBuf);  
		EtmInfo.FileName[strlen(TmpBuf)] = '\0';
		//EtmInfo.SavedFileName[strlen(TmpBuf)] = '\0';  
		dodebug(0, "ProcessData", "EtmInfo.FileName = (%s)", EtmInfo.FileName, (char*)NULL);
		//dodebug(0, "ProcessData", "EtmInfo.SavedFileName = (%s)", EtmInfo.SavedFileName, (char*)NULL);  

		free(TmpBuf);
	}

	if ((TmpBuf = GetInfo(EtmInfo.WindowSize, WINDOW_CMD)) == NULL) {
		dodebug(0, "ProcessData()", "Failed to get the window info, nothing there?",(char*)NULL);
		return(NAM_ERROR);
	}
	else {

		memset(EtmInfo.WindowSize, '\0', sizeof(EtmInfo.WindowSize));
		sprintf(EtmInfo.WindowSize, "%s", TmpBuf);
		EtmInfo.WindowSize[strlen(TmpBuf)] = '\0';
		free(TmpBuf);

		if (_strnicmp(EtmInfo.WindowSize, "NONE", strlen("NONE"))) {

			if (SetupWindowSize(WINDOW_CMD)) {
				return(NAM_ERROR);
			}
			else {
				WinInfo.NotUsed = FALSE;
			}
		}
		else {
			WinInfo.NotUsed = TRUE;
		}
	}

	if (EtmInfo.Mode != ZOOM) {

		if ((TmpBuf = GetInfo(EtmInfo.TargetLabel, REFERENCE_CMD)) == NULL) {
			dodebug(0, "ProcessData()", "Failed to get the reference info",(char*)NULL);
			return(NAM_ERROR);
		}
		else {

			memset(EtmInfo.TargetLabel, '\0', sizeof(EtmInfo.TargetLabel));
			sprintf(EtmInfo.TargetLabel, "%s", TmpBuf);
			EtmInfo.TargetLabel[strlen(TmpBuf)] = '\0';
			free(TmpBuf);
		}

		return(SUCCESS);
	}

	if ((TmpBuf = GetInfo(EtmInfo.ImageOptions, OPTIONS_CMD)) == NULL) {
		dodebug(0, "ProcessData()", "Failed to get the image option", (char*)NULL);
		return(NAM_ERROR);
	}
	else {

		memset(EtmInfo.ImageOptions, '\0', sizeof(EtmInfo.ImageOptions));
		sprintf(EtmInfo.ImageOptions, "%s", TmpBuf);
		EtmInfo.ImageOptions[strlen(TmpBuf)] = '\0';
		free(TmpBuf);

		if (_strnicmp(EtmInfo.ImageOptions, "NONE", strlen("NONE"))) {

			if (SetupWindowSize(OPTIONS_CMD)) {
				return(NAM_ERROR);
			}
			else {
				ImagInfo.NotUsed = FALSE;
			}
		}
		else {
			ImagInfo.NotUsed = TRUE;
		}
	}
	
	dodebug(0,"ProcessData()","Entering function in GenUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	GatherData:	This function will call the function get_data, passing the
//						data type being requested and the variable in which it
//						is to set with said data.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int GatherData(void)
{
	
	dodebug(0,"GatherData()","Entering function in GenUtilities class", (char*)NULL);
	memset(EtmInfo.FileName, '\0', sizeof(EtmInfo.FileName));
	
	if (GetData(FILE_MSG, EtmInfo.FileName)) {
		return(NAM_ERROR);
	}
	
	memset(EtmInfo.WindowSize, '\0', sizeof(EtmInfo.WindowSize));
	
	if (GetData(WINDOW_MSG, EtmInfo.WindowSize)) {
		return(NAM_ERROR);
	}

	if (EtmInfo.Mode != ZOOM) {

		memset(EtmInfo.TargetLabel, '\0', sizeof(EtmInfo.TargetLabel));
	
		if (GetData(REFERENCE_MSG, EtmInfo.TargetLabel)) {
			return(NAM_ERROR);
		}
	}
	else {

		memset(EtmInfo.ImageOptions, '\0', sizeof(EtmInfo.ImageOptions));
		
		if (GetData(OPTIONS_MSG, EtmInfo.ImageOptions)) {
			return(NAM_ERROR);
		}
	}
	dodebug(0,"GatherData()","Leaving function in GenUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	GetData:	This function will pass to message_request the variable
//					data_type.  This is the what is being requested from the
//					calling process.  Then will insert the requested information
//					into the variable string_to_set.
//							
//	Parameters:
//		DataType:		The information is being request from calling process.
//		StringToSet:	The variable in which the requested will be stored.
//		
//	Returns:
//		SUCCESS:		  0  = There was a successful read.
//		NAM_ERROR:		(-1) = The read failed.
//
//

int GetData(char *DataType, char *StringToSet)
{
	
	dodebug(0,"GetData()","Entering function in GenUtilities class", (char*)NULL);
	if (MessageRequest(DataType)) {
		return(NAM_ERROR);
	}
	
	else {
		sprintf(StringToSet, "%s", PipeInfo.ReadMsg);
		StringToSet[strlen(StringToSet)] = '\0';
	}
	
	dodebug(0,"GetData()","Leaving function in GenUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	GenerateCommandLine:	This function will first call the proper function
//								depending upon the variable type that was passed
//								to it to setup the common variables used by
//								both imager and reader program.  Then if all is
//								well then the command line string is generated.
//							
//	Parameters:
//		type:			Used to determine which executable is to be executed.
//		
//	Returns:
//		SUCCESS:		  0  = The command line was generated.
//		NAM_ERROR:		(-1) = The command line generation failed.
//
//

int GenerateCommandLine(int type)
{

	int		c1;
	char	programString[NAM_MAX_PATH];
	char	labelString[NAM_MAX_PATH];
	char	midString[128];
	char	endString[128];
	
	dodebug(0,"GenerateCommandLine()","Entering function in GenUtilities class", (char*)NULL);
	//NULL out the strings variables that will be used throughtout this function.
	c1 = 32;
	memset(programString, '\0', sizeof(programString));
	memset(labelString, '\0', sizeof(labelString));
	memset(midString, '\0', sizeof(midString));
	memset(endString, '\0', sizeof(endString));
	memset(EtmInfo.CommandString, '\0', sizeof(EtmInfo.CommandString));

	switch(type) {

		case READER_ONLY:

		case TARGET:

			ReaderCommandLine(programString, midString, endString);
			/*c1 = EtmInfo.IadsVer == BASE3413 ? '\0' : '!';  
						if (EtmInfo.IadsVer != BASE3413) {
				sprintf(labelString, "%s", EtmInfo.TargetLabel);
			}*/
			c1 = '!';
			sprintf(labelString, "%s", EtmInfo.TargetLabel);

			break;

		case ZOOM:

			ImagerCommandLine(programString, midString, endString);
			dodebug(0, "GenerateCommandLine()", "endString is (%s)", endString, (char*)NULL);  
			//fflush(NULL);  

			sprintf(labelString, "");
			c1 = 0;  
			

			break;

		default  :

			dodebug(0, "GenerateCommandLine()", "type = %d", type);
			return(NAM_ERROR);

			break;
	}

	//reformatted commandstring for IADS 3.4
	sprintf(EtmInfo.CommandString, "%s %s%s%c%s%s", programString,
			"\"", EtmInfo.FileName, c1, labelString, endString);

	dodebug(0, "GenerateCommandLine()", "%s", EtmInfo.CommandString, NULL);

	/*if ((type == ZOOM) && (ImagInfo.WNotUsed == FALSE)) { 

		char TmpString[NAM_MAX_PATH];

		memset(TmpString, '\0', sizeof(TmpString));
		sprintf(TmpString, "%s", EtmInfo.CommandString);
		memset(EtmInfo.CommandString, '\0', sizeof(EtmInfo.CommandString));

		sprintf(EtmInfo.CommandString, "%s -W '(%d, %d, %d, %d)'", TmpString,
			   ImagInfo.wxoff, ImagInfo.wyoff, ImagInfo.wxlen, ImagInfo.wylen);
	}*/

	dodebug(0, "GenerateCommandLine()", "%s", EtmInfo.CommandString, (char*)NULL); 
	
	dodebug(0,"GenerateCommandLine()","Leaving function in GenUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	free_items:		This function will check the variable passed to it to see if
//						is empty or not.  If the variable is not empty then the
//						function will free the memory that allocated for it.
//							
//	Parameters:
//		item:		The variable that needs to have the memory freed that was
//					allocated for it.
//		
//	Returns:
//		none:
//
//

void free_item(char *item)
{
	
	dodebug(0,"free_item()","Entering function in GenUtilities class", (char*)NULL);
	if (item != NULL) {
		free(item);
	}
	dodebug(0,"free_item()","Leaving function in GenUtilities class", (char*)NULL);
}

//
//	Round:		This function will round the value passed to it to an integer.
//							
//	Parameters:
//		value:		This is the variable that will be rounded.
//		
//	Returns:
//		value:		Either the integer part rounded or value.
//
//

double Round(double value)
{
	
	dodebug(0,"Round()","Entering function in GenUtilities class", (char*)NULL);
	double	fracpart, integpart;

	fracpart = modf(value, &integpart);
	
	dodebug(0,"Round()","Leaving function in GenUtilities class", (char*)NULL);
	return(fracpart != 0.0 ? (integpart + floor(fracpart + .5)) : value);
}

//
//	EnumWindProc:	This function will first set the threadId variable
//						to the proper value the call checkForWindow which
//						will look for a match to the threadId and that
//						function will return the window handle so the
//						proper variable can be set to this handle.
//							
//	Parameters:
//		hWnd:		This is handle passed from the EnumWindows function.
//		lParam:		This variable is the mode of operation that is being
//					preformed.
//		
//	Returns:
//		TRUE:		Haven't found what we are looking for yet.
//		FALSE:		Stop the loop we found the user interface.
//
//

BOOL CALLBACK EnumWindProc(HWND hWnd, LPARAM lParam)
{

	DWORD	threadid;
	HWND	winHnd;
	
	dodebug(0,"EnumWindProc()","Entering function in GenUtilities class", (char*)NULL);
	if (lParam == ZOOM) {
		threadid = ProcInfo.ImageProcInfo.dwThreadId;
	}
	else {
		threadid = ProcInfo.ReadProcInfo.dwThreadId;
	}
	if ((winHnd = CheckForWindow(hWnd, threadid)) != NULL) {
		if (lParam == ZOOM) {
			WinInfo.ImageWhndl = winHnd;
		}
		else {
			WinInfo.ReadWhndl = winHnd;
		}
		return(FALSE);
	}
	
	dodebug(0,"EnumWindProc()","Leaving function in GenUtilities class", (char*)NULL);
	return(TRUE);
}

//
//	CheckForWindow:	This function will compare the return from
//						GetWindowThreadProcessId to the passed
//						threadId.  If they match then the while
//						loop looking for the correct window will
//						have its variable set to FALSE.
//							
//	Parameters:
//		hWnd:		This is the handle of a window that is from 
//					the while loop looking for the correct window.
//		threadId:	This is the threadId of the correct user inter-
//					face window that we are looking for.
//		
//	Returns:
//		value:		Either the integer part rounded or value.
//
//

HWND CheckForWindow(HWND hWnd, DWORD threadId)
{

	dodebug(0,"CheckForWindow()","Entering fucntion in GenUtilities class", (char*)NULL);
	unsigned long	procId;
	
	if (threadId == GetWindowThreadProcessId(hWnd, &procId)) {
		if (GetTopWindow(hWnd)) {
			continueEnum = FALSE;
			return(hWnd);
		}
	}

	continueEnum = TRUE;
	dodebug(0,"CheckForWindow()","Leaving function in GenUtilities class", (char*)NULL);
	return(NULL);
}

//
//	SetAppWindow:	This function will set the size of the user interface
//						window to either the requested size or to the size
//						of the window that the operator has sized the last
//						window to.
//							
//	Parameters:
//		none:
//
//	Returns:
//		SUCCESS:		 0   = The function set the user interface window size.
//		NAM_ERROR:		(-1) = The function failed to set the window size.
//
//

int SetAppWindow(void)
{

	long	x1, x2, y1, y2;
	HWND	hWnd;
	
	dodebug(0,"SetAppWindow()","Entering function in GenUtilities class", (char*)NULL);
	hWnd = EtmInfo.Mode == ZOOM ? WinInfo.ImageWhndl : WinInfo.ReadWhndl;
	x1 = EtmInfo.Mode == ZOOM ? WinInfo.Ix1 : WinInfo.Rx1;
	x2 = EtmInfo.Mode == ZOOM ? WinInfo.Ix2 : WinInfo.Rx2;
	y1 = EtmInfo.Mode == ZOOM ? WinInfo.Iy1 : WinInfo.Ry1;
	y2 = EtmInfo.Mode == ZOOM ? WinInfo.Iy2 : WinInfo.Ry2;

	if (!SetWindowPos(hWnd, HWND_TOP, x1, y1, x2, y2, SWP_NOACTIVATE)) {
		dodebug(0, "SetAppWindow()", "Failed to set window size", (char*)NULL);
		return(NAM_ERROR);
	}

	/*if (EtmInfo.Mode == ZOOM) {  
		WinInfo.ImagNotUsed = TRUE;
	}
	else {
		WinInfo.ReadrNotUsed = TRUE;
	}*/
	
	dodebug(0,"SetAppWindow()","Leaving function in GenUtilities class", (char*)NULL);
	return(SUCCESS);
}
