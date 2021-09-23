/****************************************************************************
 *	File:	m9concur.h														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		1.0		Assigned it a version number.								*
 *																			*
 *		1.2		Added to the structure DTB_INFO the element dtb_hd, this is	*
 *				used in the function open_dti() where it is filled and in	*
 *				do_dtb() where it is read.  Added the declaration for the	*
 *				function open_dti() which was need when the function call to*
 *				terM9_executeDigitalTest() was broken into seperate elements*
 *				and then divided into two different functions.  This allowed*
 *				for the m9concur to get the dtb loaded into memory and		*
 *				everything ready so that the only thing left before control *
 *				was turned back to ATLAS was to run the burst(s).  This		*
 *				fixed an inconsistency problem on how long it would take	*
 *				before the burst would run after control was given back to	*
 *				ATLAS.														*
 *		1.3		Added the define statement for DEBUGIT, this allows the		*
 *				m9concur to be put into the debug mode without a reboot.	*
 *				Removed an unused variable in the dtb_info structure and	*
 *				added one in it's place, debug_option see previous statement*
 *																			*
 ***************************************************************************/

#ifndef	M9_ERROR
	#define	M9_ERROR	(-1)
#endif

#ifndef	TRUE
	#define	TRUE		1
#endif

#ifndef	FALSE
	#define	FALSE		0
#endif

#define	DTB_FILE_NAME		0
#define	RESET_DTI			1
#define	DIAG_TYPE			2
#define	PIN_STATE			3	
#define	CONCURRENT			4
#define	CIRCUIT_FILE_NAME	5

#define	FILE_ARG		0
#define	PIPE_ARG		1

#define	SUCCESS			0
#define	BEGINING		0L
#define	END				(-1L)
#define	CHARACTER		0
#define	LINE			1

#define	QUIT			1
#define	STATUS			2
#define	BYE				3
#define	SHUTITDOWN		4

#define	BURST_FAILED	162
#define	BURST_MAX_TIME	163
#define	BURST_NOT_RUN	164
#define	BURST_PASSED	166

#define	FILE_NAME             701
#define	FILE_WRITE_ERROR      702
#define	FILE_READ_ERROR       703
#define	USAGE_ERROR           704
#define	LIST_ERROR            705
#define	ARGNUM                706
#define	SYS_CMD_ERROR         707

#define	FILE_FORMAT_ERROR     800
#define	NO_DTB_STATED         801
#define	NO_CIR_FILE           802
#define	CIR_FILE_ERROR        803
#define	RESET_VALUE           804
#define	DIAG_VALUE            805
#define	STATE_VALUE           806
#define	CONCURT_VALUE         807
#define	DIR_WRITE_ERROR       808
#define	DIAG_FILE_WRITE_ERROR 809
#define	S_O_T_CALL_ERROR      810
#define	E_O_T_CALL_ERROR      811
#define	P_P_R_CALL_ERROR      812
#define	P_S_S_CALL_ERROR      813
#define	P_S_E_CALL_ERROR      814
#define	P_B_P_CALL_ERROR      815
#define	D_T_CALL_ERROR        816
#define	S_O_P_CALL_ERROR	  817
#define	E_O_P_CALL_ERROR	  818
#define	S_O_T_RET_ERROR       819
#define	E_O_T_RET_ERROR       820
#define	P_P_R_RET_ERROR       821
#define	P_S_S_RET_ERROR       822
#define	P_S_E_RET_ERROR       823
#define	P_B_P_RET_ERROR       824
#define	D_T_RET_ERROR         825
#define	S_O_P_RET_ERROR		  826
#define	E_O_P_RET_ERROR	      827
#define	PROBE_STAB_VAL        828
#define	PROBE_RESET_VAL       829
#define	PROBE_MIS_VAL         830
#define	MAX_SEED_VALUE        831
#define	NO_DTB_FILE           832
#define	DTB_VAR_ERROR         833
#define CXS_VAR_ERROR         834
#define	IMPROPER_ARG          835
#define	DIAG_DIR_ERROR		  836
#define	DIAG_DIR_IS_FILE	  837
#define	PROBE_NOWAIT		  838
#define	CONCURRENT_RUNNING	  839
#define	CONCUR_WONT_DIE		  840
#define	PROCESS_DONT_AGREE	  841
#define	PIPE_HANDLE_ERROR	  842
#define	IMPROPER_SHUTDOWN	  843
#define	WRITE_MSG_ERROR		  844
#define	READ_MSG_ERROR		  845
#define	CONCURRENT_ERROR	  846
#define	CHILD_RUN_WILD		  847
#define	DIAG_NOT_ALLOWED	  848
#define	DIAG_STATE_VAL        999

#define	DTBSUFFIX			".DTB"
#define	LOGLOCATION			"C:\\APS\\DATA\\"
#define	LOGFILE				"m910nam.dia"
#define	IDEFILE				"m910nam.ide"
#define	DEBUGFILENAME		"m9concur_debug"
#define	PIPE_FILE_HEADER	"\\\\.\\pipe\\"
#define	MAGIC_HEADER		"!#%magic@$file"
#define DEBUGIT				"debugit_m9con"

#define	UNSET_VALUE		99999

#define	NO_DIAG      0
#define	SEEDED_PROBE 1
#define	FAULT_DICT   2
#define	PROBE_ONLY   3

#define	MAX_PROCESS		1024
#define	MAX_MSG_SIZE	128

#define	M9_MAX_PATH _MAX_PATH

extern int		DE_BUG;
extern FILE		*debugfp;

typedef	struct{
	int			reset_flag;
	int			diag_type;
	int			pin_state;
	int			fault_info_requested;
	int			test_status;
	int			dtb_errno;
	int			opened;
	int			alldone;
	int			concurrent;
	char		dtb_file[M9_MAX_PATH];
	char		cir_file[M9_MAX_PATH];
	char		file_name[M9_MAX_PATH];
	char		result_file[81];
	char		fault_call[MAX_MSG_SIZE];
	char		fault_mess[MAX_MSG_SIZE];
	char		log_location[M9_MAX_PATH];
	char		log_file[M9_MAX_PATH];
	char		debug_option[M9_MAX_PATH];
	ViInt32		dtb_hd;
	ViSession	dti_handle;
}DTB_INFO;

typedef	struct{
	int		error_code;
	char	func_name[80];
	char	error_string[80];
}ERR_INFO;

typedef	struct{
	DWORD			threadid;
	HANDLE			threadhd;
}CONCUR_INFO;

typedef struct {
	int				message_value;
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
extern DTB_INFO		dtb_info;
extern ERR_INFO		err_info;

extern	int			get_pipe_hd(void);
extern	int			open_dti(void);
extern	int			terminate_running_m910(void);
extern	void		format_error(ViStatus,	char*);
extern	void		close_dti(void);
extern	int			error_ms(int, char*, char*, int);
extern	void		dodebug(int, char*, char*);
extern	int			fileexten(void);
extern	int			create_ide(void);
extern	int			check_header(char*, char*, int, int);
extern	int			compare_command(char*);
extern	int			parse_argument(int, char*);
extern	int			fill_in_struct_data(int, char*);
extern	int			parse_file(char*, int, int);
extern	int			write_message(void);
extern	int			read_message(void);
extern	void		clean_up(void);
extern	int			concur_main(int, char**);
extern	int			checkintvalues(void);
extern	void		perform_concurrent_op(void);
extern	int			start_proc(void);
extern	void		return_status(void);
extern	void		halt_running_dtb(void);
extern	int			get_response(void);
extern	void		respond_to_controlling_process(void);
extern	DWORD WINAPI do_dtb(LPVOID *);
