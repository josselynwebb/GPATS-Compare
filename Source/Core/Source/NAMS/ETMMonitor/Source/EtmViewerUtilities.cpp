/////////////////////////////////////////////////////////////////////////////
//	File:	EtmViewerUtilities.cpp											/
//																			/
//	Creation Date:	9 June 2012												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Changed the size of KeyData.ItemValue to allow for up to/
//					windows max path size.  Changed the value of			/
//					KeyData[0].ItemValue to c:\\APS\\DATA\\m910nam.xml to	/
//					allow for IADS 3.4.13 to display m910 fault callout file/
//					SetUpIads()												/
//					Changed the if statement to also test for EtmInfo.Mode	/
//					to be the readr, include a debug statement.				/
//					GetIadsVersion()										/
//					Added two debug statements to help in debugging.		/
//					SetUpTheRegistry()										/
//					Had to do major overhaul, the m910 fault callout file	/
//					couldn't be displayed so the rework of this function was/
//					required.												/
//					ExtractApsKeyName()										/
//					Deleted DirBuf, nolonger need to get _getcwd(), using	/
//					the path of the EtmInfo.FileName to get the correct dir	/
//					deleted the call to _getcwd().  Added a second call to	/
//					GetInfo() checking for DIRECTORY2_CMD, / instead of \	/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <sys/types.h>
#include <sys/stat.h>
#include <windows.h>
#include <stdio.h>
#include <direct.h>
#include <tchar.h>
#include "..\..\Common\Include\Constants.h"
#include "EtmMonitor.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local																/
/////////////////////////////////////////////////////////////////////////////

typedef	struct {
	char	ItemName[14];
	char	ItemValue[FILE_DATA];
}KEY_DATA;

KEY_DATA	KeyData[] = {

//  ItemName				ItemValue
{	"DefaultDoc",			"F:\\",						},
{	"LocalDir",				"C:\\Program Files (x86)\\IADS3.4",},
{	"R",					"F:",						 },
{	"TM_INFO_PATH",			"F:\\",						 },
};


/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////


