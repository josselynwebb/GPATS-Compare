Attribute VB_Name = "terM9bas"
Option Explicit
DefInt A-Z

'
' Copyright 1997 by Teradyne, Inc., Boston, MA
'
' Module:   terM9.bas
' Creator:  Alycia A. McGoldrick
'
' Abstract: This file contains the function prototypes for the M9 VXI
'           plug&play driver for Visual Basic.  It is based on the following
'           C header files:  terM9.h, terM9_scope.h, terM9_defs.h,
'           terM9_errors.h, terM9_types.h
'
' Latest .h revisions:
'           terM9.h             M90301.11
'           terM9_scope.h       M90240
'           terM9_defs.h        M90300.10
'           terM9_errors.h      M90301.00
'           terM9_types.h       M90301.00
'
' Revision History:
'
' MANTEC.H_ 11-Nov-97 18:26:14    mam    add scope.bas and interrupt handler routines
' M90301.29 31-Oct-97 07:55:44    tpl    add diagnostic selftest function
' M90301.18 03-Sep-97 10:26:55    tpl    add some aliases
' M90301.11 12-Aug-97 10:26:01    tpl    add new functions
' M90301.07 31-Jul-97 15:20:29    tpl    rename getLastSyncResourceIndex
' M90301.05 31-Jul-97 10:45:20    tpl    Remove setResultIndex,set/getProbe add
'                                        parameters to some prepareprobe functions
' M90205    08-Jul-97 11:36:00    yae    Realigning with the latest revs of terM9.h
' M90204    03-Jul-97 10:46:00    aam    Realigning with the latest revs for the
'                                        FCS DLL
' M90203    23-May-97 10:00:00    aam    Updated to align with Beta-ship DLL
' M90202     1-May-97 11:15:00    aam    Changed TSETIdxes to be Longs not Integers
' M90201    25-Mar-97 11:56:29    aam    Creation
'
' terM9.h
' Note:  the format of the functions is:
'   Declare Function function Lib "terM9_32.dll" ([ByVal] parameterName As parameterType, ...) As Long
'
'   Note:  every function declaration must be declared on a single line
'

Declare Function terM9_init Lib "terM9_32.dll" (ByVal rsrcName As String, ByVal idQuery As Integer, ByVal resetInstr As Integer, vi As Long) As Long

Declare Function terM9_executeDigitalTest Lib "terM9_32.dll" (ByVal vi As Long, ByVal testFile As String, ByVal expandLoops As Integer, testResult As Long) As Long

Declare Function terM9_openDigitalTest Lib "terM9_32.dll" (ByVal vi As Long, ByVal testFile As String, testHandle As Long) As Long

Declare Function terM9_configureDigitalTestSetup Lib "terM9_32.dll" (ByVal vi As Long, ByVal testHandle As Long) As Long

Declare Function terM9_configureDigitalTestPatternSet Lib "terM9_32.dll" (ByVal vi As Long, ByVal testHandle As Long, ByVal patternSetIdx As Long) As Long

Declare Function terM9_closeDigitalTest Lib "terM9_32.dll" (ByVal vi As Long, ByVal testHandle As Long) As Long

Declare Function terM9_getPinmap Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinmapMaxCount As Long, pinmapCount As Long, pinmap As Long) As Long

Declare Function terM9_getPinmapValues Lib "terM9_32.dll" (ByVal vi As Long, ByVal startingIndex As Long, ByVal locationCount As Long, contents As Long) As Long

Declare Function terM9_getPinmapGroup Lib "terM9_32.dll" (ByVal vi As Long, ByVal index As Long, ByVal groupMaxCount As Long, groupCount As Long, group As Long) As Long

Declare Function terM9_setPinmap Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinmapCount As Long, pinmap As Long) As Long

Declare Function terM9_setPinmapValues Lib "terM9_32.dll" (ByVal vi As Long, ByVal startingIndex As Long, ByVal locationCount As Long, contents As Long) As Long

Declare Function terM9_setPinmapGroup Lib "terM9_32.dll" (ByVal vi As Long, ByVal index As Long, ByVal groupCount As Long, group As Long) As Long

Declare Function terM9_getPinListPinCount Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, pinListPinCount As Long) As Long

Declare Function terM9_resetPinmap Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_getChannel Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, Capture As Long, chanMode As Long, connect As Long, Format As Long, impedance As Long, LevelIdx As Long, Load As Long, PhaseIdx As Long, WindowIdx As Long) As Long

Declare Function terM9_getChannelCapture Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, Capture As Long) As Long

Declare Function terM9_getChannelChanMode Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, chanMode As Long) As Long

Declare Function terM9_getChannelConnect Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, connect As Long) As Long

Declare Function terM9_getChannelFormat Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, Format As Long) As Long

Declare Function terM9_getChannelImpedance Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, impedance As Long) As Long

Declare Function terM9_getChannelLevel Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, LevelIdx As Long) As Long

Declare Function terM9_getChannelLoad Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, Load As Long) As Long

Declare Function terM9_getChannelPhase Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, PhaseIdx As Long) As Long

Declare Function terM9_getChannelWindow Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, WindowIdx As Long) As Long

Declare Function terM9_setChannel Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal Capture As Long, ByVal chanMode As Long, ByVal connect As Long, ByVal Format As Long, ByVal impedance As Long, ByVal LevelIdx As Long, ByVal Load As Long, ByVal PhaseIdx As Long, ByVal WindowIdx As Long) As Long

Declare Function terM9_setChannelCapture Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal Capture As Long) As Long

Declare Function terM9_setChannelChanMode Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal chanMode As Long) As Long

Declare Function terM9_setChannelConnect Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal connect As Long) As Long

Declare Function terM9_setChannelFormat Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal Format As Long) As Long

Declare Function terM9_setChannelImpedance Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal impedance As Long) As Long

Declare Function terM9_setChannelLevel Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal LevelIdx As Long) As Long

Declare Function terM9_setChannelLoad Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal Load As Long) As Long

Declare Function terM9_setChannelPhase Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal PhaseIdx As Long) As Long

Declare Function terM9_setChannelWindow Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal WindowIdx As Long) As Long

Declare Function terM9_getTimingSet Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, clockPeriod As Double, ByVal scopeIdx As Long, ByVal phaseEntryMaxCount As Long, phaseEntryCount As Long, phaseEntry As Double, ByVal windowEntryMaxCount As Long, windowEntryCount As Long, windowEntry As Double) As Long

Declare Function terM9_getTimingSetPeriod Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, clockPeriod As Double) As Long

Declare Function terM9_getTimingSetPhase Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, assertTime As Double, returnTime As Double) As Long

Declare Function terM9_getTimingSetPhaseAssert Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, assertTime As Double) As Long

Declare Function terM9_getTimingSetPhaseReturn Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, returnTime As Double) As Long

Declare Function terM9_getTimingSetWindow Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal WindowIdx As Long, openTime As Double, closeTime As Double) As Long

Declare Function terM9_getTimingSetWindowOpen Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal WindowIdx As Long, openTime As Double) As Long

Declare Function terM9_getTimingSetWindowClose Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal WindowIdx As Long, closeTime As Double) As Long

Declare Function terM9_setTimingSet Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal clockPeriod As Double, ByVal scopeIdx As Long, ByVal phaseEntryCount As Long, phaseEntry As Double, ByVal windowEntryCount As Long, windowEntry As Double) As Long

Declare Function terM9_setTimingSetPeriod Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal clockPeriod As Double) As Long

Declare Function terM9_setTimingSetPhase Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, ByVal assertTime As Double, ByVal returnTime As Double) As Long

Declare Function terM9_setTimingSetPhaseAssert Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, ByVal assertTime As Double) As Long

Declare Function terM9_setTimingSetPhaseReturn Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, ByVal returnTime As Double) As Long

Declare Function terM9_setTimingSetWindow Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal WindowIdx As Long, ByVal openTime As Double, ByVal closeTime As Double) As Long

Declare Function terM9_setTimingSetWindowOpen Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal WindowIdx As Long, ByVal openTime As Double) As Long

Declare Function terM9_setTimingSetWindowClose Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long, ByVal scopeIdx As Long, ByVal WindowIdx As Long, ByVal closeTime As Double) As Long

Declare Function terM9_getPhase Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, freerun As Integer, trigger As Long) As Long

Declare Function terM9_getPhaseFreerun Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, freerun As Integer) As Long

Declare Function terM9_getPhaseTrigger Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, trigger As Long) As Long

Declare Function terM9_setPhase Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, ByVal freerun As Integer, ByVal trigger As Long) As Long

Declare Function terM9_setPhaseFreerun Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, ByVal freerun As Integer) As Long

Declare Function terM9_setPhaseTrigger Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal PhaseIdx As Long, ByVal trigger As Long) As Long

Declare Function terM9_getExtClockSrcLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, logicSelect As Long) As Long

Declare Function terM9_setExtClockSrcLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_getSystemClock Lib "terM9_32.dll" (ByVal vi As Long, divider As Long, edge As Long, mode As Long, reference As Long, outpath As Long, logicSelect As Long) As Long

