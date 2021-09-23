// AtXmlApiBenchmark.cpp : Defines the entry point for the console application.
//

#include "windows.h"
#include "AtXmlInterfaceApiC.h"
static int s_ParseValDataResponse(char *Response, int BufferSize);
static int s_ParseErrDbgResponse(char *Response, int BufferSize);

int main(int argc, char* argv[])
{
    int Status;
    char Response[4096];
    char XmlBuf[4096];
//    int  Count;

	printf("Call atxml_Initialize()\n");
    Status = atxml_Initialize("PAWS_WRTS","U2");
    printf("atxml_Initialise() = %d\n",Status);

    // Create a single test requirement XML Snippet
    sprintf(XmlBuf,
        "<AtXmlTestRequirements> "
		"    <ResourceRequirement> "
		"	    <ResourceType>Source</ResourceType> "
		"	    <SignalResourceName>DevX_1</SignalResourceName> "
		"    </ResourceRequirement> "
    	"</AtXmlTestRequirements>"
        );

    // Query system for availability
    Status = atxml_ValidateRequirements(XmlBuf, "PawsAllocation.xml",
                             Response, 4096);
    printf("atxml_ValidateRequirements = %d\n  Response:\n",Status);
    s_ParseErrDbgResponse(Response, 4096);
    s_ParseValDataResponse(Response, 4096);

/**/
    // Issue Setup Signal test
	printf("Call atxml_IssueSignal(Setup)\n");
    sprintf(XmlBuf,
	  "<AtXmlSignalDescription xmlns:atxml=\"ATXML_TSF\">\n"
	  "	<SignalAction>Setup</SignalAction>\n"
	  "	<SignalTimeOut>5.1 s</SignalTimeOut>\n"
      " <SignalResourceName>DevX_1</SignalResourceName>\n"
	  " <SignalSnippet>\n"
        "<Signal name=\"\" Out=\"Meas\" In=\"Cha Ext Chb\">\n"
            "<Load name=\"TEST_EQUIP_IMP\" resistance=\"50 Ohm\" In=\"Cha\" />\n" 
            "<Constant name=\"dc_offset\" amplitude=\"Pk-Pk 1.5 V+-10.3k%\" />\n" 
            "<Sinusoid name=\"ac_comp\" amplitude=\"2.5 V\" frequency=\"3.5kHz\" />\n" 
            "<atxml:Port name=\"Cha\"/>\n"
            "<Instantaneous name=\"Gate_Start\" nominal=\"3V\" condition=\"GT\" In=\"Cha\" />\n" 
            "<Instantaneous name=\"Gate_Stop\" nominal=\"2V\" condition=\"LT\" In=\"Cha\" />\n" 
            "<Instantaneous name=\"Trigger_Start\" nominal=\"1.5\" condition=\"GT\" In=\"Ext\" />\n" 
            "<HighPass name=\"AC_COUPLE\" cutoff=\"0\" In=\"TEST_EQUIP_IMP\" />\n" 
            "<EventedEvent name=\"EventedEvent7\" In=\"Gate_Start Gate_Stop\" />\n" 
            "<SignalDelay name=\"Trigger_Delay\" delay=\"2s\" In=\"Trigger_Start\" />\n" 
            "<Attenuator name=\"Attenuate\" gain=\"0\" In=\"AC_COUPLE\" />\n" 
            "<Sum name=\"Sum23\" In=\"dc_offset ac_comp\" />\n" 
            "<Instantaneous name=\"Meas\" As=\"Sum23\" Gate=\"EventedEvent7\" Sync=\"Trigger_Delay\" In=\"Attenuate\" attribute=\"Generic\"/>\n" 
        "</Signal>\n"
      "   </SignalSnippet>\n"
      "</AtXmlSignalDescription>\n"
      );
    Status = atxml_IssueSignal(XmlBuf, Response, 4096);
    printf("atxml_IssueSignal = %d\n  Response:\n",Status);
    s_ParseErrDbgResponse(Response, 4096);
    s_ParseValDataResponse(Response, 4096);
/**/
/*
    // Issue Driver Function test
	printf("Call atxml_IssueDriverFunctionCall(viIn8)\n");
    sprintf(XmlBuf,
    "<AtXmlDriverFunctionCall>\n"
    "  <SignalResourceName>DevX_1</SignalResourceName>\n"
    "  <DriverFunctionCall>\n"
    "    <FunctionName>TestParam</FunctionName>\n"
    "    <Parameter   ParamNumber=\"0\" ParamType=\"RetInt32\"/>\n"
    "    <Parameter   ParamNumber=\"1\" ParamType=\"Handle\" Value=\"5\"/>\n"
    "    <Parameter   ParamNumber=\"2\" ParamType=\"SrcUInt16\" Value=\"0x3B05\"/>\n"
    "    <Parameter   ParamNumber=\"3\" ParamType=\"SrcDblPtr\" Value=\"35.e+3 36.0 37.0e5\"/>\n"
    "    <Parameter   ParamNumber=\"4\" ParamType=\"SrcStrPtr\" Value=\"xxx yyy zzz\"/>\n"
    "    <Parameter   ParamNumber=\"5\" ParamType=\"RetInt8Ptr\" Size=\"1\"/>\n"
    "    <Parameter   ParamNumber=\"6\" ParamType=\"RetStrPtr\" Size=\"52\"/>\n"
    "  </DriverFunctionCall>\n"
    "</AtXmlDriverFunctionCall>\n"
       );

    Status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096);
    printf("atxml_IssueDriverFunctionCall = %d\n  Response:\n",Status);
    s_ParseErrDbgResponse(Response, 4096);
    s_ParseValDataResponse(Response, 4096);
/**/
/*
{
int RetVal;
char *SrcStr = "xxx yyy zzz";
double SrcDblArray[] = {35.e+3, 36.0, 37.0e5};
unsigned char RetInt8 = 250;
char RetStr[1024];
strcpy(RetStr,"retstr");
    Status = atxml_CallDriverFunction("DevX_1", "TestParam",
        "RetInt32,SrcInt32,SrcUInt16,SrcDblPtr 3,SrcStrPtr,RetUInt8, RetStrPtr 1024",
        &RetVal,5,0x3B05,SrcDblArray,SrcStr,&RetInt8,RetStr);
}
/*
{
double  DblVal = 10.5e3;  // changes to 35.6e3
double  SrcDblArray[] = {35.e+3, 36.0, 37.0e5};
double  RetDblArray[5]; // Return {35.e+3, 36.0, 37.0e5}
unsigned int     IntVal = 0x50505050; // changes to 0x55555555
unsigned int     SrcIntArray[] = {0xaaaaaaaa, 0xbbbbbbbb, 0xcccccccc};
unsigned int     RetIntArray[5]; // Return {0xdddddddd, 0xeeeeeeee, 0xffffffff}
unsigned short   ShortVal = 0x5050; // changes to 0x5555
unsigned short   SrcShortArray[] = {0x1111, 0x2222, 0x3333};
unsigned short   RetShortArray[5]; // Return {0x4444, 0x5555, 0x6666}
unsigned char    CharVal = 0x50; // changes to 0x55
unsigned char    SrcCharArray[] = {0x66, 0x77, 0x88};
unsigned char    RetCharArray[5]; // Return {0x99, 0xaa, 0xbb}
char   *SrcStr = "xxx yyy zzz";
char    RetStr[1024]; // return "xx yy zz"
    Status = atxml_CallDriverFunction("DevX_1", "TestParamAll",
        "Void, Handle,"
        "SrcDbl,SrcDblPtr 3,"
        "SrcInt32,SrcInt32Ptr 3,"
        "SrcUInt16,SrcUInt16Ptr 3,"
        "SrcUInt8,SrcUInt8Ptr 3,"
        "SrcStrPtr,"
        "RetDbl,RetDblPtr 5,"
        "RetInt32,RetInt32Ptr 5,"
        "RetUInt16,RetUInt16Ptr 5,"
        "RetUInt8,RetUInt8Ptr 5,"
        "RetStrPtr 1024",
        NULL,NULL,
        DblVal, SrcDblArray,
        IntVal, SrcIntArray,
        ShortVal, SrcShortArray,
        CharVal, SrcCharArray,
        SrcStr,
        &DblVal, RetDblArray,
        &IntVal, RetIntArray,
        &ShortVal, RetShortArray,
        &CharVal, RetCharArray,
        RetStr);
    printf("%d",IntVal);
}
/**/
/**/
/*
    // Issue Signal test
	printf("Call atxml_IssueSignal(Fetch)\n");
    sprintf(XmlBuf,
		"<AtXmlSignalDescription xmlns:atxml=\"ATXML_TSF\">"
		"	<SignalAction>Fetch</SignalAction>"
        "   <SignalResourceName>DevX_1</SignalResourceName>"
		"	<SignalSnippet>"
		"		<Signal Out=\"DMM_DC_SIGNAL\">"
		"			<Sinusoid name=\"AC_Component\" amplitude= \"3.0 mV\" frequency= \"10 mHz\"/>"
		"			<Constant name=\"DC_Level\" amplitude= \"5V\"/>"
		"			<Sum name=\"DMM_DC_SIGNAL\" In=\"DC_Level AC_Component\"/>"
		"			<TwoWire name=\"ConnDMM_DC_SIGNAL\" hi=\"uut_pin_hi\" lo=\"uut_pin_lo\"/>"
		"				<Measure name=\"DMM\" As=\"DMM_DC_SIGNAL\" attribute=\"dc_ampl\" samples=\"1\"" 
		"				                             Conn=\"ConnDMM_DC_SIGNAL\"/>"
		"		</Signal>"
        "   </SignalSnippet>"
		"</AtXmlSignalDescription>"
        );
    Status = atxml_IssueSignal(XmlBuf, Response, 4096);
    printf("atxml_IssueSignal = %d\n  Response:\n",Status);
    cs_ParseResponse(Response);
/**/


/*
    // Issue Commands Test
	printf("Call atxml_IssueNativeCmds(Write)\n");
    sprintf(XmlBuf,
	"<AtXmlIssueInstrumentCommands>\n"
	"	<SignalResourceName>DevX_1</SignalResourceName>\n"
	"	<InstrumentCommands>\n"
	"		<Commands>*RST;IDN?</Commands>\n"
	"		<ExpectedResponseString MaxLength=\"200\" DelayInSeconds=\"0.001\"/>\n"
	"	</InstrumentCommands>\n"
	"</AtXmlIssueInstrumentCommands>\n"
        );
    Status = atxml_IssueNativeCmds(XmlBuf, Response, 4096);
    printf("atxml_IssueNativeCmds = %d\n  Response:\n",Status);
    cs_ParseResponse(Response);
/**/

/*
	printf("Call atxml_WriteCmds(Write)\n");
    Status = atxml_WriteCmds("DevX_1", "*RST;", &Count);
    printf("atxml_WriteCmds = %d\n  Count:%d\n",Status,Count);

	printf("Call atxml_ReadCmds(Write)\n");
    Status = atxml_ReadCmds("DevX_1", Response, 1024, &Count);
    printf("atxml_ReadCmds = %d\n   Response:[%s]\n   Count:%d\n",Status,Response,Count);

/**/


    getchar();
	return 0;
}

static int s_ParseValDataResponse(char *Response, int BufferSize)
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

static int s_ParseErrDbgResponse(char *Response, int BufferSize)
{
    ATXML_XML_Handle ErrRespHandle;
    char  *ModuleName="";
    char  *LeadText="";
    char  *Message="";
    int    ErrorCode;
    int    DbgLevel;
    int    x;

    x = atxml_ParseErrDbgResponse(Response, BufferSize, true, &ErrRespHandle);
    if((x == 0) && ErrRespHandle)
    {
        while(atxml_ErrDbgNextError(ErrRespHandle, &ModuleName, &LeadText,
                                      &ErrorCode, &Message))
        {
            printf("  <ErrStatus ...%x %s - %s - %30s ...\n",
                                    ErrorCode,ModuleName,LeadText,Message);
        }
        while(atxml_ErrDbgNextDebug(ErrRespHandle, &ModuleName,
                                      &DbgLevel, &Message))
        {
            printf("  <DebugMessage ...%d %s - %30s ...\n",
                                    DbgLevel,ModuleName,Message);
        }
        atxml_ErrDbgClose(ErrRespHandle);
    }
    return(0);
}
