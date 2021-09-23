//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    SLS232A_T.cpp
//
// Date:	    13FEB06
//
// Purpose:	    ATXMLW Instrument Driver for SLS232A
//
// Instrument:	SLS232A  <Device Description> (<device Type>)
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
// 1.0.0.0  13FEB06  Baseline								  D. Bubenik, EADS North America Defense
// 1.4.0.1  09AUG07  Updated to allow the opening or closing
//                   of the handle.							  D. Bubenik, EADS North America Defense
// 1.4.0.2  14AUG07  Added call to close events when closing
//                   the handle.							  D. Bubenik, EADS North America Defense
// 1.4.0.3  15AUG07  Added resetEvent calls					  D. Bubenik, EADS North America Defense
// 1.4.0.4  15AUG07  Added PurgeComm calls					  D. Bubenik, EADS North America Defense
// 1.6.1.0  26Jan09  Modified code to support loopback        E. Larson, EADS North America Defense
//                    cabilities of the bus
///////////////////////////////////////////////////////////////////////////////
// Includes
#include <sys/types.h>
#include <sys/stat.h>
#include "visa.h"
#include "AtxmlWrapper.h"
//#include "Ssismv4Deviceinterface.h"
#include "AtxmlSLS_T.h"
#include <stdexcept>
#include <stdio.h>
#include <stdlib.h>
#include <winioctl.h>
#include <new>
#include <string>

// Local Defines
#define IFHNDL(x) { \
                    if(resource.hport) { x ;} \
                  }

// Function codes
#define RS232	1
#define RS422	2 
#define RS485	3

#define ODD		1
#define EVEN	2 
#define NONE	3

#define TALKER_LISTENER 1
#define ALL_LISTENER    2

#define DATA 1
#define ALL  2

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024
#define MAX_STR_LEN         100
#define MAX_PORTS_OPEN      18
#define FIRST_PORTNO        1
#define MAX_DATA_BLOCK 1024;



#define TDRV002EXA_VERSION  "2.1.0"

 TCHAR           portName[MAX_PORTS_OPEN][10] =
 {
	TEXT("\\\\.\\COM1"),
	TEXT("\\\\.\\COM2"),
	TEXT("\\\\.\\COM3"),
	TEXT("\\\\.\\COM4"),
	TEXT("\\\\.\\COM5"),
	TEXT("\\\\.\\COM6"),
	TEXT("\\\\.\\COM7"),
	TEXT("\\\\.\\COM8"),
	TEXT("\\\\.\\COM9"),
	TEXT("\\\\.\\COM10"),
	TEXT("\\\\.\\COM11"),
	TEXT("\\\\.\\COM12"),
	TEXT("\\\\.\\COM13"),
	TEXT("\\\\.\\COM14"),
	TEXT("\\\\.\\COM15"),
	TEXT("\\\\.\\COM16"),
	TEXT("\\\\.\\COM17"),
	TEXT("\\\\.\\COM18")
 };

 char selection[MAX_STR_LEN];
 char inText[MAX_STR_LEN];
 int selnum;
 int numbytes;

 int intfSelect;
 SYSTEMTIME Starttime, Stoptime;

 int				DE_BUG = 0;
FILE			*debugfp = 0;
// Local Function Prototypes
void array_to_bin_string(char array [], int size, int length, char output []);
void bin_string_to_array(char input [], int length, char array [], int * size);


