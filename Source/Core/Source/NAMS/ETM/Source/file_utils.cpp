/////////////////////////////////////////////////////////////////////////////
//	File:	file_utils.cpp													/
//																			/
//	Creation Date:	19 Jan 2005												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of etmnam to implement Iads ver 3.2	/
//					software.												/
//		2.0.1.0		correct_target_frame()									/
//					modified the way the two character arrays were being	/
//					zero'd/initialized.										/
//					test_for_file()											/
//					modified the way the character array is being			/
//					zero'd/intialized also zero'd the character array being	/
// 					sent to convert_char().									/
//					convert_char()											/
//					modified the way the character array is being			/
//					zero'd/initialize, also removed the where newString		/
// 					character array zero element was being set to NULL at	/
// 					end of the function, and modified the sprintf() there.	/
//					file_style()											/
//					modified the way the three character arrays were being	/
//					zero'd/initialized. Modified the sprintf() in the case	/
// 					statement DRIVEDIR, and the first _snprintf() call in	/
// 					case statement RELATIVEPATH.							/
//					file_present()											/
//					modified the way the character array is being			/
//					zero'd/initialized, also modified the way the sprintf()	/
// 					at the else if READ_ONLY statement was being used, this	/
// 					should prevent possible of not terminating the character/
// 					array. Removed the fileName[0] = '\0'; statement in the	/
// 					else of the else if READ_ONLY statement.				/
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
#include <direct.h>
#include <stdlib.h>
#include <malloc.h>
#include <string.h>
#include "etmnam.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

//
// CorrectTargetFrame:	This function will added escape characters for the
//								characters that need them.  Right now the only
//								character that needs it is the % character. This
//								will be escaped by another % character.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

void CorrectTargetFrame(void)
{

	int		i, j;
	char	tmpBuf[NAM_MAX_PATH];
	
	dodebug(0,"CorrectTargetFrame()","Entering function in file_utils class", (char*)NULL);
	memset(tmpBuf, '\0', sizeof(tmpBuf));

	for (i = 0, j = 0; i < (int)strlen(EtmInfo.FrameTarget); i++, j++) {

		if (EtmInfo.FrameTarget[i] == '%') {
			tmpBuf[j] = '%';
			j++;
		}
	
		tmpBuf[j] = EtmInfo.FrameTarget[i];
	}

	tmpBuf[j] = '\0';
	memset(EtmInfo.FrameTarget, '\0', sizeof(EtmInfo.FrameTarget));
	sprintf(EtmInfo.FrameTarget, "%s", tmpBuf);
	dodebug(0,"CorrectTargetFrame()","Leaving function in file_utils class", (char*)NULL);

}

//
// TestForFile:	This function will first determine if the file is in the
//						current directory path or is in another directory path
//						that is relative to current working directory.  Also
//						will incorperate the full path to the file name.  This
//						is done to prevent any problems of the executable not
//						being able to handle relative paths.  Then will convert
//						all \ to / which makes it easier to work with.  Then see
//						if the file has an extension, if there there is an
//						extension then test to see if it exist else add the
//						proper extension then test to see if it exist.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

int TestForFile(void)
{

	int		i, extension = NO;
	char	tmpBuf[NAM_MAX_PATH];
	
	dodebug(0,"TestForFile()","Entering function in file_utils class", (char*)NULL);
	memset(tmpBuf, '\0', sizeof(tmpBuf));

	if (FileStyle(tmpBuf, EtmInfo.FileLocation)) {
		return(NAM_ERROR);
	}

	memset(EtmInfo.FileLocation, '\0', sizeof(EtmInfo.FileLocation));
	ConvertChar(tmpBuf, EtmInfo.FileLocation, '\\', '/', YES);

	for (i = strlen(EtmInfo.FileLocation); i != 0; i--) {

		if (EtmInfo.FileLocation[i] == '.') {
			extension = YES;
		}
	}

	if (FilePresent(EtmInfo.FileLocation, extension)) {
		return(NAM_ERROR);
	}
	dodebug(0,"TestForFile()","Leaving function in file_utils class", (char*)NULL);

	return(SUCCESS);	
}

