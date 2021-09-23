/**
*
*  BTIEBR WIN32 DRIVER  Version 1.0.3  (07/08/2015)
*  Copyright (c) 2013-2015
*  Ballard Technology, Inc.
*  www.ballardtech.com
*  support@ballardtech.com
*  ALL RIGHTS RESERVED
*
*  NAME:   BTIEBR.H -- Visual C++ 32-bit
*                       BTIEBR Driver Include Header File.
*
**/

/**
*
*  This file defines the procedures provided by the Visual
*  C++ 32-bit Driver Library for Ballard Technology EBR-1553
*  cards.  Applications using the BTI1EBR Driver Library must
*  incorporate this include file using the preprocessor directive
*  #include.  If this file is in the current working directory,
*  the form would be:
*
*  #include "BTIEBR.H"
*
*  Refer to the Visual C++ manual for more information on
*  header files.
*
**/

/**
*
*  Conditional block to prevent multiple defines.
*
**/

#ifndef __BTIEBR_H
#define __BTIEBR_H

/**
*
*  Pragma to specify link library.
*
**/

#ifdef __BORLANDC__
#pragma comment(lib,"btiebrbc.lib")
#else
#if defined(_WIN32)
#if defined(_WIN64)
#pragma comment(lib,"btiebr64.lib")
#else
#pragma comment(lib,"btiebr.lib")
#endif
#endif
#endif

/**
*
*  Typedefs used by the BTIEBR Driver.
*
**/

#ifndef BTIEBRAPI
#ifdef __BORLANDC__
#define BTIEBRAPI
#else
#define BTIEBRAPI __declspec(dllimport)
#endif
#endif

#ifndef VOID
typedef void VOID;
#endif

#ifndef LPVOID
typedef void * LPVOID;
#endif

#ifndef INT32
typedef int INT32;
#endif

#ifndef LPINT32
typedef int * LPINT32;
#endif

#ifndef BYTE
typedef unsigned char BYTE;
#endif

#ifndef LPBYTE
typedef unsigned char * LPBYTE;
#endif

#ifndef CHAR
typedef char CHAR;
#endif

#ifndef UINT16
typedef unsigned short UINT16;
#endif

#ifndef LPUINT16
typedef unsigned short * LPUINT16;
#endif

#ifndef UINT32
typedef unsigned int UINT32;
#endif

#ifndef LPUINT32
typedef unsigned int * LPUINT32;
#endif

#ifndef MSGADDR
typedef unsigned long MSGADDR;
#endif

#ifndef LISTADDR
typedef unsigned long LISTADDR;
#endif

#ifndef LPMSGADDR
typedef unsigned long * LPMSGADDR;
#endif

#ifndef LPCSTR
typedef const char * LPCSTR;
#endif

#ifndef LPSTR
typedef char * LPSTR;
#endif

#ifndef BOOL
typedef int BOOL;
#endif

#ifndef HCARD
#if defined(_WIN64)
typedef void * HCARD;
#else
typedef int HCARD;
#endif
#endif

#ifndef HCORE
#if defined(_WIN64)
typedef void * HCORE;
#else
typedef int HCORE;
#endif
#endif

#ifndef LPHCORE
#if defined(_WIN64)
typedef void ** LPHCORE;
#else
typedef int * LPHCORE;
#endif
#endif

#ifndef ERRVAL
typedef int ERRVAL;
#endif

#ifndef SCHNDX
typedef int SCHNDX;
#endif

/**
*
*  Structs used by the BTIEBR Driver.
*
**/

#ifndef XMITFIELDSEBR

typedef struct
{
    UINT32 ctrlflags;           //User writes message configuration options
    UINT16 flag1;               //Card writes message flag 1
    UINT16 flag2;               //Card writes message flag 2
    UINT16 errflags;            //Card writes error flags
    UINT16 actflags;            //Card writes activity flags
    UINT16 resptime;            //Card writes response time
    UINT16 resv7;               //Reserved field
    UINT16 datacount;           //Card writes data count
    UINT16 extflag;             //Driver writes external flags
    UINT32 timetag;             //Card writes time tag
    UINT32 elapsetime;          //Card writes elapsed time
    UINT32 preaddr;             //Driver writes pre-proc address
    UINT32 postaddr;            //Driver writes post-proc address
    UINT32 timetagh;            //Card writes extended timetag
    UINT16 resv18;              //Reserved field
    UINT16 resv19;              //Reserved field
    UINT16 cwd;                 //User writes command word
	UINT16 cwdinfo;				//User writes command word info
	UINT16 swd;                 //Card writes status word
	UINT16 swdinfo;				//Card writes status word info
    UINT16 data[32];            //User writes data words
    UINT16 extra[8];            //User writes extra words
} XMITFIELDSEBR;

#endif

#ifndef LPXMITFIELDSEBR
typedef XMITFIELDSEBR * LPXMITFIELDSEBR;
#endif

#ifndef MSGFIELDSEBR

typedef struct
{
    UINT16 msgflag;             //Driver writes message flags
    UINT16 msgopt;              //Driver writes message options
    UINT16 msgerr;              //Card writes message errors
    UINT16 msgact;              //Card writes message activity
    UINT16 resptime;            //Card writes response time
    UINT16 resv5;               //Reserved field
    UINT16 datacount;           //Card writes data count
    UINT16 extflag;             //Driver writes external flags
    union {
        UINT32 timetag;         //Card writes time tag
        UINT32 hitcount;        //Card writes hit count
    };
    union {
        UINT32 elapsetime;      //Card writes elapsed time
        UINT32 mintime;         //Card writes min time
        UINT32 maxtime;         //Card writes max time
    };
    UINT32 preaddr;             //Driver writes pre-proc address
    UINT32 postaddr;            //Driver writes post-proc address
    UINT32 timetagh;            //Card writes extended timetag
    UINT16 resv18;              //Reserved field
    UINT16 resv19;              //Reserved field
    UINT16 cwd;                 //Card/User writes command word (R/T)
	UINT16 cwdinfo;				//Card/User writes command word info
    UINT16 swd;                 //Card writes status word
	UINT16 swdinfo;		    	//Card writes status word info
    UINT16 data[32];            //User/Card writes data words (R/T)
    UINT16 extra[8];            //User/Card writes extra words (R/T)
} MSGFIELDSEBR;

#endif