Declare Function terM9_getSystemClockDivider Lib "terM9_32.dll" (ByVal vi As Long, divider As Long) As Long

Declare Function terM9_getSystemClockEdge Lib "terM9_32.dll" (ByVal vi As Long, edge As Long) As Long

Declare Function terM9_getSystemClockMode Lib "terM9_32.dll" (ByVal vi As Long, mode As Long) As Long

Declare Function terM9_getSystemClockReference Lib "terM9_32.dll" (ByVal vi As Long, reference As Long) As Long

Declare Function terM9_getSystemClockOutPath Lib "terM9_32.dll" (ByVal vi As Long, outpath As Long) As Long

Declare Function terM9_getSystemClockOutLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, logicSelect As Long) As Long

Declare Function terM9_setSystemClock Lib "terM9_32.dll" (ByVal vi As Long, ByVal divider As Long, ByVal edge As Long, ByVal mode As Long, ByVal reference As Long, ByVal outpath As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_setSystemClockDivider Lib "terM9_32.dll" (ByVal vi As Long, ByVal divider As Long) As Long

Declare Function terM9_setSystemClockEdge Lib "terM9_32.dll" (ByVal vi As Long, ByVal edge As Long) As Long

Declare Function terM9_setSystemClockMode Lib "terM9_32.dll" (ByVal vi As Long, ByVal mode As Long) As Long

Declare Function terM9_setSystemClockReference Lib "terM9_32.dll" (ByVal vi As Long, ByVal reference As Long) As Long

Declare Function terM9_setSystemClockOutPath Lib "terM9_32.dll" (ByVal vi As Long, ByVal outpath As Long) As Long

Declare Function terM9_setSystemClockOutLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_getSystemClutch Lib "terM9_32.dll" (ByVal vi As Long, enable As Integer, polarity As Long, logicSelect As Long) As Long

Declare Function terM9_getSystemClutchEnable Lib "terM9_32.dll" (ByVal vi As Long, enable As Integer) As Long

Declare Function terM9_getSystemClutchPolarity Lib "terM9_32.dll" (ByVal vi As Long, polarity As Long) As Long

Declare Function terM9_getSystemClutchLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, logicSelect As Long) As Long

Declare Function terM9_setSystemClutch Lib "terM9_32.dll" (ByVal vi As Long, ByVal enable As Integer, ByVal polarity As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_setSystemClutchEnable Lib "terM9_32.dll" (ByVal vi As Long, ByVal enable As Integer) As Long

Declare Function terM9_setSystemClutchPolarity Lib "terM9_32.dll" (ByVal vi As Long, ByVal polarity As Long) As Long

Declare Function terM9_setSystemClutchLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_getPatternClutch Lib "terM9_32.dll" (ByVal vi As Long, enable As Integer, polarity As Long, logicSelect As Long) As Long

Declare Function terM9_getPatternClutchEnable Lib "terM9_32.dll" (ByVal vi As Long, enable As Integer) As Long

Declare Function terM9_getPatternClutchPolarity Lib "terM9_32.dll" (ByVal vi As Long, polarity As Long) As Long

Declare Function terM9_getPatternClutchLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, logicSelect As Long) As Long

Declare Function terM9_setPatternClutch Lib "terM9_32.dll" (ByVal vi As Long, ByVal enable As Integer, ByVal polarity As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_setPatternClutchEnable Lib "terM9_32.dll" (ByVal vi As Long, ByVal enable As Integer) As Long

Declare Function terM9_setPatternClutchPolarity Lib "terM9_32.dll" (ByVal vi As Long, ByVal polarity As Long) As Long

Declare Function terM9_setPatternClutchLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_getEXT1InLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, logicSelect As Long) As Long

Declare Function terM9_setEXT1InLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_getEXT2InLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, logicSelect As Long) As Long

Declare Function terM9_setEXT2InLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_getLevelSet Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, VIHValue As Double, VILValue As Double, VOHValue As Double, VOLValue As Double, IOHValue As Double, IOLValue As Double, VCOMValue As Double, slewRate As Long) As Long

Declare Function terM9_getLevelSetIOH Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, IOHValue As Double) As Long

Declare Function terM9_getLevelSetIOL Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, IOLValue As Double) As Long

Declare Function terM9_getLevelSetVCOM Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, VCOMValue As Double) As Long

Declare Function terM9_getLevelSetVIH Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, VIHValue As Double) As Long

Declare Function terM9_getLevelSetVIL Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, VILValue As Double) As Long

Declare Function terM9_getLevelSetVOH Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, VOHValue As Double) As Long

Declare Function terM9_getLevelSetVOL Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, VOLValue As Double) As Long

Declare Function terM9_getLevelSetSlewRate Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, slewRate As Long) As Long

Declare Function terM9_setLevelSet Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal VIHValue As Double, ByVal VILValue As Double, ByVal VOHValue As Double, ByVal VOLValue As Double, ByVal IOHValue As Double, ByVal IOLValue As Double, ByVal VCOMValue As Double, ByVal slewRate As Long) As Long

Declare Function terM9_setLevelSetIOH Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal IOHValue As Double) As Long

Declare Function terM9_setLevelSetIOL Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal IOLValue As Double) As Long

Declare Function terM9_setLevelSetVCOM Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal VCOMValue As Double) As Long

Declare Function terM9_setLevelSetVIH Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal VIHValue As Double) As Long

Declare Function terM9_setLevelSetVIL Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal VILValue As Double) As Long

Declare Function terM9_setLevelSetVOH Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal VOHValue As Double) As Long

Declare Function terM9_setLevelSetVOL Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal VOLValue As Double) As Long

Declare Function terM9_setLevelSetSlewRate Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal LevelIdx As Long, ByVal slewRate As Long) As Long

Declare Function terM9_getLevelSetProbe Lib "terM9_32.dll" (ByVal vi As Long, PRHIValue As Double, PRLOValue As Double) As Long

Declare Function terM9_getLevelSetProbePRHI Lib "terM9_32.dll" (ByVal vi As Long, PRHIValue As Double) As Long

Declare Function terM9_getLevelSetProbePRLO Lib "terM9_32.dll" (ByVal vi As Long, PRLOValue As Double) As Long

Declare Function terM9_setLevelSetProbe Lib "terM9_32.dll" (ByVal vi As Long, ByVal PRHIValue As Double, ByVal PRLOValue As Double) As Long

Declare Function terM9_setLevelSetProbePRLO Lib "terM9_32.dll" (ByVal vi As Long, ByVal PRLOValue As Double) As Long

Declare Function terM9_setLevelSetProbePRHI Lib "terM9_32.dll" (ByVal vi As Long, ByVal PRHIValue As Double) As Long

Declare Function terM9_getChannelPinOpcode Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, opcode As Long) As Long

Declare Function terM9_setChannelPinOpcode Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal opcode As Long) As Long

Declare Function terM9_getGroupGroupOpcode Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, groupOpcode As Long, groupValue As Long) As Long

Declare Function terM9_setGroupGroupOpcode Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal groupOpcode As Long, groupValue As Long) As Long

Declare Function terM9_getFrontPanelSyncState Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, state As Long) As Long

Declare Function terM9_setFrontPanelSyncState Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, ByVal state As Long) As Long

Declare Function terM9_getBackplaneSyncState Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, state As Long) As Long

Declare Function terM9_setBackplaneSyncState Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, ByVal state As Long) As Long

Declare Function terM9_resetChannelSignature Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long) As Long

Declare Function terM9_prepareDynamicPatternSetLoading Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_getDynamicPattern Lib "terM9_32.dll" (ByVal vi As Long, clocksPerPattern As Long, TSETIdx As Long, controlOpcode As Long, controlCount As Long, controlCond As Long, failCapture As Integer, resultCapture As Integer, PSETIdx As Long, probeCapture As Integer) As Long

Declare Function terM9_getDynamicPatternClocksPerPattern Lib "terM9_32.dll" (ByVal vi As Long, clocksPerPattern As Long) As Long

Declare Function terM9_getDynamicPatternTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Long, TSETIdx As Long) As Long

Declare Function terM9_getDynamicPatternControl Lib "terM9_32.dll" (ByVal vi As Long, controlOpcode As Long, controlCount As Long, controlCond As Long) As Long

Declare Function terM9_getDynamicPatternFailCapture Lib "terM9_32.dll" (ByVal vi As Long, failCapture As Integer) As Long

Declare Function terM9_getDynamicPatternResultCapture Lib "terM9_32.dll" (ByVal vi As Long, resultCapture As Integer) As Long

Declare Function terM9_getDynamicPatternProbeTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Long, PSETIdx As Long) As Long

Declare Function terM9_getDynamicPatternProbeCapture Lib "terM9_32.dll" (ByVal vi As Long, probeCapture As Integer) As Long

Declare Function terM9_getDynamicPatternCount Lib "terM9_32.dll" (ByVal vi As Long, patternCount As Long) As Long

Declare Function terM9_getDynamicPatternIndex Lib "terM9_32.dll" (ByVal vi As Long, patternIdx As Long) As Long

Declare Function terM9_getResultIndex Lib "terM9_32.dll" (ByVal vi As Long, resultIdx As Long) As Long

