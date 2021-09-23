///////////////////////////////////////////////////////////////////////////////
// File    : CemStationUtil.cpp
//
// Purpose : General functions for performing station dependant functions.
//
//
//
// Functions
// Name						Purpose
// =======================  ===================================================
// cs_CheckStationStatus    Checks Station Status (e.g. ICA Interlock, Temp etc.)
// cs_GetUniqCalCfg         Returns last Cal date and checks latest Config etc.
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

// Local static variables

// Local static functions


//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: cs_CheckStationStatus
//
// Purpose: This function will call the CheckUniqStationStatus routine in the
//          SwxSrvr.dll
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ErrMsg           Pointer         Return pointer to an static error message
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_CheckStationStatus()
{
    int RetVal;
    char *ErrMsg;

    //FIX the use of ErrMsg etc. Move to CheckUniq... Response etc.
    RetVal = CheckUniqStationStatus(&ErrMsg);
    if(RetVal)
    {
        if(RetVal == CS_STA_INIT)
        {
            CEMERROR(EB_SEVERITY_WARNING, ErrMsg);
        }
        else
        {
            CEMERROR(EB_SEVERITY_ERROR, ErrMsg);
            CEMERROR(EB_ACTION_RESET, ErrMsg);
        }
    }
    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: cs_GetUniqCalCfg
//
// Purpose: This function will call the s_GetUniqCalCfg routine below
//          All errors are processed at this level and only the error code is returned
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// DevName          CharStr         Zero terminated char string of DeviceName
//                                  from Busconfi
// ExpTs            time_t          Seconds from last cal to expire (Cal cycle time)
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// CalData          Pointer         Pointer to double array to return Cal Data
// Count            int             Number of values in Cal Data array
// Sim              int             Indicate that this device is being simulated
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_GetUniqCalCfg(char *DevName, time_t ExpTs, double *CalData, int Count,
									 int Sim)
{
    int   RetVal = 0;
    char  Response[4096];

    Response[0] = '\0';
    RetVal = GetUniqCalAvail(DevName, ExpTs, CalData, Count, Response, 4096, Sim);
    if(RetVal || (Response[0] != '\0'))
    {
		if(Sim)
		{
			RetVal = cs_ParseResponseErrorOvr(EB_SEVERITY_WARNING, Response, NULL, 0);
		}
		else
		{
			RetVal = cs_ParseResponseError(Response, NULL, 0);
		}
        RetVal = 0; //Ignore for now
    }
    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: cs_SetUniqCalCfg
//
// Purpose: This function will call the s_SetUniqCalCfg routine below
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// DevName          CharStr         Zero terminated char string of DeviceName
//                                  from Busconfi
// Ts               time_t          TimeStamp of latest Calibration
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// CalData          Pointer         Pointer to double array to return Cal Data
// Count            int             Number of values in Cal Data array
// ErrMsg           Pointer         Return pointer to an static error message
// Sim              int             Indicate that this device is being simulated
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_SetUniqCalCfg(char *DevName, time_t Ts, double *CalData, int Count,
									 int Sim)
{
    int RetVal = 0;
    char  Response[4096];

    Response[0] = '\0';
    RetVal = SetUniqCalCfg(DevName, Ts, CalData, Count, Response, 4096, Sim);
    if(RetVal || (Response[0] != '\0'))
    {
	    RetVal = cs_ParseResponseError(Response, NULL, 0);
    }
    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: cs_SysResetDevice
//
// Purpose: This function will call the SwxSrvr SysResetDevice routine
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// DevName          CharStr         Zero terminated char string of DeviceName
//                                  from Busconfi
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_SysResetDevice(char *DevName)
{
    int RetVal = 0;
    char  Response[4096];

    Response[0] = '\0';
    RetVal = SysResetDevice(DevName, Response, 4096);
    if(RetVal || (Response[0] != '\0'))
    {
	    RetVal = cs_ParseResponseError(Response, NULL, 0);
    }
    return(RetVal);
}
///////////////////////////////////////////////////////////////////////////////
// Function: cs_IssueAtmlSignal
//
// Purpose: This function will issue the ATML Signal Description
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// Verb              char            AtXml Verb (Action)
// DeviceName        char*           Device Name used as Resource Name
// SignalDescription char*           IEEEE 1641 XML Signal Description
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValueResponse    char*           Xml Value return
//
// Return: 0 - AOK
//       neg - Error code (Messages issued at this level)
//
///////////////////////////////////////////////////////////////////////////////
int  cs_IssueAtmlSignal(char *Verb, char *DevName, char *SignalDescription,
                        char *ValueResponse, int BufferSize)
{
    return(cs_IssueAtmlSignalMaxTime(Verb, DevName, -1.0, SignalDescription,
                        ValueResponse, BufferSize));
}
int  cs_IssueAtmlSignalMaxTime(char *Verb, char *DevName, double MaxTime, char *SignalDescription,
                        char *ValueResponse, int BufferSize)
{
    int Status = 0;
    char  *Response;
    int    BufSize = 4096;

    if(BufferSize > BufSize)
        BufSize = BufferSize;
    Response = new char[BufSize+4];
    if(ValueResponse) ValueResponse[0] = '\0';
    Response[0] = '\0';
    Status = CemIssueAtmlSignal(Verb, DevName, MaxTime, SignalDescription,
                                         Response, BufSize);
    if(Status || (Response[0] != '\0'))
    {
	    Status = cs_ParseResponseError(Response, ValueResponse, BufferSize);
    }
    if(Response)
        delete(Response);
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: cs_IssueIst
//
// Purpose: This function will issue the command to issue Instrument SelfTest
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// DeviceName        char*           Device Name used as Resource Name
// Level             Int             IST Level
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ValueResponse    char*           Xml Value return
//
// Return: 0 - AOK
//       neg - Error code (Messages issued at this level)
//
///////////////////////////////////////////////////////////////////////////////
int cs_IssueIst(char *DevName, int Level, char *XmlValueResponse, int RespSize)
{
    return(IssueIst(DevName, Level, XmlValueResponse, RespSize));
}
///////////////////////////////////////////////////////////////////////////////
// Function: cs_GetSingleIntValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:integer" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// XmlValueResponse  char*           Response ReturnData sequence for double datum
// MeasFunc          char*           Attribute being fetched
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// MeasValue        int*            Returned value
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_GetSingleIntValue(char *XmlValueResponse, char *MeasFunc, int *MeasValue)
{
    return(GetSingleIntValue(XmlValueResponse, MeasFunc, MeasValue));
/*
    char *cptr, *BegEleCptr, *EndEleCptr;
    char MeasAttribute[56];
    char cMeasValue[56];
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
/*
    BegEleCptr = XmlValueResponse;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        MeasAttribute[0] = '\0';
        cMeasValue[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",MeasAttribute);
            if((MeasFunc) && (strcmp(MeasAttribute, MeasFunc) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if((cptr = strstr(cptr,"<c:Datum")) &&
           (cptr = strstr(cptr,"value")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"value = \"%i",MeasValue);
        }
        BegEleCptr = EndEleCptr;
    }

    return(0);
*/
}

///////////////////////////////////////////////////////////////////////////////
// Function: cs_GetIntArrayValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:integer" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// XmlValueResponse  char*           Response ReturnData sequence for double datum
// MeasFunc          char*           Attribute being fetched
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// MeasArray        int*            Returned values
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_GetIntArrayValue(char *XmlValueResponse, char *Attr, 
                        int *IntValue, int *ArraySize)
{
    return(GetIntArrayValue(XmlValueResponse, Attr, 
                        IntValue, ArraySize));
/*
    char *cptr, *cptr2, *BegEleCptr, *EndEleCptr;
    char Attribute[56];
    char cIntValue[56];
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
	    	</ReturnData>
	    </AtXmlResponse>
*/
/*
    BegEleCptr = XmlValueResponse;
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
        if((cptr = strstr(cptr,"<c:IndexedArray")) &&
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
                IntValue[Idx] = 0;
                cptr2 = strstr(cptr,"value");
                if(cptr2)
                    sscanf(cptr2,"value = \"%i",&IntValue[Idx]);
                if((cptr = strstr(++cptr,"<c:Element")) &&
                   (cptr < EndEleCptr))
                    continue;
                else
                    break;
            }
        }
        BegEleCptr = EndEleCptr;
        *ArraySize = MaxIdx+1;
    }

    return(0);
*/
}


///////////////////////////////////////////////////////////////////////////////
// Function: cs_GetSingleDblValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:double" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// XmlValueResponse  char*           Response ReturnData sequence for double datum
// MeasFunc          char*           Attribute being fetched
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// MeasValue        double*         Returned value
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_GetSingleDblValue(char *XmlValueResponse, char *MeasFunc, double *MeasValue)
{
    return(GetSingleDblValue(XmlValueResponse, MeasFunc, MeasValue));
/*
    char *cptr, *BegEleCptr, *EndEleCptr;
    char MeasAttribute[56];
    char cMeasValue[56];
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
/*
    BegEleCptr = XmlValueResponse;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        MeasAttribute[0] = '\0';
        cMeasValue[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",MeasAttribute);
            if((MeasFunc) && (strcmp(MeasAttribute, MeasFunc) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if((cptr = strstr(cptr,"<c:Datum")) &&
           (cptr = strstr(cptr,"value")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"value = \"%lf",MeasValue);
        }
        BegEleCptr = EndEleCptr;
    }

    return(0);
*/
}

///////////////////////////////////////////////////////////////////////////////
// Function: cs_GetDblArrayValue
//
// Purpose: This function will issue retrieve a "ReturnData:Datum:double" from
//          the XML Response
//
// Input Parameters
// Parameter		 Type			Purpose
// ===============   ==============  ===========================================
// XmlValueResponse  char*           Response ReturnData sequence for double datum
// MeasFunc          char*           Attribute being fetched
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// MeasArray        double*         Returned values
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_GetDblArrayValue(char *XmlValueResponse, char *MeasFunc,
                        double *MeasValue, int *ArraySize)
{
    return(GetDblArrayValue(XmlValueResponse, MeasFunc,
                        MeasValue, ArraySize));
/*
    char *cptr, *cptr2, *BegEleCptr, *EndEleCptr;
    char MeasAttribute[56];
    char cMeasValue[56];
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
	    	</ReturnData>
	    </AtXmlResponse>
*/
/*
    BegEleCptr = XmlValueResponse;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        MeasAttribute[0] = '\0';
        cMeasValue[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",MeasAttribute);
            if((MeasFunc) && (strcmp(MeasAttribute, MeasFunc) != 0))
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
        if((cptr = strstr(cptr,"<c:IndexedArray")) &&
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
                MeasValue[Idx] = 0.0;
                cptr2 = strstr(cptr,"value");
                if(cptr2)
                    sscanf(cptr2,"value = \"%lf",&MeasValue[Idx]);
                if((cptr = strstr(++cptr,"<c:Element")) &&
                   (cptr < EndEleCptr))
                    continue;
                else
                    break;
            }
        }
        BegEleCptr = EndEleCptr;
        *ArraySize = MaxIdx+1;
    }

    return(0);
*/
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
// XmlValueResponse  char*           Response ReturnData sequence for double datum
// MeasFunc          char*           Attribute being fetched
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// MeasValue        double*         Returned value
//
// Return: 0 - AOK
//       neg - Error code
//
///////////////////////////////////////////////////////////////////////////////
int cs_GetStringValue(char *XmlValueResponse, char *MeasFunc, char *StrValue, int BufferSize)
{
    return(GetStringValue(XmlValueResponse, MeasFunc, StrValue, BufferSize));
/*
    char *cptr, *BegEleCptr, *EndEleCptr;
    char MeasAttribute[50];
    char cMeasValue[50];
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
/*
    BegEleCptr = XmlValueResponse;
    while(BegEleCptr && (BegEleCptr = strstr(BegEleCptr,"<ValuePair")))
    {
        // find end of element
        EndEleCptr = strstr(BegEleCptr,"</ValuePair");
        if(EndEleCptr == NULL)
            break;
        MeasAttribute[0] = '\0';
        cMeasValue[0] = '\0';
        if((cptr = strstr(BegEleCptr,"<Attribute")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<Attribute> %[^ <]",MeasAttribute);
            if((MeasFunc) && (strcmp(MeasAttribute, MeasFunc) != 0))
            {
                BegEleCptr = EndEleCptr;
                continue;
            }
        }
        if((cptr = strstr(cptr,"<c:Datum")) &&
            (cptr = strstr(cptr,"<c:Value")) &&
            (cptr < EndEleCptr))
        {
            sscanf(cptr,"<c:Value>  %[^<]",StrValue);
        }
        BegEleCptr = EndEleCptr;
    }

    return(0);
*/
}

//++++/////////////////////////////////////////////////////////////////////////
//      Local Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: array_to_bin_string()
//
// Purpose: Converts an integer array to a Null terminated string comforming to
//           the serial data type(H's and L's) in xml 1641
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            int *           integer array to be converted
// size             int             size of integer array to be converted
// length			int				length of word in bits
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// output           char *          Null terminated string in 1641 format
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void cs_array_to_bin_string(int array [], int size, int length, char output [])
{
	char string[2048] = {0};
	char temp[20] = {0};

	for(int i = 0; i < size; i++)
	{
		for(int j = length - 1; j >= 0; j--)
		{
			if((array[i] >> j) & 0x0001)
			{

				strcat(temp, "H");
			}
			else
			{
				strcat(temp, "L");
			}
		}

		if(i != size - 1)
		{
			strcat(temp, ", ");
		}
			
		strcat(string, temp);
		temp[0] = '\0';
	}
	
    strcpy(output, string);
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: bin_string_to_array()
//
// Purpose: Converts a Null terminated string comforming to
//           the serial data type(H's and L's) in xml 1641 to an integer array
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// input            char *          Null terminated string in 1641 format
// length			int				length of word in bits
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            int **          integer array to be filled
// size             int *           size of integer array filled
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void cs_bin_string_to_array(char input [], int length, int array [], int * size)
{
	char string[2048];
	char *temp;
	int i = 0, j = 0;

	memset(string, '\0', sizeof(string));

	sprintf(string, "%s", input);
	temp = strtok(string, ",");
	
	while(temp != NULL)
	{

		array[i] = 0;
		for(j = 0; j < length; j++)
		{
			if(temp[j] == 'H')
			{
				array[i] = array[i] + (1 << (length-1-j)); 
			}				
		}

		i++;
		temp = strtok(NULL, ",");
		if(temp)
		{
			temp++;
		}
	}

	*size = i;
	
	return;
}