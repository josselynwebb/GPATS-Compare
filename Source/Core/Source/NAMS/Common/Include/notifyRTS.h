#ifndef	_NOTIFYRTS_H_INCLUDED_
#define	_NOTIFYRTS_H_INCLUDED_

#include <TCHAR.h>
#include <stdio.h>
#import "..\com\PublishersIO.exe" no_namespace

#define TRY_COM try {

// all the following 10 macros have to be used inside of a TRY_COM CATCH_COM block
#define INITIALIZE_DISPLAY			IPublisherPtr _spPublisherDisplay(__uuidof(Display));
#define INITIALIZE_OUTPUT			IPublisherPtr _spPublisherOutput(__uuidof(Output));
#define INITIALIZE_WARNING			IPublisherPtr _spPublisherWarning(__uuidof(Warning));
#define INITIALIZE_ERROR			IPublisherPtr _spPublisherError(__uuidof(Error));
#define INITIALIZE_INFO				IPublisherPtr _spPublisherInfo(__uuidof(Info));

#define DISPLAY_TO_RTS(msg)			_spPublisherDisplay->Display(msg);
#define OUTPUT_TO_RTS(msg)			_spPublisherOutput->Display(msg);
#define WARNING_TO_RTS(msg)			_spPublisherWarning->Display(msg);
#define ERROR_TO_RTS(msg)			_spPublisherError->Display(msg);
#define INFO_TO_RTS(msg)			_spPublisherInfo->Display(msg);

#define CATCH_COM } \
	catch (const _com_error& e)\
	{\
		TCHAR message[1024];\
		if (e.Description().length())\
			_stprintf(message, _T("COM error, %s"), (LPCTSTR)e.Description());\
		else\
			_stprintf(message, _T("COM error, 0x%08X, %s"), e.Error(), e.ErrorMessage());\
		::MessageBox(NULL, message, _T("COM error"), MB_OK);\
	}

#endif	/* _NOTIFYRTS_H_INCLUDED_ */