Declare Function terM9_setDynamicPattern Lib "terM9_32.dll" (ByVal vi As Long, ByVal clocksPerPattern As Long, ByVal TSETIdx As Long, ByVal controlOpcode As Long, ByVal controlCount As Long, ByVal controlCond As Long, ByVal failCapture As Integer, ByVal resultCapture As Integer, ByVal PSETIdx As Long, ByVal probeCapture As Integer) As Long

Declare Function terM9_setDynamicPatternClocksPerPattern Lib "terM9_32.dll" (ByVal vi As Long, ByVal clocksPerPattern As Long) As Long

Declare Function terM9_setDynamicPatternTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal TSETIdx As Long) As Long

Declare Function terM9_setDynamicPatternControl Lib "terM9_32.dll" (ByVal vi As Long, ByVal controlOpcode As Long, ByVal controlCount As Long, ByVal controlCond As Long) As Long

Declare Function terM9_setDynamicPatternFailCapture Lib "terM9_32.dll" (ByVal vi As Long, ByVal failCapture As Integer) As Long

Declare Function terM9_setDynamicPatternResultCapture Lib "terM9_32.dll" (ByVal vi As Long, ByVal resultCapture As Integer) As Long

Declare Function terM9_setDynamicPatternProbeTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal PSETIdx As Long) As Long

Declare Function terM9_setDynamicPatternProbeCapture Lib "terM9_32.dll" (ByVal vi As Long, ByVal probeCapture As Integer) As Long

Declare Function terM9_setDynamicPatternIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal patternIdx As Long) As Long

Declare Function terM9_loadDynamicPattern Lib "terM9_32.dll" (ByVal vi As Long, ByVal step As Integer, ByVal test As Integer) As Long

Declare Function terM9_fetchDynamicChannelPinOpcode Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal initPatternIdx As Long, ByVal patternCount As Long, pinOpcodeBuffer As Long) As Long

Declare Function terM9_modifyDynamicChannelPinOpcode Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal initPatternIdx As Long, ByVal patternCount As Long, ByVal pinOpcodeCount As Long, pinOpcodeBuffer As Long) As Long

Declare Function terM9_fetchDynamicPatternModifiers Lib "terM9_32.dll" (ByVal vi As Long, ByVal initPatternIdx As Long, ByVal patternCount As Long, clocksPerPattern As Long, TSETIdx As Long, controlOpcode As Long, controlCount As Long, controlCond As Long, test As Integer, failCapture As Integer, resultCapture As Integer, PSETIdx As Long, step As Integer, probeCapture As Integer) As Long

Declare Function terM9_getStaticPatternProbeCapture Lib "terM9_32.dll" (ByVal vi As Long, probeCapture As Integer) As Long

Declare Function terM9_getStaticPatternCount Lib "terM9_32.dll" (ByVal vi As Long, patternCount As Long) As Long

Declare Function terM9_setStaticPatternProbeCapture Lib "terM9_32.dll" (ByVal vi As Long, ByVal probeCapture As Integer) As Long

Declare Function terM9_fetchStaticChannelPinState Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, pinStateBuffer As Long) As Long

Declare Function terM9_getProbeMethod Lib "terM9_32.dll" (ByVal vi As Long, probeMethod As Long) As Long

Declare Function terM9_setProbeMethod Lib "terM9_32.dll" (ByVal vi As Long, ByVal probeMethod As Long) As Long

Declare Function terM9_loadProbeExpectData Lib "terM9_32.dll" (ByVal vi As Long, ByVal expectDataCount As Long, expectData As Long) As Long

Declare Function terM9_loadProbeMapData Lib "terM9_32.dll" (ByVal vi As Long, ByVal mapDataCount As Long, mapData As Long) As Long

Declare Function terM9_loadProbeTimingSetIndexes Lib "terM9_32.dll" (ByVal vi As Long, ByVal initPatternIdx As Long, ByVal patternCount As Long, timingIndexes As Long, ByVal stepOnly As Integer) As Long

Declare Function terM9_prepareDynamicProbeLoading Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_getProbeTimingSet Lib "terM9_32.dll" (ByVal vi As Long, ByVal PSETIdx As Long, openTime As Double, closeTime As Double) As Long

Declare Function terM9_getProbeTimingSetOpen Lib "terM9_32.dll" (ByVal vi As Long, ByVal PSETIdx As Long, openTime As Double) As Long

Declare Function terM9_getProbeTimingSetClose Lib "terM9_32.dll" (ByVal vi As Long, ByVal PSETIdx As Long, closeTime As Double) As Long

Declare Function terM9_setProbeTimingSet Lib "terM9_32.dll" (ByVal vi As Long, ByVal PSETIdx As Long, ByVal openTime As Double, ByVal closeTime As Double) As Long

Declare Function terM9_setProbeTimingSetOpen Lib "terM9_32.dll" (ByVal vi As Long, ByVal PSETIdx As Long, ByVal openTime As Double) As Long

Declare Function terM9_setProbeTimingSetClose Lib "terM9_32.dll" (ByVal vi As Long, ByVal PSETIdx As Long, ByVal closeTime As Double) As Long

Declare Function terM9_prepareStaticProbeLoading Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_getHighVoltagePinGroupSettings Lib "terM9_32.dll" (ByVal vi As Long, threshold As Double, ByVal HVPinMaxCount As Long, HVPinCount As Long, mode As Long) As Long

Declare Function terM9_getHighVoltagePinGroup Lib "terM9_32.dll" (ByVal vi As Long, ByVal HVPinMaxCount As Long, HVPinCount As Long, mode As Long) As Long

Declare Function terM9_getHighVoltagePinMode Lib "terM9_32.dll" (ByVal vi As Long, ByVal HVPinIdx As Long, mode As Long) As Long

Declare Function terM9_getHighVoltagePinGroupThreshold Lib "terM9_32.dll" (ByVal vi As Long, threshold As Double) As Long

Declare Function terM9_setHighVoltagePinGroupSettings Lib "terM9_32.dll" (ByVal vi As Long, ByVal threshold As Double, ByVal HVPinCount As Long, mode As Long) As Long

Declare Function terM9_setHighVoltagePinGroup Lib "terM9_32.dll" (ByVal vi As Long, ByVal HVPinCount As Long, mode As Long) As Long

Declare Function terM9_setHighVoltagePinMode Lib "terM9_32.dll" (ByVal vi As Long, ByVal HVPinIdx As Long, ByVal mode As Long) As Long

Declare Function terM9_setHighVoltagePinGroupThreshold Lib "terM9_32.dll" (ByVal vi As Long, ByVal threshold As Double) As Long

Declare Function terM9_getFrontPanelSyncResource Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, logicSelect As Long, pulseGenIdx As Long, connect As Long) As Long

Declare Function terM9_getFrontPanelSyncLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, logicSelect As Long) As Long

Declare Function terM9_getFrontPanelSyncPulseSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, pulseGenIdx As Long) As Long

Declare Function terM9_getFrontPanelSyncConnect Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, connect As Long) As Long

Declare Function terM9_setFrontPanelSyncResource Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, ByVal logicSelect As Long, ByVal pulseGenIdx As Long, ByVal connect As Long) As Long

Declare Function terM9_setFrontPanelSyncLogicSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, ByVal logicSelect As Long) As Long

Declare Function terM9_setFrontPanelSyncPulseSelect Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, ByVal pulseGenIdx As Long) As Long

Declare Function terM9_setFrontPanelSyncConnect Lib "terM9_32.dll" (ByVal vi As Long, ByVal syncIdx As Long, ByVal connect As Long) As Long

Declare Function terM9_getSyncResourcePulseWidth Lib "terM9_32.dll" (ByVal vi As Long, ByVal pulseGenIdx As Long, pulseWidth As Double) As Long

Declare Function terM9_setSyncResourcePulseWidth Lib "terM9_32.dll" (ByVal vi As Long, ByVal pulseGenIdx As Long, ByVal pulseWidth As Double) As Long

Declare Function terM9_getUserClock Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, connect As Long, highVoltageLevel As Double, lowVoltageLevel As Double, mode As Long, ByVal phaseEntryMaxCount As Long, phaseEntryCount As Long, phaseEntry As Double, trigger As Long) As Long

Declare Function terM9_getUserClockConnect Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, connect As Long) As Long

Declare Function terM9_getUserClockMode Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, mode As Long) As Long

Declare Function terM9_getUserClockPhase Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal phaseEntryMaxCount As Long, phaseEntryCount As Long, phaseEntry As Double) As Long

Declare Function terM9_getUserClockPhaseAssert Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal PhaseIdx As Long, assertTime As Double) As Long

Declare Function terM9_getUserClockPhaseReturn Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal PhaseIdx As Long, returnTime As Double) As Long

Declare Function terM9_getUserClockTrigger Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, trigger As Long) As Long

Declare Function terM9_getUserClockHighVoltageLevel Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, highVoltageLevel As Double) As Long

Declare Function terM9_getUserClockLowVoltageLevel Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, lowVoltageLevel As Double) As Long

