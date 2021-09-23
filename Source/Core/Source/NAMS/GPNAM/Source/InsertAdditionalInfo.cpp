/////////////////////////////////////////////////////////////////////////////
//	File:	InsertAdditionalInfo.cpp										/
//																			/
//	Creation Date:	3 June 2015												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0:	Created													/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <malloc.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "gpnam.h"

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
// InsertAdditionalInfo:	This program will insert additional info at the	/
//								bottom of the m910nam .dia and will call the/
//								function FixTmpFile to correct the .xml/.ide/																			/
// Parameters:																/
//		argc:		The # of arguments passed to main.						/
//		argv:		Character list of the arguments passed to main.			/
//																			/
// Returns:																	/
//		none:		This is a void function call.							/
//																			/
/////////////////////////////////////////////////////////////////////////////

void InsertAdditionalInfo(int argc, char *argv[])
{

	int				FileType;
	int				i;
	char			StringToFill[81];
	char			TmpFileName[GP_MAX_PATH];
	FILE			*FileToBeReadfp;
	struct _stat	StatBuf;

//
// Make sure that all files are written and the info is there before
// this programs opens the files for reading and writing.
//

	fflush(NULL);

//
// Find out what version of IADS is being used so that
// the proper file is selected.
//

	FileType = _stat(IADS_3_2_7,  &StatBuf) == 0 ? IDE :
			   _stat(IADS_3_4_13, &StatBuf) == 0 ? XML : 
			   _stat(IADS_3_4_25, &StatBuf) == 0 ? XML : GP_ERROR;

	if (FileType == GP_ERROR) {
		dodebug(0, "InsertAdditionalInfo()", "Couldn't stat which IADS is being used", (char*)NULL);
		return;
	}

	sprintf(TmpFileName, "%s%s.%s", gp_info.log_location, "m910nam", FileType == IDE ? "ide" : "xml");

	if ((FixTmpFile(TmpFileName, argc, argv)) == GP_ERROR) {
		dodebug(0, "InsertAdditionalInfo()", "failed after  FixTmpFile", (char*)NULL);
		return;
	}

	if ((_stat(TmpFileName, &StatBuf)) == GP_ERROR) {
		dodebug(errno, "InsertAdditionalInfo()", NULL, (char*)NULL);
		gp_info.return_value = FILE_READ_ERROR;
		return;
	}

	memset(TmpFileName, '\0', sizeof(TmpFileName));
	sprintf(TmpFileName, "%s%s.%s", gp_info.log_location, "m910nam", "dia");

	if ((FileToBeReadfp = fopen(TmpFileName, "a+b")) == NULL) {
		dodebug(errno, "InsertAdditionalInfo()", NULL, (char*)NULL);
		gp_info.return_value = FILE_OPEN_ERROR;
		return;
	}

	for (i = ATLAS + 2; i < argc; i++) {
		setArgumentValue (ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR, argv[i], StringToFill, sizeof(StringToFill), NULL);
		fprintf(FileToBeReadfp, "%s\n", StringToFill);
	}

	fflush(NULL);
	fclose(FileToBeReadfp);
	return;
}


/////////////////////////////////////////////////////////////////////////////
// FixTmpFile:		This function will insert additional info at the bottom	/
//						of the m910nam .xml/.ide file request by the test	/
//						program.											/
//																			/
// Parameters:																/
//		FileToFix:	The file that is to be modified.						/
//		argc:		The # of arguments passed to main.						/
//		argv:		Character list of the arguments passed to main.			/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int FixTmpFile(char *FileToFix, int argc, char *argv[])
{

	int				i, j;
	unsigned int	ReadValue;
	char			*FileContents;
	char			StringToFill[81];
	FILE			*FileToBeReadfp;
	struct _stat	StatBuf;

//
// Check to see if file exists, it better, also need to get file info.
//

	fflush(NULL);

	if ((_stat(FileToFix, &StatBuf)) == GP_ERROR) {
		dodebug(errno, "FixTmpFile()", NULL, (char*)NULL);
		gp_info.return_value = FILE_READ_ERROR;
		return(GP_ERROR);
	}

	if ((FileToBeReadfp = fopen(FileToFix, "rb")) == NULL) {
		dodebug(errno, "FixTmpFile()", NULL, (char*)NULL);
		gp_info.return_value = FILE_OPEN_ERROR;
		return(GP_ERROR);
	}

//
// Allocate and read in the file, checking to make sure all was read.
//

	if ((FileContents = (char *)calloc((size_t)StatBuf.st_size + 2, sizeof(char))) != NULL) {

		if ((ReadValue = fread(FileContents, sizeof(char), (size_t)StatBuf.st_size,
							   FileToBeReadfp)) != (size_t)StatBuf.st_size) {
			free(FileContents);
			dodebug(errno, "FixTmpFile()", NULL, (char*)NULL);
			gp_info.return_value = FILE_READ_ERROR;
			return(GP_ERROR);
		}

		FileContents[(size_t)StatBuf.st_size] = '\0';
		fclose(FileToBeReadfp);
	}
	else {
		dodebug(0, "FixTmpFile()", "Failed to allocated the correct amount of memory", (char*)NULL);
		gp_info.return_value = FILE_READ_ERROR;
		return(GP_ERROR);
	}

//
// Reopen the file for writting which empties the file.
//

	if ((FileToBeReadfp = fopen(FileToFix, "wb")) == NULL) {
		dodebug(errno, "FixTmpFile()", NULL, (char*)NULL);
		gp_info.return_value = FILE_OPEN_ERROR;
		return(GP_ERROR);
	}

//
// Now modifiy the file, first by coping all of the data down to the location of "</V"
// then insert whatever the programmer wants inserted, then copy the last of the remaining
// of the old file.
//

	for (i = 0; i < StatBuf.st_size; i++) {

		if (FileContents[i] == '<' && FileContents[i + 1] == '/' && FileContents[i + 2] == 'V') {

			for (j = ATLAS + 2; j < argc; j++) {
				setArgumentValue (ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR, argv[j], StringToFill, sizeof(StringToFill), NULL);;
				fprintf(FileToBeReadfp, "%s\n", StringToFill);
			}

			fprintf(FileToBeReadfp, "%c", FileContents[i]);
			i++;
		}

		fprintf(FileToBeReadfp, "%c", FileContents[i]);
	}

	fclose(FileToBeReadfp);
	return(0);
}
