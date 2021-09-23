/////////////////////////////////////////////////////////////////////////////
//	File:	etmnam.h														/
//																			/
//	Creation Date:	10 Mar 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0	Complete rebuild of etmnam to implement Iads ver 3.2		/
//				software.													/
// 		2.0.1.0	Removed log_file and process elements from the etm_info		/
// 				structure, they weren't being used.							/
//		2.0.2.0	Removed the #define PROG_BIN_DIR "c:/usr/tyx/bin", use the	/
//				environmental variable path for correct path.				/
//																			/
/////////////////////////////////////////////////////////////////////////////

#define	MAX_STRING 1024

#ifndef	NAM_ERROR
	#define	NAM_ERROR (-1)
#endif

#ifndef	TRUE
	#define	TRUE 1
#endif

#ifndef	FALSE
	#define	FALSE 0
#endif

#define	NONE_TYPE		0
#define	DIGIT_TYPE		1
#define	STRING_TYPE		2

#define	NO				0
#define	YES				1

#define	NULL_IT			1
#define	END_IT			2

#define	KEEP			0
#define	DISREGARD		1

#define	SUCCESS			0
#define	REQUIRED		1
#define	NO_INFO			1

#define	ZOOM			1
#define	TARGET			3
#define	TARGETZOOM		2
#define	READ_ONLY		1
#define	READ_TARGET		2
#define	ZOOM_VIEW		3

#define	ROOTDIR			1
#define	DRIVEDIR		2
#define	RELATIVEPATH	3
#define	INPATH			4
#define	FIXRELATIVE		5

#define	ARGNUM				801
#define	DATACHAR			802
#define	DATAINT				803
#define	UNKOPT				804
#define	XYUSAGE				805
#define	FILEOPEN			806
#define	TARGOPT				807
#define	CONCURRENTOPT		808
#define	ZOOMOPT				809
#define	CONSUC				810
#define	CONCURRENT_ERROR	811
#define	CHILD_RUN_WILD		812
#define	PIPE_HANDLE_ERROR	813
#define	WRITE_MSG_ERROR		814
#define	READ_MSG_ERROR		815
#define	PROCESS_WONT_DIE	816
#define	PROCESS_DONT_AGREE	817


#define	OFFSET		0
#define	LENGTH		1

#define	TMP_FILE	1
#define	OPTION		2

#define	TERM		3
#define	FILENAME	1
#define	TERMINATE	4
#define	ARGC_MIN	3
#define	ARGC_MAX	19

#define	BYE_CMD				0
#define	FILE_CMD			1
#define	NONE_CMD			2
#define	QUIT_CMD			3
#define	READ_CMD			4
#define	ZOOM_CMD			5
#define	READER_CMD			6
#define	CONCURRENT_CMD		7
#define	STATUS_CMD			8
#define	TARGET_CMD			9
#define	WINDOW_CMD			10
#define	OPTIONS_CMD			11
#define	SUCCESS_CMD			12
#define	ILLEGAL_CMD			13
#define	CONSECUTIVE_CMD		14
#define	REFERENCE_CMD		15
#define	FUNCTIONAL_CMD		16
#define	OPERATIONAL_CMD		17
#define	COMMAND_NUM			17

#define	BYE_MSG				"Bye"
#define	FILE_MSG			"File Name"
#define	NONE_MSG			"None"
#define	QUIT_MSG			"Quit"
#define	READ_MSG			"Reader"
#define	ZOOM_MSG			"Zoom"
#define	READER_MSG			"Reader"
#define	CONCURRENT_MSG		"Concurrent"
#define	STATUS_MSG			"Status"
#define	TARGET_MSG			"Target"
#define	WINDOW_MSG			"Window Info"
#define	OPTIONS_MSG			"Image Options"
#define	SUCCESS_MSG			"Success"
#define	ILLEGAL_MSG			"Illegal command"
#define	CONSECUTIVE_MSG		"Consecutive"
#define	REFERENCE_MSG		"Reference"
#define	FUNCTIONAL_MSG		"Functional Mode"
#define	OPERATIONAL_MSG		"Operational Mode"

#define	END_MSG				"End"
#define	WINDOW_END			"End of Window"
#define	OPTION_END			"End of Options"
#define	END_FILE			"End of File"
#define	STATUS_END			"End of Status"
#define	REFERENCE_END		"End of Reference"