Declare Function terM9_setUserClock Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal connect As Long, ByVal highVoltageLevel As Double, ByVal lowVoltageLevel As Double, ByVal mode As Long, ByVal phaseEntryCount As Long, phaseEntry As Double, ByVal trigger As Long) As Long

Declare Function terM9_setUserClockConnect Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal connect As Long) As Long

Declare Function terM9_setUserClockMode Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal mode As Long) As Long

Declare Function terM9_setUserClockPhase Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal phaseEntryCount As Long, phaseEntry As Double) As Long

Declare Function terM9_setUserClockPhaseAssert Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal PhaseIdx As Long, ByVal assertTime As Double) As Long

Declare Function terM9_setUserClockPhaseReturn Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal PhaseIdx As Long, ByVal returnTime As Double) As Long

Declare Function terM9_setUserClockTrigger Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal trigger As Long) As Long

Declare Function terM9_setUserClockHighVoltageLevel Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal highVoltageLevel As Double) As Long

Declare Function terM9_setUserClockLowVoltageLevel Lib "terM9_32.dll" (ByVal vi As Long, ByVal userClockIdx As Long, ByVal lowVoltageLevel As Double) As Long

Declare Function terM9_getSystemEnable Lib "terM9_32.dll" (ByVal vi As Long, enable As Integer) As Long

Declare Function terM9_setSystemEnable Lib "terM9_32.dll" (ByVal vi As Long, ByVal enable As Integer) As Long

Declare Function terM9_getDynamicPatternSetTimeout Lib "terM9_32.dll" (ByVal vi As Long, timeout As Double) As Long

Declare Function terM9_setDynamicPatternSetTimeout Lib "terM9_32.dll" (ByVal vi As Long, ByVal timeout As Double) As Long

Declare Function terM9_getStaticPatternDelay Lib "terM9_32.dll" (ByVal vi As Long, Delay As Double) As Long

Declare Function terM9_setStaticPatternDelay Lib "terM9_32.dll" (ByVal vi As Long, ByVal Delay As Double) As Long

Declare Function terM9_getGroundReference Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, groundReference As Long) As Long

Declare Function terM9_setGroundReference Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal groundReference As Long) As Long

Declare Function terM9_getSystemTimingMode Lib "terM9_32.dll" (ByVal vi As Long, timingMode As Long) As Long

Declare Function terM9_setSystemTimingMode Lib "terM9_32.dll" (ByVal vi As Long, ByVal timingMode As Long) As Long

Declare Function terM9_getLowPower Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, state As Integer) As Long

Declare Function terM9_setLowPower Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, ByVal state As Integer) As Long

Declare Function terM9_getDMTXToPinConnect Lib "terM9_32.dll" (ByVal vi As Long, scopeIdx As Long) As Long

Declare Function terM9_setDMTXToPinConnect Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long) As Long

Declare Function terM9_runDynamicPatternSet Lib "terM9_32.dll" (ByVal vi As Long, ByVal expandLoops As Integer, patternSetResult As Long) As Long

Declare Function terM9_prepareDynamicPatternSet Lib "terM9_32.dll" (ByVal vi As Long)

Declare Function terM9_prepareDynamicPatternSetExecution Lib "terM9_32.dll" (ByVal vi As Long, ByVal expandLoops As Integer, ByVal stopOnFail As Integer, ByVal stopOnEvent As Integer, ByVal stopOnProbe As Integer, ByVal stopOnLSEQ As Integer, ByVal stopOnPattern As Integer, ByVal stopOnPatternIdx As Long, ByVal learnFlag As Long, ByVal preCountFlag As Long, ByVal preCount As Long, ByVal postCount As Long) As Long

Declare Function terM9_initiateDynamicPatternSetExecution Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_waitForDynamicPatternSetExecution Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_stopDynamicPatternSetExecution Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_runStaticPattern Lib "terM9_32.dll" (ByVal vi As Long, ByVal step As Integer, ByVal test As Integer) As Long

Declare Function terM9_fetchChannelSignature Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, SIG As Long) As Long

Declare Function terM9_fetchChannelState Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, detectorState As Long, deskewRelay As Long, overTemperature As Integer, referenceOverload As Integer) As Long

Declare Function terM9_fetchPatternClutchState Lib "terM9_32.dll" (ByVal vi As Long, state As Long) As Long

Declare Function terM9_fetchSystemClutchState Lib "terM9_32.dll" (ByVal vi As Long, state As Long) As Long

Declare Function terM9_fetchHighVoltagePinGroupState Lib "terM9_32.dll" (ByVal vi As Long, ByVal stateMaxCount As Long, stateCount As Long, state As Long) As Long

Declare Function terM9_fetchHighVoltagePinState Lib "terM9_32.dll" (ByVal vi As Long, ByVal HVPinIdx As Long, state As Long) As Long

Declare Function terM9_fetchDynamicPatternSetResult Lib "terM9_32.dll" (ByVal vi As Long, patternSetResult As Long, resultCount As Long, lastResultIdx As Long) As Long

Declare Function terM9_fetchDynamicPatternSetState Lib "terM9_32.dll" (ByVal vi As Long, loopState As Long, runState As Long, learnedPastLimit As Integer, lastPatternValid As Integer, stoppedOnFail As Integer, stoppedOnEvent As Integer, stoppedOnProbe As Integer, stoppedOnLSEQHalt As Integer, stoppedOnPattern As Integer, stoppedOnPatternIdx As Long) As Long

Declare Function terM9_fetchDynamicFailures Lib "terM9_32.dll" (ByVal vi As Long, ByVal resultIdx As Long, ByVal pinListMaxCount As Long, pinListCount As Long, pinList As Long, pinOpcodeList As Long) As Long

Declare Function terM9_fetchDynamicPatternResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal initResultIdx As Long, ByVal resultCount As Long, resultsCount As Long, results As Long, patternIndexes As Long) As Long

Declare Function terM9_fetchPassingDynamicPatternResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal initResultIdx As Long, ByVal resultCount As Long, resultIndexesCount As Long, resultIndexes As Long, patternIndexes As Long) As Long

Declare Function terM9_fetchFailingDynamicPatternResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal initResultIdx As Long, ByVal resultCount As Long, resultIndexesCount As Long, resultIndexes As Long, patternIndexes As Long) As Long

Declare Function terM9_fetchDynamicPinResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal initResultIdx As Long, ByVal resultCount As Long, resultListCount As Long, resultList As Long) As Long

Declare Function terM9_fetchPassingDynamicPinResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal initResultIdx As Long, ByVal resultCount As Long, passingPinListCount As Long, passingPinList As Long) As Long

Declare Function terM9_fetchFailingDynamicPinResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, ByVal initResultIdx As Long, ByVal resultCount As Long, failingPinListCount As Long, failingPinList As Long) As Long

Declare Function terM9_fetchStaticPatternResult Lib "terM9_32.dll" (ByVal vi As Long, result As Long) As Long

Declare Function terM9_fetchStaticFailures Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListMaxCount As Long, pinListCount As Long, pinList As Long, pinStateList As Long) As Long

Declare Function terM9_fetchStaticPinResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, resultListCount As Long, resultList As Long) As Long

Declare Function terM9_fetchPassingStaticPinResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, passingPinListCount As Long, passingPinList As Long) As Long

Declare Function terM9_fetchFailingStaticPinResults Lib "terM9_32.dll" (ByVal vi As Long, ByVal pinListCount As Long, pinList As Long, failingPinListCount As Long, failingPinList As Long) As Long

Declare Function terM9_fetchProbeDetectData Lib "terM9_32.dll" (ByVal vi As Long, ByVal initStepIdx As Long, ByVal stepCount As Long, detectData As Long) As Long

Declare Function terM9_fetchProbeMapData Lib "terM9_32.dll" (ByVal vi As Long, ByVal mapDataMaxCount As Long, mapDataCount As Long, mapData As Long) As Long

Declare Function terM9_fetchProbeSettings Lib "terM9_32.dll" (ByVal vi As Long, probeMode As Long, probeConfig As Long, staticCapture As Long, executeMode As Long) As Long

Declare Function terM9_prepareDynamicProbeExecution Lib "terM9_32.dll" (ByVal vi As Long, ByVal executeMode As Long) As Long

Declare Function terM9_fetchDynamicProbeResult Lib "terM9_32.dll" (ByVal vi As Long, result As Long) As Long

Declare Function terM9_fetchDynamicProbeFailingDatatype Lib "terM9_32.dll" (ByVal vi As Long, failingDataType As Long) As Long

Declare Function terM9_fetchDynamicProbeFailingStep Lib "terM9_32.dll" (ByVal vi As Long, failingStep As Long) As Long

Declare Function terM9_fetchDynamicProbePatternCount Lib "terM9_32.dll" (ByVal vi As Long, patternCount As Long) As Long

Declare Function terM9_fetchDynamicProbeStepCount Lib "terM9_32.dll" (ByVal vi As Long, stepCount As Long) As Long

Declare Function terM9_prepareStaticProbeExecution Lib "terM9_32.dll" (ByVal vi As Long, ByVal executeMode As Long, ByVal staticCapture As Long) As Long

