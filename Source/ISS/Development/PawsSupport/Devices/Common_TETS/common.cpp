// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2020-07-06 16:01:5#$: Date of last commit
//    $Rev:: 27851              $: Revision of last commit
/***********************************************************************
*  FILENAME   :  Common.c
* 
*  DESCRIPTION:  Common file for PAWS drivers used in the ManTech TETS
************************************************************************
*                       SOURCE REVISION HISTORY
*
*  VER  DATE     DESCRIPTION                          AUTHOR
*  ---  ------   --------------------------------    ---------------------
*  1.0  08JUN99  Initial Release					 TYX Corp.
*  1.1  27JUL99  Changed display_error_and_unload	 F. Saracevic, TYX Corp.
*				 from unload to halt state for an error
*  1.2  09AUG99  Modified arb_genadd.c,              M. Rashleigh, TYX Corp.
*                func_genadd.c, and rf_stimadd.c to 
*                ensure that output is not enabled if
*                an error occurs.  Also, if CHANGE 
*                statement is used and an error occurs,
*                the setup will return immediately.  
*  1.3  03MAR00  Modified DC Voltage Measurement and added DC-Offset
*		     to expand the vertical axis in Scopeadd.c.
*		     Modified FUNC_GENadd.c to disable output before
*		     resetting instrument when a fgen_reset is called.
*		     Modified counteradd.c to insert a trigger level that has
*		     been set by an EVENT in the CON command when making a 
*		     pulse width measurement.
*		     These changes were implemented in accordance with ECP 4 and 
*		     have been compiled into WCEM.DLL version 4.2.0
*		     				  			  Mike Eckart, MTSI
*  1.4  11MAY00  Added a WriteHPe1428AB statement to DC Voltage Measurement 
*		     in Scopeadd.c to set channel 2 as measurement source 
*		     if it is selected.
*		     Removed Sleep(5000) statemnts used for debugging during FAT
*		     from FUNC_GENadd.c, Scopeadd.c, and measureadd.c.
*		     These changes were implemented in accordance with ECP 4 and 
*		     have been compiled into WCEM.DLL version 4.3.0
*		     				  			  Mike Eckart, MTSI
*
*  1.5  01JUN00	Jeff Hill, MTSI
*  				Added auto-detection and support for the Giga-tronics 
*				55210A Downconverter. A system may have either this 
*				downconverter or an EIP 1315A downconverter installed.
*  1.5  07JUN00	Jeff Hill, MTSI
*  				Added one READ for the 8701 Init because when it is first
*				powered up, it is trying to output a status code. If it
*				has already been used since power-up, it will error out.
*				Therefore, no status is checked.
*  1.5  15JUN00 Mike Eckart, MTSI
*				Added the function get_res_plc() to use the 
*				BLKs, MODs, and PTHs of a switch path (determined in Modsw.c)
*				to find the resistances of each switch in PLC_LFS.dat and 
*				add them together.  This value is subtracted from resistance
*				measurements in dmmadd.c.
* 1.5  26JUN00	Mike Eckart, MTSI
*				Added a "Display ("")" to arb_genadd.c to correct STR #211
*				(DR ID 416).
*				Added support for Giga-tronics 55210A Downconverter to measureadd.c.
*				Added functions to modsw.c, DMM.c, and dmmadd.c for
*				use when calculating resistive path loss in the LF switches.
*- End of WCEM V4.4 Release 6.0 Update 1 Modifications -------------------
* 1.6  05OCT00  Quoc Nguyen, MTSI
*               Modified "void get_plc(double freq)" function to calculate Medium
*               frequency and High frequency switch pathloss for RFSTIM and RF POWER METER
*- End of WCEM V4.5 Release 7.0 Modifications ----------------------------
* 
*- Begin  WCEM V4.6 Release 7.0 Patch 4 Modifications --------------------
* 1.7  05/08/01 Jeff Hill, MTSI, DR 176 (STR 607)
*               The 1.6 mod introduced a bug where the RF Stim output
*				doesn't come on when there are no switches in the CNX path.
*				Initialized the path_id variable in function resetNonDwg.
*
*	   06/13/01	Jeff Hill, MTSI, DR 195
*	           		Also discovered that RF-Stim corrections were not applied
*				for frequencies above 2,147,000,000 Hz. Found that GT50000.c
*				was added to CEM project rather than linking to GT50000.dll.
*				Somebody changed line "OffsetIndex = Frequency / 100000000;"
*				to "OffsetIndex = (int) Frequency / 100000000;". This limited
*				the range of (double) Frequency to a 32-bit signed integer.
*					Removed GT50000.c from project and link to GT50000.dll. 
*				Now updates to GT50000.dll will be realized in the WCEM.
*					Fixed bug in get_plc() where if a frequency above 500MHz
*				was specified through an S800 switch, then unknown d[] array
*				data would be returned as a path loss value.
*
*	   07/05/01	Jeff Hill, MTSI, Self-identified
*				Replaced TerM9.H and Term9.Lib with latest versions from
*				Teradyne M910 Software Release 3.3.
*- End of WCEM V4.6 Release 7.0 Patch 4 Modifications --------------------
*
*- Begin WCEM V5.0 Release 8.0 Modifications -----------------------------
* 1.6e 07SEP00  C Grard, TYX: Added the code for Electro-optics.
*
* 2.0  07/18/01	Jeff Hill, MTSI
*               Reconciled CommonEO.C from LMIS with this file (Common.C
*               for combined WCEM from LMIS).
*               Reconciled combined WCEM-Common.C from LMIS with ManTech
*               Common.C V1.7 (WCEM 4.6).
*	   10/22/01	Jeff Hill, MTSI, Self-identified
*               Renamed source files to be more logical, as follows:
*               From:           To:
*               counteradd.c    Counter_TimerAdd.c
*               scopeadd.c      Dig_ScopeAdd.c
*               ElecOpt_Add.c   Elc_OptAdd.c
*               measureadd.c    RF_MeasanAdd.c
*               rf_stimadd.c    RF_GenAdd.c
*               SWITCH_1.c      SWITCH.c
*               modsw.c         SwitchAdd.c
*
*- End of WCEM V5.0 Release 8.0 Modifications ----------------------------
*
*- Begin WCEM V5.1 Release 8.1 Modifications -----------------------------
*  ECO-3047-583
*  DmmAdd.C	03/22/02 Jeff Hill, MTSI, DR 269 (STR 966), DR 32 (STR 231)
*			Modified WriteHPe1412a() to exclude the call to 
*			CheckHPe1412aErrors() when the INIT command is being sent. 
*			This was causing the DMM session to hang up when external
*			triggering is used.
*			Added viClear() and "*CLS" commands to reset_dmm() so that
*			when an external trigger is absent, the driver can recover.
*
*- End of WCEM V5.1 Release 8.1 Modifications ----------------------------
*
*- Begin WCEM V5.2 Release 9.0 Modifications -----------------------------
*  ECO-3047-634
*  DmmAdd.C	08/16/02 Jeff Hill, MTSI, DR 287 (STR 1157)
*		PROBLEM:
*		Problem is low persistance of the displayed value during a MONITOR
*		statement. This is a TYX RTS problem that showed up after the 
*		WCEM 5.1 change was made. The CheckHPe1412aErrors() that was being
*		made caused the RTS to pause in the ATLAS INIT part of the
*		code while the DMM finished its measurement. Removing that statement
*		allowed the WCEM/RTS to continue, then pause in the FETCH code while
*		the DMM finished its measurement. The RTS apparently clears its 
*		display between INIT and FETCH in its MONITOR loop.
*		FIX:
*		Modified WriteHPe1412a() to add code when INIT is sent.
*		This code causes this function to wait until the DMM completes its
*		measurement or the VISA timeout at read (10 seconds). This is 
*		necessary so the persistance of the RTS-displayed value during a 
*		MONITOR statement is readable. Otherwise, the time spent waiting 
*		for the DMM to complete measurement is during the FETCH part, 
*		when the RTS has already blanked-out the measurement display.
*  Dig_ScopeAdd.C	08/20/02 Jeff Hill, MTSI, DR 287 (STR 1157)
*		PROBLEM: Same as above for DMM.
*		FIX:
*		Basically same as above. Use *OPC? to wait for DIGITIZE to 
*		finish within the INIT code.
*		8/21/02
*		This fix caused another problem. Now that the signal acquire
*		time is spent in INIT rather than FETCH, the MAX-TIME datum
*		must be fetched and applied to VISA timeout in INIT, not just
*		in FETCH to prevent timeout errors in INIT.
*  DmmAdd.C	08/20/02 
*       Did the same thing for the DMM INIT function.
*  Dig_ScopeAdd.C	08/20/02 Jeff Hill, MTSI, Self-Identified.
*		The fetch_scope function has code to do an AUTOSCALE if an over-
*		scale measurement is returned. I don't think it should, but it's
*		too late now. However, when this happens, timeout and query-
*		interrupted errors occur because AUT takes more than 2 seconds.
*		So I set the TMO to 8 seconds and added a *OPC? to wait for the
*		autoscale to finish (up to 8 seconds).
*  DmmAdd.C and
*  Dig_ScopeAdd.C	08/27/02 Jeff Hill, MTSI, DR 287 (STR 1157).
*		Made all of the above DR 287 changes conditional to NOT execute
*		when external triggering is used in case it is being used with
*		single-action verbs in-between which ATLAS code must execute to
*		generate the external trigger.
*  Common.C	07/22/03 Jeff Hill, MTSI
*		Now identifies EO busconfi file.
*  DigScope_Add.c 07/30/03 Nick Sazonov, MTSI, DR 188
*		Modified setup_wave1() and setup_wave2() to disable the AUT mode,
*		to divide sweeptime by 16, and to pass the TIM:RANG and TIM:REF LEFT
*		commands to the instrument.
*		Modified init_scope() and fetch_scope() to return the VISA time-out
*		to default setting of 10 sec at the end of each of the two procedures.
*  DigScope_Add.c	09/12/03 Jeff Hill, MTSI, Self-Identified
*		Added a check for VISA timeouts with conditional setting of the RTS
*		MAX-TIME flag to init_scope() and read_waveform() procedures. This
*		is so the ATLAS programmer can test for MAX-TIME when a triggered
*		measurement fails due to a missing trigger signal.
*- End of WCEM V5.2 Release 9.0 Modifications ----------------------------
*
*- Begin WCEM V6.0 Release 10.0 Modifications ----------------------------
*  ECO-3047-675
*  Elc_OptAdd.C	11/04/04 Jeff Hill, MTSI
*		Total rewrite to support EO constructs.
*- End of WCEM V6.0 Release 10.0 Modifications ---------------------------
*
*- Begin WCEM V6.1 Release 10.1 Modifications ----------------------------
*  ECO-3047-701
*  Common.C	3/8/05 Jeff Hill, Dave Joiner, MTSI, DR 323 (STR 3160)
*	PROBLEM:
*		a. When the Operator pauses within the Monitor more than 3.5 minutes
*		before proceeding, the debug mode is engaged. Seems to be a bug in 
*		the Windows OS, which only allows a file to be accessed a certain 
*		number of times. When the file access fails, the RTS goes into 
*		Debug mode.
*
*		b. In addition, most of the calls to this procedure (IsSimOrDeb) do
*		not correctly evaluate the return values. Actually it was made
*		convoluted due to the original design of the function, which
*		uses negative numbers as error return codes. Since -1 has all 
*		bits set and most function calls use a bit-masked test for 
*		individual values, these would evaluate incorrectly if there
*		were an error that returned -1. 
*
*   FIX:
*		a. To prevent the Busconfi file from being repeatedly accessed, 
*		especially during a MONITOR statement, the Static array 
*		iMode[] was added to track the current Debug/Simulation mode 
*		state per instrument. If "INIT_VARS" is sent as an argument, all 
*		instrument mode states are reset to -1. If the current mode value 
*		for the instrument has been set (is not -1), it is returned as is 
*		to prevent multiple file accesses. If the value is -1, the 
*		Busconfi file is opened and the current Debug or Sim value is 
*		determined and assigned as the current value.  
*		All values are initialized in functions SetupRTS() and ResetRTS().
*		
*       b. IsSimOrDeb() is modified so when one of the three errors is
*		detected, it will display the appropriate error and return 0. With
*		this method, this problem can be corrected by modifying only this
*		function and none of the 2,500 calls to it.
*
*		DmmAdd.c 
*       Modify function "fetch_dmm_res()" to only read resistance value from 
*       file Plc_LFS.dat, if the current value equals "0", thus preventing 
*       the file from being continually accessed in MONITOR statements.
*		Modified "reset_dmm()" to reset resistance value to 0 so a new
*		resistance value can be obtained for each measurement that may
*		be through a new switch path.
*
*  Common.C	3/9/05 Jeff Hill, MTSI, Self-Identified
*		Changed variant-detection logic to option-detection logic.
*		Before: If Busconfi had RF and EO elements, WCEM would only support
*			one of them.
*		Now: If Busconfi has RF and/or EO elements, WCEM will support both.
*
*  Dig_ScopeAdd.C	3/17/05 Jeff Hill, MTSI, Self-Identified
*		In the TETS ATLAS benchmark, Scope test 15 (Save/Load/Compare) failed
*		with this WCEM version. It would report -2.03V in all array elements
*		when is should vary +/- 3V. Probably due to a speed-up caused by the
*		modification to IsSimOrDeb(). If debug was turned on or NI-Spy was
*		used or you ran the benchmark twice after Scope power-on, the 
*		problem went away.
*		The fix was to add a 2 msec delay between sending a command and 
*		sending the "SYST:ERR?" command in function WriteHPe1428AB().
*
*  Counter_TimerAdd.C	3/22/05 Jeff Hill, MTSI, Self-Identified
*		In the TETS ATLAS benchmark, C/T test 1 & 2 (Freq & Period) failed 
*		generating acquisition timeout and "Trigger Not Found" errors. As in 
*		the problem above, turning on any kind of debug stopped the errors.
*		Fix: Increased the 500 ms delay in WriteHPe1420b() to 800 ms.
*
*  Counter_TimerAdd.C	3/22/05 Jeff Hill, MTSI, Self-Identified
*		Failed ATLAS benchmark C/T test 11 (Neg Pulse Width). Measured the
*		same as test 12 (Pos Pulse Width). Modified function 
*		setup_counter_NegPulsewidth() to correct the C/T SCPI commands "PWID"
*		to "NWID".
*
*  DcpAdd.C	3/30/05 Jeff Hill, MTSI, EO TPS Support
*		The single 'revpol' flag does not properly track the polarity of
*		supplies when single action verbs (CLOSE) are used. Modified the 
*		Setup, Close and set_voltage functions to use the amplitude 
*		polarity, which is tracked by supply, to determine the polarity
*		of a given supply.
*		Added a 200 msec delay in function get_firmrev to prevent 
*		intermittant mis-reads.
*
*  Elc_OptAdd.c 5/17/05 Jeff Hill, MTSI, EO Production Support
*		Modified all calls to function WaitForReady() to accomodate maximum
*		Wait-For-Ready time limits per module as shown:
*			30 minutes for ALTA
*			20 minutes for IR and Visible modules
*			5 minutes for the Modulated source
*		These can be changed for some FNCs with the MAX-TIME modifier.
*
*		This change is because SBIR cannot identify a time limit for any of the
*		EO module functions. They state that if Ready cannot be achieved, the module
*		will evenutally set the ERROR bit.
*		However, SBIR cannot tell us how long it will wait before changing Busy to Error
*		because it varies for some functions with conditions.
*		WaitForReady() has been changed to trap ERROR, exit the Wait-for loop and report
*		the error.
*
*- End of WCEM V6.1 ------------------------------------------------------
*
*- Begin WCEM V6.2 Release 11.0 Modifications ----------------------------
*  ECO-3047-718
*  Common.C	2/9/06 Jeff Hill, MTSI
*		Version and comments only.
*
*  Arb_GenAdd.C	1/25/06 Jeff Hill, MTSI, Self-Identified
*		Modified function setup_arb() to correct the logic that dynamically
*		checks the DC-OFFSET setting to be within range. This range varies
*		based on output VOLTAGE-PP and TEST-EQUIP-IMP settings. Before, 
*		some out-of-range values would not get trapped and some in-range
*		values would get trapped.
*
*  Elc_OptAdd.c 2/9/06 Jeff Hill, MTSI, per ECO-3047-715
*		These changes made to accomodate the LexDB cleanup per above ECO.
*		Changed modifier/token TRIGGER-MODE/TGMD to TRIG-SOURCE/TRGS.
*		Changed dim/token LASR/LASR to NORM/NORM.
*		Changed dim/token AUTO/AUTR to INT/INT.
*
*- End of WCEM V6.2 ------------------------------------------------------
*
*- Begin WCEM V6.3 Release ?.? Modifications ----------------------------
*  ECO-3047-???
*  Common.C	9/18/06 Jeff Hill, MTSI
*		Modified function resetNonDwg() to add a "SENS:ROSC:SOUR INT" so 
*		that the precision internal TCXO timebase is used. This makes
*		the C/T accuracy match what is stated in the TETS SDD.
*
*  Counter_TimerAdd.C	9/18/06 Jeff Hill, MTSI, Self-Identified for AAV
*		Modified function reset_cntr() to add a "SENS:ROSC:SOUR INT" for
*		same reason as above.
*
*  Dig_ScopeAdd.C   5/1/07 Jeff Hill, MTSI, Self-Identified for AAV
*       Problem:
*       When a high SAMPLE-COUNT setting (512) is used with the 
*       AV-VOLTAGE measured characteristic, the oscilloscope would 
*       intermittently begin to generate errors then lock up the RTS.
*       Fixes:
*		1. Modified function fetch_volt_avg() to add a call to init_scope()
*       inside the for-loop associated with the SAMPLE-COUNT modifier that
*       is used with the AV-VOLTAGE measurement. This resolved the problem.
*       2. Added an escape capability so that if a time-out DOES occur,
*       it will not lock up the TETS by attempting to complete the
*       SAMPLE-COUNT iterations when mal-functioning.
*       3. Changed the return value to be averaged by the number of
*       successful iterations, not the full SAMPLE-COUNT. However, this
*       return value may be set to 0 by the ErrMsg(5,"...") PAWS function
*       that is called when the timeout occurs in the fetch_scope()
*       function.
*       4. Changed the fetch_scope() function to delete the two 
*       "scope_reset_flag = 0;" statements that were placed immediately
*       after the reset_scope() calls that are made when a timeout occurs.
*       These statements were not necessary. The new code in function
*       fetch_volt_avg() now uses the value of variable scope_reset_flag 
*       to determine if a timeout has occured during fetch_scope().
*
*- End of WCEM V6.3 ------------------------------------------------------
*
*- Begin WCEM V6.4 Release ?.? Modifications ----------------------------
*  ECO-3047-???
*  Common.C	8/15/07 Jeff Hill, MTSI
*		Modified functions get_plc() and get_res_plc() to close the 
*		PLC file at the end of each function. Before, this caused 
*		software malfunctions after multiple executions, especially during
*		MONITOR statements.
*		Also modified get_plc() so it will NOT open the PLC file if no
*		switches are used in the current action.
*
*- End of WCEM V6.4 ------------------------------------------------------
*
*- Begin WCEM V6.5 Modifications ----------------------------
*  Common.C	3/28/2008 A. Roybal, USMC TMDE
*		Modified to allow WRTS to disaply v6.5 upon test program load.
*		WCEM 6.5 was created to include the release of the 
*		DeviceDB.DDB v5.3. 
*
*- End of WCEM V6.5 ------------------------------------------------------
*************************************************************************/

