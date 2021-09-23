/////////////////////////////////////////////////////////////////////////////
//	File:	etm_monitor.h													/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
// 		1.0.1.0		Changed the style of the text file, eliminated the		/
// 					underscores.											/
// 					modified the extern declaration of the two functions	/
// 					message_utility(), and setup_write_msg().  These were	/
// 					changed to allow for the passing of the size of the 	/
// 					character array so the function could zero out the 		/
// 					buffer.													/
//					changed the define PIPE_ARG from one to two.  This was	/
//					done to	allow for the non inheritance of open file		/
//					handles.												/
//		1.0.2.0		Added the macro define DIRECTORY2_CMD, DIR2_TOP_LEVEL,	/
//					and DIRECTORY2_SEP.  These allowded for forward /		/
//					instead of back \.  Deleted NotUsed and added two new	/
//					variables ImagNotUsed and ReadrNotUsed to the WIN_INFO	/
//					structure.  This allowed for the setting of the two		/
//					different windows sizes, one for the imager and one for	/
//					the reader.  Added new variable SavedFileName to the	/
//					ETM_INFO structure.  This allowed for the m910 fault	/
//					callout file to be displayed in IADS.  Added four new	/
//					variables to the IMAG_INFO structure; wxoff, wyoff,		/
//					wxlen, and wylen, Need for setting the image window size/
//		2.0.0.0		Combined Source from Astronics with						/
//					source from VIPERT 1.3.1.0.  							/ 
//																			/
/////////////////////////////////////////////////////////////////////////////

#define	OPERATIONAL			0
#define	FUNCTIONAL			1

#define	BASE327				1
#define	BASE3413			2
#define	BASE3425			3 


#define	TARGET				0
#define	READER_ONLY			1
#define	ZOOM				2

#define	NULL_IT				1
#define	END_IT				2

#define	WINDOW_Y2			0
#define	WINDOW_X2			1
#define	WINDOW_Y1			2
#define	WINDOW_X1			3

#define	READER_RUNNING		0x00001
#define	READER_NOT_RUNNING	0x00010
#define	IMAGER_RUNNING		0x00100
#define	IMAGER_NOT_RUNNING	0x01000
#define	WINDOW_INFO			0x10000

#define	WINDOW_DATA				18
#define	OPTION_DATA				80
#define	FILE_DATA				NAM_MAX_PATH
#define	STATUS_DATA				256
#define	REFERENCE_DATA			256
#define	FUNCTIONAL_DATA			12
#define	OPERATIONAL_DATA		12

#define	PIPE_ARG				1

#define	FORWARD					1
#define	BACKWARD				2

#define	FILE_CMD				0
#define	STATUS_CMD				1
#define	WINDOW_CMD				2
#define	OPTIONS_CMD				3
#define	REFERENCE_CMD			4
#define	FUNCTIONAL_CMD			5
#define	OPERATIONAL_CMD			6
#define	DIRECTORY_CMD			7
#define	BYE_CMD					7
#define	NONE_CMD				8
#define DIRECTORY2_CMD			8  
#define	QUIT_CMD				9
#define	ZOOM_CMD				10
#define	READER_CMD				11
#define	CONCURENT_CMD			12
#define	TARGET_CMD				13
#define	SUCCESS_CMD				14
#define	ILLEGAL_CMD				15
#define	CONSECUTIVE_CMD			16
#define	TERMINATE_ALL_CMD		17
#define	TERMINATE_READER_CMD	18
#define	TERMINATE_ZOOM_CMD		19
#define	COMMAND_NUM				19
#define	DIRECTORY_SIZE			26

#define	BYE_MSG					"Bye"
#define	FILE_MSG				"File Name"
#define	NONE_MSG				"None"
#define	QUIT_MSG				"Quit"
#define	READ_MSG				"Reader"
#define	ZOOM_MSG				"Zoom"
#define	READER_MSG				"Reader"
#define	CONCURRENT_MSG			"Concurrent"
#define	STATUS_MSG				"Status"
#define	TARGET_MSG				"Target"
#define	WINDOW_MSG				"Window Info"
#define	OPTIONS_MSG				"Image Options"
#define	SUCCESS_MSG				"Success"
#define	ILLEGAL_MSG				"Illegal Command"
#define	CONSECUTIVE_MSG			"Consecutive"
#define	REFERENCE_MSG			"Reference"
#define	FUNCTIONAL_MSG			"Functional Mode"
#define	OPERATIONAL_MSG			"Operational Mode"
#define	TERMINATE_ALL_MSG		"Terminate_All"
#define	TERMINATE_READER_MSG	"Terminate_Reader"
#define	TERMINATE_ZOOM_MSG		"Terminate_Zoomview"
#define	DIR_TOP_LEVEL			":\\"
#define	DIRECTORY_SEP			"\\"
#define	DIR2_TOP_LEVEL			":/"  
#define	DIRECTORY2_SEP			"/"   


#define	END_MSG					"End"
#define	WINDOW_END				"End of Window"
#define	OPTION_END				"End of Options"
#define	END_FILE				"End of File"
#define	REFERENCE_END			"End of Reference"

#define	PROCESS_NOT_RUN		(-1)

#define	FILE_WRITE_ERROR	701
#define	FILE_READ_ERROR		702
#define	ARGNUM				703
#define	FILE_OPEN_ERROR		804
#define	FAULT_FILE_ERROR	805
#define	PIPE_HANDLE_ERROR	806
#define	WRITE_MSG_ERROR		807
#define	READ_MSG_ERROR		808
#define	CONCURRENT_ERROR	809

