//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlFreedom01502_Single_T.cpp
//
// Date:	    07-JAN-2006
//
// Purpose:	    ATXMLW Instrument Driver for AtXmlFreedom01502_Single
//
// Instrument:	AtXmlFreedom01502_Single  Power Supplies (PDU)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
// Revision History described in AtXmlFreedom01502.cpp
//
///////////////////////////////////////////////////////////////////////////////
// Includes

#include <float.h>
#include <math.h>
#include "visa.h"
#include "aps6062.h"
#include "AtxmlWrapper.h"
#include "AtXmlFreedom01502_T.h"
//#include "cem.h"

// Local Defines

// Function codes
#define ATTRIB_VOLT_DC "dc_ampl"
#define ATTRIB_CURR_DC "dc_curr"

//////// Place AtXmlFreedom01502_Single specific data here //////////////
//////////////////////////////////////////////////////////

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables

// Local Function Prototypes

static void s_IssueDriverFunctionCallFreedom01502_Single(int Handle, ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize);

static bool s_ReversePolarity[10];		//used to indicate if the reverse polarity is already set
static bool s_VoltagePositive[10];		//indicates if voltage is positive or negative
static double s_PreviousVoltage[10];
static double s_PreviousCurrent[10];

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CAtXmlFreedom01502_Single_T
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Instno           int             System assigned instrument number
// ResourceType     int             Type of Resource Base, Physical, Virtual
// ResourceName     char*           Station resource name
// Sim              int             Simulation flag value (0/1)
// Dbglvl           int             Debug flag value
// AddressInfoPtr   InstAddPtr*     Contains all addressing information available
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
CAtXmlFreedom01502_Single_T::CAtXmlFreedom01502_Single_T(char *ResourceName, int ConfigType, int ModuleNum, int Modcnt, int Dbg, int Sim, ViSession Handle,
                         ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int  Status = 0;
	int data1, data2, data3;
	char outbuf[30]="";
    
	ISDODEBUG(dodebug(0, "CAtXmlFreedom01502_Single_T", "Entering Function", (char*) NULL));
	//member vars taken care of in .cpp, but at least define the ones 
	//needed for macros
    if(ResourceName)
    {
        strnzcpy(m_ResourceName,ResourceName,ATXMLW_MAX_NAME);
    }
    m_Sim = Sim;
    m_Dbg = Dbg;
	m_Handle = Handle;
	m_ConfigType = ConfigType;
	m_ModuleCount = Modcnt;
	m_ModuleNum = ModuleNum;
	
    InitPrivateAtXmlFreedom01502_Single();
	NullCalDataAtXmlFreedom01502_Single();
    

	//IFNSIM(Status = viClear(m_Handle)); //clear bus before doin reset

	data1 = 16 + m_ModuleNum;
	data2 = 0;
	data3 = 0;
	
	Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 10);

    m_ResetTime = time(NULL);

    if(Status && ErrorAtXmlFreedom01502_Single(Status, Response, BufferSize))
	{
        return;
	}

	ISDODEBUG(dodebug(0, "CAtXmlFreedom01502_Single_T()", "Leaving function", (char*)0));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CAtXmlFreedom01502_Single_T()