#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include <sys/types.h>
#include <sys/stat.h>
#include <ole2.h>
#include <winuser.h>
#include <time.h>
#include <mmsystem.h>
#include <string.h>

#include <stdio.h>
#include <visa.h>
#include <stdlib.h>

#include "key.h"
#pragma warning(disable : 4115)
#include "cem.h"
#pragma warning(default : 4115)
#include "visatype.h"
#include "tets.h"

// Added Giga-tronics gt50000.h New RF Stim 2/18/99 GJohnson
//#include "gt50000.h"
//#include "hpe1412a.h"
//#include "hpe1428a.h"
////#include "ri3151.h"
//#include "ri1260.h"
//#include "hpe1445a.h"
//#include "eip1140a.h"
//#include "hpe1416a.h"
//#include "rfcnt.h"
//#include "B8701.h"
//#include "aps6062.h"
//#include "hpe1420b.h"
//#include "terM9.h"
//#include "DwgData.h"
/**********************************************************************/
#include ".\\common.h"
#include "events.h"

extern void ErrorHandler(int type, char * funcName, char * msg);

extern double GetPLC(double FreqValue, char switchesUsed[SWX_USED][SWX_LENGTH]);
/* Version Information */
#define WCEMVERSION	"6.5\n"

/* Bus Configuration: Core, RF or EO? */
#define	CORE	0
#define RF		1
#define EO		2

