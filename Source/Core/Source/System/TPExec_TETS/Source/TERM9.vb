Option Strict Off
Option Explicit On
Module terM9bas
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

    Declare Function terM9_init Lib "terM9_32.dll" (ByVal rsrcName As String, ByVal idQuery As Short, ByVal resetInstr As Short, ByRef vi As Integer) As Integer

    Declare Function terM9_executeDigitalTest Lib "terM9_32.dll" (ByVal vi As Integer, ByVal testFile As String, ByVal expandLoops As Short, ByRef testResult As Integer) As Integer

    Declare Function terM9_openDigitalTest Lib "terM9_32.dll" (ByVal vi As Integer, ByVal testFile As String, ByRef testHandle As Integer) As Integer

    Declare Function terM9_configureDigitalTestSetup Lib "terM9_32.dll" (ByVal vi As Integer, ByVal testHandle As Integer) As Integer

    Declare Function terM9_configureDigitalTestPatternSet Lib "terM9_32.dll" (ByVal vi As Integer, ByVal testHandle As Integer, ByVal patternSetIdx As Integer) As Integer

    Declare Function terM9_closeDigitalTest Lib "terM9_32.dll" (ByVal vi As Integer, ByVal testHandle As Integer) As Integer

    Declare Function terM9_getPinmap Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinmapMaxCount As Integer, ByRef pinmapCount As Integer, ByRef pinmap As Integer) As Integer

    Declare Function terM9_getPinmapValues Lib "terM9_32.dll" (ByVal vi As Integer, ByVal startingIndex As Integer, ByVal locationCount As Integer, ByRef contents As Integer) As Integer

    Declare Function terM9_getPinmapGroup Lib "terM9_32.dll" (ByVal vi As Integer, ByVal index As Integer, ByVal groupMaxCount As Integer, ByRef groupCount As Integer, ByRef group As Integer) As Integer

    Declare Function terM9_setPinmap Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinmapCount As Integer, ByRef pinmap As Integer) As Integer

    Declare Function terM9_setPinmapValues Lib "terM9_32.dll" (ByVal vi As Integer, ByVal startingIndex As Integer, ByVal locationCount As Integer, ByRef contents As Integer) As Integer

    Declare Function terM9_setPinmapGroup Lib "terM9_32.dll" (ByVal vi As Integer, ByVal index As Integer, ByVal groupCount As Integer, ByRef group As Integer) As Integer

    Declare Function terM9_getPinListPinCount Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByRef pinListPinCount As Integer) As Integer

    Declare Function terM9_resetPinmap Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_getChannel Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef Capture As Integer, ByRef chanMode As Integer, ByRef connect As Integer, ByRef Format_Renamed As Integer, ByRef impedance As Integer, ByRef LevelIdx As Integer, ByRef Load As Integer, ByRef PhaseIdx As Integer, ByRef WindowIdx As Integer) As Integer

    Declare Function terM9_getChannelCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef Capture As Integer) As Integer

    Declare Function terM9_getChannelChanMode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef chanMode As Integer) As Integer

    Declare Function terM9_getChannelConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef connect As Integer) As Integer

    Declare Function terM9_getChannelFormat Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef frmt As Integer) As Integer

    Declare Function terM9_getChannelImpedance Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef impedance As Integer) As Integer

    Declare Function terM9_getChannelLevel Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef LevelIdx As Integer) As Integer

    Declare Function terM9_getChannelLoad Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef Load As Integer) As Integer

    Declare Function terM9_getChannelPhase Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef PhaseIdx As Integer) As Integer

    Declare Function terM9_getChannelWindow Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef WindowIdx As Integer) As Integer

    Declare Function terM9_setChannel Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal Capture As Integer, ByVal chanMode As Integer, ByVal connect As Integer, ByVal frmt As Integer, ByVal impedance As Integer, ByVal LevelIdx As Integer, ByVal Load As Integer, ByVal PhaseIdx As Integer, ByVal WindowIdx As Integer) As Integer

    Declare Function terM9_setChannelCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal Capture As Integer) As Integer

    Declare Function terM9_setChannelChanMode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal chanMode As Integer) As Integer

    Declare Function terM9_setChannelConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal connect As Integer) As Integer

    Declare Function terM9_setChannelFormat Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal frmt As Integer) As Integer

    Declare Function terM9_setChannelImpedance Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal impedance As Integer) As Integer

    Declare Function terM9_setChannelLevel Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal LevelIdx As Integer) As Integer

    Declare Function terM9_setChannelLoad Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal Load As Integer) As Integer

    Declare Function terM9_setChannelPhase Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal PhaseIdx As Integer) As Integer

    Declare Function terM9_setChannelWindow Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal WindowIdx As Integer) As Integer

    Declare Function terM9_getTimingSet Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByRef clockPeriod As Double, ByVal scopeIdx As Integer, ByVal phaseEntryMaxCount As Integer, ByRef phaseEntryCount As Integer, ByRef phaseEntry As Double, ByVal windowEntryMaxCount As Integer, ByRef windowEntryCount As Integer, ByRef windowEntry As Double) As Integer

    Declare Function terM9_getTimingSetPeriod Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByRef clockPeriod As Double) As Integer

    Declare Function terM9_getTimingSetPhase Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByRef assertTime As Double, ByRef returnTime As Double) As Integer

    Declare Function terM9_getTimingSetPhaseAssert Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByRef assertTime As Double) As Integer

    Declare Function terM9_getTimingSetPhaseReturn Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByRef returnTime As Double) As Integer

    Declare Function terM9_getTimingSetWindow Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal WindowIdx As Integer, ByRef openTime As Double, ByRef closeTime As Double) As Integer

    Declare Function terM9_getTimingSetWindowOpen Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal WindowIdx As Integer, ByRef openTime As Double) As Integer

    Declare Function terM9_getTimingSetWindowClose Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal WindowIdx As Integer, ByRef closeTime As Double) As Integer

    Declare Function terM9_setTimingSet Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal clockPeriod As Double, ByVal scopeIdx As Integer, ByVal phaseEntryCount As Integer, ByRef phaseEntry As Double, ByVal windowEntryCount As Integer, ByRef windowEntry As Double) As Integer

    Declare Function terM9_setTimingSetPeriod Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal clockPeriod As Double) As Integer

    Declare Function terM9_setTimingSetPhase Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByVal assertTime As Double, ByVal returnTime As Double) As Integer

    Declare Function terM9_setTimingSetPhaseAssert Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByVal assertTime As Double) As Integer

    Declare Function terM9_setTimingSetPhaseReturn Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByVal returnTime As Double) As Integer

    Declare Function terM9_setTimingSetWindow Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal WindowIdx As Integer, ByVal openTime As Double, ByVal closeTime As Double) As Integer

    Declare Function terM9_setTimingSetWindowOpen Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal WindowIdx As Integer, ByVal openTime As Double) As Integer

    Declare Function terM9_setTimingSetWindowClose Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer, ByVal scopeIdx As Integer, ByVal WindowIdx As Integer, ByVal closeTime As Double) As Integer

    Declare Function terM9_getPhase Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByRef freerun As Short, ByRef trigger As Integer) As Integer

    Declare Function terM9_getPhaseFreerun Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByRef freerun As Short) As Integer

    Declare Function terM9_getPhaseTrigger Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByRef trigger As Integer) As Integer

    Declare Function terM9_setPhase Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByVal freerun As Short, ByVal trigger As Integer) As Integer

    Declare Function terM9_setPhaseFreerun Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByVal freerun As Short) As Integer

    Declare Function terM9_setPhaseTrigger Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal PhaseIdx As Integer, ByVal trigger As Integer) As Integer

    Declare Function terM9_getExtClockSrcLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_setExtClockSrcLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_getSystemClock Lib "terM9_32.dll" (ByVal vi As Integer, ByRef divider As Integer, ByRef edge As Integer, ByRef mode As Integer, ByRef reference As Integer, ByRef outpath As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_getSystemClockDivider Lib "terM9_32.dll" (ByVal vi As Integer, ByRef divider As Integer) As Integer

    Declare Function terM9_getSystemClockEdge Lib "terM9_32.dll" (ByVal vi As Integer, ByRef edge As Integer) As Integer

    Declare Function terM9_getSystemClockMode Lib "terM9_32.dll" (ByVal vi As Integer, ByRef mode As Integer) As Integer

    Declare Function terM9_getSystemClockReference Lib "terM9_32.dll" (ByVal vi As Integer, ByRef reference As Integer) As Integer

    Declare Function terM9_getSystemClockOutPath Lib "terM9_32.dll" (ByVal vi As Integer, ByRef outpath As Integer) As Integer

    Declare Function terM9_getSystemClockOutLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_setSystemClock Lib "terM9_32.dll" (ByVal vi As Integer, ByVal divider As Integer, ByVal edge As Integer, ByVal mode As Integer, ByVal reference As Integer, ByVal outpath As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_setSystemClockDivider Lib "terM9_32.dll" (ByVal vi As Integer, ByVal divider As Integer) As Integer

    Declare Function terM9_setSystemClockEdge Lib "terM9_32.dll" (ByVal vi As Integer, ByVal edge As Integer) As Integer

    Declare Function terM9_setSystemClockMode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal mode As Integer) As Integer

    Declare Function terM9_setSystemClockReference Lib "terM9_32.dll" (ByVal vi As Integer, ByVal reference As Integer) As Integer

    Declare Function terM9_setSystemClockOutPath Lib "terM9_32.dll" (ByVal vi As Integer, ByVal outpath As Integer) As Integer

    Declare Function terM9_setSystemClockOutLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_getSystemClutch Lib "terM9_32.dll" (ByVal vi As Integer, ByRef enable As Short, ByRef polarity As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_getSystemClutchEnable Lib "terM9_32.dll" (ByVal vi As Integer, ByRef enable As Short) As Integer

    Declare Function terM9_getSystemClutchPolarity Lib "terM9_32.dll" (ByVal vi As Integer, ByRef polarity As Integer) As Integer

    Declare Function terM9_getSystemClutchLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_setSystemClutch Lib "terM9_32.dll" (ByVal vi As Integer, ByVal enable As Short, ByVal polarity As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_setSystemClutchEnable Lib "terM9_32.dll" (ByVal vi As Integer, ByVal enable As Short) As Integer

    Declare Function terM9_setSystemClutchPolarity Lib "terM9_32.dll" (ByVal vi As Integer, ByVal polarity As Integer) As Integer

    Declare Function terM9_setSystemClutchLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_getPatternClutch Lib "terM9_32.dll" (ByVal vi As Integer, ByRef enable As Short, ByRef polarity As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_getPatternClutchEnable Lib "terM9_32.dll" (ByVal vi As Integer, ByRef enable As Short) As Integer

    Declare Function terM9_getPatternClutchPolarity Lib "terM9_32.dll" (ByVal vi As Integer, ByRef polarity As Integer) As Integer

    Declare Function terM9_getPatternClutchLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_setPatternClutch Lib "terM9_32.dll" (ByVal vi As Integer, ByVal enable As Short, ByVal polarity As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_setPatternClutchEnable Lib "terM9_32.dll" (ByVal vi As Integer, ByVal enable As Short) As Integer

    Declare Function terM9_setPatternClutchPolarity Lib "terM9_32.dll" (ByVal vi As Integer, ByVal polarity As Integer) As Integer

    Declare Function terM9_setPatternClutchLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_getEXT1InLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_setEXT1InLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_getEXT2InLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_setEXT2InLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_getLevelSet Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef VIHValue As Double, ByRef VILValue As Double, ByRef VOHValue As Double, ByRef VOLValue As Double, ByRef IOHValue As Double, ByRef IOLValue As Double, ByRef VCOMValue As Double, ByRef slewRate As Integer) As Integer

    Declare Function terM9_getLevelSetIOH Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef IOHValue As Double) As Integer

    Declare Function terM9_getLevelSetIOL Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef IOLValue As Double) As Integer

    Declare Function terM9_getLevelSetVCOM Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef VCOMValue As Double) As Integer

    Declare Function terM9_getLevelSetVIH Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef VIHValue As Double) As Integer

    Declare Function terM9_getLevelSetVIL Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef VILValue As Double) As Integer

    Declare Function terM9_getLevelSetVOH Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef VOHValue As Double) As Integer

    Declare Function terM9_getLevelSetVOL Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef VOLValue As Double) As Integer

    Declare Function terM9_getLevelSetSlewRate Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByRef slewRate As Integer) As Integer

    Declare Function terM9_setLevelSet Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal VIHValue As Double, ByVal VILValue As Double, ByVal VOHValue As Double, ByVal VOLValue As Double, ByVal IOHValue As Double, ByVal IOLValue As Double, ByVal VCOMValue As Double, ByVal slewRate As Integer) As Integer

    Declare Function terM9_setLevelSetIOH Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal IOHValue As Double) As Integer

    Declare Function terM9_setLevelSetIOL Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal IOLValue As Double) As Integer

    Declare Function terM9_setLevelSetVCOM Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal VCOMValue As Double) As Integer

    Declare Function terM9_setLevelSetVIH Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal VIHValue As Double) As Integer

    Declare Function terM9_setLevelSetVIL Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal VILValue As Double) As Integer

    Declare Function terM9_setLevelSetVOH Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal VOHValue As Double) As Integer

    Declare Function terM9_setLevelSetVOL Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal VOLValue As Double) As Integer

    Declare Function terM9_setLevelSetSlewRate Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal LevelIdx As Integer, ByVal slewRate As Integer) As Integer

    Declare Function terM9_getLevelSetProbe Lib "terM9_32.dll" (ByVal vi As Integer, ByRef PRHIValue As Double, ByRef PRLOValue As Double) As Integer

    Declare Function terM9_getLevelSetProbePRHI Lib "terM9_32.dll" (ByVal vi As Integer, ByRef PRHIValue As Double) As Integer

    Declare Function terM9_getLevelSetProbePRLO Lib "terM9_32.dll" (ByVal vi As Integer, ByRef PRLOValue As Double) As Integer

    Declare Function terM9_setLevelSetProbe Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PRHIValue As Double, ByVal PRLOValue As Double) As Integer

    Declare Function terM9_setLevelSetProbePRLO Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PRLOValue As Double) As Integer

    Declare Function terM9_setLevelSetProbePRHI Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PRHIValue As Double) As Integer

    Declare Function terM9_getChannelPinOpcode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef opcode As Integer) As Integer

    Declare Function terM9_setChannelPinOpcode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal opcode As Integer) As Integer

    Declare Function terM9_getGroupGroupOpcode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef groupOpcode As Integer, ByRef groupValue As Integer) As Integer

    Declare Function terM9_setGroupGroupOpcode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal groupOpcode As Integer, ByRef groupValue As Integer) As Integer

    Declare Function terM9_getFrontPanelSyncState Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByRef state As Integer) As Integer

    Declare Function terM9_setFrontPanelSyncState Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByVal state As Integer) As Integer

    Declare Function terM9_getBackplaneSyncState Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByRef state As Integer) As Integer

    Declare Function terM9_setBackplaneSyncState Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByVal state As Integer) As Integer

    Declare Function terM9_resetChannelSignature Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer) As Integer

    Declare Function terM9_prepareDynamicPatternSetLoading Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_getDynamicPattern Lib "terM9_32.dll" (ByVal vi As Integer, ByRef clocksPerPattern As Integer, ByRef TSETIdx As Integer, ByRef controlOpcode As Integer, ByRef controlCount As Integer, ByRef controlCond As Integer, ByRef failCapture As Short, ByRef resultCapture As Short, ByRef PSETIdx As Integer, ByRef probeCapture As Short) As Integer

    Declare Function terM9_getDynamicPatternClocksPerPattern Lib "terM9_32.dll" (ByVal vi As Integer, ByRef clocksPerPattern As Integer) As Integer

    Declare Function terM9_getDynamicPatternTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef TSETIdx As Integer) As Integer

    Declare Function terM9_getDynamicPatternControl Lib "terM9_32.dll" (ByVal vi As Integer, ByRef controlOpcode As Integer, ByRef controlCount As Integer, ByRef controlCond As Integer) As Integer

    Declare Function terM9_getDynamicPatternFailCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByRef failCapture As Short) As Integer

    Declare Function terM9_getDynamicPatternResultCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByRef resultCapture As Short) As Integer

    Declare Function terM9_getDynamicPatternProbeTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef PSETIdx As Integer) As Integer

    Declare Function terM9_getDynamicPatternProbeCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByRef probeCapture As Short) As Integer

    Declare Function terM9_getDynamicPatternCount Lib "terM9_32.dll" (ByVal vi As Integer, ByRef patternCount As Integer) As Integer

    Declare Function terM9_getDynamicPatternIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef patternIdx As Integer) As Integer

    Declare Function terM9_getResultIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef resultIdx As Integer) As Integer

    Declare Function terM9_setDynamicPattern Lib "terM9_32.dll" (ByVal vi As Integer, ByVal clocksPerPattern As Integer, ByVal TSETIdx As Integer, ByVal controlOpcode As Integer, ByVal controlCount As Integer, ByVal controlCond As Integer, ByVal failCapture As Short, ByVal resultCapture As Short, ByVal PSETIdx As Integer, ByVal probeCapture As Short) As Integer

    Declare Function terM9_setDynamicPatternClocksPerPattern Lib "terM9_32.dll" (ByVal vi As Integer, ByVal clocksPerPattern As Integer) As Integer

    Declare Function terM9_setDynamicPatternTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal TSETIdx As Integer) As Integer

    Declare Function terM9_setDynamicPatternControl Lib "terM9_32.dll" (ByVal vi As Integer, ByVal controlOpcode As Integer, ByVal controlCount As Integer, ByVal controlCond As Integer) As Integer

    Declare Function terM9_setDynamicPatternFailCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByVal failCapture As Short) As Integer

    Declare Function terM9_setDynamicPatternResultCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByVal resultCapture As Short) As Integer

    Declare Function terM9_setDynamicPatternProbeTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PSETIdx As Integer) As Integer

    Declare Function terM9_setDynamicPatternProbeCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByVal probeCapture As Short) As Integer

    Declare Function terM9_setDynamicPatternIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal patternIdx As Integer) As Integer

    Declare Function terM9_loadDynamicPattern Lib "terM9_32.dll" (ByVal vi As Integer, ByVal patternStep As Short, ByVal test As Short) As Integer

    Declare Function terM9_fetchDynamicChannelPinOpcode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal initPatternIdx As Integer, ByVal patternCount As Integer, ByRef pinOpcodeBuffer As Integer) As Integer

    Declare Function terM9_modifyDynamicChannelPinOpcode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal initPatternIdx As Integer, ByVal patternCount As Integer, ByVal pinOpcodeCount As Integer, ByRef pinOpcodeBuffer As Integer) As Integer

    Declare Function terM9_fetchDynamicPatternModifiers Lib "terM9_32.dll" (ByVal vi As Integer, ByVal initPatternIdx As Integer, ByVal patternCount As Integer, ByRef clocksPerPattern As Integer, ByRef TSETIdx As Integer, ByRef controlOpcode As Integer, ByRef controlCount As Integer, ByRef controlCond As Integer, ByRef test As Short, ByRef failCapture As Short, ByRef resultCapture As Short, ByRef PSETIdx As Integer, ByRef patternStep As Short, ByRef probeCapture As Short) As Integer

    Declare Function terM9_getStaticPatternProbeCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByRef probeCapture As Short) As Integer

    Declare Function terM9_getStaticPatternCount Lib "terM9_32.dll" (ByVal vi As Integer, ByRef patternCount As Integer) As Integer

    Declare Function terM9_setStaticPatternProbeCapture Lib "terM9_32.dll" (ByVal vi As Integer, ByVal probeCapture As Short) As Integer

    Declare Function terM9_fetchStaticChannelPinState Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByRef pinStateBuffer As Integer) As Integer

    Declare Function terM9_getProbeMethod Lib "terM9_32.dll" (ByVal vi As Integer, ByRef probeMethod As Integer) As Integer

    Declare Function terM9_setProbeMethod Lib "terM9_32.dll" (ByVal vi As Integer, ByVal probeMethod As Integer) As Integer

    Declare Function terM9_loadProbeExpectData Lib "terM9_32.dll" (ByVal vi As Integer, ByVal expectDataCount As Integer, ByRef expectData As Integer) As Integer

    Declare Function terM9_loadProbeMapData Lib "terM9_32.dll" (ByVal vi As Integer, ByVal mapDataCount As Integer, ByRef mapData As Integer) As Integer

    Declare Function terM9_loadProbeTimingSetIndexes Lib "terM9_32.dll" (ByVal vi As Integer, ByVal initPatternIdx As Integer, ByVal patternCount As Integer, ByRef timingIndexes As Integer, ByVal stepOnly As Short) As Integer

    Declare Function terM9_prepareDynamicProbeLoading Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_getProbeTimingSet Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PSETIdx As Integer, ByRef openTime As Double, ByRef closeTime As Double) As Integer

    Declare Function terM9_getProbeTimingSetOpen Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PSETIdx As Integer, ByRef openTime As Double) As Integer

    Declare Function terM9_getProbeTimingSetClose Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PSETIdx As Integer, ByRef closeTime As Double) As Integer

    Declare Function terM9_setProbeTimingSet Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PSETIdx As Integer, ByVal openTime As Double, ByVal closeTime As Double) As Integer

    Declare Function terM9_setProbeTimingSetOpen Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PSETIdx As Integer, ByVal openTime As Double) As Integer

    Declare Function terM9_setProbeTimingSetClose Lib "terM9_32.dll" (ByVal vi As Integer, ByVal PSETIdx As Integer, ByVal closeTime As Double) As Integer

    Declare Function terM9_prepareStaticProbeLoading Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_getHighVoltagePinGroupSettings Lib "terM9_32.dll" (ByVal vi As Integer, ByRef threshold As Double, ByVal HVPinMaxCount As Integer, ByRef HVPinCount As Integer, ByRef mode As Integer) As Integer

    Declare Function terM9_getHighVoltagePinGroup Lib "terM9_32.dll" (ByVal vi As Integer, ByVal HVPinMaxCount As Integer, ByRef HVPinCount As Integer, ByRef mode As Integer) As Integer

    Declare Function terM9_getHighVoltagePinMode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal HVPinIdx As Integer, ByRef mode As Integer) As Integer

    Declare Function terM9_getHighVoltagePinGroupThreshold Lib "terM9_32.dll" (ByVal vi As Integer, ByRef threshold As Double) As Integer

    Declare Function terM9_setHighVoltagePinGroupSettings Lib "terM9_32.dll" (ByVal vi As Integer, ByVal threshold As Double, ByVal HVPinCount As Integer, ByRef mode As Integer) As Integer

    Declare Function terM9_setHighVoltagePinGroup Lib "terM9_32.dll" (ByVal vi As Integer, ByVal HVPinCount As Integer, ByRef mode As Integer) As Integer

    Declare Function terM9_setHighVoltagePinMode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal HVPinIdx As Integer, ByVal mode As Integer) As Integer

    Declare Function terM9_setHighVoltagePinGroupThreshold Lib "terM9_32.dll" (ByVal vi As Integer, ByVal threshold As Double) As Integer

    Declare Function terM9_getFrontPanelSyncResource Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByRef logicSelect As Integer, ByRef pulseGenIdx As Integer, ByRef connect As Integer) As Integer

    Declare Function terM9_getFrontPanelSyncLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByRef logicSelect As Integer) As Integer

    Declare Function terM9_getFrontPanelSyncPulseSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByRef pulseGenIdx As Integer) As Integer

    Declare Function terM9_getFrontPanelSyncConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByRef connect As Integer) As Integer

    Declare Function terM9_setFrontPanelSyncResource Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByVal logicSelect As Integer, ByVal pulseGenIdx As Integer, ByVal connect As Integer) As Integer

    Declare Function terM9_setFrontPanelSyncLogicSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByVal logicSelect As Integer) As Integer

    Declare Function terM9_setFrontPanelSyncPulseSelect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByVal pulseGenIdx As Integer) As Integer

    Declare Function terM9_setFrontPanelSyncConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal syncIdx As Integer, ByVal connect As Integer) As Integer

    Declare Function terM9_getSyncResourcePulseWidth Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pulseGenIdx As Integer, ByRef pulseWidth As Double) As Integer

    Declare Function terM9_setSyncResourcePulseWidth Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pulseGenIdx As Integer, ByVal pulseWidth As Double) As Integer

    Declare Function terM9_getUserClock Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByRef connect As Integer, ByRef highVoltageLevel As Double, ByRef lowVoltageLevel As Double, ByRef mode As Integer, ByVal phaseEntryMaxCount As Integer, ByRef phaseEntryCount As Integer, ByRef phaseEntry As Double, ByRef trigger As Integer) As Integer

    Declare Function terM9_getUserClockConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByRef connect As Integer) As Integer

    Declare Function terM9_getUserClockMode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByRef mode As Integer) As Integer

    Declare Function terM9_getUserClockPhase Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal phaseEntryMaxCount As Integer, ByRef phaseEntryCount As Integer, ByRef phaseEntry As Double) As Integer

    Declare Function terM9_getUserClockPhaseAssert Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal PhaseIdx As Integer, ByRef assertTime As Double) As Integer

    Declare Function terM9_getUserClockPhaseReturn Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal PhaseIdx As Integer, ByRef returnTime As Double) As Integer

    Declare Function terM9_getUserClockTrigger Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByRef trigger As Integer) As Integer

    Declare Function terM9_getUserClockHighVoltageLevel Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByRef highVoltageLevel As Double) As Integer

    Declare Function terM9_getUserClockLowVoltageLevel Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByRef lowVoltageLevel As Double) As Integer

    Declare Function terM9_setUserClock Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal connect As Integer, ByVal highVoltageLevel As Double, ByVal lowVoltageLevel As Double, ByVal mode As Integer, ByVal phaseEntryCount As Integer, ByRef phaseEntry As Double, ByVal trigger As Integer) As Integer

    Declare Function terM9_setUserClockConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal connect As Integer) As Integer

    Declare Function terM9_setUserClockMode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal mode As Integer) As Integer

    Declare Function terM9_setUserClockPhase Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal phaseEntryCount As Integer, ByRef phaseEntry As Double) As Integer

    Declare Function terM9_setUserClockPhaseAssert Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal PhaseIdx As Integer, ByVal assertTime As Double) As Integer

    Declare Function terM9_setUserClockPhaseReturn Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal PhaseIdx As Integer, ByVal returnTime As Double) As Integer

    Declare Function terM9_setUserClockTrigger Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal trigger As Integer) As Integer

    Declare Function terM9_setUserClockHighVoltageLevel Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal highVoltageLevel As Double) As Integer

    Declare Function terM9_setUserClockLowVoltageLevel Lib "terM9_32.dll" (ByVal vi As Integer, ByVal userClockIdx As Integer, ByVal lowVoltageLevel As Double) As Integer

    Declare Function terM9_getSystemEnable Lib "terM9_32.dll" (ByVal vi As Integer, ByRef enable As Short) As Integer

    Declare Function terM9_setSystemEnable Lib "terM9_32.dll" (ByVal vi As Integer, ByVal enable As Short) As Integer

    Declare Function terM9_getDynamicPatternSetTimeout Lib "terM9_32.dll" (ByVal vi As Integer, ByRef timeout As Double) As Integer

    Declare Function terM9_setDynamicPatternSetTimeout Lib "terM9_32.dll" (ByVal vi As Integer, ByVal timeout As Double) As Integer

    Declare Function terM9_getStaticPatternDelay Lib "terM9_32.dll" (ByVal vi As Integer, ByRef Delay As Double) As Integer

    Declare Function terM9_setStaticPatternDelay Lib "terM9_32.dll" (ByVal vi As Integer, ByVal Delay As Double) As Integer

    Declare Function terM9_getGroundReference Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef groundReference As Integer) As Integer

    Declare Function terM9_setGroundReference Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal groundReference As Integer) As Integer

    Declare Function terM9_getSystemTimingMode Lib "terM9_32.dll" (ByVal vi As Integer, ByRef timingMode As Integer) As Integer

    Declare Function terM9_setSystemTimingMode Lib "terM9_32.dll" (ByVal vi As Integer, ByVal timingMode As Integer) As Integer

    Declare Function terM9_getLowPower Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef state As Short) As Integer

    Declare Function terM9_setLowPower Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal state As Short) As Integer

    Declare Function terM9_getDMTXToPinConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByRef scopeIdx As Integer) As Integer

    Declare Function terM9_setDMTXToPinConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer) As Integer

    Declare Function terM9_runDynamicPatternSet Lib "terM9_32.dll" (ByVal vi As Integer, ByVal expandLoops As Short, ByRef patternSetResult As Integer) As Integer

    Declare Function terM9_prepareDynamicPatternSet Lib "terM9_32.dll" (ByVal vi As Integer) As Short

    Declare Function terM9_prepareDynamicPatternSetExecution Lib "terM9_32.dll" (ByVal vi As Integer, ByVal expandLoops As Short, ByVal stopOnFail As Short, ByVal stopOnEvent As Short, ByVal stopOnProbe As Short, ByVal stopOnLSEQ As Short, ByVal stopOnPattern As Short, ByVal stopOnPatternIdx As Integer, ByVal learnFlag As Integer, ByVal preCountFlag As Integer, ByVal preCount As Integer, ByVal postCount As Integer) As Integer

    Declare Function terM9_initiateDynamicPatternSetExecution Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_waitForDynamicPatternSetExecution Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_stopDynamicPatternSetExecution Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_runStaticPattern Lib "terM9_32.dll" (ByVal vi As Integer, ByVal patternStep As Short, ByVal test As Short) As Integer

    Declare Function terM9_fetchChannelSignature Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef SIG As Integer) As Integer

    Declare Function terM9_fetchChannelState Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef detectorState As Integer, ByRef deskewRelay As Integer, ByRef overTemperature As Short, ByRef referenceOverload As Short) As Integer

    Declare Function terM9_fetchPatternClutchState Lib "terM9_32.dll" (ByVal vi As Integer, ByRef state As Integer) As Integer

    Declare Function terM9_fetchSystemClutchState Lib "terM9_32.dll" (ByVal vi As Integer, ByRef state As Integer) As Integer

    Declare Function terM9_fetchHighVoltagePinGroupState Lib "terM9_32.dll" (ByVal vi As Integer, ByVal stateMaxCount As Integer, ByRef stateCount As Integer, ByRef state As Integer) As Integer

    Declare Function terM9_fetchHighVoltagePinState Lib "terM9_32.dll" (ByVal vi As Integer, ByVal HVPinIdx As Integer, ByRef state As Integer) As Integer

    Declare Function terM9_fetchDynamicPatternSetResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef patternSetResult As Integer, ByRef resultCount As Integer, ByRef lastResultIdx As Integer) As Integer

    Declare Function terM9_fetchDynamicPatternSetState Lib "terM9_32.dll" (ByVal vi As Integer, ByRef loopState As Integer, ByRef runState As Integer, ByRef learnedPastLimit As Short, ByRef lastPatternValid As Short, ByRef stoppedOnFail As Short, ByRef stoppedOnEvent As Short, ByRef stoppedOnProbe As Short, ByRef stoppedOnLSEQHalt As Short, ByRef stoppedOnPattern As Short, ByRef stoppedOnPatternIdx As Integer) As Integer

    Declare Function terM9_fetchDynamicFailures Lib "terM9_32.dll" (ByVal vi As Integer, ByVal resultIdx As Integer, ByVal pinListMaxCount As Integer, ByRef pinListCount As Integer, ByRef pinList As Integer, ByRef pinOpcodeList As Integer) As Integer

    Declare Function terM9_fetchDynamicPatternResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal initResultIdx As Integer, ByVal resultCount As Integer, ByRef resultsCount As Integer, ByRef results As Integer, ByRef patternIndexes As Integer) As Integer

    Declare Function terM9_fetchPassingDynamicPatternResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal initResultIdx As Integer, ByVal resultCount As Integer, ByRef resultIndexesCount As Integer, ByRef resultIndexes As Integer, ByRef patternIndexes As Integer) As Integer

    Declare Function terM9_fetchFailingDynamicPatternResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal initResultIdx As Integer, ByVal resultCount As Integer, ByRef resultIndexesCount As Integer, ByRef resultIndexes As Integer, ByRef patternIndexes As Integer) As Integer

    Declare Function terM9_fetchDynamicPinResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal initResultIdx As Integer, ByVal resultCount As Integer, ByRef resultListCount As Integer, ByRef resultList As Integer) As Integer

    Declare Function terM9_fetchPassingDynamicPinResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal initResultIdx As Integer, ByVal resultCount As Integer, ByRef passingPinListCount As Integer, ByRef passingPinList As Integer) As Integer

    Declare Function terM9_fetchFailingDynamicPinResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal initResultIdx As Integer, ByVal resultCount As Integer, ByRef failingPinListCount As Integer, ByRef failingPinList As Integer) As Integer

    Declare Function terM9_fetchStaticPatternResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef result As Integer) As Integer

    Declare Function terM9_fetchStaticFailures Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListMaxCount As Integer, ByRef pinListCount As Integer, ByRef pinList As Integer, ByRef pinStateList As Integer) As Integer

    Declare Function terM9_fetchStaticPinResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByRef resultListCount As Integer, ByRef resultList As Integer) As Integer

    Declare Function terM9_fetchPassingStaticPinResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByRef passingPinListCount As Integer, ByRef passingPinList As Integer) As Integer

    Declare Function terM9_fetchFailingStaticPinResults Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByRef failingPinListCount As Integer, ByRef failingPinList As Integer) As Integer

    Declare Function terM9_fetchProbeDetectData Lib "terM9_32.dll" (ByVal vi As Integer, ByVal initStepIdx As Integer, ByVal stepCount As Integer, ByRef detectData As Integer) As Integer

    Declare Function terM9_fetchProbeMapData Lib "terM9_32.dll" (ByVal vi As Integer, ByVal mapDataMaxCount As Integer, ByRef mapDataCount As Integer, ByRef mapData As Integer) As Integer

    Declare Function terM9_fetchProbeSettings Lib "terM9_32.dll" (ByVal vi As Integer, ByRef probeMode As Integer, ByRef probeConfig As Integer, ByRef staticCapture As Integer, ByRef executeMode As Integer) As Integer

    Declare Function terM9_prepareDynamicProbeExecution Lib "terM9_32.dll" (ByVal vi As Integer, ByVal executeMode As Integer) As Integer

    Declare Function terM9_fetchDynamicProbeResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef result As Integer) As Integer

    Declare Function terM9_fetchDynamicProbeFailingDatatype Lib "terM9_32.dll" (ByVal vi As Integer, ByRef failingDataType As Integer) As Integer

    Declare Function terM9_fetchDynamicProbeFailingStep Lib "terM9_32.dll" (ByVal vi As Integer, ByRef failingStep As Integer) As Integer

    Declare Function terM9_fetchDynamicProbePatternCount Lib "terM9_32.dll" (ByVal vi As Integer, ByRef patternCount As Integer) As Integer

    Declare Function terM9_fetchDynamicProbeStepCount Lib "terM9_32.dll" (ByVal vi As Integer, ByRef stepCount As Integer) As Integer

    Declare Function terM9_prepareStaticProbeExecution Lib "terM9_32.dll" (ByVal vi As Integer, ByVal executeMode As Integer, ByVal staticCapture As Integer) As Integer

    Declare Function terM9_fetchStaticProbeResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef result As Integer) As Integer

    Declare Function terM9_fetchStaticProbeFailingDatatype Lib "terM9_32.dll" (ByVal vi As Integer, ByRef failingDataType As Integer) As Integer

    Declare Function terM9_fetchStaticProbeFailingStep Lib "terM9_32.dll" (ByVal vi As Integer, ByRef failingStep As Integer) As Integer

    Declare Function terM9_fetchStaticProbePatternCount Lib "terM9_32.dll" (ByVal vi As Integer, ByRef patternCount As Integer) As Integer

    Declare Function terM9_fetchStaticProbeStepCount Lib "terM9_32.dll" (ByVal vi As Integer, ByRef stepCount As Integer) As Integer

    Declare Function terM9_stopForFailure Lib "terM9_32.dll" (ByVal vi As Integer, ByVal stopForFailure As Short) As Integer

    Declare Function terM9_stopAtPattern Lib "terM9_32.dll" (ByVal vi As Integer, ByVal patternIdx As Integer, ByVal stopAtPattern As Short) As Integer

    Declare Function terM9_selfTest Lib "terM9_32.dll" (ByVal vi As Integer, ByRef testResult As Integer, ByVal testMessage As String) As Integer
    Declare Function terM9_self_test Lib "terM9_32.dll" (ByVal vi As Integer, ByRef testResult As Integer, ByVal testMessage As String) As Integer

    Declare Function terM9_executeDiagnosticSelfTest Lib "terM9_32.dll" (ByVal vi As Integer, ByRef testResult As Integer, ByVal testMessage As String) As Integer

    Declare Function terM9_errorQuery Lib "terM9_32.dll" (ByVal vi As Integer, ByRef errorcode As Integer, ByVal errorMessage As String) As Integer
    Declare Function terM9_error_query Lib "terM9_32.dll" (ByVal vi As Integer, ByRef errorcode As Integer, ByVal errorMessage As String) As Integer

    Declare Function terM9_errorMessage Lib "terM9_32.dll" (ByVal vi As Integer, ByVal statusCode As Integer, ByVal Message As String) As Integer
    Declare Function terM9_error_message Lib "terM9_32.dll" (ByVal vi As Integer, ByVal statusCode As Integer, ByVal Message As String) As Integer

    Declare Function terM9_revisionQuery Lib "terM9_32.dll" (ByVal vi As Integer, ByVal driverRev As String, ByVal instrumentRev As String) As Integer
    Declare Function terM9_revision_query Lib "terM9_32.dll" (ByVal vi As Integer, ByVal driverRev As String, ByVal instrumentRev As String) As Integer

    Declare Function terM9_initializeInstrument Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_reset Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_resetHighVoltagePins Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_resetProbe Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_executeSystemAlignment Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer) As Integer

    Declare Function terM9_executeChannelAlignment Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer) As Integer

    Declare Function terM9_executeSyncResourceAlignment Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer) As Integer

    Declare Function terM9_executeUserClockAlignment Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer) As Integer

    Declare Function terM9_executeProbeAlignment Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer) As Integer

    Declare Function terM9_fetchSystemAlignmentResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer, ByVal alignmentMessage As String) As Integer

    Declare Function terM9_fetchChannelAlignmentResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer, ByVal alignmentMessage As String) As Integer

    Declare Function terM9_fetchSyncResourceAlignmentResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer, ByVal alignmentMessage As String) As Integer

    Declare Function terM9_fetchUserClockAlignmentResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer, ByVal alignmentMessage As String) As Integer

    Declare Function terM9_fetchProbeAlignmentResult Lib "terM9_32.dll" (ByVal vi As Integer, ByRef alignmentResult As Integer, ByVal alignmentMessage As String) As Integer

    Declare Function terM9_getSystemInformation Lib "terM9_32.dll" (ByVal vi As Integer, ByVal systemDesc As String, ByRef cardCount As Integer, ByRef channelCount As Integer) As Integer

    Declare Function terM9_getCardInformation Lib "terM9_32.dll" (ByVal vi As Integer, ByVal cardIdx As Integer, ByRef cardId As Integer, ByVal cardName As String, ByRef boardCount As Integer, ByRef chanCount As Integer, ByRef chassis As Integer, ByRef slot As Integer, ByVal addlInfo As String) As Integer

    Declare Function terM9_getBoardInformation Lib "terM9_32.dll" (ByVal vi As Integer, ByVal cardIdx As Integer, ByVal boardIdx As Integer, ByRef boardId As Integer, ByVal boardNum As String, ByVal boardName As String, ByVal boardRev As String, ByVal boardSerial As String, ByVal addlInfo As String) As Integer

    Declare Function terM9_getChannelInformation Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef cardIdx As Integer, ByRef ChannelNumber As Integer, ByRef channelIdx As Integer) As Integer

    Declare Function terM9_getLastPatternIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef lastPatternIdx As Integer) As Integer

    Declare Function terM9_getLastResultIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef lastResultIdx As Integer) As Integer

    Declare Function terM9_getLastTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef lastTSETIdx As Integer) As Integer

    Declare Function terM9_getLastPhaseIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef lastPhaseIdx As Integer) As Integer

    Declare Function terM9_getLastWindowIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef lastWindowIdx As Integer) As Integer

    Declare Function terM9_getLastLevelIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByRef lastLevelIdx As Integer) As Integer

    Declare Function terM9_getLastProbeTimingSetIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef lastPSETIdx As Integer) As Integer

    Declare Function terM9_getLastFrontPanelSyncResourceIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef lastSyncResourceIdx As Integer) As Integer

    Declare Function terM9_getLastStepIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef lastStepIdx As Integer) As Integer

    Declare Function terM9_getLastHighVoltagePinIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef lastHVPinIdx As Integer) As Integer

    Declare Function terM9_getLastUserClockIndex Lib "terM9_32.dll" (ByVal vi As Integer, ByRef lastUserClockIdx As Integer) As Integer

    Declare Function terM9_getInterruptHandler Lib "terM9_32.dll" (ByVal vi As Integer, ByRef interruptHandler As Integer) As Integer

    Declare Function terM9_setInterruptHandler Lib "terM9_32.dll" (ByVal vi As Integer, ByVal interruptHandler As Integer) As Integer

    Declare Function terM9_wait Lib "terM9_32.dll" (ByVal vi As Integer, ByVal waittime As Double) As Integer

    Declare Function terM9_setTimer Lib "terM9_32.dll" (ByVal vi As Integer, ByVal waittime As Double) As Integer

    Declare Function terM9_startTimer Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_waitForTimer Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_testTimer Lib "terM9_32.dll" (ByVal vi As Integer, ByRef timerDone As Short) As Integer

    Declare Function terM9_stopTimer Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    Declare Function terM9_close Lib "terM9_32.dll" (ByVal vi As Integer) As Integer

    ' terM9_errors.h

    Public Const TERM9_ERROR_FIRST As Integer = &HBFFC0800
    Public Const TERM9_ERROR_ADC_CONNECT As Integer = &HBFFC0800
    Public Const TERM9_ERROR_AMMTR_MODE As Integer = &HBFFC0801
    Public Const TERM9_ERROR_BOARDIDX As Integer = &HBFFC0802
    Public Const TERM9_ERROR_PATTERNSET_TIMEOUT As Integer = &HBFFC0803
    Public Const TERM9_ERROR_CARDIDX As Integer = &HBFFC0804
    Public Const TERM9_ERROR_CCOND As Integer = &HBFFC0805
    Public Const TERM9_ERROR_CCOUNT As Integer = &HBFFC0806
    Public Const TERM9_ERROR_CHAN_CAPTURE As Integer = &HBFFC0807
    Public Const TERM9_ERROR_CHAN_CHANMODE As Integer = &HBFFC0808
    Public Const TERM9_ERROR_CHAN_CONNECT As Integer = &HBFFC0809
    Public Const TERM9_ERROR_CHAN_FORMAT As Integer = &HBFFC080A
    Public Const TERM9_ERROR_CHAN_IMPEDANCE As Integer = &HBFFC080B
    Public Const TERM9_ERROR_CHAN_LEVELIDX As Integer = &HBFFC080C
    Public Const TERM9_ERROR_CHAN_LOAD As Integer = &HBFFC080D
    Public Const TERM9_ERROR_CHAN_PHASEIDX As Integer = &HBFFC080E
    Public Const TERM9_ERROR_CHAN_WINDOWIDX As Integer = &HBFFC080F
    Public Const TERM9_ERROR_CLK_DIVIDER As Integer = &HBFFC0810
    Public Const TERM9_ERROR_CLK_EDGE As Integer = &HBFFC0811
    Public Const TERM9_ERROR_CLK_MODE As Integer = &HBFFC0812
    Public Const TERM9_ERROR_CLK_OUTPATH As Integer = &HBFFC0813
    Public Const TERM9_ERROR_CLK_SOURCE As Integer = &HBFFC0814
    Public Const TERM9_ERROR_CLUTCH_ENABLE As Integer = &HBFFC0815
    Public Const TERM9_ERROR_CLUTCH_POLARITY As Integer = &HBFFC0816
    Public Const TERM9_ERROR_COPCODE As Integer = &HBFFC0817
    Public Const TERM9_ERROR_CPP As Integer = &HBFFC0818
    Public Const TERM9_ERROR_EXPANDLOOPS As Integer = &HBFFC0819
    Public Const TERM9_ERROR_FAIL_CAPTURE As Integer = &HBFFC081A
    Public Const TERM9_ERROR_FILE_OPEN As Integer = &HBFFC081B
    Public Const TERM9_ERROR_FILE_PROC As Integer = &HBFFC081C
    Public Const TERM9_ERROR_GROUND_REFERENCE As Integer = &HBFFC081D
    Public Const TERM9_ERROR_GROUPSTATE As Integer = &HBFFC081E
    Public Const TERM9_ERROR_HVPIN_COUNT As Integer = &HBFFC081F
    Public Const TERM9_ERROR_HVPIN_MODE As Integer = &HBFFC0820
    Public Const TERM9_ERROR_HVPIN_THRESHOLD As Integer = &HBFFC0821
    Public Const TERM9_ERROR_HVPINIDX As Integer = &HBFFC0822
    Public Const TERM9_ERROR_IOH As Integer = &HBFFC0823
    Public Const TERM9_ERROR_IOL As Integer = &HBFFC0824
    Public Const TERM9_ERROR_LEARN_FLAG As Integer = &HBFFC0825
    Public Const TERM9_ERROR_LEVELIDX As Integer = &HBFFC0826
    Public Const TERM9_ERROR_LOGIC_SELECT As Integer = &HBFFC0827
    Public Const TERM9_ERROR_LOWPOWER As Integer = &HBFFC0828
    Public Const TERM9_ERROR_MAPTYPE As Integer = &HBFFC0829
    Public Const TERM9_ERROR_MAX_PATTERNSET_TIMEOUT As Integer = &HBFFC082A
    Public Const TERM9_ERROR_MTR_CURRENT_OVERLOAD As Integer = &HBFFC082B
    Public Const TERM9_ERROR_MTR_CURRENT_RANGE As Integer = &HBFFC082C
    Public Const TERM9_ERROR_MTR_FORCE_CURRENT As Integer = &HBFFC082D
    Public Const TERM9_ERROR_MTR_INPUT As Integer = &HBFFC082E
    Public Const TERM9_ERROR_MTR_IRNG As Integer = &HBFFC082F
    Public Const TERM9_ERROR_MTR_NEG_CLMP_VOLT As Integer = &HBFFC0830
    Public Const TERM9_ERROR_MTR_POS_CLMP_VOLT As Integer = &HBFFC0831
    Public Const TERM9_ERROR_MTR_VOLTAGE_RANGE As Integer = &HBFFC0832
    Public Const TERM9_ERROR_NOT_ENOUGH_DATA As Integer = &HBFFC0833
    Public Const TERM9_ERROR_OVER_TEMP As Integer = &HBFFC0834
    Public Const TERM9_ERROR_PATTERN_COUNT As Integer = &HBFFC0835
    Public Const TERM9_ERROR_PATTERN_DELAY As Integer = &HBFFC0836
    Public Const TERM9_ERROR_PATTERNIDX As Integer = &HBFFC0837
    Public Const TERM9_ERROR_PHASE_FREERUN As Integer = &HBFFC0838
    Public Const TERM9_ERROR_PHASE_TRIGGER As Integer = &HBFFC0839
    Public Const TERM9_ERROR_PHASEIDX As Integer = &HBFFC083A
    Public Const TERM9_ERROR_PINOPCODE As Integer = &HBFFC083B
    Public Const TERM9_ERROR_POSTCOUNT As Integer = &HBFFC083C
    Public Const TERM9_ERROR_PROBE_METHOD As Integer = &HBFFC083D
    Public Const TERM9_ERROR_PRECOUNT As Integer = &HBFFC083E
    Public Const TERM9_ERROR_PRECOUNT_FLAG As Integer = &HBFFC083F
    Public Const TERM9_ERROR_PRHI As Integer = &HBFFC0840
    Public Const TERM9_ERROR_PRLO As Integer = &HBFFC0841
    Public Const TERM9_ERROR_PROBE_CAPTURE As Integer = &HBFFC0842
    Public Const TERM9_ERROR_PROBE_CLOSE As Integer = &HBFFC0843
    Public Const TERM9_ERROR_PROBE_CONFIG As Integer = &HBFFC0844
    Public Const TERM9_ERROR_PROBE_EXECUTE As Integer = &HBFFC0845
    Public Const TERM9_ERROR_PROBE_MODE As Integer = &HBFFC0846
    Public Const TERM9_ERROR_PROBE_STATCAPTURE As Integer = &HBFFC0847
    Public Const TERM9_ERROR_PROBE_OPEN As Integer = &HBFFC0848
    Public Const TERM9_ERROR_PSETIDX As Integer = &HBFFC0849
    Public Const TERM9_ERROR_PSTACK_UNDERFLOW As Integer = &HBFFC084A
    Public Const TERM9_ERROR_PSTACK_OVERFLOW As Integer = &HBFFC084B
    Public Const TERM9_ERROR_REF_OVERLOAD As Integer = &HBFFC084C
    Public Const TERM9_ERROR_REGISTER_READ As Integer = &HBFFC084D
    Public Const TERM9_ERROR_REGISTER_WRITE As Integer = &HBFFC084E
    Public Const TERM9_ERROR_RESULT_CAPTURE As Integer = &HBFFC084F
    Public Const TERM9_ERROR_RESULT_COUNT As Integer = &HBFFC0850
    Public Const TERM9_ERROR_RESULTIDX As Integer = &HBFFC0851
    Public Const TERM9_ERROR_SCOPE_CHAN As Integer = &HBFFC0852
    Public Const TERM9_ERROR_SCOPE_LEVELS As Integer = &HBFFC0853
    Public Const TERM9_ERROR_SCOPE_DMTX As Integer = &HBFFC0854
    Public Const TERM9_ERROR_SCOPE_LOWPOWER As Integer = &HBFFC0855
    Public Const TERM9_ERROR_SCOPE_PHASE As Integer = &HBFFC0856
    Public Const TERM9_ERROR_SCOPE_TSET As Integer = &HBFFC0857
    Public Const TERM9_ERROR_SCOPEIDX As Integer = &HBFFC0858
    Public Const TERM9_ERROR_SLEWRATE As Integer = &HBFFC0859
    Public Const TERM9_ERROR_STEP As Integer = &HBFFC085A
    Public Const TERM9_ERROR_STEP_COUNT As Integer = &HBFFC085B
    Public Const TERM9_ERROR_STEP_ONLY As Integer = &HBFFC085C
    Public Const TERM9_ERROR_STEPIDX As Integer = &HBFFC085D
    Public Const TERM9_ERROR_STMTX_CONNECT As Integer = &HBFFC085E
    Public Const TERM9_ERROR_STMTX_FP_CONNECT As Integer = &HBFFC085F
    Public Const TERM9_ERROR_STMTX_GLOBAL As Integer = &HBFFC0860
    Public Const TERM9_ERROR_STOP As Integer = &HBFFC0861
    Public Const TERM9_ERROR_STOP_ON_EVENT As Integer = &HBFFC0862
    Public Const TERM9_ERROR_STOP_ON_FAIL As Integer = &HBFFC0863
    Public Const TERM9_ERROR_STOP_ON_LSEQ As Integer = &HBFFC0864
    Public Const TERM9_ERROR_STOP_ON_PATTERN As Integer = &HBFFC0865
    Public Const TERM9_ERROR_STOP_ON_PATT_IDX As Integer = &HBFFC0866
    Public Const TERM9_ERROR_STOP_ON_PROBE As Integer = &HBFFC0867
    Public Const TERM9_ERROR_SYNC_PULSEGEN_WIDTH As Integer = &HBFFC0868
    Public Const TERM9_ERROR_SYNC_PULSEGENIDX As Integer = &HBFFC0869
    Public Const TERM9_ERROR_SYNC_STATE As Integer = &HBFFC086A
    Public Const TERM9_ERROR_SYNCIDX As Integer = &HBFFC086B
    Public Const TERM9_ERROR_SYSTEM_ENABLE As Integer = &HBFFC086C
    Public Const TERM9_ERROR_TEST As Integer = &HBFFC086D
    Public Const TERM9_ERROR_TIMER_START As Integer = &HBFFC086E
    Public Const TERM9_ERROR_TIMING_MODE As Integer = &HBFFC086F
    Public Const TERM9_ERROR_TSET_CLOCK As Integer = &HBFFC0870
    Public Const TERM9_ERROR_TSET_PHS_ASSERT As Integer = &HBFFC0871
    Public Const TERM9_ERROR_TSET_PHS_COUNT As Integer = &HBFFC0872
    Public Const TERM9_ERROR_TSET_PHS_RETURN As Integer = &HBFFC0873
    Public Const TERM9_ERROR_TSET_WDW_CLOSE As Integer = &HBFFC0874
    Public Const TERM9_ERROR_TSET_WDW_COUNT As Integer = &HBFFC0875
    Public Const TERM9_ERROR_TSET_WDW_OPEN As Integer = &HBFFC0876
    Public Const TERM9_ERROR_TSETIDX As Integer = &HBFFC0877
    Public Const TERM9_ERROR_UCLK_CONNECT As Integer = &HBFFC0878
    Public Const TERM9_ERROR_UCLK_HIGHV As Integer = &HBFFC0879
    Public Const TERM9_ERROR_UCLK_IMPEDANCE As Integer = &HBFFC087A
    Public Const TERM9_ERROR_UCLK_LOWV As Integer = &HBFFC087B
    Public Const TERM9_ERROR_UCLK_MODE As Integer = &HBFFC087C
    Public Const TERM9_ERROR_UCLK_PHS_ASSERT As Integer = &HBFFC087D
    Public Const TERM9_ERROR_UCLK_PHS_COUNT As Integer = &HBFFC087E
    Public Const TERM9_ERROR_UCLK_PHS_RETURN As Integer = &HBFFC087F
    Public Const TERM9_ERROR_UCLK_TRIGGER As Integer = &HBFFC0880
    Public Const TERM9_ERROR_UCLKIDX As Integer = &HBFFC0881
    Public Const TERM9_ERROR_VCOM As Integer = &HBFFC0882
    Public Const TERM9_ERROR_VIH As Integer = &HBFFC0883
    Public Const TERM9_ERROR_VIL As Integer = &HBFFC0884
    Public Const TERM9_ERROR_VOH As Integer = &HBFFC0885
    Public Const TERM9_ERROR_VOL As Integer = &HBFFC0886
    Public Const TERM9_ERROR_VOLTAGE_SWING As Integer = &HBFFC0887
    Public Const TERM9_ERROR_VXI_ACFAIL As Integer = &HBFFC0888
    Public Const TERM9_ERROR_VXI_BUSERROR As Integer = &HBFFC0889
    Public Const TERM9_ERROR_VXI_MAPERROR As Integer = &HBFFC088A
    Public Const TERM9_ERROR_VXI_SYSFAIL As Integer = &HBFFC088B
    Public Const TERM9_ERROR_VXI_SYSRESET As Integer = &HBFFC088C
    Public Const TERM9_ERROR_WINDOWIDX As Integer = &HBFFC088D
    Public Const TERM9_LIC_EDGE_RES_1NS As Integer = &HBFFC088E
    Public Const TERM9_LIC_EDGE_RES_2NS As Integer = &HBFFC088F
    Public Const TERM9_LIC_EDGE_RES_5NS As Integer = &HBFFC0890
    Public Const TERM9_LIC_EDGE_RES_10NS As Integer = &HBFFC0891
    Public Const TERM9_LIC_FORMAT_NRET As Integer = &HBFFC0892
    Public Const TERM9_LIC_FORMAT_STANDARD As Integer = &HBFFC0893
    Public Const TERM9_LIC_LEVPRG_SYS As Integer = &HBFFC0894
    Public Const TERM9_LIC_LEVSEL_CARD As Integer = &HBFFC0895
    Public Const TERM9_LIC_LEVSEL_GRP As Integer = &HBFFC0896
    Public Const TERM9_LIC_RATE_25MHZ As Integer = &HBFFC0897
    Public Const TERM9_LIC_RATE_50MHZ As Integer = &HBFFC0898
    Public Const TERM9_LIC_TIMPRG_SYS As Integer = &HBFFC0899
    Public Const TERM9_LIC_TIMSEL_CARD As Integer = &HBFFC089A
    Public Const TERM9_LIC_TIMSEL_GRP As Integer = &HBFFC089B
    Public Const TERM9_NOLIC_HVPINS As Integer = &HBFFC089C
    Public Const TERM9_NOLIC_LOAD As Integer = &HBFFC089D
    Public Const TERM9_NOLIC_PROBE As Integer = &HBFFC089E
    Public Const TERM9_NOLIC_SYNCRES As Integer = &HBFFC089F
    Public Const TERM9_NOLIC_USERCLOCK As Integer = &HBFFC08A0
    Public Const TERM9_WARN_HOT_SWITCH As Integer = &HBFFC08A1
    Public Const TERM9_WARN_RESIDUAL_CONN As Integer = &HBFFC08A2
    Public Const TERM9_ERROR_PATSETIDX As Integer = &HBFFC08A3
    Public Const TERM9_ERROR_SESSION As Integer = &HBFFC08A4
    Public Const TERM9_ERROR_INIT As Integer = &HBFFC08A5
    Public Const TERM9_ERROR_MEMORY_ALLOCATION As Integer = &HBFFC08A6
    Public Const TERM9_WARN_SOFTWARE_SIM As Integer = &HBFFC08A7
    Public Const TERM9_ERROR_GROUPOPCODE As Integer = &HBFFC08A8
    Public Const TERM9_ERROR_ENABLE As Integer = &HBFFC08A9
    Public Const TERM9_ERROR_TEST_HANDLE As Integer = &HBFFC08AA
    Public Const TERM9_ERROR_TEST_PROC As Integer = &HBFFC08AB
    Public Const TERM9_ERROR_SYNC_CONNECT As Integer = &HBFFC08AD
    Public Const TERM9_ERROR_RESET_INSTR As Integer = &HBFFC08AE
    Public Const TERM9_ERROR_ID_QUERY As Integer = &HBFFC08AF
    Public Const TERM9_ERROR_TSET_COUNT As Integer = &HBFFC08B0
    Public Const TERM9_ERROR_SCOPE_CARD As Integer = &HBFFC08B1
    Public Const TERM9_ERROR_LAST As Integer = &HBFFC08AD

    ' terM9_types.h

    Public Const TERM9_ATTRVAL_FIRST As Short = 2
    Public Const TERM9_ADC_CONN_10TO10 As Short = 2
    Public Const TERM9_ADC_CONN_12MINUS As Short = 3
    Public Const TERM9_ADC_CONN_12PLUS As Short = 4
    Public Const TERM9_ADC_CONN_3PLUS As Short = 5
    Public Const TERM9_ADC_CONN_20TO20 As Short = 6
    Public Const TERM9_ADC_CONN_24MINUS As Short = 7
    Public Const TERM9_ADC_CONN_24PLUS As Short = 8
    Public Const TERM9_ADC_CONN_5TO5 As Short = 9
    Public Const TERM9_ADC_CONN_AMMETER As Short = 10
    Public Const TERM9_ADC_CONN_PWRGND As Short = 11
    Public Const TERM9_ADC_CONN_SIGGND As Short = 12
    Public Const TERM9_ADC_CONN_VCC As Short = 13
    Public Const TERM9_ADC_CONN_VEE As Short = 14
    Public Const TERM9_ADC_CONN_VTT As Short = 15
    Public Const TERM9_ADC_CONN_UNKNOWN As Short = 16
    Public Const TERM9_AMMTR_MODE_FORCE As Short = 17
    Public Const TERM9_AMMTR_MODE_MEASURE As Short = 18
    Public Const TERM9_AMMTR_MODE_OFF As Short = 19
    Public Const TERM9_AMMTR_MODE_SELFTEST As Short = 20
    Public Const TERM9_AMMTR_MODE_UNKNOWN As Short = 21
    Public Const TERM9_PATTERNSET_LEARN_FAILS As Short = 22
    Public Const TERM9_PATTERNSET_LEARN_PATS As Short = 23
    Public Const TERM9_PATTERNSET_PRECOUNT_FAILS As Short = 24
    Public Const TERM9_PATTERNSET_PRECOUNT_PATS As Short = 25
    Public Const TERM9_CAPTURE_CLOSE As Short = 26
    Public Const TERM9_CAPTURE_OPEN As Short = 27
    Public Const TERM9_CAPTURE_WINDOW As Short = 28
    Public Const TERM9_CHANMODE_DYNAMIC As Short = 29
    Public Const TERM9_CHANMODE_STATIC As Short = 30
    Public Const TERM9_CLUTCH_INPUT_HIGH As Short = 31
    Public Const TERM9_CLUTCH_SIGNAL_HIGH As Short = 32
    Public Const TERM9_CLUTCH_INPUT_LOW As Short = 33
    Public Const TERM9_CLUTCH_SIGNAL_LOW As Short = 34
    Public Const TERM9_COND_ALWAYS As Short = 35
    Public Const TERM9_COND_TRUE As Short = 35
    Public Const TERM9_COND_EX1 As Short = 36
    Public Const TERM9_COND_EXT1 As Short = 36
    Public Const TERM9_COND_EX2 As Short = 37
    Public Const TERM9_COND_EXT2 As Short = 37
    Public Const TERM9_COND_FAIL As Short = 38
    Public Const TERM9_COND_NEVER As Short = 39
    Public Const TERM9_COND_FALSE As Short = 39
    Public Const TERM9_COND_PASS As Short = 40
    Public Const TERM9_COND_SYNC As Short = 41
    Public Const TERM9_COND_NONE As Short = 42
    Public Const TERM9_COP_CLOOP As Short = 43
    Public Const TERM9_COP_CREPEAT As Short = 44
    Public Const TERM9_COP_ENDLOOP As Short = 45
    Public Const TERM9_COP_HALT As Short = 46
    Public Const TERM9_COP_JUMP As Short = 47
    Public Const TERM9_COP_LOOP As Short = 48
    Public Const TERM9_COP_NOP As Short = 49
    Public Const TERM9_COP_REPEAT As Short = 50
    Public Const TERM9_DETECTOR_BETWEEN As Short = 51
    Public Const TERM9_DETECTOR_HIGH As Short = 52
    Public Const TERM9_DETECTOR_LOW As Short = 53
    Public Const TERM9_DETECTOR_UNKNOWN As Short = 54
    Public Const TERM9_EDGE_FALLING As Short = 55
    Public Const TERM9_EDGE_RISING As Short = 56
    Public Const TERM9_EXECUTE_COMPARE As Short = 57
    Public Const TERM9_EXECUTE_LEARN As Short = 58
    Public Const TERM9_FORMAT_COMPS As Short = 59
    Public Const TERM9_FORMAT_NRET As Short = 60
    Public Const TERM9_FORMAT_RCOMP As Short = 61
    Public Const TERM9_FORMAT_ROFF As Short = 62
    Public Const TERM9_FORMAT_RONE As Short = 63
    Public Const TERM9_FORMAT_RZERO As Short = 64
    Public Const TERM9_GROUND_EXTERNAL As Short = 65
    Public Const TERM9_GROUND_INTERNAL As Short = 66
    Public Const TERM9_IMPED_50OHM As Short = 67
    Public Const TERM9_IMPED_LOWZ As Short = 68
    Public Const TERM9_LOAD_OFF As Short = 69
    Public Const TERM9_LOAD_ON As Short = 70
    Public Const TERM9_LOGIC_ECL As Short = 71
    Public Const TERM9_LOGIC_TTL As Short = 72
    Public Const TERM9_LOOP_OK As Short = 73
    Public Const TERM9_LOOP_OVERFLOW As Short = 74
    Public Const TERM9_LOOP_UNDERFLOW As Short = 75
    Public Const TERM9_CLOCKMODE_OFF As Short = 76
    Public Const TERM9_HVPINMODE_OFF As Short = 77
    Public Const TERM9_UCLKMODE_OFF As Short = 78
    Public Const TERM9_CLOCKMODE_ON As Short = 79
    Public Const TERM9_HVPINMODE_ON As Short = 80
    Public Const TERM9_UCLKMODE_ON As Short = 81
    Public Const TERM9_MTR_INPUT_NONE As Short = 82
    Public Const TERM9_MTR_INPUT_PANEL As Short = 83
    Public Const TERM9_MTR_INPUT_STMTX As Short = 84
    Public Const TERM9_MTR_INPUT_UNKNOWN As Short = 85
    Public Const TERM9_MTR_IRNG_100MA As Short = 86
    Public Const TERM9_MTR_IRNG_10MA As Short = 87
    Public Const TERM9_MTR_IRNG_UNKNOWN As Short = 88
    Public Const TERM9_MTR_MODE_FORCE As Short = 89
    Public Const TERM9_MTR_MODE_MEASURE As Short = 90
    Public Const TERM9_MTR_MODE_OFF As Short = 91
    Public Const TERM9_MTR_MODE_SELFTEST As Short = 92
    Public Const TERM9_MTR_MODE_UNKNOWN As Short = 93
    Public Const TERM9_MTR_VRNG_10TO10 As Short = 94
    Public Const TERM9_MTR_VRNG_20TO20 As Short = 95
    Public Const TERM9_MTR_VRNG_5TO5 As Short = 96
    Public Const TERM9_MTR_VRNG_OFF As Short = 97
    Public Const TERM9_OP_SIG As Short = 98
    Public Const TERM9_OP_CRC As Short = 98
    Public Const TERM9_OP_IH As Short = 99
    Public Const TERM9_OP_IHOL As Short = 100
    Public Const TERM9_OP_IL As Short = 101
    Public Const TERM9_OP_ILOH As Short = 102
    Public Const TERM9_OP_IOX As Short = 103
    Public Const TERM9_OP_IX As Short = 104
    Public Const TERM9_OP_KEEP As Short = 105
    Public Const TERM9_OP_MH As Short = 106
    Public Const TERM9_OP_ML As Short = 107
    Public Const TERM9_OP_MX As Short = 108
    Public Const TERM9_OP_OB As Short = 109
    Public Const TERM9_OP_OH As Short = 110
    Public Const TERM9_OP_OK As Short = 111
    Public Const TERM9_OP_OL As Short = 112
    Public Const TERM9_OP_OX As Short = 113
    Public Const TERM9_OP_TOG As Short = 114
    Public Const TERM9_OP_GROUP_UNKNOWN As Short = 115
    Public Const TERM9_OP_UNKNOWN As Short = 116
    Public Const TERM9_PATH_DISABLE As Short = 117
    Public Const TERM9_PATH_ENABLE As Short = 118
    Public Const TERM9_PRB_B As Short = 119
    Public Const TERM9_PRB_EH As Short = 120
    Public Const TERM9_PRB_EL As Short = 121
    Public Const TERM9_PRB_GB As Short = 122
    Public Const TERM9_PRB_GH As Short = 123
    Public Const TERM9_PRB_GL As Short = 124
    Public Const TERM9_PRB_H As Short = 125
    Public Const TERM9_PRB_ILLEGAL As Short = 126
    Public Const TERM9_PRB_L As Short = 127
    Public Const TERM9_PRB_MB As Short = 128
    Public Const TERM9_PROBE_METHOD_HANDLE As Short = 129
    Public Const TERM9_PRB_METHOD_HANDLE As Short = 129
    Public Const TERM9_PROBE_METHOD_MATRIX As Short = 130
    Public Const TERM9_PRB_METHOD_MATRIX As Short = 130
    Public Const TERM9_PROBE_METHOD_UNKNOWN As Short = 131
    Public Const TERM9_PRB_METHOD_UNKNOWN As Short = 131
    Public Const TERM9_PRB_MH As Short = 132
    Public Const TERM9_PRB_ML As Short = 133
    Public Const TERM9_PRB_PB As Short = 134
    Public Const TERM9_PRB_PH As Short = 135
    Public Const TERM9_PRB_PL As Short = 136
    Public Const TERM9_PRB_RB As Short = 137
    Public Const TERM9_PRB_RH As Short = 138
    Public Const TERM9_PRB_RL As Short = 139
    Public Const TERM9_PRB_X As Short = 140
    Public Const TERM9_PROBE_CAPTURE_STROBE As Short = 141
    Public Const TERM9_PROBE_CAPTURE_WINDOW As Short = 142
    Public Const TERM9_PROBE_CONFIG_LOAD As Short = 143
    Public Const TERM9_PROBE_CONFIG_RUN As Short = 144
    Public Const TERM9_PROBEMODE_DYNAMIC As Short = 145
    Public Const TERM9_PROBEMODE_STATIC As Short = 146
    Public Const TERM9_PS_CRC As Short = 147
    Public Const TERM9_PS_SIG As Short = 147
    Public Const TERM9_PS_IH As Short = 148
    Public Const TERM9_PS_IHOL As Short = 149
    Public Const TERM9_PS_IL As Short = 150
    Public Const TERM9_PS_ILOH As Short = 151
    Public Const TERM9_PS_IOX As Short = 152
    Public Const TERM9_PS_MH As Short = 153
    Public Const TERM9_PS_ML As Short = 154
    Public Const TERM9_PS_OB As Short = 155
    Public Const TERM9_PS_OH As Short = 156
    Public Const TERM9_PS_OK As Short = 157
    Public Const TERM9_PS_OL As Short = 158
    Public Const TERM9_PS_UNKNOWN As Short = 159
    Public Const TERM9_RELAY_CLOSED As Short = 160
    Public Const TERM9_RELAY_OPEN As Short = 161
    Public Const TERM9_RESULT_FAIL As Short = 162
    Public Const TERM9_RESULT_MBT As Short = 163
    Public Const TERM9_RESULT_NOT_RUN As Short = 164
    Public Const TERM9_RESULT_OVERFLOW As Short = 165
    Public Const TERM9_RESULT_PASS As Short = 166
    Public Const TERM9_RESULT_UNDERFLOW As Short = 167
    Public Const TERM9_RUN_IDLE As Short = 168
    Public Const TERM9_RUN_RUNNING As Short = 169
    Public Const TERM9_RUN_STOPPING As Short = 170
    Public Const TERM9_SLEWRATE_HIGH As Short = 171
    Public Const TERM9_SLEWRATE_LOW As Short = 172
    Public Const TERM9_SLEWRATE_MEDIUM As Short = 173
    Public Const TERM9_SOURCE_EXTERNAL As Short = 174
    Public Const TERM9_SOURCE_INTERNAL As Short = 175
    Public Const TERM9_STATE_HIGH As Short = 176
    Public Const TERM9_STATE_LOW As Short = 177
    Public Const TERM9_STMTX_GLOBAL As Short = 178
    Public Const TERM9_STMTX_LOCAL As Short = 179
    Public Const TERM9_SYNC_IDLE As Short = 180
    Public Const TERM9_SYNC_LISTEN As Short = 181
    Public Const TERM9_SYNC_TRIGGER As Short = 182
    Public Const TERM9_TIMING_SLOW As Short = 184
    Public Const TERM9_TIMING_STANDARD As Short = 185
    Public Const TERM9_TRIGGER_PATCLK As Short = 186
    Public Const TERM9_TRIGGER_SYSCLK As Short = 187
    Public Const TERM9_UCLK_TRIGGER_EXTCLK As Short = 188
    Public Const TERM9_UCLK_TRIGGER_SYSCLK As Short = 189
    Public Const TERM9_EDGE_BOTH As Short = 206
    Public Const TERM9_PATTERNSET_LEARN_UNKNOWN As Short = 207
    Public Const TERM9_PATTERNSET_PRECOUNT_UNKNOWN As Short = 208
    Public Const TERM9_FORMAT_UNKNOWN As Short = 209
    Public Const TERM9_COND_UNKNOWN As Short = 210
    Public Const TERM9_COP_UNKNOWN As Short = 211
    Public Const TERM9_SLEWRATE_UNKNOWN As Short = 212
    Public Const TERM9_SYNC_UNKNOWN As Short = 213
    Public Const TERM9_ADC_CONN_PRBHNDL As Short = 214
    Public Const TERM9_ADC_CONN_REFV As Short = 215
    Public Const TERM9_ATTRVAL_LAST As Short = 215
    Public Const TERM9_INT_SYSTEM_GROUND As Short = 222

    ' terM9_scope.h

    Public Const TERM9_SCOPE_LAST As Integer = 65535
    Public Const TERM9_SCOPE_FIRST_USER As Short = 0
    Public Const TERM9_SCOPE_LAST_USER As Short = 32767
    Public Const TERM9_SCOPE_FIRST_PREDEFINED As Integer = 32768
    Public Const TERM9_SCOPE_LAST_PREDEFINED As Integer = 65535
    Public Const TERM9_SCOPE_FIRST_UC_INDEX As Integer = 33068
    Public Const TERM9_SCOPE_LAST_UC_INDEX As Integer = 33167
    Public Const TERM9_SCOPE_FIRST_SR_INDEX As Integer = 33168
    Public Const TERM9_SCOPE_LAST_SR_INDEX As Integer = 33267
    Public Const TERM9_SCOPE_FIRST_CARD_INDEX As Integer = 32868
    Public Const TERM9_SCOPE_LAST_CARD_INDEX As Integer = 33067
    Public Const TERM9_SCOPE_FIRST_CHNGRP_INDEX As Integer = 33768
    Public Const TERM9_SCOPE_LAST_CHNGRP_INDEX As Integer = 33967
    Public Const TERM9_SCOPE_FIRST_PIN_INDEX As Integer = 33968
    Public Const TERM9_SCOPE_LAST_PIN_INDEX As Integer = 65535
    Public Const TERM9_SCOPE_UNDEFINED As Short = -1
    Public Const TERM9_SCOPE_ISGROUP As Short = -2
    Public Const TERM9_SCOPE_SYSTEM As Integer = 32768
    ' Global Const TERM9_SCOPE_CARD(n) (TERM9_SCOPE_FIRST_CARD_INDEX + (n))
    Public Const TERM9_SCOPE_CHANSYSTEM As Integer = 32769
    ' Global Const TERM9_SCOPE_CHANCARD(n) (TERM9_SCOPE_FIRST_CHNGRP_INDEX + (n) - 1)
    ' Global Const TERM9_SCOPE_CHAN(n) (TERM9_SCOPE_FIRST_PIN_INDEX + (n))


    Function TERM9_SCOPE_CARD(ByVal n As Short) As Integer
        TERM9_SCOPE_CARD = TERM9_SCOPE_FIRST_CARD_INDEX + n
    End Function

    Function TERM9_SCOPE_CHAN(ByVal n As Integer) As Integer
        TERM9_SCOPE_CHAN = TERM9_SCOPE_FIRST_PIN_INDEX + n
    End Function

    Function TERM9_SCOPE_CHANCARD(ByVal n As Integer) As Integer
        TERM9_SCOPE_CHANCARD = TERM9_SCOPE_FIRST_CHNGRP_INDEX + n - 1
    End Function
End Module