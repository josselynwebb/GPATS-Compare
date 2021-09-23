/////////////////////////////////////////////////////////////////////////////
//	File:	gpconcur.h														/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0	Needed to recode the entire nam.  Could't suspend ATLAS		/
//				while the nam ran and allow the VB program to run			/
//				concurrently.												/
//																			/
/////////////////////////////////////////////////////////////////////////////

#define	PROCESS_NAME		"gpconcur.exe"
#define	PROG_BIN_DIR		"C:\\usr\\tyx\\bin\\"

#define	PIPE_FILE			"gpnam_pipe_file"
#define	PIPE_FILE_HEADER	"\\\\.\\pipe\\Local\\"

#define	MAX_PROCESS			1024

typedef	struct{
	int				concurrent_running;
	unsigned long	processid;
	char			process_name[GP_MAX_PATH];
}CONCUR_INFO;

extern CONCUR_INFO	concur_info;

