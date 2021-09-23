#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <ctype.h>
#include <malloc.h>
#include <sys/types.h>
#include <sys/stat.h>
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
// SearchForAPSName:	This function will search through a file that		/
//					contains a comma seperated semi-colon terminated lines	/
//					of APS Names, TP Names, and Part Numbers of all know	/
//					APSs that were either completed or that had a contract	/
//					let on it. This function will try to match the TP Name	/
//					and Part Number that was passed for a comparison on the	/
//					second and third element of a line. If a match is found	/
//					then the first element will be used for the APS Name.	/
//					No match then the APS Name is '\0' NULL.				/
//																			/
// Parameters:																/
//		TPSName:	This is the TP Name.									/
//		PartNumber:	This is the Part Number.								/
//																			/
// Returns:																	/
//		FoundAPSName:	This is the APS Name where a match was found for	/
//						the TP Name and Part Number.						/
//		NULL:			There was no match found.							/
//																			/
/////////////////////////////////////////////////////////////////////////////

 char	*SearchForAPSName(char *TPSName, char *PartNumber)
 {

	int				i, j, HadError = 0;
	unsigned int	ReadSize;
	char			*FileContents = NULL, *sp;
	char			ApsInfoFile[GP_MAX_PATH];
	char			TmpBuf[APS_NAME_SIZE + TPS_NAME_SIZE + PART_NUM_SIZE + 2]; 
	char			TpsAndPartNum[TPS_NAME_SIZE + PART_NUM_SIZE + 2];
	char			TpsNameOnly[TPS_NAME_SIZE + 1];
	static char		FoundAPSName[APS_NAME_SIZE];
	FILE			*fp;
	struct _stat	fz;

//
// First we will build the string that we will use later on to check against.
// This will be the TP name and Part number with a comma.
//

	memset(TmpBuf, '\0', sizeof(TmpBuf));
	memset(TpsAndPartNum, '\0', sizeof(TpsAndPartNum));
	memset(TpsNameOnly, '\0', sizeof(TpsNameOnly));
	memset(FoundAPSName, '\0', sizeof(FoundAPSName));
	memset(ApsInfoFile, '\0', sizeof(ApsInfoFile));
	sprintf(TpsAndPartNum, ",%s,%s", TPSName, PartNumber);
	sprintf(TpsNameOnly, ",%s,", TPSName);

//
// Now open the comma seperated file that has, Hopefully, the APS Name TP Name
// and Part Number in it in the binary mode, Microsoft screws this part up. I will
// then get the size of the file then allocate memory for that size for a buffer,
// so I can read it into memory.
//

	sprintf(ApsInfoFile, "%s%s", FAULT_FILE_EXE_DIR, APSNAMEFILE);

	if ((fp = fopen(ApsInfoFile, "rb")) == NULL) {

		DoDebug(0, "SearchForAPSName()", "%s%s%s", "fopen failed to open file (",
					ApsInfoFile, ") check perms", (char*)NULL);
		return(NULL);
	}

	if ((_stat(ApsInfoFile, &fz)) != GP_ERROR) {
		if ((FileContents = (char *)malloc((size_t)fz.st_size)) != NULL) {
			if ((ReadSize = fread(FileContents, sizeof(char), (size_t)fz.st_size, fp)) != (size_t)fz.st_size) {
				if (FileContents != NULL) {
					free(FileContents);
				}

				DoDebug(errno, "SearchForAPSName()", (char*)NULL);
				HadError++;
			}
		}
		else {
			DoDebug(errno, "SearchForAPSName()", (char*)NULL);
			HadError++;
		}
	}
	else {
		DoDebug(errno, "SearchForAPSName()", (char*)NULL);
		HadError++;
	}

	if (fp != NULL) {
		fclose(fp);
	}

	if (HadError) {
		return(NULL);
	}

//
// Now I will parse through the file in the reverse mode looking for
// a comma.  I will then send the TmpBuf to FindStringRight to see if the TP
// Name and Part Number are in the string, ignoring the case. If the
// TP Name and Part Number are in the string then return a pointer to
// the start of the match + 1, which should include a comma.  I will
// then put the NULL terminator there.  Then I look for the first 
// Alpha numeric character in the string, this sould get rid of the 
// semi-colon, which should be just the APS Name, which I return to the
// calling process.
//

	for (i = (fz.st_size - (strlen(TpsAndPartNum) - 1)); i > 0; i--) {

		if (FileContents[i] == ';'|| i == 1) {

			if (i == 1) {
				i = 0;
			}

			memset(TmpBuf, '\0', sizeof(TmpBuf));

			sprintf(TmpBuf, "%s", &FileContents[i]);

			if ((sp = FindStringRight(TpsAndPartNum, TmpBuf, strlen(TmpBuf) - 1, DISREGARD)) != NULL ||
				(sp = FindStringRight(TpsNameOnly, TmpBuf, strlen(TmpBuf) - 1, DISREGARD)) != NULL) {

				TmpBuf[(sp - TmpBuf)] = '\0';

				for (j =0; TmpBuf[j] != '\0'; j++) {

					if (isalnum(TmpBuf[j])) {
						sprintf(FoundAPSName, "%s", &TmpBuf[j]);

						if (FileContents != NULL) {
							free(FileContents);
						}
						
						return(FoundAPSName);
					}
				}
			}
			else {
				FileContents[i] = '\0';
			}
		}
	}

	if (FileContents != NULL) {
		free(FileContents);
	}

	return(NULL);
}