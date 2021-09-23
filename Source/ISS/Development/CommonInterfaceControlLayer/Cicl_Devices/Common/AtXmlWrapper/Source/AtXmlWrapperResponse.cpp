///////////////////////////////////////////////////////////////////////////////
// File    : AtXmlWwrapperResponse.cpp
//
// Purpose : Functions for supporting the AtXmlWwrapper interface;
//
//
// Date:	11OCT05
//
// Functions
// Name						Purpose
// =======================  ===================================================
// void    atxmlw_ScalerIntegerReturn(char *Attribute, char *Unit, int Value,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// void    atxmlw_IntegerArrayReturn(char *Attribute, char *Unit,
//                       int *IntArray, int Count,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// void    atxmlw_ScalerDoubleReturn(char *Attribute, char *Unit, double Value,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// void    atxmlw_DoubleArrayReturn(char *Attribute, char *Unit, 
//                       double *DblArray, int Count, 
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// void    atxmlw_ScalerStringReturn(char *Attribute, char *Unit, char *Value,
//                       ATXMLW_INTF_RESPONSE *Response, int BufferSize);
// void    atxmlw_ErrorResponse(char *InstId, ATXMLW_INTF_RESPONSE *Response, int BufferSize, 
//                       char *LeadText, int ErrCode, char *ErrMsg);
// void    atxmlw_DebugMsg(int MemDbgLvl, char *InstId, int ReqLvl, char *Msg);
// char   *atxmlw_FmtMsg(char *Fmt,...);
//
// Revision History
// Rev	  Date                  Reason							Author
// ===  ========  =======================================  ====================
// 1.0  11OCT05   Initial baseline release.                T.G.McQuillen EADS
//
///////////////////////////////////////////////////////////////////////////////
#include <fstream>
#include "AtXmlWrapper.h"

