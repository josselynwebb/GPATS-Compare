//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    SBSGigabit_T.cpp
//
// Date:	    20FEB06
//
// Purpose:	    ATXMLW Instrument Driver for SBSGigabit
//
// Instrument:	SBSGigabit  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
// Revision History
// Rev	     Date                  Reason					  AUTHOR
// =======  =======  =======================================  =================
// 1.0.0.0  20FEB06  Baseline								  D. Bubenik, EADS North America Defense
///////////////////////////////////////////////////////////////////////////////
// Includes
#include <winsock2.h>
#include <iphlpapi.h>
#include <NetCon.h>
#include <stdio.h>
#include "visa.h"
#include "AtxmlWrapper.h"
#include "SBSGigabit_T.h"

//Macros
#define MALLOC(x) HeapAlloc(GetProcessHeap(), 0, (x)) 
#define FREE(x) HeapFree(GetProcessHeap(), 0, (x))


// Local Defines
#define TCP		1
#define UDP		2

#define DATA    1
#define ALL		2
// Function codes

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables

// Local Function Prototypes
void stuff_string(char * string);
void unstuff_string(char * string);
void get_error_msg(int error, char *msg);
void RemoveSpaces(char* source);
int DE_BUG = 0;
FILE *debugfp = 0;
//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CSBSGigabit_T
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
CSBSGigabit_T::CSBSGigabit_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    char LclMsg[1024];
    int Status = 0;

	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Entering-CSBSGigabit_T called", (char*)NULL));
    // Save Device Info
    m_InstNo = Instno;
    m_ResourceType = ResourceType;
    m_ResourceName[0] = '\0';
    if(ResourceName)
    {
        strnzcpy(m_ResourceName,ResourceName,ATXMLW_MAX_NAME);
    }
    m_Sim = Sim;
    m_Dbg = Dbglvl;
    // Save Address Information
    if(AddressInfoPtr)
    {
        m_AddressInfo.ResourceAddress = AddressInfoPtr->ResourceAddress;
        if(AddressInfoPtr->InstrumentQueryID)
        {
            strnzcpy(m_AddressInfo.InstrumentQueryID, 
                         AddressInfoPtr->InstrumentQueryID, ATXMLW_MAX_NAME);
        }
        m_AddressInfo.InstrumentTypeNumber = AddressInfoPtr->InstrumentTypeNumber;
        if(AddressInfoPtr->ControllerType)
        {
            strnzcpy(m_AddressInfo.ControllerType, 
                         AddressInfoPtr->ControllerType, ATXMLW_MAX_NAME);
        }
        m_AddressInfo.ControllerNumber = AddressInfoPtr->ControllerNumber;
        m_AddressInfo.PrimaryAddress = AddressInfoPtr->PrimaryAddress;
        m_AddressInfo.SecondaryAddress = AddressInfoPtr->SecondaryAddress;
        m_AddressInfo.SubModuleAddress = AddressInfoPtr->SubModuleAddress;
    }


    m_Handle= NULL;
    m_InitString[0] = '\0';

    InitPrivateSBSGigabit();
	NullCalDataSBSGigabit();

    // The Form Init String
    sprintf(m_InitString,"%s", m_AddressInfo.ControllerType);
    sprintf(LclMsg,"Wrap-CSBSGigabit Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-CSBSGigabit Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg, (char*)NULL));

    // Initialize the SBSGigabit

	if (WSAStartup(MAKEWORD(1, 1), &m_wsaData) != 0) 
	{ 
            fprintf(stderr, "WSAStartup failed.\n"); 
            exit(1); 
    } 
	WSASetLastError(0);

    if(Status && ErrorSBSGigabit(Status, Response, Buffersize))
        return;

	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Leaving-CSBSGigabit_T called", (char*)NULL));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CSBSGigabit_T()
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
CSBSGigabit_T::~CSBSGigabit_T()
{
    char Dummy[1024];

    // Reset the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CSBSGigabit Class Distructor called "),Dummy,1024);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-~CSBSGigabit Class Distructor called", (char*)NULL));

	if(m_TransType==TCP)
	{
		IFNSIM(closesocket(m_Handle));
	}
	WSACleanup();

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusSBSGigabit
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
int CSBSGigabit_T::StatusSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";

    // Status action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusSBSGigabit called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-StatusSBSGigabit called", (char*)NULL));
    // Check for any pending error messages
    Status = ErrorSBSGigabit(0, Response, BufferSize);

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalSBSGigabit
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
int CSBSGigabit_T::IssueSignalSBSGigabit(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

    // IEEE 1641 Issue Signal action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalSBSGigabit Signal: "), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-IssueSignalSBSGigabit called", (char*)NULL));
    if((Status = GetStmtInfoSBSGigabit(SignalDescription, Response, BufferSize)) != 0)
	{
        return(Status);
	}

    switch(m_Action)
    {
		case ATXMLW_SA_APPLY:
			if((Status = SetupSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			if((Status = EnableSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_REMOVE:
			if((Status = DisableSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			if((Status = ResetSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_MEASURE:
			if((Status = SetupSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_READ:
			if((Status = EnableSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			if((Status = FetchSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_RESET:
			if((Status = ResetSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_SETUP:
			if((Status = SetupSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_CONNECT:
			break;
		case ATXMLW_SA_ENABLE:
			if((Status = EnableSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_DISABLE:
			if((Status = DisableSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_FETCH:
			if((Status = FetchSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_DISCONNECT:
			break;
		case ATXMLW_SA_STATUS:
			if((Status = StatusSBSGigabit(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
    }
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-IssueSignalSBSGigabit leaving", (char*)NULL));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalSBSGigabit
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
int CSBSGigabit_T::RegCalSBSGigabit(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
    char      Dummy[1024];

    // Setup action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalSBSGigabit CalData: %s", CalData),Dummy,1024);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-RegCalSBSGigabit CalData: %s", CalData, (char*)NULL));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetSBSGigabit
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
int CSBSGigabit_T::ResetSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";

    // Reset action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetSBSGigabit called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "ResetSBSGigabit called", (char*)NULL));
	// Reset the SBSGigabit
	if(m_TransType==TCP)
	{
		IFNSIM(closesocket(m_Handle));
	}
    if(Status)
	{
        ErrorSBSGigabit(Status, Response, BufferSize);
	}

	WSASetLastError(0);
    InitPrivateSBSGigabit();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstSBSGigabit
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
int CSBSGigabit_T::IstSBSGigabit(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	char cmdBuffer[1024] = {""};
	char resp[512];
	char retstats[256];

    // Reset action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstSBSGigabit called Level %d", Level), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "IstSBSGigabit called Level %d", Level, (char*)NULL));
    switch(Level)
    {
		case ATXMLW_IST_LVL_PST:
			Status = StatusSBSGigabit(Response,BufferSize);
			break;
		case ATXMLW_IST_LVL_IST:
			break;
		case ATXMLW_IST_LVL_CNF:
			sprintf(cmdBuffer, "netsh interface show interface name=\"%s\"", m_InitString);
			if(system(cmdBuffer))
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
							   Status, "");
				Status = -1;
			}
			else
			{
				sprintf(resp, "<Value>\n<c:Datum xsi:type=\"c:string\" unit=\"Null\" value=\"status clean see return info\"/>\n</Value>\n");
				strcpy(retstats, resp);
				strcat(retstats, " </ReturnData>\n</AtXmlResponse>\n \0");
				BufferSize = strlen(retstats);
				strncpy(Response, retstats, BufferSize);
				break;
			}
		default: 
			break;
    }

    if(Status)
        ErrorSBSGigabit(Status, Response, BufferSize);

    InitPrivateSBSGigabit();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsSBSGigabit
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
int CSBSGigabit_T::IssueNativeCmdsSBSGigabit(ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsSBSGigabit "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "IssueNativeCmdsSBSGigabit", (char*)NULL));

    // Retrieve the CalData
    if((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
        return(Status);
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallSBSGigabit
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
int CSBSGigabit_T::IssueDriverFunctionCallSBSGigabit(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the SBSGigabit
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallSBSGigabit", Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-IssueDriverFunctionCallSBSGigabit", (char*)NULL));

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupSBSGigabit
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
int CSBSGigabit_T::SetupSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupSBSGigabit called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-SetupSBSGigabit called", (char*)NULL));

	if(!m_LocalIP.Exists)//enable DHCP
	{
		IFNSIM(Status = set_ip(m_InitString, NULL, NULL, NULL, true));		
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "In SetupSBSGigabit calling set_ip status is [%d]", Status, (char*)NULL));
	}
	else
	{	
		IFNSIM(Status = set_ip(m_InitString, m_LocalIP.Address, m_LocalMask.Address, m_LocalGateway.Address));
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "In SetupSBSGigabit calling set_ip status is [%d]", Status, (char*)NULL));
	}

	if(m_LinkSpeed.Exists)
	{
		set_LinkSpeed(m_LinkSpeed.Address);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "In SetupSBSGigabit calling set_LinkSpeed", (char*)NULL));
	}
	
	//Commented Out. Sometimes Netsh return an error even though the operation completed successfully. 06/23/2017
    /*if(Status < 0)
        ErrorSBSGigabit(Status, Response, BufferSize);*/

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchSBSGigabit
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
int CSBSGigabit_T::FetchSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5000;
    char   *ErrMsg = "";
	timeval RecvTimeout;
	int GetData=1;
	char attribute[10];
	sockaddr_in address;
	int size=sizeof(address);

	
	fd_set fds;
	FD_ZERO(&fds);
	FD_SET(m_Handle, &fds);
	if(m_Sim)
	{
		strcpy(m_RetData, "THIS IS MY TEST MESSAGE");
	}

    // Fetch action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchSBSGigabit called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-FetchSBSGigabit called ", (char*)NULL));

	if(m_Attribute.Dim==DATA)
	{
		// Fetch data
		if(m_TransType==TCP)
		{
			
			ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-FetchSBSGigabit called trans type TCP ", (char*)NULL));
			if(m_MaxTime.Exists)
			{
				// Set timeout
				RecvTimeout.tv_sec  = 0;
				RecvTimeout.tv_usec = (long)(m_MaxTime.Real*100000);

				IFNSIM(GetData=select(0, &fds, NULL, NULL, &RecvTimeout));
				if(GetData==SOCKET_ERROR)
					ErrorSBSGigabit(GetData, Response, BufferSize);
			}

			if(GetData>0)
			{
				m_RetData[0]='\0';
				IFNSIM(Status=recv(m_Handle, m_RetData, MAX_DATA, 0));
				if(Status==MAX_DATA)
					Status=MAX_DATA-1;
				m_RetData[Status]='\0';
			}
			else
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
								Status, "No message received");
				m_RetData[0]='\0';
			}
			IFNSIM(closesocket(m_Handle));
		}
		else
		{
			m_RetData[0]='\0';
			IFNSIM(Status= recieve_udp(m_RemoteIP.Address, (unsigned short) m_RemotePort.Int, m_RetData, MAX_DATA, Response, BufferSize));
			IFNSIM(closesocket(m_Handle));
		}
	}	

	// Fetch data
	switch(m_Attribute.Dim)
	{
	case DATA:
		stuff_string(m_RetData);
		strcpy(attribute, "data");
		break;
	}

   if(Status)
    {
     //   MeasValue = FLT_MAX;
    }

    else
    {
        sscanf(ErrMsg,"%E",&MeasValue);
    }

	if(Response && (BufferSize > (int)(strlen(Response)+200)))
	{
		if(m_Attribute.Dim==DATA)
		{
			atxmlw_ScalerStringReturn(attribute, "", m_RetData,
						Response, BufferSize);
		}

		else
		{
			if(m_TransType==TCP)
			{
				ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-FetchSBSGigabit called trans type TCP ", (char*)NULL));
				if(getpeername(m_Handle, (sockaddr *)&address, &size)!=SOCKET_ERROR)
				{
					atxmlw_ScalerStringReturn("remoteIP", "", inet_ntoa(address.sin_addr),
						Response, BufferSize);
					atxmlw_ScalerIntegerReturn("remotePort", "", ntohs(address.sin_port),
						Response, BufferSize);

				}

				else //return local settings
				{
					if(m_RemoteIP.Exists)
					{
						atxmlw_ScalerStringReturn("remoteIP", "", m_RemoteIP.Address,
							Response, BufferSize);
						atxmlw_ScalerIntegerReturn("remotePort", "", m_RemotePort.Int,
							Response, BufferSize);
					}
				}
				atxmlw_ScalerStringReturn("spec", "", "TCP",
						Response, BufferSize);
			}
			else if(m_TransType==UDP)
			{
				ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-FetchSBSGigabit called trans type UDP ", (char*)NULL));
				atxmlw_ScalerStringReturn("remoteIP", "", m_RemoteIP.Address,
						Response, BufferSize);
				atxmlw_ScalerIntegerReturn("remotePort", "", m_RemotePort.Int,
						Response, BufferSize);
				atxmlw_ScalerStringReturn("spec", "", "UDP",
						Response, BufferSize);
			}

			if(getsockname(m_Handle, (sockaddr *)&address, &size)!=SOCKET_ERROR)
			{
				atxmlw_ScalerStringReturn("localIP", "", inet_ntoa(address.sin_addr),
					Response, BufferSize);
			}

			else //return local settings
			{
				if(m_LocalIP.Exists)
				{
					atxmlw_ScalerStringReturn("localIP", "", m_LocalIP.Address,
						Response, BufferSize);
				}
			}

			if(m_LocalMask.Exists)
			{
				atxmlw_ScalerStringReturn("localSubnetMask", "", m_LocalMask.Address,
					Response, BufferSize);
			}

			if(m_LocalGateway.Exists)
			{
				atxmlw_ScalerStringReturn("localGateway", "", m_LocalGateway.Address,
					Response, BufferSize);
			}

			else
			{
				sprintf(m_LocalGateway.Address, "%s", "1");
			}

			if(m_MaxTime.Exists)
			{
				atxmlw_ScalerDoubleReturn("maxTime", "", m_MaxTime.Real,
						Response, BufferSize);
			}
		}
	}
	return(0);

}



///////////////////////////////////////////////////////////////////////////////
// Function: DisableSBSGigabit
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
int CSBSGigabit_T::DisableSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    // Open action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-DisableSBSGigabit called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-DisableSBSGigabit called ", (char*)NULL));

    if(m_TransType==TCP)
	{
		IFNSIM(closesocket(m_Handle));
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableSBSGigabit
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
int CSBSGigabit_T::EnableSBSGigabit(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status=0;
    // Close action for the SBSGigabit
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableSBSGigabit called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-EnableSBSGigabit called ", (char*)NULL));

	if(m_TransType==TCP)//set up connection
	{
		IFNSIM(m_Handle = connect_by_name(m_RemoteIP.Address, (unsigned short) m_RemotePort.Int, Response, BufferSize));
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-EnableSBSGigabit trans type TCP ", (char*)NULL));
	}
	if(m_DataSize.Exists)//send Data
	{
		if(m_TransType==TCP)//set up connection
		{
			m_DataSize.Int=strlen(m_Data);
			IFNSIM(Status=send(m_Handle, m_Data, m_DataSize.Int, 0));
		}
		else
		{
			IFNSIM(Status = send_udp(m_RemoteIP.Address, (unsigned short) m_RemotePort.Int, m_Data, Response, BufferSize));
		}
	}
	else
	{
		if(m_TransType==UDP)
		{
			IFNSIM(m_Handle= bind_local(false, Response, BufferSize));
			ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-EnableSBSGigabit  trans type UDP ", (char*)NULL));
		}
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorSBSGigabit
//
// Purpose: Query SBSGigabit for the error text and send to WRTS
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
int  CSBSGigabit_T::ErrorSBSGigabit(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    char     Msg[MAX_MSG_SIZE];
    int      QError;

    retval = Status;
    Msg[0] = '\0';

    if(Status)
    {
		Err=WSAGetLastError();
		WSASetLastError(0);  //remove error from tables

        // Decode SBSGigabit lib return code
		get_error_msg(Err, Msg);

        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
                           Status, Msg);

    }

    QError = 1;
    // Retrieve any pending errors in the device
    while(QError)
    {
        QError = 0;

		QError=WSAGetLastError();
        if(QError)
        {
			WSASetLastError(0);  //remove error from tables
			// Decode SBSGigabit lib return code
			get_error_msg(Err, Msg);

			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
							Status, Msg);
        }
    }


    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoSBSGigabit
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
int CSBSGigabit_T::GetStmtInfoSBSGigabit(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int    Status = 0;
    char   LclResource[ATXMLW_MAX_NAME];
    double LclDblValue;
    char   LclUnit[ATXMLW_MAX_NAME]="";
    char  *LclCharValuePtr;
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap-GetStmtInfoSBSGigabit called ", (char*)NULL));
    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription,  Response, BufferSize)) != 0)
	{
         return(Status);
	}

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found Action %d", m_Action, (char*)NULL));

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource), Response, BufferSize);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found Resource [%s]", LclResource, (char*)NULL));

    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement, (char*)NULL));
	}

	if(strcmp(m_SignalElement, "UDP")==0)
	{
		m_TransType=UDP;
	}
	else
	{
		m_TransType=TCP;
	}
	
	//Local IP
	if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "localIP", &LclCharValuePtr)))
	{
		m_LocalIP.Exists=true;
		strcpy(m_LocalIP.Address, LclCharValuePtr);
		//trim whitespace
		RemoveSpaces(m_LocalIP.Address);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s localIP [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found %s localIP [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//Local Subnet
	if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "localSubnetMask", &LclCharValuePtr)))
	{
		m_LocalMask.Exists=true;
		strcpy(m_LocalMask.Address, LclCharValuePtr);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s localSubnetMask [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found %s localSubnetMask [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//Local Gateway
	if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "localGateway", &LclCharValuePtr)))
	{
		m_LocalGateway.Exists=true;
		strcpy(m_LocalGateway.Address, LclCharValuePtr);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s localGateway [%s]", m_SignalName, LclCharValuePtr),  Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found %s localGateway [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//Remote IP
	if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "remoteIP", &LclCharValuePtr)))
	{
		m_RemoteIP.Exists=true;
		strcpy(m_RemoteIP.Address, LclCharValuePtr);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s remoteIP [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found %s remoteIP [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//Remote Port
    if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "remotePort", &LclDblValue, LclUnit)))
	{
		m_RemotePort.Exists=true;
		m_RemotePort.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found remotePort %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found remotePort %E [%s]", LclDblValue,LclUnit, (char*)NULL));
	}

	//data
	if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "data_bits", &LclCharValuePtr)))
	{
		m_DataSize.Exists=true;
		strcpy(m_Data, LclCharValuePtr);
		unstuff_string(&m_Data[0]);
		//bin_string_to_array(LclCharValuePtr, &m_Data[0], &m_DataSize.Int);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s data [%s]", m_SignalName, m_Data), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found %s data [%s]", m_SignalName, m_Data, (char*)NULL));
	}

	//maxTime
    if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "maxTime", &LclDblValue, LclUnit)))
	{
		m_MaxTime.Exists=true;
		m_MaxTime.Real=LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Max-Time %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found Max-Time %E [%s]", LclDblValue,LclUnit, (char*)NULL));
	}

	//LinkSpeed
    if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "linkSpeed", &LclCharValuePtr)))
	{
		m_LinkSpeed.Exists = true;
		strcpy(m_LinkSpeed.Address, LclCharValuePtr);

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s linkSpeed [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found %s linkSpeed [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//attribute
	if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "attribute", &LclCharValuePtr)))
	{
		m_Attribute.Exists=true;
		if(strcmp(LclCharValuePtr, "data")==0)
		{
			m_Attribute.Dim=DATA;
		}
		else if(strcmp(LclCharValuePtr, "config")==0)
		{
			m_Attribute.Dim=ALL;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s attribute [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "WrapGSI-Found %s attribute [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}
	
	atxmlw_Close1641Xml(&m_SignalDescription);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateSBSGigabit
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
void CSBSGigabit_T::InitPrivateSBSGigabit(void)
{
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap InitPrivateSBSGigabit-Called", (char*)NULL));
	m_LocalIP.Exists=false;
	m_LocalMask.Exists=false;
	m_LocalGateway.Exists=false;
	m_RemoteIP.Exists=false;
	m_RemotePort.Exists=false;
	m_DataSize.Exists=false;
	m_MaxTime.Exists=false;
	m_Attribute.Exists=false;
	m_LinkSpeed.Exists = false;
	m_TransType=TCP;
	m_RetData[0]='\0';
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataSBSGigabit
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
void CSBSGigabit_T::NullCalDataSBSGigabit(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
    return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: connect_by_name()
//
// Purpose: Creates and connects a TCP socket to the specified address.  The
//           socket returned is ready for send or recv commands.  When transfer
//           is complete, socket must be closed manually with the close() command
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ip_or_fqdn       const char *    location socket is connected to
// Port             unsigned short  Port socket is on
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: SOCKET
//
///////////////////////////////////////////////////////////////////////////////
SOCKET CSBSGigabit_T::connect_by_name(const char * ip_or_fqdn, unsigned short port, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	sockaddr_in address;
	hostent *get_ip;
	SOCKET s;
	int status;
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap connect_by_name-Called", (char*)NULL));
	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	Sleep(2000);
	address.sin_addr.S_un.S_addr=inet_addr(m_LocalIP.Address); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	s=socket(AF_INET, SOCK_STREAM, 0);
	if(s==INVALID_SOCKET)
		ErrorSBSGigabit(s, Response, BufferSize);

	if(m_MaxTime.Exists)
	{
		//setsockopt(s, NSPROTO_IPX, SO_RCVTIMEO, &m_MaxTime.Real, sizeof m_MaxTime.Real);
	}
	
	status=bind(s, (sockaddr *)&address, sizeof(sockaddr));
	if(status==SOCKET_ERROR)
	{
		ErrorSBSGigabit(status, Response, BufferSize);
	}

	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	Sleep(2000);
	address.sin_addr.S_un.S_addr=inet_addr(ip_or_fqdn); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	status=connect(s, (sockaddr *)&address, sizeof(sockaddr)); 
	if(status==SOCKET_ERROR)
	{
		ErrorSBSGigabit(status, Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap connect_by_name-Leaving", (char*)NULL));
	return s;
}
///////////////////////////////////////////////////////////////////////////////
// Function: send_udp()
//
// Purpose: Sends a piece of udp data
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ip_or_fqdn       const char *    location data is going to
// Port             unsigned short  Port data is going on
// send_word        const char *    Word being sent
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: int
//
///////////////////////////////////////////////////////////////////////////////
int CSBSGigabit_T::send_udp(const char * ip_or_fqdn, unsigned short port, const char * send_word, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	sockaddr_in address;
	int Status;
	hostent *get_ip;
	SOCKET s;
	int retval;
	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(m_LocalIP.Address); 

	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap send_udp-Called", (char*)NULL));
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	s=socket(AF_INET, SOCK_DGRAM, 0);
	if(s==INVALID_SOCKET)
	{
		ErrorSBSGigabit(s, Response, BufferSize);
	}

	Status=bind(s, (sockaddr *)&address, sizeof(sockaddr));
	if(Status==SOCKET_ERROR)
	{
		ErrorSBSGigabit(Status, Response, BufferSize);
	}


	address.sin_family = AF_INET;         // host byte order 
	address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(ip_or_fqdn); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(m_RemoteIP.Address);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	retval=sendto(s, send_word, (int)strlen(send_word), 0, (sockaddr *)&address, sizeof(sockaddr));
	if(retval==SOCKET_ERROR)
	{
		ErrorSBSGigabit(retval, Response, BufferSize);
	}

	closesocket(s);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap send_udp-Leaving", (char*)NULL));
	return retval;
}
///////////////////////////////////////////////////////////////////////////////
// Function: recieve_udp()
//
// Purpose: Recieves a piece of udp data
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// ip_or_fqdn       const char *    location data is coming from
// Port             unsigned short  Port data is coming on
// max_len          int             Size of data buffer being passed in
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// recieve_word     char *          Word recieved from transfer
//
// Return: int
//
///////////////////////////////////////////////////////////////////////////////
int CSBSGigabit_T::recieve_udp(const char * ip_or_fqdn, unsigned short port, char * recieve_word, int max_len, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	sockaddr_in address;
	hostent *get_ip;
	int temp, retval;
	timeval RecvTimeout;
	int GetData=1;

	fd_set fds;
	  
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap recieve_udp-Called", (char*)NULL));
	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(m_LocalIP.Address); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	address.sin_family = AF_INET;         // host byte order 
	address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(ip_or_fqdn); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(m_RemoteIP.Address);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	temp=sizeof(sockaddr);

	if(m_MaxTime.Exists)
	{
		FD_ZERO(&fds);
		FD_SET(m_Handle, &fds);

		// Set timeout
		RecvTimeout.tv_sec  = 0;
		RecvTimeout.tv_usec = (long)(m_MaxTime.Real*100000);

		GetData=select(0, &fds, NULL, NULL, &RecvTimeout);
		if(GetData==SOCKET_ERROR)
			ErrorSBSGigabit(GetData, Response, BufferSize);
	}

	if(GetData>0)
	{
		retval= recvfrom(m_Handle, recieve_word, max_len, 0, (sockaddr *)&address, &temp);
		if(retval==max_len)
			retval=max_len-1;
		recieve_word[retval]='\0';
	}
	else
	{
		retval=0;
		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
                           0, "No message recieved");
	}
	closesocket(m_Handle);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap recieve_udp-Leaving", (char*)NULL));
	return retval;
}
///////////////////////////////////////////////////////////////////////////////
// Function: bind_local()
//
// Purpose: Binds an internet port to a socket
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// TCP              bool            true if TCP, false if UDP
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: SOCKET
//
///////////////////////////////////////////////////////////////////////////////
SOCKET CSBSGigabit_T::bind_local(bool TCPPORT, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	sockaddr_in address;
	hostent *get_ip;
	SOCKET s;
	int status;

	address.sin_family = AF_INET;         // host byte order 
	address.sin_port = htons(m_RemotePort.Int);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(m_LocalIP.Address); 
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap bind_local-Called", (char*)NULL));
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(m_LocalIP.Address);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	if(TCPPORT)
	{
		s=socket(AF_INET, SOCK_STREAM, 0);
	}
	else
	{
		s=socket(AF_INET, SOCK_DGRAM, 0);
	}

	if(s==INVALID_SOCKET)
	{
		ErrorSBSGigabit(s, Response, BufferSize);
	}

	if(m_MaxTime.Exists)
	{
		//setsockopt(s, NSPROTO_IPX, SO_RCVTIMEO, &m_MaxTime.Real, sizeof m_MaxTime.Real);
	}
	
	status=bind(s, (sockaddr *)&address, sizeof(sockaddr));
	if(status==SOCKET_ERROR)
	{
		ErrorSBSGigabit(status, Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap bind_local-Leaving", (char*)NULL));
	return s;
}

///////////////////////////////////////////////////////////////////////////////
// Function: set_ip()
//
// Purpose: Changes the IP setting of the network card
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Interface        char *          The windows "friendly" name for the NIC
// IP               char *          New IP address of card
// Subnet           char *          New Subnet of card
// Gateway          char *          New Gateway of card
// DHCP             bool            set to true to use DHCP(all other parameters will be ignored)
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: int
//
///////////////////////////////////////////////////////////////////////////////
int CSBSGigabit_T::set_ip(char *Interface, char *IP, char *Subnet, char *Gateway, bool DHCP)
{
	char	command[256];
	char	tmpCommandBuf[256];
	int		status = 0;

	memset(command, '\0', sizeof(command));
	memset(tmpCommandBuf, '\0', sizeof(tmpCommandBuf));
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap set_ip-Called", (char*)NULL));
	RemoveSpaces(IP);
	
	if(DHCP) 
	{
		sprintf(tmpCommandBuf, "netsh interface ip set address %s dhcp", Interface);
	}
	else 
	{
		sprintf(tmpCommandBuf, "netsh interface ip set address %s static %s %s %s 1", Interface, IP, Subnet, Gateway);
	}

	sprintf(command, tmpCommandBuf);
	status = system(command);
	Sleep(4000);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap set_ip-Leaving", (char*)NULL));
	return status;
}

///////////////////////////////////////////////////////////////////////////////
// Function: set_LinkSpeed()
//
// Purpose: Sets the Link Speed
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// LinkSpeed        char *          used to set speed for powershell command
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: int
//
///////////////////////////////////////////////////////////////////////////////
int CSBSGigabit_T::set_LinkSpeed(char *LinkSpeed)
{
	char	command[256] = {""};
	char	tmpCommandBuf[256] = {""};
	char	fullCommand[1024] = {""};
	int		status = 0;
	// STARTUPINFO siStartupInfo;  // Not Used
	// PROCESS_INFORMATION piProcessInfo;  // Not Used

	memset(command, '\0', sizeof(command));
	memset(tmpCommandBuf, '\0', sizeof(tmpCommandBuf));
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap set_LinkSpeed-Called", (char*)NULL));
	if(!stricmp(LinkSpeed, "Auto"))
	{
		strcpy(command, "/C powershell -command .\\Auto.ps1");
	}

	else if (!stricmp(LinkSpeed, "10Mbps"))
	{
		strcpy(command, "/C powershell -command .\\10Mbps.ps1");
	}

	strcat(fullCommand, "\"C:\\Windows\\SysWoW64\\cmdin64.exe\" ");
	strcat(fullCommand, command);
	system(fullCommand);

	Sleep(4000);
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap set_LinkSpeed-Leaving", (char*)NULL));
	return status;
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: stuff_string()
//
// Purpose: Finds any " and replaces with 0x02 
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          string to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          Null terminated string with escape characters
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void stuff_string(char * string)
{
	for(int i=0; i<(int)strlen(string); i++)
	{
		if(string[i]=='"')
		{
			string[i]=0x02;
		}
	}
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: unstuff_string()
//
// Purpose: Finds any 0x02 and replaces it with "
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          string to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          Null terminated string without escape characters
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void unstuff_string(char * string)
{
	int j, length=(int)strlen(string);

	for(int i=0; i<length; i++)
	{
		if(string[i]==0x02)
		{
			string[i]='"';
		}

		if(string[i]=='\\')
		{
			if(string[i+1]=='n')
			{
				string[i]=0x0A;
				for(j=i+1;j<length;j++)
				{
					string[j]=string[j+1];
				}
				length--;
			}
			else if(string[i+1]=='t')
			{
				string[i]=0x09;
				for(j=i+1;j<length;j++)
				{
					string[j]=string[j+1];
				}
				length--;
			}
			else if(string[i+1]=='\\')
			{
				for(j=i;j<length;j++)
				{
					string[j]=string[j+1];
				}
				length--;
			}
		}
	}
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: get_error_msg()
//
// Purpose: Returns the message associated with a winsock error code
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// error            int             Error code returned by WSAGetLastError
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          Message associated with error
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void get_error_msg(int error, char *msg)
{
	switch(error)
	{
		case 0:			// do nothing
			break;
		case WSAEINTR:
			strcpy(msg, "Interrupted function call.");
			break;
		case WSAEACCES:	
			strcpy(msg, "Permission denied.");
			break;
		case WSAEFAULT:	
			strcpy(msg, "Bad address.");
			break;
		case WSAEINVAL:
			strcpy(msg, "Invalid argument.");
			break;
		case WSAEMFILE:
			strcpy(msg, "Too many open files.");
			break;
		case WSAEWOULDBLOCK:
			strcpy(msg, "Resource temporarily unavailable.");
			break;
		case WSAEINPROGRESS:
			strcpy(msg, "Operation now in progress.");
			break;
		case WSAEALREADY:
			strcpy(msg, "Operation already in progress.");
			break;
		case WSAENOTSOCK:
			strcpy(msg, "Socket operation on nonsocket.");
			break;
		case WSAEDESTADDRREQ:
			strcpy(msg, "Destination address required.");
			break;
		case WSAEMSGSIZE:
			strcpy(msg, "Message too long.");
			break;
		case WSAEPROTOTYPE:
			strcpy(msg, "Protocol wrong type for socket.");
			break;
		case WSAENOPROTOOPT:
			strcpy(msg, "Bad protocol option.");
			break;
		case WSAEPROTONOSUPPORT:
			strcpy(msg, "Protocol not supported.");
			break;
		case WSAESOCKTNOSUPPORT:
			strcpy(msg, "Socket type not supported.");
			break;
		case WSAEOPNOTSUPP:
			strcpy(msg, "Operation not supported.");
			break;
		case WSAEPFNOSUPPORT:
			strcpy(msg, "Protocol family not supported.");
			break;
		case WSAEAFNOSUPPORT:
			strcpy(msg, "Address family not supported by protocol family.");
			break;
		case WSAEADDRINUSE:
			strcpy(msg, "Address already in use.");
			break;
		case WSAEADDRNOTAVAIL:
			strcpy(msg, "Cannot assign requested address.");
			break;
		case WSAENETDOWN:
			strcpy(msg, "Network is down.");
			break;
		case WSAENETUNREACH:
			strcpy(msg, "Network is unreachable.");
			break;
		case WSAENETRESET:
			strcpy(msg, "Network dropped connection on reset.");
			break;
		case WSAECONNABORTED:
			strcpy(msg, "Software caused connection abort.");
			break;
		case WSAECONNRESET:
			strcpy(msg, "Connection reset by peer.");
			break;
		case WSAENOBUFS:
			strcpy(msg, "No buffer space available.");
			break;
		case WSAEISCONN:
			strcpy(msg, "Socket is already connected.");
			break;
		case WSAENOTCONN:
			strcpy(msg, "Socket is not connected.");
			break;
		case WSAESHUTDOWN:
			strcpy(msg, "Cannot send after socket shutdown.");
			break;
		case WSAETIMEDOUT:
			strcpy(msg, "Connection timed out.");
			break;
		case WSAECONNREFUSED:
			strcpy(msg, "Connection refused.");
			break;
		case WSAEHOSTDOWN:
			strcpy(msg, "Host is down.");
			break;
		case WSAEHOSTUNREACH:
			strcpy(msg, "No route to host.");
			break;
		case WSAEPROCLIM:
			strcpy(msg, "Too many processes.");
			break;
		case WSASYSNOTREADY:
			strcpy(msg, "Network subsystem is unavailable.");
			break;
		case WSAVERNOTSUPPORTED:
			strcpy(msg, "Winsock.dll version out of range.");
			break;
		case WSANOTINITIALISED:
			strcpy(msg, "Successful WSAStartup not yet performed.");
			break;
		case WSAEDISCON:
			strcpy(msg, "Graceful shutdown in progress.");
			break;
		case WSATYPE_NOT_FOUND:
			strcpy(msg, "Class type not found.");
			break;
		case WSAHOST_NOT_FOUND:
			strcpy(msg, "Host not found.");
			break;
		case WSATRY_AGAIN:
			strcpy(msg, "Nonauthoritative host not found.");
			break;
		case WSANO_RECOVERY:
			strcpy(msg, "This is a nonrecoverable error.");
			break;
		case WSANO_DATA:
			strcpy(msg, "Valid name, no data record of requested type.");
			break;
		case WSA_INVALID_HANDLE:
			strcpy(msg, "Specified event object handle is invalid.");
			break;
		case WSA_INVALID_PARAMETER:
			strcpy(msg, "One or more parameters are invalid.");
			break;
		case WSA_IO_INCOMPLETE:
			strcpy(msg, "Overlapped I/O event object not in signaled state.");
			break;
		case WSA_IO_PENDING:
			strcpy(msg, "OS dependent Overlapped operations will complete later.");
			break;
		case WSA_NOT_ENOUGH_MEMORY:
			strcpy(msg, "Insufficient memory available.");
			break;
		case WSA_OPERATION_ABORTED:
			strcpy(msg, "Overlapped operation aborted.");
			break;
		case WSASYSCALLFAILURE:
			strcpy(msg, "System call failure.");
			break;
		default:
			strcpy(msg, "No error associated with returned error code.");
			break;
	}
	
	ISDODEBUG(dodebug(0, "CSBSGigabit_T()", "Wrap get_error_message-The message is [%s]",msg, (char*)NULL));
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: RemoveSpaces()
//
// Purpose: Returns the message associated with a winsock error code
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// source            char*             source to remove spaces
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//           
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void RemoveSpaces(char* source)
{
  char* i = source;
  char* j = source;
  while(*j != 0)
  {
    *i = *j++;
    if(*i != ' ')
	{
      i++;
	}
  }
  *i = 0;
}


////////////////////////////////////////////////////////////////////////////////
// Function : dodebug(int, char*, char* format, ...);                         //
// Purpose  : Print the message to a debug file.                              //
// Return   : None if it don't work o well                                    //
////////////////////////////////////////////////////////////////////////////////
void dodebug(int code, char *function_name, char *format, ...)
{

	static int FirstRun = 0;
	char TmpBuf[_MAX_PATH];

	if (DE_BUG == 1 && FirstRun == 0) 
	{
		sprintf(TmpBuf, "%s", DEBUGIT_GIGABIT);
		if ((debugfp = fopen(TmpBuf, "w+b")) == NULL) 
		{
			DE_BUG = 0;
			return;
		}
		FirstRun++;
	}

	va_list	arglist;

	if (DE_BUG) 
	{
		if (code != 0) 
		{
			fprintf(debugfp, "%s in %s\r\n", strerror(code), function_name);
		}
		else
		{
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