// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2020-07-06 16:01:5#$: Date of last commit
//    $Rev:: 27851              $: Revision of last commit

#include "version.h"
#include <stdio.h>
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include <psapi.h>
#include "common.h"
#include "events.h"

EVENTS_INFO		eventsInfo;
PROCESS_INFO	processInfo;

//	CloseHandle(eventsInfo.hMakeMeasurementHandle);
//	CloseHandle(eventsInfo.hSendReturnKeyHandle);

/*
 * dodebug:	This function will format and print an error message to a
 *					file that is pointed to by global debugfp.  If for some
 *					reason that debugfp is NULL this function will try to
 *					reopen the debug file and reset the debugfp to this file.
 *					Else it will error out which should end all calls to this
 *					function, hence no file info.
 *
 * Parameters:
 *		code:			Index into the predefined error messages, if code = 0,
 *						then use the character string that is passed to it.
 *		function_name:	This is the name of the function the error happened.
 *		format:			This is the format for the following variable list that
 *						is sent to be printed;
 *
 * Returns:
 *		none:			Void function
 *
 */
#pragma warning(disable : 4100 4127)
DWORD WINAPI StartEventThread(LPVOID *nothing)
{

	DWORD	eventResponse = WAIT_TIMEOUT;

	sprintf_s(eventsInfo.parentProcessName, sizeof(eventsInfo.parentProcessName), "%s", PARENT_PROCESS);
	sprintf_s(eventsInfo.makeMeasurementName, sizeof(eventsInfo.makeMeasurementName), "%s", PROBEBUTTONEVENT);
	sprintf_s(eventsInfo.sendReturnKeyName, sizeof(eventsInfo.sendReturnKeyName), "%s", SENDKEYEVENT);
	eventsInfo.timeOut = SENDKEYTIMEOUT;

	if ((eventsInfo.hMakeMeasurementHandle = OpenEvent(EVENT_ALL_ACCESS, 0,
													   eventsInfo.makeMeasurementName)) == NULL) {
		retrieveErrorMessage("StartEventThread()", "OpenEvent recieved an error: ");
	}

	if ((eventsInfo.hSendReturnKeyHandle = OpenEvent(EVENT_ALL_ACCESS, 0,
													 eventsInfo.sendReturnKeyName)) == NULL) {
		retrieveErrorMessage("StartEventThread()", "OpenEvent recieved an error: ");
	}

	if (!(ResetEvent(eventsInfo.hMakeMeasurementHandle))) {
		retrieveErrorMessage("StartEventThread()", "Resetting makeMeasurementHandle Recieved an error: ");
	}

	if (!(ResetEvent(eventsInfo.hSendReturnKeyHandle))) {
		retrieveErrorMessage("StartEventThread()", "Resetting sendReturnKeyHandle Recieved an error: ");
	}

	while(1) {

		eventResponse = WaitForSingleObject(eventsInfo.hMakeMeasurementHandle, INFINITE);

		switch (eventResponse) {

			case WAIT_TIMEOUT:
	
				break;

			case WAIT_ABANDONED:

				dodebug(0, "StartEventThread()", "Event didn't happen and WAIT_ABANDONED", (char *)NULL);

				break;

			case WAIT_OBJECT_0:

				if (checkForNotProbeEvent()){
					dodebug(0, "StartEventThread()", "Error happen in checkForNotProbeEvent", (char *)NULL);
				}

				break;

			default:

				retrieveErrorMessage("StartEventThread()", "WaitForSingleObject error: ");
				dodebug(0, "StartEventThread()", "Something very bad in winders expermental land", (char *)NULL);

		}
	}
}
#pragma warning(default : 4100 4127)

/*
 * dodebug:	This function will format and print an error message to a
 *					file that is pointed to by global debugfp.  If for some
 *					reason that debugfp is NULL this function will try to
 *					reopen the debug file and reset the debugfp to this file.
 *					Else it will error out which should end all calls to this
 *					function, hence no file info.
 *
 * Parameters:
 *		code:			Index into the predefined error messages, if code = 0,
 *						then use the character string that is passed to it.
 *		function_name:	This is the name of the function the error happened.
 *		format:			This is the format for the following variable list that
 *						is sent to be printed;
 *
 * Returns:
 *		none:			Void function
 *
 */

