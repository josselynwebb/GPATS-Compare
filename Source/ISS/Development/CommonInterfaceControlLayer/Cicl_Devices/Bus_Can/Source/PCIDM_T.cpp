//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    IcPCIDM_T.cpp
//
// Date:	    26JUN06
//
// Purpose:	    ATXMLW Instrument Driver for IcPCIDM
//
// Instrument:	IcPCIDM  <Device Description> (<device Type>)
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
// 1.0.0.0  26JUN06  Baseline								  D. Bubenik, EADS North America Defense
// 1.4.1.0  04JUN07  Updated CBTS per DME changes             DME
// 1.5.0.0  04JUN07  Updated CBTS for MNG Release             J. French
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "AtxmlWrapper.h"
#include "tdrv010.h"
#include "PCIDM_T.h"
#include "tdrv010api.h"
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
extern void stuff_string(char * string); //defined in CDDI_T.cpp
extern void unstuff_string(char * string); //defined in CDDI_T.cpp

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CIcPCIDM_T
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

CIcPCIDM_T::CIcPCIDM_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    char LclMsg[1024];
    int Status = 0;

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


    m_myhDLL = LoadLibrary("BusCarrierCard.dll");
	if(m_myhDLL == NULL && !m_Sim)
	{
		atxmlw_ErrorResponse(m_ResourceName, Response, Buffersize, "Instrument Error ",
                           Status, "Could not load the DLL");
		return;
	}

    m_InitString[0] = '\0';

    InitPrivateIcPCIDM();
	NullCalDataIcPCIDM();

    // The Form Init String
    sprintf(m_InitString,"%s",
              m_AddressInfo.ControllerType);
    sprintf(LclMsg,"Wrap-CIcPCIDM Class called with Instno %d, Sim %d, Dbg %d", 
                                      m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);

    // Initialize the IcPCIDM
	

	//if (!m_Sim && InitPcidmFunctionPointers(m_myhDLL) != 0) 
	//{ 
	//	    atxmlw_ErrorResponse(m_ResourceName, Response, Buffersize, "Instrument Error ",
 //                          -1, "InitPcidmFunctionPointers failed.");
 //           return;
 //   } 
	
    if(Status && ErrorIcPCIDM(Status, Response, Buffersize))
        return;
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CIcPCIDM_T()
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
CIcPCIDM_T::~CIcPCIDM_T()
{
    char Dummy[1024];

    // Reset the IcPCIDM
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CIcPCIDM Class Distructor called "),Dummy,1024);

	FreeLibrary(m_myhDLL);
	
	if(m_TransType==TCP)
	{
	//	IFNSIM(closesocket(m_Handle));
	}
	//WSACleanup();

    return;
}