// Local Static Variables

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_ScalerIntegerReturn
//
// Purpose: Adds the Integer Datum response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Attribute          char*                Attribute being returned
// Unit               char*                Unit of measure
// Value              int                  Integer value being returned
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxmlw_ScalerIntegerReturn(char *Attribute, char *Unit, int Value,
                   ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
    char *lclAttribute = "";
    char *lclUnit = "";

    if(Attribute) lclAttribute = Attribute;
    if(Unit) lclUnit = Unit;
    if(Response && (BufferSize > ((int)strlen(Response)+200)))
    {
        sprintf(&Response[strlen(Response)],
	    "<AtXmlResponse>\n  "
	    "	<ReturnData>\n"
	    "		<ValuePair>\n"
	    "			<Attribute>%s</Attribute>\n"
	    "			<Value>\n"
	    "				<c:Datum xsi:type=\"c:integer\" unit=\"%s\" value=\"%d\"/>\n"
	    "			</Value>\n"
	    "		</ValuePair>\n"
	    "	</ReturnData>\n"
	    "</AtXmlResponse>\n",lclAttribute,lclUnit,Value);
    }
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_IntegerArrayReturn
//
// Purpose: Adds the Integer Datum response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Attribute          char*                Attribute being returned
// Unit               char*                Unit being returned
// IntArray           int*                 Pointer to a Integer array
// Count              int                  Number of integers in array
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxmlw_IntegerArrayReturn(char *Attribute, char *Unit,
                   int *IntArray, int Count,
                   ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
    char *lclAttribute = "";
    char *lclUnit = "";
    int BufSize;
    int i;
/*
	<ValuePair>
		<Attribute>xxx1641</Attribute>
		<IntegerArray  unit="V" arrayValues="[1, 2, 3]"></IntegerArray>
	</ValuePair>
*/
    if(Attribute) lclAttribute = Attribute;
    if(Unit) lclUnit = Unit;
    BufSize = (int)strlen(Response) + 400 + (Count * 11);
    if(Response && (BufferSize > BufSize))
    {
        sprintf(&Response[strlen(Response)],
	    "<AtXmlResponse>\n"
	    " <ReturnData>\n"
	    "  <ValuePair>\n"
	    "	<Attribute>%s</Attribute>\n"
	    "	<Value>\n"
        "    <IntegerArray\" unit=\"%s\" arrayValues=\"[",
        lclAttribute,lclUnit);
        // Output Elements
        for(i=0; i<Count; i++)
        {
            if(i!=0)
                strcat(Response,",");
            sprintf(&Response[strlen(Response)],"0x%x",IntArray[i]);
        }
        sprintf(&Response[strlen(Response)],
        "]\"/>\n"
	    "   </Value>\n"
	    "  </ValuePair>\n"
	    " </ReturnData>\n"
	    "</AtXmlResponse>\n");
    }
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_ScalerDoubleReturn
//
// Purpose: Adds the Double Datum response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Attribute          char*                Attribute being returned
// Unit               char*                Unit of measure
// Value              double               Double value being returned
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxmlw_ScalerDoubleReturn(char *Attribute, char *Unit, double Value,
                   ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
    char *lclAttribute = "";
    char *lclUnit = "";

    if(Attribute) lclAttribute = Attribute;
    if(Unit) lclUnit = Unit;
    if(Response && (BufferSize > ((int)strlen(Response)+200)))
    {
        sprintf(&Response[strlen(Response)],
	    "<AtXmlResponse>\n  "
	    "	<ReturnData>\n"
	    "		<ValuePair>\n"
	    "			<Attribute>%s</Attribute>\n"
	    "			<Value>\n"
	    "				<c:Datum xsi:type=\"c:double\" unit=\"%s\" value=\"%18.12E\"/>\n"
	    "			</Value>\n"
	    "		</ValuePair>\n"
	    "	</ReturnData>\n"
	    "</AtXmlResponse>\n",lclAttribute,lclUnit,Value);
    }
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_DoubleArrayReturn
//
// Purpose: Adds the Double Datum response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Attribute          char*                Attribute being returned
// DblArray           double*              Pointer to a Double array
// Count              int                  Number of double vectors in array
// Unit               char*                pointer to Unit string
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxmlw_DoubleArrayReturn(char *Attribute, char *Unit, double *DblArray, int Count,
                   ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
    char *lclAttribute = "";
    int BufSize;
    int i;
/*
	<ValuePair>
		<Attribute>xxx</Attribute>
			<Value>
				<RealArray  unit="V" arrayValues="[1.0, 2.0, 3.0]"/>
			</Value>
	</ValuePair>
*/
    if(Attribute) lclAttribute = Attribute;
    BufSize = (int)strlen(Response) + 400 + (Count * 14);
    if(Response && (BufferSize > BufSize))
    {
        sprintf(&Response[strlen(Response)],
	    "<AtXmlResponse>\n"
	    " <ReturnData>\n"
	    "  <ValuePair>\n"
	    "	<Attribute>%s</Attribute>\n"
	    "	<Value>\n"
        "    <RealArray\" unit=\"%s\" arrayValues=\"[",
        lclAttribute,Unit);
        // Output Elements
        for(i=0; i<Count; i++)
        {
            if(i!=0)
                strcat(Response,",");
            sprintf(&Response[strlen(Response)],"%13.6E",DblArray[i]);
        }
        sprintf(&Response[strlen(Response)],
        "]\"/>\n"
        "   </Value>\n"
	    "  </ValuePair>\n"
	    " </ReturnData>\n"
	    "</AtXmlResponse>\n");
    }
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_ScalerStringReturn
//
// Purpose: Adds the String Datum response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Attribute          char*                Attribute being returned
// Unit               char*                Unit of measure
// Value              char*                String value being returned
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxmlw_ScalerStringReturn(char *Attribute, char *Unit, char *Value,
                   ATXMLW_INTF_RESPONSE *Response, int BufferSize)
{
    char *lclAttribute = "";
    char *lclUnit = "";
    char *lclValue = "";

    if(Attribute) lclAttribute = Attribute;
    if(Unit) lclUnit = Unit;
    if(Value) lclValue = Value;
    if(Response && (BufferSize > ((int)strlen(Response)+200)))
    {
        sprintf(&Response[strlen(Response)],
	    "<AtXmlResponse>\n  "
	    "	<ReturnData>\n"
	    "		<ValuePair>\n"
	    "			<Attribute>%s</Attribute>\n"
	    "			<Value>\n"
	    "				<c:Datum xsi:type=\"c:string\" unit=\"%s\"><c:Value>%s</c:Value></c:Datum>\n"
	    "			</Value>\n"
	    "		</ValuePair>\n"
	    "	</ReturnData>\n"
	    "</AtXmlResponse>\n",lclAttribute,lclUnit,lclValue);
    }
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_ErrorResponse
//
// Purpose: Adds the error response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// InstId             char*                System assigned device name "AFG_1", "AFG_2",
// LeadText           char*                Lead diagnostic text e.g. "Routine Name" etc.
// ErrCode            int                  Error Code (- Error, + Warning)
// ErrMsg             char*                Error Message Text 
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxmlw_ErrorResponse(char *InstId, ATXMLW_INTF_RESPONSE *Response, int BufferSize, 
                           char *LeadText, int ErrCode, char *ErrMsg)
{
    int   XmlSize;

    /*
	<AtXmlResponse>
		<ErrStatus moduleName="Module Name" leadText="Lead Text" errCode="0" errText=""/>
	</AtXmlResponse>
    */
    // Determine the size of the XML string
    XmlSize = 124; // Xml stuff
    XmlSize += (InstId ? (strlen(InstId)) : 0);
    XmlSize += (LeadText ? (strlen(LeadText)) : 0);
    XmlSize += (ErrMsg ? (strlen(ErrMsg)) : 0);

    // Check if Response is big enough
    if((Response == NULL) || (BufferSize < (int)(strlen(Response) + XmlSize)))
        return;

    // Issue XML at the end of the Response Buffer
    strcat(Response,
        "<AtXmlResponse>\n"
		"  <ErrStatus moduleName=\"");
    if(InstId)
        strcat(Response,InstId);
    strcat(Response,
        "\" leadText=\"");
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
// Function: atxmlw_DebugMsg
//
// Purpose: Adds the error response to the Response XML.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// MemDbgLvl          int                  Current Member debug level
// InstId             char*                System assigned device name "AFG_1", "AFG_2",
// ReqLvl             int                  Required debug level for this debug message
// Msg                char*                Debug Message Text 
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void atxmlw_DebugMsg(int MemDbgLvl, char *InstId, int ReqLvl, char *Msg, ATXMLW_INTF_RESPONSE *Response, int BufferSize)
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
    XmlSize = 124; // Xml stuff
    XmlSize += (InstId ? (strlen(InstId)) : 0);
    XmlSize += (Msg ? (strlen(Msg)) : 0);

    // Check if Response is big enough
    if((Response == NULL) || (BufferSize < (int)(strlen(Response) + XmlSize)))
        return;

    // Issue XML at the end of the Response Buffer
    strcat(Response,
        "<AtXmlResponse>\n"
		"  <DebugMessage moduleName=\"");
    if(InstId)
        strcat(Response,InstId);
    strcat(Response,
        "\" dbgLevel=\"");
    itoa(ReqLvl,&Response[strlen(Response)],10);
    strcat(Response,
        "\" message=\"");
    if(Msg)
        strcat(Response,Msg);//FIX for double quotes
    strcat(Response,"\"></DebugMessage>\n</AtXmlResponse>\n");


    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxmlw_FmtMsg
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
char *atxmlw_FmtMsg(char *Fmt,...)
{
     va_list Args;
   // Make this a Dynamic buffer that purges each call
    static char s_Msg[1024];

    s_Msg[0] = '\0';
    // prepare to use printf type arguments
    va_start(Args,Fmt);
    // Create Message
    vsprintf(s_Msg,Fmt,Args);

    return(s_Msg);
}