int checkForNotProbeEvent(void)
{

	DWORD	eventResponse = WAIT_TIMEOUT;

	eventResponse = WaitForSingleObject(eventsInfo.hSendReturnKeyHandle, eventsInfo.timeOut);

	switch (eventResponse) {

		case WAIT_TIMEOUT:
	
			if (!(ResetEvent(eventsInfo.hMakeMeasurementHandle))) {
				retrieveErrorMessage("StartEventThread()", "Resetting makeMeasurementHandle Recieved an error: ");
			}

			if (!findTheParent(eventsInfo.parentProcessName)) {

				if (findTheChildAndSendKey(processInfo.parentProcessId, RETURN_KEY)) {
					dodebug(0, "checkForNotProbeEvent()", "Error happen in findTheChildAndSendKey", (char *)NULL);
				}
			}
			else {
				dodebug(0, "checkForNotProbeEvent()", "Error happen in findTheParent", (char *)NULL);
			}

			break;

		case WAIT_ABANDONED:

			dodebug(0, "checkForNotProbeEvent()", "Event didn't happen and WAIT_ABANDONED", (char *)NULL);

			break;

		case WAIT_OBJECT_0:

			dodebug(0, "checkForNotProbeEvent()", "Event happend: WaitForProbe sent a signal", (char *) NULL);

			break;

		default:

			retrieveErrorMessage("checkForNotProbeEvent()", "WaitForSingleObject error: ");
			dodebug(0, "checkForNotProbeEvent()", "Something very bad in winders expermental land", (char *)NULL);

	}

	if (!(ResetEvent(eventsInfo.hSendReturnKeyHandle))) {
		retrieveErrorMessage("checkForNotProbeEvent()", "Recieved an error: ");
		return(CEM_ERROR);
	}

	return(SUCCESS);
}

/*
 * dodebug:	This function will format and print an error message to a
 *					file that is pointed to by global debugfp.  If for some
 *					reason that debugfp is NULL this function will try to
 *					reopen the debug file and reset the debugfp to this file.
 *					Else it will error out which should end all calls to this
 *					function, hence no file info.
 *
 * Parameters:
 *		code:			Index into the predefined error messages, if code = 0,
 *						then use the character string that is passed to it.
 *		function_name:	This is the name of the function the error happened.
 *		format:			This is the format for the following variable list that
 *						is sent to be printed;
 *
 * Returns:
 *		none:			Void function
 *
 */

int findTheParent(char *parentProcessName)
{

	unsigned int	i;
	//char			tmpProcessName[CEM_MAX_PATH];
	DWORD			processArray[MAX_PROCESS], processEnum;
	DWORD			bytesReturned = 0;
	HANDLE			processhd = NULL;
	//HMODULE			modulehd;

/*
 * First thing is to get a complete list running processes.
 */

#ifdef JimW
	if (!EnumProcesses(processArray, sizeof(processArray), &bytesReturned)) {
		dodebug(0, "findTheParent()", "Couldn't get a list of runnning process see Sysadmin", (char *)NULL);
		return(CEM_ERROR);
	}
#endif

/*
 * Now calculate how many process identifiers that were returned.
 */

	processEnum = bytesReturned / sizeof(DWORD);

/*
 * Now do the comparision one process at a time.
 */

	for (i = 0; i < processEnum; i++) {

/*
 * Here I will open the process with the TERMINATE flag set.
 */

		if ((processhd = OpenProcess(PROCESS_ALL_ACCESS, FALSE, processArray[i])) != NULL) {

/*
 * Here I will get the Module handle so I can get the process name.
 */

#ifdef JimW
			if (EnumProcessModules(processhd, &modulehd, sizeof(modulehd), &bytesReturned)) {

/*
 * Here I will get the process name for comparasion to make sure the correct process is
 * getting terminated.
 */

				if (GetModuleBaseName(processhd, modulehd, tmpProcessName,
									  sizeof(tmpProcessName))) {

/*
 * Now lets compare the 2 names to make sure that they agree. If they do
 * fill in the concur_info structure.
 */

					if (!_strnicmp(tmpProcessName, parentProcessName, strlen(parentProcessName))) {
						processInfo.parentProcessId = (unsigned long)processArray[i];
						processInfo.parentHandle = processhd;
						break;
					}
				}
			}
#endif
		}

		if (processhd != NULL) {
			CloseHandle(processhd);
			processhd = NULL;
		}
	}

	if (processhd != NULL) {
		CloseHandle(processhd);
	}

	if (i == processEnum) {
		dodebug(0, "findTheParent()", "Didn't find the process %s", parentProcessName, (char *)NULL);
		return(CEM_ERROR);
	}

	return(SUCCESS);
}

