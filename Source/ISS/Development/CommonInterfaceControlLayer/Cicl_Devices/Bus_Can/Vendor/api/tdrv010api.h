/* $Id: tdrv010api.h 27851 2020-07-06 20:01:53Z wileyj $ */
/******************************************************************************
*******************************************************************************
**                                                                           **
**                                                                           **
**                          @@@@@@@@@@@@@@@@@@@@@@@                          **
**                          @                     @                          **
**                          @ T D R V 0 1 0 A P I @                          **
**                          @                     @                          **
**                          @@@@@@@@@@@@@@@@@@@@@@@                          **
**                                                                           **
**                                                                           **
**    Project          TDRV010 - Device Driver                               **
**                                                                           **
**    File             tdrv010api.h                                          **
**                                                                           **
**    Description      application programming interface                     **
**                                                                           **
**    Owner            TEWS TECHNOLOGIES GmbH                                **
**                     Am Bahnhof 7                                          **
**                     D-25469 Halstenbek                                    **
**                     Germany                                               **
**                                                                           **
**                     Tel.: +49 / (0)4101 / 4058-0                          **
**                     Fax.: +49 / (0)4101 / 4058-19                         **
**                     EMail: Support@tews.com                               **
**                     Web: http://www.tews.com                              **
**                                                                           **
**                                                                           **
**                     Copyright (c) 2011                                    **
**                     TEWS TECHNOLOGIES GmbH                                **
**                                                                           **
**                                                                           **
*******************************************************************************
*******************************************************************************/
#ifndef __INC_TDRV010API_H
#define __INC_TDRV010API_H

#ifdef __cplusplus
extern "C" {
#endif

#include "tdrv010.h"

#define TDRV010_HANDLE      HANDLE


// Open a TDRV010 device identified by devicename
TDRV010_HANDLE tdrv010Open(char* devicename);

// Close a TDRV010 device
int tdrv010Close(TDRV010_HANDLE FileDescriptor);

// Control functions for TDRV010 device
int tdrv010Read(TDRV010_HANDLE FileDescriptor, UCHAR canChan, ULONG timeout, ULONG *pIdentifier, UCHAR *pIOFlags, UCHAR *pStatus, int *pLength, UCHAR *pData);
int tdrv010ReadNoWait(TDRV010_HANDLE FileDescriptor, UCHAR canChan, ULONG *pIdentifier, UCHAR *pIOFlags, UCHAR *pStatus, int *pLength, UCHAR *pData);
int tdrv010Write(TDRV010_HANDLE FileDescriptor, UCHAR canChan, ULONG timeout, ULONG identifier, UCHAR IOFlags, int length, UCHAR *pData);
int tdrv010WriteNoWait(TDRV010_HANDLE FileDescriptor, UCHAR canChan, ULONG identifier, UCHAR IOFlags, int length, UCHAR *pData);
int tdrv010SetBitTiming(TDRV010_HANDLE FileDescriptor, UCHAR canChan, USHORT timingValue, BOOLEAN useThreeSamples);
int tdrv010SetFilter(TDRV010_HANDLE FileDescriptor, UCHAR canChan, BOOLEAN singleFilter, ULONG acceptanceCode, ULONG acceptanceMask);
int tdrv010Start(TDRV010_HANDLE FileDescriptor, UCHAR canChan);
int tdrv010Stop(TDRV010_HANDLE FileDescriptor, UCHAR canChan);
int tdrv010FlushReceiveFifo(TDRV010_HANDLE FileDescriptor, UCHAR canChan);
int tdrv010GetControllerStatus(TDRV010_HANDLE FileDescriptor, UCHAR canChan, TDRV010_STATUS *pCANStatus);
int tdrv010SelftestEnable(TDRV010_HANDLE FileDescriptor, UCHAR canChan);
int tdrv010SelftestDisable(TDRV010_HANDLE FileDescriptor, UCHAR canChan);
int tdrv010ListenOnlyEnable(TDRV010_HANDLE FileDescriptor, UCHAR canChan);
int tdrv010ListenOnlyDisable(TDRV010_HANDLE FileDescriptor, UCHAR canChan);
int tdrv010SetLimit(TDRV010_HANDLE FileDescriptor, UCHAR canChan, UCHAR errorLimit);
int tdrv010CanReset(TDRV010_HANDLE FileDescriptor, UCHAR canChan, BOOLEAN canReset);
int tdrv010CanSel(TDRV010_HANDLE FileDescriptor, UCHAR canChan, BOOLEAN canSel);
int tdrv010CanInt(TDRV010_HANDLE FileDescriptor, UCHAR canChan, BOOLEAN canInt);


#ifdef __cplusplus
}
#endif

#endif /* __INC_TDRV010API_H */