//
// ConvertChar:	This function will convert the character in the variable
//						convertFrom to the character in the variable in
//						convertTo in the character string convertString and
//						put the converted character string in the variable
//						newString.  Also depending upon the variable dup the
//						function eliminate duplication of convertTo characters.
//
// Parameters:
//		convertString:		The character string the conversion will be done on.
//		newString:			The variable the converted string will be put into.
//		convertFrom:		The character that needs to be converted.
//		convertTo:			The character that will be converted to.
//
// Returns:
//		none:
//

void ConvertChar(char *convertString, char *newString, 
				  char convertFrom, char convertTo, int dup)
{

	int		i, j;
	char	tmpBuf[NAM_MAX_PATH];
	
	dodebug(0,"ConvertChar()","Entering function in file_utils class", (char*)NULL);
	memset(tmpBuf, '\0', sizeof(tmpBuf));

	for (i = 0, j = 0; i < (int)strlen(convertString); i++, j++) {

		if (convertString[i] == convertFrom) {

			tmpBuf[j] = convertTo;

			if (dup) {

				for (i = i + 1; i < (int)strlen(convertString); i++) {

					if (convertString[i] != convertFrom) {
						i--;
						break;
					}
				}
			}
		}
		else {
			tmpBuf[j] = convertString[i];
		}
	}

	tmpBuf[j] = '\0';
	sprintf(newString, "%s", tmpBuf);

	dodebug(0,"ConvertChar()","Leaving function in file_utils class", (char*)NULL);
}

//
// FileStyle:	This function will examine the oldFileName looking for path
//					information.  First I will start by looping through the
//					character string variable oldFileName from the end towards
//					the beginning looking for either / or \.  Then I examine the
//					loop counter to see if is zero and the character is not a
//					/ or \, then it is just the file name and I will append the
//					file name to a variable that holds the the current working
//					directory.  If this is not the case then I look to see if
//					the first character is a '.', this means that this is a
//					relative path. I will remove the file name from the path
//					information then change directory to the path information
//					then get the current working directory and join both the
//					current working directory path information with the file
//					name.  And if this is not the case then the variable
//					oldFileName contains the proper information.  The now proper
//					file name with the correct and full path information the
//					variable newFileName is set to hold this information.
//
// Parameters:
//		newFileName:	The variable that will hold the full path information
//						with the file name.
//		oldFileName:	The variable that is used to determine how to get the
//						full path information.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

