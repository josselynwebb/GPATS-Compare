/* $Id: tdrv010.h 27851 2020-07-06 20:01:53Z wileyj $ */
/******************************************************************************
*******************************************************************************
**                                                                           **
**                                                                           **
**                          @@@@@@@@@@@@@@@@@@@@@@@                          **
**                          @                     @                          **
**                          @    T D R V 0 1 0    @                          **
**                          @                     @                          **
**                          @@@@@@@@@@@@@@@@@@@@@@@                          **
**                                                                           **
**                                                                           **
**    Project          Windows WDM - TDRV010 Device Driver                   **
**                                                                           **
**                                                                           **
**    File             tdrv010.h                                             **
**                                                                           **
**                                                                           **
**    Function         Driver header file                                    **
**                                                                           **
**                                                                           **
**    Owner            TEWS TECHNOLOGIES                                     **
**                     Am Bahnhof 7                                          **
**                     D-25469 Halstenbek                                    **
**                     Germany                                               **
**                                                                           **
**                                                                           **
**                     Tel.: +49 / (0)4101 / 4058-0                          **
**                     Fax.: +49 / (0)4101 / 4058-19                         **
**                     e-mail: support@tews.com                              **
**                                                                           **
**                                                                           **
**                     Copyright (c) 2005                                    **
**                     TEWS TECHNOLOGIES GmbH                                **
**                                                                           **
**    $Date: 2020-07-06 16:01:53 -0400 (Mon, 06 Jul 2020) $   $Rev: 27851 $      **
**                                                                           **
*******************************************************************************
*******************************************************************************/

#ifndef __TDRV010_H__
#define __TDRV010_H__