/*
 * dodebug:	This function will format and print an error message to a
 *					file that is pointed to by global debugfp.  If for some
 *					reason that debugfp is NULL this function will try to
 *					reopen the debug file and reset the debugfp to this file.
 *					Else it will error out which should end all calls to this
 *					function, hence no file info.
 *
 * Parameters:
 *		code:			Index into the predefined error messages, if code = 0,
 *						then use the character string that is passed to it.
 *		function_name:	This is the name of the function the error happened.
 *		format:			This is the format for the following variable list that
 *						is sent to be printed;
 *
 * Returns:
 *		none:			Void function
 *
 */

int findTheChildAndSendKey(unsigned long parentProcessId, int keyToSend)
{

	HWND	aWindowHandle;
	DWORD	theWindowProcessId;
	DWORD	theWindowsThreadId;

	aWindowHandle = GetTopWindow(0);

	while (aWindowHandle) {

		theWindowsThreadId = GetWindowThreadProcessId(aWindowHandle, &theWindowProcessId);

		if (theWindowProcessId == parentProcessId) {

			if (sendReturnToChild(aWindowHandle, keyToSend)) {
				break;
			}
		}

		aWindowHandle = GetNextWindow(aWindowHandle, GW_HWNDNEXT);
	}

	return(SUCCESS);
}

/*
 *	find_process:		This function will try to find the requested process.  If the 
 *							requested process is running then the concur_info structure
 *							will be filled out with the proper information.
 *							
 *	Parameters:
 *		process_name:	This is the name the function will use in its search through the
 *							presently running process, checking for a match.
 *		
 *	Returns:
 *		SUCCESS:		0    = No errors were encountered.
 *		M9_ERROR:		(-1) = Couldn't do a proper search status of the running process
 *							   is unknown, looks like a Microsoft fix is required REBOOT.
 */

int sendReturnToChild(HWND parentHandle, int keyToSend)
{

	int			strlenReturn = 0;
	int			foundIt = 0;
	HWND		childHandle;
	HWND		upperChildHandle = 0;
	KEYBDINPUT	keyboardInput;
	WINDOWINFO	getWindowInfo;
	INPUT		toSendInput;
	char 		modName[CEM_MAX_PATH];

	getWindowInfo.cbSize = sizeof(WINDOWINFO);
	ZeroMemory(modName, sizeof(modName));
	ZeroMemory(&keyboardInput, sizeof(KEYBDINPUT));
	ZeroMemory(&getWindowInfo, sizeof(getWindowInfo));
	ZeroMemory(&toSendInput, sizeof(INPUT));

	keyboardInput.wVk = (short)keyToSend;
	toSendInput.type = INPUT_KEYBOARD;
	toSendInput.ki = keyboardInput;

	childHandle = GetWindow(parentHandle, GW_CHILD);

	if (GetWindowInfo(childHandle, &getWindowInfo)) {

		if (getWindowInfo.dwStyle & (WS_CHILD | WS_CHILDWINDOW)) {

			upperChildHandle = GetAncestor(childHandle, GA_PARENT);
		}
	}

	if (upperChildHandle != 0) {

		if (IsWindowEnabled(upperChildHandle)) {

			if (IsWindowVisible(upperChildHandle)) {
		
				GetWindowText(upperChildHandle, modName, CEM_MAX_PATH);

				if ((strlenReturn = strlen(modName)) > 2) { 
   
					getWindowInfo.cbSize = sizeof(WINDOWINFO);

					if (GetWindowInfo(upperChildHandle, &getWindowInfo)) {

						if (getWindowInfo.dwStyle & (WS_POPUP | WS_POPUPWINDOW)) {

							SetForegroundWindow(upperChildHandle);

							SetFocus(upperChildHandle);

							SendInput(1, &toSendInput, sizeof(toSendInput));

							foundIt = 1;
						}
					}
				}
			}
		}
	}

	return(foundIt == 0 ? 0 : 1);
}