// Added Giga-tronics gt50000.h New RF Stim 2/18/99 GJohnson
#define EIP1143A				   0 /* EIP Installed */	
#define eip1143a_MANF_ID      0x0FA8 /*  EIP Instrument manufacturer ID */
#define eip1143a_MODEL_CODE   0x0474 /*  EIP Instrument model code      */
#define GT5000A					   1 /* Giga-Tronics Installed */	
#define gt50000a_MANF_ID      0x0F52 /*  Giga-Tronics Instrument manufacturer ID */
#define gt50000a_MODEL_CODE   0xC350 /*  Giga-Tronics Instrument model code      */

#define FLAG 10

#ifdef _WIN32
  #define CCONV _stdcall
  #define NOMANGLE
#else
  #define CCONV FAR PASCAL _export
  #define NOMANGLE EXTERN_C
  #include <stdlib.h>
  #include <compobj.h>
  #include <dispatch.h>
  #include <variant.h>
  #include <olenls.h>
#endif

int			DE_BUG = 0;

FILE		*debugfp;
CEM_INFO	cemInfo;
PROBE_INFO	probeInfo;

extern void cls_mux_interconnects();
// CG This is the list of Functions that I need to use for DWG
extern void Events_Clear(void);                    // In DwgData.h/c
//extern void VL_ResetLevelIndexUsage(int nVL);      // In DwgData.h/c
//extern void DWGDspVLUsage(void);                   // In DwgOrg.c

//prototype for functions
void ErrorHandler(int type, char * funcName, char * msg);
extern void SendArbResetCommand();

__declspec( dllexport ) ViSession dwg_handle;                      // DWG handle

/*****************************************************/
char		mla[3], mta[3];
DATUM		*maxtime_datum;
ViUInt32	retCnt;
int			reset_once_only;
int			busconfi_type = CORE;
static char ebuf[256];
static int	allocAlreadyDone = NOTDONE, allocFreed = NOTDONE;

// Need to include "stdio.h" (and "cem.h")
//
// Return value from this function:
//  0: Can't open Busconfi file
//  0: Can't close the Busconfi file
//  0: Can't find the device name
//  0: Device Not in Simulation and Not in Debugmode
//  1: Device     in Simulation and Not in Debugmode
// 10: Device Not in Simulation and     in Debugmode
// 11: Device     in Simulation and     in Debugmode
// NOTE, in the busconfi file do the following:
//  For Simulation mode, write "; Simsoft" (case sensitive) on same line as device (at the end)
//  For Debugmode, write "; Debug" (case sensitive) on same line as device (at the end)
//  For Debugmode and Simulation mode, write "; Simsoft Debug" or "; Debug Simsoft" (at the end)
// Example of usage: int ret = IsSimOrDeb("DMM");
// dev_name is the name of the device that we wish to check on
// Note: Path to the busconfi file is hard coded in this function and needs to be adapted to 
// the user's project directory path.
// ===========================================================================

