/////////////////////////////////////////////////////////////////////////////
//	File:	file_utils.cpp													/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		CreateFile()											/
//					Modified the code where the call to free() was being	/
//					called.  I now check to see if the variable is not NULL	/
//					before calling free to free up the memory.				/
//		1.0.2.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily				/
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
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <sys/types.h>
#include <sys/stat.h>
#include <windows.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <malloc.h>
#include "gpconcur.h"

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	CreateFile:		This function will create, make a copy of, a file that	/
//						is a copy of another file. Simple function.			/
//																			/
//	Parameters:																/
//		FileToCopy:		Pointer to the coping file.							/
//		FileToCreate:	Pointer to the file that needs creation.			/
//																			/
//	Returns:																/
//		SUCCESS:	Everything worked, what a concept.						/
//		GP_ERROR:	Something happened that shouldn't.  The variable errno	/
//					was set to reflect the cause.							/
//																			/
//	errno values:															/
//		E2BIG:		Argument list is too big.								/
//		ENOENT:		Command interpreter can't be found.						/
//		ENOEXEC:	Command interpreter file has invalid format and can't	/
//					be executed.											/
//		ENOMEM:		Not enough memory, a common problem with M$Slop.		/
//		FILENAME:	Improper file name was passed.							/
//																			/
/////////////////////////////////////////////////////////////////////////////

int CreateFile(char *FileToCopy, char *FileToCreate)
{

	int				HadError;
	unsigned int	ReadSize;
	char			*FileContents = NULL;
	FILE			*FileToCopyFp, *FileToCreateFp;
	struct _stat	FileStructFp;

//
// Make sure that all files are written and the info is there before
// this programs copies the file. Common software practice.
//

	fflush(NULL);

//
// This is a check to insure that proper file names were passed, and not empty strings.
//

	if (FileToCopy[0] == '\0' || FileToCreate[0] == '\0') {

		DoDebug(0, "CreateFile()", "%s%s%s%s%s", "Improper file name passed to this function\n",
				"FileToCopy = ", FileToCopy, "\nFileToCreate = ", FileToCreate, (char*)NULL);
		return(GP_ERROR);
	}

	errno = HadError = 0;

//
// First we will try to create the file, if that is successful, then open the file
// we are going to copy from, else close the file pointer and return an error.
//

	if ((FileToCreateFp = fopen(FileToCreate, "wb")) != NULL) {
		if ((FileToCopyFp = fopen(FileToCopy, "rb")) == NULL) {

			DoDebug(0, "CreateFile()", "%s%s%s", "fopen failed to open FileToCopy (",
					FileToCopy, ") check perms", (char*)NULL);
			fclose(FileToCreateFp);
			return(GP_ERROR);
		}
	}
	else {

		DoDebug(0, "CreateFile()", "%s%s%s", "fopen failed to open FileToCreate (",
				FileToCreate, ")", (char*)NULL);
		return(GP_ERROR);
	}

//
// Now we will find the size ofthe file that we are going to copy and stick the
// results into the stat structure, if we can't stat the file parameters then set
// the variable HadError TRUE.  If all goes well then read the file contents into
// the variable FileContents, if we were successful in mallocing the required memory.
//

	if (_stat(FileToCopy, &FileStructFp) != GP_ERROR) {

		if ((FileContents = (char *)calloc((size_t)FileStructFp.st_size + 2, sizeof(char))) != NULL) {

			if ((ReadSize = fread(FileContents, sizeof(char), (size_t)FileStructFp.st_size,
					   FileToCopyFp)) != (size_t)FileStructFp.st_size) {

				if (FileContents != NULL) {
					free(FileContents);
				}
				
				DoDebug(FILE_READ_ERROR, "CreateFile()", (char*)NULL);
				HadError++;
			}
			else {
				FileContents[strlen(FileContents)] = '\0';
			}
		}
		else {
			DoDebug(0, "CreateFile()", "%s", "malloc failed to allocated the required memory", (char*)NULL);
			HadError++;
		}
	}
	else {
		DoDebug(errno, "CreateFile()", (char*)NULL);
		HadError++;
	}

	if (HadError) {

		if (FileToCopyFp != NULL) {
			fclose(FileToCopyFp);
		}
		if (FileToCreateFp != NULL) {
			fclose(FileToCreateFp);
		}
	}

//
// If we haven't had any errors yet then we are ready to transfer from the
// buffer FileContents to the file that we wanted to create as a copy of the
// orignal file.
//

	else {

		if (fwrite(FileContents, sizeof(char), (size_t)FileStructFp.st_size,
					FileToCreateFp) != (size_t)FileStructFp.st_size) {
			DoDebug(FILE_WRITE_ERROR, "CreateFile()", (char*)NULL);
			HadError++;
		}

		if (FileContents != NULL) {
			free(FileContents);
		}

		fclose(FileToCopyFp);
		fclose(FileToCreateFp);
	}

	return(HadError == FALSE ? SUCCESS : GP_ERROR);
}