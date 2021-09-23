/////////////////////////////////////////////////////////////////////////////
//	File:	FileUtilities.cpp												/
//																			/
//	Creation Date:	2 March 2005											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
// 		1.0.1.0		get_this()												/
// 					changed the malloc to a calloc call so the variable 	/
// 					would be initialized by default, eliminated the 		/
// 					tmpbuf[0] = '\0' statement.								/
//		1.0.2.0		Added a new element row to GetThis: DIR2_TOP_LEVEL,		/
//					DIRECTORY2_SEP, and DIRECTORY_SIZE.  This allowed for	/
//					the checking where the directory path contain / instead	/
//					of \ .													/
//					GetInfo()												/
//					Corrected the debug statement, added (char*) to			/
//					terminate the debug statement properly.					/
//					FindPoint()												/
//					Corrected the debug statement, added (char*) to			/
//					terminate the debug statement properly.					/
//					MoveToPoint()											/
//					Corrected the debug statement, added (char*) to			/
//					terminate the debug statement properly.					/
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
#include <stdlib.h>
#include <malloc.h>
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local																/
/////////////////////////////////////////////////////////////////////////////

typedef	struct {
	char	*StartPoint;
	char	*EndPoint;
	int		Size;
}GET_THIS;

GET_THIS	GetThis[] = {

//	StartPoint				EndPoint				Size				
{	FILE_MSG,				END_FILE,				FILE_DATA,			},
{	STATUS_MSG,				END_MSG,				STATUS_DATA,		},
{	WINDOW_MSG,				WINDOW_END,				WINDOW_DATA,		},
{	OPTIONS_MSG,			OPTION_END,				OPTION_DATA,		},
{	REFERENCE_MSG,			REFERENCE_END,			REFERENCE_DATA,		},
{	FUNCTIONAL_MSG,			END_MSG,				FUNCTIONAL_DATA,	},
{	OPERATIONAL_MSG,		END_MSG,				OPERATIONAL_DATA,	},
{	DIR_TOP_LEVEL,			DIRECTORY_SEP,			DIRECTORY_SIZE,		},
{	DIR2_TOP_LEVEL,			DIRECTORY2_SEP,			DIRECTORY_SIZE,		},
{	NULL,					NULL,					0,					}
};

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// RemoveSpaces:	This function will remove any white spaces from either the
//						left or right direction up to the first non white space.
//						This function will either increment or decrement the
//						index_pt's value.
//
// Parameters:
//		IndexPt:		This is the address to the integer value which is
//						requested to be changed.
//		contents:		The character string where we remove the white spaces.
//		direction:		The direction in which the white spaces are removed.
//
// Return:
//		none:
//
//

void RemoveSpaces(int *IndexPt, char *contents, int direction)
{
	
	dodebug(0,"RemoveSpaces()","Entering function in FileUtilities class", (char*)NULL);
	while (contents[*IndexPt] == '\040' || contents[*IndexPt] == '\011') {
		if (direction == FORWARD) {
			(*IndexPt)++;
		}
		else {
			(*IndexPt)--;
		}
	}
	dodebug(0,"RemoveSpaces()","Leaving function in FileUtilities class", (char*)NULL);
}

//
// GetInfo:		This function will extract the information that is between
//						the StartPoint and the EndPoint and check to see if
//						the size is less then the GetThis[item].Size.  If the
// 						contents string passed to the function has the correct
// 						information in it, a pointer to the correct string is
// 						return to the calling function, else NULL.
//
// Parameters:
//		contents:	This is the character string the information will come from.
//		item:		The index value used in the getthis structure, of what we
//					are looking for.
// 
// Return:
//		tmpbuf:		A character pointer to the string if found successfully.
//		NULL:		What is return when it didn't go well. (An Error)
//
//