//
//	SetUpIads:	This function will call the correct functions that will
//					allow for the viewer/reader to use the correct version
//					of IADS to be used.  First SetUpIads will call GetIadsVersion
//					to determine which path to the executable to use. Also
//					set the proper paths to the files that IADS need to read, or
//					put the correct information into the registry.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int SetUpIads(void)
{
	//MessageBox(0, "SetUpIads", "" , 0);
	dodebug(0,"SetUpIads()","Entering function in EtmViewerUtilities class", (char*)NULL);

	if (GetIadsVersion()) {
		return(NAM_ERROR);
	}
	
	if ((EtmInfo.IadsVer == BASE3413) || (EtmInfo.IadsVer == BASE3425) && EtmInfo.Mode != ZOOM) {
		dodebug(0, "SetUpIads()", "Calling SetUpRegistry", (char*)NULL);  
		if (SetUpTheRegistry()) {
			return(NAM_ERROR);
		}
	}
	
	dodebug(0,"SetUpIads()","Leaving function in EtmViewerUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	GetIadsVersion:	This function will test for the present of the
//					IADS executable file at the top level of the
//					test program.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int GetIadsVersion(void)
{
	struct _stat	TmpBuf;

	dodebug(0,"GetIadsVersion()","Entering function in EtmViewerUtilities class", (char*)NULL);

	// Determine which iads version we need to use.  
	if (!(_stat(IADS_3_2_7, &TmpBuf))) {
		
		dodebug(0, "GetIadsVersion()", "Found iads_3_2_7.exe", (char*)NULL);  

		sprintf(EtmInfo.ProgramBase, "%s", BASE_3_2_7);
		EtmInfo.IadsVer = BASE327;
	}
	else if (!(_stat(IADS_3_4_13, &TmpBuf))) {
		
		dodebug(0, "GetIadsVersion()", "Found iads_3_4_13.exe", (char*)NULL);  


		sprintf(EtmInfo.ProgramBase, "%s", BASE_3_4_13);
		EtmInfo.IadsVer = BASE3413;
	}
	else if (!(_stat(IADS_3_4_25, &TmpBuf))) {
		
		dodebug(0, "GetIadsVersion()", "Found iads_3_4_25.exe", (char*)NULL); 

		sprintf(EtmInfo.ProgramBase, "%s", BASE_3_4_25);
		EtmInfo.IadsVer = BASE3425;
	}

	else {
		dodebug(0, "WinMain()", "Iads executable is missing at top level", (char*)NULL);
		return(NAM_ERROR);
	}
	
	dodebug(0,"GetIadsVersion()","Leaving function in EtmViewerUtilities class", (char*)NULL);
	return(SUCCESS);
}

//
//	SetUpTheRegistry:	This function will check to see if the APS key is already in the
//							registry, if not then insert it plus the info that goes with
//							APS key.  First try to open the registry key, an error will
//							happen if the key doesn't exist.  Next if it doesn't exist
//							then create it, if it gets created then finally insert the
//							variables that IADS will need to operate for version 3.4.13.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int SetUpTheRegistry(void)
{
	int		returnValue, i;
	char	TheSubKeyName[NAM_MAX_PATH];
	// char	CurrentWorkingDir[NAM_MAX_PATH];  // Not Used
	HKEY	hdOpenedKey;
	
	dodebug(0,"SetUpTheRegistry()","Entering function in EtmViewerUtilities class", (char*)NULL);

	if (ExtractApsKeyName()) {
		return(NAM_ERROR);
	}

	memset(TheSubKeyName, '\0', sizeof(TheSubKeyName));
	sprintf(TheSubKeyName, "%s%s", SUBKEY_CONSTANT, EtmInfo.BaseDirName);
	dodebug(0, "SetUpTheRegistry()", "The SubKey name is: %s", TheSubKeyName, (char*)NULL);

	if ((returnValue = RegOpenKeyEx(HKEY_CURRENT_USER, TheSubKeyName, 0, 
									KEY_READ, &hdOpenedKey)) != ERROR_SUCCESS) {
	
		RegCloseKey(hdOpenedKey);

		if ((returnValue = RegCreateKeyEx(HKEY_CURRENT_USER, TheSubKeyName, 0, 
										  NULL, REG_OPTION_NON_VOLATILE, 
										  KEY_ALL_ACCESS, NULL, &hdOpenedKey, 
										  NULL)) != ERROR_SUCCESS) {

			RegCloseKey(hdOpenedKey);
			dodebug(0, "SetUpTheRegistry()", "Couldn't create key %s", TheSubKeyName, (char*)NULL);
			return(NAM_ERROR);
		}
		else {

			for (i = 0; i < 4; i++) {

				if ((returnValue = RegSetValueEx(hdOpenedKey, KeyData[i].ItemName, 0, 
												 REG_SZ, (LPBYTE)KeyData[i].ItemValue, 
												 (_tcslen(KeyData[i].ItemValue) + 1)
												 * sizeof(TCHAR))) != ERROR_SUCCESS) {

					dodebug(0, "SetUpTheRegistry()", "Couldn't create the item %s or value %s",
							KeyData[i].ItemName, KeyData[i].ItemValue, (char*)NULL);
					break;
				}
			}
		}
	}
	
	RegCloseKey(hdOpenedKey);

	
	dodebug(0,"SetUpTheRegistry()","Leaving function in EtmViewerUtilities class", (char*)NULL);
	return(returnValue == ERROR_SUCCESS ? SUCCESS : NAM_ERROR);
}

//
//	ExtractApsKeyName:	This function will extract the APS name from the
//							current working directory.  It will call the
//							function GetInfo() passing it the current
//							working directory retrieved from _getcwd(),
//							and specifing which elment to look for by setting
//							the second element to DIRECTORY_CMD.
//							
//	Parameters:
//		none:
//		
//	Returns:
//		SUCCESS:		  0  = No errors were encountered.
//		NAM_ERROR:		(-1) = Couldn't perform one of the required tasks.
//

int ExtractApsKeyName(void)
{
	char	*TmpBuf;	
	char	DirBuf[NAM_MAX_PATH];

	memset(DirBuf, '\0', NAM_MAX_PATH);

	dodebug(0,"ExtractApsKeyName()","Entering function in EtmViewerUtilities class", (char*)NULL);

	if ((_getcwd(DirBuf, NAM_MAX_PATH)) == NULL) {
		dodebug(0, "ExtractApsKeyName()", "Failed to get current working directory", (char*)NULL);
		return(NAM_ERROR);
	}
	
	dodebug(0, "ExtractApsKeyName()", "Current Directory is: %s.  This is being passed into GetInfo().", DirBuf, (char*)NULL);
	if ((TmpBuf = GetInfo(DirBuf, DIRECTORY_CMD)) == NULL) {
	
		dodebug(0, "ExtractApsKeyName()", "TmpBuf is: %s.  Result from GetInfo().", TmpBuf, (char*)NULL);

		if ((TmpBuf = GetInfo(DirBuf, DIRECTORY2_CMD)) == NULL) {
			dodebug(0, "ExtractApsKeyName()", "Failed to get the file info, nothing there?",(char*)NULL);
			dodebug(0, "ExtractApsKeyName()", "TmpBuf is: %s.  Result from GetInfo() nested if.", TmpBuf, (char*)NULL);
			return(NAM_ERROR);
		}
	}

	else
	{

		memset(EtmInfo.BaseDirName, '\0', sizeof(EtmInfo.BaseDirName));
		sprintf(EtmInfo.BaseDirName, "%s", TmpBuf);
		EtmInfo.BaseDirName[strlen(TmpBuf)] = '\0';

		free(TmpBuf);
	}
	
	
	dodebug(0,"ExtractApsKeyName()","Leaving function in EtmViewerUtilities class", (char*)NULL);
	return(SUCCESS);
}