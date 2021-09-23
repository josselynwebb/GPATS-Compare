///////////////////////////////////////////////////////////////////////////////
// File    : CemDisplay.cpp
//
// Purpose : Functions for displaying primarily debug messages to the RTS
//           scrolling text area.
//
//
// Date:	11OCT05
//
// Functions
// Name						Purpose
// =======================  ===================================================
// cs_DebugMsg()			Displays a CEMDEBUG message according to the current
//                          Debug setting.
//
// Revision History
// Rev	  Date                  Reason							Author
// ===  ========  =======================================  ====================
// 1.0  11OCT05   Initial baseline release.                T.G.McQuillen EADS
//
///////////////////////////////////////////////////////////////////////////////
#include "cem.h"
#include "cemsupport.h"
#include "swxsrvr.h"


// Local Prototypes
bool s_ParseTimeOut(char *Response);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: cs_FmtMsgchar *Fmt,...)
//
// Purpose: Format a print string (i.e. sprintf returning a pointer).
// Note:    This function uses the static char array s_Msg and thus is not
//          thread proof!
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fmt ...          char*           printf format and subsequent arguments
//
//
// Return: pointer to formated string or empty string
//
///////////////////////////////////////////////////////////////////////////////
char s_Msg[MAX_MSG_SIZE];
char *cs_FmtMsg(char *Fmt,...)
{
    va_list Args;

    s_Msg[0] = '\0';
    // prepare to use printf type arguments
    va_start(Args,Fmt);
    // Create Message
    vsprintf(s_Msg,Fmt,Args);

    return(s_Msg);
}

///////////////////////////////////////////////////////////////////////////////
// Function: cs_DebugMsg(int MemLevel, char *MemDevName, int ReqLevel,
//                       char *Fmt,...)
//
// Purpose: Displays a CEMDEBUG message according to the current Debug setting.
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// MemLevel         int             The current member debug level
// MemDeviceName    char*           The current member variable device name
// ReqLevel         int             The requested debug level for the message
// Fmt ...          char*           printf format and subsequent arguments
//
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void cs_DebugMsg(int MemLevel, char *MemDevName, int ReqLevel, char *UsrMsg)
{
    char Msg[MAX_MSG_SIZE];

    // Determine if to display or not
    if(MemLevel < ReqLevel)
        return;
    // Create Message
    sprintf(Msg,"\033[32;40mCEMDEBUG: %s - %s\033[m\n",MemDevName,UsrMsg);
    // Display it
    Display(Msg);

    return;
}