char *GetInfo(char *contents, int item)
{

	int		TmpIndex, IndexPos = 0;
	char	*TmpBuf;
	
	dodebug(0,"GetInfo()","Entering function in FileUtilities class", (char*)NULL);
	dodebug(0, "GetInfo()", "Contents is: %s. Passed into MoveToPoint() as 3rd param", contents, (char*)NULL);
	char *strtPt = GetThis[item].StartPoint ;
	dodebug(0, "GetInfo()", " GetThis[item].StartPoint is: %s.Passed into MoveToPoint() as 1st param", strtPt, (char*)NULL);
	if (MoveToPoint(GetThis[item].StartPoint, &IndexPos, contents, IndexPos) == NAM_ERROR) {
		return(NULL);
	}

	RemoveSpaces(&IndexPos, contents, FORWARD);

	TmpIndex = IndexPos;

	if (FindPoint(GetThis[item].EndPoint, &IndexPos, contents, IndexPos) == NAM_ERROR) {
		return(NULL);
	}

	if (IndexPos <= TmpIndex) {
		return(NULL);
	}

	RemoveSpaces(&IndexPos, contents, BACKWARD);

	if (((IndexPos - TmpIndex) + 1) > GetThis[item].Size) {
		dodebug(0, "GetInfo()", "%s%s%d%s%d", GetThis[item].StartPoint,
				" is larger then the buffer, only ", GetThis[item].Size,
				" is allowed but you want ", (IndexPos - TmpIndex) + 1, (char*)NULL);  
		return(NULL);
	}

	if ((TmpBuf = (char*)calloc(((IndexPos - TmpIndex) + 2), sizeof(char))) == NULL) {
		dodebug(errno, "GetInfo()", (char*)NULL);
		return(NULL);
	}

	strncpy(TmpBuf, &contents[TmpIndex], (IndexPos - TmpIndex) + 1);
	TmpBuf[(IndexPos - TmpIndex) + 1] = '\0';
	
	dodebug(0,"GetInfo()","Leaving function in FileUtilities class", (char*)NULL);
	return(TmpBuf);
}

//
// FindPoint:		This function will find the location of the string passed in
//						check_string in the string faultfilecontents begining at
//						location of start_pt.  If the string is found within the
//						string then the value of the index_pt is set to the
//						location of the first character of the string
//						check_string.
//
// Parameters
//		CheckString:		This is the string that is being searched for in the
//							character string.
//		IndexPt:			This is the address to the integer value which is
//							requested to be changed.
//		contents:			This is the character string that the search is
//							being done on.
//		StartPt:			This is the starting point where we look for
//							check_string.
//
// Return
//		SUCCESS:			  0  - The check_string was found and the index_pt
//								   was set to its address index value.
//		NAM_ERROR:			(-1) - The check_string wasn't found, index_pt value
//								   is left unchanged.
//
//

int FindPoint(char *CheckString, int *IndexPt, char *contents, int StartPt)
{

	char	*ptFound;
	
	dodebug(0,"FindPoint()","Entering function in FileUtilities class", (char*)NULL);
	 if ((ptFound = strstr( &contents[StartPt], CheckString)) != NULL) {
		*IndexPt = ptFound - (contents + 1);
	}

	else {
		dodebug(0, "FindPoint()", "%s%s%s", "Failed to find CheckString (",
				CheckString, ") in the passed character string", (char*)NULL);  
		return(NAM_ERROR);
	}
	dodebug(0,"FindPoint()","Leaving function in FileUtilities class", (char*)NULL);

	return(SUCCESS);
}

//
// MoveToPoint:		This function will find the location of the string
//							passed in check_string in the string contents
//							begining at location of start_pt.  If the string is
//							found within the string then the value of the
//							index_pt is set to the location of one character
//							past the last character of the string check_string.
//
// Parameters
//		check_string:		This is the string that is being searched for in the
//							character string.
//		index_pt:			This is the address to the integer value which is
//							requested to be changed.
//		contents:			This is the character string that the search is
//							being done on.
//		start_pt:			This is the starting point where we look for
//							check_string.
//
// Return
//		SUCCESS:			 0  - The check_string was found and the index_pt
//								  was set to its address index value.
//		NAM_ERROR:			-1  - The check_string wasn't found, index_pt value
//								  is left unchanged.
//
//

int MoveToPoint(char *CheckString, int *IndexPt, char *contents, int StartPt)
{

	char	*ptFound;
	
	dodebug(0,"MoveToPoint()","Entering function in FileUtilities class", (char*)NULL);
	if ((ptFound = strstr( &contents[StartPt], CheckString)) != NULL) {
		*IndexPt = ptFound - contents;
		*IndexPt += (size_t)strlen(CheckString);
	}

	else {
		dodebug(0, "MoveToPoint()", "%s%s%s %s", "Failed to find CheckString(",
				CheckString, ") in the passed character string", contents, (char*)NULL); 
		return(NAM_ERROR);
	}
	
	dodebug(0,"MoveToPoint()","Leaving function in FileUtilities class", (char*)NULL);
	return(SUCCESS);
}