int IsSimOrDeb(char dev_name[20])
{
	char line[128];			// line in the busconfi that is analyzed
	BOOL found;				// found line with required device in it
	int ch,i,j,k;			// Buffer characters and index
	FILE *busconf;			// Stream that we are going to look through
	static int iMode[14];	// Mode Value Array
	int iInstrument;		// Instrument Index
	errno_t rtnflag = 0;

	// If the argument is "INIT_VARS" then Reset the 
	// Mode values to -1
	if (strstr(dev_name, "INIT_VARS"))
	{
		for (iInstrument=0; iInstrument<14; iInstrument++)
			iMode[iInstrument] = -1;     // Initialize Mode Values
		return (0);
	}

	// Find the instrument index in the "dev_name" argument
	// and set the iIstrument value.
	if      (strstr(dev_name, "SYS_INIT"))		iInstrument = 0;
	else if (strstr(dev_name, "ARB_GEN"))		iInstrument = 1;
	else if (strstr(dev_name, "COUNTER_TIMER"))	iInstrument = 2;
	else if (strstr(dev_name, "DCP"))			iInstrument = 3;
	else if (strstr(dev_name, "DDC1553"))		iInstrument = 4;
	else if (strstr(dev_name, "DSO_1"))			iInstrument = 5;
	else if (strstr(dev_name, "DMM"))			iInstrument = 6;
	else if (strstr(dev_name, "DWG"))			iInstrument = 7;
	else if (strstr(dev_name, "FUNC_GEN"))		iInstrument = 8;
	else if (strstr(dev_name, "SWITCH_1"))		iInstrument = 9;
	else if (strstr(dev_name, "RF_GEN"))		iInstrument = 10;
	else if (strstr(dev_name, "RF_MEASAN"))		iInstrument = 11;
	else if (strstr(dev_name, "RF_PWR"))		iInstrument = 12;
	else if (strstr(dev_name, "ELC_OPT"))		iInstrument = 13;
	else
	{
		sprintf_s(msg_buf, sizeof(msg_buf), "Unsupported Device Name: %s.", dev_name);
		ErrorHandler(2,"IsSimOrDeb", msg_buf);
		return (0);
	}

	// If the Mode Value has already been set for the current Instrument, return it.
	if (iMode[iInstrument] != -1)  
		return (iMode[iInstrument]);


	// Open Busconfi for read
	//!! The following has to be adapted to your environment (location of busconfi file)
	rtnflag = fopen_s( &busconf, "c:\\usr\\tyx\\sub\\ieee716.89\\GPATSCIC\\station\\busconfi","rt");
	if (rtnflag != 0)	// If busconfi file not found
	{
		ErrorHandler(2,"IsSimOrDeb", "Cannot open file Busconfi");
		fclose(busconf);
		return (0);
	}

	// Set cursor at beginning of file
	fseek(busconf, 0L, SEEK_SET);    
	// Locate the device name in the line
	found = FALSE;	// Initialize boolean

	// Loop until found EOF or the proper device name
	while(!found && !feof(busconf))
	{
		// Read in first line into "line[]":
		ch = fgetc( busconf );
		for( i=0; (i < 127 ) && ( feof( busconf ) == 0 ) && (ch != '\n') ; i++ )
		{
			line[i] = (char)ch;
			ch = fgetc( busconf );
		}
		// Clean the rest of the line buffer
		for ( j=i+1; j < 127 ; j++)
			line[j]='\0';

		// Now that we have the line, we need to find the one with the device name on it
		// We loop until we haven't found the device and we haven't reached the end of 
		// the 'line' string and we haven't found a ';' before the device name
		for (i = 0 ; (!found) && ((unsigned)i < strlen(line)) && line[i] != ';' ; i++)
		{
			for (j = 0, k = i; ((line[k]    == dev_name[j]) ||
								((line[k]+32 == dev_name[j]) && (line[k]>64 && line[k]<91)) ||
								((line[k] == dev_name[j]+32) && (line[k]>96 && line[k]<123)) ||
								dev_name[j] == '\0' ) && (!found)   ; j ++, k++)
			{
				// If device found
				if (dev_name[j] == '\0')
				{
					// Make sure that there is white space after the device name
					// and that it's not preceeded by something else too
					if ( (line[k] == ' ' || line[k] == '\t') )
						if( ( i > 0 &&  (line[i-1] == '\t' || line[i-1] == ' ') ) || ( i == 0 ) )
							found = TRUE;
				}
			}  //End for (j = 0, k = i;...
		}  // End for (i = 0 ; (!found) && ((unsigned)i < strlen(line)) && line[i] != ';' ; i++)

		if ( found == TRUE )	// Found the device
		{
			iMode[iInstrument] = 0;										// Initialize value to 'no flags'
			if (strstr(line, "Simsoft"))	// Check for/add Simsoft value
			{
				iMode[iInstrument] += 1;
				sprintf_s(msg_buf, sizeof(msg_buf), "%s is in Simsoft mode.", dev_name);
				ErrorHandler(3,"IsSimOrDeb", msg_buf);
			}
			if (strstr(line, "DBG"))
			{
				iMode[iInstrument] += 10;	// Check for/add Debug value
			}
			fclose(busconf);
			//if ((iMode[0] & 10) == 10)	//If SYS_INIT debug flag on
			//{
			//	sprintf_s(msg_buf, sizeof(msg_buf), " -SYS_INIT Debug- Device %s set to Mode value: %d\n"
			//		,dev_name, iMode[iInstrument]);
			//	Display(msg_buf);
			//}
			return (iMode[iInstrument]);	//Successful exit
		}
	}

    // If here, device name not found
	fclose(busconf);

	if (((busconfi_type & EO) && (iInstrument == 13)) ||
		((busconfi_type & RF) && ((iInstrument >= 10) && (iInstrument <= 12))))
	{
		sprintf_s(msg_buf, sizeof(msg_buf), "Cannot find Device Name %s in Busconfi file.", dev_name);
		ErrorHandler(2,"IsSimOrDeb", msg_buf);
	}
	return (0);

}

// This function sends data to the DATUM block.
// Needed header files included: "key.h" and "cem.h" and "stdio.h"
// How to use the function for single values example: ret = SendData(&volt, &retcount, 1);
// How to use the function for an array example: ret = SendData(volt_array, &retcount, 0);
// Return of that function is 1 for succesfull fetch of Atlas value and 0 for unsuccesfull retrieval
// arg_val: argument value that has been retrieved from the Atlas.
// cnt_val: the number of elements that one needs to send the values back to the atlas from the
//			function associated to the wrapper function
// debugmode: If the debugmode is passed on (1 for yes and 0 for no -default-) then display
//			values that have been retrieved from the Atlas
// example of usage in the instrument file (Fetch function):
//
// int doHp3325a1FetchVoltage (double* pVOLT, int nCntVOLT)
// {
// double volt = 2.5;
// int ret = -1;
// int retcount = -1;
// ret = SendData(&volt, 1, &retcount);
// return (int) retcount;     // !!!! Note that this line is changed by the user !!!!
// }

int SendData(void *arg_val, int *cnt_val, int debugmode)
{
	DATUM *p_val;   // pointer in the DATUM block pointing to the value that we want to send
	int i;          // index i
	int t_val;      // This is the type number of the data being retrieved
	char txtbuf[128];// This is the buffer that includes the text to be displayed in debugmode
	int inttmp;     // buffer int value
	double dectmp;  // buffer dec value

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering SendData()\n");

	// Get the pointer to the data that we want to send to the Datum block
	p_val = FthDat();
	// If we have a pointer, that means that the value exists in the Atlas
	// If not, we return the default value passed on
	if (p_val != NULL)
	  {
		// Got a pointer so we know that the value is there to be retrieved
		// Get the number of elements in the data
		*cnt_val = DatCnt(p_val);
		// check on the type of the data
		t_val = DatTyp(p_val);
		switch(t_val)
		  {
			case INTV:
				if (*cnt_val == 1)
				  {
					inttmp = *((long *)arg_val);
					INTDatVal(p_val,0) = inttmp;
					if (debugmode == 1)
					{
						sprintf_s(txtbuf, sizeof(txtbuf), " -debug- Value being send to Datum block is:%d\n",inttmp);
						Display(txtbuf);
					}
					// Send the data
					FthCnt(1);
				  }
				else
				  {
					// retrieve the actual elements of the data
					if (debugmode == 1)
					{
						sprintf_s(txtbuf, sizeof(txtbuf), " -debug- Values being send to Datum block are:");
						Display(txtbuf);
					}
					for (i = 0; i < *cnt_val; i++)
					{
						inttmp = *((long *)arg_val);
						INTDatVal(p_val,i) = inttmp;
						if (debugmode == 1)
						{
							sprintf_s(txtbuf, sizeof(txtbuf), "%d ",inttmp);
							Display(txtbuf);
						}
					}
					if (debugmode == 1)
						Display("\n");
					// Send the data
					FthCnt(*cnt_val);
				  }
				break;

			case DECV:
				if (*cnt_val == 1)
				  {
					dectmp = *((double *)arg_val);
					DECDatVal(p_val,0) = dectmp;
					if (debugmode == 1)
					{
						sprintf_s(txtbuf, sizeof(txtbuf), " -debug- Value being send to Datum block is:%d\n",dectmp);
						Display(txtbuf);
					}
					// Send the data
					FthCnt(1);
				  }
				else
				  {
					// retrieve the actual elements of the data
					if (debugmode == 1)
					{
						sprintf_s(txtbuf, sizeof(txtbuf), " -debug- Values being send to Datum block are:");
						Display(txtbuf);
					}
					for (i = 0; i < *cnt_val; i++)
					{
						dectmp = *((double *)arg_val);
						DECDatVal(p_val,i) = dectmp;
						if (debugmode == 1)
						{
							sprintf_s(txtbuf, sizeof(txtbuf), "%f ",dectmp);
							Display(txtbuf);
						}
					}
					if (debugmode == 1)
						Display("\n");
					// Send the data
					FthCnt(*cnt_val);
				  }
			   break;
		   }
		 return 1;
	  }
	else
	  {
		if (debugmode == 1)
		{
			sprintf_s(txtbuf, sizeof(txtbuf), " -debug- No value has been sent to the Datum block\n");
			Display(txtbuf);
		}
		return 0;
	  }
}

// This function retrieves the Integer data from the DATUM block.
// Needed header files included: "key.h" and "cem.h" and "stdio.h"
// How to use the function for single values example: RetrieveData(M_VOLT,K_SET,&volt,1);
// How to use the function for an array example: RetrieveData(M_VOLT,K_SET,volt_array,0);
// Return of that function is 1 for succesfull fetch of Atlas value and 0 for unsuccesfull retrieval
// arg_token: argument token such as the one in the lex files (ex: M_VOLT)
// arg_mod: argument modifier such as min, nominal value or max (resp. K_SRN, K_SET, K_SRX)
// arg_val: argument value that has been retrieved from the Atlas.
// debugmode: If the debugmode is passed on (1 for yes and 0 for no -default-) then display
//			values that have been retrieved from the Atlas

