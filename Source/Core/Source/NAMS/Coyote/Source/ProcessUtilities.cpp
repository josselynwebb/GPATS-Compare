/////////////////////////////////////////////////////////////////////////////
//	File:	ProcessUtilities.cpp											/
//																			/
//	Creation Date:	12 February 2016										/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0	Assigned it a version number.								/
//		2.0.0.0		Combined Source from Astronics with	/
//					source from VIPERT 1.3.1.0.  							/
//		2.1.0.0		Added hnges from VIPERT 1.3.2.0							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include "version.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <windows.h>
#include <nam.h>
#include "Constants.h"
#include "CoyoteNam.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// StartTheExecutable:	This function will first see if the executable
//						and the xml file are present, no need to go any
//						further if not there.  Then setup the commandline
//						and fire off the process and save the handle to
//						the new process.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int StartTheExecutable(void)
{

	char				CommandLine[NAM_MAX_PATH];
	char				TempFileName[NAM_MAX_PATH];
    STARTUPINFO			StatupInfo;
    PROCESS_INFORMATION	ProcessInfo;
	struct _stat		StatBuf;

	sprintf(TempFileName, "%s", COYOTE_EXECUTE);

	if ((_stat(TempFileName, &StatBuf)) == NAM_ERROR) {
		dodebug(0, "StartTheExecutable()", "The file Coyote.exe missing", (char*)NULL);
		CoyoteInfo.CoyoteErrno = INVALIDEXE;
		return (NAM_ERROR);
	}

	sprintf(TempFileName, "%s", XML_FILE);

	if ((_stat(TempFileName, &StatBuf)) == NAM_ERROR) {
		dodebug(0, "StartTheExecutable()", "The xml file is missing", (char*)NULL);
		CoyoteInfo.CoyoteErrno = INVALIDEXE;
		return (NAM_ERROR);
	}

    ZeroMemory( &StatupInfo, sizeof(StatupInfo) );
    ZeroMemory( &ProcessInfo, sizeof(ProcessInfo) );

    StatupInfo.cb = sizeof(StatupInfo);

	sprintf(CommandLine, "%s", COYOTE_EXECUTE);


	if ((CreateProcess(NULL, CommandLine, NULL, NULL, TRUE, DETACHED_PROCESS,
					   NULL, NULL, &StatupInfo, &ProcessInfo)) == 0) {
		dodebug(0, "StartTheExecutable()", "Failed to start Coyote.exe CommandLine (%s)", CommandLine, (char*)NULL);
		CoyoteInfo.CoyoteErrno = INVALIDEXE;
		return (NAM_ERROR);
	}

	CoyoteInfo.CoyoteHd = ProcessInfo.hProcess;
	CoyoteInfo.ThreadHd = ProcessInfo.hThread;

	return SUCCESS;
}