int FileStyle(char *newFileName, char *oldFileName)
{
	
	int		pathType, drive, i;
	char	tmpBuf[NAM_MAX_PATH];
	char	dirPath[NAM_MAX_PATH];
	char	savePath[NAM_MAX_PATH];
	
	dodebug(0,"FileStyle()","Entering function in file_utils class", (char*)NULL);
	memset(tmpBuf, '\0', sizeof(tmpBuf));
	memset(dirPath, '\0', sizeof(dirPath));
	memset(savePath, '\0', sizeof(savePath));

	if (_getcwd(savePath, NAM_MAX_PATH) == NULL) {
		dodebug(0, "FileStyle()",
				"Failed to get current working dir", (char *)NULL);
		return(NAM_ERROR);
	}

	/*Call the function file_path to find out how the file name was passed.
	Check to see if it has any path info or not.*/
	pathType = FilePath(oldFileName[0], oldFileName[1], oldFileName[2]);

	// Now depending upon the path type, find the complete path for the filename.
	switch(pathType) {

		case ROOTDIR:

			drive = _getdrive();
			sprintf(newFileName, "%c:%s", (drive + 96), oldFileName);

			break;
		
		case DRIVEDIR:
			
			sprintf(newFileName, "%s", oldFileName);
		
			break;
		
		case FIXRELATIVE:
					
		case RELATIVEPATH:

			for (i = strlen(oldFileName); i != 0; i--) {
				if (oldFileName[i] == '\\' ||
					oldFileName[i] == '/') {
					break;
				}
			}

			if (i == 0) {
				return(NAM_ERROR);
			}

			if (pathType == RELATIVEPATH) {
				_snprintf(tmpBuf, (size_t)i, "%s", oldFileName);
			}
			else {
				_snprintf(tmpBuf, (size_t)i, ".%s", oldFileName);
			}

			if (_chdir(tmpBuf)) {
				dodebug(0, "FileStyle()",
						"Failed to change file working dir (%s)", tmpBuf, (char *)NULL);
				return(NAM_ERROR);
			}
			else {
				if (_getcwd(dirPath, NAM_MAX_PATH) == NULL) {
					dodebug(0, "FileStyle()",
							"Failed to get current working dir", (char *)NULL);
					if (_chdir(savePath)) {
						dodebug(0, "FileStyle()",
								"Failed to change working dir", (char *)NULL);
						return(NAM_ERROR);
					}
					return(NAM_ERROR);
				}
				else {

					_snprintf(newFileName, NAM_MAX_PATH, "%s%s",
							  dirPath, &oldFileName[i]);
				}
			}
			if (_chdir(savePath)) {
				dodebug(0, "FileStyle()",
						"Failed to change working dir", (char *)NULL);
				return(NAM_ERROR);
			}

			break;
			
		case INPATH:
			
			_snprintf(newFileName, NAM_MAX_PATH, "%s/%s", savePath, oldFileName);
		
			break;
		
		case NAM_ERROR:
			
			return(NAM_ERROR);
		
			break;
		
		default:
			
			dodebug(0, "FileStyle()", "Improper file type sent to etmnam",
					(char *)NULL);
			return(NAM_ERROR);

		break;
	}
	
	dodebug(0,"FileStyle()","Entering function in file_utils class", (char*)NULL);
	return(SUCCESS);
}

//
// FilePresent:	This function will test for the presence of the variable
//						fileName.  Depending upon the variable dotThere, this
//						function will install the proper extension to the
//						variable fileName before it test for the presence of the
//						fileName and if the variable doesn't have the extension
//						the function will append it to the variable fileName
//						depending what the variable EtmInfo.Mode is set to.
//
// Parameters:
//		fileName:		The variable that holds the full path and file name that
//						is to be check for existence.
//		dotThere:		TRUE  = the variable fileName has an extension.
//						FALSE = the variable fileName doesn't have an extension.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

int FilePresent(char *fileName, int dotThere)
{

	char			fileBuf[NAM_MAX_PATH];
	struct _stat	tmpBuf;
	
	dodebug(0,"FilePresent()","Entering function in file_utils class", (char*)NULL);
	memset(fileBuf, '\0', sizeof(fileBuf));

	if (dotThere) {
		if (_stat(fileName, &tmpBuf)) {
			dodebug(0, "FilePresent()", "File %s doesn't exist", fileName);
			return(NAM_ERROR);
		}
	}
	else if (EtmInfo.Mode == ZOOM_VIEW) {
		strcat(fileName, ".pic");
		if (_stat(fileName, &tmpBuf)) {
			dodebug(0, "FilePresent()", "File %s doesn't exist", fileName);
			return(NAM_ERROR);
		}
	}
	else if (EtmInfo.Mode == READ_ONLY || EtmInfo.Mode == READ_TARGET) {
		sprintf(fileBuf, "%s", fileName);
		strcat(fileBuf, ".ide");

		if (_stat(fileBuf, &tmpBuf)) {

			memset(fileBuf, '\0', sizeof(fileBuf));
			sprintf(fileBuf, "%s", fileName);
			strcat(fileBuf, ".sgm");

			if (_stat(fileName, &tmpBuf)) {

				memset(fileBuf, '\0', sizeof(fileBuf));
				sprintf(fileBuf, "%s", fileName);
				strcat(fileBuf, ".xml");

				if (_stat(fileName, &tmpBuf)) {
					dodebug(0, "FilePresent()", "File %s either isn't a IADS file or doesn't exist", fileName);
					return(NAM_ERROR);
				}
			}
		}
		else {
			sprintf(fileName, "%s", fileBuf);
		}
	}
	else {
		dodebug(0, "FilePresent()", "EtmInfo.Mode = %d", EtmInfo.Mode);
		return(NAM_ERROR);
	}
	
	dodebug(0,"FilePresent()","Leaving function in file_utils class", (char*)NULL);
	return(SUCCESS);
}

