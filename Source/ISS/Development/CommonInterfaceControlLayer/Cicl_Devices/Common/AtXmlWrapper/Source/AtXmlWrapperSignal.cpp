///////////////////////////////////////////////////////////////////////////////
// File    : AtXmlWwrapperSignal.cpp
//
// Purpose : Functions for supporting the AtXmlWwrapper interface Signal Processing;
//
//
// Date:	11OCT05
//
// Functions
// Name						Purpose
// =======================  ===================================================
// int     atxmlw_InstrumentCommands(ATXMLW_INTF_INSTCMD InstrumentCmds,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
//
// Revision History
// Rev	  Date                  Reason							Author
// ===  ========  =======================================  ====================
// 1.0  11OCT05   Initial baseline release.                T.G.McQuillen EADS
//
///////////////////////////////////////////////////////////////////////////////
#include <fstream>
#include "visa.h"
#include "AtXmlWrapper.h"

// Local Static Variables

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_InstrumentCommands
//
// Purpose: Parses the Instrument Command XML and issues the Instrument Commands.
//          It also responds to ...? commands in accordance with the ResponseMap.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
int atxmlw_InstrumentCommands(int ViHandle, ATXMLW_INTF_INSTCMD *InstrumentCmds,
                                //ATXMLW_INSTCMD_RESPONSE_TYPE *ResponseMap,
                                ATXMLW_INTF_RESPONSE *Response, int BufferSize,
                                int Dbg, int Sim)
{
    char *CmdBeg= NULL, *CmdEnd = NULL, *RespBuf = NULL, *cptr, *cptr2;
    double RespDelay = 0;
    int RespLen = 0;
    int WriteLen = 0;
    int ActWriteLen = 0;
    int ActRespLen = 0;
    int Status = 0;

/*
	<AtXmlIssueInstrumentCommands>
		<InstrumentResourceName>DMM_1</InstrumentResourceName>
		<InstrumentCommands>
			<Commands>*RST;IDN?</Commands>
			<ExpectedResponseString MaxLength="200" DelayInSeconds="0.001"/>
		</InstrumentCommands>
	</AtXmlIssueInstrumentCommands>
*/
    //Check for response first
    if((cptr = strstr(InstrumentCmds, "<ExpectedResponseString")))
    {
        // Get length
        if((cptr2 = strstr(cptr,"MaxLength")))
        {
            sscanf(cptr2,"MaxLength = \" %d",&RespLen);
        }
        else
            RespLen = 1024;
        // Get Delay
        if((cptr2 = strstr(cptr,"DelayInSeconds")))
        {
            sscanf(cptr2,"DelayInSeconds = \" %lf",&RespDelay);
        }
    }
    //Check for response first
    if((cptr = strstr(InstrumentCmds, "<Commands")))
    {
        // Isolate String
        CmdBeg = strstr(cptr,">");
        if(CmdBeg) CmdBeg++;
        CmdEnd = strstr(CmdBeg,"</Commands");
        WriteLen = CmdEnd - CmdBeg;
    }

    // Write command string
    if(WriteLen)
    {
        *CmdEnd = '\0';
        if(!Sim)
        {
            Status = viWrite(ViHandle, (ViBuf)CmdBeg, WriteLen, (ViPUInt32)&ActWriteLen);
        }
        else
            ActWriteLen = strlen(CmdBeg);
        *CmdEnd = '<';
        atxmlw_ScalerIntegerReturn("IcWriteLen", "", ActWriteLen, Response, BufferSize);
    }

    // Read Response String
    if(RespLen)
    {
        if(RespDelay)
            Sleep((int)(RespDelay * 1000.0));
        RespBuf = new char[RespLen+4];
        if(!Sim && RespBuf)
        {
            Status = viRead(ViHandle, (ViBuf)RespBuf, RespLen, (ViPUInt32)&ActRespLen);

			// TETS DSO Waveform return hack
            if (RespLen == 17000 && ActRespLen > 1599)
            {
	            //int nulCntr = 0;
	            for (int i = 0; i < ActRespLen; i++)
	            {
	                if (RespBuf[i] == '\0')
	                {
	                    //nulCntr++;
	                    RespBuf[i] = '0';
                    }
                }
            }

        }
        else
        {
            // Simulation Response
            strnzcpy(RespBuf,"1.0 Simulated viRead Response",RespLen);
            ActRespLen = strlen(RespBuf);
        }
        RespBuf[ActRespLen] = '\0';
        atxmlw_ScalerStringReturn("IcReadString", "", RespBuf, Response, BufferSize);
    }

	if(RespBuf)
		delete [] RespBuf;

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_doViDrvrFunc
//
// Purpose: Common place for implemented visa subroutine "DriverFunctionCalls".
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
bool atxmlw_doViDrvrFunc(ATXMLW_XML_HANDLE DfHandle,char *Name,ATXMLW_DF_VAL *RetValPtr)
{

    ATXMLW_DF_VAL RetVal;

    if(ISDFNAME("viSetAttribute"))
        RetVal.Int32 = viSetAttribute(SrcUInt32(1),SrcUInt32(2),SrcUInt32(3));
    else if(ISDFNAME("viGetAttribute"))
        RetVal.Int32 = viGetAttribute(SrcUInt32(1),SrcUInt32(2),RetUInt32(3));
    else if(ISDFNAME("viStatusDesc"))
        RetVal.Int32 = viStatusDesc(SrcUInt32(1),SrcInt32(2),RetStrPtr(3));
    else if(ISDFNAME("viTerminate"))
        RetVal.Int32 = viTerminate(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3));
    else if(ISDFNAME("viLock"))
        RetVal.Int32 = viLock(SrcUInt32(1),SrcUInt32(2),SrcUInt32(3),SrcStrPtr(4),RetStrPtr(5));
    else if(ISDFNAME("viUnlock"))
        RetVal.Int32 = viUnlock(SrcUInt32(1));
    else if(ISDFNAME("viEnableEvent"))
        RetVal.Int32 = viEnableEvent(SrcUInt32(1),SrcUInt32(2),SrcUInt16(3),SrcUInt32(4));
    else if(ISDFNAME("viDisableEvent"))
        RetVal.Int32 = viDisableEvent(SrcUInt32(1),SrcUInt32(2),SrcUInt16(3));
    else if(ISDFNAME("viDiscardEvents"))
        RetVal.Int32 = viDiscardEvents(SrcUInt32(1),SrcUInt32(2),SrcUInt16(32));
    else if(ISDFNAME("viWaitOnEvent"))
        RetVal.Int32 = viWaitOnEvent(SrcUInt32(1),SrcUInt32(2),SrcUInt32(3),RetUInt32(4),RetUInt32(5));
    else if(ISDFNAME("viRead"))
        RetVal.Int32 = viRead(SrcUInt32(1),RetUInt8Ptr(2),SrcUInt32(3),RetInt32(4));
    else if(ISDFNAME("viReadAsync"))
        RetVal.Int32 = viReadAsync(SrcUInt32(1),RetUInt8Ptr(2),SrcUInt32(3),RetInt32(4));
    else if(ISDFNAME("viReadToFile"))
        RetVal.Int32 = viReadToFile(SrcUInt32(1),SrcStrPtr(2),SrcUInt32(3),RetUInt32(4));
    else if(ISDFNAME("viWrite"))
        RetVal.Int32 = viWrite(SrcUInt32(1),SrcUInt8Ptr(2),SrcUInt32(3),RetInt32(4));
    else if(ISDFNAME("viWriteAsync"))
        RetVal.Int32 = viWriteAsync(SrcUInt32(1),SrcUInt8Ptr(2),SrcUInt32(3),RetInt32(4));
    else if(ISDFNAME("viWriteFromFile"))
        RetVal.Int32 = viWriteFromFile(SrcUInt32(1),SrcStrPtr(2),SrcUInt32(3),RetUInt32(4));
    else if(ISDFNAME("viAssertTrigger"))
        RetVal.Int32 = viAssertTrigger(SrcUInt32(1),SrcUInt16(2));
    else if(ISDFNAME("viReadSTB"))
        RetVal.Int32 = viReadSTB(SrcUInt32(1),RetUInt16(2));
    else if(ISDFNAME("viClear"))
        RetVal.Int32 = viClear(SrcUInt32(1));
    else if(ISDFNAME("viSetBuf"))
        RetVal.Int32 = viSetBuf(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3));
    else if(ISDFNAME("viFlush"))
        RetVal.Int32 = viFlush(SrcUInt32(1),SrcUInt16(2));
    else if(ISDFNAME("viBufWrite"))
        RetVal.Int32 = viBufWrite(SrcUInt32(1),SrcUInt8Ptr(2),SrcUInt32(3),RetUInt32(4));
    else if(ISDFNAME("viBufRead"))
        RetVal.Int32 = viBufRead(SrcUInt32(1),RetUInt8Ptr(2),SrcUInt32(3),RetUInt32(4));
    else if(ISDFNAME("viIn8"))
        RetVal.Int32 = viIn8(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),RetUInt8(4));
    else if(ISDFNAME("viOut8"))
        RetVal.Int32 = viOut8(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt8(4));
    else if(ISDFNAME("viIn16"))
        RetVal.Int32 = viIn16(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),RetUInt16(4));
    else if(ISDFNAME("viOut16"))
        RetVal.Int32 = viOut16(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4));
    else if(ISDFNAME("viIn32"))
        RetVal.Int32 = viIn32(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),RetUInt32(4));
    else if(ISDFNAME("viOut32"))
        RetVal.Int32 = viOut32(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt32(4));
    else if(ISDFNAME("viMoveIn8"))
        RetVal.Int32 = viMoveIn8(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4),RetUInt8Ptr(5));
    else if(ISDFNAME("viMoveOut8"))
        RetVal.Int32 = viMoveOut8(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4),RetUInt8Ptr(5));
    else if(ISDFNAME("viMoveIn16"))
        RetVal.Int32 = viMoveIn16(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4),RetUInt16Ptr(5));
    else if(ISDFNAME("viMoveOut16"))
        RetVal.Int32 = viMoveOut16(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4),RetUInt16Ptr(5));
    else if(ISDFNAME("viMoveIn32"))
        RetVal.Int32 = viMoveIn32(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4),RetUInt32Ptr(5));
    else if(ISDFNAME("viMoveOut32"))
        RetVal.Int32 = viMoveOut32(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4),RetUInt32Ptr(5));
    else if(ISDFNAME("viMove"))
        RetVal.Int32 = viMove(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4),SrcUInt16(5),SrcUInt32(6),SrcUInt16(7),SrcUInt16(8));
    else if(ISDFNAME("viMoveAsync"))
        RetVal.Int32 = viMoveAsync(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),SrcUInt16(4),SrcUInt16(5),SrcUInt32(6), SrcUInt16(7),SrcUInt32(8),RetUInt32(9));
    else if(ISDFNAME("viGpibControlREN"))
        RetVal.Int32 = viGpibControlREN(SrcUInt32(1),SrcUInt16(2));
    else if(ISDFNAME("viGpibControlATN"))
        RetVal.Int32 = viGpibControlATN(SrcUInt32(1),SrcUInt16(2));
    else if(ISDFNAME("viGpibSendIFC"))
        RetVal.Int32 = viGpibSendIFC(SrcUInt32(1));
    else if(ISDFNAME("viGpibCommand"))
        RetVal.Int32 = viGpibCommand(SrcUInt32(1),(unsigned char*)SrcStrPtr(2),SrcUInt32(3),RetUInt32(4));
    else if(ISDFNAME("viGpibPassControl"))
        RetVal.Int32 = viGpibPassControl(SrcUInt32(1),SrcUInt16(2),SrcUInt16(3));
    else if(ISDFNAME("viVxiCommandQuery"))
        RetVal.Int32 = viVxiCommandQuery(SrcUInt32(1),SrcUInt16(2),SrcUInt32(3),RetUInt32(4));
    else if(ISDFNAME("viAssertUtilSignal"))
        RetVal.Int32 = viAssertUtilSignal(SrcUInt32(1),SrcUInt16(2));
    else if(ISDFNAME("viAssertIntrSignal"))
        RetVal.Int32 = viAssertIntrSignal(SrcUInt32(1),SrcInt16(2),SrcUInt32(3));
    else if(ISDFNAME("viMapTrigger"))
        RetVal.Int32 = viMapTrigger(SrcUInt32(1),SrcInt16(2),SrcInt16(3),SrcUInt16(4));
    else if(ISDFNAME("viUnmapTrigger"))
        RetVal.Int32 = viUnmapTrigger(SrcUInt32(1),SrcInt16(2),SrcInt16(3));
