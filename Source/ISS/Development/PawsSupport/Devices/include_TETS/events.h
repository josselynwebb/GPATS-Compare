#define	PARENT_PROCESS		"wrts.exe"

typedef	struct{
//	int			errno;
	int			timeOut;
	char		parentProcessName[CEM_MAX_PATH];
	char		makeMeasurementName[CEM_MAX_PATH];
	char		sendReturnKeyName[CEM_MAX_PATH];
	char		logLocation[CEM_MAX_PATH];
	char		debugOption[CEM_MAX_PATH];
	HANDLE		hMakeMeasurementHandle;
	HANDLE		hSendReturnKeyHandle;
}EVENTS_INFO;

#define	MAX_PROCESS			1024
#define	SENDKEYTIMEOUT		500

typedef	struct{
	unsigned long	parentProcessId;
	HANDLE			parentHandle;
	char			parentProcessName[80];
}PROCESS_INFO;

extern PROCESS_INFO	processInfo;


extern EVENTS_INFO	eventsInfo;

extern	int				checkForNotProbeEvent(void);
extern	int				findTheParent(char*);
extern	int				findTheChildProcess(unsigned long);
extern	int				findTheParent(char*);
extern	BOOL CALLBACK 	EnumChildProc(HWND, LPARAM);
extern	int				findTheChildAndSendKey(unsigned long, int);
extern	int				sendReturnToChild(HWND, int);