//#define	LOGLOCATION			"C:/APS/DATA/"
#define	DEBUGFILENAME		"etmdebug.txt"
#define DEBUGIT				"debugit_etm"
#define	SUFFIX				".TMP"

#define	PROG_BIN_DIR		"c:/usr/tyx/bin/"
#define	ETM_CONTROLLER		"etm_monitor.exe"
#define	PIPE_FILE			"etm_pipe_file"
//#define	PIPE_FILE_HEADER	"\\\\.\\pipe\\"


#define	NAM_MAX_PATH		_MAX_PATH
#define	MAX_MSG_SIZE 		1024
#define	MAX_PROCESS			1024

extern	int		DE_BUG;
extern	FILE	*debugfp;

typedef	struct {
	int		ReturnValue;
	int		FileExist;
	int		Mode;
	int		Concurrent;
	int		NotAtlas;
	char	FrameTarget[NAM_MAX_PATH];
	char	FileLocation[NAM_MAX_PATH];
	char	LogLocation[NAM_MAX_PATH];
	char	ProcessName[NAM_MAX_PATH];
	char	DebugOption[NAM_MAX_PATH];
	char	TermType[MAX_MSG_SIZE];
	DWORD	ProcessId;
	int		ProcessRunning;
	int		ProcessTask;
}ETM_INFO;

typedef	struct {
	int		numOfVals;
	union {
		int		*intValue;
		char	**charValue;
	}u;
	char	*cmdToSend;
	char	*msgType;
	char	*endString;
	int		formatType;
}SEND_INFO;

typedef	struct {
	int		winNum;
	int		win_x1;
	int		win_y1;
	int		win_x2;
	int		win_y2;
	int		offsetNum;
	int		pic_x1;
	int		pic_y1;
	int		pic_x2;
	int		pic_y2;
	char	predefine[4];
}WIN_INFO;

typedef struct {
	int				MessageSize;
	int				MessageValue;
	int				ReadReturn;
	unsigned long	ReadMsgSize;
	int				WriteReturn;
	unsigned long	WriteMsgSize;
	char			WriteMsg[MAX_MSG_SIZE];
	char			ReadMsg[MAX_MSG_SIZE];
	char			PipeFile[NAM_MAX_PATH];
	HANDLE			Pipehd;
}PIPE_INFO;

extern	PIPE_INFO	PipeInfo;
extern	ETM_INFO 	EtmInfo;
extern	WIN_INFO 	WinInfo;
extern	SEND_INFO	SendInfo[];

extern	void	CleanUp(void);
extern	void	ConvertChar(char*, char*, char, char, int);
extern	void	CorrectTargetFrame(void);
extern	int		DoReadParse(int, char**);
extern	int		DoTargetParse(int, char**);
extern	int		DoZoomParse(int, char**);
extern	void	dodebug(int, char*, char*, ...);
extern	int		EtmnamMain(int, char**);
extern	int		FilePath(int, int, int);
extern	int		FilePresent(char*, int);
extern	int		FileStyle(char*, char*);
extern	int		FillInInfo(int);
extern	int		FindProcess(void);
extern	int		FormatSendMessage(int);
extern	void	FreeItem(char*);
extern	int		GetPipeHd(void);
extern	int		GetResponse(void);
extern	int		GetValue(int, int*, int, char**, int*);
extern	void	InitVariables(void);
extern	int 	RequestResponse(char*, char*, char*);
extern	void	MessageUtility(int, char*, char*);
extern  int		ParseArguments(int, char**, int);
extern  int		ParseArgumentsAtlas(int, char**, int);
extern	int		ParseCommandAtlas(int, char**);
extern	int		ParseCommandLine(int, char**);
extern	void	PerformReader(void);
extern	void	PerformTarget(void);
extern	int		PerformTerminate(char*);
extern	void	PerformZoom(void);
extern	int		ReadMessage(void);
extern	int		SendMessage(char*);
extern	void	SendReaderInfo(void);
extern	void	SendTargetInfo(void);
extern	int		SendTerminate(char*);
extern	void	SendZoomInfo(void);
extern	void	SetupWriteMsg(char*, char*);
extern	int		StartProc(void);
extern	int		StartUpTheProcess(void);
extern	int		StartupStatus(void);
extern	int		TerminateTheProcess(void);
extern	int		TerminateProcess(void);
extern	int		TestForFile(void);
extern	int		WriteMessage(void);
