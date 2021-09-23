/////////////////////////////////////////////////////////////////////////////
//	File:	Veo2Elements.cpp												/
//																			/
//	Creation Date:	12 February 2016										/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0	Assigned it a version number.								/
//		2.0.0.0		Combined Source from Astronics with	/
//					source from VIPERT 1.3.1.0.  							/
//		2.1.0.0		Added hnges from VIPERT 1.3.2.0							/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <windows.h>
#include <nam.h>
#include "Veo2FuncPtrs.h"
#include "Constants.h"
#include "CoyoteNam.h"

/////////////////////////////////////////////////////////////////////////////
//		Declarations														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// InitializeVeo2ForNam:	This function will first load the dll library,
//							get handles for the 5 api function calls into
//							Veo2.dll, then check to see if they are valid.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int InitializeVeo2ForNam(void)
{

	if ((CoyoteInfo.Veo2Hd = LoadLibrary(LIB_NAME)) == NULL) {
		dodebug(0, "InitializeVeo2ForNam()", "Failed to load library %s", LIB_NAME, (char*)NULL);
		CoyoteInfo.CoyoteErrno = NAM_ERROR;
		return (NAM_ERROR);
	}

	SET_CAMERA_POWER_INITIATE = (TN_SET_CAMERA_POWER_INITIATE)GetProcAddress(CoyoteInfo.Veo2Hd, "SET_CAMERA_POWER_INITIATE");
	SET_CAMERA_POWER_FETCH = (TN_SET_CAMERA_POWER_FETCH)GetProcAddress(CoyoteInfo.Veo2Hd, "SET_CAMERA_POWER_FETCH");

	IRWIN_SHUTDOWN = (TN_IRWIN_SHUTDOWN)GetProcAddress(CoyoteInfo.Veo2Hd, "IRWIN_SHUTDOWN");

	SET_SENSOR_STAGE_LASER_INITIATE = (TN_SET_SENSOR_STAGE_LASER_INITIATE)GetProcAddress(CoyoteInfo.Veo2Hd, "SET_SENSOR_STAGE_LASER_INITIATE");
	SET_SENSOR_STAGE_LASER_FETCH = (TN_SET_SENSOR_STAGE_LASER_FETCH)GetProcAddress(CoyoteInfo.Veo2Hd, "SET_SENSOR_STAGE_LASER_FETCH");

	RESET_MODULE_INITIATE = (TN_RESET_MODULE_INITIATE)GetProcAddress(CoyoteInfo.Veo2Hd, "RESET_MODULE_INITIATE");
	
	if (SET_CAMERA_POWER_INITIATE == NULL || SET_CAMERA_POWER_FETCH == NULL){
		dodebug(0, "InitializeVeo2ForNam()", "Failed to Initialize Camera Power", (char*)NULL);
		CoyoteInfo.CoyoteErrno = NAM_ERROR;
		return (NAM_ERROR);
	}

	if (SET_SENSOR_STAGE_LASER_INITIATE == NULL || SET_SENSOR_STAGE_LASER_FETCH == NULL){
		dodebug(0, "InitializeVeo2ForNam()", "Failed to Initialize Sensor Stage", (char*)NULL);
		CoyoteInfo.CoyoteErrno = NAM_ERROR;
		return (NAM_ERROR);
	}

	if (IRWIN_SHUTDOWN == NULL || RESET_MODULE_INITIATE == NULL){
		dodebug(0, "InitializeVeo2ForNam()", "Failed to Initialize Shut Down", (char*)NULL);
		CoyoteInfo.CoyoteErrno = NAM_ERROR;
		return (NAM_ERROR);
	}

	return SUCCESS;
}

//
// StartTheCamera:	This function will turn on the Veo2's camera that
//					is used for this nam.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int StartTheCamera(void)
{

	RESET_MODULE_INITIATE();
	
	if (SetTheStage()) {
		return (NAM_ERROR);
	}

	if (TurnOntheCamera()) {
		return (NAM_ERROR);
	}

	return SUCCESS;
}

//
// StopTheCamera:	This function will turn off the Veo2's camera that
//					is used for this nam.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int StopTheCamera(void)
{

	int		Veo2ReturnVal = 0;
	int		LoopCnt = 0;
	
	SET_CAMERA_POWER_INITIATE(POWER_OFF);	    			

	while (Veo2ReturnVal != POWER_OFF && LoopCnt < 6) {
		SET_CAMERA_POWER_INITIATE(POWER_ON);

		Sleep(2000);

		SET_CAMERA_POWER_FETCH(&Veo2ReturnVal);

		LoopCnt++;
	}

	if (LoopCnt == 6)  {
		dodebug(0, "StopTheCamera()", "ERROR - Unable to set internal camera power off.", (char*)NULL);
		CoyoteInfo.CoyoteErrno = POWERBAD;
	}

	IRWIN_SHUTDOWN();

	if (CoyoteInfo.Veo2Hd != NULL) {
		FreeLibrary(CoyoteInfo.Veo2Hd);
		CoyoteInfo.Veo2Hd = NULL;
	}

	return SUCCESS;
}

//
// SetTheStage:	This function will set the Veo2's stage for 
//					the camera that is used for this nam.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int SetTheStage(void)
{

	int		Veo2ReturnVal = 0;
	int		LoopCnt = 0;

	SET_SENSOR_STAGE_LASER_FETCH(&Veo2ReturnVal);

	if (Veo2ReturnVal != CAMERA) {
		SET_SENSOR_STAGE_LASER_INITIATE(CAMERA);

		Sleep(10000);
		SET_SENSOR_STAGE_LASER_FETCH(&Veo2ReturnVal);

		while (Veo2ReturnVal != CAMERA && LoopCnt < 6) {

			SET_SENSOR_STAGE_LASER_INITIATE(CAMERA);
			Sleep(10000);

			SET_SENSOR_STAGE_LASER_FETCH(&Veo2ReturnVal);
			LoopCnt++;
		}

		if (LoopCnt == 6)  {
			dodebug(0, "SetTheStage()", "ERROR - Unable to set sensor stage to camera.", (char*)NULL);
			CoyoteInfo.CoyoteErrno = STAGEBAD;
			return (NAM_ERROR);
		}
	}

	return SUCCESS;
}


//
// TurnOntheCamera:	This function will turn on the Veo2's camera that
//					is used for this nam.
//
// Parameters:
//		none:
//
// Returns:
//		SUCCESS:	  0  = Successful completion of the function.
//		NAM_ERROR:	(-1) = An error had occurr'd, what else.
//
//

int TurnOntheCamera(void)
{

	int		Veo2ReturnVal = 0;
	int		LoopCnt = 0;
	
	SET_CAMERA_POWER_INITIATE(POWER_ON);

	Sleep(5000);

	SET_CAMERA_POWER_FETCH(&Veo2ReturnVal);

	while (Veo2ReturnVal != POWER_ON && LoopCnt < 6) {

		Sleep(2000);

		SET_CAMERA_POWER_FETCH(&Veo2ReturnVal);

		LoopCnt++;
	}

	if (LoopCnt == 6)  {
		dodebug(0, "TurnOntheCamera()", "ERROR - Unable to set internal camera power on.", (char*)NULL);
		CoyoteInfo.CoyoteErrno = POWERBAD;
		return (NAM_ERROR);
	}

	IRWIN_SHUTDOWN();

	if (CoyoteInfo.Veo2Hd != NULL) {
		FreeLibrary(CoyoteInfo.Veo2Hd);
		CoyoteInfo.Veo2Hd = NULL;
	}

	return SUCCESS;
}

