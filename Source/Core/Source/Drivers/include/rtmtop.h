/*	Data Device Corporation
 *	105 Wilbur Place
 *	Bohemia N.Y. 11716
 *	(631) 567-5600
 *
 *		ENHANCED MINI-ACE 'C' Run Time Library
 *
 *			Copyright (c) 1999 by Data Device Corporation
 *			All Rights Reserved.
 *
 *
 *	RTMTop.h (Combined RT/MT operations header file)
 *
 *
 *
 *	Created 8/26/99 DL
 *
 */



#ifndef __RTMTOP_H__
#define __RTMTOP_H__

#define ACE_RTMT_COMBO_HBUF 0x00000001
#define ACE_RTMT_EXT_TT     0x0028
#define ACE_RTMT_MSGTYPE    0x0029
#define ACE_RTMT_EMA_TT     0x0001

#define ACE_RTMT_MSGTYPE_RT    0x0001
#define ACE_RTMT_MSGTYPE_MT    0x0002
#define ACE_RTMT_MSGTYPE_MERGE 0x0003
#define ACE_RTMT_MSGTYPE_DONE  0x0004

_EXTERN S16BIT _DECL aceRTMTConfigure(S16BIT DevNum,
									  U16BIT wRTCmdStkSize,
									  U16BIT wMTStkType,
									  U16BIT wMTCmdStkSize,
									  U16BIT wMTDataStkSize,
									  U32BIT dwOptions);

_EXTERN S16BIT _DECL aceRTMTStart(S16BIT DevNum);
_EXTERN S16BIT _DECL aceRTMTStop(S16BIT DevNum);

_EXTERN S16BIT _DECL aceRTMTStkToHBuf(S16BIT DevNum);

_EXTERN S16BIT _DECL aceRTMTStkToHBuf32(S16BIT DevNum);

_EXTERN S16BIT _DECL aceRTMTInstallHBuf(S16BIT DevNum,
										U32BIT dwRTMTHBufSize);

_EXTERN S16BIT _DECL aceRTMTUninstallHBuf(S16BIT DevNum);

_EXTERN S16BIT _DECL aceRTMTGetHBufMsgCount(S16BIT DevNum);

_EXTERN S16BIT _DECL aceRTMTGetHBufMsgsRaw(S16BIT DevNum,
							 			   U16BIT *pBuffer,
										   U16BIT wBufferSize,
										   U32BIT *pdwMsgCount,
										   U32BIT *pdwRTMsgLostStk,
										   U32BIT *pdwMTMsgLostStk,
										   U32BIT *pdwMsgLostHBuf);

_EXTERN S16BIT _DECL aceRTMTGetHBufMsgDecoded(S16BIT DevNum,
											  MSGSTRUCT *pMsg,
											  U32BIT *pdwMsgCount,
											  U32BIT *pdwRTMsgLostStk,
											  U32BIT *pdwMTMsgLostStk,
											  U32BIT *pdwMsgLostHBuf,
											  U16BIT wMsgLoc);

_EXTERN S16BIT _DECL aceRTMTGetHBufMetric( S16BIT DevNum,
							 			   HBUFMETRIC *pMetric,
										   U16BIT bReset);

#endif