/*
Severity Field   There are three mutually-exclusive labels for specifying the severity of the error:

EB_SEVERITY_INFO
EB_SEVERITY_WARNING
EB_SEVERITY_ERROR

Action Field   There are three mutually exclusive labels for specifying what action the RTS is to take pertaining to the execution of the ATLAS program:

EB_ACTION_HALT
EB_ACTION_RESET
EB_ACTION_ABORT

Set Field   There is one label for specifying a specific RTS action:

EB_SET_MAXTIME
*/
///////////////////////////////////////////////////////////////////////////////
// Function: cs_ErrMsg(int Type, int Flags, char *Msg)
//
// Purpose: Prefixes Msg with either "CEM - " or "INST -" and passes to
//          ErrMsgBits.
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Type             int             1 = INST, ? = CEM
// Flags            int             Std ErrMsgBits flags
// Msg              char*           Message
//
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void cs_ErrMsg(int Type, int Flags, char *UsrMsg)
{
    char Msg[MAX_MSG_SIZE];
    char *CemInst;

    CemInst = "CEM";
    if(Type == 1)
        CemInst = "INST";
    // Create Message
    sprintf(Msg,"%s - %s\n",CemInst,UsrMsg);
    // Send it
    ErrMsgBits(Flags,Msg);

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: cs_ParseResponseError & cs_ParseResponseErrorOvr
//
// Purpose: Scan the Response XML for errors and debuf statements.
//          Copy to ValueResponse non-error/non-debug elements
//
// Input Parameters
// Parameter		 Type			 Purpose
// ===============   ==============  ===========================================
// Response          char*           XML Response string
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValueResponse    char*           XML Response string with non error/debug elements
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
int cs_ParseResponseError(char *Response, char *ValueResponse, int BufferSize)
{
    int Status;

    Status = cs_ParseResponseErrorOvr(0, Response, ValueResponse, BufferSize);

    return(Status);
}
int cs_ParseResponseErrorOvr(int SetErrLevel, char *Response,
                                            char *ValueResponse, int BufferSize)
{
    void  *ErrRespHandle;
    char  *ModuleName="";
    char  *LeadText="";
    char  *Message="";
    int    ErrorCode;
    int    DbgLevel;
    int    ErrBits = EB_SEVERITY_INFO;
    int    x;
    bool   MaxTimeFlag;

    // First Check for "timeOut" value returned indicating instrument timed out
    MaxTimeFlag = s_ParseTimeOut(Response);

    x = ParseErrDbgResponse(Response, BufferSize, true, &ErrRespHandle);
    if((x == 0) && ErrRespHandle)
    {
        while(ErrDbgNextError(ErrRespHandle, &ModuleName, &LeadText,
                                      &ErrorCode, &Message))
        {
            if(SetErrLevel == 0)
            {
                if(ErrorCode == 0)
                    ErrBits = EB_SEVERITY_INFO;
                else if(ErrorCode < 0)
                    ErrBits = EB_SEVERITY_ERROR;
                else 
                    ErrBits = EB_SEVERITY_WARNING;
            }
            if(MaxTimeFlag)
            {
                ErrBits |= EB_SET_MAXTIME;
                MaxTimeFlag = 0;// indicate MaxTime handed back to wrts
            }
            cs_ErrMsg(1, ErrBits, 
                    cs_FmtMsg("%x - %s - %s - %s",ErrorCode,ModuleName,LeadText,Message));
            ErrBits = EB_SEVERITY_INFO;
        }
        while(ErrDbgNextDebug(ErrRespHandle, &ModuleName,
                                      &DbgLevel, &Message))
        {
            cs_DebugMsg(DbgLevel,ModuleName,DbgLevel,Message);
        }
        ErrDbgClose(ErrRespHandle);
    }

    if(MaxTimeFlag)
    {
        ErrMsgBits(EB_SET_MAXTIME,"");
        MaxTimeFlag = 0;// indicate MaxTime handed back to wrts
    }

    if(ValueResponse)
    {
        strnzcpy(ValueResponse,Response,BufferSize);
    }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: s_ParseTimeOut
//
// Purpose: Scan the Response XML for the "timeOut" attribute to determine if
//          MAX-TIME Flag should be set 
//
// Input Parameters
// Parameter		 Type			 Purpose
// ===============   ==============  ===========================================
// Response          char*           XML Response string
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValueResponse    char*           XML Response string with non error/debug elements
//
// Return: 
//         true timeOut = 1 
//       else false
//
///////////////////////////////////////////////////////////////////////////////
bool s_ParseTimeOut(char *Response)
{
    int  TimeOut = 0;
    bool RetVal = false;
    char *cptr, *BegEleCptr, *EndEleCptr ;
/*
	<AtXmlResponse>
		<ReturnData>
			<ValuePair>
				<Attribute>timeOut</Attribute>
				<Value>
					<c:Datum xsi:type="c:integer" value="1"/>
				</Value>
			</ValuePair>
		</ReturnData>
	</AtXmlResponse>
*/
    BegEleCptr = Response;
    EndEleCptr = Response;
    while(BegEleCptr && EndEleCptr &&
          (BegEleCptr = strstr(BegEleCptr,"<ValuePair>")) &&
          (EndEleCptr = strstr(BegEleCptr,"</ValuePair>")))
    {
        if((cptr = strstr(BegEleCptr,"timeOut")) &&
           (cptr < EndEleCptr) &&
           (cptr = strstr(cptr,"value=")) &&
           (cptr < EndEleCptr) &&
           (sscanf(cptr,"value = \" %d",&TimeOut) == 1))
        {
            RetVal = TimeOut ? true : false;
            break;
        }
        BegEleCptr = EndEleCptr;
    }

    return(RetVal);
}
