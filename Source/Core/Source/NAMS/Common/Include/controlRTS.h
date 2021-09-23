#ifndef	_CONTROLRTS_H_INCLUDED_
#define	_CONTROLRTS_H_INCLUDED_

#include <windows.h>
#include <tchar.h>

// windows messages recognized by the RTS application
#define TYX_HALT_MESSAGE		40303
#define TYX_RESET_MESSAGE		40402
#define TYX_UNLOAD_MESSAGE		40102
#define TYX_CLEAR_MEASUREMENT	40408

#define TYX_RUN_MESSAGE			40301
#define TYX_STARTAT_MESSAGE		40302
#define TYX_SKIPDLY_MESSAGE		40403
#define TYX_LOAD_MESSAGE		40101	
#define TYX_MI_MESSAGE			40401
#define TYX_USRCMD1_MESSAGE		40304
#define TYX_USRCMD2_MESSAGE		40305

// post-message macro definition
#define TYX_POST_COMMAND_TO_RTS(msg) {\
	HWND hWnd = ::FindWindow(_T("WRTS"), NULL);\
	if (hWnd)\
		::PostMessage(hWnd, WM_COMMAND, msg, 0);\
	}

// definitions of the macros used in the user's code
#define HALT_RTS				TYX_POST_COMMAND_TO_RTS(TYX_HALT_MESSAGE)
#define RESET_RTS				TYX_POST_COMMAND_TO_RTS(TYX_RESET_MESSAGE)
#define UNLOAD_RTS				TYX_POST_COMMAND_TO_RTS(TYX_UNLOAD_MESSAGE)
#define CLEAR_MEASUREMENT_RTS	TYX_POST_COMMAND_TO_RTS(TYX_CLEAR_MEASUREMENT)

#define RUN_RTS					TYX_POST_COMMAND_TO_RTS(TYX_RUN_MESSAGE)
#define STARTAT_RTS				TYX_POST_COMMAND_TO_RTS(TYX_STARTAT_MESSAGE)
#define SKIPDELAY_RTS			TYX_POST_COMMAND_TO_RTS(TYX_SKIPDLY_MESSAGE)
#define LOAD_RTS				TYX_POST_COMMAND_TO_RTS(TYX_LOAD_MESSAGE)
#define MANUALINTERVENTION_RTS	TYX_POST_COMMAND_TO_RTS(TYX_MI_MESSAGE)
#define USRCMD1_RTS				TYX_POST_COMMAND_TO_RTS(TYX_USRCMD1_MESSAGE)
#define USRCMD2_RTS				TYX_POST_COMMAND_TO_RTS(TYX_USRCMD2_MESSAGE)

#define CLOSE_RTS		{\
	HWND hWnd = ::FindWindow(_T("WRTS"), NULL);\
	if (hWnd)\
		::PostMessage(hWnd, WM_CLOSE, 0, 0);\
	}

#endif	/* _CONTROLRTS_H_INCLUDED_ */