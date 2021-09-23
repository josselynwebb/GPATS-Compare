/////////////////////////////////////////////////////////////////////////////
//	File:	gpconcur.h														/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Changed the size of the character from 80 to 1024 for	/
//					the string ErrInfo.ErrorString.  This matches the size	/
//					of tmpcharstring in the function ErrorString().			/
//		1.0.2.0		Changed the way comments are done. Changed the way the	/
//					variables are written (camel back style).				/
//																			/
/////////////////////////////////////////////////////////////////////////////

#ifndef	GP_ERROR
	#define	GP_ERROR		(-1)
#endif

#ifndef	TRUE
	#define	TRUE			1
#endif

#ifndef	FALSE
	#define	FALSE			0
#endif

#define	TPS_NAME		0
#define	TPS_ERO			1
#define	TPS_PN			2
#define	TPS_SN			3
#define	APS_NAME		4

#define	NO				0
#define	YES				1

#define	KEEP			0
#define	DISREGARD		1

#define	OPT_ARG				0
#define	PIPE_ARG			1

#define	SUCCESS				0
#define	BEGINING			0L
#define	END					(-1L)
#define	CHARACTER			0
#define	LINE				1

#define	QUIT				1
#define	STATUS				2
#define	DOIT				3

#define	PRINTIT				1
#define	CLEANUP				2

#define	PROCESS_NOT_RUN		(-1)

#define	TPS_NAME_SIZE	27
#define	APS_NAME_SIZE	27
#define	PART_NUM_SIZE	14
#define	ERO_NUM_SIZE	6
#define	SERIAL_NUM_SIZE	25
#define	FILE_INFO_SIZE	5001
#define	FORWARD			1
#define	BACKWARD		2
#define	TEST_TRUE		0
#define	SUCCESS			0
#define	REQUIRED		1
#define	NO_INFO			1

#define	FILE_WRITE_ERROR		701
#define	FILE_READ_ERROR			702
#define	ARGNUM					703
#define	FILE_OPEN_ERROR			804
#define	FAULT_FILE_ERROR		805
#define	PIPE_HANDLE_ERROR		806
#define	WRITE_MSG_ERROR			807
#define	READ_MSG_ERROR			808
#define	CONCURRENT_ERROR		809

#define	LOGLOCATION			"C:\\APS\\DATA\\"
#define	FAULT_FILE			"FAULT-FILE"
#define	DEBUGFILENAME		"gpconcur_debug.txt"
#define DEBUGIT				"debugit_gpcon"
#define	PIPE_FILE_HEADER	"\\\\.\\pipe\\Local\\"
#define	FAULT_FILE_EXE_DIR		"C:\\Program Files (x86)\\ATS\\FilePrintViewer\\"
#define	APSNAMEFILE			"aps_names"

#define	GP_MAX_PATH			_MAX_PATH

typedef	struct {
	char		*StartPoint;
	char		*EndPoint;
	int			Size;
}GET_THIS;

typedef	struct{
	int				GPErrno;
	int				OptNumber;
	char			LogLocation[GP_MAX_PATH];
	char			LogFile[GP_MAX_PATH];
	char			DebugOption[GP_MAX_PATH];
}GPCON_INFO;

typedef	struct{
	int				ErrorCode;
	char			FuncName[80];
	char			ErrorString[1024];
}ERR_INFO;

typedef struct {
	short			year;
	unsigned short	month;
	unsigned short	day;
	unsigned short	hour;
	unsigned short	min;
	unsigned short	sec;
}TIME;

typedef	struct {
	char	TPSName[30];
	char	ERONumber[6];
	char	PartNumber[30];
	char	Serial[25];
	TIME	DateTime;
	int		InfoGood;
	char	APSName[30];
	char	*FileInfo;
}FAULTFILE;

extern	FAULTFILE	FaultFp;
extern	GET_THIS	GetThis[];
extern	GPCON_INFO	GPConInfo;
extern	ERR_INFO	ErrInfo;

extern	int			DE_BUG;
extern	FILE		*debugfp;

extern	int			CleanUp(void);
extern	void		CloseDataBase(void);
extern	int			ConcurMain(int, char**);
extern	int			CreateFile(char*, char*);
extern	void		DoDebug(int, char*, char*, ...);
extern	int			ErrorString(int, char*, char*, int);
extern	int			ExtractInfo(char*, int);
extern	int			FillInDataBase(void);
extern	int			FindPoint(char*, int*, char*, int);
extern	char		*FindStringRight(char*, char*, int, int);
extern	int			GetDateTime(char*);
extern	int			GetInfo(char*, int, int);
extern	char		*GetItem(char*, int);
extern	int			GetPipeHd(void);
extern	int			GetResponse(void);
extern	int			GotoTermNumber(char*, int, int*);
extern	int			MoveToPoint(char*, int*, char*, int);
extern	int			NewFormat(char*, int);
extern	int			NumberOfThis(char*, char*);
extern	int			OldFormat(char*, int);
extern	int			OpenDataBase(void);
extern	void		PerformConcurrentOp(void);
extern	int			PrintFaultFile(void);
extern	int			ReadMessage(void);
extern	void		RemoveSpaces(int*, char*, int);
extern	void		RespondToControllingProcess(void);
extern	void		ReturnStatus(void);
extern	char		*SearchForAPSName(char*, char*);
extern	int			StartProcess(void);
extern	int			WriteMessage(void);