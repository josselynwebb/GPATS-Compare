/////////////////////////////////////////////////////////////////////////////
//	File:	setAddress.cpp													/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
// 		1.0.0.0	Assigned it a version number.  The function call was added	/
// 				to allow for a non-privilege user (operator) to set the ip	/
// 				address of the controller.									/
//		1.1.0.0	setAddress()												/
//				Corrected the function header info. Deletede the variables	/
//				finalSet, set, and reset, added new variable SetIP. This was/
//				required because the function now performs more capabilities/
//				plus depending on what is being set the setting is different/
//				Changed the if test to check for RESET_IP, this is where the/
//				different settings are done. Changed the fprintf to			/
//				accommodate the above changes.								/
//				DoResetIP()													/
//				This is a new function, it will do the setting of variable	/
//				NetworkPort.												/
//				DoSetIP()													/
//				This is a new function, it will do the setting of variable	/
//				NetworkPort.												/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include "Version.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <malloc.h>
//#include <iphlpapi.h>
//#include <WinSock2.h>
#include <direct.h>
#pragma warning (disable : 4035 4068)
#include <windows.h>
#pragma warning (default : 4035 4068)
#include <process.h>
#include "gpnam.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// setAddress:	This function call will first set tmpBuf to the location	/
// 					of c:/aps/data/address.bat were a .bat file will be		/
// 					created to allow for the setting of the address that	/
// 					was passed on the command line.  Then create the		/
// 					executable command that will be passed to the system	/
// 					api function.  Then the buffers will be flushed to		/
// 					ensure that the file .bat is written before it is used	/
// 					by the system call.  Next find the current working		/
// 					directory then change to the directory of the PSEXECDIR./
// 					Next call the system api to execute the ip address		/
// 					change.  Finallyreturn back to the former working		/
//					directory.												/
//																			/
// Parameters:																/
//		setOrReset	This int is set to either one of two values				/
//					4 - IP_ADDRESS											/
//					5 - RESET_IP											/
//		NetworkPort	This int is set to one of the three ethernet ports		/
//					0 - LOCALNETWORK										/
//					1 - GIGABIT1											/
//					2 - GIGABIT2	
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int setAddress(int setOrReset, int NetworkPort)
{

	char	tmpBuf[GP_MAX_PATH];
	char	commandBuf[128];
	char	savedDir[GP_MAX_PATH];
	char	SetIP[128];
	FILE	*addrFilePointer;


	fflush(NULL);

	sprintf(tmpBuf, "%s%s", gp_info.log_location, ADDRESS_FILE);


	if ((addrFilePointer = fopen(tmpBuf, "w")) == NULL) {

		dodebug(FILE_OPEN_ERROR, "clean_up()", NULL);
		gp_info.return_value = FILE_OPEN_ERROR;
		return(GP_ERROR);
	}

	if (setOrReset == RESET_IP) {

		
		dodebug(0, "setAddress()", "In the if set or reset", (char *)NULL);
		sprintf(gp_info.address, "%s", NetworkPort == LOCALNETWORK ? "" :
				NetworkPort == GIGABIT1	? GIGABIT1ADDR : NetworkPort == GIGABIT2 ? GIGABIT2ADDR :
				GIGABIT4ADDR);

		sprintf(gp_info.Gateway, "%s", NetworkPort == LOCALNETWORK ? "" :
			NetworkPort == GIGABIT1	? GIGABIT1GATE : NetworkPort == GIGABIT2 ? GIGABIT2GATE: GIGABIT4GATE);

		if (NetworkPort != LOCALNETWORK) {
			strcat(gp_info.Gateway, " 1");
		}

		sprintf(gp_info.mask, "%s", NetworkPort == LOCALNETWORK ? "" : DEFAULTMASK);

		setOrReset = NetworkPort == LOCALNETWORK ? RESET_IP : IP_ADDRESS;
	}
	
	dodebug(0, "setAddress()", "gp_info.address is %s",gp_info.address, (char *)NULL);
	sprintf(SetIP, "set address \"%s\" %s %s %s %s", gp_info.NetworkName,
				setOrReset == IP_ADDRESS ? "static" : "dhcp", gp_info.address,
				gp_info.mask, gp_info.Gateway);
	
	dodebug(0, "setAddress()", "SETIP is %s",SetIP, (char *)NULL);
	//fprintf(addrFilePointer, "netsh interface ip %s \r\n pause", SetIP);
	fprintf(addrFilePointer, "netsh interface ip %s", SetIP);
	fclose(addrFilePointer);

	ZeroMemory(commandBuf, sizeof(commandBuf));
	//sprintf(commandBuf, "Powershell Start-Process \" %s \"",tmpBuf);
	sprintf(commandBuf, "Powershell Start-Process \" %s \" -Verb \"runas\"",tmpBuf);
	dodebug(0, "setAddress()", "Tmp buff is %s",tmpBuf, (char *)NULL);
	dodebug(0, "setAddress()", "Command buff is %s",commandBuf, (char *)NULL);

	fflush(NULL);

	_getcwd(savedDir, GP_MAX_PATH);

	ZeroMemory(tmpBuf, sizeof(tmpBuf));
	sprintf(tmpBuf, "%s", PSEXECDIR);

	_chdir(tmpBuf);

	if (system(commandBuf)) {
		dodebug(0, "setAddress()", "Failed to perform required address setting", (char *)NULL);
		gp_info.return_value = GP_ERROR;
	}
	/*if (system("runas /user:Administrator \"c:\aps\data\address.bat\"" )) {
		dodebug(0, "setAddress()", "Failed to perform required address setting", (char *)NULL);
		gp_info.return_value = GP_ERROR;
	}*/
	_chdir(savedDir);

	return(gp_info.return_value);
}


