/***************************************************************************** 
 * RFCNT.H - Include file for:
 *
 * RFCNT.C - Source code for:
 *
 * RF Counter Function instruments driver
 * (covers EIP1315A, HP1420B)
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
 */


#define INSTR_LANGUAGE_SPECIFIC                     /* for language-specific code */

#include "visa.h"
#include "vpptype.h"                                /* VISA Transition Library */

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

#if defined (__cplusplus) || defined (__cplusplus__)
extern "C" {
#endif

ViStatus _VI_FUNC eip1315a_init (ViRsrc resourceName, ViPSession instrumentHandle);
ViStatus _VI_FUNC eip1315a_setLO(ViSession theSession, ViReal64 frequency, ViBoolean mode, 
	ViInt32 attn, ViBoolean IFDET);
ViStatus _VI_FUNC eip1315a_close (ViSession instrumentHandle);

ViStatus _VI_FUNC gt55210a_init(ViRsrc resourceName, ViPSession instrumentHandle);
ViStatus _VI_FUNC gt55210a_close(ViSession instrumentHandle);
ViStatus _VI_FUNC gt55210a_writeInstrData(ViSession instrSession,
                                          ViString writeBuffer);
ViStatus _VI_FUNC gt55210a_readInstrData(ViSession instrSession, ViInt16 numBytes, 
                                         ViChar *rdBuf, ViPInt32 bytesRead);
ViInt32 _VI_FUNC gt55210a_setLO(ViSession theSession, ViReal64 MHz,
                                ViBoolean mode, ViInt32 attn, ViInt32 IFDET);

ViStatus _VI_FUNC rfcnt_close (ViSession instrumentHandle);
ViStatus _VI_FUNC rfcnt_init (ViRsrc resourceName, ViPSession instrumentHandle);
ViStatus _VI_FUNC rfcnt_reset (ViSession instrSession);
ViStatus _VI_FUNC rfcnt_writeInstrData (ViSession instrSession, ViString writeBuffer);
ViStatus _VI_FUNC rfcnt_readInstrData (ViSession instrSession, ViInt16 numBytes, 
	                                   ViChar rdBuf[], ViPInt32 bytesRead);
ViStatus _VI_FUNC setbusy (ViBoolean flag_SAIS);
ViStatus _VI_FUNC readbusy (void);
ViStatus _VI_FUNC rf_measfreq (ViSession ihDC, ViSession ihCNT, ViReal64 expectedfreq, 
	                           ViInt32 attn, ViPReal64 measfreq);
ViStatus _VI_FUNC rf_peaktune (ViSession ihDC, ViSession ihCNT, ViPReal64 measfreq, 
	                           ViInt32 attn);
ViStatus _VI_FUNC atlas_rf_peaktune (ViSession ihDC, ViSession ihCNT, ViPReal64 measfreq, 
									 ViInt32 attn, ViReal64 freqmin, ViReal64 freqmax);
ViStatus _VI_FUNC rf_measpw (ViSession ihDC, ViSession ihCNT, ViReal64 carrierfreq, 
							 ViReal64 expectedpw, ViInt32 attn, ViPReal64 measpw);
ViStatus _VI_FUNC rf_measpd (ViSession ihDC, ViSession ihCNT, ViReal64 carrierfreq, 
							 ViReal64 expectedpd, ViInt32 attn, ViPReal64 measpd);

