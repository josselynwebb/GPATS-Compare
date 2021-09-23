//-----------------------------------------------------------------------------
// File:     PLCFileMgr.h
// Contains: 
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// History
//-----------------------------------------------------------------------------
// Date      Eng  PCR  Description
// --------  ---  ---  --------------------------------------------------------
// 05/08/06  AJP       Initial creation
//-----------------------------------------------------------------------------

#ifdef PLCFILEMGR_EXPORTS

#define PLC_FILE_MGR_API __declspec(dllexport)
#else
#define PLC_FILE_MGR_API __declspec(dllimport)
#endif

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif
int PLC_FILE_MGR_API initSpline(long *handle, double *xArray, double *yArray, int size);
int PLC_FILE_MGR_API evalSpline(long handle, double x, double *result);

#if defined(__cplusplus) || defined(__cplusplus__)
}

#endif
