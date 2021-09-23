/* $Id: tdrv002.h 27851 2020-07-06 20:01:53Z wileyj $ */
/******************************************************************************
*******************************************************************************
**                                                                           **
**                                                                           **
**                     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                     **
**                     @                               @                     **
**                     @          T D R V 0 0 2        @                     **
**                     @                               @                     **
**                     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                     **
**                                                                           **
**                                                                           **
**    Project          Windows - port driver for TEWS TDRV002 family         **
**                                                                           **
**    File             tdrv002.h                                             **
**                                                                           **
**    Function         Include for TDRV002 special ioctl definitions         **
**                                                                           **
**    Owner            TEWS TECHNOLOGIES GmbH                                **
**                     Am Bahnhof 7                                          **
**                     D-25469 Halstenbek                                    **
**                     Germany                                               **
**                                                                           **
**                     Tel.: +49 / (0)4101 / 4058-0                          **
**                     Fax.: +49 / (0)4101 / 4058-19                         **
**                     e-mail: support@tews.com                              **
**                                                                           **
**                     Copyright (c) 2004-2011                               **
**                     TEWS TECHNOLOGIES GmbH                                **
**                                                                           **
**                                                                           **
*******************************************************************************
*******************************************************************************/
#ifndef TDRV002_H
#define TDRV002_H

/* Interface configuration flags */
#define TDRV002_CFG_RS485_RS232     (1 << 0)        /* set RS485 interface, reset RS232 interface is selected */
#define TDRV002_CFG_HDPLX           (1 << 1)        /* set half-duplex interface, reset full-duplex interface is selected */
#define TDRV002_CFG_RENA            (1 << 2)        /* set enable auto RS485 receiver enable, reset disable auto RS485 receiver enable */
#define TDRV002_CFG_RTERM           (1 << 3)        /* set receive termination is enabled, reset receive termination is disabled */
#define TDRV002_CFG_TTERM           (1 << 4)        /* set transmit termination is enabled, reset transmit termination is disabled */
#define TDRV002_CFG_SLEWLIMIT       (1 << 5)        /* set slew limit mode is enabled, reset slew limit mode is disabled */
#define TDRV002_CFG_SHDN            (1 << 6)        /* set transceiver is shut down, reset transceiver works in configured mode */
#define TDRV002_CFG_AUTO_RS485      (1 << 7)        /* set UART controller uses auto RS485 mode, reset UART controller does not use auto RS485 mode */

/* Predefined configurations */
#define TDRV002_INTF_OFF          (TDRV002_CFG_SHDN)            /* Shutdown mode / disable interface  */
#define TDRV002_INTF_RS232        (0)                           /* RS232                              */
#define TDRV002_INTF_RS422        (TDRV002_CFG_RS485_RS232 |    \
                                   TDRV002_CFG_RTERM)           /* RS422 (Multidrop / Full Duplex)    */
#define TDRV002_INTF_RS485FDM     (TDRV002_CFG_RS485_RS232 |    \
                                   TDRV002_CFG_RTERM       |    \
                                   TDRV002_CFG_TTERM)           /* RS485 Full Duplex (Master)         */
#define TDRV002_INTF_RS485FDS     (TDRV002_CFG_RS485_RS232 |    \
                                   TDRV002_CFG_RTERM       |    \
                                   TDRV002_CFG_TTERM       |    \
                                   TDRV002_CFG_AUTO_RS485)      /* RS485 Full Duplex (Slave)          */
#define TDRV002_INTF_RS485HD      (TDRV002_CFG_RS485_RS232 |    \
                                   TDRV002_CFG_HDPLX       |    \
                                   TDRV002_CFG_RENA        |    \
                                   TDRV002_CFG_TTERM       |    \
                                   TDRV002_CFG_AUTO_RS485)      /* RS485 Half Duplex                  */


#define IOCTL_TDRV002_CONF_TRANS            (ULONG)CTL_CODE(FILE_DEVICE_SERIAL_PORT,77,METHOD_BUFFERED,FILE_ANY_ACCESS)
/* Do not use, functions are TDRV002 internals */
#define IOCTL_TDRV002_PROP_PROBEBAUDAVAIL   (ULONG)CTL_CODE(FILE_DEVICE_SERIAL_PORT,78,METHOD_BUFFERED,FILE_ANY_ACCESS)
#define IOCTL_TDRV002_PROP_PROBETRANSPROG   (ULONG)CTL_CODE(FILE_DEVICE_SERIAL_PORT,79,METHOD_BUFFERED,FILE_ANY_ACCESS)
#define IOCTL_TDRV002_PROP_PROBEINTFAVAIL   (ULONG)CTL_CODE(FILE_DEVICE_SERIAL_PORT,80,METHOD_BUFFERED,FILE_ANY_ACCESS)
#define IOCTL_TDRV002_PROP_READFIFOSIZE     (ULONG)CTL_CODE(FILE_DEVICE_SERIAL_PORT,81,METHOD_BUFFERED,FILE_ANY_ACCESS)

#endif  // TDRV002_H
