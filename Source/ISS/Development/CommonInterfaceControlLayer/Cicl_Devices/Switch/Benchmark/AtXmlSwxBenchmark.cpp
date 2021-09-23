// AtXmlSwxBenchmark.cpp : Defines the entry point for the console application.
//

#include "windows.h"
#include "AtXmlInterfaceApiC.h"
extern int cs_ParseResponse(char *Response);

int main(int argc, char* argv[])
{
    int Status;
    char Response[4096];
    char XmlBuf[4096];

	printf("Call atxml_Initialize()\n");
    Status = atxml_Initialize();
    printf("atxml_Initialise() = %d\n",Status);

    // Create a single test requirement XML Snippet
    sprintf(XmlBuf,
        "<AtXmlTestRequirements> "
		"    <ResourceRequirement> "
		"	    <ResourceType>Source</ResourceType> "
		"	    <SignalResourceName>PAWS_SWITCH</SignalResourceName> "
		"    </ResourceRequirement> "
    	"</AtXmlTestRequirements>"
        );

    // Query system for availability
    Status = atxml_ValidateRequirements(XmlBuf, "PawsAllocation.xml",
                             Response, 4096);
    printf("atxml_ValidateRequirements = %d\n  Response:\n",Status);
    cs_ParseResponse(Response);

/**/
    // Issue Setup Signal test
	printf("Call atxml_IssueSignal(Setup)\n");
    sprintf(XmlBuf,
		"<AtXmlSignalDescription>\n"
		"	<SignalAction>Connect</SignalAction>\n"
        "   <SignalResourceName>PAWS_SWITCH</SignalResourceName>\n"
		"	<SignalSnippet>\n"
	    "       <Signal Out=\"SHORTx\">\n"
		"           <Connection name=\"SHORTx\" path=\"1,1000,0\"/>\n"
	    "       </Signal>\n"
        "   </SignalSnippet>\n"
		"</AtXmlSignalDescription>\n"
        );
    Status = atxml_IssueSignal(XmlBuf, Response, 4096);
    printf("atxml_IssueSignal = %d\n  Response:\n",Status);
    cs_ParseResponse(Response);
/**/


    getchar();
	return 0;
}

