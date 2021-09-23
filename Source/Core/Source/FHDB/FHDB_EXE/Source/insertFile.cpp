/////////////////////////////////////////////////////////////////////////////
//	File:	insertFile.cpp													/
//																			/
//	Creation Date:	30 June 2008											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of fhdb nam, visual dll software no	/
//					longer available.  Include the dll code into the nam	/
//					program.												/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////	

#include <io.h>
#include <fcntl.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <ctype.h>
#include <malloc.h>
#include <errno.h>
#include "fhdb.h"

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
// dtbFaultCallout:		This program will extract the digital info from
//							the M910NAM.dia file and remove non useful info.
//
// Parameters:
//		teradyneDiaFile		This is the file that has the teradyne fault
//							info, either fault dictionary or guided probe.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		OSRSERR:	(-1)	= failure of a required task.
//

int dtbFaultCallout(char *teradyneDiaFile)
{

	int				i, GuidedDetected, GuidedDetectPoint = FALSE, leadingWhiteSpaces;
	int				dtbfd, snprintfReturn = 0;
	unsigned int	readValue;
	char			dtbFileBuf[NAM_MAX_PATH];
	char			faultCalloutTmpBuf[FAULT_CALLOUT_SIZE + TERMINATOR];
	char			*dtbFileContents;
	struct _stat	dtbfz;

	// Make sure that all files are written and the info is there before
	// this programs opens the files for reading and writing.
	fflush(NULL);

	// Set up the buffers with the proper file names.
	sprintf(dtbFileBuf, "%s", teradyneDiaFile);

	// Now open the fault file that is going to have stuff put in to it, and
	// the dtb file from where the stuff is coming from.
	leadingWhiteSpaces = FALSE;

	if ((_stat(dtbFileBuf, &dtbfz)) == NAM_ERROR) 
	{
		dodebug(errno, "dtbFaultCallout()", NULL, NULL);
		dodebug(NO_PRE_DEFINE, "dtbFaultCallout()", "File name is %s", dtbFileBuf, (char*)NULL);
		fhdbInfo.returnValue = FILEREAD;
		return(NAM_ERROR);
	}

	if ((dtbfd = _open(dtbFileBuf, _O_RDONLY | _O_BINARY)) == NAM_ERROR) 
	{
		dodebug(errno, "dtbFaultCallout()", NULL, NULL);
		fhdbInfo.returnValue = FILEOPEN;
		return(NAM_ERROR);
	}

	// Now get the size of the dtb file and read it into the dtbFileBuf, where we will
	// try to figure out if it is either a fault dictionary file or a guided probe
	// file.  This shouldn't be to hard, but teradyne likes to change its output file
	// format everytime it comes out with an update to their software so it will be fun.
	if ((dtbFileContents = (char*)malloc((size_t)dtbfz.st_size)) != NULL) 
	{
		if ((readValue = _read(dtbfd, dtbFileContents, (size_t)dtbfz.st_size))
															 != (size_t)dtbfz.st_size) 
		{
			free(dtbFileContents);
			dodebug(errno, "dtbFaultCallout()", NULL, NULL);
			fhdbInfo.returnValue = FILEREAD;
			return(NAM_ERROR);
		}
		dtbFileContents[(size_t)dtbfz.st_size] = '\0';
	}
	else 
	{
		dodebug(errno, "dtbFaultCallout()", NULL, NULL);
		fhdbInfo.returnValue = FILEREAD;
		return(NAM_ERROR);
	}
		
	_close(dtbfd);

	// Seeing that we have gotten this far we will now read the .dia file and see if there
	// was any guided probing done.  If so then set the variable Guided_detected.
	GuidedDetected = FALSE;

	for (i = 0; (size_t)i < (size_t)dtbfz.st_size; i++) 
	{
		if (dtbFileContents[i] == 'G') 
		{
			if (!strncmp(&dtbFileContents[i], "Guided", 6)) 
			{
				GuidedDetected++;
				GuidedDetectPoint = i;
				break;
			}
		}
	}

	// Seeing that I like doing the easy stuff first, we will do the guided probe stuffing.
	// We will start the stuffing when we find the string starting with the below, this is what
	// we want to put into the FAULT-FILE.  Also we will make the FAULT-FILE pretty by
	// removing any extra spaces and *.  We will also wrap at the 80th character.
	if (GuidedDetected) 
	{
		int k, notFirstTime;

		notFirstTime = FALSE;

		for (i = GuidedDetectPoint; (size_t)i < (size_t)dtbfz.st_size; i++) 
		{
			if (dtbFileContents[i] == 'D' || dtbFileContents[i] == 'O' || dtbFileContents[i] == 'T' || dtbFileContents[i] == 'P') 
			{

				if (!strncmp(&dtbFileContents[i], "DIAGNOSED FA", strlen("DIAGNOSED FA")) ||
					!strncmp(&dtbFileContents[i], "OPEN detecte", strlen("OPEN detecte")) ||
					!strncmp(&dtbFileContents[i], "Test is Unst", strlen("Test is Unst")) ||
					!strncmp(&dtbFileContents[i], "Possible Fee", strlen("Possible Fee"))) {

					for (k = 0; dtbFileContents[i] != '\0'; i++, k++) 
					{
						if (dtbFileContents[i] == ' ' && dtbFileContents[i - 1] == '*') 
						{
							i++;
						}

						if (dtbFileContents[i] == ' ' && dtbFileContents[i - 1] == '\n') 
						{
							i++;
							k--;
						}

						if (dtbFileContents[i] == '*') 
						{
							i++;
							k--;
						}
						else 
						{
							if (dtbFileContents[i] != '\r') 
							{
								faultCalloutTmpBuf[k] = dtbFileContents[i];
							}
							else 
							{
								k--;
							}
						}
					}
				}
			}
		}
	}

	// Now for the hard part, this must be fault dictionary info.  We start at the string
	// that begins with "One of the following", or "Fault:".  Also we will remove the info
	// starting with the string that starts with Det until we have either the end of the
	// file or back with the string starting with "One of the following" or "Fault:".  We
	// will also make it look pretty by removing extra junk at the begining of some lines,
	// and at the ends of other lines.
	else 
	{
		int		j, k;
		char	firstLine[] = "Fault Dictionary Diagnosis:\n\n";

		for (i = 10; dtbFileContents[i] != '\0'; i++) 
		{
			if (dtbFileContents[i] == 'O') 
			{
				if (!strncmp(&dtbFileContents[i], "One of the following fault", strlen("One of the following fault"))) 
				{
					break;
				}
			}
			else if (dtbFileContents[i] == 'F') 
			{
				if (!strncmp(&dtbFileContents[i], "Fault:", strlen("Fault:"))) 
				{
					break;
				}
			}
		}

		// Now i will put in a standard default line.
		for (j = 0, k = 0; (size_t)j < strlen(firstLine); j++, k++) 
		{
			faultCalloutTmpBuf[k] = firstLine[j];
		}

		for (; dtbFileContents[i] != '\0'; i++, k++) 
		{
			if (dtbFileContents[i] == ' ' && dtbFileContents[i - 1] == '*') 
			{
				i++;
			}

			if (dtbFileContents[i] == '\n' && dtbFileContents[i + 1] == ' ') 
			{
				checkFor(NEWLINESPACE, &i, &k, dtbFileContents, faultCalloutTmpBuf);
			}

			if (dtbFileContents[i] == ' ' && dtbFileContents[i + 1] == ' ') 
			{
				checkFor(NINESPACES, &i, &k, dtbFileContents, faultCalloutTmpBuf);
			}

			if (dtbFileContents[i] == ' ') 
			{
				checkFor(DETECTPOINTS, &i, &k, dtbFileContents, faultCalloutTmpBuf);
			}

			if (dtbFileContents[i] == ' ') 
			{
				checkFor(FOURSPACES, &i, &k, dtbFileContents, faultCalloutTmpBuf);
			}

			if (dtbFileContents[i] == ' ' && dtbFileContents[i + 1] == ' ' &&  dtbFileContents[i - 1] == '\n') 
			{
				i += 2;
				k--;
			}

			if (dtbFileContents[i] == ' '       && dtbFileContents[i + 1] == ' ' &&	dtbFileContents[i + 2] == '\r'  && dtbFileContents[i + 3] == '\n') 
			{
				i += 2;
				k--;
			}

			else 
			{
				if (dtbFileContents[i] != '\0' && dtbFileContents[i] != '\r') 
				{
					faultCalloutTmpBuf[k] = dtbFileContents[i];
				}
				
				if (dtbFileContents[i] == '\r') 
				{
					k--;
				}
			}
		}
	}

	if ((snprintfReturn = _snprintf(dataCollectionfp.faultCallout, FAULT_CALLOUT_SIZE, dtbFileContents)) < SUCCESS)
	{
		dodebug(NO_PRE_DEFINE, "dtbFaultCallout()", "Setting of the Fault Callout failed", (char*)NULL);
	}

	dataCollectionfp.faultCallout[(strlen(dtbFileContents)) > FAULT_CALLOUT_SIZE ? FAULT_CALLOUT_SIZE : strlen(dtbFileContents)] = '\0';
	//free(dtbFileContents);
	//the above free causes crash 08/14/2017
	return(SUCCESS);
}
