/////////////////////////////////////////////////////////////////////////////
//	File:	Veo2FuncPtrs.h													/
//																			/
//	Creation Date:	12 February 2016										/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		2.0.0.0		Combined Source from Astronics with	/
//					source from VIPERT 1.3.1.0.  							/
//		2.1.0.0		Added hnges from VIPERT 1.3.2.0							/
//																			/
/////////////////////////////////////////////////////////////////////////////
#define		CAMERA			3
#define		POWER_OFF		0
#define		POWER_ON		1

typedef void (CALLBACK* TN_RESET_MODULE_INITIATE)();
TN_RESET_MODULE_INITIATE RESET_MODULE_INITIATE;

typedef void (CALLBACK* TN_SET_CAMERA_POWER_INITIATE)(int);
TN_SET_CAMERA_POWER_INITIATE SET_CAMERA_POWER_INITIATE;

typedef void (CALLBACK* TN_SET_CAMERA_POWER_FETCH)(int*);
TN_SET_CAMERA_POWER_FETCH SET_CAMERA_POWER_FETCH;

typedef void (CALLBACK* TN_SET_SENSOR_STAGE_LASER_INITIATE)(int);
TN_SET_SENSOR_STAGE_LASER_INITIATE SET_SENSOR_STAGE_LASER_INITIATE;

typedef void (CALLBACK* TN_SET_SENSOR_STAGE_LASER_FETCH)(int*);
TN_SET_SENSOR_STAGE_LASER_FETCH SET_SENSOR_STAGE_LASER_FETCH;

typedef void (CALLBACK* TN_IRWIN_SHUTDOWN)();
TN_IRWIN_SHUTDOWN IRWIN_SHUTDOWN;