int RetrieveData(short arg_token, int arg_mod, void *arg_val, int debugmode)
{
	DATUM	*p_val = '\0';   // pointer in the DATUM block pointing to the value that we want to retrieve
	int		cnt_val;    // count number of elements in the data being retrieved
	int		i;          // index i
	int		t_val;      // This is the type number of the data being retrieved
	char	txtbuf[128];// This is the buffer that includes the text to be displayed in debugmode
	int		inttmp;     // buffer int value
	double	dectmp;  // buffer dec value

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering RetrieveData()\n");

	// Retrieve the pointer to the data of minimum value
	if (arg_mod == K_SRN)
		p_val = RetrieveDatum(arg_token,K_SRN);
	// Retrieve the pointer to the data of nominal value
	if (arg_mod == K_SET)
		p_val = RetrieveDatum(arg_token,K_SET);
	// Retrieve the pointer to the data of maximum value
	if (arg_mod == K_SRX)
		p_val = RetrieveDatum(arg_token,K_SRX);

	// If we have a pointer, that means that the value exists in the Atlas
	// If not, we return the default value passed on
	if (p_val != NULL)
	{
		// Got a pointer so we know that the value is there to be retrieved
		// Get the number of elements in the data
		cnt_val = DatCnt(p_val);
		// check on the type of the data
		t_val = DatTyp(p_val);
		
		if (debugmode) {
			sprintf_s(msg_buf, sizeof(msg_buf)," -debug- type of modifier:%d\n",t_val);
			Display(msg_buf);
		}
		switch(t_val) {
			case INTV:
				if (cnt_val == 1) {
					inttmp = INTDatVal(p_val,0);
					*((long *)arg_val) = inttmp;
					
					if (debugmode == 1) {
						sprintf_s(txtbuf, sizeof(txtbuf), " -debug- Value associated to %d retrieved:%d \n",arg_token,inttmp);
						Display(txtbuf);
					}
				}
				else {
					// retrieve the actual elements of the data
					if (debugmode == 1) {
						sprintf_s(txtbuf, sizeof(txtbuf), " -debug- Value associated to %d retrieved:",arg_token);
						Display(txtbuf);
					}
					for (i = 0; i < cnt_val; i++) {
						inttmp = INTDatVal(p_val,i);
						*((long *)arg_val) = inttmp;
						
						if (debugmode == 1) {
							sprintf_s(txtbuf, sizeof(txtbuf),"%d ",inttmp);
							Display(txtbuf);
						}
					}
					
					if (debugmode == 1)
						Display("\n");
				}
			break;

			case DECV:
				if (cnt_val == 1) {
					dectmp = DECDatVal(p_val,0);
					*((double *)arg_val) = dectmp;
					
					if (debugmode == 1) {
						sprintf_s(txtbuf, sizeof(txtbuf), " -debug- Value associated to %d retrieved:%f \n",arg_token,dectmp);
						Display(txtbuf);
					}
				}
				else {
					// retrieve the actual elements of the data
					if (debugmode == 1) {
						sprintf_s(txtbuf, sizeof(txtbuf), " -debug- Value associated to %d retrieved:",arg_token);
						Display(txtbuf);
					}
					for (i = 0; i < cnt_val; i++) {
						dectmp = DECDatVal(p_val,i);
						*((double *)arg_val) = dectmp;
						
						if (debugmode == 1) {
							sprintf_s(txtbuf, sizeof(txtbuf),"%f ",dectmp);
							Display(txtbuf);
						}
					}
					
					if (debugmode == 1)
						Display("\n");
				}
			break;
		}
		FreeDatum(p_val);
		return 1;
	}
	else {
		if (debugmode == 1) {
			sprintf_s(txtbuf, sizeof(txtbuf)," -debug- No value has been retrieved for %d \n",arg_token);
			Display(txtbuf);
		}
		// No need to do anything since the variable has been set to the default before calling this function
		FreeDatum(p_val);
		return 0;
	}
}

/******************************************************************************
 * Called after all pnp function to verify that all is all right              *
 ******************************************************************************/
void PNPstatus(char *msg)
{
	ViChar txtbuf[256];

	if (pnpstatus != VI_SUCCESS)
	  {
		sprintf_s(msg_buf, sizeof(msg_buf),"ViStatus = %h",pnpstatus);
		ErrorHandler(1,msg,msg_buf);
		//terM9_error_message (dwg_handle, (ViStatus)pnpstatus, &txtbuf[0]);
		sprintf_s(msg_buf, sizeof(msg_buf),"(error) %s\n",txtbuf);
		Display(msg_buf);
	  }
//	if (pnpstatus == TERM9_ERROR_PATTERNSET_TIMEOUT)   // Timeout
//		ErrMsg(5,"DWG Dynamic Timeout");
}


// User defined error handling function placeholder
// The function dummy is a place holder until error handling is defined 
void ErrorHandler(int type, char * funcName, char * msg)
{

	switch(type)
	{		
		case 1 :
			sprintf_s(ebuf, sizeof(ebuf), "*** ERROR from %s: %s ***\n",
				funcName, msg);
			Display(ebuf);
			break;
		case 2 :
			sprintf_s(ebuf, sizeof(ebuf), "*** WARNING from %s: %s ***\n",
				funcName, msg);
			Display(ebuf);
			break;
		case 3 :
			sprintf_s(ebuf, sizeof(ebuf),"*** INFO from %s: %s ***\n",
				funcName, msg);
			Display(ebuf);
			break;
		default :
			sprintf_s(ebuf, sizeof(ebuf),"*** UNKNOWN ERROR HANDLING TYPE. ***\n");
			Display(ebuf);
			break;
	}
}

/******************************************************************************/
void display_error_and_unload()
{
	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering display_error_and_unload()\n");

	if (err < 0)
	{
		sprintf_s(msg_buf, sizeof(msg_buf),"Error from %s - errno %x\n",instrument,err);
		Display(msg_buf);
//		fprintf(debugfp, "err # = %x\nError from %s - errno %x\n", err, instrument, err);
		fflush(NULL);
		Sleep(50);
		ErrMsg(7, "");	// halts the rts at this point
	}
	else if (err == 0x3FFF0002)
	{

//		fprintf(debugfp, "Received 3FFF0002 error\n");
		fflush(NULL);
		//arb_gen warning no. 3FFF0002h
		//this warning comes from setting default power-on states in the reset
		//and means that specified event is already enabled for at least one 
		//of the specified mechanisms.
		//Therefore, this warning will be ignored and not displayed.
	}
	else if ((err != 0x3FFF0002) && (err > 0))
	{
		sprintf_s(msg_buf, sizeof(msg_buf), "Warning from %s - warning no %x\n",instrument,err);
		Display(msg_buf);
//		fprintf(debugfp, "err # = %x\nError from %s - errno %x\n", err, instrument, err);
		fflush(NULL);
		Sleep(50);
	}
}

/******************************************************************************/
void display_error()
{
	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering display_error()\n");

	if (err < 0)
	{
		sprintf_s(msg_buf, sizeof(msg_buf), "Error from %s - errno %x\n",instrument,err);
		Display(msg_buf);
	}
}

/******************************************************************************/
void set_probe()
{
	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering set_probe()\n");

	probe = 1;
}

/******************************************************************************/
void init_probe()
{
	double temp_val;

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering init_probe()\n");

	maxtime_datum = GetDatum(M_MAXT,K_SET);
	if(maxtime_datum)
	{
		temp_val = DECDatVal(maxtime_datum,0);
		maxtimeout = temp_val;
	}
	else
	{
		maxtimeout = 99;
	}

	sysmon_err = 0;
  
	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
	{
		sprintf_s(msg_buf, sizeof(msg_buf),"wcem debug: <init_probe> maxtimeout=%f\n",maxtimeout);
		Display(msg_buf);
		sprintf_s(msg_buf, sizeof(msg_buf),"wcem debug: 0=%f\n",DECDatVal(maxtime_datum,0));
		Display(msg_buf);
		sprintf_s(msg_buf, sizeof(msg_buf),"wcem debug: 1=%f\n",DECDatVal(maxtime_datum,1));
		Display(msg_buf);
		sprintf_s(msg_buf, sizeof(msg_buf),"wcem debug: 2=%f\n",DECDatVal(maxtime_datum,2));
		Display(msg_buf);
	}
}


/**************************************************************************/
/* NOMANGLE short CCONV WaitForProbeButton (float MaxT) */
/**************************************************************************/
int WaitForProbeButt (double MaxT)
{

	int		returnResults = SUCCESS;
	DWORD	eventResponse = WAIT_TIMEOUT;

	probeInfo.timeOut = (int) (MaxT * 1000);

	eventResponse = WaitForSingleObject(probeInfo.hMakeMeasurementHandle, probeInfo.timeOut);

	switch (eventResponse) {

		case WAIT_TIMEOUT:
	
			returnResults = CEM_ERROR;

			break;

		case WAIT_ABANDONED:

			returnResults = CEM_ERROR;

			break;

		case WAIT_OBJECT_0:

			if (!(SetEvent(probeInfo.hSendReturnKeyHandle))){
				retrieveErrorMessage("WaitForProbeButt()", "OpenEvent recieved an error: ");
				dodebug(0, "WaitForProbeButt()", "Error happen in checkForNotProbeEvent", (char *)NULL);
				returnResults = CEM_ERROR;
			}
			else {
				if (!(ResetEvent(probeInfo.hMakeMeasurementHandle))) {
					retrieveErrorMessage("WaitForProbeButt()", "Recieved an error: ");
					returnResults = CEM_ERROR;
				}
			}

			break;

		default:

			retrieveErrorMessage("WaitForProbeButt()", "WaitForSingleObject error: ");
			dodebug(0, "WaitForProbeButt()", "Something very bad in winders expermental land", (char *)NULL);
			returnResults = CEM_ERROR;
	}

	return(returnResults);
}

