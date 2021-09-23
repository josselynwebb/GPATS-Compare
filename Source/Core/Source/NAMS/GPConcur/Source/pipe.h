/////////////////////////////////////////////////////////////////////////////
//	File:	pipe.h															/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Changed the way comments are done. Changed the way the	/
//					variables are written (camel back style).				/
//																			/
/////////////////////////////////////////////////////////////////////////////

#define	MAX_MSG_SIZE		128

typedef struct {
	int				MessageValue;
	int				MessageSize;
	int				ReadReturn;
	unsigned long	MessageSizeRead;
	int				WriteReturn;
	unsigned long	MessageSizeWritten;
	char			WriteMsg[MAX_MSG_SIZE];
	char			ReadMsg[MAX_MSG_SIZE];
	char			PipeFile[GP_MAX_PATH];
	HANDLE			Pipehd;
}PIPE_INFO;

extern PIPE_INFO	PipeInfo;