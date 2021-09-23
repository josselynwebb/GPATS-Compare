/////////////////////////////////////////////////////////////////////////////
//	File:	gpnam.h															/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		0.31	Assigned it a version number.								/
//		1.0.0.0	Needed to recode the entire nam.  Could't suspend ATLAS		/
//				while the nam ran and allow the VB program to run			/
//				concurrently.												/
// 		1.1.0.0	Added the #defines IP_ADDRESS; ADDRESS; MASK; NAME; PSWD;	/
// 				PSEXECDIR; and PSEXEC.  Added address and mask to gp_info	/
// 				structure.  These were added to the gpnam to allow for the	/
// 				setting of the ip address of the controller by either an	/
// 				ATLAS or any executable program.  This will allow for a		/
// 				non-privilege user to set the ip address.  Also added the	/
// 				function call setAddress and setArgumentValue for the same.	/
// 				The #define ADDRESS_FILE was also added for the above same.	/
//		1.2.0.0	Added the defines INSERT_ADDITONAL_INFO - allow for the		/
//				insertion of user specified info on fault callout, IDE XML -/
//				used to determine the file type that is to have the user	/
//				specified info inserted into,LOCALNETWORK, GIGABIT1,		/
//				GIGABIT2, RESETPORT, GATEWAY, and PORTNAME - used to set or	/
//				reset the ethernet ports. Added IADS_3_2_7 and IADS_3_4_13 -/
//				used to see which iads executable was used. Added			/
//				GIGABIT1ADDR, GIGABIT1GATE, GIGABIT2ADDR, GIGABIT2GATE, and	/
//				DEFAULTMASK - these are port address used to reset the		/
//				different ethernet ports. Added Gateway and NetworkName to	/
//				the GP_INFO structure used to determine which port is to be	/
//				set/reset. Added new functions definitions for the new		/
//				capabilities added to gpnam.								/
//																			/
/////////////////////////////////////////////////////////////////////////////

#ifndef	GP_ERROR
	#define	GP_ERROR (-1)
#endif

#ifndef	TRUE
	#define	TRUE 1
#endif

#ifndef	FALSE
	#define	FALSE 0
#endif

#define	SUCCESS			0

#define	PRINT_FAULT_FILE		1
#define	CLEAN_UP				2
#define	INSERT_INTO_FAULT_FILE	3
#define	IP_ADDRESS				4
#define	RESET_IP				5
#define	INSERT_ADDITONAL_INFO	6
#define	PING_IP					7
#define	COMPUTER_NAME			8
#define	CURRENT_WORKING_DIR		9

#define	COMPUTERNAMESIZE		4

#define	IDE				1
#define	XML				2

#define	SINGLE_PING		4
#define	MULTI_PING		9

#define	LOCALNETWORK	0
#define	GIGABIT1		1
#define	GIGABIT2		2
#define	GIGABIT4		3

#define	TMP_FILE		1
#define	OPTION			1
#define	ARGCOFFSET		1
#define	RESETPORT		2
#define	ADDRESS			2
#define	COMPUTER		2
#define	PORTNAME		3
#define	PINGNUMBER		3
#define	NAMERETURN		3
#define	DIRECTORYPATH	2
#define	MASK			4
#define	MAXTIME			4
#define	GATEWAY			5
#define	MINTIME			5
#define	AVGTIME			6
#define	DROPEDPACK		7
#define	RECVDPACK		8

#define	ATLAS_INT		0
#define	ATLAS_CHAR		1
#define	CMD_LINE_INT	2
#define	CMD_LINE_CHAR	3