Declare Function terM9_fetchStaticProbeResult Lib "terM9_32.dll" (ByVal vi As Long, result As Long) As Long

Declare Function terM9_fetchStaticProbeFailingDatatype Lib "terM9_32.dll" (ByVal vi As Long, failingDataType As Long) As Long

Declare Function terM9_fetchStaticProbeFailingStep Lib "terM9_32.dll" (ByVal vi As Long, failingStep As Long) As Long

Declare Function terM9_fetchStaticProbePatternCount Lib "terM9_32.dll" (ByVal vi As Long, patternCount As Long) As Long

Declare Function terM9_fetchStaticProbeStepCount Lib "terM9_32.dll" (ByVal vi As Long, stepCount As Long) As Long

Declare Function terM9_stopForFailure Lib "terM9_32.dll" (ByVal vi As Long, ByVal stopForFailure As Integer) As Long

Declare Function terM9_stopAtPattern Lib "terM9_32.dll" (ByVal vi As Long, ByVal patternIdx As Long, ByVal stopAtPattern As Integer) As Long

Declare Function terM9_selfTest Lib "terM9_32.dll" (ByVal vi As Long, testResult As Long, ByVal testMessage As String) As Long
Declare Function terM9_self_test Lib "terM9_32.dll" (ByVal vi As Long, testResult As Long, ByVal testMessage As String) As Long

Declare Function terM9_executeDiagnosticSelfTest Lib "terM9_32.dll" (ByVal vi As Long, testResult As Long, ByVal testMessage As String) As Long

Declare Function terM9_errorQuery Lib "terM9_32.dll" (ByVal vi As Long, errorcode As Long, ByVal errorMessage As String) As Long
Declare Function terM9_error_query Lib "terM9_32.dll" (ByVal vi As Long, errorcode As Long, ByVal errorMessage As String) As Long

Declare Function terM9_errorMessage Lib "terM9_32.dll" (ByVal vi As Long, ByVal statusCode As Long, ByVal Message As String) As Long
Declare Function terM9_error_message Lib "terM9_32.dll" (ByVal vi As Long, ByVal statusCode As Long, ByVal Message As String) As Long

Declare Function terM9_revisionQuery Lib "terM9_32.dll" (ByVal vi As Long, ByVal driverRev As String, ByVal instrRev As String) As Long
Declare Function terM9_revision_query Lib "terM9_32.dll" (ByVal vi As Long, ByVal driverRev As String, ByVal instrRev As String) As Long

Declare Function terM9_initializeInstrument Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_reset Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_resetHighVoltagePins Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_resetProbe Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_executeSystemAlignment Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long) As Long

Declare Function terM9_executeChannelAlignment Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long) As Long

Declare Function terM9_executeSyncResourceAlignment Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long) As Long

Declare Function terM9_executeUserClockAlignment Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long) As Long

Declare Function terM9_executeProbeAlignment Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long) As Long

Declare Function terM9_fetchSystemAlignmentResult Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long, ByVal alignmentMessage As String) As Long

Declare Function terM9_fetchChannelAlignmentResult Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long, ByVal alignmentMessage As String) As Long

Declare Function terM9_fetchSyncResourceAlignmentResult Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long, ByVal alignmentMessage As String) As Long

Declare Function terM9_fetchUserClockAlignmentResult Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long, ByVal alignmentMessage As String) As Long

Declare Function terM9_fetchProbeAlignmentResult Lib "terM9_32.dll" (ByVal vi As Long, alignmentResult As Long, ByVal alignmentMessage As String) As Long

Declare Function terM9_getSystemInformation Lib "terM9_32.dll" (ByVal vi As Long, ByVal systemDesc As String, cardCount As Long, channelCount As Long) As Long

Declare Function terM9_getCardInformation Lib "terM9_32.dll" (ByVal vi As Long, ByVal cardIdx As Long, cardId As Long, ByVal cardName As String, boardCount As Long, chanCount As Long, chassis As Long, slot As Long, ByVal addlInfo As String) As Long

Declare Function terM9_getBoardInformation Lib "terM9_32.dll" (ByVal vi As Long, ByVal cardIdx As Long, ByVal boardIdx As Long, boardId As Long, ByVal boardNum As String, ByVal boardName As String, ByVal boardRev As String, ByVal boardSerial As String, ByVal addlInfo As String) As Long

Declare Function terM9_getChannelInformation Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, cardIdx As Long, ChannelNumber As Long, channelIdx As Long) As Long

Declare Function terM9_getLastPatternIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, lastPatternIdx As Long) As Long

Declare Function terM9_getLastResultIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, lastResultIdx As Long) As Long

Declare Function terM9_getLastTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, lastTSETIdx As Long) As Long

Declare Function terM9_getLastPhaseIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, lastPhaseIdx As Long) As Long

Declare Function terM9_getLastWindowIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, lastWindowIdx As Long) As Long

Declare Function terM9_getLastLevelIndex Lib "terM9_32.dll" (ByVal vi As Long, ByVal scopeIdx As Long, lastLevelIdx As Long) As Long

Declare Function terM9_getLastProbeTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Long, lastPSETIdx As Long) As Long

Declare Function terM9_getLastFrontPanelSyncResourceIndex Lib "terM9_32.dll" (ByVal vi As Long, lastSyncResourceIdx As Long) As Long

Declare Function terM9_getLastStepIndex Lib "terM9_32.dll" (ByVal vi As Long, lastStepIdx As Long) As Long

Declare Function terM9_getLastHighVoltagePinIndex Lib "terM9_32.dll" (ByVal vi As Long, lastHVPinIdx As Long) As Long

Declare Function terM9_getLastUserClockIndex Lib "terM9_32.dll" (ByVal vi As Long, lastUserClockIdx As Long) As Long

Declare Function terM9_getInterruptHandler Lib "terM9_32.dll" (ByVal vi As Long, interruptHandler As Long) As Long

Declare Function terM9_setInterruptHandler Lib "terM9_32.dll" (ByVal vi As Long, ByVal interruptHandler As Long) As Long

Declare Function terM9_wait Lib "terM9_32.dll" (ByVal vi As Long, ByVal waittime As Double) As Long

Declare Function terM9_setTimer Lib "terM9_32.dll" (ByVal vi As Long, ByVal waittime As Double) As Long

Declare Function terM9_startTimer Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_waitForTimer Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_testTimer Lib "terM9_32.dll" (ByVal vi As Long, timerDone As Integer) As Long

Declare Function terM9_stopTimer Lib "terM9_32.dll" (ByVal vi As Long) As Long

Declare Function terM9_close Lib "terM9_32.dll" (ByVal vi As Long) As Long

' terM9_errors.h