#ifndef LPMSGFIELDSEBR
typedef MSGFIELDSEBR * LPMSGFIELDSEBR;
#endif

/**
*
*  "C" block if compiling a C++ file.
*
**/

#ifdef __cplusplus
extern "C" {
#endif

/**
*
*  BTIEBR Driver functions.
*
**/

BTIEBRAPI ERRVAL __stdcall BTIEBR_BCConfig(UINT32 configval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCConfigEx(UINT32 configval,UINT16 count,INT32 channum,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_BCConfigMsg(UINT32 configval,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI LISTADDR __stdcall BTIEBR_BCCreateList(UINT32 listconfigval,INT32 count,UINT32 msgconfigval,UINT16 cwd,LPUINT16 data,INT32 rtlink,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_BCCreateMsg(UINT32 configval,UINT16 cwd,LPUINT16 data,INT32 rtlink,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_BCPause(INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_BCPauseCheck(INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_BCResume(INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedAgain(SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedBranch(UINT32 condition,SCHNDX destindex,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCSchedBranchUpdate(SCHNDX destindex,SCHNDX opcodeindex,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCSchedBuild(UINT16 nummsgs,LPMSGADDR msgaddr,LPUINT16 freq,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedCall(UINT32 condition,SCHNDX destindex,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCSchedCallUpdate(SCHNDX destindex,SCHNDX opcodeindex,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedEntry(INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedFrame(UINT32 timeval,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedFrameEnd(INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedFrameStart(UINT32 timeval,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedGap(UINT16 gapval,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedHalt(INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedLog(UINT32 condition,UINT16 tagval,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedMsg(MSGADDR msgaddr,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedPause(INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedPulse(INT32 dionum,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedPulse0(INT32 dionum,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedPulse1(INT32 dionum,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedRestart(INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedRetry(UINT32 condition,UINT16 retries,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedReturn(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCSetDefaultGap(INT32 gapval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCSetTimeout(UINT16 timeoutval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCSyncDefine(BOOL enableflag,UINT16 syncmask,UINT16 pinpolarity,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCTransmitMsg(LPXMITFIELDSEBR xmitfields,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCTriggerDefine(BOOL enableflag,UINT16 trigmask,UINT16 trigval,UINT16 pinpolarity,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_ChGetInfo(UINT16 infotype,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ChIsBM(INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ChIsEBR(INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ChIsM(INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ChIsS(INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ChStart(INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ChStop(INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_CmdMaxLoopRd(INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdMaxLoopWr(UINT16 countval,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_CmdShotRd(SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_CmdShotWr(BOOL value,SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_CmdSkipRd(SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_CmdSkipWr(BOOL value,SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_CmdStepRd(SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_CmdStepWr(BOOL value,SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ErrorCtrl(UINT32 ctrlval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ErrorDefine(UINT32 defineval,UINT16 errvalue,INT32 countval,UINT16 wordpos,UINT16 bitpos,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ErrorSent(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ErrorTagBC(BOOL tagval,MSGADDR msgaddr,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ErrorTagRT(BOOL tagval,BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ErrorTriggerDefine(BOOL enableflag,UINT16 trigmask,UINT16 trigval,UINT16 pinpolarity,INT32 channum,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_ListBlockRd(LPUINT16 buf,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_ListBlockWr(LPUINT16 buf,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_ListDataRd(LPUINT16 buf,INT32 count,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_ListDataWr(LPUINT16 buf,INT32 count,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ListMultiBlockRd(LPUINT16 buf,LPINT32 blkcountptr,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ListMultiBlockWr(LPUINT16 buf,INT32 blkcount,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_MonConfig(UINT32 configval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_MonFilterRTLink(UINT32 rtlinkmask,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_MonFilterSA(INT32 rtlink,UINT32 rcvsamask,UINT32 xmtsamask,UINT32 rcvmcmask,UINT32 xmtmcmask,INT32 channum,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_MsgBlockRd(LPMSGFIELDSEBR buf,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_MsgBlockWr(LPMSGFIELDSEBR buf,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgDataRd(LPUINT16 buf,INT32 count,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgDataWr(LPUINT16 buf,INT32 count,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_MsgFieldRd(UINT16 fieldtype,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_MsgFieldWr(UINT32 fieldval,UINT16 fieldtype,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgGroupDataRd(INT32 nummsgs,UINT16 databufs[][32],LPMSGADDR msgaddrptr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgGroupDataWr(INT32 nummsgs,UINT16 databufs[][32],LPMSGADDR msgaddrptr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgGroupRd(INT32 nummsgs,LPMSGFIELDSEBR msgflds,LPMSGADDR msgaddrptr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgGroupWr(INT32 nummsgs,LPMSGFIELDSEBR msgflds,LPMSGADDR msgaddrptr,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_MsgSkipRd(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgSkipWr(BOOL skip,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_MsgSyncDefine(BOOL enableflag,UINT16 syncmask,UINT16 pinpolarity,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_MsgTriggerDefine(BOOL enableflag,UINT16 trigmask,UINT16 trigval,UINT16 pinpolarity,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_PlayConfig(UINT32 configval,UINT32 rtlinkmask,INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_PlayStatus(INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_PlayWr(LPUINT16 buf,UINT16 bufcount,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTConfig(UINT32 configval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI LISTADDR __stdcall BTIEBR_RTCreateList(UINT32 listconfigval,INT32 count,UINT32 msgconfigval,BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_RTCreateMsg(UINT32 configval,BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_RTGetMsg(BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTReset(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTResponseTimeSet(INT32 resptime,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTSetMode(UINT32 configval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_RTSWDRd(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTSWDWr(UINT16 swdval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTSyncDefine(BOOL enableflag,UINT16 syncmask,UINT16 pinpolarity,INT32 rtlink,UINT32 rcvsamask,UINT32 xmtsamask,UINT32 rcvmcmask,UINT32 xmtmcmask,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_ValPackCWD(INT32 TAval,INT32 TRflag,INT32 SAval,INT32 WCval);
BTIEBRAPI VOID __stdcall BTIEBR_ValUnpackCWD(UINT16 CWDval,LPINT32 TAval,LPINT32 TRflag,LPINT32 SAval,LPINT32 WCval);

/**
*
*  Internal functions used by the BTIEBR Driver.
*
**/

BTIEBRAPI LISTADDR __stdcall BTIEBR_BCCreateListEx(UINT32 listconfigval,INT32 skipval,INT32 count,UINT32 msgconfigval,UINT16 cwd,LPUINT16 data,INT32 rtlink,HCORE handleval);
BTIEBRAPI LISTADDR __stdcall BTIEBR_BCCreateListExx(UINT32 listconfigval,INT32 skipval,INT32 count,UINT32 msgconfigval,UINT16 cwd,LPUINT16 data,INT32 rtlink,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_BCGetMsg(INT32 index,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCIMGapRd(LPUINT16 gapval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCIMGapWr(UINT16 gapval,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedBranchEx(UINT16 condition1,UINT16 condition2,UINT16 condition3,UINT16 opcode,SCHNDX destindex,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedCallEx(UINT16 condition1,UINT16 condition2,UINT16 condition3,UINT16 opcode,SCHNDX destindex,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedLogEx(UINT16 condition1,UINT16 condition2,UINT16 condition3,UINT16 opcode,UINT16 tagval,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedNop(INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedRetryEx(UINT16 condition1,UINT16 condition2,UINT16 condition3,UINT16 retryflag,UINT16 retries,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSchedUser(UINT32 useraddr,INT32 channum,HCORE handleval);
BTIEBRAPI SCHNDX __stdcall BTIEBR_BCSetEntry(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCTransmitMsgBlkEx(BOOL writeflag,INT32 count,LPXMITFIELDSEBR xmitfields,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCTransmitMsgBlkInit(INT32 maxcount,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCTransmitMsgBlkRd(INT32 count,LPXMITFIELDSEBR xmitfields,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BCTransmitMsgBlkWr(INT32 count,LPXMITFIELDSEBR xmitfields,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_BlkAddr(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_BlkOptRd(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BlkOptWr(UINT16 rtopt,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_BlkPtrRd(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_BlkPtrWr(UINT32 descaddr,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BlkRd(LPUINT16 block,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BlkReset(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_BlkSWDRd(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BlkSWDWr(UINT16 swdval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BlkWipe(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_BlkWr(LPUINT16 block,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ChConfig(BOOL enableflag,INT32 type,UINT16 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ChIsRunning(INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_CmdAddr(SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_CmdAlloc(INT32 count,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_CmdAllocEx(UINT32 configval,INT32 count,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_CmdBaseRd(INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdBaseWr(UINT32 addrval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_CmdClear(INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_CmdCountRd(INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdCountWr(UINT16 countval,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_CmdCtrlRd(SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdCtrlWr(UINT16 ctrlval,SCHNDX index,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_CmdCurrRd(INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdCurrWr(UINT16 addrval,INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdInit(UINT16 opcode,LPUINT16 block);
BTIEBRAPI SCHNDX __stdcall BTIEBR_CmdInsert(LPUINT16 block,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_CmdRd(LPUINT16 block,UINT32 addrval,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdRestart(INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdStackClr(INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_CmdStartRd(INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdStartWr(UINT16 addrval,INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_CmdTotalRd(INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_CmdTotalWr(INT32 countval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_CmdWr(LPUINT16 block,UINT32 addrval,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_DescAddr(BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_DescAlloc(HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_DescConfig(UINT32 configval,BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_DescConfigAll(UINT32 configval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_DescOptRd(BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_DescOptWr(UINT16 descopt,BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_DescPtrRd(BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_DescPtrWr(MSGADDR msgaddr,BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_DescRd(BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,LPUINT16 desc,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_DescWr(BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,LPUINT16 desc,INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_DllUsageCount(VOID);
BTIEBRAPI BOOL __stdcall BTIEBR_ErrorIsTaggedBC(MSGADDR msgaddr,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ErrorIsTaggedRT(BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ErrorResponse(UINT16 responseval,INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_ErrorSync(BOOL value,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ErrorTimeout(UINT16 timeoutval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_HookMsgProc(INT32 msgval,LPVOID lpParam,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_HostComm(LPVOID lpParam,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_IsChan(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_LHubConfig(UINT32 configval,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_LHubPortExists(INT32 portnum,INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_LHubPortGetCount(INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_LHubPortGetRTLink(INT32 portnum,INT32 channum,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_LHubPortIsAvail(INT32 portnum,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_LHubPortIsAvailMask(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_LHubPortSetRTLink(INT32 rtlink,INT32 portnum,INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_LHubRTLinkGetPort(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_ListAddr(INT32 index,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_ListAddrEx(INT32 index,LISTADDR listaddr,LPUINT16 header,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_ListBlockRdEx(LPUINT16 buf,INT32 index,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_ListBlockWrEx(LPUINT16 buf,INT32 index,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ListClear(LISTADDR listaddr,HCORE handleval);
BTIEBRAPI LISTADDR __stdcall BTIEBR_ListConfig(UINT32 configval,INT32 count,UINT16 skipval,HCORE handleval);
BTIEBRAPI LISTADDR __stdcall BTIEBR_ListConfigEx(UINT32 configval,INT32 count,UINT16 skipval,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_ListDataRdEx(LPUINT16 buf,INT32 count,INT32 index,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_ListDataWrEx(LPUINT16 buf,INT32 count,INT32 index,LISTADDR listaddr,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ListInit(LISTADDR listaddr,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_ListNextRd(UINT32 listaddr,LPUINT16 header,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_ListNextWr(UINT32 listaddr,LPUINT16 header,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_ListPostRd(LISTADDR listaddr,LPUINT16 header,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_ListPostWr(LISTADDR listaddr,LPUINT16 header,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_ListPreRd(LISTADDR listaddr,LPUINT16 header,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_ListPreWr(LISTADDR listaddr,LPUINT16 header,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_ListStatus(LISTADDR listaddr,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsBC(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsMon(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsNotBC(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsNotMon(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsNotPlay(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsNotRT(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsNotSerial(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsPlay(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsRT(UINT16 chanflag);
BTIEBRAPI BOOL __stdcall BTIEBR_ModeIsSerial(UINT16 chanflag);
BTIEBRAPI ERRVAL __stdcall BTIEBR_MonConfigEx(UINT32 configval,UINT32 moncount,UINT16 cardnum,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_MonFilterSAEx(INT32 rtlink,UINT32 rcvsamask,UINT32 xmtsamask,UINT32 rcvmcmask,UINT32 xmtmcmask,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_MsgCheck(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_MsgCommRd(LPMSGFIELDSEBR buf,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_MsgCommWr(LPMSGFIELDSEBR buf,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_MsgConfig(UINT32 configval,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_MsgConfigEx(UINT32 configval,UINT16 cwd,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_MsgCWDLinkRd(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgCWDLinkWr(UINT16 cwdlink,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_MsgCWDRd(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgCWDWr(UINT16 cwd,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_MsgErrorRd(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgErrorWr(UINT16 value,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_MsgFlagRd(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgFlagWr(UINT16 msgflag,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI BOOL __stdcall BTIEBR_MsgIsAccessed(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_MsgOptRd(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_MsgOptWr(UINT16 msgopt,MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_MsgReset(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI MSGADDR __stdcall BTIEBR_MsgResetEx(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_MsgSWDLinkRd(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_MsgSWDRd(MSGADDR msgaddr,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_PlayClear(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_PlayConfigEx(UINT32 configval,UINT32 playcount,UINT32 rtlinkmask,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_PlayCounterWr(UINT32 totalcount,INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_PlayStatusEx(LPUINT32 totalcount,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_PlayWrEx(LPUINT16 buf,UINT16 bufcount,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ProcSetPost(UINT32 useraddr,UINT32 msgaddr,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_ProcSetPre(UINT32 useraddr,UINT32 msgaddr,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTConfigEx(UINT32 configval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTConfigExx(UINT32 configval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI LISTADDR __stdcall BTIEBR_RTCreateListEx(UINT32 listconfigval,INT32 skipval,INT32 count,UINT32 msgconfigval,BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI LISTADDR __stdcall BTIEBR_RTCreateListExx(UINT32 listconfigval,INT32 skipval,INT32 count,UINT32 msgconfigval,BOOL mcflag,INT32 rtlink,BOOL trflag,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTResponseTimeRd(LPUINT16 timeval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_RTResponseTimeWr(UINT16 timeval,INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_RTStatus(INT32 rtlink,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SABlkWr(INT32 rtlink,INT32 saval,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SABlkWrEx(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SchedBuild(INT32 nummsgs,LPMSGADDR msgaddr,LPINT32 freq,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SchedBuildEx(INT32 nummsgs,LPMSGADDR msgaddr,LPINT32 freq,LPINT32 period,LPVOID buf,UINT32 bufsize,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SchedBuildExx(UINT32 frametime,INT32 nummsgs,LPMSGADDR msgaddr,LPINT32 period,LPVOID buf,UINT32 bufsize,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SchedCalc(LPMSGADDR msgaddr,INT32 channum,HCORE handleval);
BTIEBRAPI VOID __stdcall BTIEBR_SchedDisplay(UINT32 nummsgs,LPUINT32 count);
BTIEBRAPI VOID __stdcall BTIEBR_SchedHook(INT32 index,LPVOID ptr);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SchedInsEnd(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SchedInsMsg(UINT32 index,LPMSGADDR msgaddr,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SchedInsStart(UINT32 frametime,UINT32 count,INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_SchedMaxval(LPUINT32 array,UINT32 arraycount);
BTIEBRAPI UINT32 __stdcall BTIEBR_SchedMinval(LPUINT32 array,UINT32 arraycount);
BTIEBRAPI VOID __stdcall BTIEBR_SchedPrint(LPSTR str,...);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SerialClear(INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SerialConfig(UINT32 configval,UINT16 count,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_SerialConfigEx(UINT32 configval,UINT16 count,INT32 channum,HCORE handleval);
BTIEBRAPI INT32 __stdcall BTIEBR_SerialStatus(INT32 channum,HCORE handleval);
BTIEBRAPI UINT32 __stdcall BTIEBR_SerialWr(LPUINT16 buf,INT32 channum,HCORE handleval);
BTIEBRAPI ERRVAL __stdcall BTIEBR_TestProtocol(HCORE handleval);
BTIEBRAPI UINT16 __stdcall BTIEBR_ValPackRTCWD(INT32 MCflag,INT32 TAval,INT32 TRflag,INT32 SAval,INT32 count);
BTIEBRAPI VOID __stdcall BTIEBR_ValUnpackRTCWD(UINT16 cwd,LPINT32 BCASTflag,LPINT32 MCflag,LPINT32 TAval,LPINT32 TRflag,LPINT32 SAval,LPINT32 count);

#ifdef __cplusplus
}
#endif

/**
*
*  Channel number constants
*
**/

#define CH0  0x0000           //Channel 0
#define CH1  0x0001           //Channel 1
#define CH2  0x0002           //Channel 2
#define CH3  0x0003           //Channel 3
#define CH4  0x0004           //Channel 4
#define CH5  0x0005           //Channel 5
#define CH6  0x0006           //Channel 6
#define CH7  0x0007           //Channel 7
#define	CH8  0x0008           //Channel 8
#define	CH9  0x0009           //Channel 9
#define	CH10 0x000A           //Channel 10
#define	CH11 0x000B           //Channel 11
#define	CH12 0x000C           //Channel 12
#define	CH13 0x000D           //Channel 13
#define	CH14 0x000E           //Channel 14
#define	CH15 0x000F           //Channel 15
#define	CH16 0x0010           //Channel 16
#define	CH17 0x0011           //Channel 17
#define	CH18 0x0012           //Channel 18
#define	CH19 0x0013           //Channel 19
#define	CH20 0x0014           //Channel 20
#define	CH21 0x0015           //Channel 21
#define	CH22 0x0016           //Channel 22
#define	CH23 0x0017           //Channel 23
#define	CH24 0x0018           //Channel 24
#define	CH25 0x0019           //Channel 25
#define	CH26 0x001A           //Channel 26
#define	CH27 0x001B           //Channel 27
#define	CH28 0x001C           //Channel 28
#define	CH29 0x001D           //Channel 29
#define	CH30 0x001E           //Channel 30
#define	CH31 0x001F           //Channel 31

/**
*
*  Logical hub configuration options
*
**/

enum {	LHUBCFGEBR_DEFAULT    = 0x00000000L,      //Select all default settings
		LHUBCFGEBR_ENABLE     = 0x00000000L,      //Enable all external ports (default)
		LHUBCFGEBR_DISABLE    = 0x00000001L,      //Disable all external ports
		LHUBCFGEBR_SPEC       = 0x00000000L,      //LHub will be configured for Spec mode (default)
		LHUBCFGEBR_SWITCH     = 0x00000010L,      //LHub will be configured for Switch mode
		LHUBCFGEBR_LINK       = 0x00000020L       //LHub will be configured for Link mode
};

/**
*
*  Bus controller configuration options
*
**/

enum {	BCCFGEBR_DEFAULT      = 0x00000000L,      //Select all default settings
		BCCFGEBR_ENABLE       = 0x00000000L,      //Enable bus controller (default)
		BCCFGEBR_DISABLE      = 0x00000001L,      //Disable bus controller
		BCCFGEBR_NOLOGHALT    = 0x00000000L,      //Do not generate event log on halt (default)
		BCCFGEBR_LOGHALT      = 0x00000100L,      //Generate event log on halt
		BCCFGEBR_NOLOGPAUSE   = 0x00000000L,      //Do not generate event log on pause (default)
		BCCFGEBR_LOGPAUSE     = 0x00000200L,      //Generate event log on pause
		BCCFGEBR_NOLOOPMAX    = 0x00000000L,      //Disable maximum loop count (default)
		BCCFGEBR_LOOPMAX      = 0x00001000L,      //Enable maximum loop count
		BCCFGEBR_NOSTEP       = 0x00000000L,      //Disable single stepping (default)
		BCCFGEBR_STEP         = 0x00002000L,      //Enable single stepping
		BCCFGEBR_MC01         = 0x00000000L,      //Select SA=00000 or SA=11111 for mode codes (default)
		BCCFGEBR_MC1          = 0x01000000L,      //Select SA=11111 for mode codes
		BCCFGEBR_MC0          = 0x02000000L,      //Select SA=00000 for mode codes
		BCCFGEBR_MCNONE       = 0x03000000L,      //Disable mode codes
		BCCFGEBR_PAUSE        = 0x00000400L,      //Mark channel as paused
		BCCFGEBR_UNPAUSE      = 0x00000000L,      //Mark channel as unpaused (default)
		BCCFGEBR_SELFTEST     = 0x00800000L,      //This channel will transmit/receive on the internal self-test bus
		BCCFGEBR_SELFTESTOFF  = 0x00000000L       //This channel will transmit/receive on the operational bus (default)
};

/**
*
*  Monitor configuration options
*
**/

enum {	MONCFGEBR_DEFAULT     = 0x00000000L,      //Select all default settings
		MONCFGEBR_ENABLE      = 0x00000000L,      //Enable monitor
		MONCFGEBR_DISABLE     = 0x00000001L,      //Disable monitor
		MONCFGEBR_NOBCAST     = 0x00000000L,      //Disable broadcast (default)
		MONCFGEBR_BCAST       = 0x00000100L,      //Enable broadcast
		MONCFGEBR_COMPLETE    = 0x00000000L,      //Enable receiving complete words (default)
		MONCFGEBR_INCOMPLETE  = 0x00010000L,      //Enable receiving incomplete words
		MONCFGEBR_MC01        = 0x00000000L,      //Select SA=00000 or SA=11111 for mode codes (default)
		MONCFGEBR_MC1         = 0x00100000L,      //Select SA=11111 for mode codes
		MONCFGEBR_MC0         = 0x00200000L,      //Select SA=00000 for mode codes
		MONCFGEBR_MCNONE      = 0x00300000L,      //Disable mode codes
		MONCFGEBR_SELFTEST    = 0x00800000L,      //This channel will transmit/receive on the internal self-test bus
		MONCFGEBR_SELFTESTOFF = 0x00000000L       //This channel will transmit/receive on the operational bus (default)
};

/**
*
*  Remote terminal configuration options
*
**/

enum {	RTCFGEBR_DEFAULT      = 0x00000000L,      //Select all default settings
		RTCFGEBR_SIMULATE     = 0x00000000L,      //Enable RT simulation (default)
		RTCFGEBR_DISABLE      = 0x00000001L,      //Disable RT
		RTCFGEBR_MONITOR      = 0x00000002L,      //Enable RT monitor
		RTCFGEBR_NOBCAST      = 0x00000000L,      //Disable broadcast (default)
		RTCFGEBR_BCAST        = 0x00000100L,      //Enable broadcast
		RTCFGEBR_NOAUTOBUSY   = 0x00000000L,      //Disable auto busy (default)
		RTCFGEBR_AUTOBUSY     = 0x00000200L,      //Enable auto busy
		RTCFGEBR_BUILD        = 0x00000000L,      //Enable auto building (default)
		RTCFGEBR_NOBUILD      = 0x00000400L,      //Disable auto building
		RTCFGEBR_NOIMMCLR     = 0x00000000L,      //Do not clear status word bits (default)
		RTCFGEBR_IMMCLR       = 0x00002000L,      //Status word bits are cleared immediately
		RTCFGEBR_NOBCASTADDR  = 0x00000000L,      //Disable broadcast handling for address (default)
		RTCFGEBR_BCASTADDR    = 0x00004000L,      //Enable broadcast handling for address
		RTCFGEBR_MC01         = 0x00000000L,      //Select SA=00000 or SA=11111 for mode codes (default)
		RTCFGEBR_MC1          = 0x01000000L,      //Select SA=11111 for mode codes
		RTCFGEBR_MC0          = 0x02000000L,      //Select SA=00000 for mode codes
		RTCFGEBR_MCNONE       = 0x03000000L,      //Disable mode codes
		RTCFGEBR_MCILLEGAL    = 0x04000000L,      //Illegalize mode codes defined as "Reserved - Not to be Used" in EBR
		RTCFGEBR_WIPE         = 0x00000000L,      //Enables message clear (default)
		RTCFGEBR_NOWIPE       = 0x80000000L,      //Disables message clear
		RTCFGEBR_WIPE0        = 0x00000000L,      //Initialize data with zeros (default)
		RTCFGEBR_WIPE123      = 0x10000000L,      //Initialize data with incrementing values
		RTCFGEBR_WIPECWD      = 0x20000000L,      //Initialize data with command word
		RTCFGEBR_SELFTEST     = 0x00800000L,      //This channel will transmit/receive on the internal self-test bus
		RTCFGEBR_SELFTESTOFF  = 0x00000000L       //This channel will transmit/receive on the operational bus (default)
};

/**
*
*  Playback configuration options
*
**/

enum {	PLAYCFGEBR_DEFAULT     = 0x00000000L,      //Select all default settings
		PLAYCFGEBR_ENABLE      = 0x00000000L,      //Enable Playback mode
		PLAYCFGEBR_DISABLE     = 0x00000001L,      //Disable Playback mode
		PLAYCFGEBR_16K         = 0x00000000L,      //Allocate a 16K playback buffer (default)
		PLAYCFGEBR_ALLAVAIL    = 0x00000010L,      //Allocate all available memory to playback buffer
		PLAYCFGEBR_32K         = 0x00000020L,      //Allocate a 32K playback buffer
		PLAYCFGEBR_64K         = 0x00000040L,      //Allocate a 64K playback buffer
		PLAYCFGEBR_128K        = 0x00000080L,      //Allocate a 128K playback buffer
		PLAYCFGEBR_MC01        = 0x00000000L,      //Select SA=00000 or SA=11111 for mode codes (default)
		PLAYCFGEBR_MC1         = 0x01000000L,      //Select SA=11111 for mode codes
		PLAYCFGEBR_MC0         = 0x02000000L,      //Select SA=00000 for mode codes
		PLAYCFGEBR_MCNONE      = 0x03000000L,      //Disable mode codes
};

/**
*
*  Serial configuration options
*
**/

enum {	SERCFGEBR_DEFAULT     = 0x00000000L,      //Select all default settings
		SERCFGEBR_CONTINUOUS  = 0x00000000L,      //Enable continuous mode (default)
		SERCFGEBR_DISABLE     = 0x00000001L,      //Disable free mode
		SERCFGEBR_FREE        = 0x00000002L,      //Enable free mode
		SERCFGEBR_NOLOGEMPTY  = 0x00000000L,      //Do not generate event log when empty (default)
		SERCFGEBR_LOGEMPTY    = 0x00000010L       //Generate event log when empty
};

/**
*
*  Error configuration options
*
**/

enum {	ERRDEFEBR_NONE        = 0x00000000L,      //Disable error generation (default)
		ERRDEFEBR_GAP         = 0x00010000L,      //Generate preceding gap error
		ERRDEFEBR_CNTWRDABS   = 0x00020000L,      //Generate an absolute word count error
		ERRDEFEBR_CNTWRDREL   = 0x00040000L,      //Generate a relative count error
		ERRDEFEBR_CNTBIT      = 0x00080000L,      //Generate a bit count error
		ERRDEFEBR_MAN1        = 0x00100000L,      //Generate manchester error on first half of bit
		ERRDEFEBR_MAN2        = 0x00200000L,      //Generate manchester error on second half of bit
		ERRDEFEBR_SYNC        = 0x00400000L,      //Generate a sync error
		ERRDEFEBR_PAR         = 0x00800000L,      //Generate a parity error
		ERRDEFEBR_ZEROX1      = 0x01000000L,      //Generate zero crossing error on leading zero crossing (if exists)
		ERRDEFEBR_ZEROX2      = 0x02000000L,      //Generate zero crossing error on mid-bit zero crossing

		ERRDEFEBR_DWD00       = 0x0000,           //Error will be generated in data word  0
		ERRDEFEBR_DWD01       = 0x0001,           //Error will be generated in data word  1
		ERRDEFEBR_DWD02       = 0x0002,           //Error will be generated in data word  2
		ERRDEFEBR_DWD03       = 0x0003,           //Error will be generated in data word  3
		ERRDEFEBR_DWD04       = 0x0004,           //Error will be generated in data word  4
		ERRDEFEBR_DWD05       = 0x0005,           //Error will be generated in data word  5
		ERRDEFEBR_DWD06       = 0x0006,           //Error will be generated in data word  6
		ERRDEFEBR_DWD07       = 0x0007,           //Error will be generated in data word  7
		ERRDEFEBR_DWD08       = 0x0008,           //Error will be generated in data word  8
		ERRDEFEBR_DWD09       = 0x0009,           //Error will be generated in data word  9
		ERRDEFEBR_DWD10       = 0x000A,           //Error will be generated in data word 10
		ERRDEFEBR_DWD11       = 0x000B,           //Error will be generated in data word 11
		ERRDEFEBR_DWD12       = 0x000C,           //Error will be generated in data word 12
		ERRDEFEBR_DWD13       = 0x000D,           //Error will be generated in data word 13
		ERRDEFEBR_DWD14       = 0x000E,           //Error will be generated in data word 14
		ERRDEFEBR_DWD15       = 0x000F,           //Error will be generated in data word 15
		ERRDEFEBR_DWD16       = 0x0010,           //Error will be generated in data word 16
		ERRDEFEBR_DWD17       = 0x0011,           //Error will be generated in data word 17
		ERRDEFEBR_DWD18       = 0x0012,           //Error will be generated in data word 18
		ERRDEFEBR_DWD19       = 0x0013,           //Error will be generated in data word 19
		ERRDEFEBR_DWD20       = 0x0014,           //Error will be generated in data word 20
		ERRDEFEBR_DWD21       = 0x0015,           //Error will be generated in data word 21
		ERRDEFEBR_DWD22       = 0x0016,           //Error will be generated in data word 22
		ERRDEFEBR_DWD23       = 0x0017,           //Error will be generated in data word 23
		ERRDEFEBR_DWD24       = 0x0018,           //Error will be generated in data word 24
		ERRDEFEBR_DWD25       = 0x0019,           //Error will be generated in data word 25
		ERRDEFEBR_DWD26       = 0x001A,           //Error will be generated in data word 26
		ERRDEFEBR_DWD27       = 0x001B,           //Error will be generated in data word 27
		ERRDEFEBR_DWD28       = 0x001C,           //Error will be generated in data word 28
		ERRDEFEBR_DWD29       = 0x001D,           //Error will be generated in data word 29
		ERRDEFEBR_DWD30       = 0x001E,           //Error will be generated in data word 30
		ERRDEFEBR_DWD31       = 0x001F,           //Error will be generated in data word 31
		ERRDEFEBR_CWD         = 0x0100,           //Error will be generated in CWD
		ERRDEFEBR_SWD         = 0x0102,           //Error will be generated in SWD
};

/**
*
*  Error control options
*
**/

enum {	ERRCTRLEBR_OFF        = 0x00000000L,      //Disable error generation
		ERRCTRLEBR_ON         = 0x00000001L,      //Enable error generation
		ERRCTRLEBR_ONCE       = 0x00000002L,      //Enable error generation once
		ERRCTRLEBR_TAGMSG     = 0x00000000L,      //Generate errors on tagged messages
		ERRCTRLEBR_ANYMSG     = 0x80000000L       //Generate errors on any message
};

/**
*
*  Message configuration options
*
**/

enum {	MSGCRTEBR_DEFAULT     = 0x00000000L,      //Default settings
		MSGCRTEBR_ENABLE      = 0x00000000L,      //Enable subaddress (RT) (default)
		MSGCRTEBR_DISABLE     = 0x00001000L,      //Disable subaddress (RT)
		MSGCRTEBR_RESP        = 0x00000000L,      //Enable response (RT) (default)
		MSGCRTEBR_NORESP      = 0x00002000L,      //Disable response (RT)
		MSGCRTEBR_NOWRAP      = 0x00000000L,      //Disable data wrap (RT) (default)
		MSGCRTEBR_WRAP        = 0x00004000L,      //Enable data wrap (RT)
		MSGCRTEBR_NOLOG       = 0x00000000L,      //Message will not generate event log (default)
		MSGCRTEBR_LOG         = 0x00000001L,      //Message will generate event log
		MSGCRTEBR_NOERR       = 0x00000000L,      //No message error will be generated for message (default)
		MSGCRTEBR_ERR         = 0x00000002L,      //A message error will be generated for message
		MSGCRTEBR_BCRT        = 0x00000000L,      //Message is a BC-RT transfer (BC) (default)
		MSGCRTEBR_NOMON       = 0x00000000L,      //Message will not be monitored (default)
		MSGCRTEBR_MON         = 0x00000010L,      //Message will be monitored
		MSGCRTEBR_NOTIMETAG   = 0x00000000L,      //Message will not record time-tag (default)
		MSGCRTEBR_TIMETAG     = 0x00000040L,      //Message will record time-tag
		MSGCRTEBR_NOELAPSE    = 0x00000000L,      //Message will not record elapse time (default)
		MSGCRTEBR_ELAPSE      = 0x00000080L,      //Message will record elapse time
		MSGCRTEBR_NOMIN       = 0x00000000L,      //Message will not record min time (default)
		MSGCRTEBR_MIN         = 0x00000100L,      //Message will record min time
		MSGCRTEBR_NOMAX       = 0x00000000L,      //Message will not record max time (default)
		MSGCRTEBR_MAX         = 0x00000200L,      //Message will record max time
		MSGCRTEBR_NOHIT       = 0x00000000L,      //Message will not record hit count (default)
		MSGCRTEBR_HIT         = 0x00000400L,      //Message will record hit count
		MSGCRTEBR_WIPE        = 0x00000000L,      //Enables message clear (default)
		MSGCRTEBR_NOWIPE      = 0x80000000L,      //Disables message clear
		MSGCRTEBR_WIPE0       = 0x00000000L,      //Initialize data with zeros (default)
		MSGCRTEBR_WIPE123     = 0x01000000L,      //Initialize data with incrementing values
		MSGCRTEBR_WIPECWD     = 0x02000000L       //Initialize data with command word
};

/**
*
*  List buffer options
*
**/

enum {	LISTCRTEBR_DEFAULT    = 0x00000000L,      //Select all default settings
		LISTCRTEBR_FIFO       = 0x00000000L,      //Enable FIFO mode
		LISTCRTEBR_CIRCULAR   = 0x00000001L,      //Enable circular mode
		LISTCRTEBR_TRBIT      = 0x00000000L,      //User will read or write depending upon T/R bit
		LISTCRTEBR_READ       = 0x00000010L,      //User will read from list buffer
		LISTCRTEBR_WRITE      = 0x00000020L,      //User will write to list buffer
		LISTCRTEBR_NOTSKIP    = 0x00000000L,      //Will not skip messages
		LISTCRTEBR_SKIP       = 0x00000040L,      //Enable message skipping
		LISTCRTEBR_ASYNC      = 0x00000080L,      //Asynchronous mode
		LISTCRTEBR_NOLOG      = 0x00000000L,      //Do not generate event log when list buffer empty/full (default)
		LISTCRTEBR_LOG        = 0x00000100L,      //Generate event log when list buffer empty/full
		LISTCRTEBR_NOHIT      = 0x00000000L,      //Do not generate event log when list is hit (default)
		LISTCRTEBR_HIT        = 0x00000200L,      //Generate event log when list is hit
		LISTCRTEBR_NOLOGHALF  = 0x00000000L,      //Do not generate event log on middle and last entries (default)
		LISTCRTEBR_LOGHALF    = 0x00000400L       //Generate event log on middle and last entries
};

/**
*
*  Condition flags.
*
**/

enum {	CONDEBR_ALWAYS        = 0x00000000L,      //Unconditional
		CONDEBR_FAIL          = 0x00000001L,      //Condition when all retries have failed
		CONDEBR_SRQ           = 0x00000002L,      //Condition on Service Request bit set in status word
		CONDEBR_INS           = 0x00000004L,      //Condition on Instrumentation bit set in status word
		CONDEBR_SSF           = 0x00000008L,      //Condition on Subsystem Fail bit set in status word
		CONDEBR_TF            = 0x00000010L,      //Condition on Terminal Flag bit set in status word
		CONDEBR_BUSY          = 0x00000020L,      //Condition on Busy bit set in status word
		CONDEBR_ME            = 0x00000040L,      //Condition on Message Error bit set in status word
		CONDEBR_RESPERR       = 0x00000080L,      //Condition on response error
		CONDEBR_NORESP        = 0x00000100L,      //Condition on no response error
		CONDEBR_DIO1ACT       = 0x00001000L,      //Condition on digital I/O #1 active
		CONDEBR_DIO1NACT      = 0x00002000L,      //Condition on digital I/O #1 not active
		CONDEBR_DIO2ACT       = 0x00004000L,      //Condition on digital I/O #2 active
		CONDEBR_DIO2NACT      = 0x00008000L,      //Condition on digital I/O #2 not active
		CONDEBR_DIO3ACT       = 0x00010000L,      //Condition on digital I/O #3 active
		CONDEBR_DIO3NACT      = 0x00020000L,      //Condition on digital I/O #3 not active
		CONDEBR_DIO4ACT       = 0x00040000L,      //Condition on digital I/O #4 active
		CONDEBR_DIO4NACT      = 0x00080000L       //Condition on digital I/O #4 not active
};

/**
*
*  Bit fields of the status word
*
**/

enum {	SWDEBR_TAMASK         = 0xF800,           //Masks off RT address in the SWD
		SWDEBR_ME             = 0x0400,           //Message error bit
		SWDEBR_INST           = 0x0200,           //Instrumentation bit
		SWDEBR_SRQ            = 0x0100,           //Service request bit
		SWDEBR_BCAST          = 0x0010,           //Broadcast received bit
		SWDEBR_BUSY           = 0x0008,           //Busy bit
		SWDEBR_SSF            = 0x0004,           //Subsystem flag bit
		SWDEBR_DBC            = 0x0002,           //Dynamic bus control acceptance bit
		SWDEBR_TF             = 0x0001            //Terminal flag bit
};

/**
*
*  Bit fields of the message error flag
*
**/

enum {	MSGERREBR_NORESP      = 0x8000,           //Set when we timeout waiting for response
		MSGERREBR_ANYERR      = 0x4000,           //Set when any error bit is set
		MSGERREBR_PROTOCOL    = 0x2000,           //General protocol error
		MSGERREBR_SYNC        = 0x1000,           //Set when we have the wrong sync
		MSGERREBR_DATACOUNT   = 0x0800,           //Set when improper number of data words
		MSGERREBR_LHUB        = 0x0040,           //Set when unexpected LHUB activity detected
		MSGERREBR_MANCH       = 0x0020,           //Set when manchester error detected
		MSGERREBR_PARITY      = 0x0010,           //Set when parity error detected
		MSGERREBR_WORD        = 0x0008,           //Set when word error detected
		MSGERREBR_RETRY       = 0x0004,           //All retries failed
		MSGERREBR_SYSTEM      = 0x0002,           //An internal system error occurred
		MSGERREBR_HIT         = 0x0001            //Always set
};

/**
*
*  Bit fields of the message activity flag.
*
**/

enum {	MSGACTEBR_CHMASK      = 0xF800,           //Channel number mask value
		MSGACTEBR_CHSHIFT     = 0x000B,           //Channel number shift value
		MSGACTEBR_XMTCWD      = 0x0080,           //Transmitted CWD
		MSGACTEBR_XMTSWD      = 0x0040,           //Transmitted SWD
		MSGACTEBR_RCVCWD      = 0x0020,           //Received CWD
		MSGACTEBR_RCVSWD      = 0x0010,           //Received SWD
		MSGACTEBR_XMTDWD      = 0x0008,           //Transmitted DWD
		MSGACTEBR_RCVDWD      = 0x0004,           //Received DWD
		MSGACTEBR_MODEMASK    = 0x0003,           //LHub Mode mask value
		MSGACTEBR_MODESHIFT   = 0x0000            //LHub Mode shift value
};

/**
*
*  Bit fields within the command info (cwdinfo).
*
**/

enum {
	    CWDINFOEBR_RESVMASK     = 0xFFE0,         //Reserved mask value
	    CWDINFOEBR_LINKMASK     = 0x001F,         //Link number mask value
		CWDINFOEBR_LINKSHIFT    = 0x0000          //Link number shift value
};

/**
*
*  Bit fields within the status info (swdinfo).
*
**/

enum {
	    SWDINFOEBR_RESVMASK     = 0xFFE0,         //Reserved mask value
	    SWDINFOEBR_LINKMASK     = 0x001F,         //Link number mask value
		SWDINFOEBR_LINKSHIFT    = 0x0000          //Link number shift value
};

/**
*
*  Message activity LHub Mode values.
*
**/

enum {	LHUBMODE_SPEC         = 0x0000,           //LHub mode is Spec
		LHUBMODE_SWITCH       = 0x0001,           //LHub mode is Switch
		LHUBMODE_LINK         = 0x0002            //LHub mode is Link
};

/**
*
*  Field types.
*
**/

enum {	FIELDEBR_CWD          = 0x0000,           //CWD field
		FIELDEBR_CWDINFO      = 0x0001,           //CWD info field
		FIELDEBR_SWD          = 0x0002,           //SWD field
		FIELDEBR_SWDINFO      = 0x0003,           //SWD info field
		FIELDEBR_TTAG         = 0x0004,           //Time tag field
		FIELDEBR_ELAPSE       = 0x0005,           //Elapse time field
		FIELDEBR_ERROR        = 0x0006,           //Error field
		FIELDEBR_ACT          = 0x0007,           //Activity field
		FIELDEBR_RESP         = 0x0008,           //Response time field
		FIELDEBR_COUNT        = 0x0009,           //Data count field
		FIELDEBR_FLAG1        = 0x000A,           //Flag 1 field
		FIELDEBR_FLAG2        = 0x000B,           //Flag 2 field
		FIELDEBR_EXTFLAG      = 0x000C,           //External flag field
		FIELDEBR_TTAGH        = 0x000D            //Extended time tag field
};

/**
*
*  Channel info fields.
*
**/

enum {	INFOEBR_MODEL         = 0x0001,            //Channel model type
		INFOEBR_MAXCOUNT      = 0x0002,            //Channel terminal count
		INFOEBR_MULTIMODE     = 0x0003,            //Channel supports multi-mode
		INFOEBR_ERRORGEN      = 0x0004,            //Channel supports error generation
		INFOEBR_CONCURMON     = 0x0005,            //Channel supports concurrent monitor
		INFOEBR_MONFILTER     = 0x0006,            //Channel supports filtering of monitor
		INFOEBR_PARAM         = 0x0007,            //Channel supports parametric control
		INFOEBR_RTRESPTIME    = 0x0008,            //Channel supports programmable RT response time
		INFOEBR_PLAYBACK      = 0x0009,            //Channel supports hardware playback mode
		INFOEBR_VER2CHOBJ     = 0x000A,            //Channel supports version 2 channel object
		INFOEBR_BCDFLTGAP     = 0x000B             //Channel supports programmable default gap time
};

/**
*
*  Other flags.
*
**/

#define SUBADDRESS 0x0000                          //Selects Subaddress messages
#define MODECODE   0x0001                          //Selects Modecode messages
#define EBR_DISABLED -1                            //Flag setting/indicating a disabled RT link or port

#endif