/**********************************************************************/
/* Function identifies busconfi file as CORE, RF or EO
/**********************************************************************/
int get_busconfi_type()
{
	FILE *stream;
	char line[100];
	int type = CORE;

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering get_busconfi_type()\n");

	if( (fopen_s( &stream, "c:\\usr\\tyx\\sub\\ieee716.89\\tets\\station\\BusConfi", "r" )) != 0 )
	{       /* ok to go...stream != NULL */
		if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
			Display("wcem debug: Opened Busconfig file\n");

		while ( fgets( line, 100, stream ) != NULL)
		{
			if (strncmp (line, "RF_GEN", 6) == 0)	type += RF;
			if (strncmp (line, "ELC_OPT", 7) == 0)	type += EO;
		}
		if (fclose(stream))
			Display("The file busconfig was not closed.\n");
	}
	else
		Display("The file 'busconfi' was not opened.\n");
	
	return type;
}

//ViSession rf_pwr_handle;
//ViSession rf_pwr_handle;
ViSession modsw_handle;
//ViSession rf_stim_handle;
ViSession no_rf_stim_handle;
ViSession rfcounter_handle;

/******************************************************************************
 * Gets Visa handles for all instruments but DWG                              *
 ******************************************************************************/
void JWinitNonDwg()
{
	//int				supply;
	//unsigned long	BytesRead;
	//char			string[512];
	//char			DCP_ADDR[20];

#ifdef JimW
	//Added the following code to be used by the RF Stim for 
	ViSession rmSession = 0;
	//ViSession instrSession;
	ViUInt16 manfID = 0, modelCode = 0;    

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering initNonDwg()\n");

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering allocInstrumentsReadBuffers()\n");

	if ((allocInstrumentsReadBuffers(ALLOC)) < 0) {
		if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
			Display("wcem debug: Failed allocInstrumentsReadBuffers()\n");
		display_error_and_unload();
	}
	else {
		if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
			Display("wcem debug: Passed allocInstrumentsReadBuffers()\n");
		allocAlreadyDone = BEENDONE;
		allocFreed = NOTDONE;
	}

	/* Read the BusConfig file.  Returns a null string for instruments not in file */
	/* Removed requirement to read BusConfig file to get instrument addresses      */
	/* Addresses specified in tets.h, 11/4/98, CMR                                 */
	/* get_inst_add(); */

	/* Determine if using Core, RF and/or EO bus configuration file */
	busconfi_type = get_busconfi_type();
	Display("Using Bus Configuration file with TETS Core");
	if (busconfi_type & RF) Display(", RF");
	if (busconfi_type & EO) Display(", EO");
	Display(" devices.\n");

	/* Counter */
	sprintf(CNTR_ADDR,"VXI::18::Instr");
	sprintf(instrument,"hpe1420b");
	if ((err = hpe1420b_init((ViRsrc)CNTR_ADDR,VI_FALSE,VI_FALSE,&counter_timer_handle)) != 0)
		display_error();

	if (busconfi_type & RF)
	{
		/* RF Stim */
		sprintf(FS_ADDR,"VXI::20::Instr");
		// Following code determines which RF Source is installed
		err = viOpenDefaultRM (&rmSession);
		err = viOpen (rmSession, FS_ADDR, VI_NULL, VI_NULL, &instrSession);
		err = viSetAttribute (instrSession, VI_ATTR_TMO_VALUE, 10000);
        err = viSetBuf (instrSession, VI_READ_BUF|VI_WRITE_BUF, 4000);
        err = viSetAttribute (instrSession, VI_ATTR_WR_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS);
        err = viSetAttribute (instrSession, VI_ATTR_RD_BUF_OPER_MODE,
                            VI_FLUSH_ON_ACCESS);
        err = viGetAttribute (instrSession, VI_ATTR_MANF_ID, &manfID);
        err = viGetAttribute (instrSession, VI_ATTR_MODEL_CODE, &modelCode);
		viClose (instrSession);
		viClose (rmSession);
		display_error();

		if (manfID != gt50000a_MANF_ID && modelCode != gt50000a_MODEL_CODE)
		{
			RF_Stim = 0;
            sprintf(instrument,"eip1140a");
			if ((err = eip1140a_init (FS_ADDR, VI_FALSE,VI_FALSE,&rf_stim_handle)) != 0)
				display_error();

			if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
				Display("wcem debug: Found EIP1143A RF Stimulus\n");
		}
		else
		{
			RF_Stim = 1;
            sprintf(instrument,"gt50000a");
			if ((err = gt50000_init (FS_ADDR, VI_FALSE,VI_FALSE,&rf_stim_handle)) != 0)
				display_error();

			if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
				Display("wcem debug: Found Giga-Tronics 50000A RF Stimulus\n");
		}

		/* Power Meter */
		sprintf(PMTR_ADDR,"VXI::28::Instr");
		sprintf(instrument,"hpe1416a");
		if ((err = hpe1416a_init(PMTR_ADDR,VI_FALSE,VI_FALSE,&rf_pwr_handle)) != 0)
			display_error();

		/* RF Counter */
		sprintf(RFCNTR_ADDR,"VXI::25::Instr");
		sprintf(instrument,"hpe1420b RF");
		if ((err = rfcnt_init(RFCNTR_ADDR, &rfcounter_handle)) != 0)
			display_error();

		/* RF Measurement Analyzer */
		sprintf(RFMA_ADDR,"VXI::27::Instr");
		sprintf(instrument,"b8701");
		if ((err = b8701_init (RFMA_ADDR, &meas_analyzer_handle)) != 0)
			display_error();
		else	//Perform one READ to clear the output buffer
			err = b8701_readInstrData(meas_analyzer_handle, 100, readString, &BytesRead);

		/* Downconverter Handle */
		sprintf(RFDNCVTR_ADDR,"VXI::23::Instr");
		err = eip1315a_init (RFDNCVTR_ADDR, &lo_downconverter_handle);
		if (err == 0)
			DownconverterType = 1;
		else
		{
			sprintf(RFDNCVTR_ADDR,"VXI::20::Instr");
			sprintf(instrument,"eip1315a/gt55210a");
			err = gt55210a_init (RFDNCVTR_ADDR, &lo_downconverter_handle);
			if (err == 0)
				DownconverterType = 2;
			else
				display_error();
		}
	}
	
	/* Switching */
	sprintf(MODSW_ADDR,"VXI::38::Instr");
	sprintf(instrument,"ri1260");
	if ((err = ri1260_init (MODSW_ADDR, VI_FALSE, VI_FALSE, &modsw_handle)) != 0)
		display_error();
#endif //JimW

}
/******************************************************************************
 * Resets all instruments (but DWG) using existing handles                    *
 ******************************************************************************/