Global Const TERM9_ERROR_FIRST = &HBFFC0800
Global Const TERM9_ERROR_ADC_CONNECT = &HBFFC0800
Global Const TERM9_ERROR_AMMTR_MODE = &HBFFC0801
Global Const TERM9_ERROR_BOARDIDX = &HBFFC0802
Global Const TERM9_ERROR_PATTERNSET_TIMEOUT = &HBFFC0803
Global Const TERM9_ERROR_CARDIDX = &HBFFC0804
Global Const TERM9_ERROR_CCOND = &HBFFC0805
Global Const TERM9_ERROR_CCOUNT = &HBFFC0806
Global Const TERM9_ERROR_CHAN_CAPTURE = &HBFFC0807
Global Const TERM9_ERROR_CHAN_CHANMODE = &HBFFC0808
Global Const TERM9_ERROR_CHAN_CONNECT = &HBFFC0809
Global Const TERM9_ERROR_CHAN_FORMAT = &HBFFC080A
Global Const TERM9_ERROR_CHAN_IMPEDANCE = &HBFFC080B
Global Const TERM9_ERROR_CHAN_LEVELIDX = &HBFFC080C
Global Const TERM9_ERROR_CHAN_LOAD = &HBFFC080D
Global Const TERM9_ERROR_CHAN_PHASEIDX = &HBFFC080E
Global Const TERM9_ERROR_CHAN_WINDOWIDX = &HBFFC080F
Global Const TERM9_ERROR_CLK_DIVIDER = &HBFFC0810
Global Const TERM9_ERROR_CLK_EDGE = &HBFFC0811
Global Const TERM9_ERROR_CLK_MODE = &HBFFC0812
Global Const TERM9_ERROR_CLK_OUTPATH = &HBFFC0813
Global Const TERM9_ERROR_CLK_SOURCE = &HBFFC0814
Global Const TERM9_ERROR_CLUTCH_ENABLE = &HBFFC0815
Global Const TERM9_ERROR_CLUTCH_POLARITY = &HBFFC0816
Global Const TERM9_ERROR_COPCODE = &HBFFC0817
Global Const TERM9_ERROR_CPP = &HBFFC0818
Global Const TERM9_ERROR_EXPANDLOOPS = &HBFFC0819
Global Const TERM9_ERROR_FAIL_CAPTURE = &HBFFC081A
Global Const TERM9_ERROR_FILE_OPEN = &HBFFC081B
Global Const TERM9_ERROR_FILE_PROC = &HBFFC081C
Global Const TERM9_ERROR_GROUND_REFERENCE = &HBFFC081D
Global Const TERM9_ERROR_GROUPSTATE = &HBFFC081E
Global Const TERM9_ERROR_HVPIN_COUNT = &HBFFC081F
Global Const TERM9_ERROR_HVPIN_MODE = &HBFFC0820
Global Const TERM9_ERROR_HVPIN_THRESHOLD = &HBFFC0821
Global Const TERM9_ERROR_HVPINIDX = &HBFFC0822
Global Const TERM9_ERROR_IOH = &HBFFC0823
Global Const TERM9_ERROR_IOL = &HBFFC0824
Global Const TERM9_ERROR_LEARN_FLAG = &HBFFC0825
Global Const TERM9_ERROR_LEVELIDX = &HBFFC0826
Global Const TERM9_ERROR_LOGIC_SELECT = &HBFFC0827
Global Const TERM9_ERROR_LOWPOWER = &HBFFC0828
Global Const TERM9_ERROR_MAPTYPE = &HBFFC0829
Global Const TERM9_ERROR_MAX_PATTERNSET_TIMEOUT = &HBFFC082A
Global Const TERM9_ERROR_MTR_CURRENT_OVERLOAD = &HBFFC082B
Global Const TERM9_ERROR_MTR_CURRENT_RANGE = &HBFFC082C
Global Const TERM9_ERROR_MTR_FORCE_CURRENT = &HBFFC082D
Global Const TERM9_ERROR_MTR_INPUT = &HBFFC082E
Global Const TERM9_ERROR_MTR_IRNG = &HBFFC082F
Global Const TERM9_ERROR_MTR_NEG_CLMP_VOLT = &HBFFC0830
Global Const TERM9_ERROR_MTR_POS_CLMP_VOLT = &HBFFC0831
Global Const TERM9_ERROR_MTR_VOLTAGE_RANGE = &HBFFC0832
Global Const TERM9_ERROR_NOT_ENOUGH_DATA = &HBFFC0833
Global Const TERM9_ERROR_OVER_TEMP = &HBFFC0834
Global Const TERM9_ERROR_PATTERN_COUNT = &HBFFC0835
Global Const TERM9_ERROR_PATTERN_DELAY = &HBFFC0836
Global Const TERM9_ERROR_PATTERNIDX = &HBFFC0837
Global Const TERM9_ERROR_PHASE_FREERUN = &HBFFC0838
Global Const TERM9_ERROR_PHASE_TRIGGER = &HBFFC0839
Global Const TERM9_ERROR_PHASEIDX = &HBFFC083A
Global Const TERM9_ERROR_PINOPCODE = &HBFFC083B
Global Const TERM9_ERROR_POSTCOUNT = &HBFFC083C
Global Const TERM9_ERROR_PROBE_METHOD = &HBFFC083D
Global Const TERM9_ERROR_PRECOUNT = &HBFFC083E
Global Const TERM9_ERROR_PRECOUNT_FLAG = &HBFFC083F
Global Const TERM9_ERROR_PRHI = &HBFFC0840
Global Const TERM9_ERROR_PRLO = &HBFFC0841
Global Const TERM9_ERROR_PROBE_CAPTURE = &HBFFC0842
Global Const TERM9_ERROR_PROBE_CLOSE = &HBFFC0843
Global Const TERM9_ERROR_PROBE_CONFIG = &HBFFC0844
Global Const TERM9_ERROR_PROBE_EXECUTE = &HBFFC0845
Global Const TERM9_ERROR_PROBE_MODE = &HBFFC0846
Global Const TERM9_ERROR_PROBE_STATCAPTURE = &HBFFC0847
Global Const TERM9_ERROR_PROBE_OPEN = &HBFFC0848
Global Const TERM9_ERROR_PSETIDX = &HBFFC0849
Global Const TERM9_ERROR_PSTACK_UNDERFLOW = &HBFFC084A
Global Const TERM9_ERROR_PSTACK_OVERFLOW = &HBFFC084B
Global Const TERM9_ERROR_REF_OVERLOAD = &HBFFC084C
Global Const TERM9_ERROR_REGISTER_READ = &HBFFC084D
Global Const TERM9_ERROR_REGISTER_WRITE = &HBFFC084E
Global Const TERM9_ERROR_RESULT_CAPTURE = &HBFFC084F
Global Const TERM9_ERROR_RESULT_COUNT = &HBFFC0850
Global Const TERM9_ERROR_RESULTIDX = &HBFFC0851
Global Const TERM9_ERROR_SCOPE_CHAN = &HBFFC0852
Global Const TERM9_ERROR_SCOPE_LEVELS = &HBFFC0853
Global Const TERM9_ERROR_SCOPE_DMTX = &HBFFC0854
Global Const TERM9_ERROR_SCOPE_LOWPOWER = &HBFFC0855
Global Const TERM9_ERROR_SCOPE_PHASE = &HBFFC0856
Global Const TERM9_ERROR_SCOPE_TSET = &HBFFC0857
Global Const TERM9_ERROR_SCOPEIDX = &HBFFC0858
Global Const TERM9_ERROR_SLEWRATE = &HBFFC0859
Global Const TERM9_ERROR_STEP = &HBFFC085A
Global Const TERM9_ERROR_STEP_COUNT = &HBFFC085B
Global Const TERM9_ERROR_STEP_ONLY = &HBFFC085C
Global Const TERM9_ERROR_STEPIDX = &HBFFC085D
Global Const TERM9_ERROR_STMTX_CONNECT = &HBFFC085E
Global Const TERM9_ERROR_STMTX_FP_CONNECT = &HBFFC085F
Global Const TERM9_ERROR_STMTX_GLOBAL = &HBFFC0860
Global Const TERM9_ERROR_STOP = &HBFFC0861
Global Const TERM9_ERROR_STOP_ON_EVENT = &HBFFC0862
Global Const TERM9_ERROR_STOP_ON_FAIL = &HBFFC0863
Global Const TERM9_ERROR_STOP_ON_LSEQ = &HBFFC0864
Global Const TERM9_ERROR_STOP_ON_PATTERN = &HBFFC0865
Global Const TERM9_ERROR_STOP_ON_PATT_IDX = &HBFFC0866
Global Const TERM9_ERROR_STOP_ON_PROBE = &HBFFC0867
Global Const TERM9_ERROR_SYNC_PULSEGEN_WIDTH = &HBFFC0868
Global Const TERM9_ERROR_SYNC_PULSEGENIDX = &HBFFC0869
Global Const TERM9_ERROR_SYNC_STATE = &HBFFC086A
Global Const TERM9_ERROR_SYNCIDX = &HBFFC086B
Global Const TERM9_ERROR_SYSTEM_ENABLE = &HBFFC086C
Global Const TERM9_ERROR_TEST = &HBFFC086D
Global Const TERM9_ERROR_TIMER_START = &HBFFC086E
Global Const TERM9_ERROR_TIMING_MODE = &HBFFC086F
Global Const TERM9_ERROR_TSET_CLOCK = &HBFFC0870
Global Const TERM9_ERROR_TSET_PHS_ASSERT = &HBFFC0871
Global Const TERM9_ERROR_TSET_PHS_COUNT = &HBFFC0872
Global Const TERM9_ERROR_TSET_PHS_RETURN = &HBFFC0873
Global Const TERM9_ERROR_TSET_WDW_CLOSE = &HBFFC0874
Global Const TERM9_ERROR_TSET_WDW_COUNT = &HBFFC0875
Global Const TERM9_ERROR_TSET_WDW_OPEN = &HBFFC0876
Global Const TERM9_ERROR_TSETIDX = &HBFFC0877
Global Const TERM9_ERROR_UCLK_CONNECT = &HBFFC0878
Global Const TERM9_ERROR_UCLK_HIGHV = &HBFFC0879
Global Const TERM9_ERROR_UCLK_IMPEDANCE = &HBFFC087A
Global Const TERM9_ERROR_UCLK_LOWV = &HBFFC087B
Global Const TERM9_ERROR_UCLK_MODE = &HBFFC087C
Global Const TERM9_ERROR_UCLK_PHS_ASSERT = &HBFFC087D
Global Const TERM9_ERROR_UCLK_PHS_COUNT = &HBFFC087E
Global Const TERM9_ERROR_UCLK_PHS_RETURN = &HBFFC087F
Global Const TERM9_ERROR_UCLK_TRIGGER = &HBFFC0880
Global Const TERM9_ERROR_UCLKIDX = &HBFFC0881
Global Const TERM9_ERROR_VCOM = &HBFFC0882
Global Const TERM9_ERROR_VIH = &HBFFC0883
Global Const TERM9_ERROR_VIL = &HBFFC0884
Global Const TERM9_ERROR_VOH = &HBFFC0885
Global Const TERM9_ERROR_VOL = &HBFFC0886
Global Const TERM9_ERROR_VOLTAGE_SWING = &HBFFC0887
Global Const TERM9_ERROR_VXI_ACFAIL = &HBFFC0888
Global Const TERM9_ERROR_VXI_BUSERROR = &HBFFC0889
Global Const TERM9_ERROR_VXI_MAPERROR = &HBFFC088A
Global Const TERM9_ERROR_VXI_SYSFAIL = &HBFFC088B
Global Const TERM9_ERROR_VXI_SYSRESET = &HBFFC088C
Global Const TERM9_ERROR_WINDOWIDX = &HBFFC088D
Global Const TERM9_LIC_EDGE_RES_1NS = &HBFFC088E
Global Const TERM9_LIC_EDGE_RES_2NS = &HBFFC088F
Global Const TERM9_LIC_EDGE_RES_5NS = &HBFFC0890
Global Const TERM9_LIC_EDGE_RES_10NS = &HBFFC0891
Global Const TERM9_LIC_FORMAT_NRET = &HBFFC0892
Global Const TERM9_LIC_FORMAT_STANDARD = &HBFFC0893
Global Const TERM9_LIC_LEVPRG_SYS = &HBFFC0894
Global Const TERM9_LIC_LEVSEL_CARD = &HBFFC0895
Global Const TERM9_LIC_LEVSEL_GRP = &HBFFC0896
Global Const TERM9_LIC_RATE_25MHZ = &HBFFC0897
Global Const TERM9_LIC_RATE_50MHZ = &HBFFC0898
Global Const TERM9_LIC_TIMPRG_SYS = &HBFFC0899
Global Const TERM9_LIC_TIMSEL_CARD = &HBFFC089A
Global Const TERM9_LIC_TIMSEL_GRP = &HBFFC089B
Global Const TERM9_NOLIC_HVPINS = &HBFFC089C
Global Const TERM9_NOLIC_LOAD = &HBFFC089D
Global Const TERM9_NOLIC_PROBE = &HBFFC089E
Global Const TERM9_NOLIC_SYNCRES = &HBFFC089F
Global Const TERM9_NOLIC_USERCLOCK = &HBFFC08A0
Global Const TERM9_WARN_HOT_SWITCH = &HBFFC08A1
Global Const TERM9_WARN_RESIDUAL_CONN = &HBFFC08A2
Global Const TERM9_ERROR_PATSETIDX = &HBFFC08A3
Global Const TERM9_ERROR_SESSION = &HBFFC08A4
Global Const TERM9_ERROR_INIT = &HBFFC08A5
Global Const TERM9_ERROR_MEMORY_ALLOCATION = &HBFFC08A6
Global Const TERM9_WARN_SOFTWARE_SIM = &HBFFC08A7
Global Const TERM9_ERROR_GROUPOPCODE = &HBFFC08A8
Global Const TERM9_ERROR_ENABLE = &HBFFC08A9
Global Const TERM9_ERROR_TEST_HANDLE = &HBFFC08AA
Global Const TERM9_ERROR_TEST_PROC = &HBFFC08AB
Global Const TERM9_ERROR_SYNC_CONNECT = &HBFFC08AD
Global Const TERM9_ERROR_RESET_INSTR = &HBFFC08AE
Global Const TERM9_ERROR_ID_QUERY = &HBFFC08AF
Global Const TERM9_ERROR_TSET_COUNT = &HBFFC08B0
Global Const TERM9_ERROR_SCOPE_CARD = &HBFFC08B1
Global Const TERM9_ERROR_LAST = &HBFFC08AD

