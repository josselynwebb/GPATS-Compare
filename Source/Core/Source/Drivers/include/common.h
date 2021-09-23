#ifndef	CEM_ERROR
	#define	CEM_ERROR		(-1)
#endif

#define	RETURN_KEY			VK_RETURN
#define	SUCCESS				0

#define	LOGLOCATION			"C:\\APS\\DATA\\"
#define	DEBUGFILENAME		"CEMDeBug.txt"
#define DEBUGIT				"deBugCEM"

#define PROBEBUTTONEVENT	"ProbeButtonPressed"
#define	SENDKEYEVENT		"SendReturnKey"

#define	MAX_PROCESS			1024
#define	MAX_MSG_SIZE		128

#define	CEM_MAX_PATH		 _MAX_PATH

#define	ALLOC				1
#define	UNALLOC				0
#define	DOIT				1
#define	BEENDONE			1
#define	NOTDONE				0

#define	READBUFSIZE			512

typedef	struct{
	int			errno1;
	int			timeOut;
	char		logLocation[CEM_MAX_PATH];
	char		debugOption[CEM_MAX_PATH];
}CEM_INFO;

typedef	struct{
	int			errno1;
	int			timeOut;
	char		parentProcessName[CEM_MAX_PATH];
	char		makeMeasurementName[CEM_MAX_PATH];
	char		sendReturnKeyName[CEM_MAX_PATH];
	char		logLocation[CEM_MAX_PATH];
	char		debugOption[CEM_MAX_PATH];
	HANDLE		hMakeMeasurementHandle;
	HANDLE		hSendReturnKeyHandle;
}PROBE_INFO;

extern PROBE_INFO		probeInfo;
extern CEM_INFO			cemInfo;
extern int				DE_BUG;
extern FILE				*debugfp;

extern	void			retrieveErrorMessage(char*, char*);
extern	void			dodebug(int, char*, char*, ...);
extern	int				allocInstrumentsReadBuffers();
extern	int				allocateArbGenReadBuffer(int);
extern	int				allocateCounterTimerReadBuffer(int);
extern	int				allocateDcPwrSupplyReadBuffer(int);
extern	int				allocateDigScoprReadBuffer(int);
extern	int				allocateDMMReadBuffer(int);
extern	int				allocateElecOptReadBuffer(int);
extern	int				allocateFuncGenReadBuffer(int);
extern	int				allocateRFGenReadBuffer(int);
extern	int				allocateRFMeasAnReadBuffer(int);
extern	int				allocatePwrMeterReadBuffer(int);