#ifdef NOT_IMPLEMENTED
    else if(ISDFNAME("viOpenDefaultRM"))
        RetVal.Int32 = viOpenDefaultRM(SrcUInt32(1)); /**=**/
    else if(ISDFNAME("viFindRsrc"))
        RetVal.Int32 = viFindRsrc(SrcUInt32(1),SrcStrPtr(2),RetUInt32(3),RetUInt32(4),RetStrPtr(5)); /**=**/
    else if(ISDFNAME("viFindNext"))
        RetVal.Int32 = viFindNext(SrcUInt32(1),RetStrPtr(2)); 
    else if(ISDFNAME("viParseRsrc"))
        RetVal.Int32 = viParseRsrc(SrcUInt32(1),SrcStrPtr(2),RetUInt16(3),RetUInt16(4)); /**=**/
    else if(ISDFNAME("viClose"))
        RetVal.Int32 = viClose(SrcUInt32(1));
// viMapAddress not supported
    else if(ISDFNAME("viUnmapAddress"))
        RetVal.Int32 = viUnmapAddress(SrcUInt32(1));
    else if(ISDFNAME("viPeek8"))
        viPeek8(SrcUInt32(1),SrcUInt8Ptr(2),RetUInt8(3));
    else if(ISDFNAME("viPoke8"))
        viPoke8(SrcUInt32(1),RetUInt8Ptr(2),SrcUInt8(3));
    else if(ISDFNAME("viPeek16"))
        viPeek16(SrcUInt32(1),SrcUInt16Ptr(2),RetUInt16(3));
    else if(ISDFNAME("viPoke16"))
        viPoke16(SrcUInt32(1),RetUInt16Ptr(2),SrcUInt16(3));
    else if(ISDFNAME("viPeek32"))
        viPeek32(SrcUInt32(1),SrcUInt32Ptr(2),RetUInt32(3));
    else if(ISDFNAME("viPoke32"))
        viPoke32(SrcUInt32(1),RetUInt32Ptr(2),SrcUInt32(3));
    else if(ISDFNAME("viMemAlloc"))
        RetVal.Int32 = viMemAlloc(SrcUInt32(1),SrcUInt16(2),RetUInt32(3));
    else if(ISDFNAME("viMemFree"))
        RetVal.Int32 = viMemFree(SrcUInt32(1),SrcUInt32(2));
    else if(ISDFNAME("viParseRsrcEx"))
        RetVal.Int32 = viParseRsrcEx(SrcUInt32(1),SrcStrPtr(2),RetUInt16(3),RetUInt16(4),SrcStrPtr(5),SrcStrPtr(6),SrcStrPtr(7)); /**=**/
    else if(ISDFNAME("viUsbControlOut"))
        RetVal.Int32 = viUsbControlOut(SrcUInt32(1),SrcInt16(2),SrcInt16(3),SrcUInt16(4),SrcUInt16(5),SrcUInt16(6),SrcStrPtr(7));
// viUsbControlIn" not supported
#endif
    else
        return(false);

    if(RetValPtr)
        *RetValPtr = RetVal;

    return(true);

}