//
// FillInInfo:	This function will set the values of the SendInfo structure
//						from the values gather from parsing the arguments that
//						passed to the program on startup.
//
// Parameters:
//		function:	This variable is used to set the reference element of the
//					SendInfo structure.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

int FillInInfo(int function)
{
	
	dodebug(0,"FillInInfo()","Entering function in file_utils class", (char*)NULL);
	if ((SendInfo[FILE_CMD].u.charValue =
		(char **)calloc(1, sizeof(char))) == NULL) {
		dodebug(0, "FillInInfo()", "Failed to allocate memory", (char *)NULL);
		return(NAM_ERROR);
	}

	if ((SendInfo[FILE_CMD].u.charValue[0] =
		_strdup(EtmInfo.FileLocation)) == NULL) {
		dodebug(0, "FillInInfo()", "Failed to allocate memory", (char *)NULL);
		return(NAM_ERROR);
	}
		
	SendInfo[FILE_CMD].numOfVals = 1;

	if (function == READ_ONLY || function == READ_TARGET) {
		if ((SendInfo[REFERENCE_CMD].u.charValue =
			(char **)calloc(1, sizeof(char))) == NULL) {
			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		if ((SendInfo[REFERENCE_CMD].u.charValue[0] =
			_strdup(EtmInfo.FrameTarget)) == NULL) {
			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		SendInfo[REFERENCE_CMD].numOfVals = 1;
	}

	if (WinInfo.winNum > 0) {
		if ((SendInfo[WINDOW_CMD].u.intValue = 
			(int *)calloc(4, sizeof(int))) == NULL) {

			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		SendInfo[WINDOW_CMD].numOfVals = 4;

		SendInfo[WINDOW_CMD].u.intValue[0] = WinInfo.win_x1;
		SendInfo[WINDOW_CMD].u.intValue[1] = WinInfo.win_y1;
		SendInfo[WINDOW_CMD].u.intValue[2] = WinInfo.win_x2;
		SendInfo[WINDOW_CMD].u.intValue[3] = WinInfo.win_y2;
	}
	else if (WinInfo.winNum == 0) {
		if ((SendInfo[WINDOW_CMD].u.charValue = 
			(char **)calloc(1, sizeof(char))) == NULL) {
			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		if ((SendInfo[WINDOW_CMD].u.charValue[0] = _strdup("None")) == NULL) {
			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		SendInfo[WINDOW_CMD].numOfVals = 1;
		SendInfo[WINDOW_CMD].formatType = STRING_TYPE;
	}

	if (WinInfo.offsetNum > 0 && WinInfo.predefine[0] == '\0') {
		if ((SendInfo[OPTIONS_CMD].u.intValue = 
			(int *)calloc(4, sizeof(int))) == NULL) {

			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		SendInfo[OPTIONS_CMD].numOfVals = 4;
		SendInfo[OPTIONS_CMD].formatType = DIGIT_TYPE;

		SendInfo[OPTIONS_CMD].u.intValue[0] = WinInfo.pic_x1;
		SendInfo[OPTIONS_CMD].u.intValue[1] = WinInfo.pic_y1;
		SendInfo[OPTIONS_CMD].u.intValue[2] = WinInfo.pic_x2;
		SendInfo[OPTIONS_CMD].u.intValue[3] = WinInfo.pic_y2;
	}
	else if (WinInfo.offsetNum == 0 && WinInfo.predefine[0] != '\0') {
		if ((SendInfo[OPTIONS_CMD].u.charValue =
			(char **)calloc(1, sizeof(char))) == NULL) {
			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		if ((SendInfo[OPTIONS_CMD].u.charValue[0] =
			_strdup(WinInfo.predefine)) == NULL) {
			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}			

		SendInfo[OPTIONS_CMD].numOfVals = 1;
		SendInfo[OPTIONS_CMD].formatType = STRING_TYPE;
	}
	else if (WinInfo.offsetNum == 0 && WinInfo.predefine[0] == '\0') {
		if ((SendInfo[OPTIONS_CMD].u.charValue = 
			(char **)calloc(1, sizeof(char))) == NULL) {
			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		if ((SendInfo[OPTIONS_CMD].u.charValue[0] = _strdup("None")) == NULL) {
			dodebug(0, "FillInInfo()", "Failed to allocate memory",
				   (char *)NULL);
			return(NAM_ERROR);
		}

		SendInfo[OPTIONS_CMD].numOfVals = 1;
		SendInfo[OPTIONS_CMD].formatType = STRING_TYPE;
	}
	else {
		dodebug(0, "FillInInfo()", "Improper use of the Zoom Options",
				(char *)NULL);
		return(NAM_ERROR);
	}
	
	dodebug(0,"FillInInfo()","Leaving function in file_utils class", (char*)NULL);
	return(SUCCESS);
}

//
// FilePath:	This function will test for the presence of the variable
//						fileName.  Depending upon the variable dotThere, this
//						function will install the proper extension to the
//						variable fileName before it test for the presence of the
//						fileName and if the variable doesn't have the extension
//						the function will append it to the variable fileName
//						depending what the variable EtmInfo.Mode is set to.
//
// Parameters:
//		fileName:		The variable that holds the full path and file name that
//						is to be check for existence.
//		dotThere:		TRUE  = the variable fileName has an extension.
//						FALSE = the variable fileName doesn't have an extension.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

int FilePath(int firstChar, int secondChar, int thirdChar)
{
	
	dodebug(0,"FilePath()","Entering function in file_utils class", (char*)NULL);
	if (firstChar == '\\' || firstChar == '/') 
	{
		dodebug(0,"FilePath()","Leaving function in file_utils class", (char*)NULL);
		return(ROOTDIR);
	}
	else if (((firstChar > 64 && firstChar < 91) || (firstChar > 96 && firstChar < 123)) &&	secondChar == ':' && (thirdChar == '\\' || thirdChar == '/')) 
	{
		dodebug(0,"FilePath()","Leaving function in file_utils class", (char*)NULL);
		return(DRIVEDIR);
	}
	else if (firstChar == '.'  && secondChar == '.' && (thirdChar == '\\' || thirdChar == '/')) 
	{
		dodebug(0,"FilePath()","Leaving function in file_utils class", (char*)NULL);
		return(RELATIVEPATH);
	}
	else if (firstChar == '.' && (secondChar == '\\' || secondChar == '/')) 
	{
		dodebug(0,"FilePath()","Leaving function in file_utils class", (char*)NULL);
		return(FIXRELATIVE);
	}
	else if (firstChar >  32  && firstChar <  127 && firstChar != '*' && firstChar != '"' && firstChar != '<' && firstChar != '>' && firstChar != '?' && firstChar != '|') 
	{
		dodebug(0,"FilePath()","Leaving function in file_utils class", (char*)NULL);
		return(INPATH);
	}
	else 
	{
		dodebug(0,"FilePath()","Leaving function in file_utils class", (char*)NULL);
		return(NAM_ERROR);
	}
}
