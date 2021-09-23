'/**
'*
'*  BTIEBR WIN32 DRIVER  Version 1.0.2  (01/27/2015)
'*  Copyright (c) 2013-2015
'*  Ballard Technology, Inc.
'*  www.ballardtech.com
'*  support@ballardtech.com
'*  ALL RIGHTS RESERVED
'*
'*  NAME:   BTIEBR.BAS -- VB6 32-bit
'*                       BTIEBR Driver Include Header File.
'*
'**/

'/**
'*
'*  Structs used by the BTIEBR Driver.
'*
'**/

Public Type XMITFIELDSEBR
	ctrlflags As Long	'//User writes message configuration optionvals
	flag1 As Integer	'//Card writes message flag 1
	flag2 As Integer	'//Card writes message flag 2
	errflags As Integer	'//Card writes errorval flags
	actflags As Integer	'//Card writes activity flags
	resptime As Integer	'//Card writes response time
	resv7 As Integer	'//Reserved field
	datacount As Integer	'//Card writes data count
	extflag As Integer	'//Driver writes external flags
	timetag As Long	'//Card writes time tag
	elapsetime As Long	'//Card writes elapsed time
	preaddr As Long	'//Driver writes pre-proc address
	postaddr As Long	'//Driver writes post-proc address
	timetagh As Long	'//Card writes extended timetag
	resv18 As Integer	'//Reserved field
	resv19 As Integer	'//Reserved field
	cwd As Integer	'//User writes command word
	cwdinfo As Integer	'//User writes command word info
	swd As Integer	'//Card writes status word
	swdinfo As Integer	'//Card writes status word info
	data(0 to 31) As Integer	'//User writes data words
	extra(0 to 7) As Integer	'//User writes extra words
End Type

Public Type MSGFIELDSEBR
	msgflag As Integer	'//Driver writes message flags
	msgopt As Integer	'//Driver writes message optionvals
	msgerr As Integer	'//Card writes message errorvals
	msgact As Integer	'//Card writes message activity
	resptime As Integer	'//Card writes response time
	resv5 As Integer	'//Reserved field
	datacount As Integer	'//Card writes data count
	extflag As Integer	'//Driver writes external flags
	timetag_hitcount As Long	'//Card writes time tag - '//Card writes hit count
	elapsetime_mintime_maxtime As Long	'//Card writes elapsed time - '//Card writes min time - '//Card writes max time
	preaddr As Long	'//Driver writes pre-proc address
	postaddr As Long	'//Driver writes post-proc address
	timetagh As Long	'//Card writes extended timetag
	resv18 As Integer	'//Reserved field
	resv19 As Integer	'//Reserved field
	cwd As Integer	'//Card/User writes command word (R/T)
	cwdinfo As Integer	'//Card/User writes command word info
	swd As Integer	'//Card writes status word
	swdinfo As Integer	'//Card writes status word info
	data(0 to 31) As Integer	'//User/Card writes data words (R/T)
	extra(0 to 7) As Integer	'//User/Card writes extra words (R/T)
End Type

'/**
'*
'*  BTIEBR Driver functions.
'*
'**/

'/**
'*
'*  Internal functions used by the BTIEBR Driver.
'*
'**/

