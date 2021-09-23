/****************************************************************************
 *	File:	fhdb.h															*
 *																			*
 *	Creation Date:	30 June 2008											*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0.0.0		Complete rebuild of fhdb nam, visual dll software no	*
 *					longer available.  Include the dll code into the nam	*
 *					program.												*
 *																			*
 ***************************************************************************/

#define	NAM_ERROR	(-1)

#ifndef	TRUE
	#define	TRUE			1
#endif

#ifndef	FALSE
	#define	FALSE			0
#endif

#define	NAM_MAX_PATH _MAX_PATH
#define	MAX_STRING 1024

extern int		NotAtlas;
extern int		DE_BUG;
extern FILE		*debugfp;

#define NO_PRE_DEFINE	0
#define	SUCCESS			0
#define	TMP_FILE		1
#define	PCOF_TMP		2
#define	NO_FILE			3

#define	ARGC_MIN		16
#define	ARGC_MAX		18

#define	STARTTIME		2
#define	STOPTIME		3
#define	ERONUMBER		4
#define	TPCCNUMBER		5
#define	UUTSERIALNO		6
#define	UUTREVNUMBER	7
#define	IDSERIALNUMBER	8
#define	TESTSTATUS		9
#define	FAILEDSTEP		10
#define	FAULTMESSAGE	11
#define	MEASUREVALUE	12
#define	DIMENSION		13
#define	UPPERLIMIT		14
#define	LOWERLIMIT		15
#define	OPERCOMMENT		16

#define	RETURN_STATUS	17
#define	THE_RETURN		1

#define	NEWLINESPACE	1
#define	NINESPACES		2
#define	DETECTPOINTS	3
#define	FOURSPACES		4

#define	ARGNUM				801
#define	DATACHAR			802
#define	DATAINT				803
#define	DATAREAL			804
#define	DATABOOL			805
#define	UNKOPT				806
#define	FILEOPEN			807
#define	FILEREAD			808
#define	FILEWRITE			809

#define	TERMINATOR			1
#define	TIME_STAMP_SIZE		15
#define	ERO_SIZE			5
#define	TPCCN_SIZE			25
#define	UUT_SERIAL_SIZE		15
#define	REVISION_SIZE		10
#define	ID_SERIAL_SIZE		10
#define	FAILED_STEP_SIZE	10
#define	FAULT_CALLOUT_SIZE	5000
#define	DIMENSION_SIZE		12
#define	OP_COMMENT_SIZE		256
#define	TEMPERATURE_SIZE	12
#define	COMPUTER_NAME_SIZE	8
#define	COMPUTER_NAME_SIZE_ARRAY	10

#define	LOGLOCATION		"C:\\APS\\DATA\\"
#define	DTBFAULTFILE	"m910nam.ide"
#define	DEBUGFILENAME	"fhdbdebug"
#define DEBUGIT			"debugit_fhdb"
#define	SUFFIX			".TMP"
#define	SHELLPCOF		"PCOF_TMP.DIA"
#define	SHELLTMPPCOF	"PCOF_TMP.IDE"

#define	APPNAME			"System Monitor"
#define	KEYNAME			"PRI_TEMP"

typedef	struct{
	int			functionStatus;
	int			returnValue;
	char		dataBaseFile[NAM_MAX_PATH];
	char		fileLocation[NAM_MAX_PATH];
	char		logLocation[NAM_MAX_PATH];
	char		logFile[NAM_MAX_PATH];
	char		debugOption[NAM_MAX_PATH];
}FHDB_INFO;

extern FHDB_INFO		fhdbInfo;

typedef struct {
	short			year;
	unsigned short	month;
	unsigned short	day;
	unsigned short	hour;
	unsigned short	min;
	unsigned short	sec;
}TIME;

typedef	struct {
	TIME	dateTimeStart;
	TIME	dateTimeStop;
	char	eroNo[ERO_SIZE + TERMINATOR];
	char	tpsProgCntrlNo[TPCCN_SIZE + TERMINATOR];
	char	uutSerialNo[UUT_SERIAL_SIZE + TERMINATOR];
	char	uutRev[REVISION_SIZE + TERMINATOR];
	char	idSerialNo[ID_SERIAL_SIZE + TERMINATOR];
	short	testStatus;
	char	failedStep[FAILED_STEP_SIZE + TERMINATOR];
	char	faultCallout[FAULT_CALLOUT_SIZE + TERMINATOR];
	double	measureValue;
	char	dimension[DIMENSION_SIZE + TERMINATOR];
	double	upperLimit;
	double	lowerLimit;
	char	operatorComments[OP_COMMENT_SIZE + TERMINATOR];
	double	temperature;
}DATACOLLECTION;

extern	DATACOLLECTION	dataCollectionfp;

extern void	checkFor(int, int*, int*, char*, char*);
extern int	checkForDigitalFaultCallout(char*);
extern int	CheckDecimalValues(int ArgcValue, char *argv[]);
extern int	CheckIntValues(int ArgcValue, char *argv[]);
extern int	CheckBoolValues(int ArgcValue, char *argv[]);
extern int	CheckCharValues(int ArgcValue, char *argv[]);
extern void	closedb(void);
extern void ConvertCRLF(char *stringToConvert);
extern void	dodebug(int, char*, char*, ...);
extern int	dtbFaultCallout(char*);
extern int	extractFaultInfo(char*);
extern int	fhdbNamMain(int, char**);
extern int	fillInDataBase(void);
extern void freeItem(char*);
extern int	getBigMessage(char*);
extern int	getTemperature(void);
extern void	initVariables(void);
extern int	opendb(void);
extern int	parseArguments(int, char**);
extern int	parseCommandLine(int, char**);
extern int	setCharString(char*, int);
extern int	setTimeValue(char*, int);
extern void	setValue(double, int);
