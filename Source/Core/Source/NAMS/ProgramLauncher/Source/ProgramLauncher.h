/////////////////////////////////////////////////////////////////////////////
//	File:	ProgramLauncher.h												/
//																			/
//	Creation Date:	15 March 2013											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//																			/
/////////////////////////////////////////////////////////////////////////////

#define	OPERATIONAL			0
#define	FUNCTIONAL			1

#define	EXECUTE_PROGRAM		2
#define	CMD_LINE_OPTION		3
#define	OPERATION_MODE		4

#define	PROCESS_NOT_RUN		(-1)

#define	ARGNUM				801
#define	DATACHAR			802
#define	DATAINT				803
#define	UNKOPT				804
#define	FILE_OPEN_ERROR		805
#define	PROCESS_ERROR		806

#define	TMP_FILE			1
#define	RETURN_STATUS		5

#define	ROOTDIR				1
#define	DRIVEDIR			2
#define	RELATIVEPATH		3
#define	INPATH				4
#define	FIXRELATIVE			5

#define	SUFFIX				".TMP"
#define	DEBUGFILENAME		"ProgramLauncherDebug.txt"
#define DEBUGIT				"debugit_launcher"

typedef	struct{
	int		ProgErrno;
	int		ExitStatus;
	int		LaunchMode;
	char	CommandString[MAX_MSG_SIZE];
	char	OperatorInstructions[MAX_MSG_SIZE];
	char	CommandOptions[MAX_MSG_SIZE];
	char	FileName[NAM_MAX_PATH];
	char	LogLocation[NAM_MAX_PATH];
	char	DebugOption[NAM_MAX_PATH];
	char	FileLocation[NAM_MAX_PATH];
}PROG_INFO;

typedef	struct {
	int					ProgramRunning;
	PROCESS_INFORMATION	ProgramInfo;
}PROC_INFO;

extern	PROG_INFO	ProgInfo;
extern	PROC_INFO	ProcInfo;

extern	int			DE_BUG;
extern	FILE		*debugfp;

extern	void	BuildCommandString(void);
extern	void	ConvertChar(char*, char*, char, char, int);
extern	void	dodebug(int, char*, char*, ...);
extern	int		DoInitalSetup(int argc, char *argv[]);
extern	int		ProgramLauncherMain (int, char **);
extern	int		FilePath(int, int, int);
extern	int		FilePresent(char*, int);
extern	int		FileStyle(char*, char*);
extern	void	InitVariables(void);
extern	int		ParseArguments(int, char**);
extern	int		StartProc(void);
extern	int		TerminateProgram(void);
extern	int		TerminateTheProcess(void);
extern	int		TestForFile(void);
extern	int		WaitOnTheProcess(void);
