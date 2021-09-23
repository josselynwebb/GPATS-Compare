///////////////////////////////////////////////////////////////////////////////
// File    : AtXmlResponse.cpp
//
// Purpose : Functions for supporting the AtXml;
//
//
// Date:	11OCT05
//
// Functions
// Name						Purpose
// =======================  ===================================================
// void    atxml_ErrorResponse(ATML_INTF_RESPONSE *Response, int BufferSize, 
//                       char *LeadText, int ErrCode, char *ErrMsg);
// void    atxml_DebugMsg(int MemDbgLvl, int ReqLvl, char *Msg);
// char   *atxml_FmtMsg(char *Fmt,...);
// void    atxml_SystemError(int ErrLevel, char *LeadText, int ErrCode, char *ErrMsg);
//
// Revision History
// Rev	  Date                  Reason							Author
// ===  ========  =======================================  ====================
// 1.0  11OCT05   Initial baseline release.                T.G.McQuillen EADS
//
///////////////////////////////////////////////////////////////////////////////
#include <fstream>
#include "CiCoreCommon.h"

#define MAX_LOGS 10
typedef struct CiCoreResponseLogFileStruct
{
    int mode;
    char name[256];
}CICL_LOG;
 
// Local Static Variables
static CICL_LOG s_CiclLogs[]={
    (0,0),
    (0,0),
    (0,0),
    (0,0),
    (0,0),
    (0,0),
    (0,0),
    (0,0),
    (0,0),
    (0,0),
};

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: atxml_ErrorResponse
//
// Purpose: Adds the error response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// LeadText           char*                Lead diagnostic text e.g. "Routine Name" etc.
// ErrCode            int                  Error Code (- Error, + Warning)
// ErrMsg             char*                Error Message Text 
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXML_XML_String*     Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxml_ErrorResponse(ATXML_XML_String *Response, int BufferSize, 
                           char *LeadText, int ErrCode, char *ErrMsg)
{

    int   XmlSize;

    /*
	<AtXmlResponse>
		<ErrStatus moduleName="Module Name" leadText="Lead Text" errCode="0" errText=""/>
	</AtXmlResponse>
    */
    // Determine the size of the XML string
    XmlSize = 200; // Xml stuff
    XmlSize += (LeadText ? (strlen(LeadText)) : 0);
    XmlSize += (ErrMsg ? (strlen(ErrMsg)) : 0);

    // Check if Response is big enough
    if((Response == NULL) || (BufferSize < (int)(strlen(Response) + XmlSize)))
        return;

    // Issue XML at the end of the Response Buffer
    strcat(Response,
        "<AtXmlResponse>\n"
		"  <ErrStatus moduleName=\"System\" leadText=\"");
    if(LeadText)
        strcat(Response,LeadText);
    strcat(Response,
        "\" errCode=\"");
    itoa(ErrCode,&Response[strlen(Response)],10);
    strcat(Response,
        "\" errText=\"");
    if(ErrMsg)
        strcat(Response,ErrMsg);//FIX for double quotes
    strcat(Response,"\"></ErrStatus>\n</AtXmlResponse>\n");


    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: atxml_DebugMsg
//
// Purpose: Adds the error response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// MemDbgLvl          int                  Current Member debug level
// ReqLvl             int                  Required debug level for this debug message
// Msg                char*                Debug Message Text 
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXML_XML_String*     Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxml_DebugMsg(int MemDbgLvl, int ReqLvl, char *Msg,
                   ATXML_XML_String *Response, int BufferSize)
{

    int   XmlSize;

    // Check for proper level
    if(MemDbgLvl < ReqLvl)
        return;
    /*
	<AtXmlResponse>
		<DebugMessage  DbgLevel="5" ModuleName="Module Name"  Message="This is the message" />
	</AtXmlResponse>
    */
    // Determine the size of the XML string
    XmlSize = 200; // Xml stuff
    XmlSize += (Msg ? (strlen(Msg)) : 0);

    // Check if Response is big enough
    if((Response == NULL) || (BufferSize < (int)(strlen(Response) + XmlSize)))
        return;

    // Issue XML at the end of the Response Buffer
    strcat(Response,
        "<AtXmlResponse>\n"
		"  <DebugMessage moduleName=\"System\" dbgLevel=\"");
    itoa(ReqLvl,&Response[strlen(Response)],10);
    strcat(Response,
        "\" message=\"");
    if(Msg)
        strcat(Response,Msg);//FIX for double quotes
    strcat(Response,"\"></DebugMessage>\n</AtXmlResponse>\n");
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxml_FmtMsg
//
// Purpose: Format a print string (i.e. sprintf returning a pointer).
// Note:    This function uses the static char array s_Msg and thus is not
//          thread proof!
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Fmt...             char*                printf format and subsequent arguments
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: pointer to formated string or empty string
//
///////////////////////////////////////////////////////////////////////////////
char *atxml_FmtMsg(char *Fmt,...)
{
     va_list Args;
   // Make this a Dynamic buffer that purges each call
    static char s_Msg[20000];

    s_Msg[0] = '\0';
    // prepare to use printf type arguments
    va_start(Args,Fmt);
    // Create Message
    vsprintf(s_Msg,Fmt,Args);

    return(s_Msg);
}

///////////////////////////////////////////////////////////////////////////////
// Function: void atxml_SystemError
//
// Purpose: Handle System level error conditions.  e.g. logging, display etc.
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// ErrLevel          int                 Warning, Error, Fatal etc. Ignored for now
// LeadText          char*               Lead diagnostic text e.g. "Routine Name" etc.
// ErrCode           int                 Error Code (- Error, + Warning)
// ErrMsg            char*               Error Message Text 
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxml_SystemError(int ErrLevel, char *LeadText, int ErrCode, char *ErrMsg)
{
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: void atxml_LogInit
//
// Purpose: System Status Logging, display etc. Initialization
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// Mode              char*               Type of Log 
// FileName          char*               Logfile name 
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxml_LogInit(int Mode, char *FileName, bool Append)
{
    FILE *fid;
    char *OpenString;
    if((Mode > 0) && (Mode <= MAX_LOGS))
    {
        s_CiclLogs[Mode-1].mode = Mode;
        strncpy(s_CiclLogs[Mode-1].name,FileName,250);
        OpenString = Append ? "at" : "wt";
        if((fid = fopen(FileName,OpenString)))
        {
            //FIX Timestamp
            fprintf(fid,"Station Controller Init Log File\n");
            fclose(fid);
        }
    }
    
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: void atxml_Log
//
// Purpose: System Status Logging, display etc.
//
// Input Parameters
// Parameter		 Type			     Purpose
// ================= ==================  ===========================================
// Mode              char*               Type of Log 
// Message           char*               Log text 
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxml_Log(int Mode, char *Message)
{
    FILE *fid;
    time_t ltime;
    char cnow[80];

    time(&ltime);
    sscanf((ctime(&ltime)),"%[^\n]",cnow);

    if((Mode > 0) && (Mode <= MAX_LOGS) && 
       (Mode == s_CiclLogs[Mode-1].mode) && (s_CiclLogs[Mode-1].name[0]))
    {
        if((fid = fopen(s_CiclLogs[Mode-1].name,"at")))
        {
            //FIX Timestamp
            fprintf(fid,"%s: %s\n",cnow,Message);
            fclose(fid);
        }
        if(Mode == ATXML_LOG_DEBUG)
            printf("%s\n",Message);
    }
    
    return;
}



