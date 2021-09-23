/////////////////////////////////////////////////////////////////////////////
//	File:	gpnam_main.cpp													/
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
//		1.0.1.0	Changed the exit call at the end of the function to a return/
//				call.  The exit call is improper and a return is the correct/
//				way to return from ther function.							/
// 		1.1.0.0	Changed the way the gpnam_main function works.  First the	/
// 				function will find out if it is being called by an ATLAS	/
// 				program or from another executable program.  It will do this/
// 				by checking to see if the .tmp is present and setting the	/
// 				flag ATLAS depending if the file is present or not.  Added	/
// 				function call setArgumentValue to check for proper setting	/
// 				of the command line variables if either an ATLAS program or	/
// 				a non-ATLAS program.  Added an element to the case statement/
// 				IP_ADDRESS  the element will check to ensure an address and	/
// 				a mask was sent and then call the function setAddress.		/
// 				Added the function call setArgumentValue to simplify the	/
// 				checking for proper types of variables being passed to the	/
// 				gpnam on the command line of an ATLAS or a non-ATLAS program/
//		1.2.0.0	gpnam_main()												/
//				Added the variable NetworkPort - this is set to the name	/
//				passed to program. Remove the error checking for the number	/
//				arguments being passed, now the number can be unknown. Using/
//				the new reset function for the case statement RESET_IP.		/
//				Using the new set ip function for the case statement		/
//				IP_ADDRESS.													/
//				setArgumentValue()											/
//				Added the element StringSize to the passed variables, this	/
//				is used to prevent buffer overflow. Changed the sprintf to	/
//				_snprintf to prevent buffer overflow. Deleted one dodebug	/
//				statement in the case element CMD_LINE_INT.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <direct.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <ctype.h>
#pragma warning (disable : 4035 4068)
#include <windows.h>
#pragma comment(lib, "user32.lib")
#pragma warning (default : 4035 4068)
#include <process.h>
#include "Constants.h"
#include "pipe.h"
#include "nam.h"
#include "stdafx.h"
#include "gpnam.h"
#include "gpconcur.h"


/////////////////////////////////////////////////////////////////////////////
//		External Variables and Routines										/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////
#define MAXIMUM_STRING_LENGTH 2048

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

PIPE_INFO		PipeInfo;
CONCUR_INFO		concur_info;
int x_Integer;
char x_String[MAXIMUM_STRING_LENGTH];