//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CSLS232A_T
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
#pragma warning(disable:4100)
CSLS232A_T::CSLS232A_T(int Instno, int ResourceType, char* ResourceName,int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
	BOOL success = true;
	char resourcelist[1024] = "";
	// char* resp;
	int chanNo = Instno - INSTOFFSET;
    char LclMsg[1024];
    int Status = 0;
	char temp[50]="";
	// struct _stat	StatBuf;  // Not Used

	/*DEBUG_IT = _stat("c:/aps/data/debugit_serial", &StatBuf) == -1 ? 0 : 1;

	if (DEBUG_IT) 
	{
		if ((DebugitFp = fopen("c:/aps/data/debugitserial.txt", "wb")) == NULL) 
		{
			DEBUG_IT = 0;
		}
	}*/
	ISDODEBUG(dodebug(0, "CSLS232A_T()", "Entering CSLS232A", (char*)NULL));
    // Save Device Info
    m_InstNo = Instno;
    m_ResourceType = ResourceType;
    memset(m_ResourceName, '\0', sizeof(m_ResourceName));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusSLS232A called "), Response, Buffersize);

    if(ResourceName)
    {
		strnzcpy(m_ResourceName,ResourceName,ATXMLW_MAX_NAME);
		 //if (strcmp(ResourceName, "COM_1") == 0)			//CJW was RS232_1
		 //	chanNo = 0; // assigned port 1 com 1 for SLS232A_1.
		sprintf(m_InitString,"%s%d", AddressInfoPtr->ControllerType,AddressInfoPtr->ControllerNumber);
		chanNo = AddressInfoPtr->ControllerNumber - 1;
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


	memset( &resource, 0, sizeof(resource));
    resource.hport= NULL;
    m_InitString[0] = '\0';
	memset(&m_RxTransfer, '\0', sizeof m_RxTransfer) ;
	memset(&m_TxTransfer, '\0', sizeof m_TxTransfer) ;
	strcpy(m_TxTransName, ResourceName);
	strcat(m_TxTransName, "_TX");
	strcpy(m_RxTransName, ResourceName);
	strcat(m_RxTransName, "_RX");

	InitPrivateSLS232A();
	NullCalDataSLS232A();

    // The Form Init String
    sprintf(LclMsg,"Wrap-CSLS232A Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);
	
	ISDODEBUG(dodebug(0, "CSLS232A_T()", "CSLS232A Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg, (char*)NULL));
	sprintf(resourcelist, " Port %d \n", chanNo);
	resource.hport = CreateFile((LPCSTR)&portName[chanNo][0], GENERIC_READ | GENERIC_WRITE | FILE_SHARE_READ | FILE_SHARE_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE,
								NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (resource.hport == INVALID_HANDLE_VALUE)
	{
		resource.alive = false;
		strcat(resourcelist, "failed to acquire a Port \n");
	}

	else
	{
		resource.alive = true;
		sprintf_s(resource.comnme, "SLS232A_1");
		strncpy(resource.resourcenme, ResourceName, strlen(ResourceName));
		strcat(resource.resourcenme, "\0");
		success = GetCommTimeouts(resource.hport, &resource.cts);

		if (!success)
		{
			strcat(resourcelist, "GetCommTimeouts() failed\n");
			resource.alive = false;
		}

		resource.cts.ReadIntervalTimeout = MAXDWORD;
		resource.cts.ReadTotalTimeoutMultiplier = MAXDWORD;
		resource.cts.ReadTotalTimeoutMultiplier = 0;
		resource.cts.WriteTotalTimeoutConstant = 500;
		resource.sizeblock = MAX_DATA_BLOCK;

		success = SetCommTimeouts(resource.hport, &resource.cts);

		if (!success)
		{
			strcat(resourcelist, " SetCommTimeouts() failed\n");
			resource.alive = false;
		}
	}

	Buffersize = strlen(resourcelist);

	if (Response != NULL)
	{
		strncpy(Response, resourcelist, strlen(resourcelist));
	}
	
	ISDODEBUG(dodebug(0, "CSLS232A_T()", "Leaving CSLS232A_T()", (char*)NULL));
	//		 delete resp;
    return;
}
#pragma warning(default:4100)

///////////////////////////////////////////////////////////////////////////////
// Function: ~CSLS232A_T()
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
CSLS232A_T::~CSLS232A_T()
{
	char Dummy[1024];
	
	ISDODEBUG(dodebug(0, "~CSLS232A_T()", "Entering ~CSLS232A_T()", (char*)NULL));
	// Reset the SLS232A
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CSLS232A Class Distructor called "),Dummy,1024);

	if (resource.hport != INVALID_HANDLE_VALUE)
	{
		CloseHandle(resource.hport);
		sprintf_s(resource.comnme, "");
		sprintf_s(resource.resourcenme, "Not in Use");
		resource.alive = false;
		resource.hport = 0;
	}
	
	ISDODEBUG(dodebug(0, "~CSLS232A_T()", "Leaving ~CSLS232A_T()", (char*)NULL));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusSLS232A
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
int CSLS232A_T::StatusSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	BOOL success = true;
	int status = 0;
	char* resp;
	char* retstats;
  
	ISDODEBUG(dodebug(0, "StatusSLS232A()", "Entering StatusSLS232A()", (char*)NULL));
	retstats = new char[256];
	resp = new char [128];
	sprintf(retstats, "<AtXmlResponse>\n <ReturnData>\n<AtxmlResource>%s</AtxmlResource>\n", resource.resourcenme);
	success = GetCommState(resource.hport, &resource.comStatus);
	success = GetCommTimeouts(resource.hport, &resource.cts);

	if (!success)
	{
		sprintf(resp, "<Value>\n<c:Datum xsi:type=\"c:string\" unit=\"Null\" value=\"Failed\"/>\n</Value>\n");
	}

	else
	{
		success = true;
		sprintf(resp, "<Value>\n<c:Datum xsi:type=\"c:string\" unit=\"Null\" value=\"status clean see return info\"/>\n</Value>\n");
	}

	strncat(retstats, resp, strlen(resp));
	strcat(retstats, " </ReturnData>\n</AtXmlResponse>\n \0");
	if (!success) 
	{
		status = -1;
	}

	BufferSize = strlen(retstats);
	strncpy(Response, retstats, BufferSize);
	delete retstats;
	delete resp;
	
	ISDODEBUG(dodebug(0, "StatusSLS232A()", "Leaving StatusSLS232A()", (char*)NULL));
	return (status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalSLS232A
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
int CSLS232A_T::IssueSignalSLS232A(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

    // IEEE 1641 Issue Signal action for the SLS232A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalSLS232A Signal: "), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "IssueSignalSLS232A()", "Entering IssueSignalSLS232A()", (char*)NULL));

    if((Status = GetStmtInfoSLS232A(SignalDescription, Response, BufferSize)) != 0)
	{
        return(Status);
	}

    switch(m_Action)
    {
		case ATXMLW_SA_APPLY:
			if((Status = SetupSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			if((Status = EnableSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
		case ATXMLW_SA_REMOVE:
			if((Status = DisableSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			if((Status = ResetSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
		case ATXMLW_SA_MEASURE:
			if((Status = SetupSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
		case ATXMLW_SA_READ:
			if((Status = EnableSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			if((Status = FetchSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
		case ATXMLW_SA_RESET:
			if((Status = ResetSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_SETUP:
			if((Status = SetupSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}
			break;
		case ATXMLW_SA_CONNECT:
			break;
		case ATXMLW_SA_ENABLE:
			if((Status = EnableSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
		case ATXMLW_SA_DISABLE:
			if((Status = DisableSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
		case ATXMLW_SA_FETCH:
			if((Status = FetchSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
		case ATXMLW_SA_DISCONNECT:
			break;
		case ATXMLW_SA_STATUS:
			if((Status = StatusSLS232A(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
    }
	
	ISDODEBUG(dodebug(0, "IssueSignalSLS232A()", "Leaving IssueSignalSLS232A()", (char*)NULL));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalSLS232A
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
int CSLS232A_T::RegCalSLS232A(ATXMLW_INTF_CALDATA* CalData)
{
    char      Dummy[1024];
	
	ISDODEBUG(dodebug(0, "RegCalSLS232A()", "Entering RegCalSLS232A()", (char*)NULL));
    // Setup action for the SLS232A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalSLS232A CalData: %s", CalData),Dummy,1024);
	
	ISDODEBUG(dodebug(0, "RegCalSLS232A()", "Leaving RegCalSLS232A()", (char*)NULL));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ResetSLS232A
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
int CSLS232A_T::ResetSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int   Status = 0;
	char resp[64];
	char  localResponse[2048];
	int   localBufferSize = sizeof(localResponse);

	localResponse[0] = 0;
	
	ISDODEBUG(dodebug(0, "ResetSLS232A()", "Entering ResetSLS232A()", (char*)NULL));
	// Reset action for the SLS232A
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetSLS232A called "), localResponse, localBufferSize);
	if(m_RxTransfer.hEvent!=NULL)
	{
		IFNSIM(Status = ResetEvent(m_RxTransfer.hEvent));
		IFNSIM(CloseHandle(m_RxTransfer.hEvent));
		memset(&m_RxTransfer, '\0', sizeof m_RxTransfer) ;
	}

	if(m_TxTransfer.hEvent!=NULL)
	{
		IFNSIM(Status = ResetEvent(m_TxTransfer.hEvent));
		IFNSIM(CloseHandle(m_TxTransfer.hEvent));
		memset(&m_TxTransfer, '\0', sizeof m_TxTransfer) ;
	}

	IFNSIM(IFHNDL(PurgeComm(resource.hport, PURGE_RXABORT|PURGE_RXCLEAR|PURGE_TXCLEAR|PURGE_TXABORT)));

	// Reset the SLS232A
	if (resource.hport == INVALID_HANDLE_VALUE)
	{
		sprintf_s(resp, "Failed to reset the port \n");
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("Could not open TEWS device"), localResponse, localBufferSize);
	}

	else
	{
		sprintf_s(resp, "%s \n", resource.resourcenme);
	}

	if (Response != NULL)
	{
		strncpy(Response, resp, BufferSize);
		BufferSize = strlen(Response);
	}

	InitPrivateSLS232A();
	ISDODEBUG(dodebug(0, "ResetSLS232A()", "Leaving ResetSLS232A()", (char*)NULL));
	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstSLS232A
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
int CSLS232A_T::IstSLS232A(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int   Status = 0;
	
	ISDODEBUG(dodebug(0, "IstSLS232A()", "Entering IstSLS232A()", (char*)NULL));
	// Reset action for the SLS232A
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstSLS232A called Level %d", Level), Response, BufferSize);
	// Reset the SLS232A
	switch(Level)
	{
		case ATXMLW_IST_LVL_PST:
			Status = StatusSLS232A(Response,BufferSize);
			break;
		case ATXMLW_IST_LVL_IST:
			Status = StatusSLS232A(Response, BufferSize);
			break;
		case ATXMLW_IST_LVL_CNF:
			Status = StatusSLS232A(Response,BufferSize);
			break;
		default: // Hopefully BIT 1-9
			break;
	}

	if(!Status)
	{
		ErrorSLS232A(Status, Response, BufferSize);
	}

	InitPrivateSLS232A();
	
	ISDODEBUG(dodebug(0, "IstSLS232A()", "Leaving IstSLS232A()", (char*)NULL));
	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsSLS232A
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
#pragma warning(disable:4100)
int CSLS232A_T::IssueNativeCmdsSLS232A(ATXMLW_INTF_INSTCMD* InstrumentCmds,ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsSLS232A()", "Entering IssueNativeCmdsSLS232A()", (char*)NULL));
    // Setup action for the SLS232A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsSLS232A "), Response, BufferSize);

    // Retrieve the CalData
    if((Status = atxmlw_InstrumentCommands((int)resource.hport, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
        return(Status);
    }
	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsSLS232A()", "Leaving IssueNativeCmdsSLS232A()", (char*)NULL));
    return(0);
}
#pragma warning(default:4100)

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallSLS232A
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
int CSLS232A_T::IssueDriverFunctionCallSLS232A(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	ATXMLW_DF_VAL RetVal;
    ATXMLW_XML_HANDLE DfHandle=NULL;
    char Name[ATXMLW_MAX_NAME];

	
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallSLS232A()", "Entering IssueDriverFunctionCallSLS232A()", (char*)NULL));
/*
	<AtXmlDriverFunctionCall>
		<SignalResourceName>DEVX_1</SignalResourceName>
		<SignalChannelName>Chan1</SignalChannelName>
		<DriverFunctionCall>
			<FunctionName>TestParam</FunctionName>
			<ReturnType>RetInt32</ReturnType>
			<Parameter   ParamNumber="1" ParamType="Handle" Value=""/>
			<Parameter   ParamNumber="2" ParamType="SrcUInt16" Value="0x3B05"/>
			<Parameter   ParamNumber="3" ParamType="SrcDblPtr" Value="35.e+3 36.0 37.0e5"/>
			<Parameter   ParamNumber="4" ParamType="SrcStrPtr" Value="xxx yyy zzz"/>
			<Parameter   ParamNumber="5" ParamType="RetInt8"/>
			<Parameter   ParamNumber="6" ParamType="RetStrPtr" Size="52"/>
		</DriverFunctionCall>
	</AtXmlDriverFunctionCall>
*/
    if((atxmlw_ParseDriverFunction((int)resource.hport, DriverFunction, &DfHandle, Response, BufferSize)) || (DfHandle == NULL))
	{
        return(0);
	}

    atxmlw_GetDFName(DfHandle,Name);
    RetVal.Int32 = 0;
    ///////// Implement Supported Driver Function Calls ///////////////////////
	if(ISDFNAME("CloseHandle"))
	{
		if(m_RxTransfer.hEvent!=NULL)
		{
			IFNSIM(ResetEvent(m_RxTransfer.hEvent));
			IFNSIM(CloseHandle(m_RxTransfer.hEvent));
			memset(&m_RxTransfer, '\0', sizeof m_RxTransfer) ;
		}

		if(m_TxTransfer.hEvent!=NULL)
		{
			IFNSIM(ResetEvent(m_TxTransfer.hEvent));
			IFNSIM(CloseHandle(m_TxTransfer.hEvent));
			memset(&m_TxTransfer, '\0', sizeof m_TxTransfer) ;
		}

		IFNSIM(IFHNDL(PurgeComm(resource.hport, PURGE_RXABORT|PURGE_RXCLEAR|PURGE_TXCLEAR|PURGE_TXABORT)));
    	IFNSIM(IFHNDL(RetVal.Int32 = !CloseHandle(resource.hport)));                     // handle to device
		resource.hport=NULL;
	}

	else if(ISDFNAME("OpenHandle"))
	{
	   // Initialize the SLS232A
		//parameters are as followsopen SeaMAC unit 0,open for sending and receiving,do not share,no security,device exists,normal access with overlapped I/O,no attr. template 
		IFNSIM(resource.hport = CreateFile((LPCSTR)m_InitString, GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL | FILE_FLAG_OVERLAPPED, NULL));                         
		if (resource.hport == INVALID_HANDLE_VALUE) 
		{ 
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", -1, "Could not open SeaMAC Device");
			RetVal.Int32=-1;
		}

		else
		{
			RetVal.Int32=0;
		}
	}

    else if((strncmp("vi",Name,2)==0) && atxmlw_doViDrvrFunc(DfHandle,Name,&RetVal))
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
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallSLS232A()", "Leaving IssueDriverFunctionCallSLS232A()", (char*)NULL));
    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupSLS232A
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
int CSLS232A_T::SetupSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status = 0;
	char  Msg[MAX_MSG_SIZE]="";
	char Msg1[128]="";
	BOOLEAN success = 1;
	
	ISDODEBUG(dodebug(0, "SetupSLS232A()", "Entering SetupSLS232A()", (char*)NULL));
	// Setup paramters for the  RS port. 
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-SetupSLS232A called %s ", m_ResourceName), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "SetupSLS232A()", "Setup() initstring is[%s] \n", (char*)NULL));
 
	sprintf(Msg, " Port being setup  %s  \n", m_ResourceName);
	
	ISDODEBUG(dodebug(0, "SetupSLS232A()", "Port being set up is [%s]  \n", m_ResourceName, (char*)NULL));
	

	if (!m_Delay.Exists)
	{
		m_Delay.Real = 0.001;
	}

	if (success) 
	{
		success = GetCommState(resource.hport, &resource.comStatus);
	}

	if (success) 
	{
		success = GetCommTimeouts(resource.hport, &resource.cts);
	}

	if (!success)
	{
		strcat(Msg, " GetComm Status data and timeout data failed.\n");
		ISDODEBUG(dodebug(0, "SetupSLS232A()", "GetComm Status data and timeout data failed.\n", m_InitString, (char*)NULL));
	}

	if (m_BitRate.Exists) 
	{
		resource.comStatus.BaudRate = (DWORD)m_BitRate.Real;
	}

	if (m_StopBits.Exists) 
	{
		resource.comStatus.StopBits = (BYTE)m_StopBits.Int;
	}

	if (m_Parity.Exists)
	{
		resource.comStatus.Parity = m_Parity.Dim;
	}

	if (m_WordLength.Exists)
	{
		resource.comStatus.ByteSize = m_WordLength.Int;
	}

	success = SetCommState(resource.hport, &resource.comStatus);

	if (!success)
	{
		strcat(Msg, " SetComm Setup data timing failed\n");
		ISDODEBUG(dodebug(0, "SetupSLS232A()", "SetComm Setup data timing failed\n",(char*)NULL));
	}

	if (resource.cts.ReadIntervalTimeout == NULL) 
	{
		resource.cts.ReadIntervalTimeout = MAXDWORD;
	}

	if (resource.cts.ReadTotalTimeoutMultiplier == NULL)
	{
		resource.cts.ReadTotalTimeoutMultiplier = MAXDWORD;
	}

	if (m_MaxTime.Exists)
	{
		resource.cts.ReadTotalTimeoutConstant = (DWORD)(m_MaxTime.Real*1000.0);
	}

	else 
	{
		resource.cts.ReadTotalTimeoutConstant = (((12 * resource.sizeblock * 1000) / resource.comStatus.BaudRate) + 100);;
	}

	if (resource.cts.WriteTotalTimeoutMultiplier == NULL)
	{
		resource.cts.WriteTotalTimeoutMultiplier = 1;
	}

	if (resource.cts.WriteTotalTimeoutConstant == NULL)
	{
		resource.cts.WriteTotalTimeoutConstant = (((12 * resource.sizeblock * 1000) / resource.comStatus.BaudRate) + 100);
	}

	success = SetCommTimeouts(resource.hport, &resource.cts);

	if (!success)
	{
		strcat(Msg, " SetComm Setup Timeouts failed\n");
		ISDODEBUG(dodebug(0, "SetupSLS232A()", "SetComm Setup Timeouts failed\n",(char*)NULL));
	}

	else
	{
		sprintf(Msg1, " updated with baud = %d stopbits = %d Parity = %d \n", resource.comStatus.BaudRate, resource.comStatus.StopBits, resource.comStatus.Parity);
		
		ISDODEBUG(dodebug(0, "SetupSLS232A()", "updated with baud = %d stopbits = %d Parity = %d \n", resource.comStatus.BaudRate, resource.comStatus.StopBits, resource.comStatus.Parity,(char*)NULL));
	}

	strcat(Msg, Msg1);

	strncpy(Response, Msg, strlen(Msg));
	BufferSize = strlen(Response);
	ISDODEBUG(dodebug(0, "SetupSLS232A()", "Leaving SetupSLS232A()", (char*)NULL));
	return Status;
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchSLS232A
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
int CSLS232A_T::FetchSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	BOOLEAN success = false;
	char* resp;
	char* intext;
	char* results;
	//char* resp1;
	DWORD retrievebytes = (DWORD)resource.numofbytes+1;
	
	ISDODEBUG(dodebug(0, "FetchSLS232A()", "Entering FetchSLS232A()", (char*)NULL));
	if(( m_Attribute.Dim==ALL) && (m_Attribute.Exists))
	{
 		sprintf(Response,
			"<AtXmlResponse>\n <ReturnData>\n"
			"<ValuePair>\n <Attribute>config</Attribute>\n"
				"<Value>\n"
				"<c:Datum xsi:type=\"c:string\" unit=\"None\"><c:Value>%6.5E,%d,%d,%d,%6.5E</c:Value></c:Datum>\n"
				"</Value>\n"
			"</ValuePair>\n"
			"</ReturnData>\n </AtXmlResponse>\n \0", m_BitRate.Real, m_WordLength.Int, m_Parity.Dim, m_StopBits.Int, m_MaxTime.Real );
		BufferSize = strlen(Response);
		return(0);
	}

	if( m_ReadLength.Exists == true )
	{
		resource.sizeblock = resource.numofbytes = m_ReadLength.Int;
		m_ReadLength.Exists = false;
	}

	retrievebytes = (DWORD)resource.numofbytes;

	// Fetch action for the SLS232A
	resp = new char[resource.sizeblock + 256];
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-FetchSLS232A called "), Response, BufferSize);
	sprintf(resp, "retrieve data from port %d,  Name %s ",resource.hport, m_ResourceName);

	if (resource.sizeblock == 0)
	{
		resource.sizeblock = 1000;
	}

	intext = new char[resource.sizeblock];
	sprintf(intext, "\0");
	success = GetCommTimeouts(resource.hport, &resource.cts);

	if (resource.comStatus.BaudRate!=0)
	{
		resource.cts.ReadTotalTimeoutConstant = (((12 * resource.sizeblock * 1000) / resource.comStatus.BaudRate) + 100);
		resource.cts.ReadTotalTimeoutConstant += 500;
		resource.cts.ReadTotalTimeoutMultiplier = 10;	
		resource.cts.ReadIntervalTimeout = 0;
		if (m_MaxTime.Exists)
		{
			resource.cts.ReadTotalTimeoutConstant += DWORD(m_MaxTime.Real * 1000);
		}
		resource.cts.WriteTotalTimeoutMultiplier = 1;
		resource.cts.WriteTotalTimeoutConstant = (((12 * resource.sizeblock * 1000) / resource.comStatus.BaudRate) + 100);
	}

	if (success) 
	{
		success = SetCommTimeouts(resource.hport, &resource.cts);
	}

	if (!success)
	{
		sprintf(resp, " Receive SetComm Timeout Data failed\n");
	}
	else
	{
		if ((!m_DataSize.Exists)&&(resource.numofbytes==0)) 
		{
			retrievebytes=1;
		}

		Sleep(10);
		ISDODEBUG(dodebug(0, "FetchSLS232A()", "Calling ReadFile(resource.hport, intext[%s], retrievebytes, (LPDWORD)&resource.numofbytes[%d], NULL)",intext,(LPDWORD)&resource.numofbytes, (char*)NULL));
		success = ReadFile(resource.hport, intext, retrievebytes, (LPDWORD)&resource.numofbytes, NULL);
		ISDODEBUG(dodebug(0, "FetchSLS232A()", "Returning ReadFile(resource.hport, intext[%s], retrievebytes, (LPDWORD)&resource.numofbytes[%d], NULL)",intext,(LPDWORD)&resource.numofbytes, (char*)NULL));

		if (!success)
		{
			sprintf(resp, "Failed to receive the data");
			strcpy(Response, resp);
			return -1;
		}

		else if (resource.numofbytes >0)
		{
			ISDODEBUG(dodebug(0, "FetchSLS232A()", "resource bytes > 0", (char*)NULL));
			results = new char[4 + (resource.numofbytes * (m_WordLength.Int + 2))+32];
			if(m_DataSize.Dim == 0x42)
			{
				array_to_bin_string(intext, resource.numofbytes, m_WordLength.Int, results);
				//sprintf(Response,
				// "<AtXmlResponse>\n <ReturnData>\n"
				//   "<ValuePair>\n <Attribute>data</Attribute>\n"
				//     "<Value>\n"
				//       "<c:Datum xsi:type=\"c:string\" unit=\"None\"><c:Value>%s</c:Value></c:Datum>\n"
				//     "</Value>\n"
				//   "</ValuePair>\n"
				// "</ReturnData>\n </AtXmlResponse>\n", results);
			}

			else
			{
				strncpy(results, intext, resource.numofbytes);
				strcat(results, "\0");
			}

			sprintf(Response,
				"<AtXmlResponse>\n <ReturnData>\n" 
				"<ValuePair>\n <Attribute>data</Attribute>\n"
					"<Value>\n"
					"<c:Datum xsi:type=\"c:string\" unit=\"None\"><c:Value>%s</c:Value></c:Datum>\n"
					"</Value>\n"
				"</ValuePair>\n"
				"</ReturnData>\n </AtXmlResponse>\n", results);
	 
			delete results;
			BufferSize = strlen(Response);
		}
	}

	delete intext;
	delete resp;
	
	ISDODEBUG(dodebug(0, "FetchSLS232A()", "Leaving FetchSLS232A()", (char*)NULL));
	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: DisableSLS232A
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
int CSLS232A_T::DisableSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	ISDODEBUG(dodebug(0, "DisableSLS232A()", "Entering DisableSLS232A()", (char*)NULL));
    // Open action for the SLS232A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenSLS232A called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "DisableSLS232A()", "Leaving DisableSLS232A()", (char*)NULL));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableSLS232A
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
int CSLS232A_T::EnableSLS232A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status = 0;
	//char TxBuffer[2][MAX_DATA];
	BOOL success = false;
	char resp[1024] = "";
	
	ISDODEBUG(dodebug(0, "EnableSLS232A()", "Entering EnableSLS232A()", (char*)NULL));

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-EnableSLS232A called "), Response, BufferSize);
	sprintf(resp, "send data thru port Name %s ", m_ResourceName);

	if (m_DataSize.Exists)
	{
		resource.sizeblock = m_DataSize.Int;	//CJW was strlen(m_Data);
		ISDODEBUG(dodebug(0, "EnableSLS232A()", "EnableSLS5102() Here in write file", (char*)NULL));
	}

	success = GetCommTimeouts(resource.hport, &resource.cts);
	success = GetCommState(resource.hport, &resource.comStatus);
	resource.cts.ReadTotalTimeoutConstant = (((12 * resource.sizeblock * 1000) / resource.comStatus.BaudRate) + 100);
	resource.cts.ReadIntervalTimeout = 10;
	resource.cts.ReadTotalTimeoutMultiplier = 1;
	resource.cts.WriteTotalTimeoutMultiplier = 1;
	resource.cts.WriteTotalTimeoutConstant = (((12 * resource.sizeblock * 1000) / resource.comStatus.BaudRate) + 100);
	resource.comStatus.ByteSize = m_WordLength.Int;
	resource.comStatus.StopBits = (BYTE)m_StopBits.Int;
	success = SetCommState(resource.hport, &resource.comStatus);

	if (success) 
	{
		success = SetCommTimeouts(resource.hport, &resource.cts);
	}

	if (!success)
	{
		strcat(resp, " Send SetComm Timeout Data failed\n");
	}

	if( m_DataSize.Int > 0 )
	{
		PurgeComm(resource.hport, PURGE_RXCLEAR );	//CJW ?????????????????????????????????

		if (success) 
		{
			success = WriteFile(resource.hport, m_Data,  m_DataSize.Int, (LPDWORD)&resource.numofbytes, NULL);
		}

		Sleep((DWORD)(((m_DataSize.Int*(m_WordLength.Int+4))/m_BitRate.Real)*1000));//delay long enough for data to be sent
	}

	if (!success) 
	{
		strcat(resp, "Failed to send all the data");
	}
	else 
	{
		strcat(resp, "Sent the data");
	}

	strnzcpy(Response, resp, strlen(resp)+1);
	
	ISDODEBUG(dodebug(0, "EnableSLS232A()", "Leaving EnableSLS232A()", (char*)NULL));
	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorSLS232A
//
// Purpose: Query SLS232A for the error text and send to WRTS
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
int  CSLS232A_T::ErrorSLS232A(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    char     Msg[MAX_MSG_SIZE];
    DWORD     QError;
	
	ISDODEBUG(dodebug(0, "ErrorSLS232A()", "Entering ErrorSLS232A()", (char*)NULL));
    retval = Status;
    Msg[0] = '\0';

    QError = 1;
    // Retrieve any pending errors in the device
    if(QError)
    {
        QError = 0;
		QError=GetLastError();
		
        if(QError && QError!=0x03e5) //overlapped i/o warning
        {
			LPTSTR lpMsgBuf;
			FormatMessage( FORMAT_MESSAGE_ALLOCATE_BUFFER |  FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, NULL, QError, 0, /*Default language*/
						(LPTSTR) &lpMsgBuf, 0, NULL);
			
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status,(LPSTR)lpMsgBuf);
			LocalFree( lpMsgBuf );
        }
    }
	
	ISDODEBUG(dodebug(0, "ErrorSLS232A()", "Leaving ErrorSLS232A()", (char*)NULL));
    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoSLS232A
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
int CSLS232A_T::GetStmtInfoSLS232A(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int    Status = 0;
    char   LclResource[ATXMLW_MAX_NAME];
    double LclDblValue;
    char   LclUnit[ATXMLW_MAX_NAME];
    char  *LclCharValuePtr;
	
	ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "Entering GetStmtInfoSLS232A()", (char*)NULL));
    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)))
	{
         return(Status);
	}

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);
	ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found Action %d", m_Action, (char*)NULL));

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource), Response, BufferSize);
	ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found Resource [%s]", LclResource, (char*)NULL));

    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement, (char*)NULL));
	}

	strcpy(resource.comnme, m_SignalElement);

	if(strcmp(m_SignalElement, "RS_232") == 0) 
	{
		m_BusSpec.Exists = true;
		m_BusSpec.Dim = RS232;
	}

	if(strcmp(m_SignalElement, "RS_422") == 0) 
	{
		m_BusSpec.Exists = true;
		m_BusSpec.Dim = RS422;
	}

	if(strcmp(m_SignalElement, "RS_485") == 0) 
	{
		m_BusSpec.Exists = true;
		m_BusSpec.Dim = RS485;
	}

	//Parity
	if((m_Parity.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "parity", &LclCharValuePtr)))
	{
		m_Parity.Dim = MARKPARITY;
		if(strcmp(LclCharValuePtr, "ODD")==0)
		{
			m_Parity.Dim=ODDPARITY;
		}

		else if(strcmp(LclCharValuePtr, "EVEN")==0)
		{
			m_Parity.Dim=EVENPARITY;
		}

		else if(strcmp(LclCharValuePtr, "NONE")==0)
		{
			m_Parity.Dim=NOPARITY;
		}

		else if(strcmp(LclCharValuePtr, "MARK")==0)
		{
			m_Parity.Dim=MARKPARITY;
		}

	    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Parity [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
	}

	//baud_rate
    if((m_BitRate.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "baud_rate", &LclDblValue, LclUnit)))
	{
		m_BitRate.Real=LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Bit-Rate %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found Bit-Rate %E [%s]", LclDblValue,LclUnit, (char*)NULL));
	}

	//wordLength
    if((m_WordLength.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "wordLength", &LclDblValue, LclUnit)))
	{
		m_WordLength.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Word-Length %d [%s]", m_WordLength.Int,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found Word-Length %d [%s]", m_WordLength.Int,LclUnit, (char*)NULL));
	}

	//stop_bits
    if((m_StopBits.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "stop_bits", &LclDblValue, LclUnit)))
	{
		m_StopBits.Real=LclDblValue;
		 m_StopBits.Int = ONESTOPBIT;
		if ((m_StopBits.Real== 1.5)&&(m_WordLength.Int <6)) m_StopBits.Int = ONE5STOPBITS;
		else if((m_StopBits.Real== 2)&&(m_WordLength.Int > 5)) m_StopBits.Int = TWOSTOPBITS;

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Stop-Bits %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found Stop-Bits %E [%s]", LclDblValue,LclUnit, (char*)NULL));
	}

	//Mode
	if((m_BusMode.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "Mode", &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "Talker-Listener")==0)
		{
			m_BusMode.Dim=TALKER_LISTENER;
		}

		else if(strcmp(LclCharValuePtr, "All-Listener")==0)
		{
			m_BusMode.Dim=ALL_LISTENER;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Bus-Mode [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found %s Bus-Mode [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//data
	if((m_DataSize.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "data_bits", &LclCharValuePtr)))
	{
		bin_string_to_array(LclCharValuePtr, m_WordLength.Int, &m_Data[0], &m_DataSize.Int);

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s data [%s]\nConversion:", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found %s Bus-Mode [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
		for(int i=0; i<m_DataSize.Int; i++)
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("data[%d] = [%x]", i, m_Data[i]), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "data[%d] = [%x]", i, m_Data[i], (char*)NULL));
		}

		resource.numofbytes = m_DataSize.Int;
		m_DataSize.Dim = 0x42;
	}

	else if((m_DataSize.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "data", &LclCharValuePtr)))
	{
		m_DataSize.Int = sizeof(LclCharValuePtr)/sizeof(LclCharValuePtr[0]);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s data [%s]\nConversion:", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found %s data [%s]\nConversion:", m_SignalName, LclCharValuePtr, (char*)NULL));
		for(int i=0; i<m_DataSize.Int; i++)
		{
			 m_Data[i] = LclCharValuePtr[i];
 			 ATXMLW_DEBUG(5,atxmlw_FmtMsg("data[%d] = [%x]", i, m_Data[i]), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found data[%d] = [%x]", i, m_Data[i], (char*)NULL));
		}
		resource.numofbytes = m_DataSize.Int;
		m_DataSize.Dim = 0x41;
	}

	//talker
	if(atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "talker", &LclCharValuePtr))
	{
		if(strcmp(LclCharValuePtr, "Test-Equip")==0)
		{
			m_TestEquipTalker.Exists=true;
		}

		else if(strcmp(LclCharValuePtr, "UUT")==0)
		{
			m_UutTalker.Exists=true;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s talker [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found %s talker [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//recieveAddress
	if(atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "listener", &LclCharValuePtr))
	{
		if(strcmp(LclCharValuePtr, "Test-Equip")==0)
		{
			m_TestEquipListener.Exists=true;
		}

		else if(strcmp(LclCharValuePtr, "UUT")==0)
		{
			m_UutListener.Exists=true;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s listener [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
	}

	//process
	if(atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "process", &LclCharValuePtr))
	{
		if(strcmp(LclCharValuePtr, "Proceed")==0)
		{
			m_Proceed.Exists=true;
		}

		else if(strcmp(LclCharValuePtr, "Wait")==0)
		{
			m_Wait.Exists=true;
		}
		
	    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s process [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found %s talker [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//terminated
	if((m_Terminated.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "terminate", &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "OFF")==0)
		{
			m_Terminated.Exists=false;
		}
		
	    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s terminated [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found %s terminated [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}

	//maxTime
    if((m_MaxTime.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "maxTime", &LclDblValue, LclUnit)))
	{
		m_MaxTime.Real=LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Max-Time %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found Max-Time %E [%s]", LclDblValue,LclUnit, (char*)NULL));
	}

	//delay
    if((m_Delay.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "delay", &LclDblValue, LclUnit)))
	{
		m_Delay.Real=LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Delay %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found Delay %E [%s]", LclDblValue,LclUnit, (char*)NULL));
	}

	//exnm
    if((m_Exnm.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "Exnm", &LclDblValue, LclUnit)))
	{
		m_Exnm.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Exnm %d [%s]",  m_Exnm.Int,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found Exnm %d [%s]",  m_Exnm.Int,LclUnit, (char*)NULL));
	}

	//readLength
    if((m_ReadLength.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "readLength", &LclDblValue, LclUnit)))
	{
		m_ReadLength.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found m_ReadLength %d [%s]", m_Exnm.Int,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found m_ReadLength %d [%s]", m_Exnm.Int,LclUnit, (char*)NULL));
	}

	//attribute
	if((m_Attribute.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "attribute", &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "data")==0)
		{
			m_Attribute.Dim=DATA;
		}

		else if(strcmp(LclCharValuePtr, "config")==0)
		{
			m_Attribute.Dim=ALL;
		}
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s attribute [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "WrapGSI-Found %s attribute [%s]", m_SignalName, LclCharValuePtr, (char*)NULL));
	}
	
	atxmlw_Close1641Xml(&m_SignalDescription);
	ISDODEBUG(dodebug(0, "GetStmtInfoSLS232A()", "Leaving GetStmtInfoSLS232A()", (char*)NULL));

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateSLS232A
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
void CSLS232A_T::InitPrivateSLS232A(void)
{
	ISDODEBUG(dodebug(0, "InitPrivateSLS232A()", "Entering InitPrivateSLS232A()", (char*)NULL));
	if(m_RxTransfer.hEvent!=NULL)
	{
		IFNSIM(ResetEvent(m_RxTransfer.hEvent));
		IFNSIM(CloseHandle(m_RxTransfer.hEvent));
		memset(&m_RxTransfer, '\0', sizeof m_RxTransfer) ;
	}

	if(m_TxTransfer.hEvent!=NULL)
	{
		IFNSIM(ResetEvent(m_TxTransfer.hEvent));
		IFNSIM(CloseHandle(m_TxTransfer.hEvent));
		memset(&m_TxTransfer, '\0', sizeof m_TxTransfer) ;
	}

	m_Handle = NULL;
	m_BusSpec.Exists = false;
	m_DataSize.Exists = false;
	m_DataSize.Int = 0;
    m_BusMode.Exists = false;
    m_Proceed.Exists = false;
    m_Wait.Exists = false;
	m_BitRate.Exists = false;
	m_StopBits.Exists = false;
	m_Delay.Exists = false;
	m_Parity.Exists = false;
	m_MaxTime.Exists = false;
	m_Exnm.Exists = false;
	m_UutTalker.Exists = false;
	m_UutListener.Exists = false;
	m_TestEquipTalker.Exists = false;
	m_TestEquipListener.Exists = false;
	m_Terminated.Exists = false;
	m_Attribute.Exists = false;
	m_WordLength.Exists = false;
	m_WordLength.Int = 8;
	m_ReadLength.Exists = false;
	m_ReadLength.Int = 0;
	m_Attribute.Dim = DATA;
	m_BitRate.Real = 38400.0;
	m_WordLength.Int = 8;
	m_Parity.Dim = MARKPARITY;
	m_StopBits.Int = 0;
	m_MaxTime.Real = 1.0;
	m_DataSize.Dim = 0x42;
	
	ISDODEBUG(dodebug(0, "InitPrivateSLS232A()", "Leaving InitPrivateSLS232A()", (char*)NULL));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataSLS232A
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
void CSLS232A_T::NullCalDataSLS232A(void)
{
	ISDODEBUG(dodebug(0, "NullCalDataSLS232A()", "Entering NullCalDataSLS232A()", (char*)NULL));
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
	ISDODEBUG(dodebug(0, "NullCalDataSLS232A()", "Leaving NullCalDataSLS232A()", (char*)NULL));
    return;
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
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
void bin_string_to_array(char input [], int length, char array [], int * size)
{
	ISDODEBUG(dodebug(0, "bin_string_to_array()", "Entering bin_string_to_array()", (char*)NULL));
	ISDODEBUG(dodebug(0, "bin_string_to_array()", "bin_string_to_array(char input [%s], int length[%d], char array [%s], int * size[%d])",input, length,array, size, (char*)NULL));
	char string[2048] = "";
	char *temp;
	int i = 0, j = 0;

	memset(string, '\0', sizeof(string));
	sprintf(string, "%s", input);
	
	ISDODEBUG(dodebug(0, "bin_string_to_array()",  "In bin_string_to_array()  input [%s] is assigned to string[%s]", input,string, (char*)NULL));
	temp = strtok(string, ",");

	while(temp != NULL)
	{
		array[i] = 0;
		for(j = 0; j < length; j++)
		{
			if(temp[j] == 'H')
				array[i] = array[i] + (1 << (length - 1 - j)); 
		}

		i++;
		temp = strtok(NULL, ",");
		if(temp)
		{
			temp++;
		}
	}

	*size = i;
	
	ISDODEBUG(dodebug(0, "bin_string_to_array()", "Leaving bin_string_to_array()", (char*)NULL));
	return;
}

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
void array_to_bin_string(char array [], int size, int length, char output [])
{
	ISDODEBUG(dodebug(0, "array_to_bin_string()", "Entering array_to_bin_string()", (char*)NULL));
	ISDODEBUG(dodebug(0, "array_to_bin_string()", "bin_string_to_array(char array [%s], int size[%d], int length[%d], char output [%s])",array, size, length, output , (char*)NULL));
	char string[2048]={0};
	char temp[20]={0};

	memset(string, '\0', sizeof(string));
	memset(temp, '\0', sizeof(temp));

	for(int i = 0; i < size; i++)
	{
		for(int j = length - 1; j >= 0; j--)
		{
			ISDODEBUG(dodebug(0, "array_to_bin_string", "The array at index [%d] is array[%d]", i, array[i], (char*)NULL));
			if((array[i] >> j)&0x0001)
			{
				strcat(temp, "H");
			}

			else
			{
				strcat(temp, "L");
			}
		}

		//itoa(array[i], temp, 10);
		if(i != (size - 1))
		{
			strcat(temp,", ");
		}

		strcat(string, temp);
		temp[0] = '\0';
	}

    strcpy(output, string);
	ISDODEBUG(dodebug(0, "array_to_bin_string()", "Leaving array_to_bin_string()", (char*)NULL));
	return;
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

		sprintf(TmpBuf, "%s", DEBUGIT_PCI);
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

