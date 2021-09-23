using System;
using System.Runtime.InteropServices;

namespace BALLARD
{
    public class BTIEBR
    {
        /**
        *
        *  BTIEBR WIN32 DRIVER  Version 1.0.2  (01/27/2015)
        *  Copyright (c) 2013-2015
        *  Ballard Technology, Inc.
        *  www.ballardtech.com
        *  support@ballardtech.com
        *  ALL RIGHTS RESERVED
        *
        *  NAME:   BTIEBRNET.CS -- C# 32-bit/64-bit
        *                       BTIEBR Driver Include Header File.
        *
        **/

        /**
        *
        *  Structs used by the BTIEBR Driver.
        *
        **/

        [StructLayout(LayoutKind.Sequential)]
        public struct XMITFIELDSEBR
        {
            public UInt32 ctrlflags;           //User writes message configuration optionvals
            public UInt16 flag1;               //Card writes message flag 1
            public UInt16 flag2;               //Card writes message flag 2
            public UInt16 errflags;            //Card writes errorval flags
            public UInt16 actflags;            //Card writes activity flags
            public UInt16 resptime;            //Card writes response time
            public UInt16 resv7;               //Reserved field
            public UInt16 datacount;           //Card writes data count
            public UInt16 extflag;             //Driver writes external flags
            public UInt32 timetag;             //Card writes time tag
            public UInt32 elapsetime;          //Card writes elapsed time
            public UInt32 preaddr;             //Driver writes pre-proc address
            public UInt32 postaddr;            //Driver writes post-proc address
            public UInt32 timetagh;            //Card writes extended timetag
            public UInt16 resv18;              //Reserved field
            public UInt16 resv19;              //Reserved field
            public UInt16 cwd;                 //User writes command word
            public UInt16 cwdinfo;    //User writes command word info
            public UInt16 swd;                 //Card writes status word
            public UInt16 swdinfo;    //Card writes status word info
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public UInt16[] data;	//User writes data words
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public UInt16[] extra;	//User writes extra words
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSGFIELDSEBR
        {
            public UInt16 msgflag;             //Driver writes message flags
            public UInt16 msgopt;              //Driver writes message optionvals
            public UInt16 msgerr;              //Card writes message errorvals
            public UInt16 msgact;              //Card writes message activity
            public UInt16 resptime;            //Card writes response time
            public UInt16 resv5;               //Reserved field
            public UInt16 datacount;           //Card writes data count
            public UInt16 extflag;             //Driver writes external flags
            public UInt32 timetag_hitcount; //Card writes time tag - //Card writes hit count
            public UInt32 elapsetime_mintime_maxtime; //Card writes elapsed time - //Card writes min time - //Card writes max time
            public UInt32 preaddr;             //Driver writes pre-proc address
            public UInt32 postaddr;            //Driver writes post-proc address
            public UInt32 timetagh;            //Card writes extended timetag
            public UInt16 resv18;              //Reserved field
            public UInt16 resv19;              //Reserved field
            public UInt16 cwd;                 //Card/User writes command word (R/T)
            public UInt16 cwdinfo;    //Card/User writes command word info
            public UInt16 swd;                 //Card writes status word
            public UInt16 swdinfo;       //Card writes status word info
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public UInt16[] data;	//User/Card writes data words (R/T)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public UInt16[] extra;	//User/Card writes extra words (R/T)
        }

        /**
        *
        *  BTIEBR Driver functions.
        *
        **/

        /**
        *
        *  Internal functions used by the BTIEBR Driver.
        *
        **/