int cs_ParseResponse(char *Response)
{
    char *cptr, *RespBegCptr, *RespEndCptr, *BegEleCptr, *EndEleCptr ;
    char  Element[56];
    char  ModuleName[56];
    char  LeadText[56];
    int   ErrorCode, DbgLvl;
    char *Msg,*EndMsg;

    // scan for error and debug messages and issue to WRTS
/*
	<AtXmlResponse>
		<ErrStatus ModuleName="Module Name" LeadText="Lead Text" ErrCode="0" ErrText=""></ErrStatus>
	</AtXmlResponse>
	<AtXmlResponse>
		<DebugMessage  DbgLevel="5" ModuleName="Module Name"  Message="This is the message" />
	</AtXmlResponse>
*/
    RespBegCptr = Response;
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
            ModuleName[0] = '\0';
            LeadText[0] = '\0';
            ErrorCode = 0;
            DbgLvl = 0;
            Msg = "";
            EndMsg = NULL;
            // Is this an ErrStatus block?
		    //<ErrStatus ModuleName="Module Name" LeadText="Lead Text" ErrCode="0" ErrText=""></ErrStatus>
            if(strcmp(Element,"ErrStatus") == 0)
            {
                // find end of element
                EndEleCptr = strstr(BegEleCptr,"</ErrStatus");
                cptr = strstr(BegEleCptr,"/>");
                if(EndEleCptr == NULL)
                    EndEleCptr = cptr;
                else if((cptr) && (EndEleCptr > cptr))
                    EndEleCptr = cptr;
                if(EndEleCptr == NULL)
                    break;//?????
                if((cptr = strstr(BegEleCptr,"moduleName")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"moduleName = \" %[^\"]",ModuleName);
                }
                if((cptr = strstr(BegEleCptr,"LeadText")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"leadText = \" %[^\"]",LeadText);
                }
                if((cptr = strstr(BegEleCptr,"errCode")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"errCode = \" %d",&ErrorCode);
                }
                if((cptr = strstr(BegEleCptr,"errText")) &&
                    (cptr < EndEleCptr))
                {
                    if((cptr = strstr(cptr,"\"")) &&
                    (cptr < EndEleCptr))
                    {
                        Msg = cptr+1;
                        EndMsg = strstr((Msg),"\"");
                    }
                }
                // Issue Error Message
                if(EndMsg)
                    *EndMsg = '\0';
                printf("  <ErrStatus ... %s - %s\n",ModuleName,LeadText);
                if(EndMsg)
                    *EndMsg = '"';
                BegEleCptr = EndEleCptr + 1;
                continue;
            }
            // Is this a DebugMessage block?
 		    //<DebugMessage  DbgLevel="5" ModuleName="Module Name"  Message="This is the message" />
            else if(strcmp(Element,"DebugMessage") == 0)
            {
                // find end of element
                EndEleCptr = strstr(BegEleCptr,"</DebugMessage");
                cptr = strstr(BegEleCptr,"/>");
                if(EndEleCptr == NULL)
                    EndEleCptr = cptr;
                else if((cptr) && (EndEleCptr > cptr))
                    EndEleCptr = cptr;
                if(EndEleCptr == NULL)
                    break;//?????
                if((cptr = strstr(BegEleCptr,"moduleName")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"moduleName = \" %[^\"]",ModuleName);
                }
                if((cptr = strstr(BegEleCptr,"dbgLevel")) &&
                    (cptr < EndEleCptr))
                {
                    sscanf(cptr,"dbgLevel = \" %d",&DbgLvl);
                }
                if((cptr = strstr(BegEleCptr+10,"message")) &&
                    (cptr < EndEleCptr))
                {
                    if((cptr = strstr(cptr,"\"")) &&
                    (cptr < EndEleCptr))
                    {
                        Msg = cptr+1;
                        EndMsg = strstr((Msg),"\"");
                    }
                }
                if(EndMsg)
                    *EndMsg = '\0';
                printf("  <DebugMessage %s [%s]\n",ModuleName,Msg);
                if(EndMsg)
                    *EndMsg = '"';
                BegEleCptr = EndEleCptr + 1;
                continue;
            }
            // Is this Something else - save in ValueResponse
            else 
            {
                // find end of single element entries
                if(strcmp(Element,"ReturnData") == 0)
                {
                    // find end of element
                    EndEleCptr = strstr(BegEleCptr,"</ReturnData");
                    cptr = strstr(BegEleCptr,"/>");
                    if(EndEleCptr == NULL)
                        EndEleCptr = cptr;
                    else if((cptr) && (EndEleCptr > cptr))
                        EndEleCptr = cptr;
                    if(EndEleCptr == NULL)
                        break;//?????
                    EndEleCptr = strstr(EndEleCptr,">");
                    if(EndEleCptr == NULL)
                        break;//?????
                }
                // find end of multiple element entries
                else if(strcmp(Element,"Availability") == 0)
                {
                    // find end of element
                    EndEleCptr = strstr(BegEleCptr,"</Availability");
                    if(EndEleCptr == NULL)
                        break;//?????
                    EndEleCptr = strstr(EndEleCptr,">");
                    if((EndEleCptr == NULL) || (EndEleCptr >= RespEndCptr))
                        break;//?????
                }
                else
                    continue;
                printf("  <%s ...\n", Element);
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        RespBegCptr = strstr(++RespBegCptr,"<AtXmlResponse");
        if(RespBegCptr)
            RespEndCptr = strstr(RespBegCptr,"</AtXmlResponse");
    }
    return(0);
}
