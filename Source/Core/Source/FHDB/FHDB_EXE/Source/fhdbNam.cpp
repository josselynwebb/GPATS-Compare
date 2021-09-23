/////////////////////////////////////////////////////////////////////////////
//	File:	fhdbNam.cpp														/
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

#include <sys/types.h>
#include <sys/stat.h>
#include <windows.h>
#include <stdio.h>
#include <wchar.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
#include <objbase.h>
#include "fhdb.h"
#include "nam.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

int				DE_BUG;
FILE			*debugfp;
FHDB_INFO		fhdbInfo;
DATACOLLECTION	dataCollectionfp;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
//	WinMain:	This is the main entry point for this program.  This function
//					has been incorperated into an already existing program so
//					that a console window will not appear.
//
//	Parameters:
//		hInstance:		A Microsoft object handle for this program NOT USED.
//		hPrevInstance:	A Microsoft object handle NOT USED.
//		lpCmdLine:		Pointer to a null-terminated string specifying the
//						command line for the application, excluding the
//						program name.
//		nCmdShow:		Specifies how the window is to be shown NOT USED.
//
//	Returns:
//		SUCCESS:		  0  - No errors
//		NAM_ERROR:		(-1) - Error occured
//
//
#pragma warning(disable : 4100)
int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
					 LPSTR lpCmdLine, int nCmdShow)
{

	int				nargs, i, convertedChars, hadError = 0;
	WCHAR			**cmdLineList;
	char			**argv;
	char			convertedCmdLineArg[NAM_MAX_PATH];
	struct _stat	tmpBuf;
		
// Setup some defaults, that are depended upon to be set to the proper values


	initVariables();
	CoInitialize(NULL);
	
//
// Find out if the operator wants to have debug info.  If it is to provide
// debug info then the file debugit_fhdb should be there.  If it is there, then try
// to open debug file and set DE_BUG to TRUE, if file can not be opened then set DE_BUG
// to FALSE.
//

	DE_BUG = _stat(fhdbInfo.debugOption, &tmpBuf) == NAM_ERROR ? FALSE : TRUE;

	if (DE_BUG) {

		char	debugfile[NAM_MAX_PATH];

		sprintf(debugfile, "%s%s", fhdbInfo.logLocation, DEBUGFILENAME);

		if ((debugfp = fopen(debugfile, "w+b")) == NULL) {
			DE_BUG = FALSE;
		}
	
	}

// Get the command line arguments

	if ((cmdLineList = CommandLineToArgvW(GetCommandLineW(),
											&nargs)) == NULL) {
		dodebug(0, "WinMain()", "%s", "Failed to get command line argments");

		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = FALSE;
		}
	}

//
// If we make it this far then we know the number of characters strings that
// will be in the argv list that we are going to build.  So now we will malloc
// the space for them.
//

	else if ((argv = (char **)malloc(sizeof(char*) * nargs)) == NULL) {
		dodebug(NO_PRE_DEFINE, "WinMain()", "%s",
				"Malloc failed to allocate the required memory");

		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = FALSE;
		}

		if (cmdLineList != NULL) {
			GlobalFree(cmdLineList);
		}
	}

//
// Now we will loop through the number of arguments and convert them from gates
// wide characters to ansi characters.  Then use the _strdup function to
// allocate the required string length for each argument and put them into
// *argv[] so I can use the already waiting program without any changes.
//

	else {
		for (i = 0; i < nargs; i++) {

			if ((convertedChars = WideCharToMultiByte(CP_UTF8, 0,
												cmdLineList[i], -1,
												convertedCmdLineArg,
												sizeof(convertedCmdLineArg),
												NULL, NULL)) == SUCCESS) {
				dodebug(NO_PRE_DEFINE, "WinMain()", "%s",
						"Failed to convert from wide characters to ansi");
				hadError++;
				break;
			}

			if ((argv[i] = _strdup(convertedCmdLineArg)) == NULL) {

				int	j;

				for (j = 0; j < i; j++) {
					freeItem(argv[j]);
				}

				dodebug(NO_PRE_DEFINE, "WinMain()", "%s",
						"strdup failed to allocate the required memory");
				hadError++;
				break;
			}

		}	

		if (fhdbNamMain(nargs, argv)) {
			hadError++;
		}

		if (argv != NULL) {
			free(argv);
		}

		if (cmdLineList != NULL) {
			GlobalFree(cmdLineList);
		}

		if (DE_BUG) {
			fclose(debugfp);
			DE_BUG = FALSE;
		}

	}

	try
	{
		vmClose();
	}
	// catch (int exception)
	catch (...)
	{
		// Just catch all exceptions here
	}

	return(fhdbInfo.returnValue != SUCCESS ? NAM_ERROR : SUCCESS);
}
#pragma warning(default : 4100)

//
//	initVariables:		This function performs the initilaization of required
//						variables that need to be equated to something legal.
//
//	Parameters:
//		none:
//
//	Returns:
//		none:
//
//

void initVariables(void)
{

	ZeroMemory(&fhdbInfo, sizeof(FHDB_INFO));
	ZeroMemory(&dataCollectionfp, sizeof(DATACOLLECTION));

	sprintf(fhdbInfo.logLocation, "%s", LOGLOCATION);
	sprintf(fhdbInfo.debugOption, "%s%s", fhdbInfo.logLocation, DEBUGIT);
	fhdbInfo.returnValue = SUCCESS;
}


//
//	freeItem:		This function will check the variable passed to it to see if it
//						is empty or not.  If the variable is not empty then the
//						function will free the memory that was allocated for it.
//							
//	Parameters:
//		item:		The variable that needs to have the memory freed that was
//					allocated for it.
//		
//	Returns:
//		none:
//
//

void freeItem(char *item)
{

	if (item != NULL) {
		free(item);
	}
}