void resetNonDwg()
{
	//ViString Buf;
	//char string[512];
	//char DCP_ADDR[20];
	//int i, supply;

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering resetNonDwg()\n");

	/* This prevents the reset action from being called      *
	 * from the reset macro immediately after the init macro */
	if(reset_once_only == 1)
	{
		return;
	}

	/* Initialize the path_id array to prevent unpredictable results when no switches are used with the RF Stim */
	sprintf_s(path_id[0],7,"\0");

#ifdef JimW
	/* Reset DMM */
	sprintf(instrument,"hpe1412a");
	if ((err = hpe1412a_reset(dmm_handle))!= 0)
		display_error();

	/* Reset Counter */
	sprintf(instrument,"hpe1420b");
	if ((err = hpe1420b_reset(counter_timer_handle)) != 0)
		display_error();

	/*  Set Channels to High Impedance State  */
	sprintf(string, "INPUT1:IMP 1000000");
	Buf = &string[0];
	if ((err = hpe1420b_writeInstrData(counter_timer_handle,Buf)) != 0)
		display_error();

	sprintf(string, "INPUT2:IMP 1000000");
	Buf = &string[0];
	if ((err = hpe1420b_writeInstrData(counter_timer_handle,Buf)) != 0)
		display_error();

	/*  Enable the internal precision TCXO  */
	sprintf(string, "SENS:ROSC:SOUR INT");
	Buf = &string[0];
	if ((err = hpe1420b_writeInstrData(counter_timer_handle,Buf)) != 0)
		display_error();

	/* Reset Scope */
	sprintf(instrument,"hpe1428a");
	if ((err = hpe1428a_reset(oscope_handle)) != 0)
		display_error();

	///* Reset Function Generator */
	//sprintf(instrument,"ri3152");
	//if ((err = ri3151_reset(func_gen_handle)) != 0)
	//	display_error();

	/* Reset ARB */
	sprintf(instrument,"hpe1445a");
		if ((err=hpe1445a_reset(arb_gen_handle)) != 0)
			display_error();

	if (busconfi_type & RF)
	{
		/* Reset RF Stim */
		// Following code determines which RF Source is installed and then resets it
		if (RF_Stim)
		{
			sprintf(instrument,"gt50000a");

			if ((err = gt50000_reset(rf_stim_handle)) != 0)
				display_error();
		}
		else
		{
			sprintf(instrument,"eip1140a");
	
			if ((err = eip1140a_reset(rf_stim_handle)) != 0)
				display_error();
		}

		/* Reset Power Meter */
		sprintf(instrument,"hpe1416a");
		if ((err = hpe1416a_reset(rf_pwr_handle)) != 0)
			display_error();

		/* Reset RF Counter */
		sprintf(instrument,"hpe1420b RF");
		if ((err = rfcnt_reset(rfcounter_handle)) != 0)
			display_error();

		/* Reset RF Measurement Analyzer */
		sprintf(instrument,"b8701");
		Buf = "PG99RE";
		if ((err = b8701_writeInstrData(meas_analyzer_handle,Buf)) != 0)
			display_error();
		Buf = "CL";
		if ((err = b8701_writeInstrData(meas_analyzer_handle,Buf)) != 0)
			display_error();
	}

	/* Reset DC Power Supplies 10 through 1*/
	for (supply=10; supply>0; supply--)
	{
		switch(supply)
		{
			case 1:  sprintf(DCP_ADDR,"GPIB0::5::1::Instr"); break;
			case 2:  sprintf(DCP_ADDR,"GPIB0::5::2::Instr"); break;
			case 3:  sprintf(DCP_ADDR,"GPIB0::5::3::Instr"); break;
			case 4:  sprintf(DCP_ADDR,"GPIB0::5::4::Instr"); break;
			case 5:  sprintf(DCP_ADDR,"GPIB0::5::5::Instr"); break;
			case 6:  sprintf(DCP_ADDR,"GPIB0::5::6::Instr"); break;
			case 7:  sprintf(DCP_ADDR,"GPIB0::5::7::Instr"); break;
			case 8:  sprintf(DCP_ADDR,"GPIB0::5::8::Instr"); break;
			case 9:  sprintf(DCP_ADDR,"GPIB0::5::9::Instr"); break;
			case 10: sprintf(DCP_ADDR,"GPIB0::5::10::Instr"); break;
		}

		/* Do not allow a supply to get a command within 5 seconds *
		 * after it has been reset                                 */
		while((ps_reset_time[supply] + 5000) > timeGetTime());

		sprintf(instrument,"aps6062_%d",supply);
		sprintf(string, "%c%c%c", 0x10 + supply, 0x80, 0x80);
		Buf = &string[0];
		err = aps6062_writeInstrData(dcp_handle[supply],Buf);
     
		for(i=1; (err == VI_ERROR_NLISTENERS) && i<5; i++)
		{
			if ((err = aps6062_writeInstrData(dcp_handle[supply],Buf)) != 0)
				display_error();
		}

		ps_reset_time[supply] = timeGetTime();
	}

	/* Electro Optics */
//	sprintf(instrument,"Electro Optics");
//	err = EO______reset(EO_handle);
//	display_error();

	/* Reset Switching */
	sprintf(instrument,"ri1260");
	/* Open all switch relays */
	if ((err = viWrite(modsw_handle, (unsigned char *) "RESET\n", (ViUInt32) 6, &retCnt)) != 0)
		display_error();
	Sleep(40);
	cls_mux_interconnects(); /* Close the -38 mux configuration relays */
#endif //JimW
}

/******************************************************************************
 * Closes all handles but DWG                                                 *
 ******************************************************************************/
void termNonDwg()
{
	//char DCP_ADDR[20];
	//int supply;

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering termNonDwg()\n");

#ifdef JimW
	/* DMM */
	sprintf(instrument,"hpe1412a");
	if ((err = hpe1412a_close(dmm_handle)) != 0)
		display_error();
	
	/* Counter */
	sprintf(instrument,"hpe1420b");
	if ((err = hpe1420b_close(counter_timer_handle)) != 0)
		display_error();
	
	/* Scope */
	sprintf(instrument,"hpe1428a");
	if ((err = hpe1428a_close(oscope_handle)) != 0)
		display_error();
	
	///* Function Generator */
	//sprintf(instrument,"ri3152");
	//if ((err = ri3151_close(func_gen_handle)) != 0)
	//	display_error();
	
	/* ARB */
	sprintf(instrument,"hpe1445a");
	if ((IsSimOrDeb("ARB_GEN") & SIMSOFT_FLAG) != SIMSOFT_FLAG)
		if ((err = hpe1445a_close(arb_gen_handle)) != 0)
			display_error();
	
	if (busconfi_type & RF)
	{
		/* RF Stim */
		// Following code determines which RF Source is installed and then terminates it
		if (RF_Stim)
		{
			sprintf(instrument,"gt50000a");
	
			if ((err = gt50000_close(rf_stim_handle)) != 0)
				display_error();
		}
		else
		{
			sprintf(instrument,"eip1140a");

			if ((err = eip1140a_close(rf_stim_handle)) != 0)
				display_error();
		}	
	
		/* Power Meter */
		sprintf(instrument,"hpe1416a");
		if ((err = hpe1416a_close(rf_pwr_handle)) != 0)
			display_error();
	
		/* RF Counter */
		sprintf(instrument,"hpe1420b RF");
		if ((err = rfcnt_close(rfcounter_handle)) != 0)
			display_error();
	
		/* RF Measurement Analyzer */
		sprintf(instrument,"b8701");
		if ((err = b8701_close(meas_analyzer_handle)) != 0)
			display_error();
	
		/* Close Downconverter Handle */
		if (DownconverterType == 1)
		{
			sprintf(instrument,"eip1315a");
			if ((err = eip1315a_close(lo_downconverter_handle)) != 0)
				display_error();
		}
		else if (DownconverterType == 2)
		{
			sprintf(instrument,"gt55210a");
			if ((err = gt55210a_close(lo_downconverter_handle)) != 0)
				display_error();
		}
	}

	/* DC Power Supplies 10 through 1*/
	for (supply=10; supply>0; supply--)
	{
		switch(supply)
		{
			case 1:  sprintf(DCP_ADDR,"GPIB0::5::1::Instr"); break;
			case 2:  sprintf(DCP_ADDR,"GPIB0::5::2::Instr"); break;
			case 3:  sprintf(DCP_ADDR,"GPIB0::5::3::Instr"); break;
			case 4:  sprintf(DCP_ADDR,"GPIB0::5::4::Instr"); break;
			case 5:  sprintf(DCP_ADDR,"GPIB0::5::5::Instr"); break;
			case 6:  sprintf(DCP_ADDR,"GPIB0::5::6::Instr"); break;
			case 7:  sprintf(DCP_ADDR,"GPIB0::5::7::Instr"); break;
			case 8:  sprintf(DCP_ADDR,"GPIB0::5::8::Instr"); break;
			case 9:  sprintf(DCP_ADDR,"GPIB0::5::9::Instr"); break;
			case 10: sprintf(DCP_ADDR,"GPIB0::5::10::Instr"); break;
		}
    
		sprintf(instrument,"aps6062_%d",supply);
		if ((err = aps6062_close(dcp_handle[supply])) != 0)
			display_error();
	}

	/* Electro Optics */
//	sprintf(instrument,"Electro Optics");
//	err = EO_____close(EO_handle);
//	display_error();

	/* Switching */
	sprintf(instrument,"ri1260");
	if ((err = ri1260_close(modsw_handle)) != 0)
		display_error();

	if ((allocInstrumentsReadBuffers(UNALLOC)) < 0) {
		display_error_and_unload();
	}
	else {
		allocAlreadyDone = NOTDONE;
		allocFreed = BEENDONE;
	}
#endif //JimW

}

/******************************************************************************
 * Called by File Load -> INITIALIZE macro <SYS_INIT>                         *
 * Use to get global Visa handles                                             *
 ******************************************************************************/
