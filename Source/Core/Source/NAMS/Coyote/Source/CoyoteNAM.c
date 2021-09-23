///***************************************************************************
//**							CoyoteNAM 									  **
//****************************************************************************
//**  CoyoteNAM.c															  **
//**																		  **
//**	Written By:     Robert Giumarra  03/09/10							  **
//**																		  **	
//**  Version 1.0.0.0														  **
//		2.1.0.0		Added hnges from VIPERT 1.3.2.0							/
//**																		  **
//**																		  **
//****************************************************************************
//****************************************************************************
//   Arguments - "STRINGS MUST BE PASSED IN QUOTES IF THEY CONTAIN A SPACE."
//****************************************************************************
//***     This NAM will launch the Coyote software						  **
//***		to display the VEO-2 internal camera.							  **
//****************************************************************************
//
//------ OUT ------
//RESULT		= argv[3]	Error Code 
//RESULTSTR	= argv[4]   Error Description
//
//****************************************************************************
//*								Include Files							   *
//****************************************************************************/
//
//#include		<stdio.h>
//#include		<Windows.h>
//#include		<sys/types.h>
//#include		<sys/stat.h>
//
//#include		"nam.h"
//
///***************************************************************************
//*									Globals		      			           *
//***************************************************************************/
//
//
///* Program Globals */
//#define		MAX_STRING_LENGTH 257
//char		TempFileName[40];	     /* Temporary File Name for ATLAS	 */
//int			Result;
//long	    ResultAddr;		         /* Arg "Result" Address			 */
//long		ResultStrAddr;           /* Arg "ResultStr" Address          */
//char		ResultStr[MAX_STRING_LENGTH];
//HANDLE		m_handle;				 /* handle for veo2 dll */
//
//
//typedef void (CALLBACK* TN_SET_CAMERA_POWER_INITIATE)(int);
//TN_SET_CAMERA_POWER_INITIATE SET_CAMERA_POWER_INITIATE;
//
//typedef void (CALLBACK* TN_SET_CAMERA_POWER_FETCH)(int*);
//TN_SET_CAMERA_POWER_FETCH SET_CAMERA_POWER_FETCH;
//
//typedef void (CALLBACK* TN_SET_SENSOR_STAGE_LASER_INITIATE)(int);
//TN_SET_SENSOR_STAGE_LASER_INITIATE SET_SENSOR_STAGE_LASER_INITIATE;
//
//typedef void (CALLBACK* TN_SET_SENSOR_STAGE_LASER_FETCH)(int*);
//TN_SET_SENSOR_STAGE_LASER_FETCH SET_SENSOR_STAGE_LASER_FETCH;
//
//typedef void (CALLBACK* TN_IRWIN_SHUTDOWN)();
//TN_IRWIN_SHUTDOWN IRWIN_SHUTDOWN;
//
//
///***************************************************************************
//*								Constants			     		           *
//***************************************************************************/
//
//#define SUFFIX      ".TMP"
//#define DISPLAYFILE "C:\\Program Files (x86)\\iPORT PT1000\\Binaries\\Coyote.exe"
//
///***************************************************************************
//*	                     Error Codes and Definitions				       *
//***************************************************************************/
//
//// ERROR CODE: 0
//#define SUCCESS          "Successful"
////ERROR CODE: -100
//#define INVALID_PARAM    "ERROR - Invalid number of parameters: "
////ERROR CODE: -101
//#define INVALID_EXE      "ERROR - Unable to find the Coyote executable: "
////ERROR CODE: -102
//#define POWER_BAD		 "ERROR - Unable to set internal camera power on: "
////ERROR CODE: -103
//#define STAGE_BAD		 "ERROR - Unable to set sensor stage to camera: "
//
//
///********************************************************************************
//*									Modules							            *
//********************************************************************************/
//
//void StartDisplay()
///********************************************************************************
// **     Call the Video Display Coyote.exe.  ATLAS Program execution will stop  **
// **     until the Video Display application is terminated.					   **                                **
// ********************************************************************************/
//
//{
//    STARTUPINFO si;				/* Points to the STARTUPINFO Struct */
//    PROCESS_INFORMATION pi;		/* Points to the PROCESS_INFORMATION Struct */
//
//    ZeroMemory( &si, sizeof(si) );
//    si.cb = sizeof(si);
//    ZeroMemory( &pi, sizeof(pi) );
//
//    // Start the process. 
//    if( !CreateProcess ( 
//		DISPLAYFILE,	  // Executable 
//        NULL,			  // Command line. 
//        NULL,             // Process handle not inheritable. 
//        NULL,             // Thread handle not inheritable. 
//        FALSE,            // Set handle inheritance to FALSE. 
//        0,                // No creation flags. 
//        NULL,             // Use parent's environment block. 
//        NULL,             // Use parent's starting directory. 
//        &si,              // Pointer to STARTUPINFO structure.
//        &pi )             // Pointer to PROCESS_INFORMATION structure.
//    ) 
//	/*NOTE: "WaitForSingleObject" always seems to fail without this printf statement.*/ 
//	printf ("WaitForSingleObject...");  //For Debugging
//    // Wait until child process exits.
//    WaitForSingleObject( pi.hProcess, INFINITE );
//
//    // Close process and thread handles. 
//    CloseHandle( pi.hProcess );
//    CloseHandle( pi.hThread );
//}
//
//
//int file_exists(char *file_name)
///*******************************************************************************
//**                           Check for File Exists                            **
//********************************************************************************/
//{
//	struct _stat file_stat;
//	return( (_stat(file_name, &file_stat) == 0) ? 1:0);
//}
//
//
//void CloseNAM(long CloseStatus, char ErrorStr[])
///*******************************************************************************
//**                  Report Error Results to ATLAS                             **
//********************************************************************************/
//{
//	/* Return Results and Result String to ATLAS */
//	/* ref TYX PAWS User Guide Vol 1 Sec 5 */
//
//	vmSetInteger(ResultAddr, CloseStatus);
//	vmSetText(ResultStrAddr, ErrorStr);
//
//
//	/* Close virtual memory file */
//	vmClose();
//	printf ("Close NAM.  Error String = %s", ErrorStr);
//	IRWIN_SHUTDOWN();
//	FreeLibrary(m_handle);
//
//	//Exit NAM
//	exit(CloseStatus == 0 ? 0 : -1);
//}
//
//
///*******************************************************************************/
///**************        Main Program Execution begins here         **************/
///*******************************************************************************/
//
//int main (int argc, char *argv[])
//{
//	
///*******************************************************************************
// ****	                     Delcare Variables 	           	               *****
// *******************************************************************************/
//
//	/* Declare and Initialize variables  */
//	char    sExe[257]			=   "";     /* Xml camera setup file name and path                 */
//	long    nErrCode			=	 0;		/* Error Code returned (Numerical Value)       */
//	char    sErrReturn[257]	    =	"";		/* Error Code String to pass to ATLAS          */
//	char    sErrString[257]     =   "";     /* Used to Build Error String.                 */
//	short   x					=	 0;
//	short   y					=	 0;
//	char	LibName[50]			=	"C:\\irwin2001\\veo2.dll"; //VEO2 library
//	int		veo2_val			=	 0;
//	int		POWER_ON			= 	 1;
//	int		POWER_OFF			=	 0;
///****************************************************************/
///****	    Initialize the communication library			 ****/
///****************************************************************/
//
//	/* argv[0] = The calling program's name       */
//	/* argv[1] = The name of the ATLAS TEMP file  */
//
//	/* Define ATLAS temp file name and open it */
//	strcpy(TempFileName, argv[1]);
//	strcat(TempFileName, SUFFIX);
//	
//	if(vmOpen(TempFileName) < 0)
//		return -1;
//
////	Get handle to VEO2.dll
//	printf("Load Library %s\n", LibName);
//	m_handle=LoadLibrary(LibName);
//
//	SET_CAMERA_POWER_INITIATE = (TN_SET_CAMERA_POWER_INITIATE)GetProcAddress(m_handle, "SET_CAMERA_POWER_INITIATE");
//	SET_CAMERA_POWER_FETCH = (TN_SET_CAMERA_POWER_FETCH)GetProcAddress(m_handle, "SET_CAMERA_POWER_FETCH");
//	IRWIN_SHUTDOWN = (TN_IRWIN_SHUTDOWN)GetProcAddress(m_handle, "IRWIN_SHUTDOWN");
//	SET_SENSOR_STAGE_LASER_INITIATE = (TN_SET_SENSOR_STAGE_LASER_INITIATE)GetProcAddress(m_handle, "SET_SENSOR_STAGE_LASER_INITIATE");
//	SET_SENSOR_STAGE_LASER_FETCH = (TN_SET_SENSOR_STAGE_LASER_FETCH)GetProcAddress(m_handle, "SET_SENSOR_STAGE_LASER_FETCH");
//	
//	
///***************************************************************
//****	    Check number of arguments and issue error       ****
//****	    if too many/too few.	                        ****
//****************************************************************/
//
//	printf("Number of Arguments= %i\n", (argc-2));
//	if (argc != 4) {
//		printf("Error -100: Incorrect number of COMMAND LINE arguments.\n"); 
//		sprintf( sErrString, "%s", INVALID_PARAM);
//        CloseNAM(-1, INVALID_PARAM);
//	}
//
//	ResultAddr = atol(argv[2]);
//   	    //mov_dfv(&Result, ResultAddr);
//	Result = vmGetInteger(ResultAddr);
//  	ResultStrAddr = atol(argv[3]);
//   	    //mov_dfv(&ResultStr, ResultStrAddr);
//	vmGetText(ResultStrAddr, ResultStr, MAX_STRING_LENGTH);
//
///***************************************************************
//****	    Verify Coyote executable file is present        ****
//****************************************************************/
//	printf("Display File= %s\n", DISPLAYFILE);
//	if (file_exists (DISPLAYFILE) != 1)  {
//		printf ("ERROR -101: Unable to find the Coyote Executable: %s  \n",DISPLAYFILE);
//		sprintf( sErrString, "%s %s", INVALID_EXE, DISPLAYFILE);
//		CloseNAM( -101, sErrString );
//	}
//
//	//SET sensor stage to camera (3)
//	printf("SET sensor stage to CAMERA (3);\n");  //send this 
//	SET_SENSOR_STAGE_LASER_INITIATE(3);
//	Sleep(10000);
//	SET_SENSOR_STAGE_LASER_FETCH(&veo2_val);
//	while (veo2_val != 3) {
//		printf("SET sensor stage to camera;\n");  //send this 
//		SET_SENSOR_STAGE_LASER_INITIATE(3);
//		Sleep(10000);
//		printf("Get sensor stage Setting;\n");
//		SET_SENSOR_STAGE_LASER_FETCH(&veo2_val);
//		printf("Sensor stage Setting = %i\n", veo2_val);
//		y++;
//		if (y == 6)  {
//			printf ("ERROR -103: ERROR - Unable to set sensor stage to camera. \n");
//			sprintf( sErrString, "%s", STAGE_BAD);
//			CloseNAM( -103, sErrString );
//		}
//	}
//	veo2_val=0;
//
//	//Apply power to the VEO2 internal camera
//	printf("SET Camera Power ON;\n");  //send this 
//	SET_CAMERA_POWER_INITIATE(POWER_ON);
//	Sleep(5000);
//	SET_CAMERA_POWER_FETCH(&veo2_val);
//	while (veo2_val != 1) {
//		printf("SET Camera Power ON;\n");  //send this 
//		SET_CAMERA_POWER_INITIATE(POWER_ON);
//		Sleep(2000);
//		printf("Get Camera Power Setting;\n");
//		SET_CAMERA_POWER_FETCH(&veo2_val);
//		printf("Camera Setting = %i\n", veo2_val);
//		x++;
//		if (x == 6)  {
//			printf ("ERROR -102: ERROR - Unable to set internal camera power on. \n");
//			sprintf( sErrString, "%s", POWER_BAD);
//			CloseNAM( -102, sErrString );
//		}
//	}
//
//
//	// Launch program Coyote, pass in the camera setup XML file	
//	sprintf(sExe, "%s", DISPLAYFILE);
//	printf("Call Coyote executable as: %s \n", sExe);
//
//	StartDisplay();	
//	SET_CAMERA_POWER_INITIATE(POWER_OFF);	    			
//	// Successful, Assign String
//	if (nErrCode == 0) {
//		sprintf(sErrReturn, "Successful");
//	}
//
//	/* The Error Status to pass back to ATLAS */
//	CloseNAM(nErrCode, sErrReturn);
//
//}
//
//