' terM9_types.h

Global Const TERM9_ATTRVAL_FIRST = 2&
Global Const TERM9_ADC_CONN_10TO10 = 2&
Global Const TERM9_ADC_CONN_12MINUS = 3&
Global Const TERM9_ADC_CONN_12PLUS = 4&
Global Const TERM9_ADC_CONN_3PLUS = 5&
Global Const TERM9_ADC_CONN_20TO20 = 6&
Global Const TERM9_ADC_CONN_24MINUS = 7&
Global Const TERM9_ADC_CONN_24PLUS = 8&
Global Const TERM9_ADC_CONN_5TO5 = 9&
Global Const TERM9_ADC_CONN_AMMETER = 10&
Global Const TERM9_ADC_CONN_PWRGND = 11&
Global Const TERM9_ADC_CONN_SIGGND = 12&
Global Const TERM9_ADC_CONN_VCC = 13&
Global Const TERM9_ADC_CONN_VEE = 14&
Global Const TERM9_ADC_CONN_VTT = 15&
Global Const TERM9_ADC_CONN_UNKNOWN = 16&
Global Const TERM9_AMMTR_MODE_FORCE = 17&
Global Const TERM9_AMMTR_MODE_MEASURE = 18&
Global Const TERM9_AMMTR_MODE_OFF = 19&
Global Const TERM9_AMMTR_MODE_SELFTEST = 20&
Global Const TERM9_AMMTR_MODE_UNKNOWN = 21&
Global Const TERM9_PATTERNSET_LEARN_FAILS = 22&
Global Const TERM9_PATTERNSET_LEARN_PATS = 23&
Global Const TERM9_PATTERNSET_PRECOUNT_FAILS = 24&
Global Const TERM9_PATTERNSET_PRECOUNT_PATS = 25&
Global Const TERM9_CAPTURE_CLOSE = 26&
Global Const TERM9_CAPTURE_OPEN = 27&
Global Const TERM9_CAPTURE_WINDOW = 28&
Global Const TERM9_CHANMODE_DYNAMIC = 29&
Global Const TERM9_CHANMODE_STATIC = 30&
Global Const TERM9_CLUTCH_INPUT_HIGH = 31&
Global Const TERM9_CLUTCH_SIGNAL_HIGH = 32&
Global Const TERM9_CLUTCH_INPUT_LOW = 33&
Global Const TERM9_CLUTCH_SIGNAL_LOW = 34&
Global Const TERM9_COND_ALWAYS = 35&
Global Const TERM9_COND_TRUE = 35&
Global Const TERM9_COND_EX1 = 36&
Global Const TERM9_COND_EXT1 = 36&
Global Const TERM9_COND_EX2 = 37&
Global Const TERM9_COND_EXT2 = 37&
Global Const TERM9_COND_FAIL = 38&
Global Const TERM9_COND_NEVER = 39&
Global Const TERM9_COND_FALSE = 39&
Global Const TERM9_COND_PASS = 40&
Global Const TERM9_COND_SYNC = 41&
Global Const TERM9_COND_NONE = 42&
Global Const TERM9_COP_CLOOP = 43&
Global Const TERM9_COP_CREPEAT = 44&
Global Const TERM9_COP_ENDLOOP = 45&
Global Const TERM9_COP_HALT = 46&
Global Const TERM9_COP_JUMP = 47&
Global Const TERM9_COP_LOOP = 48&
Global Const TERM9_COP_NOP = 49&
Global Const TERM9_COP_REPEAT = 50&
Global Const TERM9_DETECTOR_BETWEEN = 51&
Global Const TERM9_DETECTOR_HIGH = 52&
Global Const TERM9_DETECTOR_LOW = 53&
Global Const TERM9_DETECTOR_UNKNOWN = 54&
Global Const TERM9_EDGE_FALLING = 55&
Global Const TERM9_EDGE_RISING = 56&
Global Const TERM9_EXECUTE_COMPARE = 57&
Global Const TERM9_EXECUTE_LEARN = 58&
Global Const TERM9_FORMAT_COMPS = 59&
Global Const TERM9_FORMAT_NRET = 60&
Global Const TERM9_FORMAT_RCOMP = 61&
Global Const TERM9_FORMAT_ROFF = 62&
Global Const TERM9_FORMAT_RONE = 63&
Global Const TERM9_FORMAT_RZERO = 64&
Global Const TERM9_GROUND_EXTERNAL = 65&
Global Const TERM9_GROUND_INTERNAL = 66&
Global Const TERM9_IMPED_50OHM = 67&
Global Const TERM9_IMPED_LOWZ = 68&
Global Const TERM9_LOAD_OFF = 69&
Global Const TERM9_LOAD_ON = 70&
Global Const TERM9_LOGIC_ECL = 71&
Global Const TERM9_LOGIC_TTL = 72&
Global Const TERM9_LOOP_OK = 73&
Global Const TERM9_LOOP_OVERFLOW = 74&
Global Const TERM9_LOOP_UNDERFLOW = 75&
Global Const TERM9_CLOCKMODE_OFF = 76&
Global Const TERM9_HVPINMODE_OFF = 77&
Global Const TERM9_UCLKMODE_OFF = 78&
Global Const TERM9_CLOCKMODE_ON = 79&
Global Const TERM9_HVPINMODE_ON = 80&
Global Const TERM9_UCLKMODE_ON = 81&
Global Const TERM9_MTR_INPUT_NONE = 82&
Global Const TERM9_MTR_INPUT_PANEL = 83&
Global Const TERM9_MTR_INPUT_STMTX = 84&
Global Const TERM9_MTR_INPUT_UNKNOWN = 85&
Global Const TERM9_MTR_IRNG_100MA = 86&
Global Const TERM9_MTR_IRNG_10MA = 87&
Global Const TERM9_MTR_IRNG_UNKNOWN = 88&
Global Const TERM9_MTR_MODE_FORCE = 89&
Global Const TERM9_MTR_MODE_MEASURE = 90&
Global Const TERM9_MTR_MODE_OFF = 91&
Global Const TERM9_MTR_MODE_SELFTEST = 92&
Global Const TERM9_MTR_MODE_UNKNOWN = 93&
Global Const TERM9_MTR_VRNG_10TO10 = 94&
Global Const TERM9_MTR_VRNG_20TO20 = 95&
Global Const TERM9_MTR_VRNG_5TO5 = 96&
Global Const TERM9_MTR_VRNG_OFF = 97&
Global Const TERM9_OP_SIG = 98&
Global Const TERM9_OP_CRC = 98&
Global Const TERM9_OP_IH = 99&
Global Const TERM9_OP_IHOL = 100&
Global Const TERM9_OP_IL = 101&
Global Const TERM9_OP_ILOH = 102&
Global Const TERM9_OP_IOX = 103&
Global Const TERM9_OP_IX = 104&
Global Const TERM9_OP_KEEP = 105&
Global Const TERM9_OP_MH = 106&
Global Const TERM9_OP_ML = 107&
Global Const TERM9_OP_MX = 108&
Global Const TERM9_OP_OB = 109&
Global Const TERM9_OP_OH = 110&
Global Const TERM9_OP_OK = 111&
Global Const TERM9_OP_OL = 112&
Global Const TERM9_OP_OX = 113&
Global Const TERM9_OP_TOG = 114&
Global Const TERM9_OP_GROUP_UNKNOWN = 115&
Global Const TERM9_OP_UNKNOWN = 116&
Global Const TERM9_PATH_DISABLE = 117&
Global Const TERM9_PATH_ENABLE = 118&
Global Const TERM9_PRB_B = 119&
Global Const TERM9_PRB_EH = 120&
Global Const TERM9_PRB_EL = 121&
Global Const TERM9_PRB_GB = 122&
Global Const TERM9_PRB_GH = 123&
Global Const TERM9_PRB_GL = 124&
Global Const TERM9_PRB_H = 125&
Global Const TERM9_PRB_ILLEGAL = 126&
Global Const TERM9_PRB_L = 127&
Global Const TERM9_PRB_MB = 128&
Global Const TERM9_PROBE_METHOD_HANDLE = 129&
Global Const TERM9_PRB_METHOD_HANDLE = 129&
Global Const TERM9_PROBE_METHOD_MATRIX = 130&
Global Const TERM9_PRB_METHOD_MATRIX = 130&
Global Const TERM9_PROBE_METHOD_UNKNOWN = 131&
Global Const TERM9_PRB_METHOD_UNKNOWN = 131&
Global Const TERM9_PRB_MH = 132&
Global Const TERM9_PRB_ML = 133&
Global Const TERM9_PRB_PB = 134&
Global Const TERM9_PRB_PH = 135&
Global Const TERM9_PRB_PL = 136&
Global Const TERM9_PRB_RB = 137&
Global Const TERM9_PRB_RH = 138&
Global Const TERM9_PRB_RL = 139&
Global Const TERM9_PRB_X = 140&
Global Const TERM9_PROBE_CAPTURE_STROBE = 141&
Global Const TERM9_PROBE_CAPTURE_WINDOW = 142&
Global Const TERM9_PROBE_CONFIG_LOAD = 143&
Global Const TERM9_PROBE_CONFIG_RUN = 144&
Global Const TERM9_PROBEMODE_DYNAMIC = 145&
Global Const TERM9_PROBEMODE_STATIC = 146&
Global Const TERM9_PS_CRC = 147&
Global Const TERM9_PS_SIG = 147&
Global Const TERM9_PS_IH = 148&
Global Const TERM9_PS_IHOL = 149&
Global Const TERM9_PS_IL = 150&
Global Const TERM9_PS_ILOH = 151&
Global Const TERM9_PS_IOX = 152&
Global Const TERM9_PS_MH = 153&
Global Const TERM9_PS_ML = 154&
Global Const TERM9_PS_OB = 155&
Global Const TERM9_PS_OH = 156&
Global Const TERM9_PS_OK = 157&
Global Const TERM9_PS_OL = 158&
Global Const TERM9_PS_UNKNOWN = 159&
Global Const TERM9_RELAY_CLOSED = 160&
Global Const TERM9_RELAY_OPEN = 161&
Global Const TERM9_RESULT_FAIL = 162&
Global Const TERM9_RESULT_MBT = 163&
Global Const TERM9_RESULT_NOT_RUN = 164&
Global Const TERM9_RESULT_OVERFLOW = 165&
Global Const TERM9_RESULT_PASS = 166&
Global Const TERM9_RESULT_UNDERFLOW = 167&
Global Const TERM9_RUN_IDLE = 168&
Global Const TERM9_RUN_RUNNING = 169&
Global Const TERM9_RUN_STOPPING = 170&
Global Const TERM9_SLEWRATE_HIGH = 171&
Global Const TERM9_SLEWRATE_LOW = 172&
Global Const TERM9_SLEWRATE_MEDIUM = 173&
Global Const TERM9_SOURCE_EXTERNAL = 174&
Global Const TERM9_SOURCE_INTERNAL = 175&
Global Const TERM9_STATE_HIGH = 176&
Global Const TERM9_STATE_LOW = 177&
Global Const TERM9_STMTX_GLOBAL = 178&
Global Const TERM9_STMTX_LOCAL = 179&
Global Const TERM9_SYNC_IDLE = 180&
Global Const TERM9_SYNC_LISTEN = 181&
Global Const TERM9_SYNC_TRIGGER = 182&
Global Const TERM9_TIMING_SLOW = 184&
Global Const TERM9_TIMING_STANDARD = 185&
Global Const TERM9_TRIGGER_PATCLK = 186&
Global Const TERM9_TRIGGER_SYSCLK = 187&
Global Const TERM9_UCLK_TRIGGER_EXTCLK = 188&
Global Const TERM9_UCLK_TRIGGER_SYSCLK = 189&
Global Const TERM9_EDGE_BOTH = 206&
Global Const TERM9_PATTERNSET_LEARN_UNKNOWN = 207&
Global Const TERM9_PATTERNSET_PRECOUNT_UNKNOWN = 208&
Global Const TERM9_FORMAT_UNKNOWN = 209&
Global Const TERM9_COND_UNKNOWN = 210&
Global Const TERM9_COP_UNKNOWN = 211&
Global Const TERM9_SLEWRATE_UNKNOWN = 212&
Global Const TERM9_SYNC_UNKNOWN = 213&
Global Const TERM9_ADC_CONN_PRBHNDL = 214&
Global Const TERM9_ADC_CONN_REFV = 215&
Global Const TERM9_ATTRVAL_LAST = 215&
Global Const TERM9_INT_SYSTEM_GROUND = 222&