#define	ARGNUM					800
#define	OPTION_SENT				801
#define	INT_TYPE				802
#define	FILE_READ_ERROR			803
#define	FILE_POSITION_ERROR		804
#define	FILE_WRITE_ERROR		805
#define	FILE_OPEN_ERROR			806
#define	FAULT_FILE_ERROR		807
#define	CONCURRENT_RUNNING		809
#define	CONCUR_WONT_DIE			810
#define	PROCESS_DONT_AGREE		811
#define	PIPE_HANDLE_ERROR		812
#define	IMPROPER_SHUTDOWN		813
#define	WRITE_MSG_ERROR			814
#define	READ_MSG_ERROR			815
#define	CONCURRENT_ERROR		816
#define	CHILD_RUN_WILD			817
#define	TEXT_TYPE				818

#define	NAME				"Administrator"
#define	PSWD				"ATSU856"
//#define	LOGLOCATION2			"C:/APS/DATA/Address.bat"
#define	ADDRESS_FILE		"address.bat"
#define	FAULT_FILE			"FAULT-FILE"
#define	DIA_FILE			"M910NAM.dia"
#define	DEBUGFILENAME		"gpdebug.txt"
#define	PINGOUTPUTNAME		"PingResults.txt"
#define	DIROUTPUTPATH		"DirOutputPath.txt"
#define	COMPUTERNAME		"ComputerName.txt"
#define DEBUGIT				"debugit_gp"
#define	SUFFIX				".TMP"
#define	FAULTFILEEXEDIR		"C:\\aps\\data\\"
#define	FAULTFILEEXECUTABLE	"FaultFilePrint.exe"
#define	PSEXECDIR			"c:\\Program Files\\Tester Programs\\PsExec"
#define	PSEXEC				"psexec.exe"

#define	IADS_3_2_7			"/IADS_3_2_7.exe"
#define	IADS_3_4_13			"/IADS_3_4_13.exe"
#define	IADS_3_4_25			"/IADS_3_4_25.exe"

#define	GIGABIT1ADDR		"192.168.0.1"
#define	GIGABIT1GATE		"192.168.0.2"
#define	GIGABIT2ADDR		"192.168.200.1"
#define	GIGABIT2GATE		"192.168.200.2"
#define	DEFAULTMASK			"255.255.255.0"
#define GIGABIT4ADDR		"192.168.20.1"
#define GIGABIT4GATE		"192.168.20.2"

#define	MAX_MSG_SIZE 1024
#define	GP_MAX_PATH _MAX_PATH

extern	int		DE_BUG;
extern	int		ATLAS;

extern	unsigned int	DriveType;

extern	FILE	*debugfp;

typedef	struct {
	int		return_value;
	int		PingCount;
	char	log_location[GP_MAX_PATH];
	char	log_file[GP_MAX_PATH];
	char	debug_option[GP_MAX_PATH];
	char	PrintProg[GP_MAX_PATH + 10];
	char	address[18];
	char	mask[18];
	char	Gateway[18];
	char	NetworkName[24];
}GP_INFO;

extern	GP_INFO gp_info;

extern	void	dodebug(int, char*, char*, ...);
extern	void	DoPing(int argc, char* argv[]);
extern	int		DoResetIP(int, char**, int*);
extern	int		execute_gpconcur(int);
extern	int		find_process(char*);
extern	int		FixTmpFile(char*, int, char**);
extern	void	GetCurrentWorkingDir(char *argv[]);
extern	void	get_drive_type(void);
extern	int		GetSystemName(char *argv[]);
extern	int		gpnam_main(int, char**);
extern	void	InsertAdditionalInfo(int, char**);
extern	int		insert_into_fault_file(void);
extern	int		read_message(void);
extern	int		start_proc(int);
extern	int		terminate_process(void);
extern	int		terminate_process_on_startup(void);
extern	int		write_message(void);
extern	int		pipeHandle(void);
extern	int		get_pipe_hd(void);
extern	int		setAddress(int, int);
extern	int		returnArgumentValue (int dataType, char *argument, char *stringToFill, int StringSize, int valueToFill, FILE *OutPutfp);
extern	int		setArgumentValue(int, char*, char*, int, int*);
extern	int		DoSetIP(int, char**, int*);
extern	void	retrieveErrorMessage(char *functionName, char *message);
