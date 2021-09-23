/////////////////////////////////////////////////////////////////////////////
//	File:	CoyoteNam.h														/
//																			/
//	Creation Date:	12 February 2016										/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		2.0.0.0		Combined Source from Astronics with	/
//					source from VIPERT 1.3.1.0.  							/
//		2.1.0.0		Added hnges from VIPERT 1.3.2.0							/
//																			/
/////////////////////////////////////////////////////////////////////////////
#define	INVALIDEXE			-101
#define	POWERBAD			-102
#define	STAGEBAD			-103
#define INVALID_EXE			"ERROR - Unable to find the Coyote executable: "
#define POWER_BAD			"ERROR - Unable to set internal camera power on: "
#define STAGE_BAD			"ERROR - Unable to set sensor stage to camera: "
#define	UNKNOWN				"Unknown Error Happened"
#define	SUCCESSFUL			"Successful"

#define	VK_NOKEY			(-1)

#define	ARGNUM				801
#define	DATACHAR			802
#define	DATAINT				803
#define	DATADEC				804
#define	DATABOOL			805
#define	UNKOPT				806
#define	FILE_OPEN_ERROR		807

#define	TMP_FILE			1
#define	XML_FILE_NAME		2
#define	RETURN_STATUS		2
#define	RETURN_STRING		3

#define	MIM_NUMBER			4
#define	STRING_VAL			1

#define	SUFFIX				".TMP"
#define	DEBUGFILENAME		"CoyoteDebug.txt"
#define DEBUGIT				"DebugitCoyote"
#define	LIB_NAME			"C:\\irwin2001\\veo2.dll"

#define	COYOTE_EXECUTE		"C:\\Program Files (x86)\\iPORT PT1000\\Binaries\\Coyote.exe"
#define	OPTION_FLAG			"/f"
#define	XML_FILE			"C:\\Irwin2001\\IrwdVeo2Pleora.xml"

#define	SPACE_KEY			32
#define	BACK_SLASH			92
#define	DASH_KEY			45
#define	PERIOD_KEY			46
#define	ZERO_KEY			48
#define	NINE_KEY			57
#define	COLON_KEY			58
#define	CAP_A_KEY			65
#define	CAP_Z_KEY			90
#define	UNDERSCORE_KEY		95
#define	LITTLE_A_KEY		97
#define	LITTLE_Z_KEY		122

#define	VK_OEM_1			0xBA
#define	VK_OEM_5			0xDC
#define	VK_OEM_MINUS		0xBD
#define	VK_OEM_PERIOD		0xBE

typedef	struct{
	int		CoyoteErrno;
	int		ExitStatus;
	int		Concurrent;
	char	CommandString[MAX_MSG_SIZE];
	char	CommandOptions[MAX_MSG_SIZE];
	char	LogLocation[NAM_MAX_PATH];
	char	DebugOption[NAM_MAX_PATH];
	char	FileLocation[NAM_MAX_PATH];
	HMODULE	Veo2Hd;
	HANDLE	CoyoteHd;
	HANDLE	ThreadHd;
}COYOTE_INFO;

extern	COYOTE_INFO		CoyoteInfo;

extern	int			DE_BUG;
extern	FILE		*debugfp;

extern	int		ConvertCharacter(void);
extern	int		CoyoteNamMain (int argc, char *argv[]);
extern	int		CheckTheArguments(int argc, char *argv[]);
extern	int		CheckCharValues(int ArgcValue, char *argv[]);
extern	int		CheckIntValues(int ArgcValue, char *argv[]);
extern	int		DoInitalSetup(int argc, char *argv[]);
extern	void	dodebug(int code, char *functionName, char *format, ...);
extern	void	DoCloseout(int IsAtlas, char *argv[]);
extern	int		FillInVariables(int argc, char *argv[], int IsAtlas);
extern	int		GetCharValue(int ArgcValue, char *argv[], int StringSize);
extern	int		InitializeVeo2ForNam(void);
extern	void	InitVariables(void);
extern	int		StartTheCamera(void);
extern	int		StopTheCamera(void);
extern	int		SetTheStage(void);
extern	int		StartTheExecutable(void);
extern	int		SendKeyStrokes(void);
extern	int		SendRequestedKey(int FirstKey, int SecondKey);
extern	int		TurnOntheCamera(void);
