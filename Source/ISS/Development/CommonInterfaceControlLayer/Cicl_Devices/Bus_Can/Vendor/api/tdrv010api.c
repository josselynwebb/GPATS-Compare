/* $Id: tdrv010api.c 27851 2020-07-06 20:01:53Z wileyj $ */
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
**    File             tdrv010api.c                                          **
**                                                                           **
**    Function         TDRV010 Application Programming Interface             **
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
**                     Copyright (c) 2010-2013                               **
**                     TEWS TECHNOLOGIES GmbH                                **
**                                                                           **
**                                                                           **
*******************************************************************************
******************************************************************************/
#include <windows.h>
#include <string.h>
#include <stdio.h>
#include <conio.h>
#include <stdlib.h>
#include <winioctl.h>

#include "tdrv010api.h"



#ifndef TRUE
#define TRUE (1==1)
#endif
#ifndef FALSE
#define FALSE (1==0)
#endif

#define TDRV010_NOWAIT_TXTIMEOUT    2

/*****************************************************************************
*
* tdrv010Open -  Open TDRV010 Device
*
* RETURNS: a valid device handle on success or NULL if open failed
*
* ERRNO: appropriate error code if open failed
*/
TDRV010_HANDLE tdrv010Open(
                           char* devicename
                          )
{
    return CreateFile(devicename,
                      GENERIC_READ | GENERIC_WRITE,
					  0,
                      NULL,                 // no security attrs
                      OPEN_EXISTING,        // device always open existing
                      FILE_FLAG_OVERLAPPED, // overlapped I/O possible
                      NULL 
                     );
}

/*****************************************************************************
*
* tdrv010Close -  Close TDRV010 Device
*
* RETURNS: OK on success or ERROR if write failed
*
* ERRNO: appropriate error code if close failed
*/
int tdrv010Close(
                 TDRV010_HANDLE FileDescriptor
                )
{
    return CloseHandle( FileDescriptor );
}