#define	DEBUGFILENAME		"EtmMonitorDebug.txt"
#define DEBUGIT				"debugit_etm"

#define	IADS_3_2_7			"..\\..\\..\\IADS_3_2_7.exe"
#define	IADS_3_4_13			"..\\..\\..\\IADS_3_4_13.exe"
#define	IADS_3_4_25			"..\\..\\..\\IADS_3_4_25.exe"
#define	BASE_3_2_7			"c:/iads/programs/"
#define	BASE_3_4_13			"c:/program files (x86)/iads3.4/"
#define	BASE_3_4_25			"c:/program files (x86)/iads3.4/"
#define	READER_PROG			"readr.exe"
#define	IMAGER_PROG			"zoomview.exe"

#define	SUBKEY_CONSTANT	"Software\\IADS\\Paths\\"

#define	USERID				"-userid32767"

typedef	struct{
	int		Mode;
	int		EtmErrno;
	int		Concurrent;
	int		IadsVer;
	char	ProgramBase[NAM_MAX_PATH];
	char	CommandString[MAX_MSG_SIZE];
	char	WindowSize[MAX_MSG_SIZE];
	char	ImageOptions[MAX_MSG_SIZE];
	char	FileName[NAM_MAX_PATH];
	char	SavedFileName[NAM_MAX_PATH]; 
	char	TargetLabel[MAX_MSG_SIZE];
	char	LogLocation[NAM_MAX_PATH];
	char	DebugOption[NAM_MAX_PATH];
	char	BaseDirName[26];
}ETM_INFO;

typedef	struct{
	int		ErrorCode;
	char	FuncName[80];
	char	ErrorString[MAX_MSG_SIZE];
}ERR_INFO;

typedef	struct {
	int		NotUsed;
	int		ImagNotUsed; 
	int		ReadrNotUsed; 
	int		Heigth;
	int		Width;
	long	Rx1;
	long	Ry1;
	long	Rx2;
	long	Ry2;
	long	Ix1;
	long	Iy1;
	long	Ix2;
	long	Iy2;
	long	ScreenW;
	long	ScreenH;
	char	Title[NAM_MAX_PATH];
	HWND	ReadWhndl;
	HWND	ImageWhndl;
}WIN_INFO;

typedef	struct {
	int		NotUsed;
	int		WNotUsed; 
	int		DefinedUsed;
	int		xoff;
	int		yoff;
	int		xlen;
	int		ylen;
	int		wxoff;  
	int		wyoff;  
	int		wxlen;  
	int		wylen;  
	char	Defined[5];
}IMAG_INFO;

typedef	struct {
	int					ReaderRunning;
	int					ImagerRunning;
	PROCESS_INFORMATION	ReadProcInfo;
	PROCESS_INFORMATION	ImageProcInfo;
}PROC_INFO;

extern	ETM_INFO	EtmInfo;
extern	WIN_INFO	WinInfo;
extern	ERR_INFO	ErrInfo;
extern	IMAG_INFO	ImagInfo;
extern	PROC_INFO	ProcInfo;

extern	int			DE_BUG;
extern	int			continueEnum;
extern	FILE		*debugfp;

extern	HWND	CheckForWindow(HWND, DWORD);
extern	int		CheckReturn(int, char **, int*, char*, int*);
extern	void	dodebug(int, char*, char*, ...);
extern	BOOL	CALLBACK EnumWindProc(HWND, LPARAM);
extern	int		ErrorMs(int, char*, char*, int);
extern	int		EtmMonitorMain (int, char **);
extern	int		ExtractApsKeyName(void);
extern	int		FindPoint(char*, int*, char*, int);
extern	void	FreeItem(char*);
extern	int		GatherData(void);
extern	int		GenerateCommandLine(int);
extern	int		GetData(char*, char*);
extern	int		GetIadsVersion(void);
extern	int		GetMode(int);
extern	int		GetPipeHd(void);
extern	int		GetResponse(void);
extern	char	*GetInfo(char*, int);
extern	void	ImagerCommandLine(char*, char*, char*);
extern	void	InitVariables(void);
extern	int		MessageRequest(char*);
extern	void	MessageUtility(int, char*, int);
extern	int		MoveToPoint(char*, int*, char*, int);
extern	void	PerformConcurrentOp(void);
extern	int		PerformImagerFunction(void);
extern	int		PerformReaderFunction(void);
extern	int		ProcessData(void);
extern	int		ProcessGatheredData(void);
extern	int		ReadMessage(void);
extern	void	ReaderCommandLine(char*, char*, char*);
extern	void	RemoveSpaces(int*, char*, int);
extern	void	RespondToControllingProcess(void);
extern	void	ReturnStatus(void);
extern	double	Round(double);
extern	int		SendMessage(char*);
extern	int		SetAppWindow(void);
extern	int		SetUpIads(void);
extern	int		SetUpTheRegistry(void);
extern	void	SetupWriteMsg(char*, char*, int);
extern	int		StartProc(void);
extern	int		TerminateTheImager(void);
extern	int		TerminateImager(void);
extern	int		TerminateTheReader(void);
extern	int		TerminateReader(void);
extern	int		WaitOnViewer(DWORD);
extern	int		WriteMessage(void);
