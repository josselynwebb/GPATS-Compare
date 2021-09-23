/****************************************************************************
 *	File:	m910nam.h														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *		2.5		Added the define DEBUGIT, this was added to make it easier	*
 *				to put the program into debug mode.							*
 *				Added three elements to the structure DTB_INFO.  atlas_dir	*
 *				will hold the path to the ATLAS program.  digital_dir will	*
 *				hold the path to the digital program.  debug_option will	*
 *				the file name to look for to set the DE_BUG flag.			*
 *				Added three new functions, convert_files_set_dir is used to	*
 *				convert the dtb and cxs file to just the file with no path	*
 *				info and to set the dtb_info.atlas_dir and					*
 *				dtb_info.digital_dir,  change_dir will set the working		*
 *				directory to either the atlas_dir or digital_dir depending	*
 *				on the flag passed,  set_dir_file will set the output		*
 *				directory for the diagnose information.						*
 *																			*
 ***************************************************************************/

#ifndef	M9_ERROR
	#define	M9_ERROR (-1)
#endif

#ifndef	TRUE
	#define	TRUE 1
#endif

#ifndef	FALSE
	#define	FALSE 0
#endif

#define	RESET_AFTER		0
#define	RESET_BEFORE	1

#define	NO_RESET_BEFORE	0
#define	NO_RESET_AFTER	1

#define	DIAG_OUTPUT_DIRECTORY            0
#define	DIAG_OUTPUT_FILE_NAME            1
#define	DTB_FILE_NAME                    2
#define	CIRCUIT_FILE_NAME                3
#define	RESET_DTI                        4
#define	DIAG_TYPE                        5
#define	PIN_STATE                        6
#define	CONCURRENT                       7
#define	START_OF_TEST_CALLBACK           8
#define	END_OF_TEST_CALLBACK             9
#define	PROBE_POINT_READY_CALLBACK      10
#define	PROBE_SEQUENCE_STARTED_CALLBACK 11
#define	PROBE_SEQUENCE_ENDED_CALLBACK   12
#define	PROBE_BUTTON_PRESSED_CALLBACK   13
#define	DIAG_TEST_CALLBACK              14
#define	START_OF_PATTERN_CALLBACK       15
#define	END_OF_PATTERN_CALLBACK         16
#define	PROBE_STABILITY_COUNT           17
#define	PROBE_RESET_ENABLE              18
#define	PROBE_MISMATCH_VALUE            19
#define	MAX_SEEDING_VALUE               20
#define	PROBE_WAIT                      21
#define	EXECUTABLE_PROGRAM				22

#define	RETURN_STATUS_NEW_WAY  3
#define	FAULT_FILE_NEW_WAY	   4
#define	FAULT_CALLOUT_NEW_WAY  5
#define	FAULT_MESSAGE_NEW_WAY  6

#define	FAULT_FILE_OLD_WAY     7
#define	RETURN_STATUS_OLD_WAY  8
#define	FAULT_CALLOUT_OLD_WAY  9
#define	FAULT_MESSAGE_OLD_WAY 10

#define	OLD 0
#define NEW 5

#define	BASE327		1
#define	BASE3413	2
#define BASE3425	3
#define	TMP_FILE	1

#define	START_IT	1
#define	HALT_IT		2

#define	SUCCESS			0
#define	BEGINING		0L
#define	END				(-1L)
#define	CHARACTER		0
#define	LINE			1
#define	WHITE_SPACES	(-1)
#define	INDEX_OFFSET	8

#define	FAULT_INFO 9

#define	UNSET_VALUE	99999

#define	BURST_FAILED   162
#define	BURST_MAX_TIME 163
#define	BURST_NOT_RUN  164
#define	BURST_PASSED   166

#define	PROGRAM_FAILED		162
#define	PROGRAM_NOT_RUN		164
#define	PROGRAM_PASSED		166

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
#define	DIAG_STATE_VAL        999

#define	TMPSUFFIX "TMP"
#define	DTBSUFFIX "DTB"