Public Declare Function BTIEBR_BCConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_BCConfig@12" (ByVal configval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCConfigEx@16" (ByVal configval As Long, ByVal count As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCConfigMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCConfigMsg@12" (ByVal configval As Long, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCCreateList Lib "BTIEBR.DLL" Alias "_BTIEBR_BCCreateList@28" (ByVal listconfigval As Long, ByVal count As Long, ByVal msgconfigval As Long, ByVal cwd As Integer, ByRef data As Integer, ByVal rtlink As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCCreateMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCCreateMsg@20" (ByVal configval As Long, ByVal cwd As Integer, ByRef data As Integer, ByVal rtlink As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_BCPause Lib "BTIEBR.DLL" Alias "_BTIEBR_BCPause@8" (ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_BCPauseCheck Lib "BTIEBR.DLL" Alias "_BTIEBR_BCPauseCheck@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_BCResume Lib "BTIEBR.DLL" Alias "_BTIEBR_BCResume@8" (ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_BCSchedAgain Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedAgain@12" (ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedBranch Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedBranch@16" (ByVal condition As Long, ByVal destindex As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedBranchUpdate Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedBranchUpdate@16" (ByVal destindex As Long, ByVal opcodeindex As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedBuild Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedBuild@20" (ByVal nummsgs As Integer, ByRef msgaddr As Long, ByRef freq As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedCall Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedCall@16" (ByVal condition As Long, ByVal destindex As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedCallUpdate Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedCallUpdate@16" (ByVal destindex As Long, ByVal opcodeindex As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedEntry Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedEntry@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedFrame Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedFrame@12" (ByVal timeval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedFrameEnd Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedFrameEnd@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedFrameStart Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedFrameStart@12" (ByVal timeval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedGap Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedGap@12" (ByVal gapval As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedHalt Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedHalt@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedLog Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedLog@16" (ByVal condition As Long, ByVal tagval As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedMsg@12" (ByVal msgaddr As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedPause Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedPause@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedPulse Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedPulse@12" (ByVal dionum As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedPulse0 Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedPulse0@12" (ByVal dionum As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedPulse1 Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedPulse1@12" (ByVal dionum As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedRestart Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedRestart@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedRetry Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedRetry@16" (ByVal condition As Long, ByVal retries As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedReturn Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedReturn@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSetDefaultGap Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSetDefaultGap@12" (ByVal gapval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSetTimeout Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSetTimeout@12" (ByVal timeoutval As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSyncDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSyncDefine@20" (ByVal enableflag As Long, ByVal syncmask As Integer, ByVal pinpolarity As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCTransmitMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsg@12" (ByRef xmitfieldsval As XMITFIELDSEBR, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCTriggerDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTriggerDefine@24" (ByVal enableflag As Long, ByVal trigmask As Integer, ByVal trigval As Integer, ByVal pinpolarity As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChGetInfo Lib "BTIEBR.DLL" Alias "_BTIEBR_ChGetInfo@12" (ByVal infotype As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChIsBM Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsBM@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChIsEBR Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsEBR@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChIsM Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsM@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChIsS Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsS@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChStart Lib "BTIEBR.DLL" Alias "_BTIEBR_ChStart@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChStop Lib "BTIEBR.DLL" Alias "_BTIEBR_ChStop@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdMaxLoopRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdMaxLoopRd@8" (ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_CmdMaxLoopWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdMaxLoopWr@12" (ByVal countval As Integer, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_CmdShotRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdShotRd@12" (ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdShotWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdShotWr@16" (ByVal value As Long, ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdSkipRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdSkipRd@12" (ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdSkipWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdSkipWr@16" (ByVal value As Long, ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdStepRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStepRd@12" (ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdStepWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStepWr@16" (ByVal value As Long, ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ErrorCtrl Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorCtrl@12" (ByVal ctrlval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ErrorDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorDefine@28" (ByVal defineval As Long, ByVal errvalue As Integer, ByVal countval As Long, ByVal wordpos As Integer, ByVal bitpos As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ErrorSent Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorSent@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ErrorTagBC Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorTagBC@16" (ByVal tagval As Long, ByVal msgaddr As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ErrorTagRT Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorTagRT@28" (ByVal tagval As Long, ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ErrorTriggerDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorTriggerDefine@24" (ByVal enableflag As Long, ByVal trigmask As Integer, ByVal trigval As Integer, ByVal pinpolarity As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListBlockRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListBlockRd@12" (ByRef buf As Integer, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListBlockWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListBlockWr@12" (ByRef buf As Integer, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListDataRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListDataRd@16" (ByRef buf As Integer, ByVal count As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListDataWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListDataWr@16" (ByRef buf As Integer, ByVal count As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListMultiBlockRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListMultiBlockRd@16" (ByRef buf As Integer, ByRef blkcountptr As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListMultiBlockWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListMultiBlockWr@16" (ByRef buf As Integer, ByVal blkcount As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MonConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_MonConfig@12" (ByVal configval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MonFilterRTLink Lib "BTIEBR.DLL" Alias "_BTIEBR_MonFilterRTLink@12" (ByVal rtlinkmask As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MonFilterSA Lib "BTIEBR.DLL" Alias "_BTIEBR_MonFilterSA@28" (ByVal rtlink As Long, ByVal rcvsamask As Long, ByVal xmtsamask As Long, ByVal rcvmcmask As Long, ByVal xmtmcmask As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgBlockRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgBlockRd@12" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgBlockWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgBlockWr@12" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_MsgDataRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgDataRd@16" (ByRef buf As Integer, ByVal count As Long, ByVal msgaddr As Long, ByVal handleval As Long)
Public Declare Sub BTIEBR_MsgDataWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgDataWr@16" (ByRef buf As Integer, ByVal count As Long, ByVal msgaddr As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_MsgFieldRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgFieldRd@12" (ByVal fieldtype As Integer, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgFieldWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgFieldWr@16" (ByVal fieldval As Long, ByVal fieldtype As Integer, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_MsgGroupRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgGroupRd@16" (ByVal nummsgs As Long, ByRef msgflds As MSGFIELDSEBR, ByRef msgaddrptr As Long, ByVal handleval As Long)
Public Declare Sub BTIEBR_MsgGroupWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgGroupWr@16" (ByVal nummsgs As Long, ByRef msgflds As MSGFIELDSEBR, ByRef msgaddrptr As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_MsgSkipRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSkipRd@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_MsgSkipWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSkipWr@12" (ByVal skip As Long, ByVal msgaddr As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_MsgSyncDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSyncDefine@20" (ByVal enableflag As Long, ByVal syncmask As Integer, ByVal pinpolarity As Integer, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgTriggerDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgTriggerDefine@24" (ByVal enableflag As Long, ByVal trigmask As Integer, ByVal trigval As Integer, ByVal pinpolarity As Integer, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_PlayConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayConfig@16" (ByVal configval As Long, ByVal rtlinkmask As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_PlayStatus Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayStatus@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_PlayWr Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayWr@16" (ByRef buf As Integer, ByVal bufcount As Integer, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_RTConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_RTConfig@16" (ByVal configval As Long, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTCreateList Lib "BTIEBR.DLL" Alias "_BTIEBR_RTCreateList@36" (ByVal listconfigval As Long, ByVal count As Long, ByVal msgconfigval As Long, ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTCreateMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_RTCreateMsg@28" (ByVal configval As Long, ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTGetMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_RTGetMsg@24" (ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTReset Lib "BTIEBR.DLL" Alias "_BTIEBR_RTReset@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTResponseTimeSet Lib "BTIEBR.DLL" Alias "_BTIEBR_RTResponseTimeSet@16" (ByVal resptime As Long, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTSetMode Lib "BTIEBR.DLL" Alias "_BTIEBR_RTSetMode@16" (ByVal configval As Long, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTSWDRd Lib "BTIEBR.DLL" Alias "_BTIEBR_RTSWDRd@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_RTSWDWr Lib "BTIEBR.DLL" Alias "_BTIEBR_RTSWDWr@16" (ByVal swdval As Integer, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTSyncDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_RTSyncDefine@40" (ByVal enableflag As Long, ByVal syncmask As Integer, ByVal pinpolarity As Integer, ByVal rtlink As Long, ByVal rcvsamask As Long, ByVal xmtsamask As Long, ByVal rcvmcmask As Long, ByVal xmtmcmask As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ValPackCWD Lib "BTIEBR.DLL" Alias "_BTIEBR_ValPackCWD@16" (ByVal taval As Long, ByVal trflag As Long, ByVal saval As Long, ByVal wcval As Long) As Integer
Public Declare Sub BTIEBR_ValUnpackCWD Lib "BTIEBR.DLL" Alias "_BTIEBR_ValUnpackCWD@20" (ByVal cwdval As Integer, ByRef taval As Long, ByRef trflag As Long, ByRef saval As Long, ByRef wcval As Long)
Public Declare Function BTIEBR_BCCreateListEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCCreateListEx@32" (ByVal listconfigval As Long, ByVal skipval As Long, ByVal count As Long, ByVal msgconfigval As Long, ByVal cwd As Integer, ByRef data As Integer, ByVal rtlink As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCCreateListExx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCCreateListExx@32" (ByVal listconfigval As Long, ByVal skipval As Long, ByVal count As Long, ByVal msgconfigval As Long, ByVal cwd As Integer, ByRef data As Integer, ByVal rtlink As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCGetMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCGetMsg@12" (ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCIMGapRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BCIMGapRd@12" (ByRef gapval As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCIMGapWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BCIMGapWr@12" (ByVal gapval As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedBranchEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedBranchEx@28" (ByVal condition1 As Integer, ByVal condition2 As Integer, ByVal condition3 As Integer, ByVal opcode As Integer, ByVal destindex As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedCallEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedCallEx@28" (ByVal condition1 As Integer, ByVal condition2 As Integer, ByVal condition3 As Integer, ByVal opcode As Integer, ByVal destindex As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedLogEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedLogEx@28" (ByVal condition1 As Integer, ByVal condition2 As Integer, ByVal condition3 As Integer, ByVal opcode As Integer, ByVal tagval As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedNop Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedNop@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedRetryEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedRetryEx@28" (ByVal condition1 As Integer, ByVal condition2 As Integer, ByVal condition3 As Integer, ByVal retryflag As Integer, ByVal retries As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSchedUser Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedUser@12" (ByVal useraddr As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCSetEntry Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSetEntry@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCTransmitMsgBlkEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsgBlkEx@20" (ByVal writeflag As Long, ByVal count As Long, ByRef xmitfieldsval As XMITFIELDSEBR, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCTransmitMsgBlkInit Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsgBlkInit@12" (ByVal maxcount As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCTransmitMsgBlkRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsgBlkRd@16" (ByVal count As Long, ByRef xmitfieldsval As XMITFIELDSEBR, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BCTransmitMsgBlkWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsgBlkWr@16" (ByVal count As Long, ByRef xmitfieldsval As XMITFIELDSEBR, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BlkAddr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkAddr@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_BlkOptRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkOptRd@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_BlkOptWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkOptWr@16" (ByVal rtopt As Integer, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BlkPtrRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkPtrRd@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_BlkPtrWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkPtrWr@16" (ByVal descaddr As Long, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_BlkRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkRd@16" (ByRef block As Integer, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BlkReset Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkReset@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BlkSWDRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkSWDRd@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_BlkSWDWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkSWDWr@16" (ByVal swdval As Integer, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BlkWipe Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkWipe@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_BlkWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkWr@16" (ByRef block As Integer, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_ChConfig@20" (ByVal enableflag As Long, ByVal typeval As Long, ByVal rtlink As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ChIsRunning Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsRunning@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdAddr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdAddr@12" (ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdAlloc Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdAlloc@8" (ByVal count As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdAllocEx Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdAllocEx@12" (ByVal configval As Long, ByVal count As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdBaseRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdBaseRd@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_CmdBaseWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdBaseWr@12" (ByVal addrval As Long, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_CmdClear Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdClear@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdCountRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCountRd@8" (ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_CmdCountWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCountWr@12" (ByVal countval As Integer, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_CmdCtrlRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCtrlRd@12" (ByVal index As Long, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_CmdCtrlWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCtrlWr@16" (ByVal ctrlval As Integer, ByVal index As Long, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_CmdCurrRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCurrRd@8" (ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_CmdCurrWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCurrWr@12" (ByVal addrval As Integer, ByVal channum As Long, ByVal handleval As Long)
Public Declare Sub BTIEBR_CmdInit Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdInit@8" (ByVal opcode As Integer, ByRef block As Integer)
Public Declare Function BTIEBR_CmdInsert Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdInsert@12" (ByRef block As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_CmdRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdRd@12" (ByRef block As Integer, ByVal addrval As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_CmdRestart Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdRestart@8" (ByVal channum As Long, ByVal handleval As Long)
Public Declare Sub BTIEBR_CmdStackClr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStackClr@8" (ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_CmdStartRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStartRd@8" (ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_CmdStartWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStartWr@12" (ByVal addrval As Integer, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_CmdTotalRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdTotalRd@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_CmdTotalWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdTotalWr@12" (ByVal countval As Long, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_CmdWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdWr@12" (ByRef block As Integer, ByVal addrval As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DescAddr Lib "BTIEBR.DLL" Alias "_BTIEBR_DescAddr@24" (ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DescAlloc Lib "BTIEBR.DLL" Alias "_BTIEBR_DescAlloc@4" (ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DescConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_DescConfig@28" (ByVal configval As Long, ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DescConfigAll Lib "BTIEBR.DLL" Alias "_BTIEBR_DescConfigAll@16" (ByVal configval As Long, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DescOptRd Lib "BTIEBR.DLL" Alias "_BTIEBR_DescOptRd@24" (ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_DescOptWr Lib "BTIEBR.DLL" Alias "_BTIEBR_DescOptWr@28" (ByVal descopt As Integer, ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_DescPtrRd Lib "BTIEBR.DLL" Alias "_BTIEBR_DescPtrRd@24" (ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DescPtrWr Lib "BTIEBR.DLL" Alias "_BTIEBR_DescPtrWr@28" (ByVal msgaddr As Long, ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DescRd Lib "BTIEBR.DLL" Alias "_BTIEBR_DescRd@28" (ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByRef desc As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DescWr Lib "BTIEBR.DLL" Alias "_BTIEBR_DescWr@28" (ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByRef desc As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_DllUsageCount Lib "BTIEBR.DLL" Alias "_BTIEBR_DllUsageCount@0" () As Long
Public Declare Function BTIEBR_ErrorIsTaggedBC Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorIsTaggedBC@12" (ByVal msgaddr As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ErrorIsTaggedRT Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorIsTaggedRT@24" (ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ErrorResponse Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorResponse@12" (ByVal responseval As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_ErrorSync Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorSync@12" (ByVal value As Long, ByVal channum As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_ErrorTimeout Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorTimeout@12" (ByVal timeoutval As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_HostComm Lib "BTIEBR.DLL" Alias "_BTIEBR_HostComm@8" (ByVal lpparam As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_IsChan Lib "BTIEBR.DLL" Alias "_BTIEBR_IsChan@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_LHubConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubConfig@12" (ByVal configval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_LHubPortExists Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortExists@12" (ByVal portnum As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_LHubPortGetCount Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortGetCount@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_LHubPortGetRTLink Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortGetRTLink@12" (ByVal portnum As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_LHubPortIsAvail Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortIsAvail@12" (ByVal portnum As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_LHubPortIsAvailMask Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortIsAvailMask@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_LHubPortSetRTLink Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortSetRTLink@16" (ByVal rtlink As Long, ByVal portnum As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_LHubRTLinkGetPort Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubRTLinkGetPort@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListAddr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListAddr@12" (ByVal index As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListAddrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListAddrEx@16" (ByVal index As Long, ByVal listaddr As Long, ByRef header As Integer, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListBlockRdEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListBlockRdEx@16" (ByRef buf As Integer, ByVal index As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListBlockWrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListBlockWrEx@16" (ByRef buf As Integer, ByVal index As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListClear Lib "BTIEBR.DLL" Alias "_BTIEBR_ListClear@8" (ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_ListConfig@16" (ByVal configval As Long, ByVal count As Long, ByVal skipval As Integer, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListConfigEx@16" (ByVal configval As Long, ByVal count As Long, ByVal skipval As Integer, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListDataRdEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListDataRdEx@20" (ByRef buf As Integer, ByVal count As Long, ByVal index As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListDataWrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListDataWrEx@20" (ByRef buf As Integer, ByVal count As Long, ByVal index As Long, ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListInit Lib "BTIEBR.DLL" Alias "_BTIEBR_ListInit@12" (ByVal listaddr As Long, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListNextRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListNextRd@12" (ByVal listaddr As Long, ByRef header As Integer, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ListNextWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListNextWr@12" (ByVal listaddr As Long, ByRef header As Integer, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_ListPostRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListPostRd@12" (ByVal listaddr As Long, ByRef header As Integer, ByVal handleval As Long)
Public Declare Sub BTIEBR_ListPostWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListPostWr@12" (ByVal listaddr As Long, ByRef header As Integer, ByVal handleval As Long)
Public Declare Sub BTIEBR_ListPreRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListPreRd@12" (ByVal listaddr As Long, ByRef header As Integer, ByVal handleval As Long)
Public Declare Sub BTIEBR_ListPreWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListPreWr@12" (ByVal listaddr As Long, ByRef header As Integer, ByVal handleval As Long)
Public Declare Function BTIEBR_ListStatus Lib "BTIEBR.DLL" Alias "_BTIEBR_ListStatus@8" (ByVal listaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ModeIsBC Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsBC@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsMon Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsMon@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsNotBC Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotBC@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsNotMon Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotMon@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsNotPlay Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotPlay@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsNotRT Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotRT@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsNotSerial Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotSerial@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsPlay Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsPlay@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsRT Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsRT@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_ModeIsSerial Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsSerial@4" (ByVal chanflag As Integer) As Long
Public Declare Function BTIEBR_MonConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_MonConfigEx@20" (ByVal configval As Long, ByVal moncount As Long, ByVal cardnum As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MonFilterSAEx Lib "BTIEBR.DLL" Alias "_BTIEBR_MonFilterSAEx@28" (ByVal rtlink As Long, ByVal rcvsamask As Long, ByVal xmtsamask As Long, ByVal rcvmcmask As Long, ByVal xmtmcmask As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgCheck Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCheck@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_MsgCommRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCommRd@12" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgCommWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCommWr@12" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgConfig@8" (ByVal configval As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgConfigEx@12" (ByVal configval As Long, ByVal cwd As Integer, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgCWDLinkRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCWDLinkRd@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_MsgCWDLinkWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCWDLinkWr@12" (ByVal cwdlink As Integer, ByVal msgaddr As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_MsgCWDRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCWDRd@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_MsgCWDWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCWDWr@12" (ByVal cwd As Integer, ByVal msgaddr As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_MsgErrorRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgErrorRd@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_MsgErrorWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgErrorWr@12" (ByVal value As Integer, ByVal msgaddr As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_MsgFlagRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgFlagRd@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_MsgFlagWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgFlagWr@12" (ByVal msgflag As Integer, ByVal msgaddr As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_MsgIsAccessed Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgIsAccessed@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgOptRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgOptRd@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Integer
Public Declare Sub BTIEBR_MsgOptWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgOptWr@12" (ByVal msgopt As Integer, ByVal msgaddr As Long, ByVal handleval As Long)
Public Declare Function BTIEBR_MsgReset Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgReset@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgResetEx Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgResetEx@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_MsgSWDLinkRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSWDLinkRd@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_MsgSWDRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSWDRd@8" (ByVal msgaddr As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_PlayClear Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayClear@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_PlayConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayConfigEx@20" (ByVal configval As Long, ByVal playcount As Long, ByVal rtlinkmask As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_PlayCounterWr Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayCounterWr@12" (ByVal totalcount As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_PlayStatusEx Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayStatusEx@12" (ByRef totalcount As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_PlayWrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayWrEx@16" (ByRef buf As Integer, ByVal bufcount As Integer, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_RTConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_RTConfigEx@16" (ByVal configval As Long, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTConfigExx Lib "BTIEBR.DLL" Alias "_BTIEBR_RTConfigExx@16" (ByVal configval As Long, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTCreateListEx Lib "BTIEBR.DLL" Alias "_BTIEBR_RTCreateListEx@40" (ByVal listconfigval As Long, ByVal skipval As Long, ByVal count As Long, ByVal msgconfigval As Long, ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTCreateListExx Lib "BTIEBR.DLL" Alias "_BTIEBR_RTCreateListExx@40" (ByVal listconfigval As Long, ByVal skipval As Long, ByVal count As Long, ByVal msgconfigval As Long, ByVal mcflag As Long, ByVal rtlink As Long, ByVal trflag As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTResponseTimeRd Lib "BTIEBR.DLL" Alias "_BTIEBR_RTResponseTimeRd@16" (ByRef timeval As Integer, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTResponseTimeWr Lib "BTIEBR.DLL" Alias "_BTIEBR_RTResponseTimeWr@16" (ByVal timeval As Integer, ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_RTStatus Lib "BTIEBR.DLL" Alias "_BTIEBR_RTStatus@12" (ByVal rtlink As Long, ByVal channum As Long, ByVal handleval As Long) As Integer
Public Declare Function BTIEBR_SABlkWr Lib "BTIEBR.DLL" Alias "_BTIEBR_SABlkWr@16" (ByVal rtlink As Long, ByVal saval As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SABlkWrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_SABlkWrEx@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SchedBuild Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedBuild@20" (ByVal nummsgs As Long, ByRef msgaddr As Long, ByRef freq As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SchedBuildEx Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedBuildEx@32" (ByVal nummsgs As Long, ByRef msgaddr As Long, ByRef freq As Long, ByRef period As Long, ByRef buf As Long, ByVal bufsize As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SchedBuildExx Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedBuildExx@32" (ByVal frametime As Long, ByVal nummsgs As Long, ByRef msgaddr As Long, ByRef period As Long, ByRef buf As Long, ByVal bufsize As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SchedCalc Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedCalc@12" (ByRef msgaddr As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Sub BTIEBR_SchedDisplay Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedDisplay@8" (ByVal nummsgs As Long, ByRef count As Long)
Public Declare Sub BTIEBR_SchedHook Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedHook@8" (ByVal index As Long, ByVal ptr As Long)
Public Declare Function BTIEBR_SchedInsEnd Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedInsEnd@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SchedInsMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedInsMsg@16" (ByVal index As Long, ByRef msgaddr As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SchedInsStart Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedInsStart@16" (ByVal frametime As Long, ByVal count As Long, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SchedMaxval Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedMaxval@8" (ByRef arrayval As Long, ByVal arrayvalcount As Long) As Long
Public Declare Function BTIEBR_SchedMinval Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedMinval@8" (ByRef arrayval As Long, ByVal arrayvalcount As Long) As Long
Public Declare Function BTIEBR_SerialClear Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialClear@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SerialConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialConfig@16" (ByVal configval As Long, ByVal count As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SerialConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialConfigEx@16" (ByVal configval As Long, ByVal count As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SerialStatus Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialStatus@8" (ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_SerialWr Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialWr@12" (ByRef buf As Integer, ByVal channum As Long, ByVal handleval As Long) As Long
Public Declare Function BTIEBR_TestProtocol Lib "BTIEBR.DLL" Alias "_BTIEBR_TestProtocol@4" (ByVal handleval As Long) As Long
Public Declare Function BTIEBR_ValPackRTCWD Lib "BTIEBR.DLL" Alias "_BTIEBR_ValPackRTCWD@20" (ByVal mcflag As Long, ByVal taval As Long, ByVal trflag As Long, ByVal saval As Long, ByVal count As Long) As Integer
Public Declare Sub BTIEBR_ValUnpackRTCWD Lib "BTIEBR.DLL" Alias "_BTIEBR_ValUnpackRTCWD@28" (ByVal cwd As Integer, ByRef bcastflag As Long, ByRef mcflag As Long, ByRef taval As Long, ByRef trflag As Long, ByRef saval As Long, ByRef count As Long)

'/**
'*
'*  Channel number constants
'*
'**/

Public Const CH0 As Integer = &H0    '//Channel 0
Public Const CH1 As Integer = &H1    '//Channel 1
Public Const CH2 As Integer = &H2    '//Channel 2
Public Const CH3 As Integer = &H3    '//Channel 3
Public Const CH4 As Integer = &H4    '//Channel 4
Public Const CH5 As Integer = &H5    '//Channel 5
Public Const CH6 As Integer = &H6    '//Channel 6
Public Const CH7 As Integer = &H7    '//Channel 7
Public Const CH8 As Integer = &H8    '//Channel 8
Public Const CH9 As Integer = &H9    '//Channel 9
Public Const CH10 As Integer = &HA   '//Channel 10
Public Const CH11 As Integer = &HB   '//Channel 11
Public Const CH12 As Integer = &HC   '//Channel 12
Public Const CH13 As Integer = &HD   '//Channel 13
Public Const CH14 As Integer = &HE   '//Channel 14
Public Const CH15 As Integer = &HF   '//Channel 15
Public Const CH16 As Integer = &H10   '//Channel 16
Public Const CH17 As Integer = &H11   '//Channel 17
Public Const CH18 As Integer = &H12   '//Channel 18
Public Const CH19 As Integer = &H13   '//Channel 19
Public Const CH20 As Integer = &H14   '//Channel 20
Public Const CH21 As Integer = &H15   '//Channel 21
Public Const CH22 As Integer = &H16   '//Channel 22
Public Const CH23 As Integer = &H17   '//Channel 23
Public Const CH24 As Integer = &H18   '//Channel 24
Public Const CH25 As Integer = &H19   '//Channel 25
Public Const CH26 As Integer = &H1A   '//Channel 26
Public Const CH27 As Integer = &H1B   '//Channel 27
Public Const CH28 As Integer = &H1C   '//Channel 28
Public Const CH29 As Integer = &H1D   '//Channel 29
Public Const CH30 As Integer = &H1E   '//Channel 30
Public Const CH31 As Integer = &H1F   '//Channel 31

'/**
'*
'*  Logical hub configuration options
'*
'**/

Public Const LHUBCFGEBR_DEFAULT As Long = &H0    '//Select all default settings
Public Const LHUBCFGEBR_ENABLE As Long = &H0 '//Enable all external ports (default)
Public Const LHUBCFGEBR_DISABLE As Long = &H1    '//Disable all external ports
Public Const LHUBCFGEBR_SPEC As Long = &H0   '//LHub will be configured for Spec mode (default)
Public Const LHUBCFGEBR_SWITCH As Long = &H10 '//LHub will be configured for Switch mode
Public Const LHUBCFGEBR_LINK As Long = &H20   '//LHub will be configured for Link mode

'/**
'*
'*  Bus controller configuration options
'*
'**/

Public Const BCCFGEBR_DEFAULT As Long = &H0  '//Select all default settings
Public Const BCCFGEBR_ENABLE As Long = &H0   '//Enable bus controller (default)
Public Const BCCFGEBR_DISABLE As Long = &H1  '//Disable bus controller
Public Const BCCFGEBR_NOLOGHALT As Long = &H0    '//Do not generate event log on halt (default)
Public Const BCCFGEBR_LOGHALT As Long = &H100  '//Generate event log on halt
Public Const BCCFGEBR_NOLOGPAUSE As Long = &H0   '//Do not generate event log on pause (default)
Public Const BCCFGEBR_LOGPAUSE As Long = &H200 '//Generate event log on pause
Public Const BCCFGEBR_NOLOOPMAX As Long = &H0    '//Disable maximum loop count (default)
Public Const BCCFGEBR_LOOPMAX As Long = &H1000  '//Enable maximum loop count
Public Const BCCFGEBR_NOSTEP As Long = &H0   '//Disable single stepping (default)
Public Const BCCFGEBR_STEP As Long = &H2000 '//Enable single stepping
Public Const BCCFGEBR_MC01 As Long = &H0 '//Select SA=00000 or SA=11111 for mode codes (default)
Public Const BCCFGEBR_MC1 As Long = &H1000000  '//Select SA=11111 for mode codes
Public Const BCCFGEBR_MC0 As Long = &H2000000  '//Select SA=00000 for mode codes
Public Const BCCFGEBR_MCNONE As Long = &H3000000   '//Disable mode codes
Public Const BCCFGEBR_PAUSE As Long = &H400    '//Mark channel as paused
Public Const BCCFGEBR_UNPAUSE As Long = &H0  '//Mark channel as unpaused (default)
Public Const BCCFGEBR_SELFTEST As Long = &H800000 '//This channel will transmit/receive on the internal self-test bus
Public Const BCCFGEBR_SELFTESTOFF As Long = &H0  '//This channel will transmit/receive on the operational bus (default)

'/**
'*
'*  Monitor configuration options
'*
'**/

Public Const MONCFGEBR_DEFAULT As Long = &H0 '//Select all default settings
Public Const MONCFGEBR_ENABLE As Long = &H0  '//Enable monitor
Public Const MONCFGEBR_DISABLE As Long = &H1 '//Disable monitor
Public Const MONCFGEBR_NOBCAST As Long = &H0 '//Disable broadcast (default)
Public Const MONCFGEBR_BCAST As Long = &H100   '//Enable broadcast
Public Const MONCFGEBR_COMPLETE As Long = &H0    '//Enable receiving complete words (default)
Public Const MONCFGEBR_INCOMPLETE As Long = &H10000  '//Enable receiving incomplete words
Public Const MONCFGEBR_MC01 As Long = &H0    '//Select SA=00000 or SA=11111 for mode codes (default)
Public Const MONCFGEBR_MC1 As Long = &H100000 '//Select SA=11111 for mode codes
Public Const MONCFGEBR_MC0 As Long = &H200000 '//Select SA=00000 for mode codes
Public Const MONCFGEBR_MCNONE As Long = &H300000  '//Disable mode codes
Public Const MONCFGEBR_SELFTEST As Long = &H800000    '//This channel will transmit/receive on the internal self-test bus
Public Const MONCFGEBR_SELFTESTOFF As Long = &H0 '//This channel will transmit/receive on the operational bus (default)

'/**
'*
'*  Remote terminal configuration options
'*
'**/

Public Const RTCFGEBR_DEFAULT As Long = &H0  '//Select all default settings
Public Const RTCFGEBR_SIMULATE As Long = &H0 '//Enable RT simulation (default)
Public Const RTCFGEBR_DISABLE As Long = &H1  '//Disable RT
Public Const RTCFGEBR_MONITOR As Long = &H2  '//Enable RT monitor
Public Const RTCFGEBR_NOBCAST As Long = &H0  '//Disable broadcast (default)
Public Const RTCFGEBR_BCAST As Long = &H100    '//Enable broadcast
Public Const RTCFGEBR_NOAUTOBUSY As Long = &H0   '//Disable auto busy (default)
Public Const RTCFGEBR_AUTOBUSY As Long = &H200 '//Enable auto busy
Public Const RTCFGEBR_BUILD As Long = &H0    '//Enable auto building (default)
Public Const RTCFGEBR_NOBUILD As Long = &H400  '//Disable auto building
Public Const RTCFGEBR_NOIMMCLR As Long = &H0 '//Do not clear status word bits (default)
Public Const RTCFGEBR_IMMCLR As Long = &H2000   '//Status word bits are cleared immediately
Public Const RTCFGEBR_NOBCASTADDR As Long = &H0  '//Disable broadcast handling for address (default)
Public Const RTCFGEBR_BCASTADDR As Long = &H4000    '//Enable broadcast handling for address
Public Const RTCFGEBR_MC01 As Long = &H0 '//Select SA=00000 or SA=11111 for mode codes (default)
Public Const RTCFGEBR_MC1 As Long = &H1000000  '//Select SA=11111 for mode codes
Public Const RTCFGEBR_MC0 As Long = &H2000000  '//Select SA=00000 for mode codes
Public Const RTCFGEBR_MCNONE As Long = &H3000000   '//Disable mode codes
Public Const RTCFGEBR_MCILLEGAL As Long = &H4000000    '//Illegalize mode codes defined as "Reserved - Not to be Used" in EBR
Public Const RTCFGEBR_WIPE As Long = &H0 '//Enables message clear (default)
Public Const RTCFGEBR_NOWIPE As Long = &H80000000   '//Disables message clear
Public Const RTCFGEBR_WIPE0 As Long = &H0    '//Initialize data with zeros (default)
Public Const RTCFGEBR_WIPE123 As Long = &H10000000  '//Initialize data with incrementing values
Public Const RTCFGEBR_WIPECWD As Long = &H20000000  '//Initialize data with command word
Public Const RTCFGEBR_SELFTEST As Long = &H800000 '//This channel will transmit/receive on the internal self-test bus
Public Const RTCFGEBR_SELFTESTOFF As Long = &H0  '//This channel will transmit/receive on the operational bus (default)

'/**
'*
'*  Playback configuration options
'*
'**/

Public Const PLAYCFGEBR_DEFAULT As Long = &H0    '//Select all default settings
Public Const PLAYCFGEBR_ENABLE As Long = &H0 '//Enable Playback mode
Public Const PLAYCFGEBR_DISABLE As Long = &H1    '//Disable Playback mode
Public Const PLAYCFGEBR_16K As Long = &H0    '//Allocate a 16K playback buffer (default)
Public Const PLAYCFGEBR_ALLAVAIL As Long = &H10   '//Allocate all available memory to playback buffer
Public Const PLAYCFGEBR_32K As Long = &H20    '//Allocate a 32K playback buffer
Public Const PLAYCFGEBR_64K As Long = &H40    '//Allocate a 64K playback buffer
Public Const PLAYCFGEBR_128K As Long = &H80   '//Allocate a 128K playback buffer
Public Const PLAYCFGEBR_MC01 As Long = &H0   '//Select SA=00000 or SA=11111 for mode codes (default)
Public Const PLAYCFGEBR_MC1 As Long = &H1000000    '//Select SA=11111 for mode codes
Public Const PLAYCFGEBR_MC0 As Long = &H2000000    '//Select SA=00000 for mode codes
Public Const PLAYCFGEBR_MCNONE As Long = &H3000000 '//Disable mode codes

'/**
'*
'*  Serial configuration options
'*
'**/

Public Const SERCFGEBR_DEFAULT As Long = &H0 '//Select all default settings
Public Const SERCFGEBR_CONTINUOUS As Long = &H0  '//Enable continuous mode (default)
Public Const SERCFGEBR_DISABLE As Long = &H1 '//Disable free mode
Public Const SERCFGEBR_FREE As Long = &H2    '//Enable free mode
Public Const SERCFGEBR_NOLOGEMPTY As Long = &H0  '//Do not generate event log when empty (default)
Public Const SERCFGEBR_LOGEMPTY As Long = &H10    '//Generate event log when empty

'/**
'*
'*  Error configuration options
'*
'**/

Public Const ERRDEFEBR_NONE As Long = &H0    '//Disable error generation (default)
Public Const ERRDEFEBR_GAP As Long = &H10000 '//Generate preceding gap error
Public Const ERRDEFEBR_CNTWRDABS As Long = &H20000   '//Generate an absolute word count error
Public Const ERRDEFEBR_CNTWRDREL As Long = &H40000   '//Generate a relative count error
Public Const ERRDEFEBR_CNTBIT As Long = &H80000  '//Generate a bit count error
Public Const ERRDEFEBR_MAN1 As Long = &H100000    '//Generate manchester error on first half of bit
Public Const ERRDEFEBR_MAN2 As Long = &H200000    '//Generate manchester error on second half of bit
Public Const ERRDEFEBR_SYNC As Long = &H400000    '//Generate a sync error
Public Const ERRDEFEBR_PAR As Long = &H800000 '//Generate a parity error
Public Const ERRDEFEBR_ZEROX1 As Long = &H1000000  '//Generate zero crossing error on leading zero crossing (if exists)
Public Const ERRDEFEBR_ZEROX2 As Long = &H2000000  '//Generate zero crossing error on mid-bit zero crossing
Public Const ERRDEFEBR_DWD00 As Long = &H0   '//Error will be generated in data word  0
Public Const ERRDEFEBR_DWD01 As Long = &H1   '//Error will be generated in data word  1
Public Const ERRDEFEBR_DWD02 As Long = &H2   '//Error will be generated in data word  2
Public Const ERRDEFEBR_DWD03 As Long = &H3   '//Error will be generated in data word  3
Public Const ERRDEFEBR_DWD04 As Long = &H4   '//Error will be generated in data word  4
Public Const ERRDEFEBR_DWD05 As Long = &H5   '//Error will be generated in data word  5
Public Const ERRDEFEBR_DWD06 As Long = &H6   '//Error will be generated in data word  6
Public Const ERRDEFEBR_DWD07 As Long = &H7   '//Error will be generated in data word  7
Public Const ERRDEFEBR_DWD08 As Long = &H8   '//Error will be generated in data word  8
Public Const ERRDEFEBR_DWD09 As Long = &H9   '//Error will be generated in data word  9
Public Const ERRDEFEBR_DWD10 As Long = &HA   '//Error will be generated in data word 10
Public Const ERRDEFEBR_DWD11 As Long = &HB   '//Error will be generated in data word 11
Public Const ERRDEFEBR_DWD12 As Long = &HC   '//Error will be generated in data word 12
Public Const ERRDEFEBR_DWD13 As Long = &HD   '//Error will be generated in data word 13
Public Const ERRDEFEBR_DWD14 As Long = &HE   '//Error will be generated in data word 14
Public Const ERRDEFEBR_DWD15 As Long = &HF   '//Error will be generated in data word 15
Public Const ERRDEFEBR_DWD16 As Long = &H10   '//Error will be generated in data word 16
Public Const ERRDEFEBR_DWD17 As Long = &H11   '//Error will be generated in data word 17
Public Const ERRDEFEBR_DWD18 As Long = &H12   '//Error will be generated in data word 18
Public Const ERRDEFEBR_DWD19 As Long = &H13   '//Error will be generated in data word 19
Public Const ERRDEFEBR_DWD20 As Long = &H14   '//Error will be generated in data word 20
Public Const ERRDEFEBR_DWD21 As Long = &H15   '//Error will be generated in data word 21
Public Const ERRDEFEBR_DWD22 As Long = &H16   '//Error will be generated in data word 22
Public Const ERRDEFEBR_DWD23 As Long = &H17   '//Error will be generated in data word 23
Public Const ERRDEFEBR_DWD24 As Long = &H18   '//Error will be generated in data word 24
Public Const ERRDEFEBR_DWD25 As Long = &H19   '//Error will be generated in data word 25
Public Const ERRDEFEBR_DWD26 As Long = &H1A   '//Error will be generated in data word 26
Public Const ERRDEFEBR_DWD27 As Long = &H1B   '//Error will be generated in data word 27
Public Const ERRDEFEBR_DWD28 As Long = &H1C   '//Error will be generated in data word 28
Public Const ERRDEFEBR_DWD29 As Long = &H1D   '//Error will be generated in data word 29
Public Const ERRDEFEBR_DWD30 As Long = &H1E   '//Error will be generated in data word 30
Public Const ERRDEFEBR_DWD31 As Long = &H1F   '//Error will be generated in data word 31
Public Const ERRDEFEBR_CWD As Long = &H100 '//Error will be generated in CWD
Public Const ERRDEFEBR_SWD As Long = &H102 '//Error will be generated in SWD

'/**
'*
'*  Error control options
'*
'**/

Public Const ERRCTRLEBR_OFF As Long = &H0    '//Disable error generation
Public Const ERRCTRLEBR_ON As Long = &H1 '//Enable error generation
Public Const ERRCTRLEBR_ONCE As Long = &H2   '//Enable error generation once
Public Const ERRCTRLEBR_TAGMSG As Long = &H0 '//Generate errors on tagged messages
Public Const ERRCTRLEBR_ANYMSG As Long = &H80000000 '//Generate errors on any message

'/**
'*
'*  Message configuration options
'*
'**/

Public Const MSGCRTEBR_DEFAULT As Long = &H0 '//Default settings
Public Const MSGCRTEBR_ENABLE As Long = &H0  '//Enable subaddress (RT) (default)
Public Const MSGCRTEBR_DISABLE As Long = &H1000 '//Disable subaddress (RT)
Public Const MSGCRTEBR_RESP As Long = &H0    '//Enable response (RT) (default)
Public Const MSGCRTEBR_NORESP As Long = &H2000  '//Disable response (RT)
Public Const MSGCRTEBR_NOWRAP As Long = &H0  '//Disable data wrap (RT) (default)
Public Const MSGCRTEBR_WRAP As Long = &H4000    '//Enable data wrap (RT)
Public Const MSGCRTEBR_NOLOG As Long = &H0   '//Message will not generate event log (default)
Public Const MSGCRTEBR_LOG As Long = &H1 '//Message will generate event log
Public Const MSGCRTEBR_NOERR As Long = &H0   '//No message error will be generated for message (default)
Public Const MSGCRTEBR_ERR As Long = &H2 '//A message error will be generated for message
Public Const MSGCRTEBR_BCRT As Long = &H0    '//Message is a BC-RT transfer (BC) (default)
Public Const MSGCRTEBR_NOMON As Long = &H0   '//Message will not be monitored (default)
Public Const MSGCRTEBR_MON As Long = &H10 '//Message will be monitored
Public Const MSGCRTEBR_NOTIMETAG As Long = &H0   '//Message will not record time-tag (default)
Public Const MSGCRTEBR_TIMETAG As Long = &H40 '//Message will record time-tag
Public Const MSGCRTEBR_NOELAPSE As Long = &H0    '//Message will not record elapse time (default)
Public Const MSGCRTEBR_ELAPSE As Long = &H80  '//Message will record elapse time
Public Const MSGCRTEBR_NOMIN As Long = &H0   '//Message will not record min time (default)
Public Const MSGCRTEBR_MIN As Long = &H100 '//Message will record min time
Public Const MSGCRTEBR_NOMAX As Long = &H0   '//Message will not record max time (default)
Public Const MSGCRTEBR_MAX As Long = &H200 '//Message will record max time
Public Const MSGCRTEBR_NOHIT As Long = &H0   '//Message will not record hit count (default)
Public Const MSGCRTEBR_HIT As Long = &H400 '//Message will record hit count
Public Const MSGCRTEBR_WIPE As Long = &H0    '//Enables message clear (default)
Public Const MSGCRTEBR_NOWIPE As Long = &H80000000  '//Disables message clear
Public Const MSGCRTEBR_WIPE0 As Long = &H0   '//Initialize data with zeros (default)
Public Const MSGCRTEBR_WIPE123 As Long = &H1000000 '//Initialize data with incrementing values
Public Const MSGCRTEBR_WIPECWD As Long = &H2000000 '//Initialize data with command word

'/**
'*
'*  List buffer options
'*
'**/

Public Const LISTCRTEBR_DEFAULT As Long = &H0    '//Select all default settings
Public Const LISTCRTEBR_FIFO As Long = &H0   '//Enable FIFO mode
Public Const LISTCRTEBR_CIRCULAR As Long = &H1   '//Enable circular mode
Public Const LISTCRTEBR_TRBIT As Long = &H0  '//User will read or write depending upon T/R bit
Public Const LISTCRTEBR_READ As Long = &H10   '//User will read from list buffer
Public Const LISTCRTEBR_WRITE As Long = &H20  '//User will write to list buffer
Public Const LISTCRTEBR_NOTSKIP As Long = &H0    '//Will not skip messages
Public Const LISTCRTEBR_SKIP As Long = &H40   '//Enable message skipping
Public Const LISTCRTEBR_ASYNC As Long = &H80  '//Asynchronous mode
Public Const LISTCRTEBR_NOLOG As Long = &H0  '//Do not generate event log when list buffer empty/full (default)
Public Const LISTCRTEBR_LOG As Long = &H100    '//Generate event log when list buffer empty/full
Public Const LISTCRTEBR_NOHIT As Long = &H0  '//Do not generate event log when list is hit (default)
Public Const LISTCRTEBR_HIT As Long = &H200    '//Generate event log when list is hit
Public Const LISTCRTEBR_NOLOGHALF As Long = &H0  '//Do not generate event log on middle and last entries (default)
Public Const LISTCRTEBR_LOGHALF As Long = &H400    '//Generate event log on middle and last entries

'/**
'*
'*  Condition flags.
'*
'**/

Public Const CONDEBR_ALWAYS As Long = &H0    '//Unconditional
Public Const CONDEBR_FAIL As Long = &H1  '//Condition when all retries have failed
Public Const CONDEBR_SRQ As Long = &H2   '//Condition on Service Request bit set in status word
Public Const CONDEBR_INS As Long = &H4   '//Condition on Instrumentation bit set in status word
Public Const CONDEBR_SSF As Long = &H8   '//Condition on Subsystem Fail bit set in status word
Public Const CONDEBR_TF As Long = &H10    '//Condition on Terminal Flag bit set in status word
Public Const CONDEBR_BUSY As Long = &H20  '//Condition on Busy bit set in status word
Public Const CONDEBR_ME As Long = &H40    '//Condition on Message Error bit set in status word
Public Const CONDEBR_RESPERR As Long = &H80   '//Condition on response error
Public Const CONDEBR_NORESP As Long = &H100    '//Condition on no response error
Public Const CONDEBR_DIO1ACT As Long = &H1000   '//Condition on digital I/O #1 active
Public Const CONDEBR_DIO1NACT As Long = &H2000  '//Condition on digital I/O #1 not active
Public Const CONDEBR_DIO2ACT As Long = &H4000   '//Condition on digital I/O #2 active
Public Const CONDEBR_DIO2NACT As Long = &H8000  '//Condition on digital I/O #2 not active
Public Const CONDEBR_DIO3ACT As Long = &H10000   '//Condition on digital I/O #3 active
Public Const CONDEBR_DIO3NACT As Long = &H20000  '//Condition on digital I/O #3 not active
Public Const CONDEBR_DIO4ACT As Long = &H40000   '//Condition on digital I/O #4 active
Public Const CONDEBR_DIO4NACT As Long = &H80000  '//Condition on digital I/O #4 not active

'/**
'*
'*  Bit fields of the status word
'*
'**/

Public Const SWDEBR_TAMASK As Long = &HF800 '//Masks off RT address in the SWD
Public Const SWDEBR_ME As Long = &H400 '//Message error bit
Public Const SWDEBR_INST As Long = &H200   '//Instrumentation bit
Public Const SWDEBR_SRQ As Long = &H100    '//Service request bit
Public Const SWDEBR_BCAST As Long = &H10  '//Broadcast received bit
Public Const SWDEBR_BUSY As Long = &H8   '//Busy bit
Public Const SWDEBR_SSF As Long = &H4    '//Subsystem flag bit
Public Const SWDEBR_DBC As Long = &H2    '//Dynamic bus control acceptance bit
Public Const SWDEBR_TF As Long = &H1 '//Terminal flag bit

'/**
'*
'*  Bit fields of the message error flag
'*
'**/

Public Const MSGERREBR_NORESP As Long = &H8000  '//Set when we timeout waiting for response
Public Const MSGERREBR_ANYERR As Long = &H4000  '//Set when any error bit is set
Public Const MSGERREBR_PROTOCOL As Long = &H2000    '//General protocol error
Public Const MSGERREBR_SYNC As Long = &H1000    '//Set when we have the wrong sync
Public Const MSGERREBR_DATACOUNT As Long = &H800   '//Set when improper number of data words
Public Const MSGERREBR_LHUB As Long = &H40    '//Set when unexpected LHUB activity detected
Public Const MSGERREBR_MANCH As Long = &H20   '//Set when manchester error detected
Public Const MSGERREBR_PARITY As Long = &H10  '//Set when parity error detected
Public Const MSGERREBR_WORD As Long = &H8    '//Set when word error detected
Public Const MSGERREBR_RETRY As Long = &H4   '//All retries failed
Public Const MSGERREBR_SYSTEM As Long = &H2  '//An internal system error occurred
Public Const MSGERREBR_HIT As Long = &H1 '//Always set

'/**
'*
'*  Bit fields of the message activity flag.
'*
'**/

Public Const MSGACTEBR_CHMASK As Long = &HF800  '//Channel number mask value
Public Const MSGACTEBR_CHSHIFT As Long = &HB '//Channel number shift value
Public Const MSGACTEBR_XMTCWD As Long = &H80  '//Transmitted CWD
Public Const MSGACTEBR_XMTSWD As Long = &H40  '//Transmitted SWD
Public Const MSGACTEBR_RCVCWD As Long = &H20  '//Received CWD
Public Const MSGACTEBR_RCVSWD As Long = &H10  '//Received SWD
Public Const MSGACTEBR_XMTDWD As Long = &H8  '//Transmitted DWD
Public Const MSGACTEBR_RCVDWD As Long = &H4  '//Received DWD
Public Const MSGACTEBR_MODEMASK As Long = &H3    '//LHub Mode mask value
Public Const MSGACTEBR_MODESHIFT As Long = &H0   '//LHub Mode shift value

'/**
'*
'*  Bit fields within the command info (cwdinfo).
'*
'**/

Public Const CWDINFOEBR_RESVMASK As Long = &HFFE0   '//Reserved mask value
Public Const CWDINFOEBR_LINKMASK As Long = &H1F   '//Link number mask value
Public Const CWDINFOEBR_LINKSHIFT As Long = &H0  '//Link number shift value

'/**
'*
'*  Bit fields within the status info (swdinfo).
'*
'**/

Public Const SWDINFOEBR_RESVMASK As Long = &HFFE0   '//Reserved mask value
Public Const SWDINFOEBR_LINKMASK As Long = &H1F   '//Link number mask value
Public Const SWDINFOEBR_LINKSHIFT As Long = &H0  '//Link number shift value

'/**
'*
'*  Message activity LHub Mode values.
'*
'**/

Public Const LHUBMODE_SPEC As Long = &H0 '//LHub mode is Spec
Public Const LHUBMODE_SWITCH As Long = &H1   '//LHub mode is Switch
Public Const LHUBMODE_LINK As Long = &H2 '//LHub mode is Link

'/**
'*
'*  Field types.
'*
'**/

Public Const FIELDEBR_CWD As Long = &H0  '//CWD field
Public Const FIELDEBR_CWDINFO As Long = &H1  '//CWD info field
Public Const FIELDEBR_SWD As Long = &H2  '//SWD field
Public Const FIELDEBR_SWDINFO As Long = &H3  '//SWD info field
Public Const FIELDEBR_TTAG As Long = &H4 '//Time tag field
Public Const FIELDEBR_ELAPSE As Long = &H5   '//Elapse time field
Public Const FIELDEBR_ERROR As Long = &H6    '//Error field
Public Const FIELDEBR_ACT As Long = &H7  '//Activity field
Public Const FIELDEBR_RESP As Long = &H8 '//Response time field
Public Const FIELDEBR_COUNT As Long = &H9    '//Data count field
Public Const FIELDEBR_FLAG1 As Long = &HA    '//Flag 1 field
Public Const FIELDEBR_FLAG2 As Long = &HB    '//Flag 2 field
Public Const FIELDEBR_EXTFLAG As Long = &HC  '//External flag field
Public Const FIELDEBR_TTAGH As Long = &HD    '//Extended time tag field

'/**
'*
'*  Channel info fields.
'*
'**/

Public Const INFOEBR_MODEL As Long = &H1 '//Channel model type
Public Const INFOEBR_MAXCOUNT As Long = &H2  '//Channel terminal count
Public Const INFOEBR_MULTIMODE As Long = &H3 '//Channel supports multi-mode
Public Const INFOEBR_ERRORGEN As Long = &H4  '//Channel supports error generation
Public Const INFOEBR_CONCURMON As Long = &H5 '//Channel supports concurrent monitor
Public Const INFOEBR_MONFILTER As Long = &H6 '//Channel supports filtering of monitor
Public Const INFOEBR_PARAM As Long = &H7 '//Channel supports parametric control
Public Const INFOEBR_RTRESPTIME As Long = &H8    '//Channel supports programmable RT response time
Public Const INFOEBR_PLAYBACK As Long = &H9  '//Channel supports hardware playback mode
Public Const INFOEBR_VER2CHOBJ As Long = &HA '//Channel supports version 2 channel object
Public Const INFOEBR_BCDFLTGAP As Long = &HB '//Channel supports programmable default gap time

'/**
'*
'*  Other flags.
'*
'**/

Public Const SUBADDRESS As Integer = &H0 '//Selects Subaddress messages
Public Const MODECODE As Integer = &H1   '//Selects Modecode messages
Public Const EBR_DISABLED As Integer = -1   '//Flag setting/indicating a disabled RT link or port