/////////////////////////////////////////////////////////////////////////////
// DoResetIP:	This program will find out what netwok port is being reset	/
//					If no port is stated default to Local area connection	/
//					used in legacy code otherwise should be stated.			/
//																			/
// Parameters:																/
//		argc:			The number of arguments passed on the command line. /
//		argv:			The arguments that were passed on the command line	/
//		NetworkPort:	Pointer for the numerical value of the port selected/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
/////////////////////////////////////////////////////////////////////////////

int DoResetIP(int argc, char *argv[], int *NetworkPort)
{

	if (argc > (OPTION + ATLAS + 1)) {
		if (setArgumentValue(ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR, argv[RESETPORT + ATLAS],
							  gp_info.NetworkName, sizeof(gp_info.NetworkName), NULL)) {

			dodebug(0, "gpnam_main()", "Failed to get option value", (char *)NULL);
			gp_info.return_value = GP_ERROR;
			return(gp_info.return_value);
		}
	}
	else {
		*NetworkPort = LOCALNETWORK;
		sprintf(gp_info.NetworkName, "%s", "Local Area Connection");
		dodebug(0, "DoResetIP()", "Network name is %s ",gp_info.NetworkName, (char *)NULL);
	}

	if (!_stricmp("Local Area Connection", gp_info.NetworkName)) {
		*NetworkPort = LOCALNETWORK;
	}
	else if (!_stricmp("GIGABIT1", gp_info.NetworkName)) {
		*NetworkPort = GIGABIT1;
	}
	else if (!_stricmp("GIGABIT2", gp_info.NetworkName)) {
		*NetworkPort = GIGABIT2;
	}
	else if (!_stricmp("GIGABIT4", gp_info.NetworkName)) {
		*NetworkPort = GIGABIT4;
	}
	else {
		dodebug(0, "gpnam_main()", "Improper Port Name %s", gp_info.NetworkName, (char *)NULL);
		gp_info.return_value = GP_ERROR;
	}

	return(gp_info.return_value);
}