void SetupRTS()
{

	//int				supply;
	//HANDLE			hdEventThreadHandle = NULL;
	struct _stat	tmpBuf;

	ZeroMemory(&tmpBuf, sizeof(tmpBuf));

#ifdef JimW
	sprintf(cemInfo.logLocation, "%s", LOGLOCATION);
	sprintf(cemInfo.debugOption, "%s%s", cemInfo.logLocation, DEBUGIT);	
	sprintf(probeInfo.makeMeasurementName, "%s", PROBEBUTTONEVENT);
	sprintf(probeInfo.sendReturnKeyName, "%s", SENDKEYEVENT);

	DE_BUG = _stat(cemInfo.debugOption, &tmpBuf) == CEM_ERROR ? 0 : 1;

	if (DE_BUG) {

		char debugFile[CEM_MAX_PATH];

		sprintf(debugFile, "%s%s", cemInfo.logLocation, DEBUGFILENAME);

		DE_BUG = ((debugfp = fopen(debugFile, "w+b")) == NULL) ? 0 : 1;
	}

	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering SetupRTS()\n");

	strcat(version, WCEMVERSION);             	/* Used for displaying version	*/ 
	Display( version );

	for(supply=1; supply<11; supply++)
	{
		ps_reset_time[supply] = 0;
	}

	initNonDwg();     // for non-DWG
	reset_once_only = 0;
	resetNonDwg();    // for non-DWG
	reset_once_only = 1;
    initDWG();		  // for DWG

	if ((probeInfo.hSendReturnKeyHandle = CreateEvent(0, TRUE, FALSE, probeInfo.sendReturnKeyName)) == NULL) {
		ErrMsg(7, "Unable to initialize CreateEvent");
	}

	if ((probeInfo.hMakeMeasurementHandle = OpenEvent(EVENT_ALL_ACCESS, 0,
													  probeInfo.makeMeasurementName)) == NULL) {
		retrieveErrorMessage("SetupRTS()", "OpenEvent recieved an error: ");
		ErrMsg(7, "Unable to open event makeMeasurementName");
	}

	if ((probeInfo.hSendReturnKeyHandle = OpenEvent(EVENT_ALL_ACCESS, 0,
													probeInfo.sendReturnKeyName)) == NULL) {
		retrieveErrorMessage("SetupRTS()", "OpenEvent recieved an error: ");
		ErrMsg(7, "Unable to open event sendReturnKeyName");
	}

	if (initPLCDataTables()) {
		ErrMsg(7, "Unable to initialize Path Loss Tables");
	}

    urgent_halt = 0;  // Initialize the variable that sets SETFAULT for interupts

	if ((hdEventThreadHandle = CreateThread(NULL, 0, StartEventThread, NULL, 0, NULL))== NULL) {
		ErrMsg(7, "Unable to initialize Event Thread");
	}
#endif //JimW
}

/******************************************************************************
 * Called by File Load        -> RESETALL macro <SYS_RESET>                   * 
 * Called by REMOVE,ALL       -> RESETALL macro <SYS_RESET>                   *
 * Called by TERMINATE, ATLAS -> RESETALL macro <SYS_RESET>                   *
 * Used to unconditionally reset all with existing handles                    *
 ******************************************************************************/
void ResetRTS()
{
	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering ResetRTS()\n");

	//resetNonDwg();  /* for non-DWG  */
	//reset_once_only = 0;

#ifdef JimW

 //   //--
 //   // Defining the ground reference used by the DTI
 //   if (!dwgsimflag)
 //     {
 //       pnpstatus = terM9_setGroundReference(dwg_handle,TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL);
 //       PNPstatus("terM9_setGroundReference");
 //     }
 //   if (dwgdebugflag) 
 //       Display("(debug) terM9_setGroundReference(dwg_handle,TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)\n");
 //   if (!dwgsimflag)
 //     {
 //       pnpstatus = terM9_setSystemEnable(dwg_handle,VI_TRUE);
 //       PNPstatus("terM9_setSystemEnable");
 //     }
 //   if (dwgdebugflag) Display("(debug) terM9_setSystemEnable(dwg_handle,VI_TRUE)\n");
 //   //--
 //   // Setting the impedance of 50 Ohm for the system
 //   if (!dwgsimflag)
 //     {
 //       pnpstatus = terM9_setChannelImpedance(dwg_handle,1,dwgsystempins,TERM9_IMPED_50OHM);
 //       PNPstatus("terM9_setChannelImpedance");
 //     }
 //   if (dwgdebugflag)
 //       Display("(debug) terM9_setChannelImpedance(dwg_handle,1,dwgsystempins,TERM9_IMPED_50OHM)\n");
 //   //--
 //   // Set the static delay to a defult value; 100usec (from 100nsec to 6.5536msec)
 //   if(!dwgsimflag)
 //     {
 //       pnpstatus = terM9_setStaticPatternDelay(dwg_handle,0.005);  // Note: this value might be brought to 1 Micro
 //       PNPstatus("terM9_setStaticPatternDelay");
 //     }
 //   if(dwgdebugflag)
 //       Display("(debug) terM9_setStaticPatternDelay(dwg_handle,0.0001);\n");

    //Events_Clear();  // Clear the table of Events (from DwgDAta)
    //VL_ResetLevelIndexUsage( _VLALL );    // Clear VL usage (from DwgData)
    //if (dwgdebugflag) DWGDspVLUsage();
#endif // JimW

	// Reset all Mode Values	
	IsSimOrDeb("INIT_VARS");
}

/******************************************************************************
 * Called by File UnLoad -> TERMINATE macro <SYS_TERM>                        *
 * Used to unconditionally reset all with existing handles                    *
 *  and close all handles.                                                    *
 ******************************************************************************/
void TermRTS()
{
	if ((IsSimOrDeb("SYS_INIT") & flag) == flag)
		Display("wcem debug: Entering TermRTS()\n");
#ifdef JimW

	///* resetNonDwg();    for non-DWG  */
	//termNonDwg();     /* for non-DWG  */

 //   if (!dwgsimflag) 
 //     {
 //       pnpstatus = terM9_close(dwg_handle);
 //       PNPstatus("terM9_close");
 //     }
 //   if (dwgdebugflag) 
 //       Display("(debug) terM9_close(dwg_handle)\n");

 //   // freeing the memory
 //   if (dwg_tmppinlist != NULL) 
 //     {
 //       if (dwgdebugflag) Display("(debug) Freeing the global allocated memory (dwg_tmppinlist)\n");
 //       free(dwg_tmppinlist);
 //     }
 //   if (dwgsavecomp != NULL)
 //     {
 //       if (dwgdebugflag) Display("(debug) Freeing the global allocated memory (dwgsavecomp)\n");
 //       free(dwgsavecomp);
 //     }
 //   if (dwgerror != NULL)
 //     {
 //       if (dwgdebugflag) Display("(debug) Freeing the global allocated memory (dwgerror)\n");
 //       free(dwgerror);
 //     }
 //   if (dwgerrorindex != NULL)
 //     {
 //       if (dwgdebugflag) Display("(debug) Freeing the global allocated memory (dwgerrorindex)\n");
 //       free(dwgerrorindex);
 //     }

 //   // 
	//CloseHandle(probeInfo.hMakeMeasurementHandle);
	//CloseHandle(probeInfo.hSendReturnKeyHandle);
 //	CloseHandle(eventsInfo.hMakeMeasurementHandle);
	//CloseHandle(eventsInfo.hSendReturnKeyHandle);
 //  Events_Clear();  // Clear the table of Events (from DwgDAta)
 //   VL_ResetLevelIndexUsage( _VLALL );    // Clear VL usage (from DwgData)
 //   if (dwgdebugflag) DWGDspVLUsage();
#endif //JimW
}

/******************* PLC ***************************************************
 *  PD Reference: paragraph 3.6.3 dated 21 March 1996 update 30 April 1996 *
 ***************************************************************************/
/******************* PLC *****************************************************
 *  PD Reference: paragraph 3.6.3 dated 21 March 1996 update 30 April 1996   *
 *  PLC (path loss compensation) interface opens tets.ini file in the windows*
 * 	directory and extracts nessary data.									 *
 *****************************************************************************/
void get_plc(double freq)
{

	if ((IsSimOrDeb("RF_GEN") & flag) == flag)
		Display(" get_plc debug: Entering get_plc()\n");

	if ((IsSimOrDeb("RF_GEN") & flag) == flag)
	{
		sprintf_s(msg_buf, sizeof(msg_buf), " get_plc debug: path_id = %s \n", path_id[0]);
		Display(msg_buf);
	}
	sys_loss = GetPLC(freq, path_id);

    if ((IsSimOrDeb("RF_GEN") & flag) == flag)
	{
		sprintf_s(msg_buf, sizeof(msg_buf), " get_plc debug: SYS_LOSS: %6.3f\n", sys_loss);
		Display(msg_buf);
	}

}

/**********************************************************************
 *  Function to determine path loss for resistance measurements       *
 **********************************************************************/
double get_res_plc()
{

	if (IsSimOrDeb("DMM") & FLAG)
		Display(" dmm debug: Entering get_res_plc()\n");

	return(GetResPLC(lf_sw_path, no_of_switches));

}

///**********************************************************************
// *  Function to allocate the instruments read buffers			       *
// **********************************************************************/
//int allocInstrumentsReadBuffers(int mode)
//{
//	int dummy = mode;
//#ifdef JimW
//	if ((allocAlreadyDone == BEENDONE && mode == ALLOC) || 
//		(allocFreed == BEENDONE  && mode == UNALLOC)) {
//		return (0);
//	}
//
//	if ((err = allocateArbGenReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocateCounterTimerReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocateDcPwrSupplyReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocateDigScoprReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocateDMMReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocateElecOptReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocateFuncGenReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocateRFGenReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocateRFMeasAnReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//	if ((allocatePwrMeterReadBuffer(mode)) < 0) {
//		err = (-1);
//		return(-1);
//	}
//
//#endif
//	return(0);
//}
