//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Mic_TPMC806_T.cpp
//
// Date:	    26JUN06
//
// Purpose:	    ATXMLW Instrument Driver for Mic_TPMC806
//
// Instrument:	Mic_TPMC806  <Device Description> (<device Type>)
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
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "AtxmlWrapper.h"
//#include "BCCDLLInterface.h"
#include "Mic_T.h"

#define DATA    1
#define ALL		2
// Function codes

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables

// Local Function Prototypes
extern void stuff_string(char * string); //defined in CDDI_T.cpp
extern void unstuff_string(char * string); //defined in CDDI_T.cpp
extern void bin_string_to_array(char input [], int length, int array []); //defined in Can_T.cpp
extern void array_to_bin_string(int array [], int size, int length, char output []); //defined in Can_T.cpp

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CMic_TPMC806_T
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

CMic_TPMC806_T::CMic_TPMC806_T(int Instno, int ResourceType, char* ResourceName,
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
	if(m_myhDLL == NULL)
	{
		atxmlw_ErrorResponse(m_ResourceName, Response, Buffersize, "Instrument Error ",
                           Status, "Could not load the DLL");
		return;
	}

    m_InitString[0] = '\0';

    InitPrivateMic_TPMC806();
	NullCalDataMic_TPMC806();

    // The Form Init String
    sprintf(m_InitString,"%s",
              m_AddressInfo.ControllerType);
    sprintf(LclMsg,"Wrap-CMic_TPMC806 Class called with Instno %d, Sim %d, Dbg %d", 
                                      m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);

    // Initialize the Mic_TPMC806
	

	//if (!m_Sim && InitMicFunctionPointers(m_myhDLL) != 0) 
	//{ 
	//	atxmlw_ErrorResponse(m_ResourceName, Response, Buffersize, "Instrument Error ",
 //                          -1, "InitMicFunctionPointers failed.");
	//	return;
 //   } 
	
    if(Status && ErrorMic_TPMC806(Status, Response, Buffersize))
        return;
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CMic_TPMC806_T()
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
CMic_TPMC806_T::~CMic_TPMC806_T()
{
    char Dummy[1024];

    // Reset the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CMic_TPMC806 Class Distructor called "),Dummy,1024);

	FreeLibrary(m_myhDLL);
	
	//WSACleanup();

    return;
}

///////////////////////////////////////////////////////////////////////////////
// CODE_CHECK: ANTHONY ADDED THIS
int CMic_TPMC806_T::Open(char *VXIAddress)
{

	//IFNSIM(m_Handle=MicOpen(VXIAddress));
	//if(m_Handle==BCC_ERROR)
//	{ 
////		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
////                          BCC_ERROR, "CddiOpen() failed.");
//		return(BCC_ERROR);
//    }
	return 0;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusMic_TPMC806
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
int CMic_TPMC806_T::StatusMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";

    // Status action for the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusMic_TPMC806 called "), Response, BufferSize);
    // Check for any pending error messages
    IFNSIM(Status = ErrorMic_TPMC806(0, Response, BufferSize));

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalMic_TPMC806
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
int CMic_TPMC806_T::IssueSignalMic_TPMC806(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

    // IEEE 1641 Issue Signal action for the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalMic_TPMC806 Signal: "),
                              Response, BufferSize);

    if((Status = GetStmtInfoMic_TPMC806(SignalDescription, Response, BufferSize)) != 0)
        return(Status);

    switch(m_Action)
    {
    case ATXMLW_SA_APPLY:
        if((Status = SetupMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        if((Status = EnableMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_REMOVE:
        if((Status = DisableMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        if((Status = ResetMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_MEASURE:
        if((Status = SetupMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_READ:
        if((Status = EnableMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        if((Status = FetchMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_RESET:
        if((Status = ResetMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_SETUP:
        if((Status = SetupMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_CONNECT:
        break;
    case ATXMLW_SA_ENABLE:
        if((Status = EnableMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_DISABLE:
        if((Status = DisableMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_FETCH:
        if((Status = FetchMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_DISCONNECT:
        break;
	case ATXMLW_SA_STATUS:
        if((Status = StatusMic_TPMC806(Response, BufferSize)) != 0)
            return(Status);
        break;
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalMic_TPMC806
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
int CMic_TPMC806_T::RegCalMic_TPMC806(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
    char      Dummy[1024];

    // Setup action for the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalMic_TPMC806 CalData: %s", 
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
// Function: ResetMic_TPMC806
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
int CMic_TPMC806_T::ResetMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";

    // Reset action for the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetMic_TPMC806 called "), Response, BufferSize);
	// Reset the Mic_TPMC806
	
    if(Status)
        ErrorMic_TPMC806(Status, Response, BufferSize);

	//WSASetLastError(0);
    InitPrivateMic_TPMC806();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstMic_TPMC806
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
int CMic_TPMC806_T::IstMic_TPMC806(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;

    // Reset action for the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstMic_TPMC806 called Level %d", 
                              Level), Response, BufferSize);
    // Reset the Mic_TPMC806
    //////// Place Mic_TPMC806 specific data here //////////////
    switch(Level)
    {
    case ATXMLW_IST_LVL_PST:
        Status = StatusMic_TPMC806(Response,BufferSize);
        break;
    case ATXMLW_IST_LVL_IST:
/* Begin TEMPLATE_SAMPLE_CODE 
        IFNSIM((Status = Mic_TPMC806_IST(m_Handle)));
/* End TEMPLATE_SAMPLE_CODE */
        break;
    case ATXMLW_IST_LVL_CNF:
        Status = StatusMic_TPMC806(Response,BufferSize);
        break;
    default: // Hopefully BIT 1-9
/* Begin TEMPLATE_SAMPLE_CODE 
        IFNSIM((Status = Mic_TPMC806_BIT(m_Handle, (Level-ATXMLW_IST_LVL_BIT))));
/* End TEMPLATE_SAMPLE_CODE */
        break;
    }
/* Begin TEMPLATE_SAMPLE_CODE 
    IFNSIM((Status = Mic_TPMC806_PnPreset(m_Handle)));
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    if(Status)
        ErrorMic_TPMC806(Status, Response, BufferSize);

    InitPrivateMic_TPMC806();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsMic_TPMC806
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
int CMic_TPMC806_T::IssueNativeCmdsMic_TPMC806(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsMic_TPMC806 "), Response, BufferSize);

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
// Function: IssueDriverFunctionCallMic_TPMC806
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
int CMic_TPMC806_T::IssueDriverFunctionCallMic_TPMC806(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the Mic_TPMC806
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallMic_TPMC806", Response, BufferSize);

    // Retrieve the CalData
/* Begin TEMPLATE_SAMPLE_CODE 
    if((Status = atxmlw_IssueDriverFunctionCallMic_TPMC806(m_Handle, DriverFunction,
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
// Function: SetupMic_TPMC806
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
int CMic_TPMC806_T::SetupMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	//DeviceInfoStruct infoStruct;
	//CBTSError error;

    // Setup action for the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupMic_TPMC806 called "), Response, BufferSize);

//	memset(&infoStruct,0,sizeof(infoStruct));

	//if(m_Channel.Exists)
	//{
	//	infoStruct.channelNumber = m_Channel.Int;
	//}
	//else
	//{
	//	infoStruct.channelNumber = 1;
	//}

	//if(m_BusTimeout.Exists)
	//{
	//	infoStruct.MICOtherRegisters.BusTimeoutCounter=m_BusTimeout.Int;
	//}

	//if(m_NoCommandTimeout.Exists)
	//{
	//	infoStruct.MICOtherRegisters.NoCommandTimeoutCounter=m_NoCommandTimeout.Int;
	//}

	//if(m_NoResponseTimeout.Exists)
	//{
	//	infoStruct.MICOtherRegisters.NoResponseTimeoutCounter=m_NoResponseTimeout.Int;
	//}

	//if(m_InterruptAckTimeout.Exists)
	//{
	//	infoStruct.MICOtherRegisters.InterruptAckTimeoutCounter=m_InterruptAckTimeout.Int;
	//}

	//if(m_BaseVector.Exists)
	//{
	//	infoStruct.MICOtherRegisters.BaseVectorNumber=m_BaseVector.Int;
	//}

	//if(m_AddressReg.Exists)
	//{
	//	infoStruct.MICOtherRegisters.ModuleAddressRegister=m_AddressReg.Int;
	//}

	//if(m_Command.Exists)
	//{
	//	infoStruct.MICSetupRegister.ResetStandby = ((m_Command.Int >> 16) & 0x0001);
 //   	infoStruct.MICSetupRegister.SetinStandbyMode = ((m_Command.Int >> 15) & 0x0001);
	//	infoStruct.MICSetupRegister.SelectModuleAddressRegister = ((m_Command.Int >> 14) & 0x0001);
	//	infoStruct.MICSetupRegister.ReceiveMultipleWords = ((m_Command.Int >> 13) & 0x0001);
	//	infoStruct.MICSetupRegister.DisableReceiving = ((m_Command.Int >> 12) & 0x0001);
	//	infoStruct.MICSetupRegister.ReceiveBufferControl = ((m_Command.Int >> 11) & 0x0001);
	//	infoStruct.MICSetupRegister.DisableTimeouts = ((m_Command.Int >> 10) & 0x0001);
	//	infoStruct.MICSetupRegister.InterruptonModuleAddressOnly = ((m_Command.Int >> 9) & 0x0001);
	//	infoStruct.MICSetupRegister.InterruptonResponsesOnly = ((m_Command.Int >> 8) & 0x0001);
	//	infoStruct.MICSetupRegister.ReceiveMultipleResponses = ((m_Command.Int >> 7) & 0x0001);
	//	infoStruct.MICSetupRegister.ClearBuffers = ((m_Command.Int >> 6) & 0x0001);
	//	infoStruct.MICSetupRegister.MonitorMode = ((m_Command.Int >> 5) & 0x0001);
	//	infoStruct.MICSetupRegister.BroadcastTransmit = ((m_Command.Int >> 4) & 0x0001);
	//	infoStruct.MICSetupRegister.SoftReset = ((m_Command.Int >> 3) & 0x0001);
	//	infoStruct.MICSetupRegister.DisableBusB = ((m_Command.Int >> 2) & 0x0001);
	//	infoStruct.MICSetupRegister.DisableBusA = ((m_Command.Int >> 1) & 0x0001);
	//	infoStruct.MICSetupRegister.Transmit = (m_Command.Int & 0x0001);
	//}

	//IFNSIM(m_Handle=MicOpen("VXI::00::00"));		// CODE_CHECK: removed by AJP, an open method has been added
	//if(m_Handle==BCC_ERROR)
	//{ 
	//	atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
 //                          BCC_ERROR, "MicOpen() failed.");
	//	return(BCC_ERROR);
 //   } 
	//
	//if (!m_Sim && MicIoctl(m_Handle,&infoStruct) == BCC_ERROR)
 //   {
 //       IFNSIM(error = MicGetError(m_Handle));
 //       
	//	atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
 //                          error.code, atxmlw_FmtMsg("MicIoctl returned error code %d", error.code)); 
	//		
	//	//fprintf(stderr, "MicIoctl() failed with %d.\n", error); 
 //       return (error.code); 
 //   }
	
    if(Status)
        ErrorMic_TPMC806(Status, Response, BufferSize);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchMic_TPMC806
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
int CMic_TPMC806_T::FetchMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5000;
    char   *ErrMsg = "";
	int GetData=1;
	char attribute[10];
	char tempString[20];
	//CBTSError error;

	if(m_Sim)
	{
		strcpy(m_RetData, "THIS IS MY TEST MESSAGE");
	}

    // Fetch action for the Mic_TPMC806
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchMic_TPMC806 called "), Response, BufferSize);

	if(m_Attribute.Dim==DATA)
	{
		if(!m_MaxTime.Exists)
			m_MaxTime.Real=20000; //in milliseconds

		//IFNSIM(Status=MicRead(m_Handle, m_RetData, MAX_DATA, (int)m_MaxTime.Real));

		//if(Status==BCC_ERROR)
		//{
		//	IFNSIM(error = MicGetError(m_Handle));
	 //       atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
  //                         0, atxmlw_FmtMsg("MicRead returned error code %d", error.code)); 
		//	return 1; 
		//}

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
			if(m_BusTimeout.Exists)
			{
				atxmlw_ScalerIntegerReturn("busTimeout", "s", m_BusTimeout.Int,
						Response, BufferSize);
			}

			if(m_NoCommandTimeout.Exists)
			{
				atxmlw_ScalerIntegerReturn("noCommandTimeout", "s", m_NoCommandTimeout.Int,
						Response, BufferSize);
			}

			if(m_NoResponseTimeout.Exists)
			{
				atxmlw_ScalerIntegerReturn("noResponseTimeout", "s", m_NoResponseTimeout.Int,
						Response, BufferSize);
			}

			if(m_InterruptAckTimeout.Exists)
			{
				atxmlw_ScalerIntegerReturn("interruptAckTimeout", "s", m_InterruptAckTimeout.Int,
						Response, BufferSize);
			}

			if(m_BaseVector.Exists)
			{
				atxmlw_ScalerIntegerReturn("baseVector", "", m_BaseVector.Int,
						Response, BufferSize);
			}

			if(m_AddressReg.Exists)
			{
				atxmlw_ScalerIntegerReturn("addressReg", "", m_AddressReg.Int,
						Response, BufferSize);
			}

			if(m_Command.Exists)
			{
				tempString[0]='\0';
				array_to_bin_string(&m_Command.Int, 1, 17, tempString);
				atxmlw_ScalerStringReturn("command", "", tempString,
					Response, BufferSize);
			}

			if(m_Channel.Exists)
			{
				atxmlw_ScalerIntegerReturn("channel", "", m_Channel.Int,
						Response, BufferSize);
			}

  			if(m_MaxTime.Exists)
			{
				atxmlw_ScalerDoubleReturn("maxTime", "", m_MaxTime.Real,
						Response, BufferSize);
			}
			atxmlw_ScalerStringReturn("spec", "", "MIC",
					Response, BufferSize);
		}
	}
	return(0);

}



///////////////////////////////////////////////////////////////////////////////
// Function: DisableMic_TPMC806
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
int CMic_TPMC806_T::DisableMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	//CBTSError error;
 //   // Open action for the Mic_TPMC806
 //   ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenMic_TPMC806 called "), Response, BufferSize);

 //   if(!m_Sim && MicClose(m_Handle)==BCC_ERROR)
	//{ 
	//		IFNSIM(error=MicGetError(m_Handle));
 //           atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
	//						error.code, atxmlw_FmtMsg("MicClose returned error code %d", error.code));
 //           return(error.code); 
 //   }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableMic_TPMC806
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
int CMic_TPMC806_T::EnableMic_TPMC806(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status=0;
	//CBTSError error;
 //   // Close action for the Mic_TPMC806
 //   ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableMic_TPMC806 called "), Response, BufferSize);

	//if(m_DataSize.Exists)//send Data
	//{
	//	m_DataSize.Int=strlen(m_Data);
	//	IFNSIM(Status=MicWrite(m_Handle, m_Data, m_DataSize.Int));
	//	if(Status==BCC_ERROR)
	//	{
	//		IFNSIM(error=MicGetError(m_Handle));
	//		
	//		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
	//						0, atxmlw_FmtMsg("MicWrite returned error code %d", error.code));
	//		return 1;
	//	}
	//		
	//}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorMic_TPMC806
//
// Purpose: Query Mic_TPMC806 for the error text and send to WRTS
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
int  CMic_TPMC806_T::ErrorMic_TPMC806(int Status,
                                  ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    std::string Msg;
//	char     ErrorCodeBuff[16];
	//CBTSError error;
	////char     Msg[MAX_MSG_SIZE];
 //   //BCCErrorTypeEnum QError;
	////BCCErrorTypeEnum error;

    retval = Status;
 ////   Msg[0] = '\0';

	//if(Status)
 //   {
	//	error=MicGetError(m_Handle);
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

		QError=MicGetError(m_Handle);
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
// Function: GetStmtInfoMic_TPMC806
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
int CMic_TPMC806_T::GetStmtInfoMic_TPMC806(ATXMLW_INTF_SIGDESC* SignalDescription,
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

	//busTimeout
	if((m_BusTimeout.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "busTimeout",
                                         &LclDblValue, LclUnit)))
	{
		m_BusTimeout.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found timingValue %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}

    //noCommandTimeout
	if((m_NoCommandTimeout.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "noCommandTimeout",
                                         &LclDblValue, LclUnit)))
	{
		m_NoCommandTimeout.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found noCommandTimeout %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}

    //noResponseTimeout
	if((m_NoResponseTimeout.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "noResponseTimeout",
                                         &LclDblValue, LclUnit)))
	{
		m_NoResponseTimeout.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found noResponseTimeout %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}

	//interruptAckTimeout
	if((m_InterruptAckTimeout.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "interruptAckTimeout",
                                         &LclDblValue, LclUnit)))
	{
		m_InterruptAckTimeout.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found interruptAckTimeout %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}

	//baseVector
	if((m_BaseVector.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "baseVector",
                                         &LclDblValue, LclUnit)))
	{
		m_BaseVector.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found baseVector %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}

	//addressReg
	if((m_AddressReg.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "addressReg",
                                         &LclDblValue, LclUnit)))
	{
		m_AddressReg.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found addressReg %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}

	//channel
	if((m_Channel.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "channel",
                                         &LclDblValue, LclUnit)))
	{
		m_Channel.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found channel %E [%s]",
                              LclDblValue,LclUnit),
                              Response, BufferSize);
	}

	//command
	if((m_Command.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "command",
                                         &LclCharValuePtr)))
	{
		//m_AcceptanceMask.Int=(int)LclDblValue;
		bin_string_to_array(LclCharValuePtr, 17, &m_Command.Int);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found command %x",
								m_Command.Int),
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
// Function: InitPrivateMic_TPMC806
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
void CMic_TPMC806_T::InitPrivateMic_TPMC806(void)
{
	m_BusTimeout.Exists=false;
    m_NoCommandTimeout.Exists=false;
    m_NoResponseTimeout.Exists=false;
	m_InterruptAckTimeout.Exists=false;
	m_BaseVector.Exists=false;
	m_AddressReg.Exists=false;
	m_Command.Exists=false;
	m_DataSize.Exists=false;
	m_MaxTime.Exists=false;
	m_Attribute.Exists=false;
	m_Channel.Exists=false;
	m_RetData[0]='\0';
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataMic_TPMC806
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
void CMic_TPMC806_T::NullCalDataMic_TPMC806(void)
{
    //////// Place Mic_TPMC806 specific data here //////////////
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

