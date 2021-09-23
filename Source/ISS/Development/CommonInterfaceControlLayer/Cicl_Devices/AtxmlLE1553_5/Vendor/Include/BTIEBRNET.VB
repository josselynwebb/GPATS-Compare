Imports System
Imports System.Runtime.InteropServices

Module BTIEBRVB

    '/**
    '*
    '*  BTIEBR WIN32 DRIVER  Version 1.0.2  (01/27/2015)
    '*  Copyright (c) 2013-2015
    '*  Ballard Technology, Inc.
    '*  www.ballardtech.com
    '*  support@ballardtech.com
    '*  ALL RIGHTS RESERVED
    '*
    '*  NAME:   BTIEBRNET.VB -- VB.NET 32-bit/64-bit
    '*                       BTIEBR Driver Include Header File.
    '*
    '**/

    '/**
    '*
    '*  Structs used by the BTIEBR Driver.
    '*
    '**/

    <StructLayout(LayoutKind.Sequential)> Public Structure XMITFIELDSEBR
        Public ctrlflags As UInt32  '//User writes message configuration optionvals
        Public flag1 As UInt16  '//Card writes message flag 1
        Public flag2 As UInt16  '//Card writes message flag 2
        Public errflags As UInt16   '//Card writes errorval flags
        Public actflags As UInt16   '//Card writes activity flags
        Public resptime As UInt16   '//Card writes response time
        Public resv7 As UInt16  '//Reserved field
        Public datacount As UInt16  '//Card writes data count
        Public extflag As UInt16    '//Driver writes external flags
        Public timetag As UInt32    '//Card writes time tag
        Public elapsetime As UInt32 '//Card writes elapsed time
        Public preaddr As UInt32    '//Driver writes pre-proc address
        Public postaddr As UInt32   '//Driver writes post-proc address
        Public timetagh As UInt32   '//Card writes extended timetag
        Public resv18 As UInt16 '//Reserved field
        Public resv19 As UInt16 '//Reserved field
        Public cwd As UInt16    '//User writes command word
        Public cwdinfo As UInt16    '//User writes command word info
        Public swd As UInt16    '//Card writes status word
        Public swdinfo As UInt16    '//Card writes status word info
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> Public data() As UInt16    '//User writes data words
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> Public extra() As UInt16    '//User writes extra words
    End Structure

    <StructLayout(LayoutKind.Sequential)> Public Structure MSGFIELDSEBR
        Public msgflag As UInt16    '//Driver writes message flags
        Public msgopt As UInt16 '//Driver writes message optionvals
        Public msgerr As UInt16 '//Card writes message errorvals
        Public msgact As UInt16 '//Card writes message activity
        Public resptime As UInt16   '//Card writes response time
        Public resv5 As UInt16  '//Reserved field
        Public datacount As UInt16  '//Card writes data count
        Public extflag As UInt16    '//Driver writes external flags
        Public timetag_hitcount As UInt32   '//Card writes time tag - '//Card writes hit count
        Public elapsetime_mintime_maxtime As UInt32 '//Card writes elapsed time - '//Card writes min time - '//Card writes max time
        Public preaddr As UInt32    '//Driver writes pre-proc address
        Public postaddr As UInt32   '//Driver writes post-proc address
        Public timetagh As UInt32   '//Card writes extended timetag
        Public resv18 As UInt16 '//Reserved field
        Public resv19 As UInt16 '//Reserved field
        Public cwd As UInt16    '//Card/User writes command word (R/T)
        Public cwdinfo As UInt16    '//Card/User writes command word info
        Public swd As UInt16    '//Card writes status word
        Public swdinfo As UInt16    '//Card writes status word info
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> Public data() As UInt16    '//User/Card writes data words (R/T)
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> Public extra() As UInt16    '//User/Card writes extra words (R/T)
    End Structure

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

    Private Class BTIEBR_32
        Public Declare Function BTIEBR_BCConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_BCConfig@12" (ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCConfigEx@16" (ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCConfigMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCConfigMsg@12" (ByVal configval As UInt32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCCreateList Lib "BTIEBR.DLL" Alias "_BTIEBR_BCCreateList@28" (ByVal listconfigval As UInt32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCCreateMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCCreateMsg@20" (ByVal configval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_BCPause Lib "BTIEBR.DLL" Alias "_BTIEBR_BCPause@8" (ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_BCPauseCheck Lib "BTIEBR.DLL" Alias "_BTIEBR_BCPauseCheck@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_BCResume Lib "BTIEBR.DLL" Alias "_BTIEBR_BCResume@8" (ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_BCSchedAgain Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedAgain@12" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedBranch Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedBranch@16" (ByVal condition As UInt32, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedBranchUpdate Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedBranchUpdate@16" (ByVal destindex As Int32, ByVal opcodeindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedBuild Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedBuild@20" (ByVal nummsgs As UInt16, ByVal msgaddr As UInt32(), ByVal freq As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedCall Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedCall@16" (ByVal condition As UInt32, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedCallUpdate Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedCallUpdate@16" (ByVal destindex As Int32, ByVal opcodeindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedEntry Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedEntry@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedFrame Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedFrame@12" (ByVal timeval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedFrameEnd Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedFrameEnd@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedFrameStart Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedFrameStart@12" (ByVal timeval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedGap Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedGap@12" (ByVal gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedHalt Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedHalt@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedLog Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedLog@16" (ByVal condition As UInt32, ByVal tagval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedMsg@12" (ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedPause Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedPause@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedPulse Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedPulse@12" (ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedPulse0 Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedPulse0@12" (ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedPulse1 Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedPulse1@12" (ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedRestart Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedRestart@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedRetry Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedRetry@16" (ByVal condition As UInt32, ByVal retries As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedReturn Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedReturn@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSetDefaultGap Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSetDefaultGap@12" (ByVal gapval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSetTimeout Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSetTimeout@12" (ByVal timeoutval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSyncDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSyncDefine@20" (ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsg@12" (ByRef xmitfieldsval As XMITFIELDSEBR, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTriggerDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTriggerDefine@24" (ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChGetInfo Lib "BTIEBR.DLL" Alias "_BTIEBR_ChGetInfo@12" (ByVal infotype As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ChIsBM Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsBM@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChIsEBR Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsEBR@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChIsM Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsM@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChIsS Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsS@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChStart Lib "BTIEBR.DLL" Alias "_BTIEBR_ChStart@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChStop Lib "BTIEBR.DLL" Alias "_BTIEBR_ChStop@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdMaxLoopRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdMaxLoopRd@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdMaxLoopWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdMaxLoopWr@12" (ByVal countval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdShotRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdShotRd@12" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdShotWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdShotWr@16" (ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdSkipRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdSkipRd@12" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdSkipWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdSkipWr@16" (ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdStepRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStepRd@12" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdStepWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStepWr@16" (ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorCtrl Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorCtrl@12" (ByVal ctrlval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorDefine@28" (ByVal defineval As UInt32, ByVal errvalue As UInt16, ByVal countval As Int32, ByVal wordpos As UInt16, ByVal bitpos As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorSent Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorSent@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorTagBC Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorTagBC@16" (ByVal tagval As Int32, ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorTagRT Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorTagRT@28" (ByVal tagval As Int32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorTriggerDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorTriggerDefine@24" (ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListBlockRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListBlockRd@12" (ByVal buf As UInt16(), ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListBlockWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListBlockWr@12" (ByVal buf As UInt16(), ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListDataRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListDataRd@16" (ByVal buf As UInt16(), ByVal count As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListDataWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListDataWr@16" (ByVal buf As UInt16(), ByVal count As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListMultiBlockRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListMultiBlockRd@16" (ByVal buf As UInt16(), ByRef blkcountptr As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListMultiBlockWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListMultiBlockWr@16" (ByVal buf As UInt16(), ByVal blkcount As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MonConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_MonConfig@12" (ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MonFilterRTLink Lib "BTIEBR.DLL" Alias "_BTIEBR_MonFilterRTLink@12" (ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MonFilterSA Lib "BTIEBR.DLL" Alias "_BTIEBR_MonFilterSA@28" (ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MsgBlockRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgBlockRd@12" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgBlockWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgBlockWr@12" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_MsgDataRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgDataRd@16" (ByVal buf As UInt16(), ByVal count As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_MsgDataWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgDataWr@16" (ByVal buf As UInt16(), ByVal count As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgFieldRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgFieldRd@12" (ByVal fieldtype As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgFieldWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgFieldWr@16" (ByVal fieldval As UInt32, ByVal fieldtype As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_MsgGroupRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgGroupRd@16" (ByVal nummsgs As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal msgflds As MSGFIELDSEBR(), ByVal msgaddrptr As UInt32(), ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_MsgGroupWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgGroupWr@16" (ByVal nummsgs As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal msgflds As MSGFIELDSEBR(), ByVal msgaddrptr As UInt32(), ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgSkipRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSkipRd@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_MsgSkipWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSkipWr@12" (ByVal skip As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgSyncDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSyncDefine@20" (ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MsgTriggerDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgTriggerDefine@24" (ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayConfig@16" (ByVal configval As UInt32, ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayStatus Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayStatus@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayWr Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayWr@16" (ByVal buf As UInt16(), ByVal bufcount As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_RTConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_RTConfig@16" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTCreateList Lib "BTIEBR.DLL" Alias "_BTIEBR_RTCreateList@36" (ByVal listconfigval As UInt32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTCreateMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_RTCreateMsg@28" (ByVal configval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTGetMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_RTGetMsg@24" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTReset Lib "BTIEBR.DLL" Alias "_BTIEBR_RTReset@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTResponseTimeSet Lib "BTIEBR.DLL" Alias "_BTIEBR_RTResponseTimeSet@16" (ByVal resptime As Int32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTSetMode Lib "BTIEBR.DLL" Alias "_BTIEBR_RTSetMode@16" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTSWDRd Lib "BTIEBR.DLL" Alias "_BTIEBR_RTSWDRd@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_RTSWDWr Lib "BTIEBR.DLL" Alias "_BTIEBR_RTSWDWr@16" (ByVal swdval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTSyncDefine Lib "BTIEBR.DLL" Alias "_BTIEBR_RTSyncDefine@40" (ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ValPackCWD Lib "BTIEBR.DLL" Alias "_BTIEBR_ValPackCWD@16" (ByVal taval As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal wcval As Int32) As UInt16
        Public Declare Sub BTIEBR_ValUnpackCWD Lib "BTIEBR.DLL" Alias "_BTIEBR_ValUnpackCWD@20" (ByVal cwdval As UInt16, ByRef taval As Int32, ByRef trflag As Int32, ByRef saval As Int32, ByRef wcval As Int32)
        Public Declare Function BTIEBR_BCCreateListEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCCreateListEx@32" (ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCCreateListExx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCCreateListExx@32" (ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCGetMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_BCGetMsg@12" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCIMGapRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BCIMGapRd@12" (ByRef gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCIMGapWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BCIMGapWr@12" (ByVal gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedBranchEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedBranchEx@28" (ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedCallEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedCallEx@28" (ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedLogEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedLogEx@28" (ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal tagval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedNop Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedNop@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedRetryEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedRetryEx@28" (ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal retryflag As UInt16, ByVal retries As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedUser Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSchedUser@12" (ByVal useraddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSetEntry Lib "BTIEBR.DLL" Alias "_BTIEBR_BCSetEntry@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsgBlkEx Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsgBlkEx@20" (ByVal writeflag As Int32, ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=1)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsgBlkInit Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsgBlkInit@12" (ByVal maxcount As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsgBlkRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsgBlkRd@16" (ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsgBlkWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BCTransmitMsgBlkWr@16" (ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkAddr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkAddr@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_BlkOptRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkOptRd@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_BlkOptWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkOptWr@16" (ByVal rtopt As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkPtrRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkPtrRd@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_BlkPtrWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkPtrWr@16" (ByVal descaddr As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_BlkRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkRd@16" (ByVal block As UInt16(), ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkReset Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkReset@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkSWDRd Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkSWDRd@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_BlkSWDWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkSWDWr@16" (ByVal swdval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkWipe Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkWipe@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkWr Lib "BTIEBR.DLL" Alias "_BTIEBR_BlkWr@16" (ByVal block As UInt16(), ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_ChConfig@20" (ByVal enableflag As Int32, ByVal typeval As Int32, ByVal rtlink As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChIsRunning Lib "BTIEBR.DLL" Alias "_BTIEBR_ChIsRunning@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdAddr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdAddr@12" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_CmdAlloc Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdAlloc@8" (ByVal count As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_CmdAllocEx Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdAllocEx@12" (ByVal configval As UInt32, ByVal count As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_CmdBaseRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdBaseRd@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_CmdBaseWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdBaseWr@12" (ByVal addrval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdClear Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdClear@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdCountRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCountRd@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdCountWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCountWr@12" (ByVal countval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdCtrlRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCtrlRd@12" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdCtrlWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCtrlWr@16" (ByVal ctrlval As UInt16, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdCurrRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCurrRd@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdCurrWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdCurrWr@12" (ByVal addrval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_CmdInit Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdInit@8" (ByVal opcode As UInt16, ByVal block As UInt16())
        Public Declare Function BTIEBR_CmdInsert Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdInsert@12" (ByVal block As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdRd@12" (ByVal block As UInt16(), ByVal addrval As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_CmdRestart Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdRestart@8" (ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_CmdStackClr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStackClr@8" (ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdStartRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStartRd@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdStartWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdStartWr@12" (ByVal addrval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdTotalRd Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdTotalRd@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_CmdTotalWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdTotalWr@12" (ByVal countval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdWr Lib "BTIEBR.DLL" Alias "_BTIEBR_CmdWr@12" (ByVal block As UInt16(), ByVal addrval As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_DescAddr Lib "BTIEBR.DLL" Alias "_BTIEBR_DescAddr@24" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescAlloc Lib "BTIEBR.DLL" Alias "_BTIEBR_DescAlloc@4" (ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_DescConfig@28" (ByVal configval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_DescConfigAll Lib "BTIEBR.DLL" Alias "_BTIEBR_DescConfigAll@16" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_DescOptRd Lib "BTIEBR.DLL" Alias "_BTIEBR_DescOptRd@24" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_DescOptWr Lib "BTIEBR.DLL" Alias "_BTIEBR_DescOptWr@28" (ByVal descopt As UInt16, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_DescPtrRd Lib "BTIEBR.DLL" Alias "_BTIEBR_DescPtrRd@24" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescPtrWr Lib "BTIEBR.DLL" Alias "_BTIEBR_DescPtrWr@28" (ByVal msgaddr As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescRd Lib "BTIEBR.DLL" Alias "_BTIEBR_DescRd@28" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal desc As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescWr Lib "BTIEBR.DLL" Alias "_BTIEBR_DescWr@28" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal desc As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DllUsageCount Lib "BTIEBR.DLL" Alias "_BTIEBR_DllUsageCount@0" () As Int32
        Public Declare Function BTIEBR_ErrorIsTaggedBC Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorIsTaggedBC@12" (ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorIsTaggedRT Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorIsTaggedRT@24" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorResponse Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorResponse@12" (ByVal responseval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_ErrorSync Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorSync@12" (ByVal value As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_ErrorTimeout Lib "BTIEBR.DLL" Alias "_BTIEBR_ErrorTimeout@12" (ByVal timeoutval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_HostComm Lib "BTIEBR.DLL" Alias "_BTIEBR_HostComm@8" (ByVal lpparam As IntPtr, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_IsChan Lib "BTIEBR.DLL" Alias "_BTIEBR_IsChan@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubConfig@12" (ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortExists Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortExists@12" (ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortGetCount Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortGetCount@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortGetRTLink Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortGetRTLink@12" (ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortIsAvail Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortIsAvail@12" (ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortIsAvailMask Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortIsAvailMask@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_LHubPortSetRTLink Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubPortSetRTLink@16" (ByVal rtlink As Int32, ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubRTLinkGetPort Lib "BTIEBR.DLL" Alias "_BTIEBR_LHubRTLinkGetPort@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListAddr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListAddr@12" (ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListAddrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListAddrEx@16" (ByVal index As Int32, ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListBlockRdEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListBlockRdEx@16" (ByVal buf As UInt16(), ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListBlockWrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListBlockWrEx@16" (ByVal buf As UInt16(), ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListClear Lib "BTIEBR.DLL" Alias "_BTIEBR_ListClear@8" (ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_ListConfig@16" (ByVal configval As UInt32, ByVal count As Int32, ByVal skipval As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListConfigEx@16" (ByVal configval As UInt32, ByVal count As Int32, ByVal skipval As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListDataRdEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListDataRdEx@20" (ByVal buf As UInt16(), ByVal count As Int32, ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListDataWrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_ListDataWrEx@20" (ByVal buf As UInt16(), ByVal count As Int32, ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListInit Lib "BTIEBR.DLL" Alias "_BTIEBR_ListInit@12" (ByVal listaddr As UInt32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListNextRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListNextRd@12" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListNextWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListNextWr@12" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_ListPostRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListPostRd@12" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_ListPostWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListPostWr@12" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_ListPreRd Lib "BTIEBR.DLL" Alias "_BTIEBR_ListPreRd@12" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_ListPreWr Lib "BTIEBR.DLL" Alias "_BTIEBR_ListPreWr@12" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_ListStatus Lib "BTIEBR.DLL" Alias "_BTIEBR_ListStatus@8" (ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ModeIsBC Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsBC@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsMon Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsMon@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotBC Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotBC@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotMon Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotMon@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotPlay Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotPlay@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotRT Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotRT@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotSerial Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsNotSerial@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsPlay Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsPlay@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsRT Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsRT@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsSerial Lib "BTIEBR.DLL" Alias "_BTIEBR_ModeIsSerial@4" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_MonConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_MonConfigEx@20" (ByVal configval As UInt32, ByVal moncount As UInt32, ByVal cardnum As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MonFilterSAEx Lib "BTIEBR.DLL" Alias "_BTIEBR_MonFilterSAEx@28" (ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MsgCheck Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCheck@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_MsgCommRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCommRd@12" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgCommWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCommWr@12" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgConfig@8" (ByVal configval As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgConfigEx@12" (ByVal configval As UInt32, ByVal cwd As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgCWDLinkRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCWDLinkRd@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgCWDLinkWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCWDLinkWr@12" (ByVal cwdlink As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgCWDRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCWDRd@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgCWDWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgCWDWr@12" (ByVal cwd As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgErrorRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgErrorRd@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgErrorWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgErrorWr@12" (ByVal value As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgFlagRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgFlagRd@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgFlagWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgFlagWr@12" (ByVal msgflag As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgIsAccessed Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgIsAccessed@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MsgOptRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgOptRd@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgOptWr Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgOptWr@12" (ByVal msgopt As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgReset Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgReset@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgResetEx Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgResetEx@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgSWDLinkRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSWDLinkRd@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_MsgSWDRd Lib "BTIEBR.DLL" Alias "_BTIEBR_MsgSWDRd@8" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_PlayClear Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayClear@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayConfigEx@20" (ByVal configval As UInt32, ByVal playcount As UInt32, ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayCounterWr Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayCounterWr@12" (ByVal totalcount As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_PlayStatusEx Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayStatusEx@12" (ByRef totalcount As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayWrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_PlayWrEx@16" (ByVal buf As UInt16(), ByVal bufcount As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_RTConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_RTConfigEx@16" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTConfigExx Lib "BTIEBR.DLL" Alias "_BTIEBR_RTConfigExx@16" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTCreateListEx Lib "BTIEBR.DLL" Alias "_BTIEBR_RTCreateListEx@40" (ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTCreateListExx Lib "BTIEBR.DLL" Alias "_BTIEBR_RTCreateListExx@40" (ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTResponseTimeRd Lib "BTIEBR.DLL" Alias "_BTIEBR_RTResponseTimeRd@16" (ByRef timeval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTResponseTimeWr Lib "BTIEBR.DLL" Alias "_BTIEBR_RTResponseTimeWr@16" (ByVal timeval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTStatus Lib "BTIEBR.DLL" Alias "_BTIEBR_RTStatus@12" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_SABlkWr Lib "BTIEBR.DLL" Alias "_BTIEBR_SABlkWr@16" (ByVal rtlink As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SABlkWrEx Lib "BTIEBR.DLL" Alias "_BTIEBR_SABlkWrEx@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedBuild Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedBuild@20" (ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal freq As Int32(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedBuildEx Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedBuildEx@32" (ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal freq As Int32(), ByVal period As Int32(), ByVal buf As IntPtr(), ByVal bufsize As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedBuildExx Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedBuildExx@32" (ByVal frametime As UInt32, ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal period As Int32(), ByVal buf As IntPtr(), ByVal bufsize As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedCalc Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedCalc@12" (ByRef msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_SchedDisplay Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedDisplay@8" (ByVal nummsgs As UInt32, ByRef count As UInt32)
        Public Declare Sub BTIEBR_SchedHook Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedHook@8" (ByVal index As Int32, ByVal ptr As IntPtr)
        Public Declare Function BTIEBR_SchedInsEnd Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedInsEnd@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedInsMsg Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedInsMsg@16" (ByVal index As UInt32, ByRef msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedInsStart Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedInsStart@16" (ByVal frametime As UInt32, ByVal count As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedMaxval Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedMaxval@8" (ByVal arrayval As UInt32(), ByVal arrayvalcount As UInt32) As UInt32
        Public Declare Function BTIEBR_SchedMinval Lib "BTIEBR.DLL" Alias "_BTIEBR_SchedMinval@8" (ByVal arrayval As UInt32(), ByVal arrayvalcount As UInt32) As UInt32
        Public Declare Sub BTIEBR_SchedPrint Lib "BTIEBR.DLL" Alias "" (ByVal ParamArray list() As String)
        Public Declare Function BTIEBR_SerialClear Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialClear@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SerialConfig Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialConfig@16" (ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SerialConfigEx Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialConfigEx@16" (ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SerialStatus Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialStatus@8" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SerialWr Lib "BTIEBR.DLL" Alias "_BTIEBR_SerialWr@12" (ByVal buf As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_TestProtocol Lib "BTIEBR.DLL" Alias "_BTIEBR_TestProtocol@4" (ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ValPackRTCWD Lib "BTIEBR.DLL" Alias "_BTIEBR_ValPackRTCWD@20" (ByVal mcflag As Int32, ByVal taval As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal count As Int32) As UInt16
        Public Declare Sub BTIEBR_ValUnpackRTCWD Lib "BTIEBR.DLL" Alias "_BTIEBR_ValUnpackRTCWD@28" (ByVal cwd As UInt16, ByRef bcastflag As Int32, ByRef mcflag As Int32, ByRef taval As Int32, ByRef trflag As Int32, ByRef saval As Int32, ByRef count As Int32)
    End Class

    Private Class BTIEBR_64
        Public Declare Function BTIEBR_BCConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_BCConfig" (ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCConfigEx Lib "BTIEBR64.DLL" Alias "BTIEBR_BCConfigEx" (ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCConfigMsg Lib "BTIEBR64.DLL" Alias "BTIEBR_BCConfigMsg" (ByVal configval As UInt32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCCreateList Lib "BTIEBR64.DLL" Alias "BTIEBR_BCCreateList" (ByVal listconfigval As UInt32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCCreateMsg Lib "BTIEBR64.DLL" Alias "BTIEBR_BCCreateMsg" (ByVal configval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_BCPause Lib "BTIEBR64.DLL" Alias "BTIEBR_BCPause" (ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_BCPauseCheck Lib "BTIEBR64.DLL" Alias "BTIEBR_BCPauseCheck" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_BCResume Lib "BTIEBR64.DLL" Alias "BTIEBR_BCResume" (ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_BCSchedAgain Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedAgain" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedBranch Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedBranch" (ByVal condition As UInt32, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedBranchUpdate Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedBranchUpdate" (ByVal destindex As Int32, ByVal opcodeindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedBuild Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedBuild" (ByVal nummsgs As UInt16, ByVal msgaddr As UInt32(), ByVal freq As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedCall Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedCall" (ByVal condition As UInt32, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedCallUpdate Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedCallUpdate" (ByVal destindex As Int32, ByVal opcodeindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedEntry Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedEntry" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedFrame Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedFrame" (ByVal timeval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedFrameEnd Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedFrameEnd" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedFrameStart Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedFrameStart" (ByVal timeval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedGap Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedGap" (ByVal gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedHalt Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedHalt" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedLog Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedLog" (ByVal condition As UInt32, ByVal tagval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedMsg Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedMsg" (ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedPause Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedPause" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedPulse Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedPulse" (ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedPulse0 Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedPulse0" (ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedPulse1 Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedPulse1" (ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedRestart Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedRestart" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedRetry Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedRetry" (ByVal condition As UInt32, ByVal retries As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedReturn Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedReturn" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSetDefaultGap Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSetDefaultGap" (ByVal gapval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSetTimeout Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSetTimeout" (ByVal timeoutval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSyncDefine Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSyncDefine" (ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsg Lib "BTIEBR64.DLL" Alias "BTIEBR_BCTransmitMsg" (ByRef xmitfieldsval As XMITFIELDSEBR, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTriggerDefine Lib "BTIEBR64.DLL" Alias "BTIEBR_BCTriggerDefine" (ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChGetInfo Lib "BTIEBR64.DLL" Alias "BTIEBR_ChGetInfo" (ByVal infotype As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ChIsBM Lib "BTIEBR64.DLL" Alias "BTIEBR_ChIsBM" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChIsEBR Lib "BTIEBR64.DLL" Alias "BTIEBR_ChIsEBR" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChIsM Lib "BTIEBR64.DLL" Alias "BTIEBR_ChIsM" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChIsS Lib "BTIEBR64.DLL" Alias "BTIEBR_ChIsS" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChStart Lib "BTIEBR64.DLL" Alias "BTIEBR_ChStart" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChStop Lib "BTIEBR64.DLL" Alias "BTIEBR_ChStop" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdMaxLoopRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdMaxLoopRd" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdMaxLoopWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdMaxLoopWr" (ByVal countval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdShotRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdShotRd" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdShotWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdShotWr" (ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdSkipRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdSkipRd" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdSkipWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdSkipWr" (ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdStepRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdStepRd" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdStepWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdStepWr" (ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorCtrl Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorCtrl" (ByVal ctrlval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorDefine Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorDefine" (ByVal defineval As UInt32, ByVal errvalue As UInt16, ByVal countval As Int32, ByVal wordpos As UInt16, ByVal bitpos As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorSent Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorSent" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorTagBC Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorTagBC" (ByVal tagval As Int32, ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorTagRT Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorTagRT" (ByVal tagval As Int32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorTriggerDefine Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorTriggerDefine" (ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListBlockRd Lib "BTIEBR64.DLL" Alias "BTIEBR_ListBlockRd" (ByVal buf As UInt16(), ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListBlockWr Lib "BTIEBR64.DLL" Alias "BTIEBR_ListBlockWr" (ByVal buf As UInt16(), ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListDataRd Lib "BTIEBR64.DLL" Alias "BTIEBR_ListDataRd" (ByVal buf As UInt16(), ByVal count As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListDataWr Lib "BTIEBR64.DLL" Alias "BTIEBR_ListDataWr" (ByVal buf As UInt16(), ByVal count As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListMultiBlockRd Lib "BTIEBR64.DLL" Alias "BTIEBR_ListMultiBlockRd" (ByVal buf As UInt16(), ByRef blkcountptr As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListMultiBlockWr Lib "BTIEBR64.DLL" Alias "BTIEBR_ListMultiBlockWr" (ByVal buf As UInt16(), ByVal blkcount As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MonConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_MonConfig" (ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MonFilterRTLink Lib "BTIEBR64.DLL" Alias "BTIEBR_MonFilterRTLink" (ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MonFilterSA Lib "BTIEBR64.DLL" Alias "BTIEBR_MonFilterSA" (ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MsgBlockRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgBlockRd" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgBlockWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgBlockWr" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_MsgDataRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgDataRd" (ByVal buf As UInt16(), ByVal count As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_MsgDataWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgDataWr" (ByVal buf As UInt16(), ByVal count As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgFieldRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgFieldRd" (ByVal fieldtype As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgFieldWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgFieldWr" (ByVal fieldval As UInt32, ByVal fieldtype As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_MsgGroupRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgGroupRd" (ByVal nummsgs As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal msgflds As MSGFIELDSEBR(), ByVal msgaddrptr As UInt32(), ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_MsgGroupWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgGroupWr" (ByVal nummsgs As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal msgflds As MSGFIELDSEBR(), ByVal msgaddrptr As UInt32(), ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgSkipRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgSkipRd" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_MsgSkipWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgSkipWr" (ByVal skip As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgSyncDefine Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgSyncDefine" (ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MsgTriggerDefine Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgTriggerDefine" (ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_PlayConfig" (ByVal configval As UInt32, ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayStatus Lib "BTIEBR64.DLL" Alias "BTIEBR_PlayStatus" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayWr Lib "BTIEBR64.DLL" Alias "BTIEBR_PlayWr" (ByVal buf As UInt16(), ByVal bufcount As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_RTConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_RTConfig" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTCreateList Lib "BTIEBR64.DLL" Alias "BTIEBR_RTCreateList" (ByVal listconfigval As UInt32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTCreateMsg Lib "BTIEBR64.DLL" Alias "BTIEBR_RTCreateMsg" (ByVal configval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTGetMsg Lib "BTIEBR64.DLL" Alias "BTIEBR_RTGetMsg" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTReset Lib "BTIEBR64.DLL" Alias "BTIEBR_RTReset" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTResponseTimeSet Lib "BTIEBR64.DLL" Alias "BTIEBR_RTResponseTimeSet" (ByVal resptime As Int32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTSetMode Lib "BTIEBR64.DLL" Alias "BTIEBR_RTSetMode" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTSWDRd Lib "BTIEBR64.DLL" Alias "BTIEBR_RTSWDRd" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_RTSWDWr Lib "BTIEBR64.DLL" Alias "BTIEBR_RTSWDWr" (ByVal swdval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTSyncDefine Lib "BTIEBR64.DLL" Alias "BTIEBR_RTSyncDefine" (ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ValPackCWD Lib "BTIEBR64.DLL" Alias "BTIEBR_ValPackCWD" (ByVal taval As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal wcval As Int32) As UInt16
        Public Declare Sub BTIEBR_ValUnpackCWD Lib "BTIEBR64.DLL" Alias "BTIEBR_ValUnpackCWD" (ByVal cwdval As UInt16, ByRef taval As Int32, ByRef trflag As Int32, ByRef saval As Int32, ByRef wcval As Int32)
        Public Declare Function BTIEBR_BCCreateListEx Lib "BTIEBR64.DLL" Alias "BTIEBR_BCCreateListEx" (ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCCreateListExx Lib "BTIEBR64.DLL" Alias "BTIEBR_BCCreateListExx" (ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCGetMsg Lib "BTIEBR64.DLL" Alias "BTIEBR_BCGetMsg" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_BCIMGapRd Lib "BTIEBR64.DLL" Alias "BTIEBR_BCIMGapRd" (ByRef gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCIMGapWr Lib "BTIEBR64.DLL" Alias "BTIEBR_BCIMGapWr" (ByVal gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedBranchEx Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedBranchEx" (ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedCallEx Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedCallEx" (ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedLogEx Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedLogEx" (ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal tagval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedNop Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedNop" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedRetryEx Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedRetryEx" (ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal retryflag As UInt16, ByVal retries As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSchedUser Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSchedUser" (ByVal useraddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCSetEntry Lib "BTIEBR64.DLL" Alias "BTIEBR_BCSetEntry" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsgBlkEx Lib "BTIEBR64.DLL" Alias "BTIEBR_BCTransmitMsgBlkEx" (ByVal writeflag As Int32, ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=1)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsgBlkInit Lib "BTIEBR64.DLL" Alias "BTIEBR_BCTransmitMsgBlkInit" (ByVal maxcount As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsgBlkRd Lib "BTIEBR64.DLL" Alias "BTIEBR_BCTransmitMsgBlkRd" (ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BCTransmitMsgBlkWr Lib "BTIEBR64.DLL" Alias "BTIEBR_BCTransmitMsgBlkWr" (ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkAddr Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkAddr" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_BlkOptRd Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkOptRd" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_BlkOptWr Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkOptWr" (ByVal rtopt As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkPtrRd Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkPtrRd" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_BlkPtrWr Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkPtrWr" (ByVal descaddr As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_BlkRd Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkRd" (ByVal block As UInt16(), ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkReset Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkReset" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkSWDRd Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkSWDRd" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_BlkSWDWr Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkSWDWr" (ByVal swdval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkWipe Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkWipe" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_BlkWr Lib "BTIEBR64.DLL" Alias "BTIEBR_BlkWr" (ByVal block As UInt16(), ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_ChConfig" (ByVal enableflag As Int32, ByVal typeval As Int32, ByVal rtlink As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ChIsRunning Lib "BTIEBR64.DLL" Alias "BTIEBR_ChIsRunning" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdAddr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdAddr" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_CmdAlloc Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdAlloc" (ByVal count As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_CmdAllocEx Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdAllocEx" (ByVal configval As UInt32, ByVal count As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_CmdBaseRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdBaseRd" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_CmdBaseWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdBaseWr" (ByVal addrval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdClear Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdClear" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdCountRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdCountRd" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdCountWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdCountWr" (ByVal countval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdCtrlRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdCtrlRd" (ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdCtrlWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdCtrlWr" (ByVal ctrlval As UInt16, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdCurrRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdCurrRd" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdCurrWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdCurrWr" (ByVal addrval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_CmdInit Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdInit" (ByVal opcode As UInt16, ByVal block As UInt16())
        Public Declare Function BTIEBR_CmdInsert Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdInsert" (ByVal block As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_CmdRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdRd" (ByVal block As UInt16(), ByVal addrval As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_CmdRestart Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdRestart" (ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_CmdStackClr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdStackClr" (ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdStartRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdStartRd" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_CmdStartWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdStartWr" (ByVal addrval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdTotalRd Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdTotalRd" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_CmdTotalWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdTotalWr" (ByVal countval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_CmdWr Lib "BTIEBR64.DLL" Alias "BTIEBR_CmdWr" (ByVal block As UInt16(), ByVal addrval As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_DescAddr Lib "BTIEBR64.DLL" Alias "BTIEBR_DescAddr" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescAlloc Lib "BTIEBR64.DLL" Alias "BTIEBR_DescAlloc" (ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_DescConfig" (ByVal configval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_DescConfigAll Lib "BTIEBR64.DLL" Alias "BTIEBR_DescConfigAll" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_DescOptRd Lib "BTIEBR64.DLL" Alias "BTIEBR_DescOptRd" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_DescOptWr Lib "BTIEBR64.DLL" Alias "BTIEBR_DescOptWr" (ByVal descopt As UInt16, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_DescPtrRd Lib "BTIEBR64.DLL" Alias "BTIEBR_DescPtrRd" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescPtrWr Lib "BTIEBR64.DLL" Alias "BTIEBR_DescPtrWr" (ByVal msgaddr As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescRd Lib "BTIEBR64.DLL" Alias "BTIEBR_DescRd" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal desc As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DescWr Lib "BTIEBR64.DLL" Alias "BTIEBR_DescWr" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal desc As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_DllUsageCount Lib "BTIEBR64.DLL" Alias "BTIEBR_DllUsageCount" () As Int32
        Public Declare Function BTIEBR_ErrorIsTaggedBC Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorIsTaggedBC" (ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorIsTaggedRT Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorIsTaggedRT" (ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ErrorResponse Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorResponse" (ByVal responseval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_ErrorSync Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorSync" (ByVal value As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_ErrorTimeout Lib "BTIEBR64.DLL" Alias "BTIEBR_ErrorTimeout" (ByVal timeoutval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_HostComm Lib "BTIEBR64.DLL" Alias "BTIEBR_HostComm" (ByVal lpparam As IntPtr, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_IsChan Lib "BTIEBR64.DLL" Alias "BTIEBR_IsChan" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_LHubConfig" (ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortExists Lib "BTIEBR64.DLL" Alias "BTIEBR_LHubPortExists" (ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortGetCount Lib "BTIEBR64.DLL" Alias "BTIEBR_LHubPortGetCount" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortGetRTLink Lib "BTIEBR64.DLL" Alias "BTIEBR_LHubPortGetRTLink" (ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortIsAvail Lib "BTIEBR64.DLL" Alias "BTIEBR_LHubPortIsAvail" (ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubPortIsAvailMask Lib "BTIEBR64.DLL" Alias "BTIEBR_LHubPortIsAvailMask" (ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_LHubPortSetRTLink Lib "BTIEBR64.DLL" Alias "BTIEBR_LHubPortSetRTLink" (ByVal rtlink As Int32, ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_LHubRTLinkGetPort Lib "BTIEBR64.DLL" Alias "BTIEBR_LHubRTLinkGetPort" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListAddr Lib "BTIEBR64.DLL" Alias "BTIEBR_ListAddr" (ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListAddrEx Lib "BTIEBR64.DLL" Alias "BTIEBR_ListAddrEx" (ByVal index As Int32, ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListBlockRdEx Lib "BTIEBR64.DLL" Alias "BTIEBR_ListBlockRdEx" (ByVal buf As UInt16(), ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListBlockWrEx Lib "BTIEBR64.DLL" Alias "BTIEBR_ListBlockWrEx" (ByVal buf As UInt16(), ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListClear Lib "BTIEBR64.DLL" Alias "BTIEBR_ListClear" (ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_ListConfig" (ByVal configval As UInt32, ByVal count As Int32, ByVal skipval As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListConfigEx Lib "BTIEBR64.DLL" Alias "BTIEBR_ListConfigEx" (ByVal configval As UInt32, ByVal count As Int32, ByVal skipval As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListDataRdEx Lib "BTIEBR64.DLL" Alias "BTIEBR_ListDataRdEx" (ByVal buf As UInt16(), ByVal count As Int32, ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListDataWrEx Lib "BTIEBR64.DLL" Alias "BTIEBR_ListDataWrEx" (ByVal buf As UInt16(), ByVal count As Int32, ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListInit Lib "BTIEBR64.DLL" Alias "BTIEBR_ListInit" (ByVal listaddr As UInt32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ListNextRd Lib "BTIEBR64.DLL" Alias "BTIEBR_ListNextRd" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_ListNextWr Lib "BTIEBR64.DLL" Alias "BTIEBR_ListNextWr" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Sub BTIEBR_ListPostRd Lib "BTIEBR64.DLL" Alias "BTIEBR_ListPostRd" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_ListPostWr Lib "BTIEBR64.DLL" Alias "BTIEBR_ListPostWr" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_ListPreRd Lib "BTIEBR64.DLL" Alias "BTIEBR_ListPreRd" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        Public Declare Sub BTIEBR_ListPreWr Lib "BTIEBR64.DLL" Alias "BTIEBR_ListPreWr" (ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_ListStatus Lib "BTIEBR64.DLL" Alias "BTIEBR_ListStatus" (ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ModeIsBC Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsBC" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsMon Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsMon" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotBC Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsNotBC" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotMon Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsNotMon" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotPlay Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsNotPlay" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotRT Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsNotRT" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsNotSerial Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsNotSerial" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsPlay Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsPlay" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsRT Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsRT" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_ModeIsSerial Lib "BTIEBR64.DLL" Alias "BTIEBR_ModeIsSerial" (ByVal chanflag As UInt16) As Int32
        Public Declare Function BTIEBR_MonConfigEx Lib "BTIEBR64.DLL" Alias "BTIEBR_MonConfigEx" (ByVal configval As UInt32, ByVal moncount As UInt32, ByVal cardnum As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MonFilterSAEx Lib "BTIEBR64.DLL" Alias "BTIEBR_MonFilterSAEx" (ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MsgCheck Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgCheck" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_MsgCommRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgCommRd" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgCommWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgCommWr" (ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgConfig" (ByVal configval As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgConfigEx Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgConfigEx" (ByVal configval As UInt32, ByVal cwd As UInt16, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgCWDLinkRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgCWDLinkRd" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgCWDLinkWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgCWDLinkWr" (ByVal cwdlink As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgCWDRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgCWDRd" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgCWDWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgCWDWr" (ByVal cwd As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgErrorRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgErrorRd" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgErrorWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgErrorWr" (ByVal value As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgFlagRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgFlagRd" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgFlagWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgFlagWr" (ByVal msgflag As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgIsAccessed Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgIsAccessed" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_MsgOptRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgOptRd" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Sub BTIEBR_MsgOptWr Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgOptWr" (ByVal msgopt As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        Public Declare Function BTIEBR_MsgReset Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgReset" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgResetEx Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgResetEx" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_MsgSWDLinkRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgSWDLinkRd" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_MsgSWDRd Lib "BTIEBR64.DLL" Alias "BTIEBR_MsgSWDRd" (ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_PlayClear Lib "BTIEBR64.DLL" Alias "BTIEBR_PlayClear" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayConfigEx Lib "BTIEBR64.DLL" Alias "BTIEBR_PlayConfigEx" (ByVal configval As UInt32, ByVal playcount As UInt32, ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayCounterWr Lib "BTIEBR64.DLL" Alias "BTIEBR_PlayCounterWr" (ByVal totalcount As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_PlayStatusEx Lib "BTIEBR64.DLL" Alias "BTIEBR_PlayStatusEx" (ByRef totalcount As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_PlayWrEx Lib "BTIEBR64.DLL" Alias "BTIEBR_PlayWrEx" (ByVal buf As UInt16(), ByVal bufcount As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_RTConfigEx Lib "BTIEBR64.DLL" Alias "BTIEBR_RTConfigEx" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTConfigExx Lib "BTIEBR64.DLL" Alias "BTIEBR_RTConfigExx" (ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTCreateListEx Lib "BTIEBR64.DLL" Alias "BTIEBR_RTCreateListEx" (ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTCreateListExx Lib "BTIEBR64.DLL" Alias "BTIEBR_RTCreateListExx" (ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_RTResponseTimeRd Lib "BTIEBR64.DLL" Alias "BTIEBR_RTResponseTimeRd" (ByRef timeval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTResponseTimeWr Lib "BTIEBR64.DLL" Alias "BTIEBR_RTResponseTimeWr" (ByVal timeval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_RTStatus Lib "BTIEBR64.DLL" Alias "BTIEBR_RTStatus" (ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        Public Declare Function BTIEBR_SABlkWr Lib "BTIEBR64.DLL" Alias "BTIEBR_SABlkWr" (ByVal rtlink As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SABlkWrEx Lib "BTIEBR64.DLL" Alias "BTIEBR_SABlkWrEx" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedBuild Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedBuild" (ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal freq As Int32(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedBuildEx Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedBuildEx" (ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal freq As Int32(), ByVal period As Int32(), ByVal buf As IntPtr(), ByVal bufsize As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedBuildExx Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedBuildExx" (ByVal frametime As UInt32, ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal period As Int32(), ByVal buf As IntPtr(), ByVal bufsize As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedCalc Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedCalc" (ByRef msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Sub BTIEBR_SchedDisplay Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedDisplay" (ByVal nummsgs As UInt32, ByRef count As UInt32)
        Public Declare Sub BTIEBR_SchedHook Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedHook" (ByVal index As Int32, ByVal ptr As IntPtr)
        Public Declare Function BTIEBR_SchedInsEnd Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedInsEnd" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedInsMsg Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedInsMsg" (ByVal index As UInt32, ByRef msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedInsStart Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedInsStart" (ByVal frametime As UInt32, ByVal count As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SchedMaxval Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedMaxval" (ByVal arrayval As UInt32(), ByVal arrayvalcount As UInt32) As UInt32
        Public Declare Function BTIEBR_SchedMinval Lib "BTIEBR64.DLL" Alias "BTIEBR_SchedMinval" (ByVal arrayval As UInt32(), ByVal arrayvalcount As UInt32) As UInt32
        Public Declare Sub BTIEBR_SchedPrint Lib "BTIEBR64.DLL" Alias "" (ByVal ParamArray list() As String)
        Public Declare Function BTIEBR_SerialClear Lib "BTIEBR64.DLL" Alias "BTIEBR_SerialClear" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SerialConfig Lib "BTIEBR64.DLL" Alias "BTIEBR_SerialConfig" (ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SerialConfigEx Lib "BTIEBR64.DLL" Alias "BTIEBR_SerialConfigEx" (ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SerialStatus Lib "BTIEBR64.DLL" Alias "BTIEBR_SerialStatus" (ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_SerialWr Lib "BTIEBR64.DLL" Alias "BTIEBR_SerialWr" (ByVal buf As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        Public Declare Function BTIEBR_TestProtocol Lib "BTIEBR64.DLL" Alias "BTIEBR_TestProtocol" (ByVal handleval As IntPtr) As Int32
        Public Declare Function BTIEBR_ValPackRTCWD Lib "BTIEBR64.DLL" Alias "BTIEBR_ValPackRTCWD" (ByVal mcflag As Int32, ByVal taval As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal count As Int32) As UInt16
        Public Declare Sub BTIEBR_ValUnpackRTCWD Lib "BTIEBR64.DLL" Alias "BTIEBR_ValUnpackRTCWD" (ByVal cwd As UInt16, ByRef bcastflag As Int32, ByRef mcflag As Int32, ByRef taval As Int32, ByRef trflag As Int32, ByRef saval As Int32, ByRef count As Int32)
    End Class

    Public Function BTIEBR_BCConfig(ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCConfig(configval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCConfig(configval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCConfigEx(ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCConfigEx(configval, count, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCConfigEx(configval, count, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCConfigMsg(ByVal configval As UInt32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCConfigMsg(configval, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCConfigMsg(configval, msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_BCCreateList(ByVal listconfigval As UInt32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCCreateList(listconfigval, count, msgconfigval, cwd, data, rtlink, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCCreateList(listconfigval, count, msgconfigval, cwd, data, rtlink, handleval)
        End If
    End Function
    Public Function BTIEBR_BCCreateMsg(ByVal configval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCCreateMsg(configval, cwd, data, rtlink, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCCreateMsg(configval, cwd, data, rtlink, handleval)
        End If
    End Function
    Public Sub BTIEBR_BCPause(ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_BCPause(channum, handleval)
        Else
             BTIEBR_64.BTIEBR_BCPause(channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_BCPauseCheck(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCPauseCheck(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCPauseCheck(channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_BCResume(ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_BCResume(channum, handleval)
        Else
             BTIEBR_64.BTIEBR_BCResume(channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_BCSchedAgain(ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedAgain(index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedAgain(index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedBranch(ByVal condition As UInt32, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedBranch(condition, destindex, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedBranch(condition, destindex, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedBranchUpdate(ByVal destindex As Int32, ByVal opcodeindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedBranchUpdate(destindex, opcodeindex, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedBranchUpdate(destindex, opcodeindex, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedBuild(ByVal nummsgs As UInt16, ByVal msgaddr As UInt32(), ByVal freq As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedBuild(nummsgs, msgaddr, freq, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedBuild(nummsgs, msgaddr, freq, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedCall(ByVal condition As UInt32, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedCall(condition, destindex, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedCall(condition, destindex, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedCallUpdate(ByVal destindex As Int32, ByVal opcodeindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedCallUpdate(destindex, opcodeindex, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedCallUpdate(destindex, opcodeindex, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedEntry(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedEntry(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedEntry(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedFrame(ByVal timeval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedFrame(timeval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedFrame(timeval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedFrameEnd(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedFrameEnd(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedFrameEnd(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedFrameStart(ByVal timeval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedFrameStart(timeval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedFrameStart(timeval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedGap(ByVal gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedGap(gapval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedGap(gapval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedHalt(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedHalt(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedHalt(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedLog(ByVal condition As UInt32, ByVal tagval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedLog(condition, tagval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedLog(condition, tagval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedMsg(ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedMsg(msgaddr, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedMsg(msgaddr, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedPause(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedPause(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedPause(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedPulse(ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedPulse(dionum, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedPulse(dionum, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedPulse0(ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedPulse0(dionum, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedPulse0(dionum, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedPulse1(ByVal dionum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedPulse1(dionum, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedPulse1(dionum, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedRestart(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedRestart(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedRestart(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedRetry(ByVal condition As UInt32, ByVal retries As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedRetry(condition, retries, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedRetry(condition, retries, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedReturn(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedReturn(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedReturn(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSetDefaultGap(ByVal gapval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSetDefaultGap(gapval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSetDefaultGap(gapval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSetTimeout(ByVal timeoutval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSetTimeout(timeoutval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSetTimeout(timeoutval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSyncDefine(ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSyncDefine(enableflag, syncmask, pinpolarity, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSyncDefine(enableflag, syncmask, pinpolarity, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCTransmitMsg(ByRef xmitfieldsval As XMITFIELDSEBR, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCTransmitMsg(xmitfieldsval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCTransmitMsg(xmitfieldsval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCTriggerDefine(ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChGetInfo(ByVal infotype As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChGetInfo(infotype, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChGetInfo(infotype, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChIsBM(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChIsBM(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChIsBM(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChIsEBR(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChIsEBR(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChIsEBR(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChIsM(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChIsM(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChIsM(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChIsS(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChIsS(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChIsS(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChStart(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChStart(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChStart(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChStop(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChStop(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChStop(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdMaxLoopRd(ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdMaxLoopRd(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdMaxLoopRd(channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_CmdMaxLoopWr(ByVal countval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdMaxLoopWr(countval, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdMaxLoopWr(countval, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_CmdShotRd(ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdShotRd(index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdShotRd(index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdShotWr(ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdShotWr(value, index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdShotWr(value, index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdSkipRd(ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdSkipRd(index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdSkipRd(index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdSkipWr(ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdSkipWr(value, index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdSkipWr(value, index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdStepRd(ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdStepRd(index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdStepRd(index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdStepWr(ByVal value As Int32, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdStepWr(value, index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdStepWr(value, index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ErrorCtrl(ByVal ctrlval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorCtrl(ctrlval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorCtrl(ctrlval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ErrorDefine(ByVal defineval As UInt32, ByVal errvalue As UInt16, ByVal countval As Int32, ByVal wordpos As UInt16, ByVal bitpos As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorDefine(defineval, errvalue, countval, wordpos, bitpos, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorDefine(defineval, errvalue, countval, wordpos, bitpos, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ErrorSent(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorSent(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorSent(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ErrorTagBC(ByVal tagval As Int32, ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorTagBC(tagval, msgaddr, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorTagBC(tagval, msgaddr, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ErrorTagRT(ByVal tagval As Int32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorTagRT(tagval, mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorTagRT(tagval, mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ErrorTriggerDefine(ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ListBlockRd(ByVal buf As UInt16(), ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListBlockRd(buf, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListBlockRd(buf, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListBlockWr(ByVal buf As UInt16(), ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListBlockWr(buf, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListBlockWr(buf, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListDataRd(ByVal buf As UInt16(), ByVal count As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListDataRd(buf, count, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListDataRd(buf, count, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListDataWr(ByVal buf As UInt16(), ByVal count As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListDataWr(buf, count, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListDataWr(buf, count, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListMultiBlockRd(ByVal buf As UInt16(), ByRef blkcountptr As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListMultiBlockRd(buf, blkcountptr, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListMultiBlockRd(buf, blkcountptr, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListMultiBlockWr(ByVal buf As UInt16(), ByVal blkcount As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListMultiBlockWr(buf, blkcount, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListMultiBlockWr(buf, blkcount, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MonConfig(ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MonConfig(configval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MonConfig(configval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_MonFilterRTLink(ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MonFilterRTLink(rtlinkmask, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MonFilterRTLink(rtlinkmask, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_MonFilterSA(ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MonFilterSA(rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MonFilterSA(rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgBlockRd(ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgBlockRd(buf, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgBlockRd(buf, msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgBlockWr(ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgBlockWr(buf, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgBlockWr(buf, msgaddr, handleval)
        End If
    End Function
    Public Sub BTIEBR_MsgDataRd(ByVal buf As UInt16(), ByVal count As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgDataRd(buf, count, msgaddr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgDataRd(buf, count, msgaddr, handleval)
        End If
    End Sub
    Public Sub BTIEBR_MsgDataWr(ByVal buf As UInt16(), ByVal count As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgDataWr(buf, count, msgaddr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgDataWr(buf, count, msgaddr, handleval)
        End If
    End Sub
    Public Function BTIEBR_MsgFieldRd(ByVal fieldtype As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgFieldRd(fieldtype, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgFieldRd(fieldtype, msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgFieldWr(ByVal fieldval As UInt32, ByVal fieldtype As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgFieldWr(fieldval, fieldtype, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgFieldWr(fieldval, fieldtype, msgaddr, handleval)
        End If
    End Function
    Public Sub BTIEBR_MsgGroupRd(ByVal nummsgs As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal msgflds As MSGFIELDSEBR(), ByVal msgaddrptr As UInt32(), ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgGroupRd(nummsgs, msgflds, msgaddrptr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgGroupRd(nummsgs, msgflds, msgaddrptr, handleval)
        End If
    End Sub
    Public Sub BTIEBR_MsgGroupWr(ByVal nummsgs As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal msgflds As MSGFIELDSEBR(), ByVal msgaddrptr As UInt32(), ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgGroupWr(nummsgs, msgflds, msgaddrptr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgGroupWr(nummsgs, msgflds, msgaddrptr, handleval)
        End If
    End Sub
    Public Function BTIEBR_MsgSkipRd(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgSkipRd(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgSkipRd(msgaddr, handleval)
        End If
    End Function
    Public Sub BTIEBR_MsgSkipWr(ByVal skip As Int32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgSkipWr(skip, msgaddr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgSkipWr(skip, msgaddr, handleval)
        End If
    End Sub
    Public Function BTIEBR_MsgSyncDefine(ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgSyncDefine(enableflag, syncmask, pinpolarity, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgSyncDefine(enableflag, syncmask, pinpolarity, msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgTriggerDefine(ByVal enableflag As Int32, ByVal trigmask As UInt16, ByVal trigval As UInt16, ByVal pinpolarity As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgTriggerDefine(enableflag, trigmask, trigval, pinpolarity, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgTriggerDefine(enableflag, trigmask, trigval, pinpolarity, msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_PlayConfig(ByVal configval As UInt32, ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_PlayConfig(configval, rtlinkmask, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_PlayConfig(configval, rtlinkmask, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_PlayStatus(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_PlayStatus(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_PlayStatus(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_PlayWr(ByVal buf As UInt16(), ByVal bufcount As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_PlayWr(buf, bufcount, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_PlayWr(buf, bufcount, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTConfig(ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTConfig(configval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTConfig(configval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTCreateList(ByVal listconfigval As UInt32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTCreateList(listconfigval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTCreateList(listconfigval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTCreateMsg(ByVal configval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTCreateMsg(configval, mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTCreateMsg(configval, mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTGetMsg(ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTGetMsg(mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTGetMsg(mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTReset(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTReset(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTReset(rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTResponseTimeSet(ByVal resptime As Int32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTResponseTimeSet(resptime, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTResponseTimeSet(resptime, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTSetMode(ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTSetMode(configval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTSetMode(configval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTSWDRd(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTSWDRd(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTSWDRd(rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTSWDWr(ByVal swdval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTSWDWr(swdval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTSWDWr(swdval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTSyncDefine(ByVal enableflag As Int32, ByVal syncmask As UInt16, ByVal pinpolarity As UInt16, ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTSyncDefine(enableflag, syncmask, pinpolarity, rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTSyncDefine(enableflag, syncmask, pinpolarity, rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ValPackCWD(ByVal taval As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal wcval As Int32) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ValPackCWD(taval, trflag, saval, wcval)
        Else
            Return BTIEBR_64.BTIEBR_ValPackCWD(taval, trflag, saval, wcval)
        End If
    End Function
    Public Sub BTIEBR_ValUnpackCWD(ByVal cwdval As UInt16, ByRef taval As Int32, ByRef trflag As Int32, ByRef saval As Int32, ByRef wcval As Int32)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_ValUnpackCWD(cwdval, taval, trflag, saval, wcval)
        Else
             BTIEBR_64.BTIEBR_ValUnpackCWD(cwdval, taval, trflag, saval, wcval)
        End If
    End Sub
    Public Function BTIEBR_BCCreateListEx(ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCCreateListEx(listconfigval, skipval, count, msgconfigval, cwd, data, rtlink, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCCreateListEx(listconfigval, skipval, count, msgconfigval, cwd, data, rtlink, handleval)
        End If
    End Function
    Public Function BTIEBR_BCCreateListExx(ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal cwd As UInt16, ByVal data As UInt16(), ByVal rtlink As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCCreateListExx(listconfigval, skipval, count, msgconfigval, cwd, data, rtlink, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCCreateListExx(listconfigval, skipval, count, msgconfigval, cwd, data, rtlink, handleval)
        End If
    End Function
    Public Function BTIEBR_BCGetMsg(ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCGetMsg(index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCGetMsg(index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCIMGapRd(ByRef gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCIMGapRd(gapval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCIMGapRd(gapval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCIMGapWr(ByVal gapval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCIMGapWr(gapval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCIMGapWr(gapval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedBranchEx(ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedBranchEx(condition1, condition2, condition3, opcode, destindex, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedBranchEx(condition1, condition2, condition3, opcode, destindex, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedCallEx(ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal destindex As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedCallEx(condition1, condition2, condition3, opcode, destindex, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedCallEx(condition1, condition2, condition3, opcode, destindex, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedLogEx(ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal opcode As UInt16, ByVal tagval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedLogEx(condition1, condition2, condition3, opcode, tagval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedLogEx(condition1, condition2, condition3, opcode, tagval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedNop(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedNop(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedNop(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedRetryEx(ByVal condition1 As UInt16, ByVal condition2 As UInt16, ByVal condition3 As UInt16, ByVal retryflag As UInt16, ByVal retries As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedRetryEx(condition1, condition2, condition3, retryflag, retries, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedRetryEx(condition1, condition2, condition3, retryflag, retries, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSchedUser(ByVal useraddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSchedUser(useraddr, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSchedUser(useraddr, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCSetEntry(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCSetEntry(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCSetEntry(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCTransmitMsgBlkEx(ByVal writeflag As Int32, ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=1)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCTransmitMsgBlkEx(writeflag, count, xmitfieldsval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCTransmitMsgBlkEx(writeflag, count, xmitfieldsval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCTransmitMsgBlkInit(ByVal maxcount As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCTransmitMsgBlkInit(maxcount, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCTransmitMsgBlkInit(maxcount, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCTransmitMsgBlkRd(ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCTransmitMsgBlkRd(count, xmitfieldsval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCTransmitMsgBlkRd(count, xmitfieldsval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BCTransmitMsgBlkWr(ByVal count As Int32, <[In](), Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=0)> ByVal xmitfieldsval As XMITFIELDSEBR(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BCTransmitMsgBlkWr(count, xmitfieldsval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BCTransmitMsgBlkWr(count, xmitfieldsval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkAddr(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkAddr(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkAddr(rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkOptRd(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkOptRd(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkOptRd(rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkOptWr(ByVal rtopt As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkOptWr(rtopt, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkOptWr(rtopt, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkPtrRd(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkPtrRd(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkPtrRd(rtlink, channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_BlkPtrWr(ByVal descaddr As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_BlkPtrWr(descaddr, rtlink, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_BlkPtrWr(descaddr, rtlink, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_BlkRd(ByVal block As UInt16(), ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkRd(block, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkRd(block, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkReset(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkReset(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkReset(rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkSWDRd(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkSWDRd(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkSWDRd(rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkSWDWr(ByVal swdval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkSWDWr(swdval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkSWDWr(swdval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkWipe(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkWipe(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkWipe(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_BlkWr(ByVal block As UInt16(), ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_BlkWr(block, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_BlkWr(block, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChConfig(ByVal enableflag As Int32, ByVal typeval As Int32, ByVal rtlink As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChConfig(enableflag, typeval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChConfig(enableflag, typeval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ChIsRunning(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ChIsRunning(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ChIsRunning(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdAddr(ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdAddr(index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdAddr(index, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdAlloc(ByVal count As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdAlloc(count, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdAlloc(count, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdAllocEx(ByVal configval As UInt32, ByVal count As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdAllocEx(configval, count, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdAllocEx(configval, count, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdBaseRd(ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdBaseRd(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdBaseRd(channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_CmdBaseWr(ByVal addrval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdBaseWr(addrval, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdBaseWr(addrval, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_CmdClear(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdClear(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdClear(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdCountRd(ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdCountRd(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdCountRd(channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_CmdCountWr(ByVal countval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdCountWr(countval, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdCountWr(countval, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_CmdCtrlRd(ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdCtrlRd(index, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdCtrlRd(index, channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_CmdCtrlWr(ByVal ctrlval As UInt16, ByVal index As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdCtrlWr(ctrlval, index, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdCtrlWr(ctrlval, index, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_CmdCurrRd(ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdCurrRd(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdCurrRd(channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_CmdCurrWr(ByVal addrval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdCurrWr(addrval, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdCurrWr(addrval, channum, handleval)
        End If
    End Sub
    Public Sub BTIEBR_CmdInit(ByVal opcode As UInt16, ByVal block As UInt16())
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdInit(opcode, block)
        Else
             BTIEBR_64.BTIEBR_CmdInit(opcode, block)
        End If
    End Sub
    Public Function BTIEBR_CmdInsert(ByVal block As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdInsert(block, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdInsert(block, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_CmdRd(ByVal block As UInt16(), ByVal addrval As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdRd(block, addrval, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdRd(block, addrval, handleval)
        End If
    End Function
    Public Sub BTIEBR_CmdRestart(ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdRestart(channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdRestart(channum, handleval)
        End If
    End Sub
    Public Sub BTIEBR_CmdStackClr(ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdStackClr(channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdStackClr(channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_CmdStartRd(ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdStartRd(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdStartRd(channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_CmdStartWr(ByVal addrval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdStartWr(addrval, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdStartWr(addrval, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_CmdTotalRd(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdTotalRd(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdTotalRd(channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_CmdTotalWr(ByVal countval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_CmdTotalWr(countval, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_CmdTotalWr(countval, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_CmdWr(ByVal block As UInt16(), ByVal addrval As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_CmdWr(block, addrval, handleval)
        Else
            Return BTIEBR_64.BTIEBR_CmdWr(block, addrval, handleval)
        End If
    End Function
    Public Function BTIEBR_DescAddr(ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescAddr(mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescAddr(mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_DescAlloc(ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescAlloc(handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescAlloc(handleval)
        End If
    End Function
    Public Function BTIEBR_DescConfig(ByVal configval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescConfig(configval, mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescConfig(configval, mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_DescConfigAll(ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescConfigAll(configval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescConfigAll(configval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_DescOptRd(ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescOptRd(mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescOptRd(mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_DescOptWr(ByVal descopt As UInt16, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_DescOptWr(descopt, mcflag, rtlink, trflag, saval, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_DescOptWr(descopt, mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_DescPtrRd(ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescPtrRd(mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescPtrRd(mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_DescPtrWr(ByVal msgaddr As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescPtrWr(msgaddr, mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescPtrWr(msgaddr, mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_DescRd(ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal desc As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescRd(mcflag, rtlink, trflag, saval, desc, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescRd(mcflag, rtlink, trflag, saval, desc, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_DescWr(ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal desc As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DescWr(mcflag, rtlink, trflag, saval, desc, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_DescWr(mcflag, rtlink, trflag, saval, desc, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_DllUsageCount() As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_DllUsageCount
        Else
            Return BTIEBR_64.BTIEBR_DllUsageCount
        End If
    End Function
    Public Function BTIEBR_ErrorIsTaggedBC(ByVal msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorIsTaggedBC(msgaddr, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorIsTaggedBC(msgaddr, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ErrorIsTaggedRT(ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorIsTaggedRT(mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorIsTaggedRT(mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ErrorResponse(ByVal responseval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorResponse(responseval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorResponse(responseval, channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_ErrorSync(ByVal value As Int32, ByVal channum As Int32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_ErrorSync(value, channum, handleval)
        Else
             BTIEBR_64.BTIEBR_ErrorSync(value, channum, handleval)
        End If
    End Sub
    Public Function BTIEBR_ErrorTimeout(ByVal timeoutval As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ErrorTimeout(timeoutval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ErrorTimeout(timeoutval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_HostComm(ByVal lpparam As IntPtr, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_HostComm(lpparam, handleval)
        Else
            Return BTIEBR_64.BTIEBR_HostComm(lpparam, handleval)
        End If
    End Function
    Public Function BTIEBR_IsChan(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_IsChan(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_IsChan(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_LHubConfig(ByVal configval As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_LHubConfig(configval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_LHubConfig(configval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_LHubPortExists(ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_LHubPortExists(portnum, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_LHubPortExists(portnum, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_LHubPortGetCount(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_LHubPortGetCount(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_LHubPortGetCount(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_LHubPortGetRTLink(ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_LHubPortGetRTLink(portnum, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_LHubPortGetRTLink(portnum, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_LHubPortIsAvail(ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_LHubPortIsAvail(portnum, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_LHubPortIsAvail(portnum, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_LHubPortIsAvailMask(ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_LHubPortIsAvailMask(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_LHubPortIsAvailMask(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_LHubPortSetRTLink(ByVal rtlink As Int32, ByVal portnum As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_LHubPortSetRTLink(rtlink, portnum, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_LHubPortSetRTLink(rtlink, portnum, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_LHubRTLinkGetPort(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_LHubRTLinkGetPort(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_LHubRTLinkGetPort(rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_ListAddr(ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListAddr(index, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListAddr(index, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListAddrEx(ByVal index As Int32, ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListAddrEx(index, listaddr, header, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListAddrEx(index, listaddr, header, handleval)
        End If
    End Function
    Public Function BTIEBR_ListBlockRdEx(ByVal buf As UInt16(), ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListBlockRdEx(buf, index, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListBlockRdEx(buf, index, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListBlockWrEx(ByVal buf As UInt16(), ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListBlockWrEx(buf, index, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListBlockWrEx(buf, index, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListClear(ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListClear(listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListClear(listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListConfig(ByVal configval As UInt32, ByVal count As Int32, ByVal skipval As UInt16, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListConfig(configval, count, skipval, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListConfig(configval, count, skipval, handleval)
        End If
    End Function
    Public Function BTIEBR_ListConfigEx(ByVal configval As UInt32, ByVal count As Int32, ByVal skipval As UInt16, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListConfigEx(configval, count, skipval, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListConfigEx(configval, count, skipval, handleval)
        End If
    End Function
    Public Function BTIEBR_ListDataRdEx(ByVal buf As UInt16(), ByVal count As Int32, ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListDataRdEx(buf, count, index, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListDataRdEx(buf, count, index, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListDataWrEx(ByVal buf As UInt16(), ByVal count As Int32, ByVal index As Int32, ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListDataWrEx(buf, count, index, listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListDataWrEx(buf, count, index, listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListInit(ByVal listaddr As UInt32, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListInit(listaddr, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListInit(listaddr, msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ListNextRd(ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListNextRd(listaddr, header, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListNextRd(listaddr, header, handleval)
        End If
    End Function
    Public Function BTIEBR_ListNextWr(ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListNextWr(listaddr, header, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListNextWr(listaddr, header, handleval)
        End If
    End Function
    Public Sub BTIEBR_ListPostRd(ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_ListPostRd(listaddr, header, handleval)
        Else
             BTIEBR_64.BTIEBR_ListPostRd(listaddr, header, handleval)
        End If
    End Sub
    Public Sub BTIEBR_ListPostWr(ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_ListPostWr(listaddr, header, handleval)
        Else
             BTIEBR_64.BTIEBR_ListPostWr(listaddr, header, handleval)
        End If
    End Sub
    Public Sub BTIEBR_ListPreRd(ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_ListPreRd(listaddr, header, handleval)
        Else
             BTIEBR_64.BTIEBR_ListPreRd(listaddr, header, handleval)
        End If
    End Sub
    Public Sub BTIEBR_ListPreWr(ByVal listaddr As UInt32, ByRef header As UInt16, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_ListPreWr(listaddr, header, handleval)
        Else
             BTIEBR_64.BTIEBR_ListPreWr(listaddr, header, handleval)
        End If
    End Sub
    Public Function BTIEBR_ListStatus(ByVal listaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ListStatus(listaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_ListStatus(listaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_ModeIsBC(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsBC(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsBC(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsMon(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsMon(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsMon(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsNotBC(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsNotBC(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsNotBC(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsNotMon(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsNotMon(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsNotMon(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsNotPlay(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsNotPlay(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsNotPlay(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsNotRT(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsNotRT(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsNotRT(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsNotSerial(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsNotSerial(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsNotSerial(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsPlay(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsPlay(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsPlay(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsRT(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsRT(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsRT(chanflag)
        End If
    End Function
    Public Function BTIEBR_ModeIsSerial(ByVal chanflag As UInt16) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ModeIsSerial(chanflag)
        Else
            Return BTIEBR_64.BTIEBR_ModeIsSerial(chanflag)
        End If
    End Function
    Public Function BTIEBR_MonConfigEx(ByVal configval As UInt32, ByVal moncount As UInt32, ByVal cardnum As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MonConfigEx(configval, moncount, cardnum, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MonConfigEx(configval, moncount, cardnum, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_MonFilterSAEx(ByVal rtlink As Int32, ByVal rcvsamask As UInt32, ByVal xmtsamask As UInt32, ByVal rcvmcmask As UInt32, ByVal xmtmcmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MonFilterSAEx(rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MonFilterSAEx(rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgCheck(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgCheck(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgCheck(msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgCommRd(ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgCommRd(buf, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgCommRd(buf, msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgCommWr(ByRef buf As MSGFIELDSEBR, ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgCommWr(buf, msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgCommWr(buf, msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgConfig(ByVal configval As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgConfig(configval, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgConfig(configval, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgConfigEx(ByVal configval As UInt32, ByVal cwd As UInt16, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgConfigEx(configval, cwd, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgConfigEx(configval, cwd, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgCWDLinkRd(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgCWDLinkRd(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgCWDLinkRd(msgaddr, handleval)
        End If
    End Function
    Public Sub BTIEBR_MsgCWDLinkWr(ByVal cwdlink As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgCWDLinkWr(cwdlink, msgaddr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgCWDLinkWr(cwdlink, msgaddr, handleval)
        End If
    End Sub
    Public Function BTIEBR_MsgCWDRd(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgCWDRd(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgCWDRd(msgaddr, handleval)
        End If
    End Function
    Public Sub BTIEBR_MsgCWDWr(ByVal cwd As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgCWDWr(cwd, msgaddr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgCWDWr(cwd, msgaddr, handleval)
        End If
    End Sub
    Public Function BTIEBR_MsgErrorRd(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgErrorRd(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgErrorRd(msgaddr, handleval)
        End If
    End Function
    Public Sub BTIEBR_MsgErrorWr(ByVal value As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgErrorWr(value, msgaddr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgErrorWr(value, msgaddr, handleval)
        End If
    End Sub
    Public Function BTIEBR_MsgFlagRd(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgFlagRd(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgFlagRd(msgaddr, handleval)
        End If
    End Function
    Public Sub BTIEBR_MsgFlagWr(ByVal msgflag As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgFlagWr(msgflag, msgaddr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgFlagWr(msgflag, msgaddr, handleval)
        End If
    End Sub
    Public Function BTIEBR_MsgIsAccessed(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgIsAccessed(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgIsAccessed(msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgOptRd(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgOptRd(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgOptRd(msgaddr, handleval)
        End If
    End Function
    Public Sub BTIEBR_MsgOptWr(ByVal msgopt As UInt16, ByVal msgaddr As UInt32, ByVal handleval As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_MsgOptWr(msgopt, msgaddr, handleval)
        Else
             BTIEBR_64.BTIEBR_MsgOptWr(msgopt, msgaddr, handleval)
        End If
    End Sub
    Public Function BTIEBR_MsgReset(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgReset(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgReset(msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgResetEx(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgResetEx(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgResetEx(msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgSWDLinkRd(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgSWDLinkRd(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgSWDLinkRd(msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_MsgSWDRd(ByVal msgaddr As UInt32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_MsgSWDRd(msgaddr, handleval)
        Else
            Return BTIEBR_64.BTIEBR_MsgSWDRd(msgaddr, handleval)
        End If
    End Function
    Public Function BTIEBR_PlayClear(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_PlayClear(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_PlayClear(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_PlayConfigEx(ByVal configval As UInt32, ByVal playcount As UInt32, ByVal rtlinkmask As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_PlayConfigEx(configval, playcount, rtlinkmask, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_PlayConfigEx(configval, playcount, rtlinkmask, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_PlayCounterWr(ByVal totalcount As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_PlayCounterWr(totalcount, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_PlayCounterWr(totalcount, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_PlayStatusEx(ByRef totalcount As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_PlayStatusEx(totalcount, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_PlayStatusEx(totalcount, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_PlayWrEx(ByVal buf As UInt16(), ByVal bufcount As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_PlayWrEx(buf, bufcount, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_PlayWrEx(buf, bufcount, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTConfigEx(ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTConfigEx(configval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTConfigEx(configval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTConfigExx(ByVal configval As UInt32, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTConfigExx(configval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTConfigExx(configval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTCreateListEx(ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTCreateListEx(listconfigval, skipval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTCreateListEx(listconfigval, skipval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTCreateListExx(ByVal listconfigval As UInt32, ByVal skipval As Int32, ByVal count As Int32, ByVal msgconfigval As UInt32, ByVal mcflag As Int32, ByVal rtlink As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTCreateListExx(listconfigval, skipval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTCreateListExx(listconfigval, skipval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTResponseTimeRd(ByRef timeval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTResponseTimeRd(timeval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTResponseTimeRd(timeval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTResponseTimeWr(ByVal timeval As UInt16, ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTResponseTimeWr(timeval, rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTResponseTimeWr(timeval, rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_RTStatus(ByVal rtlink As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_RTStatus(rtlink, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_RTStatus(rtlink, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SABlkWr(ByVal rtlink As Int32, ByVal saval As Int32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SABlkWr(rtlink, saval, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SABlkWr(rtlink, saval, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SABlkWrEx(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SABlkWrEx(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SABlkWrEx(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SchedBuild(ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal freq As Int32(), ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedBuild(nummsgs, msgaddr, freq, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SchedBuild(nummsgs, msgaddr, freq, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SchedBuildEx(ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal freq As Int32(), ByVal period As Int32(), ByVal buf As IntPtr(), ByVal bufsize As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedBuildEx(nummsgs, msgaddr, freq, period, buf, bufsize, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SchedBuildEx(nummsgs, msgaddr, freq, period, buf, bufsize, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SchedBuildExx(ByVal frametime As UInt32, ByVal nummsgs As Int32, ByVal msgaddr As UInt32(), ByVal period As Int32(), ByVal buf As IntPtr(), ByVal bufsize As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedBuildExx(frametime, nummsgs, msgaddr, period, buf, bufsize, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SchedBuildExx(frametime, nummsgs, msgaddr, period, buf, bufsize, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SchedCalc(ByRef msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedCalc(msgaddr, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SchedCalc(msgaddr, channum, handleval)
        End If
    End Function
    Public Sub BTIEBR_SchedDisplay(ByVal nummsgs As UInt32, ByRef count As UInt32)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_SchedDisplay(nummsgs, count)
        Else
             BTIEBR_64.BTIEBR_SchedDisplay(nummsgs, count)
        End If
    End Sub
    Public Sub BTIEBR_SchedHook(ByVal index As Int32, ByVal ptr As IntPtr)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_SchedHook(index, ptr)
        Else
             BTIEBR_64.BTIEBR_SchedHook(index, ptr)
        End If
    End Sub
    Public Function BTIEBR_SchedInsEnd(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedInsEnd(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SchedInsEnd(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SchedInsMsg(ByVal index As UInt32, ByRef msgaddr As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedInsMsg(index, msgaddr, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SchedInsMsg(index, msgaddr, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SchedInsStart(ByVal frametime As UInt32, ByVal count As UInt32, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedInsStart(frametime, count, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SchedInsStart(frametime, count, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SchedMaxval(ByVal arrayval As UInt32(), ByVal arrayvalcount As UInt32) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedMaxval(arrayval, arrayvalcount)
        Else
            Return BTIEBR_64.BTIEBR_SchedMaxval(arrayval, arrayvalcount)
        End If
    End Function
    Public Function BTIEBR_SchedMinval(ByVal arrayval As UInt32(), ByVal arrayvalcount As UInt32) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SchedMinval(arrayval, arrayvalcount)
        Else
            Return BTIEBR_64.BTIEBR_SchedMinval(arrayval, arrayvalcount)
        End If
    End Function
    Public Sub BTIEBR_SchedPrint(ByVal ParamArray list() As String)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_SchedPrint(list)
        Else
             BTIEBR_64.BTIEBR_SchedPrint(list)
        End If
    End Sub
    Public Function BTIEBR_SerialClear(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SerialClear(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SerialClear(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SerialConfig(ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SerialConfig(configval, count, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SerialConfig(configval, count, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SerialConfigEx(ByVal configval As UInt32, ByVal count As UInt16, ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SerialConfigEx(configval, count, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SerialConfigEx(configval, count, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SerialStatus(ByVal channum As Int32, ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SerialStatus(channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SerialStatus(channum, handleval)
        End If
    End Function
    Public Function BTIEBR_SerialWr(ByVal buf As UInt16(), ByVal channum As Int32, ByVal handleval As IntPtr) As UInt32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_SerialWr(buf, channum, handleval)
        Else
            Return BTIEBR_64.BTIEBR_SerialWr(buf, channum, handleval)
        End If
    End Function
    Public Function BTIEBR_TestProtocol(ByVal handleval As IntPtr) As Int32
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_TestProtocol(handleval)
        Else
            Return BTIEBR_64.BTIEBR_TestProtocol(handleval)
        End If
    End Function
    Public Function BTIEBR_ValPackRTCWD(ByVal mcflag As Int32, ByVal taval As Int32, ByVal trflag As Int32, ByVal saval As Int32, ByVal count As Int32) As UInt16
        If (IntPtr.Size = 4) Then
            Return BTIEBR_32.BTIEBR_ValPackRTCWD(mcflag, taval, trflag, saval, count)
        Else
            Return BTIEBR_64.BTIEBR_ValPackRTCWD(mcflag, taval, trflag, saval, count)
        End If
    End Function
    Public Sub BTIEBR_ValUnpackRTCWD(ByVal cwd As UInt16, ByRef bcastflag As Int32, ByRef mcflag As Int32, ByRef taval As Int32, ByRef trflag As Int32, ByRef saval As Int32, ByRef count As Int32)
        If (IntPtr.Size = 4) Then
             BTIEBR_32.BTIEBR_ValUnpackRTCWD(cwd, bcastflag, mcflag, taval, trflag, saval, count)
        Else
             BTIEBR_64.BTIEBR_ValUnpackRTCWD(cwd, bcastflag, mcflag, taval, trflag, saval, count)
        End If
    End Sub

    '/**
    '*
    '*  Channel number constants
    '*
    '**/

    Public Const CH0 As UInt16 = &H0 '//Channel 0
    Public Const CH1 As UInt16 = &H1 '//Channel 1
    Public Const CH2 As UInt16 = &H2 '//Channel 2
    Public Const CH3 As UInt16 = &H3 '//Channel 3
    Public Const CH4 As UInt16 = &H4 '//Channel 4
    Public Const CH5 As UInt16 = &H5 '//Channel 5
    Public Const CH6 As UInt16 = &H6 '//Channel 6
    Public Const CH7 As UInt16 = &H7 '//Channel 7
    Public Const CH8 As UInt16 = &H8 '//Channel 8
    Public Const CH9 As UInt16 = &H9 '//Channel 9
    Public Const CH10 As UInt16 = &HA    '//Channel 10
    Public Const CH11 As UInt16 = &HB    '//Channel 11
    Public Const CH12 As UInt16 = &HC    '//Channel 12
    Public Const CH13 As UInt16 = &HD    '//Channel 13
    Public Const CH14 As UInt16 = &HE    '//Channel 14
    Public Const CH15 As UInt16 = &HF    '//Channel 15
    Public Const CH16 As UInt16 = &H10    '//Channel 16
    Public Const CH17 As UInt16 = &H11    '//Channel 17
    Public Const CH18 As UInt16 = &H12    '//Channel 18
    Public Const CH19 As UInt16 = &H13    '//Channel 19
    Public Const CH20 As UInt16 = &H14    '//Channel 20
    Public Const CH21 As UInt16 = &H15    '//Channel 21
    Public Const CH22 As UInt16 = &H16    '//Channel 22
    Public Const CH23 As UInt16 = &H17    '//Channel 23
    Public Const CH24 As UInt16 = &H18    '//Channel 24
    Public Const CH25 As UInt16 = &H19    '//Channel 25
    Public Const CH26 As UInt16 = &H1A    '//Channel 26
    Public Const CH27 As UInt16 = &H1B    '//Channel 27
    Public Const CH28 As UInt16 = &H1C    '//Channel 28
    Public Const CH29 As UInt16 = &H1D    '//Channel 29
    Public Const CH30 As UInt16 = &H1E    '//Channel 30
    Public Const CH31 As UInt16 = &H1F    '//Channel 31

    '/**
    '*
    '*  Logical hub configuration options
    '*
    '**/

    Public Const LHUBCFGEBR_DEFAULT As UInt32 = &H0  '//Select all default settings
    Public Const LHUBCFGEBR_ENABLE As UInt32 = &H0   '//Enable all external ports (default)
    Public Const LHUBCFGEBR_DISABLE As UInt32 = &H1  '//Disable all external ports
    Public Const LHUBCFGEBR_SPEC As UInt32 = &H0 '//LHub will be configured for Spec mode (default)
    Public Const LHUBCFGEBR_SWITCH As UInt32 = &H10   '//LHub will be configured for Switch mode
    Public Const LHUBCFGEBR_LINK As UInt32 = &H20 '//LHub will be configured for Link mode

    '/**
    '*
    '*  Bus controller configuration options
    '*
    '**/

    Public Const BCCFGEBR_DEFAULT As UInt32 = &H0    '//Select all default settings
    Public Const BCCFGEBR_ENABLE As UInt32 = &H0 '//Enable bus controller (default)
    Public Const BCCFGEBR_DISABLE As UInt32 = &H1    '//Disable bus controller
    Public Const BCCFGEBR_NOLOGHALT As UInt32 = &H0  '//Do not generate event log on halt (default)
    Public Const BCCFGEBR_LOGHALT As UInt32 = &H100    '//Generate event log on halt
    Public Const BCCFGEBR_NOLOGPAUSE As UInt32 = &H0 '//Do not generate event log on pause (default)
    Public Const BCCFGEBR_LOGPAUSE As UInt32 = &H200   '//Generate event log on pause
    Public Const BCCFGEBR_NOLOOPMAX As UInt32 = &H0  '//Disable maximum loop count (default)
    Public Const BCCFGEBR_LOOPMAX As UInt32 = &H1000    '//Enable maximum loop count
    Public Const BCCFGEBR_NOSTEP As UInt32 = &H0 '//Disable single stepping (default)
    Public Const BCCFGEBR_STEP As UInt32 = &H2000   '//Enable single stepping
    Public Const BCCFGEBR_MC01 As UInt32 = &H0   '//Select SA=00000 or SA=11111 for mode codes (default)
    Public Const BCCFGEBR_MC1 As UInt32 = &H1000000    '//Select SA=11111 for mode codes
    Public Const BCCFGEBR_MC0 As UInt32 = &H2000000    '//Select SA=00000 for mode codes
    Public Const BCCFGEBR_MCNONE As UInt32 = &H3000000 '//Disable mode codes
    Public Const BCCFGEBR_PAUSE As UInt32 = &H400  '//Mark channel as paused
    Public Const BCCFGEBR_UNPAUSE As UInt32 = &H0    '//Mark channel as unpaused (default)
    Public Const BCCFGEBR_SELFTEST As UInt32 = &H800000   '//This channel will transmit/receive on the internal self-test bus
    Public Const BCCFGEBR_SELFTESTOFF As UInt32 = &H0    '//This channel will transmit/receive on the operational bus (default)

    '/**
    '*
    '*  Monitor configuration options
    '*
    '**/

    Public Const MONCFGEBR_DEFAULT As UInt32 = &H0   '//Select all default settings
    Public Const MONCFGEBR_ENABLE As UInt32 = &H0    '//Enable monitor
    Public Const MONCFGEBR_DISABLE As UInt32 = &H1   '//Disable monitor
    Public Const MONCFGEBR_NOBCAST As UInt32 = &H0   '//Disable broadcast (default)
    Public Const MONCFGEBR_BCAST As UInt32 = &H100 '//Enable broadcast
    Public Const MONCFGEBR_COMPLETE As UInt32 = &H0  '//Enable receiving complete words (default)
    Public Const MONCFGEBR_INCOMPLETE As UInt32 = &H10000    '//Enable receiving incomplete words
    Public Const MONCFGEBR_MC01 As UInt32 = &H0  '//Select SA=00000 or SA=11111 for mode codes (default)
    Public Const MONCFGEBR_MC1 As UInt32 = &H100000   '//Select SA=11111 for mode codes
    Public Const MONCFGEBR_MC0 As UInt32 = &H200000   '//Select SA=00000 for mode codes
    Public Const MONCFGEBR_MCNONE As UInt32 = &H300000    '//Disable mode codes
    Public Const MONCFGEBR_SELFTEST As UInt32 = &H800000  '//This channel will transmit/receive on the internal self-test bus
    Public Const MONCFGEBR_SELFTESTOFF As UInt32 = &H0   '//This channel will transmit/receive on the operational bus (default)

    '/**
    '*
    '*  Remote terminal configuration options
    '*
    '**/

    Public Const RTCFGEBR_DEFAULT As UInt32 = &H0    '//Select all default settings
    Public Const RTCFGEBR_SIMULATE As UInt32 = &H0   '//Enable RT simulation (default)
    Public Const RTCFGEBR_DISABLE As UInt32 = &H1    '//Disable RT
    Public Const RTCFGEBR_MONITOR As UInt32 = &H2    '//Enable RT monitor
    Public Const RTCFGEBR_NOBCAST As UInt32 = &H0    '//Disable broadcast (default)
    Public Const RTCFGEBR_BCAST As UInt32 = &H100  '//Enable broadcast
    Public Const RTCFGEBR_NOAUTOBUSY As UInt32 = &H0 '//Disable auto busy (default)
    Public Const RTCFGEBR_AUTOBUSY As UInt32 = &H200   '//Enable auto busy
    Public Const RTCFGEBR_BUILD As UInt32 = &H0  '//Enable auto building (default)
    Public Const RTCFGEBR_NOBUILD As UInt32 = &H400    '//Disable auto building
    Public Const RTCFGEBR_NOIMMCLR As UInt32 = &H0   '//Do not clear status word bits (default)
    Public Const RTCFGEBR_IMMCLR As UInt32 = &H2000 '//Status word bits are cleared immediately
    Public Const RTCFGEBR_NOBCASTADDR As UInt32 = &H0    '//Disable broadcast handling for address (default)
    Public Const RTCFGEBR_BCASTADDR As UInt32 = &H4000  '//Enable broadcast handling for address
    Public Const RTCFGEBR_MC01 As UInt32 = &H0   '//Select SA=00000 or SA=11111 for mode codes (default)
    Public Const RTCFGEBR_MC1 As UInt32 = &H1000000    '//Select SA=11111 for mode codes
    Public Const RTCFGEBR_MC0 As UInt32 = &H2000000    '//Select SA=00000 for mode codes
    Public Const RTCFGEBR_MCNONE As UInt32 = &H3000000 '//Disable mode codes
    Public Const RTCFGEBR_MCILLEGAL As UInt32 = &H4000000  '//Illegalize mode codes defined as "Reserved - Not to be Used" in EBR
    Public Const RTCFGEBR_WIPE As UInt32 = &H0   '//Enables message clear (default)
    Public Const RTCFGEBR_NOWIPE As UInt32 = 2147483648 '//Disables message clear
    Public Const RTCFGEBR_WIPE0 As UInt32 = &H0  '//Initialize data with zeros (default)
    Public Const RTCFGEBR_WIPE123 As UInt32 = &H10000000    '//Initialize data with incrementing values
    Public Const RTCFGEBR_WIPECWD As UInt32 = &H20000000    '//Initialize data with command word
    Public Const RTCFGEBR_SELFTEST As UInt32 = &H800000   '//This channel will transmit/receive on the internal self-test bus
    Public Const RTCFGEBR_SELFTESTOFF As UInt32 = &H0    '//This channel will transmit/receive on the operational bus (default)

    '/**
    '*
    '*  Playback configuration options
    '*
    '**/

    Public Const PLAYCFGEBR_DEFAULT As UInt32 = &H0  '//Select all default settings
    Public Const PLAYCFGEBR_ENABLE As UInt32 = &H0   '//Enable Playback mode
    Public Const PLAYCFGEBR_DISABLE As UInt32 = &H1  '//Disable Playback mode
    Public Const PLAYCFGEBR_16K As UInt32 = &H0  '//Allocate a 16K playback buffer (default)
    Public Const PLAYCFGEBR_ALLAVAIL As UInt32 = &H10 '//Allocate all available memory to playback buffer
    Public Const PLAYCFGEBR_32K As UInt32 = &H20  '//Allocate a 32K playback buffer
    Public Const PLAYCFGEBR_64K As UInt32 = &H40  '//Allocate a 64K playback buffer
    Public Const PLAYCFGEBR_128K As UInt32 = &H80 '//Allocate a 128K playback buffer
    Public Const PLAYCFGEBR_MC01 As UInt32 = &H0 '//Select SA=00000 or SA=11111 for mode codes (default)
    Public Const PLAYCFGEBR_MC1 As UInt32 = &H1000000  '//Select SA=11111 for mode codes
    Public Const PLAYCFGEBR_MC0 As UInt32 = &H2000000  '//Select SA=00000 for mode codes
    Public Const PLAYCFGEBR_MCNONE As UInt32 = &H3000000   '//Disable mode codes

    '/**
    '*
    '*  Serial configuration options
    '*
    '**/

    Public Const SERCFGEBR_DEFAULT As UInt32 = &H0   '//Select all default settings
    Public Const SERCFGEBR_CONTINUOUS As UInt32 = &H0    '//Enable continuous mode (default)
    Public Const SERCFGEBR_DISABLE As UInt32 = &H1   '//Disable free mode
    Public Const SERCFGEBR_FREE As UInt32 = &H2  '//Enable free mode
    Public Const SERCFGEBR_NOLOGEMPTY As UInt32 = &H0    '//Do not generate event log when empty (default)
    Public Const SERCFGEBR_LOGEMPTY As UInt32 = &H10  '//Generate event log when empty

    '/**
    '*
    '*  Error configuration options
    '*
    '**/

    Public Const ERRDEFEBR_NONE As UInt32 = &H0  '//Disable error generation (default)
    Public Const ERRDEFEBR_GAP As UInt32 = &H10000   '//Generate preceding gap error
    Public Const ERRDEFEBR_CNTWRDABS As UInt32 = &H20000 '//Generate an absolute word count error
    Public Const ERRDEFEBR_CNTWRDREL As UInt32 = &H40000 '//Generate a relative count error
    Public Const ERRDEFEBR_CNTBIT As UInt32 = &H80000    '//Generate a bit count error
    Public Const ERRDEFEBR_MAN1 As UInt32 = &H100000  '//Generate manchester error on first half of bit
    Public Const ERRDEFEBR_MAN2 As UInt32 = &H200000  '//Generate manchester error on second half of bit
    Public Const ERRDEFEBR_SYNC As UInt32 = &H400000  '//Generate a sync error
    Public Const ERRDEFEBR_PAR As UInt32 = &H800000   '//Generate a parity error
    Public Const ERRDEFEBR_ZEROX1 As UInt32 = &H1000000    '//Generate zero crossing error on leading zero crossing (if exists)
    Public Const ERRDEFEBR_ZEROX2 As UInt32 = &H2000000    '//Generate zero crossing error on mid-bit zero crossing
    Public Const ERRDEFEBR_DWD00 As UInt32 = &H0 '//Error will be generated in data word  0
    Public Const ERRDEFEBR_DWD01 As UInt32 = &H1 '//Error will be generated in data word  1
    Public Const ERRDEFEBR_DWD02 As UInt32 = &H2 '//Error will be generated in data word  2
    Public Const ERRDEFEBR_DWD03 As UInt32 = &H3 '//Error will be generated in data word  3
    Public Const ERRDEFEBR_DWD04 As UInt32 = &H4 '//Error will be generated in data word  4
    Public Const ERRDEFEBR_DWD05 As UInt32 = &H5 '//Error will be generated in data word  5
    Public Const ERRDEFEBR_DWD06 As UInt32 = &H6 '//Error will be generated in data word  6
    Public Const ERRDEFEBR_DWD07 As UInt32 = &H7 '//Error will be generated in data word  7
    Public Const ERRDEFEBR_DWD08 As UInt32 = &H8 '//Error will be generated in data word  8
    Public Const ERRDEFEBR_DWD09 As UInt32 = &H9 '//Error will be generated in data word  9
    Public Const ERRDEFEBR_DWD10 As UInt32 = &HA '//Error will be generated in data word 10
    Public Const ERRDEFEBR_DWD11 As UInt32 = &HB '//Error will be generated in data word 11
    Public Const ERRDEFEBR_DWD12 As UInt32 = &HC '//Error will be generated in data word 12
    Public Const ERRDEFEBR_DWD13 As UInt32 = &HD '//Error will be generated in data word 13
    Public Const ERRDEFEBR_DWD14 As UInt32 = &HE '//Error will be generated in data word 14
    Public Const ERRDEFEBR_DWD15 As UInt32 = &HF '//Error will be generated in data word 15
    Public Const ERRDEFEBR_DWD16 As UInt32 = &H10 '//Error will be generated in data word 16
    Public Const ERRDEFEBR_DWD17 As UInt32 = &H11 '//Error will be generated in data word 17
    Public Const ERRDEFEBR_DWD18 As UInt32 = &H12 '//Error will be generated in data word 18
    Public Const ERRDEFEBR_DWD19 As UInt32 = &H13 '//Error will be generated in data word 19
    Public Const ERRDEFEBR_DWD20 As UInt32 = &H14 '//Error will be generated in data word 20
    Public Const ERRDEFEBR_DWD21 As UInt32 = &H15 '//Error will be generated in data word 21
    Public Const ERRDEFEBR_DWD22 As UInt32 = &H16 '//Error will be generated in data word 22
    Public Const ERRDEFEBR_DWD23 As UInt32 = &H17 '//Error will be generated in data word 23
    Public Const ERRDEFEBR_DWD24 As UInt32 = &H18 '//Error will be generated in data word 24
    Public Const ERRDEFEBR_DWD25 As UInt32 = &H19 '//Error will be generated in data word 25
    Public Const ERRDEFEBR_DWD26 As UInt32 = &H1A '//Error will be generated in data word 26
    Public Const ERRDEFEBR_DWD27 As UInt32 = &H1B '//Error will be generated in data word 27
    Public Const ERRDEFEBR_DWD28 As UInt32 = &H1C '//Error will be generated in data word 28
    Public Const ERRDEFEBR_DWD29 As UInt32 = &H1D '//Error will be generated in data word 29
    Public Const ERRDEFEBR_DWD30 As UInt32 = &H1E '//Error will be generated in data word 30
    Public Const ERRDEFEBR_DWD31 As UInt32 = &H1F '//Error will be generated in data word 31
    Public Const ERRDEFEBR_CWD As UInt32 = &H100   '//Error will be generated in CWD
    Public Const ERRDEFEBR_SWD As UInt32 = &H102   '//Error will be generated in SWD

    '/**
    '*
    '*  Error control options
    '*
    '**/

    Public Const ERRCTRLEBR_OFF As UInt32 = &H0  '//Disable error generation
    Public Const ERRCTRLEBR_ON As UInt32 = &H1   '//Enable error generation
    Public Const ERRCTRLEBR_ONCE As UInt32 = &H2 '//Enable error generation once
    Public Const ERRCTRLEBR_TAGMSG As UInt32 = &H0   '//Generate errors on tagged messages
    Public Const ERRCTRLEBR_ANYMSG As UInt32 = 2147483648   '//Generate errors on any message

    '/**
    '*
    '*  Message configuration options
    '*
    '**/

    Public Const MSGCRTEBR_DEFAULT As UInt32 = &H0   '//Default settings
    Public Const MSGCRTEBR_ENABLE As UInt32 = &H0    '//Enable subaddress (RT) (default)
    Public Const MSGCRTEBR_DISABLE As UInt32 = &H1000   '//Disable subaddress (RT)
    Public Const MSGCRTEBR_RESP As UInt32 = &H0  '//Enable response (RT) (default)
    Public Const MSGCRTEBR_NORESP As UInt32 = &H2000    '//Disable response (RT)
    Public Const MSGCRTEBR_NOWRAP As UInt32 = &H0    '//Disable data wrap (RT) (default)
    Public Const MSGCRTEBR_WRAP As UInt32 = &H4000  '//Enable data wrap (RT)
    Public Const MSGCRTEBR_NOLOG As UInt32 = &H0 '//Message will not generate event log (default)
    Public Const MSGCRTEBR_LOG As UInt32 = &H1   '//Message will generate event log
    Public Const MSGCRTEBR_NOERR As UInt32 = &H0 '//No message error will be generated for message (default)
    Public Const MSGCRTEBR_ERR As UInt32 = &H2   '//A message error will be generated for message
    Public Const MSGCRTEBR_BCRT As UInt32 = &H0  '//Message is a BC-RT transfer (BC) (default)
    Public Const MSGCRTEBR_NOMON As UInt32 = &H0 '//Message will not be monitored (default)
    Public Const MSGCRTEBR_MON As UInt32 = &H10   '//Message will be monitored
    Public Const MSGCRTEBR_NOTIMETAG As UInt32 = &H0 '//Message will not record time-tag (default)
    Public Const MSGCRTEBR_TIMETAG As UInt32 = &H40   '//Message will record time-tag
    Public Const MSGCRTEBR_NOELAPSE As UInt32 = &H0  '//Message will not record elapse time (default)
    Public Const MSGCRTEBR_ELAPSE As UInt32 = &H80    '//Message will record elapse time
    Public Const MSGCRTEBR_NOMIN As UInt32 = &H0 '//Message will not record min time (default)
    Public Const MSGCRTEBR_MIN As UInt32 = &H100   '//Message will record min time
    Public Const MSGCRTEBR_NOMAX As UInt32 = &H0 '//Message will not record max time (default)
    Public Const MSGCRTEBR_MAX As UInt32 = &H200   '//Message will record max time
    Public Const MSGCRTEBR_NOHIT As UInt32 = &H0 '//Message will not record hit count (default)
    Public Const MSGCRTEBR_HIT As UInt32 = &H400   '//Message will record hit count
    Public Const MSGCRTEBR_WIPE As UInt32 = &H0  '//Enables message clear (default)
    Public Const MSGCRTEBR_NOWIPE As UInt32 = 2147483648    '//Disables message clear
    Public Const MSGCRTEBR_WIPE0 As UInt32 = &H0 '//Initialize data with zeros (default)
    Public Const MSGCRTEBR_WIPE123 As UInt32 = &H1000000   '//Initialize data with incrementing values
    Public Const MSGCRTEBR_WIPECWD As UInt32 = &H2000000   '//Initialize data with command word

    '/**
    '*
    '*  List buffer options
    '*
    '**/

    Public Const LISTCRTEBR_DEFAULT As UInt32 = &H0  '//Select all default settings
    Public Const LISTCRTEBR_FIFO As UInt32 = &H0 '//Enable FIFO mode
    Public Const LISTCRTEBR_CIRCULAR As UInt32 = &H1 '//Enable circular mode
    Public Const LISTCRTEBR_TRBIT As UInt32 = &H0    '//User will read or write depending upon T/R bit
    Public Const LISTCRTEBR_READ As UInt32 = &H10 '//User will read from list buffer
    Public Const LISTCRTEBR_WRITE As UInt32 = &H20    '//User will write to list buffer
    Public Const LISTCRTEBR_NOTSKIP As UInt32 = &H0  '//Will not skip messages
    Public Const LISTCRTEBR_SKIP As UInt32 = &H40 '//Enable message skipping
    Public Const LISTCRTEBR_ASYNC As UInt32 = &H80    '//Asynchronous mode
    Public Const LISTCRTEBR_NOLOG As UInt32 = &H0    '//Do not generate event log when list buffer empty/full (default)
    Public Const LISTCRTEBR_LOG As UInt32 = &H100  '//Generate event log when list buffer empty/full
    Public Const LISTCRTEBR_NOHIT As UInt32 = &H0    '//Do not generate event log when list is hit (default)
    Public Const LISTCRTEBR_HIT As UInt32 = &H200  '//Generate event log when list is hit
    Public Const LISTCRTEBR_NOLOGHALF As UInt32 = &H0    '//Do not generate event log on middle and last entries (default)
    Public Const LISTCRTEBR_LOGHALF As UInt32 = &H400  '//Generate event log on middle and last entries

    '/**
    '*
    '*  Condition flags.
    '*
    '**/

    Public Const CONDEBR_ALWAYS As UInt32 = &H0  '//Unconditional
    Public Const CONDEBR_FAIL As UInt32 = &H1    '//Condition when all retries have failed
    Public Const CONDEBR_SRQ As UInt32 = &H2 '//Condition on Service Request bit set in status word
    Public Const CONDEBR_INS As UInt32 = &H4 '//Condition on Instrumentation bit set in status word
    Public Const CONDEBR_SSF As UInt32 = &H8 '//Condition on Subsystem Fail bit set in status word
    Public Const CONDEBR_TF As UInt32 = &H10  '//Condition on Terminal Flag bit set in status word
    Public Const CONDEBR_BUSY As UInt32 = &H20    '//Condition on Busy bit set in status word
    Public Const CONDEBR_ME As UInt32 = &H40  '//Condition on Message Error bit set in status word
    Public Const CONDEBR_RESPERR As UInt32 = &H80 '//Condition on response error
    Public Const CONDEBR_NORESP As UInt32 = &H100  '//Condition on no response error
    Public Const CONDEBR_DIO1ACT As UInt32 = &H1000 '//Condition on digital I/O #1 active
    Public Const CONDEBR_DIO1NACT As UInt32 = &H2000    '//Condition on digital I/O #1 not active
    Public Const CONDEBR_DIO2ACT As UInt32 = &H4000 '//Condition on digital I/O #2 active
    Public Const CONDEBR_DIO2NACT As UInt32 = &H8000    '//Condition on digital I/O #2 not active
    Public Const CONDEBR_DIO3ACT As UInt32 = &H10000 '//Condition on digital I/O #3 active
    Public Const CONDEBR_DIO3NACT As UInt32 = &H20000    '//Condition on digital I/O #3 not active
    Public Const CONDEBR_DIO4ACT As UInt32 = &H40000 '//Condition on digital I/O #4 active
    Public Const CONDEBR_DIO4NACT As UInt32 = &H80000    '//Condition on digital I/O #4 not active

    '/**
    '*
    '*  Bit fields of the status word
    '*
    '**/

    Public Const SWDEBR_TAMASK As UInt32 = &HF800   '//Masks off RT address in the SWD
    Public Const SWDEBR_ME As UInt32 = &H400   '//Message error bit
    Public Const SWDEBR_INST As UInt32 = &H200 '//Instrumentation bit
    Public Const SWDEBR_SRQ As UInt32 = &H100  '//Service request bit
    Public Const SWDEBR_BCAST As UInt32 = &H10    '//Broadcast received bit
    Public Const SWDEBR_BUSY As UInt32 = &H8 '//Busy bit
    Public Const SWDEBR_SSF As UInt32 = &H4  '//Subsystem flag bit
    Public Const SWDEBR_DBC As UInt32 = &H2  '//Dynamic bus control acceptance bit
    Public Const SWDEBR_TF As UInt32 = &H1   '//Terminal flag bit

    '/**
    '*
    '*  Bit fields of the message error flag
    '*
    '**/

    Public Const MSGERREBR_NORESP As UInt32 = &H8000    '//Set when we timeout waiting for response
    Public Const MSGERREBR_ANYERR As UInt32 = &H4000    '//Set when any error bit is set
    Public Const MSGERREBR_PROTOCOL As UInt32 = &H2000  '//General protocol error
    Public Const MSGERREBR_SYNC As UInt32 = &H1000  '//Set when we have the wrong sync
    Public Const MSGERREBR_DATACOUNT As UInt32 = &H800 '//Set when improper number of data words
    Public Const MSGERREBR_LHUB As UInt32 = &H40  '//Set when unexpected LHUB activity detected
    Public Const MSGERREBR_MANCH As UInt32 = &H20 '//Set when manchester error detected
    Public Const MSGERREBR_PARITY As UInt32 = &H10    '//Set when parity error detected
    Public Const MSGERREBR_WORD As UInt32 = &H8  '//Set when word error detected
    Public Const MSGERREBR_RETRY As UInt32 = &H4 '//All retries failed
    Public Const MSGERREBR_SYSTEM As UInt32 = &H2    '//An internal system error occurred
    Public Const MSGERREBR_HIT As UInt32 = &H1   '//Always set

    '/**
    '*
    '*  Bit fields of the message activity flag.
    '*
    '**/

    Public Const MSGACTEBR_CHMASK As UInt32 = &HF800    '//Channel number mask value
    Public Const MSGACTEBR_CHSHIFT As UInt32 = &HB   '//Channel number shift value
    Public Const MSGACTEBR_XMTCWD As UInt32 = &H80    '//Transmitted CWD
    Public Const MSGACTEBR_XMTSWD As UInt32 = &H40    '//Transmitted SWD
    Public Const MSGACTEBR_RCVCWD As UInt32 = &H20    '//Received CWD
    Public Const MSGACTEBR_RCVSWD As UInt32 = &H10    '//Received SWD
    Public Const MSGACTEBR_XMTDWD As UInt32 = &H8    '//Transmitted DWD
    Public Const MSGACTEBR_RCVDWD As UInt32 = &H4    '//Received DWD
    Public Const MSGACTEBR_MODEMASK As UInt32 = &H3  '//LHub Mode mask value
    Public Const MSGACTEBR_MODESHIFT As UInt32 = &H0 '//LHub Mode shift value

    '/**
    '*
    '*  Bit fields within the command info (cwdinfo).
    '*
    '**/

    Public Const CWDINFOEBR_RESVMASK As UInt32 = &HFFE0 '//Reserved mask value
    Public Const CWDINFOEBR_LINKMASK As UInt32 = &H1F '//Link number mask value
    Public Const CWDINFOEBR_LINKSHIFT As UInt32 = &H0    '//Link number shift value

    '/**
    '*
    '*  Bit fields within the status info (swdinfo).
    '*
    '**/

    Public Const SWDINFOEBR_RESVMASK As UInt32 = &HFFE0 '//Reserved mask value
    Public Const SWDINFOEBR_LINKMASK As UInt32 = &H1F '//Link number mask value
    Public Const SWDINFOEBR_LINKSHIFT As UInt32 = &H0    '//Link number shift value

    '/**
    '*
    '*  Message activity LHub Mode values.
    '*
    '**/

    Public Const LHUBMODE_SPEC As UInt32 = &H0   '//LHub mode is Spec
    Public Const LHUBMODE_SWITCH As UInt32 = &H1 '//LHub mode is Switch
    Public Const LHUBMODE_LINK As UInt32 = &H2   '//LHub mode is Link

    '/**
    '*
    '*  Field types.
    '*
    '**/

    Public Const FIELDEBR_CWD As UInt32 = &H0    '//CWD field
    Public Const FIELDEBR_CWDINFO As UInt32 = &H1    '//CWD info field
    Public Const FIELDEBR_SWD As UInt32 = &H2    '//SWD field
    Public Const FIELDEBR_SWDINFO As UInt32 = &H3    '//SWD info field
    Public Const FIELDEBR_TTAG As UInt32 = &H4   '//Time tag field
    Public Const FIELDEBR_ELAPSE As UInt32 = &H5 '//Elapse time field
    Public Const FIELDEBR_ERROR As UInt32 = &H6  '//Error field
    Public Const FIELDEBR_ACT As UInt32 = &H7    '//Activity field
    Public Const FIELDEBR_RESP As UInt32 = &H8   '//Response time field
    Public Const FIELDEBR_COUNT As UInt32 = &H9  '//Data count field
    Public Const FIELDEBR_FLAG1 As UInt32 = &HA  '//Flag 1 field
    Public Const FIELDEBR_FLAG2 As UInt32 = &HB  '//Flag 2 field
    Public Const FIELDEBR_EXTFLAG As UInt32 = &HC    '//External flag field
    Public Const FIELDEBR_TTAGH As UInt32 = &HD  '//Extended time tag field

    '/**
    '*
    '*  Channel info fields.
    '*
    '**/

    Public Const INFOEBR_MODEL As UInt32 = &H1   '//Channel model type
    Public Const INFOEBR_MAXCOUNT As UInt32 = &H2    '//Channel terminal count
    Public Const INFOEBR_MULTIMODE As UInt32 = &H3   '//Channel supports multi-mode
    Public Const INFOEBR_ERRORGEN As UInt32 = &H4    '//Channel supports error generation
    Public Const INFOEBR_CONCURMON As UInt32 = &H5   '//Channel supports concurrent monitor
    Public Const INFOEBR_MONFILTER As UInt32 = &H6   '//Channel supports filtering of monitor
    Public Const INFOEBR_PARAM As UInt32 = &H7   '//Channel supports parametric control
    Public Const INFOEBR_RTRESPTIME As UInt32 = &H8  '//Channel supports programmable RT response time
    Public Const INFOEBR_PLAYBACK As UInt32 = &H9    '//Channel supports hardware playback mode
    Public Const INFOEBR_VER2CHOBJ As UInt32 = &HA   '//Channel supports version 2 channel object
    Public Const INFOEBR_BCDFLTGAP As UInt32 = &HB   '//Channel supports programmable default gap time

    '/**
    '*
    '*  Other flags.
    '*
    '**/

    Public Const SUBADDRESS As UInt16 = &H0  '//Selects Subaddress messages
    Public Const MODECODE As UInt16 = &H1    '//Selects Modecode messages
    Public Const EBR_DISABLED As Int16 = -1 '//Flag setting/indicating a disabled RT link or port

End Module
