/////////////////////////////////////////////////////////////////////////////
//	File:	FileUtilities.cpp												/
//																			/
//	Creation Date:	19 Jan 2005												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
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
#include "Constants.h"
#include "ProgramLauncher.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

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
	
	memset(tmpBuf, '\0', sizeof(tmpBuf));

	if (FileStyle(tmpBuf, ProgInfo.FileName)) {
		return(NAM_ERROR);
	}

	memset(ProgInfo.FileLocation, '\0', sizeof(ProgInfo.FileLocation));
	ConvertChar(tmpBuf, ProgInfo.FileLocation, '\\', '/', YES);

	for (i = strlen(ProgInfo.FileLocation); i != 0; i--) {

		if (ProgInfo.FileLocation[i] == '.') {
			extension = YES;
		}
	}

	if (FilePresent(ProgInfo.FileLocation, extension)) {
		return(NAM_ERROR);
	}

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
//		dup					If 1 will only change 1 of back to back characters.
//
// Returns:
//		none:
//

void ConvertChar(char *convertString, char *newString, 
				  char convertFrom, char convertTo, int dup)
{

	int		i, j;
	char	tmpBuf[NAM_MAX_PATH];

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

	memset(tmpBuf, '\0', sizeof(tmpBuf));
	memset(dirPath, '\0', sizeof(dirPath));
	memset(savePath, '\0', sizeof(savePath));

	if (_getcwd(savePath, NAM_MAX_PATH) == NULL) {
		dodebug(0, "FileStyle()",
				"Failed to get current working dir", (char *)NULL);
		return(NAM_ERROR);
	}

//
// Call the function file_path to find out how the file name was passed to me.
// I will check to see if it has any path info or not.
//

	pathType = FilePath(oldFileName[0], oldFileName[1], oldFileName[2]);

//
// Now depending upon the path type, we will find the complete path for the
// filename.
//

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
			
			dodebug(0, "FileStyle()", "Improper file type sent to program",
					(char *)NULL);
			return(NAM_ERROR);

		break;
	}

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

	memset(fileBuf, '\0', sizeof(fileBuf));

	if (dotThere) {
		if (_stat(fileName, &tmpBuf)) {
			dodebug(0, "FilePresent()", "File %s doesn't exist", fileName);
			return(NAM_ERROR);
		}
	}
	else {
		sprintf(fileBuf, "%s", fileName);
		strcat(fileBuf, ".exe");

		if (_stat(fileBuf, &tmpBuf)) {

			if (_stat(fileName, &tmpBuf)) {
				dodebug(0, "FilePresent()", "File %s either isn't an executable or doesn't exist", fileName);
				return(NAM_ERROR);
			}
		}
		else {
			sprintf(fileName, "%s", fileBuf);
		}
	}

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
//		firstChar:		The first character of the character string of the program.
//		secondChar:		The second character of the character string of the program.
//		thirdChar:		The third character of the character string of the program.
//
// Returns:
//		SUCCESS:	  0	 = successful completion of the function.
//		NAM_ERROR:	(-1) = failure of a required task.
//

int FilePath(int firstChar, int secondChar, int thirdChar)
{
	
	if (firstChar == '\\' || firstChar == '/') {
		return(ROOTDIR);
	}
		
	else if (((firstChar > 64 && firstChar < 91)    ||
			  (firstChar > 96 && firstChar < 123))  &&
			secondChar == ':' && (thirdChar == '\\' || thirdChar == '/')) {
		return(DRIVEDIR);
	}
	
	else if (firstChar == '.'  && secondChar == '.' &&
			(thirdChar == '\\' || thirdChar == '/')) {
		return(RELATIVEPATH);
	}
	
	else if (firstChar == '.' && (secondChar == '\\' || secondChar == '/')) {
		return(FIXRELATIVE);
	}
	
	else if (firstChar >  32  && firstChar <  127 &&
		     firstChar != '*' && firstChar != '"' &&
			 firstChar != '<' && firstChar != '>' &&
			 firstChar != '?' && firstChar != '|') {
		return(INPATH);
	}
	
	else {
		return(NAM_ERROR);
	}
}

//
// BuildCommandString:	This function will generate the command string for the
//							function CreateProcess from the variables
//							ProgInfo.FileLocation and ProgInfo.CommandOption
//
// Parameters:
//		none:
//
// Returns:
//		none:
//

void BuildCommandString(void)
{

	if (strlen(ProgInfo.CommandOptions) > 0) {
		sprintf(ProgInfo.CommandString, "\"%s\" %s", ProgInfo.FileLocation, ProgInfo.CommandOptions);
	}
	else {
		sprintf(ProgInfo.CommandString, "%s", ProgInfo.FileLocation);
	}

	return;
}
