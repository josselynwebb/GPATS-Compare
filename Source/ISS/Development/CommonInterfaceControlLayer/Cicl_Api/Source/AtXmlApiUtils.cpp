//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlApiUtils.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXML Interface API Xml Utility Functions
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline Release                        
///////////////////////////////////////////////////////////////////////////////
// Includes
#include <string>
#include <windows.h>
#include "AtXmlInterfaceApiC.h"
//#include "AtXmlInterfaceApiCpp.h"

// Local Defines
typedef struct ErrMsgStruct
{
    char  ModuleName[ATXML_MAX_NAME];
    char  LeadText[ATXML_MAX_NAME];
    int   ErrorCode;
    char *Msg;
}S_ERRMSGSTRUCT;
typedef struct DbgMsgStruct
{
    char  ModuleName[ATXML_MAX_NAME];
    int   DbgLevel;
    char *Msg;
}S_DBGMSGSTRUCT;
typedef struct ErrDbgStruct
{
    int   ErrCount;
    int   NextErr;
    S_ERRMSGSTRUCT *ErrMsgs;
    int   DbgCount;
    int   NextDbg;
    S_DBGMSGSTRUCT *DbgMsgs;
}S_ERRDBGSTRUCT;

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_ParseAvailability
//
// Purpose: Parse the Availability XML into the AvailabilityArray
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// Availability      ATXML_XML_String*   Availability XML string
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// AvailabilityArray ATXML_AVAILABILITY* Availability structure returned
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int CALL_TYPE atxml_ParseAvailability(ATXML_XML_String* Availability,
                                  ATXML_AVAILABILITY* AvailabilityArray, int BufferSize)
{

	int	  Idx = 0;
	char *cptr,*cptr2;

	if((Availability == NULL) && (AvailabilityArray == NULL))
		return(-1);
	/*
		<Availability>
			<ResourceAvailability resourceName="DMM_1" availability="Available" />
		</Availability>
	*/
	memset(AvailabilityArray,0,(sizeof(ATXML_AVAILABILITY) * BufferSize));
	cptr = Availability;
	while(cptr && *cptr && (Idx < BufferSize))
	{
		if((cptr = strstr(cptr,"<ResourceAvailability")) &&
		   (cptr = strstr(cptr,"resourceName")) &&
		   (cptr2 = strstr(cptr,"availability")))
		{
			sscanf(cptr,"resourceName = \"%[^\" ]",AvailabilityArray[Idx].ResourceName);
			sscanf(cptr2,"availability = \"%[^\" ]",AvailabilityArray[Idx].Availability);
			Idx++;
		}
	}
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_ParseErrDbgResponse
//
// Purpose: Parse the Error and Debug messages in the Response buffer an place
//            in a structure for use by subsequent ErrDbg functions
//            All subsequent calls return pointers to strings that are only
//            valid between this call and atxml_ErrDbgClose()
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// Response          ATXML_XML_String*   Response XML string to parse
// Strip             bool                Flag to request removing the Err/DBG
//                                       XML from the Response Buffer
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// ErrDbgHandle     ATXML_XML_Handle*   Handle returned for use with subsequent 
//                                        ErrDbg Functions
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int CALL_TYPE atxml_ParseErrDbgResponse(ATXML_XML_String* Response,
                                             int BufferSize, BOOL Strip, ATXML_XML_Handle *ErrDbgHandle)
{
    char *cptr, *RespBegCptr, *RespEndCptr, *BegEleCptr, *EndEleCptr ;
    char  Element[ATXML_MAX_NAME];
    char  EndElement[ATXML_MAX_NAME];
    char *Msg,*EndMsg, c;
    char *TempBuf = NULL;
    int   ErrCount = 0;
    int   NextErr = 0;
    int   DbgCount = 0;
    int   NextDbg = 0;
    int   ErrIdx, DbgIdx;
    S_ERRDBGSTRUCT *ErrDbgPtr;

    // Set default no error messages via NULL Handle
    if(ErrDbgHandle)
        *ErrDbgHandle = NULL;
    else
        return(-1);//FIX Later diagnose

    // Set up left over buffer
    if(Strip)
    {
        TempBuf = new char[strlen(Response)+4];
        if(TempBuf)
            TempBuf[0] = '\0';
        else
            return(-4);//FIX Later diagnose
    }
    // Find out how many Error messages
    cptr = Response;
    while(cptr)
    {
        if((cptr = strstr(cptr,"<ErrStatus")))
        {
            cptr++;
            ErrCount++;
        }
    }
    // Find out how many Debug messages
    cptr = Response;
    while(cptr)
    {
        if((cptr = strstr(cptr,"<DebugMessage")))
        {
            cptr++;
            DbgCount++;
        }
    }
    if((ErrCount == 0) && (DbgCount == 0))
        return(0);
    // Init structure
    ErrDbgPtr = new S_ERRDBGSTRUCT;
    if(ErrDbgPtr == NULL)
        return(-2);//FIX Later Diagnose
    ErrDbgPtr->ErrCount = ErrCount;
    ErrDbgPtr->NextErr = 0;
    ErrDbgPtr->ErrMsgs = NULL;
    ErrDbgPtr->DbgCount = DbgCount;
    ErrDbgPtr->NextDbg = 0;
    ErrDbgPtr->DbgMsgs = NULL;
    if(ErrCount)
    {
        ErrDbgPtr->ErrMsgs = new S_ERRMSGSTRUCT[ErrCount];
        if(ErrDbgPtr->ErrMsgs == NULL)
            return(-3);//FIX Later diagnose
        memset(ErrDbgPtr->ErrMsgs,0,(sizeof(S_ERRMSGSTRUCT) * ErrCount));
    }
    if(DbgCount)
    {
        ErrDbgPtr->DbgMsgs = new S_DBGMSGSTRUCT[DbgCount];
        if(ErrDbgPtr->DbgMsgs == NULL)
            return(-3);//FIX Later diagnose
        memset(ErrDbgPtr->DbgMsgs,0,(sizeof(S_DBGMSGSTRUCT) * DbgCount));
    }
    *ErrDbgHandle = ErrDbgPtr;
    // scan for error and debug  messages and Place in Structure
/*
	<AtXmlResponse>
		<ErrStatus ModuleName="Module Name" LeadText="Lead Text" ErrCode="0" ErrText=""></ErrStatus>
	</AtXmlResponse>
	<AtXmlResponse>
		<DebugMessage  DbgLevel="5" ModuleName="Module Name"  Message="This is the message" />
	</AtXmlResponse>
*/
    RespBegCptr = Response;
    ErrIdx = 0;
    DbgIdx = 0;
    while(RespBegCptr)
    {
        // Are we done?
        if((RespBegCptr = strstr(RespBegCptr,"<AtXmlResponse")) == NULL)
            break;
        if((RespEndCptr = strstr(RespBegCptr,"</AtXmlResponse")) == NULL)
            break;
        BegEleCptr = RespBegCptr;
        while(BegEleCptr)
        {
            // Get next element
            BegEleCptr = strstr(++BegEleCptr,"<");
            if((BegEleCptr == NULL) || (BegEleCptr >= RespEndCptr))
                break;
            if(BegEleCptr[1] == '/')
                continue;
            if(sscanf(BegEleCptr, "< %[^> ]",Element) <= 0)
            {
                BegEleCptr++;
                continue;
            }
            Msg = "";
            EndMsg = NULL;
            // Is this an ErrStatus block?
		    //<ErrStatus ModuleName="Module Name" LeadText="Lead Text" ErrCode="0" ErrText=""></ErrStatus>
            if(strcmp(Element,"ErrStatus") == 0)
            {
                // find end of element
                EndEleCptr = strstr(BegEleCptr,"</ErrStatus");
/* Add back in when double quotes and > are mapped
                cptr = strstr(BegEleCptr,"/>");
                if(EndEleCptr == NULL)
                    EndEleCptr = cptr;
                else if((cptr) && (EndEleCptr > cptr))
                    EndEleCptr = cptr;
*/
                if(EndEleCptr == NULL)
                    break;//?????
                if((cptr = strstr(BegEleCptr,"moduleName")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"moduleName = \" %55[^\"]",
                        ErrDbgPtr->ErrMsgs[ErrIdx].ModuleName);
                    ErrDbgPtr->ErrMsgs[ErrIdx].ModuleName[ATXML_MAX_NAME-1] = '\0';
                }
                if((cptr = strstr(BegEleCptr,"leadText")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"leadText = \" %55[^\"]",
                        ErrDbgPtr->ErrMsgs[ErrIdx].LeadText);
                    ErrDbgPtr->ErrMsgs[ErrIdx].LeadText[ATXML_MAX_NAME-1] = '\0';
                }
                if((cptr = strstr(BegEleCptr,"errCode")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"errCode = \" %d",&(ErrDbgPtr->ErrMsgs[ErrIdx].ErrorCode));
                }
                if((cptr = strstr(BegEleCptr,"errText")) &&
                    (cptr < EndEleCptr))
                {
                    if((cptr = strstr(cptr,"\"")) &&
                    (cptr < EndEleCptr))
                    {
                        Msg = cptr+1;
                        EndMsg = strstr((Msg),"\"></ErrStatus>");
                    }
                }
                BegEleCptr = EndEleCptr + 1;
                if(EndMsg)
                {
                    *EndMsg = '\0';
                    ErrDbgPtr->ErrMsgs[ErrIdx].Msg = new char[strlen(Msg)+4];
                    strcpy(ErrDbgPtr->ErrMsgs[ErrIdx].Msg,Msg);
                    *EndMsg = '"';
                }
                ErrIdx++;
                continue;
            }
            // Is this a DebugMessage block?
 		    //<DebugMessage  DbgLevel="5" ModuleName="Module Name"  Message="This is the message" />
            else if(strcmp(Element,"DebugMessage") == 0)
            {
                // find end of element
                EndEleCptr = strstr(BegEleCptr,"</DebugMessage");
/* Add back in when double quotes and > are mapped
                cptr = strstr(BegEleCptr,"/>");
                if(EndEleCptr == NULL)
                    EndEleCptr = cptr;
                else if((cptr) && (EndEleCptr > cptr))
                    EndEleCptr = cptr;
*/
                if(EndEleCptr == NULL)
                    break;//?????
                if((cptr = strstr(BegEleCptr,"moduleName")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"moduleName = \" %[^\"]",
                        ErrDbgPtr->DbgMsgs[DbgIdx].ModuleName);
                    ErrDbgPtr->DbgMsgs[DbgIdx].ModuleName[ATXML_MAX_NAME-1] = '\0';
                }
                if((cptr = strstr(BegEleCptr,"dbgLevel")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"dbgLevel = \" %d",&(ErrDbgPtr->DbgMsgs[DbgIdx].DbgLevel));
                }
                if((cptr = strstr(BegEleCptr+10,"message")) &&
                    (cptr < EndEleCptr))
                {
                    if((cptr = strstr(cptr,"\"")) &&
                    (cptr < EndEleCptr))
                    {
                        Msg = cptr+1;
                        EndMsg = strstr((Msg),"\"></DebugMessage>");
                    }
                }
                if(EndMsg)
                {
                    *EndMsg = '\0';
                    ErrDbgPtr->DbgMsgs[DbgIdx].Msg = new char[strlen(Msg)+4];
                    strcpy(ErrDbgPtr->DbgMsgs[DbgIdx].Msg,Msg);
                    *EndMsg = '"';
                }
                BegEleCptr = EndEleCptr + 1;
                DbgIdx++;
                continue;
            }
            // Is this Something else - save in TempBuf
            else if(Strip)
            {
                // find end of single element entries
                if(strcmp(Element,"ReturnData") == 0)
                {
                    // find end of element
                    sprintf(EndElement,"</%s",Element);
                    EndEleCptr = strstr(BegEleCptr,EndElement);
                    cptr = strstr(BegEleCptr,"/>");
                    if((EndEleCptr == NULL) && cptr)
                        EndEleCptr = cptr;
                    if(EndEleCptr == NULL)
                        break;//?????
                    EndEleCptr = strstr(EndEleCptr,">");
                    if(EndEleCptr == NULL)
                        break;//?????
                }
                // find end of multiple element entries
                if(strcmp(Element,"Availability") == 0)
                {
                    // find end of element
                    sprintf(EndElement,"</%s",Element);
                    EndEleCptr = strstr(BegEleCptr,EndElement);
                    if(EndEleCptr == NULL)
                        break;//?????
                    EndEleCptr = strstr(EndEleCptr,">");
                    if((EndEleCptr == NULL) || (EndEleCptr >= RespEndCptr))
                        break;//?????
                }
                //FIX maybe - a bit tacky but include AtXmlResponse on each entry
                // check if enough room
                if(BufferSize > (40 + ((EndEleCptr - BegEleCptr) + 2) + (int)strlen(TempBuf)))
                {
                    strcat(TempBuf,"<AtXmlResponse>\n  ");
                    c = EndEleCptr[1];
                    EndEleCptr[1] = '\0';
                    strcat(TempBuf,BegEleCptr);
                    EndEleCptr[1] = c;
                    strcat(TempBuf,"\n</AtXmlResponse>\n");
                }
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        RespBegCptr = strstr(++RespBegCptr,"<AtXmlResponse");
        if(RespBegCptr)
            RespEndCptr = strstr(RespBegCptr,"</AtXmlResponse");
    }
    if(Strip)
    {
        strcpy(Response,TempBuf);
        delete(TempBuf);
    }
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_ErrDbgNextError
//
// Purpose: Return the data for the next error message.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// ErrDbgHandle     ATXML_XML_Handle     Handle used to extract next error 
//                                       XML from the Response Buffer
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// ModuleName       char **             Return a pointer to the module name string
// LeadText         char **             Return a pointer to the lead text string
// ErrorCode        int *               Pointer where to return the ErrorCode
// Message          char **             Return a pointer to the Message String
//
// Return:
//    True - error message found
//    False - no more error messages
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC bool CALL_TYPE atxml_ErrDbgNextError(ATXML_XML_Handle ErrDbgHandle,
                                             char **ModuleName, char **LeadText,
                                             int *ErrorCode, char **Message)
{
    S_ERRDBGSTRUCT *ErrDbgPtr;
    S_ERRMSGSTRUCT *ErrPtr;

    ErrDbgPtr = (S_ERRDBGSTRUCT*)ErrDbgHandle;
    ErrPtr = ErrDbgPtr->ErrMsgs;
    if((ErrDbgPtr == NULL) || (ErrPtr == NULL) ||
       (ErrDbgPtr->NextErr >= ErrDbgPtr->ErrCount))
       return(false);
    ErrPtr = &(ErrDbgPtr->ErrMsgs[ErrDbgPtr->NextErr]);
    if(ModuleName)
        *ModuleName = ErrPtr->ModuleName;
    if(LeadText)
        *LeadText = ErrPtr->LeadText;
    if(ErrorCode)
        *ErrorCode = ErrPtr->ErrorCode;
    if(Message)
        *Message = ErrPtr->Msg;

    ErrDbgPtr->NextErr++;

    return(true);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_ErrDbgNextDebug
//
// Purpose: Return the data for the next debug message.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// ErrDbgHandle     ATXML_XML_Handle     Handle used to extract next error 
//                                       XML from the Response Buffer
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// ModuleName       char **             Return a pointer to the module name string
// DbgLevel         int *               Pointer where to return the Debug Level
// Message          char **             Return a pointer to the Message String
//
// Return:
//    True - debug message found
//    False - no more debug messages
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC bool CALL_TYPE atxml_ErrDbgNextDebug(ATXML_XML_Handle ErrDbgHandle,
                                             char **ModuleName, int *DbgLevel,
                                             char **Message)
{
    S_ERRDBGSTRUCT *ErrDbgPtr;
    S_DBGMSGSTRUCT *DbgPtr;

    ErrDbgPtr = (S_ERRDBGSTRUCT*)ErrDbgHandle;
    DbgPtr = ErrDbgPtr->DbgMsgs;
    if((ErrDbgPtr == NULL) || (DbgPtr == NULL) ||
       (ErrDbgPtr->NextDbg >= ErrDbgPtr->DbgCount))
       return(false);
    DbgPtr = &(ErrDbgPtr->DbgMsgs[ErrDbgPtr->NextDbg]);
    if(ModuleName)
        *ModuleName = DbgPtr->ModuleName;
    if(DbgLevel)
        *DbgLevel = DbgPtr->DbgLevel;
    if(Message)
        *Message = DbgPtr->Msg;

    ErrDbgPtr->NextDbg++;

    return(true);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXML_FNC atxml_ErrDbgClose
//
// Purpose: Release all memory used for ErrDbg utilities.
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// ErrDbgHandle     ATXML_XML_Handle     Handle used to extract next error 
//                                       XML from the Response Buffer
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC void CALL_TYPE atxml_ErrDbgClose(ATXML_XML_Handle ErrDbgHandle)
{
    int i;
    S_ERRDBGSTRUCT *ErrDbgPtr;
    S_ERRMSGSTRUCT *ErrPtr;
    S_DBGMSGSTRUCT *DbgPtr;

    if(ErrDbgHandle == NULL)
        return;
    ErrDbgPtr = (S_ERRDBGSTRUCT*)ErrDbgHandle;
    ErrPtr = ErrDbgPtr->ErrMsgs;
    DbgPtr = ErrDbgPtr->DbgMsgs;

    // Delete Error messages strings and struct buffer
    if(ErrDbgPtr->ErrCount && ErrPtr)
    {
        for(i=0;i<ErrDbgPtr->ErrCount;i++)
        {
            if(ErrPtr->Msg)
                delete(ErrPtr->Msg);
            ErrPtr++;
        }
        delete(ErrDbgPtr->ErrMsgs);
    }
    // Delete Debug messages strings and struct buffer
    if(ErrDbgPtr->DbgCount && DbgPtr)
    {
        for(i=0;i<ErrDbgPtr->DbgCount;i++)
        {
            if(DbgPtr->Msg)
                delete(DbgPtr->Msg);
            DbgPtr++;
        }
        delete(ErrDbgPtr->DbgMsgs);
    }
    delete(ErrDbgPtr);
    
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: atxml_GetSingleIntValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:integer" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// Response          char*           Response ReturnData sequence for double datum
// Attr              char*           Attribute being fetched
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValuePtr         int*            Returned value
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int  CALL_TYPE atxml_GetSingleIntValue(ATXML_XML_String* Response, char *Attr, int *ValuePtr)
{
    char *cptr, *BegEleCptr, *EndEleCptr;
    char LclAttribute[ATXML_MAX_NAME];
    char cValuePtr[ATXML_MAX_NAME];
/*
	    "<AtXmlResponse>\n  "
	    "	<ReturnData>\n"
	    "		<ValuePair>\n"
	    "			<Attribute>%s</Attribute>\n"
	    "			<Value>\n"
	    "				<c:Datum xsi:type="c:integer" unit="V" value="8"/>\n"
	    "			</Value>\n"
	    "		</ValuePair>\n"
	    "	</ReturnData>\n"
	    "</AtXmlResponse>\n",lclAttribute,lclUnit,Value);
*/
    BegEleCptr = Response;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        LclAttribute[0] = '\0';
        cValuePtr[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",LclAttribute);
            if((Attr) && (strcmp(LclAttribute, Attr) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if((cptr = strstr(BegEleCptr,"<c:Datum")) &&
           (cptr = strstr(cptr,"value")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"value = \"%i",ValuePtr);
        }
        BegEleCptr = EndEleCptr;
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxml_GetIntArrayValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:integer" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// Response          char*           Response ReturnData sequence for double datum
// Attr              char*           Attribute being fetched
// ArraySize         int*            Max size of array
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValuePtr         int*            Returned values
// ArraySize        int*            Size of array returned
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int  CALL_TYPE atxml_GetIntArrayValue(ATXML_XML_String* Response, char *Attr,
                                                              int *ValuePtr, int *ArraySize)
{
    char *cptr, *cptr2, *BegEleCptr, *EndEleCptr;
    char Attribute[ATXML_MAX_NAME];
    char cIntValue[ATXML_MAX_NAME];
    int  i,Idx, MaxIdx, Size;

/*
	    <AtXmlResponse>
	    	<ReturnData>
	    		<ValuePair>
	    			<Attribute>%s</Attribute>
	    			<Value>
                        <c:IndexedArray xsi:type="c:integerArray" unit="V" dimensions="[2]">
	                        <c:Element value="0xffff" position="[0]"/>
	                        <c:Element value="0x10105050" position="[2]"/>
                        </c:IndexedArray>
	    			</Value>
	    		</ValuePair>
			    <ValuePair>
				    <Attribute>%s</Attribute>
				    <IntegerArray  unit="V" arrayValues="[1, 2, 3]"></IntegerArray>
			    </ValuePair>
	    	</ReturnData>
	    </AtXmlResponse>
*/
    BegEleCptr = Response;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        Attribute[0] = '\0';
        cIntValue[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",Attribute);
            if((Attr) && (strcmp(Attribute, Attr) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if(ArraySize)
            Size = *ArraySize;
        else
        {
            //Fix Diagnose bad call
            return(-1);
        }
        MaxIdx = 0;
        // Get ATML Common Indexed Array
        if((cptr = strstr(BegEleCptr,"<c:IndexedArray")) &&
           (cptr = strstr(cptr,"\"c:integerArray")) &&
           (cptr = strstr(cptr,"<c:Element")) &&
            (cptr < EndEleCptr))
        {
            for(i=0;i<Size && cptr;i++)
            {
                Idx = i;
                cptr2 = strstr(cptr,"position=");
                if(cptr2)
                    sscanf(cptr2,"position = \" [ %d",&Idx);
                if(Idx > MaxIdx)
                    MaxIdx = Idx;
                ValuePtr[Idx] = 0;
                cptr2 = strstr(cptr,"value");
                if(cptr2)
                    sscanf(cptr2,"value = \"%i",&ValuePtr[Idx]);
                if((cptr = strstr(++cptr,"<c:Element")) &&
                   (cptr < EndEleCptr))
                    continue;
                else
                    break;
            }
            break;
        }
        // Get 1641 style array
        if((cptr = strstr(BegEleCptr,"<IntegerArray")) &&
           (cptr = strstr(cptr,"arrayValues")) &&
           (cptr = strstr(cptr,"\"")) &&
            (cptr < EndEleCptr))
        {
            // skip beinning '[ if present
            if((cptr2 = strstr(cptr,"[")) && (cptr2 < EndEleCptr))
                cptr = ++cptr2;
            // Get values
            for(i=0;i<Size && cptr;i++)
            {
                sscanf(cptr,"%i",&ValuePtr[i]);
                if((cptr = strstr(cptr,",")) &&
                   (++cptr < EndEleCptr))
                    continue;
                else
                    break;
            }
            MaxIdx = i;
        }
        BegEleCptr = EndEleCptr;
        *ArraySize = MaxIdx+1;
    }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: atxml_GetSingleDblValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:double" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// Response          char*           Response ReturnData sequence for double datum
// Attr              char*           Attribute being fetched
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValuePtr         double*         Returned value
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int  CALL_TYPE atxml_GetSingleDblValue(ATXML_XML_String* Response,
                                                            char *Attr, double *ValuePtr)
{
    char *cptr, *BegEleCptr, *EndEleCptr;
    char LclAttribute[ATXML_MAX_NAME];
    char cValuePtr[ATXML_MAX_NAME];
/*
	    "<AtXmlResponse>\n  "
	    "	<ReturnData>\n"
	    "		<ValuePair>\n"
	    "			<Attribute>%s</Attribute>\n"
	    "			<Value>\n"
	    "				<c:Datum xsi:type="c:double" unit="V" value="8"/>\n"
	    "			</Value>\n"
	    "		</ValuePair>\n"
	    "	</ReturnData>\n"
	    "</AtXmlResponse>\n",lclAttribute,lclUnit,Value);
*/
    BegEleCptr = Response;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        LclAttribute[0] = '\0';
        cValuePtr[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",LclAttribute);
            if((Attr) && (strcmp(LclAttribute, Attr) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if((cptr = strstr(BegEleCptr,"<c:Datum")) &&
           (cptr = strstr(cptr,"value")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"value = \"%lf",ValuePtr);
        }
        BegEleCptr = EndEleCptr;
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxml_GetDblArrayValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:double" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// Response          char*           Response ReturnData sequence for double datum
// Attr              char*           Attribute being fetched
// ArraySize         int*            Max size of array
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValuePtr         double*         Returned values
// ArraySize        int*            Size of array returned
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int  CALL_TYPE atxml_GetDblArrayValue(ATXML_XML_String* Response, char *Attr,
                                                              double *ValuePtr, int *ArraySize)
{
    char *cptr, *cptr2, *BegEleCptr, *EndEleCptr;
    char LclAttribute[ATXML_MAX_NAME];
    char cValuePtr[ATXML_MAX_NAME];
    int  i,Idx, MaxIdx, Size;

/*
	    <AtXmlResponse>
	    	<ReturnData>
	    		<ValuePair>
	    			<Attribute>%s</Attribute>
	    			<Value>
                        <c:IndexedArray xsi:type="c:doubleArray" unit="V" dimensions="[2]">
	                        <c:Element value="10.3" position="[0]"/>
	                        <c:Element value="10.4" position="[2]"/>
                        </c:IndexedArray>
	    			</Value>
	    		</ValuePair>
			    <ValuePair>
				    <Attribute>xxx1641</Attribute>
				    <RealArray  unit="V" arrayValues="[10.3, 10.4]"></RealArray>
			    </ValuePair>
	    	</ReturnData>
	    </AtXmlResponse>
*/
    BegEleCptr = Response;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        LclAttribute[0] = '\0';
        cValuePtr[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",LclAttribute);
            if((Attr) && (strcmp(LclAttribute, Attr) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if(ArraySize)
            Size = *ArraySize;
        else
        {
            //Fix Diagnose bad call
            return(-1);
        }
        MaxIdx = 0;
        // Get ATML Common Indexed Array
        if((cptr = strstr(BegEleCptr,"<c:IndexedArray")) &&
           (cptr = strstr(cptr,"\"c:doubleArray")) &&
           (cptr = strstr(cptr,"<c:Element")) &&
            (cptr < EndEleCptr))
        {
            for(i=0;i<Size && cptr;i++)
            {
                Idx = i;
                cptr2 = strstr(cptr,"position=");
                if(cptr2)
                    sscanf(cptr2,"position = \" [ %d",&Idx);
                if(Idx > MaxIdx)
                    MaxIdx = Idx;
                ValuePtr[Idx] = 0.0;
                cptr2 = strstr(cptr,"value");
                if(cptr2)
                    sscanf(cptr2,"value = \"%lf",&ValuePtr[Idx]);
                if((cptr = strstr(++cptr,"<c:Element")) &&
                   (cptr < EndEleCptr))
                    continue;
                else
                    break;
            }
        }
        // Get 1641 style array
        if((cptr = strstr(BegEleCptr,"<RealArray")) &&
           (cptr = strstr(cptr,"arrayValues")) &&
           (cptr = strstr(cptr,"\"")) &&
            (cptr < EndEleCptr))
        {
            // skip beinning '[ if present
            if((cptr2 = strstr(cptr,"[")) && (cptr2 < EndEleCptr))
                cptr = ++cptr2;
            // Get values
            for(i=0;i<Size && cptr;i++)
            {
                sscanf(cptr,"%lf",&ValuePtr[i]);
                if((cptr = strstr(cptr,",")) &&
                   (++cptr < EndEleCptr))
                    continue;
                else
                    break;
            }
            MaxIdx = i;
        }
        BegEleCptr = EndEleCptr;
        *ArraySize = MaxIdx+1;
        BegEleCptr = EndEleCptr;
        *ArraySize = MaxIdx+1;
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: atxml_GetFltArrayValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:double" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// Response          char*           Response ReturnData sequence for double datum
// Attr              char*           Attribute being fetched
// ArraySize         int*            Max size of array
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValuePtr         double*         Returned values
// ArraySize        int*            Size of array returned
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int  CALL_TYPE atxml_GetFltArrayValue(ATXML_XML_String* Response, char *Attr,
                                                              float *ValuePtr, int *ArraySize)
{
    char *cptr, *cptr2, *BegEleCptr, *EndEleCptr;
    char LclAttribute[ATXML_MAX_NAME];
    char cValuePtr[ATXML_MAX_NAME];
    int  i,Idx, MaxIdx, Size;

/*
	    <AtXmlResponse>
	    	<ReturnData>
	    		<ValuePair>
	    			<Attribute>%s</Attribute>
	    			<Value>
                        <c:IndexedArray xsi:type="c:doubleArray" unit="V" dimensions="[2]">
	                        <c:Element value="10.3" position="[0]"/>
	                        <c:Element value="10.4" position="[2]"/>
                        </c:IndexedArray>
	    			</Value>
	    		</ValuePair>
			    <ValuePair>
				    <Attribute>xxx1641</Attribute>
				    <RealArray  unit="V" arrayValues="[10.3, 10.4]"></RealArray>
			    </ValuePair>
	    	</ReturnData>
	    </AtXmlResponse>
*/
    BegEleCptr = Response;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        LclAttribute[0] = '\0';
        cValuePtr[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",LclAttribute);
            if((Attr) && (strcmp(LclAttribute, Attr) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if(ArraySize)
            Size = *ArraySize;
        else
        {
            //Fix Diagnose bad call
            return(-1);
        }
        MaxIdx = 0;
        // Get ATML Common Indexed Array
        if((cptr = strstr(BegEleCptr,"<c:IndexedArray")) &&
           (cptr = strstr(cptr,"\"c:doubleArray")) &&
           (cptr = strstr(cptr,"<c:Element")) &&
            (cptr < EndEleCptr))
        {
            for(i=0;i<Size && cptr;i++)
            {
                Idx = i;
                cptr2 = strstr(cptr,"position=");
                if(cptr2)
                    sscanf(cptr2,"position = \" [ %d",&Idx);
                if(Idx > MaxIdx)
                    MaxIdx = Idx;
                ValuePtr[Idx] = 0.0;
                cptr2 = strstr(cptr,"value");
                if(cptr2)
                    sscanf(cptr2,"value = \"%f",&ValuePtr[Idx]);
                if((cptr = strstr(++cptr,"<c:Element")) &&
                   (cptr < EndEleCptr))
                    continue;
                else
                    break;
            }
        }
        // Get 1641 style array
        if((cptr = strstr(BegEleCptr,"<RealArray")) &&
           (cptr = strstr(cptr,"arrayValues")) &&
           (cptr = strstr(cptr,"\"")) &&
            (cptr < EndEleCptr))
        {
            // skip beinning '[ if present
            if((cptr2 = strstr(cptr,"[")) && (cptr2 < EndEleCptr))
                cptr = ++cptr2;
            // Get values
            for(i=0;i<Size && cptr;i++)
            {
                sscanf(cptr,"%f",&ValuePtr[i]);
                if((cptr = strstr(cptr,",")) &&
                   (++cptr < EndEleCptr))
                    continue;
                else
                    break;
            }
            MaxIdx = i;
        }
        BegEleCptr = EndEleCptr;
        *ArraySize = MaxIdx+1;
        BegEleCptr = EndEleCptr;
        *ArraySize = MaxIdx+1;
    }

    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: cs_GetStringValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:string" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// Response  char*           Response ReturnData sequence for double datum
// Attr          char*           Attribute being fetched
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValuePtr        double*         Returned value
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
extern ATXML_FNC int  CALL_TYPE atxml_GetStringValue(ATXML_XML_String* Response,
                                              char *Attr, char *ValuePtr, int BufferSize)
{
    char *cptr, *cptr2, c, *BegEleCptr, *EndEleCptr;
    char LclAttribute[50];
    char cValuePtr[50];
    int  Len;
/*
	    "<AtXmlResponse>\n  "
	    "	<ReturnData>\n"
	    "		<ValuePair>\n"
	    "			<Attribute>%s</Attribute>\n"
	    "			<Value>\n"
	    "				<c:Datum xsi:type="c:string" unit="V"> <c:Value>xxxx</c:Value></c:Datum>\n"
	    "			</Value>\n"
	    "		</ValuePair>\n"
	    "	</ReturnData>\n"
	    "</AtXmlResponse>\n",lclAttribute,lclUnit,Value);
*/
    BegEleCptr = Response;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        LclAttribute[0] = '\0';
        cValuePtr[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",LclAttribute);
            if((Attr) && (strcmp(LclAttribute, Attr) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if((cptr = strstr(cptr,"<c:Datum")) &&
            (cptr = strstr(cptr,"<c:Value>")) &&
            (cptr2 = strstr(cptr,"</c:Value>")) &&
            (cptr2 < EndEleCptr))
        {
            cptr += 9;
            Len = cptr2 - cptr;
            if(Len >= BufferSize)// Account for zero terminator
                Len = BufferSize - 1;
            c = cptr[Len];
            cptr[Len] = '\0';
            strcpy(ValuePtr,cptr);
            cptr[Len] = c;
        }
        BegEleCptr = EndEleCptr;
    }

    return(0);
}


 
