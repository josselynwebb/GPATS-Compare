/*
 * Copyright 1996 by Teradyne, Inc., Boston, MA
 *
 * Module:   terM9.h
 * Creator:  Teresa P. Lopes
 *
 * Abstract: This file contains the function prototypes for the M9 VXI
 *           plug&play driver.
 *
 * Revision History:
 *
 * M90510.11 22-Oct-04 11:56:00    bwf    terM9_setLevelSetReferenceSource and terM9_getLevelSetReferenceSource
 *                                        are in the public doc, and we have a customer that wants to use them.
 *                                        So, since no one can remember why they might be private, they have been
 *                                        moved back into the public header files.
 * M90510.10 30-Sep-04 13:10:00    bwf    Added TERM9_RESULT_STOPPED, which is the value returned as
 *                                        the result in terM9_fetchDynamicPatternSetResult if
 *										  terM9_stopDynamicPatternSetExecution was used to halt the
 *										  pattern set.
 * M90401.08 15-Aug-01 16:26:00    bzz    Enclosed terM9_lock and terM9_unlock in 
 *										  #ifdef INSTR_LANGUAGE_SPECIFIC 
 *										  so that they will be ignored by HPVEE when it process the header file
 * M90304.xx 16-Mar-00 17:52:34    tpl    Fix datatypes for interrupt handler
 * M90303.74 16-Jul-99 07:39:15    tpl    Misc headers - add ifdef so that visa includes work with
 *                                                       Bristol porting tool
 * M90303.24 02-Dec-98 11:12:08    tpl    terM9.h/cpp - fix prototypes
 *                                                      make all idx - index
 * M90303.14 13-Oct-98 11:28:58    tpl    Add alias for terM9_fetchFrontPanelSyncResourceState
 *                                        Name is too long for LabWindows
 * M90303.12 09-Oct-98 13:16:06    lkf    add VAL_TSET_MIN_PULSE_WIDTH,
 *                                        VAL_TSET_MIN_WINDOW_WIDTH
 * M90303.00 03-Aug-98 13:16:06    lkf    made a few M920 edits
 * M90302.25 01-Jun-98 13:16:06    lkf    add terM9_runHighVoltagePinsTest,
 *                                        terM9_fetchHighVoltagePinsDetectState
 * M90302.24 21-May-98 13:16:06    lkf    scrubbed term9.h, add
 *                                        terM9_fetchFrontPanelSyncResourceState
 *                                        terM9_fetchBackplaneSyncResourceState
 *                                        terM9_setAlignmentOption
 *                                        terM9_getAlignmentOption
 * M90302.18 16-Apr-98 09:59:14    lkf    add terM9_executeFullSelfTest, fix
 *                                        terM9_fetchDynamicPinResults, fix
 *                                        terM9_getUserClockPhaseX
 * M90302.13 25-Feb-98 10:03:51    tpl    Add get scope functions
 * M90302.10 02-Feb-98 16:45:00    lkf    added prgmming limitations
 *                                        for emerald oscilattions
 * M90302.08 26-Jan-98 16:45:00  tpl/lkf  added suport for sync res DAC
 *                                        and probe button disabling
 * M90302.07 08-Jan-98 14:42:00    lkf    merged all public header files
 * M90301.28 30-Oct-97 16:55:57    tpl    Add diagnostic selftest function
 * M90301.11 08-Aug-97 13:08:43    tpl    Can't use user defined datatypes in
 *                                        the header file, will not work with
 *                                        HPVee
 * M90301.10 07-Aug-97 19:12:28    tpl    Move DLLEXPORT stuff to .def file
 * M90301.08 06-Aug-97 13:10:12    tpl    More cleanup
 *                                        Add more functions
 * M90301.06 31-Jul-97 15:15:48    tpl    Rename getLastSyncResourceIndex
 *                                        Add new data types for interrupts
 * M90301.05 31-Jul-97 11:13:00    tpl    Probe updates
 * M90301.04 28-Jul-97 16:55:50    tpl    make setResultIndex private
 * M90301.02 08-Jul-97 09:19:58    tpl    Add resetHighVoltagePins
 * M90301.01 03-Jul-97 09:20:52    tpl    make all bitvector unsigned
 * M90301.00 25-Jun-97 10:58:40    tpl    Remove UserClock Impedance
 *                                        fix order of return codes
 *                                        add errors for get/setTimingSetInfo
 * M90300.10 10-Jun-97 08:49:23    tpl    Add an extern "C" arount all protos
 *                                        so they just work from c++
 *                                        #ifdef the language specific
 *                                        function.  Add new tset functions
 * M90300.09 30-May-97 09:41:29    tpl    Change name of DLL #define
 * M90300.08 23-May-97 09:34:54    tpl    Fix getPinmapValues prototype
 * M90300.06 14-May-97 08:58:41    tpl    Add missing functions and additional
 *                                        parameters
 *                                        add terM9_initializeInstrument
 * M90300    22-Apr-97 19:55:51    tpl    Sync header with API doc
 * M90252    06-Mar-97 21:38:32    tpl    Add LSRTAP stuff
 * M90246    04-Mar-97 16:31:45    tpl    Add missing externs
 * M90243    03-Mar-97 08:18:00    aam    Change all Idx in fn names to Index
 *                                        Change Utility Bit to HVPin
 * M90240    24-Feb-97 19:26:21    tpl    Change orginization of high level
 *                                        functions
 * M90235    27-Jan-97 09:00:58    tpl    Fix includes
 * M90228    19-Dec-96 16:31:13    tpl    Change STEP and TEST to be parameters
 *                                        of loadDynamicPattern and
 *                                        runStaticPattern instead of
 *                                        setXXXPattern
 * M90227    19-Dec-96 13:45:52    tpl    loadPSETIndexes -> loadProbeTimingSetIndexes
 * M90224    11-Dec-96 15:41:53    tpl    Really add parameter to executDyntest
 * M90223    10-Dec-96 18:03:31    tpl    Add parameter to executeDynamicTest
 *                                        Index -> Idx
 *                                        PSET/TSET -> Probe/Timing Set
 *                                        Indices -> Indexes
 * M90222    03-Dec-96 20:14:42    tpl    static/dynamic edits
 * M90215    05-Nov-96 09:51:54    tpl    fix declaration of
 *                                        initiateFormattedBurstExecution
 * M90209    05-Nov-96 08:43:11    tpl    merge awm and gns changes
 * M90202    09-Oct-96 10:50:00    gns    Fix prototype for getChannelWindow,
 *                                        for all set/getTimingSet functions,
 *                                        for all set/getphase functions,
 *                                        prepareFormattedBurstLoading,
 *                                        fetchFormattedPinResults,
 *                                        fetchNonFormattedPinResults
 * M90201    05-Oct-96 22:50:51    tpl    Creation
 */


#ifndef __TERM9_LOADED
#define __TERM9_LOADED 1
#if defined (__cplusplus) || defined (__cplusplus__) || defined (_MSC_VER) || defined(_CVI_)
#ifndef INSTR_CALLBACKS
#define INSTR_CALLBACKS
#endif


#ifndef INSTR_LANGUAGE_SPECIFIC
#define INSTR_LANGUAGE_SPECIFIC
#endif

#endif