' terM9_scope.h

Global Const TERM9_SCOPE_LAST = 65535
Global Const TERM9_SCOPE_FIRST_USER = 0&
Global Const TERM9_SCOPE_LAST_USER = 32767&
Global Const TERM9_SCOPE_FIRST_PREDEFINED = 32768
Global Const TERM9_SCOPE_LAST_PREDEFINED = 65535
Global Const TERM9_SCOPE_FIRST_UC_INDEX = 33068
Global Const TERM9_SCOPE_LAST_UC_INDEX = 33167
Global Const TERM9_SCOPE_FIRST_SR_INDEX = 33168
Global Const TERM9_SCOPE_LAST_SR_INDEX = 33267
Global Const TERM9_SCOPE_FIRST_CARD_INDEX = 32868
Global Const TERM9_SCOPE_LAST_CARD_INDEX = 33067
Global Const TERM9_SCOPE_FIRST_CHNGRP_INDEX = 33768
Global Const TERM9_SCOPE_LAST_CHNGRP_INDEX = 33967
Global Const TERM9_SCOPE_FIRST_PIN_INDEX = 33968
Global Const TERM9_SCOPE_LAST_PIN_INDEX = 65535
Global Const TERM9_SCOPE_UNDEFINED = -1&
Global Const TERM9_SCOPE_ISGROUP = -2&
Global Const TERM9_SCOPE_SYSTEM = 32768
Global Const TERM9_SCOPE_CHANSYSTEM = 32769


Function TERM9_SCOPE_CARD(ByVal n As Integer) As Long
  TERM9_SCOPE_CARD = TERM9_SCOPE_FIRST_CARD_INDEX + n
End Function

Function TERM9_SCOPE_CHAN(ByVal n As Long) As Long
  TERM9_SCOPE_CHAN = TERM9_SCOPE_FIRST_PIN_INDEX + n
End Function

Function TERM9_SCOPE_CHANCARD(ByVal n As Long) As Long
  TERM9_SCOPE_CHANCARD = TERM9_SCOPE_FIRST_CHNGRP_INDEX + n - 1
End Function

