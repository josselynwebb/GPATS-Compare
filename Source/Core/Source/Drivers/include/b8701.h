/***************************************************************************** 
 * B8701.H - Include file for:
 *
 * B8701.C - Source code for:
 *
 * RF Measurement Analyzer Function instruments driver
 * (Boonton 8701)
 * VXIPlug&Play WIN/WIN95/WINNT Framework Revision 4.0  
 * Compatible with Instrument Firmware Version 1.5 or later
 * Copyright Mantech 1996,1997, all rights reserved.
 *
 * Modification History:
 *   8/1/95:  Ver. 1.0 - Original Release, S. Mun
 *            Built with National Instruments LabWindows/CVI 4.0
 *            Compatible with VXIplug&play WIN Framework rev. 3.0
 *            Compatible with VXIplug&play VISA rev. 3.0
 *
 * Description                              Function Name
 *
 * Initialize                               b8701_init
 * <Application Functions>
 *    Read Instrument Buffer                b8701_readInstrData
 *    Write Instrument Buffer               b8701_writeInstrData
 * Close                                    b8701_close
*/


#define INSTR_LANGUAGE_SPECIFIC                     /* for language-specific code */

#include <vpptype.h>                                /* VISA Transition Library */

/* macros for handling the visa version across VTL 3.0 to VISA 1.0 */
#ifndef VI_VERSION_MAJOR
#define VI_VERSION_MAJOR(ver)       ((((ViVersion)ver) & 0xFFF00000UL) >> 20)
#define VI_VERSION_MINOR(ver)       ((((ViVersion)ver) & 0x000FFF00UL) >>  8)
#define VI_VERSION_SUBMINOR(ver)    ((((ViVersion)ver) & 0x000000FFUL)      )
#endif    

/*VXI p&p required function defines*/ 
#define INIT_DO_QUERY 1
#define INIT_SKIP_QUERY 0
#define INIT_DO_RESET 1
#define INIT_DONT_RESET 0   


/*========= DEFINES =======================================================*/

/****************************************************************************
 * General defines
 */


/* #########################################################################
 *                      USER ACCESSIBLE FUNCTION DECLARATIONS
 * #########################################################################
 */  

#if defined (__cplusplus) || defined (__cplusplus__)
extern "C" {
#endif
  
/* #########################################################################
 *                         INIT FUNCTION
 * #########################################################################
 */ 

ViStatus _VI_FUNC b8701_init (ViRsrc resourceName, ViPSession instrumentHandle);

/*===========================================================================*/
/* Function: Write To Instrument                                             */
/* Purpose:  This function writes a command string to the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC b8701_writeInstrData (ViSession instrSession, ViString writeBuffer);

/*===========================================================================*/
/* Function: Read Instrument Buffer                                          */
/* Purpose:  This function reads the output buffer of the instrument.        */
/*===========================================================================*/
ViStatus _VI_FUNC b8701_readInstrData (ViSession instrSession, ViInt16 numBytes, ViPBuf rdBuf, ViPUInt32 bytesRead);

ViStatus _VI_FUNC b8701_close (ViSession instrumentHandle);