/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// gpnam_main:	This program will check the variables that were passed to	/
//					it and by these variables determine which program to	/
//					execute and how it is executed.							/
//																			/
// Parameters:																/
//		argc:		The # of arguments passed to main.						/
//		argv:		Character list of the arguments passed to main.			/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		OSRSERR:	(-1)	= failure of a required task.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int gpnam_main (int argc, char *argv[])
{

	int				optionToPerform, NetworkPort;
	char			TempFileName[GP_MAX_PATH];
	struct _stat	statBuf;

//
// First find out if program is being run from ATLAS or not
//

	ZeroMemory(TempFileName, sizeof(TempFileName));
	sprintf(TempFileName, "%s%s", argv[TMP_FILE], SUFFIX);
	ATLAS = _stat(TempFileName, &statBuf) == GP_ERROR ? 0 : 1;
	dodebug(0, "gpnam_main()", "ATLAS is %d", ATLAS, (char*)NULL);
	dodebug(0, "gpnam_main()", "argc is %d", argc, (char*)NULL);
//
// Initialize the communication library if an ATLAS program is executing this
// program otherwise do nothing here.
//
	
	//MessageBox(NULL, "GP NAM", "Debug", MB_OK);
	if (ATLAS) {
		vmOpen(TempFileName);

		/*if(vmOpen(TempFileName) < 0)
			return -1;*/
	}

//
// Initialize some variables.
//

	PipeInfo.MessageSize	= MAX_MSG_SIZE + 24;
	PipeInfo.Pipehd			= NULL;
	int dataType;

	if(ATLAS == 1)
	{
		dataType = ATLAS_INT;
	}
	else
	{
		dataType= CMD_LINE_INT;
	}

	dodebug(0, "gpnam main()", "datatype is %d", dataType, (char*)NULL);

	if (setArgumentValue (dataType, argv[OPTION + ATLAS], NULL, 0, &optionToPerform)) {
		dodebug(0, "gpnam_main()", "Failed to get option value", (char *)NULL);
		
		if (ATLAS) {
			vmClose();
			/*if(vmClose() < 0)
				return -1;*/
		}
		
		return(-1);
	}
	
	dodebug(0, "gpnam_main()", "option value is [%d]",optionToPerform, (char *)NULL);
	get_drive_type();

	switch(optionToPerform) {

		case CLEAN_UP:

			if (DriveType != DRIVE_CDROM) {
				break;
			}

			break;
		case PRINT_FAULT_FILE:

			if (!ATLAS) {
				break;
			}
			//
			// First let's go see if the program gpconcur.exe is running.  If it is
			// we will set the bit concur_info.concurrent_running to TRUE for later use.
			//

			sprintf(concur_info.process_name, "%s", PROCESS_NAME);

			if ((find_process(concur_info.process_name)) == GP_ERROR) {
				dodebug(0, "gpmain_main()",
						"Couldn't access the running process at all get sysadmin help");
				break;
			}

			//
			// Here I will check to see if the gpconcur.exe is running.  It shouldn't be unless
			// there something wrong in you know what land.
			//

			if (concur_info.concurrent_running == TRUE) {

				if (terminate_process() == GP_ERROR) {
					dodebug(0, "gpmain_main()",
							"gpconcur running and will not terminate, get sysadmin help");
					gp_info.return_value = GP_ERROR;
					break;
				}
			}

			fflush(NULL);
			dodebug(0, "gpnam_main()", "calling execute_gpconcur()", (char*)NULL);
			execute_gpconcur(optionToPerform);
					
			if (gp_info.return_value == SUCCESS && optionToPerform == PRINT_FAULT_FILE) {

				char	argument[8];

				fflush(NULL);

				sprintf(argument, "%s", "PrintIt");

				sprintf(gp_info.PrintProg, "%s%s", FAULTFILEEXEDIR, FAULTFILEEXECUTABLE);

				if ((_spawnl(_P_WAIT, gp_info.PrintProg, argument, argument, NULL)) != SUCCESS) {
					dodebug(FAULT_FILE_ERROR, "print_fault_file()", NULL);
					gp_info.return_value = GP_ERROR;
				}
			}

			break;

		case INSERT_INTO_FAULT_FILE:

			if (ATLAS) {
				insert_into_fault_file();
			}

			break;

		case RESET_IP:

			if(DoResetIP(argc, argv, &NetworkPort) != GP_ERROR)
			{
				setAddress(optionToPerform, NetworkPort);
			}

			break;

		case IP_ADDRESS:

			fflush(NULL);

			if(DoSetIP(argc, argv, &NetworkPort) != GP_ERROR)
			{
				setAddress(optionToPerform, NetworkPort);
			}

			break;

		case INSERT_ADDITONAL_INFO:

			InsertAdditionalInfo(argc, argv);

			break;

		case PING_IP:
			
			dodebug(0, "main()", "Case PING IP", (char *)NULL);

			DoPing(argc, argv);
			
			dodebug(0, "main()", "After DO PING IP", (char *)NULL);
			break;

		case COMPUTER_NAME:

			GetSystemName(argv);

			break;

		case CURRENT_WORKING_DIR:

			GetCurrentWorkingDir(argv);

			break;

		default:

			dodebug(OPTION_SENT, "main()", (char *)NULL, (char *)NULL);
			gp_info.return_value = OPTION_SENT;

			break;
	}
	
	dodebug(0, "main()", "ATLAS is %d  before vmclose",ATLAS, (char *)NULL);
	if (ATLAS) {
		
			dodebug(0, "main()", "calling vmclose", (char *)NULL);
		int closeResult = vmClose();
		dodebug(0, "main()", "After vmclose return is %d",closeResult, (char *)NULL);
		/*if(vmClose() < 0)
				return -1;*/
	}
	
	dodebug(0, "main()", "After vmclose return value is %d ",gp_info.return_value, (char *)NULL);


	return((gp_info.return_value == SUCCESS) ? 0 : -1);

}