//
// Purpose: Destroy the instrument driver instance
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
//    Class instance destroyed.
//
///////////////////////////////////////////////////////////////////////////////
CAtXmlFreedom01502_Single_T::~CAtXmlFreedom01502_Single_T()
{
    char Dummy[1024];
	
	ISDODEBUG(dodebug(0, "~CAtXmlFreedom01502_Single_T", "Entering Function", (char*) NULL));
    // Reset the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CAtXmlFreedom01502_Single Class Distructor called "),Dummy,1024);
	
	ISDODEBUG(dodebug(0, "~CAtXmlFreedom01502_Single_T", "Leaving Function", (char*) NULL));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusAtXmlFreedom01502_Single
//
// Purpose: Perform the Status action for this driver instance
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
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::StatusAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
     int		Status = 0;

  	ISDODEBUG(dodebug(0, "StatusAtXmlFreedom01502_Single()", "Entering function", (char*)0));

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusAtXmlFreedom01502_Single called "), Response, BufferSize);
    Status = ErrorAtXmlFreedom01502_Single(0, Response, BufferSize);

  	ISDODEBUG(dodebug(0, "StatusAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CallSignalFreedom01502_Single
//
// Purpose: Perform the IEEE 1641 / Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// SignalDescription  ATXMLW_INTF_SIGDESC*   The Allocated FNC code
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
#pragma warning(disable : 4100)
int CAtXmlFreedom01502_Single_T::CallSignalFreedom01502_Single(int action, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

    int		Status = 0;

  	ISDODEBUG(dodebug(0, "CallSignalFreedom01502_Single()", "Entering function", (char*)0));

    switch(m_Action) {

		case ATXMLW_SA_APPLY:
			if((Status = CallSignalFreedom01502_Single(ATXMLW_SA_SETUP, Response, BufferSize)) != 0) {
				return(Status);
			}

			Status = CallSignalFreedom01502_Single(ATXMLW_SA_ENABLE,Response, BufferSize);
			break;

		case ATXMLW_SA_REMOVE:
			if((Status = CallSignalFreedom01502_Single(ATXMLW_SA_DISABLE, Response, BufferSize)) != 0) {
				return(Status);
			}

			Status = CallSignalFreedom01502_Single(ATXMLW_SA_RESET, Response, BufferSize);
			break;

		case ATXMLW_SA_MEASURE:
			Status = CallSignalFreedom01502_Single(ATXMLW_SA_SETUP, Response, BufferSize);
			break;

		case ATXMLW_SA_READ:
			if((Status = CallSignalFreedom01502_Single(ATXMLW_SA_ENABLE, Response, BufferSize)) != 0) {
				return(Status);
			}

			Status = CallSignalFreedom01502_Single(ATXMLW_SA_FETCH, Response, BufferSize);
			break;

		case ATXMLW_SA_RESET:
			Status = ResetAtXmlFreedom01502_Single(Response, BufferSize);
			break;

		case ATXMLW_SA_SETUP:
			Status = SetupAtXmlFreedom01502_Single(Response, BufferSize);
			break;

		case ATXMLW_SA_CONNECT:
			break;

		case ATXMLW_SA_ENABLE:
			Status = EnableAtXmlFreedom01502_Single(Response, BufferSize);
			break;

		case ATXMLW_SA_DISABLE:
			Status = DisableAtXmlFreedom01502_Single(Response, BufferSize);
			break;

		case ATXMLW_SA_FETCH:
			Status = FetchAtXmlFreedom01502_Single(Response, BufferSize);
			break;

		case ATXMLW_SA_DISCONNECT:
			break;

		case ATXMLW_SA_STATUS:
			Status = StatusAtXmlFreedom01502_Single(Response, BufferSize);
			break;
    }

  	ISDODEBUG(dodebug(0, "CallSignalFreedom01502_Single()", "Leaving function", (char*)0));
    return(Status);
}
#pragma warning(default : 4100)

///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalAtXmlFreedom01502_Single
//
// Purpose: Perform the IEEE 1641 / Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// SignalDescription  ATXMLW_INTF_SIGDESC*   The Allocated FNC code
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::IssueSignalAtXmlFreedom01502_Single(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

    int	Status = 0;

  	ISDODEBUG(dodebug(0, "IssueSignalAtXmlFreedom01502_Single()", "Entering function", (char*)0));

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalAtXmlFreedom01502_Single Signal: "),
                              Response, BufferSize);

    if((Status = GetStmtInfoAtXmlFreedom01502_Single(SignalDescription, Response, BufferSize)) == 0) {

		Status = CallSignalFreedom01502_Single(m_Action, Response, BufferSize);
	}

  	ISDODEBUG(dodebug(0, "IssueSignalAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalAtXmlFreedom01502_Single
//
// Purpose: Register/Provide the Calibration data for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// CalData            ATXMLW_INTF_CALDATA*   Xml description of the calibration data
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::RegCalAtXmlFreedom01502_Single(ATXMLW_INTF_CALDATA* CalData)
{
    char      Dummy[1024];

  	ISDODEBUG(dodebug(0, "RegCalAtXmlFreedom01502_Single()", "Entering function", (char*)0));
    // Setup action for the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalAtXmlFreedom01502_Single CalData: %s", 
                               CalData),Dummy,1024);

  	ISDODEBUG(dodebug(0, "RegCalAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetAtXmlFreedom01502_Single
//
// Purpose: Perform the Reset action for this driver instance
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
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::ResetAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";
	int data1, data2, data3;
	char outbuf[30]="";
	
  	ISDODEBUG(dodebug(0, "ResetAtXmlFreedom01502_Single()", "Entering function", (char*)0));
    // Reset action for the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetAtXmlFreedom01502_Single called "), Response, BufferSize);
    
	// Reset the AtXmlFreedom01502_Single

	//IFNSIM(Status = viClear(m_Handle)); //clear bus before doin reset
	data1 = 16 + m_ModuleNum;
	data2 = 0;
	data3 = 0;
	Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, (RESET_WAIT *1000));

    m_ResetTime = time(NULL);

    if(Status)
	{
        ErrorAtXmlFreedom01502_Single(Status, Response, BufferSize);
	}

    InitPrivateAtXmlFreedom01502_Single();

	Sanitize01502();
	
  	ISDODEBUG(dodebug(0, "ResetAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstAtXmlFreedom01502_Single
//
// Purpose: Perform the SelfTest Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Level              ATXMLW_INTF_STLEVEL    Indicates the Instrument Level To Be Performed
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::IstAtXmlFreedom01502_Single(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	
  	ISDODEBUG(dodebug(0, "IstAtXmlFreedom01502_Single()", "Entering function", (char*)0));
    // Reset action for the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstAtXmlFreedom01502_Single called Level %d", 
                              Level), Response, BufferSize);
    // Reset the AtXmlFreedom01502_Single
    switch(Level)
    {
		case ATXMLW_IST_LVL_PST:
			Status = StatusAtXmlFreedom01502_Single(Response,BufferSize);
			break;
		case ATXMLW_IST_LVL_IST:
			break;
		case ATXMLW_IST_LVL_CNF:
			Status = StatusAtXmlFreedom01502_Single(Response,BufferSize);
			break;
		default: 
			break;
    }
    if(Status)
	{
        ErrorAtXmlFreedom01502_Single(Status, Response, BufferSize);
	}

    InitPrivateAtXmlFreedom01502_Single();
	
  	ISDODEBUG(dodebug(0, "IstAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsAtXmlFreedom01502_Single
//
// Purpose: Issue Native nstrument commands for to this instrument
//          Return in the response values in Response
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// InstrumentCmds     ATXMLW_INTF_INSTCMD*   Xml description of the Native Instrument commands
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::IssueNativeCmdsAtXmlFreedom01502_Single(ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int  Status = 0;
    char *CmdBeg= NULL, *CmdEnd = NULL, *RespBuf = NULL, *cptr, *cptr2, tempbuf[200];
    double RespDelay = 0;
    int RespLen = 0;
    int WriteLen = 0;
    int ActWriteLen = 0;
    int ActRespLen = 0;
	int	stringlength = 0;
	int idx = 0;

	ISDODEBUG(dodebug(0, "IssueNativeCmdsAtXmlFreedom01502_Single()", "Entering function", (char*)0));
    // Setup action for the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsAtXmlFreedom01502_Single "), Response, BufferSize);

	// The following code added from atxmlw_InstrumentCommands() and altered for read.
	// The PDU returns a status string of 00 00 00 00 1c, which is seen as a null string.
	//
    //Check for response first
    if((cptr = strstr(InstrumentCmds, "<ExpectedResponseString")) != 0)
    {
        // Get length
        if((cptr2 = strstr(cptr,"MaxLength")))
        {
            sscanf(cptr2,"MaxLength = \" %d",&RespLen);
        }
        else
		{
            RespLen = 1024;
		}
        // Get Delay
        if((cptr2 = strstr(cptr,"DelayInSeconds")) != 0)
        {
            sscanf(cptr2,"DelayInSeconds = \" %lf",&RespDelay);
        }
    }
    //Check for response first
    if((cptr = strstr(InstrumentCmds, "<Commands")) != 0)
    {
        // Isolate String
        CmdBeg = strstr(cptr,">");
        if(CmdBeg) 
		{
			CmdBeg++;
		}
        CmdEnd = strstr(CmdBeg,"</Commands");
        WriteLen = CmdEnd - CmdBeg;
    }

    // Write command string
    if(WriteLen)
    {
        *CmdEnd = '\0';
        if(!m_Sim)
		{		
			Status = viWrite(m_Handle, (ViBuf)CmdBeg, WriteLen, (ViPUInt32)&ActWriteLen);
		}
        else
		{
            ActWriteLen = strlen(CmdBeg);
		}

        *CmdEnd = '<';
        atxmlw_ScalerIntegerReturn("IcWriteLen", "", ActWriteLen, Response, BufferSize);
    }

    // Read Response String
    if(RespLen)
    {
        if(RespDelay)
		{
            Sleep((int)(RespDelay * 1000.0));
		}

        RespBuf = new char[RespLen+4];

        if(!m_Sim && RespBuf)
        {
            Status = viRead(m_Handle, (ViBuf)RespBuf, RespLen, (ViPUInt32)&ActRespLen);
		}
        else
        {
            // Simulation Response
            strnzcpy(RespBuf,"1.0 Simulated viRead Response",RespLen);
            ActRespLen = strlen(RespBuf);
        }
        RespBuf[ActRespLen] = '\0';

		// Replace call to atxmlw_ScalerStringReturn() with code to build xml without
		// sprintf.

        //atxmlw_ScalerStringReturn("IcReadString", "", RespBuf, Response, BufferSize);

		if(Response && (BufferSize > ((int)strlen(Response)+200)))
		{
		    sprintf(&Response[strlen(Response)],
		    "<AtXmlResponse>\n  "
		    "	<ReturnData>\n"
		    "		<ValuePair>\n"
		    "			<Attribute>\"IcReadString\"</Attribute>\n"
		    "			<Value>\n"
		    "				<c:Datum xsi:type=\"c:string\" unit=\"\"><c:Value>");

			stringlength = strlen(Response);

			// only pass through data, no nulls
			for (idx = 0; idx < ActRespLen; idx++)
			{
				if (RespBuf[idx] != NULL)
					Response[stringlength++] = RespBuf[idx];
				//else
				//	Response[stringlength++] = (char)0xef;
			}
			Response[stringlength] = '\0';

			sprintf(tempbuf,"</c:Value></c:Datum>\n"
		    "			</Value>\n"
		    "		</ValuePair>\n"
		    "	</ReturnData>\n"
		    "</AtXmlResponse>\n");
			strcat(Response, tempbuf);
		}

    }
	
  	ISDODEBUG(dodebug(0, "IssueNativeCmdsAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallAtXmlFreedom01502_Single
//
// Purpose: Issue Instrument Driver function calls for to this instrument
//          Return in the response values in Response
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// DriverFunction     ATXMLW_INTF_DRVRFNC*   Xml description of the IVI Instrument commands
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::IssueDriverFunctionCallAtXmlFreedom01502_Single(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallAtXmlFreedom01502_Single()", "Entering function", (char*)0));

    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallAtXmlFreedom01502_Single", Response, BufferSize);
    IFNSIM(s_IssueDriverFunctionCallFreedom01502_Single(m_Handle, DriverFunction, Response, BufferSize));

  	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallAtXmlFreedom01502_Single()", "Leaving function", (char*)0));

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupAtXmlFreedom01502_Single
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::SetupAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	int		  i=0;
	int  data1, data2, data3, tmp, tmp_ampl, tmp_curr;
	char outbuf[30]="";
	double dV = 0.0,
			dVlim=0.0;
    time_t  ResetDelay;
    bool programError = false;    
	
  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()", "Entering function", (char*)0));
    // Setup action for the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupAtXmlFreedom01502_Single called "), Response, BufferSize);
    // Wait for ResetSetup time to elaps
    ResetDelay = time(NULL) - m_ResetTime;
    if(ResetDelay < RESET_DELAY)
	{
        Sleep((int)((RESET_DELAY - ResetDelay) * 1000));
	}

	if(!m_Measure.Exists)
	{
		if ((m_Current.Exists)&&(m_VoltageLimit.Exists))
		{
		  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()1", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), 128, 48, (char*)0));
			data1 = 0x20 + m_ModuleNum; //(32+m_ModuleNum)
			data2 = 0x80;
			data3 = 0x30;
			Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);
		}
		
		// perform this check because the setting of new larger current will prevent
		// a fault on the supply. 
		if( (s_PreviousCurrent[m_ModuleNum - 1] < m_CurrentLimit.Real) || (s_PreviousCurrent[m_ModuleNum - 1] < m_Current.Real) )
		{
			if (m_CurrentLimit.Exists)
			{
				tmp_curr = (int)(m_CurrentLimit.Real * 500);
				data1 = 32 + m_ModuleNum;
				tmp = tmp_curr & 0xF00;
				tmp = tmp >> 8;
				data2 = 64 + tmp;
				tmp = tmp_curr & 0x0FF;
				data3 = tmp;

				ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()2", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), (64 + tmp), (tmp_curr & 0x0FF), (char*)0));
				Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

				s_PreviousCurrent[m_ModuleNum - 1] = m_CurrentLimit.Real;
				
			}

			if (m_Current.Exists)
			{
				tmp_curr = (int)(m_Current.Real * 500);
				data1 = 32 + m_ModuleNum;
				tmp = tmp_curr & 0xF00;
				tmp = tmp >> 8;
				data2 = 64 + tmp;
				tmp = tmp_curr & 0x0FF;
				data3 = tmp;

				ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()3", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), (64 + tmp), (tmp_curr & 0x0FF), (char*)0));
				Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

				s_PreviousCurrent[m_ModuleNum - 1] = m_Current.Real;
			}

			if (m_Voltage.Exists)
			{
				if (m_Voltage.Real < 0)
				{                   
					//first determine if polarity has already been set,
					//because if you try to set the reverse polarity again
					// supply will turn off
					if(!s_ReversePolarity[m_ModuleNum-1])
					{
						//for now customer request that if you Atlas CHANGE from
						//a postive to a negative voltage it is illegal
                        // set voltage to zero and show error;
						if( (s_VoltagePositive[m_ModuleNum - 1]) && (s_PreviousVoltage[m_ModuleNum-1] >= 0.0) ) //(s_PreviousSupply == m_ModuleNum) )
						{
							programError = true;
						}
                        else
                        {
                            s_VoltagePositive[m_ModuleNum - 1] = false;

							//set the reverse polarity relay
							data1 = 32+m_ModuleNum;
							data2 = 128; // 0x80
							data3 = 3;

							ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()4", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), 128, 3, (char*)0));
							Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

							s_ReversePolarity[m_ModuleNum-1] = true;
					
                        }
					}
				
				}
				else
				{
					//identify that 
					s_VoltagePositive[m_ModuleNum - 1] = true;
				}

				// if previous voltage was a negative and this is positive (without resetting)
				// caused by an ATLAS change modifier, adjust the polarity relay
				//for now customer request that if you Atlas CHANGE from
				//a negative to a postive voltage it is illegal 
				//set the voltage to zero and error out.
				if((s_ReversePolarity[m_ModuleNum-1]) && (m_Voltage.Real > 0) && (s_PreviousVoltage[m_ModuleNum-1] < 0.0) )
				{
					// indicate error
					programError = true;
				}
              
				if(programError)
				{
					m_Voltage.Real = 0;
				}
					// just set the voltage
				dV = fabs(m_Voltage.Real);
				if (m_ModuleNum != 10)
				{
					tmp_ampl = (int)(dV *100);
				}
				else
				{
					tmp_ampl = (int)(dV *50);
				}

				data1 = 32 + m_ModuleNum;
				tmp = tmp_ampl & 0xF00;
				tmp = tmp >> 8;
				data2 = 80 + tmp;
				tmp = tmp_ampl & 0x0FF;
				data3 = tmp;

				ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()5", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), (80 + tmp), (tmp_ampl & 0x0FF), (char*)0));

				Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

				s_PreviousVoltage[m_ModuleNum - 1] = m_Voltage.Real;
			}
		
			if (m_VoltageLimit.Exists && (s_PreviousVoltage[m_ModuleNum - 1] != m_VoltageLimit.Real) )
			{				
				dVlim = fabs(m_VoltageLimit.Real);
				if (m_ModuleNum != 10)
				{
					tmp_ampl = (int)(dVlim *100);
				}
				else
				{
					tmp_ampl = (int)(dVlim *50);
				}

				data1 = 32 + m_ModuleNum;
				tmp = tmp_ampl & 0xF00;
				tmp = tmp >> 8;
				data2 = 80 + tmp;
				tmp = tmp_ampl & 0x0FF;
				data3 = tmp;

			  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()6", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), (80 + tmp), (tmp_ampl & 0x0FF), (char*)0));
				Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);
					
				s_PreviousVoltage[m_ModuleNum - 1] = m_VoltageLimit.Real;
			}

		}
		else //
		{
			if (m_Voltage.Exists)
			{
				if(m_Voltage.Real < 0)
				{
					//first determine if polarity has already been set,
					//because if you try to set the reverse polarity again
					// supply will turn off
					if(!s_ReversePolarity[m_ModuleNum-1])
					{

						//for now customer request that if you Atlas CHANGE from
						//a postive to a negative voltage it is illegal
						if( (s_VoltagePositive[m_ModuleNum-1]) && (s_PreviousVoltage[m_ModuleNum-1] >= 0) )
						{
							// indicate setup error
							programError = true;
						}
                        else
                        {
                            s_VoltagePositive[m_ModuleNum-1] = false;

							//set the reverse polarity relay
							data1 = 32+m_ModuleNum;
							data2 = 128; // 0x80
							data3 = 3;

						  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()7", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), 128, 3, (char*)0));
							Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

							s_ReversePolarity[m_ModuleNum-1] = true;	
                        }					
					}				
				}
				else
				{
					//identify that 
					s_VoltagePositive[m_ModuleNum-1] = true;
				}

				// if previous voltage was a negative and this is positive (without resetting)
				// caused by an ATLAS change modifier, adjust the polarity relay
				//for now customer request that if you Atlas CHANGE from
				//a negative to a postive voltage it is illegal 
				//set the voltage to zero and show error
				if( (s_ReversePolarity[m_ModuleNum-1]) && (m_Voltage.Real > 0) && (s_PreviousVoltage[m_ModuleNum-1] < 0.0) )
				{
					//indicate setup error
                    programError = true;
				}
				
				if(programError)
				{
					m_Voltage.Real = 0;
				}

				//set the voltage
				dV = fabs(m_Voltage.Real);
				if (m_ModuleNum != 10)
				{
					tmp_ampl = (int)(dV *100);
				}
				else
				{
					tmp_ampl = (int)(dV *50);
				}

				data1 = 32 + m_ModuleNum;
				tmp = tmp_ampl & 0xF00;
				tmp = tmp >> 8;
				data2 = 80 + tmp;
				tmp = tmp_ampl & 0x0FF;
				data3 = tmp;

			  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()8", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), (80 + tmp), (tmp_ampl & 0x0FF), (char*)0));
				Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

				s_PreviousVoltage[m_ModuleNum - 1] = m_Voltage.Real;
			}
		
			if (m_VoltageLimit.Exists)
			{				
				dVlim = fabs(m_VoltageLimit.Real);
				if (m_ModuleNum != 10)
				{
					tmp_ampl = (int)(dVlim *100);
				}
				else
				{
					tmp_ampl = (int)(dVlim *50);
				}

				data1 = 32 + m_ModuleNum;
				tmp = tmp_ampl & 0xF00;
				tmp = tmp >> 8;
				data2 = 80 + tmp;
				tmp = tmp_ampl & 0x0FF;
				data3 = tmp;

			  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()9", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), (80 + tmp), (tmp_ampl & 0x0FF), (char*)0));
				Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

				s_PreviousVoltage[m_ModuleNum - 1] = m_VoltageLimit.Real;
			}

			if (m_CurrentLimit.Exists)
			{
				tmp_curr = (int)(m_CurrentLimit.Real * 500);
				data1 = 32 + m_ModuleNum;
				tmp = tmp_curr & 0xF00;
				tmp = tmp >> 8;
				data2 = 64 + tmp;
				tmp = tmp_curr & 0x0FF;
				data3 = tmp;

			  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()A", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), (64 + tmp), (tmp_curr & 0x0FF), (char*)0));
				Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

				s_PreviousCurrent[m_ModuleNum - 1] = m_CurrentLimit.Real;
			}

			if (m_Current.Exists)
			{
				tmp_curr = (int)(m_Current.Real * 500);
				data1 = 32 + m_ModuleNum;
				tmp = tmp_curr & 0xF00;
				tmp = tmp >> 8;
				data2 = 64 + tmp;
				tmp = tmp_curr & 0x0FF;
				data3 = tmp;

			  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()B", "DataElement1 = %d\nDataElement2 = %d\nDataElement3 = %d", (32 + m_ModuleNum), (64 + tmp), (tmp_curr & 0x0FF), (char*)0));
				Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

				s_PreviousCurrent[m_ModuleNum - 1] = m_Current.Real;
			}

		} // end else
	
	}

	// indicate that there is an illegal setup
	if(programError)
	{
		Status = 3221159994;					
	}

    if(Status)
	{
        ErrorAtXmlFreedom01502_Single(Status, Response, BufferSize);
	}
	
  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchAtXmlFreedom01502_Single
//
// Purpose: Perform the Fetch action for this driver instance
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
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::FetchAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0, vol_pol, vol_pol_temp, current_temp, status_byte;
	double  MeasValue = 0.0;
    double  MaxTime = 10.0;
    char   *ErrMsg = "";
	ViChar Retbuf[100]="";
	int   data1, data2, data3;
	char     LclMsg[128];
	ViInt32 retCount;
	char outbuf[30]="";
	
  	ISDODEBUG(dodebug(0, "FetchAtXmlFreedom01502_Single()", "Entering function", (char*)0));
    // Fetch action for the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchAtXmlFreedom01502_Single called "), Response, BufferSize);

	// Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = m_MaxTime.Real * 1000; //convert to milliseconds
	}

	//set Visa TimeOut value
	IFNSIM(Status = viSetAttribute(m_Handle, VI_ATTR_TMO_VALUE, (unsigned long)MaxTime));
	if (Status)
    {
		sprintf(LclMsg,"Wrap-Instrument %s viSetAttribute MaxTime %le ", m_ResourceName, MaxTime);
		ATXMLW_ERROR(Status,"Wrap-FetchAtXmlFreedom01502_Single",LclMsg,Response,BufferSize);
	}
    // Fetch data

	//send measurement query
	data1 = m_ModuleNum;
	data2 = 66;
	data3 = 0;
	Sleep(200);

	Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

	IFNSIM(Status = aps6062_readInstrData(m_Handle, 100, Retbuf, &retCount));
	/* Get byte which contins info regading polarity and mask it */
	vol_pol_temp = Retbuf[2];
	vol_pol = vol_pol_temp & 0x80;

    // Accomidate multiple MeasChars as applicable
    if(strcmp(m_MeasFunc, ATTRIB_VOLT_DC)==0) // (voltage)   dc signal
	{
			if (vol_pol != 0x80)/* Checking if the voltage is normal */
			{
				data1 = vol_pol_temp & 0xF;
				data1 = data1 * 256;
				data2 = Retbuf[3] & 0xFF;  //grab first 2 bytes only to avoid getting a neg. number
				if (m_ModuleNum == 10)
				{
					MeasValue = (data1 + data2) / 50.0;		/* Supply #10 has 20 mV/bit resolution */
				}
				else
				{
					MeasValue = (data1 + data2) / 100.0;	/* Supplies #1-#9 have 10 mV/bit resolution*/
													//should be 100 not 50
				}

				ISDODEBUG(dodebug(0, "FetchAtXmlFreedom01502_Single()", "MeasValue = %E", MeasValue, (char*)0));
			}
			else if (vol_pol == 0x80)/* Checking if voltage is reversed */
			{
				data1 = vol_pol_temp & 0xF;
				data1 = data1 * 256;
				data2 = Retbuf[3] & 0xFF;

				if (m_ModuleNum == 10)
				{
					MeasValue = -((data1 + data2) / 50.0);
				}
				else
				{
					MeasValue = -((data1 + data2) / 100.0); //should be 100 not 50
				}
			}
	}
    else if(strcmp(m_MeasFunc, ATTRIB_CURR_DC)==0) // (voltage)   dc signal
	{
			current_temp = Retbuf[0];
			data1 = current_temp & 0xF;
			data1 = data1 * 256;
			data2 = Retbuf[1] & 0xFF; //grab first 2 bytes only to avoid getting a neg. number

			MeasValue = (data1 + data2) / 500.0; 
    }

    
	//send status query
	data1 = m_ModuleNum;
	data2 = 68;
	data3 = 0;
	Sleep(200);

	Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

	IFNSIM(Status = aps6062_readInstrData(m_Handle, 100, Retbuf, &retCount));

	//check for over-voltage and over-current
	status_byte = Retbuf[1] & 0xFF;

    if(Status)
    {
        MeasValue = FLT_MAX;
        ErrorAtXmlFreedom01502_Single(Status, Response, BufferSize);
    }
    else
    {
        sscanf(ErrMsg,"%E",&MeasValue);
    }

	//send value and unit back to CEM
	if(Response && (BufferSize > (int)(strlen(Response)+200)))
	{
		if(strcmp(m_MeasFunc, ATTRIB_VOLT_DC)==0) // (voltage)   dc signal
		{
			atxmlw_ScalerDoubleReturn(m_MeasFunc, "V", MeasValue, Response, BufferSize);
		}
		else if(strcmp(m_MeasFunc, ATTRIB_CURR_DC)==0) // (voltage)   dc signal
		{
			atxmlw_ScalerDoubleReturn(m_MeasFunc, "A", MeasValue, Response, BufferSize);
		}
	}
	
   	ISDODEBUG(dodebug(0, "FetchAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: DisableAtXmlFreedom01502_Single
//
// Purpose: Perform the Open action for this driver instance
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
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::DisableAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	//unsigned char data1, data2, data3;
	int data1, data2, data3;
	char outbuf[30]="";
	int Status=0;
	
  	ISDODEBUG(dodebug(0, "DisableAtXmlFreedom01502_Single()", "Entering function", (char*)0));
    // Open action for the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenAtXmlFreedom01502_Single called "), Response, BufferSize);

    //////// Place AtXmlFreedom01502_Single specific data here //////////////
	//open output relay
	data1 = 32 + m_ModuleNum;
	data2 = 0xA0;
	data3 = 0;

	Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 100);

  	ISDODEBUG(dodebug(0, "DisableAtXmlFreedom01502_Single()", "Leaving function", (char*)0));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableAtXmlFreedom01502_Single
//
// Purpose: Perform the Close action for this driver instance
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
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::EnableAtXmlFreedom01502_Single(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	//unsigned char data1, data2, data3;
	int data1, data2, data3;
	char outbuf[30]="";
	int Status = 0;
	
  	ISDODEBUG(dodebug(0, "EnableAtXmlFreedom01502_Single()", "Entering function", (char*)0));

    // Close action for the AtXmlFreedom01502_Single
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableAtXmlFreedom01502_Single called "), Response, BufferSize);

    
	if ((m_Action == ATXMLW_SA_ENABLE)&&(!m_Measure.Exists))
	{
		//close master relay
		data1 = 32 + m_ModuleNum;
		data2 = 176;
		data3 = 0;

		Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 2000);
		
		//program for remote sense measurement
		data1 = 32+m_ModuleNum;
		data2 = 131;
		data3 = 0;

		Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 10);	
		
	}
	
   	ISDODEBUG(dodebug(0, "EnableAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorAtXmlFreedom01502_Single
//
// Purpose: Query AtXmlFreedom01502_Single for the error text and send to WRTS
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Status           int             Error code returned from driver
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int  CAtXmlFreedom01502_Single_T::ErrorAtXmlFreedom01502_Single(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    char     Msg[MAX_MSG_SIZE];
    int      QError;

    retval = Status;
    Msg[0] = '\0';
	
  	ISDODEBUG(dodebug(0, "ErrorAtXmlFreedom01502_Single()", "Entering function", (char*)0));

    if(Status)
    {
        if(Err)
		{
            sprintf(Msg,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);
		}
		Err = aps6062_errorMessage(m_Handle, (long)Status, Msg);
        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);

    }

    QError = 1;
    // Retrieve any pending errors in the device
    while(QError)
    {
        QError = 0;
        if(Err != 0)
            break;
        if(QError)
        {
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);
        }
    }
	
  	ISDODEBUG(dodebug(0, "ErrorAtXmlFreedom01502_Single()", "Leaving function", (char*)0));

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoAtXmlFreedom01502_Single
//
// Purpose: Get the Modifier values from the ATLAS Statement
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
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::GetStmtInfoAtXmlFreedom01502_Single(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int    Status = 0;
    char   LclElement[ATXMLW_MAX_NAME];
    char   LclResource[ATXMLW_MAX_NAME];
    double LclDblValue;
    char   LclUnit[ATXMLW_MAX_NAME];
    char  *LclCharValuePtr;
	char   Name[ATXMLW_MAX_NAME];
	
  	ISDODEBUG(dodebug(0, "GetStmtInfoAtXmlFreedom01502_Single()", "Entering function", (char*)0));

    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)))
	{
         return(Status);
	}


    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource), Response, BufferSize);

    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement), Response, BufferSize);
	}

	strncpy(Name,m_SignalName,ATXMLW_MAX_NAME); //need Name[] for macros like ISDBLATTR

	if (strcmp(m_SignalElement, "Limit") == 0)
	{
		//get attributes in <Limit/> tag
		if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "limit", &LclDblValue, LclUnit)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found DC_Level amplitude %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		}
		if (strcmp(LclUnit, "V")==0)
		{
			m_VoltageLimit.Real = LclDblValue;
			m_VoltageLimit.Exists=true;
		}
		else if(strcmp(LclUnit, "A")==0)
		{
			m_CurrentLimit.Real = LclDblValue;
			m_CurrentLimit.Exists=true;
		}

		if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "In",  &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		}	

		//get attributes in <Constant/> tag
		if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found DC_Level Element [%s]",LclElement), Response, BufferSize);
		}

		if(strcmp(LclElement, "Constant") == 0)
		{
			//m_Amplitude.Exists=true;
			if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, "amplitude", &LclDblValue, LclUnit)))
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found DC_Level amplitude %E [%s]", LclDblValue, LclUnit), Response, BufferSize);
			}

			if (strcmp(LclUnit, "V")==0)
			{
				m_Voltage.Real = LclDblValue;
				m_Voltage.Exists=true;
			}
			else if(strcmp(LclUnit, "A")==0)
			{
				m_Current.Real = LclDblValue;
				m_Current.Exists=true;
			}

		}
		m_Measure.Exists = false;
	}
	else if (strcmp(m_SignalElement, "Measure") == 0)
	{
		//get attributes in <Measure/> tag
		if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "attribute", &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		}

		if(ISDBLATTR("maxTime",&(m_MaxTime.Real),m_MaxTime.Dim))
		{
			//MeasInfo->TimeOut = DblValue;
			m_MaxTime.Exists=true;
		}

		m_Measure.Exists=true;
		strcpy(m_MeasFunc, LclCharValuePtr);

		// end of parsing...no need to parse further back into string for 
		// <Constant> or <Limit> tags
		
	}

    atxmlw_Close1641Xml(&m_SignalDescription);
	
  	ISDODEBUG(dodebug(0, "GetStmtInfoAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateAtXmlFreedom01502_Single
//
// Purpose: Initialize/Reset all private modifier variables
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
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAtXmlFreedom01502_Single_T::InitPrivateAtXmlFreedom01502_Single(void)
{
    //////// Place AtXmlFreedom01502_Single specific data here //////////////
    int i = 0;
	
  	ISDODEBUG(dodebug(0, "InitPrivateAtXmlFreedom01502_Single()", "Entering function", (char*)0));

	m_Measure.Exists = false;
	m_VoltageLimit.Exists = false;
	m_CurrentLimit.Exists = false;
	m_Voltage.Exists = false;
	m_Current.Exists = false;
	m_MaxTime.Exists = false;

    for(i = 0; i < 10; i++)
    {
        s_ReversePolarity[i] = false;
        s_VoltagePositive[i] = true;
        s_PreviousVoltage[i] = -1.0;
	    s_PreviousCurrent[i] = 0.0;
    }

	ISDODEBUG(dodebug(0, "InitPrivateAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataAtXmlFreedom01502_Single
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAtXmlFreedom01502_Single_T::NullCalDataAtXmlFreedom01502_Single(void)
{
    ISDODEBUG(dodebug(0, "NullCalDataAtXmlFreedom01502_Single()", "Entering function", (char*)0));

    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;

  	ISDODEBUG(dodebug(0, "NullCalDataAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Sanitize01502
//
// Sets instrument to lowest stim and highest impedance settings. Opens all output relays
//
//
///////////////////////////////////////////////////////////////////////////////
void CAtXmlFreedom01502_Single_T::Sanitize01502(void)
{
	int Status = 0;
	char outbuf[256] = {""};

	//Set Output to 0V 0A
	//Open Relays
	sprintf(outbuf, "2%d,A0,00", m_ModuleNum);
	IFNSIM(Status = aps6062_writeInstrData(m_Handle, outbuf))

}


///////////////////////////////////////////////////////////////////////////////
// Function: GetFirmRevAtXmlFreedom01502_Single
//
// Purpose: Query status to get firmware revision.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::GetFirmRevAtXmlFreedom01502_Single(int ModuleNum)
{
	int			data1,data2,data3, Status;
	char		outbuf[30]="";
	ViChar		Retbuf[100]="";
	ViInt32		retCount;

	ISDODEBUG(dodebug(0, "GetFirmRevAtXmlFreedom01502_Single()", "Entering function", (char*)0));	

	/* Status Query */
	data1 = ModuleNum;			  /* x'0X' */
	data2 = 68;               /* x'44' */
	data3 = 0;                /* x'00' */

	Status = WriteToDCPowerSupply(m_Handle, data1, data2, data3, 200);

	IFNSIM(aps6062_readInstrData(m_Handle, 100, Retbuf, &retCount));
	
  	ISDODEBUG(dodebug(0, "GetFirmRevAtXmlFreedom01502_Single()", "Leaving function", (char*)0));
	return (Retbuf[3]);
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: s_IssueDriverFunctionCallFreedom01502_Single
//
// Purpose: Parse the DriverFunction XML and execute requested Driver Function
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// DeviceHandle       int                  Instrument PnP/Vi Handle 
// DriverFunction     ATXMLW_INTF_DRVRFNC* Pointer to DriverFunction XML String
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return: 
//      void
//
///////////////////////////////////////////////////////////////////////////////
void s_IssueDriverFunctionCallFreedom01502_Single(int DeviceHandle, ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    ATXMLW_DF_VAL RetVal;
    ATXMLW_XML_HANDLE DfHandle=NULL;
    char Name[ATXMLW_MAX_NAME];
	
  	ISDODEBUG(dodebug(0, "s_IssueDriverFunctionCallFreedom01502_Single()", "Entering function", (char*)0));
    if((atxmlw_ParseDriverFunction(DeviceHandle, DriverFunction, &DfHandle, Response, BufferSize)) || (DfHandle == NULL))
	{
        return;
	}

    atxmlw_GetDFName(DfHandle,Name);
    RetVal.Int32 = 0;

	if (ISDFNAME("aps6062_writeInstrData"))
	{
		RetVal.Int32 = aps6062_writeInstrData(DeviceHandle, SrcStrPtr(2));
	}
	else if (ISDFNAME("aps6062_readInstrData"))
	{
		RetVal.Int32 = aps6062_readInstrData(DeviceHandle, SrcInt16(2), RetStrPtr(3), (long*)RetInt32Ptr(4));
	}
	else if (ISDFNAME("aps6062_reset"))
	{
		RetVal.Int32 = aps6062_reset(DeviceHandle);
	}
	else if (ISDFNAME("aps6062_selfTest"))
	{
		RetVal.Int32 = aps6062_selfTest(DeviceHandle, (short*)RetUInt16Ptr(2), RetStrPtr(3));
	}
	else if (ISDFNAME("aps6062_errorQuery"))
	{
		RetVal.Int32 = aps6062_errorQuery(DeviceHandle, (long*)RetInt32Ptr(2), RetStrPtr(3));
	}
	else if (ISDFNAME("aps6062_errorMessage"))
	{
		RetVal.Int32 = aps6062_errorMessage(DeviceHandle, SrcInt32(2), RetStrPtr(3));
	}
    else if ((strncmp("vi",Name,2)==0) && atxmlw_doViDrvrFunc(DfHandle,Name,&RetVal))
	{
        int x = 0;
	}
    else
    {
        atxmlw_ErrorResponse("", Response, BufferSize, "IssueDriverFunction ", ATXMLW_WRAPPER_ERRCD_INVALID_ACTION, atxmlw_FmtMsg(" Invalid/Unimplemented Function [%s]",Name));
    }
    // return "Ret..." values and RetVal value to caller
    atxmlw_ReturnDFResponse(DfHandle,RetVal,Response,BufferSize);
    atxmlw_CloseDFXml(DfHandle);

  	ISDODEBUG(dodebug(0, "s_IssueDriverFunctionCallFreedom01502_Single()", "Leaving function", (char*)0));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: WriteToDCPowerSupply
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//	InstHandle			ViSession			Handle to selected DC suply
//	DataElement1		int					First hex byte to be sent
//	DataElement2		int					Second hex byte to be sent
//	DataElement3		int					Third hex byte to be sent
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//    zero - All OK.
//    !0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAtXmlFreedom01502_Single_T::WriteToDCPowerSupply(ViSession InstHandle, int DataElement1,
												  int DataElement2, int DataElement3, int SleepValue)
{

	int		Status = 0;
	char	OutBuf[4] = {0};

  	ISDODEBUG(dodebug(0, "WriteToDCPowerSupply()", "Entering function", (char*)0));

	sprintf(OutBuf, "%c%c%c", DataElement1, DataElement2, DataElement3);
	IFNSIM(Status = aps6062_writeInstrData(InstHandle, OutBuf));
	Sleep(SleepValue);

  	ISDODEBUG(dodebug(0, "WriteToDCPowerSupply()", "Leaving function", (char*)0));
	return(Status);
}

////////////////////////////////////////////////////////////////////////////////
// Function : dodebug(int, char*, char* format, ...);                         //
// Purpose  : Print the message to a debug file.                              //
// Return   : None if it don't work o well                                    //
////////////////////////////////////////////////////////////////////////////////
void dodebug(int code, char *function_name, char *format, ...)
{

	static int		FirstRun = 0;
	char			TmpBuf[_MAX_PATH];

	if (DE_BUG == 1 && FirstRun == 0) {

		sprintf(TmpBuf, "%s", DEBUGIT_DCPS);
		if ((debugfp = fopen(TmpBuf, "w+b")) == NULL) {
			DE_BUG = 0;
			return;
		}
		FirstRun++;
	}

	va_list	arglist;

	if (DE_BUG) {

		if (code != 0) {
			fprintf(debugfp, "%s in %s\r\n", strerror(code), function_name);
		}
		else {
			va_start(arglist, format);
			fprintf(debugfp, "In function %s ", function_name);
			vfprintf(debugfp, format, arglist);
			fprintf(debugfp, "\r\n");
			va_end(arglist);
		}

		fflush(debugfp);
	}

	return;
}