        private static class BTIEBR_32
        {
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCConfig@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCConfig(UInt32 configval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCConfigEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCConfigEx(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCConfigMsg@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCConfigMsg(UInt32 configval, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCCreateList@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCCreateList(UInt32 listconfigval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCCreateMsg@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCCreateMsg(UInt32 configval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCPause@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_BCPause(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCPauseCheck@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCPauseCheck(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCResume@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_BCResume(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedAgain@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedAgain(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedBranch@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedBranch(UInt32 condition, Int32 destindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedBranchUpdate@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedBranchUpdate(Int32 destindex, Int32 opcodeindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedBuild@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedBuild(UInt16 nummsgs, UInt32[] msgaddr, UInt16[] freq, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedCall@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedCall(UInt32 condition, Int32 destindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedCallUpdate@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedCallUpdate(Int32 destindex, Int32 opcodeindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedEntry@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedEntry(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedFrame@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedFrame(UInt32 timeval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedFrameEnd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedFrameEnd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedFrameStart@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedFrameStart(UInt32 timeval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedGap@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedGap(UInt16 gapval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedHalt@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedHalt(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedLog@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedLog(UInt32 condition, UInt16 tagval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedMsg@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedMsg(UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedPause@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedPause(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedPulse@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedPulse(Int32 dionum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedPulse0@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedPulse0(Int32 dionum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedPulse1@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedPulse1(Int32 dionum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedRestart@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedRestart(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedRetry@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedRetry(UInt32 condition, UInt16 retries, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedReturn@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedReturn(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSetDefaultGap@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSetDefaultGap(Int32 gapval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSetTimeout@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSetTimeout(UInt16 timeoutval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSyncDefine@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCTransmitMsg@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsg(ref XMITFIELDSEBR xmitfieldsval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCTriggerDefine@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChGetInfo@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ChGetInfo(UInt16 infotype, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChIsBM@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsBM(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChIsEBR@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsEBR(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChIsM@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsM(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChIsS@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsS(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChStart@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChStart(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChStop@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChStop(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdMaxLoopRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdMaxLoopRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdMaxLoopWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdMaxLoopWr(UInt16 countval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdShotRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdShotRd(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdShotWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdShotWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdSkipRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdSkipRd(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdSkipWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdSkipWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdStepRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdStepRd(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdStepWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdStepWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorCtrl@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorCtrl(UInt32 ctrlval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorDefine@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorDefine(UInt32 defineval, UInt16 errvalue, Int32 countval, UInt16 wordpos, UInt16 bitpos, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorSent@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorSent(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorTagBC@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorTagBC(Int32 tagval, UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorTagRT@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorTagRT(Int32 tagval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorTriggerDefine@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListBlockRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListBlockRd(UInt16[] buf, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListBlockWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListBlockWr(UInt16[] buf, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListDataRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListDataRd(UInt16[] buf, Int32 count, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListDataWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListDataWr(UInt16[] buf, Int32 count, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListMultiBlockRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListMultiBlockRd(UInt16[] buf, ref Int32 blkcountptr, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListMultiBlockWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListMultiBlockWr(UInt16[] buf, Int32 blkcount, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MonConfig@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonConfig(UInt32 configval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MonFilterRTLink@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonFilterRTLink(UInt32 rtlinkmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MonFilterSA@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonFilterSA(Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgBlockRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgBlockRd(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgBlockWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgBlockWr(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgDataRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgDataRd(UInt16[] buf, Int32 count, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgDataWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgDataWr(UInt16[] buf, Int32 count, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgFieldRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgFieldRd(UInt16 fieldtype, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgFieldWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgFieldWr(UInt32 fieldval, UInt16 fieldtype, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgGroupRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgGroupRd(Int32 nummsgs, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] MSGFIELDSEBR[] msgflds, UInt32[] msgaddrptr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgGroupWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgGroupWr(Int32 nummsgs, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] MSGFIELDSEBR[] msgflds, UInt32[] msgaddrptr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgSkipRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MsgSkipRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgSkipWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgSkipWr(Int32 skip, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgSyncDefine@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MsgSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgTriggerDefine@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MsgTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_PlayConfig@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayConfig(UInt32 configval, UInt32 rtlinkmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_PlayStatus@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayStatus(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_PlayWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_PlayWr(UInt16[] buf, UInt16 bufcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTConfig@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTConfig(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTCreateList@36", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTCreateList(UInt32 listconfigval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTCreateMsg@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTCreateMsg(UInt32 configval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTGetMsg@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTGetMsg(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTReset@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTReset(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTResponseTimeSet@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTResponseTimeSet(Int32 resptime, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTSetMode@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTSetMode(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTSWDRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_RTSWDRd(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTSWDWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTSWDWr(UInt16 swdval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTSyncDefine@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ValPackCWD@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_ValPackCWD(Int32 taval, Int32 trflag, Int32 saval, Int32 wcval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ValUnpackCWD@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ValUnpackCWD(UInt16 cwdval, ref Int32 taval, ref Int32 trflag, ref Int32 saval, ref Int32 wcval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCCreateListEx@32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCCreateListEx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCCreateListExx@32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCCreateListExx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCGetMsg@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCGetMsg(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCIMGapRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCIMGapRd(ref UInt16 gapval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCIMGapWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCIMGapWr(UInt16 gapval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedBranchEx@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedBranchEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, Int32 destindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedCallEx@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedCallEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, Int32 destindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedLogEx@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedLogEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, UInt16 tagval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedNop@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedNop(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedRetryEx@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedRetryEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 retryflag, UInt16 retries, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSchedUser@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedUser(UInt32 useraddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCSetEntry@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSetEntry(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCTransmitMsgBlkEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsgBlkEx(Int32 writeflag, Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCTransmitMsgBlkInit@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsgBlkInit(Int32 maxcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCTransmitMsgBlkRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsgBlkRd(Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BCTransmitMsgBlkWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsgBlkWr(Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkAddr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_BlkAddr(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkOptRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_BlkOptRd(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkOptWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkOptWr(UInt16 rtopt, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkPtrRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BlkPtrRd(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkPtrWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_BlkPtrWr(UInt32 descaddr, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkRd(UInt16[] block, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkReset@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkReset(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkSWDRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_BlkSWDRd(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkSWDWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkSWDWr(UInt16 swdval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkWipe@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkWipe(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_BlkWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkWr(UInt16[] block, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChConfig@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChConfig(Int32 enableflag, Int32 typeval, UInt16 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ChIsRunning@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsRunning(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdAddr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_CmdAddr(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdAlloc@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_CmdAlloc(Int32 count, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdAllocEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_CmdAllocEx(UInt32 configval, Int32 count, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdBaseRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_CmdBaseRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdBaseWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdBaseWr(UInt32 addrval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdClear(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdCountRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdCountRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdCountWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdCountWr(UInt16 countval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdCtrlRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdCtrlRd(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdCtrlWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdCtrlWr(UInt16 ctrlval, Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdCurrRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdCurrRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdCurrWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdCurrWr(UInt16 addrval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdInit@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdInit(UInt16 opcode, UInt16[] block);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdInsert@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdInsert(UInt16[] block, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdRd(UInt16[] block, UInt32 addrval, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdRestart@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdRestart(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdStackClr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdStackClr(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdStartRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdStartRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdStartWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdStartWr(UInt16 addrval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdTotalRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdTotalRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdTotalWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdTotalWr(Int32 countval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_CmdWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdWr(UInt16[] block, UInt32 addrval, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescAddr@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescAddr(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescAlloc@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescAlloc(IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescConfig@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_DescConfig(UInt32 configval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescConfigAll@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_DescConfigAll(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescOptRd@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_DescOptRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescOptWr@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_DescOptWr(UInt16 descopt, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescPtrRd@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescPtrRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescPtrWr@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescPtrWr(UInt32 msgaddr, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescRd@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, UInt16[] desc, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DescWr@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescWr(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, UInt16[] desc, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_DllUsageCount@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_DllUsageCount();
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorIsTaggedBC@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorIsTaggedBC(UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorIsTaggedRT@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorIsTaggedRT(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorResponse@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorResponse(UInt16 responseval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorSync@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ErrorSync(Int32 value, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ErrorTimeout@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorTimeout(UInt16 timeoutval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_HostComm@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_HostComm(IntPtr lpparam, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_IsChan@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_IsChan(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_LHubConfig@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubConfig(UInt32 configval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_LHubPortExists@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortExists(Int32 portnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_LHubPortGetCount@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortGetCount(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_LHubPortGetRTLink@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortGetRTLink(Int32 portnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_LHubPortIsAvail@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortIsAvail(Int32 portnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_LHubPortIsAvailMask@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_LHubPortIsAvailMask(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_LHubPortSetRTLink@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortSetRTLink(Int32 rtlink, Int32 portnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_LHubRTLinkGetPort@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubRTLinkGetPort(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListAddr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListAddr(Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListAddrEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListAddrEx(Int32 index, UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListBlockRdEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListBlockRdEx(UInt16[] buf, Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListBlockWrEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListBlockWrEx(UInt16[] buf, Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListClear(UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListConfig@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListConfig(UInt32 configval, Int32 count, UInt16 skipval, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListConfigEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListConfigEx(UInt32 configval, Int32 count, UInt16 skipval, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListDataRdEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListDataRdEx(UInt16[] buf, Int32 count, Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListDataWrEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListDataWrEx(UInt16[] buf, Int32 count, Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListInit@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListInit(UInt32 listaddr, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListNextRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListNextRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListNextWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListNextWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListPostRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ListPostRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListPostWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ListPostWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListPreRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ListPreRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListPreWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ListPreWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ListStatus@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListStatus(UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsBC@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsBC(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsMon@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsMon(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsNotBC@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotBC(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsNotMon@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotMon(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsNotPlay@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotPlay(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsNotRT@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotRT(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsNotSerial@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotSerial(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsPlay@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsPlay(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsRT@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsRT(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ModeIsSerial@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsSerial(UInt16 chanflag);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MonConfigEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonConfigEx(UInt32 configval, UInt32 moncount, UInt16 cardnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MonFilterSAEx@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonFilterSAEx(Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgCheck@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgCheck(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgCommRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgCommRd(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgCommWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgCommWr(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgConfig@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgConfig(UInt32 configval, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgConfigEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgConfigEx(UInt32 configval, UInt16 cwd, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgCWDLinkRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgCWDLinkRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgCWDLinkWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgCWDLinkWr(UInt16 cwdlink, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgCWDRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgCWDRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgCWDWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgCWDWr(UInt16 cwd, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgErrorRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgErrorRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgErrorWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgErrorWr(UInt16 value, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgFlagRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgFlagRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgFlagWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgFlagWr(UInt16 msgflag, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgIsAccessed@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MsgIsAccessed(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgOptRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgOptRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgOptWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgOptWr(UInt16 msgopt, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgReset@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgReset(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgResetEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgResetEx(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgSWDLinkRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgSWDLinkRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_MsgSWDRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgSWDRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_PlayClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayClear(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_PlayConfigEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayConfigEx(UInt32 configval, UInt32 playcount, UInt32 rtlinkmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_PlayCounterWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_PlayCounterWr(UInt32 totalcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_PlayStatusEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayStatusEx(ref UInt32 totalcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_PlayWrEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_PlayWrEx(UInt16[] buf, UInt16 bufcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTConfigEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTConfigEx(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTConfigExx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTConfigExx(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTCreateListEx@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTCreateListEx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTCreateListExx@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTCreateListExx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTResponseTimeRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTResponseTimeRd(ref UInt16 timeval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTResponseTimeWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTResponseTimeWr(UInt16 timeval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_RTStatus@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_RTStatus(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SABlkWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SABlkWr(Int32 rtlink, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SABlkWrEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SABlkWrEx(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedBuild@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedBuild(Int32 nummsgs, UInt32[] msgaddr, Int32[] freq, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedBuildEx@32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedBuildEx(Int32 nummsgs, UInt32[] msgaddr, Int32[] freq, Int32[] period, IntPtr[] buf, UInt32 bufsize, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedBuildExx@32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedBuildExx(UInt32 frametime, Int32 nummsgs, UInt32[] msgaddr, Int32[] period, IntPtr[] buf, UInt32 bufsize, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedCalc@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedCalc(ref UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedDisplay@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_SchedDisplay(UInt32 nummsgs, ref UInt32 count);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedHook@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_SchedHook(Int32 index, IntPtr ptr);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedInsEnd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedInsEnd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedInsMsg@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedInsMsg(UInt32 index, ref UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedInsStart@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedInsStart(UInt32 frametime, UInt32 count, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedMaxval@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_SchedMaxval(UInt32[] arrayval, UInt32 arrayvalcount);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SchedMinval@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_SchedMinval(UInt32[] arrayval, UInt32 arrayvalcount);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SerialClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SerialClear(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SerialConfig@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SerialConfig(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SerialConfigEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SerialConfigEx(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SerialStatus@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SerialStatus(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_SerialWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_SerialWr(UInt16[] buf, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_TestProtocol@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_TestProtocol(IntPtr handleval);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ValPackRTCWD@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_ValPackRTCWD(Int32 mcflag, Int32 taval, Int32 trflag, Int32 saval, Int32 count);
            [DllImport("BTIEBR.DLL", EntryPoint = "_BTIEBR_ValUnpackRTCWD@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ValUnpackRTCWD(UInt16 cwd, ref Int32 bcastflag, ref Int32 mcflag, ref Int32 taval, ref Int32 trflag, ref Int32 saval, ref Int32 count);
        }

        private static class BTIEBR_64
        {
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCConfig(UInt32 configval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCConfigEx(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCConfigMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCConfigMsg(UInt32 configval, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCCreateList", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCCreateList(UInt32 listconfigval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCCreateMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCCreateMsg(UInt32 configval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCPause", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_BCPause(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCPauseCheck", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCPauseCheck(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCResume", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_BCResume(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedAgain", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedAgain(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedBranch", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedBranch(UInt32 condition, Int32 destindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedBranchUpdate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedBranchUpdate(Int32 destindex, Int32 opcodeindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedBuild", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedBuild(UInt16 nummsgs, UInt32[] msgaddr, UInt16[] freq, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedCall", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedCall(UInt32 condition, Int32 destindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedCallUpdate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedCallUpdate(Int32 destindex, Int32 opcodeindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedEntry", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedEntry(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedFrame", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedFrame(UInt32 timeval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedFrameEnd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedFrameEnd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedFrameStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedFrameStart(UInt32 timeval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedGap", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedGap(UInt16 gapval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedHalt", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedHalt(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedLog", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedLog(UInt32 condition, UInt16 tagval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedMsg(UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedPause", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedPause(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedPulse", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedPulse(Int32 dionum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedPulse0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedPulse0(Int32 dionum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedPulse1", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedPulse1(Int32 dionum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedRestart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedRestart(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedRetry", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedRetry(UInt32 condition, UInt16 retries, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedReturn", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedReturn(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSetDefaultGap", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSetDefaultGap(Int32 gapval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSetTimeout", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSetTimeout(UInt16 timeoutval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSyncDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCTransmitMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsg(ref XMITFIELDSEBR xmitfieldsval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCTriggerDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChGetInfo", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ChGetInfo(UInt16 infotype, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChIsBM", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsBM(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChIsEBR", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsEBR(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChIsM", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsM(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChIsS", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsS(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChStart(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChStop", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChStop(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdMaxLoopRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdMaxLoopRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdMaxLoopWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdMaxLoopWr(UInt16 countval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdShotRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdShotRd(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdShotWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdShotWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdSkipRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdSkipRd(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdSkipWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdSkipWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdStepRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdStepRd(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdStepWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdStepWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorCtrl", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorCtrl(UInt32 ctrlval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorDefine(UInt32 defineval, UInt16 errvalue, Int32 countval, UInt16 wordpos, UInt16 bitpos, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorSent", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorSent(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorTagBC", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorTagBC(Int32 tagval, UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorTagRT", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorTagRT(Int32 tagval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorTriggerDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListBlockRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListBlockRd(UInt16[] buf, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListBlockWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListBlockWr(UInt16[] buf, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListDataRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListDataRd(UInt16[] buf, Int32 count, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListDataWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListDataWr(UInt16[] buf, Int32 count, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListMultiBlockRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListMultiBlockRd(UInt16[] buf, ref Int32 blkcountptr, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListMultiBlockWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListMultiBlockWr(UInt16[] buf, Int32 blkcount, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MonConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonConfig(UInt32 configval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MonFilterRTLink", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonFilterRTLink(UInt32 rtlinkmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MonFilterSA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonFilterSA(Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgBlockRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgBlockRd(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgBlockWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgBlockWr(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgDataRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgDataRd(UInt16[] buf, Int32 count, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgDataWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgDataWr(UInt16[] buf, Int32 count, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgFieldRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgFieldRd(UInt16 fieldtype, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgFieldWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgFieldWr(UInt32 fieldval, UInt16 fieldtype, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgGroupRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgGroupRd(Int32 nummsgs, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] MSGFIELDSEBR[] msgflds, UInt32[] msgaddrptr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgGroupWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgGroupWr(Int32 nummsgs, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] MSGFIELDSEBR[] msgflds, UInt32[] msgaddrptr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgSkipRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MsgSkipRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgSkipWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgSkipWr(Int32 skip, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgSyncDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MsgSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgTriggerDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MsgTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_PlayConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayConfig(UInt32 configval, UInt32 rtlinkmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_PlayStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayStatus(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_PlayWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_PlayWr(UInt16[] buf, UInt16 bufcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTConfig(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTCreateList", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTCreateList(UInt32 listconfigval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTCreateMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTCreateMsg(UInt32 configval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTGetMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTGetMsg(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTReset", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTReset(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTResponseTimeSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTResponseTimeSet(Int32 resptime, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTSetMode", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTSetMode(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTSWDRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_RTSWDRd(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTSWDWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTSWDWr(UInt16 swdval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTSyncDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ValPackCWD", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_ValPackCWD(Int32 taval, Int32 trflag, Int32 saval, Int32 wcval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ValUnpackCWD", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ValUnpackCWD(UInt16 cwdval, ref Int32 taval, ref Int32 trflag, ref Int32 saval, ref Int32 wcval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCCreateListEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCCreateListEx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCCreateListExx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCCreateListExx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCGetMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BCGetMsg(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCIMGapRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCIMGapRd(ref UInt16 gapval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCIMGapWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCIMGapWr(UInt16 gapval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedBranchEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedBranchEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, Int32 destindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedCallEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedCallEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, Int32 destindex, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedLogEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedLogEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, UInt16 tagval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedNop", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedNop(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedRetryEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedRetryEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 retryflag, UInt16 retries, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSchedUser", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSchedUser(UInt32 useraddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCSetEntry", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCSetEntry(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCTransmitMsgBlkEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsgBlkEx(Int32 writeflag, Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCTransmitMsgBlkInit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsgBlkInit(Int32 maxcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCTransmitMsgBlkRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsgBlkRd(Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BCTransmitMsgBlkWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BCTransmitMsgBlkWr(Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkAddr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_BlkAddr(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkOptRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_BlkOptRd(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkOptWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkOptWr(UInt16 rtopt, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkPtrRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_BlkPtrRd(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkPtrWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_BlkPtrWr(UInt32 descaddr, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkRd(UInt16[] block, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkReset", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkReset(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkSWDRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_BlkSWDRd(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkSWDWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkSWDWr(UInt16 swdval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkWipe", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkWipe(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_BlkWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_BlkWr(UInt16[] block, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChConfig(Int32 enableflag, Int32 typeval, UInt16 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ChIsRunning", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ChIsRunning(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdAddr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_CmdAddr(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdAlloc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_CmdAlloc(Int32 count, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdAllocEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_CmdAllocEx(UInt32 configval, Int32 count, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdBaseRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_CmdBaseRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdBaseWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdBaseWr(UInt32 addrval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdClear(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdCountRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdCountRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdCountWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdCountWr(UInt16 countval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdCtrlRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdCtrlRd(Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdCtrlWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdCtrlWr(UInt16 ctrlval, Int32 index, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdCurrRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdCurrRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdCurrWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdCurrWr(UInt16 addrval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdInit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdInit(UInt16 opcode, UInt16[] block);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdInsert", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdInsert(UInt16[] block, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdRd(UInt16[] block, UInt32 addrval, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdRestart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdRestart(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdStackClr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdStackClr(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdStartRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_CmdStartRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdStartWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdStartWr(UInt16 addrval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdTotalRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdTotalRd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdTotalWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_CmdTotalWr(Int32 countval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_CmdWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_CmdWr(UInt16[] block, UInt32 addrval, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescAddr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescAddr(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescAlloc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescAlloc(IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_DescConfig(UInt32 configval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescConfigAll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_DescConfigAll(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescOptRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_DescOptRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescOptWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_DescOptWr(UInt16 descopt, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescPtrRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescPtrRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescPtrWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescPtrWr(UInt32 msgaddr, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, UInt16[] desc, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DescWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_DescWr(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, UInt16[] desc, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_DllUsageCount", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_DllUsageCount();
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorIsTaggedBC", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorIsTaggedBC(UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorIsTaggedRT", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorIsTaggedRT(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorResponse", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorResponse(UInt16 responseval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorSync", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ErrorSync(Int32 value, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ErrorTimeout", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ErrorTimeout(UInt16 timeoutval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_HostComm", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_HostComm(IntPtr lpparam, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_IsChan", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_IsChan(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_LHubConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubConfig(UInt32 configval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_LHubPortExists", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortExists(Int32 portnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_LHubPortGetCount", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortGetCount(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_LHubPortGetRTLink", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortGetRTLink(Int32 portnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_LHubPortIsAvail", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortIsAvail(Int32 portnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_LHubPortIsAvailMask", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_LHubPortIsAvailMask(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_LHubPortSetRTLink", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubPortSetRTLink(Int32 rtlink, Int32 portnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_LHubRTLinkGetPort", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_LHubRTLinkGetPort(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListAddr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListAddr(Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListAddrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListAddrEx(Int32 index, UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListBlockRdEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListBlockRdEx(UInt16[] buf, Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListBlockWrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListBlockWrEx(UInt16[] buf, Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListClear(UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListConfig(UInt32 configval, Int32 count, UInt16 skipval, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListConfigEx(UInt32 configval, Int32 count, UInt16 skipval, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListDataRdEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListDataRdEx(UInt16[] buf, Int32 count, Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListDataWrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListDataWrEx(UInt16[] buf, Int32 count, Int32 index, UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListInit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListInit(UInt32 listaddr, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListNextRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListNextRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListNextWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_ListNextWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListPostRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ListPostRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListPostWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ListPostWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListPreRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ListPreRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListPreWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ListPreWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ListStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ListStatus(UInt32 listaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsBC", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsBC(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsMon", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsMon(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsNotBC", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotBC(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsNotMon", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotMon(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsNotPlay", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotPlay(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsNotRT", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotRT(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsNotSerial", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsNotSerial(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsPlay", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsPlay(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsRT", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsRT(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ModeIsSerial", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_ModeIsSerial(UInt16 chanflag);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MonConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonConfigEx(UInt32 configval, UInt32 moncount, UInt16 cardnum, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MonFilterSAEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MonFilterSAEx(Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgCheck", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgCheck(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgCommRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgCommRd(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgCommWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgCommWr(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgConfig(UInt32 configval, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgConfigEx(UInt32 configval, UInt16 cwd, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgCWDLinkRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgCWDLinkRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgCWDLinkWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgCWDLinkWr(UInt16 cwdlink, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgCWDRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgCWDRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgCWDWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgCWDWr(UInt16 cwd, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgErrorRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgErrorRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgErrorWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgErrorWr(UInt16 value, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgFlagRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgFlagRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgFlagWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgFlagWr(UInt16 msgflag, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgIsAccessed", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_MsgIsAccessed(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgOptRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgOptRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgOptWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_MsgOptWr(UInt16 msgopt, UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgReset", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgReset(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgResetEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_MsgResetEx(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgSWDLinkRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgSWDLinkRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_MsgSWDRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_MsgSWDRd(UInt32 msgaddr, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_PlayClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayClear(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_PlayConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayConfigEx(UInt32 configval, UInt32 playcount, UInt32 rtlinkmask, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_PlayCounterWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_PlayCounterWr(UInt32 totalcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_PlayStatusEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_PlayStatusEx(ref UInt32 totalcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_PlayWrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_PlayWrEx(UInt16[] buf, UInt16 bufcount, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTConfigEx(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTConfigExx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTConfigExx(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTCreateListEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTCreateListEx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTCreateListExx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_RTCreateListExx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTResponseTimeRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTResponseTimeRd(ref UInt16 timeval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTResponseTimeWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_RTResponseTimeWr(UInt16 timeval, Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_RTStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_RTStatus(Int32 rtlink, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SABlkWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SABlkWr(Int32 rtlink, Int32 saval, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SABlkWrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SABlkWrEx(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedBuild", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedBuild(Int32 nummsgs, UInt32[] msgaddr, Int32[] freq, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedBuildEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedBuildEx(Int32 nummsgs, UInt32[] msgaddr, Int32[] freq, Int32[] period, IntPtr[] buf, UInt32 bufsize, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedBuildExx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedBuildExx(UInt32 frametime, Int32 nummsgs, UInt32[] msgaddr, Int32[] period, IntPtr[] buf, UInt32 bufsize, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedCalc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedCalc(ref UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedDisplay", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_SchedDisplay(UInt32 nummsgs, ref UInt32 count);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedHook", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_SchedHook(Int32 index, IntPtr ptr);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedInsEnd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedInsEnd(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedInsMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedInsMsg(UInt32 index, ref UInt32 msgaddr, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedInsStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SchedInsStart(UInt32 frametime, UInt32 count, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedMaxval", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_SchedMaxval(UInt32[] arrayval, UInt32 arrayvalcount);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SchedMinval", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_SchedMinval(UInt32[] arrayval, UInt32 arrayvalcount);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SerialClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SerialClear(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SerialConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SerialConfig(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SerialConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SerialConfigEx(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SerialStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_SerialStatus(Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_SerialWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt32 BTIEBR_SerialWr(UInt16[] buf, Int32 channum, IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_TestProtocol", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern Int32 BTIEBR_TestProtocol(IntPtr handleval);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ValPackRTCWD", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern UInt16 BTIEBR_ValPackRTCWD(Int32 mcflag, Int32 taval, Int32 trflag, Int32 saval, Int32 count);
            [DllImport("BTIEBR64.DLL", EntryPoint = "BTIEBR_ValUnpackRTCWD", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTIEBR_ValUnpackRTCWD(UInt16 cwd, ref Int32 bcastflag, ref Int32 mcflag, ref Int32 taval, ref Int32 trflag, ref Int32 saval, ref Int32 count);
        }

        public static Int32 BTIEBR_BCConfig(UInt32 configval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCConfig(configval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCConfig(configval, channum, handleval);
        }
        public static Int32 BTIEBR_BCConfigEx(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCConfigEx(configval, count, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCConfigEx(configval, count, channum, handleval);
        }
        public static UInt32 BTIEBR_BCConfigMsg(UInt32 configval, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCConfigMsg(configval, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_BCConfigMsg(configval, msgaddr, handleval);
        }
        public static UInt32 BTIEBR_BCCreateList(UInt32 listconfigval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCCreateList(listconfigval, count, msgconfigval, cwd, data, rtlink, handleval);
            else
                return BTIEBR_64.BTIEBR_BCCreateList(listconfigval, count, msgconfigval, cwd, data, rtlink, handleval);
        }
        public static UInt32 BTIEBR_BCCreateMsg(UInt32 configval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCCreateMsg(configval, cwd, data, rtlink, handleval);
            else
                return BTIEBR_64.BTIEBR_BCCreateMsg(configval, cwd, data, rtlink, handleval);
        }
        public static void BTIEBR_BCPause(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_BCPause(channum, handleval);
            else
                BTIEBR_64.BTIEBR_BCPause(channum, handleval);
        }
        public static Int32 BTIEBR_BCPauseCheck(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCPauseCheck(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCPauseCheck(channum, handleval);
        }
        public static void BTIEBR_BCResume(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_BCResume(channum, handleval);
            else
                BTIEBR_64.BTIEBR_BCResume(channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedAgain(Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedAgain(index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedAgain(index, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedBranch(UInt32 condition, Int32 destindex, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedBranch(condition, destindex, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedBranch(condition, destindex, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedBranchUpdate(Int32 destindex, Int32 opcodeindex, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedBranchUpdate(destindex, opcodeindex, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedBranchUpdate(destindex, opcodeindex, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedBuild(UInt16 nummsgs, UInt32[] msgaddr, UInt16[] freq, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedBuild(nummsgs, msgaddr, freq, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedBuild(nummsgs, msgaddr, freq, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedCall(UInt32 condition, Int32 destindex, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedCall(condition, destindex, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedCall(condition, destindex, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedCallUpdate(Int32 destindex, Int32 opcodeindex, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedCallUpdate(destindex, opcodeindex, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedCallUpdate(destindex, opcodeindex, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedEntry(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedEntry(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedEntry(channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedFrame(UInt32 timeval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedFrame(timeval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedFrame(timeval, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedFrameEnd(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedFrameEnd(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedFrameEnd(channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedFrameStart(UInt32 timeval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedFrameStart(timeval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedFrameStart(timeval, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedGap(UInt16 gapval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedGap(gapval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedGap(gapval, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedHalt(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedHalt(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedHalt(channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedLog(UInt32 condition, UInt16 tagval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedLog(condition, tagval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedLog(condition, tagval, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedMsg(UInt32 msgaddr, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedMsg(msgaddr, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedMsg(msgaddr, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedPause(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedPause(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedPause(channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedPulse(Int32 dionum, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedPulse(dionum, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedPulse(dionum, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedPulse0(Int32 dionum, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedPulse0(dionum, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedPulse0(dionum, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedPulse1(Int32 dionum, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedPulse1(dionum, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedPulse1(dionum, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedRestart(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedRestart(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedRestart(channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedRetry(UInt32 condition, UInt16 retries, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedRetry(condition, retries, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedRetry(condition, retries, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedReturn(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedReturn(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedReturn(channum, handleval);
        }
        public static Int32 BTIEBR_BCSetDefaultGap(Int32 gapval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSetDefaultGap(gapval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSetDefaultGap(gapval, channum, handleval);
        }
        public static Int32 BTIEBR_BCSetTimeout(UInt16 timeoutval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSetTimeout(timeoutval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSetTimeout(timeoutval, channum, handleval);
        }
        public static Int32 BTIEBR_BCSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSyncDefine(enableflag, syncmask, pinpolarity, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSyncDefine(enableflag, syncmask, pinpolarity, channum, handleval);
        }
        public static Int32 BTIEBR_BCTransmitMsg(ref XMITFIELDSEBR xmitfieldsval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCTransmitMsg(ref xmitfieldsval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCTransmitMsg(ref xmitfieldsval, channum, handleval);
        }
        public static Int32 BTIEBR_BCTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval);
        }
        public static UInt32 BTIEBR_ChGetInfo(UInt16 infotype, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChGetInfo(infotype, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChGetInfo(infotype, channum, handleval);
        }
        public static Int32 BTIEBR_ChIsBM(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChIsBM(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChIsBM(channum, handleval);
        }
        public static Int32 BTIEBR_ChIsEBR(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChIsEBR(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChIsEBR(channum, handleval);
        }
        public static Int32 BTIEBR_ChIsM(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChIsM(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChIsM(channum, handleval);
        }
        public static Int32 BTIEBR_ChIsS(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChIsS(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChIsS(channum, handleval);
        }
        public static Int32 BTIEBR_ChStart(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChStart(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChStart(channum, handleval);
        }
        public static Int32 BTIEBR_ChStop(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChStop(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChStop(channum, handleval);
        }
        public static UInt16 BTIEBR_CmdMaxLoopRd(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdMaxLoopRd(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdMaxLoopRd(channum, handleval);
        }
        public static void BTIEBR_CmdMaxLoopWr(UInt16 countval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdMaxLoopWr(countval, channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdMaxLoopWr(countval, channum, handleval);
        }
        public static Int32 BTIEBR_CmdShotRd(Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdShotRd(index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdShotRd(index, channum, handleval);
        }
        public static Int32 BTIEBR_CmdShotWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdShotWr(value, index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdShotWr(value, index, channum, handleval);
        }
        public static Int32 BTIEBR_CmdSkipRd(Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdSkipRd(index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdSkipRd(index, channum, handleval);
        }
        public static Int32 BTIEBR_CmdSkipWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdSkipWr(value, index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdSkipWr(value, index, channum, handleval);
        }
        public static Int32 BTIEBR_CmdStepRd(Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdStepRd(index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdStepRd(index, channum, handleval);
        }
        public static Int32 BTIEBR_CmdStepWr(Int32 value, Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdStepWr(value, index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdStepWr(value, index, channum, handleval);
        }
        public static Int32 BTIEBR_ErrorCtrl(UInt32 ctrlval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorCtrl(ctrlval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorCtrl(ctrlval, channum, handleval);
        }
        public static Int32 BTIEBR_ErrorDefine(UInt32 defineval, UInt16 errvalue, Int32 countval, UInt16 wordpos, UInt16 bitpos, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorDefine(defineval, errvalue, countval, wordpos, bitpos, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorDefine(defineval, errvalue, countval, wordpos, bitpos, channum, handleval);
        }
        public static Int32 BTIEBR_ErrorSent(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorSent(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorSent(channum, handleval);
        }
        public static Int32 BTIEBR_ErrorTagBC(Int32 tagval, UInt32 msgaddr, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorTagBC(tagval, msgaddr, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorTagBC(tagval, msgaddr, channum, handleval);
        }
        public static Int32 BTIEBR_ErrorTagRT(Int32 tagval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorTagRT(tagval, mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorTagRT(tagval, mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static Int32 BTIEBR_ErrorTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval);
        }
        public static UInt32 BTIEBR_ListBlockRd(UInt16[] buf, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListBlockRd(buf, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListBlockRd(buf, listaddr, handleval);
        }
        public static UInt32 BTIEBR_ListBlockWr(UInt16[] buf, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListBlockWr(buf, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListBlockWr(buf, listaddr, handleval);
        }
        public static Int32 BTIEBR_ListDataRd(UInt16[] buf, Int32 count, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListDataRd(buf, count, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListDataRd(buf, count, listaddr, handleval);
        }
        public static Int32 BTIEBR_ListDataWr(UInt16[] buf, Int32 count, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListDataWr(buf, count, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListDataWr(buf, count, listaddr, handleval);
        }
        public static Int32 BTIEBR_ListMultiBlockRd(UInt16[] buf, ref Int32 blkcountptr, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListMultiBlockRd(buf, ref blkcountptr, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListMultiBlockRd(buf, ref blkcountptr, listaddr, handleval);
        }
        public static Int32 BTIEBR_ListMultiBlockWr(UInt16[] buf, Int32 blkcount, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListMultiBlockWr(buf, blkcount, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListMultiBlockWr(buf, blkcount, listaddr, handleval);
        }
        public static Int32 BTIEBR_MonConfig(UInt32 configval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MonConfig(configval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_MonConfig(configval, channum, handleval);
        }
        public static Int32 BTIEBR_MonFilterRTLink(UInt32 rtlinkmask, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MonFilterRTLink(rtlinkmask, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_MonFilterRTLink(rtlinkmask, channum, handleval);
        }
        public static Int32 BTIEBR_MonFilterSA(Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MonFilterSA(rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_MonFilterSA(rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval);
        }
        public static UInt32 BTIEBR_MsgBlockRd(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgBlockRd(ref buf, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgBlockRd(ref buf, msgaddr, handleval);
        }
        public static UInt32 BTIEBR_MsgBlockWr(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgBlockWr(ref buf, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgBlockWr(ref buf, msgaddr, handleval);
        }
        public static void BTIEBR_MsgDataRd(UInt16[] buf, Int32 count, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgDataRd(buf, count, msgaddr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgDataRd(buf, count, msgaddr, handleval);
        }
        public static void BTIEBR_MsgDataWr(UInt16[] buf, Int32 count, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgDataWr(buf, count, msgaddr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgDataWr(buf, count, msgaddr, handleval);
        }
        public static UInt32 BTIEBR_MsgFieldRd(UInt16 fieldtype, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgFieldRd(fieldtype, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgFieldRd(fieldtype, msgaddr, handleval);
        }
        public static UInt32 BTIEBR_MsgFieldWr(UInt32 fieldval, UInt16 fieldtype, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgFieldWr(fieldval, fieldtype, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgFieldWr(fieldval, fieldtype, msgaddr, handleval);
        }
        public static void BTIEBR_MsgGroupRd(Int32 nummsgs, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] MSGFIELDSEBR[] msgflds, UInt32[] msgaddrptr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgGroupRd(nummsgs, msgflds, msgaddrptr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgGroupRd(nummsgs, msgflds, msgaddrptr, handleval);
        }
        public static void BTIEBR_MsgGroupWr(Int32 nummsgs, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] MSGFIELDSEBR[] msgflds, UInt32[] msgaddrptr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgGroupWr(nummsgs, msgflds, msgaddrptr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgGroupWr(nummsgs, msgflds, msgaddrptr, handleval);
        }
        public static Int32 BTIEBR_MsgSkipRd(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgSkipRd(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgSkipRd(msgaddr, handleval);
        }
        public static void BTIEBR_MsgSkipWr(Int32 skip, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgSkipWr(skip, msgaddr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgSkipWr(skip, msgaddr, handleval);
        }
        public static Int32 BTIEBR_MsgSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgSyncDefine(enableflag, syncmask, pinpolarity, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgSyncDefine(enableflag, syncmask, pinpolarity, msgaddr, handleval);
        }
        public static Int32 BTIEBR_MsgTriggerDefine(Int32 enableflag, UInt16 trigmask, UInt16 trigval, UInt16 pinpolarity, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgTriggerDefine(enableflag, trigmask, trigval, pinpolarity, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgTriggerDefine(enableflag, trigmask, trigval, pinpolarity, msgaddr, handleval);
        }
        public static Int32 BTIEBR_PlayConfig(UInt32 configval, UInt32 rtlinkmask, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_PlayConfig(configval, rtlinkmask, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_PlayConfig(configval, rtlinkmask, channum, handleval);
        }
        public static Int32 BTIEBR_PlayStatus(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_PlayStatus(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_PlayStatus(channum, handleval);
        }
        public static UInt16 BTIEBR_PlayWr(UInt16[] buf, UInt16 bufcount, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_PlayWr(buf, bufcount, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_PlayWr(buf, bufcount, channum, handleval);
        }
        public static Int32 BTIEBR_RTConfig(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTConfig(configval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTConfig(configval, rtlink, channum, handleval);
        }
        public static UInt32 BTIEBR_RTCreateList(UInt32 listconfigval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTCreateList(listconfigval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTCreateList(listconfigval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static UInt32 BTIEBR_RTCreateMsg(UInt32 configval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTCreateMsg(configval, mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTCreateMsg(configval, mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static UInt32 BTIEBR_RTGetMsg(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTGetMsg(mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTGetMsg(mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static Int32 BTIEBR_RTReset(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTReset(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTReset(rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_RTResponseTimeSet(Int32 resptime, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTResponseTimeSet(resptime, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTResponseTimeSet(resptime, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_RTSetMode(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTSetMode(configval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTSetMode(configval, rtlink, channum, handleval);
        }
        public static UInt16 BTIEBR_RTSWDRd(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTSWDRd(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTSWDRd(rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_RTSWDWr(UInt16 swdval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTSWDWr(swdval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTSWDWr(swdval, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_RTSyncDefine(Int32 enableflag, UInt16 syncmask, UInt16 pinpolarity, Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTSyncDefine(enableflag, syncmask, pinpolarity, rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTSyncDefine(enableflag, syncmask, pinpolarity, rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval);
        }
        public static UInt16 BTIEBR_ValPackCWD(Int32 taval, Int32 trflag, Int32 saval, Int32 wcval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ValPackCWD(taval, trflag, saval, wcval);
            else
                return BTIEBR_64.BTIEBR_ValPackCWD(taval, trflag, saval, wcval);
        }
        public static void BTIEBR_ValUnpackCWD(UInt16 cwdval, ref Int32 taval, ref Int32 trflag, ref Int32 saval, ref Int32 wcval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_ValUnpackCWD(cwdval, ref taval, ref trflag, ref saval, ref wcval);
            else
                BTIEBR_64.BTIEBR_ValUnpackCWD(cwdval, ref taval, ref trflag, ref saval, ref wcval);
        }
        public static UInt32 BTIEBR_BCCreateListEx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCCreateListEx(listconfigval, skipval, count, msgconfigval, cwd, data, rtlink, handleval);
            else
                return BTIEBR_64.BTIEBR_BCCreateListEx(listconfigval, skipval, count, msgconfigval, cwd, data, rtlink, handleval);
        }
        public static UInt32 BTIEBR_BCCreateListExx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, UInt16 cwd, UInt16[] data, Int32 rtlink, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCCreateListExx(listconfigval, skipval, count, msgconfigval, cwd, data, rtlink, handleval);
            else
                return BTIEBR_64.BTIEBR_BCCreateListExx(listconfigval, skipval, count, msgconfigval, cwd, data, rtlink, handleval);
        }
        public static UInt32 BTIEBR_BCGetMsg(Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCGetMsg(index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCGetMsg(index, channum, handleval);
        }
        public static Int32 BTIEBR_BCIMGapRd(ref UInt16 gapval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCIMGapRd(ref gapval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCIMGapRd(ref gapval, channum, handleval);
        }
        public static Int32 BTIEBR_BCIMGapWr(UInt16 gapval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCIMGapWr(gapval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCIMGapWr(gapval, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedBranchEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, Int32 destindex, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedBranchEx(condition1, condition2, condition3, opcode, destindex, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedBranchEx(condition1, condition2, condition3, opcode, destindex, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedCallEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, Int32 destindex, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedCallEx(condition1, condition2, condition3, opcode, destindex, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedCallEx(condition1, condition2, condition3, opcode, destindex, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedLogEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 opcode, UInt16 tagval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedLogEx(condition1, condition2, condition3, opcode, tagval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedLogEx(condition1, condition2, condition3, opcode, tagval, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedNop(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedNop(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedNop(channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedRetryEx(UInt16 condition1, UInt16 condition2, UInt16 condition3, UInt16 retryflag, UInt16 retries, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedRetryEx(condition1, condition2, condition3, retryflag, retries, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedRetryEx(condition1, condition2, condition3, retryflag, retries, channum, handleval);
        }
        public static Int32 BTIEBR_BCSchedUser(UInt32 useraddr, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSchedUser(useraddr, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSchedUser(useraddr, channum, handleval);
        }
        public static Int32 BTIEBR_BCSetEntry(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCSetEntry(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCSetEntry(channum, handleval);
        }
        public static Int32 BTIEBR_BCTransmitMsgBlkEx(Int32 writeflag, Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCTransmitMsgBlkEx(writeflag, count, xmitfieldsval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCTransmitMsgBlkEx(writeflag, count, xmitfieldsval, channum, handleval);
        }
        public static Int32 BTIEBR_BCTransmitMsgBlkInit(Int32 maxcount, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCTransmitMsgBlkInit(maxcount, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCTransmitMsgBlkInit(maxcount, channum, handleval);
        }
        public static Int32 BTIEBR_BCTransmitMsgBlkRd(Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCTransmitMsgBlkRd(count, xmitfieldsval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCTransmitMsgBlkRd(count, xmitfieldsval, channum, handleval);
        }
        public static Int32 BTIEBR_BCTransmitMsgBlkWr(Int32 count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] XMITFIELDSEBR[] xmitfieldsval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BCTransmitMsgBlkWr(count, xmitfieldsval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BCTransmitMsgBlkWr(count, xmitfieldsval, channum, handleval);
        }
        public static UInt16 BTIEBR_BlkAddr(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkAddr(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkAddr(rtlink, channum, handleval);
        }
        public static UInt16 BTIEBR_BlkOptRd(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkOptRd(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkOptRd(rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_BlkOptWr(UInt16 rtopt, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkOptWr(rtopt, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkOptWr(rtopt, rtlink, channum, handleval);
        }
        public static UInt32 BTIEBR_BlkPtrRd(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkPtrRd(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkPtrRd(rtlink, channum, handleval);
        }
        public static void BTIEBR_BlkPtrWr(UInt32 descaddr, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_BlkPtrWr(descaddr, rtlink, channum, handleval);
            else
                BTIEBR_64.BTIEBR_BlkPtrWr(descaddr, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_BlkRd(UInt16[] block, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkRd(block, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkRd(block, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_BlkReset(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkReset(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkReset(rtlink, channum, handleval);
        }
        public static UInt16 BTIEBR_BlkSWDRd(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkSWDRd(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkSWDRd(rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_BlkSWDWr(UInt16 swdval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkSWDWr(swdval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkSWDWr(swdval, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_BlkWipe(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkWipe(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkWipe(channum, handleval);
        }
        public static Int32 BTIEBR_BlkWr(UInt16[] block, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_BlkWr(block, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_BlkWr(block, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_ChConfig(Int32 enableflag, Int32 typeval, UInt16 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChConfig(enableflag, typeval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChConfig(enableflag, typeval, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_ChIsRunning(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ChIsRunning(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ChIsRunning(channum, handleval);
        }
        public static UInt32 BTIEBR_CmdAddr(Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdAddr(index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdAddr(index, channum, handleval);
        }
        public static UInt32 BTIEBR_CmdAlloc(Int32 count, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdAlloc(count, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdAlloc(count, handleval);
        }
        public static UInt32 BTIEBR_CmdAllocEx(UInt32 configval, Int32 count, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdAllocEx(configval, count, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdAllocEx(configval, count, handleval);
        }
        public static UInt32 BTIEBR_CmdBaseRd(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdBaseRd(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdBaseRd(channum, handleval);
        }
        public static void BTIEBR_CmdBaseWr(UInt32 addrval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdBaseWr(addrval, channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdBaseWr(addrval, channum, handleval);
        }
        public static Int32 BTIEBR_CmdClear(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdClear(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdClear(channum, handleval);
        }
        public static UInt16 BTIEBR_CmdCountRd(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdCountRd(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdCountRd(channum, handleval);
        }
        public static void BTIEBR_CmdCountWr(UInt16 countval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdCountWr(countval, channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdCountWr(countval, channum, handleval);
        }
        public static UInt16 BTIEBR_CmdCtrlRd(Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdCtrlRd(index, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdCtrlRd(index, channum, handleval);
        }
        public static void BTIEBR_CmdCtrlWr(UInt16 ctrlval, Int32 index, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdCtrlWr(ctrlval, index, channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdCtrlWr(ctrlval, index, channum, handleval);
        }
        public static UInt16 BTIEBR_CmdCurrRd(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdCurrRd(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdCurrRd(channum, handleval);
        }
        public static void BTIEBR_CmdCurrWr(UInt16 addrval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdCurrWr(addrval, channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdCurrWr(addrval, channum, handleval);
        }
        public static void BTIEBR_CmdInit(UInt16 opcode, UInt16[] block)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdInit(opcode, block);
            else
                BTIEBR_64.BTIEBR_CmdInit(opcode, block);
        }
        public static Int32 BTIEBR_CmdInsert(UInt16[] block, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdInsert(block, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdInsert(block, channum, handleval);
        }
        public static Int32 BTIEBR_CmdRd(UInt16[] block, UInt32 addrval, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdRd(block, addrval, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdRd(block, addrval, handleval);
        }
        public static void BTIEBR_CmdRestart(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdRestart(channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdRestart(channum, handleval);
        }
        public static void BTIEBR_CmdStackClr(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdStackClr(channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdStackClr(channum, handleval);
        }
        public static UInt16 BTIEBR_CmdStartRd(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdStartRd(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdStartRd(channum, handleval);
        }
        public static void BTIEBR_CmdStartWr(UInt16 addrval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdStartWr(addrval, channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdStartWr(addrval, channum, handleval);
        }
        public static Int32 BTIEBR_CmdTotalRd(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdTotalRd(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdTotalRd(channum, handleval);
        }
        public static void BTIEBR_CmdTotalWr(Int32 countval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_CmdTotalWr(countval, channum, handleval);
            else
                BTIEBR_64.BTIEBR_CmdTotalWr(countval, channum, handleval);
        }
        public static Int32 BTIEBR_CmdWr(UInt16[] block, UInt32 addrval, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_CmdWr(block, addrval, handleval);
            else
                return BTIEBR_64.BTIEBR_CmdWr(block, addrval, handleval);
        }
        public static UInt32 BTIEBR_DescAddr(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescAddr(mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_DescAddr(mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static UInt32 BTIEBR_DescAlloc(IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescAlloc(handleval);
            else
                return BTIEBR_64.BTIEBR_DescAlloc(handleval);
        }
        public static Int32 BTIEBR_DescConfig(UInt32 configval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescConfig(configval, mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_DescConfig(configval, mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static Int32 BTIEBR_DescConfigAll(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescConfigAll(configval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_DescConfigAll(configval, rtlink, channum, handleval);
        }
        public static UInt16 BTIEBR_DescOptRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescOptRd(mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_DescOptRd(mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static void BTIEBR_DescOptWr(UInt16 descopt, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_DescOptWr(descopt, mcflag, rtlink, trflag, saval, channum, handleval);
            else
                BTIEBR_64.BTIEBR_DescOptWr(descopt, mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static UInt32 BTIEBR_DescPtrRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescPtrRd(mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_DescPtrRd(mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static UInt32 BTIEBR_DescPtrWr(UInt32 msgaddr, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescPtrWr(msgaddr, mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_DescPtrWr(msgaddr, mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static UInt32 BTIEBR_DescRd(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, UInt16[] desc, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescRd(mcflag, rtlink, trflag, saval, desc, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_DescRd(mcflag, rtlink, trflag, saval, desc, channum, handleval);
        }
        public static UInt32 BTIEBR_DescWr(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, UInt16[] desc, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DescWr(mcflag, rtlink, trflag, saval, desc, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_DescWr(mcflag, rtlink, trflag, saval, desc, channum, handleval);
        }
        public static Int32 BTIEBR_DllUsageCount()
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_DllUsageCount();
            else
                return BTIEBR_64.BTIEBR_DllUsageCount();
        }
        public static Int32 BTIEBR_ErrorIsTaggedBC(UInt32 msgaddr, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorIsTaggedBC(msgaddr, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorIsTaggedBC(msgaddr, channum, handleval);
        }
        public static Int32 BTIEBR_ErrorIsTaggedRT(Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorIsTaggedRT(mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorIsTaggedRT(mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static Int32 BTIEBR_ErrorResponse(UInt16 responseval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorResponse(responseval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorResponse(responseval, channum, handleval);
        }
        public static void BTIEBR_ErrorSync(Int32 value, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_ErrorSync(value, channum, handleval);
            else
                BTIEBR_64.BTIEBR_ErrorSync(value, channum, handleval);
        }
        public static Int32 BTIEBR_ErrorTimeout(UInt16 timeoutval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ErrorTimeout(timeoutval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_ErrorTimeout(timeoutval, channum, handleval);
        }
        public static Int32 BTIEBR_HostComm(IntPtr lpparam, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_HostComm(lpparam, handleval);
            else
                return BTIEBR_64.BTIEBR_HostComm(lpparam, handleval);
        }
        public static Int32 BTIEBR_IsChan(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_IsChan(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_IsChan(channum, handleval);
        }
        public static Int32 BTIEBR_LHubConfig(UInt32 configval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_LHubConfig(configval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_LHubConfig(configval, channum, handleval);
        }
        public static Int32 BTIEBR_LHubPortExists(Int32 portnum, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_LHubPortExists(portnum, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_LHubPortExists(portnum, channum, handleval);
        }
        public static Int32 BTIEBR_LHubPortGetCount(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_LHubPortGetCount(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_LHubPortGetCount(channum, handleval);
        }
        public static Int32 BTIEBR_LHubPortGetRTLink(Int32 portnum, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_LHubPortGetRTLink(portnum, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_LHubPortGetRTLink(portnum, channum, handleval);
        }
        public static Int32 BTIEBR_LHubPortIsAvail(Int32 portnum, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_LHubPortIsAvail(portnum, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_LHubPortIsAvail(portnum, channum, handleval);
        }
        public static UInt32 BTIEBR_LHubPortIsAvailMask(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_LHubPortIsAvailMask(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_LHubPortIsAvailMask(channum, handleval);
        }
        public static Int32 BTIEBR_LHubPortSetRTLink(Int32 rtlink, Int32 portnum, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_LHubPortSetRTLink(rtlink, portnum, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_LHubPortSetRTLink(rtlink, portnum, channum, handleval);
        }
        public static Int32 BTIEBR_LHubRTLinkGetPort(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_LHubRTLinkGetPort(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_LHubRTLinkGetPort(rtlink, channum, handleval);
        }
        public static UInt32 BTIEBR_ListAddr(Int32 index, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListAddr(index, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListAddr(index, listaddr, handleval);
        }
        public static UInt32 BTIEBR_ListAddrEx(Int32 index, UInt32 listaddr, ref UInt16 header, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListAddrEx(index, listaddr, ref header, handleval);
            else
                return BTIEBR_64.BTIEBR_ListAddrEx(index, listaddr, ref header, handleval);
        }
        public static UInt32 BTIEBR_ListBlockRdEx(UInt16[] buf, Int32 index, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListBlockRdEx(buf, index, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListBlockRdEx(buf, index, listaddr, handleval);
        }
        public static UInt32 BTIEBR_ListBlockWrEx(UInt16[] buf, Int32 index, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListBlockWrEx(buf, index, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListBlockWrEx(buf, index, listaddr, handleval);
        }
        public static Int32 BTIEBR_ListClear(UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListClear(listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListClear(listaddr, handleval);
        }
        public static UInt32 BTIEBR_ListConfig(UInt32 configval, Int32 count, UInt16 skipval, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListConfig(configval, count, skipval, handleval);
            else
                return BTIEBR_64.BTIEBR_ListConfig(configval, count, skipval, handleval);
        }
        public static UInt32 BTIEBR_ListConfigEx(UInt32 configval, Int32 count, UInt16 skipval, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListConfigEx(configval, count, skipval, handleval);
            else
                return BTIEBR_64.BTIEBR_ListConfigEx(configval, count, skipval, handleval);
        }
        public static Int32 BTIEBR_ListDataRdEx(UInt16[] buf, Int32 count, Int32 index, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListDataRdEx(buf, count, index, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListDataRdEx(buf, count, index, listaddr, handleval);
        }
        public static Int32 BTIEBR_ListDataWrEx(UInt16[] buf, Int32 count, Int32 index, UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListDataWrEx(buf, count, index, listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListDataWrEx(buf, count, index, listaddr, handleval);
        }
        public static Int32 BTIEBR_ListInit(UInt32 listaddr, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListInit(listaddr, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListInit(listaddr, msgaddr, handleval);
        }
        public static UInt32 BTIEBR_ListNextRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListNextRd(listaddr, ref header, handleval);
            else
                return BTIEBR_64.BTIEBR_ListNextRd(listaddr, ref header, handleval);
        }
        public static UInt32 BTIEBR_ListNextWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListNextWr(listaddr, ref header, handleval);
            else
                return BTIEBR_64.BTIEBR_ListNextWr(listaddr, ref header, handleval);
        }
        public static void BTIEBR_ListPostRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_ListPostRd(listaddr, ref header, handleval);
            else
                BTIEBR_64.BTIEBR_ListPostRd(listaddr, ref header, handleval);
        }
        public static void BTIEBR_ListPostWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_ListPostWr(listaddr, ref header, handleval);
            else
                BTIEBR_64.BTIEBR_ListPostWr(listaddr, ref header, handleval);
        }
        public static void BTIEBR_ListPreRd(UInt32 listaddr, ref UInt16 header, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_ListPreRd(listaddr, ref header, handleval);
            else
                BTIEBR_64.BTIEBR_ListPreRd(listaddr, ref header, handleval);
        }
        public static void BTIEBR_ListPreWr(UInt32 listaddr, ref UInt16 header, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_ListPreWr(listaddr, ref header, handleval);
            else
                BTIEBR_64.BTIEBR_ListPreWr(listaddr, ref header, handleval);
        }
        public static Int32 BTIEBR_ListStatus(UInt32 listaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ListStatus(listaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_ListStatus(listaddr, handleval);
        }
        public static Int32 BTIEBR_ModeIsBC(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsBC(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsBC(chanflag);
        }
        public static Int32 BTIEBR_ModeIsMon(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsMon(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsMon(chanflag);
        }
        public static Int32 BTIEBR_ModeIsNotBC(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsNotBC(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsNotBC(chanflag);
        }
        public static Int32 BTIEBR_ModeIsNotMon(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsNotMon(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsNotMon(chanflag);
        }
        public static Int32 BTIEBR_ModeIsNotPlay(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsNotPlay(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsNotPlay(chanflag);
        }
        public static Int32 BTIEBR_ModeIsNotRT(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsNotRT(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsNotRT(chanflag);
        }
        public static Int32 BTIEBR_ModeIsNotSerial(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsNotSerial(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsNotSerial(chanflag);
        }
        public static Int32 BTIEBR_ModeIsPlay(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsPlay(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsPlay(chanflag);
        }
        public static Int32 BTIEBR_ModeIsRT(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsRT(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsRT(chanflag);
        }
        public static Int32 BTIEBR_ModeIsSerial(UInt16 chanflag)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ModeIsSerial(chanflag);
            else
                return BTIEBR_64.BTIEBR_ModeIsSerial(chanflag);
        }
        public static Int32 BTIEBR_MonConfigEx(UInt32 configval, UInt32 moncount, UInt16 cardnum, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MonConfigEx(configval, moncount, cardnum, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_MonConfigEx(configval, moncount, cardnum, channum, handleval);
        }
        public static Int32 BTIEBR_MonFilterSAEx(Int32 rtlink, UInt32 rcvsamask, UInt32 xmtsamask, UInt32 rcvmcmask, UInt32 xmtmcmask, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MonFilterSAEx(rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_MonFilterSAEx(rtlink, rcvsamask, xmtsamask, rcvmcmask, xmtmcmask, channum, handleval);
        }
        public static UInt16 BTIEBR_MsgCheck(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgCheck(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgCheck(msgaddr, handleval);
        }
        public static UInt32 BTIEBR_MsgCommRd(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgCommRd(ref buf, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgCommRd(ref buf, msgaddr, handleval);
        }
        public static UInt32 BTIEBR_MsgCommWr(ref MSGFIELDSEBR buf, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgCommWr(ref buf, msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgCommWr(ref buf, msgaddr, handleval);
        }
        public static UInt32 BTIEBR_MsgConfig(UInt32 configval, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgConfig(configval, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgConfig(configval, handleval);
        }
        public static UInt32 BTIEBR_MsgConfigEx(UInt32 configval, UInt16 cwd, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgConfigEx(configval, cwd, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgConfigEx(configval, cwd, handleval);
        }
        public static UInt16 BTIEBR_MsgCWDLinkRd(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgCWDLinkRd(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgCWDLinkRd(msgaddr, handleval);
        }
        public static void BTIEBR_MsgCWDLinkWr(UInt16 cwdlink, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgCWDLinkWr(cwdlink, msgaddr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgCWDLinkWr(cwdlink, msgaddr, handleval);
        }
        public static UInt16 BTIEBR_MsgCWDRd(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgCWDRd(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgCWDRd(msgaddr, handleval);
        }
        public static void BTIEBR_MsgCWDWr(UInt16 cwd, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgCWDWr(cwd, msgaddr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgCWDWr(cwd, msgaddr, handleval);
        }
        public static UInt16 BTIEBR_MsgErrorRd(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgErrorRd(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgErrorRd(msgaddr, handleval);
        }
        public static void BTIEBR_MsgErrorWr(UInt16 value, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgErrorWr(value, msgaddr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgErrorWr(value, msgaddr, handleval);
        }
        public static UInt16 BTIEBR_MsgFlagRd(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgFlagRd(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgFlagRd(msgaddr, handleval);
        }
        public static void BTIEBR_MsgFlagWr(UInt16 msgflag, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgFlagWr(msgflag, msgaddr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgFlagWr(msgflag, msgaddr, handleval);
        }
        public static Int32 BTIEBR_MsgIsAccessed(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgIsAccessed(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgIsAccessed(msgaddr, handleval);
        }
        public static UInt16 BTIEBR_MsgOptRd(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgOptRd(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgOptRd(msgaddr, handleval);
        }
        public static void BTIEBR_MsgOptWr(UInt16 msgopt, UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_MsgOptWr(msgopt, msgaddr, handleval);
            else
                BTIEBR_64.BTIEBR_MsgOptWr(msgopt, msgaddr, handleval);
        }
        public static UInt32 BTIEBR_MsgReset(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgReset(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgReset(msgaddr, handleval);
        }
        public static UInt32 BTIEBR_MsgResetEx(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgResetEx(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgResetEx(msgaddr, handleval);
        }
        public static UInt16 BTIEBR_MsgSWDLinkRd(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgSWDLinkRd(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgSWDLinkRd(msgaddr, handleval);
        }
        public static UInt16 BTIEBR_MsgSWDRd(UInt32 msgaddr, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_MsgSWDRd(msgaddr, handleval);
            else
                return BTIEBR_64.BTIEBR_MsgSWDRd(msgaddr, handleval);
        }
        public static Int32 BTIEBR_PlayClear(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_PlayClear(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_PlayClear(channum, handleval);
        }
        public static Int32 BTIEBR_PlayConfigEx(UInt32 configval, UInt32 playcount, UInt32 rtlinkmask, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_PlayConfigEx(configval, playcount, rtlinkmask, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_PlayConfigEx(configval, playcount, rtlinkmask, channum, handleval);
        }
        public static UInt32 BTIEBR_PlayCounterWr(UInt32 totalcount, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_PlayCounterWr(totalcount, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_PlayCounterWr(totalcount, channum, handleval);
        }
        public static Int32 BTIEBR_PlayStatusEx(ref UInt32 totalcount, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_PlayStatusEx(ref totalcount, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_PlayStatusEx(ref totalcount, channum, handleval);
        }
        public static UInt16 BTIEBR_PlayWrEx(UInt16[] buf, UInt16 bufcount, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_PlayWrEx(buf, bufcount, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_PlayWrEx(buf, bufcount, channum, handleval);
        }
        public static Int32 BTIEBR_RTConfigEx(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTConfigEx(configval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTConfigEx(configval, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_RTConfigExx(UInt32 configval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTConfigExx(configval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTConfigExx(configval, rtlink, channum, handleval);
        }
        public static UInt32 BTIEBR_RTCreateListEx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTCreateListEx(listconfigval, skipval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTCreateListEx(listconfigval, skipval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static UInt32 BTIEBR_RTCreateListExx(UInt32 listconfigval, Int32 skipval, Int32 count, UInt32 msgconfigval, Int32 mcflag, Int32 rtlink, Int32 trflag, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTCreateListExx(listconfigval, skipval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTCreateListExx(listconfigval, skipval, count, msgconfigval, mcflag, rtlink, trflag, saval, channum, handleval);
        }
        public static Int32 BTIEBR_RTResponseTimeRd(ref UInt16 timeval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTResponseTimeRd(ref timeval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTResponseTimeRd(ref timeval, rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_RTResponseTimeWr(UInt16 timeval, Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTResponseTimeWr(timeval, rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTResponseTimeWr(timeval, rtlink, channum, handleval);
        }
        public static UInt16 BTIEBR_RTStatus(Int32 rtlink, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_RTStatus(rtlink, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_RTStatus(rtlink, channum, handleval);
        }
        public static Int32 BTIEBR_SABlkWr(Int32 rtlink, Int32 saval, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SABlkWr(rtlink, saval, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SABlkWr(rtlink, saval, channum, handleval);
        }
        public static Int32 BTIEBR_SABlkWrEx(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SABlkWrEx(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SABlkWrEx(channum, handleval);
        }
        public static Int32 BTIEBR_SchedBuild(Int32 nummsgs, UInt32[] msgaddr, Int32[] freq, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedBuild(nummsgs, msgaddr, freq, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SchedBuild(nummsgs, msgaddr, freq, channum, handleval);
        }
        public static Int32 BTIEBR_SchedBuildEx(Int32 nummsgs, UInt32[] msgaddr, Int32[] freq, Int32[] period, IntPtr[] buf, UInt32 bufsize, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedBuildEx(nummsgs, msgaddr, freq, period, buf, bufsize, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SchedBuildEx(nummsgs, msgaddr, freq, period, buf, bufsize, channum, handleval);
        }
        public static Int32 BTIEBR_SchedBuildExx(UInt32 frametime, Int32 nummsgs, UInt32[] msgaddr, Int32[] period, IntPtr[] buf, UInt32 bufsize, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedBuildExx(frametime, nummsgs, msgaddr, period, buf, bufsize, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SchedBuildExx(frametime, nummsgs, msgaddr, period, buf, bufsize, channum, handleval);
        }
        public static Int32 BTIEBR_SchedCalc(ref UInt32 msgaddr, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedCalc(ref msgaddr, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SchedCalc(ref msgaddr, channum, handleval);
        }
        public static void BTIEBR_SchedDisplay(UInt32 nummsgs, ref UInt32 count)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_SchedDisplay(nummsgs, ref count);
            else
                BTIEBR_64.BTIEBR_SchedDisplay(nummsgs, ref count);
        }
        public static void BTIEBR_SchedHook(Int32 index, IntPtr ptr)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_SchedHook(index, ptr);
            else
                BTIEBR_64.BTIEBR_SchedHook(index, ptr);
        }
        public static Int32 BTIEBR_SchedInsEnd(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedInsEnd(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SchedInsEnd(channum, handleval);
        }
        public static Int32 BTIEBR_SchedInsMsg(UInt32 index, ref UInt32 msgaddr, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedInsMsg(index, ref msgaddr, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SchedInsMsg(index, ref msgaddr, channum, handleval);
        }
        public static Int32 BTIEBR_SchedInsStart(UInt32 frametime, UInt32 count, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedInsStart(frametime, count, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SchedInsStart(frametime, count, channum, handleval);
        }
        public static UInt32 BTIEBR_SchedMaxval(UInt32[] arrayval, UInt32 arrayvalcount)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedMaxval(arrayval, arrayvalcount);
            else
                return BTIEBR_64.BTIEBR_SchedMaxval(arrayval, arrayvalcount);
        }
        public static UInt32 BTIEBR_SchedMinval(UInt32[] arrayval, UInt32 arrayvalcount)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SchedMinval(arrayval, arrayvalcount);
            else
                return BTIEBR_64.BTIEBR_SchedMinval(arrayval, arrayvalcount);
        }
        public static Int32 BTIEBR_SerialClear(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SerialClear(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SerialClear(channum, handleval);
        }
        public static Int32 BTIEBR_SerialConfig(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SerialConfig(configval, count, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SerialConfig(configval, count, channum, handleval);
        }
        public static Int32 BTIEBR_SerialConfigEx(UInt32 configval, UInt16 count, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SerialConfigEx(configval, count, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SerialConfigEx(configval, count, channum, handleval);
        }
        public static Int32 BTIEBR_SerialStatus(Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SerialStatus(channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SerialStatus(channum, handleval);
        }
        public static UInt32 BTIEBR_SerialWr(UInt16[] buf, Int32 channum, IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_SerialWr(buf, channum, handleval);
            else
                return BTIEBR_64.BTIEBR_SerialWr(buf, channum, handleval);
        }
        public static Int32 BTIEBR_TestProtocol(IntPtr handleval)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_TestProtocol(handleval);
            else
                return BTIEBR_64.BTIEBR_TestProtocol(handleval);
        }
        public static UInt16 BTIEBR_ValPackRTCWD(Int32 mcflag, Int32 taval, Int32 trflag, Int32 saval, Int32 count)
        {
            if (IntPtr.Size == 4)
                return BTIEBR_32.BTIEBR_ValPackRTCWD(mcflag, taval, trflag, saval, count);
            else
                return BTIEBR_64.BTIEBR_ValPackRTCWD(mcflag, taval, trflag, saval, count);
        }
        public static void BTIEBR_ValUnpackRTCWD(UInt16 cwd, ref Int32 bcastflag, ref Int32 mcflag, ref Int32 taval, ref Int32 trflag, ref Int32 saval, ref Int32 count)
        {
            if (IntPtr.Size == 4)
                BTIEBR_32.BTIEBR_ValUnpackRTCWD(cwd, ref bcastflag, ref mcflag, ref taval, ref trflag, ref saval, ref count);
            else
                BTIEBR_64.BTIEBR_ValUnpackRTCWD(cwd, ref bcastflag, ref mcflag, ref taval, ref trflag, ref saval, ref count);
        }

        /**
        *
        *  Channel number constants
        *
        **/

        public const UInt32 CH0 = 0x0000;           //Channel 0
        public const UInt32 CH1 = 0x0001;           //Channel 1
        public const UInt32 CH2 = 0x0002;           //Channel 2
        public const UInt32 CH3 = 0x0003;           //Channel 3
        public const UInt32 CH4 = 0x0004;           //Channel 4
        public const UInt32 CH5 = 0x0005;           //Channel 5
        public const UInt32 CH6 = 0x0006;           //Channel 6
        public const UInt32 CH7 = 0x0007;           //Channel 7
        public const UInt32 CH8 = 0x0008;           //Channel 8
        public const UInt32 CH9 = 0x0009;           //Channel 9
        public const UInt32 CH10 = 0x000A;           //Channel 10
        public const UInt32 CH11 = 0x000B;           //Channel 11
        public const UInt32 CH12 = 0x000C;           //Channel 12
        public const UInt32 CH13 = 0x000D;           //Channel 13
        public const UInt32 CH14 = 0x000E;           //Channel 14
        public const UInt32 CH15 = 0x000F;           //Channel 15
        public const UInt32 CH16 = 0x0010;           //Channel 16
        public const UInt32 CH17 = 0x0011;           //Channel 17
        public const UInt32 CH18 = 0x0012;           //Channel 18
        public const UInt32 CH19 = 0x0013;           //Channel 19
        public const UInt32 CH20 = 0x0014;           //Channel 20
        public const UInt32 CH21 = 0x0015;           //Channel 21
        public const UInt32 CH22 = 0x0016;           //Channel 22
        public const UInt32 CH23 = 0x0017;           //Channel 23
        public const UInt32 CH24 = 0x0018;           //Channel 24
        public const UInt32 CH25 = 0x0019;           //Channel 25
        public const UInt32 CH26 = 0x001A;           //Channel 26
        public const UInt32 CH27 = 0x001B;           //Channel 27
        public const UInt32 CH28 = 0x001C;           //Channel 28
        public const UInt32 CH29 = 0x001D;           //Channel 29
        public const UInt32 CH30 = 0x001E;           //Channel 30
        public const UInt32 CH31 = 0x001F;           //Channel 31

        /**
        *
        *  Logical hub configuration options
        *
        **/

        public const UInt32 LHUBCFGEBR_DEFAULT = 0x00000000;	//Select all default settings
        public const UInt32 LHUBCFGEBR_ENABLE = 0x00000000;	//Enable all external ports (default)
        public const UInt32 LHUBCFGEBR_DISABLE = 0x00000001;	//Disable all external ports
        public const UInt32 LHUBCFGEBR_SPEC = 0x00000000;	//LHub will be configured for Spec mode (default)
        public const UInt32 LHUBCFGEBR_SWITCH = 0x00000010;	//LHub will be configured for Switch mode
        public const UInt32 LHUBCFGEBR_LINK = 0x00000020;	//LHub will be configured for Link mode

        /**
        *
        *  Bus controller configuration options
        *
        **/

        public const UInt32 BCCFGEBR_DEFAULT = 0x00000000;	//Select all default settings
        public const UInt32 BCCFGEBR_ENABLE = 0x00000000;	//Enable bus controller (default)
        public const UInt32 BCCFGEBR_DISABLE = 0x00000001;	//Disable bus controller
        public const UInt32 BCCFGEBR_NOLOGHALT = 0x00000000;	//Do not generate event log on halt (default)
        public const UInt32 BCCFGEBR_LOGHALT = 0x00000100;	//Generate event log on halt
        public const UInt32 BCCFGEBR_NOLOGPAUSE = 0x00000000;	//Do not generate event log on pause (default)
        public const UInt32 BCCFGEBR_LOGPAUSE = 0x00000200;	//Generate event log on pause
        public const UInt32 BCCFGEBR_NOLOOPMAX = 0x00000000;	//Disable maximum loop count (default)
        public const UInt32 BCCFGEBR_LOOPMAX = 0x00001000;	//Enable maximum loop count
        public const UInt32 BCCFGEBR_NOSTEP = 0x00000000;	//Disable single stepping (default)
        public const UInt32 BCCFGEBR_STEP = 0x00002000;	//Enable single stepping
        public const UInt32 BCCFGEBR_MC01 = 0x00000000;	//Select SA=00000 or SA=11111 for mode codes (default)
        public const UInt32 BCCFGEBR_MC1 = 0x01000000;	//Select SA=11111 for mode codes
        public const UInt32 BCCFGEBR_MC0 = 0x02000000;	//Select SA=00000 for mode codes
        public const UInt32 BCCFGEBR_MCNONE = 0x03000000;	//Disable mode codes
        public const UInt32 BCCFGEBR_PAUSE = 0x00000400;	//Mark channel as paused
        public const UInt32 BCCFGEBR_UNPAUSE = 0x00000000;	//Mark channel as unpaused (default)
        public const UInt32 BCCFGEBR_SELFTEST = 0x00800000;	//This channel will transmit/receive on the internal self-test bus
        public const UInt32 BCCFGEBR_SELFTESTOFF = 0x00000000;	//This channel will transmit/receive on the operational bus (default)

        /**
        *
        *  Monitor configuration options
        *
        **/

        public const UInt32 MONCFGEBR_DEFAULT = 0x00000000;	//Select all default settings
        public const UInt32 MONCFGEBR_ENABLE = 0x00000000;	//Enable monitor
        public const UInt32 MONCFGEBR_DISABLE = 0x00000001;	//Disable monitor
        public const UInt32 MONCFGEBR_NOBCAST = 0x00000000;	//Disable broadcast (default)
        public const UInt32 MONCFGEBR_BCAST = 0x00000100;	//Enable broadcast
        public const UInt32 MONCFGEBR_COMPLETE = 0x00000000;	//Enable receiving complete words (default)
        public const UInt32 MONCFGEBR_INCOMPLETE = 0x00010000;	//Enable receiving incomplete words
        public const UInt32 MONCFGEBR_MC01 = 0x00000000;	//Select SA=00000 or SA=11111 for mode codes (default)
        public const UInt32 MONCFGEBR_MC1 = 0x00100000;	//Select SA=11111 for mode codes
        public const UInt32 MONCFGEBR_MC0 = 0x00200000;	//Select SA=00000 for mode codes
        public const UInt32 MONCFGEBR_MCNONE = 0x00300000;	//Disable mode codes
        public const UInt32 MONCFGEBR_SELFTEST = 0x00800000;	//This channel will transmit/receive on the internal self-test bus
        public const UInt32 MONCFGEBR_SELFTESTOFF = 0x00000000;	//This channel will transmit/receive on the operational bus (default)

        /**
        *
        *  Remote terminal configuration options
        *
        **/

        public const UInt32 RTCFGEBR_DEFAULT = 0x00000000;	//Select all default settings
        public const UInt32 RTCFGEBR_SIMULATE = 0x00000000;	//Enable RT simulation (default)
        public const UInt32 RTCFGEBR_DISABLE = 0x00000001;	//Disable RT
        public const UInt32 RTCFGEBR_MONITOR = 0x00000002;	//Enable RT monitor
        public const UInt32 RTCFGEBR_NOBCAST = 0x00000000;	//Disable broadcast (default)
        public const UInt32 RTCFGEBR_BCAST = 0x00000100;	//Enable broadcast
        public const UInt32 RTCFGEBR_NOAUTOBUSY = 0x00000000;	//Disable auto busy (default)
        public const UInt32 RTCFGEBR_AUTOBUSY = 0x00000200;	//Enable auto busy
        public const UInt32 RTCFGEBR_BUILD = 0x00000000;	//Enable auto building (default)
        public const UInt32 RTCFGEBR_NOBUILD = 0x00000400;	//Disable auto building
        public const UInt32 RTCFGEBR_NOIMMCLR = 0x00000000;	//Do not clear status word bits (default)
        public const UInt32 RTCFGEBR_IMMCLR = 0x00002000;	//Status word bits are cleared immediately
        public const UInt32 RTCFGEBR_NOBCASTADDR = 0x00000000;	//Disable broadcast handling for address (default)
        public const UInt32 RTCFGEBR_BCASTADDR = 0x00004000;	//Enable broadcast handling for address
        public const UInt32 RTCFGEBR_MC01 = 0x00000000;	//Select SA=00000 or SA=11111 for mode codes (default)
        public const UInt32 RTCFGEBR_MC1 = 0x01000000;	//Select SA=11111 for mode codes
        public const UInt32 RTCFGEBR_MC0 = 0x02000000;	//Select SA=00000 for mode codes
        public const UInt32 RTCFGEBR_MCNONE = 0x03000000;	//Disable mode codes
        public const UInt32 RTCFGEBR_MCILLEGAL = 0x04000000;	//Illegalize mode codes defined as "Reserved - Not to be Used" in EBR
        public const UInt32 RTCFGEBR_WIPE = 0x00000000;	//Enables message clear (default)
        public const UInt32 RTCFGEBR_NOWIPE = 0x80000000;	//Disables message clear
        public const UInt32 RTCFGEBR_WIPE0 = 0x00000000;	//Initialize data with zeros (default)
        public const UInt32 RTCFGEBR_WIPE123 = 0x10000000;	//Initialize data with incrementing values
        public const UInt32 RTCFGEBR_WIPECWD = 0x20000000;	//Initialize data with command word
        public const UInt32 RTCFGEBR_SELFTEST = 0x00800000;	//This channel will transmit/receive on the internal self-test bus
        public const UInt32 RTCFGEBR_SELFTESTOFF = 0x00000000;	//This channel will transmit/receive on the operational bus (default)

        /**
        *
        *  Playback configuration options
        *
        **/

        public const UInt32 PLAYCFGEBR_DEFAULT = 0x00000000;	//Select all default settings
        public const UInt32 PLAYCFGEBR_ENABLE = 0x00000000;	//Enable Playback mode
        public const UInt32 PLAYCFGEBR_DISABLE = 0x00000001;	//Disable Playback mode
        public const UInt32 PLAYCFGEBR_16K = 0x00000000;	//Allocate a 16K playback buffer (default)
        public const UInt32 PLAYCFGEBR_ALLAVAIL = 0x00000010;	//Allocate all available memory to playback buffer
        public const UInt32 PLAYCFGEBR_32K = 0x00000020;	//Allocate a 32K playback buffer
        public const UInt32 PLAYCFGEBR_64K = 0x00000040;	//Allocate a 64K playback buffer
        public const UInt32 PLAYCFGEBR_128K = 0x00000080;	//Allocate a 128K playback buffer
        public const UInt32 PLAYCFGEBR_MC01 = 0x00000000;	//Select SA=00000 or SA=11111 for mode codes (default)
        public const UInt32 PLAYCFGEBR_MC1 = 0x01000000;	//Select SA=11111 for mode codes
        public const UInt32 PLAYCFGEBR_MC0 = 0x02000000;	//Select SA=00000 for mode codes
        public const UInt32 PLAYCFGEBR_MCNONE = 0x03000000;	//Disable mode codes

        /**
        *
        *  Serial configuration options
        *
        **/

        public const UInt32 SERCFGEBR_DEFAULT = 0x00000000;	//Select all default settings
        public const UInt32 SERCFGEBR_CONTINUOUS = 0x00000000;	//Enable continuous mode (default)
        public const UInt32 SERCFGEBR_DISABLE = 0x00000001;	//Disable free mode
        public const UInt32 SERCFGEBR_FREE = 0x00000002;	//Enable free mode
        public const UInt32 SERCFGEBR_NOLOGEMPTY = 0x00000000;	//Do not generate event log when empty (default)
        public const UInt32 SERCFGEBR_LOGEMPTY = 0x00000010;	//Generate event log when empty

        /**
        *
        *  Error configuration options
        *
        **/

        public const UInt32 ERRDEFEBR_NONE = 0x00000000;	//Disable error generation (default)
        public const UInt32 ERRDEFEBR_GAP = 0x00010000;	//Generate preceding gap error
        public const UInt32 ERRDEFEBR_CNTWRDABS = 0x00020000;	//Generate an absolute word count error
        public const UInt32 ERRDEFEBR_CNTWRDREL = 0x00040000;	//Generate a relative count error
        public const UInt32 ERRDEFEBR_CNTBIT = 0x00080000;	//Generate a bit count error
        public const UInt32 ERRDEFEBR_MAN1 = 0x00100000;	//Generate manchester error on first half of bit
        public const UInt32 ERRDEFEBR_MAN2 = 0x00200000;	//Generate manchester error on second half of bit
        public const UInt32 ERRDEFEBR_SYNC = 0x00400000;	//Generate a sync error
        public const UInt32 ERRDEFEBR_PAR = 0x00800000;	//Generate a parity error
        public const UInt32 ERRDEFEBR_ZEROX1 = 0x01000000;	//Generate zero crossing error on leading zero crossing (if exists)
        public const UInt32 ERRDEFEBR_ZEROX2 = 0x02000000;	//Generate zero crossing error on mid-bit zero crossing
        public const UInt32 ERRDEFEBR_DWD00 = 0x0000;	//Error will be generated in data word  0
        public const UInt32 ERRDEFEBR_DWD01 = 0x0001;	//Error will be generated in data word  1
        public const UInt32 ERRDEFEBR_DWD02 = 0x0002;	//Error will be generated in data word  2
        public const UInt32 ERRDEFEBR_DWD03 = 0x0003;	//Error will be generated in data word  3
        public const UInt32 ERRDEFEBR_DWD04 = 0x0004;	//Error will be generated in data word  4
        public const UInt32 ERRDEFEBR_DWD05 = 0x0005;	//Error will be generated in data word  5
        public const UInt32 ERRDEFEBR_DWD06 = 0x0006;	//Error will be generated in data word  6
        public const UInt32 ERRDEFEBR_DWD07 = 0x0007;	//Error will be generated in data word  7
        public const UInt32 ERRDEFEBR_DWD08 = 0x0008;	//Error will be generated in data word  8
        public const UInt32 ERRDEFEBR_DWD09 = 0x0009;	//Error will be generated in data word  9
        public const UInt32 ERRDEFEBR_DWD10 = 0x000A;	//Error will be generated in data word 10
        public const UInt32 ERRDEFEBR_DWD11 = 0x000B;	//Error will be generated in data word 11
        public const UInt32 ERRDEFEBR_DWD12 = 0x000C;	//Error will be generated in data word 12
        public const UInt32 ERRDEFEBR_DWD13 = 0x000D;	//Error will be generated in data word 13
        public const UInt32 ERRDEFEBR_DWD14 = 0x000E;	//Error will be generated in data word 14
        public const UInt32 ERRDEFEBR_DWD15 = 0x000F;	//Error will be generated in data word 15
        public const UInt32 ERRDEFEBR_DWD16 = 0x0010;	//Error will be generated in data word 16
        public const UInt32 ERRDEFEBR_DWD17 = 0x0011;	//Error will be generated in data word 17
        public const UInt32 ERRDEFEBR_DWD18 = 0x0012;	//Error will be generated in data word 18
        public const UInt32 ERRDEFEBR_DWD19 = 0x0013;	//Error will be generated in data word 19
        public const UInt32 ERRDEFEBR_DWD20 = 0x0014;	//Error will be generated in data word 20
        public const UInt32 ERRDEFEBR_DWD21 = 0x0015;	//Error will be generated in data word 21
        public const UInt32 ERRDEFEBR_DWD22 = 0x0016;	//Error will be generated in data word 22
        public const UInt32 ERRDEFEBR_DWD23 = 0x0017;	//Error will be generated in data word 23
        public const UInt32 ERRDEFEBR_DWD24 = 0x0018;	//Error will be generated in data word 24
        public const UInt32 ERRDEFEBR_DWD25 = 0x0019;	//Error will be generated in data word 25
        public const UInt32 ERRDEFEBR_DWD26 = 0x001A;	//Error will be generated in data word 26
        public const UInt32 ERRDEFEBR_DWD27 = 0x001B;	//Error will be generated in data word 27
        public const UInt32 ERRDEFEBR_DWD28 = 0x001C;	//Error will be generated in data word 28
        public const UInt32 ERRDEFEBR_DWD29 = 0x001D;	//Error will be generated in data word 29
        public const UInt32 ERRDEFEBR_DWD30 = 0x001E;	//Error will be generated in data word 30
        public const UInt32 ERRDEFEBR_DWD31 = 0x001F;	//Error will be generated in data word 31
        public const UInt32 ERRDEFEBR_CWD = 0x0100;	//Error will be generated in CWD
        public const UInt32 ERRDEFEBR_SWD = 0x0102;	//Error will be generated in SWD

        /**
        *
        *  Error control options
        *
        **/

        public const UInt32 ERRCTRLEBR_OFF = 0x00000000;	//Disable error generation
        public const UInt32 ERRCTRLEBR_ON = 0x00000001;	//Enable error generation
        public const UInt32 ERRCTRLEBR_ONCE = 0x00000002;	//Enable error generation once
        public const UInt32 ERRCTRLEBR_TAGMSG = 0x00000000;	//Generate errors on tagged messages
        public const UInt32 ERRCTRLEBR_ANYMSG = 0x80000000;	//Generate errors on any message

        /**
        *
        *  Message configuration options
        *
        **/

        public const UInt32 MSGCRTEBR_DEFAULT = 0x00000000;	//Default settings
        public const UInt32 MSGCRTEBR_ENABLE = 0x00000000;	//Enable subaddress (RT) (default)
        public const UInt32 MSGCRTEBR_DISABLE = 0x00001000;	//Disable subaddress (RT)
        public const UInt32 MSGCRTEBR_RESP = 0x00000000;	//Enable response (RT) (default)
        public const UInt32 MSGCRTEBR_NORESP = 0x00002000;	//Disable response (RT)
        public const UInt32 MSGCRTEBR_NOWRAP = 0x00000000;	//Disable data wrap (RT) (default)
        public const UInt32 MSGCRTEBR_WRAP = 0x00004000;	//Enable data wrap (RT)
        public const UInt32 MSGCRTEBR_NOLOG = 0x00000000;	//Message will not generate event log (default)
        public const UInt32 MSGCRTEBR_LOG = 0x00000001;	//Message will generate event log
        public const UInt32 MSGCRTEBR_NOERR = 0x00000000;	//No message error will be generated for message (default)
        public const UInt32 MSGCRTEBR_ERR = 0x00000002;	//A message error will be generated for message
        public const UInt32 MSGCRTEBR_BCRT = 0x00000000;	//Message is a BC-RT transfer (BC) (default)
        public const UInt32 MSGCRTEBR_NOMON = 0x00000000;	//Message will not be monitored (default)
        public const UInt32 MSGCRTEBR_MON = 0x00000010;	//Message will be monitored
        public const UInt32 MSGCRTEBR_NOTIMETAG = 0x00000000;	//Message will not record time-tag (default)
        public const UInt32 MSGCRTEBR_TIMETAG = 0x00000040;	//Message will record time-tag
        public const UInt32 MSGCRTEBR_NOELAPSE = 0x00000000;	//Message will not record elapse time (default)
        public const UInt32 MSGCRTEBR_ELAPSE = 0x00000080;	//Message will record elapse time
        public const UInt32 MSGCRTEBR_NOMIN = 0x00000000;	//Message will not record min time (default)
        public const UInt32 MSGCRTEBR_MIN = 0x00000100;	//Message will record min time
        public const UInt32 MSGCRTEBR_NOMAX = 0x00000000;	//Message will not record max time (default)
        public const UInt32 MSGCRTEBR_MAX = 0x00000200;	//Message will record max time
        public const UInt32 MSGCRTEBR_NOHIT = 0x00000000;	//Message will not record hit count (default)
        public const UInt32 MSGCRTEBR_HIT = 0x00000400;	//Message will record hit count
        public const UInt32 MSGCRTEBR_WIPE = 0x00000000;	//Enables message clear (default)
        public const UInt32 MSGCRTEBR_NOWIPE = 0x80000000;	//Disables message clear
        public const UInt32 MSGCRTEBR_WIPE0 = 0x00000000;	//Initialize data with zeros (default)
        public const UInt32 MSGCRTEBR_WIPE123 = 0x01000000;	//Initialize data with incrementing values
        public const UInt32 MSGCRTEBR_WIPECWD = 0x02000000;	//Initialize data with command word

        /**
        *
        *  List buffer options
        *
        **/

        public const UInt32 LISTCRTEBR_DEFAULT = 0x00000000;	//Select all default settings
        public const UInt32 LISTCRTEBR_FIFO = 0x00000000;	//Enable FIFO mode
        public const UInt32 LISTCRTEBR_CIRCULAR = 0x00000001;	//Enable circular mode
        public const UInt32 LISTCRTEBR_TRBIT = 0x00000000;	//User will read or write depending upon T/R bit
        public const UInt32 LISTCRTEBR_READ = 0x00000010;	//User will read from list buffer
        public const UInt32 LISTCRTEBR_WRITE = 0x00000020;	//User will write to list buffer
        public const UInt32 LISTCRTEBR_NOTSKIP = 0x00000000;	//Will not skip messages
        public const UInt32 LISTCRTEBR_SKIP = 0x00000040;	//Enable message skipping
        public const UInt32 LISTCRTEBR_ASYNC = 0x00000080;	//Asynchronous mode
        public const UInt32 LISTCRTEBR_NOLOG = 0x00000000;	//Do not generate event log when list buffer empty/full (default)
        public const UInt32 LISTCRTEBR_LOG = 0x00000100;	//Generate event log when list buffer empty/full
        public const UInt32 LISTCRTEBR_NOHIT = 0x00000000;	//Do not generate event log when list is hit (default)
        public const UInt32 LISTCRTEBR_HIT = 0x00000200;	//Generate event log when list is hit
        public const UInt32 LISTCRTEBR_NOLOGHALF = 0x00000000;	//Do not generate event log on middle and last entries (default)
        public const UInt32 LISTCRTEBR_LOGHALF = 0x00000400;	//Generate event log on middle and last entries

        /**
        *
        *  Condition flags.
        *
        **/

        public const UInt32 CONDEBR_ALWAYS = 0x00000000;	//Unconditional
        public const UInt32 CONDEBR_FAIL = 0x00000001;	//Condition when all retries have failed
        public const UInt32 CONDEBR_SRQ = 0x00000002;	//Condition on Service Request bit set in status word
        public const UInt32 CONDEBR_INS = 0x00000004;	//Condition on Instrumentation bit set in status word
        public const UInt32 CONDEBR_SSF = 0x00000008;	//Condition on Subsystem Fail bit set in status word
        public const UInt32 CONDEBR_TF = 0x00000010;	//Condition on Terminal Flag bit set in status word
        public const UInt32 CONDEBR_BUSY = 0x00000020;	//Condition on Busy bit set in status word
        public const UInt32 CONDEBR_ME = 0x00000040;	//Condition on Message Error bit set in status word
        public const UInt32 CONDEBR_RESPERR = 0x00000080;	//Condition on response error
        public const UInt32 CONDEBR_NORESP = 0x00000100;	//Condition on no response error
        public const UInt32 CONDEBR_DIO1ACT = 0x00001000;	//Condition on digital I/O #1 active
        public const UInt32 CONDEBR_DIO1NACT = 0x00002000;	//Condition on digital I/O #1 not active
        public const UInt32 CONDEBR_DIO2ACT = 0x00004000;	//Condition on digital I/O #2 active
        public const UInt32 CONDEBR_DIO2NACT = 0x00008000;	//Condition on digital I/O #2 not active
        public const UInt32 CONDEBR_DIO3ACT = 0x00010000;	//Condition on digital I/O #3 active
        public const UInt32 CONDEBR_DIO3NACT = 0x00020000;	//Condition on digital I/O #3 not active
        public const UInt32 CONDEBR_DIO4ACT = 0x00040000;	//Condition on digital I/O #4 active
        public const UInt32 CONDEBR_DIO4NACT = 0x00080000;	//Condition on digital I/O #4 not active

        /**
        *
        *  Bit fields of the status word
        *
        **/

        public const UInt32 SWDEBR_TAMASK = 0xF800;	//Masks off RT address in the SWD
        public const UInt32 SWDEBR_ME = 0x0400;	//Message error bit
        public const UInt32 SWDEBR_INST = 0x0200;	//Instrumentation bit
        public const UInt32 SWDEBR_SRQ = 0x0100;	//Service request bit
        public const UInt32 SWDEBR_BCAST = 0x0010;	//Broadcast received bit
        public const UInt32 SWDEBR_BUSY = 0x0008;	//Busy bit
        public const UInt32 SWDEBR_SSF = 0x0004;	//Subsystem flag bit
        public const UInt32 SWDEBR_DBC = 0x0002;	//Dynamic bus control acceptance bit
        public const UInt32 SWDEBR_TF = 0x0001;	//Terminal flag bit

        /**
        *
        *  Bit fields of the message error flag
        *
        **/

        public const UInt32 MSGERREBR_NORESP = 0x8000;	//Set when we timeout waiting for response
        public const UInt32 MSGERREBR_ANYERR = 0x4000;	//Set when any error bit is set
        public const UInt32 MSGERREBR_PROTOCOL = 0x2000;	//General protocol error
        public const UInt32 MSGERREBR_SYNC = 0x1000;	//Set when we have the wrong sync
        public const UInt32 MSGERREBR_DATACOUNT = 0x0800;	//Set when improper number of data words
        public const UInt32 MSGERREBR_LHUB = 0x0040;	//Set when unexpected LHUB activity detected
        public const UInt32 MSGERREBR_MANCH = 0x0020;	//Set when manchester error detected
        public const UInt32 MSGERREBR_PARITY = 0x0010;	//Set when parity error detected
        public const UInt32 MSGERREBR_WORD = 0x0008;	//Set when word error detected
        public const UInt32 MSGERREBR_RETRY = 0x0004;	//All retries failed
        public const UInt32 MSGERREBR_SYSTEM = 0x0002;	//An internal system error occurred
        public const UInt32 MSGERREBR_HIT = 0x0001;	//Always set

        /**
        *
        *  Bit fields of the message activity flag.
        *
        **/

        public const UInt32 MSGACTEBR_CHMASK = 0xF800;	//Channel number mask value
        public const UInt32 MSGACTEBR_CHSHIFT = 0x000B;	//Channel number shift value
        public const UInt32 MSGACTEBR_XMTCWD = 0x0080;	//Transmitted CWD
        public const UInt32 MSGACTEBR_XMTSWD = 0x0040;	//Transmitted SWD
        public const UInt32 MSGACTEBR_RCVCWD = 0x0020;	//Received CWD
        public const UInt32 MSGACTEBR_RCVSWD = 0x0010;	//Received SWD
        public const UInt32 MSGACTEBR_XMTDWD = 0x0008;	//Transmitted DWD
        public const UInt32 MSGACTEBR_RCVDWD = 0x0004;	//Received DWD
        public const UInt32 MSGACTEBR_MODEMASK = 0x0003;	//LHub Mode mask value
        public const UInt32 MSGACTEBR_MODESHIFT = 0x0000;	//LHub Mode shift value

        /**
        *
        *  Bit fields within the command info (cwdinfo).
        *
        **/

        public const UInt32 CWDINFOEBR_RESVMASK = 0xFFE0;	//Reserved mask value
        public const UInt32 CWDINFOEBR_LINKMASK = 0x001F;	//Link number mask value
        public const UInt32 CWDINFOEBR_LINKSHIFT = 0x0000;	//Link number shift value

        /**
        *
        *  Bit fields within the status info (swdinfo).
        *
        **/

        public const UInt32 SWDINFOEBR_RESVMASK = 0xFFE0;	//Reserved mask value
        public const UInt32 SWDINFOEBR_LINKMASK = 0x001F;	//Link number mask value
        public const UInt32 SWDINFOEBR_LINKSHIFT = 0x0000;	//Link number shift value

        /**
        *
        *  Message activity LHub Mode values.
        *
        **/

        public const UInt32 LHUBMODE_SPEC = 0x0000;	//LHub mode is Spec
        public const UInt32 LHUBMODE_SWITCH = 0x0001;	//LHub mode is Switch
        public const UInt32 LHUBMODE_LINK = 0x0002;	//LHub mode is Link

        /**
        *
        *  Field types.
        *
        **/

        public const UInt32 FIELDEBR_CWD = 0x0000;	//CWD field
        public const UInt32 FIELDEBR_CWDINFO = 0x0001;	//CWD info field
        public const UInt32 FIELDEBR_SWD = 0x0002;	//SWD field
        public const UInt32 FIELDEBR_SWDINFO = 0x0003;	//SWD info field
        public const UInt32 FIELDEBR_TTAG = 0x0004;	//Time tag field
        public const UInt32 FIELDEBR_ELAPSE = 0x0005;	//Elapse time field
        public const UInt32 FIELDEBR_ERROR = 0x0006;	//Error field
        public const UInt32 FIELDEBR_ACT = 0x0007;	//Activity field
        public const UInt32 FIELDEBR_RESP = 0x0008;	//Response time field
        public const UInt32 FIELDEBR_COUNT = 0x0009;	//Data count field
        public const UInt32 FIELDEBR_FLAG1 = 0x000A;	//Flag 1 field
        public const UInt32 FIELDEBR_FLAG2 = 0x000B;	//Flag 2 field
        public const UInt32 FIELDEBR_EXTFLAG = 0x000C;	//External flag field
        public const UInt32 FIELDEBR_TTAGH = 0x000D;	//Extended time tag field

        /**
        *
        *  Channel info fields.
        *
        **/

        public const UInt32 INFOEBR_MODEL = 0x0001;	//Channel model type
        public const UInt32 INFOEBR_MAXCOUNT = 0x0002;	//Channel terminal count
        public const UInt32 INFOEBR_MULTIMODE = 0x0003;	//Channel supports multi-mode
        public const UInt32 INFOEBR_ERRORGEN = 0x0004;	//Channel supports error generation
        public const UInt32 INFOEBR_CONCURMON = 0x0005;	//Channel supports concurrent monitor
        public const UInt32 INFOEBR_MONFILTER = 0x0006;	//Channel supports filtering of monitor
        public const UInt32 INFOEBR_PARAM = 0x0007;	//Channel supports parametric control
        public const UInt32 INFOEBR_RTRESPTIME = 0x0008;	//Channel supports programmable RT response time
        public const UInt32 INFOEBR_PLAYBACK = 0x0009;	//Channel supports hardware playback mode
        public const UInt32 INFOEBR_VER2CHOBJ = 0x000A;	//Channel supports version 2 channel object
        public const UInt32 INFOEBR_BCDFLTGAP = 0x000B;	//Channel supports programmable default gap time

        /**
        *
        *  Other flags.
        *
        **/

        public const UInt32 SUBADDRESS = 0x0000;                          //Selects Subaddress messages
        public const UInt32 MODECODE = 0x0001;                          //Selects Modecode messages
        public const Int16 EBR_DISABLED = -1;	//Flag setting/indicating a disabled RT link or port

    }	//end class
}	//end namespace
