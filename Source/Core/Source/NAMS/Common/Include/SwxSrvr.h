///////////////////////////////////////////////////////////////////////////////
// File:	swxsrvr.h
//
// Purpose: Prototypes of SwxSrvr utility functions
//
// Date:	05/12/04
//
//
// Revision History
//           Revisions tracked in SwxSrvr.cpp file
//
///////////////////////////////////////////////////////////////////////////////
#ifndef SWXSRVR
#define SWXSRVR

#define SWXSRVR_UUID "{98C80A04-A32F-4e77-848F-0E9B2FCC0E3B}"

#ifdef SWXSRVR_EXPORTS
#define SWXSRVR_API __declspec(dllexport)
#else
#define SWXSRVR_API __declspec(dllimport)
#endif

#include <vector>

using std::vector;

typedef struct switchtriplet {
	long	blk;
	long	mod;
	long	pth;
}Triplet;

typedef vector<Triplet> PathData;


extern "C" SWXSRVR_API int InitSwitch(int Dbg, int Sim, char *Response, int BufferSize);
extern "C" SWXSRVR_API int ReleaseSwitch(void);
extern "C" SWXSRVR_API int CloseSwitches(PathData &,char *Response, int BufferSize);
extern "C" SWXSRVR_API int OpenSwitches(PathData &,char *Response, int BufferSize);
//extern "C" SWXSRVR_API int ResetSwitch(void);
extern "C" SWXSRVR_API void GetErrorMessage(int & returnCode, char * card, char * msg);

// Other Station Dependant Routines
// Defines must match CS_ defines in cemsupport.h
#define SU_STA_INTERLOCK   -1000
#define SU_STA_TEMP_FAIL   -1001
#define SU_STA_INIT        -1002
#define SU_CAL_EXPIRED       -2000
#define SU_CFG_INVALID       -2001
#define SU_CAL_CFG_NOT_FOUND -2002
extern "C" SWXSRVR_API int CheckUniqStationStatus(char **ErrMsg);
extern "C" SWXSRVR_API int ClearUniqStationStatus(char **ErrMsg);
extern "C" SWXSRVR_API int CloseUniqStationStatus(void);
extern "C" SWXSRVR_API int GetUniqCalAvail(char *DevName, time_t ExpTs, double *CalData, int Count,
									 char *Response, int BufferSize, int Sim);
extern "C" SWXSRVR_API int SetUniqCalCfg(char *DevName, time_t Ts, double *CalData, int Count,
									 char *Response, int BufferSize, int Sim);
extern "C" SWXSRVR_API int SysResetDevice(char *DevName,char *Response, int BufferSize); 
// ATML Interface Routines
extern "C" SWXSRVR_API int CemIssueAtmlSignal(char *Verb, char *DevName, double MaxTime, char *SignalDescription,
                                         char *Response, int BufferSize);
extern "C" SWXSRVR_API int IssueIst(char *DevName, int Level, char *XmlValueResponse, int RespSize);
extern "C" SWXSRVR_API int ParseErrDbgResponse(char* Response, int BufferSize,
                                         bool Strip, void **ErrDbgHandle);
extern "C" SWXSRVR_API bool ErrDbgNextError(void *ErrDbgHandle, char **ModuleName, char **LeadText,
                                                              int *ErrCode, char **Message);
extern "C" SWXSRVR_API bool ErrDbgNextDebug(void *ErrDbgHandle, char **ModuleName, 
                                                              int *DbgLevel, char **Message);
extern "C" SWXSRVR_API void ErrDbgClose(void *ErrDbgHandle);
extern "C" SWXSRVR_API int GetSingleIntValue(char *XmlValueResponse, char *Attribute, int *Value);
extern "C" SWXSRVR_API int GetIntArrayValue(char *XmlValueResponse, char *Attribute, int *Value, int *ArraySize);
extern "C" SWXSRVR_API int GetSingleDblValue(char *XmlValueResponse, char *Attribute, double *Value);
extern "C" SWXSRVR_API int GetDblArrayValue(char *XmlValueResponse, char *Attribute, double *Value, int *ArraySize);
extern "C" SWXSRVR_API int GetStringValue(char *XmlValueResponse, char *Attribute, char *Value, int BufferSize);
#endif