#ifdef __cplusplus
extern "C" {
#endif

#include "tdrv010.h"

#define TDRV010_MAKE_IOCTL(c)\
        (ULONG)CTL_CODE(FILE_DEVICE_UNKNOWN, 0x800+(c), METHOD_BUFFERED, FILE_ANY_ACCESS)

//
// DeviceIoControl commands
//
#define IOCTL_TDRV010_READ                TDRV010_MAKE_IOCTL(0)
#define IOCTL_TDRV010_WRITE               TDRV010_MAKE_IOCTL(1)
#define IOCTL_TDRV010_BITTIMING           TDRV010_MAKE_IOCTL(2)
#define IOCTL_TDRV010_SETFILTER           TDRV010_MAKE_IOCTL(3)
#define IOCTL_TDRV010_BUSON               TDRV010_MAKE_IOCTL(4)
#define IOCTL_TDRV010_BUSOFF              TDRV010_MAKE_IOCTL(5)
#define IOCTL_TDRV010_FLUSH               TDRV010_MAKE_IOCTL(6)
#define IOCTL_TDRV010_CANSTATUS           TDRV010_MAKE_IOCTL(7)
#define IOCTL_TDRV010_ENABLE_SELFTEST     TDRV010_MAKE_IOCTL(8)
#define IOCTL_TDRV010_DISABLE_SELFTEST    TDRV010_MAKE_IOCTL(9)
#define IOCTL_TDRV010_ENABLE_LISTENONLY   TDRV010_MAKE_IOCTL(10)
#define IOCTL_TDRV010_DISABLE_LISTENONLY  TDRV010_MAKE_IOCTL(11)
#define IOCTL_TDRV010_SETLIMIT            TDRV010_MAKE_IOCTL(12)

// additional DeviceIoControl commands (TPMC310 only)
#define IOCTL_TDRV010_CAN_RESET           TDRV010_MAKE_IOCTL(13)
#define IOCTL_TDRV010_CAN_SEL             TDRV010_MAKE_IOCTL(14)
#define IOCTL_TDRV010_CAN_INT             TDRV010_MAKE_IOCTL(15)
#define IOCTL_TDRV010_NOT_SUPPORTED       TDRV010_MAKE_IOCTL(16)	// PLD functions are not supported on a TPMC810


/* ----------- Function Datastructures ----------- */

/* -- IOCTL_TDRV010_READ -- */
/* -- IOCTL_TDRV010_WRITE -- */
/*
**  Message buffer status bit definitions
*/
#define TDRV010_SUCCESS               0
#define TDRV010_FIFO_OVERRUN          (1<<0)
#define TDRV010_MSGOBJ_OVERRUN        (1<<1)

/*
**  Message buffer I/O flags
*/
#define TDRV010_STANDARD              0
#define TDRV010_EXTENDED              (1<<0)
#define TDRV010_REMOTE_FRAME          (1<<1)
#define TDRV010_SINGLE_SHOT           (1<<2)
#define TDRV010_SELF_RECEPTION        (1<<3)

/*
**  CAN message read/write buffer
*/
typedef struct
{
    UCHAR channel;                      /* channel number */
    BOOLEAN noWait;                     /* don't wait for message or acknowledge */
    ULONG Identifier;                   /* Message identifier 29-bit or 11-bit */
    UCHAR IOFlags;                      /* Control flags for the read/write operation */
    UCHAR MsgLen;                       /* CAN message length ( 0..8 ) */
    UCHAR Data[8];                      /* up to 8 message bytes */
    ULONG Timeout;                      /* in clock ticks */
    UCHAR Status;                       /* extra message status field */
} TDRV010_MSG_BUF, *PTDRV010_MSG_BUF;

    
    
/* -- IOCTL_TDRV010_BITTIMING -- */
/* -------------- CAN bus bit timing ------------- */
#define TDRV010_1_6MBIT               0x1100  /* 1.6 Mbit/s   max. distance = 20 m    */
#define TDRV010_1MBIT                 0x1400  /*   1 Mbit/s   max. distance = 40 m    */
#define TDRV010_500KBIT               0x1c00  /* 500 Kbit/s   max. distance = 130 m   */
#define TDRV010_250KBIT               0x1c01  /* 250 Kbit/s   max. distance = 270 m   */
#define TDRV010_125KBIT               0x1c03  /* 125 Kbit/s   max. distance = 530 m   */
#define TDRV010_100KBIT               0x2f43  /* 100 Kbit/s   max. distance = 620 m   */
#define TDRV010_50KBIT                0x2f47  /*  50 Kbit/s   max. distance = 1.3 km  */
#define TDRV010_20KBIT                0x2f53  /*  20 Kbit/s   max. distance = 3.3 km  */
#define TDRV010_10KBIT                0x2f67  /*  10 Kbit/s   max. distance = 6.7 km  */
#define TDRV010_5KBIT                 0x7f7f  /*   5 Kbit/s   max. distance = 10 km   */
/*                                        xx......Bit Timing 0    */
/*                                      xx........Bit Timing 1    */


/* Support for additional TPMC310 PLD functionality */
typedef struct
{
	UCHAR channel;						/* channel number */
	UCHAR bitvalue;						/* 0 = sets certain bit to 0, 1 = sets certain bit to 1, bit position in pld control register depends on DeviceIoControl command */
} TDRV010_PLDBUF, *PTDRV010_PLDBUF;

/* Standard functionality for TPMC810 & TPMC310 */
typedef struct
{
    UCHAR channel;                      /* channel number */
    USHORT TimingValue;                 /* CAN bus bit timing */
                                        /* bit 0..7  =  bit timing register 0 */
                                        /* bit 8..15 =  bit timing register 1 */
    BOOLEAN ThreeSamples;               /* TRUE to sample the CAN bus three times */
                                        /* per bit time instead of one time */
} TDRV010_TIMING, *PTDRV010_TIMING;


/* -- IOCTL_TDRV010_SETFILTER -- */
typedef struct
{
    UCHAR channel;                      /* channel number */
    BOOLEAN SingleFilter;               /* TRUE, means use single filter mode instead of a dual filter */
    ULONG AcceptanceCode;
    ULONG AcceptanceMask;               /* a '1' at a bit position means this bit is don't care */
} TDRV010_FILTER, *PTDRV010_FILTER;


/* -- IOCTL_TDRV010_BUSON -- */
/* -- IOCTL_TDRV010_BUSOFF -- */
/* -- IOCTL_TDRV010_FLUSH -- */
/* -- IOCTL_TDRV010_ENABLE_SELFTEST -- */
/* -- IOCTL_TDRV010_DISABLE_SELFTEST -- */
/* -- IOCTL_TDRV010_ENABLE_LISTENONLY -- */
/* -- IOCTL_TDRV010_DISABLE_LISTENONLY -- */
typedef struct
{
    UCHAR channel;                      /* channel number */
} TDRV010_DEFAULTBUF, *PTDRV010_DEFAULTBUF;


/* -- IOCTL_TDRV010_CANSTATUS -- */
typedef struct
{
    UCHAR channel;                      /* channel number */

    UCHAR ArbitrationLostCapture;       /* contents of the arbitration lost register    */
    UCHAR ErrorCodeCapture;             /* contents of the error code cature register   */
    UCHAR TxErrorCounter;               /* contents of the Tx error counter register    */
    UCHAR RxErrorCounter;               /* contents of the Rx error counter register    */

    UCHAR ErrorWarningLimit;            /* contents of the error warning limit register */
    UCHAR StatusRegister;               /* contents of the status register              */
    UCHAR ModeRegister;                 /* contents of the mode register                */

    UCHAR RxMessageCounterMax;          /* peak value of the CAN controller receive FIFO */
} TDRV010_STATUS, *PTDRV010_STATUS;


/* -- IOCTL_TDRV010_SETLIMIT -- */
typedef struct
{
    UCHAR channel;                      /* channel number */
    UCHAR limit;                        /* new limit value */
} TDRV010_LIMITBUF, *PTDRV010_LIMITBUF;



#ifdef __cplusplus
}
#endif


#endif  //  __TDRV010_H__