/////////////////////////////////////////////////////////////////////////////
// DoSetIP:	This program will find out what netwok port is being set		/
//					If no port is stated default to Local area connection	/
//					used in legacy code otherwise should be stated.			/
//																			/
// Parameters:																/
//		argc:			The number of arguments passed on the command line. /
//		argv:			The arguments that were passed on the command line	/
//		NetworkPort:	Pointer for the numerical value of the port selected/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
/////////////////////////////////////////////////////////////////////////////

int DoSetIP(int argc, char *argv[], int *NetworkPort)
{

	//int	TmpValue = ATLAS == 1 ? 0 : 1;
	
	dodebug(0, "DoSetIP()", "Argv is [%s] [%s] [%s] [%s] [%s] [%s]", argv[0],argv[1],argv[2],argv[3],argv[4],argv[5]);
	dodebug(0, "DoSetIP()", "here in DoSetIP argc = %d", argc, (char*)NULL);
	fflush(NULL);

	dodebug(0, "DoSetIP()", "ATLAS is %d", ATLAS, (char*) NULL);
	if (setArgumentValue(ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR, 
						  argv[ADDRESS + ATLAS], gp_info.address, sizeof(gp_info.address), NULL)) {
	
		dodebug(0, "DoSetIP()", "Failed to get option value", (char *)NULL);
		gp_info.return_value = GP_ERROR;
		return(gp_info.return_value);
	}

	dodebug(0, "DoSetIP()", "getting mask value", (char*)NULL);
	fflush(NULL);

	if (setArgumentValue(ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR, 
						  argv[MASK + ATLAS], gp_info.mask, sizeof(gp_info.mask), NULL)) {
	
		dodebug(0, "gpnam_main()", "Failed to get option value", (char *)NULL);
		gp_info.return_value = GP_ERROR;
		return(gp_info.return_value);
	}

	dodebug(0, "DoSetIP()", "argc = %d MASK+ATLAS = %d", argc, ATLAS+MASK, (char*)NULL);
	fflush(NULL);

	if (argc > (MASK + ATLAS + 1)) {

		if (setArgumentValue(ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR, argv[GATEWAY + ATLAS],
							  gp_info.Gateway, sizeof(gp_info.Gateway), NULL)) {
			dodebug(0, "gpnam_main()", "Failed to get option value", (char *)NULL);
			gp_info.return_value = GP_ERROR;
			return(gp_info.return_value);
		}
		else {
			strcat(gp_info.Gateway, " 1");
		}
		
		if (setArgumentValue(ATLAS == 1 ? ATLAS_CHAR : CMD_LINE_CHAR, argv[PORTNAME + ATLAS],
							  gp_info.NetworkName, sizeof(gp_info.NetworkName), NULL)) {
			dodebug(0, "gpnam_main()", "Failed to get option value", (char *)NULL);
			gp_info.return_value = GP_ERROR;
			return(gp_info.return_value);
		}

		if (!_stricmp("Local Area Connection", gp_info.NetworkName)) {
			*NetworkPort = LOCALNETWORK;
		}
		else if (!_stricmp("GIGABIT1", gp_info.NetworkName)) {
			*NetworkPort = GIGABIT1;
		}
		else if (!_stricmp("GIGABIT2", gp_info.NetworkName)) {
			*NetworkPort = GIGABIT2;
		}
		else if (!_stricmp("GIGABIT4", gp_info.NetworkName)) {
			*NetworkPort = GIGABIT4;
		}
		else {
			dodebug(0, "gpnam_main()", "Improper Port Name %s", gp_info.NetworkName, (char *)NULL);
			gp_info.return_value = GP_ERROR;
		}
	}
	else {

		*NetworkPort = LOCALNETWORK;
		sprintf(gp_info.NetworkName, "%s", "Local Area Connection");
		sprintf(gp_info.Gateway, "%s", "");
	}

	return(gp_info.return_value);
}