#define	LOGLOCATION   "C:\\APS\\DATA\\"
#define	LOGFILE       "m910nam.dia"
#define	DEBUGFILENAME "m910debug"
#define DEBUGIT		  "debugit_m910"
#define	IADS_3_2_7			"..\\..\\..\\IADS_3_2_7.exe"
#define	IADS_3_4_13			"..\\..\\..\\IADS_3_4_13.exe"
#define	IADS_3_4_25			"..\\..\\..\\IADS_3_4_25.exe"
#define	FAULT_CALLOUT	"FaultCallout.txt"
#define	FAULT_MESSAGE	"FaultMessage.txt"

#define	NO_DIAG      0
#define	SEEDED_PROBE 1
#define	FAULT_DICT   2
#define	PROBE_ONLY   3

#define	MAX_MSG_SIZE 1024
#define	M9_MAX_PATH _MAX_PATH

#define	MAGIC_HEADER "!#%magic@$file"

extern int		DE_BUG;
extern	int		ATLAS;
extern FILE		*debugfp;
extern FILE		*FaultCalloutFp, *FaultMessageFp;

typedef	struct{
	short	used;
	char	*program;
}CB_INFO;

typedef struct{
	short	used;
	char	*name;
	DLL_EXPORT DiagStatus (_stdcall * fp_callback) ();
	union {
		void	(_stdcall * fp_callback_fp) (char *);
		void	(_stdcall * fp_callback_fpp) (DiagStatus);
	}u;
	char	*options;
}CB_FUNC;

typedef	struct{
	int			reset_flag;
	int			diag_type;
	int			pin_state;
	int			fault_info_requested;
	long		test_status;
	int			dtb_errno;
	int			opened;
	int			method;
	int			alldone;
	int			return_value;
	int			probe_wait;
	int			concurrent;
	int			mismatch_value;
	int			seed_value;
	int			probe_reset;
	int			concurrent_running;
	int			iads_version;
	long		stability_count;
	char		dtb_file[M9_MAX_PATH];
	char		cir_file[M9_MAX_PATH];
	char		file_name[M9_MAX_PATH];
	char		result_file[81];
	char		fault_call[MAX_MSG_SIZE];
	char		fault_mess[MAX_MSG_SIZE];
	char		log_location[M9_MAX_PATH];
	char		log_file[M9_MAX_PATH];
	char		execute_prog[M9_MAX_PATH];
	char		atlas_dir[M9_MAX_PATH];
	char		digital_dir[M9_MAX_PATH];
	char		debug_option[M9_MAX_PATH];
	DiagEnable	probe_nowait;
	ViSession	dti_handle;
}DTB_INFO;

extern DTB_INFO		dtb_info;
extern CB_INFO		*cb_info;
extern CB_FUNC		cb_func[];

extern	int			get_pipe_hd(void);
extern	int			terminate_running_m910(void);
extern	DiagStatus	do_diag_output(void);
extern	int			diag_setup(void);
extern	int			do_diag(void);
extern	void		format_error(ViStatus,	char*);
extern	int			do_dtb(void);
extern	int			close_dti(void);
extern	int			error_ms(int, char*, char*);
extern	void		dodebug(int, char*, char*);
extern	int			create_file(char*, char*);
extern	int			insert_text(char*, long, int, char**);
extern	int			change_char(char*, char*, char*);
extern	int			check_dtb_cxs_exten(void);
extern	int			create_ide(void);
extern	int			check_header(char*, char*, int, int);
extern	int			compare_command(char*);
extern	int			parse_argument(int, char*);
extern	int			fill_in_struct_data(int, char*);
extern	int			parse_file(char*, int, int);
extern	int			write_message(void);
extern	int			read_message(void);
extern	void		clean_up(void);
extern	int			main(int, char**);
extern	int			checkintvalues(void);
extern	int			fill_in_data(int, int, char**);
extern	int			checkargs(int, char**, int);
extern	int			newm910way(int, char**);
extern	int			oldm910way(int, char**);
extern	int			terminate_process(void);
extern	int			find_process(char*);
extern	int			perform_concurrent_op(void);
extern	int			terminate_process_on_startup(void);
extern	int			start_proc(void);
extern	char		*exten_routine(char*, char*, char*);
extern	int			convert_files_set_dir(void);
extern	int			change_dir(int);
extern	int			set_dir_file(void);