//
// SendKeyStrokes:	This function will send the key strokes needed
//					to select the correct function and bring the
//					camera interface to the front.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int SendKeyStrokes(void)
{

	int	i, KeyValue;
	int	FirstKeyToSend, SecondKeyToSend;

	int	TheFirstKeys[3][2] = { 
			{VK_MENU,	'F'		},
			{'O',		VK_NOKEY},
			{ 0,		VK_NOKEY} };

	int	TheSecondKeys[16][2] = {
			{VK_TAB,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_SPACE,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_RIGHT,	VK_NOKEY},
			{VK_TAB,	VK_NOKEY},
			{VK_SPACE,	VK_NOKEY},
			{VK_MENU,	VK_SPACE},
			{'N',		VK_NOKEY},
			{ 0,		VK_NOKEY} };

	for (i = 0; TheFirstKeys[i][0] != 0; i++) {

		if (SendRequestedKey(TheFirstKeys[i][0], TheFirstKeys[i][1])) {
			return (NAM_ERROR);
		}

		Sleep(200);
	}

	for (i = 0; CoyoteInfo.FileLocation[i] != '\0'; i++) {

		KeyValue = CoyoteInfo.FileLocation[i];

		if ((KeyValue >= CAP_A_KEY    && KeyValue <= CAP_Z_KEY)    ||
			(KeyValue >= LITTLE_A_KEY && KeyValue <= LITTLE_Z_KEY) ||
			(KeyValue >= ZERO_KEY     && KeyValue <= NINE_KEY)) {

			FirstKeyToSend = KeyValue > CAP_Z_KEY ? (KeyValue - SPACE_KEY) : KeyValue;
			SecondKeyToSend = VK_NOKEY;
		}
		else {

			switch(KeyValue) {

				case SPACE_KEY:
					FirstKeyToSend = VK_SPACE;
					SecondKeyToSend = VK_NOKEY;
					break;

				case BACK_SLASH:
					FirstKeyToSend = VK_OEM_5;
					SecondKeyToSend = VK_NOKEY;
					break;

				case COLON_KEY:
					FirstKeyToSend = VK_SHIFT;
					SecondKeyToSend = VK_OEM_1;
					break;

				case DASH_KEY:
					FirstKeyToSend = VK_OEM_MINUS;
					SecondKeyToSend = VK_NOKEY;
					break;

				case PERIOD_KEY:
					FirstKeyToSend = VK_OEM_PERIOD;
					SecondKeyToSend = VK_NOKEY;
					break;

				case UNDERSCORE_KEY:
					FirstKeyToSend = VK_SHIFT;
					SecondKeyToSend = VK_OEM_MINUS;
					break;

				default:
					dodebug(0, "SendKeyStrokes()", "Sent incorrect character (%d)", CoyoteInfo.FileLocation[i], (char*)NULL);
					CoyoteInfo.CoyoteErrno = INVALIDEXE;
					return (NAM_ERROR);
					break;
			}
		}

		if ((KeyValue >= DASH_KEY     && KeyValue <= COLON_KEY)    ||
			(KeyValue >= CAP_A_KEY    && KeyValue <= CAP_Z_KEY)    ||
			(KeyValue <= LITTLE_A_KEY && KeyValue <= LITTLE_Z_KEY)) {

				KeyValue = KeyValue;
		}
		if (SendRequestedKey(FirstKeyToSend, SecondKeyToSend)) {
			return (NAM_ERROR);
		}
	}

	for (i = 0; TheSecondKeys[i][0] != 0; i++) {

		if (SendRequestedKey(TheSecondKeys[i][0], TheSecondKeys[i][1])) {
			return (NAM_ERROR);
		}

		Sleep(200);
	}

	return SUCCESS;
}

//
// SendRequestedKey:	This function will receive the requested key
//						to be sent and send it checking to see that
//						it was sent. Will also determine if just one
//						or both need to be sent.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int SendRequestedKey(int FirstKey, int SecondKey)
{

	INPUT				ip;

	ip.type			  = INPUT_KEYBOARD;
	ip.ki.wScan		  = 0;
	ip.ki.time		  = 0;
	ip.ki.dwExtraInfo = 0;

	ip.ki.wVk = (unsigned short)FirstKey;
	ip.ki.dwFlags = 0;

	if ((SendInput(1, &ip, sizeof(INPUT))) == 0) {
		dodebug(0, "SendRequestedKey()", "Failed to send First Key %d Key Down", FirstKey, (char*)NULL);
		CoyoteInfo.CoyoteErrno = INVALIDEXE;
		return (NAM_ERROR);
	}

	if (SecondKey != -1) {
		ip.ki.wVk = (unsigned short)SecondKey;
		ip.ki.dwFlags = 0;

		if ((SendInput(1, &ip, sizeof(INPUT))) == 0) {
			dodebug(0, "SendRequestedKey()", "Failed to send Second Key %d Key Down", SecondKey, (char*)NULL);
			CoyoteInfo.CoyoteErrno = INVALIDEXE;
			return (NAM_ERROR);
		}

		ip.ki.wVk = (unsigned short)SecondKey;
		ip.ki.dwFlags = KEYEVENTF_KEYUP;

		if ((SendInput(1, &ip, sizeof(INPUT))) == 0) {
			dodebug(0, "SendRequestedKey()", "Failed to send Second Key %d Key Up", SecondKey, (char*)NULL);
			CoyoteInfo.CoyoteErrno = INVALIDEXE;
			return (NAM_ERROR);
		}
	}

	ip.ki.wVk = (unsigned short)FirstKey;
	ip.ki.dwFlags = KEYEVENTF_KEYUP;

	if ((SendInput(1, &ip, sizeof(INPUT))) == 0) {
		dodebug(0, "SendRequestedKey()", "Failed to send First Key %d Key Up", FirstKey, (char*)NULL);
		CoyoteInfo.CoyoteErrno = INVALIDEXE;
		return (NAM_ERROR);
	}

	return 0;
}