/*****************************************************************************
*
* tdrv010Read -  Read a CAN message from TDRV010 device
*
* RETURNS: 0 on success or appropriate error code if read failed
*/
int tdrv010Read
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    ULONG           timeout,
    ULONG           *pIdentifier,
    UCHAR           *pIOFlags,
    UCHAR           *pStatus,
    int             *pLength,
    UCHAR           *pData
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_MSG_BUF     rcvMsgBuf;
    DWORD                 numRead;

    if ((pIdentifier == NULL) ||
        (pIOFlags == NULL) ||
        (pStatus == NULL) ||
        (pLength == NULL) ||
        (pData == NULL))
    {
        return -ERROR_INVALID_PARAMETER;
    }

    rcvMsgBuf.channel = canChan;
    rcvMsgBuf.noWait  = FALSE;
    rcvMsgBuf.Timeout = timeout;

    success = DeviceIoControl(FileDescriptor,       //  TDRV010 handle
                              IOCTL_TDRV010_READ,   //  control code
                              &rcvMsgBuf,
                              sizeof(rcvMsgBuf),
                              &rcvMsgBuf,
                              sizeof(rcvMsgBuf),
                              &numRead,
                              NULL);
    if (success)
    {
        *pIdentifier = rcvMsgBuf.Identifier;
        *pIOFlags = rcvMsgBuf.IOFlags;
        *pStatus  = rcvMsgBuf.Status;
        *pLength  = rcvMsgBuf.MsgLen;
        memcpy(pData, &rcvMsgBuf.Data, *pLength);

        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010ReadNoWait -  Read a CAN message from TDRV010 device, do not wait also
*                      if there is no message available
*
* RETURNS: 0 on success or appropriate error code if read failed
*/
int tdrv010ReadNoWait
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    ULONG           *pIdentifier,
    UCHAR           *pIOFlags,
    UCHAR           *pStatus,
    int             *pLength,
    UCHAR           *pData
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_MSG_BUF     rcvMsgBuf;
    DWORD                 numRead;

    if ((pIdentifier == NULL) ||
        (pIOFlags == NULL) ||
        (pStatus == NULL) ||
        (pLength == NULL) ||
        (pData == NULL))
    {
        return -ERROR_INVALID_PARAMETER;
    }

    rcvMsgBuf.channel = canChan;
    rcvMsgBuf.noWait  = TRUE;
    rcvMsgBuf.Timeout = 0;

    success = DeviceIoControl(FileDescriptor,       //  TDRV010 handle
                              IOCTL_TDRV010_READ,   //  control code
                              &rcvMsgBuf,
                              sizeof(rcvMsgBuf),
                              &rcvMsgBuf,
                              sizeof(rcvMsgBuf),
                              &numRead,
                              NULL);
    if (success)
    {
        *pIdentifier = rcvMsgBuf.Identifier;
        *pIOFlags = rcvMsgBuf.IOFlags;
        *pStatus  = rcvMsgBuf.Status;
        *pLength  = rcvMsgBuf.MsgLen;
        memcpy(pData, &rcvMsgBuf.Data, *pLength);

        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010Write -  Send a CAN message on TDRV010 device
*
* RETURNS: 0 on success or appropriate error code if read failed
*/
int tdrv010Write
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    ULONG           timeout,
    ULONG           identifier,
    UCHAR           IOFlags,
    int             length,
    UCHAR           *pData
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_MSG_BUF     sndMsgBuf;
    DWORD                 numRead;

    sndMsgBuf.channel    = canChan;
    sndMsgBuf.Identifier = identifier;
    sndMsgBuf.MsgLen     = length <= 8 ? length : 8;
    memcpy(&sndMsgBuf.Data, pData, length <= 8 ? length : 8);
    sndMsgBuf.IOFlags    = IOFlags;
    sndMsgBuf.noWait     = FALSE;
    sndMsgBuf.Timeout    = timeout;

    success = DeviceIoControl(FileDescriptor,       //  TDRV010 handle
                              IOCTL_TDRV010_WRITE,  //  control code
                              &sndMsgBuf,
                              sizeof(sndMsgBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010WriteNoWait -  Send a CAN message on TDRV010 device, return immediately
*
* RETURNS: 0 on success or appropriate error code if read failed
*/
int tdrv010WriteNoWait
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    ULONG           identifier,
    UCHAR           IOFlags,
    int             length,
    UCHAR           *pData
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_MSG_BUF     sndMsgBuf;
    DWORD                 numRead;

    sndMsgBuf.channel    = canChan;
    sndMsgBuf.Identifier = identifier;
    sndMsgBuf.MsgLen     = length;
    memcpy(&sndMsgBuf.Data, pData, length);
    sndMsgBuf.IOFlags    = IOFlags;
    sndMsgBuf.noWait     = TRUE;
    sndMsgBuf.Timeout    = TDRV010_NOWAIT_TXTIMEOUT;

    success = DeviceIoControl(FileDescriptor,       //  TDRV010 handle
                              IOCTL_TDRV010_WRITE,  //  control code
                              &sndMsgBuf,
                              sizeof(sndMsgBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010SetBitTiming -  Set controller bit timings and baudrate 
*
* RETURNS: 0 on success or appropriate error code if setting failed
*
*/
int tdrv010SetBitTiming
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    USHORT          timingValue,
    BOOLEAN         useThreeSamples
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_TIMING      timingBuf;
    DWORD                 numRead;

    timingBuf.channel      = canChan;
    timingBuf.TimingValue  = timingValue;
    timingBuf.ThreeSamples = useThreeSamples;

    success = DeviceIoControl(FileDescriptor,           //  TDRV010 handle
                              IOCTL_TDRV010_BITTIMING,  //  control code
                              &timingBuf,
                              sizeof(timingBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010SetFilter -  Set acceptance filter masks 
*                     This functions writes new filter values directly to the
*                     corresponding CAN controller registers.
*
* RETURNS: 0 on success or appropriate error code if setting failed
*
*/
int tdrv010SetFilter
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    BOOLEAN         singleFilter,
    ULONG           acceptanceCode,
    ULONG           acceptanceMask
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_FILTER      filterBuf;
    DWORD                 numRead;

    filterBuf.channel        = canChan;
    filterBuf.SingleFilter   = singleFilter;
    filterBuf.AcceptanceCode = acceptanceCode;
    filterBuf.AcceptanceMask = acceptanceMask;

    success = DeviceIoControl(FileDescriptor,           //  TDRV010 handle
                              IOCTL_TDRV010_SETFILTER,  //  control code
                              &filterBuf,
                              sizeof(filterBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010Start -  brings the device online to be an active CAN node
*
* RETURNS: 0 on success or appropriate error code if BUSON failed
*
*/
int tdrv010Start
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_DEFAULTBUF  chanBuf;
    DWORD                 numRead;

    chanBuf.channel = canChan;

    success = DeviceIoControl(FileDescriptor,       //  TDRV010 handle
                              IOCTL_TDRV010_BUSON,  //  control code
                              &chanBuf,
                              sizeof(chanBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010Stop -  takes the device offline and enter the bus off state
*
* RETURNS: 0 on success or appropriate error code if BUSOFF failed
*
*/
int tdrv010Stop
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_DEFAULTBUF  chanBuf;
    DWORD                 numRead;

    chanBuf.channel = canChan;

    success = DeviceIoControl(FileDescriptor,       //  TDRV010 handle
                              IOCTL_TDRV010_BUSOFF, //  control code
                              &chanBuf,
                              sizeof(chanBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010FlushReceiveFifo - discard all messages in the receive FIFO
*
* RETURNS: 0 on success or appropriate error code if flush failed
*
*/
int tdrv010FlushReceiveFifo
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_DEFAULTBUF  chanBuf;
    DWORD                 numRead;

    chanBuf.channel = canChan;

    success = DeviceIoControl(FileDescriptor,       //  TDRV010 handle
                              IOCTL_TDRV010_FLUSH,  //  control code
                              &chanBuf,
                              sizeof(chanBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010GetControllerStatus - get contents of the controller status register
*
* RETURNS: 0 on success or appropriate error code if status read failed
*/
int tdrv010GetControllerStatus
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    TDRV010_STATUS  *pCANStatus
)
{
    BOOLEAN             success;
    int                 result;
    DWORD                 numRead;

    if (pCANStatus == NULL)
    {
        return ERROR_INVALID_PARAMETER;
    }
	{
		int iii = sizeof(*pCANStatus);
		iii = sizeof(TDRV010_STATUS);
		iii = 0;
	}

    pCANStatus->channel = canChan;

    success = DeviceIoControl(FileDescriptor,           //  TDRV010 handle
                              IOCTL_TDRV010_CANSTATUS,  //  control code
                              pCANStatus,
                              sizeof(*pCANStatus),
                              pCANStatus,
                              sizeof(*pCANStatus),
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010SelftestEnable - Enable Controller Selftest Mode
*
* RETURNS: 0 on success or appropriate error code if selftest enable failed
*
*/
int tdrv010SelftestEnable
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_DEFAULTBUF  chanBuf;
    DWORD                 numRead;

    chanBuf.channel = canChan;

    success = DeviceIoControl(FileDescriptor,                   //  TDRV010 handle
                              IOCTL_TDRV010_ENABLE_SELFTEST,    //  control code
                              &chanBuf,
                              sizeof(chanBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010SelftestDisable - Disable Controller Selftest Mode
*
* RETURNS: 0 on success or appropriate error code if selftest disable failed
*
*/
int tdrv010SelftestDisable
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_DEFAULTBUF  chanBuf;
    DWORD                 numRead;

    chanBuf.channel = canChan;

    success = DeviceIoControl(FileDescriptor,                   //  TDRV010 handle
                              IOCTL_TDRV010_DISABLE_SELFTEST,   //  control code
                              &chanBuf,
                              sizeof(chanBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010ListenOnlyEnable - Enable Controller Listen-Only Mode
*
* RETURNS: 0 on success or appropriate error code if Listen-Only enable failed
*
*/
int tdrv010ListenOnlyEnable
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_DEFAULTBUF  chanBuf;
    DWORD                 numRead;

    chanBuf.channel = canChan;

    success = DeviceIoControl(FileDescriptor,                   //  TDRV010 handle
                              IOCTL_TDRV010_ENABLE_LISTENONLY,  //  control code
                              &chanBuf,
                              sizeof(chanBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010ListenOnlyDisable - Disable Controller Listen-Only Mode
*
* RETURNS: 0 on success or appropriate error code if Listen-Only disable failed
*
*/
int tdrv010ListenOnlyDisable
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_DEFAULTBUF  chanBuf;
    DWORD                 numRead;

    chanBuf.channel = canChan;

    success = DeviceIoControl(FileDescriptor,                   //  TDRV010 handle
                              IOCTL_TDRV010_DISABLE_LISTENONLY, //  control code
                              &chanBuf,
                              sizeof(chanBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010SetLimit - Set Error warning limit
*
* RETURNS: 0 on success or appropriate error code if setting failed
*
*/
int tdrv010SetLimit
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    UCHAR           errorLimit
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_LIMITBUF    limitBuf;
    DWORD                 numRead;

    limitBuf.channel = canChan;
    limitBuf.limit   = errorLimit;

    success = DeviceIoControl(FileDescriptor,           //  TDRV010 handle
                              IOCTL_TDRV010_SETLIMIT,   //  control code
                              &limitBuf,
                              sizeof(limitBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010CanReset - Set CAN Controller to RESET or OPERATING state
*                   (TPMC310 only)
*
* RETURNS: 0 on success or appropriate error code if reset failed
*
*/
int tdrv010CanReset
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    BOOLEAN         canReset
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_PLDBUF      pldBuf;
    DWORD                 numRead;

    pldBuf.channel  = canChan;
    pldBuf.bitvalue = canReset ? 1 : 0;

    success = DeviceIoControl(FileDescriptor,           //  TDRV010 handle
                              IOCTL_TDRV010_CAN_RESET,  //  control code
                              &pldBuf,
                              sizeof(pldBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010CanSel - Set CAN Controller to SILENT or OPERATING state
*                 (TPMC310 only)
*
* RETURNS: 0 on success or appropriate error code if set failed
*
*/
int tdrv010CanSel
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    BOOLEAN         canSel
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_PLDBUF      pldBuf;
    DWORD                 numRead;

    pldBuf.channel  = canChan;
    pldBuf.bitvalue = canSel ? 1 : 0;

    success = DeviceIoControl(FileDescriptor,           //  TDRV010 handle
                              IOCTL_TDRV010_CAN_SEL,    //  control code
                              &pldBuf,
                              sizeof(pldBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}


/*****************************************************************************
*
* tdrv010CanInt - Enable or disable the CAN Controller's interrupt
*                 (TPMC310 only)
*
* RETURNS: 0 on success or appropriate error code if set failed
*
*/
int tdrv010CanInt
(
    TDRV010_HANDLE  FileDescriptor,
    UCHAR           canChan,
    BOOLEAN         canInt
)
{
    BOOLEAN             success;
    int                 result;
    TDRV010_PLDBUF      pldBuf;
    DWORD                 numRead;

    pldBuf.channel  = canChan;
    pldBuf.bitvalue = canInt ? 1 : 0;

    success = DeviceIoControl(FileDescriptor,           //  TDRV010 handle
                              IOCTL_TDRV010_CAN_INT,    //  control code
                              &pldBuf,
                              sizeof(pldBuf),
                              NULL,
                              0,
                              &numRead,
                              NULL);
    if (success)
    {
        result = 0;
    }
    else
    {
        result = -((int)GetLastError());
    }
    return result;
}