/////////////////////////////////////////////////////////////////////////////
// setArgumentValue:	This program will check the variables that were		/
//						passed to it and by these variables determine which	/
//						program to execute and how it is executed.			/
//																			/
// Parameters:																/
// 		dataType:		This variable is used in the switch statement to	/
// 						determine which case element to use.				/
// 		argument:		This is the argv[*] element that is being checked	/
// 						for proper use.										/
// 		stringToFill:	This is the string variable that will be set from	/
// 						the argv[*] variable after checking to ensure it is	/
// 						a char[]. This may be NULL if not used.				/
//		StringSize		This is the size of the stringToFill variable, it is/
//						used in the _snprintf to limit to the size of the	/
//						variable buffer size.
// 		valueToFill:	This the numerical variable that will be set from	/
// 						the argv[*] variable after checking to ensure it	/
// 						equates to a number,  this may be NULL if not used.	/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		OSRSERR:	(-1)	= failure of a required task.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int setArgumentValue (int dataType, char *argument, char *stringToFill, int StringSize,int *valueToFill)
{

	long	xad;
	dodebug(0, "setArgumentValue()", "Entering function datatype is %d", dataType,(char*)NULL);
	
	/*dodebug(0, "setArgumentValue()", "dataType[%d] argument[%s][%s][%s][%s][%s][%s][%s][%s][%s][%s]",
		argument[0],argument[1],argument[2],argument[3],argument[4],argument[5],argument[6],argument[7],argument[8],argument[9], (char*)NULL);*/
	switch(dataType) {

		case ATLAS_INT:

			xad = atol(argument);
			//mov_dfv(&x, xad);
			
			dodebug(0, "setArgumentValue()", "xad is %d", xad, (char*)NULL);
			if (vmGetDataType(xad) != ITYPE) {
				//mov_dfv(&x, xad);
				dodebug(INT_TYPE, "setArgumentValue()", (char *)NULL, (char *)NULL);
				gp_info.return_value = INT_TYPE;
				return(GP_ERROR);	
			}
			x_Integer = vmGetInteger(xad);
			dodebug(0, "setArgumentValue()", "x_Integer is %d", x_Integer, (char*)NULL);
			*valueToFill = x_Integer;	
			dodebug(0, "setArgumentValue()", "value to fill is %d", valueToFill, (char*)NULL);

			break;

		case ATLAS_CHAR:

			xad = atol(argument);
			//mov_dfv(&x, xad);
			
			dodebug(0, "setArgumentValue()", "xad is %d", xad, (char*)NULL);
			if (vmGetDataType(xad) != TTYPE) {
				dodebug(TEXT_TYPE, "setArgumentValue()", (char *)NULL, (char *)NULL);
				gp_info.return_value = TEXT_TYPE;
				return(GP_ERROR);
			}
			
			vmGetText(xad, x_String, MAXIMUM_STRING_LENGTH);
			_snprintf(stringToFill, StringSize - 1, "%s", x_String);
			stringToFill[StringSize - 1] = '\0';

			break;

		case CMD_LINE_INT:

			if ((*valueToFill = atoi(argument)) == 0) {

				dodebug(INT_TYPE, "setArgumentValue()", (char *)NULL, (char *)NULL);
				dodebug(0, "setArgumentValue()", "argument = %s", argument);
				gp_info.return_value = INT_TYPE;
				return(GP_ERROR);
			}

			break;

		case CMD_LINE_CHAR:

			_snprintf(stringToFill, StringSize - 1, "%s", argument);
			stringToFill[StringSize - 1] = '\0';

			break;

		default:

			dodebug(0, "setArgumentValue()", "Invalid dataType sent: %d", dataType, (char *)NULL);
			return(GP_ERROR);

			break;
	}

	return(SUCCESS);
}


/////////////////////////////////////////////////////////////////////////////
// returnArgumentValue:	This program will check the variables that were		/
//						passed to it and by these variables determine which	/
//						program to execute and how it is executed.			/
//																			/
// Parameters:																/
// 		dataType:		This variable is used in the switch statement to	/
// 						determine which case element to use.				/
// 		argument:		This is the argv[*] element that is being checked	/
// 						for proper use.										/
// 		stringToFill:	This is the string variable that will be set from	/
// 						the argv[*] variable after checking to ensure it is	/
// 						a char[]. This may be NULL if not used.				/
//		StringSize		This is the size of the stringToFill variable, it is/
//						used in the _snprintf to limit to the size of the	/
//						variable buffer size.
// 		valueToFill:	This the numerical variable that will be set from	/
// 						the argv[*] variable after checking to ensure it	/
// 						equates to a number,  this may be NULL if not used.	/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		OSRSERR:	(-1)	= failure of a required task.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int returnArgumentValue (int dataType, char *argument, char *stringToFill, int StringSize, int valueToFill, FILE *OutPutfp)
{

	long	xad;
	dodebug(0, "returnArgumentValue()", "The datatype is %d", dataType, (char*)NULL);
	switch(dataType) {

		case ATLAS_INT:

			xad = atol(argument);
			//mov_dfv(&x, xad);

			dodebug(0, "returnArgumentValue()", "The argument is %s  The value is %d", argument, valueToFill, (char *)NULL);

			if (vmGetDataType(xad) != ITYPE) {

				dodebug(INT_TYPE, "returnArgumentValue()", (char *)NULL, (char *)NULL);
				gp_info.return_value = INT_TYPE;
				return(GP_ERROR);
			}
			vmSetInteger(xad, valueToFill);
			x_Integer = valueToFill;
			//mov_dtv(xad, &x);

			break;

		case ATLAS_CHAR:

			xad = atol(argument);
			//mov_dfv(&x, xad);

			if (vmGetDataType(xad) != TTYPE) {

				dodebug(TEXT_TYPE, "returnArgumentValue()", (char *)NULL, (char *)NULL);
				gp_info.return_value = TEXT_TYPE;
				return(GP_ERROR);
			}
		
			_snprintf(x_String, StringSize, "%s", stringToFill);
			x_String[StringSize] = '\0';
			//mov_dtv(xad, &x);
			vmSetText(xad, x_String);

			break;

		case CMD_LINE_INT:

			fprintf(OutPutfp, "Integer = %d\n", valueToFill);

			break;

		case CMD_LINE_CHAR:

			fprintf(OutPutfp, "String size = %d\n", StringSize);
			fprintf(OutPutfp, "String = %s\n", stringToFill);

			break;

		default:

			dodebug(0, "returnArgumentValue()", "Invalid dataType sent: %d", dataType, (char *)NULL);
			return(GP_ERROR);

			break;
	}

	return(SUCCESS);
}