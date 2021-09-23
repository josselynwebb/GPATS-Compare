///////////////////////////////////////////////////////////////////////////////
// File:	cemsupport.h
//
// Purpose: Prototypes of cemsupport utility functions
//
// Date:	05/12/04
//
//
// Revision History
//           Revisions tracked in appropriate .cpp file
//
///////////////////////////////////////////////////////////////////////////////
#pragma warning ( push, 1 )
#include <string>
#pragma warning ( pop )
#include <windows.h>

#if     _MSC_VER > 1000
#pragma once
#endif
#pragma warning(disable:4996)

#ifndef CEM_SUPPORT_H
#define CEM_SUPPORT_H

//++++/////////////////////////////////////////////////////////////////////////
// Definitions and prototypes for CemDevInfo.cpp
///////////////////////////////////////////////////////////////////////////////
#define strnzcpy(x,y,z) {strncpy(x,y,z); x[z - 1] = '\0';}
#define IFNSIM(x) { \
                    if(!m_Sim) { x ;} \
                  }
#define CEMDEBUG(lvl, msg)  cs_DebugMsg(m_Dbg,m_DeviceName,lvl, msg)

/*
CEMERROR and INSTERROR use the standard ErrMsgBits flags:

Severity Field   There are three mutually-exclusive labels for specifying the severity of the error:

EB_SEVERITY_INFO
EB_SEVERITY_WARNING
EB_SEVERITY_ERROR

Action Field   There are three mutually exclusive labels for specifying what action the RTS is to take pertaining to the execution of the ATLAS program:

EB_ACTION_HALT
EB_ACTION_RESET
EB_ACTION_ABORT

Set Field   There is one label for specifying a specific RTS action:

EB_SET_MAXTIME
*/
#define CEMERROR(flags, msg) cs_ErrMsg(0, flags, msg)
#define INSTERROR(flags, msg) cs_ErrMsg(1, flags, msg)
                               
#define MAX_BC_DEV_NAME         32
#define MAX_MSG_SIZE          2048
#define DEV_NOT_FOUND			-1
#define FILE_NOT_FOUND			-2
#define PAWSENV_INI_NOT_FOUND	-3
#ifndef FLT_MAX
#define FLT_MAX  2.0E+36
#endif

typedef struct BusConfigDataStruct
{
    char  DeviceName[MAX_BC_DEV_NAME];
    int   DeviceNo;     // Multiple device number
    int   BusNo;        // BUS / Channel number
    int   PrimaryAdr;   // MLA or MTA
    int   SecondaryAdr; // MSA
    int   Allocated;    // Is device used by this program (0 - no, 1 - yes)
    int   Sim;          // Simulate flag value (0-false, 1-true)
    int   Dbg;          // Debug flag value (0- 9)
    void *DriverClass;  // Place holder for class instance pointer
} CS_DEVINFO;

//CemDevInfo.cpp
extern int         cs_GetDevInfo(const char *devprefix, int Fnc, CS_DEVINFO *DevInfo, int MaxDev);
extern CS_DEVINFO *cs_FindDevInfo(char *Device, CS_DEVINFO *DevInfo, int MaxDev);
extern int         cs_GetCurDeviceFnc(CS_DEVINFO *DevInfo, int MaxDev,
                                      void **DriverClass, int *Fnc);
//CemDisplay.cpp
extern void        cs_DebugMsg(int MemLevel, char *MemDevName, int ReqLevel, char *Msg);
extern char       *cs_FmtMsg(char *Fmt,...);
extern void        cs_ErrMsg(int Type, int Flags, char *UsrMsg);
extern int         cs_ParseResponseError(char *Response, char *ValueResponse, int BufferSize);
extern int         cs_ParseResponseErrorOvr(int SetErrLevel, char *Response,
                                            char *ValueResponse, int BufferSize);
//CemSwitch.cpp
extern int		   cs_DoSwitching(int key);

//CemStationUtil.cpp
// Defines must match SU_ defines in swxsrvr.h
#define CS_STA_INTERLOCK     -1000
#define CS_STA_TEMP_FAIL     -1001
#define CS_STA_INIT          -1002
#define CS_CAL_EXPIRED       -2000
#define CS_CFG_INVALID       -2001
#define CS_CAL_CFG_NOT_FOUND -2002
extern int          cs_CheckStationStatus();
extern int          cs_GetUniqCalCfg(char *DevName, time_t ExpTs, double *CalData, int Count,
									 int Sim);
extern int          cs_SetUniqCalCfg(char *DevName, time_t Ts, double *CalData, int Count,
									 int Sim);
extern int          cs_IssueAtmlSignal(char *Verb, char *DevName, char *SignalDescription,
                                     char *XmlValueResponse, int RespSize); 
extern int          cs_IssueAtmlSignalMaxTime(char *Verb, char *DevName, double MaxTimeSec, char *SignalDescription,
                                     char *XmlValueResponse, int RespSize);
extern int          cs_SysResetDevice(char *DevName);
extern int          cs_IssueIst(char *DevName, int Level, char *XmlValueResponse, int RespSize);
extern int          cs_StationReset(int Sim);
extern int          cs_GetSingleIntValue(char *XmlValueResponse, char *Attr, int *IntValue);
extern int          cs_GetIntArrayValue(char *XmlValueResponse, char *Attr,
                                     int *IntValue, int *ArraySize);
extern int          cs_GetSingleDblValue(char *XmlValueResponse, char *MeasFunc, double *MeasValue);
extern int          cs_GetDblArrayValue(char *XmlValueResponse, char *MeasFunc,
                                     double *MeasValue, int *ArraySize);
extern int          cs_GetStringValue(char *XmlValueResponse, char *MeasFunc, char *StrValue, int BufferSize);

#endif