///////////////////////////////////////////////////////////////////////////////
// CODE_CHECK: ANTHONY ADDED THIS
int CIcPCIDM_T::Open(char *VXIAddress)
{

	//IFNSIM(m_Handle=PcidmOpen(VXIAddress));
	//if(m_Handle==BCC_ERROR)
	{ 
//		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
//                           BCC_ERROR, "CddiOpen() failed.");
		//return(BCC_ERROR);
    }
	return 0;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusIcPCIDM
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
int CIcPCIDM_T::StatusIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";

    // Status action for the IcPCIDM
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusIcPCIDM called "), Response, BufferSize);
    // Check for any pending error messages
    Status = ErrorIcPCIDM(0, Response, BufferSize);

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalIcPCIDM
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
int CIcPCIDM_T::IssueSignalIcPCIDM(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

    // IEEE 1641 Issue Signal action for the IcPCIDM
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalIcPCIDM Signal: "),
                              Response, BufferSize);

    if((Status = GetStmtInfoIcPCIDM(SignalDescription, Response, BufferSize)) != 0)
        return(Status);

    switch(m_Action)
    {
    case ATXMLW_SA_APPLY:
        if((Status = SetupIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        if((Status = EnableIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_REMOVE:
        if((Status = DisableIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        if((Status = ResetIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_MEASURE:
        if((Status = SetupIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_READ:
        if((Status = EnableIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        if((Status = FetchIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_RESET:
        if((Status = ResetIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_SETUP:
        if((Status = SetupIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_CONNECT:
        break;
    case ATXMLW_SA_ENABLE:
        if((Status = EnableIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_DISABLE:
        if((Status = DisableIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_FETCH:
        if((Status = FetchIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_DISCONNECT:
        break;
	case ATXMLW_SA_STATUS:
        if((Status = StatusIcPCIDM(Response, BufferSize)) != 0)
            return(Status);
        break;
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalIcPCIDM
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
int CIcPCIDM_T::RegCalIcPCIDM(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
    char      Dummy[1024];

    // Setup action for the IcPCIDM
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalIcPCIDM CalData: %s", 
                               CalData),Dummy,1024);

    // Retrieve the CalData
/* Begin TEMPLATE_SAMPLE_CODE 
    if((Status = atxmlw_ParseXml(CalData, &CalHandle, NULL, 0)))
    {
        atxmlw_CloseXmlHandle();
        return(Status);
    }
    if((CalString = atxmlw_GetAttributeFirst(CalHandle, &CalString,
                       "voltagecomp","value") != NULL))
    {
        m_CalData[0] = atxmlw_GetDoubleValue(CalString);
        ...
    }
    
    atxmlw_CloseXmlHandle();

/* End TEMPLATE_SAMPLE_CODE */

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetIcPCIDM
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
int CIcPCIDM_T::ResetIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";

    // Reset action for the IcPCIDM
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetIcPCIDM called "), Response, BufferSize);
	// Reset the IcPCIDM
	if(m_TransType==TCP)
	{
	//	IFNSIM(closesocket(m_Handle));
	}
    if(Status)
        ErrorIcPCIDM(Status, Response, BufferSize);

	//WSASetLastError(0);
    InitPrivateIcPCIDM();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstIcPCIDM
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
int CIcPCIDM_T::IstIcPCIDM(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;

    // Reset action for the IcPCIDM
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstIcPCIDM called Level %d", 
                              Level), Response, BufferSize);
    // Reset the IcPCIDM
    //////// Place IcPCIDM specific data here //////////////
    switch(Level)
    {
    case ATXMLW_IST_LVL_PST:
        Status = StatusIcPCIDM(Response,BufferSize);
        break;
    case ATXMLW_IST_LVL_IST:
/* Begin TEMPLATE_SAMPLE_CODE 
        IFNSIM((Status = IcPCIDM_IST(m_Handle)));
/* End TEMPLATE_SAMPLE_CODE */
        break;
    case ATXMLW_IST_LVL_CNF:
        Status = StatusIcPCIDM(Response,BufferSize);
        break;
    default: // Hopefully BIT 1-9
/* Begin TEMPLATE_SAMPLE_CODE 
        IFNSIM((Status = IcPCIDM_BIT(m_Handle, (Level-ATXMLW_IST_LVL_BIT))));
/* End TEMPLATE_SAMPLE_CODE */
        break;
    }
/* Begin TEMPLATE_SAMPLE_CODE 
    IFNSIM((Status = IcPCIDM_PnPreset(m_Handle)));
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    if(Status)
        ErrorIcPCIDM(Status, Response, BufferSize);

    InitPrivateIcPCIDM();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsIcPCIDM
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
int CIcPCIDM_T::IssueNativeCmdsIcPCIDM(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the IcPCIDM
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsIcPCIDM "), Response, BufferSize);

    // Retrieve the CalData
/* Begin TEMPLATE_SAMPLE_CODE */
    if((Status = atxmlw_InstrumentCommands(10, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
        return(Status);
    }

/* End TEMPLATE_SAMPLE_CODE */

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallIcPCIDM
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
int CIcPCIDM_T::IssueDriverFunctionCallIcPCIDM(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the IcPCIDM
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallIcPCIDM", Response, BufferSize);

    // Retrieve the CalData
/* Begin TEMPLATE_SAMPLE_CODE 
    if((Status = atxmlw_IssueDriverFunctionCallIcPCIDM(m_Handle, DriverFunction,
                                        Response, BufferSize)))
    {
        return(Status);
    }
/* End TEMPLATE_SAMPLE_CODE */

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupIcPCIDM
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
int CIcPCIDM_T::SetupIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	//DeviceInfoStruct infoStruct;
	//CBTSError error;

 //   // Setup action for the IcPCIDM
 //   ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupIcPCIDM called "), Response, BufferSize);

	//memset(&infoStruct,0,sizeof(infoStruct));

	//if(m_LocalIP.Exists)
	//	strcpy(infoStruct.myIPInfo.IPAddress,m_LocalIP.Address);

	//if(m_LocalMask.Exists)
	//	strcpy(infoStruct.myIPInfo.NetworkMask,m_LocalMask.Address);

	//if(m_LocalGateway.Exists)
	//	strcpy(infoStruct.myIPInfo.RouterAddress,m_LocalGateway.Address);

	//if(m_RemoteIP.Exists)
	//	strcpy(infoStruct.destinationIPInfo.IPAddress,m_RemoteIP.Address);

	//infoStruct.radioType=(PCIDMRadioTypeEnum)m_TransType;

	////IFNSIM(m_Handle=PcidmOpen("VXI::00::00"));	// CODE_CHECK: removed by AJP, an open method has been added.
	//if(!m_Sim && m_Handle==BCC_ERROR)
	//{ 
	//	    atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
 //                          BCC_ERROR, "PcidmOpen() failed.");
 //           return(BCC_ERROR);
 //   } 
	//
	//if (!m_Sim && PcidmIoctl(m_Handle,&infoStruct) == BCC_ERROR)
 //   {
 //       error = PcidmGetError(m_Handle);
 //       
	//	atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
 //                          error.code, atxmlw_FmtMsg("PcidmIoctl returned error code %d", error.code));
	//		
	//	//fprintf(stderr, "PcidmIoctl() failed with %d.\n", error); 
 //       return(error.code); 
 //   }
	///*if(!m_LocalIP.Exists)//enable DHCP
	//{
	//	IFNSIM(Status = set_ip(m_InitString, NULL, NULL, NULL, true));
	//}
	//else
	//{
	//	IFNSIM(Status = set_ip(m_InitString, m_LocalIP.Address, m_LocalMask.Address, m_LocalGateway.Address));
	//}*/
	//
 //   if(Status)
 //       ErrorIcPCIDM(Status, Response, BufferSize);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchIcPCIDM
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
int CIcPCIDM_T::FetchIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int     Status = 0;
	//double  MeasValue = 0.0;
 //   double  MaxTime = 5000;
 //   char   *ErrMsg = "";
	//int GetData=1;
	//char attribute[10];
	//CBTSError error;

	//if(m_Sim)
	//{
	//	strcpy(m_RetData, "THIS IS MY TEST MESSAGE");
	//}

 //   // Fetch action for the IcPCIDM
 //   ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchIcPCIDM called "), Response, BufferSize);

	//if(m_Attribute.Dim==DATA)
	//{
	//	if(!m_MaxTime.Exists)
	//		m_MaxTime.Real=20000; //in milliseconds

	//	IFNSIM(Status=PcidmRead(m_Handle, m_RetData, MAX_DATA, (int)m_MaxTime.Real));

	//	if(Status==BCC_ERROR)
	//	{
	//		error = PcidmGetError(m_Handle);
	//        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
 //                          0, atxmlw_FmtMsg("PcidmRead returned error code %d", error.code));
	//		return 1; 
	//	}

	//}	

	//// Fetch data
	//switch(m_Attribute.Dim)
	//{
	//case DATA:
	//	stuff_string(m_RetData);
	//	strcpy(attribute, "data");
	//	break;
	//}

 //  if(Status)
 //   {
 //    //   MeasValue = FLT_MAX;
 //   }
 //   else
 //   {
 //       sscanf(ErrMsg,"%E",&MeasValue);
 //   }
	//if(Response && (BufferSize > (int)(strlen(Response)+200)))
	//{
	//	if(m_Attribute.Dim==DATA)
	//	{
	//		atxmlw_ScalerStringReturn(attribute, "", m_RetData,
	//					Response, BufferSize);
	//	}
	//	else
	//	{
	//		if(m_TransType==PCIDM_MTS)
	//		{
	//			atxmlw_ScalerStringReturn("spec", "", "MTS",
	//					Response, BufferSize);
	//		}
	//		else if(m_TransType==PCIDM_AFAPD)
	//		{
	//			atxmlw_ScalerStringReturn("spec", "", "AFAPD",
	//					Response, BufferSize);
	//		}
	//		else if(m_TransType==PCIDM_IDL)
	//		{
	//			atxmlw_ScalerStringReturn("spec", "", "IDL",
	//					Response, BufferSize);
	//		}
	//		else
	//		{
	//			atxmlw_ScalerStringReturn("spec", "", "TACFIRE",
	//					Response, BufferSize);
	//		}

	//		if(m_LocalIP.Exists)
	//		{
	//			atxmlw_ScalerStringReturn("localIP", "", m_LocalIP.Address,
	//				Response, BufferSize);
	//		}
	//		if(m_LocalMask.Exists)
	//		{
	//			atxmlw_ScalerStringReturn("localSubnetMask", "", m_LocalMask.Address,
	//				Response, BufferSize);
	//		}
	//		if(m_LocalGateway.Exists)
	//		{
	//			atxmlw_ScalerStringReturn("localGateway", "", m_LocalGateway.Address,
	//				Response, BufferSize);
	//		}
	//		if(m_RemoteIP.Exists)
	//		{
	//			atxmlw_ScalerStringReturn("remoteIP", "", m_RemoteIP.Address,
	//				Response, BufferSize);
	//		}
	//		if(m_MaxTime.Exists)
	//		{
	//			atxmlw_ScalerDoubleReturn("maxTime", "", m_MaxTime.Real,
	//					Response, BufferSize);
	//		}
	//	}
	//}
	return(0);

}



///////////////////////////////////////////////////////////////////////////////
// Function: DisableIcPCIDM
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
int CIcPCIDM_T::DisableIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	//CBTSError error;
 //   // Open action for the IcPCIDM
 //   ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenIcPCIDM called "), Response, BufferSize);

 //   if(!m_Sim && PcidmClose(m_Handle) == BCC_ERROR)
	//{ 
	//		error=PcidmGetError(m_Handle);
 //           atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
	//						BCC_ERROR, atxmlw_FmtMsg("PcidmClose returned error code %d", error.code));  
 //           return(BCC_ERROR); 
 //   }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableIcPCIDM
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
int CIcPCIDM_T::EnableIcPCIDM(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status=0;
	//CBTSError error;
 //   // Close action for the IcPCIDM
 //   ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableIcPCIDM called "), Response, BufferSize);

	//if(m_DataSize.Exists)//send Data
	//{
	//	m_DataSize.Int=strlen(m_Data);
	//	IFNSIM(Status=PcidmWrite(m_Handle, m_Data, m_DataSize.Int));
	//	if(Status==BCC_ERROR)
	//	{
	//		error=PcidmGetError(m_Handle);
	//		
	//		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
	//						0, atxmlw_FmtMsg("PcidmWrite returned error code %d", error.code)); 
	//		return 1;
	//	}
	//		
	//}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorIcPCIDM
//
// Purpose: Query IcPCIDM for the error text and send to WRTS
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
int  CIcPCIDM_T::ErrorIcPCIDM(int Status,
                                  ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
//    int      retval;
    int      Err = 0;
    //char     Msg[MAX_MSG_SIZE];
    //BCCErrorTypeEnum QError;
	//BCCErrorTypeEnum error;
	std::string Msg;
//	char     ErrorCodeBuff[16];
	//CBTSError error;

 //   retval = Status;
 //   //Msg[0] = '\0';

 //   if(Status)
 //   {
	//	IFNSIM(error=PcidmGetError(m_Handle));
	//	if (error.code != BCC_OK)
	//	{
	//		sprintf(ErrorCodeBuff, " Error Code = %d .", error.code);
	//		Msg = ErrorCodeBuff;
	//		Msg += error.desc;
	//		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
 //                                Status, (char*)Msg.c_str());
	//	}
	//}
		/*
		switch(error)
		{
		case BCC_OK:
			return 0;
			break;
		case BCC_EBADF:
			strcpy(Msg, "Invalid Handle");
			break;
		case BCC_EFAULT:
			strcpy(Msg,"Inaccessible memory area was referenced");
			break;
		case BCC_ENOTTY:
			strcpy(Msg,"Specified request does not apply to the kind of object that the descriptor references");
			break;
		case BCC_EINVAL:
			strcpy(Msg,"Request was invalid");
			break;
		case BCC_LOSSOFCOM:
			strcpy(Msg,"Loss of communication somewhere in the data pipeline that stopped the flow of data");
			break;
		case BCC_UNSPECIFIEDERROR:
			strcpy(Msg,"Error occurred somewhere in the data pipeline, but the exact nature was not known");
			break;
		}


		if(error==BCC_TIMEOUT)
		{
			return 0;
		}
		else
		{
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
                           Status, Msg);
		}
    }

    QError = BCC_ERROR;
    // Retrieve any pending errors in the device
    while(QError!=BCC_OK)
    {
        QError = BCC_OK;

		IFNSIM(QError=PcidmGetError(m_Handle));
        if(QError)
        {
			switch(error)
			{
			case BCC_EBADF:
				strcpy(Msg, "Invalid Handle");
				break;
			case BCC_EFAULT:
				strcpy(Msg,"Inaccessible memory area was referenced");
				break;
			case BCC_ENOTTY:
				strcpy(Msg,"Specified request does not apply to the kind of object that the descriptor references");
				break;
			case BCC_EINVAL:
				strcpy(Msg,"Request was invalid");
				break;
			case BCC_LOSSOFCOM:
				strcpy(Msg,"Loss of communication somewhere in the data pipeline that stopped the flow of data");
				break;
			case BCC_UNSPECIFIEDERROR:
				strcpy(Msg,"Error occurred somewhere in the data pipeline, but the exact nature was not known");
				break;
			}


			if(error==BCC_TIMEOUT)
			{
				return 0;
			}
			else
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
							Status, Msg);
			}
        }
    }
*/
    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoIcPCIDM
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
int CIcPCIDM_T::GetStmtInfoIcPCIDM(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int    Status = 0;
    char   LclResource[ATXMLW_MAX_NAME];
    double LclDblValue;
    char   LclUnit[ATXMLW_MAX_NAME]="";
    char  *LclCharValuePtr;

    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription,
                             Response, BufferSize)))
         return(Status);

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription,
                      Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action),
                              Response, BufferSize);

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource,
                      Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource),
                              Response, BufferSize);

	
	if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]",
                              m_SignalName, m_SignalElement),
                              Response, BufferSize);
	
	//attribute
	if((m_Attribute.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "attribute",
                                         &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "data")==0)
			m_Attribute.Dim=DATA;
		else if(strcmp(LclCharValuePtr, "config")==0)
		{
			m_Attribute.Dim=ALL;
			return 0;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s attribute [%s]",
                              m_SignalName, LclCharValuePtr),
                              Response, BufferSize);
	}

	//if(strcmp(m_SignalElement, "AFAPD")==0)
	//{
	//	m_TransType=PCIDM_AFAPD;
	//}
	//else if(strcmp(m_SignalElement, "MTS")==0)
	//{
	//	m_TransType=PCIDM_MTS;
	//}
	//else if(strcmp(m_SignalElement, "IDL")==0)
	//{
	//	m_TransType=PCIDM_IDL;
	//}
	//else
	//{
	//	m_TransType=PCIDM_TACFIRE;
	//}

	//Local IP
	if((m_LocalIP.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "localIP",
                                         &LclCharValuePtr)))
	{
		strcpy(m_LocalIP.Address, LclCharValuePtr);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s localIP [%s]",
                              m_SignalName, LclCharValuePtr),
                              Response, BufferSize);
	}

	//Local Subnet
	if((m_LocalMask.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "localSubnetMask",
                                         &LclCharValuePtr)))
	{
		strcpy(m_LocalMask.Address, LclCharValuePtr);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s localSubnetMask [%s]",
                              m_SignalName, LclCharValuePtr),
                              Response, BufferSize);
	}

	//Local Gateway
	if((m_LocalGateway.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "localGateway",
                                         &LclCharValuePtr)))
	{
		strcpy(m_LocalGateway.Address, LclCharValuePtr);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s localGateway [%s]",
                              m_SignalName, LclCharValuePtr),
                              Response, BufferSize);
	}

	//Remote IP
	if((m_RemoteIP.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "remoteIP",
                                         &LclCharValuePtr)))
	{
		strcpy(m_RemoteIP.Address, LclCharValuePtr);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s remoteIP [%s]",
                              m_SignalName, LclCharValuePtr),
                              Response, BufferSize);
	}

	//Remote Port
    if((m_RemotePort.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "remotePort",
                                         &LclDblValue, LclUnit)))
	{
		m_RemotePort.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found remotePort %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}

	//data
	if((m_DataSize.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "data_bits",
                                         &LclCharValuePtr)))
	{
		strcpy(m_Data, LclCharValuePtr);
		unstuff_string(&m_Data[0]);
		//bin_string_to_array(LclCharValuePtr, &m_Data[0], &m_DataSize.Int);
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s data [%s]",
                              m_SignalName, m_Data),
                              Response, BufferSize);
	}

	//maxTime
    if((m_MaxTime.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "maxTime",
                                         &LclDblValue, LclUnit)))
	{
		m_MaxTime.Real=LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Max-Time %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}
	
	atxmlw_Close1641Xml(&m_SignalDescription);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateIcPCIDM
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
void CIcPCIDM_T::InitPrivateIcPCIDM(void)
{
	m_LocalIP.Exists=false;
	m_LocalMask.Exists=false;
	m_LocalGateway.Exists=false;
	m_RemoteIP.Exists=false;
	m_RemotePort.Exists=false;
	m_DataSize.Exists=false;
	m_MaxTime.Exists=false;
	m_Attribute.Exists=false;
	//m_TransType=PCIDM_TACFIRE;
	m_RetData[0]='\0';
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataIcPCIDM
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
void CIcPCIDM_T::NullCalDataIcPCIDM(void)
{
    //////// Place IcPCIDM specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE */
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    return;
}
//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

