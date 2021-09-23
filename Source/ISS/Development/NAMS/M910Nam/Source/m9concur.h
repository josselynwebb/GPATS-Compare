/****************************************************************************
 *	File:	m9concur.h														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *																			*
 ***************************************************************************/

#define	PROCESS_NAME		"m9concur.exe"
#define	PROG_BIN_DIR		"C:\\usr\\tyx\\bin\\"
#define	PROGRAM_LAUNCHER	"m9launcher.exe"

#define	PIPE_FILE			"m910_pipe_file"
#define	PIPE_FILE_HEADER	"\\\\.\\pipe\\"

#define	MAX_PROCESS			1024

typedef	struct{
	int				concurrent_running;
	unsigned long	processid;
	char			process_name[M9_MAX_PATH];
}CONCUR_INFO;

typedef struct {
	int				message_size;
	int				read_return;
	unsigned long	message_size_read;
	int				write_return;
	unsigned long	message_size_written;
	char			write_msg[MAX_MSG_SIZE];
	char			read_msg[MAX_MSG_SIZE];
	char			pipe_file[M9_MAX_PATH];
	HANDLE			pipehd;
}PIPE_INFO;

extern CONCUR_INFO	concur_info;
extern PIPE_INFO	pipe_info;