#if defined (__cplusplus) || defined (__cplusplus__)
extern "C" {
#endif

/*
 * When compiling with the Bristol tool on unix, can't have WIN32 defined when
 * including vpptype.h.  After the include redefine WIN32
 */
#ifdef _WINDU_SOURCE
#undef WIN32
#undef _WIN32
#endif
#include "vpptype.h"
#include "visa.h"
#ifdef _WINDU_SOURCE
#define WIN32
#define _WIN32
#endif

#ifndef __TERM9_ERRORS_LOADED
#define __TERM9_ERRORS_LOADED 1
#define TERM9_ERROR_FIRST (_VI_ERROR + 0x3FFC0800L)
#define TERM9_ERROR_ADC_CONNECT (TERM9_ERROR_FIRST + 0)
#define TERM9_ERROR_AMMTR_MODE (TERM9_ERROR_FIRST + 1)
#define TERM9_ERROR_BOARDIDX (TERM9_ERROR_FIRST + 2)
#define TERM9_ERROR_PATTERNSET_TIMEOUT (TERM9_ERROR_FIRST + 3)
#define TERM9_ERROR_CARDIDX (TERM9_ERROR_FIRST + 4)
#define TERM9_ERROR_CCOND (TERM9_ERROR_FIRST + 5)
#define TERM9_ERROR_CCOUNT (TERM9_ERROR_FIRST + 6)
#define TERM9_ERROR_CHAN_CAPTURE (TERM9_ERROR_FIRST + 7)
#define TERM9_ERROR_CHAN_CHANMODE (TERM9_ERROR_FIRST + 8)
#define TERM9_ERROR_CHAN_CONNECT (TERM9_ERROR_FIRST + 9)
#define TERM9_ERROR_CHAN_FORMAT (TERM9_ERROR_FIRST + 10)
#define TERM9_ERROR_CHAN_IMPEDANCE (TERM9_ERROR_FIRST + 11)
#define TERM9_ERROR_CHAN_LEVELIDX (TERM9_ERROR_FIRST + 12)
#define TERM9_ERROR_CHAN_LOAD (TERM9_ERROR_FIRST + 13)
#define TERM9_ERROR_CHAN_PHASEIDX (TERM9_ERROR_FIRST + 14)
#define TERM9_ERROR_CHAN_WINDOWIDX (TERM9_ERROR_FIRST + 15)
#define TERM9_ERROR_CLK_DIVIDER (TERM9_ERROR_FIRST + 16)
#define TERM9_ERROR_CLK_EDGE (TERM9_ERROR_FIRST + 17)
#define TERM9_ERROR_CLK_MODE (TERM9_ERROR_FIRST + 18)
#define TERM9_ERROR_CLK_OUTPATH (TERM9_ERROR_FIRST + 19)
#define TERM9_ERROR_CLK_SOURCE (TERM9_ERROR_FIRST + 20)
#define TERM9_ERROR_CLUTCH_ENABLE (TERM9_ERROR_FIRST + 21)
#define TERM9_ERROR_CLUTCH_POLARITY (TERM9_ERROR_FIRST + 22)
#define TERM9_ERROR_COPCODE (TERM9_ERROR_FIRST + 23)
#define TERM9_ERROR_CPP (TERM9_ERROR_FIRST + 24)
#define TERM9_ERROR_EXPANDLOOPS (TERM9_ERROR_FIRST + 25)
#define TERM9_ERROR_FAIL_CAPTURE (TERM9_ERROR_FIRST + 26)
#define TERM9_ERROR_FILE_OPEN (TERM9_ERROR_FIRST + 27)
#define TERM9_ERROR_FILE_PROC (TERM9_ERROR_FIRST + 28)
#define TERM9_ERROR_GROUND_REFERENCE (TERM9_ERROR_FIRST + 29)
#define TERM9_ERROR_GROUPSTATE (TERM9_ERROR_FIRST + 30)
#define TERM9_ERROR_HVPIN_COUNT (TERM9_ERROR_FIRST + 31)
#define TERM9_ERROR_HVPIN_MODE (TERM9_ERROR_FIRST + 32)
#define TERM9_ERROR_HVPIN_THRESHOLD (TERM9_ERROR_FIRST + 33)
#define TERM9_ERROR_HVPINIDX (TERM9_ERROR_FIRST + 34)
#define TERM9_ERROR_IOH (TERM9_ERROR_FIRST + 35)
#define TERM9_ERROR_IOL (TERM9_ERROR_FIRST + 36)
#define TERM9_ERROR_LEARN_FLAG (TERM9_ERROR_FIRST + 37)
#define TERM9_ERROR_LEVELIDX (TERM9_ERROR_FIRST + 38)
#define TERM9_ERROR_LOGIC_SELECT (TERM9_ERROR_FIRST + 39)
#define TERM9_ERROR_LOWPOWER (TERM9_ERROR_FIRST + 40)
#define TERM9_ERROR_MAPTYPE (TERM9_ERROR_FIRST + 41)
#define TERM9_ERROR_MAX_PATTERNSET_TIMEOUT (TERM9_ERROR_FIRST + 42)
#define TERM9_ERROR_MTR_CURRENT_OVERLOAD (TERM9_ERROR_FIRST + 43)
#define TERM9_ERROR_MTR_CURRENT_RANGE (TERM9_ERROR_FIRST + 44)
#define TERM9_ERROR_MTR_FORCE_CURRENT (TERM9_ERROR_FIRST + 45)
#define TERM9_ERROR_MTR_INPUT (TERM9_ERROR_FIRST + 46)
#define TERM9_ERROR_MTR_IRNG (TERM9_ERROR_FIRST + 47)
#define TERM9_ERROR_MTR_NEG_CLMP_VOLT (TERM9_ERROR_FIRST + 48)
#define TERM9_ERROR_MTR_POS_CLMP_VOLT (TERM9_ERROR_FIRST + 49)
#define TERM9_ERROR_MTR_VOLTAGE_RANGE (TERM9_ERROR_FIRST + 50)
#define TERM9_ERROR_NOT_ENOUGH_DATA (TERM9_ERROR_FIRST + 51)
#define TERM9_ERROR_OVER_TEMP (TERM9_ERROR_FIRST + 52)
#define TERM9_ERROR_PATTERN_COUNT (TERM9_ERROR_FIRST + 53)
#define TERM9_ERROR_PATTERN_DELAY (TERM9_ERROR_FIRST + 54)
#define TERM9_ERROR_PATTERNIDX (TERM9_ERROR_FIRST + 55)
#define TERM9_ERROR_PHASE_FREERUN (TERM9_ERROR_FIRST + 56)
#define TERM9_ERROR_PHASE_TRIGGER (TERM9_ERROR_FIRST + 57)
#define TERM9_ERROR_PHASEIDX (TERM9_ERROR_FIRST + 58)
#define TERM9_ERROR_PINOPCODE (TERM9_ERROR_FIRST + 59)
#define TERM9_ERROR_POSTCOUNT (TERM9_ERROR_FIRST + 60)
#define TERM9_ERROR_PROBE_METHOD (TERM9_ERROR_FIRST + 61)
#define TERM9_ERROR_PRECOUNT (TERM9_ERROR_FIRST + 62)
#define TERM9_ERROR_PRECOUNT_FLAG (TERM9_ERROR_FIRST + 63)
#define TERM9_ERROR_PRHI (TERM9_ERROR_FIRST + 64)
#define TERM9_ERROR_PRLO (TERM9_ERROR_FIRST + 65)
#define TERM9_ERROR_PROBE_CAPTURE (TERM9_ERROR_FIRST + 66)
#define TERM9_ERROR_PROBE_CLOSE (TERM9_ERROR_FIRST + 67)
#define TERM9_ERROR_PROBE_CONFIG (TERM9_ERROR_FIRST + 68)
#define TERM9_ERROR_PROBE_EXECUTE (TERM9_ERROR_FIRST + 69)
#define TERM9_ERROR_PROBE_MODE (TERM9_ERROR_FIRST + 70)
#define TERM9_ERROR_PROBE_STATCAPTURE (TERM9_ERROR_FIRST + 71)
#define TERM9_ERROR_PROBE_OPEN (TERM9_ERROR_FIRST + 72)
#define TERM9_ERROR_PSETIDX (TERM9_ERROR_FIRST + 73)
#define TERM9_ERROR_PSTACK_UNDERFLOW (TERM9_ERROR_FIRST + 74)
#define TERM9_ERROR_PSTACK_OVERFLOW (TERM9_ERROR_FIRST + 75)
#define TERM9_ERROR_REF_OVERLOAD (TERM9_ERROR_FIRST + 76)
#define TERM9_ERROR_REGISTER_READ (TERM9_ERROR_FIRST + 77)
#define TERM9_ERROR_REGISTER_WRITE (TERM9_ERROR_FIRST + 78)
#define TERM9_ERROR_RESULT_CAPTURE (TERM9_ERROR_FIRST + 79)
#define TERM9_ERROR_RESULT_COUNT (TERM9_ERROR_FIRST + 80)
#define TERM9_ERROR_RESULTIDX (TERM9_ERROR_FIRST + 81)
#define TERM9_ERROR_SCOPE_CHAN (TERM9_ERROR_FIRST + 82)
#define TERM9_ERROR_SCOPE_LEVELS (TERM9_ERROR_FIRST + 83)
#define TERM9_ERROR_SCOPE_DMTX (TERM9_ERROR_FIRST + 84)
#define TERM9_ERROR_SCOPE_LOWPOWER (TERM9_ERROR_FIRST + 85)
#define TERM9_ERROR_SCOPE_PHASE (TERM9_ERROR_FIRST + 86)
#define TERM9_ERROR_SCOPE_TSET (TERM9_ERROR_FIRST + 87)
#define TERM9_ERROR_SCOPEIDX (TERM9_ERROR_FIRST + 88)
#define TERM9_ERROR_SLEWRATE (TERM9_ERROR_FIRST + 89)
#define TERM9_ERROR_STEP (TERM9_ERROR_FIRST + 90)
#define TERM9_ERROR_STEP_COUNT (TERM9_ERROR_FIRST + 91)
#define TERM9_ERROR_STEP_ONLY (TERM9_ERROR_FIRST + 92)
#define TERM9_ERROR_STEPIDX (TERM9_ERROR_FIRST + 93)
#define TERM9_ERROR_STMTX_CONNECT (TERM9_ERROR_FIRST + 94)
#define TERM9_ERROR_STMTX_FP_CONNECT (TERM9_ERROR_FIRST + 95)
#define TERM9_ERROR_STMTX_GLOBAL (TERM9_ERROR_FIRST + 96)
#define TERM9_ERROR_STOP (TERM9_ERROR_FIRST + 97)
#define TERM9_ERROR_STOP_ON_EVENT (TERM9_ERROR_FIRST + 98)
#define TERM9_ERROR_STOP_ON_FAIL (TERM9_ERROR_FIRST + 99)
#define TERM9_ERROR_STOP_ON_LSEQ (TERM9_ERROR_FIRST + 100)
#define TERM9_ERROR_STOP_ON_PATTERN (TERM9_ERROR_FIRST + 101)
#define TERM9_ERROR_STOP_ON_PATT_IDX (TERM9_ERROR_FIRST + 102)
#define TERM9_ERROR_STOP_ON_PROBE (TERM9_ERROR_FIRST + 103)
#define TERM9_ERROR_SYNC_PULSEGEN_WIDTH (TERM9_ERROR_FIRST + 104)
#define TERM9_ERROR_SYNC_PULSEGENIDX (TERM9_ERROR_FIRST + 105)
#define TERM9_ERROR_SYNC_STATE (TERM9_ERROR_FIRST + 106)
#define TERM9_ERROR_SYNCIDX (TERM9_ERROR_FIRST + 107)
#define TERM9_ERROR_SYSTEM_ENABLE (TERM9_ERROR_FIRST + 108)
#define TERM9_ERROR_TEST (TERM9_ERROR_FIRST + 109)
#define TERM9_ERROR_TIMER_START (TERM9_ERROR_FIRST + 110)
#define TERM9_ERROR_TIMING_MODE (TERM9_ERROR_FIRST + 111)
#define TERM9_ERROR_TSET_CLOCK (TERM9_ERROR_FIRST + 112)
#define TERM9_ERROR_TSET_PHS_ASSERT (TERM9_ERROR_FIRST + 113)
#define TERM9_ERROR_TSET_PHS_COUNT (TERM9_ERROR_FIRST + 114)
#define TERM9_ERROR_TSET_PHS_RETURN (TERM9_ERROR_FIRST + 115)
#define TERM9_ERROR_TSET_WDW_CLOSE (TERM9_ERROR_FIRST + 116)
#define TERM9_ERROR_TSET_WDW_COUNT (TERM9_ERROR_FIRST + 117)
#define TERM9_ERROR_TSET_WDW_OPEN (TERM9_ERROR_FIRST + 118)
#define TERM9_ERROR_TSETIDX (TERM9_ERROR_FIRST + 119)
#define TERM9_ERROR_UCLK_CONNECT (TERM9_ERROR_FIRST + 120)
#define TERM9_ERROR_UCLK_HIGHV (TERM9_ERROR_FIRST + 121)
#define TERM9_ERROR_UCLK_IMPEDANCE (TERM9_ERROR_FIRST + 122)
#define TERM9_ERROR_UCLK_LOWV (TERM9_ERROR_FIRST + 123)
#define TERM9_ERROR_UCLK_MODE (TERM9_ERROR_FIRST + 124)
#define TERM9_ERROR_UCLK_PHS_ASSERT (TERM9_ERROR_FIRST + 125)
#define TERM9_ERROR_UCLK_PHS_COUNT (TERM9_ERROR_FIRST + 126)
#define TERM9_ERROR_UCLK_PHS_RETURN (TERM9_ERROR_FIRST + 127)
#define TERM9_ERROR_UCLK_TRIGGER (TERM9_ERROR_FIRST + 128)
#define TERM9_ERROR_UCLKIDX (TERM9_ERROR_FIRST + 129)
#define TERM9_ERROR_VCOM (TERM9_ERROR_FIRST + 130)
#define TERM9_ERROR_VIH (TERM9_ERROR_FIRST + 131)
#define TERM9_ERROR_VIL (TERM9_ERROR_FIRST + 132)
#define TERM9_ERROR_VOH (TERM9_ERROR_FIRST + 133)
#define TERM9_ERROR_VOL (TERM9_ERROR_FIRST + 134)
#define TERM9_ERROR_VOLTAGE_SWING (TERM9_ERROR_FIRST + 135)
#define TERM9_ERROR_VXI_ACFAIL (TERM9_ERROR_FIRST + 136)
#define TERM9_ERROR_VXI_BUSERROR (TERM9_ERROR_FIRST + 137)
#define TERM9_ERROR_VXI_MAPERROR (TERM9_ERROR_FIRST + 138)
#define TERM9_ERROR_VXI_SYSFAIL (TERM9_ERROR_FIRST + 139)
#define TERM9_ERROR_VXI_SYSRESET (TERM9_ERROR_FIRST + 140)
#define TERM9_ERROR_WINDOWIDX (TERM9_ERROR_FIRST + 141)
#define TERM9_LIC_EDGE_RES_1NS (TERM9_ERROR_FIRST + 142)
#define TERM9_LIC_EDGE_RES_2NS (TERM9_ERROR_FIRST + 143)
#define TERM9_LIC_EDGE_RES_5NS (TERM9_ERROR_FIRST + 144)
#define TERM9_LIC_EDGE_RES_10NS (TERM9_ERROR_FIRST + 145)
#define TERM9_LIC_FORMAT_NRET (TERM9_ERROR_FIRST + 146)
#define TERM9_LIC_FORMAT_STANDARD (TERM9_ERROR_FIRST + 147)
#define TERM9_LIC_LEVPRG_SYS (TERM9_ERROR_FIRST + 148)
#define TERM9_LIC_LEVSEL_CARD (TERM9_ERROR_FIRST + 149)
#define TERM9_LIC_LEVSEL_GRP (TERM9_ERROR_FIRST + 150)
#define TERM9_LIC_RATE_25MHZ (TERM9_ERROR_FIRST + 151)
#define TERM9_LIC_RATE_50MHZ (TERM9_ERROR_FIRST + 152)
#define TERM9_LIC_TIMPRG_SYS (TERM9_ERROR_FIRST + 153)
#define TERM9_LIC_TIMSEL_CARD (TERM9_ERROR_FIRST + 154)
#define TERM9_LIC_TIMSEL_GRP (TERM9_ERROR_FIRST + 155)
#define TERM9_NOLIC_HVPINS (TERM9_ERROR_FIRST + 156)
#define TERM9_NOLIC_LOAD (TERM9_ERROR_FIRST + 157)
#define TERM9_NOLIC_PROBE (TERM9_ERROR_FIRST + 158)
#define TERM9_NOLIC_SYNCRES (TERM9_ERROR_FIRST + 159)
#define TERM9_NOLIC_USERCLOCK (TERM9_ERROR_FIRST + 160)
#define TERM9_WARN_HOT_SWITCH (TERM9_ERROR_FIRST + 161)
#define TERM9_WARN_RESIDUAL_CONN (TERM9_ERROR_FIRST + 162)
#define TERM9_ERROR_PATSETIDX (TERM9_ERROR_FIRST + 163)
#define TERM9_ERROR_SESSION (TERM9_ERROR_FIRST + 164)
#define TERM9_ERROR_INIT (TERM9_ERROR_FIRST + 165)
#define TERM9_ERROR_MEMORY_ALLOCATION (TERM9_ERROR_FIRST + 166)
#define TERM9_WARN_SOFTWARE_SIM (TERM9_ERROR_FIRST + 167)
#define TERM9_ERROR_GROUPOPCODE (TERM9_ERROR_FIRST + 168)
#define TERM9_ERROR_ENABLE (TERM9_ERROR_FIRST + 169)
#define TERM9_ERROR_TEST_HANDLE (TERM9_ERROR_FIRST + 170)
#define TERM9_ERROR_TEST_PROC (TERM9_ERROR_FIRST + 171)
#define TERM9_ERROR_SYNC_CONNECT (TERM9_ERROR_FIRST + 172)
#define TERM9_ERROR_RESET_INSTR (TERM9_ERROR_FIRST + 173)
#define TERM9_ERROR_ID_QUERY (TERM9_ERROR_FIRST + 174)
#define TERM9_ERROR_TSET_COUNT (TERM9_ERROR_FIRST + 175)
#define TERM9_ERROR_SCOPE_CARD (TERM9_ERROR_FIRST + 176)
#define TERM9_ERROR_NOT_YET (TERM9_ERROR_FIRST + 177)
#define TERM9_ERROR_PROBE_BUTTON_ENABLE (TERM9_ERROR_FIRST + 178)
#define TERM9_ERROR_VIH_TOO_CLOSE (TERM9_ERROR_FIRST + 179)
#define TERM9_ERROR_VIL_TOO_CLOSE (TERM9_ERROR_FIRST + 180)
#define TERM9_ERROR_VOH_TOO_CLOSE (TERM9_ERROR_FIRST + 181)
#define TERM9_ERROR_VOL_TOO_CLOSE (TERM9_ERROR_FIRST + 182)
#define TERM9_ERROR_VCOM_TOO_CLOSE (TERM9_ERROR_FIRST + 183)
#define TERM9_ERROR_ALIGNMENT_OPTION (TERM9_ERROR_FIRST + 184)
#define TERM9_ERROR_ALIGNMENT_OPTION_VALUE (TERM9_ERROR_FIRST + 185)
#define TERM9_ERROR_UNKNOWN_RESOURCE (TERM9_ERROR_FIRST + 186)
#define TERM9_ERROR_BAUD_RATE (TERM9_ERROR_FIRST + 187)
#define TERM9_ERROR_SERIAL_ADDRESS (TERM9_ERROR_FIRST + 188)
#define TERM9_ERROR_SERIAL_BUS_TIMEOUT (TERM9_ERROR_FIRST + 189)
#define TERM9_ERROR_MEASUREMENT (TERM9_ERROR_FIRST + 190)
#define TERM9_ERROR_FCNTR_TIMEOUT (TERM9_ERROR_FIRST + 191)
#define TERM9_ERROR_FCNTR_UNAVAILABLE (TERM9_ERROR_FIRST + 192)
#define TERM9_ERROR_CYCLE_COUNT (TERM9_ERROR_FIRST + 193)
#define TERM9_ERROR_LEVEL_RANGE (TERM9_ERROR_FIRST + 194)
#define TERM9_ERROR_HYBRID_CONNECT (TERM9_ERROR_FIRST + 195)
#define TERM9_ERROR_EXISTING_CONNECTION (TERM9_ERROR_FIRST + 196)
#define TERM9_ERROR_SYNTAX (TERM9_ERROR_FIRST + 197)
#define TERM9_ERROR_LOOPBACK_ENABLE (TERM9_ERROR_FIRST + 198)
#define TERM9_ERROR_UNKNOWN_CARD (TERM9_ERROR_FIRST + 199)
#define TERM9_ERROR_USRCLK_PULSE_WIDTH (TERM9_ERROR_FIRST + 200)
#define TERM9_ERROR_HVPIN_STATE (TERM9_ERROR_FIRST + 201)
#define TERM9_ERROR_NOEDGE (TERM9_ERROR_FIRST + 202)
#define TERM9_ERROR_TSET_PULSE_WIDTH (TERM9_ERROR_FIRST + 203)
#define TERM9_ERROR_TSET_WINDOW_WIDTH (TERM9_ERROR_FIRST + 204)
#define TERM9_ERROR_DLL_LOAD (TERM9_ERROR_FIRST + 205)
#define TERM9_ERROR_DLL_MISSING_FUNCTION (TERM9_ERROR_FIRST + 206)
#define TERM9_ERROR_TIME_VALUE (TERM9_ERROR_FIRST + 207)
#define TERM9_ERROR_PROBE_TIMEOUT (TERM9_ERROR_FIRST + 208)
#define TERM9_ERROR_INTERRUPT (TERM9_ERROR_FIRST + 209)
#define TERM9_ERROR_SLOT (TERM9_ERROR_FIRST + 210)
#define TERM9_ERROR_SELFTEST_TYPE (TERM9_ERROR_FIRST + 211)
#define TERM9_ERROR_UCLKCHANIDX (TERM9_ERROR_FIRST + 212)
#define TERM9_ERROR_INTERNAL (TERM9_ERROR_FIRST + 213)
#define TERM9_ERROR_VIH_LESS_THAN_VIL (TERM9_ERROR_FIRST + 214)
#define TERM9_ERROR_SOURCE (TERM9_ERROR_FIRST + 215)
#define TERM9_ERROR_REFERENCE_SOURCE (TERM9_ERROR_FIRST + 216)
#define TERM9_PARTIAL_SUCCESS (TERM9_ERROR_FIRST + 217)
#define TERM9_ERROR_HW_DELAY_NOT_AVAILABLE (TERM9_ERROR_FIRST + 218)
#define TERM9_ERROR_NO_METER (TERM9_ERROR_FIRST + 219)
#define TERM9_ERROR_INVALID_METER_PARAM (TERM9_ERROR_FIRST + 220)
#define TERM9_ERROR_FILTER_OPTION (TERM9_ERROR_FIRST + 221)
#define TERM9_ERROR_FILTER_OPTION_VALUE (TERM9_ERROR_FIRST + 222)
#define TERM9_ERROR_SPECIFY_PINMAP (TERM9_ERROR_FIRST + 223)
#define TERM9_ERROR_RSRC_LOCKED (TERM9_ERROR_FIRST + 224)
#define TERM9_ERROR_NO_CHANNEL_CARD ( TERM9_ERROR_FIRST + 225)
#define TERM9_ERROR_VCOM_OUT_OF_RANGE ( TERM9_ERROR_FIRST + 226)
#define TERM9_WARN_INVALID_CARD ( TERM9_ERROR_FIRST + 227)
#define TERM9_WARN_RUN_LOW_POWER ( TERM9_ERROR_FIRST + 228)
#define TERM9_ERROR_LAST (TERM9_ERROR_FIRST + 228)
#endif
#ifndef __TERM9_TYPES_LOADED
#define __TERM9_TYPES_LOADED 1
#define TERM9_ATTRVAL_FIRST 2
#define TERM9_ADC_CONN_10TO10 2
#define TERM9_ADC_CONN_12MINUS 3
#define TERM9_ADC_CONN_12PLUS 4
#define TERM9_ADC_CONN_3PLUS 5
#define TERM9_ADC_CONN_20TO20 6
#define TERM9_ADC_CONN_24MINUS 7
#define TERM9_ADC_CONN_24PLUS 8
#define TERM9_ADC_CONN_5TO5 9
#define TERM9_ADC_CONN_AMMETER 10
#define TERM9_ADC_CONN_PWRGND 11
#define TERM9_ADC_CONN_SIGGND 12
#define TERM9_ADC_CONN_VCC 13
#define TERM9_ADC_CONN_VEE 14
#define TERM9_ADC_CONN_VTT 15
#define TERM9_ADC_CONN_UNKNOWN 16
#define TERM9_AMMTR_MODE_FORCE 17
#define TERM9_AMMTR_MODE_MEASURE 18
#define TERM9_AMMTR_MODE_OFF 19
#define TERM9_AMMTR_MODE_SELFTEST 20
#define TERM9_AMMTR_MODE_UNKNOWN 21
#define TERM9_PATTERNSET_LEARN_FAILS 22
#define TERM9_PATTERNSET_LEARN_PATS 23
#define TERM9_PATTERNSET_PRECOUNT_FAILS 24
#define TERM9_PATTERNSET_PRECOUNT_PATS 25
#define TERM9_CAPTURE_CLOSE 26
#define TERM9_CAPTURE_OPEN 27
#define TERM9_CAPTURE_WINDOW 28
#define TERM9_CHANMODE_DYNAMIC 29
#define TERM9_CHANMODE_STATIC 30
#define TERM9_CLUTCH_INPUT_HIGH 31
#define TERM9_CLUTCH_SIGNAL_HIGH 32
#define TERM9_CLUTCH_INPUT_LOW 33
#define TERM9_CLUTCH_SIGNAL_LOW 34
#define TERM9_COND_ALWAYS 35
#define TERM9_COND_TRUE 35
#define TERM9_COND_EX1 36
#define TERM9_COND_EXT1 36
#define TERM9_COND_EX2 37
#define TERM9_COND_EXT2 37
#define TERM9_COND_FAIL 38
#define TERM9_COND_NEVER 39
#define TERM9_COND_FALSE 39
#define TERM9_COND_PASS 40
#define TERM9_COND_SYNC 41
#define TERM9_COND_NONE 42
#define TERM9_COP_CLOOP 43
#define TERM9_COP_CREPEAT 44
#define TERM9_COP_ENDLOOP 45
#define TERM9_COP_HALT 46
#define TERM9_COP_JUMP 47
#define TERM9_COP_LOOP 48
#define TERM9_COP_NOP 49
#define TERM9_COP_REPEAT 50
#define TERM9_DETECTOR_BETWEEN 51
#define TERM9_DETECTOR_HIGH 52
#define TERM9_DETECTOR_LOW 53
#define TERM9_DETECTOR_UNKNOWN 54
#define TERM9_EDGE_FALLING 55
#define TERM9_EDGE_RISING 56
#define TERM9_EXECUTE_COMPARE 57
#define TERM9_EXECUTE_LEARN 58
#define TERM9_FORMAT_COMPS 59
#define TERM9_FORMAT_NRET 60
#define TERM9_FORMAT_RCOMP 61
#define TERM9_FORMAT_ROFF 62
#define TERM9_FORMAT_RONE 63
#define TERM9_FORMAT_RZERO 64
#define TERM9_GROUND_EXTERNAL 65
#define TERM9_GROUND_INTERNAL 66
#define TERM9_IMPED_50OHM 67
#define TERM9_IMPED_LOWZ 68
#define TERM9_LOAD_OFF 69
#define TERM9_LOAD_ON 70
#define TERM9_LOGIC_ECL 71
#define TERM9_LOGIC_TTL 72
#define TERM9_LOOP_OK 73
#define TERM9_LOOP_OVERFLOW 74
#define TERM9_LOOP_UNDERFLOW 75
#define TERM9_CLOCKMODE_OFF 76
#define TERM9_HVPINMODE_OFF 77
#define TERM9_UCLKMODE_OFF 78
#define TERM9_CLOCKMODE_ON 79
#define TERM9_HVPINMODE_ON 80
#define TERM9_UCLKMODE_ON 81
#define TERM9_MTR_INPUT_NONE 82
#define TERM9_MTR_INPUT_PANEL 83
#define TERM9_MTR_INPUT_STMTX 84
#define TERM9_MTR_INPUT_UNKNOWN 85
#define TERM9_MTR_IRNG_100MA 86
#define TERM9_MTR_IRNG_50MA 86
#define TERM9_MTR_IRNG_10MA 87
#define TERM9_MTR_IRNG_UNKNOWN 88
#define TERM9_MTR_MODE_FORCE 89
#define TERM9_MTR_MODE_MEASURE 90
#define TERM9_MTR_MODE_OFF 91
#define TERM9_MTR_MODE_SELFTEST 92
#define TERM9_MTR_MODE_UNKNOWN 93
#define TERM9_MTR_VRNG_10TO10 94
#define TERM9_MTR_VRNG_20TO20 95
#define TERM9_MTR_VRNG_5TO5 96
#define TERM9_MTR_VRNG_OFF 97
#define TERM9_OP_SIG 98
#define TERM9_OP_CRC 98
#define TERM9_OP_IH 99
#define TERM9_OP_IHOL 100
#define TERM9_OP_IL 101
#define TERM9_OP_ILOH 102
#define TERM9_OP_IOX 103
#define TERM9_OP_IX 104
#define TERM9_OP_KEEP 105
#define TERM9_OP_MH 106
#define TERM9_OP_ML 107
#define TERM9_OP_MX 108
#define TERM9_OP_OB 109
#define TERM9_OP_OH 110
#define TERM9_OP_OK 111
#define TERM9_OP_OL 112
#define TERM9_OP_OX 113
#define TERM9_OP_TOG 114
#define TERM9_OP_GROUP_UNKNOWN 115
#define TERM9_OP_UNKNOWN 116
#define TERM9_PATH_DISABLE 117
#define TERM9_PATH_ENABLE 118
#define TERM9_PRB_B 119
#define TERM9_PRB_EH 120
#define TERM9_PRB_EL 121
#define TERM9_PRB_GB 122
#define TERM9_PRB_GH 123
#define TERM9_PRB_GL 124
#define TERM9_PRB_H 125
#define TERM9_PRB_ILLEGAL 126
#define TERM9_PRB_L 127
#define TERM9_PRB_MB 128
#define TERM9_PRB_METHOD_HANDLE 129
#define TERM9_PROBE_METHOD_HANDLE 129
#define TERM9_PRB_METHOD_MATRIX 130
#define TERM9_PROBE_METHOD_MATRIX 130
#define TERM9_PRB_METHOD_UNKNOWN 131
#define TERM9_PROBE_METHOD_UNKNOWN 131
#define TERM9_PRB_MH 132
#define TERM9_PRB_ML 133
#define TERM9_PRB_PB 134
#define TERM9_PRB_PH 135
#define TERM9_PRB_PL 136
#define TERM9_PRB_RB 137
#define TERM9_PRB_RH 138
#define TERM9_PRB_RL 139
#define TERM9_PRB_X 140
#define TERM9_PROBE_CAPTURE_STROBE 141
#define TERM9_PROBE_CAPTURE_WINDOW 142
#define TERM9_PROBE_CONFIG_LOAD 143
#define TERM9_PROBE_CONFIG_RUN 144
#define TERM9_PROBEMODE_DYNAMIC 145
#define TERM9_PROBEMODE_STATIC 146
#define TERM9_PS_CRC 147
#define TERM9_PS_SIG 147
#define TERM9_PS_IH 148
#define TERM9_PS_IHOL 149
#define TERM9_PS_IL 150
#define TERM9_PS_ILOH 151
#define TERM9_PS_IOX 152
#define TERM9_PS_MH 153
#define TERM9_PS_ML 154
#define TERM9_PS_OB 155
#define TERM9_PS_OH 156
#define TERM9_PS_OK 157
#define TERM9_PS_OL 158
#define TERM9_PS_UNKNOWN 159
#define TERM9_RELAY_CLOSED 160
#define TERM9_RELAY_OPEN 161
#define TERM9_RESULT_FAIL 162
#define TERM9_RESULT_MBT 163
#define TERM9_RESULT_NOT_RUN 164
#define TERM9_RESULT_OVERFLOW 165
#define TERM9_RESULT_PASS 166
#define TERM9_RESULT_UNDERFLOW 167  //finkb Added TERM9_RESULT_STOPPED at 190
#define TERM9_RUN_IDLE 168
#define TERM9_RUN_RUNNING 169
#define TERM9_RUN_STOPPING 170
#define TERM9_SLEWRATE_HIGH 171
#define TERM9_SLEWRATE_LOW 172
#define TERM9_SLEWRATE_MEDIUM 173
#define TERM9_SOURCE_EXTERNAL 174
#define TERM9_SOURCE_INTERNAL 175
#define TERM9_STATE_HIGH 176
#define TERM9_STATE_LOW 177
#define TERM9_STMTX_GLOBAL 178
#define TERM9_STMTX_LOCAL 179
#define TERM9_SYNC_IDLE 180
#define TERM9_SYNC_LISTEN 181
#define TERM9_SYNC_TRIGGER 182
#define TERM9_TIMING_HALF 183
#define TERM9_TIMING_SLOW 184
#define TERM9_TIMING_STANDARD 185
#define TERM9_TRIGGER_PATCLK 186
#define TERM9_TRIGGER_SYSCLK 187
#define TERM9_UCLK_TRIGGER_EXTCLK 188
#define TERM9_UCLK_TRIGGER_SYSCLK 189
#define TERM9_RESULT_STOPPED 190  //added by finkb 9/29/2004
#define TERM9_EDGE_BOTH 206
#define TERM9_PATTERNSET_LEARN_UNKNOWN 207
#define TERM9_PATTERNSET_PRECOUNT_UNKNOWN 208
#define TERM9_FORMAT_UNKNOWN 209
#define TERM9_COND_UNKNOWN 210
#define TERM9_COP_UNKNOWN 211
#define TERM9_SLEWRATE_UNKNOWN 212
#define TERM9_SYNC_UNKNOWN 213
#define TERM9_ADC_CONN_PRBHNDL 214
#define TERM9_ADC_CONN_REFV 215
#define TERM9_INT_PATTERN_SET_DONE 216
#define TERM9_INT_STATIC_PATTERN_COMPLETE 217
#define TERM9_INT_PROBE_BUTTON 218
#define TERM9_INT_MAX_PATTERN_SET_TIMEOUT 219
#define TERM9_INT_OVER_TEMPERATURE 220
#define TERM9_INT_REF_OVERLOAD 221
#define TERM9_INT_SYSTEM_GROUND 222
#define TERM9_INT_VXI_ACFAIL 223
#define TERM9_INT_VXI_SYSRESET 224
#define TERM9_INT_VXI_SYSFAIL 225
#define TERM9_INT_VXI_BUSERROR 226
#define TERM9_INT_VXI_MAPERROR 227
#define TERM9_INT_STATUS_BUS 228
#define TERM9_ALIGN_OPTION_CHANNEL_BOUNDARY 229
#define TERM9_ALIGN_OPTION_CHANNEL_SELECT 230
#define TERM9_ALIGN_OPTION_PROBE_BOUNDARY 231
#define TERM9_ALIGN_OPTION_PROBE_CHANNEL_SELECT 232
#define TERM9_ALIGN_OPTION_SYNC_BOUNDARY 233
#define TERM9_ALIGN_BOUNDARY_UUT 234
#define TERM9_ALIGN_BOUNDARY_FRONT_PANEL 235
#define TERM9_ALIGN_CHANNEL_SELECT_DYNAMIC 236
#define TERM9_ALIGN_CHANNEL_SELECT_ALL 237
#define TERM9_BAUD_300 238
#define TERM9_BAUD_1200 239
#define TERM9_BAUD_1800 240
#define TERM9_BAUD_2400 241
#define TERM9_BAUD_3600 242
#define TERM9_BAUD_4800 243
#define TERM9_BAUD_5600 244
#define TERM9_BAUD_9600 245
#define TERM9_BAUD_18200 246
#define TERM9_BAUD_36400 247
#define TERM9_LEVEL_RANGE_5TO15 248
#define TERM9_LEVEL_RANGE_2TO5 249
#define TERM9_LEVEL_RANGE_10TO10 250  // added TERM9_LEVEL_RANGE_30TO30 at the end
#define TERM9_UNDEFINED 251
#define TERM9_ALIGN_OPTION_MINIMUMCLOCKPERIOD 252
#define TERM9_BAUD_1000000 253
#define TERM9_SELFTEST_CONFIDENCE 254
#define TERM9_SELFTEST_FUNCTIONAL 255
#define TERM9_SELFTEST_DIAGNOSTIC 256
#define TERM9_LEVEL_RANGE_30TO30 257
#define TERM9_INT_RAIL_POWER_FAULT 258
#define TERM9_ALIGN_OPTION_CHANNEL_VOLTAGE 259
#define TERM9_ALIGN_VOLTAGE 260
#define TERM9_DONT_ALIGN_VOLTAGE 261
#define TERM9_METERPATH_MAIN 262
#define TERM9_METERPATH_CALIBRATION 263
#define TERM9_METERPATH_DIRECT 264
#define TERM9_METERCONNECT_CHANNEL 265
#define TERM9_METERCONNECT_CT962 266
#define TERM9_METERCONNECT_10VREF 267
#define TERM9_METERCONNECT_GROUNDREF 268
#define TERM9_METERCONNECT_EXTREF 269
#define TERM9_METERCONNECT_EXTREF_DAC 270 //remove extref 10V and Gnd
#define TERM9_METERCONNECT_MAXTEST_POS 271
#define TERM9_METERCONNECT_MAXTEST_NEG 272
#define TERM9_METERCONNECT_RAIL_POS 273
#define TERM9_METERCONNECT_RAIL_NEG 274
#define TERM9_METERCONNECT_CAL_VER 275
#define TERM9_METER_PARAM_VIH 276
#define TERM9_METER_PARAM_VIL 277
#define TERM9_METER_PARAM_VOH 278
#define TERM9_METER_PARAM_VOL 279
#define TERM9_METER_PARAM_NONE 280
#define TERM9_METER_PARAM_CT962_3V 281
#define TERM9_METER_PARAM_CT962_6V 282
#define TERM9_METER_PARAM_CT962_SWITCH_POS 283
#define TERM9_METER_PARAM_CT962_SWITCH_NEG 284
#define TERM9_METER_PARAM_CT962_OPAMP_NEG 285
#define TERM9_METER_PARAM_CT962_OPAMP_POS 286
#define TERM9_METER_PARAM_CT962_GROUND 287
#define TERM9_METER_PARAM_CT962_SPARE 288
#define TERM9_FILTER_OUT_OPTION_CHANNEL 289
#define TERM9_FILTER_OUT_OPTION_LEVEL 290
#define TERM9_FILTER_OUT_OPTION_TSET 291
#define TERM9_FILTER_OUT_OPTION_PATERN_SET 292
#define TERM9_FILTER_OUT_OPTION_CHANNEL_NONE 293
#define TERM9_FILTER_OUT_OPTION_CHANNEL_USE_PINMAP 294
#define TERM9_FILTER_OUT_OPTION_CHANNEL_UNCONNECTED 295
#define TERM9_FILTER_OUT_OPTION_CHANNEL_DYNAMIC 296
#define TERM9_FILTER_OUT_OPTION_CHANNEL_STATIC 297
#define TERM9_FILTER_OUT_OPTION_LEVEL_NONE 298
#define TERM9_FILTER_OUT_OPTION_LEVEL_UNUSED 299
#define TERM9_FILTER_OUT_OPTION_TSET_NONE 300
#define TERM9_FILTER_OUT_OPTION_TSET_UNUSED 301
#define TERM9_FILTER_OUT_OPTION_PATTERN_SET_NONE 302
#define TERM9_FILTER_OUT_OPTION_PATTERN_SET_ALL 303
#define TERM9_FILTER_OUT_OPTION_PATTERN_SET_STATIC 304
#define TERM9_FILTER_OUT_OPTION_PATTERN_SET_DYNAMIC 305
#define TERM9_ALIGN_OPTION_FORCE_CHANNEL 306
#define TERM9_LEVEL_RANGE_AUTO 307
#define TERM9_FILTER_OUT_OPTION_STATIC_FAILURE_NONE 308
#define TERM9_FILTER_OUT_OPTION_STATIC_FAILURE_ALL 309
#define TERM9_FILTER_OUT_OPTION_DYNAMIC_FAILURE_ALL 310
#define TERM9_FILTER_OUT_OPTION_DYNAMIC_FAILURE_AFTER_HALT 311
#define TERM9_ATTRVAL_LAST 311

#endif

#ifndef __TERM9_DEFS_LOADED
#define __TERM9_DEFS_LOADED 1
#define TERM9_CHECK_STATUS()   if ( status != VI_SUCCESS ) return status
#ifdef INSTR_LANGUAGE_SPECIFIC
typedef ViInt32 terM9_AttrVal;
typedef terM9_AttrVal * terM9_PAttrVal;
typedef ViInt32 terM9_Result;
typedef terM9_Result * terM9_PResult;
typedef ViInt32 terM9_PatternSetResult;
typedef terM9_PatternSetResult * terM9_PPatternSetResult;
typedef ViInt32 terM9_ScopeIdx;
typedef terM9_ScopeIdx * terM9_PScopeIdx;
typedef ViInt32 terM9_Interrupt;
typedef terM9_Interrupt * terM9_PInterrupt;
typedef struct _RESOURCEMAPPING
{
  terM9_ScopeIdx scopeIdx;
  ViInt32 resourceIdx;
} terM9_ResourceMapping;
typedef struct _TSETINFO
{
  ViInt32 TSETIdx;
  ViReal64 clockPeriod;
  ViReal64 *phaseEdges;
  ViReal64 *windowEdges;
} terM9_TSETInfo;
#endif

#ifdef INSTR_CALLBACKS
typedef void ( _VI_FUNCH _VI_PTR terM9_InterruptHandler )
        (ViInt32 interrupt, ViChar _VI_FAR addlInfo[]);
typedef terM9_InterruptHandler * terM9_PInterruptHandler;
#endif
#endif
#ifndef __TERM9_SCOPE_LOADED
#define __TERM9_SCOPE_LOADED 1
#define TERM9_SCOPE_LAST 65535
#define TERM9_SCOPE_FIRST_USER 0
#define TERM9_SCOPE_LAST_USER 32767
#define TERM9_SCOPE_FIRST_PREDEFINED 32768
#define TERM9_SCOPE_LAST_PREDEFINED 65535
#define TERM9_SCOPE_FIRST_UC_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 300)
#define TERM9_SCOPE_LAST_UC_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 399)
#define TERM9_SCOPE_FIRST_SR_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 400)
#define TERM9_SCOPE_LAST_SR_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 499)
#define TERM9_SCOPE_FIRST_CARD_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 100)
#define TERM9_SCOPE_LAST_CARD_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 299)
#define TERM9_SCOPE_FIRST_CHNGRP_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 1000)
#define TERM9_SCOPE_LAST_CHNGRP_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 1199)
#define TERM9_SCOPE_FIRST_PIN_INDEX (TERM9_SCOPE_FIRST_PREDEFINED + 1200)
#define TERM9_SCOPE_LAST_PIN_INDEX (TERM9_SCOPE_LAST_PREDEFINED)
#define TERM9_SCOPE_UNDEFINED -1
#define TERM9_SCOPE_ISGROUP -2
#define TERM9_SCOPE_SYSTEM (TERM9_SCOPE_FIRST_PREDEFINED)
#define TERM9_SCOPE_CARD(n) (TERM9_SCOPE_FIRST_CARD_INDEX + (n))
#define TERM9_SCOPE_CHANSYSTEM (TERM9_SCOPE_FIRST_PREDEFINED + 1)
#define TERM9_SCOPE_CHANCARD(n) (TERM9_SCOPE_FIRST_CHNGRP_INDEX + (n) - 1)
#define TERM9_SCOPE_CHAN(n) (TERM9_SCOPE_FIRST_PIN_INDEX + (n))

#endif

#ifdef INSTR_CALLBACKS
ViStatus _VI_FUNC terM9_getInterruptHandler (ViSession vi, terM9_PInterruptHandler
                              interruptHandler);
ViStatus _VI_FUNC terM9_setInterruptHandler (ViSession vi, terM9_InterruptHandler
                              interruptHandler);
#endif

#ifdef INSTR_LANGUAGE_SPECIFIC
ViStatus _VI_FUNC terM9_unlock (ViSession vi);
ViStatus _VI_FUNC terM9_lock (ViSession vi,
                              ViAccessMode lockType,
                              ViUInt32 timeout,
                              ViKeyId requestedKey,
                              ViChar accesskey[]);
ViStatus _VI_FUNC terM9_getProbeButtonEnable (ViSession vi, ViPBoolean enable);
ViStatus _VI_FUNC terM9_getTimingSetInfo (ViSession vi, ViInt32 phaseCount,
                              terM9_ResourceMapping phaseMapping[], ViInt32
                              windowCount, terM9_ResourceMapping windowMapping[],
                              ViInt32 startingTSETIndex, ViInt32 TSETCount,
                              terM9_TSETInfo TSETInfo[]);
ViStatus _VI_FUNC terM9_setProbeButtonEnable (ViSession vi, ViBoolean enable);
ViStatus _VI_FUNC terM9_setTimingSetInfo (ViSession vi, ViInt32 phaseCount,
                              terM9_ResourceMapping phaseMapping[], ViInt32
                              windowCount, terM9_ResourceMapping windowMapping[],
                              ViInt32 TSETCount, terM9_TSETInfo TSETInfo[]);
#endif

ViStatus _VI_FUNC terM9_calibrateMeter (ViSession vi, ViPReal64 result);
ViStatus _VI_FUNC terM9_calibrateT0Clock (ViSession vi);
ViStatus _VI_FUNC terM9_calibrateTDR (ViSession vi, ViPReal64 result);
ViStatus _VI_FUNC terM9_close (ViSession vi);
ViStatus _VI_FUNC terM9_closeDigitalTest (ViSession vi, ViInt32 testHandle);
ViStatus _VI_FUNC terM9_configureDigitalTestPatternSet (ViSession vi, ViInt32
                              testHandle, ViInt32 patternSetIndex);
ViStatus _VI_FUNC terM9_configureDigitalTestSetup (ViSession vi, ViInt32
                              testHandle);
ViStatus _VI_FUNC terM9_error_message (ViSession vi, ViStatus statusCode, ViChar
                              _VI_FAR message[]);
ViStatus _VI_FUNC terM9_error_query (ViSession vi, ViPInt32 errorCode, ViChar
                              _VI_FAR errorMessage[]);
ViStatus _VI_FUNC terM9_errorMessage (ViSession vi, ViStatus statusCode, ViChar
                              _VI_FAR message[]);
ViStatus _VI_FUNC terM9_errorQuery (ViSession vi, ViPInt32 errorCode, ViChar
                              _VI_FAR errorMessage[]);
ViStatus _VI_FUNC terM9_executeChannelAlignment (ViSession vi, ViPInt32
                              alignmentResult);
ViStatus _VI_FUNC terM9_executeDiagnosticSelfTest (ViSession vi, ViPInt32
                              testResult, ViChar _VI_FAR testMessage[]);
ViStatus _VI_FUNC terM9_executeDigitalTest (ViSession vi, ViString testFile,
                              ViBoolean expandLoops, ViPInt32 testResult);
ViStatus _VI_FUNC terM9_executeFullSelfTest (ViSession vi, ViPInt32 testResult,
                              ViChar _VI_FAR testMessage[]);
ViStatus _VI_FUNC terM9_executeFunctionalSelfTest (ViSession vi, ViPInt32
                              testResult, ViChar _VI_FAR testMessage[]);
ViStatus _VI_FUNC terM9_executeProbeAlignment (ViSession vi, ViPInt32
                              alignmentResult);
ViStatus _VI_FUNC terM9_executeSelfTest (ViSession vi, ViInt32 selfTestType,
                              ViInt32 slot, ViPInt32 testResult, ViChar _VI_FAR
                              testMessage[]);
ViStatus _VI_FUNC terM9_executeSyncResourceAlignment (ViSession vi, ViPInt32
                              alignmentResult);
ViStatus _VI_FUNC terM9_executeSystemAlignment (ViSession vi, ViPInt32
                              alignmentResult);
ViStatus _VI_FUNC terM9_executeUserClockAlignment (ViSession vi, ViPInt32
                              alignmentResult);
ViStatus _VI_FUNC terM9_fetchBackplaneSyncResourceState (ViSession vi, ViInt32
                              syncListCount, ViInt32 _VI_FAR syncList[], ViInt32
                              initialPatternIndex, ViInt32 patternCount, ViInt32
                              _VI_FAR syncStateBuffer[]);
ViStatus _VI_FUNC terM9_fetchChannelAlignmentResult (ViSession vi, ViPInt32
                              alignmentResult, ViChar _VI_FAR
                              alignmentMessage[]);
ViStatus _VI_FUNC terM9_fetchChannelSignature (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 signature);
ViStatus _VI_FUNC terM9_fetchChannelState (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 detectorState, ViPInt32 deskewRelay,
                              ViPBoolean overTemperature, ViPBoolean
                              referenceOverload);
ViStatus _VI_FUNC terM9_fetchDynamicChannelPinOpcode (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViInt32
                              initialPatternIndex, ViInt32 patternCount, ViInt32
                              _VI_FAR pinOpcodeBuffer[]);
ViStatus _VI_FUNC terM9_fetchDynamicFailures (ViSession vi, ViInt32 resultIndex,
                              ViInt32 pinListMaxCount, ViPInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 _VI_FAR
                              pinOpcodeList[]);
ViStatus _VI_FUNC terM9_fetchDynamicPatternModifiers (ViSession vi, ViInt32
                              initialPatternIndex, ViInt32 patternCount, ViInt32
                              _VI_FAR clocksPerPattern[], ViInt32 _VI_FAR
                              TSETIndex[], ViInt32 _VI_FAR controlOpcode[],
                              ViInt32 _VI_FAR controlCount[], ViInt32 _VI_FAR
                              controlCond[], ViBoolean _VI_FAR test[], ViBoolean
                              _VI_FAR failCapture[], ViBoolean _VI_FAR
                              resultCapture[], ViInt32 _VI_FAR PSETIndex[],
                              ViBoolean _VI_FAR step[], ViBoolean _VI_FAR
                              probeCapture[]);
ViStatus _VI_FUNC terM9_fetchDynamicPatternResults (ViSession vi, ViInt32
                              initialResultIndex, ViInt32 resultCount, ViPInt32
                              resultsCount, ViInt32 _VI_FAR results[], ViInt32
                              _VI_FAR patternIndexes[]);
ViStatus _VI_FUNC terM9_fetchDynamicPatternSetResult (ViSession vi, ViPInt32
                              patternSetResult, ViPInt32 resultCount, ViPInt32
                              lastResultIndex);
ViStatus _VI_FUNC terM9_fetchDynamicPatternSetState (ViSession vi, ViPInt32
                              loopState, ViPInt32 runState, ViPBoolean
                              learnedPastLimit, ViPBoolean lastPatternValid,
                              ViPBoolean stoppedOnFail, ViPBoolean
                              stoppedOnEvent, ViPBoolean stoppedOnProbe,
                              ViPBoolean stoppedOnLSEQHalt, ViPBoolean
                              stoppedOnPattern, ViPInt32 stoppedOnPatternIndex);
ViStatus _VI_FUNC terM9_fetchDynamicPinResults (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViInt32
                              initialResultIndex, ViInt32 resultCount, ViPInt32
                              resultListCount, ViInt32 _VI_FAR resultList[]);
ViStatus _VI_FUNC terM9_fetchDynamicProbeFailingDatatyp (ViSession vi, ViPInt32
                              failingDataType);
ViStatus _VI_FUNC terM9_fetchDynamicProbeFailingDatatype (ViSession vi, ViPInt32
                              failingDataType);
ViStatus _VI_FUNC terM9_fetchDynamicProbeFailingStep (ViSession vi, ViPInt32
                              failingStep);
ViStatus _VI_FUNC terM9_fetchDynamicProbePatternCount (ViSession vi, ViPInt32
                              patternCount);
ViStatus _VI_FUNC terM9_fetchDynamicProbeResult (ViSession vi, ViPInt32 result);
ViStatus _VI_FUNC terM9_fetchDynamicProbeStepCount (ViSession vi, ViPInt32
                              stepCount);
ViStatus _VI_FUNC terM9_fetchFailingDynamicPatternResul (ViSession vi, ViInt32
                              initialResultIndex, ViInt32 resultCount, ViPInt32
                              resultIndexesCount, ViInt32 _VI_FAR
                              resultIndexes[], ViInt32 _VI_FAR
                              patternIndexes[]);
ViStatus _VI_FUNC terM9_fetchFailingDynamicPatternResults (ViSession vi, ViInt32
                              initialResultIndex, ViInt32 resultCount, ViPInt32
                              resultIndexesCount, ViInt32 _VI_FAR
                              resultIndexes[], ViInt32 _VI_FAR
                              patternIndexes[]);
ViStatus _VI_FUNC terM9_fetchFailingDynamicPinResults (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViInt32
                              initialResultIndex, ViInt32 resultCount, ViPInt32
                              failingPinListCount, ViInt32 _VI_FAR
                              failingPinList[]);
ViStatus _VI_FUNC terM9_fetchFailingStaticPinResults (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViPInt32
                              failingPinListCount, ViInt32 _VI_FAR
                              failingPinList[]);
ViStatus _VI_FUNC terM9_fetchFrontPanelSyncResourceStat (ViSession vi, ViInt32
                              syncListCount, ViInt32 _VI_FAR syncList[], ViInt32
                              initialPatternIndex, ViInt32 patternCount, ViInt32
                              _VI_FAR syncStateBuffer[]);
ViStatus _VI_FUNC terM9_fetchFrontPanelSyncResourceState (ViSession vi, ViInt32
                              syncListCount, ViInt32 _VI_FAR syncList[], ViInt32
                              initialPatternIndex, ViInt32 patternCount, ViInt32
                              _VI_FAR syncStateBuffer[]);
ViStatus _VI_FUNC terM9_fetchHighVoltagePinGroupState (ViSession vi, ViInt32
                              stateMaxCount, ViPInt32 stateCount, ViUInt32
                              _VI_FAR state[]);
ViStatus _VI_FUNC terM9_fetchHighVoltagePinState (ViSession vi, ViInt32
                              HVPinIndex, ViPInt32 state);
ViStatus _VI_FUNC terM9_fetchHighVoltagePinsDetectState (ViSession vi, ViInt32
                              stateMaxCount, ViPInt32 stateCount, ViUInt32
                              _VI_FAR detectState[]);
ViStatus _VI_FUNC terM9_fetchInterruptState (ViSession vi, ViInt32 interrupt,
                              ViPBoolean state);
ViStatus _VI_FUNC terM9_fetchPassingDynamicPatternResul (ViSession vi, ViInt32
                              initialResultIndex, ViInt32 resultCount, ViPInt32
                              resultIndexesCount, ViInt32 _VI_FAR
                              resultIndexes[], ViInt32 _VI_FAR
                              patternIndexes[]);
ViStatus _VI_FUNC terM9_fetchPassingDynamicPatternResults (ViSession vi, ViInt32
                              initialResultIndex, ViInt32 resultCount, ViPInt32
                              resultIndexesCount, ViInt32 _VI_FAR
                              resultIndexes[], ViInt32 _VI_FAR
                              patternIndexes[]);
ViStatus _VI_FUNC terM9_fetchPassingDynamicPinResults (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViInt32
                              initialResultIndex, ViInt32 resultCount, ViPInt32
                              passingPinListCount, ViInt32 _VI_FAR
                              passingPinList[]);
ViStatus _VI_FUNC terM9_fetchPassingStaticPinResults (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViPInt32
                              passingPinListCount, ViInt32 _VI_FAR
                              passingPinList[]);
ViStatus _VI_FUNC terM9_fetchPatternClutchState (ViSession vi, ViPInt32 state);
ViStatus _VI_FUNC terM9_fetchProbeAlignmentResult (ViSession vi, ViPInt32
                              alignmentResult, ViChar _VI_FAR
                              alignmentMessage[]);
ViStatus _VI_FUNC terM9_fetchProbeDetectData (ViSession vi, ViInt32
                              initialStepIndex, ViInt32 stepCount, ViInt32
                              _VI_FAR detectData[]);
ViStatus _VI_FUNC terM9_fetchProbeMapData (ViSession vi, ViInt32
                              mapDataMaxCount, ViPInt32 mapDataCount, ViInt32
                              _VI_FAR mapData[]);
ViStatus _VI_FUNC terM9_fetchProbeSettings (ViSession vi, ViPInt32 probeMode,
                              ViPInt32 probeConfig, ViPInt32 staticCapture,
                              ViPInt32 executeMode);
ViStatus _VI_FUNC terM9_fetchStaticChannelPinState (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViInt32
                              _VI_FAR pinStateBuffer[]);
ViStatus _VI_FUNC terM9_fetchStaticFailures (ViSession vi, ViInt32
                              pinListMaxCount, ViPInt32 pinListCount, ViInt32
                              _VI_FAR pinList[], ViInt32 _VI_FAR
                              pinStateList[]);
ViStatus _VI_FUNC terM9_fetchStaticPatternResult (ViSession vi, ViPInt32
                              result);
ViStatus _VI_FUNC terM9_fetchStaticPinResults (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViPInt32
                              resultListCount, ViInt32 _VI_FAR resultList[]);
ViStatus _VI_FUNC terM9_fetchStaticProbeFailingDatatype (ViSession vi, ViPInt32
                              failingDataType);
ViStatus _VI_FUNC terM9_fetchStaticProbeFailingStep (ViSession vi, ViPInt32
                              failingStep);
ViStatus _VI_FUNC terM9_fetchStaticProbePatternCount (ViSession vi, ViPInt32
                              patternCount);
ViStatus _VI_FUNC terM9_fetchStaticProbeResult (ViSession vi, ViPInt32 result);
ViStatus _VI_FUNC terM9_fetchStaticProbeStepCount (ViSession vi, ViPInt32
                              stepCount);
ViStatus _VI_FUNC terM9_fetchSyncResourceAlignmentResul (ViSession vi, ViPInt32
                              alignmentResult, ViChar _VI_FAR
                              alignmentMessage[]);
ViStatus _VI_FUNC terM9_fetchSyncResourceAlignmentResult (ViSession vi, ViPInt32
                              alignmentResult, ViChar _VI_FAR
                              alignmentMessage[]);
ViStatus _VI_FUNC terM9_fetchSystemAlignmentResult (ViSession vi, ViPInt32
                              alignmentResult, ViChar _VI_FAR
                              alignmentMessage[]);
ViStatus _VI_FUNC terM9_fetchSystemClutchState (ViSession vi, ViPInt32 state);
ViStatus _VI_FUNC terM9_fetchUserClockAlignmentResult (ViSession vi, ViPInt32
                              alignmentResult, ViChar _VI_FAR
                              alignmentMessage[]);
ViStatus _VI_FUNC terM9_generateTestFromHardware (ViSession vi, ViString testFile);
ViStatus _VI_FUNC terM9_getAlignmentOption (ViSession vi, ViInt32
                              alignmentOption, ViPInt32 alignmentOptionValue);
ViStatus _VI_FUNC terM9_getAlignmentReal64Option (ViSession vi, ViInt32
                              alignmentOption, ViPReal64 alignmentOptionValue);
ViStatus _VI_FUNC terM9_getAperture (ViSession vi, ViPReal64 aperture);
ViStatus _VI_FUNC terM9_getBackplaneSyncState (ViSession vi, ViInt32 syncIndex,
                              ViPInt32 state);
ViStatus _VI_FUNC terM9_getBoardInformation (ViSession vi, ViInt32 cardIndex,
                              ViInt32 boardIndex, ViPInt32 boardId, ViChar
                              _VI_FAR boardNum[], ViChar _VI_FAR boardName[],
                              ViChar _VI_FAR boardRev[], ViChar _VI_FAR
                              boardSerial[], ViChar _VI_FAR addlInfo[]);
ViStatus _VI_FUNC terM9_getCardInformation (ViSession vi, ViInt32 cardIndex,
                              ViPInt32 cardId, ViChar _VI_FAR cardName[],
                              ViPInt32 boardCount, ViPInt32 chanCount, ViPInt32
                              chassis, ViPInt32 slot, ViChar _VI_FAR
                              addlInfo[]);
ViStatus _VI_FUNC terM9_getChannel (ViSession vi, ViInt32 scopeIndex, ViPInt32
                              capture, ViPInt32 chanMode, ViPInt32 connect,
                              ViPInt32 format, ViPInt32 impedance, ViPInt32
                              levelSetIndex, ViPInt32 load, ViPInt32 phaseIndex,
                              ViPInt32 windowIndex);
ViStatus _VI_FUNC terM9_getChannelCapture (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 capture);
ViStatus _VI_FUNC terM9_getChannelChanMode (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 chanMode);
ViStatus _VI_FUNC terM9_getChannelConnect (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 connect);
ViStatus _VI_FUNC terM9_getChannelFormat (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 format);
ViStatus _VI_FUNC terM9_getChannelHybridConnect (ViSession vi, ViInt32
                              scopeIndex, ViPInt32 connect);
ViStatus _VI_FUNC terM9_getChannelImpedance (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 impedance);
ViStatus _VI_FUNC terM9_getChannelInformation (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 cardIndex, ViPInt32 channelNumber,
                              ViPInt32 channelIndex);
ViStatus _VI_FUNC terM9_getChannelLevel (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 levelSetIndex);
ViStatus _VI_FUNC terM9_getChannelLoad (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 load);
ViStatus _VI_FUNC terM9_getChannelPhase (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 phaseIndex);
ViStatus _VI_FUNC terM9_getChannelPinOpcode (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 opcode);
ViStatus _VI_FUNC terM9_getChannelWindow (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 windowIndex);
ViStatus _VI_FUNC terM9_getCurrentRange (ViSession vi, ViPInt32 currentRange);
ViStatus _VI_FUNC terM9_getDMTXToPinConnect (ViSession vi, ViPInt32 scopeIndex);
ViStatus _VI_FUNC terM9_getDynamicPattern (ViSession vi, ViPInt32
                              clocksPerPattern, ViPInt32 TSETIndex, ViPInt32
                              controlOpcode, ViPInt32 controlCount, ViPInt32
                              controlCond, ViPBoolean failCapture, ViPBoolean
                              resultCapture, ViPInt32 PSETIndex, ViPBoolean
                              probeCapture);
ViStatus _VI_FUNC terM9_getDynamicPatternClocksPerPatte (ViSession vi, ViPInt32
                              clocksPerPattern);
ViStatus _VI_FUNC terM9_getDynamicPatternClocksPerPattern (ViSession vi,
                              ViPInt32 clocksPerPattern);
ViStatus _VI_FUNC terM9_getDynamicPatternControl (ViSession vi, ViPInt32
                              controlOpcode, ViPInt32 controlCount, ViPInt32
                              controlCond);
ViStatus _VI_FUNC terM9_getDynamicPatternCount (ViSession vi, ViPInt32
                              patternCount);
ViStatus _VI_FUNC terM9_getDynamicPatternFailCapture (ViSession vi, ViPBoolean
                              failCapture);
ViStatus _VI_FUNC terM9_getDynamicPatternIndex (ViSession vi, ViPInt32
                              patternIndex);
ViStatus _VI_FUNC terM9_getDynamicPatternProbeCapture (ViSession vi, ViPBoolean
                              probeCapture);
ViStatus _VI_FUNC terM9_getDynamicPatternProbeTimingSet (ViSession vi, ViPInt32
                              PSETIndex);
ViStatus _VI_FUNC terM9_getDynamicPatternProbeTimingSetIndex (ViSession vi,
                              ViPInt32 PSETIndex);
ViStatus _VI_FUNC terM9_getDynamicPatternResultCapture (ViSession vi, ViPBoolean
                              resultCapture);
ViStatus _VI_FUNC terM9_getDynamicPatternSetTimeout (ViSession vi, ViPReal64
                              timeout);
ViStatus _VI_FUNC terM9_getDynamicPatternTimingSetIndex (ViSession vi, ViPInt32
                              TSETIndex);
ViStatus _VI_FUNC terM9_getEXT1InLogicSelect (ViSession vi, ViPInt32
                              logicSelect);  
ViStatus _VI_FUNC terM9_getEXT2InLogicSelect (ViSession vi, ViPInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_getExtClockSrcLogicSelect (ViSession vi, ViPInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_getFrequencyCounterCycleCount (ViSession vi, ViPInt32
                              cycleCount);
ViStatus _VI_FUNC terM9_getFrontPanelSyncConnect (ViSession vi, ViInt32
                              syncIndex, ViPInt32 connect);
ViStatus _VI_FUNC terM9_getFrontPanelSyncLogicSelect (ViSession vi, ViInt32
                              syncIndex, ViPInt32 logicSelect);
ViStatus _VI_FUNC terM9_getFrontPanelSyncPulseSelect (ViSession vi, ViInt32
                              syncIndex, ViPInt32 pulseGeneratorIndex);
ViStatus _VI_FUNC terM9_getFrontPanelSyncResource (ViSession vi, ViInt32
                              syncIndex, ViPInt32 logicSelect, ViPInt32
                              pulseGeneratorIndex, ViPInt32 connect);
ViStatus _VI_FUNC terM9_getFrontPanelSyncState (ViSession vi, ViInt32 syncIndex,
                              ViPInt32 state);
ViStatus _VI_FUNC terM9_getGenerateTestFilterOption (ViSession vi, ViInt32
                              filterOption, ViPInt32 filterOptionValue);
ViStatus _VI_FUNC terM9_getGenerateTestPinmapFile (ViSession vi, ViChar _VI_FAR
                              testFile[]);
ViStatus _VI_FUNC terM9_getGroundReference (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 groundReference);
ViStatus _VI_FUNC terM9_getGroupGroupOpcode (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 groupOpcode, ViUInt32 _VI_FAR
                              groupValue[]);
ViStatus _VI_FUNC terM9_getHighVoltagePinGroup (ViSession vi, ViInt32
                              HVPinMaxCount, ViPInt32 HVPinCount, ViUInt32
                              _VI_FAR mode[]);
ViStatus _VI_FUNC terM9_getHighVoltagePinGroupSettings (ViSession vi, ViPReal64
                              threshold, ViInt32 HVPinMaxCount, ViPInt32
                              HVPinCount, ViUInt32 _VI_FAR mode[]);
ViStatus _VI_FUNC terM9_getHighVoltagePinGroupThreshold (ViSession vi, ViPReal64
                              threshold);
ViStatus _VI_FUNC terM9_getHighVoltagePinMode (ViSession vi, ViInt32 HVPinIndex,
                              ViPInt32 mode);
ViStatus _VI_FUNC terM9_getInstrumentCount (ViPInt32 count);
ViStatus _VI_FUNC terM9_getInstrumentInformation (ViSession vi, ViChar _VI_FAR
                              systemDesc[], ViPInt32 cardCount, ViPInt32
                              channelCount);
ViStatus _VI_FUNC terM9_getInstrumentResourceName (ViInt32 index, ViChar _VI_FAR
                              name[]);
ViStatus _VI_FUNC terM9_getLastFrontPanelSyncResourceIn (ViSession vi, ViPInt32
                              lastSyncResourceIndex);
ViStatus _VI_FUNC terM9_getLastFrontPanelSyncResourceIndex (ViSession vi,
                              ViPInt32 lastSyncResourceIndex);
ViStatus _VI_FUNC terM9_getLastHighVoltagePinIndex (ViSession vi, ViPInt32
                              lastHVPinIndex);
ViStatus _VI_FUNC terM9_getLastLevelIndex (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 lastLevelIndex);
ViStatus _VI_FUNC terM9_getLastPatternIndex (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 lastPatternIndex);
ViStatus _VI_FUNC terM9_getLastPhaseIndex (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 lastPhaseIndex);
ViStatus _VI_FUNC terM9_getLastProbeTimingSetIndex (ViSession vi, ViPInt32
                              lastPSETIndex);
ViStatus _VI_FUNC terM9_getLastResultIndex (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 lastResultIndex);
ViStatus _VI_FUNC terM9_getLastStepIndex (ViSession vi, ViPInt32 lastStepIndex);
ViStatus _VI_FUNC terM9_getLastTimingSetIndex (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 lastTSETIndex);
ViStatus _VI_FUNC terM9_getLastUserClockIndex (ViSession vi, ViPInt32
                              lastUserClockIndex);
ViStatus _VI_FUNC terM9_getLastWindowIndex (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 lastWindowIndex);
ViStatus _VI_FUNC terM9_getLevelRange (ViSession vi, ViInt32 scopeIndex,
                              ViPInt32 range);
ViStatus _VI_FUNC terM9_getLevelSet (ViSession vi, ViInt32 scopeIndex, ViInt32
                              levelSetIndex, ViPReal64 VIHValue, ViPReal64
                              VILValue, ViPReal64 VOHValue, ViPReal64 VOLValue,
                              ViPReal64 IOHValue, ViPReal64 IOLValue, ViPReal64
                              VCOMValue, ViPInt32 slewRate);
ViStatus _VI_FUNC terM9_getLevelSetIOH (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViPReal64 IOHValue);
ViStatus _VI_FUNC terM9_getLevelSetIOL (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViPReal64 IOLValue);
ViStatus _VI_FUNC terM9_getLevelSetProbe (ViSession vi, ViPReal64 PRHIValue,
                              ViPReal64 PRLOValue);
ViStatus _VI_FUNC terM9_getLevelSetProbePRHI (ViSession vi, ViPReal64
                              PRHIValue);
ViStatus _VI_FUNC terM9_getLevelSetProbePRLO (ViSession vi, ViPReal64
                              PRLOValue);
ViStatus _VI_FUNC terM9_getLevelSetSlewRate (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViPInt32 slewRate);
ViStatus _VI_FUNC terM9_getLevelSetVCOM (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViPReal64 VCOMValue);
ViStatus _VI_FUNC terM9_getLevelSetVIH (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViPReal64 VIHValue);
ViStatus _VI_FUNC terM9_getLevelSetVIL (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViPReal64 VILValue);
ViStatus _VI_FUNC terM9_getLevelSetVOH (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViPReal64 VOHValue);
ViStatus _VI_FUNC terM9_getLevelSetVOL (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViPReal64 VOLValue);
ViStatus _VI_FUNC terM9_getLowPower (ViSession vi, ViInt32 scopeIndex,
                              ViPBoolean state);
ViStatus _VI_FUNC terM9_getNegativeClampVoltage (ViSession vi, ViPReal64
                              negativeClampVoltage);
ViStatus _VI_FUNC terM9_getPatternClutch (ViSession vi, ViPBoolean enable,
                              ViPInt32 polarity, ViPInt32 logicSelect);
ViStatus _VI_FUNC terM9_getPatternClutchEnable (ViSession vi, ViPBoolean
                              enable);
ViStatus _VI_FUNC terM9_getPatternClutchLogicSelect (ViSession vi, ViPInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_getPatternClutchPolarity (ViSession vi, ViPInt32
                              polarity);
ViStatus _VI_FUNC terM9_getPhase (ViSession vi, ViInt32 scopeIndex, ViInt32
                              phaseIndex, ViPBoolean freerun, ViPInt32 trigger);
ViStatus _VI_FUNC terM9_getPhaseFreerun (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 phaseIndex, ViPBoolean freerun);
ViStatus _VI_FUNC terM9_getPhaseTrigger (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 phaseIndex, ViPInt32 trigger);
ViStatus _VI_FUNC terM9_getPinListPinCount (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViPInt32
                              pinListPinCount);
ViStatus _VI_FUNC terM9_getPinmap (ViSession vi, ViInt32 pinmapMaxCount,
                              ViPInt32 pinmapCount, ViInt32 _VI_FAR pinmap[]);
ViStatus _VI_FUNC terM9_getPinmapGroup (ViSession vi, ViInt32 index, ViInt32
                              groupMaxCount, ViPInt32 groupCount, ViInt32
                              _VI_FAR group[]);
ViStatus _VI_FUNC terM9_getPinmapValues (ViSession vi, ViInt32 startingIndex,
                              ViInt32 valueCount, ViInt32 _VI_FAR values[]);
ViStatus _VI_FUNC terM9_getPositiveClampVoltage (ViSession vi, ViPReal64
                              positiveClampVoltage);
ViStatus _VI_FUNC terM9_getProbeMethod (ViSession vi, ViPInt32 probeMethod);
ViStatus _VI_FUNC terM9_getProbeTimingSet (ViSession vi, ViInt32 PSETIndex,
                              ViPReal64 openTime, ViPReal64 closeTime);
ViStatus _VI_FUNC terM9_getProbeTimingSetClose (ViSession vi, ViInt32 PSETIndex,
                              ViPReal64 closeTime);
ViStatus _VI_FUNC terM9_getProbeTimingSetOpen (ViSession vi, ViInt32 PSETIndex,
                              ViPReal64 openTime);
ViStatus _VI_FUNC terM9_getResultIndex (ViSession vi, ViPInt32 resultIndex);
ViStatus _VI_FUNC terM9_getScopeCard (ViInt32 cardIndex, ViPInt32 scopeIndex);
ViStatus _VI_FUNC terM9_getScopeChanCard (ViInt32 cardIndex, ViPInt32
                              scopeIndex);
ViStatus _VI_FUNC terM9_getScopeChanSystem (ViPInt32 scopeIndex);
ViStatus _VI_FUNC terM9_getScopeChannel (ViInt32 chanIndex, ViPInt32
                              scopeIndex);
ViStatus _VI_FUNC terM9_getScopeSystem (ViPInt32 scopeIndex);
ViStatus _VI_FUNC terM9_getStaticPatternCount (ViSession vi, ViPInt32
                              patternCount);
ViStatus _VI_FUNC terM9_getStaticPatternDelay (ViSession vi, ViPReal64 delay);
ViStatus _VI_FUNC terM9_getStaticPatternProbeCapture (ViSession vi, ViPBoolean
                              probeCapture);
ViStatus _VI_FUNC terM9_getSyncResourcePulseWidth (ViSession vi, ViInt32
                              pulseGeneratorIndex, ViPReal64 pulseWidth);
ViStatus _VI_FUNC terM9_getSystemClock (ViSession vi, ViPInt32 divider, ViPInt32
                              edge, ViPInt32 mode, ViPInt32 reference, ViPInt32
                              outPath, ViPInt32 logicSelect);
ViStatus _VI_FUNC terM9_getSystemClockDivider (ViSession vi, ViPInt32 divider);
ViStatus _VI_FUNC terM9_getSystemClockEdge (ViSession vi, ViPInt32 edge);
ViStatus _VI_FUNC terM9_getSystemClockMode (ViSession vi, ViPInt32 mode);
ViStatus _VI_FUNC terM9_getSystemClockOutLogicSelect (ViSession vi, ViPInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_getSystemClockOutPath (ViSession vi, ViPInt32 outPath);
ViStatus _VI_FUNC terM9_getSystemClockReference (ViSession vi, ViPInt32
                              reference);
ViStatus _VI_FUNC terM9_getSystemClutch (ViSession vi, ViPBoolean enable,
                              ViPInt32 polarity, ViPInt32 logicSelect);
ViStatus _VI_FUNC terM9_getSystemClutchEnable (ViSession vi, ViPBoolean enable);
ViStatus _VI_FUNC terM9_getSystemClutchLogicSelect (ViSession vi, ViPInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_getSystemClutchPolarity (ViSession vi, ViPInt32
                              polarity);
ViStatus _VI_FUNC terM9_getSystemEnable (ViSession vi, ViPBoolean enable);
ViStatus _VI_FUNC terM9_getSystemInformation (ViSession vi, ViChar _VI_FAR
                              systemDesc[], ViPInt32 cardCount, ViPInt32
                              channelCount);
ViStatus _VI_FUNC terM9_getSystemTimingMode (ViSession vi, ViPInt32 timingMode);
ViStatus _VI_FUNC terM9_getTimingSet (ViSession vi, ViInt32 TSETIndex, ViPReal64
                              clockPeriod, ViInt32 scopeIndex, ViInt32
                              phaseEntryMaxCount, ViPInt32 phaseEntryCount,
                              ViReal64 _VI_FAR phaseEntry[], ViInt32
                              windowEntryMaxCount, ViPInt32 windowEntryCount,
                              ViReal64 _VI_FAR windowEntry[]);
ViStatus _VI_FUNC terM9_getTimingSetPeriod (ViSession vi, ViInt32 TSETIndex,
                              ViPReal64 clockPeriod);
ViStatus _VI_FUNC terM9_getTimingSetPhase (ViSession vi, ViInt32 TSETIndex,
                              ViInt32 scopeIndex, ViInt32 phaseIndex, ViPReal64
                              assertTime, ViPReal64 returnTime);
ViStatus _VI_FUNC terM9_getTimingSetPhaseAssert (ViSession vi, ViInt32
                              TSETIndex, ViInt32 scopeIndex, ViInt32 phaseIndex,
                              ViPReal64 assertTime);
ViStatus _VI_FUNC terM9_getTimingSetPhaseReturn (ViSession vi, ViInt32
                              TSETIndex, ViInt32 scopeIndex, ViInt32 phaseIndex,
                              ViPReal64 returnTime);
ViStatus _VI_FUNC terM9_getTimingSetWindow (ViSession vi, ViInt32 TSETIndex,
                              ViInt32 scopeIndex, ViInt32 windowIndex, ViPReal64
                              openTime, ViPReal64 closeTime);
ViStatus _VI_FUNC terM9_getTimingSetWindowClose (ViSession vi, ViInt32
                              TSETIndex, ViInt32 scopeIndex, ViInt32
                              windowIndex, ViPReal64 closeTime);
ViStatus _VI_FUNC terM9_getTimingSetWindowOpen (ViSession vi, ViInt32 TSETIndex,
                              ViInt32 scopeIndex, ViInt32 windowIndex, ViPReal64
                              openTime);
ViStatus _VI_FUNC terM9_getUserClock (ViSession vi, ViInt32 userClockIndex,
                              ViPInt32 connect, ViPReal64 highVoltageLevel,
                              ViPReal64 lowVoltageLevel, ViPInt32 mode, ViInt32
                              phaseEntryMaxCount, ViPInt32 phaseEntryCount,
                              ViReal64 _VI_FAR phaseEntry[], ViPInt32 trigger);
ViStatus _VI_FUNC terM9_getUserClockConnect (ViSession vi, ViInt32
                              userClockIndex, ViPInt32 connect);
ViStatus _VI_FUNC terM9_getUserClockHighVoltageLevel (ViSession vi, ViInt32
                              userClockIndex, ViPReal64 highVoltageLevel);
ViStatus _VI_FUNC terM9_getUserClockLowVoltageLevel (ViSession vi, ViInt32
                              userClockIndex, ViPReal64 lowVoltageLevel);
ViStatus _VI_FUNC terM9_getUserClockMode (ViSession vi, ViInt32 userClockIndex,
                              ViPInt32 mode);
ViStatus _VI_FUNC terM9_getUserClockPhase (ViSession vi, ViInt32 userClockIndex,
                              ViInt32 phaseEntryMaxCount, ViPInt32
                              phaseEntryCount, ViReal64 _VI_FAR phaseEntry[]);
ViStatus _VI_FUNC terM9_getUserClockPhaseAssert (ViSession vi, ViInt32
                              userClockIndex, ViInt32 phaseIndex, ViPReal64
                              assertTime);
ViStatus _VI_FUNC terM9_getUserClockPhaseReturn (ViSession vi, ViInt32
                              userClockIndex, ViInt32 phaseIndex, ViPReal64
                              returnTime);
ViStatus _VI_FUNC terM9_getUserClockTrigger (ViSession vi, ViInt32
                              userClockIndex, ViPInt32 trigger);
ViStatus _VI_FUNC terM9_init (ViRsrc rsrcName, ViBoolean idQuery, ViBoolean
                              resetInstr, ViPSession vi);
ViStatus _VI_FUNC terM9_initializeInstrument (ViSession vi);
ViStatus _VI_FUNC terM9_initiateDynamicPatternSetExecut (ViSession vi);
ViStatus _VI_FUNC terM9_initiateDynamicPatternSetExecution (ViSession vi);
ViStatus _VI_FUNC terM9_initiateProbeButton (ViSession vi);
ViStatus _VI_FUNC terM9_loadDynamicPattern (ViSession vi, ViBoolean step,
                              ViBoolean test);
ViStatus _VI_FUNC terM9_loadProbeExpectData (ViSession vi, ViInt32
                              expectDataCount, ViInt32 _VI_FAR expectData[]);
ViStatus _VI_FUNC terM9_loadProbeMapData (ViSession vi, ViInt32 mapDataCount,
                              ViInt32 _VI_FAR mapData[]);
ViStatus _VI_FUNC terM9_loadProbeTimingSetIndexes (ViSession vi, ViInt32
                              initialPatternIndex, ViInt32 patternCount, ViInt32
                              _VI_FAR timingIndexes[], ViBoolean stepsOnly);
ViStatus _VI_FUNC terM9_loadSimulationConfiguration (ViString configFile);
ViStatus _VI_FUNC terM9_measureCurrent (ViSession vi, ViInt32 scopeIndex,
                              ViPReal64 current);
ViStatus _VI_FUNC terM9_measureDCVoltage (ViSession vi, ViInt32 scopeIndex,
                              ViPReal64 voltage);
ViStatus _VI_FUNC terM9_measureFrequency (ViSession vi, ViInt32 scopeIndex,
                              ViPReal64 frequency);
ViStatus _VI_FUNC terM9_modifyDynamicChannelPinOpcode (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViInt32
                              initialPatternIndex, ViInt32 patternCount, ViInt32
                              pinOpcodeBufferCount, ViInt32 _VI_FAR
                              pinOpcodeBuffer[]);
ViStatus _VI_FUNC terM9_openDigitalTest (ViSession vi, ViString testFile,
                              ViPInt32 testHandle);
ViStatus _VI_FUNC terM9_prepareDynamicPatternSetExecuti (ViSession vi, ViBoolean
                              expandLoops, ViBoolean stopOnFail, ViBoolean
                              stopOnEvent, ViBoolean stopOnProbe, ViBoolean
                              stopOnLSEQ, ViBoolean stopOnPattern, ViInt32
                              stopOnPatternIndex, ViInt32 learnFlag, ViInt32
                              preCountFlag, ViInt32 preCount, ViInt32
                              postCount);
ViStatus _VI_FUNC terM9_prepareDynamicPatternSetExecution (ViSession vi,
                              ViBoolean expandLoops, ViBoolean stopOnFail,
                              ViBoolean stopOnEvent, ViBoolean stopOnProbe,
                              ViBoolean stopOnLSEQ, ViBoolean stopOnPattern,
                              ViInt32 stopOnPatternIndex, ViInt32 learnFlag,
                              ViInt32 preCountFlag, ViInt32 preCount, ViInt32
                              postCount);
ViStatus _VI_FUNC terM9_prepareDynamicPatternSetLoading (ViSession vi);
ViStatus _VI_FUNC terM9_prepareDynamicProbeExecution (ViSession vi, ViInt32
                              executeMode);
ViStatus _VI_FUNC terM9_prepareDynamicProbeLoading (ViSession vi);
ViStatus _VI_FUNC terM9_prepareStaticProbeExecution (ViSession vi, ViInt32
                              executeMode, ViInt32 staticCapture);
ViStatus _VI_FUNC terM9_prepareStaticProbeLoading (ViSession vi);
ViStatus _VI_FUNC terM9_readCurrent (ViSession vi, ViInt32 scopeIndex, ViPReal64
                              current);
ViStatus _VI_FUNC terM9_readDCVoltage (ViSession vi, ViInt32 scopeIdx, ViPReal64
                              voltage);
ViStatus _VI_FUNC terM9_readFrequency (ViSession vi, ViInt32 scopeIndex,
                              ViPReal64 frequency);
ViStatus _VI_FUNC terM9_reset (ViSession vi);
ViStatus _VI_FUNC terM9_resetChannelSignature (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[]);
ViStatus _VI_FUNC terM9_resetHighVoltagePins (ViSession vi);
ViStatus _VI_FUNC terM9_resetPinmap (ViSession vi);
ViStatus _VI_FUNC terM9_resetProbe (ViSession vi);
ViStatus _VI_FUNC terM9_revision_query (ViSession vi, ViChar _VI_FAR
                              driverRev[], ViChar _VI_FAR instrRev[]);
ViStatus _VI_FUNC terM9_revisionQuery (ViSession vi, ViChar _VI_FAR driverRev[],
                              ViChar _VI_FAR instrRev[]);
ViStatus _VI_FUNC terM9_runDynamicPatternSet (ViSession vi, ViBoolean
                              expandLoops, ViPInt32 patternSetResult);
ViStatus _VI_FUNC terM9_runHighVoltagePinsTest (ViSession vi, ViInt32
                              testHandle, ViPInt32 testResult);
ViStatus _VI_FUNC terM9_runStaticPattern (ViSession vi, ViBoolean step,
                              ViBoolean test);
ViStatus _VI_FUNC terM9_self_test (ViSession vi, ViPInt32 testResult, ViChar
                              _VI_FAR testMessage[]);
ViStatus _VI_FUNC terM9_selfTest (ViSession vi, ViPInt32 testResult, ViChar
                              _VI_FAR testMessage[]);
ViStatus _VI_FUNC terM9_setAlignmentOption (ViSession vi, ViInt32
                              alignmentOption, ViInt32 alignmentOptionValue);
ViStatus _VI_FUNC terM9_setAlignmentReal64Option (ViSession vi, ViInt32
                              alignmentOption, ViReal64 alignmentOptionValue);
ViStatus _VI_FUNC terM9_setAperture (ViSession vi, ViReal64 aperture);
ViStatus _VI_FUNC terM9_setBackplaneSyncState (ViSession vi, ViInt32 syncIndex,
                              ViInt32 state);
ViStatus _VI_FUNC terM9_setChannel (ViSession vi, ViInt32 pinListCount, ViInt32
                              _VI_FAR pinList[], ViInt32 capture, ViInt32
                              chanMode, ViInt32 connect, ViInt32 format, ViInt32
                              impedance, ViInt32 levelSetIndex, ViInt32 load,
                              ViInt32 phaseIndex, ViInt32 windowIndex);
ViStatus _VI_FUNC terM9_setChannelCapture (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 capture);
ViStatus _VI_FUNC terM9_setChannelChanMode (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 chanMode);
ViStatus _VI_FUNC terM9_setChannelConnect (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 connect);
ViStatus _VI_FUNC terM9_setChannelFormat (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 format);
ViStatus _VI_FUNC terM9_setChannelHybridConnect (ViSession vi, ViInt32
                              pinListCount, ViInt32 _VI_FAR pinList[], ViInt32
                              connect);
ViStatus _VI_FUNC terM9_setChannelImpedance (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 impedance);
ViStatus _VI_FUNC terM9_setChannelLevel (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 levelSetIndex);
ViStatus _VI_FUNC terM9_setChannelLoad (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 load);
ViStatus _VI_FUNC terM9_setChannelPhase (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 phaseIndex);
ViStatus _VI_FUNC terM9_setChannelPinOpcode (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 opcode);
ViStatus _VI_FUNC terM9_setChannelWindow (ViSession vi, ViInt32 pinListCount,
                              ViInt32 _VI_FAR pinList[], ViInt32 windowIndex);
ViStatus _VI_FUNC terM9_setCurrentRange (ViSession vi, ViInt32 currentRange);
ViStatus _VI_FUNC terM9_setDMTXToPinConnect (ViSession vi, ViInt32 scopeIndex);
ViStatus _VI_FUNC terM9_setDynamicPattern (ViSession vi, ViInt32
                              clocksPerPattern, ViInt32 TSETIndex, ViInt32
                              controlOpcode, ViInt32 controlCount, ViInt32
                              controlCond, ViBoolean failCapture, ViBoolean
                              resultCapture, ViInt32 PSETIndex, ViBoolean
                              probeCapture);
ViStatus _VI_FUNC terM9_setDynamicPatternClocksPerPatte (ViSession vi, ViInt32
                              clocksPerPattern);
ViStatus _VI_FUNC terM9_setDynamicPatternClocksPerPattern (ViSession vi, ViInt32
                              clocksPerPattern);
ViStatus _VI_FUNC terM9_setDynamicPatternControl (ViSession vi, ViInt32
                              controlOpcode, ViInt32 controlCount, ViInt32
                              controlCond);
ViStatus _VI_FUNC terM9_setDynamicPatternFailCapture (ViSession vi, ViBoolean
                              failCapture);
ViStatus _VI_FUNC terM9_setDynamicPatternIndex (ViSession vi, ViInt32
                              patternIndex);
ViStatus _VI_FUNC terM9_setDynamicPatternProbeCapture (ViSession vi, ViBoolean
                              probeCapture);
ViStatus _VI_FUNC terM9_setDynamicPatternProbeTimingSet (ViSession vi, ViInt32
                              PSETIndex);
ViStatus _VI_FUNC terM9_setDynamicPatternProbeTimingSetIndex (ViSession vi,
                              ViInt32 PSETIndex);
ViStatus _VI_FUNC terM9_setDynamicPatternResultCapture (ViSession vi, ViBoolean
                              resultCapture);
ViStatus _VI_FUNC terM9_setDynamicPatternSetTimeout (ViSession vi, ViReal64
                              timeout);
ViStatus _VI_FUNC terM9_setDynamicPatternTimingSetIndex (ViSession vi, ViInt32
                              TSETIndex);
ViStatus _VI_FUNC terM9_setEXT1InLogicSelect (ViSession vi, ViInt32
                              logicSelect);  
ViStatus _VI_FUNC terM9_setEXT2InLogicSelect (ViSession vi, ViInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_setExtClockSrcLogicSelect (ViSession vi, ViInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_setFrequencyCounterCycleCount (ViSession vi, ViInt32
                              cycleCount);
ViStatus _VI_FUNC terM9_setFrontPanelSyncConnect (ViSession vi, ViInt32
                              syncIndex, ViInt32 connect);
ViStatus _VI_FUNC terM9_setFrontPanelSyncLogicSelect (ViSession vi, ViInt32
                              syncIndex, ViInt32 logicSelect);
ViStatus _VI_FUNC terM9_setFrontPanelSyncPulseSelect (ViSession vi, ViInt32
                              syncIndex, ViInt32 pulseGeneratorIndex);
ViStatus _VI_FUNC terM9_setFrontPanelSyncResource (ViSession vi, ViInt32
                              syncIndex, ViInt32 logicSelect, ViInt32
                              pulseGeneratorIndex, ViInt32 connect);
ViStatus _VI_FUNC terM9_setFrontPanelSyncState (ViSession vi, ViInt32 syncIndex,
                              ViInt32 state);
ViStatus _VI_FUNC terM9_setGenerateTestFilterOption (ViSession vi, ViInt32
                              filterOption, ViInt32 filterOptionValue);
ViStatus _VI_FUNC terM9_setGenerateTestPinmapFile (ViSession vi, ViString
                              testFile);
ViStatus _VI_FUNC terM9_setGroundReference (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 groundReference);
ViStatus _VI_FUNC terM9_setGroupGroupOpcode (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 groupOpcode, ViUInt32 _VI_FAR
                              groupValue[]);
ViStatus _VI_FUNC terM9_setHighVoltagePinGroup (ViSession vi, ViInt32
                              HVPinCount, ViUInt32 _VI_FAR mode[]);
ViStatus _VI_FUNC terM9_setHighVoltagePinGroupSettings (ViSession vi, ViReal64
                              threshold, ViInt32 HVPinCount, ViUInt32 _VI_FAR
                              mode[]);
ViStatus _VI_FUNC terM9_setHighVoltagePinGroupThreshold (ViSession vi, ViReal64
                              threshold);
ViStatus _VI_FUNC terM9_setHighVoltagePinMode (ViSession vi, ViInt32 HVPinIndex,
                              ViInt32 mode);
ViStatus _VI_FUNC terM9_setLevelRange (ViSession vi, ViInt32 scopeIndex, ViInt32
                              range);
ViStatus _VI_FUNC terM9_setLevelSet (ViSession vi, ViInt32 scopeIndex, ViInt32
                              levelSetIndex, ViReal64 VIHValue, ViReal64
                              VILValue, ViReal64 VOHValue, ViReal64 VOLValue,
                              ViReal64 IOHValue, ViReal64 IOLValue, ViReal64
                              VCOMValue, ViInt32 slewRate);
ViStatus _VI_FUNC terM9_setLevelSetIOH (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViReal64 IOHValue);
ViStatus _VI_FUNC terM9_setLevelSetIOL (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViReal64 IOLValue);
ViStatus _VI_FUNC terM9_setLevelSetProbe (ViSession vi, ViReal64 PRHIValue,
                              ViReal64 PRLOValue);
ViStatus _VI_FUNC terM9_setLevelSetProbePRHI (ViSession vi, ViReal64 PRHIValue);
ViStatus _VI_FUNC terM9_setLevelSetProbePRLO (ViSession vi, ViReal64 PRLOValue);
ViStatus _VI_FUNC terM9_setLevelSetSlewRate (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViInt32 slewRate);
ViStatus _VI_FUNC terM9_setLevelSetVCOM (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViReal64 VCOMValue);
ViStatus _VI_FUNC terM9_setLevelSetVIH (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViReal64 VIHValue);
ViStatus _VI_FUNC terM9_setLevelSetVIL (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViReal64 VILValue);
ViStatus _VI_FUNC terM9_setLevelSetVOH (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViReal64 VOHValue);
ViStatus _VI_FUNC terM9_setLevelSetVOL (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 levelSetIndex, ViReal64 VOLValue);
ViStatus _VI_FUNC terM9_setLowPower (ViSession vi, ViInt32 scopeIndex, ViBoolean
                              state);
ViStatus _VI_FUNC terM9_setNegativeClampVoltage (ViSession vi, ViReal64
                              negativeClampV);
ViStatus _VI_FUNC terM9_setPatternClutch (ViSession vi, ViBoolean enable,
                              ViInt32 polarity, ViInt32 logicSelect);
ViStatus _VI_FUNC terM9_setPatternClutchEnable (ViSession vi, ViBoolean enable);
ViStatus _VI_FUNC terM9_setPatternClutchLogicSelect (ViSession vi, ViInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_setPatternClutchPolarity (ViSession vi, ViInt32
                              polarity);
ViStatus _VI_FUNC terM9_setPhase (ViSession vi, ViInt32 scopeIndex, ViInt32
                              phaseIndex, ViBoolean freerun, ViInt32 trigger);
ViStatus _VI_FUNC terM9_setPhaseFreerun (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 phaseIndex, ViBoolean freerun);
ViStatus _VI_FUNC terM9_setPhaseTrigger (ViSession vi, ViInt32 scopeIndex,
                              ViInt32 phaseIndex, ViInt32 trigger);
ViStatus _VI_FUNC terM9_setPinmap (ViSession vi, ViInt32 pinmapCount, ViInt32
                              _VI_FAR pinmap[]);
ViStatus _VI_FUNC terM9_setPinmapGroup (ViSession vi, ViInt32 index, ViInt32
                              groupCount, ViInt32 _VI_FAR group[]);
ViStatus _VI_FUNC terM9_setPinmapValues (ViSession vi, ViInt32 startingIndex,
                              ViInt32 valueCount, ViInt32 _VI_FAR values[]);
ViStatus _VI_FUNC terM9_setPositiveClampVoltage (ViSession vi, ViReal64
                              positiveClampV);
ViStatus _VI_FUNC terM9_setProbeMethod (ViSession vi, ViInt32 probeMethod);
ViStatus _VI_FUNC terM9_setProbeTimingSet (ViSession vi, ViInt32 PSETIndex,
                              ViReal64 openTime, ViReal64 closeTime);
ViStatus _VI_FUNC terM9_setProbeTimingSetClose (ViSession vi, ViInt32 PSETIndex,
                              ViReal64 closeTime);
ViStatus _VI_FUNC terM9_setProbeTimingSetOpen (ViSession vi, ViInt32 PSETIndex,
                              ViReal64 openTime);
ViStatus _VI_FUNC terM9_setStaticPatternDelay (ViSession vi, ViReal64 delay);
ViStatus _VI_FUNC terM9_setStaticPatternProbeCapture (ViSession vi, ViBoolean
                              probeCapture);
ViStatus _VI_FUNC terM9_setSyncResourcePulseWidth (ViSession vi, ViInt32
                              pulseGeneratorIndex, ViReal64 pulseWidth);
ViStatus _VI_FUNC terM9_setSystemClock (ViSession vi, ViInt32 divider, ViInt32
                              edge, ViInt32 mode, ViInt32 reference, ViInt32
                              outPath, ViInt32 logicSelect);
ViStatus _VI_FUNC terM9_setSystemClockDivider (ViSession vi, ViInt32 divider);
ViStatus _VI_FUNC terM9_setSystemClockEdge (ViSession vi, ViInt32 edge);
ViStatus _VI_FUNC terM9_setSystemClockMode (ViSession vi, ViInt32 mode);
ViStatus _VI_FUNC terM9_setSystemClockOutLogicSelect (ViSession vi, ViInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_setSystemClockOutPath (ViSession vi, ViInt32 outPath);
ViStatus _VI_FUNC terM9_setSystemClockReference (ViSession vi, ViInt32
                              reference);
ViStatus _VI_FUNC terM9_setSystemClutch (ViSession vi, ViBoolean enable, ViInt32
                              polarity, ViInt32 logicSelect);
ViStatus _VI_FUNC terM9_setSystemClutchEnable (ViSession vi, ViBoolean enable);
ViStatus _VI_FUNC terM9_setSystemClutchLogicSelect (ViSession vi, ViInt32
                              logicSelect);
ViStatus _VI_FUNC terM9_setSystemClutchPolarity (ViSession vi, ViInt32
                              polarity);
ViStatus _VI_FUNC terM9_setSystemEnable (ViSession vi, ViBoolean enable);
ViStatus _VI_FUNC terM9_setSystemTimingMode (ViSession vi, ViInt32 timingMode);
ViStatus _VI_FUNC terM9_setTimer (ViSession vi, ViReal64 waitTime);
ViStatus _VI_FUNC terM9_setTimingSet (ViSession vi, ViInt32 TSETIndex, ViReal64
                              clockPeriod, ViInt32 scopeIndex, ViInt32
                              phaseEntryCount, ViReal64 _VI_FAR phaseEntry[],
                              ViInt32 windowEntryCount, ViReal64 _VI_FAR
                              windowEntry[]);
ViStatus _VI_FUNC terM9_setTimingSetPeriod (ViSession vi, ViInt32 TSETIndex,
                              ViReal64 clockPeriod);
ViStatus _VI_FUNC terM9_setTimingSetPhase (ViSession vi, ViInt32 TSETIndex,
                              ViInt32 scopeIndex, ViInt32 phaseIndex, ViReal64
                              assertTime, ViReal64 returnTime);
ViStatus _VI_FUNC terM9_setTimingSetPhaseAssert (ViSession vi, ViInt32
                              TSETIndex, ViInt32 scopeIndex, ViInt32 phaseIndex,
                              ViReal64 assertTime);
ViStatus _VI_FUNC terM9_setTimingSetPhaseReturn (ViSession vi, ViInt32
                              TSETIndex, ViInt32 scopeIndex, ViInt32 phaseIndex,
                              ViReal64 returnTime);
ViStatus _VI_FUNC terM9_setTimingSetWindow (ViSession vi, ViInt32 TSETIndex,
                              ViInt32 scopeIndex, ViInt32 windowIndex, ViReal64
                              openTime, ViReal64 closeTime);
ViStatus _VI_FUNC terM9_setTimingSetWindowClose (ViSession vi, ViInt32
                              TSETIndex, ViInt32 scopeIndex, ViInt32
                              windowIndex, ViReal64 closeTime);
ViStatus _VI_FUNC terM9_setTimingSetWindowOpen (ViSession vi, ViInt32 TSETIndex,
                              ViInt32 scopeIndex, ViInt32 windowIndex, ViReal64
                              openTime);
ViStatus _VI_FUNC terM9_setUserClock (ViSession vi, ViInt32 userClockIndex,
                              ViInt32 connect, ViReal64 highVoltageLevel,
                              ViReal64 lowVoltageLevel, ViInt32 mode, ViInt32
                              phaseEntryCount, ViReal64 _VI_FAR phaseEntry[],
                              ViInt32 trigger);
ViStatus _VI_FUNC terM9_setUserClockConnect (ViSession vi, ViInt32
                              userClockIndex, ViInt32 connect);
ViStatus _VI_FUNC terM9_setUserClockHighVoltageLevel (ViSession vi, ViInt32
                              userClockIndex, ViReal64 highVoltageLevel);
ViStatus _VI_FUNC terM9_setUserClockLowVoltageLevel (ViSession vi, ViInt32
                              userClockIndex, ViReal64 lowVoltageLevel);
ViStatus _VI_FUNC terM9_setUserClockMode (ViSession vi, ViInt32 userClockIndex,
                              ViInt32 mode);
ViStatus _VI_FUNC terM9_setUserClockPhase (ViSession vi, ViInt32 userClockIndex,
                              ViInt32 phaseEntryCount, ViReal64 _VI_FAR
                              phaseEntry[]);
ViStatus _VI_FUNC terM9_setUserClockPhaseAssert (ViSession vi, ViInt32
                              userClockIndex, ViInt32 phaseIndex, ViReal64
                              assertTime);
ViStatus _VI_FUNC terM9_setUserClockPhaseReturn (ViSession vi, ViInt32
                              userClockIndex, ViInt32 phaseIndex, ViReal64
                              returnTime);
ViStatus _VI_FUNC terM9_setUserClockTrigger (ViSession vi, ViInt32
                              userClockIndex, ViInt32 trigger);
ViStatus _VI_FUNC terM9_startTimer (ViSession vi);
ViStatus _VI_FUNC terM9_stopAtPattern (ViSession vi, ViInt32 patternIndex,
                              ViBoolean stopAtPattern);
ViStatus _VI_FUNC terM9_stopDynamicPatternSetExecution (ViSession vi);
ViStatus _VI_FUNC terM9_stopForFailure (ViSession vi, ViBoolean stopForFailure);
ViStatus _VI_FUNC terM9_stopProbeButton (ViSession vi);
ViStatus _VI_FUNC terM9_stopTimer (ViSession vi);
ViStatus _VI_FUNC terM9_testTimer (ViSession vi, ViPBoolean timerDone);
ViStatus _VI_FUNC terM9_wait (ViSession vi, ViReal64 waitTime);
ViStatus _VI_FUNC terM9_waitForDynamicPatternSetExecuti (ViSession vi);
ViStatus _VI_FUNC terM9_waitForDynamicPatternSetExecution (ViSession vi);
ViStatus _VI_FUNC terM9_waitForProbeButton (ViSession vi, ViReal64 timeout);
ViStatus _VI_FUNC terM9_waitForTimer (ViSession vi);
ViStatus _VI_FUNC terM9_setLevelSetReferenceSource (ViSession vi, ViInt32
                              scopeIdx, ViInt32 source);
ViStatus _VI_FUNC terM9_getLevelSetReferenceSource (ViSession vi, ViInt32
                              scopeIdx, ViPInt32 source);


#if defined (__cplusplus) || defined (__cplusplus__)
}
#endif
#endif
