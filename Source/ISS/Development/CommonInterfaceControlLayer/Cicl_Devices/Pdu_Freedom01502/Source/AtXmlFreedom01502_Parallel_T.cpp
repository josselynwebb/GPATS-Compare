//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlFreedom01502_Parallel_T.cpp
//
// Date:	    07-JAN-2006
//
// Purpose:	    ATXMLW Instrument Driver for AtXmlFreedom01502_Parallel
//
// Instrument:	AtXmlFreedom01502_Parallel  Power Supplies (PDU)
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

int DE_BUG = 0;
FILE *debugfp = 0;

// Local Defines

// Function codes
#define ATTRIB_VOLT_DC "dc_ampl"
#define ATTRIB_CURR_DC "dc_curr"

//////// Place AtXmlFreedom01502_Parallel specific data here //////////////
//////////////////////////////////////////////////////////

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables
static bool s_VoltageParallelPositive[10];
static bool s_ReverseParallelPolarity[10];
static double s_PreviousParallelVoltage[10];
static double s_PreviousParallelCurrent[10];
static int s_PreviousParallelSupply[10];

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CAtXmlFreedom01502_Parallel_T
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// ResourceName       char*                System assigned device name "AFG_1", "AFG_2",
// ConfigType		  int                  configuration 
// ModuleArr          int*                 Supplies in parallel
// Modcnt             int                  Number of supplies in parallel
// sim                int                  Simulation flag value (0/1)
// dbglvl             int                  Debug flag value
// Handle             ViSession*           Submodule handles
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
CAtXmlFreedom01502_Parallel_T::CAtXmlFreedom01502_Parallel_T(char *ResourceName, int ConfigType, int *ModuleArr, int Modcnt, int Dbg, int Sim, ViSession *Handle, 
															 ATXMLW_INTF_RESPONSE* response, int buffersize)
{
    int  Status = 0;
	char outbuf[30]="";
	int  i;

	ISDODEBUG(dodebug(0, "CAtXmlFreedom01502_Parallel_T()", "Entering function", (char*)0));
    // Save Device Array Info
    if(ResourceName)
    {
        strnzcpy(m_ResourceName,ResourceName,ATXMLW_MAX_NAME);
    }
    m_Sim = Sim;
    m_Dbg = Dbg;
	//m_Handle = Handle;
	m_ConfigType = ConfigType;
	m_ModuleCount = Modcnt;
	for(i=0; i<15; i++)
		m_ModuleArray[i]=ModuleArr[i];
	for(i=0; i<10; i++)
	{
		m_Handle[i]=Handle[i];
		if(Status && ErrorAtXmlFreedom01502_Parallel(Status, i, response, buffersize))
			return;
	}


    InitPrivateAtXmlFreedom01502_Parallel();
	NullCalDataAtXmlFreedom01502_Parallel();

    m_ResetTime = time(NULL);    

   ISDODEBUG(dodebug(0, "CAtXmlFreedom01502_Parallel_T()", "Leaving function", (char*)0));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CAtXmlFreedom01502_Parallel_T()
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
CAtXmlFreedom01502_Parallel_T::~CAtXmlFreedom01502_Parallel_T()
{
    char Dummy[1024];

  	ISDODEBUG(dodebug(0, "CAtXmlFreedom01502_Parallel_T()", "Entering function", (char*)0));
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CAtXmlFreedom01502_Parallel Class Distructor called "), Dummy, 1024);

  	ISDODEBUG(dodebug(0, "CAtXmlFreedom01502_Parallel_T()", "Leaving function", (char*)0));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::StatusAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    ISDODEBUG(dodebug(0, "StatusAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusAtXmlFreedom01502_Parallel called "), Response, BufferSize);

  	ISDODEBUG(dodebug(0, "StatusAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CallSignalFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::CallSignalFreedom01502_Parallel(int action, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

    int	Status = 0;

  	ISDODEBUG(dodebug(0, "CallSignalFreedom01502_Parallel()", "Entering function", (char*)0));

    switch(m_Action) {

		case ATXMLW_SA_APPLY:
			if((Status = CallSignalFreedom01502_Parallel(ATXMLW_SA_SETUP, Response, BufferSize)) != 0) {
				return(Status);
			}

			Status = CallSignalFreedom01502_Parallel(ATXMLW_SA_ENABLE,Response, BufferSize);
			break;

		case ATXMLW_SA_REMOVE:
			if((Status = CallSignalFreedom01502_Parallel(ATXMLW_SA_DISABLE, Response, BufferSize)) != 0) {
				return(Status);
			}

			Status = CallSignalFreedom01502_Parallel(ATXMLW_SA_RESET, Response, BufferSize);
			break;

		case ATXMLW_SA_MEASURE:
			Status = CallSignalFreedom01502_Parallel(ATXMLW_SA_SETUP, Response, BufferSize);
			break;

		case ATXMLW_SA_READ:
			if((Status = CallSignalFreedom01502_Parallel(ATXMLW_SA_ENABLE, Response, BufferSize)) != 0) {
				return(Status);
			}

			Status = CallSignalFreedom01502_Parallel(ATXMLW_SA_FETCH, Response, BufferSize);
			break;

		case ATXMLW_SA_RESET:
			Status = ResetAtXmlFreedom01502_Parallel(Response, BufferSize);
			break;

		case ATXMLW_SA_SETUP:
			Status = SetupAtXmlFreedom01502_Parallel(Response, BufferSize);
			break;

		case ATXMLW_SA_CONNECT:
			break;

		case ATXMLW_SA_ENABLE:
			Status = EnableAtXmlFreedom01502_Parallel(Response, BufferSize);
			break;

		case ATXMLW_SA_DISABLE:
			Status = DisableAtXmlFreedom01502_Parallel(Response, BufferSize);
			break;

		case ATXMLW_SA_FETCH:
			Status = FetchAtXmlFreedom01502_Parallel(Response, BufferSize);
			break;

		case ATXMLW_SA_DISCONNECT:
			break;

		case ATXMLW_SA_STATUS:
			Status = StatusAtXmlFreedom01502_Parallel(Response, BufferSize);
			break;
    }

  	ISDODEBUG(dodebug(0, "CallSignalFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(Status);
}
#pragma warning(default : 4100)

///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::IssueSignalAtXmlFreedom01502_Parallel(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

    int		Status = 0;

  	ISDODEBUG(dodebug(0, "IssueSignalAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalAtXmlFreedom01502_Parallel Signal: "),
								 Response, BufferSize);

    if((Status = GetStmtInfoAtXmlFreedom01502_Parallel(SignalDescription, Response, BufferSize)) == 0) {

		Status = CallSignalFreedom01502_Parallel(m_Action, Response, BufferSize);
	}

  	ISDODEBUG(dodebug(0, "IssueSignalAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::RegCalAtXmlFreedom01502_Parallel(ATXMLW_INTF_CALDATA* CalData)
{
    char	Dummy[1024];

  	ISDODEBUG(dodebug(0, "RegCalAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalAtXmlFreedom01502_Parallel CalData: %s", 
                               CalData),Dummy,1024);

  	ISDODEBUG(dodebug(0, "RegCalAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::ResetAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int LastPS;
	int   Status = 0;
    char *ErrMsg = "";
	int  data1, data2, data3;
	char outbuf[30]="";

	ISDODEBUG(dodebug(0, "ResetAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));

    // Reset action for the AtXmlFreedom01502_Parallel
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetAtXmlFreedom01502_Parallel called "), Response, BufferSize);
    // Reset the AtXmlFreedom01502_Parallel
	LastPS = m_ModuleArray[0] + m_ModuleCount - 1;
    data1 = 16 + LastPS;
	data2 = 0;
	data3 = 0;
	for(LastPS; LastPS >= m_ModuleArray[0]; LastPS--)  //do slaves first then master
	{
		//close slaves and master output relays
		Status = WriteToDCPowerSupply(m_Handle[LastPS],data1, data2, data3, 500);

		if(Status)
		{
			ErrorAtXmlFreedom01502_Parallel(Status, LastPS, Response, BufferSize);
		}
	}

	

    IFNSIM(Sleep((RESET_WAIT * 1000)));
    m_ResetTime = time(NULL);    

    InitPrivateAtXmlFreedom01502_Parallel();
	
  	ISDODEBUG(dodebug(0, "ResetAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::IstAtXmlFreedom01502_Parallel(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	
  	ISDODEBUG(dodebug(0, "IstAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));
    // Reset action for the AtXmlFreedom01502_Parallel
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstAtXmlFreedom01502_Parallel called Level %d", 
                              Level), Response, BufferSize);
    // Reset the AtXmlFreedom01502_Parallel
    //////// Place AtXmlFreedom01502_Parallel specific data here //////////////
    switch(Level)
    {
		case ATXMLW_IST_LVL_PST:
			Status = StatusAtXmlFreedom01502_Parallel(Response,BufferSize);
			break;
		case ATXMLW_IST_LVL_IST:
			break;
		case ATXMLW_IST_LVL_CNF:
			Status = StatusAtXmlFreedom01502_Parallel(Response,BufferSize);
			break;
		default: 
			break;
    }

    InitPrivateAtXmlFreedom01502_Parallel();
	
  	ISDODEBUG(dodebug(0, "IstAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::IssueNativeCmdsAtXmlFreedom01502_Parallel(ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
    char *CmdBeg= NULL, *CmdEnd = NULL, *RespBuf = NULL, *cptr, *cptr2, tempbuf[200];
    double RespDelay = 0;
    int RespLen = 0;
    int WriteLen = 0;
    int ActWriteLen = 0;
    int ActRespLen = 0;
	int	stringlength = 0;
	int idx = 0;

  	ISDODEBUG(dodebug(0, "IssueNativeCmdsAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));

    // Setup action for the AtXmlFreedom01502_Parallel
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsAtXmlFreedom01502_Parallel "), Response, BufferSize);

	// The following code added from atxmlw_InstrumentCommands() and altered for read.
	// The PDU returns a status string of 00 00 00 00 1c, which is seen as a null string.
	//
    //Check for response first
    if((cptr = strstr(InstrumentCmds, "<ExpectedResponseString")) != 0)
    {
        // Get length
        if((cptr2 = strstr(cptr,"MaxLength")) != 0)
        {
            sscanf(cptr2,"MaxLength = \" %d",&RespLen);
        }
        else
            RespLen = 1024;
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
			Status = viWrite(m_Handle[m_ModuleArray[0]], (ViBuf)CmdBeg, WriteLen, (ViPUInt32)&ActWriteLen);
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
            Status = viRead(m_Handle[m_ModuleArray[0]], (ViBuf)RespBuf, RespLen, (ViPUInt32)&ActRespLen);
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
				{
					Response[stringlength++] = RespBuf[idx];
				}
				
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
    
  	ISDODEBUG(dodebug(0, "IssueNativeCmdsAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallAtXmlFreedom01502_Parallel
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
#pragma warning(disable : 4100)
int CAtXmlFreedom01502_Parallel_T::IssueDriverFunctionCallAtXmlFreedom01502_Parallel(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{

  	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));

    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallAtXmlFreedom01502_Parallel", Response, BufferSize);

  	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(0);
}
#pragma warning(default : 4100)

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::SetupAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	int		  i=0;
	//int       data1, data2, data3, tmp, tmp_ampl, tmp_curr;
	char outbuf[30]="";
    time_t  ResetDelay;

  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));

    // Setup action for the AtXmlFreedom01502_Parallel
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupAtXmlFreedom01502_Parallel called "), Response, BufferSize);

    // Wait for ResetSetup time to elaps
    ResetDelay = time(NULL) - m_ResetTime;
    if(ResetDelay < RESET_DELAY)
	{
        Sleep((int)((RESET_DELAY - ResetDelay) * 1000));
	}
	
  	ISDODEBUG(dodebug(0, "SetupAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::FetchAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int     Status = 0,
		    vol_pol,
			vol_pol_temp,
			current_temp;
	double  MeasValue = 0.0;
    double  MaxTime = 5000;
    char   *ErrMsg = "";
	ViChar Outbuf[100];
	int   data1, data2;
	ViInt32 retCount;
	
  	ISDODEBUG(dodebug(0, "FetchAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));
    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = m_MaxTime.Real * 1000;
	}

    // Fetch action for the AtXmlFreedom01502_Parallel
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchAtXmlFreedom01502_Parallel called "), Response, BufferSize);

    // Fetch data
	IFNSIM(Status = aps6062_readInstrData(m_Handle[m_ModuleArray[0]], 100, Outbuf, &retCount));
	/* Get byte which contins info regading polarity and mask it */
	vol_pol_temp = Outbuf[2];
	vol_pol = vol_pol_temp & 0xF0;

    // Accomodate multiple MeasChars as applicable
    if(strcmp(m_MeasFunc, ATTRIB_VOLT_DC)==0) // (voltage)   dc signal
	{
			if (vol_pol == 0x20)			/* Checking if the voltage is normal */
			{
				data1 = vol_pol_temp & 0xF;
				data1 = data1 * 256;
				data2 = Outbuf[3];
				if (m_ModuleArray[0] == 10)
				{
					MeasValue = (data1 + data2) / 50.0;		/* Supply #10 has 20 mV/bit resolution */
				}
				else
				{
					MeasValue = (data1 + data2) / 100.0;	/* Supplies #1-#9 have 10 mV/bit resolution*/
				}

			}
			else if (vol_pol == 0xA0)		/* Checking if voltage is reversed */
			{
				data1 = vol_pol_temp & 0xF;
				data1 = data1 * 256;
				data2 = Outbuf[3];

				if (m_ModuleArray[0] == 10)
				{
					MeasValue = -((data1 + data2) / 50.0);
				}
				else
				{
					MeasValue = -((data1 + data2) / 100.0);
				}
			}
	}
    else if(strcmp(m_MeasFunc, ATTRIB_CURR_DC)==0) // (voltage)   dc signal
	{
			current_temp = Outbuf[0];
			data1 = current_temp & 0xF;
			data1 = data1 * 256;
			data2 = Outbuf[1];

			MeasValue = (data1 + data2) / 500.0;
    }
    

    if(Status)
    {
        MeasValue = FLT_MAX;
        ErrorAtXmlFreedom01502_Parallel(Status, m_ModuleArray[0],Response, BufferSize);
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
			atxmlw_ScalerDoubleReturn(m_MeasFunc, "V", MeasValue,
                   Response, BufferSize);
		}
		else if(strcmp(m_MeasFunc, ATTRIB_CURR_DC)==0) // (voltage)   dc signal
		{
			atxmlw_ScalerDoubleReturn(m_MeasFunc, "A", MeasValue,
                   Response, BufferSize);
		}
	}
	
  	ISDODEBUG(dodebug(0, "FetchAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: DisableAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::DisableAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int LastPS, 
		Status=0;
	int data1, data2, data3, tmp, tmp_ampl;
	char outbuf[30]="";
	
  	ISDODEBUG(dodebug(0, "DisableAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));
    // Open action for the AtXmlFreedom01502_Parallel
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenAtXmlFreedom01502_Parallel called "), Response, BufferSize);

	//set master voltage to 0
	tmp_ampl=0;
	data1 = 32 + m_ModuleArray[0];
	tmp = tmp_ampl & 0xF00;
	tmp = tmp >> 8;
	data2 = 80 + tmp;
	tmp = tmp_ampl & 0x0FF;
	data3 = tmp;

	Status = WriteToDCPowerSupply(m_Handle[m_ModuleArray[0]], data1, data2, data3, 100);

	//open all supplies
	LastPS = m_ModuleArray[0] + m_ModuleCount - 1;
	for(LastPS; LastPS >= m_ModuleArray[0]; LastPS--)
	{
		//close slaves and master output relays
		data1 = 32+LastPS;
		data2 = 0xA0;
		data3 = 0;

		Status = WriteToDCPowerSupply(m_Handle[m_ModuleArray[0]], data1, data2, data3, 100);
	}

	ISDODEBUG(dodebug(0, "DisableAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::EnableAtXmlFreedom01502_Parallel(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int LastPS,
		Status=0;
	int data1, data2, data3, tmp, tmp_ampl, tmp_curr;
	char outbuf[30]="";
	double dV=0.0;
	bool previousVoltagePositive[10];
	int i;
	bool setupError = false;
	
  	ISDODEBUG(dodebug(0, "EnableAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));

	for(i = 0; i<10; i++)
	{
		previousVoltagePositive[i] = s_VoltageParallelPositive[i];
	}

	// Close action for the AtXmlFreedom01502_Parallel
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableAtXmlFreedom01502_Parallel called "), Response, BufferSize);

    
	//set master voltage to 0
	tmp_ampl=0;
	data1 = 32 + m_ModuleArray[0];
	tmp = tmp_ampl & 0xF00;
	tmp = tmp >> 8;
	data2 = 80 + tmp;
	tmp = tmp_ampl & 0x0FF;
	data3 = tmp;	

	Status = WriteToDCPowerSupply(m_Handle[m_ModuleArray[0]], data1, data2, data3, 100);

	if (m_Action == ATXMLW_SA_ENABLE)
	{
		//close slave relays
		LastPS = m_ModuleArray[0] + m_ModuleCount - 1;
		for(LastPS; LastPS > m_ModuleArray[0]; LastPS--)
		{
			//set to slave mode
			data1 = 32+LastPS;
			data2 = 140;
			data3 = 0;
			
			Status = WriteToDCPowerSupply(m_Handle[m_ModuleArray[0]], data1, data2, data3, 100);

			if (m_Voltage.Exists)
			{
				if (m_Voltage.Real < 0)
				{					
					//first determine if polarity has already been set,
					//because if you try to set the reverse polarity again
					// supply will turn off
					if(!s_ReverseParallelPolarity[LastPS-1])
					{	
						//for now customer request that if you Atlas CHANGE from
						//a postive to a negative voltage it is illegal
						if( (s_VoltageParallelPositive[LastPS-1]) && (s_PreviousParallelVoltage[m_ModuleArray[0]-1] >= 0.0) )
						{
							setupError = true;
							// set Status for illegal setup
							Status = 3221159994;
							if(Status)
							{
								ErrorAtXmlFreedom01502_Parallel(Status, LastPS, Response, BufferSize);
							}
						}
                        else
                        {
                            s_VoltageParallelPositive[LastPS-1] = false;

							data1 = 32+LastPS;
							data2 = 128; // 0x80
							data3 = 3;

							Status = WriteToDCPowerSupply(m_Handle[m_ModuleArray[0]], data1, data2, data3, 100);

							s_ReverseParallelPolarity[LastPS-1] = true;	
                        }
					}					
				}
				else
				{
					//identify that 
                    s_VoltageParallelPositive[LastPS-1] = true;

                    // if previous voltage was a negative and this is positive (without resetting)
				    // caused by an ATLAS change modifier, adjust the polarity relay
				    //for now customer request that if you Atlas CHANGE from
				    //a negative to a postive voltage it is illegal 
				    //this will cause PPU to shut off
				    if((s_ReverseParallelPolarity[LastPS-1]) && (m_Voltage.Real > 0) && (s_PreviousParallelVoltage[m_ModuleArray[0]-1] < 0.0) )
				    {

					    s_ReverseParallelPolarity[LastPS-1] = false;
						
						setupError = true;
					    //for now customer request that if you Atlas CHANGE on same supply from
					    //a negative to a postive voltage it is illegal
					    // set Status for illegal setup					
					    Status = 3221159994;
					    if(Status)
						{
						    ErrorAtXmlFreedom01502_Parallel(Status, LastPS, Response, BufferSize);
						}
    									
				    }					
				}
				
			}// end if(m_Voltage.Exists)

			//close slave output relay
			data1 = 32+LastPS;
			data2 = 176;
			data3 = 0;
			
			Status = WriteToDCPowerSupply(m_Handle[LastPS], data1, data2, data3, 100);

		}

		if (m_Voltage.Exists)
		{
			if (m_Voltage.Real < 0)
			{
				//no check for firmware rev.

				//first determine if polarity has already been set,
				//because if you try to set the reverse polarity again
				// supply will turn off
				if(!s_ReverseParallelPolarity[LastPS-1])
				{
					
					//for now customer request that if you Atlas CHANGE from
					//a postive to a negative voltage it is illegal
					if( (s_VoltageParallelPositive[LastPS-1]) && (s_PreviousParallelVoltage[m_ModuleArray[0]-1] >= 0.0) )
					{
						setupError = true;
						// set Status for illegal setup
						Status = 3221159994;
						if(Status)
						{
							ErrorAtXmlFreedom01502_Parallel(Status, LastPS, Response, BufferSize);
						}
					}
                    else
                    {
                     	s_VoltageParallelPositive[LastPS-1] = false;

						data1 = 32+LastPS;
						data2 = 128; // 0x80
						data3 = 3;
						Status = WriteToDCPowerSupply(m_Handle[LastPS], data1, data2, data3, 100);

						s_ReverseParallelPolarity[LastPS-1] = true;							
			
                    }
				}
				
			}
            else
            {
                s_VoltageParallelPositive[LastPS-1] = true;

                // if previous voltage was a negative and this is positive (without resetting)
				    // caused by an ATLAS change modifier, adjust the polarity relay
				    //for now customer request that if you Atlas CHANGE from
				    //a negative to a postive voltage it is illegal 
				    //set voltage to zero and error out
				    if((s_ReverseParallelPolarity[LastPS-1]) && (m_Voltage.Real > 0) && (s_PreviousParallelVoltage[m_ModuleArray[0]-1] < 0.0) )
				    {
						setupError = true;					
					    Status = 3221159994;
					    if(Status)
						{
						    ErrorAtXmlFreedom01502_Parallel(Status, LastPS, Response, BufferSize);
						}
    									
				    }					
            }
			

		}
		//close master relay
		data1 = 32 + m_ModuleArray[0];
		data2 = 176;
		data3 = 0;

		Status = WriteToDCPowerSupply(m_Handle[LastPS], data1, data2, data3, 100);

	}
	
	//program for remote sense measurement on Master
	data1 = 32+m_ModuleArray[0];
	data2 = 131;
	data3 = 0;
	
	Status = WriteToDCPowerSupply(m_Handle[LastPS], data1, data2, data3, 100);

	// this is prevent a fault because of overcurrent during an ATLAS Change
	if(s_PreviousParallelCurrent[m_ModuleArray[0]-1] < m_CurrentLimit.Real) 
	{
		if (m_CurrentLimit.Exists)
		{
			tmp_curr = (int)(m_CurrentLimit.Real * 500);

			data1 = 32 + m_ModuleArray[0];
			tmp = tmp_curr & 0xF00;
			tmp = tmp >> 8;
			data2 = 64 + tmp;
			tmp = tmp_curr & 0x0FF;
			data3 = tmp;
			
			Status = WriteToDCPowerSupply(m_Handle[LastPS], data1, data2, data3, 100);

			s_PreviousParallelCurrent[m_ModuleArray[0]-1] = m_CurrentLimit.Real;
		}

		if (m_Voltage.Exists)
		{
			//if we had an error set the voltage to 0
			if(setupError)
			{
				m_Voltage.Real = 0;
			}

			dV = fabs(m_Voltage.Real);
			if (m_ModuleArray[0] != 10)
			{
				tmp_ampl = (int)(dV *100);
			}
			else
			{
				tmp_ampl = (int)(dV *50);
			}

				data1 = 32 + m_ModuleArray[0];
				tmp = tmp_ampl & 0xF00;
				tmp = tmp >> 8;
				data2 = 80 + tmp;
				tmp = tmp_ampl & 0x0FF;
				data3 = tmp;

				Status = WriteToDCPowerSupply(m_Handle[LastPS], data1, data2, data3, 100);

				s_PreviousParallelVoltage[m_ModuleArray[0]-1] = m_Voltage.Real;
		}
	}
	else
	{		
		if (m_Voltage.Exists)
		{
			// if we had set up error set voltage to zero
			if(setupError)
			{
				m_Voltage.Real = 0;
			}

			dV = fabs(m_Voltage.Real);
			if (m_ModuleArray[0] != 10)
			{
				tmp_ampl = (int)(dV *100);
			}
			else
			{
				tmp_ampl = (int)(dV *50);
			}

				data1 = 32 + m_ModuleArray[0];
				tmp = tmp_ampl & 0xF00;
				tmp = tmp >> 8;
				data2 = 80 + tmp;
				tmp = tmp_ampl & 0x0FF;
				data3 = tmp;

				Status = WriteToDCPowerSupply(m_Handle[LastPS], data1, data2, data3, 100);

				s_PreviousParallelVoltage[m_ModuleArray[0]-1] = m_Voltage.Real;
		}

		if (m_CurrentLimit.Exists)
		{
			tmp_curr = (int)(m_CurrentLimit.Real * 500);

			data1 = 32 + m_ModuleArray[0];
			tmp = tmp_curr & 0xF00;
			tmp = tmp >> 8;
			data2 = 64 + tmp;
			tmp = tmp_curr & 0x0FF;
			data3 = tmp;

			Status = WriteToDCPowerSupply(m_Handle[LastPS], data1, data2, data3, 100);

			s_PreviousParallelCurrent[m_ModuleArray[0]-1] = m_CurrentLimit.Real;
			
		}
	}
	
  	ISDODEBUG(dodebug(0, "EnableAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorAtXmlFreedom01502_Parallel
//
// Purpose: Query AtXmlFreedom01502_Parallel for the error text and send to WRTS
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
int  CAtXmlFreedom01502_Parallel_T::ErrorAtXmlFreedom01502_Parallel(int Status, int Supply, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    char     Msg[MAX_MSG_SIZE];
    int      QError;

    retval = Status;
    Msg[0] = '\0';

    if(Status)
    {
        if(Err)
		{
            sprintf(Msg,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);
		}

		Err = aps6062_errorMessage(m_Handle[Supply], (long)Status, Msg);

        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);

    }

    QError = 1;
    // Retrieve any pending errors in the device
    while(QError)
    {
        QError = 0;
        if(Err != 0)
		{
            break;
		}
        if(QError)
        {
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);
        }
    }
	
  	ISDODEBUG(dodebug(0, "ErrorAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::GetStmtInfoAtXmlFreedom01502_Parallel(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char LclElement[ATXMLW_MAX_NAME];
    char   LclResource[ATXMLW_MAX_NAME];
    double LclDblValue;
    char   LclUnit[ATXMLW_MAX_NAME];
    char  *LclCharValuePtr;

	ISDODEBUG(dodebug(0, "GetStmtInfoAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));

    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)))
	{
         return(Status);
	}

    //////// Place AtXmlFreedom01502_Parallel specific data here //////////////

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription,
                      Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action),
                              Response, BufferSize);

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource,
                      Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource),
                              Response, BufferSize);

    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]",
                              m_SignalName, m_SignalElement),
                              Response, BufferSize);
	}

	if (strcmp(m_SignalElement, "Limit") == 0)
	{
		//get attributes in <Limit/> tag
		if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "limit", &LclDblValue, LclUnit)))
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found DC_Level amplitude %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
		if (strcmp(LclUnit, "V")==0)
		{
			//NO CONSTANT CURRENT MODE
			//m_VoltageLimit.Real = LclDblValue;
			//m_VoltageLimit.Exists=true;
		}

		else if(strcmp(LclUnit, "A")==0)
		{
			m_CurrentLimit.Real = LclDblValue;
			m_CurrentLimit.Exists=true;
			m_CurrentLimit.Real = m_CurrentLimit.Real/m_ModuleCount;
		}

		if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "In", &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		}	

		//get attributes in <Constant/> tag
		if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found DC_Level Element [%s]",LclElement), Response, BufferSize);
		}
		if(strcmp(LclElement, "Constant")==0)
		{
			//m_Amplitude.Exists=true;
			if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, "amplitude",
                                         &LclDblValue, LclUnit)))
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found DC_Level amplitude %E [%s]",
                              LclDblValue, LclUnit),
                              Response, BufferSize);

			if (strcmp(LclUnit, "V")==0)
			{
				m_Voltage.Real = LclDblValue;
				m_Voltage.Exists=true;
			}
			else if(strcmp(LclUnit, "A")==0)
			{
				//NO CONSTANT CURRENT MODE
				//m_Current.Real = LclDblValue;
				//m_Current.Exists=true;
				//m_Current.Real = m_Current.Real/m_ModuleCount;
			}
		}
	}
	else if (strcmp(m_SignalElement, "Measure")==0) //no measuring in Parallel_T.cpp
	{
	//get attributes in <Measure/> tag
		if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "attribute",
                                         &LclCharValuePtr)))
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]",
                              m_SignalName, LclCharValuePtr),
                              Response, BufferSize);
		if(strcmp(LclCharValuePtr, "amplitude"))
		{/*m_Amplitude.Exists=true;*/}
		
	}

    atxmlw_Close1641Xml(&m_SignalDescription);
	
  	ISDODEBUG(dodebug(0, "GetStmtInfoAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateAtXmlFreedom01502_Parallel
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
void CAtXmlFreedom01502_Parallel_T::InitPrivateAtXmlFreedom01502_Parallel(void)
{
	int i;
    
  	ISDODEBUG(dodebug(0, "InitPrivateAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));

	m_Measure.Exists = false;
	m_CurrentLimit.Exists = false;
	m_Voltage.Exists = false;
	
	for(i=0; i<10; i++)
	{
		s_VoltageParallelPositive[i] = true;
		s_ReverseParallelPolarity[i] = false;
		s_PreviousParallelVoltage[i] = -1.0;
		s_PreviousParallelCurrent[i] = 0.0;
		s_PreviousParallelSupply[i] = -1;
	}

  	ISDODEBUG(dodebug(0, "InitPrivateAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataAtXmlFreedom01502_Parallel
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
void CAtXmlFreedom01502_Parallel_T::NullCalDataAtXmlFreedom01502_Parallel(void)
{
    ISDODEBUG(dodebug(0, "NullCalDataAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;

  	ISDODEBUG(dodebug(0, "NullCalDataAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: GetFirmRevAtXmlFreedom01502_Parallel
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
int CAtXmlFreedom01502_Parallel_T::GetFirmRevAtXmlFreedom01502_Parallel(int ModuleNum)
{
	int			data1,data2,data3, Status;
	char		outbuf[30]="";
	ViChar		Retbuf[100]="";
	ViInt32		retCount;

  	ISDODEBUG(dodebug(0, "GetFirmRevAtXmlFreedom01502_Parallel()", "Entering function", (char*)0));	

	/* Status Query */
	data1 = ModuleNum;			  /* x'0X' */
	data2 = 68;               /* x'44' */
	data3 = 0;                /* x'00' */
	Status = WriteToDCPowerSupply(m_Handle[ModuleNum], ModuleNum, 68, 0, 200);

	IFNSIM(aps6062_readInstrData(m_Handle[ModuleNum], 100, Retbuf, &retCount));

  	ISDODEBUG(dodebug(0, "GetFirmRevAtXmlFreedom01502_Parallel()", "Leaving function", (char*)0));

	return (Retbuf[3]);
}
//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

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
int CAtXmlFreedom01502_Parallel_T::WriteToDCPowerSupply(ViSession InstHandle, int DataElement1,
													int DataElement2, int DataElement3, int SleepValue)
{

	int		Status = 0;
	char	OutBuf[30] = {0};

  	ISDODEBUG(dodebug(0, "WriteToDCPowerSupply()", "Entering function", (char*)0));

	sprintf(OutBuf, "%c%c%c", DataElement1, DataElement2, DataElement3);
	IFNSIM(Status = aps6062_writeInstrData(InstHandle, OutBuf));
	Sleep(SleepValue);

  	ISDODEBUG(dodebug(0, "WriteToDCPowerSupply()", "Leaving function", (char*)0));
	return(Status);
}
